using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankLoanSystem.Reports
{
    public partial class RptDivLotInspectionFeeReceipt : System.Web.UI.Page
    {
        /// <summary>
        /// Frontend Page: Report(lot inspection fee receipt)
        /// Title:load details of paid lot inspection fees
        /// Deigned:Piyumi Perera
        /// User story:
        /// Developed:Piyumi Perera
        /// Date created:
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;
                //check selected loan id is not empty and assign to variable
                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);
                //check selected start date is not empty and assign to variable
                if (string.IsNullOrEmpty(Request.QueryString["startDate"])) return;
                var startDate = DateTime.ParseExact(Request.QueryString["startDate"], "MM/dd/yyyy", new CultureInfo("en-US"));
                //check selected end date is not empty and assign to variable
                if (string.IsNullOrEmpty(Request.QueryString["endDate"])) return;
                var endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", new CultureInfo("en-US"));

                ReportAccess ra = new ReportAccess();
                //get paid lot inspection fee details
                List<RptFee> lotInspectionFee = ra.GetFeeReceiptByDateRange(loanId, "lotInspectionFee", startDate, endDate);
                //check list count is >0
                if(lotInspectionFee.Count>0)
                {
                    //load report
                    RenderReport(loanId, startDate, endDate, lotInspectionFee);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowFrame", "ShowDive();", true);
                }
                else
                {
                    //show no data found message
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideFrame", "HideDive();", true);
                }
            }
        }
        /// <summary>
        /// Frontend Page: Report(lot inspection fee receipt)
        /// Title:load details of paid lot inspection fee 
        /// Deigned:Piyumi Perera
        /// User story:
        /// Developed:Piyumi Perera
        /// Date created: 
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="lotInspectionFee"></param>
        public void RenderReport(int loanId, DateTime startDate, DateTime endDate, List<RptFee> lotInspectionFee)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            rptViewerLotInspectionFeeReceipt.ProcessingMode = ProcessingMode.Local;
            rptViewerLotInspectionFeeReceipt.Reset();
            rptViewerLotInspectionFeeReceipt.LocalReport.EnableExternalImages = true;
            rptViewerLotInspectionFeeReceipt.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLotInspectionFeeReceipt.rdlc");
            rptViewerLotInspectionFeeReceipt.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            //get loan details of selected loan
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId, userData.UserId);

            foreach (var dates in details)
            {
                //assign date fields values
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }
            //add data sources
            rptViewerLotInspectionFeeReceipt.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerLotInspectionFeeReceipt.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", lotInspectionFee));
        }
        /// <summary>
        /// Frontend Page: Report(lot inspection fee receipt)
        /// Title:print lot inspection fee receipt report
        /// Deigned:Piyumi Perera
        /// User story:
        /// Developed:Piyumi Perera
        /// Date created: 
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ReportViewer PrintPage(int loanId, DateTime startDate, DateTime endDate)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            ReportViewer rptViewerLotInspectionFeeReceiptPrint = new ReportViewer();
            rptViewerLotInspectionFeeReceiptPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerLotInspectionFeeReceiptPrint.Reset();
            rptViewerLotInspectionFeeReceiptPrint.LocalReport.EnableExternalImages = true;
            rptViewerLotInspectionFeeReceiptPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLotInspectionFeeReceipt.rdlc");

            ReportAccess ra = new ReportAccess();
            //get loan details of selected loan
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId, userData.UserId);

            foreach (var dates in details)
            {
                //assign date fields values
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //get paid lot inspection fee details
            List<RptFee> curtailments = ra.GetFeeReceiptByDateRange(loanId, "lotInspectionFee", startDate, endDate);
            //add data sources
            rptViewerLotInspectionFeeReceiptPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerLotInspectionFeeReceiptPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));

            return rptViewerLotInspectionFeeReceiptPrint;
        }
    }
}