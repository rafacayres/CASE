using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;
using MySql.Data.MySqlClient;

namespace CASE
{
    public partial class LoginForm : Form
    {
        private readonly AutenticacaoService autenticacaoService = new AutenticacaoService();

        public LoginForm()
        {
            InitializeComponent();
            IconoAplicacao.Aplicar(this);

            VisualUtils.Arredondar(panelLogin, 15);
            btnEntrar.Click += BtnEntrar_Click;
            AcceptButton = btnEntrar;
            VisualUtils.Arredondar(btnEntrar, 7);
            VisualUtils.Arredondar(panelUsuario, 7);
            VisualUtils.Arredondar(panelSenha, 7);
            VisualUtils.AplicarBorda(panelUsuario);
            VisualUtils.AplicarBorda(panelSenha);

            PrepararCampoLogin(textUsuario, panelUsuario, "Seu nome de usuário");
            PrepararCampoLogin(textSenha, panelSenha, "Sua senha");
            textSenha.UseSystemPasswordChar = true;
            textUsuario.TabIndex = 0;
            textSenha.TabIndex = 1;
            Shown += (s, e) => textUsuario.Focus();
        }

        private static void PrepararCampoLogin(TextBox campo, Panel painel, string placeholder)
        {
            painel.Padding = new Padding(12, 2, 12, 2);
            painel.Cursor = Cursors.IBeam;
            campo.Dock = DockStyle.Fill;
            campo.Margin = Padding.Empty;
            campo.Font = new Font("Segoe UI", 10F);
            VisualUtils.EstilizarCampo(campo);
            VisualUtils.DefinirPlaceholder(campo, placeholder);
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            DefinirMensagem(string.Empty);

            if (string.IsNullOrWhiteSpace(textUsuario.Text))
            {
                DefinirMensagem("Informe o login.");
                textUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(textSenha.Text))
            {
                DefinirMensagem("Informe a senha.");
                textSenha.Focus();
                return;
            }

            try
            {
                Usuario usuario = autenticacaoService.Autenticar(
                    textUsuario.Text,
                    textSenha.Text);

                if (usuario == null)
                {
                    DefinirMensagem("Não foi possível autenticar com os dados informados.");
                    textSenha.Clear();
                    return;
                }

                textSenha.Clear();
                Hide();

                try
                {
                    using (PrincipalForm formularioPrincipal = new PrincipalForm(usuario))
                    {
                        formularioPrincipal.ShowDialog(this);
                    }
                }
                finally
                {
                    autenticacaoService.EncerrarSessao();
                    Show();
                    textSenha.Clear();
                    textUsuario.Focus();
                }
            }
            catch (MySqlException erro)
            {
                RegistrarErroDeDesenvolvimento(erro);
                DefinirMensagem("Não foi possível acessar o banco de dados.");
                textSenha.Clear();
            }
            catch (Exception erro)
            {
                RegistrarErroDeDesenvolvimento(erro);
                DefinirMensagem("Não foi possível concluir o login.");
                textSenha.Clear();
            }
        }

        [Conditional("DEBUG")]
        private static void RegistrarErroDeDesenvolvimento(Exception erro)
        {
            if (erro == null)
            {
                return;
            }

            Debug.WriteLine("Falha no login. Tipo: " + erro.GetType().FullName);
            Debug.WriteLine(erro.StackTrace);

            if (erro.InnerException != null)
            {
                Debug.WriteLine("Falha interna. Tipo: " + erro.InnerException.GetType().FullName);
                Debug.WriteLine(erro.InnerException.StackTrace);
            }
        }

        private void DefinirMensagem(string mensagem)
        {
            lblMensagem.Text = mensagem;

            if (string.IsNullOrEmpty(mensagem))
            {
                lblMensagem.Visible = false;
            }
            else
            {
                lblMensagem.Visible = true;
                lblMensagem.BringToFront();
            }
        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (CadastroForm formularioCadastro = new CadastroForm())
            {
                formularioCadastro.ShowDialog(this);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }
    }
}
