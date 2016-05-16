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
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/06
        /// removed existing connection open method
        /// call DataHandler class method and getting dataset object,
        /// create and return company type object list using that dataset
        /// 
        /// </summary>
        /// <returns>List<CompanyType></returns>
        public List<CompanyType> GetAllCompanyType()
        {
            List<CompanyType> ctList = new List<CompanyType>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetAllCompanyType");
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        CompanyType ct = new CompanyType();
                        ct.TypeId = Convert.ToInt32(dataRow["company_type_id"]);
                        ct.TypeName = dataRow["company_type_name"].ToString();

                        ctList.Add(ct);
                    }
                    return ctList;
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/06
        /// removed existing connection open method
        /// call DataHandler class method and getting dataset object,
        /// create and return state object list using that dataset 
        /// </summary>
        /// <returns></returns>
        public List<State> GetAllStates()
        {
            List<State> stateList = new List<State>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetState");
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        State state = new State();
                        state.StateId = Convert.ToInt32(dataRow["state_id"]);
                        state.StateName = dataRow["state_name"].ToString();

                        stateList.Add(state);
                    }
                    return stateList;
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/17
        /// 
        /// Check company name is already exists in thd database
        /// 
        /// argument : companyName (string)
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/06
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting boolean value,
        /// 
        /// </summary>
        /// <returns>true/false</returns>
        public bool IsUniqueCompanyName(string companyName)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_name", companyName });
            try
            {
                return dataHandler.GetDataExistance("spIsUniqueCompanyName", paramertList);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/17
        /// 
        /// Get latest company code to create next company code
        /// 
        /// argument : companyName (string)
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/06
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting dataset object,
        /// return company code using dataset object  
        /// 
        /// </summary>
        /// <returns>true/false</returns>
        public string GetLatestCompanyCode(string prefix)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_code_prefix", prefix });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetCompanyCodebyCode", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    return dataSet.Tables[0].Rows[0]["company_code"].ToString();

                }
                else
                {
                    return "";
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/02/08
        /// 
        /// Get latest company code to create next company code
        /// 
        /// argument : companyName (string)
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/06
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting dataset object,
        /// return non registered company code using dataset object  
        /// 
        /// </summary>
        /// <returns>true/false</returns>
        public string GetLatestNonRegCompanyCode(string prefix)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_code_prefix", prefix });

            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetNonRegCompanyCodebyCode", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    return dataSet.Tables[0].Rows[0]["company_code"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 01/26/2016
        /// 
        /// Get company details
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/06
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting dataset object,
        /// return company object using dataset object  
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public Company GetCompanyDetailsCompanyId(int companyId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@company_id", companyId });
            try
            {
                Company company = new Company();
                DataSet dsCompany = dataHandler.GetDataSet("spGetCompanyDetailsCompanyId", paramertList);
                if (dsCompany != null && dsCompany.Tables.Count != 0)
                {
                    company.CompanyId = int.Parse(dsCompany.Tables[0].Rows[0]["company_Id"].ToString());
                    company.CompanyName = dsCompany.Tables[0].Rows[0]["company_name"].ToString();
                    company.CompanyCode = dsCompany.Tables[0].Rows[0]["company_code"].ToString().Trim();
                    company.CompanyAddress1 = dsCompany.Tables[0].Rows[0]["company_address_1"].ToString().Trim();

                    if (!string.IsNullOrEmpty(dsCompany.Tables[0].Rows[0]["company_address_2"].ToString()))
                    {
                        company.CompanyAddress2 = dsCompany.Tables[0].Rows[0]["company_address_2"].ToString().Trim();
                    }
                    
                    company.StateId = int.Parse(dsCompany.Tables[0].Rows[0]["stateId"].ToString());
                    company.City = dsCompany.Tables[0].Rows[0]["city"].ToString().Trim();
                    company.Zip = dsCompany.Tables[0].Rows[0]["zip"].ToString().Trim();

                    string[] zipWithExtention = company.Zip.Split('-');

                    if (zipWithExtention[0] != null) company.ZipPre = zipWithExtention[0];
                    if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) company.Extension = zipWithExtention[1];

                    company.Email = dsCompany.Tables[0].Rows[0]["email"].ToString().Trim();
                    company.PhoneNum1 = dsCompany.Tables[0].Rows[0]["phone_num_1"].ToString().Trim();
                    if (!string.IsNullOrEmpty(dsCompany.Tables[0].Rows[0]["phone_num_2"].ToString()))
                    {
                        company.PhoneNum2 = dsCompany.Tables[0].Rows[0]["phone_num_2"].ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(dsCompany.Tables[0].Rows[0]["phone_num_3"].ToString()))
                    {
                        company.PhoneNum3 = dsCompany.Tables[0].Rows[0]["phone_num_3"].ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(dsCompany.Tables[0].Rows[0]["fax"].ToString()))
                    {
                        company.Fax = dsCompany.Tables[0].Rows[0]["fax"].ToString();
                    }
                    //company.PhoneNum2 = dsCompany.Tables[0].Rows[0]["phone_num_2"].ToString().Trim();
                    //company.PhoneNum3 = dsCompany.Tables[0].Rows[0]["phone_num_3"].ToString().Trim();
                    //company.Fax = dsCompany.Tables[0].Rows[0]["fax"].ToString().Trim();
                    if (!string.IsNullOrEmpty(dsCompany.Tables[0].Rows[0]["website_url"].ToString()))
                    {
                        company.WebsiteUrl = dsCompany.Tables[0].Rows[0]["website_url"].ToString().Trim();
                    }
                    
                    company.TypeId = int.Parse(dsCompany.Tables[0].Rows[0]["company_type"].ToString());

                    return company;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 01/26/2016
        /// 
        /// Insert company in setup process
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/06
        /// removed existing connection open method and set parameter's to object list and pass stored procedure name to
        /// call DataHandler class to save company object
        /// 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int InsertCompany(Company company, string type)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_name", company.CompanyName ?? "" });
            paramertList.Add(new object[] { "@company_code", company.CompanyCode ?? "" });
            paramertList.Add(new object[] { "@company_address_1", company.CompanyAddress1 ?? "" });
            paramertList.Add(new object[] { "@company_address_2", company.CompanyAddress2 ?? "" });
            paramertList.Add(new object[] { "@stateId", company.StateId });
            paramertList.Add(new object[] { "@city", company.City ?? "" });
            paramertList.Add(new object[] { "@zip", company.Zip ?? "" });
            paramertList.Add(new object[] { "@email", company.Email ?? "" });
            paramertList.Add(new object[] { "@phone_num_1", company.PhoneNum1 ?? "" });
            paramertList.Add(new object[] { "@phone_num_2", company.PhoneNum2 ?? "" });
            paramertList.Add(new object[] { "@phone_num_3", company.PhoneNum3 ?? "" });
            paramertList.Add(new object[] { "@fax", company.Fax ?? "" });
            paramertList.Add(new object[] { "@website_url", company.WebsiteUrl ?? "" });
            paramertList.Add(new object[] { "@created_by", company.CreatedBy });
            paramertList.Add(new object[] { "@created_date", DateTime.Now });
            paramertList.Add(new object[] { "@company_type", company.TypeId });
            paramertList.Add(new object[] { "@first_super_admin_id", company.FirstSuperAdminId });
            paramertList.Add(new object[] { "@company_status", company.CompanyStatus });
            paramertList.Add(new object[] { "@transaction_type", type });

            try
            {
                return dataHandler.ExecuteSQLWithReturnVal("spInsertCompany", paramertList);
            }
            catch (Exception ex)
            {
                throw ex;
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
                    com3.Parameters.Add("@step_status", SqlDbType.Int).Value = 1;
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
                catch (Exception ex)
                {
                    throw ex;
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
                catch (Exception ex)
                {
                    throw ex;
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
                catch (SqlException ex)
                {
                    tran.Rollback();

                    throw ex;
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
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_name", company.CompanyName.Trim() });
            paramertList.Add(new object[] { "@company_code", company.CompanyCode.Trim() });
            paramertList.Add(new object[] { "@company_address_1", company.CompanyAddress1.Trim() });
            if (!string.IsNullOrEmpty(company.CompanyAddress2))
            {
                company.CompanyAddress2.Trim();
                paramertList.Add(new object[] { "@company_address_2", company.CompanyAddress2.Trim() });
            }
           
            paramertList.Add(new object[] { "@stateId", company.StateId });
            paramertList.Add(new object[] { "@city", company.City.Trim() });
            paramertList.Add(new object[] { "@zip", company.Zip.Trim() });
            if (!string.IsNullOrEmpty(company.Email))
            {
                company.Email.Trim();
            }
            if (!string.IsNullOrEmpty(company.PhoneNum2))
            {
                company.PhoneNum2.Trim();
            }
            if (!string.IsNullOrEmpty(company.PhoneNum3))
            {
                company.PhoneNum3.Trim();
            }
            paramertList.Add(new object[] { "@email", company.Email });
            paramertList.Add(new object[] { "@phone_num_1", company.PhoneNum1 });
            paramertList.Add(new object[] { "@phone_num_2", company.PhoneNum2 });
            paramertList.Add(new object[] { "@phone_num_3", company.PhoneNum3 });
            paramertList.Add(new object[] { "@fax", company.Fax });
            paramertList.Add(new object[] { "@website_url", company.WebsiteUrl });
            paramertList.Add(new object[] { "@created_by", company.CreatedBy });
            paramertList.Add(new object[] { "@created_date", DateTime.Now });
            paramertList.Add(new object[] { "@company_type", company.TypeId });
            paramertList.Add(new object[] { "@reg_company_id", company.CreatedByCompany });


            try
            {
                return dataHandler.ExecuteSQL("spInsertNonRegisteredCompany", paramertList);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// CreatedBy : Piyumi
        /// CreatedDate: 05/04/2016
        /// 
        /// Insert company in setup process 
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int InsertNonRegisteredCompanyAtDashboard(PartnerCompany company)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_name", company.CompanyName.Trim() });
            paramertList.Add(new object[] { "@company_code", company.CompanyCode.Trim() });
            paramertList.Add(new object[] { "@company_address_1", company.CompanyAddress1.Trim() });
            if (!string.IsNullOrEmpty(company.CompanyAddress2))
            {
                company.CompanyAddress2.Trim();
                paramertList.Add(new object[] { "@company_address_2", company.CompanyAddress2.Trim() });
            }

            paramertList.Add(new object[] { "@stateId", company.StateId });
            paramertList.Add(new object[] { "@city", company.City.Trim() });
            paramertList.Add(new object[] { "@zip", company.Zip.Trim() });
            if (!string.IsNullOrEmpty(company.Email))
            {
                company.Email.Trim();
            }
            if (!string.IsNullOrEmpty(company.PhoneNum2))
            {
                company.PhoneNum2.Trim();
            }
            if (!string.IsNullOrEmpty(company.PhoneNum3))
            {
                company.PhoneNum3.Trim();
            }
            paramertList.Add(new object[] { "@email", company.Email });
            paramertList.Add(new object[] { "@phone_num_1", company.PhoneNum1 });
            paramertList.Add(new object[] { "@phone_num_2", company.PhoneNum2 });
            paramertList.Add(new object[] { "@phone_num_3", company.PhoneNum3 });
            paramertList.Add(new object[] { "@fax", company.Fax });
            paramertList.Add(new object[] { "@website_url", company.WebsiteUrl });
            paramertList.Add(new object[] { "@created_by", company.CreatedBy });
            paramertList.Add(new object[] { "@created_date", DateTime.Now });
            paramertList.Add(new object[] { "@company_type", company.TypeId });
            paramertList.Add(new object[] { "@reg_company_id", company.RegCompanyId });


            try
            {
                return dataHandler.ExecuteSQLReturn("spInsertNonRegisteredCompanyAtDashboard", paramertList);
            }
            catch (Exception ex)
            {
                throw ex;
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

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@reg_company_id", regCompanyId });
            try
            {
                Company company = new Company();
                DataSet dsCompany = dataHandler.GetDataSet("spGetNonRegCompanyDetailsByRegCompanyId", paramertList);

                company.CompanyId = int.Parse(dsCompany.Tables[0].Rows[0]["company_Id"].ToString());
                company.CompanyName = dsCompany.Tables[0].Rows[0]["company_name"].ToString();
                company.CompanyCode = dsCompany.Tables[0].Rows[0]["company_code"].ToString();
                company.CompanyAddress1 = dsCompany.Tables[0].Rows[0]["company_address_1"].ToString();
                company.CompanyAddress2 = dsCompany.Tables[0].Rows[0]["company_address_2"].ToString();
                company.StateId = int.Parse(dsCompany.Tables[0].Rows[0]["stateId"].ToString());
                company.City = dsCompany.Tables[0].Rows[0]["city"].ToString();
                company.Zip = dsCompany.Tables[0].Rows[0]["zip"].ToString();

                string[] zipWithExtention = company.Zip.Split('-');

                if (zipWithExtention[0] != null) company.ZipPre = zipWithExtention[0];
                if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) company.Extension = zipWithExtention[1];

                company.Email = dsCompany.Tables[0].Rows[0]["email"].ToString();
                company.PhoneNum1 = dsCompany.Tables[0].Rows[0]["phone_num_1"].ToString().Trim();
                company.PhoneNum2 = dsCompany.Tables[0].Rows[0]["phone_num_2"].ToString().Trim();
                company.PhoneNum3 = dsCompany.Tables[0].Rows[0]["phone_num_3"].ToString().Trim();
                company.Fax = dsCompany.Tables[0].Rows[0]["fax"].ToString().Trim();
                company.WebsiteUrl = dsCompany.Tables[0].Rows[0]["website_url"].ToString();
                company.TypeId = int.Parse(dsCompany.Tables[0].Rows[0]["company_type"].ToString());

                return company;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        ///  CreatedBy : Asanka Senarathna
        ///  CreatedDate: 9/03/201
        ///  Pass company list object to addunit page
        /// </summary>
        /// <param name="regCompanyId"></param>
        /// <returns></returns>
        public List<Company> GetNonRegCompanyDetailsByRegCompanyId1(int regCompanyId)
        {
            List<Company> companyList = new List<Company>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@reg_company_id", regCompanyId });

            try
            {
                DataSet dsCompany = dataHandler.GetDataSet("spGetNonRegCompanyDetailsByRegCompanyId", paramertList);
                if (dsCompany != null && dsCompany.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dsCompany.Tables[0].Rows)
                    {
                        Company company = new Company();
                        company.CompanyId = int.Parse(dsCompany.Tables[0].Rows[0]["company_Id"].ToString());
                        company.CompanyName = dsCompany.Tables[0].Rows[0]["company_name"].ToString();
                        company.CompanyCode = dsCompany.Tables[0].Rows[0]["company_code"].ToString();
                        company.CompanyAddress1 = dsCompany.Tables[0].Rows[0]["company_address_1"].ToString();
                        company.CompanyAddress2 = dsCompany.Tables[0].Rows[0]["company_address_2"].ToString();
                        company.StateId = int.Parse(dsCompany.Tables[0].Rows[0]["stateId"].ToString());
                        company.City = dsCompany.Tables[0].Rows[0]["city"].ToString();
                        company.Zip = dsCompany.Tables[0].Rows[0]["zip"].ToString();
                        company.Email = dsCompany.Tables[0].Rows[0]["email"].ToString();
                        company.PhoneNum1 = dsCompany.Tables[0].Rows[0]["phone_num_1"].ToString();
                        company.PhoneNum2 = dsCompany.Tables[0].Rows[0]["phone_num_2"].ToString();
                        company.PhoneNum3 = dsCompany.Tables[0].Rows[0]["phone_num_3"].ToString();
                        company.Fax = dsCompany.Tables[0].Rows[0]["fax"].ToString();
                        companyList.Add(company);
                    }
                    return companyList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Company> GetCompanyByCreayedCompany(int createdByComId)
        {
            List<Company> nonRegCompanies = new List<Company>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@reg_company_id", createdByComId });
            
                try
                {
                    DataSet dsCompany = dataHandler.GetDataSet("spGetNonRegCompanyByCreatedCompany", paramertList);
                    if (dsCompany != null && dsCompany.Tables.Count != 0)
                    {
                        foreach (DataRow dataRow in dsCompany.Tables[0].Rows)
                        {
                           
                            Company company = new Company();
                            company.CompanyId = Convert.ToInt32(dataRow["company_id"]);
                            company.CompanyName = dataRow["company_name"].ToString().Trim();
                            company.CompanyCode = dataRow["company_code"].ToString().Trim();
                            company.CompanyAddress1 = dataRow["company_address_1"].ToString().Trim();
                            company.CompanyAddress2 = dataRow["company_address_2"].ToString().Trim();
                            company.StateId = Convert.ToInt32(dataRow["stateId"]);
                            company.City = dataRow["city"].ToString().Trim();
                            company.Zip = dataRow["zip"].ToString().Trim();

                            string[] zipWithExtention = company.Zip.Split('-');

                            if (zipWithExtention[0] != null) company.ZipPre = zipWithExtention[0];
                            if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) company.Extension = zipWithExtention[1];
                            else if (zipWithExtention.Count() == 1) company.Extension = "";

                            company.Email = dataRow["email"].ToString().Trim();
                            company.PhoneNum1 = dataRow["phone_num_1"].ToString().Trim();
                            company.PhoneNum2 = dataRow["phone_num_2"].ToString().Trim();
                            company.PhoneNum3 = dataRow["phone_num_3"].ToString().Trim();
                            company.Fax = dataRow["fax"].ToString().Trim();
                        company.WebsiteUrl = dataRow["website_url"] == null ? "" : dataRow["website_url"].ToString();

                            nonRegCompanies.Add(company);
                        }

                    }
                }
            catch (Exception ex)
            {
                throw ex;
            }


            return nonRegCompanies;
        }


        public Company GetNonRegCompanyByCompanyId(int companyId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_id", companyId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetNonRegCompanyByCompanyId", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[0];
                    Company nonRegCompany = new Company();

                    nonRegCompany.CompanyId = Convert.ToInt32(dataRow["company_id"]);
                    nonRegCompany.CompanyName = dataRow["company_name"].ToString();
                    nonRegCompany.CompanyCode = dataRow["company_code"].ToString();
                    nonRegCompany.CompanyAddress1 = dataRow["company_address_1"].ToString();
                    nonRegCompany.CompanyAddress2 = dataRow["company_address_2"].ToString();
                    nonRegCompany.StateId = Convert.ToInt32(dataRow["stateId"]);
                    nonRegCompany.City = dataRow["city"].ToString();
                    nonRegCompany.Zip = dataRow["zip"].ToString();

                    string[] zipWithExtention = nonRegCompany.Zip.Split('-');

                    if (zipWithExtention[0] != null) nonRegCompany.ZipPre = zipWithExtention[0];
                    if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) nonRegCompany.Extension = zipWithExtention[1];

                    nonRegCompany.Email = dataRow["email"].ToString();
                    nonRegCompany.PhoneNum1 = dataRow["phone_num_1"].ToString();
                    nonRegCompany.PhoneNum2 = dataRow["phone_num_2"].ToString();
                    nonRegCompany.PhoneNum3 = dataRow["phone_num_3"].ToString();
                    nonRegCompany.Fax = dataRow["fax"].ToString();



                    return nonRegCompany;
                }
                else
                {
                    return null;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:5/3/2016
        /// Update Company details
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public int UpdateCompany(Company company,int userId)
        {
            if (company != null)
            {
                DataHandler dataHandler = new DataHandler();
                List<object[]> paramertList = new List<object[]>();


                paramertList.Add(new object[] { "@company_id", company.CompanyId });
                paramertList.Add(new object[] { "@company_name", company.CompanyName });
                paramertList.Add(new object[] { "@address1", company.CompanyAddress1 });
                if (!string.IsNullOrEmpty(company.CompanyAddress2)){
                    paramertList.Add(new object[] { "@address2", company.CompanyAddress2 });
                }
                else
                {
                    paramertList.Add(new object[] { "@address2", null });
                }
                paramertList.Add(new object[] { "@city",company.City });
                paramertList.Add(new object[] { "@state", company.StateId });
                paramertList.Add(new object[] { "@zip", company.Zip });
                paramertList.Add(new object[] { "@phone_no", company.PhoneNum1 });
                if (!string.IsNullOrEmpty(company.PhoneNum2))
                {
                    paramertList.Add(new object[] { "@phone_no2", company.PhoneNum2 });
                }
                else
                {
                    paramertList.Add(new object[] { "@phone_no2", null });
                }
                if (!string.IsNullOrEmpty(company.PhoneNum3))
                {
                    paramertList.Add(new object[] { "@phone_no3", company.PhoneNum3 });
                }
                else
                {
                    paramertList.Add(new object[] { "@phone_no3", null });
                }

                if (!string.IsNullOrEmpty(company.Fax))
                {
                    paramertList.Add(new object[] { "@fax", company.Fax });
                }
                else
                {
                    paramertList.Add(new object[] { "@fax", null });
                }
                if (!string.IsNullOrEmpty(company.Email))
                {
                    paramertList.Add(new object[] { "@email", company.Email });
                }
                else
                {
                    paramertList.Add(new object[] { "@email", null });
                }
                if (!string.IsNullOrEmpty(company.WebsiteUrl))
                {
                    paramertList.Add(new object[] { "@web", company.WebsiteUrl });
                }
                else
                {
                    paramertList.Add(new object[] { "@web", null });
                }
                paramertList.Add(new object[] { "@modified_date", DateTime.Now });
                paramertList.Add(new object[] { "@modified_by", userId });

                try
                {
                    return dataHandler.ExecuteSQLReturn("spUpdateCompany", paramertList);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return 0;
            }
        }

        public List<PartnerCompany> GetNonRegCompanyDetailsByRegCompanyId2(int company_Id)
        {
            List<PartnerCompany> companyList = new List<PartnerCompany>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@reg_company_id", company_Id });

            try
            {
                DataSet dsCompany = dataHandler.GetDataSet("spGetNonRegCompanyDetailsByRegCompanyId", paramertList);
                if (dsCompany != null && dsCompany.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dsCompany.Tables[0].Rows)
                    {
                        PartnerCompany company = new PartnerCompany();
                        company.CompanyId = int.Parse(dataRow["company_Id"].ToString());
                        company.CompanyName = dataRow["company_name"].ToString();
                        company.CompanyCode = dataRow["company_code"].ToString();
                        company.CompanyAddress1 = dataRow["company_address_1"].ToString();
                        company.CompanyAddress2 = dataRow["company_address_2"].ToString();
                        company.StateId = int.Parse(dataRow["stateId"].ToString());
                        company.City = dataRow["city"].ToString();
                        company.Zip = dataRow["zip"].ToString();
                        string[] zipWithExtention = company.Zip.Split('-');

                        if (zipWithExtention[0] != null) company.ZipPre = zipWithExtention[0];
                        if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) company.Extension = zipWithExtention[1];

                        company.Email = dataRow["email"].ToString();
                        company.PhoneNum1 = dataRow["phone_num_1"].ToString();
                        if (!string.IsNullOrEmpty(dataRow["phone_num_2"].ToString()))
                        {
                            company.PhoneNum2 = dataRow["phone_num_2"].ToString().Trim();
                        }
                        if (!string.IsNullOrEmpty(dataRow["phone_num_3"].ToString()))
                        {
                            company.PhoneNum3 = dataRow["phone_num_3"].ToString().Trim();
                        }
                        if (!string.IsNullOrEmpty(dataRow["fax"].ToString()))
                        {
                            company.Fax = dataRow["fax"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dsCompany.Tables[0].Rows[0]["website_url"].ToString()))
                        {
                            company.WebsiteUrl = dataRow["website_url"].ToString().Trim();
                        }
                        company.TypeId = int.Parse(dataRow["company_type"].ToString());
                        company.CompanyType = (company.TypeId == 1) ? "Lender" : "Dealer";
                        companyList.Add(company);
                    }
                    return companyList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:5/5/2016
        /// Edit Partner Company
        /// </summary>
        /// <param name="partnerCompany"></param>
        /// <returns></returns>
        public int UpdatePartnerCompany(PartnerCompany company)
        {
            if (company != null)
            {
                DataHandler dataHandler = new DataHandler();
                List<object[]> paramertList = new List<object[]>();


                paramertList.Add(new object[] { "@company_name", company.CompanyName.Trim() });
                if (!string.IsNullOrEmpty(company.CompanyCode))
                {
                    paramertList.Add(new object[] { "@company_code", company.CompanyCode.Trim() });
                }
                    
                paramertList.Add(new object[] { "@company_id", company.CompanyId });
                paramertList.Add(new object[] { "@company_address_1", company.CompanyAddress1.Trim() });
                if (!string.IsNullOrEmpty(company.CompanyAddress2))
                {
                    company.CompanyAddress2.Trim();
                    paramertList.Add(new object[] { "@company_address_2", company.CompanyAddress2.Trim() });
                }

                paramertList.Add(new object[] { "@stateId", company.StateId });
                paramertList.Add(new object[] { "@city", company.City.Trim() });
                paramertList.Add(new object[] { "@zip", company.Zip.Trim() });
                if (!string.IsNullOrEmpty(company.Email))
                {
                    company.Email.Trim();
                }
                if (!string.IsNullOrEmpty(company.PhoneNum2))
                {
                    company.PhoneNum2.Trim();
                }
                if (!string.IsNullOrEmpty(company.PhoneNum3))
                {
                    company.PhoneNum3.Trim();
                }
                paramertList.Add(new object[] { "@email", company.Email });
                paramertList.Add(new object[] { "@phone_num_1", company.PhoneNum1 });
                paramertList.Add(new object[] { "@phone_num_2", company.PhoneNum2 });
                paramertList.Add(new object[] { "@phone_num_3", company.PhoneNum3 });
                paramertList.Add(new object[] { "@fax", company.Fax });
                paramertList.Add(new object[] { "@website_url", company.WebsiteUrl });
                paramertList.Add(new object[] { "@modified_by", company.CreatedBy });
                paramertList.Add(new object[] { "@modified_date", DateTime.Now });
                paramertList.Add(new object[] { "@company_type", company.TypeId });
                paramertList.Add(new object[] { "@reg_company_id", company.RegCompanyId });

                try
                {
                    return dataHandler.ExecuteSQLReturn("spUpdatePartnerCompany", paramertList);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}