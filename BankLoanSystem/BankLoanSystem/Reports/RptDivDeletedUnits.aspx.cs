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
    public partial class RptDivDeletedUnits : System.Web.UI.Page
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
            rptViewerDeletedUnits.ProcessingMode = ProcessingMode.Local;
            rptViewerDeletedUnits.Reset();
            rptViewerDeletedUnits.LocalReport.EnableExternalImages = true;
            rptViewerDeletedUnits.LocalReport.ReportPath = Server.MapPath("~/Reports/RptDeletedUnits.rdlc");
            rptViewerDeletedUnits.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerDeletedUnits.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<RptDeletedUnit> deletedUnits = ra.RptGetDeletedUnitByLoanIdDateRange(loanId, startDate, endDate);
            rptViewerDeletedUnits.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", deletedUnits));
        }

        public ReportViewer PrintPage(int loanId, DateTime startDate, DateTime endDate)
        {
            ReportViewer rptViewerDeletedUnitsPrint = new ReportViewer();

            rptViewerDeletedUnitsPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerDeletedUnitsPrint.Reset();
            rptViewerDeletedUnitsPrint.LocalReport.EnableExternalImages = true;
            rptViewerDeletedUnitsPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptDeletedUnits.rdlc");
            rptViewerDeletedUnitsPrint.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerDeletedUnitsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<RptDeletedUnit> deletedUnits = ra.RptGetDeletedUnitByLoanIdDateRange(loanId, startDate, endDate);
            rptViewerDeletedUnitsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", deletedUnits));

            return rptViewerDeletedUnitsPrint;
        }
    }
}