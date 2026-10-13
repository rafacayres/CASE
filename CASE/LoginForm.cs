using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CASE
{

    public partial class LoginForm : Form
    {

        private void Arredondar(Control controle, int raio)
        {
            int diametro = raio * 2;

            using (GraphicsPath caminho = new GraphicsPath())
            {
                Rectangle area = new Rectangle(
                    0, 0,
                    controle.Width,
                    controle.Height
                );

                caminho.AddArc(0, 0, diametro, diametro, 180, 90);
                caminho.AddArc(area.Width - diametro, 0, diametro, diametro, 270, 90);
                caminho.AddArc(area.Width - diametro, area.Height - diametro, diametro, diametro, 0, 90);
                caminho.AddArc(0, area.Height - diametro, diametro, diametro, 90, 90);
                caminho.CloseFigure();

                Region regiaoAnterior = controle.Region;
                controle.Region = new Region(caminho);
                regiaoAnterior?.Dispose();

            }



        }

        private void DesenharBorda(object sender, PaintEventArgs e)
        {
            Panel painel = (Panel)sender;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int raio = 8;
            int diametro = raio * 2;

            Rectangle area = new Rectangle(
                0, 0,
                painel.Width - 1,
                painel.Height - 1
            );

            using (GraphicsPath caminho = new GraphicsPath())
            using (Pen caneta = new Pen(Color.FromArgb(205, 213, 223), 1))
            {
                caminho.AddArc(area.Left, area.Top, diametro, diametro, 180, 90);
                caminho.AddArc(area.Right - diametro, area.Top, diametro, diametro, 270, 90);
                caminho.AddArc(area.Right - diametro, area.Bottom - diametro, diametro, diametro, 0, 90);
                caminho.AddArc(area.Left, area.Bottom - diametro, diametro, diametro, 90, 90);

                caminho.CloseFigure();

                e.Graphics.DrawPath(caneta, caminho);
            }
        }
        public LoginForm()
        {
            InitializeComponent();

            Arredondar(panelLogin, 18);
            Arredondar(btnEntrar, 7);

            Arredondar(panelUsuario, 7);
            Arredondar(panelSenha, 7);

            panelUsuario.Paint += DesenharBorda;
            panelSenha.Paint += DesenharBorda;
        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CadastroForm cadastro = new CadastroForm();
            cadastro.ShowDialog();
        }

        private void lblUsuario_Click(object sender, EventArgs e)
        {

        }

        private void lblSubtitulo_Click(object sender, EventArgs e)
        {

        }
    }
}