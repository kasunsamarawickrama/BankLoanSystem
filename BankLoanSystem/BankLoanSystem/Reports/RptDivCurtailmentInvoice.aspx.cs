using System;
using System.Collections.Generic;
using System.Globalization;
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
                DateTime startDate = new DateTime();
                DateTime endDate = new DateTime();

                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);
                if (Request.QueryString["startDate"] != "")
                    startDate = DateTime.ParseExact(Convert.ToDateTime(Request.QueryString["startDate"]).ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                if (Request.QueryString["endDate"] != "")
                    endDate = DateTime.ParseExact(Convert.ToDateTime(Request.QueryString["endDate"]).ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture); //Convert.ToDateTime(Request.QueryString["endDate"]);

                RenderReport(loanId, startDate, endDate);
            }
        }

        public void RenderReport(int loanId, DateTime startDate, DateTime endDate)
        {
            rptViewerCurtailmentInvoice.Reset();
            rptViewerCurtailmentInvoice.LocalReport.EnableExternalImages = true;
            rptViewerCurtailmentInvoice.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurtailmentInvoice.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            
            List<CurtailmentShedule> curtailments = ra.GetCurtailmentScheduleByDateRange(loanId, startDate, endDate);

            rptViewerCurtailmentInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerCurtailmentInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));
        }

    }
}