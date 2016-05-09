using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;
using Unit = BankLoanSystem.Models.Unit;

namespace BankLoanSystem.Reports
{
    public partial class RptDivCUrtailmentReceiptDuringSession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int loanId = 1421;
                //RenderReport(loanId);
                int loanId = 0;

                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                RenderReport(loanId);
            }
        }


        public void RenderReport(int loanId)
        {
            rptViewerCurtailmentReceiptDuringSession.ProcessingMode = ProcessingMode.Local;
            rptViewerCurtailmentReceiptDuringSession.Reset();
            rptViewerCurtailmentReceiptDuringSession.LocalReport.EnableExternalImages = true;
            rptViewerCurtailmentReceiptDuringSession.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurtailmentDuringSession.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerCurtailmentReceiptDuringSession.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<CurtailmentShedule> selectedCurtailmentSchedules =
                (List<CurtailmentShedule>) Session["CurtUnitDuringSession"];

            if (selectedCurtailmentSchedules != null && selectedCurtailmentSchedules.Count > 0)
            {
                rptViewerCurtailmentReceiptDuringSession.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", selectedCurtailmentSchedules));
            }
        }

        public int PrintPage(int loanId)
        {
            ReportViewer rptViewerCurtailmentReceiptDuringSessionPrint = new ReportViewer();
            rptViewerCurtailmentReceiptDuringSessionPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerCurtailmentReceiptDuringSessionPrint.Reset();
            rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.EnableExternalImages = true;
            rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurtailmentDuringSession.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<CurtailmentShedule> selectedCurtailmentSchedules =
                (List<CurtailmentShedule>)Session["CurtUnitDuringSession"];

            if (selectedCurtailmentSchedules != null && selectedCurtailmentSchedules.Count > 0)
            {
                rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", selectedCurtailmentSchedules));
            }

            ReportPrintDocument rpd = new ReportPrintDocument(rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport);
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