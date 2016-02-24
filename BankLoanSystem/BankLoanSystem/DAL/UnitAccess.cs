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
    public class UnitAccess
    {
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/23
        /// Get all not advanced unit details by loan id
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns>unitList</returns>
        public List<Unit> GetNotAdvancedUnitDetailsByLoanId(int loanId)
        {

            List<Unit> unitList = new List<Unit>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {

                    var command = new SqlCommand("spGetNotAdvancedUnitDetailsByLoanId", con) { CommandType = CommandType.StoredProcedure };
                    command.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Unit unitDetails = new Unit()
                            {
                                UnitId = reader["unit_id"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["created_date"].ToString()),
                                IdentificationNumber = reader["identification_number"].ToString(),
                                Year = Convert.ToInt32(reader["year"].ToString()),
                                Make = reader["make"].ToString(),
                                Model = reader["model"].ToString(),
                                Cost = Convert.ToDecimal(reader["cost"].ToString()),
                                AdvanceAmount = Convert.ToDecimal(reader["advance_amount"].ToString())
                            };
                            unitList.Add(unitDetails);
                        }
                    }
                    return unitList;
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
        /// CreatedDate:2016/2/23
        /// Advance all selected items
        /// </summary>
        /// <param name="advanceDate"></param>
        /// <param name="loanId"></param>
        /// <param name="unitList"></param>
        /// <returns>true/false</returns>
        public int AdvanceAllSelectedItems(List<Unit> unitList,int loanId,int userId,DateTime advanceDate)
        {
            int countVal = 0;
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
            
                    
                    try
                    {
                        foreach (Unit unitObj in unitList)
                        {
                            using (SqlCommand cmd = new SqlCommand("spAdvanceAllSelectedItems", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                                cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                                cmd.Parameters.Add("@advance_date", SqlDbType.DateTime).Value = advanceDate;
                                cmd.Parameters.Add("@unit_id", SqlDbType.VarChar).Value = unitObj.UnitId;
                                cmd.Parameters.Add("@advance_amount", SqlDbType.Decimal).Value = unitObj.AdvanceAmount;

                            con.Open();

                                SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);


                                returnParameter.Direction = ParameterDirection.ReturnValue;
                                cmd.ExecuteNonQuery();

                                countVal = (int)returnParameter.Value;


                            //return countVal;
                            }
                        countVal = countVal + 1;
                        }
                    return countVal;
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
        /// CreatedBy:Irfan
        /// CreatedDate:2016/2/24
        /// Get loan payment details by loan id
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns>LoanPaymentDetails</returns>
        public LoanPaymentDetails GetLoanPaymentDetailsByLoanId(int loanId)
        {

            LoanPaymentDetails loanPaymentDetails = new LoanPaymentDetails();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {

                    var command = new SqlCommand("spGetLoanPaymentDetailsByLoanId", con) { CommandType = CommandType.StoredProcedure };
                    command.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;
                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            loanPaymentDetails.Amount = (Decimal)reader["loan_amount"];
                            loanPaymentDetails.BalanceAmount = (reader["balance_amount"]) != DBNull.Value ? (Decimal) reader["balance_amount"] :  (Decimal)0.00; 
                            loanPaymentDetails.PendingAmount = (reader["pending_amount"]) != DBNull.Value ? (Decimal) reader["pending_amount"] : (Decimal)0.00;
                            loanPaymentDetails.UsedAmount = (reader["used_amount"]) != DBNull.Value ? (Decimal)reader["used_amount"] : (Decimal)0.00; 
                            loanPaymentDetails.BalanceAfterPending = (reader["balance_amount_after_pending"]) != DBNull.Value ? (Decimal)reader["balance_amount_after_pending"] : (Decimal)0.00;

                        }
                    }
                    return loanPaymentDetails;
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