using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankLoanSystem.Controllers.Curtailments
{
    public class CurtailmentsController : Controller
    {
        string lCode=string.Empty;
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
            
           
            
            return View(this.GetCurtailmentSchedule(DateTime.Now));
        }

        public ActionResult PayCurtailmentForSelectedDueDate(DateTime dueDate)
        {
            return PartialView(this.GetCurtailmentSchedule(dueDate));
        }

        private CurtailmentScheduleModel GetCurtailmentSchedule(DateTime dueDate)
        {
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(lCode);
            ViewBag.loanDetails = loanDetails;

            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
            List<CurtailmentShedule> curtailmentSchedule = curtailmentAccess.GetCurtailmentScheduleByDueDate(loanDetails.loanId, dueDate);
            CurtailmentScheduleModel curtailmentScheduleModel = new CurtailmentScheduleModel();
            curtailmentScheduleModel.CurtailmentScheduleInfoModel = new List<CurtailmentShedule>();
            curtailmentScheduleModel.CurtailmentScheduleInfoModel.AddRange(curtailmentSchedule);
            curtailmentScheduleModel.DueDate = dueDate;

            return curtailmentScheduleModel;
        }
    }
}