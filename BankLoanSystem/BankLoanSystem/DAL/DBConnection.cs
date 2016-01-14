using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace BankLoanSystem.DAL
{
    public class DBConnection
    {
        public SqlConnection connection;
        public DBConnection() {

            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString);
            
            

        }

        public SqlConnection CreateConnection() {

            try
            {
                connection.Open();
            }
            catch (Exception)
            {
                Console.WriteLine("Connection failed");
            }
            return connection;
        }

        public void CloseConnection()
        {
            connection.Close();
        }
    }
}