using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

namespace BankLoanSystem.Controllers.DashBoard
{
    public class DashBoardController : Controller
    {

        User userData = new User();
        /// <summary>
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// user dashboard view
        /// </summary>
        /// <param name="id">userid fromlogin page</param>
        /// <returns></returns>

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["AuthenticatedUser"] != null)
            {
                try
                {
                    userData = ((User)Session["AuthenticatedUser"]);
                }
                catch
                {
                    filterContext.Result = new RedirectResult("~/Login/UserLogin");
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
                //return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
            }
        }

        public ActionResult UserDashBoard()
        {
            ViewBag.login = false;

            var id = userData.UserId;

            var dashBoardModel = new Models.DashBoard();

            var newDashDAL = new DashBoardAccess();

            if (id > 0)
            {

                ///get level id by userid
                int userLevelId = newDashDAL.GetUserLevelByUserId(id);

                dashBoardModel.userId = id;
                dashBoardModel.userName = userData.UserName;
                dashBoardModel.roleName = (new UserManageAccess()).getUserRoleName(id);
                if (userLevelId == 1)
                {

                    dashBoardModel.levelId = 1;
                    return PartialView("~/Views/Shared/_UserDetail.cshtml", dashBoardModel);

                }
                else if (userLevelId == 2)
                {

                    dashBoardModel.levelId = 2;
                    return PartialView("~/Views/Shared/_UserDetail.cshtml", dashBoardModel);

                }
                else if (userLevelId == 3)
                {

                    dashBoardModel.levelId = 3;
                    return PartialView("~/Views/Shared/_UserDetail.cshtml", dashBoardModel);
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

        /// <summary>
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// Manage User Profiles 
        /// </summary>
        /// <param name="type">search users type
        /// index 1 =super admin
        /// type 2 = admin
        /// type 3 = user
        /// type 4 = profile
        /// </param>
        /// <returns>return view with type</returns>
        public ActionResult SearchUsers(int index)
        {


            if (Session["userId"] == null)
            {
                return new HttpStatusCodeResult(404);
            }

            if (index == 1)
            {
                Session["type"] = "";
                Session["rowId"] = (int)Session["userId"];
                return RedirectToAction("Details", "UserManagement");
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

            else if (index == 4 || index == 5 || index == 6)
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
            else if (index == 7)
            {
                Session["type"] = "CompanyEmployee";

                return RedirectToAction("CreateFirstSuperUser", "CreateUser");
            }

            else {
                return RedirectToAction("UserDashBoard", "DashBoard");
            }
        }

        public ActionResult EmployeeDetail()
        {

            return View();
        }
    }
}