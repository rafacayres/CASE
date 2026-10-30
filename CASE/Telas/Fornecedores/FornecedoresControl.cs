using System;
using System.Linq;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{
    public partial class FornecedoresControl : UserControl
    {
        private OperacoesPreview.Fornecedor selecionado;

        public FornecedoresControl()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            FrontendTheme.Aplicar(this);
            VisualUtils.ExibirEstadoVazio(dgvFornecedores, "Nenhum fornecedor encontrado. Altere a busca ou cadastre um novo.");
            txtBusca.TextChanged += (s, e) => Atualizar();
            btnNovo.Click += (s, e) => { Limpar(); txtNome.Focus(); };
            dgvFornecedores.SelectionChanged += (s, e) => Selecionar();
            btnSalvar.Click += (s, e) => Salvar();
            Atualizar();
        }

        private void Atualizar()
        {
            dgvFornecedores.Rows.Clear();
            foreach (var fornecedor in OperacoesPreview.Fornecedores.Where(f => (f.Nome + " " + f.Documento).IndexOf(txtBusca.Text, StringComparison.CurrentCultureIgnoreCase) >= 0))
            {
                int linha = dgvFornecedores.Rows.Add(fornecedor.Nome, fornecedor.Documento, fornecedor.Email, fornecedor.Telefone);
                dgvFornecedores.Rows[linha].Tag = fornecedor;
            }
            dgvFornecedores.ClearSelection();
            Limpar();
        }

        private void Limpar()
        {
            selecionado = null;
            txtNome.Clear();
            txtDocumento.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            dgvFornecedores.ClearSelection();
            lblMensagem.Text = "Novo fornecedor · informe o nome e os contatos disponíveis.";
            btnSalvar.Text = "Cadastrar fornecedor";
        }

        private void Selecionar()
        {
            if (dgvFornecedores.SelectedRows.Count == 0) return;
            selecionado = dgvFornecedores.SelectedRows[0].Tag as OperacoesPreview.Fornecedor;
            if (selecionado == null) return;
            txtNome.Text = selecionado.Nome;
            txtDocumento.Text = selecionado.Documento;
            txtEmail.Text = selecionado.Email;
            txtTelefone.Text = selecionado.Telefone;
            lblMensagem.Text = "Editando " + selecionado.Nome;
            btnSalvar.Text = "Salvar alterações";
        }

        private void Salvar()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                lblMensagem.Text = "Informe o nome do fornecedor.";
                return;
            }
            string documento = new string(txtDocumento.Text.Where(char.IsDigit).ToArray());
            if (documento.Length != 0 && documento.Length != 11 && documento.Length != 14)
            {
                lblMensagem.Text = "Informe CPF com 11 dígitos ou CNPJ com 14 dígitos, ou deixe em branco.";
                return;
            }
            if (documento.Length > 0 && OperacoesPreview.Fornecedores.Any(f => f != selecionado && f.Documento == documento))
            {
                lblMensagem.Text = "Este documento já está cadastrado.";
                return;
            }
            var fornecedor = selecionado ?? new OperacoesPreview.Fornecedor();
            fornecedor.Nome = txtNome.Text.Trim();
            fornecedor.Documento = documento;
            fornecedor.Email = txtEmail.Text.Trim();
            fornecedor.Telefone = txtTelefone.Text.Trim();
            if (selecionado == null) OperacoesPreview.Fornecedores.Add(fornecedor);
            Atualizar();
            lblMensagem.Text = "Fornecedor atualizado.";
        }
    }
}
