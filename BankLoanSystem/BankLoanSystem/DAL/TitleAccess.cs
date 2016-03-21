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
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate: 03/16/2016
        /// get titles list by identification number
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns>resultList</returns>
        public List<Unit> SearchTitle(string loanCode,string identificationNumber)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
           
            List<Unit> resultList = new List<Unit>();
            if (!string.IsNullOrEmpty(identificationNumber))
            {
                paramertList.Add(new object[] { "@loan_code", loanCode });
                paramertList.Add(new object[] { "@identification_number", identificationNumber });
                try
                {
                    DataSet dataSet = dataHandler.GetDataSet("spGetTitlesByIdentificationNumber", paramertList);
                    if (dataSet != null && dataSet.Tables.Count != 0)
                    {
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            Unit title = new Unit();
                            title.AdvanceDate = DateTime.Parse(dataRow["advance_date"].ToString());
                            title.IdentificationNumber = dataRow["identification_number"].ToString();
                            title.Year = int.Parse(dataRow["year"].ToString());
                            title.Make = dataRow["make"].ToString();
                            title.Model = dataRow["model"].ToString();

                            if (int.Parse(dataRow["title_status"].ToString()) == 0)
                            {
                                title.CurrentTitleStatus = "Not Received";
                            }
                            else if (int.Parse(dataRow["title_status"].ToString()) == 1)
                            {
                                title.CurrentTitleStatus = "Received";
                            }
                            else if (int.Parse(dataRow["title_status"].ToString()) == 2)
                            {
                                title.CurrentTitleStatus = "Returned to Dealer";
                            }
                            else if (int.Parse(dataRow["title_status"].ToString()) == 3)
                            {
                                title.CurrentTitleStatus = "Sent to Bank";
                            }
                            title.UnitStatus = int.Parse(dataRow["unit_status"].ToString());
                            if (int.Parse(dataRow["unit_status"].ToString()) == 0)
                            {
                                title.CurrentUnitStatus = "InActive";
                            }
                            else if (int.Parse(dataRow["unit_status"].ToString()) == 1)
                            {
                                title.CurrentUnitStatus = "Active";
                            }
                            else if (int.Parse(dataRow["unit_status"].ToString()) == 2)
                            {
                                title.CurrentUnitStatus = "Paid";
                            }
                           
                            resultList.Add(title);
                        }

                        return resultList;
                    }
                    else
                    {
                        return null;
                    }

                }
                catch
                {
                    return null;
                }
                
            }
            return null;
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate: 03/17/2016
        /// get titles list by identification number
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="loanCode"></param>
        /// <returns>true/false</returns>
        public bool UpdateTitle(Unit unit,string loanCode,int userId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@identification_number", unit.IdentificationNumber });
            paramertList.Add(new object[] { "@year", unit.Year });
            paramertList.Add(new object[] { "@make", unit.Make });
            paramertList.Add(new object[] { "@model", unit.Model });
            paramertList.Add(new object[] { "@title_status", unit.TitleStatus });
            paramertList.Add(new object[] { "@loan_code", loanCode });
            paramertList.Add(new object[] { "@user_id", userId });
            paramertList.Add(new object[] { "@modified_date", DateTime.Now });

            try
            {
                return dataHandler.ExecuteSQL("spUpdateTitleStatus", paramertList) ? true : false;

            }
            catch
            {
                return false;
            }
        }
    }
}