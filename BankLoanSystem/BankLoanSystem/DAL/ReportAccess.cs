using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.Mvc;
using BankLoanSystem.Models;

namespace BankLoanSystem.DAL
{
    public class ReportAccess : Controller
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
                }
            }

            return loanNumbers;
        }


        /*

    Frontend page: Report Page
    Title: Get active account details of (Super Admin/ Admin)
    Designed: Irfan Mam
    User story: DFP-446
    Developed: Irfan MAM
    Date created: 6/23/2016

*/

        public List<Account> GetAccountDetails(int companyIdORBranchId, int userRole)
        {
            List<Account> Accounts = new List<Account>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

            // if user is super admin pass company id for companyIdORBranchId
            // if user is a admin pass  branch id for companyIdORBranchId
            paramertList.Add(new object[] { "@comp_or_branch_id", companyIdORBranchId });
            paramertList.Add(new object[] { "@user_role", userRole });

            // calling stored procedure
            DataSet dataSet = dataHandler.GetDataSet("GetAccountDetailsByUserRole", paramertList);

            // biding data to list
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    Account account = new Account();
                    account.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                    account.BranchId = Convert.ToInt32(dataRow["branch_id"]);
                    account.LoanNumber = dataRow["loan_number"].ToString();
                    account.BranchName = dataRow["branch_name"].ToString();
                    account.PatBranchName = dataRow["nr_branch_name"].ToString();
                    account.LoanAmount = Convert.ToDecimal(dataRow["loan_amount"].ToString());
                    account.UsedAmount = Convert.ToDecimal(dataRow["used_amount"].ToString());
                    account.ActiveUnits = Convert.ToInt32(dataRow["active_units"]);
                    account.PatBranchAddress1 = dataRow["branch_address_1"].ToString();
                    account.PatBranchAddress2 = dataRow["branch_address_2"].ToString();
                    account.PatCity = dataRow["city"].ToString();
                    Accounts.Add(account);

                }


            }


            // returning the List of account details of relevant user (Super Admin/ Admin)
            return Accounts;
        }

        /*

       Frontend page: Report Page
       Title: Get active account details of Users who has report rights(User/ Dealer User)
       Designed: Irfan Mam
       User story: DFP-474
       Developed: Irfan MAM
       Date created: 6/29/2016

*/

        public List<Account> GetAccountDetailsForUser( int userId)
        {
            List<Account> Accounts = new List<Account>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();

           
           // add user id to parameter list
            paramertList.Add(new object[] { "@user_id", userId });

            // calling stored procedure
            DataSet dataSet = dataHandler.GetDataSet("GetAccountDetailsByUserId", paramertList);

            // biding data to list
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    Account account = new Account();
                    account.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                    
                    account.LoanNumber = dataRow["loan_number"].ToString();
                    
                    account.PatBranchName = dataRow["branch_name"].ToString();
                    account.LoanAmount = Convert.ToDecimal(dataRow["loan_amount"].ToString());
                    account.UsedAmount = Convert.ToDecimal(dataRow["used_amount"].ToString());
                    account.ActiveUnits = Convert.ToInt32(dataRow["active_units"]);
                    account.PatBranchAddress1 = dataRow["branch_address_1"].ToString();
                    account.PatBranchAddress2 = dataRow["branch_address_2"].ToString();
                    account.PatCity = dataRow["city"].ToString();
                    account.PatCity = dataRow["report_rights"].ToString();
                    Accounts.Add(account);

                }


            }


            // returning the List of account details of relevant user
            return Accounts;
        }




        /*

     Frontend page: Report Page
     Title: Get number of authorized loan details of the User who has report rights (User/ Dealer User)
     Designed: Irfan Mam
     User story: DFP-474
     Developed: Irfan MAM
     Date created: 6/29/2016

*/

        public int GetLoanCountAccountDetailsForUser(int userId)
        {
            int numberOfAccounts = 0;

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();


            // add user id to parameter list
            paramertList.Add(new object[] { "@user_id", userId });

            // calling stored procedure
            DataSet dataSet = dataHandler.GetDataSet("GetLoanCountAccountUserId", paramertList);

            // biding data to list
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    
                    numberOfAccounts = Convert.ToInt32(dataRow["loan_count"]);
   
                    

                }


            }


            // returning the number of accounts
            return numberOfAccounts;
        }

        public List<UserRights> GeUserRightsForReporting(int userId)
        {
            List<UserRights> userLoanRights = new List<UserRights>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", userId });

            DataSet dataSet = dataHandler.GetDataSet("spGeUserRightsForReporting", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    UserRights userRights = new UserRights();
                    userRights.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                    userRights.PermissionList = dataRow["right_id"].ToString();
                    userLoanRights.Add(userRights);
                }
            }

            return userLoanRights;
        }

        /*

            Frontend page: Report viewr(Report Page)
            Title: Get loan details
            Designed: Kanishk SHM
            User story: 
            Developed: Kanishk SHM
            Date created: 

        */

        public List<LoanDetailsRpt> TopHeaderDetails(int loanId, int userId)
        {
            List<LoanDetailsRpt> loanDetails = new List<LoanDetailsRpt>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@user_id", userId });
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetTopHeaderDetailsRpts", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    LoanDetailsRpt details = new LoanDetailsRpt();

                    details.CreaterName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dataRow["first_name"] + " " + dataRow["last_name"]);
                    details.LenderBrnchName = dataRow["branch_name"].ToString();
                    details.DealerBrnchName = dataRow["nr_branch_name"].ToString();
                    details.LoanNumber = dataRow["loan_number"].ToString();

                    loanDetails.Add(details);

                    break;
                }
            }

            return loanDetails;
        }

        public List<LoanDetailsRpt> GetLoanDetailsRpt(int loanId)
        {
            List<LoanDetailsRpt> loanDetails = new List<LoanDetailsRpt>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            //paramertList.Add(new object[] { "@user_id", userId });
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

        /*

            Frontend page: Report viewr(Report Page)
            Title: Get curtailment details of units by date range
            Designed: Kanishk SHM
            User story: 
            Developed: Kanishk SHM
            Date created: 

        */
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
            return null;
        }

        /*

            Frontend page: Report viewr(Report Page)
            Title: Get curtailment payment details of units by date range
            Designed: Kanishk SHM
            User story: 
            Developed: Kanishk SHM
            Date created: 

        */
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
                    curtailment.CurtAmount = Convert.ToDecimal(dataRow["curt_amount"]);
                    curtailment.PaidCurtAmount = Convert.ToDecimal(dataRow["CurtPaidAmount"]);



                    curtailment.AdvanceDate = Convert.ToDateTime(dataRow["paid_date"].ToString()).ToString("MM/dd/yyyy");

                    totalpaid = totalpaid + Convert.ToDecimal(dataRow["CurtPaidAmount"]);
                    lstCurtailmentShedule.Add(curtailment);
                }

                for (int i = 0; i < lstCurtailmentShedule.Count; i++)
                    lstCurtailmentShedule[i].TotalAmountPaid = totalpaid;


            }
            return lstCurtailmentShedule;
        }

        /*

            Frontend page: Report viewr(Report Page)
            Title: Get all active units details by loan id
            Designed: Kanishk SHM
            User story: 
            Developed: Kanishk SHM
            Date created: 

        */
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
                    unit.IdentificationNumber = dataRow["identification_number"].ToString();
                    unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyyy");
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
        public List<RptAddUnit> GetJustAddedUnitDetails(int userId, int loanId)
        {

            List<RptAddUnit> justAddedUnitList = new List<RptAddUnit>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@user_id", userId });

            DataSet dataSet = dataHandler.GetDataSet("spGetJustAddedUnitsByLoanId", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    RptAddUnit justAddedUnit = new RptAddUnit();

                    justAddedUnit.AddDate = !dataRow.IsNull("created_date") ? Convert.ToDateTime(dataRow["created_date"]).ToString("MM/dd/yyy") : "";
                    justAddedUnit.AdvanceDate = !dataRow.IsNull("advance_date") ? Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyy") : "";

                    justAddedUnit.IdentificationNumber = dataRow["identification_number"].ToString();
                    justAddedUnit.Year = Convert.ToInt32(dataRow["year"]);
                    justAddedUnit.Make = dataRow["make"].ToString();
                    justAddedUnit.Model = dataRow["model"].ToString();
                    justAddedUnit.PurchasePrice = (dataRow["cost"]) != DBNull.Value ? (Decimal)dataRow["cost"] : (Decimal)0.00M;
                    justAddedUnit.AdvanceAmount = (dataRow["advance_amount"]) != DBNull.Value ? (Decimal)dataRow["advance_amount"] : (Decimal)0.00M;
                    justAddedUnit.UnitStaus = Convert.ToBoolean(dataRow["is_advanced"]) ? "Advanced" : "Not Advanced";
                    int status = Convert.ToInt32(dataRow["title_status"]);

                    if (status == 0)
                    {
                        justAddedUnit.TitleStatus = "Not Received";
                    }
                    else if (status == 1)
                    {
                        justAddedUnit.TitleStatus = "Received";
                    }
                    else if (status == 2)
                    {
                        justAddedUnit.TitleStatus = "Returned to Dealer";
                    }

                    justAddedUnitList.Add(justAddedUnit);

                }
            }

            return justAddedUnitList;

        }

        /*

            Frontend page: Report viewr(Report Page)
            Title: Get unit details by title status
            Designed: Kanishk SHM
            User story: 
            Developed: Kanishk SHM
            Date created: 

        */
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
                    else if (unit.TitleStatus == 2)
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

        /*

            Frontend page: Report viewr(Report Page)
            Title: Get all active unit details
            Designed: Kanishk SHM
            User story: 
            Developed: Kanishk SHM
            Date created: 

        */
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
                    unit.AdvanceAmount = Convert.ToDecimal(dataRow["advance_amount"]);
                    unit.TotalCurtPaid = Convert.ToDecimal(dataRow["TotalPaid"]);
                    unit.BalanceDue = Convert.ToDecimal(dataRow["Balance"]);

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

        /*

            Frontend page: Report viewr(Report Page)
            Title: Get advance unit details(this session)
            Designed: Kanishk SHM
            User story: 
            Developed: Kanishk SHM
            Date created: 

        */
        public List<RptAddUnit> AdvanceUnitsDuringSession(string unitIdList)
        {
            List<RptAddUnit> AdvanceUnits = new List<RptAddUnit>();

            DataHandler dataHandler = new DataHandler();

            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@Input", unitIdList });
            try
            {
                DataSet dataSet = dataHandler.GetDataSet("spGetAdvanceUnitsDuringSession", paramertList);

                if (dataSet != null && dataSet.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        RptAddUnit unit = new RptAddUnit();
                        unit.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                        unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyy");
                        unit.IdentificationNumber = dataRow["identification_number"].ToString();
                        unit.Year = Convert.ToInt32(dataRow["year"]);
                        unit.Make = dataRow["make"].ToString();
                        unit.Model = dataRow["model"].ToString();
                        unit.AdvanceAmount = (dataRow["advance_amount"]) != DBNull.Value ? (Decimal)dataRow["advance_amount"] : (Decimal)0.00M;
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

                        AdvanceUnits.Add(unit);

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return AdvanceUnits;
        }

        public List<RptFee> GetFeeInvoiceByDateRange(int loanId, string type, DateTime dueDateStart, DateTime dueDateEnd)
        {
            List<RptFee> feeInvoiceData = new List<RptFee>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@type", type });
            paramertList.Add(new object[] { "@due_date_start", dueDateStart });
            paramertList.Add(new object[] { "@due_date_end", dueDateEnd });

            decimal totalDue = 0.00M;

            DataSet dataSet = dataHandler.GetDataSet("spGetFeeInvoiceDetailsByDateRange", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    RptFee feeInvoice = new RptFee();
                    feeInvoice.IdentificationNumber = dataRow["identification_number"].ToString();
                    feeInvoice.Year = (dataRow["year"]) != DBNull.Value ? (Int32)dataRow["year"] : 0000;//Convert.ToInt32(dataRow["year"]);
                    feeInvoice.Make = dataRow["make"].ToString();
                    feeInvoice.Model = dataRow["model"].ToString();
                    feeInvoice.DueDate = Convert.ToDateTime(dataRow["due_date"].ToString()).ToString("MM/dd/yyyy");
                    feeInvoice.PurchasePrice = (dataRow["cost"]) != DBNull.Value ? (Decimal)dataRow["advance_amount"] : (Decimal)0.00M;//Convert.ToDecimal(dataRow["cost"]);
                    feeInvoice.AdvanceAmount = Convert.ToDecimal(dataRow["amount"]);
                    totalDue = totalDue + Convert.ToDecimal(dataRow["amount"]);
                    feeInvoiceData.Add(feeInvoice);
                }
                if (feeInvoiceData.Count > 0)
                    feeInvoiceData[0].TotalAdvanceAmount = totalDue;

                return feeInvoiceData;
            }
            else
            {
                return null;
            }
        }

        public List<ReportFullInventoryUnit> GetAdvanceUnitByLoanId(int loanId, DateTime advanceDateStart, DateTime advanceDateEnd)
        {
            List<ReportFullInventoryUnit> units = new List<ReportFullInventoryUnit>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@advance_date_start", advanceDateStart });
            paramertList.Add(new object[] { "@advance_date_end", advanceDateEnd });

            DataSet dataSet = dataHandler.GetDataSet("spRptAdvanceUnitByDateRange", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    ReportFullInventoryUnit unit = new ReportFullInventoryUnit();

                    unit.IdentificationNumber = dataRow["identification_number"].ToString();
                    unit.Year = Convert.ToInt32(dataRow["year"]);
                    unit.Make = dataRow["make"].ToString();
                    unit.Model = dataRow["model"].ToString();
                    unit.PurchasePrice = Convert.ToDecimal(dataRow["cost"]);
                    unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyyy");
                    unit.AdvanceAmount = Convert.ToDecimal(dataRow["advance_amount"]);

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
                    units.Add(unit);

                }
            }

            return units;
        }

        /// <summary>
        /// Created By: IRFAN MAM
        /// 
        /// 
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="advanceDateStart"></param>
        /// <param name="advanceDateEnd"></param>
        /// <returns></returns>
        public List<ReportPayOff> GetPayOffDetailsByLoanId(int loanId, DateTime advanceDateStart, DateTime advanceDateEnd)
        {
            List<ReportPayOff> units = new List<ReportPayOff>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@date_start", advanceDateStart });
            paramertList.Add(new object[] { "@date_end", advanceDateEnd });

            DataSet dataSet = dataHandler.GetDataSet("spRptPayOffDetailsByDateRange", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    ReportPayOff unit = new ReportPayOff();

                    unit.IdentificationNumber = dataRow["identification_number"].ToString();
                    unit.Year = Convert.ToInt32(dataRow["year"]);
                    unit.Make = dataRow["make"].ToString();
                    unit.Model = dataRow["model"].ToString();
                    unit.PurchasePrice = Convert.ToDecimal(dataRow["cost"]);

                    unit.AdvanceAmount = Convert.ToDecimal(dataRow["advance_amount"]);
                    unit.PayOffAmount = Convert.ToDecimal(dataRow["payoff_amount"]);
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
                    unit.AdvanceDate = Convert.ToDateTime(dataRow["advance_date"]).ToString("MM/dd/yyyy");
                    unit.PayOffDate = Convert.ToDateTime(dataRow["pay_date"]).ToString("MM/dd/yyyy");
                    units.Add(unit);

                }
            }

            return units;
        }


        public List<RptFee> GetFeeReceiptByDateRange(int loanId, string type, DateTime dueDateStart, DateTime dueDateEnd)
        {
            List<RptFee> feeInvoiceData = new List<RptFee>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@type", type });
            paramertList.Add(new object[] { "@paid_date_start", dueDateStart });
            paramertList.Add(new object[] { "@paid_date_end", dueDateEnd });

            decimal totalDue = 0.00M;

            DataSet dataSet = dataHandler.GetDataSet("spGetFeeReceiptDetailsByDateRange", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    RptFee feeInvoice = new RptFee();
                    feeInvoice.IdentificationNumber = dataRow["identification_number"].ToString();
                    feeInvoice.Year = (dataRow["year"]) != DBNull.Value ? (Int32)dataRow["year"] : 0000;//Convert.ToInt32(dataRow["year"]);
                    feeInvoice.Make = dataRow["make"].ToString();
                    feeInvoice.Model = dataRow["model"].ToString();
                    feeInvoice.DueDate = Convert.ToDateTime(dataRow["paid_date"].ToString()).ToString("MM/dd/yyyy");
                    feeInvoice.PurchasePrice = (dataRow["cost"]) != DBNull.Value ? (Decimal)dataRow["advance_amount"] : (Decimal)0.00M;//Convert.ToDecimal(dataRow["cost"]);
                    feeInvoice.AdvanceAmount = Convert.ToDecimal(dataRow["amount"]);
                    totalDue = totalDue + Convert.ToDecimal(dataRow["amount"]);
                    feeInvoiceData.Add(feeInvoice);
                }
                if (feeInvoiceData.Count > 0)
                    feeInvoiceData[0].TotalAdvanceAmount = totalDue;

                return feeInvoiceData;
            }
            else
            {
                return null;
            }
        }


        public List<CurtailmentShedule> GetCurtailmentPaidDetailsDuringSession()
        {
            List<CurtailmentShedule> lstCurtailmentShedule = new List<CurtailmentShedule>();
            //DataHandler dataHandler = new DataHandler();
            //List<object[]> paramertList = new List<object[]>();
            //paramertList.Add(new object[] { "@loan_id", loanId });
            //paramertList.Add(new object[] { "@paid_date_start", paidDateStart });
            //paramertList.Add(new object[] { "@paid_date_end", paidDateEnd });

            decimal totalpaid = 0.00M;

            //DataSet dataSet = dataHandler.GetDataSet("spGetCurtailmentPaidDetailsByDateRange", paramertList);
            //if (dataSet != null && dataSet.Tables.Count != 0)
            //{
            //    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            //    {
            //        CurtailmentShedule curtailment = new CurtailmentShedule();
            //        curtailment.LoanId = Convert.ToInt32(dataRow["loan_id"]);
            //        curtailment.UnitId = dataRow["UnitId"].ToString();
            //        curtailment.IDNumber = dataRow["identification_number"].ToString();
            //        curtailment.Year = Convert.ToInt32(dataRow["year"]);
            //        curtailment.Make = dataRow["make"].ToString();
            //        curtailment.Model = dataRow["model"].ToString();
            //        curtailment.PurchasePrice = Convert.ToDecimal(dataRow["cost"]);
            //        curtailment.CurtNumber = Convert.ToInt32(dataRow["curt_number"]);

            //        curtailment.PaidDate = Convert.ToDateTime(dataRow["paid_date"].ToString()).ToString("MM/dd/yyyy");
            //        //curtailment.DueDate = Convert.ToDateTime(dataRow["curt_due_date"].ToString()).ToString("MM/dd/yyyy");
            //        //curtailment.Status = Convert.ToInt32(dataRow["curt_status"]);
            //        curtailment.CurtAmount = Convert.ToDecimal(dataRow["curt_amount"]);
            //        curtailment.PaidCurtAmount = Convert.ToDecimal(dataRow["CurtPaidAmount"]);



            //        curtailment.AdvanceDate = Convert.ToDateTime(dataRow["paid_date"].ToString()).ToString("MM/dd/yyyy");

            //        totalpaid = totalpaid + Convert.ToDecimal(dataRow["CurtPaidAmount"]);
            //        lstCurtailmentShedule.Add(curtailment);
            //    }
            //if (lstCurtailmentShedule.Count > 0)
            //    lstCurtailmentShedule[0].TotalAmountPaid = totalpaid;

            for (int i = 0; i < lstCurtailmentShedule.Count; i++)
                lstCurtailmentShedule[i].TotalAmountPaid = totalpaid;


            //}
            return lstCurtailmentShedule;
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:5/9/2016
        /// 
        /// EditedBy: Kanishka
        /// EditedDate:6/17/2016
        /// 
        /// Get all transaction details by date range
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="dueDateStart"></param>
        /// <param name="dueDateEnd"></param>
        /// <returns></returns>
        public List<ReportTransactionHistory> GetTransactionHistoryByDateRange(int loanId, DateTime dueDateStart, DateTime dueDateEnd)
        {
            List<ReportTransactionHistory> loanhiHistories = new List<ReportTransactionHistory>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@due_date_start", dueDateStart });
            paramertList.Add(new object[] { "@due_date_end", dueDateEnd });

            //DataSet dataSet = dataHandler.GetDataSet("spGetLoanSummary", paramertList);
            DataSet dataSet = dataHandler.GetDataSet("spGetTransactionHistory", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    ReportTransactionHistory loanHistory = new ReportTransactionHistory();
                    loanHistory.IdentificationNumber = dataRow["identification_number"].ToString();
                    loanHistory.Year = (dataRow["year"]) != DBNull.Value ? (Int32)dataRow["year"] : 0000;
                    loanHistory.Make = dataRow["make"].ToString();
                    loanHistory.Model = dataRow["model"].ToString();
                    loanHistory.TransactionDate = Convert.ToDateTime(dataRow["transac_date"].ToString()).ToString("MM/dd/yyyy");
                    loanHistory.TransactionType = dataRow["transac_Type"].ToString();
                    loanHistory.TransactionAdvanced = (dataRow["debit_amount"]) != DBNull.Value ? (Decimal)dataRow["debit_amount"] : (Decimal)0.00M;
                    loanHistory.TransactionDeducted = (dataRow["credit_amount"]) != DBNull.Value ? (Decimal)dataRow["credit_amount"] : (Decimal)0.00M;

                    loanhiHistories.Add(loanHistory);
                }

                return loanhiHistories;
            }
            return null;
        }

        /// <summary>
        /// CreatedBy: Kanishka
        /// CreatedDate: 5/28/2016
        /// Get unit cost by unit id list
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public List<CurtailmentShedule> GetCurtailmentPaidDetailsDuringSession(string xmlDoc)
        {
            List<CurtailmentShedule> lstCurtailmentShedule = new List<CurtailmentShedule>();
            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@Input", xmlDoc });

            DataSet dataSet = dataHandler.GetDataSet("spGetCurtailmentPaidDetailsDuringSession", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    CurtailmentShedule curtailment = new CurtailmentShedule();
                    curtailment.UnitId = dataRow["unit_id"].ToString();
                    curtailment.PurchasePrice = Convert.ToDecimal(dataRow["cost"]);
                    lstCurtailmentShedule.Add(curtailment);
                }
            }
            return lstCurtailmentShedule;
        }

        #region Loan Term report

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public List<RptLoanTerms> RptLoanTermsDetails(int loanId)
        {
            List<RptLoanTerms> loanTerms = new List<RptLoanTerms>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetLoanDetailForLoanTerms", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    RptLoanTerms loanTerm = new RptLoanTerms();

                    loanTerm.CompanyBranch = dataRow["branch_name"].ToString();
                    loanTerm.PartnerBranch = dataRow["non_reg_branch_name"].ToString();
                    loanTerm.LoanNumber = dataRow["loan_number"].ToString();
                    loanTerm.StartDate = Convert.ToDateTime(dataRow["start_date"].ToString()).ToString("MM/dd/yyyy");
                    loanTerm.MaturityDate = Convert.ToDateTime(dataRow["maturity_date"].ToString()).ToString("MM/dd/yyyy");
                    loanTerm.LoanAmount = Convert.ToDecimal(dataRow["loan_amount"]);
                    loanTerm.AdvancePercentage = Convert.ToInt32(dataRow["advance"]);

                    if (dataRow["is_title_tracked"] != DBNull.Value)
                        loanTerm.TitleRequired = Convert.ToInt32(dataRow["is_title_tracked"]) == 1 ? "Yes" : "No";
                    else
                        loanTerm.TitleRequired = "No";

                    loanTerm.DocumentAcceptance = dataRow["receipt_required_method"] != DBNull.Value ? dataRow["receipt_required_method"].ToString() : "No";

                    loanTerms.Add(loanTerm);
                }
            }

            return loanTerms;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public List<RptFeeLoanTerm> RptLoanTermsFeeDetails(int loanId)
        {
            List<RptFeeLoanTerm> loanFees = new List<RptFeeLoanTerm>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });

            DataSet dataSet = dataHandler.GetDataSet("spGetFeesDetailForLoanTerms ", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    RptFeeLoanTerm loanFee = new RptFeeLoanTerm();

                    loanFee.FeeType = dataRow["FeeType"].ToString();
                    loanFee.FeeAmount = Convert.ToDecimal(dataRow["FeeAmount"]);

                    if (String.Equals(dataRow["payment_due_date"].ToString(), "VP", StringComparison.CurrentCultureIgnoreCase))
                        loanFee.DueDate = "Vehicle Payoff";
                    else if (String.Equals(dataRow["payment_due_date"].ToString(), "ToA", StringComparison.CurrentCultureIgnoreCase))
                    {
                        loanFee.DueDate = "Time of Advance";
                    }
                    else if (String.Equals(dataRow["payment_due_date"].ToString(), "EoM", StringComparison.CurrentCultureIgnoreCase))
                    {
                        loanFee.DueDate = "End of Month";
                    }
                    else
                    {
                        loanFee.DueDate = dataRow["payment_due_date"].ToString();
                    }

                    loanFees.Add(loanFee);
                }
            }

            return loanFees;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public List<RptEmailReminder> RptLoanTermsEmailReminders(int loanId)
        {
            List<RptEmailReminder> emailReminders = new List<RptEmailReminder>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]> { new object[] { "@loan_id", loanId } };

            DataSet dataSet = dataHandler.GetDataSet("spGetAutoReminderDetailForLoanTerms ", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    RptEmailReminder emailReminder = new RptEmailReminder
                    {
                        ReminderName = dataRow["ReminderType"].ToString(),
                        TimeFrame = Convert.ToInt32(dataRow["TimeFrame"]),
                        Email = dataRow["email"].ToString()
                    };


                    emailReminders.Add(emailReminder);
                }
            }

            return emailReminders;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public IList<UnitType> RptGetUnitTypes(int loanId)
        {
            IList<UnitType> unittypes = new List<UnitType>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]> { new object[] { "@loan_id", loanId } };

            DataSet dataSet = dataHandler.GetDataSet("spGetLoanUnitTypesByLoanId ", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    UnitType unitType = new UnitType();
                    unitType.unitTypeId = Convert.ToInt32((dataRow["unit_type_id"]));
                    unitType.unitTypeName = dataRow["unit_type_name"].ToString();

                    unittypes.Add(unitType);
                }
            }

            return unittypes;
        }

        #endregion

        #region Summary Report

        /// <summary>
        /// 
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<RptCompanySummary> RptGetCompanySummary(int companyId)
        {
            List<RptCompanySummary> companySummaryList = new List<RptCompanySummary>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]> { new object[] { "@company_id", companyId } };

            DataSet dataSet = dataHandler.GetDataSet("spGetCompanySummary ", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    RptCompanySummary companySummary = new RptCompanySummary();

                    companySummary.BranchName = dataRow["branchName"].ToString();
                    companySummary.NoOfPartnerBranches = Convert.ToInt32(dataRow["noOfPartnerBranch"]);
                    companySummary.NoOfActiveLoans = dataRow["NoOfActiveLoans"] != DBNull.Value ? Convert.ToInt32(dataRow["NoOfActiveLoans"]) : 0;
                    companySummary.TotalActiveUnits = dataRow["noOfActiveUnits"] != DBNull.Value ? Convert.ToInt32(dataRow["noOfActiveUnits"]) : 0;
                    companySummary.TotalLoanBalance = dataRow["totalLoanBalance"] != DBNull.Value ? Convert.ToDecimal(dataRow["totalLoanBalance"]) : 0;
                    companySummary.TotalLoanAmount = dataRow["totalLoanAmount"] != DBNull.Value ? Convert.ToDecimal(dataRow["totalLoanAmount"]) : 0;

                    companySummaryList.Add(companySummary);
                }
            }

            return companySummaryList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public List<RptBranchSummary> GetBranchSummarRptDetails(int branchId)
        {
            List<RptBranchSummary> branchLoans = new List<RptBranchSummary>();


            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]> { new object[] { "@branch_id", branchId } };

            DataSet dataSet = dataHandler.GetDataSet("spGetLoanDetailsByBranchId ", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                decimal totalAmount = 0.00M;
                decimal totalBalance = 0.00M;
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    RptBranchSummary loan = new RptBranchSummary();
                    if (string.IsNullOrEmpty(dataRow["loanId"].ToString()) && !string.IsNullOrEmpty(dataRow["iloanId"].ToString()))
                    {
                        loan.LoanNumber = dataRow["iloanNumber"].ToString();
                        loan.PartnerBranch = dataRow["inonRegBranchName"].ToString();
                        loan.ActiveUnits = 0;
                        loan.InActiveUnits = int.Parse(dataRow["inactiveUnits"].ToString());
                        loan.LoanBalance = decimal.Parse(dataRow["iusedAmount"].ToString());
                        loan.PendingBalance = decimal.Parse(dataRow["ipendingAmount"].ToString());
                        loan.LoanAmount = decimal.Parse(dataRow["iloanAmount"].ToString());
                        totalAmount = totalAmount + loan.LoanAmount;
                        totalBalance = totalBalance + loan.LoanBalance;
                        branchLoans.Add(loan);
                    }
                    else if (!string.IsNullOrEmpty(dataRow["loanId"].ToString()) && string.IsNullOrEmpty(dataRow["iloanId"].ToString()))
                    {
                        loan.LoanNumber = dataRow["loanNumber"].ToString();
                        loan.PartnerBranch = dataRow["nonRegBranchName"].ToString();
                        loan.ActiveUnits = int.Parse(dataRow["activeUnits"].ToString());
                        loan.InActiveUnits = 0;
                        loan.LoanBalance = decimal.Parse(dataRow["usedAmount"].ToString());
                        loan.PendingBalance = decimal.Parse(dataRow["pendingAmount"].ToString());
                        loan.LoanAmount = decimal.Parse(dataRow["loanAmount"].ToString());
                        totalAmount = totalAmount + loan.LoanAmount;
                        totalBalance = totalBalance + loan.LoanBalance;
                        branchLoans.Add(loan);
                    }
                    else if (!string.IsNullOrEmpty(dataRow["loanId"].ToString()) && !string.IsNullOrEmpty(dataRow["iloanId"].ToString()))
                    {
                        loan.LoanNumber = dataRow["loanNumber"].ToString();
                        loan.PartnerBranch = dataRow["nonRegBranchName"].ToString();
                        loan.ActiveUnits = int.Parse(dataRow["activeUnits"].ToString());
                        loan.InActiveUnits = int.Parse(dataRow["inactiveUnits"].ToString());
                        loan.LoanBalance = decimal.Parse(dataRow["usedAmount"].ToString());
                        loan.PendingBalance = decimal.Parse(dataRow["pendingAmount"].ToString());
                        loan.LoanAmount = decimal.Parse(dataRow["loanAmount"].ToString());
                        totalAmount = totalAmount + loan.LoanAmount;
                        totalBalance = totalBalance + loan.LoanBalance;
                        branchLoans.Add(loan);
                    }

                }
                if (branchLoans.Count > 0)
                {
                    branchLoans[0].TotalLoanAmounts = totalAmount;
                    branchLoans[0].TotalLoanBalances = totalBalance;
                }
            }

            return branchLoans;
        }

        /// <summary>
        /// Created By: Kasun Samarawickrama
        /// Loan Summary report - for a loan need to retrive all informations about it
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="dueDateStart">start date</param>
        /// <param name="dueDateEnd">end date</param>
        /// <returns></returns>
        public List<RptLoanSummary> GetLoanSummaryReport(int loanId, DateTime dueDateStart, DateTime dueDateEnd)
        {

            List<RptLoanSummary> loanSummaryList = new List<RptLoanSummary>();
            DataHandler dataHandler = new DataHandler();

            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@due_date_start", dueDateStart });
            paramertList.Add(new object[] { "@due_date_end", dueDateEnd });

            DataSet dataSet = dataHandler.GetDataSet("spGetLoanSummaryReport", paramertList);
            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    RptLoanSummary loanSummary = new RptLoanSummary();

                    loanSummary.LoanAmount = dataRow["LoanAmount"] != DBNull.Value ? Convert.ToDecimal(dataRow["LoanAmount"].ToString()) : 0.00M;
                    loanSummary.TotalUnitsAdded = int.Parse(dataRow["TotalUnitsAdded"].ToString());
                    loanSummary.TotalUnitsAdvanced = int.Parse(dataRow["TotalUnitsAdvanced"].ToString());
                    loanSummary.TotalAmountAdvanced = dataRow["TotalAmountAdvanced"] != DBNull.Value ? Convert.ToDecimal(dataRow["TotalAmountAdvanced"].ToString()) : 0.00M;
                    loanSummary.TotalAdvanceFees = dataRow["TotalAdvanceFees"] != DBNull.Value ? decimal.Parse(dataRow["TotalAdvanceFees"].ToString()) : 0.00M;
                    loanSummary.TotalCurtailmentsRecieved =
                        dataRow["TotalCurtailmentsRecieved"] != DBNull.Value ? decimal.Parse(dataRow["TotalCurtailmentsRecieved"].ToString()) : 0.00M;
                    loanSummary.TotalUnitsPaidOff = dataRow["TotalUnitsPaidOff"] != DBNull.Value ? int.Parse(dataRow["TotalUnitsPaidOff"].ToString()) : 0;
                    loanSummary.TotalAmountPaidOff = dataRow["TotalAmountPaidOff"] != DBNull.Value ? decimal.Parse(dataRow["TotalAmountPaidOff"].ToString()) : 0.00M;
                    loanSummary.TotalUnitsDeleted = dataRow["TotalUnitsDeleted"] != DBNull.Value
                        ? int.Parse(dataRow["TotalUnitsDeleted"].ToString())
                        : 0;

                    loanSummaryList.Add(loanSummary);
                }
            }

            return loanSummaryList;
        }

        #endregion

        public static string SubStrDeletion = "Delete reason";
        public List<RptDeletedUnit> RptGetDeletedUnitByLoanIdDateRange(int loanId, DateTime startDate, DateTime endDate)
        {
            List<RptDeletedUnit> deletedUnits = new List<RptDeletedUnit>();

            DataHandler dataHandler = new DataHandler();
            List<object[]> paramertList = new List<object[]>();
            paramertList.Add(new object[] { "@loan_id", loanId });
            paramertList.Add(new object[] { "@date_start", startDate });
            paramertList.Add(new object[] { "@date_end", endDate });

            DataSet dataSet = dataHandler.GetDataSet("spGetDeletedUnitDetails ", paramertList);

            if (dataSet != null && dataSet.Tables.Count != 0)
            {
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    RptDeletedUnit unit = new RptDeletedUnit();

                    unit.LoanId = Convert.ToInt32(dataRow["loan_id"]);
                    unit.UnitId = dataRow["unit_id"].ToString();
                    unit.IdentificationNumber = dataRow["identification_number"].ToString();
                    unit.Year = (dataRow["year"]) != DBNull.Value ? (int)dataRow["year"] : 0000;
                    unit.Make = dataRow["make"].ToString();
                    unit.Model = dataRow["model"].ToString();
                    unit.AdvanceDate = !dataRow.IsNull("advance_date") ? Convert.ToDateTime(dataRow["advance_date"].ToString()).ToString("MM/dd/yyyy") : "";
                    unit.PurchasePrice = Convert.ToDecimal(dataRow["cost"]);
                    unit.AdvanceAmount = Convert.ToDecimal(dataRow["advance_amount"]);
                    unit.TotalCurtPaid = !dataRow.IsNull("CurtailmentPaid") ? Convert.ToDecimal(dataRow["CurtailmentPaid"]) : 0.00M;
                    unit.BalanceDue = !dataRow.IsNull("BalanceDue") ? Convert.ToDecimal(dataRow["BalanceDue"]) : 0.00M;

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

                    unit.DeletedDate = !dataRow.IsNull("deletedDate") ? Convert.ToDateTime(dataRow["deletedDate"].ToString()).ToString("MM/dd/yyyy") : "";
                    string note = dataRow["Note"].ToString();
                    string[] words = note.Split(';');
                    string getFirst = words[0];
                    unit.ReasonForDeletion = "";
                    if (getFirst.Contains(SubStrDeletion))
                    {
                        string[] reason = getFirst.Split(':');
                        if (!string.IsNullOrEmpty(reason[1]))
                            unit.ReasonForDeletion = reason[1];
                    }

                    deletedUnits.Add(unit);
                }
            }

            return deletedUnits;
        }
    }
}