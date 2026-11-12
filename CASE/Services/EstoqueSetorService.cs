using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class EstoqueSetorService
    {
        private readonly EstoqueSetorRepository estoqueSetorRepository = new EstoqueSetorRepository();
        private readonly SetorRepository setorRepository = new SetorRepository();
        private readonly EventoRepository eventoRepository = new EventoRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();
        private readonly LocalEstoqueRepository localEstoqueRepository = new LocalEstoqueRepository();

        public List<EstoqueSetor> Listar(Usuario usuario)
        {
            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            List<EstoqueSetor> associacoes = estoqueSetorRepository.Listar();
            List<EstoqueSetor> associacoesPermitidas = new List<EstoqueSetor>();

            foreach (EstoqueSetor associacao in associacoes)
            {
                Setor setor = setorRepository.BuscarPorId(associacao.IdSetor);

                if (autorizacaoService.PodeAcessarSetor(usuarioAtual, nomePerfil, setor))
                {
                    associacoesPermitidas.Add(associacao);
                }
            }

            return associacoesPermitidas;
        }

        public EstoqueSetor BuscarPorSetor(Usuario usuario, int idSetor)
        {
            if (idSetor <= 0)
            {
                throw new ArgumentException(
                    "O identificador do setor deve ser maior que zero.",
                    nameof(idSetor));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            EstoqueSetor associacao = estoqueSetorRepository.BuscarPorSetor(idSetor);

            if (associacao == null)
            {
                return null;
            }

            Setor setor = setorRepository.BuscarPorId(idSetor);

            if (!autorizacaoService.PodeAcessarSetor(usuarioAtual, nomePerfil, setor))
            {
                throw new UnauthorizedAccessException("Você não pode acessar o estoque deste setor.");
            }

            return associacao;
        }

        public void Associar(Usuario usuario, int idSetor, int idLocalEstoque)
        {
            EstoqueSetor associacao = ValidarAssociacao(
                usuario,
                idSetor,
                idLocalEstoque);

            estoqueSetorRepository.Associar(associacao);
        }

        public void AtualizarLocal(Usuario usuario, int idSetor, int idLocalEstoque)
        {
            EstoqueSetor associacao = ValidarAssociacao(
                usuario,
                idSetor,
                idLocalEstoque);

            if (!estoqueSetorRepository.AtualizarLocal(associacao))
            {
                throw new InvalidOperationException(
                    "Não foi encontrada uma associação de estoque para o setor.");
            }
        }

        private EstoqueSetor ValidarAssociacao(
            Usuario usuario,
            int idSetor,
            int idLocalEstoque)
        {
            if (idSetor <= 0 || idLocalEstoque <= 0)
            {
                throw new ArgumentException(
                    "Informe um setor e um local de estoque válidos.");
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(nomePerfil, "Gerente", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException(
                    "Somente o gerente responsável pode associar estoque a setores.");
            }

            Setor setor = setorRepository.BuscarPorId(idSetor);
            LocalEstoque local = localEstoqueRepository.BuscarPorId(idLocalEstoque);
            Evento evento = null;

            if (setor != null)
            {
                evento = eventoRepository.BuscarPorId(setor.IdEvento);
            }

            if (setor == null || local == null ||
                !autorizacaoService.PodeAcessarSetor(usuarioAtual, nomePerfil, setor))
            {
                throw new UnauthorizedAccessException(
                    "Você não pode configurar o estoque deste setor.");
            }

            if (!local.Ativo || local.EhCentral || local.EhGeralEvento ||
                !local.IdEvento.HasValue || local.IdEvento.Value != setor.IdEvento ||
                evento == null || evento.IdInstituicao != local.IdInstituicao)
            {
                throw new InvalidOperationException(
                    "O local associado deve ser uma barraca ativa do mesmo evento.");
            }

            EstoqueSetor associacao = new EstoqueSetor();
            associacao.IdSetor = setor.IdSetor;
            associacao.IdLocalEstoque = local.IdLocalEstoque;
            associacao.IdEvento = setor.IdEvento;
            return associacao;
        }
    }
}
