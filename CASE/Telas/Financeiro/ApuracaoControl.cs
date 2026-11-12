using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class ApuracaoControl : UserControl
    {
        private readonly Usuario usuarioAutenticado;
        private readonly Evento eventoSelecionado;
        private readonly NumericUpDown dinheiro = new NumericUpDown();
        private readonly NumericUpDown pix = new NumericUpDown();
        private readonly NumericUpDown cartao = new NumericUpDown();
        private bool carregando = true;
        private decimal esperadoDinheiro;
        private decimal esperadoPix;
        private decimal esperadoCartao;

        public ApuracaoControl() : this(null, null)
        {
        }

        public ApuracaoControl(QuermessePreview exemplos) : this(null, null)
        {
        }

        public ApuracaoControl(Usuario usuario, Evento evento)
        {
            InitializeComponent();
            usuarioAutenticado = usuario;
            eventoSelecionado = evento;
            FrontendTheme.Aplicar(this);
            cmbSetorApuracao.Items.Clear();
            cmbTurnoApuracao.Items.Clear();
            cmbTurnoApuracao.Visible = false;
            lblTurno.Visible = false;
            lblSetor.Text = "Jornada / escala";
            cmbSetorApuracao.Width = 490;
            cmbSetorApuracao.DisplayMember = "descricao";
            dtpApuracao.Value = DateTime.Today;
            numValorApurado.ReadOnly = true;
            numValorApurado.Maximum = 99999999.99M;
            AdicionarModalidade(dinheiro, "Dinheiro", 18);
            AdicionarModalidade(pix, "Pix", 245);
            AdicionarModalidade(cartao, "Cartão", 472);
            btnConferirApuracao.Enabled = false;
            lblSituacaoConferencia.Text = "Selecione uma jornada autorizada.";
            labelAviso.Text = "Conferências não alteram vendas nem estoque.";
            if (usuario == null || evento == null)
            {
                carregando = false;
                return;
            }
            try
            {
                DataTable escalas = new ConsultaOperacionalService().Escalas(usuario, evento.IdEvento);
                foreach (DataRowView escala in escalas.DefaultView)
                {
                    cmbSetorApuracao.Items.Add(escala);
                }
                carregando = false;
                if (cmbSetorApuracao.Items.Count > 0)
                {
                    cmbSetorApuracao.SelectedIndex = 0;
                }
            }
            catch (Exception erro)
            {
                ExibirErro(erro);
            }
            finally
            {
                carregando = false;
            }
        }

        private void AdicionarModalidade(NumericUpDown campo, string titulo, int esquerda)
        {
            campo.DecimalPlaces = 2;
            campo.Maximum = 99999999.99M;
            campo.SetBounds(esquerda + 75, 105, 140, 28);
            panelConferencia.Controls.Add(FrontendTheme.Legenda(titulo,
                new Rectangle(esquerda, 108, 75, 24)));
            panelConferencia.Controls.Add(campo);
            campo.ValueChanged += AtualizarTotal;
        }

        private void AtualizarTotal(object sender, EventArgs e)
        {
            decimal total = dinheiro.Value + pix.Value + cartao.Value;
            numValorApurado.Value = Math.Min(total, numValorApurado.Maximum);
        }

        private void Filtros_Changed(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                AtualizarApuracaoDemo();
                return;
            }

            if (carregando || usuarioAutenticado == null || eventoSelecionado == null)
            {
                return;
            }
            try
            {
                DataRowView escala = cmbSetorApuracao.SelectedItem as DataRowView;
                if (escala == null)
                {
                    return;
                }
                int jornada = Convert.ToInt32(escala["id_jornada"]);
                carregando = true;
                dtpApuracao.Value = Convert.ToDateTime(escala["data_jornada"]);
                dtpApuracao.Enabled = false;
                carregando = false;
                new ApuracaoService().ListarPorJornada(usuarioAutenticado, jornada);
                DataTable vendas = new ConsultaOperacionalService().Registros(usuarioAutenticado,
                    eventoSelecionado.IdEvento, dtpApuracao.Value, dtpApuracao.Value);
                esperadoDinheiro = 0;
                esperadoPix = 0;
                esperadoCartao = 0;
                decimal semModalidade = 0;
                bool precosPendentes = false;
                dgvApuracao.Rows.Clear();
                foreach (DataRow venda in vendas.Rows)
                {
                    if (Convert.ToInt32(venda["id_jornada"]) != jornada ||
                        Convert.ToString(venda["tipo"]) != "venda")
                    {
                        continue;
                    }
                    if (venda.IsNull("total"))
                    {
                        precosPendentes = true;
                        dgvApuracao.Rows.Add(venda["responsavel"], venda["setor"], venda["turno"],
                            venda["quantidade"], "Preço não registrado");
                        continue;
                    }
                    decimal total = Convert.ToDecimal(venda["total"]);
                    string modalidade = Convert.ToString(venda["forma_pagamento"]);
                    if (modalidade == "dinheiro")
                    {
                        esperadoDinheiro += total;
                    }
                    else if (modalidade == "pix")
                    {
                        esperadoPix += total;
                    }
                    else if (modalidade == "cartao")
                    {
                        esperadoCartao += total;
                    }
                    else
                    {
                        semModalidade += total;
                    }
                    dgvApuracao.Rows.Add(venda["responsavel"], venda["setor"], venda["turno"],
                        venda["quantidade"], total.ToString("C2"));
                }
                lblValorConferencia.Text = (esperadoDinheiro + esperadoPix + esperadoCartao).ToString("C2");
                lblSituacaoConferencia.Text = "Esperado: Dinheiro " + esperadoDinheiro.ToString("C2") +
                    " / Pix " + esperadoPix.ToString("C2") + " / Cartão " + esperadoCartao.ToString("C2");
                labelAviso.Text = "Vendas sem modalidade: " + semModalidade.ToString("C2") +
                    ". Confira a jornada e a data antes de registrar.";
                btnConferirApuracao.Enabled = true;
                if (precosPendentes)
                {
                    btnConferirApuracao.Enabled = false;
                    labelAviso.Text = "Há vendas antigas sem preço praticado. A conferência depende da regularização auditável desses registros.";
                }
            }
            catch (Exception erro)
            {
                btnConferirApuracao.Enabled = false;
                ExibirErro(erro);
            }
        }

        private void btnConferirApuracao_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                labelAviso.Text = "Conferência simulada somente em memória. Nenhum fechamento foi salvo no MySQL.";
                return;
            }

            DataRowView escala = cmbSetorApuracao.SelectedItem as DataRowView;
            if (escala == null)
            {
                return;
            }
            btnConferirApuracao.Enabled = false;
            try
            {
                Apuracao apuracao = new Apuracao();
                apuracao.IdJornada = Convert.ToInt32(escala["id_jornada"]);
                apuracao.ValorDinheiro = dinheiro.Value;
                apuracao.ValorPix = pix.Value;
                apuracao.ValorCartao = cartao.Value;
                apuracao.Observacao = "Conferência por modalidade; data selecionada: " +
                    dtpApuracao.Value.ToString("yyyy-MM-dd");
                int id = new ApuracaoService().RegistrarConferencia(usuarioAutenticado, apuracao);
                labelAviso.Text = "Conferência " + id + " registrada. Diferenças: Dinheiro " +
                    (dinheiro.Value - esperadoDinheiro).ToString("C2") + "; Pix " +
                    (pix.Value - esperadoPix).ToString("C2") + "; Cartão " +
                    (cartao.Value - esperadoCartao).ToString("C2") + ". Estoque e vendas preservados.";
            }
            catch (Exception erro)
            {
                ExibirErro(erro);
                btnConferirApuracao.Enabled = true;
            }
        }

        private void ExibirErro(Exception erro)
        {
            if (erro is ArgumentException || erro is InvalidOperationException || erro is UnauthorizedAccessException)
            {
                labelAviso.Text = erro.Message;
            }
            else
            {
                labelAviso.Text = "Não foi possível carregar ou registrar a apuração.";
                System.Diagnostics.Trace.WriteLine(erro.GetType().Name + " em ApuracaoControl: " + erro.StackTrace);
            }
        }
    }
}
