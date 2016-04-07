using System;
using System.Collections.Generic;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivLotInspection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            rptViewerLotInspection.Reset();
            rptViewerLotInspection.LocalReport.EnableExternalImages = true;
            rptViewerLotInspection.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLotInspection.rdlc");

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

        public void PrintPage(int loanId)
        {
            string a = "sasasasasas";
            //ReportPrintDocument rpd = new ReportPrintDocument(rptViewerLotInspection.LocalReport);
            //rpd.Print();
        }
    }
}