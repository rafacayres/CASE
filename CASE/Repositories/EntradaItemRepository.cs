using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class EntradaItemRepository
    {

        public List<EntradaItem> ListarPorEntrada(int idEntrada)
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_entrada_item,id_entrada,id_produto,quantidade,custo_unitario FROM entradaitem WHERE id_entrada=@id ORDER BY id_entrada_item",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idEntrada)
            );
        }

        private static EntradaItem ConverterLinha(MySqlDataReader leitor)
        {
            return new EntradaItem
            {
                IdEntradaItem = leitor.GetInt32("id_entrada_item"),
                IdEntrada = leitor.GetInt32("id_entrada"),
                IdProduto = leitor.GetInt32("id_produto"),
                Quantidade = leitor.GetDecimal("quantidade"),
                CustoUnitario = AcessoDados.LerDecimalOpcional(leitor, "custo_unitario")
            };
        }
    }
}
