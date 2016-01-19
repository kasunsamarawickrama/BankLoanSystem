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
        public ActionResult CreateBranch()
        {
            var type = (string) Session["type"];
            if (type == "CompanyEmployee")
            {
                ViewBag.Type = "CompanyEmployee";
                _type = "CompanyEmployee";
                _userCompany = (UserCompanyModel)TempData["UserCompany"];
            }
            return View();
        }
        [HttpPost]
        [ActionName("CreateBranch")]
        public ActionResult CreateBranchPost(Branch branch)
        {
            if (_type == "CompanyEmployee")
            {
                BranchAccess ba = new BranchAccess();
                branch.BranchCode = ba.createBranchCode(_userCompany.Company.CompanyCode);
                CompanyAccess ca = new CompanyAccess();
                if (ca.SetupCompany(_userCompany, branch))
                {
                    ViewBag.SuccessMsg = "Company is successfully setup";
                    return View();
                }
                else
                {
                    ViewBag.SuccessMsg = "Failed to setup company";
                    return RedirectToAction("CreateFirstSuperUser", "CreateUser"); ;
                }
                
            }
            else
            {
                int id = (int)Session["id"];
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

        }
    }
}
