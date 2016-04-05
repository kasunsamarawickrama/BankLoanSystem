using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

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

            ViewBag.LoanId = new SelectList(userLoanNumbers, "LoanId", "LoanNumberB");

            return View();
        }

    }
}