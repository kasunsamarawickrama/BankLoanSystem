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
        /*

            Frontend page: Report page
            Title: view lot inspect report
            Designed: Kanishka SHM
            User story:
            Developed: Kanishka SHM
            Date created: 

        */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;

                //get loan id from query string
                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                RenderReport(loanId);
            }
        }

        /*

            Frontend page: Report Page
            Title: Load details to report and show on browser
            Designed: Kanishka SHM
            User story: 
            Developed: Kanishka SHM
            Date created: 

        */
        public void RenderReport(int loanId)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            rptViewerLotInspection.ProcessingMode = ProcessingMode.Local;
            rptViewerLotInspection.Reset();
            rptViewerLotInspection.LocalReport.EnableExternalImages = true;
            rptViewerLotInspection.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLotInspection.rdlc");
            //rptViewerLotInspection.ZoomMode = ZoomMode.PageWidth;

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            //add current date
            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //set data source to report viwer - 1
            rptViewerLotInspection.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            //get all active units
            List<ReportUnitModels> units = ra.GetAllActiveUnitDetailsRpt(loanId);

            foreach (var unit in units)
            {
                unit.View = false;
            }

            //set data source to report viwer - 2
            rptViewerLotInspection.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));
        }

        /*

            Frontend page: Report Page
            Title: Loan pdf view on browser
            Designed: Kanishka SHM
            User story: 
            Developed: Kanishka SHM
            Date created: 

        */
        public ReportViewer PrintPage(int loanId)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            ReportViewer rptViewerLotInspectionPrint = new ReportViewer();
            rptViewerLotInspectionPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerLotInspectionPrint.Reset();
            rptViewerLotInspectionPrint.LocalReport.EnableExternalImages = true;
            rptViewerLotInspectionPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLotInspection.rdlc");

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            //add current date
            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //set data source to report viwer - 1
            rptViewerLotInspectionPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            //get all active units
            List<ReportUnitModels> units = ra.GetAllActiveUnitDetailsRpt(loanId);

            foreach (var unit in units)
            {
                unit.View = false;
            }

            //set data source to report viwer - 2
            rptViewerLotInspectionPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));

            //return report viwer
            return rptViewerLotInspectionPrint;
        }
    }
}