using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BankLoanSystem.DAL
{
    /// <summary>
    /// Loan curtailment related operation define inside the class
    /// </summary>
    public class CurtailmentAccess
    {
        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2016/02/09
        /// 
        /// retreive Curtailment Detail By CurtailmentId
        /// 
        /// argument : curtailment_id (int)
        /// 
        /// </summary>
        /// <returns>curtailment list</returns>

        public List<Curtailment> retreiveCurtailmentByLoanId(int id)
        {


            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spRetrieveCurtailmentByLoanId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = id;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        List<Curtailment> lstCurtailment = new List<Curtailment>();

                        while (reader.Read())
                        {
                            Curtailment curtailment = new Curtailment();
                            curtailment.CurtailmentId = int.Parse(reader["curtailment_id"].ToString());
                            curtailment.LoanId = int.Parse(reader["loan_id"].ToString());
                            curtailment.TimePeriod = Convert.ToInt32(reader["time_period"]);
                            curtailment.Percentage = Convert.ToInt32(reader["percentage"].ToString());
                            lstCurtailment.Add(curtailment);

                        }
                        return lstCurtailment;

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
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2016/02/09
        /// 
        /// update curtailment Details of timePeriod, percentage
        /// 
        /// argument : curtailment_id (int)
        /// 
        /// </summary>
        /// <returns>curtailment object</returns>

        public bool updateUserDetails(int curtailmentId, string timePeriod, float percentage)
        {

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spUpdateUserDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@curtailment_id", SqlDbType.Int).Value = curtailmentId;
                        cmd.Parameters.Add("@time_period", SqlDbType.NVarChar).Value = timePeriod;
                        cmd.Parameters.Add("@percentage", SqlDbType.Float).Value = percentage;

                        con.Open();

                        SqlParameter returnParameter = cmd.Parameters.Add("@return", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        int countVal = (int)returnParameter.Value;

                        if (countVal == 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;

                        }

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



            return true;
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2016/02/09
        /// 
        /// Insert curtailment details
        /// 
        /// argument : curtailment list
        /// 
        /// </summary>
        /// <returns>1</returns>

        public LoanSetupStep1 GetLoanDetailsByLoanId(int loanId)
        {
            LoanSetupStep1 loan = new LoanSetupStep1();

            using (
                SqlConnection con =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {

                try
                {
                    var command = new SqlCommand("spGetLoanDetailsByLoanId", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@loan_id", loanId);

                    con.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loan.startDate = Convert.ToDateTime(reader["start_date"]);
                            loan.maturityDate = Convert.ToDateTime(reader["maturity_date"]);
                            loan.loanAmount = Convert.ToDecimal(reader["loan_amount"]);
                            loan.advancePercentage = Convert.ToInt32(reader["advance"]);

                            //0 - day, 1 - month
                            if (reader["pay_off_type"].ToString() == "d")
                                loan.payOffPeriodType = 0;
                            loan.payOffPeriodType = 1;
                            loan.payOffPeriod = Convert.ToInt32(reader["pay_off_period"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return loan;
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2016/02/09
        /// 
        /// Insert curtailment details
        /// 
        /// argument : curtailment list
        /// 
        /// </summary>
        /// <returns>1</returns>

        public int InsertCurtailment(List<Curtailment> lstCurtailment)
        {
            int flag = 0;
            int delFlag = 0;
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                con.Open();

                var commandDelete = new SqlCommand("spDeleteCurtailment", con) { CommandType = CommandType.StoredProcedure };
                commandDelete.Parameters.Add("@loan_id", SqlDbType.Int).Value = lstCurtailment[0].LoanId;

                SqlParameter returnDelParameter = commandDelete.Parameters.Add("@return", SqlDbType.Int);
                returnDelParameter.Direction = ParameterDirection.ReturnValue;
                commandDelete.ExecuteNonQuery();

                delFlag = (int)returnDelParameter.Value;

                var command = new SqlCommand("spInsertCurtailment", con) { CommandType = CommandType.StoredProcedure };

                foreach (Curtailment curtailment in lstCurtailment)
                {
                    command.Parameters.Add("@loan_id", SqlDbType.Int).Value = curtailment.LoanId;
                    command.Parameters.Add("@curtailment_id", SqlDbType.Int).Value = curtailment.CurtailmentId;
                    command.Parameters.Add("@time_period", SqlDbType.NVarChar).Value = curtailment.TimePeriod;
                    command.Parameters.Add("@percentage", SqlDbType.Float).Value = curtailment.Percentage;


                    SqlParameter returnParameter = command.Parameters.Add("@return", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;
                    command.ExecuteNonQuery();

                    flag = (int)returnParameter.Value;

                    command.Parameters.Clear();
                }
            }
            //if (delFlag == 2) flag = delFlag;
            return flag; ;
        }
    }
}