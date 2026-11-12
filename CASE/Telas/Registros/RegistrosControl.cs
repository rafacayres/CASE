using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CASE.Models;
using CASE.Services;
using CASE.Utils;

namespace CASE.Quermesse
{
    public partial class RegistrosControl : UserControl
    {
        private readonly Usuario usuarioAutenticado;
        private readonly Evento eventoSelecionado;
        private readonly ConsultaOperacionalService consultas = new ConsultaOperacionalService();
        private readonly NumericUpDown precoPraticado = new NumericUpDown();
        private readonly ComboBox formaPagamento = new ComboBox();
        private bool carregando = true;
        private string chaveOperacao = Guid.NewGuid().ToString();

        public RegistrosControl() : this(null, null)
        {
        }

        public RegistrosControl(QuermessePreview exemplos) : this(null, null)
        {
        }

        public RegistrosControl(Usuario usuario, Evento evento)
        {
            InitializeComponent();
            usuarioAutenticado = usuario;
            eventoSelecionado = evento;
            FrontendTheme.Aplicar(this);
            lblTitulo.Text = "Vendas e perdas";
            lblSubtitulo.Text = "Registros reais das escalas autorizadas";
            lblContextoRegistro.Text = "Selecione uma escala de trabalho";
            panelNovoRegistro.Visible = false;
            lblObservacao.Width = 120;
            txtObservacaoRegistro.Left = 150;
            txtObservacaoRegistro.AutoSize = true;
            txtObservacaoRegistro.BorderStyle = BorderStyle.FixedSingle;
            txtObservacaoRegistro.BringToFront();
            VisualUtils.DefinirPlaceholder(txtObservacaoRegistro, "Detalhes ou justificativa da perda");
            lblSetor.Text = "Escala de trabalho";
            lblFuncionario.Visible = false;
            cmbPessoaRegistro.Visible = false;
            cmbSetorRegistro.Width = 900;
            cmbSetorRegistro.Items.Clear();
            cmbProdutoRegistro.Items.Clear();
            cmbTipoRegistro.Items.Clear();
            cmbTipoRegistro.Items.AddRange(new object[] { "venda", "perda" });
            cmbTipoRegistro.SelectedIndex = 0;
            precoPraticado.Name = "precoPraticado";
            precoPraticado.DecimalPlaces = 2;
            precoPraticado.Maximum = 99999999.99M;
            precoPraticado.SetBounds(735, 73, 120, 28);
            panelNovoRegistro.Controls.Add(precoPraticado);
            panelNovoRegistro.Controls.Add(FrontendTheme.Legenda("Preço unitário (R$)",
                new Rectangle(735, 47, 200, 22)));
            formaPagamento.Name = "formaPagamento";
            formaPagamento.DropDownStyle = ComboBoxStyle.DropDownList;
            formaPagamento.Items.AddRange(new object[] { "dinheiro", "pix", "cartao" });
            formaPagamento.SelectedIndex = 0;
            formaPagamento.SetBounds(150, 160, 215, 28);
            VisualUtils.EstilizarSelecao(formaPagamento);
            panelNovoRegistro.Controls.Add(formaPagamento);
            panelNovoRegistro.Controls.Add(FrontendTheme.Legenda("Pagamento",
                new Rectangle(18, 163, 130, 22)));
            panelNovoRegistro.Height = 215;
            panelListaRegistros.Top = 335;
            panelListaRegistros.Height = 260;
            cmbProdutoRegistro.DisplayMember = "nome";
            cmbSetorRegistro.DisplayMember = "descricao";
            cmbProdutoRegistro.SelectedIndexChanged += ProdutoAlterado;
            cmbTipoRegistro.SelectedIndexChanged += TipoAlterado;
            dgvRegistrosEquipe.Columns.Add("precoPraticado", "Preço praticado");
            dgvRegistrosEquipe.Columns.Add("pagamento", "Pagamento");
            labelAviso.Text = "Autentique-se para registrar operações.";
            btnRegistrar.Enabled = false;
            if (usuarioAutenticado == null)
            {
                carregando = false;
                return;
            }
            try
            {
                DataTable escalas = consultas.Escalas(usuarioAutenticado, IdEvento());
                foreach (DataRowView linha in escalas.DefaultView)
                {
                    cmbSetorRegistro.Items.Add(linha);
                }
                carregando = false;
                if (cmbSetorRegistro.Items.Count > 0)
                {
                    cmbSetorRegistro.SelectedIndex = 0;
                }
                else
                {
                    labelAviso.Text = "Não há escalas autorizadas. Cadastre equipe, jornada e escala primeiro.";
                }
                AtualizarRegistros();
            }
            catch (Exception erro)
            {
                ExibirErro(erro);
            }
            finally
            {
                carregando = false;
            }
        }

        private int IdEvento()
        {
            if (eventoSelecionado == null)
            {
                return 0;
            }
            return eventoSelecionado.IdEvento;
        }

        private void Filtros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                SelecionarSetorDemo();
                return;
            }

            if (carregando || usuarioAutenticado == null)
            {
                return;
            }
            try
            {
                DataRowView escala = cmbSetorRegistro.SelectedItem as DataRowView;
                cmbProdutoRegistro.Items.Clear();
                if (escala == null)
                {
                    return;
                }
                DataTable produtos = consultas.ProdutosDaEscala(usuarioAutenticado,
                    Convert.ToInt32(escala["id_escala"]));
                foreach (DataRowView produto in produtos.DefaultView)
                {
                    cmbProdutoRegistro.Items.Add(produto);
                }
                if (cmbProdutoRegistro.Items.Count > 0)
                {
                    cmbProdutoRegistro.SelectedIndex = 0;
                }
                string perfil;
                new AutorizacaoService().ValidarUsuarioEObterPerfil(usuarioAutenticado, out perfil);
                panelNovoRegistro.Visible = perfil == "Membro";
                btnRegistrar.Enabled = perfil == "Membro" && produtos.Rows.Count > 0;
                lblContextoRegistro.Text = Convert.ToString(escala["descricao"]);
                labelAviso.Text = "Vendas e perdas são permitidas durante eventos em andamento.";
            }
            catch (Exception erro)
            {
                btnRegistrar.Enabled = false;
                ExibirErro(erro);
            }
        }

        private void ProdutoAlterado(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                PrecoProdutoDemo();
                return;
            }

            DataRowView produto = cmbProdutoRegistro.SelectedItem as DataRowView;
            if (produto != null)
            {
                precoPraticado.Value = Convert.ToDecimal(produto["preco_venda"]);
            }
        }

        private void TipoAlterado(object sender, EventArgs e)
        {
            bool venda = cmbTipoRegistro.Text == "venda";
            precoPraticado.Enabled = venda;
            formaPagamento.Enabled = venda;
        }

        private void AtualizarRegistros()
        {
            if (dadosDemo != null)
            {
                AtualizarRegistrosDemo();
                return;
            }

            DataTable dados = consultas.Registros(usuarioAutenticado, IdEvento(),
                new DateTime(2000, 1, 1), DateTime.Today);
            dgvRegistrosEquipe.Rows.Clear();
            foreach (DataRow linha in dados.Rows)
            {
                dgvRegistrosEquipe.Rows.Add(linha["data_hora"], linha["responsavel"], linha["setor"],
                    linha["turno"], linha["tipo"], linha["produto"], linha["quantidade"],
                    linha["observacao"], linha["preco_unitario_praticado"], linha["forma_pagamento"]);
            }
            VisualUtils.ExibirEstadoVazio(dgvRegistrosEquipe, "Nenhum registro autorizado encontrado.");
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (dadosDemo != null)
            {
                RegistrarDemo();
                return;
            }

            DataRowView escala = cmbSetorRegistro.SelectedItem as DataRowView;
            DataRowView produto = cmbProdutoRegistro.SelectedItem as DataRowView;
            if (escala == null || produto == null || usuarioAutenticado == null)
            {
                labelAviso.Text = "Selecione uma escala e um produto autorizados.";
                return;
            }
            btnRegistrar.Enabled = false;
            try
            {
                Registro registro = new Registro();
                registro.IdEscala = Convert.ToInt32(escala["id_escala"]);
                registro.Tipo = cmbTipoRegistro.Text;
                registro.FormaPagamento = formaPagamento.Text;
                registro.Observacao = txtObservacaoRegistro.Text;
                registro.ChaveOperacao = chaveOperacao;
                RegistroItem item = new RegistroItem();
                item.IdProdutoSetor = Convert.ToInt32(produto["id_produto_setor"]);
                item.Quantidade = numQuantidadeRegistro.Value;
                item.PrecoUnitarioPraticado = precoPraticado.Value;
                int id = new RegistroService().Registrar(usuarioAutenticado, registro,
                    new List<RegistroItem> { item });
                chaveOperacao = Guid.NewGuid().ToString();
                txtObservacaoRegistro.Clear();
                numQuantidadeRegistro.Value = 1;
                labelAviso.Text = "Registro " + id + " confirmado. O estoque foi atualizado.";
                AtualizarRegistros();
            }
            catch (Exception erro)
            {
                ExibirErro(erro);
            }
            finally
            {
                btnRegistrar.Enabled = true;
            }
        }

        private void ExibirErro(Exception erro)
        {
            if (erro is ArgumentException || erro is InvalidOperationException ||
                erro is UnauthorizedAccessException)
            {
                labelAviso.Text = erro.Message;
            }
            else
            {
                labelAviso.Text = "Não foi possível concluir a operação. Confira a conexão e tente novamente.";
                System.Diagnostics.Trace.WriteLine(erro.GetType().Name + " em RegistrosControl: " + erro.StackTrace);
            }
        }
    }
}
