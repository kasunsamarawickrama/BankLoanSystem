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
        /// <summary>
        // Frontend Page: Report page(Branch Summary report)
        /// Title: Display Branch Summary report
        /// Designed: Piyumi Perera
        /// User story: 
        /// Developed: Piyumi Perera
        /// Date created: 
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int branchId = 0;
                string branchName = "";

                //if session is not null and assign it.
                if (Request.QueryString["branchId"] != "")
                    branchId = Convert.ToInt32(Request.QueryString["branchId"]);
                if (Request.QueryString["branchName"] != "")
                    branchName = Request.QueryString["branchName"];

                //Get branch summary details
                ReportAccess ra = new ReportAccess();
                List<RptBranchSummary> branchSummary = ra.GetBranchSummarRptDetails(branchId);

                //if result count is greater then zero show report, otherwise give a message
                if (branchSummary.Count > 0)
                {
                    //call RenderReport function to show report on report viwer
                    RenderReport(branchId, branchName, branchSummary);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowFrame", "ShowDive();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideFrame", "HideDive();", true);
                }
            }
        }

        /// <summary>
        // Frontend Page: Report page(Branch Summary report)
        /// Title: Display Branch Summary report
        /// Designed: Piyumi Perera
        /// User story: 
        /// Developed: Piyumi Perera
        /// Date created: 
        /// </summary>
        public void RenderReport(int branchId,string branchName, List<RptBranchSummary> branchSummary)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //set reportviewr properties
            rptViewerBranchSummary.ProcessingMode = ProcessingMode.Local;
            rptViewerBranchSummary.Reset();
            rptViewerBranchSummary.LocalReport.EnableExternalImages = true;
            rptViewerBranchSummary.LocalReport.ReportPath = Server.MapPath("~/Reports/RptBranchSummary.rdlc");
            rptViewerBranchSummary.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            User usr = new User();
            usr = (new UserAccess()).retreiveUserByUserId(userData.UserId);
            List<LoanDetailsRpt> details = new List<LoanDetailsRpt>();
            LoanDetailsRpt detail = new LoanDetailsRpt();
            detail.CompanyName = userData.CompanyName;
            if (userData.RoleId == 1)
            {
                detail.LenderBrnchName = branchName;
            }
            else
            {
                detail.LenderBrnchName = userData.BranchName;
            }
            
            detail.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            detail.CreaterName = usr.FirstName + " " + usr.LastName;
            details.Add(detail);
            rptViewerBranchSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", details));

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }
            
            rptViewerBranchSummary.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", branchSummary));
        }

        /// <summary>
        /// Frontend Page: Report page(Branch Summary report)
        /// Title: Display Branch Summary print page
        /// Designed: Piyumi Perera
        /// User story: 
        /// Developed: Piyumi Perera
        /// Date created: 
        /// </summary>
        public ReportViewer PrintPage(int branchId,string branchName)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            //create reportviwer & set reportviewr properties
            ReportViewer rptViewerBranchSummaryPrint = new ReportViewer();
            rptViewerBranchSummaryPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerBranchSummaryPrint.Reset();
            rptViewerBranchSummaryPrint.LocalReport.EnableExternalImages = true;
            rptViewerBranchSummaryPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptBranchSummary.rdlc");
            rptViewerBranchSummaryPrint.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            User usr = new User();
            usr = (new UserAccess()).retreiveUserByUserId(userData.UserId);
            List<LoanDetailsRpt> details = new List<LoanDetailsRpt>();
            LoanDetailsRpt detail = new LoanDetailsRpt();
            detail.CompanyName = userData.CompanyName;
            
            detail.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            detail.CreaterName = usr.FirstName + " " + usr.LastName;
            detail.LenderBrnchName = branchName;
            details.Add(detail);
            rptViewerBranchSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", details));

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }
            List<RptBranchSummary> branchSummary = ra.GetBranchSummarRptDetails(branchId);

            rptViewerBranchSummaryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", branchSummary));

            return rptViewerBranchSummaryPrint;
        }
    }
}