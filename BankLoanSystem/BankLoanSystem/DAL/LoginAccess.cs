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
using static System.String;


namespace BankLoanSystem.DAL
{
    public class LoginAccess
    {
        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// user login authentication
        /// 
        /// UpdatedBy : Asanka
        /// UpdatedDate: 2016/03/04
        /// removed existing connection open method and set parameter's to object list and pass stored procedure name to
        /// call DataHandler class to save user object
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// 
        /// <returns>userid</returns>
        public DataSet CheckUserLogin(User user)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            
            paramertList.Add(new object[] { "@userName", user.UserName });
            try
            {
                return dataHandler.GetDataSet("spUserLogin", paramertList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/14
        /// EditedBy  : Kanishka Mahanama
        /// EditedDate: 06
        /// 
        /// check employee authentication
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>employeeid</returns>
        public int CheckEmployeeLogin(string username, string password)
        {
            int idOut = 0;
            string passwordOut = "";

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@userName", username });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spEmployeeLogin", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        idOut = Convert.ToInt32(dataRow["system_admin_id"]);
                        passwordOut = dataRow["password"].ToString();
                        break;
                    }

                    if (CompareOrdinal(password, passwordOut) == 0)
                    {
                        return idOut;
                    }
                    
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            //{
            //    try
            //    {
            //        using (SqlCommand cmd = new SqlCommand("spEmployeeLogin", con))
            //        {
            //            cmd.CommandType = CommandType.StoredProcedure;

            //            cmd.Parameters.Add("@userName", SqlDbType.VarChar).Value = username;
                        

            //            con.Open();
            //            cmd.ExecuteNonQuery();

            //            int idOut = 0;
            //            string passwordOut = "";
            //            var reader = cmd.ExecuteReader();
            //            if (reader.Read())
            //            {
            //                idOut = Convert.ToInt32(reader["system_admin_id"]);
            //                passwordOut = reader["password"].ToString();
            //            }


            //            int idFromDB = (int)idOut;
            //            string passwordFromDB = (string)passwordOut;

            //            //char[] delimiter = { ':' };

            //            //string[] split = passwordFromDB.Split(delimiter);

            //            //string passwordEncripted = PasswordEncryption.encryptPassword(password,split[1]);

            //            if (string.Compare(password, passwordFromDB) == 0)
            //            {
            //                return idFromDB;
            //            }
            //            else {
            //                return -1;
            //            }

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}
        }

        public DataSet GetDealerUserCompanyBranch(int userId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@user_id", userId });
            try
            {
                return dataHandler.GetDataSet("spGetDealerUserCompanyBranch", paramertList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}