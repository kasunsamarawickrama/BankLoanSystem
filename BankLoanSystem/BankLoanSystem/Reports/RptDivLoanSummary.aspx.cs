using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                var startDate = DateTime.ParseExact(Request.QueryString["startDate"], "MM/dd/yyyy", new CultureInfo("en-US"));

                if (string.IsNullOrEmpty(Request.QueryString["endDate"])) return;
                var endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", new CultureInfo("en-US"));

                RenderReport(loanId, startDate, endDate);
            }
        }

        public void RenderReport(int loanId, DateTime startDate, DateTime endDate)
        {
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


            List<ReportLoanSummary> loanSumm = ra.GetLoanSummaryByDateRange(loanId,startDate, endDate);

            rptViewerLoanSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerLoanSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", loanSumm));
        }

        public ReportViewer PrintPage(int loanId, DateTime startDate, DateTime endDate)
        {
            ReportViewer rptViewerLoanSummaryPrint = new ReportViewer();
            rptViewerLoanSummaryPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerLoanSummaryPrint.Reset();
            rptViewerLoanSummaryPrint.LocalReport.EnableExternalImages = true;
            rptViewerLoanSummaryPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLoanSummary.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            List<ReportLoanSummary> loanSumm = ra.GetLoanSummaryByDateRange(loanId, startDate, endDate);

            rptViewerLoanSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerLoanSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", loanSumm));

            return rptViewerLoanSummaryPrint;
        }
    }
}