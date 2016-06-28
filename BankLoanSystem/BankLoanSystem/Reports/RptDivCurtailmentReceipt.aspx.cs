using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivCurtailmentReceipt : System.Web.UI.Page
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
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            rptViewerCurtailmentReceipt.ProcessingMode=ProcessingMode.Local;
            rptViewerCurtailmentReceipt.Reset();
            rptViewerCurtailmentReceipt.LocalReport.EnableExternalImages = true;
            rptViewerCurtailmentReceipt.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurtailmentReceipt.rdlc");
            rptViewerCurtailmentReceipt.ZoomMode = ZoomMode.PageWidth;

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            //add dates, date range and current date
            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //get curtailment paid details
            List<CurtailmentShedule> curtailments = ra.GetCurtailmentPaidDetailsByDateRange(loanId, startDate, endDate);

            //set data source to report viwer
            rptViewerCurtailmentReceipt.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerCurtailmentReceipt.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));
        }

        public ReportViewer PrintPage(int loanId, DateTime startDate, DateTime endDate)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            ReportViewer rptViewerCurtailmentReceiptPrint = new ReportViewer();
            rptViewerCurtailmentReceiptPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerCurtailmentReceiptPrint.Reset();
            rptViewerCurtailmentReceiptPrint.LocalReport.EnableExternalImages = true;
            rptViewerCurtailmentReceiptPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurtailmentReceipt.rdlc");

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            //add dates, date range and current date
            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //get curtailment paid details
            List<CurtailmentShedule> curtailments = ra.GetCurtailmentPaidDetailsByDateRange(loanId, startDate, endDate);

            //set data source to report viwer
            rptViewerCurtailmentReceiptPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerCurtailmentReceiptPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));

            //return report viwer
            return rptViewerCurtailmentReceiptPrint;
        }

    }
}