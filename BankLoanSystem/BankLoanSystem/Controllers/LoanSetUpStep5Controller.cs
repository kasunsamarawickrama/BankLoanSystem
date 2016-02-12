using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers
{
    public class LoanSetUpStep5Controller : Controller
    {
        private static int _isEdit;
        private static float _remaininPrecentage;

        private static LoanSetupStep1 _loan;
        private static CurtailmentModel _gCurtailment;

        // GET: LoanSetUpStep5
        public ActionResult Step5()
        {
            Session["userId"] = 1;
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");
            int userId = Convert.ToInt32(Session["userId"]);
            LoanSetupAccess la = new LoanSetupAccess();
           
            CurtailmentAccess curAccess = new CurtailmentAccess();

            int loanId = la.getLoanIdByUserId(userId);           
            _loan = curAccess.GetLoanDetailsByLoanId(loanId);
            _loan.loanId = loanId;

            int noOfDays = (int)(_loan.maturityDate - _loan.startDate).TotalDays;

            CurtailmentModel obj = new CurtailmentModel();
            obj.RemainingPercentage = _loan.advancePercentage;
            _remaininPrecentage = _loan.advancePercentage;
            obj.InfoModel = new List<Curtailment>();

            obj.InfoModel.Add(new Curtailment { CurtailmentId = 1 });
            ViewData["objmodel"] = obj;
            _gCurtailment = obj;
            return View(obj);
        }

        [HttpPost]
        public ActionResult Step5(CurtailmentModel objmodel, string submit)
        {

            //CalculationBase is true - Full payment
            //TimeBase is true - Month
            float payPercentage = objmodel.CalculationBase == "Full payment" ? _loan.advancePercentage : 100;
            float totalPercentage = 0;

            Session["userId"] = 1;
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");

            int userId = Convert.ToInt32(Session["userId"]);

            int noOfDays = (int)(_loan.maturityDate - _loan.startDate).TotalDays;
            int payTime = objmodel.TimeBase == "Month" ? noOfDays / 30 : noOfDays;
            int curId = 1;
            switch (submit)
            {
                case "+":

                    for (int i = 0; i < objmodel.InfoModel.Count; i++)
                    {
                        Curtailment curtailment = objmodel.InfoModel[i];
                        if (i == 0)
                        {
                            if (objmodel.InfoModel[i].TimePeriod > payTime)
                            {
                                ViewBag.ErrorMsg = "Entered time period is invalid!";
                                return View(objmodel);
                            }
                        }

                        totalPercentage += curtailment.Percentage;
                        curtailment.CurtailmentId = curId;
                        objmodel.RemainingTime = curtailment.TimePeriod;
                        objmodel.RemainingPercentage = payPercentage - totalPercentage;
                        curId++;

                        if (i >= 1)
                        {
                            if (objmodel.InfoModel[i - 1].TimePeriod >= objmodel.InfoModel[i].TimePeriod)
                            {
                                ViewBag.ErrorMsg = "Entered time period is invalid!";
                                return View(objmodel);
                            }
                        }


                    }
                    objmodel.RemainingPercentage = payPercentage - totalPercentage;

                    if (objmodel.RemainingPercentage >= 0 && objmodel.RemainingTime <= payTime)
                    {
                        if (objmodel.RemainingPercentage != 0 && objmodel.RemainingTime != payTime)
                            objmodel.InfoModel.Add(new Curtailment { CurtailmentId = objmodel.InfoModel.Count + 1 });
                        ViewBag.ErrorMsg = "";
                    }
                    else if (objmodel.RemainingPercentage < 0)
                    {
                        ViewBag.ErrorMsg = "Invalid percentage's found!";
                    }
                    else if (objmodel.RemainingTime > payTime)
                    {
                        ViewBag.ErrorMsg = "Invalid time's found!";
                    }
                    break;

                case "-":
                    CurtailmentModel newObjmodel = new CurtailmentModel();
                    newObjmodel.InfoModel = new List<Curtailment>();
                    for (int i = 0; i < objmodel.InfoModel.Count - 1; i++)
                    {
                        Curtailment curtailment = objmodel.InfoModel[i];
                        curtailment.CurtailmentId = (i + 1);
                        newObjmodel.InfoModel.Add(curtailment);
                        totalPercentage += curtailment.Percentage;
                    }
                    newObjmodel.RemainingPercentage = payPercentage - totalPercentage;
                    if (objmodel.InfoModel.Count > 1)
                        return View(newObjmodel);
                    else
                    {
                        objmodel.InfoModel[0].CurtailmentId = 1;
                        objmodel.RemainingPercentage = payPercentage;
                        return View(objmodel);
                    }

                case "Next":
                    if (objmodel.TimeBase == "Month")
                    {
                        
                    }
                    for (int i = 0; i < objmodel.InfoModel.Count; i++)
                    {
                        Curtailment curtailment = objmodel.InfoModel[i];
                        if (i == 0)
                        {
                            if (objmodel.InfoModel[i].TimePeriod > payTime)
                            {
                                ViewBag.ErrorMsg = "Entered time period is invalid!";
                                return View(objmodel);
                            }
                        }

                        totalPercentage += curtailment.Percentage;
                        curtailment.CurtailmentId = curId;
                        objmodel.RemainingTime = curtailment.TimePeriod;
                        objmodel.RemainingPercentage = payPercentage - totalPercentage;
                        curId++;

                        if (i >= 1)
                        {
                            if (objmodel.InfoModel[i - 1].TimePeriod >= objmodel.InfoModel[i].TimePeriod)
                            {
                                ViewBag.ErrorMsg = "Entered time period is invalid!";
                                return View(objmodel);
                            }
                        }


                    }
                    objmodel.RemainingPercentage = payPercentage - totalPercentage;

                    if (objmodel.RemainingPercentage >= 0 && objmodel.RemainingTime <= payTime)
                    {
                        if (objmodel.RemainingPercentage != 0 && objmodel.RemainingTime != payTime)
                            objmodel.InfoModel.Add(new Curtailment { CurtailmentId = objmodel.InfoModel.Count + 1 });
                        ViewBag.ErrorMsg = "";
                    }
                    else if (objmodel.RemainingPercentage < 0)
                    {
                        ViewBag.ErrorMsg = "Invalid percentage's found!";
                    }
                    else if (objmodel.RemainingTime > payTime)
                    {
                        ViewBag.ErrorMsg = "Invalid time's found!";
                    }

                    if (ViewBag.ErrorMsg == "" && objmodel.RemainingPercentage == 0)
                    {
                        CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
                        //LoanSetupAccess la = new LoanSetupAccess();
                        //int loanId = la.getLoanIdByUserId(userId);
                        List<Curtailment> lstCurtailment = new List<Curtailment>();
                        foreach (Curtailment curtailment in objmodel.InfoModel)
                        {
                            if (curtailment.Percentage != 0 && curtailment.TimePeriod != 0)
                            {
                                curtailment.LoanId = _loan.loanId;
                                lstCurtailment.Add(curtailment);
                            }
                        }

                        if (curtailmentAccess.InsertCurtailment(lstCurtailment, "I") == 1)
                        {
                            ViewBag.SuccessMsg = "Curtailment Details added successfully";
                            StepAccess stepAccess = new StepAccess();
                            stepAccess.updateStepNumberByUserId(userId, 11, _loan.loanId);
                        }
                        else
                        {
                            ViewBag.SuccessMsg = "Curtailment Details updated successfully";
                        }
                       
                    }
                    break;
            }
            return View(objmodel);
        }

        //[HttpPost]
        //public ActionResult Step5(CurtailmentModel objmodel, string submit)
        //{
        //    Session["userId"] = 1;
        //    if (Session["userId"] == null || Session["userId"].ToString() == "")
        //        return RedirectToAction("UserLogin", "Login");

        //    int userId = Convert.ToInt32(Session["userId"]);
        //    float payPercentage = objmodel.CalculationBase == "Full payment" ? _loan.advancePercentage : 100;
        //    float totalPercentage = 0;

        //    int noOfDays = (int)(_loan.maturityDate - _loan.startDate).TotalDays;
        //    int payTime = objmodel.TimeBase == "Month" ? noOfDays / 30 : noOfDays;

        //    int curId = 1;
        //    //CalculationBase is true - Full payment
        //    //TimeBase is true - Month
        //    switch (submit)
        //    {
        //        #region Add New Row
        //        case "Add Row":
        //            for (int i = 0; i < objmodel.InfoModel.Count; i++)
        //            {

        //                Curtailment curtailment = objmodel.InfoModel[i];
        //                if (i == 0)
        //                {
        //                    if (objmodel.InfoModel[i].TimePeriod > payTime)
        //                    {
        //                        ViewBag.ErrorMsg = "Entered time period is invalid!";
        //                        objmodel.RemainingPercentage = _remaininPrecentage;
        //                        return View(objmodel);
        //                    }
        //                }
        //                if (i >= 1)
        //                {
        //                    if (objmodel.InfoModel[i - 1].TimePeriod >= objmodel.InfoModel[i].TimePeriod)
        //                    {
        //                        ViewBag.ErrorMsg = "Entered time period is invalid!";
        //                        objmodel.RemainingPercentage = _remaininPrecentage;
        //                        return View(objmodel);
        //                    }
        //                }

        //                totalPercentage += curtailment.Percentage;
        //                curtailment.CurtailmentId = curId;
        //                objmodel.RemainingTime = curtailment.TimePeriod;
        //                curId++;
        //            }
        //            objmodel.RemainingPercentage = payPercentage - totalPercentage;
        //            _remaininPrecentage = objmodel.RemainingPercentage;
        //            if (objmodel.RemainingPercentage >= 0 && objmodel.RemainingTime <= payTime)
        //            {
        //                if(objmodel.RemainingPercentage != 0 && objmodel.RemainingTime != payTime)
        //                    objmodel.InfoModel.Add(new Curtailment { CurtailmentId = objmodel.InfoModel.Count + 1});
        //                ViewBag.ErrorMsg = "";
        //            }
        //            else if(objmodel.RemainingPercentage < 0)
        //            {
        //                ViewBag.ErrorMsg = "Invalid percentage's found!";
        //            }
        //            else if (objmodel.RemainingTime > payTime)
        //            {
        //                ViewBag.ErrorMsg = "Invalid time's found!";
        //            }
        //            break;
        //        #endregion

        //        #region Save/Update Curtailment
        //        case "Next":                     
        //            for (int i = 0; i < objmodel.InfoModel.Count; i++)
        //            {
        //                Curtailment curtailment = objmodel.InfoModel[i];
        //                if (i == 0)
        //                {
        //                    if (objmodel.InfoModel[i].TimePeriod > payTime)
        //                    {
        //                        ViewBag.ErrorMsg = "Entered time period is invalid!";
        //                        objmodel.RemainingPercentage = _remaininPrecentage;
        //                        return View(objmodel);
        //                    }
        //                }
        //                if (i >= 1)
        //                {
        //                    if (objmodel.InfoModel[i - 1].TimePeriod >= objmodel.InfoModel[i].TimePeriod)
        //                    {
        //                        ViewBag.ErrorMsg = "Entered time period is invalid!";
        //                        objmodel.RemainingPercentage = _remaininPrecentage;
        //                        return View(objmodel);
        //                    }
        //                }

        //                totalPercentage += curtailment.Percentage;
        //                curtailment.CurtailmentId = curId;
        //                objmodel.RemainingTime = curtailment.TimePeriod;
        //                curId++;
        //            }
        //            objmodel.RemainingPercentage = payPercentage - totalPercentage;
        //            _remaininPrecentage = objmodel.RemainingPercentage;
        //            if (objmodel.RemainingPercentage >= 0 && objmodel.RemainingTime <= payTime)
        //            {
        //                if (objmodel.RemainingPercentage != 0 && objmodel.RemainingTime != payTime)
        //                    objmodel.InfoModel.Add(new Curtailment { CurtailmentId = objmodel.InfoModel.Count + 1 });
        //                ViewBag.ErrorMsg = "";
        //            }
        //            else if (objmodel.RemainingPercentage < 0)
        //            {
        //                ViewBag.ErrorMsg = "Invalid percentage's found!";
        //            }
        //            else if (objmodel.RemainingTime > payTime)
        //            {
        //                ViewBag.ErrorMsg = "Invalid time's found!";
        //            }

        //            if (ViewBag.ErrorMsg == "" && objmodel.RemainingPercentage == 0)
        //            {
        //                CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
        //                //LoanSetupAccess la = new LoanSetupAccess();
        //                //int loanId = la.getLoanIdByUserId(userId);
        //                List<Curtailment> lstCurtailment = new List<Curtailment>();
        //                foreach (Curtailment curtailment in objmodel.InfoModel)
        //                {
        //                    if (curtailment.Percentage != 0 && curtailment.TimePeriod != 0)
        //                    {
        //                        curtailment.LoanId = _loan.loanId;
        //                        lstCurtailment.Add(curtailment);
        //                    }
        //                }

        //                if (curtailmentAccess.InsertCurtailment(lstCurtailment, "I") == 1)
        //                {
        //                    ViewBag.SuccessMsg = "Curtailment Details added successfully";
        //                    StepAccess stepAccess = new StepAccess();
        //                    stepAccess.updateStepNumberByUserId(userId, 11, _loan.loanId);                           
        //                }
        //                else
        //                {
        //                    ViewBag.SuccessMsg = "Curtailment Details updated successfully";
        //                }
        //            }
        //            break;
        //            #endregion

        //    }
        //    return View(objmodel);
        //}

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2/09/2016
        /// Add curtailment schedule for loan
        /// in the setup proccess
        /// </summary>
        /// <returns></returns>
        public ActionResult Create(int? edit)
        {
            if ((TempData["objmodel"] != null) && (TempData["objmodel"].ToString() != ""))
            {
                CurtailmentModel curtailmentModel = new CurtailmentModel();

                curtailmentModel = (CurtailmentModel)TempData["objmodel"];

                Session["userId"] = 2;
                if (Session["userId"] == null || Session["userId"].ToString() == "")
                    return RedirectToAction("UserLogin", "Login");

                int userId = Convert.ToInt32(Session["userId"]);

                StepAccess sa = new StepAccess();
                if (sa.getStepNumberByUserId(userId) >= 1 && edit != 1)
                {
                    string type;
                    if (_isEdit != 1)
                    {
                        type = "INSERT";
                    }
                    else
                    {
                        type = "UPDATE";
                        _isEdit = 0;
                    }

                    CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
                    // Get company types to list
                    List<Curtailment> lstCurtailment = new List<Curtailment>();
                    foreach (Curtailment curtailment in curtailmentModel.InfoModel)
                    {
                        if (curtailment.Percentage != 0 && curtailment.TimePeriod != 0)
                        {
                            curtailment.LoanId = 1;
                            lstCurtailment.Add(curtailment);
                        }
                    }

                    if (curtailmentAccess.InsertCurtailment(lstCurtailment, type) == 1)
                    {
                        ViewBag.SuccessMsg = "Curtailment Details added successfully";

                        //If succeed update step table to step2 
                        //StepAccess sa = new StepAccess();
                        //sa.updateStepNumberByUserId(int.Parse(Session["userId"].ToString()), 5);

                        //Send company detail to NEXT step
                        //CompanyBranchModel comBranch = new CompanyBranchModel();
                        //comBranch.Company = curtailment;

                        //TempData["Company"] = comBranch;
                        //return View(curtailmentModel);
                    }
                    else
                    {
                        ViewBag.SuccessMsg = "Curtailment Details updated successfully";
                    }
                    return PartialView("Step5", curtailmentModel);
                }

                if (edit == 1)
                {
                    if (!string.IsNullOrEmpty(Session["userId"].ToString()))
                    {
                        userId = Convert.ToInt32(Session["userId"]);
                        Company preCompany = new Company();//ca.GetCompanyDetailsByFirstSpUserId(userId);

                        //string[] zipWithExtention = preCompany.Zip.Split('-');

                        //if (zipWithExtention[0] != null) preCompany.ZipPre = zipWithExtention[0];
                        //if (zipWithExtention.Count() >= 2 && zipWithExtention[1] != null) preCompany.Extension = zipWithExtention[1];

                        //_comCode = preCompany.CompanyCode;
                        // ViewBag.Edit = "Yes";
                        //_isEdit = 1;

                        return PartialView(preCompany);
                    }
                }
            }
            return RedirectToAction("UserLogin", "Login");
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2/09/2016
        /// </summary>
        /// <param name="curtailment"></param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult Fetch()
        {

            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return new HttpStatusCodeResult(404, "Your Session Expired");

            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
            // Get company types to list
            List<Curtailment> lstCurtailment = curtailmentAccess.retreiveCurtailmentByLoanId(1);

            CurtailmentModel obj = new CurtailmentModel();
            obj.InfoModel = lstCurtailment;
            ViewData["objmodel"] = obj;
            return View(obj);

        }       
        
    }
}