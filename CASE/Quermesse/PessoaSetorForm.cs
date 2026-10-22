using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class PessoaSetorForm : Form
    {
        public PessoaSetorForm() : this(new QuermessePreview(), null) { }

        public PessoaSetorForm(QuermessePreview exemplos, PessoaPreview pessoa)
        {
            InitializeComponent();
            if (pessoa != null)
            {
                Text = lblTitulo.Text = "Funcionário e setor";
                txtNomeFuncionario.Text = pessoa.Nome;
                txtLoginFuncionario.Text = pessoa.Login;
                cmbPerfilFuncionario.SelectedItem = pessoa.Perfil.ToString();
                cmbSetorFuncionario.SelectedItem = pessoa.Setor;
                cmbTurnoFuncionario.SelectedItem = pessoa.Turno;
                chkFuncionarioAtivo.Checked = pessoa.Ativo;
            }
            FrontendTheme.Aplicar(this);
        }

        private void btnSalvarFuncionario_Click(object sender, EventArgs e)
        {
            labelAvisoFormulario.Text = "Prévia da interface: nenhuma alteração foi salva.";
        }
    }
}
