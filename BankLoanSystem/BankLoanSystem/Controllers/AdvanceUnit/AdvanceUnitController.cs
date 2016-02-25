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

            List<BankLoanSystem.Models.AdvanceUnit> unitList = unitAccess.GetNotAdvancedUnitDetailsByLoanId(187);
            //List<Models.Unit> units = new List<Models.Unit>();
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

        //[HttpPost]
        //public ActionResult Advance(List<BankLoanSystem.Models.Unit> unitList)
        //{
        //    return View(unitList);
        //}

        [HttpPost]
        public ActionResult Advance(List<BankLoanSystem.Models.Unit> unitListf, string[] ids, string identificationNumber, string year, string make, string vehicleModel, string search)
        {
            List<Models.Unit> unitList = new List<Models.Unit>();
            List<Models.Unit> resultList = new List<Models.Unit>();
            if (!string.IsNullOrEmpty(search))
            {


                Models.Unit unit = new Models.Unit();
                unit.IdentificationNumber = "identy1";
                unit.Year = 1999;
                unit.Model = "model1";
                unit.Make = "usa";

                unitList.Add(unit);

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
                return View(resultList);
            }
            else
            {
                return View();
            }

        }


        //POST: SearchItem
        [HttpPost]
        public ActionResult SearchItem(string identificationNumber, string year, string make, string vehicleModel)
        {
            List<Models.Unit> unitList = new List<Models.Unit>();
            List<Models.Unit> resultList = new List<Models.Unit>();

            Models.Unit unit = new Models.Unit();
            unit.IdentificationNumber = "identy1";
            unit.Year = 1999;
            unit.Model = "model1";
            unit.Make = "usa";

            unitList.Add(unit);

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
            return View(resultList);
        }
    }
}