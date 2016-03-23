using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Linq;

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

        internal bool updateCurtailmets(SelectedCurtailmentList curtailmentScheduleModel , int loanId)
        {
            try
            {
                XElement xEle = new XElement("Curtailments",
                    from curtailmentShedule in curtailmentScheduleModel.SelectedCurtailmentSchedules
                    select new XElement("CurtailmentShedule",
                        new XElement("CurtNo", curtailmentShedule.CurtNumber),
                        new XElement("UnitId", curtailmentShedule.UnitId),
                        new XElement("CurtAmount", curtailmentShedule.CurtAmount),
                        new XElement("PayDate", curtailmentShedule.PayDate)
                        
                        ));
                string xmlDoc = xEle.ToString();
                

                DataHandler dataHandler = new DataHandler();
                List<object[]> paramertList2 = new List<object[]>();
                paramertList2.Add(new object[] { "@loan_id", loanId });
                paramertList2.Add(new object[] { "@Input", xmlDoc });

                try
                {
                    return dataHandler.ExecuteSQL("spUpdateCurtailmentSchedule", paramertList2);
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
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
                    paramertList2.Add(new object[] { "@payment_percentage", curtailment.PaymentPercentage });
                    try
                    {
                        executeCount = dataHandler.ExecuteSQL("spInsertCurtailment", paramertList2) ? executeCount + 1 : executeCount;
                    }
                    catch (Exception ex)
                    {
                        return 0;
                    }
                }
            }
            return executeCount;
            //if (delFlag == 2) flag = delFlag;
            //return flag; 
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2016/03/17
        /// 
        /// Insert curtailment breakdown details
        /// 
        /// 
        /// </summary>
        /// <param name="xmlDoc">curtailment breakdown list</param>
        /// <param name="unitId">unit id</param>
        /// <param name="loanId">loan id</param>
        /// <returns></returns>
        public bool InsertCurtailmentScheduleInfo(string xmlDoc, string unitId, int loanId)
        {
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList2 = new List<object[]>();
            paramertList2.Add(new object[] { "@loan_id", loanId });
            paramertList2.Add(new object[] { "@unit_id", unitId });
            paramertList2.Add(new object[] { "@Input", xmlDoc });           

            try
            {
                return dataHandler.ExecuteSQL("spInsertCurtailmentSchedule", paramertList2);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// CreatedBy : Irfan
        /// CreatedDate: 2016/03/17
        /// 
        /// Getting Curtailment Shedule
        /// 
        /// 
        /// </summary>
        /// 
        /// <param name="dueDate">due date</param>
        /// <param name="loanId">loan id</param>
        /// <returns></returns>
        public List<CurtailmentShedule> GetCurtailmentScheduleByDueDate( int loanId , DateTime dueDate)
        {
            List<CurtailmentShedule> lstCurtailmentShedule = new List<CurtailmentShedule>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@due_date", dueDate });

            DataSet dataSet = dataHandler.GetDataSet("spGetCurtailmentSheduleByDueDate", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    CurtailmentShedule curtailment = new CurtailmentShedule();
                    curtailment.UnitId = dataRow["unit_id"].ToString();
                    curtailment.LoanId = int.Parse(dataRow["loan_id"].ToString());
                    curtailment.Year = int.Parse(dataRow["year"].ToString());
                    curtailment.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"].ToString()).ToString("MM/dd/yyyy");
                    curtailment.DueDate = Convert.ToDateTime(dataRow["curt_due_date"].ToString()).ToString("MM/dd/yyyy");
                    curtailment.Status = Convert.ToInt32(dataRow["curt_status"].ToString());
                    curtailment.CurtAmount = Convert.ToDecimal(dataRow["curt_amount"].ToString());
                    curtailment.IDNumber = dataRow["identification_number"].ToString();
                    curtailment.CurtNumber = int.Parse(dataRow["curt_number"].ToString());
                    curtailment.Make = dataRow["make"].ToString();
                    curtailment.Model = dataRow["model"].ToString();
                    curtailment.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"].ToString()).ToString("MM/dd/yyyy");

                    lstCurtailmentShedule.Add(curtailment);
                }
                return lstCurtailmentShedule;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// CreatedBy : Kanishka
        /// CreatedDate: 2016/03/17
        /// 
        /// Insert curtailment breakdown details
        /// 
        /// 
        /// </summary>
        public List<UnitPayOffModel> GetUnitPayOffList(int loanId)
        {
            List<UnitPayOffModel> payOffList = new List<UnitPayOffModel>();
            
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spRetriveUnitPayoff", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    UnitPayOffModel payoff = new UnitPayOffModel();
                    payoff.LoanId = loanId;
                    payoff.UnitId = dataRow["UnitId"].ToString();
                    payoff.DateEntered = DateTime.Parse(dataRow["created_date"].ToString());
                    payoff.IdentificationNumber = dataRow["identification_number"].ToString();
                    payoff.Year = Convert.ToInt32(dataRow["year"]);
                    payoff.Make = dataRow["make"].ToString();
                    payoff.Model = dataRow["model"].ToString();
                    payoff.PurchesePrice = Convert.ToDecimal(dataRow["cost"].ToString());
                    payoff.Balance = Convert.ToDecimal(dataRow["Balance"].ToString());
                    payOffList.Add(payoff);
                }
                return payOffList;
            }
            else
            {
                return null;
            }
        }

        public int PayOffUnits(string unitIdList, DateTime payOffDate, int titleStatus)
        {
            DataHandler dataHandler = new DataHandler();

            List<object[]> paramertList1 = new List<object[]>();
            paramertList1.Add(new object[] { "@Input", unitIdList });
            paramertList1.Add(new object[] { "@pay_date", payOffDate });
            paramertList1.Add(new object[] { "@title_status", titleStatus });
            try
            {
                return dataHandler.ExecuteSQLReturn("spCurtailmentsBackup", paramertList1);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}