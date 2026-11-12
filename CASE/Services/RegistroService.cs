using System;
using System.Collections.Generic;
using CASE.Models;
using CASE.Repositories;

namespace CASE.Services
{
    public class RegistroService
    {
        private readonly RegistroRepository registroRepository = new RegistroRepository();
        private readonly RegistroItemRepository registroItemRepository = new RegistroItemRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();
        private readonly EscalaRepository escalaRepository = new EscalaRepository();
        private readonly JornadaSetorRepository jornadaRepository = new JornadaSetorRepository();
        private readonly SetorRepository setorRepository = new SetorRepository();
        private readonly EventoRepository eventoRepository = new EventoRepository();

        public int Registrar(Usuario usuario, Registro registro, IList<RegistroItem> itens)
        {
            if (registro == null)
            {
                throw new ArgumentNullException(nameof(registro));
            }

            if (itens == null || itens.Count == 0)
            {
                throw new ArgumentException(
                    "O registro deve possuir pelo menos um item.",
                    nameof(itens));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);

            if (!string.Equals(nomePerfil, "Membro", StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedAccessException(
                    "Somente membros podem registrar vendas ou perdas.");
            }

            Escala escala = escalaRepository.BuscarPorId(registro.IdEscala);

            if (escala == null || !autorizacaoService.PodeAcessarEscala(
                usuarioAtual,
                nomePerfil,
                escala))
            {
                throw new UnauthorizedAccessException(
                    "Você não está vinculado à escala informada.");
            }

            JornadaSetor jornada = jornadaRepository.BuscarPorId(escala.IdJornada);

            if (jornada == null)
            {
                throw new InvalidOperationException("A jornada da escala não foi encontrada.");
            }

            Setor setor = setorRepository.BuscarPorId(jornada.IdSetor);

            if (setor == null)
            {
                throw new InvalidOperationException("O setor da escala não foi encontrado.");
            }

            Evento evento = eventoRepository.BuscarPorId(setor.IdEvento);

            if (evento == null || !string.Equals(
                evento.Situacao,
                SituacoesEvento.EmAndamento,
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException(
                    "Vendas e perdas só podem ser registradas enquanto o evento estiver em andamento.");
            }

            registro.Tipo = TiposRegistro.Normalizar(registro.Tipo);
            if (registro.Tipo == TiposRegistro.Venda)
            {
                if (registro.FormaPagamento != "dinheiro" && registro.FormaPagamento != "pix" &&
                    registro.FormaPagamento != "cartao")
                {
                    throw new ArgumentException("Selecione Dinheiro, Pix ou Cartão para a venda.");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(registro.Observacao))
                {
                    throw new ArgumentException("Informe a justificativa da perda.");
                }
                registro.FormaPagamento = null;
            }
            registro.IdUsuarioResponsavel = usuarioAtual.IdUsuario;

            if (registro.DataHora == DateTime.MinValue)
            {
                registro.DataHora = DateTime.Now;
            }

            HashSet<int> produtosInformados = new HashSet<int>();
            foreach (RegistroItem item in itens)
            {
                if (item == null)
                {
                    throw new ArgumentException("O registro contém um item nulo.", nameof(itens));
                }

                ValidarQuantidade(item.Quantidade);
                if (!produtosInformados.Add(item.IdProdutoSetor))
                {
                    throw new ArgumentException("Agrupe a quantidade de cada produto em um único item.");
                }
                if (registro.Tipo == TiposRegistro.Venda &&
                    (!item.PrecoUnitarioPraticado.HasValue || item.PrecoUnitarioPraticado.Value < 0 ||
                    item.PrecoUnitarioPraticado.Value > 99999999.99M ||
                    decimal.Round(item.PrecoUnitarioPraticado.Value, 2) != item.PrecoUnitarioPraticado.Value))
                {
                    throw new ArgumentException("Informe o preço efetivamente praticado na venda, com até duas casas decimais.");
                }
                if (registro.Tipo == TiposRegistro.Perda)
                {
                    item.PrecoUnitarioPraticado = null;
                }
            }

            return registroRepository.RegistrarComItens(registro, itens);
        }

        public void Editar(
            Usuario usuario,
            Registro registro,
            IList<RegistroItem> itens,
            string justificativa)
        {
            autorizacaoService.ExigirAdministradorOuGerente(usuario);
            throw new InvalidOperationException(
                "Lançamentos confirmados são imutáveis. A correção exige uma nova operação auditada.");
        }

        private Evento ObterEventoDoRegistro(Registro registro)
        {
            Escala escala = escalaRepository.BuscarPorId(registro.IdEscala);

            if (escala == null)
            {
                return null;
            }

            JornadaSetor jornada = jornadaRepository.BuscarPorId(escala.IdJornada);

            if (jornada == null)
            {
                return null;
            }

            Setor setor = setorRepository.BuscarPorId(jornada.IdSetor);

            if (setor == null)
            {
                return null;
            }

            return eventoRepository.BuscarPorId(setor.IdEvento);
        }

        public List<Registro> ListarPorEscala(Usuario usuario, int idEscala)
        {
            if (idEscala <= 0)
            {
                throw new ArgumentException(
                    "O identificador da escala deve ser maior que zero.",
                    nameof(idEscala));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            EscalaRepository escalaRepository = new EscalaRepository();
            Escala escala = escalaRepository.BuscarPorId(idEscala);

            if (escala == null)
            {
                throw new InvalidOperationException("A escala não foi encontrada.");
            }

            if (!autorizacaoService.PodeAcessarEscala(usuarioAtual, nomePerfil, escala))
            {
                throw new UnauthorizedAccessException("Você não pode consultar registros desta escala.");
            }

            List<Registro> registros = registroRepository.ListarPorEscala(idEscala);
            List<Registro> registrosPermitidos = new List<Registro>();

            foreach (Registro registro in registros)
            {
                if (autorizacaoService.PodeAcessarRegistro(
                    usuarioAtual,
                    nomePerfil,
                    registro))
                {
                    registrosPermitidos.Add(registro);
                }
            }

            return registrosPermitidos;
        }

        public Registro BuscarPorId(Usuario usuario, int idRegistro)
        {
            if (idRegistro <= 0)
            {
                throw new ArgumentException(
                    "O identificador do registro deve ser maior que zero.",
                    nameof(idRegistro));
            }

            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuario,
                out nomePerfil);
            Registro registro = registroRepository.BuscarPorId(idRegistro);

            if (registro != null && !autorizacaoService.PodeAcessarRegistro(
                usuarioAtual,
                nomePerfil,
                registro))
            {
                throw new UnauthorizedAccessException("Você não pode acessar este registro.");
            }

            return registro;
        }

        public List<RegistroItem> ListarItens(Usuario usuario, int idRegistro)
        {
            if (idRegistro <= 0)
            {
                throw new ArgumentException(
                    "O identificador do registro deve ser maior que zero.",
                    nameof(idRegistro));
            }

            Registro registro = BuscarPorId(usuario, idRegistro);

            if (registro == null)
            {
                throw new InvalidOperationException("O registro não foi encontrado.");
            }

            return registroItemRepository.ListarPorRegistro(idRegistro);
        }

        private static void ValidarQuantidade(decimal quantidade)
        {
            if (quantidade <= 0 || quantidade != decimal.Truncate(quantidade))
            {
                throw new ArgumentException(
                    "A quantidade registrada deve ser um número inteiro maior que zero.");
            }
        }
    }
}
