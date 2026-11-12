using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE
{
    public partial class FornecedoresControl : UserControl
    {
        private readonly FornecedorService fornecedorService = new FornecedorService();
        private List<Fornecedor> fornecedores = new List<Fornecedor>();
        private Fornecedor fornecedorSelecionado;
        private readonly Usuario usuarioAutenticado;
        private readonly ToolTip dicasCampos = new ToolTip();

        public FornecedoresControl()
            : this(null)
        {
        }

        public FornecedoresControl(Usuario usuario)
        {
            InitializeComponent();
            usuarioAutenticado = usuario;

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            FrontendTheme.Aplicar(this);
            VisualUtils.ExibirEstadoVazio(
                dgvFornecedores,
                "Nenhum fornecedor encontrado. Altere a busca ou cadastre um novo.");

            dicasCampos.SetToolTip(txtNome, "Exemplo: Mercado São José ou João da Silva");
            dicasCampos.SetToolTip(txtDocumento, "Opcional. Exemplo: CPF ou CNPJ do fornecedor");
            dicasCampos.SetToolTip(txtTelefone, "Opcional. Exemplo: (11) 3333-4444");
            dicasCampos.SetToolTip(txtEmail, "Opcional. Exemplo: contato@fornecedor.com.br");

            txtBusca.TextChanged += TxtBusca_TextChanged;
            btnNovo.Click += BtnNovo_Click;
            dgvFornecedores.SelectionChanged += DgvFornecedores_SelectionChanged;
            btnSalvar.Click += BtnSalvar_Click;

            if (usuarioAutenticado == null)
            {
                btnNovo.Enabled = false;
                btnSalvar.Enabled = false;
                lblMensagem.Text = "Entre com uma conta autorizada para consultar fornecedores.";
                return;
            }

            CarregarFornecedores();
            try
            {
                string perfil;
                new AutorizacaoService().ValidarUsuarioEObterPerfil(usuarioAutenticado, out perfil);
                if (perfil != "Administrador")
                {
                    btnNovo.Enabled = false;
                    btnSalvar.Enabled = false;
                    txtNome.ReadOnly = true;
                    txtDocumento.ReadOnly = true;
                    txtTelefone.ReadOnly = true;
                    txtEmail.ReadOnly = true;
                    lblMensagem.Text = "Consulta de fornecedores. A manutenção do cadastro é administrativa.";
                }
            }
            catch (Exception)
            {
                btnNovo.Enabled = false;
                btnSalvar.Enabled = false;
            }
        }

        private void CarregarFornecedores()
        {
            try
            {
                fornecedores = fornecedorService.Listar(usuarioAutenticado);
                AtualizarLista();
            }
            catch (UnauthorizedAccessException)
            {
                fornecedores = new List<Fornecedor>();
                dgvFornecedores.Rows.Clear();
                btnNovo.Enabled = false;
                btnSalvar.Enabled = false;
                lblMensagem.Text = "Seu perfil não pode consultar fornecedores.";
            }
            catch (Exception)
            {
                fornecedores = new List<Fornecedor>();
                dgvFornecedores.Rows.Clear();
                lblMensagem.Text = "Não foi possível carregar os fornecedores.";
            }
        }

        private void AtualizarLista()
        {
            dgvFornecedores.Rows.Clear();
            string textoBusca = txtBusca.Text.Trim();

            foreach (Fornecedor fornecedor in fornecedores)
            {
                string textoFornecedor = fornecedor.Nome;

                if (fornecedor.Documento != null)
                {
                    textoFornecedor += " " + fornecedor.Documento;
                }

                if (fornecedor.Email != null)
                {
                    textoFornecedor += " " + fornecedor.Email;
                }

                bool correspondeBusca = textoFornecedor.IndexOf(
                    textoBusca,
                    StringComparison.CurrentCultureIgnoreCase) >= 0;

                if (!correspondeBusca)
                {
                    continue;
                }

                int indiceLinha = dgvFornecedores.Rows.Add(
                    fornecedor.Nome,
                    fornecedor.Documento,
                    fornecedor.Email,
                    fornecedor.Telefone);

                dgvFornecedores.Rows[indiceLinha].Tag = fornecedor;
            }

            dgvFornecedores.ClearSelection();
        }

        private void LimparCampos()
        {
            fornecedorSelecionado = null;
            txtNome.Clear();
            txtDocumento.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            dgvFornecedores.ClearSelection();
            lblMensagem.Text = "Novo fornecedor. Informe o nome e os contatos disponíveis.";
            btnSalvar.Text = "Cadastrar fornecedor";
        }

        private void SelecionarFornecedor()
        {
            if (dgvFornecedores.SelectedRows.Count == 0)
            {
                return;
            }

            fornecedorSelecionado = dgvFornecedores.SelectedRows[0].Tag as Fornecedor;

            if (fornecedorSelecionado == null)
            {
                return;
            }

            txtNome.Text = fornecedorSelecionado.Nome;
            txtDocumento.Text = fornecedorSelecionado.Documento;
            txtEmail.Text = fornecedorSelecionado.Email;
            txtTelefone.Text = fornecedorSelecionado.Telefone;
            lblMensagem.Text = "Editando " + fornecedorSelecionado.Nome;
            btnSalvar.Text = "Salvar alterações";
        }

        private void SalvarFornecedor()
        {
            if (dadosDemo != null)
            {
                SalvarFornecedorDemo();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                lblMensagem.Text = "Informe o nome do fornecedor.";
                return;
            }

            Fornecedor fornecedor;
            bool novoFornecedor = fornecedorSelecionado == null;

            if (novoFornecedor)
            {
                fornecedor = new Fornecedor();
            }
            else
            {
                fornecedor = fornecedorSelecionado;
            }

            fornecedor.Nome = txtNome.Text.Trim();
            fornecedor.Documento = txtDocumento.Text.Trim();
            fornecedor.Email = txtEmail.Text.Trim();
            fornecedor.Telefone = txtTelefone.Text.Trim();

            try
            {
                if (novoFornecedor)
                {
                    fornecedorService.Cadastrar(usuarioAutenticado, fornecedor);
                }
                else
                {
                    fornecedorService.Atualizar(usuarioAutenticado, fornecedor);
                }

                CarregarFornecedores();
                LimparCampos();
                lblMensagem.Text = "Fornecedor salvo com sucesso.";
            }
            catch (ArgumentException erro)
            {
                lblMensagem.Text = erro.Message;
            }
            catch (UnauthorizedAccessException)
            {
                lblMensagem.Text = "Seu perfil não pode cadastrar ou editar fornecedores.";
            }
            catch (Exception)
            {
                lblMensagem.Text = "Não foi possível salvar o fornecedor.";
            }
        }

        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        public void Pesquisar(string texto)
        {
            if (texto == null)
            {
                texto = string.Empty;
            }

            txtBusca.Text = texto;
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();
            txtNome.Focus();
        }

        private void DgvFornecedores_SelectionChanged(object sender, EventArgs e)
        {
            SelecionarFornecedor();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            SalvarFornecedor();
        }
    }
}
