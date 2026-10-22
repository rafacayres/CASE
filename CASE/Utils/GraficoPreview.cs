using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CASE.Utils
{
    // Desenho vetorial de dados fixos, apenas para a apresentação do frontend.
    internal sealed class GraficoPreview : Control
    {
        public string Tipo { get; set; }
        public GraficoPreview()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 8);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Width < 100 || Height < 90) return;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Color[] cores = { FrontendTheme.Azul, Color.FromArgb(255, 85, 91),
                Color.FromArgb(255, 196, 73), Color.FromArgb(66, 199, 174) };
            if (Tipo == "rosca")
            {
                int tamanho = Math.Min(Height - 40, Width / 2 - 20);
                var area = new Rectangle(12, 20, tamanho, tamanho);
                float inicio = -90;
                int[] partes = {45,25,20,10};
                string[] nomes = {"Informática   45%", "Móveis          25%", "Eletrônicos   20%", "Outros          10%"};
                for (int i = 0; i < 4; i++)
                {
                    using (var pincel = new SolidBrush(cores[i]))
                    {
                        g.FillPie(pincel, area, inicio, partes[i] * 3.6f);
                        g.FillEllipse(pincel, tamanho + 32, 28 + i * 28, 7, 7);
                    }
                    g.DrawString(nomes[i], Font, Brushes.SlateGray, tamanho + 45, 23 + i * 28);
                    inicio += partes[i] * 3.6f;
                }
                g.FillEllipse(Brushes.White, area.X + tamanho / 4, area.Y + tamanho / 4, tamanho / 2, tamanho / 2);
                return;
            }
            int esquerda = 36, topo = 34, largura = Width - 52, altura = Height - 66;
            using (var linha = new Pen(Color.FromArgb(233, 239, 246)))
            for (int i = 0; i <= 4; i++)
            {
                float y = topo + altura * i / 4f;
                g.DrawLine(linha, esquerda, y, esquerda + largura, y);
                g.DrawString((40 - i * 10).ToString(), Font, Brushes.SlateGray, 7, y - 6);
            }
            string[] dias = Tipo == "barras" ? new[] {"01/10","02/10","03/10","04/10","05/10","06/10","07/10"}
                : new[] {"Seg","Ter","Qua","Qui","Sex","Sáb","Dom"};
            int[][] valores = { new[] {20,19,23,14,27,18,31}, new[] {12,17,13,19,16,13,20} };
            for (int serie = 0; serie < 2; serie++)
            using (var pincel = new SolidBrush(cores[serie]))
            using (var caneta = new Pen(cores[serie], 2))
            {
                g.FillEllipse(pincel, Width - 165 + serie * 85, 9, 7, 7);
                g.DrawString(serie == 0 ? "Entradas" : "Saídas", Font, Brushes.SlateGray, Width - 154 + serie * 85, 5);
                var pontos = new PointF[7];
                for (int i = 0; i < 7; i++)
                {
                    float x = esquerda + largura * (i + .5f) / 7;
                    float y = topo + altura * (1 - valores[serie][i] / 40f);
                    pontos[i] = new PointF(x, y);
                    if (Tipo == "barras") g.FillRectangle(pincel, x - 11 + serie * 12, y, 10, topo + altura - y);
                    else g.FillEllipse(pincel, x - 2, y - 2, 4, 4);
                    if (serie == 0) g.DrawString(dias[i], Font, Brushes.SlateGray, x - 15, topo + altura + 10);
                }
                if (Tipo != "barras") g.DrawCurve(caneta, pontos, .25f);
            }
        }
    }
}
