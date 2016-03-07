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
            DataHandler dataHandler = new DataHandler();            

            DataSet dataSet = dataHandler.GetDataSetBySQL("spGetAllAccrualMethods");
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    AccrualMethods methods = new AccrualMethods()
                    {
                        MethodId = Convert.ToInt32(dataRow["accrual_method_id"]),
                        MethodName = dataRow["accrual_method_name"].ToString()
                    };
                    methodList.Add(methods);
                }

                return methodList;
            }
            else
            {
                return null;
            }
           

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
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();            

            paramertList.Add(new object[] { "@interest_rate", interest.InterestRate });
            paramertList.Add(new object[] { "@paid_date", interest.PaidDate });
            paramertList.Add(new object[] { "@payment_period", interest.PaymentPeriod });
            paramertList.Add(new object[] { "@auto_remind_email", interest.AutoRemindEmail });
            paramertList.Add(new object[] { "@auto_remind_period", interest.RemindPeriod });
            paramertList.Add(new object[] { "@loan_id", interest.LoanId });
            paramertList.Add(new object[] { "@accrual_method_id", interest.AccrualMethodId });           

            try
            {
                return dataHandler.ExecuteSQL("spInsertInterestDetails", paramertList) ? 1 : 0;
            }
            catch
            {
                return 0;
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
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetInterestDetailsByLoanId", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                Interest interest = new Interest();
                DataRow dataRow = dataSet.Tables[0].Rows[0];

                interest.InterestRate = Math.Round(decimal.Parse(dataRow["interest_rate"].ToString()), 3);
                if ((dataRow["paid_date"].ToString().Contains("payoff")))
                {
                    interest.option = "payoff";
                    interest.PaidDate = "payoff";
                }
                else
                {
                    interest.option = "once a month";
                    interest.PaidDate = dataRow["paid_date"].ToString();
                }
                interest.PaymentPeriod = dataRow["payment_period"].ToString();
                interest.AccrualMethodId = int.Parse(dataRow["accrual_method_id"].ToString());
                interest.AutoRemindEmail = dataRow["auto_remind_email"].ToString();
                if ((interest.AutoRemindEmail != null) && (interest.AutoRemindEmail != ""))
                {
                    interest.NeedReminder = true;
                }
                interest.RemindPeriod = int.Parse(dataRow["auto_remind_period"].ToString());
                interest.LoanId = int.Parse(dataRow["loan_id"].ToString());
                return interest;
            }
            else
            {
                return null;
            }           
        }
    }
}