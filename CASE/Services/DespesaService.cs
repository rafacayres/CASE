using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class DespesaService
    {
        private readonly DespesaRepository despesaRepository = new DespesaRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();

        public int Cadastrar(Usuario usuario, Despesa despesa)
        {
            if (despesa == null)
            {
                throw new ArgumentNullException(nameof(despesa));
            }
            // Reutiliza a autorização de consulta do evento, inclusive a restrição a membros.
            ListarPorEvento(usuario, despesa.IdEvento);
            if (string.IsNullOrWhiteSpace(despesa.Descricao) || despesa.Descricao.Length > 255)
            {
                throw new ArgumentException("Informe a descrição da despesa com até 255 caracteres.");
            }
            if (despesa.Valor <= 0 || despesa.Valor > 99999999.99M ||
                decimal.Round(despesa.Valor, 2) != despesa.Valor || despesa.Data == DateTime.MinValue)
            {
                throw new ArgumentException("Informe uma data e um valor positivo com até duas casas decimais.");
            }
            despesa.IdUsuarioResponsavel = usuario.IdUsuario;
            return despesaRepository.Cadastrar(despesa);
        }

        public List<Despesa> ListarPorEvento(Usuario usuario, int idEvento)
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
            EventoRepository eventoRepository = new EventoRepository();
            Evento evento = eventoRepository.BuscarPorId(idEvento);

            if (evento == null)
            {
                throw new InvalidOperationException("O evento não foi encontrado.");
            }

            if (!autorizacaoService.PodeAcessarEvento(usuarioAtual, nomePerfil, evento) ||
                nomePerfil.Equals("Membro", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException("Você não pode consultar as despesas deste evento.");
            }

            return despesaRepository.ListarPorEvento(idEvento);
        }

        public Despesa BuscarPorId(Usuario usuario, int idDespesa)
        {
            if (idDespesa <= 0)
            {
                throw new ArgumentException(
                    "O identificador da despesa deve ser maior que zero.",
                    nameof(idDespesa));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            Despesa despesa = despesaRepository.BuscarPorId(idDespesa);

            if (despesa != null && !autorizacaoService.PodeAcessarDespesa(
                usuarioAtual,
                nomePerfil,
                despesa))
            {
                throw new UnauthorizedAccessException("Você não pode acessar esta despesa.");
            }

            return despesa;
        }
    }
}
