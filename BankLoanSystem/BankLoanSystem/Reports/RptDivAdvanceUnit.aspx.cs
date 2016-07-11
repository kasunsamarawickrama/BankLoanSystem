using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;
using BankLoanSystem.Code;
using BankLoanSystem.DAL;
using BankLoanSystem.Models;
using Microsoft.Reporting.WebForms;

namespace BankLoanSystem.Reports
{
    public partial class RptDivAdvanceUnit : Page
    {
        /// <summary>
        /// Frontend Page: Advance Unit Page(Advance Unit Report)
        /// Title: Display Advance Units during session
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
        /// Frontend Page: Advance Unit Page(Advance Unit Report)
        /// Title: Display Advance Units during session
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
            rptViewerAdvanceUnit.ProcessingMode = ProcessingMode.Local;
            rptViewerAdvanceUnit.Reset();
            rptViewerAdvanceUnit.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceUnit.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceUnit.rdlc");
            rptViewerAdvanceUnit.ZoomMode = ZoomMode.PageWidth;
            List<Unit> units = (List<Unit>)Session["AdvItems"];

            //Get account details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Set data set to report
            rptViewerAdvanceUnit.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            //if unit count is not null and count is greater than 0, create xml string
            if (units != null && units.Count > 0)
            {
                try
                {
                    //create xml string
                    XElement xEle = new XElement("Units",
                    from unit in units
                    select new XElement("Unit",
                        new XElement("UnitId", unit.UnitId)
                        ));
                    string xmlDoc = xEle.ToString();
                    
                    //get all advance unit during session
                    var advanceUnits = ra.AdvanceUnitsDuringSession(xmlDoc);

                    //Set data set to report
                    rptViewerAdvanceUnit.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", advanceUnits));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {
                units = new List<Unit>();
            }
        }

        /// <summary>
        /// Frontend Page: Advance Unit Page(Advance Unit Report)
        /// Title: Display Advance Units during session
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
            ReportViewer rptViewerAdvanceUnitPrint = new ReportViewer();
            rptViewerAdvanceUnitPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerAdvanceUnitPrint.Reset();
            rptViewerAdvanceUnitPrint.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceUnitPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceUnit.rdlc");
            
            List<Unit> units = (List<Unit>)Session["AdvItems"];

            //Get account details
            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.TopHeaderDetails(loanId, userData.UserId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            //Set data set to report
            rptViewerAdvanceUnitPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            //if unit count is not null and count is greater than 0, create xml string
            if (units != null && units.Count > 0)
            {
                try
                {
                    //create xml string
                    XElement xEle = new XElement("Units",
                    from unit in units
                    select new XElement("Unit",
                        new XElement("UnitId", unit.UnitId)
                        ));
                    string xmlDoc = xEle.ToString();

                    //get all advance unit during session
                    var advanceUnits = ra.AdvanceUnitsDuringSession(xmlDoc);

                    //Set data set to report
                    rptViewerAdvanceUnitPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", advanceUnits));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            //return reportviwer
            return rptViewerAdvanceUnitPrint;
        }
    }
}