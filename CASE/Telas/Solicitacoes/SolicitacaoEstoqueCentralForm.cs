using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;

namespace CASE.Quermesse
{
    public class SolicitacaoEstoqueCentralForm : Form
    {
        private readonly Usuario usuarioAutenticado;
        private readonly Evento eventoSelecionado;
        private readonly SolicitacaoEstoqueCentralService solicitacaoService =
            new SolicitacaoEstoqueCentralService();
        private readonly ProdutoService produtoService = new ProdutoService();
        private readonly ComboBox cmbTipo = new ComboBox();
        private readonly DataGridView dgvItens = new DataGridView();
        private readonly Button btnAdicionarItem = new Button();
        private readonly Button btnRemoverItem = new Button();
        private readonly Button btnEnviar = new Button();
        private readonly Button btnCancelar = new Button();
        private readonly Label lblAviso = new Label();
        private List<Produto> produtos = new List<Produto>();

        public SolicitacaoEstoqueCentralForm(Usuario usuario, Evento evento)
        {
            usuarioAutenticado = usuario;
            eventoSelecionado = evento;
            ConfigurarFormulario();
            CarregarProdutos();
        }

        private void ConfigurarFormulario()
        {
            Text = "Solicitação ao estoque central";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ClientSize = new System.Drawing.Size(660, 470);
            Font = new System.Drawing.Font("Segoe UI", 9F);
            BackColor = System.Drawing.Color.White;

            Label lblTipo = new Label();
            lblTipo.Text = "Tipo do pedido";
            lblTipo.SetBounds(20, 18, 150, 22);

            cmbTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipo.Items.Add("Liberação do estoque central");
            cmbTipo.Items.Add("Devolução ao estoque central");
            cmbTipo.SelectedIndex = 0;
            cmbTipo.SetBounds(20, 42, 610, 30);

            dgvItens.SetBounds(20, 88, 610, 280);
            dgvItens.AllowUserToAddRows = false;
            dgvItens.AllowUserToDeleteRows = false;
            dgvItens.MultiSelect = false;
            dgvItens.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvItens.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvItens.RowHeadersVisible = false;
            dgvItens.Columns.Add(new DataGridViewComboBoxColumn
            {
                Name = "produto",
                HeaderText = "Produto",
                FillWeight = 75
            });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "quantidade",
                HeaderText = "Quantidade",
                FillWeight = 25
            });

            btnAdicionarItem.Text = "Adicionar produto";
            btnAdicionarItem.SetBounds(20, 378, 140, 32);
            btnAdicionarItem.Click += btnAdicionarItem_Click;

            btnRemoverItem.Text = "Remover linha";
            btnRemoverItem.SetBounds(170, 378, 120, 32);
            btnRemoverItem.Click += btnRemoverItem_Click;

            lblAviso.SetBounds(20, 416, 390, 36);
            lblAviso.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);

            btnEnviar.Text = "Enviar pedido";
            btnEnviar.SetBounds(415, 420, 105, 32);
            btnEnviar.Click += btnEnviar_Click;

            btnCancelar.Text = "Cancelar";
            btnCancelar.SetBounds(530, 420, 100, 32);
            btnCancelar.DialogResult = DialogResult.Cancel;

            Controls.Add(lblTipo);
            Controls.Add(cmbTipo);
            Controls.Add(dgvItens);
            Controls.Add(btnAdicionarItem);
            Controls.Add(btnRemoverItem);
            Controls.Add(lblAviso);
            Controls.Add(btnEnviar);
            Controls.Add(btnCancelar);
            AcceptButton = btnEnviar;
            CancelButton = btnCancelar;
        }

        private void CarregarProdutos()
        {
            try
            {
                List<Produto> todosProdutos = produtoService.Listar(usuarioAutenticado);

                foreach (Produto produto in todosProdutos)
                {
                    if (produto.Ativo)
                    {
                        produtos.Add(produto);
                    }
                }

                DataGridViewComboBoxColumn colunaProduto =
                    (DataGridViewComboBoxColumn)dgvItens.Columns["produto"];
                colunaProduto.DataSource = produtos;
                colunaProduto.DisplayMember = "Nome";
                colunaProduto.ValueMember = "IdProduto";

                if (produtos.Count == 0)
                {
                    btnAdicionarItem.Enabled = false;
                    btnEnviar.Enabled = false;
                    lblAviso.Text = "Não há produtos ativos para solicitar.";
                    return;
                }

                AdicionarLinha();
            }
            catch (Exception)
            {
                btnAdicionarItem.Enabled = false;
                btnEnviar.Enabled = false;
                lblAviso.Text = "Não foi possível carregar os produtos.";
            }
        }

        private void btnAdicionarItem_Click(object sender, EventArgs e)
        {
            AdicionarLinha();
        }

        private void AdicionarLinha()
        {
            if (produtos.Count == 0)
            {
                return;
            }

            int primeiraLinha = dgvItens.Rows.Add(produtos[0].IdProduto, "1");
            dgvItens.CurrentCell = dgvItens.Rows[primeiraLinha].Cells[0];
        }

        private void btnRemoverItem_Click(object sender, EventArgs e)
        {
            if (dgvItens.SelectedRows.Count > 0)
            {
                dgvItens.Rows.Remove(dgvItens.SelectedRows[0]);
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvItens.EndEdit();
                List<SolicitacaoEstoqueCentralItem> itens = MontarItens();
                int idSolicitacao;

                if (cmbTipo.SelectedIndex == 0)
                {
                    idSolicitacao = solicitacaoService.CriarLiberacao(
                        usuarioAutenticado,
                        eventoSelecionado.IdEvento,
                        itens);
                }
                else
                {
                    idSolicitacao = solicitacaoService.CriarDevolucao(
                        usuarioAutenticado,
                        eventoSelecionado.IdEvento,
                        itens);
                }

                MessageBox.Show(
                    "Pedido " + idSolicitacao + " enviado para análise administrativa.",
                    "Solicitação enviada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                lblAviso.Text = ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                lblAviso.Text = ex.Message;
            }
            catch (UnauthorizedAccessException)
            {
                lblAviso.Text = "Sua conta não pode enviar este pedido.";
            }
            catch (Exception)
            {
                lblAviso.Text = "Não foi possível registrar a solicitação.";
            }
        }

        private List<SolicitacaoEstoqueCentralItem> MontarItens()
        {
            List<SolicitacaoEstoqueCentralItem> itens =
                new List<SolicitacaoEstoqueCentralItem>();

            foreach (DataGridViewRow linha in dgvItens.Rows)
            {
                if (linha.IsNewRow)
                {
                    continue;
                }

                object valorProduto = linha.Cells[0].Value;
                int idProduto;
                int quantidade;

                if (valorProduto == null ||
                    !int.TryParse(valorProduto.ToString(), out idProduto) ||
                    !int.TryParse(Convert.ToString(linha.Cells[1].Value), out quantidade) ||
                    quantidade <= 0)
                {
                    throw new ArgumentException(
                        "Selecione um produto e informe uma quantidade inteira maior que zero.");
                }

                SolicitacaoEstoqueCentralItem item = new SolicitacaoEstoqueCentralItem();
                item.IdProduto = idProduto;
                item.Quantidade = quantidade;
                itens.Add(item);
            }

            if (itens.Count == 0)
            {
                throw new ArgumentException("Adicione pelo menos um produto ao pedido.");
            }

            return itens;
        }
    }
}
