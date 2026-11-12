using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class ProdutoFornecedorRepository
    {

        public List<ProdutoFornecedor> ListarPorProduto(int idProduto)
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_produto,id_fornecedor,codigo_no_fornecedor FROM produtofornecedor WHERE id_produto=@id ORDER BY id_fornecedor",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idProduto)
            );
        }

        public ProdutoFornecedor BuscarPorChave(int idProduto, int idFornecedor)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT id_produto,id_fornecedor,codigo_no_fornecedor FROM produtofornecedor WHERE id_produto=@produto AND id_fornecedor=@fornecedor",
                ConverterLinha,
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@produto", idProduto);
                    AcessoDados.AdicionarParametro(comando, "@fornecedor", idFornecedor);
                });

        }

        public void Vincular(ProdutoFornecedor item)
        {
            AcessoDados.Executar(
                "INSERT INTO produtofornecedor (id_produto,id_fornecedor,codigo_no_fornecedor) VALUES (@produto,@fornecedor,@codigo)",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@produto", item.IdProduto);
                    AcessoDados.AdicionarParametro(comando, "@fornecedor", item.IdFornecedor);
                    AcessoDados.AdicionarTextoOpcional(comando, "@codigo", item.CodigoNoFornecedor);
                });

        }

        public bool AtualizarCodigo(int idProduto, int idFornecedor, string codigo)
        {
            return AcessoDados.Executar(
                "UPDATE produtofornecedor SET codigo_no_fornecedor=@codigo WHERE id_produto=@produto AND id_fornecedor=@fornecedor",
                comando =>
                {
                    AcessoDados.AdicionarTextoOpcional(comando, "@codigo", codigo);
                    AcessoDados.AdicionarParametro(comando, "@produto", idProduto);
                    AcessoDados.AdicionarParametro(comando, "@fornecedor", idFornecedor);
                }) > 0;

        }

        public bool Desvincular(int idProduto, int idFornecedor)
        {
            return AcessoDados.Executar(
                "DELETE FROM produtofornecedor WHERE id_produto=@produto AND id_fornecedor=@fornecedor",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@produto", idProduto);
                    AcessoDados.AdicionarParametro(comando, "@fornecedor", idFornecedor);
                }) > 0;

        }

        private static ProdutoFornecedor ConverterLinha(MySqlDataReader leitor)
        {
            return new ProdutoFornecedor
            {
                IdProduto = leitor.GetInt32("id_produto"),
                IdFornecedor = leitor.GetInt32("id_fornecedor"),
                CodigoNoFornecedor = AcessoDados.LerTextoOpcional(leitor, "codigo_no_fornecedor")
            };
        }
    }
}
