﻿using System.Linq;
using System.Web.Mvc;
using System.Data;
using BankLoanSystem.Models;
using BankLoanSystem.DAL;
using BankLoanSystem.Code;
using System.Web;
using System;

namespace BankLoanSystem.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// user login view
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogin(string lbl, string lbl3)

        {
          

                //
                Session["detail"] = null;
            Session["dashboard"] = null;
            Session["loanDashboard"] = null;
            Session.Remove("oneLoanDashboard");
            //// for show the popup message in login page
            //if (Session["isNotCompleteStep"] != null && int.Parse(Session["isNotCompleteStep"].ToString()) == 1)
            //    {
            //    Session["isNotCompleteStep"] = null;
            //    return View();
            //}

            ViewBag.login = true;
            if (lbl3 != null)
            {
                ViewBag.SuccessMsg = lbl3;
            }
            if (lbl != null)
            {
                var loginlbl = new UserLogin();
                loginlbl.lbl = lbl;
                Session["AuthenticatedUser"] = null;
                Session["loanStep"] = null;
                Session["companyStep"] = null;
                Session["loanCode"] = null;
                return View(loginlbl);
            }
            else {
                Session["AuthenticatedUser"] = null;
                Session["loanStep"] = null;
                Session["companyStep"] = null;
                Session["loanCode"] = null;
                return View();
            }
        }

        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// employee login view
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeeLogin(string lbl)
        {
            ViewBag.login = true;
            if (lbl != null)
            {
                var loginlbl = new UserLogin();
                loginlbl.lbl = lbl;
                return View(loginlbl);
            }
            else {
                return View();
            }
        }

        /// <summary>
        /// CreatedBy : Kasun Samarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// User Login Controller
        /// 

        /// UpdatedBy : Asanka
        /// UpdatedDate: 2016/03/04
        /// 
        /// </summary>
        /// <param name="user">login user details</param>
        /// <returns>rederect to user dashboard</returns>
        [HttpPost]
        public ActionResult UserLogin(UserLogin user)
        {
            try
            {
                //string ip = Request.UserHostAddress;
                DataSet dsUser = new DataSet();
                var login = new LoginAccess();
                var step = new StepAccess();
                User userData = new User();
                userData.UserName = user.userName;

                //pass user name to database and get user details
                dsUser = login.CheckUserLogin(userData);
                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    userData.UserId = int.Parse(dsUser.Tables[0].Rows[0]["user_id"].ToString());
                    userData.UserName = dsUser.Tables[0].Rows[0]["user_name"].ToString();
                    userData.Password = dsUser.Tables[0].Rows[0]["password"].ToString();
                   
                    userData.RoleId = int.Parse(dsUser.Tables[0].Rows[0]["role_id"].ToString());
                    if (userData.RoleId == 4)
                    {
                        DataSet dsDelearCompany = new DataSet();
                        dsDelearCompany = login.GetDealerUserCompanyBranch(userData.UserId);
                        if (dsDelearCompany.Tables[0].Rows[0]["company_id"].ToString() != "")
                        {
                            userData.Company_Id = int.Parse(dsDelearCompany.Tables[0].Rows[0]["company_id"].ToString());
                            userData.CompanyName = dsDelearCompany.Tables[0].Rows[0]["company_name"].ToString();
                        }
                        if (dsDelearCompany.Tables[0].Rows[0]["branch_id"].ToString() != "")
                        {
                            userData.BranchId = int.Parse(dsDelearCompany.Tables[0].Rows[0]["branch_id"].ToString());
                            userData.BranchName = dsDelearCompany.Tables[0].Rows[0]["branch_name"].ToString();
                        }

                    }
                    else {
                        userData.BranchId = int.Parse(dsUser.Tables[0].Rows[0]["branch_id"].ToString());
                        userData.BranchName = dsUser.Tables[0].Rows[0]["branch_name"].ToString();
                        if (dsUser.Tables[0].Rows[0]["company_id"].ToString() != "")
                        {
                            userData.Company_Id = int.Parse(dsUser.Tables[0].Rows[0]["company_id"].ToString());
                            userData.CompanyType = int.Parse(dsUser.Tables[0].Rows[0]["company_type"].ToString());
                            userData.CompanyCode = dsUser.Tables[0].Rows[0]["company_code"].ToString();
                        }
                        else
                        {
                            userData.Company_Id = 0;
                        }
                        userData.CompanyName = dsUser.Tables[0].Rows[0]["company_name"].ToString();
                    }
                    
                    userData.step_status = int.Parse(dsUser.Tables[0].Rows[0]["step_status"].ToString());
                    
                    //To compair Database password and user enter password
                    string passwordFromDB = userData.Password;
                    char[] delimiter = { ':' };
                    string[] split = passwordFromDB.Split(delimiter);
                    var checkCharHave = passwordFromDB.ToLowerInvariant().Contains(':');
                    if (passwordFromDB == null || (checkCharHave == false))
                    {
                        Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, 0, "User Login", "User : " + userData.UserName + " was unable to login, Entered password did not match", DateTime.Now);

                        int islog = (new LogAccess()).InsertLog(log);
                        return RedirectToAction("UserLogin", "Login", new { lbl = "Incorrect Username & Password combination. Please confirm entry and resubmit." });
                    }

                    string passwordEncripted = PasswordEncryption.encryptPassword(user.password, split[1]);
                    if (string.Compare(passwordEncripted, passwordFromDB) == 0)
                    {
                       
                        //user object pass to session
                        Session["AuthenticatedUser"] = userData;

                        // Does not complete atleast one cycle
                        if (userData.step_status == 0)
                        {
                            if (userData.RoleId == 3)
                            {
                                Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, 0, "User Login", "User : " + userData.UserName + " was unable to login", DateTime.Now);

                                int islog = (new LogAccess()).InsertLog(log);
                                return RedirectToAction("UserLogin", "Login", new { lbl = "Company setup process is on going please contact admin." });
                            }
                            else
                            {
                                Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, 0, "User Login", "User : " + userData.UserName + " has logged successfully", DateTime.Now);

                                int islog = (new LogAccess()).InsertLog(log);
                                if (userData.Company_Id == 0)
                                {
                                    Session["companyStep"] = 1;
                                    return RedirectToAction("Index", "SetupProcess");
                                }
                                else if (userData.Company_Id > 0)
                                {
                                    if (userData.RoleId == 1)
                                    {
                                        DataSet dsStepNo = new DataSet();
                                        dsStepNo = step.checkSuperAdminLoginWhileCompanySetup(userData);
                                        if (dsStepNo.Tables[0].Rows.Count > 0)
                                        {
                                            Session["companyStep"] = int.Parse(dsStepNo.Tables[0].Rows[0]["step_number"].ToString());
                                            return RedirectToAction("Index", "SetupProcess");
                                        }
                                        else
                                        {
                                            LoanSetupStep loanStep = new LoanSetupStep();
                                            DataSet dsLoanStepNo = new DataSet();
                                            dsLoanStepNo = step.checkUserLoginWhileLoanSetup(userData);
                                            if (dsLoanStepNo.Tables[0].Rows.Count > 0)
                                            {
                                                loanStep.CompanyId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["company_id"].ToString());
                                                loanStep.BranchId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["branch_id"].ToString());
                                                loanStep.stepId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["step_number"].ToString());
                                                loanStep.nonRegisteredBranchId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["non_registered_branch_id"].ToString());
                                                if (dsLoanStepNo.Tables[0].Rows[0]["loan_id"].ToString() != "")
                                                {
                                                    loanStep.loanId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["loan_id"].ToString());
                                                }
                                                else
                                                {
                                                    loanStep.loanId = 0;
                                                }
                                                Session["loanStep"] = loanStep;
                                                if (userData.RoleId == 1)
                                                {
                                                    return RedirectToAction("Step" + (loanStep.stepId + 5), "SetupProcess");
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //if step table has record pass(company id and branch id)
                                        DataSet dsStepNo = new DataSet();
                                        dsStepNo = step.checkUserLoginWhileCompanySetup(userData);
                                        if (dsStepNo.Tables[0].Rows.Count > 0)
                                        {
                                            Session["companyStep"] = int.Parse(dsStepNo.Tables[0].Rows[0]["step_number"].ToString());
                                            return RedirectToAction("Index", "SetupProcess");
                                        }
                                        else
                                        {
                                            //No Step recor in relavent Company and branch
                                            LoanSetupStep loanStep = new LoanSetupStep();
                                            DataSet dsLoanStepNo = new DataSet();
                                            dsLoanStepNo = step.checkUserLoginWhileLoanSetup(userData);
                                            if (dsLoanStepNo.Tables[0].Rows.Count > 0)
                                            {
                                                loanStep.CompanyId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["company_id"].ToString());
                                                loanStep.BranchId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["branch_id"].ToString());
                                                loanStep.stepId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["step_number"].ToString());
                                                loanStep.nonRegisteredBranchId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["non_registered_branch_id"].ToString());
                                                if (dsLoanStepNo.Tables[0].Rows[0]["loan_id"].ToString() != "")
                                                {
                                                    loanStep.loanId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["loan_id"].ToString());
                                                }
                                                else
                                                {
                                                    loanStep.loanId = 0;
                                                }

                                                Session["loanStep"] = loanStep;
                                                return RedirectToAction("Step" + (loanStep.stepId + 5), "SetupProcess");
                                            }

                                        }
                                    }


                                }
                            }
                        }
                        // Complete cycle and no start new cycle
                        else if (userData.step_status == 1 || userData.step_status == 2)
                        {
                            //delete just added unit if exists
                            UnitAccess ua = new UnitAccess();
                            ua.DeleteJustAddedUnits(userData.UserId);

                            //insert log
                            Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, 0, "User Login", "User : " + userData.UserName + " has logged successfully", DateTime.Now);

                            int islog = (new LogAccess()).InsertLog(log);

                            return RedirectToAction("UserDetails", "UserManagement");
                        }
                        // atleast one cycle complete and Start new cycle 
                        //else if (userData.step_status == 2)
                        //{

                        //    //delete just added unit if exists
                        //    UnitAccess ua = new UnitAccess();
                        //    ua.DeleteJustAddedUnits(userData.UserId);
                        //    //insert log
                        //    Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, 0, "User Login", "User : " + userData.UserName + " has logged successfully", DateTime.Now);

                        //    int islog = (new LogAccess()).InsertLog(log);
                        //    if (userData.RoleId == 1)
                        //    {
                        //        DataSet dsStepNo = new DataSet();
                        //        dsStepNo = step.checkSuperAdminLoginWhileCompanySetup(userData);
                        //        if (dsStepNo.Tables[0].Rows.Count > 0)
                        //        {
                        //            Session["companyStep"] = int.Parse(dsStepNo.Tables[0].Rows[0]["step_number"].ToString());
                        //            Session["isNotCompleteStep"] = 1;
                        //            //return RedirectToAction("Index", "SetupProcess");
                        //            return RedirectToAction("UserLogin", "Login");
                        //        }
                        //        else
                        //        {
                        //            LoanSetupStep loanStep = new LoanSetupStep();
                        //            DataSet dsLoanStepNo = new DataSet();
                        //            dsLoanStepNo = step.checkUserLoginWhileLoanSetup(userData);
                        //            if (dsLoanStepNo.Tables[0].Rows.Count > 0)
                        //            {
                        //                loanStep.CompanyId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["company_id"].ToString());
                        //                loanStep.BranchId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["branch_id"].ToString());
                        //                loanStep.stepId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["step_number"].ToString());
                        //                loanStep.nonRegisteredBranchId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["non_registered_branch_id"].ToString());
                        //                if (dsLoanStepNo.Tables[0].Rows[0]["loan_id"].ToString() != "")
                        //                {
                        //                    loanStep.loanId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["loan_id"].ToString());
                        //                }
                        //                else
                        //                {
                        //                    loanStep.loanId = 0;
                        //                }
                        //                Session["isNotCompleteStep"] = 1;
                        //                Session["loanStep"] = loanStep;
                        //                if (userData.RoleId == 1)
                        //                {
                        //                    //return RedirectToAction("Step" + (loanStep.stepId + 5), "SetupProcess");
                        //                    return RedirectToAction("UserLogin", "Login");
                        //                }
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        //if step table has record pass(company id and branch id)
                        //        DataSet dsStepNo = new DataSet();
                        //        dsStepNo = step.checkUserLoginWhileCompanySetup(userData);
                        //        if (dsStepNo.Tables[0].Rows.Count > 0)
                        //        {
                        //            Session["isNotCompleteStep"] = 1;
                        //            Session["companyStep"] = int.Parse(dsStepNo.Tables[0].Rows[0]["step_number"].ToString());
                        //            return RedirectToAction("UserLogin", "Login");
                        //        }
                        //        else
                        //        {
                        //            //No Step recor in relavent Company and branch
                        //            LoanSetupStep loanStep = new LoanSetupStep();
                        //            DataSet dsLoanStepNo = new DataSet();
                        //            dsLoanStepNo = step.checkUserLoginWhileLoanSetup(userData);
                        //            if (dsLoanStepNo.Tables[0].Rows.Count > 0)
                        //            {
                        //                loanStep.CompanyId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["company_id"].ToString());
                        //                loanStep.BranchId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["branch_id"].ToString());
                        //                loanStep.stepId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["step_number"].ToString());
                        //                loanStep.nonRegisteredBranchId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["non_registered_branch_id"].ToString());
                        //                if (dsLoanStepNo.Tables[0].Rows[0]["loan_id"].ToString() != "")
                        //                {
                        //                    loanStep.loanId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["loan_id"].ToString());
                        //                }
                        //                else
                        //                {
                        //                    loanStep.loanId = 0;
                        //                }

                        //                Session["isNotCompleteStep"] = 1;
                        //                Session["loanStep"] = loanStep;

                        //                //return RedirectToAction("Step" + (loanStep.stepId + 5), "SetupProcess");
                        //                return RedirectToAction("UserLogin", "Login");
                        //            }
                        //            //if SA stepstatus 2 and no records in loan setup and companysetup
                        //            else
                        //            {
                        //                return RedirectToAction("UserDetails", "Usermanagement");
                        //            }
                        //        }


                        //    }
                        //}
                        else
                        {
                            //insert log
                            Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, 0, "User Login", "User : " + userData.UserName + " was unable to login", DateTime.Now);

                            int islog = (new LogAccess()).InsertLog(log);
                            return RedirectToAction("UserLogin", "Login", new { lbl = "Company setup process is on going please contact admin." });
                        }







                        //    //check Company setup process
                        //    //Check SuperAdmin
                        //    //company ID null or 0 then redirect to step process 1
                        //    if (userData.Company_Id == 0)
                        //    {
                        //        Session["companyStep"] = 1;
                        //        return RedirectToAction("Index", "SetupProcess");
                        //    }
                        //    else if (userData.Company_Id > 0)
                        //    {
                        //        //check branch count more than one and 
                        //        if (userData.RoleId == 1)
                        //        {
                        //            //check branch count in view and step table row count
                        //            //IF more than branch count and has step record ask question

                        //            DataSet dsStepNo = new DataSet();
                        //            dsStepNo = step.checkSuperAdminLoginWhileCompanySetup(userData);
                        //            if (dsStepNo.Tables[0].Rows.Count > 0)
                        //            {
                        //                int bcount = 0;
                        //                if (dsStepNo.Tables[0].Rows[0]["branchCount"].ToString() != "")
                        //                {
                        //                    bcount = int.Parse(dsStepNo.Tables[0].Rows[0]["branchCount"].ToString());
                        //                }
                        //                int scount = 0;
                        //                if (dsStepNo.Tables[0].Rows[0]["stepCount"].ToString() != "")
                        //                {
                        //                    scount = int.Parse(dsStepNo.Tables[0].Rows[0]["stepCount"].ToString());
                        //                }
                        //                if (bcount <= scount)
                        //                {
                        //                    Session["companyStep"] = int.Parse(dsStepNo.Tables[0].Rows[0]["step_number"].ToString());
                        //                    return RedirectToAction("Index", "SetupProcess");

                        //                }
                        //                else
                        //                {
                        //                    //message: Not complete Step, Do you want to complete it.
                        //                    Session["isNotCompleteStep"] = 1;
                        //                    Session["companyStep"] = int.Parse(dsStepNo.Tables[0].Rows[0]["step_number"].ToString());
                        //                    return RedirectToAction("UserLogin", "Login");
                        //                }
                        //            }
                        //            else
                        //            {
                        //                LoanSetupStep loanStep = new LoanSetupStep();
                        //                DataSet dsLoanStepNo = new DataSet();
                        //                dsLoanStepNo = step.checkUserLoginWhileLoanSetup(userData);
                        //                if (dsLoanStepNo.Tables[0].Rows.Count > 0)
                        //                {
                        //                    loanStep.CompanyId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["company_id"].ToString());
                        //                    loanStep.BranchId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["branch_id"].ToString());
                        //                    loanStep.stepId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["step_number"].ToString());
                        //                    loanStep.nonRegisteredBranchId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["non_registered_branch_id"].ToString());
                        //                    if (dsLoanStepNo.Tables[0].Rows[0]["loan_id"].ToString() != "")
                        //                    {
                        //                        loanStep.loanId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["loan_id"].ToString());
                        //                    }
                        //                    else
                        //                    {
                        //                        loanStep.loanId = 0;
                        //                    }
                        //                    Session["loanStep"] = loanStep;
                        //                    if (userData.RoleId == 1)
                        //                    {
                        //                        return RedirectToAction("Step" + (loanStep.stepId + 5), "SetupProcess");
                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    //Redirect to Super Admin dashboard
                        //                    return RedirectToAction("UserDetails", "UserManagement");
                        //                }

                        //            }

                        //        }
                        //        else
                        //        {
                        //            //if step table has record pass(company id and branch id)
                        //            DataSet dsStepNo = new DataSet();
                        //            dsStepNo = step.checkUserLoginWhileCompanySetup(userData);
                        //            if (dsStepNo.Tables[0].Rows.Count > 0)
                        //            {
                        //                Session["companyStep"] = int.Parse(dsStepNo.Tables[0].Rows[0]["step_number"].ToString());
                        //                if (userData.RoleId == 2)
                        //                {
                        //                    return RedirectToAction("Index", "SetupProcess");
                        //                }
                        //                else
                        //                {
                        //                    return RedirectToAction("UserLogin", "Login", new { lbl = "Company setup process is on going please contact admin." });
                        //                }
                        //            }
                        //            else
                        //            {
                        //                //No Step recor in relavent Company and branch
                        //                LoanSetupStep loanStep = new LoanSetupStep();
                        //                DataSet dsLoanStepNo = new DataSet();
                        //                dsLoanStepNo = step.checkUserLoginWhileLoanSetup(userData);
                        //                if (dsLoanStepNo.Tables[0].Rows.Count > 0)
                        //                {
                        //                    loanStep.CompanyId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["company_id"].ToString());
                        //                    loanStep.BranchId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["branch_id"].ToString());
                        //                    loanStep.stepId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["step_number"].ToString());
                        //                    loanStep.nonRegisteredBranchId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["non_registered_branch_id"].ToString());
                        //                    if (dsLoanStepNo.Tables[0].Rows[0]["loan_id"].ToString() != "")
                        //                    {
                        //                        loanStep.loanId = int.Parse(dsLoanStepNo.Tables[0].Rows[0]["loan_id"].ToString());
                        //                    }
                        //                    else
                        //                    {
                        //                        loanStep.loanId = 0;
                        //                    }

                        //                    Session["loanStep"] = loanStep;
                        //                    if (userData.RoleId == 2)
                        //                    {
                        //                        //return RedirectToAction("Index", "SetupProcess");
                        //                        return RedirectToAction("Step" + (loanStep.stepId+5), "SetupProcess");
                        //                    }
                        //                }
                        //                else
                        //                {
                        //                    if (userData.RoleId == 2)
                        //                    {
                        //                        //Redirect to Branch Admin dashboard
                        //                        return RedirectToAction("UserDetails", "UserManagement");
                        //                    }
                        //                    else
                        //                    {
                        //                        //Redirect to User dashboard
                        //                        return RedirectToAction("UserDetails", "UserManagement");
                        //                    }
                        //                }


                        //            }

                        //        }
                        //    }

                        //}
                        //else
                        //{
                        //    //User Name Correct but user enter password does not match with database password value
                        //    return RedirectToAction("UserLogin", "Login", new { lbl = "Incorrect Username or Password, please confirm and submit." });
                        //}
                        //insert log entry
                      
                    }
                    else
                    {
                        //User Name Correct but user enter password does not match with database password value
                        Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, 0, "User Login", "User : " + userData.UserName + " was unable to login, Entered password did not match ", DateTime.Now);

                        int islog = (new LogAccess()).InsertLog(log);
                        return RedirectToAction("UserLogin", "Login", new { lbl = "Incorrect Username & Password combination. Please confirm entry and resubmit." });
                    }
                }
                else
                {
                    //Incorrect UserName
                    Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, 0, "User Login", "User : " + userData.UserName + " was unable to login, Entered username did not exist ", DateTime.Now);

                    int islog = (new LogAccess()).InsertLog(log);
                    return RedirectToAction("UserLogin", "Login", new { lbl = "Incorrect Username & Password combination. Please confirm entry and resubmit." });
                }     
            }
            catch
            {
                return RedirectToAction("UserLogin", "Login", new { lbl = "An error has occurred.Please try again later" });
            }
            return RedirectToAction("UserLogin", "Login");
        }

        /// <summary>
        /// CreatedBy : Kasun Smarawickrama
        /// CreatedDate: 2016/01/14
        /// 
        /// user login controller
        /// </summary>
        /// <param name="user">login employee details</param>
        /// <returns>rederect to employee dashboard</returns>
        [HttpPost]
        public ActionResult EmployeeLogin(UserLogin user)
        {
            var login = new LoginAccess();

            int userId = login.CheckEmployeeLogin(user.userName, user.password);

            if (userId > 0)
            {
                Session["employeeId"] = userId;
                return RedirectToAction("EmployeeDetail", "EmployeeDashBoard");
            }
            else {
                return RedirectToAction("EmployeeLogin", "Login", new { lbl = "Incorrect Username & Password combination. Please confirm entry and resubmit." });
            }
        }


        public ActionResult EmpLogin()
        {
            Session["employeeId"] = null;
            if (TempData["lbl"] != null)
            {
                var loginlbl = new UserLogin();
                loginlbl.lbl = (string)TempData["lbl"];
                return View(loginlbl);
            }

            return View();
        }

        [HttpPost]
        public ActionResult EmpLogin(UserLogin user)
        {
            var login = new LoginAccess();

            int userId = login.CheckEmployeeLogin(user.userName, user.password);

            if (userId > 0)
            {
                Session["employeeId"] = userId;
                return RedirectToAction("SignUp", "SetupSignUp");
            }

            TempData["lbl"] = "Incorrect Username & Password combination. Please confirm entry and resubmit.";
            return RedirectToAction("EmpLogin", "Login");
        }
    }
}