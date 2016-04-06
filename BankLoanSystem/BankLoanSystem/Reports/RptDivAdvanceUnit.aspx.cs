using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;
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
                RenderReport();
            }
        }

        public void RenderReport()
        {
            rptViewerAdvanceUnit.Reset();
            rptViewerAdvanceUnit.LocalReport.EnableExternalImages = true;
            rptViewerAdvanceUnit.LocalReport.ReportPath = Server.MapPath("~/Reports/RptAdvanceUnit.rdlc");
            List<RptAddUnit> advanceUnits = new List<RptAddUnit>();
            List<Unit> units = (List<Unit>)Session["AdvItems"];

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
                    ReportAccess ra = new ReportAccess();
                    advanceUnits = ra.AdvanceUnitsDuringSession(xmlDoc);
                    List<LoanDetailsRpt> details = ra.GetLoanDetailsRpt(advanceUnits[0].LoanId);

                    foreach (var dates in details)
                    {
                        dates.ReportDate = DateTime.Now.ToString("MM/dd/yyyy");
                    }

                    rptViewerAdvanceUnit.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", details));
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
    }
}