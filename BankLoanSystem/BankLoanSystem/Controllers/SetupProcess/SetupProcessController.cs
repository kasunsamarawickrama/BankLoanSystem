using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.SetupProcess
{
    public class SetupProcessController : Controller
    {
        // GET: SetupProcess
        public ActionResult Step1()
        {
            return View();
        }
    }
}