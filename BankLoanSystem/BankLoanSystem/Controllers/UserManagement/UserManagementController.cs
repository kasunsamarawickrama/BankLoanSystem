using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

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
                    //return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
                    filterContext.Result = new RedirectResult("~/Login/UserLogin");
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
            if (Session["AuthenticatedUser"] != null)
            {
                DashBoardAccess da = new DashBoardAccess();
                ViewBag.Username = userData.UserName;
                ViewBag.Company = userData.CompanyName;
                ViewBag.roleId = userData.RoleId;
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
                else if (userData.RoleId == 3)
                {
                    ViewBag.LoanCount = da.GetLoanCount(userData.UserId, 3);
                    ViewBag.Branch = userData.BranchName;
                    ViewBag.Position = "User";

                }
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

                        if (userData.RoleId == 3)
                        {
                            if ((loanSelected.Rights.Count() > 0) && (loan.Rights != null))
                            {
                                foreach (string s in loanSelected.Rights)
                                {
                                    if (s == "U004")
                                    {
                                        ViewBag.AddUnits = 1;
                                    }
                                    else
                                    {
                                        ViewBag.AddUnits = 0;
                                    }
                                    if ((s == "U006") || (s == "U007"))
                                    {
                                        ViewBag.ViewReports = 1;
                                    }
                                    else
                                    {
                                        ViewBag.ViewReports = 0;
                                    }
                                }
                            }

                        }

                        else
                        {
                            ViewBag.AddUnits = 1;
                            ViewBag.ViewReports = 1;
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
                else if (ViewBag.LoanCount == 1)
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
                    if (loan != null)
                    {
                       
                        ViewBag.PartnerName = loan.PartnerName;
                        ViewBag.PartnerType = loan.PartnerType;
                        ViewBag.Branch = loan.BranchName;
                        ViewBag.LoanNum = loan.LoanNumber;
                        ViewBag.IsTitleTrack = loan.IsTitleTrack;

                        if (userData.RoleId == 3)
                        {
                            if ((loan.Rights.Count() > 0) && (loan.Rights != null))
                            {
                                foreach (string s in loan.Rights)
                                {
                                    if (s == "U004")
                                    {
                                        ViewBag.AddUnits = 1;
                                    }
                                    else
                                    {
                                        ViewBag.AddUnits = 0;
                                    }
                                    if ((s == "U006") || (s == "U007"))
                                    {
                                        ViewBag.ViewReports = 1;
                                    }
                                    else
                                    {
                                        ViewBag.ViewReports = 0;
                                    }
                                }
                            }

                        }
                        else
                        {
                            ViewBag.AddUnits = 1;
                            ViewBag.ViewReports = 1;
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
                else
                {
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
                List<Right> permissionString = access.getRightsString(userId);
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

            LoanSelection detail = (new UnitAccess()).GetPermisssionGivenLoanwithBranchDeatils(userData.UserId,userData.Company_Id,userData.BranchId,userData.RoleId);

            if (detail == null)
            {
                ViewBag.type = "return";
                return PartialView();
            }

            Session["detail"] = detail;

            int userId = userData.UserId; ;
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

        public ActionResult setLoanCode(string loanCode)
        {
            //Session["loanCode"] = loanCode;
            if (loanCode == null) {
                return RedirectToAction("UserDetails");
            }
            LoanSelection list3 = (LoanSelection)Session["detail"];
            Loan finalSelectedLoan = new Loan();
            foreach (var l in list3.LoanList) {
                if (l.loanCode == loanCode) {

                    finalSelectedLoan.NonRegBranchId = l.nonRegisteredBranchId;
                    finalSelectedLoan.LoanId = l.loanId;
                    finalSelectedLoan.LoanNumber = l.loanNumber;
                    finalSelectedLoan.Rights = l.rightId.Split(',');
                    if (l.titleTracked == true)
                    {
                        finalSelectedLoan.IsTitleTrack = 1;
                    }
                    else {
                        finalSelectedLoan.IsTitleTrack = 0;
                    }
                    //finalSelectedLoan.IsTitleTrack =

                    foreach (var nrbr in list3.NonRegBranchList) {
                        if (nrbr.NonRegBranchId == l.nonRegisteredBranchId) {
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
                    Session["loanDashboard"] = finalSelectedLoan;
                }
            }
            Session["detail"]="";
            return RedirectToAction("UserDetails");
        }

        public List<Right> PermissionList(int userId)
        {
            var access = new UserRightsAccess();

            //retrive all rights
            List<Right> rights = access.getRights();

            int userRole = (new UserManageAccess()).getUserRole(userId);

            if (userRole == 3)
            {
                //get permission string for the relevent user
                List<Right> permissionString = access.getRightsString(userId);
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

            // check he is a super admin or admin

            int roleId = userData.RoleId;

            if (roleId > 2)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            // check if   step is 3...

            if (lbls != null && lbls.Equals("User Successfully Created"))
            {
                ViewBag.SuccessMsg = "User Successfully Created";
                //sa.updateStepNumberByUserId(userId, 4);
                //int rol = int.Parse(Session["abcRol"].ToString());
                //int br = int.Parse(Session["abcBrnc"].ToString());
                //if ((rol == 1) && (br == 0))
                //{
                //    sa.UpdateCompanySetupStep(userData.Company_Id, userData.BranchId, 4);
                //}
                //else if ((rol == 2) && (br != 0))
                //{
                //    sa.UpdateCompanySetupStep(userData.Company_Id, br, 4);
                //}
                //Session["abcRol"] = "";
                //Session["abcBrnc"] = "";
                //Session["companyStep"] = 4;

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

            ViewBag.CurrUserRoleType = roleId;

            RoleAccess ra = new RoleAccess();
            List<UserRole> roleList = ra.GetAllUserRoles();
            List<UserRole> tempRoleList = new List<UserRole>();

            for (int i = roleId - 1; i < roleList.Count && ViewBag.CurrUserRoleType != 3; i++)
            {
                if (roleList[i].RoleId == 4)
                {
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


            ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");

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

    }
}