using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class RegistroItemRepository
    {

        public List<RegistroItem> ListarPorRegistro(int idRegistro)
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_registro_item,id_registro,id_produto_setor,quantidade,preco_unitario_praticado FROM registroitem WHERE id_registro=@id ORDER BY id_registro_item",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idRegistro)
            );
        }

        private static RegistroItem ConverterLinha(MySqlDataReader leitor)
        {
            return new RegistroItem
            {
                IdRegistroItem = leitor.GetInt32("id_registro_item"),
                IdRegistro = leitor.GetInt32("id_registro"),
                IdProdutoSetor = leitor.GetInt32("id_produto_setor"),
                Quantidade = leitor.GetDecimal("quantidade"),
                PrecoUnitarioPraticado = AcessoDados.LerDecimalOpcional(leitor, "preco_unitario_praticado")
            };
        }
    }
}
