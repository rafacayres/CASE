using System;

namespace CASE.Models
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public int IdInstituicao { get; set; }
        public int IdTipoEvento { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int IdResponsavel { get; set; }
        public string Situacao { get; set; }
        public string Observacao { get; set; }
    }
}
