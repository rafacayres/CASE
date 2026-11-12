using System;

namespace CASE.Models
{
    public class Registro
    {
        public int IdRegistro { get; set; }
        public int IdEscala { get; set; }
        public int IdUsuarioResponsavel { get; set; }
        public string Tipo { get; set; }
        public string FormaPagamento { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacao { get; set; }
        public string ChaveOperacao { get; set; }
        public DateTime? CriadoEm { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public uint Versao { get; set; }
    }
}
