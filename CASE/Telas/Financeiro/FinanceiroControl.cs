using System;
using System.Data;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class FinanceiroControl : UserControl
    {
        private readonly Usuario usuarioAutenticado;
        private readonly DespesaService despesas = new DespesaService();
        private bool carregando = true;

        public FinanceiroControl() : this(null, null)
        {
        }

        public FinanceiroControl(QuermessePreview exemplos) : this(null, null)
        {
        }

        public FinanceiroControl(Usuario usuario, Evento evento)
        {
            InitializeComponent();
            usuarioAutenticado = usuario;
            FrontendTheme.Aplicar(this);
            dtpFinanceiro.Value = DateTime.Today;
            cmbFiltroFinanceiro.Items.Clear();
            cmbSetorCusto.Items.Clear();
            cmbFiltroFinanceiro.DisplayMember = "Nome";
            cmbSetorCusto.DisplayMember = "Nome";
            lblSetor.Text = "Evento";
            lblSetor2.Text = "Evento";
            lblSubtitulo.Text = "Vendas e despesas registradas por evento e data";
            lblResultadoparcial.Text = "Vendas menos despesas";
            lblValorFinanceiroResumo0.Text = "—";
            lblValorFinanceiroResumo1.Text = "—";
            lblValorFinanceiroResumo2.Text = "—";
            lblDatadocustohoje.Text = "Data selecionada no filtro";
            labelAviso.Text = "Selecione um evento autorizado.";
            btnAdicionarCusto.Enabled = false;
            txtDescricaoCusto.MaxLength = 255;
            Button apurar = new Button();
            apurar.Text = "Apuração por modalidade";
            apurar.SetBounds(700, 32, 240, 36);
            VisualUtils.EstilizarBotao(apurar);
            panelFiltrosFinanceiro.Controls.Add(apurar);
            apurar.Click += AbrirApuracao;
            if (usuario == null)
            {
                carregando = false;
                return;
            }
            try
            {
                foreach (Evento permitido in new EventoService().Listar(usuario))
                {
                    cmbFiltroFinanceiro.Items.Add(permitido);
                    cmbSetorCusto.Items.Add(permitido);
                    if (evento != null && permitido.IdEvento == evento.IdEvento)
                    {
                        cmbFiltroFinanceiro.SelectedItem = permitido;
                        cmbSetorCusto.SelectedItem = permitido;
                    }
                }
                if (cmbFiltroFinanceiro.SelectedIndex < 0 && cmbFiltroFinanceiro.Items.Count > 0)
                {
                    cmbFiltroFinanceiro.SelectedIndex = 0;
                    cmbSetorCusto.SelectedIndex = 0;
                }
                carregando = false;
                AtualizarDados();
            }
            catch (Exception erro)
            {
                ExibirErro(erro);
            }
            finally
            {
                carregando = false;
            }
            AjustarIndicadores();
        }

        private void AtualizarDados()
        {
            if (dadosDemo != null)
            {
                AtualizarFinanceiroDemo();
                return;
            }

            Evento evento = cmbFiltroFinanceiro.SelectedItem as Evento;
            if (usuarioAutenticado == null || evento == null)
            {
                return;
            }
            decimal totalDespesas = 0;
            decimal totalVendas = 0;
            bool precosPendentes = false;
            dgvCustos.Rows.Clear();
            foreach (Despesa despesa in despesas.ListarPorEvento(usuarioAutenticado, evento.IdEvento))
            {
                if (despesa.Data.Date != dtpFinanceiro.Value.Date)
                {
                    continue;
                }
                totalDespesas += despesa.Valor;
                dgvCustos.Rows.Add(despesa.Data.ToString("dd/MM/yyyy"), despesa.Descricao,
                    evento.Nome, despesa.Valor.ToString("C2"), despesa.IdUsuarioResponsavel);
            }
            DataTable vendas = new ConsultaOperacionalService().Registros(usuarioAutenticado,
                evento.IdEvento, dtpFinanceiro.Value, dtpFinanceiro.Value);
            foreach (DataRow venda in vendas.Rows)
            {
                if (Convert.ToString(venda["tipo"]) == "venda")
                {
                    if (venda.IsNull("total"))
                    {
                        precosPendentes = true;
                        continue;
                    }
                    totalVendas += Convert.ToDecimal(venda["total"]);
                }
            }
            lblValorFinanceiroResumo0.Text = totalVendas.ToString("C2");
            lblValorFinanceiroResumo1.Text = totalDespesas.ToString("C2");
            lblValorFinanceiroResumo2.Text = (totalVendas - totalDespesas).ToString("C2");
            labelAviso.Text = "Valores da data selecionada. A diferença não representa lucro líquido.";
            if (precosPendentes)
            {
                lblValorFinanceiroResumo0.Text = "Indisponível";
                lblValorFinanceiroResumo2.Text = "Indisponível";
                labelAviso.Text = "Há vendas antigas sem preço praticado. Não é possível informar uma receita completa.";
            }
            btnAdicionarCusto.Enabled = true;
            VisualUtils.ExibirEstadoVazio(dgvCustos, "Nenhuma despesa registrada nesta data.");
        }

        private void Filtros_Changed(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                AtualizarFinanceiroDemo();
                return;
            }

            if (carregando)
            {
                return;
            }
            try
            {
                cmbSetorCusto.SelectedItem = cmbFiltroFinanceiro.SelectedItem;
                AtualizarDados();
            }
            catch (Exception erro)
            {
                btnAdicionarCusto.Enabled = false;
                ExibirErro(erro);
            }
        }

        private void btnAdicionarCusto_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                SalvarCustoDemo();
                return;
            }

            Evento evento = cmbSetorCusto.SelectedItem as Evento;
            if (evento == null)
            {
                labelAviso.Text = "Selecione o evento da despesa.";
                return;
            }
            btnAdicionarCusto.Enabled = false;
            try
            {
                Despesa despesa = new Despesa();
                despesa.IdEvento = evento.IdEvento;
                despesa.Descricao = txtDescricaoCusto.Text;
                despesa.Valor = numValorCusto.Value;
                despesa.Data = dtpFinanceiro.Value.Date;
                int id = despesas.Cadastrar(usuarioAutenticado, despesa);
                txtDescricaoCusto.Clear();
                AtualizarDados();
                labelAviso.Text = "Despesa " + id + " registrada.";
            }
            catch (Exception erro)
            {
                ExibirErro(erro);
            }
            finally
            {
                btnAdicionarCusto.Enabled = usuarioAutenticado != null;
            }
        }

        private void AbrirApuracao(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                AbrirApuracaoDemo();
                return;
            }

            Evento evento = cmbFiltroFinanceiro.SelectedItem as Evento;
            if (evento == null || usuarioAutenticado == null)
            {
                return;
            }
            using (Form janela = new Form())
            {
                janela.Text = "Apuração do evento";
                janela.ClientSize = new System.Drawing.Size(1100, 750);
                janela.StartPosition = FormStartPosition.CenterParent;
                ApuracaoControl controle = new ApuracaoControl(usuarioAutenticado, evento);
                controle.Dock = DockStyle.Fill;
                janela.Controls.Add(controle);
                janela.ShowDialog(this);
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
                labelAviso.Text = "Não foi possível consultar ou registrar os dados financeiros.";
                System.Diagnostics.Trace.WriteLine(erro.GetType().Name + " em FinanceiroControl: " + erro.StackTrace);
            }
        }

        private void panelCorpo_SizeChanged(object sender, EventArgs e)
        {
            AjustarIndicadores();
        }

        private void AjustarIndicadores()
        {
            Panel[] indicadores = { panelFinanceiroResumo0, panelFinanceiroResumo1, panelFinanceiroResumo2 };
            for (int i = 0; i < indicadores.Length; i++)
            {
                indicadores[i].SetBounds(i * (panelCorpo.Width + 15) / 3, 90, (panelCorpo.Width - 30) / 3, 100);
            }
        }
    }
}
