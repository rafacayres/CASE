using System;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{
    public partial class ConfiguracoesControl : UserControl
    {
        public event EventHandler GerenciarAcessosSolicitado;
        public ConfiguracoesControl()
        {
            InitializeComponent();
            CASE.Utils.FrontendTheme.Aplicar(this);
            VisualUtils.Arredondar(panelPerfilUsuario, 12);
            panelPerfilUsuario.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelPerfilUsuario);
            VisualUtils.Arredondar(btnSalvarPerfil, 8);
            btnSalvarPerfil.SizeChanged += AjustarArredondamento;
            VisualUtils.Arredondar(panelPreferencias, 12);
            panelPreferencias.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelPreferencias);
            VisualUtils.Arredondar(btnSalvarPreferencias, 8);
            btnSalvarPreferencias.SizeChanged += AjustarArredondamento;
            VisualUtils.Arredondar(panelSolicitacoesPendentes, 12);
            panelSolicitacoesPendentes.SizeChanged += AjustarArredondamento;
            VisualUtils.AplicarBorda(panelSolicitacoesPendentes);
            VisualUtils.Arredondar(btnAprovarSolicitacao, 8);
            btnAprovarSolicitacao.SizeChanged += AjustarArredondamento;
            VisualUtils.Arredondar(btnRejeitarSolicitacao, 8);
            btnRejeitarSolicitacao.SizeChanged += AjustarArredondamento;
            dgvSolicitacoes.Rows.Add(new object[] { "Marina Souza", "marina.souza", "marina@example.com", "01/10/2026", "Pendente" });
            dgvSolicitacoes.Rows.Add(new object[] { "Pedro Lima", "pedro.lima", "pedro@example.com", "02/10/2026", "Pendente" });
            dgvSolicitacoes.ClearSelection();
            lblSubtituloConfiguracoes.Text = "Gerencie suas informações e preferências";
            txtPerfilNome.Text = "Rafael Cayres";
            txtPerfilUsuario.Text = "rafael";
            txtPerfilEmail.Text = "rafael@case.com";
            var menu = new FlowLayoutPanel { Name = "panelMenuConfiguracoes",
                Bounds = new System.Drawing.Rectangle(30, 115, 180, 320), FlowDirection = FlowDirection.TopDown };
            Controls.Add(menu);
            panelPerfilUsuario.SetBounds(230, 115, 710, 360);
            panelPreferencias.SetBounds(230, 115, 710, 360);
            panelSolicitacoesPendentes.MinimumSize = System.Drawing.Size.Empty;
            panelSolicitacoesPendentes.SetBounds(230, 115, 710, 360);
            dgvSolicitacoes.Width = 670;
            btnAprovarSolicitacao.Left = 460;
            btnRejeitarSolicitacao.Left = 575;
            Label[] titulos = { lblPerfilNome, lblPerfilUsuario, lblPerfilEmail };
            TextBox[] campos = { txtPerfilNome, txtPerfilUsuario, txtPerfilEmail };
            for (int i = 0; i < campos.Length; i++)
            {
                titulos[i].SetBounds(24, 52 + i * 75, 620, 23);
                campos[i].SetBounds(24, 77 + i * 75, 660, 28);
            }
            btnSalvarPerfil.SetBounds(514, 304, 170, 36);
            btnSalvarPerfil.Click += (s, e) => MessageBox.Show(this, "Prévia visual: alterações não são persistidas.", "Perfil");
            btnSalvarPreferencias.Click += (s, e) => MessageBox.Show(this, "Prévia visual: preferências não são persistidas.", "Preferências");
            var categorias = new Panel { Name = "panelCategorias", BackColor = System.Drawing.Color.White,
                Bounds = panelPerfilUsuario.Bounds, Anchor = panelPerfilUsuario.Anchor };
            categorias.Controls.Add(FrontendTheme.Legenda("Categorias de produtos", new System.Drawing.Rectangle(24, 20, 400, 30)));
            var lista = new ListBox { Name = "listCategorias", BorderStyle = BorderStyle.None,
                Bounds = new System.Drawing.Rectangle(24, 66, 650, 220), Font = new System.Drawing.Font("Segoe UI", 11), ItemHeight = 40 };
            lista.Items.AddRange(new object[] { "Informática", "Móveis", "Eletrônicos", "Outros" });
            categorias.Controls.Add(lista);
            Controls.Add(categorias);
            Panel[] paginas = { panelPerfilUsuario, panelSolicitacoesPendentes, categorias, panelPreferencias };
            string[] nomes = { "Perfil", "Usuários", "Categorias", "Preferências" };
            var botoes = new System.Collections.Generic.List<Button>();
            for (int i = 0; i < nomes.Length; i++)
            {
                Panel pagina = paginas[i];
                var botao = new Button { Name = "btnConfig" + i, Text = nomes[i], Width = 170, Height = 40,
                    FlatStyle = FlatStyle.Flat, ForeColor = FrontendTheme.Texto, BackColor = BackColor,
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft, Cursor = Cursors.Hand };
                botao.FlatAppearance.BorderSize = 0;
                menu.Controls.Add(botao);
                botoes.Add(botao);
                botao.Click += (s, e) =>
                {
                    if (pagina == panelSolicitacoesPendentes)
                    {
                        GerenciarAcessosSolicitado?.Invoke(this, EventArgs.Empty);
                        return;
                    }
                    foreach (Panel item in paginas) item.Visible = item == pagina;
                    foreach (Button item in botoes) item.BackColor = item == botao ? System.Drawing.Color.FromArgb(215, 233, 255) : BackColor;
                };
            }
            botoes[0].PerformClick();
        }

        private void AjustarArredondamento(object sender, EventArgs e)
        {
            Control controle = (Control)sender;
            VisualUtils.Arredondar(controle, controle is Panel ? 12 : 8);
            controle.Invalidate();
        }
    }
}

