<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptDivCurtailmentReceipt.aspx.cs" Inherits="BankLoanSystem.Reports.RptDivCurtailmentReceipt" %>
<%@ Register TagPrefix="rsweb" Namespace="Microsoft.Reporting.WebForms" Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" %>

<form id="form1" runat="server">
    <div>
        <asp:scriptmanager id="ScriptManager1" runat="server"></asp:scriptmanager>
        <rsweb:ReportViewer ID="rptViewerCurtailmentReceipt" runat="server" Font-Size="8pt" Height="100%" Width="100%" repo ShowPrintButton="True" AsyncRendering="False" SizeToReportContent="True">
            
        </rsweb:ReportViewer>
        
        <script type="text/JavaScript">
            function doResize() {
                var viewer = document.getElementById("rptViewerLotInspection");
                var htmlheight = document.documentElement.clientHeight;
                //alert(htmlheight);
        viewer.style.height = (htmlheight - 150) + "px";
    }

    //window.onresize = function resize() {
    //    doResize();
    //}
    //doResize();
        </script>

    </div>
</form>