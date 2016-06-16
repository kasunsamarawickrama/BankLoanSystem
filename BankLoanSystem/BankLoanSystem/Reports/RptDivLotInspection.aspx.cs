using System;
using System.Collections.Generic;
using System.Web.UI;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivLotInspection : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["AuthenticatedUser"] == null)
            //{
            //    return;
            //}
            if (!IsPostBack)
            {
                int loanId = 0;

                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                RenderReport(loanId);
            }
        }

        public void RenderReport(int loanId)
        {
            rptViewerLotInspection.ProcessingMode = ProcessingMode.Local;
            rptViewerLotInspection.Reset();
            rptViewerLotInspection.LocalReport.EnableExternalImages = true;
            rptViewerLotInspection.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLotInspection.rdlc");
            //rptViewerLotInspection.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerLotInspection.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<ReportUnitModels> units = ra.GetAllActiveUnitDetailsRpt(loanId);

            foreach (var unit in units)
            {
                unit.View = false;
            }

            rptViewerLotInspection.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));
        }

        public ReportViewer PrintPage(int loanId)
        {
            //if (Session["AuthenticatedUser"] == null)
            //{
            //    return null;
            //}

            ReportViewer rptViewerLotInspectionPrint = new ReportViewer();
            rptViewerLotInspectionPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerLotInspectionPrint.Reset();
            rptViewerLotInspectionPrint.LocalReport.EnableExternalImages = true;
            rptViewerLotInspectionPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLotInspection.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerLotInspectionPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<ReportUnitModels> units = ra.GetAllActiveUnitDetailsRpt(loanId);

            foreach (var unit in units)
            {
                unit.View = false;
            }

            rptViewerLotInspectionPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));
            return rptViewerLotInspectionPrint;

        }
    }
}