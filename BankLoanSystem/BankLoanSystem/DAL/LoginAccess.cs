using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using BankLoanSystem.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace BankLoanSystem.DAL
{
    public class LoginAccess
    {
        public int CheckUserLogin(string username, string password)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spUserLogin", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = username;
                        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlParameter returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        int userId = (int)returnParameter.Value;

                        return userId;
                    }
                }


                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public int CheckEmployeeLogin(string username, string password)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spEmployeeLogin", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = username;
                        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlParameter returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        int userId = (int)returnParameter.Value;

                        return userId;
                    }
                }


                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}