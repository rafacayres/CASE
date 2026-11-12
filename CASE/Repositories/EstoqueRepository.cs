using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class EstoqueRepository
    {

        private const string Colunas = "id_estoque,id_local_estoque,id_produto,quantidade_minima,quantidade_ideal,quantidade_atual";

        public List<Estoque> ListarPorLocal(int idLocalEstoque)
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas + " FROM estoque WHERE id_local_estoque=@id ORDER BY id_produto",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idLocalEstoque)
            );
        }

        public Estoque BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT " + Colunas + " FROM estoque WHERE id_estoque=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public Estoque BuscarPorLocalEProduto(int idLocal, int idProduto)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT " + Colunas + " FROM estoque WHERE id_local_estoque=@local AND id_produto=@produto",
                ConverterLinha,
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@local", idLocal);
                    AcessoDados.AdicionarParametro(comando, "@produto", idProduto);
                }
            );
        }

        public int Cadastrar(Estoque item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO estoque (id_local_estoque,id_produto,quantidade_minima,quantidade_ideal) VALUES (@local,@produto,@minima,@ideal)",
                comando => Vincular(comando, item)
            );
        }

        public bool AtualizarQuantidadesAlvo(Estoque item)
        {
            return AcessoDados.Executar(
                "UPDATE estoque SET quantidade_minima=@minima,quantidade_ideal=@ideal WHERE id_estoque=@id",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@id", item.IdEstoque);
                    AcessoDados.AdicionarParametro(comando, "@minima", item.QuantidadeMinima);
                    AcessoDados.AdicionarParametro(comando, "@ideal", item.QuantidadeIdeal);
                }) > 0;

        }

        private static void Vincular(MySqlCommand comando, Estoque item)
        {
            AcessoDados.AdicionarParametro(comando, "@local", item.IdLocalEstoque);
            AcessoDados.AdicionarParametro(comando, "@produto", item.IdProduto);
            AcessoDados.AdicionarParametro(comando, "@minima", item.QuantidadeMinima);
            AcessoDados.AdicionarParametro(comando, "@ideal", item.QuantidadeIdeal);
        }

        private static Estoque ConverterLinha(MySqlDataReader leitor)
        {
            return new Estoque
            {
                IdEstoque = leitor.GetInt32("id_estoque"),
                IdLocalEstoque = leitor.GetInt32("id_local_estoque"),
                IdProduto = leitor.GetInt32("id_produto"),
                QuantidadeMinima = leitor.GetDecimal("quantidade_minima"),
                QuantidadeIdeal = leitor.GetDecimal("quantidade_ideal"),
                QuantidadeAtual = leitor.GetDecimal("quantidade_atual")
            };
        }
    }
}
