using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;
using Unit = BankLoanSystem.Models.Unit;

namespace BankLoanSystem.Reports
{
    public partial class RptDivCUrtailmentReceiptDuringSession : System.Web.UI.Page
    {
        /// <summary>
        /// Frontend Page: Pay Curtailment Page(Curtailment Receipt Report)
        /// Title: Display Curtailment Receipt report during session
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
        /// Frontend Page: Pay Curtailment Page(Curtailment Receipt Report)
        /// Title: Display Curtailment Receipt report during session
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

            //set reportviewr properties
            rptViewerCurtailmentReceiptDuringSession.ProcessingMode = ProcessingMode.Local;
            rptViewerCurtailmentReceiptDuringSession.Reset();
            rptViewerCurtailmentReceiptDuringSession.LocalReport.EnableExternalImages = true;
            rptViewerCurtailmentReceiptDuringSession.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurtailmentDuringSession.rdlc");
            rptViewerCurtailmentReceiptDuringSession.ZoomMode = ZoomMode.PageWidth;

            //Get account details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Set data set to report
            rptViewerCurtailmentReceiptDuringSession.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            //get unit list from session
            List<CurtailmentShedule> selectedCurtailmentSchedules =
                (List<CurtailmentShedule>) Session["CurtUnitDuringSession"];

            //assign unit ids
            List<string> uniList = (from item in selectedCurtailmentSchedules
                                    select item.UnitId).Distinct().ToList();

            //create xml string
            XElement xEle = new XElement("Curtailments",
                    from id in uniList
                    select new XElement("Unit",
                        new XElement("UnitId", id)
                        ));
            string xmlDoc = xEle.ToString();

            //get unit curatilment details during session
            List<CurtailmentShedule> unitWithAdvanceAmount = ra.GetCurtailmentPaidDetailsDuringSession(xmlDoc);

            foreach (var item in selectedCurtailmentSchedules)
            {
                item.PurchasePrice = unitWithAdvanceAmount.First(x => x.UnitId == item.UnitId).PurchasePrice;
            }

            if (selectedCurtailmentSchedules != null && selectedCurtailmentSchedules.Count > 0)
            {
                //Set data set to report
                rptViewerCurtailmentReceiptDuringSession.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", selectedCurtailmentSchedules));
            }
        }

        /// <summary>
        /// Frontend Page: Pay Curtailment Page(Curtailment Receipt Report)
        /// Title: Display Curtailment Receipt print page
        /// Designed: Kanishka SHM
        /// User story: 
        /// Developed: Kanishka SHM
        /// Date created: 
        /// </summary>
        public ReportViewer PrintPage(int loanId)
        {
            //Check authentication session is null then return
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            //create reportviwer & set reportviewr properties
            ReportViewer rptViewerCurtailmentReceiptDuringSessionPrint = new ReportViewer();
            rptViewerCurtailmentReceiptDuringSessionPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerCurtailmentReceiptDuringSessionPrint.Reset();
            rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.EnableExternalImages = true;
            rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurtailmentDuringSession.rdlc");

            //Get account details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Set data set to report
            rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            //get unit curatilment details during session
            List<CurtailmentShedule> selectedCurtailmentSchedules =
                (List<CurtailmentShedule>)Session["CurtUnitDuringSession"];

            if (selectedCurtailmentSchedules != null && selectedCurtailmentSchedules.Count > 0)
            {
                rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", selectedCurtailmentSchedules));
            }
            
            return rptViewerCurtailmentReceiptDuringSessionPrint;
        }
    }
}