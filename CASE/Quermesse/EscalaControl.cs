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

        // Exemplos visuais; sem persistência ou regras de negócio.
        private void CarregarExemplos()
        {
            dgvEscala.Rows.Add(new object[] { "Marina Souza", "Bebidas", "22/10/2026", "Noite", "18:00", "23:00", "Confirmada" });
            dgvEscala.Rows.Add(new object[] { "Pedro Lima", "Alimentação", "22/10/2026", "Noite", "18:00", "23:00", "Confirmada" });
            dgvEscala.Rows.Add(new object[] { "Julia Santos", "Doces", "22/10/2026", "Tarde", "13:00", "18:00", "A confirmar" });
            dgvEscala.ClearSelection();
        }
    }
}
