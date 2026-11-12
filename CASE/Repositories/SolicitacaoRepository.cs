using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Services;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class SolicitacaoRepository
    {

        private const string Colunas = "id_solicitacao,id_escala,tipo,descricao,data_hora,situacao,id_usuario_solicitante,id_usuario_decisor,decidida_em,justificativa_rejeicao,criado_em,versao";

        public List<Solicitacao> ListarPorEscala(int idEscala)
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas + " FROM solicitacao WHERE id_escala=@id ORDER BY data_hora DESC,id_solicitacao DESC",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idEscala)
            );
        }

        public List<Solicitacao> ListarPorSolicitante(int idUsuario)
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas + " FROM solicitacao WHERE id_usuario_solicitante=@usuario ORDER BY data_hora DESC,id_solicitacao DESC",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@usuario", idUsuario));
        }

        public List<Solicitacao> ListarPorEvento(int idEvento)
        {
            return AcessoDados.ConsultarLista(
                "SELECT DISTINCT so.id_solicitacao,so.id_escala,so.tipo,so.descricao,so.data_hora,so.situacao," +
                "so.id_usuario_solicitante,so.id_usuario_decisor,so.decidida_em,so.justificativa_rejeicao,so.criado_em,so.versao " +
                "FROM solicitacao so " +
                "JOIN escala e ON e.id_escala=so.id_escala " +
                "JOIN equipeevento ee ON ee.id_equipe=e.id_equipe " +
                "JOIN jornadasetor j ON j.id_jornada=e.id_jornada " +
                "JOIN setor s ON s.id_setor=j.id_setor " +
                "WHERE ee.id_evento=@evento AND s.id_evento=@evento " +
                "ORDER BY so.data_hora DESC,so.id_solicitacao DESC",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@evento", idEvento));
        }

        public Solicitacao BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT " + Colunas + " FROM solicitacao WHERE id_solicitacao=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public List<Solicitacao> ListarPendentesPorEvento(int idEvento)
        {
            return AcessoDados.ConsultarLista(
                "SELECT DISTINCT so.id_solicitacao,so.id_escala,so.tipo,so.descricao,so.data_hora,so.situacao," +
                "so.id_usuario_solicitante,so.id_usuario_decisor,so.decidida_em,so.justificativa_rejeicao,so.criado_em,so.versao " +
                " FROM solicitacao so " +
                "JOIN escala e ON e.id_escala=so.id_escala " +
                "JOIN equipeevento ee ON ee.id_equipe=e.id_equipe " +
                "JOIN jornadasetor j ON j.id_jornada=e.id_jornada " +
                "JOIN setor s ON s.id_setor=j.id_setor " +
                "WHERE ee.id_evento=@evento AND s.id_evento=@evento AND so.situacao=@situacao " +
                "ORDER BY so.data_hora,so.id_solicitacao",
                ConverterLinha,
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@evento", idEvento);
                    AcessoDados.AdicionarTextoObrigatorio(
                        comando,
                        "@situacao",
                        SituacoesSolicitacao.Pendente);
                }
            );
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
                    "A decisao deve aprovar ou rejeitar a solicitacao.",
                    nameof(situacao));
            }

            if (situacao == SituacoesSolicitacao.Rejeitada &&
                string.IsNullOrWhiteSpace(justificativa))
            {
                throw new ArgumentException(
                    "A rejeicao exige uma justificativa.",
                    nameof(justificativa));
            }

            if (!string.IsNullOrWhiteSpace(justificativa) &&
                justificativa.Trim().Length > 500)
            {
                throw new ArgumentException(
                    "A justificativa pode ter no maximo 500 caracteres.",
                    nameof(justificativa));
            }

            using (var conexao = CASE.Data.Conexao.CriarConexao())
            {
                conexao.Open();

                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        ValidarGerenteResponsavel(
                            conexao,
                            transacao,
                            idSolicitacao,
                            versao,
                            idUsuarioDecisor);

                        using (var comando = new MySqlCommand(
                            "UPDATE solicitacao " +
                            "SET situacao=@situacao,id_usuario_decisor=@usuario,decidida_em=NOW()," +
                            "justificativa_rejeicao=CASE WHEN @situacao='rejeitada' " +
                            "THEN @justificativa ELSE NULL END,versao=versao+1 " +
                            "WHERE id_solicitacao=@id AND situacao=@pendente AND versao=@versao",
                            conexao,
                            transacao))
                        {
                            comando.Parameters.AddWithValue("@situacao", situacao);
                            comando.Parameters.AddWithValue("@usuario", idUsuarioDecisor);
                            comando.Parameters.AddWithValue("@id", idSolicitacao);
                            comando.Parameters.AddWithValue("@pendente", SituacoesSolicitacao.Pendente);
                            comando.Parameters.AddWithValue("@versao", versao);
                            AcessoDados.AdicionarTextoOpcional(
                                comando,
                                "@justificativa",
                                justificativa);

                            if (comando.ExecuteNonQuery() != 1)
                            {
                                throw new InvalidOperationException(
                                    "A solicitação foi alterada por outra pessoa. Atualize a lista e tente novamente.");
                            }
                        }

                        using (var comando = new MySqlCommand(
                            "INSERT INTO historicoalteracaooperacao " +
                            "(entidade,id_entidade,id_usuario,justificativa,dados_anteriores,dados_novos) " +
                            "VALUES ('solicitacao',@id,@usuario,@justificativa," +
                            "JSON_OBJECT('situacao','pendente'),JSON_OBJECT('situacao',@situacao))",
                            conexao,
                            transacao))
                        {
                            AcessoDados.AdicionarParametro(comando, "@id", idSolicitacao);
                            AcessoDados.AdicionarParametro(comando, "@usuario", idUsuarioDecisor);
                            AcessoDados.AdicionarTextoOpcional(
                                comando,
                                "@justificativa",
                                justificativa);
                            AcessoDados.AdicionarTextoObrigatorio(comando, "@situacao", situacao);
                            comando.ExecuteNonQuery();
                        }

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

        private static void ValidarGerenteResponsavel(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idSolicitacao,
            uint versao,
            int idUsuario)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT so.id_solicitacao FROM solicitacao so " +
                "JOIN escala e ON e.id_escala=so.id_escala " +
                "JOIN equipeevento ee ON ee.id_equipe=e.id_equipe " +
                "JOIN jornadasetor j ON j.id_jornada=e.id_jornada " +
                "JOIN setor s ON s.id_setor=j.id_setor " +
                "JOIN evento ev ON ev.id_evento=s.id_evento " +
                "JOIN usuario u ON u.id_usuario=@usuario " +
                "JOIN pessoa p ON p.id_pessoa=u.id_pessoa " +
                "JOIN perfilsistema ps ON ps.id_perfil_sistema=u.id_perfil_sistema " +
                "WHERE so.id_solicitacao=@id AND so.situacao='pendente' " +
                "AND so.versao=@versao AND ee.id_evento=ev.id_evento " +
                "AND ev.id_responsavel=p.id_pessoa AND ee.ativo=1 " +
                "AND u.situacao_conta='aprovado' AND ps.nome='Gerente' FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idSolicitacao);
                comando.Parameters.AddWithValue("@versao", versao);
                comando.Parameters.AddWithValue("@usuario", idUsuario);

                if (comando.ExecuteScalar() == null)
                {
                    throw new UnauthorizedAccessException(
                        "A decisão exige o gerente responsável pelo evento e uma solicitação pendente.");
                }
            }
        }

        public int CadastrarComItens(Solicitacao solicitacao, IList<SolicitacaoItem> itens)
        {
            if (solicitacao == null)
            {
                throw new ArgumentNullException(nameof(solicitacao));
            }

            if (itens == null)
            {
                throw new ArgumentNullException(nameof(itens));
            }
            using (var conexao = CASE.Data.Conexao.CriarConexao())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        if (!solicitacao.IdUsuarioSolicitante.HasValue)
                        {
                            throw new UnauthorizedAccessException(
                                "A solicitação precisa identificar o usuário solicitante.");
                        }

                        ValidarMembroCriador(
                            conexao,
                            transacao,
                            solicitacao.IdEscala,
                            solicitacao.IdUsuarioSolicitante.Value);
                        ValidarItensDoSetor(
                            conexao,
                            transacao,
                            solicitacao.IdEscala,
                            itens);

                        int id = AcessoDados.Inserir(
                            conexao,
                            transacao,
                            "INSERT INTO solicitacao (id_escala,tipo,descricao,data_hora,situacao,id_usuario_solicitante,criado_em) VALUES (@escala,@tipo,@descricao,@data,@situacao,@solicitante,@criadoEm)",
                            comando =>
                            {
                                AcessoDados.AdicionarParametro(comando, "@escala", solicitacao.IdEscala);
                                AcessoDados.AdicionarTextoObrigatorio(comando, "@tipo", solicitacao.Tipo);
                                AcessoDados.AdicionarTextoOpcional(comando, "@descricao", solicitacao.Descricao);
                                AcessoDados.AdicionarParametro(comando, "@data", solicitacao.DataHora);
                                AcessoDados.AdicionarTextoObrigatorio(comando, "@situacao", solicitacao.Situacao);
                                AcessoDados.AdicionarParametro(comando, "@solicitante", solicitacao.IdUsuarioSolicitante);
                                AcessoDados.AdicionarParametro(comando, "@criadoEm", solicitacao.CriadoEm);
                            });

                        foreach (var item in itens)
                        {
                            if (item == null)
                            {
                                throw new ArgumentException("A lista contem um item nulo.", nameof(itens));
                            }
                            AcessoDados.Executar(
                                conexao,
                                transacao,
                                "INSERT INTO solicitacaoitem (id_solicitacao,id_produto_setor,quantidade) VALUES (@solicitacao,@produtoSetor,@quantidade)",
                                comando =>
                                {
                                    AcessoDados.AdicionarParametro(comando, "@solicitacao", id);
                                    AcessoDados.AdicionarParametro(comando, "@produtoSetor", item.IdProdutoSetor);
                                    AcessoDados.AdicionarParametro(comando, "@quantidade", item.Quantidade);
                                });

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

        private static void ValidarMembroCriador(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idEscala,
            int idUsuario)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT e.id_escala FROM escala e " +
                "JOIN equipeevento ee ON ee.id_equipe=e.id_equipe " +
                "JOIN usuario u ON u.id_usuario=@usuario " +
                "JOIN pessoa p ON p.id_pessoa=u.id_pessoa " +
                "JOIN perfilsistema ps ON ps.id_perfil_sistema=u.id_perfil_sistema " +
                "JOIN jornadasetor j ON j.id_jornada=e.id_jornada " +
                "JOIN setor s ON s.id_setor=j.id_setor " +
                "JOIN evento ev ON ev.id_evento=s.id_evento " +
                "WHERE e.id_escala=@escala AND ee.id_pessoa=p.id_pessoa " +
                "AND ee.id_evento=ev.id_evento AND ee.ativo=1 " +
                "AND u.situacao_conta='aprovado' AND ps.nome='Membro' " +
                "AND ev.situacao IN ('planejado','em_andamento') FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@escala", idEscala);
                comando.Parameters.AddWithValue("@usuario", idUsuario);

                if (comando.ExecuteScalar() == null)
                {
                    throw new UnauthorizedAccessException(
                        "O solicitante não possui uma escala ativa válida neste evento.");
                }
            }
        }

        private static void ValidarItensDoSetor(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idEscala,
            IList<SolicitacaoItem> itens)
        {
            if (itens.Count == 0)
            {
                throw new ArgumentException(
                    "A solicitação precisa possuir ao menos um item.",
                    nameof(itens));
            }

            foreach (SolicitacaoItem item in itens)
            {
                if (item == null || item.IdProdutoSetor <= 0 || item.Quantidade <= 0 ||
                    item.Quantidade != decimal.Truncate(item.Quantidade))
                {
                    throw new ArgumentException(
                        "A solicitação contém um item ou quantidade inválida.",
                        nameof(itens));
                }

                using (MySqlCommand comando = new MySqlCommand(
                    "SELECT ps.id_produto_setor FROM escala e " +
                    "JOIN jornadasetor j ON j.id_jornada=e.id_jornada " +
                    "JOIN produtosetor ps ON ps.id_setor=j.id_setor " +
                    "WHERE e.id_escala=@escala AND ps.id_produto_setor=@produtoSetor FOR UPDATE",
                    conexao,
                    transacao))
                {
                    comando.Parameters.AddWithValue("@escala", idEscala);
                    comando.Parameters.AddWithValue("@produtoSetor", item.IdProdutoSetor);

                    if (comando.ExecuteScalar() == null)
                    {
                        throw new InvalidOperationException(
                            "Um produto solicitado não pertence ao setor da escala.");
                    }
                }
            }
        }

        private static Solicitacao ConverterLinha(MySqlDataReader leitor)
        {
            return new Solicitacao
            {
                IdSolicitacao = leitor.GetInt32("id_solicitacao"),
                IdEscala = leitor.GetInt32("id_escala"),
                Tipo = leitor.GetString("tipo"),
                Descricao = AcessoDados.LerTextoOpcional(leitor, "descricao"),
                DataHora = leitor.GetDateTime("data_hora"),
                Situacao = leitor.GetString("situacao"),
                IdUsuarioSolicitante = AcessoDados.LerInteiroOpcional(leitor, "id_usuario_solicitante"),
                IdUsuarioDecisor = AcessoDados.LerInteiroOpcional(leitor, "id_usuario_decisor"),
                DecididaEm = AcessoDados.LerDataHoraOpcional(leitor, "decidida_em"),
                JustificativaRejeicao = AcessoDados.LerTextoOpcional(
                    leitor,
                    "justificativa_rejeicao"),
                CriadoEm = AcessoDados.LerDataHoraOpcional(leitor, "criado_em"),
                Versao = leitor.GetUInt32("versao")
            };
        }
    }
}
