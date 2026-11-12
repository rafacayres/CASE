using System;
using System.Windows.Forms;
using CASE.Utils;
using CASE.Models;
using CASE.Services;
using System.Data;

namespace CASE
{
    public partial class MovimentacoesControl : UserControl
    {
        private readonly Usuario usuarioAutenticado;

        public MovimentacoesControl() : this(null)
        {
        }

        public MovimentacoesControl(Usuario usuario)
        {
            InitializeComponent();
            usuarioAutenticado = usuario;
            CASE.Utils.FrontendTheme.Aplicar(this);
            VisualUtils.Arredondar(panelCardEntradasMes, 12);
            panelCardEntradasMes.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelCardEntradasMes);
            VisualUtils.Arredondar(panelCardSaidasMes, 12);
            panelCardSaidasMes.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelCardSaidasMes);
            VisualUtils.Arredondar(panelFiltrosMovimentacoes, 12);
            panelFiltrosMovimentacoes.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelFiltrosMovimentacoes);
            VisualUtils.Arredondar(btnNovaMovimentacao, 8);
            btnNovaMovimentacao.SizeChanged += AjustarArredondamento;
            VisualUtils.Arredondar(panelListaMovimentacoes, 12);
            panelListaMovimentacoes.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelListaMovimentacoes);
            lblSubtituloMovimentacoes.Text = "Histórico de entradas e saídas";
            panelCardEntradasMes.Visible = panelCardSaidasMes.Visible = false;
            panelFiltrosMovimentacoes.Top = 115;
            panelListaMovimentacoes.Top = 225;
            panelListaMovimentacoes.Height = 340;
            dgvMovimentacoes.Height = 260;
            btnNovaMovimentacao.Text = "Filtrar";
            dgvMovimentacoesColMotivo.Visible = false;
            dtpDataInicial.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpDataFinal.Value = DateTime.Today;
            cmbTipoMovimentacao.Items.Clear();
            cmbTipoMovimentacao.Items.AddRange(new object[] { "Todos", "entrada", "transferencia", "venda", "perda" });
            cmbTipoMovimentacao.SelectedIndex = 0;
            foreach (Control campo in new Control[] { cmbTipoMovimentacao, txtProdutoMovimentacao, dtpDataInicial, dtpDataFinal })
            {
                string titulo = campo == cmbTipoMovimentacao ? "Tipo" : campo == txtProdutoMovimentacao ? "Produto" : campo == dtpDataInicial ? "Data inicial" : "Data final";
                panelFiltrosMovimentacoes.Controls.Add(FrontendTheme.Legenda(titulo,
                    new System.Drawing.Rectangle(campo.Left, 7, campo.Width, 20)));
            }
            btnNovaMovimentacao.Click += (s, e) =>
            {
                CarregarMovimentacoes();
                foreach (DataGridViewRow linha in dgvMovimentacoes.Rows)
                {
                    DateTime data = DateTime.ParseExact(Convert.ToString(linha.Cells[0].Value), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    linha.Visible = (cmbTipoMovimentacao.SelectedIndex <= 0 || Convert.ToString(linha.Cells[2].Value) == cmbTipoMovimentacao.Text)
                        && Convert.ToString(linha.Cells[1].Value).IndexOf(txtProdutoMovimentacao.Text, StringComparison.CurrentCultureIgnoreCase) >= 0
                        && data >= dtpDataInicial.Value.Date && data <= dtpDataFinal.Value.Date;
                }
                dgvMovimentacoes.ClearSelection();
            };
            txtProdutoMovimentacao.TextChanged += (s, e) => FiltrarProduto();
            CarregarMovimentacoes();
        }

        private void CarregarMovimentacoes()
        {
            if (dadosDemo != null)
            {
                CarregarMovimentosDemo();
                return;
            }

            dgvMovimentacoes.Rows.Clear();
            if (usuarioAutenticado == null)
            {
                lblListaMovimentacoes.Text = "Autentique-se para consultar o histórico.";
                btnNovaMovimentacao.Enabled = false;
                return;
            }
            try
            {
                DataTable movimentos = new ConsultaOperacionalService().Movimentacoes(
                    usuarioAutenticado, dtpDataInicial.Value, dtpDataFinal.Value);
                foreach (DataRow movimento in movimentos.Rows)
                {
                    dgvMovimentacoes.Rows.Add(Convert.ToDateTime(movimento["data_hora"]).ToString("dd/MM/yyyy"),
                        movimento["produto"], movimento["tipo"], movimento["quantidade"], movimento["responsavel"],
                        movimento["origem"] + " → " + movimento["destino"]);
                }
                dgvMovimentacoesColMotivo.Visible = true;
                dgvMovimentacoesColMotivo.HeaderText = "Origem → destino";
                lblListaMovimentacoes.Text = movimentos.Rows.Count + " movimentação(ões) autorizada(s) no período";
                FiltrarProduto();
            }
            catch (Exception erro)
            {
                lblListaMovimentacoes.Text = "Não foi possível consultar o histórico.";
                if (erro is ArgumentException || erro is UnauthorizedAccessException)
                {
                    lblListaMovimentacoes.Text = erro.Message;
                }
                System.Diagnostics.Trace.WriteLine(erro.GetType().Name + " em MovimentacoesControl: " + erro.StackTrace);
            }
        }

        private void FiltrarProduto()
        {
            foreach (DataGridViewRow linha in dgvMovimentacoes.Rows)
            {
                linha.Visible = Convert.ToString(linha.Cells[1].Value).IndexOf(
                    txtProdutoMovimentacao.Text.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0 &&
                    (cmbTipoMovimentacao.SelectedIndex <= 0 || Convert.ToString(linha.Cells[2].Value) == cmbTipoMovimentacao.Text);
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

