using BankLoanSystem.DAL;
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
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="branch"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("CreateBranch")]
        public ActionResult CreateBranchPost(Branch branch)
        {
            ViewBag.Type = "";
            int id = (int) Session["userId"];
            BranchAccess br = new BranchAccess();
            bool reslt = br.insertBranchDetails(branch, id);
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


        public ActionResult CreateBranchFirstBranch()
        {
            var type = (string) Session["type"];
            if (type == "CompanyEmployee")
            {
                ViewBag.Type = "CompanyEmployee";
                _type = "CompanyEmployee";
                
                _userCompany = (UserCompanyModel) TempData["UserCompany"];
                _userCompany.Branch = new Branch();
            }
            else
                ViewBag.Type = "";
            return View(_userCompany);
        }

        [HttpPost]
        public ActionResult CreateBranchFirstBranch(UserCompanyModel userCompany)
        {
            if (_type == "CompanyEmployee")
            {
                ViewBag.Type = "CompanyEmployee";
                BranchAccess ba = new BranchAccess();
                userCompany.Branch.BranchCode = ba.createBranchCode(_userCompany.Company.CompanyCode);
                _userCompany.Branch = userCompany.Branch;
                CompanyAccess ca = new CompanyAccess();
                if (ca.SetupCompany(_userCompany))
                {
                    ViewBag.SuccessMsg = "Company is successfully setup";
                    return View();
                }
                else
                {
                    ViewBag.SuccessMsg = "Failed to setup company";
                    return RedirectToAction("CreateFirstSuperUser", "CreateUser");
                }

            }
            return View();
        }
    }
}
