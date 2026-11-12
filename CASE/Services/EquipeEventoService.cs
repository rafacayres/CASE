using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class EquipeEventoService
    {
        private readonly EquipeEventoRepository equipeEventoRepository = new EquipeEventoRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();
        private readonly EventoRepository eventoRepository = new EventoRepository();
        private readonly PerfilRepository perfilRepository = new PerfilRepository();
        private readonly UsuarioRepository usuarioRepository = new UsuarioRepository();

        public List<Perfil> ListarPerfis(Usuario usuario, int idEvento)
        {
            ValidarGerenteDoEvento(usuario, idEvento);
            return perfilRepository.Listar();
        }

        public List<Pessoa> ListarPessoasDisponiveis(Usuario usuario, int idEvento)
        {
            ValidarGerenteDoEvento(usuario, idEvento);
            return equipeEventoRepository.ListarPessoasDisponiveis(idEvento);
        }

        public Pessoa BuscarPessoaDisponivelPorLogin(
            Usuario usuario,
            int idEvento,
            string login)
        {
            ValidarGerenteDoEvento(usuario, idEvento);

            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentException("Informe o login do membro.", nameof(login));
            }

            return equipeEventoRepository.BuscarPessoaDisponivelPorLogin(
                idEvento,
                login.Trim());
        }

        public Pessoa BuscarPessoaVinculada(
            Usuario usuario,
            int idEvento,
            int idEquipe)
        {
            ValidarGerenteDoEvento(usuario, idEvento);
            EquipeEvento vinculo = equipeEventoRepository.BuscarPorId(idEquipe);

            if (vinculo == null || vinculo.IdEvento != idEvento)
            {
                throw new InvalidOperationException("O vínculo de equipe não foi encontrado.");
            }

            return equipeEventoRepository.BuscarPessoaPorId(vinculo.IdPessoa);
        }

        public string BuscarLoginDaPessoaVinculada(
            Usuario usuario,
            int idEvento,
            int idEquipe)
        {
            Pessoa pessoa = BuscarPessoaVinculada(usuario, idEvento, idEquipe);

            if (pessoa == null)
            {
                return string.Empty;
            }

            return usuarioRepository.BuscarLoginPorPessoa(pessoa.IdPessoa);
        }

        public int VincularMembro(
            Usuario usuario,
            int idEvento,
            string login,
            int idPerfil)
        {
            Usuario usuarioAtual = ValidarGerenteDoEvento(usuario, idEvento);
            Perfil perfil = perfilRepository.BuscarPorId(idPerfil);

            if (perfil == null)
            {
                throw new ArgumentException("Selecione um perfil válido para o evento.", nameof(idPerfil));
            }

            string loginPesquisado = string.Empty;

            if (login != null)
            {
                loginPesquisado = login.Trim();
            }

            Pessoa pessoa = equipeEventoRepository.BuscarPessoaDisponivelPorLogin(
                idEvento,
                loginPesquisado);

            if (pessoa == null)
            {
                throw new InvalidOperationException(
                    "Não foi encontrado um membro aprovado e disponível com esse login.");
            }

            EquipeEvento itemExistente = BuscarVinculoDaPessoa(idEvento, pessoa.IdPessoa);

            if (itemExistente != null)
            {
                if (itemExistente.Ativo)
                {
                    throw new InvalidOperationException("A pessoa já está vinculada a este evento.");
                }

                itemExistente.IdPerfil = idPerfil;
                itemExistente.Ativo = true;
                itemExistente.IdUsuarioAlteracao = usuarioAtual.IdUsuario;

                if (!equipeEventoRepository.Atualizar(itemExistente))
                {
                    throw new InvalidOperationException(
                        "O vínculo foi alterado por outra operação. Atualize a lista e tente novamente.");
                }

                return itemExistente.IdEquipe;
            }

            EquipeEvento novoVinculo = new EquipeEvento();
            novoVinculo.IdEvento = idEvento;
            novoVinculo.IdPessoa = pessoa.IdPessoa;
            novoVinculo.IdPerfil = idPerfil;
            novoVinculo.Ativo = true;
            novoVinculo.IdUsuarioCriacao = usuarioAtual.IdUsuario;
            novoVinculo.CriadoEm = DateTime.Now;

            return equipeEventoRepository.Cadastrar(novoVinculo);
        }

        public void AtualizarVinculo(Usuario usuario, EquipeEvento vinculo)
        {
            if (vinculo == null || vinculo.IdEquipe <= 0)
            {
                throw new ArgumentException("O vínculo de equipe é inválido.", nameof(vinculo));
            }

            Usuario usuarioAtual = ValidarGerenteDoEvento(usuario, vinculo.IdEvento);
            EquipeEvento atual = equipeEventoRepository.BuscarPorId(vinculo.IdEquipe);

            if (atual == null || atual.IdEvento != vinculo.IdEvento ||
                atual.IdPessoa != vinculo.IdPessoa)
            {
                throw new InvalidOperationException("O vínculo de equipe não corresponde ao evento.");
            }

            if (perfilRepository.BuscarPorId(vinculo.IdPerfil) == null)
            {
                throw new ArgumentException("Selecione um perfil válido para o evento.", nameof(vinculo));
            }

            if (atual.Versao != vinculo.Versao)
            {
                throw new InvalidOperationException(
                    "O vínculo foi alterado desde que a tela foi aberta. Atualize a lista.");
            }

            vinculo.IdUsuarioCriacao = atual.IdUsuarioCriacao;
            vinculo.CriadoEm = atual.CriadoEm;
            vinculo.IdUsuarioAlteracao = usuarioAtual.IdUsuario;

            if (!equipeEventoRepository.Atualizar(vinculo))
            {
                throw new InvalidOperationException(
                    "O vínculo foi alterado por outra operação. Atualize a lista e tente novamente.");
            }
        }

        private Usuario ValidarGerenteDoEvento(Usuario usuario, int idEvento)
        {
            if (idEvento <= 0)
            {
                throw new ArgumentException("O evento informado é inválido.", nameof(idEvento));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(nomePerfil, "Gerente", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException(
                    "Somente o gerente responsável pode administrar a equipe do evento.");
            }

            Evento evento = eventoRepository.BuscarPorId(idEvento);

            if (evento == null || evento.IdResponsavel != usuarioAtual.IdPessoa)
            {
                throw new UnauthorizedAccessException(
                    "Você não pode administrar a equipe deste evento.");
            }

            return usuarioAtual;
        }

        private EquipeEvento BuscarVinculoDaPessoa(int idEvento, int idPessoa)
        {
            List<EquipeEvento> equipe = equipeEventoRepository.ListarPorEvento(idEvento);

            foreach (EquipeEvento membro in equipe)
            {
                if (membro.IdPessoa == idPessoa)
                {
                    return membro;
                }
            }

            return null;
        }

        public List<EquipeEvento> ListarPorEvento(Usuario usuario, int idEvento)
        {
            if (idEvento <= 0)
            {
                throw new ArgumentException(
                    "O identificador do evento deve ser maior que zero.",
                    nameof(idEvento));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            List<EquipeEvento> equipe = equipeEventoRepository.ListarPorEvento(idEvento);
            List<EquipeEvento> equipePermitida = new List<EquipeEvento>();

            foreach (EquipeEvento membro in equipe)
            {
                if (autorizacaoService.PodeAcessarEquipe(usuarioAtual, nomePerfil, membro))
                {
                    equipePermitida.Add(membro);
                }
            }

            return equipePermitida;
        }

        public EquipeEvento BuscarPorId(Usuario usuario, int idEquipe)
        {
            if (idEquipe <= 0)
            {
                throw new ArgumentException(
                    "O identificador da equipe deve ser maior que zero.",
                    nameof(idEquipe));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            EquipeEvento membro = equipeEventoRepository.BuscarPorId(idEquipe);

            if (membro != null && !autorizacaoService.PodeAcessarEquipe(
                usuarioAtual,
                nomePerfil,
                membro))
            {
                throw new UnauthorizedAccessException("Você não pode acessar esta equipe.");
            }

            return membro;
        }
    }
}
