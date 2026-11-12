using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class DespesaRepository
    {

        private const string Colunas = "id_despesa,id_evento,id_fornecedor,descricao,`data`,valor,id_usuario_responsavel";

        public List<Despesa> ListarPorEvento(int idEvento)
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas + " FROM despesa WHERE id_evento=@id ORDER BY `data` DESC,id_despesa DESC",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idEvento)
            );
        }

        public Despesa BuscarPorId(int id)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT " + Colunas + " FROM despesa WHERE id_despesa=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", id)
            );
        }

        public int Cadastrar(Despesa item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO despesa (id_evento,id_fornecedor,descricao,`data`,valor,id_usuario_responsavel) VALUES (@evento,@fornecedor,@descricao,@data,@valor,@usuario)",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@evento", item.IdEvento);
                    AcessoDados.AdicionarParametro(comando, "@fornecedor", item.IdFornecedor);
                    AcessoDados.AdicionarTextoObrigatorio(comando, "@descricao", item.Descricao);
                    AcessoDados.AdicionarParametro(comando, "@data", item.Data);
                    AcessoDados.AdicionarParametro(comando, "@valor", item.Valor);
                    AcessoDados.AdicionarParametro(comando, "@usuario", item.IdUsuarioResponsavel);
                });

        }

        private static Despesa ConverterLinha(MySqlDataReader leitor)
        {
            return new Despesa
            {
                IdDespesa = leitor.GetInt32("id_despesa"),
                IdEvento = leitor.GetInt32("id_evento"),
                IdFornecedor = AcessoDados.LerInteiroOpcional(leitor, "id_fornecedor"),
                Descricao = leitor.GetString("descricao"),
                Data = leitor.GetDateTime("data"),
                Valor = leitor.GetDecimal("valor"),
                IdUsuarioResponsavel = leitor.GetInt32("id_usuario_responsavel")
            };
        }
    }
}
