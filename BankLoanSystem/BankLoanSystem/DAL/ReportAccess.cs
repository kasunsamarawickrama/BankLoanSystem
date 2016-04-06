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
                //if (lstCurtailmentShedule.Count > 0)
                //    lstCurtailmentShedule[0].TotalAmountPaid = totalpaid;

                for(int i = 0; i < lstCurtailmentShedule.Count; i++ )
                    lstCurtailmentShedule[i].TotalAmountPaid = totalpaid;

                
            }
            return lstCurtailmentShedule;
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


        /// <summary>
        /// CreatedBy:Irfan
        /// CreatedDate:2016/2/24
        /// Get just added units by loan id and user id
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns>list of units</returns>
        public List<Unit> GetJustAddedUnitDetails(int userId, int loanId)
        {

            List<Unit> justAddedUnitList = new List<Unit>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@user_id", userId });

            DataSet dataSet = dataHandler.GetDataSet("spGetJustAddedUnitsByLoanId", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    


                    Unit justAddedUnit = new Unit();

                    justAddedUnit.Model = dataRow["model"].ToString();
                    justAddedUnit.AdvanceAmount = (dataRow["advance_amount"]) != DBNull.Value ? (Decimal)dataRow["advance_amount"] : (Decimal)0.00;
                    justAddedUnit.IsAdvanced = Convert.ToBoolean(dataRow["is_advanced"]);
                    justAddedUnit.IdentificationNumber = dataRow["identification_number"].ToString();

                    justAddedUnit.Year = Convert.ToInt32(dataRow["year"]);
                    justAddedUnit.Make = dataRow["make"].ToString();
                    justAddedUnit.CreatedDate = Convert.ToDateTime(dataRow["created_date"].ToString());
                    justAddedUnit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"].ToString());
                    justAddedUnit.TitleStatus = Convert.ToInt32(dataRow["title_status"]);
                    justAddedUnitList.Add(justAddedUnit);
                    
                }
            }

            return justAddedUnitList;

        }


        public List<Unit> GeUnitDetailsByTitleStatus(int loanId, int titleStatus)
        {

            List<Unit> units = new List<Unit>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@title_status", titleStatus });

            DataSet dataSet = dataHandler.GetDataSet("spGetUnitsByLoanIdTitleStatus", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    Unit unit = new Unit();

                    unit.IdentificationNumber = dataRow["identification_number"].ToString();
                    unit.Year = Convert.ToInt32(dataRow["year"]);
                    unit.Make = dataRow["make"].ToString();
                    unit.Model = dataRow["model"].ToString();
                    unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"].ToString());
                    unit.AdvanceDateStr = unit.AdvanceDate.ToString("MM/dd/yyyy")
                    ;
                    unit.TitleStatus = Convert.ToInt32(dataRow["title_status"]);

                    if (unit.TitleStatus == 0)
                    {
                        unit.TitleStatusText = "Not Received";
                    }
                    else if (unit.TitleStatus == 1)
                    {
                        unit.TitleStatusText = "Received";
                    }
                    else if(unit.TitleStatus == 2)
                    {
                        unit.TitleStatusText = "Returned to Dealer";
                    }

                    unit.AdvanceAmount = (dataRow["advance_amount"]) != DBNull.Value ? (Decimal)dataRow["advance_amount"] : (Decimal)0.00;
                    unit.IsAdvanced = Convert.ToBoolean(dataRow["is_advanced"]);
                    unit.CreatedDate = Convert.ToDateTime(dataRow["created_date"].ToString());
                    
                    units.Add(unit);

                }
            }

            return units;

        }

        public List<ReportFullInventoryUnit> GetFullInventoryByLoanId(int loanId)
        {
            List<ReportFullInventoryUnit> units = new List<ReportFullInventoryUnit>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetFullInventoryByLoanId", paramertList);

            decimal loanBalance = 0.00M;
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    ReportFullInventoryUnit unit = new ReportFullInventoryUnit();

                    unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyyy");
                    unit.IdentificationNumber = dataRow["identification_number"].ToString();
                    unit.Year = Convert.ToInt32(dataRow["year"]);
                    unit.Make = dataRow["make"].ToString();
                    unit.Model = dataRow["model"].ToString();
                    unit.PurchasePrice = Convert.ToDecimal(dataRow["cost"]);
                    unit.AdvanceAmount = Convert.ToDecimal(dataRow["cost"]);
                    unit.TotalCurtPaid = Convert.ToDecimal(dataRow["cost"]);
                    unit.BalanceDue = Convert.ToDecimal(dataRow["cost"]);

                    int status = Convert.ToInt32(dataRow["title_status"]);

                    if (status == 0)
                    {
                        unit.TitleStatus = "Not Received";
                    }
                    else if (status == 1)
                    {
                        unit.TitleStatus = "Received";
                    }
                    else if (status == 2)
                    {
                        unit.TitleStatus = "Returned to Dealer";
                    }
                    loanBalance += unit.BalanceDue;
                    units.Add(unit);

                }

                if (units.Count > 0)
                    units[0].LoanBalance = loanBalance;
            }

            return units;
        }
    }
}