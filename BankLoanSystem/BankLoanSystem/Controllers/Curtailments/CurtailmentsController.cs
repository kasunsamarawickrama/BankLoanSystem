using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Helpers;

namespace BankLoanSystem.Controllers.Curtailments
{
    public class CurtailmentsController : Controller
    {
        string lCode=string.Empty;
        private static LoanSetupStep1 loan;
        User userData = new User();
        // Check session in page initia stage
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
                    filterContext.Controller.TempData.Add("UserLogin", "Login");
                }
            }
            catch
            {
                //filterContext.Result = new RedirectResult("~/Login/UserLogin");
                filterContext.Controller.TempData.Add("UserLogin", "Login");
            }
        }


        // GET: Curtailments
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult setLoanCode(string loanCode)
        {
            Session["loanCode"] = loanCode;
            lCode = loanCode;
            return RedirectToAction("PayCurtailments");
        }

        // GET: Curtailments
        public ActionResult PayCurtailments()
        {
            try
            {
                lCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                return new HttpStatusCodeResult(404, "Session Expired");
            }
            
           
            
            return View();
        }

        public ActionResult PayCurtailmentForSelectedDueDate(DateTime dueDate)
        {
            return PartialView(this.GetCurtailmentSchedule(dueDate));
        }

        private CurtailmentScheduleModel GetCurtailmentSchedule(DateTime dueDate)
        {
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString());
            ViewBag.loanDetails = loanDetails;

            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
            List<CurtailmentShedule> curtailmentSchedule = curtailmentAccess.GetCurtailmentScheduleByDueDate(loanDetails.loanId, dueDate);
            CurtailmentScheduleModel curtailmentScheduleModel = new CurtailmentScheduleModel();
            curtailmentScheduleModel.CurtailmentScheduleInfoModel = new List<CurtailmentShedule>();
            curtailmentScheduleModel.CurtailmentScheduleInfoModel.AddRange(curtailmentSchedule);
            curtailmentScheduleModel.DueDate = dueDate;

            return curtailmentScheduleModel;
        }







        [HttpPost]
        public string PayCurtailments(SelectedCurtailmentList selectedCurtailmentList)
        {


            //dynamic stuff = JsonConvert.DeserializeObject(Request["SelectedCurtailmentScheduleInfoModel"]);

            //dynamic obj = serializer.Deserialize(, typeof(object));
            int userId = userData.UserId;
            string loanCode;
            
            try
            {
                loanCode = Session["loanCode"].ToString();
            }
            catch (Exception)
            {
                //filterContext.Controller.TempData.Add("UserLogin", "Login");
                return null;
            }


            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);


            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
            string returnValue  = curtailmentAccess.updateCurtailmets(selectedCurtailmentList, loanDetails.loanId);
            
            return returnValue;
        }

        /// <summary>
        /// CreatedBy:Irfan
        /// CreatedDate:2016/3/18
        /// Search curtailments
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="vehicleModel"></param>
        /// <returns></returns>
        public ActionResult SearchCurtailment(string identificationNumber, string year, string make, string vehicleModel , CurtailmentScheduleModel CurtailmentList)
        {



            List<CurtailmentShedule> SearchList = new List<CurtailmentShedule>();
            
            
            if (((!string.IsNullOrEmpty(identificationNumber)) || (!string.IsNullOrEmpty(year)) || (!string.IsNullOrEmpty(make)) || (!string.IsNullOrEmpty(vehicleModel))))
            {
                //search through list elements
                Search sc = new Search();

                SearchList = sc.GetSearchCurtailmentList(CurtailmentList, identificationNumber.Trim().ToLower(), year.Trim().ToLower(), make.Trim().ToLower(), vehicleModel.Trim().ToLower() );

                return PartialView(SearchList);
            }
            else
            {
                
                return PartialView(SearchList);
            }
        }

        
    }
}