using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using BankLoanSystem.Code;
using System.Data;


using SRVTextToImage;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace BankLoanSystem.Controllers
{
    public class UserManagementController : Controller
    {
        /// <summary>
        /// CreatedBy : piyumi
        /// CreatedDate: 2016/01/13
        /// 
        /// Showing user list of selected user role
        /// 
        /// </summary>
        /// <returns></returns>
        /// 

        User userData = new User();
        //public static string CompanyType = "Lender";
        //public static int PartnerCompanyType = 0;
        // Check session in page initia stage
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
                    if (HttpContext.Request.IsAjaxRequest())
                    {

                        filterContext.Result = new HttpStatusCodeResult(404, "Due to inactivity your session has timed out, please log in again.");

                    } else
                    {
                        filterContext.Result = new RedirectResult("/Login/UserLogin?lbl=Due to inactivity your session has timed out, please log in again.");

                    }
                    //return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });

                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                filterContext.Result = new RedirectResult("~/Exceptions/Index");
            }
        }

        public ActionResult UserList()
        {

            int idval;
            int typeval = 0;

            try
            {
                idval = userData.UserId;

                if ((string)Session["searchType"] == "SuperAdmin")
                {
                    typeval = 1;
                    ViewBag.Manage = "Manage SuperAdmins";
                }
                else if ((string)Session["searchType"] == "Admin")
                {
                    typeval = 2;
                    ViewBag.Manage = "Manage Admins";
                }
                else if ((string)Session["searchType"] == "User")
                {
                    typeval = 3;
                    ViewBag.Manage = "Manage Users";
                }

                UserManageAccess obj1 = new UserManageAccess();
                int role = userData.RoleId;
                if ((typeval > 0) && (idval > 0))
                {

                    var ret = obj1.getUserByType(typeval, idval);
                    ViewBag.noList = ret.Count;
                    return PartialView(ret);
                }
                else
                {
                    return PartialView();
                }
            }
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login");
            }


        }
        /// <summary>
        /// CreatedBy : Piyumi
        /// CreatedDate: 2016/01/18
        /// 
        /// get selected rowId
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult Detailsset(int id)
        {
            Session["rowId"] = id;

            return RedirectToAction("Details", "UserManagement");
        }


        [HttpPost]
        public ActionResult UserList(int id, string clickType)
        {
            if (clickType.Equals("edit"))
            {
                Session["editId"] = id;
                return RedirectToAction("editUser", "ManageUsers");


            }

            return View();

        }

        /// <summary>
        /// CreatedBy : Piyumi
        /// CreatedDate: 2016/01/13
        /// 
        /// Showing details of selected user
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult Details()
        {
            int id;
            int logId;

            try
            {
                id = (int)Session["rowId"];
                logId = userData.UserId;
                UserManageAccess obj1 = new UserManageAccess();
                if (id != 0)
                {
                    var ret = obj1.getUserById(id);

                    if (id == logId)
                    {
                        ViewBag.userId = ret.userId;
                        Session["editId"] = id;
                    }
                    else
                    {
                        ViewBag.userId = 0;
                    }
                    return PartialView(ret);
                }
                else { return PartialView(); }
            }
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login");
            }



        }
        /// <summary>
        /// CreatedBy : Irfan
        /// CreatedDate: 2016/01/13
        /// 
        /// Showing details of selected user
        /// EditedBy: Piyumi
        /// EditedDate:2016/03/30
        /// Edited for new dashboard
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult UserDetails()
        {
            Session["rowId"] = userData.UserId;
            Session["loanStep"] = null;
            Loan loan = new Loan();

            if (Session["loanDashboardJoinDealer"] != null)
            {
                Session.Remove("loanDashboardJoinDealer");
            }
            if (Session["loanDashboardAssignUser"] != null)
            {
                Session.Remove("loanDashboardAssignUser");
            }

            if (Session["AuthenticatedUser"] != null)
            {
                DashBoardAccess da = new DashBoardAccess();
                ViewBag.Username = userData.UserName;
                ViewBag.Userid = userData.UserId;
                ViewBag.Company = userData.CompanyName;
                ViewBag.roleId = userData.RoleId;
                if (Session["loanDashboard"] != null)
                {
                    ViewBag.LoanCount = 1;
                    ViewBag.loanSelected = 1;
                    Loan loanSelected = (Loan)Session["loanDashboard"];

                    if (loanSelected != null)
                    {
                        if (userData.RoleId == 1)
                        {
                            ViewBag.PartnerType = 2;
                        }
                        else if (userData.RoleId == 2)
                        {
                            ViewBag.PartnerType = 1;
                        }
                        else {
                            ViewBag.PartnerType = 0;
                        }
                        ViewBag.PartnerName = loanSelected.PartnerName;

                        ViewBag.Branch = loanSelected.BranchName;
                        ViewBag.LoanNum = loanSelected.LoanNumber;
                        ViewBag.IsTitleTrack = loanSelected.IsTitleTrack;

                        Session["IsTitleTrack"] = loanSelected.IsTitleTrack;
                        if ((loanSelected.AdvanceFee == 1) || (loanSelected.LotInspectionFee == 1) || (loanSelected.MonthlyLoanFee == 1))
                        {
                            ViewBag.Fee = 1;
                        }
                        else
                        {
                            ViewBag.Fee = 0;
                        }
                        Session["loanCode"] = loanSelected.LoanCode;

                        if (userData.RoleId == 3)
                        {
                            if ((string)Session["CurrentLoanRights"] != "")
                            {
                                //string[] charactors = loanSelected.Rights.Split(',');

                                string rgts = (string)(Session["CurrentLoanRights"]);
                                string[] rightsStringList = rgts.Split(',');

                                List<string> rightList = new List<string>();
                                for (int i = 0; i < rightsStringList.Length; i++) {
                                    rightList.Add(rightsStringList[i]);
                                }

                                //List<string> rightList = new List<string>(charactors);
                                ViewBag.RightList = rightList;
                            }

                        }
                        else
                        {
                            ViewBag.AdvanceUnits = 1;
                            ViewBag.AddUnits = 1;
                            ViewBag.ViewReports = 1;
                            ViewBag.PayoffUnits = 1;
                            ViewBag.Curtailment = 1;
                            ViewBag.TitleAdd = 1;
                            ViewBag.PayFees = 1;
                        }


                        
                        return View();
                    }
                    else
                    {
                        return View();
                    }

                }

                if (userData.RoleId == 2)
                {
                    //ViewBag.Branch = (ba.getBranchByBranchId(user.BranchId)).BranchName;
                    ViewBag.LoanCount = da.GetLoanCount(userData.BranchId, 2);
                    ViewBag.Branch = userData.BranchName;
                    ViewBag.Position = "Admin";

                }
                else if (userData.RoleId == 1)
                {
                    ViewBag.LoanCount = da.GetLoanCount(userData.Company_Id, 1);
                    ViewBag.Branch = "";
                    ViewBag.Position = "Super Admin";

                }
                else if (userData.RoleId == 3 || userData.RoleId == 4)
                {
                    ViewBag.LoanCount = da.GetLoanCount(userData.UserId, 3);
                    ViewBag.Branch = userData.BranchName;
                    ViewBag.Position = "User";

                }

                if (ViewBag.LoanCount == 1)
                {

                    if (userData.RoleId == 2)
                    {
                        loan = da.GetLoanDetails(userData.BranchId, 2);

                    }
                    else if (userData.RoleId == 1)
                    {
                        loan = da.GetLoanDetails(userData.Company_Id, 1);

                    }
                    else if (userData.RoleId == 3)
                    {
                        loan = da.GetLoanDetails(userData.UserId, 3);

                    }
                    else if (userData.RoleId == 4)
                    {
                        loan = da.GetLoanDetails(userData.UserId, 4);

                    }
                    if (loan != null)
                    {
                        Session["LoanOne"] = loan;
                        if (userData.RoleId == 3)
                        {
                            Session["CurrentLoanRights"] = loan.Rights;
                        }
                        ViewBag.PartnerName = ((Loan)Session["LoanOne"]).PartnerName;
                        ViewBag.PartnerType = loan.PartnerType;
                        ViewBag.Branch = ((Loan)Session["LoanOne"]).BranchName;
                        ViewBag.LoanNum = loan.LoanNumber;
                        ViewBag.IsTitleTrack = loan.IsTitleTrack;
                        Session["IsTitleTrack"] = loan.IsTitleTrack;
                        if ((loan.AdvanceFee == 1) || (loan.LotInspectionFee == 1) || (loan.MonthlyLoanFee == 1))
                        {
                            ViewBag.Fee = 1;
                        }
                        else
                        {
                            ViewBag.Fee = 0;
                        }

                        // 
                        Session["loanCode"] = loan.LoanCode;
                        if (userData.RoleId == 3)
                        {

                            if ((loan.Rights.Length > 0) && (loan.Rights != null))
                            {
                                string[] charactors = { };
                                if (loan.Rights != "")
                                {
                                    charactors = loan.Rights.Split(',');
                                }
                                List<string> rightLst = new List<string>(charactors);

                                ViewBag.RightList = rightLst;

                            }


                        }
                        else if ((userData.RoleId == 1) || (userData.RoleId == 2))
                        {
                            ViewBag.AdvanceUnits = 1;
                            ViewBag.AddUnits = 1;
                            ViewBag.ViewReports = 1;
                            ViewBag.PayoffUnits = 1;
                            ViewBag.Curtailment = 1;
                            ViewBag.TitleAdd = 1;
                            ViewBag.PayFees = 1;
                        }


                        //ViewBag.CompType = (new BranchAccess()).getCompanyTypeByUserId(userData.UserId);
                        //ViewBag.CompType
                        Session["oneLoanDashboard"] = loan;
                        return View();
                    }
                    else
                    {
                        return View();
                    }

                }
                else
                {
                    ViewBag.PartnerType = (userData.CompanyType == 1) ? 2 : 1;
                    return View();
                }


            }
            else
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
            }



        }



        /// <summary>
        /// Frontend page:    dashboard page
        /// title:              checking is atleast one permission for report access
        /// designed:           irfan mam
        /// User story:         DFP 476
        /// developed:          irfan mam
        /// date creaed:        6/28/2016
        /// 
        /// </summary>
        /// 
        /// <returns>
        /// if there is no loan has user rights -> false
        ///  if there is atleast one user right for any loan -> true
        /// </returns>
        public bool IsAtleastOnePermissionForReport(int userId)
        {

            return (new DashBoardAccess()).IsAtleastOnePermissionForReport(userId);


        }




        /*

      Frontend page: Dashboard page
      Title: Get all loans and branch details and bind it to Json
      Designed: Irfan MAM
      User story: DFP- 437
      Developed: Irfan MAM
      Date created: 06/27/2016

*/

        public JsonResult GetLoanBranchDetails(string sidx, string sord, int page, int rows, bool _search)
        {
            
            // get all branch and loan details
            List<DashboardGridModel> dashboardGridModel = (new DashBoardAccess()).GetAllLoanBranchDetails(userData.Company_Id, userData.BranchId);
           

          

            // these varibles are for JqGrid purpose

            var count = dashboardGridModel.Count(); // number of rows
            int pageIndex = page; // number of pages on the grid
            int pageSize = rows; // maximum page sige
            int startRow = (pageIndex * pageSize) + 1;
            int totalRecords = count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            // binding list to json object for jqGrid
            var result = new
            {
                total = totalPages,
                page = pageIndex,
                records = count,
                rows = dashboardGridModel.Select(x => new
                {
                    x.Id,
                    x.BranchId,
                    x.BranchName,
                    x.PartnerBranchId,

                    x.PartnerBranchName,


                    x.Loanid,
                    x.LoanNumber,
                    x.TotalAmount,
                    x.UsedAmount,
                    x.StatusId,
                    x.Status,
                    x.StepNo
                }
                                         ).ToArray().Select(x => new
                                         {
                                             id = x.Id.ToString(),
                                             cell = new string[] {
                                                        x.BranchId.ToString(),
                                                        x.BranchName,
                                                        x.PartnerBranchId.ToString(),
                                                        x.PartnerBranchName,
                                                        x.Loanid.ToString(),
                                                        x.LoanNumber,
                                                        x.TotalAmount.ToString(), 
                                                        x.UsedAmount.ToString(), 
                                                        x.StatusId.ToString(),
                                                         x.Status,
                                                         x.StepNo.ToString()
                                                      }
                                         }
                      ).ToArray()



            };

            // returning json object
            return Json(result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult editUserSet(int editId)
        {

            // check if he is allowable to view/edit


            Session["editId"] = editId;
            return RedirectToAction("editUser");
        }
        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/13
        /// 
        /// Showing editable User using currentUserId and editableUserId
        /// 
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult editUser()
        {
            int currentUserId;
            int editUserId;

            try
            {
                currentUserId = int.Parse(Session["userId"].ToString());
                editUserId = int.Parse(Session["editId"].ToString());
            }
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
            }

            int companyId;
            User editUser = (new UserAccess()).retreiveUserByUserId(editUserId);
            companyId = editUser.Company_Id;
            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(companyId);

            // insert all branches into selectedlist
            List<SelectListItem> branchSelectLists = new List<SelectListItem>();
            foreach (Branch branch in branchesLists)
            {
                branchSelectLists.Add(new SelectListItem() { Text = branch.BranchName, Value = branch.BranchId.ToString() });

            }

            ViewBag.BranchId = new SelectList(branchSelectLists, "Value", "Text", editUser.BranchId);



            if (currentUserId == editUserId)
            {
                ViewBag.isSame = true;
                Session["rowId"] = editUserId;

                return PartialView(editUser);
            }
            else { ViewBag.isSame = false; }



            List<UserLogin> details;

            string typevalue;
            int typeval = 0;

            try
            {
                typevalue = (string)Session["searchtype"];

                if (typevalue == "SuperAdmin")
                {
                    typeval = 1;
                }
                else if (typevalue == "Admin")
                {
                    typeval = 2;
                }
                else if (typevalue == "User")
                {
                    typeval = 3;
                    ViewBag.Manage = "Manage Users";
                }

                details = (new UserManageAccess()).getUserByType(typeval, currentUserId);
            }
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
            }




            bool isEditable = false;
            foreach (UserLogin user in details)
            {
                if (user.userId == editUserId && user.isEdit == true)
                {
                    isEditable = true;
                    break;
                }
            }
            if (!isEditable)
            {
                return new HttpStatusCodeResult(404);
            }




            Session["editUserId"] = editUserId;
            return PartialView(editUser);
        }
        /// <summary>
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/17
        /// 
        /// edit rights Set session variables
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ActionResult editRights(User user)
        {
            int currentUserId = (int)Session["userId"];
            int editUserId = (int)Session["editUserId"];

            Session["editUserIds"] = editUserId;

            return RedirectToAction("EditRights", "EditRights");

        }
        /// <summary>
        /// CreatedBy : MAM. IRFAN
        /// CreatedDate: 2016/01/13
        /// 
        /// updating the user detail
        /// 
        /// argument: User
        /// 
        /// </summary>
        /// <returns>update successful mesage / failed to update</returns>

        [HttpPost]
        public ActionResult editUser(User user)
        {

            int currentUserId;
            int editUserId;

            string typevalue;



            try
            {
                currentUserId = int.Parse(Session["userId"].ToString());
                editUserId = int.Parse(Session["editId"].ToString());
            }
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
            }

            bool isUpdate;
            if (currentUserId == editUserId)
            {// Update the data into database
                ViewBag.isSame = true;
                isUpdate = (new UserAccess()).updateProfileDetails(editUserId, user.UneditUserName, user.FirstName, user.LastName, user.Email, user.PhoneNumber, DateTime.Now);
            }
            else
            {   // Update the data into database
                ViewBag.isSame = false;
                typevalue = (string)Session["searchtype"];

                if (typevalue == "User")
                {

                    ViewBag.Manage = "Manage Users";
                }
                isUpdate = (new UserAccess()).updateUserDetails(editUserId, user.UneditUserName, user.FirstName, user.LastName, user.Email, user.PhoneNumber, user.Status, user.BranchId, DateTime.Now);
            }





            if (isUpdate)
                ViewBag.SuccessMsg = "Data Successfully Updated";
            else
            {
                ViewBag.ErrorMsg = "Updating failed";
                return View(user);
            }

            user = (new UserAccess()).retreiveUserByUserId(editUserId);
            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(user.Company_Id);

            // insert all branches into selectedlist
            List<SelectListItem> branchSelectLists = new List<SelectListItem>();
            foreach (Branch branch in branchesLists)
            {
                branchSelectLists.Add(new SelectListItem() { Text = branch.BranchName, Value = branch.BranchId.ToString() });

            }


            ViewBag.BranchId = new SelectList(branchSelectLists, "Value", "Text", user.BranchId);


            return PartialView(user);
        }

        /// <summary>
        /// CreatedBy : Piyumi
        /// CreatedDate: 2016/01/18
        /// 
        /// get selected rowId to delete
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult PrevDelete(int id)
        {
            TempData["delRowId"] = id;

            return RedirectToAction("Delete", "UserManagement");
        }

        /// <summary>
        /// CreatedBy : Piyumi
        /// CreatedDate: 2016/01/18
        /// 
        /// delete selected user
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult Delete()
        {
            int id = (int)TempData["delRowId"];

            DashBoardAccess db = new DashBoardAccess();

            UserManageAccess obj1 = new UserManageAccess();
            if (id != 0)
            {
                bool ret = obj1.deleteUser(id);
                if (ret)
                {
                    ViewBag.SuccessMsg = "User is successfully deleted";

                }
                else
                {
                    ViewBag.ErrorMsg = "Failed to delete user";
                }

            }

            return RedirectToAction("UserList", "UserManagement");


        }


        /*

   Frontend page: Change Password
   Title:  view of the change Password page
   Designed: Irfan Mam
   User story: 
   Developed: Irfan MAM
   Date created: 1/25/2016

*/
        public ActionResult ChangePassword()
        {

            // returning the page
            return PartialView();
        }


        /*

Frontend page: Change Password
Title:  update the password
Designed: Irfan Mam
User story: 
Developed: Irfan MAM
Date created: 1/25/2016

*/
        [HttpPost]
        public ActionResult ChangePassword(ResetPassword resetPasswordModel)
        {
            int userId = int.Parse(Session["editId"].ToString());
            bool isSuccess = (new forgotPasswordTokenAccess()).resetPassword(userId, resetPasswordModel);

            if (!isSuccess)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ViewBag.message = "Password Sucessfully Changed";

            return PartialView();
        }


        public ActionResult DashBoard()
        {

            int userId = userData.UserId;

            var access = new UserRightsAccess();

            ///retrive all rights
            List<Right> rights = access.getRights();

            int userRole = userData.RoleId;

            if (userRole == 3)
            {
                ///get permission string for the relevent user
                List<Right> permissionString = access.getRightsString(userId, 0);
                if (permissionString.Count == 1)
                {


                    string permission = permissionString[0].rightsPermissionString;
                    if (permission != "")
                    {
                        string[] charactors = permission.Split(',');

                        List<Right> temprights = new List<Right>();

                        foreach (var charactor in charactors)
                        {
                            foreach (var obj in rights)
                            {
                                if (string.Compare(obj.rightId, charactor) == 0)
                                {
                                    temprights.Add(obj);
                                    break;

                                }

                            }
                        }

                        rights = temprights;


                    }
                    else
                    {
                        rights = new List<Right>();
                    }


                }

                else if (permissionString.Count == 0)
                {

                    rights = new List<Right>();
                }



            }

            return PartialView(rights);

        }

        LoanSelection loanSelection = new LoanSelection();


        public ActionResult Selectloan(string type)
        {



            LoanSelection detail = new LoanSelection();
            //if edit loan
            if (!string.IsNullOrEmpty(type) && type.Contains("tidenaol"))
            {
                detail = (new UnitAccess()).GetInActiveLoans(userData.UserId, userData.Company_Id, userData.BranchId, userData.RoleId);
                if (detail != null)
                {
                    Session["detail"] = detail;
                }
                else
                {
                    return RedirectToAction("UserLogin", "Login");
                }
            }
            else if (!string.IsNullOrEmpty(type) && type.Contains("aticno"))
            {
                detail = (new UnitAccess()).GetPermisssionGivenLoanwithBranchDeatils(userData.UserId, userData.Company_Id, userData.BranchId, userData.RoleId);
                if (detail != null)
                {
                    Session["detail"] = detail;
                }
                else
                {
                    return RedirectToAction("UserLogin", "Login");
                }
            }
            else if(!string.IsNullOrEmpty(type) && type.Contains("tcaninaol"))
            {
                detail = (new UnitAccess()).GetActiveLoanforInactive(userData.Company_Id, userData.BranchId, userData.RoleId);
                if (detail != null)
                {
                    Session["detail"] = detail;
                }
                else
                {
                    return RedirectToAction("UserLogin", "Login");
                }
            }
            else if (!string.IsNullOrEmpty(type))
            {
                detail = (new UnitAccess()).GetPermisssionGivenLoanwithBranchDeatils(userData.UserId, userData.Company_Id, userData.BranchId, userData.RoleId);
                if (detail == null)
                {
                    ViewBag.type = "return";
                    return PartialView();
                }
                else if (detail != null) {
                    Session["detail"] = detail;
                }


            }



            int userId = userData.UserId;
            // if Session is expired throw an error


            loanSelection.RegBranches = new List<Branch>();
            loanSelection.NonRegBranchList = new List<NonRegBranch>();
            loanSelection.LoanList = new List<LoanSetupStep1>();


            //getting user role
            UserAccess ua = new UserAccess();



            // curUser.Company_Id   asanka 8/3/2016
            //create list for nonRegisterCompaniers

            List<NonRegBranch> NonRegisteredBranchLists = detail.NonRegBranchList; //(new BranchAccess()).getNonRegBranches(userData.Company_Id);

            if (userData.RoleId == 1)
            {

                loanSelection.RegBranches = detail.RegBranches; //(new BranchAccess()).getBranches(userData.Company_Id);

                if (loanSelection.RegBranches != null && loanSelection.RegBranches.Count() == 1)
                {



                    // the get non registered branches details for perticular branch  from the non registeres branches list
                    foreach (NonRegBranch branch in NonRegisteredBranchLists)
                    {
                        if (branch.BranchId == loanSelection.RegBranches[0].BranchId)
                        {
                            loanSelection.NonRegBranchList.Add(branch);
                        }
                    }

                    if (loanSelection.NonRegBranchList.Count() == 1)
                    {

                        List<LoanSetupStep1> loanLists = detail.LoanList; //new LoanSetupAccess().GetLoanDetailsByNonRegBranchId(loanSelection.NonRegBranchList[0].NonRegBranchId);
                        loanSelection.LoanList = new List<LoanSetupStep1>();
                        foreach (LoanSetupStep1 loan in loanLists) {
                            // if(loan.LoanStatus == true)
                            //  {
                            loanSelection.LoanList.Add(loan);
                            //  }
                        }

                        //if loans count is one redirect to add unit page
                    }
                }

            } else if (userData.RoleId == 2)
            {

                //loanSelection.RegBranches.Add((new BranchAccess()).getBranchByBranchId(userData.BranchId));
                if (detail.RegBranches != null && detail.RegBranches.Count > 0)
                {
                    loanSelection.RegBranches.Add(detail.RegBranches[0]);
                }



                // the get non registered branches details for perticular branch  from the non registeres branches list
                foreach (NonRegBranch branch in NonRegisteredBranchLists)
                {
                    if (branch.BranchId == userData.BranchId)
                    {

                        loanSelection.NonRegBranchList.Add(branch);


                    }
                }
                if (loanSelection.NonRegBranchList.Count() == 1)
                {
                    loanSelection.LoanList = detail.LoanList; //new LoanSetupAccess().GetLoanDetailsByNonRegBranchId(loanSelection.NonRegBranchList[0].NonRegBranchId);

                }
            }
            else if (userData.RoleId == 3)
            {

                //loanSelection.RegBranches.Add((new BranchAccess()).getBranchByBranchId(userData.BranchId));

                loanSelection.RegBranches.Add(detail.RegBranches[0]);


                // the get non registered branches details for perticular branch  from the non registeres branches list
                foreach (NonRegBranch branch in NonRegisteredBranchLists)
                {
                    if (branch.BranchId == userData.BranchId)
                    {
                        loanSelection.NonRegBranchList.Add(branch);
                    }
                }
                if (loanSelection.NonRegBranchList.Count() == 1)
                {
                    loanSelection.LoanList = detail.LoanList; //new LoanSetupAccess().GetLoanDetailsByNonRegBranchId(loanSelection.NonRegBranchList[0].NonRegBranchId);

                }
            }


            Session["popUpSelectionType"] = type;
            if (type == "asderruy") // for add unit page
            {
                ViewBag.type = "AddUnit";
                return PartialView(loanSelection);
            }

            else if (type == "tyuirede") // for add unit page
            {
                ViewBag.type = "Advance";
                return PartialView(loanSelection);
            }

            else if (type == "sedretyt")
            {
                ViewBag.type = "Curtailment";
                return PartialView(loanSelection);
            }

            else if (type == "wsedtgio")
            {
                ViewBag.type = "PayOff";
                return PartialView(loanSelection);
            }

            else if (type == "frtgcvfd")
            {
                ViewBag.type = "Title";
                return PartialView(loanSelection);
            }
            else if (type == "dashboard")
            {
                ViewBag.type = "DashBoard";
                return PartialView(loanSelection);
            }
            else if (type == "linkDealer")
            {
                ViewBag.type = "linkDealer";
                return PartialView(loanSelection);
            }
            else if (type == "assignRights")
            {
                ViewBag.type = "assignRights";
                return PartialView(loanSelection);
            }

            else if (type == "tidenaol") // for add unit page
            {
                ViewBag.type = "EditLoan";
                return PartialView(loanSelection);
            }
            else if (type == "tcaninaol") 
            {
                ViewBag.type = "InactiveLoan";
                return PartialView(loanSelection);
            }
            else if (type == "aticno")
            {
                ViewBag.type = "RenewLoan";
                return PartialView(loanSelection);
            }
            return PartialView(loanSelection);
        }



        public ActionResult GetLoansByNonRegBranchId(int NonRegBranchId, string type)
        {
            ViewBag.type = type;
            LoanSelection list2 = (LoanSelection)Session["detail"];
            //new LoanSetupAccess().GetLoanDetailsByNonRegBranchId(NonRegBranchId)
            List<LoanSetupStep1> LoanList = new List<LoanSetupStep1>();
            foreach (var l in list2.LoanList)
            {
                if (NonRegBranchId == l.nonRegisteredBranchId)
                {
                    LoanList.Add(l);
                }
            }
            return PartialView(LoanList);
        }


        public ActionResult getNonRegBranchesByRegBranchId(int RegBranchId, string type)
        {
            LoanSelection list = (LoanSelection)Session["detail"];

            List<NonRegBranch> NonRegisteredBranchLists = list.NonRegBranchList;//(new BranchAccess()).getNonRegBranches(userData.Company_Id);
            LoanSelection loanSelection = new LoanSelection();

            loanSelection.NonRegBranchList = new List<NonRegBranch>();
            loanSelection.LoanList = new List<LoanSetupStep1>();

            // the get non registered branches details for perticular branch  from the non registeres branches list
            foreach (NonRegBranch branch in NonRegisteredBranchLists)
            {
                if (branch.BranchId == RegBranchId)
                {
                    loanSelection.NonRegBranchList.Add(branch);
                }
            }


            ViewBag.type = type;



            if (loanSelection.NonRegBranchList != null && loanSelection.NonRegBranchList.Count() == 1)
            {

                //loanSelection.LoanList = list.LoanList;   //new LoanSetupAccess().GetLoanDetailsByNonRegBranchId(loanSelection.NonRegBranchList[0].NonRegBranchId);
                //if loans count is one redirect to add unit page
                foreach (LoanSetupStep1 l in list.LoanList)
                {
                    if (loanSelection.NonRegBranchList[0].NonRegBranchId == l.nonRegisteredBranchId)
                    {
                        //loanSelection.NonRegBranchList.Add(branch);
                        loanSelection.LoanList.Add(l);
                    }
                }
            }


            return PartialView(loanSelection);
        }

        public ActionResult setLoanCode(string loanCode)//, string action
        {

            Session["loanCode"] = loanCode;
            if (loanCode == null || Session["detail"] == null)
            {
                return RedirectToAction("UserDetails");
            }
            LoanSelection list3 = (LoanSelection)Session["detail"];
            Loan finalSelectedLoan = new Loan();
            foreach (var l in list3.LoanList)
            {
                if (l.loanCode == loanCode)
                {
                    if (userData.RoleId == 3)
                    {
                        string permission = "";
                        List<Right> permissionString = (new UserRightsAccess()).getRightsString(userData.UserId, l.loanId);
                        if (permissionString.Count >= 1)
                        {
                            permission = permissionString[0].rightsPermissionString;
                            Session["CurrentLoanRights"] = permission;
                        }
                        else {
                            Session["CurrentLoanRights"] = "";
                        }
                    }
                    finalSelectedLoan.NonRegBranchId = l.nonRegisteredBranchId;
                    finalSelectedLoan.LoanId = l.loanId;
                    finalSelectedLoan.LoanNumber = l.loanNumber;
                    finalSelectedLoan.LoanCode = l.loanCode;
                    finalSelectedLoan.Rights = l.rightId;
                    finalSelectedLoan.AdvanceFee = l.AdvanceFee;
                    finalSelectedLoan.MonthlyLoanFee = l.MonthlyLoanFee;
                    finalSelectedLoan.LotInspectionFee = l.LotInspectionFee;
                    if (l.titleTracked == true)
                    {
                        finalSelectedLoan.IsTitleTrack = 1;
                    }
                    else {
                        finalSelectedLoan.IsTitleTrack = 0;
                    }
                    //finalSelectedLoan.IsTitleTrack =
                    //for edit loan
                    finalSelectedLoan.CurrentLoanStatus = l.CurrentLoanStatus;
                    finalSelectedLoan.CreatedDate = l.CreatedDate;
                    finalSelectedLoan.StartDate = l.startDate;
                    finalSelectedLoan.MaturityDate = l.maturityDate;
                    finalSelectedLoan.LoanAmount = l.loanAmount;
                    foreach (var nrbr in list3.NonRegBranchList)
                    {
                        if (nrbr.NonRegBranchId == l.nonRegisteredBranchId)
                        {
                            finalSelectedLoan.BranchId = nrbr.BranchId;
                            finalSelectedLoan.PartnerName = nrbr.CompanyNameBranchName;

                            foreach (var br in list3.RegBranches)
                            {
                                if (br.BranchId == finalSelectedLoan.BranchId)
                                {
                                    finalSelectedLoan.BranchName = br.BranchName;
                                }
                            }
                        }
                    }
                    if ((string)Session["popUpSelectionType"] == "linkDealer")
                    {
                        Session["loanDashboardJoinDealer"] = finalSelectedLoan;
                    } else if ((string)Session["popUpSelectionType"] == "assignRights")
                    {
                        Session["loanDashboardAssignUser"] = finalSelectedLoan;

                    }
                    else if ((string)Session["popUpSelectionType"] == "tidenaol")
                    {
                        Session["loanDashboardEditLoan"] = finalSelectedLoan;

                    }
                    else if ((string)Session["popUpSelectionType"] == "tcaninaol")
                    {
                        Session["loanDashboardActiveLoanInact"] = finalSelectedLoan;

                    }
                    else if ((string)Session["popUpSelectionType"] == "aticno")
                    {
                        Session["loanDashboardRenewLoan"] = finalSelectedLoan;

                    }
                    else {
                        Session["loanDashboard"] = finalSelectedLoan;
                    }
                }
            }
            Session["detail"] = null;
            if ((string)Session["popUpSelectionType"] == "assignRights")
            {
                return RedirectToAction("AssignRights");
            }
            else if ((string)Session["popUpSelectionType"] == "linkDealer")
            {
                return RedirectToAction("../CreateDealer/LinkDealer");
            }
            else if ((string)Session["popUpSelectionType"] == "tidenaol")
            {
                return RedirectToAction("EditLoan");
            }
            else if ((string)Session["popUpSelectionType"] == "tcaninaol")
            {
                return RedirectToAction("InactiveLoan");
            }
            else if ((string)Session["popUpSelectionType"] == "aticno")
            {
                return RedirectToAction("RenewLoan", "LoanManagement");
            }
            else
            {
                return RedirectToAction("UserDetails");
            }
            //return RedirectToAction(action);
        }

        public List<Right> PermissionList(int userId, int loanId)
        {
            var access = new UserRightsAccess();

            //retrive all rights
            List<Right> rights = access.getRights();
            int userRole = (new UserManageAccess()).getUserRole(userId);

            if (userRole == 3)
            {
                //get permission string for the relevent user
                List<Right> permissionString = access.getRightsString(userId, loanId);
                if (permissionString.Count >= 1)
                {
                    string permission = permissionString[0].rightsPermissionString;
                    if (permission != "")
                    {
                        string[] charactors = permission.Split(',');

                        List<Right> temprights = new List<Right>();

                        foreach (var charactor in charactors)
                        {
                            foreach (var obj in rights)
                            {
                                if (String.CompareOrdinal(obj.rightId, charactor) == 0)
                                {
                                    temprights.Add(obj);
                                    break;
                                }
                            }
                        }
                        rights = temprights;
                    }
                    else
                    {
                        rights = new List<Right>();
                    }
                }
                else if (permissionString.Count == 0)
                {
                    rights = new List<Right>();
                }
            }

            return rights;
        }


        public ActionResult ManageUserLoanRights()
        {


            return View();
        }

        [HttpPost]
        [ActionName("ManageUserLoanRights")]
        public ActionResult ManageUserLoanRightsPost()
        {
            return View();
        }

        /// <summary>
        /// Frontend page: Create User(Dashboard)
        /// Title: Get view of Create user page in dashboard
        /// Designed: Piyumi P
        /// User story:
        /// Developed: Piyumi P
        /// Date created: 4/1/2016
        /// Edited: 6/21/2016
        /// </summary>
        /// <param name="lbls"></param>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CreateDashboardUser(string lbls)
        {

            // take firstsuperadmin userid....
            int userId = userData.UserId;
            StepAccess sa = new StepAccess();
            DashBoardAccess da = new DashBoardAccess();
            User us = new User();
            // check he is a super admin or admin

            int roleId = userData.RoleId;
            //Check user role is user or dealer user
            if ((roleId == 3)||(roleId == 4))
            {
                //return to login page
                return RedirectToAction("UserLogin", "Login");
            }
            //Check result of insert user details
            if (TempData["createUserResult"] != null)
            {
                //result is 1 = success
            if(int.Parse(TempData["createUserResult"].ToString()) == 1) {
                    ViewBag.SuccessMsg = "User Successfully Created";
                }
                //result is 0 = failure
                else if (int.Parse(TempData["createUserResult"].ToString()) == 0)
                {
                    ViewBag.ErrorMsg = "Failed To Create User";
                }
            }
            

            ViewBag.CurrUserRoleType = roleId;
            int loanCount = -1;
            //Check user role is admin
            if (userData.RoleId == 2)
            {
                //get loan count for branch which admin is assigned to
                loanCount = da.GetLoanCount(userData.BranchId, 2);
                

            }
            //Check user role is super admin
            else if (userData.RoleId == 1)
            {
                //get loan count for company which super admin is assigned to
                loanCount = da.GetLoanCount(userData.Company_Id, 1);
                
            }
            RoleAccess ra = new RoleAccess();
            List<UserRole> roleList = ra.GetAllUserRoles();
            List<UserRole> tempRoleList = new List<UserRole>();
            // filter user roles for page user role drop down compairing with role of user who logged in
            for (int i = roleId - 1; i < roleList.Count && ViewBag.CurrUserRoleType != 3; i++)
            {
                //Check role is dealer user 
                if (roleList[i].RoleId == 4)
                {
                    continue;
                }
                //Check role is user and loan count is 0
                else if ((roleList[i].RoleId == 3) &&(loanCount==0)) 
                {
                    continue;
                }
                //Check role is super admin and logged user role is admin
                else if ((userData.RoleId==2)&&(roleList[i].RoleId == 1)) {
                    continue;
                }
                UserRole tempRole = new UserRole()
                {
                    RoleId = roleList[i].RoleId,
                    RoleName = roleList[i].RoleName
                };
                tempRoleList.Add(tempRole);
            }

            ViewBag.RoleId = new SelectList(tempRoleList, "RoleId", "RoleName");

            // get all branches which belong to company
            List<Branch> branchesLists = (new BranchAccess()).getBranches(userData.Company_Id);
            List<Branch> branchesListAdmin = new List<Branch>();
            //Check user is super admin
            if (userData.RoleId == 1) {
                ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");
            }
            else {
                //filter retrieved branch list for admin
                branchesListAdmin = branchesLists.FindAll(t => t.BranchId == userData.BranchId);
                ViewBag.BranchId = new SelectList(branchesListAdmin, "BranchId", "BranchName");
            }
           

            List<Branch> branchesListsLoan =  new List<Branch>();
            List<Branch> branchesListsLoanAd = new List<Branch>();
            //get list of branches which has atleast one loan 
            branchesListsLoan = (new BranchAccess()).GetLoansBranches(userData.Company_Id);
           //check user is super admin
            if (userData.RoleId == 1)
            {
                //convert branch list to select list
                ViewBag.BranchIdUser = new SelectList(branchesListsLoan, "BranchId", "BranchName");
            }
            else {
                //filter branch which admin is assigned
                branchesListsLoanAd = branchesListsLoan.FindAll(t => t.BranchId == userData.BranchId);
                //convert branch list to select list
                ViewBag.BranchIdUser = new SelectList(branchesListsLoanAd, "BranchId", "BranchName");
            }
           //check request is ajax request
            if (HttpContext.Request.IsAjaxRequest())
            {
                ViewBag.AjaxRequest = 1;
                return PartialView();
            }
            else
            {

                return View();
            }

        }

        /// <summary>
        /// Frontend page: Create User (Dashboard) 
        /// Title: Get active loans for given branch id and right list
        /// Designed: Piyumi P
        /// User story:
        /// Developed: Piyumi P
        /// Date created: 4/1/2016
        /// </summary>
        /// <param name="BranchIdL"></param>
        /// <returns></returns>
        public ActionResult GetLoansByBranches(int BranchIdL) 
        {
            User us = new User();
            List<Branch> listLoan = new List<Branch>();
            List<Branch> listLoan2 = new List<Branch>();
            //get loans which belong to given branch id
            listLoan = (new BranchAccess()).GetLoansByBranches(BranchIdL);
            //check loan list is not null and count >0
            if(listLoan!=null && listLoan.Count > 0)
            {
                //assigned loan list to user object property
                us.BranchList = listLoan;
                //convert loan list to session object
                Session["LoanTitle"] = listLoan;
            }
            ViewBag.LoanId = new SelectList(listLoan,"LoanId","LoanNumber");
            List<Right> rightLists = new List<Right>();

            //get all rights list
            rightLists = (new UserRightsAccess()).getRights();

            us.UserRightsList = rightLists;
            //get all report list
            us.ReportRightsList = (new UserRightsAccess()).getReportRights();
            //check request is ajax request
            if (HttpContext.Request.IsAjaxRequest())
            {
                ViewBag.AjaxRequest = 1;
                return PartialView(us);
            }
            else
            {

                return PartialView(us);
            }
            //return View();
        }

        /// <summary>
        /// Frontend page: Create User (Dashboard) 
        /// Title: Insert user details
        /// Designed: Piyumi P
        /// User story:
        /// Developed: Piyumi P
        /// Date created: 4/1/2016
        /// Edited: Piyumi P
        /// Date edited: 6/24/2016
        /// 
        /// </summary>
        /// <param name="userObj"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateDashboardUser(User userObj)
        {
            //assign phone number to object property
            userObj.PhoneNumber = userObj.PhoneNumber2;
            //assign user id to variable
            int currentUser = userData.UserId;

            // assign role to variable
            int roleId = userData.RoleId;
            //assign current user id to created by property
            userObj.CreatedBy = currentUser;
            //is delete property as false
            userObj.IsDelete = false;
            //encrypt password
            string passwordTemp = userObj.Password;

            UserAccess ua = new UserAccess();
           DashBoardAccess da = new DashBoardAccess();
            string newSalt = PasswordEncryption.RandomString();
            userObj.Password = PasswordEncryption.encryptPassword(userObj.Password, newSalt);

            userObj.Email = userObj.NewEmail;

            //assign logged user's company id to created user's company id
            userObj.Company_Id = userData.Company_Id;
            //check user role is admin
            if (roleId == 2)
            {
                //assign logged user's branch id to created user's branch id
                userObj.BranchId = userData.BranchId;
            }
            //check created user is super admin and logged user is super admin
            if ((userObj.RoleId == 1)&&(userData.RoleId==1))
            {
                //assign logged user's step status to created user's step status
                userObj.step_status = userData.step_status;
            }
            //check created user is admin
            else if (userObj.RoleId == 2)
            {
                //get step status for given branch id
            int step= ua.GetStepStatusByUserBranchId(userObj.BranchId);
                //check step is 0 or greater than 0
            if(step>=0) 
            {
                    //assign step to created user's step status
                    userObj.step_status = step;
            }
                
            }
            //check created user is user
            else if (userObj.RoleId == 3)
            {
                //check Session["LoanTitle"] is not null
                if (Session["LoanTitle"] != null)
                {
                    //convert session to list
                    List<Branch> loanList = (List<Branch>)Session["LoanTitle"];
                    for (var j = 0; j < loanList.Count; j++)
                    {
                        //check created user's loan id
                        if (loanList[j].LoanId == userObj.LoanId)
                        {
                            foreach (Right rgt1 in userObj.UserRightsList)
                            {
                                //check title is needed to be tracked for created user's loan
                                if (!loanList[j].IsTitleTrack && rgt1.rightId == "U02")
                                {
                                    //assign title page rights as false if title is not needed to be tracked
                                    rgt1.active = false;
                                }
                                //check if there is atleast one fee for created user's loan
                                if (!loanList[j].HasFee && rgt1.rightId == "U07")
                                {
                                    //assign fee page rights as false if there is no atleast one fee
                                    rgt1.active = false;
                                }
                            }
                               
                            //check report rights according to the loan setup details
                            foreach(Right rgt in userObj.ReportRightsList)
                            {
                                //check title need to be tracked and related right id
                                if(!loanList[j].IsTitleTrack && rgt.rightId== "R04")
                                {
                                    rgt.active = false;
                                }
                                //check loan has advance fee and related right id for advance fee invoice
                                if (!loanList[j].HasAdvanceFee && rgt.rightId == "R07")
                                {
                                    rgt.active = false;
                                }
                                //check loan has advance fee and related right id for advance fee receipt
                                if (!loanList[j].HasAdvanceFee && rgt.rightId == "R08")
                                {
                                    rgt.active = false;
                                }
                                //check loan has monthly fee and related right id for monthly fee invoice
                                if (!loanList[j].HasMonthlyFee && rgt.rightId == "R09")
                                {
                                    rgt.active = false;
                                }
                                //check loan has monthly fee and related right id for monthly fee receipt
                                if (!loanList[j].HasMonthlyFee && rgt.rightId == "R10")
                                {
                                    rgt.active = false;
                                }
                                //check loan has lot inspection fee and related right id for lot inspection fee invoice
                                if (!loanList[j].HasLotFee && rgt.rightId == "R11")
                                {
                                    rgt.active = false;
                                }
                                //check loan has lot inspection fee and related right id for lot inspection fee receipt
                                if (!loanList[j].HasLotFee && rgt.rightId == "R12")
                                {
                                    rgt.active = false;
                                }
                            }
                        }
                    }
                }
                //assign 1 for created user's step status
                userObj.step_status= 1;
                //assign selected branch id for created user's branch id
                userObj.BranchId = userObj.BranchIdUser;
                string[] arrList = new string[userObj.UserRightsList.Count];
                string[] arrList2 = new string[userObj.ReportRightsList.Count];
                int i = 0;
                int k = 0;
                //create user right list string by checking each right in right list active status
                foreach (var x in userObj.UserRightsList)
                {
                    if (x.active)
                    {
                        arrList[i] = x.rightId;
                        i++;
                    }
                }
                //create user report right list string by checking each right in report right list active status
                foreach (var y in userObj.ReportRightsList)
                {
                    if (y.active)
                    {
                        arrList2[k] = y.rightId;
                        k++;
                    }
                }
                arrList = arrList.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                
                userObj.UserRights = string.Join(",", arrList);
                //add report rights
                arrList2 = arrList2.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                userObj.ReportRights = string.Join(",", arrList2);
            }
         
            //Insert user details
            int res = da.InsertUserInDashboard(userObj);

            //check result of insert user function
            if (res > 0)
            {
                //update Companay Step States in incomplete Branches continued in dashboard
                StepAccess sa = new StepAccess();
                sa.UpdateCompanySetupStep(userData.Company_Id, userObj.BranchId, 4);

                //if created user's status is active send email to inform his username and password
                if (userObj.Status)
                {

                string body = "Hi " + userObj.FirstName + "! <br /><br /> Your account has been successfully created. Below in your account detail." +
                              "<br /><br /> User name: " + userObj.UserName +
                                    "<br /> Password : <b>" + passwordTemp +
                                 
                              "<br /><br/> Thanks,<br /> Admin.";

                Email email = new Email(userObj.Email);

              
                email.SendMail(body, "Account details");

                }

                string roleName = "";
                //check created user is super admin
                if (userObj.RoleId == 1)
                {
                    //assign role name as super admin
                    roleName = "Super Admin";
                }
                //check created user is admin
                else if (userObj.RoleId == 2)
                {
                    //assign role name as admin
                    roleName = "Admin";
                }
                //check created user is user
                else if (userObj.RoleId == 3)
                {
                    //assign role name as user
                    roleName = "User";
                }
                //insert log record
                Log log = new Log(userData.UserId, userData.Company_Id, userObj.BranchId, 0, "Create User", "Create "+roleName+" ,Username:"+userObj.UserName, DateTime.Now);

                int islog = (new LogAccess()).InsertLog(log);
                TempData["createUserResult"] = 1;
                //return RedirectToAction("CreateDashboardUser");
                Session["LoanTitle"] = null;

            }
            else
            {
                TempData["createUserResult"] = 0;
                //return View();
            }
            return RedirectToAction("CreateDashboardUser");
        }


        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2016/04/01
        /// 
        /// to show the view
        /// </summary>
        /// <returns></returns>
        public ActionResult AssignUserRights()
        {
            CompanyAccess ca = new CompanyAccess();
            BranchAccess ba = new BranchAccess();
            Loan loan = (Loan)Session["loanDashboard"];

            NonRegBranch nonRegBranches = ba.getNonRegBranchByNonRegBranchId(loan.NonRegBranchId);
            ViewBag.nonRegBranches = nonRegBranches.BranchName;// nonRegBranches.BranchName;
            ViewBag.nonRegCompany = nonRegBranches.CompanyNameBranchName;
            return View();
        }

        public ActionResult UserRequestAns()
        {
            List<UserRequest> RequestList = new List<UserRequest>();
            try
            {
                UserRequestAccess userRequest = new UserRequestAccess();
                
                ViewBag.Anscount = 0;
                DataSet dataset = userRequest.SelectRequestAns(userData);
                if (dataset != null && dataset.Tables.Count != 0)
                {
                    foreach (DataRow dataRow in dataset.Tables[0].Rows)
                    {
                        UserRequest request = new UserRequest();
                        request.answer = dataRow["answer"].ToString();
                        request.is_rep_view = bool.Parse(dataRow["is_rep_view"].ToString());
                        RequestList.Add(request);
                        ViewBag.Anscount = int.Parse(dataRow["Anscount"].ToString());
                    }
                    var ret= RequestList;
                    return PartialView(ret);
                }
                else
                {
                    return PartialView();
                }
            }
            catch
            {
                return RedirectToAction("UserLogin", "Login");
            }
        }
        //edited kasun 2016/6/27
        public ActionResult UserRequestMessage()
        {
            int userrole = userData.RoleId;
            int userId = userData.UserId;

            return PartialView();
        }

        //edited kasun 2016/6/27
        [HttpPost]
        [ActionName("UserRequestMessage")]
        public ActionResult UserRequestMessagePost(UserRequest userReq)
        {
            //if (this.Session["CaptchaImageText"].ToString() == userReq.captcha)
            //{
                string loancod = "";
                string page_nam = "";
                userReq.company_id = userData.Company_Id;
                userReq.branch_id = userData.BranchId;
                userReq.user_id = userData.UserId;
                userReq.role_id = userData.RoleId;
                if (Session["loanCode"] != null)
                {
                    loancod = Session["loanCode"].ToString();
                }
                if (Session["pagetitle"] != null)
                {
                    page_nam = Session["pagetitle"].ToString();
                }
                userReq.loan_code = loancod;
                userReq.page_name = page_nam;
                userReq.topic = userReq.topic;
                userReq.message = userReq.message;
                userReq.priority_level = "high";

                UserRequestAccess userreqAccsss = new UserRequestAccess();
                int reslt = userreqAccsss.InsertUserRequest(userReq);
                if (reslt >= 0)
                {
                    string body = "User Name     :" + userData.UserName + "< br />" +
                                  "Position      :" + (string)Session["searchType"] + "< br />" +
                                  "Company       :" + userData.CompanyName + "< br />" +
                                  "Branch        :" + userData.BranchName + "< br />" +
                                  "Loan          :" + loancod + "< br />" +
                                  "Date and Time :" + DateTime.Now + "< br />" +
                                  "Title         :" + userReq.topic + "< br />" +
                                  "Message       :" + userReq.message + "< br />" +
                                  "Page          :" + page_nam + "< br />";

                    Email email = new Email("asanka@thefuturenet.com");
                    email.SendMail(body, "User Request From Dealer Floor Plan Management Software");

                    ViewBag.SuccessMsg = "Response will be delivered to your program inbox";
                    return RedirectToAction("UserRequestMessage", "UserManagement");
                }
                else
                {
                    ViewBag.SuccessMsg = "Error Occured";
                    return RedirectToAction("UserRequestMessage");
                }
            //}
            //else {
            //    TempData["message"] = userReq.message;
            //    TempData["topic"] = userReq.message;
            //    return RedirectToAction("UserRequestMessage");
            //}


           
        }

        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/06/27
        /// 
        /// This action for get Captcha Image
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        // This is for output cache false
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] 
        public FileResult GetCaptchaImage()
        {
            CaptchaRandomImage CI = new CaptchaRandomImage();
            // here 5 means I want to get 5 char long captcha
            this.Session["CaptchaImageText"] = CI.GetRandomString(5); 

            CI.GenerateImage(this.Session["CaptchaImageText"].ToString(), 150, 50, Color.Black, Color.LightBlue);
            MemoryStream stream = new MemoryStream();
            CI.Image.Save(stream, ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(stream, "image/png");
        }

        /// <summary>
        /// CreatedBy: Asanka Senarathna
        /// CreatedDate: 30/31/2016
        /// Update View answer in user request table
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UserRequestAns(User user)
        {
            int currentUserId;

            try
            {
                currentUserId = userData.UserId;
                UserRequestAccess userRequest = new UserRequestAccess();
                userRequest.UpdateUserViewAnswer(currentUserId);

                return PartialView();
            }
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
            }
        }

        ///<summary>
        /// Frontend page: Inactive Loan
        /// Title: create view and get active loan details for inactive
        /// Designed : Asanka Senarathna
        /// User story: DFP-103
        /// Developed: Asanka Senarathna
        /// Date created: 6/27/2016
        ///</summary>

        public ActionResult InactiveLoan()
        {
            Session.Remove("popUpSelectionType");
            Loan loan = new Loan();
            // If session not null then assign value to loan object
            if (Session["oneLoanDashboard"] != null)
            {
                loan = (Loan)Session["oneLoanDashboard"];
            }
            if (Session["loanDashboardAssignUser"] != null)
            {
                loan = (Loan)Session["loanDashboardAssignUser"];
            }
            if (Session["loanDashboardActiveLoanInact"] != null)
            {
                loan = (Loan)Session["loanDashboardActiveLoanInact"];
            }
            if (TempData["EditReslt"] != null)
            {
                //Check pass value in view and display message
                if ((string)TempData["EditReslt"] == "success")
                {
                    ViewBag.SuccessMsg = "Loan Status Successfully Updated";
                    if ((Session["loanDashboardActiveLoanInact"] != null) && (Session["loanDashboardActiveLoanInact"].ToString() != ""))
                    {
                        loan.CurrentLoanStatus = true;
                    }

                    Session["loanDashboardActiveLoanInact"] = loan;
                }
                else if ((string)TempData["EditReslt"] == "failed")
                {
                    ViewBag.ErrorMsg = "Failed To Update Loan Status";
                }
            }
            if ((Session["loanDashboardActiveLoanInact"] != null) && (Session["loanDashboardActiveLoanInact"].ToString() != ""))
            {

                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return PartialView(loan);
                }
                else
                {

                    return View(loan);
                }
            }
            else {
                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return RedirectToAction("UserDetails");
                }
                else
                {
                    return RedirectToAction("UserDetails");
                }
            }
        }
       
        ///<summary>
        /// Frontend page: Post Method for Inactive Loan
        /// Title: create view and get active loan details for inactive in Post method
        /// Designed : Asanka Senarathna
        /// User story: DFP-103
        /// Developed: Asanka Senarathna
        /// Date created: 6/27/2016
        ///</summary>
        /// <param name="slctdLoanId"></param>
        /// <param name="slctdLoanCode"></param>
        public void UpdateLoanStatus_ActiveInactive(int slctdLoanId, string slctdLoanCode)
        {
            //check Loan ID and Loan code has value for update loan
            if ((slctdLoanId > 0) && (!string.IsNullOrEmpty(slctdLoanCode)))
           {
                LoanSetupAccess ls = new LoanSetupAccess();
                int reslt = ls.UpdateLoanStatus_ActiveInactive(slctdLoanId, slctdLoanCode);
                //Update loan statues as Inactive(set loan_status = 0)
                if (reslt == 1)
                {
                    Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, 0, "Inactive Loan", "Loan Id : " + slctdLoanId + " ,Edited Status : Inactive", DateTime.Now);
                    //Insert new record to log 
                    int islog = (new LogAccess()).InsertLog(log);
                    TempData["EditReslt"] = "success";
                }
                else
                {
                    TempData["EditReslt"] = "failed";
                }
            }
            else
            {
                TempData["EditReslt"] = "failed";
            }
        }
        /// <summary>
        /// Frontend page: Assign rights
        /// Title: Assign new rights for a loan for users and edit rights
        /// Designed : Kasun Samarawickrama
        /// User story: 
        /// Developed: Kasun Samarawickrama
        /// Date created: 
        /// </summary>
        /// <returns></returns>
        public ActionResult AssignRights()
        {
            Session.Remove("popUpSelectionType");
            Loan loan = new Loan();
            // get loan details from session
            if (Session["oneLoanDashboard"] != null)
            {
                loan = (Loan)Session["oneLoanDashboard"];
                //Session.Remove("oneLoanDashboard");
            }
            if (Session["loanDashboardAssignUser"] != null)
            {
                loan = (Loan)Session["loanDashboardAssignUser"];
            }
            //check from submission 
            if (TempData["submit"] != null) {
                if ((string)TempData["submit"] == "success") {
                    ViewBag.SuccessMsg = "User Rights Successfully Updated";
                }
                else if ((string)TempData["submit"] == "failed")
                {
                    ViewBag.ErrorMsg = "Failed To Update User Rights";
                }
            }
            // check loan null or not
            if (Session["oneLoanDashboard"] != null || Session["loanDashboardAssignUser"] != null)
            {
                ViewBag.LoanId = loan.LoanId;
                ViewBag.LoanNumber = loan.LoanNumber;
                UserManageAccess ua = new UserManageAccess();

                // get user list for that branch
                List<User> userList = ua.getUsersByRoleBranch(3, loan.BranchId);
                List<User> tempRoleList = new List<User>();

                // add users to select list for front end 
                for (int i = 0; i < userList.Count; i++)
                {
                    User tempRole = new User()
                    {
                        UserId = userList[i].UserId,
                        UserName = userList[i].UserName
                    };
                    tempRoleList.Add(tempRole);
                }
                ViewBag.userSelectList = tempRoleList;

                User user = new Models.User();
                List<Right> list = new List<Right>();

                user.UserRightsList = new List<Right>();

                // retrive all rights from table
                list = (new UserRightsAccess()).getRights();

                // check title track yes or no.
                if ( loan.IsTitleTrack !=  1)
                {
                    foreach (var x in list)
                    {
                        // title page right
                        if (x.rightId != "U02")
                        {
                            user.UserRightsList.Add(x);
                        }
                    }
                }

                else {
                    user.UserRightsList = list;
                }

                //get all report list
                
                List<Right> ReportRightsList = (new UserRightsAccess()).getReportRights();
                user.ReportRightsList = new List<Right>();
                // filter report rights according to the loan
                if (ReportRightsList != null && ReportRightsList.Count > 0)
                {
                    foreach (Right rgt in ReportRightsList)
                    {
                        //Check dealer user can view the report
                        if (!rgt.DealerView)
                        {
                            continue;
                        }
                        else
                        {
                            //check title need not to be tracked for selected loan and report right for Title Status
                            if ((loan.IsTitleTrack == 0) && (rgt.rightId == "R04"))
                            {
                                //if title need not to be tracked report right for Title Status is not added to right list
                                continue;
                            }
                            //check there is no advance fee for selected loan and report right for advance fee invoice and advance fee receipt
                            if ((loan.AdvanceFee == 0) && ((rgt.rightId == "R07") || (rgt.rightId == "R08")))
                            {
                                //if there is no advance fee, report right for advance fee invoice and advance fee receipt are not added to right list
                                continue;
                            }
                            //check there is no monthly loan fee for selected loan and report right for monthly loan fee invoice and monthly loan fee receipt
                            if ((loan.MonthlyLoanFee == 0) && ((rgt.rightId == "R09") || (rgt.rightId == "R10")))
                            {
                                //if there is no monthly loan fee, report right for monthly loan fee invoice and monthly loan fee receipt are not added to right list
                                continue;
                            }
                            //check there is no lot inspection fee for selected loan and report right for lot inspection fee invoice and lot inspection fee receipt
                            if ((loan.LotInspectionFee == 0) && ((rgt.rightId == "R11") || (rgt.rightId == "R12")))
                            {
                                //if there is no lot inspection fee, report right for lot inspection fee invoice and lot inspection fee receipt are not added to right list
                                continue;
                            }
                        }
                        user.ReportRightsList.Add(rgt);
                    }
                }
                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return PartialView(user);
                }
                else
                {
           
                    return View(user);
                }
                //return View();
            }
            else {
            if (HttpContext.Request.IsAjaxRequest())
            {
                ViewBag.AjaxRequest = 1;
                    return RedirectToAction("UserDetails");
            }
            else
            {

                    return RedirectToAction("UserDetails");
                }
            }
        }
        /// <summary>
        /// Frontend page: Assign rights(post)
        /// Title: Assign new rights for a loan for users and edit rights
        /// Designed : Kasun Samarawickrama
        /// User story: 
        /// Developed: Kasun Samarawickrama
        /// Date created: 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AssignRights(User user)
        {
            // add rigts list to array and check active rights to a permission strinng which contain comma seperated rights
            string[] arrList = new string[user.UserRightsList.Count];
            int i = 0;
            foreach (var x in user.UserRightsList) {
                if (x.active) {
                    arrList[i] = x.rightId;
                    i++;
                }
            }

            arrList = arrList.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            // converting user right list to comma seperated string.
            user.UserRights = string.Join(",", arrList);

            // update user right details
            int check = (new UserAccess()).updateUserRightDetails(user,userData.UserId);
            if (check == 1)
            {
                // insert log 
                Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId,user.LoanId, "Assign User Rights", "Assign user rights for User ID: " + user.UserId, DateTime.Now);

                int islog = (new LogAccess()).InsertLog(log);
                TempData["submit"] = "success";

            }

            
            else
                TempData["submit"] = "failed";

            return RedirectToAction("AssignRights");
            //return View();

        }
        /// <summary>
        /// Frontend page: Assign rights
        /// Title: Get Existing user rights for a loan
        /// Designed : Kasun Samarawickrama
        /// User story: 
        /// Developed: Kasun Samarawickrama
        /// Date created: 
        /// </summary>
        /// <returns></returns>
        public string ExistingUserRights(int userId, int loanId)
        {
            User usr = new User();
            List<Right> rights = new List<Right>();
            // page rights and report rights retrive
            rights = (new UserRightsAccess()).getRightsString(userId, loanId);
            string str1 = "";
            string str2 = "";
            string str = "";
            // check rights empty
            if (rights != null && rights.Count >0)
            {
                //page rights
                str1 = rights[0].rightsPermissionString;
                //report rights
                str2 = rights[0].reportRightsPermissionString;
            }
            // combine rights for a one string seperated by : mark
            str = str1 +":"+ str2;
            return str;
        }

        /// <summary>
        /// CreatedBy : Piyumi
        /// CreatedDate: 2016/04/20
        /// 
        /// edit loan
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult SelectInActiveLoan(string type)
        {
            if ((userData.RoleId == 1) || (userData.RoleId == 2))
            {
                LoanSelection detail = (new UnitAccess()).GetInActiveLoans(userData.UserId, userData.Company_Id, userData.BranchId, userData.RoleId);

                if (detail != null)
                {
                    Session["InActiveLoanDetails"] = detail;
                }

                //Session["detail"] = detail;

                int userId = userData.UserId; 
                // if Session is expired throw an error


                loanSelection.RegBranches = new List<Branch>();
                loanSelection.NonRegBranchList = new List<NonRegBranch>();
                loanSelection.LoanList = new List<LoanSetupStep1>();


                //getting user role
                UserAccess ua = new UserAccess();



                // curUser.Company_Id   asanka 8/3/2016
                //create list for nonRegisterCompaniers

                List<NonRegBranch> NonRegisteredBranchLists = detail.NonRegBranchList; //(new BranchAccess()).getNonRegBranches(userData.Company_Id);

                if (userData.RoleId == 1)
                {

                    loanSelection.RegBranches = detail.RegBranches; //(new BranchAccess()).getBranches(userData.Company_Id);

                    if (loanSelection.RegBranches.Count() == 1)
                    {



                        // the get non registered branches details for perticular branch  from the non registeres branches list
                        foreach (NonRegBranch branch in NonRegisteredBranchLists)
                        {
                            if (branch.BranchId == loanSelection.RegBranches[0].BranchId)
                            {

                                loanSelection.NonRegBranchList.Add(branch);


                            }
                        }



                        if (loanSelection.NonRegBranchList.Count() == 1)
                        {

                            List<LoanSetupStep1> loanLists = detail.LoanList; //new LoanSetupAccess().GetLoanDetailsByNonRegBranchId(loanSelection.NonRegBranchList[0].NonRegBranchId);
                            loanSelection.LoanList = new List<LoanSetupStep1>();
                            foreach (LoanSetupStep1 loan in loanLists)
                            {
                                // if(loan.LoanStatus == true)
                                //  {
                                loanSelection.LoanList.Add(loan);
                                //  }
                            }

                            //if loans count is one redirect to add unit page
                        }
                    }

                }
                else if (userData.RoleId == 2)
                {

                    //loanSelection.RegBranches.Add((new BranchAccess()).getBranchByBranchId(userData.BranchId));

                    loanSelection.RegBranches.Add(detail.RegBranches[0]);


                    // the get non registered branches details for perticular branch  from the non registeres branches list
                    foreach (NonRegBranch branch in NonRegisteredBranchLists)
                    {
                        if (branch.BranchId == userData.BranchId)
                        {

                            loanSelection.NonRegBranchList.Add(branch);


                        }
                    }
                    if (loanSelection.NonRegBranchList.Count() == 1)
                    {
                        loanSelection.LoanList = detail.LoanList; //new LoanSetupAccess().GetLoanDetailsByNonRegBranchId(loanSelection.NonRegBranchList[0].NonRegBranchId);

                    }
                }
                Session["popUpType"] = type;
                if (type == "tidenaol") // for add unit page
                {
                    ViewBag.type = "EditLoan";
                    return PartialView(loanSelection);
                }

                else
                {
                    return View();
                }


                //return PartialView(loanSelection);


                //return View();
            }

            else
            {
                return RedirectToAction("UserLogin", "Login");
            }
        }

        /// <summary>
        /// CreatedBy : Piyumi
        /// CreatedDate: 2016/04/20
        /// 
        /// edit loan
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult EditLoan()
        {
            Session.Remove("popUpSelectionType");
            Loan loan = new Loan();
            if (Session["oneLoanDashboard"] != null)
            {
                loan = (Loan)Session["oneLoanDashboard"];
                //Session.Remove("oneLoanDashboard");
            }
            if (Session["loanDashboardAssignUser"] != null)
            {
                loan = (Loan)Session["loanDashboardAssignUser"];
            }
            if (Session["loanDashboardEditLoan"] != null)
            {
                loan = (Loan)Session["loanDashboardEditLoan"];
            }
            if (TempData["EditReslt"] != null)
            {
                if ((string)TempData["EditReslt"] == "success")
                {
                    ViewBag.SuccessMsg = "Loan Status Successfully Updated";
                    if ((Session["loanDashboardEditLoan"] != null) && (Session["loanDashboardEditLoan"].ToString() != ""))
                    {
                        loan.CurrentLoanStatus = true;
                    }

                    Session["loanDashboardEditLoan"] = loan;
                    //loan = new Loan();
                    //return View(loan);
                }
                else if ((string)TempData["EditReslt"] == "failed")
                {
                    ViewBag.ErrorMsg = "Failed To Update Loan Status";
                }
            }
            if ((Session["loanDashboardEditLoan"] != null)&&(Session["loanDashboardEditLoan"].ToString()!=""))
            {
            
                
                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return PartialView(loan);
                }
                else
                {

                    return View(loan);
                }
                //return View();
            }
            else {
                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return RedirectToAction("UserDetails");
                }
                else
                {

                    return RedirectToAction("UserDetails");
                }
            }
            //return View();
        }

        /// <summary>
        /// CreatedBy : Piyumi
        /// CreatedDate: 2016/04/20
        /// 
        /// edit loan - post
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
       
        public void UpdateLoanStatus(int slctdLoanId,string slctdLoanCode,string slctdActiveDate)
        {
           if((slctdLoanId>0)&&(!string.IsNullOrEmpty(slctdLoanCode)) && (!string.IsNullOrEmpty(slctdActiveDate))) 
           {
                LoanSetupAccess ls = new LoanSetupAccess();
                int reslt = ls.UpdateLoanStatus(slctdLoanId, slctdLoanCode, slctdActiveDate);
                if(reslt==1) 
                {
                    Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, 0, "Edit Loan", "Loan Id : " + slctdLoanId+" ,Edited Status : Active", DateTime.Now);

                    int islog = (new LogAccess()).InsertLog(log);
                    TempData["EditReslt"] = "success";
                    //Session["loanDashboardEditLoan"] = "";
                }
                else 
                {
                    TempData["EditReslt"] = "failed";
                }
           }
            else
            {
                TempData["EditReslt"] = "failed";
            }
        }

        /// <summary>
        /// CreatedBy : Piyumi
        /// CreatedDate: 2016/04/22
        /// 
        /// edit user(not include edit rights)
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult EditUserAtDashboard()
        {
            if (Session["AuthenticatedUser"] != null)
            {
                ViewBag.UserRole = userData.RoleId;

                if(TempData["UpdteReslt"]!=null) 
                {
                if(int.Parse(TempData["UpdteReslt"].ToString())==1) 
                {
                        ViewBag.SuccessMsg = "User is successfully updated";
                }
                   else if (int.Parse(TempData["UpdteReslt"].ToString()) == 0)
                    {
                        ViewBag.ErrorMsg = "Failed to update user";
                    }
                    else if (int.Parse(TempData["UpdteReslt"].ToString()) == -1)
                    {
                        ViewBag.ErrorMsg = "Failed to update user";
                    }
                }

                RoleAccess ra = new RoleAccess();
                List<UserRole> roleList = ra.GetAllUserRoles(userData.Company_Id);
                List<UserRole> tempRoleList = new List<UserRole>();

                for (int i = 0; i < roleList.Count; i++)
                {
                    if ((userData.RoleId == 2) && (roleList[i].RoleId == 1))
                    {
                        continue;
                    }
                    //if (roleList[i].RoleId == 4)
                    //{
                    //    continue;
                    //}
                    UserRole tempRole = new UserRole()
                    {
                        RoleId = roleList[i].RoleId,
                        RoleName = roleList[i].RoleName
                    };
                    tempRoleList.Add(tempRole);
                }

                ViewBag.RoleId = new SelectList(tempRoleList, "RoleId", "RoleName");
                if ((userData.RoleId == 1) ||(userData.RoleId == 2))
                {
                    User eum = new User();
                    List<User> usrList = new List<User>();
                    List<Branch> brList = new List<Branch>();
                    UserAccess uas = new UserAccess();
                    //usrList = uas.GetAllUsersByCompanyId(userData.Company_Id);

                    if (userData.RoleId == 1)
                    {
                        //get all branches for the company
                        BranchAccess ba = new BranchAccess();

                        eum.BranchList = ba.GetBranchesByCompanyId(userData.Company_Id);

                        if (eum.BranchList == null)
                        {
                            eum.BranchList = new List<Branch>();
                        }
                        eum.UserList = new List<User>();

                        ViewBag.BranchId = new SelectList(eum.BranchList, "BranchId", "BranchNameAddress");
                        ViewBag.UserId = new SelectList(eum.UserList, "UserId", "UserName");

                        //return View(eum);
                    }
                    else if (userData.RoleId == 2)
                    {
                        eum.BranchList = new List<Branch>();
                        eum.UserList = new List<User>();
                        ViewBag.BranchId = new SelectList(eum.BranchList, "BranchId", "BranchNameAddress");
                        ViewBag.UserId = new SelectList(eum.UserList, "UserId", "UserName");
                    }


                    return View(eum);
                }
               
                else
                {
                    return RedirectToAction("UserLogin", "Login");
                }
            }
            else
            {
                return RedirectToAction("UserLogin", "Login");
            }
            //return View();
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate: 4/22/2016
        /// Get Users By BranchId
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUsersByBranchId(int roleId,int branchId)
        {
        if(branchId==0) 
        {
            if(userData.BranchId>0) 
            {
                        branchId = userData.BranchId;
            }
        }
            if ((branchId > 0)&&(roleId>1))
            {
                User eum = new User();
                UserAccess uas = new UserAccess();
                eum.UserList = uas.GetAllUsersByBranchId(roleId,branchId);
                 if(eum.UserList != null) 
                {
                    //SelectList UserList1 = new SelectList(eum.UserList, "UserId", "UserName");
                    return Json(eum);
                }
               else 
               {
                    return RedirectToAction("UserLogin", "Login");
                }
            }

            else
            {
                return RedirectToAction("UserLogin", "Login");
            }
        }
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate: 4/22/2016
        /// Get super admins
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSuperAdminsDetails(int roleId)
        {
            if (roleId == 1)
            {
                User eum = new User();
                UserAccess uas = new UserAccess();
                eum.UserList = uas.GetSuperAdminsByCompanyId(userData.Company_Id);
                //SelectList UserList1 = new SelectList(eum.UserList, "UserId", "UserName");
                //return Json(UserList1);
               if (eum.UserList != null)
                {
                    //SelectList UserList1 = new SelectList(eum.UserList, "UserId", "UserName");
                    return Json(eum);
                }
                else
                {
                    return RedirectToAction("UserLogin", "Login");
                }
            }
            else
            {
                return RedirectToAction("UserLogin", "Login");
            }
        }
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate: 4/22/2016
        /// Get Users By UserId
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetUserByUserId(int userId)
        {
            if (userId > 0)
            {
                User userObj = new User();
                UserAccess uas = new UserAccess();
                userObj = uas.retreiveUserByUserId(userId);
                if (userObj.UserId > 1)
                {
                    userObj.PhoneNumber2 = userObj.PhoneNumber;
                }
                //SelectList UserList1 = new SelectList(eum.UserList, "UserId", "UserName");
                return Json(userObj);
            }
            else
            {
                return RedirectToAction("UserLogin", "Login");
            }
        }
        
        public int CheckPasswd(int userId,string Cpwd)
        {
            if (userId > 0)
            {
                User userObj = new User();
                UserAccess uas = new UserAccess();
                userObj = uas.retreiveUserByUserId(userId);
                string passwordFromDB = userObj.Password;
                //user.Password = userObj.Password;
                char[] delimiter = { ':' };

                string[] split = passwordFromDB.Split(delimiter);

                var checkCharHave = passwordFromDB.ToLowerInvariant().Contains(':');

                if (passwordFromDB == null || (checkCharHave == false))
                {
                   // return RedirectToAction("UserLogin", "Login", new { lbl = "Incorrect Username or Password, please confirm and submit." });
                }

                string passwordEncripted = PasswordEncryption.encryptPassword(Cpwd, split[1]);
                int reslt = 0;
                if (string.Compare(passwordEncripted, passwordFromDB) == 0)
                {
                    reslt = 1;

                }
                else
                {
                    reslt = 0;
                    
                }
                return reslt;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// CreatedBy : Piyumi
        /// CreatedDate: 2016/04/22
        /// 
        /// post method - edit user(not include edit rights)
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult EditUserAtDashboard(User user)
        {
        if(user!=null) 
        {
                if ((!string.IsNullOrEmpty(user.CurrentPassword)) && (!string.IsNullOrEmpty(user.Password)) && (!string.IsNullOrEmpty(user.ConfirmPassword)))
                {
                    User userObj = new User();
                    userObj = (new UserAccess()).retreiveUserByUserId(user.UserId);
                    string passwordFromDB = userObj.Password;
                    //user.Password = userObj.Password;
                    char[] delimiter = { ':' };

                    string[] split = passwordFromDB.Split(delimiter);

                    var checkCharHave = passwordFromDB.ToLowerInvariant().Contains(':');

                    if (passwordFromDB == null || (checkCharHave == false))
                    {
                        return RedirectToAction("UserLogin", "Login");
                    }

                    string passwordEncripted = PasswordEncryption.encryptPassword(user.CurrentPassword, split[1]);

                    if (string.Compare(passwordEncripted, passwordFromDB) == 0)
                    {
                        string passwordEncripted1 = PasswordEncryption.encryptPassword(user.Password, split[1]);
                        user.Password = passwordEncripted1;
                        user.CurrentPassword = passwordFromDB;

                    }
                    else
                    {

                        TempData["UpdteReslt"] = -1;
                        return RedirectToAction("EditUserAtDashboard");
                        //return View();
                    }
                    //string newSalt = PasswordEncryption.RandomString();
                    //user.CurrentPassword = PasswordEncryption.encryptPassword(user.CurrentPassword, newSalt);
                    //user.Password = PasswordEncryption.encryptPassword(user.Password, newSalt);
                }

                UserAccess usrAcc = new UserAccess();
                int reslt = usrAcc.UpdateUser(user,userData.UserId);
                if((reslt==1)||(reslt == 2)) 
                {
                    Log log = new Log(userData.UserId, userData.Company_Id, user.BranchId, 0, "Edit User", "Edit User : " + user.UserName, DateTime.Now);

                    int islog = (new LogAccess()).InsertLog(log);

                    if ((user.UserId == userData.UserId) && (!user.Status))
                    {
                        Session["AuthenticatedUser"] = null;
                    }
                    else
                    {
                        TempData["UpdteReslt"] = 1;
                    }
                    


                }
                else 
                {
                    TempData["UpdteReslt"] = 0;
                }
               
                return RedirectToAction("EditUserAtDashboard");
            }
        else 
        {
                return RedirectToAction("UserLogin", "Login");
        }
            
        }

        /// <summary>
        /// Frontend page: Edit Company
        /// Title: Get company details for edit company view
        /// Designed : Piyumi P
        /// User story:
        /// Developed: Piyumi P
        /// Date created: 2016/05/03
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult EditCompany()
        {
            //check user is super admin
            if (userData.RoleId == 1)
            {
                
                Company cmp = new Company();
                CompanyAccess ca = new CompanyAccess();
                //Get states to list
                List<State> stateList = ca.GetAllStates();
                ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");
                //check result of update company is not null and value is 1
                if (TempData["updateComReslt"]!=null && int.Parse(TempData["updateComReslt"].ToString()) == 1)
                {
                    ViewBag.SuccessMsg = "Company is updated successfully";
                    
                }
                //check result of update company is not null and value is 0
                else if (TempData["updateComReslt"] != null && int.Parse(TempData["updateComReslt"].ToString()) == 0)
                {
                    ViewBag.ErrorMsg = "Failed to update company";
                }
               
               //get company details of given company id
                cmp = ca.GetCompanyDetailsCompanyId(userData.Company_Id);
                //check company object is not null
                if (cmp != null)
                {
                    //return company object to view
                    return View(cmp);
                }
                else
                {
                    //return empty company object to view
                    cmp = new Company();
                    return View(cmp);
                }
            }
            else
            {
                //if user is not a super admin return to login page
                return new HttpStatusCodeResult(404);
            }
        }

        /// <summary>
        /// Frontend page: Edit Company
        /// Title: Update company details
        /// Designed : Piyumi P
        /// User story:
        /// Developed: Piyumi P
        /// Date created: 05/03/2016
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult EditCompany(Company company)
        {
            //check user is super admin
            if (userData.RoleId == 1)
            {
                //set zip code 
                company.Zip = company.ZipPre;
                if (company.Extension != null)
                    company.Zip += "-" + company.Extension;
                //get result of update company
                int reslt = (new CompanyAccess().UpdateCompany(company,userData.UserId));
                //check update result is 1
                if(reslt == 1)
                {
                    //insert log data
                    Log log = new Log(userData.UserId, userData.Company_Id, 0, 0, "Edit Company", "Edit Company : " + userData.Company_Id, DateTime.Now);

                    int islog = (new LogAccess()).InsertLog(log);
                    //assign result to TempData object
                    TempData["updateComReslt"] = reslt;
                    //check Session["AuthenticatedUser"] is not null
                    if (Session["AuthenticatedUser"] != null)
                    {
                        //assign updated company name to session company name 
                        ((User)Session["AuthenticatedUser"]).CompanyName = company.CompanyName;
                    }
                }
                else
                {
                    TempData["updateComReslt"] = 0;
                }
                //return to edit company view
                return RedirectToAction("EditCompany");
            }
            else
            {
                //if user is not super admin return to login page
                return new HttpStatusCodeResult(404);
            }
        }



        /// <summary>
        /// Frontend page: Create Branch(used in dashboard)
        /// Title: create view of create branch
        /// Designed : Asanka Senarathna
        /// User story:
        /// Developed: Asanka Senarathna
        /// Date created: 5/4/2016
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CreateDashboardBranch()
        {
            CompanyBranchModel userCompany;

            int userId = userData.UserId;
            int roleId = userData.RoleId;
            // check he is a super admin or admin


            if (roleId != 1)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            if (TempData["createBranchResult"] != null)
            {
                //
                if (int.Parse(TempData["createBranchResult"].ToString()) == 1)
                {
                    ViewBag.SuccessMsg = "Branch Successfully Created";
                }

                else if (int.Parse(TempData["createBranchResult"].ToString()) == 0)
                {
                    ViewBag.ErrorMsg = "Failed To Create Branch";
                }
            }

            userCompany = new CompanyBranchModel();

                ViewBag.BranchIndex = 0;

                //Get company details by company id
                CompanyAccess ca = new CompanyAccess();
                Company preCompany = ca.GetCompanyDetailsCompanyId(userData.Company_Id);

                BranchAccess ba = new BranchAccess();
                IList<Branch> branches = ba.getBranchesByCompanyCode(preCompany.CompanyCode);

                //Get states to list
                List<State> stateList = ca.GetAllStates();
                ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return PartialView();
                }
                else
                {

                    return View();
                }
        }

        /// <summary>
        /// Frontend page: Create Branch(used in dashboard) POST method
        /// Title: create view of create branch
        /// Designed : Asanka Senarathna
        /// User story:
        /// Developed: Asanka Senarathna
        /// Date created: 5/4/2016
        /// </summary>
        /// <param name="userCompany2"></param>
        /// <param name="branchCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateDashboardBranch(CompanyBranchModel userCompany2, string branchCode)
        {
            CompanyAccess userCompany = new CompanyAccess();
            
            int userId = userData.UserId;

            userCompany2.Company = userCompany.GetCompanyDetailsCompanyId(userData.Company_Id);
            userCompany2.MainBranch.StateId = userCompany2.StateId;
            userCompany2.MainBranch.BranchCode = branchCode;

            BranchAccess ba = new BranchAccess();
            //Insert record for Branch Table
            int reslt = ba.insertFirstBranchDetails(userCompany2, userId);

            //Create new record for company Step Table
            StepAccess sa = new StepAccess();
            sa.UpdateCompanySetupStep(userData.Company_Id, reslt, 3);

            if (reslt > 0)
            {
                TempData["createBranchResult"] = 1;
            }
            else
            {
                TempData["createBranchResult"] = 0;
            }

            return RedirectToAction("CreateDashboardBranch");
            
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult EditDashboardBranch()
        {
            CompanyBranchModel userCompany;

            //edit = 3;
            int userId = userData.UserId;
            int roleId = userData.RoleId;
            // check he is a super admin or admin


            if (roleId != 1)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            if (TempData["editBranchResult"] != null)
            {
                if (int.Parse(TempData["editBranchResult"].ToString()) == 1)
                {
                    ViewBag.SuccessMsg = "Branch is successfully updated";
                }

                else if (int.Parse(TempData["editBranchResult"].ToString()) == 0)
                {
                    ViewBag.ErrorMsg = "Failed To Update Branch";
                }
            }

            userCompany = new CompanyBranchModel();

            ViewBag.BranchIndex = 0;

            //Get company details by company id
            CompanyAccess ca = new CompanyAccess();
            Company preCompany = ca.GetCompanyDetailsCompanyId(userData.Company_Id);

            BranchAccess ba = new BranchAccess();
            IList<Branch> branches = ba.getBranchesByCompanyCode(preCompany.CompanyCode);
            userCompany.SubBranches = branches;

            //Get states to list
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            if (HttpContext.Request.IsAjaxRequest())
            {
                ViewBag.AjaxRequest = 1;
                return PartialView(userCompany);
            }
            else
            {

                return View(userCompany);
            }
        }

        [HttpPost]
        public ActionResult EditDashboardBranch(CompanyBranchModel userCompany2, string branchCode)
        {
            CompanyAccess userCompany = new CompanyAccess();

            int userId = userData.UserId;

            userCompany2.Company = userCompany.GetCompanyDetailsCompanyId(userData.Company_Id);
            userCompany2.MainBranch.StateId = userCompany2.StateId;
            userCompany2.MainBranch.BranchCode = branchCode;

            BranchAccess ba = new BranchAccess();
            if (string.IsNullOrEmpty(branchCode))
            {
                userCompany2.MainBranch.BranchCode = ba.createBranchCode(userCompany2.Company.CompanyCode);
            }

            int reslt = ba.insertFirstBranchDetails(userCompany2, userId);
            if (reslt == 0)
            {
                TempData["editBranchResult"] = 1;

                if (Session["loanDashboard"] != null)
                {
                    if (((Loan)Session["loanDashboard"]).BranchId == userCompany2.MainBranch.BranchId)
                    {
                        ((Loan)Session["loanDashboard"]).BranchName = userCompany2.MainBranch.BranchName;
                    }
                }
            }
            else
            {
                TempData["editBranchResult"] = 0;
            }

            return RedirectToAction("EditDashboardBranch");

        }

        static int _compType;


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CreatePartnerBranchAtDashboard(string lbls)
        {
            if (userData.RoleId != 1 && userData.RoleId != 2)
            {
                return RedirectToAction("UserDetails", "UserManagement");
            }

            if (lbls != null &&
                (lbls.Equals("Dealer branch is successfully inserted") ||
                 lbls.Equals("Lender branch is successfully inserted")))
            {
                ViewBag.SuccessMsg = lbls;
            }
            else if (lbls != null &&
                (lbls.Equals("Failed to udate")))
            {
                ViewBag.ErrorMsg = lbls;
            }

            BranchAccess ba = new BranchAccess();
            _compType = ba.getCompanyTypeByUserId(userData.UserId);

            //int compType = userData.CompanyType;
            if (_compType == 1)
            {
                ViewBag.ThisCompanyType = "Dealer";
            }
            else if (_compType == 2)
            {
                ViewBag.ThisCompanyType = "Lender";
            }
            else
            {
                ViewBag.compType = "";
            }

            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(userData.Company_Id);

            //filter admin's branch
            if (userData.RoleId == 2)
            {
                branchesLists = branchesLists.Where(x => x.BranchId == userData.BranchId).ToList();
            }

            ViewBag.RegBranchId = new SelectList(branchesLists, "BranchId", "BranchName");

            //Get all non reg companies
            CompanyAccess ca = new CompanyAccess();
            List<Company> nonRegCompanyList = ca.GetCompanyByCreayedCompany(userData.Company_Id);
            ViewBag.NonRegCompanyId = new SelectList(nonRegCompanyList, "CompanyId", "CompanyName", 1);

            //Get states to list
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            return View();
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CreatePartnerBranchAtDashboard(EditPartnerBranceModel model)
        {
            if (userData.RoleId != 1 && userData.RoleId != 2)
            {
                return RedirectToAction("UserDetails", "UserManagement");
            }

            CompanyBranchModel nonRegBranch = model.CompanyBranch;

            nonRegBranch.MainBranch.StateId = model.StateId;

            nonRegBranch.MainBranch.BranchCreatedBy = model.RegBranchId;
            nonRegBranch.MainBranch.BranchCompany = model.NonRegCompanyId;

            CompanyAccess ca = new CompanyAccess();
            BranchAccess ba = new BranchAccess();
            Company company = ca.GetNonRegCompanyByCompanyId(model.NonRegCompanyId);
            //nonRegBranch.MainBranch.BranchCode = ba.createNonRegBranchCode(company.CompanyCode);

            
            int reslt = ba.insertNonRegBranchDetails(nonRegBranch, userData.UserId, company.CompanyCode);

            if (reslt > 0)
            {
                //update Companay Step States in incomplete Branches continued in dashboard
                StepAccess sa = new StepAccess();
                sa.UpdateLoanSetupStep(userData.UserId, userData.Company_Id, model.RegBranchId, reslt, 0, 1);

                if (_compType == 1)
                {
                    ViewBag.SuccessMsg = "Dealer branch is successfully inserted";
                }
                else if (_compType == 2)
                {
                    ViewBag.SuccessMsg = "Lender branch is successfully inserted";
                }

                return RedirectToAction("CreatePartnerBranchAtDashboard", new { lbls = ViewBag.SuccessMsg });
            }
            else
            {
                ViewBag.ErrorMsg = "Failed to udate";
                return RedirectToAction("CreatePartnerBranchAtDashboard", new { lbls = ViewBag.ErrorMsg });
            }
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult EditPartnerBranchAtDashboard(string lbls)
        {
            if (userData.RoleId != 1 && userData.RoleId != 2)
            {
                return RedirectToAction("UserDetails", "UserManagement");
            }

            if (lbls != null &&
                (lbls.Equals("Dealer branch is successfully updated") ||
                 lbls.Equals("Lender branch is successfully updated")))
            {
                ViewBag.SuccessMsg = lbls;
            }
            else if (lbls != null &&
                (lbls.Equals("Failed to update")))
            {
                ViewBag.ErrorMsg = lbls;
            }

            BranchAccess ba = new BranchAccess();
            _compType = ba.getCompanyTypeByUserId(userData.UserId);

            //int compType = userData.CompanyType;
            if (_compType == 1)
            {
                ViewBag.ThisCompanyType = "Dealer";
            }
            else if (_compType == 2)
            {
                ViewBag.ThisCompanyType = "Lender";
            }
            else
            {
                ViewBag.compType = "";
            } 
            
            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(userData.Company_Id);

            //Get all non registered branches by company id
            EditPartnerBranceModel nonRegCompanyBranch = new EditPartnerBranceModel();
            
            List<NonRegBranch> nonRegBranches = ba.getNonRegBranches(userData.Company_Id);
            

            //filter admin's branch
            if (userData.RoleId == 2)
            {
                branchesLists = branchesLists.Where(x => x.BranchId == userData.BranchId).ToList();
                nonRegBranches = nonRegBranches.Where(x => x.BranchId == userData.BranchId).ToList();
            }
            nonRegCompanyBranch.NonRegBranches = nonRegBranches;
            ViewBag.RegBranchId = new SelectList(branchesLists, "BranchId", "BranchName");

            ViewBag.Count = nonRegBranches.Count;

            //Get all non reg companies
            CompanyAccess ca = new CompanyAccess();
            List<Company> nonRegCompanyList = ca.GetCompanyByCreayedCompany(userData.Company_Id);
            ViewBag.NonRegCompanyId = new SelectList(nonRegCompanyList, "CompanyId", "CompanyName", 1);

            //Get states to list
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            return View(nonRegCompanyBranch);
        }

        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult EditPartnerBranchAtDashboard(EditPartnerBranceModel model, string branchCode)
        {
            if (userData.RoleId != 1 && userData.RoleId != 2)
            {
                return RedirectToAction("UserDetails", "UserManagement");
            }

            CompanyBranchModel nonRegBranch = model.CompanyBranch;

            nonRegBranch.MainBranch.StateId = model.StateId;
            nonRegBranch.MainBranch.BranchCode = branchCode;

            nonRegBranch.MainBranch.BranchCreatedBy = model.RegBranchId;
            nonRegBranch.MainBranch.BranchCompany = model.NonRegCompanyId;

            BranchAccess ba = new BranchAccess();
            int reslt = ba.insertNonRegBranchDetails(nonRegBranch, userData.UserId);

            if (reslt > 0)
            {
                if (_compType == 1)
                {
                    ViewBag.SuccessMsg = "Dealer branch is successfully updated";
                }
                else if (_compType == 2)
                {
                    ViewBag.SuccessMsg = "Lender branch is successfully updated";
                }
                if (Session["loanDashboard"] != null)
                {
                    if (((Loan)Session["loanDashboard"]).NonRegBranchId == model.CompanyBranch.MainBranch.NonRegBranchId)
                    {
                        ((Loan)Session["loanDashboard"]).PartnerName = model.NonRegCompanyName+" - "+model.CompanyBranch.MainBranch.BranchName;
                    }
                }
                else if (Session["LoanOne"] != null)
                {
                    if (((Loan)Session["LoanOne"]).NonRegBranchId == model.CompanyBranch.MainBranch.NonRegBranchId)
                    {
                        ((Loan)Session["LoanOne"]).PartnerName = model.NonRegCompanyName + " - " + model.CompanyBranch.MainBranch.BranchName;
                    }
                }
                return RedirectToAction("EditPartnerBranchAtDashboard", new { lbls = ViewBag.SuccessMsg });
            }
            else
            {
                ViewBag.ErrorMsg = "Failed to update";
                return RedirectToAction("EditPartnerBranchAtDashboard", new { lbls = ViewBag.ErrorMsg });
            }
        }

        /// <summary>
        /// Frontend page: Create Partner Company
        /// Title: create view of create partner company
        /// Designed : Piyumi P
        /// User story:
        /// Developed: Piyumi P
        /// Date created: 5/4/2016
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CreatePartnerCompanyAtDashboard()
        {
            //check user is super admin or admin
            if ((userData.RoleId == 1) ||(userData.RoleId == 2))
            {
                //assign company type to variable
                int comType = userData.CompanyType;
                //company type 1 - Lender; Partner company type Dealer
                //company type 2 - Dealer; Partner company type Lender
                ViewBag.ThisCompanyType = (comType == 1) ? "Dealer" : "Lender";
                CompanyAccess ca = new CompanyAccess();
                //get all states
                List<State> stateList = ca.GetAllStates();
                ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");
                //check result of create partner company is not null and 1 - success
                if(TempData["partnerReslt"]!=null && int.Parse(TempData["partnerReslt"].ToString()) == 1)
                {
                    ViewBag.SuccessMsg = "Partner Company Created Successfully";
                }
                //check result of create partner company is not null and 0 - failure
                else if (TempData["partnerReslt"] != null && int.Parse(TempData["partnerReslt"].ToString()) == 0)
                {
                    ViewBag.ErrorMsg = "Failed to create partner company";
                }
                return View();
            }
            else
            {
                //if user is not a super admin or admin return to login page
                return new HttpStatusCodeResult(404);
            }
           
        }

        /// <summary>
        /// Frontend page: Create Partner Company
        /// Title: create new partner company
        /// Designed : Piyumi P
        /// User story:
        /// Developed: Piyumi P
        /// Date created: 5/4/2016
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CreatePartnerCompanyAtDashboard(PartnerCompany  partnerCompany)
        {
            //check user is super admin or admin
            if ((userData.RoleId == 1) || (userData.RoleId == 2))
            {
                //check object is not null
                if (partnerCompany != null)
                {
                  //set company zip code
                    partnerCompany.Zip = partnerCompany.ZipPre;
                    if (partnerCompany.Extension != null)
                        partnerCompany.Zip += "-" + partnerCompany.Extension;
                    //assign created by property value as logged user id
                    partnerCompany.CreatedBy = userData.UserId;
                    //assign created company type - (1-Lender, 2-Dealer)
                    partnerCompany.TypeId = (userData.CompanyType == 1) ? 2 : 1; ;
                   
                    CompanyAccess ca = new CompanyAccess();
                 
                    partnerCompany.RegCompanyId = userData.Company_Id; //regCompany.CompanyId;  asanka
                    //check result of create partner company
                    if (ca.InsertNonRegisteredCompanyAtDashboard(partnerCompany) == 1)
                    {
                        //assign result to TempData object
                        TempData["partnerReslt"] = 1;
                    }
                    else
                    {
                        TempData["partnerReslt"] = 0;
                    }
                    //return to create partner company page
                        return RedirectToAction("CreatePartnerCompanyAtDashboard");
                }
                else
                {
                    //if partner company object is null return to login page
                    return new HttpStatusCodeResult(404);
                }
            }
            else
            {
                //if user is not a super admin or admin return to login page
                return new HttpStatusCodeResult(404);
            }

            
        }

        public ActionResult EditProfile()
        {
            User userObj = new User();
            UserAccess uas = new UserAccess();
            userObj = uas.retreiveUserByUserId(userData.UserId);

            string roleName = "";

            if (userData.RoleId == 1)
                roleName = "Super Admin";
            else if (userData.RoleId == 2)
                roleName = "Admin";
            else if (userData.RoleId == 3)
                roleName = "User";
            else if (userData.RoleId == 4)
                roleName = "Dealer User";

            ViewBag.RoleName = roleName;

            if (TempData["UpdteReslt"] != null)
            {
                if (int.Parse(TempData["UpdteReslt"].ToString()) == 1)
                {
                    ViewBag.SuccessMsg = "Profile is successfully updated";
                }
                else if (int.Parse(TempData["UpdteReslt"].ToString()) == 0)
                {
                    ViewBag.ErrorMsg = "Failed to update Profile";
                }
                else if (int.Parse(TempData["UpdteReslt"].ToString()) == -1)
                {
                    ViewBag.ErrorMsg = "Failed to update Profile";
                }
            }
            userObj.PhoneNumber2 = userObj.PhoneNumber;
            return View(userObj);
        }

        [HttpPost]
        public ActionResult EditProfile(User model)
        {
            if (model != null)
            {
                if ((!string.IsNullOrEmpty(model.CurrentPassword)) && (!string.IsNullOrEmpty(model.Password)) && (!string.IsNullOrEmpty(model.ConfirmPassword)))
                {
                    User userObj = new User();
                    userObj = (new UserAccess()).retreiveUserByUserId(userData.UserId);
                    string passwordFromDB = userObj.Password;
                    //user.Password = userObj.Password;
                    char[] delimiter = { ':' };

                    string[] split = passwordFromDB.Split(delimiter);

                    var checkCharHave = passwordFromDB.ToLowerInvariant().Contains(':');

                    if (passwordFromDB == null || (checkCharHave == false))
                    {
                        return RedirectToAction("UserLogin", "Login", new { lbl = "Incorrect Username or Password, please confirm and submit." });
                    }

                    string passwordEncripted = PasswordEncryption.encryptPassword(model.CurrentPassword, split[1]);

                    if (string.Compare(passwordEncripted, passwordFromDB) == 0)
                    {
                        string passwordEncripted1 = PasswordEncryption.encryptPassword(model.Password, split[1]);
                        model.Password = passwordEncripted1;
                        model.CurrentPassword = passwordFromDB;

                    }
                    else
                    {

                        TempData["UpdteReslt"] = -1;
                        return RedirectToAction("EditProfile");
                        //return View();
                    }
                    //string newSalt = PasswordEncryption.RandomString();
                    //user.CurrentPassword = PasswordEncryption.encryptPassword(user.CurrentPassword, newSalt);
                    //user.Password = PasswordEncryption.encryptPassword(user.Password, newSalt);
                }
                model.UserId = userData.UserId;
                model.Status = true;
                UserAccess usrAcc = new UserAccess();
                int reslt = usrAcc.UpdateUser(model, userData.UserId);
                if ((reslt == 1) || (reslt == 2))
                {
                    Log log = new Log(userData.UserId, userData.Company_Id, model.BranchId, 0, "Edit User", "Edit User : " + model.UserName, DateTime.Now);

                    int islog = (new LogAccess()).InsertLog(log);
                    TempData["UpdteReslt"] = 1;
                }
                else
                {
                    TempData["UpdteReslt"] = 0;
                }
                return RedirectToAction("EditProfile");
            }

            return RedirectToAction("UserLogin", "Login");

            
        }


        /// <summary>
        /// Frontend page: Edit Partner Company
        /// Title: create view of Edit Partner Company
        /// Designed : Piyumi P
        /// User story:
        /// Developed: Piyumi P
        /// Date created: 5/4/2016
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPartnerCompanyAtDashboard()
        {
            //check user is super admin or admin
            if ((userData.RoleId == 1) || (userData.RoleId == 2))
            {
                CompanyAccess ca = new CompanyAccess();
                //get all states
                List<State> stateList = ca.GetAllStates();
                ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");
                PartnerCompany pc = new PartnerCompany();
                //get all non registered companies by registered company id
                pc.PartnerCompanyList = ca.GetNonRegCompanyDetailsByRegCompanyId2(userData.Company_Id);
                //check partner company list is null
                if (pc.PartnerCompanyList == null)
                {
                    //create empty partner company list
                    pc.PartnerCompanyList =  new List<PartnerCompany>();
                }
                //get company type by user id
                int comType = (new BranchAccess()).getCompanyTypeByUserId(userData.UserId);
                //company type 1(lender) - partner company type 2(dealer)
                //company type 2(dealer) - partner company type 1(lender)
                ViewBag.ThisCompanyType = (comType == 1) ? "Dealer" : "Lender";
                //check result of update partner company is null and value is 1
                if (TempData["partnerEditReslt"] != null && int.Parse(TempData["partnerEditReslt"].ToString()) == 1)
                {
                    //result 1 - success
                    ViewBag.SuccessMsg = "Partner Company Updated Successfully";
                }
                //check result of update partner company is null and value is 0
                else if (TempData["partnerEditReslt"] != null && int.Parse(TempData["partnerEditReslt"].ToString()) == 0)
                {
                    //result 0 - failure
                    ViewBag.ErrorMsg = "Failed to update partner company";
                }
                //return object to view
                return View(pc);
            }
            else
            {
                //if user is not super admin or admin return to login page
                return new HttpStatusCodeResult(404);
            }

        }

        /// <summary>
        /// Frontend page: Edit Partner Company
        /// Title: create view of Edit Partner Company
        /// Designed : Piyumi P
        /// User story:
        /// Developed: Piyumi P
        /// Date created: 5/4/2016
        /// </summary>
        /// <returns></returns>
        ///
        [HttpPost]
        public ActionResult EditPartnerCompanyAtDashboard(PartnerCompany partnerCompany)
        {
            //check user is super admin or admin
            if ((userData.RoleId == 1) || (userData.RoleId == 2))
            {
                //check object is not null
                if (partnerCompany != null)
                {
                    //assign logged user id to created by property
                    partnerCompany.CreatedBy = userData.UserId;
                    //assign logged user's company id to registered company id property
                    partnerCompany.RegCompanyId = userData.Company_Id;
                    //set zip code
                    partnerCompany.Zip = partnerCompany.ZipPre;
                    if (partnerCompany.Extension != null)
                        partnerCompany.Zip += "-" + partnerCompany.Extension;
                    //check result of update partner company is 1
                    if ( (new CompanyAccess()).UpdatePartnerCompany(partnerCompany) == 1)
                    {
                        TempData["partnerEditReslt"] = 1;
                       
                    }
                    else
                    {
                        TempData["partnerEditReslt"] = 0;
                    }
                    //return to edit partner company page
                    return RedirectToAction("EditPartnerCompanyAtDashboard");
                }
                else
                {
                    //if object is null return to login page
                    return new HttpStatusCodeResult(404);
                }
            }
            else
            {
                //if user is not super admin or admin return to login page
                return new HttpStatusCodeResult(404);
            }
           
        }
        }

    



}