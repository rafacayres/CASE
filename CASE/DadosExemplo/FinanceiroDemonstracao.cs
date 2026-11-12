using System;
using System.Windows.Forms;
using CASE.Models;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class FinanceiroControl
    {
        private void AbrirApuracaoDemo()
        {
            using (Form janela = new Form())
            {
                janela.Text = "Apuração · modo demonstração";
                janela.ClientSize = new System.Drawing.Size(1100, 750);
                janela.StartPosition = FormStartPosition.CenterParent;
                ApuracaoControl tela = new ApuracaoControl();
                tela.AtivarDemonstracao(dadosDemo);
                tela.Dock = DockStyle.Fill;
                janela.Controls.Add(tela);
                janela.ShowDialog(this);
            }
        }
    }

    public partial class ApuracaoControl
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            cmbSetorApuracao.DisplayMember = string.Empty;
            foreach (Setor setor in dados.Setores)
            {
                cmbSetorApuracao.Items.Add(setor.Nome);
            }

            cmbSetorApuracao.SelectedIndex = 0;
            btnConferirApuracao.Enabled = true;
            AtualizarApuracaoDemo();
        }

        private void AtualizarApuracaoDemo()
        {
            dgvApuracao.Rows.Clear();
            decimal total = 0;
            foreach (RegistroPreview registro in dadosDemo.Equipe.Registros)
            {
                if (registro.Tipo != "venda" || registro.Setor != cmbSetorApuracao.Text)
                {
                    continue;
                }

                decimal valor = registro.Quantidade * registro.PrecoUnitario;
                total += valor;
                dgvApuracao.Rows.Add(registro.Pessoa, registro.Setor, registro.Turno, registro.Quantidade, valor.ToString("C2"));
            }

            lblValorConferencia.Text = total.ToString("C2");
            labelAviso.Text = DadosDemonstracao.Aviso;
            lblSituacaoConferencia.Text = "Valores ilustrativos; a conferência não altera vendas ou estoques reais.";
        }
    }
}