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
        public ActionResult UserDashBoard(Int32? id)
        {
            var dashBoardModel = new Models.DashBoard();

            var newDashDAL = new DAL.DashBoardAccess();
            if (!id.HasValue)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            if (id.HasValue) {

                ///get level id by userid
                int userLevelId = newDashDAL.GetUserLevelByUserId((int)id);

                dashBoardModel.userId = (int)id;

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
        public ActionResult EmployeeDashBoard(Int32? id)
        {
            var dashBoardModel = new Models.DashBoard();

            if (!id.HasValue)
            {
                return RedirectToAction("EmployeeLogin", "Login");
            }
            else
            {
                dashBoardModel.userId = id.Value;
                return View(dashBoardModel);
            }


        }

        /// <summary>
        /// Manage User Profiles 
        /// </summary>
        /// <param name="type">search users type
        /// type 1 =super admin
        /// type 2 = admin
        /// type 3 = user
        /// type 4 = profile
        /// </param>
        /// <returns>return view with type</returns>
        public ActionResult SearchUsers(int type)
        {
            if (type > 0)
            {
                return RedirectToAction("next", "next", new { id = type });
            }
            else {
                return RedirectToAction("UserDashBoard", "DashBoard");
            }
        }
    }
}