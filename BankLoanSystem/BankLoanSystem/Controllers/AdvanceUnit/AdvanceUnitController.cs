using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
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
                    //filterContext.Controller.TempData.Add("UserLogin", "Login");
                    filterContext.Result = new RedirectResult("~/Login/UserLogin");
                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
            }
        }

        public ActionResult setLoanCode(string loanCode)
        {
            Session["loanCode"] = loanCode;

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
        public ActionResult Advance()
        {
            int flag = -1;
            int userId = userData.UserId;
            string loanCode;
            //Session["userId"] = 62;
            //Session["loanCode"] = "COM06_01-00001";
            try
            {
                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                return RedirectToAction("UserLogin", "Login");
            }


            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);


            ViewBag.loanDetails = loanDetails;
            Models.Unit unit = new Models.Unit();
            AdvanceUnit advanceUnit = this.GetAdvanceUnitList(loanDetails.loanId);
            Session["notAdvancedList"] = advanceUnit.NotAdvanced;
            ViewBag.advanceList = advanceUnit.NotAdvanced;
            if((TempData["updateReslt"]!=null)&&(TempData["updateReslt"].ToString() != ""))
            {
                flag = int.Parse(TempData["updateReslt"].ToString());
            }
            if ((flag == 1)||(flag == 2))
            {
                ViewBag.Msg = "Success";
            }
            else if (flag == 0)
            {
                ViewBag.Msg = "Error";
            }
           
            return View(advanceUnit);
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
            string loanCode;
            try
            {
                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                throw ;
            }
            //int userId = 57;
            LoanSetupStep1 loanSetupStep1 = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);           
            ViewBag.ErrorMsg = "";
            UnitAccess unitAccess = new UnitAccess();
            int reslt = unitAccess.AdvanceItem(unit, loanSetupStep1.loanId, userData.UserId, unit.AdvanceDate);
            TempData["updateReslt"] = reslt;

            return reslt;


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
            string loanCode;
            try
            {
                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                throw;
            }
            //int userId = 57;

            LoanSetupStep1 loanSetupStep1 = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);
            ViewBag.ErrorMsg = "";
            UnitAccess unitAccess = new UnitAccess();
            int reslt = unitAccess.AdvanceItemList(list.ItemList, loanSetupStep1.loanId, userData.UserId, list.ItemList[0].AdvanceDate);
            TempData["updateReslt"] = reslt;
            return reslt;        
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
           
            return unitList1;
        }
    }
}