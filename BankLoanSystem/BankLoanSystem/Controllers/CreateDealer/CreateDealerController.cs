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
            if(TempData["msg"] !=null )
            {
                if (TempData["msg"].ToString() == "1")
                {
                    ViewBag.SuccessMsg = "User Successfully Created";
                }else if(TempData["msg"].ToString() == "2")
                {
                    ViewBag.Error = "Error";
                }
            }
            CompanyAccess ca = new CompanyAccess();
            BranchAccess ba = new BranchAccess();
            Loan loan = new Loan();
            if (Session["oneLoanDashboard"] != null)
            {
                loan = (Loan)Session["oneLoanDashboard"];
            }
            else if (Session["loanDashboard"] != null)
            {
                loan = (Loan)Session["loanDashboard"];
            }
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
            user.Company_Id = userData.Company_Id;
            user.BranchId = userData.BranchId;
            user.RoleId = 4;
            user.Email = user.NewEmail;

            BranchAccess ba = new BranchAccess();
            Loan loan = new Loan();
            if (Session["oneLoanDashboard"] != null)
            {
                loan = (Loan)Session["oneLoanDashboard"];
                Session.Remove("oneLoanDashboard");
            }
            else if (Session["loanDashboard"] != null)
            {
                loan = (Loan)Session["loanDashboard"];
            }
            user.NonRegBranchId = loan.NonRegBranchId;

            // NonRegBranch nonRegBranches = ba.getNonRegBranchByNonRegBranchId(loan.NonRegBranchId);
            // user.NonRegCompanyId = nonRegBranches.NonRegCompanyId;
            // user.NonRegBranchId = nonRegBranches.BranchId;
            user.LoanId = loan.LoanId;

            string passwordTemp = user.Password;

            UserAccess ua = new UserAccess();

            string newSalt = PasswordEncryption.RandomString();
            user.Password = PasswordEncryption.encryptPassword(user.Password, newSalt);
            user.ActivationCode = Guid.NewGuid().ToString();

            //Insert user
            int newUserId = ua.InsertDealerUser(user);
           
            if (newUserId != 0)
            {

                                                        
                string body = "Hi " + user.FirstName + "! <br /><br /> Your account has been successfully created. Below in your account detail." +
                              "<br /><br /> User name: " + user.UserName +
                                    "<br /> Password : <b>" + passwordTemp +
                              "<br />Click <a href='http://localhost:57318/CreateUser/ConfirmAccount?userId=" + newUserId + "&activationCode=" + user.ActivationCode + "'>here</a> to activate your account." +
                              "<br /><br/> Thanks,<br /> Admin.";

                Email email = new Email(user.Email);

                Session["abcRol"] = user.RoleId;
                Session["abcBrnc"] = user.BranchId;
                email.SendMail(body, "Account details");


                TempData["msg"] = 1;
                return RedirectToAction("LinkDealer");               

            }
            else
            {
                TempData["msg"] = 2;
                return RedirectToAction("LinkDealer");                
            }
        }        
    }
}