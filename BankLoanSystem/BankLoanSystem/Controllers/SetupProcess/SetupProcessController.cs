using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.SetupProcess
{
    public class SetupProcessController : Controller
    {
        private static CompanyBranchModel userCompany = null;
        private static CompanyBranchModel userNonRegCompany = null;
        public static string CompanyType = "Lender";
        private static string _comCode;
        private static string _branchCode;
        private static int _isEdit;

        private static int _curBranchId;
        private static int _curUserRoleId;

        /// <summary>
        /// CreatedBy : Irfan MAM
        /// CreatedDate: 2016/01/27
        /// Calling the default view for all step number pages
        /// Redirect to Appropriate controller using step number
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns>Return the view</returns>
        public ActionResult Index()

        {

            int stepNo;
            int userId;
            try
            {
                stepNo = int.Parse(Session["stepNo"].ToString());
                userId = int.Parse(Session["userId"].ToString());
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404, "Your Session Expired");
            }

            ViewBag.Step = stepNo;
            //Get company details if branch same as company
            CompanyAccess ca = new CompanyAccess();
            Company company = ca.GetCompanyDetailsByFirstSpUserId(userId);
            if (stepNo == 2)
            {
                string[] zipWithExtention = company.Zip.Split('-');

                if (zipWithExtention[0] != null) company.ZipPre = zipWithExtention[0];
                if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) company.Extension = zipWithExtention[1];

                CompanyBranchModel comBranch = new CompanyBranchModel();
                comBranch.Company = company;
                TempData["Company"] = comBranch;
                return View();
            }

            else if (stepNo == 5)
            {
                Company nonRegCompany = ca.GetNonRegCompanyDetailsByRegCompanyId(company.CompanyId);

                if (string.IsNullOrEmpty(nonRegCompany.CompanyName)) return RedirectToAction("Step4", "SetupProcess");

                string[] zipWithExtention = nonRegCompany.Zip.Split('-');

                if (zipWithExtention[0] != null) nonRegCompany.ZipPre = zipWithExtention[0];
                if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) nonRegCompany.Extension = zipWithExtention[1];

                CompanyBranchModel comBranch = new CompanyBranchModel();
                comBranch.Company = nonRegCompany;
                TempData["NonRegCompany"] = comBranch;

                return View();
            }

            else if (stepNo == 0)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Company Setup is on going Please Contact Admin" });
            }
            else
            {
                return View();
            }
        }


        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/26
        /// As the initial super admin I should able to create company
        /// in the setup proccess
        /// </summary>
        /// <returns></returns>
        public ActionResult Step1(int? edit)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");

            int userId = Convert.ToInt32(Session["userId"]);

            CompanyAccess ca = new CompanyAccess();

            // check he is a super admin or admin
            UserManageAccess uma = new UserManageAccess();
            int roleId = uma.getUserRole(userId);

            if (roleId != 1)
            {
                return new HttpStatusCodeResult(404);
            }

            // Get company types to list
            List<CompanyType> ctList = ca.GetAllCompanyType();
            ViewBag.TypeId = new SelectList(ctList, "TypeId", "TypeName");

            //Get states to list
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            StepAccess sa = new StepAccess();
            if (sa.getStepNumberByUserId(userId) >= 1 && edit != 1)
            {
                return PartialView();
            }

            if (edit == 1)
            {
                if (!string.IsNullOrEmpty(Session["userId"].ToString()))
                {
                    userId = Convert.ToInt32(Session["userId"]);
                    Company preCompany = ca.GetCompanyDetailsByFirstSpUserId(userId);

                    string[] zipWithExtention = preCompany.Zip.Split('-');

                    if (zipWithExtention[0] != null) preCompany.ZipPre = zipWithExtention[0];
                    if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) preCompany.Extension = zipWithExtention[1];

                    _comCode = preCompany.CompanyCode;
                    ViewBag.Edit = "Yes";
                    _isEdit = 1;

                    return PartialView(preCompany);
                }
            }

            return RedirectToAction("UserLogin", "Login");
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/26
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step1(Company company)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return new HttpStatusCodeResult(404, "Your Session Expired");

            string type;
            if (_isEdit != 1)
            {
                GeneratesCode gc = new GeneratesCode();
                _comCode = company.CompanyCode = gc.GenerateCompanyCode(company.CompanyName);
                type = "INSERT";
            }
            else
            {
                company.CompanyCode = _comCode;
                type = "UPDATE";
                _isEdit = 0;
            }

            company.Zip = company.ZipPre;
            if (company.Extension != null)
                company.Zip += "-" + company.Extension;

            company.CreatedBy = company.FirstSuperAdminId = Convert.ToInt32(Session["userId"]);
            company.CompanyStatus = true;
            CompanyAccess ca = new CompanyAccess();

            if (ca.InsertCompany(company, type))
            {
                ViewBag.SuccessMsg = "Company Successfully setup.";

                CompanyType = (company.TypeId == 1) ? "Lender" : "Dealer";

                //If succeed update step table to step2 
                StepAccess sa = new StepAccess();
                sa.updateStepNumberByUserId(company.FirstSuperAdminId, 2);

                //Send company detail to step 2
                CompanyBranchModel comBranch = new CompanyBranchModel();
                comBranch.Company = company;

                TempData["Company"] = comBranch;
                return RedirectToAction("Step2");
            }


            return new HttpStatusCodeResult(404, "Failed to Setup company.");
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/26
        /// Get first branch details
        /// </summary>
        /// <returns></returns>
        public ActionResult Step2(int? edit)
        {
            if ((Session["userId"] != null) && (Session["userId"].ToString() != ""))
            //if(userId>0)
            {
                int userId = (int)Session["userId"];

                // check he is a super admin or admin
                UserManageAccess uma = new UserManageAccess();
                int roleId = uma.getUserRole(userId);

                if (roleId != 1)
                {
                    return new HttpStatusCodeResult(404);
                }

                StepAccess cs = new StepAccess();

                int reslt = cs.getStepNumberByUserId(userId);
                if (reslt >= 2)
                {
                    userCompany = new CompanyBranchModel();
                    if ((TempData["Company"] != null) && (TempData["Company"].ToString() != ""))
                    {
                        userCompany = (CompanyBranchModel)TempData["Company"];

                        CompanyType = (userCompany.Company.TypeId == 1) ? "Lender" : "Dealer";

                        if (userCompany.Company.Extension == null)
                            userCompany.Company.Extension = "";
                    }

                    if (edit == 1)
                    {
                        _isEdit = 1;
                    }

                    userCompany.MainBranch = new Branch();
                    ViewBag.BranchIndex = 0;

                    //Get company details by user id
                    userId = Convert.ToInt32(Session["userId"]);
                    CompanyAccess ca = new CompanyAccess();
                    Company preCompany = ca.GetCompanyDetailsByFirstSpUserId(userId);
                    userCompany.Company = preCompany;

                    BranchAccess ba = new BranchAccess();
                    IList<Branch> branches = ba.getBranchesByCompanyCode(preCompany.CompanyCode);
                    userCompany.SubBranches = branches;

                    //Get states to list
                    List<State> stateList = ca.GetAllStates();
                    ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

                    return PartialView(userCompany);

                }
                else
                {
                    return new HttpStatusCodeResult(404, "Your Session is Expired");
                }
            }
            else
            {
                return new HttpStatusCodeResult(404, "Your Session is Expired");
            }
        }

        //Post Branch
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/26
        /// insert branch details
        /// </summary>
        /// <param name="userCompany2"></param>
        /// <param name="branchCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step2(CompanyBranchModel userCompany2, string branchCode)
        {
            int userId = (int)Session["userId"];

            userCompany2.Company = userCompany.Company;
            userCompany2.MainBranch.StateId = userCompany2.StateId;
            userCompany2.MainBranch.BranchCode = branchCode;

            BranchAccess ba = new BranchAccess();
            if (string.IsNullOrEmpty(branchCode))
            {
                _branchCode = userCompany2.MainBranch.BranchCode = ba.createBranchCode(userCompany.Company.CompanyCode);
                userCompany.MainBranch = userCompany2.MainBranch;
            }

            bool reslt = ba.insertFirstBranchDetails(userCompany2, userId);
            if (reslt)
            {
                StepAccess sa = new StepAccess();
                if (sa.updateStepNumberByUserId(userId, 3))
                {
                    bool reslt2 = ba.updateUserBranchId(userCompany2, userId);
                    if (reslt2)
                    {
                        return RedirectToAction("Step3");
                    }
                }
            }
            else
            {

                return new HttpStatusCodeResult(404, "Your Session is Expired");
            }

            userCompany.MainBranch = new Branch();
            ViewBag.BranchIndex = 0;

            //Get company details by user id
            userId = Convert.ToInt32(Session["userId"]);
            CompanyAccess ca = new CompanyAccess();
            Company preCompany = ca.GetCompanyDetailsByFirstSpUserId(userId);
            userCompany.Company = preCompany;

            IList<Branch> branches = ba.getBranchesByCompanyCode(preCompany.CompanyCode);
            userCompany.SubBranches = branches;

            //Get states to list
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            return PartialView();
        }

        // GET: SetupProcess : As the initial Super Admin I should be able to create Super Admins, Admins, Users in the set up process.
        /// <summary>
        /// CreatedBy : Irfan MAM
        /// CreatedDate: 2016/01/26
        /// 
        /// to create Users
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Step3(string lbls)
        {

            // take firstsuperadmin userid....
            int userId;
            StepAccess sa = new StepAccess();
            try
            {
                userId = int.Parse(Session["userId"].ToString());

            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404);
            }

            // check he is a super admin or admin
            UserManageAccess uma = new UserManageAccess();
            int roleId = uma.getUserRole(userId);

            if (roleId > 2)
            {
                return new HttpStatusCodeResult(404);
            }

            // check if   step is 3...

            if (sa.getStepNumberByUserId(userId) < 3)
            {
                return new HttpStatusCodeResult(404);
            }

            if (lbls != null && lbls.Equals("User Successfully Created"))
            {
                ViewBag.SuccessMsg = "User Successfully Created";
                sa.updateStepNumberByUserId(userId, 4);
                return PartialView();
            }

            UserAccess ua = new UserAccess();
            User curUser = ua.retreiveUserByUserId(userId);

            ViewBag.CurrUserRoleType = curUser.RoleId;
            _curUserRoleId = curUser.RoleId;
            _curBranchId = curUser.BranchId;

            RoleAccess ra = new RoleAccess();
            List<UserRole> roleList = ra.GetAllUserRoles();
            List<UserRole> tempRoleList = new List<UserRole>();

            for (int i = curUser.RoleId - 1; i < roleList.Count && ViewBag.CurrUserRoleType != 3; i++)
            {
                UserRole tempRole = new UserRole()
                {
                    RoleId = roleList[i].RoleId,
                    RoleName = roleList[i].RoleName
                };
                tempRoleList.Add(tempRole);
            }

            ViewBag.RoleId = new SelectList(tempRoleList, "RoleId", "RoleName");

            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(curUser.Company_Id);


            ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");

            //return PartialView(userViewModel);
            return PartialView();
        }


        //public ActionResult Step3(string lbls, int? edit)
        //{

        //    // take firstsuperadmin userid....
        //    int userId;
        //    StepAccess sa = new StepAccess();
        //    try
        //    {
        //        userId = int.Parse(Session["userId"].ToString());

        //    }
        //    catch (Exception)
        //    {
        //        return new HttpStatusCodeResult(404);
        //    }

        //    // check he is a super admin or not
        //    if ((new UserManageAccess()).getUserRole(userId) != 1)
        //    {
        //        return new HttpStatusCodeResult(404);
        //    }

        //    // check if   step is 3...

        //    if (sa.getStepNumberByUserId(userId) < 3)
        //    {
        //        return new HttpStatusCodeResult(404);
        //    }

        //    if (lbls != null && lbls.Equals("User Successfully Created"))
        //    {
        //        ViewBag.SuccessMsg = "User Successfully Created";
        //        sa.updateStepNumberByUserId(userId, 4);
        //        return PartialView();
        //    }

        //    UserAccess ua = new UserAccess();
        //    User curUser = ua.retreiveUserByUserId(userId);

        //    CompanyAccess ca = new CompanyAccess();
        //    Company company = ca.GetCompanyDetailsByFirstSpUserId(userId);
        //    //get users by company
        //    List<User> userList = ua.GetUserList(company.CompanyId, curUser.RoleId);


        //    UserListViewModel userViewModel = new UserListViewModel();
        //    userViewModel.Users = userList;
        //    ViewBag.UserIndex = 0;
        //    //ViewBag.BranchIndex = 0;

        //    RoleAccess ra = new RoleAccess();
        //    List<UserRole> roleList = ra.GetAllUserRoles();

        //    // get all branches
        //    List<Branch> branchesLists = (new BranchAccess()).getBranches(curUser.Company_Id);

        //    ViewBag.RoleId = new SelectList(roleList, "RoleId", "RoleName");
        //    ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");

        //    if (edit == 1)
        //    {
        //        _isEdit = 1;
        //        ViewBag.IsEdit = 1;
        //    }

        //    //return PartialView(userViewModel);
        //    return PartialView();
        //}


        // GET: SetupProcess : As the initial Super Admin I should be able to create Super Admins, Admins, Users in the set up process.
        /// <summary>
        /// CreatedBy : Irfan MAM
        /// CreatedDate: 2016/01/26
        /// 
        /// to show the view
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step3(User user)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");

            int currentUser = int.Parse(Session["userId"].ToString());

            // check he is a super admin or admin
            UserManageAccess uma = new UserManageAccess();
            int roleId = uma.getUserRole(currentUser);

            if (roleId > 2)
            {
                return new HttpStatusCodeResult(404);
            }

            // check if   step is 3...
            StepAccess sa = new StepAccess();
            if (sa.getStepNumberByUserId(currentUser) < 3)
            {
                return new HttpStatusCodeResult(404);
            }

            user.CreatedBy = currentUser;
            user.IsDelete = false;
            user.Status = false;

            string passwordTemp = user.Password;

            UserAccess ua = new UserAccess();

            string newSalt = PasswordEncryption.RandomString();
            user.Password = PasswordEncryption.encryptPassword(user.Password, newSalt);

            user.Email = user.NewEmail;

            CompanyAccess ca = new CompanyAccess();
            Company company = ca.GetCompanyDetailsByFirstSpUserId(currentUser);
            user.Company_Id = company.CompanyId;

            //Set admin branch to new user 
            if (_curUserRoleId == 2)
            {
                user.BranchId = _curBranchId;
            }

            //Insert user
            int res = ua.InsertUser(user);

            //Insert new user to user activation table
            string activationCode = Guid.NewGuid().ToString();
            int userId = (new UserAccess()).getUserId(user.Email);
            res = ua.InsertUserActivation(userId, activationCode);
            if (res == 1)
            {


                string body = "Hi " + user.FirstName + "! <br /><br /> Your account has been successfully created. Below in your account detail." +
                              "<br /><br /> User name: " + user.UserName +
                                    "<br /> Password : <b>" + passwordTemp +
                              "<br />Click <a href='http://localhost:57318/CreateUser/ConfirmAccount?userId=" + userId + "&activationCode=" + activationCode + "'>here</a> to activate your account." +
                              "<br /><br/> Thanks,<br /> Admin.";

                Email email = new Email(user.Email);
                email.SendMail(body, "Account details");



                ViewBag.SuccessMsg = "User Successfully Created";



                return RedirectToAction("Step3", new { lbls = ViewBag.SuccessMsg });

            }
            else
            {
                ViewBag.ErrorMsg = "Failed to create user!";

                //Restrict to create above user role 
                RoleAccess ra = new RoleAccess();
                List<UserRole> roleList = ra.GetAllUserRoles();



                ViewBag.RoleId = new SelectList(roleList, "RoleId", "RoleName");



                User curUser = ua.retreiveUserByUserId(userId);
                // get all branches
                List<Branch> branchesLists = (new BranchAccess()).getBranches(curUser.Company_Id);
                ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");


                return PartialView();
            }
        }


        //public ActionResult Step3(UserListViewModel modelUser, int? editUserId)
        //{
        //    User user = modelUser.User;

        //    if (Session["userId"] == null || Session["userId"].ToString() == "")
        //        return RedirectToAction("UserLogin", "Login");

        //    int currentUser = int.Parse(Session["userId"].ToString());
        //    // check he is a super admin or not
        //    if ((new UserManageAccess()).getUserRole(currentUser) != 1)
        //    {
        //        return new HttpStatusCodeResult(404);
        //    }

        //    // check if   step is 3...
        //    StepAccess sa = new StepAccess();
        //    if (sa.getStepNumberByUserId(currentUser) < 3)
        //    {
        //        return new HttpStatusCodeResult(404);
        //    }

        //    user.CreatedBy = currentUser;
        //    user.IsDelete = false;
        //    user.Status = false;

        //    string passwordTemp = user.Password;

        //    UserAccess ua = new UserAccess();

        //    string newSalt = PasswordEncryption.RandomString();
        //    user.Password = PasswordEncryption.encryptPassword(user.Password, newSalt);

        //    user.RoleId = modelUser.RoleId;
        //    user.BranchId = modelUser.BranchId;
        //    user.Email = modelUser.EditableEmail;

        //    if (editUserId != null)
        //    {
        //        user.UserId = (int) editUserId;
        //        user.UserName = modelUser.EditableUserName;
        //    }

        //    //Insert user
        //    int res = ua.InsertUser(user);

        //    //Insert new user to user activation table
        //    string activationCode = Guid.NewGuid().ToString();
        //    int userId = (new UserAccess()).getUserId(user.Email);
        //    res = ua.InsertUserActivation(userId, activationCode);
        //    if (res == 1)
        //    {


        //        string body = "Hi " + user.FirstName + "! <br /><br /> Your account has been successfully created. Below in your account detail." +
        //                      "<br /><br /> User name: " + user.UserName +
        //                            "<br /> Password : <b>" + passwordTemp +
        //                      "<br />Click <a href='http://localhost:57318/CreateUser/ConfirmAccount?userId=" + userId + "&activationCode=" + activationCode + "'>here</a> to activate your account." +
        //                      "<br /><br/> Thanks,<br /> Admin.";

        //        Email email = new Email(user.Email);
        //        email.SendMail(body, "Account details");



        //        ViewBag.SuccessMsg = "User Successfully Created";



        //        return RedirectToAction("Step3", new { lbls = ViewBag.SuccessMsg });

        //    }
        //    else
        //    {
        //        ViewBag.ErrorMsg = "Failed to create user!";

        //        //Restrict to create above user role 
        //        RoleAccess ra = new RoleAccess();
        //        List<UserRole> roleList = ra.GetAllUserRoles();



        //        ViewBag.RoleId = new SelectList(roleList, "RoleId", "RoleName");



        //        User curUser = ua.retreiveUserByUserId(userId);
        //        // get all branches
        //        List<Branch> branchesLists = (new BranchAccess()).getBranches(curUser.Company_Id);
        //        ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");


        //        return PartialView();
        //    }
        //}


        // GET: SetupProcess : As the initial Super Admin I should be able to create Super Admins, Admins, Users in the set up process.
        /// <summary>
        /// CreatedBy : Irfan MAM
        /// CreatedDate: 2016/01/26
        /// 
        /// to show the loan view
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Step6()
        {
            int userId;
            try
            {
                userId = int.Parse(Session["userId"].ToString());
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404, "Your Session is Expired");
            }
            UserAccess ua = new UserAccess();
            User curUser = ua.retreiveUserByUserId(userId);

            UserManageAccess uma = new UserManageAccess();
            int userrole = curUser.RoleId;

            // check he is super admin or admin
            if (userrole == 3)
            {
                return new HttpStatusCodeResult(404);
            }

            // check if   step is 6...
            StepAccess sa = new StepAccess();
            int stepNo = sa.getStepNumberByUserId(userId);
            if (stepNo < 0)
            {
                stepNo = sa.checkUserLoginWhileCompanySetup(userId);
            }

            if (stepNo < 6) 
            {
                return new HttpStatusCodeResult(404, "You are not allowed");
            }



            ViewBag.userroleName = uma.getUserRoleName(userId);

            BranchAccess ba = new BranchAccess();


            int comType = ba.getCompanyTypeByUserId(userId);
            ViewBag.ThisCompanyType = (comType == 1) ? "Lender" : "Dealer";//

            //

            LoanSetupAccess la = new LoanSetupAccess();
            int loanId = la.getLoanIdByUserId(userId);
            




            // retrive registered branches

            List<Branch> RegisteredBranchLists = (new BranchAccess()).getBranches(curUser.Company_Id);
            List<NonRegBranch> NonRegisteredBranchLists = (new BranchAccess()).getNonRegBranches(curUser.Company_Id);

            List<string> paymentMethods = new List<string>();
            paymentMethods.Add("Auto Deduct/Deposit");
            paymentMethods.Add("Invoice/Check");
            ViewBag.paymentMethods = paymentMethods;

            

            LoanSetupStep1 loanSetupStep1 = new LoanSetupStep1();
            loanSetupStep1.startDate = DateTime.Today;
            loanSetupStep1.maturityDate = DateTime.Today;

            if (loanId > 0)
            {
                loanSetupStep1 = la.GetLoanStepOne(loanId);
            }




            if (userrole == 2)
            {


                foreach (Branch branch in RegisteredBranchLists)
                {
                    if (branch.BranchId == curUser.BranchId)
                    {
                        var newList = new List<Branch>();
                        newList.Add(branch);
                        ViewBag.RegisteredBranchId = new SelectList(newList, "BranchId", "BranchName", curUser.BranchId);
                    }
                }
                var newNonRegList = new List<Branch>();
               
                foreach (NonRegBranch branch in NonRegisteredBranchLists)
                {
                    if (branch.BranchId == curUser.BranchId)
                    {

                        newNonRegList.Add(branch);


                    }
                }


                ViewBag.NonRegisteredBranchId = new SelectList(newNonRegList, "NonRegBranchId", "BranchName");
                
                
            }
            else
            {
                if (loanId > 0)
                {
                    NonRegBranch nonRegBranch = (new BranchAccess()).getNonRegBranchByNonRegBranchId(loanSetupStep1.nonRegisteredBranchId);
                    loanSetupStep1.RegisteredBranchId = nonRegBranch.BranchId;
                    
                }
                
                ViewBag.RegisteredBranchId = new SelectList(RegisteredBranchLists, "BranchId", "BranchName");
                    ViewBag.NonRegisteredBranchId = new SelectList(NonRegisteredBranchLists, "NonRegBranchId", "BranchName");
                
            }







            loanSetupStep1.allUnitTypes = (new LoanSetupAccess()).getAllUnitTypes();

            if (loanId > 0)
            {
                
                loanSetupStep1.allUnitTypes = (new LoanSetupAccess()).getAllUnitTypes();
                //(new LoanSetupAccess()).getSelectedUnitTypes(loanId, loanSetupStep1);
                foreach (UnitType unitType in (List<UnitType>)loanSetupStep1.selectedUnitTypes)
                {
                    foreach (UnitType allUnitType in (List<UnitType>)loanSetupStep1.allUnitTypes)
                    {
                        if(allUnitType.unitTypeId == unitType.unitTypeId)
                        {
                            allUnitType.isSelected = true;
                            continue;
                        }
                    }

                }


            }



            return PartialView(loanSetupStep1);

        }


        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/27
        /// </summary>
        /// <returns></returns>
        public ActionResult Step4(int? edit)
        {

            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");

            int userId = Convert.ToInt32(Session["userId"]);

            StepAccess sa = new StepAccess();
            int stepNo = sa.getStepNumberByUserId(userId);
            if (stepNo < 0)
            {
                stepNo = sa.checkUserLoginWhileCompanySetup(userId);
            }

            if (edit == 1)
            {
                _isEdit = 1;
            }

            if (stepNo >= 3)
            {
                BranchAccess ba = new BranchAccess();
                int comType = ba.getCompanyTypeByUserId(userId);
                ViewBag.ThisCompanyType = (comType == 1) ? "Dealer" : "Lender";

                //Get states to list
                CompanyAccess ca = new CompanyAccess();
                List<State> stateList = ca.GetAllStates();
                ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

                Company regCompany = ca.GetCompanyDetailsByFirstSpUserId(userId);

                List<Company> nonRegCompanies = ca.GetCompanyByCreayedCompany(regCompany.CompanyId);

                CompanyViewModel companyViewModel = new CompanyViewModel();
                companyViewModel.Companies = nonRegCompanies;
                return PartialView(companyViewModel);
            }

            return new HttpStatusCodeResult(404, "You are not allowed");
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/27
        /// </summary>
        /// <param name="nonRegComModel"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step4(CompanyViewModel nonRegComModel, string companyCode)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");

            nonRegComModel.Company.CompanyCode = companyCode;

            if (string.IsNullOrEmpty(companyCode))
            {
                GeneratesCode gc = new GeneratesCode();
                nonRegComModel.Company.CompanyCode = gc.GenerateNonRegCompanyCode(nonRegComModel.Company.CompanyName);
            }

            nonRegComModel.Company.Zip = nonRegComModel.Company.ZipPre;
            if (nonRegComModel.Company.Extension != null)
                nonRegComModel.Company.Zip += "-" + nonRegComModel.Company.Extension;

            int userId = Convert.ToInt32(Session["userId"]);
            nonRegComModel.Company.CreatedBy = Convert.ToInt32(Session["userId"]);
            nonRegComModel.Company.TypeId = (CompanyType == "Lender") ? 2 : 1;
            nonRegComModel.Company.StateId = nonRegComModel.StateId;

            CompanyAccess ca = new CompanyAccess();
            Company regCompany = ca.GetCompanyDetailsByFirstSpUserId(userId);

            nonRegComModel.Company.CreatedByCompany = regCompany.CompanyId;

            Company nonRegCom = nonRegComModel.Company;

            if (ca.InsertNonRegisteredCompany(nonRegCom))
            {
                ViewBag.SuccessMsg = ((CompanyType == "Lender") ? "Dealer" : "Lender") + " Successfully created.";

                //If succeed update step table to step2 
                StepAccess sa = new StepAccess();
                sa.updateStepNumberByUserId(userId, 5);

                //Send company detail to step 2
                CompanyBranchModel comBranch = new CompanyBranchModel();
                comBranch.Company = nonRegCom;

                TempData["NonRegCompany"] = comBranch;
                return RedirectToAction("Step5");
            }
            ViewBag.ErrorMsg = "Failed to create " + ((CompanyType == "Lender") ? "Dealer" : "Lender") + " company.";

            return new HttpStatusCodeResult(404, ViewBag.ErrorMsg);
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/27
        /// Get Lender/Dealer branch details
        /// </summary>
        /// <returns></returns>
        public ActionResult Step5()
        {
            //int userId = 68;
            if ((Session["userId"] != null) && (Session["userId"].ToString() != ""))
            //if (userId > 0)
            {
                int userId = (int)Session["userId"];
                BranchAccess ba = new BranchAccess();
                int compType = ba.getCompanyTypeByUserId(userId);
                if (compType == 1)
                {
                    ViewBag.compType = "Create Dealer Branch";
                }
                else if (compType == 2)
                {
                    ViewBag.compType = "Create Lender Branch";
                }
                else
                {
                    ViewBag.compType = "";
                }
                StepAccess cs = new StepAccess();
                int stepNo = cs.getStepNumberByUserId(userId);
                if (stepNo < 0)
                {
                    stepNo = cs.checkUserLoginWhileCompanySetup(userId);
                }

                if (stepNo >= 5)
                {
                    if ((TempData["NonRegCompany"] != null) && (TempData["NonRegCompany"].ToString() != ""))
                    {
                        userNonRegCompany = new CompanyBranchModel();

                        userNonRegCompany = (CompanyBranchModel)TempData["NonRegCompany"];
                        userNonRegCompany.MainBranch = new Branch();
                        if (userNonRegCompany.Company.Extension == null)
                            userNonRegCompany.Company.Extension = "";
                    }

                    UserAccess ua = new UserAccess();
                    User curUser = ua.retreiveUserByUserId(userId);


                    ViewBag.CurrUserRoleType = curUser.RoleId;
                    _curUserRoleId = curUser.RoleId;
                    _curBranchId = curUser.BranchId;

                    //Get states to list
                    CompanyAccess ca = new CompanyAccess();
                    List<State> stateList = ca.GetAllStates();
                    ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

                    // get all branches
                    List<Branch> branchesLists = (new BranchAccess()).getBranches(curUser.Company_Id);
                    ViewBag.RegBranchId = new SelectList(branchesLists, "BranchId", "BranchName");

                    //Get all non reg companies
                    List<Company> nonRegCompanyList = ca.GetCompanyByCreayedCompany(curUser.Company_Id);
                    ViewBag.NonRegCompanyId = new SelectList(nonRegCompanyList, "CompanyId", "CompanyName");

                    NonRegCompanyBranchModel nonRegCompanyBranch = new NonRegCompanyBranchModel();

                    //Get all non registered branches by company id
                    List<NonRegBranch> nonRegBranches = ba.getNonRegBranches(curUser.Company_Id);
                    nonRegCompanyBranch.NonRegBranches = nonRegBranches;

                    List<NonRegBranch> adminBonRegBranches = new List<NonRegBranch>();
                    if (curUser.RoleId == 2)
                    {
                        for (int i = 0; i < nonRegBranches.Count; i++)
                        {
                            if (curUser.BranchId == nonRegBranches[i].BranchId)
                            {
                                adminBonRegBranches.Add(nonRegBranches[i]);
                            }
                        }

                        nonRegCompanyBranch.NonRegBranches = adminBonRegBranches;
                    }

                    return PartialView(nonRegCompanyBranch);

                }
                else
                {
                    return new HttpStatusCodeResult(404, "Your Session is Expired");
                }
            }
            else
            {
                return RedirectToAction("UserLogin", "Login");
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/27
        /// Insert Non registered branch details
        /// </summary>
        /// <param name="nonRegCompanyBranch"></param>
        /// <returns></returns>
        [HttpPost]
        //public ActionResult Step5(CompanyBranchModel nonRegBranch)
        public ActionResult Step5(NonRegCompanyBranchModel nonRegCompanyBranch, string branchCode)
        {
            CompanyBranchModel nonRegBranch = nonRegCompanyBranch.CompanyBranch;

            int userId = (int)Session["userId"];
            //int userId = 68;
            BranchAccess ba = new BranchAccess();
            CompanyAccess ca = new CompanyAccess();

            int compType = ba.getCompanyTypeByUserId(userId);


            nonRegBranch.MainBranch.StateId = nonRegCompanyBranch.StateId;

            nonRegBranch.MainBranch.BranchCode = branchCode;

            if (string.IsNullOrEmpty(branchCode))
            {
                Company company = ca.GetNonRegCompanyByCompanyId(nonRegCompanyBranch.NonRegCompanyId);
                nonRegBranch.MainBranch.BranchCode = ba.createNonRegBranchCode(company.CompanyCode);
            }

            nonRegBranch.MainBranch = nonRegBranch.MainBranch;

            //Get created branch id
            UserManageAccess uma = new UserManageAccess();
            //userNonRegCompany.MainBranch.BranchCreatedBy = uma.getUserById(userId).BranchId;
            nonRegBranch.MainBranch.BranchCreatedBy = nonRegCompanyBranch.RegBranchId;
            nonRegBranch.MainBranch.BranchCompany = nonRegCompanyBranch.NonRegCompanyId;

            //Set admin branch to new user 
            if (_curUserRoleId == 2)
            {
                nonRegBranch.MainBranch.BranchCreatedBy = _curBranchId;
            }

            bool reslt = ba.insertNonRegBranchDetails(nonRegBranch, userId);
            if (reslt)
            {
                StepAccess sa = new StepAccess();
                if (sa.updateStepNumberByUserId(userId, 6))
                {
                    if (compType == 1)
                    {
                        ViewBag.SuccessMsg = "Create A Dealer Branch Successfully";
                    }
                    else if (compType == 2)
                    {
                        ViewBag.SuccessMsg = "Create A Lender Branch Successfully";
                    }
                    return RedirectToAction("Step6");
                    //ViewBag.SuccessMsg = "First Branch is created successfully";
                }

            }
            else
            {
                ViewBag.ErrorMsg = "Failed to create branch";
            }

            //Get states to list
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");
            return PartialView();

        }
        // GET: SetupProcess : As the initial Super Admin I should be able to create Super Admins, Admins, Users in the set up process.
        /// <summary>
        /// CreatedBy : Irfan MAM
        /// CreatedDate: 2016/01/26
        /// 
        /// update the loan
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Step6")]
        public ActionResult Step6_Post(LoanSetupStep1 loanSetupStep1)
        {
            int userId = int.Parse(Session["userId"].ToString());
            //int branchId = int.Parse(Session["branchId"].ToString());

            if (!IsAtleastOneSelectUnitType(loanSetupStep1.allUnitTypes))
            {
                return new HttpStatusCodeResult(404, "Select Atleast One Unit Type");
            }

            // check he is super admin or admin
            if (new UserManageAccess().getUserRole(userId) > 2)
            {
                return new HttpStatusCodeResult(404);
            }

            // check if   step is 6...
            StepAccess sa = new StepAccess();
            if (sa.getStepNumberByUserId(userId) < 6)
            {
                return new HttpStatusCodeResult(404);
            }

            LoanSetupAccess loanSetupAccess = new LoanSetupAccess();

            LoanSetupAccess la = new LoanSetupAccess();
            int loanId = la.getLoanIdByBranchId(loanSetupStep1.RegisteredBranchId);

            if (loanId > 0) {
                loanId = loanSetupAccess.insertLoanStepOne(loanSetupStep1, loanId);
            }
            else
            {
                loanId = loanSetupAccess.insertLoanStepOne(loanSetupStep1, loanId);
                if(loanId > 0)
                    sa.updateStepNumberByUserId(userId, 7, loanId, loanSetupStep1.RegisteredBranchId);
            }

            Session["branchId"] = loanSetupStep1.RegisteredBranchId;
            //if (loanSetupStep1.isInterestCalculate)
            //{
              //  return RedirectToAction("step7");
            //}
            //else
            //{
                sa.updateStepNumberByUserId(userId, 8, loanId, loanSetupStep1.RegisteredBranchId);
                return RedirectToAction("step8");
            //}

            

            


        }

        /// <summary>
        /// CreatedBy : Irfan MAM
        /// CreatedDate: 2016/02/02
        /// 
        /// Setup dashboard view
        /// </summary>
        /// <param name></param>
        /// <returns></returns>
        public ActionResult SetupDashBoard()
        {
            ViewBag.login = false;
            if (Session["userId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            var id = (int)Session["userId"];

            var dashBoardModel = new Models.DashBoard();

            var newDashDAL = new DashBoardAccess();

            if (id <= 0)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            else
            {

                ///get level id by userid
                int userLevelId = newDashDAL.GetUserLevelByUserId(id);

                dashBoardModel.userId = id;
                dashBoardModel.userName = (new UserAccess()).retreiveUserByUserId(id).UserName;
                dashBoardModel.roleName = (new UserManageAccess()).getUserRoleName(id);
                dashBoardModel.levelId = userLevelId;
                return PartialView(dashBoardModel);



            }

        }

        /// <summary>
        /// CreatedBy : Irfan MAM
        /// CreatedDate: 2016/08/02
        /// /Action result for ajax call
        /// filtering all non reg branches by reg branch
        /// 
        /// argument: regBranchId(int)
        /// 
        /// </summary>
        /// <returns>Return JsonResult</returns>

        [HttpPost]
        public ActionResult getNonRegBranchesByRegBranchId(int regBranchId)
        {
            int userId;
            try
            {
                userId = int.Parse(Session["userId"].ToString());
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(404, "Your Session is Expired");
            }
            UserAccess ua = new UserAccess();
            User curUser = ua.retreiveUserByUserId(userId);
            List<NonRegBranch> NonRegisteredBranchLists = (new BranchAccess()).getNonRegBranches(curUser.Company_Id);
            List<NonRegBranch> newNonRegList = new List<NonRegBranch>();

            foreach (NonRegBranch nonRegBranch in NonRegisteredBranchLists)
            {
                if (nonRegBranch.BranchId == regBranchId)
                {
                    newNonRegList.Add(nonRegBranch);
                }
            }
            SelectList NonRegisteredBranches = new SelectList(newNonRegList, "NonRegBranchId", "BranchName");

            return Json(NonRegisteredBranches);
        }

        /// <summary>
        /// CreatedBy : Irfan MAM
        /// CreatedDate: 2016/08/02
        /// 
        /// Check whether loan number already exist
        /// 
        /// argument: loanNumber(string)
        /// 
        /// </summary>
        /// <returns>Return JsonResult</returns>
        public JsonResult IsLoanNumberExists(string loanNumber, int RegisteredBranchId)
        {
            //check user name is already exist.  
            int userId = int.Parse(Session["userId"].ToString());
            return Json((new LoanSetupAccess()).IsUniqueLoanNumberForBranch(loanNumber, RegisteredBranchId, userId), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/8
        /// Get Interest Deatils If Exists
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
        // GET: Interest
        public ActionResult Step7(int? edit)
        {
            int uId = int.Parse(Session["userId"].ToString());
            //int branchId = int.Parse(Session["branchId"].ToString());
            int branchId = 35;
            List<SelectListItem> listdates = new List<SelectListItem>();
            for (int i = 1; i <= 28; i++)
            {
                listdates.Add(new SelectListItem
                {
                    Text = i.ToString(),
                    Value = i.ToString()
                });
            }

            listdates.Add(new SelectListItem
            {
                Text = "End of the month",
                Value = "End of the month"
            });


            InterestAccess ia = new InterestAccess();
            Interest intrst = new Interest();
            //get Accrual Methods
            List<AccrualMethods> methodList = ia.GetAllAccrualMethods();

            if (uId > 0)
            {
                LoanSetupAccess la = new LoanSetupAccess();
                int loanId = la.getLoanIdByBranchId(branchId);
                
                //int loanId = 1;
                if (loanId > 0)
                {
                    //var intrst = ia.getInterestDetails(loanId);
                    if (ia.getInterestDetails(loanId) != null)
                    {

                        ViewBag.Edit = 1;
                        intrst = ia.getInterestDetails(loanId);
                        ViewBag.AccrualMethodId = new SelectList(methodList, "MethodId", "MethodName", intrst.AccrualMethodId);
                        
                        if (intrst.option != "once a month")
                        {
                            ViewBag.Option = true;
                        }
                        else
                        {
                            ViewBag.Option = false;
                        }
                        ViewBag.PaidDate = new SelectList(listdates, "Value", "Text", intrst.PaidDate);
                        ViewBag.DefaultEmail = intrst.AutoRemindEmail;
                        return PartialView(intrst);
                    }

                    else
                    {
                        ViewBag.Edit = 0;
                        ViewBag.AccrualMethodId = new SelectList(methodList, "MethodId", "MethodName");
                        ViewBag.PaidDate = new SelectList(listdates, "Value", "Text");
                        string defaultEmail = la.getAutoRemindEmailByLoanId(loanId);
                        //intrst.AutoRemindEmail = defaultEmail;
                        ViewBag.Email = defaultEmail;
                        return PartialView();
                        }
                    //return PartialView();
                }
                
                else
                {
                    return new HttpStatusCodeResult(404, "error message");
                }
            }

            else
            {
                return new HttpStatusCodeResult(404, "error message");
            }

        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/8
        /// Post Interest Deatils 
        /// </summary>
        /// <param name="interest"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step7(Interest interest)
        {

            int userId = int.Parse(Session["userId"].ToString());
            //int branchId = int.Parse(Session["branchId"].ToString());
            int  branchId = 35;
            if (interest.option == "payoff")
            {
                interest.PaidDate = interest.option;
            }

            InterestAccess ia = new InterestAccess();
            LoanSetupAccess la = new LoanSetupAccess();
            int loanId = la.getLoanIdByBranchId(branchId);
            if (!interest.NeedReminder)
            {
                interest.AutoRemindEmail = null;
            }
            interest.LoanId = loanId;
            // if()
            int reslt = ia.insertInterestDetails(interest);

            if (reslt >= 1)
            {
                StepAccess sa = new StepAccess();
                if (sa.updateStepNumberByUserId(userId, 8, interest.LoanId, branchId))
                {
                    return RedirectToAction("Step8");
                }
                else
                {
                    return new HttpStatusCodeResult(404, "error message");
                }
            }
            else if (reslt == 0)
            {
                return RedirectToAction("Step8");
            }
            else
            {
                return new HttpStatusCodeResult(404, "error message");
            }
            
        }

        /// <summary>
        /// CreatedBy :kasun samarawickrama
        /// CreatedDate: 2016/08/02
        /// 
        /// loan fees section -step 8
        /// 
        /// return: step8 view
        /// </summary>
        /// <returns></returns>
        public ActionResult Step8()
        {
            //Session["userId"] = 2;
            if (Session["userId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            var userId = (int)Session["userId"];

            BranchAccess branch = new BranchAccess();
            int companyType = branch.getCompanyTypeByUserId(userId);
 
            companyType = 1;
            if (companyType == 1)
            {
                ViewBag.isLender = true;
            }
            else {
                ViewBag.isLender = false;
            }
            Fees fee = new Fees(); 
            LoanSetupAccess loan = new LoanSetupAccess();
            fee.LoanId = loan.getLoanIdByUserId(userId);
            //check the loan is in a update

            var hasLoan = loan.checkLoanIsInFeesTables(fee.LoanId);

            if (hasLoan.AdvanceAmount > 0 || hasLoan.MonthlyLoanAmount > 0 || hasLoan.LotInspectionAmount > 0)
            {
                ViewBag.isEdit = "editable";
                Session["isEdit"] = true;
                hasLoan.LoanId = fee.LoanId;
                return PartialView(hasLoan);
            }
            else {
                ViewBag.isEdit = "notEditable";
            
                Fees feeUpdate = new Fees();
            feeUpdate.LoanId = fee.LoanId;

                if (feeUpdate.LoanId > 0)
                {
                    var email = loan.getAutoRemindEmailByLoanId(feeUpdate.LoanId);
                    feeUpdate.MonthlyLoanLenderEmail = email;
                    feeUpdate.MonthlyLoanDealerEmail = email;
                    feeUpdate.LotInspectionLenderEmail = email;
                    feeUpdate.LotInspectionDealerEmail = email;
                    feeUpdate.AdvanceLenderEmail = email;
                    feeUpdate.AdvanceDealerEmail = email;

                return PartialView(feeUpdate);
            }
            else {
                return RedirectToAction("Step7");
            }
        }
        }
        /// <summary>
        /// CreatedBy :kasun samarawickrama
        /// CreatedDate: 2016/09/02
        /// 
        /// loan fees section step post method
        /// 
        /// return: step8 view 
        /// </summary>
        /// <param name="fees"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step8(Fees fees)
        {
            StepAccess step = new StepAccess();

            if (fees.AdvanceDue == "Vehicle Payoff")
            {
                fees.AdvanceDueDate = "VP";
            }
            if (fees.MonthlyLoanDue == "Vehicle Payoff")
            {
                fees.MonthlyLoanDueDate = "VP";
            }
            if (fees.AdvanceDue == "Time of Advance")
            {
                fees.AdvanceDueDate = "TOA";
            }
            if (fees.MonthlyLoanDue == "Time of Advance")
            {
                fees.MonthlyLoanDueDate = "TOA";
            }
            if (fees.AdvanceDue == "Once a Month" && fees.AdvanceRadio =="month")
            {
                fees.AdvanceDueDate = "EOM";
            }
            if (fees.MonthlyLoanDue == "Once a Month" && fees.MonthlyLoanRadio == "month")
            {
                fees.MonthlyLoanDueDate = "EOM";
            }
            if (step.InsertFeesDetails(fees))
            {
                //Session["userId"] = 2;
                var userId = (int)Session["userId"];
                var branchId = (int)Session["branchId"];

                if ((bool)Session["isEdit"] == true) {
                    Session["isEdit"] = false;
                    return RedirectToAction("Step9");
                }
                else if(step.updateStepNumberByUserId(userId, 9, fees.LoanId, branchId))
                {
                    return RedirectToAction("Step9");
                }
                else
                {
                    return RedirectToAction("Step8");
                }
            }
            else
            {
                return RedirectToAction("Step8");
            }
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/9
        /// Get Title Deatils If Exists
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
        // GET: Interest
        public ActionResult Step9(int? edit)
        {
            int uId = int.Parse(Session["userId"].ToString());
            int branchId = int.Parse(Session["branchId"].ToString());
            //Accept Methods
            List<SelectListItem> acceptMethodsList = new List<SelectListItem>();

            acceptMethodsList.Add(new SelectListItem
            {
                Text = "title present to advance",
                Value = "title present to advance"
            });


            acceptMethodsList.Add(new SelectListItem
            {
                Text = "scanned title adequate",
                Value = "scanned title adequate"
            });

            acceptMethodsList.Add(new SelectListItem
            {
                Text = "title can arrive at any time",
                Value = "title can arrive at any time"
            });

            acceptMethodsList.Add(new SelectListItem
            {
                Text = "title can arrive within a set time",
                Value = "title can arrive within a set time"
            });


            //Time Limit Options
            List<SelectListItem> timeLimitList = new List<SelectListItem>();

            timeLimitList.Add(new SelectListItem
            {
                Text = "at advance date",
                Value = "at advance date"
            });


            timeLimitList.Add(new SelectListItem
            {
                Text = "with in 7 days",
                Value = "with in 7 days"
            });

            timeLimitList.Add(new SelectListItem
            {
                Text = "at any time",
                Value = "at any time"
            });
            if (uId > 0)
            {
                LoanSetupAccess la = new LoanSetupAccess();
                TitleAccess ta = new TitleAccess();
                Title title = new Title();
                int loanId = la.getLoanIdByBranchId(branchId);
                //int loanId = 1;
                if (loanId > 0)
                {
                    //var title = ta.getTitleDetails(loanId);
                    if (ta.getTitleDetails(loanId) != null)
                    {

                        ViewBag.Edit = 1;
                        title = ta.getTitleDetails(loanId);
                        ViewBag.TitleAcceptMethod = new SelectList(acceptMethodsList, "Value", "Text", title.TitleAcceptMethod);
                        ViewBag.ReceivedTimeLimit = new SelectList(timeLimitList, "Value", "Text", title.ReceivedTimeLimit);

                        ViewBag.DefaultEmail = title.RemindEmail;
                        //return PartialView(title);
                    }

                    else
                    {
                        ViewBag.Edit = 0;
                        ViewBag.TitleAcceptMethod = new SelectList(acceptMethodsList, "Value", "Text");
                        ViewBag.ReceivedTimeLimit = new SelectList(timeLimitList, "Value", "Text");

                        string defaultEmail = la.getAutoRemindEmailByLoanId(loanId);

                        title.RemindEmail = defaultEmail;
                        title.LoanId = loanId;
                        //return PartialView(titl);
                    }
                    return PartialView(title);
                }

                else
                {
                    return new HttpStatusCodeResult(404, "error message");
                }
            }
            else
            {
                return new HttpStatusCodeResult(404, "error message");
            }

        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/9
        /// Post Title Deatils
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step9(Title title)
        {
            int userId = int.Parse(Session["userId"].ToString());
            int branchId = int.Parse(Session["branchId"].ToString());
            //int loanId = 1;

            if (title.NeedPyhsical && title.NeedScanCopy)
            {
                title.ReceiptRequiredMethod = "physically and scan copy";
            }
            else if (title.NeedPyhsical)
            {
                title.ReceiptRequiredMethod = "physically";
            }
            else if (title.NeedScanCopy)
            {
                title.ReceiptRequiredMethod = "scan copy";
            }
            TitleAccess ta = new TitleAccess();
            LoanSetupAccess la = new LoanSetupAccess();
            int loanId = la.getLoanIdByBranchId(branchId);
            title.LoanId = loanId;

            if (title.IsReceipRequired || title.IsTitleTrack)
            {
            int reslt = ta.insertTitleDetails(title);
                if (reslt >= 1)
            {
                StepAccess sa = new StepAccess();
                if (sa.updateStepNumberByUserId(userId, 10, title.LoanId, branchId))
                {
                    return RedirectToAction("Step10");
                }
                else
                {
                    return new HttpStatusCodeResult(404, "error message");
                }
                }
                else if (reslt == 0)
                {
                    return RedirectToAction("Step10");
            }
            else
            {
                return new HttpStatusCodeResult(404, "error message");
            }
            }
            else
            {
                return RedirectToAction("Step10");
            }

           
        }
        /// <summary>
        /// CreatedBy : Irfan MAM
        /// CreatedDate: 2016/09/02
        /// 
        /// Check whether atleast one unit type selected or not
        /// 
        /// argument: allUnitTypes(IList<UnitType>)
        /// 
        /// </summary>
        /// <returns>Return JsonResult</returns>
        public bool IsAtleastOneSelectUnitType(IList<UnitType> allUnitTypes)
        {
            //check user name is already exist.  
            foreach (UnitType unitType in allUnitTypes)
            {
                if (unitType.isSelected == true)
                {
                    return true;

                }
            }
            return false;
        }



        /// <summary>
        /// CreatedBy : Irfan MAM
        /// CreatedDate: 2016/09/02
        /// 
        /// Check the payoff period which are between the start date and maturity date
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns>Return JsonResult</returns>
        public JsonResult CheckTheRangeOfPayOffPeriod(int payOffPeriod,DateTime startDate, DateTime maturityDate,int payOffPeriodType)
        {
            if (payOffPeriodType == 0) {
                int totalDays = (int)(maturityDate - startDate).TotalDays;
                if (payOffPeriod <= totalDays) {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                  }
            else {

                int diffMonths = (maturityDate.Month + maturityDate.Year * 12) - (startDate.Month + startDate.Year * 12);
                if (payOffPeriod <= diffMonths)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }

           
        }

    }
}

