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
            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(lCode);
            List<CurtailmentShedule> curtailmentSchedule = curtailmentAccess.GetCurtailmentScheduleByDueDate(loanDetails.loanId,DateTime.Now);
            CurtailmentScheduleModel curtailmentScheduleModel = new CurtailmentScheduleModel();
            curtailmentScheduleModel.CurtailmentScheduleInfoModel = new List<CurtailmentShedule>();
            curtailmentScheduleModel.CurtailmentScheduleInfoModel.AddRange(curtailmentSchedule);
            return View(curtailmentScheduleModel);
        }
    }
}