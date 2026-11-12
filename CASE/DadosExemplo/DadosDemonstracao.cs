using System;
using System.Collections.Generic;
using System.Data;
using CASE.Models;
using CASE.Quermesse;

namespace CASE
{
    // Dados exclusivamente em memória. Esta classe não conhece conexão, Repository ou sessão.
    public sealed class DadosDemonstracao
    {
        public const string Aviso = "Modo demonstração · dados ilustrativos, sem gravação no MySQL";
        public readonly QuermessePreview Equipe = new QuermessePreview();
        public readonly List<Produto> Produtos = new List<Produto>();
        public readonly List<Categoria> Categorias = new List<Categoria>();
        public readonly List<Fornecedor> Fornecedores = new List<Fornecedor>();
        public readonly List<Evento> Eventos = new List<Evento>();
        public readonly List<TipoEvento> TiposEvento = new List<TipoEvento>();
        public readonly List<Setor> Setores = new List<Setor>();
        public readonly List<LocalEstoque> Locais = new List<LocalEstoque>();
        public readonly DataTable Solicitacoes = new DataTable();
        public readonly DataTable Escalas = new DataTable();
        internal readonly List<OperacoesPreview.Movimento> Movimentos = new List<OperacoesPreview.Movimento>();
        internal readonly List<OperacoesPreview.Conta> Contas = new List<OperacoesPreview.Conta>();
        public event EventHandler Alterado;
        public DadosDemonstracao()
        {
            Categorias.Add(new Categoria { IdCategoria = 1, Nome = "Bebidas" });
            Categorias.Add(new Categoria { IdCategoria = 2, Nome = "Alimentação" });
            Categorias.Add(new Categoria { IdCategoria = 3, Nome = "Doces" });
            TiposEvento.Add(new TipoEvento { IdTipoEvento = 1, Nome = "Festa comunitária" });
            TiposEvento.Add(new TipoEvento { IdTipoEvento = 2, Nome = "Ação solidária" });
            Eventos.Add(new Evento { IdEvento = 1, IdInstituicao = 1, IdTipoEvento = 1, Nome = "Festa da Comunidade", Local = "Pátio da comunidade", IdResponsavel = 1, DataInicio = DateTime.Today.AddDays(-1), DataFim = DateTime.Today.AddDays(2), Situacao = "em_andamento", Observacao = "Encontro comunitário com alimentação e arrecadação solidária." });
            Equipe.Evento = Eventos[0].Nome;
            string[] nomesSetores =
            {
                "Bebidas",
                "Alimentação",
                "Doces"
            };
            Locais.Add(new LocalEstoque { IdLocalEstoque = 1, IdInstituicao = 1, Nome = "Estoque central", EhCentral = true, Ativo = true });
            Locais.Add(new LocalEstoque { IdLocalEstoque = 2, IdInstituicao = 1, IdEvento = 1, Nome = "Geral · Festa da Comunidade", EhGeralEvento = true, Ativo = true });
            for (int i = 0; i < nomesSetores.Length; i++)
            {
                Equipe.Setores.Add(nomesSetores[i]);
                Setores.Add(new Setor { IdSetor = i + 1, IdEvento = 1, Nome = nomesSetores[i] });
                Locais.Add(new LocalEstoque { IdLocalEstoque = i + 3, IdInstituicao = 1, IdEvento = 1, Nome = nomesSetores[i], Ativo = true });
            }

            string[] nomes =
            {
                "Água mineral 500 ml",
                "Refrigerante 350 ml",
                "Pastel",
                "Cachorro-quente",
                "Brigadeiro"
            };
            int[] categorias =
            {
                1,
                1,
                2,
                2,
                3
            };
            int[] entradas =
            {
                500,
                300,
                200,
                150,
                250
            };
            int[] distribuicao =
            {
                200,
                120,
                80,
                60,
                100
            };
            int[] barracas =
            {
                120,
                60,
                60,
                40,
                60
            };
            int[] vendas =
            {
                25,
                18,
                20,
                15,
                30
            };
            int[] perdas =
            {
                2,
                0,
                3,
                0,
                2
            };
            for (int i = 0; i < nomes.Length; i++)
            {
                Produtos.Add(new Produto { IdProduto = i + 1, IdCategoria = categorias[i], Nome = nomes[i], Descricao = "Produto ilustrativo da apresentação CASE", UnidadeMedida = "UN", Ativo = true });
                using (var memoria = new System.IO.MemoryStream())
                {
                    Properties.Resources.LucideAzulProdutos.Save(memoria, System.Drawing.Imaging.ImageFormat.Png);
                    Produtos[Produtos.Count - 1].Imagem = memoria.ToArray();
                }

                string setor = nomesSetores[categorias[i] - 1];
                Equipe.Produtos.Add(new ProdutoPreview { Nome = nomes[i], Setor = setor });
                AdicionarMovimento(nomes[i], Locais[0].Nome, "entrada", entradas[i], "Recebimento ilustrativo", DateTime.Today.AddDays(-2));
                TransferirMemoria(Locais[0].Nome, Locais[1].Nome, nomes[i], distribuicao[i], DateTime.Today.AddDays(-1));
                TransferirMemoria(Locais[1].Nome, setor, nomes[i], barracas[i], DateTime.Today);
                RegistrarMemoria(setor, nomes[i], "venda", vendas[i], "Atendimento da comunidade");
                if (perdas[i] > 0)
                {
                    RegistrarMemoria(setor, nomes[i], "perda", perdas[i], "Embalagem danificada");
                }
            }

            Equipe.Pessoas.Add(new PessoaPreview { Id = 1, Nome = "Marina Alves", Login = "marina.demo", Perfil = PerfilUsuario.Gerente, Setor = "Coordenação", Turno = "Integral" });
            Equipe.Pessoas.Add(new PessoaPreview { Id = 2, Nome = "João Santos", Login = "joao.demo", Perfil = PerfilUsuario.Vendedor, Setor = "Bebidas", Turno = "Noite" });
            Equipe.Pessoas.Add(new PessoaPreview { Id = 3, Nome = "Ana Oliveira", Login = "ana.demo", Perfil = PerfilUsuario.Vendedor, Setor = "Alimentação", Turno = "Noite" });
            Equipe.Pessoas.Add(new PessoaPreview { Id = 4, Nome = "Lucas Pereira", Login = "lucas.demo", Perfil = PerfilUsuario.Vendedor, Setor = "Doces", Turno = "Tarde" });
            Escalas.Columns.Add("Nome");
            Escalas.Columns.Add("Setor");
            Escalas.Columns.Add("Data", typeof(DateTime));
            Escalas.Columns.Add("Turno");
            Escalas.Columns.Add("Entrada");
            Escalas.Columns.Add("Saída");
            Escalas.Columns.Add("Situação");
            Escalas.Columns.Add("PessoaId", typeof(int));
            Escalas.Rows.Add("João Santos", "Bebidas", DateTime.Today, "Noite", "18:00", "23:00", "confirmada", 2);
            Escalas.Rows.Add("Ana Oliveira", "Alimentação", DateTime.Today, "Noite", "18:00", "23:00", "confirmada", 3);
            Escalas.Rows.Add("Lucas Pereira", "Doces", DateTime.Today, "Tarde", "14:00", "18:00", "confirmada", 4);
            Fornecedores.Add(new Fornecedor { IdFornecedor = 1, Nome = "Distribuidora Horizonte · demonstração", Telefone = "(11) 3333-1200", Email = "contato@horizonte.example" });
            Fornecedores.Add(new Fornecedor { IdFornecedor = 2, Nome = "Mercado São José · demonstração", Telefone = "(11) 3333-2400", Email = "contato@mercado.example" });
            foreach (PessoaPreview pessoa in Equipe.Pessoas)
            {
                string perfil = "Membro";
                if (pessoa.Perfil == PerfilUsuario.Gerente)
                {
                    perfil = "Gerente";
                }

                Contas.Add(new OperacoesPreview.Conta { Nome = pessoa.Nome, Login = pessoa.Login, Email = "pessoa@example.invalid", Situacao = "aprovado", Perfil = perfil });
            }

            Contas.Add(new OperacoesPreview.Conta { Nome = "Beatriz Costa", Login = "beatriz.demo", Email = "beatriz@example.invalid", Situacao = "pendente", Perfil = "Membro" });
            Equipe.Custos.Add(new CustoPreview { Descricao = "Gelo e conservação", Setor = "Bebidas", Valor = 35, Data = DateTime.Today, Responsavel = "Marina Alves" });
            Equipe.Custos.Add(new CustoPreview { Descricao = "Material de atendimento", Setor = "Alimentação", Valor = 60, Data = DateTime.Today, Responsavel = "Marina Alves" });
            Equipe.Custos.Add(new CustoPreview { Descricao = "Embalagens", Setor = "Doces", Valor = 20, Data = DateTime.Today, Responsavel = "Marina Alves" });
            Solicitacoes.Columns.Add("id", typeof(int));
            Solicitacoes.Columns.Add("solicitante");
            Solicitacoes.Columns.Add("setor");
            Solicitacoes.Columns.Add("tipo");
            Solicitacoes.Columns.Add("produto");
            Solicitacoes.Columns.Add("quantidade", typeof(int));
            Solicitacoes.Columns.Add("situacao");
            Solicitacoes.Columns.Add("justificativa");
            Solicitacoes.Rows.Add(1, "Marina Alves", "Geral do evento", "liberacao", nomes[0], 50, "pendente", "Reforço para o segundo dia");
            Solicitacoes.Rows.Add(2, "João Santos", "Bebidas", "reposicao", nomes[0], 20, "pendente", "Reposição para atendimento noturno");
            Solicitacoes.Rows.Add(3, "Marina Alves", "Geral do evento", "devolucao", nomes[4], 10, "pendente", "Devolução de excedente");
        }

        public decimal Preco(string produto)
        {
            int id = 0;
            foreach (Produto item in Produtos) { if (item.Nome == produto) { id = item.IdProduto; } }
            if (id == 1)
            {
                return 3.50M;
            }

            if (id == 2)
            {
                return 6M;
            }

            if (id == 3)
            {
                return 8M;
            }

            if (id == 4)
            {
                return 10M;
            }

            if (id == 5)
            {
                return 3M;
            }

            return 5M;
        }

        public void RenomearProduto(int id, string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) { throw new ArgumentException("Informe o nome ilustrativo do produto."); }
            Produto produto = Produtos.Find(p => p.IdProduto == id);
            if (produto == null) { throw new ArgumentException("O produto ilustrativo não existe."); }
            string anterior = produto.Nome;
            foreach (var movimento in Movimentos) { if (movimento.Produto == anterior) { movimento.Produto = nome; } }
            foreach (ProdutoPreview item in Equipe.Produtos) { if (item.Nome == anterior) { item.Nome = nome; } }
            foreach (RegistroPreview registro in Equipe.Registros) { if (registro.Produto == anterior) { registro.Produto = nome; } }
            foreach (DataRow pedido in Solicitacoes.Rows) { if (Convert.ToString(pedido["produto"]) == anterior) { pedido["produto"] = nome; } }
            produto.Nome = nome;
            NotificarAlteracao();
        }

        public decimal Saldo(string local, string produto)
        {
            decimal saldo = 0;
            foreach (var movimento in Movimentos)
            {
                if (movimento.Local == local && movimento.Produto == produto)
                {
                    saldo += movimento.Quantidade;
                }
            }

            return saldo;
        }

        public DataTable TabelaEstoques()
        {
            DataTable tabela = new DataTable();
            tabela.Columns.Add("Local");
            tabela.Columns.Add("Produto");
            tabela.Columns.Add("Quantidade", typeof(decimal));
            tabela.Columns.Add("Unidade");
            tabela.Columns.Add("Mínimo", typeof(decimal));
            tabela.Columns.Add("Situação");
            foreach (LocalEstoque local in Locais)
            {
                foreach (Produto produto in Produtos)
                {
                    decimal saldo = Saldo(local.Nome, produto.Nome);
                    if (saldo > 0 || TemMovimentacao(local.Nome, produto.Nome))
                    {
                        string situacao = "Em estoque";
                        if (saldo < Minimo(local.Nome, produto.Nome))
                        {
                            situacao = "Estoque baixo";
                        }

                        if (saldo == 0)
                        {
                            situacao = "Sem estoque";
                        }

                        tabela.Rows.Add(local.Nome, produto.Nome, saldo, "UN", Minimo(local.Nome, produto.Nome), situacao);
                    }
                }
            }

            return tabela;
        }

        public bool TemMovimentacao(string local, string produto)
        {
            foreach (var movimento in Movimentos)
            {
                if (movimento.Local == local && movimento.Produto == produto)
                {
                    return true;
                }
            }

            return false;
        }

        public decimal Minimo(string local, string produto)
        {
            if (local == "Estoque central")
            {
                return 50;
            }

            if (local == "Bebidas" && produto.StartsWith("Água"))
            {
                return 100;
            }

            if (local == "Bebidas")
            {
                return 50;
            }

            if (local == "Alimentação")
            {
                return 20;
            }

            if (local == "Doces")
            {
                return 30;
            }

            return 10;
        }

        public DataTable TabelaMovimentos(DateTime inicio, DateTime fim)
        {
            DataTable tabela = new DataTable();
            tabela.Columns.Add("Data", typeof(DateTime));
            tabela.Columns.Add("Produto");
            tabela.Columns.Add("Tipo");
            tabela.Columns.Add("Quantidade", typeof(decimal));
            tabela.Columns.Add("Local");
            tabela.Columns.Add("Referência");
            for (int i = Movimentos.Count - 1; i >= 0; i--)
            {
                var movimento = Movimentos[i];
                if (movimento.Data.Date >= inicio.Date && movimento.Data.Date <= fim.Date)
                {
                    tabela.Rows.Add(movimento.Data, movimento.Produto, movimento.Tipo, movimento.Quantidade, movimento.Local, movimento.Referencia);
                }
            }

            return tabela;
        }

        public void Transferir(string origem, string destino, string produto, decimal quantidade)
        {
            bool origemExiste = false;
            bool destinoExiste = false;
            foreach (LocalEstoque local in Locais)
            {
                if (local.Nome == origem) { origemExiste = true; }
                if (local.Nome == destino) { destinoExiste = true; }
            }
            if (!origemExiste || !destinoExiste || origem == destino || quantidade <= 0 || quantidade != decimal.Truncate(quantidade) || Saldo(origem, produto) < quantidade)
            {
                throw new ArgumentException("Selecione locais diferentes e uma quantidade inteira disponível na origem.");
            }

            TransferirMemoria(origem, destino, produto, quantidade, DateTime.Now);
            NotificarAlteracao();
        }

        private void TransferirMemoria(string origem, string destino, string produto, decimal quantidade, DateTime data)
        {
            AdicionarMovimento(produto, origem, "transferencia", -quantidade, origem + " → " + destino, data);
            AdicionarMovimento(produto, destino, "transferencia", quantidade, origem + " → " + destino, data);
        }

        public void Registrar(string setor, string produto, string tipo, int quantidade, string observacao, decimal? preco = null, string pagamento = "dinheiro")
        {
            if (tipo != "venda" && tipo != "perda") { throw new ArgumentException("Selecione venda ou perda."); }
            if (preco.HasValue && preco.Value < 0) { throw new ArgumentException("O preço não pode ser negativo."); }
            if (quantidade <= 0 || quantidade > Saldo(setor, produto))
            {
                throw new ArgumentException("Quantidade inválida ou superior ao saldo ilustrativo do setor.");
            }

            if (tipo == "perda" && string.IsNullOrWhiteSpace(observacao))
            {
                throw new ArgumentException("Informe o motivo da perda simulada.");
            }

            RegistrarMemoria(setor, produto, tipo, quantidade, observacao);
            RegistroPreview registro = Equipe.Registros[Equipe.Registros.Count - 1];
            if (tipo == "venda")
            {
                if (preco.HasValue)
                {
                    registro.PrecoUnitario = preco.Value;
                }

                registro.FormaPagamento = pagamento;
            }

            NotificarAlteracao();
        }

        private void RegistrarMemoria(string setor, string produto, string tipo, int quantidade, string observacao)
        {
            string pessoa = "João Santos";
            if (setor == "Alimentação")
            {
                pessoa = "Ana Oliveira";
            }

            if (setor == "Doces")
            {
                pessoa = "Lucas Pereira";
            }

            Equipe.Registros.Add(new RegistroPreview { Data = DateTime.Now, Pessoa = pessoa, Setor = setor, PessoaId = 2, Turno = "Noite", Tipo = tipo, Produto = produto, Quantidade = quantidade, Observacao = observacao, PrecoUnitario = Preco(produto), FormaPagamento = "dinheiro" });
            RegistroPreview novo = Equipe.Registros[Equipe.Registros.Count - 1];
            if (setor == "Alimentação")
            {
                novo.PessoaId = 3;
                novo.FormaPagamento = "cartao";
            }

            if (setor == "Doces")
            {
                novo.PessoaId = 4;
                novo.Turno = "Tarde";
            }

            if (produto.StartsWith("Refrigerante"))
            {
                novo.FormaPagamento = "pix";
            }

            if (tipo == "perda")
            {
                novo.PrecoUnitario = 0;
                novo.FormaPagamento = string.Empty;
            }

            AdicionarMovimento(produto, setor, tipo, -quantidade, pessoa, DateTime.Now);
        }

        internal void AdicionarMovimento(string produto, string local, string tipo, decimal quantidade, string referencia, DateTime data)
        {
            Movimentos.Add(new OperacoesPreview.Movimento { Data = data, Produto = produto, Local = local, Tipo = tipo, Quantidade = quantidade, Referencia = referencia });
        }

        public void Decidir(int id, bool aprovar, string motivo)
        {
            foreach (DataRow pedido in Solicitacoes.Rows)
            {
                if (Convert.ToInt32(pedido["id"]) != id)
                {
                    continue;
                }

                if (Convert.ToString(pedido["situacao"]) != "pendente")
                {
                    throw new InvalidOperationException("A solicitação ilustrativa já foi decidida.");
                }

                if (!aprovar && string.IsNullOrWhiteSpace(motivo))
                {
                    throw new ArgumentException("Informe o motivo da rejeição simulada.");
                }

                if (aprovar)
                {
                    pedido["situacao"] = "aprovada";
                }
                else
                {
                    pedido["situacao"] = "rejeitada";
                    pedido["justificativa"] = motivo;
                }

                NotificarAlteracao();
                return;
            }

            throw new ArgumentException("A solicitação ilustrativa não foi encontrada.");
        }

        public int Pendencias()
        {
            int quantidade = 0;
            foreach (DataRow pedido in Solicitacoes.Rows)
            {
                if (Convert.ToString(pedido["situacao"]) == "pendente")
                {
                    quantidade++;
                }
            }

            foreach (var conta in Contas)
            {
                if (conta.Situacao == "pendente")
                {
                    quantidade++;
                }
            }

            return quantidade;
        }

        public void Atender(int id)
        {
            foreach (DataRow pedido in Solicitacoes.Rows)
            {
                if (Convert.ToInt32(pedido["id"]) != id)
                {
                    continue;
                }

                if (Convert.ToString(pedido["situacao"]) != "aprovada")
                {
                    throw new InvalidOperationException("Aprove o pedido ilustrativo antes de atender. Um pedido atendido não pode ser processado novamente.");
                }

                string origem = Locais[0].Nome;
                string destino = Locais[1].Nome;
                string tipo = Convert.ToString(pedido["tipo"]);
                if (tipo == "reposicao")
                {
                    origem = Locais[1].Nome;
                    destino = Convert.ToString(pedido["setor"]);
                }

                if (tipo == "devolucao")
                {
                    origem = Locais[1].Nome;
                    destino = Locais[0].Nome;
                }

                Transferir(origem, destino, Convert.ToString(pedido["produto"]), Convert.ToInt32(pedido["quantidade"]));
                pedido["situacao"] = "atendida";
                NotificarAlteracao();
                return;
            }

            throw new ArgumentException("A solicitação ilustrativa não foi encontrada.");
        }

        public void NotificarAlteracao()
        {
            EventHandler evento = Alterado;
            if (evento != null)
            {
                evento(this, EventArgs.Empty);
            }
        }
    }
}
