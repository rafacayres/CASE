using System;
using System.Data;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class ConsultaOperacionalService
    {
        private readonly AutorizacaoService autorizacao = new AutorizacaoService();
        private readonly ConsultaOperacionalRepository repository = new ConsultaOperacionalRepository();

        public DataTable Escalas(Usuario usuario, int idEvento)
        {
            string perfil;
            Usuario atual = autorizacao.ValidarUsuarioEObterPerfil(usuario, out perfil);
            return repository.Escalas(perfil, atual.IdPessoa, idEvento);
        }

        public DataTable ProdutosDaEscala(Usuario usuario, int idEscala)
        {
            if (new EscalaService().BuscarPorId(usuario, idEscala) == null)
            {
                throw new InvalidOperationException("A escala não foi encontrada.");
            }
            return repository.ProdutosDaEscala(idEscala);
        }

        public DataTable Registros(Usuario usuario, int evento, DateTime inicio, DateTime fim)
        {
            ValidarPeriodo(inicio, fim);
            string perfil;
            Usuario atual = autorizacao.ValidarUsuarioEObterPerfil(usuario, out perfil);
            return repository.Registros(perfil, atual.IdPessoa, evento, inicio, fim);
        }

        public DataTable Estoques(Usuario usuario)
        {
            string perfil;
            Usuario atual = autorizacao.ValidarUsuarioEObterPerfil(usuario, out perfil);
            return repository.Estoques(perfil, atual.IdPessoa);
        }

        public DataTable Movimentacoes(Usuario usuario, DateTime inicio, DateTime fim)
        {
            ValidarPeriodo(inicio, fim);
            string perfil;
            Usuario atual = autorizacao.ValidarUsuarioEObterPerfil(usuario, out perfil);
            return repository.Movimentacoes(perfil, atual.IdPessoa, inicio, fim);
        }

        public void ExportarPdf(Usuario usuario, bool estoqueAtual, DateTime inicio, DateTime fim, string caminho)
        {
            DataTable dados;
            if (estoqueAtual)
            {
                dados = Estoques(usuario);
            }
            else
            {
                dados = Movimentacoes(usuario, inicio, fim);
            }
            CASE.Utils.ExportadorRelatorio.ExportarPdf(dados, caminho);
        }

        public void ExportarCsv(Usuario usuario, bool estoqueAtual, DateTime inicio, DateTime fim, string caminho)
        {
            DataTable dados;
            if (estoqueAtual)
            {
                dados = Estoques(usuario);
            }
            else
            {
                dados = Movimentacoes(usuario, inicio, fim);
            }
            CASE.Utils.ExportadorRelatorio.ExportarCsv(dados, caminho);
        }

        private static void ValidarPeriodo(DateTime inicio, DateTime fim)
        {
            if (inicio.Date > fim.Date || fim.Date == DateTime.MaxValue.Date)
            {
                throw new ArgumentException("Informe um período válido.");
            }
        }
    }
}
