using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{
    public partial class ProdutoForm : Form
    {
        public ProdutoForm()
        {
            InitializeComponent();
            FrontendTheme.Aplicar(this);
            VisualUtils.Arredondar(pictureImagemProduto, 10);
        }

        public void PreencherPreview(string nome)
        {
            lblTituloProduto.Text = "Editar Produto";
            txtNomeProduto.Text = nome;
            cmbCategoriaProduto.SelectedIndex = 1;
        }

        private void btnSalvarProduto_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Esta é uma prévia visual. Nenhum dado foi gravado.", "Cadastro de Produto");
        }
    }
}