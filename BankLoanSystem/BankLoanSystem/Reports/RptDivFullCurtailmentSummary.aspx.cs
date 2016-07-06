using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankLoanSystem.Reports
{
    public partial class RptDivFullCurtailmentSummary : System.Web.UI.Page
    {
        /// <summary>
        /// Frontend Page: Report page(Full curtailment summary report)
        /// Title: Display Full curtailment summary report
        /// Designed: Piyumi Perera
        /// User story: DFP 486
        /// Developed: Piyumi Perera
        /// Date created: 7/1/2016
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;

                //if session is not null and assign it.
                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

               

                //call RenderReport function to show report on report viwer
                RenderReport(loanId);
            }
        }
        /// <summary>
        /// Frontend Page: Report page(Full curtailment summary report)
        /// Title: Display Full curtailment summary report
        /// Designed: Piyumi Perera
        /// User story: DFP 486
        /// Developed: Piyumi Perera
        /// Date created: 7/1/2016
        /// </summary>
        /// <param name="loanId"></param>
        public void RenderReport(int loanId)
        {
            //Check authentication session is null then return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //Dynamicaly set report viwer propertiece
            rptViewerFullCurtailmentSummary.ProcessingMode = ProcessingMode.Local;
            rptViewerFullCurtailmentSummary.Reset();
            rptViewerFullCurtailmentSummary.LocalReport.EnableExternalImages = true;
            rptViewerFullCurtailmentSummary.LocalReport.ReportPath = Server.MapPath("~/Reports/RptFullCurtailmentSummary.rdlc");
            rptViewerFullCurtailmentSummary.ZoomMode = ZoomMode.PageWidth;

            //Get details of loan
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);
            foreach (var dates in details)
            {
                
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Set data set to report
            rptViewerFullCurtailmentSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            //Get all curtailment summary details
            List<RptFullCurtailmentSummary> fullCurtailment = ra.GetFullCurtailmentSummarRptDetails(loanId);

            //Set data set to report
            rptViewerFullCurtailmentSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", fullCurtailment));
        }

        public ReportViewer PrintPage(int loanId)
        {
            //Check authentication session is null then return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            ReportViewer rptViewerFullCurtailmentSummaryPrint = new ReportViewer();

            rptViewerFullCurtailmentSummaryPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerFullCurtailmentSummaryPrint.Reset();
            rptViewerFullCurtailmentSummaryPrint.LocalReport.EnableExternalImages = true;
            rptViewerFullCurtailmentSummaryPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptFullCurtailmentSummary.rdlc");
            rptViewerFullCurtailmentSummaryPrint.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);
            foreach (var dates in details)
            {

                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerFullCurtailmentSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<RptFullCurtailmentSummary> fullCurtailment = ra.GetFullCurtailmentSummarRptDetails(loanId);
            rptViewerFullCurtailmentSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", fullCurtailment));

            return rptViewerFullCurtailmentSummaryPrint;
        }
    }
}