using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE
{
    public partial class TransferenciaEstoqueForm : Form
    {
        private readonly Usuario usuarioAutenticado;
        private readonly List<LocalEstoque> locaisEvento = new List<LocalEstoque>();
        private readonly TransferenciaEstoqueService transferenciaService =
            new TransferenciaEstoqueService();
        private readonly LocalEstoqueService localEstoqueService = new LocalEstoqueService();
        private readonly EstoqueService estoqueService = new EstoqueService();
        private readonly ProdutoService produtoService = new ProdutoService();
        private readonly string chaveOperacao = Guid.NewGuid().ToString("N");
        private bool salvando;

        public TransferenciaEstoqueForm() : this(null)
        {
        }

        public TransferenciaEstoqueForm(Usuario usuario)
        {
            InitializeComponent();
            IconoAplicacao.Aplicar(this);
            usuarioAutenticado = usuario;

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            cmbOrigem.DisplayMember = "Nome";
            cmbDestino.DisplayMember = "Nome";
            cmbProduto.DisplayMember = "Nome";
            numQuantidade.DecimalPlaces = 0;
            numQuantidade.Minimum = 1;
            cmbOrigem.SelectedIndexChanged += cmbOrigem_SelectedIndexChanged;
            cmbProduto.SelectedIndexChanged += cmbProduto_SelectedIndexChanged;
            btnConfirmar.Click += btnConfirmar_Click;
            FrontendTheme.Aplicar(this);

            if (usuarioAutenticado == null)
            {
                btnConfirmar.Enabled = false;
                lblMensagem.Text = "Autentique-se para transferir produtos.";
                return;
            }

            CarregarDados();
        }

        private void CarregarDados()
        {
            try
            {
                foreach (LocalEstoque local in localEstoqueService.Listar(usuarioAutenticado))
                {
                    if (local.Ativo && !local.EhCentral && local.IdEvento.HasValue)
                    {
                        locaisEvento.Add(local);
                    }
                }

                foreach (LocalEstoque local in locaisEvento)
                {
                    cmbOrigem.Items.Add(local);
                }

                foreach (Produto produto in produtoService.Listar(usuarioAutenticado))
                {
                    if (produto.Ativo)
                    {
                        cmbProduto.Items.Add(produto);
                    }
                }

                if (cmbOrigem.Items.Count > 0)
                {
                    cmbOrigem.SelectedIndex = 0;
                }

                if (cmbProduto.Items.Count > 0)
                {
                    cmbProduto.SelectedIndex = 0;
                }

                if (cmbOrigem.Items.Count == 0 || cmbProduto.Items.Count == 0)
                {
                    btnConfirmar.Enabled = false;
                    lblMensagem.Text =
                        "Não há locais ou produtos disponíveis para uma transferência autorizada.";
                }
            }
            catch (Exception)
            {
                btnConfirmar.Enabled = false;
                lblMensagem.Text = "Não foi possível carregar os dados da transferência.";
            }
        }

        private void cmbOrigem_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarDestinos();
            MostrarSaldo();
        }

        private void cmbProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarSaldo();
        }

        private void AtualizarDestinos()
        {
            cmbDestino.Items.Clear();
            LocalEstoque origem = cmbOrigem.SelectedItem as LocalEstoque;

            if (origem == null || !origem.IdEvento.HasValue)
            {
                return;
            }

            foreach (LocalEstoque local in locaisEvento)
            {
                if (local.IdLocalEstoque != origem.IdLocalEstoque &&
                    local.IdEvento == origem.IdEvento)
                {
                    cmbDestino.Items.Add(local);
                }
            }

            if (cmbDestino.Items.Count > 0)
            {
                cmbDestino.SelectedIndex = 0;
            }
        }

        private void MostrarSaldo()
        {
            if (dadosDemo != null)
            {
                MostrarSaldoDemo();
                return;
            }

            LocalEstoque origem = cmbOrigem.SelectedItem as LocalEstoque;
            Produto produto = cmbProduto.SelectedItem as Produto;

            if (origem == null || produto == null || usuarioAutenticado == null)
            {
                lblSaldo.Text = "Disponível na origem: —";
                return;
            }

            try
            {
                List<Estoque> saldos = estoqueService.ListarPorLocal(
                    usuarioAutenticado,
                    origem.IdLocalEstoque);

                decimal quantidadeDisponivel = 0;

                foreach (Estoque estoque in saldos)
                {
                    if (estoque.IdProduto == produto.IdProduto)
                    {
                        quantidadeDisponivel = estoque.QuantidadeAtual;
                        break;
                    }
                }

                lblSaldo.Text = "Disponível na origem: " +
                    quantidadeDisponivel.ToString("N2") + " un";
            }
            catch (Exception)
            {
                lblSaldo.Text = "Saldo indisponível.";
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            ConfirmarTransferencia();
        }

        private void ConfirmarTransferencia()
        {
            if (dadosDemo != null)
            {
                ConfirmarTransferenciaDemo();
                return;
            }

            if (salvando)
            {
                return;
            }

            LocalEstoque origem = cmbOrigem.SelectedItem as LocalEstoque;
            LocalEstoque destino = cmbDestino.SelectedItem as LocalEstoque;
            Produto produto = cmbProduto.SelectedItem as Produto;

            if (origem == null || destino == null || produto == null)
            {
                lblMensagem.Text = "Selecione a origem, o destino e o produto.";
                return;
            }

            if (numQuantidade.Value <= 0)
            {
                lblMensagem.Text = "A quantidade deve ser maior que zero.";
                return;
            }

            TransferenciaEstoque transferencia = new TransferenciaEstoque();
            transferencia.IdLocalOrigem = origem.IdLocalEstoque;
            transferencia.IdLocalDestino = destino.IdLocalEstoque;
            transferencia.DataHora = DateTime.Now;
            transferencia.Observacao = txtObservacao.Text.Trim();
            transferencia.ChaveOperacao = chaveOperacao;

            List<TransferenciaItem> itens = new List<TransferenciaItem>();
            TransferenciaItem item = new TransferenciaItem();
            item.IdProduto = produto.IdProduto;
            item.Quantidade = numQuantidade.Value;
            itens.Add(item);

            salvando = true;
            btnConfirmar.Enabled = false;

            try
            {
                transferenciaService.Registrar(usuarioAutenticado, transferencia, itens);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                lblMensagem.Text = ex.Message;
                ReabilitarConfirmacao();
            }
            catch (InvalidOperationException ex)
            {
                lblMensagem.Text = ex.Message;
                ReabilitarConfirmacao();
            }
            catch (UnauthorizedAccessException)
            {
                lblMensagem.Text = "Sua conta não pode realizar esta transferência.";
                ReabilitarConfirmacao();
            }
            catch (Exception)
            {
                lblMensagem.Text = "Não foi possível concluir a transferência.";
                ReabilitarConfirmacao();
            }
        }

        private void ReabilitarConfirmacao()
        {
            salvando = false;
            btnConfirmar.Enabled = true;
        }
    }
}
