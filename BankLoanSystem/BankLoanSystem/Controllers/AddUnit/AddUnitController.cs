using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

namespace BankLoanSystem.Controllers.AddUnit
{
    public class AddUnitController : Controller
    {
        // GET: AddUnit
        public ActionResult AddUnit()
        {
            Session["userId"] = 2;
            int loanId = 187;

            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanStepOne(loanId);

            ViewBag.loanDetails = loanDetails;



            return View();
        }
    }
}