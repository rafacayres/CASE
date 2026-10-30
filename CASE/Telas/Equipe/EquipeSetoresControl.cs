using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class EquipeSetoresControl : UserControl
    {
        private readonly QuermessePreview exemplos;

        public EquipeSetoresControl() : this(new QuermessePreview()) { }

        public EquipeSetoresControl(QuermessePreview exemplos)
        {
            this.exemplos = exemplos;
            InitializeComponent();
            FrontendTheme.Aplicar(this);
            CarregarExemplos();
        }

        private void btnNovoFuncionario_Click(object sender, EventArgs e)
        {
            AbrirFormulario(null);
        }

        private void btnEditarFuncionario_Click(object sender, EventArgs e)
        {
            if (dgvEquipe.CurrentRow == null) { labelAviso.Text = "Selecione um funcionário na lista."; return; }
            AbrirFormulario(dgvEquipe.CurrentRow.Tag as PessoaPreview);
        }

        private void dgvEquipe_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) AbrirFormulario(dgvEquipe.Rows[e.RowIndex].Tag as PessoaPreview);
        }

        private void AbrirFormulario(PessoaPreview pessoa)
        {
            using (var formulario = new PessoaSetorForm(exemplos, pessoa)) formulario.ShowDialog(this);
        }

        private void btnAdicionarSetor_Click(object sender, EventArgs e)
        {
            labelAviso.Text = "Prévia do formulário de setor: nenhum dado foi salvo.";
        }
        private void CarregarExemplos()
        {
            dgvEquipe.Rows.Add(new object[] { "Rafael Cayres", "rafael", "Administrador", "Organização", "Noite", "Ativo" });
            dgvEquipe.Rows.Add(new object[] { "Ana Silva", "ana", "Gerente", "Gestão", "Noite", "Ativo" });
            dgvEquipe.Rows.Add(new object[] { "Marina Souza", "marina", "Vendedor", "Bebidas", "Noite", "Ativo" });
            dgvEquipe.Rows.Add(new object[] { "Pedro Lima", "pedro", "Vendedor", "Alimentação", "Noite", "Ativo" });
            dgvEquipe.Rows.Add(new object[] { "Julia Santos", "julia", "Vendedor", "Doces", "Tarde", "Ativo" });
            dgvEquipe.ClearSelection();
            dgvSetores.Rows.Add(new object[] { "Bebidas", "1", "1" });
            dgvSetores.Rows.Add(new object[] { "Alimentação", "1", "1" });
            dgvSetores.Rows.Add(new object[] { "Doces", "1", "1" });
            dgvSetores.Rows.Add(new object[] { "Brincadeiras", "0", "0" });
            dgvSetores.ClearSelection();
            for (int i = 0; i < exemplos.Pessoas.Count; i++) dgvEquipe.Rows[i].Tag = exemplos.Pessoas[i];
        }
    }
}
