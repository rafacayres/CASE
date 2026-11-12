using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class ProdutoSetorRepository
    {

        private const string Colunas = "id_produto_setor,id_setor,id_produto,preco_venda";

        public List<ProdutoSetor> ListarPorSetor(int idSetor)
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas + " FROM produtosetor WHERE id_setor=@id ORDER BY id_produto_setor",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idSetor)
            );
        }

        public ProdutoSetor BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT " + Colunas + " FROM produtosetor WHERE id_produto_setor=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public int Cadastrar(ProdutoSetor item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO produtosetor (id_setor,id_produto,preco_venda) VALUES (@setor,@produto,@preco)",
                comando => Vincular(comando, item)
            );
        }

        public int SalvarPreco(ProdutoSetor item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO produtosetor (id_setor,id_produto,preco_venda) VALUES (@setor,@produto,@preco) " +
                "ON DUPLICATE KEY UPDATE id_produto_setor=LAST_INSERT_ID(id_produto_setor),preco_venda=@preco",
                comando => Vincular(comando, item));
        }

        public bool Atualizar(ProdutoSetor item)
        {
            return AcessoDados.Executar(
                "UPDATE produtosetor SET id_setor=@setor,id_produto=@produto,preco_venda=@preco WHERE id_produto_setor=@id",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@id", item.IdProdutoSetor);
                    Vincular(comando, item);
                }) > 0;

        }

        private static void Vincular(MySqlCommand comando, ProdutoSetor item)
        {
            AcessoDados.AdicionarParametro(comando, "@setor", item.IdSetor);
            AcessoDados.AdicionarParametro(comando, "@produto", item.IdProduto);
            AcessoDados.AdicionarParametro(comando, "@preco", item.PrecoVenda);
        }

        private static ProdutoSetor ConverterLinha(MySqlDataReader leitor)
        {
            return new ProdutoSetor
            {
                IdProdutoSetor = leitor.GetInt32("id_produto_setor"),
                IdSetor = leitor.GetInt32("id_setor"),
                IdProduto = leitor.GetInt32("id_produto"),
                PrecoVenda = leitor.GetDecimal("preco_venda")
            };
        }
    }
}
