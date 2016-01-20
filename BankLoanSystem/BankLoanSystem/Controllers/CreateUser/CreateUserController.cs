using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

namespace BankLoanSystem.Controllers.CreateUser
{
    public class CreateUserController : Controller
    {
        private static int _createById;
        private static int _companyId;
        private static int _curUserRoleId;
        private static int _curBranchId;

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/16
        /// 
        /// Inserting user details
        /// 
        /// argument: None
        /// 
        /// </summary>
        /// <returns>Return view</returns>

        // GET: CreateUser
        public ActionResult Create()
        {
            if (Session["userId"] == null)
                return RedirectToAction("UserLogin", "Login");

            int id = (int) Session["userId"];
            UserAccess ua = new UserAccess();
            User curUser = ua.retreiveUserByUserId(id);
            ViewBag.CurrUserRoleType = curUser.RoleId;

            //Restrict to create above user role 
            RoleAccess ra = new RoleAccess();
            List<UserRole> roleList = ra.GetAllUserRoles();
            List<UserRole> tempRoleList = new List<UserRole>();

            for (int i = roleList[ViewBag.CurrUserRoleType - 1].RoleId; i <= roleList.Count && ViewBag.CurrUserRoleType != 3; i++)
            {
                UserRole tempRole = new UserRole()
                {
                    RoleId = roleList[i - 1].RoleId,
                    RoleName = roleList[i - 1].RoleName
                };
                tempRoleList.Add(tempRole);
            }
            _createById = curUser.UserId;
            _companyId = curUser.Company_Id;
            _curUserRoleId = curUser.RoleId;
            ViewBag.RoleId = new SelectList(tempRoleList, "RoleId", "RoleName");
            _curBranchId = curUser.BranchId;

            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(curUser.Company_Id);

            //if current user is admin restrict to creat user for another branch
            if (ViewBag.CurrUserRoleType == 2)
            {
                //ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName", curUser.BranchId);
                _curBranchId = curUser.BranchId;
            }
            else
            {
                ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");
            }

            return View();
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/16
        /// 
        /// Inserting user details
        /// 
        /// argument: User
        /// 
        /// </summary>
        /// <returns>Data Successfully inserted! / Failed to insert!</returns>
        [HttpPost]
        public ActionResult Create(User user)
        {
            user.CreatedBy = _createById;
            user.IsDelete = false;
            user.Status = false;

            //Set admin branch to new user 
            if (_curUserRoleId == 2)
            {
                user.BranchId = _curBranchId;
            }

            //Check role is selected
            if (user.RoleId == 0)
                user.RoleId = 1;

            //Check branch is selected
            if (_curUserRoleId == 1 && user.BranchId == 0)
            {
                user.BranchId = _curBranchId;
            }

            UserAccess ua = new UserAccess();
            int res = ua.InsertUser(user);

            if (res == 1)
            {
                ViewBag.SuccessMsg = "Data Successfully inserted!";
                int userId = (new UserAccess()).getUserId(user.Email);
                string body = "Hi " + user.FirstName + "! <br /><br /> Your account has been successfully created. Below in your account detail." +
                              "<br /><br /> User name: " + user.UserName +
                                    "<br /> Password : <b>" + user.Password +
                              "<br />Click <a href='http://localhost:57318/CreateUser/ConfirmAccount?userId=" + userId + "'>here</a> to activate your account." +
                              "<br /><br/> Thanks,<br /> Admin.";

                Email email = new Email(user.Email);
                email.SendMail(body, "Account details");

                Session["editUserIds"] = userId;
                return RedirectToAction("SetRights", "EditRights");
            }
            else
            {
                ViewBag.ErrorMsg = "Failed to insert!";

                //Restrict to create above user role 
                RoleAccess ra = new RoleAccess();
                List<UserRole> roleList = ra.GetAllUserRoles();
                List<UserRole> tempRoleList = new List<UserRole>();

                for (int i = roleList[_curUserRoleId - 1].RoleId; i <= roleList.Count && _curUserRoleId != 3; i++)
                {
                    UserRole tempRole = new UserRole()
                    {
                        RoleId = roleList[i - 1].RoleId,
                        RoleName = roleList[i - 1].RoleName
                    };
                    tempRoleList.Add(tempRole);
                }

                ViewBag.RoleId = new SelectList(tempRoleList, "RoleId", "RoleName");

                // get all branches
                List<Branch> branchesLists = (new BranchAccess()).getBranches(_companyId);
                ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");


                return View();
            }
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
        public JsonResult IsUserNameExists(string userName)
        {
            //check user name is already exist.  
            return Json((new UserAccess()).IsUniqueUserName(userName), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/17
        /// EditedDate:2016/01/19
        /// Create first super admin view
        /// 
        /// argument: None
        /// 
        /// </summary>
        /// <returns>Return to view create first super admin</returns>
        [HttpGet]
        public ActionResult CreateFirstSuperUser()
        {
            if (Session["type"] == null)
                return RedirectToAction("UserLogin", "Login");

            return View();
        }

        /// <summary>
        /// CreatedBy :  Kanishka SHM
        /// CreatedDate: 2016/01/18
        /// EditedDate:  2016/01/19
        /// Create first super admin
        /// 
        /// argument: user(User)
        /// 
        /// </summary>
        /// <returns>Return to view create first super admin</returns>
        [HttpPost]
        public ActionResult CreateFirstSuperUser(User user)
        {
            TempData["User"] = user;
            return RedirectToAction("Setup", "SetupCompany", new { id = 0, type = "CompanyEmployee" });
        }

        /// <summary>
        /// CreatedBy :  Kanishka SHM
        /// CreatedDate: 2016/01/19
        /// activated account
        /// 
        /// argument: userId(int)
        /// 
        /// </summary>
        /// <returns>Return to view create first super admin</returns>
        public ActionResult ConfirmAccount(int userId)
        {
            UserAccess ua = new UserAccess();
            if(ua.UpdateUserSatus(userId) == 1)
                return View();
            else
            {
                return null;
            }
        }
    }
}