namespace CASE.Models
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public int IdCategoria { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string UnidadeMedida { get; set; }
        public bool Ativo { get; set; }
        public byte[] Imagem { get; set; }
    }
}
