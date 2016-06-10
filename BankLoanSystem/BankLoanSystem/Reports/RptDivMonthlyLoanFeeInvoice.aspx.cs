using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivMonthlyLoanFeeInvoice : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;
                string feeType = "monthlyLoanFee";

                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                if (string.IsNullOrEmpty(Request.QueryString["startDate"])) return;
                var startDate = DateTime.ParseExact(Request.QueryString["startDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                if (string.IsNullOrEmpty(Request.QueryString["endDate"])) return;
                var endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                RenderReport(loanId, startDate, endDate, feeType);
            }
        }

        public void RenderReport(int loanId, DateTime startDate, DateTime endDate, string feeType)
        {
            rptViewerMonthlyLoanFeeInvoice.ProcessingMode = ProcessingMode.Local;
            rptViewerMonthlyLoanFeeInvoice.Reset();
            rptViewerMonthlyLoanFeeInvoice.LocalReport.EnableExternalImages = true;
            rptViewerMonthlyLoanFeeInvoice.LocalReport.ReportPath = Server.MapPath("~/Reports/RptMonthlyLoanFeeInvoice.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            List<RptFee>monthlyLoanFeeInvoice = ra.GetFeeInvoiceByDateRange(loanId, feeType, startDate, endDate);

            rptViewerMonthlyLoanFeeInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerMonthlyLoanFeeInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", monthlyLoanFeeInvoice));
        }
        public ReportViewer PrintPage(int loanId, DateTime startDate, DateTime endDate)
        {
            ReportViewer rptViewerMonthlyLoanFeeInvoicePrint = new ReportViewer();
            rptViewerMonthlyLoanFeeInvoicePrint.ProcessingMode = ProcessingMode.Local;
            rptViewerMonthlyLoanFeeInvoicePrint.Reset();
            rptViewerMonthlyLoanFeeInvoicePrint.LocalReport.EnableExternalImages = true;
            rptViewerMonthlyLoanFeeInvoicePrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptMonthlyLoanFeeInvoice.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            List<RptFee> monthlyLoanFeeInvoice = ra.GetFeeInvoiceByDateRange(loanId, "monthlyLoanFee", startDate, endDate);

            rptViewerMonthlyLoanFeeInvoicePrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerMonthlyLoanFeeInvoicePrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", monthlyLoanFeeInvoice));
            
            return rptViewerMonthlyLoanFeeInvoicePrint;
        }
    }
}