using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BankLoanSystem.DAL
{
    
    public class BranchAccess
    {

    /// <summary>
    /// CreatedBy: Piyumi
    /// CreatedDate: 4/4/2016
    /// Get branches at least one loan assigned
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
        public List<Branch> GetLoansBranches(int companyId)
        {
            List<Branch> branchesLists = new List<Branch>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_id", companyId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetLoanBranchesByCompanyId", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Branch branch = new Branch();
                        branch.BranchId = Convert.ToInt32(dataRow["branch_id"].ToString());
                        branch.BranchName = dataRow["branch_name"].ToString();
                        //branch.LoanId = int.Parse(dataRow["loan_id"].ToString());
                        //branch.LoanNumber = dataRow["loan_number"].ToString();
                        branchesLists.Add(branch);
                    }

                    return branchesLists;
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
        /// CreatedBy: MAM. IRFAN
        /// CreatedDate: 2016/01/16
        /// 
        /// Getting all branches
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/04
        /// removed existing connection open method and set parameter to object list and pass stored procedure name to
        /// call DataHandler class method and getting dataset object,
        /// create and return branche object list using that dataset
        /// 
        /// </summary>
        /// <returns> a list contain all branches</returns>
        /// 
        public List<Branch> getBranches(int companyId)
        {
            List<Branch> branchesLists = new List<Branch>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@companyId", companyId });
            try
            {
            DataSet dataSet = dataHandler.GetDataSet("spGetBranchesByCompanyId", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    Branch branch = new Branch();
                    branch.BranchId = Convert.ToInt32(dataRow["branch_id"].ToString());
                    branch.BranchName = dataRow["branch_name"].ToString();
                    branch.BranchCode = dataRow["branch_code"].ToString();
                    branch.BranchAddress1 = dataRow["branch_address_1"].ToString();

                    branchesLists.Add(branch);
                }

                return branchesLists;
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
        /// CreatedDate:2016/1/27
        /// get company Id by company code
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/04
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting dataset object,
        /// return company id using dataset object 
        /// 
        /// 
        /// </summary>
        /// <param name="id">company code</param>
        /// <returns>compId</returns>
        public int getCompanyIdByCompanyCode(string compCode)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_code", compCode });
            try
            {
            DataSet dataSet = dataHandler.GetDataSet("spGetCompanyIdByCompanyCode", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                return int.Parse(dataSet.Tables[0].Rows[0]["company_id"].ToString());

            }
            return 0;
        }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/27
        /// get company Id by company code
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/04
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting dataset object,
        /// return partner company id using dataset object 
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>compId</returns>
        public int getNonRegCompanyIdByCompanyCode(string compCode)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_code", compCode });
            try
            {
            DataSet dataSet = dataHandler.GetDataSet("spGetNonRegCompanyIdByCompanyCode", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                return int.Parse(dataSet.Tables[0].Rows[0]["company_id"].ToString());

            }
            return 0;
        }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy: Piyumi
        /// CreatedDate: 2016/1/17
        /// Get CompanyId By UserId
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/04
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting dataset object,
        /// return company id using dataset object 
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>compId</returns>
        public int getCompanyIdByUserId(int id)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", id });
            try
            {
            DataSet dataSet = dataHandler.GetDataSet("spGetCompanyCodeByUserId", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                return int.Parse(dataSet.Tables[0].Rows[0]["company_id"].ToString());

            }
                return 0;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy:piyumi
        /// CreatedDate:2016/1/27
        /// update branch id when first branch is created
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/03
        /// removed existing connection open method and set parameter's to object list and pass stored procedure name to
        /// call DataHandler class to update given branch details 
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool updateUserBranchId(CompanyBranchModel nonRegBranch, int userId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", userId });
            paramertList.Add(new object[] { "@branch_code", nonRegBranch.MainBranch.BranchCode });

            try
            {
                return dataHandler.ExecuteSQL("spUpdateBranchId", paramertList) ? true : false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/27
        /// Get company type of a given user
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/04
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting dataset object,
        /// return company type id using dataset object  
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int getCompanyTypeByUserId(int userId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", userId });
            try
            {
            DataSet dataSet = dataHandler.GetDataSet("spGetCompanyTypeByUserId", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                return int.Parse(dataSet.Tables[0].Rows[0]["company_type"].ToString());

            }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy : kasun Samarawickrama
        /// CreatedDate: 2016/02/03
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/04
        /// removed existing connection open method and set parameter to object list and pass stored procedure name to
        /// call DataHandler class method and getting dataset object,
        /// create and return branch object list using that dataset          
        /// 
        /// 
        /// get Branchs by company code 
        /// </summary>
        /// <returns>branches list</returns>
        /// 
        public IList<Branch> getBranchesByCompanyCode(string companyCode)
        {
            List<Branch> branchesLists = new List<Branch>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_code", companyCode });
            try
            {
            DataSet dataSet = dataHandler.GetDataSet("spGetBranchesByCompanyCode", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    Branch branch = new Branch();
                    branch.BranchId = Convert.ToInt32(dataRow["branch_id"].ToString());
                    branch.BranchName = dataRow["branch_name"].ToString();
                    branch.BranchCode = dataRow["branch_code"].ToString();
                    branch.BranchAddress1 = dataRow["branch_address_1"].ToString();
                    branch.BranchAddress2 = dataRow["branch_address_2"].ToString();
                    branch.StateId = int.Parse(dataRow["state_id"].ToString());
                    branch.BranchCity = dataRow["city"].ToString();
                    branch.BranchZip = dataRow["zip"].ToString();

                    string[] zipWithExtention = branch.BranchZip.Split('-');
                    if (zipWithExtention[0] != null) branch.ZipPre = zipWithExtention[0];
                    if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) branch.Extention = zipWithExtention[1];
                    else if(zipWithExtention.Count() == 1)
                        {
                            branch.Extention = "";
                        }

                            branch.BranchEmail = dataRow["email"].ToString();
                    branch.BranchPhoneNum1 = dataRow["phone_num_1"].ToString();
                    branch.BranchPhoneNum2 = dataRow["phone_num_2"].ToString();
                    branch.BranchPhoneNum3 = dataRow["phone_num_3"].ToString();
                    branch.BranchFax = dataRow["fax"].ToString();

                    branchesLists.Add(branch);
                }

                return branchesLists;
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
        /// CreatedBy: MAM. IRFAN
        /// CreatedDate: 2016/02/05
        /// 
        /// Getting all non reistered company branches by Registered company id
        /// 
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/04
        /// removed existing connection open method and set parameter to object list and pass stored procedure name to
        /// call DataHandler class method and getting dataset object,
        /// create and return partner branch object list using that dataset   
        /// 
        /// </summary>
        /// <returns> a list contain all branches</returns>
        /// 
        public List<NonRegBranch> getNonRegBranches(int companyId)
        {
            List<NonRegBranch> branchesLists = new List<NonRegBranch>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@companyId", companyId });
            try
            {
            DataSet dataSet = dataHandler.GetDataSet("spGetNonRegBranchesByCompanyId", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    NonRegBranch branch = new NonRegBranch();

                    branch.NonRegBranchId = int.Parse(dataRow["non_reg_branch_id"].ToString());

                    branch.BranchName = dataRow["branch_name"].ToString();
                    branch.BranchCode = dataRow["branch_code"].ToString();
                    branch.BranchId = int.Parse(dataRow["branch_id"].ToString());
                    branch.BranchAddress1 = dataRow["branch_address_1"].ToString();
                    branch.BranchAddress2 = dataRow["branch_address_2"].ToString();
                    branch.StateId = Convert.ToInt32(dataRow["state_id"].ToString());
                    branch.BranchCity = dataRow["city"].ToString();
                    branch.BranchZip = dataRow["zip"].ToString();

                    string[] zipWithExtention = branch.BranchZip.Split('-');

                    if (zipWithExtention[0] != null) branch.ZipPre = zipWithExtention[0];
                        if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) branch.Extention = zipWithExtention[1];
                        else if (zipWithExtention.Count() == 1) branch.Extention = "";

                    branch.CompanyNameBranchName = dataRow["company_name"].ToString() + " - "  +dataRow["branch_name"].ToString();
                    branch.BranchEmail = dataRow["email"].ToString();
                    branch.BranchPhoneNum1 = dataRow["phone_num_1"].ToString();
                    branch.BranchPhoneNum2 = dataRow["phone_num_2"].ToString();
                    branch.BranchPhoneNum3 = dataRow["phone_num_3"].ToString();
                    branch.BranchFax = dataRow["fax"].ToString();
                    branch.BranchCompany = Convert.ToInt32(dataRow["company_id"]);
                    branchesLists.Add(branch);
                }
                return branchesLists;
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
        /// CreatedBy: MAM. IRFAN
        /// CreatedDate: 2016/02/05
        /// 
        /// Getting all non reistered company branches by Registered company id
        /// 
        /// </summary>
        /// <returns> a list contain all branches</returns>
        /// 
        public List<NonRegBranch> getNonRegBranchesNonCompId(int nonRegCompanyId)
        {
            List<NonRegBranch> branchesLists = new List<NonRegBranch>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@non_reg_companyId", nonRegCompanyId });
            try
            {
            DataSet dataSet = dataHandler.GetDataSet("spGetNonRegBranchesByNonRegCompanyId", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    NonRegBranch branch = new NonRegBranch();
                    branch.NonRegBranchId = int.Parse(dataRow["non_reg_branch_id"].ToString());

                    branch.BranchName = dataRow["branch_name"].ToString();
                    branch.BranchCode = dataRow["branch_code"].ToString();
                    branch.BranchId = int.Parse(dataRow["branch_id"].ToString());
                    branch.BranchAddress1 = dataRow["branch_address_1"].ToString();
                    branch.BranchAddress2 = dataRow["branch_address_2"].ToString();
                    branch.StateId = Convert.ToInt32(dataRow["state_id"].ToString());
                    branch.BranchCity = dataRow["city"].ToString();
                    branch.BranchZip = dataRow["zip"].ToString();
                    branch.CompanyNameBranchName = dataRow["company_name"].ToString() + " - " + dataRow["branch_name"].ToString();
                    string[] zipWithExtention = branch.BranchZip.Split('-');

                    if (zipWithExtention[0] != null) branch.ZipPre = zipWithExtention[0];
                    if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) branch.Extention = zipWithExtention[1];

                    branch.BranchEmail = dataRow["email"].ToString();
                    branch.BranchPhoneNum1 = dataRow["phone_num_1"].ToString();
                    branch.BranchPhoneNum2 = dataRow["phone_num_2"].ToString();
                    branch.BranchPhoneNum3 = dataRow["phone_num_3"].ToString();
                    branch.BranchFax = dataRow["fax"].ToString();
                    branch.BranchCompany = Convert.ToInt32(dataRow["company_id"]);
                    branchesLists.Add(branch);
                }

                return branchesLists;
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
        /// CreatedBy:Irfan
        /// CreatedDate:2016/02/11
        /// Get Branch by branch Id
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/04
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting dataset object,
        /// return branch object using dataset object   
        /// 
        /// </summary>
        /// <param name="branch Id"></param>
        /// <returns></returns>
        public Branch getBranchByBranchId(int branchId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@branch_id", branchId });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetBranchByBranchId", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[0];
                    Branch branch = new Branch();

                    branch.BranchCode = dataRow["branch_code"].ToString();
                    branch.BranchId = int.Parse(dataRow["branch_id"].ToString());
                    branch.BranchName = dataRow["branch_name"].ToString();

                    return branch;
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
        /// CreatedBy:Irfan
        /// CreatedDate:2016/02/11
        /// Get Non Reg Branch by non reg branch Id
        /// 
        /// UpdatedBy : nadeeka
        /// UpdatedDate: 2016/03/04
        /// removed existing connection open method and set parameter to object list and pass stored procedure name
        /// call DataHandler class method and getting dataset object,
        /// return partner branch object using dataset object    
        /// 
        /// </summary>
        /// <param name="branch Id"></param>
        /// <returns></returns>
        public NonRegBranch getNonRegBranchByNonRegBranchId(int nonRegBranchId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@non_reg_branch_id", nonRegBranchId });
            try
            {
            DataSet dataSet = dataHandler.GetDataSet("spGetNonRegBranchByNonRegBranchId", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[0];
                    NonRegBranch branch = new NonRegBranch();
                    branch.BranchId = int.Parse(dataRow["branch_id"].ToString());
                    branch.NonRegBranchId = int.Parse(dataRow["non_reg_branch_id"].ToString());
                    branch.BranchCode = dataRow["branch_code"].ToString();
                    branch.BranchName = dataRow["branch_name"].ToString();
                    branch.BranchAddress1 = dataRow["branch_address_1"].ToString();
                    branch.BranchAddress2 = dataRow["branch_address_2"].ToString();
                    branch.BranchCity = dataRow["city"].ToString();
                    branch.CompanyNameBranchName = dataRow["company_name"].ToString();
                    branch.NonRegCompanyId = int.Parse(dataRow["company_id"].ToString());
                    branch.BranchEmail = dataRow["email"].ToString();
                    return branch;
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
        /// CreatedDate:2016/1/17
        /// Create a branch code for a company
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns>branchCode</returns>
        public string createBranchCode(string companyCode)
        {
            try
            {
                string branchCode = "";
                string latestBranchCode = getLatestBranchCode(companyCode);
                int latestBranchId = 0;

                if (latestBranchCode != "")
                {
                    string[] s = latestBranchCode.Split('_');
                    latestBranchId = int.Parse(s[1]);
                }


                if ((latestBranchId >= 0) && (latestBranchId < 9))
                {
                    branchCode = companyCode + "_0" + (latestBranchId + 1).ToString();
                }
                else
                {
                    branchCode = companyCode + "_" + (latestBranchId + 1).ToString();
                }

                return branchCode;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/17
        /// Get branchId of a company
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns>topId</returns>
        public string getLatestBranchCode(string companyCode)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_code", companyCode });
            try
            {
            DataSet dataSet = dataHandler.GetDataSet("spGetTopBranchIdByCompanyCode", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                return dataSet.Tables[0].Rows[0]["branch_code"].ToString();

            }
            return "";
        }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/17
        /// Get branchId of a company
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns>topId</returns>
        public string getLatestNonRegBranchCode(string companyCode)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_code", companyCode });
            try {
            DataSet dataSet = dataHandler.GetDataSet("spGetTopNonRegBranchIdByCompanyCode", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                return dataSet.Tables[0].Rows[0]["branch_code"].ToString();

            }
            return "";
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/17
        /// Create a branch code for a company
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns>branchCode</returns>
        public string createNonRegBranchCode(string companyCode)
        {
            try
            {
                string branchCode = "";
                string latestBranchCode = getLatestNonRegBranchCode(companyCode);
                int latestBranchId = 0;
                if (latestBranchCode != "")
                {
                    string[] s = latestBranchCode.Split('_');
                    latestBranchId = int.Parse(s[1]);
                }


                if ((latestBranchId >= 0) && (latestBranchId < 9))
                {
                    branchCode = companyCode + "_0" + (latestBranchId + 1).ToString();
                }
                else
                {
                    branchCode = companyCode + "_" + (latestBranchId + 1).ToString();
                }

                return branchCode;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy : nadeeka
        /// UpdatedDate: 2016/03/04
        /// set parameter's to object list and pass stored procedure name to DataHandler class to save branch object
        /// 
        /// </summary>
        /// <param name="branch">branch object</param>
        /// <param name="id"> user id</param>
        /// <returns></returns>
        public int insertBranch(Branch branch, int id)
        {
            if (string.IsNullOrEmpty(branch.BranchCode))
            {
                branch.BranchCode = createBranchCode(getCompanyCodeByUserId(id));
            }

            //branch.BranchCompany = getCompanyIdByUserId(id);

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", id });
            paramertList.Add(new object[] { "@branch_code", branch.BranchCode });
            paramertList.Add(new object[] { "@branch_name", branch.BranchName });
            paramertList.Add(new object[] { "@branch_address_1", branch.BranchAddress1 });
            paramertList.Add(new object[] { "@branch_address_2", branch.BranchAddress2 ?? "" });
            paramertList.Add(new object[] { "@state_id", branch.StateId });
            paramertList.Add(new object[] { "@city", branch.BranchCity });
            if ((branch.Extention != null) && (branch.Extention.ToString() != ""))
            {
                branch.BranchZip = branch.ZipPre + "-" + branch.Extention;
            }
            else
            {
                branch.BranchZip = branch.ZipPre;
            }
            paramertList.Add(new object[] { "@zip", branch.BranchZip });
            paramertList.Add(new object[] { "@email", branch.BranchEmail });
            paramertList.Add(new object[] { "@phone_num_1", branch.BranchPhoneNum1 });
            paramertList.Add(new object[] { "@phone_num_2", branch.BranchPhoneNum2 ?? "" });
            paramertList.Add(new object[] { "@phone_num_3", branch.BranchPhoneNum3 ?? "" });
            paramertList.Add(new object[] { "@fax", branch.BranchFax ?? "" });
            paramertList.Add(new object[] { "@created_date", DateTime.Now });
            paramertList.Add(new object[] { "@company_id", branch.BranchCompany });

            try
            {
                return dataHandler.ExecuteSQLReturn("spInsertBranch", paramertList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy: Piyumi
        /// CreatedDate: 2016/1/17
        /// Insert branch details
        /// </summary>
        /// <param name="branch object"></param>
        /// <param name="id"></param>
        /// <returns>true/false</returns>
        public int insertBranchDetails(Branch branch, int id)
        {
            branch.BranchCode = createBranchCode(getCompanyCodeByUserId(id));
            branch.BranchCompany = getCompanyIdByUserId(id);
            branch.BranchCreatedDate = DateTime.Now;

            return this.insertBranch(branch, id);
        }

        /// <summary>
        /// CreatedBy: Piyumi
        /// CreatedDate: 2016/1/26
        /// Insert branch details
        /// </summary>
        /// <param name="branch object"></param>
        /// <param name="userCompany3"></param>
        /// <param name="id"></param>
        /// <returns>true/false</returns>
        public int insertFirstBranchDetails(CompanyBranchModel userCompany3, int id)
        {
            string companyCode = userCompany3.Company.CompanyCode;
            //userCompany3.MainBranch.BranchCode = createBranchCode(companyCode);
            userCompany3.MainBranch.BranchCompany = getCompanyIdByCompanyCode(companyCode);
            userCompany3.MainBranch.BranchCreatedDate = DateTime.Now;

            return this.insertBranch(userCompany3.MainBranch, id);
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/27
        /// Insert non registered branch
        /// </summary>
        /// <param name="nonRegBranch"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int insertNonRegBranchDetails(CompanyBranchModel nonRegBranch, int userId)
        {
            nonRegBranch.MainBranch.BranchCreatedDate = DateTime.Now;
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", userId });
            paramertList.Add(new object[] { "@branch_code", nonRegBranch.MainBranch.BranchCode.Trim() });
            paramertList.Add(new object[] { "@branch_name", nonRegBranch.MainBranch.BranchName.Trim() });
            paramertList.Add(new object[] { "@branch_address_1", nonRegBranch.MainBranch.BranchAddress1.Trim() });
            paramertList.Add(new object[] { "@branch_address_2", nonRegBranch.MainBranch.BranchAddress2 ?? "" });
            paramertList.Add(new object[] { "@state_id", nonRegBranch.MainBranch.StateId });
            paramertList.Add(new object[] { "@city", nonRegBranch.MainBranch.BranchCity ?? "" });
            if ((nonRegBranch.MainBranch.Extention != null) && (nonRegBranch.MainBranch.Extention.ToString() != ""))
            {
                nonRegBranch.MainBranch.BranchZip = nonRegBranch.MainBranch.ZipPre + "-" + nonRegBranch.MainBranch.Extention;
            }
            else
            {
                nonRegBranch.MainBranch.BranchZip = nonRegBranch.MainBranch.ZipPre;
            }
            paramertList.Add(new object[] { "@zip", nonRegBranch.MainBranch.BranchZip.Trim() });
            paramertList.Add(new object[] { "@email", nonRegBranch.MainBranch.BranchEmail ?? "" });
            paramertList.Add(new object[] { "@phone_num_1", nonRegBranch.MainBranch.BranchPhoneNum1.Trim() });
            paramertList.Add(new object[] { "@phone_num_2", nonRegBranch.MainBranch.BranchPhoneNum2 ?? "" });
            paramertList.Add(new object[] { "@phone_num_3", nonRegBranch.MainBranch.BranchPhoneNum3 ?? "" });
            paramertList.Add(new object[] { "@fax", nonRegBranch.MainBranch.BranchFax ?? "" });
            paramertList.Add(new object[] { "@created_date", DateTime.Now });
            paramertList.Add(new object[] { "@company_id", nonRegBranch.MainBranch.BranchCompany });
            paramertList.Add(new object[] { "@branch_id", nonRegBranch.MainBranch.BranchCreatedBy });

            try
            {
                return dataHandler.ExecuteSQLReturn("spInsertNonRegisteredBranch", paramertList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:17/1/2016
        /// Get companyId by userId 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BranchCompanyCode</returns>
        public string getCompanyCodeByUserId(int id)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", id });
            try {
            DataSet dataSet = dataHandler.GetDataSet("spGetCompanyCodeByUserId", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                return dataSet.Tables[0].Rows[0]["company_code"].ToString();

            }
                return "";
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  List<Branch> GetLoansByBranches(int branchIdL)
        {
            List<Branch> branchesLists = new List<Branch>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@branch_id", branchIdL });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetLoanByBranchId", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Branch branch = new Branch();
                        branch.BranchId = Convert.ToInt32(dataRow["branch_id"].ToString());
                        branch.BranchName = dataRow["branch_name"].ToString();
                        branch.LoanId = int.Parse(dataRow["loan_id"].ToString());
                        branch.LoanNumber = dataRow["loan_number"].ToString();
                        branchesLists.Add(branch);
                    }

                    return branchesLists;
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
        /// CreatedDate:4/22/2016
        /// Get all branches by companyId
        /// </summary>
        /// <param name="company_Id"></param>
        /// <returns></returns>
        public List<Branch> GetBranchesByCompanyId(int company_Id)
        {
            List<Branch> branchesLists = new List<Branch>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@companyId", company_Id });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetBranchesByCompanyId", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Branch branch = new Branch();

                        branch.BranchId = int.Parse(dataRow["branch_id"].ToString());

                        branch.BranchName = dataRow["branch_name"].ToString();
                        branch.BranchCode = dataRow["branch_code"].ToString();
                        //branch.BranchId = int.Parse(dataRow["branch_id"].ToString());
                        branch.BranchAddress1 = dataRow["branch_address_1"].ToString();
                       
                        branch.BranchCity = dataRow["city"].ToString();

                        if (!string.IsNullOrEmpty(branch.BranchAddress1) && !string.IsNullOrEmpty(branch.BranchCity))
                        {
                            branch.BranchNameAddress = branch.BranchName + "-" + branch.BranchAddress1 + "," + branch.BranchCity;
                        }
                        else
                        {
                            branch.BranchNameAddress = branch.BranchName;
                        }
                      
                        branchesLists.Add(branch);
                    }
                    return branchesLists;
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
    }
}