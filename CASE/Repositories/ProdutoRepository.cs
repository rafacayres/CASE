using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

using System;
using CASE.Data;

namespace CASE.Repositories
{
    public class ProdutoRepository
    {

        private const string Colunas = "id_produto,id_categoria,nome,descricao,unidade_medida,ativo,imagem";

        public List<Produto> Listar()
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas + " FROM produto ORDER BY nome,id_produto",
                ConverterLinha
            );
        }

        public int Contar()
        {
            using (var conexao = Conexao.CriarConexao())
            {
                conexao.Open();

                string sql = "SELECT COUNT(*) FROM produto";

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }

        public Produto BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT " + Colunas + " FROM produto WHERE id_produto=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public int Cadastrar(Produto item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO produto (id_categoria,nome,descricao,unidade_medida,ativo,imagem) VALUES (@categoria,@nome,@descricao,@unidade,@ativo,@imagem)",
                comando => Vincular(comando, item)
            );
        }

        public bool Atualizar(Produto item)
        {
            return AcessoDados.Executar(
                "UPDATE produto SET id_categoria=@categoria,nome=@nome,descricao=@descricao,unidade_medida=@unidade,ativo=@ativo,imagem=@imagem WHERE id_produto=@id",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@id", item.IdProduto);
                    Vincular(comando, item);
                }) > 0;

        }

        private static void Vincular(MySqlCommand comando, Produto item)
        {
            AcessoDados.AdicionarParametro(comando, "@categoria", item.IdCategoria);
            AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", item.Nome);
            AcessoDados.AdicionarTextoOpcional(comando, "@descricao", item.Descricao);
            AcessoDados.AdicionarTextoObrigatorio(comando, "@unidade", item.UnidadeMedida);
            AcessoDados.AdicionarParametro(comando, "@ativo", item.Ativo);
            AcessoDados.AdicionarParametro(comando, "@imagem", item.Imagem);
        }

        private static Produto ConverterLinha(MySqlDataReader leitor)
        {
            return new Produto
            {
                IdProduto = leitor.GetInt32("id_produto"),
                IdCategoria = leitor.GetInt32("id_categoria"),
                Nome = leitor.GetString("nome"),
                Descricao = AcessoDados.LerTextoOpcional(leitor, "descricao"),
                UnidadeMedida = leitor.GetString("unidade_medida"),
                Ativo = leitor.GetBoolean("ativo"),
                Imagem = AcessoDados.LerBytesOpcional(leitor, "imagem")
            };
        }
    }
}
