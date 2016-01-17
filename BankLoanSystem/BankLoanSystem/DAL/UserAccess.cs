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

        public User retreiveUserByUserId(int id) {


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
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
                            user.Status = (bool)reader["status"];
                            user.CreatedDate = (DateTime)reader["created_date"];
                            user.IsDelete = (bool)reader["is_delete"];
                            user.CreatedBy = int.Parse(reader["created_by"].ToString());
                            user.BranchId = int.Parse(reader["branch_id"].ToString());
                            user.RoleId = int.Parse(reader["role_id"].ToString());
                            user.UserName = reader["user_name"].ToString();
                            user.Password = reader["password"].ToString();
                            user.Company_Id = int.Parse(reader["company_id"].ToString());

                        }
                        return user;

                    }
                }


                catch (Exception ex )
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
        /// update User Details By userName, firstName, lastName, email, phone, isActive, branchId
        /// 
        /// argument : user_id (int)
        /// 
        /// </summary>
        /// <returns>User object</returns>

        public bool updateUserDetails(int userId, string userName , string firstName, string lastName, string email , string phone , bool isActive, int branchId, DateTime modifiedDate, string password )
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
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
                        cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;

                        con.Open();
                        cmd.ExecuteNonQuery();
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



            return true;
        }




        public bool updateProfileDetails(int userId, string userName, string firstName, string lastName, string email, string phone, DateTime modifiedDate, string password)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
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
                        cmd.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;

                        con.Open();
                        cmd.ExecuteNonQuery();
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
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/13
        /// 
        /// Check 
        /// 
        /// argument : companyName (string)
        /// 
        /// </summary>
        /// <returns>true/false</returns>
        public bool IsUniqueUserName(string userName)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                var command = new SqlCommand("spIsUniqueUserName", con) { CommandType = CommandType.StoredProcedure };
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
                var command = new SqlCommand("spInsertUser", con) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = user.Password;
                command.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = user.FirstName;
                command.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = user.LastName;
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = user.Email;
                command.Parameters.Add("@phone_no", SqlDbType.NVarChar).Value = user.PhoneNumber;
                command.Parameters.Add("@status", SqlDbType.Bit).Value = user.Status;
                command.Parameters.Add("@created_by", SqlDbType.Int).Value = user.CreatedBy;
                command.Parameters.Add("@branch_id", SqlDbType.Int).Value = user.BranchId;
                command.Parameters.Add("@role_id", SqlDbType.Int).Value = user.RoleId;
                con.Open();
                return command.ExecuteNonQuery();
            }
        }

    }
}