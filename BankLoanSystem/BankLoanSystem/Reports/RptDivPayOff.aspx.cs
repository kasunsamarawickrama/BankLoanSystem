using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankLoanSystem.Reports
{
    public partial class RptPayOff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int loanId = 0;
                

                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                if (string.IsNullOrEmpty(Request.QueryString["startDate"])) return;
                var startDate = DateTime.ParseExact(Request.QueryString["startDate"], "MM/dd/yyyy", new CultureInfo("en-US"));

                if (string.IsNullOrEmpty(Request.QueryString["endDate"])) return;
                var endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", new CultureInfo("en-US"));

                RenderReport(loanId, startDate, endDate);
            }


        }

        public void RenderReport(int loanId, DateTime startDate, DateTime endDate)
        {
            rptViewerPayOff.ProcessingMode = ProcessingMode.Local;
            rptViewerPayOff.Reset();
            rptViewerPayOff.LocalReport.EnableExternalImages = true;
            rptViewerPayOff.LocalReport.ReportPath = Server.MapPath("~/Reports/RptPayOff.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            List<ReportPayOff> curtailments = ra.GetPayOffDetailsByLoanId(loanId, startDate, endDate);

            rptViewerPayOff.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerPayOff.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));
        }

        public int PrintPage(int loanId, DateTime startDate, DateTime endDate)
        {
            ReportViewer rptViewerPayOffPrint = new ReportViewer();
            rptViewerPayOffPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerPayOffPrint.Reset();
            rptViewerPayOffPrint.LocalReport.EnableExternalImages = true;
            rptViewerPayOffPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptPayOff.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            List<ReportPayOff> curtailments = ra.GetPayOffDetailsByLoanId(loanId, startDate, endDate);

            rptViewerPayOffPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerPayOffPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));

            ReportPrintDocument rpd = new ReportPrintDocument(rptViewerPayOffPrint.LocalReport);
            try
            {
                rpd.Print();
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}