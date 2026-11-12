using System;

namespace CASE.Models
{
    public class Apuracao
    {
        public int IdApuracao { get; set; }
        public int IdJornada { get; set; }
        public decimal ValorInformado { get; set; }
        public decimal? ValorDinheiro { get; set; }
        public decimal? ValorPix { get; set; }
        public decimal? ValorCartao { get; set; }
        public DateTime DataHora { get; set; }
        public int IdUsuarioResponsavel { get; set; }
        public string Observacao { get; set; }
    }
}
