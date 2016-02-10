using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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


        public List<State> GetAllStates()
        {
            List<State> stateList = new List<State>();

            using (
                var con =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                var command = new SqlCommand("spGetState", con) { CommandType = CommandType.StoredProcedure };
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        State state = new State()
                        {
                            StateId = Convert.ToInt32(reader["state_id"]),
                            StateName = reader["state_name"].ToString()
                        };
                        stateList.Add(state);
                    }
                }
            }

            return stateList;
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
        /// CreatedDate: 2016/02/08
        /// 
        /// Get latest company code to create next company code
        /// 
        /// argument : companyName (string)
        /// 
        /// </summary>
        /// <returns>true/false</returns>
        public string GetLatestNonRegCompanyCode(string prefix)
        {
            string latestCompanyCode = "";
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                var command = new SqlCommand("spGetNonRegCompanyCodebyCode", con) { CommandType = CommandType.StoredProcedure };
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
        /// CreatedDate: 01/26/2016
        /// 
        /// Get company details
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Company GetCompanyDetailsByFirstSpUserId(int userId)
        {
            Company company = new Company();
            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    var command = new SqlCommand("spGetCompanyDetailsBySUserId", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userId);
                    con.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            company.CompanyId = Convert.ToInt32(reader["company_Id"]);
                            company.CompanyName = reader["company_name"].ToString();
                            company.CompanyCode = reader["company_code"].ToString();
                            company.CompanyAddress1 = reader["company_address_1"].ToString();
                            company.CompanyAddress2 = reader["company_address_2"].ToString();
                            company.StateId = Convert.ToInt32(reader["stateId"]);
                            company.City = reader["city"].ToString();
                            company.Zip = reader["zip"].ToString();

                            string[] zipWithExtention = company.Zip.Split('-');

                            if (zipWithExtention[0] != null) company.ZipPre = zipWithExtention[0];
                            if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) company.Extension = zipWithExtention[1];

                            company.Email = reader["email"].ToString();
                            company.PhoneNum1 = reader["phone_num_1"].ToString();
                            company.PhoneNum2 = reader["phone_num_2"].ToString();
                            company.PhoneNum3 = reader["phone_num_3"].ToString();
                            company.Fax = reader["fax"].ToString();
                            company.WebsiteUrl = reader["website_url"].ToString();
                            company.TypeId = Convert.ToInt32(reader["company_type"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return company;
        }


        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 01/26/2016
        /// 
        /// Insert company in setup process 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool InsertCompany(Company company, string type)
        {
            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    var command = new SqlCommand("spInsertCompany", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@company_name", company.CompanyName ?? "");
                    command.Parameters.AddWithValue("@company_code", company.CompanyCode ?? "");
                    command.Parameters.AddWithValue("@company_address_1", company.CompanyAddress1 ?? "");
                    command.Parameters.AddWithValue("@company_address_2", company.CompanyAddress2 ?? "");
                    command.Parameters.AddWithValue("@stateId", company.StateId);
                    command.Parameters.AddWithValue("@city", company.City ?? "");
                    command.Parameters.AddWithValue("@zip", company.Zip ?? "");
                    command.Parameters.AddWithValue("@email", company.Email ?? "");
                    command.Parameters.AddWithValue("@phone_num_1", company.PhoneNum1 ?? "");
                    command.Parameters.AddWithValue("@phone_num_2", company.PhoneNum2 ?? "");
                    command.Parameters.AddWithValue("@phone_num_3", company.PhoneNum3 ?? "");
                    command.Parameters.AddWithValue("@fax", company.Fax ?? "");
                    command.Parameters.AddWithValue("@website_url", company.WebsiteUrl ?? "");
                    command.Parameters.AddWithValue("@created_by", company.CreatedBy);
                    command.Parameters.AddWithValue("@created_date", DateTime.Now);
                    command.Parameters.AddWithValue("@company_type", company.TypeId);
                    command.Parameters.AddWithValue("@first_super_admin_id", company.FirstSuperAdminId);
                    command.Parameters.AddWithValue("@company_status", company.CompanyStatus);
                    command.Parameters.AddWithValue("@transaction_type", type);

                    con.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
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
                        userCompany.Company.CompanyAddress1;
                    com1.Parameters.Add("@state", SqlDbType.NVarChar).Value = userCompany.Company.StateId;
                    com1.Parameters.Add("@city", SqlDbType.NVarChar).Value = userCompany.Company.City;
                    com1.Parameters.Add("@zip", SqlDbType.NVarChar).Value = userCompany.Company.Zip;
                    com1.Parameters.Add("@email", SqlDbType.NVarChar).Value = userCompany.Company.Email;
                    com1.Parameters.Add("@phone_num", SqlDbType.NVarChar).Value = userCompany.Company.PhoneNum1;
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
                        userCompany.Branch.BranchAddress1;
                    com2.Parameters.Add("@state", SqlDbType.NVarChar).Value = userCompany.Branch.StateId;
                    com2.Parameters.Add("@city", SqlDbType.NVarChar).Value = userCompany.Branch.BranchCity;
                    com2.Parameters.Add("@zip", SqlDbType.NVarChar).Value = userCompany.Branch.BranchZip;
                    com2.Parameters.Add("@email", SqlDbType.NVarChar).Value = userCompany.Branch.BranchEmail;
                    com2.Parameters.Add("@phone_num", SqlDbType.NVarChar).Value = userCompany.Branch.BranchPhoneNum1;
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
        public bool SetupCompanyRollback(UserCompanyModel userCompany)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                int companyId = 0, branchId = 0, superAdminId = 0;
                try
                {
                    var com1 = new SqlCommand("spSetupCompany", con)
                    {
                        CommandType = CommandType.StoredProcedure,
                    };
                    com1.Parameters.Add("@c_company_name", SqlDbType.NVarChar).Value =
                        userCompany.Company.CompanyName ?? "";
                    com1.Parameters.Add("@c_company_code", SqlDbType.NVarChar).Value = userCompany.Company.CompanyCode ?? "";
                    com1.Parameters.Add("@c_company_address_1", SqlDbType.NVarChar).Value =
                        userCompany.Company.CompanyAddress1 ?? "";
                    com1.Parameters.Add("@c_company_address_2", SqlDbType.NVarChar).Value =
                        userCompany.Company.CompanyAddress2 ?? "";
                    com1.Parameters.Add("@c_stateId", SqlDbType.Int).Value = userCompany.Company.StateId;
                    com1.Parameters.Add("@c_city", SqlDbType.NVarChar).Value = userCompany.Company.City ?? "";
                    com1.Parameters.Add("@c_zip", SqlDbType.NVarChar).Value = userCompany.Company.Zip ?? "";
                    com1.Parameters.Add("@c_email", SqlDbType.NVarChar).Value = userCompany.Company.Email ?? "";
                    com1.Parameters.Add("@c_phone_num_1", SqlDbType.NVarChar).Value = userCompany.Company.PhoneNum1 ?? "";
                    com1.Parameters.Add("@c_phone_num_2", SqlDbType.NVarChar).Value = userCompany.Company.PhoneNum2 ?? "";
                    com1.Parameters.Add("@c_phone_num_3", SqlDbType.NVarChar).Value = userCompany.Company.PhoneNum3 ?? "";
                    com1.Parameters.Add("@c_fax", SqlDbType.NVarChar).Value = userCompany.Company.Fax ?? "";
                    com1.Parameters.Add("@c_website_url", SqlDbType.NVarChar).Value =
                        userCompany.Company.WebsiteUrl ?? "";
                    com1.Parameters.Add("@c_created_by", SqlDbType.Int).Value = 0;
                    com1.Parameters.Add("@c_created_date", SqlDbType.DateTime).Value = DateTime.Now;
                    com1.Parameters.Add("@c_company_type", SqlDbType.Int).Value = userCompany.Company.TypeId;
                    com1.Parameters.Add("@c_first_super_admin_id", SqlDbType.Int).Value = 0;
                    com1.Parameters.AddWithValue("@c_company_status", true);

                    com1.Parameters.Add("@b_user_id", SqlDbType.Int).Value = 0;
                    com1.Parameters.Add("@b_branch_code", SqlDbType.NVarChar).Value =
                        userCompany.Branch.BranchCode ?? "";
                    com1.Parameters.Add("@b_branch_name", SqlDbType.NVarChar).Value =
                        userCompany.Branch.BranchName ?? "";
                    com1.Parameters.Add("@b_branch_address_1", SqlDbType.NVarChar).Value =
                        userCompany.Branch.BranchAddress1 ?? "";
                    com1.Parameters.Add("@b_branch_address_2", SqlDbType.NVarChar).Value =
                        userCompany.Branch.BranchAddress2 ?? "";
                    com1.Parameters.Add("@b_state_id", SqlDbType.Int).Value = userCompany.Branch.StateId;
                    com1.Parameters.Add("@b_city", SqlDbType.NVarChar).Value = userCompany.Branch.BranchCity ?? "";
                    com1.Parameters.Add("@b_zip", SqlDbType.NVarChar).Value = userCompany.Branch.BranchZip ?? "";
                    com1.Parameters.Add("@b_email", SqlDbType.NVarChar).Value = userCompany.Branch.BranchEmail ?? "";
                    com1.Parameters.Add("@b_phone_num_1", SqlDbType.NVarChar).Value = userCompany.Branch.BranchPhoneNum1 ?? "";
                    com1.Parameters.Add("@b_phone_num_2", SqlDbType.NVarChar).Value = userCompany.Branch.BranchPhoneNum2 ?? "";
                    com1.Parameters.Add("@b_phone_num_3", SqlDbType.NVarChar).Value = userCompany.Branch.BranchPhoneNum3 ?? "";
                    com1.Parameters.Add("@b_fax", SqlDbType.NVarChar).Value = userCompany.Branch.BranchFax ?? "";
                    com1.Parameters.Add("@b_company_id", SqlDbType.Int).Value = 0;
                    com1.Parameters.Add("@b_created_date", SqlDbType.DateTime).Value = DateTime.Now;

                    com1.Parameters.Add("@u_user_name", SqlDbType.NVarChar).Value = userCompany.User.UserName ?? "";
                    com1.Parameters.Add("@u_password", SqlDbType.NVarChar).Value = userCompany.User.Password ?? "";
                    com1.Parameters.Add("@u_first_name", SqlDbType.NVarChar).Value = userCompany.User.FirstName ?? "";
                    com1.Parameters.Add("@u_last_name", SqlDbType.NVarChar).Value = userCompany.User.LastName ?? "";
                    com1.Parameters.Add("@u_email", SqlDbType.NVarChar).Value = userCompany.User.Email ?? "";
                    com1.Parameters.Add("@u_phone_no", SqlDbType.NVarChar).Value = userCompany.User.PhoneNumber ?? "";
                    com1.Parameters.Add("@u_status", SqlDbType.Bit).Value = userCompany.User.Status;
                    com1.Parameters.Add("@u_is_delete", SqlDbType.Bit).Value = 0;
                    com1.Parameters.Add("@u_created_by", SqlDbType.Int).Value = 0;
                    com1.Parameters.Add("@u_create_Date", SqlDbType.DateTime).Value = DateTime.Now;
                    com1.Parameters.Add("@u_branch_id", SqlDbType.Int).Value = 0;
                    com1.Parameters.Add("@u_role_id", SqlDbType.Int).Value = 1;
                    con.Open();
                    SqlParameter returnParameter = com1.Parameters.Add("@return", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    com1.ExecuteNonQuery();

                    int returnVal = (int)returnParameter.Value;

                    con.Close();

                    if (returnVal == 1)
                    {
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
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (SqlException sEx)
                {
                    return false;
                }
            }
        }



        public bool SetupCompanyRollbackXxx(UserCompanyModel userCompany)
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
                        userCompany.Company.CompanyAddress1;
                    com1.Parameters.Add("@state", SqlDbType.NVarChar).Value = userCompany.Company.StateId;
                    com1.Parameters.Add("@city", SqlDbType.NVarChar).Value = userCompany.Company.City;
                    com1.Parameters.Add("@zip", SqlDbType.NVarChar).Value = userCompany.Company.Zip;
                    com1.Parameters.Add("@email", SqlDbType.NVarChar).Value = userCompany.Company.Email;
                    com1.Parameters.Add("@phone_num", SqlDbType.NVarChar).Value = userCompany.Company.PhoneNum1;
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
                        userCompany.Branch.BranchAddress1;
                    com2.Parameters.Add("@state", SqlDbType.NVarChar).Value = userCompany.Branch.StateId;
                    com2.Parameters.Add("@city", SqlDbType.NVarChar).Value = userCompany.Branch.BranchCity;
                    com2.Parameters.Add("@zip", SqlDbType.NVarChar).Value = userCompany.Branch.BranchZip;
                    com2.Parameters.Add("@email", SqlDbType.NVarChar).Value = userCompany.Branch.BranchEmail;
                    com2.Parameters.Add("@phone_num", SqlDbType.NVarChar).Value = userCompany.Branch.BranchPhoneNum1;
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
                    return true;
                }
                catch (SqlException sEx)
                {
                    tran.Rollback();
                    //throw sEx;
                    return false;
                }
            }
        }


        /// <summary>
        /// CreatedBy : Irfan
        /// CreatedDate: 01/27/2016
        /// 
        /// Insert company in setup process 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public bool InsertNonRegisteredCompany(Company company)
        {
            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    var command = new SqlCommand("spInsertNonRegisteredCompany", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@company_name", company.CompanyName ?? "");
                    command.Parameters.AddWithValue("@company_code", company.CompanyCode ?? "");
                    command.Parameters.AddWithValue("@company_address_1", company.CompanyAddress1 ?? "");
                    command.Parameters.AddWithValue("@company_address_2", company.CompanyAddress2 ?? "");
                    command.Parameters.AddWithValue("@stateId", company.StateId);
                    command.Parameters.AddWithValue("@city", company.City ?? "");
                    command.Parameters.AddWithValue("@zip", company.Zip ?? "");
                    command.Parameters.AddWithValue("@email", company.Email ?? "");
                    command.Parameters.AddWithValue("@phone_num_1", company.PhoneNum1 ?? "");
                    command.Parameters.AddWithValue("@phone_num_2", company.PhoneNum2 ?? "");
                    command.Parameters.AddWithValue("@phone_num_3", company.PhoneNum3 ?? "");
                    command.Parameters.AddWithValue("@fax", company.Fax ?? "");
                    command.Parameters.AddWithValue("@website_url", company.WebsiteUrl ?? "");
                    command.Parameters.AddWithValue("@created_by", company.CreatedBy);
                    command.Parameters.AddWithValue("@created_date", DateTime.Now);
                    command.Parameters.AddWithValue("@company_type", company.TypeId);
                    command.Parameters.AddWithValue("@reg_company_id", company.CreatedByCompany);


                    con.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 01/27/2016
        /// </summary>
        /// <param name="regCompanyId"></param>
        /// <returns></returns>
        public Company GetNonRegCompanyDetailsByRegCompanyId(int regCompanyId)
        {
            Company company = new Company();
            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    var command = new SqlCommand("spGetNonRegCompanyDetailsByRegCompanyId", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@reg_company_id", regCompanyId);
                    con.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            company.CompanyName = reader["company_name"].ToString();
                            company.CompanyCode = reader["company_code"].ToString();
                            company.CompanyAddress1 = reader["company_address_1"].ToString();
                            company.CompanyAddress2 = reader["company_address_2"].ToString();
                            company.StateId = Convert.ToInt32(reader["stateId"]);
                            company.City = reader["city"].ToString();
                            company.Zip = reader["zip"].ToString();
                            company.Email = reader["email"].ToString();
                            company.PhoneNum1 = reader["phone_num_1"].ToString();
                            company.PhoneNum2 = reader["phone_num_2"].ToString();
                            company.PhoneNum3 = reader["phone_num_3"].ToString();
                            company.Fax = reader["fax"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return company;
        }


        public List<Company> GetCompanyByCreayedCompany(int createdByComId)
        {
            List<Company> nonRegCompanies = new List<Company>();
            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    //
                    var command = new SqlCommand("spGetNonRegCompanyByCreatedCompany", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@reg_company_id", createdByComId);
                    con.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Company company = new Company();
                            company.CompanyId = Convert.ToInt32(reader["company_id"]);
                            company.CompanyName = reader["company_name"].ToString();
                            company.CompanyCode = reader["company_code"].ToString();
                            company.CompanyAddress1 = reader["company_address_1"].ToString();
                            company.CompanyAddress2 = reader["company_address_2"].ToString();
                            company.StateId = Convert.ToInt32(reader["stateId"]);
                            company.City = reader["city"].ToString();
                            company.Zip = reader["zip"].ToString();

                            string[] zipWithExtention = company.Zip.Split('-');

                            if (zipWithExtention[0] != null) company.ZipPre = zipWithExtention[0];
                            if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) company.Extension = zipWithExtention[1];

                            company.Email = reader["email"].ToString();
                            company.PhoneNum1 = reader["phone_num_1"].ToString();
                            company.PhoneNum2 = reader["phone_num_2"].ToString();
                            company.PhoneNum3 = reader["phone_num_3"].ToString();
                            company.Fax = reader["fax"].ToString();

                            nonRegCompanies.Add(company);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return nonRegCompanies;
        }


        public Company GetNonRegCompanyByCompanyId(int companyId)
        {
            Company nonRegCompany = new Company();
            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    //
                    var command = new SqlCommand("spGetNonRegCompanyByCompanyId", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@company_id", companyId);
                    con.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nonRegCompany.CompanyId = Convert.ToInt32(reader["company_id"]);
                            nonRegCompany.CompanyName = reader["company_name"].ToString();
                            nonRegCompany.CompanyCode = reader["company_code"].ToString();
                            nonRegCompany.CompanyAddress1 = reader["company_address_1"].ToString();
                            nonRegCompany.CompanyAddress2 = reader["company_address_2"].ToString();
                            nonRegCompany.StateId = Convert.ToInt32(reader["stateId"]);
                            nonRegCompany.City = reader["city"].ToString();
                            nonRegCompany.Zip = reader["zip"].ToString();

                            string[] zipWithExtention = nonRegCompany.Zip.Split('-');

                            if (zipWithExtention[0] != null) nonRegCompany.ZipPre = zipWithExtention[0];
                            if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) nonRegCompany.Extension = zipWithExtention[1];

                            nonRegCompany.Email = reader["email"].ToString();
                            nonRegCompany.PhoneNum1 = reader["phone_num_1"].ToString();
                            nonRegCompany.PhoneNum2 = reader["phone_num_2"].ToString();
                            nonRegCompany.PhoneNum3 = reader["phone_num_3"].ToString();
                            nonRegCompany.Fax = reader["fax"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return nonRegCompany;
        }

    }
}