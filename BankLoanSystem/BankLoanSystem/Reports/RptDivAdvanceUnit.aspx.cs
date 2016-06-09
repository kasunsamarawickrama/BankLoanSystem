﻿using System;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int loanId = 0;

                if (Request.QueryString["loanId"] != "")
                    loanId = Convert.ToInt32(Request.QueryString["loanId"]);

                RenderReport(loanId);
            }
        }

        public void RenderReport(int loanId)
        {
            rptViewerAdvanceUnit.ProcessingMode = ProcessingMode.Local;
            rptViewerAdvanceUnit.Reset();
            rptViewerAdvanceUnit.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceUnit.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceUnit.rdlc");
            rptViewerAdvanceUnit.ZoomMode = ZoomMode.PageWidth;
            List<Unit> units = (List<Unit>)Session["AdvItems"];

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerAdvanceUnit.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            if (units != null && units.Count > 0)
            {
                try
                {
                    XElement xEle = new XElement("Units",
                    from unit in units
                    select new XElement("Unit",
                        new XElement("UnitId", unit.UnitId)
                        ));
                    string xmlDoc = xEle.ToString();
                    
                    var advanceUnits = ra.AdvanceUnitsDuringSession(xmlDoc);
                    
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

        public ReportViewer PrintPage(int loanId)
        {
            ReportViewer rptViewerAdvanceUnitPrint = new ReportViewer();

            rptViewerAdvanceUnitPrint.ProcessingMode = ProcessingMode.Local;
            rptViewerAdvanceUnitPrint.Reset();
            rptViewerAdvanceUnitPrint.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceUnitPrint.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceUnit.rdlc");
            
            List<Unit> units = (List<Unit>)Session["AdvItems"];

            ReportAccess ra = new ReportAccess();
            List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(loanId);

            foreach (var dates in details)
            {
                dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
            }

            rptViewerAdvanceUnitPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));

            if (units != null && units.Count > 0)
            {
                try
                {
                    XElement xEle = new XElement("Units",
                    from unit in units
                    select new XElement("Unit",
                        new XElement("UnitId", unit.UnitId)
                        ));
                    string xmlDoc = xEle.ToString();

                    var advanceUnits = ra.AdvanceUnitsDuringSession(xmlDoc);

                    rptViewerAdvanceUnitPrint.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", advanceUnits));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            return rptViewerAdvanceUnitPrint;
        }
    }
}