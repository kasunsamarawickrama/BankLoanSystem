﻿using System;
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
        /// 
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
        /// 
        /// insert company, insert main branch, insert first super admin 
        /// 
        /// argument : companyBranchModel (CompanyBranchModel), user (User)
        /// 
        /// </summary>
        /// <returns>true/false</returns>
        public void SetupCompany(CompanyBranchModel companyBranchModel, User user)
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
                    com1.Parameters.Add("@company_name", SqlDbType.NVarChar).Value = companyBranchModel.Company.CompanyName;
                    com1.Parameters.Add("@company_code", SqlDbType.NVarChar).Value = companyBranchModel.Company.CompanyCode;
                    com1.Parameters.Add("@company_address", SqlDbType.NVarChar).Value =
                        companyBranchModel.Company.CompanyAddress;
                    com1.Parameters.Add("@state", SqlDbType.NVarChar).Value = companyBranchModel.Company.State;
                    com1.Parameters.Add("@city", SqlDbType.NVarChar).Value = companyBranchModel.Company.City;
                    com1.Parameters.Add("@zip", SqlDbType.NVarChar).Value = companyBranchModel.Company.Zip;
                    com1.Parameters.Add("@email", SqlDbType.NVarChar).Value = companyBranchModel.Company.Email;
                    com1.Parameters.Add("@phone_num", SqlDbType.NVarChar).Value = companyBranchModel.Company.PhoneNum;
                    com1.Parameters.Add("@fax", SqlDbType.NVarChar).Value = companyBranchModel.Company.Fax;
                    com1.Parameters.Add("@website_url", SqlDbType.NVarChar).Value =
                        companyBranchModel.Company.WebsiteUrl;
                    com1.Parameters.Add("@created_by", SqlDbType.Int).Value = 0;
                    com1.Parameters.Add("@created_date", SqlDbType.DateTime).Value = DateTime.Now;
                    com1.Parameters.Add("@company_type", SqlDbType.Int).Value = companyBranchModel.Company.TypeId;
                    com1.Parameters.Add("@first_super_admin_id", SqlDbType.Int).Value = 0;
                    con.Open();
                    com1.ExecuteNonQuery();
                    con.Close();

                    //get company id
                    var command = new SqlCommand("spGetCompanyIdByCompanyName", con) { CommandType = CommandType.StoredProcedure };
                    con.Open();
                    command.Parameters.Add("@company_name", SqlDbType.NVarChar).Value = companyBranchModel.Company.CompanyName;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            companyId = Convert.ToInt32(reader["company_id"]);
                    }
                    con.Close();

                    //Insert branch
                    var com2 = new SqlCommand("spInsertBranch", con) {
                        CommandType = CommandType.StoredProcedure,
                        Connection = con
                    };
                    com2.Parameters.Add("@user_id", SqlDbType.Int).Value = 0;
                    com2.Parameters.Add("@branch_code", SqlDbType.NVarChar).Value =
                        companyBranchModel.MainBranch.BranchCode;
                    com2.Parameters.Add("@branch_name", SqlDbType.NVarChar).Value =
                        companyBranchModel.MainBranch.BranchName;
                    com2.Parameters.Add("@branch_address", SqlDbType.NVarChar).Value =
                        companyBranchModel.MainBranch.BranchAddress;
                    com2.Parameters.Add("@state", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchState;
                    com2.Parameters.Add("@city", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchCity;
                    com2.Parameters.Add("@zip", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchZip;
                    com2.Parameters.Add("@email", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchEmail;
                    com2.Parameters.Add("@phone_num", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchPhoneNum;
                    com2.Parameters.Add("@fax", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchFax;
                    com2.Parameters.Add("@company_id", SqlDbType.Int).Value = companyId;
                    com2.Parameters.Add("@created_date", SqlDbType.DateTime).Value = DateTime.Now;
                    con.Open();
                    com2.ExecuteNonQuery();
                    con.Close();

                    //get branch id by branch code 
                    command = new SqlCommand("spGetBranchIdByBranchCode", con) { CommandType = CommandType.StoredProcedure };
                    con.Open();
                    command.Parameters.Add("@branch_code", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchCode;
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

                    com3.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user.UserName;
                    com3.Parameters.Add("@password", SqlDbType.NVarChar).Value = user.Password;
                    com3.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = user.FirstName;
                    com3.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = user.LastName;
                    com3.Parameters.Add("@email", SqlDbType.NVarChar).Value = user.Email;
                    com3.Parameters.Add("@phone_no", SqlDbType.NVarChar).Value = user.PhoneNumber;
                    com3.Parameters.Add("@status", SqlDbType.Bit).Value = user.Status;
                    com3.Parameters.Add("@is_delete", SqlDbType.Bit).Value = 0;
                    com3.Parameters.Add("@created_by", SqlDbType.Int).Value = 0;
                    com3.Parameters.Add("@create_Date", SqlDbType.DateTime).Value = DateTime.Now;
                    com3.Parameters.Add("@branch_id", SqlDbType.Int).Value = branchId;
                    com3.Parameters.Add("@role_id", SqlDbType.Int).Value = 1;
                    con.Open();
                    com3.ExecuteNonQuery();
                    con.Close();

                    //Get first super admin id
                    command = new SqlCommand("spGetUserIdByUserName", con) { CommandType = CommandType.StoredProcedure};
                    con.Open();
                    command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user.UserName;
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
                        command.Parameters.Add("@company_name", SqlDbType.NVarChar).Value = companyBranchModel.Company.CompanyName;
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
                        command.Parameters.Add("@branch_code", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchCode;
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

                }
                catch (SqlException sEx)
                {
                    throw sEx;
                }
            }
        }


        public void SetupCompanyRollback(CompanyBranchModel companyBranchModel, User user)
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
                    com1.Parameters.Add("@company_name", SqlDbType.NVarChar).Value = companyBranchModel.Company.CompanyName;
                    com1.Parameters.Add("@company_code", SqlDbType.NVarChar).Value = companyBranchModel.Company.CompanyCode;
                    com1.Parameters.Add("@company_address", SqlDbType.NVarChar).Value =
                        companyBranchModel.Company.CompanyAddress;
                    com1.Parameters.Add("@state", SqlDbType.NVarChar).Value = companyBranchModel.Company.State;
                    com1.Parameters.Add("@city", SqlDbType.NVarChar).Value = companyBranchModel.Company.City;
                    com1.Parameters.Add("@zip", SqlDbType.NVarChar).Value = companyBranchModel.Company.Zip;
                    com1.Parameters.Add("@email", SqlDbType.NVarChar).Value = companyBranchModel.Company.Email;
                    com1.Parameters.Add("@phone_num", SqlDbType.NVarChar).Value = companyBranchModel.Company.PhoneNum;
                    com1.Parameters.Add("@fax", SqlDbType.NVarChar).Value = companyBranchModel.Company.Fax;
                    com1.Parameters.Add("@website_url", SqlDbType.NVarChar).Value =
                        companyBranchModel.Company.WebsiteUrl;
                    com1.Parameters.Add("@created_by", SqlDbType.Int).Value = 0;
                    com1.Parameters.Add("@created_date", SqlDbType.DateTime).Value = DateTime.Now;
                    com1.Parameters.Add("@company_type", SqlDbType.Int).Value = companyBranchModel.Company.TypeId;
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
                        companyBranchModel.MainBranch.BranchCode;
                    com2.Parameters.Add("@branch_name", SqlDbType.NVarChar).Value =
                        companyBranchModel.MainBranch.BranchName;
                    com2.Parameters.Add("@branch_address", SqlDbType.NVarChar).Value =
                        companyBranchModel.MainBranch.BranchAddress;
                    com2.Parameters.Add("@state", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchState;
                    com2.Parameters.Add("@city", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchCity;
                    com2.Parameters.Add("@zip", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchZip;
                    com2.Parameters.Add("@email", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchEmail;
                    com2.Parameters.Add("@phone_num", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchPhoneNum;
                    com2.Parameters.Add("@fax", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchFax;
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

                    com3.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user.UserName;
                    com3.Parameters.Add("@password", SqlDbType.NVarChar).Value = user.Password;
                    com3.Parameters.Add("@first_name", SqlDbType.NVarChar).Value = user.FirstName;
                    com3.Parameters.Add("@last_name", SqlDbType.NVarChar).Value = user.LastName;
                    com3.Parameters.Add("@email", SqlDbType.NVarChar).Value = user.Email;
                    com3.Parameters.Add("@phone_no", SqlDbType.NVarChar).Value = user.PhoneNumber;
                    com3.Parameters.Add("@status", SqlDbType.Bit).Value = user.Status;
                    com3.Parameters.Add("@is_delete", SqlDbType.Bit).Value = 0;
                    com3.Parameters.Add("@created_by", SqlDbType.Int).Value = 0;
                    com3.Parameters.Add("@create_Date", SqlDbType.DateTime).Value = DateTime.Now;
                    com3.Parameters.Add("@branch_id", SqlDbType.Int).Value = branchId;
                    com3.Parameters.Add("@role_id", SqlDbType.Int).Value = 1;
                    com3.ExecuteNonQuery();

                    //get company id
                    var command = new SqlCommand("spGetCompanyIdByCompanyName", con) { CommandType = CommandType.StoredProcedure };
                    con.Open();
                    command.Parameters.Add("@company_name", SqlDbType.NVarChar).Value = companyBranchModel.Company.CompanyName;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            companyId = Convert.ToInt32(reader["company_id"]);
                    }
                    con.Close();

                    //get branch id by branch code 
                    command = new SqlCommand("spGetBranchIdByBranchCode", con) { CommandType = CommandType.StoredProcedure };
                    con.Open();
                    command.Parameters.Add("@branch_code", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchCode;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            branchId = Convert.ToInt32(reader["branch_id"]);
                    }
                    con.Close();

                    //Get first super admin id
                    command = new SqlCommand("spGetUserIdByUserName", con) { CommandType = CommandType.StoredProcedure };
                    con.Open();
                    command.Parameters.Add("@user_name", SqlDbType.NVarChar).Value = user.UserName;
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
                        command.Parameters.Add("@company_name", SqlDbType.NVarChar).Value = companyBranchModel.Company.CompanyName;
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
                        command.Parameters.Add("@branch_code", SqlDbType.NVarChar).Value = companyBranchModel.MainBranch.BranchCode;
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