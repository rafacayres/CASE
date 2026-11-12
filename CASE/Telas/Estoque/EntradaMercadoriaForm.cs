using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE
{
    public partial class EntradaMercadoriaForm : Form
    {
        private readonly Usuario usuarioAutenticado;
        private readonly List<EntradaItem> itens = new List<EntradaItem>();
        private readonly List<Produto> produtos = new List<Produto>();
        private readonly EntradaMercadoriaService entradaService = new EntradaMercadoriaService();
        private readonly ProdutoService produtoService = new ProdutoService();
        private readonly FornecedorService fornecedorService = new FornecedorService();
        private readonly LocalEstoqueService localEstoqueService = new LocalEstoqueService();
        private readonly string chaveOperacao = Guid.NewGuid().ToString("N");
        private bool salvando;

        public EntradaMercadoriaForm() : this(null)
        {
        }

        public EntradaMercadoriaForm(Usuario usuario)
        {
            InitializeComponent();
            IconoAplicacao.Aplicar(this);
            usuarioAutenticado = usuario;

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            cmbFornecedor.DisplayMember = "Nome";
            cmbDestino.DisplayMember = "Nome";
            cmbProduto.DisplayMember = "Nome";
            cmbTipo.Items.Clear();
            cmbTipo.Items.Add("compra");
            cmbTipo.Items.Add("doacao");
            cmbTipo.SelectedIndex = 0;
            numQuantidade.DecimalPlaces = 0;
            numQuantidade.Minimum = 1;
            dtpData.Value = DateTime.Now;

            FrontendTheme.Aplicar(this);
            VisualUtils.ExibirEstadoVazio(dgvItens,
                "Adicione os produtos recebidos para conferir as quantidades e o total.");
            dgvItens.Columns[0].FillWeight = 180;

            for (int indiceColuna = 1; indiceColuna < dgvItens.Columns.Count; indiceColuna++)
            {
                dgvItens.Columns[indiceColuna].DefaultCellStyle.Alignment =
                    DataGridViewContentAlignment.MiddleRight;
            }

            btnAdicionar.Click += BtnAdicionar_Click;
            btnRemover.Click += BtnRemover_Click;
            btnConfirmar.Click += BtnConfirmar_Click;

            if (usuarioAutenticado == null)
            {
                btnConfirmar.Enabled = false;
                lblMensagem.Text = "Autentique-se para registrar uma entrada.";
                return;
            }

            CarregarDados();
        }

        private void CarregarDados()
        {
            try
            {
                List<LocalEstoque> locais = localEstoqueService.Listar(usuarioAutenticado);

                foreach (LocalEstoque local in locais)
                {
                    if (local.EhCentral && !local.IdEvento.HasValue)
                    {
                        cmbDestino.Items.Add(local);
                    }
                }

                if (cmbDestino.Items.Count == 1)
                {
                    cmbDestino.SelectedIndex = 0;
                }

                List<Produto> produtosDisponiveis = produtoService.Listar(usuarioAutenticado);

                foreach (Produto produto in produtosDisponiveis)
                {
                    if (produto.Ativo)
                    {
                        produtos.Add(produto);
                        cmbProduto.Items.Add(produto);
                    }
                }

                if (cmbProduto.Items.Count > 0)
                {
                    cmbProduto.SelectedIndex = 0;
                }

                cmbFornecedor.Items.Add("Sem fornecedor");
                cmbFornecedor.SelectedIndex = 0;

                foreach (Fornecedor fornecedor in fornecedorService.Listar(usuarioAutenticado))
                {
                    cmbFornecedor.Items.Add(fornecedor);
                }

                if (cmbDestino.Items.Count == 0)
                {
                    btnConfirmar.Enabled = false;
                    lblMensagem.Text = "Não há estoque central autorizado para registrar entradas.";
                }
            }
            catch (UnauthorizedAccessException)
            {
                btnConfirmar.Enabled = false;
                lblMensagem.Text = "Somente uma conta autorizada pode registrar entradas.";
            }
            catch (Exception)
            {
                btnConfirmar.Enabled = false;
                lblMensagem.Text = "Não foi possível carregar os dados da entrada.";
            }
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            AdicionarItem();
        }

        private void BtnRemover_Click(object sender, EventArgs e)
        {
            if (dgvItens.CurrentRow == null)
            {
                return;
            }

            EntradaItem item = dgvItens.CurrentRow.Tag as EntradaItem;

            if (item != null)
            {
                itens.Remove(item);
                AtualizarItens();
            }
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            ConfirmarEntrada();
        }

        private void AdicionarItem()
        {
            Produto produto = cmbProduto.SelectedItem as Produto;

            if (produto == null)
            {
                lblMensagem.Text = "Selecione um produto.";
                return;
            }

            EntradaItem item = new EntradaItem();
            item.IdProduto = produto.IdProduto;
            item.Quantidade = numQuantidade.Value;
            item.CustoUnitario = numCusto.Value;

            itens.Add(item);
            AtualizarItens();
            lblMensagem.Text = "Item adicionado. Confira a lista antes de confirmar.";
        }

        private void AtualizarItens()
        {
            dgvItens.Rows.Clear();
            decimal valorTotal = 0;
            CultureInfo cultura = CultureInfo.GetCultureInfo("pt-BR");

            foreach (EntradaItem item in itens)
            {
                Produto produto = BuscarProduto(item.IdProduto);
                string nomeProduto;

                if (produto == null)
                {
                    nomeProduto = "Produto " + item.IdProduto;
                }
                else
                {
                    nomeProduto = produto.Nome;
                }
                decimal custoUnitario = 0;

                if (item.CustoUnitario.HasValue)
                {
                    custoUnitario = item.CustoUnitario.Value;
                }

                decimal subtotal = item.Quantidade * custoUnitario;
                valorTotal += subtotal;

                int indiceLinha = dgvItens.Rows.Add(
                    nomeProduto,
                    item.Quantidade,
                    custoUnitario.ToString("C2", cultura),
                    subtotal.ToString("C2", cultura));

                dgvItens.Rows[indiceLinha].Tag = item;
            }

            lblTotal.Text = "Total: " + valorTotal.ToString("C2", cultura);
        }

        private Produto BuscarProduto(int idProduto)
        {
            foreach (Produto produto in produtos)
            {
                if (produto.IdProduto == idProduto)
                {
                    return produto;
                }
            }

            return null;
        }

        private void ConfirmarEntrada()
        {
            if (dadosDemo != null)
            {
                ConfirmarEntradaDemo();
                return;
            }

            if (salvando)
            {
                return;
            }

            LocalEstoque destino = cmbDestino.SelectedItem as LocalEstoque;

            if (destino == null)
            {
                lblMensagem.Text = "Selecione um estoque central.";
                return;
            }

            if (itens.Count == 0)
            {
                lblMensagem.Text = "Adicione pelo menos um produto à entrada.";
                return;
            }

            Fornecedor fornecedor = cmbFornecedor.SelectedItem as Fornecedor;
            EntradaMercadoria entrada = new EntradaMercadoria();
            entrada.IdLocalDestino = destino.IdLocalEstoque;
            entrada.DataHora = dtpData.Value;
            entrada.TipoEntrada = Convert.ToString(cmbTipo.SelectedItem);
            entrada.NumeroDocumento = txtDocumento.Text.Trim();

            if (fornecedor != null)
            {
                entrada.IdFornecedor = fornecedor.IdFornecedor;
            }

            entrada.ChaveOperacao = chaveOperacao;

            salvando = true;
            btnConfirmar.Enabled = false;

            try
            {
                entradaService.Registrar(usuarioAutenticado, entrada, itens);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException || ex is InvalidOperationException)
                {
                    lblMensagem.Text = ex.Message;
                }
                else if (ex is UnauthorizedAccessException)
                {
                    lblMensagem.Text = "Sua conta não pode registrar entradas no estoque central.";
                }
                else
                {
                    lblMensagem.Text = "Não foi possível registrar a entrada.";
                }

                salvando = false;
                btnConfirmar.Enabled = true;
            }
        }
    }
}
