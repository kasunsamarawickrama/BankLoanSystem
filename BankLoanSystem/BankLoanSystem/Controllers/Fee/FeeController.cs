using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.Fee
{
    public class FeeController : Controller
    {
        User userData = new User();
        string lCode = string.Empty;
        
       

        // GET: Fee
        public ActionResult Index()
        {
            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if (Session["AuthenticatedUser"] != null)
                {
                    userData = ((User)Session["AuthenticatedUser"]);
                    
                }
                else
                {
                    //return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
                    //filterContext.Controller.TempData.Add("UserLogin", "Login");
                    filterContext.Result = new RedirectResult("~/Login/UserLogin");
                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                filterContext.Result = new RedirectResult("~/Login/UserLogin");
            }
        }

        // GET: Curtailments
        public ActionResult PayFees()
        {
            try
            {
                lCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                return RedirectToAction("UserLogin", "Login", new { lbl = "Your Session Expired" });
            }

           
            LoanSetupStep1 _loanDetails = new LoanSetupStep1();
            _loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString());
            ViewBag.loanDetails = _loanDetails;


            Loan loanSelected = new Loan();
            if (Session["loanDashboard"] != null)
            {
                
                 loanSelected = (Loan)Session["loanDashboard"];
            }
            else if (Session["oneLoanDashboard"] != null)
            {
                
                loanSelected = (Loan)Session["loanDashboard"];
            }
            else
            {
                return RedirectToAction("Login","UserLogin");
            }
            ViewBag.loanSelected = loanSelected;

            string advDuedate;
            string monDueDate;
            string lotDuedate;
            bool returnValue = (new FeeAccess()).GetFeesDueDates(_loanDetails.loanId, out advDuedate, out monDueDate, out lotDuedate);

            
            ViewBag.advDuedate = advDuedate;
            ViewBag.monDueDate = monDueDate;
            ViewBag.lotDuedate = lotDuedate;

            //Console.WriteLine(advDuedate + "" + monDueDate + "" + lotDuedate);
                return View();
        }

        public ActionResult PayFeesForSelectedDueDate(DateTime dueDate, string type)
        {
            return PartialView(this.GetFees(dueDate, type));
        }

        public FeesModel GetFees(DateTime dueDate, string type)
        {
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString());
            ViewBag.loanDetails = loanDetails;
                       
            FeeAccess feeAccess = new FeeAccess();
            List<Fees> lstFee = feeAccess.GetFeesByDueDate(loanDetails.loanId, dueDate, type);
            FeesModel curtailmentScheduleModel = new FeesModel();
            curtailmentScheduleModel.FeeModelList = new List<Fees>();
            curtailmentScheduleModel.Type = type;


            if (lstFee != null && lstFee.Count > 0)
            {
                curtailmentScheduleModel.FeeModelList.AddRange(lstFee);
                curtailmentScheduleModel.DueDate = lstFee[0].DueDate;
            }

            return curtailmentScheduleModel;
        }

        [HttpPost]
        public int PayFees(List<Fees> lstFee)
        {
            int userId = userData.UserId;
            FeeAccess feeAccess = new FeeAccess();
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString());
            int returnValue = feeAccess.updateFees(lstFee, lstFee[0].PaidDate, loanDetails.loanId, userId);

            return returnValue;
        }

        /// <summary>
        /// CreatedBy:Nadeeka
        /// CreatedDate:2016/4/25
        /// Search Fees
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="vehicleModel"></param>
        /// <returns></returns>
        public ActionResult SearchFee(string identificationNumber, string year, string make, string vehicleModel, FeesModel CurtailmentList)
        {
            List<Fees> SearchList = new List<Fees>();

            if (((!string.IsNullOrEmpty(identificationNumber)) || (!string.IsNullOrEmpty(year)) || (!string.IsNullOrEmpty(make)) || (!string.IsNullOrEmpty(vehicleModel))))
            {
                //search through list elements
                Search sc = new Search();

                SearchList = sc.GetSearchFeeList(CurtailmentList, identificationNumber.Trim().ToLower(), year.Trim().ToLower(), make.Trim().ToLower(), vehicleModel.Trim().ToLower());

                return PartialView(SearchList);
            }
            else
            {

                return PartialView(SearchList);
            }
        }

    }
}