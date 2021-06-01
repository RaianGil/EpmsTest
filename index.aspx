<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ePMS | electronic Policy Manager Solution</title>
    <script language="javascript" type="text/javascript">
        function GetUserName() {
            var username = document.getElementById('TxtUserName');
            //username.value = '<%=System.Environment.UserName%>';
            //username.value = '<%=System.Web.HttpContext.Current.User.Identity.Name%>';
            // var name = '<%=System.Web.HttpContext.Current.User.Identity.Name%>';
            // window.navigate("default.aspx?u465s8789e987e867r87i9667d=" + username.value);
            window.navigate("Default.aspx");
            //window.location.href = "default.aspx?u465s8789e987e867r87i9667d=" + username.value;
        }
    </script>
    <style type="text/css">
        .style1
        {
            font-size: 24pt;
            font-family: Tahoma;
        }
        .style5
        {
            font-family: Verdana;
            font-size: small;
            color: #0033CC;
        }
        .style6
        {
            font-family: Verdana;
            font-size: 13pt;
            color: #0033CC;
        }
    </style>
</head>
<body onload="GetUserName()">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <asp:TextBox ID="TxtUserName" runat="server" CssClass="loginTB" TabIndex="1" BackColor="White"
            BorderColor="White" BorderStyle="Solid" ForeColor="White"></asp:TextBox>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <span id="result_box" lang="en" tabindex="-1"><span class="style1"><strong>Please</strong></span>
            <span class="style1"><strong>&nbsp;wait, </strong></span>&nbsp;<span class="style1"><strong>while
                the</strong></span> <span class="style1"><strong>&nbsp;system is connected to </strong>
                </span>&nbsp;<span class="style1"><strong>a secure link.<br />
                    <br />
                    <img alt="" src="Images2/loader.gif" style="width: 35px; height: 35px;" />
                    <span><span class="style5"></span><span class="style6">Please wait...</span></span>
    </div>
    </form>
</body>
</html>
