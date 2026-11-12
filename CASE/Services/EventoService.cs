using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class EventoService
    {
        private readonly EventoRepository eventoRepository = new EventoRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();
        private readonly InstituicaoRepository instituicaoRepository = new InstituicaoRepository();
        private readonly TipoEventoRepository tipoEventoRepository = new TipoEventoRepository();
        private readonly LocalEstoqueRepository localEstoqueRepository = new LocalEstoqueRepository();
        private readonly EstoqueRepository estoqueRepository = new EstoqueRepository();

        public string ConsultarNomeResponsavel(Usuario usuario, int idEvento)
        {
            Evento evento = BuscarPorId(usuario, idEvento);
            if (evento == null)
            {
                throw new ArgumentException("O evento não foi encontrado.");
            }
            Pessoa pessoa = new PessoaRepository().BuscarPorId(evento.IdResponsavel);
            if (pessoa == null)
            {
                throw new InvalidOperationException("O responsável do evento não foi encontrado.");
            }
            return pessoa.Nome;
        }

        public List<Evento> Listar(Usuario usuario)
        {
            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (string.Equals(nomePerfil, "Gerente", StringComparison.OrdinalIgnoreCase))
            {
                return eventoRepository.ListarPorResponsavel(usuarioAtual.IdPessoa);
            }

            if (string.Equals(nomePerfil, "Administrador", StringComparison.OrdinalIgnoreCase))
            {
                return eventoRepository.ListarTodos();
            }

            throw new UnauthorizedAccessException(
                "Seu perfil não pode consultar a administração de eventos.");
        }

        public int ObterIdInstituicaoUnica(Usuario usuario)
        {
            string nomePerfil;
            autorizacaoService.ValidarUsuarioEObterPerfil(usuario, out nomePerfil);

            bool podeCriarEvento = string.Equals(
                nomePerfil,
                "Gerente",
                StringComparison.OrdinalIgnoreCase);

            if (!podeCriarEvento)
            {
                throw new UnauthorizedAccessException(
                    "Somente um gerente pode criar eventos.");
            }

            List<Instituicao> instituicoes = instituicaoRepository.Listar();

            if (instituicoes.Count != 1)
            {
                throw new InvalidOperationException(
                    "Cadastre exatamente uma instituição antes de criar eventos.");
            }

            return instituicoes[0].IdInstituicao;
        }

        public int Criar(Usuario usuario, Evento evento)
        {
            if (evento == null)
            {
                throw new ArgumentNullException(nameof(evento));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(nomePerfil, "Gerente", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException(
                    "Somente gerentes podem criar eventos.");
            }

            List<Instituicao> instituicoes = instituicaoRepository.Listar();

            if (instituicoes.Count != 1 ||
                instituicoes[0].IdInstituicao != evento.IdInstituicao)
            {
                throw new InvalidOperationException(
                    "O CASE precisa ter cadastrada sua única instituição antes da criação de eventos.");
            }

            if (tipoEventoRepository.BuscarPorId(evento.IdTipoEvento) == null)
            {
                throw new ArgumentException("O tipo de evento informado não foi encontrado.", nameof(evento));
            }

            if (string.IsNullOrWhiteSpace(evento.Nome) || string.IsNullOrWhiteSpace(evento.Local))
            {
                throw new ArgumentException("Informe o nome e o local do evento.", nameof(evento));
            }

            if (evento.DataInicio == DateTime.MinValue ||
                evento.DataFim == DateTime.MinValue ||
                evento.DataFim < evento.DataInicio)
            {
                throw new ArgumentException("As datas do evento são inválidas.", nameof(evento));
            }

            evento.IdResponsavel = usuarioAtual.IdPessoa;
            evento.IdUsuarioCriacao = usuarioAtual.IdUsuario;
            evento.CriadoEm = DateTime.Now;
            evento.Situacao = SituacoesEvento.Planejado;

            return eventoRepository.Cadastrar(evento);
        }

        public bool Atualizar(Usuario usuario, Evento evento)
        {
            if (evento == null)
            {
                throw new ArgumentNullException(nameof(evento));
            }

            if (evento.IdEvento <= 0)
            {
                throw new ArgumentException("O evento informado é inválido.", nameof(evento));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            Evento eventoExistente = eventoRepository.BuscarPorId(evento.IdEvento);

            if (eventoExistente == null)
            {
                throw new InvalidOperationException("O evento não foi encontrado.");
            }

            if (evento.Versao != eventoExistente.Versao)
            {
                throw new InvalidOperationException(
                    "O evento foi alterado desde que a tela foi aberta. Atualize a lista antes de editar.");
            }

            if (!string.Equals(nomePerfil, "Gerente", StringComparison.OrdinalIgnoreCase) ||
                eventoExistente.IdResponsavel != usuarioAtual.IdPessoa)
            {
                throw new UnauthorizedAccessException(
                    "Somente o gerente responsável pode editar este evento.");
            }

            if (string.IsNullOrWhiteSpace(evento.Nome) ||
                string.IsNullOrWhiteSpace(evento.Local))
            {
                throw new ArgumentException("Informe o nome e o local do evento.", nameof(evento));
            }

            if (evento.IdTipoEvento <= 0 ||
                tipoEventoRepository.BuscarPorId(evento.IdTipoEvento) == null)
            {
                throw new ArgumentException("Selecione um tipo de evento válido.", nameof(evento));
            }

            if (evento.DataInicio == DateTime.MinValue ||
                evento.DataFim == DateTime.MinValue ||
                evento.DataFim < evento.DataInicio)
            {
                throw new ArgumentException("As datas do evento são inválidas.", nameof(evento));
            }

            // A edição do formulário não altera instituição, responsável ou situação.
            evento.IdInstituicao = eventoExistente.IdInstituicao;
            evento.IdResponsavel = eventoExistente.IdResponsavel;
            evento.Situacao = eventoExistente.Situacao;
            evento.IdUsuarioCriacao = eventoExistente.IdUsuarioCriacao;
            evento.CriadoEm = eventoExistente.CriadoEm;
            evento.Nome = evento.Nome.Trim();
            evento.Local = evento.Local.Trim();

            bool atualizado = eventoRepository.Atualizar(evento);

            if (!atualizado)
            {
                throw new InvalidOperationException(
                    "O evento foi alterado por outra operação. Atualize a lista e tente novamente.");
            }

            return true;
        }

        public List<Evento> ListarPorInstituicao(Usuario usuario, int idInstituicao)
        {
            if (idInstituicao <= 0)
            {
                throw new ArgumentException(
                    "O identificador da instituição deve ser maior que zero.",
                    nameof(idInstituicao));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            List<Evento> eventos = eventoRepository.ListarPorInstituicao(idInstituicao);
            List<Evento> eventosPermitidos = new List<Evento>();

            foreach (Evento evento in eventos)
            {
                if (autorizacaoService.PodeAcessarEvento(usuarioAtual, nomePerfil, evento))
                {
                    eventosPermitidos.Add(evento);
                }
            }

            return eventosPermitidos;
        }

        public Evento BuscarPorId(Usuario usuario, int idEvento)
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
            Evento evento = eventoRepository.BuscarPorId(idEvento);

            if (evento != null && !autorizacaoService.PodeAcessarEvento(
                usuarioAtual,
                nomePerfil,
                evento))
            {
                throw new UnauthorizedAccessException("Você não pode acessar este evento.");
            }

            return evento;
        }

        public void AlterarSituacao(Usuario usuario, int idEvento, string novaSituacao)
        {
            if (idEvento <= 0)
            {
                throw new ArgumentException("O identificador do evento deve ser válido.", nameof(idEvento));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            Evento evento = eventoRepository.BuscarPorId(idEvento);

            if (evento == null)
            {
                throw new InvalidOperationException("O evento não foi encontrado.");
            }

            if (!string.Equals(nomePerfil, "Gerente", StringComparison.OrdinalIgnoreCase) ||
                !autorizacaoService.PodeAcessarEvento(usuarioAtual, nomePerfil, evento))
            {
                throw new UnauthorizedAccessException(
                    "Somente o gerente responsável pode alterar a situação deste evento.");
            }

            novaSituacao = SituacoesEvento.Normalizar(novaSituacao);
            SituacoesEvento.ValidarTransicao(evento.Situacao, novaSituacao);

            if (novaSituacao == SituacoesEvento.Finalizado || novaSituacao == SituacoesEvento.Cancelado)
            {
                VerificarSaldosZerados(evento.IdEvento);
            }

            eventoRepository.AtualizarSituacao(
                evento.IdEvento,
                evento.Versao,
                novaSituacao,
                usuarioAtual.IdUsuario,
                evento.Situacao);
        }

        private void VerificarSaldosZerados(int idEvento)
        {
            List<LocalEstoque> locais = localEstoqueRepository.ListarPorEvento(idEvento);

            foreach (LocalEstoque local in locais)
            {
                List<Estoque> saldos = estoqueRepository.ListarPorLocal(local.IdLocalEstoque);

                foreach (Estoque estoque in saldos)
                {
                    if (estoque.QuantidadeAtual != 0)
                    {
                        throw new InvalidOperationException(
                            "O evento não pode ser finalizado enquanto houver produtos nos locais das barracas.");
                    }
                }
            }
        }
    }
}
