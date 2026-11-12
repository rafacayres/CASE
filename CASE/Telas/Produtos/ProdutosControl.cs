using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE
{
    public partial class ProdutosControl : UserControl
    {
        private readonly ProdutoService produtoService = new ProdutoService();
        private readonly CategoriaService categoriaService = new CategoriaService();
        private readonly Usuario usuarioAutenticado;
        private List<Produto> produtos = new List<Produto>();
        private List<Categoria> categorias = new List<Categoria>();
        private readonly List<System.Drawing.Image> imagensDaGrade = new List<System.Drawing.Image>();

        public ProdutosControl() : this(null)
        {
        }

        public ProdutosControl(Usuario usuario)
        {
            InitializeComponent();
            usuarioAutenticado = usuario;

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            FrontendTheme.Aplicar(this);
            VisualUtils.Arredondar(panelAcoesProdutos, 12);
            VisualUtils.Arredondar(panelListaProdutos, 12);
            VisualUtils.Arredondar(btnNovoProduto, 8);
            VisualUtils.Arredondar(btnPaginaAnterior, 8);
            VisualUtils.Arredondar(btnProximaPagina, 8);
            VisualUtils.AplicarBorda(panelAcoesProdutos);
            VisualUtils.AplicarBorda(panelListaProdutos);

            panelAcoesProdutos.SizeChanged += AjustarArredondamento;
            panelListaProdutos.SizeChanged += AjustarArredondamento;

            colCodigo.Visible = false;

            DataGridViewImageColumn colunaImagem = new DataGridViewImageColumn();
            colunaImagem.Name = "colImagemProduto";
            colunaImagem.HeaderText = "Imagem";
            colunaImagem.ImageLayout = DataGridViewImageCellLayout.Zoom;
            colunaImagem.FillWeight = 65;
            colunaImagem.DefaultCellStyle.NullValue = null;
            dgvProdutos.Columns.Insert(0, colunaImagem);

            colAcoes.Text = "Editar";

            DataGridViewButtonColumn colunaExcluir = new DataGridViewButtonColumn();
            colunaExcluir.Name = "colExcluir";
            colunaExcluir.HeaderText = "";
            colunaExcluir.Text = "Excluir";
            colunaExcluir.UseColumnTextForButtonValue = true;
            colunaExcluir.FlatStyle = FlatStyle.Flat;
            colunaExcluir.FillWeight = 65;
            colunaExcluir.DefaultCellStyle.ForeColor = System.Drawing.Color.Firebrick;
            // Não há exclusão de produtos com histórico implementada; não ofereça uma ação simulada.
            colunaExcluir.Visible = false;
            dgvProdutos.Columns.Add(colunaExcluir);

            btnNovoProduto.Click += BtnNovoProduto_Click;
            dgvProdutos.CellContentClick += DgvProdutos_CellContentClick;
            txtPesquisarProduto.TextChanged += Filtros_Changed;
            cmbFiltroCategoria.SelectedIndexChanged += Filtros_Changed;

            btnPaginaAnterior.Enabled = false;
            btnProximaPagina.Enabled = false;
            dgvProdutos.ClearSelection();

            VisualUtils.ExibirEstadoVazio(
                dgvProdutos,
                "Nenhum produto cadastrado.");

            if (usuarioAutenticado != null)
            {
                CarregarProdutos();
                try
                {
                    string perfil;
                    new AutorizacaoService().ValidarUsuarioEObterPerfil(usuarioAutenticado, out perfil);
                    btnNovoProduto.Enabled = perfil == "Administrador";
                    if (perfil != "Administrador")
                    {
                        colAcoes.Text = "Consultar";
                    }
                }
                catch (Exception)
                {
                    btnNovoProduto.Enabled = false;
                }
            }
            else
            {
                btnNovoProduto.Enabled = false;
                lblPaginacao.Text = "Entre no sistema para consultar os produtos.";
            }
            Disposed += (s, e) => LiberarImagensDaGrade();
        }

        private void CarregarProdutos()
        {
            if (dadosDemo != null)
            {
                AtivarDemonstracao(dadosDemo);
                return;
            }

            try
            {
                produtos = produtoService.Listar(usuarioAutenticado);
                categorias = categoriaService.Listar(usuarioAutenticado);
                CarregarFiltroDeCategorias();
                ExibirProdutos();
            }
            catch (Exception)
            {
                produtos = new List<Produto>();
                categorias = new List<Categoria>();
                dgvProdutos.Rows.Clear();
                lblPaginacao.Text = "Não foi possível carregar os produtos.";
            }
        }

        private void CarregarFiltroDeCategorias()
        {
            cmbFiltroCategoria.Items.Clear();
            cmbFiltroCategoria.Items.Add("Todas as categorias");

            foreach (Categoria categoria in categorias)
            {
                cmbFiltroCategoria.Items.Add(categoria.Nome);
            }

            cmbFiltroCategoria.SelectedIndex = 0;
        }

        private void ExibirProdutos()
        {
            dgvProdutos.Rows.Clear();
            LiberarImagensDaGrade();

            foreach (Produto produto in produtos)
            {
                string nomeCategoria = ObterNomeCategoria(produto.IdCategoria);

                object[] valores = new object[]
                {
                    CriarMiniatura(produto),
                    produto.IdProduto,
                    produto.Nome,
                    nomeCategoria,
                    "Não disponível nesta tela",
                    "Definido por setor"
                };

                int indiceLinha = dgvProdutos.Rows.Add(valores);
                dgvProdutos.Rows[indiceLinha].Tag = produto;
            }

            AplicarFiltros();
        }

        private System.Drawing.Image CriarMiniatura(Produto produto)
        {
            if (produto.Imagem == null || produto.Imagem.Length == 0)
            {
                return null;
            }
            try
            {
                using (var memoria = new System.IO.MemoryStream(produto.Imagem))
                using (var original = System.Drawing.Image.FromStream(memoria))
                {
                    double proporcao = Math.Min(64.0 / original.Width, 64.0 / original.Height);
                    var tamanho = new System.Drawing.Size(Math.Max(1, (int)(original.Width * proporcao)),
                        Math.Max(1, (int)(original.Height * proporcao)));
                    var miniatura = new System.Drawing.Bitmap(original, tamanho);
                    imagensDaGrade.Add(miniatura);
                    return miniatura;
                }
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        private void LiberarImagensDaGrade()
        {
            foreach (System.Drawing.Image imagem in imagensDaGrade)
            {
                imagem.Dispose();
            }
            imagensDaGrade.Clear();
        }

        private string ObterNomeCategoria(int idCategoria)
        {
            foreach (Categoria categoria in categorias)
            {
                if (categoria.IdCategoria == idCategoria)
                {
                    return categoria.Nome;
                }
            }

            return "Categoria não encontrada";
        }

        private void AplicarFiltros()
        {
            dgvProdutos.CurrentCell = null;

            int quantidadeVisivel = 0;
            string textoBusca = txtPesquisarProduto.Text.Trim();

            foreach (DataGridViewRow linha in dgvProdutos.Rows)
            {
                Produto produto = linha.Tag as Produto;

                if (produto == null)
                {
                    linha.Visible = false;
                    continue;
                }

                string nomeCategoria = Convert.ToString(linha.Cells[colCategoria.Index].Value);
                bool nomeCorresponde = produto.Nome.IndexOf(
                    textoBusca,
                    StringComparison.CurrentCultureIgnoreCase) >= 0;
                bool categoriaCorresponde = cmbFiltroCategoria.SelectedIndex <= 0 ||
                    string.Equals(
                        nomeCategoria,
                        cmbFiltroCategoria.Text,
                        StringComparison.CurrentCultureIgnoreCase);

                linha.Visible = nomeCorresponde && categoriaCorresponde;

                if (linha.Visible)
                {
                    quantidadeVisivel++;
                }
            }

            lblPaginacao.Text = quantidadeVisivel + " de " + produtos.Count + " produtos";
            dgvProdutos.ClearSelection();
        }

        private void AbrirFormularioDeProduto(Produto produto)
        {
            if (dadosDemo != null)
            {
                AbrirProdutoDemo(produto);
                return;
            }

            using (ProdutoForm formulario = new ProdutoForm(usuarioAutenticado, produto))
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    CarregarProdutos();
                }
            }
        }

        private void BtnNovoProduto_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                AbrirProdutoDemo(null);
                return;
            }

            using (ProdutoForm formulario = new ProdutoForm(usuarioAutenticado, null))
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    CarregarProdutos();
                }
            }
        }

        private void DgvProdutos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            Produto produto = dgvProdutos.Rows[e.RowIndex].Tag as Produto;

            if (produto == null)
            {
                return;
            }

            if (dgvProdutos.Columns[e.ColumnIndex] == colAcoes)
            {
                AbrirFormularioDeProduto(produto);
                return;
            }

            if (dgvProdutos.Columns[e.ColumnIndex].Name == "colExcluir")
            {
                MessageBox.Show(
                    this,
                    "A exclusão não está disponível para produtos nesta etapa.",
                    "Produtos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void Filtros_Changed(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        public void Pesquisar(string texto)
        {
            if (texto == null)
            {
                texto = string.Empty;
            }

            txtPesquisarProduto.Text = texto;
        }

        private void AjustarArredondamento(object sender, EventArgs e)
        {
            Control controle = (Control)sender;
            VisualUtils.Arredondar(controle, 12);
            controle.Invalidate();
        }
    }
}
