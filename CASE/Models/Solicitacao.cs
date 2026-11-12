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
        public int? IdUsuarioSolicitante { get; set; }
        public int? IdUsuarioDecisor { get; set; }
        public DateTime? DecididaEm { get; set; }
        public string JustificativaRejeicao { get; set; }
        public DateTime? CriadoEm { get; set; }
        public uint Versao { get; set; }
    }
}
