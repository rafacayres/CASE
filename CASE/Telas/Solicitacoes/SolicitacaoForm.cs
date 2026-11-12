using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class SolicitacaoForm : Form
    {
        private class EscalaOpcao
        {
            public Escala Escala { get; set; }
            public string Descricao { get; set; }
        }

        private readonly Usuario usuarioAutenticado;
        private readonly int? idSolicitacaoDetalhe;
        private readonly SolicitacaoService solicitacaoService = new SolicitacaoService();
        private readonly EscalaService escalaService = new EscalaService();
        private readonly ComboBox comboProdutos = new ComboBox();

        public SolicitacaoForm() : this(null, null)
        {
        }

        public SolicitacaoForm(bool detalhe) : this(null, null)
        {
            if (detalhe)
            {
                btnSalvarVisual.Enabled = false;
                labelAvisoFormulario.Text = "Entre no sistema para consultar os detalhes.";
            }
        }

        public SolicitacaoForm(Usuario usuario) : this(usuario, null)
        {
        }

        public SolicitacaoForm(Usuario usuario, int? idSolicitacao)
        {
            InitializeComponent();
            IconoAplicacao.Aplicar(this);
            FrontendTheme.Aplicar(this);
            usuarioAutenticado = usuario;
            idSolicitacaoDetalhe = idSolicitacao;

            ConfigurarComboProdutos();
            cmbTipoSolicitacao.Items.Clear();
            cmbTipoSolicitacao.Items.Add("reposicao");
            cmbTipoSolicitacao.SelectedIndex = 0;
            cmbStatusSolicitacao.Visible = false;
            lblSituacao.Visible = false;

            if (usuarioAutenticado == null)
            {
                btnSalvarVisual.Enabled = false;
                labelAvisoFormulario.Text = "Entre no sistema para registrar uma solicitação.";
                return;
            }

            if (idSolicitacaoDetalhe.HasValue)
            {
                CarregarDetalhes();
            }
            else
            {
                PrepararNovaSolicitacao();
            }
        }

        private void ConfigurarComboProdutos()
        {
            comboProdutos.DropDownStyle = ComboBoxStyle.DropDownList;
            comboProdutos.Font = txtProdutoSolicitacao.Font;
            comboProdutos.BackColor = txtProdutoSolicitacao.BackColor;
            comboProdutos.ForeColor = txtProdutoSolicitacao.ForeColor;
            comboProdutos.Bounds = txtProdutoSolicitacao.Bounds;
            comboProdutos.DisplayMember = "Nome";
            comboProdutos.Visible = false;
            panelFormulario.Controls.Add(comboProdutos);
            comboProdutos.BringToFront();
            txtProdutoSolicitacao.Visible = false;
            lblProduto.Text = "Produto disponível no setor";
            lblSetor.Text = "Minha escala e setor";
            cmbSetorSolicitacao.Items.Clear();
            cmbSetorSolicitacao.SelectedIndexChanged += cmbSetorSolicitacao_SelectedIndexChanged;
        }

        private void PrepararNovaSolicitacao()
        {
            txtSolicitante.ReadOnly = true;
            txtSolicitante.Text = "Conta autenticada #" + usuarioAutenticado.IdUsuario;
            cmbStatusSolicitacao.Visible = false;
            lblSituacao.Visible = false;
            txtObservacaoSolicitacao.Text = string.Empty;
            txtQuantidadeSolicitacao.Text = string.Empty;

            try
            {
                List<Escala> escalas = escalaService.ListarMinhas(usuarioAutenticado);

                foreach (Escala escala in escalas)
                {
                    JornadaSetor jornada = new JornadaSetorService().BuscarPorId(
                        usuarioAutenticado,
                        escala.IdJornada);

                    if (jornada == null)
                    {
                        continue;
                    }

                    Setor setor = new SetorService().BuscarPorId(
                        usuarioAutenticado,
                        jornada.IdSetor);
                    Turno turno = new CASE.Repositories.TurnoRepository().BuscarPorId(
                        jornada.IdTurno);

                    if (setor == null)
                    {
                        continue;
                    }

                    string nomeTurno = "Turno " + jornada.IdTurno;

                    if (turno != null)
                    {
                        nomeTurno = turno.Nome;
                    }

                    EscalaOpcao opcao = new EscalaOpcao();
                    opcao.Escala = escala;
                    opcao.Descricao = setor.Nome + " - " + jornada.Data.ToString("dd/MM/yyyy") +
                        " - " + nomeTurno;
                    cmbSetorSolicitacao.Items.Add(opcao);
                }

                cmbSetorSolicitacao.DisplayMember = "Descricao";

                if (cmbSetorSolicitacao.Items.Count == 0)
                {
                    btnSalvarVisual.Enabled = false;
                    labelAvisoFormulario.Text = "Você não possui escalas disponíveis para solicitar produtos.";
                    return;
                }

                cmbSetorSolicitacao.SelectedIndex = 0;
                CarregarProdutosDaEscalaSelecionada();
                Text = lblTitulo.Text = "Nova solicitação de reposição";
            }
            catch (UnauthorizedAccessException)
            {
                btnSalvarVisual.Enabled = false;
                labelAvisoFormulario.Text = "Somente membros vinculados podem solicitar reposição.";
            }
            catch (Exception)
            {
                btnSalvarVisual.Enabled = false;
                labelAvisoFormulario.Text = "Não foi possível carregar suas escalas.";
            }
        }

        private void cmbSetorSolicitacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                SelecionarProdutosSolicitacaoDemo();
                return;
            }

            CarregarProdutosDaEscalaSelecionada();
        }

        private void CarregarProdutosDaEscalaSelecionada()
        {
            comboProdutos.Items.Clear();

            EscalaOpcao opcao = cmbSetorSolicitacao.SelectedItem as EscalaOpcao;

            if (opcao == null)
            {
                return;
            }

            try
            {
                List<SolicitacaoService.ProdutoSolicitacaoOpcao> produtos =
                    solicitacaoService.ListarProdutosDaEscala(
                        usuarioAutenticado,
                        opcao.Escala.IdEscala);

                foreach (SolicitacaoService.ProdutoSolicitacaoOpcao produto in produtos)
                {
                    comboProdutos.Items.Add(produto);
                }

                if (comboProdutos.Items.Count > 0)
                {
                    comboProdutos.SelectedIndex = 0;
                    comboProdutos.Visible = true;
                    btnSalvarVisual.Enabled = true;
                    labelAvisoFormulario.Text = "Selecione o produto e informe a quantidade desejada.";
                }
                else
                {
                    comboProdutos.Visible = false;
                    btnSalvarVisual.Enabled = false;
                    labelAvisoFormulario.Text = "Este setor não possui produtos configurados para solicitação.";
                }
            }
            catch (Exception)
            {
                btnSalvarVisual.Enabled = false;
                labelAvisoFormulario.Text = "Não foi possível carregar os produtos do setor.";
            }
        }

        private void CarregarDetalhes()
        {
            try
            {
                Solicitacao solicitacao = solicitacaoService.BuscarPorId(
                    usuarioAutenticado,
                    idSolicitacaoDetalhe.Value);
                SolicitacaoService.ResumoSolicitacao resumo = solicitacaoService.ObterResumo(
                    usuarioAutenticado,
                    solicitacao.IdSolicitacao);

                Text = lblTitulo.Text = "Detalhes da solicitação";
                txtSolicitante.Text = resumo.Solicitante;
                txtSolicitante.ReadOnly = true;
                txtProdutoSolicitacao.Text = resumo.Produtos;
                txtProdutoSolicitacao.Visible = true;
                comboProdutos.Visible = false;
                txtProdutoSolicitacao.ReadOnly = true;
                txtQuantidadeSolicitacao.Text = string.Empty;
                txtQuantidadeSolicitacao.ReadOnly = true;
                txtObservacaoSolicitacao.Text = solicitacao.Descricao;
                txtObservacaoSolicitacao.ReadOnly = true;
                cmbSetorSolicitacao.Items.Clear();
                cmbSetorSolicitacao.Items.Add(resumo.Setor);
                cmbSetorSolicitacao.SelectedIndex = 0;
                cmbSetorSolicitacao.Enabled = false;
                cmbTipoSolicitacao.Items.Clear();
                cmbTipoSolicitacao.Items.Add(solicitacao.Tipo);
                cmbTipoSolicitacao.SelectedIndex = 0;
                cmbTipoSolicitacao.Enabled = false;
                cmbStatusSolicitacao.Items.Clear();
                cmbStatusSolicitacao.Items.Add(solicitacao.Situacao);
                cmbStatusSolicitacao.SelectedIndex = 0;
                cmbStatusSolicitacao.Visible = true;
                lblSituacao.Visible = true;
                btnSalvarVisual.Visible = false;
                labelAvisoFormulario.Text = "A decisão e o histórico são registrados pelo Service.";
            }
            catch (UnauthorizedAccessException)
            {
                btnSalvarVisual.Enabled = false;
                labelAvisoFormulario.Text = "Sua conta não pode consultar esta solicitação.";
            }
            catch (Exception)
            {
                btnSalvarVisual.Enabled = false;
                labelAvisoFormulario.Text = "Não foi possível carregar os detalhes da solicitação.";
            }
        }

        private void btnSalvarVisual_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                SalvarSolicitacaoDemo();
                return;
            }

            EscalaOpcao escalaSelecionada = cmbSetorSolicitacao.SelectedItem as EscalaOpcao;
            SolicitacaoService.ProdutoSolicitacaoOpcao produtoSelecionado =
                comboProdutos.SelectedItem as SolicitacaoService.ProdutoSolicitacaoOpcao;
            int quantidade;

            if (escalaSelecionada == null || produtoSelecionado == null)
            {
                labelAvisoFormulario.Text = "Selecione uma escala e um produto.";
                return;
            }

            if (!int.TryParse(txtQuantidadeSolicitacao.Text.Trim(), out quantidade) || quantidade <= 0)
            {
                labelAvisoFormulario.Text = "Informe uma quantidade inteira maior que zero.";
                txtQuantidadeSolicitacao.Focus();
                return;
            }

            Solicitacao solicitacao = new Solicitacao();
            solicitacao.IdEscala = escalaSelecionada.Escala.IdEscala;
            solicitacao.Tipo = "reposicao";
            solicitacao.Descricao = txtObservacaoSolicitacao.Text.Trim();

            List<SolicitacaoItem> itens = new List<SolicitacaoItem>();
            SolicitacaoItem item = new SolicitacaoItem();
            item.IdProdutoSetor = produtoSelecionado.IdProdutoSetor;
            item.Quantidade = quantidade;
            itens.Add(item);

            try
            {
                solicitacaoService.Criar(usuarioAutenticado, solicitacao, itens);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                labelAvisoFormulario.Text = ex.Message;
            }
            catch (UnauthorizedAccessException)
            {
                labelAvisoFormulario.Text = "Sua conta não pode solicitar produtos nesta escala.";
            }
            catch (Exception)
            {
                labelAvisoFormulario.Text = "Não foi possível registrar a solicitação.";
            }
        }
    }
}
