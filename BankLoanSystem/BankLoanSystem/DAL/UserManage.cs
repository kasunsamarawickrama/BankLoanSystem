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
    public class UserManage
    {
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:13/1/2016
        /// Description:This method is created for retrieving user_id,user_name,password of system users
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
                            user.userId = int.Parse(reader["user_id"].ToString());
                            user.userName = reader["user_name"].ToString();
                            
                            user.createdBy = int.Parse(reader["created_by"].ToString());
                            user.createdByRole = getUserRole(user.createdBy);
                            if (user.createdByRole == userRole)
                            {
                                user.isEdit = false;
                            }
                            else
                            {
                                user.isEdit = true;
                            }
                            user.createdName = getUserNameById(user.createdBy);
                            user.roleId = int.Parse(reader["role_id"].ToString());
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
        ///  CreatedDate:13/1/2016
        /// Description:This method is created for retrieving user details of selected users
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
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

                            dtl.first_name = reader["first_name"].ToString();
                            dtl.last_name = reader["last_name"].ToString();
                            dtl.email = reader["email"].ToString();
                            dtl.phone_no = reader["phone_no"].ToString();
                            dtl.status = reader["status"].ToString();
                            dtl.created_date = reader["created_date"].ToString();
                            dtl.modified_date = reader["modified_date"].ToString();
                            dtl.is_delete = bool.Parse(reader["is_delete"].ToString());
                            dtl.role_name = reader["role_name"].ToString();
                            dtl.branch_name = reader["branch_name"].ToString();

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