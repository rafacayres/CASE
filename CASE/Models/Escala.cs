using System;

namespace CASE.Models
{
    public class Escala
    {
        public int IdEscala { get; set; }
        public int IdEquipe { get; set; }
        public int IdJornada { get; set; }
        public TimeSpan EntradaPrevista { get; set; }
        public TimeSpan SaidaPrevista { get; set; }
        public string Situacao { get; set; }
    }
}
