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
    }
}