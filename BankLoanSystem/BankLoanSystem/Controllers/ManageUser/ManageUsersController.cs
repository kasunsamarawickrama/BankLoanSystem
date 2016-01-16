using BankLoanSystemTFN.DAL;
using BankLoanSystemTFN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystemTFN.Controllers
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

            UserModel editUser = (new User()).retreiveUserByUserId(editUserId);     //-- edit User id is hard coded
            companyId = editUser.Company_Id;
            // get all branches
            List<BranchModel> branchesLists = (new Branch()).getBranches(companyId);

            // insert all branches into selectedlist
            List<SelectListItem> branchSelectLists = new List<SelectListItem>();
            foreach (BranchModel branch in branchesLists)
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
        public ActionResult editUser(UserModel user)
        {

            int editUser = 1;// edit user is hard coded
            


            // Update the data into database
            bool isUpdate = (new User()).updateUserDetails(editUser, user.UserName, user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.Status, user.BranchId, DateTime.Now,user.Password);

            if (isUpdate)
                ViewBag.SuccessMsg = "Data Successfully Updated";
            else { 
                ViewBag.ErrorMsg = "Updating failed";
                return View(user);
            }

            user = (new User()).retreiveUserByUserId(editUser);
            // get all branches
            List<BranchModel> branchesLists = (new Branch()).getBranches(user.Company_Id);

            // insert all branches into selectedlist
            List<SelectListItem> branchSelectLists = new List<SelectListItem>();
            foreach (BranchModel branch in branchesLists) {
                branchSelectLists.Add(new SelectListItem() { Text = branch.BranchName, Value = branch.BranchId.ToString() });

            }


            ViewBag.BranchId = new SelectList(branchSelectLists, "Value", "Text", user.BranchId);

            
            return View(user);
        }
    }
}