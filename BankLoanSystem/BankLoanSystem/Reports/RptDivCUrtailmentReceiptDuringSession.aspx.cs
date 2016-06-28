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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //int loanId = 1421;
                //RenderReport(loanId);
                int loanId = 0;

                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                RenderReport(loanId);
            }
        }


        public void RenderReport(int loanId)
        {
            if (Session["AuthenticatedUser"] == null) return;
            User userData = (User)Session["AuthenticatedUser"];

            rptViewerCurtailmentReceiptDuringSession.ProcessingMode = ProcessingMode.Local;
            rptViewerCurtailmentReceiptDuringSession.Reset();
            rptViewerCurtailmentReceiptDuringSession.LocalReport.EnableExternalImages = true;
            rptViewerCurtailmentReceiptDuringSession.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurtailmentDuringSession.rdlc");
            rptViewerCurtailmentReceiptDuringSession.ZoomMode = ZoomMode.PageWidth;

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerCurtailmentReceiptDuringSession.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            List<CurtailmentShedule> selectedCurtailmentSchedules =
                (List<CurtailmentShedule>) Session["CurtUnitDuringSession"];

            List<string> uniList = (from item in selectedCurtailmentSchedules
                                    select item.UnitId).Distinct().ToList();

            XElement xEle = new XElement("Curtailments",
                    from id in uniList
                    select new XElement("Unit",
                        new XElement("UnitId", id)
                        ));
            string xmlDoc = xEle.ToString();
            List<CurtailmentShedule> unitWithAdvanceAmount = ra.GetCurtailmentPaidDetailsDuringSession(xmlDoc);

            foreach (var item in selectedCurtailmentSchedules)
            {
                item.PurchasePrice = unitWithAdvanceAmount.First(x => x.UnitId == item.UnitId).PurchasePrice;
            }

            if (selectedCurtailmentSchedules != null && selectedCurtailmentSchedules.Count > 0)
            {
                rptViewerCurtailmentReceiptDuringSession.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", selectedCurtailmentSchedules));
            }
        }

        public ReportViewer PrintPage(int loanId)
        {
            if (Session["AuthenticatedUser"] == null) return null;
            User userData = (User)Session["AuthenticatedUser"];

            ReportViewer rptViewerCurtailmentReceiptDuringSessionPrint = new ReportViewer();
            rptViewerCurtailmentReceiptDuringSessionPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerCurtailmentReceiptDuringSessionPrint.Reset();
            rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.EnableExternalImages = true;
            rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptCurtailmentDuringSession.rdlc");

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerCurtailmentReceiptDuringSessionPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

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