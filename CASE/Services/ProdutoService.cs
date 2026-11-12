using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository produtoRepository = new ProdutoRepository();
        private readonly CategoriaRepository categoriaRepository = new CategoriaRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();

        public List<ProdutoSetor> ListarConfiguracoesDoSetor(Usuario usuario, int idSetor)
        {
            ValidarGerenteDoSetor(usuario, idSetor);
            return new ProdutoSetorRepository().ListarPorSetor(idSetor);
        }

        public int ConfigurarNoSetor(Usuario usuario, int idSetor, int idProduto, decimal preco)
        {
            Evento evento = ValidarGerenteDoSetor(usuario, idSetor);
            if (evento.Situacao != SituacoesEvento.Planejado && evento.Situacao != SituacoesEvento.EmAndamento)
            {
                throw new InvalidOperationException("Este evento não permite configurar produtos.");
            }
            Produto produto = produtoRepository.BuscarPorId(idProduto);
            if (produto == null || !produto.Ativo)
            {
                throw new ArgumentException("Selecione um produto ativo.");
            }
            if (preco < 0 || preco > 99999999.99M || decimal.Round(preco, 2) != preco)
            {
                throw new ArgumentException("Informe um preço não negativo com até duas casas decimais.");
            }
            ProdutoSetor configuracao = new ProdutoSetor();
            configuracao.IdSetor = idSetor;
            configuracao.IdProduto = idProduto;
            configuracao.PrecoVenda = preco;
            return new ProdutoSetorRepository().SalvarPreco(configuracao);
        }

        private Evento ValidarGerenteDoSetor(Usuario usuario, int idSetor)
        {
            string perfil;
            Usuario atual = autorizacaoService.ValidarUsuarioEObterPerfil(usuario, out perfil);
            Setor setor = new SetorRepository().BuscarPorId(idSetor);
            if (setor == null)
            {
                throw new ArgumentException("O setor não foi encontrado.");
            }
            Evento evento = new EventoRepository().BuscarPorId(setor.IdEvento);
            if (perfil != "Gerente" || evento == null || evento.IdResponsavel != atual.IdPessoa)
            {
                throw new UnauthorizedAccessException("Somente o gerente responsável configura os produtos deste setor.");
            }
            return evento;
        }

        public List<Produto> Listar()
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para consultar produtos.");
        }

        public List<Produto> Listar(Usuario usuario)
        {
            autorizacaoService.ExigirAdministradorOuGerente(usuario);
            return produtoRepository.Listar();
        }

        public int Contar()
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para consultar o total de produtos.");
        }

        public int Contar(Usuario usuario)
        {
            autorizacaoService.ExigirAdministradorOuGerente(usuario);
            return produtoRepository.Contar();
        }

        public Produto BuscarPorId(int idProduto)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para consultar produtos.");
        }

        public Produto BuscarPorId(Usuario usuario, int idProduto)
        {
            if (idProduto <= 0)
            {
                throw new ArgumentException("O identificador do produto deve ser maior que zero.", nameof(idProduto));
            }

            autorizacaoService.ExigirAdministradorOuGerente(usuario);
            return produtoRepository.BuscarPorId(idProduto);
        }

        public int Cadastrar(Produto produto)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para cadastrar produtos.");
        }

        public int Cadastrar(Usuario usuario, Produto produto)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            ValidarProduto(produto);
            produto.Nome = produto.Nome.Trim();
            produto.UnidadeMedida = produto.UnidadeMedida.Trim();

            return produtoRepository.Cadastrar(produto);
        }

        public bool Atualizar(Produto produto)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para atualizar produtos.");
        }

        public bool Atualizar(Usuario usuario, Produto produto)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            ValidarProduto(produto);

            if (produto.IdProduto <= 0)
            {
                throw new ArgumentException("O identificador do produto deve ser maior que zero.", nameof(produto));
            }

            produto.Nome = produto.Nome.Trim();
            produto.UnidadeMedida = produto.UnidadeMedida.Trim();

            return produtoRepository.Atualizar(produto);
        }

        private void ValidarProduto(Produto produto)
        {
            if (produto == null)
            {
                throw new ArgumentNullException(nameof(produto));
            }

            if (produto.IdCategoria <= 0)
            {
                throw new ArgumentException("Selecione uma categoria válida.", nameof(produto));
            }

            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                throw new ArgumentException("O nome do produto é obrigatório.", nameof(produto));
            }

            if (string.IsNullOrWhiteSpace(produto.UnidadeMedida))
            {
                throw new ArgumentException("A unidade de medida é obrigatória.", nameof(produto));
            }

            Categoria categoria = categoriaRepository.BuscarPorId(produto.IdCategoria);
            if (categoria == null)
            {
                throw new ArgumentException("A categoria informada não foi encontrada.", nameof(produto));
            }
        }
    }
}
