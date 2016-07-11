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
    public partial class RptDivAdvanceFeeReceipt : System.Web.UI.Page
    {
        /// <summary>
        /// Frontend Page: Report page(Advance Fee Receipt Report)
        /// Title: Display Advance Fee Receipt
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

                //if session is not null and assign it.
                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                if (string.IsNullOrEmpty(Request.QueryString["startDate"])) return;
                var startDate = DateTime.ParseExact(Request.QueryString["startDate"], "MM/dd/yyyy", new CultureInfo("en-US"));

                if (string.IsNullOrEmpty(Request.QueryString["endDate"])) return;
                var endDate = DateTime.ParseExact(Request.QueryString["endDate"], "MM/dd/yyyy", new CultureInfo("en-US"));

                //Get Advance Fee receipt during time period
                ReportAccess ra = new ReportAccess();
                List<RptFee> advanceFeeReceipt = ra.GetFeeReceiptByDateRange(loanId, "advanceFee", startDate, endDate);

                //if result count is greater then zero show report, otherwise give a message
                if (advanceFeeReceipt.Count > 0)
                {
                    //call RenderReport function to show report on report viwer
                    RenderReport(loanId, startDate, endDate, advanceFeeReceipt);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowFrame", "ShowDive();", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "HideFrame", "HideDive();", true);
                }
            }


        }

        /// <summary>
        /// Frontend Page: Report page(Advance Fee Receipt Report)
        /// Title: Display Advance Fee Receipt
        /// Designed: Kasun Samarawickrama
        /// User story: 
        /// Developed: Kasun Samarawickrama
        /// Date created: 
        /// </summary>
        public void RenderReport(int loanId, DateTime startDate, DateTime endDate, List<RptFee> advanceFeeReceipt)
        {
            //Check authentication session is null then return
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            rptViewerAdvanceFeeReceipt.ProcessingMode = ProcessingMode.Local;
            rptViewerAdvanceFeeReceipt.Reset();
            rptViewerAdvanceFeeReceipt.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceFeeReceipt.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceFeeReceipt.rdlc");
            rptViewerAdvanceFeeReceipt.ZoomMode = ZoomMode.PageWidth;

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
            rptViewerAdvanceFeeReceipt.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerAdvanceFeeReceipt.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", advanceFeeReceipt));
        }

        /// <summary>
        /// Frontend Page: Report page(Advance Fee Receipt Report)
        /// Title: Display Advance Fee Receipt
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

            ReportViewer rptViewerAdvanceFeeReceiptPrint = new ReportViewer();

            rptViewerAdvanceFeeReceiptPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerAdvanceFeeReceiptPrint.Reset();
            rptViewerAdvanceFeeReceiptPrint.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceFeeReceiptPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceFeeReceipt.rdlc");
            rptViewerAdvanceFeeReceiptPrint.ZoomMode = ZoomMode.PageWidth;

            //Get account details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.StartRange = startDate.ToString("MM/dd/yyyy");
                dates.EndRange = endDate.ToString("MM/dd/yyyy");
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Get Advance Fee receipt during time period
            List<RptFee> curtailments = ra.GetFeeReceiptByDateRange(loanId, "advanceFee", startDate, endDate);

            //Set data set to report
            rptViewerAdvanceFeeReceiptPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
            rptViewerAdvanceFeeReceiptPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", curtailments));

            //return reportviwer
            return rptViewerAdvanceFeeReceiptPrint;
        }
    }
}