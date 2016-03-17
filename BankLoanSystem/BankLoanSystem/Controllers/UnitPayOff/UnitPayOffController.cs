using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankLoanSystem.Models;
using BankLoanSystem.DAL;

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
            UnitPayOffViewModel unitPayOffViewModel = new UnitPayOffViewModel();

            CurtailmentAccess payoff = new CurtailmentAccess();

            unitPayOffViewModel.UnitPayOffList = payoff.GetUnitPayOffList(190);

            return View(unitPayOffViewModel);
        }

        [HttpPost]
        public ActionResult PayOff(UnitPayOffViewModel resModel)
        {
            return View();
        }
    }
}