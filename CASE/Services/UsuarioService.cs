using System;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository usuarioRepository = new UsuarioRepository();
        private readonly SenhaService senhaService = new SenhaService();

        public System.Collections.Generic.List<Usuario> ListarCadastrosPendentes(Usuario administrador)
        {
            AutorizacaoService autorizacao = new AutorizacaoService();
            autorizacao.ExigirAdministrador(administrador);

            System.Collections.Generic.List<Usuario> usuarios = usuarioRepository.Listar();
            System.Collections.Generic.List<Usuario> pendentes =
                new System.Collections.Generic.List<Usuario>();

            foreach (Usuario usuario in usuarios)
            {
                if (string.Equals(
                    usuario.SituacaoConta,
                    SituacoesUsuario.Pendente,
                    StringComparison.OrdinalIgnoreCase))
                {
                    pendentes.Add(usuario);
                }
            }

            return pendentes;
        }

        public System.Collections.Generic.List<Usuario> ListarUsuariosAdministrativos(
            Usuario administrador)
        {
            new AutorizacaoService().ExigirAdministrador(administrador);
            return usuarioRepository.Listar();
        }

        public System.Collections.Generic.List<PerfilSistema> ListarPerfisDeSistema(
            Usuario administrador)
        {
            new AutorizacaoService().ExigirAdministrador(administrador);
            System.Collections.Generic.List<PerfilSistema> perfis =
                new PerfilSistemaRepository().Listar();
            System.Collections.Generic.List<PerfilSistema> permitidos =
                new System.Collections.Generic.List<PerfilSistema>();

            foreach (PerfilSistema perfil in perfis)
            {
                if (string.Equals(perfil.Nome, "Administrador", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(perfil.Nome, "Gerente", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(perfil.Nome, "Membro", StringComparison.OrdinalIgnoreCase))
                {
                    permitidos.Add(perfil);
                }
            }

            return permitidos;
        }

        public void AprovarCadastro(Usuario administrador, int idUsuario, int idPerfilSistema)
        {
            string nomePerfil;
            Usuario adminAtual = new AutorizacaoService().ValidarUsuarioEObterPerfil(
                administrador,
                out nomePerfil);

            if (!string.Equals(nomePerfil, "Administrador", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Somente um administrador pode aprovar cadastros.");
            }

            if (idUsuario <= 0 || idPerfilSistema <= 0)
            {
                throw new ArgumentException("Selecione um cadastro e um perfil válidos.");
            }

            PerfilSistema perfil = new PerfilSistemaRepository().BuscarPorId(idPerfilSistema);

            if (perfil == null ||
                (!string.Equals(perfil.Nome, "Administrador", StringComparison.OrdinalIgnoreCase) &&
                 !string.Equals(perfil.Nome, "Gerente", StringComparison.OrdinalIgnoreCase) &&
                 !string.Equals(perfil.Nome, "Membro", StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException("O perfil de sistema selecionado não é válido.", nameof(idPerfilSistema));
            }

            usuarioRepository.RegistrarDecisaoCadastro(
                idUsuario,
                adminAtual.IdUsuario,
                idPerfilSistema,
                SituacoesUsuario.Aprovado,
                null);
        }

        public void RejeitarCadastro(Usuario administrador, int idUsuario, string justificativa)
        {
            string nomePerfil;
            Usuario adminAtual = new AutorizacaoService().ValidarUsuarioEObterPerfil(
                administrador,
                out nomePerfil);

            if (!string.Equals(nomePerfil, "Administrador", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Somente um administrador pode rejeitar cadastros.");
            }

            if (idUsuario <= 0)
            {
                throw new ArgumentException("Selecione um cadastro válido.", nameof(idUsuario));
            }

            if (string.IsNullOrWhiteSpace(justificativa) || justificativa.Trim().Length > 500)
            {
                throw new ArgumentException("Informe um motivo com até 500 caracteres.", nameof(justificativa));
            }

            usuarioRepository.RegistrarDecisaoCadastro(
                idUsuario,
                adminAtual.IdUsuario,
                null,
                SituacoesUsuario.Rejeitado,
                justificativa.Trim());
        }

        public System.Collections.Generic.List<AnaliseUsuario> ListarHistorico(
            Usuario usuario,
            int idUsuario)
        {
            string nomePerfil;
            Usuario usuarioAtual = new AutorizacaoService().ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(nomePerfil, "Administrador", StringComparison.OrdinalIgnoreCase) &&
                usuarioAtual.IdUsuario != idUsuario)
            {
                throw new UnauthorizedAccessException(
                    "Você não pode consultar o histórico deste cadastro.");
            }

            return new AnaliseUsuarioRepository().ListarPorUsuario(idUsuario);
        }

        public int SolicitarCadastro(Pessoa pessoa, string login, string senha)
        {
            if (pessoa == null)
            {
                throw new ArgumentNullException(nameof(pessoa));
            }

            if (string.IsNullOrWhiteSpace(pessoa.Nome))
            {
                throw new ArgumentException("O nome é obrigatório.", nameof(pessoa));
            }

            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentException("O login é obrigatório.", nameof(login));
            }

            if (string.IsNullOrEmpty(senha))
            {
                throw new ArgumentException("A senha é obrigatória.", nameof(senha));
            }

            string loginNormalizado = login.Trim();
            Usuario usuarioExistente = usuarioRepository.BuscarPorLogin(loginNormalizado);

            if (usuarioExistente != null)
            {
                throw new InvalidOperationException("Já existe uma conta com esse login.");
            }

            pessoa.Nome = pessoa.Nome.Trim();

            if (pessoa.FotoPerfil != null)
            {
                FotoPerfilService fotoPerfilService = new FotoPerfilService();
                pessoa.FotoPerfil = fotoPerfilService.PrepararFoto(pessoa.FotoPerfil);
            }

            Usuario novoUsuario = new Usuario();
            novoUsuario.Login = loginNormalizado;
            novoUsuario.SenhaHash = senhaService.GerarHash(senha);
            novoUsuario.SituacaoConta = SituacoesUsuario.Pendente;
            novoUsuario.IdPerfilSistema = null;

            return usuarioRepository.CadastrarSolicitacao(pessoa, novoUsuario);
        }
    }
}
