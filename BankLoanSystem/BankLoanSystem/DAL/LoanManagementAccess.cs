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

        public bool LoanRenew(Loan l, int userId) {

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    var command = new SqlCommand("spUpdateLoanRenewalDateByUserId", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@loan_id", l.LoanId);
                    command.Parameters.AddWithValue("@maturity_date", l.MaturityDate);
                    command.Parameters.AddWithValue("@renewal_date", l.RenewalDate);
                    command.Parameters.AddWithValue("@changed_date", DateTime.Now);

                    SqlParameter returnParameter = command.Parameters.Add("@ReturnValue", SqlDbType.Bit);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    con.Open();
                    command.ExecuteNonQuery();

                    return (bool)returnParameter.Value;
                    
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }
    }
}