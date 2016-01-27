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
        public static string CompanyType = "Lender";
        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/26
        /// As the initial super admin I should able to create company
        /// in the setup proccess
        /// </summary>
        /// <returns></returns>
        public ActionResult Step1()
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");

            int userId = Convert.ToInt32(Session["userId"]);

            StepAccess sa = new StepAccess();
            if (sa.getStepNumberByUserId(userId) == 1)
            {
                CompanyAccess ca = new CompanyAccess();

                // Get company types to list
                List<CompanyType> ctList = ca.GetAllCompanyType();
                ViewBag.TypeId = new SelectList(ctList, "TypeId", "TypeName");

                //Get states to list
                List<State> stateList = ca.GetAllStates();
                ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

                return View();
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
                return RedirectToAction("UserLogin", "Login");

            GeneratesCode gc = new GeneratesCode();
            company.CompanyCode = gc.GenerateCompanyCode(company.CompanyName);

            company.Zip = company.ZipPre;
            if (company.Extension != null)
                company.Zip += "-" + company.Extension;

            company.CreatedBy = company.FirstSuperAdminId = Convert.ToInt32(Session["userId"]);

            CompanyAccess ca = new CompanyAccess();

            if (ca.InsertCompany(company))
            {
                ViewBag.SuccessMsg = "Company Successfully setup.";

                CompanyType = (company.TypeId == 1) ? "Lender" : "Dealer";

                //If succeed update step table to step2 
                StepAccess sa = new StepAccess();
                sa.updateStepNumberByUserId(company.FirstSuperAdminId, 2);

                //Send company detail to step 2
                CompanyBranchModel comBranch = new CompanyBranchModel();
                comBranch.Company = company; 

                TempData["Company"] = comBranch.Company;
                return RedirectToAction("Step2");
            }
            ViewBag.ErrorMsg = "Failed to Setup company.";

            // Get company types to list
            List<CompanyType> ctList = ca.GetAllCompanyType();
            ViewBag.TypeId = new SelectList(ctList, "TypeId", "TypeName");

            //Get states to list
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            return View();
        }

/// <summary>
/// CreatedBy:Piyumi
/// CreatedDate:2016/1/26
/// Get first branch details
/// </summary>
/// <returns></returns>
        public ActionResult Step2()
        {
            //Session["userId"] = 4;
           //int userId = 68;
            if ((Session["userId"]!=null)&& (Session["userId"].ToString()!=""))
            //if(userId>0)
            {
                int userId = (int)Session["userId"];
                StepAccess cs = new StepAccess();
                int reslt = cs.getStepNumberByUserId(userId);
                if (reslt == 2)
                {
                    if ((TempData["Company"] != null) && (TempData["Company"].ToString() != ""))
                    {
                        userCompany = (CompanyBranchModel) TempData["Company"];
                        userCompany.MainBranch = new Branch();
                        if (userCompany.Company.Extension == null)
                            userCompany.Company.Extension = "";
                    }

                    //Get states to list
                    CompanyAccess ca = new CompanyAccess();
                    List<State> stateList = ca.GetAllStates();
                    ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

                    return View(userCompany);

                }
                else
                {
                    return RedirectToAction("UserLogin", "Login");
                }
            }
            else {
                return RedirectToAction("UserLogin", "Login");
            }




        }

        //Post Branch
        /// <summary>
        /// CreatedBy:Piyumi
        /// CreatedDate:2016/1/26
        /// insert branch details
        /// </summary>
        /// <param name="userCompany2"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step2(CompanyBranchModel userCompany2)
        {
            int userId = (int)Session["userId"];
            //int userId = 68;
            BranchAccess ba = new BranchAccess();


            //userCompany2.MainBranch.BranchCode = ba.createBranchCode(userCompany.Company.CompanyCode);
            //userCompany.MainBranch = userCompany2.MainBranch;
            bool reslt = ba.insertFirstBranchDetails(userCompany2, userId);
            if (reslt)
            {
                StepAccess sa = new StepAccess();
                if(sa.updateStepNumberByUserId(userId,2)) 
                {
                    return RedirectToAction("Step3");
                    //ViewBag.SuccessMsg = "First Branch is created successfully";
                }
                
            }
            else
            {
                ViewBag.ErrorMsg = "Failed to create first branch";
            }
            return View();
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
            StepAccess sa = new StepAccess();
            int userId = int.Parse(Session["userId"].ToString());



            // check he is a super admin or not
            if ((new UserManageAccess()).getUserRole(userId) != 1)
            {
                return new HttpStatusCodeResult(404);
            }

            // check if   step is 3...
            
            if(sa.getStepNumberByUserId(userId) != 3 && sa.getStepNumberByUserId(userId) != 4)
            {
                return new HttpStatusCodeResult(404);
            }

            if (lbls != null && lbls.Equals("User Successfully Created"))
            {
                ViewBag.SuccessMsg = "User Successfully Created";
                sa.updateStepNumberByUserId(userId,4);
                return View();
            }

            UserAccess ua = new UserAccess();
            User curUser = ua.retreiveUserByUserId(userId);

            
            RoleAccess ra = new RoleAccess();
            List<UserRole> roleList = ra.GetAllUserRoles();






            // get all branches
            List<Branch> branchesLists = (new BranchAccess()).getBranches(curUser.Company_Id);

            ViewBag.RoleId = new SelectList(roleList, "RoleId", "RoleName");
            ViewBag.BranchId = new SelectList(branchesLists, "BranchId", "BranchName");

            return View();
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

            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");

            
            

            int currentUser = int.Parse(Session["userId"].ToString());
            // check he is a super admin or not
            if ((new UserManageAccess()).getUserRole(currentUser) != 1)
            {
                return new HttpStatusCodeResult(404);
            }

            // check if   step is 3...
            StepAccess sa = new StepAccess();
            if (sa.getStepNumberByUserId(currentUser) != 3 && sa.getStepNumberByUserId(currentUser) != 4)
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


                return View();
            }
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/27
        /// </summary>
        /// <returns></returns>
        public ActionResult Step4()
        {

            //if (Session["userId"] == null || Session["userId"].ToString() == "")
            //    return RedirectToAction("UserLogin", "Login");

            //int userId = Convert.ToInt32(Session["userId"]);

            //StepAccess sa = new StepAccess();
            //if (sa.getStepNumberByUserId(userId) == 4)
            //{
            

            ViewBag.ThisCompanyType = (CompanyType == "Lender") ? "Dealer" : "Lender";

            //Get states to list
            CompanyAccess ca = new CompanyAccess();
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            return View();
            
            
            //}

            //return RedirectToAction("UserLogin", "Login");
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/27
        /// </summary>
        /// <param name="nonRegCom"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step4(Company nonRegCom)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");

            GeneratesCode gc = new GeneratesCode();
            nonRegCom.CompanyCode = gc.GenerateCompanyCode(nonRegCom.CompanyName);

            nonRegCom.Zip = nonRegCom.ZipPre;
            if (nonRegCom.Extension != null)
                nonRegCom.Zip += "-" + nonRegCom.Extension;

            nonRegCom.CreatedBy = Convert.ToInt32(Session["userId"]);
            nonRegCom.TypeId = (CompanyType == "Lender") ? 2 : 1;

            CompanyAccess ca = new CompanyAccess();

            if (ca.InsertNonRegisteredCompany(nonRegCom))
            {
                ViewBag.SuccessMsg = ((CompanyType == "Lender") ? "Dealer" : "Lender") + " Successfully created.";

                //If succeed update step table to step2 
                StepAccess sa = new StepAccess();
                sa.updateStepNumberByUserId(nonRegCom.FirstSuperAdminId, 5);

                //Send company detail to step 2
                CompanyBranchModel comBranch = new CompanyBranchModel();
                comBranch.Company = nonRegCom;

                TempData["NonRegCompany"] = comBranch.Company;
                return RedirectToAction("Step5");
            }
            ViewBag.ErrorMsg = "Failed to create " + ((CompanyType == "Lender") ? "Dealer" : "Lender") + " company.";

            //Get states to list
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            return View();
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
        public ActionResult Step6()
        {
            

            int userId = int.Parse(Session["userId"].ToString());
            
            // check he is super admin or admin
            if (new UserManageAccess().getUserRole(userId) > 2)
            {
                return new HttpStatusCodeResult(404);
            }

            // check if   step is 6...
            StepAccess sa = new StepAccess();
            if (sa.getStepNumberByUserId(userId) != 6)
            {
                return new HttpStatusCodeResult(404);
            }

            

            return View();

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
        public ActionResult Step6_Post()
        {
            int userId = int.Parse(Session["userId"].ToString());
            
            
            // check he is super admin or admin
            if (new UserManageAccess().getUserRole(userId) > 2)
            {
                return new HttpStatusCodeResult(404);
            }

            // check if   step is 6...
            StepAccess sa = new StepAccess();
            if (sa.getStepNumberByUserId(userId) != 6)
            {
                return new HttpStatusCodeResult(404);
            }

            sa.updateStepNumberByUserId(userId, 7);

            return RedirectToAction("Detailsset", "UserManagement" ,new { id  = userId });

        }




    }


}

