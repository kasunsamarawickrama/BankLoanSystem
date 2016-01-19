using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankLoanSystem.Models;
using BankLoanSystem.DAL;

namespace BankLoanSystem.Controllers.DashBoard
{
    public class DashBoardController : Controller
    {


        /// <summary>
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// user dashboard view
        /// </summary>
        /// <param name="id">userid fromlogin page</param>
        /// <returns></returns>
        public ActionResult UserDashBoard()
        {
            ViewBag.login = false;
            var id = (int)Session["userId"];

            var dashBoardModel = new Models.DashBoard();

            var newDashDAL = new DashBoardAccess();
            if (id <= 0)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            if (id >0) {

                ///get level id by userid
                int userLevelId = newDashDAL.GetUserLevelByUserId(id);

                dashBoardModel.userId = id;

                if (userLevelId == 1) {

                    dashBoardModel.levelId = 1;
                    return View(dashBoardModel);

                } else if (userLevelId == 2) {

                    dashBoardModel.levelId = 2;
                    return View(dashBoardModel);

                } else if (userLevelId == 3) {

                    dashBoardModel.levelId = 3;
                    return View(dashBoardModel);
                }
                else {
                    return RedirectToAction("UserLogin", "Login");
                }
            }
            else {
                return RedirectToAction("UserLogin", "Login");
            }
        }

        /// <summary>
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// emplyee dashboard view
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeeDashBoard()
        {
            var id = (int)Session["employeeId"];
            var dashBoardModel = new Models.DashBoard();

            if (id <=0)
            {
                return RedirectToAction("EmployeeLogin", "Login");
            }
            else
            {
                dashBoardModel.userId = id;
                return View(dashBoardModel);
            }


        }

        /// <summary>
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// Manage User Profiles 
        /// </summary>
        /// <param name="type">search users type
        /// type 1 =super admin
        /// type 2 = admin
        /// type 3 = user
        /// type 4 = profile
        /// </param>
        /// <returns>return view with type</returns>
        public ActionResult SearchUsers(int index)
        {


            if (index == 1)
            {
                Session["type"] = "CompanyEmployee";

                return RedirectToAction("CreateFirstSuperUser", "CreateUser");
            }

            else if (index == 2)
            {
                Session["type"] = "";

                return RedirectToAction("Create", "CreateUser");
            }
            else if (index == 3)
            {
                Session["type"] = "";

                return RedirectToAction("CreateBranch", "CreateBranch");
            }

            if (index == 4 || index == 5 || index == 6)
            {
                if (index == 4)
                {

                    Session["searchType"] = "SuperAdmin";
                }
                else if (index == 5)
                {

                    Session["searchType"] = "Admin";
                }
                else
                {
                    Session["searchType"] = "User";
                }

                return RedirectToAction("UserList", "UserManagement");
            }

            else {
                return RedirectToAction("UserDashBoard", "DashBoard");
            }
        }
    }
}