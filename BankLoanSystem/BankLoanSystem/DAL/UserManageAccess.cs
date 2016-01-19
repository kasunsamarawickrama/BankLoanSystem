using BankLoanLocal.Models;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BankLoanSystem.DAL
{
    public class UserManageAccess
    {
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/13
        /// Retrieve user_name and created person of users
        /// </summary>
        /// <param name="userType"></param>
        /// <returns>userLogin object</returns>
        public List<UserLogin> getUserByType(int levelId,int userId)
        {


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    int userRole = getUserRole(userId);
                    using (SqlCommand cmd = new SqlCommand("spGetUserLoginDetailsByType", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@level_id", SqlDbType.Int).Value = levelId;
                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;


                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        List<UserLogin> UserList = new List<UserLogin>();
                        UserLogin user;
                        
                        while (reader.Read())
                        {
                            user = new UserLogin();
                            user.loginId = userId;
                            user.userId = int.Parse(reader["user_id"].ToString());
                            user.userName = reader["user_name"].ToString();
                            
                            user.createdBy = int.Parse(reader["created_by"].ToString());
                            user.createdByRole = getUserRole(user.createdBy);
                            
                            user.createdName = getUserNameById(user.createdBy);
                            user.roleId = int.Parse(reader["role_id"].ToString());
                            
                             if ((userRole == 1) && (levelId == 1) && (user.createdBy != userId))
                            {
                                user.isEdit = false;
                            }
                            else if ((userRole == 1) && (levelId == 1) && (user.createdBy == userId))
                            {
                                user.isEdit = true;
                            }
                            else if ((userRole == 1) && (levelId == 2))
                            {
                                user.isEdit = true;
                            }
                            else if ((userRole == 2) && (levelId==1))
                            {
                                user.isEdit = false;
                            }
                            else if ((userRole == 2) && (levelId == 2)&&(user.createdBy!=userId))
                            {
                                user.isEdit = false;
                            }
                            else if ((userRole == 2) && (levelId == 2) && (user.createdBy == userId))
                            {
                                user.isEdit = true;
                            }
                            else if (((userRole == 1) && (levelId == 3))||((userRole == 2) && (levelId == 3)))
                            {
                                user.isEdit = true;
                            }
                            
                            UserList.Add(user);
                        }

                        return UserList;
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
        /// CreatedBy: Piyumi
        /// CreatedDate:2016/1/18/
        /// Delete user details of a selected user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true/false</returns>
        public bool deleteUser(int id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spDeleteUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = id;
                        
                        con.Open();
                        
                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);


                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        cmd.ExecuteNonQuery();
                        int countVal = (int)returnParameter.Value;

                        if (countVal == 1)
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
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/17
        /// Get userName by userId
        /// </summary>
        /// <param name="createdBy"></param>
        /// <returns>userName</returns>
        public string getUserNameById(int createdBy)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetUserNameByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = createdBy;


                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        UserLogin dtl = new UserLogin();

                        while (reader.Read())
                        {

                            dtl.userName = reader["user_name"].ToString();
                           

                        }

                        return dtl.userName;
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
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/13
        /// Get user details of a given user
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns>GetDetails Object</returns>
        public GetDetails getUserById(int user_id)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetUserDetailsById", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = user_id;


                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        GetDetails dtl = new GetDetails();

                        while (reader.Read())
                        {
                            dtl.userId= int.Parse(reader["user_id"].ToString());
                            dtl.firstName = reader["first_name"].ToString();
                            dtl.lastName = reader["last_name"].ToString();
                            dtl.email = reader["email"].ToString();
                            dtl.phoneNo = reader["phone_no"].ToString();
                            if (bool.Parse(reader["status"].ToString())) {

                                dtl.status = "Active";
                            }
                            else
                            {
                                dtl.status = "Not Active";
                            }
                            DateTime day = DateTime.Parse(reader["created_date"].ToString());
                            dtl.createdDate = day.ToShortDateString();
                            
                            dtl.roleName = reader["role_name"].ToString();
                            dtl.branchName = reader["branch_name"].ToString();

                        }

                        return dtl;
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
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/17
        /// Get User Role By userId 
        /// </summary>
        /// <param name="idval"></param>
        /// <returns>role</returns>


        public int getUserRole(int idval)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetUserRole", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = idval;


                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        int role = 0;

                        while (reader.Read())
                        {

                            role = int.Parse(reader["role_id"].ToString());

                        }

                        return role;
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