using System;

namespace CASE.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdPessoa { get; set; }
        public int? IdPerfilSistema { get; set; }
        public string Login { get; set; }
        public string SenhaHash { get; set; }
        public string SituacaoConta { get; set; }
        public DateTime SolicitadoEm { get; set; }
    }
}
