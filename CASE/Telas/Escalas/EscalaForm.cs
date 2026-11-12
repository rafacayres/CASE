using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class EscalaForm : Form
    {
        private readonly Usuario usuarioAutenticado;
        private readonly Evento eventoSelecionado;
        private readonly Escala escalaExistente;
        private readonly EscalaService escalaService = new EscalaService();
        private readonly EquipeEventoService equipeService = new EquipeEventoService();
        private readonly SetorService setorService = new SetorService();
        private readonly JornadaSetorService jornadaService = new JornadaSetorService();

        public EscalaForm() : this(null, null, null)
        {
        }

        public EscalaForm(QuermessePreview exemplos) : this(null, null, null)
        {
        }

        public EscalaForm(Usuario usuario, Evento evento, Escala escala)
        {
            InitializeComponent();
            IconoAplicacao.Aplicar(this);
            FrontendTheme.Aplicar(this);
            OrganizarCamposPorDependencia();

            usuarioAutenticado = usuario;
            eventoSelecionado = evento;
            escalaExistente = escala;

            cmbEventoEscala.Items.Clear();
            cmbPessoaEscala.Items.Clear();
            cmbSetorEscala.Items.Clear();
            cmbTurnoEscala.Items.Clear();

            if (usuarioAutenticado == null || eventoSelecionado == null)
            {
                btnSalvarVisual.Enabled = false;
                labelAvisoFormulario.Text = "Selecione um evento e entre com uma conta autorizada.";
                return;
            }

            cmbEventoEscala.Items.Add(eventoSelecionado);
            cmbEventoEscala.DisplayMember = "Nome";
            cmbEventoEscala.SelectedIndex = 0;
            if (escalaExistente == null)
            {
                DateTime dia = DateTime.Today;
                if (dia < eventoSelecionado.DataInicio.Date)
                {
                    dia = eventoSelecionado.DataInicio.Date;
                }
                if (dia > eventoSelecionado.DataFim.Date)
                {
                    dia = eventoSelecionado.DataFim.Date;
                }
                dtpDiaEscala.Value = dia;
                dtpDiaEscala.MinDate = eventoSelecionado.DataInicio.Date;
                dtpDiaEscala.MaxDate = eventoSelecionado.DataFim.Date;
            }

            CarregarOpcoes();
        }

        private void OrganizarCamposPorDependencia()
        {
            // O preenchimento segue a ordem: evento, setor, data, turno, membro e horários.
            PosicionarCampo(lblSetorbarraca, cmbSetorEscala, 123, 145);
            PosicionarCampo(lblData, dtpDiaEscala, 174, 196);
            PosicionarCampo(lblTurno, cmbTurnoEscala, 225, 247);
            PosicionarCampo(lblFuncionario, cmbPessoaEscala, 276, 298);

            cmbEventoEscala.TabIndex = 0;
            cmbSetorEscala.TabIndex = 1;
            dtpDiaEscala.TabIndex = 2;
            cmbTurnoEscala.TabIndex = 3;
            cmbPessoaEscala.TabIndex = 4;
            dtpHoraInicioEscala.TabIndex = 5;
            dtpHoraFimEscala.TabIndex = 6;
            btnSalvarVisual.TabIndex = 7;
            btnCancelar.TabIndex = 8;
        }

        private static void PosicionarCampo(Control rotulo, Control campo, int topoRotulo, int topoCampo)
        {
            rotulo.Top = topoRotulo;
            campo.Top = topoCampo;
        }

        private void CarregarOpcoes()
        {
            try
            {
                List<EquipeEvento> equipe = equipeService.ListarPorEvento(
                    usuarioAutenticado,
                    eventoSelecionado.IdEvento);

                foreach (EquipeEvento item in equipe)
                {
                    if (!item.Ativo)
                    {
                        continue;
                    }

                    Pessoa pessoa = equipeService.BuscarPessoaVinculada(
                        usuarioAutenticado,
                        eventoSelecionado.IdEvento,
                        item.IdEquipe);

                    if (pessoa != null)
                    {
                        cmbPessoaEscala.Items.Add(pessoa);
                    }
                }

                cmbPessoaEscala.DisplayMember = "Nome";

                List<Setor> setores = setorService.ListarPorEvento(
                    usuarioAutenticado,
                    eventoSelecionado.IdEvento);

                foreach (Setor setor in setores)
                {
                    cmbSetorEscala.Items.Add(setor);
                }

                cmbSetorEscala.DisplayMember = "Nome";

                List<Turno> turnos = jornadaService.ListarTurnos(
                    usuarioAutenticado,
                    eventoSelecionado.IdEvento);

                foreach (Turno turno in turnos)
                {
                    cmbTurnoEscala.Items.Add(turno);
                }

                cmbTurnoEscala.DisplayMember = "Nome";

                if (cmbPessoaEscala.Items.Count == 0 ||
                    cmbSetorEscala.Items.Count == 0 ||
                    cmbTurnoEscala.Items.Count == 0)
                {
                    btnSalvarVisual.Enabled = false;
                    labelAvisoFormulario.Text = "Cadastre membros, setores e turnos antes de criar escalas.";
                    return;
                }

                cmbPessoaEscala.SelectedIndex = 0;
                cmbSetorEscala.SelectedIndex = 0;
                cmbTurnoEscala.SelectedIndex = 0;

                if (escalaExistente != null)
                {
                    PrepararEdicao();
                }
                else
                {
                    Text = lblTitulo.Text = "Criar escala";
                    labelAvisoFormulario.Text = "Defina o horário de trabalho do membro no setor.";
                }
            }
            catch (UnauthorizedAccessException)
            {
                btnSalvarVisual.Enabled = false;
                labelAvisoFormulario.Text = "Sua conta não pode administrar escalas deste evento.";
            }
            catch (Exception)
            {
                btnSalvarVisual.Enabled = false;
                labelAvisoFormulario.Text = "Não foi possível carregar os dados da escala.";
            }
        }

        private void PrepararEdicao()
        {
            try
            {
                JornadaSetor jornada = jornadaService.BuscarPorId(
                    usuarioAutenticado,
                    escalaExistente.IdJornada);
                EquipeEvento equipe = equipeService.BuscarPorId(
                    usuarioAutenticado,
                    escalaExistente.IdEquipe);

                if (jornada == null || equipe == null)
                {
                    btnSalvarVisual.Enabled = false;
                    labelAvisoFormulario.Text = "A escala ou seus vínculos não foram encontrados.";
                    return;
                }

                Pessoa pessoa = equipeService.BuscarPessoaVinculada(
                    usuarioAutenticado,
                    eventoSelecionado.IdEvento,
                    equipe.IdEquipe);
                SelecionarPessoa(pessoa);
                SelecionarSetor(jornada.IdSetor);
                SelecionarTurno(jornada.IdTurno);
                dtpDiaEscala.Value = jornada.Data;
                dtpHoraInicioEscala.Value = DateTime.Today.Add(escalaExistente.EntradaPrevista);
                dtpHoraFimEscala.Value = DateTime.Today.Add(escalaExistente.SaidaPrevista);
                Text = lblTitulo.Text = "Editar escala";
                labelAvisoFormulario.Text = "A alteração será validada contra conflitos de horário.";
            }
            catch (Exception)
            {
                btnSalvarVisual.Enabled = false;
                labelAvisoFormulario.Text = "Não foi possível carregar a escala para edição.";
            }
        }

        private void SelecionarPessoa(Pessoa selecionada)
        {
            if (selecionada == null)
            {
                return;
            }

            for (int indice = 0; indice < cmbPessoaEscala.Items.Count; indice++)
            {
                Pessoa pessoa = cmbPessoaEscala.Items[indice] as Pessoa;

                if (pessoa != null && pessoa.IdPessoa == selecionada.IdPessoa)
                {
                    cmbPessoaEscala.SelectedIndex = indice;
                    return;
                }
            }
        }

        private void SelecionarSetor(int idSetor)
        {
            for (int indice = 0; indice < cmbSetorEscala.Items.Count; indice++)
            {
                Setor setor = cmbSetorEscala.Items[indice] as Setor;

                if (setor != null && setor.IdSetor == idSetor)
                {
                    cmbSetorEscala.SelectedIndex = indice;
                    return;
                }
            }
        }

        private void SelecionarTurno(int idTurno)
        {
            for (int indice = 0; indice < cmbTurnoEscala.Items.Count; indice++)
            {
                Turno turno = cmbTurnoEscala.Items[indice] as Turno;

                if (turno != null && turno.IdTurno == idTurno)
                {
                    cmbTurnoEscala.SelectedIndex = indice;
                    return;
                }
            }
        }

        private void btnSalvarVisual_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                SalvarEscalaDemo();
                return;
            }

            if (cmbPessoaEscala.SelectedItem == null ||
                cmbSetorEscala.SelectedItem == null ||
                cmbTurnoEscala.SelectedItem == null)
            {
                labelAvisoFormulario.Text = "Selecione membro, setor e turno.";
                return;
            }

            Pessoa pessoaSelecionada = (Pessoa)cmbPessoaEscala.SelectedItem;
            Setor setorSelecionado = (Setor)cmbSetorEscala.SelectedItem;
            Turno turnoSelecionado = (Turno)cmbTurnoEscala.SelectedItem;

            try
            {
                List<EquipeEvento> equipe = equipeService.ListarPorEvento(
                    usuarioAutenticado,
                    eventoSelecionado.IdEvento);
                EquipeEvento equipeSelecionada = null;

                foreach (EquipeEvento item in equipe)
                {
                    if (item.IdPessoa == pessoaSelecionada.IdPessoa && item.Ativo)
                    {
                        equipeSelecionada = item;
                        break;
                    }
                }

                if (equipeSelecionada == null)
                {
                    labelAvisoFormulario.Text = "O membro não está ativo na equipe deste evento.";
                    return;
                }

                JornadaSetor jornada = jornadaService.ObterOuCriar(
                    usuarioAutenticado,
                    eventoSelecionado.IdEvento,
                    setorSelecionado.IdSetor,
                    dtpDiaEscala.Value.Date,
                    turnoSelecionado.IdTurno);

                Escala escala = new Escala();
                escala.IdEquipe = equipeSelecionada.IdEquipe;
                escala.IdJornada = jornada.IdJornada;
                escala.EntradaPrevista = dtpHoraInicioEscala.Value.TimeOfDay;
                escala.SaidaPrevista = dtpHoraFimEscala.Value.TimeOfDay;

                if (escalaExistente == null)
                {
                    escalaService.Criar(usuarioAutenticado, escala);
                }
                else
                {
                    escala.IdEscala = escalaExistente.IdEscala;
                    escalaService.Atualizar(usuarioAutenticado, escala);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                labelAvisoFormulario.Text = ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                labelAvisoFormulario.Text = ex.Message;
            }
            catch (UnauthorizedAccessException)
            {
                labelAvisoFormulario.Text = "Sua conta não pode salvar escalas neste evento.";
            }
            catch (Exception)
            {
                labelAvisoFormulario.Text = "Não foi possível salvar a escala.";
            }
        }
    }
}
