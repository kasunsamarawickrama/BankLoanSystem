using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.CreateDealer
{
    public class CreateDealerController : Controller
    {
        User userData = new User();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if ((Session["AuthenticatedUser"] != null))
                {
                    userData = ((User)Session["AuthenticatedUser"]);                   
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Login/UserLogin");
                }
            }
            catch (Exception e)
            {
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
            }
        }

        // GET: Dealer
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2016/03/30
        /// 
        /// to show the view
        /// </summary>
        /// <returns></returns>
        public ActionResult LinkDealer()
        {            
            CompanyAccess ca = new CompanyAccess();
            BranchAccess ba = new BranchAccess();
            Loan loan = (Loan)Session["loanDashboard"];
            Session.Remove("popUpSelectionType");
            NonRegBranch nonRegBranches = ba.getNonRegBranchByNonRegBranchId(loan.NonRegBranchId);
            ViewBag.nonRegBranches = nonRegBranches.BranchName;// nonRegBranches.BranchName;
            ViewBag.nonRegCompany = nonRegBranches.CompanyNameBranchName;
            return View();
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2016/03/30
        /// 
        /// to show the view
        /// </summary>
        /// <returns></returns>
        public ActionResult LinkDealerr(string loanId, string nonRegBranch)
        {
            CompanyAccess ca = new CompanyAccess();
            Loan finalSelectedLoan = new Loan();
            finalSelectedLoan.LoanId = int.Parse(loanId);
            finalSelectedLoan.NonRegBranchId = int.Parse(nonRegBranch);
            Session["loanDashboard"] = finalSelectedLoan;


            BranchAccess ba = new BranchAccess();
            Loan loan = (Loan)Session["loanDashboard"];
            Session.Remove("popUpSelectionType");
            NonRegBranch nonRegBranches = ba.getNonRegBranchByNonRegBranchId(loan.NonRegBranchId);
            ViewBag.nonRegBranches = nonRegBranches.BranchName;// nonRegBranches.BranchName;
            ViewBag.nonRegCompany = nonRegBranches.CompanyNameBranchName;
            return View();
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2016/03/30
        /// 
        /// to insert user
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LinkDealer(DealerUserModel user)
        {
            user.PhoneNumber = user.PhoneNumber2;
            user.CreatedBy = userData.UserId;
            user.IsDelete = false;
            user.Status = false;
            user.Company_Id = userData.Company_Id;//  company.CompanyId;  - asanka
            user.BranchId = userData.BranchId;
            user.RoleId = 4;
            user.Email = user.NewEmail;

            BranchAccess ba = new BranchAccess();
            Loan loan = (Loan)Session["loanDashboard"];
          
            NonRegBranch nonRegBranches = ba.getNonRegBranchByNonRegBranchId(loan.NonRegBranchId);

            user.NonRegCompanyId = nonRegBranches.NonRegCompanyId;
            user.NonRegBranchId = nonRegBranches.BranchId;
            user.LoanId = loan.LoanId;

            string passwordTemp = user.Password;

            UserAccess ua = new UserAccess();

            string newSalt = PasswordEncryption.RandomString();
            user.Password = PasswordEncryption.encryptPassword(user.Password, newSalt);

            

           
            //Insert user
            int res = ua.InsertDealerUser(user);

            //Insert new user to user activation table
            string activationCode = Guid.NewGuid().ToString();
            int userId = (new UserAccess()).getUserId(user.Email);
            res = ua.InsertUserActivation(userId, activationCode);
            if (res == 1)
            {


                string body = "Hi " + user.FirstName + "! <br /><br /> Your account has been successfully created. Below in your account detail." +
                              "<br /><br /> User name: " + user.UserName +
                                    "<br /> Password : <b>" + passwordTemp +
                              "<br />Click <a href='http://localhost:57318/CreateUser/ConfirmAccount?userId=" + userId + "&activationCode=" + activationCode + "'>here</a> to activate your account." +
                              "<br /><br/> Thanks,<br /> Admin.";

                Email email = new Email(user.Email);

                Session["abcRol"] = user.RoleId;
                Session["abcBrnc"] = user.BranchId;
                email.SendMail(body, "Account details");



                ViewBag.SuccessMsg = "User Successfully Created";

                return PartialView();

            }
            else
            {
                ViewBag.ErrorMsg = "Failed to create user!";

                //Restrict to create above user role 
                RoleAccess ra = new RoleAccess();
                List<UserRole> roleList = ra.GetAllUserRoles();



                ViewBag.RoleId = new SelectList(roleList, "RoleId", "RoleName");



                User curUser = ua.retreiveUserByUserId(userId);
                // get all branches
                List<Branch> branchesLists = (new BranchAccess()).getBranches(curUser.Company_Id);
                ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");


                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return PartialView();
                }
                else
                {

                    return View();
                }
            }
        }

        //public ActionResult setLoanCode(string loanCode)
        //{
        //    //Session["loanCode"] = loanCode;
        //    if (loanCode == null)
        //    {
        //        return RedirectToAction("UserDetails");
        //    }
        //    LoanSelection list3 = (LoanSelection)Session["detail"];
        //    Loan finalSelectedLoan = new Loan();
        //    foreach (var l in list3.LoanList)
        //    {
        //        if (l.loanCode == loanCode)
        //        {

        //            finalSelectedLoan.NonRegBranchId = l.nonRegisteredBranchId;
        //            finalSelectedLoan.LoanId = l.loanId;
        //            finalSelectedLoan.LoanNumber = l.loanNumber;
        //            finalSelectedLoan.Rights = l.rightId.Split(',');
        //            //finalSelectedLoan.IsTitleTrack =

        //            foreach (var nrbr in list3.NonRegBranchList)
        //            {
        //                if (nrbr.NonRegBranchId == l.nonRegisteredBranchId)
        //                {
        //                    finalSelectedLoan.BranchId = nrbr.BranchId;

        //                    foreach (var br in list3.RegBranches)
        //                    {
        //                        if (br.BranchId == finalSelectedLoan.BranchId)
        //                        {
        //                            finalSelectedLoan.BranchName = br.BranchName;
        //                        }
        //                    }
        //                }
        //            }
        //            Session["loanDashboard"] = finalSelectedLoan;
        //        }
        //    }
        //    Session["detail"] = "";
        //    return RedirectToAction("LinkDealer");
        //}
    }
}