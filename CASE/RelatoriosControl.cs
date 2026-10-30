using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{
    public partial class RelatoriosControl : UserControl
    {
        public RelatoriosControl()
        {
            InitializeComponent();
            CASE.Utils.FrontendTheme.Aplicar(this);
            VisualUtils.Arredondar(panelAcoesRelatorios, 12);
            panelAcoesRelatorios.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelAcoesRelatorios);
            VisualUtils.Arredondar(btnExportarPdf, 8);
            btnExportarPdf.SizeChanged += AjustarArredondamento;
            VisualUtils.Arredondar(btnExportarCsv, 8);
            btnExportarCsv.SizeChanged += AjustarArredondamento;
            VisualUtils.Arredondar(panelCardRelBaixo, 12);
            panelCardRelBaixo.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelCardRelBaixo);
            VisualUtils.Arredondar(panelCardRelSemEstoque, 12);
            panelCardRelSemEstoque.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelCardRelSemEstoque);
            VisualUtils.Arredondar(panelCardRelValor, 12);
            panelCardRelValor.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelCardRelValor);
            VisualUtils.Arredondar(panelCardRelSugestao, 12);
            panelCardRelSugestao.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelCardRelSugestao);
            VisualUtils.Arredondar(panelGraficoEsquerdo, 12);
            panelGraficoEsquerdo.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelGraficoEsquerdo);
            VisualUtils.Arredondar(panelGraficoDireito, 12);
            panelGraficoDireito.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelGraficoDireito);
            lblSubtituloRelatorios.Text = "Acompanhe o desempenho do seu estoque";
            lblGraficoEsquerdo.Text = "Entradas e saídas por dia";
            lblGraficoDireito.Text = "Distribuição por categoria";
            panelGraficoEsquerdoArea.BackColor = System.Drawing.Color.White;
            panelGraficoDireitoArea.BackColor = System.Drawing.Color.White;
            FrontendTheme.Grafico(panelGraficoEsquerdoArea, "barras");
            FrontendTheme.Grafico(panelGraficoDireitoArea, "rosca");
            btnExportarPdf.Text = "Gerar";
            btnExportarCsv.Visible = false;
            btnExportarPdf.Click += (s, e) => { panelGraficoEsquerdoArea.Invalidate(true); panelGraficoDireitoArea.Invalidate(true); };
            panelCardRelBaixo.Visible = panelCardRelSemEstoque.Visible = panelCardRelValor.Visible = panelCardRelSugestao.Visible = false;
            panelGraficoEsquerdo.Top = panelGraficoDireito.Top = 215;
            panelGraficoEsquerdo.Height = panelGraficoDireito.Height = 330;
            panelGraficoEsquerdoArea.Height = panelGraficoDireitoArea.Height = 250;
            cmbRelatorioCategoria.Items.Clear();
            cmbRelatorioCategoria.Items.AddRange(new object[] { "Movimentações", "Estoque" });
            cmbRelatorioCategoria.SelectedIndex = 0;
            dtpRelatorioInicio.Value = new DateTime(2026, 10, 1);
            dtpRelatorioFim.Value = new DateTime(2026, 10, 31);
            foreach (Control campo in new Control[] { cmbRelatorioCategoria, dtpRelatorioInicio, dtpRelatorioFim })
            {
                campo.Top = 30;
                panelAcoesRelatorios.Controls.Add(FrontendTheme.Legenda(campo == cmbRelatorioCategoria ? "Tipo de relatório" : campo == dtpRelatorioInicio ? "Data inicial" : "Data final",
                    new System.Drawing.Rectangle(campo.Left, 6, campo.Width, 20)));
            }
        }

        private void AjustarArredondamento(object sender, EventArgs e)
        {
            Control controle = (Control)sender;
            VisualUtils.Arredondar(controle, controle is Panel ? 12 : 8);
            controle.Invalidate();
        }
    }
}

