using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class ApuracaoControl : UserControl
    {
        public ApuracaoControl() : this(new QuermessePreview()) { }

        public ApuracaoControl(QuermessePreview exemplos)
        {
            InitializeComponent();
            dtpApuracao.Value = DateTime.Today;
            FrontendTheme.Aplicar(this);
            AtualizarExemplo();
        }

        private void Filtros_Changed(object sender, EventArgs e) { AtualizarExemplo(); }

        private void AtualizarExemplo()
        {
            dgvApuracao.Rows.Clear();
            if (cmbSetorApuracao.SelectedIndex == 0 && cmbTurnoApuracao.Text == "Noite" && dtpApuracao.Value.Date == DateTime.Today)
                dgvApuracao.Rows.Add("Marina Souza", "Bebidas", "Noite", "9", "R$ 54,00");
            dgvApuracao.ClearSelection();
        }

        private void btnConferirApuracao_Click(object sender, EventArgs e)
        {
            labelAviso.Text = "Prévia da conferência: nenhum fechamento ou cálculo foi executado.";
        }
    }
}
