using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BankLoanSystem.Reports;
using Microsoft.Reporting.WebForms;


namespace BankLoanSystem.Controllers
{
    /// <summary>
    /// Get not advanced unit list and display in table
    /// can select one or more units and can do the advance
    /// can search unit by vin/year/make or model
    /// </summary>
    public class AdvanceUnitController : Controller
    {
        //private static LoanSetupStep1 loan;
        User userData = new User();
        
        // Check session in page initial stage
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
                    if (HttpContext.Request.IsAjaxRequest())
                    {

                       
                        filterContext.Result = new HttpStatusCodeResult(404, "Due to inactivity your session has timed out, please log in again.");
                    }
                    else
                    {

                        filterContext.Result = new RedirectResult("/Login/UserLogin?lbl=Due to inactivity your session has timed out, please log in again.");
                    }
                }
            }
            catch
            {
            
                filterContext.Result = new RedirectResult("~/Exceptions/Index");
            }
        }

        /// <summary>
        /// Frontend page: Advance Unit
        /// Title: set loan code to session
        /// Designed: Nadeeka
        /// User story:
        /// Developed: Nadeeka
        /// Date created:2016/2/24
        /// 
        /// </summary>
        /// <param name="loanCode"></param>
        /// <returns></returns>
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
        /// Frontend page: Advance Unit
        /// Title: Get loan details and not advanced unit details from database and return to view
        /// Designed: Nadeeka
        /// User story:
        /// Developed: Nadeeka
        /// Date created: 02/24/2016
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return partial view</returns>
        public ActionResult Advance()
        {
            int flag = -1;
            //assign logged user id to variable
            int userId = userData.UserId;
            string loanCode;
            try
            {
                //convert session to string variable
                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                //if exception occured return to login page
                return RedirectToAction("UserLogin", "Login");
            }
            BranchAccess branch = new BranchAccess();
            //retrieve company type for given user id
            int companyType = branch.getCompanyTypeByUserId(userId);
            //check company type 1-Lender
            if (companyType == 1)
            {
                ViewBag.isLender = true;
            }
            //company type 2 - Dealer
            else
            {
                ViewBag.isLender = false;
            }

            ViewBag.unitClickId = "";
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            //retrieve loan delails for given loan code
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);
            //check logged user role is user
            if (userData.RoleId == 3)
            {
                //check Session["CurrentLoanRights"] is null or empty
                if (Session["CurrentLoanRights"] == null || Session["CurrentLoanRights"].ToString() == "")
                {
                    return RedirectToAction("UserDetails", "UserManagement");
                }
                else {
                    var checkPermission = false;
                    string rgts = "";
                    //convert Session["CurrentLoanRights"] to string variable
                    rgts = (string)Session["CurrentLoanRights"];
                    string[] rgtList = null;
                    //check right string is not empty
                    if (rgts != "")
                    {
                        //split string
                        rgtList = rgts.Split(',');
                    }
                    //check right list is not null
                    if (rgtList != null)
                    {
                        foreach (var x in rgtList)
                        {
                            //check relevant right for Advance Unit page contains in the user right list
                            if (x == "U01")
                            {
                                checkPermission = true;
                            }
                        }
                        //check permission value is false
                        if (checkPermission == false)
                        {
                            //return to dashboard
                            return RedirectToAction("UserDetails", "UserManagement");
                        }
                    }
                    else {
                        //return to dashboard
                        return RedirectToAction("UserDetails", "UserManagement");
                    }

                }
            }
            //check logged user is dealer user
            else if (userData.RoleId == 4)
            {
                //return to dashboard
                return RedirectToAction("UserDetails", "UserManagement");
            }
            ViewBag.loanDetails = loanDetails;
            Models.Unit unit = new Models.Unit();
            //get units which need to advance for given loan id
            AdvanceUnit advanceUnit = this.GetAdvanceUnitList(loanDetails.loanId);
            //assign loan id to session object
            Session["advUnitloanId"] = loanDetails.loanId;
            //assign not advanced unit list to session object
            Session["notAdvancedList"] = advanceUnit.NotAdvanced;
            ViewBag.advanceList = advanceUnit.NotAdvanced;
            //check update result of Advance Unit page
            if((TempData["updateReslt"]!=null)&&(TempData["updateReslt"].ToString() != ""))
            {
                //convert result to integer
                flag = int.Parse(TempData["updateReslt"].ToString());
            }
            //check result value is 1
            if (flag == 1)
            {
                //flag 1 - success
                ViewBag.Msg = "Success";
            }
            else if (flag == 0)
            {
                //flag 0 - error
                ViewBag.Msg = "Error";
            }
            else if (flag == 2)
            {
                //flag 2 - Advance amount error
                ViewBag.Msg = "Advance amount error";
            }else if(flag == 3)
            {
                //flag 3 - Advance error
                ViewBag.Msg = "Advance Error";
            }
            
            //return advance unit list to view
            return View(advanceUnit);
        }
        /*

         Frontend page   : Load titles
         Title           : Load titles for each unit
         Designed        : Kasun Samarawickrama
         User story      : 
         Developed       : Kasun Samarawickrama
         Date created    : 

         */
        public ActionResult loadTitles(string unitId) {

            List<TitleUpload> tl = (new UnitAccess()).GetUploadTitlesByLoanId(unitId);
            
            return PartialView(tl);
        }



        /// <summary>
        /// Frontend page: Advance Unit(Search section)
        /// Title: get unit list which match with given search parameters
        /// Designed: Piyumi P
        /// User story:
        /// Developed: Piyumi P
        /// Date created: 02/27/2016
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
                //convert session to string variable
                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                //if exception occured return to login page
                return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
            }
            //get loan details for given loan code
            LoanSetupStep1 loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);
            

            ViewBag.loanDetails = loanDetails;
            //convert Session["notAdvancedList"] to list object
            List<Models.Unit> unitList = (List<Models.Unit>)Session["notAdvancedList"];
            Models.AdvanceUnit unitListMain = new Models.AdvanceUnit();
            unitListMain.NotAdvanced = new List<Models.Unit>();
            //check atleast one parameter is not null or empty
            if (((!string.IsNullOrEmpty(identificationNumber)) || (!string.IsNullOrEmpty(year)) || (!string.IsNullOrEmpty(make)) || (!string.IsNullOrEmpty(vehicleModel))))
            {
                //search through list elements
                Search sc = new Search();
                //get result list of search
                unitListMain.Search = sc.GetSearchResultsList(unitList, identificationNumber.Trim().ToLower(), year.Trim().ToLower(), make.Trim().ToLower(), vehicleModel.Trim().ToLower());
                //return result list in to partial view
                return PartialView(unitListMain);
            }
            else
            {
                //create empty list if all search parameters are null or empty
                unitListMain.Search = new List<Models.Unit>();
                //return list to partial view
                return PartialView(unitListMain);
            }
        }

        /// <summary>
        /// Frontend page: Advance Unit
        /// Title: Advance one unit
        /// Designed: Nadeeka
        /// User story:
        /// Developed: Nadeeka
        /// Date created: 02/24/2016
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return partial view</returns>
        public int UpdateAdvance(BankLoanSystem.Models.Unit unit)
        {            
            string loanCode;
            try
            {
                //convert session to string variable
                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                throw;
            }
            //get loan details by loan code
            LoanSetupStep1 loanSetupStep1 = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);           
            ViewBag.ErrorMsg = "";
            UnitAccess unitAccess = new UnitAccess();
            //check Session["notAdvancedList"] is not null
            if (Session["notAdvancedList"] != null)
            {
                //convert Session["notAdvancedList"] to list object
                List<Models.Unit> lstUnit = (List<Models.Unit>)Session["notAdvancedList"];
                //check advance amount can not be edited and given advance amount in loanset up and advance amount of selected unit is equal
                if(!loanSetupStep1.isEditAllowable && lstUnit.Find(a => a.UnitId == unit.UnitId).AdvanceAmount != unit.AdvanceAmount)
                {
                   
                    TempData["updateReslt"] = 3;
                    return 3;
                    
                }
                
            }
            //get result of advance one item
            int reslt = unitAccess.AdvanceItem(unit, loanSetupStep1.loanId, userData.UserId, unit.AdvanceDate);
            TempData["updateReslt"] = reslt;

            // after success save**
            //check result of advance
            if (reslt == 1)
            {
                //if mention advance fee, then insert in to fee table - asanka
                if ((Session["loanDashboard"] != null) || (Session["oneLoanDashboard"] != null))
                {
                    Loan loanObj = new Loan();
                    if(Session["loanDashboard"] != null){
                        loanObj = (Loan)Session["loanDashboard"];
                    }
                    else if (Session["oneLoanDashboard"] != null)
                    {
                        loanObj = (Loan)Session["oneLoanDashboard"];
                    }
                    if (loanObj.AdvanceFee == 1)
                    {
                        //check advance amount and other details
                        unitAccess.insertFreeDetailsForAdvance(unit, loanSetupStep1.loanId);
                    }
                }

                //insert to log 
                
                Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, loanSetupStep1.loanId, "Advance Unit", "Advanced Unit:" + unit.IdentificationNumber + (unit.Cost * loanSetupStep1.advancePercentage / 100 != unit.AdvanceAmount ? ",Advance amount edited to: " + unit.AdvanceAmount : ",Advance amount: " + unit.AdvanceAmount) + " ,Advance date:" + unit.AdvanceDate, DateTime.Now);

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
        /// Frontend page: Advance Unit
        /// Title: Advance more than one unit
        /// Designed: Nadeeka
        /// User story:
        /// Developed: Nadeeka
        /// Date created: 02/25/2016
        /// CreatedBy : Nadeeka
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return partial view</returns>
        public int UpdateAdvanceAll(ListViewModel list)
        {           
            string loanCode;
            try
            {
                //convert session to string variable
                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                throw;
            }
            //get loan details by loan code
            LoanSetupStep1 loanSetupStep1 = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);
            ViewBag.ErrorMsg = "";
            //check Session["notAdvancedList"] is not null
            if (Session["notAdvancedList"] != null)
            {
                List<Models.Unit> lstUnit = (List<Models.Unit>)Session["notAdvancedList"];
                foreach (Models.Unit modifiedUnit in list.ItemList)
                {
                    //check advance amount can not be edited and given advance amount in loanset up and advance amount of selected unit is equal
                    if (!loanSetupStep1.isEditAllowable && lstUnit.Find(a => a.UnitId == modifiedUnit.UnitId).AdvanceAmount != modifiedUnit.AdvanceAmount)
                    {
                        TempData["updateReslt"] = 3;
                        return 0;
                    }
                }

            }

            UnitAccess unitAccess = new UnitAccess();
            //get result of advance item list
            int reslt = unitAccess.AdvanceItemList(list.ItemList, loanSetupStep1.loanId, userData.UserId, list.ItemList[0].AdvanceDate);
            TempData["updateReslt"] = reslt;

            // after success save**
            if (reslt == 1)
            {
                string[] arrList = new string[list.ItemList.Count];
                int i = 0;
                foreach (var x in list.ItemList)
                {
                    if (!string.IsNullOrEmpty(x.UnitId))
                    {
                        arrList[i] = "Advanced Unit: " + x.IdentificationNumber + " ,Advance amount:" + x.AdvanceAmount + " ,Advance date: " + x.AdvanceDate;
                        i++;
                    }
                }
                
         
                string units = string.Join(",", arrList);
                //insert log data
                Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, loanSetupStep1.loanId, "Advance Unit", units, DateTime.Now);

                int islog = (new LogAccess()).InsertLog(log);
                //if mention advance fee, then insert in to fee table - asanka
                if ((Session["loanDashboard"] != null) || (Session["oneLoanDashboard"] != null))
                {
                    Loan loanObj = new Loan();
                    if (Session["loanDashboard"] != null)
                    {
                        loanObj = (Loan)Session["loanDashboard"];
                    }
                    else
                    {
                        loanObj = (Loan)Session["oneLoanDashboard"];
                    }
                        //loanObj = (Loan)Session["loanDashboard"]; 
                    if (loanObj.AdvanceFee == 1)
                    {
                        //check advance amount and other details
                        foreach (BankLoanSystem.Models.Unit unitObj in list.ItemList)
                        {
                            unitAccess.insertFreeDetailsForAdvance(unitObj, loanSetupStep1.loanId);
                        }
                    }
                }

                // saving for reporting purpose
                if (Session["AdvItems"] == null)
                {
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
            int loanId = (int) Session["advUnitloanId"];
            ViewBag.LoanId = loanId;
            return View();
        }

        public FileResult PrintPage()
        {
            if (Session["advUnitloanId"] == null) return null;
            int loanId = (int)Session["advUnitloanId"];

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            RptDivAdvanceUnit advanceUnitReport = new RptDivAdvanceUnit();
            var rptViewerPrint = advanceUnitReport.PrintPage(loanId);

            var bytes = rptViewerPrint.LocalReport.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);

            var fsResult = File(bytes, "application/pdf");
            return fsResult;
        }
        /*

       Frontend page   : Load titles
       Title           : View titles for each unit
       Designed        : Kasun Samarawickrama
       User story      : 
       Developed       : Kasun Samarawickrama
       Date created    : 

       */
        public FileResult Download(string imagePath)
        {
            string[] tokens = imagePath.Split('/');
            string fileName = tokens[tokens.Length - 1];
            return File(imagePath, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        //public FileResult Downloadx(string image, string path)
        //{
        //    return File(path + image, System.Net.Mime.MediaTypeNames.Application.Octet);
        //}
        //public ActionResult Downloaderx(string imagePath)
        //{
        //    return File(imagePath, "application/pdf");

        //}
    }
}