<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptDivLotInspection.aspx.cs" Inherits="BankLoanSystem.Reports.RptDivLotInspection" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>

<form id="form1" runat="server">
    <div>
        <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
        <rsweb:ReportViewer ID="rptViewerLotInspection" runat="server" Font-Size="8pt" Height="100%" Width="100%" repo Font-Names="Verdana" ProcessingMode="Remote" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
            <ServerReport ReportPath="/Reports/RptLotInspection.rdlc" ReportServerUrl="http://www.dfp.carmartnet.net/reportserver" />
            
        </rsweb:ReportViewer>

    </div>
</form>
