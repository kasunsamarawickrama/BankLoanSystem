using System;
using System.Collections.Generic;
using System.Web.UI;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivLoanTerms : Page
    {
        /// <summary>
        /// Frontend Page: Report Page(Loan Terms Report)
        /// Title: Display Loan Terms Report
        /// Designed: Kanishka SHM
        /// User story: 
        /// Developed: Kanishka SHM
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

                //call RenderReport function to show report on report viwer
                RenderReport(loanId);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowFrame", "ShowDive();", true);

            }
        }

        public void RenderReport(int loanId)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //set reportviewr properties
            rptViewerLoanTerms.ProcessingMode = ProcessingMode.Local;
            rptViewerLoanTerms.Reset();
            rptViewerLoanTerms.LocalReport.EnableExternalImages = true;
            rptViewerLoanTerms.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLoanTerms.rdlc");
            rptViewerLoanTerms.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();

            //Get loan details and set to reportviwer
            List<RptLoanTerms> loanTermsDetails = ra.RptLoanTermsDetails(loanId);
            rptViewerLoanTerms.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", loanTermsDetails));

            List<LoanDetailsRpt> details = ra.GetLoanDetailsRptforCompanySummary(userData.Company_Id, userData.UserId);
            rptViewerLoanTerms.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", details));

            //get curtailment schedule details and set to reportviwer
            CurtailmentAccess ca = new CurtailmentAccess();
            List<Curtailment> curtailments = ca.retreiveCurtailmentByLoanId(loanId);

            if (curtailments != null && curtailments.Count > 0)
            {
                for (int i = 0; i < curtailments.Count; i++)
                {
                    curtailments[i].CurtailmentId = i + 1;
                }
            }
            rptViewerLoanTerms.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));

            //get fees details and set to reportviwer
            List<RptFeeLoanTerm> loanTermsFeeDetails = ra.RptLoanTermsFeeDetails(loanId);
            rptViewerLoanTerms.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", loanTermsFeeDetails));

            //get reminders details and set to reportviwer
            List<RptEmailReminder> loanTermsEmailReminders = ra.RptLoanTermsEmailReminders(loanId);
            rptViewerLoanTerms.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", loanTermsEmailReminders));

            //get unit types and set  to reportviwer
            IList<UnitType> unitTypes = ra.RptGetUnitTypes(loanId);
            rptViewerLoanTerms.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", unitTypes));
        }

        public ReportViewer PrintPage(int loanId)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            //create reportviwer & set reportviewr properties
            ReportViewer rptViewerLoanTermsPrint = new ReportViewer();
            rptViewerLoanTermsPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerLoanTermsPrint.Reset();
            rptViewerLoanTermsPrint.LocalReport.EnableExternalImages = true;
            rptViewerLoanTermsPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLoanTerms.rdlc");
            rptViewerLoanTermsPrint.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();

            //Get loan details and set to reportviwer
            List<RptLoanTerms> loanTermsDetails = ra.RptLoanTermsDetails(loanId);
            rptViewerLoanTermsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", loanTermsDetails));

            List<LoanDetailsRpt> details = ra.GetLoanDetailsRptforCompanySummary(userData.Company_Id, userData.UserId);
            rptViewerLoanTermsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", details));

            //get curtailment schedule details and set to reportviwer
            CurtailmentAccess ca = new CurtailmentAccess();
            List<Curtailment> curtailments = ca.retreiveCurtailmentByLoanId(loanId);

            if (curtailments != null && curtailments.Count > 0)
            {
                for (int i = 0; i < curtailments.Count; i++)
                {
                    curtailments[i].CurtailmentId = i + 1;
                }
            }
            rptViewerLoanTermsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));

            //get fees details and set to reportviwer
            List<RptFeeLoanTerm> loanTermsFeeDetails = ra.RptLoanTermsFeeDetails(loanId);
            rptViewerLoanTermsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", loanTermsFeeDetails));

            //get reminders details and set to reportviwer
            List<RptEmailReminder> loanTermsEmailReminders = ra.RptLoanTermsEmailReminders(loanId);
            rptViewerLoanTermsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", loanTermsEmailReminders));

            //get unit types and set  to reportviwer
            IList<UnitType> unitTypes = ra.RptGetUnitTypes(loanId);
            rptViewerLoanTermsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", unitTypes));

            //return reportviwer
            return rptViewerLoanTermsPrint;
        }
    }
}