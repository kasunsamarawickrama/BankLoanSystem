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

                        filterContext.Result = new HttpStatusCodeResult(404, "Session Expired");

                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("~/Login/UserLogin");

                    }
                    //return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });

                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
            }
        }



        // GET: LoanManagement
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult RenewLoan()
        {

            Session.Remove("popUpSelectionType");
            Loan loan = new Loan();
            if ((Session["loanDashboardRenewLoan"] != null) && (Session["loanDashboardRenewLoan"].ToString() != ""))
            {

                loan = (Loan)Session["loanDashboardRenewLoan"];
                

                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return PartialView(loan);
                }
                else
                {

                    return View(loan);
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

        [HttpPost]
        public ActionResult RenewLoan(Loan l) {



            return RedirectToAction("RenewLoan");
        }
    }
}