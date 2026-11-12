using System;

namespace CASE.Models
{
    public class TransferenciaEstoque
    {
        public int IdTransferencia { get; set; }
        public int IdLocalOrigem { get; set; }
        public int IdLocalDestino { get; set; }
        public int? IdSolicitacao { get; set; }
        public int? IdSolicitacaoEstoqueCentral { get; set; }
        public int IdUsuarioResponsavel { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacao { get; set; }
        public string ChaveOperacao { get; set; }
        public DateTime? CriadoEm { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public uint Versao { get; set; }
    }
}
