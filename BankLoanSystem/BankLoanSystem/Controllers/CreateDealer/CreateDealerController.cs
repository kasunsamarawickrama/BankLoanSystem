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
            int loan_id = 179;            
            int nonRegBranchId = 2;
            
            NonRegBranch nonRegBranches = ba.getNonRegBranchByNonRegBranchId(nonRegBranchId);
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
            int loan_id = 179;
            int nonRegBranchId = 2;
            NonRegBranch nonRegBranches = ba.getNonRegBranchByNonRegBranchId(nonRegBranchId);

            user.NonRegCompanyId = nonRegBranches.NonRegCompanyId;
            user.NonRegBranchId = nonRegBranches.BranchId;
            user.LoanId = loan_id;

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

    }
}