<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptDivCUrtailmentReceiptDuringSession.aspx.cs" Inherits="BankLoanSystem.Reports.RptDivCUrtailmentReceiptDuringSession" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>

<form id="form1" runat="server">
    <div>
        <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
        <rsweb:ReportViewer ID="rptViewerCurtailmentReceiptDuringSession" runat="server" Font-Size="8pt" Height="650px" Width="100%" repo ShowPrintButton="True" AsyncRendering="False">
            
        </rsweb:ReportViewer>

    </div>
</form>