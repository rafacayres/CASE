using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE
{
    public partial class AcessosControl : UserControl
    {
        public event EventHandler AcessosAlterados;

        private readonly Usuario usuarioAutenticado;
        private readonly UsuarioService usuarioService = new UsuarioService();
        private readonly PessoaService pessoaService = new PessoaService();
        private Usuario selecionado;
        private List<PerfilSistema> perfis = new List<PerfilSistema>();

        public AcessosControl() : this(null)
        {
        }

        public AcessosControl(Usuario usuario)
        {
            InitializeComponent();

            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            usuarioAutenticado = usuario;
            FrontendTheme.Aplicar(this);
            lblModoApresentacao.Visible = false;
            VisualUtils.ExibirEstadoVazio(
                dgvContas,
                "Nenhuma conta encontrada para os filtros selecionados.");
            VisualUtils.AplicarIcone(btnPerfil, Properties.Resources.LucideAcaoAcessos);
            btnRejeitar.ForeColor = System.Drawing.Color.FromArgb(174, 55, 65);
            btnRejeitar.FlatAppearance.MouseOverBackColor =
                System.Drawing.Color.FromArgb(253, 238, 239);

            cmbSituacao.Items.Clear();
            cmbSituacao.Items.Add("Todas");
            cmbSituacao.Items.Add(SituacoesUsuario.Pendente);
            cmbSituacao.Items.Add(SituacoesUsuario.Aprovado);
            cmbSituacao.Items.Add(SituacoesUsuario.Rejeitado);
            cmbSituacao.SelectedIndex = 0;

            cmbPerfil.Items.Clear();
            cmbPerfil.DisplayMember = "Nome";
            cmbSituacao.SelectedIndexChanged += cmbSituacao_SelectedIndexChanged;
            txtBusca.TextChanged += txtBusca_TextChanged;
            dgvContas.SelectionChanged += dgvContas_SelectionChanged;
            btnAprovar.Click += btnAprovar_Click;
            btnRejeitar.Click += btnRejeitar_Click;
            btnSuspender.Enabled = false;
            btnPerfil.Enabled = false;

            if (usuarioAutenticado == null)
            {
                HabilitarLeitura(false);
                lblMensagem.Text = "Entre com uma conta de administrador para gerenciar cadastros.";
                return;
            }

            CarregarDados();
        }

        private void HabilitarLeitura(bool habilitado)
        {
            cmbSituacao.Enabled = habilitado;
            txtBusca.Enabled = habilitado;
            btnAprovar.Enabled = false;
            btnRejeitar.Enabled = false;
            btnPerfil.Enabled = false;
        }

        private void CarregarDados()
        {
            dgvContas.Rows.Clear();

            try
            {
                perfis = usuarioService.ListarPerfisDeSistema(usuarioAutenticado);
                cmbPerfil.Items.Clear();

                foreach (PerfilSistema perfil in perfis)
                {
                    cmbPerfil.Items.Add(perfil);
                }

                List<Usuario> usuarios = usuarioService.ListarUsuariosAdministrativos(
                    usuarioAutenticado);

                foreach (Usuario usuario in usuarios)
                {
                    Pessoa pessoa = pessoaService.BuscarPorId(
                        usuarioAutenticado,
                        usuario.IdPessoa);
                    string nome = "Pessoa " + usuario.IdPessoa;
                    string email = string.Empty;

                    if (pessoa != null)
                    {
                        nome = pessoa.Nome;
                        email = pessoa.Email;
                    }

                    string nomePerfil = ObterNomePerfil(usuario.IdPerfilSistema);
                    int indice = dgvContas.Rows.Add(
                        nome,
                        usuario.Login,
                        email,
                        usuario.SituacaoConta,
                        nomePerfil,
                        usuario.SolicitadoEm.ToString("dd/MM/yyyy"));
                    dgvContas.Rows[indice].Tag = usuario;
                }

                Selecionar();
                AtualizarBotoes();
                lblMensagem.Text = usuarios.Count + " conta(s) consultada(s).";
            }
            catch (UnauthorizedAccessException)
            {
                HabilitarLeitura(false);
                lblMensagem.Text = "Somente um administrador pode consultar contas.";
            }
            catch (Exception)
            {
                HabilitarLeitura(false);
                lblMensagem.Text = "Não foi possível carregar as contas.";
            }
        }

        private string ObterNomePerfil(int? idPerfilSistema)
        {
            if (!idPerfilSistema.HasValue)
            {
                return "Sem perfil";
            }

            foreach (PerfilSistema perfil in perfis)
            {
                if (perfil.IdPerfilSistema == idPerfilSistema.Value)
                {
                    return perfil.Nome;
                }
            }

            return "Perfil não encontrado";
        }

        private void cmbSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void AplicarFiltros()
        {
            if (cmbSituacao.SelectedIndex < 0)
            {
                return;
            }

            foreach (DataGridViewRow linha in dgvContas.Rows)
            {
                string situacao = Convert.ToString(linha.Cells[3].Value);
                string nome = Convert.ToString(linha.Cells[0].Value);
                string login = Convert.ToString(linha.Cells[1].Value);
                bool situacaoCorresponde = cmbSituacao.SelectedIndex == 0 ||
                    string.Equals(
                        situacao,
                        cmbSituacao.Text,
                        StringComparison.OrdinalIgnoreCase);
                bool buscaCorresponde = (nome + " " + login).IndexOf(
                    txtBusca.Text,
                    StringComparison.CurrentCultureIgnoreCase) >= 0;
                linha.Visible = situacaoCorresponde && buscaCorresponde;
            }

            dgvContas.ClearSelection();
            Selecionar();
        }

        private void dgvContas_SelectionChanged(object sender, EventArgs e)
        {
            Selecionar();
        }

        private void Selecionar()
        {
            if (dadosDemo != null)
            {
                SelecionarContaDemo();
                return;
            }

            selecionado = null;

            if (dgvContas.SelectedRows.Count > 0)
            {
                selecionado = dgvContas.SelectedRows[0].Tag as Usuario;
            }

            if (selecionado != null)
            {
                lblSelecionado.Text = Convert.ToString(
                    dgvContas.SelectedRows[0].Cells[0].Value) + " · " +
                    selecionado.SituacaoConta;
                txtMotivo.Text = string.Empty;
                SelecionarPerfil(selecionado.IdPerfilSistema);

                List<AnaliseUsuario> historico = usuarioService.ListarHistorico(
                    usuarioAutenticado,
                    selecionado.IdUsuario);

                if (historico.Count > 0)
                {
                    AnaliseUsuario ultimaAnalise = historico[historico.Count - 1];
                    txtMotivo.Text = ultimaAnalise.Justificativa;
                }
            }
            else
            {
                lblSelecionado.Text = "Selecione uma conta para analisar.";
                txtMotivo.Clear();
                cmbPerfil.SelectedIndex = -1;
            }

            AtualizarBotoes();
        }

        private void SelecionarPerfil(int? idPerfilSistema)
        {
            cmbPerfil.SelectedIndex = -1;

            if (!idPerfilSistema.HasValue)
            {
                return;
            }

            for (int indice = 0; indice < cmbPerfil.Items.Count; indice++)
            {
                PerfilSistema perfil = cmbPerfil.Items[indice] as PerfilSistema;

                if (perfil != null && perfil.IdPerfilSistema == idPerfilSistema.Value)
                {
                    cmbPerfil.SelectedIndex = indice;
                    return;
                }
            }
        }

        private void AtualizarBotoes()
        {
            if (dadosDemo != null)
            {
                SelecionarContaDemo();
                return;
            }

            bool pendente = selecionado != null && string.Equals(
                selecionado.SituacaoConta,
                SituacoesUsuario.Pendente,
                StringComparison.OrdinalIgnoreCase);
            cmbPerfil.Enabled = pendente;
            txtMotivo.Enabled = pendente;
            btnAprovar.Enabled = pendente && cmbPerfil.Items.Count > 0;
            btnRejeitar.Enabled = pendente;
            btnSuspender.Enabled = false;
            btnPerfil.Enabled = false;
        }

        private void btnAprovar_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                DecidirContaDemo(true);
                return;
            }

            if (selecionado == null || cmbPerfil.SelectedItem == null)
            {
                lblMensagem.Text = "Selecione a conta e o perfil de sistema que será atribuído.";
                return;
            }

            PerfilSistema perfil = (PerfilSistema)cmbPerfil.SelectedItem;

            try
            {
                usuarioService.AprovarCadastro(
                    usuarioAutenticado,
                    selecionado.IdUsuario,
                    perfil.IdPerfilSistema);
                AcessosAlterados?.Invoke(this, EventArgs.Empty);
                lblMensagem.Text = "Cadastro aprovado e decisão registrada.";
                CarregarDados();
            }
            catch (UnauthorizedAccessException)
            {
                lblMensagem.Text = "Sua conta não pode aprovar cadastros.";
            }
            catch (InvalidOperationException ex)
            {
                lblMensagem.Text = ex.Message;
                CarregarDados();
            }
            catch (Exception)
            {
                lblMensagem.Text = "Não foi possível aprovar o cadastro.";
            }
        }

        private void btnRejeitar_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                DecidirContaDemo(false);
                return;
            }

            if (selecionado == null)
            {
                lblMensagem.Text = "Selecione uma conta pendente.";
                return;
            }

            try
            {
                usuarioService.RejeitarCadastro(
                    usuarioAutenticado,
                    selecionado.IdUsuario,
                    txtMotivo.Text);
                AcessosAlterados?.Invoke(this, EventArgs.Empty);
                lblMensagem.Text = "Cadastro rejeitado e decisão registrada.";
                CarregarDados();
            }
            catch (ArgumentException ex)
            {
                lblMensagem.Text = ex.Message;
            }
            catch (UnauthorizedAccessException)
            {
                lblMensagem.Text = "Sua conta não pode rejeitar cadastros.";
            }
            catch (InvalidOperationException ex)
            {
                lblMensagem.Text = ex.Message;
                CarregarDados();
            }
            catch (Exception)
            {
                lblMensagem.Text = "Não foi possível rejeitar o cadastro.";
            }
        }
    }
}
