using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.AddUnit
{
    public class AddUnitController : Controller
    {
        // GET: AddUnit
        public ActionResult AddUnit()
        {
            Session["userId"] = 2;

            ViewBag.ListOfUnitTypes = new SelectList(new[]
            {
            new { Value = "1", Text = "Vehicle" },
            new { Value = "2", Text = "RV" },
            new { Value = "3", Text = "Camper" },
            new { Value = "4", Text = "ATV" },
            new { Value = "5", Text = "Boat" },
            new { Value = "6", Text = "Motorcycle" },
            new { Value = "7", Text = "Snowmobile" },
            new { Value = "8", Text = "Heavy Equipment" },
            }, "Value", "Text");

            return View();
        }
    }
}