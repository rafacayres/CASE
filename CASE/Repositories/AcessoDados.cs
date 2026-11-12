using System;
using System.Collections.Generic;
using CASE.Data;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    internal static class AcessoDados
    {
        // Executa um SELECT e converte cada linha em um objeto.
        internal static List<T> ConsultarLista<T>(
            string sql,
            Func<MySqlDataReader, T> converterLinha,
            Action<MySqlCommand> preencherParametros = null)
        {
            using (var conexao = Conexao.CriarConexao())
            {
                conexao.Open();

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    if (preencherParametros != null)
                    {
                        preencherParametros(comando);
                    }

                    using (var leitor = comando.ExecuteReader())
                    {
                        List<T> resultados = new List<T>();

                        while (leitor.Read())
                        {
                            resultados.Add(converterLinha(leitor));
                        }

                        return resultados;
                    }
                }
            }
        }

        // Executa um SELECT que retorna no maximo uma linha.
        internal static T ConsultarUnico<T>(
            string sql,
            Func<MySqlDataReader, T> converterLinha,
            Action<MySqlCommand> preencherParametros = null) where T : class
        {
            using (var conexao = Conexao.CriarConexao())
            {
                conexao.Open();

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    if (preencherParametros != null)
                    {
                        preencherParametros(comando);
                    }

                    using (var leitor = comando.ExecuteReader())
                    {
                        if (leitor.Read())
                        {
                            return converterLinha(leitor);
                        }

                        return null;
                    }
                }
            }
        }

        // Executa um INSERT e devolve o identificador criado pelo MySQL.
        internal static int Inserir(string sql, Action<MySqlCommand> preencherParametros)
        {
            using (var conexao = Conexao.CriarConexao())
            {
                conexao.Open();

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    preencherParametros(comando);
                    comando.ExecuteNonQuery();
                }

                using (var comandoId = new MySqlCommand("SELECT LAST_INSERT_ID()", conexao))
                {
                    return Convert.ToInt32(comandoId.ExecuteScalar());
                }
            }
        }

        // Versao do INSERT que participa de uma transacao ja aberta.
        internal static int Inserir(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            string sql,
            Action<MySqlCommand> preencherParametros)
        {
            using (var comando = new MySqlCommand(sql, conexao, transacao))
            {
                preencherParametros(comando);
                comando.ExecuteNonQuery();
            }

            using (var comandoId = new MySqlCommand("SELECT LAST_INSERT_ID()", conexao, transacao))
            {
                return Convert.ToInt32(comandoId.ExecuteScalar());
            }
        }

        // Executa INSERT, UPDATE ou DELETE e devolve o numero de linhas afetadas.
        internal static int Executar(string sql, Action<MySqlCommand> preencherParametros)
        {
            using (var conexao = Conexao.CriarConexao())
            {
                conexao.Open();

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    if (preencherParametros != null)
                    {
                        preencherParametros(comando);
                    }

                    return comando.ExecuteNonQuery();
                }
            }
        }

        // Versao que executa o comando dentro de uma transacao ja aberta.
        internal static int Executar(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            string sql,
            Action<MySqlCommand> preencherParametros)
        {
            using (var comando = new MySqlCommand(sql, conexao, transacao))
            {
                preencherParametros(comando);
                return comando.ExecuteNonQuery();
            }
        }

        // Associa um valor a um parametro do SQL. null e enviado como NULL.
        internal static void AdicionarParametro(MySqlCommand comando, string nome, object valor)
        {
            object valorParametro = valor;

            if (valorParametro == null)
            {
                valorParametro = DBNull.Value;
            }

            comando.Parameters.AddWithValue(nome, valorParametro);
        }

        internal static void AdicionarTextoObrigatorio(
            MySqlCommand comando,
            string nome,
            string valor)
        {
            if (valor == null)
            {
                throw new ArgumentNullException(nome);
            }

            comando.Parameters.AddWithValue(nome, valor);
        }

        // Texto vazio ou composto apenas por espacos e enviado como NULL.
        internal static void AdicionarTextoOpcional(
            MySqlCommand comando,
            string nome,
            string valor)
        {
            object valorParametro = valor;

            if (string.IsNullOrWhiteSpace(valor))
            {
                valorParametro = DBNull.Value;
            }

            comando.Parameters.AddWithValue(nome, valorParametro);
        }

        internal static string LerTextoOpcional(MySqlDataReader leitor, string coluna)
        {
            int indiceColuna = leitor.GetOrdinal(coluna);

            if (leitor.IsDBNull(indiceColuna))
            {
                return null;
            }

            // MySql.Data pode materializar CHAR(36) como Guid, inclusive chaves de operação.
            return Convert.ToString(leitor.GetValue(indiceColuna));
        }

        internal static int? LerInteiroOpcional(MySqlDataReader leitor, string coluna)
        {
            int indiceColuna = leitor.GetOrdinal(coluna);

            if (leitor.IsDBNull(indiceColuna))
            {
                return null;
            }

            return leitor.GetInt32(indiceColuna);
        }

        internal static decimal? LerDecimalOpcional(MySqlDataReader leitor, string coluna)
        {
            int indiceColuna = leitor.GetOrdinal(coluna);

            if (leitor.IsDBNull(indiceColuna))
            {
                return null;
            }

            return leitor.GetDecimal(indiceColuna);
        }

        internal static DateTime? LerDataHoraOpcional(MySqlDataReader leitor, string coluna)
        {
            int indiceColuna = leitor.GetOrdinal(coluna);

            if (leitor.IsDBNull(indiceColuna))
            {
                return null;
            }

            return leitor.GetDateTime(indiceColuna);
        }

        internal static byte[] LerBytesOpcional(MySqlDataReader leitor, string coluna)
        {
            int indiceColuna = leitor.GetOrdinal(coluna);

            if (leitor.IsDBNull(indiceColuna))
            {
                return null;
            }

            return (byte[])leitor.GetValue(indiceColuna);
        }
    }
}
