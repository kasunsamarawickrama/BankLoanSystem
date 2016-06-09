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
    public partial class RptDivAdvanceFeeReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;
                //DateTime startDate = Convert.ToDateTime("5/5/2016");
                //DateTime endDate = Convert.ToDateTime("5/5/2019");

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
            rptViewerAdvanceFeeReceipt.ProcessingMode = ProcessingMode.Local;
            rptViewerAdvanceFeeReceipt.Reset();
            rptViewerAdvanceFeeReceipt.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceFeeReceipt.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceFeeReceipt.rdlc");
            rptViewerAdvanceFeeReceipt.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            List<RptFee> curtailments = ra.GetFeeReceiptByDateRange(loanId, "advanceFee", startDate, endDate);

            rptViewerAdvanceFeeReceipt.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerAdvanceFeeReceipt.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));
        }

        public ReportViewer PrintPage(int loanId, DateTime startDate, DateTime endDate)
        {
            ReportViewer rptViewerAdvanceFeeReceiptPrint = new ReportViewer();

            rptViewerAdvanceFeeReceiptPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerAdvanceFeeReceiptPrint.Reset();
            rptViewerAdvanceFeeReceiptPrint.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceFeeReceiptPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceFeeReceipt.rdlc");
            rptViewerAdvanceFeeReceiptPrint.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            List<RptFee> curtailments = ra.GetFeeReceiptByDateRange(loanId, "advanceFee", startDate, endDate);

            rptViewerAdvanceFeeReceiptPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerAdvanceFeeReceiptPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));

            return rptViewerAdvanceFeeReceiptPrint;
        }
    }
}