using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

namespace BankLoanSystem.Controllers.CreateUser
{
    public class CreateUserController : Controller
    {
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
            //Hard corded
            int companyId = 2;
            int currUserRoleType = 1;

            //Restrict to create above user role 
            ViewBag.CompanyEmployee = true;

            RoleAccess ra = new RoleAccess();
            List<UserRole> roleList = ra.GetAllUserRoles();
            if (ViewBag.CompanyEmployee == false)
            {
                List<UserRole> tempRoleList = new List<UserRole>();

                for (int i = roleList[currUserRoleType - 1].RoleId; i <= roleList.Count && currUserRoleType != 3; i++)
                {
                    UserRole tempRole = new UserRole()
                    {
                        RoleId = roleList[i-1].RoleId,
                        RoleName = roleList[i-1].RoleName
                    };
                    tempRoleList.Add(tempRole);
                }

                //ViewBag.RoleId = new SelectList(roleList, "RoleId", "RoleName");
                ViewBag.RoleId = new SelectList(tempRoleList, "RoleId", "RoleName");
            }
            else
            {
                ViewBag.RoleId = new SelectList(roleList, "RoleId", "RoleName", roleList[0].RoleId);
            }

            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(companyId);
            ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");

            

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
            ViewBag.CompanyEmployee = false;
            if (ViewBag.CompanyEmployee == false)
            {
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
            }

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
    }
}