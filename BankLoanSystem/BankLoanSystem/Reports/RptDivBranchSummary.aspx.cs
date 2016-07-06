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
    public partial class RptDivBranchSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int branchId = 0;
                string branchName = "";

                if (Request.QueryString["branchId"] != "")
                    branchId = Convert.ToInt32(Request.QueryString["branchId"]);
                if (Request.QueryString["branchName"] != "")
                    branchName = Request.QueryString["branchName"];

                RenderReport(branchId, branchName);
            }
        }

        public void RenderReport(int branchId,string branchName)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            rptViewerBranchSummary.ProcessingMode = ProcessingMode.Local;
            rptViewerBranchSummary.Reset();
            rptViewerBranchSummary.LocalReport.EnableExternalImages = true;
            rptViewerBranchSummary.LocalReport.ReportPath = Server.MapPath("~/Reports/RptBranchSummary.rdlc");
            rptViewerBranchSummary.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            User usr = new User();
            usr = (new UserAccess()).retreiveUserByUserId(userData.UserId);
            List<LoanDetailsRpt> details = new List<LoanDetailsRpt>();
            LoanDetailsRpt detail = new LoanDetailsRpt();
            detail.CompanyName = userData.CompanyName;
            if (userData.RoleId == 1)
            {
                detail.LenderBrnchName = branchName;
            }
            else
            {
                detail.LenderBrnchName = userData.BranchName;
            }
            
            detail.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            detail.CreaterName = usr.FirstName + " " + usr.LastName;
            details.Add(detail);
            rptViewerBranchSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", details));

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }
            List<RptBranchSummary> branchSummary = ra.GetBranchSummarRptDetails(branchId);
            
            rptViewerBranchSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", branchSummary));
        }

        public ReportViewer PrintPage(int branchId)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            ReportViewer rptViewerBranchSummaryPrint = new ReportViewer();

            rptViewerBranchSummaryPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerBranchSummaryPrint.Reset();
            rptViewerBranchSummaryPrint.LocalReport.EnableExternalImages = true;
            rptViewerBranchSummaryPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptBranchSummary.rdlc");
            rptViewerBranchSummaryPrint.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = new List<LoanDetailsRpt>();
            LoanDetailsRpt detail = new LoanDetailsRpt();
            detail.CompanyName = userData.CompanyName;
            detail.LenderBrnchName = userData.BranchName;
            detail.ReportDate = DateTime.Now.ToString("MM/dd/yyyy"); ;
            details.Add(detail);
            rptViewerBranchSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", details));

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }
            List<RptBranchSummary> branchSummary = ra.GetBranchSummarRptDetails(branchId);

            rptViewerBranchSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", branchSummary));

            return rptViewerBranchSummaryPrint;
        }
    }
}