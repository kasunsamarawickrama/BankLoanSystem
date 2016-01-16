using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankLoanSystem.Models;
using BankLoanSystem.DAL;

namespace BankLoanSystem.Controllers.ManageUser
{
    public class EditRightsController : Controller
    {
        // GET: EditRights
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditRights(int? userId)
        {
            if (userId.HasValue)
            {
                List<Right> rights = (new UserRightsAccess()).getRights(userId.Value);

                return View();
            }
            else
            {
                return RedirectToAction("UserLogin", "Login");
            }                
        }
    }
}