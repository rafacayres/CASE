using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class TransferenciaItemRepository
    {

        public List<TransferenciaItem> ListarPorTransferencia(int idTransferencia)
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_transferencia_item,id_transferencia,id_produto,quantidade FROM transferenciaitem WHERE id_transferencia=@id ORDER BY id_transferencia_item",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idTransferencia)
            );
        }

        private static TransferenciaItem ConverterLinha(MySqlDataReader leitor)
        {
            return new TransferenciaItem
            {
                IdTransferenciaItem = leitor.GetInt32("id_transferencia_item"),
                IdTransferencia = leitor.GetInt32("id_transferencia"),
                IdProduto = leitor.GetInt32("id_produto"),
                Quantidade = leitor.GetDecimal("quantidade")
            };
        }
    }
}
