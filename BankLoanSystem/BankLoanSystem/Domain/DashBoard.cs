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
    public class DashBoard
    {
        /// <summary>
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// Retrive User Level by userid
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns></returns>
        public int GetUserLevelByUserId(int userId)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetUserLevelByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlParameter returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        int userLevelId = (int)returnParameter.Value;

                        return userLevelId;
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