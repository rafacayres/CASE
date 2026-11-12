using System;

namespace CASE.Models
{
    public class SolicitacaoEstoqueCentral
    {
        public int IdSolicitacaoEstoqueCentral { get; set; }
        public string Tipo { get; set; }
        public int IdInstituicao { get; set; }
        public int IdEvento { get; set; }
        public int IdLocalOrigem { get; set; }
        public int IdLocalDestino { get; set; }
        public int IdUsuarioSolicitante { get; set; }
        public DateTime DataHora { get; set; }
        public string Situacao { get; set; }
        public int? IdUsuarioDecisor { get; set; }
        public DateTime? DecididaEm { get; set; }
        public string JustificativaRejeicao { get; set; }
        public uint Versao { get; set; }
    }
}
