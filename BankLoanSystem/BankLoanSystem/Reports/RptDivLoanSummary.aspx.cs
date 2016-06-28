using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivLoanSummary : System.Web.UI.Page
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

        public void RenderReport(int loanId, DateTime startDate, DateTime endDate)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            rptViewerLoanSummary.ProcessingMode = ProcessingMode.Local;
            rptViewerLoanSummary.Reset();
            rptViewerLoanSummary.LocalReport.EnableExternalImages = true;
            rptViewerLoanSummary.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLoanSummary.rdlc");
            rptViewerLoanSummary.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerLoanSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<RptLoanSummary> loanSummaryList = ra.GetLoanSummaryReport(loanId, startDate, endDate);

            rptViewerLoanSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", loanSummaryList));
        }

        public ReportViewer PrintPage(int loanId, DateTime startDate, DateTime endDate)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            ReportViewer rptViewerLoanSummaryPrint = new ReportViewer();
            rptViewerLoanSummaryPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerLoanSummaryPrint.Reset();
            rptViewerLoanSummaryPrint.LocalReport.EnableExternalImages = true;
            rptViewerLoanSummaryPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLoanSummary.rdlc");
            rptViewerLoanSummaryPrint.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerLoanSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<RptLoanSummary> loanSummaryList = ra.GetLoanSummaryReport(loanId, startDate, endDate);

            rptViewerLoanSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", loanSummaryList));

            return rptViewerLoanSummaryPrint;
        }
    }
}