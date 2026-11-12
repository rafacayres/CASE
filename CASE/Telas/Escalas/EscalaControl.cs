using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CASE.Models;
using CASE.Repositories;
using CASE.Services;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class EscalaControl : UserControl
    {
        private readonly Usuario usuarioAutenticado;
        private readonly Evento eventoSelecionado;
        private readonly EscalaService escalaService = new EscalaService();
        private readonly EquipeEventoService equipeService = new EquipeEventoService();
        private readonly JornadaSetorService jornadaService = new JornadaSetorService();
        private readonly SetorService setorService = new SetorService();
        private readonly TurnoRepository turnoRepository = new TurnoRepository();
        private bool podeAdministrarEscalas;
        private bool usuarioEhMembro;

        public EscalaControl() : this(null, null)
        {
        }

        public EscalaControl(QuermessePreview exemplos) : this(null, null)
        {
        }

        public EscalaControl(Usuario usuario, Evento evento)
        {
            InitializeComponent();
            FrontendTheme.Aplicar(this);
            usuarioAutenticado = usuario;
            eventoSelecionado = evento;
            dgvEscala.CellDoubleClick += dgvEscala_CellDoubleClick;

            if (usuarioAutenticado == null)
            {
                btnNovaAlocacao.Enabled = false;
                labelAviso.Text = "Entre no sistema para consultar as escalas.";
                return;
            }

            try
            {
                string nomePerfil;
                new AutorizacaoService().ValidarUsuarioEObterPerfil(
                    usuarioAutenticado,
                    out nomePerfil);
                usuarioEhMembro = string.Equals(
                    nomePerfil,
                    "Membro",
                    StringComparison.OrdinalIgnoreCase);
                podeAdministrarEscalas = string.Equals(
                    nomePerfil,
                    "Gerente",
                    StringComparison.OrdinalIgnoreCase);

                if (podeAdministrarEscalas && eventoSelecionado != null)
                {
                    Evento eventoAutorizado = new EventoService().BuscarPorId(
                        usuarioAutenticado,
                        eventoSelecionado.IdEvento);

                    podeAdministrarEscalas = eventoAutorizado != null &&
                        eventoAutorizado.IdResponsavel == usuarioAutenticado.IdPessoa;
                }
            }
            catch (Exception)
            {
                podeAdministrarEscalas = false;
            }

            if (!podeAdministrarEscalas || eventoSelecionado == null)
            {
                btnNovaAlocacao.Enabled = false;
            }

            if (eventoSelecionado == null && !usuarioEhMembro)
            {
                labelAviso.Text = "Abra Eventos e selecione um evento antes de alocar funcionários.";
                return;
            }

            CarregarEscalas();
        }

        private void btnNovaAlocacao_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                AbrirEscalaDemo(false);
                return;
            }

            if (eventoSelecionado == null || !podeAdministrarEscalas)
            {
                labelAviso.Text = "Somente o gerente responsável pode criar escalas do evento.";
                return;
            }

            AbrirFormulario(null);
        }

        private void dgvEscala_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dadosDemo != null)
            {
                AbrirEscalaDemo(true);
                return;
            }

            if (e.RowIndex < 0 || eventoSelecionado == null)
            {
                return;
            }

            Escala escala = dgvEscala.Rows[e.RowIndex].Tag as Escala;

            if (escala != null)
            {
                AbrirFormulario(escala);
            }
        }

        private void AbrirFormulario(Escala escala)
        {
            using (EscalaForm formulario = new EscalaForm(
                usuarioAutenticado,
                eventoSelecionado,
                escala))
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    CarregarEscalas();
                }
            }
        }

        private void Filtros_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltros();
        }

        private void CarregarEscalas()
        {
            dgvEscala.Rows.Clear();

            if (eventoSelecionado == null)
            {
                if (usuarioEhMembro)
                {
                    CarregarMinhasEscalas();
                }
                else
                {
                    labelAviso.Text = "Selecione um evento para consultar as escalas.";
                }

                return;
            }

            try
            {
                List<Escala> escalas = escalaService.ListarPorEvento(
                    usuarioAutenticado,
                    eventoSelecionado.IdEvento);

                foreach (Escala escala in escalas)
                {
                    AdicionarLinha(escala);
                }

                PreencherFiltros();
                AplicarFiltros();
                labelAviso.Text = escalas.Count + " escala(s) carregada(s).";
            }
            catch (UnauthorizedAccessException)
            {
                btnNovaAlocacao.Enabled = false;
                labelAviso.Text = "Sua conta não pode consultar as escalas deste evento.";
            }
            catch (Exception)
            {
                labelAviso.Text = "Não foi possível carregar as escalas do evento.";
            }
        }

        private void CarregarMinhasEscalas()
        {
            try
            {
                List<Escala> escalas = escalaService.ListarMinhas(usuarioAutenticado);

                foreach (Escala escala in escalas)
                {
                    AdicionarLinha(escala);
                }

                PreencherFiltros();
                AplicarFiltros();
                btnNovaAlocacao.Enabled = false;
                labelAviso.Text = escalas.Count + " escala(s) vinculada(s) à sua conta.";
            }
            catch (UnauthorizedAccessException)
            {
                btnNovaAlocacao.Enabled = false;
                labelAviso.Text = "Selecione um evento ou consulte suas escalas com uma conta de membro.";
            }
            catch (Exception)
            {
                labelAviso.Text = "Não foi possível carregar as escalas.";
            }
        }

        private void AdicionarLinha(Escala escala)
        {
            EquipeEvento equipe = equipeService.BuscarPorId(
                usuarioAutenticado,
                escala.IdEquipe);
            JornadaSetor jornada = jornadaService.BuscarPorId(
                usuarioAutenticado,
                escala.IdJornada);

            if (equipe == null || jornada == null)
            {
                return;
            }

            Pessoa pessoa = equipeService.BuscarPessoaVinculada(
                usuarioAutenticado,
                equipe.IdEvento,
                equipe.IdEquipe);
            Setor setor = setorService.BuscarPorId(
                usuarioAutenticado,
                jornada.IdSetor);
            Turno turno = turnoRepository.BuscarPorId(jornada.IdTurno);

            string nomePessoa = "Pessoa " + equipe.IdPessoa;
            string nomeSetor = "Setor " + jornada.IdSetor;
            string nomeTurno = "Turno " + jornada.IdTurno;

            if (pessoa != null)
            {
                nomePessoa = pessoa.Nome;
            }

            if (setor != null)
            {
                nomeSetor = setor.Nome;
            }

            if (turno != null)
            {
                nomeTurno = turno.Nome;
            }

            int indiceLinha = dgvEscala.Rows.Add(
                nomePessoa,
                nomeSetor,
                jornada.Data.ToString("dd/MM/yyyy"),
                nomeTurno,
                escala.EntradaPrevista.ToString(@"hh\:mm"),
                escala.SaidaPrevista.ToString(@"hh\:mm"),
                escala.Situacao);
            dgvEscala.Rows[indiceLinha].Tag = escala;
        }

        private void PreencherFiltros()
        {
            string setorSelecionado = cmbFiltroEscala.Text;
            string turnoSelecionado = cmbFiltroTurnoEscala.Text;

            cmbFiltroEscala.Items.Clear();
            cmbFiltroTurnoEscala.Items.Clear();
            cmbFiltroEscala.Items.Add("Todos os setores");
            cmbFiltroTurnoEscala.Items.Add("Todos os turnos");

            foreach (DataGridViewRow linha in dgvEscala.Rows)
            {
                AdicionarFiltroSeAusente(cmbFiltroEscala, Convert.ToString(linha.Cells[1].Value));
                AdicionarFiltroSeAusente(cmbFiltroTurnoEscala, Convert.ToString(linha.Cells[3].Value));
            }

            SelecionarFiltro(cmbFiltroEscala, setorSelecionado, "Todos os setores");
            SelecionarFiltro(cmbFiltroTurnoEscala, turnoSelecionado, "Todos os turnos");
        }

        private static void AdicionarFiltroSeAusente(ComboBox combo, string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return;
            }

            if (!combo.Items.Contains(valor))
            {
                combo.Items.Add(valor);
            }
        }

        private static void SelecionarFiltro(ComboBox combo, string valor, string padrao)
        {
            int indice = combo.Items.IndexOf(valor);

            if (indice < 0)
            {
                indice = combo.Items.IndexOf(padrao);
            }

            if (indice >= 0)
            {
                combo.SelectedIndex = indice;
            }
        }

        private void AplicarFiltros()
        {
            if (cmbFiltroEscala.SelectedIndex < 0 || cmbFiltroTurnoEscala.SelectedIndex < 0)
            {
                return;
            }

            foreach (DataGridViewRow linha in dgvEscala.Rows)
            {
                bool setorCorresponde = cmbFiltroEscala.SelectedIndex == 0 ||
                    Convert.ToString(linha.Cells[1].Value) == cmbFiltroEscala.Text;
                bool turnoCorresponde = cmbFiltroTurnoEscala.SelectedIndex == 0 ||
                    Convert.ToString(linha.Cells[3].Value) == cmbFiltroTurnoEscala.Text;
                linha.Visible = setorCorresponde && turnoCorresponde;
            }

            dgvEscala.ClearSelection();
        }
    }
}
