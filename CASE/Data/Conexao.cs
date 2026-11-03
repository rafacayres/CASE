using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace CASE.Data
{
    public static class Conexao
    {
        public static MySqlConnection CriarConexao()
        {
            var configuracao = ConfigurationManager
                .ConnectionStrings["CaseDb"];

            if (configuracao == null)
            {
                throw new InvalidOperationException(
                    "A conexão CaseDb não foi encontrada no App.config.");
            }

            string senha = Environment.GetEnvironmentVariable(
                "CASE_DB_PASSWORD");

            if (string.IsNullOrWhiteSpace(senha))
            {
                throw new InvalidOperationException(
                    "A variável CASE_DB_PASSWORD não foi configurada.");
            }

            var builder = new MySqlConnectionStringBuilder(
                configuracao.ConnectionString);

            builder.Password = senha;

            return new MySqlConnection(builder.ConnectionString);
        }
    }   
}
