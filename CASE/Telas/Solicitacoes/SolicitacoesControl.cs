using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class SolicitacoesControl : UserControl
    {
        private readonly Usuario usuarioAutenticado;
        private readonly Evento eventoSelecionado;
        private readonly SolicitacaoService solicitacaoService = new SolicitacaoService();
        private bool gerenteAutorizado;
        private bool exibindoPedidosCentrais;

        public SolicitacoesControl() : this(null, null)
        {
        }

        public SolicitacoesControl(QuermessePreview exemplos) : this(null, null)
        {
        }

        public SolicitacoesControl(Usuario usuario, Evento evento)
        {
            InitializeComponent();
            FrontendTheme.Aplicar(this);
            usuarioAutenticado = usuario;
            eventoSelecionado = evento;
            btnDetalhesSolicitacao.Text = "Detalhes / analisar";
            dgvSolicitacoesQuermesse.CellDoubleClick += dgvSolicitacoesQuermesse_CellDoubleClick;

            if (usuarioAutenticado == null)
            {
                btnNovaSolicitacao.Enabled = false;
                btnDetalhesSolicitacao.Enabled = false;
                labelAviso.Text = "Entre no sistema para consultar solicitações.";
                return;
            }

            try
            {
                string nomePerfil;
                new AutorizacaoService().ValidarUsuarioEObterPerfil(
                    usuarioAutenticado,
                    out nomePerfil);
                gerenteAutorizado = string.Equals(
                    nomePerfil,
                    "Gerente",
                    StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception)
            {
                gerenteAutorizado = false;
            }

            CarregarSolicitacoes();
        }

        private void btnNovaSolicitacao_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                AbrirPedidoDemo(0);
                return;
            }

            if (gerenteAutorizado)
            {
                if (eventoSelecionado == null)
                {
                    labelAviso.Text = "Selecione o evento antes de solicitar produtos ao central.";
                    return;
                }

                using (SolicitacaoEstoqueCentralForm formulario =
                    new SolicitacaoEstoqueCentralForm(usuarioAutenticado, eventoSelecionado))
                {
                    if (formulario.ShowDialog(this) == DialogResult.OK)
                    {
                        exibindoPedidosCentrais = true;
                        CarregarSolicitacoes();
                    }
                }

                return;
            }

            using (SolicitacaoForm formulario = new SolicitacaoForm(usuarioAutenticado))
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    CarregarSolicitacoes();
                }
            }
        }

        private void btnDetalhesSolicitacao_Click(object sender, EventArgs e)
        {
            if (gerenteAutorizado)
            {
                exibindoPedidosCentrais = !exibindoPedidosCentrais;
                CarregarSolicitacoes();
                return;
            }

            AbrirSolicitacaoSelecionada();
        }

        private void dgvSolicitacoesQuermesse_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AbrirSolicitacaoSelecionada();
            }
        }

        private void AbrirSolicitacaoSelecionada()
        {
            if (dadosDemo != null)
            {
                AbrirSolicitacaoDemoSelecionada();
                return;
            }

            if (dgvSolicitacoesQuermesse.SelectedRows.Count == 0)
            {
                labelAviso.Text = "Selecione uma solicitação.";
                return;
            }

            if (exibindoPedidosCentrais)
            {
                SolicitacaoEstoqueCentral pedidoCentral =
                    dgvSolicitacoesQuermesse.SelectedRows[0].Tag as SolicitacaoEstoqueCentral;

                if (pedidoCentral != null)
                {
                    MostrarDetalhesCentral(pedidoCentral.IdSolicitacaoEstoqueCentral);
                }

                return;
            }

            Solicitacao solicitacao = dgvSolicitacoesQuermesse.SelectedRows[0].Tag as Solicitacao;

            if (solicitacao == null)
            {
                return;
            }

            if (string.Equals(
                solicitacao.Situacao,
                SituacoesSolicitacao.Pendente,
                StringComparison.OrdinalIgnoreCase))
            {
                if (gerenteAutorizado)
                {
                    DecidirSolicitacao(solicitacao);
                }
                else
                {
                    MostrarDetalhes(solicitacao.IdSolicitacao);
                }

                return;
            }

            if (string.Equals(
                solicitacao.Situacao,
                SituacoesSolicitacao.Aprovada,
                StringComparison.OrdinalIgnoreCase))
            {
                if (!gerenteAutorizado)
                {
                    MostrarDetalhes(solicitacao.IdSolicitacao);
                    return;
                }

                DialogResult resposta = MessageBox.Show(
                    "A solicitação foi aprovada. Deseja transferir todos os itens do estoque geral para a barraca?",
                    "Atender solicitação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resposta == DialogResult.Yes)
                {
                    AtenderSolicitacao(solicitacao);
                }

                return;
            }

            MostrarDetalhes(solicitacao.IdSolicitacao);
        }

        private void DecidirSolicitacao(Solicitacao solicitacao)
        {
            DialogResult resposta = MessageBox.Show(
                "Sim: aprovar o pedido completo. Não: rejeitar com justificativa. Cancelar: voltar.",
                "Decidir solicitação",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            try
            {
                if (resposta == DialogResult.Yes)
                {
                    solicitacaoService.Aprovar(
                        usuarioAutenticado,
                        solicitacao.IdSolicitacao,
                        null);
                    labelAviso.Text = "Solicitação aprovada. O estoque não foi movimentado.";
                    CarregarSolicitacoes();
                }
                else if (resposta == DialogResult.No)
                {
                    string justificativa = SolicitarJustificativa();

                    if (string.IsNullOrWhiteSpace(justificativa))
                    {
                        return;
                    }

                    solicitacaoService.Rejeitar(
                        usuarioAutenticado,
                        solicitacao.IdSolicitacao,
                        justificativa);
                    labelAviso.Text = "Solicitação rejeitada.";
                    CarregarSolicitacoes();
                }
            }
            catch (UnauthorizedAccessException)
            {
                labelAviso.Text = "Sua conta não pode decidir esta solicitação.";
            }
            catch (InvalidOperationException ex)
            {
                labelAviso.Text = ex.Message;
                CarregarSolicitacoes();
            }
            catch (Exception)
            {
                labelAviso.Text = "Não foi possível registrar a decisão.";
            }
        }

        private string SolicitarJustificativa()
        {
            using (Form dialogo = new Form())
            using (TextBox campo = new TextBox())
            using (Button confirmar = new Button())
            using (Button cancelar = new Button())
            {
                dialogo.Text = "Motivo da rejeição";
                dialogo.FormBorderStyle = FormBorderStyle.FixedDialog;
                dialogo.StartPosition = FormStartPosition.CenterParent;
                dialogo.ClientSize = new Size(420, 150);
                dialogo.MinimizeBox = false;
                dialogo.MaximizeBox = false;

                campo.Multiline = true;
                campo.MaxLength = 500;
                campo.SetBounds(12, 12, 396, 72);

                confirmar.Text = "Rejeitar";
                confirmar.DialogResult = DialogResult.OK;
                confirmar.SetBounds(222, 102, 88, 30);

                cancelar.Text = "Cancelar";
                cancelar.DialogResult = DialogResult.Cancel;
                cancelar.SetBounds(320, 102, 88, 30);

                dialogo.Controls.Add(campo);
                dialogo.Controls.Add(confirmar);
                dialogo.Controls.Add(cancelar);
                dialogo.AcceptButton = confirmar;
                dialogo.CancelButton = cancelar;

                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    return campo.Text.Trim();
                }
            }

            return string.Empty;
        }

        private void AtenderSolicitacao(Solicitacao solicitacao)
        {
            try
            {
                new TransferenciaEstoqueService().AtenderSolicitacaoMembro(
                    usuarioAutenticado,
                    solicitacao.IdSolicitacao);
                labelAviso.Text = "Solicitação atendida integralmente por transferência.";
                CarregarSolicitacoes();
            }
            catch (UnauthorizedAccessException)
            {
                labelAviso.Text = "Sua conta não pode atender esta solicitação.";
            }
            catch (InvalidOperationException ex)
            {
                labelAviso.Text = ex.Message;
            }
            catch (Exception)
            {
                labelAviso.Text = "Não foi possível atender a solicitação.";
            }
        }

        private void MostrarDetalhes(int idSolicitacao)
        {
            using (SolicitacaoForm formulario = new SolicitacaoForm(
                usuarioAutenticado,
                idSolicitacao))
            {
                formulario.ShowDialog(this);
            }
        }

        private void Filtros_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void CarregarSolicitacoes()
        {
            dgvSolicitacoesQuermesse.Rows.Clear();
            btnNovaSolicitacao.Enabled = false;

            if (gerenteAutorizado && exibindoPedidosCentrais)
            {
                CarregarPedidosCentrais();
                return;
            }

            try
            {
                List<Solicitacao> solicitacoes = ObterSolicitacoesPermitidas();

                foreach (Solicitacao solicitacao in solicitacoes)
                {
                    SolicitacaoService.ResumoSolicitacao resumo = solicitacaoService.ObterResumo(
                        usuarioAutenticado,
                        solicitacao.IdSolicitacao);
                    int indice = dgvSolicitacoesQuermesse.Rows.Add(
                        solicitacao.DataHora.ToString("dd/MM/yyyy HH:mm"),
                        resumo.Solicitante,
                        resumo.Setor,
                        solicitacao.Tipo,
                        resumo.Produtos,
                        string.Empty,
                        solicitacao.Situacao);
                    dgvSolicitacoesQuermesse.Rows[indice].Tag = solicitacao;
                }

                bool membroComEscala = PodeSolicitarComoMembro();
                btnNovaSolicitacao.Enabled = gerenteAutorizado || membroComEscala;

                if (gerenteAutorizado)
                {
                    btnNovaSolicitacao.Text = "+ Solicitar ao estoque central";
                    btnDetalhesSolicitacao.Text = "Ver meus pedidos ao central";
                }
                else
                {
                    btnNovaSolicitacao.Text = "+ Nova solicitação";
                }
                ConfigurarFiltros();
                AplicarFiltros();
                labelAviso.Text = solicitacoes.Count + " solicitação(ões) carregada(s).";
            }
            catch (UnauthorizedAccessException)
            {
                labelAviso.Text = "Sua conta não possui acesso a estas solicitações.";
            }
            catch (Exception)
            {
                labelAviso.Text = "Não foi possível carregar as solicitações.";
            }
        }

        private void CarregarPedidosCentrais()
        {
            try
            {
                List<SolicitacaoEstoqueCentral> pedidos =
                    new SolicitacaoEstoqueCentralService().ListarDoGerente(
                        usuarioAutenticado);

                foreach (SolicitacaoEstoqueCentral pedido in pedidos)
                {
                    string resumo = MontarResumoItensCentrais(pedido);
                    int indice = dgvSolicitacoesQuermesse.Rows.Add(
                        pedido.DataHora.ToString("dd/MM/yyyy HH:mm"),
                        "Você",
                        "Evento " + pedido.IdEvento,
                        pedido.Tipo,
                        resumo,
                        string.Empty,
                        pedido.Situacao);
                    dgvSolicitacoesQuermesse.Rows[indice].Tag = pedido;
                }

                exibindoPedidosCentrais = true;
                btnNovaSolicitacao.Enabled = true;
                btnNovaSolicitacao.Text = "+ Novo pedido ao central";
                btnDetalhesSolicitacao.Text = "Voltar às reposições";
                ConfigurarFiltros();
                AplicarFiltros();
                labelAviso.Text = pedidos.Count + " pedido(s) ao estoque central.";
            }
            catch (UnauthorizedAccessException)
            {
                labelAviso.Text = "Somente o gerente responsável pode consultar seus pedidos ao central.";
            }
            catch (Exception)
            {
                labelAviso.Text = "Não foi possível carregar os pedidos ao estoque central.";
            }
        }

        private string MontarResumoItensCentrais(SolicitacaoEstoqueCentral pedido)
        {
            SolicitacaoEstoqueCentralService service = new SolicitacaoEstoqueCentralService();
            List<SolicitacaoEstoqueCentralItem> itens = service.ListarItens(
                usuarioAutenticado,
                pedido.IdSolicitacaoEstoqueCentral);
            List<string> resumoItens = new List<string>();

            foreach (SolicitacaoEstoqueCentralItem item in itens)
            {
                Produto produto = new ProdutoService().BuscarPorId(
                    usuarioAutenticado,
                    item.IdProduto);
                string nomeProduto = "Produto " + item.IdProduto;

                if (produto != null)
                {
                    nomeProduto = produto.Nome;
                }

                resumoItens.Add(nomeProduto + " (" + item.Quantidade + ")");
            }

            return string.Join(", ", resumoItens);
        }

        private void MostrarDetalhesCentral(int idSolicitacao)
        {
            try
            {
                SolicitacaoEstoqueCentralService service =
                    new SolicitacaoEstoqueCentralService();
                SolicitacaoEstoqueCentral pedido = service.BuscarPorId(
                    usuarioAutenticado,
                    idSolicitacao);
                string detalhes = "Pedido " + pedido.Tipo + " · " + pedido.Situacao +
                    " · evento " + pedido.IdEvento + Environment.NewLine +
                    MontarResumoItensCentrais(pedido);

                if (!string.IsNullOrWhiteSpace(pedido.JustificativaRejeicao))
                {
                    detalhes += Environment.NewLine + "Motivo: " +
                        pedido.JustificativaRejeicao;
                }

                int? idTransferencia = service.BuscarIdTransferenciaAtendimento(
                    usuarioAutenticado,
                    idSolicitacao);

                if (idTransferencia.HasValue)
                {
                    detalhes += Environment.NewLine + "Transferência: " +
                        idTransferencia.Value;
                }

                MessageBox.Show(
                    detalhes,
                    "Detalhes do pedido ao central",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                labelAviso.Text = "Não foi possível consultar os detalhes do pedido.";
            }
        }

        private List<Solicitacao> ObterSolicitacoesPermitidas()
        {
            if (eventoSelecionado != null)
            {
                try
                {
                    return solicitacaoService.ListarDoEvento(
                        usuarioAutenticado,
                        eventoSelecionado.IdEvento);
                }
                catch (UnauthorizedAccessException)
                {
                    return solicitacaoService.ListarMinhas(usuarioAutenticado);
                }
            }

            return solicitacaoService.ListarMinhas(usuarioAutenticado);
        }

        private bool PodeSolicitarComoMembro()
        {
            try
            {
                List<Escala> escalas = new EscalaService().ListarMinhas(usuarioAutenticado);
                return escalas.Count > 0;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }

        private void ConfigurarFiltros()
        {
            string setorSelecionado = cmbFiltroSolicitacoes.Text;
            cmbFiltroSolicitacoes.Items.Clear();
            cmbFiltroSolicitacoes.Items.Add("Todos os setores");

            foreach (DataGridViewRow linha in dgvSolicitacoesQuermesse.Rows)
            {
                string setor = Convert.ToString(linha.Cells[2].Value);

                if (!cmbFiltroSolicitacoes.Items.Contains(setor))
                {
                    cmbFiltroSolicitacoes.Items.Add(setor);
                }
            }

            int indiceSetor = cmbFiltroSolicitacoes.Items.IndexOf(setorSelecionado);

            if (indiceSetor < 0)
            {
                indiceSetor = 0;
            }

            cmbFiltroSolicitacoes.SelectedIndex = indiceSetor;

            cmbStatusListaSolicitacoes.Items.Clear();
            cmbStatusListaSolicitacoes.Items.Add("Todas");
            cmbStatusListaSolicitacoes.Items.Add(SituacoesSolicitacao.Pendente);
            cmbStatusListaSolicitacoes.Items.Add(SituacoesSolicitacao.Aprovada);
            cmbStatusListaSolicitacoes.Items.Add(SituacoesSolicitacao.Atendida);
            cmbStatusListaSolicitacoes.Items.Add(SituacoesSolicitacao.Rejeitada);
            cmbStatusListaSolicitacoes.Items.Add(SituacoesSolicitacao.Cancelada);
            cmbStatusListaSolicitacoes.SelectedIndex = 0;
        }

        private void AplicarFiltros()
        {
            if (cmbFiltroSolicitacoes.SelectedIndex < 0 ||
                cmbStatusListaSolicitacoes.SelectedIndex < 0)
            {
                return;
            }

            foreach (DataGridViewRow linha in dgvSolicitacoesQuermesse.Rows)
            {
                bool setorCorresponde = cmbFiltroSolicitacoes.SelectedIndex == 0 ||
                    Convert.ToString(linha.Cells[2].Value) == cmbFiltroSolicitacoes.Text;
                bool statusCorresponde = cmbStatusListaSolicitacoes.SelectedIndex == 0 ||
                    string.Equals(
                        Convert.ToString(linha.Cells[6].Value),
                        cmbStatusListaSolicitacoes.Text,
                        StringComparison.OrdinalIgnoreCase);
                linha.Visible = setorCorresponde && statusCorresponde;
            }

            dgvSolicitacoesQuermesse.ClearSelection();
        }
    }
}
