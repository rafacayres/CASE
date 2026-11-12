using System;
using System.Windows.Forms;
using CASE.Utils;
using System.Data;
using System.IO;
using System.Text;
using CASE.Models;
using CASE.Services;

namespace CASE
{
    public partial class RelatoriosControl : UserControl
    {
        private readonly Usuario usuarioAutenticado;
        private readonly DataGridView gradeRelatorio = new DataGridView();
        private readonly Label resumoRelatorio = new Label();
        private DataTable dadosRelatorio;

        public RelatoriosControl() : this(null)
        {
        }

        public RelatoriosControl(Usuario usuario)
        {
            InitializeComponent();
            usuarioAutenticado = usuario;
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
            gradeRelatorio.Dock = DockStyle.Fill;
            gradeRelatorio.ReadOnly = true;
            gradeRelatorio.AllowUserToAddRows = false;
            gradeRelatorio.AllowUserToDeleteRows = false;
            gradeRelatorio.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            panelGraficoEsquerdoArea.Controls.Add(gradeRelatorio);
            resumoRelatorio.Dock = DockStyle.Fill;
            resumoRelatorio.AutoEllipsis = true;
            panelGraficoDireitoArea.Controls.Add(resumoRelatorio);
            lblGraficoEsquerdo.Text = "Dados autorizados";
            lblGraficoDireito.Text = "Resumo da consulta";
            btnExportarPdf.Text = "Gerar";
            btnExportarCsv.Visible = true;
            btnExportarCsv.Text = "Exportar...";
            btnExportarCsv.Enabled = false;
            btnExportarPdf.Click += GerarRelatorio;
            btnExportarCsv.Click += MostrarExportacoes;
            panelCardRelBaixo.Visible = panelCardRelSemEstoque.Visible = panelCardRelValor.Visible = panelCardRelSugestao.Visible = false;
            panelGraficoEsquerdo.Top = panelGraficoDireito.Top = 215;
            panelGraficoEsquerdo.Height = panelGraficoDireito.Height = 330;
            panelGraficoEsquerdoArea.Height = panelGraficoDireitoArea.Height = 250;
            cmbRelatorioCategoria.Items.Clear();
            cmbRelatorioCategoria.Items.AddRange(new object[] { "Movimentações", "Estoque" });
            cmbRelatorioCategoria.SelectedIndex = 0;
            dtpRelatorioInicio.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpRelatorioFim.Value = DateTime.Today;
            foreach (Control campo in new Control[] { cmbRelatorioCategoria, dtpRelatorioInicio, dtpRelatorioFim })
            {
                campo.Top = 30;
                panelAcoesRelatorios.Controls.Add(FrontendTheme.Legenda(campo == cmbRelatorioCategoria ? "Tipo de relatório" : campo == dtpRelatorioInicio ? "Data inicial" : "Data final",
                    new System.Drawing.Rectangle(campo.Left, 6, campo.Width, 20)));
            }
            if (usuario != null)
            {
                GerarRelatorio(this, EventArgs.Empty);
            }
            else
            {
                resumoRelatorio.Text = "Autentique-se para consultar relatórios.";
                btnExportarPdf.Enabled = false;
            }
        }

        private void GerarRelatorio(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                GerarRelatorioDemo();
                return;
            }

            btnExportarCsv.Enabled = false;
            gradeRelatorio.DataSource = null;
            dadosRelatorio = null;
            try
            {
                ConsultaOperacionalService consulta = new ConsultaOperacionalService();
                if (cmbRelatorioCategoria.SelectedIndex == 1)
                {
                    dadosRelatorio = consulta.Estoques(usuarioAutenticado);
                    resumoRelatorio.Text = "Saldo atual por produto e local. O filtro de datas não se aplica ao saldo atual.";
                }
                else
                {
                    dadosRelatorio = consulta.Movimentacoes(usuarioAutenticado,
                        dtpRelatorioInicio.Value, dtpRelatorioFim.Value);
                    resumoRelatorio.Text = "Histórico do período. Transferências são exibidas uma vez, com origem e destino.";
                }
                gradeRelatorio.DataSource = dadosRelatorio;
                resumoRelatorio.Text += Environment.NewLine + Environment.NewLine +
                    dadosRelatorio.Rows.Count + " linha(s) autorizada(s).";
                btnExportarCsv.Enabled = dadosRelatorio.Rows.Count > 0;
            }
            catch (Exception erro)
            {
                if (erro is ArgumentException || erro is UnauthorizedAccessException)
                {
                    resumoRelatorio.Text = erro.Message;
                }
                else
                {
                    resumoRelatorio.Text = "Não foi possível consultar os dados do relatório.";
                    System.Diagnostics.Trace.WriteLine(erro.GetType().Name + " em RelatoriosControl: " + erro.StackTrace);
                }
            }
        }

        private void ExportarCsv(object sender, EventArgs e)
        {
            // Revalida a autorização para não exportar dados de uma sessão revogada.
            GerarRelatorio(sender, e);
            if (dadosRelatorio == null || dadosRelatorio.Rows.Count == 0)
            {
                return;
            }
            using (SaveFileDialog dialogo = new SaveFileDialog())
            {
                dialogo.Filter = "CSV compatível com Excel (*.csv)|*.csv";
                dialogo.FileName = "CASE_relatorio_" + DateTime.Today.ToString("yyyyMMdd") + ".csv";
                if (dadosDemo != null)
                {
                    dialogo.FileName = "CASE_DEMONSTRACAO_" + DateTime.Today.ToString("yyyyMMdd") + ".csv";
                }
                if (dialogo.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                try
                {
                    if (dadosDemo != null)
                    {
                        ExportadorRelatorio.ExportarCsv(dadosRelatorio, dialogo.FileName);
                    }
                    else
                    {
                        new ConsultaOperacionalService().ExportarCsv(usuarioAutenticado,
                            cmbRelatorioCategoria.SelectedIndex == 1, dtpRelatorioInicio.Value,
                            dtpRelatorioFim.Value, dialogo.FileName);
                    }
                    resumoRelatorio.Text = "Relatório exportado para CSV.";
                }
                catch (Exception erro)
                {
                    resumoRelatorio.Text = "Não foi possível salvar o arquivo. Verifique a pasta e se ele está aberto.";
                    System.Diagnostics.Trace.WriteLine(erro.GetType().Name + " em ExportarCsv");
                }
            }
        }

        private void MostrarExportacoes(object sender, EventArgs e)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem csv = new ToolStripMenuItem("CSV compatível com Excel");
            ToolStripMenuItem pdf = new ToolStripMenuItem("PDF");
            csv.Click += ExportarCsv;
            pdf.Click += ExportarPdf;
            menu.Items.Add(csv);
            menu.Items.Add(pdf);
            menu.Closed += (s, argumentos) => BeginInvoke(new Action(menu.Dispose));
            menu.Show(btnExportarCsv, new System.Drawing.Point(0, btnExportarCsv.Height));
        }

        private void ExportarPdf(object sender, EventArgs e)
        {
            using (SaveFileDialog dialogo = new SaveFileDialog())
            {
                dialogo.Filter = "Documento PDF (*.pdf)|*.pdf";
                dialogo.FileName = "CASE_relatorio_" + DateTime.Today.ToString("yyyyMMdd") + ".pdf";
                if (dadosDemo != null)
                {
                    dialogo.FileName = "CASE_DEMONSTRACAO_" + DateTime.Today.ToString("yyyyMMdd") + ".pdf";
                }
                if (dialogo.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                btnExportarCsv.Enabled = false;
                try
                {
                    if (dadosDemo != null)
                    {
                        GerarRelatorioDemo();
                        ExportadorRelatorio.ExportarPdf(dadosRelatorio, dialogo.FileName);
                    }
                    else
                    {
                        new ConsultaOperacionalService().ExportarPdf(usuarioAutenticado,
                            cmbRelatorioCategoria.SelectedIndex == 1, dtpRelatorioInicio.Value,
                            dtpRelatorioFim.Value, dialogo.FileName);
                    }
                    resumoRelatorio.Text = "Relatório enviado para o arquivo PDF selecionado.";
                }
                catch (Exception erro)
                {
                    resumoRelatorio.Text = "Não foi possível gerar o PDF.";
                    if (erro is ArgumentException || erro is InvalidOperationException || erro is UnauthorizedAccessException)
                    {
                        resumoRelatorio.Text = erro.Message;
                    }
                    System.Diagnostics.Trace.WriteLine(erro.GetType().Name + " em ExportarPdf");
                }
                finally
                {
                    btnExportarCsv.Enabled = dadosRelatorio != null && dadosRelatorio.Rows.Count > 0;
                }
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

