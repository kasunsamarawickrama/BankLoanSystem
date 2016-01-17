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
        public ActionResult Create(int id, string type)
        {
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

            //ViewBag.RoleId = new SelectList(roleList, "RoleId", "RoleName");
            ViewBag.RoleId = new SelectList(tempRoleList, "RoleId", "RoleName");


            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(curUser.Company_Id);

            //if current user is admin restrict to creat user for another branch
            if (ViewBag.CurrUserRoleType == 2)
            {
                ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName", curUser.BranchId);
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
            UserAccess ua = new UserAccess();
            int res = ua.InsertUser(user);

            if (res == 1)
            {
                ViewBag.SuccessMsg = "Data Successfully inserted!";
            }
            else
            {
                ViewBag.ErrorMsg = "Failed to insert!";
            }

            //Hard corded
            int companyId = 2;
            int currUserRoleType = 1;

            //Restrict to create above user role 
            RoleAccess ra = new RoleAccess();
            List<UserRole> roleList = ra.GetAllUserRoles();
            List<UserRole> tempRoleList = new List<UserRole>();

            for (int i = roleList[currUserRoleType - 1].RoleId; i <= roleList.Count && currUserRoleType != 3; i++)
            {
                UserRole tempRole = new UserRole()
                {
                    RoleId = roleList[i - 1].RoleId,
                    RoleName = roleList[i - 1].RoleName
                };
                tempRoleList.Add(tempRole);
            }

            //ViewBag.RoleId = new SelectList(roleList, "RoleId", "RoleName");
            ViewBag.RoleId = new SelectList(tempRoleList, "RoleId", "RoleName");

            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(companyId);
            ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");


            return View();
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
        /// 
        /// Create first super admin
        /// 
        /// argument: company(Company)
        /// 
        /// </summary>
        /// <returns>Return to view create first super admin</returns>
        public ActionResult CreateFirstSuperUser(Company company)
        {
            return View();
        }

    }
}