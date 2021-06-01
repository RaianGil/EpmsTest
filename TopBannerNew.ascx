<%@ Control Language="c#" Inherits="EPolicy.TopBannerNew" CodeFile="TopBannerNew.ascx.cs" %>

<meta http-equiv="content-type" content="text/html; charset=UTF-8">
<meta charset="utf-8">
<link href="css/bootstrap.min.css" rel="stylesheet">
<link href="css/main.css" rel="stylesheet">
<%--    <link href="epolicy.css" type="text/css" rel="stylesheet" />--%>
<link rel="icon" href="Images2/LogoGuardian.ico" type="image/x-icon" />
<title>Guardian Insurance</title>
<script type="text/javascript">
    javascript: window.history.forward(1);
</script>
<link href="css/fonts.css" rel="stylesheet"/>
<link rel="stylesheet" href="css/stylesheet.css"/>
<link rel="stylesheet" href="css/bootstrap.min.css"/>
<link rel="stylesheet" href="css/main.css"/>
<link rel="Stylesheet"" href="font-awesome/css/font-awesome.css" />


<body style="background-color: #17529B; height: 100%;">
      <asp:UpdatePanel ID="UpdatePanel100" runat="server" RenderMode="Block">
            <ContentTemplate>
    <div align="left">
        <img src="Images2/guardian-login-logo.png" class="sideMenu-logo img-responsive img-centered"
            width="414" height="169" alt="" style="margin-bottom: 15px" />
        <div align="right">
            <asp:Label ID="lblVersion" runat="server"  Font-Size="Smaller"
            Font-Bold="False" ForeColor="White">v 3.0.0</asp:Label>
        </div>
        <div align="left">
            <asp:Label ID="lblTest" runat="server"  Font-Size="Medium"
            Font-Bold="True" ForeColor="Red">TEST ENVIRONMENT</asp:Label>
        </div>
            <asp:Label ID="lblUserName" runat="server"  Font-Size="14pt" Visible="false"
            Font-Bold="false" ForeColor="White"></asp:Label>
        <br />       
        <br />
         <br />
        <nav2>
        <ul class="nav2">
         <asp:Literal ID="Literal1" runat="server"></asp:Literal>

            <%--<li class="active"><a href="RedirectByMenu.aspx?page=Home"><span class="icon-home-icon-epps-r"></span>Home</a></li>
            <li class=""><a href="#" class=""><span class="icon-search-icon-epps-r"></span>Search</a>
                <ul>
                    <li class=""><a href="RedirectByMenu.aspx?page=By ControlID">By ControlID</a></li>
                    <li><a href="RedirectByMenu.aspx?page=By Customers">By Customers</a></li>
                    <li><a href="RedirectByMenu.aspx?page=By Policies">By Policies</a></li>
                </ul>
            </li>
            <li class=""><a href="#" class=""><span class="icon-transaction-icon-epps-r"></span>Transaction</a>
                <ul>
                    <li><a href="RedirectByMenu.aspx?page=Customers">Customers</a></li>
                    <li><a href="RedirectByMenu.aspx?page=Auto Personal">Auto Personal</a></li>                   
                    <li><a href="RedirectByMenu.aspx?page=New VI Quote">New VI Quote</a></li>
                     <li><a href="RedirectByMenu.aspx?page=GuardianXtra">Guardian Xtra</a></li>
                </ul>
            </li>
            <li><a href="RedirectByMenu.aspx?page=Reports"><span class="icon-reports-icon-epps-r"></span>Report</a></li>
            <li><a href="#"><span class="icon-transaction-icon-epps-r"></span>Administration</a>
                <ul>
                    <li><a href="RedirectByMenu.aspx?page=LookupTables">Lookup Tables</a></li>
                    <li><a href="RedirectByMenu.aspx?page=UserPropertiesList">User Properties List</a></li>
                    <li><a href="RedirectByMenu.aspx?page=AddUser">Add User</a></li>
                    <li><a href="RedirectByMenu.aspx?page=GroupPermissions">Group & Permissions</a></li>
                </ul>
            </li>
            <li><a href="RedirectByMenu.aspx?page=Logout" class="" target="_ext"><span class="icon-logout-icon-epps-r"></span>
                Log Out</a></li>--%>
        </ul>
        </nav>
    </div>
     </ContentTemplate>
        </asp:UpdatePanel>
        <script src="//ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <script src="js/bootstrap.min.js"></script>
		<script src="js/scripts.js"></script>

</body>
