using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Services;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class TransferenciaEstoqueRepository
    {

        private const string Colunas = "id_transferencia,id_local_origem,id_local_destino,id_solicitacao,id_solicitacao_estoque_central,id_usuario_responsavel,data_hora,observacao,chave_operacao,criado_em,id_usuario_alteracao,atualizado_em,versao";

        public List<TransferenciaEstoque> ListarPorLocalOrigem(int idLocal)
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas + " FROM transferenciaestoque WHERE id_local_origem=@id ORDER BY data_hora DESC,id_transferencia DESC",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idLocal)
            );
        }

        public TransferenciaEstoque BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT " + Colunas + " FROM transferenciaestoque WHERE id_transferencia=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public int AtenderSolicitacaoEstoqueCentral(
            int idSolicitacao,
            int idUsuarioResponsavel)
        {
            using (MySqlConnection conexao = CASE.Data.Conexao.CriarConexao())
            {
                conexao.Open();

                using (MySqlTransaction transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        ValidarAdministrador(conexao, transacao, idUsuarioResponsavel);

                        int idInstituicao;
                        int idEvento;
                        int idLocalOrigem;
                        int idLocalDestino;
                        string tipo;
                        uint versaoSolicitacao;
                        string situacao;

                        using (MySqlCommand comando = new MySqlCommand(
                            "SELECT tipo,id_instituicao,id_evento,id_local_origem,id_local_destino," +
                            "situacao,versao FROM solicitacaoestoquecentral " +
                            "WHERE id_solicitacao_estoque_central=@id FOR UPDATE",
                            conexao,
                            transacao))
                        {
                            AcessoDados.AdicionarParametro(comando, "@id", idSolicitacao);

                            using (MySqlDataReader leitor = comando.ExecuteReader())
                            {
                                if (!leitor.Read())
                                {
                                    throw new InvalidOperationException(
                                        "A solicitação do estoque central não foi encontrada.");
                                }

                                tipo = leitor.GetString("tipo");
                                idInstituicao = leitor.GetInt32("id_instituicao");
                                idEvento = leitor.GetInt32("id_evento");
                                idLocalOrigem = leitor.GetInt32("id_local_origem");
                                idLocalDestino = leitor.GetInt32("id_local_destino");
                                situacao = leitor.GetString("situacao");
                                versaoSolicitacao = leitor.GetUInt32("versao");
                            }
                        }

                        if (string.Equals(
                            situacao,
                            SituacoesSolicitacao.Atendida,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            int idTransferenciaExistente = BuscarTransferenciaVinculada(
                                conexao,
                                transacao,
                                idSolicitacao);
                            transacao.Commit();
                            return idTransferenciaExistente;
                        }

                        if (!string.Equals(
                            situacao,
                            SituacoesSolicitacao.Aprovada,
                            StringComparison.OrdinalIgnoreCase))
                        {
                            throw new InvalidOperationException(
                                "Somente uma solicitação aprovada pode ser atendida.");
                        }

                        ValidarLocaisDaSolicitacao(
                            conexao,
                            transacao,
                            idInstituicao,
                            idEvento,
                            idLocalOrigem,
                            idLocalDestino,
                            tipo);

                        TransferenciaEstoque transferencia =
                            new TransferenciaEstoque();
                        transferencia.IdLocalOrigem = idLocalOrigem;
                        transferencia.IdLocalDestino = idLocalDestino;
                        transferencia.IdUsuarioResponsavel = idUsuarioResponsavel;
                        transferencia.DataHora = DateTime.Now;
                        transferencia.CriadoEm = DateTime.Now;
                        transferencia.ChaveOperacao = Guid.NewGuid().ToString();
                        if (tipo == TiposSolicitacaoEstoqueCentral.Liberacao)
                        {
                            transferencia.Observacao =
                                "Atendimento integral da solicitacao de liberacao do estoque central.";
                        }
                        else
                        {
                            transferencia.Observacao =
                                "Atendimento integral da solicitacao de devolucao ao estoque central.";
                        }

                        BloquearEventoDaTransferencia(
                            conexao,
                            transacao,
                            transferencia);

                        List<TransferenciaItem> itens =
                            CarregarItensSolicitacaoCentral(
                                conexao,
                                transacao,
                                idSolicitacao);

                        if (itens.Count == 0)
                        {
                            throw new InvalidOperationException(
                                "A solicitação aprovada não possui itens.");
                        }

                        int idTransferencia = AcessoDados.Inserir(
                            conexao,
                            transacao,
                            "INSERT INTO transferenciaestoque " +
                            "(id_local_origem,id_local_destino,id_solicitacao_estoque_central," +
                            "id_usuario_responsavel,data_hora,observacao,chave_operacao,criado_em) " +
                            "VALUES (@origem,@destino,@solicitacao,@usuario,@data," +
                            "@observacao,@chave,@criadoEm)",
                            comando =>
                            {
                                AcessoDados.AdicionarParametro(
                                    comando,
                                    "@origem",
                                    transferencia.IdLocalOrigem);
                                AcessoDados.AdicionarParametro(
                                    comando,
                                    "@destino",
                                    transferencia.IdLocalDestino);
                                AcessoDados.AdicionarParametro(
                                    comando,
                                    "@solicitacao",
                                    idSolicitacao);
                                AcessoDados.AdicionarParametro(
                                    comando,
                                    "@usuario",
                                    idUsuarioResponsavel);
                                AcessoDados.AdicionarParametro(
                                    comando,
                                    "@data",
                                    transferencia.DataHora);
                                AcessoDados.AdicionarTextoOpcional(
                                    comando,
                                    "@observacao",
                                    transferencia.Observacao);
                                AcessoDados.AdicionarTextoObrigatorio(
                                    comando,
                                    "@chave",
                                    transferencia.ChaveOperacao);
                                AcessoDados.AdicionarParametro(
                                    comando,
                                    "@criadoEm",
                                    transferencia.CriadoEm);
                            });

                        List<Tuple<int, int, decimal>> ajustesSaldo =
                            new List<Tuple<int, int, decimal>>();

                        foreach (TransferenciaItem item in itens)
                        {
                            AcessoDados.Executar(
                                conexao,
                                transacao,
                                "INSERT INTO transferenciaitem " +
                                "(id_transferencia,id_produto,quantidade) " +
                                "VALUES (@transferencia,@produto,@quantidade)",
                                comando =>
                                {
                                    AcessoDados.AdicionarParametro(
                                        comando,
                                        "@transferencia",
                                        idTransferencia);
                                    AcessoDados.AdicionarParametro(
                                        comando,
                                        "@produto",
                                        item.IdProduto);
                                    AcessoDados.AdicionarParametro(
                                        comando,
                                        "@quantidade",
                                        item.Quantidade);
                                });

                            ajustesSaldo.Add(Tuple.Create(
                                idLocalOrigem,
                                item.IdProduto,
                                -item.Quantidade));
                            ajustesSaldo.Add(Tuple.Create(
                                idLocalDestino,
                                item.IdProduto,
                                item.Quantidade));
                        }

                        ControleSaldoEstoque.AplicarAjustes(
                            conexao,
                            transacao,
                            ajustesSaldo);

                        AtualizarSolicitacaoCentralAtendida(
                            conexao,
                            transacao,
                            idSolicitacao,
                            versaoSolicitacao,
                            idUsuarioResponsavel,
                            idTransferencia);

                        RegistrarHistoricoAtendimentoCentral(
                            conexao,
                            transacao,
                            idTransferencia,
                            idSolicitacao,
                            idUsuarioResponsavel,
                            tipo,
                            idLocalOrigem,
                            idLocalDestino);

                        transacao.Commit();
                        return idTransferencia;
                    }
                    catch
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }
        }

        private static int BuscarTransferenciaVinculada(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idSolicitacao)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT id_transferencia FROM transferenciaestoque " +
                "WHERE id_solicitacao_estoque_central=@solicitacao",
                conexao,
                transacao))
            {
                AcessoDados.AdicionarParametro(
                    comando,
                    "@solicitacao",
                    idSolicitacao);
                object resultado = comando.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                {
                    throw new InvalidOperationException(
                        "A solicitação está marcada como atendida, mas não possui transferência vinculada.");
                }

                return Convert.ToInt32(resultado);
            }
        }

        private static void ValidarLocaisDaSolicitacao(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idInstituicao,
            int idEvento,
            int idLocalOrigem,
            int idLocalDestino,
            string tipo)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT origem.id_local_estoque FROM localestoque origem " +
                "JOIN evento ev ON ev.id_evento=@evento " +
                "JOIN localestoque destino ON destino.id_local_estoque=@destino " +
                "WHERE origem.id_local_estoque=@origem " +
                "AND origem.id_instituicao=@instituicao " +
                "AND origem.ativo=1 AND destino.ativo=1 " +
                "AND ev.id_instituicao=@instituicao " +
                "AND destino.id_instituicao=@instituicao " +
                "AND ((@tipo='liberacao' AND origem.eh_central=1 " +
                "AND origem.eh_geral_evento=0 AND origem.id_evento IS NULL " +
                "AND destino.id_evento=@evento AND destino.eh_central=0 " +
                "AND destino.eh_geral_evento=1) " +
                "OR (@tipo='devolucao' AND origem.id_evento=@evento " +
                "AND origem.eh_central=0 AND origem.eh_geral_evento=1 " +
                "AND destino.eh_central=1 AND destino.eh_geral_evento=0 " +
                "AND destino.id_evento IS NULL)) FOR UPDATE",
                conexao,
                transacao))
            {
                AcessoDados.AdicionarParametro(comando, "@evento", idEvento);
                AcessoDados.AdicionarParametro(comando, "@destino", idLocalDestino);
                AcessoDados.AdicionarParametro(comando, "@origem", idLocalOrigem);
                AcessoDados.AdicionarParametro(comando, "@instituicao", idInstituicao);
                AcessoDados.AdicionarTextoObrigatorio(comando, "@tipo", tipo);

                bool encontrouLocalValido = false;

                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    if (leitor.Read())
                    {
                        encontrouLocalValido = true;
                    }
                }

                if (!encontrouLocalValido)
                {
                    throw new InvalidOperationException(
                        "A origem precisa ser o estoque central da instituição e o destino precisa pertencer ao evento.");
                }
            }
        }

        private static List<TransferenciaItem> CarregarItensSolicitacaoCentral(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idSolicitacao)
        {
            List<TransferenciaItem> itens = new List<TransferenciaItem>();

            using (MySqlCommand comando = new MySqlCommand(
                "SELECT id_produto,quantidade " +
                "FROM solicitacaoestoquecentralitem " +
                "WHERE id_solicitacao_estoque_central=@id " +
                "ORDER BY id_produto FOR UPDATE",
                conexao,
                transacao))
            {
                AcessoDados.AdicionarParametro(comando, "@id", idSolicitacao);

                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        TransferenciaItem item = new TransferenciaItem();
                        item.IdProduto = leitor.GetInt32("id_produto");
                        item.Quantidade = leitor.GetDecimal("quantidade");
                        itens.Add(item);
                    }
                }
            }

            return itens;
        }

        private static void AtualizarSolicitacaoCentralAtendida(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idSolicitacao,
            uint versao,
            int idUsuario,
            int idTransferencia)
        {
            int linhasAfetadas = AcessoDados.Executar(
                conexao,
                transacao,
                "UPDATE solicitacaoestoquecentral " +
                "SET situacao=@atendida,versao=versao+1 " +
                "WHERE id_solicitacao_estoque_central=@id " +
                "AND situacao=@aprovada AND versao=@versao",
                comando =>
                {
                    AcessoDados.AdicionarTextoObrigatorio(
                        comando,
                        "@atendida",
                        SituacoesSolicitacao.Atendida);
                    AcessoDados.AdicionarParametro(comando, "@id", idSolicitacao);
                    AcessoDados.AdicionarTextoObrigatorio(
                        comando,
                        "@aprovada",
                        SituacoesSolicitacao.Aprovada);
                    AcessoDados.AdicionarParametro(comando, "@versao", versao);
                });

            if (linhasAfetadas != 1)
            {
                throw new InvalidOperationException(
                    "A solicitação mudou durante o atendimento. Atualize os dados.");
            }

            AcessoDados.Executar(
                conexao,
                transacao,
                "INSERT INTO historicoalteracaooperacao " +
                "(entidade,id_entidade,id_usuario,justificativa,dados_anteriores,dados_novos) " +
                "VALUES ('solicitacaoestoquecentral',@id,@usuario," +
                "'Atendimento integral da solicitação'," +
                "JSON_OBJECT('situacao','aprovada')," +
                "JSON_OBJECT('situacao','atendida','id_transferencia',@transferencia))",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@id", idSolicitacao);
                    AcessoDados.AdicionarParametro(comando, "@usuario", idUsuario);
                    AcessoDados.AdicionarParametro(
                        comando,
                        "@transferencia",
                        idTransferencia);
                });
        }

        public int CadastrarComItens(TransferenciaEstoque transferencia, IList<TransferenciaItem> itens)
        {
            if (transferencia == null)
            {
                throw new ArgumentNullException(nameof(transferencia));
            }

            if (itens == null)
            {
                throw new ArgumentNullException(nameof(itens));
            }

            if (itens.Count == 0)
            {
                throw new ArgumentException("A transferência deve possuir pelo menos um item.", nameof(itens));
            }

            if (transferencia.IdLocalOrigem == transferencia.IdLocalDestino)
            {
                throw new ArgumentException("A origem e o destino da transferência devem ser diferentes.");
            }

            if (string.IsNullOrWhiteSpace(transferencia.ChaveOperacao))
            {
                throw new ArgumentException(
                    "Informe uma chave de operacao unica para evitar repeticao da transferencia.",
                    nameof(transferencia));
            }

            if (!transferencia.CriadoEm.HasValue)
            {
                transferencia.CriadoEm = DateTime.Now;
            }

            using (var conexao = CASE.Data.Conexao.CriarConexao())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        if (transferencia.IdSolicitacaoEstoqueCentral.HasValue)
                        {
                            throw new UnauthorizedAccessException(
                                "Pedidos administrativos devem usar o atendimento transacional proprio.");
                        }

                        ValidarTransferenciaInterna(
                            conexao,
                            transacao,
                            transferencia,
                            transferencia.IdUsuarioResponsavel);
                        BloquearEventoDaTransferencia(conexao, transacao, transferencia);

                        uint versaoSolicitacao = 0;

                        if (transferencia.IdSolicitacao.HasValue)
                        {
                            versaoSolicitacao = ValidarSolicitacaoIntegral(
                                conexao,
                                transacao,
                                transferencia,
                                itens,
                                SituacoesSolicitacao.Aprovada);
                        }

                        int id = AcessoDados.Inserir(
                            conexao,
                            transacao,
                            "INSERT INTO transferenciaestoque (id_local_origem,id_local_destino,id_solicitacao,id_usuario_responsavel,data_hora,observacao,chave_operacao,criado_em) VALUES (@origem,@destino,@solicitacao,@usuario,@data,@observacao,@chave,@criadoEm)",
                            comando =>
                            {
                                AcessoDados.AdicionarParametro(comando, "@origem", transferencia.IdLocalOrigem);
                                AcessoDados.AdicionarParametro(comando, "@destino", transferencia.IdLocalDestino);
                                AcessoDados.AdicionarParametro(comando, "@solicitacao", transferencia.IdSolicitacao);
                                AcessoDados.AdicionarParametro(comando, "@usuario", transferencia.IdUsuarioResponsavel);
                                AcessoDados.AdicionarParametro(comando, "@data", transferencia.DataHora);
                                AcessoDados.AdicionarTextoOpcional(comando, "@observacao", transferencia.Observacao);
                                AcessoDados.AdicionarTextoObrigatorio(comando, "@chave", transferencia.ChaveOperacao);
                                AcessoDados.AdicionarParametro(comando, "@criadoEm", transferencia.CriadoEm);
                            });

                        List<Tuple<int, int, decimal>> ajustesSaldo =
                            new List<Tuple<int, int, decimal>>();

                        foreach (TransferenciaItem item in itens)
                        {
                            if (item == null)
                            {
                                throw new ArgumentException("A lista contem um item nulo.", nameof(itens));
                            }

                            ValidarQuantidade(item.Quantidade);

                            AcessoDados.Executar(
                                conexao,
                                transacao,
                                "INSERT INTO transferenciaitem (id_transferencia,id_produto,quantidade) VALUES (@transferencia,@produto,@quantidade)",
                                comando =>
                                {
                                    AcessoDados.AdicionarParametro(comando, "@transferencia", id);
                                    AcessoDados.AdicionarParametro(comando, "@produto", item.IdProduto);
                                    AcessoDados.AdicionarParametro(comando, "@quantidade", item.Quantidade);
                                });

                            ajustesSaldo.Add(Tuple.Create(
                                transferencia.IdLocalOrigem,
                                item.IdProduto,
                                -item.Quantidade));
                            ajustesSaldo.Add(Tuple.Create(
                                transferencia.IdLocalDestino,
                                item.IdProduto,
                                item.Quantidade));

                        }

                        ControleSaldoEstoque.AplicarAjustes(
                            conexao,
                            transacao,
                            ajustesSaldo);

                        if (transferencia.IdSolicitacao.HasValue)
                        {
                            MarcarSolicitacaoAtendida(
                                conexao,
                                transacao,
                                transferencia.IdSolicitacao.Value,
                                versaoSolicitacao,
                                transferencia.IdUsuarioResponsavel,
                                id);
                        }

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
            TransferenciaEstoque transferencia,
            IList<TransferenciaItem> itens,
            int idUsuarioAlteracao,
            string justificativa)
        {
            throw new InvalidOperationException(
                "Movimentações confirmadas são imutáveis. A correção exige uma nova operação auditada.");
        }

        private static TransferenciaEstoque BuscarParaAtualizacao(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idTransferencia)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT " + Colunas + " FROM transferenciaestoque " +
                "WHERE id_transferencia=@id FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idTransferencia);

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
            int idTransferencia)
        {
            int idOrigem;
            int idDestino;

            using (MySqlCommand comando = new MySqlCommand(
                "SELECT id_local_origem,id_local_destino FROM transferenciaestoque " +
                "WHERE id_transferencia=@id",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idTransferencia);

                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    if (!leitor.Read())
                    {
                        throw new InvalidOperationException("A transferência não foi encontrada.");
                    }

                    idOrigem = leitor.GetInt32("id_local_origem");
                    idDestino = leitor.GetInt32("id_local_destino");
                }
            }

            List<Tuple<int, int, decimal>> ajustes = new List<Tuple<int, int, decimal>>();

            using (MySqlCommand comando = new MySqlCommand(
                "SELECT id_produto,quantidade FROM transferenciaitem " +
                "WHERE id_transferencia=@id FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idTransferencia);

                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        int idProduto = leitor.GetInt32("id_produto");
                        decimal quantidade = leitor.GetDecimal("quantidade");
                        ajustes.Add(Tuple.Create(idOrigem, idProduto, quantidade));
                        ajustes.Add(Tuple.Create(idDestino, idProduto, -quantidade));
                    }
                }
            }

            return ajustes;
        }

        private static string CriarSnapshot(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idTransferencia)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT CAST(JSON_OBJECT('id_local_origem',te.id_local_origem," +
                "'id_local_destino',te.id_local_destino,'id_solicitacao',te.id_solicitacao," +
                "'id_usuario_responsavel',te.id_usuario_responsavel,'data_hora',te.data_hora," +
                "'observacao',te.observacao,'itens',(SELECT JSON_ARRAYAGG(JSON_OBJECT(" +
                "'id_produto',ti.id_produto,'quantidade',ti.quantidade)) " +
                "FROM transferenciaitem ti WHERE ti.id_transferencia=te.id_transferencia)) AS CHAR) " +
                "FROM transferenciaestoque te WHERE te.id_transferencia=@id",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idTransferencia);
                return Convert.ToString(comando.ExecuteScalar());
            }
        }

        private static void RegistrarHistorico(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idTransferencia,
            int idUsuario,
            string justificativa,
            string dadosAnteriores,
            string dadosNovos)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "INSERT INTO historicoalteracaooperacao " +
                "(entidade,id_entidade,id_usuario,justificativa,dados_anteriores,dados_novos) " +
                "VALUES ('transferenciaestoque',@id,@usuario,@justificativa,CAST(@anterior AS JSON),CAST(@novo AS JSON))",
                conexao,
                transacao))
            {
                AcessoDados.AdicionarParametro(comando, "@id", idTransferencia);
                AcessoDados.AdicionarParametro(comando, "@usuario", idUsuario);
                AcessoDados.AdicionarTextoOpcional(comando, "@justificativa", justificativa);
                comando.Parameters.AddWithValue("@anterior", dadosAnteriores);
                comando.Parameters.AddWithValue("@novo", dadosNovos);
                comando.ExecuteNonQuery();
            }
        }

        private static uint ValidarSolicitacaoIntegral(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            TransferenciaEstoque transferencia,
            IList<TransferenciaItem> itens,
            string situacaoEsperada)
        {
            uint versao;

            using (MySqlCommand comando = new MySqlCommand(
                "SELECT situacao,versao FROM solicitacao WHERE id_solicitacao=@id FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", transferencia.IdSolicitacao.Value);

                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    if (!leitor.Read())
                    {
                        throw new InvalidOperationException("A solicitação vinculada não foi encontrada.");
                    }

                    if (!string.Equals(
                        leitor.GetString("situacao"),
                        situacaoEsperada,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        throw new InvalidOperationException(
                            "Somente solicitações aprovadas podem ser atendidas.");
                    }

                    versao = leitor.GetUInt32("versao");
                }
            }

            int idLocalDoSetor;

            using (MySqlCommand comando = new MySqlCommand(
                "SELECT es.id_local_estoque " +
                "FROM solicitacao so " +
                "JOIN escala e ON e.id_escala=so.id_escala " +
                "JOIN jornadasetor j ON j.id_jornada=e.id_jornada " +
                "JOIN estoquesetor es ON es.id_setor=j.id_setor " +
                "JOIN localestoque destino ON destino.id_local_estoque=es.id_local_estoque " +
                "JOIN localestoque origem ON origem.id_local_estoque=@origem " +
                "WHERE so.id_solicitacao=@id FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", transferencia.IdSolicitacao.Value);
                comando.Parameters.AddWithValue("@origem", transferencia.IdLocalOrigem);
                object resultado = comando.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                {
                    throw new InvalidOperationException(
                        "O setor da solicitação não possui um local de estoque associado.");
                }

                idLocalDoSetor = Convert.ToInt32(resultado);
            }

            if (transferencia.IdLocalDestino != idLocalDoSetor)
            {
                throw new InvalidOperationException(
                    "A transferência deve ser destinada ao local do setor solicitado.");
            }

            using (MySqlCommand comando = new MySqlCommand(
                "SELECT origem.id_local_estoque " +
                "FROM localestoque origem " +
                "JOIN localestoque destino ON destino.id_local_estoque=@destino " +
                "WHERE origem.id_local_estoque=@origem " +
                "AND origem.eh_central=0 AND origem.eh_geral_evento=1 " +
                "AND origem.id_evento=destino.id_evento " +
                "AND destino.eh_central=0 AND destino.eh_geral_evento=0 " +
                "AND origem.ativo=1 AND destino.ativo=1 FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@origem", transferencia.IdLocalOrigem);
                comando.Parameters.AddWithValue("@destino", transferencia.IdLocalDestino);

                if (comando.ExecuteScalar() == null)
                {
                    throw new InvalidOperationException(
                        "O atendimento de uma solicitação de membro deve sair do estoque geral para a barraca solicitante.");
                }
            }

            Dictionary<int, decimal> quantidadesSolicitadas = new Dictionary<int, decimal>();

            using (MySqlCommand comando = new MySqlCommand(
                "SELECT ps.id_produto,SUM(si.quantidade) AS quantidade " +
                "FROM solicitacaoitem si " +
                "JOIN produtosetor ps ON ps.id_produto_setor=si.id_produto_setor " +
                "WHERE si.id_solicitacao=@id GROUP BY ps.id_produto",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", transferencia.IdSolicitacao.Value);

                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    while (leitor.Read())
                    {
                        quantidadesSolicitadas.Add(
                            leitor.GetInt32("id_produto"),
                            leitor.GetDecimal("quantidade"));
                    }
                }
            }

            Dictionary<int, decimal> quantidadesTransferidas = new Dictionary<int, decimal>();

            foreach (TransferenciaItem item in itens)
            {
                decimal quantidadeAtual;

                if (quantidadesTransferidas.TryGetValue(item.IdProduto, out quantidadeAtual))
                {
                    quantidadesTransferidas[item.IdProduto] = quantidadeAtual + item.Quantidade;
                }
                else
                {
                    quantidadesTransferidas.Add(item.IdProduto, item.Quantidade);
                }
            }

            if (quantidadesSolicitadas.Count != quantidadesTransferidas.Count)
            {
                throw new InvalidOperationException(
                    "Os produtos transferidos não correspondem integralmente à solicitação.");
            }

            foreach (KeyValuePair<int, decimal> quantidadeSolicitada in quantidadesSolicitadas)
            {
                decimal quantidadeTransferida;

                if (!quantidadesTransferidas.TryGetValue(
                    quantidadeSolicitada.Key,
                    out quantidadeTransferida) ||
                    quantidadeTransferida != quantidadeSolicitada.Value)
                {
                    throw new InvalidOperationException(
                        "As quantidades transferidas devem ser iguais às quantidades solicitadas.");
                }
            }

            return versao;
        }

        private static void BloquearEventoDaTransferencia(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            TransferenciaEstoque transferencia)
        {
            int idLocalEvento;

            using (MySqlCommand comando = new MySqlCommand(
                "SELECT id_evento FROM localestoque " +
                "WHERE id_local_estoque IN (@origem,@destino) AND eh_central=0 " +
                "ORDER BY id_local_estoque LIMIT 1 FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@origem", transferencia.IdLocalOrigem);
                comando.Parameters.AddWithValue("@destino", transferencia.IdLocalDestino);
                object resultado = comando.ExecuteScalar();

                if (resultado == null || resultado == DBNull.Value)
                {
                    throw new InvalidOperationException(
                        "A transferência precisa ter um local de evento associado.");
                }

                idLocalEvento = Convert.ToInt32(resultado);
            }

            using (MySqlCommand comando = new MySqlCommand(
                "SELECT situacao FROM evento WHERE id_evento=@id FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idLocalEvento);
                object situacao = comando.ExecuteScalar();

                if (situacao == null || situacao == DBNull.Value)
                {
                    throw new InvalidOperationException("O evento da transferência não foi encontrado.");
                }

                string valorSituacao = Convert.ToString(situacao);
                bool podeMovimentar = string.Equals(
                    valorSituacao,
                    SituacoesEvento.Planejado,
                    StringComparison.OrdinalIgnoreCase);

                if (!podeMovimentar)
                {
                    podeMovimentar = string.Equals(
                        valorSituacao,
                        SituacoesEvento.EmAndamento,
                        StringComparison.OrdinalIgnoreCase);
                }

                if (!podeMovimentar)
                {
                    throw new InvalidOperationException(
                        "Não é permitido transferir produtos para ou de um evento encerrado.");
                }
            }
        }

        private static void ValidarTransferenciaInterna(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            TransferenciaEstoque transferencia,
            int idUsuario)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT ev.id_evento FROM localestoque origem " +
                "JOIN localestoque destino " +
                "ON destino.id_local_estoque=@destino " +
                "JOIN evento ev ON ev.id_evento=origem.id_evento " +
                "JOIN usuario u ON u.id_usuario=@usuario " +
                "JOIN pessoa p ON p.id_pessoa=u.id_pessoa " +
                "JOIN perfilsistema ps ON ps.id_perfil_sistema=u.id_perfil_sistema " +
                "WHERE origem.id_local_estoque=@origem " +
                "AND origem.eh_central=0 AND destino.eh_central=0 " +
                "AND origem.id_evento IS NOT NULL " +
                "AND origem.id_evento=destino.id_evento " +
                "AND origem.id_instituicao=destino.id_instituicao " +
                "AND origem.ativo=1 AND destino.ativo=1 " +
                "AND (origem.eh_geral_evento=1 OR EXISTS (" +
                "SELECT 1 FROM estoquesetor es " +
                "WHERE es.id_local_estoque=origem.id_local_estoque)) " +
                "AND (destino.eh_geral_evento=1 OR EXISTS (" +
                "SELECT 1 FROM estoquesetor es " +
                "WHERE es.id_local_estoque=destino.id_local_estoque)) " +
                "AND ev.id_responsavel=p.id_pessoa " +
                "AND u.situacao_conta='aprovado' AND ps.nome='Gerente' " +
                "AND ev.situacao IN ('planejado','em_andamento') FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@origem", transferencia.IdLocalOrigem);
                comando.Parameters.AddWithValue("@destino", transferencia.IdLocalDestino);
                comando.Parameters.AddWithValue("@usuario", idUsuario);

                if (comando.ExecuteScalar() == null)
                {
                    throw new InvalidOperationException(
                        "A transferência precisa ligar dois locais ativos do mesmo evento em operação.");
                }
            }
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
                        "A liberação ou confirmação exige uma conta administrativa aprovada.");
                }
            }
        }

        private static void RegistrarHistoricoAtendimentoCentral(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idTransferencia,
            int idSolicitacao,
            int idUsuario,
            string tipo,
            int idLocalOrigem,
            int idLocalDestino)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "INSERT INTO historicoalteracaooperacao " +
                "(entidade,id_entidade,id_usuario,justificativa,dados_anteriores,dados_novos) " +
                "VALUES ('transferenciaestoque',@id,@usuario,@justificativa," +
                "JSON_OBJECT(),JSON_OBJECT('id_solicitacao_estoque_central',@solicitacao," +
                "'tipo_solicitacao',@tipo,'id_local_origem',@origem," +
                "'id_local_destino',@destino))",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idTransferencia);
                comando.Parameters.AddWithValue("@usuario", idUsuario);
                AcessoDados.AdicionarTextoObrigatorio(
                    comando,
                    "@justificativa",
                    "Atendimento integral do pedido administrativo.");
                comando.Parameters.AddWithValue("@solicitacao", idSolicitacao);
                AcessoDados.AdicionarTextoObrigatorio(comando, "@tipo", tipo);
                comando.Parameters.AddWithValue("@origem", idLocalOrigem);
                comando.Parameters.AddWithValue("@destino", idLocalDestino);
                comando.ExecuteNonQuery();
            }
        }

        private static void MarcarSolicitacaoAtendida(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idSolicitacao,
            uint versao,
            int idUsuario,
            int idTransferencia)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "UPDATE solicitacao SET situacao=@atendida,versao=versao+1 " +
                "WHERE id_solicitacao=@id AND situacao=@aprovada AND versao=@versao",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@atendida", SituacoesSolicitacao.Atendida);
                comando.Parameters.AddWithValue("@aprovada", SituacoesSolicitacao.Aprovada);
                comando.Parameters.AddWithValue("@id", idSolicitacao);
                comando.Parameters.AddWithValue("@versao", versao);

                if (comando.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException(
                        "A solicitação mudou durante a transferência. Atualize os dados e tente novamente.");
                }
            }

            using (MySqlCommand comando = new MySqlCommand(
                "INSERT INTO historicoalteracaooperacao " +
                "(entidade,id_entidade,id_usuario,justificativa,dados_anteriores,dados_novos) " +
                "VALUES ('solicitacao',@id,@usuario,'Atendimento integral da solicitação'," +
                "JSON_OBJECT('situacao','aprovada'),JSON_OBJECT('situacao','atendida','id_transferencia',@transferencia))",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idSolicitacao);
                comando.Parameters.AddWithValue("@usuario", idUsuario);
                comando.Parameters.AddWithValue("@transferencia", idTransferencia);
                comando.ExecuteNonQuery();
            }
        }

        private static void ValidarQuantidade(decimal quantidade)
        {
            if (quantidade <= 0 || quantidade != decimal.Truncate(quantidade))
            {
                throw new ArgumentException(
                    "A quantidade transferida deve ser um número inteiro maior que zero.");
            }
        }

        private static TransferenciaEstoque ConverterLinha(MySqlDataReader leitor)
        {
            return new TransferenciaEstoque
            {
                IdTransferencia = leitor.GetInt32("id_transferencia"),
                IdLocalOrigem = leitor.GetInt32("id_local_origem"),
                IdLocalDestino = leitor.GetInt32("id_local_destino"),
                IdSolicitacao = AcessoDados.LerInteiroOpcional(leitor, "id_solicitacao"),
                IdSolicitacaoEstoqueCentral = AcessoDados.LerInteiroOpcional(
                    leitor,
                    "id_solicitacao_estoque_central"),
                IdUsuarioResponsavel = leitor.GetInt32("id_usuario_responsavel"),
                DataHora = leitor.GetDateTime("data_hora"),
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
