using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace BankLoanSystem.Controllers
{
    /// <summary>
    /// Get not advanced unit list and display in table
    /// can select one or more units and can do the advance
    /// can search unit by vin/year/make or model
    /// </summary>
    public class AdvanceUnitController : Controller
    {
        private static LoanSetupStep1 loan;
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

        /// <summary>
        /// CreatedBy:Nadeeka
        /// CreatedDate:2016/2/24
        /// set loan code to session
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
        /// CreatedBy : Nadeeka
        /// CreatedDate: 02/24/2016
        /// 
        /// Get loan details and not advanced unit details from database and return to view
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return partial view</returns>
        public ActionResult Advance()
        {
            int flag = -1;
            int userId = userData.UserId;
            string loanCode;
            try
            {
                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            BranchAccess branch = new BranchAccess();
            int companyType = branch.getCompanyTypeByUserId(userId);
            //int companyType = userData.CompanyType;
            if (companyType == 1)
            {
                ViewBag.isLender = true;
            }
            else
            {
                ViewBag.isLender = false;
            }

            ViewBag.unitClickId = "";
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loan = loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);

            if (userData.RoleId == 3)
            {
                if (Session["CurrentLoanRights"] == null || Session["CurrentLoanRights"].ToString() == "")
                {
                    return RedirectToAction("UserDetails", "UserManagement");
                }
                else {
                    var checkPermission = false;
                    string rgts = "";
                    rgts = (string)Session["CurrentLoanRights"];
                    string[] rgtList = null;
                    if (rgts != "")
                    {
                        rgtList = rgts.Split(',');
                    }
                    if (rgtList != null)
                    {
                        foreach (var x in rgtList)
                        {
                            if (x == "U001")
                            {
                                checkPermission = true;
                            }
                        }
                        if (checkPermission == false)
                        {
                            return RedirectToAction("UserDetails", "UserManagement");
                        }
                    }
                    else {
                        return RedirectToAction("UserDetails", "UserManagement");
                    }

                }
            }

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
            else if (flag == 3)
            {
                ViewBag.Msg = "Advance amount error";
            }

            return View(advanceUnit);
        }

        public ActionResult loadTitles(string unitId) {

            List<TitleUpload> tl = (new UnitAccess()).GetUploadTitlesByLoanId(unitId);
            
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

            LoanSetupStep1 loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);
            

            ViewBag.loanDetails = loanDetails;
            List<Models.Unit> unitList = (List<Models.Unit>)Session["notAdvancedList"];
            Models.AdvanceUnit unitListMain = new Models.AdvanceUnit();
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
        /// Get selected advance units to update advance amount in the unit table, 
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
                throw;
            }
            LoanSetupStep1 loanSetupStep1 = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);           
            ViewBag.ErrorMsg = "";
            UnitAccess unitAccess = new UnitAccess();
            
            if (Session["notAdvancedList"] != null)
            {
                List<Models.Unit> lstUnit = (List<Models.Unit>)Session["notAdvancedList"];
                if(!loanSetupStep1.isEditAllowable && lstUnit.Find(a => a.UnitId == unit.UnitId).AdvanceAmount != unit.AdvanceAmount)
                {
                    TempData["updateReslt"] = 3;
                    return 3;
                    
                }
                
            }

            int reslt = unitAccess.AdvanceItem(unit, loanSetupStep1.loanId, userData.UserId, unit.AdvanceDate);
            TempData["updateReslt"] = reslt;

            // after success save**
            if (reslt == 1)
            {
                //if mention advance fee, then insert in to fee table - asanka
                if ((Session["loanDashboard"] != null) || (Session["oneLoanDashboard"] != null))
                {
                    Loan loanObj = new Loan();
                    loanObj = (Loan)Session["loanDashboard"];
                    if (loanObj.AdvanceFee == 1)
                    {
                        //check advance amount and other details
                        unitAccess.insertFreeDetailsForAdvance(unit, loanSetupStep1.loanId);
                    }
                }

                //insert to log 
                //Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, unit.LoanId, "Add Unit", unit.UnitId + " unit " + (unit.AddAndAdvance ? "added and advanced" : "added") + (unit.Cost * _loan.advancePercentage / 100 != unit.AdvanceAmount ? ", Advance amount edited to " + unit.AdvanceAmount : ""), DateTime.Now);
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

            LoanSetupStep1 loanSetupStep1 = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);
            ViewBag.ErrorMsg = "";

            if (Session["notAdvancedList"] != null)
            {
                List<Models.Unit> lstUnit = (List<Models.Unit>)Session["notAdvancedList"];
                foreach (Models.Unit modifiedUnit in list.ItemList)
                {
                    if (!loanSetupStep1.isEditAllowable && lstUnit.Find(a => a.UnitId == modifiedUnit.UnitId).AdvanceAmount != modifiedUnit.AdvanceAmount)
                    {
                        TempData["updateReslt"] = 3;
                        return 0;
                    }
                }

            }

            UnitAccess unitAccess = new UnitAccess();
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
                
                //arrList = arrList.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                ////user.UserRights = arrList.ToString();
                string units = string.Join(",", arrList);
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
        public ActionResult Downloaderx(string ImageName)
        {
            return File(ImageName, "application/pdf");

        }
    }
}