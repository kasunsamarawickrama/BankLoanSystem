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
        //public int getStepNumberByUserId(int userId)
        //{
        //    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
        //    {
        //        try
        //        {
        //            var command = new SqlCommand("spGetStepNumberByUserId", con);
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@user_id", userId);

        //            SqlParameter returnParameter = command.Parameters.Add("@ReturnValue", SqlDbType.Int);
        //            returnParameter.Direction = ParameterDirection.ReturnValue;

        //            con.Open();
        //            command.ExecuteNonQuery();

        //            return (int)returnParameter.Value;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;

        //        }
        //    }
        //}


        /// <summary>
        /// CreatedBy : kasun Samarawickrama
        /// CreatedDate: 2016/01/25
        /// 
        /// Get Step Nomber By UserId
        /// </summary>
        /// <returns>step number</returns>
        /// 
        public int getBranchIdByUserId(int userId)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    var command = new SqlCommand("spGetBranchIdByUserId", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userId);

                    SqlParameter returnParameter = command.Parameters.Add("@Return", SqlDbType.Int);
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
                    if ((int)returnParameter.Value >= 1)
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
        /// CreatedDate: 2016/01/26
        /// 
        /// Update Step Number By UserId parameter 3 
        /// 
        /// this function will call after 6th process
        /// </summary>
        /// <returns>update true/false</returns>
        /// 
        public bool updateStepNumberByUserId(int userId, int stepNumber, int loanNumber, int branchId)
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
                    command.Parameters.AddWithValue("@branch_id", branchId);

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
        /// 
        /// Edit Asanka Senarathna
        /// UpdatedDate: 2016/03/06
        /// removed existing connection open method and set parameter's to object list and pass stored procedure name to
        /// call DataHandler class to save user object
        /// </summary>
        /// <returns>update true/false</returns>
        /// 
        public DataSet checkUserLoginWhileCompanySetup(User user)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@company_id", user.Company_Id });
            paramertList.Add(new object[] { "@branch_id", user.BranchId });
            try
            {
                return dataHandler.GetDataSet("spCheckUserLoginWhileCompanySetup1", paramertList);
            }
            catch
            {
                return null;
            }
        }

        ///<summary>
        ///Created By : Asanka Senarathna
        ///CreatedDate : 2016/03/09
        ///
        /// Check the Super Admin Login While Company setup and branch count
        ///</summary>
        public DataSet checkSuperAdminLoginWhileCompanySetup(User user)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@company_id", user.Company_Id });
            try
            {
                return dataHandler.GetDataSet("spCheckUserLoginWhileCompanySetup2", paramertList);
            }
            catch
            {
                return null;
            }
        }

        ///<summary>
        ///CreatedBy: Asanka Senarathna
        ///CreatedDate : 2016/03/08
        ///
        /// While the User login check loan step 
        ///</summary>
        public DataSet checkUserLoginWhileLoanSetup(User user)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            paramertList.Add(new object[] { "@company_id", user.Company_Id });
            paramertList.Add(new object[] { "@branch_id", user.BranchId });
            try
            {
                return dataHandler.GetDataSet("spCheckUserLoginWhileLoanSetup", paramertList);
            }
            catch
            {
                return null;
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

                        command.Parameters.Add("@advance_fee_amount", SqlDbType.Decimal).Value = fees.AdvanceAmount;
                        command.Parameters.Add("@advance_fee_calculate_type", SqlDbType.NVarChar).Value = fees.AdvanceFeeCalculateType;
                        command.Parameters.Add("@advance_receipt", SqlDbType.Bit).Value = fees.AdvanceNeedReceipt;
                        command.Parameters.Add("@advance_payment_due_method", SqlDbType.NVarChar).Value = fees.AdvanceDue;
                        command.Parameters.Add("@advance_payment_due_date", SqlDbType.NVarChar).Value = fees.AdvanceDueDate;
                        command.Parameters.Add("@advance_auto_remind_dealer_email", SqlDbType.NVarChar).Value = fees.AdvanceFeeDealerEmail;
                        command.Parameters.Add("@advance_delaer_remind_period", SqlDbType.NVarChar).Value = fees.AdvanceFeeDealerEmailRemindPeriod;
                        command.Parameters.Add("@advance_due_auto_remind_email", SqlDbType.NVarChar).Value = fees.AdvanceDueEmail;
                        command.Parameters.Add("@advance_due_auto_remind_period", SqlDbType.NVarChar).Value = fees.AdvanceDueEmailRemindPeriod;

                        command.Parameters.Add("@monthly_loan_fee_amount", SqlDbType.Decimal).Value = fees.MonthlyLoanAmount;
                        command.Parameters.Add("@monthly_loan_receipt", SqlDbType.Bit).Value = fees.MonthlyLoanNeedReceipt;
                        command.Parameters.Add("@monthly_loan_payment_due_method", SqlDbType.NVarChar).Value = fees.MonthlyLoanDue;
                        command.Parameters.Add("@monthly_loan_payment_due_date", SqlDbType.NVarChar).Value = fees.MonthlyLoanDueDate;
                        command.Parameters.Add("@monthly_loan_auto_remind_dealer_email", SqlDbType.NVarChar).Value = fees.MonthlyLoanFeeDealerEmail;
                        command.Parameters.Add("@monthly_loan_delaer_remind_period", SqlDbType.NVarChar).Value = fees.MonthlyLoanFeeDealerEmailRemindPeriod;
                        command.Parameters.Add("@monthly_loan_due_auto_remind_email", SqlDbType.NVarChar).Value = fees.MonthlyLoanDueEmail;
                        command.Parameters.Add("@monthly_loan_due_auto_remind_period", SqlDbType.NVarChar).Value = fees.MonthlyLoanDueEmailRemindPeriod;

                        command.Parameters.Add("@lot_inspection_amount", SqlDbType.Decimal).Value = fees.LotInspectionAmount;
                        command.Parameters.Add("@lot_inspection_receipt", SqlDbType.Bit).Value = fees.LotInspectionNeedReceipt;
                        command.Parameters.Add("@lot_payment_due_method", SqlDbType.NVarChar).Value = fees.LotInspectionDue;
                        command.Parameters.Add("@lot_payment_due_date", SqlDbType.NVarChar).Value = fees.LotInspectionDueDate;
                        command.Parameters.Add("@lot_inspection_auto_remind_dealer_email", SqlDbType.NVarChar).Value = fees.LotInspectionFeeDealerEmail;
                        command.Parameters.Add("@lot_inspection_delaer_remind_period", SqlDbType.NVarChar).Value = fees.LotInspectionFeeDealerEmailRemindPeriod;
                        command.Parameters.Add("@lot_inspection_due_auto_remind_email", SqlDbType.NVarChar).Value = fees.LotInspectionDueEmail;
                        command.Parameters.Add("@lot_inspection_due_auto_remind_period", SqlDbType.NVarChar).Value = fees.LotInspectionDueEmailRemindPeriod;

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
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/3/4
        /// Update company setup step table
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="branchId"></param>
        /// <param name="stepNumber"></param>
        /// <returns></returns>
        public bool UpdateCompanySetupStep(int companyId, int branchId, int stepNumber)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_id", companyId });
            paramertList.Add(new object[] { "@branch_id", branchId });
            paramertList.Add(new object[] { "@step_number", stepNumber });


            try
            {
                return dataHandler.ExecuteSQL("spUpdateCompanySetupStep", paramertList) ? true : false;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/3/4
        /// Update loan setup step table
        /// </summary>
        /// <param name="nonRegisteredBranchId"></param>
        /// <param name="loanId"></param>
        /// <param name="stepNumber"></param>
        /// <param name="branchId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public bool UpdateLoanSetupStep(int companyId, int branchId,int nonRegisteredBranchId, int loanId, int stepNumber)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_id", companyId });
            paramertList.Add(new object[] { "@branch_id", branchId });
            paramertList.Add(new object[] { "@non_registered_branch_id", nonRegisteredBranchId });
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@step_number", stepNumber });


            try
            {
                return dataHandler.ExecuteSQL("spUpdateLoanSetupStep", paramertList) ? true : false;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2016/03/15
        /// 
        /// Get loan curtailment schedule by loan id
        /// 
        /// </summary>
        /// <param name="loanId">loan id</param>
        /// <returns>curtialment breakdown as a DataSet</returns>
        public LoanSetupStep1 GetLoanCurtailmentBreakdown(int loanId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetLoanCurtailmentBreakdown", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                LoanSetupStep1 loan = new LoanSetupStep1();
                loan.curtailmetList = new List<Curtailment>();
                loan.payOffPeriodType = dataSet.Tables[0].Rows[0]["pay_off_type"].ToString().Equals("d") ? 0 : 1;
                loan.payOffPeriod = Convert.ToInt32(dataSet.Tables[0].Rows[0]["pay_off_period"].ToString());
                loan.CurtailmentCalculationBase = dataSet.Tables[0].Rows[0]["curtailment_calculation_type"].ToString();
                loan.advancePercentage = Convert.ToInt32(dataSet.Tables[0].Rows[0]["advance"].ToString());
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    Curtailment curtailment = new Curtailment();
                    curtailment.CurtailmentId = Convert.ToInt32(dataRow["curtailment_id"].ToString());
                    curtailment.TimePeriod = Convert.ToInt32(dataRow["time_period"].ToString());
                    curtailment.Percentage = Convert.ToInt32(dataRow["percentage"].ToString());
                    loan.curtailmetList.Add(curtailment);
                }

                return loan;
            }
            else
            {
                return null;
            }
        }
    }
}
