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
    public class TitleAccess
    {
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/9
        /// Get details of title related to given loanId
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns>TitleObject</returns>
        public Title getTitleDetails(int loanId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spGetTitleDetailsByLoanId", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = loanId;

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();


                        //Title obj1 = null;
                        Title obj1 = new Title();


                        while (reader.Read())
                        {
                            obj1.LoanId = int.Parse(reader["loan_id"].ToString());
                            obj1.IsTitleTrack = bool.Parse(reader["is_title_tracked"].ToString());
                            if (obj1.IsTitleTrack)
                            {

                                obj1.TitleAcceptMethod = reader["title_accept_method"].ToString();
                                obj1.ReceivedTimeLimit = reader["title_received_time_period"].ToString();
                                obj1.RemindEmail = reader["auto_remind_email"].ToString();
                                
                            }
                            else
                            {
                                LoanSetupAccess st = new LoanSetupAccess();
                                obj1.RemindEmail = st.getAutoRemindEmailByLoanId(obj1.LoanId);
                            }
                            obj1.IsReceipRequired = bool.Parse(reader["is_receipt_required"].ToString());

                            if (obj1.IsReceipRequired)
                            {

                                obj1.ReceiptRequiredMethod = reader["receipt_required_method"].ToString();
                            }

                           


                        }
                       if(obj1 == null)
                        {
                            obj1.IsTitleTrack = false;
                            obj1.IsReceipRequired = false;
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

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/9
        /// Insert details of title related to a loanId
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns>countVal</returns>
        public int insertTitleDetails(Title title)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AutoDealersConnection"].ConnectionString))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("spInsertTitleDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@is_title_tracked", SqlDbType.Bit).Value = title.IsTitleTrack;
                        if (title.IsTitleTrack)
                        {
                            cmd.Parameters.Add("@title_accept_method", SqlDbType.VarChar).Value = title.TitleAcceptMethod;
                            cmd.Parameters.Add("@title_received_time_period", SqlDbType.VarChar).Value = title.ReceivedTimeLimit;

                            cmd.Parameters.Add("@auto_remind_email", SqlDbType.VarChar).Value = title.RemindEmail;
                        }
                        else
                        {
                            cmd.Parameters.Add("@title_accept_method", SqlDbType.VarChar).Value = null;
                            cmd.Parameters.Add("@title_received_time_period", SqlDbType.VarChar).Value = null;

                            cmd.Parameters.Add("@auto_remind_email", SqlDbType.VarChar).Value = null;
                        }

                        cmd.Parameters.Add("@is_receipt_required", SqlDbType.Bit).Value = title.IsReceipRequired;
                        if (title.IsReceipRequired)
                        {

                            cmd.Parameters.Add("receipt_required_method", SqlDbType.VarChar).Value = title.ReceiptRequiredMethod;
                        }
                        else
                        {
                            cmd.Parameters.Add("receipt_required_method", SqlDbType.VarChar).Value = null;
                        }
                        cmd.Parameters.Add("@loan_id", SqlDbType.Int).Value = title.LoanId;



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
    }
}