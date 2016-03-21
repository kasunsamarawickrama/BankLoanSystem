using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using BankLoanSystem.Models;
using BankLoanSystem.DAL;

namespace BankLoanSystem.Controllers.UnitPayOff
{
    public class UnitPayOffController : Controller
    {
        private static LoanSetupStep1 loan;
        User userData = new User();
        // Check session in page initia stage
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (Session["AuthenticatedUser"] != null)
                {
                    userData = ((User)Session["AuthenticatedUser"]);
                }
                else
                {
                    //return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
                    filterContext.Controller.TempData.Add("UserLogin", "Login");
                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                filterContext.Controller.TempData.Add("UserLogin", "Login");
            }
        }

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
            int userId = userData.UserId;
            string loanCode;

            try
            {
                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                return new HttpStatusCodeResult(404, "Session Expired");
            }


            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);


            ViewBag.loanDetails = loanDetails;
            Models.Unit unit = new Models.Unit();
            AdvanceUnit advanceUnit = this.GetAdvanceUnitList(loanDetails.loanId);
            //Session["notAdvancedList"] = advanceUnit.NotAdvanced;
            ViewBag.advanceList = advanceUnit.NotAdvanced;

            UnitPayOffViewModel unitPayOffViewModel = new UnitPayOffViewModel();

            CurtailmentAccess payoff = new CurtailmentAccess();

            unitPayOffViewModel.UnitPayOffList = new List<UnitPayOffModel>();
            unitPayOffViewModel.PayDate = DateTime.Now;

            unitPayOffViewModel.UnitPayOffList = payoff.GetUnitPayOffList(loanDetails.loanId);
            Session["payoffList"] = unitPayOffViewModel.UnitPayOffList;
            return View(unitPayOffViewModel);
        }

        //[HttpPost]
        //public ActionResult PayOff(UnitPayOffViewModel resModel)
        //{
        //    return View();
        //}

        public int UnitListPay(List<UnitPayOffModel> payOffModelList, DateTime payDate, string titleReturn)
        {
            try
            {
                XElement xEle = new XElement("Units",
                    from unit in payOffModelList
                    select new XElement("Unit",
                        new XElement("UnitId", unit.UnitId),
                        new XElement("Balance", unit.Balance)
                        ));
                string xmlDoc = xEle.ToString();

                int titleStatus = titleReturn == "Yes" ? 2:4;

                return (new CurtailmentAccess()).PayOffUnits(xmlDoc, payDate, titleStatus);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// CreatedBy:kasun
        /// CreatedDate:2016/3/21
        /// Search for payoff units
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="vehicleModel"></param>
        /// <returns></returns>
        public ActionResult SearchUnit(string identificationNumber, string year, string make, string vehicleModel)
        {
            string loanCode;
            try
            {
                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404, "Session Expired");
            }
            //int userId = 57;

            LoanSetupStep1 loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);


            ViewBag.loanDetails = loanDetails;
            List<Models.UnitPayOffModel> unitList = (List<Models.UnitPayOffModel>)Session["payoffList"];

            Models.UnitPayOffViewModel unitListMain = new Models.UnitPayOffViewModel();
            //unitListMain.NotAdvanced = unitList;
            unitListMain.UnitPayOffList = new List<Models.UnitPayOffModel>();
            if (((!string.IsNullOrEmpty(identificationNumber)) || (!string.IsNullOrEmpty(year)) || (!string.IsNullOrEmpty(make)) || (!string.IsNullOrEmpty(vehicleModel))))
            {
                //search through list elements
                Search sc = new Search();

                unitListMain.SearchList = sc.GetSearchPayOffList(unitList, identificationNumber.Trim().ToLower(), year.Trim().ToLower(), make.Trim().ToLower(), vehicleModel.Trim().ToLower());

                return PartialView(unitListMain);
            }
            else
            {
                unitListMain.SearchList = new List<Models.UnitPayOffModel>();
                return PartialView(unitListMain);
            }
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