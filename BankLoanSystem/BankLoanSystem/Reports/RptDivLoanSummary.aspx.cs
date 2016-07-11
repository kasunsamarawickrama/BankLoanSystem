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
        /// <summary>
        /// Frontend Page: Report Page(Loan Summary Report)
        /// Title: Display Loan Summary Report
        /// Designed: Kasun Samarawickrama
        /// User story: 
        /// Developed: Kasun Samarawickrama
        /// Date created: 
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;

                //if session is not null and assign it.
                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                if (string.IsNullOrEmpty(Request.QueryString["startDate"])) return;
                var startDate = DateTime.ParseExact(Request.QueryString["startDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                if (string.IsNullOrEmpty(Request.QueryString["endDate"])) return;
                var endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                //get loan summary details 
                ReportAccess ra = new ReportAccess();
                List<RptLoanSummary> loanSummaryList = ra.GetLoanSummaryReport(loanId, startDate, endDate);

                //if result count is greater then zero show report, otherwise give a message 
                if (loanSummaryList.Count > 0)
                {
                    //call RenderReport function to show report on report viwer
                    RenderReport(loanId, startDate, endDate, loanSummaryList);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowFrame", "ShowDive();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideFrame", "HideDive();", true);
                }
            }
        }

        /// <summary>
        /// Frontend Page: Report Page(Loan Summary Report)
        /// Title: Display Loan Summary Report
        /// Designed: Kasun Samarawickrama
        /// User story: 
        /// Developed: Kasun Samarawickrama
        /// Date created: 
        /// </summary>
        public void RenderReport(int loanId, DateTime startDate, DateTime endDate, List<RptLoanSummary> loanSummaryList)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            rptViewerLoanSummary.ProcessingMode = ProcessingMode.Local;
            rptViewerLoanSummary.Reset();
            rptViewerLoanSummary.LocalReport.EnableExternalImages = true;
            rptViewerLoanSummary.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLoanSummary.rdlc");
            rptViewerLoanSummary.ZoomMode = ZoomMode.PageWidth;

            //Get account details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //set data source to report viwer
            rptViewerLoanSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerLoanSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", loanSummaryList));
        }

        /// <summary>
        /// Frontend Page: Report Page(Loan Summary Report)
        /// Title: Display Loan Summary Report print page
        /// Designed: Kasun Samarawickrama
        /// User story: 
        /// Developed: Kasun Samarawickrama
        /// Date created: 
        /// </summary>
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
            rptViewerLoanSummaryPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLoanSummary.rdlc");
            rptViewerLoanSummaryPrint.ZoomMode = ZoomMode.PageWidth;

            //Get account details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //set data source to report viwer
            rptViewerLoanSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            //get loan summary details 
            List<RptLoanSummary> loanSummaryList = ra.GetLoanSummaryReport(loanId, startDate, endDate);

            //set data source to report viwer
            rptViewerLoanSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", loanSummaryList));

            //return reportviwer
            return rptViewerLoanSummaryPrint;
        }
    }
}