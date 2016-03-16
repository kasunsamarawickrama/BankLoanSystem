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
    /// 
    /// CreatedBy : nadeeka
    /// CreatedDate: 2016/02/09
    /// 
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
        public List<Curtailment> retreiveCurtailmentByLoanId(int loanId)
        {
            List<Curtailment> lstCurtailment = new List<Curtailment>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spRetrieveCurtailmentByLoanId", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    Curtailment curtailment = new Curtailment();
                    curtailment.CurtailmentId = int.Parse(dataRow["curtailment_id"].ToString());
                    curtailment.LoanId = int.Parse(dataRow["loan_id"].ToString());
                    curtailment.TimePeriod = Convert.ToInt32(dataRow["time_period"]);
                    curtailment.Percentage = Convert.ToInt32(dataRow["percentage"].ToString());
                    lstCurtailment.Add(curtailment);
                }
                return lstCurtailment;
            }
            else
            {
                return null;
            }
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
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetLoanDetailsByLoanId", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
            {
                DataRow dataRow = dataSet.Tables[0].Rows[0];
                loan.loanNumber = dataRow["loan_number"].ToString();
                loan.startDate = Convert.ToDateTime(dataRow["start_date"]);
                loan.maturityDate = Convert.ToDateTime(dataRow["maturity_date"]);
                loan.loanAmount = Convert.ToDecimal(dataRow["loan_amount"]);
                loan.advancePercentage = Convert.ToInt32(dataRow["advance"]);
                //0 - day, 1 - month
                loan.payOffPeriodType = 1;
                if (dataRow["pay_off_type"].ToString() == "d")
                    loan.payOffPeriodType = 0;

                loan.payOffPeriod = Convert.ToInt32(dataRow["pay_off_period"]);
                loan.LoanStatus = Convert.ToBoolean(dataRow["loan_status"]);
                loan.isInterestCalculate = Convert.ToBoolean(dataRow["is_interest_calculate"]);
                loan.isEditAllowable = Convert.ToBoolean(dataRow["is_edit_allowable"]);
                loan.CurtailmentDueDate = dataRow["curtailment_due_date"].ToString();
                loan.CurtailmentAutoRemindEmail = dataRow["curtailment_auto_remind_email"].ToString();
                loan.CurtailmentEmailRemindPeriod = Convert.ToInt32(dataRow["curtailment_remind_period"].ToString());
                loan.CurtailmentCalculationBase = dataRow["curtailment_calculation_type"].ToString();

                return loan;
            }
            else
            {
                return null;
            }           
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
        public int InsertCurtailment(List<Curtailment> lstCurtailment, int loanId)
        {
            //int flag = 0;
            //int delFlag = 0;

            int executeCount = 0;
            DataHandler dataHandler = new DataHandler();

            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            if (dataHandler.ExecuteSQL("spDeleteCurtailment", paramertList))
            {
                foreach (Curtailment curtailment in lstCurtailment)
                {
                    List<object[]> paramertList2 = new List<object[]>();
                    paramertList2.Add(new object[] { "@loan_id", loanId });
                    paramertList2.Add(new object[] { "@curtailment_id", curtailment.CurtailmentId });
                    paramertList2.Add(new object[] { "@time_period", curtailment.TimePeriod });
                    paramertList2.Add(new object[] { "@percentage", curtailment.Percentage });

                    try
                    {
                        executeCount = dataHandler.ExecuteSQL("spInsertCurtailment", paramertList2) ? executeCount + 1 : executeCount;
                    }
                    catch(Exception ex)
                    {
                        return 0;
                    }
                }
            }
            return executeCount;
            //if (delFlag == 2) flag = delFlag;
            //return flag; 
        }
       
        public void InsertCurtailmentSchedule()
        {

        }
    }
}