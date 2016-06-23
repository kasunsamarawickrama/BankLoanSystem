<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptDivLoanSummary.aspx.cs" Inherits="BankLoanSystem.Reports.RptDivLoanSummary" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>

<form id="form1" runat="server">
    <div>
        <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
        <rsweb:ReportViewer ID="rptViewerLoanSummary" runat="server" Font-Size="8pt" Height="100%" Width="100%" Font-Names="Verdana" SizeToReportContent="True" ZoomMode="Percent" ZoomPercent="85" >
            
        </rsweb:ReportViewer>

    </div>
</form>