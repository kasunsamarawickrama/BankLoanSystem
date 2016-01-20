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
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// user login view
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogin(string lbl, string lbl3)
        {
            ViewBag.login = true;
            if (lbl3 != null)
            {
                ViewBag.SuccessMsg = lbl3;
            }
            if (lbl != null)
            {
                var loginlbl = new UserLogin();
                loginlbl.lbl = lbl;
                Session["userId"] = "";
                return View(loginlbl);
            }
            else {
                Session["userId"] = "";
                return View();
            }
        }

        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// employee login view
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeeLogin(string lbl)
        {
            ViewBag.login = true;
            if (lbl != null)
            {
                var loginlbl = new UserLogin();
                loginlbl.lbl = lbl;
                return View(loginlbl);
            }
            else {
                return View();
            }
        }

        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// User Login Controller
        /// </summary>
        /// <param name="user">login user details</param>
        /// <returns>rederect to user dashboard</returns>
        [HttpPost]
        public ActionResult UserLogin(UserLogin user)
        {
            var login = new LoginAccess();

            int userId = login.CheckUserLogin(user.userName, user.password);

            if (userId > 0)
            {
                Session["userId"] = userId;
                Session["rowId"] = userId;
                return RedirectToAction("Details", "UserManagement");
            }
            else {

                return RedirectToAction("UserLogin", "Login", new { lbl = "Incorrect username or password" });
            }
        }
        /// <summary>
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// user login controller
        /// </summary>
        /// <param name="user">login employee details</param>
        /// <returns>rederect to employee dashboard</returns>
        [HttpPost]
        public ActionResult EmployeeLogin(UserLogin user)
        {
            var login = new LoginAccess();

            int userId = login.CheckEmployeeLogin(user.userName, user.password);

            if (userId > 0)
            {
                Session["employeeId"] = userId;
                return RedirectToAction("EmployeeDetail", "DashBoard");
            }
            else {
                return RedirectToAction("EmployeeLogin", "Login", new { lbl = "Incorrect username or password" });
            }
        }

    }
}