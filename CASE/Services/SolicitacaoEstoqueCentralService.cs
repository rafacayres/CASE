using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class SolicitacaoEstoqueCentralService
    {
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();
        private readonly EventoRepository eventoRepository = new EventoRepository();
        private readonly LocalEstoqueService localEstoqueService = new LocalEstoqueService();
        private readonly ProdutoRepository produtoRepository = new ProdutoRepository();
        private readonly SolicitacaoEstoqueCentralRepository solicitacaoRepository =
            new SolicitacaoEstoqueCentralRepository();
        private readonly TransferenciaEstoqueRepository transferenciaRepository =
            new TransferenciaEstoqueRepository();

        public int CriarLiberacao(
            Usuario usuario,
            int idEvento,
            IList<SolicitacaoEstoqueCentralItem> itens)
        {
            return Criar(
                usuario,
                idEvento,
                TiposSolicitacaoEstoqueCentral.Liberacao,
                itens);
        }

        public int CriarDevolucao(
            Usuario usuario,
            int idEvento,
            IList<SolicitacaoEstoqueCentralItem> itens)
        {
            return Criar(
                usuario,
                idEvento,
                TiposSolicitacaoEstoqueCentral.Devolucao,
                itens);
        }

        private int Criar(
            Usuario usuario,
            int idEvento,
            string tipo,
            IList<SolicitacaoEstoqueCentralItem> itens)
        {
            if (idEvento <= 0)
            {
                throw new ArgumentException(
                    "O evento informado é inválido.",
                    nameof(idEvento));
            }

            ValidarItens(itens);

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(
                nomePerfil,
                "Gerente",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException(
                    "Somente o gerente responsável pode solicitar produtos do estoque central.");
            }

            Evento evento = eventoRepository.BuscarPorId(idEvento);

            if (evento == null)
            {
                throw new InvalidOperationException("O evento não foi encontrado.");
            }

            if (evento.IdResponsavel != usuarioAtual.IdPessoa)
            {
                throw new UnauthorizedAccessException(
                    "Você não é responsável por este evento.");
            }

            if (!EventoPermiteOperacao(evento.Situacao))
            {
                throw new InvalidOperationException(
                    "O evento não está em uma situação que permita movimentações de estoque.");
            }

            LocalEstoque localCentral = localEstoqueService.ObterCentral(
                usuarioAtual,
                evento.IdInstituicao);

            if (!localCentral.Ativo || !localCentral.EhCentral ||
                localCentral.EhGeralEvento || localCentral.IdEvento.HasValue)
            {
                throw new InvalidOperationException(
                    "A instituição precisa possuir exatamente um estoque central ativo.");
            }

            LocalEstoque localGeral = localEstoqueService.ObterGeralDoEvento(
                usuarioAtual,
                evento.IdEvento);

            if (localGeral.EhCentral || !localGeral.EhGeralEvento ||
                localGeral.IdInstituicao != evento.IdInstituicao ||
                localGeral.IdEvento != evento.IdEvento)
            {
                throw new InvalidOperationException(
                    "O evento precisa possuir exatamente um estoque geral ativo.");
            }

            LocalEstoque localOrigem;
            LocalEstoque localDestino;

            if (tipo == TiposSolicitacaoEstoqueCentral.Liberacao)
            {
                localOrigem = localCentral;
                localDestino = localGeral;
            }
            else
            {
                if (tipo != TiposSolicitacaoEstoqueCentral.Devolucao)
                {
                    throw new ArgumentException("O tipo da solicitação não é válido.", nameof(tipo));
                }

                localOrigem = localGeral;
                localDestino = localCentral;
            }

            foreach (SolicitacaoEstoqueCentralItem item in itens)
            {
                if (produtoRepository.BuscarPorId(item.IdProduto) == null)
                {
                    throw new ArgumentException(
                        "Um dos produtos informados não existe.",
                        nameof(itens));
                }
            }

            SolicitacaoEstoqueCentral solicitacao =
                new SolicitacaoEstoqueCentral();
            solicitacao.Tipo = tipo;
            solicitacao.IdInstituicao = evento.IdInstituicao;
            solicitacao.IdEvento = evento.IdEvento;
            solicitacao.IdLocalOrigem = localOrigem.IdLocalEstoque;
            solicitacao.IdLocalDestino = localDestino.IdLocalEstoque;
            solicitacao.IdUsuarioSolicitante = usuarioAtual.IdUsuario;
            solicitacao.DataHora = DateTime.Now;
            solicitacao.Situacao = SituacoesSolicitacao.Pendente;
            solicitacao.Versao = 1;

            return solicitacaoRepository.CadastrarComItens(solicitacao, itens);
        }

        private static bool EventoPermiteOperacao(string situacao)
        {
            if (string.Equals(
                situacao,
                SituacoesEvento.Planejado,
                StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Equals(
                situacao,
                SituacoesEvento.EmAndamento,
                StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public List<SolicitacaoEstoqueCentral> ListarPendentesParaAdministrador(
            Usuario usuario)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            return solicitacaoRepository.ListarPendentes();
        }

        public List<SolicitacaoEstoqueCentral> ListarAprovadasSemAtendimento(
            Usuario usuario)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            return solicitacaoRepository.ListarAprovadasSemAtendimento();
        }

        public List<SolicitacaoEstoqueCentral> ListarDoGerente(Usuario usuario)
        {
            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(
                nomePerfil,
                "Gerente",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException(
                    "Somente gerentes podem consultar os próprios pedidos ao estoque central.");
            }

            return solicitacaoRepository.ListarPorSolicitante(usuarioAtual.IdUsuario);
        }

        public SolicitacaoEstoqueCentral BuscarPorId(
            Usuario usuario,
            int idSolicitacao)
        {
            if (idSolicitacao <= 0)
            {
                throw new ArgumentException(
                    "O identificador da solicitação é inválido.",
                    nameof(idSolicitacao));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            SolicitacaoEstoqueCentral solicitacao =
                solicitacaoRepository.BuscarPorId(idSolicitacao);

            if (solicitacao == null)
            {
                return null;
            }

            bool administrador = string.Equals(
                nomePerfil,
                "Administrador",
                StringComparison.OrdinalIgnoreCase);
            bool gerente = string.Equals(
                nomePerfil,
                "Gerente",
                StringComparison.OrdinalIgnoreCase);
            bool solicitante = solicitacao.IdUsuarioSolicitante == usuarioAtual.IdUsuario;

            if (!administrador && !(gerente && solicitante))
            {
                throw new UnauthorizedAccessException(
                    "Você não pode consultar esta solicitação.");
            }

            return solicitacao;
        }

        public List<SolicitacaoEstoqueCentralItem> ListarItens(
            Usuario usuario,
            int idSolicitacao)
        {
            SolicitacaoEstoqueCentral solicitacao = BuscarPorId(
                usuario,
                idSolicitacao);

            if (solicitacao == null)
            {
                throw new InvalidOperationException("A solicitação não foi encontrada.");
            }

            return solicitacaoRepository.ListarItens(idSolicitacao);
        }

        public int? BuscarIdTransferenciaAtendimento(
            Usuario usuario,
            int idSolicitacao)
        {
            SolicitacaoEstoqueCentral solicitacao = BuscarPorId(
                usuario,
                idSolicitacao);

            if (solicitacao == null)
            {
                throw new InvalidOperationException("A solicitação não foi encontrada.");
            }

            return solicitacaoRepository.BuscarIdTransferenciaAtendimento(
                idSolicitacao);
        }

        public void Aprovar(Usuario usuario, int idSolicitacao, uint versao)
        {
            Decidir(
                usuario,
                idSolicitacao,
                versao,
                SituacoesSolicitacao.Aprovada,
                null);
        }

        public void Rejeitar(
            Usuario usuario,
            int idSolicitacao,
            uint versao,
            string justificativa)
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

            Decidir(
                usuario,
                idSolicitacao,
                versao,
                SituacoesSolicitacao.Rejeitada,
                justificativa.Trim());
        }

        public int AtenderComTransferencia(Usuario usuario, int idSolicitacao)
        {
            if (idSolicitacao <= 0)
            {
                throw new ArgumentException(
                    "O identificador da solicitação é inválido.",
                    nameof(idSolicitacao));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(
                nomePerfil,
                "Administrador",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException(
                    "Somente um administrador pode liberar o estoque central.");
            }

            return transferenciaRepository.AtenderSolicitacaoEstoqueCentral(
                idSolicitacao,
                usuarioAtual.IdUsuario);
        }

        private void Decidir(
            Usuario usuario,
            int idSolicitacao,
            uint versao,
            string situacao,
            string justificativa)
        {
            if (idSolicitacao <= 0)
            {
                throw new ArgumentException(
                    "O identificador da solicitação é inválido.",
                    nameof(idSolicitacao));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(
                nomePerfil,
                "Administrador",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException(
                    "Somente um administrador pode decidir pedidos do estoque central.");
            }

            SolicitacaoEstoqueCentral solicitacao =
                solicitacaoRepository.BuscarPorId(idSolicitacao);

            if (solicitacao == null)
            {
                throw new InvalidOperationException("A solicitação não foi encontrada.");
            }

            if (solicitacao.IdUsuarioSolicitante == usuarioAtual.IdUsuario)
            {
                throw new UnauthorizedAccessException(
                    "O solicitante não pode decidir o próprio pedido.");
            }

            if (!string.Equals(
                solicitacao.Situacao,
                SituacoesSolicitacao.Pendente,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Somente pedidos pendentes podem ser decididos.");
            }

            solicitacaoRepository.RegistrarDecisao(
                idSolicitacao,
                versao,
                usuarioAtual.IdUsuario,
                situacao,
                justificativa);
        }

        private void ValidarItens(IList<SolicitacaoEstoqueCentralItem> itens)
        {
            if (itens == null || itens.Count == 0)
            {
                throw new ArgumentException(
                    "A solicitação precisa possuir pelo menos um produto.",
                    nameof(itens));
            }

            HashSet<int> produtosInformados = new HashSet<int>();

            foreach (SolicitacaoEstoqueCentralItem item in itens)
            {
                if (item == null)
                {
                    throw new ArgumentException(
                        "A solicitação contém um item vazio.",
                        nameof(itens));
                }

                if (item.IdProduto <= 0)
                {
                    throw new ArgumentException(
                        "Um dos produtos informados é inválido.",
                        nameof(itens));
                }

                if (item.Quantidade <= 0 ||
                    item.Quantidade != decimal.Truncate(item.Quantidade))
                {
                    throw new ArgumentException(
                        "As quantidades devem ser números inteiros maiores que zero.",
                        nameof(itens));
                }

                if (!produtosInformados.Add(item.IdProduto))
                {
                    throw new ArgumentException(
                        "Cada produto deve aparecer uma única vez na solicitação.",
                        nameof(itens));
                }
            }
        }
    }
}
