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
                companyId = 1151;
                RenderReport(companyId);
            }
        }

        public void RenderReport(int companyId)
        {
            rptViewerCompanySummary.ProcessingMode = ProcessingMode.Local;
            rptViewerCompanySummary.Reset();
            rptViewerCompanySummary.LocalReport.EnableExternalImages = true;
            rptViewerCompanySummary.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCompanySummary.rdlc");
            rptViewerCompanySummary.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<RptCompanySummary> loanSumaList = ra.RptGetCompanySummary(companyId);

            rptViewerCompanySummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", loanSumaList));
        }
    }
}