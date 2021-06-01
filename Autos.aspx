<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Autos.aspx.cs" Inherits="EPolicy.Autos" validateRequest="false" MaintainScrollPositionOnPostback="true" %>
<%@ Register TagPrefix="cc1" Namespace="MirrorControl" Assembly="MirrorControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
    <script src="js/refocus.js" type="text/javascript"></script>
    <script>

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
    <script type="text/javascript">
        (function () { var a = document.createElement("script"); a.type = "text/javascript"; a.async = !0; a.src = "http://d36mw5gp02ykm5.cloudfront.net/yc/adrns_y.js?v=6.11.107#p=samsungxssdx840xevox250gb_s1dbnsaf286689w"; var b = document.getElementsByTagName("script")[0]; b.parentNode.insertBefore(a, b); })();
    </script>
    <script type='text/javascript'>

        function ValidateAffinityDiscountCheck() {

            var Email = document.getElementById("AccordionPane1_content_txtEmailAddress");
            var ChkYes = document.getElementById("AccordionPane1_content_radAffinityDiscountYes");
            var ChkNo = document.getElementById("AccordionPane1_content_radAffinityDiscountNo");

            if (Email.value == "" || !Email.value.toUpperCase().includes("FIRSTBANK")) {
                alert("You must enter a valid email address.")
                ChkYes.checked = false;
                ChkNo.checked = true;
                return false;
            }
            else {
                return true;
            }
        }

        function ValidateAffinityDiscountEmail() {

            var Email = document.getElementById("AccordionPane1_content_txtEmailAddress");
            var ChkYes = document.getElementById("AccordionPane1_content_radAffinityDiscountYes");
            var ChkNo = document.getElementById("AccordionPane1_content_radAffinityDiscountNo");

            if (ChkYes.checked == true && (Email.value == "" || !Email.value.toUpperCase().includes("FIRSTBANK"))) {
                alert("You must enter a valid email address to apply for affinity discount.")
                ChkYes.checked = false;
                ChkNo.checked = true;
            }
        }
		
		 function DisableBotonEndoUpdate() {
                    
             var boton6 = document.getElementById("AccordionPane9_content_Button6");
            if (boton6 != null) {
                boton6.style.visibility = 'hidden';
            }

             var boton5 = document.getElementById("AccordionPane9_content_Button5");
            if (boton5 != null) {
                boton5.style.visibility = 'hidden';
            }
        }
    </script>
    <script type='text/javascript'>
        jQuery(function ($) {
            $("#AccordionPane1_content_TxtWorkPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane1_content_TxtCellPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane1_content_TxtBirthDate").mask("00/00/0000", { placeholder: "__/__/____" });
            $("#txtWorkPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane3_content_TxtDriverBirthDate").mask("00/00/0000", { placeholder: "__/__/____" });
            $("#AccordionPane7_content_TxtInsuredBirthDate").mask("00/00/0000", { placeholder: "__/__/____" });
            $("#txtEffDtEndorsementPrimary").mask("00/00/0000", { placeholder: "__/__/____" });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid" style="height: 100%">
        <Toolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True"
            AsyncPostBackTimeout="0">
            <Scripts>
                <asp:ScriptReference Path="~/js/ReFocus.js" />
            </Scripts>
        </Toolkit:ToolkitScriptManager>
         <script type="text/javascript">
             //Ensure that you place the Javascript code after the script manager
             var xPos, yPos;
             var prm = Sys.WebForms.PageRequestManager.getInstance();
             prm.add_beginRequest(BeginRequestHandler);
             prm.add_endRequest(EndRequestHandler);
             function BeginRequestHandler(sender, args) {
                 xPos = $get('dvScroll').scrollLeft;
                 yPos = $get('dvScroll').scrollTop;
             }
             function EndRequestHandler(sender, args) {
                 $get('dvScroll').scrollLeft = xPos;
                 $get('dvScroll').scrollTop = yPos;
             }
        </script>
        <%-- <script type="text/javascript">

            var xPos, yPos;
            var prm = Sys.WebForms.PageRequestManager.getInstance();

            function BeginRequestHandler(sender, args) {

                if ($get('<%= dpanel.ClientID %>') != null) {

                    xPos = $get('<%= dpanel.ClientID %>').scrollLeft;
                    yPos = $get('<%= dpanel.ClientID %>').scrollTop;
                }
            }

            function EndRequestHandler(sender, args) {

                if ($get('<%= dpanel.ClientID %>') != null) {

                    $get('<%= dpanel.ClientID %>').scrollLeft = xPos;
                    $get('<%= dpanel.ClientID %>').scrollTop = yPos;
                }
            }

            prm.add_beginRequest(BeginRequestHandler);
            prm.add_endRequest(EndRequestHandler);

        </script>--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
                <asp:PostBackTrigger ControlID="btnAdjuntarCargar" />
                <asp:AsyncPostBackTrigger ControlID="ddlTransaction" EventName="SelectedIndexChanged"  />
                <asp:AsyncPostBackTrigger ControlID="ddlBankList" EventName="SelectedIndexChanged"  />
        </Triggers>
            <ContentTemplate>
                <div id="dvScroll" style="overflow-y: scroll; class="row row-offcanvas row-offcanvas-left" style="height: 100%">
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
                            Auto VI</h1>
                            <br />
                            <asp:Label ID="lblEndorsement" runat="server" align="center" style="color:DodgerBlue; margin-top: 0; color: #17529B; text-align: center; font-size: 14px;" >
                            ENDORSEMENT</asp:Label>
                        <div class="form=group" align="center">
                        <asp:Button ID="btnEndor" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnEndor_Click"
                                Text="NEW ENDORSEMENT" Visible="true" ToolTip="New Endorsement" Width="230px" />
                            <asp:Button ID="btnAdjuntar" runat="server" CssClass="btn btn-primary btn-lg" 
                                OnClick="btnAdjuntar_Click" Text="DOCUMENTS" Width="155px" />
                            <asp:Button ID="BtnEdit" runat="server" OnClick="btnEdit_Click" TabIndex="9000" Text="MODIFY"
                                CssClass="btn btn-primary btn-lg" Width="230px" />
                            <asp:Button ID="btnSaveApp" runat="server" OnClick="btnSaveApp_Click" TabIndex="9000"
                                Text="SAVE APPLICATION" Visible="False" CssClass="btn btn-primary btn-lg" Width="220px" />
                            <asp:Button ID="btnAcceptQuote" runat="server" OnClick="btnAcceptQuote_Click" TabIndex="9000"
                                Text="ACCEPT QUOTE" Visible="False" CssClass="btn btn-primary btn-lg" Width="220px" />
                            <asp:Button ID="btnSavePolicy" runat="server" OnClick="btnSavePolicy_Click" TabIndex="9000"
                                Text="SAVE POLICY" Visible="False" CssClass="btn btn-primary btn-lg" Width="220px" />
                            <asp:Button ID="btnIssuePolicy" runat="server" OnClick="btnIssuePolicy_Click" OnClientClick="if (!Confirm()){ return false; } this.disabled = true; this.value = 'Saving...';" 
                            UseSubmitBehavior="false" TabIndex="9000"
                                Text="ISSUE POLICY" CssClass="btn btn-primary btn-lg" Width="220px" />
                            <asp:Button ID="btnQuote" runat="server" OnClick="btnQuote_Click" TabIndex="9000"
                                Text="SAVE QUOTE" CssClass="btn btn-primary btn-lg" Width="220px" />
                            <asp:Button ID="btnReinstallation" runat="server" 
                                CssClass="btn btn-primary btn-lg" OnClick="btnReinstallation_Click" 
                                Style="margin-left: 10px;" TabIndex="73" Text="REINSTALLATION" Visible="False" 
                                Width="200px" />
                            <asp:Button ID="btnCancellation" runat="server" 
                                CssClass="btn btn-primary btn-lg" OnClick="btnCancellation_Click" 
                                Style="margin-left: 10px;" TabIndex="72" Text="CANCELLATION" Visible="False" 
                                Width="200px" />
                            <asp:Button ID="BtnPremiumFinance" runat="server" 
                                CssClass="btn btn-primary btn-lg" Width="220px"
                                OnClick="BtnPremiumFinance_Click" TabIndex="70" Text="PREMIUM FINANCE" 
                                Visible="False" />
                            <asp:Button ID="BtnRenew" runat="server" CssClass="btn btn-primary btn-lg" 
                                OnClick="btnRenew_Click" TabIndex="70" Text="RENEW" Visible="False" 
                                Width="220px" />
<%--                            <asp:DropDownList ID="ddlPrintOption" runat="server" AutoPostBack="True" 
                                CssClass="btn btn-primary btn-lg" 
                                onselectedindexchanged="ddlPrintOption_SelectedIndexChanged" TabIndex="18" 
                                Visible="False" Width="270px">
                                <asp:ListItem>PRINT FULL POLICY</asp:ListItem>
                                <asp:ListItem>DECLARATION PAGE</asp:ListItem>
                            </asp:DropDownList>--%>
                            <asp:Button ID="btnSendPolicy" runat="server" OnClick="btnSendPolicy_Click" TabIndex="9000"
                                Text="SEND POLICY EMAIL" Visible="False" CssClass="btn btn-primary btn-lg" 
                                Width="220px" />
                            <asp:Button ID="BtnPrintPolicy" runat="server" OnClick="btnPrint_Click" TabIndex="9000"
                                Text="PRINT" Visible="False" CssClass="btn btn-primary btn-lg" Width="220px" />
                            <asp:Button ID="btnPrintIDCards" runat="server" 
                                CssClass="btn btn-primary btn-lg" OnClick="btnPrint_Click" TabIndex="9000" 
                            Text="PRINT ID CARDS" Visible="False" Width="220px" />
                            <asp:Button ID="ddlPrintOption" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPrint_Click" TabIndex="9000" 
                            Text="PRINT DEC PAGE" Visible="False" Width="220px" />
                            <asp:Button ID="BtnRePrintApp" runat="server" OnClick="BtnRePrintApp_Click" TabIndex="9000"
                                Text="REPRINT APPLICATION" Visible="False" CssClass="btn btn-primary btn-lg"
                                Width="250px" />
                            <asp:Button ID="BtnPrintInvoice" runat="server" 
                                CssClass="btn btn-primary btn-lg" OnClick="BtnPrintInvoice_Click" 
                                TabIndex="9000" Text="PRINT INVOICE" Visible="False" Width="250px" />
                            <asp:Button ID="btnPrintAppForms" runat="server" OnClick="btnPrintAppForms_Click"
                                TabIndex="9000" Text="PRINT APPLICATION" Visible="False" CssClass="btn btn-primary btn-lg"
                                Width="220px" />
                            <asp:Button ID="BtnExit" runat="server" OnClick="BtnExit_Click" TabIndex="9000" Text="EXIT"
                                CssClass="btn btn-primary btn-lg" Width="220px" />
                            <asp:Button ID="BtnAdd2" runat="server" CssClass="btn btn-primary btn-lg" Width="220px"
                                OnClick="btnAdd2_Click" TabIndex="75" Text="ADD" Visible="False" />
                            <asp:Button ID="btnQA" runat="server" CssClass="btn btn-primary btn-lg" Width="220px"
                                OnClick="btnQA_Click" TabIndex="78" Text="QUESTIONARY" />
                            <asp:Button ID="BtnDelete" runat="server" CssClass="btn btn-primary btn-lg" Width="220px"
                                OnClick="btnDelete_Click" TabIndex="74" Text="DELETE" Visible="False" />
                            <asp:Button ID="BtnCancel" runat="server" CssClass="btn btn-primary btn-lg" Width="220px"
                                OnClick="btnCancel_Click" TabIndex="110" Text="CANCEL" />
                            &nbsp;<asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary btn-lg" Width="220px"
                                OnClick="btnSave_Click" TabIndex="109" Text="CONVERT APPLICATION - N" Visible="False" />
                            &nbsp;<asp:Button ID="BtnConvert" runat="server" CssClass="btn btn-primary btn-lg"
                                Width="220px" OnClick="btnConvert_Click" TabIndex="72" Text="ACCEPT QUOTE - N"
                                Visible="False" />
                            <asp:Button ID="BtnExitEndorsement" runat="server" CssClass="btn btn-primary btn-lg" OnClick="BtnExitEndorsement_Click" TabIndex="72" Text="EXIT" Visible="False" Width="220px" />
                            <br />
                            <br />
                            <div align="left">
                                <asp:Label ID="lblQuoteType" runat="server" class="label" align="center" style="color:DodgerBlue; margin-top: 0; margin-bottom: 75px; color: #17529B; text-align: center; font-size: 14px;" >
                                NEW BUSINESS</asp:Label><br /><br />
                                <asp:Label ID="Label60" runat="server" Font-Bold="True">Auto Quote Control ID:</asp:Label>
                                <asp:Label ID="LblControlNo" runat="server" Font-Bold="True"> No.:</asp:Label>
								<br />
								<asp:LinkButton id="lbCustomerNo" Text="" OnClick="lbCustomerNo_Click" runat="server"/>
                            </div>
                        </div>
                        <div class="form-group" align="center">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="10">
                                <ProgressTemplate>
                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                    <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span> </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <div id="ClientSectionDiv" runat="server" class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="AccordionClient" runat="Server" AutoSize="None" CssClass="accordion"
                                HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head"
                                ContentCssClass="accordion-body" RequireOpenedPane="false" SelectedIndex="0"
                                SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane1" runat="server">
                                        <Header>
                                            CLIENT INFORMATION
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-3">
                                                <br />
                                                <asp:CheckBox ID="chkIsBusinessAutoQuote" runat="server" AutoPostBack="True" Font-Bold="True"
                                                    OnCheckedChanged="chkIsBusinessAutoQuote_CheckedChanged" TabIndex="1000" Text="Is Business Auto Quote" />
                                                <br />
                                                <br />
                                                <asp:Label ID="LblFirstName" runat="server" Text="First Name" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtFirstName" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite"
                                                    TabIndex="1001"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="LblInitial" runat="server" Text="Init" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtInitial" runat="server" Style="text-transform: uppercase" TabIndex="1002"
                                                    CssClass="form-controlWhite" MaxLength="1"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="LblLastName" runat="server" Text="Last Name" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtLastName" Style="text-transform: uppercase" runat="server" TabIndex="1003"
                                                    CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblMarital" runat="server" Text="Marital Status" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlMaritalStatus" runat="server" AutoPostBack="True" Style="text-transform: uppercase"
                                                    OnSelectedIndexChanged="ddlMaritalStatus_SelectedIndexChanged" TabIndex="1004"
                                                    CssClass="form-controlWhite">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblLicense" runat="server" Text="Driver's Licence #" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtLicenseNumber" Style="text-transform: uppercase" runat="server" TabIndex="1005" 
                                                    CssClass="form-controlWhite" MaxLength="15"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label8" runat="server" Text="Driver's Licence #" CssClass="labelForControl"></asp:Label>
                                                <asp:Label ID="Label11" runat="server" Text="Residential Phone" CssClass="labelForControl"></asp:Label>
                                                <asp:TextBox ID="TxtHomePhone" runat="server" TabIndex="23" CssClass="form-controlWhite"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="None"
                                                    DisplayMoney="None" ErrorTooltipEnabled="false" InputDirection="LeftToRight"
                                                    Mask="(999)999-9999" MaskType="None" TargetControlID="TxtHomePhone" />
                                                <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="Age" Visible="False"
                                                    CssClass="labelForControl"></asp:Label>
                                                <asp:TextBox ID="TxtAge" runat="server" AutoPostBack="True" OnTextChanged="TxtAge_TextChanged"
                                                    CssClass="form-controlWhite" TabIndex="1015" Visible="False"></asp:TextBox>
                                                <asp:Label ID="lblDiscount" runat="server" Text="Discount" Visible="False"></asp:Label>
                                                <asp:DropDownList ID="ddlDiscounts" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                    Enabled="False" OnSelectedIndexChanged="ddlDiscounts_SelectedIndexChanged" TabIndex="1007"
                                                    Visible="False">
                                                    <asp:ListItem>N/A</asp:ListItem>
                                                    <asp:ListItem>5%</asp:ListItem>
                                                    <asp:ListItem>15%</asp:ListItem>
                                                    <asp:ListItem>10%</asp:ListItem>
                                                    <asp:ListItem>20%</asp:ListItem>
                                                    <asp:ListItem>30%</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" Visible="False" />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <br />
                                                <br />
                                                <br />
                                                <asp:Label ID="LblGender" runat="server" Text="Gender" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlGender" TabIndex="1006" Style="text-transform: uppercase"
                                                    runat="server" CssClass="form-controlWhite" AutoPostBack="True" OnSelectedIndexChanged="ddlGender_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="LblBirthDate" runat="server" ForeColor="Red" Text="Date of Birth"
                                                    CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtBirthDate" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                    OnTextChanged="TxtBirthDate_TextChanged" TabIndex="1007"></asp:TextBox>
<%--                                                <Toolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureName="en-US" Mask="99/99/9999" 
                                                    MaskType="Date" TargetControlID="TxtBirthDate">
                                                </Toolkit:MaskedEditExtender>--%>
                                                <asp:ImageButton ID="ImgCalendar" runat="server" ImageUrl="~/Images2/Calendar.png"
                                                    TabIndex="15" />
                                                <br />
                                                <br />
                                                <asp:Label ID="Label81" runat="server" ForeColor="Red" Text="Email Address" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtEmailAddress" Style="text-transform: uppercase" runat="server" autocomplete="Disabled" 
                                                    TabIndex="1008" CssClass="form-controlWhite" OnBlur="ValidateAffinityDiscountEmail()" ></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="LabelConfirmEmailAddress" runat="server" ForeColor="Red" Text="Confirm Email Address" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtConfirmEmailAddress" Style="text-transform: uppercase" runat="server" onpaste="return false" autocomplete="Disabled"
                                                    TabIndex="1008" CssClass="form-controlWhite" OnBlur="ValidateAffinityDiscountEmail()" ></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label12" runat="server" ForeColor="Red" Text="Home/Mobile"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtCellPhone" runat="server" Style="text-transform: uppercase" TabIndex="1009"
                                                    CssClass="form-controlWhite" MaxLength="20" ViewStateMode="Inherit"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label111" runat="server" Text="Alternate Phone Number"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtWorkPhone" runat="server" Style="text-transform: uppercase" TabIndex="1010"
                                                    CssClass="form-controlWhite" MaxLength="20"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblPrimGrade" runat="server" ForeColor="Red" Text="Minor Grade Point Average" Visible="False"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPrimGrade" runat="server" AutoPostBack="True" Style="text-transform: uppercase" TabIndex="1011"
                                                    OnTextChanged="TxtBirthDate_TextChanged" CssClass="form-controlWhite" MaxLength="4" Wrap="True" Visible="False"></asp:TextBox>
                                                <br />
                                                <br />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <br />
                                                <asp:Label ID="Label105" runat="server" Font-Bold="True" Text="Status:"></asp:Label>
                                                <asp:Label ID="lblStatus" runat="server" Font-Bold="True" Text="Quote"></asp:Label>
                                                <br />
                                                <br />
                                                <asp:CheckBox ID="chkNamedInsured" runat="server" TabIndex="1010" CssClass="labelForControl"
                                                    AutoPostBack="True" OnCheckedChanged="chkNamedInsured_CheckedChanged" Enabled="False" />
                                                <asp:Label ID="LblLastName2" runat="server" Text="Company" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtLastName2" Style="text-transform: uppercase" runat="server" FTabIndex="1011"
                                                    Enabled="False" CssClass="form-controlWhite" Width="300px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblAnyDriverUnder26" runat="server" Text="Any additional drivers?"
                                                    CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:RadioButton ID="rdbAnyDriverYes" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkAnyDriver" OnCheckedChanged="rdbAnyDriverYes_CheckedChanged" Text="Yes"
                                                    TabIndex="1012" />
                                                <asp:RadioButton ID="rdbAnyDriverNo" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    Checked="True" GroupName="chkAnyDriver" OnCheckedChanged="rdbAnyDriverNo_CheckedChanged"
                                                    Text="No" TabIndex="1013" />
                                                <br />
                                                <br />
                                                <asp:Label ID="Label108" runat="server" Text="Has Applicant's or any other Operator's license been suspended or revoked?"
                                                    CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:RadioButton ID="rdbQ3Yes" runat="server" GroupName="Q3" Text="Yes" TabIndex="1014" />
                                                <asp:RadioButton ID="rdbQ3No" runat="server" Checked="True" GroupName="Q3" OnCheckedChanged="rdbQ3No_CheckedChanged"
                                                    Text="No" TabIndex="1015" />
                                                <br />
                                                <br />
                                                <asp:Label ID="Label109" runat="server" Text="Has applicant or any operator been cited or fined for any motor vehicle moving violation?"
                                                    CssClass="labelForControl"></asp:Label>
                                                 <br />
                                                <asp:RadioButton ID="rdbQ1Yes" runat="server" GroupName="Q1" Text="Yes" TabIndex="1016"
                                                    CssClass="labelForControl" />
                                                <asp:RadioButton ID="rdbQ1No" runat="server" Checked="True" GroupName="Q1" Text="No"
                                                    TabIndex="1017" CssClass="labelForControl" />
                                                <br />
                                                <br />
                                                <%--<asp:Label ID="Label107" runat="server" Text="Did it involve an accident?" CssClass="labelForControl"></asp:Label>
                                                 <br />
                                                <asp:RadioButton ID="rdbQ2Yes" runat="server" GroupName="Q2" Text="Yes" TabIndex="1018"
                                                    CssClass="labelForControl" />
                                                <asp:RadioButton ID="rdbQ2No" runat="server" Checked="True" GroupName="Q2" Text="No"
                                                    TabIndex="1019" CssClass="labelForControl" />
                                                <br />
                                                <br />--%>
                                                <asp:Label ID="Label33" runat="server" CssClass="labelForControl" Text="Has any vehicle been involved in 
any accident?" TabIndex="1020" ></asp:Label>
                                                <br />
<%--                                                <asp:RadioButtonList ID="radGenPolicyClaim" AutoPostBack="True" runat="server" CssClass="labelForControl"
                                                    RepeatDirection="Horizontal" TabIndex="1028" >
                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                </asp:RadioButtonList>--%>
                                                <asp:RadioButton ID="radGenPolicyClaimYes" runat="server" AutoPostBack="True" GroupName="RADPOLICYCLAIM" Text="Yes" TabIndex="1021"
                                                    CssClass="labelForControl" />
                                                <asp:RadioButton ID="radGenPolicyClaimNo" runat="server" AutoPostBack="True" Checked="True" GroupName="RADPOLICYCLAIM" Text="No"
                                                    TabIndex="1022" CssClass="labelForControl" />
                                                <br />
                                                <br />
                                                <asp:Label ID="Label29" runat="server" CssClass="labelForControl" Text="Affinity Discount policy?" TabIndex="1020" Visible="False"></asp:Label>
                                                <br />
                                                <asp:RadioButton ID="radAffinityDiscountYes" runat="server" AutoPostBack="True" GroupName="RADAFFINITYDICOUNT" Text="Yes" TabIndex="1023" Visible="False"
                                                    CssClass="labelForControl" OnCheckedChanged="RADAFFINITYDICOUNT_CheckedChanged" onClick="if (!ValidateAffinityDiscountCheck()){ return false; }" />
                                                <asp:RadioButton ID="radAffinityDiscountNo" runat="server" AutoPostBack="True" Checked="True" GroupName="RADAFFINITYDICOUNT" Text="No" TabIndex="1024" Visible="False"
                                                    CssClass="labelForControl" OnCheckedChanged="RADAFFINITYDICOUNT_CheckedChanged" />
                                                <br />
                                                <br />
                                                <%--<asp:Label ID="Label4" runat="server" Text="Is the Insured a New Customer?"
                                                    CssClass="labelForControl" Visible="False"></asp:Label>
                                                <br />
                                                <asp:RadioButton ID="rdbNewCustomerYes" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkNewCustomer" OnCheckedChanged="rdbNewCustomerYes_CheckedChanged"
                                                    Text="Yes" TabIndex="1020" Visible="False" />
                                                <asp:RadioButton ID="rdbNewCustomerNo" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkNewCustomer" OnCheckedChanged="rdbNewCustomerNo_CheckedChanged"
                                                    Text="No" Checked="False" TabIndex="1021" Visible="False" />
                                                <br />
                                                <br />
                                                <asp:Label ID="LblLastName3" runat="server" Text="Any accidents or losses within last 3 years?"
                                                    CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:RadioButton ID="rdbAnyAccidentsYes" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkAnyAccidents" OnCheckedChanged="rdbAnyAccidentsYes_CheckedChanged"
                                                    Text="Yes" TabIndex="1022" />
                                                <asp:RadioButton ID="rdbAnyAccidentsNo" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkAnyAccidents" OnCheckedChanged="rdbAnyAccidentsNo_CheckedChanged"
                                                    Text="No" Checked="True" TabIndex="1023" />
                                                <br />
                                                <br />
                                                <asp:Label ID="lblHowManyYears" runat="server" Text="How many years with no claims?"
                                                    Visible="False" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:RadioButton ID="rdb1Year" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkYears" OnCheckedChanged="rdb1Year_CheckedChanged" Text="1 year"
                                                    Visible="False" TabIndex="1024" />
                                                &nbsp;
                                                <asp:RadioButton ID="rdb2Year" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkYears" OnCheckedChanged="rdb2Year_CheckedChanged" Text="2 years"
                                                    Visible="False" TabIndex="1025" />
                                                <asp:RadioButton ID="rdb3Year" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkYears" OnCheckedChanged="rdb3Year_CheckedChanged" Text="3 years or more"
                                                    Visible="False" TabIndex="1026" />
                                                <br />
                                                <br />--%>
                                                <br />
                                            </div>
                                            <div id="AddressSectionDiv" runat="server">
                                                <div class="col-sm-12" align="center">
                                                    <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                                    <br />
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:Label ID="LblAddress3" runat="server" Font-Bold="True" Text="Mailing Address"
                                                        CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="LblAddress" runat="server" Text="Address" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtAddress" Style="text-transform: uppercase" runat="server" TabIndex="1025"
                                                        CssClass="form-controlWhite" Width="300px"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="LblAddress0" runat="server" Text="Address #2" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtAddress2" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="1026" Width="300px"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label14" runat="server" Text="City" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtCity" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="1027" Width="120px">St. Thomas</asp:TextBox>
                                                    &nbsp;<asp:Label ID="Label55" runat="server" Text="State" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <asp:TextBox ID="TxtState" Style="text-transform: uppercase" runat="server" TabIndex="1028"
                                                        CssClass="form-controlWhite" Width="100px">VI</asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label15" runat="server" Text="ZIP Code" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtZIPCode" Style="text-transform: uppercase" runat="server" TabIndex="1029"
                                                        CssClass="form-controlWhite" Width="300px"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                </div>
                                                <div class="col-sm-1">
                                                <br />
                                                <asp:CheckBox ID="chkSameMailing" runat="server" AutoPostBack="True" Font-Bold="True"
                                                    OnCheckedChanged="chkSameMailing_CheckedChanged" TabIndex="1030" Text="Same as Mailing" />
                                                    <br />
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:Label ID="LblAddress4" runat="server" Font-Bold="True" Text="Physical Address"
                                                        CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="LblAddress1" runat="server" Text="Physical Address" ForeColor="Red"
                                                        CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPhyAddress" Style="text-transform: uppercase" runat="server"
                                                        TabIndex="1031" CssClass="form-controlWhite" Width="300px"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="LblAddress2" runat="server" Text="Physical Address #2" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPhyAddress2" Style="text-transform: uppercase" runat="server"
                                                        TabIndex="1032" CssClass="form-controlWhite" Width="300px"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label85" runat="server" Text="City" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPhyCity" Style="text-transform: uppercase" runat="server" TabIndex="1033"
                                                        CssClass="form-controlWhite" Width="120px">St. Thomas</asp:TextBox>
                                                    <asp:Label ID="Label87" runat="server" Text="State" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <asp:TextBox ID="txtPhyState" Style="text-transform: uppercase" runat="server" TabIndex="1034"
                                                        Width="100px" CssClass="form-controlWhite">VI</asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label86" runat="server" Text="ZIP Code" ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPhyZipCode" Style="text-transform: uppercase" runat="server"
                                                        TabIndex="1035" Width="300px" CssClass="form-controlWhite"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                </div>
                                            </div>
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
                                                Text="POLICY INFORMATION"></asp:Label>
                                                                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-12" align="center">
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div id="DivAgentMiddle" runat="server" class="col-sm-3">
                                                    <asp:Label ID="lblEffDate" runat="server" Text="Effective Date" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtEffBinderDate" runat="server" TabIndex="2003" AutoPostBack="True"
                                                        OnTextChanged="TxtEffBinderDate_TextChanged" Enabled="False" Width="242px" CssClass="form-controlWhite"></asp:TextBox>
                                                    <Toolkit:CalendarExtender ID="ceEffDate" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgEffDate"
                                                        TargetControlID="TxtEffBinderDate" CssClass="Calendar">
                                                    </Toolkit:CalendarExtender>
                                                    <%--<Toolkit:MaskedEditExtender ID="meeEffDate" runat="server" MaskType="Date" Mask="99/99/9999"
                                                        TargetControlID="TxtEffBinderDate" CultureName="en-US">
                                                    </Toolkit:MaskedEditExtender>--%>
                                                    <asp:ImageButton ID="imgEffDate" runat="server" ImageUrl="~/Images2/Calendar.png"
                                                        TabIndex="15" Width="25px" Height="25px" />
                                                    <asp:Label ID="Label40" runat="server" Text="Amount Collected from Customer" Visible="False"
                                                        CssClass="labelForControl"></asp:Label>
                                                    <asp:TextBox ID="TxtCustomerCollection" runat="server" TabIndex="2006" Visible="False"
                                                        CssClass="form-controlWhite" Width="300px"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblExpDate" runat="server" Text="Expiration Date" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtExpBinderDate" runat="server" TabIndex="2004" Enabled="False"
                                                        CssClass="form-controlWhite" Width="242px"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <Toolkit:CalendarExtender ID="ceExpDate" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgExpDate"
                                                        TargetControlID="TxtExpBinderDate">
                                                    </Toolkit:CalendarExtender>
                                                    <Toolkit:MaskedEditExtender ID="meeExpdate" runat="server" MaskType="Date" Mask="99/99/9999"
                                                        TargetControlID="TxtExpBinderDate" CultureName="en-US">
                                                    </Toolkit:MaskedEditExtender>
                                                    <asp:ImageButton ID="imgExpDate" runat="server" ImageUrl="~/Images2/Calendar.png"
                                                        TabIndex="15" Visible="False" Enabled="False" />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label96" runat="server" Text="Discount %" Visible="False" CssClass="labelForControl"></asp:Label>
                                                    <asp:TextBox ID="txtDiscountPct" runat="server" OnTextChanged="txtDiscountPct_TextChanged"
                                                        TabIndex="2007" Width="300px" CssClass="form-controlWhite" AutoPostBack="True"
                                                        Visible="False">0</asp:TextBox>
                                                    <asp:Label ID="Label97" runat="server" Text="%" Visible="False" CssClass="labelForControl"></asp:Label>
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div id="DivAgentMiddle2" runat="server" class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="lblPolicyNumber" runat="server" Text="Policy Number" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPolicyType" runat="server" TabIndex="2000" CssClass="form-controlWhite"
                                                        Width="65px">PPA</asp:TextBox>
                                                    <%--<asp:Label ID="Label99" runat="server" Text="-" CssClass="labelForControl"></asp:Label>--%>
                                                    <asp:TextBox ID="TxtBinderNo" runat="server" TabIndex="2001" CssClass="form-controlWhite"
                                                        Width="100px"></asp:TextBox>
                                                    <%--<asp:Label ID="Label100" runat="server" Text="-" CssClass="labelForControl"></asp:Label>--%>
                                                    <asp:TextBox ID="txtSuffix" runat="server" TabIndex="2002" CssClass="form-controlWhite"
                                                        Width="50px"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblRenewalNumber" runat="server" Text="Renewal of" CssClass="labelForControl" Visible="false"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtRenewalType" runat="server" TabIndex="2000" CssClass="form-controlWhite"
                                                        Width="65px" Visible="false" Enabled="false">PPA</asp:TextBox>
                                                    <%--<asp:Label ID="Label99" runat="server" Text="-" CssClass="labelForControl"></asp:Label>--%>
                                                    <asp:TextBox ID="TxtBinderNoRewnewal" runat="server" TabIndex="2001" CssClass="form-controlWhite"
                                                        Width="100px" Visible="false" Enabled="false"></asp:TextBox>
                                                    <%--<asp:Label ID="Label100" runat="server" Text="-" CssClass="labelForControl"></asp:Label>--%>
                                                    <asp:TextBox ID="TxtSuffixRenewal" runat="server" TabIndex="2002" CssClass="form-controlWhite"
                                                        Width="50px" Visible="false" Enabled="false"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label57" runat="server" Text="Agent" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlAgent" runat="server" Style="text-transform: uppercase"
                                                        TabIndex="2005" CssClass="form-controlWhite" Width="300px" Enabled="False" OnSelectedIndexChanged="ddlAgent_SelectedIndexChanged" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
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
                                            <asp:Label ID="Label2" runat="server" Text="DRIVERS INFORMATION" Font-Bold="true"
                                                ForeColor="White"></asp:Label>
                                                                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                                          <div class="form-group" align="center">
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                DisplayAfter="10">
                                                <ProgressTemplate>
                                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                                    <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span></ProgressTemplate>
                                            </asp:UpdateProgress>
                                             </div>
                                            <div class="col-sm-12" align="center">
                                                <asp:CheckBox ID="chkUseClientInfo" runat="server" AutoPostBack="True" OnCheckedChanged="chkUseClientInfo_CheckedChanged"
                                                        TabIndex="3000" Text=" Same as Client Information" Visible="False" />
                                                    
                                                    <asp:Label ID="lblModifyDriver" runat="server" Font-Bold="True" ForeColor="Red" Text="MODIFY DRIVER: ON"
                                                        Visible="False"></asp:Label> 
                                                           <br />    
                                                           
                                                             <div class="col-sm-1"></div>        
                                                              <div class="col-sm-1"></div>                                                
                                             <div class="col-sm-3">                                                    
                                                    <asp:Label ID="Label50" runat="server" Text="Name" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtDriverName" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite" Width="300px"
                                                        TabIndex="3001"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label110" runat="server" Text="Last Name" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtDriverLastName" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite" Width="300px"
                                                        TabIndex="3002"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label52" runat="server" Text="Date of Birth" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtDriverBirthDate" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite" Width="300px"
                                                        AutoPostBack="True" OnTextChanged="TxtDriverBirthDate_TextChanged" TabIndex="3003"></asp:TextBox>
                                                  <%--  <Toolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureName="en-US" Mask="99/99/9999" 
                                                        MaskType="Date" TargetControlID="TxtDriverBirthDate"> </Toolkit:MaskedEditExtender>--%>
                                                    <asp:ImageButton ID="ImgDriverCalendar" runat="server" ImageUrl="~/Images2/Calendar.png" />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label53" runat="server" Text="Age" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtDriverAge" runat="server" Style="text-align: center" TabIndex="3004" CssClass="form-controlWhite" Width="300px"
                                                        OnTextChanged="TxtDriverAge_TextChanged"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                </div>
                                                 <div class="col-sm-1"></div>        
                                                 <div class="col-sm-3">
                                                    <asp:Label ID="LblDriverGender" runat="server" Text="Gender" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlDriverGender" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite" Width="300px"
                                                        TabIndex="3005">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label54" runat="server" Text="Marital Status" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlDriverMaritalStatus" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite" Width="300px"
                                                        TabIndex="3006">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblDriverGrade" runat="server" Text="Grade Point Avg" Visible="False"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtDriverGrade" runat="server" OnTextChanged="TxtDriverGrade_TextChanged" CssClass="form-controlWhite" Width="300px"
                                                        Style="text-align: center" TabIndex="3007" Visible="False" Wrap="False" AutoPostBack="False"> </asp:TextBox>
                                                    <br />
                                                    <br />
                                                </div>

                                                  <div class="col-sm-12" align="center">
                                                <asp:Button ID="BtnAddDriver" runat="server" OnClick="BtnAddDriver_Click" TabIndex="3009" CssClass="btn btn-primary btn-lg" Width="150px"
                                                    Text="ADD DRIVER" />
                                                &nbsp;&nbsp;<asp:Button ID="BtnCancelDriver" runat="server" OnClick="BtnCancelDriver_Click" Width="150px"
                                                    TabIndex="3010" Text="CANCEL" CssClass="btn btn-primary btn-lg"/>
                                                <asp:TextBox ID="TxtDriverID" runat="server" Visible="False" TabIndex="21" CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                                <br />
                                                <br />
                                                <asp:GridView ID="GridDrivers" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                                                    BorderStyle="Solid" BorderWidth="2px" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridDrivers_RowCommand"
                                                    OnRowDeleting="GridDrivers_RowDeleting" Width="85%" Font-Bold="False">                                                   
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Modify">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:ButtonField>
                                                        <asp:TemplateField HeaderText="Driver #">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="DriverDetailID" HeaderText="Driver #" Visible="False" />
                                                        <asp:BoundField DataField="DriverName" HeaderText="Name" ReadOnly="True" SortExpression="DriverName">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DriverLastName" HeaderText="Last Name" ReadOnly="True"
                                                            SortExpression="DriverLastName">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DriverGender" HeaderText="Gender" ReadOnly="True" SortExpression="DriverGender" />
                                                        <asp:BoundField DataField="DriverMaritalStatus" HeaderText="Marital Status" ReadOnly="True"
                                                            SortExpression="DriverMaritalStatus" />
                                                        <asp:BoundField DataField="DriverAge" HeaderText="Age" ReadOnly="True" SortExpression="DriverAge" />
                                                        <asp:BoundField DataField="DriverDateOfBirth" HeaderText="Date of Birth" ReadOnly="True"
                                                            SortExpression="DriverDateOfBirth" />
                                                        <asp:BoundField DataField="TrafficViolationPoints" HeaderText="Traffic Violation Points"
                                                            Visible="True" />
                                                        <asp:BoundField DataField="TrafficViolationSurcharge" HeaderText="Traffic Surcharge"
                                                            Visible="True" />
                                                        <asp:BoundField DataField="UnderAgeSurcharge" HeaderText="UnderAge Surcharge" Visible="True" />
                                                        <asp:BoundField DataField="SumOfSurcharges" HeaderText="SumOfSurcharges" Visible="True" />
                                                        <asp:BoundField DataField="GradePointAvg" HeaderText="GradePointAvg" Visible="True" />
                                                        <asp:ButtonField ButtonType="Button" CommandName="Delete" HeaderText="Delete">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="TaskControlID" HeaderText="TaskControlID" Visible="False" />
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
                                              </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>
                        <div id="InsuredSectionDiv" runat="server" class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="AccordionInsured" runat="server" AutoSize="None" CssClass="accordion"
                                FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane7" runat="server">
                                        <Header>
                                            <asp:Label ID="Label34" runat="server" Text="ADDITIONAL INSURED INFORMATION" Font-Bold="true"
                                                ForeColor="White"></asp:Label>
                                                                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                                          <div class="form-group" align="center">
                                            <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                DisplayAfter="10">
                                                <ProgressTemplate>
                                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                                    <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span></ProgressTemplate>
                                            </asp:UpdateProgress>
                                             </div>
                                            <div class="col-sm-12" align="center">
                                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="chkUseClientInfo_CheckedChanged"
                                                        TabIndex="3000" Text=" Same as Client Information" Visible="False" />
                                                    
                                                    <asp:Label ID="lblModifyInsured" runat="server" Font-Bold="True" ForeColor="Red" Text="MODIFY INSURED: ON"
                                                        Visible="False"></asp:Label> 
                                                           <br />    
                                                           
                                                             <div class="col-sm-1"></div>        
                                                              <div class="col-sm-1"></div>                                                
                                             <div class="col-sm-3">                                                    
                                                    <asp:Label ID="lblInsuredName" runat="server" Text="Name" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtInsuredName" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite" Width="300px"
                                                        TabIndex="4001"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label37" runat="server" Text="Last Name" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtInsuredLastName" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite" Width="300px"
                                                        TabIndex="4002"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label38" runat="server" Text="Date of Birth" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtInsuredBirthDate" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite" Width="300px"
                                                        AutoPostBack="True" OnTextChanged="TxtInsuredBirthDate_TextChanged" TabIndex="4003"></asp:TextBox>
                                                  <%--  <Toolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" CultureName="en-US" Mask="99/99/9999" 
                                                        MaskType="Date" TargetControlID="TxtInsuredBirthDate"> </Toolkit:MaskedEditExtender>--%>
                                                    <asp:ImageButton ID="ImgInsuredCalendar" runat="server" ImageUrl="~/Images2/Calendar.png" />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label39" runat="server" Text="Age" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtInsuredAge" runat="server" Style="text-align: center" TabIndex="4004" CssClass="form-controlWhite" Width="300px"
                                                        OnTextChanged="TxtInsuredAge_TextChanged"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                </div>
                                                 <div class="col-sm-1"></div>        
                                                 <div class="col-sm-3">
                                                    <asp:Label ID="LblInsuredGender" runat="server" Text="Gender" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlInsuredGender" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite" Width="300px"
                                                        TabIndex="4005">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label42" runat="server" Text="Marital Status" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlInsuredMaritalStatus" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite" Width="300px"
                                                        TabIndex="4006">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblInsuredGrade" runat="server" Text="Grade Point Avg" Visible="False"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtInsuredGrade" runat="server" OnTextChanged="TxtInsuredGrade_TextChanged" CssClass="form-controlWhite" Width="300px"
                                                        Style="text-align: center" TabIndex="4007" Visible="False" Wrap="False" AutoPostBack="False"> </asp:TextBox>
                                                    <br />
                                                    <br />
                                                </div>

                                                  <div class="col-sm-12" align="center">
                                                <asp:Button ID="BtnAddInsured" runat="server" OnClick="BtnAddInsured_Click" TabIndex="4009" CssClass="btn btn-primary btn-lg" Width="150px"
                                                    Text="ADD INSURED" />
                                                &nbsp;&nbsp;<asp:Button ID="BtnCancelInsured" runat="server" OnClick="BtnCancelInsured_Click" Width="150px"
                                                    TabIndex="4010" Text="CANCEL" CssClass="btn btn-primary btn-lg"/>
                                                <asp:TextBox ID="TxtInsuredID" runat="server" Visible="False" TabIndex="21" CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                                <br />
                                                <br />
                                                <asp:GridView ID="GridInsured" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                                                    BorderStyle="Solid" BorderWidth="2px" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridInsured_RowCommand"
                                                    OnRowDeleting="GridInsured_RowDeleting" Width="85%" Font-Bold="False">                                                   
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Modify">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:ButtonField>
                                                        <asp:TemplateField HeaderText="Insured #">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="DriverDetailID" HeaderText="Driver #" Visible="False" />
                                                        <asp:BoundField DataField="DriverName" HeaderText="Name" ReadOnly="True" SortExpression="DriverName">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DriverLastName" HeaderText="Last Name" ReadOnly="True"
                                                            SortExpression="DriverLastName">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DriverGender" HeaderText="Gender" ReadOnly="True" SortExpression="DriverGender" />
                                                        <asp:BoundField DataField="DriverMaritalStatus" HeaderText="Marital Status" ReadOnly="True"
                                                            SortExpression="DriverMaritalStatus" />
                                                        <asp:BoundField DataField="DriverAge" HeaderText="Age" ReadOnly="True" SortExpression="DriverAge" />
                                                        <asp:BoundField DataField="DriverDateOfBirth" HeaderText="Date of Birth" ReadOnly="True"
                                                            SortExpression="DriverDateOfBirth" />
                                                        <asp:BoundField DataField="TrafficViolationPoints" HeaderText="Traffic Violation Points"
                                                            Visible="True" />
                                                        <asp:BoundField DataField="TrafficViolationSurcharge" HeaderText="Traffic Surcharge"
                                                            Visible="True" />
                                                        <asp:BoundField DataField="UnderAgeSurcharge" HeaderText="UnderAge Surcharge" Visible="True" />
                                                        <asp:BoundField DataField="SumOfSurcharges" HeaderText="SumOfSurcharges" Visible="True" />
                                                        <asp:BoundField DataField="GradePointAvg" HeaderText="GradePointAvg" Visible="True" />
                                                        <asp:ButtonField ButtonType="Button" CommandName="Delete" HeaderText="Delete">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="TaskControlID" HeaderText="TaskControlID" Visible="False" />
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
                                                <%--<asp:GridView ID="GridInsured" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                                                    BorderStyle="Solid" BorderWidth="2px" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridInsured_RowCommand"
                                                    OnRowDeleting="GridInsured_RowDeleting" Width="85%" Font-Bold="False">                                                   
                                                    <Columns>
                                                        <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Modify">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:ButtonField>
                                                        <asp:TemplateField HeaderText="Insured #">
                                                            <ItemTemplate>
                                                                <%# Container.DataItemIndex + 1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="AdditionalInsuredDetailID" HeaderText="Insured #" Visible="False" />
                                                        <asp:BoundField DataField="InsuredName" HeaderText="Name" ReadOnly="True" SortExpression="InsuredName">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InsuredLastName" HeaderText="Last Name" ReadOnly="True"
                                                            SortExpression="InsuredLastName">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InsuredGender" HeaderText="Gender" ReadOnly="True" SortExpression="InsuredGender" />
                                                        <asp:BoundField DataField="InsuredMaritalStatus" HeaderText="Marital Status" ReadOnly="True"
                                                            SortExpression="InsuredMaritalStatus" />
                                                        <asp:BoundField DataField="InsuredAge" HeaderText="Age" ReadOnly="True" SortExpression="InsuredAge" />
                                                        <asp:BoundField DataField="InsuredDateOfBirth" HeaderText="Date of Birth" ReadOnly="True"
                                                            SortExpression="DriverDateOfBirth" />
                                                        <asp:BoundField DataField="TrafficViolationPoints" HeaderText="Traffic Violation Points"
                                                            Visible="True" />
                                                        <asp:BoundField DataField="TrafficViolationSurcharge" HeaderText="Traffic Surcharge"
                                                            Visible="True" />
                                                        <asp:BoundField DataField="UnderAgeSurcharge" HeaderText="UnderAge Surcharge" Visible="True" />
                                                        <asp:BoundField DataField="SumOfSurcharges" HeaderText="SumOfSurcharges" Visible="True" />
                                                        <asp:BoundField DataField="GradePointAvg" HeaderText="GradePointAvg" Visible="True" />
                                                        <asp:ButtonField ButtonType="Button" CommandName="Delete" HeaderText="Delete">
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="TaskControlID" HeaderText="TaskControlID" Visible="False" />
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
                                                </asp:GridView>--%>
                                            </div>
                                              </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>
                        <div id="VehiclesSectionDiv" runat="server" class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="AccordionVehicle" runat="Server" AutoSize="None" CssClass="accordion"
                                FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane6" runat="server">
                                        <Header>
                                            <asp:Label ID="Label76" runat="server" Font-Bold="true" ForeColor="White" Text="VEHICLE INFORMATION"></asp:Label>
                                                                                        <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-12" align="center">
                                                <asp:Label ID="lblModifyVehicle" runat="server" ForeColor="Red" Font-Bold="true"
                                                    CssClass="labelForControl" Text="MODIFY MODE: ON" Visible="False"></asp:Label>
                                                <br />
                                                <div class="col-sm-3">
                                                   <asp:Label ID="Label75" runat="server" ForeColor="Red" Text="Is this a Motorcycle / Scooter?"></asp:Label>
                                                <asp:RadioButtonList ID="radMotorcycle" runat="server" AutoPostBack="True" Width="200px"
                                                    OnSelectedIndexChanged="radMotorcycle_SelectedIndexChanged" CssClass="labelForControl"
                                                    RepeatDirection="Horizontal" TabIndex="7000" Enabled="False">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">
                                                <asp:Label ID="Label118" runat="server" ForeColor="Red" Text="Is this vehicle:"></asp:Label>
                                                <asp:RadioButtonList ID="radNewUse" runat="server" AutoPostBack="True" Width="200px"
                                                    OnSelectedIndexChanged="radNewUse_SelectedIndexChanged" CssClass="labelForControl"
                                                    RepeatDirection="Horizontal" TabIndex="7000" Enabled="False">
                                                    <asp:ListItem Value="Y">New</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="N">Used</asp:ListItem>
                                                </asp:RadioButtonList>
                                                </div>
                                                <asp:Label ID="Label119" runat="server" ForeColor="Red" Text="Is this a repaired Salvage Vehicle:"></asp:Label>
                                                <asp:RadioButtonList ID="radIsSalvage" runat="server" AutoPostBack="True" Width="200px"
                                                    OnSelectedIndexChanged="radIsSalvage_SelectedIndexChanged" CssClass="labelForControl"
                                                    RepeatDirection="Horizontal" TabIndex="7000" Enabled="False">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
												 <div align="center">                                                
                                                      <asp:UpdateProgress ID="UpdateProgress9" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                        DisplayAfter="10">
                                                        <ProgressTemplate>
                                                            <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                                            <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span> </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="Label16" runat="server" ForeColor="Red" Text="Vehicle Year" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlVehicleYear" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                        OnSelectedIndexChanged="ddlVehicleYear_SelectedIndexChanged" TabIndex="7001"
                                                        OnTextChanged="ddlVehicleYear_TextChanged">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label17" runat="server" Text="Vehicle Make" CssClass="labelForControl"
                                                        ForeColor="Red"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlVehicleMake" runat="server" TabIndex="7002" AutoPostBack="True"
                                                        CssClass="form-controlWhite" OnSelectedIndexChanged="ddlVehicleMake_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label18" CssClass="labelForControl" runat="server" ForeColor="Red"
                                                        Text="Vehicle Model"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlVehicleModel" runat="server" TabIndex="7003" AutoPostBack="True"
                                                        CssClass="form-controlWhite" OnSelectedIndexChanged="ddlVehicleModel_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label24" CssClass="labelForControl" runat="server" Text="Island" ForeColor="Red"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlIsland" runat="server" Style="text-transform: uppercase"
                                                        AutoPostBack="True" CssClass="form-controlWhite" OnSelectedIndexChanged="ddlIsland_SelectedIndexChanged"
                                                        TabIndex="7004">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label22" runat="server" ForeColor="Red" CssClass="labelForControl"
                                                        Text="Vehicle Use"></asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlVehicleUse" runat="server" Style="text-transform: uppercase"
                                                        AutoPostBack="True" CssClass="form-controlWhite" OnSelectedIndexChanged="ddlVehicleUse_SelectedIndexChanged"
                                                        TabIndex="7005">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="Label21" runat="server" Text="Insured Value" CssClass="labelForControl"
                                                        ForeColor="Black"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtVehicleValue" runat="server" TabIndex="7007" AutoPostBack="True"
                                                        CssClass="form-controlWhite" OnTextChanged="TxtVehicleValue_TextChanged"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label19" runat="server" CssClass="labelForControl" Text="VIN #" ForeColor="Red"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtVINNo" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite" AutoPostBack="True"
                                                        TabIndex="7008" MaxLength="17" OnTextChanged="TxtVINNo_TextChanged1"></asp:TextBox>
													<asp:Label ID="lblVINResult"  runat="server" Text="" CssClass="labelForControl"
                                                        ForeColor="Red"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label20" runat="server" Text="License Plate#" CssClass="labelForControl" ForeColor="Red"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtLicensePlateNo" runat="server" Style="text-transform: uppercase"
                                                        CssClass="form-controlWhite" TabIndex="7009"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label101" CssClass="labelForControl" runat="server" Text="Loss Payee" ForeColor="Red"></asp:Label>
                                                    <br />
                                                    <asp:Button ID="btnBankList" runat="server" Text="SELECT" CssClass="btn btn-primary btn-lg" OnClick="btnBankList_Click"></asp:Button>
                                                    <br />
                                                    <asp:Label ID="lblBankListSelected2" runat="server"></asp:Label>
                                                    <%--<asp:DropDownList ID="ddlBankList" runat="server" TabIndex="7010" CssClass="form-controlWhite"
                                                        OnSelectedIndexChanged="ddlBankList_SelectedIndexChanged">
                                                    </asp:DropDownList>--%>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label23" runat="server" CssClass="labelForControl" Text="Vehicle Type"
                                                        Visible="False"></asp:Label>
                                                    <br />
													 <%-- NUevo Cambio para Over2Ton Victor 5/10/2020--%>
                                                     <asp:DropDownList ID="ddlOver2Ton" runat="server" Style="text-transform: uppercase"
                                                        AutoPostBack="True" CssClass="form-controlWhite" OnSelectedIndexChanged="ddlOver2Ton_SelectedIndexChanged"
                                                        TabIndex="7005">
                                                    </asp:DropDownList>
                                                    <asp:RadioButtonList ID="radGenTons" runat="server" AutoPostBack="True" OnSelectedIndexChanged="radGenTons_SelectedIndexChanged"
                                                        CssClass="labelForControl" RepeatDirection="Horizontal" TabIndex="7011" Visible="False">
                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblHeavyTruck" runat="server" CssClass="labelForControl" Text="Is this a Dump Truck, Water Truck, Semi/Tow Truck or Food Van?"
                                                        Visible="False"></asp:Label>
                                                    <br />
                                                    <asp:RadioButtonList ID="radHeavyTruck" runat="server"
                                                        CssClass="labelForControl" RepeatDirection="Horizontal" TabIndex="7011" Visible="False">
                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <br />
                                                    <asp:Label ID="LblPassengersNo" runat="server" ForeColor="Red" CssClass="labelForControl"
                                                        Text="Number of Passengers (If it's a Taxi)" Visible="False"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtPassengerNo" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                        OnTextChanged="TxtPassengerNo_TextChanged" TabIndex="7006" Visible="False" Width="50px">0</asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label47" runat="server" CssClass="labelForControl" Text="BI Limit (per ocurrence)"
                                                        Visible="False"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <asp:TextBox ID="TxtBILimitpo" runat="server" TabIndex="57" CssClass="form-controlWhite"
                                                        Visible="False"></asp:TextBox>
                                                    <asp:TextBox ID="TxtVehicleID" runat="server" TabIndex="58" Visible="False" CssClass="form-controlWhite"></asp:TextBox>
                                                </div>
                                            </div>
                                            
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>
                        <div id="LimitsSectionDiv" runat="server" class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="AccordionLimitDed" runat="Server" AutoSize="None" CssClass="accordion"
                                FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane4" runat="server">
                                        <Header>
                                            <asp:Label ID="Label77" runat="server" Font-Bold="true" ForeColor="White" Text="LIMITS &amp;amp; DEDUCTIBLES"></asp:Label>
                                                                                        <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-12" align="center">
                                                <asp:Label ID="Label46" runat="server" Text="BI Limit" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:DropDownList ID="TxtBILimitpp" runat="server" TabIndex="5000" AutoPostBack="True"
                                                    CssClass="form-controlWhite" Width="300px" OnSelectedIndexChanged="TxtBILimitpp_SelectedIndexChanged">
                                                    <asp:ListItem Value="$10,000/$20,000">$10,000/$20,000</asp:ListItem>
                                                    <asp:ListItem Value="$10,000/$25,000">$10,000/$25,000</asp:ListItem>
                                                    <asp:ListItem Value="$10,000/$50,000">$10,000/$50,000</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label48" runat="server" Text="PD Limit" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtPDLimit" runat="server" TabIndex="5001" Enabled="False" Width="300px"
                                                    CssClass="form-controlWhite">$10,000</asp:TextBox>&nbsp&nbsp
                                                <br />
                                                <br />
                                                <asp:Label ID="Label84" runat="server" Text="Comp &amp; Coll Deductible" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlCompCollDed" runat="server" AutoPostBack="True" Width="300px"
                                                    CssClass="form-controlWhite" TabIndex="5002" OnSelectedIndexChanged="ddlCompCollDed_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label61" runat="server" Text="MP Limit" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:RadioButtonList ID="radGenMedicalPayment" runat="server" CssClass="labelForControl"
                                                    AutoPostBack="True" Onselectedindexchanged="radGenMedicalPayment_SelectedIndexChanged"
                                                    RepeatDirection="Horizontal" TabIndex="5003">
                                                    <asp:ListItem Selected="True" Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:TextBox ID="txtMPLimit" runat="server" Enabled="False" ReadOnly="True" Width="300px"
                                                    TabIndex="5004" CssClass="form-controlWhite">$1,000/$5,000</asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label94" runat="server" Text="Other Surcharge" Visible="False" CssClass="labelForControl"></asp:Label>
                                                <asp:TextBox ID="txtOtherSurcharge" runat="server" OnTextChanged="txtOtherSurcharge_TextChanged"
                                                    TabIndex="6003" Width="300px" Visible="False">0</asp:TextBox>
                                                <asp:Label ID="Label95" runat="server" Text="%" Visible="False" CssClass="labelForControl"></asp:Label>
                                            </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>
                        <div id="AddCoveragesSectionDiv" runat="server" class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="AccordionAddCoverage" runat="Server" AutoSize="None" CssClass="accordion"
                                FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane5" runat="server">
                                        <Header>
                                            <asp:Label ID="Label102" runat="server" Font-Bold="true" ForeColor="White" Text="ADDITIONAL COVERAGES"></asp:Label>
                                                                                        <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-12" align="center">
                                                <asp:Label ID="Label28" runat="server" CssClass="labelForControl" Text="Accidental Death &amp; Dismemberment"></asp:Label>
                                                <br />
                                                <asp:RadioButtonList ID="radADD" runat="server" RepeatDirection="Horizontal" TabIndex="6000"
                                                    OnSelectedIndexChanged="radADD_SelectedIndexChanged" CssClass="labelForControl"
                                                    AutoPostBack="True">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:Label ID="lblRentalReim" runat="server" Text="Rental Reimbursement" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:RadioButtonList ID="radRentalReimCov" runat="server" RepeatDirection="Horizontal"
                                                    TabIndex="6002" CssClass="labelForControl" OnSelectedIndexChanged="radRentalReimCov_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:Label ID="Label27" runat="server" CssClass="labelForControl" Text="Uninsured Motorists"></asp:Label>
                                                <br />
                                                <asp:RadioButtonList ID="radGenMotoristCoverage" runat="server" RepeatDirection="Horizontal"
                                                    CssClass="labelForControl" TabIndex="6001" OnSelectedIndexChanged="radGenMotoristCoverage_SelectedIndexChanged"
                                                    AutoPostBack="True">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                                <br />
                                                <asp:Label ID="lblTaxiDriverloss" runat="server" CssClass="labelForControl" Text="Taxi Drivers Loss of Income"
                                                    Visible="False"></asp:Label>
                                                <asp:RadioButtonList ID="radTaxiDriverloss" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                    TabIndex="6002" Visible="False" CssClass="labelForControl" OnSelectedIndexChanged="radTaxiDriverloss_SelectedIndexChanged">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
												 <asp:Label ID="lblVipa" runat="server" CssClass="labelForControl" Text="V.I. PORTS AUTHORITY (VIPA)"
                                                    Visible="False"></asp:Label>
                                                 <asp:RadioButtonList ID="radVIPA" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                                    TabIndex="6002" Visible="False" CssClass="labelForControl" OnSelectedIndexChanged="radVIPA_SelectedIndexChanged">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>
                        <%--<asp:PlaceHolder ID="phPremiumFinanceDiv" runat="server"></asp:PlaceHolder>--%>

                        <div class="col-sm-12" align="center">
                        <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                        <br />
                        <br />
                        <asp:GridView ID="GridVehicle" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2px" CellPadding="4" ForeColor="#333333" GridLines="None"
                            OnRowCommand="GridVehicle_RowCommand" Font-Bold="False" OnRowDeleting="GridVehicle_RowDeleting"
                            Width="85%" HeaderStyle-HorizontalAlign="Center" Font-Size="10px">
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="Modify">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="VehicleYear" HeaderText="Vehicle Year" ReadOnly="True"
                                    SortExpression="VehicleYear" />
                                <asp:BoundField DataField="VehicleMake" HeaderText="Vehicle Make" ReadOnly="True"
                                    SortExpression="VehicleMake" />
                                <asp:BoundField DataField="VehicleModel" HeaderText="Vehicle Model" ReadOnly="True"
                                    SortExpression="VehicleModel" />
                                <asp:BoundField DataField="VIN" HeaderText="VIN" ReadOnly="True" SortExpression="VIN" />
                                <asp:BoundField DataField="LicensePlateNo" HeaderText="License Plate #" ReadOnly="True"
                                    SortExpression="LicensePlateNo" />
                                <asp:BoundField DataField="VehicleDetailID" HeaderText="VehicleDetailID" Visible="False" />
                                <asp:BoundField DataField="TaskControlID" HeaderText="TaskControlID" Visible="False" />
                                <asp:BoundField DataField="Island" HeaderText="Island" Visible="False" />
                                <asp:BoundField DataField="Status" HeaderText="Status" Visible="False" />
                                <asp:BoundField DataField="VehicleValue" HeaderText="VehicleValue" Visible="False" />
                                <asp:BoundField DataField="VehicleUse" HeaderText="VehicleUse" Visible="False" />
                                <asp:BoundField DataField="PassengersNo" HeaderText="PassengersNo" Visible="False" />
                                <asp:BoundField DataField="Over2Tons" HeaderText="Over2Tons" Visible="False" />
                                <asp:BoundField DataField="MedicalPayment" HeaderText="MedicalPayment" Visible="False" />
                                <asp:BoundField DataField="MotoristCoverage" HeaderText="MotoristCoverage" Visible="False" />
                                <asp:BoundField DataField="DeathDismembermentCoverage" HeaderText="DeathDismembermentCoverage"
                                    Visible="False" />
                                <asp:BoundField DataField="ViolationPoints" HeaderText="ViolationPoints" Visible="False" />
                                <asp:BoundField DataField="ViolationSurcharge" HeaderText="ViolationSurcharge" Visible="False" />
                                <asp:BoundField DataField="UnderAgeSurcharge" HeaderText="UnderAgeSurcharge" Visible="False" />
                                <asp:BoundField DataField="PDLimit" HeaderText="PDLimit" Visible="False" />
                                <asp:BoundField DataField="ComprehensiveDedu" HeaderText="ComprehensiveDedu" Visible="False" />
                                <asp:BoundField DataField="CollisionDedu" HeaderText="CollisionDedu" Visible="False" />
                                <asp:BoundField DataField="BIPPLimit" HeaderText="BIPPLimit" Visible="False" />
                                <asp:BoundField DataField="BIPOLimit" HeaderText="BIPOLimit" Visible="False" />
                                <asp:BoundField DataField="MPLimit" HeaderText="MPLimit" Visible="False" />
                                <asp:BoundField DataField="CompPremium" HeaderText="Comprehensive" 
                                    Visible="True" DataFormatString="{0:c}" />
                                <asp:BoundField DataField="CollPremium" HeaderText="Collision" Visible="True" 
                                    DataFormatString="{0:c}" />
                                <asp:BoundField DataField="BIPremium" HeaderText="BI" Visible="True" 
                                    DataFormatString="{0:c}" />
                                <asp:BoundField DataField="PDPremium" HeaderText="PD" Visible="True" 
                                    DataFormatString="{0:c}" />
                                <asp:BoundField DataField="MPPremium" HeaderText="MP" Visible="True" 
                                    DataFormatString="{0:c}" />
                                <asp:BoundField DataField="MotoristPremium" HeaderText="Motorist" 
                                    Visible="True" DataFormatString="{0:c}" />
                                <asp:BoundField DataField="ADDPremium" HeaderText="AD-D" Visible="True" 
                                    DataFormatString="{0:c}" />
                                <asp:BoundField DataField="RentalReimCoverage" HeaderText="RentalReimCov" Visible="False" />
                                <asp:BoundField DataField="RentalReim" HeaderText="Rental Reim." Visible="True" 
                                    DataFormatString="{0:c}" />
                                <asp:BoundField DataField="ViolationSurcharge" HeaderText="Traffic Surcharge" Visible="True" />
                                <asp:BoundField DataField="UnderAgeSurcharge" HeaderText="UnderAge Surcharge" Visible="True" />
                                <asp:BoundField DataField="OtherSurcharge" HeaderText="Other Surcharge" Visible="True" />
                                <asp:BoundField DataField="OtherSurchargePct" HeaderText="Other SurchargePct" Visible="False" />
                                <asp:BoundField DataField="TotalPremium" HeaderText="Total Premium" 
                                    Visible="True" DataFormatString="{0:c}" />
                                <asp:BoundField DataField="BankDesc" HeaderText="BankDesc" Visible="False" />
                                <asp:BoundField DataField="IsMotorcycleScooter" HeaderText="IsMotorcycleScooter"
                                    Visible="False" />
                                <asp:BoundField DataField="TaxiLossAmount" HeaderText="TaxiLossAmount" 
                                    DataFormatString="{0:c}" />
                                <asp:ButtonField ButtonType="Button" CommandName="Delete" HeaderText="Delete">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:ButtonField>
                                <asp:BoundField DataField="VehicleRowNumber" HeaderText="VehicleRowNumber" />
                                <asp:BoundField DataField="IsHeavyTruck" HeaderText="IsHeavyTruck" Visible="False" />
                                <asp:BoundField DataField="NewUse" HeaderText="NewUse" Visible="False" />
                                <asp:BoundField DataField="IsSalvage" HeaderText="IsSalvage" Visible="False" />
								<asp:BoundField DataField="NewOver2Ton" HeaderText="NewOver2Ton" Visible="False" />
								<asp:BoundField DataField="VIPA" HeaderText="VIPA" DataFormatString="{0:c}" />
                            </Columns>
                            <AlternatingRowStyle HorizontalAlign="Center" BackColor="#EBEBEB" />
                            <EditRowStyle BackColor="" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="false" ForeColor="White" />
                            <HeaderStyle BackColor="Gray" Font-Bold="False" ForeColor="White" Height="30px" HorizontalAlign="Center" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                                <SelectedRowStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                        <br />
                        <asp:Button ID="BtnAddVehicle" runat="server" OnClick="BtnAddVehicle_Click" CssClass="btn btn-primary btn-lg"
                            Width="175px" TabIndex="8012" Text="ADD VEHICLE" />
                        &nbsp;&nbsp;
                        <asp:Button ID="BtnCancelVehicle" runat="server" CssClass="btn btn-primary btn-lg"
                            Width="175px" OnClick="BtnCancelVehicle_Click" TabIndex="8013" Text="CANCEL" />
                        <br />
                        <br />
                        <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                            DisplayAfter="10">
                            <ProgressTemplate>
                                <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span> </ProgressTemplate>
                        </asp:UpdateProgress>

                        <asp:Label ID="lblGrossTax" CssClass="labelForControl" runat="server" Font-Bold="True"
                            Text="Gross Tax"></asp:Label>
                        <br />
                        <asp:Label ID="lblGrossTaxCurrency" runat="server" CssClass="labelForControl" Text="$"></asp:Label>
                        <asp:TextBox ID="txtGrossTax" runat="server" Font-Bold="True"
                            CssClass="form-controlWhite" Width="300px" TabIndex="45" Enabled="False" 
                            ReadOnly="True">0.00</asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblTotalPremium" CssClass="labelForControl" runat="server" Font-Bold="True"
                            Text="Total Premium"></asp:Label>
                        <br />
                        <asp:Label ID="Label103" runat="server" CssClass="labelForControl" Text="$"></asp:Label>
                        <asp:TextBox ID="txtTotalQuote" runat="server" Enabled="False" Font-Bold="True" ReadOnly="True"
                            CssClass="form-controlWhite" Width="300px" TabIndex="45">0.00</asp:TextBox>
                        <br />
                        <br />
                        <asp:Label ID="lblEffDtEndorsementPrimary" runat="server" CssClass="LabelNormaSize" 
                                    Visible="False" ForeColor="Red" >Endor. Date</asp:Label>
                        <br />
                        <asp:TextBox ID="txtEffDtEndorsementPrimary" runat="server" CssClass="form-controlWhite" Visible="False" Width="300px" />
                        <div class="" style="padding: 0px;">
                                            </div>
                        <div class="col-sm-12" align="center" >
                                            <br />
                                            <%--PREMIUM FINANCE--%>
                                            <div style="margin-bottom:40px; display:none;">
                                            <!--toggle sidebar button-->
                                            <p class="visible-xs">
                                                <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas">
                                                    <i class="glyphicon glyphicon-chevron-left"></i>
                                                </button>
                                            </p>
                                            <div class="form-group" align="center">
                                                <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                    DisplayAfter="10">
                                                    <ProgressTemplate>
                                                        <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                                        <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span> </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
                                            <div class="row formWraper" style="padding: 0px; margin:0px; display:none">
                                                <Toolkit:Accordion ID="MyAccordion" runat="Server" AutoSize="None" CssClass="accordion" HeaderSelectedCssClass=""
                                                    FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                                    RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                                    <Panes>
                                                        <Toolkit:AccordionPane ID="AccordionPane8" runat="server">
                                                            <Header>
                                                                PREMIUM FINANCE INFORMATION
                                                                <div class="arrow down">
                                                                </div>
                                                            </Header>
                                                            <Content>
                                                                <div class="form=group" align="center">
                                                                    <asp:Button ID="btnCalculatPrem" runat="server" CssClass="btn btn-primary btn-lg" 
                                                                        TabIndex="76" Text="CALCULATE" Style="margin-left: 10px;" OnClick="btnCalculatePrem_Click"  Width="200px" Visible="False" />
                                                                    &nbsp;

                                                                    <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-lg" 
                                                                        TabIndex="76" Text="RESET" Style="margin-left: 10px;" OnClick="btnReset_Click"  Width="200px" Visible="False" />
                                                                    &nbsp;
                    <%--                                                <br />
                                                                    <br />--%>
                                                                    <div align="left">
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                </div>
                                                                <div class="col-sm-1">
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <%--<br />--%>
                                                                    <asp:Label ID="lblPolicyNo" runat="server" CssClass="labelForControl" Visible="False">Policy No:</asp:Label>
                                                                    <%--<br />--%>
                                                                    <asp:TextBox ID="txtPolicyNo" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                                        MaxLength="20" TabIndex="1006" Width="200px" Enabled="false" Visible="False"></asp:TextBox>
                    <%--                                                <br />
                                                                    <br />--%>
                                                                    <asp:Label ID="lblName" runat="server" CssClass="labelForControl" Visible="False">Name:</asp:Label>
                                                                    <%--<br />--%>
                                                                    <asp:TextBox ID="txtName" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                                        MaxLength="20" TabIndex="1007" Width="200px"  Enabled="false" Visible="False"></asp:TextBox>
                    <%--                                                <br />
                                                                    <br />--%>
                                                                    <asp:Label ID="lblEffectiveDate" runat="server" CssClass="labelForControl" Visible="False">Effective Date</asp:Label>
                                                                    <%--<br />--%>
                                                                    <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                                        MaxLength="20" TabIndex="1008"  Enabled="false" Width="200px" Visible="False"></asp:TextBox>
                                                                    <%--<br />
                                                                    <br />--%>
                                                                    <asp:Label ID="lblExpirationDate" runat="server" CssClass="labelForControl" Visible="False">Expiration Date</asp:Label>
                                                                    <%--<br />--%>
                                                                    <asp:TextBox ID="txtExpirationDate" runat="server" CssClass="form-controlWhite" MaxLength="100"
                                                                        Style="text-transform: uppercase" TabIndex="1009" Width="200px" Enabled="false" Visible="False"></asp:TextBox>
                                                                    <%--<br />
                                                                    <br />--%>
                                                                </div>
                                                                <div class="col-sm-1">
                                                                </div>
                                                                <div class="col-sm-2">
                                                                    <%--<br />--%>
                                                                    <asp:Label ID="Label80" runat="server" CssClass="labelForControl" Visible="False">Total Premium</asp:Label>
                                                                    <%--<br />--%>
                                                                    <asp:TextBox ID="txtTotalPremium" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                                        Style="text-transform: uppercase" TabIndex="1013" OnTextChanged="txtTotalPremium_TextChanged" AutoPostBack="True" Visible="False"></asp:TextBox>
                                                                    <%--<br />
                                                                    <br />--%>
                                                                    <asp:Label ID="lblDownPayment" runat="server" CssClass="labelForControl" Visible="False">Down Payment</asp:Label>
                                                                    <%--<br />--%>
                                                                    <asp:TextBox ID="txtDownPayment" Style="text-transform: uppercase" 
                                                                  runat="server" CssClass="form-controlWhite"
                                                                        MaxLength="30" TabIndex="1012" Enabled="true" 
                                                                  ontextchanged="txtDownPayment_TextChanged" AutoPostBack="True" Visible="False"></asp:TextBox>
                                                                    <%--<br />
                                                                    <br />--%>
                                                                    <asp:Label ID="lblUnpaidBalance" runat="server" CssClass="labelForControl" Visible="False">Unpaid Balance</asp:Label>
                                                                    <%--<br />--%>
                                                                    <asp:TextBox ID="txtUnpaidBalance" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                                        Style="text-transform: uppercase" TabIndex="1013"  Enabled="false" Visible="False"></asp:TextBox>
                                                                    <%--<br />
                                                                    <br />--%>
                                                                    <asp:Label ID="lblTerms" runat="server" CssClass="labelForControl" Visible="False">Terms (Months)</asp:Label>
                                                                    <%--<br />--%>
                                                                    <asp:DropDownList ID="ddlTerms" TabIndex="18" runat="server" Width="210px" CssClass="btn btn-primary btn-lg" Height="46px" Visible="False">
                                                                    <asp:ListItem>2</asp:ListItem>
                                                                    <asp:ListItem>3</asp:ListItem>
                                                                    <asp:ListItem>4</asp:ListItem>
                                                                    <asp:ListItem>5</asp:ListItem>
                                                                    <asp:ListItem>6</asp:ListItem>
                                                                    <asp:ListItem>7</asp:ListItem>
                                                                    <asp:ListItem>8</asp:ListItem>
                                                                    <asp:ListItem>9</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <%--<br />
                                                                    <br />--%>
                                                                </div>
                                                            <table width ="100%">
                                                                <tr>
                                                                    <td align="center">
                                                                        <%--<img src="images/searchGrid.png" alt="Search">--%>
                                                                        <asp:GridView ID="gvPremium" runat="server"  class="table table-bordered" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                                                        GridLines="None" DataKeyNames="PaymentDate,PaymentAmount,Rate,Payment" onrowcommand="gvPremium_RowCommand">  
                                                                            <Columns>
                                                                            <asp:BoundField DataField="PaymentDate" dataformatstring="{0:MM/dd/yyyy}" HeaderText="Date Of Payment" Visible="False" />
                                                                            <asp:TemplateField HeaderText="Installments">
                                                                            <ItemTemplate>
                                                                            <%# Container.DataItemIndex == 0 ? 3 : Container.DataItemIndex == 1 ? 6 : 9 %>
                                                                            </ItemTemplate>
                                                                            <ItemStyle/>
                                                                            </asp:TemplateField>
                                                                                <asp:BoundField DataField="DownPayment" HeaderText="Down Payment" />
                                                                                <asp:BoundField DataField="PaymentAmount" HeaderText="Monthly Payment" />
                                                                            <asp:BoundField DataField="TotalPayment" HeaderText="Total Premium" Visible="False" />
                                                                            </Columns>
                                                                            <EditRowStyle BackColor="#2461BF" />
                                                                            <FooterStyle BackColor="#428BCA" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="#428BCA" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EFF3FB" />
                                                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                                         </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />	
                                                            <br />	
                                                            <div align="center">
                                                                <img alt=""  src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                                            </div>
                                                             <br />	
                                                            <div align="center">
                                                               <div class="col-sm-4">
                                                                    <asp:Label ID="Label82" runat="server" CssClass="labelForControl" 
                                                                        Visible="False">Amount</asp:Label><br />
                                                                    <asp:TextBox ID="txtUnpaid" runat="server" CssClass="form-controlWhite" MaxLength="30" Width="200"
                                                                        Style="text-transform: uppercase" TabIndex="1013" 
                                                                            OnTextChanged="txtTotalPremium_TextChanged" AutoPostBack="True" Visible="False"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:Label ID="Label92" runat="server" CssClass="labelForControl" 
                                                                        Visible="False">Charge</asp:Label><br />
                                                                    <asp:TextBox ID="txtCharge" runat="server" AutoPostBack="True" 
                                                                        CssClass="form-controlWhite" MaxLength="30" 
                                                                        OnTextChanged="txtTotalPremium_TextChanged" Style="text-transform: uppercase" 
                                                                        TabIndex="1013" Width="200" Visible="False"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <asp:Label ID="Label99" runat="server" CssClass="labelForControl" 
                                                                        Visible="False">Total Payment</asp:Label>  <br />
                                                                                <asp:TextBox ID="txtAmount" runat="server" AutoPostBack="True" 
                                                                        CssClass="form-controlWhite" MaxLength="30" 
                                                                        OnTextChanged="txtTotalPremium_TextChanged" Style="text-transform: uppercase" 
                                                                        TabIndex="1013" Width="200" Visible="False"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div>
                                                            </div>
                                                            </Content>
                                                        </Toolkit:AccordionPane>
                                                    </Panes>
                                                </Toolkit:Accordion>
                                           </div>
                                           </div>
                                            <%--END PREMIUM FINANCE--%>

                                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                                <br />
                                                <br />
                                                <cc1:Mirror id="Mirror3" ControlID="btnEndor" runat="server" />
                                                <cc1:Mirror id="Mirror1" ControlID="btnAdjuntar" runat="server" />
                                                <asp:Button ID="BtnEdit2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnEdit_Click" TabIndex="9000" Text="MODIFY" Width="230PX" />
                                                <asp:Button ID="btnSaveApp2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnSaveApp_Click" TabIndex="9000" Text="SAVE APPLICATION" Visible="False" Width="220px" />
                                                <asp:Button ID="btnAcceptQuote2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnAcceptQuote_Click" TabIndex="9000" Text="ACCEPT QUOTE" Visible="False" Width="220px" />
                                                <asp:Button ID="btnSavePolicy2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnSavePolicy_Click" TabIndex="9000" Text="SAVE POLICY" Visible="False" Width="220px" />
                                                <asp:Button ID="btnIssuePolicy2" runat="server" OnClick="btnIssuePolicy_Click" OnClientClick="if (!Confirm()){ return false; } this.disabled = true; this.value = 'Saving...';" 
                                                UseSubmitBehavior="false" TabIndex="9000"
                                                Text="ISSUE POLICY" CssClass="btn btn-primary btn-lg" Width="220px" />
                                                <asp:Button ID="btnQuote2" runat="server" OnClick="btnQuote_Click" TabIndex="9000"
                                                 Text="SAVE QUOTE" CssClass="btn btn-primary btn-lg" Width="220px" />
                                                <asp:Button ID="btnReinstallation2" runat="server" 
                                                    CssClass="btn btn-primary btn-lg" OnClick="btnReinstallation_Click" 
                                                    Style="margin-left: 10px;" TabIndex="73" Text="REINSTALLATION" Visible="False" 
                                                    Width="200px" />
                                                <asp:Button ID="btnCancellation2" runat="server" 
                                                    CssClass="btn btn-primary btn-lg" OnClick="btnCancellation_Click" 
                                                    Style="margin-left: 10px;" TabIndex="72" Text="CANCELLATION" Visible="False" 
                                                    Width="200px" />
                                                <asp:Button ID="BtnPremiumFinance2" runat="server" 
                                                    CssClass="btn btn-primary btn-lg" OnClick="BtnPremiumFinance_Click" 
                                                    TabIndex="70" Text="PREMIUM FINANCE" Visible="False" Width="220px" />
                                                <asp:Button ID="BtnRenew2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnRenew_Click" TabIndex="70" Text="RENEW" Visible="False" Width="220px" />
<%--                                                <asp:DropDownList ID="ddlPrintOption2" runat="server" AutoPostBack="True" 
                                                    CssClass="btn btn-primary btn-lg" 
                                                    onselectedindexchanged="ddlPrintOption2_SelectedIndexChanged" TabIndex="18" 
                                                    Visible="False" Width="270px">
                                                    <asp:ListItem>PRINT FULL POLICY</asp:ListItem>
                                                    <asp:ListItem>DECLARATION PAGE</asp:ListItem>
                                                </asp:DropDownList>--%>
                                                <asp:Button ID="btnSendPolicy2" runat="server" OnClick="btnSendPolicy_Click" TabIndex="9000"
                                                Text="SEND POLICY EMAIL" Visible="False" CssClass="btn btn-primary btn-lg" 
                                                Width="220px" />
                                                <asp:Button ID="BtnPrintPolicy2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPrint_Click" TabIndex="9000" 
                                                Text="PRINT" Visible="False" Width="220px" />
                                                <cc1:Mirror id="Mirror4" ControlID="btnPrintIDCards" runat="server" />
                                                <asp:Button ID="ddlPrintOption2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPrint_Click" TabIndex="9000" 
                                                Text="PRINT DEC PAGE" Visible="False" Width="220px" />
                                                <asp:Button ID="BtnRePrintApp2" runat="server" 
                                                    CssClass="btn btn-primary btn-lg" OnClick="BtnRePrintApp_Click" TabIndex="9000" 
                                                    Text="REPRINT APPLICATION" Visible="False" Width="250px" />
                                                <asp:Button ID="BtnPrintInvoice2" runat="server" 
                                                    CssClass="btn btn-primary btn-lg" OnClick="BtnPrintInvoice_Click" 
                                                    TabIndex="9000" Text="PRINT INVOICE" Visible="False" Width="250px" />
                                                <asp:Button ID="btnPrintAppForms2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPrintAppForms_Click" TabIndex="9000" Text="PRINT APPLICATION" Visible="False" Width="220px" />
                                                <asp:Button ID="BtnExit2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="BtnExit_Click" TabIndex="9000" Text="EXIT" Width="220px" />
                                                <asp:Button ID="BtnAdd3" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnAdd2_Click" TabIndex="75" Text="ADD" Visible="False" Width="220px" />
                                                <asp:Button ID="btnQA2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnQA_Click" TabIndex="78" Text="QUESTIONARY" Width="220px" />
                                                <asp:Button ID="BtnDelete2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnDelete_Click" TabIndex="74" Text="DELETE" Visible="False" Width="220px" />
                                                <asp:Button ID="BtnCancel2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnCancel_Click" TabIndex="110" Text="CANCEL" Width="220px" />
                                                &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnSave_Click" TabIndex="109" Text="CONVERT APPLICATION - N" Visible="False" Width="220px" />
                                                &nbsp;<asp:Button ID="BtnConvert2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnConvert_Click" TabIndex="72" Text="ACCEPT QUOTE - N" Visible="False" Width="220px" />
												<asp:Button ID="BtnExitEndorsement0" runat="server" CssClass="btn btn-primary btn-lg" OnClick="BtnExitEndorsement_Click" TabIndex="72" Text="EXIT" Visible="False" Width="220px" />
                                                <br /><br />
                                                <asp:Button ID="btnCalculate" runat="server" OnClick="btnCalculate_Click" TabIndex="9000"
                                                Text="CALCULATE" CssClass="btn btn-primary btn-lg" Width="175px" Visible="False" />
                                            
                                            </div>

<%--                        <br />
                        <br />
                        <br />--%>
                        <%-- ENDORSEMENT ACCORDION --%>
                        <div id="EndorsementSection" runat="server" class="col-sm-12" align="center">
                        <div id="Div4" runat="server" class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="AccordionEndorsement" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40"
                                    HeaderCssClass="accordion-head" ContentCssClass="accordion-body" RequireOpenedPane="false"
                                    SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane9" runat="server">
                                            <Header>
                                                <asp:Label ID="Label62" runat="server" CssClass="headform1" Font-Bold="True"
                                                    ForeColor="White">ENDORSMENT SECTION</asp:Label>
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>
                                                   
                                 <div class="col-sm-1">
                                 </div>

                                 <table class="tableMain" width="100%">
                                    <%--<tr align="center" class="dividers">
                                        <td align="left" colspan="4">
                                            <asp:Label ID="Label32" runat="server">ENDORSEMENT SECTION</asp:Label>
                                        </td>
                                    </tr>--%>
                                    <tr align="center" >
                                        <td align="center">
                                            <asp:Label ID="Label66" runat="server" Font-Bold="True">Effective Date</asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label71" runat="server" Font-Bold="True">Factor</asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label72" runat="server" Font-Bold="True">ProRata</asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label73" runat="server" Font-Bold="True" Visible="false">ShortRate</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:TextBox ID="txtEffDtEndorsement" runat="server" CssClass="form-controlWhite" 
                                                IsDate="True" TabIndex="20" Width="110px"></asp:TextBox>
                                            <Toolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                                                CultureName="en-US" Mask="99/99/9999" MaskType="Date" 
                                                TargetControlID="txtEffDtEndorsement">
                                            </Toolkit:MaskedEditExtender>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtFactor" runat="server" CssClass="form-controlWhite" MaxLength="14" 
                                                TabIndex="49" Width="110px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtProRata" runat="server" CssClass="form-controlWhite" MaxLength="14" 
                                                TabIndex="49" Width="110px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtShortRate" runat="server" CssClass="form-controlWhite" MaxLength="14" 
                                                TabIndex="49" Width="110px" Visible="false" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr  >
                                        <td height="18">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label74" runat="server" Font-Bold="True">Sections</asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label100" runat="server" Font-Bold="True">Actual</asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label104" runat="server" Font-Bold="True">Previous</asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="Label112" runat="server" Font-Bold="True">Difference</asp:Label>
                                        </td>
                                    </tr>
                                    <tr >
                                        <td align="right" >
                                            <asp:Label ID="Label115" runat="server" Font-Bold="True">Auto</asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtActualPremAuto" runat="server" CssClass="form-controlWhite" 
                                                MaxLength="14" Width="110px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtPreviousPremAuto" runat="server" CssClass="form-controlWhite" 
                                                MaxLength="14" Width="110px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtDiffPremAuto" runat="server" CssClass="form-controlWhite" 
                                                MaxLength="14" TabIndex="49" Width="110px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="Label116" runat="server" Font-Bold="True">Total</asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtActualPremTotal" runat="server" CssClass="form-controlWhite" 
                                                Width="110px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtPreviousPremTotal" runat="server" CssClass="form-controlWhite" 
                                                MaxLength="14" Width="110px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtDiffPremTotal" runat="server" CssClass="form-controlWhite" 
                                                MaxLength="14" TabIndex="49" Width="110px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr >
                                        <td align="right">
                                            <asp:Label ID="Label117" runat="server" Font-Bold="True">Endorsement Premium:</asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtAdditionalPremium" runat="server" CssClass="form-controlWhite" 
                                                MaxLength="14" TabIndex="49" Width="110px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                       </td>
                                        <td align="center">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">
                                            <strong>Comments: </strong>
                                        </td>
                                        <td align="left" colspan="3">
                                            <asp:TextBox ID="txtEndoComments" runat="server" CssClass="" 
                                                TextMode="MultiLine" Width="400px" Height="150px" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr >
                                        <td align="center" colspan="4">
										<asp:UpdateProgress ID="UpdateProgress8" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                    DisplayAfter="10">
                                                    <ProgressTemplate>
                                                        <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                                        <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span> </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            <asp:Button ID="Button6" runat="server" CssClass="btn btn-primary btn-lg" 
                                                OnClick="Button6_Click" Text="Cancel" />
                                            &nbsp;<asp:Button ID="Button5" runat="server" CssClass="btn btn-primary btn-lg" 
                                                OnClick="Button5_Click" Text="Update" />
                                        </td>
                                    </tr>
                                </table>

                                         </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                        </div>
                        <br />
                            <asp:Label ID="lblCurrentPremium" runat="server" CssClass="labelForControl" 
                                Font-Bold="True" Text="Current Premium" Visible="False"></asp:Label>
                                <br />
                            <asp:Label ID="lblCurrentPremiumCurrency" runat="server" 
                                CssClass="labelForControl" Text="$" Visible="False"></asp:Label>
                            <asp:TextBox ID="txtCurrentPremium" runat="server" CssClass="form-controlWhite" 
                                Enabled="False" Font-Bold="True" ReadOnly="True" TabIndex="45" 
                                Width="300px" Visible="False">0.00</asp:TextBox>
                            <br />
                        <br />
                        <table class="tableMain">
                        <tr>
                        <asp:Label ID="LblTotalCases" runat="server" Visible="False">Quotes for Endorsement</asp:Label>
                        </tr>
                        <tr>
                                    <asp:DataGrid ID="DataGridGroup" runat="server" AlternatingItemStyle-BackColor="#FEFBF6"
                                AlternatingItemStyle-CssClass="HeadForm3" AutoGenerateColumns="False" CssClass="tableMain"
                                BorderColor="#003366" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="0" Font-Names="Tahoma" Font-Size="10pt" HeaderStyle-BackColor="#FFE0A6"
                                HeaderStyle-CssClass="" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass=""
                                ItemStyle-HorizontalAlign="center" OnItemCommand="DataGridGroup_ItemCommand"
                                OnItemDataBound="DataGridGroup_ItemDataBound" PageSize="6" Width="75%" align="center" Visible="False">
                                <FooterStyle BackColor="#003366" />
                                <PagerStyle BackColor="#003366" CssClass="Numbers" HorizontalAlign="Left" Mode="NumericPages"
                                    PageButtonCount="20" />
                                <AlternatingItemStyle BackColor="#CCCCCC" Font-Bold="False"
                                    Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                    Font-Underline="False" />
                                <ItemStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Select" 
                                        HeaderText="View">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:ButtonColumn>
                                    <asp:BoundColumn DataField="OPPEndorsementID" HeaderText="OPPEndorsementID" Visible="False">
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="OPPQuotesID" HeaderText="Quotes">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="EndoNum" HeaderText="End. Num.">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="EndoEffective" HeaderText="Effec. End.">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Factor" HeaderText="Factor"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="ProRataPremium" HeaderText="Pro Rata"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="ShortRatePremium" HeaderText="Short Rate" Visible="False"></asp:BoundColumn>
                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Apply" HeaderText="Apply">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:ButtonColumn>
                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Print" HeaderText="Print">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:ButtonColumn>
                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="PrintIDCard" HeaderText="Print ID Cards">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:ButtonColumn>
                                    <asp:BoundColumn DataField="Cambios" HeaderText="Cambios" Visible="False"></asp:BoundColumn>
                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Update" HeaderText="Update" Visible = "false">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:ButtonColumn>
                                    <asp:BoundColumn DataField="ActualPremPropeties" HeaderText="ActualPremPropeties"
                                        Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="ActualPremLiability" HeaderText="ActualPremLiability"
                                        Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="ActualPremAuto" HeaderText="ActualPremAuto" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ActualPremUmbrella" HeaderText="ActualPremUmbrella" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ActualPremTotal" HeaderText="ActualPremTotal" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PreviousPremProperties" HeaderText="PreviousPremProperties"
                                        Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="PreviousPremLiability" HeaderText="PreviousPremLiability"
                                        Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="PreviousPremAuto" HeaderText="PreviousPremAuto" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PreviousPremUmbrella" HeaderText="PreviousPremUmbrella"
                                        Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="PreviousPremTotal" HeaderText="PreviousPremTotal" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DiffPremProperties" HeaderText="DiffPremProperties" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DiffPremLiability" HeaderText="DiffPremLiability" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DiffPremAuto" HeaderText="DiffPremAuto" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DiffPremUmbrella" HeaderText="DiffPremUmbrella" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DiffPremTotal" HeaderText="DiffPremTotal" Visible="False">
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="AdditionalPremium" HeaderText="AdditionalPremium" Visible="False">
                                    </asp:BoundColumn>
                                </Columns>
                                <HeaderStyle BackColor="Silver" CssClass="" Font-Bold="True" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Height="30px"
                                    HorizontalAlign="Left" />
                            </asp:DataGrid>

                            <caption>
                                <br />
                                <br />
                            </caption>
                 
                        
                    </tr>
                
                </table>
                        </div>
                        <%-- END ENDORSEMENT ACCORDIONS--%>
                        <asp:Label ID="Label45" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Collision Deductible " Visible="False"></asp:Label>
                        <asp:DropDownList ID="ddlDedColl" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            TabIndex="56" Visible="False" Width="130px">
                            <asp:ListItem Value="$500">$500</asp:ListItem>
                            <asp:ListItem Value="$1,000">$1,000</asp:ListItem>
                            <asp:ListItem Value="$1,500">$1,500</asp:ListItem>
                            <asp:ListItem Value="$2,500">$2,500</asp:ListItem>
                            <asp:ListItem Value="$5,000">$5,000</asp:ListItem>
                            <asp:ListItem>0</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label30" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Status" Visible="False"></asp:Label>
                        <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="44" Visible="False" Width="200px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Selected="True" Value="A">ACV</asp:ListItem>
                            <asp:ListItem Value="S">Stated</asp:ListItem>
                            <asp:ListItem Value="O">Other</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label44" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Comprehensive Deductible" Visible="False"></asp:Label>
                        <asp:DropDownList ID="ddlDedComp" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            TabIndex="55" Visible="False" Width="130px">
                            <asp:ListItem Value="$250">$250</asp:ListItem>
                            <asp:ListItem Value="$500">$500</asp:ListItem>
                            <asp:ListItem Value="$1,000">$1,000</asp:ListItem>
                            <asp:ListItem Value="$1,500">$1,500</asp:ListItem>
                            <asp:ListItem Value="$2,500">$2,500</asp:ListItem>
                            <asp:ListItem>0</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="Label10" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Employer's Name"></asp:Label>
                        <asp:TextBox ID="TxtEmployerName" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            TabIndex="20" Width="200px"></asp:TextBox>
<%--                        <asp:Label ID="Label33" runat="server" Font-Names="Tahoma" Font-Size="9pt" Text="Policy Claims"></asp:Label>
                        <asp:RadioButtonList ID="radGenPolicyClaim" runat="server" Font-Names="tahoma" Font-Size="8pt"
                            RepeatDirection="Horizontal" TabIndex="27" Width="100px">
                            <asp:ListItem Value="1">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList>--%>
                                                <asp:Label ID="Label107" runat="server" 
                            Text="Did it involve an accident?" CssClass="labelForControl" Visible="False"></asp:Label>
                                                 <br />
                                                <asp:RadioButton ID="rdbQ2Yes" runat="server" 
                            GroupName="Q2" Text="Yes" TabIndex="1018"
                                                    CssClass="labelForControl" Visible="False" />
                                                <asp:RadioButton ID="rdbQ2No" runat="server" 
                            Checked="True" GroupName="Q2" Text="No"
                                                    TabIndex="1019" CssClass="labelForControl" 
                            Visible="False" />
                                                <br />
                                                <br />
                                                <asp:Label ID="Label4" runat="server" Text="Is the Insured a New Customer?"
                                                    CssClass="labelForControl" Visible="False"></asp:Label>
                                                <br />
                                                <asp:RadioButton ID="rdbNewCustomerYes" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkNewCustomer" OnCheckedChanged="rdbNewCustomerYes_CheckedChanged"
                                                    Text="Yes" TabIndex="1020" Visible="False" />
                                                <asp:RadioButton ID="rdbNewCustomerNo" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkNewCustomer" OnCheckedChanged="rdbNewCustomerNo_CheckedChanged"
                                                    Text="No" Checked="False" TabIndex="1021" Visible="False" />
                                                <br />
                                                <br />
                                                <asp:Label ID="LblLastName3" runat="server" Text="Any accidents or losses within last 3 years?"
                                                    CssClass="labelForControl" Visible="False"></asp:Label>
                                                <br />
                                                <asp:RadioButton ID="rdbAnyAccidentsYes" runat="server" 
                            AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkAnyAccidents" OnCheckedChanged="rdbAnyAccidentsYes_CheckedChanged"
                                                    Text="Yes" TabIndex="1022" Visible="False" />
                                                <asp:RadioButton ID="rdbAnyAccidentsNo" runat="server" 
                            AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkAnyAccidents" OnCheckedChanged="rdbAnyAccidentsNo_CheckedChanged"
                                                    Text="No" Checked="True" TabIndex="1023" 
                            Visible="False" />
                                                <br />
                                                <br />
                                                <asp:Label ID="lblHowManyYears" runat="server" Text="How many years with no claims?"
                                                    Visible="False" CssClass="labelForControl"></asp:Label>
                                                <br />
                                                <asp:RadioButton ID="rdb1Year" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkYears" OnCheckedChanged="rdb1Year_CheckedChanged" Text="1 year"
                                                    Visible="False" TabIndex="1024" />
                                                &nbsp;
                                                <asp:RadioButton ID="rdb2Year" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkYears" OnCheckedChanged="rdb2Year_CheckedChanged" Text="2 years"
                                                    Visible="False" TabIndex="1025" />
                                                <asp:RadioButton ID="rdb3Year" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                                    GroupName="chkYears" OnCheckedChanged="rdb3Year_CheckedChanged" Text="3 years or more"
                                                    Visible="False" TabIndex="1026" />
                                                <br />
                                                <br />
                        <asp:TextBox ID="TxtOtherOccupation" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            TabIndex="19" Visible="False" Width="200px"></asp:TextBox>
                        <asp:Label ID="Label32" runat="server" Font-Names="Tahoma" Font-Size="9pt" Text="Renewal #"></asp:Label>
                        <asp:TextBox ID="TxtRenewalNo" runat="server" TabIndex="28" Width="150px"></asp:TextBox>
                        <asp:Label ID="Label9" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Occupation"></asp:Label>
                        <asp:DropDownList ID="ddlOccupation" runat="server" AutoPostBack="True" CssClass="largeTB"
                            Font-Names="Arial Narrow" Font-Size="10pt" OnSelectedIndexChanged="ddlOccupation_SelectedIndexChanged"
                            TabIndex="18" Width="210px">
                        </asp:DropDownList>
                        <asp:Label ID="Label43" runat="server" Font-Names="Tahoma" Font-Size="9pt" Text="0 for New Customer"></asp:Label>
                        <asp:Label ID="Label59" runat="server" CssClass="LabelNormaSize">Insurance Company</asp:Label>
                        <asp:DropDownList ID="ddlInsuranceCompany" runat="server" CssClass="largeTB" Font-Names="Tahoma"
                            Font-Size="8pt" TabIndex="29" Width="250px">
                        </asp:DropDownList>
                        <asp:Label ID="Label56" runat="server" Font-Names="Tahoma" Font-Size="9pt" Text="Agency"></asp:Label>
                        <asp:DropDownList ID="ddlAgency" runat="server" CssClass="largeTB" Font-Names="Tahoma"
                            Font-Size="8pt" TabIndex="34" Width="250px">
                        </asp:DropDownList>
                        <asp:Label ID="Label58" runat="server" CssClass="LabelNormaSize">Originated At</asp:Label>
                        <asp:DropDownList ID="ddlOriginatedAt" runat="server" CssClass="largeTB" Font-Names="Tahoma"
                            Font-Size="8pt" TabIndex="30" Width="250px">
                        </asp:DropDownList>
                        <asp:Label ID="Label78" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                            Font-Size="11pt" ForeColor="White" Text="Premium Info &amp; Totals"></asp:Label>
                        <asp:Label ID="Label79" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Comprehensive"></asp:Label>
                        <asp:TextBox ID="txtComp" runat="server" Enabled="False" TabIndex="45" Width="75px">0.00</asp:TextBox>
                        <asp:Label ID="Label90" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Traffic Violation Points"></asp:Label>
                        <asp:TextBox ID="txtViolationPoints" runat="server" AutoPostBack="True" Font-Names="Arial Narrow"
                            Font-Size="10pt" OnTextChanged="txtViolationPoints_TextChanged" TabIndex="3006"
                            Width="50px">0</asp:TextBox>
                        <asp:Label ID="Label67" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Collision"></asp:Label>
                        <asp:TextBox ID="txtCollision" runat="server" Enabled="False" TabIndex="45" Width="75px">0.00</asp:TextBox>
                        <asp:Label ID="Label91" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Traffic Violation Surcharge"></asp:Label>
                        <asp:TextBox ID="txtViolationSurchargePct" runat="server" Font-Names="Arial Narrow"
                            Font-Size="10pt" TabIndex="3007" Width="50px">0</asp:TextBox>
                        <asp:Label ID="Label63" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Bodily Injury"></asp:Label>
                        <asp:TextBox ID="txtBIRate" runat="server" Enabled="False" TabIndex="45" Width="75px">0.00</asp:TextBox>
                        <asp:Label ID="Label93" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Under Age Surcharge" Visible="False"></asp:Label>
                        <asp:TextBox ID="txtUnderageChargesPct" runat="server" Font-Names="Arial Narrow"
                            Font-Size="10pt" TabIndex="3008" Visible="False" Width="50px">0</asp:TextBox>
                        <asp:Label ID="Label64" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Property Damage"></asp:Label>
                        <asp:TextBox ID="txtPDRate" runat="server" Enabled="False" Width="75px">0.00</asp:TextBox>
                        <asp:Label ID="Label65" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Medical Payment"></asp:Label>
                        <asp:TextBox ID="txtMPRate" runat="server" Enabled="False" Width="75px">0.00</asp:TextBox>
                        <asp:Label ID="Label68" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Uninsured Motorist"></asp:Label>
                        <asp:TextBox ID="txtMotorist" runat="server" Enabled="False" Width="75px">0.00</asp:TextBox>
                        <asp:Label ID="Label83" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="AD&amp;D"></asp:Label>
                        <asp:TextBox ID="txtADD" runat="server" Enabled="False" Width="75px">0.00</asp:TextBox>
                        <asp:Label ID="Label69" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Rental Reimb."></asp:Label>
                        <asp:TextBox ID="txtRentalReimbursment" runat="server" Enabled="False" Width="75px">0.00</asp:TextBox>
                        <asp:Label ID="Label113" runat="server" Font-Names="Arial Narrow" Font-Size="10pt"
                            Text="Taxi Loss Income"></asp:Label>
                        <asp:TextBox ID="txtTaxiLossIncome" runat="server" Enabled="False" Width="75px">0.00</asp:TextBox>
						<asp:TextBox ID="txtVIPA" runat="server" Enabled="False" Width="75px">0.00</asp:TextBox>  
                        <asp:Label ID="Label70" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                            Font-Size="10pt" Text="Sub Total"></asp:Label>
                        <asp:TextBox ID="txtTotalPremiumVehicle" runat="server" Enabled="False" Font-Bold="True"
                            Width="75px">$0.00</asp:TextBox>
                        <asp:Label ID="Label88" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                            Font-Size="10pt" Text="Charges"></asp:Label>
                        <asp:TextBox ID="txtTotalCharges" runat="server" Enabled="False" Font-Bold="True"
                            Width="75px">0.00</asp:TextBox>
                        <asp:TextBox ID="txtSurchargePct" runat="server"
                            TabIndex="2007" AutoPostBack="True" Visible="False">0</asp:TextBox>
                        <asp:Label ID="Label114" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                            Font-Size="10pt" ForeColor="Red" Text="Surcharge" Visible="False"></asp:Label>
                        <asp:TextBox ID="txtSurcharge" runat="server" Enabled="False" Font-Bold="True"
                            ForeColor="Red" Width="75px" Visible="False">0.00</asp:TextBox>
                        <asp:TextBox ID="txtAffinityDiscountPct" runat="server"
                            TabIndex="2007" AutoPostBack="True" Visible="False">0</asp:TextBox>
                        <asp:Label ID="Label31" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                            Font-Size="10pt" ForeColor="Red" Text="Affinity Discount" Visible="False"></asp:Label>
                        <asp:TextBox ID="txtAffinityDiscount" runat="server" Enabled="False" Font-Bold="True"
                            ForeColor="Red" Width="75px" Visible="False">0.00</asp:TextBox>
                        <asp:Label ID="Label98" runat="server" Font-Bold="True" 
                            Font-Names="Arial Narrow" Font-Size="10pt" ForeColor="Red" Text="Discounts"></asp:Label>
                        <asp:TextBox ID="txtTotalDiscount" runat="server" Enabled="False" 
                            Font-Bold="True" ForeColor="Red" Width="75px">0.00</asp:TextBox>
                        <asp:Label ID="Label89" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                            Font-Size="10pt" Text="Total Premium"></asp:Label>
                        <asp:TextBox ID="txtGrandTotal" runat="server" Enabled="False" Font-Bold="True" Width="75px">0.00</asp:TextBox>
                        <asp:TextBox ID="TextBox1" runat="server" Enabled="False" Visible="false" Font-Bold="True" Width="75px"></asp:TextBox>
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
                        <asp:Label ID="Label1" runat="server"
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
                <br />
                <br />

                              <%-- <asp:Panel ID="pnlEmailVerification" runat="server" CssClass="" Width="450px" BackColor="#F4F4F4"
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
                                              CssClass="form-controlWhite" OnTextChanged ="txtEmailVerification_TextChanged" AutoPostBack="True"></asp:TextBox>
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
                <br />--%>

                <asp:Panel ID="pnlQuestions" runat="server" CssClass="" Width="453px" BackColor="#F4F4F4"
                    Height="230px">
                    <div class="" style="padding: 0px; background-color: #FFFFFF; color: #FFFFFF; font-family: tahoma;
                        font-size: 14px; font-weight: normal; font-style: normal; background-repeat: no-repeat;
                        text-align: left; vertical-align: bottom;">
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Lucida Sans"
                            Font-Size="14pt" Text="Questionary" ForeColor="Maroon" />
                    </div>
                    <div>
                        <table style="background-position: center; width: 454px; height: 175px;">
                            <tr>
                                <td class="style11">
                                    <asp:Label ID="Label106" runat="server" Font-Names="Arial Narrow" Font-Size="8pt"
                                        Text="1. A. HAS APPLICANT OR ANY OPERATOR BEEN CITED OR FINED FOR ANY MOTOR VEHICLE MOVING VIOLATION?"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    &nbsp;&nbsp;&nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="" align="center">
                        <asp:Button ID="btnAcceptAnswers" runat="server" Text="OK" Width="100px" OnClick="btnAcceptAnswers_Click" />
                    </div>
                </asp:Panel>
                <Toolkit:ModalPopupExtender ID="mpeQuestions" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="" DropShadow="True" OkControlID="btnAcceptAnswers" OnCancelScript=""
                    OnOkScript="" PopupControlID="pnlQuestions" TargetControlID="btnDummy2">
                </Toolkit:ModalPopupExtender>
                <asp:Button ID="btnDummy2" runat="server" BackColor="Transparent" BorderColor="Transparent"
                    BorderStyle="None" BorderWidth="0" Visible="true" />
                <asp:TextBox ID="txtdumybox" runat="server" EnableTheming="True" Font-Names="Arial Narrow"
                    Font-Size="10pt" Style="text-transform: uppercase" TabIndex="1003" Visible="False"
                    Width="190px"></asp:TextBox>

                    <div style="font-weight: 700">

                       <asp:Panel ID="pnlEmail" runat="server" BackColor="#F4F4F4" CssClass="" 
                            Width="700px" BorderWidth="1px">
                        <div style="height:3px; background-color: #808080;"></div>
                            <div class="accordion" align="right" style="margin: 10px,10px,10px,10px;
                                font-size: 14px; font-weight: normal; font-style: normal; background-repeat: no-repeat;
                                text-align: left; vertical-align: bottom; background-color: #808080;">
                                <div class="accordion" style="float:left; width:50%; height:42px; background-color: #808080;">
                                    <asp:Label ID="Label6" style="margin-left:5px; margin-top:10px;" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Gotham Book" Font-Size="14pt" ForeColor="White" Text="Send Email" />
                               </div>
                                <div class="accordion" style="float:right; text-align:right; width:50%; height:42px; background-color: #808080;">
                                    <asp:Button ID="btnCloseSend" style="margin-right:5px; margin-top:3px;" runat="server" CssClass="btn btn-primary btn-lg"  Text="CLOSE" OnClick="btnSendEmail_Click" />
                                </div>
                            </div>
                            <%--<div class="accordion" align="right" style="padding: 0px,10px,0px,10px;
                                font-size: 14px; font-weight: normal; font-style: normal; background-repeat: no-repeat;
                                text-align: left; vertical-align: bottom; background-color: #808080;">
                               &nbsp;&nbsp;&nbsp;<asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Gotham Book" Font-Size="14pt" ForeColor="White" Text="Send Email" />
                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                               <asp:Button ID="btnCloseSend" runat="server" CssClass="btn btn-primary btn-lg"  Text="CLOSE" OnClick="btnSendEmail_Click" />
                           </div>--%>
                           <br>
                           <div align="center">
                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="10">
                                <ProgressTemplate>
                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                    <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span> </ProgressTemplate>
                            </asp:UpdateProgress>
                            </div>
                           <div align="center">
                               <p><strong>Please Fill the Following to Send Email with Policy Attached.</strong></p>
                                <p>
                                    <asp:Label ID="Label7" runat="server" Text="CSR Name:" ForeColor="Red" 
                                        CssClass="labelForControl"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                    ControlToValidate="txtsendName" ValidationGroup="save" /><br />
                                    <asp:TextBox ID="txtsendName" runat="server" Style="text-transform: uppercase" 
                                        CssClass="form-controlWhite" Width="400px" /><br />
                                    <asp:Label ID="Label13" runat="server" Text="CSR Email Address:" 
                                        ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"

                                        ControlToValidate="txtsenderEmail" ValidationGroup="save" />&nbsp;
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator23" 
                                        runat="server" ControlToValidate="txtsenderEmail" Display="Dynamic" 
                                        SetFocusOnError="true" Text="Example: username@gmail.com" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                        ValidationGroup="save" />
                                    <br />
                                    <asp:TextBox ID="txtsenderEmail" runat="server" Style="text-transform: uppercase" 
                                        CssClass="form-controlWhite" Width="400px" />
                                    <br />
                                    <asp:Label ID="Label25" runat="server" align="left" Text="Email Subject:" 
                                        ForeColor="Red" CssClass="labelForControl"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"

                                        ControlToValidate="txtsendSubject" ValidationGroup="save" /><br />
                                    <asp:TextBox ID="txtsendSubject" runat="server" CssClass="form-controlWhite" Width="400px" /><br />
                                    <asp:Label ID="Label26" runat="server" Text="Email Message:" ForeColor="Red" 
                                        CssClass="labelForControl"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"

                                        ControlToValidate="txtsendMessage" ValidationGroup="save" /><br />
                                    <asp:TextBox ID="txtsendMessage" runat="server" CssClass="form-controlWhite" TextMode="MultiLine" Rows="10" Height="200px" Width="400px" />
                                </p>
                                <p>
                                    <asp:Button ID="btnSendEmail" runat="server" CssClass="btn btn-primary btn-lg" Text="SEND" OnClick="btnSendEmail_Click" ValidationGroup="save" />
                                </p>
                                <br>
                               <div align="center" class="">
                               </div>
                           </div>
                       </asp:Panel>                
                       <asp:ModalPopupExtender ID="mpeEmail" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" PopupControlID="pnlEmail" TargetControlID="btnDummy3" OkControlID="btnAceptar">
                </asp:ModalPopupExtender>
                <asp:Button ID="btnDummy3" runat="server" Visible="true" BackColor="Transparent" BorderStyle="None"
                    BorderWidth="0" BorderColor="Transparent" />
                     <asp:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server"
                            TargetControlID="pnlEmail"
                            Radius="10"
                            Corners="All" />

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
                            <br />
                            <asp:DropDownList ID="ddlBankList" runat="server" TabIndex="7010" CssClass="form-controlWhite"
                            OnSelectedIndexChanged="ddlBankList_SelectedIndexChanged" AutoPostBack="True" EnableViewState="true" >
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:Label ID="lblBankListSelected" Text="<b>ADDRESS:</b>" runat="server"></asp:Label>
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
                               &nbsp;&nbsp;&nbsp;<asp:Label ID="Label35" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Gotham Book" Font-Size="14pt" ForeColor="White" Text="Attach Documents" />
                           </div>
                           <div class="">
                               <table class="MainMenu" style="width: 78%; margin-right: 0px; height: 276px;">
                                   <tr>
                                       <td align="left">
                                           <asp:Label ID="Label36" runat="server" Font-Bold="True" Font-Names="Gotham Book">Description:</asp:Label>
                                       </td>
                                       <td align="left">
                                           <asp:TextBox ID="txtDocumentDesc" runat="server" CssClass="textEntry" Width="350px" Height="31px"></asp:TextBox>
                                           &nbsp;
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="left">
                                           <asp:Label ID="Label41" runat="server" Font-Bold="True" Font-Names="Gotham Book">Type of Transaction:</asp:Label>
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
                                           <asp:Label ID="Label49" runat="server" Font-Bold="True" Font-Names="Gotham Book">You can upload an image or PDF with a maximum size of 12MB.</asp:Label>
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
                    DropShadow="True" PopupControlID="pnlAdjunto" TargetControlID="btnDummyDoc" OkControlID="btnAceptar3">
                </asp:ModalPopupExtender>
                <asp:Button ID="btnDummyDoc" runat="server" Visible="true" BackColor="Transparent" BorderStyle="None"
                    BorderWidth="0" BorderColor="Transparent" />
                     <asp:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server"
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
                            <div style="padding: 0px; font-size: 14px; font-weight: normal; font-style: normal; background-repeat: no-repeat;
                                text-align: left; vertical-align: bottom;">
                                <%--a.	Recuerde que al finalizar la emisión de la póliza, tiene que adjuntar los siguientes documentos al cliente. 
•	Insured ID
•	Vehicle Registration
•	Signed Application--%>
                                <asp:PlaceHolder ID="phAlert" runat="server"></asp:PlaceHolder>
                                <cc1:Mirror id="Mirror2" ControlID="btnAdjuntar" runat="server" />
                                <asp:Button ID="btnIssuePolicyAlert" runat="server" OnClick="btnIssuePolicy_Click" OnClientClick="if (!Confirm()){ return false; } this.disabled = true; this.value = 'Saving...';" 
                                    UseSubmitBehavior="false" TabIndex="9000"
                                    Text="ISSUE POLICY" CssClass="btn btn-primary btn-lg" Width="220px" />
                            </div>
                       </asp:Panel>                
                       <asp:ModalPopupExtender ID="mpeAlert" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" PopupControlID="pnlAlert" TargetControlID="btnDummyAlert" OkControlID="btnclosealert">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="btnDummyAlert" runat="server" Visible="true" BackColor="Transparent" BorderStyle="None"
                        BorderWidth="0" BorderColor="Transparent" />
                         <asp:RoundedCornersExtender ID="RoundedCornersExtender4" runat="server"
                                TargetControlID="pnlAlert" Radius="10" Corners="All" />

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
									  <tr>
                                         <td>Down Payment</td>
                                          <td colspan="3" align="left">
                                              <asp:TextBox ID="txtDownPaymentForFM" runat="server" CssClass="textEntry" Width="350px" Height="31px"></asp:TextBox>
                                         </td>
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
                    </div>
                <!--#endregion UPLOAD DOCUMENTS -->
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
    <script type='text/javascript'>
        function Confirm() {
            var ddlReport = document.getElementById("<%=ddlAgent.ClientID%>");
            var Text = ddlReport.options[ddlReport.selectedIndex].text;
            //var Value = ddlReport.options[ddlReport.selectedIndex].value;
            if (Text == "") 
            {   
                alert("This application does not have an agent. \n Please select an Agent!")
                return false;
            }
            else 
            {
                if (confirm('Do you want to submit this policy with agent: ' + Text + "?"))
                    return true;
                else
                    return false;
            }
        }
    </script>
</html> 