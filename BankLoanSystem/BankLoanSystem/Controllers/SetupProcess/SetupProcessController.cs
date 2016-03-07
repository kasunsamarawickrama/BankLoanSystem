using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;

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

        private static string _calMode;
        User userData = new User();

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

        public ActionResult Index()
        {
            int stepNo = Convert.ToInt32(Session["companyStep"]);
            int userId = userData.UserId;

            ViewBag.Step = stepNo;
            //Get company details if branch same as company

            if (stepNo == 2)
            {
                CompanyAccess ca = new CompanyAccess();
                Company company = new Company();
                DataSet dsCompany = new DataSet();
                dsCompany = ca.GetCompanyDetailsByFirstSpUserId(userData);
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    company.CompanyId = int.Parse(dsCompany.Tables[0].Rows[0]["company_Id"].ToString());
                    company.CompanyName = dsCompany.Tables[0].Rows[0]["company_name"].ToString(); 
                    company.CompanyCode = dsCompany.Tables[0].Rows[0]["company_code"].ToString(); 
                    company.CompanyAddress1 = dsCompany.Tables[0].Rows[0]["company_address_1"].ToString();
                    company.CompanyAddress2 = dsCompany.Tables[0].Rows[0]["company_address_2"].ToString(); 
                    company.StateId = int.Parse(dsCompany.Tables[0].Rows[0]["stateId"].ToString()); 
                    company.City = dsCompany.Tables[0].Rows[0]["city"].ToString(); 
                    company.Zip = dsCompany.Tables[0].Rows[0]["zip"].ToString();

                string[] zipWithExtention = company.Zip.Split('-');

                if (zipWithExtention[0] != null) company.ZipPre = zipWithExtention[0];
                if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) company.Extension = zipWithExtention[1];

                    company.Email = dsCompany.Tables[0].Rows[0]["email"].ToString();
                    company.PhoneNum1 = dsCompany.Tables[0].Rows[0]["phone_num_1"].ToString(); 
                    company.PhoneNum2 = dsCompany.Tables[0].Rows[0]["phone_num_2"].ToString();
                    company.PhoneNum3 = dsCompany.Tables[0].Rows[0]["phone_num_3"].ToString(); 
                    company.Fax = dsCompany.Tables[0].Rows[0]["fax"].ToString(); 
                    company.WebsiteUrl = dsCompany.Tables[0].Rows[0]["website_url"].ToString();
                    company.TypeId = int.Parse(dsCompany.Tables[0].Rows[0]["company_type"].ToString()); 
                }

                //CompanyBranchModel comBranch = new CompanyBranchModel(); asanka
                //comBranch.Company = company;
                //TempData["Company"] = comBranch; asanka
                return View();
            }

            else if (stepNo == 5)
            {
                CompanyAccess ca = new CompanyAccess();
                Company company = new Company();
                Company nonRegCompany = ca.GetNonRegCompanyDetailsByRegCompanyId(company.CompanyId)[0];

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
            int userId = userData.UserId;
            int roleId = userData.RoleId;
            CompanyAccess ca = new CompanyAccess();

            // check he is a super admin or admin
            if (roleId != 1)
            {
                //disabled step 1
                return new HttpStatusCodeResult(404);
            }

            // Get company types to list
            List<CompanyType> ctList = ca.GetAllCompanyType();
            ViewBag.TypeId = new SelectList(ctList, "TypeId", "TypeName");

            //Get states to list
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            if (Convert.ToInt32(Session["companyStep"]) >= 1 && edit != 1)
            {
                return PartialView();
            }

            if (edit == 1)
            {
                Company preCompany = new Company();
                DataSet dsCompany = new DataSet();
                dsCompany = ca.GetCompanyDetailsByFirstSpUserId(userData);
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    preCompany.CompanyCode = dsCompany.Tables[0].Rows[0]["company_code"].ToString();
                    preCompany.Zip = dsCompany.Tables[0].Rows[0]["zip"].ToString();
                }

                    string[] zipWithExtention = preCompany.Zip.Split('-');

                    if (zipWithExtention[0] != null) preCompany.ZipPre = zipWithExtention[0];
                    if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) preCompany.Extension = zipWithExtention[1];

                    _comCode = preCompany.CompanyCode;
                    ViewBag.Edit = "Yes";
                    _isEdit = 1;

                    return PartialView(preCompany);
                }

            return new HttpStatusCodeResult(404, "Your Session Expired");
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

            company.CreatedBy = company.FirstSuperAdminId = userData.UserId;
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
                int userId = userData.UserId;
                int roleId = userData.RoleId;
                // check he is a super admin or admin


                if (roleId != 1)
                {
                    return new HttpStatusCodeResult(404);
                }

                //StepAccess cs = new StepAccess();

                int reslt = Convert.ToInt32(Session["companyStep"]);
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
                    userId = userData.UserId;
                    CompanyAccess ca = new CompanyAccess();
                Company preCompany = new Company();
                DataSet dsCompany = new DataSet();
                dsCompany = ca.GetCompanyDetailsByFirstSpUserId(userData);
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    preCompany.CompanyId = int.Parse(dsCompany.Tables[0].Rows[0]["company_Id"].ToString());
                    preCompany.CompanyName = dsCompany.Tables[0].Rows[0]["company_name"].ToString();
                    preCompany.CompanyCode = dsCompany.Tables[0].Rows[0]["company_code"].ToString();
                    preCompany.CompanyAddress1 = dsCompany.Tables[0].Rows[0]["company_address_1"].ToString();
                    preCompany.CompanyAddress2 = dsCompany.Tables[0].Rows[0]["company_address_2"].ToString();
                    preCompany.StateId = int.Parse(dsCompany.Tables[0].Rows[0]["stateId"].ToString());
                    preCompany.City = dsCompany.Tables[0].Rows[0]["city"].ToString();
                    preCompany.Zip = dsCompany.Tables[0].Rows[0]["zip"].ToString();

                    string[] zipWithExtention = preCompany.Zip.Split('-');

                    if (zipWithExtention[0] != null) preCompany.ZipPre = zipWithExtention[0];
                    if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) preCompany.Extension = zipWithExtention[1];

                    preCompany.Email = dsCompany.Tables[0].Rows[0]["email"].ToString();
                    preCompany.PhoneNum1 = dsCompany.Tables[0].Rows[0]["phone_num_1"].ToString();
                    preCompany.PhoneNum2 = dsCompany.Tables[0].Rows[0]["phone_num_2"].ToString();
                    preCompany.PhoneNum3 = dsCompany.Tables[0].Rows[0]["phone_num_3"].ToString();
                    preCompany.Fax = dsCompany.Tables[0].Rows[0]["fax"].ToString();
                    preCompany.WebsiteUrl = dsCompany.Tables[0].Rows[0]["website_url"].ToString();
                    preCompany.TypeId = int.Parse(dsCompany.Tables[0].Rows[0]["company_type"].ToString());
                }


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
            int userId = userData.UserId;

            userCompany2.Company = userCompany.Company;
            userCompany2.MainBranch.StateId = userCompany2.StateId;
            userCompany2.MainBranch.BranchCode = branchCode;

            BranchAccess ba = new BranchAccess();
            if (string.IsNullOrEmpty(branchCode))
            {
                _branchCode = userCompany2.MainBranch.BranchCode = ba.createBranchCode(userCompany.Company.CompanyCode);
                userCompany.MainBranch = userCompany2.MainBranch;
            }

            int reslt = ba.insertFirstBranchDetails(userCompany2, userId);
            userCompany2.MainBranch.BranchId = reslt;
            if (reslt>0)
            {
                StepAccess sa = new StepAccess();
                if (sa.UpdateCompanySetupStep(userCompany2.Company.CompanyId,userCompany2.MainBranch.BranchId, 3))
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
            userId = userData.UserId;

            // need common method for that - asanka
            CompanyAccess ca = new CompanyAccess();
            Company preCompany = new Company();
            DataSet dsCompany = new DataSet();
            dsCompany = ca.GetCompanyDetailsByFirstSpUserId(userData);
            if (dsCompany.Tables[0].Rows.Count > 0)
            {
                preCompany.CompanyId = int.Parse(dsCompany.Tables[0].Rows[0]["company_Id"].ToString());
                preCompany.CompanyName = dsCompany.Tables[0].Rows[0]["company_name"].ToString();
                preCompany.CompanyCode = dsCompany.Tables[0].Rows[0]["company_code"].ToString();
                preCompany.CompanyAddress1 = dsCompany.Tables[0].Rows[0]["company_address_1"].ToString();
                preCompany.CompanyAddress2 = dsCompany.Tables[0].Rows[0]["company_address_2"].ToString();
                preCompany.StateId = int.Parse(dsCompany.Tables[0].Rows[0]["stateId"].ToString());
                preCompany.City = dsCompany.Tables[0].Rows[0]["city"].ToString();
                preCompany.Zip = dsCompany.Tables[0].Rows[0]["zip"].ToString();

                string[] zipWithExtention = preCompany.Zip.Split('-');

                if (zipWithExtention[0] != null) preCompany.ZipPre = zipWithExtention[0];
                if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) preCompany.Extension = zipWithExtention[1];

                preCompany.Email = dsCompany.Tables[0].Rows[0]["email"].ToString();
                preCompany.PhoneNum1 = dsCompany.Tables[0].Rows[0]["phone_num_1"].ToString();
                preCompany.PhoneNum2 = dsCompany.Tables[0].Rows[0]["phone_num_2"].ToString();
                preCompany.PhoneNum3 = dsCompany.Tables[0].Rows[0]["phone_num_3"].ToString();
                preCompany.Fax = dsCompany.Tables[0].Rows[0]["fax"].ToString();
                preCompany.WebsiteUrl = dsCompany.Tables[0].Rows[0]["website_url"].ToString();
                preCompany.TypeId = int.Parse(dsCompany.Tables[0].Rows[0]["company_type"].ToString());
            }


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
            int userId= userData.UserId;
            StepAccess sa = new StepAccess();

            // check he is a super admin or admin
           
            int roleId = userData.RoleId;

            if (roleId > 2)
            {
                return new HttpStatusCodeResult(404);
            }

            // check if   step is 3...

            if (Convert.ToInt32(Session["companyStep"]) < 3)
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
            int currentUser = userData.UserId;

            // check he is a super admin or admin
            int roleId = userData.RoleId;

            if (roleId > 2)
            {
                return new HttpStatusCodeResult(404);
            }

            // check if   step is 3...
            if (Convert.ToInt32(Session["companyStep"]) < 3)
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

            //CompanyAccess ca = new CompanyAccess();
            //Company company = ca.GetCompanyDetailsByFirstSpUserId(currentUser);
            user.Company_Id = userData.Company_Id;//  company.CompanyId;  - asanka

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
            int userId= userData.UserId; 
            // if Session is expired throw an error
            

            //getting user role
            UserAccess ua = new UserAccess();
            User curUser = ua.retreiveUserByUserId(userId);

            UserManageAccess uma = new UserManageAccess();
            int userrole = curUser.RoleId;

            // if he is a user throw a error
            if (userrole == 3)
            {
                return new HttpStatusCodeResult(404, "You are not Allowed");
            }

            // check if step is less than 6, not allowed to this page...
            StepAccess sa = new StepAccess();
            int stepNo = Convert.ToInt32(Session["companyStep"]);
            if (stepNo < 0)
            {
                stepNo = Convert.ToInt32(Session["companyStep"]);
            }

            if (stepNo < 6)
            {
                return new HttpStatusCodeResult(404, "You are not allowed");
            }


            // get the Role Name for front end view
            ViewBag.userroleName = uma.getUserRoleName(userId);

            BranchAccess ba = new BranchAccess();

            // get the Company type for front end view
            int comType = ba.getCompanyTypeByUserId(userId);
            ViewBag.ThisCompanyType = (comType == 1) ? "Lender" : "Dealer";//





            // retrieve registered branches, nonregistered branches using his company Id

            List<Branch> RegisteredBranchLists = (new BranchAccess()).getBranches(curUser.Company_Id);
            List<NonRegBranch> NonRegisteredBranchLists = (new BranchAccess()).getNonRegBranches(curUser.Company_Id);

            // get the payments method for front End View
            List<string> paymentMethods = new List<string>();
            paymentMethods.Add("Auto Deduct/Deposit");
            paymentMethods.Add("Invoice/Check");
            ViewBag.paymentMethods = paymentMethods;


            // Defaul loan setup form and default dates
            LoanSetupStep1 loanSetupStep1 = new LoanSetupStep1();
            loanSetupStep1.startDate = DateTime.Today;
            loanSetupStep1.maturityDate = DateTime.Today.AddDays(1);

            // get loan Id for each user
            LoanSetupAccess la = new LoanSetupAccess();
            int loanId = 0;

            if (userrole == 2)
            {
                loanId = la.getLoanIdByBranchId(curUser.BranchId);
            }
            else if (userrole == 1)
            {
                loanId = la.getLoanIdByUserId(userId);
            }


            // if loan number exists get the loan details
            if (loanId > 0)
            {
                loanSetupStep1 = la.GetLoanStepOne(loanId);
            }

            if (userrole == 2)
            {
                // if user is a admin, his branch id is registerd branch id
                loanSetupStep1.RegisteredBranchId = curUser.BranchId;

                // the get registered branch detail from the company branches list
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

                // the get non registered branches details for perticular branch  from the non registeres branches list
                foreach (NonRegBranch branch in NonRegisteredBranchLists)
                {
                    if (branch.BranchId == curUser.BranchId)
                    {

                        newNonRegList.Add(branch);


                    }
                }



                ViewBag.NonRegisteredBranchId = new SelectList(newNonRegList, "NonRegBranchId", "CompanyNameBranchName");


            }
            // if he is a super admin, add all company branches and non registered branches in to the list
            else
            {
                // if super admin get the branch id of the loan
                if (loanId > 0)
                {
                    NonRegBranch nonRegBranch = (new BranchAccess()).getNonRegBranchByNonRegBranchId(loanSetupStep1.nonRegisteredBranchId);
                    loanSetupStep1.RegisteredBranchId = nonRegBranch.BranchId;

                }

                ViewBag.RegisteredBranchId = new SelectList(RegisteredBranchLists, "BranchId", "BranchName");
                ViewBag.NonRegisteredBranchId = new SelectList(NonRegisteredBranchLists, "NonRegBranchId", "CompanyNameBranchName");

            }

            if (NonRegisteredBranchLists.Count == 1)
            {
                loanSetupStep1.nonRegisteredBranchId = NonRegisteredBranchLists[0].NonRegBranchId;
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
                        if (allUnitType.unitTypeId == unitType.unitTypeId)
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
                return new HttpStatusCodeResult(404, "Your Session Expired");

            int userId = Convert.ToInt32(Session["userId"]);

            StepAccess sa = new StepAccess();
            int stepNo = Convert.ToInt32(Session["companyStep"]);
            if (stepNo < 0)
            {
                stepNo = Convert.ToInt32(Session["companyStep"]);
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

                //Company regCompany = ca.GetCompanyDetailsByFirstSpUserId(userId);

                List<Company> nonRegCompanies = ca.GetCompanyByCreayedCompany(userData.Company_Id);//regCompany.CompanyId   asanka

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
                return new HttpStatusCodeResult(404, "Your Session Expired");

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
            //Company regCompany = ca.GetCompanyDetailsByFirstSpUserId(userId);

            nonRegComModel.Company.CreatedByCompany = userData.Company_Id; //regCompany.CompanyId;  asanka

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
                int stepNo = Convert.ToInt32(Session["companyStep"]);
                if (stepNo < 0)
                {
                    stepNo = Convert.ToInt32(Session["companyStep"]);
                }

                if (stepNo < 5) return new HttpStatusCodeResult(404, "Your Session is Expired");
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
                ViewBag.NonRegCompanyId = new SelectList(nonRegCompanyList, "CompanyId", "CompanyName",1);

                NonRegCompanyBranchModel nonRegCompanyBranch = new NonRegCompanyBranchModel();
                nonRegCompanyBranch.CompanyBranch = new CompanyBranchModel();
                nonRegCompanyBranch.CompanyBranch.Company = new Company(); 
                //Get all non registered branches by company id
                List<NonRegBranch> nonRegBranches = ba.getNonRegBranches(curUser.Company_Id);
                nonRegCompanyBranch.NonRegBranches = nonRegBranches;
                nonRegCompanyBranch.CompanyBranch.Company = userNonRegCompany.Company;

                if (curUser.RoleId != 2) return PartialView(nonRegCompanyBranch);

                //Select non registered branch for admin's branch
                var adminBonRegBranches = new List<NonRegBranch>();
                adminBonRegBranches.AddRange(nonRegBranches.Where(t => curUser.BranchId == t.BranchId));
                nonRegCompanyBranch.NonRegBranches = adminBonRegBranches;
                
                return PartialView(nonRegCompanyBranch);
            }
            return new HttpStatusCodeResult(404, "Your Session Expired");
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/27
        /// Insert Non registered branch details
        /// </summary>
        /// <param name="nonRegCompanyBranch"></param>
        /// <param name="branchCode"></param>
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


            if (!CheckTheRangeOfPayOffPeriod(loanSetupStep1.payOffPeriod, loanSetupStep1.startDate, loanSetupStep1.maturityDate, loanSetupStep1.payOffPeriodType))
            {
                return new HttpStatusCodeResult(404, "Pay off period is out of range");
            }
            if (!CheckTheRangeOfPayOffPeriod(loanSetupStep1.autoReminderPeriod, loanSetupStep1.startDate, loanSetupStep1.maturityDate, 0))
            {

                return new HttpStatusCodeResult(404, "Auto reminder period is out of range");
            }

            if (!IsAtleastOneSelectUnitType(loanSetupStep1.allUnitTypes))
            {
                return new HttpStatusCodeResult(404, "Select Atleast One Unit Type");
            }

            // check he is super admin or admin
            if (new UserManageAccess().getUserRole(userId) > 2)
            {
                return new HttpStatusCodeResult(404, "You are Not Allowed");
            }

            // check if   step is 6...
            StepAccess sa = new StepAccess();
            if (Convert.ToInt32(Session["companyStep"]) < 6)
            {
                return new HttpStatusCodeResult(404, "You are Not Allowed");
            }

            LoanSetupAccess loanSetupAccess = new LoanSetupAccess();

            LoanSetupAccess la = new LoanSetupAccess();
            int loanId = la.getLoanIdByBranchId(loanSetupStep1.RegisteredBranchId);

            if (loanId > 0)
            {
                loanId = loanSetupAccess.insertLoanStepOne(loanSetupStep1, loanId);
            }
            else
            {
                loanId = la.getLoanIdByUserId(userId);


                loanId = loanSetupAccess.insertLoanStepOne(loanSetupStep1, loanId);
                if (loanId > 0)
                {
                    sa.updateStepNumberByUserId(userId, Convert.ToInt32(Session["companyStep"]), loanId, loanSetupStep1.RegisteredBranchId);
                }
            }

            Session["branchId"] = loanSetupStep1.RegisteredBranchId;
            if (loanSetupStep1.isInterestCalculate)
            {
                return RedirectToAction("step7");
            }
            else
            {
                sa.updateStepNumberByUserId(userId, 8, loanId, loanSetupStep1.RegisteredBranchId);
                return RedirectToAction("step8");
            }






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
            if (Session["AuthenticatedUser"] != null)
            {
                try
                {
                    userData = ((User)Session["AuthenticatedUser"]);
                }
                catch
                {
                    return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
                }
            }
            else
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
            }

            var id = userData.UserId;

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
                dashBoardModel.userName = (new UserManageAccess()).getUserNameById(id);
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
            User user = (new UserAccess()).retreiveUserByUserId(userId);
            return Json((new LoanSetupAccess()).IsUniqueLoanNumberForBranch(loanNumber, RegisteredBranchId, user), JsonRequestBehavior.AllowGet);
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
            int branchId = int.Parse(Session["branchId"].ToString());
            //int branchId = 35;
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
                Text = "EOM",
                Value = "EOM"
            });


            InterestAccess ia = new InterestAccess();
            Interest intrst = new Interest();
            //get Accrual Methods
            List<AccrualMethods> methodList = ia.GetAllAccrualMethods();
            //yes no list
            List<SelectListItem> yesOrNoList = new List<SelectListItem>();

            yesOrNoList.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "true"
            });
            yesOrNoList.Add(new SelectListItem
            {
                Text = "No",
                Value = "false"
            });
            ViewBag.NeedReminder = new SelectList(yesOrNoList, "Value", "Text");
            if (uId > 0)
            {
                LoanSetupAccess la = new LoanSetupAccess();
                int loanId = la.getLoanIdByBranchId(branchId);

                //int loanId = 1;
                if (loanId > 0)
                {
                    var intrstobj = ia.getInterestDetails(loanId);
                    if (intrstobj != null)
                    {

                        ViewBag.Edit = 1;
                        //intrst = ia.getInterestDetails(loanId);
                        ViewBag.AccrualMethodId = new SelectList(methodList, "MethodId", "MethodName", intrstobj.AccrualMethodId);

                        if (intrstobj.option != "once a month")
                        {
                            ViewBag.Option = true;
                        }
                        else
                        {
                            ViewBag.Option = false;
                        }
                        ViewBag.PaidDate = new SelectList(listdates, "Value", "Text", intrstobj.PaidDate);
                        //ViewBag.Email = intrst.AutoRemindEmail;
                        return PartialView(intrstobj);
                    }

                    else
                    {
                        ViewBag.Edit = 0;
                        ViewBag.AccrualMethodId = new SelectList(methodList, "MethodId", "MethodName");
                        ViewBag.PaidDate = new SelectList(listdates, "Value", "Text");
                        string defaultEmail = la.getAutoRemindEmailByLoanId(loanId);
                        ViewBag.Email = defaultEmail;
                        //intrst.AutoRemindEmail = defaultEmail;

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
            int branchId = int.Parse(Session["branchId"].ToString());
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

            if (reslt >= 0)
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
            //else if (reslt == 0)
            //{
            //    return RedirectToAction("Step8");
            //}
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
            if (Session["userId"] == null)
            {
                return new HttpStatusCodeResult(404, "Your Session Expired");
            }
            var userId = (int)Session["userId"];

            BranchAccess branch = new BranchAccess();
            int companyType = branch.getCompanyTypeByUserId(userId);
            if (companyType == 1)
            {
                ViewBag.isLender = true;
            }
            else
            {
                ViewBag.isLender = false;
            }
            Fees fee = new Fees();
            LoanSetupAccess loan = new LoanSetupAccess();
            fee.LoanId = loan.getLoanIdByUserId(userId);

            var hasLoan = loan.checkLoanIsInFeesTables(fee.LoanId);

            if (hasLoan.AdvanceAmount > 0 || hasLoan.MonthlyLoanAmount > 0 || hasLoan.LotInspectionAmount > 0)
            {
                ViewBag.isEdit = "editable";
                hasLoan.LoanId = fee.LoanId;
                hasLoan.isEdit = true;
                hasLoan.IsAdvanceFeeCompleteEmailReminder = false;
                hasLoan.IsLoanFeeCompleteEmailReminder = false;
                hasLoan.IsLotFeeCompleteEmailReminder = false;
                hasLoan.IsAdvanceFeeDueEmailReminder = false;
                hasLoan.IsLoanFeeDueEmailReminder = false;
                hasLoan.IsLotFeeDueEmailReminder = false;

                if (hasLoan.AdvanceFeeDealerEmail != "")
                {
                    hasLoan.IsAdvanceFeeCompleteEmailReminder = true;
                }
                if (hasLoan.MonthlyLoanFeeDealerEmail != "")
                {
                    hasLoan.IsLoanFeeCompleteEmailReminder = true;
                }
                if (hasLoan.LotInspectionFeeDealerEmail != "")
                {
                    hasLoan.IsLotFeeCompleteEmailReminder = true;
                }
                if (hasLoan.AdvanceDueEmail != "")
                {
                    hasLoan.IsAdvanceFeeDueEmailReminder = true;
                }
                if (hasLoan.MonthlyLoanDueEmail != "")
                {
                    hasLoan.IsLoanFeeDueEmailReminder = true;
                }
                if (hasLoan.LotInspectionDueEmail != "")
                {
                    hasLoan.IsLotFeeDueEmailReminder = true;
                }
                return PartialView(hasLoan);
            }
            else
            {
                ViewBag.isEdit = "notEditable";

                Fees feeNew = new Fees();
                feeNew.LoanId = fee.LoanId;

                if (feeNew.LoanId > 0)
                {
                    feeNew.isEdit = false;
                    var email = loan.getAutoRemindEmailByLoanId(feeNew.LoanId);

                    feeNew.AdvanceDueEmail = email;
                    feeNew.MonthlyLoanDueEmail = email;
                    feeNew.LotInspectionDueEmail = email;

                    hasLoan.IsAdvanceFeeCompleteEmailReminder = false;
                    hasLoan.IsLotFeeCompleteEmailReminder = false;
                    hasLoan.IsLoanFeeCompleteEmailReminder = false;
                    hasLoan.IsAdvanceFeeDueEmailReminder = false;
                    hasLoan.IsLotFeeDueEmailReminder = false;
                    hasLoan.IsLoanFeeDueEmailReminder = false;

                    return PartialView(feeNew);
                }
                else
                {
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
                fees.AdvanceDueDate = "ToA";
            }
            if (fees.MonthlyLoanDue == "Time of Advance")
            {
                fees.MonthlyLoanDueDate = "ToA";
            }
            if (fees.IsAdvanceFeeCompleteEmailReminder == false)
            {
                fees.AdvanceFeeDealerEmail = "";
                fees.AdvanceFeeDealerEmailRemindPeriod = 0;
            }
            if (fees.IsLoanFeeCompleteEmailReminder == false)
            {
                fees.MonthlyLoanFeeDealerEmail = "";
                fees.MonthlyLoanFeeDealerEmailRemindPeriod = 0;
            }
            if (fees.IsLotFeeCompleteEmailReminder == false)
            {
                fees.LotInspectionFeeDealerEmail = "";
                fees.LotInspectionFeeDealerEmailRemindPeriod = 0;
            }
            if (fees.IsAdvanceFeeDueEmailReminder == false)
            {
                fees.AdvanceDueEmail = "";
                fees.AdvanceDueEmailRemindPeriod = 0;
            }
            if (fees.IsLoanFeeDueEmailReminder == false)
            {
                fees.MonthlyLoanDueEmail = "";
                fees.MonthlyLoanDueEmailRemindPeriod = 0;
            }
            if (fees.IsLotFeeDueEmailReminder == false)
            {
                fees.LotInspectionDueEmail = "";
                fees.LotInspectionDueEmailRemindPeriod = 0;
            }
            if (step.InsertFeesDetails(fees))
            {
                var userId = (int)Session["userId"];
                var branchId = (int)Session["branchId"];

                if (fees.isEdit == true)
                {
                    return RedirectToAction("Step9");
                }
                else if (step.updateStepNumberByUserId(userId, 9, fees.LoanId, branchId))
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
            //yes no list
            List<SelectListItem> isTitleTrackList = new List<SelectListItem>();

            isTitleTrackList.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "true"
            });
            isTitleTrackList.Add(new SelectListItem
            {
                Text = "No",
                Value = "false"
            });
            ViewBag.isTitleTrack = new SelectList(isTitleTrackList, "Value", "Text");

            List<SelectListItem> isReceiptList = new List<SelectListItem>();

            isReceiptList.Add(new SelectListItem
            {
                Text = "Yes",
                Value = "true"
            });
            isReceiptList.Add(new SelectListItem
            {
                Text = "No",
                Value = "false"
            });
            ViewBag.IsReceipRequired = new SelectList(isReceiptList, "Value", "Text");
            //Accept Methods
            List<SelectListItem> acceptMethodsList = new List<SelectListItem>();

            acceptMethodsList.Add(new SelectListItem
            {
                Text = "Title Present To Advance",
                Value = "Title Present To Advance"
            });


            acceptMethodsList.Add(new SelectListItem
            {
                Text = "Scanned Title Adequate",
                Value = "Scanned Title Adequate"
            });

            acceptMethodsList.Add(new SelectListItem
            {
                Text = "Title Can Arrive At Any Time",
                Value = "Title Can Arrive At Any Time"
            });

            acceptMethodsList.Add(new SelectListItem
            {
                Text = "Title Can Arrive Within A Set Time",
                Value = "Title Can Arrive Within A Set Time"
            });


            //Time Limit Options
            List<SelectListItem> timeLimitList = new List<SelectListItem>();

            timeLimitList.Add(new SelectListItem
            {
                Text = "At Advance Date",
                Value = "At Advance Date"
            });


            timeLimitList.Add(new SelectListItem
            {
                Text = "With In 7 Days",
                Value = "With In 7 Days"
            });

            timeLimitList.Add(new SelectListItem
            {
                Text = "At Any Time",
                Value = "At Any Time"
            });

            //Receipt required methods
            List<SelectListItem> receiptRequiredMethodList = new List<SelectListItem>();

            receiptRequiredMethodList.Add(new SelectListItem
            {
                Text = "Physically",
                Value = "Physically"
            });


            receiptRequiredMethodList.Add(new SelectListItem
            {
                Text = "Scan Copy",
                Value = "Scan Copy"
            });

            receiptRequiredMethodList.Add(new SelectListItem
            {
                Text = "Physically And Scan Copy",
                Value = "Physically And Scan Copy"
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
                    var titleObj = ta.getTitleDetails(loanId);
                    if (titleObj != null)
                    {

                        ViewBag.Edit = 1;
                        //title = ta.getTitleDetails(loanId);
                        ViewBag.TitleAcceptMethod = new SelectList(acceptMethodsList, "Value", "Text", titleObj.TitleAcceptMethod);
                        ViewBag.ReceivedTimeLimit = new SelectList(timeLimitList, "Value", "Text", titleObj.ReceivedTimeLimit);
                        ViewBag.ReceiptRequiredMethod = new SelectList(receiptRequiredMethodList, "Value", "Text", titleObj.ReceiptRequiredMethod);
                        ViewBag.DefaultEmail = titleObj.RemindEmail;
                        return PartialView(titleObj);
                    }

                    else
                    {
                        ViewBag.Edit = 0;
                        ViewBag.TitleAcceptMethod = new SelectList(acceptMethodsList, "Value", "Text");
                        ViewBag.ReceivedTimeLimit = new SelectList(timeLimitList, "Value", "Text");
                        ViewBag.ReceiptRequiredMethod = new SelectList(receiptRequiredMethodList, "Value", "Text");
                        string defaultEmail = la.getAutoRemindEmailByLoanId(loanId);

                        ViewBag.Email = defaultEmail;
                        //title.LoanId = loanId;
                        return PartialView();
                    }
                    //return PartialView(title);
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


            TitleAccess ta = new TitleAccess();
            LoanSetupAccess la = new LoanSetupAccess();
            StepAccess sa = new StepAccess();
            int loanId = la.getLoanIdByBranchId(branchId);
            title.LoanId = loanId;

            //if (title.IsReceipRequired || title.IsTitleTrack)
            //{
            int reslt = ta.insertTitleDetails(title);
            if (reslt >= 0)
            {

                if (sa.updateStepNumberByUserId(userId, 10, title.LoanId, branchId))
                {
                    return RedirectToAction("Step10");
                }
                else
                {
                    return new HttpStatusCodeResult(404, "error message");
                }
            }
            //    else if (reslt == 0)
            //    {
            //        return RedirectToAction("Step10");
            //}
            else
            {
                return new HttpStatusCodeResult(404, "error message");
            }
            //}
            //else
            //{
            //    if (sa.updateStepNumberByUserId(userId, 10, title.LoanId, branchId))
            //    {
            //    return RedirectToAction("Step10");
            //}
            //    else
            //    {
            //        return new HttpStatusCodeResult(404, "error message");
            //    }

            //}


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
        public bool CheckTheRangeOfPayOffPeriod(int payOffPeriod, DateTime startDate, DateTime maturityDate, int payOffPeriodType)
        {
            if (payOffPeriodType == 0)
            {
                int totalDays = (int)(maturityDate - startDate).TotalDays;
                if (payOffPeriod <= totalDays)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {

                int diffMonths = (maturityDate.Month + maturityDate.Year * 12) - (startDate.Month + startDate.Year * 12);
                if (payOffPeriod <= diffMonths)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        //Gloable variables
        private static LoanSetupStep1 _loan;
        private static CurtailmentModel _gCurtailment;
        private static int _difPercentage;

        // GET: LoanSetUpStep5
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Step10()
        {
            //Check current session is not null or empty
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return new HttpStatusCodeResult(404, "Your Session Expired");

            int userId = Convert.ToInt32(Session["userId"]);

            //check user step is valid for this step
            StepAccess sa = new StepAccess();
            if (Convert.ToInt32(Session["companyStep"]) == 10)
            {
                int branchId = int.Parse(Session["branchId"].ToString());

                LoanSetupAccess la = new LoanSetupAccess();
                int loanId = la.getLoanIdByBranchId(branchId);

                CurtailmentAccess curAccess = new CurtailmentAccess();
                _loan = curAccess.GetLoanDetailsByLoanId(loanId);
                _loan.loanId = loanId;

                //_gCurtailment = new CurtailmentModel();
                _gCurtailment = new CurtailmentModel();
                _gCurtailment.AdvancePt = _loan.advancePercentage;
                _difPercentage = 100 - _loan.advancePercentage;
                _gCurtailment.RemainingPercentage = _gCurtailment.AdvancePt;

                _gCurtailment.RemainingTime = _loan.payOffPeriod;

                _gCurtailment.TimeBase = "Months";
                if (_loan.payOffPeriodType == 0) _gCurtailment.TimeBase = "Days";

                _gCurtailment.Activate = _loan.LoanStatus ? "Yes" : "No";

                _gCurtailment.InfoModel = new List<Curtailment>();

                var curtailments = curAccess.retreiveCurtailmentByLoanId(loanId);

                int payPercentage = _loan.advancePercentage;
                int? totalPercentage = 0;

                int curId = 0;
                if (curtailments !=null && curtailments.Count > 0)
                {
                    for (int i = 0; i < curtailments.Count; i++)
                    {
                        curId++;
                        totalPercentage += curtailments[i].Percentage;
                        _gCurtailment.InfoModel.Add(new Curtailment { CurtailmentId = curId, TimePeriod = curtailments[i].TimePeriod, Percentage = curtailments[i].Percentage });
                    }
                }

                _calMode = "Full Payment";
                ViewBag.CalMode = _calMode;
                _gCurtailment.RemainingPercentage = payPercentage - totalPercentage;

                if (_gCurtailment.RemainingPercentage > 0)
                    _gCurtailment.InfoModel.Add(new Curtailment { CurtailmentId = curId + 1 });
                ViewData["objmodel"] = _gCurtailment;
                return PartialView(_gCurtailment);
            }
            return new HttpStatusCodeResult(404, "Your Session Expired");
        }

        [HttpPost]
        public ActionResult Step10(string submit)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return new HttpStatusCodeResult(404, "Your Session is Expired");
            ViewBag.CalMode = _calMode;
            return PartialView(_gCurtailment);
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 02/19/2016
        /// 
        /// save data 
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCurtailment()
        {
            bool isError = false;

            if (_gCurtailment.RemainingPercentage != 0) return PartialView("Step10", _gCurtailment);
            foreach (Curtailment item in _gCurtailment.InfoModel)
            {
                if (item.TimePeriod == 0 || item.TimePeriod == null)
                {
                    isError = true;
                    break;
                }
                if ((item.Percentage == 0 || item.Percentage == null) &&
                    (item.TimePeriod != 0 || item.TimePeriod != null))
                {
                    isError = true;
                    break;
                }
            }

            int userId = Convert.ToInt32(Session["userId"]);

            if(string.IsNullOrEmpty(userId.ToString()))
                return new HttpStatusCodeResult(404, "Your Session Expired");

            if (isError) return PartialView("Step10", _gCurtailment);
            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();

            bool loanActive = _gCurtailment.Activate == "Yes";

            if (curtailmentAccess.InsertCurtailment(_gCurtailment.InfoModel, _loan.loanId) == 1)
            {


                ViewBag.SuccessMsg = "Curtailment Details added successfully";
                StepAccess stepAccess = new StepAccess();

                stepAccess.updateStepNumberByUserId(userId, 11);
                ViewBag.Redirect = 1;
                //return RedirectToAction("UserDetails", "UserManagement");
            }
            else
            {
                ViewBag.SuccessMsg = "Curtailment Details updated successfully";
            }
            LoanSetupAccess loanAccess = new LoanSetupAccess();
            loanAccess.updateLoanActivation(loanActive, _loan.loanId);

            ViewBag.CalMode = _calMode;
            return PartialView("Step10", _gCurtailment);
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 02/19/2016
        /// 
        /// Bind current row data to global model
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return partial view</returns>
        public ActionResult SetCurtailment(Curtailment model)
        {
            ViewBag.ErrorMsg = "";
            int? prePercentage = _gCurtailment.InfoModel[model.CurtailmentId - 1].Percentage ?? 0;

            //validate percentage
            if (model.TimePeriod == 0 || model.TimePeriod == null)
            {
                ViewBag.ErrorMsg = "Invalid TimePeriod found.";
                return PartialView("Step10", _gCurtailment);
            }
            else if (model.CurtailmentId > 1 &&
                     model.TimePeriod <=
                     _gCurtailment.InfoModel[model.CurtailmentId - 2].TimePeriod && model.TimePeriod <= _gCurtailment.RemainingTime)
            {
                _gCurtailment.InfoModel[model.CurtailmentId - 1].TimePeriod = model.TimePeriod;
                ViewBag.ErrorMsg = "Entered time period is invalid!";
                return PartialView("Step10", _gCurtailment);
            }
            if (model.TimePeriod > _gCurtailment.RemainingTime)
            {
                ViewBag.ErrorMsg = "TimePeriod must be less than pay off period";
                return PartialView("Step10", _gCurtailment);
            }

            if ((model.Percentage == 0 || model.Percentage == null) && (model.TimePeriod != 0 || model.TimePeriod != null))
            {
                ViewBag.ErrorMsg = "Invalid Percentage found.";
                return PartialView("Step10", _gCurtailment);
            }
            if (model.Percentage > 0 && _gCurtailment.RemainingPercentage - model.Percentage + prePercentage < 0)
            {
                ViewBag.ErrorMsg = "Invalid percentage found!";
                return PartialView("Step10", _gCurtailment);
            }

            //update curtailment grid row
            if (_gCurtailment.InfoModel.Count >= model.CurtailmentId)
            {
                _gCurtailment.InfoModel[model.CurtailmentId - 1].TimePeriod = model.TimePeriod;
                _gCurtailment.InfoModel[model.CurtailmentId - 1].Percentage = model.Percentage;
                _gCurtailment.RemainingPercentage -= model.Percentage - prePercentage;
            }

            if (_gCurtailment.InfoModel.Count == model.CurtailmentId && _gCurtailment.RemainingPercentage > 0)
                _gCurtailment.InfoModel.Add(new Curtailment { CurtailmentId = model.CurtailmentId + 1 });

            ViewBag.CalMode = _calMode;
            return PartialView("Step10", _gCurtailment);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult DeleteCurtailmentRow(Curtailment model)
        {
            int? curRmeinpt = _gCurtailment.RemainingPercentage;
            int preCout = _gCurtailment.InfoModel.Count;
            if (_gCurtailment.InfoModel.Count > 1 && _gCurtailment.InfoModel.Count != model.CurtailmentId)
            {
                _gCurtailment.InfoModel.RemoveAt(model.CurtailmentId - 1);
                _gCurtailment.RemainingPercentage += model.Percentage;

                for (int i = model.CurtailmentId - 1; i < _gCurtailment.InfoModel.Count; i++)
                    //_gCurtailment.InfoModel.Count > 0 && 
                {
                    _gCurtailment.InfoModel[i].CurtailmentId = i + 1;
                }
            }
            ViewBag.CalMode = _calMode;

            if(curRmeinpt != null && curRmeinpt == 0 && preCout > model.CurtailmentId)
                _gCurtailment.InfoModel.Add(new Curtailment { CurtailmentId = _gCurtailment.InfoModel.Count + 1 });
            return PartialView("Step10", _gCurtailment);
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 02/19/2016
        /// 
        /// Check entered time is valid
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return partial view</returns>
        public ActionResult CheckTimePeriod(Curtailment model)
        {
            ViewBag.ErrorMsg = "";

            if (model.TimePeriod == 0 || model.TimePeriod == null)
            {
                _gCurtailment.InfoModel[model.CurtailmentId - 1].TimePeriod = 0;
                ViewBag.ErrorMsg = "Invalid TimePeriod found.";
            }
            else if (model.CurtailmentId > 1 &&
                     model.TimePeriod <=
                     _gCurtailment.InfoModel[model.CurtailmentId - 2].TimePeriod &&
                     model.TimePeriod <= _gCurtailment.RemainingTime)
            {
                ViewBag.ErrorMsg = "Entered time period is invalid!";
            }
            else if(model.TimePeriod > _gCurtailment.RemainingTime)
            {
                ViewBag.ErrorMsg = "TimePeriod must be less than pay off period";
            }
            ViewBag.CalMode = _calMode;
            _gCurtailment.InfoModel[model.CurtailmentId - 1].TimePeriod = model.TimePeriod;
            return PartialView("Step10", _gCurtailment);
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 02/19/2016
        /// 
        /// Change remaining percentage when user change 
        /// calculation base type
        /// 
        /// </summary>
        /// <param name="calcMode"></param>
        /// <returns>Return partial view</returns>
        public ActionResult SetPercentage(string calcMode)
        {
            int? curTotalPt = 0;

            for (int i = 0; i < _gCurtailment.InfoModel.Count - 1; i++)
            {
                curTotalPt += _gCurtailment.InfoModel[i].Percentage;
            }

            if (calcMode == "Advance")
            {
                _calMode = "Advance";
                //_gCurtailment.RemainingPercentage += _difPercentage;

                _gCurtailment.RemainingPercentage = 100;
                if (curTotalPt !=null)
                    _gCurtailment.RemainingPercentage = 100 - curTotalPt;
            }
            else
            {
                _calMode = "Full Payment";
                //_gCurtailment.RemainingPercentage -= _difPercentage;

                _gCurtailment.RemainingPercentage = _gCurtailment.AdvancePt;
                if (curTotalPt != null)
                    _gCurtailment.RemainingPercentage = _gCurtailment.AdvancePt - curTotalPt;
            }

            ViewBag.CalMode = _calMode;

            return PartialView("Step10", _gCurtailment);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loanStatus"></param>
        /// <returns></returns>
        public ActionResult SetLoanStatus(string loanStatus)
        {
            _gCurtailment.Activate = loanStatus == "Yes" ? "Yes" : "No";

            ViewBag.CalMode = _calMode;
            return PartialView("Step10", _gCurtailment);
        }
    }
}

