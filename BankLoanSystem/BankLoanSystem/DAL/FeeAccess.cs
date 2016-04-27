using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BankLoanSystem.DAL
{
    public class FeeAccess
    {

        /// <summary>
        /// CreatedBy : Irfan
        /// CreatedDate: 04/22/2016
        /// 
        /// Getting Curtailment Shedule
        /// 
        /// 
        /// </summary>
        /// 
        /// <param name="dueDate">due date</param>
        /// <param name="loanId">loan id</param>
        /// <returns></returns>
        public bool GetFeesDueDates(int loanId, out string advPayDueDate, out string monPayDueDate, out string lotPayDueDate)
        {
            try
            {
                advPayDueDate = "";
                monPayDueDate = "";
                lotPayDueDate = "";

                DataHandler dataHandler = new DataHandler();
                List<object[]> paramertList = new List<object[]>();
                paramertList.Add(new object[] { "@loan_id", loanId });


                DataSet dataSet = dataHandler.GetDataSet("spGetFeesDueDates", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        advPayDueDate = dataRow["adv_payment_due_date"].ToString();
                        monPayDueDate = dataRow["mon_payment_due_date"].ToString();
                        lotPayDueDate = dataRow["lot_payment_due_date"].ToString();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 04/21/2016
        /// 
        /// Getting Fees by due date
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="dueDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Fees> GetFeesByDueDate(int loanId, DateTime dueDate, string type)
        {
            try
            {
                List<Fees> lstFee = new List<Fees>();
                DataHandler dataHandler = new DataHandler();
                List<object[]> paramertList = new List<object[]>();
                paramertList.Add(new object[] { "@loan_id", loanId });
                //paramertList.Add(new object[] { "@unit_id", loanId });
                paramertList.Add(new object[] { "@bill_due_date", dueDate });
                paramertList.Add(new object[] { "@type", type });

                DataSet dataSet = dataHandler.GetDataSet("spGetFeesByDueDate", paramertList);
                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        Fees fee = new Fees();
                        fee.FeeId = int.Parse(dataRow["fee_id"].ToString());
                        fee.UnitId = dataRow["unit_id"].ToString();
                        fee.LoanId = int.Parse(dataRow["loan_id"].ToString());
                        fee.Type = dataRow["type"].ToString();
                        fee.Amount = Convert.ToDecimal(dataRow["amount"].ToString());
                        fee.Description = dataRow["description"].ToString();
                        fee.BillDueDate = Convert.ToDateTime(dataRow["bill_due_date"].ToString());
                        fee.AdvanceDate = Convert.ToDateTime(dataRow["due_date"].ToString());

                        if (type == "advanceFee")
                        {
                            string[] info = fee.Description.Split(',');
                            if (info != null && info.Length > 0)
                            {
                                if (info[1] != "")
                                {
                                    fee.IdentificationNumber = info[1];
                                }
                                if (info.Length > 1 && info[2] != "")
                                {
                                    fee.Year = Convert.ToInt32(info[2]);
                                }
                                if (info.Length > 2 && info[3] != "")
                                {
                                    fee.Make = info[3];
                                }
                                if (info.Length > 3 && info[4] != "")
                                {
                                    fee.Model = info[4];
                                }
                            }
                        }
                        lstFee.Add(fee);
                    }
                    return lstFee;
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
        
        internal int updateFees(List<Fees> lstFee,DateTime paidDate, int loanId , int userId)
        {
            try
            {
                int i = 1;
                XElement xEle = new XElement("Fee",
                    from fee in lstFee
                    select new XElement("FeeUnit",
                        new XElement("FeeId", fee.FeeId),
                        new XElement("Type", fee.Type),
                       
                        
                        new XElement("id", i++)
                        ));
                string xmlDoc = xEle.ToString();


                DataHandler dataHandler = new DataHandler();
                List<object[]> paramertList2 = new List<object[]>();
                paramertList2.Add(new object[] { "@loan_id", loanId });
                paramertList2.Add(new object[] { "@paid_date", paidDate });
                paramertList2.Add(new object[] { "@user_id", userId });
                paramertList2.Add(new object[] { "@Input", xmlDoc });

                try
                {
                    return dataHandler.ExecuteSQLWithReturnVal("spUpdateFee", paramertList2);
                }
                catch (Exception ex)
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}