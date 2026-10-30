using System;
using System.Linq;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class FinanceiroControl : UserControl
    {
        private readonly QuermessePreview exemplos;

        public FinanceiroControl() : this(new QuermessePreview()) { }

        public FinanceiroControl(QuermessePreview exemplos)
        {
            this.exemplos = exemplos;
            InitializeComponent();
            dtpFinanceiro.Value = DateTime.Today;
            FrontendTheme.Aplicar(this);
            AtualizarExemplos();
            AjustarIndicadores();
        }

        private void AtualizarExemplos()
        {
            dgvCustos.Rows.Clear();
            foreach (var custo in exemplos.Custos.Where(c => c.Data.Date == dtpFinanceiro.Value.Date
                && (cmbFiltroFinanceiro.SelectedIndex == 0 || c.Setor == cmbFiltroFinanceiro.Text)))
                dgvCustos.Rows.Add(custo.Data.ToString("dd/MM/yyyy"), custo.Descricao, custo.Setor,
                    custo.Valor.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("pt-BR")), custo.Responsavel);
            dgvCustos.ClearSelection();
        }

        private void Filtros_Changed(object sender, EventArgs e)
        {
            if (exemplos != null) AtualizarExemplos();
        }

        private void btnAdicionarCusto_Click(object sender, EventArgs e)
        {
            labelAviso.Text = "Prévia do formulário de custo: nenhum valor foi registrado.";
        }

        private void panelCorpo_SizeChanged(object sender, EventArgs e) { AjustarIndicadores(); }

        private void AjustarIndicadores()
        {
            var indicadores = new[] { panelFinanceiroResumo0, panelFinanceiroResumo1, panelFinanceiroResumo2 };
            for (int i = 0; i < indicadores.Length; i++)
                indicadores[i].SetBounds(i * (panelCorpo.Width + 15) / 3, 90, (panelCorpo.Width - 30) / 3, 100);
        }
    }
}
