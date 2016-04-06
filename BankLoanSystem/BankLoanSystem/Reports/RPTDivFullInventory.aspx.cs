﻿using System;
using System.Collections.Generic;
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
        }
    }
}