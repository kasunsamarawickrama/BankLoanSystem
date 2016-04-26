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
        private static int _isEdit;

        private static string _calMode;
        User userData = new User();
        LoanSetupStep loanData = new LoanSetupStep();
        int loanstep = 0;

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
            try
            {
                if ((Session["AuthenticatedUser"] != null))// || (Session["loanStep"] != null)
                 {    
                    userData = ((User)Session["AuthenticatedUser"]);
                    if (Session["loanStep"] != null)
                    {
                        
                        loanData = ((LoanSetupStep)Session["loanStep"]);
                        Session["companyStep"] = 5;

                        if(loanData.loanId > 0)
                        {
                            loanstep = loanData.stepId;
                            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();

                           
                            _loan = curtailmentAccess.GetLoanDetailsByLoanId(loanData.loanId);
                            
                            Session["isInterest"] = _loan.isInterestCalculate;
                        }
                    }
                }
                else
                {
                    if (HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new HttpStatusCodeResult(404, "Session Expired");
                    }
                    else
                    {

                        filterContext.Result = new RedirectResult("~/Login/UserLogin");
                    }
                }
            }
            catch(Exception e)
            {
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
            }
        }

        public ActionResult Index()
        {
            int stepNo = Convert.ToInt32(Session["companyStep"]);
            int userId = userData.UserId;

            stepNo = stepNo + loanstep;

            ViewBag.Step = stepNo;
            Session["stepNo"] = stepNo;
            //Get company details if branch same as company

            if (stepNo == 2)
            {
                CompanyAccess ca = new CompanyAccess();
                Company company = ca.GetCompanyDetailsCompanyId(userData.Company_Id);

                //return View();
                return RedirectToAction("Step2");
            }

            else if (stepNo == 5)
            {
                CompanyAccess ca = new CompanyAccess();

                Company nonRegCompany = ca.GetNonRegCompanyDetailsByRegCompanyId(userData.Company_Id);

                if (string.IsNullOrEmpty(nonRegCompany.CompanyName)) return RedirectToAction("Step4", "SetupProcess");

                CompanyBranchModel comBranch = new CompanyBranchModel();
                comBranch.Company = nonRegCompany;
                TempData["NonRegCompany"] = comBranch;

                //return View();
                return RedirectToAction("Step5");
            }
            else if (stepNo == 0)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Company Setup is on going Please Contact Admin" });
            }
            else
            {
                return RedirectToAction("Step" + stepNo);
            }
        }


        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/26
        /// As the initial super admin I should able to create company
        /// in the setup proccess
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step1(string edit)
        {
            int userId = userData.UserId;
            int roleId = userData.RoleId;
            CompanyAccess ca = new CompanyAccess();

            // check he is a super admin or admin
            if (roleId != 1)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Could not authenticate you." });
            }

            // Get company types to list
            List<CompanyType> ctList = ca.GetAllCompanyType();
            ViewBag.TypeId = new SelectList(ctList, "TypeId", "TypeName");

            //Get states to list
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            if ((Convert.ToInt32(Session["companyStep"]) >= 1)||((Convert.ToInt32(Session["companyStep"])==0)&&(edit== "bshdrd")))
            {
                Company preCompany = ca.GetCompanyDetailsCompanyId(userData.Company_Id);
               

                if (preCompany != null)
                {
                    _comCode = preCompany.CompanyCode;
                    ViewBag.Edit = "Yes";
                    _isEdit = 1;
                }

                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return PartialView(preCompany);
                }
                else
                {

                    return View(preCompany);
                }
            }

            
            return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
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

            int companyId = ca.InsertCompany(company, type);

            if (companyId > 0)
            {
                ViewBag.SuccessMsg = "Company Successfully setup.";

                CompanyType = (company.TypeId == 1) ? "Lender" : "Dealer";

                //If succeed update step table to step2 
                StepAccess sa = new StepAccess();
                if(type== "INSERT")
                {
                    bool res = sa.UpdateCompanySetupStep(companyId, userData.BranchId, 2);

                    //insert to log 
                    Log log = new Log(userData.UserId, companyId, 0, 0, "Company Step", "Inserted company : " + _comCode, DateTime.Now);

                   (new LogAccess()).InsertLog(log);
                }else if (type == "UPDATE")
                {
                    //insert to log 
                    Log log = new Log(userData.UserId, companyId, 0, 0, "Company Step", "Updated company : " + company.CompanyCode, DateTime.Now);

                    (new LogAccess()).InsertLog(log);
                }
                
                Session["companyStep"] = 2;

                //user object pass to session
                userData.Company_Id = companyId;
                userData.CompanyName = company.CompanyName;
                Session["AuthenticatedUser"] = userData;

                //sa.updateStepNumberByUserId(company.FirstSuperAdminId, 2);

                //Send company detail to step 2
                CompanyBranchModel comBranch = new CompanyBranchModel();
                comBranch.Company = company;

                TempData["Company"] = comBranch;
                return RedirectToAction("Step2");
            }

            return RedirectToAction("UserLogin", "Login", new { lbl = "Failed to Setup company." });
            //return new HttpStatusCodeResult(404, "Failed to Setup company.");
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/26
        /// Get first branch details
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step2(string edit1,int? edit)
        {
            //edit = 3;
            int userId = userData.UserId;
            int roleId = userData.RoleId;
            // check he is a super admin or admin


            if (roleId != 1)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            //StepAccess cs = new StepAccess();
            int reslt = Convert.ToInt32(Session["companyStep"]);
            if ((reslt==0)&&(edit1 == "bshdrdhbrn"))
            {
                reslt = 2;
            }

            //int reslt = 2;
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

                //Get company details by company id
                CompanyAccess ca = new CompanyAccess();
                Company preCompany = ca.GetCompanyDetailsCompanyId(userData.Company_Id);

                //CompanyAccess ca = new CompanyAccess();
                //Company preCompany = new Company();
                //DataSet dsCompany = new DataSet();
                //dsCompany = ca.GetCompanyDetailsCompanyId(userData.Company_Id);
                //if (dsCompany.Tables[0].Rows.Count > 0)
                //{
                //    preCompany.CompanyId = int.Parse(dsCompany.Tables[0].Rows[0]["company_Id"].ToString());
                //    preCompany.CompanyName = dsCompany.Tables[0].Rows[0]["company_name"].ToString();
                //    preCompany.CompanyCode = dsCompany.Tables[0].Rows[0]["company_code"].ToString();
                //    preCompany.CompanyAddress1 = dsCompany.Tables[0].Rows[0]["company_address_1"].ToString();
                //    preCompany.CompanyAddress2 = dsCompany.Tables[0].Rows[0]["company_address_2"].ToString();
                //    preCompany.StateId = int.Parse(dsCompany.Tables[0].Rows[0]["stateId"].ToString());
                //    preCompany.City = dsCompany.Tables[0].Rows[0]["city"].ToString();
                //    preCompany.Zip = dsCompany.Tables[0].Rows[0]["zip"].ToString();

                //    string[] zipWithExtention = preCompany.Zip.Split('-');

                //    if (zipWithExtention[0] != null) preCompany.ZipPre = zipWithExtention[0];
                //    if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) preCompany.Extension = zipWithExtention[1];

                //    preCompany.Email = dsCompany.Tables[0].Rows[0]["email"].ToString();
                //    preCompany.PhoneNum1 = dsCompany.Tables[0].Rows[0]["phone_num_1"].ToString();
                //    preCompany.PhoneNum2 = dsCompany.Tables[0].Rows[0]["phone_num_2"].ToString();
                //    preCompany.PhoneNum3 = dsCompany.Tables[0].Rows[0]["phone_num_3"].ToString();
                //    preCompany.Fax = dsCompany.Tables[0].Rows[0]["fax"].ToString();
                //    preCompany.WebsiteUrl = dsCompany.Tables[0].Rows[0]["website_url"].ToString();
                //    preCompany.TypeId = int.Parse(dsCompany.Tables[0].Rows[0]["company_type"].ToString());
                //}


                userCompany.Company = preCompany;

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
            else
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
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
                userCompany2.MainBranch.BranchCode = ba.createBranchCode(userCompany.Company.CompanyCode);
                userCompany.MainBranch = userCompany2.MainBranch;
            }

            int reslt = ba.insertFirstBranchDetails(userCompany2, userId);
            //userData.BranchId = reslt;
            if (reslt > 0)
            {
                Session["companyStep"] = 3;

                //user object pass to session
                if(userData.BranchId == 0)
                {
                    userData.BranchId = reslt;
                }
                
                Session["AuthenticatedUser"] = userData;

                StepAccess sa = new StepAccess();
                if (sa.UpdateCompanySetupStep(userData.Company_Id, reslt, 3))
                {

                    // bool reslt2 = ba.updateUserBranchId(userCompany2, userId);

                    return RedirectToAction("Step3");

                }
            }
            else
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Failed to set up branch" });
                //return new HttpStatusCodeResult(404, "Failed to set up branch");
            }

            userCompany.MainBranch = new Branch();
            ViewBag.BranchIndex = 0;

            //Get company details by user id
            userId = userData.UserId;

            // need common method for that - asanka

            CompanyAccess ca = new CompanyAccess();
            Company preCompany = ca.GetCompanyDetailsCompanyId(userData.Company_Id);
            //CompanyAccess ca = new CompanyAccess();
            //Company preCompany = new Company();
            //DataSet dsCompany = new DataSet();
            //dsCompany = ca.GetCompanyDetailsCompanyId(userData.Company_Id);
            //if (dsCompany.Tables[0].Rows.Count > 0)
            //{
            //    preCompany.CompanyId = int.Parse(dsCompany.Tables[0].Rows[0]["company_Id"].ToString());
            //    preCompany.CompanyName = dsCompany.Tables[0].Rows[0]["company_name"].ToString();
            //    preCompany.CompanyCode = dsCompany.Tables[0].Rows[0]["company_code"].ToString();
            //    preCompany.CompanyAddress1 = dsCompany.Tables[0].Rows[0]["company_address_1"].ToString();
            //    preCompany.CompanyAddress2 = dsCompany.Tables[0].Rows[0]["company_address_2"].ToString();
            //    preCompany.StateId = int.Parse(dsCompany.Tables[0].Rows[0]["stateId"].ToString());
            //    preCompany.City = dsCompany.Tables[0].Rows[0]["city"].ToString();
            //    preCompany.Zip = dsCompany.Tables[0].Rows[0]["zip"].ToString();

            //    string[] zipWithExtention = preCompany.Zip.Split('-');

            //    if (zipWithExtention[0] != null) preCompany.ZipPre = zipWithExtention[0];
            //    if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) preCompany.Extension = zipWithExtention[1];

            //    preCompany.Email = dsCompany.Tables[0].Rows[0]["email"].ToString();
            //    preCompany.PhoneNum1 = dsCompany.Tables[0].Rows[0]["phone_num_1"].ToString();
            //    preCompany.PhoneNum2 = dsCompany.Tables[0].Rows[0]["phone_num_2"].ToString();
            //    preCompany.PhoneNum3 = dsCompany.Tables[0].Rows[0]["phone_num_3"].ToString();
            //    preCompany.Fax = dsCompany.Tables[0].Rows[0]["fax"].ToString();
            //    preCompany.WebsiteUrl = dsCompany.Tables[0].Rows[0]["website_url"].ToString();
            //    preCompany.TypeId = int.Parse(dsCompany.Tables[0].Rows[0]["company_type"].ToString());
            //}


            userCompany.Company = preCompany;

            IList<Branch> branches = ba.getBranchesByCompanyCode(preCompany.CompanyCode);
            userCompany.SubBranches = branches;

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
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step3(string lbls)
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

            if (Convert.ToInt32(Session["companyStep"]) < 3)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            if (lbls != null && lbls.Equals("User Successfully Created"))
            {
                ViewBag.SuccessMsg = "User Successfully Created";
                //sa.updateStepNumberByUserId(userId, 4);
                int rol = int.Parse(Session["abcRol"].ToString());
                int br = int.Parse(Session["abcBrnc"].ToString());
                if ((rol == 1) && (br == 0))
                {
                    sa.UpdateCompanySetupStep(userData.Company_Id, userData.BranchId, 4);
                }
                else if ((rol == 2) && (br != 0))
                {
                    sa.UpdateCompanySetupStep(userData.Company_Id, br, 4);
                }
                Session["abcRol"] = "";
                Session["abcBrnc"] = "";
                Session["companyStep"] = 4;

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
                if ((roleList[i].RoleId == 3)||(roleList[i].RoleId == 4))
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
            
                user.PhoneNumber = user.PhoneNumber2;
            
            int currentUser = userData.UserId;

            // check he is a super admin or admin
            int roleId = userData.RoleId;

            if (roleId > 2)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            // check if   step is 3...
            if (Convert.ToInt32(Session["companyStep"]) < 3)
            {
                return RedirectToAction("UserLogin", "Login");
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
            if (roleId == 2)
            {
                user.BranchId = userData.BranchId;
            }
            user.step_status = userData.step_status;
            //Insert user
            int res = ua.InsertUser(user);

            //Insert new user to user activation table
            string activationCode = Guid.NewGuid().ToString();
            int userId = (new UserAccess()).getUserId(user.Email);
            res = ua.InsertUserActivation(userId, activationCode);
            if (res == 1)
            {
                //insert to log 
                Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId,0, "Create User in Company setup", "created "+(user.RoleId == 1 ? "Super Admin" : "Admin") + ", Username : " + user.UserName, DateTime.Now);

                (new LogAccess()).InsertLog(log);

                string body = "Hi " + user.FirstName + "! <br /><br /> Your account has been successfully created. Below in your account detail." +
                              "<br /><br /> User name: " + user.UserName +
                                    "<br /> Password : <b>" + passwordTemp +
                              "<br />Click <a href='http://localhost:57318/CreateUser/ConfirmAccount?userId=" + userId + "&activationCode=" + activationCode + "'>here</a> to activate your account." +
                              "<br /><br/> Thanks,<br /> Admin.";

                Email email = new Email(user.Email);
                
                Session["abcRol"] = user.RoleId;
                Session["abcBrnc"] = user.BranchId;
                email.SendMail(body, "Account details");



                ViewBag.SuccessMsg = "User Successfully Created";

                //additional page ----> Add User Rights
                //if()

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
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step6(string dashbrd)
        {
            //dashbrd = 6;
            int userrole = userData.RoleId;
            int userId = userData.UserId;

            if (userrole >= 3)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
            }

            UserManageAccess uma = new UserManageAccess();

            // check if step is less than 6, not allowed to this page...

            int stepNo = loanData.stepId;


            if (stepNo < 0)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
            }
            else if ((stepNo == 0)&&(dashbrd== "bshdrdoanl"))
            {
                stepNo = 6;
                loanData.stepId = 1;
                Session["dashboard"] = 1;

                // set the loan setup session to step 1

               
                Session["companyStep"] = 5;
                loanData.stepId = 1;
                Session["loanStep"] = loanData;



            }

            // get the Role Name for front end view
            ViewBag.userroleName = uma.getUserRoleName(userId);

            BranchAccess ba = new BranchAccess();

            // get the Company type for front end view
            int comType = ba.getCompanyTypeByUserId(userId);
            //int comType = userData.CompanyType;
            ViewBag.ThisCompanyType = (comType == 1) ? "Lender" : "Dealer";//

            // retrieve registered branches, nonregistered branches using his company Id

            List<Branch> RegisteredBranchLists = (new BranchAccess()).getBranches(userData.Company_Id);
            List<NonRegBranch> NonRegisteredBranchLists = (new BranchAccess()).getNonRegBranches(userData.Company_Id);

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

            if ((userrole == 1) || (userrole == 2))
            {
                loanId = loanData.loanId;
            }

            // if loan number exists get the loan details
            if (loanId > 0)
            {
                loanSetupStep1 = la.GetLoanStepOne(loanId);
            }

            if (userrole == 2)
            {
                // if user is a admin, his branch id is registerd branch id
                loanSetupStep1.RegisteredBranchId = userData.BranchId;

                // the get registered branch detail from the company branches list
                foreach (Branch branch in RegisteredBranchLists)
                {
                    if (branch.BranchId == userData.BranchId)
                    {
                        var newList = new List<Branch>();
                        newList.Add(branch);
                        ViewBag.RegisteredBranchId = new SelectList(newList, "BranchId", "BranchName", userData.BranchId);
                    }
                }
                var newNonRegList = new List<Branch>();

                // the get non registered branches details for perticular branch  from the non registeres branches list
                foreach (NonRegBranch branch in NonRegisteredBranchLists)
                {
                    if (branch.BranchId == userData.BranchId)
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

                // add banches which contain non reg branches only
                List<Branch> newBranches = new List<Branch>();
                foreach (Branch branch in RegisteredBranchLists)
                {
                    foreach (NonRegBranch nonbranch in NonRegisteredBranchLists)
                    {
                        if (branch.BranchId == nonbranch.BranchId)
                        {

                            newBranches.Add(branch);

                            break;
                        }
                    }
                }


                ViewBag.RegisteredBranchId = new SelectList(newBranches, "BranchId", "BranchName");

                if (newBranches.Count >= 1)
                {
                    ViewBag.NonRegisteredBranchId = new SelectList(NonRegisteredBranchLists, "NonRegBranchId", "CompanyNameBranchName");
                }

            }

            if (NonRegisteredBranchLists.Count == 1)
            {   
                if(userData.RoleId == 1)
                {
                    // the get registered branch detail from the company branches list
                    foreach (Branch branch in RegisteredBranchLists)
                    {
                        if (branch.BranchId == NonRegisteredBranchLists[0].BranchId)
                        {
                            var newList = new List<Branch>();
                            newList.Add(branch);
                            ViewBag.RegisteredBranchId = new SelectList(newList, "BranchId", "BranchName", userData.BranchId);
                        }
                    }
                }
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



            if (HttpContext.Request.IsAjaxRequest())
            {
                ViewBag.AjaxRequest = 1;
                return PartialView(loanSetupStep1);
            }
            else
            {

                return View(loanSetupStep1);
            }
        }


        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/27
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step4(string dashbrd)
        {
            //dashbrd = 4;
            StepAccess sa = new StepAccess();
            int stepNo = Convert.ToInt32(Session["companyStep"]);
            if (stepNo == 3)
            {

                if (sa.UpdateCompanySetupStep(userData.Company_Id, userData.BranchId, 4))
                {
                    Session["companyStep"] = 4;
                }
                stepNo = Convert.ToInt32(Session["companyStep"]);
            }
            else if ((stepNo == 0) && (dashbrd == "bshdrdhomcrpt"))
            {
                stepNo = 4;
            }
            if (stepNo >= 3)
            {
                BranchAccess ba = new BranchAccess();
               int comType = ba.getCompanyTypeByUserId(userData.UserId);
                //int comType = userData.CompanyType;
                ViewBag.ThisCompanyType = (comType == 1) ? "Dealer" : "Lender";

                //Get states to list
                CompanyAccess ca = new CompanyAccess();
                List<State> stateList = ca.GetAllStates();
                ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

                //Company regCompany = ca.GetCompanyDetailsByFirstSpUserId(userId);

                List<Company> nonRegCompanies = ca.GetCompanyByCreayedCompany(userData.Company_Id);//regCompany.CompanyId   asanka

                CompanyViewModel companyViewModel = new CompanyViewModel();
                companyViewModel.Companies = nonRegCompanies;

                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return PartialView(companyViewModel);
                }
                else
                {

                    return View(companyViewModel);
                }

            }
            return RedirectToAction("UserLogin", "Login");
            //return new HttpStatusCodeResult(404, "You are not allowed");
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
            nonRegComModel.Company.CompanyCode = companyCode;

            if (string.IsNullOrEmpty(companyCode))
            {
                GeneratesCode gc = new GeneratesCode();
                nonRegComModel.Company.CompanyCode = gc.GenerateNonRegCompanyCode(nonRegComModel.Company.CompanyName);
            }

            nonRegComModel.Company.Zip = nonRegComModel.Company.ZipPre;
            if (nonRegComModel.Company.Extension != null)
                nonRegComModel.Company.Zip += "-" + nonRegComModel.Company.Extension;

            int userId = userData.UserId;
            nonRegComModel.Company.CreatedBy = userId;
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
                //sa.updateStepNumberByUserId(userId, 5);
                sa.UpdateCompanySetupStep(userData.Company_Id, userData.BranchId, 5);
                Session["companyStep"] = 5;

                //Send company detail to step 2
                CompanyBranchModel comBranch = new CompanyBranchModel();
                comBranch.Company = nonRegCom;

                TempData["NonRegCompany"] = comBranch;
                return RedirectToAction("Step5");
            }
            ViewBag.ErrorMsg = "Failed to create " + ((CompanyType == "Lender") ? "Dealer" : "Lender") + " company.";

            //return new HttpStatusCodeResult(404, ViewBag.ErrorMsg);
            return RedirectToAction("UserLogin", "Login", new { lbl = ViewBag.ErrorMsg });
        }

        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/27
        /// Get Lender/Dealer branch details
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step5(string lbls)
        {
            int userId = userData.UserId;
            BranchAccess ba = new BranchAccess();
            int compType = ba.getCompanyTypeByUserId(userId);
            //int compType = userData.CompanyType;
            if (compType == 1)
            {
                ViewBag.ThisCompanyType = "Dealer";
            }
            else if (compType == 2)
            {
                ViewBag.ThisCompanyType = "Lender";
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

            if (stepNo < 5) return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });

            if (lbls != null && (lbls.Equals("Dealer branch is successfully created") || lbls.Equals("Lender branch is successfully created")))
            {
                ViewBag.SuccessMsg = lbls;
                //sa.updateStepNumberByUserId(userId, 4);
                //sa.UpdateCompanySetupStep(userData.Company_Id, userData.BranchId, 4);
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

            userNonRegCompany = new CompanyBranchModel();
            if ((TempData["NonRegCompany"] != null) && (TempData["NonRegCompany"].ToString() != ""))
            {


                userNonRegCompany = (CompanyBranchModel)TempData["NonRegCompany"];
                userNonRegCompany.MainBranch = new Branch();
                if (userNonRegCompany.Company.Extension == null)
                    userNonRegCompany.Company.Extension = "";
            }


            ViewBag.CurrUserRoleType = userData.RoleId;

            //Get states to list
            CompanyAccess ca = new CompanyAccess();
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(userData.Company_Id);
            ViewBag.RegBranchId = new SelectList(branchesLists, "BranchId", "BranchName");

            //Get all non reg companies
            List<Company> nonRegCompanyList = ca.GetCompanyByCreayedCompany(userData.Company_Id);
            ViewBag.NonRegCompanyId = new SelectList(nonRegCompanyList, "CompanyId", "CompanyName", 1);

            NonRegCompanyBranchModel nonRegCompanyBranch = new NonRegCompanyBranchModel();
            nonRegCompanyBranch.CompanyBranch = new CompanyBranchModel();
            nonRegCompanyBranch.CompanyBranch.Company = new Company();
            nonRegCompanyBranch.NonRegCompany= new Company();
            //Get all non registered branches by company id
            List<NonRegBranch> nonRegBranches = ba.getNonRegBranches(userData.Company_Id);
            nonRegCompanyBranch.NonRegBranches = nonRegBranches;
            if ((TempData["NonRegCompany"] != null) && (TempData["NonRegCompany"].ToString() != ""))
            {
                nonRegCompanyBranch.CompanyBranch.Company = userNonRegCompany.Company;
            }
            else {
                nonRegCompanyBranch.CompanyBranch.Company = nonRegCompanyList[0];
            }

            if (nonRegCompanyList != null)
            {
                if (nonRegCompanyList.Count() == 1)
                {
                    nonRegCompanyBranch.NonRegCompany.CompanyId = nonRegCompanyList[0].CompanyId;
                }
            }
            if (userData.RoleId != 2)
            {
                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return PartialView(nonRegCompanyBranch);
                }
                else
                {

                    return View(nonRegCompanyBranch);
                }

            }
            //Select non registered branch for admin's branch
           
            if (nonRegBranches != null) {
                var adminBonRegBranches = new List<NonRegBranch>();
                adminBonRegBranches.AddRange(nonRegBranches.Where(t => userData.BranchId == t.BranchId));
                nonRegCompanyBranch.NonRegBranches = adminBonRegBranches;
            }

            
            if (HttpContext.Request.IsAjaxRequest())
            {
                ViewBag.AjaxRequest = 1;
                return PartialView(nonRegCompanyBranch);
            }
            else
            {

                return View(nonRegCompanyBranch);
            }
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
            //if (nonRegCompanyBranch.NonRegCompanyId == 0)
            //{
            //    nonRegCompanyBranch.NonRegCompanyId = nonRegCompanyBranch.NonRegCompany.CompanyId;
            //}
            
            CompanyBranchModel nonRegBranch = nonRegCompanyBranch.CompanyBranch;

            int userId = userData.UserId;
            //int userId = 68;
            BranchAccess ba = new BranchAccess();
            CompanyAccess ca = new CompanyAccess();

            int compType = ba.getCompanyTypeByUserId(userId);
            //int compType = userData.CompanyType;

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
            if (userData.RoleId == 2)
            {
                nonRegBranch.MainBranch.BranchCreatedBy = userData.BranchId;
            }

            int reslt = ba.insertNonRegBranchDetails(nonRegBranch, userId);
            if (reslt > 0)
            {
                StepAccess sa = new StepAccess();
                bool reslt2 = false;
                if(userData.RoleId == 2)
                {
                    reslt2 = sa.UpdateLoanSetupStep(userData.UserId,userData.Company_Id, userData.BranchId, reslt, 0, 1);
                }

                else if(userData.RoleId == 1)
                {
                    reslt2 = sa.UpdateLoanSetupStep(userData.UserId,userData.Company_Id, nonRegCompanyBranch.RegBranchId, reslt, 0, 1);
                }
                if (reslt2)
                {
                    //Session["companyStep"] = 6;
                    if (compType == 1)
                    {
                        ViewBag.SuccessMsg = "Dealer branch is successfully created";
                    }
                    else if (compType == 2)
                    {
                        ViewBag.SuccessMsg = "Lender branch is successfully created";
                    }
                    loanData.stepId = 1;
                    Session["loanStep"] = loanData;

                    if((userData.step_status==1)|| (userData.step_status == 2)) {
                        return RedirectToAction("Step5", new { lbls = ViewBag.SuccessMsg });
                    }
                    else if(userData.step_status == 0) {
                        return RedirectToAction("Step6");
                    }
                    //return RedirectToAction("Step5");
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
            //return PartialView();

            if (HttpContext.Request.IsAjaxRequest())
            {
                ViewBag.AjaxRequest = 1;
                return PartialView(nonRegCompanyBranch);
            }
            else
            {

                return View(nonRegCompanyBranch);
            }

        }
        // GET: SetupProcess : As the initial Super Admin I should be able to create Super Admins, Admins, Users in the set up process.
        /// <summary>
        /// CreatedBy : Irfan MAM
        /// CreatedDate: 2016/01/26
        /// 
        /// update the loan
        /// EditedBy : Piyumi
        /// EditedDate : 2016/03/08
        /// remove getLoanIdByBranchId, getLoanIdByUserId method call
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Step6")]
        public ActionResult Step6_Post(LoanSetupStep1 loanSetupStep1)
        {
            int userId = userData.UserId;
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
            if (userData.RoleId > 2)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            if ((Session["dashboard"] !=null))
            {
                loanData.stepId = 1;
            }
            // check if   step is 6...
            StepAccess sa = new StepAccess();
            if (loanData.stepId < 1)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            LoanSetupAccess loanSetupAccess = new LoanSetupAccess();

            LoanSetupAccess la = new LoanSetupAccess();
            int loanId = loanData.loanId;

            loanData.CompanyId = userData.Company_Id;

            
            Session["loanStep"] = loanData;

            if (loanId > 0)
            {
                loanId = loanSetupAccess.insertLoanStepOne(loanSetupStep1, loanId);
            }
            else
            {
                //loanId = la.getLoanIdByUserId(userId);


                loanId = loanSetupAccess.insertLoanStepOne(loanSetupStep1, loanId);
                //need to update loanSetup object
             


            }
            if (loanId > 0)
            {
                if (loanSetupStep1.isInterestCalculate)
                {
                    
                    sa.UpdateLoanSetupStep(userData.UserId, loanData.CompanyId, loanSetupStep1.RegisteredBranchId, loanSetupStep1.nonRegisteredBranchId, loanId, 2);
                    if (loanData.stepId < 2)
                    {
                        loanData.stepId = 2;
                    }
                    //loanData.stepId = 2;
                }
                else
                {
                    sa.UpdateLoanSetupStep(userData.UserId,loanData.CompanyId, loanSetupStep1.RegisteredBranchId, loanSetupStep1.nonRegisteredBranchId, loanId, 3);
                    if (loanData.stepId < 3)
                    {
                        loanData.stepId = 3;
                    }
                }
                loanData.BranchId = loanSetupStep1.RegisteredBranchId;
                loanData.nonRegisteredBranchId = loanSetupStep1.nonRegisteredBranchId;
                loanData.loanId = loanId;
                
                Session["loanStep"] = loanData;
            }
            Session["loanStep"] = loanData;
            if (loanSetupStep1.isInterestCalculate)
            {
                return RedirectToAction("step7");
            }
            else
            {
                //sa.UpdateLoanSetupStep(loanData.CompanyId, loanData.BranchId, loanSetupStep1.nonRegisteredBranchId, loanId, 3);
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
            var dashBoardModel = new Models.DashBoard();

            //var newDashDAL = new DashBoardAccess();

            ///get level id by userid
            int userLevelId = userData.RoleId;

            dashBoardModel.userId = userData.UserId;
            dashBoardModel.userName = userData.UserName;
            dashBoardModel.roleName = (new UserManageAccess()).getUserRoleName(userData.UserId);
            dashBoardModel.levelId = userLevelId;
            dashBoardModel.step_status = userData.step_status;
            return PartialView(dashBoardModel);
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
                userId = userData.UserId;
            }
            catch (Exception)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
            }
            UserAccess ua = new UserAccess();
            //User curUser = ua.retreiveUserByUserId(userId);
            List<NonRegBranch> NonRegisteredBranchLists = (new BranchAccess()).getNonRegBranches(userData.Company_Id);
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
            int userId = userData.UserId;
            //User user = (new UserAccess()).retreiveUserByUserId(userId);
            return Json((new LoanSetupAccess()).IsUniqueLoanNumberForBranch(loanNumber, RegisteredBranchId, userData,loanData.loanId), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/2/8
        /// Get Interest Deatils If Exists
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
        // GET: Interest
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step7(int? edit)
        {
            int uId = userData.UserId;
            int branchId = loanData.BranchId;
            if (userData.RoleId >= 3)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
            }
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

            
            ViewBag.PaidDate = new SelectList(listdates, "Value", "Text");
            InterestAccess ia = new InterestAccess();
            Interest intrst = new Interest();
            //get Accrual Methods
            List<AccrualMethods> methodList = ia.GetAllAccrualMethods();
            ViewBag.AccrualMethodId = new SelectList(methodList, "MethodId", "MethodName");

            if (uId > 0)
            {
                LoanSetupAccess la = new LoanSetupAccess();
                int loanId = loanData.loanId;

                //int loanId = 1;
                if (loanId > 0)
                {
                    var intrstobj = ia.getInterestDetails(loanId);
                    if (intrstobj != null)
                    {

                        ViewBag.Edit = 1;
                        //intrst = ia.getInterestDetails(loanId);
                        //ViewBag.AccrualMethodId = new SelectList(methodList, "MethodId", "MethodName", intrstobj.AccrualMethodId);

                        if (intrstobj.option != "once a month")
                        {
                            ViewBag.Option = true;
                        }
                        else
                        {
                            ViewBag.Option = false;
                        }
                        //ViewBag.PaidDate = new SelectList(listdates, "Value", "Text", intrstobj.PaidDate);
                        //ViewBag.Email = intrst.AutoRemindEmail;

                        if (HttpContext.Request.IsAjaxRequest())
                        {
                            ViewBag.AjaxRequest = 1;
                            return PartialView(intrstobj);
                        }
                        else
                        {

                            return View(intrstobj);
                        }
                    }

                    else
                    {
                        ViewBag.Edit = 0;
                        //ViewBag.AccrualMethodId = new SelectList(methodList, "MethodId", "MethodName");
                        //ViewBag.PaidDate = new SelectList(listdates, "Value", "Text");
                        string defaultEmail = la.getAutoRemindEmailByLoanId(loanId);
                        ViewBag.Email = defaultEmail;
                        //intrst.AutoRemindEmail = defaultEmail;

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
                    //return PartialView();
                }

                else
                {
                    return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
                }
            }

            else
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
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

            int userId = userData.UserId;
            int branchId = loanData.BranchId;
            if (userData.RoleId >= 3)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
            }
            
            if (interest.option == "payoff")
            {
                interest.PaidDate = interest.option;
            }

            InterestAccess ia = new InterestAccess();
            LoanSetupAccess la = new LoanSetupAccess();
            //int loanId = la.getLoanIdByBranchId(branchId);
            int loanId = loanData.loanId;
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
                if (sa.UpdateLoanSetupStep(userData.UserId,loanData.CompanyId, loanData.BranchId, loanData.nonRegisteredBranchId, loanData.loanId, 3))
                {
                    if (loanData.stepId < 3)
                    {
                        loanData.stepId = 3;
                    }
                   
                    //loanData.stepId = 3;
                    Session["loanStep"] = loanData;
                    return RedirectToAction("Step8");

                }
                else
                {
                    return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
                }

            }
            //else if (reslt == 0)
            //{
            //    return RedirectToAction("Step8");
            //}
            else
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
            }

        }

        /// <summary>
        /// CreatedBy :kasun samarawickrama
        /// CreatedDate: 2016/02/08
        /// 
        /// loan fees section -step 8
        /// EditedBy :Piyumi
        /// EditedDate :2016/3/8
        /// 
        /// remove session variables and unwanted method calls
        /// return: step8 view
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step8()
        {

            var userId = userData.UserId;

            BranchAccess branch = new BranchAccess();
            if (userData.RoleId >= 3)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
            }
            int companyType = branch.getCompanyTypeByUserId(userId);
            //int companyType = userData.CompanyType;
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
            //fee.LoanId = loan.getLoanIdByUserId(userId);
            fee.LoanId = loanData.loanId;
            
            if (loanData.stepId > 3) {
                ViewBag.isEdit = "editable";
                var hasLoan = loan.checkLoanIsInFeesTables(fee.LoanId);
                if (hasLoan.LotInspectionAmount == 0)
                {
                    hasLoan.AdvanceId = "2";
                }
                else {
                    hasLoan.AdvanceId = "1";
                }
                if (hasLoan.MonthlyLoanAmount == 0)
                {
                    hasLoan.MonthlyLoanId = "2";
                }
                else {
                    hasLoan.MonthlyLoanId = "1";
                }
                if (hasLoan.LotInspectionAmount == 0)
                {
                    hasLoan.LotInspectionId = "2";
                }
                else {
                    hasLoan.LotInspectionId = "1";
                }

                if (hasLoan.AdvanceAmount > 0 || hasLoan.MonthlyLoanAmount > 0 || hasLoan.LotInspectionAmount > 0)
                {
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
                }

                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return PartialView(hasLoan);
                }
                else
                {

                    return View(hasLoan);
                }
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
                    if (email != null) { 
                        feeNew.AdvanceDueEmail = email;
                        feeNew.MonthlyLoanDueEmail = email;
                        feeNew.LotInspectionDueEmail = email;
                    }
                    feeNew.IsAdvanceFeeCompleteEmailReminder = false;
                    feeNew.IsLotFeeCompleteEmailReminder = false;
                    feeNew.IsLoanFeeCompleteEmailReminder = false;
                    feeNew.IsAdvanceFeeDueEmailReminder = false;
                    feeNew.IsLotFeeDueEmailReminder = false;
                    feeNew.IsLoanFeeDueEmailReminder = false;

                    if (HttpContext.Request.IsAjaxRequest())
                    {
                        ViewBag.AjaxRequest = 1;
                        return PartialView(feeNew);
                    }
                    else
                    {

                        return View(feeNew);
                    }

                }
                else
                {
                    return RedirectToAction("Step7");
                }
            }

        }
        /// <summary>
        /// CreatedBy :kasun samarawickrama
        /// CreatedDate: 2016/02/09
        /// 
        /// loan fees section step post method
        /// EditedBy :Piyumi
        /// EditedDate :2016/03/08
        /// return: step8 view 
        /// </summary>
        /// <param name="fees"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step8(Fees fees)
        {
            if (userData.RoleId >= 3)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
            }
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
            fees.LoanId = loanData.loanId;

            // after hide the due methods

            fees.MonthlyLoanDue = "Once a Month";
            fees.LotInspectionDue = "Monthly";

            if (step.InsertFeesDetails(fees))
            {
                var userId = userData.UserId;
                var branchId = loanData.loanId;

                if (fees.isEdit == true)
                {
                    return RedirectToAction("Step9");
                }
                else if (step.UpdateLoanSetupStep(userData.UserId,loanData.CompanyId, loanData.BranchId, loanData.nonRegisteredBranchId, loanData.loanId, 4))
                {
                    
                    if (loanData.stepId < 4) {
                        loanData.stepId = 4;
                    }
                    Session["loanStep"] = loanData;
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

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step9(int? edit)
        {
            int uId = userData.UserId;
            int branchId = loanData.BranchId;
            if (userData.RoleId >= 3)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
            }
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

            ViewBag.TitleAcceptMethod = new SelectList(acceptMethodsList, "Value", "Text");
            
            
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
            ViewBag.ReceivedTimeLimit = new SelectList(timeLimitList, "Value", "Text");
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
            ViewBag.ReceiptRequiredMethod = new SelectList(receiptRequiredMethodList, "Value", "Text");
            if (uId > 0)
            {
                LoanSetupAccess la = new LoanSetupAccess();
                TitleAccess ta = new TitleAccess();
                Title title = new Title();
                //int loanId = la.getLoanIdByBranchId(branchId);

                int loanId = loanData.loanId;
                if (loanId > 0)
                {
                    
                    if (loanData.stepId>4)
                    {
                        var titleObj = ta.getTitleDetails(loanId);
                        ViewBag.Edit = 1;
                        if (titleObj != null)
                        {
                            
                            //title = ta.getTitleDetails(loanId);
                            //ViewBag.TitleAcceptMethod = new SelectList(acceptMethodsList, "Value", "Text", titleObj.TitleAcceptMethod);
                            //ViewBag.ReceivedTimeLimit = new SelectList(timeLimitList, "Value", "Text", titleObj.ReceivedTimeLimit);
                            //ViewBag.ReceiptRequiredMethod = new SelectList(receiptRequiredMethodList, "Value", "Text", titleObj.ReceiptRequiredMethod);
                            ViewBag.DefaultEmail = titleObj.RemindEmail;
                        }
                        else if(titleObj == null)
                        {
                            return RedirectToAction("UserLogin", "Login");
                        }

                        if (HttpContext.Request.IsAjaxRequest())
                        {
                            ViewBag.AjaxRequest = 1;
                            return PartialView(titleObj);
                        }
                        else
                        {

                            return View(titleObj);
                        }

                    }

                    else
                    {
                        ViewBag.Edit = 0;
                        //ViewBag.TitleAcceptMethod = new SelectList(acceptMethodsList, "Value", "Text");
                        //ViewBag.ReceivedTimeLimit = new SelectList(timeLimitList, "Value", "Text");
                        //ViewBag.ReceiptRequiredMethod = new SelectList(receiptRequiredMethodList, "Value", "Text");
                        string defaultEmail = la.getAutoRemindEmailByLoanId(loanId);

                        ViewBag.Email = defaultEmail;
                        //title.LoanId = loanId;

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
                    //return PartialView(title);
                }

                else
                {
                    return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
                }
            }
            else
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
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
            int userId = userData.UserId;
            int branchId = loanData.BranchId;
            if (userData.RoleId >= 3)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
            }
            //int loanId = 1;


            TitleAccess ta = new TitleAccess();
            LoanSetupAccess la = new LoanSetupAccess();
            StepAccess sa = new StepAccess();
            //int loanId = la.getLoanIdByBranchId(branchId);
            int loanId = loanData.loanId;
            title.LoanId = loanId;

            //if (title.IsReceipRequired || title.IsTitleTrack)
            //{
            int reslt = ta.insertTitleDetails(title);
            if (reslt >= 0)
            {

                if (sa.UpdateLoanSetupStep(userData.UserId,loanData.CompanyId, loanData.BranchId, loanData.nonRegisteredBranchId, loanData.loanId, 5))
                {
                    if (loanData.stepId < 5)
                    {
                        loanData.stepId = 5;
                    }
                    
                    Session["loanStep"] = loanData;
                    return RedirectToAction("Step10");
                }
                else
                {
                    return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
                }
            }
            //    else if (reslt == 0)
            //    {
            //        return RedirectToAction("Step10");
            //}
            else
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
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

        // GET: LoanSetUpStep5
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step10(string lbl)
        {
            int userId = userData.UserId;
            if (userData.RoleId >= 3)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
            }

            //check user step is valid for this step
            StepAccess sa = new StepAccess();
            if (loanData.stepId == 5)
            {
                if (lbl == "Details added successfully")
                {
                    ViewBag.SuccessMsg = "Loan setup is completed";
                    Session["loanStep"] = null;
                    if (HttpContext.Request.IsAjaxRequest())
                    {
                        ViewBag.AjaxRequest = 1;
                        return PartialView();
                    }
                    return View();
                }

                int branchId = loanData.BranchId;

                LoanSetupAccess la = new LoanSetupAccess();
                //int loanId = la.getLoanIdByBranchId(branchId);
                int loanId = loanData.loanId;
                CurtailmentAccess curAccess = new CurtailmentAccess();
                _loan = curAccess.GetLoanDetailsByLoanId(loanId);
                _loan.loanId = loanId;

                //_gCurtailment = new CurtailmentModel();
                _gCurtailment = new CurtailmentModel();
                _gCurtailment.AdvancePt = _loan.advancePercentage;
                _gCurtailment.RemainingPercentage = _gCurtailment.AdvancePt;

                _gCurtailment.RemainingTime = _loan.payOffPeriod;

                _gCurtailment.TimeBase = "Months";
                if (_loan.payOffPeriodType == 0) _gCurtailment.TimeBase = "Days";

                _gCurtailment.InfoModel = new List<Curtailment>();

                var curtailments = curAccess.retreiveCurtailmentByLoanId(loanId);

                int payPercentage = _loan.advancePercentage;
                int? totalPercentage = 0;

                int curId = 0;
                if (curtailments != null && curtailments.Count > 0)
                {
                    for (int i = 0; i < curtailments.Count; i++)
                    {
                        curId++;
                        totalPercentage += curtailments[i].Percentage;
                        _gCurtailment.InfoModel.Add(new Curtailment { CurtailmentId = curId, TimePeriod = curtailments[i].TimePeriod, Percentage = curtailments[i].Percentage });
                    }
                    _gCurtailment.LoanStatus = _loan.LoanStatus ? "Yes" : "No";

                    _gCurtailment.CalculationBase = _loan.CurtailmentCalculationBase == "a" ? "Advance" : "Full payment";
                    _gCurtailment.DueDate = _loan.CurtailmentDueDate;
                    _gCurtailment.AutoRemindEmail = _loan.CurtailmentAutoRemindEmail;
                    _gCurtailment.EmailRemindPeriod = _loan.CurtailmentEmailRemindPeriod;
                }

                _calMode = "Full Payment";
                ViewBag.CalMode = _calMode;
                _gCurtailment.RemainingPercentage = payPercentage - totalPercentage;

                if (_gCurtailment.RemainingPercentage > 0)
                    _gCurtailment.InfoModel.Add(new Curtailment { CurtailmentId = curId + 1 });
                ViewData["objmodel"] = _gCurtailment;

                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return PartialView(_gCurtailment);
                }
                else
                {

                    return View(_gCurtailment);
                }
            }
            return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 03/10/2016
        /// </summary>
        /// 
        /// Save curtailment data
        /// 
        /// <param name="curtailmentList"></param>
        /// <param name="curtaiulmentModel"></param>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult AddCurtailment(List<Curtailment> curtailmentList, CurtailmentModel curtaiulmentModel)
        {
            //calculate payment percentage
            if (userData.RoleId >= 3)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
            }
            int index = 0;
            foreach (Curtailment curtailment in curtailmentList)
            {
                curtailmentList[index].PaymentPercentage = Convert.ToDecimal((curtailment.Percentage*100)/_gCurtailment.AdvancePt);
                index++;
            }

            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
            StepAccess sa = new StepAccess();
            if (curtailmentAccess.InsertCurtailment(curtailmentList, loanData.loanId) == 1)
            {
                ViewBag.SuccessMsg = "Curtailment Details added successfully";
                sa.UpdateLoanSetupStep(userData.UserId,loanData.CompanyId, loanData.BranchId, loanData.nonRegisteredBranchId,
                    loanData.loanId, 6);
                ViewBag.Redirect = 1;
            }
            else
            {
                sa.UpdateLoanSetupStep(userData.UserId,loanData.CompanyId, loanData.BranchId, loanData.nonRegisteredBranchId,
                    loanData.loanId, 6);
                ViewBag.SuccessMsg = "Curtailment Details updated successfully";
            }

            //bool loanActive = curtaiulmentModel.LoanStatus == "Yes";

            LoanSetupAccess loanAccess = new LoanSetupAccess();
            loanAccess.UpdateLoanCurtailment(curtaiulmentModel, loanData.loanId);

            return RedirectToAction("Step10", new { lbl = "Details added successfully" });
        }
        
    }
}

