using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class EntradaMercadoriaService
    {
        private readonly EntradaMercadoriaRepository entradaRepository = new EntradaMercadoriaRepository();
        private readonly EntradaItemRepository entradaItemRepository = new EntradaItemRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();
        private readonly ProdutoRepository produtoRepository = new ProdutoRepository();
        private readonly LocalEstoqueRepository localRepository = new LocalEstoqueRepository();

        public int Registrar(
            Usuario usuario,
            EntradaMercadoria entrada,
            IList<EntradaItem> itens)
        {
            if (entrada == null)
            {
                throw new ArgumentNullException(nameof(entrada));
            }

            if (itens == null || itens.Count == 0)
            {
                throw new ArgumentException(
                    "A entrada deve possuir pelo menos um item.",
                    nameof(itens));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(nomePerfil, "Administrador", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException(
                    "Somente administradores podem registrar entradas no estoque central.");
            }

            LocalEstoque local = localRepository.BuscarPorId(entrada.IdLocalDestino);

            if (local == null || !local.EhCentral || local.IdEvento.HasValue)
            {
                throw new InvalidOperationException(
                    "Toda entrada deve ter como destino o estoque central.");
            }

            List<LocalEstoque> locaisCentrais = localRepository.ListarCentrais(local.IdInstituicao);

            if (locaisCentrais.Count != 1 || locaisCentrais[0].IdLocalEstoque != local.IdLocalEstoque)
            {
                throw new InvalidOperationException(
                    "A instituição precisa possuir exatamente um estoque central válido.");
            }

            if (!autorizacaoService.PodeAcessarEntrada(usuarioAtual, nomePerfil, entrada))
            {
                throw new UnauthorizedAccessException(
                    "Você não está autorizado a registrar mercadorias neste estoque central.");
            }

            entrada.TipoEntrada = TiposEntradaMercadoria.Normalizar(entrada.TipoEntrada);
            entrada.IdUsuarioResponsavel = usuarioAtual.IdUsuario;

            if (entrada.DataHora == DateTime.MinValue)
            {
                entrada.DataHora = DateTime.Now;
            }

            foreach (EntradaItem item in itens)
            {
                if (item == null)
                {
                    throw new ArgumentException("A entrada contém um item nulo.", nameof(itens));
                }

                ValidarQuantidade(item.Quantidade);

                if (produtoRepository.BuscarPorId(item.IdProduto) == null)
                {
                    throw new ArgumentException("Um produto informado não foi encontrado.", nameof(itens));
                }
            }

            return entradaRepository.CadastrarComItens(entrada, itens);
        }

        public void Editar(
            Usuario usuario,
            EntradaMercadoria entrada,
            IList<EntradaItem> itens,
            string justificativa)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            throw new InvalidOperationException(
                "Movimentações confirmadas são imutáveis. A correção exige uma nova operação auditada.");
        }

        public List<EntradaMercadoria> ListarPorLocalDestino(Usuario usuario, int idLocalEstoque)
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
            LocalEstoque local = localRepository.BuscarPorId(idLocalEstoque);

            if (local == null)
            {
                throw new InvalidOperationException("O local de estoque não foi encontrado.");
            }

            if (!autorizacaoService.PodeAcessarLocalEstoque(usuarioAtual, nomePerfil, local))
            {
                throw new UnauthorizedAccessException("Você não pode consultar entradas deste local.");
            }

            return entradaRepository.ListarPorLocalDestino(idLocalEstoque);
        }

        public EntradaMercadoria BuscarPorId(Usuario usuario, int idEntrada)
        {
            if (idEntrada <= 0)
            {
                throw new ArgumentException(
                    "O identificador da entrada deve ser maior que zero.",
                    nameof(idEntrada));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            EntradaMercadoria entrada = entradaRepository.BuscarPorId(idEntrada);

            if (entrada != null && !autorizacaoService.PodeAcessarEntrada(
                usuarioAtual,
                nomePerfil,
                entrada))
            {
                throw new UnauthorizedAccessException("Você não pode acessar esta entrada.");
            }

            return entrada;
        }

        public List<EntradaItem> ListarItens(Usuario usuario, int idEntrada)
        {
            if (idEntrada <= 0)
            {
                throw new ArgumentException(
                    "O identificador da entrada deve ser maior que zero.",
                    nameof(idEntrada));
            }

            EntradaMercadoria entrada = BuscarPorId(usuario, idEntrada);

            if (entrada == null)
            {
                throw new InvalidOperationException("A entrada não foi encontrada.");
            }

            return entradaItemRepository.ListarPorEntrada(idEntrada);
        }

        private static void ValidarQuantidade(decimal quantidade)
        {
            if (quantidade <= 0 || quantidade != decimal.Truncate(quantidade))
            {
                throw new ArgumentException(
                    "A quantidade recebida deve ser um número inteiro maior que zero.");
            }
        }
    }
}
