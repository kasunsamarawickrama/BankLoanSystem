using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace BankLoanSystem.DAL
{
    /// <summary>
    /// CreatedBy : Kasun Smarawickrama
    /// CreatedDate: 2016/01/14
    /// 
    /// DB connection Initialize
    /// </summary>
    public class DBConnection : IDisposable
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

        public void Dispose()
        {
            connection.Close();
        }
    }
}