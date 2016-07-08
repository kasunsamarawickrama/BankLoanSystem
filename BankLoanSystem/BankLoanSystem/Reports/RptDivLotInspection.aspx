﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptDivLotInspection.aspx.cs" Inherits="BankLoanSystem.Reports.RptDivLotInspection" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>

<script src="/scripts/jquery-1.10.2.min.js"></script>
<form id="form1" runat="server">
    <div><span id="errorMsg" style="float: right;margin-right: 179px;color: red;"></span></div>
    <div>
        <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
        <rsweb:ReportViewer ID="rptViewerLotInspection" runat="server" Font-Size="8pt" Height="100%" Width="100%" Font-Names="Verdana" SizeToReportContent="True" ZoomMode="Percent" ZoomPercent="85" >
            
        </rsweb:ReportViewer>

    </div>
    
    <script type="text/javascript">
        function HideDive() {
            //alert("There is no data to display for the report. Please change your criteria and try again.");
            $('#errorMsg').text('There is no data to display for the report. Please change your criteria and try again.');
            parent.ParentPrintHide();
        }

        function ShowDive() {
            parent.ParentPrintShow();
        }
    </script>

</form>
