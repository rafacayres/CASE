using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class PessoaSetorForm : Form
    {
        private readonly Usuario usuarioAutenticado;
        private readonly Evento evento;
        private readonly EquipeEvento vinculo;
        private readonly EquipeEventoService equipeService = new EquipeEventoService();

        public PessoaSetorForm() : this(null, null, null)
        {
        }

        public PessoaSetorForm(Usuario usuario, Evento evento, EquipeEvento item)
        {
            InitializeComponent();
            IconoAplicacao.Aplicar(this);
            FrontendTheme.Aplicar(this);

            usuarioAutenticado = usuario;
            this.evento = evento;
            vinculo = item;

            lblPerfildeacesso.Text = "Perfil neste evento";
            lblSetorbarraca.Text = "Setor (definido nas escalas)";
            lblTurno.Text = "Turno (definido nas escalas)";
            cmbSetorFuncionario.Enabled = false;
            cmbTurnoFuncionario.Enabled = false;
            cmbSetorFuncionario.Items.Clear();
            cmbTurnoFuncionario.Items.Clear();
            cmbPerfilFuncionario.Items.Clear();

            if (usuarioAutenticado == null || this.evento == null)
            {
                btnSalvarFuncionario.Enabled = false;
                labelAvisoFormulario.Text = "Selecione um evento e entre com uma conta autorizada.";
                return;
            }

            CarregarPerfis();

            if (vinculo == null)
            {
                Text = lblTitulo.Text = "Vincular membro ao evento";
                txtNomeFuncionario.ReadOnly = true;
                txtLoginFuncionario.Leave += txtLoginFuncionario_Leave;
            }
            else
            {
                CarregarVinculo();
            }
        }

        private void CarregarPerfis()
        {
            try
            {
                List<Perfil> perfis = equipeService.ListarPerfis(
                    usuarioAutenticado,
                    evento.IdEvento);

                foreach (Perfil perfil in perfis)
                {
                    cmbPerfilFuncionario.Items.Add(perfil);
                }

                cmbPerfilFuncionario.DisplayMember = "Nome";

                if (cmbPerfilFuncionario.Items.Count == 0)
                {
                    btnSalvarFuncionario.Enabled = false;
                    labelAvisoFormulario.Text = "Não há perfis disponíveis para este evento.";
                }
            }
            catch (Exception)
            {
                btnSalvarFuncionario.Enabled = false;
                labelAvisoFormulario.Text = "Não foi possível carregar os perfis do evento.";
            }
        }

        private void CarregarVinculo()
        {
            try
            {
                Pessoa pessoa = equipeService.BuscarPessoaVinculada(
                    usuarioAutenticado,
                    evento.IdEvento,
                    vinculo.IdEquipe);

                if (pessoa == null)
                {
                    btnSalvarFuncionario.Enabled = false;
                    labelAvisoFormulario.Text = "O membro vinculado não foi encontrado.";
                    return;
                }

                txtNomeFuncionario.Text = pessoa.Nome;
                txtLoginFuncionario.Text = equipeService.BuscarLoginDaPessoaVinculada(
                    usuarioAutenticado,
                    evento.IdEvento,
                    vinculo.IdEquipe);
                txtLoginFuncionario.ReadOnly = true;
                chkFuncionarioAtivo.Checked = vinculo.Ativo;

                foreach (Perfil perfil in cmbPerfilFuncionario.Items)
                {
                    if (perfil.IdPerfil == vinculo.IdPerfil)
                    {
                        cmbPerfilFuncionario.SelectedItem = perfil;
                        break;
                    }
                }

                Text = lblTitulo.Text = "Editar vínculo de equipe";
            }
            catch (Exception)
            {
                btnSalvarFuncionario.Enabled = false;
                labelAvisoFormulario.Text = "Não foi possível carregar o vínculo selecionado.";
            }
        }

        private void txtLoginFuncionario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLoginFuncionario.Text))
            {
                txtNomeFuncionario.Clear();
                return;
            }

            try
            {
                Pessoa pessoa = equipeService.BuscarPessoaDisponivelPorLogin(
                    usuarioAutenticado,
                    evento.IdEvento,
                    txtLoginFuncionario.Text.Trim());

                if (pessoa == null)
                {
                    txtNomeFuncionario.Clear();
                    labelAvisoFormulario.Text = "Não foi encontrado membro aprovado e disponível com esse login.";
                    return;
                }

                txtNomeFuncionario.Text = pessoa.Nome;
                labelAvisoFormulario.Text = "Membro localizado. Escolha o perfil e salve o vínculo.";
            }
            catch (Exception)
            {
                txtNomeFuncionario.Clear();
                labelAvisoFormulario.Text = "Não foi possível consultar esse membro.";
            }
        }

        private void btnSalvarFuncionario_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                SalvarMembroDemo();
                return;
            }

            if (cmbPerfilFuncionario.SelectedItem == null)
            {
                labelAvisoFormulario.Text = "Selecione o perfil do membro neste evento.";
                cmbPerfilFuncionario.Focus();
                return;
            }

            Perfil perfil = (Perfil)cmbPerfilFuncionario.SelectedItem;

            try
            {
                if (vinculo == null)
                {
                    if (string.IsNullOrWhiteSpace(txtLoginFuncionario.Text) ||
                        string.IsNullOrWhiteSpace(txtNomeFuncionario.Text))
                    {
                        labelAvisoFormulario.Text = "Informe o login de um membro aprovado e disponível.";
                        txtLoginFuncionario.Focus();
                        return;
                    }

                    equipeService.VincularMembro(
                        usuarioAutenticado,
                        evento.IdEvento,
                        txtLoginFuncionario.Text.Trim(),
                        perfil.IdPerfil);
                }
                else
                {
                    vinculo.IdPerfil = perfil.IdPerfil;
                    vinculo.Ativo = chkFuncionarioAtivo.Checked;
                    equipeService.AtualizarVinculo(usuarioAutenticado, vinculo);
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
                labelAvisoFormulario.Text = "Sua conta não pode alterar a equipe deste evento.";
            }
            catch (Exception)
            {
                labelAvisoFormulario.Text = "Não foi possível salvar o vínculo.";
            }
        }
    }
}
