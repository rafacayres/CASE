namespace CASE.Models
{
    public class EntradaItem
    {
        public int IdEntradaItem { get; set; }
        public int IdEntrada { get; set; }
        public int IdProduto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal? CustoUnitario { get; set; }
    }
}
