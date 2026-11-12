using System.Collections.Generic;
using CASE.Models;
using MySql.Data.MySqlClient;

namespace CASE.Repositories
{
    public class AnaliseUsuarioRepository
    {

        public List<AnaliseUsuario> ListarPorUsuario(int idUsuario)
        {
            return AcessoDados.ConsultarLista(
                "SELECT id_analise,id_usuario,id_analisador,decisao,data_hora,justificativa FROM analiseusuario WHERE id_usuario=@id ORDER BY data_hora,id_analise",
                ConverterLinha,
                comando => AcessoDados.AdicionarParametro(comando, "@id", idUsuario)
            );
        }

        public int Registrar(AnaliseUsuario analise)
        {
            return AcessoDados.Inserir(
                "INSERT INTO analiseusuario (id_usuario,id_analisador,decisao,data_hora,justificativa) VALUES (@usuario,@analisador,@decisao,@data,@justificativa)",
                comando =>
                {
                    AcessoDados.AdicionarParametro(comando, "@usuario", analise.IdUsuario);
                    AcessoDados.AdicionarParametro(comando, "@analisador", analise.IdAnalisador);
                    AcessoDados.AdicionarTextoObrigatorio(comando, "@decisao", analise.Decisao);
                    AcessoDados.AdicionarParametro(comando, "@data", analise.DataHora);
                    AcessoDados.AdicionarTextoOpcional(comando, "@justificativa", analise.Justificativa);
                });

        }

        private static AnaliseUsuario ConverterLinha(MySqlDataReader leitor)
        {
            return new AnaliseUsuario
            {
                IdAnalise = leitor.GetInt32("id_analise"),
                IdUsuario = leitor.GetInt32("id_usuario"),
                IdAnalisador = leitor.GetInt32("id_analisador"),
                Decisao = leitor.GetString("decisao"),
                DataHora = leitor.GetDateTime("data_hora"),
                Justificativa = AcessoDados.LerTextoOpcional(leitor, "justificativa")
            };
        }
    }
}
