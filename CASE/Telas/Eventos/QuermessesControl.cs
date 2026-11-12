using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class QuermessesControl : UserControl
    {
        public event Action<Evento> EventoSelecionado;

        private readonly Usuario usuarioAutenticado;
        private readonly EventoService eventoService = new EventoService();
        private readonly TipoEventoService tipoEventoService = new TipoEventoService();
        private readonly Dictionary<int, string> nomesTiposEvento = new Dictionary<int, string>();
        private List<Evento> eventosCarregados = new List<Evento>();

        public QuermessesControl() : this((Usuario)null)
        {
        }

        public QuermessesControl(QuermessePreview exemplos) : this((Usuario)null)
        {
        }

        public QuermessesControl(Usuario usuario)
        {
            InitializeComponent();
            FrontendTheme.Aplicar(this);
            usuarioAutenticado = usuario;
            btnNovaQuermesse.Enabled = false;
            if (usuario != null)
            {
                try
                {
                    string perfil;
                    new AutorizacaoService().ValidarUsuarioEObterPerfil(usuario, out perfil);
                    btnNovaQuermesse.Enabled = perfil == "Gerente";
                }
                catch (Exception)
                {
                    btnNovaQuermesse.Enabled = false;
                }
            }
            dgvQuermesses.CellDoubleClick += dgvQuermesses_CellDoubleClick;
            dgvQuermesses.SelectionChanged += dgvQuermesses_SelectionChanged;
            ConfigurarTransicoesEvento();

            if (System.ComponentModel.LicenseManager.UsageMode !=
                System.ComponentModel.LicenseUsageMode.Designtime &&
                usuarioAutenticado != null)
            {
                CarregarEventos();
            }
        }

        private void ConfigurarTransicoesEvento()
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem iniciar = new ToolStripMenuItem("Iniciar evento");
            ToolStripMenuItem finalizar = new ToolStripMenuItem("Finalizar evento");
            ToolStripMenuItem cancelar = new ToolStripMenuItem("Cancelar evento");
            iniciar.Click += (s, e) => AlterarSituacaoSelecionada(SituacoesEvento.EmAndamento);
            finalizar.Click += (s, e) => AlterarSituacaoSelecionada(SituacoesEvento.Finalizado);
            cancelar.Click += (s, e) => AlterarSituacaoSelecionada(SituacoesEvento.Cancelado);
            menu.Items.Add(iniciar);
            menu.Items.Add(finalizar);
            menu.Items.Add(cancelar);
            menu.Opening += (s, e) =>
            {
                Evento evento = ObterEventoSelecionado();
                bool gerente = dadosDemo != null;
                if (usuarioAutenticado != null && evento != null)
                {
                    try
                    {
                        string perfil;
                        new AutorizacaoService().ValidarUsuarioEObterPerfil(usuarioAutenticado, out perfil);
                        gerente = perfil == "Gerente" && evento.IdResponsavel == usuarioAutenticado.IdPessoa;
                    }
                    catch (Exception)
                    {
                        gerente = false;
                    }
                }
                iniciar.Enabled = gerente && evento.Situacao == SituacoesEvento.Planejado;
                finalizar.Enabled = gerente && evento.Situacao == SituacoesEvento.EmAndamento;
                cancelar.Enabled = gerente && (evento.Situacao == SituacoesEvento.Planejado ||
                    evento.Situacao == SituacoesEvento.EmAndamento);
            };
            dgvQuermesses.ContextMenuStrip = menu;
            dgvQuermesses.CellMouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    dgvQuermesses.CurrentCell = dgvQuermesses.Rows[e.RowIndex].Cells[0];
                    dgvQuermesses.Rows[e.RowIndex].Selected = true;
                }
            };
            Button situacao = new Button();
            situacao.Name = "btnSituacaoEvento";
            situacao.Text = "Situação do evento";
            situacao.SetBounds(520, 12, 210, 36);
            situacao.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            situacao.Click += (s, e) => menu.Show(situacao, new System.Drawing.Point(0, situacao.Height));
            panelListaQuermesses.Controls.Add(situacao);
            lblEventoscadastrados.Width = 480;
            VisualUtils.EstilizarBotao(situacao);
            Disposed += (s, e) => menu.Dispose();
        }

        private void AlterarSituacaoSelecionada(string situacao)
        {
            if (dadosDemo != null)
            {
                AlterarEventoDemo(situacao);
                return;
            }

            Evento evento = ObterEventoSelecionado();
            if (evento == null || usuarioAutenticado == null)
            {
                labelAviso.Text = "Selecione um evento autorizado.";
                return;
            }
            if (MessageBox.Show(this, "Confirma alterar a situação para " + situacao + "?",
                "Situação do evento", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
            try
            {
                eventoService.AlterarSituacao(usuarioAutenticado, evento.IdEvento, situacao);
                CarregarEventos();
                labelAviso.Text = "Situação atualizada.";
            }
            catch (Exception erro)
            {
                labelAviso.Text = "Não foi possível atualizar a situação do evento.";
                if (erro is ArgumentException || erro is InvalidOperationException || erro is UnauthorizedAccessException)
                {
                    labelAviso.Text = erro.Message;
                }
                System.Diagnostics.Trace.WriteLine(erro.GetType().Name + " em AlterarSituacaoSelecionada");
            }
        }

        private void btnNovaQuermesse_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                AbrirEventoDemo(null);
                return;
            }

            if (!VerificarSessao())
            {
                return;
            }

            using (var formulario = new QuermesseForm(usuarioAutenticado, null))
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    CarregarEventos();
                }
            }
        }

        private void btnEditarQuermesse_Click(object sender, EventArgs e)
        {
            AbrirEventoSelecionado();
        }

        private void dgvQuermesses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AbrirEventoSelecionado();
            }
        }

        private void dgvQuermesses_SelectionChanged(object sender, EventArgs e)
        {
            Evento evento = null;

            if (dgvQuermesses.CurrentRow != null)
            {
                evento = dgvQuermesses.CurrentRow.Tag as Evento;
            }

            Action<Evento> eventoSelecionado = EventoSelecionado;

            if (eventoSelecionado != null)
            {
                eventoSelecionado(evento);
            }
        }

        public Evento ObterEventoSelecionado()
        {
            if (dgvQuermesses.CurrentRow == null)
            {
                return null;
            }

            return dgvQuermesses.CurrentRow.Tag as Evento;
        }

        private void AbrirEventoSelecionado()
        {
            if (dadosDemo != null)
            {
                AbrirEventoDemo(ObterEventoSelecionado());
                return;
            }

            if (!VerificarSessao())
            {
                return;
            }

            if (dgvQuermesses.CurrentRow == null ||
                !(dgvQuermesses.CurrentRow.Tag is Evento))
            {
                labelAviso.Text = "Selecione um evento na lista.";
                return;
            }

            Evento evento = (Evento)dgvQuermesses.CurrentRow.Tag;

            using (var formulario = new QuermesseForm(usuarioAutenticado, evento))
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    CarregarEventos();
                }
            }
        }

        private bool VerificarSessao()
        {
            if (usuarioAutenticado != null)
            {
                return true;
            }

            MessageBox.Show(
                this,
                "Entre no sistema com uma conta autorizada para usar esta função.",
                "Eventos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return false;
        }

        private void CarregarEventos()
        {
            try
            {
                List<TipoEvento> tipos = tipoEventoService.Listar(usuarioAutenticado);
                nomesTiposEvento.Clear();

                foreach (TipoEvento tipo in tipos)
                {
                    nomesTiposEvento[tipo.IdTipoEvento] = tipo.Nome;
                }

                List<Evento> eventos = eventoService.Listar(usuarioAutenticado);
                eventosCarregados = eventos;

                dgvQuermesses.Rows.Clear();

                foreach (Evento evento in eventos)
                {
                    string nomeTipo = "Tipo " + evento.IdTipoEvento;

                    if (nomesTiposEvento.ContainsKey(evento.IdTipoEvento))
                    {
                        nomeTipo = nomesTiposEvento[evento.IdTipoEvento];
                    }

                    int indiceLinha = dgvQuermesses.Rows.Add(
                        evento.Nome,
                        nomeTipo,
                        evento.Local,
                        evento.DataInicio.ToString("dd/MM/yyyy"),
                        evento.DataFim.ToString("dd/MM/yyyy"),
                        evento.IdResponsavel.ToString(),
                        evento.Situacao);

                    dgvQuermesses.Rows[indiceLinha].Tag = evento;
                }

                dgvQuermesses.ClearSelection();
                SelecionarPrimeiroEventoSeDisponivel();

                if (eventos.Count == 0)
                {
                    labelAviso.Text = "Nenhum evento cadastrado.";
                }
                else
                {
                    labelAviso.Text = eventos.Count + " evento(s) carregado(s) do banco.";
                }

                if (eventos.Count > 0)
                {
                    Evento eventoAtual = eventos[0];
                    lblQuermessedeSaoJoao2026.Text = eventoAtual.Nome;
                    lblPatiodacomunidadeEmandamento.Text =
                        eventoAtual.Local + " · " + eventoAtual.Situacao;
                }
                else
                {
                    lblQuermessedeSaoJoao2026.Text = "Nenhum evento cadastrado";
                    lblPatiodacomunidadeEmandamento.Text = string.Empty;
                }
            }
            catch (UnauthorizedAccessException)
            {
                eventosCarregados = new List<Evento>();
                dgvQuermesses.Rows.Clear();
                labelAviso.Text = "Sua conta não tem permissão para consultar eventos.";
                MessageBox.Show(
                    this,
                    "Sua conta não tem permissão para consultar os eventos.",
                    "Eventos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                eventosCarregados = new List<Evento>();
                dgvQuermesses.Rows.Clear();
                labelAviso.Text = "Não foi possível carregar os eventos.";
                MessageBox.Show(
                    this,
                    "Não foi possível consultar os eventos. Confira a conexão e suas permissões.",
                    "Eventos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void Pesquisar(string texto)
        {
            dgvQuermesses.Rows.Clear();
            string termo = texto;
            if (termo == null)
            {
                termo = string.Empty;
            }

            foreach (Evento evento in eventosCarregados)
            {
                string conteudo = evento.Nome + " " + evento.Local + " " + evento.Situacao;
                if (conteudo.IndexOf(termo, StringComparison.CurrentCultureIgnoreCase) < 0)
                {
                    continue;
                }

                string nomeTipo = "Tipo " + evento.IdTipoEvento;
                if (nomesTiposEvento.ContainsKey(evento.IdTipoEvento))
                {
                    nomeTipo = nomesTiposEvento[evento.IdTipoEvento];
                }

                int indiceLinha = dgvQuermesses.Rows.Add(
                    evento.Nome,
                    nomeTipo,
                    evento.Local,
                    evento.DataInicio.ToString("dd/MM/yyyy"),
                    evento.DataFim.ToString("dd/MM/yyyy"),
                    evento.IdResponsavel.ToString(),
                    evento.Situacao);
                dgvQuermesses.Rows[indiceLinha].Tag = evento;
            }

            dgvQuermesses.ClearSelection();
            SelecionarPrimeiroEventoSeDisponivel();

            if (dgvQuermesses.Rows.Count == 0)
            {
                labelAviso.Text = string.IsNullOrWhiteSpace(termo)
                    ? "Nenhum evento cadastrado."
                    : "Nenhum evento corresponde à busca.";
            }
            else
            {
                labelAviso.Text = dgvQuermesses.Rows.Count + " evento(s) encontrado(s).";
            }
        }

        private void SelecionarPrimeiroEventoSeDisponivel()
        {
            if (dgvQuermesses.Rows.Count == 0 || dgvQuermesses.Columns.Count == 0)
            {
                return;
            }

            dgvQuermesses.CurrentCell = dgvQuermesses.Rows[0].Cells[0];
            dgvQuermesses.Rows[0].Selected = true;
        }
    }
}
