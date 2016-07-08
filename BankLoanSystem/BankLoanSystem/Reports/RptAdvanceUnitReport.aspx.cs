﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptAdvanceUnitReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;

                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                if (string.IsNullOrEmpty(Request.QueryString["startDate"])) return;
                var startDate = DateTime.ParseExact(Request.QueryString["startDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                if (string.IsNullOrEmpty(Request.QueryString["endDate"])) return;
                var endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                ReportAccess ra = new ReportAccess();
                List<ReportFullInventoryUnit> units = ra.GetAdvanceUnitByLoanId(loanId, startDate, endDate);

                if (units.Count > 0)
                {
                    RenderReport(loanId, startDate, endDate, units);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowFrame", "ShowDive();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideFrame", "HideDive();", true);
                }
            }
        }

        public void RenderReport(int loanId, DateTime startDate, DateTime endDate, List<ReportFullInventoryUnit> units)
        {
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            rptViewerAdvanceUnitRpt.ProcessingMode = ProcessingMode.Local;
            rptViewerAdvanceUnitRpt.Reset();
            rptViewerAdvanceUnitRpt.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceUnitRpt.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceReport.rdlc");
            rptViewerAdvanceUnitRpt.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerAdvanceUnitRpt.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            rptViewerAdvanceUnitRpt.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));

        }


        public ReportViewer PrintPage(int loanId, DateTime startDate, DateTime endDate)
        {
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            ReportViewer rptViewerAdvanceUnitRptPrint = new ReportViewer();
            rptViewerAdvanceUnitRptPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerAdvanceUnitRptPrint.Reset();
            rptViewerAdvanceUnitRptPrint.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceUnitRptPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceReport.rdlc");
            rptViewerAdvanceUnitRptPrint.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerAdvanceUnitRptPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<ReportFullInventoryUnit> units = ra.GetAdvanceUnitByLoanId(loanId, startDate, endDate);
            rptViewerAdvanceUnitRptPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));

            return rptViewerAdvanceUnitRptPrint;

        }

    }
}