﻿using System;
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
            int typeval=0;
            try
            {
                idval = (int)Session["userId"];
                
                if ((string)Session["searchType"] == "SuperAdmin")
                {
                    typeval = 1;
                }
                else if((string)Session["searchType"]== "Admin")
                {
                    typeval = 2;
                }
                else if ((string)Session["searchType"] == "User")
                {
                    typeval = 3;
                }

                UserManageAccess obj1 = new UserManageAccess();
                int role = obj1.getUserRole(idval);
                if ((typeval > 0) && (idval > 0))
                {

                    var ret = obj1.getUserByType(typeval, idval);
                    return View(ret);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404);
            }
           

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
            int logId;
            try
            {
                id = (int)TempData["rowId"];
                logId = (int)Session["userId"];
                UserManageAccess obj1 = new UserManageAccess();
                if (id != 0)
                {
                    var ret = obj1.getUserById(id);
                    
                    if (id == logId)
                    {
                        ViewBag.userId = ret.userId;
                        Session["editId"] = id;
                    }
                    else
                    {
                        ViewBag.userId = 0;
                    }
                    return View(ret);
                }
                else { return View(); }
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404);
            }
           
            

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

            int companyId;
            User editUser = (new UserAccess()).retreiveUserByUserId(editUserId);
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



            if (currentUserId == editUserId) { ViewBag.isSame = true;
                TempData["rowId"]= editUserId;

                return View(editUser);
            }
            else { ViewBag.isSame = false; }


            
            List<UserLogin> details;

            int typeval;


            try
            {
                typeval = (int)Session["searchtype"];
                details = (new UserManageAccess()).getUserByType(typeval, currentUserId);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404);
            }
            

            

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
            //int lId = (int)TempData["logId"];
            //int roleId = (int)TempData["roleId"];
            DashBoardAccess db = new DashBoardAccess();
            
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
            
            return RedirectToAction("UserList", "UserManagement");


        }
    }
}