using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;

namespace CASE.Services
{
    public class SenhaService
    {
        private const int TamanhoSalt = 16;
        private const int TamanhoHash = 32;
        private const int MemoriaPadraoKb = 65536;
        private const int IteracoesPadrao = 3;
        private const int ParalelismoPadrao = 1;
        private const int MemoriaMinimaKb = 8192;
        private const int MemoriaMaximaKb = 262144;
        private const int IteracoesMinimas = 1;
        private const int IteracoesMaximas = 10;
        private const int ParalelismoMinimo = 1;
        private const int ParalelismoMaximo = 8;
        private static readonly Encoding CodificacaoSenha = new UTF8Encoding(false, true);

        private readonly int memoriaKb;
        private readonly int iteracoes;
        private readonly int paralelismo;

        public SenhaService()
            : this(MemoriaPadraoKb, IteracoesPadrao, ParalelismoPadrao)
        {
        }

        public SenhaService(int memoriaKb, int iteracoes, int paralelismo)
        {
            ValidarParametros(memoriaKb, iteracoes, paralelismo);

            this.memoriaKb = memoriaKb;
            this.iteracoes = iteracoes;
            this.paralelismo = paralelismo;
        }

        public string GerarHash(string senha)
        {
            if (string.IsNullOrEmpty(senha))
            {
                throw new ArgumentException("A senha não pode ficar vazia.", nameof(senha));
            }

            byte[] salt = new byte[TamanhoSalt];
            byte[] senhaBytes;

            try
            {
                senhaBytes = CodificacaoSenha.GetBytes(senha);
            }
            catch (EncoderFallbackException erro)
            {
                throw new ArgumentException(
                    "A senha contém caracteres inválidos.",
                    nameof(senha),
                    erro);
            }

            byte[] hash = new byte[TamanhoHash];

            try
            {
                using (RandomNumberGenerator geradorAleatorio = RandomNumberGenerator.Create())
                {
                    geradorAleatorio.GetBytes(salt);
                }

                GerarBytesArgon2id(
                    senhaBytes,
                    salt,
                    hash,
                    memoriaKb,
                    iteracoes,
                    paralelismo);

                string saltCodificado = Convert.ToBase64String(salt);
                string hashCodificado = Convert.ToBase64String(hash);

                return string.Format(
                    CultureInfo.InvariantCulture,
                    "argon2id$v=19$m={0},t={1},p={2}${3}${4}",
                    memoriaKb,
                    iteracoes,
                    paralelismo,
                    saltCodificado,
                    hashCodificado);
            }
            finally
            {
                Array.Clear(senhaBytes, 0, senhaBytes.Length);
                Array.Clear(salt, 0, salt.Length);
                Array.Clear(hash, 0, hash.Length);
            }
        }

        public bool VerificarSenha(string senha, string hashArmazenado)
        {
            if (string.IsNullOrEmpty(senha) || string.IsNullOrWhiteSpace(hashArmazenado))
            {
                return false;
            }

            int memoriaHash;
            int iteracoesHash;
            int paralelismoHash;
            byte[] salt;
            byte[] hashEsperado;

            if (!TentarLerHash(
                hashArmazenado,
                out memoriaHash,
                out iteracoesHash,
                out paralelismoHash,
                out salt,
                out hashEsperado))
            {
                return false;
            }

            byte[] senhaBytes;

            try
            {
                senhaBytes = CodificacaoSenha.GetBytes(senha);
            }
            catch (EncoderFallbackException)
            {
                return false;
            }

            byte[] hashCalculado = new byte[TamanhoHash];

            try
            {
                GerarBytesArgon2id(
                    senhaBytes,
                    salt,
                    hashCalculado,
                    memoriaHash,
                    iteracoesHash,
                    paralelismoHash);

                return CompararTempoConstante(hashCalculado, hashEsperado);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            finally
            {
                Array.Clear(senhaBytes, 0, senhaBytes.Length);
                Array.Clear(salt, 0, salt.Length);
                Array.Clear(hashEsperado, 0, hashEsperado.Length);
                Array.Clear(hashCalculado, 0, hashCalculado.Length);
            }
        }

        private static void GerarBytesArgon2id(
            byte[] senha,
            byte[] salt,
            byte[] resultado,
            int memoriaKb,
            int iteracoes,
            int paralelismo)
        {
            Argon2Parameters parametros = new Argon2Parameters.Builder(
                Argon2Parameters.Argon2id)
                .WithVersion(Argon2Parameters.Version13)
                .WithMemoryAsKB(memoriaKb)
                .WithIterations(iteracoes)
                .WithParallelism(paralelismo)
                .WithSalt(salt)
                .Build();

            Argon2BytesGenerator gerador = new Argon2BytesGenerator();
            gerador.Init(parametros);
            gerador.GenerateBytes(senha, resultado);
        }

        private static bool TentarLerHash(
            string hashArmazenado,
            out int memoriaKb,
            out int iteracoes,
            out int paralelismo,
            out byte[] salt,
            out byte[] hash)
        {
            memoriaKb = 0;
            iteracoes = 0;
            paralelismo = 0;
            salt = new byte[0];
            hash = new byte[0];

            if (hashArmazenado.Length > 255)
            {
                return false;
            }

            string[] partes = hashArmazenado.Split('$');

            if (partes.Length != 5 || partes[0] != "argon2id" || partes[1] != "v=19")
            {
                return false;
            }

            string[] parametros = partes[2].Split(',');

            if (parametros.Length != 3 ||
                !LerParametro(parametros[0], "m", out memoriaKb) ||
                !LerParametro(parametros[1], "t", out iteracoes) ||
                !LerParametro(parametros[2], "p", out paralelismo))
            {
                return false;
            }

            if (!ParametrosValidos(memoriaKb, iteracoes, paralelismo))
            {
                return false;
            }

            try
            {
                salt = Convert.FromBase64String(partes[3]);
                hash = Convert.FromBase64String(partes[4]);
            }
            catch (FormatException)
            {
                return false;
            }

            if (salt.Length < TamanhoSalt || salt.Length > 64 || hash.Length != TamanhoHash)
            {
                Array.Clear(salt, 0, salt.Length);
                Array.Clear(hash, 0, hash.Length);
                salt = new byte[0];
                hash = new byte[0];
                return false;
            }

            return true;
        }

        private static bool LerParametro(string texto, string nome, out int valor)
        {
            valor = 0;
            string prefixo = nome + "=";

            if (!texto.StartsWith(prefixo, StringComparison.Ordinal))
            {
                return false;
            }

            string valorTexto = texto.Substring(prefixo.Length);

            return int.TryParse(
                valorTexto,
                NumberStyles.None,
                CultureInfo.InvariantCulture,
                out valor);
        }

        private static void ValidarParametros(int memoriaKb, int iteracoes, int paralelismo)
        {
            if (!ParametrosValidos(memoriaKb, iteracoes, paralelismo))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(memoriaKb),
                    "Os parâmetros de custo do Argon2id estão fora dos limites permitidos.");
            }
        }

        private static bool ParametrosValidos(int memoriaKb, int iteracoes, int paralelismo)
        {
            if (memoriaKb < MemoriaMinimaKb || memoriaKb > MemoriaMaximaKb)
            {
                return false;
            }

            if (iteracoes < IteracoesMinimas || iteracoes > IteracoesMaximas)
            {
                return false;
            }

            if (paralelismo < ParalelismoMinimo || paralelismo > ParalelismoMaximo)
            {
                return false;
            }

            if (memoriaKb < 8 * paralelismo)
            {
                return false;
            }

            return true;
        }

        private static bool CompararTempoConstante(byte[] primeiro, byte[] segundo)
        {
            if (primeiro == null || segundo == null || primeiro.Length != segundo.Length)
            {
                return false;
            }

            int diferenca = 0;

            for (int indice = 0; indice < primeiro.Length; indice++)
            {
                diferenca |= primeiro[indice] ^ segundo[indice];
            }

            return diferenca == 0;
        }
    }
}
