using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class PessoaService
    {
        private readonly PessoaRepository pessoaRepository = new PessoaRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();

        public List<Pessoa> Listar()
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para consultar pessoas.");
        }

        public List<Pessoa> Listar(Usuario usuario)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            return pessoaRepository.Listar();
        }

        public Pessoa BuscarPorId(int idPessoa)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para consultar pessoas.");
        }

        public Pessoa BuscarPorId(Usuario usuario, int idPessoa)
        {
            if (idPessoa <= 0)
            {
                throw new ArgumentException("O identificador da pessoa deve ser maior que zero.", nameof(idPessoa));
            }

            autorizacaoService.ExigirAdministrador(usuario);
            return pessoaRepository.BuscarPorId(idPessoa);
        }

        public int Cadastrar(Pessoa pessoa)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para cadastrar pessoas.");
        }

        public int Cadastrar(Usuario usuario, Pessoa pessoa)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            ValidarPessoa(pessoa);
            pessoa.Nome = pessoa.Nome.Trim();

            return pessoaRepository.Cadastrar(pessoa);
        }

        public bool Atualizar(Pessoa pessoa)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para atualizar pessoas.");
        }

        public bool Atualizar(Usuario usuario, Pessoa pessoa)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            ValidarPessoa(pessoa);

            if (pessoa.IdPessoa <= 0)
            {
                throw new ArgumentException("O identificador da pessoa deve ser maior que zero.", nameof(pessoa));
            }

            pessoa.Nome = pessoa.Nome.Trim();
            return pessoaRepository.Atualizar(pessoa);
        }

        public Pessoa BuscarMeusDados(Usuario usuario)
        {
            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            Pessoa pessoa = pessoaRepository.BuscarPorId(usuarioAtual.IdPessoa);

            if (pessoa == null)
            {
                throw new InvalidOperationException(
                    "Os dados pessoais da conta não foram encontrados.");
            }

            return pessoa;
        }

        public bool AtualizarMeusDados(
            Usuario usuario,
            string nome,
            string email)
        {
            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            Pessoa pessoa = pessoaRepository.BuscarPorId(usuarioAtual.IdPessoa);

            if (pessoa == null)
            {
                throw new InvalidOperationException(
                    "Os dados pessoais da conta não foram encontrados.");
            }

            pessoa.Nome = nome;
            pessoa.Email = email;
            ValidarPessoa(pessoa);
            pessoa.Nome = pessoa.Nome.Trim();

            if (!pessoaRepository.Atualizar(pessoa))
            {
                throw new InvalidOperationException(
                    "Os dados não foram atualizados. Recarregue a tela e tente novamente.");
            }

            return true;
        }

        private static void ValidarPessoa(Pessoa pessoa)
        {
            if (pessoa == null)
            {
                throw new ArgumentNullException(nameof(pessoa));
            }

            if (string.IsNullOrWhiteSpace(pessoa.Nome))
            {
                throw new ArgumentException("O nome da pessoa é obrigatório.", nameof(pessoa));
            }
        }
    }
}
