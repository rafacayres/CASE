namespace CASE.Models
{
    public class Estoque
    {
        public int IdEstoque { get; set; }
        public int IdLocalEstoque { get; set; }
        public int IdProduto { get; set; }
        public decimal QuantidadeMinima { get; set; }
        public decimal QuantidadeIdeal { get; set; }
    }
}
