using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using BankLoanSystem.Reports;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Controllers.Reports
{
    public class ReportsController : Controller
    {
        User _userData = new User();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (Session["AuthenticatedUser"] != null)
                {
                    _userData = ((User)Session["AuthenticatedUser"]);
                }
                else
                {
                    if (HttpContext.Request.IsAjaxRequest())
                    {

                        //new HttpStatusCodeResult(404, "Failed to Setup company.");
                        filterContext.Result = new HttpStatusCodeResult(404, "Due to inactivity your session has timed out, please log in again.");
                    }
                    else
                    {

                        filterContext.Result = new RedirectResult("/Login/UserLogin?lbl=Due to inactivity your session has timed out, please log in again.");
                    }
                }
            }
            catch
            {
                filterContext.Result = new RedirectResult("~/Exceptions/Index");
            }
        }

        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Reporting page
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportIndex()
        {
            ReportAccess ra = new ReportAccess();
            List<Account> loanNumbers = new List<Account>(); 

            List<Account> userLoanNumbers = new List<Account>();

           
            ViewBag.RoleId = _userData.RoleId;
            ViewBag.BranchId = _userData.BranchId;
            if (_userData.RoleId == 1)
            {
                loanNumbers = ra.GetAccountDetails(_userData.Company_Id, _userData.RoleId);
                userLoanNumbers = loanNumbers;
                ViewBag.ComId = _userData.Company_Id;
            }
            else if (_userData.RoleId == 2 || _userData.RoleId == 3)
            {
                loanNumbers = ra.GetAccountDetails(_userData.BranchId, _userData.RoleId);
                userLoanNumbers = loanNumbers;

            }
            if (_userData.RoleId == 3 || _userData.RoleId == 4)
            {
                if (Session["CurrentLoanRights"] == null || Session["CurrentLoanRights"].ToString() == "")
                {
                    return RedirectToAction("UserDetails", "UserManagement");
                }
                else {
                    var checkPermission = false;
                    string rgts = "";
                    rgts = (string)Session["CurrentLoanRights"];
                    string[] rgtList = null;
                    if (rgts != "")
                    {
                        rgtList = rgts.Split(',');
                    }
                    if (rgtList != null)
                    {
                        foreach (var x in rgtList)
                        {
                            if (x == "U006")
                            {
                                checkPermission = true;
                            }
                        }
                        if (checkPermission == false)
                        {
                            return RedirectToAction("UserDetails", "UserManagement");
                        }
                        else if (Session["oneLoanDashboard"] != null)
                        {
                            Loan loan = (Loan) Session["oneLoanDashboard"];
                            userLoanNumbers = new List<Account>();
                            Account uLoans = new Account();
                            uLoans.LoanId = loan.LoanId;
                            uLoans.LoanNumber = loan.LoanNumber;
                            uLoans.BranchId = loan.BranchId;
                            userLoanNumbers.Add(uLoans);
                        }
                        else if (checkPermission && Session["oneLoanDashboard"] == null)
                        {
                            List<UserRights> userLoanRights = ra.GeUserRightsForReporting(_userData.UserId);
                            List<Account> loanNumbersUsers = new List<Account>();
                            foreach (var item in userLoanRights)
                            {
                                string[] tokens = null;
                                string loanRights = item.PermissionList;
                                if (loanRights != "") tokens = loanRights.Split(',');
                                if (tokens != null)
                                {
                                    foreach (var x in tokens)
                                    {
                                        if (x == "U006")
                                        {
                                            loanNumbersUsers.AddRange(loanNumbers.Where(loans => item.LoanId == loans.LoanId));
                                        }
                                    }
                                }
                            }
                            userLoanNumbers = loanNumbersUsers;

                        }
                    }
                    else {
                        return RedirectToAction("UserDetails", "UserManagement");
                    }

                }
            }
            ViewBag.LoanId = new SelectList(userLoanNumbers, "LoanId", "LoanNumberB");

            return View();
        }


        /*

        Frontend page: Report page
        Title: Get active loans of user for grid
        Designed: Irfan MAM
        User story: DFP- 446
        Developed: Irfan MAM
        Date created: 06/23/2016

*/

            public JsonResult GetActiveLoans(string sidx, string sord, int page, int rows, bool _search)
        {
            ReportAccess ra = new ReportAccess();

            List<Account> loanNumbers;

            // if user is a superadmin, get account details of his company
            if (_userData.RoleId == 1) {
                 loanNumbers = ra.GetAccountDetails(_userData.Company_Id, _userData.RoleId);

            }

            // if user is a admin, get account details of his branch
            else if (_userData.RoleId == 2)
            {
                loanNumbers = ra.GetAccountDetails(_userData.BranchId, _userData.RoleId);
            }
            // if user is a user deler user, get account details of his assigned loans
            else
            {
                // should change
                loanNumbers = ra.GetAccountDetails(_userData.BranchId, _userData.RoleId);
            }

            // these varibles are for JqGrid purpose

            var count = loanNumbers.Count(); // number of rows
            int pageIndex = page; // number of pages on the grid
            int pageSize = rows; // maximum page sige
            int startRow = (pageIndex * pageSize) + 1;
            int totalRecords = count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            // json object for jqGrid
            var result = new
            {
                total = totalPages,
                page = pageIndex,
                records = count,
                rows = loanNumbers.Select(x => new
                {
                    x.LoanId,
                    x.BranchName,
                    x.PatBranchName,
                    x.LoanNumber,
                   
                    x.LoanAmount,
                   
                   
                    x.UsedAmount,
                    x.ActiveUnits,
                    x.PatBranchAddress1,
                    x.PatBranchAddress2,
                    x.PatCity
                }
                                         ).ToArray().Select(x => new
                                         {
                                             id = x.LoanId.ToString(),
                                             cell = new string[] { 
                                                        x.BranchName,
                                                        x.PatBranchName,
                                                        x.LoanNumber.ToString(),
                                                        x.LoanAmount.ToString(),
                                                        x.UsedAmount.ToString(),
                                                        x.ActiveUnits.ToString(),
                                                        x.PatBranchAddress1,
                                                        x.PatBranchAddress2,
                                                        x.PatCity
                                                      }
                                         }
                      ).ToArray()

               

            };
            
            // returning json object
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        // <summary>
        // Print reports
        // </summary>
        // <param name="rptType"></param>
        // <param name="loanId"></param>
        // <param name="range1"></param>
        // <param name="range2"></param>
        // <param name="titleStatus"></param>
        //[HttpPost]
        //public int PrintPage(string rptType, int loanId, string range1, string range2, string titleStatus)

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public FileResult PrintPage()
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            ReportViewer rptViewerPrint = new ReportViewer();

            string rptType = "";
            int loanId = 0;
            string range1 = "";
            string range2 = "";
            int titleStatus = 0;

            if (Request.QueryString["rptType"] != "")
                rptType = Request.QueryString["rptType"];

            if (Request.QueryString["loanId"] != "")
                loanId = Convert.ToInt32(Request.QueryString["loanId"]);

            if(Request.QueryString["range1"] != "")
                range1 = Request.QueryString["range1"];

            if (Request.QueryString["range2"] != "")
                range2 = Request.QueryString["range2"];

            if(Request.QueryString["titleStatus"] != "")
                titleStatus = Convert.ToInt32(Request.QueryString["titleStatus"]);

            if (rptType == "LotInspection")
            {
                RptDivLotInspection lInspection = new RptDivLotInspection();
                rptViewerPrint = lInspection.PrintPage(loanId);
            }
            else if (rptType == "CurtailmentInvoice")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivCurtailmentInvoice ciInvoice = new RptDivCurtailmentInvoice();
                rptViewerPrint = ciInvoice.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "AdvanceFeeInvoice")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivAdvanceFeeInvoice afInvoice = new RptDivAdvanceFeeInvoice();
                rptViewerPrint = afInvoice.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "LoanFeeInvoice")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivMonthlyLoanFeeInvoice lfInvoice = new RptDivMonthlyLoanFeeInvoice();
                rptViewerPrint = lfInvoice.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "LotInspectionFeeInvoice")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivLotInspectionFeeInvoice liInvoice = new RptDivLotInspectionFeeInvoice();
                rptViewerPrint = liInvoice.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "PayOff")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptPayOff payOff = new RptPayOff();
                rptViewerPrint = payOff.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "AdvanceFeeReceipt")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivAdvanceFeeReceipt adFeeReceipt = new RptDivAdvanceFeeReceipt();
                rptViewerPrint = adFeeReceipt.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "MonthlyLoanFeeReceipt")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivMonthlyLoanFeeReceipt monthlyLoanFeeReceipt = new RptDivMonthlyLoanFeeReceipt();
                rptViewerPrint = monthlyLoanFeeReceipt.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "LotInspectionFeeReceipt")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivLotInspectionFeeReceipt lotInspectionFeeReceipt = new RptDivLotInspectionFeeReceipt();
                rptViewerPrint = lotInspectionFeeReceipt.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "CurtailmentReceipt")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivCurtailmentReceipt crReceipt = new RptDivCurtailmentReceipt();
                rptViewerPrint = crReceipt.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "TransactionHistory")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivTransactionHistory loanSmmry = new RptDivTransactionHistory();
                rptViewerPrint = loanSmmry.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "TitlesStatus")
            {
                RptDivTitleStatus tsStatus = new RptDivTitleStatus();
                rptViewerPrint = tsStatus.PrintPage(loanId, titleStatus);
            }
            else if (rptType == "FullInventory")
            {
                RptDivFullInventory fInventory = new RptDivFullInventory();
                rptViewerPrint = fInventory.PrintPage(loanId);
            }
            else if (rptType == "AdvanceUnit")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptAdvanceUnitReport advanceUnit = new RptAdvanceUnitReport();
                rptViewerPrint = advanceUnit.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "LoanTerms")
            {
                RptDivLoanTerms loanTerms = new RptDivLoanTerms();
                rptViewerPrint = loanTerms.PrintPage(loanId);
            }
            else if(rptType == "CompanySummary")
            {
                
            }
            else if (rptType == "BranchSummary")
            {

            }
            else if (rptType == "DeletedUnits")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivDeletedUnits deleteUnits = new RptDivDeletedUnits();
                rptViewerPrint = deleteUnits.PrintPage(loanId, startDate, endtDate);
            }
            else
            {
                return null;
            }

            var bytes = rptViewerPrint.LocalReport.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);

            var fsResult = File(bytes, "application/pdf");
            return fsResult;
        }

    }
}