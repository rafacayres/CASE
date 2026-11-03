using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class QuermesseForm : Form
    {
        public QuermesseForm() : this(false) { }

        public QuermesseForm(bool editar)
        {
            InitializeComponent();
            if (editar)
            {
                Text = lblTitulo.Text = "Editar evento";
                txtNomeQuermesse.Clear();
                txtLocalQuermesse.Text = "Pátio da comunidade";
                txtResponsavelQuermesse.Clear();
                cmbSituacaoQuermesse.SelectedIndex = 1;
            }
            FrontendTheme.Aplicar(this);
        }

        private void btnSalvarVisual_Click(object sender, EventArgs e)
        {
            labelAvisoFormulario.Text = "Prévia da interface: nenhuma alteração foi salva.";
        }
    }
}
