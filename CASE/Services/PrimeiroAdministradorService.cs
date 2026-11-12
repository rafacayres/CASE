using System;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class PrimeiroAdministradorService
    {
        private const string VariavelAmbiente = "CASE_PERMITIR_BOOTSTRAP_ADMIN";
        private const string ValorHabilitado = "CONFIRMAR_PRIMEIRO_ADMINISTRADOR";
        private const string ConfirmacaoExigida = "CRIAR O PRIMEIRO ADMINISTRADOR";
        private const int TamanhoMaximoLogin = 50;

        private readonly UsuarioRepository usuarioRepository = new UsuarioRepository();
        private readonly SenhaService senhaService = new SenhaService();

        public int CriarPrimeiroAdministrador(
            Pessoa pessoa,
            string login,
            string senha,
            string confirmacaoOperador)
        {
            ValidarHabilitacaoDoOperador(confirmacaoOperador);
            ValidarDados(pessoa, login, senha);

            string loginNormalizado = login.Trim();
            string hashSenha = senhaService.GerarHash(senha);
            pessoa.Nome = pessoa.Nome.Trim();

            return usuarioRepository.CriarPrimeiroAdministrador(
                pessoa,
                loginNormalizado,
                hashSenha);
        }

        private static void ValidarHabilitacaoDoOperador(string confirmacaoOperador)
        {
            string habilitacao = Environment.GetEnvironmentVariable(VariavelAmbiente);

            if (!string.Equals(habilitacao, ValorHabilitado, StringComparison.Ordinal))
            {
                throw new UnauthorizedAccessException(
                    "A inicialização do administrador está desabilitada. Habilite-a somente " +
                    "durante o procedimento controlado de configuração inicial.");
            }

            if (!string.Equals(
                confirmacaoOperador,
                ConfirmacaoExigida,
                StringComparison.Ordinal))
            {
                throw new UnauthorizedAccessException(
                    "A confirmação explícita do operador não foi informada corretamente.");
            }
        }

        private static void ValidarDados(Pessoa pessoa, string login, string senha)
        {
            if (pessoa == null)
            {
                throw new ArgumentNullException(nameof(pessoa));
            }

            if (string.IsNullOrWhiteSpace(pessoa.Nome))
            {
                throw new ArgumentException("Informe o nome do administrador.", nameof(pessoa));
            }

            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentException("Informe o login do administrador.", nameof(login));
            }

            if (login.Trim().Length > TamanhoMaximoLogin)
            {
                throw new ArgumentException(
                    "O login não pode ultrapassar 50 caracteres.",
                    nameof(login));
            }

            if (string.IsNullOrEmpty(senha))
            {
                throw new ArgumentException("Informe a senha do administrador.", nameof(senha));
            }
        }
    }
}
