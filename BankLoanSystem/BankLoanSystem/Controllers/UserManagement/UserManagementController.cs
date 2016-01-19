using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankLoanSystem.DAL;

namespace BankLoanSystem.Controllers
{
    public class UserManagementController : Controller
    {
        
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/13
        /// Get Method of UserList View
        /// </summary>
        /// <returns>UserList View</returns>
        public ActionResult UserList()
        {
            int idval = (int)Session["id"];
            int typeval = (int)Session["type"];
            UserManageAccess obj1 = new UserManageAccess();
            int role = obj1.getUserRole(idval);
            if ((typeval>0) && (idval>0)) {

                var ret = obj1.getUserByType(typeval, idval);
                return View(ret);
            }
            else { return View(); }
            
        }
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/18
        /// Post Method of UserList View
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns to Details Action</returns>
        public ActionResult Detailsset(int id)
        {
           
                try
                {
                    TempData["rowId"] = id;
                    return RedirectToAction("Details", "UserManagement");
                }
                catch(Exception ex)
                {
                    throw ex;
                }

        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/15
        /// Get Method of Details View
        /// </summary>
        /// <returns>Details View</returns>
        public ActionResult Details()
        {
            int id = (int)TempData["rowId"];
            UserManageAccess obj1 = new UserManageAccess();
            if (id != 0)
            {
                var ret = obj1.getUserById(id);
                return View(ret);
            }
            else { return View(); }


        }
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/18
        /// Post Method of Details View
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lId"></param>
        /// <param name="roleId"></param>
        /// <returns>Delete Action</returns>
        public ActionResult PrevDelete(int id, int lId, int roleId)
        {
            try
            {
                TempData["delRowId"] = id;
                TempData["logId"] = lId;
                TempData["roleId"] = roleId;
                return RedirectToAction("Delete", "UserManagement");
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/18
        /// Get Method of Delete
        /// </summary>
        /// <returns>UserList View</returns>
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