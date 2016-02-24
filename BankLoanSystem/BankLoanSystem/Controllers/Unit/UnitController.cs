using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System.Collections.Generic;
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


        public ActionResult LoanInfo( string title)
        {
            int userId = 57;
            int loanId = 184;
            ViewBag.Title = title;
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



        public ActionResult LoanPaymentDetails() {

            //int userId = 57;
            int loanId = 184;

            return PartialView((new UnitAccess()).GetLoanPaymentDetailsByLoanId(loanId));

        }

        
        public ActionResult GetLinkBar() {
            int userId = 57;

            var access = new UserRightsAccess();

            ///retrive all rights
            List<Right> rights = access.getRights();

            int userRole = (new UserManageAccess()).getUserRole(userId);

            if(userRole == 3)
            {
                ///get permission string for the relevent user
                List<Right> permissionString = access.getRightsString(userId);
                if (permissionString.Count == 1)
                {


                    string permission = permissionString[0].rightsPermissionString;
                    if (permission != "")
                    {
                        string[] charactors = permission.Split(',');

                        List<Right> temprights = new List<Right>();

                        foreach (var charactor in charactors)
                        {
                            foreach (var obj in rights)
                            {
                                if (string.Compare(obj.rightId, charactor) == 0)
                                {
                                    temprights.Add(obj);
                                    break;

                                }

                            }
                        }

                        rights = temprights;


                    }
                    else
                    {
                        rights = new List<Right>();
                    }
                  

                }

                else if (permissionString.Count == 0)
                {

                    rights = new List<Right>();
                }
              
             

            }

            return PartialView(rights);

        }



        public ActionResult GetJustAddedUnits()
        {
            int userId = 57;
            int loanId = 184;


            return PartialView((new UnitAccess().GetJustAddedUnitDetails(userId,loanId)));
        }
    }
}