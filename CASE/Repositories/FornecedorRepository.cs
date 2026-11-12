
using System;
using System.Collections.Generic;
using CASE.Data;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class FornecedorRepository
    {
        public List<Fornecedor> Listar()
        {
            using (var conexao = Conexao.CriarConexao())
            {
                conexao.Open();

                string sql = "SELECT id_fornecedor, nome, documento, telefone, email FROM fornecedor";

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    using (var leitor = comando.ExecuteReader())
                    {
                        List<Fornecedor> fornecedores = new List<Fornecedor>();

                        while (leitor.Read())
                        {
                            Fornecedor fornecedor = new Fornecedor();

                            fornecedor.IdFornecedor = Convert.ToInt32(leitor["id_fornecedor"]);
                            fornecedor.Nome = leitor["nome"].ToString();
                            fornecedor.Documento = AcessoDados.LerTextoOpcional(leitor, "documento");
                            fornecedor.Telefone = AcessoDados.LerTextoOpcional(leitor, "telefone");
                            fornecedor.Email = AcessoDados.LerTextoOpcional(leitor, "email");

                            fornecedores.Add(fornecedor);
                        }

                        return fornecedores;
                    }
                }
            }
        }

        public void Cadastrar(Fornecedor fornecedor)
        {
            using (var conexao = Conexao.CriarConexao())
            {
                conexao.Open();

                string sql = "INSERT INTO fornecedor (nome, documento, telefone, email) VALUES (@nome, @documento, @telefone, @email)";

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", fornecedor.Nome);
                    AcessoDados.AdicionarTextoOpcional(comando, "@documento", fornecedor.Documento);
                    AcessoDados.AdicionarTextoOpcional(comando, "@telefone", fornecedor.Telefone);
                    AcessoDados.AdicionarTextoOpcional(comando, "@email", fornecedor.Email);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public Fornecedor BuscarPorId(int id)
        {
            using (var conexao = Conexao.CriarConexao())
            {
                conexao.Open();

                string sql = "SELECT id_fornecedor, nome, documento, telefone, email FROM fornecedor WHERE id_fornecedor = @id";

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    using (var leitor = comando.ExecuteReader())
                    {
                        if (leitor.Read())
                        {
                            Fornecedor fornecedor = new Fornecedor();

                            fornecedor.IdFornecedor = Convert.ToInt32(leitor["id_fornecedor"]);
                            fornecedor.Nome = leitor["nome"].ToString();
                            fornecedor.Documento = AcessoDados.LerTextoOpcional(leitor, "documento");
                            fornecedor.Telefone = AcessoDados.LerTextoOpcional(leitor, "telefone");
                            fornecedor.Email = AcessoDados.LerTextoOpcional(leitor, "email");

                            return fornecedor;
                        }

                        return null;
                    }
                }
            }
        }

        public void Atualizar(Fornecedor fornecedor)
        {
            using (var conexao = Conexao.CriarConexao())
            {
                conexao.Open();

                string sql = @"UPDATE fornecedor
                       SET nome = @nome,
                           documento = @documento,
                           telefone = @telefone,
                           email = @email
                       WHERE id_fornecedor = @id";

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@id", fornecedor.IdFornecedor);
                    AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", fornecedor.Nome);
                    AcessoDados.AdicionarTextoOpcional(comando, "@documento", fornecedor.Documento);
                    AcessoDados.AdicionarTextoOpcional(comando, "@telefone", fornecedor.Telefone);
                    AcessoDados.AdicionarTextoOpcional(comando, "@email", fornecedor.Email);

                    comando.ExecuteNonQuery();
                }
            }
        }

        public bool Excluir(int id)
        {
            using (var conexao = Conexao.CriarConexao())
            {
                conexao.Open();

                string sql = "DELETE FROM fornecedor WHERE id_fornecedor = @id";

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@id", id);

                    int linhasAfetadas = comando.ExecuteNonQuery();

                    return linhasAfetadas > 0;
                }
            }
        }

    }
}
