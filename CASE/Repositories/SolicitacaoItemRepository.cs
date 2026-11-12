using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class SolicitacaoItemRepository
    {

        public List<SolicitacaoItem> ListarPorSolicitacao(int idSolicitacao)
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_solicitacao_item,id_solicitacao,id_produto_setor,quantidade FROM solicitacaoitem WHERE id_solicitacao=@id ORDER BY id_solicitacao_item",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idSolicitacao)
            );
        }

        private static SolicitacaoItem ConverterLinha(MySqlDataReader leitor)
        {
            return new SolicitacaoItem
            {
                IdSolicitacaoItem = leitor.GetInt32("id_solicitacao_item"),
                IdSolicitacao = leitor.GetInt32("id_solicitacao"),
                IdProdutoSetor = leitor.GetInt32("id_produto_setor"),
                Quantidade = leitor.GetDecimal("quantidade")
            };
        }
    }
}
