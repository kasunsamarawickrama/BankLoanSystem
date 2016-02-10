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