using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using BankLoanSystem.Reports;

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
                        filterContext.Result = new HttpStatusCodeResult(404, "Session Expired");
                    }
                    else
                    {

                        filterContext.Result = new RedirectResult("~/Login/UserLogin");
                    }
                }
            }
            catch
            {
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
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
            List<LoanIdNumber> loanNumbers = ra.GetLoanNumbersWithBranch(_userData.Company_Id);

            List<LoanIdNumber> userLoanNumbers = new List<LoanIdNumber>();

            if (_userData.RoleId == 1)
            {
                userLoanNumbers = loanNumbers;
            }
            else if (_userData.RoleId == 2 || _userData.RoleId == 3)
            {
                //foreach (var loans in loanNumbers)
                //{
                //    if (_userData.BranchId == loans.BranchId)
                //        userLoanNumbers.Add(loans);
                //}
                userLoanNumbers.AddRange(loanNumbers.Where(loans => _userData.BranchId == loans.BranchId));
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
                            userLoanNumbers = new List<LoanIdNumber>();
                            LoanIdNumber uLoans = new LoanIdNumber();
                            uLoans.LoanId = loan.LoanId;
                            uLoans.LoanNumberB = loan.LoanNumber;
                            uLoans.BranchId = loan.BranchId;
                            userLoanNumbers.Add(uLoans);
                        }
                        else if (checkPermission && Session["oneLoanDashboard"] == null)
                        {
                            List<UserRights> userLoanRights = ra.GeUserRightsForReporting(_userData.UserId);
                            List<LoanIdNumber> loanNumbersUsers = new List<LoanIdNumber>();
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

        /// <summary>
        /// Print reports
        /// </summary>
        /// <param name="rptType"></param>
        /// <param name="loanId"></param>
        /// <param name="range1"></param>
        /// <param name="range2"></param>
        /// <param name="titleStatus"></param>
        [HttpPost]
        public int PrintPage(string rptType, int loanId, string range1, string range2, string titleStatus)
        {
            if (rptType == "LotInspection")
            {
                RptDivLotInspection lInspection = new RptDivLotInspection();
                return lInspection.PrintPage(loanId);
            }
            else if (rptType == "CurtailmentInvoice")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivCurtailmentInvoice ciInvoice = new RptDivCurtailmentInvoice();
                return ciInvoice.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "AdvanceFeeInvoice")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

               RptDivAdvanceFeeInvoice afInvoice = new RptDivAdvanceFeeInvoice();
                return afInvoice.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "LoanFeeInvoice")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivMonthlyLoanFeeInvoice lfInvoice = new RptDivMonthlyLoanFeeInvoice();
                return lfInvoice.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "LotInspectionFeeInvoice")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivLotInspectionFeeInvoice liInvoice = new RptDivLotInspectionFeeInvoice();
                return liInvoice.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "PayOff")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptPayOff payOff = new RptPayOff();
                return payOff.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "AdvanceFeeReceipt")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivAdvanceFeeInvoice ciInvoice = new RptDivAdvanceFeeInvoice();
                return ciInvoice.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "MonthlyLoanFeeReceipt")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivMonthlyLoanFeeInvoice ciInvoice = new RptDivMonthlyLoanFeeInvoice();
                return ciInvoice.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "LotInspectionFeeReceipt")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivLotInspectionFeeInvoice ciInvoice = new RptDivLotInspectionFeeInvoice();
                return ciInvoice.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "CurtailmentReceipt")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivCurtailmentReceipt crReceipt = new RptDivCurtailmentReceipt();
                return crReceipt.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "LoanSummary")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivLoanSummary loanSmmry = new RptDivLoanSummary();
                return loanSmmry.PrintPage(loanId, startDate, endtDate);
            }
            else if (rptType == "TitlesStatus")
            {
                RptDivTitleStatus tsStatus = new RptDivTitleStatus();
                return tsStatus.PrintPage(loanId, 0);
            }
            else if (rptType == "FullInventory")
            {
                RptDivFullInventory fInventory = new RptDivFullInventory();
                return fInventory.PrintPage(loanId);
            }
           
            return -1;
        }

    }
}