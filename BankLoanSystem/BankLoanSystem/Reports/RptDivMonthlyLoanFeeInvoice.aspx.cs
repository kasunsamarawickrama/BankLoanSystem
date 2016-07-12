using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivMonthlyLoanFeeInvoice : Page
    {
        /// <summary>
        /// Frontend Page: Report(monthly loan fee invoice)
        /// Title:load details of monthly loan fee invoice
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
                string feeType = "monthlyLoanFee";
                //check slected loan id is not empty and assign to variable
                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);
                //check selected start date is not empty and assign to variable
                if (string.IsNullOrEmpty(Request.QueryString["startDate"])) return;
                var startDate = DateTime.ParseExact(Request.QueryString["startDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);
                //check selected end date is not empty and assign to variable
                if (string.IsNullOrEmpty(Request.QueryString["endDate"])) return;
                var endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                ReportAccess ra = new ReportAccess();
                //get monthly loan fee details which match with given date range
                List<RptFee> monthlyLoanFeeInvoice = ra.GetFeeInvoiceByDateRange(loanId, feeType, startDate, endDate);
                //check list count is>0
                if (monthlyLoanFeeInvoice.Count > 0)
                {
                    //load report
                    RenderReport(loanId, startDate, endDate, feeType, monthlyLoanFeeInvoice);
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
        /// Frontend Page: Report(monthly loan fee invoice)
        /// Title:load details of monthly loan fee invoice
        /// Deigned:Piyumi Perera
        /// User story:
        /// Developed:Piyumi Perera
        /// Date created:
        /// </summary>
        /// <param name="loanId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="feeType"></param>
        /// <param name="monthlyLoanFeeInvoice"></param>
        public void RenderReport(int loanId, DateTime startDate, DateTime endDate, string feeType, List<RptFee> monthlyLoanFeeInvoice)
        {
            //check session is null
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            rptViewerMonthlyLoanFeeInvoice.ProcessingMode = ProcessingMode.Local;
            rptViewerMonthlyLoanFeeInvoice.Reset();
            rptViewerMonthlyLoanFeeInvoice.LocalReport.EnableExternalImages = true;
            rptViewerMonthlyLoanFeeInvoice.LocalReport.ReportPath = Server.MapPath("~/Reports/RptMonthlyLoanFeeInvoice.rdlc");
            rptViewerMonthlyLoanFeeInvoice.ZoomMode = ZoomMode.PageWidth;

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
            rptViewerMonthlyLoanFeeInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerMonthlyLoanFeeInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", monthlyLoanFeeInvoice));
        }
        /// <summary>
        /// Frontend Page: Report(monthly loan fee invoice)
        /// Title:print monthly loan fee invoice report
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
            //check session is null
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            ReportViewer rptViewerMonthlyLoanFeeInvoicePrint = new ReportViewer();
            rptViewerMonthlyLoanFeeInvoicePrint.ProcessingMode = ProcessingMode.Local;
            rptViewerMonthlyLoanFeeInvoicePrint.Reset();
            rptViewerMonthlyLoanFeeInvoicePrint.LocalReport.EnableExternalImages = true;
            rptViewerMonthlyLoanFeeInvoicePrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptMonthlyLoanFeeInvoice.rdlc");

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

            //get monthly loan fee details which match with given date range
            List<RptFee> monthlyLoanFeeInvoice = ra.GetFeeInvoiceByDateRange(loanId, "monthlyLoanFee", startDate, endDate);
            //add data sources
            rptViewerMonthlyLoanFeeInvoicePrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerMonthlyLoanFeeInvoicePrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", monthlyLoanFeeInvoice));
            
            return rptViewerMonthlyLoanFeeInvoicePrint;
        }
    }
}