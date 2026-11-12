using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class PessoaRepository
    {
        private const string Colunas = "id_pessoa, nome, telefone, email";

        public List<Pessoa> Listar()
        {
            string sql = "SELECT " + Colunas + " FROM pessoa ORDER BY id_pessoa";
            return AcessoDados.ConsultarLista(sql, ConverterLinha);
        }

        public Pessoa BuscarPorId(int id)
        {
            string sql = "SELECT " + Colunas + " FROM pessoa WHERE id_pessoa = @id";
            return AcessoDados.ConsultarUnico(sql, ConverterLinha, comando => AcessoDados.AdicionarParametro(comando, "@id", id));
        }

        public int Cadastrar(Pessoa pessoa)
        {
            string sql = "INSERT INTO pessoa (nome, telefone, email) VALUES (@nome, @telefone, @email)";
            return AcessoDados.Inserir(sql, comando =>
            {
                AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", pessoa.Nome);
                AcessoDados.AdicionarTextoOpcional(comando, "@telefone", pessoa.Telefone);
                AcessoDados.AdicionarTextoOpcional(comando, "@email", pessoa.Email);
            });
        }

        public bool Atualizar(Pessoa pessoa)
        {
            string sql = "UPDATE pessoa SET nome = @nome, telefone = @telefone, email = @email WHERE id_pessoa = @id";
            return AcessoDados.Executar(sql, comando =>
            {
                AcessoDados.AdicionarParametro(comando, "@id", pessoa.IdPessoa);
                AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", pessoa.Nome);
                AcessoDados.AdicionarTextoOpcional(comando, "@telefone", pessoa.Telefone);
                AcessoDados.AdicionarTextoOpcional(comando, "@email", pessoa.Email);
            }) > 0;
        }

        public byte[] BuscarFotoPerfil(int idPessoa)
        {
            using (var conexao = CASE.Data.Conexao.CriarConexao())
            {
                conexao.Open();

                string sql = "SELECT foto_perfil FROM pessoa WHERE id_pessoa = @idPessoa";

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    AcessoDados.AdicionarParametro(comando, "@idPessoa", idPessoa);

                    object resultado = comando.ExecuteScalar();

                    if (resultado == null || resultado == System.DBNull.Value)
                    {
                        return null;
                    }

                    return (byte[])resultado;
                }
            }
        }

        public void AtualizarFotoPerfil(int idPessoa, byte[] fotoPerfil)
        {
            using (var conexao = CASE.Data.Conexao.CriarConexao())
            {
                conexao.Open();

                string sql = "UPDATE pessoa SET foto_perfil = @fotoPerfil " +
                    "WHERE id_pessoa = @idPessoa";

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    AcessoDados.AdicionarParametro(comando, "@idPessoa", idPessoa);

                    MySqlParameter parametroFoto = comando.Parameters.Add(
                        "@fotoPerfil",
                        MySqlDbType.MediumBlob);

                    if (fotoPerfil == null)
                    {
                        parametroFoto.Value = System.DBNull.Value;
                    }
                    else
                    {
                        parametroFoto.Value = fotoPerfil;
                    }

                    comando.ExecuteNonQuery();
                }
            }
        }

        private static Pessoa ConverterLinha(MySqlDataReader leitor)
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
