using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class EstoqueService
    {
        private readonly EstoqueRepository estoqueRepository = new EstoqueRepository();
        private readonly LocalEstoqueRepository localEstoqueRepository = new LocalEstoqueRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();

        public void ConfigurarQuantidadesAlvo(Usuario usuario, int idEstoque, decimal minima, decimal ideal)
        {
            string perfil;
            Usuario atual = autorizacaoService.ValidarUsuarioEObterPerfil(usuario, out perfil);
            if (perfil != "Administrador" && perfil != "Gerente")
            {
                throw new UnauthorizedAccessException("Somente administradores e gerentes configuram os limites de estoque.");
            }
            Estoque estoque = BuscarPorId(atual, idEstoque);
            if (estoque == null)
            {
                throw new ArgumentException("O estoque não foi encontrado.");
            }
            LocalEstoque local = localEstoqueRepository.BuscarPorId(estoque.IdLocalEstoque);
            if (local.EhCentral && perfil != "Administrador")
            {
                throw new UnauthorizedAccessException("Somente o administrador configura o estoque central.");
            }
            if (minima < 0 || ideal < minima || ideal > 9999999 ||
                minima != decimal.Truncate(minima) || ideal != decimal.Truncate(ideal))
            {
                throw new ArgumentException("Informe unidades inteiras não negativas; o ideal deve ser maior ou igual ao mínimo.");
            }
            estoque.QuantidadeMinima = minima;
            estoque.QuantidadeIdeal = ideal;
            estoqueRepository.AtualizarQuantidadesAlvo(estoque);
        }

        public List<Estoque> ListarPorLocal(Usuario usuario, int idLocalEstoque)
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
            LocalEstoque local = localEstoqueRepository.BuscarPorId(idLocalEstoque);

            if (local == null)
            {
                throw new InvalidOperationException("O local de estoque não foi encontrado.");
            }

            if (!autorizacaoService.PodeAcessarLocalEstoque(usuarioAtual, nomePerfil, local))
            {
                throw new UnauthorizedAccessException("Você não pode acessar este local de estoque.");
            }

            return estoqueRepository.ListarPorLocal(idLocalEstoque);
        }

        public Estoque BuscarPorId(Usuario usuario, int idEstoque)
        {
            if (idEstoque <= 0)
            {
                throw new ArgumentException(
                    "O identificador do estoque deve ser maior que zero.",
                    nameof(idEstoque));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            Estoque estoque = estoqueRepository.BuscarPorId(idEstoque);

            if (estoque == null)
            {
                return null;
            }

            LocalEstoque local = localEstoqueRepository.BuscarPorId(estoque.IdLocalEstoque);

            if (!autorizacaoService.PodeAcessarLocalEstoque(usuarioAtual, nomePerfil, local))
            {
                throw new UnauthorizedAccessException("Você não pode acessar este estoque.");
            }

            return estoque;
        }
    }
}
