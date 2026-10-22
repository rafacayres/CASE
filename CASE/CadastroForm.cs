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

            panelNomeCompleto.Paint += VisualUtils.DesenharBorda;
            panelUsuarioCadastro.Paint += VisualUtils.DesenharBorda;
            panelEmail.Paint += VisualUtils.DesenharBorda;
            panelSenhaCadastro.Paint += VisualUtils.DesenharBorda;
            panelConfirmarSenha.Paint += VisualUtils.DesenharBorda;
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