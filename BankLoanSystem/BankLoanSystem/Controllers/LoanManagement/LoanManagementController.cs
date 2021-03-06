﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using BankLoanSystem.Code;
using System.Data;

namespace BankLoanSystem.Controllers
{
    public class LoanManagementController : Controller
    {

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
                    if (HttpContext.Request.IsAjaxRequest())
                    {

                        filterContext.Result = new HttpStatusCodeResult(404, "Due to inactivity your session has timed out, please log in again.");

                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("/Login/UserLogin?lbl=Due to inactivity your session has timed out, please log in again.");

                    }
                    //return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });

                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                filterContext.Result = new RedirectResult("~/Exceptions/Index");
            }
        }



        // GET: LoanManagement
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult RenewLoan()
        {

            Session.Remove("popUpSelectionType");
            Loan loan = new Loan();
            if ((Session["loanDashboardRenewLoan"] != null) && (Session["loanDashboardRenewLoan"].ToString() != ""))
            {

                loan = (Loan)Session["loanDashboardRenewLoan"];

                if (TempData["message"] != null && (string)TempData["message"] != "")
                {
                    string str = (string)TempData["message"];
                    if (str == "success")
                    {
                        ViewBag.SuccessMsg = "success";
                    }
                    else {
                        ViewBag.ErrorMsg = "fail";
                    }
                    Loan loanUpdated = new Loan();
                    loanUpdated = (new LoanManagementAccess()).GetLoanByLoanCode(loan.LoanId,loan.LoanCode);
                    loanUpdated.CreatedDate = loan.CreatedDate;
                    loanUpdated.PartnerName = loan.PartnerName;
                    loanUpdated.BranchName = loan.BranchName;

                    loan.MaturityDate = loanUpdated.MaturityDate;
                    Session["loanDashboardRenewLoan"] = loan;

                    if (HttpContext.Request.IsAjaxRequest())
                    {
                        ViewBag.AjaxRequest = 1;
                        return PartialView(loanUpdated);
                    }
                    else
                    {

                        return View(loanUpdated);
                    }
                }
                else {
                    if (HttpContext.Request.IsAjaxRequest())
                    {
                        ViewBag.AjaxRequest = 1;
                        return PartialView(loan);
                    }
                    else
                    {

                        return View(loan);
                    }
                }
               
                //return View();
            }
            else {
                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return RedirectToAction("UserDetails","UserManagement");
                }
                else
                {

                    return RedirectToAction("UserDetails", "UserManagement");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RenewLoan(Loan l) {

            Loan loanPost = new Loan();
            if ((Session["loanDashboardRenewLoan"] != null) && (Session["loanDashboardRenewLoan"].ToString() != ""))
            {
                loanPost = (Loan)Session["loanDashboardRenewLoan"];
                loanPost.RenewalDate = l.RenewalDate;
            }

            if ((new LoanManagementAccess()).LoanRenew(loanPost, userData.UserId)) {
                //Trace the action - create a new record for log table
                Log log = new Log(userData.UserId, userData.Company_Id, loanPost.BranchId, loanPost.LoanId, "Renew Loan", "Renew Loan Number" + loanPost.LoanNumber + " , Renewal Date: " + loanPost.RenewalDate, DateTime.Now);

                int islog = (new LogAccess()).InsertLog(log);

                TempData["message"] = "success";
            }
            else {
                TempData["message"] = "fail";
            }

            return RedirectToAction("RenewLoan");
        }
    }
}