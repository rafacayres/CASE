using System;
using System.Collections.Generic;
using System.Linq;

namespace CASE
{
    // Dados da sessão, descartados ao fechar o aplicativo.
    internal static class OperacoesPreview
    {
        internal sealed class Fornecedor
        {
            public string Nome { get; set; }
            public string Documento { get; set; }
            public string Email { get; set; }
            public string Telefone { get; set; }

            public override string ToString()
            {
                return Nome;
            }
        }

        internal sealed class Conta
        {
            public string Nome { get; set; }
            public string Login { get; set; }
            public string Email { get; set; }
            public string Situacao { get; set; }
            public string Perfil { get; set; }
            public string Motivo { get; set; }
            public DateTime SolicitadoEm { get; set; }

            public Conta()
            {
                Situacao = "Pendente";
                Perfil = "Padrão";
                Motivo = string.Empty;
                SolicitadoEm = DateTime.Now;
            }
        }

        internal sealed class ItemEntrada
        {
            public string Produto { get; set; }
            public decimal Quantidade { get; set; }
            public decimal Custo { get; set; }
        }

        internal sealed class Movimento
        {
            public DateTime Data { get; set; }
            public string Produto { get; set; }
            public string Local { get; set; }
            public string Tipo { get; set; }
            public string Referencia { get; set; }
            public decimal Quantidade { get; set; }
        }
        public static readonly string[] Locais = new string[0];
        public static readonly string[] Produtos = new string[0];
        public static readonly List<Fornecedor> Fornecedores = new List<Fornecedor>();
        public static readonly List<Conta> Contas = new List<Conta>();
        public static readonly List<Movimento> Movimentos = new List<Movimento>();
        private static int sequencia;

        public static decimal Saldo(string local, string produto)
        {
            return Movimentos.Where(m => m.Local == local && m.Produto == produto).Sum(m => m.Quantidade);
        }

        public static void Receber(string local, string tipo, Fornecedor fornecedor, DateTime data, string documento, IList<ItemEntrada> itens)
        {
            if (!Locais.Contains(local) || itens.Count == 0 || (tipo == "Compra" && fornecedor == null)
                || itens.Any(i => !Produtos.Contains(i.Produto) || i.Quantidade <= 0 || i.Custo < 0))
                throw new InvalidOperationException("Revise o destino, o fornecedor e os itens da entrada.");
            string referencia = "Entrada #" + (++sequencia) + " · " + (fornecedor == null ? "Sem fornecedor" : fornecedor.Nome)
                + (string.IsNullOrWhiteSpace(documento) ? "" : " · " + documento);
            foreach (var item in itens)
                Movimentos.Add(new Movimento { Data = data, Local = local, Produto = item.Produto,
                    Tipo = tipo, Quantidade = item.Quantidade, Referencia = referencia });
        }

        public static void Transferir(string origem, string destino, string produto, decimal quantidade, string observacao)
        {
            if (!Locais.Contains(origem) || !Locais.Contains(destino) || origem == destino || !Produtos.Contains(produto)
                || quantidade <= 0 || quantidade > Saldo(origem, produto))
                throw new InvalidOperationException("Escolha locais diferentes e uma quantidade disponível na origem.");
            string referencia = "Transferência #" + (++sequencia) + (string.IsNullOrWhiteSpace(observacao) ? "" : " · " + observacao);
            DateTime agora = DateTime.Now;
            Movimentos.Add(new Movimento { Data = agora, Produto = produto, Local = origem, Tipo = "Transferência · saída", Quantidade = -quantidade, Referencia = referencia });
            Movimentos.Add(new Movimento { Data = agora, Produto = produto, Local = destino, Tipo = "Transferência · entrada", Quantidade = quantidade, Referencia = referencia });
        }
    }
}
