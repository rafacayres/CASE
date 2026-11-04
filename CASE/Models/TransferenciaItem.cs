namespace CASE.Models
{
    public class TransferenciaItem
    {
        public int IdTransferenciaItem { get; set; }
        public int IdTransferencia { get; set; }
        public int IdProduto { get; set; }
        public decimal Quantidade { get; set; }
    }
}
