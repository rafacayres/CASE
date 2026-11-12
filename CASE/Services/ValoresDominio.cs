using System;

namespace CASE.Services
{
    public static class SituacoesUsuario
    {
        public const string Pendente = "pendente";
        public const string Aprovado = "aprovado";
        public const string Rejeitado = "rejeitado";

        public static string Normalizar(string situacao)
        {
            if (string.IsNullOrWhiteSpace(situacao))
            {
                throw new ArgumentException("Informe a situação da conta.", nameof(situacao));
            }

            string valor = situacao.Trim();

            if (string.Equals(valor, Pendente, StringComparison.OrdinalIgnoreCase))
            {
                return Pendente;
            }

            if (string.Equals(valor, Aprovado, StringComparison.OrdinalIgnoreCase))
            {
                return Aprovado;
            }

            if (string.Equals(valor, Rejeitado, StringComparison.OrdinalIgnoreCase))
            {
                return Rejeitado;
            }

            throw new ArgumentException(
                "A situação informada para a conta não é válida.",
                nameof(situacao));
        }
    }

    public static class SituacoesEvento
    {
        public const string Planejado = "planejado";
        public const string EmAndamento = "em_andamento";
        public const string Finalizado = "finalizado";
        public const string Cancelado = "cancelado";

        public static string Normalizar(string situacao)
        {
            if (string.IsNullOrWhiteSpace(situacao))
            {
                throw new ArgumentException("Informe a situação do evento.", nameof(situacao));
            }

            string valor = situacao.Trim();

            if (string.Equals(valor, Planejado, StringComparison.OrdinalIgnoreCase))
            {
                return Planejado;
            }

            if (string.Equals(valor, EmAndamento, StringComparison.OrdinalIgnoreCase))
            {
                return EmAndamento;
            }

            if (string.Equals(valor, Finalizado, StringComparison.OrdinalIgnoreCase))
            {
                return Finalizado;
            }

            if (string.Equals(valor, Cancelado, StringComparison.OrdinalIgnoreCase))
            {
                return Cancelado;
            }

            throw new ArgumentException(
                "A situação informada para o evento não é válida.",
                nameof(situacao));
        }

        public static bool PodeTransicionar(string situacaoAtual, string novaSituacao)
        {
            string atual = Normalizar(situacaoAtual);
            string nova = Normalizar(novaSituacao);

            if (atual == Planejado && (nova == EmAndamento || nova == Cancelado))
            {
                return true;
            }

            if (atual == EmAndamento && (nova == Finalizado || nova == Cancelado))
            {
                return true;
            }

            return false;
        }

        public static void ValidarTransicao(string situacaoAtual, string novaSituacao)
        {
            if (!PodeTransicionar(situacaoAtual, novaSituacao))
            {
                throw new InvalidOperationException(
                    "A transição de situação do evento não é permitida.");
            }
        }
    }

    public static class SituacoesEscala
    {
        public const string Pendente = "pendente";
        public const string Confirmada = "confirmada";
        public const string Cancelada = "cancelada";

        public static string Normalizar(string situacao)
        {
            if (string.IsNullOrWhiteSpace(situacao))
            {
                throw new ArgumentException("Informe a situação da escala.", nameof(situacao));
            }

            string valor = situacao.Trim();

            if (string.Equals(valor, Pendente, StringComparison.OrdinalIgnoreCase))
            {
                return Pendente;
            }

            if (string.Equals(valor, Confirmada, StringComparison.OrdinalIgnoreCase))
            {
                return Confirmada;
            }

            if (string.Equals(valor, Cancelada, StringComparison.OrdinalIgnoreCase))
            {
                return Cancelada;
            }

            throw new ArgumentException(
                "A situação informada para a escala não é válida.",
                nameof(situacao));
        }

        public static bool PodeTransicionar(string situacaoAtual, string novaSituacao)
        {
            string atual = Normalizar(situacaoAtual);
            string nova = Normalizar(novaSituacao);

            if (atual == Pendente && (nova == Confirmada || nova == Cancelada))
            {
                return true;
            }

            if (atual == Confirmada && nova == Cancelada)
            {
                return true;
            }

            return false;
        }

        public static void ValidarTransicao(string situacaoAtual, string novaSituacao)
        {
            if (!PodeTransicionar(situacaoAtual, novaSituacao))
            {
                throw new InvalidOperationException(
                    "A transição de situação da escala não é permitida.");
            }
        }
    }

    public static class SituacoesSolicitacao
    {
        public const string Pendente = "pendente";
        public const string Aprovada = "aprovada";
        public const string Atendida = "atendida";
        public const string Rejeitada = "rejeitada";
        public const string Cancelada = "cancelada";

        public static string Normalizar(string situacao)
        {
            if (string.IsNullOrWhiteSpace(situacao))
            {
                throw new ArgumentException("Informe a situação da solicitação.", nameof(situacao));
            }

            string valor = situacao.Trim();

            if (string.Equals(valor, Pendente, StringComparison.OrdinalIgnoreCase))
            {
                return Pendente;
            }

            if (string.Equals(valor, Aprovada, StringComparison.OrdinalIgnoreCase))
            {
                return Aprovada;
            }

            if (string.Equals(valor, Atendida, StringComparison.OrdinalIgnoreCase))
            {
                return Atendida;
            }

            if (string.Equals(valor, Rejeitada, StringComparison.OrdinalIgnoreCase))
            {
                return Rejeitada;
            }

            if (string.Equals(valor, Cancelada, StringComparison.OrdinalIgnoreCase))
            {
                return Cancelada;
            }

            throw new ArgumentException(
                "A situação informada para a solicitação não é válida.",
                nameof(situacao));
        }

        public static bool PodeTransicionar(string situacaoAtual, string novaSituacao)
        {
            string atual = Normalizar(situacaoAtual);
            string nova = Normalizar(novaSituacao);

            if (atual == Pendente &&
                (nova == Aprovada || nova == Rejeitada || nova == Cancelada))
            {
                return true;
            }

            if (atual == Aprovada && (nova == Atendida || nova == Cancelada))
            {
                return true;
            }

            return false;
        }

        public static void ValidarTransicao(string situacaoAtual, string novaSituacao)
        {
            if (!PodeTransicionar(situacaoAtual, novaSituacao))
            {
                throw new InvalidOperationException(
                    "A transição de situação da solicitação não é permitida.");
            }
        }
    }

    public static class TiposSolicitacaoEstoqueCentral
    {
        public const string Liberacao = "liberacao";
        public const string Devolucao = "devolucao";
    }

    public static class TiposEntradaMercadoria
    {
        public const string Compra = "compra";
        public const string Doacao = "doacao";

        public static string Normalizar(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
            {
                throw new ArgumentException("Informe o tipo da entrada de mercadoria.", nameof(tipo));
            }

            string valor = tipo.Trim();

            if (string.Equals(valor, Compra, StringComparison.OrdinalIgnoreCase))
            {
                return Compra;
            }

            if (string.Equals(valor, Doacao, StringComparison.OrdinalIgnoreCase))
            {
                return Doacao;
            }

            throw new ArgumentException("O tipo informado para a entrada não é válido.", nameof(tipo));
        }
    }

    public static class TiposRegistro
    {
        public const string Venda = "venda";
        public const string Perda = "perda";

        public static string Normalizar(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
            {
                throw new ArgumentException("Informe o tipo do registro.", nameof(tipo));
            }

            string valor = tipo.Trim();

            if (string.Equals(valor, Venda, StringComparison.OrdinalIgnoreCase))
            {
                return Venda;
            }

            if (string.Equals(valor, Perda, StringComparison.OrdinalIgnoreCase))
            {
                return Perda;
            }

            throw new ArgumentException("O tipo informado para o registro não é válido.", nameof(tipo));
        }
    }
}
