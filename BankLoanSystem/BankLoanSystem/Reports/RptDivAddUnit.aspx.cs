using System;
using System.Collections.Generic;
using System.Web.UI;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivAddUnit : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;
                int userId = 0;

                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);
                if(Request.QueryString["userId"] != "")
                    userId = Convert.ToInt32(Request.QueryString["userId"]);

                RenderReport(loanId, userId);
            }
        }

        public void RenderReport(int loanId, int userId)
        {
            rptViewerAddUnit.ProcessingMode = ProcessingMode.Local;
            rptViewerAddUnit.Reset();
            rptViewerAddUnit.LocalReport.EnableExternalImages = true;
            rptViewerAddUnit.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAddUnit.rdlc");
            rptViewerAddUnit.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerAddUnit.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<RptAddUnit> units = ra.GetJustAddedUnitDetails(userId, loanId);

            rptViewerAddUnit.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));


        }

        //public void PrintPage(int loanId)
        //{
        //    ReportPrintDocument rpd = new ReportPrintDocument(rptViewerAddUnit.LocalReport);
        //    rpd.Print();
        //}
    }
}