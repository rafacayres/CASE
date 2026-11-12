using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class CategoriaService
    {
        private readonly CategoriaRepository categoriaRepository = new CategoriaRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();

        public List<Categoria> Listar()
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para consultar categorias.");
        }

        public List<Categoria> Listar(Usuario usuario)
        {
            string nomePerfil;
            autorizacaoService.ValidarUsuarioEObterPerfil(usuario, out nomePerfil);

            bool podeConsultar = string.Equals(
                nomePerfil,
                "Administrador",
                StringComparison.OrdinalIgnoreCase);

            if (!podeConsultar)
            {
                podeConsultar = string.Equals(
                    nomePerfil,
                    "Gerente",
                    StringComparison.OrdinalIgnoreCase);
            }

            if (!podeConsultar)
            {
                throw new UnauthorizedAccessException("Seu perfil não pode consultar categorias.");
            }

            return categoriaRepository.Listar();
        }

        public Categoria BuscarPorId(int idCategoria)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para consultar categorias.");
        }

        public Categoria BuscarPorId(Usuario usuario, int idCategoria)
        {
            if (idCategoria <= 0)
            {
                throw new ArgumentException(
                    "O identificador da categoria deve ser maior que zero.",
                    nameof(idCategoria));
            }

            Listar(usuario);
            return categoriaRepository.BuscarPorId(idCategoria);
        }

        public int Cadastrar(Categoria categoria)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para cadastrar categorias.");
        }

        public int Cadastrar(Usuario usuario, Categoria categoria)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            ValidarCategoria(categoria);
            categoria.Nome = categoria.Nome.Trim();
            VerificarNomeDuplicado(categoria);

            return categoriaRepository.Cadastrar(categoria);
        }

        public bool Atualizar(Categoria categoria)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para atualizar categorias.");
        }

        public bool Atualizar(Usuario usuario, Categoria categoria)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            ValidarCategoria(categoria);

            if (categoria.IdCategoria <= 0)
            {
                throw new ArgumentException("O identificador da categoria deve ser maior que zero.", nameof(categoria));
            }

            categoria.Nome = categoria.Nome.Trim();
            VerificarNomeDuplicado(categoria);
            return categoriaRepository.Atualizar(categoria);
        }

        private static void ValidarCategoria(Categoria categoria)
        {
            if (categoria == null)
            {
                throw new ArgumentNullException(nameof(categoria));
            }

            if (string.IsNullOrWhiteSpace(categoria.Nome))
            {
                throw new ArgumentException("O nome da categoria é obrigatório.", nameof(categoria));
            }
        }

        private void VerificarNomeDuplicado(Categoria categoria)
        {
            List<Categoria> categorias = categoriaRepository.Listar();

            foreach (Categoria categoriaExistente in categorias)
            {
                if (categoriaExistente.IdCategoria == categoria.IdCategoria)
                {
                    continue;
                }

                bool mesmoNome = string.Equals(
                    categoriaExistente.Nome,
                    categoria.Nome,
                    StringComparison.OrdinalIgnoreCase);

                if (mesmoNome)
                {
                    throw new InvalidOperationException("Já existe uma categoria com esse nome.");
                }
            }
        }
    }
}
