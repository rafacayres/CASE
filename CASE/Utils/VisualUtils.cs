
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CASE.Utils
{
    public static class VisualUtils
    {
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

            if (painel == null)
                return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int raio = 8;
            int diametro = raio * 2;

            Rectangle area = new Rectangle(
                0, 0, painel.Width - 1, painel.Height - 1
            );

            using (GraphicsPath caminho = new GraphicsPath())
            using (Pen caneta = new Pen(Color.FromArgb(205, 213, 223), 1))
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
