using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;

namespace CASE.Utils
{
    public static class ExportadorRelatorio
    {
        public static void ExportarCsv(DataTable dados, string caminho)
        {
            if (dados == null || dados.Columns.Count == 0 || string.IsNullOrWhiteSpace(caminho))
            {
                throw new ArgumentException("Selecione dados e um arquivo de destino.");
            }
            using (StreamWriter escritor = new StreamWriter(caminho, false, new UTF8Encoding(true)))
            {
                string[] valores = new string[dados.Columns.Count];
                for (int i = 0; i < valores.Length; i++)
                {
                    valores[i] = CampoCsv(dados.Columns[i].ColumnName);
                }
                escritor.WriteLine(string.Join(";", valores));
                foreach (DataRow linha in dados.Rows)
                {
                    for (int i = 0; i < valores.Length; i++)
                    {
                        valores[i] = CampoCsv(Convert.ToString(linha[i]));
                    }
                    escritor.WriteLine(string.Join(";", valores));
                }
            }
        }

        private static string CampoCsv(string valor)
        {
            string inicio = valor.TrimStart();
            if (inicio.StartsWith("=") || inicio.StartsWith("+") || inicio.StartsWith("-") ||
                inicio.StartsWith("@") || valor.StartsWith("\t") || valor.StartsWith("\r"))
            {
                valor = "'" + valor;
            }
            return "\"" + valor.Replace("\"", "\"\"") + "\"";
        }

        public static void ExportarPdf(DataTable dados, string caminho)
        {
            if (dados == null || dados.Columns.Count == 0 || string.IsNullOrWhiteSpace(caminho))
            {
                throw new ArgumentException("Selecione dados e um arquivo de destino.");
            }
            using (PrintDocument documento = new PrintDocument())
            using (Font fonte = new Font("Segoe UI", 8F))
            using (Font cabecalho = new Font("Segoe UI", 8F, FontStyle.Bold))
            {
                documento.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                if (!documento.PrinterSettings.IsValid)
                {
                    throw new InvalidOperationException("A impressora Microsoft Print to PDF não está disponível neste Windows. Utilize a exportação CSV.");
                }
                documento.PrintController = new StandardPrintController();
                documento.DocumentName = "CASE — relatório";
                documento.PrinterSettings.PrintToFile = true;
                documento.PrinterSettings.PrintFileName = caminho;
                documento.DefaultPageSettings.Landscape = true;
                documento.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40);
                int linhaAtual = 0;
                int pagina = 0;
                documento.PrintPage += (s, e) =>
                {
                    pagina++;
                    float larguraColuna = (float)e.MarginBounds.Width / dados.Columns.Count;
                    float topo = e.MarginBounds.Top;
                    e.Graphics.DrawString("CASE — relatório / página " + pagina,
                        cabecalho, Brushes.Black, e.MarginBounds.Left, topo);
                    topo += 28;
                    float alturaCabecalho = 38;
                    for (int coluna = 0; coluna < dados.Columns.Count; coluna++)
                    {
                        float esquerda = e.MarginBounds.Left + coluna * larguraColuna;
                        e.Graphics.DrawString(dados.Columns[coluna].ColumnName, cabecalho, Brushes.Black,
                            new RectangleF(esquerda + 3, topo, larguraColuna - 6, alturaCabecalho));
                    }
                    topo += alturaCabecalho;
                    while (linhaAtual < dados.Rows.Count)
                    {
                        DataRow linha = dados.Rows[linhaAtual];
                        float altura = 25;
                        for (int coluna = 0; coluna < dados.Columns.Count; coluna++)
                        {
                            altura = Math.Max(altura, e.Graphics.MeasureString(
                                Convert.ToString(linha[coluna]), fonte, (int)larguraColuna - 6).Height + 8);
                        }
                        if (altura > e.MarginBounds.Height - 70)
                        {
                            throw new InvalidOperationException("Um texto excede o espaço de uma página. Exporte este relatório em CSV para preservar o conteúdo completo.");
                        }
                        if (topo + altura > e.MarginBounds.Bottom)
                        {
                            e.HasMorePages = true;
                            return;
                        }
                        for (int coluna = 0; coluna < dados.Columns.Count; coluna++)
                        {
                            float esquerda = e.MarginBounds.Left + coluna * larguraColuna;
                            e.Graphics.DrawString(Convert.ToString(linha[coluna]), fonte, Brushes.Black,
                                new RectangleF(esquerda + 3, topo + 3, larguraColuna - 6, altura));
                        }
                        topo += altura;
                        e.Graphics.DrawLine(Pens.LightGray, e.MarginBounds.Left, topo, e.MarginBounds.Right, topo);
                        linhaAtual++;
                    }
                    e.HasMorePages = false;
                };
                documento.Print();
            }
        }
    }
}
