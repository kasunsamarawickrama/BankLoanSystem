﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptDivAddUnit.aspx.cs" Inherits="BankLoanSystem.Reports.RptDivAddUnit" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>

<form id="form1" runat="server">
    <div>
        <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
        <rsweb:ReportViewer ID="rptViewerAddUnit" runat="server" Font-Size="8pt" Height="100%" Width="100%" repo ShowPrintButton="True">
            
        </rsweb:ReportViewer>

    </div>
</form>