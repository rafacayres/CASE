using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class EscalaRepository
    {

        private const string Colunas = "id_escala,id_equipe,id_jornada,entrada_prevista,saida_prevista,situacao";

        public List<Escala> ListarPorJornada(int idJornada)
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas + " FROM escala WHERE id_jornada=@id ORDER BY entrada_prevista,id_escala",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idJornada)
            );
        }

        public List<Escala> ListarPorEquipe(int idEquipe)
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas + " FROM escala WHERE id_equipe=@id ORDER BY id_jornada,entrada_prevista",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idEquipe)
            );
        }

        public List<Escala> ListarPorEvento(int idEvento)
        {
            const string sql = @"SELECT e.id_escala,e.id_equipe,e.id_jornada,
                    e.entrada_prevista,e.saida_prevista,e.situacao
                FROM escala e
                INNER JOIN equipeevento ee ON ee.id_equipe=e.id_equipe
                INNER JOIN jornadasetor js ON js.id_jornada=e.id_jornada
                INNER JOIN setor s ON s.id_setor=js.id_setor
                WHERE ee.id_evento=@evento AND s.id_evento=@evento
                ORDER BY js.`data`,e.entrada_prevista,e.id_escala";

            return AcessoDados.ConsultarLista(
                sql,
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@evento", idEvento));
        }

        public Escala BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT " + Colunas + " FROM escala WHERE id_escala=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public int Cadastrar(Escala item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO escala (id_equipe,id_jornada,entrada_prevista,saida_prevista,situacao) VALUES (@equipe,@jornada,@entrada,@saida,@situacao)",
                comando => Vincular(comando, item)
            );
        }

        public bool Atualizar(Escala item)
        {
            return AcessoDados.Executar(
                "UPDATE escala SET id_equipe=@equipe,id_jornada=@jornada,entrada_prevista=@entrada,saida_prevista=@saida,situacao=@situacao WHERE id_escala=@id",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@id", item.IdEscala);
                    Vincular(comando, item);
                }) > 0;

        }

        public int SalvarValidandoConflito(
            Escala item,
            int idPessoa,
            int? idEscalaExistente)
        {
            using (var conexao = CASE.Data.Conexao.CriarConexao())
            {
                conexao.Open();

                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        int idPessoaAnterior = idPessoa;

                        if (idEscalaExistente.HasValue)
                        {
                            idPessoaAnterior = BuscarPessoaDaEscala(
                                conexao,
                                transacao,
                                idEscalaExistente.Value);
                        }

                        int primeiroId = idPessoaAnterior;
                        int segundoId = idPessoa;

                        if (primeiroId > segundoId)
                        {
                            primeiroId = idPessoa;
                            segundoId = idPessoaAnterior;
                        }

                        BloquearPessoa(conexao, transacao, primeiroId);

                        if (segundoId != primeiroId)
                        {
                            BloquearPessoa(conexao, transacao, segundoId);
                        }

                        if (ExisteConflito(conexao, transacao, item, idPessoa, idEscalaExistente))
                        {
                            throw new System.InvalidOperationException(
                                "O membro já possui uma escala sobreposta neste dia.");
                        }

                        int idSalvo;

                        if (idEscalaExistente.HasValue)
                        {
                            int linhasAfetadas = AcessoDados.Executar(
                                conexao,
                                transacao,
                                "UPDATE escala SET id_equipe=@equipe,id_jornada=@jornada,entrada_prevista=@entrada,saida_prevista=@saida,situacao=@situacao WHERE id_escala=@id",
                                comando =>
                                {
                                    AcessoDados.AdicionarParametro(comando, "@id", idEscalaExistente.Value);
                                    Vincular(comando, item);
                                });

                            if (linhasAfetadas == 0)
                            {
                                throw new System.InvalidOperationException(
                                    "A escala não foi atualizada. Atualize a lista e tente novamente.");
                            }

                            idSalvo = idEscalaExistente.Value;
                        }
                        else
                        {
                            idSalvo = AcessoDados.Inserir(
                                conexao,
                                transacao,
                                "INSERT INTO escala (id_equipe,id_jornada,entrada_prevista,saida_prevista,situacao) VALUES (@equipe,@jornada,@entrada,@saida,@situacao)",
                                comando => Vincular(comando, item));
                        }

                        transacao.Commit();
                        return idSalvo;
                    }
                    catch
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }
        }

        private static int BuscarPessoaDaEscala(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idEscala)
        {
            using (var comando = new MySqlCommand(
                "SELECT ee.id_pessoa FROM escala e INNER JOIN equipeevento ee ON ee.id_equipe=e.id_equipe WHERE e.id_escala=@id FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idEscala);
                object resultado = comando.ExecuteScalar();

                if (resultado == null || resultado == System.DBNull.Value)
                {
                    throw new System.InvalidOperationException("A escala não foi encontrada.");
                }

                return System.Convert.ToInt32(resultado);
            }
        }

        private static void BloquearPessoa(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idPessoa)
        {
            using (var comando = new MySqlCommand(
                "SELECT id_pessoa FROM pessoa WHERE id_pessoa=@id FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@id", idPessoa);
                object resultado = comando.ExecuteScalar();

                if (resultado == null || resultado == System.DBNull.Value)
                {
                    throw new System.InvalidOperationException("O membro da escala não foi encontrado.");
                }
            }
        }

        private static bool ExisteConflito(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            Escala item,
            int idPessoa,
            int? idEscalaExistente)
        {
            const string sql = @"SELECT COUNT(*)
                FROM escala e
                INNER JOIN equipeevento ee ON ee.id_equipe=e.id_equipe
                INNER JOIN jornadasetor js ON js.id_jornada=e.id_jornada
                WHERE ee.id_pessoa=@pessoa
                  AND js.`data`=(SELECT `data` FROM jornadasetor WHERE id_jornada=@jornada)
                  AND LOWER(e.situacao)<>'cancelada'
                  AND e.entrada_prevista<@saida
                  AND e.saida_prevista>@entrada
                  AND (@escala IS NULL OR e.id_escala<>@escala)";

            using (var comando = new MySqlCommand(sql, conexao, transacao))
            {
                comando.Parameters.AddWithValue("@pessoa", idPessoa);
                comando.Parameters.AddWithValue("@jornada", item.IdJornada);
                comando.Parameters.AddWithValue("@entrada", item.EntradaPrevista);
                comando.Parameters.AddWithValue("@saida", item.SaidaPrevista);

                if (idEscalaExistente.HasValue)
                {
                    comando.Parameters.AddWithValue("@escala", idEscalaExistente.Value);
                }
                else
                {
                    comando.Parameters.AddWithValue("@escala", System.DBNull.Value);
                }

                return System.Convert.ToInt32(comando.ExecuteScalar()) > 0;
            }
        }

        private static void Vincular(MySqlCommand comando, Escala item)
        {
            AcessoDados.AdicionarParametro(comando, "@equipe", item.IdEquipe);
            AcessoDados.AdicionarParametro(comando, "@jornada", item.IdJornada);
            AcessoDados.AdicionarParametro(comando, "@entrada", item.EntradaPrevista);
            AcessoDados.AdicionarParametro(comando, "@saida", item.SaidaPrevista);
            AcessoDados.AdicionarTextoObrigatorio(comando, "@situacao", item.Situacao);
        }

        private static Escala ConverterLinha(MySqlDataReader leitor)
        {
            return new Escala
            {
                IdEscala = leitor.GetInt32("id_escala"),
                IdEquipe = leitor.GetInt32("id_equipe"),
                IdJornada = leitor.GetInt32("id_jornada"),
                EntradaPrevista = (System.TimeSpan)leitor.GetValue(leitor.GetOrdinal("entrada_prevista")),
                SaidaPrevista = (System.TimeSpan)leitor.GetValue(leitor.GetOrdinal("saida_prevista")),
                Situacao = leitor.GetString("situacao")
            };
        }
    }
}
