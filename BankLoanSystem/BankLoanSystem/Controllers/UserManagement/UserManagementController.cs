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
                    if (loan != null)
                    {
                       
                        ViewBag.PartnerName = loan.PartnerName;
                        ViewBag.PartnerType = loan.PartnerType;
                        ViewBag.Branch = loan.BranchName;
                        ViewBag.LoanNum = loan.LoanNumber;
                        ViewBag.IsTitleTrack = loan.IsTitleTrack;
                        ViewBag.AddUnits = 1;
                        ViewBag.ViewReports = 1;
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

        public ActionResult Selectloan(string type)
        {
            int userId = userData.UserId; ;
            // if Session is expired throw an error
            
            LoanSelection loanSelection = new LoanSelection();


            loanSelection.RegBranches = new List<Branch>();
            loanSelection.NonRegBranchList = new List<NonRegBranch>();
            loanSelection.LoanList = new List<LoanSetupStep1>();
            //getting user role
            UserAccess ua = new UserAccess();



            // curUser.Company_Id   asanka 8/3/2016
            //create list for nonRegisterCompaniers

            List<NonRegBranch> NonRegisteredBranchLists = (new BranchAccess()).getNonRegBranches(userData.Company_Id);

            if (userData.RoleId == 1)
            {

                loanSelection.RegBranches = (new BranchAccess()).getBranches(userData.Company_Id);

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

                        List<LoanSetupStep1> loanLists = new LoanSetupAccess().GetLoanDetailsByNonRegBranchId(loanSelection.NonRegBranchList[0].NonRegBranchId);
                        loanSelection.LoanList = new List<LoanSetupStep1>();
                        foreach (LoanSetupStep1 loan in loanLists) {
                            if(loan.LoanStatus == true)
                            {
                                loanSelection.LoanList.Add(loan);
                            }
                        }
                         
                        //if loans count is one redirect to add unit page
                    }
                }

            }else if (userData.RoleId == 2)
            {

                loanSelection.RegBranches.Add((new BranchAccess()).getBranchByBranchId(userData.BranchId));

                


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
                        loanSelection.LoanList = new LoanSetupAccess().GetLoanDetailsByNonRegBranchId(loanSelection.NonRegBranchList[0].NonRegBranchId);
                   
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


            return PartialView(loanSelection);
        }



        public ActionResult GetLoansByNonRegBranchId(int NonRegBranchId, string type)
        {
            ViewBag.type = type;

            return PartialView(new LoanSetupAccess().GetLoanDetailsByNonRegBranchId(NonRegBranchId));
        }


        public ActionResult getNonRegBranchesByRegBranchId(int RegBranchId, string type)
        {
            List<NonRegBranch> NonRegisteredBranchLists = (new BranchAccess()).getNonRegBranches(userData.Company_Id);
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
   
                    loanSelection.LoanList = new LoanSetupAccess().GetLoanDetailsByNonRegBranchId(loanSelection.NonRegBranchList[0].NonRegBranchId);
                    //if loans count is one redirect to add unit page
                
            }


            return PartialView(loanSelection);
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

    }
}