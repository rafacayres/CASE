using System;

namespace CASE.Models
{
    public class Registro
    {
        public int IdRegistro { get; set; }
        public int IdEscala { get; set; }
        public int IdUsuarioResponsavel { get; set; }
        public string Tipo { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacao { get; set; }
    }
}
