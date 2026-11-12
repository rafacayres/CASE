using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;
using MySql.Data.MySqlClient;

namespace CASE
{
    public partial class CadastroForm : Form
    {
        private readonly UsuarioService usuarioService = new UsuarioService();
        private readonly FotoPerfilService fotoPerfilService = new FotoPerfilService();

        private readonly PictureBox pictureFotoCadastro = new PictureBox();
        private readonly Button btnSelecionarFoto = new Button();
        private readonly CheckBox chkMostrarSenha = new CheckBox();
        private readonly Label lblFotoOpcional = new Label();
        private readonly Label lblMensagemCadastro = new Label();
        private readonly ContextMenuStrip menuFoto = new ContextMenuStrip();

        private byte[] fotoSelecionada;
        private Image imagemPrevia;

        public CadastroForm()
        {
            InitializeComponent();
            IconoAplicacao.Aplicar(this);
            PrepararLayout();
            PrepararCampos();
            PrepararFotoOpcional();
            PrepararFormularioResponsivo();

            btnCadastrar.Text = "Solicitar cadastro";
            btnCadastrar.Click += BtnCadastrar_Click;

            Shown += (s, e) => txtNomeCompleto.Focus();
            FormClosed += CadastroForm_FormClosed;
        }

        private void PrepararLayout()
        {
            BackgroundImage = Properties.Resources.FundoLogin;
            BackgroundImageLayout = ImageLayout.Stretch;
            BackColor = VisualUtils.Marinho;
            ClientSize = new Size(1280, 820);
            MinimumSize = new Size(1040, 780);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;

            // A arte de fundo já contém a marca e os elementos gráficos do CASE.
            panelEsquerdo.Visible = false;
            panelDireito.Visible = false;
            Controls.Add(panelCadastro);
            panelCadastro.Visible = true;
            panelCadastro.Size = new Size(520, 730);
            panelCadastro.BackColor = Color.White;
            VisualUtils.Arredondar(panelCadastro, 16);

            lblTituloCadastro.Text = "Criar sua conta";
            lblTituloCadastro.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            lblTituloCadastro.ForeColor = VisualUtils.Marinho;

            lblSubtituloCadastro.Text = "Solicite acesso ao CASE. O cadastro será analisado pela instituição.";
            lblSubtituloCadastro.Font = new Font("Segoe UI", 9.5F);
            lblSubtituloCadastro.ForeColor = VisualUtils.TextoSuave;

            lblNomeCompleto.Text = "Nome completo *";
            lblUsuario.Text = "Nome de usuário *";
            lblEmail.Text = "E-mail (opcional)";
            lblSenha.Text = "Senha *";
            lblConfirmarSenha.Text = "Confirmar senha *";

            Label[] rotulos =
            {
                lblNomeCompleto,
                lblUsuario,
                lblEmail,
                lblSenha,
                lblConfirmarSenha
            };

            foreach (Label rotulo in rotulos)
            {
                rotulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                rotulo.ForeColor = VisualUtils.Texto;
                rotulo.BackColor = Color.Transparent;
            }

            VisualUtils.EstilizarBotao(btnCadastrar);
            btnCadastrar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCadastrar.TabIndex = 10;

            linkVoltarLogin.Text = "Já tem uma conta? Voltar ao login";
            linkVoltarLogin.Font = new Font("Segoe UI", 9F);
            linkVoltarLogin.LinkColor = VisualUtils.Azul;
            linkVoltarLogin.TabIndex = 11;

            lblMensagemCadastro.Name = "lblMensagemCadastro";
            lblMensagemCadastro.Font = new Font("Segoe UI", 8.5F);
            lblMensagemCadastro.ForeColor = Color.FromArgb(190, 55, 65);
            lblMensagemCadastro.BackColor = Color.Transparent;
            lblMensagemCadastro.TextAlign = ContentAlignment.MiddleLeft;
            lblMensagemCadastro.Visible = false;
            panelCadastro.Controls.Add(lblMensagemCadastro);
            lblMensagemCadastro.BringToFront();
        }

        private void PrepararCampos()
        {
            PrepararCampo(txtNomeCompleto, panelNomeCompleto, "Ex.: Maria da Silva", 0);
            PrepararCampo(txtUsuario, panelUsuarioCadastro, "Escolha um nome de usuário", 1);
            PrepararCampo(txtEmail, panelEmail, "Ex.: maria@exemplo.com", 2);
            PrepararCampo(txtSenha, panelSenhaCadastro, "Digite sua senha", 3);
            PrepararCampo(txtConfirmarSenha, panelConfirmarSenha, "Digite a senha novamente", 4);

            txtSenha.UseSystemPasswordChar = true;
            txtConfirmarSenha.UseSystemPasswordChar = true;

            txtNomeCompleto.TextChanged += CampoCadastro_TextChanged;
            txtUsuario.TextChanged += CampoCadastro_TextChanged;
            txtEmail.TextChanged += CampoCadastro_TextChanged;
            txtSenha.TextChanged += CampoCadastro_TextChanged;
            txtConfirmarSenha.TextChanged += CampoCadastro_TextChanged;
        }

        private static void PrepararCampo(TextBox campo, Panel painel, string placeholder, int ordem)
        {
            painel.Height = 42;
            painel.BackColor = Color.FromArgb(248, 250, 252);
            painel.Padding = new Padding(12, 2, 12, 2);
            painel.TabIndex = ordem;
            painel.Cursor = Cursors.IBeam;

            campo.Dock = DockStyle.Fill;
            campo.Margin = Padding.Empty;
            campo.Font = new Font("Segoe UI", 10F);
            campo.TabIndex = 0;
            campo.BorderStyle = BorderStyle.None;

            VisualUtils.Arredondar(painel, 8);
            VisualUtils.AplicarBorda(painel);
            VisualUtils.EstilizarCampo(campo);
            VisualUtils.DefinirPlaceholder(campo, placeholder);
        }

        private void PrepararFotoOpcional()
        {
            pictureFotoCadastro.Name = "pictureFotoCadastro";
            pictureFotoCadastro.Size = new Size(52, 52);
            pictureFotoCadastro.SizeMode = PictureBoxSizeMode.Zoom;
            pictureFotoCadastro.BackColor = Color.FromArgb(239, 244, 251);
            pictureFotoCadastro.Image = Properties.Resources.LucideAzulUsuario;
            pictureFotoCadastro.TabStop = false;
            pictureFotoCadastro.Cursor = Cursors.Hand;
            VisualUtils.Arredondar(pictureFotoCadastro, 26);

            btnSelecionarFoto.Name = "btnSelecionarFoto";
            btnSelecionarFoto.Text = "Escolher foto";
            btnSelecionarFoto.Size = new Size(128, 34);
            btnSelecionarFoto.TabIndex = 5;
            btnSelecionarFoto.Click += SelecionarFoto_Click;
            VisualUtils.EstilizarBotao(btnSelecionarFoto);

            lblFotoOpcional.Text = "Foto opcional";
            lblFotoOpcional.AutoSize = false;
            lblFotoOpcional.Size = new Size(180, 26);
            lblFotoOpcional.Font = new Font("Segoe UI", 8.5F);
            lblFotoOpcional.ForeColor = VisualUtils.TextoSuave;
            lblFotoOpcional.TextAlign = ContentAlignment.MiddleLeft;

            panelCadastro.Controls.Add(pictureFotoCadastro);
            panelCadastro.Controls.Add(btnSelecionarFoto);
            panelCadastro.Controls.Add(lblFotoOpcional);

            chkMostrarSenha.Name = "chkMostrarSenha";
            chkMostrarSenha.Text = "Mostrar";
            chkMostrarSenha.Font = new Font("Segoe UI", 8F);
            chkMostrarSenha.ForeColor = VisualUtils.TextoSuave;
            chkMostrarSenha.BackColor = Color.Transparent;
            chkMostrarSenha.AutoSize = true;
            chkMostrarSenha.CheckedChanged += MostrarSenha_CheckedChanged;
            panelCadastro.Controls.Add(chkMostrarSenha);

            ToolStripMenuItem itemRemoverFoto = new ToolStripMenuItem("Remover foto selecionada");
            itemRemoverFoto.Click += RemoverFoto_Click;
            menuFoto.Items.Add(itemRemoverFoto);
            pictureFotoCadastro.ContextMenuStrip = menuFoto;
            pictureFotoCadastro.MouseUp += PictureFotoCadastro_MouseUp;
        }

        private void PrepararFormularioResponsivo()
        {
            Resize += CadastroForm_Resize;
            CadastroForm_Resize(this, EventArgs.Empty);
        }

        private void CadastroForm_Resize(object sender, EventArgs e)
        {
            if (panelCadastro == null || panelCadastro.IsDisposed)
            {
                return;
            }

            int larguraCartao = Math.Min(520, ClientSize.Width - 48);
            int alturaCartao = Math.Min(730, ClientSize.Height - 40);

            if (larguraCartao < 440 || alturaCartao < 680)
            {
                return;
            }

            panelCadastro.Size = new Size(larguraCartao, alturaCartao);

            int inicioX = 32;
            int larguraConteudo = larguraCartao - (inicioX * 2);
            int xCartao = (int)(ClientSize.Width * 0.70) - (larguraCartao / 2);
            int yCartao = (ClientSize.Height - alturaCartao) / 2;
            panelCadastro.Location = new Point(Math.Max(24, xCartao), Math.Max(20, yCartao));

            lblTituloCadastro.SetBounds(inicioX, 24, larguraConteudo, 40);
            lblSubtituloCadastro.SetBounds(inicioX, 67, larguraConteudo, 36);
            pictureFotoCadastro.SetBounds(inicioX, 111, 52, 52);
            btnSelecionarFoto.SetBounds(inicioX + 66, 120, 128, 34);
            lblFotoOpcional.SetBounds(inicioX + 204, 124, larguraConteudo - 204, 26);

            int y = 178;
            int alturaRotulo = 19;
            int alturaCampo = 42;
            int distanciaEntreCampos = 75;

            PosicionarCampo(lblNomeCompleto, panelNomeCompleto, inicioX, y, larguraConteudo, alturaRotulo, alturaCampo);
            y += distanciaEntreCampos;
            PosicionarCampo(lblUsuario, panelUsuarioCadastro, inicioX, y, larguraConteudo, alturaRotulo, alturaCampo);
            y += distanciaEntreCampos;
            PosicionarCampo(lblEmail, panelEmail, inicioX, y, larguraConteudo, alturaRotulo, alturaCampo);
            y += distanciaEntreCampos;
            PosicionarCampo(lblSenha, panelSenhaCadastro, inicioX, y, larguraConteudo, alturaRotulo, alturaCampo);
            lblSenha.Width = larguraConteudo - 100;
            chkMostrarSenha.SetBounds(
                inicioX + larguraConteudo - 90,
                y - 1,
                90,
                alturaRotulo + 2);
            y += distanciaEntreCampos;
            PosicionarCampo(lblConfirmarSenha, panelConfirmarSenha, inicioX, y, larguraConteudo, alturaRotulo, alturaCampo);

            int topoAcoes = panelCadastro.Height - 112;
            lblMensagemCadastro.SetBounds(inicioX, topoAcoes - 44, larguraConteudo, 38);
            btnCadastrar.SetBounds(inicioX, topoAcoes, larguraConteudo, 48);
            linkVoltarLogin.SetBounds(inicioX, topoAcoes + 59, larguraConteudo, 24);
        }

        private static void PosicionarCampo(
            Label rotulo,
            Panel painel,
            int x,
            int y,
            int largura,
            int alturaRotulo,
            int alturaCampo)
        {
            rotulo.SetBounds(x, y, largura, alturaRotulo);
            painel.SetBounds(x, y + alturaRotulo + 4, largura, alturaCampo);
        }

        private void SelecionarFoto_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog seletor = new OpenFileDialog())
            {
                seletor.Title = "Selecionar foto de perfil";
                seletor.Filter = "Imagens JPG ou PNG|*.jpg;*.jpeg;*.png";
                seletor.CheckFileExists = true;
                seletor.Multiselect = false;

                if (seletor.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    byte[] dadosArquivo = File.ReadAllBytes(seletor.FileName);
                    fotoSelecionada = fotoPerfilService.PrepararFoto(dadosArquivo);
                    ExibirPreviaFoto(fotoSelecionada);
                    ExibirMensagem(string.Empty, false);
                }
                catch (ArgumentException erro)
                {
                    ExibirMensagem(erro.Message, true);
                }
                catch (Exception)
                {
                    ExibirMensagem("Não foi possível abrir essa imagem. Selecione um arquivo JPG ou PNG válido.", true);
                }
            }
        }

        private void ExibirPreviaFoto(byte[] dadosFoto)
        {
            Image imagemNova;

            using (MemoryStream fluxo = new MemoryStream(dadosFoto))
            using (Image imagemTemporaria = Image.FromStream(fluxo))
            {
                imagemNova = new Bitmap(imagemTemporaria);
            }

            Image imagemAnterior = imagemPrevia;
            imagemPrevia = imagemNova;
            pictureFotoCadastro.Image = imagemPrevia;

            if (imagemAnterior != null)
            {
                imagemAnterior.Dispose();
            }

            lblFotoOpcional.Text = "Foto selecionada";
        }

        private void PictureFotoCadastro_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                menuFoto.Show(pictureFotoCadastro, e.Location);
            }
        }

        private void RemoverFoto_Click(object sender, EventArgs e)
        {
            fotoSelecionada = null;
            Image imagemAnterior = imagemPrevia;
            imagemPrevia = null;
            pictureFotoCadastro.Image = Properties.Resources.LucideAzulUsuario;
            lblFotoOpcional.Text = "Foto opcional";

            if (imagemAnterior != null)
            {
                imagemAnterior.Dispose();
            }
        }

        private void MostrarSenha_CheckedChanged(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = !chkMostrarSenha.Checked;
            txtConfirmarSenha.UseSystemPasswordChar = !chkMostrarSenha.Checked;
        }

        private void CampoCadastro_TextChanged(object sender, EventArgs e)
        {
            TextBox campo = sender as TextBox;
            VisualUtils.DefinirEstadoErro(campo, false);

            if (string.IsNullOrEmpty(lblMensagemCadastro.Text))
            {
                lblMensagemCadastro.Visible = false;
            }
        }

        private void BtnCadastrar_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                CadastrarDemo();
                return;
            }

            ExibirMensagem(string.Empty, false);

            if (string.IsNullOrWhiteSpace(txtNomeCompleto.Text))
            {
                InformarCampoObrigatorio(txtNomeCompleto, "Informe seu nome completo.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                InformarCampoObrigatorio(txtUsuario, "Informe o nome de usuário.");
                return;
            }

            if (string.IsNullOrEmpty(txtSenha.Text))
            {
                InformarCampoObrigatorio(txtSenha, "Informe sua senha.");
                return;
            }

            if (string.IsNullOrEmpty(txtConfirmarSenha.Text))
            {
                InformarCampoObrigatorio(txtConfirmarSenha, "Confirme sua senha.");
                return;
            }

            if (!string.Equals(txtSenha.Text, txtConfirmarSenha.Text, StringComparison.Ordinal))
            {
                VisualUtils.DefinirEstadoErro(txtConfirmarSenha, true);
                ExibirMensagem("A confirmação da senha não confere.", true);
                txtConfirmarSenha.Focus();
                return;
            }

            Pessoa pessoa = new Pessoa();
            pessoa.Nome = txtNomeCompleto.Text.Trim();
            pessoa.Email = txtEmail.Text.Trim();
            pessoa.FotoPerfil = fotoSelecionada;

            try
            {
                usuarioService.SolicitarCadastro(
                    pessoa,
                    txtUsuario.Text,
                    txtSenha.Text);

                txtSenha.Clear();
                txtConfirmarSenha.Clear();

                MessageBox.Show(
                    this,
                    "Sua solicitação foi registrada e depende de aprovação para liberar o acesso.",
                    "Solicitação enviada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Close();
            }
            catch (ArgumentException erro)
            {
                ExibirMensagem(erro.Message, true);
                txtUsuario.Focus();
            }
            catch (InvalidOperationException erro)
            {
                ExibirMensagem(erro.Message, true);
                txtSenha.Clear();
                txtConfirmarSenha.Clear();
            }
            catch (MySqlException)
            {
                ExibirMensagem("Não foi possível registrar o cadastro no banco de dados. Tente novamente.", true);
                txtSenha.Clear();
                txtConfirmarSenha.Clear();
            }
            catch (NotSupportedException erro)
            {
                ExibirMensagem(erro.Message, true);
                txtSenha.Clear();
                txtConfirmarSenha.Clear();
            }
            catch (Exception)
            {
                ExibirMensagem("Não foi possível concluir o cadastro. Tente novamente.", true);
                txtSenha.Clear();
                txtConfirmarSenha.Clear();
            }
        }

        private void InformarCampoObrigatorio(TextBox campo, string mensagem)
        {
            VisualUtils.DefinirEstadoErro(campo, true);
            ExibirMensagem(mensagem, true);
            campo.Focus();
        }

        private void ExibirMensagem(string mensagem, bool erro)
        {
            lblMensagemCadastro.Text = mensagem;
            lblMensagemCadastro.ForeColor = erro
                ? Color.FromArgb(190, 55, 65)
                : Color.FromArgb(43, 133, 104);
            lblMensagemCadastro.Visible = !string.IsNullOrWhiteSpace(mensagem);
            lblMensagemCadastro.BringToFront();
        }

        private void linkVoltarLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                this,
                "Deseja voltar para a tela de login?",
                "Confirmação",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Close();
            }
        }

        private void CadastroForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (imagemPrevia != null)
            {
                imagemPrevia.Dispose();
                imagemPrevia = null;
            }

            menuFoto.Dispose();
        }
    }
}
