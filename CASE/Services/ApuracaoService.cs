using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class ApuracaoService
    {
        private readonly ApuracaoRepository apuracaoRepository = new ApuracaoRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();

        public int RegistrarConferencia(Usuario usuario, Apuracao apuracao)
        {
            if (apuracao == null)
            {
                throw new ArgumentNullException(nameof(apuracao));
            }
            ListarPorJornada(usuario, apuracao.IdJornada);
            ValidarValor(apuracao.ValorDinheiro);
            ValidarValor(apuracao.ValorPix);
            ValidarValor(apuracao.ValorCartao);
            apuracao.ValorInformado = apuracao.ValorDinheiro.Value + apuracao.ValorPix.Value +
                apuracao.ValorCartao.Value;
            if (apuracao.ValorInformado > 99999999.99M)
            {
                throw new ArgumentException("O total informado excede o limite da apuração.");
            }
            apuracao.IdUsuarioResponsavel = usuario.IdUsuario;
            apuracao.DataHora = DateTime.Now;
            return apuracaoRepository.Cadastrar(apuracao);
        }

        private static void ValidarValor(decimal? valor)
        {
            if (!valor.HasValue || valor.Value < 0 || valor.Value > 99999999.99M ||
                decimal.Round(valor.Value, 2) != valor.Value)
            {
                throw new ArgumentException("Informe valores não negativos com até duas casas decimais.");
            }
        }

        public List<Apuracao> ListarPorJornada(Usuario usuario, int idJornada)
        {
            if (idJornada <= 0)
            {
                throw new ArgumentException(
                    "O identificador da jornada deve ser maior que zero.",
                    nameof(idJornada));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            JornadaSetorRepository jornadaRepository = new JornadaSetorRepository();
            JornadaSetor jornada = jornadaRepository.BuscarPorId(idJornada);

            if (jornada == null)
            {
                throw new InvalidOperationException("A jornada não foi encontrada.");
            }

            if (nomePerfil.Equals("Membro", StringComparison.OrdinalIgnoreCase) ||
                !autorizacaoService.PodeAcessarJornada(usuarioAtual, nomePerfil, jornada))
            {
                throw new UnauthorizedAccessException("Você não pode consultar as apurações desta jornada.");
            }

            return apuracaoRepository.ListarPorJornada(idJornada);
        }

        public Apuracao BuscarPorId(Usuario usuario, int idApuracao)
        {
            if (idApuracao <= 0)
            {
                throw new ArgumentException(
                    "O identificador da apuração deve ser maior que zero.",
                    nameof(idApuracao));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            Apuracao apuracao = apuracaoRepository.BuscarPorId(idApuracao);

            if (apuracao != null && !autorizacaoService.PodeAcessarApuracao(
                usuarioAtual,
                nomePerfil,
                apuracao))
            {
                throw new UnauthorizedAccessException("Você não pode acessar esta apuração.");
            }

            return apuracao;
        }
    }
}
