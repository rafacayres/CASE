using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;

namespace CASE
{
    public partial class DashboardControl : UserControl
    {
        private readonly Usuario usuarioAutenticado;
        private readonly Dictionary<DateTime, decimal> quantidadesPorDia = new Dictionary<DateTime, decimal>();

        public DashboardControl() : this(null)
        {
        }

        public DashboardControl(Usuario usuario)
        {
            InitializeComponent();
            usuarioAutenticado = usuario;
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;
            CASE.Utils.FrontendTheme.Aplicar(this);

            CASE.Utils.VisualUtils.Arredondar(panelTotalProdutos, 12);
            CASE.Utils.VisualUtils.Arredondar(panelEstoqueBaixo, 12);
            CASE.Utils.VisualUtils.Arredondar(panelIconeProdutos, 10);
            CASE.Utils.VisualUtils.Arredondar(panelIconeEstoqueBaixo, 10);
            CASE.Utils.VisualUtils.Arredondar(panelEntradasMes, 12);
            CASE.Utils.VisualUtils.Arredondar(panelSaidasMes, 12);
            CASE.Utils.VisualUtils.Arredondar(panelIconeEntradas, 10);
            CASE.Utils.VisualUtils.Arredondar(panelIconeSaidas, 10);
            CASE.Utils.VisualUtils.Arredondar(panelGraficoMovimentacoes, 12);
            CASE.Utils.VisualUtils.Arredondar(panelMovimentacoesRecentes, 12);
            BackColor = CASE.Utils.FrontendTheme.Fundo;
            AutoScroll = true;
            AutoScrollMinSize = new Size(970, 620);
            lblSubtituloDashboard.Text = "Resumo dos dados disponíveis";
            lblTituloDashboard.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTituloDashboard.SetBounds(80, 25, 400, 40);
            lblSubtituloDashboard.Location = new Point(82, 68);
            panelIconeProdutos.BackColor = Color.FromArgb(224, 237, 255);
            panelIconeEstoqueBaixo.BackColor = Color.FromArgb(255, 244, 219);
            panelIconeEntradas.BackColor = Color.FromArgb(220, 247, 237);
            panelIconeSaidas.BackColor = Color.FromArgb(255, 229, 233);
            CarregarTotalDeProdutos();
            CarregarEstoqueBaixo();
            lblEntradasMesValor.Text = "—";
            lblSaidasMesValor.Text = "—";
            lblTituloGrafico.Text = "Movimentações sem dados consolidados";
            lblTituloMovimentacoes.Text = "Sem dados disponíveis";
            lblTituloMovimentacoes.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            CarregarMovimentosReais();
            EventHandler ajustar = (s, e) =>
            {
                int largura = Math.Max(970, ClientSize.Width) - 60;
                Panel[] cards = { panelTotalProdutos, panelEstoqueBaixo, panelEntradasMes, panelSaidasMes };
                Panel[] icones = { panelIconeProdutos, panelIconeEstoqueBaixo, panelIconeEntradas, panelIconeSaidas };
                for (int i = 0; i < 4; i++)
                {
                    cards[i].SetBounds(30 + i * (largura + 16) / 4, 112, (largura - 48) / 4, 112);
                    icones[i].Location = new Point(cards[i].Width - 54, 16);
                    CASE.Utils.VisualUtils.Arredondar(cards[i], 12);
                }
                int direita = Math.Max(360, largura * 38 / 100);
                int altura = Math.Max(330, ClientSize.Height - 278);
                panelGraficoMovimentacoes.SetBounds(30, 244, largura - direita - 16, altura);
                panelMovimentacoesRecentes.SetBounds(30 + largura - direita, 244, direita, altura);
                CASE.Utils.VisualUtils.Arredondar(panelGraficoMovimentacoes, 12);
                CASE.Utils.VisualUtils.Arredondar(panelMovimentacoesRecentes, 12);
            };
            SizeChanged += ajustar;
            ajustar(this, EventArgs.Empty);
        }

        private void CarregarTotalDeProdutos()
        {
            try
            {
                string perfil;
                new AutorizacaoService().ValidarUsuarioEObterPerfil(usuarioAutenticado, out perfil);
                if (perfil == "Membro")
                {
                    HashSet<int> produtos = new HashSet<int>();
                    foreach (DataRow linha in new ConsultaOperacionalService().Estoques(usuarioAutenticado).Rows)
                    {
                        produtos.Add(Convert.ToInt32(linha["id_produto"]));
                    }
                    lblTotalProdutosValor.Text = produtos.Count.ToString();
                    return;
                }
                ProdutoService produtoService = new ProdutoService();
                int totalProdutos = produtoService.Contar(usuarioAutenticado);
                lblTotalProdutosValor.Text = totalProdutos.ToString();
            }
            catch (Exception)
            {
                lblTotalProdutosValor.Text = "—";
                lblSubtituloDashboard.Text = "Não foi possível carregar os dados do banco.";
            }
        }

        private void CarregarEstoqueBaixo()
        {
            try
            {
                LocalEstoqueService localService = new LocalEstoqueService();
                EstoqueService estoqueService = new EstoqueService();
                List<CASE.Models.LocalEstoque> locais = localService.Listar(
                    usuarioAutenticado);
                int quantidadeEstoqueBaixo = 0;

                foreach (CASE.Models.LocalEstoque local in locais)
                {
                    List<CASE.Models.Estoque> saldos = estoqueService.ListarPorLocal(
                        usuarioAutenticado,
                        local.IdLocalEstoque);

                    foreach (CASE.Models.Estoque estoque in saldos)
                    {
                        if (estoque.QuantidadeAtual <= estoque.QuantidadeMinima)
                        {
                            quantidadeEstoqueBaixo++;
                        }
                    }
                }

                lblEstoqueBaixoValor.Text = quantidadeEstoqueBaixo.ToString();
            }
            catch (Exception)
            {
                lblEstoqueBaixoValor.Text = "—";
            }
        }

        private void CarregarMovimentosReais()
        {
            try
            {
                DateTime inicio = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                DataTable movimentos = new ConsultaOperacionalService().Movimentacoes(
                    usuarioAutenticado, inicio, DateTime.Today);
                decimal entradas = 0;
                decimal saidas = 0;
                ListBox recentes = new ListBox();
                recentes.BorderStyle = BorderStyle.None;
                recentes.Font = new Font("Segoe UI", 9F);
                recentes.SetBounds(16, 62, panelMovimentacoesRecentes.Width - 32, panelMovimentacoesRecentes.Height - 80);
                recentes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                recentes.HorizontalScrollbar = true;
                foreach (DataRow movimento in movimentos.Rows)
                {
                    decimal quantidade = Convert.ToDecimal(movimento["quantidade"]);
                    string tipo = Convert.ToString(movimento["tipo"]);
                    if (tipo == "entrada")
                    {
                        entradas += quantidade;
                    }
                    else if (tipo == "venda" || tipo == "perda")
                    {
                        saidas += quantidade;
                    }
                    DateTime dia = Convert.ToDateTime(movimento["data_hora"]).Date;
                    if (!quantidadesPorDia.ContainsKey(dia))
                    {
                        quantidadesPorDia.Add(dia, 0);
                    }
                    quantidadesPorDia[dia] += quantidade;
                    if (recentes.Items.Count < 20)
                    {
                        recentes.Items.Add(dia.ToString("dd/MM") + " · " + tipo + " · " +
                            movimento["produto"] + " · " + quantidade.ToString("N0"));
                    }
                }
                lblEntradasMesValor.Text = entradas.ToString("N0");
                lblSaidasMesValor.Text = saidas.ToString("N0");
                lblSubtituloDashboard.Text = "Entradas: recebimentos externos. Saídas: vendas e perdas. Transferências não são somadas nesses cards.";
                lblTituloGrafico.Text = "Unidades movimentadas por dia";
                lblTituloMovimentacoes.Text = "Movimentações recentes";
                if (recentes.Items.Count == 0)
                {
                    recentes.Items.Add("Nenhuma movimentação autorizada neste mês.");
                }
                panelMovimentacoesRecentes.Controls.Add(recentes);
                panelGraficoMovimentacoes.Paint += PintarMovimentosReais;
                panelGraficoMovimentacoes.Resize += (s, e) => panelGraficoMovimentacoes.Invalidate();
            }
            catch (Exception erro)
            {
                lblEntradasMesValor.Text = "—";
                lblSaidasMesValor.Text = "—";
                lblTituloMovimentacoes.Text = "Não foi possível consultar o histórico";
                System.Diagnostics.Trace.WriteLine(erro.GetType().Name + " em DashboardControl: " + erro.StackTrace);
            }
        }

        private void PintarMovimentosReais(object sender, PaintEventArgs e)
        {
            if (quantidadesPorDia.Count == 0)
            {
                e.Graphics.DrawString("Sem movimentações neste mês.", Font, Brushes.Gray, 20, 90);
                return;
            }
            decimal maior = 1;
            foreach (decimal quantidade in quantidadesPorDia.Values)
            {
                maior = Math.Max(maior, quantidade);
            }
            List<DateTime> dias = new List<DateTime>(quantidadesPorDia.Keys);
            dias.Sort();
            Rectangle area = new Rectangle(64, 90,
                Math.Max(1, panelGraficoMovimentacoes.Width - 100),
                Math.Max(1, panelGraficoMovimentacoes.Height - 145));
            PointF[] pontos = new PointF[dias.Count];
            double intervalo = Math.Max(1, (dias[dias.Count - 1] - dias[0]).TotalDays);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (Pen grade = new Pen(Color.FromArgb(232, 238, 246)))
            using (Pen linha = new Pen(CASE.Utils.VisualUtils.Azul, 3F))
            using (Brush pincel = new SolidBrush(CASE.Utils.VisualUtils.Azul))
            using (Font fonte = new Font("Segoe UI", 9F))
            {
                for (int nivel = 0; nivel <= 4; nivel++)
                {
                    float y = area.Bottom - area.Height * nivel / 4F;
                    e.Graphics.DrawLine(grade, area.Left, y, area.Right, y);
                    string valor = (maior * nivel / 4).ToString("N0");
                    SizeF tamanho = e.Graphics.MeasureString(valor, fonte);
                    e.Graphics.DrawString(valor, fonte, Brushes.Gray,
                        area.Left - tamanho.Width - 8, y - tamanho.Height / 2);
                }

                int ultimoRotulo = -100;
                for (int i = 0; i < dias.Count; i++)
                {
                    float x = area.Left + (float)((dias[i] - dias[0]).TotalDays / intervalo * area.Width);
                    if (dias.Count == 1)
                    {
                        x = area.Left + area.Width / 2F;
                    }

                    float y = area.Bottom - (float)(quantidadesPorDia[dias[i]] / maior) * area.Height;
                    pontos[i] = new PointF(x, y);
                    if (x - ultimoRotulo >= 58 || i == dias.Count - 1)
                    {
                        string data = dias[i].ToString("dd/MM");
                        SizeF tamanho = e.Graphics.MeasureString(data, fonte);
                        e.Graphics.DrawString(data, fonte, Brushes.Gray,
                            x - tamanho.Width / 2, area.Bottom + 12);
                        ultimoRotulo = (int)x;
                    }
                }

                if (pontos.Length > 1)
                {
                    e.Graphics.DrawLines(linha, pontos);
                }

                foreach (PointF ponto in pontos)
                {
                    e.Graphics.FillEllipse(Brushes.White, ponto.X - 5, ponto.Y - 5, 10, 10);
                    e.Graphics.FillEllipse(pincel, ponto.X - 3, ponto.Y - 3, 6, 6);
                }
            }
        }
    }
}
