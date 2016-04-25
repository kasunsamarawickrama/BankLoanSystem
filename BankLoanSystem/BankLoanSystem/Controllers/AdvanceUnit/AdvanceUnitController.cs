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
            ViewBag.unitClickId = "";
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loan = loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);


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


        public ActionResult loadTitles(string unitId) {

            List<TitleUpload> tl = (new UnitAccess()).GetUploadTitlesByLoanId(unitId);
            //if (tl != null && tl.Count > 0)
            //{
            //    ViewBag.Titles = tl;
            //}
            return PartialView(tl);
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
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
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

            // after success save**
            if(reslt == 1 ) {
                ////if mention advance fee, then insert in to fee table - asanka
                //if ((Session["loanDashboard"] != null) || (Session["oneLoanDashboard"] != null))
                //{
                //    Loan loanObj = new Loan();
                //    loanObj = (Loan)Session["loanDashboard"];
                //    if (loanObj.AdvanceFee == 1)
                //    {
                //        //check advance amount and other details
                //        unitAccess.insertFreeDetails(unit);
                //    }
                //}


                //insert to log 
                Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, loanSetupStep1.loanId, "Advance Unit", "Advance - "+unit.UnitId, DateTime.Now);

                int islog = (new LogAccess()).InsertLog(log);
                // saving for reporting purpose
                if (Session["AdvItems"] == null)
            {
                List<Models.Unit> unitlist = new List<Models.Unit>();
                unitlist.Add(unit);
                Session["AdvItems"] = unitlist;
            }
            else
            {
                List<Models.Unit> unitlist = new List<Models.Unit>();
                unitlist = (List<Models.Unit>)Session["AdvItems"];
                unitlist.Add(unit);
                Session["AdvItems"] = unitlist;
            }
            }

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

            // after success save**
            if(reslt == 1) {
                ////if mention advance fee, then insert in to fee table - asanka
                //if ((Session["loanDashboard"] != null) || (Session["oneLoanDashboard"] != null))
                //{
                //    Loan loanObj = new Loan();
                //    loanObj = (Loan)Session["loanDashboard"];
                //    if (loanObj.AdvanceFee == 1)
                //    {
                //        //check advance amount and other details
                //        foreach (BankLoanSystem.Models.Unit unitObj in list.ItemList)
                //        {
                //            unitAccess.insertFreeDetails(unitObj);
                //        }
                //    }
                //}
                string[] arrList = new string[list.ItemList.Count];
                int i = 0;
                foreach (var x in list.ItemList)
                {
                    if (!string.IsNullOrEmpty(x.UnitId))
                    {
                        arrList[i] = x.UnitId;
                        i++;
                    }
                }

                //arrList = arrList.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                ////user.UserRights = arrList.ToString();
                string units = string.Join(",", arrList);
                Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, loanSetupStep1.loanId, "Advance Unit", "Advance - " + units, DateTime.Now);

                int islog = (new LogAccess()).InsertLog(log);
                // saving for reporting purpose
                if (Session["AdvItems"] == null) { 
            Session["AdvItems"] = list.ItemList;
            }
            else
            {
                List<Models.Unit> unitlist = new List<Models.Unit>();
                unitlist = (List<Models.Unit>)Session["AdvItems"];
                unitlist.AddRange(list.ItemList);
                Session["AdvItems"] = unitlist;
            }
            }
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

        public ActionResult AdvanceUnitReport()
        {
            ViewBag.LoanId = loan.loanId;
            return View();
        }
        public FileResult Download(string ImageName)
        {
            return File(ImageName, System.Net.Mime.MediaTypeNames.Application.Octet);
        }
        public FileResult Downloadx(string image, string path)
        {
            return File(path + image, System.Net.Mime.MediaTypeNames.Application.Octet);
        }
    }
}