using BankLoanSystem.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.DashBoard
{
    public class EmployeeDashBoardController : Controller
    {
        // GET: EmployeeDashBoard
        public ActionResult EmployeeDetail()
        {
            return View();
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

            if (Session["employeeId"] == null)
            {
                return RedirectToAction("EmployeeLogin", "Login");
            }
            var id = (int)Session["employeeId"];

            var dashBoardModel = new Models.DashBoard();
            dashBoardModel.userName = (new UserAccess().getCompanyEmployeeName(id));
            if (id <= 0)
            {
                return RedirectToAction("EmployeeLogin", "Login");
            }
            else
            {
                dashBoardModel.userId = id;
                return PartialView("~/Views/Shared/_EmployeeDetail.cshtml", dashBoardModel);
            }


        }
    }
}