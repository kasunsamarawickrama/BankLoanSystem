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
    public class UserAccess
    {
        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/13
        /// 
        /// retreive User Detail By UserId
        /// 
        /// argument : user_id (int)
        /// 
        /// </summary>
        /// <returns>User object</returns>

        public User retreiveUserByUserId(int id)
        {


            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spRetrieveUserByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = id;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        User user = new User();

                        while (reader.Read())
                        {
                            user.UserId = int.Parse(reader["user_id"].ToString());
                            user.FirstName = reader["first_name"].ToString();
                            user.LastName = reader["last_name"].ToString();
                            user.Email = reader["email"].ToString();
                            user.PhoneNumber = reader["phone_no"].ToString();
                            user.Status = (bool) reader["status"];
                            user.CreatedDate = (DateTime) reader["created_date"];

                            user.IsDelete = (bool) reader["is_delete"];
                            user.CreatedBy = int.Parse(reader["created_by"].ToString());
                            user.BranchId = int.Parse(reader["branch_id"].ToString());
                            user.RoleId = int.Parse(reader["role_id"].ToString());
                            user.UserName = reader["user_name"].ToString();
                            user.UneditUserName = reader["user_name"].ToString();
                            user.Password = reader["password"].ToString();
                            user.Company_Id = int.Parse(reader["company_id"].ToString());

                        }
                        return user;

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
        /// CreatedDate: 2016/01/13
        /// 
        /// update User Details userName, firstName, lastName, email, phone, isActive, branchId, password
        /// 
        /// argument : user_id (int)
        /// 
        /// </summary>
        /// <returns>User object</returns>

        public bool updateUserDetails(int userId, string userName, string firstName, string lastName, string email,
            string phone, bool isActive, int branchId, DateTime modifiedDate)
        {

            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spUpdateUserDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                        cmd.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = userName;
                        cmd.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = firstName;
                        cmd.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = lastName;
                        cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                        cmd.Parameters.Add("@phone_no", SqlDbType.NVarChar).Value = phone;
                        cmd.Parameters.Add("@status", SqlDbType.Bit).Value = isActive;
                        cmd.Parameters.Add("@branch_id", SqlDbType.Int).Value = branchId;
                        cmd.Parameters.Add("@modified_date", SqlDbType.DateTime).Value = modifiedDate;
                        

                        con.Open();

                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        int countVal = (int) returnParameter.Value;

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



            return true;
        }

        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/13
        /// 
        /// update User Details  userName, firstName, lastName, email, phone, password
        /// 
        /// argument : user_id (int)
        /// 
        /// </summary>
        /// <returns>User object</returns>


        public bool updateProfileDetails(int userId, string userName, string firstName, string lastName, string email,
            string phone, DateTime modifiedDate)
        {
            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spUpdateProfileDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                        cmd.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = userName;
                        cmd.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = firstName;
                        cmd.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = lastName;
                        cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                        cmd.Parameters.Add("@phone_no", SqlDbType.NVarChar).Value = phone;
                        cmd.Parameters.Add("@modified_date", SqlDbType.DateTime).Value = modifiedDate;
                        

                        con.Open();

                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        int countVal = (int) returnParameter.Value;

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
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/13
        /// 
        /// Check 
        /// 
        /// argument : userName (string)
        /// 
        /// </summary>
        /// <returns>true/false</returns>
        public bool IsUniqueUserName(string userName)
        {
            using (
                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                var command = new SqlCommand("spIsUniqueUserName", con) {CommandType = CommandType.StoredProcedure};
                command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = userName;
                con.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/16
        /// 
        /// Insert User details
        /// 
        /// argument : user (User)
        /// 
        /// </summary>
        /// <returns>1</returns>

        public int InsertUser(User user)
        {
            using (
                var con =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                var command = new SqlCommand("spInsertUser", con) {CommandType = CommandType.StoredProcedure};
                command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = user.Password;
                command.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = user.FirstName;
                command.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = user.LastName;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = user.Email;
                command.Parameters.Add("@phone_no", SqlDbType.NVarChar).Value = user.PhoneNumber;
                command.Parameters.Add("@status", SqlDbType.Bit).Value = user.Status;
                command.Parameters.Add("@is_delete", SqlDbType.Bit).Value = user.IsDelete;
                command.Parameters.Add("@created_by", SqlDbType.Int).Value = user.CreatedBy;
                command.Parameters.Add("@create_Date", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("@branch_id", SqlDbType.Int).Value = user.BranchId;
                command.Parameters.Add("@role_id", SqlDbType.Int).Value = user.RoleId;
                con.Open();
                return command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// CreatedBy : irfan
        /// CreatedDate: 2016/01/16
        /// 
        /// get user id using email
        /// 
        /// argument : email (string)
        /// 
        /// </summary>
        /// <returns>1</returns>
        public int getUserId(string email)
        {


            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetUserIdByemail", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;


                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        int userId = 0;

                        while (reader.Read())
                        {
                            userId = int.Parse(reader["user_id"].ToString());
                        }


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

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/19
        /// 
        /// activated user account 
        /// 
        /// argument : userId (int)
        /// 
        /// </summary>
        /// <returns>true/false</returns>
        public int UpdateUserSatus(int userId, string activationCode)
        {
            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    var command = new SqlCommand("spUpdateUserStatus", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@activation_code", activationCode);

                    SqlParameter returnParameter = command.Parameters.Add("@return_val", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    con.Open();
                    command.ExecuteNonQuery();

                    int res = (int)returnParameter.Value;
                    return (int) returnParameter.Value;
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }

        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/21
        /// 
        /// Insert new created user to user_activation table
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="activationCode"></param>
        /// <returns></returns>
        public int InsertUserActivation(int userId, string activationCode)
        {
            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    var command = new SqlCommand("spInsertUserActivation", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@activation_code", activationCode);

                    SqlParameter returnParameter = command.Parameters.Add("@return_val", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    con.Open();
                    command.ExecuteNonQuery();

                    int res = (int)returnParameter.Value;
                    return (int)returnParameter.Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }


        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/20
        /// 
        /// get Company Employee Details  userName, password
        /// 
        /// argument : user_id (int)
        /// 
        /// </summary>
        /// <returns>Company EMployee Name</returns>


        public string getCompanyEmployeeName(int userId)
        {
            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetCompanyEmployeeDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@system_admin_id", SqlDbType.Int).Value = userId;
                        

                        con.Open();

                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        SqlDataReader reader = cmd.ExecuteReader();
                        string employeeName = "";

                        while (reader.Read())
                        {
                            employeeName = reader["user_name"].ToString();
                        }


                        return employeeName;

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