using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers
{
    public class LoanSetUpStep5Controller : Controller
    {
        private static int _createById;
        private static int _isEdit;
        //private static float totalPercentage = 0;
        private static string _timeBase;
        private static int _timePeriod;
        private static float _remaininPrecentage;

        private static LoanSetupStep1 _loan;
        private static CurtailmentModel _gCurtailment;

        // GET: LoanSetUpStep5
        public ActionResult Step5()
        {
            Session["userId"] = 14;
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return RedirectToAction("UserLogin", "Login");

            int loanId = 1;
            CurtailmentAccess curAccess = new CurtailmentAccess();
            _loan = curAccess.GetLoanDetailsByLoanId(loanId);
            
            int noOfDays = (int)(_loan.maturityDate - _loan.startDate).TotalDays;

            CurtailmentModel obj = new CurtailmentModel();
            obj.RemainingPercentage = _loan.advancePercentage;
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
            switch (submit)
            {
                case "Add Row":

                    float payPercentage = objmodel.CalculationBase == "Full payment" ? _loan.advancePercentage : 100;
                    float totalPercentage = 0;

                    int noOfDays = (int)(_loan.maturityDate - _loan.startDate).TotalDays;
                    int payTime = objmodel.TimeBase == "Month" ? noOfDays/30 : noOfDays;

                    int curId = 1;
                    for (int i = 0; i < objmodel.InfoModel.Count; i++)
                    {   Curtailment curtailment = objmodel.InfoModel[i];
                        if (i == 0)
                        {
                            if (objmodel.InfoModel[i].TimePeriod > payTime)
                            {
                                ViewBag.ErrorMsg = "Entered time period is invalid!";
                                return View(objmodel);
                            }
                        }
                        if (i >= 1)
                        {
                            if (objmodel.InfoModel[i - 1].TimePeriod >= objmodel.InfoModel[i].TimePeriod)
                            {
                                ViewBag.ErrorMsg = "Entered time period is invalid!";
                                return View(objmodel);
                            }
                        }
                        
                        totalPercentage += curtailment.Percentage;
                        curtailment.CurtailmentId = curId;
                        objmodel.RemainingTime = curtailment.TimePeriod;
                        curId++;
                    }
                    objmodel.RemainingPercentage = payPercentage - totalPercentage;

                    if (objmodel.RemainingPercentage >= 0 && objmodel.RemainingTime <= payTime)
                    {
                        if(objmodel.RemainingPercentage != 0 && objmodel.RemainingTime != payTime)
                            objmodel.InfoModel.Add(new Curtailment { CurtailmentId = objmodel.InfoModel.Count + 1});
                        ViewBag.ErrorMsg = "";
                    }
                    else if(objmodel.RemainingPercentage < 0)
                    {
                        ViewBag.ErrorMsg = "Invalid percentage's found!";
                    }
                    else if (objmodel.RemainingTime > payTime)
                    {
                        ViewBag.ErrorMsg = "Invalid time's found!";
                    }
                    break;

                case "Next":
                    TempData["objmodel"] = objmodel;
                    return RedirectToAction("Create");
                    break;
            }
            return View(objmodel);
        }

        public JsonResult CalculateTimePeriod(int timePeriod)
        {
            bool res = false;

            int noOfDays = (int)(_loan.maturityDate - _loan.startDate).TotalDays;
            if (Convert.ToInt32(timePeriod) < noOfDays) res = true;

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckPrecentage(float percentage, string cal)
        {
            //bool res = percentage < _remaininPrecentage;
            //return Json(res, JsonRequestBehavior.AllowGet);

            float payPercentage = cal == "Full Payment" ? _loan.advancePercentage : 100;
            float totalPercentage = 0;
            int curId = 1;
            foreach (Curtailment curtailment in _gCurtailment.InfoModel)
            {
                totalPercentage += curtailment.Percentage;
                curtailment.CurtailmentId = curId;
                curId++;
            }
            _remaininPrecentage = _gCurtailment.RemainingPercentage = payPercentage - totalPercentage;

            CurtailmentModel temp = new CurtailmentModel();

            if (_gCurtailment.RemainingPercentage > 0)
            {
                for (int i = 0; i < curId; i++)
                {
                    temp.InfoModel.Add(new Curtailment { CurtailmentId = i+1 });
                }
                
                //_gCurtailment.InfoModel.Add(new Curtailment { CurtailmentId = _gCurtailment.InfoModel.Count + 1 });
            }
            else
            {
                ViewBag.ErrorMsg = "Invalid percentage's found!";
            }
            return PartialView("Step5", _gCurtailment);
        }

        /// <summary>
        /// CreatedBy : Kanishka SHM
        /// CreatedDate: 2016/01/16
        /// 
        /// Check weather user name already exist
        /// 
        /// argument: userName(string)
        /// 
        /// </summary>
        /// <returns>Return JsonResult</returns>
        public JsonResult KeepCalculationBaseInfo(string calculationBase)
        {
            var res = calculationBase;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //public void KeepTimeBaseType(string timeBase)

        public JsonResult KeepTimeBaseType(string timeBase)
        {
            _timeBase = timeBase;
            return Json(true, JsonRequestBehavior.AllowGet);
        }


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
                        if (curtailment.Percentage != 0 && curtailment.TimePeriod != "")
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
                    //return PartialView();
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


        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2/09/2016
        /// </summary>
        /// <param name="curtailment"></param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult Step12(Curtailment curtailment)
        {
            if (Session["userId"] == null || Session["userId"].ToString() == "")
                return new HttpStatusCodeResult(404, "Your Session Expired");

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

            curtailment.LoanId = 1;
            //curtailment.TimePeriod = "1";
            curtailment.Percentage = 10;
            lstCurtailment.Add(curtailment);




            if (curtailmentAccess.InsertCurtailment(lstCurtailment, type) == 1)
            {
                ViewBag.SuccessMsg = "Company Successfully setup.";

                //If succeed update step table to step2 
                StepAccess sa = new StepAccess();
                sa.updateStepNumberByUserId(int.Parse(Session["userId"].ToString()), 5);

                //Send company detail to NEXT step
                //CompanyBranchModel comBranch = new CompanyBranchModel();
                //comBranch.Company = curtailment;

                //TempData["Company"] = comBranch;
                return RedirectToAction("Step2");
            }


            return new HttpStatusCodeResult(404, "Failed to Setup company.");
        }

        /// <summary>
        /// CreatedBy : Nadeeka
        /// CreatedDate: 2/09/2016
        /// 
        /// Inserting curtailment details
        /// 
        /// argument: curtailment list
        /// 
        /// </summary>
        /// <returns>Data Successfully inserted! / Failed to insert!</returns>
        //[HttpPost]
        public ActionResult Create(Curtailment curtailment)
        {
            int.Parse(Session["userId"].ToString());

            List<Curtailment> lstCurtailment = new List<Curtailment>();
            curtailment.LoanId = 1;
            //curtailment.TimePeriod = "1";
            curtailment.Percentage = 10;
            lstCurtailment.Add(curtailment);

            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();

            //Insert user
            int res = curtailmentAccess.InsertCurtailment(lstCurtailment, "1");

            if (res == 1)
            {
                ViewBag.SuccessMsg = "Data Successfully inserted!";

                return RedirectToAction("create", new { lbls = ViewBag.SuccessMsg });

            }
            else
            {
                ViewBag.ErrorMsg = "Failed to create curtailment!";
                return PartialView("Create");
            }
        }

    }
}