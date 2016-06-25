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
                    filterContext.Result = new RedirectResult("/Login/UserLogin?lbl=Due to inactivity your session has timed out, please log in again.");
                }
            }
            catch (Exception e)
            {
                filterContext.Result = new RedirectResult("~/Exceptions/Index");
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
           if (Session["loanDashboardJoinDealer"] != null)
            {
                loan = (Loan)Session["loanDashboardJoinDealer"];
            }
            Session.Remove("popUpSelectionType");
           
            NonRegBranch nonRegBranches = ba.getNonRegBranchByNonRegBranchId(loan.NonRegBranchId);
            ViewBag.nonRegBranches = nonRegBranches.BranchName;// nonRegBranches.BranchName;
            ViewBag.nonRegCompany = nonRegBranches.CompanyNameBranchName;
            List<User> userList = new List<User>();
            userList = (new UserAccess()).GetUserListByCompany(userData.Company_Id);
            userList = userList.FindAll(t => t.BranchId == loan.BranchId || (t.BranchId ==0 && t.RoleId==1));
            ViewBag.UserIdForSendReq = new SelectList(userList, "UserId", "UserName");

            //get report list for dealer user
            List<Right> ReportRightsList = new List<Right>();
            User us = new User();
            us.ReportRightsList = new List<Right>();
            ReportRightsList = (new UserRightsAccess()).getReportRights();
            if(ReportRightsList!=null && ReportRightsList.Count > 0)
            {
                foreach (Right rgt in ReportRightsList)
                {
                    if((rgt.rightId=="RP001")|| (rgt.rightId == "RP017") || (rgt.rightId == "RP018") || (rgt.rightId == "RP018"))
                    {
                        continue;
                    }
                    us.ReportRightsList.Add(rgt);
                }
            }
            if (userData.RoleId == 1)
            {
                //ViewBag.UserIdForSendReq = new SelectList(userList, "UserId", "UserName");
                Session["UserReqList"] = userList;
            }
            else if (userData.RoleId == 2)
            {
                
                //ViewBag.UserIdForSendReq = new SelectList(userList, "UserId", "UserName");
                Session["UserReqList"] = userList;
            }
            else
            {
                return RedirectToAction("UserDetails", "UserManagement");
            }
            return View(us);
        }


        /// <summary>
        /// Frontend page: Join Dealer
        /// Title: Insert dealer user details
        /// Designed:Nadeeka
        /// User story:
        /// Developed : Nadeeka
        /// Date created: 03/30/2016
        /// Edited: Piyumi
        /// Date edited: 06/24/2016
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LinkDealer(DealerUserModel user)
        {
            //initialize properties of user object
            user.PhoneNumber = user.PhoneNumber2;
            user.CreatedBy = userData.UserId;
            user.IsDelete = false;
            user.Status = true;
            user.Company_Id = userData.Company_Id;
            user.BranchId = userData.BranchId;
            user.RoleId = 4;
            user.Email = user.NewEmail;

            BranchAccess ba = new BranchAccess();
            Loan loan = new Loan();
            //Check Session["oneLoanDashboard"] is not null
            if (Session["oneLoanDashboard"] != null)
            {
                //convert session object to loan object
                loan = (Loan)Session["oneLoanDashboard"];
                
            }
            //Check Session["loanDashboardJoinDealer"] is not null
            if (Session["loanDashboardJoinDealer"] != null)
            {
                //convert session object to loan object
                loan = (Loan)Session["loanDashboardJoinDealer"];
            }
            //initialize non registered branch id
            user.NonRegBranchId = loan.NonRegBranchId;
            //initialize loan id
            user.LoanId = loan.LoanId;
            //encrypt given password
            string passwordTemp = user.Password;

            UserAccess ua = new UserAccess();

            string newSalt = PasswordEncryption.RandomString();
            user.Password = PasswordEncryption.encryptPassword(user.Password, newSalt);
            user.ActivationCode = Guid.NewGuid().ToString();
            //report rights
            string[] arrList = new string[user.ReportRightsList.Count];
            int k = 0;
            foreach (var y in user.ReportRightsList)
            {
                if (y.active)
                {
                    arrList[k] = y.rightId;
                    k++;
                }
            }
            arrList = arrList.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            user.ReportRights = string.Join(",", arrList);
            //Insert dealer user details and retrieve user id of inserted user
            int newUserId = ua.InsertDealerUser(user);
           //Check user id is not 0
            if (newUserId != 0)
            {


                //Generate email to send username and password to created dealer user                                        
                string body = "Hi " + user.FirstName + "! <br /><br /> Your account has been successfully created. Below in your account detail." +
                              "<br /><br /> User name: " + user.UserName +
                                    "<br /> Password : <b>" + passwordTemp +
                              "<br />Click <a href='www.dfpso.com'>here</a> to activate your account." +
                              "<br /><br/> Thanks,<br /> Admin.";

                Email email = new Email(user.Email);

                Session["abcRol"] = user.RoleId;
                Session["abcBrnc"] = user.BranchId;
                email.SendMail(body, "Account details");

                Log log = new Log(userData.UserId, userData.Company_Id, user.BranchId, user.LoanId, "Create Dealer Account", "Inserted Dealer : " + user.UserName, DateTime.Now);

                int islog = (new LogAccess()).InsertLog(log);

                TempData["msg"] = 1;
                return RedirectToAction("LinkDealer");               

            }
            else
            {
                TempData["msg"] = 2;
                return RedirectToAction("LinkDealer");                
            }
        }

        [HttpPost]
        public ActionResult GetUserEmailByUserId(int userId)
        {
            User userObj = new User();
            if (userId > 0 && Session["UserReqList"]!=null)
            {
                List<User> userList = new List<User>();
                userList = (List<User>)Session["UserReqList"];
                foreach(User u in userList)
                {
                    if (u.UserId == userId)
                    {
                        userObj = u;
                    }
                }
                return Json(userObj);
            }
            else
            {
                return RedirectToAction("UserLogin", "Login");
            }
        }
    }
}