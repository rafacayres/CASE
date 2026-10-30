using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{
    public partial class TransferenciaEstoqueForm : Form
    {
        private bool confirmado;
        public TransferenciaEstoqueForm()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            cmbOrigem.Items.AddRange(OperacoesPreview.Locais);
            cmbDestino.Items.AddRange(OperacoesPreview.Locais);
            cmbProduto.Items.AddRange(OperacoesPreview.Produtos);
            cmbOrigem.SelectedIndex = cmbProduto.SelectedIndex = 0;
            cmbDestino.SelectedIndex = 1;
            cmbOrigem.SelectedIndexChanged += (s, e) => MostrarSaldo();
            cmbProduto.SelectedIndexChanged += (s, e) => MostrarSaldo();
            btnConfirmar.Click += (s, e) => Confirmar();
            FrontendTheme.Aplicar(this);
            MostrarSaldo();
        }

        private void MostrarSaldo()
        {
            lblSaldo.Text = "Disponível na origem: " + OperacoesPreview.Saldo(cmbOrigem.Text, cmbProduto.Text).ToString("N2") + " un";
        }

        private void Confirmar()
        {
            if (confirmado) return;
            try
            {
                OperacoesPreview.Transferir(cmbOrigem.Text, cmbDestino.Text, cmbProduto.Text, numQuantidade.Value, txtObservacao.Text.Trim());
                confirmado = true;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (InvalidOperationException ex)
            {
                lblMensagem.Text = ex.Message;
            }
        }
    }
}
