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
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/17
        /// 
        /// edit rights Set session variables
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult editRights(User user)
        {
            int currentUserId = 1;
            int editUserId = 3;

            TempData["userId"] = currentUserId;
            TempData["editUserId"] = editUserId;

            return RedirectToAction("EditRights","EditRights");

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
            int currentUserId = 1;
            int editUserId = 2;// edit user is hard coded
            if (currentUserId == editUserId) { ViewBag.isSame = true; }
            else { ViewBag.isSame = false; }


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

            int currentUserId = 1;
            int editUserId = 2;// edit user is hard coded

            bool isUpdate;
            if (currentUserId == editUserId)
            {// Update the data into database
                ViewBag.isSame = true;
                isUpdate = (new UserAccess()).updateProfileDetails(editUserId, user.UneditUserName, user.FirstName, user.LastName, user.Email, user.PhoneNumber, DateTime.Now, user.Password);
            }
            else
            {   // Update the data into database
                ViewBag.isSame = false;
                isUpdate = (new UserAccess()).updateUserDetails(editUserId, user.UneditUserName, user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.Status, user.BranchId, DateTime.Now, user.Password);
            }

            if (isUpdate)
                ViewBag.SuccessMsg = "Data Successfully Updated";
            else {
                ViewBag.ErrorMsg = "Updating failed";
                return View(user);
            }

            user = (new UserAccess()).retreiveUserByUserId(editUserId);
            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(user.Company_Id);

            // insert all branches into selectedlist
            List<SelectListItem> branchSelectLists = new List<SelectListItem>();
            foreach (Branch branch in branchesLists)
            {
                branchSelectLists.Add(new SelectListItem() { Text = branch.BranchName, Value = branch.BranchId.ToString() });

            }
            ViewBag.BranchId = new SelectList(branchSelectLists, "Value", "Text", user.BranchId);


            return View(user);
        }

    }
}