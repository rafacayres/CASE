using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{

    public partial class LoginForm : Form
    {

  
        public LoginForm()
        {
            InitializeComponent();

            VisualUtils.Arredondar(panelLogin, 15);
            // Navegação visual de demonstração, sem autenticação.
            btnEntrar.Click += (s, e) =>
            {
                Hide();
                using (var principal = new PrincipalForm()) principal.ShowDialog(this);
                Show();
            };
            AcceptButton = btnEntrar;
            VisualUtils.Arredondar(btnEntrar, 7);

            VisualUtils.Arredondar(panelUsuario, 7);
            VisualUtils.Arredondar(panelSenha, 7);

            panelUsuario.Paint += VisualUtils.DesenharBorda;
            panelSenha.Paint += VisualUtils.DesenharBorda;
        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CadastroForm cadastro = new CadastroForm();
            cadastro.ShowDialog();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
