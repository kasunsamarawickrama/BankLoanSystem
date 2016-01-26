using System.Collections.Generic;
using System.Web.Mvc;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

namespace BankLoanSystem.Controllers.SetupCompany
{
    public class SetupCompanyController : Controller
    {
        private static User _user = null;

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/18
        /// 
        /// Create company view
        /// </summary>
        /// <returns></returns>
        // GET: SetupCompany
        public ActionResult Setup()
        {
            if(Session["type"] == null)
                return RedirectToAction("UserLogin", "Login");
            var type = (string)Session["type"];
            //if (type == "CompanyEmployee")
            //{

            //}
            _user = (User) TempData["User"];
            CompanyAccess ca = new CompanyAccess();
            List<CompanyType> ctList = ca.GetAllCompanyType();
            ViewBag.TypeId = new SelectList(ctList, "TypeId", "TypeName");



            return View();
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/18
        /// 
        /// Get company details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Setup(Company company)
        {
            //if (type == "CompanyEmployee")
            //{

            //}

            GeneratesCode gc = new GeneratesCode();
            company.CompanyCode = gc.GenerateCompanyCode(company.CompanyName);

            UserCompanyModel userCom = new UserCompanyModel();
            userCom.User = _user;
            userCom.Company = company;

            TempData["UserCompany"] = userCom;
            return RedirectToAction("CreateBranchFirstBranch", "CreateBranch", new {id = 0, type = "CompanyEmployee"});
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