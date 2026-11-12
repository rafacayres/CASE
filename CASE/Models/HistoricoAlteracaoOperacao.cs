using System;

namespace CASE.Models
{
    public class HistoricoAlteracaoOperacao
    {
        public ulong IdHistorico { get; set; }
        public string Entidade { get; set; }
        public int IdEntidade { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataHora { get; set; }
        public string Justificativa { get; set; }
        public string DadosAnteriores { get; set; }
        public string DadosNovos { get; set; }
    }
}
