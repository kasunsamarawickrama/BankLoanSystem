using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

namespace BankLoanSystem.Controllers.DashBoard
{
    public class DashBoardController : Controller
    {

        User userData = new User();
        LoanSetupStep loanStep = new LoanSetupStep();

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
            try
            {
                if (Session["AuthenticatedUser"] != null)
                {
                    userData = ((User)Session["AuthenticatedUser"]);
                }
                else
                {
                    //return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
                    //filterContext.Controller.TempData.Add("UserLogin", "Login");
                    filterContext.Result = new RedirectResult("~/Login/UserLogin");
                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
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

            if (index == 1)
            {
                Session["type"] = "";
                Session["rowId"] = (int)Session["userId"];
                return RedirectToAction("Details", "UserManagement");
            }

            else if (index == 2)
            {
                Session["companyStep"] = 5;

                return RedirectToAction("Index", "SetupProcess");
            }
            else if (index == 3)
            {
                Session["companyStep"] = 6;
                Session["dashboard"] = 1;
                return RedirectToAction("Step6", "SetupProcess");
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
    }
}