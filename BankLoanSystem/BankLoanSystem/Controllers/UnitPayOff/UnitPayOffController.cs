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
        static int _companyType = 0;
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
                    if (HttpContext.Request.IsAjaxRequest())
                    {

                        //new HttpStatusCodeResult(404, "Failed to Setup company.");
                        filterContext.Result = new HttpStatusCodeResult(404, "Session Expired");
                    }
                    else
                    {

                        filterContext.Result = new RedirectResult("~/Login/UserLogin");
                    }
                }
            }
            catch(Exception ex)
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
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

       

        public ActionResult loadGrid()
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
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
            }          

            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);



            BranchAccess ba = new BranchAccess();
            _companyType = ba.getCompanyTypeByUserId(userData.UserId);
            ViewBag.ComType = _companyType;
            ViewBag.loanId = loanDetails.loanId;
            ViewBag.loanDetails = loanDetails;

            UnitPayOffViewModel unitPayOffViewModel = new UnitPayOffViewModel();
            CurtailmentAccess payoff = new CurtailmentAccess();
            unitPayOffViewModel.UnitPayOffList = new List<UnitPayOffModel>();
            unitPayOffViewModel.PayDate = DateTime.Now;

            unitPayOffViewModel.UnitPayOffList = payoff.GetUnitPayOffList(loanDetails.loanId);

            decimal advanceFee = payoff.AdvanceForPayOffUnits(loanDetails.loanId);
            ViewBag.AdvanceFee = advanceFee;

            var unitPayOffList = unitPayOffViewModel.UnitPayOffList;

            Session["payoffList"] = unitPayOffList;
            ViewBag.payOffList = unitPayOffList;

            TitleAccess ta = new TitleAccess();
            Title title = ta.getTitleDetails(loanDetails.loanId);

            if (title != null)
            {
                bool isTitleTrack = title.IsTitleTrack;
                if (isTitleTrack)
                    ViewBag.IsTitleTrack = "Yes";

            }

            return PartialView(unitPayOffViewModel);
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
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
            }
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
                            if (x == "U003")
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

            //LoanSetupStep1 loanDetails = new LoanSetupStep1();
            //loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);

            

            //BranchAccess ba = new BranchAccess();
            //_companyType = ba.getCompanyTypeByUserId(userData.UserId);
            //ViewBag.ComType = _companyType;
            //ViewBag.loanId = loanDetails.loanId;




            //UnitPayOffViewModel unitPayOffViewModel = new UnitPayOffViewModel();

            //CurtailmentAccess payoff = new CurtailmentAccess();
            //unitPayOffViewModel.UnitPayOffList = new List<UnitPayOffModel>();
            //unitPayOffViewModel.PayDate = DateTime.Now;

            //unitPayOffViewModel.UnitPayOffList = payoff.GetUnitPayOffList(loanDetails.loanId);
            //var unitPayOffList = unitPayOffViewModel.UnitPayOffList;
            //Session["payoffList"] = unitPayOffList;
            //ViewBag.payOffList = unitPayOffList;
            //return View(unitPayOffViewModel);

            //Check title 
            //TitleAccess ta = new TitleAccess();
            //Title title = ta.getTitleDetails(loanDetails.loanId);

            //if (title != null)
            //{
            //    bool isTitleTrack = title.IsTitleTrack;
            //    if (isTitleTrack)
            //        ViewBag.IsTitleTrack = "Yes";

            //}

            //loanDetails
            //ViewBag.loanDetails = loanDetails;


            //if (TempData["message"] != null)
            //{
            //    int res = Convert.ToInt32(TempData["message"]);
            //    if (res == 0)
            //    {
            //        ViewBag.Msg = "PayOffError";
            //        TempData["out"] = "PayOffError";
            //    }
            //    else {
            //        ViewBag.Msg = "PayOffSuccess";
            //        TempData["out"] = "PayOffSuccess";
            //    }
            //}
            //else if (TempData["out"] != null)
            //{
            //    string str = TempData["out"].ToString();
            //    if (str == "PayOffError")
            //    {
            //        ViewBag.Msg = "PayOffError";
            //        return View(unitPayOffViewModel);
            //    }
            //    else {
            //        ViewBag.Msg = "PayOffSuccess";
            //        return View(unitPayOffViewModel);
            //    }
            //}
            //if (HttpContext.Request.IsAjaxRequest())
            //{
            //    ViewBag.AjaxRequest = 1;
            //    return PartialView();
            //}
            //else
            //{

                return View();
            //}
        }

        //[HttpPost]
        //public ActionResult PayOff(UnitPayOffViewModel resModel)
        //{
        //    return View();
        //}
        [HttpPost]
        public int UnitListPay(List<UnitPayOffModel> payOffModelList, DateTime payDate, string titleReturn)
        {
            int result = 0;
            try
            {
                XElement xEle = new XElement("Units",
                    from unit in payOffModelList
                    select new XElement("Unit",
                        new XElement("LoanId", unit.LoanId),
                        new XElement("UnitId", unit.UnitId),
                        new XElement("Balance", unit.Balance)
                        ));
                string xmlDoc = xEle.ToString();

                int titleStatus = 0;

                if (_companyType == 1)
                    titleStatus = titleReturn == "Yes" ? 2 : 4;
                else if (_companyType == 2)
                    titleStatus = titleReturn == "Yes" ? 3 : 4;

                result = (new CurtailmentAccess()).PayOffUnits(xmlDoc, payDate, titleStatus);

                if(result == 1)
                {
                    //if mention advance fee, then insert in to fee table - asanka
                    UnitAccess unitAccess = new UnitAccess();
                   

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
                            foreach (var payoff in payOffModelList)
                            {
                                unitAccess.insertFreeDetailsForPayOffPage(payoff, payDate);
                            }
                         }
                    }


                    List<string> IDNumbers = new List<string>();

                    foreach (var payoff in payOffModelList)
                    {
                        IDNumbers.Add(payoff.IdentificationNumber);
                       

                    }

                    //insert to log 
                    Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, payOffModelList[0].LoanId, "Pay Off", "Pay Off unit(s) : " + string.Join(",", IDNumbers) + ", Pay Date : " + payDate.ToString("dd/MM/yyyy"), DateTime.Now);

                    int islog = (new LogAccess()).InsertLog(log);
                }

                TempData["message"] = result;
                
                return result;
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
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
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