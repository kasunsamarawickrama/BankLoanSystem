using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                return new HttpStatusCodeResult(404);
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
                return new HttpStatusCodeResult(404);
            }



        }


        /// <summary>
        /// CreatedBy : Irfan
        /// CreatedDate: 2016/01/13
        /// 
        /// Showing details of selected user
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult UserDetails()
        {
            Session["rowId"] = userData.UserId;


            return View();
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
                return new HttpStatusCodeResult(404);
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
                return new HttpStatusCodeResult(404);
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
                return new HttpStatusCodeResult(404);
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
                return new HttpStatusCodeResult(404);
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
            //getting user role
            UserAccess ua = new UserAccess();
            


            // curUser.Company_Id
            loanSelection.NonRegCompanies = (new CompanyAccess()).GetNonRegCompanyDetailsByRegCompanyId(userData.Company_Id);

            if (loanSelection.NonRegCompanies.Count() == 1)
            {
                loanSelection.NonRegBranchList = (new BranchAccess()).getNonRegBranchesNonCompId((loanSelection.NonRegCompanies[0].CompanyId));

                if (loanSelection.NonRegBranchList.Count() == 1)
                {
                    loanSelection.LoanList = new LoanSetupAccess().GetLoanDetailsByNonRegBranchId(loanSelection.NonRegBranchList[0].NonRegBranchId);
                    //if loans count is one redirect to add unit page
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


            return PartialView(loanSelection);
        }



        public ActionResult GetLoansByNonRegBranchId(int NonRegBranchId, string type)
        {

            if (type == "AddUnit")
            {
                ViewBag.type = "AddUnit";
            }
            else if (type == "Advance")
            {
                ViewBag.type = "Advance";
            }

            return PartialView(new LoanSetupAccess().GetLoanDetailsByNonRegBranchId(NonRegBranchId));
        }


        public ActionResult getNonRegBranchesByNonRegComId(int NonRegCompId, string type)
        {

            if (type == "AddUnit")
            {
                ViewBag.type = "AddUnit";
            }
            else if (type == "Advance")
            {
                ViewBag.type = "Advance";
            }
            return PartialView((new BranchAccess()).getNonRegBranchesNonCompId(NonRegCompId));
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