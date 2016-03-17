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
        // GET: Title
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TitleStatusUpdate()
        {
            return View();
        }
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate: 03/17/2016
        /// search titles by identification number
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns></returns>
        public ActionResult SearchTitle(string identificationNumber)
        {
            //string loanCode = Session["loanCode"].ToString();
            string loanCode = "fhfh";
            TitleAccess obj1 = new TitleAccess();
            TitleStatus obj2 = new TitleStatus();
            if ((!string.IsNullOrEmpty(identificationNumber))&&(!string.IsNullOrEmpty(loanCode)))
            {
                obj2.TitleList = obj1.SearchTitle(loanCode,identificationNumber);
                ViewBag.TitleList = obj2.TitleList;
            }
            return View(obj2);
        }
    }
}