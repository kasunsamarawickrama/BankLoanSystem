using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class UserRightsAccess
    {
        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/16
        /// 
        /// Get all rights in database
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Right list</returns>
        /// 
        public List<Right> getRights()
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetRights", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        List<Right> RightsLists = new List<Right>();

                        while (reader.Read())
                        {
                            Right right = new Right();
                            right.rightId = int.Parse(reader["id"].ToString());
                            right.active = false;
                            right.description = reader["description"].ToString();

                            RightsLists.Add(right);

                        }
                        return RightsLists;

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
        /// CreatedDate: 2016/01/17
        /// 
        /// Get user permission permission string which contain rightId's
        /// </summary>
        /// <param name="userId"> Profile edit users id</param>
        /// <returns>Right List,  but first Right contain the string, If List have more than 1 value it is going to be an unAuthorize one</returns>
        /// 
        public List<Right> getRightsString(int userId)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetRightsStringByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        List<Right> RightsLists = new List<Right>();

                        while (reader.Read())
                        {
                            Right right = new Right();
                            right.userId = int.Parse(reader["user_id"].ToString());
                            right.rightsPermissionString = reader["rights_id"].ToString();

                            RightsLists.Add(right);

                        }
                        return RightsLists;

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
        /// CreatedDate: 2016/01/17
        /// 
        /// Rew Right string Updating
        /// </summary>
        /// <param name="returnRight"></param>
        /// <param name="writerId"></param>
        /// <returns>boolian value</returns>
        /// 
        public bool postNewRights(Right returnRight)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spUpdateRightsStringByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@editor_id", SqlDbType.Int).Value = returnRight.userId;
                        cmd.Parameters.Add("@permission", SqlDbType.VarChar).Value = returnRight.rightsPermissionString;
                        cmd.Parameters.Add("@modified_user", SqlDbType.Int).Value = returnRight.editorId;
                        cmd.Parameters.Add("@DateNow", SqlDbType.DateTime).Value = DateTime.Now;
                        
                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        int checkUpdate = (int)returnParameter.Value;
                        if (checkUpdate == 1)
                        {
                            return true;
                        }
                        else {
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