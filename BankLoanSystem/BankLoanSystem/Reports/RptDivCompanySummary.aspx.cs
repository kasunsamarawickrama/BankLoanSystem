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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int companyId = 0;

                if (Request.QueryString["companyId"] != "")
                    companyId = Convert.ToInt32(Request.QueryString["companyId"]);

                //hard coded
                //companyId = 1151;
                RenderReport(companyId);
            }
        }

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

            //company name and report date
            List<LoanDetailsRpt> details = new List<LoanDetailsRpt>();
            LoanDetailsRpt detail = new LoanDetailsRpt();
            detail.CompanyName = userData.CompanyName;
            detail.ReportDate = DateTime.Now.ToString("MM/dd/yyyy"); ;
            details.Add(detail);
            //set data source to report viwer - 2
            rptViewerCompanySummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", details));
        }

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

            //company name and report date
            List<LoanDetailsRpt> details = new List<LoanDetailsRpt>();
            LoanDetailsRpt detail = new LoanDetailsRpt();
            detail.CompanyName = userData.CompanyName;
            detail.ReportDate = DateTime.Now.ToString("MM/dd/yyyy"); ;
            details.Add(detail);
            //set data source to report viwer - 2
            rptViewerCompanySummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", details));

            //return report viwer
            return rptViewerCompanySummaryPrint;
        }

    }
}