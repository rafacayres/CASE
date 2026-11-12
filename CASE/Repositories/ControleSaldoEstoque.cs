using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    // Atualiza os saldos dentro da transacao da operacao que os originou.
    internal static class ControleSaldoEstoque
    {
        internal static void AplicarAjustes(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            IList<Tuple<int, int, decimal>> ajustes)
        {
            Dictionary<Tuple<int, int>, decimal> saldoPorChave =
                new Dictionary<Tuple<int, int>, decimal>();

            foreach (Tuple<int, int, decimal> ajuste in ajustes)
            {
                if (ajuste == null || ajuste.Item1 <= 0 || ajuste.Item2 <= 0)
                {
                    throw new ArgumentException("O ajuste de estoque possui identificadores inválidos.");
                }

                Tuple<int, int> chave = Tuple.Create(ajuste.Item1, ajuste.Item2);
                decimal saldoSomado;

                if (saldoPorChave.TryGetValue(chave, out saldoSomado))
                {
                    saldoPorChave[chave] = saldoSomado + ajuste.Item3;
                }
                else
                {
                    saldoPorChave.Add(chave, ajuste.Item3);
                }
            }

            List<Tuple<int, int>> chaves = new List<Tuple<int, int>>();

            foreach (KeyValuePair<Tuple<int, int>, decimal> item in saldoPorChave)
            {
                if (item.Value != 0)
                {
                    chaves.Add(item.Key);
                }
            }

            chaves.Sort(CompararChaves);

            Dictionary<Tuple<int, int>, Tuple<int, decimal>> saldosNovos =
                new Dictionary<Tuple<int, int>, Tuple<int, decimal>>();

            foreach (Tuple<int, int> chave in chaves)
            {
                CriarLinhaSeNecessario(conexao, transacao, chave.Item1, chave.Item2);

                int idEstoque;
                decimal saldoAtual;
                LerSaldoComBloqueio(
                    conexao,
                    transacao,
                    chave.Item1,
                    chave.Item2,
                    out idEstoque,
                    out saldoAtual);

                decimal saldoNovo = saldoAtual + saldoPorChave[chave];

                if (saldoNovo < 0)
                {
                    throw new InvalidOperationException(
                        "A operação deixaria o estoque com saldo negativo.");
                }

                if (saldoNovo != decimal.Truncate(saldoNovo))
                {
                    throw new InvalidOperationException(
                        "O saldo de estoque deve permanecer em unidades inteiras.");
                }

                saldosNovos.Add(chave, Tuple.Create(idEstoque, saldoNovo));
            }

            foreach (KeyValuePair<Tuple<int, int>, Tuple<int, decimal>> item in saldosNovos)
            {
                using (MySqlCommand comando = new MySqlCommand(
                    "UPDATE estoque SET quantidade_atual=@quantidade WHERE id_estoque=@id",
                    conexao,
                    transacao))
                {
                    comando.Parameters.AddWithValue("@quantidade", item.Value.Item2);
                    comando.Parameters.AddWithValue("@id", item.Value.Item1);

                    if (comando.ExecuteNonQuery() != 1)
                    {
                        throw new InvalidOperationException(
                            "Não foi possível atualizar o saldo do produto.");
                    }
                }
            }
        }

        private static void CriarLinhaSeNecessario(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idLocal,
            int idProduto)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "INSERT INTO estoque (id_local_estoque,id_produto,quantidade_minima,quantidade_ideal,quantidade_atual) " +
                "VALUES (@local,@produto,0,0,0) " +
                "ON DUPLICATE KEY UPDATE id_estoque=id_estoque",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@local", idLocal);
                comando.Parameters.AddWithValue("@produto", idProduto);
                comando.ExecuteNonQuery();
            }
        }

        private static void LerSaldoComBloqueio(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            int idLocal,
            int idProduto,
            out int idEstoque,
            out decimal saldoAtual)
        {
            using (MySqlCommand comando = new MySqlCommand(
                "SELECT id_estoque,quantidade_atual FROM estoque " +
                "WHERE id_local_estoque=@local AND id_produto=@produto FOR UPDATE",
                conexao,
                transacao))
            {
                comando.Parameters.AddWithValue("@local", idLocal);
                comando.Parameters.AddWithValue("@produto", idProduto);

                using (MySqlDataReader leitor = comando.ExecuteReader())
                {
                    if (!leitor.Read())
                    {
                        throw new InvalidOperationException(
                            "Não foi possível localizar o saldo do produto no local.");
                    }

                    idEstoque = leitor.GetInt32("id_estoque");
                    saldoAtual = leitor.GetDecimal("quantidade_atual");
                }
            }
        }

        private static int CompararChaves(Tuple<int, int> primeira, Tuple<int, int> segunda)
        {
            int comparacaoLocal = primeira.Item1.CompareTo(segunda.Item1);

            if (comparacaoLocal != 0)
            {
                return comparacaoLocal;
            }

            return primeira.Item2.CompareTo(segunda.Item2);
        }
    }
}
