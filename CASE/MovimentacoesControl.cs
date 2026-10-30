using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{
    public partial class MovimentacoesControl : UserControl
    {
        public MovimentacoesControl()
        {
            InitializeComponent();
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
            dgvMovimentacoes.Rows.Add("06/10/2026", "Notebook Dell", "Entrada", "5", "Rafael", "Compra");
            dgvMovimentacoes.Rows.Add("05/10/2026", "Mouse Logitech", "Saída", "2", "Rafael", "Venda");
            dgvMovimentacoes.Rows.Add("04/10/2026", "Teclado Mecânico", "Entrada", "10", "Rafael", "Compra");
            dgvMovimentacoes.Rows.Add("03/10/2026", "Monitor LG", "Saída", "1", "Rafael", "Venda");
            dtpDataInicial.Value = new DateTime(2026, 10, 1);
            dtpDataFinal.Value = new DateTime(2026, 10, 31);
            cmbTipoMovimentacao.SelectedIndex = 0;
            foreach (Control campo in new Control[] { cmbTipoMovimentacao, txtProdutoMovimentacao, dtpDataInicial, dtpDataFinal })
            {
                string titulo = campo == cmbTipoMovimentacao ? "Tipo" : campo == txtProdutoMovimentacao ? "Produto" : campo == dtpDataInicial ? "Data inicial" : "Data final";
                panelFiltrosMovimentacoes.Controls.Add(FrontendTheme.Legenda(titulo,
                    new System.Drawing.Rectangle(campo.Left, 7, campo.Width, 20)));
            }
            btnNovaMovimentacao.Click += (s, e) =>
            {
                foreach (DataGridViewRow linha in dgvMovimentacoes.Rows)
                {
                    DateTime data = DateTime.ParseExact(Convert.ToString(linha.Cells[0].Value), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    linha.Visible = (cmbTipoMovimentacao.SelectedIndex <= 0 || Convert.ToString(linha.Cells[2].Value) == cmbTipoMovimentacao.Text)
                        && Convert.ToString(linha.Cells[1].Value).IndexOf(txtProdutoMovimentacao.Text, StringComparison.CurrentCultureIgnoreCase) >= 0
                        && data >= dtpDataInicial.Value.Date && data <= dtpDataFinal.Value.Date;
                }
                dgvMovimentacoes.ClearSelection();
            };
        }

        private void AjustarArredondamento(object sender, EventArgs e)
        {
            Control controle = (Control)sender;
            VisualUtils.Arredondar(controle, controle is Panel ? 12 : 8);
            controle.Invalidate();
        }
    }
}

