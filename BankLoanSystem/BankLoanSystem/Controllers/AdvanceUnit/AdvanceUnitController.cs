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
        public ActionResult Advance()
        {
            Session["userId"] = 2;
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");
            int userId = Convert.ToInt32(Session["userId"]);

            Session["userId"] = 2;
            int loanId = 187;

            
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanStepOne(loanId);

            ViewBag.loanDetails = loanDetails;
            Models.Unit unit = new Models.Unit();
            //List<BankLoanSystem.Models.Unit> unitList = new List<Models.Unit>();
            //unitList = unitAccess.GetNotAdvancedUnitDetailsByLoanId(loanId);
            //List<BankLoanSystem.Models.Unit> unitList2 = new List<Models.Unit>();
            //Models.AdvanceUnit unitList1 = new Models.AdvanceUnit();
            //unitList1.NotAdvanced = unitList;

            //unitList1.Search = unitList2;


            Session["notAdvancedList"] = this.GetAdvanceUnitList(loanId).NotAdvanced;

            return View(this.GetAdvanceUnitList(loanId));
        }        

        [HttpPost]
        public ActionResult Advance(List<BankLoanSystem.Models.Unit> unitListf, string[] ids, string identificationNumber, string year, string make, string vehicleModel, string search)
        {
            List<Models.Unit> unitList = (List<Models.Unit>)Session["notAdvancedList"];
            List<Models.Unit> resultList = new List<Models.Unit>();
            Models.AdvanceUnit unitListMain = new Models.AdvanceUnit();
            if (!string.IsNullOrEmpty(search))
            {

                //search through list elements
                foreach (Models.Unit unitElement in unitList)
                {
                    if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Year.ToString().Contains(year) && unitElement.Make.Contains(make) && unitElement.Model.Contains(vehicleModel))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Year.ToString().Contains(year) && unitElement.Make.Contains(make))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Year.ToString().Contains(year))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.IdentificationNumber.Contains(identificationNumber))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (string.IsNullOrEmpty(vehicleModel) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.Year.ToString().Contains(year) && unitElement.Make.Contains(make) && unitElement.Model.Contains(vehicleModel))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (string.IsNullOrEmpty(vehicleModel) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.Make.Contains(make) && unitElement.Model.Contains(vehicleModel))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (!string.IsNullOrEmpty(vehicleModel) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.Model.Contains(vehicleModel))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.Year.ToString().Contains(year))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.Make.Contains(make))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.Year.ToString().Contains(year) && unitElement.Make.Contains(make))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Model.Contains(vehicleModel))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Make.Contains(make))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Model.Contains(vehicleModel))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (!string.IsNullOrEmpty(identificationNumber) && string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Make.Contains(make) && unitElement.Model.Contains(vehicleModel))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                    else if (!string.IsNullOrEmpty(identificationNumber) && !string.IsNullOrEmpty(year) && string.IsNullOrEmpty(make) && !string.IsNullOrEmpty(vehicleModel))
                    {
                        if (unitElement.IdentificationNumber.Contains(identificationNumber) && unitElement.Year.ToString().Contains(year) && unitElement.Model.Contains(vehicleModel))
                        {
                            resultList.Add(unitElement);
                        }
                    }
                }
                unitListMain.Search = resultList;
                unitListMain.NotAdvanced = unitList;
                return View(unitListMain);
            }
            else
            {
                return View();
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
        public ActionResult UpdateAdvance(BankLoanSystem.Models.Unit unit)
        {
            if (string.IsNullOrEmpty(Session["userId"].ToString()))
                RedirectToAction("UserLogin", "Login");

            int userId = Convert.ToInt16(Session["userId"]);

            ViewBag.ErrorMsg = "";
            UnitAccess unitAccess = new UnitAccess();

            var res = unitAccess.AdvanceSelectedItem(unit, 187, userId, DateTime.Now);
            if (res == 0)
            {
                return RedirectToAction("Advance");
            }
            return RedirectToAction("Advance", this.GetAdvanceUnitList(187));



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
        public ActionResult UpdateAdvanceAll(ListViewModel list)
        {
            Session["userId"] = 2;
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");
            int userId = Convert.ToInt32(Session["userId"]);
            ViewBag.ErrorMsg = "";


            UnitAccess unitAccess = new UnitAccess();
            foreach(Models.Unit u in list.ItemList)
            {
                u.AdvanceDate = DateTime.Now;
            }
            int count = unitAccess.AdvanceAllSelectedItems(list.ItemList, 187, userId, list.ItemList[0].AdvanceDate);
            if (count > 0)
            {
                return RedirectToAction("Advance");
            }
            else
            {
                return RedirectToAction("Advance");
            }



            return PartialView("Step10", this.GetAdvanceUnitList(187));
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