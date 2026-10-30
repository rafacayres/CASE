using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace CASE.Utils
{
    internal sealed class AcabamentoArredondado
    {
        private static readonly ConditionalWeakTable<Control, AcabamentoArredondado> acabamentos =
            new ConditionalWeakTable<Control, AcabamentoArredondado>();

        private readonly Control controle;
        private int raio;
        private bool borda;

        private AcabamentoArredondado(Control controle)
        {
            this.controle = controle;
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(controle, true, null);
            controle.Paint += Pintar;
            controle.SizeChanged += Invalidar;
            controle.BackColorChanged += Invalidar;
            controle.EnabledChanged += Invalidar;
            controle.ParentChanged += Invalidar;
        }

        public static void Aplicar(Control controle, int raio)
        {
            var acabamento = acabamentos.GetValue(controle, c => new AcabamentoArredondado(c));
            acabamento.raio = Math.Max(0, raio);
            var regiao = controle.Region;
            controle.Region = null;
            regiao?.Dispose();

            var botao = controle as Button;
            if (botao != null && botao.FlatAppearance.BorderSize > 0)
            {
                acabamento.borda = true;
                botao.FlatAppearance.BorderSize = 0;
            }
            controle.Invalidate();
        }

        public static void DefinirBorda(Control controle, bool visivel)
        {
            AcabamentoArredondado acabamento;
            if (!acabamentos.TryGetValue(controle, out acabamento))
            {
                Aplicar(controle, 12);
                acabamento = acabamentos.GetValue(controle, c => new AcabamentoArredondado(c));
            }
            acabamento.borda = visivel;
            controle.Invalidate();
        }

        private void Invalidar(object sender, EventArgs e)
        {
            controle.Invalidate();
        }

        private Color CorDoFundo()
        {
            for (Control pai = controle.Parent; pai != null; pai = pai.Parent)
                if (pai.BackColor.A == 255) return pai.BackColor;
            return SystemColors.Control;
        }

        private void Pintar(object sender, PaintEventArgs e)
        {
            if (controle.Width < 2 || controle.Height < 2 || raio == 0) return;

            GraphicsState estado = e.Graphics.Save();
            try
            {
                float escala = e.Graphics.DpiX / 96F;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                var area = new RectangleF(0.5F, 0.5F, controle.Width - 1F, controle.Height - 1F);
                using (var contorno = VisualUtils.CriarRetanguloArredondado(area, raio * escala))
                using (var cantos = new GraphicsPath(FillMode.Alternate))
                using (var fundo = new SolidBrush(CorDoFundo()))
                {
                    // A pintura preserva a suavização que se perde no recorte por Region.
                    cantos.AddRectangle(new RectangleF(-1, -1, controle.Width + 2, controle.Height + 2));
                    cantos.AddPath(contorno, false);
                    e.Graphics.FillPath(fundo, cantos);
                    if (borda)
                    {
                        var botao = controle as Button;
                        Color cor = botao == null ? VisualUtils.Borda : botao.FlatAppearance.BorderColor;
                        using (var caneta = new Pen(cor, escala))
                            e.Graphics.DrawPath(caneta, contorno);
                    }
                }
            }
            finally
            {
                e.Graphics.Restore(estado);
            }
        }
    }
}
