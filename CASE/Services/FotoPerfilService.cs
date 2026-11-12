using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using CASE.Models;
using CASE.Repositories;
using MySql.Data.MySqlClient;

namespace CASE.Services
{
    public class FotoPerfilService
    {
        private const int TamanhoMaximoArquivo = 2 * 1024 * 1024;
        private const int TamanhoMaximoImagem = 256;
        private const int DimensaoMaximaOriginal = 12000;
        private const long TotalMaximoPixels = 30000000;

        private readonly PessoaRepository pessoaRepository = new PessoaRepository();
        private readonly AutorizacaoService autorizacaoService = new AutorizacaoService();

        public byte[] BuscarFotoPerfil(Usuario usuarioAutenticado)
        {
            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuarioAutenticado,
                out nomePerfil);

            try
            {
                return pessoaRepository.BuscarFotoPerfil(usuarioAtual.IdPessoa);
            }
            catch (MySqlException erro)
            {
                if (erro.Number == 1054)
                {
                    return null;
                }

                throw;
            }
        }

        public byte[] SalvarFotoPerfil(Usuario usuarioAutenticado, byte[] arquivoImagem)
        {
            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuarioAutenticado,
                out nomePerfil);

            byte[] fotoRedimensionada = PrepararFoto(arquivoImagem);

            try
            {
                pessoaRepository.AtualizarFotoPerfil(
                    usuarioAtual.IdPessoa,
                    fotoRedimensionada);
            }
            catch (MySqlException erro)
            {
                if (erro.Number == 1054)
                {
                    throw new InvalidOperationException(
                        "A foto de perfil ainda não está habilitada no banco. " +
                        "A migração da coluna foto_perfil precisa ser aplicada.");
                }

                throw;
            }

            return fotoRedimensionada;
        }

        public void RemoverFotoPerfil(Usuario usuarioAutenticado)
        {
            string nomePerfil;
            Usuario usuarioAtual = autorizacaoService.ValidarUsuarioEObterPerfil(
                usuarioAutenticado,
                out nomePerfil);

            try
            {
                pessoaRepository.AtualizarFotoPerfil(usuarioAtual.IdPessoa, null);
            }
            catch (MySqlException erro)
            {
                if (erro.Number == 1054)
                {
                    throw new InvalidOperationException(
                        "A foto de perfil ainda não está habilitada no banco. " +
                        "A migração da coluna foto_perfil precisa ser aplicada.");
                }

                throw;
            }
        }

        public byte[] PrepararFoto(byte[] arquivoImagem)
        {
            if (arquivoImagem == null || arquivoImagem.Length == 0)
            {
                throw new ArgumentException("Selecione um arquivo de imagem.", nameof(arquivoImagem));
            }

            if (arquivoImagem.Length > TamanhoMaximoArquivo)
            {
                throw new ArgumentException("A imagem não pode ultrapassar 2 MB.", nameof(arquivoImagem));
            }

            using (MemoryStream fluxoImagem = new MemoryStream(arquivoImagem))
            using (Image imagemOriginal = Image.FromStream(fluxoImagem, true, true))
            {
                ValidarFormato(imagemOriginal);
                ValidarDimensoes(imagemOriginal);

                int largura;
                int altura;
                CalcularDimensoesReduzidas(
                    imagemOriginal.Width,
                    imagemOriginal.Height,
                    out largura,
                    out altura);

                using (Bitmap imagemRedimensionada = new Bitmap(largura, altura))
                using (Graphics grafico = Graphics.FromImage(imagemRedimensionada))
                using (MemoryStream fluxoResultado = new MemoryStream())
                {
                    grafico.CompositingMode = CompositingMode.SourceCopy;
                    grafico.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    grafico.SmoothingMode = SmoothingMode.HighQuality;
                    grafico.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    grafico.DrawImage(
                        imagemOriginal,
                        new Rectangle(0, 0, largura, altura));

                    imagemRedimensionada.Save(fluxoResultado, ImageFormat.Png);
                    byte[] fotoPreparada = fluxoResultado.ToArray();

                    if (fotoPreparada.Length > TamanhoMaximoArquivo)
                    {
                        throw new ArgumentException(
                            "A imagem redimensionada ultrapassou o limite permitido.",
                            nameof(arquivoImagem));
                    }

                    return fotoPreparada;
                }
            }
        }

        private static void ValidarFormato(Image imagem)
        {
            Guid formato = imagem.RawFormat.Guid;

            if (formato != ImageFormat.Jpeg.Guid && formato != ImageFormat.Png.Guid)
            {
                throw new ArgumentException("O arquivo precisa estar no formato JPG ou PNG.");
            }
        }

        private static void ValidarDimensoes(Image imagem)
        {
            if (imagem.Width <= 0 || imagem.Height <= 0 ||
                imagem.Width > DimensaoMaximaOriginal ||
                imagem.Height > DimensaoMaximaOriginal)
            {
                throw new ArgumentException("As dimensões da imagem não são válidas.");
            }

            long quantidadePixels = (long)imagem.Width * imagem.Height;

            if (quantidadePixels > TotalMaximoPixels)
            {
                throw new ArgumentException("A resolução da imagem é grande demais.");
            }
        }

        private static void CalcularDimensoesReduzidas(
            int larguraOriginal,
            int alturaOriginal,
            out int largura,
            out int altura)
        {
            double escalaLargura = (double)TamanhoMaximoImagem / larguraOriginal;
            double escalaAltura = (double)TamanhoMaximoImagem / alturaOriginal;
            double escala = Math.Min(1.0, Math.Min(escalaLargura, escalaAltura));

            largura = Math.Max(1, (int)Math.Round(larguraOriginal * escala));
            altura = Math.Max(1, (int)Math.Round(alturaOriginal * escala));
        }
    }
}
