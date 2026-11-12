namespace CASE.Models
{
    public class LocalEstoque
    {
        public int IdLocalEstoque { get; set; }
        public int IdInstituicao { get; set; }
        public int? IdEvento { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public bool EhCentral { get; set; }
        public bool EhGeralEvento { get; set; }
    }
}
