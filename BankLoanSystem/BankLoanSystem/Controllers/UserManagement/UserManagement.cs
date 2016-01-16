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

            UserManage obj1 = new UserManage();
            int role = obj1.getUserRole(idval.Value);
            if (typeval.HasValue && idval.HasValue) {

                var ret = obj1.getUserByType(typeval.Value, idval.Value);
                return View(ret);
            }
            else { return View(); }
            
        }
        public ActionResult Details(int id)
        {

            UserManage obj1 = new UserManage();
            if (id != 0)
            {
                var ret = obj1.getUserById(id);
                return View(ret);
            }
            else { return View(); }


        }


    }
}