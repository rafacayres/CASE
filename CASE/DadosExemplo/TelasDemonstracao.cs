using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CASE.Models;
using CASE.Utils;
using CASE.Quermesse;

namespace CASE
{
    public static class TelasDemonstracao
    {
        public static Form CriarFormulario(string tipo, DadosDemonstracao dados)
        {
            Form formulario;
            switch (tipo)
            {
                case "Cadastro":
                    var cadastro = new CadastroForm();
                    cadastro.AtivarDemonstracao(dados);
                    formulario = cadastro;
                    break;
                case "Evento":
                    var evento = new QuermesseForm();
                    evento.AtivarDemonstracao(dados, null);
                    formulario = evento;
                    break;
                case "Produto":
                    var produto = new ProdutoForm();
                    produto.AtivarDemonstracao(dados, null);
                    formulario = produto;
                    break;
                case "Funcionário":
                    var pessoa = new PessoaSetorForm();
                    pessoa.AtivarDemonstracao(dados, null);
                    formulario = pessoa;
                    break;
                case "Escala":
                    var escala = new EscalaForm();
                    escala.AtivarDemonstracao(dados, null);
                    formulario = escala;
                    break;
                case "Solicitação":
                    var solicitacao = new SolicitacaoForm();
                    solicitacao.AtivarDemonstracao(dados, null);
                    formulario = solicitacao;
                    break;
                case "Entrada":
                    var entrada = new EntradaMercadoriaForm();
                    entrada.AtivarDemonstracao(dados);
                    formulario = entrada;
                    break;
                case "Transferência":
                    var transferencia = new TransferenciaEstoqueForm();
                    transferencia.AtivarDemonstracao(dados);
                    formulario = transferencia;
                    break;
                default:
                    throw new ArgumentException("Formulário demonstrativo não encontrado.");
            }

            formulario.Text += " · demonstração";
            return formulario;
        }

        public static UserControl Criar(string menu, DadosDemonstracao dados)
        {
            switch (menu)
            {
                case "buttonDashboard":
                    var dashboard = new DashboardControl();
                    dashboard.AtivarDemonstracao(dados);
                    return dashboard;
                case "buttonProdutos":
                    var produtos = new ProdutosControl();
                    produtos.AtivarDemonstracao(dados);
                    return produtos;
                case "buttonFornecedores":
                    var fornecedores = new FornecedoresControl();
                    fornecedores.AtivarDemonstracao(dados);
                    return fornecedores;
                case "buttonQuermesses":
                    var eventos = new QuermessesControl();
                    eventos.AtivarDemonstracao(dados);
                    return eventos;
                case "buttonEquipeSetores":
                    var equipe = new EquipeSetoresControl();
                    equipe.AtivarDemonstracao(dados);
                    return equipe;
                case "buttonEscala":
                    var escala = new EscalaControl();
                    escala.AtivarDemonstracao(dados);
                    return escala;
                case "buttonEstoque":
                    var estoque = new OperacoesEstoqueControl();
                    estoque.AtivarDemonstracao(dados);
                    return estoque;
                case "buttonHistorico":
                    var historico = new MovimentacoesControl();
                    historico.AtivarDemonstracao(dados);
                    return historico;
                case "buttonMovimentacoes":
                    var registros = new RegistrosControl();
                    registros.AtivarDemonstracao(dados);
                    return registros;
                case "buttonSolicitacoes":
                    var solicitacoes = new SolicitacoesControl();
                    solicitacoes.AtivarDemonstracao(dados);
                    return solicitacoes;
                case "buttonRelatorios":
                    var relatorios = new RelatoriosControl();
                    relatorios.AtivarDemonstracao(dados);
                    return relatorios;
                case "buttonFinanceiro":
                    var financeiro = new FinanceiroControl();
                    financeiro.AtivarDemonstracao(dados);
                    return financeiro;
                case "buttonAcessos":
                    var acessos = new AcessosControl();
                    acessos.AtivarDemonstracao(dados);
                    return acessos;
                case "buttonConfiguracoes":
                    var configuracoes = new ConfiguracoesControl();
                    configuracoes.AtivarDemonstracao(dados);
                    return configuracoes;
                default:
                    throw new InvalidOperationException("Esta tela ainda não possui uma apresentação demonstrativa.");
            }
        }

        internal static void Abrir(Form formulario, Control origem)
        {
            using (formulario)
            {
                formulario.ShowDialog(origem);
            }
        }

        internal static void Erro(Control origem, Exception erro)
        {
            MessageBox.Show(origem, erro.Message, "Demonstração · somente memória", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public partial class DashboardControl
    {
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            lblSubtituloDashboard.Text = DadosDemonstracao.Aviso;
            lblTotalProdutosValor.Text = dados.Produtos.Count.ToString();
            decimal entradas = 0;
            decimal saidas = 0;
            quantidadesPorDia.Clear();
            foreach (var movimento in dados.Movimentos)
            {
                if (movimento.Tipo == "entrada")
                {
                    entradas += movimento.Quantidade;
                }

                if (movimento.Tipo == "venda" || movimento.Tipo == "perda")
                {
                    saidas -= movimento.Quantidade;
                }

                DateTime dia = movimento.Data.Date;
                if (!quantidadesPorDia.ContainsKey(dia))
                {
                    quantidadesPorDia.Add(dia, 0);
                }

                quantidadesPorDia[dia] += Math.Abs(movimento.Quantidade);
            }

            lblEntradasMesValor.Text = entradas.ToString("N0");
            lblSaidasMesValor.Text = saidas.ToString("N0");
            int baixos = 0;
            foreach (DataRow saldo in dados.TabelaEstoques().Rows)
            {
                if (Convert.ToDecimal(saldo["Quantidade"]) < Convert.ToDecimal(saldo["Mínimo"]))
                {
                    baixos++;
                }
            }

            lblEstoqueBaixoValor.Text = baixos.ToString();
            lblTituloGrafico.Text = "Unidades movimentadas por dia";
            lblTituloMovimentacoes.Text = "Atividade recente";
            panelGraficoMovimentacoes.Paint += PintarMovimentosReais;
            panelGraficoMovimentacoes.Resize += (sender, argumentos) => panelGraficoMovimentacoes.Invalidate();
            ListBox lista = new ListBox();
            lista.BorderStyle = BorderStyle.None;
            lista.Font = new Font("Segoe UI", 9F);
            lista.SetBounds(16, 60, panelMovimentacoesRecentes.Width - 32, panelMovimentacoesRecentes.Height - 80);
            lista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lista.HorizontalScrollbar = true;
            for (int i = dados.Equipe.Registros.Count - 1; i >= 0; i--)
            {
                RegistroPreview registro = dados.Equipe.Registros[i];
                lista.Items.Add(registro.Tipo + " · " + registro.Quantidade + " " + registro.Produto + " · " + registro.Setor);
            }

            panelMovimentacoesRecentes.Controls.Add(lista);
        }

    }

    public partial class ProdutosControl
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            produtos = dados.Produtos;
            categorias = dados.Categorias;
            btnNovoProduto.Enabled = true;
            cmbFiltroCategoria.Items.Clear();
            cmbFiltroCategoria.Items.Add("Todas as categorias");
            foreach (Categoria categoria in categorias)
            {
                cmbFiltroCategoria.Items.Add(categoria.Nome);
            }

            cmbFiltroCategoria.SelectedIndex = 0;
            ExibirProdutos();
        }

        private void AbrirProdutoDemo(Produto produto)
        {
            using (ProdutoForm formulario = new ProdutoForm())
            {
                formulario.AtivarDemonstracao(dadosDemo, produto);
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    AtivarDemonstracao(dadosDemo);
                }
            }
        }
    }

    public partial class ProdutoForm
    {
        private DadosDemonstracao dadosDemo;
        private Produto produtoDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados, Produto produto)
        {
            dadosDemo = dados;
            produtoDemo = produto;
            cmbCategoriaProduto.Items.Clear();
            cmbCategoriaProduto.DisplayMember = "Nome";
            foreach (Categoria categoria in dados.Categorias)
            {
                cmbCategoriaProduto.Items.Add(categoria);
            }

            cmbCategoriaProduto.SelectedIndex = 0;
            btnSalvarProduto.Enabled = true;
            lblSubtituloProduto.Text = DadosDemonstracao.Aviso;
            if (produto != null)
            {
                PreencherProduto(produto);
            }
        }

        private void SalvarProdutoDemo()
        {
            if (string.IsNullOrWhiteSpace(txtNomeProduto.Text))
            {
                lblSubtituloProduto.Text = "Informe o nome · somente memória de demonstração";
                return;
            }

            if (produtoDemo == null)
            {
                produtoDemo = new Produto
                {
                    IdProduto = dadosDemo.Produtos.Count + 1,
                    Ativo = true
                };
                dadosDemo.Produtos.Add(produtoDemo);
            }

            dadosDemo.RenomearProduto(produtoDemo.IdProduto, txtNomeProduto.Text.Trim());
            produtoDemo.Descricao = txtDescricaoProduto.Text;
            produtoDemo.IdCategoria = ((Categoria)cmbCategoriaProduto.SelectedItem).IdCategoria;
            produtoDemo.UnidadeMedida = "UN";
            produtoDemo.Imagem = imagemProduto;
            dadosDemo.NotificarAlteracao();
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public partial class FornecedoresControl
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            fornecedores = dados.Fornecedores;
            btnNovo.Enabled = true;
            btnSalvar.Enabled = true;
            AtualizarLista();
            lblMensagem.Text = DadosDemonstracao.Aviso;
        }

        private void SalvarFornecedorDemo()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                lblMensagem.Text = "Informe o nome do fornecedor ilustrativo.";
                return;
            }

            Fornecedor item = fornecedorSelecionado;
            if (item == null)
            {
                item = new Fornecedor
                {
                    IdFornecedor = dadosDemo.Fornecedores.Count + 1
                };
                dadosDemo.Fornecedores.Add(item);
            }

            item.Nome = txtNome.Text.Trim();
            item.Telefone = txtTelefone.Text;
            item.Documento = txtDocumento.Text;
            item.Email = txtEmail.Text;
            AtualizarLista();
            LimparCampos();
            lblMensagem.Text = "Fornecedor salvo somente na memória de demonstração.";
            dadosDemo.NotificarAlteracao();
        }
    }
}

namespace CASE.Quermesse
{
    public partial class QuermessesControl
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            eventosCarregados = dados.Eventos;
            btnNovaQuermesse.Enabled = true;
            dgvQuermesses.Rows.Clear();
            foreach (Evento evento in dados.Eventos)
            {
                string tipo = dados.TiposEvento[0].Nome;
                foreach (TipoEvento opcao in dados.TiposEvento)
                {
                    if (opcao.IdTipoEvento == evento.IdTipoEvento)
                    {
                        tipo = opcao.Nome;
                    }
                }

                int indice = dgvQuermesses.Rows.Add(evento.Nome, tipo, evento.Local, evento.DataInicio.ToString("dd/MM/yyyy"), evento.DataFim.ToString("dd/MM/yyyy"), "Marina Alves", evento.Situacao);
                dgvQuermesses.Rows[indice].Tag = evento;
            }

            lblQuermessedeSaoJoao2026.Text = dados.Eventos[0].Nome;
            lblPatiodacomunidadeEmandamento.Text = "Pátio da comunidade · demonstração";
            labelAviso.Text = DadosDemonstracao.Aviso;
            if (dgvQuermesses.Rows.Count > 0)
            {
                dgvQuermesses.CurrentCell = dgvQuermesses.Rows[0].Cells[0];
            }
        }

        private void AbrirEventoDemo(Evento evento)
        {
            using (QuermesseForm formulario = new QuermesseForm())
            {
                formulario.AtivarDemonstracao(dadosDemo, evento);
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    AtivarDemonstracao(dadosDemo);
                }
            }
        }

        private void AlterarEventoDemo(string situacao)
        {
            Evento evento = ObterEventoSelecionado();
            if (evento == null)
            {
                return;
            }

            evento.Situacao = situacao;
            AtivarDemonstracao(dadosDemo);
            labelAviso.Text = "Situação alterada somente na memória de demonstração.";
        }
    }

    public partial class QuermesseForm
    {
        private DadosDemonstracao dadosDemo;
        private Evento eventoDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados, Evento evento)
        {
            dadosDemo = dados;
            eventoDemo = evento;
            cmbTipoEvento.Items.Clear();
            foreach (TipoEvento tipo in dados.TiposEvento)
            {
                cmbTipoEvento.Items.Add(tipo);
            }

            cmbTipoEvento.SelectedIndex = 0;
            cmbSituacaoQuermesse.Items.Clear();
            string situacao = "planejado";
            if (evento != null)
            {
                situacao = evento.Situacao;
            }

            cmbSituacaoQuermesse.Items.Add(situacao);
            cmbSituacaoQuermesse.SelectedIndex = 0;
            txtResponsavelQuermesse.Text = "Marina Alves · responsável ilustrativa";
            dtpInicioQuermesse.Value = DateTime.Today;
            dtpFimQuermesse.Value = DateTime.Today.AddDays(2);
            if (evento != null)
            {
                txtNomeQuermesse.Text = evento.Nome;
                txtLocalQuermesse.Text = evento.Local;
                txtObservacaoQuermesse.Text = evento.Observacao;
                dtpInicioQuermesse.Value = evento.DataInicio;
                dtpFimQuermesse.Value = evento.DataFim;
            }

            btnSalvarVisual.Enabled = true;
            labelAvisoFormulario.Text = DadosDemonstracao.Aviso;
            labelAvisoFormulario.ForeColor = VisualUtils.TextoSuave;
        }

        private void SalvarEventoDemo()
        {
            if (string.IsNullOrWhiteSpace(txtNomeQuermesse.Text) || string.IsNullOrWhiteSpace(txtLocalQuermesse.Text) || dtpFimQuermesse.Value.Date < dtpInicioQuermesse.Value.Date)
            {
                ExibirAviso("Informe nome, local e datas válidas · demonstração.", true);
                return;
            }

            if (eventoDemo == null)
            {
                eventoDemo = new Evento
                {
                    IdEvento = dadosDemo.Eventos.Count + 1,
                    Situacao = "planejado"
                };
                dadosDemo.Eventos.Add(eventoDemo);
                dadosDemo.Locais.Add(new LocalEstoque { IdLocalEstoque = dadosDemo.Locais.Count + 1,
                    IdEvento = eventoDemo.IdEvento, IdInstituicao = 1, Nome = "Geral · " + txtNomeQuermesse.Text.Trim(),
                    Ativo = true, EhGeralEvento = true });
            }

            eventoDemo.Nome = txtNomeQuermesse.Text.Trim();
            eventoDemo.Local = txtLocalQuermesse.Text.Trim();
            eventoDemo.IdTipoEvento = ((TipoEvento)cmbTipoEvento.SelectedItem).IdTipoEvento;
            eventoDemo.DataInicio = dtpInicioQuermesse.Value.Date;
            eventoDemo.DataFim = dtpFimQuermesse.Value.Date;
            eventoDemo.Observacao = txtObservacaoQuermesse.Text;
            dadosDemo.NotificarAlteracao();
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public partial class EquipeSetoresControl
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            btnAdicionarSetor.Enabled = true;
            btnNovoFuncionario.Enabled = true;
            btnEditarFuncionario.Enabled = true;
            dgvEquipe.Rows.Clear();
            dgvSetores.Rows.Clear();
            lblSubtitulo.Text = dados.Eventos[0].Nome + " · funções e escalas ilustrativas";
            colEquipePerfil.HeaderText = "Função";
            panelCorpo.Height = Math.Max(590, Height - 170);
            panelSetores.Height = Math.Max(230, panelCorpo.Height - panelSetores.Top);
            labelAviso.Top = panelCorpo.Bottom + 12;
            AutoScrollMinSize = new Size(970, labelAviso.Bottom + 16);
            foreach (PessoaPreview pessoa in dados.Equipe.Pessoas)
            {
                string funcao = "Atendente";
                string situacao = "Ativo";
                if (pessoa.Perfil == PerfilUsuario.Gerente) { funcao = "Coordenação"; }
                if (!pessoa.Ativo) { situacao = "Inativo"; }
                int indice = dgvEquipe.Rows.Add(pessoa.Nome, pessoa.Login, funcao, pessoa.Setor, pessoa.Turno, situacao);
                dgvEquipe.Rows[indice].Tag = pessoa;
            }

            foreach (Setor setor in dados.Setores)
            {
                int membros = 0;
                foreach (PessoaPreview pessoa in dados.Equipe.Pessoas)
                {
                    if (pessoa.Setor == setor.Nome)
                    {
                        membros++;
                    }
                }

                int registros = 0;
                foreach (RegistroPreview registro in dados.Equipe.Registros) { if (registro.Setor == setor.Nome) { registros++; } }
                int indice = dgvSetores.Rows.Add(setor.Nome, membros, registros);
                dgvSetores.Rows[indice].Tag = setor;
            }

            labelAviso.Text = DadosDemonstracao.Aviso;
        }

        private void AbrirMembroDemo(bool editar)
        {
            PessoaPreview pessoa = null;
            if (editar && dgvEquipe.CurrentRow != null)
            {
                pessoa = dgvEquipe.CurrentRow.Tag as PessoaPreview;
            }

            using (PessoaSetorForm formulario = new PessoaSetorForm())
            {
                formulario.AtivarDemonstracao(dadosDemo, pessoa);
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    AtivarDemonstracao(dadosDemo);
                }
            }
        }

        private void AdicionarSetorDemo()
        {
            if (string.IsNullOrWhiteSpace(txtNovoSetor.Text))
            {
                labelAviso.Text = "Informe o nome do setor ilustrativo.";
                return;
            }

            string nome = txtNovoSetor.Text.Trim();
            dadosDemo.Setores.Add(new Setor { IdSetor = dadosDemo.Setores.Count + 1, IdEvento = 1, Nome = nome });
            dadosDemo.Equipe.Setores.Add(nome);
            dadosDemo.Locais.Add(new LocalEstoque { IdLocalEstoque = dadosDemo.Locais.Count + 1, IdEvento = 1, Nome = nome, Ativo = true });
            txtNovoSetor.Clear();
            AtivarDemonstracao(dadosDemo);
            labelAviso.Text = "Setor adicionado somente à memória de demonstração.";
        }
    }

    public partial class PessoaSetorForm
    {
        private DadosDemonstracao dadosDemo;
        private PessoaPreview pessoaDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados, PessoaPreview pessoa)
        {
            dadosDemo = dados;
            pessoaDemo = pessoa;
            txtNomeFuncionario.ReadOnly = false;
            cmbPerfilFuncionario.Items.Clear();
            cmbPerfilFuncionario.Items.AddRange(new object[] { "Atendente", "Coordenação" });
            cmbPerfilFuncionario.SelectedIndex = 0;
            cmbSetorFuncionario.Enabled = true;
            cmbTurnoFuncionario.Enabled = true;
            cmbSetorFuncionario.Items.Clear();
            cmbTurnoFuncionario.Items.Clear();
            foreach (Setor setor in dados.Setores)
            {
                cmbSetorFuncionario.Items.Add(setor.Nome);
            }

            cmbTurnoFuncionario.Items.AddRange(new object[] { "Tarde", "Noite" });
            cmbSetorFuncionario.SelectedIndex = 0;
            cmbTurnoFuncionario.SelectedIndex = 1;
            if (pessoa != null)
            {
                txtNomeFuncionario.Text = pessoa.Nome;
                txtLoginFuncionario.Text = pessoa.Login;
                cmbSetorFuncionario.SelectedItem = pessoa.Setor;
                cmbTurnoFuncionario.SelectedItem = pessoa.Turno;
                chkFuncionarioAtivo.Checked = pessoa.Ativo;
                if (pessoa.Perfil == PerfilUsuario.Gerente) { cmbPerfilFuncionario.SelectedIndex = 1; }
            }

            btnSalvarFuncionario.Enabled = true;
            labelAvisoFormulario.Text = DadosDemonstracao.Aviso;
        }

        private void SalvarMembroDemo()
        {
            if (string.IsNullOrWhiteSpace(txtNomeFuncionario.Text))
            {
                labelAvisoFormulario.Text = "Informe o nome ilustrativo.";
                return;
            }

            if (pessoaDemo == null)
            {
                pessoaDemo = new PessoaPreview
                {
                    Id = dadosDemo.Equipe.Pessoas.Count + 1,
                    Perfil = PerfilUsuario.Vendedor
                };
                dadosDemo.Equipe.Pessoas.Add(pessoaDemo);
            }

            pessoaDemo.Nome = txtNomeFuncionario.Text.Trim();
            pessoaDemo.Login = txtLoginFuncionario.Text;
            pessoaDemo.Setor = cmbSetorFuncionario.Text;
            pessoaDemo.Turno = cmbTurnoFuncionario.Text;
            pessoaDemo.Ativo = chkFuncionarioAtivo.Checked;
            pessoaDemo.Perfil = PerfilUsuario.Vendedor;
            if (cmbPerfilFuncionario.Text == "Coordenação") { pessoaDemo.Perfil = PerfilUsuario.Gerente; }
            dadosDemo.NotificarAlteracao();
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public partial class EscalaControl
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            btnNovaAlocacao.Enabled = true;
            dgvEscala.Rows.Clear();
            foreach (DataRow linha in dados.Escalas.Rows)
            {
                int indice = dgvEscala.Rows.Add(linha["Nome"], linha["Setor"], Convert.ToDateTime(linha["Data"]).ToString("dd/MM/yyyy"), linha["Turno"], linha["Entrada"], linha["Saída"], linha["Situação"]);
                dgvEscala.Rows[indice].Tag = linha;
            }

            PreencherFiltros();
            AplicarFiltros();
            labelAviso.Text = DadosDemonstracao.Aviso;
        }

        private void AbrirEscalaDemo(bool editar)
        {
            DataRow linha = null;
            if (editar && dgvEscala.CurrentRow != null)
            {
                linha = dgvEscala.CurrentRow.Tag as DataRow;
            }

            using (EscalaForm formulario = new EscalaForm())
            {
                formulario.AtivarDemonstracao(dadosDemo, linha);
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    AtivarDemonstracao(dadosDemo);
                }
            }
        }
    }

    public partial class EscalaForm
    {
        private DadosDemonstracao dadosDemo;
        private DataRow escalaDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados, DataRow linha)
        {
            dadosDemo = dados;
            escalaDemo = linha;
            cmbEventoEscala.Items.Add(dados.Eventos[0]);
            cmbEventoEscala.DisplayMember = "Nome";
            cmbEventoEscala.SelectedIndex = 0;
            cmbSetorEscala.DisplayMember = "Nome";
            cmbPessoaEscala.DisplayMember = "Nome";
            foreach (Setor setor in dados.Setores)
            {
                cmbSetorEscala.Items.Add(setor);
            }

            foreach (PessoaPreview pessoa in dados.Equipe.Pessoas)
            {
                cmbPessoaEscala.Items.Add(pessoa);
            }

            cmbTurnoEscala.Items.AddRange(new object[] { "Tarde", "Noite" });
            cmbSetorEscala.SelectedIndex = 0;
            cmbPessoaEscala.SelectedIndex = 1;
            cmbTurnoEscala.SelectedIndex = 1;
            dtpDiaEscala.Value = DateTime.Today;
            if (linha != null)
            {
                foreach (PessoaPreview pessoa in cmbPessoaEscala.Items)
                {
                    if (pessoa.Id == Convert.ToInt32(linha["PessoaId"]))
                    {
                        cmbPessoaEscala.SelectedItem = pessoa;
                    }
                }

                foreach (Setor setor in cmbSetorEscala.Items)
                {
                    if (setor.Nome == Convert.ToString(linha["Setor"]))
                    {
                        cmbSetorEscala.SelectedItem = setor;
                    }
                }

                cmbTurnoEscala.SelectedItem = linha["Turno"];
                dtpDiaEscala.Value = Convert.ToDateTime(linha["Data"]);
                dtpHoraInicioEscala.Value = DateTime.Today.Add(TimeSpan.Parse(Convert.ToString(linha["Entrada"])));
                dtpHoraFimEscala.Value = DateTime.Today.Add(TimeSpan.Parse(Convert.ToString(linha["Saída"])));
            }

            btnSalvarVisual.Enabled = true;
            labelAvisoFormulario.Text = DadosDemonstracao.Aviso;
        }

        private void SalvarEscalaDemo()
        {
            if (dtpHoraFimEscala.Value.TimeOfDay <= dtpHoraInicioEscala.Value.TimeOfDay)
            {
                labelAvisoFormulario.Text = "A saída deve ser posterior à entrada.";
                return;
            }

            PessoaPreview pessoa = cmbPessoaEscala.SelectedItem as PessoaPreview;
            Setor setor = cmbSetorEscala.SelectedItem as Setor;
            if (pessoa == null || setor == null)
            {
                return;
            }
            foreach (DataRow outra in dadosDemo.Escalas.Rows)
            {
                if (outra == escalaDemo || Convert.ToInt32(outra["PessoaId"]) != pessoa.Id ||
                    Convert.ToDateTime(outra["Data"]).Date != dtpDiaEscala.Value.Date) { continue; }
                TimeSpan entrada = TimeSpan.Parse(Convert.ToString(outra["Entrada"]));
                TimeSpan saida = TimeSpan.Parse(Convert.ToString(outra["Saída"]));
                if (dtpHoraInicioEscala.Value.TimeOfDay < saida && dtpHoraFimEscala.Value.TimeOfDay > entrada)
                {
                    labelAvisoFormulario.Text = "Já existe uma alocação ilustrativa para esse membro neste horário.";
                    return;
                }
            }

            if (escalaDemo == null)
            {
                escalaDemo = dadosDemo.Escalas.NewRow();
                dadosDemo.Escalas.Rows.Add(escalaDemo);
            }

            escalaDemo["Nome"] = pessoa.Nome;
            escalaDemo["PessoaId"] = pessoa.Id;
            escalaDemo["Setor"] = setor.Nome;
            escalaDemo["Data"] = dtpDiaEscala.Value.Date;
            escalaDemo["Turno"] = cmbTurnoEscala.Text;
            escalaDemo["Entrada"] = dtpHoraInicioEscala.Value.ToString("HH:mm");
            escalaDemo["Saída"] = dtpHoraFimEscala.Value.ToString("HH:mm");
            escalaDemo["Situação"] = "confirmada";
            dadosDemo.NotificarAlteracao();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
