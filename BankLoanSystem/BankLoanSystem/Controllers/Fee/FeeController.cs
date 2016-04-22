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

            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString());
            ViewBag.loanDetails = loanDetails;

            return View();
        }

        public ActionResult PayFeesForSelectedDueDate(DateTime dueDate, string type)
        {
            return PartialView(this.GetFees(dueDate, type));
        }

        private FeesModel GetFees(DateTime dueDate, string type)
        {
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString());
            ViewBag.loanDetails = loanDetails;
                       
            LoanSetupAccess curtailmentAccess = new LoanSetupAccess();
            List<Fees> lstFee = curtailmentAccess.GetFeesByDueDate(loanDetails.loanId, dueDate, type);
            FeesModel curtailmentScheduleModel = new FeesModel();
            curtailmentScheduleModel.FeeModelList = new List<Fees>();
            
            if (lstFee != null && lstFee.Count > 0)
            {
                curtailmentScheduleModel.FeeModelList.AddRange(lstFee);
                curtailmentScheduleModel.DueDate = lstFee[0].DueDate;
            }

            return curtailmentScheduleModel;
        }
    }
}