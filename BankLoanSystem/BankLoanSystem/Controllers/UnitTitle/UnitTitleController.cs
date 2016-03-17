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
                    filterContext.Controller.TempData.Add("UserLogin", "Login");
                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                filterContext.Controller.TempData.Add("UserLogin", "Login");
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
            return View(obj2);
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
           
            if ((!string.IsNullOrEmpty(identificationNumber))&&(!string.IsNullOrEmpty(loanCode)))
            {
                obj2.TitleList = obj1.SearchTitle(loanCode,identificationNumber);
                //ViewBag.TitleList = obj2.TitleList;
            }
            return PartialView(obj2);
        }
    }
}