using System;
using System.Collections.Generic;
using System.IO;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivFullInventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;
                int titleStatus = 0;

                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                if (!string.IsNullOrEmpty(Request.QueryString["titleStatus"]))
                    titleStatus = Convert.ToInt32(Request.QueryString["titleStatus"]);
                RenderReport(loanId, titleStatus);
            }
        }

        public void RenderReport(int loanId, int titleStatus)
        {
            rptViewerFullInventory.ProcessingMode=ProcessingMode.Local;
            rptViewerFullInventory.Reset();
            rptViewerFullInventory.LocalReport.EnableExternalImages = true;
            rptViewerFullInventory.LocalReport.ReportPath = Server.MapPath("~/Reports/RptFullInventory.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            List<ReportFullInventoryUnit> units = ra.GetFullInventoryByLoanId(loanId);

            rptViewerFullInventory.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerFullInventory.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));

            //Warning[] warnings;
            //string[] streamids;
            //string mimeType;
            //string encoding;
            //string filenameExtension;

            //byte[] bytes = rptViewerFullInventory.LocalReport.Render(
            //    "PDF", null, out mimeType, out encoding, out filenameExtension,
            //    out streamids, out warnings);

            //using (FileStream fs = new FileStream(Server.MapPath("FullInvontory.pdf"), FileMode.Create))
            //{
            //    fs.Write(bytes, 0, bytes.Length);
            //}

            //rptViewerFullInventory.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            //rptViewerFullInventory.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));
        }

        public ReportViewer PrintPage(int loanId)
        {
            ReportViewer rptViewerFullInventoryPrint = new ReportViewer();
            rptViewerFullInventoryPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerFullInventoryPrint.Reset();
            rptViewerFullInventoryPrint.LocalReport.EnableExternalImages = true;
            rptViewerFullInventoryPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptFullInventory.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }


            List<ReportFullInventoryUnit> units = ra.GetFullInventoryByLoanId(loanId);

            rptViewerFullInventoryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerFullInventoryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));

            //ReportPrintDocument rpd = new ReportPrintDocument(rptViewerFullInventoryPrint.LocalReport);
            //try
            //{
            //    rpd.Print();
            //    return 1;
            //}
            //catch (Exception e)
            //{
            //    return 0;
            //}
            return rptViewerFullInventoryPrint;
        }
    }
}