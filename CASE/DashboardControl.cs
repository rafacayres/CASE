using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CASE
{
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            InitializeComponent();
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
            lblSubtituloDashboard.Text = "Visão geral do seu estoque";
            lblTituloDashboard.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTituloDashboard.SetBounds(80, 25, 400, 40);
            lblSubtituloDashboard.Location = new Point(82, 68);
            panelIconeProdutos.BackColor = Color.FromArgb(224, 237, 255);
            panelIconeEstoqueBaixo.BackColor = Color.FromArgb(255, 244, 219);
            panelIconeEntradas.BackColor = Color.FromArgb(220, 247, 237);
            panelIconeSaidas.BackColor = Color.FromArgb(255, 229, 233);
            lblTotalProdutosValor.Text = "120";
            lblEstoqueBaixoValor.Text = "8";
            lblEntradasMesValor.Text = "45";
            lblSaidasMesValor.Text = "32";
            lblTituloMovimentacoes.Text = "Produtos em destaque";
            lblTituloMovimentacoes.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            var grafico = new Panel { Name = "panelGraficoPreview", BackColor = Color.White };
            panelGraficoMovimentacoes.Controls.Add(grafico);
            CASE.Utils.FrontendTheme.Grafico(grafico, "linhas");
            string[] nomes = { "Notebook Dell", "Mouse Logitech", "Teclado Mecânico", "Monitor LG" };
            string[] quantidades = { "12 unidades", "3 unidades", "25 unidades", "5 unidades" };
            for (int i = 0; i < nomes.Length; i++)
            {
                var linha = new Panel { Name = "produtoDestaque" + i, Height = 65, BackColor = Color.White };
                linha.Controls.Add(new PictureBox { Name = "pictureProduto" + i,
                    Bounds = new Rectangle(12, 12, 38, 38), BackColor = Color.FromArgb(240, 245, 251),
                    SizeMode = PictureBoxSizeMode.Zoom, AccessibleName = "Imagem de " + nomes[i] });
                linha.Controls.Add(CASE.Utils.FrontendTheme.Legenda(nomes[i], new Rectangle(60, 9, 180, 22)));
                linha.Controls.Add(CASE.Utils.FrontendTheme.Legenda(quantidades[i], new Rectangle(60, 32, 150, 20)));
                var status = CASE.Utils.FrontendTheme.Legenda(i % 2 == 0 ? "Em estoque" : "Estoque baixo", new Rectangle(245, 19, 105, 24));
                status.BackColor = i % 2 == 0 ? Color.FromArgb(222, 247, 238) : Color.FromArgb(255, 234, 236);
                status.ForeColor = i % 2 == 0 ? Color.SeaGreen : Color.Firebrick;
                status.TextAlign = ContentAlignment.MiddleCenter;
                linha.Controls.Add(status);
                panelMovimentacoesRecentes.Controls.Add(linha);
            }
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
                grafico.SetBounds(12, 52, panelGraficoMovimentacoes.Width - 24, altura - 66);
                for (int i = 0; i < 4; i++)
                {
                    var linha = panelMovimentacoesRecentes.Controls["produtoDestaque" + i];
                    linha.SetBounds(8, 54 + i * 65, direita - 16, 65);
                    linha.Controls[3].Left = linha.Width - 114;
                    linha.Controls[1].Width = linha.Width - 178;
                }
                CASE.Utils.VisualUtils.Arredondar(panelGraficoMovimentacoes, 12);
                CASE.Utils.VisualUtils.Arredondar(panelMovimentacoesRecentes, 12);
            };
            SizeChanged += ajustar;
            ajustar(this, EventArgs.Empty);
        }
    }
}
