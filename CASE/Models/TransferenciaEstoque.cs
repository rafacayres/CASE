using System;

namespace CASE.Models
{
    public class TransferenciaEstoque
    {
        public int IdTransferencia { get; set; }
        public int IdLocalOrigem { get; set; }
        public int IdLocalDestino { get; set; }
        public int? IdSolicitacao { get; set; }
        public int IdUsuarioResponsavel { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacao { get; set; }
    }
}
