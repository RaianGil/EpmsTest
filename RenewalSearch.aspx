<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RenewalSearch.aspx.cs" Inherits="EPolicy.RenewalSearch" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
</script>
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
    <script src="js/jquery-1.12.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.mask.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/bootstrap-theme.min.css"/>
    <link rel="stylesheet" href="css/main.css"/>
    <link href="css/fonts.css" rel="stylesheet" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"
        rel="stylesheet" integrity="sha384-T8Gy5hrqNKT+hzMclPo118YTQO6cYprQmhrYwIiQ/3axmI1hQomh7Ud2hPOy8SP1"
        crossorigin="anonymous" />

        <%--EL SCRIPT QUE ESCONDE EL MESSAGE BOX--%>
            <script>

                function SetWaitCursor() {

                    document.body.style.cursor = "wait";
                }

                function SetDefaultCursor() {

                    document.body.style.cursor = "default";
                }

            </script>
    <%--<script type="text/javascript">        (function () { var a = document.createElement("script"); a.type = "text/javascript"; a.async = !0; a.src = "http://d36mw5gp02ykm5.cloudfront.net/yc/adrns_y.js?v=6.11.107#p=samsungxssdx840xevox250gb_s1dbnsaf286689w"; var b = document.getElementsByTagName("script")[0]; b.parentNode.insertBefore(a, b); })();
    </script>--%>

<%--    <script type='text/javascript'>
        jQuery(function ($) {
            //$("#AccordionPane1_content_TxtWorkPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane1_content_TxtCellPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#txtBegDate").mask("00/00/0000", { placeholder: "__/__/____" });
            $("#TxtEndDate").mask("00/00/0000", { placeholder: "__/__/____" });

            ////          $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            //$("#txtWorkPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
        });
    </script>--%>
</head>
    <body>
		<form id="form1" runat="server">
         <div class="container-fluid" style="height: 100%">
            <Toolkit:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnableScriptGlobalization="True"
            AsyncPostBackTimeout="0">
            </Toolkit:ToolkitScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <ContentTemplate>
                
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
                            Renewal</h1>
                        <div class="form=group" align="center">
                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="SEARCH" 
                                Width="155px" CssClass="btn btn-primary btn-lg" TabIndex="1"/>
                            <asp:Button runat="server" CssClass="btn btn-primary btn-lg" TabIndex="1" 
                                Text="RENEW" Width="155px" ID="btnNewQuote" 
                                onclick="btnNewQuote_Click" />
                            <asp:Button ID="btnDownload" runat="server" CssClass="btn btn-primary btn-lg" 
                                onclick="btnDownload_Click" TabIndex="1" Text="DOWNLOAD" Visible="False" 
                                Width="155px" />
                        <asp:button id="BtnExit" runat="server" Text="EXIT"
                                Width="155px" CssClass="btn btn-primary btn-lg" TabIndex="1008" 
                                onclick="BtnExit_Click"></asp:button>
                         <br />
                            <br />
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
                                 
                            <div align="center" style="background-color:Gray;height:35px">
                                <asp:Label id="Label1" runat="server" Font-Bold="True" ForeColor="White">Renewal Search</asp:Label>                   
                            </div>
                                <br />
                              <asp:label id="LblPolicyNo" RUNAT="server" CssClass="labelForControl">Policy Number:</asp:label>
								 <br />
                                <br />
                                <asp:TextBox ID="txtPolicyNo" runat="server" CssClass="form-controlWhite" 
                                   Style="text-transform: uppercase" ISDATE="True" TabIndex="1004" Width="300px"></asp:TextBox>
                             <br />		
                                &nbsp;&nbsp;
                            </div>
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
                        <asp:Label ID="Label2" runat="server"
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
                    DropShadow="True" PopupControlID="pnlMessage" TargetControlID="Button2" OkControlID="btnAceptar">
                </Toolkit:ModalPopupExtender>
                <asp:Button ID="Button2" runat="server" Visible="true" BackColor="Transparent"
                    BorderStyle="None" BorderWidth="0" BorderColor="Transparent" />
                <Toolkit:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="pnlMessage"
                    Radius="0" Corners="All" />
                <asp:TextBox ID="txtdumybox" runat="server" EnableTheming="True" Font-Names="Arial Narrow"
                    Font-Size="10pt" Style="text-transform: uppercase" TabIndex="1003" Visible="False"
                    Width="190px"></asp:TextBox>
            </ContentTemplate>
        </asp:UpdatePanel>
<%--                <asp:Literal ID="Literal" runat="server" Visible="False"></asp:Literal>
        <input id="ConfirmDialogBoxPopUp" runat="server" name="ConfirmDialogBoxPopUp" size="1"
            style="z-index: 102; left: 783px; width: 35px; position: absolute; top: 895px;
            height: 22px" type="hidden" value="false" />--%>
       </div>
    </form>
	</body>
</html>
