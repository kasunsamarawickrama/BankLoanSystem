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
    public partial class RptDivIndividualCurtailmentSummary : System.Web.UI.Page
    {
        /// <summary>
        /// Frontend Page: Report page(Individual curtailment summary report)
        /// Title: Display Individual curtailment summary report
        /// Designed: Piyumi Perera
        /// User story: DFP 488
        /// Developed: Piyumi Perera
        /// Date created: 7/5/2016
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;
                string idNumber = "";
                //if session is not null and assign it.
                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                if (Request.QueryString["idNumber"] != "" && Request.QueryString["idNumber"] != null)
                    idNumber = Request.QueryString["idNumber"];

                ReportAccess ra = new ReportAccess();
                //Get all curtailment summary details
                List<RptIndividualCurtailmentSummary> indiCurtailment = ra.GetIndividualCurtailmentSummarRptDetails(loanId, idNumber);

                if (indiCurtailment.Count > 0)
                {
                    //call RenderReport function to show report on report viwer
                    RenderReport(loanId, idNumber, indiCurtailment);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowFrame", "ShowDive();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideFrame", "HideDive();", true);
                }
            }
        }
        /// <summary>
        /// Frontend Page: Report page(Individual curtailment summary report)
        /// Title: Display Individual curtailment summary report
        /// Designed: Piyumi Perera
        /// User story: DFP 488
        /// Developed: Piyumi Perera
        /// Date created: 7/5/2016
        /// </summary>
        /// <param name="loanId"></param>
        public void RenderReport(int loanId, string idNumbe, List<RptIndividualCurtailmentSummary> indiCurtailment)
        {
            //Check authentication session is null then return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //Dynamicaly set report viwer propertiece
            rptViewerIndividualCurtailmentSummary.ProcessingMode = ProcessingMode.Local;
            rptViewerIndividualCurtailmentSummary.Reset();
            rptViewerIndividualCurtailmentSummary.LocalReport.EnableExternalImages = true;
            rptViewerIndividualCurtailmentSummary.LocalReport.ReportPath = Server.MapPath("~/Reports/RptIndividualCurtailmentSummary.rdlc");
            rptViewerIndividualCurtailmentSummary.ZoomMode = ZoomMode.PageWidth;

            //Get details of loan
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);
            foreach (var dates in details)
            {

                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Set data set to report
            rptViewerIndividualCurtailmentSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            //Set data set to report
            rptViewerIndividualCurtailmentSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", indiCurtailment));
        }

        public ReportViewer PrintPage(int loanId, string idNumber)
        {
            //Check authentication session is null then return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            ReportViewer rptViewerIndividualCurtailmentSummaryPrint = new ReportViewer();

            rptViewerIndividualCurtailmentSummaryPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerIndividualCurtailmentSummaryPrint.Reset();
            rptViewerIndividualCurtailmentSummaryPrint.LocalReport.EnableExternalImages = true;
            rptViewerIndividualCurtailmentSummaryPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptIndividualCurtailmentSummary.rdlc");
            rptViewerIndividualCurtailmentSummaryPrint.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);
            foreach (var dates in details)
            {

                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerIndividualCurtailmentSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            //Get all curtailment summary details
            List<RptIndividualCurtailmentSummary> indiCurtailment = ra.GetIndividualCurtailmentSummarRptDetails(loanId, idNumber);
            rptViewerIndividualCurtailmentSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", indiCurtailment));

            return rptViewerIndividualCurtailmentSummaryPrint;
        }
    }
}