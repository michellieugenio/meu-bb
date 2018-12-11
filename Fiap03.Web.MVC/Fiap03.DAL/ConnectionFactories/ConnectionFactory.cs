using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap03.DAL.ConnectionFactories
{
    //Responsável por criar as conexões com o banco de dados
    public class ConnectionFactory
    {
        private static string _connectionString = ConfigurationManager.ConnectionStrings["DBCarros"].ConnectionString;
        public static IDbConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }



    }
}
