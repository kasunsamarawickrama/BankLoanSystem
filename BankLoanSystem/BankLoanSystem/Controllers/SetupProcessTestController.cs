using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System.Data;

namespace BankLoanSystem.Controllers
{
    public class SetupProcessTestController : Controller
    {
        private static CompanyBranchModel userCompany = null;
        private static CompanyBranchModel userNonRegCompany = null;
        private static string CompanyType = "Lender";
        private static string _comCode;
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

        // GET: SetupProcessTest
        public ActionResult Step1(int? edit)
        {
            Session["userId"] = 229;

            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");

            int userId = Convert.ToInt32(Session["userId"]);
            CompanyAccess ca = new CompanyAccess();
            StepAccess sa = new StepAccess();
            // Get company types to list
            List<CompanyType> ctList = ca.GetAllCompanyType();
            ViewBag.TypeId = new SelectList(ctList, "TypeId", "TypeName");

            //Get states to list
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            if (Convert.ToInt32(Session["companyStep"]) == 1 && edit != 1)
            {
                return View();
            }

            if (edit == 1)
            {
                if (!string.IsNullOrEmpty(Session["userId"].ToString()))
                {
                    userId = Convert.ToInt32(Session["userId"]);
                    //----------
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

                    return View(preCompany);
                }
            }

            return RedirectToAction("UserLogin", "Login");
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/26
        /// </summary>
        /// <param name="company"></param>
        /// <param name="edit"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Step1(Company company, int? edit)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");


            string type;
            if (edit != 1)
            {
                GeneratesCode gc = new GeneratesCode();
                _comCode = company.CompanyCode = gc.GenerateCompanyCode(company.CompanyName);
                type = "INSERT";
            }
            else
            {
                company.CompanyCode = _comCode;
                type = "UPDATE";
            }

            //
            company.Zip = company.ZipPre;
            if (company.Extension != null)
                company.Zip += "-" + company.Extension;

            company.CreatedBy = company.FirstSuperAdminId = Convert.ToInt32(Session["userId"]);
            company.CompanyStatus = true;
            CompanyAccess ca = new CompanyAccess();

            //check this record is new one or exitsting one
            //string type = (edit == 1) ? "UPDATE" : "INSERT";

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
            ViewBag.ErrorMsg = "Failed to Setup company.";

            // Get company types to list
            List<CompanyType> ctList = ca.GetAllCompanyType();
            ViewBag.TypeId = new SelectList(ctList, "TypeId", "TypeName");

            //Get states to list
            List<State> stateList = ca.GetAllStates();
            ViewBag.StateId = new SelectList(stateList, "StateId", "StateName");

            return View();

        }

        public ActionResult Step2()
        {
            Session["userId"] = 229;
            //Session["userId"] = 4;
            //int userId = 68;
            if ((Session["userId"] != null) && (Session["userId"].ToString() != ""))
            //if(userId>0)
            {
                int userId = (int)Session["userId"];
                StepAccess cs = new StepAccess();
                int reslt = Convert.ToInt32(Session["companyStep"]);
                if (reslt == 2)
                {
                    if ((TempData["Company"] != null) && (TempData["Company"].ToString() != ""))
                    {
                        userCompany = new CompanyBranchModel();
                        userCompany = (CompanyBranchModel)TempData["Company"];

                        CompanyType = (userCompany.Company.TypeId == 1) ? "Lender" : "Dealer";

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
            else
            {
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
            userCompany2.MainBranch.StateId = userCompany2.StateId;
            userCompany2.MainBranch.BranchCode = ba.createBranchCode(userCompany.Company.CompanyCode);
            userCompany.MainBranch = userCompany2.MainBranch;
            bool reslt = ba.insertFirstBranchDetails(userCompany, userId);
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
                ViewBag.ErrorMsg = "Failed to create first branch";
            }
            return View();
        }
    }
}