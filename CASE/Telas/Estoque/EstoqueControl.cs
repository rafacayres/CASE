using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{
    public partial class EstoqueControl : UserControl
    {
        public EstoqueControl()
        {
            InitializeComponent();
            CASE.Utils.FrontendTheme.Aplicar(this);
            VisualUtils.Arredondar(panelCardEstoqueTotal, 12);
            panelCardEstoqueTotal.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelCardEstoqueTotal);
            VisualUtils.Arredondar(panelCardEstoqueBaixo, 12);
            panelCardEstoqueBaixo.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelCardEstoqueBaixo);
            VisualUtils.Arredondar(panelCardSemEstoque, 12);
            panelCardSemEstoque.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelCardSemEstoque);
            VisualUtils.Arredondar(panelCardValorEstoque, 12);
            panelCardValorEstoque.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelCardValorEstoque);
            VisualUtils.Arredondar(panelFiltrosEstoque, 12);
            panelFiltrosEstoque.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelFiltrosEstoque);
            VisualUtils.Arredondar(btnAtualizarEstoque, 8);
            btnAtualizarEstoque.SizeChanged += AjustarArredondamento;
            VisualUtils.Arredondar(panelListaEstoque, 12);
            panelListaEstoque.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelListaEstoque);
            panelCardEstoqueTotal.Visible = panelCardEstoqueBaixo.Visible = false;
            panelCardSemEstoque.Visible = panelCardValorEstoque.Visible = false;
            panelFiltrosEstoque.Top = 115;
            panelListaEstoque.Top = 210;
            panelListaEstoque.Height = 380;
            dgvEstoque.Height = 300;
            lblSubtituloEstoque.Text = "Controle o estoque dos produtos";
            txtPesquisarEstoque.Visible = cmbCategoriaEstoque.Visible = cmbStatusEstoque.Visible = false;
            btnAtualizarEstoque.Visible = false;
            dgvEstoqueColCodigo.Visible = dgvEstoqueColIdeal.Visible = false;
            dgvEstoque.Rows.Add("001", "Refrigerante", "Bebidas", "120", "30", "150", "Em estoque");
            dgvEstoque.Rows.Add("002", "Água mineral", "Bebidas", "8", "20", "100", "Estoque baixo");
            dgvEstoque.Rows.Add("003", "Cachorro-quente", "Alimentação", "5", "10", "80", "Estoque baixo");
            dgvEstoque.Rows.Add("004", "Bolo de milho", "Doces", "2", "5", "40", "Estoque baixo");
            dgvEstoque.Rows.Add("005", "Paçoca", "Doces", "0", "10", "50", "Sem estoque");
            string[] filtros = { "Todos", "Em estoque", "Estoque baixo", "Sem estoque" };
            var botoes = new System.Collections.Generic.List<Button>();
            for (int i = 0; i < filtros.Length; i++)
            {
                string filtro = filtros[i];
                var botao = new Button { Name = "btnFiltroEstoque" + i, Text = filtro,
                    Bounds = new System.Drawing.Rectangle(16 + i * 150, 18, 140, 36),
                    FlatStyle = FlatStyle.Flat, BackColor = System.Drawing.Color.White, ForeColor = FrontendTheme.Texto };
                botao.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(221, 230, 240);
                botoes.Add(botao);
                panelFiltrosEstoque.Controls.Add(botao);
                botao.Click += (s, e) =>
                {
                    foreach (var item in botoes)
                    {
                        item.BackColor = item == botao ? System.Drawing.Color.FromArgb(226, 238, 255) : System.Drawing.Color.White;
                        item.ForeColor = item == botao ? FrontendTheme.Azul : FrontendTheme.Texto;
                    }
                    foreach (DataGridViewRow linha in dgvEstoque.Rows)
                        linha.Visible = filtro == "Todos" || Convert.ToString(linha.Cells[dgvEstoqueColStatus.Index].Value) == filtro;
                    dgvEstoque.ClearSelection();
                };
            }
            FrontendTheme.Aplicar(panelFiltrosEstoque);
            botoes[0].PerformClick();
        }

        private void AjustarArredondamento(object sender, EventArgs e)
        {
            Control controle = (Control)sender;
            VisualUtils.Arredondar(controle, controle is Panel ? 12 : 8);
            controle.Invalidate();
        }
    }
}

