using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class EscalaControl : UserControl
    {
        private readonly QuermessePreview exemplos;

        public EscalaControl() : this(new QuermessePreview()) { }

        public EscalaControl(QuermessePreview exemplos)
        {
            this.exemplos = exemplos;
            InitializeComponent();
            FrontendTheme.Aplicar(this);
            CarregarExemplos();
        }

        private void btnNovaAlocacao_Click(object sender, EventArgs e)
        {
            using (var formulario = new EscalaForm(exemplos)) formulario.ShowDialog(this);
        }

        private void Filtros_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvEscala.CurrentCell = null;
            foreach (DataGridViewRow linha in dgvEscala.Rows)
                linha.Visible = (cmbFiltroEscala.SelectedIndex == 0 || Convert.ToString(linha.Cells[1].Value) == cmbFiltroEscala.Text)
                    && (cmbFiltroTurnoEscala.SelectedIndex == 0 || Convert.ToString(linha.Cells[3].Value) == cmbFiltroTurnoEscala.Text);
            dgvEscala.ClearSelection();
        }
        private void CarregarExemplos()
        {
            dgvEscala.ClearSelection();
        }
    }
}
