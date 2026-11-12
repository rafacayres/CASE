using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class TipoEventoService
    {
        private readonly TipoEventoRepository tipoEventoRepository = new TipoEventoRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();

        public List<TipoEvento> Listar()
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para consultar tipos de evento.");
        }

        public List<TipoEvento> Listar(Usuario usuario)
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
                throw new UnauthorizedAccessException("Seu perfil não pode consultar tipos de evento.");
            }

            return tipoEventoRepository.Listar();
        }

        public TipoEvento BuscarPorId(int idTipoEvento)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para consultar tipos de evento.");
        }

        public TipoEvento BuscarPorId(Usuario usuario, int idTipoEvento)
        {
            if (idTipoEvento <= 0)
            {
                throw new ArgumentException(
                    "O identificador do tipo de evento deve ser maior que zero.",
                    nameof(idTipoEvento));
            }

            Listar(usuario);
            return tipoEventoRepository.BuscarPorId(idTipoEvento);
        }

        public int Cadastrar(TipoEvento tipoEvento)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para cadastrar tipos de evento.");
        }

        public int Cadastrar(Usuario usuario, TipoEvento tipoEvento)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            ValidarTipoEvento(tipoEvento);
            tipoEvento.Nome = tipoEvento.Nome.Trim();
            VerificarNomeDuplicado(tipoEvento);

            return tipoEventoRepository.Cadastrar(tipoEvento);
        }

        public bool Atualizar(TipoEvento tipoEvento)
        {
            throw new UnauthorizedAccessException("Informe a conta autenticada para atualizar tipos de evento.");
        }

        public bool Atualizar(Usuario usuario, TipoEvento tipoEvento)
        {
            autorizacaoService.ExigirAdministrador(usuario);
            ValidarTipoEvento(tipoEvento);

            if (tipoEvento.IdTipoEvento <= 0)
            {
                throw new ArgumentException(
                    "O identificador do tipo de evento deve ser maior que zero.",
                    nameof(tipoEvento));
            }

            tipoEvento.Nome = tipoEvento.Nome.Trim();
            VerificarNomeDuplicado(tipoEvento);
            return tipoEventoRepository.Atualizar(tipoEvento);
        }

        private static void ValidarTipoEvento(TipoEvento tipoEvento)
        {
            if (tipoEvento == null)
            {
                throw new ArgumentNullException(nameof(tipoEvento));
            }

            if (string.IsNullOrWhiteSpace(tipoEvento.Nome))
            {
                throw new ArgumentException("O nome do tipo de evento é obrigatório.", nameof(tipoEvento));
            }
        }

        private void VerificarNomeDuplicado(TipoEvento tipoEvento)
        {
            List<TipoEvento> tiposEvento = tipoEventoRepository.Listar();

            foreach (TipoEvento tipoExistente in tiposEvento)
            {
                if (tipoExistente.IdTipoEvento == tipoEvento.IdTipoEvento)
                {
                    continue;
                }

                bool mesmoNome = string.Equals(
                    tipoExistente.Nome,
                    tipoEvento.Nome,
                    StringComparison.OrdinalIgnoreCase);

                if (mesmoNome)
                {
                    throw new InvalidOperationException("Já existe um tipo de evento com esse nome.");
                }
            }
        }
    }
}
