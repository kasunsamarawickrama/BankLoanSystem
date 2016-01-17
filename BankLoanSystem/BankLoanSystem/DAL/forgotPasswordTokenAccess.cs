using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class forgotPasswordTokenAccess
    {
        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/17
        /// 
        /// update Token using user id and token
        /// if a user not exists it will insert user and token
        /// 
        /// argument : user_id (int), token : string
        /// 
        /// </summary>
        /// <returns>true : success, false : fail</returns>
        public bool updateToken(int userId, string token)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spUpdateOrInsertToken", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                        cmd.Parameters.Add("@token", SqlDbType.NVarChar).Value = token;
                        cmd.Parameters.Add("@expired_date", SqlDbType.DateTime).Value = DateTime.Now.AddHours(24);


                        con.Open();
                        
                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        cmd.ExecuteNonQuery();
                        int countVal = (int)returnParameter.Value;

                        if (countVal > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;

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
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/17
        /// 
        ///Verify the account using userId and token
        /// 
        /// argument : user_id (int), token : string
        /// 
        /// </summary>
        /// <returns>true : success, false : fail</returns>
        public bool verifyAccount(int userId, string token) {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spverifyAccountBytoken", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                        cmd.Parameters.Add("@token", SqlDbType.NVarChar).Value = token;
                        cmd.Parameters.Add("@expired_date", SqlDbType.DateTime).Value = DateTime.Now;


                        con.Open();
                        
                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        cmd.ExecuteNonQuery();
                        int countVal = (int)returnParameter.Value;

                        if (countVal > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;

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
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/17
        /// 
        ///update the Password
        /// 
        /// argument : user_id (int), resetPasswordModel : ResetPassword
        /// 
        /// </summary>
        /// <returns>true : success, false : fail</returns>
        public bool resetPassword(int userId, ResetPassword resetPasswordModel)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spUpdatePassword", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                        cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = resetPasswordModel.Password;


                        con.Open();

                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        cmd.ExecuteNonQuery();
                        int countVal = (int)returnParameter.Value;

                        if (countVal > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;

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