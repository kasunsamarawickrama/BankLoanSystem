using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BankLoanSystem.DAL
{
    public class LoanSetupAccess
    {

        public bool IsUniqueLoanNumberForBranch(string loanNumber, int RegisteredBranchId)
        {

            using (
                var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                var command = new SqlCommand("IsUniqueLoanNumberForBranch", con) { CommandType = CommandType.StoredProcedure };
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
    }
}