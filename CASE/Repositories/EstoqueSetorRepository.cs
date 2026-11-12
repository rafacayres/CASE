using System;
using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class EstoqueSetorRepository
    {

        public List<EstoqueSetor> Listar()
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_setor,id_local_estoque,id_evento FROM estoquesetor ORDER BY id_setor",
                ConverterLinha
            );
        }

        public EstoqueSetor BuscarPorSetor(int idSetor)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT id_setor,id_local_estoque,id_evento FROM estoquesetor WHERE id_setor=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idSetor)
            );
        }

        public void Associar(EstoqueSetor item)
        {
            using (var conexao = CASE.Data.Conexao.CriarConexao())
            {
                conexao.Open();

                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        ValidarLocalDeBarraca(conexao, transacao, item);
                        AcessoDados.Executar(
                            conexao,
                            transacao,
                            "INSERT INTO estoquesetor (id_setor,id_local_estoque,id_evento) " +
                            "VALUES (@setor,@local,@evento)",
                            comando => Vincular(comando, item));
                        transacao.Commit();
                    }
                    catch
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool AtualizarLocal(EstoqueSetor item)
        {
            using (var conexao = CASE.Data.Conexao.CriarConexao())
            {
                conexao.Open();

                using (var transacao = conexao.BeginTransaction())
                {
                    try
                    {
                        ValidarLocalDeBarraca(conexao, transacao, item);
                        int linhasAfetadas = AcessoDados.Executar(
                            conexao,
                            transacao,
                            "UPDATE estoquesetor SET id_local_estoque=@local,id_evento=@evento " +
                            "WHERE id_setor=@setor",
                            comando => Vincular(comando, item));
                        transacao.Commit();
                        return linhasAfetadas > 0;
                    }
                    catch
                    {
                        transacao.Rollback();
                        throw;
                    }
                }
            }

        }

        private static void Vincular(MySqlCommand comando, EstoqueSetor item)
        {
            AcessoDados.AdicionarParametro(comando, "@setor", item.IdSetor);
            AcessoDados.AdicionarParametro(comando, "@local", item.IdLocalEstoque);
            AcessoDados.AdicionarParametro(comando, "@evento", item.IdEvento);
        }

        private static void ValidarLocalDeBarraca(
            MySqlConnection conexao,
            MySqlTransaction transacao,
            EstoqueSetor item)
        {
            if (item == null || item.IdSetor <= 0 || item.IdLocalEstoque <= 0 || item.IdEvento <= 0)
            {
                throw new ArgumentException("Os dados da associação do estoque são inválidos.", nameof(item));
            }

            using (MySqlCommand comando = new MySqlCommand(
                "SELECT s.id_setor FROM setor s " +
                "JOIN evento ev ON ev.id_evento=s.id_evento " +
                "JOIN localestoque le ON le.id_local_estoque=@local " +
                "WHERE s.id_setor=@setor AND s.id_evento=@evento " +
                "AND ev.id_evento=@evento AND ev.id_instituicao=le.id_instituicao " +
                "AND le.id_evento=@evento AND le.eh_central=0 " +
                "AND le.eh_geral_evento=0 AND le.ativo=1 FOR UPDATE",
                conexao,
                transacao))
            {
                AcessoDados.AdicionarParametro(comando, "@setor", item.IdSetor);
                AcessoDados.AdicionarParametro(comando, "@local", item.IdLocalEstoque);
                AcessoDados.AdicionarParametro(comando, "@evento", item.IdEvento);

                if (comando.ExecuteScalar() == null)
                {
                    throw new InvalidOperationException(
                        "O estoque associado precisa ser uma barraca ativa do mesmo evento e instituição.");
                }
            }
        }

        private static EstoqueSetor ConverterLinha(MySqlDataReader leitor)
        {
            return new EstoqueSetor
            {
                IdSetor = leitor.GetInt32("id_setor"),
                IdLocalEstoque = leitor.GetInt32("id_local_estoque"),
                IdEvento = leitor.GetInt32("id_evento")
            };
        }
    }
}

