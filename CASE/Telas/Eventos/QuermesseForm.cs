using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CASE.Models;
using CASE.Repositories;
using CASE.Services;
using CASE.Utils;
using MySql.Data.MySqlClient;

namespace CASE.Quermesse
{
    public partial class QuermesseForm : Form
    {
        private readonly Usuario usuarioAutenticado;
        private readonly Evento eventoEmEdicao;
        private readonly EventoService eventoService = new EventoService();
        private readonly TipoEventoService tipoEventoService = new TipoEventoService();

        public QuermesseForm() : this(null, null)
        {
        }

        public QuermesseForm(Usuario usuario, Evento evento)
        {
            InitializeComponent();
            IconoAplicacao.Aplicar(this);
            FrontendTheme.Aplicar(this);
            AlinharCamposEvento();

            usuarioAutenticado = usuario;
            eventoEmEdicao = evento;
            cmbTipoEvento.Items.Clear();
            cmbTipoEvento.DisplayMember = "Nome";
            cmbTipoEvento.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoEvento.DrawMode = DrawMode.Normal;
            cmbTipoEvento.FlatStyle = FlatStyle.Standard;

            labelAvisoFormulario.Text = string.Empty;
            btnSalvarVisual.Enabled = false;

            // A situação é controlada pelas transições do Service, não pelo formulário.
            cmbSituacaoQuermesse.Items.Clear();
            cmbSituacaoQuermesse.Enabled = false;
            cmbSituacaoQuermesse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSituacaoQuermesse.DrawMode = DrawMode.Normal;
            cmbSituacaoQuermesse.FlatStyle = FlatStyle.Standard;

            lblResponsavel.Text = "Gerente responsável (automático)";
            txtResponsavelQuermesse.ReadOnly = true;
            txtResponsavelQuermesse.TabStop = false;
            lblSituacao.Text = "Situação (automática)";

            // Dá espaço entre o ComboBox de situação e o próximo rótulo.
            lblObservacao.Top = 438;
            txtObservacaoQuermesse.Top = 464;
            labelAvisoFormulario.Top = 499;

            txtNomeQuermesse.TabIndex = 0;
            cmbTipoEvento.TabIndex = 1;
            txtLocalQuermesse.TabIndex = 2;
            dtpInicioQuermesse.TabIndex = 3;
            dtpFimQuermesse.TabIndex = 4;
            txtResponsavelQuermesse.TabIndex = 5;
            txtObservacaoQuermesse.TabIndex = 6;
            cmbSituacaoQuermesse.TabStop = false;
            btnSalvarVisual.TabIndex = 7;
            btnCancelar.TabIndex = 8;

            if (System.ComponentModel.LicenseManager.UsageMode !=
                System.ComponentModel.LicenseUsageMode.Designtime &&
                usuarioAutenticado != null)
            {
                CarregarTiposEvento();
                PrepararFormulario();

                if (btnSalvarVisual.Enabled)
                {
                    ExibirAviso("O responsável será definido pela conta autenticada.", false);
                }
                try
                {
                    string perfil;
                    new AutorizacaoService().ValidarUsuarioEObterPerfil(usuarioAutenticado, out perfil);
                    if (eventoEmEdicao != null)
                    {
                        txtResponsavelQuermesse.Text = eventoService.ConsultarNomeResponsavel(
                            usuarioAutenticado, eventoEmEdicao.IdEvento);
                    }
                    if (perfil != "Gerente")
                    {
                        btnSalvarVisual.Enabled = false;
                        txtNomeQuermesse.ReadOnly = true;
                        txtLocalQuermesse.ReadOnly = true;
                        txtObservacaoQuermesse.ReadOnly = true;
                        cmbTipoEvento.Enabled = false;
                        dtpInicioQuermesse.Enabled = false;
                        dtpFimQuermesse.Enabled = false;
                        ExibirAviso("Detalhes do evento. A edição é exclusiva do gerente responsável.", false);
                    }
                }
                catch (Exception)
                {
                    btnSalvarVisual.Enabled = false;
                    ExibirAviso("Não foi possível confirmar a autorização e o responsável deste evento.", true);
                }
            }
            else
            {
                ExibirAviso("Entre com uma conta autorizada para cadastrar eventos.", true);
            }
        }

        private void AlinharCamposEvento()
        {
            Control[] campos = { txtNomeQuermesse, cmbTipoEvento, txtLocalQuermesse,
                dtpInicioQuermesse, dtpFimQuermesse, txtResponsavelQuermesse,
                cmbSituacaoQuermesse, txtObservacaoQuermesse };
            Label[] rotulos = { lblNomedoevento, lblTipoEvento, lblLocal, lblDatainicial,
                lblDatafinal, lblResponsavel, lblSituacao, lblObservacao };
            for (int i = 0; i < campos.Length; i++)
            {
                campos[i].Left = 28;
                campos[i].Width = panelFormulario.ClientSize.Width - 56;
                campos[i].Top = 98 + i * 52;
                campos[i].Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                rotulos[i].Left = 28;
                rotulos[i].Top = campos[i].Top - 24;
                rotulos[i].AutoSize = false;
                rotulos[i].Width = campos[i].Width;
                rotulos[i].Height = 21;
                rotulos[i].Anchor = campos[i].Anchor;
            }
        }

        private void PrepararFormulario()
        {
            if (eventoEmEdicao == null)
            {
                Text = lblTitulo.Text = "Novo evento";
                cmbSituacaoQuermesse.Items.Add(SituacoesEvento.Planejado);
                cmbSituacaoQuermesse.SelectedIndex = 0;
                txtResponsavelQuermesse.Text = usuarioAutenticado.Login;
                dtpInicioQuermesse.Value = DateTime.Today;
                dtpFimQuermesse.Value = DateTime.Today;
                return;
            }

            Text = lblTitulo.Text = "Editar evento";
            txtNomeQuermesse.Text = eventoEmEdicao.Nome;
            txtLocalQuermesse.Text = eventoEmEdicao.Local;
            txtResponsavelQuermesse.Text = usuarioAutenticado.Login;
            txtObservacaoQuermesse.Text = eventoEmEdicao.Observacao;
            dtpInicioQuermesse.Value = eventoEmEdicao.DataInicio;
            dtpFimQuermesse.Value = eventoEmEdicao.DataFim;
            cmbSituacaoQuermesse.Items.Add(eventoEmEdicao.Situacao);
            cmbSituacaoQuermesse.SelectedIndex = 0;
            SelecionarTipoEvento(eventoEmEdicao.IdTipoEvento);
        }

        private void btnSalvarVisual_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                SalvarEventoDemo();
                return;
            }

            if (usuarioAutenticado == null)
            {
                ExibirAviso("Entre com uma conta autorizada para salvar eventos.", true);
                return;
            }

            TipoEvento tipoEvento = cmbTipoEvento.SelectedItem as TipoEvento;

            if (tipoEvento == null)
            {
                ExibirAviso("Selecione um tipo de evento cadastrado.", true);
                cmbTipoEvento.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNomeQuermesse.Text))
            {
                ExibirAviso("Informe o nome do evento.", true);
                txtNomeQuermesse.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLocalQuermesse.Text))
            {
                ExibirAviso("Informe o local do evento.", true);
                txtLocalQuermesse.Focus();
                return;
            }

            if (dtpFimQuermesse.Value.Date < dtpInicioQuermesse.Value.Date)
            {
                ExibirAviso("A data final não pode ser anterior à data inicial.", true);
                dtpFimQuermesse.Focus();
                return;
            }

            try
            {
                Evento evento;

                if (eventoEmEdicao == null)
                {
                    evento = new Evento();
                    evento.IdInstituicao = eventoService.ObterIdInstituicaoUnica(
                        usuarioAutenticado);
                }
                else
                {
                    evento = new Evento();
                    evento.IdEvento = eventoEmEdicao.IdEvento;
                    evento.IdInstituicao = eventoEmEdicao.IdInstituicao;
                    evento.IdResponsavel = eventoEmEdicao.IdResponsavel;
                    evento.Situacao = eventoEmEdicao.Situacao;
                    evento.Versao = eventoEmEdicao.Versao;
                    evento.IdUsuarioCriacao = eventoEmEdicao.IdUsuarioCriacao;
                    evento.CriadoEm = eventoEmEdicao.CriadoEm;
                }

                evento.IdTipoEvento = tipoEvento.IdTipoEvento;
                evento.Nome = txtNomeQuermesse.Text.Trim();
                evento.Local = txtLocalQuermesse.Text.Trim();
                evento.DataInicio = dtpInicioQuermesse.Value.Date;
                evento.DataFim = dtpFimQuermesse.Value.Date;
                evento.Observacao = txtObservacaoQuermesse.Text.Trim();

                if (eventoEmEdicao == null)
                {
                    eventoService.Criar(usuarioAutenticado, evento);
                }
                else
                {
                    eventoService.Atualizar(usuarioAutenticado, evento);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                ExibirAviso(ex.Message, true);
            }
            catch (InvalidOperationException ex)
            {
                ExibirAviso(ex.Message, true);
            }
            catch (UnauthorizedAccessException)
            {
                ExibirAviso("Sua conta não tem permissão para salvar este evento.", true);
            }
            catch (MySqlException)
            {
                ExibirAviso("Não foi possível gravar o evento no banco de dados.", true);
            }
            catch (Exception)
            {
                ExibirAviso("Não foi possível salvar o evento. Tente novamente.", true);
            }
        }

        private void CarregarTiposEvento()
        {
            try
            {
                List<TipoEvento> tiposEvento = tipoEventoService.Listar(usuarioAutenticado);

                foreach (TipoEvento tipoEvento in tiposEvento)
                {
                    cmbTipoEvento.Items.Add(tipoEvento);
                }

                if (eventoEmEdicao != null)
                {
                    SelecionarTipoEvento(eventoEmEdicao.IdTipoEvento);
                }
                else if (cmbTipoEvento.Items.Count > 0)
                {
                    cmbTipoEvento.SelectedIndex = 0;
                }

                if (cmbTipoEvento.Items.Count == 0)
                {
                    btnSalvarVisual.Enabled = false;
                    ExibirAviso(
                        "Não há tipos de evento cadastrados. Peça a um administrador para cadastrar um tipo antes de continuar.",
                        true);
                }
                else
                {
                    btnSalvarVisual.Enabled = true;
                }
            }
            catch (Exception)
            {
                ExibirAviso("Não foi possível carregar os tipos de evento. Confira o acesso ao banco.", true);
                btnSalvarVisual.Enabled = false;
            }
        }

        private void ExibirAviso(string mensagem, bool erro)
        {
            labelAvisoFormulario.Text = mensagem;
            labelAvisoFormulario.ForeColor = erro
                ? System.Drawing.Color.FromArgb(190, 55, 65)
                : VisualUtils.TextoSuave;
        }

        private void SelecionarTipoEvento(int idTipoEvento)
        {
            for (int indice = 0; indice < cmbTipoEvento.Items.Count; indice++)
            {
                TipoEvento tipoEvento = cmbTipoEvento.Items[indice] as TipoEvento;

                if (tipoEvento != null && tipoEvento.IdTipoEvento == idTipoEvento)
                {
                    cmbTipoEvento.SelectedIndex = indice;
                    return;
                }
            }
        }
    }
}
