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
    public partial class RptDivMonthlyLoanFeeInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;
                string feeType = "monthlyLoanFee";
                //DateTime startDate = Convert.ToDateTime("5/5/2016");
                //DateTime endDate = Convert.ToDateTime("5/5/2019");

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
        public int PrintPage(int loanId, DateTime startDate, DateTime endDate)
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

            ReportPrintDocument rpd = new ReportPrintDocument(rptViewerMonthlyLoanFeeInvoicePrint.LocalReport);
            try
            {
                rpd.Print();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}