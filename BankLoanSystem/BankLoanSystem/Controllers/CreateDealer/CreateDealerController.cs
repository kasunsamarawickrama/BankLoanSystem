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
        /// Frontend page: Join Dealer
        /// Title: view of Link Dealer page
        /// Designed: Nadeeka
        /// User story:
        /// Developed: Nadeeka
        /// Date created: 2016/03/30
        /// Edited: Piyumi P
        /// </summary>
        /// <returns></returns>
        public ActionResult LinkDealer()
        {
            //Check result of Join Dealer page
            if(TempData["msg"] !=null )
            {
                //Check TempData["msg"] is 1
                if (TempData["msg"].ToString() == "1")
                {
                    ViewBag.SuccessMsg = "User Successfully Created";
                }
                //Check TempData["msg"] is 2
                else if (TempData["msg"].ToString() == "2")
                {
                    ViewBag.Error = "Error";
                }
            }
            CompanyAccess ca = new CompanyAccess();
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
            //remove Session["popUpSelectionType"]
            Session.Remove("popUpSelectionType");
           //return non registered branch details by non registered branch id
            NonRegBranch nonRegBranches = ba.getNonRegBranchByNonRegBranchId(loan.NonRegBranchId);
            ViewBag.nonRegBranches = nonRegBranches.BranchName;
            ViewBag.nonRegCompany = nonRegBranches.CompanyNameBranchName;
            List<User> userList = new List<User>();
            //return all users of given company id
            userList = (new UserAccess()).GetUserListByCompany(userData.Company_Id);
            //filter user list who has authorization for selected loan
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
                    //Check dealer user can view the report
                    if(!rgt.DealerView)
                    {
                        continue;
                    }
                    else
                    {
                        //check title need not to be tracked for selected loan and report right for Title Status
                        if ((loan.IsTitleTrack == 0) && (rgt.rightId=="R04"))
                        {
                            //if title need not to be tracked report right for Title Status is not added to right list
                            continue;
                        }
                        //check there is no advance fee for selected loan and report right for advance fee invoice and advance fee receipt
                        if ((loan.AdvanceFee == 0) && ((rgt.rightId == "R07")||(rgt.rightId == "R08")))
                        {
                            //if there is no advance fee, report right for advance fee invoice and advance fee receipt are not added to right list
                            continue;
                        }
                        //check there is no monthly loan fee for selected loan and report right for monthly loan fee invoice and monthly loan fee receipt
                        if ((loan.MonthlyLoanFee == 0) && ((rgt.rightId == "R09") || (rgt.rightId == "R10")))
                        {
                            //if there is no monthly loan fee, report right for monthly loan fee invoice and monthly loan fee receipt are not added to right list
                            continue;
                        }
                        //check there is no lot inspection fee for selected loan and report right for lot inspection fee invoice and lot inspection fee receipt
                        if ((loan.LotInspectionFee == 0) && ((rgt.rightId == "R11") || (rgt.rightId == "R12")))
                        {
                            //if there is no lot inspection fee, report right for lot inspection fee invoice and lot inspection fee receipt are not added to right list
                            continue;
                        }
                    }
                    us.ReportRightsList.Add(rgt);
                }
            }
            //Check user is super admin
            if (userData.RoleId == 1)
            {
                //convert user list to session object
                Session["UserReqList"] = userList;
            }
            //Check user is admin
            else if (userData.RoleId == 2)
            {
                //convert user list to session object
                Session["UserReqList"] = userList;
            }
            else
            {
                //return to dashboard
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
         
                //Check whether a particular report right is given to user
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
                //insert log record after user is created
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

        /// <summary>
        /// Frontend page: Join Dealer
        /// Title: retrieve user email for selected user
        /// Designed:
        /// User story:
        /// Developed : Piyumi
        /// Date created: 05/26/2016
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserEmailByUserId(int userId)
        {
            User userObj = new User();
            //Check user id is not 0 and Session["UserReqList"] is not null
            if (userId > 0 && Session["UserReqList"]!=null)
            {
                List<User> userList = new List<User>();
                //convert session object to user list
                userList = (List<User>)Session["UserReqList"];
                foreach(User u in userList)
                {
                    //check each user id with selected user id
                    if (u.UserId == userId)
                    {
                        userObj = u;
                    }
                }
                //return user object which match with selected user
                return Json(userObj);
            }
            else
            {
                //return to login page
                return RedirectToAction("UserLogin", "Login");
            }
        }
    }
}