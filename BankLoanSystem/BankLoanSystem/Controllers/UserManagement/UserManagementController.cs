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
        // GET: UserManagement
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
        public ActionResult Detailsset(int id)
        {
            TempData["rowId"]=id;

            return RedirectToAction("Details", "UserManagement");
        }

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