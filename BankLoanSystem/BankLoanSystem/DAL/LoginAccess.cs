using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using BankLoanSystem.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using BankLoanSystem.Code;


namespace BankLoanSystem.DAL
{
    public class LoginAccess
    {
        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// user login authentication
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// 
        /// <returns>userid</returns>
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


                        con.Open();
                        cmd.ExecuteNonQuery();
                        //SqlParameter idOut = cmd.Parameters.Add("@IdOut", SqlDbType.Int);
                        //SqlParameter passwordOut = cmd.Parameters.Add("@PasswordOut", SqlDbType.VarChar);
                        //idOut.Direction = ParameterDirection.ReturnValue;
                        //passwordOut.Direction = ParameterDirection.ReturnValue;
                        //cmd.ExecuteNonQuery();
                        int idOut = 0;
                        string passwordOut = "";
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            idOut = Convert.ToInt32(reader["user_id"]);
                            passwordOut = reader["password"].ToString();
                        }


                        int idFromDB = (int)idOut;
                        string passwordFromDB = (string)passwordOut;

                        char[] delimiter = { ':' };

                        string[] split = passwordFromDB.Split(delimiter);

                        var checkCharHave = passwordFromDB.ToLowerInvariant().Contains(':');

                        if (passwordFromDB == null ||( checkCharHave == false)) {
                            return -1;
                        }

                        string passwordEncripted = PasswordEncryption.encryptPassword(password, split[1]);

                        if (string.Compare(passwordEncripted, passwordOut) == 0)
                        {
                            return idFromDB;
                        }
                        else {
                            return -1;
                        }
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
        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// check employee authentication
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>employeeid</returns>
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
                        

                        con.Open();
                        cmd.ExecuteNonQuery();
                        //SqlParameter idOut = cmd.Parameters.Add("@IdOut", SqlDbType.Int);
                        //SqlParameter passwordOut = cmd.Parameters.Add("@PasswordOut", SqlDbType.VarChar);
                        //idOut.Direction = ParameterDirection.ReturnValue;
                        //passwordOut.Direction = ParameterDirection.ReturnValue;
                        //cmd.ExecuteNonQuery();
                        int idOut = 0;
                        string passwordOut = "";
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            idOut = Convert.ToInt32(reader["system_admin_id"]);
                            passwordOut = reader["password"].ToString();
                        }


                        int idFromDB = (int)idOut;
                        string passwordFromDB = (string)passwordOut;

                        //char[] delimiter = { ':' };

                        //string[] split = passwordFromDB.Split(delimiter);

                        //string passwordEncripted = PasswordEncryption.encryptPassword(password,split[1]);

                        if (string.Compare(password, password) == 0)
                        {
                            return idFromDB;
                        }
                        else {
                            return -1;
                        }

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