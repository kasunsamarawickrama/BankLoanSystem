using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class CompanyAccess
    {
        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/17
        /// 
        /// Get all company types
        /// 
        /// argument : None
        /// 
        /// </summary>
        /// <returns>List<CompanyType></returns>
        public List<CompanyType> GetAllCompanyType()
        {
            List<CompanyType> ctList = new List<CompanyType>();

            using (
                var con =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                var command = new SqlCommand("spGetAllCompanyType", con) { CommandType = CommandType.StoredProcedure };
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CompanyType ct = new CompanyType()
                        {
                            TypeId = Convert.ToInt32(reader["company_type_id"]),
                            TypeName = reader["company_type_name"].ToString()
                        };
                        ctList.Add(ct);
                    }
                }
            }

            return ctList;
        }


        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/17
        /// 
        /// Check company name is already exists in thd database
        /// 
        /// argument : companyName (string)
        /// 
        /// </summary>
        /// <returns>true/false</returns>
        public bool IsUniqueCompanyName(string companyName)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                var command = new SqlCommand("spIsUniqueCompanyName", con) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@company_name", SqlDbType.NVarChar).Value = companyName;
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
        /// CreatedDate: 2016/01/17
        /// 
        /// Get latest company code to create next company code
        /// 
        /// argument : companyName (string)
        /// 
        /// </summary>
        /// <returns>true/false</returns>
        public string GetLatestCompanyCode(string prefix)
        {
            string latestCompanyCode = "";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                var command = new SqlCommand("spGetCompanyCodebyCode", con) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@company_code_prefix", SqlDbType.NVarChar).Value = prefix;
                con.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        latestCompanyCode = reader["company_code"].ToString();
                    }
                }
            }
            return latestCompanyCode;
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/18
        /// EditedDate: 2016/01/19
        /// insert company, insert main branch, insert first super admin 
        /// 
        /// argument : userCompany (UserCompanyModel)
        /// 
        /// </summary>
        /// <returns>true/false</returns>
        public bool SetupCompany(UserCompanyModel userCompany)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                int superAdminId = 0, companyId = 0, branchId = 0;
                try
                {
                    //Insert company
                    var com1 = new SqlCommand("spInsertCompany", con)
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = con
                    };
                    com1.Parameters.Add("@company_name", SqlDbType.NVarChar).Value = userCompany.Company.CompanyName;
                    com1.Parameters.Add("@company_code", SqlDbType.NVarChar).Value = userCompany.Company.CompanyCode;
                    com1.Parameters.Add("@company_address", SqlDbType.NVarChar).Value =
                        userCompany.Company.CompanyAddress;
                    com1.Parameters.Add("@state", SqlDbType.NVarChar).Value = userCompany.Company.State;
                    com1.Parameters.Add("@city", SqlDbType.NVarChar).Value = userCompany.Company.City;
                    com1.Parameters.Add("@zip", SqlDbType.NVarChar).Value = userCompany.Company.Zip;
                    com1.Parameters.Add("@email", SqlDbType.NVarChar).Value = userCompany.Company.Email;
                    com1.Parameters.Add("@phone_num", SqlDbType.NVarChar).Value = userCompany.Company.PhoneNum;
                    com1.Parameters.Add("@fax", SqlDbType.NVarChar).Value = userCompany.Company.Fax;
                    com1.Parameters.Add("@website_url", SqlDbType.NVarChar).Value =
                        userCompany.Company.WebsiteUrl;
                    com1.Parameters.Add("@created_by", SqlDbType.Int).Value = 0;
                    com1.Parameters.Add("@created_date", SqlDbType.DateTime).Value = DateTime.Now;
                    com1.Parameters.Add("@company_type", SqlDbType.Int).Value = userCompany.Company.TypeId;
                    com1.Parameters.Add("@first_super_admin_id", SqlDbType.Int).Value = 0;
                    con.Open();
                    com1.ExecuteNonQuery();
                    con.Close();

                    //get company id
                    var command = new SqlCommand("spGetCompanyIdByCompanyName", con) { CommandType = CommandType.StoredProcedure };
                    con.Open();
                    command.Parameters.Add("@company_name", SqlDbType.NVarChar).Value = userCompany.Company.CompanyName;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            companyId = Convert.ToInt32(reader["company_id"]);
                    }
                    con.Close();

                    //Insert branch
                    var com2 = new SqlCommand("spInsertBranch", con)
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = con
                    };
                    com2.Parameters.Add("@user_id", SqlDbType.Int).Value = 0;
                    com2.Parameters.Add("@branch_code", SqlDbType.NVarChar).Value =
                        userCompany.Branch.BranchCode;
                    com2.Parameters.Add("@branch_name", SqlDbType.NVarChar).Value =
                        userCompany.Branch.BranchName;
                    com2.Parameters.Add("@branch_address", SqlDbType.NVarChar).Value =
                        userCompany.Branch.BranchAddress;
                    com2.Parameters.Add("@state", SqlDbType.NVarChar).Value = userCompany.Branch.BranchState;
                    com2.Parameters.Add("@city", SqlDbType.NVarChar).Value = userCompany.Branch.BranchCity;
                    com2.Parameters.Add("@zip", SqlDbType.NVarChar).Value = userCompany.Branch.BranchZip;
                    com2.Parameters.Add("@email", SqlDbType.NVarChar).Value = userCompany.Branch.BranchEmail;
                    com2.Parameters.Add("@phone_num", SqlDbType.NVarChar).Value = userCompany.Branch.BranchPhoneNum;
                    com2.Parameters.Add("@fax", SqlDbType.NVarChar).Value = userCompany.Branch.BranchFax;
                    com2.Parameters.Add("@company_id", SqlDbType.Int).Value = companyId;
                    com2.Parameters.Add("@created_date", SqlDbType.DateTime).Value = DateTime.Now;
                    con.Open();
                    com2.ExecuteNonQuery();
                    con.Close();

                    //get branch id by branch code 
                    command = new SqlCommand("spGetBranchIdByBranchCode", con) { CommandType = CommandType.StoredProcedure };
                    con.Open();
                    command.Parameters.Add("@branch_code", SqlDbType.NVarChar).Value = userCompany.Branch.BranchCode;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            branchId = Convert.ToInt32(reader["branch_id"]);
                    }
                    con.Close();

                    //Insert user
                    var com3 = new SqlCommand("spInsertUser", con)
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = con
                    };

                    com3.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = userCompany.User.UserName;
                    com3.Parameters.Add("@password", SqlDbType.NVarChar).Value = userCompany.User.Password;
                    com3.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = userCompany.User.FirstName;
                    com3.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = userCompany.User.LastName;
                    com3.Parameters.Add("@email", SqlDbType.NVarChar).Value = userCompany.User.Email;
                    com3.Parameters.Add("@phone_no", SqlDbType.NVarChar).Value = userCompany.User.PhoneNumber;
                    com3.Parameters.Add("@status", SqlDbType.Bit).Value = userCompany.User.Status;
                    com3.Parameters.Add("@is_delete", SqlDbType.Bit).Value = 0;
                    com3.Parameters.Add("@created_by", SqlDbType.Int).Value = 0;
                    com3.Parameters.Add("@create_Date", SqlDbType.DateTime).Value = DateTime.Now;
                    com3.Parameters.Add("@branch_id", SqlDbType.Int).Value = branchId;
                    com3.Parameters.Add("@role_id", SqlDbType.Int).Value = 1;
                    con.Open();
                    com3.ExecuteNonQuery();
                    con.Close();

                    //Get first super admin id
                    command = new SqlCommand("spGetUserIdByUserName", con) { CommandType = CommandType.StoredProcedure };
                    con.Open();
                    command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = userCompany.User.UserName;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            superAdminId = Convert.ToInt32(reader["user_id"]);
                    }
                    con.Close();

                    //update company super admin
                    if (superAdminId != 0)
                    {
                        command = new SqlCommand("spUpdateCompanySuperAdmin", con) { CommandType = CommandType.StoredProcedure };
                        command.Parameters.Add("@user_id", SqlDbType.Int).Value = superAdminId;
                        command.Parameters.Add("@company_name", SqlDbType.NVarChar).Value = userCompany.Company.CompanyName;
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    //update branch , company id and created by id 
                    if (companyId != 0)
                    {
                        command = new SqlCommand("spUpdateBranchCompanyId", con) { CommandType = CommandType.StoredProcedure };
                        command.Parameters.Add("@company_id", SqlDbType.Int).Value = companyId;
                        command.Parameters.Add("@created_by", SqlDbType.Int).Value = superAdminId;
                        command.Parameters.Add("@branch_code", SqlDbType.NVarChar).Value = userCompany.Branch.BranchCode;
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    //update user branch id 
                    if (branchId != 0)
                    {
                        command = new SqlCommand("spUpdateUserCreatedById", con) { CommandType = CommandType.StoredProcedure };
                        command.Parameters.Add("@created_by", SqlDbType.Int).Value = superAdminId;
                        command.Parameters.Add("@user_id", SqlDbType.Int).Value = superAdminId;
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                    return true;
                }
                catch (SqlException sEx)
                {
                    //throw sEx;
                    return false;
                    
                }
            }
        }


        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/18
        /// EditedDate: 2016/01/19
        /// 
        /// company, insert main branch, insert first super admin 
        /// with sql transaction(Roolback)
        /// 
        /// argument : companyName (string)
        /// 
        /// </summary>
        /// <returns></returns>
        public void SetupCompanyRollback(UserCompanyModel userCompany)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                int superAdminId = 0, companyId = 0, branchId = 0;
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                try
                {
                   

                    //Insert company
                    var com1 = new SqlCommand("spInsertCompany", con)
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = con,
                        Transaction = tran
                    };
                    com1.Parameters.Add("@company_name", SqlDbType.NVarChar).Value = userCompany.Company.CompanyName;
                    com1.Parameters.Add("@company_code", SqlDbType.NVarChar).Value = userCompany.Company.CompanyCode;
                    com1.Parameters.Add("@company_address", SqlDbType.NVarChar).Value =
                        userCompany.Company.CompanyAddress;
                    com1.Parameters.Add("@state", SqlDbType.NVarChar).Value = userCompany.Company.State;
                    com1.Parameters.Add("@city", SqlDbType.NVarChar).Value = userCompany.Company.City;
                    com1.Parameters.Add("@zip", SqlDbType.NVarChar).Value = userCompany.Company.Zip;
                    com1.Parameters.Add("@email", SqlDbType.NVarChar).Value = userCompany.Company.Email;
                    com1.Parameters.Add("@phone_num", SqlDbType.NVarChar).Value = userCompany.Company.PhoneNum;
                    com1.Parameters.Add("@fax", SqlDbType.NVarChar).Value = userCompany.Company.Fax;
                    com1.Parameters.Add("@website_url", SqlDbType.NVarChar).Value =
                        userCompany.Company.WebsiteUrl;
                    com1.Parameters.Add("@created_by", SqlDbType.Int).Value = 0;
                    com1.Parameters.Add("@created_date", SqlDbType.DateTime).Value = DateTime.Now;
                    com1.Parameters.Add("@company_type", SqlDbType.Int).Value = userCompany.Company.TypeId;
                    com1.Parameters.Add("@first_super_admin_id", SqlDbType.Int).Value = 0;
                    com1.ExecuteNonQuery();

                    //Insert branch
                    var com2 = new SqlCommand("spInsertBranch", con)
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = con,
                        Transaction = tran
                    };
                    com2.Parameters.Add("@user_id", SqlDbType.Int).Value = 0;
                    com2.Parameters.Add("@branch_code", SqlDbType.NVarChar).Value =
                        userCompany.Branch.BranchCode;
                    com2.Parameters.Add("@branch_name", SqlDbType.NVarChar).Value =
                        userCompany.Branch.BranchName;
                    com2.Parameters.Add("@branch_address", SqlDbType.NVarChar).Value =
                        userCompany.Branch.BranchAddress;
                    com2.Parameters.Add("@state", SqlDbType.NVarChar).Value = userCompany.Branch.BranchState;
                    com2.Parameters.Add("@city", SqlDbType.NVarChar).Value = userCompany.Branch.BranchCity;
                    com2.Parameters.Add("@zip", SqlDbType.NVarChar).Value = userCompany.Branch.BranchZip;
                    com2.Parameters.Add("@email", SqlDbType.NVarChar).Value = userCompany.Branch.BranchEmail;
                    com2.Parameters.Add("@phone_num", SqlDbType.NVarChar).Value = userCompany.Branch.BranchPhoneNum;
                    com2.Parameters.Add("@fax", SqlDbType.NVarChar).Value = userCompany.Branch.BranchFax;
                    com2.Parameters.Add("@company_id", SqlDbType.Int).Value = companyId;
                    com2.Parameters.Add("@created_date", SqlDbType.DateTime).Value = DateTime.Now;
                    com2.ExecuteNonQuery();

                    //Insert user
                    var com3 = new SqlCommand("spInsertUser", con)
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = con, 
                        Transaction = tran
                    };

                    com3.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = userCompany.User.UserName;
                    com3.Parameters.Add("@password", SqlDbType.NVarChar).Value = userCompany.User.Password;
                    com3.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = userCompany.User.FirstName;
                    com3.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = userCompany.User.LastName;
                    com3.Parameters.Add("@email", SqlDbType.NVarChar).Value = userCompany.User.Email;
                    com3.Parameters.Add("@phone_no", SqlDbType.NVarChar).Value = userCompany.User.PhoneNumber;
                    com3.Parameters.Add("@status", SqlDbType.Bit).Value = userCompany.User.Status;
                    com3.Parameters.Add("@is_delete", SqlDbType.Bit).Value = 0;
                    com3.Parameters.Add("@created_by", SqlDbType.Int).Value = 0;
                    com3.Parameters.Add("@create_Date", SqlDbType.DateTime).Value = DateTime.Now;
                    com3.Parameters.Add("@branch_id", SqlDbType.Int).Value = branchId;
                    com3.Parameters.Add("@role_id", SqlDbType.Int).Value = 1;
                    com3.ExecuteNonQuery();

                    //get company id
                    var command = new SqlCommand("spGetCompanyIdByCompanyName", con) { CommandType = CommandType.StoredProcedure };
                    con.Open();
                    command.Parameters.Add("@company_name", SqlDbType.NVarChar).Value = userCompany.Company.CompanyName;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            companyId = Convert.ToInt32(reader["company_id"]);
                    }
                    con.Close();

                    //get branch id by branch code 
                    command = new SqlCommand("spGetBranchIdByBranchCode", con) { CommandType = CommandType.StoredProcedure };
                    con.Open();
                    command.Parameters.Add("@branch_code", SqlDbType.NVarChar).Value = userCompany.Branch.BranchCode;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            branchId = Convert.ToInt32(reader["branch_id"]);
                    }
                    con.Close();

                    //Get first super admin id
                    command = new SqlCommand("spGetUserIdByUserName", con) { CommandType = CommandType.StoredProcedure };
                    con.Open();
                    command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = userCompany.User.UserName;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            superAdminId = Convert.ToInt32(reader["user_id"]);
                    }
                    con.Close();

                    //update company super admin
                    if (superAdminId != 0)
                    {
                        command = new SqlCommand("spUpdateCompanySuperAdmin", con) { CommandType = CommandType.StoredProcedure };
                        command.Parameters.Add("@user_id", SqlDbType.Int).Value = superAdminId;
                        command.Parameters.Add("@company_name", SqlDbType.NVarChar).Value = userCompany.Company.CompanyName;
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    //update branch , company id and created by id 
                    if (companyId != 0)
                    {
                        command = new SqlCommand("spUpdateBranchCompanyId", con) { CommandType = CommandType.StoredProcedure };
                        command.Parameters.Add("@company_id", SqlDbType.Int).Value = companyId;
                        command.Parameters.Add("@created_by", SqlDbType.Int).Value = superAdminId;
                        command.Parameters.Add("@branch_code", SqlDbType.NVarChar).Value = userCompany.Branch.BranchCode;
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }

                    //update user branch id 
                    if (branchId != 0)
                    {
                        command = new SqlCommand("spUpdateUserCreatedById", con) { CommandType = CommandType.StoredProcedure };
                        command.Parameters.Add("@created_by", SqlDbType.Int).Value = superAdminId;
                        command.Parameters.Add("@user_id", SqlDbType.Int).Value = superAdminId;
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();
                    }
                    tran.Commit();
                }
                catch (SqlException sEx)
                {
                    tran.Rollback();
                    throw sEx;
                }
            }
        }

    }
}