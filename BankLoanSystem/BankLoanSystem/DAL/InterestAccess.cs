using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankLoanSystem.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace BankLoanSystem.DAL
{
    public class InterestAccess
    {
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/5
        /// get all accrual methods to a list
        /// </summary>
        /// <returns>methodList</returns>
        public List<AccrualMethods> GetAllAccrualMethods()
        {
            List<AccrualMethods> methodList = new List<AccrualMethods>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                var command = new SqlCommand("spGetAllAccrualMethods", con) { CommandType = CommandType.StoredProcedure };
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AccrualMethods methods = new AccrualMethods()
                        {
                            MethodId = Convert.ToInt32(reader["accrual_method_id"]),
                            MethodName = reader["accrual_method_name"].ToString()
                        };
                        methodList.Add(methods);
                    }
                }
            }

            return methodList;
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/5
        /// insert details of interest which related to a loan
        /// </summary>
        /// <returns>countval</returns>
        public int insertInterestDetails(Interest interest)
        {
            string rate = null;
            if (!interest.InterestRate.ToString().Contains("."))
            {
                rate = interest.InterestRate.ToString() + ".000";

            }
            else if (interest.InterestRate.ToString().Contains("."))
            {
                string[] ratearr = interest.InterestRate.ToString().Split('.');
                if (ratearr[1].Length == 1)
                {
                    rate = interest.InterestRate.ToString() + "00";
                }
                else if (ratearr[1].Length == 2)
                {
                    rate = interest.InterestRate.ToString() + "0";
                }
            }
            interest.InterestRate = Double.Parse(rate);
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spInsertInterestDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@interest_rate", SqlDbType.Float).Value = interest.InterestRate;
                        cmd.Parameters.Add("@paid_date", SqlDbType.VarChar).Value = interest.PaidDate;
                        cmd.Parameters.Add("@payment_period", SqlDbType.VarChar).Value = interest.PaymentPeriod;

                        cmd.Parameters.Add("@auto_remind_email", SqlDbType.VarChar).Value = interest.AutoRemindEmail;

                        cmd.Parameters.Add("@auto_remind_period", SqlDbType.VarChar).Value = interest.RemindPeriod;


                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = interest.LoanId;
                        cmd.Parameters.Add("@accrual_method_id", SqlDbType.Int).Value = interest.AccrualMethodId;


                        con.Open();

                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);


                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        int countVal = (int)returnParameter.Value;

                        return countVal;

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
        /// CreatedDate:2016/2/7
        /// Get details of interest related to given loanId
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns>InterestObject</returns>
        public Interest getInterestDetails(int loanId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetInterestDetailsByLoanId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();


                        Interest obj1 = null;

                        while (reader.Read())
                        {
                            obj1 = new Interest();
                            obj1.InterestRate = Double.Parse(reader["interest_rate"].ToString());
                            if ((reader["paid_date"].ToString().Contains("payoff")))
                            {
                                obj1.option = "payoff";
                                obj1.PaidDate = "payoff";
                            }
                            else
                            {
                               
                                obj1.option = "once a month";
                                obj1.PaidDate = reader["paid_date"].ToString();
                            }

                            obj1.PaymentPeriod = reader["payment_period"].ToString();
                            obj1.AccrualMethodId = int.Parse(reader["accrual_method_id"].ToString());
                            obj1.AutoRemindEmail = reader["auto_remind_email"].ToString();
                            if((obj1.AutoRemindEmail!=null)&& (obj1.AutoRemindEmail != ""))
                            {
                                obj1.NeedReminder = true;
                            }
                            obj1.RemindPeriod = reader["auto_remind_period"].ToString();
                            obj1.LoanId = int.Parse(reader["loan_id"].ToString());




                        }
                        return obj1;

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