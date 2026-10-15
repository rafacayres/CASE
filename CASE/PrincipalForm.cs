using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CASE.Utils;

namespace CASE
{
    public partial class PrincipalForm : Form
    {
        public PrincipalForm()
        {
            InitializeComponent();

            VisualUtils.Arredondar(buttonDashboard, 8);
            VisualUtils.Arredondar(buttonEstoque, 8);
            VisualUtils.Arredondar(buttonRelatorios, 8);
            VisualUtils.Arredondar(buttonProdutos, 8);
            VisualUtils.Arredondar(buttonMovimentacoes, 8);

            VisualUtils.Arredondar(buttonConfiguracoes, 8);
            VisualUtils.Arredondar(buttonSair, 8);

            VisualUtils.Arredondar(panelPesquisa, 12);
            VisualUtils.Arredondar(panelTotalProdutos, 12);
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
