using System;
using System.Collections.Generic;
using System.Web.UI;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivAddUnit : Page
    {
        /// <summary>
        /// Frontend Page: Add Unit(Add Unit Report)
        /// Title: Display Advance Units
        /// Designed: Kanishka SHM
        /// User story: 
        /// Developed: Kanishka SHM
        /// Date created: 
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;

                //if session is not null and assign it.
                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                //call RenderReport function to show report on report viwer
                RenderReport(loanId);
            }
        }

        /// <summary>
        /// Frontend Page: Add Unit(Add Unit Report)
        /// Title: Display Advance Units
        /// Designed: Kanishka SHM
        /// User story: 
        /// Developed: Kanishka SHM
        /// Date created: 
        /// </summary>
        public void RenderReport(int loanId)
        {
            //Check authentication session is null then return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            rptViewerAddUnit.ProcessingMode = ProcessingMode.Local;
            rptViewerAddUnit.Reset();
            rptViewerAddUnit.LocalReport.EnableExternalImages = true;
            rptViewerAddUnit.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAddUnit.rdlc");
            rptViewerAddUnit.ZoomMode = ZoomMode.PageWidth;

            //Get account details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Set data set to report
            rptViewerAddUnit.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            //Get just added units details
            List<RptAddUnit> units = ra.GetJustAddedUnitDetails(userData.UserId, loanId);

            //Set data set to report
            rptViewerAddUnit.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));


        }

        /// <summary>
        /// Frontend Page: Add Unit(Add Unit Report)
        /// Title: Display Advance Units print page
        /// Designed: Kanishka SHM
        /// User story: 
        /// Developed: Kanishka SHM
        /// Date created: 
        /// </summary>
        public ReportViewer PrintPage(int loanId, int userId)
        {
            //Check authentication session is null then return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            ReportViewer rptViewerAddUnitPrint = new ReportViewer();

            rptViewerAddUnitPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerAddUnitPrint.Reset();
            rptViewerAddUnitPrint.LocalReport.EnableExternalImages = true;
            rptViewerAddUnitPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAddUnit.rdlc");
            rptViewerAddUnitPrint.ZoomMode = ZoomMode.PageWidth;

            //Get account details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Set data set to report
            rptViewerAddUnitPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            //
            List<RptAddUnit> units = ra.GetJustAddedUnitDetails(userId, loanId);

            //Set data set to report
            rptViewerAddUnitPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));

            //return reportviwer
            return rptViewerAddUnitPrint;
        }
    }
}