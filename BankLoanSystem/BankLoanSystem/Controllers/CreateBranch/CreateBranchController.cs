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
        private static Company _company = null;
        // GET: CreateBranch
        public ActionResult CreateBranch(string type)
        {
            if (type == "CompanyEmployee")
            {
                ViewBag.Type = "CompanyEmployee";
                _type = "CompanyEmployee";
                _company = (Company)TempData["Company"];
            }
            return View();
        }
        [HttpPost]
        [ActionName("CreateBranch")]
        public ActionResult CreateBranchPost(Branch branch, int id)
        {
            if (_type == "CompanyEmployee")
            {
                


                CompanyBranchModel comBra = new CompanyBranchModel();
                comBra.Company = _company;
                comBra.MainBranch = branch;

                BranchAccess ba = new BranchAccess();
                comBra.MainBranch.BranchCode = ba.createBranchCode(comBra.Company.CompanyCode);
                TempData["CompanyMainBranch"] = comBra;
                return RedirectToAction("CreateFirstSuperUser", "CreateUser");
            }
            else
            {
                //int id = 2;
                BranchAccess br = new BranchAccess();
                bool reslt = br.insertBranchDetails(branch, id);
                if (reslt)
                {
                    return RedirectToAction("UserDashBoard", "DashBoard");
                }
                return View();
            }

        }
    }
}
