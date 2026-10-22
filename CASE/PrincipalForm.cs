using System;

using System.Drawing;
using System.Linq;

using System.Windows.Forms;
using CASE.Utils;
using CASE.Quermesse;

namespace CASE
{
    public partial class PrincipalForm : Form
    {
        private readonly QuermessePreview exemplos = new QuermessePreview();



        public PrincipalForm()
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            AplicarAcabamento();

            VisualUtils.Arredondar(buttonDashboard, 8);
            VisualUtils.Arredondar(buttonEstoque, 8);
            VisualUtils.Arredondar(buttonRelatorios, 8);
            VisualUtils.Arredondar(buttonProdutos, 8);
            VisualUtils.Arredondar(buttonMovimentacoes, 8);

            VisualUtils.Arredondar(buttonConfiguracoes, 8);
            VisualUtils.Arredondar(buttonSair, 8);

            VisualUtils.Arredondar(panelPesquisa, 12);
            MinimumSize = new Size(1240, 790);
            Size = new Size(1320, 850);
            textPesquisa_Leave(this, EventArgs.Empty);
            buttonDashboard.Click += (s, e) => AbrirTela(buttonDashboard, () => new DashboardControl());
            buttonProdutos.Click += (s, e) => AbrirTela(buttonProdutos, () => new ProdutosControl());
            buttonEstoque.Click += (s, e) => AbrirTela(buttonEstoque, () => new EstoqueControl());
            buttonMovimentacoes.Click += (s, e) => AbrirTela(buttonMovimentacoes, () => new RegistrosControl(exemplos));
            buttonRelatorios.Click += (s, e) => AbrirTela(buttonRelatorios, () => new ApuracaoControl(exemplos));
            buttonConfiguracoes.Click += (s, e) => AbrirTela(buttonConfiguracoes, CriarConfiguracoes);
            buttonSair.Click += (s, e) => Close();
            ConfigurarQuermesse();
        }

        private void ConfigurarQuermesse()
        {
            buttonEquipeSetores.Click += (s, e) => AbrirTela(buttonEquipeSetores, () => new EquipeSetoresControl(exemplos));
            buttonFinanceiro.Click += (s, e) => AbrirTela(buttonFinanceiro, () => new FinanceiroControl(exemplos));
            buttonQuermesses.Click += (s, e) => AbrirTela(buttonQuermesses, () => new QuermessesControl(exemplos));
            buttonEscala.Click += (s, e) => AbrirTela(buttonEscala, () => new EscalaControl(exemplos));
            buttonSolicitacoes.Click += (s, e) => AbrirTela(buttonSolicitacoes, () => new SolicitacoesControl(exemplos));
            foreach (var botao in new[] { buttonEquipeSetores, buttonFinanceiro, buttonQuermesses, buttonEscala, buttonSolicitacoes })
            {
                VisualUtils.Arredondar(botao, 8);
                botao.SizeChanged += (s, e) => VisualUtils.Arredondar(botao, 8);
            }
            usuarioPreview.Items.AddRange(exemplos.Pessoas.Cast<object>().ToArray());
            usuarioPreview.SelectedItem = exemplos.Atual;
            usuarioPreview.SelectedIndexChanged += (s, e) =>
            {
                if (usuarioPreview.SelectedItem == null) return;
                exemplos.Atual = (PessoaPreview)usuarioPreview.SelectedItem;
                AtualizarCabecalho();
                AbrirTela(buttonDashboard, () => new DashboardControl());
            };
            AtualizarCabecalho();
            AbrirTela(buttonDashboard, () => new DashboardControl());
        }

        private void AplicarAcabamento()
        {
            BackColor = VisualUtils.Fundo;
            panelAreaPrincipal.BackColor = panelConteudo.BackColor = VisualUtils.Fundo;
            panelMenu.BackColor = panelRodapeMenu.BackColor = navegacao.BackColor = VisualUtils.Marinho;
            panelCabecalho.BackColor = Color.White;
            panelCabecalho.Paint += (s, e) =>
            {
                using (var linha = new Pen(VisualUtils.Borda))
                    e.Graphics.DrawLine(linha, 0, panelCabecalho.Height - 1, panelCabecalho.Width, panelCabecalho.Height - 1);
            };
            labelPreviewUsuario.Text = "PRÉVIA · USUÁRIO";
            labelPreviewUsuario.Font = new Font("Segoe UI", 8);
            labelPreviewUsuario.ForeColor = VisualUtils.TextoSuave;
            usuarioPreview.Width = 330;
            VisualUtils.EstilizarSelecao(usuarioPreview);
            labelNomeUsuario.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            labelNomeUsuario.ForeColor = VisualUtils.Texto;
            labelTipoUsuario.Font = new Font("Segoe UI", 8.5F);
            labelTipoUsuario.ForeColor = VisualUtils.TextoSuave;
            panelPerfil.Width = 260;
            panelPesquisa.BackColor = textPesquisa.BackColor = Color.FromArgb(247, 249, 252);
            panelPesquisa.Paint += VisualUtils.DesenharBorda;
            foreach (var botao in navegacao.Controls.OfType<Button>().Concat(new[] { buttonConfiguracoes, buttonSair }))
            {
                botao.BackColor = VisualUtils.Marinho;
                botao.ForeColor = Color.FromArgb(213, 225, 239);
                botao.Font = new Font("Segoe UI", 9.5F);
                botao.FlatAppearance.MouseOverBackColor = Color.FromArgb(26, 61, 98);
                botao.FlatAppearance.BorderSize = 0;
                botao.Cursor = Cursors.Hand;
                botao.Paint += (s, e) =>
                {
                    if (botao.BackColor != Color.FromArgb(27, 75, 125)) return;
                    using (var pincel = new SolidBrush(Color.FromArgb(89, 163, 255)))
                        e.Graphics.FillRectangle(pincel, 0, 10, 3, Math.Max(1, botao.Height - 20));
                };
            }
        }

        private void AtualizarCabecalho()
        {

            labelNomeUsuario.Text = exemplos.Atual.Nome;
            labelTipoUsuario.Text = exemplos.Atual.Perfil.ToString();
            // O perfil tem seu próprio painel; nomes longos não invadem o cabeçalho.
            labelNomeUsuario.AutoSize = false;
            labelNomeUsuario.Size = new Size(110, 20);
            labelNomeUsuario.AutoEllipsis = true;
            buttonMovimentacoes.Text = exemplos.Atual.Perfil == PerfilUsuario.Vendedor ? "Meus registros" : "Registros da equipe";
        }

        private UserControl CriarConfiguracoes()
        {
            var controle = new ConfiguracoesControl();
            foreach (string nome in new[] { "btnConfig1", "btnConfig2" })
            {
                var item = controle.Controls.Find(nome, true).FirstOrDefault();
                if (item != null) item.Visible = false;
            }
            ((TextBox)controle.Controls.Find("txtPerfilNome", true)[0]).Text = exemplos.Atual.Nome;
            ((TextBox)controle.Controls.Find("txtPerfilUsuario", true)[0]).Text = exemplos.Atual.Login;
            ((TextBox)controle.Controls.Find("txtPerfilEmail", true)[0]).Text = "";
            return controle;
        }

        private void SelecionarMenu(Button ativo)
        {
            foreach (Button botao in navegacao.Controls.OfType<Button>().Concat(new[] { buttonConfiguracoes }))
            {
                botao.BackColor = botao == ativo ? Color.FromArgb(27, 75, 125) : panelMenu.BackColor;
                botao.ForeColor = botao == ativo ? Color.White : Color.FromArgb(213, 225, 239);
                botao.FlatAppearance.MouseOverBackColor = Color.FromArgb(26, 61, 98);
            }
        }

        private void AbrirTela(Button menu, Func<UserControl> criar)
        {
            panelConteudo.SuspendLayout();
            while (panelConteudo.Controls.Count > 0) panelConteudo.Controls[0].Dispose();
            var tela = criar();
            tela.Dock = DockStyle.Fill;
            panelConteudo.Controls.Add(tela);
            panelConteudo.ResumeLayout();
            SelecionarMenu(menu);
        }

        private void textPesquisa_Leave(object sender, EventArgs e)
        {
            if (textPesquisa.Text == string.Empty)
            {
                textPesquisa.Text = "Pesquisar...";
                textPesquisa.ForeColor = Color.FromArgb(148, 163, 184);
            }
        }

        private void textPesquisa_Enter(object sender, EventArgs e)
        {
            if(textPesquisa.Text == "Pesquisar...")
            {
                textPesquisa.Text = string.Empty;
                textPesquisa.ForeColor = Color.FromArgb(18, 47, 80);
            }   
        }

        private void PrincipalForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

    }
}
