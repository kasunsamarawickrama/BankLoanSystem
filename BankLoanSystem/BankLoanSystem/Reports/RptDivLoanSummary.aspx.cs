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
            rptViewerLoanSummary.ProcessingMode = ProcessingMode.Local;
            rptViewerLoanSummary.Reset();
            rptViewerLoanSummary.LocalReport.EnableExternalImages = true;
            rptViewerLoanSummary.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLotInspection.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerLoanSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<RptLoanSummary> loanSummaryList = ra.GetLoanSummaryReport(loanId, startDate, endDate);

            rptViewerLoanSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", loanSummaryList));
        }
    }
}