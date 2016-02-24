using System;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.Unit
{
    public class UnitController : Controller
    {
        private static LoanSetupStep1 _loan;

        public ActionResult AddUnit()
        {
            Session["userId"] = 64;
            int loanId = 184;

            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanStepOne(loanId);

            ViewBag.loanDetails = loanDetails;
            Models.Unit unit = new Models.Unit();

            if (string.IsNullOrEmpty(Session["userId"].ToString()))
                RedirectToAction("UserLogin", "Login");

            int userId = Convert.ToInt16(Session["userId"]);

            CurtailmentAccess curAccess = new CurtailmentAccess();
            _loan = curAccess.GetLoanDetailsByLoanId(loanId);
            _loan.loanId = loanId;
            unit.AdvancePt = _loan.advancePercentage;
            unit.LoanId = loanId;
            unit.LoanAmount = _loan.loanAmount;
            unit.StartDate = _loan.startDate;
            unit.EndDate = _loan.maturityDate;

            ViewBag.Editable = _loan.isEditAllowable ? "Yes" : "No";

            return PartialView(unit);
        }


        [HttpPost]
        [ActionName("AddUnit")]
        public ActionResult AddUnitPost(Models.Unit unit, string btnAdd)
        {
            if (string.IsNullOrEmpty(Session["userId"].ToString()))
                RedirectToAction("UserLogin", "Login");

            if (string.IsNullOrEmpty(Session["UserId"].ToString()))
                RedirectToAction("UserLogin", "Login");

            switch (btnAdd)
            {
                case "Add":
                    unit.IsAdvanced = false;
                    unit.AddAndAdvance = false;
                    break;
                case "Add and Advance":
                    unit.IsAdvanced = true;
                    unit.AddAndAdvance = true;
                    break;
        }
            var userId = (int)Session["userId"];
            UnitAccess ua = new UnitAccess();
            var res = ua.InsertUnit(unit, userId, _loan.loanNumber);

            return RedirectToAction("AddUnit");
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
            ViewBag.loanCompanyType = (comType == 1) ? "Dealer" : "Lender";

            LoanSetupStep1 loanSetupStep1 = (new LoanSetupAccess()).GetLoanStepOne(loanId);
            NonRegBranch nonRegBranch = ba.getNonRegBranchByNonRegBranchId(loanSetupStep1.nonRegisteredBranchId);
            ViewBag.loanBranchAddress = nonRegBranch.BranchName + " - " + (nonRegBranch.BranchAddress1 != "" ? nonRegBranch.BranchAddress1 : "") + (nonRegBranch.BranchAddress2 != "" ? "," + nonRegBranch.BranchAddress2 : "") + (nonRegBranch.BranchCity != "" ? "," + nonRegBranch.BranchCity : "");

            ViewBag.LoanNumber = loanSetupStep1.loanNumber;
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
    }
}