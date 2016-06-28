using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivDeletedUnits : System.Web.UI.Page
    {
        /*

        Frontend page   : Report page
        Title           : View Delete units on the report
        Designed        : Kanishka SHM
        User story      : 
        Developed       : Kanishka SHM
        Date created    : 06/23/2016

        */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;

                //if session is not null and assign it.
                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                if (string.IsNullOrEmpty(Request.QueryString["startDate"])) return;
                var startDate = DateTime.ParseExact(Request.QueryString["startDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                if (string.IsNullOrEmpty(Request.QueryString["endDate"])) return;
                var endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                //call RenderReport function to show report on report viwer
                RenderReport(loanId, startDate, endDate);
            }
        }

        /*

        Frontend page   : Report page
        Title           : 
        Designed        : Kanishka SHM
        User story      : 
        Developed       : Kanishka SHM
        Date created    : 06/23/2016

        */
        public void RenderReport(int loanId, DateTime startDate, DateTime endDate)
        {
            //Check authentication session is null then return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //Dynamicaly set report viwer propertiece
            rptViewerDeletedUnits.ProcessingMode = ProcessingMode.Local;
            rptViewerDeletedUnits.Reset();
            rptViewerDeletedUnits.LocalReport.EnableExternalImages = true;
            rptViewerDeletedUnits.LocalReport.ReportPath = Server.MapPath("~/Reports/RptDeletedUnits.rdlc");
            rptViewerDeletedUnits.ZoomMode = ZoomMode.PageWidth;

            //Get details of loan
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Set data set to report
            rptViewerDeletedUnits.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            //Get all delete units details
            List<RptDeletedUnit> deletedUnits = ra.RptGetDeletedUnitByLoanIdDateRange(loanId, startDate, endDate);

            //Set data set to report
            rptViewerDeletedUnits.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", deletedUnits));
        }

        public ReportViewer PrintPage(int loanId, DateTime startDate, DateTime endDate)
        {
            //Check authentication session is null then return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            ReportViewer rptViewerDeletedUnitsPrint = new ReportViewer();

            rptViewerDeletedUnitsPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerDeletedUnitsPrint.Reset();
            rptViewerDeletedUnitsPrint.LocalReport.EnableExternalImages = true;
            rptViewerDeletedUnitsPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptDeletedUnits.rdlc");
            rptViewerDeletedUnitsPrint.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerDeletedUnitsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<RptDeletedUnit> deletedUnits = ra.RptGetDeletedUnitByLoanIdDateRange(loanId, startDate, endDate);
            rptViewerDeletedUnitsPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", deletedUnits));

            return rptViewerDeletedUnitsPrint;
        }
    }
}