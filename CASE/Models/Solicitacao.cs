using System;

namespace CASE.Models
{
    public class Solicitacao
    {
        public int IdSolicitacao { get; set; }
        public int IdEscala { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHora { get; set; }
        public string Situacao { get; set; }
    }
}
