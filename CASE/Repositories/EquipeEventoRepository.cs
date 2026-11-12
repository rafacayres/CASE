using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class EquipeEventoRepository
    {

        private const string Colunas = "id_equipe,id_evento,id_pessoa,id_perfil,ativo,id_usuario_criacao,criado_em,id_usuario_alteracao,atualizado_em,versao";

        public List<EquipeEvento> ListarPorEvento(int idEvento)
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas + " FROM equipeevento WHERE id_evento=@id ORDER BY id_equipe",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idEvento)
            );
        }

        public List<EquipeEvento> ListarPorPessoa(int idPessoa)
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas + " FROM equipeevento WHERE id_pessoa=@pessoa AND ativo=1 ORDER BY id_evento,id_equipe",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@pessoa", idPessoa));
        }

        public List<Pessoa> ListarPessoasDisponiveis(int idEvento)
        {
            const string sql = @"SELECT p.id_pessoa,p.nome,p.telefone,p.email
                FROM pessoa p
                INNER JOIN usuario u ON u.id_pessoa=p.id_pessoa
                INNER JOIN perfilsistema ps ON ps.id_perfil_sistema=u.id_perfil_sistema
                WHERE LOWER(u.situacao_conta)='aprovado'
                  AND LOWER(ps.nome)='membro'
                  AND NOT EXISTS (
                      SELECT 1 FROM equipeevento ee
                      WHERE ee.id_evento=@evento AND ee.id_pessoa=p.id_pessoa AND ee.ativo=1)
                ORDER BY p.nome";

            return AcessoDados.ConsultarLista(
                sql,
                ConverterPessoa,
                comando => AcessoDados.AdicionarParametro(comando, "@evento", idEvento));
        }

        public Pessoa BuscarPessoaDisponivelPorLogin(int idEvento, string login)
        {
            const string sql = @"SELECT p.id_pessoa,p.nome,p.telefone,p.email
                FROM pessoa p
                INNER JOIN usuario u ON u.id_pessoa=p.id_pessoa
                INNER JOIN perfilsistema ps ON ps.id_perfil_sistema=u.id_perfil_sistema
                WHERE u.login=@login
                  AND LOWER(u.situacao_conta)='aprovado'
                  AND LOWER(ps.nome)='membro'
                  AND NOT EXISTS (
                      SELECT 1 FROM equipeevento ee
                      WHERE ee.id_evento=@evento AND ee.id_pessoa=p.id_pessoa AND ee.ativo=1)";

            return AcessoDados.ConsultarUnico(
                sql,
                ConverterPessoa,
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@login", login);
                    AcessoDados.AdicionarParametro(comando, "@evento", idEvento);
                });
        }

        public Pessoa BuscarPessoaPorId(int idPessoa)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT id_pessoa,nome,telefone,email FROM pessoa WHERE id_pessoa=@id",
                ConverterPessoa,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idPessoa));
        }

        public EquipeEvento BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT " + Colunas + " FROM equipeevento WHERE id_equipe=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public int Cadastrar(EquipeEvento item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO equipeevento (id_evento,id_pessoa,id_perfil,ativo,id_usuario_criacao,criado_em) VALUES (@evento,@pessoa,@perfil,@ativo,@usuarioCriacao,@criadoEm)",
                comando =>
                {
                    Vincular(comando, item);
                    AcessoDados.AdicionarParametro(comando, "@usuarioCriacao", item.IdUsuarioCriacao);
                    AcessoDados.AdicionarParametro(comando, "@criadoEm", item.CriadoEm);
                }
            );
        }

        public bool Atualizar(EquipeEvento item)
        {
            return AcessoDados.Executar(
                "UPDATE equipeevento SET id_evento=@evento,id_pessoa=@pessoa,id_perfil=@perfil,ativo=@ativo,id_usuario_alteracao=@usuarioAlteracao,atualizado_em=NOW(),versao=versao+1 WHERE id_equipe=@id AND versao=@versao",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@id", item.IdEquipe);
                    AcessoDados.AdicionarParametro(comando, "@versao", item.Versao);
                    AcessoDados.AdicionarParametro(comando, "@usuarioAlteracao", item.IdUsuarioAlteracao);
                    Vincular(comando, item);
                }) > 0;

        }

        private static void Vincular(MySqlCommand comando, EquipeEvento item)
        {
            AcessoDados.AdicionarParametro(comando, "@evento", item.IdEvento);
            AcessoDados.AdicionarParametro(comando, "@pessoa", item.IdPessoa);
            AcessoDados.AdicionarParametro(comando, "@perfil", item.IdPerfil);
            AcessoDados.AdicionarParametro(comando, "@ativo", item.Ativo);
        }

        private static EquipeEvento ConverterLinha(MySqlDataReader leitor)
        {
            return new EquipeEvento
            {
                IdEquipe = leitor.GetInt32("id_equipe"),
                IdEvento = leitor.GetInt32("id_evento"),
                IdPessoa = leitor.GetInt32("id_pessoa"),
                IdPerfil = leitor.GetInt32("id_perfil"),
                Ativo = leitor.GetBoolean("ativo"),
                IdUsuarioCriacao = AcessoDados.LerInteiroOpcional(leitor, "id_usuario_criacao"),
                CriadoEm = AcessoDados.LerDataHoraOpcional(leitor, "criado_em"),
                IdUsuarioAlteracao = AcessoDados.LerInteiroOpcional(leitor, "id_usuario_alteracao"),
                AtualizadoEm = AcessoDados.LerDataHoraOpcional(leitor, "atualizado_em"),
                Versao = leitor.GetUInt32("versao")
            };
        }

        private static Pessoa ConverterPessoa(MySqlDataReader leitor)
        {
            return new Pessoa
            {
                IdPessoa = leitor.GetInt32("id_pessoa"),
                Nome = leitor.GetString("nome"),
                Telefone = AcessoDados.LerTextoOpcional(leitor, "telefone"),
                Email = AcessoDados.LerTextoOpcional(leitor, "email")
            };
        }
    }
}
