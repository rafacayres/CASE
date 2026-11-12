using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE
{
    public partial class ProdutoForm : Form
    {
        private readonly Usuario usuarioAutenticado;
        private readonly Produto produtoEmEdicao;
        private readonly ProdutoService produtoService = new ProdutoService();
        private readonly CategoriaService categoriaService = new CategoriaService();
        private readonly ContextMenuStrip menuImagem = new ContextMenuStrip();
        private byte[] imagemProduto;

        public ProdutoForm() : this(null, null)
        {
        }

        public ProdutoForm(Usuario usuario, Produto produto)
        {
            InitializeComponent();
            IconoAplicacao.Aplicar(this);
            FrontendTheme.Aplicar(this);
            VisualUtils.Arredondar(pictureImagemProduto, 10);
            usuarioAutenticado = usuario;
            produtoEmEdicao = produto;

            // Preço e estoque são mantidos por setor e local, não pelo cadastro geral do produto.
            cmbFornecedorProduto.Enabled = false;
            numQuantidadeInicial.Enabled = false;
            numEstoqueMinimo.Enabled = false;
            numEstoqueIdeal.Enabled = false;
            txtPrecoUnitario.Enabled = false;
            lblSubtituloProduto.Text =
                "Dados gerais do produto. Preços e estoque são configurados nos módulos próprios.";
            ConfigurarSelecaoDeImagem();

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            if (usuarioAutenticado == null)
            {
                btnSalvarProduto.Enabled = false;
                lblSubtituloProduto.Text = "Entre no sistema para cadastrar ou editar produtos.";
                return;
            }

            CarregarCategorias();

            if (produtoEmEdicao != null)
            {
                PreencherProduto(produtoEmEdicao);
            }
            try
            {
                string perfil;
                new AutorizacaoService().ValidarUsuarioEObterPerfil(usuarioAutenticado, out perfil);
                if (perfil != "Administrador")
                {
                    btnSalvarProduto.Enabled = false;
                    txtNomeProduto.ReadOnly = true;
                    txtDescricaoProduto.ReadOnly = true;
                    cmbCategoriaProduto.Enabled = false;
                    pictureImagemProduto.Enabled = false;
                    lblSubtituloProduto.Text = "Consulta do catálogo. O cadastro geral de produtos é administrado pelo administrador.";
                }
            }
            catch (Exception)
            {
                btnSalvarProduto.Enabled = false;
            }
        }

        public void PreencherPreview(Produto produto)
        {
            PreencherProduto(produto);
        }

        private void PreencherProduto(Produto produto)
        {
            if (produto == null)
            {
                return;
            }

            lblTituloProduto.Text = "Editar Produto";
            txtNomeProduto.Text = produto.Nome;
            txtDescricaoProduto.Text = produto.Descricao;
            imagemProduto = produto.Imagem;
            ExibirImagemProduto(imagemProduto);

            foreach (Categoria categoria in cmbCategoriaProduto.Items)
            {
                if (categoria.IdCategoria == produto.IdCategoria)
                {
                    cmbCategoriaProduto.SelectedItem = categoria;
                    break;
                }
            }
        }

        private void btnSalvarProduto_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                SalvarProdutoDemo();
                return;
            }

            Categoria categoria = cmbCategoriaProduto.SelectedItem as Categoria;

            if (categoria == null)
            {
                MessageBox.Show(
                    this,
                    "Selecione uma categoria.",
                    "Cadastro de produto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            Produto produto;

            if (produtoEmEdicao == null)
            {
                produto = new Produto();
                produto.Ativo = true;
            }
            else
            {
                produto = new Produto();
                produto.IdProduto = produtoEmEdicao.IdProduto;
                produto.Ativo = produtoEmEdicao.Ativo;
                produto.Imagem = produtoEmEdicao.Imagem;
            }

            produto.IdCategoria = categoria.IdCategoria;
            produto.Nome = txtNomeProduto.Text.Trim();
            produto.Descricao = txtDescricaoProduto.Text.Trim();
            produto.UnidadeMedida = "UN";
            produto.Imagem = imagemProduto;

            try
            {
                if (produtoEmEdicao == null)
                {
                    produtoService.Cadastrar(usuarioAutenticado, produto);
                }
                else
                {
                    produtoService.Atualizar(usuarioAutenticado, produto);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(this, ex.Message, "Cadastro de produto",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(this, ex.Message, "Cadastro de produto",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(this, "Sua conta não tem permissão para alterar produtos.",
                    "Cadastro de produto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Não foi possível salvar o produto.",
                    "Cadastro de produto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarSelecaoDeImagem()
        {
            ToolStripMenuItem selecionarImagem = new ToolStripMenuItem("Selecionar imagem");
            selecionarImagem.Click += SelecionarImagem_Click;

            ToolStripMenuItem removerImagem = new ToolStripMenuItem("Remover imagem");
            removerImagem.Click += RemoverImagem_Click;

            menuImagem.Items.Add(selecionarImagem);
            menuImagem.Items.Add(removerImagem);
            menuImagem.Opening += MenuImagem_Opening;

            pictureImagemProduto.ContextMenuStrip = menuImagem;
            pictureImagemProduto.Cursor = Cursors.Hand;
            pictureImagemProduto.Click += PictureImagemProduto_Click;
            lblImagemdoproduto2.Cursor = Cursors.Hand;
            lblImagemdoproduto2.Click += PictureImagemProduto_Click;
        }

        private void PictureImagemProduto_Click(object sender, EventArgs e)
        {
            menuImagem.Show(pictureImagemProduto, new Point(0, pictureImagemProduto.Height));
        }

        private void MenuImagem_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            menuImagem.Items[1].Enabled = imagemProduto != null && imagemProduto.Length > 0;
        }

        private void SelecionarImagem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog seletor = new OpenFileDialog())
            {
                seletor.Title = "Selecionar imagem do produto";
                seletor.Filter = "Imagens JPG ou PNG|*.jpg;*.jpeg;*.png";
                seletor.CheckFileExists = true;
                seletor.Multiselect = false;

                if (seletor.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    byte[] dados = File.ReadAllBytes(seletor.FileName);
                    imagemProduto = ValidarEPrepararImagem(dados);
                    ExibirImagemProduto(imagemProduto);
                }
                catch (ArgumentException erro)
                {
                    MessageBox.Show(this, erro.Message, "Imagem do produto",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Não foi possível abrir a imagem selecionada.",
                        "Imagem do produto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private byte[] ValidarEPrepararImagem(byte[] dados)
        {
            if (dados == null || dados.Length == 0)
            {
                throw new ArgumentException("Selecione um arquivo de imagem válido.");
            }

            using (MemoryStream fluxo = new MemoryStream(dados))
            using (Image imagem = Image.FromStream(fluxo, true, true))
            {
                Guid formato = imagem.RawFormat.Guid;
                if (formato != ImageFormat.Jpeg.Guid && formato != ImageFormat.Png.Guid)
                {
                    throw new ArgumentException("O arquivo deve ser uma imagem JPG ou PNG válida.");
                }

                using (MemoryStream copia = new MemoryStream())
                {
                    imagem.Save(copia, imagem.RawFormat);
                    return copia.ToArray();
                }
            }
        }

        private void ExibirImagemProduto(byte[] dados)
        {
            Image imagemAnterior = pictureImagemProduto.Image;
            pictureImagemProduto.Image = null;

            if (imagemAnterior != null)
            {
                imagemAnterior.Dispose();
            }

            if (dados == null || dados.Length == 0)
            {
                lblImagemdoproduto2.Visible = true;
                return;
            }

            using (MemoryStream fluxo = new MemoryStream(dados))
            using (Image imagem = Image.FromStream(fluxo, true, true))
            {
                pictureImagemProduto.Image = new Bitmap(imagem);
            }

            lblImagemdoproduto2.Visible = false;
        }

        private void RemoverImagem_Click(object sender, EventArgs e)
        {
            imagemProduto = null;
            ExibirImagemProduto(null);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (pictureImagemProduto.Image != null)
            {
                pictureImagemProduto.Image.Dispose();
                pictureImagemProduto.Image = null;
            }

            menuImagem.Dispose();
            base.OnFormClosed(e);
        }

        private void CarregarCategorias()
        {
            cmbCategoriaProduto.Items.Clear();
            cmbCategoriaProduto.DisplayMember = "Nome";

            try
            {
                List<Categoria> categorias = categoriaService.Listar(usuarioAutenticado);

                foreach (Categoria categoria in categorias)
                {
                    cmbCategoriaProduto.Items.Add(categoria);
                }

                if (cmbCategoriaProduto.Items.Count > 0)
                {
                    cmbCategoriaProduto.SelectedIndex = 0;
                }
                else
                {
                    btnSalvarProduto.Enabled = false;
                }
            }
            catch (Exception)
            {
                btnSalvarProduto.Enabled = false;
                MessageBox.Show(
                    this,
                    "Não foi possível carregar as categorias.",
                    "Cadastro de produto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
