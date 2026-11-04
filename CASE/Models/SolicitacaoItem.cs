namespace CASE.Models
{
    public class SolicitacaoItem
    {
        public int IdSolicitacaoItem { get; set; }
        public int IdSolicitacao { get; set; }
        public int IdProdutoSetor { get; set; }
        public decimal Quantidade { get; set; }
    }
}
