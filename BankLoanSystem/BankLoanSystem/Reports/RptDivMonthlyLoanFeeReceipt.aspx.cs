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
    public partial class RptDivMonthlyLoanFeeReceipt : System.Web.UI.Page
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

                ReportAccess ra = new ReportAccess();
                List<RptFee> monthlyLoanFeeReceipt = ra.GetFeeReceiptByDateRange(loanId, "monthlyLoanFee", startDate, endDate);

                if (monthlyLoanFeeReceipt.Count > 0)
                {
                    RenderReport(loanId, startDate, endDate, monthlyLoanFeeReceipt);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowFrame", "ShowDive();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideFrame", "HideDive();", true);
                }
            }
        }

        public void RenderReport(int loanId, DateTime startDate, DateTime endDate, List<RptFee> monthlyLoanFeeReceipt)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            rptViewerMonthlyLoanFeeReceipt.ProcessingMode = ProcessingMode.Local;
            rptViewerMonthlyLoanFeeReceipt.Reset();
            rptViewerMonthlyLoanFeeReceipt.LocalReport.EnableExternalImages = true;
            rptViewerMonthlyLoanFeeReceipt.LocalReport.ReportPath = Server.MapPath("~/Reports/RptMonthlyLoanFeeReceipt.rdlc");
            rptViewerMonthlyLoanFeeReceipt.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            //List<RptFee> curtailments = ra.GetFeeReceiptByDateRange(loanId, "monthlyLoanFee", startDate, endDate);

            rptViewerMonthlyLoanFeeReceipt.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerMonthlyLoanFeeReceipt.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", monthlyLoanFeeReceipt));
        }

        public ReportViewer PrintPage(int loanId, DateTime startDate, DateTime endDate)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            ReportViewer rptViewerMonthlyLoanFeeReceiptPrint = new ReportViewer();
            rptViewerMonthlyLoanFeeReceiptPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerMonthlyLoanFeeReceiptPrint.Reset();
            rptViewerMonthlyLoanFeeReceiptPrint.LocalReport.EnableExternalImages = true;
            rptViewerMonthlyLoanFeeReceiptPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptMonthlyLoanFeeReceipt.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            List<RptFee> curtailments = ra.GetFeeReceiptByDateRange(loanId, "monthlyLoanFee", startDate, endDate);

            rptViewerMonthlyLoanFeeReceiptPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerMonthlyLoanFeeReceiptPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));

            return rptViewerMonthlyLoanFeeReceiptPrint;
        }
    }
}