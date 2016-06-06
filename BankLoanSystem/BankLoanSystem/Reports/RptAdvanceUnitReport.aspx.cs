using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptAdvanceUnitReport : System.Web.UI.Page
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
            rptViewerAdvanceUnitRpt.ProcessingMode = ProcessingMode.Local;
            rptViewerAdvanceUnitRpt.Reset();
            rptViewerAdvanceUnitRpt.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceUnitRpt.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceReport.rdlc");
            rptViewerAdvanceUnitRpt.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerAdvanceUnitRpt.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<ReportFullInventoryUnit> units = ra.GetAdvanceUnitByLoanId(loanId, startDate, endDate);
            rptViewerAdvanceUnitRpt.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));

        }

    }
}