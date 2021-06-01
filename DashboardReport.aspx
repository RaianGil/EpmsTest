<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashboardReport.aspx.cs" Inherits="EPolicy.DashboardReport" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


h2
{
    font-size: 1.5em;
    font-weight: 600;
}

h1, h2, h3, h4, h5, h6
{
    font-size: 1.5em;
    color: #666666;
    font-variant: small-caps;
    text-transform: none;
    font-weight: 200;
    margin-bottom: 0px;
}

p
{
    margin-bottom: 10px;
    line-height: 1.6em;
}


a:link, a:visited
{
    color: #034af3;
}

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 1024px; height: 768px; text-align: center">
    
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
        </asp:ScriptManager>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="600px" 
                        Width="850px" BorderStyle="Solid" Font-Names="Verdana" Font-Size="8pt" 
                        InteractiveDeviceInfos="(Collection)" Visible="False" 
                        WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" 
                        ShowCredentialPrompts="False" ShowDocumentMapButton="False" 
            AsyncRendering="False" DocumentMapWidth="100%">
                        <LocalReport ReportPath="Reports\ReportClaimSROSales.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>
