using System;

namespace CASE.Models
{
    public class EntradaMercadoria
    {
        public int IdEntrada { get; set; }
        public int? IdFornecedor { get; set; }
        public int IdLocalDestino { get; set; }
        public int IdUsuarioResponsavel { get; set; }
        public string TipoEntrada { get; set; }
        public DateTime DataHora { get; set; }
        public string NumeroDocumento { get; set; }
        public string Observacao { get; set; }
    }
}
