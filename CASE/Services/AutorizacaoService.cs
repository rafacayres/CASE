using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class AutorizacaoService
    {
        private const string PerfilAdministrador = "Administrador";
        private const string PerfilGerente = "Gerente";
        private const string PerfilMembro = "Membro";
        private readonly UsuarioRepository usuarioRepository = new UsuarioRepository();
        private readonly PerfilSistemaRepository perfilSistemaRepository = new PerfilSistemaRepository();
        private readonly EventoRepository eventoRepository = new EventoRepository();
        private readonly SetorRepository setorRepository = new SetorRepository();
        private readonly EquipeEventoRepository equipeEventoRepository = new EquipeEventoRepository();
        private readonly JornadaSetorRepository jornadaSetorRepository = new JornadaSetorRepository();
        private readonly EscalaRepository escalaRepository = new EscalaRepository();
        private readonly LocalEstoqueRepository localEstoqueRepository = new LocalEstoqueRepository();
        private readonly EstoqueSetorRepository estoqueSetorRepository = new EstoqueSetorRepository();

        internal Usuario ValidarUsuarioEObterPerfil(Usuario usuarioDaSessao, out string nomePerfil)
        {
            SessaoUsuario.ExigirUsuario(usuarioDaSessao);

            if (usuarioDaSessao.IdUsuario <= 0)
            {
                throw new UnauthorizedAccessException("A conta autenticada é inválida.");
            }

            Usuario usuarioBanco = usuarioRepository.BuscarPorId(usuarioDaSessao.IdUsuario);

            if (usuarioBanco == null)
            {
                throw new UnauthorizedAccessException("A conta autenticada não existe mais.");
            }

            if (usuarioBanco.IdPessoa != usuarioDaSessao.IdPessoa ||
                usuarioBanco.IdPerfilSistema != usuarioDaSessao.IdPerfilSistema)
            {
                throw new UnauthorizedAccessException("Os dados da sessão não correspondem à conta atual.");
            }

            if (!string.Equals(
                usuarioBanco.SituacaoConta,
                SituacoesUsuario.Aprovado,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("A conta não está aprovada para acessar o sistema.");
            }

            if (!usuarioBanco.IdPerfilSistema.HasValue)
            {
                throw new UnauthorizedAccessException("A conta não possui um perfil de sistema.");
            }

            PerfilSistema perfil = perfilSistemaRepository.BuscarPorId(
                usuarioBanco.IdPerfilSistema.Value);

            if (perfil == null || string.IsNullOrWhiteSpace(perfil.Nome))
            {
                throw new UnauthorizedAccessException("O perfil da conta não foi encontrado.");
            }

            nomePerfil = perfil.Nome.Trim();
            if (PerfilEh(nomePerfil, PerfilAdministrador))
            {
                nomePerfil = PerfilAdministrador;
            }
            else if (PerfilEh(nomePerfil, PerfilGerente))
            {
                nomePerfil = PerfilGerente;
            }
            else if (PerfilEh(nomePerfil, PerfilMembro))
            {
                nomePerfil = PerfilMembro;
            }
            else
            {
                throw new UnauthorizedAccessException("O perfil da conta não é reconhecido pelo sistema.");
            }
            return usuarioBanco;
        }

        internal void ExigirAdministrador(Usuario usuarioDaSessao)
        {
            string nomePerfil;
            ValidarUsuarioEObterPerfil(usuarioDaSessao, out nomePerfil);

            if (!PerfilEh(nomePerfil, PerfilAdministrador))
            {
                throw new UnauthorizedAccessException(
                    "Esta operação é permitida somente para administradores.");
            }
        }

        internal void ExigirAdministradorOuGerente(Usuario usuarioDaSessao)
        {
            string nomePerfil;
            ValidarUsuarioEObterPerfil(usuarioDaSessao, out nomePerfil);

            if (!PerfilEh(nomePerfil, PerfilAdministrador) &&
                !PerfilEh(nomePerfil, PerfilGerente))
            {
                throw new UnauthorizedAccessException(
                    "Esta operação é permitida somente para administradores e gerentes.");
            }
        }

        internal bool PodeAcessarEvento(Usuario usuario, string nomePerfil, Evento evento)
        {
            if (evento == null)
            {
                return false;
            }

            if (PerfilEh(nomePerfil, PerfilAdministrador))
            {
                return true;
            }

            if (PerfilEh(nomePerfil, PerfilGerente))
            {
                return evento.IdResponsavel == usuario.IdPessoa;
            }

            if (PerfilEh(nomePerfil, PerfilMembro))
            {
                return MembroEstaVinculadoAoEvento(usuario, evento.IdEvento);
            }

            return false;
        }

        internal bool PodeAcessarSetor(Usuario usuario, string nomePerfil, Setor setor)
        {
            if (setor == null)
            {
                return false;
            }

            if (PerfilEh(nomePerfil, PerfilAdministrador))
            {
                return true;
            }

            Evento evento = eventoRepository.BuscarPorId(setor.IdEvento);

            if (evento == null)
            {
                return false;
            }

            if (PerfilEh(nomePerfil, PerfilGerente))
            {
                return evento.IdResponsavel == usuario.IdPessoa;
            }

            if (PerfilEh(nomePerfil, PerfilMembro))
            {
                return MembroEstaVinculadoAoSetor(usuario, setor);
            }

            return false;
        }

        internal bool PodeAcessarEquipe(Usuario usuario, string nomePerfil, EquipeEvento equipe)
        {
            if (equipe == null)
            {
                return false;
            }

            if (PerfilEh(nomePerfil, PerfilAdministrador))
            {
                return true;
            }

            Evento evento = eventoRepository.BuscarPorId(equipe.IdEvento);

            if (evento == null)
            {
                return false;
            }

            if (PerfilEh(nomePerfil, PerfilGerente))
            {
                return evento.IdResponsavel == usuario.IdPessoa;
            }

            if (PerfilEh(nomePerfil, PerfilMembro))
            {
                return equipe.Ativo && equipe.IdPessoa == usuario.IdPessoa;
            }

            return false;
        }

        internal bool PodeAcessarJornada(Usuario usuario, string nomePerfil, JornadaSetor jornada)
        {
            if (jornada == null)
            {
                return false;
            }

            Setor setor = setorRepository.BuscarPorId(jornada.IdSetor);

            if (PerfilEh(nomePerfil, PerfilMembro))
            {
                return MembroEstaVinculadoAJornada(usuario, jornada, setor);
            }

            return PodeAcessarSetor(usuario, nomePerfil, setor);
        }

        internal bool PodeAcessarEscala(Usuario usuario, string nomePerfil, Escala escala)
        {
            if (escala == null)
            {
                return false;
            }

            EquipeEvento equipe = equipeEventoRepository.BuscarPorId(escala.IdEquipe);
            JornadaSetor jornada = jornadaSetorRepository.BuscarPorId(escala.IdJornada);

            if (equipe == null || jornada == null)
            {
                return false;
            }

            Setor setor = setorRepository.BuscarPorId(jornada.IdSetor);

            if (setor == null || equipe.IdEvento != setor.IdEvento)
            {
                return false;
            }

            if (PerfilEh(nomePerfil, PerfilAdministrador))
            {
                return true;
            }

            Evento evento = eventoRepository.BuscarPorId(equipe.IdEvento);

            if (evento == null)
            {
                return false;
            }

            if (PerfilEh(nomePerfil, PerfilGerente))
            {
                return evento.IdResponsavel == usuario.IdPessoa;
            }

            if (PerfilEh(nomePerfil, PerfilMembro))
            {
                return equipe.Ativo && equipe.IdPessoa == usuario.IdPessoa;
            }

            return false;
        }

        internal bool PodeAcessarLocalEstoque(Usuario usuario, string nomePerfil, LocalEstoque local)
        {
            if (local == null)
            {
                return false;
            }

            if (PerfilEh(nomePerfil, PerfilAdministrador))
            {
                return true;
            }

            if (local.EhCentral)
            {
                if (PerfilEh(nomePerfil, PerfilGerente))
                {
                    foreach (Evento eventoGerenciado in eventoRepository.ListarPorResponsavel(usuario.IdPessoa))
                    {
                        if (eventoGerenciado.IdInstituicao == local.IdInstituicao)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            if (!local.IdEvento.HasValue)
            {
                return false;
            }

            Evento evento = eventoRepository.BuscarPorId(local.IdEvento.Value);

            if (evento == null || evento.IdInstituicao != local.IdInstituicao)
            {
                return false;
            }

            if (PerfilEh(nomePerfil, PerfilGerente))
            {
                return evento.IdResponsavel == usuario.IdPessoa;
            }

            if (PerfilEh(nomePerfil, PerfilMembro))
            {
                List<EstoqueSetor> setoresAssociados = estoqueSetorRepository.Listar();

                foreach (EstoqueSetor associacao in setoresAssociados)
                {
                    if (associacao.IdLocalEstoque != local.IdLocalEstoque ||
                        associacao.IdEvento != local.IdEvento.Value)
                    {
                        continue;
                    }

                    Setor setor = setorRepository.BuscarPorId(associacao.IdSetor);

                    if (setor != null && setor.IdEvento == local.IdEvento.Value &&
                        MembroEstaVinculadoAoSetor(usuario, setor))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        internal bool PodeAcessarEntrada(
            Usuario usuario,
            string nomePerfil,
            EntradaMercadoria entrada)
        {
            if (entrada == null)
            {
                return false;
            }

            if (!PerfilEh(nomePerfil, PerfilAdministrador) && !PerfilEh(nomePerfil, PerfilGerente))
            {
                return false;
            }

            LocalEstoque local = localEstoqueRepository.BuscarPorId(entrada.IdLocalDestino);

            if (local != null && local.EhCentral)
            {
                return PerfilEh(nomePerfil, PerfilAdministrador);
            }

            return PodeAcessarLocalEstoque(usuario, nomePerfil, local);
        }

        internal bool PodeAcessarTransferencia(
            Usuario usuario,
            string nomePerfil,
            TransferenciaEstoque transferencia)
        {
            if (transferencia == null)
            {
                return false;
            }

            if (PerfilEh(nomePerfil, PerfilAdministrador))
            {
                return true;
            }

            if (!PerfilEh(nomePerfil, PerfilGerente))
            {
                return false;
            }

            LocalEstoque localOrigem = localEstoqueRepository.BuscarPorId(transferencia.IdLocalOrigem);
            LocalEstoque localDestino = localEstoqueRepository.BuscarPorId(transferencia.IdLocalDestino);

            if (localOrigem == null || localDestino == null ||
                localOrigem.EhCentral || localDestino.EhCentral ||
                !localOrigem.IdEvento.HasValue || !localDestino.IdEvento.HasValue ||
                localOrigem.IdEvento.Value != localDestino.IdEvento.Value ||
                localOrigem.IdInstituicao != localDestino.IdInstituicao)
            {
                return false;
            }

            return PodeAcessarLocalEstoque(usuario, nomePerfil, localOrigem);
        }

        internal bool PodeAcessarSolicitacao(
            Usuario usuario,
            string nomePerfil,
            Solicitacao solicitacao)
        {
            if (solicitacao == null)
            {
                return false;
            }

            Escala escala = escalaRepository.BuscarPorId(solicitacao.IdEscala);
            return PodeAcessarEscala(usuario, nomePerfil, escala);
        }

        internal bool PodeGerenciarSolicitacao(
            Usuario usuario,
            string nomePerfil,
            Solicitacao solicitacao)
        {
            if (solicitacao == null || !PerfilEh(nomePerfil, PerfilGerente))
            {
                return false;
            }

            Escala escala = escalaRepository.BuscarPorId(solicitacao.IdEscala);

            if (escala == null)
            {
                return false;
            }

            EquipeEvento equipe = equipeEventoRepository.BuscarPorId(escala.IdEquipe);
            JornadaSetor jornada = jornadaSetorRepository.BuscarPorId(escala.IdJornada);

            if (equipe == null || jornada == null)
            {
                return false;
            }

            Setor setor = setorRepository.BuscarPorId(jornada.IdSetor);

            if (setor == null || setor.IdEvento != equipe.IdEvento)
            {
                return false;
            }

            Evento evento = eventoRepository.BuscarPorId(setor.IdEvento);
            return evento != null && evento.IdResponsavel == usuario.IdPessoa;
        }

        internal bool PodeAcessarRegistro(Usuario usuario, string nomePerfil, Registro registro)
        {
            if (registro == null)
            {
                return false;
            }

            Escala escala = escalaRepository.BuscarPorId(registro.IdEscala);

            if (!PodeAcessarEscala(usuario, nomePerfil, escala))
            {
                return false;
            }

            if (PerfilEh(nomePerfil, PerfilMembro))
            {
                return registro.IdUsuarioResponsavel == usuario.IdUsuario;
            }

            return PerfilEh(nomePerfil, PerfilAdministrador) || PerfilEh(nomePerfil, PerfilGerente);
        }

        internal bool PodeAcessarDespesa(Usuario usuario, string nomePerfil, Despesa despesa)
        {
            if (despesa == null)
            {
                return false;
            }

            if (PerfilEh(nomePerfil, PerfilAdministrador))
            {
                return true;
            }

            if (!PerfilEh(nomePerfil, PerfilGerente))
            {
                return false;
            }

            Evento evento = eventoRepository.BuscarPorId(despesa.IdEvento);

            if (evento == null)
            {
                return false;
            }

            return evento.IdResponsavel == usuario.IdPessoa;
        }

        internal bool PodeAcessarApuracao(Usuario usuario, string nomePerfil, Apuracao apuracao)
        {
            if (apuracao == null)
            {
                return false;
            }

            if (PerfilEh(nomePerfil, PerfilAdministrador))
            {
                return true;
            }

            if (!PerfilEh(nomePerfil, PerfilGerente))
            {
                return false;
            }

            JornadaSetor jornada = jornadaSetorRepository.BuscarPorId(apuracao.IdJornada);
            return PodeAcessarJornada(usuario, nomePerfil, jornada);
        }

        private bool MembroEstaVinculadoAoEvento(Usuario usuario, int idEvento)
        {
            List<EquipeEvento> equipe = equipeEventoRepository.ListarPorEvento(idEvento);

            foreach (EquipeEvento membro in equipe)
            {
                if (membro.Ativo && membro.IdPessoa == usuario.IdPessoa)
                {
                    return true;
                }
            }

            return false;
        }

        private bool MembroEstaVinculadoAoSetor(Usuario usuario, Setor setor)
        {
            List<EquipeEvento> equipe = equipeEventoRepository.ListarPorEvento(setor.IdEvento);

            foreach (EquipeEvento membro in equipe)
            {
                if (!membro.Ativo || membro.IdPessoa != usuario.IdPessoa)
                {
                    continue;
                }

                List<Escala> escalas = escalaRepository.ListarPorEquipe(membro.IdEquipe);

                foreach (Escala escala in escalas)
                {
                    JornadaSetor jornada = jornadaSetorRepository.BuscarPorId(escala.IdJornada);

                    if (jornada != null && jornada.IdSetor == setor.IdSetor)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool MembroEstaVinculadoAJornada(
            Usuario usuario,
            JornadaSetor jornada,
            Setor setor)
        {
            if (setor == null)
            {
                return false;
            }

            List<EquipeEvento> equipe = equipeEventoRepository.ListarPorEvento(setor.IdEvento);

            foreach (EquipeEvento membro in equipe)
            {
                if (!membro.Ativo || membro.IdPessoa != usuario.IdPessoa)
                {
                    continue;
                }

                List<Escala> escalas = escalaRepository.ListarPorEquipe(membro.IdEquipe);

                foreach (Escala escala in escalas)
                {
                    if (escala.IdJornada == jornada.IdJornada)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool PerfilEh(string perfilAtual, string perfilEsperado)
        {
            return string.Equals(
                perfilAtual,
                perfilEsperado,
                StringComparison.OrdinalIgnoreCase);
        }
    }
}
