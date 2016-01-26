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
        private static UserCompanyModel userCompany = null;
        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/26
        /// As the initial super admin I should able to create company
        /// in the setup proccess
        /// </summary>
        /// <returns></returns>
        public ActionResult Step1()
        {
            Session["userId"] = 10;

            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");

            CompanyAccess ca = new CompanyAccess();

            // Get company types to list
            List<CompanyType> ctList = ca.GetAllCompanyType();
            ViewBag.TypeId = new SelectList(ctList, "TypeId", "TypeName");

            //Get states to list
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            return View();
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
            if (company.Extention != null)
                company.Zip += "-" + company.Extention;

            company.CreatedBy = company.FirstSuperAdminId = Convert.ToInt32(Session["userId"]);

            CompanyAccess ca = new CompanyAccess();

            if (ca.InsertCompany(company))
            {
                ViewBag.SuccessMsg = "Company Successfully setup.";
            }
            else
            {
                ViewBag.ErrorMsg = "Failed to Setup company.";
            }

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
            int userId = 4;
            if (userId > 0)
            {
                //int userId = (int)Session["userId"];
                StepAccess cs = new StepAccess();
                int reslt = cs.getStepNumberByUserId(userId);
                if (reslt == 1)
                {
                    userCompany = (UserCompanyModel)TempData["UserCompany"];
                    userCompany.Branch = new Branch();
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
        public ActionResult CreateFirstBranch(UserCompanyModel userCompany2)
        {
            int userId = (int)Session["userId"];
            BranchAccess ba = new BranchAccess();
            userCompany2.Branch.BranchCode = ba.createBranchCode(userCompany.Company.CompanyCode);
            userCompany.Branch = userCompany2.Branch;
            bool reslt = ba.insertFirstBranchDetails(userCompany, userId);
            if (reslt)
            {
                ViewBag.SuccessMsg = "First Branch is created successfully";
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
            int userId = 68;

            // check he is a super admin or not
            if ((new UserManageAccess()).getUserRole(userId) != 1)
            {
                return new HttpStatusCodeResult(404);
            }

            // check if   step is 3...

            if(lbls != null && lbls.Equals("User Successfully Created"))
            {
                ViewBag.SuccessMsg = "User Successfully Created";

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

            //if (Session["userId"] == null || Session["userId"].ToString() == "")
            //    return RedirectToAction("UserLogin", "Login");

            //int currentUser = int.Parse(Session["userId"].ToString());

            int currentUser = 68; // user id is hardcoded here
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


                    // update user step to 3
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

    }
}