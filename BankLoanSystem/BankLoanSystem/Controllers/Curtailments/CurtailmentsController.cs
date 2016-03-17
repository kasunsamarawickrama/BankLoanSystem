using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.Curtailments
{
    public class CurtailmentsController : Controller
    {
        // GET: Curtailments
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult setLoanCode(string loanCode)
        {
            Session["loanCode"] = loanCode;

            return RedirectToAction("PayCurtailments");
        }

        // GET: Curtailments
        public ActionResult PayCurtailments()
        {
            return View();
        }
    }
}