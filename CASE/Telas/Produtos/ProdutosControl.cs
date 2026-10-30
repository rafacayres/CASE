using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{
    public partial class ProdutosControl : UserControl
    {
        public ProdutosControl()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            CASE.Utils.FrontendTheme.Aplicar(this);

            VisualUtils.Arredondar(panelAcoesProdutos, 12);
            VisualUtils.Arredondar(panelListaProdutos, 12);
            VisualUtils.Arredondar(btnNovoProduto, 8);
            VisualUtils.Arredondar(btnPaginaAnterior, 8);
            VisualUtils.Arredondar(btnProximaPagina, 8);

            VisualUtils.AplicarBorda(panelAcoesProdutos);
            VisualUtils.AplicarBorda(panelListaProdutos);

            panelAcoesProdutos.SizeChanged += AjustarArredondamento;
            panelListaProdutos.SizeChanged += AjustarArredondamento;
            cmbFiltroCategoria.SelectedIndex = 0;
            dgvProdutos.Rows.Add("001", "Refrigerante", "Bebidas", "120", "6,00");
            dgvProdutos.Rows.Add("002", "Água mineral", "Bebidas", "80", "4,00");
            dgvProdutos.Rows.Add("003", "Cachorro-quente", "Alimentação", "65", "12,00");
            dgvProdutos.Rows.Add("004", "Bolo de milho", "Doces", "35", "5,00");
            colCodigo.Visible = false;
            var imagem = new DataGridViewImageColumn { Name = "colImagemProduto", HeaderText = "Imagem",
                ImageLayout = DataGridViewImageCellLayout.Zoom, FillWeight = 65 };
            imagem.DefaultCellStyle.NullValue = null;
            dgvProdutos.Columns.Insert(0, imagem);
            colAcoes.Text = "Editar";
            var excluir = new DataGridViewButtonColumn { Name = "colExcluir", HeaderText = "", Text = "Excluir",
                UseColumnTextForButtonValue = true, FlatStyle = FlatStyle.Flat, FillWeight = 65 };
            excluir.DefaultCellStyle.ForeColor = System.Drawing.Color.Firebrick;
            dgvProdutos.Columns.Add(excluir);
            lblPaginacao.Text = "1 a 4 de 4 produtos";
            cmbFiltroCategoria.Items.Clear();
            cmbFiltroCategoria.Items.AddRange(new object[] { "Todas as categorias", "Bebidas", "Alimentação", "Doces", "Brincadeiras" });
            cmbFiltroCategoria.SelectedIndex = 0;
            btnNovoProduto.Click += (s, e) => { using (var formulario = new ProdutoForm()) formulario.ShowDialog(this); };
            dgvProdutos.CellContentClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                if (dgvProdutos.Columns[e.ColumnIndex] == colAcoes)
                    using (var formulario = new ProdutoForm())
                    {
                        formulario.PreencherPreview(Convert.ToString(dgvProdutos.Rows[e.RowIndex].Cells[colProduto.Index].Value));
                        formulario.ShowDialog(this);
                    }
                else if (dgvProdutos.Columns[e.ColumnIndex] == excluir)
                    MessageBox.Show(this, "A exclusão será conectada ao backend posteriormente.", "Prévia do frontend");
            };
            EventHandler filtrar = (s, e) =>
            {
                int visiveis = 0;
                foreach (DataGridViewRow linha in dgvProdutos.Rows)
                {
                    linha.Visible = Convert.ToString(linha.Cells[colProduto.Index].Value).IndexOf(txtPesquisarProduto.Text, StringComparison.CurrentCultureIgnoreCase) >= 0
                        && (cmbFiltroCategoria.SelectedIndex <= 0 || Convert.ToString(linha.Cells[colCategoria.Index].Value) == cmbFiltroCategoria.Text);
                    if (linha.Visible) visiveis++;
                }
                lblPaginacao.Text = visiveis + " de 4 produtos";
            };
            txtPesquisarProduto.TextChanged += filtrar;
            cmbFiltroCategoria.SelectedIndexChanged += filtrar;
            btnPaginaAnterior.Enabled = btnProximaPagina.Enabled = false;
            dgvProdutos.ClearSelection();
        }

        private void AjustarArredondamento(object sender, EventArgs e)
        {
            Control controle = (Control)sender;
            VisualUtils.Arredondar(controle, 12);
            controle.Invalidate();
        }
    }
}
