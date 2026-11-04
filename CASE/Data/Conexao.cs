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

            return new MySqlConnection(configuracao.ConnectionString);
        }
    }   
}
