using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class QuermessesControl : UserControl
    {
        public QuermessesControl() : this(new QuermessePreview()) { }

        public QuermessesControl(QuermessePreview exemplos)
        {
            InitializeComponent();
            FrontendTheme.Aplicar(this);
            CarregarExemplos();
        }

        private void btnNovaQuermesse_Click(object sender, EventArgs e)
        {
            using (var formulario = new QuermesseForm()) formulario.ShowDialog(this);
        }

        private void btnEditarQuermesse_Click(object sender, EventArgs e)
        {
            using (var formulario = new QuermesseForm(true)) formulario.ShowDialog(this);
        }

        // Exemplos visuais; sem persistência ou regras de negócio.
        private void CarregarExemplos()
        {
            dgvQuermesses.Rows.Add(new object[] { "São João 2026", "Quermesse", "Pátio da comunidade", "22/10/2026", "25/10/2026", "Rafael Cayres", "Em andamento" });
            dgvQuermesses.ClearSelection();
        }
    }
}
