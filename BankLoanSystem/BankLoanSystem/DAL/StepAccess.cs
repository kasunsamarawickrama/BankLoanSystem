using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class StepAccess
    {

        /// <summary>
        /// CreatedBy : kasun Samarawickrama
        /// CreatedDate: 2016/01/25
        /// 
        /// Get Step Nomber By UserId
        /// </summary>
        /// <returns>step number</returns>
        /// 
        public int getStepNumberByUserId(int userId)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    var command = new SqlCommand("spGetStepNumberByUserId", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userId);

                    SqlParameter returnParameter = command.Parameters.Add("@ReturnValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    con.Open();
                    command.ExecuteNonQuery();

                    return (int)returnParameter.Value;
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }

        /// <summary>
        /// CreatedBy : kasun Samarawickrama
        /// CreatedDate: 2016/01/26
        /// 
        /// Update Step Number By UserId
        /// </summary>
        /// <returns>update true/false</returns>
        /// 
        public bool updateStepNumberByUserId(int userId, int stepNumber)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    var command = new SqlCommand("spUpdateStepNumberByUserId", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@step_id", stepNumber);

                    SqlParameter returnParameter = command.Parameters.Add("@ReturnValue", SqlDbType.Bit);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    con.Open();
                    command.ExecuteNonQuery();
                    //if ((int)returnParameter.Value >=1) {
                    //    return true ;
                    //}
                    //else {
                    //    return false;
                    //}
                    return true;

                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }

        /// <summary>
        /// CreatedBy : kasun Samarawickrama
        /// CreatedDate: 2016/01/26
        /// 
        /// Update Step Number By UserId parameter 3 
        /// 
        /// this function will call after 6th process
        /// </summary>
        /// <returns>update true/false</returns>
        /// 
        public bool updateStepNumberByUserId(int userId, int stepNumber, int loanNumber)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    var command = new SqlCommand("spUpdateStepNumberByUserId", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@step_id", stepNumber);
                    command.Parameters.AddWithValue("@loan_id", loanNumber);

                    SqlParameter returnParameter = command.Parameters.Add("@ReturnValue", SqlDbType.Bit);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    con.Open();
                    command.ExecuteNonQuery();
                    if ((int)returnParameter.Value == 1)
                    {
                        return true;
                    }
                    else {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }

        /// <summary>
        /// CreatedBy : kasun Samarawickrama
        /// CreatedDate: 2016/01/27
        /// 
        /// check the User Login While Company Setup is ongoing by super Admin
        /// </summary>
        /// <returns>update true/false</returns>
        /// 
        public int checkUserLoginWhileCompanySetup(int userId)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    var command = new SqlCommand("spCheckUserLoginWhileCompanySetup", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userId);

                    SqlParameter returnParameter = command.Parameters.Add("@ReturnValue", SqlDbType.Bit);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    con.Open();
                    command.ExecuteNonQuery();
                    return (int)returnParameter.Value;
                  
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }
        
        /// <summary>
        /// CreatedBy : kasun Samarawickrama
        /// CreatedDate: 2016/02/03
        /// 
        /// get Branchs by company code 
        /// </summary>
        /// <returns>branches list</returns>
        /// 
        public IList<Branch> getBranchesByCompanyCode(string companyCode)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetBranchesByCompanyCode", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@company_code", SqlDbType.VarChar).Value = companyCode;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        List<Branch> branchesLists = new List<Branch>();


                        while (reader.Read())
                        {
                            Branch branch = new Branch();
                            branch.BranchId = int.Parse(reader["branch_id"].ToString());
                            branch.BranchName = reader["branch_name"].ToString();
                            branch.BranchCode = reader["branch_code"].ToString();
                            branch.BranchAddress1 = reader["branch_address_1"].ToString();
                            branch.BranchAddress2 = reader["branch_address_2"].ToString();
                            branch.StateId = int.Parse(reader["state_id"].ToString());
                            branch.BranchCity = reader["city"].ToString();
                            branch.BranchZip = reader["zip"].ToString();
                            branch.BranchEmail = reader["email"].ToString();
                            branch.BranchPhoneNum1 = reader["phone_num_1"].ToString();
                            branch.BranchPhoneNum2 = reader["phone_num_2"].ToString();
                            branch.BranchPhoneNum3 = reader["phone_num_3"].ToString();
                            branch.BranchFax = reader["fax"].ToString();
                            branchesLists.Add(branch);

                        }
                        return branchesLists;

                    }

                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }

        public bool InsertFeesDetails(Fees fees)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("spInsertFeesDetails", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@advance_fee_amount", SqlDbType.Float).Value = fees.AdvanceAmount;
                        command.Parameters.Add("@advance_receipt", SqlDbType.Bit).Value = fees.AdvanceNeedReceipt;
                        command.Parameters.Add("@advance_payment_due_method", SqlDbType.NVarChar).Value = fees.AdvanceDue;
                        command.Parameters.Add("@advance_payment_due_date", SqlDbType.NVarChar).Value = fees.AdvanceDueDate;
                        command.Parameters.Add("@advance_auto_remind_dealer_email", SqlDbType.NVarChar).Value = fees.AdvanceDealerEmail;
                        command.Parameters.Add("@advance_delaer_remind_period", SqlDbType.NVarChar).Value = fees.AdvanceDealerEmailRemindPeriod;
                        command.Parameters.Add("@advance_auto_remind_lender_email", SqlDbType.NVarChar).Value = fees.AdvanceLenderEmail;
                        command.Parameters.Add("@advance_lender_remind_period", SqlDbType.NVarChar).Value = fees.AdvanceLenderEmailRemindPeriod;

                        command.Parameters.Add("@monthly_loan_fee_amount", SqlDbType.Float).Value = fees.MonthlyLoanAmount;
                        command.Parameters.Add("@monthly_loan_receipt", SqlDbType.Bit).Value = fees.MonthlyLoanNeedReceipt;
                        command.Parameters.Add("@monthly_loan_payment_due_method", SqlDbType.NVarChar).Value = fees.MonthlyLoanDue;
                        command.Parameters.Add("@monthly_loan_payment_due_date", SqlDbType.NVarChar).Value = fees.MonthlyLoanDueDate;
                        command.Parameters.Add("@monthly_loan_auto_remind_dealer_email", SqlDbType.NVarChar).Value = fees.MonthlyLoanDealerEmail;
                        command.Parameters.Add("@monthly_loan_delaer_remind_period", SqlDbType.NVarChar).Value = fees.MonthlyLoanDealerEmailRemindPeriod;
                        command.Parameters.Add("@monthly_loan_auto_remind_lender_email", SqlDbType.NVarChar).Value = fees.MonthlyLoanLenderEmail;
                        command.Parameters.Add("@monthly_loan_lender_remind_period", SqlDbType.NVarChar).Value = fees.MonthlyLoanLenderEmailRemindPeriod;

                        command.Parameters.Add("@lot_inspection_amount", SqlDbType.Float).Value = fees.LotInspectionAmount;
                        command.Parameters.Add("@lot_inspection_receipt", SqlDbType.Bit).Value = fees.LotInspectionNeedReceipt;
                        command.Parameters.Add("@lot_payment_due_method", SqlDbType.NVarChar).Value = fees.LotInspectionDue;
                        command.Parameters.Add("@lot_payment_due_date", SqlDbType.NVarChar).Value = "9";//fees.MonthlyLoanDueDate;
                        command.Parameters.Add("@lot_inspection_auto_remind_dealer_email", SqlDbType.NVarChar).Value = fees.LotInspectionDealerEmail;
                        command.Parameters.Add("@lot_inspection_delaer_remind_period", SqlDbType.NVarChar).Value = fees.LotInspectionDealerEmailRemindPeriod;
                        command.Parameters.Add("@lot_inspection_auto_remind_lender_email", SqlDbType.NVarChar).Value = fees.LotInspectionLenderEmail;
                        command.Parameters.Add("@lot_inspection_lender_remind_period", SqlDbType.NVarChar).Value = fees.LotInspectionLenderEmailRemindPeriod;

                        command.Parameters.Add("@loan_id", SqlDbType.Int).Value = fees.LoanId;

                        SqlParameter returnParameter = command.Parameters.Add("@ReturnValue", SqlDbType.Bit);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        con.Open();
                        command.ExecuteNonQuery();
                        if ((int)returnParameter.Value == 1)
                        {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
           
        }

    }
}