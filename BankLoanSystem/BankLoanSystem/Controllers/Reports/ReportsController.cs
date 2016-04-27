using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankLoanSystem.Code;
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
                    filterContext.Result = new RedirectResult("~/Login/UserLogin");
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
                foreach (var loans in loanNumbers)
                {
                    if(_userData.BranchId == loans.BranchId)
                        userLoanNumbers.Add(loans);
                }
            }
            if (_userData.RoleId == 3)
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
            else if (rptType == "CurtailmentReceipt")
            {
                DateTime startDate = Convert.ToDateTime(range1);
                DateTime endtDate = Convert.ToDateTime(range2);

                RptDivCurtailmentReceipt crReceipt = new RptDivCurtailmentReceipt();
                return crReceipt.PrintPage(loanId, startDate, endtDate);
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