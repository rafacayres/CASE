using System;
using System.Linq;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{
    public partial class AcessosControl : UserControl
    {
        public event EventHandler AcessosAlterados;
        private OperacoesPreview.Conta selecionada;
        public AcessosControl()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime) return;
            cmbSituacao.SelectedIndex = cmbPerfil.SelectedIndex = 0;
            cmbSituacao.SelectedIndexChanged += (s, e) => Atualizar();
            txtBusca.TextChanged += (s, e) => Atualizar();
            dgvContas.SelectionChanged += (s, e) => Selecionar();
            btnAprovar.Click += (s, e) => Decidir("Aprovado");
            btnRejeitar.Click += (s, e) => Decidir("Rejeitado");
            btnSuspender.Click += (s, e) => Decidir("Suspenso");
            btnPerfil.Click += (s, e) => Decidir(null);
            FrontendTheme.Aplicar(this);
            VisualUtils.ExibirEstadoVazio(dgvContas, "Nenhuma conta encontrada para os filtros selecionados.");
            VisualUtils.AplicarIcone(btnPerfil, Properties.Resources.LucideAcaoAcessos);
            btnRejeitar.ForeColor = System.Drawing.Color.FromArgb(174, 55, 65);
            btnRejeitar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(253, 238, 239);
            Atualizar();
        }

        private void Atualizar()
        {
            dgvContas.Rows.Clear();
            foreach (var conta in OperacoesPreview.Contas.Where(c => (cmbSituacao.SelectedIndex == 0 || c.Situacao == cmbSituacao.Text)
                && (c.Nome + " " + c.Login).IndexOf(txtBusca.Text, StringComparison.CurrentCultureIgnoreCase) >= 0))
            {
                int linha = dgvContas.Rows.Add(conta.Nome, conta.Login, conta.Email, conta.Situacao, conta.Perfil, conta.SolicitadoEm.ToString("dd/MM/yyyy"));
                dgvContas.Rows[linha].Tag = conta;
            }
            dgvContas.ClearSelection();
            selecionada = null;
            lblSelecionado.Text = "Selecione uma conta para analisar.";
            txtMotivo.Clear();
            AtualizarBotoes();
        }

        private void Selecionar()
        {
            selecionada = dgvContas.SelectedRows.Count == 0 ? null : dgvContas.SelectedRows[0].Tag as OperacoesPreview.Conta;
            if (selecionada != null)
            {
                lblSelecionado.Text = selecionada.Nome + " · " + selecionada.Situacao;
                cmbPerfil.SelectedItem = selecionada.Perfil;
                txtMotivo.Text = selecionada.Motivo;
            }
            AtualizarBotoes();
        }

        private void AtualizarBotoes()
        {
            cmbPerfil.Enabled = txtMotivo.Enabled = selecionada != null;
            btnAprovar.Text = selecionada != null && selecionada.Situacao == "Suspenso" ? "Reativar conta" : "Aprovar";
            btnAprovar.Enabled = selecionada != null && (selecionada.Situacao == "Pendente" || selecionada.Situacao == "Suspenso");
            btnRejeitar.Enabled = selecionada != null && selecionada.Situacao == "Pendente";
            btnSuspender.Enabled = btnPerfil.Enabled = selecionada != null && selecionada.Situacao == "Aprovado";
        }

        private void Decidir(string situacao)
        {
            if (selecionada == null)
            {
                lblMensagem.Text = "Selecione uma conta.";
                return;
            }
            if ((situacao == "Rejeitado" || situacao == "Suspenso") && string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                lblMensagem.Text = "Informe a justificativa antes de continuar.";
                return;
            }
            string novoPerfil = cmbPerfil.Text;
            if (selecionada.Situacao == "Aprovado" && selecionada.Perfil == "Administrador"
                && (situacao == "Suspenso" || novoPerfil != "Administrador")
                && OperacoesPreview.Contas.Count(c => c.Situacao == "Aprovado" && c.Perfil == "Administrador") == 1)
            {
                lblMensagem.Text = "Mantenha ao menos um administrador aprovado.";
                return;
            }
            if (situacao != null) selecionada.Situacao = situacao;
            if (situacao == null || situacao == "Aprovado") selecionada.Perfil = novoPerfil;
            selecionada.Motivo = txtMotivo.Text.Trim();
            Atualizar();
            lblMensagem.Text = "Permissões atualizadas. A função no evento permanece independente.";
            AcessosAlterados?.Invoke(this, EventArgs.Empty);
        }
    }
}
