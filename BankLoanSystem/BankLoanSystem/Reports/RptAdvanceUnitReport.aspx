<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptAdvanceUnitReport.aspx.cs" Inherits="BankLoanSystem.Reports.RptAdvanceUnitReport" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>
<script src="/scripts/jquery-1.10.2.min.js"></script>
<form id="form1" runat="server">
    
    <div>
        <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
        <rsweb:ReportViewer ID="rptViewerAdvanceUnitRpt" runat="server" Font-Size="8pt" Height="100%" Width="100%" repo ShowPrintButton="True">
            
        </rsweb:ReportViewer>

    </div>
    
    <script type="text/javascript">
        function HideDive() {
            //alert("There is no data to display for the report. Please change your criteria and try again.");
            parent.ParentPrintHide();
        }

        function ShowDive() {
            parent.ParentPrintShow();
        }
    </script>

</form>