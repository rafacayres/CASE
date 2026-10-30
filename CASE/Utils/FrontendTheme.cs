using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace CASE.Utils
{
    internal static class FrontendTheme
    {
        public static readonly Color Azul = VisualUtils.Azul;
        public static readonly Color Texto = VisualUtils.Texto;
        public static readonly Color Fundo = VisualUtils.Fundo;

        private static readonly ConditionalWeakTable<Control, object> Estilizados = new ConditionalWeakTable<Control, object>();

        public static void Aplicar(Control raiz)
        {
            if (raiz is UserControl || raiz is Form) raiz.BackColor = Fundo;
            foreach (Control controle in raiz.Controls)
            {
                object aplicado;
                if (Estilizados.TryGetValue(controle, out aplicado)) { Aplicar(controle); continue; }
                Estilizados.Add(controle, new object());
                var painel = controle as Panel;
                if (painel != null && painel.BackColor.ToArgb() == Color.White.ToArgb())
                {
                    VisualUtils.Arredondar(painel, 12);
                    VisualUtils.AplicarBorda(painel);
                    painel.SizeChanged += (s, e) => VisualUtils.Arredondar(painel, 12);
                }
                var botao = controle as Button;
                if (botao != null)
                {
                    VisualUtils.EstilizarBotao(botao);
                }
                var grade = controle as DataGridView;
                if (grade != null)
                {
                    grade.BorderStyle = BorderStyle.None;
                    grade.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                    grade.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                    grade.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 253);
                    grade.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                    grade.DefaultCellStyle.Font = new Font("Segoe UI", 9);
                    grade.DefaultCellStyle.Padding = new Padding(8, 0, 4, 0);
                    grade.DefaultCellStyle.SelectionBackColor = Color.FromArgb(222, 235, 254);
                    grade.DefaultCellStyle.SelectionForeColor = Texto;
                    grade.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 253);
                    grade.ColumnHeadersDefaultCellStyle.SelectionForeColor = Texto;
                    grade.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
                    grade.GridColor = Color.FromArgb(240, 243, 248);
                    grade.RowTemplate.Height = 50;
                    grade.ColumnHeadersHeight = 42;
                    grade.ColumnHeadersDefaultCellStyle.ForeColor = VisualUtils.TextoSuave;
                    grade.DefaultCellStyle.ForeColor = Texto;
                    foreach (DataGridViewRow linha in grade.Rows) linha.Height = 50;
                    grade.CellPainting += PintarCelula;
                }
                var campo = controle as TextBox;
                if (campo != null)
                {
                    VisualUtils.EstilizarCampo(campo);
                }
                var combo = controle as ComboBox;
                if (combo != null) VisualUtils.EstilizarSelecao(combo);
                var icone = controle as PictureBox;
                if (icone != null && (icone.Image == null || icone.Name == "pictureIconeTela"))
                {
                    VisualUtils.Arredondar(icone, 8);
                    icone.SizeChanged += (s, e) => VisualUtils.Arredondar(icone, 8);
                }
                var titulo = controle as Label;
                if (titulo != null && titulo.Font.Size >= 18)
                {
                    titulo.Font = new Font("Segoe UI", titulo.Font.Size >= 24 ? 24 : 20, FontStyle.Bold);
                    titulo.ForeColor = VisualUtils.Marinho;
                }
                if (controle is Label && controle.BackColor.ToArgb() == Color.White.ToArgb() && !(raiz is Panel))
                    controle.BackColor = Color.Transparent;
                Aplicar(controle);
            }
        }

        private static void PintarCelula(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var grade = (DataGridView)sender;
            string valor = Convert.ToString(e.FormattedValue);
            bool acao = grade.Columns[e.ColumnIndex] is DataGridViewButtonColumn;
            bool verde = valor == "Aprovado" || valor == "Entrada" || valor == "Em estoque" || valor == "Normal" || valor == "Ativo" || valor == "Confirmada" || valor == "Concluída";
            bool vermelho = valor == "Rejeitado" || valor == "Suspenso" || valor == "Saída" || valor == "Baixo" || valor == "Estoque baixo" || valor == "Sem estoque" || valor == "Inativo";
            bool pendente = valor == "Pendente" || valor == "A confirmar";
            bool azul = valor == "Em andamento" || valor == "Em atendimento" || valor == "Venda";
            if (!acao && !verde && !vermelho && !pendente && !azul) return;
            e.PaintBackground(e.ClipBounds, false);
            bool excluir = valor == "Excluir";
            Color fundo = acao ? (excluir ? Color.FromArgb(253, 238, 239) : Color.FromArgb(234, 242, 254))
                : verde ? Color.FromArgb(233, 247, 241) : pendente ? Color.FromArgb(255, 247, 229)
                : azul ? Color.FromArgb(234, 242, 254) : Color.FromArgb(253, 238, 239);
            Color texto = acao ? (excluir ? Color.FromArgb(194, 78, 88) : Azul)
                : verde ? Color.FromArgb(43, 133, 104) : pendente ? Color.FromArgb(163, 120, 39)
                : azul ? Azul : Color.FromArgb(194, 78, 88);
            int largura = Math.Min(e.CellBounds.Width - 12, TextRenderer.MeasureText(valor, grade.Font).Width + 16);
            var area = new Rectangle(e.CellBounds.Left + 6, e.CellBounds.Top + 10, largura, e.CellBounds.Height - 20);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (var caminho = VisualUtils.CriarRetanguloArredondado(area, 5))
            using (var pincel = new SolidBrush(fundo)) e.Graphics.FillPath(pincel, caminho);
            TextRenderer.DrawText(e.Graphics, valor, grade.Font, area, texto,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            e.Handled = true;
        }

        public static Label Legenda(string texto, Rectangle area)
        {
            return new Label { Text = texto, Bounds = area, ForeColor = Texto,
                Font = new Font("Segoe UI", 9), BackColor = Color.Transparent };
        }

        public static void Grafico(Panel painel, string tipo)
        {
            painel.Controls.Add(new GraficoPreview { Dock = DockStyle.Fill, Tipo = tipo,
                AccessibleName = "Gráfico ilustrativo com dados de demonstração" });
        }
    }
}
