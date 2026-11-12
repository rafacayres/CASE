using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class FornecedorService
    {
        private readonly FornecedorRepository fornecedorRepository = new FornecedorRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();

        public List<Fornecedor> Listar()
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para consultar fornecedores.");
        }

        public List<Fornecedor> Listar(Usuario usuario)
        {
            autorizacaoService.ExigirAdministradorOuGerente(usuario);
            return fornecedorRepository.Listar();
        }

        public Fornecedor BuscarPorId(int idFornecedor)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para consultar fornecedores.");
        }

        public Fornecedor BuscarPorId(Usuario usuario, int idFornecedor)
        {
            if (idFornecedor <= 0)
            {
                throw new ArgumentException(
                    "O identificador do fornecedor deve ser maior que zero.",
                    nameof(idFornecedor));
            }

            autorizacaoService.ExigirAdministradorOuGerente(usuario);
            return fornecedorRepository.BuscarPorId(idFornecedor);
        }

        public void Cadastrar(Fornecedor fornecedor)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para cadastrar fornecedores.");
        }

        public void Cadastrar(Usuario usuario, Fornecedor fornecedor)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            if (fornecedor == null)
            {
                throw new ArgumentNullException(nameof(fornecedor));
            }

            if (string.IsNullOrWhiteSpace(fornecedor.Nome))
            {
                throw new ArgumentException("O nome do fornecedor é obrigatório.", nameof(fornecedor));
            }

            fornecedor.Nome = fornecedor.Nome.Trim();
            fornecedorRepository.Cadastrar(fornecedor);
        }

        public void Atualizar(Fornecedor fornecedor)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para atualizar fornecedores.");
        }

        public void Atualizar(Usuario usuario, Fornecedor fornecedor)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            if (fornecedor == null)
            {
                throw new ArgumentNullException(nameof(fornecedor));
            }

            if (fornecedor.IdFornecedor <= 0)
            {
                throw new ArgumentException(
                    "O identificador do fornecedor deve ser maior que zero.",
                    nameof(fornecedor));
            }

            if (string.IsNullOrWhiteSpace(fornecedor.Nome))
            {
                throw new ArgumentException("O nome do fornecedor é obrigatório.", nameof(fornecedor));
            }

            fornecedor.Nome = fornecedor.Nome.Trim();
            fornecedorRepository.Atualizar(fornecedor);
        }
    }
}
