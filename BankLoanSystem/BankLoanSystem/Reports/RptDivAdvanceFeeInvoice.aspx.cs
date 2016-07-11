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
    public partial class RptDivAdvanceFeeInvoice : System.Web.UI.Page
    {
        /// <summary>
        /// Frontend Page: Report page(Advance Fee invoice Report)
        /// Title: Display Advance Fee invoice
        /// Designed: Kasun Samarawickrama
        /// User story: 
        /// Developed: Kasun Samarawickrama
        /// Date created: 
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;
                string feeType = "advanceFee"; //Report type

                //if session is not null and assign it.
                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                if (string.IsNullOrEmpty(Request.QueryString["startDate"])) return;
                var startDate = DateTime.ParseExact(Request.QueryString["startDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                if (string.IsNullOrEmpty(Request.QueryString["endDate"])) return;
                var endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", CultureInfo.InvariantCulture);

                //Get Advance Fee invoice details during time period
                ReportAccess ra = new ReportAccess();
                List<RptFee> advanceFeeInvoice = ra.GetFeeInvoiceByDateRange(loanId, feeType, startDate, endDate);

                //if result count is greater then zero show report, otherwise give a message
                if (advanceFeeInvoice.Count > 0)
                {
                    //call RenderReport function to show report on report viwer
                    RenderReport(loanId, startDate, endDate,feeType, advanceFeeInvoice);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowFrame", "ShowDive();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideFrame", "HideDive();", true);
                }
            }
        }

        /// <summary>
        /// Frontend Page: Report page(Advance Fee invoice Report)
        /// Title: Display Advance Fee invoice
        /// Designed: Kasun Samarawickrama
        /// User story: 
        /// Developed: Kasun Samarawickrama
        /// Date created: 
        /// </summary>
        public void RenderReport(int loanId, DateTime startDate, DateTime endDate, string feeType, List<RptFee> advanceFeeInvoice)
        {
            //Check authentication session is null then return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            rptViewerAdvanceFeeInvoice.ProcessingMode = ProcessingMode.Local;
            rptViewerAdvanceFeeInvoice.Reset();
            rptViewerAdvanceFeeInvoice.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceFeeInvoice.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceFeeInvoice.rdlc");
            rptViewerAdvanceFeeInvoice.ZoomMode = ZoomMode.PageWidth;

            //Get account details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Set data set to report
            rptViewerAdvanceFeeInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerAdvanceFeeInvoice.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", advanceFeeInvoice));
        }

        /// <summary>
        /// Frontend Page: Report page(Advance Fee invoice Report)
        /// Title: Display Advance Fee invoice
        /// Designed: Kasun Samarawickrama
        /// User story: 
        /// Developed: Kasun Samarawickrama
        /// Date created: 
        /// </summary>
        public ReportViewer PrintPage(int loanId, DateTime startDate, DateTime endDate)
        {
            //Check authentication session is null then return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            ReportViewer rptViewerAdvanceFeeInvoicePrint = new ReportViewer();
            rptViewerAdvanceFeeInvoicePrint.ProcessingMode = ProcessingMode.Local;
            rptViewerAdvanceFeeInvoicePrint.Reset();
            rptViewerAdvanceFeeInvoicePrint.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceFeeInvoicePrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceFeeInvoice.rdlc");

            //Get account details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Get Advance Fee invoice details during time period
            List<RptFee> advanceFeeInvoice = ra.GetFeeInvoiceByDateRange(loanId, "advanceFee", startDate, endDate);

            //Set data set to report
            rptViewerAdvanceFeeInvoicePrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerAdvanceFeeInvoicePrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", advanceFeeInvoice));

            //return reportviwer
            return rptViewerAdvanceFeeInvoicePrint;
        }
    }
}