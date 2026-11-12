using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CASE.Models;
using CASE.Repositories;
using CASE.Services;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class EquipeSetoresControl : UserControl
    {
        private readonly Usuario usuarioAutenticado;
        private readonly Evento eventoSelecionado;
        private readonly SetorService setorService = new SetorService();
        private readonly EquipeEventoService equipeService = new EquipeEventoService();
        private readonly EscalaService escalaService = new EscalaService();
        private readonly JornadaSetorService jornadaService = new JornadaSetorService();
        private readonly TurnoRepository turnoRepository = new TurnoRepository();
        private bool podeAdministrarEvento;

        public EquipeSetoresControl() : this(null, null)
        {
        }

        public EquipeSetoresControl(QuermessePreview exemplos) : this(null, null)
        {
        }

        public EquipeSetoresControl(Usuario usuario, Evento evento)
        {
            InitializeComponent();
            FrontendTheme.Aplicar(this);
            // O painel superior não pode crescer sobre a seção de setores ao redimensionar.
            panelEquipe.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelSetores.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblSetoresbarracas.Width = 250;
            txtNovoSetor.MaxLength = 100;
            usuarioAutenticado = usuario;
            eventoSelecionado = evento;

            if (usuarioAutenticado == null)
            {
                labelAviso.Text = "Entre no sistema para consultar a equipe do evento.";
                btnAdicionarSetor.Enabled = false;
                btnNovoFuncionario.Enabled = false;
                btnEditarFuncionario.Enabled = false;
                return;
            }

            if (eventoSelecionado == null)
            {
                labelAviso.Text = "Selecione um evento na tela Eventos para consultar seus setores.";
                btnAdicionarSetor.Enabled = false;
                btnNovoFuncionario.Enabled = false;
                btnEditarFuncionario.Enabled = false;
                return;
            }

            try
            {
                string nomePerfil;
                new AutorizacaoService().ValidarUsuarioEObterPerfil(
                    usuarioAutenticado,
                    out nomePerfil);
                podeAdministrarEvento = string.Equals(
                    nomePerfil,
                    "Gerente",
                    StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception)
            {
                podeAdministrarEvento = false;
            }

            btnAdicionarSetor.Enabled = podeAdministrarEvento;
            btnNovoFuncionario.Enabled = podeAdministrarEvento;
            btnEditarFuncionario.Enabled = podeAdministrarEvento;

            CarregarSetores();
            CarregarEquipe();
            dgvSetores.CellDoubleClick += ConfigurarProdutoDoSetor;
            ContextMenuStrip menuSetor = new ContextMenuStrip();
            ToolStripMenuItem configurarProduto = new ToolStripMenuItem("Produtos e preços do setor selecionado");
            configurarProduto.Click += (s, e) => ConfigurarProdutoDoSetor(s, null);
            configurarProduto.Enabled = podeAdministrarEvento;
            menuSetor.Items.Add(configurarProduto);
            dgvSetores.ContextMenuStrip = menuSetor;
            Disposed += (s, e) => menuSetor.Dispose();

            if (!podeAdministrarEvento)
            {
                labelAviso.Text = "Equipe em modo de consulta para este perfil.";
            }
        }

        private void btnNovoFuncionario_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                AbrirMembroDemo(false);
                return;
            }

            AbrirFormularioMembro(null);
        }

        private void btnEditarFuncionario_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                AbrirMembroDemo(true);
                return;
            }

            EquipeEvento vinculo = ObterVinculoSelecionado();

            if (vinculo != null)
            {
                AbrirFormularioMembro(vinculo);
            }
        }

        private void dgvEquipe_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dadosDemo != null)
            {
                AbrirMembroDemo(true);
                return;
            }

            if (e.RowIndex < 0)
            {
                return;
            }

            EquipeEvento vinculo = dgvEquipe.Rows[e.RowIndex].Tag as EquipeEvento;

            if (vinculo != null)
            {
                AbrirFormularioMembro(vinculo);
            }
        }

        private EquipeEvento ObterVinculoSelecionado()
        {
            if (dgvEquipe.SelectedRows.Count == 0)
            {
                labelAviso.Text = "Selecione um membro da equipe.";
                return null;
            }

            return dgvEquipe.SelectedRows[0].Tag as EquipeEvento;
        }

        private void AbrirFormularioMembro(EquipeEvento vinculo)
        {
            using (PessoaSetorForm formulario = new PessoaSetorForm(
                usuarioAutenticado,
                eventoSelecionado,
                vinculo))
            {
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    CarregarEquipe();
                    labelAviso.Text = "Equipe atualizada.";
                }
            }
        }

        private void btnAdicionarSetor_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                AdicionarSetorDemo();
                return;
            }

            if (eventoSelecionado == null || usuarioAutenticado == null)
            {
                labelAviso.Text = "Selecione um evento e autentique-se antes de criar um setor.";
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNovoSetor.Text))
            {
                labelAviso.Text = "Informe o nome do setor.";
                txtNovoSetor.Focus();
                return;
            }

            Setor setor = new Setor();
            setor.IdEvento = eventoSelecionado.IdEvento;
            setor.Nome = txtNovoSetor.Text.Trim();

            try
            {
                setorService.Criar(usuarioAutenticado, setor);
                txtNovoSetor.Clear();
                labelAviso.Text = "Setor cadastrado e local de estoque criado.";
                CarregarSetores();
            }
            catch (ArgumentException ex)
            {
                labelAviso.Text = ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                labelAviso.Text = ex.Message;
            }
            catch (UnauthorizedAccessException)
            {
                labelAviso.Text = "Sua conta não pode criar setores neste evento.";
            }
            catch (Exception)
            {
                labelAviso.Text = "Não foi possível cadastrar o setor.";
            }
        }

        private void CarregarSetores()
        {
            dgvSetores.Rows.Clear();

            try
            {
                List<Setor> setores = setorService.ListarPorEvento(
                    usuarioAutenticado,
                    eventoSelecionado.IdEvento);

                foreach (Setor setor in setores)
                {
                    int indiceLinha = dgvSetores.Rows.Add(setor.Nome, "—", "—");
                    dgvSetores.Rows[indiceLinha].Tag = setor;
                }

                dgvSetores.ClearSelection();
            }
            catch (UnauthorizedAccessException)
            {
                labelAviso.Text = "Sua conta não pode consultar os setores deste evento.";
            }
            catch (Exception)
            {
                labelAviso.Text = "Não foi possível carregar os setores do evento.";
            }
        }

        private void ConfigurarProdutoDoSetor(object sender, DataGridViewCellEventArgs e)
        {
            if (!podeAdministrarEvento || dgvSetores.SelectedRows.Count == 0 ||
                (e != null && e.RowIndex < 0))
            {
                return;
            }
            Setor setor = dgvSetores.SelectedRows[0].Tag as Setor;
            if (setor == null)
            {
                return;
            }
            try
            {
                ProdutoService produtos = new ProdutoService();
                var configuracoes = produtos.ListarConfiguracoesDoSetor(usuarioAutenticado, setor.IdSetor);
                using (Form dialogo = new Form())
                using (ComboBox produto = new ComboBox())
                using (NumericUpDown preco = new NumericUpDown())
                using (Button salvar = new Button())
                {
                    dialogo.Text = "Produtos e preços — " + setor.Nome;
                    dialogo.ClientSize = new System.Drawing.Size(480, 190);
                    dialogo.StartPosition = FormStartPosition.CenterParent;
                    dialogo.FormBorderStyle = FormBorderStyle.FixedDialog;
                    dialogo.MaximizeBox = false;
                    produto.DisplayMember = "Nome";
                    produto.DropDownStyle = ComboBoxStyle.DropDownList;
                    produto.SetBounds(20, 42, 440, 30);
                    foreach (Produto item in produtos.Listar(usuarioAutenticado))
                    {
                        if (item.Ativo)
                        {
                            produto.Items.Add(item);
                        }
                    }
                    preco.DecimalPlaces = 2;
                    preco.Maximum = 99999999.99M;
                    preco.SetBounds(20, 113, 180, 28);
                    produto.SelectedIndexChanged += (origem, argumentos) =>
                    {
                        preco.Value = 0;
                        Produto selecionado = produto.SelectedItem as Produto;
                        foreach (ProdutoSetor configuracao in configuracoes)
                        {
                            if (selecionado != null && configuracao.IdProduto == selecionado.IdProduto)
                            {
                                preco.Value = configuracao.PrecoVenda;
                                break;
                            }
                        }
                    };
                    salvar.Text = "Salvar preço";
                    salvar.SetBounds(280, 110, 180, 36);
                    salvar.Click += (origem, argumentos) =>
                    {
                        Produto selecionado = produto.SelectedItem as Produto;
                        if (selecionado == null)
                        {
                            return;
                        }
                        try
                        {
                            produtos.ConfigurarNoSetor(usuarioAutenticado, setor.IdSetor,
                                selecionado.IdProduto, preco.Value);
                            dialogo.DialogResult = DialogResult.OK;
                            dialogo.Close();
                        }
                        catch (Exception erro)
                        {
                            string mensagem = "Não foi possível configurar o produto.";
                            if (erro is ArgumentException || erro is InvalidOperationException || erro is UnauthorizedAccessException)
                            {
                                mensagem = erro.Message;
                            }
                            MessageBox.Show(dialogo, mensagem, "Produto por setor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    };
                    dialogo.Controls.Add(FrontendTheme.Legenda("Produto", new System.Drawing.Rectangle(20, 15, 400, 22)));
                    dialogo.Controls.Add(FrontendTheme.Legenda("Preço padrão de venda (R$)", new System.Drawing.Rectangle(20, 84, 400, 22)));
                    dialogo.Controls.Add(produto);
                    dialogo.Controls.Add(preco);
                    dialogo.Controls.Add(salvar);
                    FrontendTheme.Aplicar(dialogo);
                    if (produto.Items.Count > 0)
                    {
                        produto.SelectedIndex = 0;
                    }
                    if (dialogo.ShowDialog(this) == DialogResult.OK)
                    {
                        labelAviso.Text = "Produto vinculado ao setor e preço padrão salvo. Vendas anteriores foram preservadas.";
                    }
                }
            }
            catch (Exception erro)
            {
                labelAviso.Text = "Não foi possível carregar os produtos autorizados.";
                System.Diagnostics.Trace.WriteLine(erro.GetType().Name + " em ConfigurarProdutoDoSetor");
            }
        }

        private void CarregarEquipe()
        {
            dgvEquipe.Rows.Clear();

            try
            {
                List<EquipeEvento> equipe = equipeService.ListarPorEvento(
                    usuarioAutenticado,
                    eventoSelecionado.IdEvento);
                List<Perfil> perfis = equipeService.ListarPerfis(
                    usuarioAutenticado,
                    eventoSelecionado.IdEvento);

                foreach (EquipeEvento membro in equipe)
                {
                    Pessoa pessoa = equipeService.BuscarPessoaVinculada(
                        usuarioAutenticado,
                        eventoSelecionado.IdEvento,
                        membro.IdEquipe);
                    string nomePessoa = "Pessoa " + membro.IdPessoa;
                    string login = string.Empty;

                    if (pessoa != null)
                    {
                        nomePessoa = pessoa.Nome;
                        login = equipeService.BuscarLoginDaPessoaVinculada(
                            usuarioAutenticado,
                            eventoSelecionado.IdEvento,
                            membro.IdEquipe);
                    }

                    string nomePerfil = "—";

                    foreach (Perfil perfil in perfis)
                    {
                        if (perfil.IdPerfil == membro.IdPerfil)
                        {
                            nomePerfil = perfil.Nome;
                            break;
                        }
                    }

                    string resumoSetores;
                    string resumoTurnos;
                    MontarResumoEscala(membro, out resumoSetores, out resumoTurnos);
                    string situacao = "Inativo";

                    if (membro.Ativo)
                    {
                        situacao = "Ativo";
                    }

                    int indiceLinha = dgvEquipe.Rows.Add(
                        nomePessoa,
                        login,
                        nomePerfil,
                        resumoSetores,
                        resumoTurnos,
                        situacao);
                    dgvEquipe.Rows[indiceLinha].Tag = membro;
                }

                dgvEquipe.ClearSelection();
            }
            catch (UnauthorizedAccessException)
            {
                labelAviso.Text = "Sua conta não pode consultar a equipe deste evento.";
            }
            catch (Exception)
            {
                labelAviso.Text = "Não foi possível carregar a equipe do evento.";
            }
        }

        private void MontarResumoEscala(
            EquipeEvento membro,
            out string resumoSetores,
            out string resumoTurnos)
        {
            List<Escala> escalas = escalaService.ListarPorEquipe(
                usuarioAutenticado,
                membro.IdEquipe);
            List<string> nomesSetores = new List<string>();
            List<string> nomesTurnos = new List<string>();

            if (escalas.Count == 0)
            {
                resumoSetores = "Sem escala";
                resumoTurnos = "Sem escala";
                return;
            }

            foreach (Escala escala in escalas)
            {
                JornadaSetor jornada = jornadaService.BuscarPorId(
                    usuarioAutenticado,
                    escala.IdJornada);

                if (jornada == null)
                {
                    continue;
                }

                string nomeSetor = "Setor " + jornada.IdSetor;
                List<Setor> setores = setorService.ListarPorEvento(
                    usuarioAutenticado,
                    eventoSelecionado.IdEvento);

                foreach (Setor setor in setores)
                {
                    if (setor.IdSetor == jornada.IdSetor)
                    {
                        nomeSetor = setor.Nome;
                        break;
                    }
                }

                string nomeTurno = "Turno " + jornada.IdTurno;
                List<Turno> turnos = turnoRepository.Listar();

                foreach (Turno turno in turnos)
                {
                    if (turno.IdTurno == jornada.IdTurno)
                    {
                        nomeTurno = turno.Nome;
                        break;
                    }
                }

                AdicionarSeAusente(nomesSetores, nomeSetor);
                AdicionarSeAusente(nomesTurnos, nomeTurno);
            }

            resumoSetores = string.Join(", ", nomesSetores);
            resumoTurnos = string.Join(", ", nomesTurnos);

            if (string.IsNullOrWhiteSpace(resumoSetores))
            {
                resumoSetores = "Escala cadastrada";
            }

            if (string.IsNullOrWhiteSpace(resumoTurnos))
            {
                resumoTurnos = "Escala cadastrada";
            }
        }

        private static void AdicionarSeAusente(List<string> valores, string valor)
        {
            if (!valores.Contains(valor))
            {
                valores.Add(valor);
            }
        }
    }
}
