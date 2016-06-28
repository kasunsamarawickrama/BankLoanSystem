using System;
using System.Collections.Generic;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivTitleStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;
                int titleStatus = 0;

                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                if(!string.IsNullOrEmpty(Request.QueryString["titleStatus"]))
                    titleStatus = Convert.ToInt32(Request.QueryString["titleStatus"]);
                RenderReport(loanId, titleStatus);
            }
        }

        /*

            Frontend page: Report Page
            Title: Load details to report and show on browser
            Designed: Kanishka SHM
            User story: 
            Developed: Kanishka SHM
            Date created: 

        */
        public void RenderReport(int loanId, int titleStatus)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            rptViewerTitleStatus.ProcessingMode=ProcessingMode.Local;
            rptViewerTitleStatus.Reset();
            rptViewerTitleStatus.LocalReport.EnableExternalImages = true;
            rptViewerTitleStatus.LocalReport.ReportPath = Server.MapPath("~/Reports/RptTitleStatus.rdlc");
            rptViewerTitleStatus.ZoomMode = ZoomMode.PageWidth;

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Get unit details by given title status
            List<Unit> units = ra.GeUnitDetailsByTitleStatus(loanId, titleStatus);

            //set data source to report viwer
            rptViewerTitleStatus.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerTitleStatus.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));
        }

        /*

            Frontend page: Report Page
            Title: Load pdf view on browser
            Designed: Kanishka SHM
            User story: 
            Developed: Kanishka SHM
            Date created: 

        */
        public ReportViewer PrintPage(int loanId, int titleStatus)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            ReportViewer rptViewerTitleStatusPrint = new ReportViewer();
            rptViewerTitleStatusPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerTitleStatusPrint.Reset();
            rptViewerTitleStatusPrint.LocalReport.EnableExternalImages = true;
            rptViewerTitleStatusPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptTitleStatus.rdlc");

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Get unit details by given title status
            List<Unit> units = ra.GeUnitDetailsByTitleStatus(loanId, titleStatus);

            //set data source to report viwer
            rptViewerTitleStatusPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerTitleStatusPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));

            return rptViewerTitleStatusPrint;
        }
    }
}