
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CASE.Utils
{
    public static class VisualUtils
    {
        public static readonly Color Azul = Color.FromArgb(23, 105, 210);
        public static readonly Color Marinho = Color.FromArgb(16, 43, 73);
        public static readonly Color Texto = Color.FromArgb(30, 51, 75);
        public static readonly Color TextoSuave = Color.FromArgb(115, 132, 151);
        public static readonly Color Fundo = Color.FromArgb(246, 248, 252);
        public static readonly Color Borda = Color.FromArgb(228, 234, 242);

        public static PictureBox CriarEspacoIcone(string nome, Rectangle area, Color fundo)
        {
            var icone = new PictureBox { Name = nome, Bounds = area, BackColor = fundo,
                SizeMode = PictureBoxSizeMode.Zoom, TabStop = false, AccessibleName = "Espaço para ícone" };
            Arredondar(icone, 8);
            icone.SizeChanged += (s, e) => Arredondar(icone, 8);
            return icone;
        }

        public static void EstilizarBotao(Button botao)
        {
            bool principal = botao.ForeColor.ToArgb() == Color.White.ToArgb();
            botao.FlatStyle = FlatStyle.Flat;
            botao.Cursor = Cursors.Hand;
            botao.Font = new Font("Segoe UI", 9.5F, principal ? FontStyle.Bold : FontStyle.Regular);
            botao.BackColor = principal ? Azul : Color.White;
            botao.ForeColor = principal ? Color.White : Texto;
            botao.FlatAppearance.BorderSize = principal ? 0 : 1;
            botao.FlatAppearance.BorderColor = Borda;
            botao.FlatAppearance.MouseOverBackColor = principal ? Color.FromArgb(20, 91, 181) : Color.FromArgb(241, 246, 252);
            botao.FlatAppearance.MouseDownBackColor = principal ? Color.FromArgb(17, 76, 154) : Color.FromArgb(231, 239, 249);
            Arredondar(botao, 8);
            botao.SizeChanged += (s, e) => Arredondar(botao, 8);
        }

        public static void EstilizarCampo(TextBox campo)
        {
            campo.BorderStyle = BorderStyle.None;
            campo.BackColor = Color.White;
            campo.ForeColor = Texto;
            Control parent = campo.Parent;
            parent.Paint += (s, e) =>
            {
                if (!campo.Visible) return;
                var area = new Rectangle(campo.Left - 7, campo.Top - 6, campo.Width + 14, campo.Height + 12);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var caminho = CriarRetanguloArredondado(area, 7))
                using (var caneta = new Pen(campo.Focused ? Azul : Borda, campo.Focused ? 1.4F : 1F))
                    e.Graphics.DrawPath(caneta, caminho);
            };
            campo.Enter += (s, e) => parent.Invalidate();
            campo.Leave += (s, e) => parent.Invalidate();
            campo.LocationChanged += (s, e) => parent.Invalidate();
            campo.SizeChanged += (s, e) => parent.Invalidate();
            campo.VisibleChanged += (s, e) => parent.Invalidate();
        }

        public static void EstilizarSelecao(ComboBox campo)
        {
            campo.FlatStyle = FlatStyle.Flat;
            campo.BackColor = Color.White;
            campo.ForeColor = Texto;
            campo.DrawMode = DrawMode.OwnerDrawFixed;
            campo.ItemHeight = 23;
            campo.DrawItem += (s, e) =>
            {
                if (e.Bounds.Width <= 0 || e.Bounds.Height <= 0) return;
                bool selecionado = (e.State & DrawItemState.Selected) != 0 && (e.State & DrawItemState.ComboBoxEdit) == 0;
                using (var pincel = new SolidBrush(selecionado ? Color.FromArgb(237, 244, 254) : Color.White))
                    e.Graphics.FillRectangle(pincel, e.Bounds);
                string texto = e.Index >= 0 ? campo.GetItemText(campo.Items[e.Index]) : campo.Text;
                var area = new Rectangle(e.Bounds.Left + 8, e.Bounds.Top, e.Bounds.Width - 12, e.Bounds.Height);
                TextRenderer.DrawText(e.Graphics, texto, campo.Font, area, campo.Enabled ? Texto : TextoSuave,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
            };
        }
        public static GraphicsPath CriarRetanguloArredondado(Rectangle area, int raio)
        {
            int d = Math.Max(2, Math.Min(raio * 2, Math.Min(area.Width, area.Height)));
            var caminho = new GraphicsPath();
            caminho.AddArc(area.Left, area.Top, d, d, 180, 90);
            caminho.AddArc(area.Right - d, area.Top, d, d, 270, 90);
            caminho.AddArc(area.Right - d, area.Bottom - d, d, d, 0, 90);
            caminho.AddArc(area.Left, area.Bottom - d, d, d, 90, 90);
            caminho.CloseFigure();
            return caminho;
        }

        // Arredonda os cantos de um controle
        public static void Arredondar(Control controle, int raio)
        {
            if (controle.Width <= 0 || controle.Height <= 0)
                return;

            raio = Math.Max(0, Math.Min(raio,
                Math.Min(controle.Width, controle.Height) / 2));

            if (raio == 0)
            {
                controle.Region?.Dispose();
                controle.Region = null;
                return;
            }

            int diametro = raio * 2;

            using (GraphicsPath caminho = new GraphicsPath())
            {
                Rectangle area = new Rectangle(
                    0, 0, controle.Width, controle.Height
                );

                caminho.AddArc(0, 0, diametro, diametro, 180, 90);
                caminho.AddArc(area.Width - diametro, 0, diametro, diametro, 270, 90);
                caminho.AddArc(area.Width - diametro, area.Height - diametro, diametro, diametro, 0, 90);
                caminho.AddArc(0, area.Height - diametro, diametro, diametro, 90, 90);
                caminho.CloseFigure();

                Region anterior = controle.Region;
                controle.Region = new Region(caminho);
                anterior?.Dispose();
            }
        }

        // Desenha a borda cinza arredondada dos Panels
        public static void DesenharBorda(object sender, PaintEventArgs e)
        {
            Panel painel = sender as Panel;

            if (painel == null || painel.Width < 18 || painel.Height < 18)
                return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int raio = 8;
            int diametro = raio * 2;

            Rectangle area = new Rectangle(
                0, 0, painel.Width - 1, painel.Height - 1
            );

            using (GraphicsPath caminho = new GraphicsPath())
            using (Pen caneta = new Pen(Borda, 1))
            {
                caminho.AddArc(area.Left, area.Top, diametro, diametro, 180, 90);
                caminho.AddArc(area.Right - diametro, area.Top, diametro, diametro, 270, 90);
                caminho.AddArc(area.Right - diametro, area.Bottom - diametro, diametro, diametro, 0, 90);
                caminho.AddArc(area.Left, area.Bottom - diametro, diametro, diametro, 90, 90);
                caminho.CloseFigure();

                e.Graphics.DrawPath(caneta, caminho);
            }
        }
    }
}
