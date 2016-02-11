using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class LoanSetupAccess
    {
        /// <summary>
        /// CreatedBy:Irfan MAM
        /// CreatedDate:2016/2/9
        /// check the loan number is unique for a branch
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsUniqueLoanNumberForBranch(string loanNumber, int RegisteredBranchId)
        {

            using (
                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                var command = new SqlCommand("spIsUniqueLoanNumberForBranch", con) { CommandType = CommandType.StoredProcedure };
                command.Parameters.Add("@loan_number", SqlDbType.NVarChar).Value = loanNumber;
                command.Parameters.Add("@branch_id", SqlDbType.Int).Value = RegisteredBranchId;
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
        /// CreatedBy:Irfan MAM
        /// CreatedDate:2016/2/9
        /// getting all unit types
        /// </summary>
        /// <returns>IList<UnitType></returns>

        internal IList<UnitType> getAllUnitTypes()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetAllUnitTypes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        List<UnitType> unitTypes = new List<UnitType>();


                        while (reader.Read())
                        {
                            UnitType unitType = new UnitType();
                           

                            unitType.unitTypeName = reader["unit_type_name"].ToString();

                            unitType.unitTypeId = int.Parse(reader["unit_type_id"].ToString());
                            unitType.isSelected = false;
                            unitTypes.Add(unitType);

                        }
                        return unitTypes;

                    }
                }


                catch (Exception ex)
                {
                    throw ex;

                }
                finally
                {
                    con.Close();
                }
            }
        }
        
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/8
        /// get loanId by userId from step table
        /// </summary>
        /// <returns>loanId</returns>
        public int getLoanIdByUserId(int uId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetLoanIdByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@user_id", SqlDbType.Int).Value =uId;

                        con.Open();

                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);


                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        int loanId = (int)returnParameter.Value;

                        return loanId;
                    }
                }


                catch (Exception ex)
                {
                    throw ex;

                }
                finally
                {
                    con.Close();
                }
            }
        }
        
        /// <summary>
        /// Created By: kasun Smarawickrama
        /// Date : 10/2/2012
        /// 
        /// check the loan inserted values to advance,lot,monthly loan fee tables.
        /// </summary>
        /// <param name="loanId">loan id</param>
        /// <returns>fees Details</returns>
        /// 
        public Fees checkLoanIsInFeesTables(int loanId)
        {
            Fees fees = new Fees();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spCheckLoanIsInAdvanceFeeTable", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read()) {                          
                            fees.AdvanceAmount = double.Parse(reader["advance_fee_amount"].ToString());
                            fees.AdvanceNeedReceipt = bool.Parse(reader["receipt"].ToString());
                            fees.AdvanceDue = reader["payment_due_method"].ToString();
                            fees.AdvanceDueDate = reader["payment_due_date"].ToString();
                            fees.AdvanceDealerEmail = reader["auto_remind_dealer_email"].ToString();
                            fees.AdvanceDealerEmailRemindPeriod = int.Parse(reader["delaer_remind_period"].ToString());
                            fees.AdvanceLenderEmail = reader["auto_remind_lender_email"].ToString();
                            fees.AdvanceLenderEmailRemindPeriod = int.Parse(reader["lender_remind_period"].ToString());
                            
                        }
                        reader.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("spCheckLoanIsInMonthlyLoanFeeTable", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            fees.MonthlyLoanAmount = double.Parse(reader["monthly_loan_fee_amount"].ToString());
                            fees.MonthlyLoanNeedReceipt = bool.Parse(reader["receipt"].ToString());
                            fees.MonthlyLoanDue = reader["payment_due_method"].ToString();
                            fees.MonthlyLoanDueDate = reader["payment_due_date"].ToString();
                            fees.MonthlyLoanDealerEmail = reader["auto_remind_dealer_email"].ToString();
                            fees.MonthlyLoanDealerEmailRemindPeriod = int.Parse(reader["delaer_remind_period"].ToString());
                            fees.MonthlyLoanLenderEmail = reader["auto_remind_lender_email"].ToString();
                            fees.MonthlyLoanLenderEmailRemindPeriod = int.Parse(reader["lender_remind_period"].ToString());

                        }
                        reader.Close();
                    }
                    using (SqlCommand cmd = new SqlCommand("spCheckLoanIsInLotInspectionFeeTable", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            fees.LotInspectionAmount = double.Parse(reader["lot_inspection_amount"].ToString());
                            fees.LotInspectionNeedReceipt = bool.Parse(reader["receipt"].ToString());
                            fees.LotInspectionDue = reader["payment_due_method"].ToString();
                            fees.LotInspectionDueDate = reader["payment_due_date"].ToString();
                            fees.LotInspectionDealerEmail = reader["auto_remind_dealer_email"].ToString();
                            if (reader["delaer_remind_period"] != System.DBNull.Value)
                            {
                                fees.LotInspectionDealerEmailRemindPeriod = int.Parse(reader["delaer_remind_period"].ToString());
                            }
                            fees.LotInspectionLenderEmail = reader["auto_remind_lender_email"].ToString();
                            if (reader["lender_remind_period"] != System.DBNull.Value)
                            {
                                fees.LotInspectionLenderEmailRemindPeriod = int.Parse(reader["lender_remind_period"].ToString());
                            }
                        }
                        reader.Close();
                    }

                    return fees;
                }


                catch (Exception ex)
                {
                    throw ex;

                }
                finally
                {
                    con.Close();
                }
            }
        }
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/8
        /// get auto remind email from loan table
        /// </summary>
        /// <returns>autoRemindEmail</returns>
        public string getAutoRemindEmailByLoanId(int loanId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetAutoRemindEmailByLoanId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();


                        string autoRemindEmail = null;

                        while (reader.Read())
                        {


                            autoRemindEmail = reader["auto_remind_email"].ToString();
                                

                        }
                        return autoRemindEmail;

                    }
                }


                catch (Exception ex)
                {
                    throw ex;

                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}