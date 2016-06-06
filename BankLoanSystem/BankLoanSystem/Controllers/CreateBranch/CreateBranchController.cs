using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.CreateBranch
{
    public class CreateBranchController : Controller
    {
        private static string _type = "";
        private static UserCompanyModel _userCompany = null;
        User userData = new User();

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
                    //return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
                    //filterContext.Controller.TempData.Add("UserLogin", "Login");
                    filterContext.Result = new RedirectResult("~/Login/UserLogin");
                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
            }
        }

        // GET: CreateBranch
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateBranch(int success = 0)
        {

            if(success == 1)
            {
                ViewBag.SuccessMsg = "Branch is successfully added";
            }
            //Get states to list
            CompanyAccess ca = new CompanyAccess();
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");
            return PartialView();
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

            int id = userData.UserId;
            //branch.StateId = branch.StateId2;
            BranchAccess br = new BranchAccess();
            //int reslt = br.insertBranchDetails(branch, id);
            int reslt = 0;
            CompanyAccess ca = new CompanyAccess();
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            if (reslt>0)
            {
                ViewBag.SuccessMsg = "Branch is successfully added";
                return RedirectToAction("CreateBranch", "CreateBranch", new {success = 1 });
                //branch = new Branch();
                //return PartialView(branch);
            }
            else
            {
                ViewBag.ErrorMsg = "Failed to add branch";
                return PartialView();
            }
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
