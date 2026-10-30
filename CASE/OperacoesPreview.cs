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
            public string Nome, Documento, Email, Telefone;
            public override string ToString() { return Nome; }
        }
        internal sealed class Conta
        {
            public string Nome, Login, Email, Situacao = "Pendente", Perfil = "Padrão", Motivo = "";
            public DateTime SolicitadoEm = DateTime.Now;
        }
        internal sealed class ItemEntrada
        {
            public string Produto;
            public decimal Quantidade, Custo;
        }
        internal sealed class Movimento
        {
            public DateTime Data;
            public string Produto, Local, Tipo, Referencia;
            public decimal Quantidade;
        }
        public static readonly string[] Locais = { "Depósito central", "São João 2026 · Bebidas", "São João 2026 · Alimentação", "São João 2026 · Doces" };
        public static readonly string[] Produtos = { "Refrigerante", "Água mineral", "Cachorro-quente", "Bolo de milho", "Paçoca" };
        public static readonly List<Fornecedor> Fornecedores = new List<Fornecedor> {
            new Fornecedor { Nome = "Distribuidora Silva", Email = "contato@example.com", Documento = "", Telefone = "" },
            new Fornecedor { Nome = "Mercado da Comunidade", Email = "mercado@example.com", Documento = "", Telefone = "" }
        };
        public static readonly List<Conta> Contas = new List<Conta> {
            new Conta { Nome = "Marina Souza", Login = "marina.souza", Email = "marina@example.com" },
            new Conta { Nome = "Pedro Lima", Login = "pedro.lima", Email = "pedro@example.com" },
            new Conta { Nome = "Ana Silva", Login = "ana", Email = "ana@example.com", Situacao = "Aprovado" },
            new Conta { Nome = "Rafael Cayres", Login = "rafael", Email = "rafael@example.com", Situacao = "Aprovado", Perfil = "Administrador" },
            new Conta { Nome = "Conta de exemplo", Login = "exemplo", Email = "exemplo@example.com", Situacao = "Rejeitado", Motivo = "Cadastro duplicado" }
        };
        public static readonly List<Movimento> Movimentos = new List<Movimento>();
        private static int sequencia;

        static OperacoesPreview()
        {
            foreach (string produto in Produtos)
                Movimentos.Add(new Movimento { Data = DateTime.Today, Local = Locais[0], Produto = produto, Tipo = "Saldo inicial", Quantidade = 100, Referencia = "Amostra" });
            Movimentos.Add(new Movimento { Data = DateTime.Today, Local = Locais[1], Produto = Produtos[0], Tipo = "Saldo inicial", Quantidade = 12, Referencia = "Amostra" });
            Movimentos.Add(new Movimento { Data = DateTime.Today, Local = Locais[1], Produto = Produtos[1], Tipo = "Saldo inicial", Quantidade = 8, Referencia = "Amostra" });
        }

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
