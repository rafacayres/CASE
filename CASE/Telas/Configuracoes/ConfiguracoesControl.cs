using System;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE
{
    public partial class ConfiguracoesControl : UserControl
    {
        public event EventHandler GerenciarAcessosSolicitado;
        private readonly Usuario usuarioAutenticado;
        private readonly PessoaService pessoaService = new PessoaService();
        private readonly Label lblMensagemPerfil = new Label();
        private bool administrador;

        public ConfiguracoesControl() : this(null)
        {
        }

        public ConfiguracoesControl(Usuario usuario)
        {
            InitializeComponent();
            usuarioAutenticado = usuario;
            if (usuario != null)
            {
                try
                {
                    string perfil;
                    new AutorizacaoService().ValidarUsuarioEObterPerfil(usuario, out perfil);
                    administrador = perfil == "Administrador";
                }
                catch (Exception)
                {
                    administrador = false;
                }
            }
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
            dgvSolicitacoes.ClearSelection();
            lblSubtituloConfiguracoes.Text = "Gerencie suas informações e preferências";
            txtPerfilNome.Clear();
            txtPerfilUsuario.Clear();
            txtPerfilEmail.Clear();
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
            txtPerfilUsuario.ReadOnly = true;
            txtPerfilUsuario.TabStop = false;

            lblMensagemPerfil.Name = "lblMensagemPerfil";
            lblMensagemPerfil.SetBounds(24, 342, 660, 22);
            lblMensagemPerfil.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            lblMensagemPerfil.ForeColor = VisualUtils.TextoSuave;
            lblMensagemPerfil.Visible = false;
            panelPerfilUsuario.Controls.Add(lblMensagemPerfil);

            btnSalvarPerfil.Click += BtnSalvarPerfil_Click;
            btnSalvarPreferencias.Enabled = false;
            btnSalvarPreferencias.AccessibleDescription =
                "O banco atual não possui armazenamento para estas preferências.";

            CarregarMeusDados();
            var categorias = new Panel { Name = "panelCategorias", BackColor = System.Drawing.Color.White,
                Bounds = panelPerfilUsuario.Bounds, Anchor = panelPerfilUsuario.Anchor };
            categorias.Controls.Add(FrontendTheme.Legenda("Categorias de produtos", new System.Drawing.Rectangle(24, 20, 400, 30)));
            var lista = new ListBox { Name = "listCategorias", BorderStyle = BorderStyle.None,
                Bounds = new System.Drawing.Rectangle(24, 66, 650, 220), Font = new System.Drawing.Font("Segoe UI", 11), ItemHeight = 40 };
            if (administrador)
            {
                CarregarCategorias(lista);
            }
            categorias.Controls.Add(lista);
            ConfigurarCadastroCategorias(categorias, lista);
            Controls.Add(categorias);
            Panel tipos = CriarCadastroTiposEvento(categorias.Bounds);
            Controls.Add(tipos);
            Panel instituicao = CriarConfiguracaoInstituicao(categorias.Bounds);
            Panel turnos = CriarCadastroApoio(categorias.Bounds, true);
            Panel funcoes = CriarCadastroApoio(categorias.Bounds, false);
            Controls.Add(instituicao);
            Controls.Add(turnos);
            Controls.Add(funcoes);
            menu.Height = 440;
            Panel[] paginas = { panelPerfilUsuario, instituicao, categorias, tipos, turnos, funcoes, panelSolicitacoesPendentes, panelPreferencias };
            string[] nomes = { "Perfil", "Instituição", "Categorias", "Tipos de evento", "Turnos", "Funções na equipe", "Usuários", "Preferências" };
            var botoes = new System.Collections.Generic.List<Button>();
            for (int i = 0; i < nomes.Length; i++)
            {
                Panel pagina = paginas[i];
                var botao = new Button { Name = "btnConfig" + i, Text = nomes[i], Width = 170, Height = 40,
                    FlatStyle = FlatStyle.Flat, ForeColor = FrontendTheme.Texto, BackColor = BackColor,
                    TextAlign = System.Drawing.ContentAlignment.MiddleLeft, Cursor = Cursors.Hand };
                botao.FlatAppearance.BorderSize = 0;
                if (!administrador && pagina != panelPerfilUsuario && pagina != panelPreferencias)
                {
                    botao.Visible = false;
                }
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
                    foreach (Button item in botoes)
                    {
                        if (item == botao)
                        {
                            item.BackColor = System.Drawing.Color.FromArgb(215, 233, 255);
                        }
                        else
                        {
                            item.BackColor = BackColor;
                        }
                    }
                };
            }
            botoes[0].PerformClick();
        }

        private void CarregarMeusDados()
        {
            if (usuarioAutenticado == null)
            {
                txtPerfilNome.Enabled = false;
                txtPerfilUsuario.Enabled = false;
                txtPerfilEmail.Enabled = false;
                btnSalvarPerfil.Enabled = false;
                ExibirMensagemPerfil(
                    "Entre no sistema para consultar e alterar seus dados pessoais.",
                    true);
                return;
            }

            try
            {
                Pessoa pessoa = pessoaService.BuscarMeusDados(usuarioAutenticado);
                txtPerfilNome.Text = pessoa.Nome;
                txtPerfilUsuario.Text = usuarioAutenticado.Login;
                txtPerfilEmail.Text = pessoa.Email;
                ExibirMensagemPerfil(string.Empty, false);
            }
            catch (UnauthorizedAccessException)
            {
                btnSalvarPerfil.Enabled = false;
                ExibirMensagemPerfil("Sua sessão não permite consultar estes dados.", true);
            }
            catch (Exception)
            {
                btnSalvarPerfil.Enabled = false;
                ExibirMensagemPerfil("Não foi possível carregar os dados do perfil.", true);
            }
        }

        private void BtnSalvarPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                pessoaService.AtualizarMeusDados(
                    usuarioAutenticado,
                    txtPerfilNome.Text,
                    txtPerfilEmail.Text);

                ExibirMensagemPerfil("Seus dados foram atualizados.", false);
                MessageBox.Show(
                    this,
                    "Seus dados pessoais foram atualizados.",
                    "Perfil",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (ArgumentException erro)
            {
                ExibirMensagemPerfil(erro.Message, true);
            }
            catch (UnauthorizedAccessException)
            {
                ExibirMensagemPerfil("Sua sessão não permite alterar estes dados.", true);
            }
            catch (Exception)
            {
                ExibirMensagemPerfil("Não foi possível salvar os dados do perfil.", true);
            }
        }

        private void ExibirMensagemPerfil(string mensagem, bool erro)
        {
            lblMensagemPerfil.Text = mensagem;
            lblMensagemPerfil.ForeColor = erro
                ? System.Drawing.Color.FromArgb(190, 55, 65)
                : System.Drawing.Color.FromArgb(43, 133, 104);
            lblMensagemPerfil.Visible = !string.IsNullOrWhiteSpace(mensagem);
        }

        private void AjustarArredondamento(object sender, EventArgs e)
        {
            Control controle = (Control)sender;
            int arredondamento;

            if (controle is Panel)
            {
                arredondamento = 12;
            }
            else
            {
                arredondamento = 8;
            }

            VisualUtils.Arredondar(controle, arredondamento);
            controle.Invalidate();
        }

        private void CarregarCategorias(ListBox lista)
        {
            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime)
            {
                return;
            }

            try
            {
                lista.Items.Clear();
                lista.DisplayMember = "Nome";
                CategoriaService categoriaService = new CategoriaService();
                var categorias = categoriaService.Listar(usuarioAutenticado);

                foreach (Categoria categoria in categorias)
                {
                    lista.Items.Add(categoria);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(
                    this,
                    "Não foi possível carregar as categorias.",
                    "Configurações",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigurarCadastroCategorias(Panel painel, ListBox lista)
        {
            TextBox nome = new TextBox();
            nome.Name = "txtNomeCategoria";
            nome.MaxLength = 100;
            nome.SetBounds(24, 290, 400, 28);
            Button novo = new Button();
            novo.Text = "Nova";
            novo.SetBounds(434, 286, 100, 36);
            Button salvar = new Button();
            salvar.Text = "Salvar";
            salvar.SetBounds(544, 286, 130, 36);
            novo.Click += (s, e) =>
            {
                lista.ClearSelected();
                nome.Clear();
                nome.Focus();
            };
            lista.SelectedIndexChanged += (s, e) =>
            {
                Categoria categoria = lista.SelectedItem as Categoria;
                if (categoria != null)
                {
                    nome.Text = categoria.Nome;
                }
            };
            salvar.Click += (s, e) =>
            {
                try
                {
                    Categoria categoria = lista.SelectedItem as Categoria;
                    bool existente = categoria != null;
                    if (categoria == null)
                    {
                        categoria = new Categoria();
                    }
                    categoria.Nome = nome.Text.Trim();
                    CategoriaService service = new CategoriaService();
                    if (existente)
                    {
                        service.Atualizar(usuarioAutenticado, categoria);
                    }
                    else
                    {
                        service.Cadastrar(usuarioAutenticado, categoria);
                    }
                    CarregarCategorias(lista);
                    nome.Clear();
                }
                catch (Exception erro)
                {
                    MostrarErroCadastro(erro);
                }
            };
            painel.Controls.Add(nome);
            painel.Controls.Add(novo);
            painel.Controls.Add(salvar);
            FrontendTheme.Aplicar(painel);
        }

        private Panel CriarCadastroTiposEvento(System.Drawing.Rectangle area)
        {
            Panel painel = new Panel();
            painel.Name = "panelTiposEvento";
            painel.Bounds = area;
            painel.BackColor = System.Drawing.Color.White;
            ListBox lista = new ListBox();
            lista.DisplayMember = "Nome";
            lista.SetBounds(24, 56, 650, 220);
            TextBox nome = new TextBox();
            nome.MaxLength = 100;
            nome.SetBounds(24, 290, 400, 28);
            Button novo = new Button();
            novo.Text = "Novo";
            novo.SetBounds(434, 286, 100, 36);
            Button salvar = new Button();
            salvar.Text = "Salvar";
            salvar.SetBounds(544, 286, 130, 36);
            TipoEventoService service = new TipoEventoService();
            EventHandler carregar = (s, e) =>
            {
                lista.Items.Clear();
                foreach (TipoEvento tipo in service.Listar(usuarioAutenticado))
                {
                    lista.Items.Add(tipo);
                }
            };
            lista.SelectedIndexChanged += (s, e) =>
            {
                TipoEvento tipo = lista.SelectedItem as TipoEvento;
                if (tipo != null)
                {
                    nome.Text = tipo.Nome;
                }
            };
            novo.Click += (s, e) =>
            {
                lista.ClearSelected();
                nome.Clear();
                nome.Focus();
            };
            salvar.Click += (s, e) =>
            {
                try
                {
                    TipoEvento tipo = lista.SelectedItem as TipoEvento;
                    bool existente = tipo != null;
                    if (tipo == null)
                    {
                        tipo = new TipoEvento();
                    }
                    tipo.Nome = nome.Text.Trim();
                    if (existente)
                    {
                        service.Atualizar(usuarioAutenticado, tipo);
                    }
                    else
                    {
                        service.Cadastrar(usuarioAutenticado, tipo);
                    }
                    carregar(this, EventArgs.Empty);
                    nome.Clear();
                }
                catch (Exception erro)
                {
                    MostrarErroCadastro(erro);
                }
            };
            painel.Controls.Add(FrontendTheme.Legenda("Tipos de evento", new System.Drawing.Rectangle(24, 20, 500, 30)));
            painel.Controls.Add(lista);
            painel.Controls.Add(nome);
            painel.Controls.Add(novo);
            painel.Controls.Add(salvar);
            FrontendTheme.Aplicar(painel);
            if (administrador)
            {
                try
                {
                    carregar(this, EventArgs.Empty);
                }
                catch (Exception erro)
                {
                    MostrarErroCadastro(erro);
                }
            }
            return painel;
        }

        private Panel CriarConfiguracaoInstituicao(System.Drawing.Rectangle area)
        {
            Panel painel = new Panel();
            painel.Name = "panelInstituicao";
            painel.Bounds = area;
            painel.BackColor = System.Drawing.Color.White;
            painel.Controls.Add(FrontendTheme.Legenda("1. Instituição e estoque central", new System.Drawing.Rectangle(24, 16, 600, 30)));
            string[] titulos = { "Nome da instituição *", "CPF/CNPJ (opcional)", "Telefone (opcional)", "E-mail (opcional)" };
            int[] limites = { 150, 18, 20, 150 };
            TextBox[] campos = new TextBox[4];
            for (int i = 0; i < campos.Length; i++)
            {
                painel.Controls.Add(FrontendTheme.Legenda(titulos[i], new System.Drawing.Rectangle(24, 48 + i * 56, 600, 22)));
                campos[i] = new TextBox();
                campos[i].MaxLength = limites[i];
                campos[i].SetBounds(24, 71 + i * 56, 650, 25);
                painel.Controls.Add(campos[i]);
            }
            Button salvar = new Button();
            salvar.Text = "Salvar instituição";
            salvar.SetBounds(24, 292, 200, 36);
            Button central = new Button();
            central.Text = "Inicializar estoque central";
            central.SetBounds(240, 292, 270, 36);
            Label mensagem = FrontendTheme.Legenda("Salve a instituição e inicialize seu estoque central uma única vez.",
                new System.Drawing.Rectangle(24, 334, 650, 23));
            Instituicao atual = null;
            ConfiguracaoInstitucionalService service = new ConfiguracaoInstitucionalService();
            if (administrador)
            {
                try
                {
                    atual = service.ObterInstituicao(usuarioAutenticado);
                    if (atual != null)
                    {
                        campos[0].Text = atual.Nome;
                        campos[1].Text = atual.Documento;
                        campos[2].Text = atual.Telefone;
                        campos[3].Text = atual.Email;
                    }
                }
                catch (Exception erro)
                {
                    MostrarErroCadastro(erro);
                }
            }
            salvar.Enabled = administrador;
            central.Enabled = administrador;
            salvar.Click += (s, e) =>
            {
                try
                {
                    Instituicao item = new Instituicao();
                    if (atual != null)
                    {
                        item.IdInstituicao = atual.IdInstituicao;
                    }
                    item.Nome = campos[0].Text;
                    item.Documento = campos[1].Text;
                    item.Telefone = campos[2].Text;
                    item.Email = campos[3].Text;
                    item.IdInstituicao = service.SalvarInstituicao(usuarioAutenticado, item);
                    atual = item;
                    mensagem.Text = "Instituição salva. Você já pode inicializar o estoque central.";
                }
                catch (Exception erro)
                {
                    MostrarErroCadastro(erro);
                }
            };
            central.Click += (s, e) =>
            {
                try
                {
                    service.InicializarCentral(usuarioAutenticado);
                    mensagem.Text = "Estoque central disponível. Repetir esta ação não cria outro local.";
                }
                catch (Exception erro)
                {
                    MostrarErroCadastro(erro);
                }
            };
            painel.Controls.Add(salvar);
            painel.Controls.Add(central);
            painel.Controls.Add(mensagem);
            FrontendTheme.Aplicar(painel);
            return painel;
        }

        private Panel CriarCadastroApoio(System.Drawing.Rectangle area, bool ehTurno)
        {
            Panel painel = new Panel();
            painel.Bounds = area;
            painel.BackColor = System.Drawing.Color.White;
            string titulo = "Funções de participação na equipe (não são perfis de acesso)";
            if (ehTurno)
            {
                titulo = "Turnos utilizados nas jornadas e escalas";
            }
            painel.Controls.Add(FrontendTheme.Legenda(titulo, new System.Drawing.Rectangle(24, 16, 650, 30)));
            ListBox lista = new ListBox();
            lista.DisplayMember = "Nome";
            lista.SetBounds(24, 56, 650, 220);
            TextBox nome = new TextBox();
            nome.MaxLength = 100;
            if (ehTurno)
            {
                nome.MaxLength = 50;
            }
            nome.SetBounds(24, 290, 400, 28);
            Button novo = new Button();
            novo.Text = "Novo";
            novo.SetBounds(434, 286, 100, 36);
            Button salvar = new Button();
            salvar.Text = "Salvar";
            salvar.SetBounds(544, 286, 130, 36);
            salvar.Enabled = administrador;
            ConfiguracaoInstitucionalService service = new ConfiguracaoInstitucionalService();
            EventHandler carregar = (s, e) =>
            {
                lista.Items.Clear();
                if (ehTurno)
                {
                    foreach (Turno item in service.ListarTurnos(usuarioAutenticado))
                    {
                        lista.Items.Add(item);
                    }
                }
                else
                {
                    foreach (Perfil item in service.ListarFuncoesEquipe(usuarioAutenticado))
                    {
                        lista.Items.Add(item);
                    }
                }
            };
            lista.SelectedIndexChanged += (s, e) => nome.Text = lista.GetItemText(lista.SelectedItem);
            novo.Click += (s, e) =>
            {
                lista.ClearSelected();
                nome.Clear();
                nome.Focus();
            };
            salvar.Click += (s, e) =>
            {
                try
                {
                    if (ehTurno)
                    {
                        Turno anterior = lista.SelectedItem as Turno;
                        Turno item = new Turno();
                        if (anterior != null)
                        {
                            item.IdTurno = anterior.IdTurno;
                        }
                        item.Nome = nome.Text.Trim();
                        service.SalvarTurno(usuarioAutenticado, item);
                    }
                    else
                    {
                        Perfil anterior = lista.SelectedItem as Perfil;
                        Perfil item = new Perfil();
                        if (anterior != null)
                        {
                            item.IdPerfil = anterior.IdPerfil;
                        }
                        item.Nome = nome.Text.Trim();
                        service.SalvarFuncaoEquipe(usuarioAutenticado, item);
                    }
                    carregar(this, EventArgs.Empty);
                    nome.Clear();
                }
                catch (Exception erro)
                {
                    MostrarErroCadastro(erro);
                }
            };
            painel.Controls.Add(lista);
            painel.Controls.Add(nome);
            painel.Controls.Add(novo);
            painel.Controls.Add(salvar);
            FrontendTheme.Aplicar(painel);
            if (administrador)
            {
                try
                {
                    carregar(this, EventArgs.Empty);
                }
                catch (Exception erro)
                {
                    MostrarErroCadastro(erro);
                }
            }
            return painel;
        }

        private void MostrarErroCadastro(Exception erro)
        {
            string mensagem = "Não foi possível salvar ou consultar este cadastro.";
            if (erro is ArgumentException || erro is InvalidOperationException || erro is UnauthorizedAccessException)
            {
                mensagem = erro.Message;
            }
            MessageBox.Show(this, mensagem, "Configurações", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

