using System;

namespace CASE.Models
{
    public class AnaliseUsuario
    {
        public int IdAnalise { get; set; }
        public int IdUsuario { get; set; }
        public int IdAnalisador { get; set; }
        public string Decisao { get; set; }
        public DateTime DataHora { get; set; }
        public string Justificativa { get; set; }
    }
}
