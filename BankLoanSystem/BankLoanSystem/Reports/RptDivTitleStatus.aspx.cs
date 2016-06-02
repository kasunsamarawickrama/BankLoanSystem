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

        //public void RenderReport(int loanId, DateTime startDate, DateTime endDate)
        public void RenderReport(int loanId, int titleStatus)
        {
            rptViewerTitleStatus.ProcessingMode=ProcessingMode.Local;
            rptViewerTitleStatus.Reset();
            rptViewerTitleStatus.LocalReport.EnableExternalImages = true;
            rptViewerTitleStatus.LocalReport.ReportPath = Server.MapPath("~/Reports/RptTitleStatus.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            List<Unit> units = ra.GeUnitDetailsByTitleStatus(loanId, titleStatus);

            rptViewerTitleStatus.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerTitleStatus.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));
        }

        public ReportViewer PrintPage(int loanId, int titleStatus)
        {
            ReportViewer rptViewerTitleStatusPrint = new ReportViewer();
            rptViewerTitleStatusPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerTitleStatusPrint.Reset();
            rptViewerTitleStatusPrint.LocalReport.EnableExternalImages = true;
            rptViewerTitleStatusPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptTitleStatus.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            List<Unit> units = ra.GeUnitDetailsByTitleStatus(loanId, titleStatus);

            rptViewerTitleStatusPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerTitleStatusPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));

            ////ReportPrintDocument rpd = new ReportPrintDocument(rptViewerTitleStatusPrint.LocalReport);
            ////try
            ////{
            ////    rpd.Print();
            ////    return 1;
            ////}
            ////catch (Exception e)
            ////{
            ////    return 0;
            ////}

            return rptViewerTitleStatusPrint;
        }
    }
}