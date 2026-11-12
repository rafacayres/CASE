using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class InstituicaoService
    {
        private readonly InstituicaoRepository instituicaoRepository = new InstituicaoRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();

        public List<Instituicao> Listar()
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para consultar instituições.");
        }

        public List<Instituicao> Listar(Usuario usuario)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            return instituicaoRepository.Listar();
        }

        public Instituicao BuscarPorId(int idInstituicao)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para consultar instituições.");
        }

        public Instituicao BuscarPorId(Usuario usuario, int idInstituicao)
        {
            if (idInstituicao <= 0)
            {
                throw new ArgumentException(
                    "O identificador da instituição deve ser maior que zero.",
                    nameof(idInstituicao));
            }

            autorizacaoService.ExigirAdministrador(usuario);
            return instituicaoRepository.BuscarPorId(idInstituicao);
        }

        public int Cadastrar(Instituicao instituicao)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para cadastrar instituições.");
        }

        public int Cadastrar(Usuario usuario, Instituicao instituicao)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            ValidarInstituicao(instituicao);
            instituicao.Nome = instituicao.Nome.Trim();

            return instituicaoRepository.Cadastrar(instituicao);
        }

        public bool Atualizar(Instituicao instituicao)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para atualizar instituições.");
        }

        public bool Atualizar(Usuario usuario, Instituicao instituicao)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            ValidarInstituicao(instituicao);

            if (instituicao.IdInstituicao <= 0)
            {
                throw new ArgumentException(
                    "O identificador da instituição deve ser maior que zero.",
                    nameof(instituicao));
            }

            instituicao.Nome = instituicao.Nome.Trim();
            return instituicaoRepository.Atualizar(instituicao);
        }

        private static void ValidarInstituicao(Instituicao instituicao)
        {
            if (instituicao == null)
            {
                throw new ArgumentNullException(nameof(instituicao));
            }

            if (string.IsNullOrWhiteSpace(instituicao.Nome))
            {
                throw new ArgumentException("O nome da instituição é obrigatório.", nameof(instituicao));
            }
        }
    }
}
