using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class LocalEstoqueRepository
    {
        public int GarantirCentral(int idInstituicao)
        {
            using (var conexao = CASE.Data.Conexao.CriarConexao())
            {
                conexao.Open();
                using (var transacao = conexao.BeginTransaction())
                {
                    using (var comando = new MySqlCommand(
                        "SELECT id_instituicao FROM instituicao WHERE id_instituicao=@id FOR UPDATE", conexao, transacao))
                    {
                        comando.Parameters.AddWithValue("@id", idInstituicao);
                        if (comando.ExecuteScalar() == null)
                        {
                            throw new System.InvalidOperationException("Cadastre a instituição primeiro.");
                        }
                    }
                    int idCentral = 0;
                    using (var comando = new MySqlCommand(
                        "SELECT id_local_estoque,ativo FROM localestoque WHERE id_instituicao=@id AND eh_central=1 FOR UPDATE", conexao, transacao))
                    {
                        comando.Parameters.AddWithValue("@id", idInstituicao);
                        using (var leitor = comando.ExecuteReader())
                        {
                            while (leitor.Read())
                            {
                                if (idCentral > 0 || !leitor.GetBoolean("ativo"))
                                {
                                    throw new System.InvalidOperationException("O estoque central precisa ser revisado: duplicado ou inativo.");
                                }
                                idCentral = leitor.GetInt32("id_local_estoque");
                            }
                        }
                    }
                    if (idCentral == 0)
                    {
                        using (var comando = new MySqlCommand(
                            "INSERT INTO localestoque(id_instituicao,nome,ativo,eh_central,eh_geral_evento,id_evento) " +
                            "VALUES (@id,'Estoque central',1,1,0,NULL)", conexao, transacao))
                        {
                            comando.Parameters.AddWithValue("@id", idInstituicao);
                            comando.ExecuteNonQuery();
                            idCentral = checked((int)comando.LastInsertedId);
                        }
                    }
                    transacao.Commit();
                    return idCentral;
                }
            }
        }

        public List<LocalEstoque> Listar()
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_local_estoque,id_instituicao,id_evento,nome,ativo,eh_central,eh_geral_evento FROM localestoque ORDER BY id_local_estoque",
                ConverterLinha
            );
        }

        public List<LocalEstoque> ListarCentrais(int idInstituicao)
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_local_estoque,id_instituicao,id_evento,nome,ativo,eh_central,eh_geral_evento FROM localestoque WHERE id_instituicao=@instituicao AND eh_central=1 ORDER BY id_local_estoque",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@instituicao", idInstituicao)
            );
        }

        public List<LocalEstoque> ListarPorEvento(int idEvento)
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_local_estoque,id_instituicao,id_evento,nome,ativo,eh_central,eh_geral_evento " +
                "FROM localestoque WHERE id_evento=@evento ORDER BY id_local_estoque",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@evento", idEvento)
            );
        }

        public List<LocalEstoque> ListarGeraisPorEvento(int idEvento)
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_local_estoque,id_instituicao,id_evento,nome,ativo,eh_central,eh_geral_evento " +
                "FROM localestoque WHERE id_evento=@evento AND eh_geral_evento=1 " +
                "ORDER BY id_local_estoque",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@evento", idEvento)
            );
        }

        public LocalEstoque BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT id_local_estoque,id_instituicao,id_evento,nome,ativo,eh_central,eh_geral_evento FROM localestoque WHERE id_local_estoque=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public int Cadastrar(LocalEstoque item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO localestoque (id_instituicao,id_evento,nome,ativo,eh_central,eh_geral_evento) VALUES (@instituicao,@evento,@nome,@ativo,@central,@geralEvento)",
                comando => Vincular(comando, item)
            );
        }

        public bool Atualizar(LocalEstoque item)
        {
            return AcessoDados.Executar(
                "UPDATE localestoque SET id_instituicao=@instituicao,id_evento=@evento,nome=@nome,ativo=@ativo,eh_central=@central,eh_geral_evento=@geralEvento WHERE id_local_estoque=@id",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@id", item.IdLocalEstoque);
                    Vincular(comando, item);
                }) > 0;

        }

        private static void Vincular(MySqlCommand comando, LocalEstoque item)
        {
            AcessoDados.AdicionarParametro(comando, "@instituicao", item.IdInstituicao);
            AcessoDados.AdicionarParametro(comando, "@evento", item.IdEvento);
            AcessoDados.AdicionarTextoObrigatorio(comando, "@nome", item.Nome);
            AcessoDados.AdicionarParametro(comando, "@ativo", item.Ativo);
            AcessoDados.AdicionarParametro(comando, "@central", item.EhCentral);
            AcessoDados.AdicionarParametro(comando, "@geralEvento", item.EhGeralEvento);
        }

        private static LocalEstoque ConverterLinha(MySqlDataReader leitor)
        {
            return new LocalEstoque
            {
                IdLocalEstoque = leitor.GetInt32("id_local_estoque"),
                IdInstituicao = leitor.GetInt32("id_instituicao"),
                IdEvento = AcessoDados.LerInteiroOpcional(leitor, "id_evento"),
                Nome = leitor.GetString("nome"),
                Ativo = leitor.GetBoolean("ativo"),
                EhCentral = leitor.GetBoolean("eh_central"),
                EhGeralEvento = leitor.GetBoolean("eh_geral_evento")
            };
        }
    }
}
