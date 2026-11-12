using System;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class AutenticacaoService
    {
        private readonly UsuarioRepository usuarioRepository = new UsuarioRepository();
        private readonly PerfilSistemaRepository perfilSistemaRepository = new PerfilSistemaRepository();
        private readonly SenhaService senhaService = new SenhaService();

        public Usuario Autenticar(string login, string senha)
        {
            if (SessaoUsuario.EstaAutenticado)
            {
                throw new InvalidOperationException(
                    "Encerre a sessão atual antes de autenticar outra conta.");
            }

            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentException("Informe o login.", nameof(login));
            }

            if (string.IsNullOrEmpty(senha))
            {
                throw new ArgumentException("Informe a senha.", nameof(senha));
            }

            string loginNormalizado = login.Trim();
            Usuario usuario = usuarioRepository.BuscarPorLogin(loginNormalizado);

            if (usuario == null)
            {
                return null;
            }

            if (!senhaService.VerificarSenha(senha, usuario.SenhaHash))
            {
                return null;
            }

            if (!string.Equals(
                usuario.SituacaoConta,
                SituacoesUsuario.Aprovado,
                StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            if (!usuario.IdPerfilSistema.HasValue)
            {
                return null;
            }

            PerfilSistema perfil = perfilSistemaRepository.BuscarPorId(
                usuario.IdPerfilSistema.Value);

            if (perfil == null || !PerfilPermitido(perfil.Nome))
            {
                return null;
            }

            try
            {
                usuario.SituacaoConta = SituacoesUsuario.Normalizar(usuario.SituacaoConta);
            }
            catch (ArgumentException)
            {
                return null;
            }

            if (!string.Equals(
                usuario.SituacaoConta,
                SituacoesUsuario.Aprovado,
                StringComparison.Ordinal))
            {
                return null;
            }

            Usuario usuarioAutenticado = new Usuario();
            usuarioAutenticado.IdUsuario = usuario.IdUsuario;
            usuarioAutenticado.IdPessoa = usuario.IdPessoa;
            usuarioAutenticado.IdPerfilSistema = usuario.IdPerfilSistema;
            usuarioAutenticado.Login = usuario.Login;
            usuarioAutenticado.SituacaoConta = usuario.SituacaoConta;
            usuarioAutenticado.SolicitadoEm = usuario.SolicitadoEm;

            SessaoUsuario.Iniciar(usuarioAutenticado);

            return usuarioAutenticado;
        }

        public void EncerrarSessao()
        {
            SessaoUsuario.Encerrar();
        }

        private static bool PerfilPermitido(string nomePerfil)
        {
            if (string.Equals(nomePerfil, "Administrador", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Equals(nomePerfil, "Gerente", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Equals(nomePerfil, "Membro", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}
