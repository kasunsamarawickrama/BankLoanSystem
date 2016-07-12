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
    public partial class RptDivLotInspectionFeeInvoice : System.Web.UI.Page
    {
        /// <summary>
        /// Frontend Page: Report(lot inspection fee invoice)
        /// Title:load details of lot inspection fee invoice
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
                //assign fee type
                string feeType = "lotInspectionFee";
                //check selected loan id is not empty and assign to variable
                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);
                //check selected start date is not empty and assign to variable
                if (string.IsNullOrEmpty(Request.QueryString["startDate"])) return;
                var startDate = DateTime.ParseExact(Request.QueryString["startDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //check selected end date is not empty and assign to variable
                if (string.IsNullOrEmpty(Request.QueryString["endDate"])) return;
                var endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                ReportAccess ra = new ReportAccess();
                //get all lot inspection details which match with the given date range
                List<RptFee> lotInspectionFeeInvoice = ra.GetFeeInvoiceByDateRange(loanId, feeType, startDate, endDate);
                //check list count is>0
                if(lotInspectionFeeInvoice.Count>0)
                {
                    //load report
                    RenderReport(loanId, startDate, endDate, feeType, lotInspectionFeeInvoice);
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
        /// Frontend Page: Report(lot inspection fee invoice)
        /// Title:load details of lot inspection fee invoice
        /// Deigned:Piyumi Perera
        /// User story:
        /// Developed:Piyumi Perera
        /// Date created: 
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="feeType"></param>
        /// <param name="lotInspectionFeeInvoice"></param>
        public void RenderReport(int loanId, DateTime startDate, DateTime endDate, string feeType, List<RptFee> lotInspectionFeeInvoice)
        {
            //check authentication session is null, if null return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            rptViewerLotInspectionFeeInvoice.ProcessingMode = ProcessingMode.Local;
            rptViewerLotInspectionFeeInvoice.Reset();
            rptViewerLotInspectionFeeInvoice.LocalReport.EnableExternalImages = true;
            rptViewerLotInspectionFeeInvoice.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLotInspectionFeeInvoice.rdlc");
            rptViewerLotInspectionFeeInvoice.ZoomMode = ZoomMode.PageWidth;

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
            rptViewerLotInspectionFeeInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerLotInspectionFeeInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", lotInspectionFeeInvoice));
        }
        /// <summary>
        /// Frontend Page: Report(lot inspection fee invoice)
        /// Title:print lot inspection fee invoice report
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

            ReportViewer rptViewerLotInspectionFeeInvoicePrint = new ReportViewer();
            rptViewerLotInspectionFeeInvoicePrint.ProcessingMode = ProcessingMode.Local;
            rptViewerLotInspectionFeeInvoicePrint.Reset();
            rptViewerLotInspectionFeeInvoicePrint.LocalReport.EnableExternalImages = true;
            rptViewerLotInspectionFeeInvoicePrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptLotInspectionFeeInvoice.rdlc");

            ReportAccess ra = new ReportAccess();
            //get loan details of selected loan
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId, userData.UserId);

            foreach (var dates in details)
            {
                //assign date fields
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //get lot inspection fee details which match with the given date range
            List<RptFee> lotInspectionFeeInvoice = ra.GetFeeInvoiceByDateRange(loanId, "lotInspectionFee", startDate, endDate);
            //add data sources
            rptViewerLotInspectionFeeInvoicePrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerLotInspectionFeeInvoicePrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", lotInspectionFeeInvoice));

            
            return rptViewerLotInspectionFeeInvoicePrint;
        }
    }
}