namespace CASE.Models
{
    public class SolicitacaoEstoqueCentralItem
    {
        public int IdSolicitacaoEstoqueCentralItem { get; set; }
        public int IdSolicitacaoEstoqueCentral { get; set; }
        public int IdProduto { get; set; }
        public decimal Quantidade { get; set; }
    }
}
