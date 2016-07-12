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


        /*

   Frontend page: Dashboard Page
   Title: set loan code to session and redirect to curtailment page
   Designed: Nadeeka
   User story: 
   Developed: Nadeeka
   Date created: 3/21/2016

*/

        public ActionResult setLoanCode(string loanCode)
        {
            Session["loanCode"] = loanCode;
            lCode = loanCode;
            return RedirectToAction("PayCurtailments");
        }


        /*

  Frontend page: Curtailment Page
  Title: Load curtailment page
  Designed: Nadeeka
  User story: 
  Developed: Nadeeka
  Date created: 3/21/2016

*/
        public ActionResult PayCurtailments()
        {

            // if user access via url -- wrong access
            if (Session["loanCode"] == null || Session["loanCode"].ToString() == "")
                return RedirectToAction("UserLogin", "Login", new { lbl = "Failed find loan" });

            lCode = Session["loanCode"].ToString(); // take the loan code


            // if user is a normal user, check his user rights
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

            // if user is a dealer user -- he is not allowed
            else if (userData.RoleId == 4)
            {
                return RedirectToAction("UserDetails", "UserManagement");
            }


            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString()); // take the loan details
            Session["curtLaonId"] = loanDetails.loanId;  // save the loan id on session
            ViewBag.curtTotal = (new CurtailmentAccess()).GetCurtailmentsTotal(loanDetails.loanId); // pass the total amount of curtailment to the view

            // pass the loan details to the view
            ViewBag.loanDetails = loanDetails;
            ViewBag.LoanId = loanDetails.loanId;

            // return pay curtailment page
            return View();
        }


        /*

 Frontend page: Curtailment Page
 Title: Curtailments by selected due date
 Designed: Nadeeka
 User story: 
 Developed: Nadeeka
 Date created: 3/21/2016

*/
        public ActionResult PayCurtailmentForSelectedDueDate(DateTime dueDate)
        {
            CurtailmentScheduleModel curtailmentScheduleModel = new CurtailmentScheduleModel();
            LoanSetupStep1 loanDetails = new LoanSetupStep1();
            loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(Session["loanCode"].ToString()); // take loan details of selected loan
            ViewBag.loanDetails = loanDetails;

            string f = dueDate.ToShortDateString();
            DateTime dd = Convert.ToDateTime(f);


            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
            List<CurtailmentShedule> curtailmentSchedule = curtailmentAccess.GetCurtailmentScheduleByDueDate(loanDetails.loanId, dd);  // get curtailment list by due date
            ViewBag.DealerEmail = (string)Session["DealerEmail"]; // pass dealer email to the view
            ViewBag.LoanId = loanDetails.loanId;  // pass loan id to the view

            curtailmentScheduleModel.CurtailmentScheduleInfoModel = new List<CurtailmentShedule>();
            curtailmentScheduleModel.CurtailmentScheduleInfoModel.AddRange(curtailmentSchedule); // bind the list of curtailment to model
            curtailmentScheduleModel.DueDate = dueDate; // bind the due date to model

            // return the partial view page
            return PartialView(curtailmentScheduleModel);
        }


        /*

Frontend page: Curtailment Page
Title: Pay selected curtailment/s
Designed: Nadeeka
User story: 
Developed: Nadeeka
Date created: 3/21/2016

*/
        [HttpPost]
        public string PayCurtailments(SelectedCurtailmentList selectedCurtailmentList, string needSend, string dealerEmail, string dueDate)
        {
            // if session expired -- return null 
            if (Session["loanCode"] == null || Session["loanCode"].ToString() == "")
                return null;

            var loanCode = Session["loanCode"].ToString(); // take loan code from session 
            string paidDate = "";

            var loanDetails = (new LoanSetupAccess()).GetLoanDetailsByLoanCode(loanCode); // take loan details of the loan code

            CurtailmentAccess curtailmentAccess = new CurtailmentAccess();
            string returnValue = curtailmentAccess.updateCurtailmets(selectedCurtailmentList, loanDetails.loanId, dealerEmail); // update curtailment details as paid


            // if curtailment successfully updated
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
                // add all paid curtailment details to array
                foreach (var x in selectedCurtailmentList.SelectedCurtailmentSchedules)
                {
                    if (!string.IsNullOrEmpty(x.UnitId))
                    {
                        arrList[i] = "Pay Curtailment(s) for unit(s): " + x.IDNumber + " ,Curtailment No: " + x.CurtNumber + " ,Curtailment Amount:" + x.CurtAmount + " ,Paid Date:" + x.PayDate;
                        i++;
                    }
                }

                // join the array with comma seperated
                string units = string.Join(",", arrList);
                Log log = new Log(userData.UserId, userData.Company_Id, userData.BranchId, loanDetails.loanId, "Pay Curtailments", units, DateTime.Now);
                // insert into log
                int islog = (new LogAccess()).InsertLog(log);
            }
            // return the value
            return returnValue;
        }


        /*

   Frontend page: Curtailment Page
   Title: Searching curtailment details on curtailment list
   Designed: Irfan Mam
   User story: 
   Developed: Irfan MAM
   Date created: 3/18/2016

*/ 
        public ActionResult SearchCurtailment(string identificationNumber, string year, string make, string vehicleModel , CurtailmentScheduleModel CurtailmentList)
        {
            List<CurtailmentShedule> SearchList = new List<CurtailmentShedule>();
            
            // if atleast one arg has the value, do search
            if (((!string.IsNullOrEmpty(identificationNumber)) || (!string.IsNullOrEmpty(year)) || (!string.IsNullOrEmpty(make)) || (!string.IsNullOrEmpty(vehicleModel))))
            {
                
                Search sc = new Search();  // search object

                //search through list of elements
                SearchList = sc.GetSearchCurtailmentList(CurtailmentList, identificationNumber.Trim().ToLower(), year.Trim().ToLower(), make.Trim().ToLower(), vehicleModel.Trim().ToLower() );


                // return the partial page of searched elements
                return PartialView(SearchList);
            }
            else
            {
                // return the partial page of empty elements
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