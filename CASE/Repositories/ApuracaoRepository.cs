using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class ApuracaoRepository
    {
        private const string Colunas =
            "id_apuracao,id_jornada,valor_informado,data_hora,id_usuario_responsavel,observacao,valor_dinheiro,valor_pix,valor_cartao";

        public int Cadastrar(Apuracao item)
        {
            return AcessoDados.Inserir(
                "INSERT INTO apuracao (id_jornada,valor_informado,data_hora,id_usuario_responsavel," +
                "observacao,valor_dinheiro,valor_pix,valor_cartao) " +
                "VALUES (@jornada,@total,@data,@usuario,@observacao,@dinheiro,@pix,@cartao)",
                comando =>
                {
                    comando.Parameters.AddWithValue("@jornada", item.IdJornada);
                    comando.Parameters.AddWithValue("@total", item.ValorInformado);
                    comando.Parameters.AddWithValue("@data", item.DataHora);
                    comando.Parameters.AddWithValue("@usuario", item.IdUsuarioResponsavel);
                    AcessoDados.AdicionarTextoOpcional(comando, "@observacao", item.Observacao);
                    comando.Parameters.AddWithValue("@dinheiro", item.ValorDinheiro);
                    comando.Parameters.AddWithValue("@pix", item.ValorPix);
                    comando.Parameters.AddWithValue("@cartao", item.ValorCartao);
                });
        }

        public List<Apuracao> ListarPorJornada(int idJornada)
        {
            return AcessoDados.ConsultarLista(
                "SELECT " + Colunas + " FROM apuracao WHERE id_jornada=@id ORDER BY data_hora,id_apuracao",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idJornada));
        }

        public Apuracao BuscarPorId(int idApuracao)
        {
            return AcessoDados.ConsultarUnico(
                "SELECT " + Colunas + " FROM apuracao WHERE id_apuracao=@id",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idApuracao));
        }

        private static Apuracao ConverterLinha(MySqlDataReader leitor)
        {
            return new Apuracao
            {
                IdApuracao = leitor.GetInt32("id_apuracao"),
                IdJornada = leitor.GetInt32("id_jornada"),
                ValorInformado = leitor.GetDecimal("valor_informado"),
                ValorDinheiro = AcessoDados.LerDecimalOpcional(leitor, "valor_dinheiro"),
                ValorPix = AcessoDados.LerDecimalOpcional(leitor, "valor_pix"),
                ValorCartao = AcessoDados.LerDecimalOpcional(leitor, "valor_cartao"),
                DataHora = leitor.GetDateTime("data_hora"),
                IdUsuarioResponsavel = leitor.GetInt32("id_usuario_responsavel"),
                Observacao = AcessoDados.LerTextoOpcional(leitor, "observacao")
            };
        }
    }
}
