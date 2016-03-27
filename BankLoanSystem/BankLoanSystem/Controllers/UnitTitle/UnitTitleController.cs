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
                    //return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
                    filterContext.Result = new RedirectResult("~/Login/UserLogin");
                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
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
                        obj2.TitleList.AddRange(resultList.Where(t => t.UnitStatus == 0));

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