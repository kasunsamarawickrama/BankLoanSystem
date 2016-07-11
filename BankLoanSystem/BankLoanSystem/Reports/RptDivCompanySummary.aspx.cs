using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivCompanySummary : System.Web.UI.Page
    {
        /// <summary>
        /// Frontend Page: Report Page(Company Summary Report)
        /// Title: Display Company Summary details
        /// Designed: Kanishka SHM
        /// User story: 
        /// Developed: Kanishka SHM
        /// Date created: 
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int companyId = 0;

                //if session is not null and assign it.
                if (Request.QueryString["companyId"] != "")
                    companyId = Convert.ToInt32(Request.QueryString["companyId"]);

                //call RenderReport function to show report on report viwer
                RenderReport(companyId);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowFrame", "ShowDive();", true);
            }
        }

        /// <summary>
        /// Frontend Page: Report Page(Company Summary Report)
        /// Title: Display Company Summary details
        /// Designed: Kanishka SHM
        /// User story: 
        /// Developed: Kanishka SHM
        /// Date created: 
        /// </summary>
        public void RenderReport(int companyId)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            rptViewerCompanySummary.ProcessingMode = ProcessingMode.Local;
            rptViewerCompanySummary.Reset();
            rptViewerCompanySummary.LocalReport.EnableExternalImages = true;
            rptViewerCompanySummary.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCompanySummary.rdlc");
            rptViewerCompanySummary.ZoomMode = ZoomMode.PageWidth;

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<RptCompanySummary> loanSumaList = ra.RptGetCompanySummary(companyId);

            //set data source to report viwer - 1
            rptViewerCompanySummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", loanSumaList));

            List<LoanDetailsRpt> details = ra.GetLoanDetailsRptforCompanySummary(userData.Company_Id, userData.UserId);
            //set data source to report viwer - 2
            rptViewerCompanySummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", details));
        }

        /// <summary>
        /// Frontend Page: Report Page(Company Summary Report)
        /// Title: Display Company Summary details print page
        /// Designed: Kanishka SHM
        /// User story: 
        /// Developed: Kanishka SHM
        /// Date created: 
        /// </summary>
        public ReportViewer PrintPage(int companyId)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            ReportViewer rptViewerCompanySummaryPrint = new ReportViewer();
            rptViewerCompanySummaryPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerCompanySummaryPrint.Reset();
            rptViewerCompanySummaryPrint.LocalReport.EnableExternalImages = true;
            rptViewerCompanySummaryPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCompanySummary.rdlc");
            rptViewerCompanySummaryPrint.ZoomMode = ZoomMode.PageWidth;

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<RptCompanySummary> loanSumaList = ra.RptGetCompanySummary(companyId);

            //set data source to report viwer - 1
            rptViewerCompanySummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", loanSumaList));

            List<LoanDetailsRpt> details = ra.GetLoanDetailsRptforCompanySummary(userData.Company_Id, userData.UserId);
            //set data source to report viwer - 2
            rptViewerCompanySummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", details));

            //return report viwer
            return rptViewerCompanySummaryPrint;
        }

    }
}