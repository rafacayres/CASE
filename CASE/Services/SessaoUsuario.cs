using System;
using CASE.Models;

namespace CASE.Services
{
    internal static class SessaoUsuario
    {
        private static Usuario usuarioAtual;

        public static bool EstaAutenticado
        {
            get
            {
                return usuarioAtual != null;
            }
        }

        public static Usuario UsuarioAtual
        {
            get
            {
                if (usuarioAtual == null)
                {
                    return null;
                }

                return CopiarIdentidade(usuarioAtual);
            }
        }

        public static void Iniciar(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new ArgumentNullException(nameof(usuario));
            }

            if (usuario.IdUsuario <= 0 || usuario.IdPessoa <= 0)
            {
                throw new UnauthorizedAccessException("A identidade autenticada é inválida.");
            }

            if (!usuario.IdPerfilSistema.HasValue)
            {
                throw new UnauthorizedAccessException("A conta não possui um perfil de acesso.");
            }

            if (!string.Equals(
                usuario.SituacaoConta,
                SituacoesUsuario.Aprovado,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("A conta não está aprovada para acessar o sistema.");
            }

            if (usuarioAtual != null && usuarioAtual.IdUsuario != usuario.IdUsuario)
            {
                throw new InvalidOperationException(
                    "Encerre a sessão atual antes de iniciar uma sessão para outra conta.");
            }

            usuarioAtual = CopiarIdentidade(usuario);
        }

        public static Usuario ObterUsuarioAtual()
        {
            if (usuarioAtual == null)
            {
                throw new UnauthorizedAccessException("É necessário estar autenticado.");
            }

            return CopiarIdentidade(usuarioAtual);
        }

        public static void ExigirUsuario(Usuario usuario)
        {
            if (usuario == null)
            {
                throw new UnauthorizedAccessException("É necessário estar autenticado.");
            }

            if (usuarioAtual == null)
            {
                throw new UnauthorizedAccessException("Não existe uma sessão autenticada.");
            }

            if (usuario.IdUsuario != usuarioAtual.IdUsuario ||
                usuario.IdPessoa != usuarioAtual.IdPessoa ||
                usuario.IdPerfilSistema != usuarioAtual.IdPerfilSistema)
            {
                throw new UnauthorizedAccessException(
                    "A identidade informada não corresponde à sessão autenticada.");
            }
        }

        public static void Encerrar()
        {
            usuarioAtual = null;
        }

        private static Usuario CopiarIdentidade(Usuario usuario)
        {
            Usuario copia = new Usuario();
            copia.IdUsuario = usuario.IdUsuario;
            copia.IdPessoa = usuario.IdPessoa;
            copia.IdPerfilSistema = usuario.IdPerfilSistema;
            copia.Login = usuario.Login;
            copia.SituacaoConta = usuario.SituacaoConta;
            copia.SolicitadoEm = usuario.SolicitadoEm;

            return copia;
        }
    }
}
