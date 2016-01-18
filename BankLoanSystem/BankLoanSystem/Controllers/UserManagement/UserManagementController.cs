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
        public ActionResult UserList(int? typeval, int? idval)
        {

            UserManageAccess obj1 = new UserManageAccess();
            int role = obj1.getUserRole(idval.Value);
            if (typeval.HasValue && idval.HasValue) {

                var ret = obj1.getUserByType(typeval.Value, idval.Value);
                return View(ret);
            }
            else { return View(); }
            
        }
        public ActionResult Details(int id)
        {

            UserManageAccess obj1 = new UserManageAccess();
            if (id != 0)
            {
                var ret = obj1.getUserById(id);
                return View(ret);
            }
            else { return View(); }


        }
        public ActionResult Delete(int id,int lId,int roleId)
        {
            DashBoardAccess db = new DashBoardAccess();
            //int role = db.GetUserLevelByUserId(lId);
            UserManageAccess obj1 = new UserManageAccess();
            if (id != 0)
            {
                bool ret = obj1.deleteUser(id);
                if (ret)
                {
                    return RedirectToAction("UserList","UserManagement",new { typeval= roleId, idval = lId });
                }

            }
           
                return View();
           

        }

    }
}