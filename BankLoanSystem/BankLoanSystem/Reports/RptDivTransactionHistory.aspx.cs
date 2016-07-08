using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BankLoanSystem.Reports
{
    public partial class RptDivTransactionHistory : System.Web.UI.Page
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

                ReportAccess ra = new ReportAccess();
                List<ReportTransactionHistory> loanSumm = ra.GetTransactionHistoryByDateRange(loanId, startDate, endDate);

                if(loanSumm.Count>0)
                {
                    RenderReport(loanId, startDate, endDate, loanSumm);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowFrame", "ShowDive();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideFrame", "HideDive();", true);
                }
            }
        }

        public void RenderReport(int loanId, DateTime startDate, DateTime , List<ReportTransactionHistory> loanSumm)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            rptViewerLoanSummary.ProcessingMode = ProcessingMode.Local;
            rptViewerLoanSummary.Reset();
            rptViewerLoanSummary.LocalReport.EnableExternalImages = true;
            rptViewerLoanSummary.LocalReport.ReportPath = Server.MapPath("~/Reports/RptTransactionHistory.rdlc");
            rptViewerLoanSummary.ZoomMode = ZoomMode.PageWidth;

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);


            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerLoanSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerLoanSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", loanSumm));
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
            rptViewerLoanSummaryPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptTransactionHistory.rdlc");

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            List<ReportTransactionHistory> loanSumm = ra.GetTransactionHistoryByDateRange(loanId, startDate, endDate);

            rptViewerLoanSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerLoanSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", loanSumm));

            return rptViewerLoanSummaryPrint;
        }
    }
}