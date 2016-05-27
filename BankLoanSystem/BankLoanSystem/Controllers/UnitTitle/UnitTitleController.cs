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
                        filterContext.Result = new HttpStatusCodeResult(404, "Session Expired");
                    }
                    else
                    {

                        filterContext.Result = new RedirectResult("~/Login/UserLogin");
                    }
                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
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

        public ActionResult TitleStatusUpdate()
        {
            TitleStatus obj2 = new TitleStatus();
            obj2.TitleList = new List<Models.Unit>();
            int compType = 0;
            
            //get company type by company id
            if (userData.UserId > 0)
            {
                BranchAccess obj1 = new BranchAccess();
                compType = obj1.getCompanyTypeByUserId(userData.UserId);
            }
            if (compType > 0)
            {
                ViewBag.CompanyType = compType;
            }
            int flag = -1;

            if (userData.RoleId == 3 || userData.RoleId == 4)
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
                            if (x == "U002")
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
            if ((TempData["reslt"] !=null)&& (TempData["reslt"].ToString() != ""))
            {
                flag = int.Parse(TempData["reslt"].ToString());
                if (flag == 1)
                {
                    ViewBag.Msg = "Success";
                }
                else if (flag == 0)
                {
                    ViewBag.Msg = "Error";
                }
                return View(obj2);
            }
            else
            {
                return View(obj2);
            }
            
           
        }
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate: 03/17/2016
        /// search titles by identification number
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public ActionResult SearchTitleStatus(string identificationNumber)
        {
            if(Session["AuthenticatedUser"]==null || Session["loanCode"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            string loanCode = Session["loanCode"].ToString();

            TitleAccess obj1 = new TitleAccess();
            TitleStatus obj2 = new TitleStatus();
            List<Models.Unit> resultList = new List<Models.Unit>();


            if ((!string.IsNullOrEmpty(identificationNumber))&&(!string.IsNullOrEmpty(loanCode)))
            {
                resultList = obj1.SearchTitle(loanCode,identificationNumber);
                //ViewBag.TitleList = obj2.TitleList;
            }
            if (resultList != null)
            {
                obj2.TitleList = resultList.FindAll(t => t.UnitStatus == 1);
                if (obj2.TitleList.Count() == 0)
                {
                    obj2.TitleList = resultList.FindAll(t => t.UnitStatus == 2);
                    if (obj2.TitleList.Count() == 0)
                    {
                        obj2.TitleList = resultList.FindAll(t => t.UnitStatus == 0);

                    }
                }
                return PartialView(obj2);
            }
            else
            {
                obj2.TitleList = new List<Models.Unit>();
                return PartialView(obj2);
            }
            
        }
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate: 03/17/2016
        /// search titles by identification number
        /// 0=>NotReceived
        /// 1=>Received
        /// 2=>Returned to Dealer
        /// 3=>Sent to Bank
        /// </summary>
        /// <param name="unitTitle"></param>
        /// <returns></returns>
        public int UpdateTitleStatus(Models.Unit unitTitle)
        {
            string loanCode = null;
            if (!string.IsNullOrEmpty(Session["loanCode"].ToString()))
            {
                loanCode = Session["loanCode"].ToString();
            }
            TitleAccess titleObj = new TitleAccess();
            bool reslt = titleObj.UpdateTitle(unitTitle, loanCode,userData.UserId);
            if (reslt)
            {
                LoanSetupStep1 loanDetails = new LoanSetupStep1();
                loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);
                string status = "";

                if (unitTitle.TitleStatus == 0)
                {
                    status = "Not Received";
                }
                else if (unitTitle.TitleStatus == 1)
                {
                    status = "Received";
                }
                else if (unitTitle.TitleStatus == 2)
                {
                    status = "Returned to Dealer";
                }
                else if (unitTitle.TitleStatus == 3)
                {
                    status = "Sent to Bank";
                }
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