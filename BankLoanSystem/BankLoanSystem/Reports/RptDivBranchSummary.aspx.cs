﻿using BankLoanSystem.DAL;
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

                if (Request.QueryString["branchId"] != "")
                    branchId = Convert.ToInt32(Request.QueryString["branchId"]);

                RenderReport(branchId);
            }
        }

        public void RenderReport(int branchId)
        {
            rptViewerBranchSummary.ProcessingMode = ProcessingMode.Local;
            rptViewerBranchSummary.Reset();
            rptViewerBranchSummary.LocalReport.EnableExternalImages = true;
            rptViewerBranchSummary.LocalReport.ReportPath = Server.MapPath("~/Reports/RptBranchSummary.rdlc");
            rptViewerBranchSummary.ZoomMode = ZoomMode.PageWidth;

            User userData = ((User)Session["AuthenticatedUser"]);
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = new List<LoanDetailsRpt>();
            LoanDetailsRpt detail = new LoanDetailsRpt();
            detail.CompanyName = userData.CompanyName;
            detail.LenderBrnchName = userData.BranchName;
            detail.ReportDate = DateTime.Now.ToString("MM/dd/yyyy"); ;
            details.Add(detail);
            rptViewerBranchSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", details));

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }
            List<RptBranchSummary> branchSummary = ra.GetBranchSummarRptDetails(branchId);

            // rptViewerLotInspectionFeeInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerBranchSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", branchSummary));
            //rptViewerAdvanceUnit.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

           
        }
    }
}