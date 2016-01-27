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
        /// 
        /// Updated: kasun
        /// Reason : to add step processes
        /// 
        /// step1 => company setup -> type 1
        /// step2 => create branch
        /// step3 => create Admin/user
        /// step4 => company setup -> type 2
        /// step5 => create branch
        /// step6 => loan setup
        /// 
        /// </summary>
        /// <param name="user">login user details</param>
        /// <returns>rederect to user dashboard</returns>
        [HttpPost]
        public ActionResult UserLogin(UserLogin user)
        {
            var login = new LoginAccess();
            var step = new StepAccess();

            int userId = login.CheckUserLogin(user.userName, user.password);

            if (userId > 0)
            {
                ///check wether the company setup is ongoing 
                if (step.checkUserLoginWhileCompanySetup(userId))
                {

                    Session["userId"] = userId;
                    ///get the step nomber if the user is in company setup process
                    int stepNo = step.getStepNumberByUserId(userId);

                    if (stepNo == 1)
                    {
                        return RedirectToAction("Step1", "SetupProcess");
                    }
                    else if (stepNo == 2)
                    {
                        CompanyAccess ca = new CompanyAccess();
                        Company company = ca.GetCompanyDetailsByFirstSpUserId(userId);
                        CompanyBranchModel comBranch = new CompanyBranchModel();
                        comBranch.Company = company;
                        TempData["Company"] = comBranch;
                        return RedirectToAction("Step2", "SetupProcess");
                    }
                    else if (stepNo == 3)
                    {
                        return RedirectToAction("Step3", "SetupProcess");
                    }
                    else if (stepNo == 4)
                    {
                        return RedirectToAction("Step4", "SetupProcess");
                    }
                    else if (stepNo == 5)
                    {
                        return RedirectToAction("Step5", "SetupProcess");
                    }
                    else if (stepNo == 6)
                    {
                        return RedirectToAction("Step6", "SetupProcess");
                    }
                    else {
                        Session["rowId"] = userId;
                        return RedirectToAction("Details", "UserManagement");
                    }
                }
                else {
                    return RedirectToAction("UserLogin", "Login", new { lbl = "Company Setup is on going Please Contact Admin" });
                }
                
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