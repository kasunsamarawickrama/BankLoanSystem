using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankLoanSystem.Models;
using System.Linq;

namespace BankLoanSystem.DAL
{
    public class LoanManagementAccess
    {
        /// <summary>
        /// Created by : kasun
        /// CreatedDate:05/04/2016
        /// 
        /// Loan Renew date insert(update maturity date)
        /// </summary>
        /// <param name="l"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool LoanRenew(Loan l, int userId) {

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    var command = new SqlCommand("spUpdateLoanRenewalDateByLoanId", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@loan_id", l.LoanId);
                    command.Parameters.AddWithValue("@loan_code", l.LoanCode);
                    command.Parameters.AddWithValue("@maturity_date", l.MaturityDate);
                    command.Parameters.AddWithValue("@renewal_date", l.RenewalDate);
                    command.Parameters.AddWithValue("@changed_date", DateTime.Now);

                    SqlParameter returnParameter = command.Parameters.Add("@ReturnValue", SqlDbType.Bit);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    con.Open();
                    command.ExecuteNonQuery();

                    if ((int)returnParameter.Value > 0)
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
        /// CreatedBy:  Kasun
        /// CreatedDate:05/04/2016
        /// 
        /// get loan details by loan code
        /// </summary>
        /// <param name="loanCode"></param>
        /// <returns></returns>
        public Loan GetLoanByLoanCode(int loanId,string loanCode)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_code", loanCode });
            paramertList.Add(new object[] { "@loan_id", loanId });

            Loan loan = new Loan();
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetLoanByLoanCode", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    DataRow dataRow = dataSet.Tables[0].Rows[0];

                    loan.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                    loan.LoanAmount = Convert.ToDecimal(dataRow["loan_amount"]);
                    loan.LoanCode = dataRow["loan_code"].ToString();
                    loan.MaturityDate = Convert.ToDateTime(dataRow["maturity_date"]);
                    loan.StartDate = Convert.ToDateTime(dataRow["start_date"]);
                    loan.LoanStatus = Convert.ToBoolean(dataRow["loan_status"]);                    
                    loan.LoanNumber = dataRow["loan_number"].ToString();

                    return loan;
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