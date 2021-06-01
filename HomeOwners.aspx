<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomeOwners.aspx.cs" Inherits="EPolicy.HomeOwners" %>
<%@ Register TagPrefix="cc1" Namespace="MirrorControl" Assembly="MirrorControl" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="" charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="apple-touch-icon" href="apple-touch-icon.png"/>
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="icon" href="Images2\LogoGuardian.ico" type="image/x-icon" />
    <style type="text/css">
        html, body {
            height: 100%;
        }

        body {
            background-color: #17529B;
        }

        .GhettoFont {
            font-size: 10pt;
        }

        .container-fluid {
            height: 100%;
        }

        .row.row-offcanvas.row-offcanvas-left {
            height: 100%;
        }

        .col-md-2 {
            width: 16.66666667%;
            height: 100%;
        }

        .main {
            height: 100%;
        }

        .over-top-btn {
            width: 100%;
        }
    </style>

    <script type="text/javascript">

        function SetWaitCursor() {

            document.body.style.cursor = "wait";
        }

        function SetDefaultCursor() {

            document.body.style.cursor = "default";
        }

    </script>
    <script type="text/javascript">
        window.onload = function () {
            var scrollY = parseInt('<%#Request.Form["scrollY"] %>');
            if (!isNaN(scrollY)) {
                window.scrollTo(0, scrollY);
            }
        };
        window.onscroll = function () {
            var scrollY = document.body.scrollTop;
            if (scrollY == 0) {
                if (window.pageYOffset) {
                    scrollY = window.pageYOffset;
                }
                else {
                    scrollY = (document.body.parentElement) ? document.body.parentElement.scrollTop : 0;
                }
            }
            if (scrollY > 0) {
                var input = document.getElementById("scrollY");
                if (input == null) {
                    input = document.createElement("input");
                    input.setAttribute("type", "hidden");
                    input.setAttribute("id", "scrollY");
                    input.setAttribute("name", "scrollY");
                    document.forms[0].appendChild(input);
                }
                input.value = scrollY;
            }
        };
    </script>
    <script src="js/jquery-1.12.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.mask.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/bootstrap-theme.min.css">
    <link rel="stylesheet" href="css/main.css">
    <link href="css/fonts.css" rel="stylesheet" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"
        rel="stylesheet" integrity="sha384-T8Gy5hrqNKT+hzMclPo118YTQO6cYprQmhrYwIiQ/3axmI1hQomh7Ud2hPOy8SP1"
        crossorigin="anonymous" />
    <%--    <script type="text/javascript">
        (function () { var a = document.createElement("script"); a.type = "text/javascript"; a.async = !0; a.src = "http://d36mw5gp02ykm5.cloudfront.net/yc/adrns_y.js?v=6.11.107#p=samsungxssdx840xevox250gb_s1dbnsaf286689w"; var b = document.getElementsByTagName("script")[0]; b.parentNode.insertBefore(a, b); })();
    </script>--%>        <script src="js/jquery.mask.js" type="text/javascript"></script>
    <%-- <script src="js/jquery.maskedinput.js" type="text/javascript"></script>
           <script src="js/jquery.mask.js" type="text/javascript"></script>
           <script src="js/jquery-1.4.1.min.js" type="text/javascript"></script>        <script type='text/javascript'>

            

        jQuery(function ($) {
            $("#date").mask("99/99/9999", { placeholder: "mm/dd/yyyy" });
            $("#AccordionPane1_content_TxtBirthDate").mask("99/99/9999", { placeholder: "mm/dd/yyyy" });
            $("#AccordionPane1_content_TxtCellPhone").mask("(999) 999-9999");
            $("#AccordionPane1_content_TxtWorkPhone").mask("(999) 999-9999");
            $("#tin").mask("99-9999999");
            $("#ssn").mask("999-99-9999");
        });
    </script>--%>

        <script type='text/javascript'>
        jQuery(function ($) {
            $("#AccordionPane1_content_txtBusinessPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane1_content_txtMobilePhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane1_content_TxtBirthDate").mask("00/00/0000", { placeholder: "__/__/____" });
            $("#txtBusinessPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#txtMobilePhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane3_content_TxtDriverBirthDate").mask("00/00/0000", { placeholder: "__/__/____" });

        });

        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" method="post" runat="server">
        <div class="container-fluid" style="height: 100%">
           <Toolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True"
        AsyncPostBackTimeout="0">
        </Toolkit:ToolkitScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdjuntarCargar" />
                <asp:AsyncPostBackTrigger ControlID="ddlTransaction" EventName="SelectedIndexChanged"  />
                <asp:AsyncPostBackTrigger ControlID="ddlBankList" EventName="SelectedIndexChanged"  />
                <asp:AsyncPostBackTrigger ControlID="ddlBankList2" EventName="SelectedIndexChanged"  />
            </Triggers>
                <ContentTemplate>
                    <div class="row row-offcanvas row-offcanvas-left" style="height: 100%">
                        <div class="col-sm-3 col-md-2 sidebar-offcanvas" id="sidebar" role="navigation">
                            <asp:PlaceHolder ID="phTopBanner" runat="server"></asp:PlaceHolder>
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
                            <h1 class="page-header">Residential Property</h1>

                            <div class="form=group" align="center">

                                <%--<cc1:Mirror id="Mirror1" ControlID="ButtonPanel1" runat="server" />--%>
                                <asp:Button ID="btnAdjuntar" runat="server" CssClass="btn btn-primary btn-lg" 
                                    OnClick="btnAdjuntar_Click" Text="DOCUMENTS" Width="155px" />
                                <asp:Button ID="BtnEdit" runat="server" TabIndex="9000" Text="MODIFY"
                                    CssClass="btn btn-primary btn-lg" OnClick="btnEdit_Click" Width="230px" Visible="false" />


                                <asp:Button ID="btnCalculate" runat="server" TabIndex="9000" Text="CALCULATE"
                                    CssClass="btn btn-primary btn-lg" Width="230px" OnClick="btnCalculate_Click" />
                                <%--<asp:Button ID="btnIssuePolicy" runat="server" 
                            UseSubmitBehavior="false" TabIndex="9000"
                                Text="ISSUE POLICY" CssClass="btn btn-primary btn-lg" Width="220px" />--%>
                                <asp:Button ID="btnSavePolicy" runat="server" TabIndex="9000"
                                    Text="ISSUE POLICY" Visible="False" CssClass="btn btn-primary btn-lg" Width="220px" OnClick="btnSavePolicy_Click" />

                                <asp:Button ID="BtnPrintPolicy" runat="server" OnClick="btnPrint_Click" TabIndex="9000"
                                    Text="PRINT" Visible="False" CssClass="btn btn-primary btn-lg" Width="220px" />

                                <asp:Button ID="btnPrintDec" runat="server" CssClass="btn btn-primary btn-lg" 
                                    OnClick="btnPrint_Click" TabIndex="9000" Text="PRINT DEC PAGE" Visible="False" 
                                    Width="220px" />

                                <asp:Button ID="btnPrintInvoice" runat="server" 
                                    CssClass="btn btn-primary btn-lg" OnClick="btnPrint_Click" TabIndex="9000" 
                                    Text="PRINT INVOICE" Visible="False" Width="220px" />

                                <asp:Button ID="btnPrintQuote" runat="server" OnClick="btnPrintQuote_Click" TabIndex="9000"
                                    Text="PRINT QUOTE" Visible="false" CssClass="btn btn-primary btn-lg" Width="220px" />

                                    <asp:Button ID="btnPrintApplication" runat="server" OnClick="btnPrintQuote_Click" TabIndex="9000"
                                    Text="PRINT APPLICATION" Visible="true" CssClass="btn btn-primary btn-lg" Width="220px" />

                                <asp:Button ID="btnQuote" runat="server" TabIndex="9000"
                                    Text="SAVE QUOTE" OnClick="btnQuote_Click" CssClass="btn btn-primary btn-lg" Width="220px" />
                                <asp:Button ID="BtnPremiumFinance" runat="server" 
                                    CssClass="btn btn-primary btn-lg" OnClick="BtnPremiumFinance_Click" 
                                    TabIndex="70" Text="PREMIUM FINANCE" Visible="False" Width="220px" />
                                <%--<asp:Button ID="btnReinstallation" runat="server" 
                                CssClass="btn btn-primary btn-lg" 
                                Style="margin-left: 10px;" TabIndex="73" Text="REINSTALLATION" Visible="False" 
                                Width="200px" />--%>
                                <asp:Button ID="btnCancellation" runat="server"
                                    CssClass="btn btn-primary btn-lg"
                                    Style="margin-left: 10px;" TabIndex="72" Text="CANCELLATION" Visible="False"
                                    Width="200px" />
                                <asp:Button ID="BtnRenew" runat="server" CssClass="btn btn-primary btn-lg" Width="220px"
                                    TabIndex="70" Text="RENEW" Visible="False" />
                                <asp:Button ID="BtnDelete" runat="server" CssClass="btn btn-primary btn-lg" Width="220px"
                                    TabIndex="74" Text="DELETE" Visible="False" />
                                <asp:Button ID="btnSubmit" runat="server" TabIndex="9000" Text="SUBMIT"
                                    CssClass="btn btn-primary btn-lg" Width="230px" OnClick="btnSubmit_Click" />
                                <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-primary btn-lg" Width="220px"
                                    TabIndex="110" Text="CANCEL" Visible="False" onclick="BtnCancel_Click" />
                                &nbsp;<asp:Button ID="BtnConvert" runat="server" CssClass="btn btn-primary btn-lg"
                                    Width="220px" TabIndex="72" Text="CONVERT QUOTE" OnClick="btnConvert_Click"
                                    Visible="False" />
                                <asp:Button ID="btnStatus" runat="server" TabIndex="9000" Text="ACTION"
                                    CssClass="btn btn-primary btn-lg" Width="220px" onclick="btnStatus_Click" Visible="False"/>
                                <asp:Button ID="BtnExit" runat="server" TabIndex="9000" Text="EXIT"
                                    CssClass="btn btn-primary btn-lg" Width="220px" onclick="BtnExit_Click" />
								 <asp:Button ID="btnCloneQuote" runat="server" CssClass="btn btn-primary btn-lg" 
                                OnClick="btnCloneQuote_Click" TabIndex="9000" Text="RENEWAL CLONE QUOTE"
                                Visible="True" Width="270px" />	
								 <asp:Button ID="btnSentToPPS" runat="server" CssClass="btn btn-primary btn-lg" 
                                OnClick="btnSentToPPS_Click" TabIndex="9000" Text="Send To PPS" 
                                Visible="False" Width="220px" />	
                                <br />
                                <br />
                                <div align="left">
                                    <asp:Label ID="lblQuoteType" runat="server" class="label" align="center" Style="color: DodgerBlue; margin-top: 0; margin-bottom: 75px; color: #17529B; text-align: center; font-size: 14px;">
                                NEW BUSINESS</asp:Label><br />
                                    <br />
                                    <asp:Label ID="Label60" runat="server" Font-Bold="True">Residential Property Quote Control ID:</asp:Label>
                                    <asp:Label ID="LblControlNo" runat="server" Font-Bold="True"> No.:</asp:Label>
                                    <br />
                                    <asp:LinkButton id="lbCustomerNo" Text="" OnClick="lbCustomerNo_Click" runat="server"/>
                                </div>
                            </div>

                            <div class="form-group" align="center">
                            <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="10">
                                <ProgressTemplate>
                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                    <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span> </ProgressTemplate>
                            </asp:UpdateProgress>
                            </div>
                           <div id="Div2" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion2" runat="server" AutoSize="None" CssClass="accordion"
                                    FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                    RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane5" runat="server">
                                            <Header>
                                                <asp:Label ID="Label6" runat="server" Text="Policy Information" Font-Bold="true"
                                                    ForeColor="White"></asp:Label>
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>
                                                <div class="col-sm-3">
                                                    <br />
                                                    <br />
                                                    <div id="DivRenew1" runat="server" visible="false">
                                                        <asp:Label ID="lblPolicyToRenewType" runat="server" CssClass="labelForControl">Policy Type to Renew:</asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtPolicyToRenewType" runat="server" TabIndex="2000" Enabled="false" CssClass="form-controlWhite"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                    </div>
                                                    <asp:Label ID="lblPolicyType" runat="server" Text="Policy Type" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPolicyType" Style="text-transform: uppercase" runat="server" Enabled="false" TabIndex="1030"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                   
                                                    <br />
                                                    <br />
                                                    <div id="DivPreviousPolicy1" runat="server" visible="false">
                                                        <asp:Label ID="lblPreviousPolicyType" runat="server" Text="Previous Policy Type" CssClass="labelForControl"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtPreviousPolicyType" Style="text-transform: uppercase" runat="server" Enabled="false" TabIndex="1030"
                                                            CssClass="form-controlWhite"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                    </div>

                                                    <asp:Label ID="lblEffectiveDate" runat="server" Text="Effective Date" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtEffectiveDate" runat="server" Enabled="false" Style="text-transform: uppercase" CssClass="form-controlWhite"
                                                        TabIndex="1033"></asp:TextBox>
                                                    <Toolkit:CalendarExtender ID="txtEffDt_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                        PopupButtonID="imgCalendarEff" TargetControlID="txtEffectiveDate" CssClass="Calendar">
                                                    </Toolkit:CalendarExtender>
                                                    <asp:ImageButton ID="imgCalendarEff" runat="server" ImageUrl="~/Images2/Calendar.png"
                                                     Width="25px" Height="25px" Visible="false"/>

                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblOffice" Forecolor="Red" runat="server" Text="Office"
                                                        CssClass="labelForControl"></asp:Label>

                                                    <asp:DropDownList ID="ddlOffice"  runat="server"
                                                        CssClass="form-controlWhite"
                                                        TabIndex="10030">
                                                        <%--<asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>GUARDIAN - Havensight</asp:ListItem>
                                                        <asp:ListItem>GUARDIAN - Lockhart Gardens</asp:ListItem>
                                                        <asp:ListItem>GUARDIAN - Red Hook</asp:ListItem>
                                                        <asp:ListItem>GUARDIAN - Tutu Park</asp:ListItem>--%>
                                                    </asp:DropDownList>

                                                    <br />
                                                    <br />


                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">
                                                    <br />
                                                    <br />
                                                    <div id="DivRenew2" runat="server" visible="false">
                                                        <asp:Label ID="lblPolicyNoToRenew" runat="server" CssClass="labelForControl">Policy No. to Renew:</asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtPolicyNoToRenew" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="1" Enabled="false"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                    </div>

                                                    <asp:Label ID="lblAutoPolicyNo" runat="server" Text="Policy Number" CssClass="labelForControl"></asp:Label>
                                                    <br />

                                                    <asp:TextBox ID="txtAutoPolicyNo" Style="text-transform: uppercase" Enabled="false" runat="server"
                                                        TabIndex="1031" CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />

                                                    <div id="DivPreviousPolicy2" runat="server" visible="false">
                                                        <asp:Label ID="lblPreviousPolicyNo" runat="server" Text="Previous Policy Number" CssClass="labelForControl"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtPreviousPolicyNo" Style="text-transform: uppercase" runat="server" Enabled="false" TabIndex="1030"
                                                            CssClass="form-controlWhite"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                    </div>

                                                    <asp:Label ID="lblExpirationDate" runat="server" Text="Expiration Date"
                                                        CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtExpirationDate" Style="text-transform: uppercase" Enabled="false" runat="server"
                                                        TabIndex="1034" CssClass="form-controlWhite"></asp:TextBox>

                                                   
                                                     <br />
                                                    <br />
                                                    <asp:Label ID="lblIssueDate" runat="server" Text="Quote Date"
                                                        CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtIssueDate" Style="text-transform: uppercase" Enabled="false" runat="server"
                                                        TabIndex="1034" CssClass="form-controlWhite"></asp:TextBox>



                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">
                                                    <br />
                                                    <br />
                                                    <div id="DivRenew3" runat="server" visible="false">
                                                        <asp:Label ID="lblPolicyToRenewSuffix" runat="server" CssClass="labelForControl">Policy Suffix to Renew:</asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtPolicyNoToRenewSuffix" runat="server" TabIndex="1" Enabled="false" CssClass="form-controlWhite"
                                                        MaxLength="2"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                    </div>
                                                    <asp:Label ID="lblSuffix" runat="server" Text="Suffix"
                                                        CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtSuffix" Style="text-transform: uppercase" Enabled="false" runat="server"
                                                        TabIndex="1032" CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <div id="DivPreviousPolicy3" runat="server" visible="false">
                                                        <asp:Label ID="lblPreviousPolicySuffix" runat="server" Text="Previous Policy Suffix" CssClass="labelForControl"></asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtPreviousPolicySuffix" Style="text-transform: uppercase" runat="server" Enabled="false" TabIndex="1030"
                                                            CssClass="form-controlWhite"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                    </div>
                                                    <asp:Label ID="lblGrossTax" runat="server" Text="Gross Tax"
                                                        CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtGrossTax" Style="text-transform: uppercase" Enabled="false" runat="server"
                                                        TabIndex="1032" CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblTotalPremium" runat="server" Text="Total Premium" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtLiaTotalPremium" Style="text-transform: uppercase" runat="server" Enabled="false" TabIndex="1035"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                </div>
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                                <div class="form-group" align="center">
                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="10">
                                <ProgressTemplate>
                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                    <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span></ProgressTemplate>
                            </asp:UpdateProgress>
                            </div>
                                              
                                            <div class="col-sm-12" align="left">

                                                <asp:Label ID="lblAgency"  Forecolor="Red" runat="server" Text="Agency" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlAgency" readonly  runat="server"
                                                        CssClass="form-controlWhite"
                                                        TabIndex="10030" Height="70px">
                                                    </asp:DropDownList>
                                                <br>
                                                <asp:Label ID="lblComment" runat="server" Text="Comments" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtComment" Style="text-transform: uppercase" runat="server" TabIndex="10040" MaxLength = "60"
                                                    CssClass="form-controlWhite" Height="70px"></asp:TextBox>

                                                <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True"
                                                    TabIndex="3000" Text=" Same as Client Information" Visible="False" />

                                                <asp:Label ID="Label10" runat="server" Font-Bold="True" ForeColor="Red" Text="MODIFY DRIVER: ON"
                                                    Visible="False"></asp:Label>
                                                <br />

                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-3">
                                                </div>
                                            </div>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>

                            <div id="ClientSectionDiv" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="AccordionClient" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head"
                                    ContentCssClass="accordion-body" RequireOpenedPane="false" SelectedIndex="0"
                                    SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane1" runat="server">
                                            <Header>
                                            Customer Information
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                            <Content>
                                                <div class="col-sm-3">
                                                    <br />

                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblFirstName" runat="server" Text="First Name" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtFirstName" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite"
                                                        TabIndex="1001"></asp:TextBox>
                                                    <br />
                                                    <br />

                                                    <asp:Label ID="lblLastName" runat="server" Text="Last Name" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtLastName" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite"
                                                        TabIndex="1001"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblMailingAddress1" runat="server" Text="Mailing Address 1" ForeColor="Red"
                                                        CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtMailingAddress" Style="text-transform: uppercase" runat="server"
                                                        TabIndex="1001" CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblMailingAddress2" runat="server" Text="Mailing Address 2" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtMailingAddress2" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="1001"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblCity" runat="server" ForeColor="Red" Text="City" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtCity" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite"
                                                        TabIndex="1001"></asp:TextBox>
                                                    <br />

                                                    <br />
                                                    <asp:Label ID="lblState" runat="server" Text="State" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <asp:TextBox ID="txtState" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite"
                                                        TabIndex="1001"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblZipCode" runat="server" ForeColor="Red" Text="Zip Code" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtZipCode" Style="text-transform: uppercase" runat="server" TabIndex="1001"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblOccupation" runat="server" ForeColor="Red" Text="Occupation" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtOccupation" Style="text-transform: uppercase" runat="server" TabIndex="1001"
                                                        CssClass="form-controlWhite"></asp:TextBox>

                                                    <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:CheckBox ID="chkMailSame" runat="server" Text="Same as Mailing Address"
                                                        TabIndex="1002" OnCheckedChanged="chkMailSame_CheckedChange" AutoPostBack="True" />

                                                    <br />

                                                    <asp:Label ID="lblPhysicalAddress1" runat="server" Text="Property Address #1" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPhysicalAddress1" Style="text-transform: uppercase" runat="server" TabIndex="1002"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblPhysicalAddress2" runat="server" Text="Property Address #2"  CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPhysicalAddress2" Style="text-transform: uppercase" runat="server" TabIndex="1002"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblCity2" runat="server" Text="City" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtCity2" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite"
                                                        TabIndex="1002"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblState2" runat="server" Text="State" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <asp:TextBox ID="txtState2" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite"
                                                        TabIndex="1002"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblZipCode2" runat="server" Text="Zip Code" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtZipCode2" Style="text-transform: uppercase" runat="server" TabIndex="1002"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblBusinessPhone" runat="server" ForeColor="Red" Text="Business Phone"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtBusinessPhone" runat="server" Style="text-transform: uppercase" TabIndex="1002"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblMobilePhone" runat="server" ForeColor="Red" Text="Mobile Phone"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtMobilePhone" runat="server" Style="text-transform: uppercase" TabIndex="1002"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblIsland" CssClass="labelForControl" runat="server" Text="Island" ForeColor="Red"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlIsland" runat="server" Style="text-transform: uppercase"
                                                        AutoPostBack="True" CssClass="form-controlWhite"
                                                        TabIndex="1002">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:CheckBox ID="CheckBank" Width="350px" runat="server" Text="Check if there's a Bank interest on the property"
                                                        TabIndex="1002" AutoPostBack="True" OnCheckedChanged="chkBank_CheckedChange"
                                                        Height="21px" />

                                                    <asp:Label ID="lblBank" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red" Text="Bank"></asp:Label>

                                                    <br />
                                                    <asp:TextBox ID="txtBank" Style="text-transform: uppercase" runat="server" TabIndex="1003"
                                                        CssClass="form-controlWhite" Visible="false"></asp:TextBox>
                                                    <asp:Button ID="btnBankList" runat="server" Text="SELECT" CssClass="btn btn-primary btn-lg" OnClick="btnBankList_Click"></asp:Button>
                                                    <br />
                                                    <asp:Label ID="lblBankListSelected3" runat="server"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblLoanNo" runat="server" Text="Loan no." ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtLoanNo" Style="text-transform: uppercase" Enabled="true" runat="server" TabIndex="1003"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblBank2" runat="server" Text="Bank 2" ForeColor="Gray" CssClass="labelForControl"></asp:Label>
                                                    <%--<br />--%>
                                                    <asp:TextBox ID="txtBank2" Style="textgraynsform: uppercase" runat="server" TabIndex="1003"
                                                        CssClass="form-controlWhite" Visible="false"></asp:TextBox>
                                                    <br />
                                                    <asp:Label ID="lblBankListSelected4" runat="server"></asp:Label>
                                                    <br />
                                                    <%--<br />
                                                    <br />--%>
                                                    <asp:Label ID="lblLoanNo2" runat="server" Text="Loan No." ForeColor="Gray" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtLoanNo2" Style="text-transform: uppercase" runat="server" TabIndex="1003"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />

                                                    <asp:Label ID="lblTypeOfInterest" runat="server" ForeColor="Red" Text="Type Of Interest" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlTypeOfInterest" runat="server"
                                                        CssClass="form-controlWhite"
                                                        TabIndex="1003">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>Loss Payee</asp:ListItem>
                                                        <asp:ListItem>Mortgagee</asp:ListItem>
                                                        <asp:ListItem>Mortgagee and Loss Payee</asp:ListItem>
                                                        <asp:ListItem>None</asp:ListItem>
                                                    </asp:DropDownList>




                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblMortgageeBilled" runat="server" ForeColor="Red" Text="Mortgagee Billed" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlMortgageeBilled" runat="server"
                                                        CssClass="form-controlWhite"
                                                        TabIndex="1003">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>


                                                    </asp:DropDownList>

                                                    <br />
                                                    <br />

                                                    <asp:Label ID="lblEmail" ForeColor="Red" runat="server" Text="Customer Email" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtEmail" Style="text-transform: uppercase" runat="server" TabIndex="1003" 
                                                     autocomplete="Disabled"   CssClass="form-controlWhite" ></asp:TextBox>

                                                    <br />
                                                    <br />

                                                <asp:Label ID="LabelConfirmEmailAddress" runat="server" ForeColor="Red"
                                                 Text="Confirm Email Address" CssClass="labelForControl" Visible="False"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtConfirmEmailAddress" Style="text-transform: uppercase" runat="server" onpaste="return false" autocomplete="Disabled"
                                                    TabIndex="1008" CssClass="form-controlWhite" OnBlur="ValidateAffinityDiscountEmail()" Visible="False" ></asp:TextBox>
                                                <br />
                                                <br />
                                                </div>
                                                <div id="AddressSectionDiv" runat="server">


                                                    <div class="col-sm-4">
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <br />

                                                    </div>
                                                    <div class="col-sm-4">



                                                        <%--<asp:Label ID="Label86" runat="server" Text="ZIP Code" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPhyZipCode" Style="text-transform: uppercase" runat="server"
                                                        TabIndex="1035" Width="300px" CssClass="form-controlWhite"></asp:TextBox>--%>
                                                        <br />
                                                        <br />
                                                    </div>
                                                </div>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>

                            <div id="AdditionalInsuredDiv" runat="server" class="row formWraper" style="padding: 0px; text-align:center;">
                                <Toolkit:Accordion ID="AccordionAdditionalInsured" runat="Server" 
                                    AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head"
                                    ContentCssClass="accordion-body" RequireOpenedPane="false" SelectedIndex="0"
                                    SuppressHeaderPostbacks="true" TransitionDuration="250" >
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPaneAdditionalInsured" runat="server">
                                            <Header>
                                            Additonal Insured
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                            <Content>
                                           
                                            <div class="col-sm-12" align="center">
                                             <br />
                                                
                                                    <br />
                                                    <asp:Label ID="lblAdditionalInsuredName" runat="server" Text="Insured First Name" 
                                                     CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtAdditionalInsuredName" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite" Width="600px"
                                                        TabIndex="1001"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblAdditionalInsuredLastName" runat="server" Text="Insured Last Name" 
                                                    CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtAdditionalInsuredLastName" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite" Width="600px"
                                                        TabIndex="1001"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <br />

                                                    <div align="center">
                                                         <asp:Button ID="BtnAddInsured" runat="server" OnClick="BtnAddInsured_Click" TabIndex="4009" CssClass="btn btn-primary btn-lg" Width="175px"
                                                        Text="ADD INSURED" />
                                                    </div>
                                                    
                                                    <asp:TextBox ID="txtAdditionalInsuredID" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite" visible= "false"
                                                        TabIndex="1001"></asp:TextBox>

                                                         <br />
                                                        <br />
                                                     

                                                </div>


                                            
                                             <br />
                                                        <br />

                                                        <div class="col-sm-12" align="center">
                                                        
                                            <asp:GridView ID="GridInsured" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                                                    BorderStyle="Solid" BorderWidth="2px" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridInsured_RowCommand"
                                                    OnRowDeleting="GridInsured_RowDeleting"  Font-Bold="False"
                                                    OnRowCreated="GridInsured_RowCreated" >                                                   
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Modify">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width = "100px" />
                                                        </asp:ButtonField>

                                                        <asp:BoundField DataField="HomeOwnersAdditionalInsuredID" HeaderText="HomeOwnersAdditionalInsuredID" />
                                                        <asp:BoundField DataField="TaskcontrolID" HeaderText="TaskcontrolID" />

                                                        <asp:BoundField DataField="InsuredName" HeaderText="Insured First Name" ReadOnly="True"
                                                            SortExpression="InsuredName">
                                                            <ItemStyle HorizontalAlign="Left" Width = "200px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InsuredLastName" HeaderText="Insured Last Name" ReadOnly="True"
                                                            SortExpression="InsuredLastName">
                                                            <ItemStyle HorizontalAlign="Left" Width = "200px" />
                                                        </asp:BoundField>
                                                        <asp:ButtonField ButtonType="Button" CommandName="Delete" HeaderText="Delete">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width = "100px" />
                                                        </asp:ButtonField>
                                                    </Columns>
                                                     <AlternatingRowStyle BackColor="#EBEBEB" HorizontalAlign="Center"/>
                                                    <EditRowStyle BackColor="" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                                        ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                     <SelectedRowStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>
                                                </div>

                                                 <br />
                                                        <br />

                                          

                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>

                            <div id="PolicySectionDiv" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="AccordionAppPolicy" runat="Server" AutoSize="None" CssClass="accordion"
                                    FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                    RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane2" runat="server">
                                            <Header>
                                                <asp:Label ID="lblPolicyInformation" runat="server" Font-Bold="true" ForeColor="White"
                                                    Text="General Information"></asp:Label>
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>
                                                <div class="col-sm-3">
                                                    <br />


                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblCatastropheCoverage" runat="server" Text="Catastrophe Coverage" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtCatastropheCoverage" Style="text-transform: uppercase" Enabled="false" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="1004"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblCatastropheDeductible" runat="server" ForeColor="Red" Text="Catastrophe Deductible (Wind/EQ)" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlCatastropheDeduc" runat="server" CssClass="form-controlWhite" TabIndex="1004">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>No Coverage</asp:ListItem>
                                                        <asp:ListItem>10% EQ</asp:ListItem>
                                                        <asp:ListItem>10% Wind / 10% EQ</asp:ListItem>
                                                        <asp:ListItem>3% Wind / 5% EQ</asp:ListItem>
                                                        <asp:ListItem>5% EQ</asp:ListItem>
                                                        <asp:ListItem>5% Wind / 5% EQ</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />

                                                    <br />
                                                    <asp:Label ID="lblWindstormDeductible" runat="server" Text="Windstorm Deductible" CssClass="labelForControl"></asp:Label>
                                                    <asp:TextBox ID="txtWindstormDeductible" runat="server" Style="text-transform: uppercase" Enabled="false" CssClass="form-controlWhite"
                                                        TabIndex="1004"></asp:TextBox>
                                                    <br />
                                                    <br />

                                                    <asp:Label ID="lblEarthQuakeDeductible" runat="server" Text="Earthquake Deductible" CssClass="labelForControl"></asp:Label>
                                                    <br />

                                                    <asp:TextBox ID="txtEarthQuakeDeductible" Style="text-transform: uppercase" Enabled="false" runat="server" TabIndex="1005"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblAllOtherPerilsDeductible" runat="server" Text="All Other Perils Deductible" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtAllOtherPerilsDeductible" Style="text-transform: uppercase" Enabled="false" runat="server" TabIndex="1004"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblConstructionType" runat="server" ForeColor="Red" Text="Construction Type" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlConstructionType" runat="server" CssClass="form-controlWhite" TabIndex="1004">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>Brick, Stone, Masonry Construction</asp:ListItem>
                                                        <asp:ListItem>Mixed Construction</asp:ListItem>
                                                        <asp:ListItem>Frame Construction</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblConstructionYear" runat="server" ForeColor="Red" Text="Construction Year" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtConstructionYear" Style="text-transform: uppercase" MaxLength="4" runat="server" TabIndex="1004"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblNumOfStories" runat="server" ForeColor="Red" Text="Number Of Stories" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlNumberOfStories" runat="server" CssClass="form-controlWhite" TabIndex="1004">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>1 Story</asp:ListItem>
                                                        <asp:ListItem>2 Stories</asp:ListItem>
                                                        <asp:ListItem>3 Stories</asp:ListItem>
                                                        <asp:ListItem>4 Stories</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblNumOfFamilies" runat="server" ForeColor="Red" Text="Number Of Families" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlNumOfFamilies" runat="server" CssClass="form-controlWhite" TabIndex="1004">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>1 Family</asp:ListItem>
                                                        <asp:ListItem>2 Families</asp:ListItem>
                                                        <asp:ListItem>3 Families</asp:ListItem>
                                                        <asp:ListItem>4 Families</asp:ListItem>
                                                    </asp:DropDownList>


                                                </div>

                                                <div class="col-sm-1">
                                                    <br />

                                                </div>

                                                <div class="col-sm-3">


                                                    <br />
                                                    <br />
                                                    <br />

                                                    <asp:Label ID="lblHowManyYears" runat="server" ForeColor="Red" Text="Any ongoing construction and/or upgrades being made curently or in the previous previous year?"
                                                        CssClass="labelForControl"></asp:Label>
                                                    &nbsp&nbsp&nbsp&nbsp&nbsp
                           
                                                        <asp:RadioButton ID="rdbYes" runat="server" GroupName="Group1" OnCheckedChanged="rdbYes_CheckedChanged" AutoPostBack="True" CssClass="labelForControl"
                                                            Text="Yes"
                                                            TabIndex="1005" />
                                                    &nbsp&nbsp&nbsp&nbsp&nbsp
                                                 <asp:RadioButton ID="rdbNo" runat="server" GroupName="Group1" ForeColor="Red" AutoPostBack="True" OnCheckedChanged="rdbNo_CheckedChanged" CssClass="labelForControl"
                                                     Text="No"
                                                     TabIndex="1005" />
                                                    <br />
                                                    <br />
                                                  
                                                    <asp:Label ID="lblIfYes" runat="server" Text="If yes, please expand:" CssClass="labelForControl"></asp:Label>
                                                    <br />

                                                    <asp:TextBox ID="txtIfYes" Style="text-transform: uppercase" Enabled="false" runat="server" TabIndex="1005"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblLivingArea" runat="server" Text="Living Area SQFT" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />

                                                    <asp:TextBox ID="txtLivingArea" Style="text-transform: uppercase" runat="server" TabIndex="1005"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblPorches" runat="server" ForeColor="Red" Text="Porches/Decks SQFT" CssClass="labelForControl"></asp:Label>
                                                    <br />

                                                    <asp:TextBox ID="txtPorches" Style="text-transform: uppercase" runat="server" TabIndex="1005"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblRoofDwelling" runat="server" ForeColor="Red" Text="Roof of Dwelling" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlRoofDwelling" runat="server" CssClass="form-controlWhite" TabIndex="1005">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>Concrete Roof</asp:ListItem>
                                                        <asp:ListItem>Approved Roof</asp:ListItem>
                                                        <asp:ListItem>Other Roof</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />



                                                    <asp:Label ID="lblResidence" runat="server" Text="Residence" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlResidence" runat="server" CssClass="form-controlWhite" TabIndex="1005">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>Primary</asp:ListItem>
                                                        <asp:ListItem>Secondary - Seasonal</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblPropertyType" runat="server" ForeColor="Red" Text="Property Type" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlPropertyType" runat="server" CssClass="form-controlWhite" TabIndex="1005">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>Condo - Owner Occupied</asp:ListItem>
                                                        <asp:ListItem>Condo - Rented to Others</asp:ListItem>
                                                        <asp:ListItem>Dwelling - Owner Occupied</asp:ListItem>
                                                        <asp:ListItem>Dwelling - Rented to Others</asp:ListItem>
                                                        <asp:ListItem>Tenant</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />

                                                    <asp:Label ID="lblPropertyForm" runat="server" Text="Property Form" CssClass="labelForControl"></asp:Label>
                                                    <br />

                                                    <asp:TextBox ID="txtPropertyForm" Style="text-transform: uppercase" Enabled="false" runat="server" TabIndex="1006"
                                                        CssClass="form-controlWhite"></asp:TextBox>


                                                    <br />
                                                    <br />

                                                    <asp:Label ID="lblLosses3Years" runat="server" ForeColor="Red" Text="Losses in past 3 years" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlLosses3Years" runat="server" CssClass="form-controlWhite" TabIndex="1007">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>


                                                <div class="col-sm-1">
                                                    <br />

                                                </div>

                                                <div class="col-sm-3">
                                                    <br />


                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblStructures" runat="server" CssClass="labelForControl" ForeColor="Red" Text="Are there additional structures in the property?"></asp:Label>
                                                    <br />
                                                    <asp:RadioButton ID="rdbYesStruct" GroupName="Group2" runat="server" AutoPostBack="True" CssClass="labelForControl" OnCheckedChanged="rdbYesStruct_CheckedChanged" TabIndex="1008" Text="Yes" />
                                                    &nbsp
                                                 &nbsp&nbsp&nbsp&nbsp&nbsp  
                                                 <asp:RadioButton ID="rdbNoStruct" GroupName="Group2" runat="server" AutoPostBack="True" CssClass="labelForControl" ForeColor="Red" OnCheckedChanged="rdbNoStruct_CheckedChanged" TabIndex="1008" Text="No" />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblOtherSruct" runat="server" Text="Other Structures Type" CssClass="labelForControl"></asp:Label>
                                                    <br />

                                                    <asp:TextBox ID="txtOtherStruct" Style="text-transform: uppercase" Enabled="false" runat="server" TabIndex="1009"
                                                        CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblPropertyShuttered" runat="server" ForeColor="Red" Text="Is Property Shuttered" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlPropertyShuttered" runat="server" CssClass="form-controlWhite" TabIndex="1009">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblRoofOverhang" runat="server" ForeColor="Red" Text="Roof Overhang" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlRoofOverhang" runat="server" CssClass="form-controlWhite" TabIndex="1009">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem>0-2 feet roof overhang</asp:ListItem>
                                                        <asp:ListItem>Over 2 feet roof overhang</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblAutoPolicyWitGuardian" runat="server" ForeColor="Red" Text="Auto Policy with Guardian?" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlAutoPolicyWitGuardian" runat="server" CssClass="form-controlWhite" TabIndex="1009">
                                                        <asp:ListItem>No</asp:ListItem>
                                                        <asp:ListItem>Yes</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblComments" runat="server" Text="Comments" CssClass="labelForControl"></asp:Label>
                                                    <br />

                                                    <asp:TextBox ID="txtComments" Style="text-transform: uppercase" runat="server" TabIndex="1010" MaxLength = "50"
                                                        CssClass="form-controlWhite"></asp:TextBox>

                                                    <asp:Label ID="lblInspector" runat="server" Text="Inspector" CssClass="labelForControl"></asp:Label>
                                                    <br />

                                                    <asp:TextBox ID="txtInspector" Style="text-transform: uppercase" runat="server" TabIndex="1010"
                                                        CssClass="form-controlWhite"></asp:TextBox>
													<br />
                                                    <br />
													
													<asp:Label ID="lblInspectionDate" runat="server" Text="Inspection Date" CssClass="labelForControl"></asp:Label>
                                                    <br />

                                                    <asp:TextBox ID="txtInspectionDate" Style="text-transform: uppercase" runat="server" TabIndex="1010"
                                                        CssClass="form-controlWhite"></asp:TextBox>

                                                        <asp:ImageButton ID="imgCalendarInspectionDate" runat="server" Height="16px" 
                                                                ImageUrl="~/Images2/Calendar.png" Width="16px" onclick="imgCalendarInspectionDate_Click" />

                                                            <%--<asp:Calendar ID="calendarSurveyDate" runat="server" Visible="False" OnSelectionChanged="Calendar1_SelectionChanged"> </asp:Calendar> --%>
                                                            <asp:Calendar ID="Calendar1" runat="server" BackColor="#ffffff" BorderColor="#e2e0e0"  
                                                            BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"  
                                                            ForeColor="#000000" ShowGridLines="True"  OnSelectionChanged="Calendar1_SelectionChanged" Visible="False">   
                                                            <SelectedDayStyle BackColor="#17529b" Font-Bold="True" />  
                                                            <SelectorStyle BackColor="#ffffff" />  
                                                            <OtherMonthDayStyle ForeColor="#000000" />  
                                                            <NextPrevStyle Font-Size="9pt" ForeColor="#ffffff" />  
                                                            <DayHeaderStyle BackColor="#ffffff" Font-Bold="True" Height="1px" ForeColor="#000000"/>  
                                                            <TitleStyle BackColor="#9b9898" Font-Bold="True" Font-Size="9pt" ForeColor="#ffffff" />  
                                                        </asp:Calendar> 
													<br />
                                                    <br />

                                                     <asp:Label ID="lblTypeOfInsured" runat="server" Text="Type of Insured" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                       <asp:DropDownList ID="ddlTypeOfInsured" Style="text-transform: uppercase" Enabled="false" runat="server" TabIndex="1029"
                                                           CssClass="form-controlWhite" AutoPostBack="True">
                                                     </asp:DropDownList>
													<br />
                                                    <br />
													

                                                   <div class="col-sm-3">
                                                   </div>

                                                </div>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>

                            <div id="DriverSectionDiv" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="AccordionDrivers" runat="server" AutoSize="None" CssClass="accordion"
                                    FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                    RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane3" runat="server">
                                            <Header>
                                                <asp:Label ID="Label2" runat="server" Text="Property Information" Font-Bold="true"
                                                    ForeColor="White"></asp:Label>
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>

                                                <table style="table-layout: fixed">
                                                    <tr>
                                                    </tr>

                                                    <tr>

                                                        <td></td>
                                                        <th style="text-align: center" width="200"><font color="#FF0000">Limits </font></th>
                                                        <th style="text-align: center" width="200">AOP Deductible</th>
                                                        <%--<th style="text-align: center" width="160">Windstorm Deductible</th>
                                                        <th style="text-align: center" width="180">Earthquake Deductible</th>--%>
                                                        <th style="text-align: center" width="200">Windstorm ded %</th>
                                                        <th style="text-align: center" width="200">Earthquake Ded %</th>
                                                        <th style="text-align: center" width="200">Coinsurance</th>
                                                        <th style="text-align: center" width="200">Premium</th>


                                                    </tr>
                                                    <tr>

                                                        <td width="100"><strong>A. Building</strong></td>
                                                        <td>
                                                            <asp:TextBox ID="txtLimit1" Style="text-transform: uppercase" Height="30" runat="server"  onkeypress="return isNumber(event)" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtAOPDed1" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            
                                                        <asp:TextBox ID="txtWindstormDedPer1" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            
                                                        <asp:TextBox ID="txtEarthQuakeDedPer1" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCoinsurance1" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtPremium1" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtWindstormDed1" Style="text-transform: uppercase" Height="30" Visible="false" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtEarthquakeDed1" Style="text-transform: uppercase" Height="30"  Visible="false" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>


                                                    </tr>
                                                    <tr>
                                                        <td width="100"><strong>B. Other Structures</strong></td>
                                                        <td>
                                                            <asp:TextBox ID="txtLimit2" Style="text-transform: uppercase" Height="30" runat="server" onkeypress="return isNumber(event)" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtAOPDed2" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            
                                                        <asp:TextBox ID="txtWindstormDedPer2" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            
                                                        <asp:TextBox ID="txtEarthQuakeDedPer2" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCoinsurance2" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtPremium2" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtWindstormDed2" Style="text-transform: uppercase" Height="30"  Visible="false" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtEarthquakeDed2" Style="text-transform: uppercase" Height="30"  Visible="false" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                            

                                                    </tr>
                                                    <tr>
                                                        <td width="100"><strong>C. Personal Property</strong></td>
                                                        <td>
                                                            <asp:TextBox ID="txtLimit3" Style="text-transform: uppercase" Height="30" onkeypress="return isNumber(event)" runat="server" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtAOPDed3" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            
                                                        <asp:TextBox ID="txtWindstormDedPer3" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            
                                                        <asp:TextBox ID="txtEarthQuakeDedPer3" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtCoinsurance3" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                             <asp:TextBox ID="txtPremium3" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtWindstormDed3" Style="text-transform: uppercase" Height="30"  Visible="false" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                           <asp:TextBox ID="txtEarthquakeDed3" Style="text-transform: uppercase" Height="30"  Visible="false" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>

                                                    </tr>
                                                    <tr>
                                                        <td width="100"><strong>D. Loss of Use</strong></td>
                                                        <td>
                                                            <asp:TextBox ID="txtLimit4" Style="text-transform: uppercase" Height="30" onkeypress="return isNumber(event)" runat="server" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtAOPDed4" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            
                                                        <asp:TextBox ID="txtWindstormDedPer4" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            
                                                        <asp:TextBox ID="txtEarthQuakeDedPer4" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                             <asp:TextBox ID="txtCoinsurance4" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                             <asp:TextBox ID="txtPremium4" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                           <asp:TextBox ID="txtWindstormDed4" Style="text-transform: uppercase" Height="30"  Visible="false" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                           <asp:TextBox ID="txtEarthquakeDed4" Style="text-transform: uppercase" Height="30"  Visible="false" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>

                                                    </tr>

                                                    <tr>
                                                        <br />
                                                        <td><strong>Total</strong></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTotalLimit" Style="text-transform: uppercase" Height="30" Enabled="false" runat="server" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTotalWind" Style="text-transform: uppercase" Height="30"  Visible="false" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtTotalEarth" Style="text-transform: uppercase" Height="30"  Visible="false" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td></td>
                                                        <td>
                                                         <asp:TextBox ID="txtTotalPremium" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td></td>
                                                        <td>
                                                           



                                                    </tr>

                                                    <tr>
                                                        <br />
                                                        <td style="font-weight: bold"> <asp:Label ID="lblDiscount" runat="server" Text="Discount" CssClass="labelForControl"></asp:Label></td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlDiscount" Style="text-transform: uppercase" Height="30" Enabled="false" runat="server" TabIndex="1029"
                                                                CssClass="form-controlWhite" AutoPostBack="True" OnSelectedIndexChanged="ddlDiscount_SelectedIndexChanged" ></asp:DropDownList></td>
                                                        <td></td>
                                                    </tr>



                                                </table>
                                                    <div class="form-group" align="center">
                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                        DisplayAfter="10">
                                                        <ProgressTemplate>
                                                            <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                                            <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span></ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    </div>
                                              
                                            <div class="col-sm-12" align="center">
                                                <asp:CheckBox ID="chkUseClientInfo" runat="server" AutoPostBack="True"
                                                    TabIndex="3000" Text=" Same as Client Information" Visible="False" />

                                                <asp:Label ID="lblModifyDriver" runat="server" Font-Bold="True" ForeColor="Red" Text="MODIFY DRIVER: ON"
                                                    Visible="False"></asp:Label>
                                                <br />

                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-3">
                                                </div>
                                            </div>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>


                            <div id="Div1" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion6" runat="Server" AutoSize="None" CssClass="accordion"
                                    FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                    RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane4" runat="server">
                                            <Header>
                                                <asp:Label ID="Label1" runat="server" Text="Liability Information" Font-Bold="true"
                                                    ForeColor="White"></asp:Label>
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>

                                                <table style="table-layout: fixed">
                                                    <tr>
                                                    </tr>

                                                    <tr>

                                                        <td></td>
                                                        <th style="text-align: center" width="175">Property Type</th>
                                                        <th style="text-align: center" width="175">Policy Type</th>
                                                        <th style="text-align: center" width="175">Number of Families</th>
                                                        <th style="text-align: center" width="175"><font color="#FF0000">Limit</font></th>
                                                        <th style="text-align: center" width="175">Medical Payments</th>
                                                        <th style="text-align: center" width="175">Premium</th>



                                                    </tr>
                                                    <tr>

                                                        <td width="100"><strong>Residence 1</strong></td>
                                                        <td>
                                                            <asp:TextBox ID="txtLiaPropertyType" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtLiaPolicyType" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtLiaNumOfFamilies" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>

                                                        <td>
                                                            <asp:DropDownList ID="ddlLiaLimit" runat="server"
                                                                CssClass="form-controlWhite"
                                                                TabIndex="1029" Height="30">
                                                                <asp:ListItem>$0.00</asp:ListItem>
                                                                <asp:ListItem>$50,000</asp:ListItem>
                                                                <asp:ListItem>$100,000</asp:ListItem>
                                                                <asp:ListItem>$300,000</asp:ListItem>
                                                                <asp:ListItem>$500,000</asp:ListItem>
                                                                <asp:ListItem>$1,000,000</asp:ListItem>
                                                            </asp:DropDownList></td>
                                                        <td>
                                                            <asp:TextBox ID="txtLiaMedicalPayments" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                        <td>
                                                            <asp:TextBox ID="txtLiaPremium" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029" ClientIDMode="Static"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                    </tr>

                                                    <tr>
                                                        <br />
                                                        <td><strong>Total</strong></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txtPremium" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                    </tr>

                                                    <tr>
                                                        <br />

                                                    </tr>



                                                </table>


                                                <br />
                                                <br />
                                               <div class="form-group" align="center">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="10">
                                <ProgressTemplate>
                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                    <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span></ProgressTemplate>
                            </asp:UpdateProgress>
                            </div>
                                               
                                            <div class="col-sm-12" align="center">
                                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True"
                                                    TabIndex="3000" Text=" Same as Client Information" Visible="False" />

                                                <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Red" Text="MODIFY DRIVER: ON"
                                                    Visible="False"></asp:Label>
                                                <br />

                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-1"></div>
                                                <div class="col-sm-3">
                                                </div>
                                            </div>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                                <div class="col-sm-12" align="center">
                                            <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                            <br />
                                            <br />
                                            <cc1:Mirror id="Mirror1" ControlID="btnAdjuntar" runat="server" />
                                            <asp:Button ID="BtnEdit2" runat="server" TabIndex="9000" Text="MODIFY"
                                CssClass="btn btn-primary btn-lg" OnClick="btnEdit_Click" Width="230px" Visible="false" />
                                            <asp:Button ID="btnCalculate2" runat="server" TabIndex="9000" Text="CALCULATE"
                                                CssClass="btn btn-primary btn-lg" Width="230px" 
                                                OnClick="btnCalculate_Click" />
                                            <%--<asp:Button ID="btnIssuePolicy" runat="server" 
                                        UseSubmitBehavior="false" TabIndex="9000"
                                            Text="ISSUE POLICY" CssClass="btn btn-primary btn-lg" Width="220px" />--%>
                                            <asp:Button ID="btnSavePolicy2" runat="server" TabIndex="9000"
                                                Text="ISSUE POLICY" Visible="False" CssClass="btn btn-primary btn-lg" 
                                                Width="220px" OnClick="btnSavePolicy_Click" />

                                            <asp:Button ID="BtnPrintPolicy2" runat="server" OnClick="btnPrint_Click" TabIndex="9000"
                                                Text="PRINT" Visible="False" CssClass="btn btn-primary btn-lg" Width="220px" />

                                            <cc1:Mirror id="Mirror3" ControlID="btnPrintDec" runat="server" />
                                            <cc1:Mirror id="Mirror6" ControlID="btnPrintInvoice" runat="server" />

                                            <asp:Button ID="btnPrintQuote2" runat="server" OnClick="btnPrintQuote_Click" TabIndex="9000"
                                                Text="PRINT QUOTE" Visible="false" CssClass="btn btn-primary btn-lg" Width="220px" />

                                            <asp:Button ID="btnQuote2" runat="server" TabIndex="9000"
                                                Text="SAVE QUOTE" OnClick="btnQuote_Click" CssClass="btn btn-primary btn-lg" Width="220px" />
                                            <cc1:Mirror ID="Mirror5" runat="server" ControlID="BtnPremiumFinance" />
                                            <%--<asp:Button ID="btnReinstallation" runat="server" 
                                            CssClass="btn btn-primary btn-lg" 
                                            Style="margin-left: 10px;" TabIndex="73" Text="REINSTALLATION" Visible="False" 
                                            Width="200px" />--%>
                                            <asp:Button ID="btnCancellation2" runat="server"
                                                CssClass="btn btn-primary btn-lg"
                                                Style="margin-left: 10px;" TabIndex="72" Text="CANCELLATION" Visible="False"
                                                Width="200px" />
                                            <asp:Button ID="BtnRenew2" runat="server" CssClass="btn btn-primary btn-lg" Width="220px"
                                                TabIndex="70" Text="RENEW" Visible="False" />
                                            <asp:Button ID="BtnDelete2" runat="server" CssClass="btn btn-primary btn-lg" Width="220px"
                                                TabIndex="74" Text="DELETE" Visible="False" />
                                            <asp:Button ID="btnSubmit2" runat="server" TabIndex="9000" Text="SUBMIT"
                                                CssClass="btn btn-primary btn-lg" Width="230px" OnClick="btnSubmit_Click" />
                                            <asp:Button ID="BtnCancel2" runat="server" CssClass="btn btn-primary btn-lg" Width="220px"
                                                TabIndex="110" Text="CANCEL" Visible="False" onclick="BtnCancel_Click" />
                                            &nbsp;<asp:Button ID="BtnConvert2" runat="server" CssClass="btn btn-primary btn-lg"
                                                Width="220px" TabIndex="72" Text="CONVERT QUOTE" OnClick="btnConvert_Click"
                                                Visible="False" />

                                            <cc1:Mirror ID="Mirror4" runat="server" ControlID="btnStatus" />

                                            <asp:Button ID="BtnExit2" runat="server" TabIndex="9000" Text="EXIT"
                                                CssClass="btn btn-primary btn-lg" Width="220px" onclick="BtnExit_Click" />
                                            <br />
                                            <br />
                                    <div align="left">
                                        <br />
                                        <br />
                                        <asp:TextBox ID="TextBox1" Height="30" runat="server" 
                                        Enabled="false" Visible="false" TabIndex="1029" CssClass="form-controlWhite"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--BANK LIST PANEL START--%>
                    <asp:Panel ID="pnlBankList" runat="server" BackColor="#F4F4F4" BorderWidth="1px" style="box-shadow: 0px 11px 24px -3px rgba(0,0,0,0.75)" 
                        CssClass="" Width="700px">
                        <div style="height:3px; background-color: #808080;">
                        </div>
                        <asp:Button ID="btnclosebanklist" style="background: transparent url('Images2/close-sign.png') 0 0 no-repeat;position: absolute;top: 5px;right: 5px;width:40px;height:48px;display:block;"></asp:Button>
                        <div align="right" class="accordion" 
                            style="margin: 10px,10px,10px,10px;
                                    font-size: 14px; font-weight: normal; font-style: normal; background-repeat: no-repeat;
                                    text-align: left; vertical-align: bottom; background-color: #808080;">
                            <div class="accordion" 
                                style="float:left; width:50%; height:42px; background-color: #808080;">
                                <asp:Label ID="Label121" runat="server" Font-Bold="False" Font-Italic="False" 
                                    Font-Names="Gotham Book" Font-Size="14pt" ForeColor="White" 
                                    style="margin-left:5px; margin-top:10px;" Text="Select Bank" />
                            </div>
                            <div class="accordion" 
                                style="float:right; text-align:right; width:50%; height:42px; background-color: #808080;">
                            </div>
                        </div>
                        <div align="center">
                            <br />
                            <asp:UpdateProgress ID="UpdateProgress7" runat="server" 
                                AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="10">
                                <progresstemplate>
                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                    <span><span class=""></span><span class="" style="font-size: 18px">Please 
                                    wait...</span></span>
                                </progresstemplate>
                            </asp:UpdateProgress>
                        </div>
                        <div align="center">
                            <p style="padding: 2%;">
                                <asp:Label ID="labelBankList" Text="Bank" runat="server"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlBankList" runat="server" TabIndex="7010" CssClass="form-controlWhite"
                                OnSelectedIndexChanged="ddlBankList_SelectedIndexChanged" AutoPostBack="True" EnableViewState="true" >
                                </asp:DropDownList>
                                <br />
                                <br />
                                <asp:Label ID="lblBankListSelected" Text="<b>ADDRESS:</b>" runat="server"></asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="labelBankList2" Text="Bank 2" runat="server"></asp:Label>
                                <br />
                                <asp:DropDownList ID="ddlBankList2" runat="server" TabIndex="7010" CssClass="form-controlWhite"
                                OnSelectedIndexChanged="ddlBankList2_SelectedIndexChanged" AutoPostBack="True" EnableViewState="true" >
                                </asp:DropDownList>
                                <br />
                                <br />
                                <asp:Label ID="lblBankListSelected2" Text="<b>ADDRESS:</b>" runat="server"></asp:Label>
                            </p>
                            <p>
                                &nbsp;</p>
                            <div align="center" class="">
                            </div>
                            </div>
                        <br />
                        </asp:Panel>
                    <asp:RoundedCornersExtender ID="pnlBankList_RoundedCornersExtender" 
                        runat="server" Corners="All" Radius="10" TargetControlID="pnlBankList" />
                    <asp:Button ID="btnBankListOpen" runat="server" BackColor="Transparent" 
                        BorderColor="Transparent" BorderStyle="None" BorderWidth="0" Visible="true" />
                    <asp:ModalPopupExtender ID="mpeBankList" runat="server" 
                        BackgroundCssClass="modalBackground" DropShadow="True" OkControlID="btnclosebanklist" 
                        PopupControlID="pnlBankList" TargetControlID="btnBankListOpen">
                    </asp:ModalPopupExtender>
                    <%--BANK LIST PANEL END--%>

                    <!--#region UPLOAD DOCUMENTS  -->
                     <div style="font-weight: 700">

                       <asp:Panel ID="pnlAdjunto" runat="server" BackColor="#F4F4F4" CssClass="" Width="750px" ScrollBars="Vertical" Height="632px">
                        <div class="accordion" style="padding: 0px;
                        font-size: 14px; font-weight: normal; font-style: normal; background-repeat: no-repeat;
                        text-align: left; vertical-align: bottom; background-color: #808080;">
                               &nbsp;&nbsp;&nbsp;<asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Gotham Book" Font-Size="14pt" ForeColor="White" Text="Attach Documents" />
                           </div>
                           <div class="">
                               <table class="MainMenu" style="width: 78%; margin-right: 0px; height: 276px;">
                                   <tr>
                                       <td align="left">
                                           <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="Gotham Book">Description:</asp:Label>
                                       </td>
                                       <td align="left">
                                           <asp:TextBox ID="txtDocumentDesc" runat="server" CssClass="textEntry" Width="350px" Height="31px"></asp:TextBox>
                                           &nbsp;
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="left">
                                           <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Gotham Book">Type of Transaction:</asp:Label>
                                       </td>
                                       <td align="left">
                                                <asp:DropDownList ID="ddlTransaction" runat="server" 
                                                    CssClass="form-controlWhite" Width="350px" Height="31px" AutoPostBack="True" EnableViewState="true" 
                                                    onselectedindexchanged="ddlTransaction_SelectedIndexChanged"></asp:DropDownList>
                                           &nbsp;
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="left" colspan="2" valign="middle">
                                           <asp:FileUpload ID="FileUpload1" runat="server" BorderColor="#8CB3D9" BorderStyle="Double" BorderWidth="1px" CssClass="btn" Height="33px" Width="400px" Font-Names="Gotham Book" />
                                           <br/>
                                           <asp:Button ID="btnAdjuntarCargar" runat="server" 
                                               onclick="btnAdjuntarCargar_Click" style="margin-left: 0px; margin-right: 20px;" Text="Load Document" Width="135px" CssClass="btn btn-primary" Font-Names="Gotham Book" />
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="left" colspan="2" valign="middle"><strong>
                                           <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Gotham Book">You can upload an image or PDF with a maximum size of 12MB.</asp:Label>
                                           </strong></td>
                                   </tr>
                                   <tr>
                                       <td align="left" colspan="2">
                                           <img alt="" src="" width="740" style="height: 48px" class="hidden" />
                                           <asp:Button ID="btnAceptar3" runat="server" CssClass="btn btn-primary" Font-Names="Gotham Book" Height="33px" Text="Exit" Width="80px" />
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="left" class="style12" colspan="2">
                                           &nbsp;&nbsp;
                                           <asp:GridView ID="gvAdjuntar" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                               CellPadding="1" Font-Names="Gotham Book" Font-Size="9pt" ForeColor="Black" GridLines="Horizontal" Height="224px" 
                                              onrowcommand="gvAdjuntar_RowCommand" onrowcreated="gvAdjuntar_RowCreated" onrowdeleting="gvAdjuntar_RowDeleting" PageSize="5" Width="696px">
                                               <Columns>
                                                   <asp:ButtonField ButtonType="Button" CommandName="View" HeaderText="View" ItemStyle-HorizontalAlign="Center">
                                                   <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                   <ItemStyle HorizontalAlign="Center" />
                                                   </asp:ButtonField>
                                                   <asp:BoundField DataField="DocumentsID" HeaderText="DocumentsID">
                                                   <ControlStyle Width="200px" />
                                                   <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                   </asp:BoundField>
                                                   <asp:BoundField DataField="Description" HeaderText="Description">
                                                   <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                                   <ItemStyle Width="145px" />
                                                   </asp:BoundField>
                                                   <asp:BoundField DataField="TaskControlTypeDesc" HeaderText="Transaction Type">
                                                   <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                                   <ItemStyle Width="145px" />
                                                   </asp:BoundField>
                                                   <asp:BoundField DataField="TaskControlID" HeaderText="Control #">
                                                   <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                                   <ItemStyle Width="145px" />
                                                   </asp:BoundField>
                                                    <asp:ButtonField ButtonType="Button" CommandName="Delete" HeaderText="Delete" ItemStyle-HorizontalAlign="Left" CausesValidation="False">
                                                    </asp:ButtonField>
                                               </Columns>
                                               <HeaderStyle BackColor="#CCCCCC" Height="15px" />
                                               <RowStyle Height="30px" />
                                           </asp:GridView>
                                       </td>
                                   </tr>
                               </table>
                           </div>
                           <div align="center" class="">
                           </div>
                       </asp:Panel>                
                       <asp:ModalPopupExtender ID="mpeAdjunto" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" PopupControlID="pnlAdjunto" TargetControlID="btnDummyDoc" OkControlID="btnAceptar">
                </asp:ModalPopupExtender>
                <asp:Button ID="btnDummyDoc" runat="server" Visible="true" BackColor="Transparent" BorderStyle="None"
                    BorderWidth="0" BorderColor="Transparent" />
                     <asp:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server"
                            TargetControlID="pnlAdjunto"
                            Radius="10"
                            Corners="All" />


                        <asp:Panel ID="pnlAlert" runat="server" BackColor="#F4F4F4" CssClass="" Width="750px">
                            <div class="" style="padding: 0px; border-radius: 0px; background-color: #17529B;
                                color: #FFFFFF; font-size: 14px; font-weight: normal; 
                                background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                                &nbsp;&nbsp;
                                <asp:Label ID="Label51" runat="server"
                                    Font-Size="14pt" Text="Message..." ForeColor="White" />
                                <asp:Button ID="btnclosealert" style="background: transparent url('Images2/close-sign.png') 0 0 no-repeat;position: absolute;top: 5px;right: 5px;width:40px;height:48px;display:block;"></asp:Button>
                            </div>
                            <div style="padding: 10px; font-size: 14px; font-weight: normal; font-style: normal; background-repeat: no-repeat;
                                text-align: left; vertical-align: bottom;">
                                <%--a.	Recuerde que al finalizar la emisión de la póliza, tiene que adjuntar los siguientes documentos al cliente. 
•	Insured ID
•	Vehicle Registration
•	Signed Application--%>
                                <asp:PlaceHolder ID="phAlert" runat="server"></asp:PlaceHolder>
                                <cc1:Mirror id="Mirror2" ControlID="btnAdjuntar" runat="server" />

                                 <div  align="left">
                                             <br/>
                                                <asp:Label ID="lblAlertComment" runat="server" Text="Comment:" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtAlertComment" Style="text-transform: uppercase" runat="server" TabIndex="10040" TextMode="MultiLine" 
                                                    CssClass="form-controlWhite" Height="125px"></asp:TextBox>
                                            </div>
                                             <br />
                                             <div  align="center">

                                             <asp:Label ID="lblAlertEntryDate" runat="server" Text="Submitted Date:" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                    <asp:TextBox ID="txtAlertEntryDate" Style="text-transform: uppercase" Height="35px" runat="server" TabIndex="1029" Enabled="False"
                                                                CssClass="form-controlWhite" width="160px" ></asp:TextBox>
                                             </div>
                                              <br />

                                <asp:Button ID="btnSubmitAlert" runat="server" TabIndex="9000" Text="SUBMIT" CssClass="btn btn-primary btn-lg" Width="230px" 
                                OnClientClick=" this.disabled = true; this.value = 'Submitting...';" UseSubmitBehavior="false" 
                                OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-primary btn-lg" 
                                    OnClick="btnStatus_Click" TabIndex="9000" Text="APPROVE" Width="230px" />
                                <asp:Button ID="btnRejected" runat="server" CssClass="btn btn-primary btn-lg" 
                                    OnClick="btnStatus_Click" TabIndex="9000" Text="DECLINE" Width="230px" />
                                <asp:Button ID="btnRevert" runat="server" CssClass="btn btn-primary btn-lg" 
                                    OnClick="btnStatus_Click" TabIndex="9000" Text="REVERT APPROVAL" 
                                    Width="230px" />
                            </div>   
                       </asp:Panel>                
                       <asp:ModalPopupExtender ID="mpeAlert" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" PopupControlID="pnlAlert" TargetControlID="btnDummyAlert" OkControlID="btnclosealert">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="btnDummyAlert" runat="server" Visible="true" BackColor="Transparent" BorderStyle="None"
                        BorderWidth="0" BorderColor="Transparent" />
                         <asp:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server"
                                TargetControlID="pnlAlert" Radius="10" Corners="All" />
                    </div>
                     <!--#endregion UPLOAD DOCUMENTS -->

                       <asp:Panel ID="pnlMessage" runat="server" CssClass="" Width="450px" BackColor="#F4F4F4"
                    Height="260px">
                    <div class="" style="padding: 0px; border-radius: 0px; background-color: #17529B;
                        color: #FFFFFF; font-size: 14px; font-weight: normal; 
                        background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                        &nbsp;&nbsp;
                        <asp:Label ID="Label3" runat="server"
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

                                                   <asp:Panel ID="pnlEmailVerification" runat="server" CssClass="" Width="450px" BackColor="#F4F4F4"
                    Height="260px">
                    <div class="" style="padding: 0px; border-radius: 0px; background-color: #17529B;
                        color: #FFFFFF; font-size: 14px; font-weight: normal; 
                        background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                        &nbsp;&nbsp;
                        <asp:Label ID="LabelEmailVerification" runat="server"
                            Font-Size="14pt" Text="Email Confirmation" ForeColor="White" />
                    </div>
                    <div style="padding: 0px; border-radius: 0px;">
                        <table style="background-position: center; width: 430px; height: 175px;">
                            <tr>
                                <td align="center" valign="middle">
                                    <asp:Label ID="lblEmailVerification1" runat="server" Font-Bold="False" Font-Italic="False"
                                        Font-Size="10.5pt" Font-Underline="False" ForeColor="#333333" Text="Please Confirm The Email."
                                        Width="350px" CssClass="Labelfield2-14" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle">
                                  <asp:TextBox ID="txtEmailVerification" Style="text-transform: uppercase" runat="server" onpaste="return false" autocomplete="Disabled" 
                                              CssClass="form-controlWhite"></asp:TextBox>
                            </tr>
                        </table>
                    </div>
                    <div class="" align="center">
                        <asp:Button ID="btnAceptarEmailVerification" runat="server" Text="OK" Width="150px"  style="display:none" CssClass="btn btn-primary btn-lg btn-block" />
                    </div>
                </asp:Panel>
                <Toolkit:ModalPopupExtender ID="mpeEmailVerification" runat="server" BackgroundCssClass=""
                    DropShadow="True" PopupControlID="pnlEmailVerification" TargetControlID="ButtonEmailVerification" OkControlID="btnAceptarEmailVerification">
                </Toolkit:ModalPopupExtender>
                <asp:Button ID="ButtonEmailVerification" runat="server" Visible="true" BackColor="Transparent"
                    BorderStyle="None" BorderWidth="0" BorderColor="Transparent" />
                <Toolkit:RoundedCornersExtender ID="RoundedCornersExtenderEmailVerification" runat="server" TargetControlID="pnlEmailVerification"
                    Radius="0" Corners="All" />
                <br />
                <br />

                    <asp:Panel ID="pnlAlert2" runat="server" BackColor="#F4F4F4" CssClass="" Width="750px">
                            <div class="" style="padding: 0px; border-radius: 0px; background-color: #17529B;
                                color: #FFFFFF; font-size: 14px; font-weight: normal; 
                                background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                                &nbsp;&nbsp;
                                <asp:Label ID="Label120" runat="server"
                                    Font-Size="14pt" Text="Finance Options" ForeColor="White" />
                                <asp:Button ID="btnclosealert2" style="background: transparent url('Images2/close-sign.png') 0 0 no-repeat;position: absolute;top: 5px;right: 5px;width:40px;height:48px;display:block;"></asp:Button>
                            </div>
                            <div style="padding: 0px; font-size: 14px; font-weight: normal; font-style: normal; background-repeat: no-repeat;
                                text-align: left; vertical-align: bottom;">
                                <div align="center">
                                <h4><b>PLEASE SELECT AN INSTALLMENT OPTION:</b></h4>
                                <br />
                                <h4><b>INSTALLMENTS</b></h4>
                                <%--<asp:Button ID="btnFM1" runat="server" Text="ACCEPT" CssClass="btn btn-primary btn-lg" onclick="btnFinanceMaster_Click"/>
                                <asp:Label ID="lblFM1" runat="server" Font-Bold="true" >--%>
                                <%--10 Payments, $365.00 Deposit, $75.62 Payment, 34.21% APR, $81.20 Interest, $25.00 Fee--%>
                                <%--</asp:Label>
                                <br />
                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                <br />--%>
                                <table style="width:95%;" border="1">
                                      <tr>
                                        <td>Payments</td>
                                        <td><asp:Button ID="btnFM2" runat="server" Text="3" 
                                                CssClass="btn btn-primary btn-lg" onclick="btnFinanceMaster_Click" 
                                                Enabled="False"/></td>
                                        <td><asp:Button ID="btnFM5" runat="server" Text="6" 
                                                CssClass="btn btn-primary btn-lg" onclick="btnFinanceMaster_Click" 
                                                Enabled="False"/></td> 
                                        <td><asp:Button ID="btnFM8" runat="server" Text="9" 
                                                CssClass="btn btn-primary btn-lg" onclick="btnFinanceMaster_Click" 
                                                Enabled="False"/></td>
                                      </tr>
                                      <tr>
                                        <td>Deposit</td>
                                        <td><asp:Label ID="lblFM2" runat="server" Font-Bold="true" ></asp:Label></td>
                                        <td><asp:Label ID="lblFM5" runat="server" Font-Bold="true" ></asp:Label></td> 
                                        <td><asp:Label ID="lblFM8" runat="server" Font-Bold="true" ></asp:Label></td>
                                      </tr>
                                      <tr>
                                        <td>Installment Amount</td>
                                        <td><asp:Label ID="LabelFM2_Payment" runat="server" Font-Bold="true" > </asp:Label></td>
                                        <td><asp:Label ID="LabelFM5_Payment" runat="server" Font-Bold="true" ></asp:Label></td> 
                                        <td><asp:Label ID="LabelFM8_Payment" runat="server" Font-Bold="true" ></asp:Label></td>
                                      </tr>
                                </table>
                                

                                <%--10 Payments, $365.00 Deposit, $75.62 Payment, 34.21% APR, $81.20 Interest, $25.00 Fee--%>
                                
                                
                               <%-- <br />
                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                <br />--%>
                                <%--<asp:Button ID="btnFM3" runat="server" Text="ACCEPT" CssClass="btn btn-primary btn-lg" onclick="btnFinanceMaster_Click"/>
                                <asp:Label ID="lblFM3" runat="server" Font-Bold="true" >--%>
                                <%--10 Payments, $365.00 Deposit, $75.62 Payment, 34.21% APR, $81.20 Interest, $25.00 Fee--%>
                               <%-- </asp:Label>
                                <br />
                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                <br />--%>
                                <%--<asp:Button ID="btnFM4" runat="server" Text="ACCEPT" CssClass="btn btn-primary btn-lg" onclick="btnFinanceMaster_Click"/>
                                <asp:Label ID="lblFM4" runat="server" Font-Bold="true" >--%>
                                <%--10 Payments, $365.00 Deposit, $75.62 Payment, 34.21% APR, $81.20 Interest, $25.00 Fee--%>
                                <%--</asp:Label>
                                <br />
                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                <br />--%>
                               
                                
                                
                                <%--10 Payments, $365.00 Deposit, $75.62 Payment, 34.21% APR, $81.20 Interest, $25.00 Fee--%>
                                
                                 
                               <%-- <br />
                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                <br />--%>
                                <%--<asp:Button ID="btnFM6" runat="server" Text="ACCEPT" CssClass="btn btn-primary btn-lg" onclick="btnFinanceMaster_Click"/>
                                <asp:Label ID="lblFM6" runat="server" Font-Bold="true" >--%>
                                <%--10 Payments, $365.00 Deposit, $75.62 Payment, 34.21% APR, $81.20 Interest, $25.00 Fee--%>
                               <%-- </asp:Label>
                                <br />
                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                <br />--%>
                                <%--<asp:Button ID="btnFM7" runat="server" Text="ACCEPT" CssClass="btn btn-primary btn-lg" onclick="btnFinanceMaster_Click"/>
                                <asp:Label ID="lblFM7" runat="server" Font-Bold="true" >--%>
                                <%--10 Payments, $365.00 Deposit, $75.62 Payment, 34.21% APR, $81.20 Interest, $25.00 Fee--%>
                                <%--</asp:Label>
                                <br />
                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                <br />--%>
                                
                                
                                
                                <%--10 Payments, $365.00 Deposit, $75.62 Payment, 34.21% APR, $81.20 Interest, $25.00 Fee--%>
                                <%--</asp:Label>
                                <br />
                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                <br />--%>
                                <%--<asp:Button ID="btnFM9" runat="server" Text="ACCEPT" CssClass="btn btn-primary btn-lg" onclick="btnFinanceMaster_Click"/>
                                <asp:Label ID="lblFM9" runat="server" Font-Bold="true" >--%>
                                <%--10 Payments, $365.00 Deposit, $75.62 Payment, 34.21% APR, $81.20 Interest, $25.00 Fee--%>
                                
                                
                                <br />
                                <br />
                                <asp:PlaceHolder ID="phAlert2" runat="server"></asp:PlaceHolder>
                                </div>
                            </div>
                       </asp:Panel>                
                       <asp:ModalPopupExtender ID="mpeAlert2" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" PopupControlID="pnlAlert2" TargetControlID="btnDummyAlert2" OkControlID="btnclosealert2">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="btnDummyAlert2" runat="server" Visible="true" BackColor="Transparent" BorderStyle="None"
                        BorderWidth="0" BorderColor="Transparent" />
                         <asp:RoundedCornersExtender ID="RoundedCornersExtender5" runat="server"
                                TargetControlID="pnlAlert2" Radius="10" Corners="All" />
                    
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--10 Payments, $365.00 Deposit, $75.62 Payment, 34.21% APR, $81.20 Interest, $25.00 Fee--%>
        </div>
    </form>
</body>
</html>
