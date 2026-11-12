using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class EscalaService
    {
        private readonly EscalaRepository escalaRepository = new EscalaRepository();
        private readonly JornadaSetorRepository jornadaSetorRepository = new JornadaSetorRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();
        private readonly EquipeEventoRepository equipeRepository = new EquipeEventoRepository();
        private readonly SetorRepository setorRepository = new SetorRepository();
        private readonly EventoRepository eventoRepository = new EventoRepository();

        public List<Escala> ListarMinhas(Usuario usuario)
        {
            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(nomePerfil, "Membro", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException(
                    "Esta consulta está disponível para membros autenticados.");
            }

            EquipeEventoRepository equipePessoaRepository = new EquipeEventoRepository();
            List<EquipeEvento> equipes = equipePessoaRepository.ListarPorPessoa(
                usuarioAtual.IdPessoa);
            List<Escala> minhasEscalas = new List<Escala>();

            foreach (EquipeEvento equipe in equipes)
            {
                List<Escala> escalasDaEquipe = escalaRepository.ListarPorEquipe(equipe.IdEquipe);

                foreach (Escala escala in escalasDaEquipe)
                {
                    if (autorizacaoService.PodeAcessarEscala(
                        usuarioAtual,
                        nomePerfil,
                        escala))
                    {
                        minhasEscalas.Add(escala);
                    }
                }
            }

            return minhasEscalas;
        }

        public List<Escala> ListarPorEvento(Usuario usuario, int idEvento)
        {
            Evento evento = eventoRepository.BuscarPorId(idEvento);

            if (evento == null)
            {
                throw new InvalidOperationException("O evento não foi encontrado.");
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!autorizacaoService.PodeAcessarEvento(usuarioAtual, nomePerfil, evento))
            {
                throw new UnauthorizedAccessException("Você não pode consultar as escalas deste evento.");
            }

            List<Escala> escalas = escalaRepository.ListarPorEvento(idEvento);
            List<Escala> escalasPermitidas = new List<Escala>();

            foreach (Escala escala in escalas)
            {
                if (autorizacaoService.PodeAcessarEscala(usuarioAtual, nomePerfil, escala))
                {
                    escalasPermitidas.Add(escala);
                }
            }

            return escalasPermitidas;
        }

        public int Criar(Usuario usuario, Escala escala)
        {
            if (escala == null)
            {
                throw new ArgumentNullException(nameof(escala));
            }

            ValidarGerenteResponsavel(
                usuario,
                escala.IdEquipe,
                escala.IdJornada,
                out EquipeEvento equipe,
                out _);

            ValidarHorario(escala);
            escala.Situacao = SituacoesEscala.Pendente;

            return escalaRepository.SalvarValidandoConflito(
                escala,
                equipe.IdPessoa,
                null);
        }

        public void Atualizar(Usuario usuario, Escala escala)
        {
            if (escala == null || escala.IdEscala <= 0)
            {
                throw new ArgumentException("A escala informada é inválida.", nameof(escala));
            }

            Escala atual = escalaRepository.BuscarPorId(escala.IdEscala);

            if (atual == null)
            {
                throw new InvalidOperationException("A escala não foi encontrada.");
            }

            if (string.Equals(atual.Situacao, SituacoesEscala.Cancelada,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("Uma escala cancelada não pode ser reaberta ou editada.");
            }

            ValidarGerenteResponsavel(
                usuario,
                atual.IdEquipe,
                atual.IdJornada,
                out _,
                out JornadaSetor jornadaAnterior);

            ValidarGerenteResponsavel(
                usuario,
                escala.IdEquipe,
                escala.IdJornada,
                out EquipeEvento novaEquipe,
                out JornadaSetor novaJornada);

            Setor setorAnterior = setorRepository.BuscarPorId(jornadaAnterior.IdSetor);
            Setor novoSetor = setorRepository.BuscarPorId(novaJornada.IdSetor);

            if (setorAnterior.IdEvento != novoSetor.IdEvento)
            {
                throw new InvalidOperationException("Uma escala não pode ser transferida para outro evento.");
            }

            ValidarHorario(escala);
            escala.Situacao = atual.Situacao;

            escalaRepository.SalvarValidandoConflito(
                escala,
                novaEquipe.IdPessoa,
                escala.IdEscala);
        }

        private Usuario ValidarGerenteResponsavel(
            Usuario usuario,
            int idEquipe,
            int idJornada,
            out EquipeEvento equipe,
            out JornadaSetor jornada)
        {
            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(nomePerfil, "Gerente", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Somente gerentes podem administrar escalas.");
            }

            equipe = equipeRepository.BuscarPorId(idEquipe);
            jornada = jornadaSetorRepository.BuscarPorId(idJornada);

            if (equipe == null || jornada == null || !equipe.Ativo)
            {
                throw new InvalidOperationException("A equipe ou jornada informada não está disponível.");
            }

            Setor setor = setorRepository.BuscarPorId(jornada.IdSetor);

            if (setor == null || equipe.IdEvento != setor.IdEvento)
            {
                throw new InvalidOperationException("A equipe e a jornada precisam pertencer ao mesmo evento.");
            }

            Evento evento = eventoRepository.BuscarPorId(equipe.IdEvento);

            if (evento == null || evento.IdResponsavel != usuarioAtual.IdPessoa)
            {
                throw new UnauthorizedAccessException("Você não pode administrar escalas deste evento.");
            }
            if (jornada.Data.Date < evento.DataInicio.Date || jornada.Data.Date > evento.DataFim.Date)
            {
                throw new ArgumentException("A jornada da escala está fora do período do evento.");
            }

            if (!string.Equals(evento.Situacao, SituacoesEvento.Planejado,
                    StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(evento.Situacao, SituacoesEvento.EmAndamento,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("O evento não permite alterações de escala nesta situação.");
            }

            return usuarioAtual;
        }

        private static void ValidarHorario(Escala escala)
        {
            if (escala.EntradaPrevista < TimeSpan.Zero ||
                escala.SaidaPrevista <= escala.EntradaPrevista ||
                escala.SaidaPrevista >= TimeSpan.FromDays(1))
            {
                throw new ArgumentException("O horário de saída deve ser posterior ao horário de entrada.");
            }
        }

        public List<Escala> ListarPorJornada(Usuario usuario, int idJornada)
        {
            if (idJornada <= 0)
            {
                throw new ArgumentException(
                    "O identificador da jornada deve ser maior que zero.",
                    nameof(idJornada));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            JornadaSetor jornada = jornadaSetorRepository.BuscarPorId(idJornada);

            if (jornada == null)
            {
                throw new InvalidOperationException("A jornada informada não foi encontrada.");
            }

            if (!autorizacaoService.PodeAcessarJornada(usuarioAtual, nomePerfil, jornada))
            {
                throw new UnauthorizedAccessException("Você não pode acessar esta jornada.");
            }

            List<Escala> escalas = escalaRepository.ListarPorJornada(idJornada);
            List<Escala> escalasPermitidas = new List<Escala>();

            foreach (Escala escala in escalas)
            {
                if (autorizacaoService.PodeAcessarEscala(usuarioAtual, nomePerfil, escala))
                {
                    escalasPermitidas.Add(escala);
                }
            }

            return escalasPermitidas;
        }

        public List<Escala> ListarPorEquipe(Usuario usuario, int idEquipe)
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
            EquipeEventoRepository equipeRepository = new EquipeEventoRepository();
            EquipeEvento equipe = equipeRepository.BuscarPorId(idEquipe);

            if (equipe == null)
            {
                throw new InvalidOperationException("A equipe informada não foi encontrada.");
            }

            if (!autorizacaoService.PodeAcessarEquipe(usuarioAtual, nomePerfil, equipe))
            {
                throw new UnauthorizedAccessException("Você não pode acessar esta equipe.");
            }

            List<Escala> escalas = escalaRepository.ListarPorEquipe(idEquipe);
            List<Escala> escalasPermitidas = new List<Escala>();

            foreach (Escala escala in escalas)
            {
                if (autorizacaoService.PodeAcessarEscala(usuarioAtual, nomePerfil, escala))
                {
                    escalasPermitidas.Add(escala);
                }
            }

            return escalasPermitidas;
        }

        public Escala BuscarPorId(Usuario usuario, int idEscala)
        {
            if (idEscala <= 0)
            {
                throw new ArgumentException(
                    "O identificador da escala deve ser maior que zero.",
                    nameof(idEscala));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            Escala escala = escalaRepository.BuscarPorId(idEscala);

            if (escala != null && !autorizacaoService.PodeAcessarEscala(
                usuarioAtual,
                nomePerfil,
                escala))
            {
                throw new UnauthorizedAccessException("Você não pode acessar esta escala.");
            }

            return escala;
        }
    }
}
