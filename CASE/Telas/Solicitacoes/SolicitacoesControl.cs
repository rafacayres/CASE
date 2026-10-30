using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class SolicitacoesControl : UserControl
    {
        public SolicitacoesControl() : this(new QuermessePreview()) { }

        public SolicitacoesControl(QuermessePreview exemplos)
        {
            InitializeComponent();
            FrontendTheme.Aplicar(this);
            CarregarExemplos();
        }

        private void btnNovaSolicitacao_Click(object sender, EventArgs e)
        {
            using (var formulario = new SolicitacaoForm()) formulario.ShowDialog(this);
        }

        private void btnDetalhesSolicitacao_Click(object sender, EventArgs e)
        {
            using (var formulario = new SolicitacaoForm(true)) formulario.ShowDialog(this);
        }

        private void Filtros_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvSolicitacoesQuermesse.CurrentCell = null;
            foreach (DataGridViewRow linha in dgvSolicitacoesQuermesse.Rows)
                linha.Visible = (cmbFiltroSolicitacoes.SelectedIndex == 0 || Convert.ToString(linha.Cells[2].Value) == cmbFiltroSolicitacoes.Text)
                    && (cmbStatusListaSolicitacoes.SelectedIndex == 0 || Convert.ToString(linha.Cells[6].Value) == cmbStatusListaSolicitacoes.Text);
            dgvSolicitacoesQuermesse.ClearSelection();
        }
        private void CarregarExemplos()
        {
            dgvSolicitacoesQuermesse.Rows.Add(new object[] { "22/10 18:10", "Marina Souza", "Bebidas", "Reposição", "Água mineral", "20", "Pendente" });
            dgvSolicitacoesQuermesse.Rows.Add(new object[] { "22/10 18:25", "Pedro Lima", "Alimentação", "Ocorrência", "Pastel", "2", "Em atendimento" });
            dgvSolicitacoesQuermesse.Rows.Add(new object[] { "22/10 16:40", "Julia Santos", "Doces", "Reposição", "Bolo de milho", "10", "Concluída" });
            dgvSolicitacoesQuermesse.ClearSelection();
        }
    }
}
