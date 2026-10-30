using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{
    public partial class EntradaMercadoriaForm : Form
    {
        private readonly List<OperacoesPreview.ItemEntrada> itens = new List<OperacoesPreview.ItemEntrada>();
        private bool confirmado;
        public EntradaMercadoriaForm()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            cmbFornecedor.Items.Add("Selecione um fornecedor");
            cmbFornecedor.Items.AddRange(OperacoesPreview.Fornecedores.Cast<object>().ToArray());
            cmbFornecedor.SelectedIndex = 0;
            cmbDestino.Items.AddRange(OperacoesPreview.Locais);
            cmbProduto.Items.AddRange(OperacoesPreview.Produtos);
            cmbTipo.SelectedIndex = cmbDestino.SelectedIndex = cmbProduto.SelectedIndex = 0;
            dtpData.Value = DateTime.Now;
            FrontendTheme.Aplicar(this);
            VisualUtils.ExibirEstadoVazio(dgvItens, "Adicione os produtos recebidos para conferir as quantidades e o total.");
            dgvItens.Columns[0].FillWeight = 180;
            for (int i = 1; i < dgvItens.Columns.Count; i++)
                dgvItens.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            btnAdicionar.Click += (s, e) => Adicionar();
            btnRemover.Click += (s, e) =>
            {
                if (dgvItens.CurrentRow == null) return;
                itens.Remove(dgvItens.CurrentRow.Tag as OperacoesPreview.ItemEntrada);
                AtualizarItens();
            };
            btnConfirmar.Click += (s, e) => Confirmar();
        }

        private void Adicionar()
        {
            if (cmbProduto.SelectedIndex < 0)
            {
                lblMensagem.Text = "Selecione um produto.";
                return;
            }
            itens.Add(new OperacoesPreview.ItemEntrada { Produto = cmbProduto.Text, Quantidade = numQuantidade.Value, Custo = numCusto.Value });
            AtualizarItens();
            lblMensagem.Text = "Item adicionado. Confira a lista antes de confirmar.";
        }

        private void AtualizarItens()
        {
            dgvItens.Rows.Clear();
            var cultura = CultureInfo.GetCultureInfo("pt-BR");
            foreach (var item in itens)
            {
                decimal subtotal = item.Quantidade * item.Custo;
                int linha = dgvItens.Rows.Add(item.Produto, item.Quantidade, item.Custo.ToString("C2", cultura), subtotal.ToString("C2", cultura));
                dgvItens.Rows[linha].Tag = item;
            }
            lblTotal.Text = "Total: " + itens.Sum(i => i.Quantidade * i.Custo).ToString("C2", cultura);
        }

        private void Confirmar()
        {
            if (confirmado) return;
            try
            {
                OperacoesPreview.Receber(cmbDestino.Text, cmbTipo.Text, cmbFornecedor.SelectedItem as OperacoesPreview.Fornecedor, dtpData.Value, txtDocumento.Text.Trim(), itens);
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
