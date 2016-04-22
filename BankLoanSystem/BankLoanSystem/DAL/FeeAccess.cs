using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

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
        public bool GetFeesDueDates(int loanId, out string advPayDueDate  , out string monPayDueDate , out string lotPayDueDate)
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

        /// <summary>
        ///CreatedBy : Nadeeka
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
                    fee.BillDueDate = Convert.ToDateTime(dataRow["bill_due_date"].ToString());//.ToString("MM/dd/yyyy"

                    if (type.Equals("advanceFee"))
                    {
                        if (!string.IsNullOrEmpty(dataRow["identification_number"].ToString()))
                        {
                            fee.IdentificationNumber = dataRow["identification_number"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dataRow["year"].ToString()))
                        {
                            fee.Year = int.Parse(dataRow["year"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dataRow["make"].ToString()))
                        {
                            fee.Make = dataRow["make"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dataRow["model"].ToString()))
                        {
                            fee.Model = dataRow["model"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dataRow["advance_date"].ToString()))
                        {
                            fee.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"].ToString());
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
    }
}