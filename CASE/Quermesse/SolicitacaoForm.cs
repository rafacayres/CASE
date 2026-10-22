using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class SolicitacaoForm : Form
    {
        public SolicitacaoForm() : this(false) { }

        public SolicitacaoForm(bool detalhe)
        {
            InitializeComponent();
            if (detalhe)
            {
                Text = lblTitulo.Text = "Detalhes da solicitação";
                txtSolicitante.Text = "Marina Souza";
                txtProdutoSolicitacao.Text = "Água mineral";
                txtQuantidadeSolicitacao.Text = "20";
                txtObservacaoSolicitacao.Text = "Reposição para o turno da noite";
            }
            FrontendTheme.Aplicar(this);
        }

        private void btnSalvarVisual_Click(object sender, EventArgs e)
        {
            labelAvisoFormulario.Text = "Prévia da interface: nenhuma alteração foi salva.";
        }
    }
}
