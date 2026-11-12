using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class ConfiguracaoInstitucionalService
    {
        private readonly AutorizacaoService autorizacao = new AutorizacaoService();

        public Instituicao ObterInstituicao(Usuario usuario)
        {
            autorizacao.ExigirAdministrador(usuario);
            List<Instituicao> instituicoes = new InstituicaoRepository().Listar();
            if (instituicoes.Count > 1)
            {
                throw new InvalidOperationException("O CASE está configurado para uma única instituição.");
            }
            if (instituicoes.Count == 0)
            {
                return null;
            }
            return instituicoes[0];
        }

        public int SalvarInstituicao(Usuario usuario, Instituicao instituicao)
        {
            autorizacao.ExigirAdministrador(usuario);
            if (instituicao == null)
            {
                throw new ArgumentNullException(nameof(instituicao));
            }
            ValidarNome(instituicao.Nome, 150);
            if ((instituicao.Documento != null && instituicao.Documento.Length > 18) ||
                (instituicao.Telefone != null && instituicao.Telefone.Length > 20) ||
                (instituicao.Email != null && instituicao.Email.Length > 150))
            {
                throw new ArgumentException("Documento, telefone ou e-mail excede o tamanho permitido.");
            }
            instituicao.Nome = instituicao.Nome.Trim();
            return new InstituicaoRepository().SalvarUnica(instituicao);
        }

        public int InicializarCentral(Usuario usuario)
        {
            Instituicao instituicao = ObterInstituicao(usuario);
            if (instituicao == null)
            {
                throw new InvalidOperationException("Salve os dados da instituição antes de inicializar o central.");
            }
            return new LocalEstoqueRepository().GarantirCentral(instituicao.IdInstituicao);
        }

        public List<Turno> ListarTurnos(Usuario usuario)
        {
            autorizacao.ExigirAdministrador(usuario);
            return new TurnoRepository().Listar();
        }

        public int SalvarTurno(Usuario usuario, Turno turno)
        {
            autorizacao.ExigirAdministrador(usuario);
            if (turno == null)
            {
                throw new ArgumentNullException(nameof(turno));
            }
            ValidarNome(turno.Nome, 50);
            TurnoRepository repository = new TurnoRepository();
            if (turno.IdTurno == 0)
            {
                return repository.Cadastrar(turno);
            }
            if (repository.BuscarPorId(turno.IdTurno) == null)
            {
                throw new ArgumentException("O turno não foi encontrado.");
            }
            repository.Atualizar(turno);
            return turno.IdTurno;
        }

        public List<Perfil> ListarFuncoesEquipe(Usuario usuario)
        {
            autorizacao.ExigirAdministrador(usuario);
            return new PerfilRepository().Listar();
        }

        public int SalvarFuncaoEquipe(Usuario usuario, Perfil funcao)
        {
            autorizacao.ExigirAdministrador(usuario);
            if (funcao == null)
            {
                throw new ArgumentNullException(nameof(funcao));
            }
            ValidarNome(funcao.Nome, 100);
            PerfilRepository repository = new PerfilRepository();
            if (funcao.IdPerfil == 0)
            {
                return repository.Cadastrar(funcao);
            }
            if (repository.BuscarPorId(funcao.IdPerfil) == null)
            {
                throw new ArgumentException("A função não foi encontrada.");
            }
            repository.Atualizar(funcao);
            return funcao.IdPerfil;
        }

        private static void ValidarNome(string nome, int limite)
        {
            if (string.IsNullOrWhiteSpace(nome) || nome.Length > limite)
            {
                throw new ArgumentException("Informe um nome com até " + limite + " caracteres.");
            }
        }
    }
}
