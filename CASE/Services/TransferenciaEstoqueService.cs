using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class TransferenciaEstoqueService
    {
        private readonly TransferenciaEstoqueRepository transferenciaRepository = new TransferenciaEstoqueRepository();
        private readonly TransferenciaItemRepository transferenciaItemRepository = new TransferenciaItemRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();
        private readonly SolicitacaoRepository solicitacaoRepository = new SolicitacaoRepository();
        private readonly LocalEstoqueRepository localRepository = new LocalEstoqueRepository();
        private readonly SolicitacaoItemRepository solicitacaoItemRepository = new SolicitacaoItemRepository();
        private readonly EscalaRepository escalaRepository = new EscalaRepository();
        private readonly JornadaSetorRepository jornadaRepository = new JornadaSetorRepository();
        private readonly SetorRepository setorRepository = new SetorRepository();
        private readonly EstoqueSetorRepository estoqueSetorRepository = new EstoqueSetorRepository();

        public int AtenderSolicitacaoMembro(Usuario usuario, int idSolicitacao)
        {
            if (idSolicitacao <= 0)
            {
                throw new ArgumentException("Informe uma solicitação válida.", nameof(idSolicitacao));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            Solicitacao solicitacao = solicitacaoRepository.BuscarPorId(idSolicitacao);

            if (solicitacao == null || !autorizacaoService.PodeGerenciarSolicitacao(
                usuarioAtual,
                nomePerfil,
                solicitacao))
            {
                throw new UnauthorizedAccessException(
                    "Somente o gerente responsável pode atender esta solicitação.");
            }

            if (!string.Equals(
                solicitacao.Situacao,
                SituacoesSolicitacao.Aprovada,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "A solicitação precisa estar aprovada antes do atendimento.");
            }

            Escala escala = escalaRepository.BuscarPorId(solicitacao.IdEscala);

            if (escala == null)
            {
                throw new InvalidOperationException("A escala da solicitação não foi encontrada.");
            }

            JornadaSetor jornada = jornadaRepository.BuscarPorId(escala.IdJornada);

            if (jornada == null)
            {
                throw new InvalidOperationException("A jornada da solicitação não foi encontrada.");
            }

            Setor setor = setorRepository.BuscarPorId(jornada.IdSetor);

            if (setor == null)
            {
                throw new InvalidOperationException("O setor da solicitação não foi encontrado.");
            }

            List<LocalEstoque> locaisEvento = localRepository.ListarPorEvento(setor.IdEvento);
            LocalEstoque estoqueGeral = null;

            foreach (LocalEstoque local in locaisEvento)
            {
                if (local.EhGeralEvento && local.Ativo)
                {
                    if (estoqueGeral != null)
                    {
                        throw new InvalidOperationException(
                            "O evento possui mais de um estoque geral ativo.");
                    }

                    estoqueGeral = local;
                }
            }

            EstoqueSetor estoqueSetor = estoqueSetorRepository.BuscarPorSetor(setor.IdSetor);

            if (estoqueGeral == null || estoqueSetor == null ||
                estoqueSetor.IdEvento != setor.IdEvento)
            {
                throw new InvalidOperationException(
                    "Não foi possível localizar os estoques geral e da barraca desta solicitação.");
            }

            List<SolicitacaoItem> itensSolicitados = solicitacaoItemRepository.ListarPorSolicitacao(
                solicitacao.IdSolicitacao);
            List<TransferenciaItem> itensTransferencia = new List<TransferenciaItem>();

            foreach (SolicitacaoItem itemSolicitado in itensSolicitados)
            {
                ProdutoSetor produtoSetor = new ProdutoSetorRepository().BuscarPorId(
                    itemSolicitado.IdProdutoSetor);

                if (produtoSetor == null || produtoSetor.IdSetor != setor.IdSetor)
                {
                    throw new InvalidOperationException(
                        "Os itens da solicitação não correspondem à barraca solicitante.");
                }

                TransferenciaItem itemTransferencia = new TransferenciaItem();
                itemTransferencia.IdProduto = produtoSetor.IdProduto;
                itemTransferencia.Quantidade = itemSolicitado.Quantidade;
                itensTransferencia.Add(itemTransferencia);
            }

            if (itensTransferencia.Count == 0)
            {
                throw new InvalidOperationException("A solicitação não possui itens para atender.");
            }

            TransferenciaEstoque transferencia = new TransferenciaEstoque();
            transferencia.IdLocalOrigem = estoqueGeral.IdLocalEstoque;
            transferencia.IdLocalDestino = estoqueSetor.IdLocalEstoque;
            transferencia.IdSolicitacao = solicitacao.IdSolicitacao;
            transferencia.ChaveOperacao = Guid.NewGuid().ToString();
            transferencia.DataHora = DateTime.Now;
            transferencia.Observacao = "Atendimento integral da solicitação " +
                solicitacao.IdSolicitacao;

            return Registrar(usuarioAtual, transferencia, itensTransferencia);
        }

        public int Registrar(
            Usuario usuario,
            TransferenciaEstoque transferencia,
            IList<TransferenciaItem> itens)
        {
            if (transferencia == null)
            {
                throw new ArgumentNullException(nameof(transferencia));
            }

            if (itens == null || itens.Count == 0)
            {
                throw new ArgumentException(
                    "A transferência deve possuir pelo menos um item.",
                    nameof(itens));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(nomePerfil, "Gerente", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException(
                    "Somente gerentes podem registrar transferências de estoque.");
            }

            LocalEstoque localOrigem = localRepository.BuscarPorId(transferencia.IdLocalOrigem);
            LocalEstoque localDestino = localRepository.BuscarPorId(transferencia.IdLocalDestino);

            if (localOrigem == null || localDestino == null ||
                !localOrigem.Ativo || !localDestino.Ativo ||
                localOrigem.EhCentral || localDestino.EhCentral ||
                localOrigem.EhGeralEvento && localDestino.EhGeralEvento ||
                !localOrigem.IdEvento.HasValue || !localDestino.IdEvento.HasValue ||
                localOrigem.IdEvento.Value != localDestino.IdEvento.Value ||
                localOrigem.IdInstituicao != localDestino.IdInstituicao)
            {
                throw new ArgumentException(
                    "A transferência comum deve ocorrer entre locais de estoque do mesmo evento.");
            }

            if (transferencia.IdSolicitacaoEstoqueCentral.HasValue)
            {
                throw new UnauthorizedAccessException(
                    "Solicitações administrativas devem ser atendidas pelo fluxo próprio do estoque central.");
            }

            if (!autorizacaoService.PodeAcessarTransferencia(
                usuarioAtual,
                nomePerfil,
                transferencia))
            {
                throw new UnauthorizedAccessException(
                    "Você não pode movimentar estoque de um evento que não administra.");
            }

            if (transferencia.IdSolicitacao.HasValue)
            {
                if (!localOrigem.EhGeralEvento || localDestino.EhGeralEvento)
                {
                    throw new InvalidOperationException(
                        "Uma solicitação de membro deve ser atendida do estoque geral para o estoque do setor.");
                }

                Solicitacao solicitacao = solicitacaoRepository.BuscarPorId(
                    transferencia.IdSolicitacao.Value);

                if (solicitacao == null ||
                    !autorizacaoService.PodeGerenciarSolicitacao(
                        usuarioAtual,
                        nomePerfil,
                        solicitacao))
                {
                    throw new UnauthorizedAccessException(
                        "Você não pode atender solicitações de outro gerente.");
                }

                if (!string.Equals(
                    solicitacao.Situacao,
                    SituacoesSolicitacao.Aprovada,
                    StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        "A solicitação precisa estar aprovada antes da transferência.");
                }
            }

            HashSet<int> produtosInformados = new HashSet<int>();

            foreach (TransferenciaItem item in itens)
            {
                if (item == null)
                {
                    throw new ArgumentException(
                        "A transferência contém um item nulo.",
                        nameof(itens));
                }

                ValidarQuantidade(item.Quantidade);

                if (!produtosInformados.Add(item.IdProduto))
                {
                    throw new ArgumentException(
                        "Informe cada produto uma única vez na transferência.",
                        nameof(itens));
                }
            }

            Guid chaveOperacao;

            if (!Guid.TryParse(transferencia.ChaveOperacao, out chaveOperacao))
            {
                throw new ArgumentException(
                    "Gere uma chave de operacao antes de iniciar a transferencia e reutilize-a em tentativas repetidas.",
                    nameof(transferencia));
            }

            transferencia.ChaveOperacao = chaveOperacao.ToString();

            transferencia.IdUsuarioResponsavel = usuarioAtual.IdUsuario;

            if (transferencia.DataHora == DateTime.MinValue)
            {
                transferencia.DataHora = DateTime.Now;
            }

            return transferenciaRepository.CadastrarComItens(transferencia, itens);
        }

        public void Editar(
            Usuario usuario,
            TransferenciaEstoque transferencia,
            IList<TransferenciaItem> itens,
            string justificativa)
        {
            autorizacaoService.ExigirAdministradorOuGerente(usuario);
            throw new InvalidOperationException(
                "Movimentações confirmadas são imutáveis. A correção exige uma nova operação auditada.");
        }

        public List<TransferenciaEstoque> ListarPorLocalOrigem(Usuario usuario, int idLocalEstoque)
        {
            if (idLocalEstoque <= 0)
            {
                throw new ArgumentException(
                    "O identificador do local de estoque deve ser maior que zero.",
                    nameof(idLocalEstoque));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            LocalEstoqueRepository localRepository = new LocalEstoqueRepository();
            LocalEstoque local = localRepository.BuscarPorId(idLocalEstoque);

            if (local == null)
            {
                throw new InvalidOperationException("O local de estoque não foi encontrado.");
            }

            if (!autorizacaoService.PodeAcessarLocalEstoque(usuarioAtual, nomePerfil, local))
            {
                throw new UnauthorizedAccessException("Você não pode consultar transferências deste local.");
            }

            List<TransferenciaEstoque> transferencias =
                transferenciaRepository.ListarPorLocalOrigem(idLocalEstoque);
            List<TransferenciaEstoque> transferenciasPermitidas =
                new List<TransferenciaEstoque>();

            foreach (TransferenciaEstoque transferencia in transferencias)
            {
                if (autorizacaoService.PodeAcessarTransferencia(
                    usuarioAtual,
                    nomePerfil,
                    transferencia))
                {
                    transferenciasPermitidas.Add(transferencia);
                }
            }

            return transferenciasPermitidas;
        }

        public TransferenciaEstoque BuscarPorId(Usuario usuario, int idTransferencia)
        {
            if (idTransferencia <= 0)
            {
                throw new ArgumentException(
                    "O identificador da transferência deve ser maior que zero.",
                    nameof(idTransferencia));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            TransferenciaEstoque transferencia = transferenciaRepository.BuscarPorId(idTransferencia);

            if (transferencia != null && !autorizacaoService.PodeAcessarTransferencia(
                usuarioAtual,
                nomePerfil,
                transferencia))
            {
                throw new UnauthorizedAccessException("Você não pode acessar esta transferência.");
            }

            return transferencia;
        }

        public List<TransferenciaItem> ListarItens(Usuario usuario, int idTransferencia)
        {
            if (idTransferencia <= 0)
            {
                throw new ArgumentException(
                    "O identificador da transferência deve ser maior que zero.",
                    nameof(idTransferencia));
            }

            TransferenciaEstoque transferencia = BuscarPorId(usuario, idTransferencia);

            if (transferencia == null)
            {
                throw new InvalidOperationException("A transferência não foi encontrada.");
            }

            return transferenciaItemRepository.ListarPorTransferencia(idTransferencia);
        }

        private static void ValidarQuantidade(decimal quantidade)
        {
            if (quantidade <= 0 || quantidade != decimal.Truncate(quantidade))
            {
                throw new ArgumentException(
                    "A quantidade transferida deve ser um número inteiro maior que zero.");
            }
        }
    }
}
