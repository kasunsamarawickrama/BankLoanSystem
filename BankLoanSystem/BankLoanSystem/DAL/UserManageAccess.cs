using BankLoanLocal.Models;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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
            List<UserLogin> UserList = new List<UserLogin>();
            DataHandler dataHandler = new DataHandler();
            int userRole = getUserRole(userId);
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@level_id", levelId });
            paramertList.Add(new object[] { "@user_id", userId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetUserLoginDetailsByType");
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        UserLogin user = new UserLogin();
                        user.loginId = userId;
                        user.userId = int.Parse(dataRow["user_id"].ToString());
                        user.userName = dataRow["user_name"].ToString();

                        user.createdBy = int.Parse(dataRow["created_by"].ToString());
                        user.createdByRole = getUserRole(user.createdBy);

                        user.createdName = getUserNameById(user.createdBy);
                        user.roleId = int.Parse(dataRow["role_id"].ToString());

                        if (userRole == 1)
                        {
                            user.isEdit = true;
                        }

                        else if ((userRole == 2) && (levelId == 1))
                        {
                            user.isEdit = false;
                        }
                        else if ((userRole == 2) && (levelId == 2))
                        {
                            user.isEdit = true;
                        }
                        else if ((userRole == 2) && (levelId == 3))
                        {
                            user.isEdit = true;
                        }

                        UserList.Add(user);
                    }

                    return UserList;
                }
                else
                {
                    return null;
                }
            }

            catch
            {
                return null;
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
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", id });
           
            try
            {
                return dataHandler.ExecuteSQL("spDeleteUser", paramertList);
            }
            catch
            {
                return false;
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
        /// 
        /// Get user details of a given user
        /// 
        /// EditedBy: Kanishka
        /// EditedDate:2016/2/26
        /// 
        /// Add user company code and branch code
        /// 
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
                            if (reader["branch_name"].ToString() != "")
                            {
                                dtl.branchName = reader["branch_name"].ToString();
                                dtl.BranchId = Convert.ToInt32(reader["branch_id"]);
                                dtl.BranchCode = reader["BranchCode"].ToString();
                            }
                            else
                            {
                                dtl.branchName = "";
                                dtl.BranchId = 0;
                                dtl.BranchCode = "";
                            }
                            dtl.CompanyCode = reader["CompanyCode"].ToString();

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

        /// <summary>
        /// CreatedBy:irfan
        /// CreatedDate:2016/1/20
        /// Get User Role By userId 
        /// </summary>
        /// <param name="idval"></param>
        /// <returns>role name</returns>


        public string getUserRoleName(int idval)
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
                        string role = "";

                        while (reader.Read())
                        {

                            role = reader["role_name"].ToString();

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

        /// <summary>
        /// CreatedBy:Nadeeka
        /// CreatedDate:2016/4/4
        /// Retrieve user detaild by attached branch id and role id
        /// </summary>
        /// <param name="roleId"></param>
        /// /// <param name="nonRegBranchId"></param>
        /// <returns>user object</returns>
        public List<User> getUsersByRoleBranch(int roleId, int branchId)
        {
            List<User> UserList = new List<User>();
            DataHandler dataHandler = new DataHandler();

            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@branch_id", branchId });
            paramertList.Add(new object[] { "@role_id", roleId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetUserDetailsByRoleBranch", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        User user = new User();

                        user.UserId = int.Parse(dataRow["user_id"].ToString());
                        user.UserName = dataRow["user_name"].ToString();
                        user.FirstName = dataRow["first_name"].ToString();
                        user.LastName = dataRow["last_name"].ToString();

                        UserList.Add(user);
                    }

                    return UserList;
                }
                else
                {
                    return null;
                }
            }

            catch
            {
                return null;
            }
        }
    }
}
