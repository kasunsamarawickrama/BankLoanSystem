using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.UnitTitle
{
    public class UnitTitleController : Controller
    {
        User userData = new User();

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
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                filterContext.Result = new RedirectResult("~/Exceptions/Index");
            }
        }
        // GET: Title
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult setLoanCode(string loanCode)
        {
            Session["loanCode"] = loanCode;
            
            return RedirectToAction("TitleStatusUpdate");
        }

        /*
        Frontend page: Update Titles
        Title: Return View of Update Titles page
        Designed: Piyumi
        User story:
        Developed: Piyumi
        Date created: 03/17/2016
        */
        public ActionResult TitleStatusUpdate()
        {
            //Check Session["IsTitleTrack"] is not null and value is 0
            if (Session["IsTitleTrack"] !=null && int.Parse(Session["IsTitleTrack"].ToString()) == 0)
            {
                //return to dashboard if title doesnot need to be tracked
                return RedirectToAction("UserDetails", "UserManagement");
            }
            //user role 3 - user
            else if (userData.RoleId == 3)
            {
                //Check Session["CurrentLoanRights"] is not null or empty
                if (Session["CurrentLoanRights"] == null || Session["CurrentLoanRights"].ToString() == "")
                {
                    //return to dashboard if Session["CurrentLoanRights"] is null or empty
                    return RedirectToAction("UserDetails", "UserManagement");
                }
                else {
                    var checkPermission = false;
                    string rgts = "";
                    //convert Session["CurrentLoanRights"] to string
                    rgts = (string)Session["CurrentLoanRights"];
                    string[] rgtList = null;
                    //Check string is not empty
                    if (rgts != "")
                    {
                        //split right string and insert to a list
                        rgtList = rgts.Split(',');
                    }
                    //Check list is not null
                    if (rgtList != null)
                    {
                        foreach (var x in rgtList)
                        {
                            //Check right list contains the relevant right which represent Update Titles page
                            if (x == "U02")
                            {
                                checkPermission = true;
                            }
                        }
                        //Check user is given permission to Update Titles page
                        if (checkPermission == false)
                        {
                            //if no permission to Update Titles page return to dashboard
                            return RedirectToAction("UserDetails", "UserManagement");
                        }
                    }
                    else {
                        //if right list is null return to dashboard
                        return RedirectToAction("UserDetails", "UserManagement");
                    }

                }
            }
            //user role 4 - dealer user
            else if (userData.RoleId == 4)
            {
                //if dealer user return to dashboard
                return RedirectToAction("UserDetails", "UserManagement");
            }
            TitleStatus obj2 = new TitleStatus();
            obj2.TitleList = new List<Models.Unit>();
            int compType = 0;
            
            //get company type by company id
            if (userData.UserId > 0)
            {
                BranchAccess obj1 = new BranchAccess();
                compType = obj1.getCompanyTypeByUserId(userData.UserId);
            }
            //Check company type is greater than 0
            if (compType > 0)
            {
                //assign company type to viewbag variable
                ViewBag.CompanyType = compType;
            }
            int flag = -1;

            //Check result after updating title status
            if ((TempData["reslt"] !=null)&& (TempData["reslt"].ToString() != ""))
            {
                //assign result value to a variable
                flag = int.Parse(TempData["reslt"].ToString());
                //Check value of result
                if (flag == 1)
                {
                    ViewBag.Msg = "Success";
                }
                else if (flag == 0)
                {
                    ViewBag.Msg = "Error";
                }
                //return TitleStatus model object to view
                return View(obj2);
            }
            else
            {
                //return TitleStatus model object to view
                return View(obj2);
            }
            
           
        }

        /*
      Frontend page: Update Titles
      Title: Search titles by identification number
      Designed: Piyumi
      User story:
      Developed: Piyumi
      Date created: 03/17/2016
      */
        public ActionResult SearchTitleStatus(string identificationNumber)
        {
            //check Session["AuthenticatedUser"] is null or Session["loanCode"] is null
            if (Session["AuthenticatedUser"]==null || Session["loanCode"] == null)
            {
                //return to login page if sessions are null
                return RedirectToAction("UserLogin", "Login",new { lbl = " Due to inactivity your session has timed out, please log in again." });
            }
            //Conver session to string variable
            string loanCode = Session["loanCode"].ToString();

            TitleAccess obj1 = new TitleAccess();
            TitleStatus obj2 = new TitleStatus();
            List<Models.Unit> resultList = new List<Models.Unit>();

            //Check input parameter:identificationNumber is null or empty and loancode is null or empty 
            if ((!string.IsNullOrEmpty(identificationNumber))&&(!string.IsNullOrEmpty(loanCode)))
            {
                //search from units where matching the loan code and last 6 digits of identification number
                resultList = obj1.SearchTitle(loanCode,identificationNumber);
                
            }
            //Check search result list is not null
            if (resultList != null)
            {
                //filter list if contain active units - 1
                obj2.TitleList = resultList.FindAll(t => t.UnitStatus == 1);
                //Check active units count is 0
                if (obj2.TitleList.Count() == 0)
                {
                    //filter list if contain payoff units - 2
                    obj2.TitleList = resultList.FindAll(t => t.UnitStatus == 2);
                    //Check inactive units count is 0
                    if (obj2.TitleList.Count() == 0)
                    {
                        //filter list if contain inactive units - 0
                        obj2.TitleList = resultList.FindAll(t => t.UnitStatus == 0);

                    }
                }
                //return search result list to view
                return PartialView(obj2);
            }
            else
            {
                obj2.TitleList = new List<Models.Unit>();
                //return search result list to view
                return PartialView(obj2);
            }
            
        }
            /*
        Frontend page: Update Titles
        Title: Update title status
        Designed: Piyumi
        User story:
        Developed: Piyumi
        Date created: 03/17/2016
        */
        public int UpdateTitleStatus(Models.Unit unitTitle)
        {
            string loanCode = null;
            //Check Session["loanCode"] is null or empty
            if (!string.IsNullOrEmpty(Session["loanCode"].ToString()))
            {
            //if not null or empty convert session to string variable
                loanCode = Session["loanCode"].ToString();
            }
            TitleAccess titleObj = new TitleAccess();
            //update title status
            bool reslt = titleObj.UpdateTitle(unitTitle, loanCode,userData.UserId);
            //Check result of update title
            if (reslt)
            {
            //if result is true get loan details by loan code
                LoanSetupStep1 loanDetails = new LoanSetupStep1();
                loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);
                string status = "";
                //Check title status
                //TitleStatus 0 - Not received
                if (unitTitle.TitleStatus == 0)
                {
                    status = "Not Received";
                }
                //TitleStatus 1 - Received
                else if (unitTitle.TitleStatus == 1)
                {
                    status = "Received";
                }
                //TitleStatus 2 - Returned to Dealer
                else if (unitTitle.TitleStatus == 2)
                {
                    status = "Returned to Dealer";
                }
                //TitleStatus 3 - Sent to Bank
                else if (unitTitle.TitleStatus == 3)
                {
                    status = "Sent to Bank";
                }
                //insert log entry 
                Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId,loanDetails.loanId, "Title Status Update", "Update title status of unit:" + unitTitle.IdentificationNumber +" ,Updated status:"+status+",Updated date:"+ DateTime.Now, DateTime.Now);

                int islog = (new LogAccess()).InsertLog(log);
                TempData["reslt"] = 1;
                return 1;
            }
            else
            {
                TempData["reslt"] =0;
                return 0;
            }
        }
    }
}