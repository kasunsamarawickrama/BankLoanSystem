using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankLoanSystem.Models;
using BankLoanSystem.DAL;

namespace BankLoanSystem.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// user login view
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogin()
        {
            return View();
        }

        /// <summary>
        /// employee login view
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeeLogin()
        {
            return View();
        }

        /// <summary>
        /// User Login Controller
        /// </summary>
        /// <param name="user">login user details</param>
        /// <returns>rederect to user dashboard</returns>
        [HttpPost]
        public ActionResult UserLogin(UserLogin user)
        {
            var login = new Login();

            int userId = login.CheckUserLogin(user.userName,user.password);

            if (userId > 0) {
                return RedirectToAction("UserDashBoard", "DashBoard", new { id = userId });
            }
            else {
                return RedirectToAction("UserLogin", "Login");
            }           
        }
        /// <summary>
        /// user login controller
        /// </summary>
        /// <param name="user">login employee details</param>
        /// <returns>rederect to employee dashboard</returns>
        [HttpPost]
        public ActionResult EmployeeLogin(UserLogin user)
        {
            var login = new Login();

            int userId = login.CheckEmployeeLogin(user.userName, user.password);

            if (userId > 0)
            {
                return RedirectToAction("EmployeeDashBoard", "DashBoard", new { id = userId });
            }
            else {
                return RedirectToAction("EmployeeLogin", "Login");
            }
        }

    }
}