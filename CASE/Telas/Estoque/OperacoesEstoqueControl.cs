using System;
using System.Linq;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{
    public partial class OperacoesEstoqueControl : UserControl
    {
        public event EventHandler FornecedoresSolicitados;
        public OperacoesEstoqueControl()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            cmbLocal.Items.AddRange(OperacoesPreview.Locais);
            cmbLocal.SelectedIndex = 0;
            cmbLocal.SelectedIndexChanged += (s, e) => Atualizar();
            btnEntrada.Click += RegistrarEntrada;
            btnTransferir.Click += TransferirEstoque;
            btnFornecedores.Click += (s, e) => FornecedoresSolicitados?.Invoke(this, EventArgs.Empty);
            FrontendTheme.Aplicar(this);
            VisualUtils.ExibirEstadoVazio(dgvHistorico, "Este local ainda não tem movimentações registradas.");
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
            Atualizar();
        }

        private void RegistrarEntrada(object sender, EventArgs e)
        {
            using (var formulario = new EntradaMercadoriaForm())
            {
                if (formulario.ShowDialog(this) != DialogResult.OK) return;
                Atualizar();
                lblMensagem.Text = "Entrada confirmada. Consulte o saldo e o histórico do local.";
            }
        }

        private void TransferirEstoque(object sender, EventArgs e)
        {
            using (var formulario = new TransferenciaEstoqueForm())
            {
                if (formulario.ShowDialog(this) != DialogResult.OK) return;
                Atualizar();
                lblMensagem.Text = "Transferência concluída entre os estoques selecionados.";
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
            btnAbaSaldo.BackColor = historico ? System.Drawing.Color.White : VisualUtils.Azul;
            btnAbaSaldo.ForeColor = historico ? VisualUtils.Texto : System.Drawing.Color.White;
            btnAbaHistorico.BackColor = historico ? VisualUtils.Azul : System.Drawing.Color.White;
            btnAbaHistorico.ForeColor = historico ? System.Drawing.Color.White : VisualUtils.Texto;
            btnAbaSaldo.FlatAppearance.MouseOverBackColor = historico ? VisualUtils.Fundo : VisualUtils.Azul;
            btnAbaHistorico.FlatAppearance.MouseOverBackColor = historico ? VisualUtils.Azul : VisualUtils.Fundo;
            btnAbaSaldo.AccessibleDescription = historico ? "Mostrar saldo" : "Saldo selecionado";
            btnAbaHistorico.AccessibleDescription = historico ? "Histórico selecionado" : "Mostrar histórico";
        }

        private void Atualizar()
        {
            dgvSaldos.Rows.Clear();
            dgvHistorico.Rows.Clear();
            foreach (string produto in OperacoesPreview.Produtos)
                dgvSaldos.Rows.Add(produto, OperacoesPreview.Saldo(cmbLocal.Text, produto).ToString("N2"), "un");
            foreach (var movimento in OperacoesPreview.Movimentos.Where(m => m.Local == cmbLocal.Text).OrderByDescending(m => m.Data))
                dgvHistorico.Rows.Add(movimento.Data.ToString("dd/MM/yyyy HH:mm"), movimento.Produto, movimento.Tipo,
                    movimento.Quantidade.ToString("+0.##;-0.##;0"), movimento.Referencia);
            dgvSaldos.ClearSelection();
            dgvHistorico.ClearSelection();
            int disponiveis = OperacoesPreview.Produtos.Count(p => OperacoesPreview.Saldo(cmbLocal.Text, p) > 0);
            lblResumo.Text = disponiveis + " de " + OperacoesPreview.Produtos.Length + " produtos com saldo";
        }
    }
}
