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

        /*

            Frontend page: Report page
            Title: Get active loans of user for grid
            Designed: Kanishka SHM
            User story: 
            Developed: Kanishka SHM
            Date created: 

            Edited By: Irfan MAM
            Purpose: Grid Functionality and user right access

        */
        public ActionResult ReportIndex()
        {
            DashBoardAccess da = new DashBoardAccess();
           int loanCount = 0; 


            ViewBag.RoleId = _userData.RoleId; //user role
            ViewBag.BranchId = _userData.BranchId; // branch

            // if user is a super admin
            if (_userData.RoleId == 1)
            {
                // get total number of active loans which belong to his company
                loanCount = da.GetLoanCount(_userData.Company_Id, _userData.RoleId);
                
                ViewBag.ComId = _userData.Company_Id;

                // if there is no active loan then redirect to login -- wrong access
                if (loanCount < 1)
                {
                    return RedirectToAction("UserLogin", "Login");
            }
            }
            // if user is a admin
            else if (_userData.RoleId == 2)
            {
                // get total number of active loans which belong to his branch
                loanCount = da.GetLoanCount(_userData.BranchId, _userData.RoleId);

                // if there is no active loan then redirect to login -- wrong access
                if (loanCount < 1)
                {
                    return RedirectToAction("UserLogin", "Login");
                }

            }
            // if user is a user
            else if (_userData.RoleId == 3)
            {
                // get total number of autorized loans which belong to him
                loanCount = da.GetLoanCount(_userData.UserId, _userData.RoleId);

                // if there is no authorized loan then redirect to login -- wrong access
                if (loanCount < 1)
                {
                    return RedirectToAction("UserLogin", "Login");
                }
                //  if user selected the authorized loan from dashboard
                else if(Session["CurrentLoanRights"] != null && Session["CurrentLoanRights"].ToString().Contains("U06"))
                {


                    // pass the user rights to view
            }
                // if user selected the non authorized loan from dashboard
                else if (Session["CurrentLoanRights"] != null && !Session["CurrentLoanRights"].ToString().Contains("U06"))
            {


                    // clear the session selected loan


                    if(loanCount== 1) {
                        // get that loan detail and report rights
                    }
                }


                //if (Session["CurrentLoanRights"] == null || Session["CurrentLoanRights"].ToString() == "")
                //{
                //    return RedirectToAction("UserDetails", "UserManagement");
                //}
                //else {
                //    bool checkPermission = false;
                //    string rgts = "";
                //    rgts = (string)Session["CurrentLoanRights"];
                //    string[] rgtList = null;
                //    if (rgts != "")
                //    {
                //        rgtList = rgts.Split(',');
                //    }
                //    if (rgtList != null)
                //    {
                //        foreach (string x in rgtList)
                //        {
                //            if (x == "U06")
                //            {
                //                checkPermission = true;
                //            }
                //        }
                //        if (checkPermission == false)
                //        {
                //            return RedirectToAction("UserDetails", "UserManagement");
                //        }
                //        else if (Session["oneLoanDashboard"] != null)
                //        {
                //            Loan loan = (Loan) Session["oneLoanDashboard"];
                //            userLoanNumbers = new List<Account>();
                //            Account uLoans = new Account();
                //            uLoans.LoanId = loan.LoanId;
                //            uLoans.LoanNumber = loan.LoanNumber;
                //            uLoans.BranchId = loan.BranchId;
                //            userLoanNumbers.Add(uLoans);
                //        }
                //        else if (checkPermission && Session["oneLoanDashboard"] == null)
                //        {
                //            List<UserRights> userLoanRights = ra.GeUserRightsForReporting(_userData.UserId);
                //            List<Account> loanNumbersUsers = new List<Account>();
                //            foreach (UserRights item in userLoanRights)
                //            {
                //                string[] tokens = null;
                //                string loanRights = item.PermissionList;
                //                if (loanRights != "") tokens = loanRights.Split(',');
                //                if (tokens != null)
                //                {
                //                    foreach (string x in tokens)
                //                    {
                //                        if (x == "U06")
                //                        {
                //                            loanNumbersUsers.AddRange(loanNumbers.Where(loans => item.LoanId == loans.LoanId));
                //                        }
                //                    }
                //                }
                //            }
                //            userLoanNumbers = loanNumbersUsers;

                //        }
                //    }
                //    else {
                //        return RedirectToAction("UserDetails", "UserManagement");
                //    }

                //}
            }

            else if ( _userData.RoleId == 4)
            {
                ViewBag.loanCount = 1;

            }

                //ViewBag.LoanId = new SelectList(userLoanNumbers, "LoanId", "LoanNumberB");
                ViewBag.loanCount = loanCount;
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

            int count = loanNumbers.Count(); // number of rows
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

        /*

            Frontend page: Report page
            Title: Loan pdf viwer
            Designed: Kanishka SHM
            User story: 
            Developed: Kanishka SHM
            Date created: 
            Modified: Kanishka SHM
            Date Modified: 06/28/2016
        */
        public FileResult PrintPage()
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            //to store report viwer as a pdf file
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            ReportViewer rptViewerPrint;

            string rptType = "";
            int loanId = 0;
            string range1 = "";
            string range2 = "";
            int titleStatus = 0;

            // get report type
            if (Request.QueryString["rptType"] != "")
                rptType = Request.QueryString["rptType"];

            // get loan id
            if (Request.QueryString["loanId"] != "")
                loanId = Convert.ToInt32(Request.QueryString["loanId"]);

            // stat date of range
            if(Request.QueryString["range1"] != "")
                range1 = Request.QueryString["range1"];

            // end date of range
            if (Request.QueryString["range2"] != "")
                range2 = Request.QueryString["range2"];

            // title status
            if(Request.QueryString["titleStatus"] != "")
                titleStatus = Convert.ToInt32(Request.QueryString["titleStatus"]);

            // call pdf viwer by report type
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
                RptDivCompanySummary companySummary = new RptDivCompanySummary();
                rptViewerPrint = companySummary.PrintPage(userData.Company_Id);
            }
            else if (rptType == "BranchSummary")
            {
                RptDivBranchSummary branchSummary = new RptDivBranchSummary();
                rptViewerPrint = branchSummary.PrintPage(userData.BranchId);
            }
            else if (rptType == "DeletedUnits")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivDeletedUnits deleteUnits = new RptDivDeletedUnits();
                rptViewerPrint = deleteUnits.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "LoanSummary")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivLoanSummary deleteUnits = new RptDivLoanSummary();
                rptViewerPrint = deleteUnits.PrintPage(loanId, startDate, endtDate);
            }
            else
            {
                return null;
            }

            // store report viewer as pdf format
            byte[] bytes = rptViewerPrint.LocalReport.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);

            // return pdf file
            FileContentResult fsResult = File(bytes, "application/pdf");
            return fsResult;
        }

    }
}