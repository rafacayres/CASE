using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class EscalaForm : Form
    {
        public EscalaForm() : this(new QuermessePreview()) { }

        public EscalaForm(QuermessePreview exemplos)
        {
            InitializeComponent();
            cmbEventoEscala.Items.Clear();
            cmbEventoEscala.Items.Add(exemplos.Evento);
            cmbEventoEscala.SelectedIndex = 0;
            FrontendTheme.Aplicar(this);
        }

        private void btnSalvarVisual_Click(object sender, EventArgs e)
        {
            labelAvisoFormulario.Text = "Prévia da interface: nenhuma alteração foi salva.";
        }
    }
}
