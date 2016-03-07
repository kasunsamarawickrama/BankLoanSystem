using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;

namespace BankLoanSystem.Controllers
{
    public class SetupProcessTestController : Controller
    {
        private static CompanyBranchModel userCompany = null;
        private static CompanyBranchModel userNonRegCompany = null;
        private static string CompanyType = "Lender";
        private static string _comCode;

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

            if (sa.getStepNumberByUserId(userId) == 1 && edit != 1)
            {
                return View();
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
                int reslt = cs.getStepNumberByUserId(userId);
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
            int reslt = ba.insertFirstBranchDetails(userCompany, userId);
            if (reslt>0)
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