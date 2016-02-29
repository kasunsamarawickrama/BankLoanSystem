using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BankLoanSystem.Controllers
{
    /// <summary>
    /// Get not advanced unit list and display in table
    /// can select one or more units and do the advance
    /// can search unit by vin/year/make or model
    /// </summary>
    public class AdvanceUnitController : Controller
    {
        private static LoanSetupStep1 loan;

        public ActionResult setLoanCode(string loancode)
        {
            Session["loanCode"] = loancode;

            return RedirectToAction("Advance");
        }

        // GET: AdvanceUnit
        public ActionResult Index()
        {
            return View();

        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 02/24/2016
        /// 
        /// Get loan details and not advanced unit details from database and return not advance unit list to view
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return partial view</returns>
        public ActionResult Advance(int? flag)
        {
            Session["userId"] = 2;
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");
            int userId = Convert.ToInt32(Session["userId"]);

            Session["userId"] = 2;
            string loanCode = Session["loanCode"].ToString();


            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);
            

            ViewBag.loanDetails = loanDetails;
            Models.Unit unit = new Models.Unit();
            List<Models.Unit> advanceUnit = this.GetAdvanceUnitList(loanDetails.loanId).NotAdvanced;
            Session["notAdvancedList"] = advanceUnit;

            if (flag > 0)
            {
                ViewBag.Msg = "Success";
                return View(advanceUnit);
            }
            else
            {
                ViewBag.Msg = "Error";
                return View(advanceUnit);
            }

        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/27
        /// Search not advanced units
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="vehicleModel"></param>
        /// <returns></returns>
        public ActionResult SearchUnit(string identificationNumber, string year, string make, string vehicleModel)
        {
            int loanId = 187;
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanStepOne(loanId);

            ViewBag.loanDetails = loanDetails;
            List<Models.Unit> unitList = (List<Models.Unit>)Session["notAdvancedList"];

            Models.AdvanceUnit unitListMain = new Models.AdvanceUnit();
            //unitListMain.NotAdvanced = unitList;
            unitListMain.NotAdvanced = new List<Models.Unit>();
            if (((!string.IsNullOrEmpty(identificationNumber)) || (!string.IsNullOrEmpty(year)) || (!string.IsNullOrEmpty(make)) || (!string.IsNullOrEmpty(vehicleModel))))
            {
                //search through list elements
                Search sc = new Search();

                unitListMain.Search = sc.GetSearchResultsList(unitList, identificationNumber.Trim().ToLower(), year.Trim().ToLower(), make.Trim().ToLower(), vehicleModel.Trim().ToLower());

                return PartialView(unitListMain);
            }
            else
            {
                unitListMain.Search = new List<Models.Unit>();
                return PartialView(unitListMain);
            }
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 02/24/2016
        /// 
        /// Get selected advance units to update advance amount of the unit table, 
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return partial view</returns>
        public int UpdateAdvance(BankLoanSystem.Models.Unit unit)
        {
            if (string.IsNullOrEmpty(Session["userId"].ToString()))
                RedirectToAction("UserLogin", "Login");

            int userId = Convert.ToInt16(Session["userId"]);

            ViewBag.ErrorMsg = "";
            UnitAccess unitAccess = new UnitAccess();

            return unitAccess.AdvanceSelectedItem(unit, 187, userId, unit.AdvanceDate);
           
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 02/25/2016
        /// 
        /// Get selected advance unit list to update advance amount of the unit table 
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return partial view</returns>
        public int UpdateAdvanceAll(ListViewModel list)
        {
            Session["userId"] = 2;
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                RedirectToAction("UserLogin", "Login");
            int userId = Convert.ToInt32(Session["userId"]);
            ViewBag.ErrorMsg = "";


            UnitAccess unitAccess = new UnitAccess();
           
            return unitAccess.AdvanceAllSelectedItems(list.ItemList, 187, userId, list.ItemList[0].AdvanceDate);           
        }

        private Models.AdvanceUnit GetAdvanceUnitList(int loanId)
        {
            UnitAccess unitAccess = new UnitAccess();
            List<BankLoanSystem.Models.Unit> unitList = new List<Models.Unit>();
            unitList = unitAccess.GetNotAdvancedUnitDetailsByLoanId(loanId);
            List<BankLoanSystem.Models.Unit> unitList2 = new List<Models.Unit>();
            Models.AdvanceUnit unitList1 = new Models.AdvanceUnit();
            unitList1.NotAdvanced = unitList;

            unitList1.Search = unitList2;
            unitList1.AdvanceDate = DateTime.Now;
            return unitList1;
        }
    }
}