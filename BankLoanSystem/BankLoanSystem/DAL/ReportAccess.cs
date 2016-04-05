using System;
using System.Collections.Generic;
using System.Data;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class ReportAccess
    {
        public List<LoanIdNumber> GetLoanNumbersWithBranch(int companyId)
        {
            List<LoanIdNumber> loanNumbers = new List<LoanIdNumber>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@company_id", companyId });

            DataSet dataSet = dataHandler.GetDataSet("spGetLoanNumbersWithBranch", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    LoanIdNumber loanNumber = new LoanIdNumber();
                    loanNumber.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                    loanNumber.LoanNumberB = dataRow["LoanDisplay"].ToString();
                    loanNumber.BranchId = Convert.ToInt32(dataRow["branch_id"]);
                    loanNumbers.Add(loanNumber);
                    //unitModels.
                }
            }

            return loanNumbers;
        }

        public List<LoanDetailsRpt> GetLoanDetailsRpt(int loanId)
        {
            List<LoanDetailsRpt> loanDetails = new List<LoanDetailsRpt>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetLoanDetailsByLoanIdRpts", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    LoanDetailsRpt details = new LoanDetailsRpt();

                    details.LenderBrnchName = dataRow["branch_name"].ToString();
                    details.DealerBrnchName = dataRow["nr_branch_name"].ToString();
                    details.LoanNumber = dataRow["loan_number"].ToString();

                    loanDetails.Add(details);

                    break;
                }
            }

            return loanDetails;
        }

        public List<CurtailmentShedule> GetCurtailmentScheduleByDateRange(int loanId, DateTime dueDateStart, DateTime dueDateEnd)
        {
            List<CurtailmentShedule> lstCurtailmentShedule = new List<CurtailmentShedule>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@due_date_start", dueDateStart });
            paramertList.Add(new object[] { "@due_date_end", dueDateEnd });

            decimal totalDue = 0.00M;

            DataSet dataSet = dataHandler.GetDataSet("spGetCurtailmentSheduleByDateRange", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    CurtailmentShedule curtailment = new CurtailmentShedule();
                    curtailment.UnitId = dataRow["unit_id"].ToString();
                    curtailment.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                    curtailment.Year = Convert.ToInt32(dataRow["year"]);
                    curtailment.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"].ToString()).ToString("MM/dd/yyyy");
                    curtailment.DueDate = Convert.ToDateTime(dataRow["curt_due_date"].ToString()).ToString("MM/dd/yyyy");
                    curtailment.Status = Convert.ToInt32(dataRow["curt_status"]);
                    curtailment.CurtAmount = Convert.ToDecimal(dataRow["curt_amount"]);
                    curtailment.IDNumber = dataRow["identification_number"].ToString();
                    curtailment.CurtNumber = Convert.ToInt32(dataRow["curt_number"]);
                    curtailment.Make = dataRow["make"].ToString();
                    curtailment.Model = dataRow["model"].ToString();
                    curtailment.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"].ToString()).ToString("MM/dd/yyyy");
                    curtailment.PurchasePrice = Convert.ToDecimal(dataRow["cost"]);
                    totalDue = totalDue + Convert.ToDecimal(dataRow["curt_amount"]);
                    lstCurtailmentShedule.Add(curtailment);
                }
                if (lstCurtailmentShedule.Count > 0)
                    lstCurtailmentShedule[0].TotalAmountDue = totalDue;

                return lstCurtailmentShedule;
            }
            else
            {
                return null;
            }
        }

        public List<CurtailmentShedule> GetCurtailmentPaidDetailsByDateRange(int loanId, DateTime paidDateStart, DateTime paidDateEnd)
        {
            List<CurtailmentShedule> lstCurtailmentShedule = new List<CurtailmentShedule>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@paid_date_start", paidDateStart });
            paramertList.Add(new object[] { "@paid_date_end", paidDateEnd });

            decimal totalpaid = 0.00M;

            DataSet dataSet = dataHandler.GetDataSet("spGetCurtailmentPaidDetailsByDateRange", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    CurtailmentShedule curtailment = new CurtailmentShedule();
                    curtailment.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                    curtailment.UnitId = dataRow["UnitId"].ToString();
                    curtailment.IDNumber = dataRow["identification_number"].ToString();
                    curtailment.Year = Convert.ToInt32(dataRow["year"]);
                    curtailment.Make = dataRow["make"].ToString();
                    curtailment.Model = dataRow["model"].ToString();
                    curtailment.PurchasePrice = Convert.ToDecimal(dataRow["cost"]); 
                    curtailment.CurtNumber = Convert.ToInt32(dataRow["curt_number"]);

                    curtailment.PaidDate = Convert.ToDateTime(dataRow["paid_date"].ToString()).ToString("MM/dd/yyyy");
                    //curtailment.DueDate = Convert.ToDateTime(dataRow["curt_due_date"].ToString()).ToString("MM/dd/yyyy");
                    //curtailment.Status = Convert.ToInt32(dataRow["curt_status"]);
                    curtailment.CurtAmount = Convert.ToDecimal(dataRow["curt_amount"]);
                    curtailment.PaidCurtAmount = Convert.ToDecimal(dataRow["CurtPaidAmount"]);



                    curtailment.AdvanceDate = Convert.ToDateTime(dataRow["paid_date"].ToString()).ToString("MM/dd/yyyy");

                    totalpaid = totalpaid + Convert.ToDecimal(dataRow["CurtPaidAmount"]);
                    lstCurtailmentShedule.Add(curtailment);
                }
                if (lstCurtailmentShedule.Count > 0)
                    lstCurtailmentShedule[0].TotalAmountPaid = totalpaid;

                return lstCurtailmentShedule;
            }
            else
            {
                return null;
            }
        }

        public List<ReportUnitModels> GetAllActiveUnitDetailsRpt(int loanId)
        {
            List<ReportUnitModels> units = new List<ReportUnitModels>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetUnitLotInspection", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    ReportUnitModels unit = new ReportUnitModels();
                    unit.LoanNumber = dataRow["loan_number"].ToString();
                    unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyyy");
                    unit.IdentificationNumber = dataRow["identification_number"].ToString();
                    unit.Year = Convert.ToInt32(dataRow["year"]);
                    unit.Make = dataRow["make"].ToString();
                    unit.Model = dataRow["model"].ToString();
                    unit.Color = dataRow["color"].ToString();

                    units.Add(unit);

                    //unitModels.
                }
            }

            return units;
        }
    }
}