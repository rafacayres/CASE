using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CASE.Services;
using CASE.Utils;

namespace CASE
{
    public partial class PrincipalForm
    {
        private bool modoDemonstracao;
        private DadosDemonstracao dadosDemonstracao;
        private readonly Button botaoModoDemonstracao = new Button();
        private void ConfigurarModoDemonstracao()
        {
            string perfil;
            new AutorizacaoService().ValidarUsuarioEObterPerfil(usuarioAutenticado, out perfil);
            if (perfil != "Administrador")
            {
                return;
            }

            botaoModoDemonstracao.Name = "btnModoDemonstracao";
            botaoModoDemonstracao.Text = "Ativar demonstração";
            botaoModoDemonstracao.SetBounds(300, 17, 210, 36);
            botaoModoDemonstracao.BackColor = Color.White;
            botaoModoDemonstracao.ForeColor = VisualUtils.Texto;
            botaoModoDemonstracao.Click += AlternarModoDemonstracao;
            VisualUtils.EstilizarBotao(botaoModoDemonstracao);
            panelCabecalho.Controls.Add(botaoModoDemonstracao);
            botaoModoDemonstracao.BringToFront();
        }

        private void AlternarModoDemonstracao(object sender, EventArgs e)
        {
            try
            {
                if (!modoDemonstracao)
                {
                    // A prévia nunca fornece identidade nem substitui a autorização do login real.
                    string perfil;
                    new AutorizacaoService().ValidarUsuarioEObterPerfil(usuarioAutenticado, out perfil);
                    if (perfil != "Administrador")
                    {
                        throw new UnauthorizedAccessException("A apresentação está disponível ao administrador autenticado.");
                    }

                    if (dadosDemonstracao == null)
                    {
                        dadosDemonstracao = new DadosDemonstracao();
                        dadosDemonstracao.Alterado += (s, argumentos) =>
                        {
                            if (modoDemonstracao)
                            {
                                AtualizarContagemDemonstracao();
                            }
                        };
                    }

                    modoDemonstracao = true;
                    botaoModoDemonstracao.Text = "Modo demonstração · sair";
                    botaoModoDemonstracao.BackColor = Color.FromArgb(225, 238, 255);
                }
                else
                {
                    modoDemonstracao = false;
                    botaoModoDemonstracao.Text = "Ativar demonstração";
                    botaoModoDemonstracao.BackColor = Color.White;
                }

                // IDs ilustrativos nunca devem ser reutilizados no modo real.
                eventoSelecionado = null;
                textPesquisa.Text = string.Empty;
                AbrirTela(buttonDashboard, () => new DashboardControl(usuarioAutenticado));
                AtualizarContagemNotificacoes();
            }
            catch (Exception erro)
            {
                MessageBox.Show(this, erro.Message, "Modo demonstração", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void AtualizarContagemDemonstracao()
        {
            int quantidade = dadosDemonstracao.Pendencias();
            indicadorNotificacoes.Visible = quantidade > 0;
            indicadorNotificacoes.Text = quantidade.ToString();
            dicaNotificacoes.SetToolTip(pictureNotificacoes, quantidade + " pendências ilustrativas · modo demonstração");
        }

        private void AbrirNotificacoesDemonstracao()
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            foreach (var conta in dadosDemonstracao.Contas)
            {
                if (conta.Situacao != "pendente")
                {
                    continue;
                }

                ToolStripMenuItem cadastro = new ToolStripMenuItem("Cadastro ilustrativo · " + conta.Nome);
                cadastro.Click += (s, e) => AbrirTela(buttonAcessos, CriarGestaoAcessos);
                menu.Items.Add(cadastro);
            }

            foreach (DataRow pedido in dadosDemonstracao.Solicitacoes.Rows)
            {
                if (Convert.ToString(pedido["situacao"]) != "pendente")
                {
                    continue;
                }

                ToolStripMenuItem item = new ToolStripMenuItem(pedido["tipo"] + " · " + pedido["solicitante"] + " · " + pedido["quantidade"] + " " + pedido["produto"]);
                item.Tag = Convert.ToInt32(pedido["id"]);
                item.Click += (s, e) =>
                {
                    AbrirTela(buttonSolicitacoes, () => new CASE.Quermesse.SolicitacoesControl(usuarioAutenticado, eventoSelecionado));
                    var tela = panelConteudo.Controls[0] as CASE.Quermesse.SolicitacoesControl;
                    if (tela != null)
                    {
                        tela.AbrirPedidoDemo((int)((ToolStripMenuItem)s).Tag);
                    }
                };
                menu.Items.Add(item);
            }

            if (menu.Items.Count == 0)
            {
                menu.Items.Add(new ToolStripMenuItem("Nenhuma pendência ilustrativa") { Enabled = false });
            }

            menu.Closed += (s, e) => BeginInvoke(new Action(menu.Dispose));
            menu.Show(pictureNotificacoes, new Point(0, pictureNotificacoes.Height));
        }
    }
}