namespace CASE.Models
{
    public class RegistroItem
    {
        public int IdRegistroItem { get; set; }
        public int IdRegistro { get; set; }
        public int IdProdutoSetor { get; set; }
        public decimal Quantidade { get; set; }
        public decimal? PrecoUnitarioPraticado { get; set; }
    }
}
