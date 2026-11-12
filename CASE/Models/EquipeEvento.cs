using System;

namespace CASE.Models
{
    public class EquipeEvento
    {
        public int IdEquipe { get; set; }
        public int IdEvento { get; set; }
        public int IdPessoa { get; set; }
        public int IdPerfil { get; set; }
        public bool Ativo { get; set; }
        public int? IdUsuarioCriacao { get; set; }
        public DateTime? CriadoEm { get; set; }
        public int? IdUsuarioAlteracao { get; set; }
        public DateTime? AtualizadoEm { get; set; }
        public uint Versao { get; set; }
    }
}
