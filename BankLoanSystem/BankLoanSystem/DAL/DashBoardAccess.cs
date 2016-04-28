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
                    loanObj.PartnerName = dataSet.Tables[0].Rows[0]["company_name"].ToString();
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
                    paramertList.Add(new object[] { "@loan_id", user.LoanId });
                }
                else
                {
                    paramertList.Add(new object[] { "@rights",""});
                    paramertList.Add(new object[] { "@loan_id", 0 });
                }
                paramertList.Add(new object[] { "@step_status", user.step_status });
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