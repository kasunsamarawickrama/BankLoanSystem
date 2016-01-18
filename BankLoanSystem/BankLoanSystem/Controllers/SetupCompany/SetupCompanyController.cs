﻿using System.Collections.Generic;
using System.Web.Mvc;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

namespace BankLoanSystem.Controllers.SetupCompany
{
    public class SetupCompanyController : Controller
    {
        // GET: SetupCompany
        public ActionResult Setup()
        {
            var type = (string)Session["type"];
            //if (type == "CompanyEmployee")
            //{
                    
            //}
            CompanyAccess ca = new CompanyAccess();
            List<CompanyType> ctList = ca.GetAllCompanyType();
            ViewBag.TypeId = new SelectList(ctList, "TypeId", "TypeName");
            return View();
        }

        [HttpPost]
        public ActionResult Setup(Company company)
        {
            //if (type == "CompanyEmployee")
            //{

            //}
            //CompanyAccess ca = new CompanyAccess();
            //List<CompanyType> ctList = ca.GetAllCompanyType();
            //ViewBag.TypeId = new SelectList(ctList, "TypeId", "TypeName");
            //return View();

            //generate company code 
            GeneratesCode gc = new GeneratesCode();
            company.CompanyCode = gc.GenerateCompanyCode(company.CompanyName);

            TempData["Company"] = company;
            return RedirectToAction("CreateBranch", "CreateBranch", new {id = 0, type = "CompanyEmployee"});
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/16
        /// 
        /// Check weather user name already exist
        /// 
        /// argument: userName(string)
        /// 
        /// </summary>
        /// <returns>Return JsonResult</returns>
        public JsonResult IsCompanyNameExists(string companyName)
        {
            //check user name is already exist.  
            return Json((new CompanyAccess()).IsUniqueCompanyName(companyName), JsonRequestBehavior.AllowGet);
        }
    }
}