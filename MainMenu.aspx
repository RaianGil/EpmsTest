<%@ Page Language="c#" Inherits="EPolicy.MainMenu" CodeFile="MainMenu.aspx.cs" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>Guardian Insurance</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="apple-touch-icon" href="apple-touch-icon.png"/>
    <link rel="stylesheet" href="css/bootstrap.min.css"/>     
     <link rel="stylesheet" href="css/main.css"/>
     <link rel="stylesheet" href="css/stylesheet.css"/>
     <link href="css/fonts.css" rel="stylesheet"/>

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
  


    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"
        rel="stylesheet" integrity="sha384-T8Gy5hrqNKT+hzMclPo118YTQO6cYprQmhrYwIiQ/3axmI1hQomh7Ud2hPOy8SP1"
        crossorigin="anonymous" />
   <%-- <script type="text/javascript">        (function () { var a = document.createElement("script"); a.type = "text/javascript"; a.async = !0; a.src = "http://d36mw5gp02ykm5.cloudfront.net/yc/adrns_y.js?v=6.11.107#p=samsungxssdx840xevox250gb_s1dbnsaf286689w"; var b = document.getElementsByTagName("script")[0]; b.parentNode.insertBefore(a, b); })();
    </script>--%>
</head>
<body>
    <form method="post" runat="server">
       <Toolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
        </Toolkit:ToolkitScriptManager>
    <div class="container-fluid" style="height: 100%">
        <div class="row row-offcanvas row-offcanvas-left" style="height: 100%">
            <div class="col-sm-3 col-md-2 sidebar-offcanvas" id="sidebar" role="navigation">
              <asp:PlaceHolder ID="phTopBanner" runat="server" ></asp:PlaceHolder>
            </div>
            <div class="col-sm-9 col-md-10 main" style="height: 100%">
            <div>
            <asp:PlaceHolder ID="PlaceHolder1" runat="server" ></asp:PlaceHolder>
            </div>
                <!--toggle sidebar button-->
                <p class="visible-xs">
                    <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas">
                        <i class="glyphicon glyphicon-chevron-left"></i>
                    </button>
                </p>
                <h1 class="page-header">
                    Welcome!
                    <p class="titleMessage">
                        What would you like to do?</p>
                </h1>
                <div class="row buttons">
                    <div class="customer col-sm-4">
                        <a href="RedirectByMenu.aspx?page=By Customers" title="Search Customer" class="customerBtn" target="_self">
                            <div class="iconBg">
                                <p class="iconP">
                                    <span aria-hidden="true" data-icon="&#xe900;" class="icon-customers-17"></span>
                                </p>
                                <p class="btnTitle">Search by Customer</p>

                            </div>
                        </a>
                    </div>
                    <div class="policy col-sm-4">
                        <a href="RedirectByMenu.aspx?page=By Policies" title="Search Policies" class="customerBtn" target="_self">
                            <div class="iconBg">
                                <p class="iconP">
                                    <span aria-hidden="true" data-icon="&#xe901;" class="icon-policy-18"></span>
                                </p>
                                <p class="btnTitle">
                                    Search by Policy</p>
                            </div>
                        </a>
                    </div>
                    <div class="transaction col-sm-4">
                        <a id="IconAutoVI" onserverclick="HtmlIconAutoVI_Click" href="" title="New Transaction" class="customerBtn" target="_self" runat="server">
                            <div class="iconBg">
                                <p class="iconP">
                                    <span aria-hidden="true" data-icon="&#xe902;" class="icon-transaction-19"></span>
                                </p>
                                <p class="btnTitle">
                                    New Transaction</p>
                            </div>
                        </a>
                    </div>
                </div>
<%--                <div align="center">
                    <a href="/User Manual/ePPS Users Manual.pdf" target="_blank">ePPS User Manual</a>          
                </div>--%>
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
