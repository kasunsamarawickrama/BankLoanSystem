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

        /*

   Frontend page: Fee Page
   Title: Getting Curtailment Shedule
   Designed: Irfan Mam
   User story:
   Developed: Irfan MAM
   Date created: 4/22/2016

*/

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



        /*

 Frontend page: Fee Page
 Title: Getting Fees by due date
 Designed: Nadeeka
 User story:
 Developed: Nadeeka
 Date created: 4/21/2016

*/
        public List<Fees> GetFeesByDueDate(int loanId, DateTime dueDate, string type)
        {
            try
            {
                List<Fees> lstFee = new List<Fees>();
                DataHandler dataHandler = new DataHandler();
                List<object[]> paramertList = new List<object[]>();
                paramertList.Add(new object[] { "@loan_id", loanId });
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
                        fee.FeeDueDate = Convert.ToDateTime(dataRow["due_date"].ToString());

                        if (type == "advanceFee")
                        {
                            string[] info = fee.Description.Split(',');
                            if (info != null && info.Length > 0)
                            {
                                if (info[1] != "")
                                {
                                    fee.IdentificationNumber = info[1];
                                }
                                if (info.Length > 2 && info[2] != "")
                                {
                                    fee.Year = Convert.ToInt32(info[2]);
                                }
                                if (info.Length > 3 && info[3] != "")
                                {
                                    fee.Make = info[3];
                                }
                                if (info.Length > 4 && info[4] != "")
                                {
                                    fee.Model = info[4];
                                }
                                if (info.Length > 5 && info[5] != "")
                                {
                                    fee.AdvanceDate = Convert.ToDateTime(info[5]);
                                }
                                else
                                {
                                    fee.AdvanceDate = Convert.ToDateTime(dataRow["due_date"].ToString());
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


        /*

 Frontend page: Fee Page
 Title: Update Fees 
 Designed: Irfan MAM
 User story:
 Developed: Irfan MAM
 Date created: 4/22/2016

*/
        internal int updateFees(List<Fees> lstFee,DateTime paidDate, int loanId , int userId)
        {
            try
            {
                int i = 1;

                // bind the list of fee details to xml 
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
                catch 
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