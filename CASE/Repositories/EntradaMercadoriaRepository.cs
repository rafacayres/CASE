using System;
using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class EntradaMercadoriaRepository
    {

        private const string Colunas = "id_entrada,id_fornecedor,id_local_destino,id_usuario_responsavel,tipo_entrada,data_hora,numero_documento,observacao,chave_operacao,criado_em,id_usuario_alteracao,atualizado_em,versao";

        public List<EntradaMercadoria> ListarPorLocalDestino(int idLocal)
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas + " FROM entradamercadoria WHERE id_local_destino=@id ORDER BY data_hora DESC,id_entrada DESC",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idLocal)
            );
        }

        public EntradaMercadoria BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT " + Colunas + " FROM entradamercadoria WHERE id_entrada=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public int CadastrarComItens(EntradaMercadoria entrada, IList<EntradaItem> itens)
        {
            if (entrada == null)
            {
                throw new ArgumentNullException(nameof(entrada));
            }

            if (itens == null)
            {
                throw new ArgumentNullException(nameof(itens));
            }

            if (itens.Count == 0)
            {
                throw new ArgumentException("A entrada deve possuir pelo menos um item.", nameof(itens));
            }

            if (string.IsNullOrWhiteSpace(entrada.ChaveOperacao))
            {
                entrada.ChaveOperacao = Guid.NewGuid().ToString();
            }

            if (!entrada.CriadoEm.HasValue)
            {
                entrada.CriadoEm = DateTime.Now;
            }

            using (var conexao = CASE.Data.Conexao.CriarConexao())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        ValidarAdministradorEEstoqueCentral(
                            conexao,
                            transacao,
                            entrada.IdUsuarioResponsavel,
                            entrada.IdLocalDestino);

                        int id = AcessoDados.Inserir(
                            conexao,
                            transacao,
                            "INSERT INTO entradamercadoria (id_fornecedor,id_local_destino,id_usuario_responsavel,tipo_entrada,data_hora,numero_documento,observacao,chave_operacao,criado_em) VALUES (@fornecedor,@local,@usuario,@tipo,@data,@documento,@observacao,@chave,@criadoEm)",
                            comando => Vincular(comando, entrada)
                        );
                        List<Tuple<int, int, decimal>> ajustesSaldo =
                            new List<Tuple<int, int, decimal>>();

                        foreach (EntradaItem item in itens)
                        {
                            if (item == null)
                            {
                                throw new ArgumentException("A lista contem um item nulo.", nameof(itens));
                            }

                            ValidarQuantidade(item.Quantidade);

                            AcessoDados.Executar(
                                conexao,
                                transacao,
                                "INSERT INTO entradaitem (id_entrada,id_produto,quantidade,custo_unitario) VALUES (@entrada,@produto,@quantidade,@custo)",
                                comando =>
                                {
                                    AcessoDados.AdicionarParametro(comando, "@entrada", id);
                                    AcessoDados.AdicionarParametro(comando, "@produto", item.IdProduto);
                                    AcessoDados.AdicionarParametro(comando, "@quantidade", item.Quantidade);
                                    AcessoDados.AdicionarParametro(comando, "@custo", item.CustoUnitario);
                                });

                            ajustesSaldo.Add(Tuple.Create(
                                entrada.IdLocalDestino,
                                item.IdProduto,
                                item.Quantidade));

                        }

                        ControleSaldoEstoque.AplicarAjustes(
                            conexao,
                            transacao,
                            ajustesSaldo);

                        transacao.Commit();
                        return id;
                    }
                    catch
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }
        }

        public void AtualizarComItens(
            EntradaMercadoria entrada,
            IList<EntradaItem> itens,
            int idUsuarioAlteracao,
            string justificativa)
        {
            throw new InvalidOperationException(
                "Movimentações confirmadas são imutáveis. A correção exige uma nova operação auditada.");
        }

        private static EntradaMercadoria BuscarParaAtualizacao(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idEntrada)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT " + Colunas + " FROM entradamercadoria WHERE id_entrada=@id FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idEntrada);

                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    if (!leitor.Read())
                    {
                        return null;
                    }

                    return ConverterLinha(leitor);
                }
            }
        }

        private static List<Tuple<int, int, decimal>> CarregarAjustesAnteriores(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idEntrada)
        {
            int idLocal;
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT id_local_destino FROM entradamercadoria WHERE id_entrada=@id",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idEntrada);
                idLocal = Convert.ToInt32(comando.ExecuteScalar());
            }

            List<Tuple<int, int, decimal>> ajustes = new List<Tuple<int, int, decimal>>();

            using (MySqlCommand comando = new MySqlCommand(
                "SELECT id_produto,quantidade FROM entradaitem WHERE id_entrada=@id FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idEntrada);

                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        ajustes.Add(Tuple.Create(
                            idLocal,
                            leitor.GetInt32("id_produto"),
                            -leitor.GetDecimal("quantidade")));
                    }
                }
            }

            return ajustes;
        }

        private static string CriarSnapshot(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idEntrada)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT CAST(JSON_OBJECT('id_fornecedor',em.id_fornecedor," +
                "'id_local_destino',em.id_local_destino,'id_usuario_responsavel',em.id_usuario_responsavel," +
                "'tipo_entrada',em.tipo_entrada,'data_hora',em.data_hora,'numero_documento',em.numero_documento," +
                "'observacao',em.observacao,'itens',(SELECT JSON_ARRAYAGG(JSON_OBJECT(" +
                "'id_produto',ei.id_produto,'quantidade',ei.quantidade,'custo_unitario',ei.custo_unitario)) " +
                "FROM entradaitem ei WHERE ei.id_entrada=em.id_entrada)) AS CHAR) " +
                "FROM entradamercadoria em WHERE em.id_entrada=@id",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idEntrada);
                return Convert.ToString(comando.ExecuteScalar());
            }
        }

        private static void RegistrarHistorico(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idEntrada,
            int idUsuario,
            string justificativa,
            string dadosAnteriores,
            string dadosNovos)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "INSERT INTO historicoalteracaooperacao " +
                "(entidade,id_entidade,id_usuario,justificativa,dados_anteriores,dados_novos) " +
                "VALUES ('entradamercadoria',@id,@usuario,@justificativa,CAST(@anterior AS JSON),CAST(@novo AS JSON))",
                conexao,
                transacao))
            {
                AcessoDados.AdicionarParametro(comando, "@id", idEntrada);
                AcessoDados.AdicionarParametro(comando, "@usuario", idUsuario);
                AcessoDados.AdicionarTextoOpcional(comando, "@justificativa", justificativa);
                comando.Parameters.AddWithValue("@anterior", dadosAnteriores);
                comando.Parameters.AddWithValue("@novo", dadosNovos);
                comando.ExecuteNonQuery();
            }
        }

        private static void Vincular(MySqlCommand comando, EntradaMercadoria item)
        {
            AcessoDados.AdicionarParametro(comando, "@fornecedor", item.IdFornecedor);
            AcessoDados.AdicionarParametro(comando, "@local", item.IdLocalDestino);
            AcessoDados.AdicionarParametro(comando, "@usuario", item.IdUsuarioResponsavel);
            AcessoDados.AdicionarTextoObrigatorio(comando, "@tipo", item.TipoEntrada);
            AcessoDados.AdicionarParametro(comando, "@data", item.DataHora);
            AcessoDados.AdicionarTextoOpcional(comando, "@documento", item.NumeroDocumento);
            AcessoDados.AdicionarTextoOpcional(comando, "@observacao", item.Observacao);
            AcessoDados.AdicionarTextoObrigatorio(comando, "@chave", item.ChaveOperacao);
            AcessoDados.AdicionarParametro(comando, "@criadoEm", item.CriadoEm);
        }

        private static void ValidarAdministradorEEstoqueCentral(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idUsuario,
            int idLocal)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT le.id_local_estoque FROM localestoque le " +
                "JOIN usuario u ON u.id_usuario=@usuario " +
                "JOIN perfilsistema ps ON ps.id_perfil_sistema=u.id_perfil_sistema " +
                "WHERE le.id_local_estoque=@local AND le.eh_central=1 " +
                "AND le.eh_geral_evento=0 AND le.id_evento IS NULL AND le.ativo=1 " +
                "AND u.situacao_conta='aprovado' AND ps.nome='Administrador' " +
                "FOR UPDATE",
                conexao,
                transacao))
            {
                AcessoDados.AdicionarParametro(comando, "@usuario", idUsuario);
                AcessoDados.AdicionarParametro(comando, "@local", idLocal);

                if (comando.ExecuteScalar() == null)
                {
                    throw new UnauthorizedAccessException(
                        "A entrada no estoque central exige administrador aprovado e local central ativo.");
                }
            }
        }

        private static void ValidarQuantidade(decimal quantidade)
        {
            if (quantidade <= 0 || quantidade != decimal.Truncate(quantidade))
            {
                throw new ArgumentException(
                    "A quantidade da entrada deve ser um número inteiro maior que zero.");
            }
        }

        private static EntradaMercadoria ConverterLinha(MySqlDataReader leitor)
        {
            return new EntradaMercadoria
            {
                IdEntrada = leitor.GetInt32("id_entrada"),
                IdFornecedor = AcessoDados.LerInteiroOpcional(leitor, "id_fornecedor"),
                IdLocalDestino = leitor.GetInt32("id_local_destino"),
                IdUsuarioResponsavel = leitor.GetInt32("id_usuario_responsavel"),
                TipoEntrada = leitor.GetString("tipo_entrada"),
                DataHora = leitor.GetDateTime("data_hora"),
                NumeroDocumento = AcessoDados.LerTextoOpcional(leitor, "numero_documento"),
                Observacao = AcessoDados.LerTextoOpcional(leitor, "observacao"),
                ChaveOperacao = AcessoDados.LerTextoOpcional(leitor, "chave_operacao"),
                CriadoEm = AcessoDados.LerDataHoraOpcional(leitor, "criado_em"),
                IdUsuarioAlteracao = AcessoDados.LerInteiroOpcional(leitor, "id_usuario_alteracao"),
                AtualizadoEm = AcessoDados.LerDataHoraOpcional(leitor, "atualizado_em"),
                Versao = leitor.GetUInt32("versao")
            };
        }
    }
}
