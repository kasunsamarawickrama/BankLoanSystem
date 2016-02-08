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
                    if ((int)returnParameter.Value ==1) {
                        return true ;
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

        public void InsertFeesDetails(Fees fees)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("spInsertFeesDetails", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@advance_fee_amount", SqlDbType.NVarChar).Value = fees.AdvanceAmount;
                        //command.Parameters.Add("@advance_payment_due_method", SqlDbType.NVarChar).Value = fees;
                        command.Parameters.Add("@advance_payment_due_date", SqlDbType.DateTime).Value = fees.AdvanceDue;

                        command.Parameters.Add("@monthly_loan_fee_amount", SqlDbType.NVarChar).Value = fees.MonthlyLoanAmount;
                        //command.Parameters.Add("@monthly_loan_payment_due_method", SqlDbType.NVarChar).Value = fees;
                        command.Parameters.Add("@monthly_loan_payment_due_date", SqlDbType.DateTime).Value = fees.MonthlyLoanDue;

                        command.Parameters.Add("@lot_inspection_amount", SqlDbType.NVarChar).Value = fees.LotInspectionAmount;
                        //command.Parameters.Add("@lot_payment_due_method", SqlDbType.NVarChar).Value = fees;
                        command.Parameters.Add("@lot_payment_due_date", SqlDbType.DateTime).Value = fees.LotInspectionDue;

                        command.Parameters.Add("@auto_remind_dealer_email", SqlDbType.NVarChar).Value = "kasun2030@gmail.com";
                        command.Parameters.Add("@delaer_remind_period", SqlDbType.NVarChar).Value = "kasun2030@gmail.com";
                        command.Parameters.Add("@auto_remind_lender_email", SqlDbType.NVarChar).Value = "kasun2030@gmail.com";
                        command.Parameters.Add("@lender_remind_period", SqlDbType.NVarChar).Value = "kasun2030@gmail.com";
                        command.Parameters.Add("@loan_id", SqlDbType.Int).Value = fees.LoanId;
                        con.Open();
                        command.ExecuteNonQuery();
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