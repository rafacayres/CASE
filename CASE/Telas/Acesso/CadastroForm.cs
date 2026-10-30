using System;
using System.Drawing;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{
    public partial class CadastroForm : Form
    {
        public CadastroForm()
        {
            InitializeComponent();

            VisualUtils.Arredondar(panelCadastro, 15);


            VisualUtils.Arredondar(btnCadastrar, 10);

            VisualUtils.Arredondar(panelNomeCompleto, 8);
            VisualUtils.Arredondar(panelUsuarioCadastro, 8);
            VisualUtils.Arredondar(panelEmail, 8);
            VisualUtils.Arredondar(panelSenhaCadastro, 8);
            VisualUtils.Arredondar(panelConfirmarSenha, 8);

            VisualUtils.AplicarBorda(panelNomeCompleto);
            VisualUtils.AplicarBorda(panelUsuarioCadastro);
            VisualUtils.AplicarBorda(panelEmail);
            VisualUtils.AplicarBorda(panelSenhaCadastro);
            VisualUtils.AplicarBorda(panelConfirmarSenha);
            lblSubtituloCadastro.Text = "Solicite seu acesso para análise da instituição";
            btnCadastrar.Text = "Solicitar cadastro";
            btnCadastrar.Click += (s, e) => SolicitarCadastro();
        }

        private void SolicitarCadastro()
        {
            if (string.IsNullOrWhiteSpace(txtNomeCompleto.Text) || string.IsNullOrWhiteSpace(txtUsuario.Text)
                || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            { MessageBox.Show(this, "Preencha nome, usuário, email e senha.", "Cadastro"); return; }
            if (txtSenha.Text != txtConfirmarSenha.Text)
            { MessageBox.Show(this, "A confirmação da senha não confere.", "Cadastro"); return; }
            string login = txtUsuario.Text.Trim();
            if (OperacoesPreview.Contas.Exists(c => string.Equals(c.Login, login, StringComparison.OrdinalIgnoreCase)))
            { MessageBox.Show(this, "Este usuário já existe na demonstração.", "Cadastro"); return; }
            OperacoesPreview.Contas.Add(new OperacoesPreview.Conta { Nome = txtNomeCompleto.Text.Trim(), Login = login, Email = txtEmail.Text.Trim() });
            txtSenha.Clear(); txtConfirmarSenha.Clear();
            MessageBox.Show(this, "Solicitação pendente na demonstração. Um administrador poderá analisá-la em Usuários e acessos. Os dados são temporários e nenhuma senha foi armazenada.", "Solicitação enviada");
            Close();
        }

        private void linkVoltarLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(MessageBox.Show("Deseja voltar para a tela de login?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
