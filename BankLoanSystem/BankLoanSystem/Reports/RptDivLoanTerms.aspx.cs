﻿using System;
using System.Collections.Generic;
using System.Web.UI;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivLoanTerms : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;

                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                //int loanId = 3519;

                RenderReport(loanId);
            }
        }

        public void RenderReport(int loanId)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            rptViewerLoanTerms.ProcessingMode = ProcessingMode.Local;
            rptViewerLoanTerms.Reset();
            rptViewerLoanTerms.LocalReport.EnableExternalImages = true;
            rptViewerLoanTerms.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLoanTerms.rdlc");
            rptViewerLoanTerms.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<RptLoanTerms> loanTermsDetails = ra.RptLoanTermsDetails(loanId);

            rptViewerLoanTerms.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", loanTermsDetails));

            List<LoanDetailsRpt> details = ra.GetLoanDetailsRptforCompanySummary(userData.Company_Id, userData.UserId);
            rptViewerLoanTerms.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", details));

            CurtailmentAccess ca = new CurtailmentAccess();
            List<Curtailment> curtailments = ca.retreiveCurtailmentByLoanId(loanId);

            if (curtailments != null && curtailments.Count > 0)
            {
                for (int i = 0; i < curtailments.Count; i++)
                {
                    curtailments[i].CurtailmentId = i + 1;
                }
            }
            rptViewerLoanTerms.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));

            List<RptFeeLoanTerm> loanTermsFeeDetails = ra.RptLoanTermsFeeDetails(loanId);
            rptViewerLoanTerms.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", loanTermsFeeDetails));

            List<RptEmailReminder> loanTermsEmailReminders = ra.RptLoanTermsEmailReminders(loanId);
            rptViewerLoanTerms.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", loanTermsEmailReminders));

            IList<UnitType> unitTypes = ra.RptGetUnitTypes(loanId);
            rptViewerLoanTerms.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", unitTypes));
        }

        public ReportViewer PrintPage(int loanId)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            ReportViewer rptViewerLoanTermsPrint = new ReportViewer();

            rptViewerLoanTermsPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerLoanTermsPrint.Reset();
            rptViewerLoanTermsPrint.LocalReport.EnableExternalImages = true;
            rptViewerLoanTermsPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLoanTerms.rdlc");
            rptViewerLoanTermsPrint.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<RptLoanTerms> loanTermsDetails = ra.RptLoanTermsDetails(loanId);

            rptViewerLoanTermsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", loanTermsDetails));

            List<LoanDetailsRpt> details = ra.GetLoanDetailsRptforCompanySummary(userData.Company_Id, userData.UserId);
            rptViewerLoanTerms.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", details));

            CurtailmentAccess ca = new CurtailmentAccess();
            List<Curtailment> curtailments = ca.retreiveCurtailmentByLoanId(loanId);

            if (curtailments != null && curtailments.Count > 0)
            {
                for (int i = 0; i < curtailments.Count; i++)
                {
                    curtailments[i].CurtailmentId = i + 1;
                }
            }
            rptViewerLoanTermsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));

            List<RptFeeLoanTerm> loanTermsFeeDetails = ra.RptLoanTermsFeeDetails(loanId);
            rptViewerLoanTermsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", loanTermsFeeDetails));

            List<RptEmailReminder> loanTermsEmailReminders = ra.RptLoanTermsEmailReminders(loanId);
            rptViewerLoanTermsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", loanTermsEmailReminders));

            IList<UnitType> unitTypes = ra.RptGetUnitTypes(loanId);
            rptViewerLoanTermsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", unitTypes));

            return rptViewerLoanTermsPrint;
        }
    }
}