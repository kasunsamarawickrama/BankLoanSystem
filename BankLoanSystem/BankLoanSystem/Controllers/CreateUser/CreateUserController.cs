using System.Collections.Generic;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

namespace BankLoanSystem.Controllers.CreateUser
{
    public class CreateUserController : Controller
    {
        // GET: CreateUser
        public ActionResult Create()
        {
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
                        RoleId = roleList[i-1].RoleId,
                        RoleName = roleList[i-1].RoleName
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

        [HttpPost]
        public ActionResult Create(User user)
        {
            UserAccess ua = new UserAccess();
            ua.InsertUser(user);



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


        public JsonResult IsUserNameExists(string userName)
        {
            //check user name is already exist.  
            return Json((new UserAccess()).IsUniqueUserName(userName), JsonRequestBehavior.AllowGet);
        }
    }
}