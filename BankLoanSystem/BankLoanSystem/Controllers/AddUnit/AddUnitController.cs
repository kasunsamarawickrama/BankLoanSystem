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
            int loanId = 182;

            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanStepOne(loanId);

            List<SelectListItem> ListOfUnitTypes = new List<SelectListItem>();

            int id = 0;
            string name = "";

            foreach (var type in loanDetails.selectedUnitTypes) {

                id = type.unitTypeId;
                name = type.unitTypeName;

                ListOfUnitTypes.Add(new SelectListItem
                {
                    Value = id.ToString(),
                    Text = name
                });
            }
            ViewBag.ListOfUnitTypes = ListOfUnitTypes;

            return View();
        }
    }
}