using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using BankLoanSystem.Code;
using System.Text;

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
        public ActionResult Create(string lbls)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");


            if(lbls != null)
            {
                ViewBag.SuccessMsg = "User Successfully Created";
            }
            //int id = (int)Session["userId"];
            int id = Convert.ToInt32(Session["userId"].ToString());
            UserAccess ua = new UserAccess();
            User curUser = ua.retreiveUserByUserId(id);
            ViewBag.CurrUserRoleType = curUser.RoleId;

            //Restrict to create above user role 
            RoleAccess ra = new RoleAccess();
            List<UserRole> roleList = ra.GetAllUserRoles();
            List<UserRole> tempRoleList = new List<UserRole>();

            //if current user is first super admin he can create aditional super admin
            if (curUser.UserId == curUser.CreatedBy)
            {
                //ViewBag.RoleId = new SelectList(roleList, "RoleId", "RoleName");
                tempRoleList = roleList;
            }
            else
            {
                for (int i = 1; i < roleList.Count && ViewBag.CurrUserRoleType != 3; i++)
                {
                    UserRole tempRole = new UserRole()
                    {
                        RoleId = roleList[i].RoleId,
                        RoleName = roleList[i].RoleName
                    };
                    tempRoleList.Add(tempRole);
                }
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

            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");
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
                user.RoleId = 2;

            //Check branch is selected
            if (_curUserRoleId == 1 && user.BranchId == 0)
            {
                user.BranchId = _curBranchId;
            }
            string passwordTemp = user.Password;

            UserAccess ua = new UserAccess();

            string newSalt = PasswordEncryption.RandomString();
            user.Password = PasswordEncryption.encryptPassword(user.Password, newSalt);
            user.Email = user.NewEmail;

            //Insert user
            int res = ua.InsertUser(user);

            //Insert new user to user activation table
            string activationCode = Guid.NewGuid().ToString();
            int userId = (new UserAccess()).getUserId(user.Email);
            res = ua.InsertUserActivation(userId, activationCode);
            if (res == 1)
            {
                ViewBag.SuccessMsg = "Data Successfully inserted!";
                
                string body = "Hi " + user.FirstName + "! <br /><br /> Your account has been successfully created. Below in your account detail." +
                              "<br /><br /> User name: " + user.UserName +
                                    "<br /> Password : <b>" + passwordTemp +
                              "<br />Click <a href='http://localhost:57318/CreateUser/ConfirmAccount?userId=" + userId + "&activationCode=" + activationCode + "'>here</a> to activate your account." +
                              "<br /><br/> Thanks,<br /> Admin.";

                Email email = new Email(user.Email);
                email.SendMail(body, "Account details");

                
                // check the user as superadmin or admin..
                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    ViewBag.SuccessMsg = "User Successfully Created";
                    

                   
                    return RedirectToAction("create",new { lbls = ViewBag.SuccessMsg });
                }

                Session["editUserIds"] = userId;


                return RedirectToAction("SetRights", "EditRights", new {@lbl1 = ViewBag.SuccessMsg });
            }
            else
            {
                ViewBag.ErrorMsg = "Failed to create user!";

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
        /// CreatedDate: 2016/01/16
        /// 
        /// Check weather user name already exist
        /// 
        /// argument: userName(string)
        /// 
        /// </summary>
        /// <returns>Return JsonResult</returns>
        public JsonResult IsEmailExists(string newEmail)
        {
            //check user name is already exist.  
            return Json((new UserAccess()).IsUniqueEmail(newEmail), JsonRequestBehavior.AllowGet);
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
            //user.Password = PasswordEncryption.encryptPassword(user.Password);

            string newSalt = PasswordEncryption.RandomString();
            user.Password = PasswordEncryption.encryptPassword(user.Password, newSalt);
            user.Email = user.NewEmail;
            TempData["User"] = user;
            return RedirectToAction("Setup", "SetupCompany", new { id = 0, type = "CompanyEmployee" });
        }

        ///// <summary>
        ///// CreatedBy :  Kanishka SHM
        ///// CreatedDate: 2016/01/19
        ///// activated account
        /// 
        /// <param name="userId"></param>
        /// <param name="activationCode"></param>
        /// <returns>Return to view create first super admin</returns>
        public ActionResult ConfirmAccount(int userId, string activationCode)
        {
            UserAccess ua = new UserAccess();
            if (ua.UpdateUserSatus(userId, activationCode) == 1)
            {
                return View();
            }
            else
            {
                ViewBag.IsError = 1;
                ViewBag.SuccessMsg = "You have already activated your acount.";
                return View();
            }
        }
    }
}