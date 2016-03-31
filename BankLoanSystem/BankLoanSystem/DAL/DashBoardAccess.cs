using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using BankLoanSystem.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace BankLoanSystem.DAL
{
    public class DashBoardAccess
    {
        /// <summary>
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// Retrive User Level by userid
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns></returns>
        public int GetUserLevelByUserId(int userId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetUserLevelByUserId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;

                        con.Open();
                        cmd.ExecuteNonQuery();
                        SqlParameter returnParameter = cmd.Parameters.Add("@ReturnValue", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;
                        cmd.ExecuteNonQuery();

                        int userLevelId = (int)returnParameter.Value;

                        return userLevelId;
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
        /// CreatedBy: Piyumi
        /// CreatedDate: 3/30/2016
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetLoanCount(int para, int roleId)
        {
            //int loanCount = 0;
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@para", para });
            paramertList.Add(new object[] { "@role", roleId });
            try
            {
                return dataHandler.ExecuteSQLReturn("spGetLoanCount", paramertList);
            }
            catch
            {
                return 0;
            }


        }
        public Loan GetLoanDetails(int id,int role)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@para", id });
            paramertList.Add(new object[] { "@role", role });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetLoan", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    Loan loanObj = new Loan();
                    loanObj.LoanNumber = dataSet.Tables[0].Rows[0]["loan_number"].ToString();
                    loanObj.PartnerName = dataSet.Tables[0].Rows[0]["company_name"].ToString();
                    loanObj.PartnerType = int.Parse(dataSet.Tables[0].Rows[0]["company_type"].ToString());
                    loanObj.BranchName = dataSet.Tables[0].Rows[0]["branch_name"].ToString();
                    if (bool.Parse(dataSet.Tables[0].Rows[0]["is_title_tracked"].ToString()))
                    {
                        loanObj.IsTitleTrack = 1;
                    }
                    else
                    {
                        loanObj.IsTitleTrack = 0;
                    }
                    if (role == 3)
                    {
                        if (!string.IsNullOrEmpty(dataSet.Tables[0].Rows[0]["right_id"].ToString()))
                        {
                            loanObj.Rights = dataSet.Tables[0].Rows[0]["right_id"].ToString().Split(',');
                        }
                       
                    }
                    return loanObj;

                }
                return null;
            }

            catch
            {
                return null;
            }
        }
    }
}