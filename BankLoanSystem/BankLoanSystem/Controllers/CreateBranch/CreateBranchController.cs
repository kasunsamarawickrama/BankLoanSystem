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
        // GET: CreateBranch
        public ActionResult CreateBranch()
        {
            return View();
        }
        [HttpPost]
        [ActionName("CreateBranch")]
        public ActionResult CreateBranchPost(Branch branch, int id)
        {
            //int id = 2;
            BranchAccess br = new BranchAccess();
            bool reslt = br.insertBranchDetails(branch, id);
            if (reslt)
            {
                return RedirectToAction("CreateBranch");
            }
            return View();

        }
    }
}
