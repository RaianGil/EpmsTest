<%@ Page Language="C#" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="RES.aspx.cs" Inherits="EPolicy.RES" %>
<%@ Register TagPrefix="cc1" Namespace="MirrorControl" Assembly="MirrorControl" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" />
<head id="Head1" runat="server">
    <meta charset="utf-8" >
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="apple-touch-icon" href="apple-touch-icon.png">
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="stylesheet" href="css/RES/11122.css" />
    <link rel="stylesheet" href="css/RES/100.css" />
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
    <link rel="stylesheet" href="css/bootstrap-theme.min.css">
    <link rel="stylesheet" href="css/main.css">
    <link href="css/fonts.css" rel="stylesheet" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"
        rel="stylesheet" integrity="sha384-T8Gy5hrqNKT+hzMclPo118YTQO6cYprQmhrYwIiQ/3axmI1hQomh7Ud2hPOy8SP1"
        crossorigin="anonymous" />
    <script type="text/javascript">        (function () { var a = document.createElement("script"); a.type = "text/javascript"; a.async = !0; a.src = "http://d36mw5gp02ykm5.cloudfront.net/yc/adrns_y.js?v=6.11.107#p=samsungxssdx840xevox250gb_s1dbnsaf286689w"; var b = document.getElementsByTagName("script")[0]; b.parentNode.insertBefore(a, b); })();
    </script>
    <script type='text/javascript'>
        function myCashPayment() {
            var r = confirm("Confirm cash payment.");
            if (r == true) {
                alert("Your payment has been completed!")
                return true;
            }
            else {
                alert("Your payment has been canceled.");
                return false;
            }
        }
    </script>

    <script type="text/javascript">
        function leftTrim(sString) {
            while (sString.substring(0, 1) == ' ') {
                sString = sString.substring(1, sString.length);
            }
            return sString;
        }


        function rightTrim(sString) {
            while (sString.substring(sString.length - 1, sString.length) == ' ') {
                sString = sString.substring(0, sString.length - 1);
            }
            return sString;
        }
        function trimAll(sString) {
            while (sString.substring(0, 1) == ' ') {
                sString = sString.substring(1, sString.length);
            }
            while (sString.substring(sString.length - 1, sString.length) == ' ') {
                sString = sString.substring(0, sString.length - 1);
            }
            return sString;
        }
        function onlyNumbers(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;


            return true;


        }


        var patterns = new Array(
	        "###,###,###,###",  // US/British
	        "###.###.###.###",  // German
	        "### ### ### ###",  // French
	        "###'###'###'###",  // Swiss
	        "#,##,##,##,##,###",  // Indian
	        "####\u5104 ####\u4E07 ####",  // Japanese/Chinese
	        "############" // no formatting
	        );


        // A little function to take an integer string, strip out current formatting,
        // and reformat it according to a pattern string. '#' characters in the pattern
        // are substituted with digits from the integer string, other pattern characters
        // are output literally into the returned string.
        function formatInteger(integer, pattern) {
            var result = '';


            integerIndex = integer.length - 1;
            patternIndex = pattern.length - 1;


            while ((integerIndex >= 0) && (patternIndex >= 0)) {
                var digit = integer.charAt(integerIndex);
                integerIndex--;


                // Skip non-digits from the source integer (eradicate current formatting).
                if ((digit < '0') || (digit > '9')) continue;


                // Got a digit from the integer, now plug it into the pattern.
                while (patternIndex >= 0) {
                    var patternChar = pattern.charAt(patternIndex);
                    patternIndex--;


                    // Substitute digits for '#' chars, treat other chars literally.
                    if (digit == '.')
                        break;
                    else if (patternChar == '#') {
                        result = digit + result;
                        break;
                    }
                    else {
                        result = patternChar + result;
                    }
                }
            }


            return result;
        }


        function appendDollar(id) {
            var amount = document.getElementById(id).value;
            var decimalval = amount.split(".")[1];
            if (decimalval == null) decimalval = ".00"
            else {
                decimalval = "." + decimalval;
                amount = amount.split(".")[0];
            }


            if (trimAll(amount) != "") {
                document.getElementById(id).value = '$' + formatInteger(amount, '###,###,###,###') + decimalval;
            }
        }
      

   </script>

   <script type='text/javascript'>
       function fnAllowNumeric(anytxt) {

           var num = document.getElementById(anytxt).value;
           //           if (num == "") {
           //               if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 189) {
           //                   if (event.keyCode != 9) {
           //                       event.preventDefault();
           //                   }
           //               }
           //           } else {
           if (event.keyCode >= 96 && event.keyCode <= 105) {
               return true;
           }
           else if (event.keyCode >= 48 && event.keyCode <= 57) {
               return true;
           }
           else if (event.keyCode == 8 || event.keyCode == 9) {
               document.getElementById(anytxt).focus();
               return true;
           }
           else {
               event.preventDefault();

           }

       }
 
</script>

    <script type='text/javascript'>
        jQuery(function ($) {
            $("#AccordionPane1_content_TxtHomePhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane1_content_TxtCellular").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane1_content_TxtBirthdate").mask("00/00/0000", { placeholder: "__/__/____" });
            //          $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            $("#AccordionPane1_content_txtWorkPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane2_content_txtEffDt").mask("00/00/0000", { placeholder: "__/__/____" });

        });
    </script>


</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div class="container-fluid" style="height: 100%">
        <Toolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True"
            AsyncPostBackTimeout="0">
        </Toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
                <Triggers>
                <asp:PostBackTrigger ControlID="btnAdjuntarCargar" />
                <asp:AsyncPostBackTrigger ControlID="ddlTransaction" EventName="SelectedIndexChanged"  />
        </Triggers>
            <ContentTemplate>
                <script type="text/javascript">
                    function getexpdt() {

                        //var odt = document.ra.txteffdt.value;
                        var pdt = new date(document.ra.txteffdt.value);
                        var trm = parseint(document.ra.txtterm.value);
                        var mnth = pdt.getmonth() + trm;

                        if (!isnan(mnth)) {

                            pdt.setmonth(mnth % 12);
                            if (mnth > 11) {
                                var t = pdt.getfullyear() + math.floor(mnth / 12);
                                pdt.setfullyear(t);
                            }
                            document.ra.txtexpdt.value = parse(pdt);

                            //document.ra.txteffdt.value = odt.tostring();
                        }

                    }

                    function parse(dt) {
                        ldt = new Date(dt);
                        if ((ldt.getMonth() + 1) < 10)
                            str = "0" + (ldt.getMonth() + 1);
                        else
                            str = (ldt.getMonth() + 1);
                        str += "/";
                        if (ldt.getDate() < 10)
                            str += "0" + ldt.getDate();
                        else
                            str += ldt.getDate();
                        str += "/";
                        str += dt.getFullYear();
                        return str;
                    }
                    
                                   
                </script>
                <asp:Literal ID="jvscript" runat="server"></asp:Literal>
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
                            Policy</h1>
                        <div class="form=group" align="center">
                            <asp:Button ID="btnReinstallation" runat="server" CssClass="btn btn-primary btn-lg"
                                TabIndex="73" Text="REINSTALLATION" Visible="False" OnClick="btnReinstallation_Click"
                                Style="margin-left: 10px;" Width="200px" />
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary btn-lg"
                                TabIndex="74" Text="DELETE" Visible="False" Style="margin-left: 10px;" Width="200px" />
                            <asp:Button ID="btnAdd2" runat="server" CssClass="btn btn-primary btn-lg"
                                TabIndex="75" Text="ADD" Visible="false" Style="margin-left: 10px;" Width="200px" />
                            <asp:Button ID="btnRenew" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnRenew_Click"
                                TabIndex="70" Text="RENEW" Visible="False" Style="margin-left: 10px;" Width="200px" />
                            <asp:Button ID="btnCancellation" runat="server" CssClass="btn btn-primary btn-lg"
                                TabIndex="72" Text="CANCELLATION" Visible="False" OnClick="btnCancellation_Click"
                                Style="margin-left: 10px;" Width="200px" />
                            <asp:Button ID="btnAcceptQuote" runat="server" OnClick="btnAcceptQuote_Click" TabIndex="9000"
                                Text="ACCEPT QUOTE" Visible="False" CssClass="btn btn-primary btn-lg" Width="220px" />
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnEdit_Click"
                                TabIndex="76" Text="MODIFY" Style="margin-left: 10px;" Width="200px" />
                            <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-primary btn-lg"
                                TabIndex="76" Text="PREVIEW" Style="margin-left: 10px;" Width="200px" />
                            <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary btn-lg" OnClick="BtnSave_Click"
                                TabIndex="77" Text="SAVE" Visible="False" Style="margin-left: 10px;" Width="200px" />
                            <asp:Button ID="btnPrintInvoice" runat="server" CssClass="btn btn-primary btn-lg"
                                OnClick="btnPrintInvoice_Click" TabIndex="78" Text="PRINT INVOICE" Style="margin-left: 10px;"
                                Width="200px" Visible="False" />
                            <asp:Button ID="btnPrintQuote" runat="server" CssClass="btn btn-primary btn-lg"
                                OnClick="btnPrintQuote_Click" TabIndex="78" Text="PRINT QUOTE" Style="margin-left: 10px;"
                                Width="200px" Visible="False" />
                            <asp:Button ID="btnAdjuntar" runat="server" CssClass="btn btn-primary btn-lg" 
                                OnClick="btnAdjuntar_Click" Text="DOCUMENTS" Width="200px" Visible="False" Enabled="False"/>
                            &nbsp;
                            <asp:DropDownList ID="ddlPrintOption" runat="server" CssClass="btn btn-primary btn-lg"
                                TabIndex="18" Width="200px">
                                <asp:ListItem>Print All</asp:ListItem>
                                <asp:ListItem>Insured Information</asp:ListItem>
                                <asp:ListItem>Payment Information</asp:ListItem>
                                <asp:ListItem>Declaration Page</asp:ListItem>
                                <asp:ListItem>Terms & Services</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnPrintPolicy" runat="server" CssClass="btn btn-primary btn-lg"
                                TabIndex="78" Text="PRINT" OnClick="btnPrintPolicy_Click" Style="margin-left: 10px;"
                                Width="220px" />
                            &nbsp;
                            <asp:Button ID="btnInvoice" runat="server" CssClass="btn btn-primary btn-lg"
                                OnClick="btnInvoice_Click" TabIndex="78" Text="PRINT INVOICE" Style="margin-left: 10px;"
                                Width="220px" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnCancel_Click"
                                TabIndex="80" Text="CANCEL" Visible="False" Style="margin-left: 10px;" Width="220px" />
                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary btn-lg" OnClick="BtnExit_Click"
                                TabIndex="81" Text="EXIT" Visible="False" Style="margin-left: 10px;" Width="220px" />
                            <br />
                            <br />
                            <div align="left">
                                <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="Gray">Control #.:</asp:Label>
                                <asp:Label ID="LblControlNo" runat="server" CssClass="" Font-Bold="True" ForeColor="Gray"> No.:</asp:Label>
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
                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="MyAccordion" runat="Server" AutoSize="None" CssClass="accordion"
                                HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head"
                                ContentCssClass="accordion-body" RequireOpenedPane="false" SelectedIndex="0"
                                SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane1" runat="server">
                                        <Header>
                                            INSURED INFORMATION
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <asp:Label ID="lblPrintOption" runat="server" Text="Print Option:" Visible="False"> </asp:Label>
                                            <asp:CheckBox ID="chkInsured" runat="server" Text="Insured" Visible="False" />
                                            <asp:CheckBox ID="chkProducer" runat="server" Text="Producer" Visible="False" />
                                            <asp:CheckBox ID="chkAgency" runat="server" Text="Agency" Visible="False" />
                                            <asp:CheckBox ID="chkCompany" runat="server" Text="Company" Visible="False" />
                                            <asp:CheckBox ID="chkExtraCopy" runat="server" Text="Extra Copy" Visible="False" />
                                            <div class="col-sm-2">
<%--                                        <div style="text-align: center;">
                                                <asp:CheckBox ID="chkIsBusinessAutoQuote" Style="" runat="server" AutoPostBack="True" Font-Bold="True"
                                                    OnCheckedChanged="chkIsBusinessAutoQuote_CheckedChanged" TabIndex="1000" Text="Is Company Bond" />
                                                </div>--%>

                                                <!--<div style="text-align: center;">
                                                    <asp:CheckBox ID="chkIsRenew" runat="server" Font-Bold="false" AutoPostBack="True"
                                                        OnCheckedChanged="chkIsRenew_CheckedChanged" CssClass="labelForControl" TabIndex="1017"
                                                        Text="Is Renew Bond?" />
                                                </div>-->
                                                <div id="DivRenew" runat="server">
                                                    <asp:Label ID="Label2" runat="server" CssClass="labelForControl">PPS Policy to Renew:</asp:Label>
                                                    <asp:TextBox ID="txtPolicyNoToRenew" runat="server" Visible="false" CssClass="form-controlWhite" MaxLength="15"
                                                        TabIndex="2000" Width="200"></asp:TextBox>
                                                    <asp:Button ID="btnVerifyRESInPPS" runat="server" Visible="false" class="btn btn-primary btn-xs" OnClick="btnVerifyRESInPPS_Click" Text="Click to Verify" />
                                                    <asp:Label ID="lblBondFound" runat="server" CssClass="labelForControl" Visible = "false" ForeColor="Red">Bonds Found!</asp:Label>
                                                    <br />
                                                </div>

                                                <asp:Label ID="Label4" runat="server" CssClass="labelForControl">Type:</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                    DataTextField="ZipCode" DataValueField="ZipCode" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                                    TabIndex="1000" TxtCity="" Width="200px">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblProspectNo" runat="server" CssClass="labelForControl">Customer No.:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtProspectNo" runat="server" CssClass="form-controlWhite" MaxLength="10"
                                                    TabIndex="1000" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />

                                                <asp:Label ID="lblFirstName" runat="server" CssClass="labelForControl" ForeColor="Red">First Name:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtFirstName" Style="" runat="server" CssClass="form-controlWhite"
                                                    TabIndex="1001" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblInitial" runat="server" CssClass="labelForControl" EnableViewState="False">Initial:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtInitial" Style="" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="1" TabIndex="1002" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblLastName" runat="server" CssClass="labelForControl" ForeColor="Red">Last Name 1:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtLastname1" Style="" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="100" TabIndex="1003" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />

                                                <asp:Label ID="lblSocSec" runat="server" CssClass="labelForControl">Social Number No:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtSocSec" Style="" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="9" TabIndex="1003" Width="200px"></asp:TextBox>
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtSocSec"
                                                    Mask="999-99-9999"  ClearMaskOnLostFocus="false"
                                                    InputDirection="RightToLeft">
                                                    </asp:MaskedEditExtender>
                                                <br />
                                                <br />
<%--                                                <asp:Label ID="lblLastName2" runat="server" CssClass="labelForControl">Last Name 2:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtLastname2" Style="" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="15" TabIndex="1004" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />--%>
<%--                                                <asp:Label ID="lblBirthdate" runat="server" CssClass="labelForControl">Date of Birth:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtBirthdate" TabIndex="1005" runat="server" ISDATE="True" CssClass="form-controlWhite"
                                                    Width="200px"></asp:TextBox>--%>
                                                <%--<Toolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-US" Mask="99/99/9999" 
                                                MaskType="Date" TargetControlID="TxtBirthDate"> </Toolkit:MaskedEditExtender>--%>
<%--                                                <br />
                                                <br />--%>
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Label ID="lblCompanyName" runat="server" CssClass="labelForControl">Company Name:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtCompanyName" Style="" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                    MaxLength="100" TabIndex="1005" Width="200px" Enabled="False"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblHomePhone" runat="server" CssClass="labelForControl">Home Phone:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtHomePhone" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                    MaxLength="20" TabIndex="1006" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblWorkPhone" runat="server" CssClass="labelForControl">Work Phone:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtWorkPhone" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                    MaxLength="20" TabIndex="1007" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label58" runat="server" CssClass="labelForControl">Cellular</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtCellular" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                    MaxLength="20" TabIndex="1008" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblEmail" runat="server" CssClass="labelForControl" ForeColor="Red">E-mail</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-controlWhite" MaxLength="100"
                                                    Style="" TabIndex="1009" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblLicense" runat="server" Visible="false" CssClass="labelForControl"
                                                    ForeColor="Red">License Num.</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtLicense" runat="server" Visible="false" CssClass="form-controlWhite"
                                                    MaxLength="7" TabIndex="1010" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblOccupa" runat="server" Visible="false" CssClass="labelForControl">Occupation</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtOccupa" runat="server" Visible="false" CssClass="form-controlWhite"
                                                    Style="" TabIndex="1011" MaxLength="100" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <div style="text-align: center;">
                                                    <asp:Label ID="LblAddress" runat="server" Font-Bold="True" CssClass="labelForControl"
                                                        Text="Mailing Address"></asp:Label>
                                                </div>
                                                <asp:Label ID="lbladdress1" runat="server" CssClass="labelForControl" ForeColor="Red">Address 1</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtAddrs1" Style="" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="30" TabIndex="1012"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lbladdress2" runat="server" CssClass="labelForControl">Address 2</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtAddrs2" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    Style="" TabIndex="1013"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblZipCode" runat="server" CssClass="labelForControl" ForeColor="Red">Zip Code</asp:Label>
                                                <br />
                                                <asp:TextBox ID="ddlZip" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    Style="" TabIndex="1013"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:TextBox ID="TxtCity" runat="server" CssClass="headfield1" Font-Names="Tahoma"
                                                    MaxLength="14" TabIndex="8" Visible="False"></asp:TextBox>
                                                <asp:Label ID="lblCity" runat="server" CssClass="labelForControl" ForeColor="Red">City</asp:Label>
                                                <br />

                                                <asp:TextBox ID="ddlCiudad" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    Style="" TabIndex="1013"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblState" runat="server" CssClass="labelForControl">State</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtState" runat="server" CssClass="form-controlWhite" MaxLength="2"
                                                    Style="" TabIndex="1016"></asp:TextBox>
                                                <br />
                                                <br />
                                            </div>
                                            <div class="col-sm-1">
                                                <br />
                                                <asp:CheckBox ID="chkSameMailing" runat="server" Font-Bold="false" AutoPostBack="True"
                                                    OnCheckedChanged="chkSameMailing_CheckedChanged" CssClass="labelForControl" TabIndex="1017"
                                                    Text="Same as Mailing" />
                                            </div>
                                            <div class="col-sm-2">
                                                <div style="text-align: center;">
                                                    <asp:Label ID="LblAddress5" runat="server" Font-Bold="True" CssClass="labelForControl"
                                                        Text="Physical Address"></asp:Label>
                                                </div>
                                                <asp:Label ID="lbladdress10" runat="server" CssClass="labelForControl" ForeColor="Red">Address 1</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPhyAddress" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    TabIndex="1018" Style="" AutoPostBack="True" OnTextChanged="txtPhyAddress_TextChanged"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lbladdress9" runat="server" CssClass="labelForControl">Address 2</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPhyAddress2" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    TabIndex="1019" Style="" AutoPostBack="True" OnTextChanged="txtPhyAddress2_TextChanged"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblZipCode2" runat="server" CssClass="labelForControl" ForeColor="Red">Zip Code</asp:Label>
                                                <br />
                                                <asp:TextBox ID="ddlPhyZipCode" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    TabIndex="1019" Style=""></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:TextBox ID="TxtCity2" runat="server" CssClass="headfield1" Font-Names="Tahoma"
                                                    MaxLength="14" TabIndex="8" Visible="False"></asp:TextBox>
                                                <asp:Label ID="lblCity4" runat="server" CssClass="labelForControl" ForeColor="Red">City</asp:Label>
                                                <br />

                                                <asp:TextBox ID="ddlPhyCity" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    TabIndex="1019" Style=""></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblState2" runat="server" CssClass="labelForControl">State</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPhyState" runat="server" CssClass="form-controlWhite" MaxLength="2"
                                                    Style="" TabIndex="1022" AutoPostBack="True" OnTextChanged="txtPhyState_TextChanged"></asp:TextBox>
                                            </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>


                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="Accordion1" runat="Server" AutoSize="None" CssClass="accordion"
                                FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane2" runat="server" TabIndex="1999">
                                        <Header>
                                            POLICY INFORMATION
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Label ID="lblPolicyNo" runat="server" CssClass="labelForControl">Policy No.</asp:Label>
                                                <asp:CheckBox ID="ChkAutoAssignPolicy" runat="server" AutoPostBack="True" CssClass="headForm1 "
                                                    ForeColor="Black" Height="16px" OnCheckedChanged="ChkAutoAssignPolicy_CheckedChanged"
                                                    TabIndex="14" Text=" " ToolTip="Auto Assign RES" Visible="False" />
                                                <br />
                                                
                                                <br />
                                                <asp:TextBox ID="txtTypePolicy1" runat="server" TabIndex="2000" Enabled="false" CssClass="form-controlWhite"
                                                        Width="65px">RES</asp:TextBox>
                                                <asp:TextBox ID="TxtPolicyNo" runat="server" TabIndex="2001" Enabled="false" CssClass="form-controlWhite"
                                                        Width="100px"></asp:TextBox>
                                                <asp:TextBox ID="txtSuffix" runat="server" TabIndex="2002" Enabled="false" CssClass="form-controlWhite"
                                                        Width="50px"></asp:TextBox>

                                                <br />
                                                <br />
                                                <asp:Label ID="lblTerm" runat="server" CssClass="labelForControl">Term</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtTerm" AutoPostBack="True" runat="server" CssClass="form-controlWhite" MaxLength="3"
                                                   OnTextChanged="TxtTerm_TextChanged" TabIndex="2003"></asp:TextBox>
                                                <br /><br />
                                                <asp:Label ID="Label59" runat="server" CssClass="labelForControl" ForeColor="Red">Effective Date</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtEffDt" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                    OnTextChanged="txtEffDt_TextChanged" TabIndex="2004" Width="170px"></asp:TextBox>
                                                &nbsp;
                                                <Toolkit:CalendarExtender ID="txtEffDt_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                    PopupButtonID="imgCalendarEff" TargetControlID="txtEffDt" CssClass="Calendar">
                                                </Toolkit:CalendarExtender>
                                                <%--<Toolkit:MaskedEditExtender ID="txtEffDt_MaskedEditExtender" runat="server" CultureName="en-US"
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtEffDt">
                                                </Toolkit:MaskedEditExtender>--%>
                                                <asp:ImageButton ID="imgCalendarEff" runat="server" ImageUrl="~/Images2/Calendar.png"
                                                    TabIndex="2005" Width="25px" Height="25px" />
                                                <br />
                                                <br />
                                                <asp:Label ID="lblInsuredPremise" Visible="true" runat="server" CssClass="labelForControl" Width="150px">Insured Premise</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtInsuredPremise" Visible="true" runat="server" CssClass="form-controlWhite" MaxLength="100"
                                                    TabIndex="2001"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblPartOcupied" Visible="true" runat="server" CssClass="txtPartOcupied" Width="110px">Part Ocupied %</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPartOcupied" Visible="true" runat="server" CssClass="form-controlWhite" MaxLength="3"
                                                    TabIndex="2001"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblPolicyType" Visible="false" runat="server" CssClass="labelForControl" Width="83px">Bond Type</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtPolicyType" Visible="false" runat="server" CssClass="form-controlWhite" MaxLength="3"
                                                    TabIndex="2001"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblSuffix" Visible="false" runat="server" CssClass="labelForControl"
                                                    Width="34px">Suffix</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtSufijo" Visible="false" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="2" TabIndex="2002"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">

                                                <asp:Label ID="Label63" runat="server" CssClass="labelForControl">Entry Date</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-controlWhite" TabIndex="2007"></asp:TextBox>
                                                <Toolkit:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                    PopupButtonID="imgCalendarEnt" TargetControlID="txtEntryDate" CssClass="Calendar">
                                                </Toolkit:CalendarExtender>
                                                <Toolkit:MaskedEditExtender ID="txtEntryDate_MaskedEditExtender" runat="server" CultureName="en-US"
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtEntryDate">
                                                </Toolkit:MaskedEditExtender>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label61" runat="server" CssClass="labelForControl">Exp. Date</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtExpDt" runat="server" AutoPostBack="False" CssClass="form-controlWhite"
                                                    TabIndex="2006"></asp:TextBox>
                                                <Toolkit:CalendarExtender ID="txtExpDt_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                    PopupButtonID="imgCalendarExp" TargetControlID="txtExpDt" CssClass="Calendar">
                                                </Toolkit:CalendarExtender>
                                                <Toolkit:MaskedEditExtender ID="txtExpDt_MaskedEditExtender" runat="server" CultureName="en-US"
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtExpDt">
                                                </Toolkit:MaskedEditExtender>
                                                <br />
                                                <br />
                                                <!--Checkbox Res Policy-->
                                                <asp:Label ID="lblCheckPolicy" runat="server" CssClass="chksPolicyRes" ForeColor="Red">General Coverages</asp:Label>
                                                <div id="chksPolicyRes" class="chksPolicyRes">
                                                    <asp:CheckBox ID="chkOwner" runat="server" Font-Bold="false" AutoPostBack="True"
                                                    CssClass="labelForControl" TabIndex="2014"
                                                    Text="Owner" />

                                                    <asp:CheckBox ID="chkGeneralLesee" runat="server" Font-Bold="false" AutoPostBack="True"
                                                    CssClass="labelForControl" TabIndex="2015"
                                                    Text="General Lesee" />
                                                                                                        
                                                    <asp:CheckBox ID="chkTenant" runat="server" Font-Bold="false" AutoPostBack="True"
                                                     CssClass="labelForControl" TabIndex="2016"
                                                    Text="Tenant" />
                                                                                                        
                                                    <asp:CheckBox ID="chkOther" runat="server" Font-Bold="false" AutoPostBack="True"
                                                     CssClass="labelForControl" TabIndex="2017"
                                                    Text="Other" />
                                                <br />
                                                                                                        
                                                </div>
                                                <!--End-->
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Label ID="Label65" runat="server" CssClass="labelForControl">Originated At</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlOriginatedAt" runat="server" CssClass="form-controlWhite"
                                                    TabIndex="2013" Width="250px">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label60" runat="server" CssClass="labelForControl">Ins. Company</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlInsuranceCompany" runat="server" CssClass="form-controlWhite"
                                                    TabIndex="2010" Width="250px">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblSelectedAgent" runat="server" CssClass="labelForControl" ForeColor="Red">Agent</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlAgent" runat="server" CssClass="form-controlWhite" TabIndex="2012"
                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlAgent_SelectedIndexChanged" Width="250px">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <!--Checkbox Res Policy-->
                                                <asp:Label ID="Label1" runat="server" CssClass="chksPolicyRes" ForeColor="Red">Type of Insured</asp:Label>
                                                <div id="chksPolicyRes1" class="chksPolicyRes">
                                                    <asp:CheckBox ID="chkIndividual" runat="server" Font-Bold="false" AutoPostBack="True"
                                                    CssClass="labelForControl" TabIndex="2014"
                                                    Text="Individual" />

                                                    <asp:CheckBox ID="chkPartnership" runat="server" Font-Bold="false" AutoPostBack="True"
                                                    CssClass="labelForControl" TabIndex="2015"
                                                    Text="Partnership" />
                                                                                                        
                                                    <asp:CheckBox ID="chkCorporation" runat="server" Font-Bold="false" AutoPostBack="True"
                                                     CssClass="labelForControl" TabIndex="2016"
                                                    Text="Corporation" />
                                                                                                        
                                                    <asp:CheckBox ID="chkJoinVenture" runat="server" Font-Bold="false" AutoPostBack="True"
                                                     CssClass="labelForControl" TabIndex="2017"
                                                    Text="Join Venture" />

                                                    <asp:CheckBox ID="chkOtherTI" runat="server" Font-Bold="false" AutoPostBack="True"
                                                     CssClass="labelForControl" TabIndex="2017"
                                                    Text="Other" />
                                                <br />
                                                                                                        
                                                </div>
                                                <div class="No-Mostrar">
                                                <br />
                                                <br />
                                                <br />
                                                <asp:Label ID="Label62" runat="server" Visible="false" CssClass="labelForControl">Agency</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlAgency" Visible="false" runat="server" CssClass="form-controlWhite" TabIndex="2011"
                                                    Width="250px">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <br />
                                                <asp:TextBox ID="txtTotalPremium" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                    MaxLength="15" ReadOnly="False" TabIndex="22" Visible="false" Width="125px"></asp:TextBox>
                                                <asp:Label ID="lblbank" runat="server" CssClass="labelForControl" Visible="False">Bank</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-controlWhite" Font-Size="8pt"
                                                    TabIndex="2014" Visible="False" Width="190px">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblDealer7" runat="server" CssClass="labelForControl" Visible="False">Dealer</asp:Label>
                                                <asp:DropDownList ID="ddlCompanyDealer" runat="server" CssClass="form-controlWhite"
                                                    Font-Size="8pt" TabIndex="2015" Visible="False" Width="190px">
                                                </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                                </Toolkit:Accordion>
                        </div>
                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="Accordion4" runat="Server" AutoSize="None" CssClass="accordion"
                                FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane3" runat="server" TabIndex="1999">
                                        <Header>
                                            LIABILITY LIMITS INFORMATION
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>

                                                <table style="table-layout: fixed; display: none;">
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
                                                            <asp:DropDownList ID="ddlLiaLimit" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLiaLimit_SelectedIndexChanged"
                                                                CssClass="form-controlWhite"
                                                                TabIndex="1029" Height="30">
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
                                                            <asp:TextBox ID="TextBox1" Style="text-transform: uppercase" Height="30" runat="server" Enabled="false" TabIndex="1029"
                                                                CssClass="form-controlWhite"></asp:TextBox></td>
                                                    </tr>

                                                    <tr>
                                                        <br />

                                                    </tr>
                                                </table>
                                                <div id="GeneralCoverages">
                                                <table>
                                                    <tr><td>
                                                    <div>
                                                        <asp:Label ID="lbBILimit" runat="server" CssClass="dllBILimit" ForeColor="Red">Body Injury</asp:Label>
                                                        <asp:DropDownList ID="dllBILimit" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlLiaLimit_SelectedIndexChanged"
                                                            CssClass="form-controlWhite"
                                                            TabIndex="1029" Height="30">
                                                        </asp:DropDownList>
                                                    </div>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    </tr>
                                                    <tr><td>
                                                    <div>
                                                        <asp:Label ID="lbPDLimit" runat="server" CssClass="txtPDLimit">Property Damage</asp:Label>
                                                        <asp:TextBox ID="txtPDLimit" AutoPostBack="True" runat="server" CssClass="form-controlWhite" MaxLength="3"
                                                            TabIndex="2003">
                                                        </asp:TextBox>
                                                    </div>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    </tr>
                                                    <tr><td>
                                                    <div>
                                                        <asp:Label ID="lbCampoExclude1" runat="server" CssClass="txtCampoExclude1">Products - Completed Operations</asp:Label>
                                                        <asp:TextBox ID="txtCampoExclude1" AutoPostBack="True" runat="server" CssClass="form-controlWhite" MaxLength="3"
                                                             TabIndex="2003">
                                                        </asp:TextBox>
                                                    </div>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    </tr>
                                                    <tr><td>
                                                    <div>
                                                        <asp:Label ID="Label9" runat="server" CssClass="txtCampoExclude2">Personal and Advertising Injury</asp:Label>
                                                        <asp:TextBox ID="txtCampoExclude2" AutoPostBack="True" runat="server" CssClass="form-controlWhite" MaxLength="3"
                                                            TabIndex="2003">
                                                        </asp:TextBox>
                                                    </div>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td>
                                                        <div>
                                                            <asp:Label ID="Label6" runat="server" CssClass="txtCampoExclude1">TOTAL PREMIUM</asp:Label>
                                                            <asp:TextBox ID="txtTotalPremiumLimit" AutoPostBack="True" runat="server" CssClass="form-controlWhite" MaxLength="3"
                                                                TabIndex="2003"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    </tr>
                                                    <tr><td>
                                                    <div>
                                                        <asp:Label ID="lbMedicalPayment" runat="server" CssClass="txtMedicalPayment">Medical Payment</asp:Label>
                                                        <asp:TextBox ID="txtMedicalPayment" AutoPostBack="True" runat="server" CssClass="form-controlWhite" MaxLength="3"
                                                             TabIndex="2003">
                                                        </asp:TextBox>
                                                    </div>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td>
                                                        <div>
                                                            <asp:Label ID="Label7" runat="server" CssClass="txtCampoExclude1">GROSS RECEIPT TAX</asp:Label>
                                                            <asp:TextBox ID="txtGrossTax" AutoPostBack="True" runat="server" CssClass="form-controlWhite" MaxLength="3"
                                                                TabIndex="2003"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    </tr>
                                                    <tr><td>
                                                    <div>
                                                        <asp:Label ID="lbFireDamage" runat="server" CssClass="txtFireDamage">Fire Damage</asp:Label>
                                                        <asp:TextBox ID="txtFireDamage" AutoPostBack="True" runat="server" CssClass="form-controlWhite" MaxLength="3"
                                                            TabIndex="2003">
                                                        </asp:TextBox>
                                                    </div>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                    <td><div><asp:Label ID="Label10" runat="server" CssClass="txtCampoExclude1">TOTAL PREMIUM</asp:Label>
                                                            <asp:TextBox ID="txtTotalPremiumResult" AutoPostBack="True" runat="server" CssClass="form-controlWhite" MaxLength="3"
                                                            TabIndex="2003"></asp:TextBox>
                                                    </div></td>
                                                    </tr>
                                                    </table>
                                                </div>
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

                                                <asp:Label ID="Label231" runat="server" Font-Bold="True" ForeColor="Red" Text="MODIFY DRIVER: ON"
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
                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="Accordion2" runat="Server" AutoSize="None" CssClass="accordion"
                                FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                            </Toolkit:Accordion>
                          <div class="col-sm-12" align="center">
                           <br />
                            <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                            <br />

                            <cc1:Mirror id="Mirror2" ControlID="btnReinstallation" runat="server" />
                            <cc1:Mirror id="Mirror3" ControlID="btnDelete" runat="server" />
                            <cc1:Mirror id="Mirror4" ControlID="btnAdd2" runat="server" />
                            <cc1:Mirror id="Mirror5" ControlID="btnRenew" runat="server" />
                            <cc1:Mirror id="Mirror6" ControlID="btnCancellation" runat="server" />
                            <cc1:Mirror id="Mirror7" ControlID="btnAcceptQuote" runat="server" />
                            <cc1:Mirror id="Mirror8" ControlID="btnEdit" runat="server" />
                            <cc1:Mirror id="Mirror9" ControlID="BtnSave" runat="server" />
                            <cc1:Mirror id="Mirror10" ControlID="btnPrintInvoice" runat="server" />
                            <cc1:Mirror id="Mirror12" ControlID="btnPrintPolicy" runat="server" />
                            <cc1:Mirror id="Mirror14" ControlID="btnInvoice" runat="server" />
                            <cc1:Mirror id="Mirror13" ControlID="btnPreview" runat="server" />
                            <cc1:Mirror id="Mirror15" ControlID="btnAdjuntar" runat="server" />
                            <cc1:Mirror id="Mirror11" ControlID="btnCancel" runat="server" />
                            <cc1:Mirror id="Mirror1" ControlID="BtnExit" runat="server" />
                           <br /><br /><br />
                        </div>
                        </div>
                        <div class="row formWraper" runat="server" id="PaymentInfo" style="padding: 0px;">
                            <Toolkit:Accordion ID="Accordion3" runat="Server" AutoSize="None" CssClass="accordion"
                                FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250"
                                Visible="False">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane4" runat="server">
                                        <Header>
                                            PAYMENT INFORMATION
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:Label ID="lblPrefPayment" runat="server" CssClass="labelForControl">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                Which is your preffered payment method:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </asp:Label>
                                                <br />
                                                <br />
                                                <br />
                                                <br />
                                            </div>
                                            <div class="col-sm-3">
                                                <br />
                                                <br />
                                                <br />
                                                <asp:TextBox ID="txtIDRoadAssist" runat="server" Visible="False" Width="20px"></asp:TextBox>
                                                <td align="center" colspan="15">
                                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                        DisplayAfter="10">
                                                        <ProgressTemplate>
                                                            <img alt="" src="Images2/loader.gif" style="width: 35px; height: 35px" />
                                                            <span><span class="style5">&nbsp;&nbsp;</span><span class="style6">Please wait...</span></span></ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                    <asp:Label ID="LblTotalCases" runat="server" CssClass="headform3" Font-Names="Arial"
                                                        Font-Size="9pt" Height="3px" Visible="False" Width="194px">Quotes for Endorsement</asp:Label>
                                            </div>
                                            </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                       

  
                         </div>

                    </div>
                </div>

                               <!--#region UPLOAD DOCUMENTS  -->
                    <div style="font-weight: 700">

                       <asp:Panel ID="pnlAdjunto" runat="server" BackColor="#F4F4F4" CssClass="" Width="750px">
                        <div class="accordion" style="padding: 0px;
                        font-size: 14px; font-weight: normal; font-style: normal; background-repeat: no-repeat;
                        text-align: left; vertical-align: bottom; background-color: #808080;">
                               &nbsp;&nbsp;&nbsp;<asp:Label ID="Label35" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Gotham Book" Font-Size="14pt" ForeColor="White" Text="Attach Documents" />
                           </div>
                           <div class="">
                               <table class="MainMenu" style="width: 78%; margin-right: 0px; height: 276px;">
                                   <tr>
                                       <td align="left">
                                           <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="Gotham Book">Line of Business:</asp:Label>
                                       </td>
                                       <td align="left">
                                                <asp:DropDownList ID="ddlPolicyClass" runat="server" 
                                                    CssClass="form-controlWhite" Width="350px" Height="31px" AutoPostBack="True" EnableViewState="true" 
                                                    onselectedindexchanged="ddlPolicyClass_SelectedIndexChanged"></asp:DropDownList>
                                           &nbsp;
                                       </td>
                                   </tr>
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
                    DropShadow="True" PopupControlID="pnlAdjunto" TargetControlID="btnDummyDoc" OkControlID="btnAceptar">
                </asp:ModalPopupExtender>
                <asp:Button ID="btnDummyDoc" runat="server" Visible="true" BackColor="Transparent" BorderStyle="None"
                    BorderWidth="0" BorderColor="Transparent" />
                     <asp:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server"
                            TargetControlID="pnlAdjunto"
                            Radius="10"
                            Corners="All" />
                 <!--#endregion UPLOAD DOCUMENTS -->

                <asp:Panel ID="pnlMessage" runat="server" CssClass="" Width="450px" BackColor="#F4F4F4"
                    Height="260px">
                    <div class="" style="padding: 0px; background-color: #17529B; color: #FFFFFF; font-size: 14px;
                        font-weight: normal; background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                        &nbsp;&nbsp;
                        <asp:Label ID="Label3" runat="server" Font-Size="14pt" Text="Message..." ForeColor="White" />
                    </div>
                    <div style="padding: 0px;">
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

</body>
