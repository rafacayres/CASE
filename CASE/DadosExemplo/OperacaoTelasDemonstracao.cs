using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CASE.Models;
using CASE.Utils;
using CASE.Quermesse;

namespace CASE
{
    public partial class OperacoesEstoqueControl
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            cmbLocal.Items.Clear();
            foreach (LocalEstoque local in dados.Locais)
            {
                cmbLocal.Items.Add(local);
            }

            cmbLocal.SelectedIndex = 0;
            btnEntrada.Enabled = true;
            btnTransferir.Enabled = true;
            btnFornecedores.Enabled = true;
            lblMensagem.Text = DadosDemonstracao.Aviso;
            AtualizarEstoqueDemo();
        }

        private void AtualizarEstoqueDemo()
        {
            LocalEstoque local = cmbLocal.SelectedItem as LocalEstoque;
            if (local == null)
            {
                return;
            }

            dgvSaldos.Rows.Clear();
            dgvHistorico.Rows.Clear();
            int quantidade = 0;
            foreach (Produto produto in dadosDemo.Produtos)
            {
                decimal saldo = dadosDemo.Saldo(local.Nome, produto.Nome);
                if (saldo > 0 || dadosDemo.TemMovimentacao(local.Nome, produto.Nome))
                {
                    int indice = dgvSaldos.Rows.Add(produto.Nome, saldo.ToString("N0"), "UN");
                    quantidade++;
                    if (saldo < dadosDemo.Minimo(local.Nome, produto.Nome))
                    {
                        dgvSaldos.Rows[indice].DefaultCellStyle.ForeColor = Color.FromArgb(180, 60, 60);
                    }

                    dgvSaldos.Rows[indice].Cells[0].ToolTipText = "Mínimo ilustrativo: " + dadosDemo.Minimo(local.Nome, produto.Nome) + " UN";
                }
            }

            foreach (var movimento in dadosDemo.Movimentos)
            {
                if (movimento.Local == local.Nome)
                {
                    dgvHistorico.Rows.Add(movimento.Data.ToString("dd/MM HH:mm"), movimento.Produto, movimento.Tipo, movimento.Quantidade, movimento.Referencia);
                }
            }

            lblResumo.Text = quantidade + " produtos neste local · demonstração";
            lblMensagem.Text = DadosDemonstracao.Aviso;
        }

        private void AbrirEntradaDemo()
        {
            using (EntradaMercadoriaForm formulario = new EntradaMercadoriaForm())
            {
                formulario.AtivarDemonstracao(dadosDemo);
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    AtualizarEstoqueDemo();
                }
            }
        }

        private void AbrirTransferenciaDemo()
        {
            using (TransferenciaEstoqueForm formulario = new TransferenciaEstoqueForm())
            {
                formulario.AtivarDemonstracao(dadosDemo);
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    AtualizarEstoqueDemo();
                }
            }
        }
    }

    public partial class EntradaMercadoriaForm
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            cmbDestino.Items.Clear();
            cmbDestino.Items.Add(dados.Locais[0]);
            cmbDestino.SelectedIndex = 0;
            cmbFornecedor.Items.Clear();
            cmbFornecedor.Items.Add("Sem fornecedor");
            foreach (Fornecedor item in dados.Fornecedores)
            {
                cmbFornecedor.Items.Add(item);
            }

            cmbFornecedor.SelectedIndex = 0;
            produtos.Clear();
            cmbProduto.Items.Clear();
            foreach (Produto item in dados.Produtos)
            {
                produtos.Add(item);
                cmbProduto.Items.Add(item);
            }

            cmbProduto.SelectedIndex = 0;
            btnConfirmar.Enabled = true;
            lblMensagem.Text = DadosDemonstracao.Aviso;
        }

        private void ConfirmarEntradaDemo()
        {
            if (itens.Count == 0)
            {
                lblMensagem.Text = "Adicione pelo menos um item ilustrativo.";
                return;
            }

            foreach (EntradaItem item in itens)
            {
                Produto produto = produtos.Find(p => p.IdProduto == item.IdProduto);
                dadosDemo.AdicionarMovimento(produto.Nome, dadosDemo.Locais[0].Nome, "entrada", item.Quantidade, "Entrada simulada · somente memória", DateTime.Now);
            }

            dadosDemo.NotificarAlteracao();
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public partial class TransferenciaEstoqueForm
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            locaisEvento.Clear();
            cmbOrigem.Items.Clear();
            cmbProduto.Items.Clear();
            foreach (LocalEstoque local in dados.Locais)
            {
                if (!local.EhCentral)
                {
                    locaisEvento.Add(local);
                    cmbOrigem.Items.Add(local);
                }
            }

            foreach (Produto produto in dados.Produtos)
            {
                cmbProduto.Items.Add(produto);
            }

            cmbProduto.SelectedIndex = 0;
            cmbOrigem.SelectedIndex = 0;
            AtualizarDestinos();
            MostrarSaldoDemo();
            btnConfirmar.Enabled = true;
            lblMensagem.Text = DadosDemonstracao.Aviso;
        }

        private void MostrarSaldoDemo()
        {
            LocalEstoque local = cmbOrigem.SelectedItem as LocalEstoque;
            Produto produto = cmbProduto.SelectedItem as Produto;
            if (local != null && produto != null)
            {
                lblSaldo.Text = "Saldo ilustrativo na origem: " + dadosDemo.Saldo(local.Nome, produto.Nome).ToString("N0") + " UN";
            }
        }

        private void ConfirmarTransferenciaDemo()
        {
            try
            {
                LocalEstoque origem = cmbOrigem.SelectedItem as LocalEstoque;
                LocalEstoque destino = cmbDestino.SelectedItem as LocalEstoque;
                Produto produto = cmbProduto.SelectedItem as Produto;
                if (origem == null || destino == null || produto == null)
                {
                    throw new ArgumentException("Selecione origem, destino e produto.");
                }

                dadosDemo.Transferir(origem.Nome, destino.Nome, produto.Nome, numQuantidade.Value);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException erro)
            {
                lblMensagem.Text = erro.Message;
            }
        }
    }

    public partial class MovimentacoesControl
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            btnNovaMovimentacao.Enabled = true;
            cmbTipoMovimentacao.Items.Clear();
            cmbTipoMovimentacao.Items.AddRange(new object[] { "Todos", "entrada", "transferencia", "venda", "perda" });
            cmbTipoMovimentacao.SelectedIndex = 0;
            dtpDataInicial.Value = DateTime.Today.AddDays(-7);
            dtpDataFinal.Value = DateTime.Today;
            CarregarMovimentosDemo();
        }

        private void CarregarMovimentosDemo()
        {
            dgvMovimentacoes.Rows.Clear();
            foreach (DataRow linha in dadosDemo.TabelaMovimentos(dtpDataInicial.Value, dtpDataFinal.Value).Rows)
            {
                dgvMovimentacoes.Rows.Add(Convert.ToDateTime(linha["Data"]).ToString("dd/MM/yyyy"), linha["Produto"], linha["Tipo"], linha["Quantidade"], "Equipe ilustrativa", linha["Local"] + " · " + linha["Referência"]);
            }

            dgvMovimentacoesColMotivo.Visible = true;
            dgvMovimentacoesColMotivo.HeaderText = "Local / referência";
            lblListaMovimentacoes.Text = DadosDemonstracao.Aviso;
            FiltrarProduto();
        }
    }

    public partial class RelatoriosControl
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            btnExportarPdf.Enabled = true;
            dtpRelatorioInicio.Value = DateTime.Today.AddDays(-7);
            dtpRelatorioFim.Value = DateTime.Today;
            GerarRelatorioDemo();
        }

        private void GerarRelatorioDemo()
        {
            if (cmbRelatorioCategoria.SelectedIndex == 1)
            {
                dadosRelatorio = dadosDemo.TabelaEstoques();
            }
            else
            {
                dadosRelatorio = dadosDemo.TabelaMovimentos(dtpRelatorioInicio.Value, dtpRelatorioFim.Value);
            }

            dadosRelatorio.Columns.Add("Modo");
            foreach (DataRow linha in dadosRelatorio.Rows) { linha["Modo"] = "Demonstração · ilustrativo"; }
            gradeRelatorio.DataSource = dadosRelatorio;
            resumoRelatorio.Text = DadosDemonstracao.Aviso + Environment.NewLine + Environment.NewLine + dadosRelatorio.Rows.Count + " linhas ilustrativas. Os dados são os mesmos usados em Estoque e Dashboard.";
            btnExportarCsv.Enabled = dadosRelatorio.Rows.Count > 0;
        }
    }

    public partial class AcessosControl
    {
        private DadosDemonstracao dadosDemo;
        private bool botaoCadastroDemoConfigurado;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            dgvContas.Rows.Clear();
            foreach (var conta in dados.Contas)
            {
                int indice = dgvContas.Rows.Add(conta.Nome, conta.Login, conta.Email, conta.Situacao, conta.Perfil, conta.SolicitadoEm.ToString("dd/MM/yyyy"));
                dgvContas.Rows[indice].Tag = conta;
            }

            lblMensagem.Text = "Simulações de cadastro e análise somente em memória. Não criam acesso ao CASE real.";
            lblModoApresentacao.Visible = true;
            lblModoApresentacao.Text = "Modo demonstração";
            cmbSituacao.Enabled = true;
            txtBusca.Enabled = true;
            cmbPerfil.DisplayMember = string.Empty;
            cmbPerfil.Items.Clear();
            cmbPerfil.Items.AddRange(new object[] { "Administrador", "Gerente", "Membro" });
            btnPerfil.Text = "Simular cadastro";
            btnPerfil.Enabled = true;
            if (!botaoCadastroDemoConfigurado)
            {
                botaoCadastroDemoConfigurado = true;
                btnPerfil.Click += (s, e) =>
                {
                    using (Form formulario = TelasDemonstracao.CriarFormulario("Cadastro", dadosDemo))
                    {
                        if (formulario.ShowDialog(this) == DialogResult.OK)
                        {
                            AtivarDemonstracao(dadosDemo);
                        }
                    }
                };
            }

            SelecionarContaDemo();
        }

        private void SelecionarContaDemo()
        {
            OperacoesPreview.Conta conta = null;
            if (dgvContas.CurrentRow != null)
            {
                conta = dgvContas.CurrentRow.Tag as OperacoesPreview.Conta;
            }

            btnAprovar.Enabled = conta != null && conta.Situacao == "pendente";
            btnRejeitar.Enabled = btnAprovar.Enabled;
            if (conta != null)
            {
                lblSelecionado.Text = conta.Nome + " · " + conta.Situacao + " · demonstração";
                cmbPerfil.SelectedItem = conta.Perfil;
            }
        }

        private void DecidirContaDemo(bool aprovar)
        {
            OperacoesPreview.Conta conta = null;
            if (dgvContas.CurrentRow != null)
            {
                conta = dgvContas.CurrentRow.Tag as OperacoesPreview.Conta;
            }

            if (conta == null || conta.Situacao != "pendente")
            {
                return;
            }

            if (!aprovar && string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                lblMensagem.Text = "Informe um motivo para rejeitar o cadastro ilustrativo.";
                return;
            }

            if (aprovar)
            {
                if (cmbPerfil.SelectedIndex < 0)
                {
                    lblMensagem.Text = "Selecione um perfil ilustrativo.";
                    return;
                }

                conta.Situacao = "aprovado";
                conta.Perfil = cmbPerfil.Text;
            }
            else
            {
                conta.Situacao = "rejeitado";
                conta.Motivo = txtMotivo.Text;
            }

            dadosDemo.NotificarAlteracao();
            AtivarDemonstracao(dadosDemo);
        }
    }

    public partial class CadastroForm
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            btnCadastrar.Text = "Simular solicitação";
            lblSubtituloCadastro.Text = "Cadastro ilustrativo. Não cria usuário, senha ou acesso no sistema real.";
            ExibirMensagem(DadosDemonstracao.Aviso, false);
        }

        private void CadastrarDemo()
        {
            if (string.IsNullOrWhiteSpace(txtNomeCompleto.Text) || string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrEmpty(txtSenha.Text) || txtSenha.Text != txtConfirmarSenha.Text)
            {
                ExibirMensagem("Preencha nome, usuário e senhas coincidentes para a simulação.", true);
                return;
            }

            foreach (var conta in dadosDemo.Contas)
            {
                if (string.Equals(conta.Login, txtUsuario.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    ExibirMensagem("Já existe um usuário ilustrativo com esse nome.", true);
                    return;
                }
            }

            dadosDemo.Contas.Add(new OperacoesPreview.Conta { Nome = txtNomeCompleto.Text.Trim(), Login = txtUsuario.Text.Trim(), Email = txtEmail.Text.Trim(), Situacao = "pendente", Perfil = "Membro" });
            txtSenha.Clear();
            txtConfirmarSenha.Clear();
            dadosDemo.NotificarAlteracao();
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public partial class ConfiguracoesControl
    {
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            lblSubtituloConfiguracoes.Text = "Configurações ilustrativas · somente consulta";
            txtPerfilNome.Text = "Comunidade São Francisco · demonstração";
            txtPerfilUsuario.Text = "Identidade real preservada no cabeçalho";
            txtPerfilEmail.Text = "secretaria@example.invalid";
            Control[] listas = Controls.Find("listCategorias", true);
            if (listas.Length > 0)
            {
                ListBox lista = (ListBox)listas[0];
                lista.DisplayMember = "Nome";
                foreach (Categoria categoria in dados.Categorias)
                {
                    lista.Items.Add(categoria);
                }
            }

            foreach (Control controle in Controls)
            {
                FlowLayoutPanel menu = controle as FlowLayoutPanel;
                if (menu == null)
                {
                    continue;
                }

                foreach (Control item in menu.Controls)
                {
                    item.Visible = item.Name == "btnConfig0" || item.Name == "btnConfig2";
                }
            }

            DesabilitarGravacoesDemo(this);
        }

        private static void DesabilitarGravacoesDemo(Control raiz)
        {
            foreach (Control controle in raiz.Controls)
            {
                Button botao = controle as Button;
                if (botao != null && !botao.Name.StartsWith("btnConfig"))
                {
                    botao.Enabled = false;
                }

                DesabilitarGravacoesDemo(controle);
            }
        }
    }
}

namespace CASE.Quermesse
{
    public partial class RegistrosControl
    {
        private DadosDemonstracao dadosDemo;
        private bool preparandoDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            preparandoDemo = true;
            panelNovoRegistro.Visible = true;
            cmbSetorRegistro.DisplayMember = string.Empty;
            cmbSetorRegistro.Items.Clear();
            foreach (Setor setor in dados.Setores)
            {
                cmbSetorRegistro.Items.Add(setor.Nome);
            }

            cmbProdutoRegistro.DisplayMember = "Nome";
            btnRegistrar.Enabled = true;
            preparandoDemo = false;
            cmbSetorRegistro.SelectedIndex = 0;
            AtualizarRegistrosDemo();
            lblSubtitulo.Text = DadosDemonstracao.Aviso;
            labelAviso.Text = DadosDemonstracao.Aviso;
        }

        private void SelecionarSetorDemo()
        {
            if (preparandoDemo)
            {
                return;
            }

            cmbProdutoRegistro.Items.Clear();
            foreach (Produto produto in dadosDemo.Produtos)
            {
                bool permitido = false;
                foreach (ProdutoPreview opcao in dadosDemo.Equipe.Produtos)
                {
                    if (opcao.Nome == produto.Nome && opcao.Setor == cmbSetorRegistro.Text)
                    {
                        permitido = true;
                    }
                }

                if (permitido)
                {
                    cmbProdutoRegistro.Items.Add(produto);
                }
            }

            if (cmbProdutoRegistro.Items.Count > 0)
            {
                cmbProdutoRegistro.SelectedIndex = 0;
            }

            lblContextoRegistro.Text = "Registro ilustrativo · " + cmbSetorRegistro.Text + " · sem MySQL";
        }

        private void PrecoProdutoDemo()
        {
            Produto produto = cmbProdutoRegistro.SelectedItem as Produto;
            if (produto != null)
            {
                precoPraticado.Value = dadosDemo.Preco(produto.Nome);
            }
        }

        private void RegistrarDemo()
        {
            try
            {
                Produto produto = cmbProdutoRegistro.SelectedItem as Produto;
                if (produto == null)
                {
                    throw new ArgumentException("Selecione um produto ilustrativo.");
                }

                dadosDemo.Registrar(cmbSetorRegistro.Text, produto.Nome, cmbTipoRegistro.Text, (int)numQuantidadeRegistro.Value, txtObservacaoRegistro.Text, precoPraticado.Value, formaPagamento.Text);
                AtualizarRegistrosDemo();
                txtObservacaoRegistro.Clear();
                labelAviso.Text = "Registro simulado concluído somente na memória de demonstração.";
            }
            catch (ArgumentException erro)
            {
                labelAviso.Text = erro.Message;
            }
        }

        private void AtualizarRegistrosDemo()
        {
            dgvRegistrosEquipe.Rows.Clear();
            for (int i = dadosDemo.Equipe.Registros.Count - 1; i >= 0; i--)
            {
                RegistroPreview registro = dadosDemo.Equipe.Registros[i];
                dgvRegistrosEquipe.Rows.Add(registro.Data.ToString("dd/MM HH:mm"), registro.Pessoa, registro.Setor, registro.Turno, registro.Tipo, registro.Produto, registro.Quantidade, registro.Observacao, registro.PrecoUnitario.ToString("C2"), registro.FormaPagamento);
            }
        }
    }

    public partial class SolicitacoesControl
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            btnNovaSolicitacao.Enabled = true;
            btnDetalhesSolicitacao.Enabled = true;
            dgvSolicitacoesQuermesse.Rows.Clear();
            foreach (DataRow pedido in dados.Solicitacoes.Rows)
            {
                int indice = dgvSolicitacoesQuermesse.Rows.Add(DateTime.Today.ToString("dd/MM HH:mm"), pedido["solicitante"], pedido["setor"], pedido["tipo"], pedido["produto"], pedido["quantidade"], pedido["situacao"]);
                dgvSolicitacoesQuermesse.Rows[indice].Tag = pedido;
            }

            cmbFiltroSolicitacoes.Items.Clear();
            cmbFiltroSolicitacoes.Items.Add("Todos os setores");
            foreach (Setor setor in dados.Setores)
            {
                cmbFiltroSolicitacoes.Items.Add(setor.Nome);
            }

            cmbFiltroSolicitacoes.Items.Add("Geral do evento");
            cmbFiltroSolicitacoes.SelectedIndex = 0;
            cmbStatusListaSolicitacoes.Items.Clear();
            cmbStatusListaSolicitacoes.Items.AddRange(new object[] { "Todas", "pendente", "aprovada", "atendida", "rejeitada" });
            cmbStatusListaSolicitacoes.SelectedIndex = 0;
            labelAviso.Text = DadosDemonstracao.Aviso;
        }

        internal void AbrirPedidoDemo(int id)
        {
            DataRow pedido = null;
            foreach (DataRow linha in dadosDemo.Solicitacoes.Rows)
            {
                if (Convert.ToInt32(linha["id"]) == id)
                {
                    pedido = linha;
                }
            }

            using (SolicitacaoForm formulario = new SolicitacaoForm())
            {
                formulario.AtivarDemonstracao(dadosDemo, pedido);
                if (formulario.ShowDialog(this) == DialogResult.OK)
                {
                    AtivarDemonstracao(dadosDemo);
                }
            }
        }

        private void AbrirSolicitacaoDemoSelecionada()
        {
            DataRow linha = null;
            if (dgvSolicitacoesQuermesse.CurrentRow != null)
            {
                linha = dgvSolicitacoesQuermesse.CurrentRow.Tag as DataRow;
            }

            if (linha != null)
            {
                AbrirPedidoDemo(Convert.ToInt32(linha["id"]));
            }
        }
    }

    public partial class SolicitacaoForm
    {
        private DadosDemonstracao dadosDemo;
        private DataRow pedidoDemo;
        private void SelecionarProdutosSolicitacaoDemo()
        {
            comboProdutos.Items.Clear();
            foreach (Produto produto in dadosDemo.Produtos)
            {
                bool permitido = cmbTipoSolicitacao.Text != "reposicao";
                foreach (ProdutoPreview opcao in dadosDemo.Equipe.Produtos)
                {
                    if (opcao.Nome == produto.Nome && opcao.Setor == cmbSetorSolicitacao.Text)
                    {
                        permitido = true;
                    }
                }

                if (permitido)
                {
                    comboProdutos.Items.Add(produto);
                }
            }

            if (comboProdutos.Items.Count > 0)
            {
                comboProdutos.SelectedIndex = 0;
            }
        }

        internal void AtivarDemonstracao(DadosDemonstracao dados, DataRow pedido)
        {
            dadosDemo = dados;
            pedidoDemo = pedido;
            cmbSetorSolicitacao.DisplayMember = string.Empty;
            foreach (Setor setor in dados.Setores)
            {
                cmbSetorSolicitacao.Items.Add(setor.Nome);
            }

            cmbSetorSolicitacao.Items.Add("Geral do evento");
            cmbSetorSolicitacao.SelectedIndex = 0;
            comboProdutos.Visible = true;
            comboProdutos.Items.Clear();
            foreach (Produto produto in dados.Produtos)
            {
                comboProdutos.Items.Add(produto);
            }

            comboProdutos.SelectedIndex = 0;
            cmbTipoSolicitacao.Items.Clear();
            cmbTipoSolicitacao.Items.AddRange(new object[] { "reposicao", "liberacao", "devolucao" });
            cmbTipoSolicitacao.SelectedIndex = 0;
            cmbTipoSolicitacao.SelectedIndexChanged += (s, e) => SelecionarProdutosSolicitacaoDemo();
            txtSolicitante.Text = "João Santos";
            txtSolicitante.ReadOnly = true;
            txtQuantidadeSolicitacao.Text = "10";
            btnSalvarVisual.Enabled = true;
            labelAvisoFormulario.Text = DadosDemonstracao.Aviso;
            if (pedido != null)
            {
                txtSolicitante.Text = Convert.ToString(pedido["solicitante"]);
                cmbSetorSolicitacao.SelectedItem = pedido["setor"];
                cmbTipoSolicitacao.SelectedItem = pedido["tipo"];
                foreach (Produto produto in comboProdutos.Items)
                {
                    if (produto.Nome == Convert.ToString(pedido["produto"]))
                    {
                        comboProdutos.SelectedItem = produto;
                    }
                }

                txtQuantidadeSolicitacao.Text = Convert.ToString(pedido["quantidade"]);
                txtQuantidadeSolicitacao.ReadOnly = true;
                cmbSetorSolicitacao.Enabled = false;
                cmbTipoSolicitacao.Enabled = false;
                comboProdutos.Enabled = false;
                string estado = Convert.ToString(pedido["situacao"]);
                btnSalvarVisual.Text = "Aprovar";
                if (estado == "aprovada")
                {
                    btnSalvarVisual.Text = "Atender (simulação)";
                }

                if (estado != "pendente" && estado != "aprovada")
                {
                    btnSalvarVisual.Enabled = false;
                }

                lblObservacao.Text = "Motivo da rejeição (para rejeitar)";
                Button rejeitar = new Button();
                rejeitar.Text = "Rejeitar";
                rejeitar.SetBounds(btnSalvarVisual.Left - 130, btnSalvarVisual.Top, 120, btnSalvarVisual.Height);
                rejeitar.Enabled = estado == "pendente";
                rejeitar.Click += (s, e) =>
                {
                    try
                    {
                        dadosDemo.Decidir(Convert.ToInt32(pedido["id"]), false, txtObservacaoSolicitacao.Text);
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    catch (ArgumentException erro)
                    {
                        labelAvisoFormulario.Text = erro.Message;
                    }
                };
                panelFormulario.Controls.Add(rejeitar);
                VisualUtils.EstilizarBotao(rejeitar);
                labelAvisoFormulario.Text = "Pedido " + pedido["id"] + " · " + estado + " · " + DadosDemonstracao.Aviso;
            }
        }

        private void SalvarSolicitacaoDemo()
        {
            try
            {
                if (pedidoDemo != null)
                {
                    if (Convert.ToString(pedidoDemo["situacao"]) == "pendente")
                    {
                        dadosDemo.Decidir(Convert.ToInt32(pedidoDemo["id"]), true, string.Empty);
                    }
                    else
                    {
                        dadosDemo.Atender(Convert.ToInt32(pedidoDemo["id"]));
                    }
                }
                else
                {
                    int quantidade;
                    if (!int.TryParse(txtQuantidadeSolicitacao.Text, out quantidade) || quantidade <= 0)
                    {
                        throw new ArgumentException("Informe uma quantidade inteira positiva.");
                    }

                    Produto produto = comboProdutos.SelectedItem as Produto;
                    if (produto == null)
                    {
                        throw new ArgumentException("Selecione um produto.");
                    }

                    string setor = cmbSetorSolicitacao.Text;
                    if (cmbTipoSolicitacao.Text != "reposicao")
                    {
                        setor = "Geral do evento";
                    }

                    dadosDemo.Solicitacoes.Rows.Add(dadosDemo.Solicitacoes.Rows.Count + 1, txtSolicitante.Text, setor, cmbTipoSolicitacao.Text, produto.Nome, quantidade, "pendente", txtObservacaoSolicitacao.Text);
                    dadosDemo.NotificarAlteracao();
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception erro)
            {
                labelAvisoFormulario.Text = erro.Message;
            }
        }
    }

    public partial class FinanceiroControl
    {
        private DadosDemonstracao dadosDemo;
        internal void AtivarDemonstracao(DadosDemonstracao dados)
        {
            dadosDemo = dados;
            carregando = true;
            cmbFiltroFinanceiro.Items.Clear();
            cmbSetorCusto.Items.Clear();
            foreach (Evento evento in dados.Eventos)
            {
                cmbFiltroFinanceiro.Items.Add(evento);
                cmbSetorCusto.Items.Add(evento);
            }

            cmbFiltroFinanceiro.SelectedIndex = 0;
            cmbSetorCusto.SelectedIndex = 0;
            carregando = false;
            btnAdicionarCusto.Enabled = true;
            AtualizarFinanceiroDemo();
        }

        private void AtualizarFinanceiroDemo()
        {
            decimal despesas = 0;
            decimal receita = 0;
            dgvCustos.Rows.Clear();
            foreach (CustoPreview custo in dadosDemo.Equipe.Custos)
            {
                if (custo.Data.Date != dtpFinanceiro.Value.Date)
                {
                    continue;
                }

                despesas += custo.Valor;
                dgvCustos.Rows.Add(custo.Data.ToString("dd/MM/yyyy"), custo.Descricao, custo.Setor, custo.Valor.ToString("C2"), custo.Responsavel);
            }

            foreach (RegistroPreview registro in dadosDemo.Equipe.Registros)
            {
                if (registro.Tipo == "venda" && registro.Data.Date == dtpFinanceiro.Value.Date)
                {
                    receita += registro.Quantidade * registro.PrecoUnitario;
                }
            }

            lblValorFinanceiroResumo0.Text = receita.ToString("C2");
            lblValorFinanceiroResumo1.Text = despesas.ToString("C2");
            lblValorFinanceiroResumo2.Text = (receita - despesas).ToString("C2");
            labelAviso.Text = DadosDemonstracao.Aviso;
        }

        private void SalvarCustoDemo()
        {
            if (string.IsNullOrWhiteSpace(txtDescricaoCusto.Text) || numValorCusto.Value <= 0)
            {
                labelAviso.Text = "Informe descrição e valor positivo.";
                return;
            }

            dadosDemo.Equipe.Custos.Add(new CustoPreview { Descricao = txtDescricaoCusto.Text, Valor = numValorCusto.Value, Data = dtpFinanceiro.Value.Date, Responsavel = "Marina Alves", Setor = "Evento" });
            AtualizarFinanceiroDemo();
            labelAviso.Text = "Despesa simulada somente em memória.";
        }
    }
}
