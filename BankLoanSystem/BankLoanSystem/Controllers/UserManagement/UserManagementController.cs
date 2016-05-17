using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using BankLoanSystem.Code;
using System.Data;

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

                        filterContext.Result = new HttpStatusCodeResult(404, "Session Expired");

                    }else
                    {
                        filterContext.Result = new RedirectResult("~/Login/UserLogin");

                    }
                    //return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
                    
                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
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
                ViewBag.Company = userData.CompanyName;
                ViewBag.roleId = userData.RoleId;
                if (Session["loanDashboard"] != null)
                {
                    ViewBag.LoanCount = 1;
                    ViewBag.loanSelected = 1;
                    Loan loanSelected = (Loan)Session["loanDashboard"];
                    if (userData.RoleId == 2)
                    {
                        //loan = da.GetLoanDetails(userData.BranchId, 2);

                    }
                    else if (userData.RoleId == 1)
                    {
                        //loan = da.GetLoanDetails(userData.Company_Id, 1);

                    }
                    else if (userData.RoleId == 3)
                    {
                        //loan = da.GetLoanDetails(userData.UserId, 3);

                    }
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
                            if ((loanSelected.Rights != ""))
                            {
                                string[] charactors = loanSelected.Rights.Split(',');

                                List<string> rightList = new List<string>(charactors);
                                ViewBag.RightList = rightList;
                                Session["CurrentLoanRights"] = loanSelected.Rights;
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


                        //ViewBag.CompType = (new BranchAccess()).getCompanyTypeByUserId(userData.UserId);
                        //ViewBag.CompType 
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
                        if (userData.RoleId == 3)
                        {
                            Session["CurrentLoanRights"] = loan.Rights;
                        }
                        ViewBag.PartnerName = loan.PartnerName;
                        ViewBag.PartnerType = loan.PartnerType;
                        ViewBag.Branch = loan.BranchName;
                        ViewBag.LoanNum = loan.LoanNumber;
                        ViewBag.IsTitleTrack = loan.IsTitleTrack;
                        if ((loan.AdvanceFee==1) || (loan.LotInspectionFee==1) || (loan.MonthlyLoanFee==1))
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

                                ViewBag.RightList = loan.Rights;
                                
                            }
                            

                        }
                        else if((userData.RoleId == 1)||(userData.RoleId == 2))
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
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
            }
                
            
            
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
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
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
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
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
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
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

        /// <summary>
        /// CreatedBy : Irfan
        /// CreatedDate: 2016/01/25
        /// 
        /// to view the change Password model
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult ChangePassword()
        {


            return PartialView();
        }


        /// <summary>
        /// CreatedBy : Irfan
        /// CreatedDate: 2016/01/25
        /// 
        /// update the password model
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
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
                List<Right> permissionString = access.getRightsString(userId,0);
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
                if(detail!=null) 
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
            else if(!string.IsNullOrEmpty(type)) 
           {
                detail = (new UnitAccess()).GetPermisssionGivenLoanwithBranchDeatils(userData.UserId, userData.Company_Id, userData.BranchId, userData.RoleId);
                if (detail == null)
                {
                    ViewBag.type = "return";
                    return PartialView();
                }
                else if(detail!=null){
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

            }else if (userData.RoleId == 2)
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
                        Session["CurrentLoanRights"] = PermissionList(userData.UserId,l.loanId);
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
                    }else if ((string)Session["popUpSelectionType"] == "assignRights")
                    {
                        Session["loanDashboardAssignUser"] = finalSelectedLoan;

                    }
                    else if ((string)Session["popUpSelectionType"] == "tidenaol")
                    {
                        Session["loanDashboardEditLoan"] = finalSelectedLoan;

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
            Session["detail"] = "";
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
            else if ((string)Session["popUpSelectionType"] == "aticno")
            {
                return RedirectToAction("RenewLoan","LoanManagement");
            }
            else
            {
                return RedirectToAction("UserDetails");
            }
            //return RedirectToAction(action);
        }

        public List<Right> PermissionList(int userId,int loanId)
        {
            var access = new UserRightsAccess();

            //retrive all rights
            List<Right> rights = access.getRights();
            int userRole = (new UserManageAccess()).getUserRole(userId);

            if (userRole == 3)
            {
                //get permission string for the relevent user
                List<Right> permissionString = access.getRightsString(userId, loanId);
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
        /// CreatedBy: Piyumi
        /// CreatedDate: 4/1/2016
        /// Create user from dashboard
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

            if (roleId > 2)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            // check if   step is 3...

            if (TempData["createUserResult"] != null)
            {
            if(int.Parse(TempData["createUserResult"].ToString()) == 1) {
                    ViewBag.SuccessMsg = "User Successfully Created";
                }

                else if (int.Parse(TempData["createUserResult"].ToString()) == 0)
                {
                    ViewBag.ErrorMsg = "Failed To Create User";
                }
            }
            

            ViewBag.CurrUserRoleType = roleId;
            int loanCount = -1;
            if (userData.RoleId == 2)
            {
                //ViewBag.Branch = (ba.getBranchByBranchId(user.BranchId)).BranchName;
                loanCount = da.GetLoanCount(userData.BranchId, 2);
                

            }
            else if (userData.RoleId == 1)
            {
                loanCount = da.GetLoanCount(userData.Company_Id, 1);
                
            }
            RoleAccess ra = new RoleAccess();
            List<UserRole> roleList = ra.GetAllUserRoles();
            List<UserRole> tempRoleList = new List<UserRole>();

            for (int i = roleId - 1; i < roleList.Count && ViewBag.CurrUserRoleType != 3; i++)
            {
                if (roleList[i].RoleId == 4)
                {
                    continue;
                }
                else if((roleList[i].RoleId == 3) &&(loanCount==0)) 
                {
                    continue;
                }
                else if((userData.RoleId==2)&&(roleList[i].RoleId == 1)) {
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

            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(userData.Company_Id);
            List<Branch> branchesListAdmin = new List<Branch>();
            if (userData.RoleId == 1) {
                ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");
            }
            else {
                branchesListAdmin = branchesLists.FindAll(t => t.BranchId == userData.BranchId);
                ViewBag.BranchId = new SelectList(branchesListAdmin, "BranchId", "BranchName");
            }
           

            List<Branch> branchesListsLoan =  new List<Branch>();
            List<Branch> branchesListsLoanAd = new List<Branch>();
            branchesListsLoan = (new BranchAccess()).GetLoansBranches(userData.Company_Id);
            //List<Branch> branchesLists2 = new List<Branch>();
            //branchesLists2 = branchesListsLoan.Distinct().ToList();
            if (userData.RoleId == 1)
            {
                ViewBag.BranchIdUser = new SelectList(branchesListsLoan, "BranchId", "BranchName");
            }
            else {
                branchesListsLoanAd = branchesListsLoan.FindAll(t => t.BranchId == userData.BranchId);
                ViewBag.BranchIdUser = new SelectList(branchesListsLoanAd, "BranchId", "BranchName");
            }
            //List<Branch> branchesLists3 = new List<Branch>();
            //branchesLists3 = branchesListsLoan;
            //Session["BranchLoans"] = branchesLists3;
            //ViewBag.LoanId = new SelectList(branchesLists3, "LoanId", "BranchName");

            //List<Right> rightLists = new List<Right>();


            //rightLists = (new UserRightsAccess()).getRights();

            //us.UserRightsList = rightLists;

            //return PartialView(userViewModel);

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

        public ActionResult GetLoansByBranches(int BranchIdL) 
        {
            User us = new User();
            List<Branch> listLoan = new List<Branch>();
            List<Branch> listLoan2 = new List<Branch>();
            listLoan = (new BranchAccess()).GetLoansByBranches(BranchIdL);
            ViewBag.LoanId = new SelectList(listLoan,"LoanId","LoanNumber");
            List<Right> rightLists = new List<Right>();


            rightLists = (new UserRightsAccess()).getRights();

            us.UserRightsList = rightLists;

            //return PartialView(userViewModel);

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
        [HttpPost]
        public ActionResult CreateDashboardUser(User userObj)
        {

            userObj.PhoneNumber = userObj.PhoneNumber2;

            int currentUser = userData.UserId;

            //// check he is a super admin or admin
            int roleId = userData.RoleId;

            



            userObj.CreatedBy = currentUser;
            userObj.IsDelete = false;
            //userObj.Status = false;

            string passwordTemp = userObj.Password;

            UserAccess ua = new UserAccess();
           DashBoardAccess da = new DashBoardAccess();
            string newSalt = PasswordEncryption.RandomString();
            userObj.Password = PasswordEncryption.encryptPassword(userObj.Password, newSalt);

            userObj.Email = userObj.NewEmail;


            userObj.Company_Id = userData.Company_Id;//  company.CompanyId;  - asanka

            ////Set admin branch to new user 
            if (roleId == 2)
            {
                userObj.BranchId = userData.BranchId;
            }
            if ((userObj.RoleId == 1)&&(userData.RoleId==1))
            {
                userObj.step_status = userData.step_status;
            }
            else if (userObj.RoleId == 2)
            {
            int step= ua.GetStepStatusByUserBranchId(userObj.BranchId);
            if(step>=0) 
            {
                    userObj.step_status = step;
            }
                
            }
            else if (userObj.RoleId == 3)
            {
                userObj.step_status= 1;
                userObj.BranchId = userObj.BranchIdUser;
                string[] arrList = new string[userObj.UserRightsList.Count];
                int i = 0;
                foreach (var x in userObj.UserRightsList)
                {
                    if (x.active)
                    {
                        arrList[i] = x.rightId;
                        i++;
                    }
                }

                arrList = arrList.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                //user.UserRights = arrList.ToString();
                userObj.UserRights = string.Join(",", arrList);
            }
           
            //for(int i=0;i<userObj.UserRightsList.Count();i++) {
            //if(userObj.UserRightsList[i].active) {
            //        userObj.UserRights = userObj.UserRightsList[i].rightId + ",";
            //}

            //}
            //Insert user
            int res = da.InsertUserInDashboard(userObj);

            //Insert new user to user activation table
            //string activationCode = Guid.NewGuid().ToString();
            //int userId = (new UserAccess()).getUserId(userObj.Email);
            //res = ua.InsertUserActivation(userId, activationCode);
            if (res > 0)
            {
                if (userObj.Status)
                {

                string body = "Hi " + userObj.FirstName + "! <br /><br /> Your account has been successfully created. Below in your account detail." +
                              "<br /><br /> User name: " + userObj.UserName +
                                    "<br /> Password : <b>" + passwordTemp +
                                  //"<br />Click <a href='http://localhost:57318/CreateUser/ConfirmAccount?userId=" + userId + "&activationCode=" + activationCode + "'>here</a> to activate your account." +
                              "<br /><br/> Thanks,<br /> Admin.";

                Email email = new Email(userObj.Email);

              
                email.SendMail(body, "Account details");

                }




                //ViewBag.SuccessMsg = "User Successfully Created";
                //RoleAccess ra = new RoleAccess();
                //List<UserRole> roleList = ra.GetAllUserRoles();



                //ViewBag.RoleId = new SelectList(roleList, "RoleId", "RoleName");
                //List<Branch> branchesLists = (new BranchAccess()).getBranches(userData.Company_Id);
                //ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");
                //List<Branch> branchesLists2 = (new BranchAccess()).GetLoansBranches(userData.Company_Id);


                //ViewBag.BranchIdUser = new SelectList(branchesLists2, "BranchId", "BranchName");
                string roleName = "";
                if (userObj.RoleId == 1)
                {
                    roleName = "Super Admin";
                }
                else if (userObj.RoleId == 2)
                {
                    roleName = "Admin";
                }
                else if (userObj.RoleId == 3)
                {
                    roleName = "User";
                }
                Log log = new Log(userData.UserId, userData.Company_Id, userObj.BranchId, 0, "Create User", "Create "+roleName+" ,Username:"+userObj.UserName, DateTime.Now);

                int islog = (new LogAccess()).InsertLog(log);
                TempData["createUserResult"] = 1;
                //return RedirectToAction("CreateDashboardUser");

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

        public ActionResult UserRequestMessage()
        {
            int userrole = userData.RoleId;
            int userId = userData.UserId;

            return PartialView();
        }

        [HttpPost]
        [ActionName("UserRequestMessage")]
        public ActionResult UserRequestMessagePost(UserRequest userReq)
        {
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
            return View();
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
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
            }
        }

        public ActionResult AssignRights()
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
            if (TempData["submit"] != null) {
                if ((string)TempData["submit"] == "success") {
                    ViewBag.SuccessMsg = "User Rights Successfully Updated";
                }
                else if ((string)TempData["submit"] == "failed")
                {
                    ViewBag.ErrorMsg = "Failed To Update User Rights";
                }
            }
            if (Session["oneLoanDashboard"] != null || Session["loanDashboardAssignUser"] != null)
            {
                ViewBag.LoanId = loan.LoanId;
                ViewBag.LoanNumber = loan.LoanNumber;
            UserManageAccess ua = new UserManageAccess();
                List<User> userList = ua.getUsersByRoleBranch(3, loan.BranchId);
            List<User> tempRoleList = new List<User>();

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
            //ViewBag.RoleId = new SelectList(userList, "RoleId", "RoleName");
                User user = new Models.User();
                user.UserRightsList = new List<Right>();

                user.UserRightsList = (new UserRightsAccess()).getRights();
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
        [HttpPost]
        public ActionResult AssignRights(User user)
        {
            string[] arrList = new string[user.UserRightsList.Count];
            int i = 0;
            foreach (var x in user.UserRightsList) {
                if (x.active) {
                    arrList[i] = x.rightId;
                    i++;
                }
            }

            arrList = arrList.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //user.UserRights = arrList.ToString();
            user.UserRights = string.Join(",", arrList);
            int check = (new UserAccess()).updateUserRightDetails(user,userData.UserId);
            if(check==1)
                TempData["submit"] = "success";
            else
                TempData["submit"] = "failed";

            return RedirectToAction("AssignRights");
            //return View();

        }

        public string ExistingUserRights(int userId, int loanId)
        {
            User usr = new User();
            List<Right> rights = new List<Right>();
            rights = (new UserRightsAccess()).getRightsString(userId, loanId);
            string str="";
            if (rights != null && rights.Count >0)
            {
                str = rights[0].rightsPermissionString;
            }
            
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
       
        public void UpdateLoanStatus(int slctdLoanId,string slctdLoanCode)
        {
           if((slctdLoanId>0)&&(!string.IsNullOrEmpty(slctdLoanCode))) 
           {
                LoanSetupAccess ls = new LoanSetupAccess();
                int reslt = ls.UpdateLoanStatus(slctdLoanId, slctdLoanCode);
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
                        return RedirectToAction("UserLogin", "Login", new { lbl = "Incorrect Username or Password, please confirm and submit." });
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
                    TempData["UpdteReslt"] = 1;


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
        /// CreatedBy : Piyumi
        /// CreatedDate: 2016/05/03
        /// 
        /// edit company
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        
        public ActionResult EditCompany()
        {
            if (userData.RoleId == 1)
            {
                //Get states to list
                Company cmp = new Company();
                CompanyAccess ca = new CompanyAccess();
                List<State> stateList = ca.GetAllStates();
                ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");
                if (TempData["updateComReslt"]!=null && int.Parse(TempData["updateComReslt"].ToString()) == 1)
                {
                    ViewBag.SuccessMsg = "Company is updated successfully";
                    //cmp = new Company();
                    //return View(cmp);
                }
                else if (TempData["updateComReslt"] != null && int.Parse(TempData["updateComReslt"].ToString()) == 0)
                {
                    ViewBag.ErrorMsg = "Failed to update company";
                }
               
               
                cmp = ca.GetCompanyDetailsCompanyId(userData.Company_Id);
                if (cmp != null)
                {
                    return View(cmp);
                }
                else
                {
                    cmp = new Company();
                    return View(cmp);
                }
            }
            else
            {
                return new HttpStatusCodeResult(404);
            }
        }

        /// <summary>
        /// CreatedBy : Piyumi
        /// CreatedDate: 2016/05/03
        /// 
        /// edit company - post
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult EditCompany(Company company)
        {
            if (userData.RoleId == 1)
            {
                company.Zip = company.ZipPre;
                if (company.Extension != null)
                    company.Zip += "-" + company.Extension;
                int reslt = (new CompanyAccess().UpdateCompany(company,userData.UserId));
                if(reslt == 1)
                {
                    Log log = new Log(userData.UserId, userData.Company_Id, 0, 0, "Edit Company", "Edit Company : " + userData.Company_Id, DateTime.Now);

                    int islog = (new LogAccess()).InsertLog(log);
                    TempData["updateComReslt"] = reslt;
                }
                else
                {
                    TempData["updateComReslt"] = 0;
                }
                return RedirectToAction("EditCompany");
            }
            else
            {
                return new HttpStatusCodeResult(404);
            }
        }
   
            
     

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CreateDashboardBranch()
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

            if (TempData["createBranchResult"] != null)
            {
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
                //userCompany.SubBranches = branches;

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


        [HttpPost]
        public ActionResult CreateDashboardBranch(CompanyBranchModel userCompany2, string branchCode)
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
            if(reslt > 0)
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
            nonRegBranch.MainBranch.BranchCode = ba.createNonRegBranchCode(company.CompanyCode);

            
            int reslt = ba.insertNonRegBranchDetails(nonRegBranch, userData.UserId);

            if (reslt > 0)
            {
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

                return RedirectToAction("EditPartnerBranchAtDashboard", new { lbls = ViewBag.SuccessMsg });
            }
            else
            {
                ViewBag.ErrorMsg = "Failed to udate";
                return RedirectToAction("EditPartnerBranchAtDashboard", new { lbls = ViewBag.ErrorMsg });
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:5/4/2016
        /// Create Partner Company
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CreatePartnerCompanyAtDashboard()
        {
            if ((userData.RoleId == 1) ||(userData.RoleId == 2))
            {
                //int comType = (new BranchAccess()).getCompanyTypeByUserId(userData.UserId);
                int comType = userData.CompanyType;
                ViewBag.ThisCompanyType = (comType == 1) ? "Dealer" : "Lender";
                CompanyAccess ca = new CompanyAccess();
                List<State> stateList = ca.GetAllStates();
                ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

                //string type = (PartnerCompanyType == 1) ? "Lender" : "Dealer";
                if(TempData["partnerReslt"]!=null && int.Parse(TempData["partnerReslt"].ToString()) == 1)
                {
                    ViewBag.SuccessMsg = "Partner Company Created Successfully";
                }
                else if (TempData["partnerReslt"] != null && int.Parse(TempData["partnerReslt"].ToString()) == 0)
                {
                    ViewBag.ErrorMsg = "Failed to create partner company";
                }
                return View();
            }
            else
            {
                return new HttpStatusCodeResult(404);
            }
           
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:5/4/2016
        /// Create Partner Company
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult CreatePartnerCompanyAtDashboard(PartnerCompany  partnerCompany)
        {
            if ((userData.RoleId == 1) || (userData.RoleId == 2))
            {
                if (partnerCompany != null)
                {
                    partnerCompany.CompanyCode = (new GeneratesCode()).GenerateNonRegCompanyCode(partnerCompany.CompanyName);
                    partnerCompany.Zip = partnerCompany.ZipPre;
                    if (partnerCompany.Extension != null)
                        partnerCompany.Zip += "-" + partnerCompany.Extension;
                    partnerCompany.CreatedBy = userData.UserId;
                    partnerCompany.TypeId = (userData.CompanyType == 1) ? 2 : 1; ;
                   
                    CompanyAccess ca = new CompanyAccess();
                    //Company regCompany = ca.GetCompanyDetailsByFirstSpUserId(userId);

                    partnerCompany.RegCompanyId = userData.Company_Id; //regCompany.CompanyId;  asanka

                    //Company nonRegCom = partnerCompany.Company;
                    
                    if (ca.InsertNonRegisteredCompanyAtDashboard(partnerCompany) == 1)
                    {

                        TempData["partnerReslt"] = 1;
                    }
                    else
                    {
                        TempData["partnerReslt"] = 0;
                    }
                        return RedirectToAction("CreatePartnerCompanyAtDashboard");
                }
                else
                {
                    return new HttpStatusCodeResult(404);
                }
            }
            else
            {
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
        /// CreatedBy:Piyumi
        /// CreatedDate:5/4/2016
        /// Edit Partner Company
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPartnerCompanyAtDashboard()
        {
            if ((userData.RoleId == 1) || (userData.RoleId == 2))
            {
                CompanyAccess ca = new CompanyAccess();
                List<State> stateList = ca.GetAllStates();
                ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");
                PartnerCompany pc = new PartnerCompany();
                pc.PartnerCompanyList = ca.GetNonRegCompanyDetailsByRegCompanyId2(userData.Company_Id);
                if (pc.PartnerCompanyList == null)
                {
                    pc.PartnerCompanyList =  new List<PartnerCompany>();
                }
                
                int comType = (new BranchAccess()).getCompanyTypeByUserId(userData.UserId);
                //int comType = userData.CompanyType;
                ViewBag.ThisCompanyType = (comType == 1) ? "Dealer" : "Lender";

                //string type = (PartnerCompanyType == 1) ? "Lender" : "Dealer";
                if (TempData["partnerEditReslt"] != null && int.Parse(TempData["partnerEditReslt"].ToString()) == 1)
                {
                    ViewBag.SuccessMsg = "Partner Company Updated Successfully";
                }
                else if (TempData["partnerEditReslt"] != null && int.Parse(TempData["partnerEditReslt"].ToString()) == 0)
                {
                    ViewBag.ErrorMsg = "Failed to update partner company";
                }
                return View(pc);
            }
            else
            {
                return new HttpStatusCodeResult(404);
            }

        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:5/4/2016
        /// Edit Partner Company
        /// </summary>
        /// <returns></returns>
        ///
        [HttpPost]
        public ActionResult EditPartnerCompanyAtDashboard(PartnerCompany partnerCompany)
        {
            if ((userData.RoleId == 1) || (userData.RoleId == 2))
            {
                if (partnerCompany != null)
                {
                    partnerCompany.CreatedBy = userData.UserId;
                    partnerCompany.RegCompanyId = userData.Company_Id;
                    partnerCompany.Zip = partnerCompany.ZipPre;
                    if (partnerCompany.Extension != null)
                        partnerCompany.Zip += "-" + partnerCompany.Extension;
                    if ( (new CompanyAccess()).UpdatePartnerCompany(partnerCompany) == 1)
                    {
                        TempData["partnerEditReslt"] = 1;
                    }
                    else
                    {
                        TempData["partnerEditReslt"] = 0;
                    }
                    return RedirectToAction("EditPartnerCompanyAtDashboard");
                }
                else
                {
                    return new HttpStatusCodeResult(404);
                }
            }
            else
            {
                return new HttpStatusCodeResult(404);
            }
            //return View();
        }
        }
}