using System;

namespace CASE.Models
{
    public class Despesa
    {
        public int IdDespesa { get; set; }
        public int IdEvento { get; set; }
        public int? IdFornecedor { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public int IdUsuarioResponsavel { get; set; }
    }
}
