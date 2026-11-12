using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class SetorService
    {
        private readonly SetorRepository setorRepository = new SetorRepository();
        private readonly EventoRepository eventoRepository = new EventoRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();

        public int Criar(Usuario usuario, Setor setor)
        {
            if (setor == null)
            {
                throw new ArgumentNullException(nameof(setor));
            }

            if (setor.IdEvento <= 0 || string.IsNullOrWhiteSpace(setor.Nome))
            {
                throw new ArgumentException(
                    "Informe um evento e o nome do setor.",
                    nameof(setor));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            Evento evento = eventoRepository.BuscarPorId(setor.IdEvento);

            if (!string.Equals(nomePerfil, "Gerente", StringComparison.OrdinalIgnoreCase) ||
                evento == null || evento.IdResponsavel != usuarioAtual.IdPessoa)
            {
                throw new UnauthorizedAccessException(
                    "Somente o gerente responsável pode criar setores neste evento.");
            }

            bool eventoPermiteSetores = string.Equals(
                evento.Situacao,
                SituacoesEvento.Planejado,
                StringComparison.OrdinalIgnoreCase);

            if (!eventoPermiteSetores)
            {
                eventoPermiteSetores = string.Equals(
                    evento.Situacao,
                    SituacoesEvento.EmAndamento,
                    StringComparison.OrdinalIgnoreCase);
            }

            if (!eventoPermiteSetores)
            {
                throw new InvalidOperationException(
                    "O evento não permite a criação de setores nesta situação.");
            }

            setor.Nome = setor.Nome.Trim();

            return setorRepository.CadastrarComEstoqueDeBarraca(
                setor,
                usuarioAtual.IdUsuario);
        }

        public List<Setor> ListarPorEvento(Usuario usuario, int idEvento)
        {
            if (idEvento <= 0)
            {
                throw new ArgumentException(
                    "O identificador do evento deve ser maior que zero.",
                    nameof(idEvento));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            List<Setor> setores = setorRepository.ListarPorEvento(idEvento);
            List<Setor> setoresPermitidos = new List<Setor>();

            foreach (Setor setor in setores)
            {
                if (autorizacaoService.PodeAcessarSetor(usuarioAtual, nomePerfil, setor))
                {
                    setoresPermitidos.Add(setor);
                }
            }

            return setoresPermitidos;
        }

        public Setor BuscarPorId(Usuario usuario, int idSetor)
        {
            if (idSetor <= 0)
            {
                throw new ArgumentException(
                    "O identificador do setor deve ser maior que zero.",
                    nameof(idSetor));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            Setor setor = setorRepository.BuscarPorId(idSetor);

            if (setor != null && !autorizacaoService.PodeAcessarSetor(
                usuarioAtual,
                nomePerfil,
                setor))
            {
                throw new UnauthorizedAccessException("Você não pode acessar este setor.");
            }

            return setor;
        }
    }
}
