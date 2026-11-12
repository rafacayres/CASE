using System;

using System.Drawing;
using System.IO;
using System.Linq;

using System.Windows.Forms;
using System.Collections.Generic;
using CASE.Utils;
using CASE.Quermesse;
using CASE.Models;

namespace CASE
{
    public partial class PrincipalForm : Form
    {
        private readonly Usuario usuarioAutenticado;
        private Evento eventoSelecionado;
        private Button buttonFornecedores;
        private Button buttonAcessos;
        private readonly ToolTip dicaNotificacoes = new ToolTip();
        private readonly ToolTip dicaNomeUsuario = new ToolTip();
        private readonly Label indicadorNotificacoes = new Label();
        private readonly ContextMenuStrip menuFotoPerfil = new ContextMenuStrip();
        private readonly CASE.Services.FotoPerfilService fotoPerfilService =
            new CASE.Services.FotoPerfilService();
        private Image fotoPerfilPersonalizada;



        public PrincipalForm() : this(null)
        {
        }

        internal PrincipalForm(Usuario usuario)
        {
            InitializeComponent();
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            IconoAplicacao.Aplicar(this);

            if (usuario == null)
            {
                throw new InvalidOperationException("É necessário autenticar uma conta autorizada.");
            }

            CASE.Services.SessaoUsuario.ExigirUsuario(usuario);

            if (!usuario.IdPerfilSistema.HasValue)
            {
                throw new InvalidOperationException("A conta não possui um perfil de acesso.");
            }

            if (!string.Equals(
                usuario.SituacaoConta,
                "Aprovado",
                StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("A conta ainda não foi autorizada.");
            }

            usuarioAutenticado = usuario;
            ConfigurarFotoPerfil();
            ConfigurarNovosCadastros();
            AplicarAcabamento();
            ConfigurarNotificacoes();

            VisualUtils.Arredondar(buttonDashboard, 8);
            VisualUtils.Arredondar(buttonEstoque, 8);
            VisualUtils.Arredondar(buttonRelatorios, 8);
            VisualUtils.Arredondar(buttonProdutos, 8);
            VisualUtils.Arredondar(buttonMovimentacoes, 8);

            VisualUtils.Arredondar(buttonConfiguracoes, 8);
            VisualUtils.Arredondar(buttonSair, 8);

            VisualUtils.Arredondar(panelPesquisa, 12);
            Rectangle areaDisponivel = Screen.FromControl(this).WorkingArea;
            MinimumSize = new Size(Math.Min(1100, areaDisponivel.Width), Math.Min(680, areaDisponivel.Height));
            Size = new Size(Math.Min(1320, areaDisponivel.Width), Math.Min(850, areaDisponivel.Height));
            textPesquisa_Leave(this, EventArgs.Empty);
            textPesquisa.TextChanged += textPesquisa_TextChanged;
            buttonDashboard.Click += (s, e) => AbrirTela(buttonDashboard, () => new DashboardControl(usuarioAutenticado));
            buttonProdutos.Click += (s, e) => AbrirTela(buttonProdutos, () => new ProdutosControl(usuarioAutenticado));
            buttonEstoque.Click += (s, e) => AbrirTela(buttonEstoque, CriarOperacoesEstoque);
            buttonMovimentacoes.Click += (s, e) => AbrirTela(buttonMovimentacoes, () => new RegistrosControl(usuarioAutenticado, eventoSelecionado));
            buttonRelatorios.Text = "Relatórios";
            buttonRelatorios.Click += (s, e) => AbrirTela(buttonRelatorios, () => new RelatoriosControl(usuarioAutenticado));
            buttonConfiguracoes.Click += (s, e) => AbrirTela(buttonConfiguracoes, CriarConfiguracoes);
            buttonSair.Click += (s, e) => Close();
            ConfigurarQuermesse();
            ConfigurarModoDemonstracao();
        }

        private void ConfigurarQuermesse()
        {
            buttonEquipeSetores.Click += (s, e) => AbrirTela(
                buttonEquipeSetores,
                () => new EquipeSetoresControl(usuarioAutenticado, eventoSelecionado));
            buttonFinanceiro.Click += (s, e) => AbrirTela(buttonFinanceiro, () => new FinanceiroControl(usuarioAutenticado, eventoSelecionado));
            buttonQuermesses.Click += (s, e) => AbrirTela(buttonQuermesses, CriarTelaEventos);
            buttonEscala.Click += (s, e) => AbrirTela(
                buttonEscala,
                () => new EscalaControl(usuarioAutenticado, eventoSelecionado));
            buttonSolicitacoes.Click += (s, e) => AbrirTela(
                buttonSolicitacoes,
                () => new SolicitacoesControl(usuarioAutenticado, eventoSelecionado));
            foreach (var botao in new[] { buttonEquipeSetores, buttonFinanceiro, buttonQuermesses, buttonEscala, buttonSolicitacoes })
            {
                VisualUtils.Arredondar(botao, 8);
                botao.SizeChanged += (s, e) => VisualUtils.Arredondar(botao, 8);
            }
            usuarioPreview.Visible = false;
            labelPreviewUsuario.Visible = false;
            AtualizarCabecalho();
            AbrirTela(buttonDashboard, () => new DashboardControl(usuarioAutenticado));
        }

        private UserControl CriarTelaEventos()
        {
            QuermessesControl tela = new QuermessesControl(usuarioAutenticado);
            tela.EventoSelecionado += AtualizarEventoSelecionado;
            AtualizarEventoSelecionado(tela.ObterEventoSelecionado());
            return tela;
        }

        private void AtualizarEventoSelecionado(Evento evento)
        {
            eventoSelecionado = evento;
        }

        private void ConfigurarNovosCadastros()
        {
            buttonFornecedores = CriarBotaoMenu("buttonFornecedores", "Fornecedores");
            buttonAcessos = CriarBotaoMenu("buttonAcessos", "Usuários e acessos");
            AdicionarIconeMenu(buttonFornecedores, Properties.Resources.LucideMenuFornecedores);
            AdicionarIconeMenu(buttonAcessos, Properties.Resources.LucideMenuAcessos);
            buttonAcessos.Visible = false;
            buttonFornecedores.Click += (s, e) => AbrirTela(buttonFornecedores, () => new FornecedoresControl(usuarioAutenticado));
            buttonAcessos.Click += (s, e) => AbrirTela(buttonAcessos, CriarGestaoAcessos);
            Button historico = CriarBotaoMenu("buttonHistorico", "Movimentações");
            AdicionarIconeMenu(historico, Properties.Resources.LucideAzulRegistros);
            historico.Click += (s, e) => AbrirTela(historico, () => new MovimentacoesControl(usuarioAutenticado));
        }

        private Button CriarBotaoMenu(string nome, string texto)
        {
            var botao = new Button { Name = nome, Text = texto, Size = buttonEstoque.Size,
                Margin = buttonEstoque.Margin, Padding = buttonEstoque.Padding,
                TextAlign = buttonEstoque.TextAlign, FlatStyle = FlatStyle.Flat,
                BackColor = VisualUtils.Marinho, ForeColor = Color.White };
            navegacao.Controls.Add(botao);
            VisualUtils.Arredondar(botao, 8);
            return botao;
        }

        private void AdicionarIconeMenu(Button botao, Image imagem)
        {
            var icone = new PictureBox { Name = "icone" + botao.Name, Image = imagem,
                Bounds = new Rectangle(12, 9, 20, 20), SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent, Enabled = false, TabStop = false };
            botao.Controls.Add(icone);
        }

        private UserControl CriarOperacoesEstoque()
        {
            var tela = new OperacoesEstoqueControl(usuarioAutenticado);
            tela.FornecedoresSolicitados += (s, e) => AbrirTela(buttonFornecedores, () => new FornecedoresControl(usuarioAutenticado));
            return tela;
        }

        private UserControl CriarGestaoAcessos()
        {
            var tela = new AcessosControl(usuarioAutenticado);
            tela.AcessosAlterados += (s, e) =>
            {
                AtualizarCabecalho();
            };
            return tela;
        }

        private void ConfigurarNotificacoes()
        {
            pictureNotificacoes.Cursor = Cursors.Hand;
            pictureNotificacoes.Click += pictureNotificacoes_Click;
            indicadorNotificacoes.Name = "indicadorNotificacoes";
            indicadorNotificacoes.Size = new Size(18, 18);
            indicadorNotificacoes.Location = new Point(
                pictureNotificacoes.Right - 7,
                pictureNotificacoes.Top - 6);
            indicadorNotificacoes.TextAlign = ContentAlignment.MiddleCenter;
            indicadorNotificacoes.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
            indicadorNotificacoes.ForeColor = Color.White;
            indicadorNotificacoes.BackColor = Color.FromArgb(220, 53, 69);
            indicadorNotificacoes.Visible = false;
            indicadorNotificacoes.Cursor = Cursors.Hand;
            indicadorNotificacoes.Click += pictureNotificacoes_Click;
            panelPerfil.Controls.Add(indicadorNotificacoes);
            indicadorNotificacoes.BringToFront();
            VisualUtils.Arredondar(indicadorNotificacoes, 9);
            AtualizarContagemNotificacoes();
        }

        private void AtualizarContagemNotificacoes()
        {
            if (modoDemonstracao)
            {
                AtualizarContagemDemonstracao();
                return;
            }
            try
            {
                List<CASE.Services.NotificacaoPendente> pendencias =
                    new CASE.Services.NotificacaoService().ListarPendencias(usuarioAutenticado);
                indicadorNotificacoes.Visible = pendencias.Count > 0;

                if (pendencias.Count > 9)
                {
                    indicadorNotificacoes.Text = "9+";
                }
                else
                {
                    indicadorNotificacoes.Text = pendencias.Count.ToString();
                }

                dicaNotificacoes.SetToolTip(
                    pictureNotificacoes,
                    pendencias.Count + " pendência(s) que exigem atenção.");
            }
            catch (Exception)
            {
                indicadorNotificacoes.Visible = false;
                dicaNotificacoes.SetToolTip(
                    pictureNotificacoes,
                    "Não foi possível consultar as pendências.");
            }
        }

        private void pictureNotificacoes_Click(object sender, EventArgs e)
        {
            if (modoDemonstracao)
            {
                AbrirNotificacoesDemonstracao();
                return;
            }
            ContextMenuStrip menu = new ContextMenuStrip();

            try
            {
                List<CASE.Services.NotificacaoPendente> pendencias =
                    new CASE.Services.NotificacaoService().ListarPendencias(usuarioAutenticado);

                if (pendencias.Count == 0)
                {
                    ToolStripMenuItem vazio = new ToolStripMenuItem("Nenhuma pendência no momento");
                    vazio.Enabled = false;
                    menu.Items.Add(vazio);
                }
                else
                {
                    foreach (CASE.Services.NotificacaoPendente pendencia in pendencias)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem(
                            pendencia.Titulo + " — " + pendencia.Descricao);
                        item.Tag = pendencia;
                        item.Click += NotificacaoSelecionada_Click;
                        menu.Items.Add(item);
                    }
                }

                // O Click do item ainda pode estar na fila quando o menu se fecha.
                menu.Closed += (menuSender, menuArgs) =>
                {
                    if (!IsDisposed && IsHandleCreated)
                    {
                        BeginInvoke(new Action(menu.Dispose));
                    }
                    else
                    {
                        menu.Dispose();
                    }
                };
                menu.Show(pictureNotificacoes, new Point(0, pictureNotificacoes.Height));
                AtualizarContagemNotificacoes();
            }
            catch (Exception)
            {
                menu.Dispose();
                MessageBox.Show(
                    "Não foi possível carregar as pendências.",
                    "Notificações",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void NotificacaoSelecionada_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            if (item == null)
            {
                return;
            }

            CASE.Services.NotificacaoPendente pendencia =
                item.Tag as CASE.Services.NotificacaoPendente;

            if (pendencia == null)
            {
                return;
            }

            if (pendencia.Tipo == "cadastro_usuario")
            {
                AbrirTela(buttonAcessos, CriarGestaoAcessos);
            }
            else if (pendencia.Tipo == "solicitacao_membro")
            {
                try
                {
                    eventoSelecionado = new CASE.Services.EventoService().BuscarPorId(
                        usuarioAutenticado,
                        pendencia.IdEvento);
                    AbrirTela(
                        buttonSolicitacoes,
                        () => new SolicitacoesControl(usuarioAutenticado, eventoSelecionado));
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "Não foi possível abrir a solicitação deste evento.",
                        "Notificações",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            else if (pendencia.Tipo == "solicitacao_central" ||
                pendencia.Tipo == "solicitacao_central_aprovada")
            {
                DecidirSolicitacaoCentral(pendencia.IdReferencia);
            }

            AtualizarContagemNotificacoes();
        }

        private void DecidirSolicitacaoCentral(int idSolicitacao)
        {
            CASE.Services.SolicitacaoEstoqueCentralService service =
                new CASE.Services.SolicitacaoEstoqueCentralService();

            try
            {
                CASE.Models.SolicitacaoEstoqueCentral solicitacao = service.BuscarPorId(
                    usuarioAutenticado,
                    idSolicitacao);
                List<CASE.Models.SolicitacaoEstoqueCentralItem> itens = service.ListarItens(
                    usuarioAutenticado,
                    idSolicitacao);
                System.Text.StringBuilder detalhes = new System.Text.StringBuilder();
                detalhes.AppendLine("Pedido " + solicitacao.Tipo + " do evento " + solicitacao.IdEvento);
                detalhes.AppendLine();

                foreach (CASE.Models.SolicitacaoEstoqueCentralItem solicitacaoItem in itens)
                {
                    CASE.Models.Produto produto = new CASE.Services.ProdutoService().BuscarPorId(
                        usuarioAutenticado,
                        solicitacaoItem.IdProduto);
                    string nomeProduto = "Produto " + solicitacaoItem.IdProduto;

                    if (produto != null)
                    {
                        nomeProduto = produto.Nome;
                    }

                    detalhes.AppendLine(nomeProduto + " — " + solicitacaoItem.Quantidade);
                }

                if (string.Equals(
                    solicitacao.Situacao,
                    CASE.Services.SituacoesSolicitacao.Aprovada,
                    StringComparison.OrdinalIgnoreCase))
                {
                    DialogResult atender = MessageBox.Show(
                        detalhes.ToString() +
                        "\nO pedido já foi aprovado. Deseja efetivar a transferência agora?",
                        "Atender pedido aprovado",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (atender == DialogResult.Yes)
                    {
                        service.AtenderComTransferencia(
                            usuarioAutenticado,
                            solicitacao.IdSolicitacaoEstoqueCentral);
                    }

                    AtualizarContagemNotificacoes();
                    return;
                }

                DialogResult decisao = MessageBox.Show(
                    detalhes.ToString() + "\nSim: aprovar integralmente. Não: rejeitar. Cancelar: voltar.",
                    "Solicitação ao estoque central",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (decisao == DialogResult.Yes)
                {
                    service.Aprovar(
                        usuarioAutenticado,
                        solicitacao.IdSolicitacaoEstoqueCentral,
                        solicitacao.Versao);

                    DialogResult liberarAgora = MessageBox.Show(
                        "Pedido aprovado sem movimentar saldo. Deseja efetivar a transferência agora?",
                        "Efetivar atendimento",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (liberarAgora == DialogResult.Yes)
                    {
                        service.AtenderComTransferencia(
                            usuarioAutenticado,
                            solicitacao.IdSolicitacaoEstoqueCentral);
                    }
                }
                else if (decisao == DialogResult.No)
                {
                    string justificativa = SolicitarMotivoRejeicao();

                    if (string.IsNullOrWhiteSpace(justificativa))
                    {
                        return;
                    }

                    service.Rejeitar(
                        usuarioAutenticado,
                        solicitacao.IdSolicitacaoEstoqueCentral,
                        solicitacao.Versao,
                        justificativa);
                }

                AtualizarContagemNotificacoes();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(
                    "Sua conta não pode decidir este pedido.",
                    "Solicitação ao estoque central",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Solicitação ao estoque central",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                AtualizarContagemNotificacoes();
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Não foi possível processar o pedido.",
                    "Solicitação ao estoque central",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string SolicitarMotivoRejeicao()
        {
            using (Form dialogo = new Form())
            using (TextBox campo = new TextBox())
            using (Button rejeitar = new Button())
            using (Button cancelar = new Button())
            {
                dialogo.Text = "Motivo da rejeição";
                dialogo.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialogo.StartPosition = FormStartPosition.CenterParent;
                dialogo.ClientSize = new Size(420, 150);
                dialogo.MinimizeBox = false;
                dialogo.MaximizeBox = false;
                campo.Multiline = true;
                campo.MaxLength = 500;
                campo.SetBounds(12, 12, 396, 72);
                rejeitar.Text = "Rejeitar";
                rejeitar.DialogResult = DialogResult.OK;
                rejeitar.SetBounds(222, 102, 88, 30);
                cancelar.Text = "Cancelar";
                cancelar.DialogResult = DialogResult.Cancel;
                cancelar.SetBounds(320, 102, 88, 30);
                dialogo.Controls.Add(campo);
                dialogo.Controls.Add(rejeitar);
                dialogo.Controls.Add(cancelar);
                dialogo.AcceptButton = rejeitar;
                dialogo.CancelButton = cancelar;

                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    return campo.Text.Trim();
                }
            }

            return string.Empty;
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
            VisualUtils.AplicarBorda(panelPesquisa);
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

        private void ConfigurarFotoPerfil()
        {
            ToolStripMenuItem itemAlterarFoto = new ToolStripMenuItem("Alterar foto");
            itemAlterarFoto.Click += AlterarFotoPerfil_Click;

            ToolStripMenuItem itemRemoverFoto = new ToolStripMenuItem("Remover foto");
            itemRemoverFoto.Click += RemoverFotoPerfil_Click;

            menuFotoPerfil.Items.Add(itemAlterarFoto);
            menuFotoPerfil.Items.Add(itemRemoverFoto);
            menuFotoPerfil.Opening += MenuFotoPerfil_Opening;

            pictureUsuario.ContextMenuStrip = menuFotoPerfil;
            pictureUsuario.Cursor = Cursors.Hand;
            pictureUsuario.Click += pictureUsuario_Click;
            FormClosed += PrincipalForm_FormClosed;

            try
            {
                byte[] foto = fotoPerfilService.BuscarFotoPerfil(usuarioAutenticado);

                if (foto != null && foto.Length > 0)
                {
                    AplicarFotoPerfil(foto);
                }
            }
            catch (Exception)
            {
                RestaurarFotoPadrao();
            }
        }

        private void pictureUsuario_Click(object sender, EventArgs e)
        {
            menuFotoPerfil.Show(
                pictureUsuario,
                new Point(0, pictureUsuario.Height));
        }

        private void MenuFotoPerfil_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (modoDemonstracao)
            {
                e.Cancel = true;
                return;
            }
            if (menuFotoPerfil.Items.Count > 1)
            {
                menuFotoPerfil.Items[1].Enabled = fotoPerfilPersonalizada != null;
            }
        }

        private void AlterarFotoPerfil_Click(object sender, EventArgs e)
        {
            if (modoDemonstracao)
            {
                MessageBox.Show(this, "Saia do modo demonstração para alterar sua foto real.", "Modo demonstração");
                return;
            }
            using (OpenFileDialog seletorArquivo = new OpenFileDialog())
            {
                seletorArquivo.Title = "Selecionar foto de perfil";
                seletorArquivo.Filter = "Imagens JPG ou PNG|*.jpg;*.jpeg;*.png";
                seletorArquivo.CheckFileExists = true;
                seletorArquivo.Multiselect = false;

                if (seletorArquivo.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    FileInfo arquivo = new FileInfo(seletorArquivo.FileName);

                    if (arquivo.Length > 2 * 1024 * 1024)
                    {
                        MessageBox.Show(
                            this,
                            "A imagem não pode ultrapassar 2 MB.",
                            "Foto de perfil",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    byte[] arquivoImagem = File.ReadAllBytes(seletorArquivo.FileName);
                    byte[] fotoSalva = fotoPerfilService.SalvarFotoPerfil(
                        usuarioAutenticado,
                        arquivoImagem);

                    AplicarFotoPerfil(fotoSalva);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show(
                        this,
                        "O arquivo não é uma imagem JPG ou PNG válida.",
                        "Foto de perfil",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                catch (InvalidOperationException erro)
                {
                    MessageBox.Show(
                        this,
                        erro.Message,
                        "Foto de perfil",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        this,
                        "Não foi possível salvar a foto de perfil.",
                        "Foto de perfil",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void RemoverFotoPerfil_Click(object sender, EventArgs e)
        {
            if (modoDemonstracao)
            {
                return;
            }
            try
            {
                fotoPerfilService.RemoverFotoPerfil(usuarioAutenticado);
                RestaurarFotoPadrao();
            }
            catch (InvalidOperationException erro)
            {
                MessageBox.Show(
                    this,
                    erro.Message,
                    "Foto de perfil",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show(
                    this,
                    "Não foi possível remover a foto de perfil.",
                    "Foto de perfil",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AplicarFotoPerfil(byte[] dadosFoto)
        {
            using (MemoryStream fluxoImagem = new MemoryStream(dadosFoto))
            using (Image imagemCarregada = Image.FromStream(fluxoImagem, true, true))
            {
                Image copiaImagem = new Bitmap(imagemCarregada);
                LiberarFotoPersonalizada();
                fotoPerfilPersonalizada = copiaImagem;
                pictureUsuario.Image = fotoPerfilPersonalizada;
            }
        }

        private void RestaurarFotoPadrao()
        {
            pictureUsuario.Image = Properties.Resources.LucideUsuario;
            LiberarFotoPersonalizada();
        }

        private void LiberarFotoPersonalizada()
        {
            if (fotoPerfilPersonalizada != null)
            {
                fotoPerfilPersonalizada.Dispose();
                fotoPerfilPersonalizada = null;
            }
        }

        private void PrincipalForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RestaurarFotoPadrao();
            menuFotoPerfil.Dispose();
        }

        private void AtualizarCabecalho()
        {
            labelNomeUsuario.Text = usuarioAutenticado.Login;
            labelTipoUsuario.Text = "Conta autenticada";
            buttonAcessos.Visible = false;

            try
            {
                string nomePerfil;
                new CASE.Services.AutorizacaoService().ValidarUsuarioEObterPerfil(
                    usuarioAutenticado,
                    out nomePerfil);
                buttonAcessos.Visible = string.Equals(
                    nomePerfil,
                    "Administrador",
                    StringComparison.OrdinalIgnoreCase);
                labelTipoUsuario.Text = nomePerfil;
                bool membro = string.Equals(nomePerfil, "Membro", StringComparison.OrdinalIgnoreCase);
                buttonProdutos.Visible = !membro;
                buttonFornecedores.Visible = !membro;
                buttonQuermesses.Visible = !membro;
                buttonEquipeSetores.Visible = !membro;
                buttonFinanceiro.Visible = !membro;
                buttonRelatorios.Visible = true;
            }
            catch (Exception)
            {
                buttonAcessos.Visible = false;
            }

            labelNomeUsuario.AutoSize = false;
            labelNomeUsuario.Size = new Size(110, 20);
            labelNomeUsuario.AutoEllipsis = true;
            dicaNomeUsuario.SetToolTip(labelNomeUsuario, usuarioAutenticado.Login);
            buttonMovimentacoes.Text = "Registros";
        }

        private UserControl CriarConfiguracoes()
        {
            var controle = new ConfiguracoesControl(usuarioAutenticado);
            controle.GerenciarAcessosSolicitado += (s, e) => AbrirTela(buttonAcessos, CriarGestaoAcessos);
            return controle;
        }

        private void SelecionarMenu(Button ativo)
        {
            foreach (Button botao in navegacao.Controls.OfType<Button>().Concat(new[] { buttonConfiguracoes }))
            {
                if (botao == ativo)
                {
                    botao.BackColor = Color.FromArgb(27, 75, 125);
                    botao.ForeColor = Color.White;
                }
                else
                {
                    botao.BackColor = panelMenu.BackColor;
                    botao.ForeColor = Color.FromArgb(213, 225, 239);
                }

                botao.FlatAppearance.MouseOverBackColor = Color.FromArgb(26, 61, 98);
            }
        }

        private void AbrirTela(Button menu, Func<UserControl> criar)
        {
            UserControl tela = null;
            try
            {
                // Construa antes de descartar a tela atual: uma falha não deve encerrar a navegação.
                if (modoDemonstracao)
                {
                    tela = TelasDemonstracao.Criar(menu.Name, dadosDemonstracao);
                    OperacoesEstoqueControl estoqueDemo = tela as OperacoesEstoqueControl;
                    if (estoqueDemo != null)
                    {
                        estoqueDemo.FornecedoresSolicitados += (s, e) =>
                            AbrirTela(buttonFornecedores, () => new FornecedoresControl(usuarioAutenticado));
                    }
                }
                else
                {
                    tela = criar();
                }
                tela.Dock = DockStyle.Fill;
                panelConteudo.SuspendLayout();
                while (panelConteudo.Controls.Count > 0)
                {
                    panelConteudo.Controls[0].Dispose();
                }
                panelConteudo.Controls.Add(tela);
                SelecionarMenu(menu);
                AplicarPesquisaAtual(tela);
            }
            catch (Exception erro)
            {
                if (tela != null && tela.Parent == null)
                {
                    tela.Dispose();
                }
                System.Diagnostics.Trace.WriteLine(erro.GetType().Name + " em AbrirTela: " + erro.StackTrace);
                MessageBox.Show(this, "Não foi possível abrir esta tela. Confira a conexão e suas permissões.",
                    "CASE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                panelConteudo.ResumeLayout(true);
            }
        }

        private void AplicarPesquisaAtual(UserControl tela)
        {
            if (tela == null || textPesquisa.Text == "Pesquisar...")
            {
                return;
            }

            string texto = textPesquisa.Text.Trim();

            if (tela is ProdutosControl)
            {
                ((ProdutosControl)tela).Pesquisar(texto);
            }
            else if (tela is FornecedoresControl)
            {
                ((FornecedoresControl)tela).Pesquisar(texto);
            }
            else if (tela is CASE.Quermesse.QuermessesControl)
            {
                ((CASE.Quermesse.QuermessesControl)tela).Pesquisar(texto);
            }
            else
            {
                VisualUtils.FiltrarGrades(tela, texto);
            }
        }

        private void textPesquisa_TextChanged(object sender, EventArgs e)
        {
            if (textPesquisa.Text == "Pesquisar..." || panelConteudo.Controls.Count == 0)
            {
                return;
            }

            AplicarPesquisaAtual(panelConteudo.Controls[0] as UserControl);
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
