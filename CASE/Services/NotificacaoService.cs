using System;
using System.Collections.Generic;
using CASE.Models;

namespace CASE.Services
{
    public class NotificacaoPendente
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public int IdReferencia { get; set; }
        public int IdEvento { get; set; }
    }

    public class NotificacaoService
    {
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();
        private readonly UsuarioService usuarioService = new UsuarioService();
        private readonly EventoService eventoService = new EventoService();
        private readonly SolicitacaoService solicitacaoService = new SolicitacaoService();
        private readonly SolicitacaoEstoqueCentralService solicitacaoCentralService =
            new SolicitacaoEstoqueCentralService();

        public List<NotificacaoPendente> ListarPendencias(Usuario usuario)
        {
            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            List<NotificacaoPendente> notificacoes = new List<NotificacaoPendente>();

            if (string.Equals(nomePerfil, "Administrador", StringComparison.OrdinalIgnoreCase))
            {
                AdicionarCadastrosPendentes(usuarioAtual, notificacoes);
                AdicionarSolicitacoesCentraisPendentes(usuarioAtual, notificacoes);
            }
            else if (string.Equals(nomePerfil, "Gerente", StringComparison.OrdinalIgnoreCase))
            {
                AdicionarSolicitacoesDosEventos(usuarioAtual, notificacoes);
            }

            return notificacoes;
        }

        private void AdicionarCadastrosPendentes(
            Usuario administrador,
            List<NotificacaoPendente> notificacoes)
        {
            List<Usuario> pendentes = usuarioService.ListarCadastrosPendentes(administrador);

            foreach (Usuario pendente in pendentes)
            {
                NotificacaoPendente notificacao = new NotificacaoPendente();
                notificacao.Titulo = "Cadastro aguardando aprovação";
                notificacao.Descricao = "Conta " + pendente.Login + " · ID " + pendente.IdUsuario;
                notificacao.Tipo = "cadastro_usuario";
                notificacao.IdReferencia = pendente.IdUsuario;
                notificacoes.Add(notificacao);
            }
        }

        private void AdicionarSolicitacoesCentraisPendentes(
            Usuario administrador,
            List<NotificacaoPendente> notificacoes)
        {
            List<SolicitacaoEstoqueCentral> pendentes =
                solicitacaoCentralService.ListarPendentesParaAdministrador(administrador);

            List<SolicitacaoEstoqueCentral> aprovadas =
                solicitacaoCentralService.ListarAprovadasSemAtendimento(administrador);

            foreach (SolicitacaoEstoqueCentral solicitacao in pendentes)
            {
                NotificacaoPendente notificacao = new NotificacaoPendente();
                notificacao.Titulo = "Pedido de estoque central pendente";
                notificacao.Descricao = solicitacao.Tipo + " · Evento " + solicitacao.IdEvento;
                notificacao.Tipo = "solicitacao_central";
                notificacao.IdReferencia = solicitacao.IdSolicitacaoEstoqueCentral;
                notificacao.IdEvento = solicitacao.IdEvento;
                notificacoes.Add(notificacao);
            }

            foreach (SolicitacaoEstoqueCentral solicitacao in aprovadas)
            {
                NotificacaoPendente notificacao = new NotificacaoPendente();
                notificacao.Titulo = "Pedido aprovado aguardando transferência";
                notificacao.Descricao = solicitacao.Tipo + " · Evento " + solicitacao.IdEvento;
                notificacao.Tipo = "solicitacao_central_aprovada";
                notificacao.IdReferencia = solicitacao.IdSolicitacaoEstoqueCentral;
                notificacao.IdEvento = solicitacao.IdEvento;
                notificacoes.Add(notificacao);
            }
        }

        private void AdicionarSolicitacoesDosEventos(
            Usuario gerente,
            List<NotificacaoPendente> notificacoes)
        {
            List<Evento> eventos = eventoService.Listar(gerente);

            foreach (Evento evento in eventos)
            {
                List<Solicitacao> pendentes = solicitacaoService.ListarPendentesDoEvento(
                    gerente,
                    evento.IdEvento);

                foreach (Solicitacao solicitacao in pendentes)
                {
                    NotificacaoPendente notificacao = new NotificacaoPendente();
                    notificacao.Titulo = "Reposição de membro pendente";
                    notificacao.Descricao = evento.Nome + " · Pedido " + solicitacao.IdSolicitacao;
                    notificacao.Tipo = "solicitacao_membro";
                    notificacao.IdReferencia = solicitacao.IdSolicitacao;
                    notificacao.IdEvento = evento.IdEvento;
                    notificacoes.Add(notificacao);
                }
            }
        }
    }
}
