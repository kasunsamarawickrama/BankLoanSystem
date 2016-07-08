using System;
using System.Collections.Generic;
using System.IO;
using BankLoanSystem.Code;
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

                ReportAccess ra = new ReportAccess();
                //Get unit details with payment details
                List<ReportFullInventoryUnit> units = ra.GetFullInventoryByLoanId(loanId);

                if (units.Count > 0)
                {
                    RenderReport(loanId, units);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowFrame", "ShowDive();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideFrame", "HideDive();", true);
                }
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
        public void RenderReport(int loanId, List<ReportFullInventoryUnit> units)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            //set report viewr property dynamically
            rptViewerFullInventory.ProcessingMode=ProcessingMode.Local;
            rptViewerFullInventory.Reset();
            rptViewerFullInventory.LocalReport.EnableExternalImages = true;
            rptViewerFullInventory.LocalReport.ReportPath = Server.MapPath("~/Reports/RptFullInventory.rdlc");
            rptViewerFullInventory.ZoomMode = ZoomMode.PageWidth;

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            //add current date
            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //set data source to report viwer
            rptViewerFullInventory.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerFullInventory.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));
        }

        /*

            Frontend page: Report Page
            Title: Load pdf view on browser
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
            ReportViewer rptViewerFullInventoryPrint = new ReportViewer();
            rptViewerFullInventoryPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerFullInventoryPrint.Reset();
            rptViewerFullInventoryPrint.LocalReport.EnableExternalImages = true;
            rptViewerFullInventoryPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptFullInventory.rdlc");

            //get report header details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Get unit details with payment details
            List<ReportFullInventoryUnit> units = ra.GetFullInventoryByLoanId(loanId);

            //set data source to report viwer
            rptViewerFullInventoryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerFullInventoryPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", units));
            
            //return report view
            return rptViewerFullInventoryPrint;
        }
    }
}