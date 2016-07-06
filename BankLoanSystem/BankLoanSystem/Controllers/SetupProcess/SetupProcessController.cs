using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Data;
using System.Web.UI;
using Microsoft.Reporting.WebForms;
using BankLoanSystem.Reports;

namespace BankLoanSystem.Controllers.SetupProcess
{
    public class SetupProcessController : Controller
    {
        User userData = new User();
        LoanSetupStep loanData = new LoanSetupStep();
        int loanstep = 0;


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
                        else if (loanData.loanId == 0)
                        {
                            loanstep = 1;
                            //CurtailmentAccess curtailmentAccess = new CurtailmentAccess();


                            // _loan = curtailmentAccess.GetLoanDetailsByLoanId(loanData.loanId);

                            // Session["isInterest"] = _loan.isInterestCalculate;
                        }
                    }
                }
                else
                {
                    if (HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new HttpStatusCodeResult(404, "Due to inactivity your session has timed out, please log in again.");
                    }
                    else
                    {

                        filterContext.Result = new RedirectResult("/Login/UserLogin?lbl=Due to inactivity your session has timed out, please log in again.");
                    }
                }
            }
            catch
            {
                filterContext.Result = new RedirectResult("~/Exceptions/Index");
            }
        }



        /*

    Frontend page: Dashboard page
    Title: Redirect to loan set up page
    Designed: Irfan Mam
    User story: DFP-484
    Developed: Irfan MAM
    Date created: 7/2/2016

*/


        public ActionResult RedirectToStep(string loanCode, int stepNo)
        {

            // super admin and admin only accesible
            if (userData.RoleId == 1 || userData.RoleId == 2)
            {

                // if no loan code selected and if there no step number -- wrong access
                if ((loanCode == null || loanCode == "") && stepNo < 1)
                {
                    return RedirectToAction("UserLogin", "Login", new { lbl = "Access Denied" });
                }

                // if there is no loan code and step equal to 1 --> redirect to loan create page
                else if ((loanCode == null || loanCode == "") && stepNo == 1)
                {
                    Session["dashboard"] = true;
                    
                    return RedirectToAction("Step6", "SetupProcess");
                }
                // if there is a loan code and step number
                else if(stepNo >= 1 && loanCode != null && loanCode != "")
                {
                    LoanSetupStep loanStep = new LoanSetupStep();
                    LoanSetupStep1 loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode); // take the loan details
                    
                    // step 2 is interest page..
                    if(stepNo == 2)
                    {
                        stepNo = 3;
                    }

                    // if there is a loan to loan code, assign all loan data which need to setup process 
                    if (loanDetails != null)
                    {
                        loanStep.CompanyId = userData.Company_Id;
                        loanStep.BranchId = loanDetails.RegisteredBranchId;
                        loanStep.stepId = stepNo;
                        loanStep.nonRegisteredBranchId = loanDetails.nonRegisteredBranchId;
                        loanStep.loanId = loanDetails.loanId;
                        
                        
                        Session["loanStep"] = loanStep;
                        Session["dashboard"] = true;
                        return RedirectToAction("Step" + (loanStep.stepId + 5), "SetupProcess");
                        
        }
                }
                
            }
          // redirect to login -> if unautorized access
                return RedirectToAction("UserLogin", "Login", new { lbl = "Access Denied" });

            
        }


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
            //convert Session["companyStep"] to int value
            int stepNo = Convert.ToInt32(Session["companyStep"]);
            //assign logged user id to variable
            int userId = userData.UserId;
            //add step no and loan step(loan setup step 1 is considerd as step 6)
            stepNo = stepNo + loanstep;

            ViewBag.Step = stepNo;
            //assign step number to session variable
            Session["stepNo"] = stepNo;
            //Get company details if branch same as company
            //check step number
            //stepNo 2 - Branch setup
            if (stepNo == 2)
            {
                CompanyAccess ca = new CompanyAccess();
                Company company = ca.GetCompanyDetailsCompanyId(userData.Company_Id);

                //return View();
                return RedirectToAction("Step2");
            }
            //stepNo 5 - Partner Branch setup
            else if (stepNo == 5)
            {
                CompanyAccess ca = new CompanyAccess();
                //get partner company details
                Company nonRegCompany = ca.GetNonRegCompanyDetailsByRegCompanyId(userData.Company_Id);
                //if no partcompanies return to step 4 (Partner Company Setup)
                if (string.IsNullOrEmpty(nonRegCompany.CompanyName)) return RedirectToAction("Step4", "SetupProcess");

                CompanyBranchModel comBranch = new CompanyBranchModel();
                comBranch.Company = nonRegCompany;
                //assign returned partner company object to TempData object
                TempData["NonRegCompany"] = comBranch;

                //return View();
                return RedirectToAction("Step5");
            }
            //check stepNo is 0 and return to login page
            else if (stepNo == 0)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "Company Setup is on going Please Contact Admin" });
            }
            //return to appropriate step
            else
            {
                return RedirectToAction("Step" + stepNo);
            }
        }


        /// <summary>
        /// Frontend Page: Step 1 - Company Setup at setup process
        /// Title: get details to company setup page
        /// Designed: Kanishka SHM
        /// User story: As the initial super admin I should able to create company in the setup procces
        /// Developed: Kanishka SHM
        /// Date created: 2016/01/26
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step1()
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
            //check company step is greate than or equal to 1
            if (Convert.ToInt32(Session["companyStep"]) >= 1)
            {
                //get company details
                Company preCompany = ca.GetCompanyDetailsCompanyId(userData.Company_Id);
               
                //check ajax request
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
            //if company step session is null return to login page with error message
            return new HttpStatusCodeResult(404, "Due to inactivity your session has timed out, please log in again.");
           
        }

        /// <summary>
        /// Frontend Page: Step 1 - Company Setup at setup process
        /// Title: insert company
        /// Designed: Kanishka SHM
        /// User story: 
        /// Developed: Kanishka SHM
        /// Date created: 2016/01/26
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step1(Company company)
        {
            string type;
            //check session company step is null
            if (Session["companyStep"] == null)
            {
                //check ajax request
                if (HttpContext.Request.IsAjaxRequest())
                {
                    //return to login page with error message
                    return new HttpStatusCodeResult(404, "Due to inactivity your session has timed out, please log in again.");
                }
                else
                {
                    //return to login page with error message
                    return RedirectToAction("UserLogin", "Login");
                }
            }
            type = "UPDATE";
            //check company code is null
            if (string.IsNullOrEmpty(company.CompanyCode))
            {
              //assign type as insert 
                type = "INSERT";
            }
            //set zip code
            company.Zip = company.ZipPre;
            if (company.Extension != null)
                company.Zip += "-" + company.Extension;
            //assign looged user id to created by property
            company.CreatedBy = company.FirstSuperAdminId = userData.UserId;
            //assign true for company status
            company.CompanyStatus = true;
            CompanyAccess ca = new CompanyAccess();
            //insert company details and return inserted company id
            int companyId = ca.InsertCompany(company, type);
            //check company id is not 0
            if (companyId > 0)
            {
                //assign success message
                ViewBag.SuccessMsg = "Company Successfully setup.";
                //assign company code and company type to user data object company code and company type
                userData.CompanyCode = company.CompanyCode;
                userData.CompanyType = company.TypeId;

                //If succeed update step table to step2 
                StepAccess sa = new StepAccess();
                if (type == "INSERT")
                {
                    bool res = sa.UpdateCompanySetupStep(companyId, userData.BranchId, 2);

                    //insert to log 
                    Log log = new Log(userData.UserId, companyId, 0, 0, "Company Step", "Inserted company : " + company.CompanyCode, DateTime.Now);

                    (new LogAccess()).InsertLog(log);
                }
                else if (type == "UPDATE")
                {
                    //insert to log 
                    Log log = new Log(userData.UserId, companyId, 0, 0, "Company Step", "Updated company : " + company.CompanyCode, DateTime.Now);

                    (new LogAccess()).InsertLog(log);
                }
                //check company step is 1
                if (Convert.ToInt32(Session["companyStep"].ToString()) < 2)
                {
                    //update company step to 2
                    Session["companyStep"] = 2;
                }


                //user object pass to session
                userData.Company_Id = companyId;
                userData.CompanyName = company.CompanyName;
                Session["AuthenticatedUser"] = userData;


                //Send company detail to step 2
                CompanyBranchModel comBranch = new CompanyBranchModel();
                comBranch.Company = company;

                TempData["Company"] = comBranch;
                return RedirectToAction("Step2");
            }

            //return RedirectToAction("UserLogin", "Login", new { lbl = "Failed to Setup company." });
            return new HttpStatusCodeResult(404, "Failed to Setup company.");
        }

        /// <summary>
        /// Frontend Page: Step 2 - Branch Setup at setup process
        /// Title: retrieve content of branch setup page
        /// Designed: Piyumi Perera
        /// User story: 
        /// Developed: Piyumi Perera
        /// Date created: 2016/01/26
        /// Edited: Kanishka SHM
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step2(string edit1,int? edit)
        {
          
            int userId = userData.UserId;
            int roleId = userData.RoleId;
            // check he is a super admin or admin

            if (roleId != 1)
            {
                //if not a super admin return to login page
                return RedirectToAction("UserLogin", "Login");
            }
            //check insert or update result of branch setup is >0
            if (TempData["Step2Reslt"] != null && int.Parse(TempData["Step2Reslt"].ToString())>0)
            {
                ViewBag.SuccessMsg = "Branch Successfully Created";
            }
            //check insert or update result of branch setup is 0
            else if (TempData["Step2Reslt"] != null && int.Parse(TempData["Step2Reslt"].ToString()) == 0)
            {
                ViewBag.SuccessMsg = "Branch Successfully Updated";
            }
            else
            {
                ViewBag.ErrorMsg = "Failed to Create Branch";
            }
            //convert Session["companyStep"] to integer     
                int reslt = Convert.ToInt32(Session["companyStep"]);
           //check step is greater  than or equal to 2
            if (reslt >= 2)
            {
                CompanyBranchModel userCompany = new CompanyBranchModel();
                //check TempData["Company"] is not null and not empty
                if ((TempData["Company"] != null) && (TempData["Company"].ToString() != ""))
                {
                    //convert TempData["Company"] to object
                    userCompany = (CompanyBranchModel)TempData["Company"];
                    //check zip extension is null
                    if (userCompany.Company.Extension == null)
                        //assign empty string to extension
                        userCompany.Company.Extension = "";
                }

                userCompany.MainBranch = new Branch();
                ViewBag.BranchIndex = 0;

                //Get company details by company id
                CompanyAccess ca = new CompanyAccess();
                Company preCompany = ca.GetCompanyDetailsCompanyId(userData.Company_Id);


                userCompany.Company = preCompany;

                BranchAccess ba = new BranchAccess();
                IList<Branch> branches = ba.getBranchesByCompanyCode(preCompany.CompanyCode);
                userCompany.SubBranches = branches;

                //Get states to list
                List<State> stateList = ca.GetAllStates();
                ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");
                //check ajax request
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
                //if company step is less than 2 return to login page with error message
                return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
            }
        }

        //Post Branch
        /// <summary>
        /// Frontend Page: Step 2 - Branch Setup at setup process
        /// Title: insert branch details
        /// Designed: Piyumi Perera
        /// User story: 
        /// Developed: Piyumi Perera
        /// Date created: 2016/01/26
        /// Edited: Kanishka SHM
        /// </summary>
        /// <param name="userCompany2"></param>
        /// <param name="branchCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step2(CompanyBranchModel userCompany2, string branchCode)
        {
            //assign logged user's user id to variable
            int userId = userData.UserId;
            //check Session["companyStep"] is null
            if (Session["companyStep"] == null)
            {
                //check ajax request
                if (HttpContext.Request.IsAjaxRequest())
                {
                    //return to login page with error message
                    return new HttpStatusCodeResult(404, "Due to inactivity your session has timed out, please log in again.");
                }
                else
                {
                    //return to login page with error message
                    return RedirectToAction("UserLogin", "Login");
                }
            }
            //assign selected state id to branch object property
            userCompany2.MainBranch.StateId = userCompany2.StateId;
            //assign branch code to branch object property
            userCompany2.MainBranch.BranchCode = branchCode;

            BranchAccess ba = new BranchAccess();

            userCompany2.Company = new Company();
            //check company code of userdata object is not null
            if (!string.IsNullOrEmpty(userData.CompanyCode))
            {
                //assign company code of userdata to company object company code
                userCompany2.Company.CompanyCode = userData.CompanyCode;
            }
            else
            {
                //get company details
                Company cmp = new Company();
                cmp = (new CompanyAccess()).GetCompanyDetailsCompanyId(userData.Company_Id);
                //assign retrieved company code to company object company code
                userCompany2.Company.CompanyCode = cmp.CompanyCode ;
            }
            //insert branch details
            int reslt = ba.insertFirstBranchDetails(userCompany2, userId);
            //check inserted or updated result is not 0
            if (reslt >= 0)
            {
                //assign result to a TempData object
                TempData["Step2Reslt"] = reslt;
                //check current value of company setup is less than 3
                if(Convert.ToInt32(Session["companyStep"].ToString()) < 3){ 
                    //assign 3 for Session["companyStep"]
                Session["companyStep"] = 3;
                }

                //user object pass to session
                if (userData.BranchId == 0)
                {
                    userData.BranchId = reslt;
                }
                
                Session["AuthenticatedUser"] = userData;

                StepAccess sa = new StepAccess();
                //update company setup step table check result
                if (sa.UpdateCompanySetupStep(userData.Company_Id, reslt, 3))
                {
                    //return to branch setup page
                    return RedirectToAction("Step2");

                }
            }
            else
            {
                //if update or insert result is less than to 0 assign 0 to TempData object
                TempData["Step2Reslt"] = 0;
                return RedirectToAction("UserLogin", "Login", new { lbl = "Failed to set up branch" });
                
            }

            ViewBag.BranchIndex = 0;

            //Get company details by user id
            userId = userData.UserId;

            // need common method for that - asanka

            CompanyAccess ca = new CompanyAccess();
            Company preCompany = ca.GetCompanyDetailsCompanyId(userData.Company_Id);

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


      /*

       Frontend page: Create User page on setup process
       Title: creating users on the setup process -- step3
       Designed: Irfan Mam
       User story:  As the initial Super Admin I should be able to create Super Admins, Admins in the set up process.
       Developed: Irfan MAM
       Date created: 1/26/2016

       */
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step3(string lbls)
        {

            // if there is no session exist - redirect to login -- wrong access
            if (Session["companyStep"] == null)
            {
                if (HttpContext.Request.IsAjaxRequest())
                {

                    return new HttpStatusCodeResult(404, "Due to inactivity your session has timed out, please log in again.");
                }
                else
                {

                    return RedirectToAction("UserLogin", "Login");
                }
            }

            
            int userId = userData.UserId; // current user id
            StepAccess sa = new StepAccess();

           

            int roleId = userData.RoleId; // current user's role

            // if he is not a super admin or admin , not allowed -- wrong access
            if (roleId > 2)
            {
                return RedirectToAction("UserLogin", "Login");
            }


            // check if the user completed the step 1 and 2, if not redirect to login -- wrong access
            if (Convert.ToInt32(Session["companyStep"]) < 3)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            // after user created 
            // if user scussefully created
            if (lbls != null && lbls.Equals("User Successfully Created"))
            {

                // pass the sucessfull message to view
                ViewBag.SuccessMsg = "User Successfully Created";
               
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

                if (Convert.ToInt32(Session["companyStep"].ToString()) < 4)
                {
                    Session["companyStep"] = 4;
                }

                

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

            // if error occurs while creating the user
            else if (lbls != null && lbls.Equals("Failed to create user!"))
            {


                ViewBag.ErrorMsg = "Failed to create user";

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
                return new HttpStatusCodeResult(404,"You are not allowed");
            }

            // check if   step is 3...
            if (Convert.ToInt32(Session["companyStep"]) < 3)
            {
                return new HttpStatusCodeResult(404, "You are not allowed");
            }

            user.CreatedBy = currentUser;
            user.IsDelete = false;
           // user.Status = false;

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

            if (res > 0)
            {
                //insert to log 
                Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId,0, "Create User in Company setup", "created "+(user.RoleId == 1 ? "Super Admin" : "Admin") + ", Username : " + user.UserName, DateTime.Now);

                (new LogAccess()).InsertLog(log);

                if (user.Status)
                {
                    string body = "Hi " + user.FirstName + "! <br /><br /> Your account has been successfully created. Below in your account detail." +
                             "<br /><br /> User name: " + user.UserName +
                                   "<br /> Password : <b>" + passwordTemp +
                             //"<br />Click <a href='http://localhost:57318/CreateUser/ConfirmAccount?userId=" + userId + "&activationCode=" + activationCode + "'>here</a> to activate your account." +
                             "<br /><br/> Thanks,<br /> Admin.";

                    Email email = new Email(user.Email);
                    email.SendMail(body, "Account details");
                }

                Session["abcRol"] = user.RoleId;
                Session["abcBrnc"] = user.BranchId;
                



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



               // User curUser = ua.retreiveUserByUserId(userId);
                // get all branches
                List<Branch> branchesLists = (new BranchAccess()).getBranches(userData.Company_Id);
                ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");


                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return RedirectToAction("Step3", new { lbls = ViewBag.ErrorMsg });
                }
                else
                {

                    return RedirectToAction("Step3", new { lbls = ViewBag.ErrorMsg });
                }
            }
        }



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

            if(TempData["error"] != null && TempData["error"].ToString() == "error")
            {
                ViewBag.Error = "Failed to create loan";
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
        /// Frontent page: partner company setup in setup process
        /// Title:create view 
        /// Designed: Kanishka SHM
        /// User story:
        /// Developed : Kanishka SHM
        /// Date created: 2016/01/27
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step4()
        {
           
            StepAccess sa = new StepAccess();
            //convert session to integer
            int stepNo = Convert.ToInt32(Session["companyStep"]);
            //check company step is 3
            if (stepNo == 3)
            {
                //update company set up step to 4
                if (sa.UpdateCompanySetupStep(userData.Company_Id, userData.BranchId, 4))
                {
                    //check Session["companyStep"] value is less than 4 
                    if (Convert.ToInt32(Session["companyStep"].ToString()) < 4)
                    {
                        //update Session["companyStep"] to 4
                        Session["companyStep"] = 4;
                    }

                    
                }
                stepNo = Convert.ToInt32(Session["companyStep"]);
            }
            
            //check company step is equal or greater than 3
            if (stepNo >= 3)
            {
                BranchAccess ba = new BranchAccess();
                //get company type
               int comType = ba.getCompanyTypeByUserId(userData.UserId);
                //set partner company type
                //company type 1-lender,company type 2 - dealer
                ViewBag.ThisCompanyType = (comType == 1) ? "Dealer" : "Lender";

                //Get states to list
                CompanyAccess ca = new CompanyAccess();
                List<State> stateList = ca.GetAllStates();
                ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");
                //get partner companies
                List<Company> nonRegCompanies = ca.GetCompanyByCreayedCompany(userData.Company_Id);//regCompany.CompanyId   asanka

                CompanyViewModel companyViewModel = new CompanyViewModel();
                companyViewModel.Companies = nonRegCompanies;
                //check ajax request
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
            //is company step is less than 3 return to login page
            return RedirectToAction("UserLogin", "Login");
          
        }

        /// <summary>
        /// Frontend page: create partner company
        /// Title: insert partner company
        /// Designed:Kanishka SHM
        /// User story:
        /// Developed : Kanishka SHM
        /// Date created: 2016/01/27
        /// </summary>
        /// <param name="nonRegComModel"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step4(CompanyViewModel nonRegComModel, string companyCode)
        {
            //assign company code to object
            nonRegComModel.Company.CompanyCode = companyCode;
            //check company step is null
            if (Session["companyStep"] == null)
            {
                //check ajax request
                if (HttpContext.Request.IsAjaxRequest())
                {
                    //return to login with error code 404
                    return new HttpStatusCodeResult(404, "Due to inactivity your session has timed out, please log in again.");
                }
                else
                {
                    //return to login page
                    return RedirectToAction("UserLogin", "Login");
                }
            }

            //assign companay zip with extension
            nonRegComModel.Company.Zip = nonRegComModel.Company.ZipPre;
            if (nonRegComModel.Company.Extension != null)
                nonRegComModel.Company.Zip += "-" + nonRegComModel.Company.Extension;
            //assign created by,company type and state id
            int userId = userData.UserId;
            nonRegComModel.Company.CreatedBy = userId;
            nonRegComModel.Company.TypeId = (userData.CompanyType == 1) ? 2:1;
            nonRegComModel.Company.StateId = nonRegComModel.StateId;

            CompanyAccess ca = new CompanyAccess();

            nonRegComModel.Company.CreatedByCompany = userData.Company_Id; //regCompany.CompanyId;  asanka

            Company nonRegCom = nonRegComModel.Company;
            //check result of insert or update partner company 
            if (ca.InsertNonRegisteredCompany(nonRegCom))
            {
                //assign success msg according to company type
                ViewBag.SuccessMsg = ((userData.CompanyType == 1) ? "Dealer" : "Lender") + " Successfully created.";

                //If succeed update step table to step2 
                StepAccess sa = new StepAccess();
                //sa.updateStepNumberByUserId(userId, 5);
                sa.UpdateCompanySetupStep(userData.Company_Id, userData.BranchId, 5);

                

                if (Convert.ToInt32(Session["companyStep"].ToString()) < 5)
                {
                    Session["companyStep"] = 5;
                }

                

                //Send company detail to step 2
                CompanyBranchModel comBranch = new CompanyBranchModel();
                comBranch.Company = nonRegCom;

                TempData["NonRegCompany"] = comBranch;
                return RedirectToAction("Step5");
            }
            ViewBag.ErrorMsg = "Failed to create " + ((userData.CompanyType == 1) ? "Dealer" : "Lender") + " company.";

            //return new HttpStatusCodeResult(404, ViewBag.ErrorMsg);
            return RedirectToAction("UserLogin", "Login", new { lbl = ViewBag.ErrorMsg });
        }

        /// <summary>
        /// Frontend page: create partner branch in setup process
        /// Title: Get Lender/Dealer branch details
        /// Designed: Piyumi Perera
        /// User story:
        /// Developed:Piyumi p
        /// Date created:2016/1/27
        /// Edited: Kanishka SHM
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step5(string lbls)
         {
            int userId = userData.UserId;
            BranchAccess ba = new BranchAccess();
            //get company type
            int compType = ba.getCompanyTypeByUserId(userId);
            //if lender company
            if (compType == 1)
            {
                ViewBag.ThisCompanyType = "Dealer";
            }
            //if dealer company
            else if (compType == 2)
            {
                ViewBag.ThisCompanyType = "Lender";
            }
            else
            {
                ViewBag.compType = "";
            }
            StepAccess cs = new StepAccess();
            //convert session to integer
            int stepNo = Convert.ToInt32(Session["companyStep"]);
            //check step is less than 0
            if (stepNo < 0)
            {
                stepNo = Convert.ToInt32(Session["companyStep"]);
            }
            //check step is less than 5 and return to login page
            if (stepNo < 5) return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
            //check result of insert or update
            if (lbls != null && (lbls.Equals("Dealer branch is successfully created") || lbls.Equals("Lender branch is successfully created")))
            {
                ViewBag.SuccessMsg = lbls;
               //check ajax request
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

            CompanyBranchModel userNonRegCompany = new CompanyBranchModel();
            //check TempData["NonRegCompany"] (partner company ) is not null and empty
            if ((TempData["NonRegCompany"] != null) && (TempData["NonRegCompany"].ToString() != ""))
            {
                //convert to model object 
                userNonRegCompany = (CompanyBranchModel)TempData["NonRegCompany"];
                userNonRegCompany.MainBranch = new Branch();
                //check extension is null
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
            //check partner company list is not null
            if (nonRegCompanyList != null)
            {
                //check if one partner company and assign that company id
                if (nonRegCompanyList.Count() == 1)
                {
                    nonRegCompanyBranch.NonRegCompany.CompanyId = nonRegCompanyList[0].CompanyId;
                }
            }
            //check user is not admin
            if (userData.RoleId != 2)
            {
                //check ajax request
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

            //check ajax request
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
        /// Frontend page: create partner company
        /// Title: Insert Non registered branch details
        /// Designed: Piyumi Perera
        /// User story:
        /// Developed: Piyumi p
        /// Date created:2016/1/27
        /// Edited: Kanishka SHM
        /// </summary>
        /// <param name="nonRegCompanyBranch"></param>
        /// <param name="branchCode"></param>
        /// <returns></returns>
        [HttpPost]
        
        public ActionResult Step5(NonRegCompanyBranchModel nonRegCompanyBranch, string branchCode)
        {
            
            CompanyBranchModel nonRegBranch = nonRegCompanyBranch.CompanyBranch;

            int userId = userData.UserId;
           
            BranchAccess ba = new BranchAccess();
            CompanyAccess ca = new CompanyAccess();

            int compType = ba.getCompanyTypeByUserId(userId);

            nonRegBranch.MainBranch.StateId = nonRegCompanyBranch.StateId;

            nonRegBranch.MainBranch.BranchCode = branchCode;
            Company company = new Company();
            //check partner branch code is null
            if (string.IsNullOrEmpty(branchCode))
            {
                //get partner company details
               company = ca.GetNonRegCompanyByCompanyId(nonRegCompanyBranch.NonRegCompanyId);
               
            }

            nonRegBranch.MainBranch = nonRegBranch.MainBranch;

            //Get created branch id
            UserManageAccess uma = new UserManageAccess();
            
            nonRegBranch.MainBranch.BranchCreatedBy = nonRegCompanyBranch.RegBranchId;
            nonRegBranch.MainBranch.BranchCompany = nonRegCompanyBranch.NonRegCompanyId;

            //Set admin branch to new user 
            if (userData.RoleId == 2)
            {
                nonRegBranch.MainBranch.BranchCreatedBy = userData.BranchId;
            }
            //insert or update partner branch details
            int reslt = ba.insertNonRegBranchDetails(nonRegBranch, userId, company.CompanyCode);
            //check result is greater than 0
            if (reslt > 0)
            {
                StepAccess sa = new StepAccess();
                bool reslt2 = false;
                //check user is admin
                if(userData.RoleId == 2)
                {
                    //update company setup and insert a record to loan setup step
                    reslt2 = sa.UpdateLoanSetupStep(userData.UserId,userData.Company_Id, userData.BranchId, reslt, 0, 1);
                }
                //check user is super admin
                else if(userData.RoleId == 1)
                {
                    //update company setup and insert a record to loan setup step
                    reslt2 = sa.UpdateLoanSetupStep(userData.UserId,userData.Company_Id, nonRegCompanyBranch.RegBranchId, reslt, 0, 1);
                }
                //check update result
                if (reslt2)
                {
                   //if lender company
                    if (compType == 1)
                    {
                        ViewBag.SuccessMsg = "Dealer branch is successfully created";
                    }
                    //if dealer company
                    else if (compType == 2)
                    {
                        ViewBag.SuccessMsg = "Lender branch is successfully created";
                    }
                    //----------------
                    //check loan step is less than 1
                    if(loanData.stepId<1)
                    {
                        //update loan step and assign to session
                        loanData.stepId = 1;
                        Session["loanStep"] = loanData;
                    }
                   
                    //------------------------
                    //check user's step status is 1 or 2
                    if((userData.step_status==1)|| (userData.step_status == 2)) {
                        //return to step5
                        return RedirectToAction("Step5", new { lbls = ViewBag.SuccessMsg });
                    }
                    //check user's step status is 0
                    else if (userData.step_status == 0) {
                        //return loan step 1
                        return RedirectToAction("Step6");
                    }
                   
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
            //check ajax request
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
                return new HttpStatusCodeResult(404, "Not Allowed");
            }

            if ((Session["dashboard"] !=null))
            {
                loanData.stepId = 1;
            }
            // check if   step is 6...
            StepAccess sa = new StepAccess();
            if (loanData.stepId < 1)
            {
                return new HttpStatusCodeResult(404, "Not Allowed");
            }

            LoanSetupAccess loanSetupAccess = new LoanSetupAccess();

            LoanSetupAccess la = new LoanSetupAccess();
            int loanId = loanData.loanId;

            loanData.CompanyId = userData.Company_Id;

            
            Session["loanStep"] = loanData;

            if (loanId > 0)
            {
                loanId = loanSetupAccess.insertLoanStepOne(loanSetupStep1, loanId);
                if (loanId > 0)
                {
                    Log log = new Log(userData.UserId, userData.Company_Id, loanSetupStep1.RegisteredBranchId, loanId, "Loan Details", "Edited Loan : " + loanId, DateTime.Now);

                    int islog = (new LogAccess()).InsertLog(log);
                }
                else
                {
                    TempData["error"] = "error";
                    return RedirectToAction("step6");
                }
              
            }
            else
            {
                //loanId = la.getLoanIdByUserId(userId);


                loanId = loanSetupAccess.insertLoanStepOne(loanSetupStep1, loanId);
                if (loanId > 0)
                {
                    Log log = new Log(userData.UserId, userData.Company_Id, loanSetupStep1.RegisteredBranchId, loanId, "Loan Details", "Inserted Loan : " + loanId, DateTime.Now);

                    int islog = (new LogAccess()).InsertLog(log);
                }
                //need to update loanSetup object
                else
                {
                    TempData["error"] = "error";
                    return RedirectToAction("step6");
                }


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
                return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
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
        /// 
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
        public JsonResult IsLoanNumberExists(string loanNumber, int RegisteredBranchId)
        {
            //check user name is already exist.  
            int userId = userData.UserId;
            //User user = (new UserAccess()).retreiveUserByUserId(userId);
            return Json((new LoanSetupAccess()).IsUniqueLoanNumberForBranch(loanNumber, RegisteredBranchId, userData,loanData.loanId), JsonRequestBehavior.AllowGet);
        }


        ///// <summary>
        ///// CreatedBy:Piyumi
        ///// CreatedDate:2016/2/8
        ///// Get Interest Deatils If Exists
        ///// </summary>
        ///// <param name="edit"></param>
        ///// <returns></returns>
        //// GET: Interest
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        //public ActionResult Step7(int? edit)
        //{
        //    int uId = userData.UserId;
        //    int branchId = loanData.BranchId;
        //    if (userData.RoleId >= 3)
        //    {
        //        return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
        //    }
        //    //int branchId = 35;
        //    List<SelectListItem> listdates = new List<SelectListItem>();
        //    for (int i = 1; i <= 28; i++)
        //    {
        //        listdates.Add(new SelectListItem
        //        {
        //            Text = i.ToString(),
        //            Value = i.ToString()
        //        });
        //    }

        //    listdates.Add(new SelectListItem
        //    {
        //        Text = "EOM",
        //        Value = "EOM"
        //    });


        //    ViewBag.PaidDate = new SelectList(listdates, "Value", "Text");
        //    InterestAccess ia = new InterestAccess();
        //    Interest intrst = new Interest();
        //    //get Accrual Methods
        //    List<AccrualMethods> methodList = ia.GetAllAccrualMethods();
        //    ViewBag.AccrualMethodId = new SelectList(methodList, "MethodId", "MethodName");

        //    if (uId > 0)
        //    {
        //        LoanSetupAccess la = new LoanSetupAccess();
        //        int loanId = loanData.loanId;

        //        //int loanId = 1;
        //        if (loanId > 0)
        //        {
        //            var intrstobj = ia.getInterestDetails(loanId);
        //            if (intrstobj != null)
        //            {

        //                ViewBag.Edit = 1;
        //                //intrst = ia.getInterestDetails(loanId);
        //                //ViewBag.AccrualMethodId = new SelectList(methodList, "MethodId", "MethodName", intrstobj.AccrualMethodId);

        //                if (intrstobj.option != "once a month")
        //                {
        //                    ViewBag.Option = true;
        //                }
        //                else
        //                {
        //                    ViewBag.Option = false;
        //                }
        //                //ViewBag.PaidDate = new SelectList(listdates, "Value", "Text", intrstobj.PaidDate);
        //                //ViewBag.Email = intrst.AutoRemindEmail;

        //                if (HttpContext.Request.IsAjaxRequest())
        //                {
        //                    ViewBag.AjaxRequest = 1;
        //                    return PartialView(intrstobj);
        //                }
        //                else
        //                {

        //                    return View(intrstobj);
        //                }
        //            }

        //            else
        //            {
        //                ViewBag.Edit = 0;
        //                //ViewBag.AccrualMethodId = new SelectList(methodList, "MethodId", "MethodName");
        //                //ViewBag.PaidDate = new SelectList(listdates, "Value", "Text");
        //                string defaultEmail = la.getAutoRemindEmailByLoanId(loanId);
        //                ViewBag.Email = defaultEmail;
        //                //intrst.AutoRemindEmail = defaultEmail;

        //                if (HttpContext.Request.IsAjaxRequest())
        //                {
        //                    ViewBag.AjaxRequest = 1;
        //                    return PartialView();
        //                }
        //                else
        //                {

        //                    return View();
        //                }

        //            }
        //            //return PartialView();
        //        }

        //        else
        //        {
        //            return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
        //        }
        //    }

        //    else
        //    {
        //        return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
        //    }

        //}

        ///// <summary>
        ///// CreatedBy:Piyumi
        ///// CreatedDate:2016/2/8
        ///// Post Interest Deatils 
        ///// </summary>
        ///// <param name="interest"></param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult Step7(Interest interest)
        //{

        //    int userId = userData.UserId;
        //    int branchId = loanData.BranchId;
        //    if (userData.RoleId >= 3)
        //    {
        //        return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
        //    }

        //    if (interest.option == "payoff")
        //    {
        //        interest.PaidDate = interest.option;
        //    }

        //    InterestAccess ia = new InterestAccess();
        //    LoanSetupAccess la = new LoanSetupAccess();
        //    //int loanId = la.getLoanIdByBranchId(branchId);
        //    int loanId = loanData.loanId;
        //    if (!interest.NeedReminder)
        //    {
        //        interest.AutoRemindEmail = null;
        //    }
        //    interest.LoanId = loanId;
        //    // if()
        //    int reslt = ia.insertInterestDetails(interest);

        //    if (reslt >= 0)
        //    {
        //        StepAccess sa = new StepAccess();
        //        if (sa.UpdateLoanSetupStep(userData.UserId,loanData.CompanyId, loanData.BranchId, loanData.nonRegisteredBranchId, loanData.loanId, 3))
        //        {
        //            if (loanData.stepId < 3)
        //            {
        //                loanData.stepId = 3;
        //            }
        //            //insert log record
        //            if (reslt == 0)
        //            {

        //                Log log = new Log(userData.UserId, userData.Company_Id, loanData.BranchId, loanId, "Interest", "Edited interest details of loan : " + loanId, DateTime.Now);

        //                int islog = (new LogAccess()).InsertLog(log);
        //            }
        //            else if(reslt > 0)
        //            {
        //                Log log = new Log(userData.UserId, userData.Company_Id, loanData.BranchId, loanId, "Interest", "Inserted interest details of loan : " + loanId, DateTime.Now);

        //                int islog = (new LogAccess()).InsertLog(log);
        //            }
        //            //loanData.stepId = 3;
        //            Session["loanStep"] = loanData;
        //            return RedirectToAction("Step8");

        //        }
        //        else
        //        {
        //            return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
        //        }

        //    }
        //    //else if (reslt == 0)
        //    //{
        //    //    return RedirectToAction("Step8");
        //    //}
        //    else
        //    {
        //        return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
        //    }

        //}

        /// <summary>
        /// Frontend page: Fees in loan setup process
        /// Title: return view of fee page
        /// Designed: kasun samarawickrama
        /// User story:
        /// Developed :kasun samarawickrama
        /// Date Created: 02/08/2016
        /// Edited :Piyumi Perera
        /// Date Edited :2016/3/8
        /// remove session variables and unwanted method calls
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step8()
        {

            var userId = userData.UserId;

            BranchAccess branch = new BranchAccess();
            //check user is user or dealer user
            if (userData.RoleId >= 3)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
            }
            //get company type
            int companyType = branch.getCompanyTypeByUserId(userId);
            //check lender company
            if (companyType == 1)
            {
                ViewBag.isLender = true;
            }
            //check dealer company
            else
            {
                ViewBag.isLender = false;
            }
            Fees fee = new Fees();
            LoanSetupAccess loan = new LoanSetupAccess();
           
            fee.LoanId = loanData.loanId;
            //check insert or update
            if (loanData.stepId > 3)
            {
                ViewBag.isEdit = "editable";
                //get fee details
                var hasLoan = loan.checkLoanIsInFeesTables(fee.LoanId);
                //check advance amount
                if (hasLoan.AdvanceAmount == 0)
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
                    //hasLoan.IsAdvanceFeeCompleteEmailReminder = false;
                    //hasLoan.IsLoanFeeCompleteEmailReminder = false;
                    //hasLoan.IsLotFeeCompleteEmailReminder = false;
                    hasLoan.IsAdvanceFeeDueEmailReminder = false;
                    hasLoan.IsLoanFeeDueEmailReminder = false;
                    hasLoan.IsLotFeeDueEmailReminder = false;

                    //if (hasLoan.AdvanceFeeDealerEmail != "")
                    //{
                    //    hasLoan.IsAdvanceFeeCompleteEmailReminder = true;
                    //}
                    //if (hasLoan.MonthlyLoanFeeDealerEmail != "")
                    //{
                    //    hasLoan.IsLoanFeeCompleteEmailReminder = true;
                    //}
                    //if (hasLoan.LotInspectionFeeDealerEmail != "")
                    //{
                    //    hasLoan.IsLotFeeCompleteEmailReminder = true;
                    //}
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
                    if (email != null)
                    {
                        feeNew.AdvanceDueEmail = email;
                        feeNew.MonthlyLoanDueEmail = email;
                        feeNew.LotInspectionDueEmail = email;
                    }
                    //feeNew.IsAdvanceFeeCompleteEmailReminder = false;
                    //feeNew.IsLotFeeCompleteEmailReminder = false;
                    //feeNew.IsLoanFeeCompleteEmailReminder = false;
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
            if (fees.AdvanceDue == "Time of Advance")
            {
                fees.AdvanceDueDate = "ToA";
            }

            //if (fees.IsAdvanceFeeCompleteEmailReminder == false)
            //{
            //    fees.AdvanceFeeDealerEmail = "";
            //    fees.AdvanceFeeDealerEmailRemindPeriod = 0;
            //}
            //if (fees.IsLoanFeeCompleteEmailReminder == false)
            //{
            //    fees.MonthlyLoanFeeDealerEmail = "";
            //    fees.MonthlyLoanFeeDealerEmailRemindPeriod = 0;
            //}
            //if (fees.IsLotFeeCompleteEmailReminder == false)
            //{
            //    fees.LotInspectionFeeDealerEmail = "";
            //    fees.LotInspectionFeeDealerEmailRemindPeriod = 0;
            //}
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
                else if (step.UpdateLoanSetupStep(userData.UserId, loanData.CompanyId, loanData.BranchId, loanData.nonRegisteredBranchId, loanData.loanId, 4))
                {

                    if (loanData.stepId < 4)
                    {
                        loanData.stepId = 4;
                        Log log = new Log(userData.UserId, userData.Company_Id, loanData.BranchId, fees.LoanId, "Fees", "Inserted fees details of loan : " + fees.LoanId, DateTime.Now);

                        int islog = (new LogAccess()).InsertLog(log);
                    }
                    else
                    {
                        Log log = new Log(userData.UserId, userData.Company_Id, loanData.BranchId, fees.LoanId, "Fees", "Edited fees details of loan : " + fees.LoanId, DateTime.Now);

                        int islog = (new LogAccess()).InsertLog(log);
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
        /// Frontend page: Title setup in loan setup
        /// Title: return view of title page
        /// Designed: Piyumi Perera
        /// User story:
        /// Developed:Piyumi Perera
        /// Date Created:2016/2/9
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns>
        // GET: Interest

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step9(int? edit)
        {
            int uId = userData.UserId;
            int branchId = loanData.BranchId;
            //check user is user or dealer user
            if (userData.RoleId >= 3)
            {
                //return to login
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
        
            //Time Limit Options
            List<SelectListItem> timeLimitList = new List<SelectListItem>();

            timeLimitList.Add(new SelectListItem
            {
                Text = "Title required to advance",
                Value = "1"
            });


            timeLimitList.Add(new SelectListItem
            {
                Text = "Title can arrive at any time",
                Value = "2"
            });

            timeLimitList.Add(new SelectListItem
            {
                Text = "Title must arrive within a specified time",
                Value = "3"
            });
            ViewBag.ReceivedTimeLimit = new SelectList(timeLimitList, "Value", "Text");
            //Receipt required methods
            List<SelectListItem> receiptRequiredMethodList = new List<SelectListItem>();

            receiptRequiredMethodList.Add(new SelectListItem
            {
                Text = "Must be physically present",
                Value = "1"
            });


            receiptRequiredMethodList.Add(new SelectListItem
            {
                Text = "Scanned copy only",
                Value = "2"
            });

            receiptRequiredMethodList.Add(new SelectListItem
            {
                Text = "Physically present or scanned copy",
                Value = "3"
            });
            ViewBag.ReceiptRequiredMethod = new SelectList(receiptRequiredMethodList, "Value", "Text");
            if (uId > 0)
            {
                LoanSetupAccess la = new LoanSetupAccess();
                TitleAccess ta = new TitleAccess();
                Title title = new Title();
                
                int loanId = loanData.loanId;
                //check loan id is greater than 0
                if (loanId > 0)
                {
                    //check is update
                    if (loanData.stepId>4)
                    {
                        //get title details
                        var titleObj = ta.getTitleDetails(loanId);
                        ViewBag.Edit = 1;
                        //check object is not null
                        if (titleObj != null)
                        {
                            if (titleObj.ReceivedTimeLimit == "Title required to advance")
                            {
                                titleObj.ReceivedTimeLimit = "1";
                            }
                            else if (titleObj.ReceivedTimeLimit == "Title can arrive at any time")
                            {
                                titleObj.ReceivedTimeLimit = "2";
                            }
                            else if (titleObj.ReceivedTimeLimit == "Title must arrive within a specified time")
                            {
                                titleObj.ReceivedTimeLimit = "3";
                            }

                            if (titleObj.ReceiptRequiredMethod == "Must be physically present")
                            {
                                titleObj.ReceiptRequiredMethod = "1";
                            }
                            else if (titleObj.ReceiptRequiredMethod == "Scanned copy only")
                            {
                                titleObj.ReceiptRequiredMethod = "2";
                            }
                            else if (titleObj.ReceiptRequiredMethod == "Physically present or scanned copy")
                            {
                                titleObj.ReceiptRequiredMethod = "3";
                            }
                           
                            ViewBag.DefaultEmail = titleObj.RemindEmail;
                        }
                        //check object is null
                        else if(titleObj == null)
                        {
                            //return to login
                            return RedirectToAction("UserLogin", "Login");
                        }
                        //check ajax request
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
                        //get auto remind email given in loan setup step 1
                        string defaultEmail = la.getAutoRemindEmailByLoanId(loanId);

                        ViewBag.Email = defaultEmail;
                       //check ajax request
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
                //if loan id is 0 or less than 0
                else
                {
                    //return to login page
                    return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
                }
            }
            //if logged user id is 0 or less than 0
            else
            {
                //return to login page
                return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
            }

        }

        /// <summary>
        /// Frontend page: Title setup in loan setup
        /// Title: insert or update title
        /// Designed: Piyumi Perera
        /// User story:
        /// Developed:Piyumi Perera
        /// Date Created:2016/2/9
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step9(Title title)
        {
            int userId = userData.UserId;
            int branchId = loanData.BranchId;
            //check user is user or dealer user
            if (userData.RoleId >= 3)
            {
                //return to login page
                return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
            }
         
            TitleAccess ta = new TitleAccess();
            LoanSetupAccess la = new LoanSetupAccess();
            StepAccess sa = new StepAccess();
          
            int loanId = loanData.loanId;
            title.LoanId = loanId;

            if (title.ReceivedTimeLimit == "1")
            {
                title.ReceivedTimeLimit = "Title required to advance";
            }
            else if (title.ReceivedTimeLimit == "2")
            {
                title.ReceivedTimeLimit = "Title can arrive at any time";
            }
            else if (title.ReceivedTimeLimit == "3")
            {
                title.ReceivedTimeLimit = "Title must arrive within a specified time";
            }

            if (title.ReceiptRequiredMethod == "1")
            {
                title.ReceiptRequiredMethod = "Must be physically present";
            }
            else if (title.ReceiptRequiredMethod == "2")
            {
                title.ReceiptRequiredMethod = "Scanned copy only";
            }
            else if (title.ReceiptRequiredMethod == "3")
            {
                title.ReceiptRequiredMethod = "Physically present or scanned copy";
            }
            //insert or update title details and get result
            int reslt = ta.insertTitleDetails(title);
            //check result is not 0
            if (reslt >= 0)
            {
                //update loan setup step table
                if (sa.UpdateLoanSetupStep(userData.UserId,loanData.CompanyId, loanData.BranchId, loanData.nonRegisteredBranchId, loanData.loanId, 5))
                {
                    //check current step value of loanData object is less than 5
                    if (loanData.stepId < 5)
                    {
                        //update step
                        loanData.stepId = 5;
                    }
                    //if title update or insert result is 0
                    if (reslt == 0)
                    {
                        //insert record in to log table as insert
                        Log log = new Log(userData.UserId, userData.Company_Id, loanData.BranchId, title.LoanId, "Title", "Edited title details of loan : " + title.LoanId, DateTime.Now);

                        int islog = (new LogAccess()).InsertLog(log);
                    }
                    if (reslt > 0)
                    {
                        //insert record in to log table as update
                        Log log = new Log(userData.UserId, userData.Company_Id, loanData.BranchId, title.LoanId, "Title", "Inserted title details of loan : " + title.LoanId, DateTime.Now);

                        int islog = (new LogAccess()).InsertLog(log);
                    }
                    //convert loan data object to session variable
                    Session["loanStep"] = loanData;
                    //return to curtailment page
                    return RedirectToAction("Step10");
                }
                //if update loan setup step result is false return to login page
                else
                {
                    return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
                }
            }
           
            else
            {
                return new HttpStatusCodeResult(404);
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
        private LoanSetupStep1 _loan;

        // GET: LoanSetUpStep5
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Step10(string lbl)
        {
            CurtailmentModel curtailment = new CurtailmentModel();

            int userId = userData.UserId;
            if (userData.RoleId >= 3)
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "You are not Allowed." });
            }

            //check user step is valid for this step
            StepAccess sa = new StepAccess();
            if (loanData.stepId == 5)
            {
                ViewBag.LoanId = 0;
                if (lbl == "Details added successfully")
                {
                    ViewBag.SuccessMsg = "Loan setup is completed";
                    Session["loanStep"] = null;
                    if (TempData["LoanId"] != null && (int)TempData["LoanId"] > 0) {
                        ViewBag.LoanId = (int)TempData["LoanId"];
                    }
                   
                    if (HttpContext.Request.IsAjaxRequest())
                    {
                        ViewBag.AjaxRequest = 1;
                        return PartialView(curtailment);
                    }
                    return View(curtailment);
                }

                int branchId = loanData.BranchId;

                LoanSetupAccess la = new LoanSetupAccess();
                int loanId = loanData.loanId;
                CurtailmentAccess curAccess = new CurtailmentAccess();
                _loan = curAccess.GetLoanDetailsByLoanId(loanId);
                _loan.loanId = loanId;
                
                curtailment.AdvancePt = _loan.advancePercentage;
                curtailment.RemainingPercentage = curtailment.AdvancePt;

                curtailment.InfoModel = new List<Curtailment>();

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
                        curtailment.InfoModel.Add(new Curtailment { CurtailmentId = curId, TimePeriod = curtailments[i].TimePeriod, Percentage = curtailments[i].Percentage });
                    }
                    curtailment.LoanStatus = _loan.LoanStatus ? "Yes" : "No";

                    curtailment.CalculationBase = _loan.CurtailmentCalculationBase == "a" ? "Advance" : "Full payment";
                    curtailment.DueDate = _loan.CurtailmentDueDate;
                    curtailment.AutoRemindEmail = _loan.CurtailmentAutoRemindEmail;
                    curtailment.EmailRemindPeriod = _loan.CurtailmentEmailRemindPeriod;
                }

                ViewBag.CalMode = "Full Payment";
                curtailment.RemainingPercentage = payPercentage - totalPercentage;

                if (curtailment.RemainingPercentage > 0)
                    curtailment.InfoModel.Add(new Curtailment { CurtailmentId = curId + 1 });
                ViewData["objmodel"] = curtailment;

                if (HttpContext.Request.IsAjaxRequest())
                {
                    ViewBag.AjaxRequest = 1;
                    return PartialView(curtailment);
                }
                else
                {

                    return View(curtailment);
                }
            }
            return RedirectToAction("UserLogin", "Login", new { lbl = "Due to inactivity your session has timed out, please log in again." });
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

            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
            StepAccess sa = new StepAccess();
            if (curtailmentAccess.InsertCurtailment(curtailmentList, loanData.loanId) == 1)
            {
                ViewBag.SuccessMsg = "Curtailment Details added successfully";
                sa.UpdateLoanSetupStep(userData.UserId,loanData.CompanyId, loanData.BranchId, loanData.nonRegisteredBranchId, loanData.loanId, 6);
               
                    Log log = new Log(userData.UserId, userData.Company_Id, loanData.BranchId, loanData.loanId, "Curtailment", "Inserted curtailment details of loan : " + loanData.loanId, DateTime.Now);

                    int islog = (new LogAccess()).InsertLog(log);

                ViewBag.Redirect = 1;
            }
            else
            {
                sa.UpdateLoanSetupStep(userData.UserId,loanData.CompanyId, loanData.BranchId, loanData.nonRegisteredBranchId, loanData.loanId, 6);
                
                    Log log = new Log(userData.UserId, userData.Company_Id, loanData.BranchId, loanData.loanId, "Curtailment", "Edited curtailment details of loan : " + loanData.loanId, DateTime.Now);

                    int islog = (new LogAccess()).InsertLog(log);
               
                ViewBag.SuccessMsg = "Curtailment Details updated successfully";
            }

            //bool loanActive = curtaiulmentModel.LoanStatus == "Yes";

            LoanSetupAccess loanAccess = new LoanSetupAccess();
            loanAccess.UpdateLoanCurtailment(curtaiulmentModel, loanData.loanId);
            TempData["LoanId"] = loanData.loanId;
            return RedirectToAction("Step10", new { lbl = "Details added successfully" });
        }

        /// <summary>
        /// Created by : kasun
        /// Created Date: 7/6/2016
        /// 
        /// Summery: Loan term print
        /// </summary>
        /// <param name="loanId"></param>
        /// <returns></returns>
        public FileResult PrintPage(int loanId)
        {

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;

            RptDivLoanTerms loanTerm = new RptDivLoanTerms();
            var rptViewerPrint = loanTerm.PrintPage(loanId);

            var bytes = rptViewerPrint.LocalReport.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);

            var fsResult = File(bytes, "application/pdf");
            return fsResult;
        }

    }
}

