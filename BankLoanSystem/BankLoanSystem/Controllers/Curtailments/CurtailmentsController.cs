using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Helpers;
using BankLoanSystem.Reports;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Controllers.Curtailments
{
    public class CurtailmentsController : Controller
    {
        string lCode=string.Empty;
        User userData = new User();

        // Check session in page initial stage
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
                    if (HttpContext.Request.IsAjaxRequest())
                    {

                        //new HttpStatusCodeResult(404, "Failed to Setup company.");
                        filterContext.Result = new HttpStatusCodeResult(404, "Due to inactivity your session has timed out, please log in again.");
                    }
                    else
                    {

                        filterContext.Result = new RedirectResult("/Login/UserLogin?lbl=Due to inactivity your session has timed out, please log in again.");
                        

                    }
                }
            }
            catch
            {
                filterContext.Result = new RedirectResult("~/Exceptions/Index");
            }
        }
        
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// CreatedBy:Nadeeka
        /// CreatedDate:2016/3/21
        /// set loan code to session
        /// </summary>
        /// <param name="loanCode"></param>
        /// <returns></returns>
        public ActionResult setLoanCode(string loanCode)
        {
            Session["loanCode"] = loanCode;
            lCode = loanCode;
            return RedirectToAction("PayCurtailments");
        }

        /// <summary>
        /// CreatedBy:Nadeeka
        /// CreatedDate:2016/3/21
        /// GET: Curtailments by today date
        /// </summary>
        /// <returns></returns>
        public ActionResult PayCurtailments()
        {
            if (Session["loanCode"] == null || Session["loanCode"].ToString() == "")
                return RedirectToAction("UserLogin", "Login", new { lbl = "Failed find loan" });

            lCode = Session["loanCode"].ToString();

            if (userData.RoleId == 3)
            {
                if (Session["CurrentLoanRights"] == null || Session["CurrentLoanRights"].ToString() == "")
                {
                    return RedirectToAction("UserDetails", "UserManagement");
                }
                else {
                    var checkPermission = false;
                    string rgts = "";
                    rgts = (string)Session["CurrentLoanRights"];
                    string[] rgtList = null;
                    if (rgts != "")
                    {
                        rgtList = rgts.Split(',');
                    }
                    if (rgtList != null)
                    {
                        foreach (var x in rgtList)
                        {
                            if (x == "U05")
                            {
                                checkPermission = true;
                            }
                        }
                        if (checkPermission == false)
                        {
                            return RedirectToAction("UserDetails", "UserManagement");
                        }
                    }
                    else {
                        return RedirectToAction("UserDetails", "UserManagement");
                    }

                }
            }
            else if (userData.RoleId == 4)
            {
                return RedirectToAction("UserDetails", "UserManagement");
            }
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString());
            Session["curtLaonId"] = loanDetails.loanId;
            ViewBag.curtTotal = (new CurtailmentAccess()).GetCurtailmentsTotal(loanDetails.loanId);
            ViewBag.loanDetails = loanDetails;
            ViewBag.LoanId = loanDetails.loanId;

            return View();
        }

        /// <summary>
        /// CreatedBy:Nadeeka
        /// CreatedDate:2016/3/21
        /// GET: Curtailments by selected due date
        /// </summary>
        /// <param name="dueDate"></param>
        /// <returns></returns>
        public ActionResult PayCurtailmentForSelectedDueDate(DateTime dueDate)
        {
            CurtailmentScheduleModel curtailmentScheduleModel = new CurtailmentScheduleModel();
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString());
            ViewBag.loanDetails = loanDetails;

            string f = dueDate.ToShortDateString();
            DateTime dd = Convert.ToDateTime(f);


            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
            List<CurtailmentShedule> curtailmentSchedule = curtailmentAccess.GetCurtailmentScheduleByDueDate(loanDetails.loanId, dd);
            ViewBag.DealerEmail = (string)Session["DealerEmail"];
            ViewBag.LoanId = loanDetails.loanId;

            curtailmentScheduleModel.CurtailmentScheduleInfoModel = new List<CurtailmentShedule>();
            curtailmentScheduleModel.CurtailmentScheduleInfoModel.AddRange(curtailmentSchedule);
            curtailmentScheduleModel.DueDate = dueDate;
            return PartialView(curtailmentScheduleModel);
        }

        /// <summary>
        /// CreatedBy:Nadeeka
        /// CreatedDate:2016/3/21
        /// POST: Pay selected curtailment/s
        /// 
        /// </summary>
        /// <param name="selectedCurtailmentList"></param>
        /// <returns></returns>
        [HttpPost]
        public string PayCurtailments(SelectedCurtailmentList selectedCurtailmentList, string needSend, string dealerEmail, string dueDate)
        {
            if (Session["loanCode"] == null || Session["loanCode"].ToString() == "")
                return null;

            var loanCode = Session["loanCode"].ToString();
            string paidDate = "";

            var loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode);

            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
            string returnValue = curtailmentAccess.updateCurtailmets(selectedCurtailmentList, loanDetails.loanId, dealerEmail);

            if (returnValue != null)
            {
                // saving for reporting purpose
                decimal totalpaid = 0.00M;
                List<CurtailmentShedule> selectedCurtailmentSchedules = selectedCurtailmentList.SelectedCurtailmentSchedules;
                foreach (var items in selectedCurtailmentSchedules)
                {
                    items.PaidDate = items.PayDate.ToString("MM/dd/yyyy");
                    totalpaid += items.CurtAmount;
                    paidDate = items.PaidDate;
                }

                foreach (var items in selectedCurtailmentSchedules)
                {
                    items.TotalAmountPaid = totalpaid;
                }

                Session["CurtUnitDuringSession"] = selectedCurtailmentSchedules;
                if (needSend == "Yes")
                {
                    ReportViewer rptViewerCurtailmentReceiptDuringSession = new ReportViewer();
                    rptViewerCurtailmentReceiptDuringSession.ProcessingMode = ProcessingMode.Local;
                    rptViewerCurtailmentReceiptDuringSession.Reset();
                    rptViewerCurtailmentReceiptDuringSession.LocalReport.EnableExternalImages = true;
                    rptViewerCurtailmentReceiptDuringSession.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurtailmentDuringSession.rdlc");

                    ReportAccess ra = new ReportAccess();
                    List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanDetails.loanId, userData.UserId);

                    foreach (var dates in details)
                    {
                        dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
                    }

                    rptViewerCurtailmentReceiptDuringSession.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

                    if (selectedCurtailmentSchedules != null && selectedCurtailmentSchedules.Count > 0)
                    {
                        try
                        {
                            rptViewerCurtailmentReceiptDuringSession.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", selectedCurtailmentSchedules));

                            Warning[] warnings;
                            string[] streamids;
                            string mimeType;
                            string encoding;
                            string filenameExtension;

                            //create pdf file
                            byte[] bytes = rptViewerCurtailmentReceiptDuringSession.LocalReport.Render(
                                "PDF", null, out mimeType, out encoding, out filenameExtension,
                                out streamids, out warnings);

                            if (dealerEmail != "")
                            {
                                string mailSubject = "Curtailment Paid Receipt - Loan " + loanDetails.loanNumber;
                                string mailBody =
                                    "Curtailments for Loan " + loanDetails.loanNumber + " which were due on or before " + dueDate  + " have been paid on " + paidDate + ". " +
                                    "Please view the attached PDF file for full curtailment payment details. " +
                                    Environment.NewLine + Environment.NewLine +
                                    "Thank you," +
                                    Environment.NewLine +
                                    "Dealer Floor Plan Software Team";

                                Thread thread = new Thread(delegate ()
                                {
                                    Email email = new Email(dealerEmail);
                                    email.SendMailWithAttachment(mailSubject, mailBody, bytes);
                                });

                                thread.IsBackground = true;
                                thread.Start();

                            }

                        }
                        catch (Exception e)
                        {
                            throw e;
                        }
                    }
                }

                //insert to log
                string[] arrList = new string[selectedCurtailmentList.SelectedCurtailmentSchedules.Count];
                int i = 0;
                foreach (var x in selectedCurtailmentList.SelectedCurtailmentSchedules)
                {
                    if (!string.IsNullOrEmpty(x.UnitId))
                    {
                        arrList[i] = "Pay Curtailment(s) for unit(s): " + x.IDNumber + " ,Curtailment No: " + x.CurtNumber + " ,Curtailment Amount:" + x.CurtAmount + " ,Paid Date:" + x.PayDate;
                        i++;
                    }
                }

                //arrList = arrList.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                ////user.UserRights = arrList.ToString();
                string units = string.Join(",", arrList);
                Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, loanDetails.loanId, "Pay Curtailments", units, DateTime.Now);

                int islog = (new LogAccess()).InsertLog(log);
            }

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


        //[HttpPost]
        public FileResult PrintPage()
        {
            if (Session["curtLaonId"] == null || Session["curtLaonId"].ToString() == "")
            {
                return null;
            }

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            ReportViewer rptViewerPrint = new ReportViewer();

            int loanId = (int) Session["curtLaonId"];
            //RptDivCUrtailmentReceiptDuringSession curtThisSession = new RptDivCUrtailmentReceiptDuringSession();
            //return curtThisSession.PrintPage(loanId);


            ReportViewer rptViewerCurtailmentReceiptDuringSessionPrint = new ReportViewer();
            rptViewerCurtailmentReceiptDuringSessionPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerCurtailmentReceiptDuringSessionPrint.Reset();
            rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.EnableExternalImages = true;
            rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurtailmentDuringSession.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<CurtailmentShedule> selectedCurtailmentSchedules =
                (List<CurtailmentShedule>)Session["CurtUnitDuringSession"];

            if (selectedCurtailmentSchedules != null && selectedCurtailmentSchedules.Count > 0)
            {
                rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", selectedCurtailmentSchedules));
            }

            var bytes = rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.Render(
                "PDF", null, out mimeType, out encoding, out filenameExtension,
                out streamids, out warnings);

            var fsResult = File(bytes, "application/pdf");
            return fsResult;
        }

    }
}