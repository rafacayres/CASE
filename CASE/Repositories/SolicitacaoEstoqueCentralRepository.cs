using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using CASE.Data;
using CASE.Models;
using CASE.Services;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class SolicitacaoEstoqueCentralRepository
    {
        private const string Colunas =
            "id_solicitacao_estoque_central,tipo,id_instituicao,id_evento," +
            "id_local_origem,id_local_destino,id_usuario_solicitante,data_hora," +
            "situacao,id_usuario_decisor,decidida_em,justificativa_rejeicao,versao";

        public int CadastrarComItens(
            SolicitacaoEstoqueCentral solicitacao,
            IList<SolicitacaoEstoqueCentralItem> itens)
        {
            if (solicitacao == null)
            {
                throw new ArgumentNullException(nameof(solicitacao));
            }

            if (itens == null || itens.Count == 0)
            {
                throw new ArgumentException(
                    "A solicitação precisa possuir itens.",
                    nameof(itens));
            }

            if (solicitacao.Tipo != TiposSolicitacaoEstoqueCentral.Liberacao &&
                solicitacao.Tipo != TiposSolicitacaoEstoqueCentral.Devolucao)
            {
                throw new ArgumentException(
                    "O tipo da solicitação administrativa não é válido.",
                    nameof(solicitacao));
            }

            ValidarItensParaGravacao(itens);

            using (MySqlConnection conexao = Conexao.CriarConexao())
            {
                conexao.Open();

                using (MySqlTransaction transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        List<SolicitacaoEstoqueCentralItem> itensOrdenados =
                            new List<SolicitacaoEstoqueCentralItem>(itens);
                        itensOrdenados.Sort(CompararItensPorProduto);

                        ValidarRelacionamentosDaSolicitacao(
                            conexao,
                            transacao,
                            solicitacao);

                        ValidarEstoqueDisponivel(
                            conexao,
                            transacao,
                            solicitacao.IdLocalOrigem,
                            itensOrdenados);

                        int idSolicitacao = AcessoDados.Inserir(
                            conexao,
                            transacao,
                            "INSERT INTO solicitacaoestoquecentral " +
                            "(tipo,id_instituicao,id_evento,id_local_origem,id_local_destino," +
                            "id_usuario_solicitante,data_hora,situacao,versao) " +
                            "VALUES (@tipo,@instituicao,@evento,@origem,@destino," +
                            "@solicitante,@data,@situacao,1)",
                            comando =>
                            {
                                AcessoDados.AdicionarTextoObrigatorio(
                                    comando,
                                    "@tipo",
                                    solicitacao.Tipo);
                                AcessoDados.AdicionarParametro(
                                    comando,
                                    "@instituicao",
                                    solicitacao.IdInstituicao);
                                AcessoDados.AdicionarParametro(
                                    comando,
                                    "@evento",
                                    solicitacao.IdEvento);
                                AcessoDados.AdicionarParametro(
                                    comando,
                                    "@origem",
                                    solicitacao.IdLocalOrigem);
                                AcessoDados.AdicionarParametro(
                                    comando,
                                    "@destino",
                                    solicitacao.IdLocalDestino);
                                AcessoDados.AdicionarParametro(
                                    comando,
                                    "@solicitante",
                                    solicitacao.IdUsuarioSolicitante);
                                AcessoDados.AdicionarParametro(
                                    comando,
                                    "@data",
                                    solicitacao.DataHora);
                                AcessoDados.AdicionarTextoObrigatorio(
                                    comando,
                                    "@situacao",
                                    SituacoesSolicitacao.Pendente);
                            });

                        foreach (SolicitacaoEstoqueCentralItem item in itensOrdenados)
                        {
                            AcessoDados.Executar(
                                conexao,
                                transacao,
                                "INSERT INTO solicitacaoestoquecentralitem " +
                                "(id_solicitacao_estoque_central,id_produto,quantidade) " +
                                "VALUES (@solicitacao,@produto,@quantidade)",
                                comando =>
                                {
                                    AcessoDados.AdicionarParametro(
                                        comando,
                                        "@solicitacao",
                                        idSolicitacao);
                                    AcessoDados.AdicionarParametro(
                                        comando,
                                        "@produto",
                                        item.IdProduto);
                                    AcessoDados.AdicionarParametro(
                                        comando,
                                        "@quantidade",
                                        item.Quantidade);
                                });
                        }

                        transacao.Commit();
                        return idSolicitacao;
                    }
                    catch
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<SolicitacaoEstoqueCentral> ListarPendentes()
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas +
                " FROM solicitacaoestoquecentral " +
                "WHERE situacao=@situacao ORDER BY data_hora,id_solicitacao_estoque_central",
                ConverterLinha,
                comando => AcessoDados.AdicionarTextoObrigatorio(
                    comando,
                    "@situacao",
                    SituacoesSolicitacao.Pendente));
        }

        public List<SolicitacaoEstoqueCentral> ListarAprovadasSemAtendimento()
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas +
                " FROM solicitacaoestoquecentral s " +
                "WHERE s.situacao=@situacao AND NOT EXISTS (" +
                "SELECT 1 FROM transferenciaestoque t " +
                "WHERE t.id_solicitacao_estoque_central=s.id_solicitacao_estoque_central) " +
                "ORDER BY s.data_hora,s.id_solicitacao_estoque_central",
                ConverterLinha,
                comando => AcessoDados.AdicionarTextoObrigatorio(
                    comando,
                    "@situacao",
                    SituacoesSolicitacao.Aprovada));
        }

        public List<SolicitacaoEstoqueCentral> ListarPorSolicitante(
            int idUsuarioSolicitante)
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas +
                " FROM solicitacaoestoquecentral " +
                "WHERE id_usuario_solicitante=@usuario " +
                "ORDER BY data_hora DESC,id_solicitacao_estoque_central DESC",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(
                    comando,
                    "@usuario",
                    idUsuarioSolicitante));
        }

        public SolicitacaoEstoqueCentral BuscarPorId(int idSolicitacao)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT " + Colunas +
                " FROM solicitacaoestoquecentral " +
                "WHERE id_solicitacao_estoque_central=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(
                    comando,
                    "@id",
                    idSolicitacao));
        }

        public List<SolicitacaoEstoqueCentralItem> ListarItens(
            int idSolicitacao)
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_solicitacao_estoque_central_item," +
                "id_solicitacao_estoque_central,id_produto,quantidade " +
                "FROM solicitacaoestoquecentralitem " +
                "WHERE id_solicitacao_estoque_central=@id ORDER BY id_produto",
                ConverterItem,
                comando => AcessoDados.AdicionarParametro(
                    comando,
                    "@id",
                    idSolicitacao));
        }

        public int? BuscarIdTransferenciaAtendimento(int idSolicitacao)
        {
            List<int> transferencias = AcessoDados.ConsultarLista(
                "SELECT id_transferencia FROM transferenciaestoque " +
                "WHERE id_solicitacao_estoque_central=@id",
                leitor => leitor.GetInt32("id_transferencia"),
                comando => AcessoDados.AdicionarParametro(
                    comando,
                    "@id",
                    idSolicitacao));

            if (transferencias.Count == 0)
            {
                return null;
            }

            if (transferencias.Count > 1)
            {
                throw new InvalidOperationException(
                    "A solicitação está vinculada a mais de uma transferência.");
            }

            return transferencias[0];
        }

        public void RegistrarDecisao(
            int idSolicitacao,
            uint versao,
            int idUsuarioDecisor,
            string situacao,
            string justificativa)
        {
            if (situacao != SituacoesSolicitacao.Aprovada &&
                situacao != SituacoesSolicitacao.Rejeitada)
            {
                throw new ArgumentException(
                    "A decisão deve aprovar ou rejeitar o pedido.",
                    nameof(situacao));
            }

            if (situacao == SituacoesSolicitacao.Rejeitada &&
                string.IsNullOrWhiteSpace(justificativa))
            {
                throw new ArgumentException(
                    "A rejeição exige uma justificativa.",
                    nameof(justificativa));
            }

            if (!string.IsNullOrWhiteSpace(justificativa) && justificativa.Trim().Length > 500)
            {
                throw new ArgumentException(
                    "A justificativa pode ter no máximo 500 caracteres.",
                    nameof(justificativa));
            }

            using (MySqlConnection conexao = Conexao.CriarConexao())
            {
                conexao.Open();

                using (MySqlTransaction transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        ValidarAdministrador(conexao, transacao, idUsuarioDecisor);

                        SolicitacaoEstoqueCentral solicitacao = null;

                        using (MySqlCommand comando = new MySqlCommand(
                            "SELECT tipo,id_instituicao,id_evento,id_local_origem," +
                            "id_local_destino,id_usuario_solicitante,situacao,versao " +
                            "FROM solicitacaoestoquecentral " +
                            "WHERE id_solicitacao_estoque_central=@id FOR UPDATE",
                            conexao,
                            transacao))
                        {
                            AcessoDados.AdicionarParametro(comando, "@id", idSolicitacao);

                            using (MySqlDataReader leitor = comando.ExecuteReader())
                            {
                                if (leitor.Read())
                                {
                                    solicitacao = new SolicitacaoEstoqueCentral();
                                    solicitacao.Tipo = leitor.GetString("tipo");
                                    solicitacao.IdInstituicao = leitor.GetInt32("id_instituicao");
                                    solicitacao.IdEvento = leitor.GetInt32("id_evento");
                                    solicitacao.IdLocalOrigem = leitor.GetInt32("id_local_origem");
                                    solicitacao.IdLocalDestino = leitor.GetInt32("id_local_destino");
                                    solicitacao.IdUsuarioSolicitante = leitor.GetInt32(
                                        "id_usuario_solicitante");
                                    solicitacao.Situacao = leitor.GetString("situacao");
                                    solicitacao.Versao = leitor.GetUInt32("versao");
                                }
                            }
                        }

                        if (solicitacao == null)
                        {
                            throw new InvalidOperationException(
                                "A solicitação não foi encontrada.");
                        }

                        if (!string.Equals(
                                solicitacao.Situacao,
                                SituacoesSolicitacao.Pendente,
                                StringComparison.OrdinalIgnoreCase) ||
                            solicitacao.Versao != versao)
                        {
                            throw new InvalidOperationException(
                                "A solicitação foi decidida ou alterada. Atualize os dados.");
                        }

                        if (solicitacao.IdUsuarioSolicitante == idUsuarioDecisor)
                        {
                            throw new UnauthorizedAccessException(
                                "O solicitante não pode decidir o próprio pedido.");
                        }

                        ValidarRelacionamentosDaSolicitacao(
                            conexao,
                            transacao,
                            solicitacao);

                        int linhasAfetadas = AcessoDados.Executar(
                            conexao,
                            transacao,
                            "UPDATE solicitacaoestoquecentral " +
                            "SET situacao=@situacao,id_usuario_decisor=@usuario," +
                            "decidida_em=NOW(),justificativa_rejeicao=@justificativa," +
                            "versao=versao+1 " +
                            "WHERE id_solicitacao_estoque_central=@id " +
                            "AND situacao=@pendente AND versao=@versao",
                            comando =>
                            {
                                AcessoDados.AdicionarTextoObrigatorio(
                                    comando,
                                    "@situacao",
                                    situacao);
                                AcessoDados.AdicionarParametro(
                                    comando,
                                    "@usuario",
                                    idUsuarioDecisor);
                                AcessoDados.AdicionarTextoOpcional(
                                    comando,
                                    "@justificativa",
                                    justificativa);
                                AcessoDados.AdicionarParametro(comando, "@id", idSolicitacao);
                                AcessoDados.AdicionarTextoObrigatorio(
                                    comando,
                                    "@pendente",
                                    SituacoesSolicitacao.Pendente);
                                AcessoDados.AdicionarParametro(comando, "@versao", versao);
                            });

                        if (linhasAfetadas != 1)
                        {
                            throw new InvalidOperationException(
                                "A solicitação foi alterada por outra pessoa. Atualize os dados.");
                        }

                        RegistrarHistoricoDecisao(
                            conexao,
                            transacao,
                            idSolicitacao,
                            idUsuarioDecisor,
                            situacao,
                            justificativa);

                        transacao.Commit();
                    }
                    catch
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }
        }

        private static void ValidarEstoqueDisponivel(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idLocalCentral,
            IList<SolicitacaoEstoqueCentralItem> itens)
        {
            List<string> insuficiencias = new List<string>();

            foreach (SolicitacaoEstoqueCentralItem item in itens)
            {
                AcessoDados.Executar(
                    conexao,
                    transacao,
                    "INSERT INTO estoque " +
                    "(id_local_estoque,id_produto,quantidade_minima,quantidade_ideal,quantidade_atual) " +
                    "VALUES (@local,@produto,0,0,0) " +
                    "ON DUPLICATE KEY UPDATE id_estoque=id_estoque",
                    comando =>
                    {
                        AcessoDados.AdicionarParametro(comando, "@local", idLocalCentral);
                        AcessoDados.AdicionarParametro(comando, "@produto", item.IdProduto);
                    });

                string nomeProduto;
                decimal quantidadeDisponivel = 0;

                using (MySqlCommand comando = new MySqlCommand(
                    "SELECT p.nome,e.quantidade_atual " +
                    "FROM produto p " +
                    "JOIN estoque e ON e.id_produto=p.id_produto " +
                    "AND e.id_local_estoque=@local " +
                    "WHERE p.id_produto=@produto FOR UPDATE",
                    conexao,
                    transacao))
                {
                    AcessoDados.AdicionarParametro(comando, "@local", idLocalCentral);
                    AcessoDados.AdicionarParametro(comando, "@produto", item.IdProduto);

                    using (MySqlDataReader leitor = comando.ExecuteReader())
                    {
                        if (!leitor.Read())
                        {
                            throw new InvalidOperationException(
                                "Um dos produtos da solicitação não foi encontrado.");
                        }

                        nomeProduto = leitor.GetString("nome");

                        if (!leitor.IsDBNull(leitor.GetOrdinal("quantidade_atual")))
                        {
                            quantidadeDisponivel =
                                leitor.GetDecimal("quantidade_atual");
                        }
                    }
                }

                if (quantidadeDisponivel < item.Quantidade)
                {
                    decimal quantidadeFaltante =
                        item.Quantidade - quantidadeDisponivel;
                    string mensagem = string.Format(
                        CultureInfo.CurrentCulture,
                        "{0}: solicitado {1:N0}, disponível {2:N0}, faltam {3:N0}.",
                        nomeProduto,
                        item.Quantidade,
                        quantidadeDisponivel,
                        quantidadeFaltante);
                    insuficiencias.Add(mensagem);
                }
            }

            if (insuficiencias.Count > 0)
            {
                StringBuilder mensagemCompleta = new StringBuilder();
                mensagemCompleta.AppendLine(
                    "O local de origem não possui saldo suficiente para todos os itens.");

                foreach (string insuficiencia in insuficiencias)
                {
                    mensagemCompleta.AppendLine(insuficiencia);
                }

                throw new InvalidOperationException(mensagemCompleta.ToString().Trim());
            }
        }

        private static void ValidarRelacionamentosDaSolicitacao(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            SolicitacaoEstoqueCentral solicitacao)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT ev.id_evento " +
                "FROM evento ev " +
                "JOIN usuario u ON u.id_usuario=@solicitante " +
                "JOIN pessoa p ON p.id_pessoa=u.id_pessoa " +
                "JOIN localestoque origem " +
                "ON origem.id_local_estoque=@origem " +
                "JOIN localestoque destino " +
                "ON destino.id_local_estoque=@destino " +
                "JOIN perfilsistema perfil ON perfil.id_perfil_sistema=u.id_perfil_sistema " +
                "WHERE ev.id_evento=@evento " +
                "AND ev.id_instituicao=@instituicao " +
                "AND ev.id_responsavel=p.id_pessoa " +
                "AND u.situacao_conta='aprovado' AND perfil.nome='Gerente' " +
                "AND ev.situacao IN ('planejado','em_andamento') " +
                "AND origem.id_instituicao=ev.id_instituicao " +
                "AND origem.ativo=1 " +
                "AND destino.id_instituicao=ev.id_instituicao " +
                "AND destino.ativo=1 " +
                "AND ((@tipo='liberacao' AND origem.eh_central=1 " +
                "AND origem.id_evento IS NULL " +
                "AND origem.eh_geral_evento=0 AND destino.eh_central=0 " +
                "AND destino.eh_geral_evento=1 AND destino.id_evento=ev.id_evento) " +
                "OR (@tipo='devolucao' AND origem.eh_central=0 " +
                "AND origem.eh_geral_evento=1 AND destino.eh_central=1 " +
                "AND origem.id_evento=ev.id_evento " +
                "AND destino.eh_geral_evento=0 AND destino.id_evento IS NULL)) " +
                "FOR UPDATE",
                conexao,
                transacao))
            {
                AcessoDados.AdicionarParametro(
                    comando,
                    "@solicitante",
                    solicitacao.IdUsuarioSolicitante);
                AcessoDados.AdicionarParametro(
                    comando,
                    "@origem",
                    solicitacao.IdLocalOrigem);
                AcessoDados.AdicionarParametro(
                    comando,
                    "@destino",
                    solicitacao.IdLocalDestino);
                AcessoDados.AdicionarParametro(
                    comando,
                    "@evento",
                    solicitacao.IdEvento);
                AcessoDados.AdicionarParametro(
                    comando,
                    "@instituicao",
                    solicitacao.IdInstituicao);
                AcessoDados.AdicionarTextoObrigatorio(
                    comando,
                    "@tipo",
                    solicitacao.Tipo);

                object resultado = comando.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                {
                    throw new InvalidOperationException(
                        "O evento, o gerente solicitante e os locais de estoque não possuem vínculos válidos.");
                }
            }
        }

        private static void RegistrarHistoricoDecisao(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idSolicitacao,
            int idUsuario,
            string situacao,
            string justificativa)
        {
            AcessoDados.Executar(
                conexao,
                transacao,
                "INSERT INTO historicoalteracaooperacao " +
                "(entidade,id_entidade,id_usuario,justificativa,dados_anteriores,dados_novos) " +
                "VALUES ('solicitacaoestoquecentral',@id,@usuario,@justificativa," +
                "JSON_OBJECT('situacao','pendente')," +
                "JSON_OBJECT('situacao',@situacao,'justificativa',@justificativa))",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@id", idSolicitacao);
                    AcessoDados.AdicionarParametro(comando, "@usuario", idUsuario);
                    AcessoDados.AdicionarTextoOpcional(
                        comando,
                        "@justificativa",
                        justificativa);
                    AcessoDados.AdicionarTextoObrigatorio(
                        comando,
                        "@situacao",
                        situacao);
                });
        }

        private static void ValidarAdministrador(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idUsuario)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT u.id_usuario FROM usuario u " +
                "JOIN perfilsistema p ON p.id_perfil_sistema=u.id_perfil_sistema " +
                "WHERE u.id_usuario=@id AND u.situacao_conta='aprovado' " +
                "AND p.nome='Administrador' FOR UPDATE",
                conexao,
                transacao))
            {
                AcessoDados.AdicionarParametro(comando, "@id", idUsuario);

                if (comando.ExecuteScalar() == null)
                {
                    throw new UnauthorizedAccessException(
                        "A decisão exige uma conta administrativa aprovada.");
                }
            }
        }

        private static int CompararItensPorProduto(
            SolicitacaoEstoqueCentralItem itemEsquerdo,
            SolicitacaoEstoqueCentralItem itemDireito)
        {
            return itemEsquerdo.IdProduto.CompareTo(itemDireito.IdProduto);
        }

        private static void ValidarItensParaGravacao(
            IList<SolicitacaoEstoqueCentralItem> itens)
        {
            HashSet<int> produtosInformados = new HashSet<int>();

            foreach (SolicitacaoEstoqueCentralItem item in itens)
            {
                if (item == null || item.IdProduto <= 0 || item.Quantidade <= 0 ||
                    item.Quantidade != decimal.Truncate(item.Quantidade))
                {
                    throw new ArgumentException(
                        "A solicitação contém um produto ou uma quantidade inválida.",
                        nameof(itens));
                }

                if (!produtosInformados.Add(item.IdProduto))
                {
                    throw new ArgumentException(
                        "Cada produto deve aparecer uma única vez na solicitação.",
                        nameof(itens));
                }
            }
        }

        private static SolicitacaoEstoqueCentral ConverterLinha(
            MySqlDataReader leitor)
        {
            return new SolicitacaoEstoqueCentral
            {
                IdSolicitacaoEstoqueCentral =
                    leitor.GetInt32("id_solicitacao_estoque_central"),
                Tipo = leitor.GetString("tipo"),
                IdInstituicao = leitor.GetInt32("id_instituicao"),
                IdEvento = leitor.GetInt32("id_evento"),
                IdLocalOrigem = leitor.GetInt32("id_local_origem"),
                IdLocalDestino = leitor.GetInt32("id_local_destino"),
                IdUsuarioSolicitante = leitor.GetInt32("id_usuario_solicitante"),
                DataHora = leitor.GetDateTime("data_hora"),
                Situacao = leitor.GetString("situacao"),
                IdUsuarioDecisor =
                    AcessoDados.LerInteiroOpcional(leitor, "id_usuario_decisor"),
                DecididaEm =
                    AcessoDados.LerDataHoraOpcional(leitor, "decidida_em"),
                JustificativaRejeicao =
                    AcessoDados.LerTextoOpcional(leitor, "justificativa_rejeicao"),
                Versao = leitor.GetUInt32("versao")
            };
        }

        private static SolicitacaoEstoqueCentralItem ConverterItem(
            MySqlDataReader leitor)
        {
            return new SolicitacaoEstoqueCentralItem
            {
                IdSolicitacaoEstoqueCentralItem =
                    leitor.GetInt32("id_solicitacao_estoque_central_item"),
                IdSolicitacaoEstoqueCentral =
                    leitor.GetInt32("id_solicitacao_estoque_central"),
                IdProduto = leitor.GetInt32("id_produto"),
                Quantidade = leitor.GetDecimal("quantidade")
            };
        }
    }
}
