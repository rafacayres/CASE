using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class SolicitacaoService
    {
        private readonly SolicitacaoRepository solicitacaoRepository = new SolicitacaoRepository();
        private readonly SolicitacaoItemRepository solicitacaoItemRepository = new SolicitacaoItemRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();
        private readonly EventoRepository eventoRepository = new EventoRepository();
        private readonly EscalaRepository escalaRepository = new EscalaRepository();
        private readonly JornadaSetorRepository jornadaRepository = new JornadaSetorRepository();
        private readonly SetorRepository setorRepository = new SetorRepository();
        private readonly ProdutoSetorRepository produtoSetorRepository = new ProdutoSetorRepository();
        private readonly ProdutoRepository produtoRepository = new ProdutoRepository();
        private readonly UsuarioRepository usuarioRepository = new UsuarioRepository();
        private readonly PessoaRepository pessoaRepository = new PessoaRepository();

        public class ProdutoSolicitacaoOpcao
        {
            public int IdProdutoSetor { get; set; }
            public string Nome { get; set; }
        }

        public class ResumoSolicitacao
        {
            public string Solicitante { get; set; }
            public string Setor { get; set; }
            public string Produtos { get; set; }
        }

        public ResumoSolicitacao ObterResumo(Usuario usuario, int idSolicitacao)
        {
            Solicitacao solicitacao = BuscarPorId(usuario, idSolicitacao);

            if (solicitacao == null)
            {
                throw new InvalidOperationException("A solicitação não foi encontrada.");
            }

            ResumoSolicitacao resumo = new ResumoSolicitacao();
            resumo.Solicitante = "Conta sem identificação";

            if (solicitacao.IdUsuarioSolicitante.HasValue)
            {
                Usuario solicitante = usuarioRepository.BuscarPorId(
                    solicitacao.IdUsuarioSolicitante.Value);

                if (solicitante != null)
                {
                    Pessoa pessoa = pessoaRepository.BuscarPorId(solicitante.IdPessoa);

                    if (pessoa != null)
                    {
                        resumo.Solicitante = pessoa.Nome;
                    }
                }
            }

            Escala escala = escalaRepository.BuscarPorId(solicitacao.IdEscala);
            JornadaSetor jornada = jornadaRepository.BuscarPorId(escala.IdJornada);
            Setor setor = setorRepository.BuscarPorId(jornada.IdSetor);
            resumo.Setor = setor.Nome;

            List<SolicitacaoItem> itens = solicitacaoItemRepository.ListarPorSolicitacao(
                idSolicitacao);
            List<string> linhasProdutos = new List<string>();

            foreach (SolicitacaoItem item in itens)
            {
                ProdutoSetor produtoSetor = produtoSetorRepository.BuscarPorId(
                    item.IdProdutoSetor);
                string nomeProduto = "Produto " + item.IdProdutoSetor;

                if (produtoSetor != null)
                {
                    Produto produto = produtoRepository.BuscarPorId(
                        produtoSetor.IdProduto);

                    if (produto != null)
                    {
                        nomeProduto = produto.Nome;
                    }
                }

                linhasProdutos.Add(nomeProduto + " (" + item.Quantidade + ")");
            }

            resumo.Produtos = string.Join(", ", linhasProdutos);
            return resumo;
        }

        public List<Solicitacao> ListarMinhas(Usuario usuario)
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

            return solicitacaoRepository.ListarPorSolicitante(usuarioAtual.IdUsuario);
        }

        public List<Solicitacao> ListarDoEvento(Usuario usuario, int idEvento)
        {
            if (idEvento <= 0)
            {
                throw new ArgumentException("Informe um evento válido.", nameof(idEvento));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            Evento evento = eventoRepository.BuscarPorId(idEvento);

            if (!string.Equals(nomePerfil, "Gerente", StringComparison.OrdinalIgnoreCase) ||
                evento == null || evento.IdResponsavel != usuarioAtual.IdPessoa)
            {
                throw new UnauthorizedAccessException(
                    "Somente o gerente responsável pode consultar solicitações do evento.");
            }

            return solicitacaoRepository.ListarPorEvento(idEvento);
        }

        public List<ProdutoSolicitacaoOpcao> ListarProdutosDaEscala(
            Usuario usuario,
            int idEscala)
        {
            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            Escala escala = escalaRepository.BuscarPorId(idEscala);

            if (!string.Equals(nomePerfil, "Membro", StringComparison.OrdinalIgnoreCase) ||
                escala == null || !autorizacaoService.PodeAcessarEscala(
                    usuarioAtual,
                    nomePerfil,
                    escala))
            {
                throw new UnauthorizedAccessException(
                    "Somente o membro vinculado à escala pode consultar seus produtos.");
            }

            JornadaSetor jornada = jornadaRepository.BuscarPorId(escala.IdJornada);

            if (jornada == null)
            {
                throw new InvalidOperationException("A jornada da escala não foi encontrada.");
            }

            List<ProdutoSetor> produtosSetor = produtoSetorRepository.ListarPorSetor(
                jornada.IdSetor);
            List<ProdutoSolicitacaoOpcao> opcoes = new List<ProdutoSolicitacaoOpcao>();

            foreach (ProdutoSetor produtoSetor in produtosSetor)
            {
                Produto produto = produtoRepository.BuscarPorId(produtoSetor.IdProduto);

                if (produto == null || !produto.Ativo)
                {
                    continue;
                }

                ProdutoSolicitacaoOpcao opcao = new ProdutoSolicitacaoOpcao();
                opcao.IdProdutoSetor = produtoSetor.IdProdutoSetor;
                opcao.Nome = produto.Nome;
                opcoes.Add(opcao);
            }

            return opcoes;
        }

        public int Criar(
            Usuario usuario,
            Solicitacao solicitacao,
            IList<SolicitacaoItem> itens)
        {
            if (solicitacao == null)
            {
                throw new ArgumentNullException(nameof(solicitacao));
            }

            if (itens == null || itens.Count == 0)
            {
                throw new ArgumentException(
                    "A solicitação deve possuir pelo menos um item.",
                    nameof(itens));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(nomePerfil, "Membro", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException(
                    "Somente membros podem criar solicitações de produtos.");
            }

            Escala escala = escalaRepository.BuscarPorId(solicitacao.IdEscala);

            if (escala == null || !autorizacaoService.PodeAcessarEscala(
                usuarioAtual,
                nomePerfil,
                escala))
            {
                throw new UnauthorizedAccessException(
                    "Você não pode solicitar produtos para esta escala.");
            }

            JornadaSetor jornada = jornadaRepository.BuscarPorId(escala.IdJornada);

            if (jornada == null)
            {
                throw new InvalidOperationException("A jornada da escala não foi encontrada.");
            }

            Setor setor = setorRepository.BuscarPorId(jornada.IdSetor);

            if (setor == null)
            {
                throw new InvalidOperationException("O setor da escala não foi encontrado.");
            }

            foreach (SolicitacaoItem item in itens)
            {
                if (item == null)
                {
                    throw new ArgumentException("A solicitação contém um item nulo.", nameof(itens));
                }

                ValidarQuantidade(item.Quantidade);

                ProdutoSetor produtoSetor = produtoSetorRepository.BuscarPorId(item.IdProdutoSetor);

                if (produtoSetor == null || produtoSetor.IdSetor != setor.IdSetor)
                {
                    throw new ArgumentException(
                        "Todos os produtos solicitados devem pertencer ao setor da escala.",
                        nameof(itens));
                }
            }

            solicitacao.IdUsuarioSolicitante = usuarioAtual.IdUsuario;
            solicitacao.Situacao = SituacoesSolicitacao.Pendente;
            solicitacao.DataHora = DateTime.Now;
            solicitacao.CriadoEm = DateTime.Now;

            return solicitacaoRepository.CadastrarComItens(solicitacao, itens);
        }

        public List<Solicitacao> ListarPendentesDoEvento(Usuario usuario, int idEvento)
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

            if (!string.Equals(nomePerfil, "Gerente", StringComparison.OrdinalIgnoreCase) ||
                evento == null ||
                evento.IdResponsavel != usuarioAtual.IdPessoa)
            {
                throw new UnauthorizedAccessException(
                    "Somente o gerente responsável pode consultar as solicitações do evento.");
            }

            return solicitacaoRepository.ListarPendentesPorEvento(idEvento);
        }

        public void Aprovar(Usuario usuario, int idSolicitacao, string justificativa)
        {
            Decidir(usuario, idSolicitacao, SituacoesSolicitacao.Aprovada, justificativa);
        }

        public void Rejeitar(Usuario usuario, int idSolicitacao, string justificativa)
        {
            if (string.IsNullOrWhiteSpace(justificativa))
            {
                throw new ArgumentException(
                    "Informe o motivo da rejeição.",
                    nameof(justificativa));
            }

            if (justificativa.Trim().Length > 500)
            {
                throw new ArgumentException(
                    "O motivo da rejeição deve ter no máximo 500 caracteres.",
                    nameof(justificativa));
            }

            justificativa = justificativa.Trim();

            Decidir(usuario, idSolicitacao, SituacoesSolicitacao.Rejeitada, justificativa);
        }

        private void Decidir(
            Usuario usuario,
            int idSolicitacao,
            string novaSituacao,
            string justificativa)
        {
            if (idSolicitacao <= 0)
            {
                throw new ArgumentException(
                    "O identificador da solicitação deve ser válido.",
                    nameof(idSolicitacao));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            Solicitacao solicitacao = solicitacaoRepository.BuscarPorId(idSolicitacao);

            if (solicitacao == null)
            {
                throw new InvalidOperationException("A solicitação não foi encontrada.");
            }

            if (!autorizacaoService.PodeGerenciarSolicitacao(
                usuarioAtual,
                nomePerfil,
                solicitacao))
            {
                throw new UnauthorizedAccessException(
                    "Somente o gerente responsável pelo evento pode decidir esta solicitação.");
            }

            if (!string.Equals(
                solicitacao.Situacao,
                SituacoesSolicitacao.Pendente,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Somente solicitações pendentes podem ser aprovadas ou rejeitadas.");
            }

            solicitacaoRepository.RegistrarDecisao(
                solicitacao.IdSolicitacao,
                solicitacao.Versao,
                usuarioAtual.IdUsuario,
                novaSituacao,
                justificativa);
        }

        private static void ValidarQuantidade(decimal quantidade)
        {
            if (quantidade <= 0 || quantidade != decimal.Truncate(quantidade))
            {
                throw new ArgumentException(
                    "A quantidade solicitada deve ser um número inteiro maior que zero.");
            }
        }

        public List<Solicitacao> ListarPorEscala(Usuario usuario, int idEscala)
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
            EscalaRepository escalaRepository = new EscalaRepository();
            Escala escala = escalaRepository.BuscarPorId(idEscala);

            if (escala == null)
            {
                throw new InvalidOperationException("A escala não foi encontrada.");
            }

            if (!autorizacaoService.PodeAcessarEscala(usuarioAtual, nomePerfil, escala))
            {
                throw new UnauthorizedAccessException("Você não pode consultar solicitações desta escala.");
            }

            return solicitacaoRepository.ListarPorEscala(idEscala);
        }

        public Solicitacao BuscarPorId(Usuario usuario, int idSolicitacao)
        {
            if (idSolicitacao <= 0)
            {
                throw new ArgumentException(
                    "O identificador da solicitação deve ser maior que zero.",
                    nameof(idSolicitacao));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            Solicitacao solicitacao = solicitacaoRepository.BuscarPorId(idSolicitacao);

            if (solicitacao != null && !autorizacaoService.PodeAcessarSolicitacao(
                usuarioAtual,
                nomePerfil,
                solicitacao))
            {
                throw new UnauthorizedAccessException("Você não pode acessar esta solicitação.");
            }

            return solicitacao;
        }

        public List<SolicitacaoItem> ListarItens(Usuario usuario, int idSolicitacao)
        {
            if (idSolicitacao <= 0)
            {
                throw new ArgumentException(
                    "O identificador da solicitação deve ser maior que zero.",
                    nameof(idSolicitacao));
            }

            Solicitacao solicitacao = BuscarPorId(usuario, idSolicitacao);

            if (solicitacao == null)
            {
                throw new InvalidOperationException("A solicitação não foi encontrada.");
            }

            return solicitacaoItemRepository.ListarPorSolicitacao(idSolicitacao);
        }
    }
}
