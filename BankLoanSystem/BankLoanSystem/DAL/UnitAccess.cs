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
        public List<Unit> GetNotAdvancedUnitDetailsByLoanId(int loanId) {

            List<Unit> unitList = new List<Unit>();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                var command = new SqlCommand("spGetNotAdvancedUnitDetailsByLoanId", con) { CommandType = CommandType.StoredProcedure };
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Unit unitDetails = new Unit()
                        {
                            CreatedDate = Convert.ToDateTime(reader["created_date"].ToString()),
                            IdentificationNumber = reader["identification_number"].ToString(),
                            Year = Convert.ToInt32(reader["year"].ToString()),
                            Make = reader["make"].ToString(),
                            Model = reader["model"].ToString(),
                            Cost = Convert.ToDouble(reader["cost"].ToString()),
                            AdvanceAmount = Convert.ToDouble(reader["advance_amount"].ToString())
                        };
                        unitList.Add(unitDetails);
                    }
                }
            }

            return unitList;

        }
    }
}