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
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetTitleDetailsByLoanId", paramertList);
                Title obj1 = new Title();
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                   
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        
                        obj1.LoanId = int.Parse(dataRow["loan_id"].ToString());
                        obj1.IsTitleTrack = bool.Parse(dataRow["is_title_tracked"].ToString());
                        if (obj1.IsTitleTrack)
                        {

                            //obj1.TitleAcceptMethod = dataRow["title_accept_method"].ToString();
                            obj1.ReceivedTimeLimit = dataRow["title_received_time_period"].ToString();
                            
                            if (!string.IsNullOrEmpty(dataRow["auto_remind_period"].ToString()))
                            {
                                obj1.RemindPeriod = int.Parse(dataRow["auto_remind_period"].ToString());
                            }
                            //else
                            //{
                            //    obj1.RemindPeriod = 0;
                            //}
                            if (!string.IsNullOrEmpty(dataRow["auto_remind_email"].ToString()))
                            {
                                obj1.RemindEmail = dataRow["auto_remind_email"].ToString();
                            }
                            else
                            {
                                LoanSetupAccess st = new LoanSetupAccess();
                                obj1.RemindEmail = st.getAutoRemindEmailByLoanId(obj1.LoanId);
                            }

                        }
                        else 
                        {
                            LoanSetupAccess st = new LoanSetupAccess();
                            obj1.RemindEmail = st.getAutoRemindEmailByLoanId(obj1.LoanId);
                        }
                        obj1.IsReceipRequired = bool.Parse(dataRow["is_receipt_required"].ToString());

                        if (obj1.IsReceipRequired)
                        {

                            obj1.ReceiptRequiredMethod = dataRow["receipt_required_method"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dataRow["need_scan_copy"].ToString()))
                        {
                            obj1.NeedScanCopy = bool.Parse(dataRow["need_scan_copy"].ToString());
                            if (obj1.NeedScanCopy)
                            {
                                obj1.TitleAcceptMethod = "Scanned Title Adequate";
                            }
                        }
                    }
                    return obj1;
                }
                else
                {
                  return obj1;
                }
             }
            catch (Exception ex)
            {
                throw ex;

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
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@is_title_tracked", title.IsTitleTrack });
            if (title.IsTitleTrack)
            {
                //paramertList.Add(new object[] { "@title_accept_method", title.TitleAcceptMethod });
                paramertList.Add(new object[] { "@title_received_time_period", title.ReceivedTimeLimit });
                paramertList.Add(new object[] { "@auto_remind_period", title.RemindPeriod });
                paramertList.Add(new object[] { "@auto_remind_email", title.RemindEmail });
                if (title.NeedScanCopy)
                {
                    paramertList.Add(new object[] { "@need_scan_copy", 1 });
                }
                else
                {
                    paramertList.Add(new object[] { "@need_scan_copy", 0 });
                }
               
            }
            else
            {
               // paramertList.Add(new object[] { "@title_accept_method", null });
                paramertList.Add(new object[] { "@title_received_time_period", null });
                paramertList.Add(new object[] { "@auto_remind_period", null });
                paramertList.Add(new object[] { "@auto_remind_email", null});
                paramertList.Add(new object[] { "@need_scan_copy", 0 });
            }

            paramertList.Add(new object[] { "@is_receipt_required", title.IsReceipRequired });
            if (title.IsReceipRequired)
            {

                paramertList.Add(new object[] { "receipt_required_method", title.ReceiptRequiredMethod });
            }
            else
            {
                paramertList.Add(new object[] { "receipt_required_method",null});
            }
            paramertList.Add(new object[] { "@loan_id", title.LoanId });

            try
            {
                return dataHandler.ExecuteSQLReturn("spInsertTitleDetails", paramertList);
            }
            catch(Exception ex)
            {
                throw ex;
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
                            if (!string.IsNullOrEmpty(dataRow["advance_date"].ToString()))
                            {
                                title.AdvanceDate = DateTime.Parse(dataRow["advance_date"].ToString());
                            }
                            //else
                            //{
                            //    title.AdvanceDate = "";
                            //}    
                            title.UnitId = dataRow["unit_id"].ToString();
                            title.IdentificationNumber = dataRow["identification_number"].ToString();
                            title.Year = int.Parse(dataRow["year"].ToString());
                            title.Make = dataRow["make"].ToString();
                            title.Model = dataRow["model"].ToString();

                            if (!string.IsNullOrEmpty(dataRow["title_status"].ToString()))
                            {
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
                            }
                            else
                            {
                                title.CurrentTitleStatus = "";
                            }
                            if (!string.IsNullOrEmpty(dataRow["unit_status"].ToString()))
                            {
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
                            }

                            else
                            {
                                title.CurrentUnitStatus = "";
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
                catch (Exception ex)
                {
                    throw ex;
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
            paramertList.Add(new object[] { "@unit_id", unit.UnitId });
            //paramertList.Add(new object[] { "@identification_number", unit.IdentificationNumber });
            //paramertList.Add(new object[] { "@year", unit.Year });
            //paramertList.Add(new object[] { "@make", unit.Make });
            //paramertList.Add(new object[] { "@model", unit.Model });
            paramertList.Add(new object[] { "@title_status", unit.TitleStatus });
            paramertList.Add(new object[] { "@loan_code", loanCode });
            paramertList.Add(new object[] { "@user_id", userId });
            paramertList.Add(new object[] { "@modified_date", DateTime.Now });

            try
            {
                return dataHandler.ExecuteSQL("spUpdateTitleStatus", paramertList) ? true : false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}