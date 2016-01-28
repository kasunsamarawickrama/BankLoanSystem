﻿using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.CreateBranch
{
    public class CreateBranchController : Controller
    {
        private static string _type = "";
        private static UserCompanyModel _userCompany = null;
        // GET: CreateBranch
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateBranch()
        {
            //Get states to list
            CompanyAccess ca = new CompanyAccess();
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");
            return View();
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/17
        /// insert branch details
        /// </summary>
        /// <param name="branch"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("CreateBranch")]
        public ActionResult CreateBranchPost(Branch branch)
        {
            ViewBag.Type = "";
            if (Session["userId"] == null || Session["userId"].ToString() == "")
            {
                return RedirectToAction("UserLogin", "Login");
            }

            int id = (int)Session["userId"];
            //branch.StateId = branch.StateId2;
            BranchAccess br = new BranchAccess();
            bool reslt = br.insertBranchDetails(branch, id);

            CompanyAccess ca = new CompanyAccess();
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            if (reslt)
            {
                ViewBag.SuccessMsg = "Branch is successfully added";
                //return RedirectToAction("CreateBranch", "CreateBranch");
            }
            else
            {
                ViewBag.ErrorMsg = "Failed to add branch";

            }
            return View();

        }

        /// <summary>
        /// CreatedBy: Kanishka
        /// CreatedDate:2016/1/18
        /// insert first branch details
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateBranchFirstBranch()
        {
            var type = (string)Session["type"];
            if (type == "CompanyEmployee")
            {
                ViewBag.Type = "CompanyEmployee";
                _type = "CompanyEmployee";

                if (TempData["UserCompany"] == null) return RedirectToAction("EmployeeLogin", "Login");

                CompanyAccess ca = new CompanyAccess();
                //Get states to list
                List<State> stateList = ca.GetAllStates();
                ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

                _userCompany = (UserCompanyModel) TempData["UserCompany"];
                _userCompany.Branch = new Branch();

                return View(_userCompany);
            }
            else
            {
                ViewBag.Type = "";
                return RedirectToAction("EmployeeLogin", "Login");
            }
            
        }

        /// <summary>
        /// CreatedBy: Kanishka
        /// CreatedDate:2016/1/18
        /// 
        /// </summary>
        /// <param name="userCompany"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateBranchFirstBranch(UserCompanyModel userCompany)
        {
            if (_type == "CompanyEmployee")
            {
                ViewBag.Type = "CompanyEmployee";
                BranchAccess ba = new BranchAccess();
                userCompany.Branch.BranchCode = ba.createBranchCode(_userCompany.Company.CompanyCode);
                _userCompany.Branch = userCompany.Branch;
                _userCompany.Branch.StateId = userCompany.StateId;
                CompanyAccess ca = new CompanyAccess();
                //if (ca.SetupCompany(_userCompany))
                if (ca.SetupCompanyRollback(_userCompany))
                {
                    ViewBag.SuccessMsg = "Company is successfully setup";
                    return View();
                }
                else
                {
                    ViewBag.ErrorMsg = "Failed to setup company";
                    return RedirectToAction("CreateFirstSuperUser", "CreateUser");
                }

            }
            return View();
        }
    }
}
