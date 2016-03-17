using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankLoanSystem.Models;

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

            UnitPayOffModel upom = new Models.UnitPayOffModel()
            {
                LoanId = 1,
                UnitId = "ooo",
                Balance = 12.05M,
                PurchesePrice = 15.09M,
                Year = 2012,
                Make = "Lamboghini",
                Model = "Veneno"
            };

            unitPayOffViewModel.UnitPayOffList = new List<UnitPayOffModel> {upom};

            return View(unitPayOffViewModel);
        }

        [HttpPost]
        public ActionResult PayOff(UnitPayOffViewModel resModel)
        {
            return View();
        }
    }
}