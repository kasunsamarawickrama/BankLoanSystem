using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers
{
    public class ManageUsersController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/13
        /// 
        /// Showing editable User using currentUserId and editableUserId
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult editUser()
        {
            int editUserId = 1;// edit user is hard coded
            int companyId;

            User editUser = (new UserAccess()).retreiveUserByUserId(editUserId);     //-- edit User id is hard coded
            companyId = editUser.Company_Id;
            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(companyId);

            // insert all branches into selectedlist
            List<SelectListItem> branchSelectLists = new List<SelectListItem>();
            foreach (Branch branch in branchesLists)
            {
                branchSelectLists.Add(new SelectListItem() { Text = branch.BranchName, Value = branch.BranchId.ToString() });

            }

            ViewBag.BranchId = new SelectList(branchSelectLists, "Value", "Text", editUser.BranchId);           
            
            return View(editUser);
        }

        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/13
        /// 
        /// updating the user detail
        /// 
        /// argument: User
        /// 
        /// </summary>
        /// <returns>update successful mesage / failed to update</returns>

        [HttpPost]
        public ActionResult editUser(User user)
        {

            int editUser = 1;// edit user is hard coded            

            // Update the data into database
            bool isUpdate = (new UserAccess()).updateUserDetails(editUser, user.UserName, user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.Status, user.BranchId, DateTime.Now,user.Password);

            if (isUpdate)
                ViewBag.SuccessMsg = "Data Successfully Updated";
            else { 
                ViewBag.ErrorMsg = "Updating failed";
                return View(user);
            }

            user = (new UserAccess()).retreiveUserByUserId(editUser);
            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(user.Company_Id);

            // insert all branches into selectedlist
            List<SelectListItem> branchSelectLists = new List<SelectListItem>();
            foreach (Branch branch in branchesLists) {
                branchSelectLists.Add(new SelectListItem() { Text = branch.BranchName, Value = branch.BranchId.ToString() });

            }


            ViewBag.BranchId = new SelectList(branchSelectLists, "Value", "Text", user.BranchId);

            
            return View(user);
        }


        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/16
        /// 
        /// Check the User name Exists or not in anywhere otherthan his own
        /// 
        /// argument: UserName
        /// 
        /// </summary>
        /// <returns>return json object with message</returns>
        public JsonResult IsUserExists(string UserName)
        {
            int userId = 1; // user id is hard coded
            //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  
            return Json(!(new UserAccess()).isUserNameExistsAnyElse(userId,UserName), JsonRequestBehavior.AllowGet);
        }
    }
}