using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BankLoanSystem.Controllers
{
    public class AdvanceUnitController : Controller
    {
        // GET: AdvanceUnit
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Advance()
        {
            Session["userId"] = 2;
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");
            int userId = Convert.ToInt32(Session["userId"]);

            UnitAccess unitAccess = new UnitAccess();

            List <BankLoanSystem.Models.Unit> unitList = unitAccess.GetNotAdvancedUnitDetailsByLoanId(187);
            //unitList.Add(new Unit
            //{
            //    UnitId = 1,
            //    CreatedDate = DateTime.Now,
            //    IdentificationNumber = "s1"

            //});
            //unitList.Add(new Unit
            //{
            //    UnitId = 1,
            //    CreatedDate = DateTime.Now,
            //    IdentificationNumber = "s2"
            //});

            return View(unitList);
        }

        [HttpPost]
        public ActionResult Advance(string[] ids)
        {
            Session["userId"] = 2;
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");
            int userId = Convert.ToInt32(Session["userId"]);

            UnitAccess unitAccess = new UnitAccess();

            List<BankLoanSystem.Models.Unit> unitList = unitAccess.GetNotAdvancedUnitDetailsByLoanId(187);

            // In the real application you can ids 

            if (ids != null)
            {
                ViewBag.Message = "You have selected following Customer ID(s):" + string.Join(", ", ids);
            }
            else
            {
                ViewBag.Message = "No record selected";
            }

            return View(unitList);
        }
    }
}