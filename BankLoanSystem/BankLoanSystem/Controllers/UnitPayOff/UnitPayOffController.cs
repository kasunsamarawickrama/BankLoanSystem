using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.UnitPayOff
{
    public class UnitPayOffController : Controller
    {
        // GET: UnitPayOff
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult setLoanCode(string loanCode)
        {
            Session["loanCode"] = loanCode;

            return RedirectToAction("PayOff");
        }

        public ActionResult PayOff()
        {
            return View();
        }
    }
}