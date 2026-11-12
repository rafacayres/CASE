using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;
using System;

namespace CASE.Repositories
{
    public class InstituicaoRepository
    {
        public int SalvarUnica(Instituicao item)
        {
            using (var conexao = CASE.Data.Conexao.CriarConexao())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    int existente = 0;
                    using (var comando = new MySqlCommand("SELECT id_instituicao FROM instituicao FOR UPDATE", conexao, transacao))
                    using (var leitor = comando.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            if (existente != 0)
                            {
                                throw new InvalidOperationException("Há mais de uma instituição cadastrada. Revise a configuração antes de continuar.");
                            }
                            existente = leitor.GetInt32(0);
                        }
                    }
                    if (existente != item.IdInstituicao)
                    {
                        throw new InvalidOperationException("A configuração institucional mudou. Reabra a tela antes de salvar.");
                    }
                    string sql = "INSERT INTO instituicao(nome,documento,telefone,email) VALUES (@nome,@documento,@telefone,@email)";
                    if (existente > 0)
                    {
                        sql = "UPDATE instituicao SET nome=@nome,documento=@documento,telefone=@telefone,email=@email WHERE id_instituicao=@id";
                    }
                    using (var comando = new MySqlCommand(sql, conexao, transacao))
                    {
                        comando.Parameters.AddWithValue("@id", existente);
                        AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", item.Nome);
                        AcessoDados.AdicionarTextoOpcional(comando, "@documento", item.Documento);
                        AcessoDados.AdicionarTextoOpcional(comando, "@telefone", item.Telefone);
                        AcessoDados.AdicionarTextoOpcional(comando, "@email", item.Email);
                        comando.ExecuteNonQuery();
                        if (existente == 0)
                        {
                            existente = checked((int)comando.LastInsertedId);
                        }
                    }
                    transacao.Commit();
                    return existente;
                }
            }
        }

        public List<Instituicao> Listar()
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_instituicao,nome,documento,telefone,email FROM instituicao ORDER BY id_instituicao",
                ConverterLinha
            );
        }

        public Instituicao BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT id_instituicao,nome,documento,telefone,email FROM instituicao WHERE id_instituicao=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public int Cadastrar(Instituicao item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO instituicao (nome,documento,telefone,email) VALUES (@nome,@documento,@telefone,@email)",
                comando =>
                {
                    AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", item.Nome);
                    AcessoDados.AdicionarTextoOpcional(comando, "@documento", item.Documento);
                    AcessoDados.AdicionarTextoOpcional(comando, "@telefone", item.Telefone);
                    AcessoDados.AdicionarTextoOpcional(comando, "@email", item.Email);
                });

        }

        public bool Atualizar(Instituicao item)
        {
            return AcessoDados.Executar(
                "UPDATE instituicao SET nome=@nome,documento=@documento,telefone=@telefone,email=@email WHERE id_instituicao=@id",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@id", item.IdInstituicao);
                    AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", item.Nome);
                    AcessoDados.AdicionarTextoOpcional(comando, "@documento", item.Documento);
                    AcessoDados.AdicionarTextoOpcional(comando, "@telefone", item.Telefone);
                    AcessoDados.AdicionarTextoOpcional(comando, "@email", item.Email);
                }) > 0;

        }

        private static Instituicao ConverterLinha(MySqlDataReader leitor)
        {
            return new Instituicao
            {
                IdInstituicao = leitor.GetInt32("id_instituicao"),
                Nome = leitor.GetString("nome"),
                Documento = AcessoDados.LerTextoOpcional(leitor, "documento"),
                Telefone = AcessoDados.LerTextoOpcional(leitor, "telefone"),
                Email = AcessoDados.LerTextoOpcional(leitor, "email")
            };
        }
    }
}
