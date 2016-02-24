using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.Unit
{
    public class UnitController : Controller
    {
        // GET: AddUnit
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AddUnit()
        {
            Session["UserId"] = 64;

            if (string.IsNullOrEmpty(Session["UserId"].ToString()))
                RedirectToAction("UserLogin", "Login");



            return View();
        }


        public ActionResult LoanInfo()
        {
            int userId = 57;
            int loanId = 184;

            User user = (new UserAccess()).retreiveUserByUserId(userId);
            ViewBag.Username = user.UserName;
            BranchAccess ba = new BranchAccess();
            ViewBag.Branch = (ba.getBranchByBranchId(user.BranchId)).BranchName;
            ViewBag.roleId = user.RoleId;
            // get the Company type for front end view
            int comType = ba.getCompanyTypeByUserId(userId);
            ViewBag.loanCompanyType = (comType == 1) ?   "Dealer" : "Lender" ;

            LoanSetupStep1 loanSetupStep1 = (new LoanSetupAccess()).GetLoanStepOne(loanId);
            NonRegBranch nonRegBranch = ba.getNonRegBranchByNonRegBranchId(loanSetupStep1.nonRegisteredBranchId);
            ViewBag.loanBranchAddress = nonRegBranch.BranchName + " - " + (nonRegBranch.BranchAddress1 != "" ? nonRegBranch.BranchAddress1:"" ) + (nonRegBranch.BranchAddress2 != "" ? "," + nonRegBranch.BranchAddress2 : "")+ (nonRegBranch.BranchCity != "" ? "," + nonRegBranch.BranchCity : "");

            ViewBag.LoanNumber = loanSetupStep1.loanNumber;
            return View();
        }

        [HttpPost]
        [ActionName("AddUnit")]
        public ActionResult AddUnitPost()
        {
            return View();
        }
    }
}