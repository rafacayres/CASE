using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE
{
    public partial class OperacoesEstoqueControl : UserControl
    {
        private readonly Usuario usuarioAutenticado;
        private readonly LocalEstoqueService localEstoqueService = new LocalEstoqueService();
        private readonly EstoqueService estoqueService = new EstoqueService();
        private readonly ProdutoService produtoService = new ProdutoService();
        private readonly Dictionary<int, Produto> produtosPorId = new Dictionary<int, Produto>();

        public event EventHandler FornecedoresSolicitados;

        public OperacoesEstoqueControl() : this(null)
        {
        }

        public OperacoesEstoqueControl(Usuario usuario)
        {
            InitializeComponent();
            usuarioAutenticado = usuario;

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            cmbLocal.DisplayMember = "Nome";
            cmbLocal.SelectedIndexChanged += cmbLocal_SelectedIndexChanged;
            btnEntrada.Click += RegistrarEntrada;
            btnTransferir.Click += TransferirEstoque;
            dgvSaldos.CellDoubleClick += ConfigurarLimitesEstoque;
            btnFornecedores.Click += (s, e) => FornecedoresSolicitados?.Invoke(this, EventArgs.Empty);
            FrontendTheme.Aplicar(this);
            VisualUtils.ExibirEstadoVazio(dgvHistorico, "O histórico consolidado ainda não está disponível.");
            dgvSaldos.Columns[0].FillWeight = 180;
            dgvSaldos.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSaldos.Columns[2].FillWeight = 50;
            dgvHistorico.Columns[4].FillWeight = 180;
            VisualUtils.AplicarIcone(btnEntrada, Properties.Resources.LucideAcaoEntrada);
            VisualUtils.AplicarIcone(btnTransferir, Properties.Resources.LucideAcaoTransferencia);
            VisualUtils.AplicarIcone(btnFornecedores, Properties.Resources.LucideAcaoFornecedores);
            btnAbaSaldo.Click += (s, e) => SelecionarAba(false);
            btnAbaHistorico.Click += (s, e) => SelecionarAba(true);
            SizeChanged += (s, e) => Organizar();
            Organizar();
            SelecionarAba(false);

            if (usuarioAutenticado == null)
            {
                lblMensagem.Text = "Autentique-se para consultar os estoques.";
                btnEntrada.Enabled = false;
                btnTransferir.Enabled = false;
                return;
            }

            CarregarLocais();
            try
            {
                string perfil;
                new AutorizacaoService().ValidarUsuarioEObterPerfil(usuarioAutenticado, out perfil);
                btnEntrada.Enabled = perfil == "Administrador" && cmbLocal.Items.Count > 0;
                btnTransferir.Enabled = perfil == "Gerente" && cmbLocal.Items.Count > 0;
                btnFornecedores.Enabled = perfil == "Administrador" || perfil == "Gerente";
            }
            catch (Exception)
            {
                btnEntrada.Enabled = false;
                btnTransferir.Enabled = false;
                btnFornecedores.Enabled = false;
            }
        }

        private void ConfigurarLimitesEstoque(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || usuarioAutenticado == null)
            {
                return;
            }
            Estoque estoque = dgvSaldos.Rows[e.RowIndex].Tag as Estoque;
            if (estoque == null)
            {
                return;
            }
            try
            {
                string perfil;
                new AutorizacaoService().ValidarUsuarioEObterPerfil(usuarioAutenticado, out perfil);
                LocalEstoque local = cmbLocal.SelectedItem as LocalEstoque;
                if (perfil == "Membro" || (local != null && local.EhCentral && perfil != "Administrador"))
                {
                    lblMensagem.Text = "Este local está disponível somente para consulta no seu perfil.";
                    return;
                }
            }
            catch (Exception)
            {
                lblMensagem.Text = "Sua sessão não permite configurar este estoque.";
                return;
            }
            using (Form janela = new Form())
            using (NumericUpDown minima = new NumericUpDown())
            using (NumericUpDown ideal = new NumericUpDown())
            using (Button salvar = new Button())
            {
                janela.Text = "Mínimo e ideal do produto neste local";
                janela.StartPosition = FormStartPosition.CenterParent;
                janela.ClientSize = new System.Drawing.Size(430, 200);
                janela.FormBorderStyle = FormBorderStyle.FixedDialog;
                janela.MaximizeBox = false;
                minima.Maximum = 9999999;
                ideal.Maximum = 9999999;
                minima.Value = estoque.QuantidadeMinima;
                ideal.Value = estoque.QuantidadeIdeal;
                minima.SetBounds(200, 30, 200, 28);
                ideal.SetBounds(200, 80, 200, 28);
                salvar.Text = "Salvar limites";
                salvar.SetBounds(220, 140, 180, 36);
                janela.Controls.Add(FrontendTheme.Legenda("Quantidade mínima", new System.Drawing.Rectangle(20, 32, 180, 24)));
                janela.Controls.Add(FrontendTheme.Legenda("Quantidade ideal", new System.Drawing.Rectangle(20, 82, 180, 24)));
                janela.Controls.Add(minima);
                janela.Controls.Add(ideal);
                janela.Controls.Add(salvar);
                salvar.Click += (s, argumentos) =>
                {
                    try
                    {
                        estoqueService.ConfigurarQuantidadesAlvo(usuarioAutenticado,
                            estoque.IdEstoque, minima.Value, ideal.Value);
                        janela.DialogResult = DialogResult.OK;
                        janela.Close();
                    }
                    catch (Exception erro)
                    {
                        string mensagem = "Não foi possível salvar os limites.";
                        if (erro is ArgumentException || erro is InvalidOperationException || erro is UnauthorizedAccessException)
                        {
                            mensagem = erro.Message;
                        }
                        MessageBox.Show(janela, mensagem, "Estoque", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };
                FrontendTheme.Aplicar(janela);
                if (janela.ShowDialog(this) == DialogResult.OK)
                {
                    Atualizar();
                    lblMensagem.Text = "Limites salvos. O saldo atual não foi alterado.";
                }
            }
        }

        private void CarregarLocais()
        {
            try
            {
                List<LocalEstoque> locais = localEstoqueService.Listar(usuarioAutenticado);
                cmbLocal.Items.Clear();

                foreach (LocalEstoque local in locais)
                {
                    cmbLocal.Items.Add(local);
                }

                if (cmbLocal.Items.Count > 0)
                {
                    cmbLocal.SelectedIndex = 0;
                    lblMensagem.Text = string.Empty;
                }
                else
                {
                    btnEntrada.Enabled = false;
                    btnTransferir.Enabled = false;
                    lblMensagem.Text = "Não há locais de estoque disponíveis para esta conta.";
                }
            }
            catch (Exception)
            {
                lblMensagem.Text = "Não foi possível carregar os locais de estoque.";
                MessageBox.Show(
                    this,
                    "Não foi possível consultar os locais autorizados.",
                    "Estoque",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void RegistrarEntrada(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                AbrirEntradaDemo();
                return;
            }

            using (var formulario = new EntradaMercadoriaForm(usuarioAutenticado))
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    Atualizar();
                    lblMensagem.Text = "Entrada registrada. O saldo foi atualizado pelo sistema.";
                }
            }
        }

        private void TransferirEstoque(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                AbrirTransferenciaDemo();
                return;
            }

            using (var formulario = new TransferenciaEstoqueForm(usuarioAutenticado))
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    Atualizar();
                    lblMensagem.Text = "Transferência registrada. Os saldos foram atualizados pelo sistema.";
                }
            }
        }

        private void Organizar()
        {
            int altura = Math.Max(740, ClientSize.Height);
            panelSaldos.Top = panelHistorico.Top = 280;
            panelSaldos.Height = panelHistorico.Height = altura - 334;
            lblMensagem.Top = altura - 40;
        }

        private void SelecionarAba(bool historico)
        {
            panelSaldos.Visible = !historico;
            panelHistorico.Visible = historico;
            if (historico)
            {
                btnAbaSaldo.BackColor = System.Drawing.Color.White;
                btnAbaSaldo.ForeColor = VisualUtils.Texto;
                btnAbaHistorico.BackColor = VisualUtils.Azul;
                btnAbaHistorico.ForeColor = System.Drawing.Color.White;
                btnAbaSaldo.FlatAppearance.MouseOverBackColor = VisualUtils.Fundo;
                btnAbaHistorico.FlatAppearance.MouseOverBackColor = VisualUtils.Azul;
                btnAbaSaldo.AccessibleDescription = "Mostrar saldo";
                btnAbaHistorico.AccessibleDescription = "Histórico selecionado";
            }
            else
            {
                btnAbaSaldo.BackColor = VisualUtils.Azul;
                btnAbaSaldo.ForeColor = System.Drawing.Color.White;
                btnAbaHistorico.BackColor = System.Drawing.Color.White;
                btnAbaHistorico.ForeColor = VisualUtils.Texto;
                btnAbaSaldo.FlatAppearance.MouseOverBackColor = VisualUtils.Azul;
                btnAbaHistorico.FlatAppearance.MouseOverBackColor = VisualUtils.Fundo;
                btnAbaSaldo.AccessibleDescription = "Saldo selecionado";
                btnAbaHistorico.AccessibleDescription = "Mostrar histórico";
            }
        }

        private void cmbLocal_SelectedIndexChanged(object sender, EventArgs e)
        {
            Atualizar();
        }

        private void Atualizar()
        {
            if (dadosDemo != null)
            {
                AtualizarEstoqueDemo();
                return;
            }

            dgvSaldos.Rows.Clear();
            dgvHistorico.Rows.Clear();
            produtosPorId.Clear();

            LocalEstoque local = cmbLocal.SelectedItem as LocalEstoque;

            if (local == null || usuarioAutenticado == null)
            {
                return;
            }

            try
            {
                System.Data.DataTable produtos = new ConsultaOperacionalService().Estoques(usuarioAutenticado);
                foreach (System.Data.DataRow linhaProduto in produtos.Rows)
                {
                    Produto produto = new Produto();
                    produto.IdProduto = Convert.ToInt32(linhaProduto["id_produto"]);
                    produto.Nome = Convert.ToString(linhaProduto["produto"]);
                    produto.UnidadeMedida = Convert.ToString(linhaProduto["unidade_medida"]);
                    produtosPorId[produto.IdProduto] = produto;
                }

                List<Estoque> saldos = estoqueService.ListarPorLocal(
                    usuarioAutenticado,
                    local.IdLocalEstoque);
                int produtosComSaldo = 0;

                foreach (Estoque estoque in saldos)
                {
                    Produto produto;
                    string nomeProduto = "Produto " + estoque.IdProduto;
                    string unidade = string.Empty;

                    if (produtosPorId.TryGetValue(estoque.IdProduto, out produto))
                    {
                        nomeProduto = produto.Nome;
                        unidade = produto.UnidadeMedida;
                    }

                    int linha = dgvSaldos.Rows.Add(
                        nomeProduto,
                        estoque.QuantidadeAtual.ToString("N2", CultureInfo.GetCultureInfo("pt-BR")),
                        unidade);

                    dgvSaldos.Rows[linha].Tag = estoque;

                    if (estoque.QuantidadeAtual > 0)
                    {
                        produtosComSaldo++;
                    }
                }

                dgvSaldos.ClearSelection();
                System.Data.DataTable movimentos = new ConsultaOperacionalService().Movimentacoes(
                    usuarioAutenticado, DateTime.Today.AddDays(-30), DateTime.Today);
                foreach (System.Data.DataRow movimento in movimentos.Rows)
                {
                    int origem = Convert.ToInt32(movimento["id_origem"]);
                    int destino = Convert.ToInt32(movimento["id_destino"]);
                    if (origem != local.IdLocalEstoque && destino != local.IdLocalEstoque)
                    {
                        continue;
                    }
                    decimal quantidade = Convert.ToDecimal(movimento["quantidade"]);
                    if (origem == local.IdLocalEstoque)
                    {
                        quantidade = -quantidade;
                    }
                    dgvHistorico.Rows.Add(movimento["data_hora"], movimento["produto"],
                        movimento["tipo"], quantidade,
                        movimento["origem"] + " → " + movimento["destino"] + " / " + movimento["responsavel"]);
                }
                VisualUtils.ExibirEstadoVazio(dgvHistorico, "Nenhuma movimentação neste local nos últimos 30 dias.");
                lblResumo.Text = produtosComSaldo + " produto(s) com saldo neste local";
            }
            catch (UnauthorizedAccessException)
            {
                lblMensagem.Text = "Sua conta não tem acesso a este local.";
                dgvSaldos.Rows.Clear();
            }
            catch (Exception)
            {
                lblMensagem.Text = "Não foi possível carregar os saldos deste local.";
                dgvSaldos.Rows.Clear();
            }
        }
    }
}
