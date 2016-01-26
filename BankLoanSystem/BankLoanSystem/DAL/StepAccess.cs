using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class StepAccess
    {

        /// <summary>
        /// CreatedBy : kasun Samarawickrama
        /// CreatedDate: 2016/01/25
        /// 
        /// Get Step Nomber By UserId
        /// </summary>
        /// <returns>step nomber</returns>
        /// 
        public int GetStepNumberByUserId(int userId)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    var command = new SqlCommand("spGetStepNomberByUserId", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userId);

                    SqlParameter returnParameter = command.Parameters.Add("@ReturnValue", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    con.Open();
                    command.ExecuteNonQuery();

                    return (int)returnParameter.Value;
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }

        /// <summary>
        /// CreatedBy : kasun Samarawickrama
        /// CreatedDate: 2016/01/26
        /// 
        /// Update Step Nomber By UserId
        /// </summary>
        /// <returns>update true/false</returns>
        /// 
        public bool UpdateStepNomberByUserId(int userId, int stepNomber)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ToString()))
            {
                try
                {
                    var command = new SqlCommand("spUpdateStepNomberByUserId", con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@step_id", stepNomber);

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