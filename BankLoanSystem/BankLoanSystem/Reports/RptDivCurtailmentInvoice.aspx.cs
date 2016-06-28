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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;

                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);
                
                if (string.IsNullOrEmpty(Request.QueryString["startDate"])) return;
                var startDate = DateTime.ParseExact(Request.QueryString["startDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                if (string.IsNullOrEmpty(Request.QueryString["endDate"])) return;
                var endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                RenderReport(loanId, startDate, endDate);
            }
        }

        /*

            Frontend page: Report Page
            Title: Load details to report and show on browser
            Designed: Kanishka SHM
            User story: 
            Developed: Kanishka SHM
            Date created: 

        */
        public void RenderReport(int loanId, DateTime startDate, DateTime endDate)
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

            //Get curtailment details by given time period
            List<CurtailmentShedule> curtailments = ra.GetCurtailmentScheduleByDateRange(loanId, startDate, endDate);

            //set data source to report viwer
            rptViewerCurtailmentInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerCurtailmentInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));
        }

        /*

            Frontend page: Report Page
            Title: Load pdf view on browser
            Designed: Kanishka SHM
            User story: 
            Developed: Kanishka SHM
            Date created: 

        */
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

            //Get curtailment details by given time period
            List<CurtailmentShedule> curtailments = ra.GetCurtailmentScheduleByDateRange(loanId, startDate, endDate);

            //set data source to report viwer
            rptViewerCurtailmentInvoicePrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerCurtailmentInvoicePrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));

            //return report viwer
            return rptViewerCurtailmentInvoicePrint;
        }

    }
}