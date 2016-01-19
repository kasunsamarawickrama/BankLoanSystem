using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

namespace BankLoanSystem.Controllers
{
    public class UserManagementController : Controller
    {
        // GET: UserManagement
        public ActionResult UserList()
        {

            int idval;
            int typeval;
            try
            {
                idval = (int)Session["id"];
                typeval = (int)Session["type"];
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404);
            }
            UserManageAccess obj1 = new UserManageAccess();
            int role = obj1.getUserRole(idval);
            if ((typeval > 0) && (idval > 0))
            {

                var ret = obj1.getUserByType(typeval, idval);
                return View(ret);
            }
            else { return View(); }

        }

        public ActionResult Detailsset(int id)
        {
            TempData["rowId"] = id;

            return RedirectToAction("Details", "UserManagement");
        }


        [HttpPost]
        public ActionResult UserList(int id, string clickType)
        {
            if (clickType.Equals("edit"))
            {
                Session["editId"] = id;
                return RedirectToAction("editUser", "ManageUsers");


            }

            return View();

        }
        public ActionResult Details()
        {
            int id;
            try
            {
                id = (int)TempData["rowId"];
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404);
            }
            UserManageAccess obj1 = new UserManageAccess();
            if (id != 0)
            {
                var ret = obj1.getUserById(id);
                return View(ret);
            }
            else { return View(); }


        }



        public ActionResult editUserSet(int editId)
        {

            // check if he is allowable to view/edit


            Session["editId"] = editId;
            return RedirectToAction("editUser");
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
            int currentUserId;
            int editUserId;

            try
            {
                currentUserId = int.Parse(Session["userId"].ToString());
                editUserId = int.Parse(Session["editId"].ToString());
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404);
            }
            if (currentUserId == editUserId) { ViewBag.isSame = true; }
            else { ViewBag.isSame = false; }


            int companyId;

            int typeval = (int)Session["type"];

            List<UserLogin> details = (new UserManageAccess()).getUserByType(typeval, currentUserId);

            bool isEditable = false;
            foreach (UserLogin user in details)
            {
                if(user.userId == editUserId && user.isEdit == true)
                {
                    isEditable = true;
                    break;
                }
            }
            if (!isEditable)
            {
                return new HttpStatusCodeResult(404);
            }

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


            TempData["editUserId"] = editUserId;
            return View(editUser);
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
            int currentUserId =(int) Session["userId"];
            int editUserId = (int)TempData["editUserId"];

            Session["editUserIds"] = editUserId;

            return RedirectToAction("EditRights", "EditRights");

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

            int currentUserId;
            int editUserId;

            try
            {
                currentUserId = int.Parse(Session["userId"].ToString());
                editUserId = int.Parse(Session["editId"].ToString());
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404);
            }

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

        public ActionResult PrevDelete(int id, int lId, int roleId)
        {
            TempData["delRowId"] = id;
            TempData["logId"] = lId;
            TempData["roleId"] = roleId;
            return RedirectToAction("Delete", "UserManagement");
        }

        public ActionResult Delete()
        {
            int id = (int)TempData["delRowId"];
            int lId = (int)TempData["logId"];
            int roleId = (int)TempData["roleId"];
            DashBoardAccess db = new DashBoardAccess();
            //int role = db.GetUserLevelByUserId(lId);
            UserManageAccess obj1 = new UserManageAccess();
            if (id != 0)
            {
                bool ret = obj1.deleteUser(id);
                if (ret)
                {
                    ViewBag.SuccessMsg = "User is successfully deleted";

                }
                else
                {
                    ViewBag.ErrorMsg = "Failed to delete user";
                }

            }

            return RedirectToAction("UserList", "UserManagement", new { typeval = roleId, idval = lId });


        }
    }
}