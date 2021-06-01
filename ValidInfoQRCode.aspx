<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ValidInfoQRCode.aspx.cs"
    Inherits="ValidInfoQRCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" href="Images2/LogoGuardian.ico" type="image/x-icon" />
    <title>Guardian Insurance</title>
    <link rel="stylesheet" href="css/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="css/main.css" />
    <link href="css/fonts.css" rel="stylesheet" />
     <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"
        rel="stylesheet" integrity="sha384-T8Gy5hrqNKT+hzMclPo118YTQO6cYprQmhrYwIiQ/3axmI1hQomh7Ud2hPOy8SP1"
        crossorigin="anonymous" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
    </script>
</head>
<body style="background-color:White;">
    <form id="form1" runat="server">
    <div style="background-color: #17529B; height: 100%; text-align:center;">
      <img src="Images2/guardian-login-logo.png" class="sideMenu-logo img-responsive img-centered"
            width="414" height="169" alt="" style="margin-bottom: 15px" />
    </div>
    <div align="center" >
        <h1 class="page-header" style="color:#17529B;font-size:60px; font-family: Gotham Book;" >Validate Info</h1>
    </div>
    <div align="center" >
     <br />
      <br />
       <br />
         <br />
        <asp:Label ID="lblMessage" runat="server" 
            style="color:Red;font-size:48px;font-family: Gotham Book;" Font-Size="48px"></asp:Label>    
        <br />
          <br />
        <asp:Label ID="lblMessage2" runat="server" 
            style="color:Red;font-size:48px;font-family: Gotham Book;" Font-Size="48px"></asp:Label>    
    </div>
    </form>
</body>
</html>
