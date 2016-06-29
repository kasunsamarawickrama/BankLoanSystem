using System;
using System.Collections.Generic;
using BankLoanSystem.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace BankLoanSystem.DAL
{
    public class DashBoardAccess
    {
        /// <summary>
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// Retrive User Level by userid
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns></returns>
        /// UpdatedBy : Asanka Senarathna
        public int GetUserLevelByUserId(int userId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@userId", userId });
            try
            {
                return dataHandler.ExecuteSQLReturn("spGetUserLevelByUserId", paramertList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy: Piyumi
        /// CreatedDate: 3/30/2016
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetLoanCount(int para, int roleId)
        {
            //int loanCount = 0;
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@para", para });
            paramertList.Add(new object[] { "@role", roleId });
            try
            {
                return dataHandler.ExecuteSQLReturn("spGetLoanCount", paramertList);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }



        /// <summary>
        /// Frontend page:    dashboard page
        /// title:              checking is atleast one permission for report access
        /// designed:           irfan mam
        /// User story:         DFP 476
        /// developed:          irfan mam
        /// date creaed:        6/23/2016
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// if there is no loan has user rights -> false
        ///  if there is atleast one user right for any loan -> true
        /// </returns>
        public bool IsAtleastOnePermissionForReport( int userId)
        {

            bool ret = false; // set ret value false as default 
            DataHandler dataHandler = new DataHandler(); 
            List<object[]> paramertList = new List<object[]>(); // argument list
           
            // add user id to argument list
            paramertList.Add(new object[] { "@user_id", userId });
            try
            {
                // if stored proceture return 1
                if( dataHandler.ExecuteSQLReturn("isAtleastOnePermissionForReport", paramertList)== 1)
                {
                    // set return value to true
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // return the ret value
            return ret;


        }


        /// <summary>
        /// Frontend page:      dashboard page
        /// title:              getting all loans and branch details
        /// designed:           irfan mam
        /// User story:         DFP 437
        /// developed:          irfan mam
        /// date creaed:        6/27/2016
        /// 
        /// </summary>
        /// 
        public List<DashboardGridModel> GetAllLoanBranchDetails(int comId, int branchId)
        {

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>(); // argument list 
            List<DashboardGridModel> gridList = new List<DashboardGridModel>();
            // add the arguments to list
            paramertList.Add(new object[] { "@company_id", comId });
            paramertList.Add(new object[] { "@branch_id", branchId });
            try
            {

                // execute stored procedure and get data list
                DataSet dataSet = dataHandler.GetDataSet("spGetFullDetailsforLoan", paramertList);
                
                // if data exists, bind it to a list
                if (dataSet != null && dataSet.Tables.Count != 0)
                {

                    foreach (DataRow dataRow in dataSet.Tables[0].Rows) {
                    DashboardGridModel dashboardGridModel = new DashboardGridModel();
                    dashboardGridModel.Id = Convert.ToInt32(dataRow["number"].ToString());
                    dashboardGridModel.BranchId = Convert.ToInt32(dataRow["branch_id"].ToString());
                    dashboardGridModel.BranchName = dataRow["branch_name"].ToString();
                    dashboardGridModel.PartnerBranchId = (dataRow.IsNull("non_reg_branch_id") ? -1 : Convert.ToInt32(dataRow["non_reg_branch_id"].ToString()));
                    dashboardGridModel.PartnerBranchName = (dataRow.IsNull("non_reg_branch_name") ? "" : dataRow["non_reg_branch_name"].ToString()); 
                    dashboardGridModel.Loanid = (dataRow.IsNull("loan_id") ? -1 : Convert.ToInt32(dataRow["loan_id"].ToString()));

                    dashboardGridModel.LoanNumber = (dataRow.IsNull("loan_number") ? "" : dataRow["loan_number"].ToString()); 
                    dashboardGridModel.TotalAmount = Convert.ToDecimal(dataRow.IsNull("loan_amount") ? "0.00" : dataRow["loan_amount"].ToString());
                    dashboardGridModel.UsedAmount = Convert.ToDecimal(dataRow.IsNull("used_amount") ? "0.00": dataRow["used_amount"].ToString()); 
                    dashboardGridModel.StatusId = Convert.ToInt32(dataRow["loan_status"].ToString());  
                    dashboardGridModel.Status = dataRow["loan_status_text"].ToString();
                    dashboardGridModel.StepNo = ( dataRow.IsNull("step_no") ? -1 : Convert.ToInt32(dataRow["step_no"].ToString()));
                    gridList.Add(dashboardGridModel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // return the list of loan branch details
            return gridList;
        }


        /// <summary>
        /// CreatedBy: Piyumi
        /// CreatedDate: 3/30/2016
        /// get loan details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns>Loan</returns>
        public Loan GetLoanDetails(int id,int role)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@para", id });
            paramertList.Add(new object[] { "@role", role });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetLoan", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    Loan loanObj = new Loan();
                    loanObj.LoanNumber = dataSet.Tables[0].Rows[0]["loan_number"].ToString();
                    //loanObj.PartnerName = dataSet.Tables[0].Rows[0]["company_name"].ToString()+" - " + dataSet.Tables[0].Rows[0]["non_reg_branch_name"].ToString(); comment by asanka
                    loanObj.PartnerName =  dataSet.Tables[0].Rows[0]["non_reg_branch_name"].ToString();
                    loanObj.LoanCode = dataSet.Tables[0].Rows[0]["loan_code"].ToString();

                    loanObj.PartnerType = int.Parse(dataSet.Tables[0].Rows[0]["company_type"].ToString());
                    loanObj.BranchName = dataSet.Tables[0].Rows[0]["branch_name"].ToString();
                    if (!string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["is_title_tracked"].ToString()))
                    {
                        if (bool.Parse(dataSet.Tables[0].Rows[0]["is_title_tracked"].ToString()))
                        {
                            loanObj.IsTitleTrack = 1;
                        }
                        else
                        {
                            loanObj.IsTitleTrack = 0;
                        }
                    }
                      
                    else
                    {
                        loanObj.IsTitleTrack = 0;
                    }
                    if (!string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["has_lot_inspection_fee"].ToString()))
                    {
                        if (bool.Parse(dataSet.Tables[0].Rows[0]["has_lot_inspection_fee"].ToString()))
                        {
                            loanObj.LotInspectionFee = 1;
                        }
                        else
                        {
                            loanObj.LotInspectionFee = 0;
                        }
                    }

                    else
                    {
                        loanObj.LotInspectionFee = 0;
                    }
                    if (!string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["has_monthly_loan_fee"].ToString()))
                    {
                        if (bool.Parse(dataSet.Tables[0].Rows[0]["has_monthly_loan_fee"].ToString()))
                        {
                            loanObj.MonthlyLoanFee = 1;
                        }
                        else
                        {
                            loanObj.MonthlyLoanFee = 0;
                        }
                    }

                    else
                    {
                        loanObj.MonthlyLoanFee = 0;
                    }
                    if (!string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["has_advance_fee"].ToString()))
                    {
                        if (bool.Parse(dataSet.Tables[0].Rows[0]["has_advance_fee"].ToString()))
                        {
                            loanObj.AdvanceFee = 1;
                        }
                        else
                        {
                            loanObj.AdvanceFee = 0;
                        }
                        if (!string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["payment_due_method"].ToString()))
                        {
                            if(dataSet.Tables[0].Rows[0]["payment_due_method"].ToString().Contains("Vehicle Payoff"))
                            {
                                loanObj.AdvanceFeePayAtPayoff = true;
                            }
                            else
                            {
                                loanObj.AdvanceFeePayAtPayoff = false;
                            }
                            
                        }
                    }

                    else
                    {
                        loanObj.AdvanceFee = 0;
                    }
                    if (role == 3)
                    {
                        if (!string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["right_id"].ToString()))
                        {
                            loanObj.Rights = dataSet.Tables[0].Rows[0]["right_id"].ToString();
                        }
                       
                    }

                    loanObj.NonRegBranchId = int.Parse(dataSet.Tables[0].Rows[0]["non_reg_branch_id"].ToString());
                    loanObj.LoanId = int.Parse(dataSet.Tables[0].Rows[0]["loan_id"].ToString());

                    return loanObj;

                }
                return null;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertUserInDashboard(User user)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            if (user != null)
            {
                paramertList.Add(new object[] { "@user_name", user.UserName });
                paramertList.Add(new object[] { "@password", user.Password });
                paramertList.Add(new object[] { "@first_name", user.FirstName });
                paramertList.Add(new object[] { "@last_name", user.LastName });
                paramertList.Add(new object[] { "@email", user.Email });
                paramertList.Add(new object[] { "@phone_no", user.PhoneNumber2 });
                paramertList.Add(new object[] { "@created_date", DateTime.Now });
                paramertList.Add(new object[] { "@created_by", user.CreatedBy});
                paramertList.Add(new object[] { "@company_id", user.Company_Id });
                paramertList.Add(new object[] { "@branch_id", user.BranchId });
                paramertList.Add(new object[] { "@role_id", user.RoleId});
                

                if (user.RoleId == 3)
                {
                    paramertList.Add(new object[] { "@rights", user.UserRights });
                    paramertList.Add(new object[] { "@report_rights", user.ReportRights });
                    paramertList.Add(new object[] { "@loan_id", user.LoanId });
                }
                else
                {
                    paramertList.Add(new object[] { "@rights",""});
                    paramertList.Add(new object[] { "@report_rights", "" });
                    paramertList.Add(new object[] { "@loan_id", 0 });
                }
                paramertList.Add(new object[] { "@step_status", user.step_status });
                paramertList.Add(new object[] { "@status", user.Status });
            }
           

            try
            {
                return dataHandler.ExecuteSQLReturn("spInsertDashboardUserDetails", paramertList);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}