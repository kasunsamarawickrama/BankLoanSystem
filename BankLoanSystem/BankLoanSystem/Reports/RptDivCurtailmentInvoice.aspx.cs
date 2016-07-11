using System;
using System.Collections.Generic;
using System.Globalization;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivCurtailmentInvoice : System.Web.UI.Page
    {
        /// <summary>
        /// Frontend Page: Report Page(Curtailment Invoice Report)
        /// Title: Display Curtailment Invoice details
        /// Designed: Kanishka SHM
        /// User story: 
        /// Developed: Kanishka SHM
        /// Date created: 
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;

                //if session is not null and assign it.
                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);
                
                if (string.IsNullOrEmpty(Request.QueryString["startDate"])) return;
                var startDate = DateTime.ParseExact(Request.QueryString["startDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                if (string.IsNullOrEmpty(Request.QueryString["endDate"])) return;
                var endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                //Get all active units curtailment details during time period
                ReportAccess ra = new ReportAccess();
                List<CurtailmentShedule> curtailments = ra.GetCurtailmentScheduleByDateRange(loanId, startDate, endDate);

                //if result count is greater then zero show report, otherwise give a message 
                if (curtailments.Count > 0)
                {
                    //call RenderReport function to show report on report viwer
                    RenderReport(loanId, startDate, endDate, curtailments);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowFrame", "ShowDive();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideFrame", "HideDive();", true);
                }
            }
        }

        /// <summary>
        /// Frontend Page: Report Page(Curtailment Invoice Report)
        /// Title: Display Curtailment Invoice details report
        /// Designed: Kanishka SHM
        /// User story: 
        /// Developed: Kanishka SHM
        /// Date created: 
        /// </summary>
        public void RenderReport(int loanId, DateTime startDate, DateTime endDate, List<CurtailmentShedule> curtailments)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            rptViewerCurtailmentInvoice.ProcessingMode = ProcessingMode.Local;
            rptViewerCurtailmentInvoice.Reset();
            rptViewerCurtailmentInvoice.LocalReport.EnableExternalImages = true;
            rptViewerCurtailmentInvoice.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurtailmentInvoice.rdlc");
            rptViewerCurtailmentInvoice.ZoomMode = ZoomMode.PageWidth;

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            //add dates, date range and current date
            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //set data source to report viwer
            rptViewerCurtailmentInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerCurtailmentInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));
        }

        /// <summary>
        /// Frontend Page: Report Page(Curtailment Invoice Report)
        /// Title: Display Curtailment Invoice details print page
        /// Designed: Kanishka SHM
        /// User story: 
        /// Developed: Kanishka SHM
        /// Date created: 
        /// </summary>
        public ReportViewer PrintPage(int loanId, DateTime startDate, DateTime endDate)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            ReportViewer rptViewerCurtailmentInvoicePrint = new ReportViewer();
            rptViewerCurtailmentInvoicePrint.ProcessingMode = ProcessingMode.Local;
            rptViewerCurtailmentInvoicePrint.Reset();
            rptViewerCurtailmentInvoicePrint.LocalReport.EnableExternalImages = true;
            rptViewerCurtailmentInvoicePrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurtailmentInvoice.rdlc");

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            //add dates, date range and current date
            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Get all active units curtailment details during time period
            List<CurtailmentShedule> curtailments = ra.GetCurtailmentScheduleByDateRange(loanId, startDate, endDate);

            //set data source to report viwer
            rptViewerCurtailmentInvoicePrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerCurtailmentInvoicePrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));

            //return report viwer
            return rptViewerCurtailmentInvoicePrint;
        }

    }
}