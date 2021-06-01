<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Yacht.aspx.cs" Inherits="Yacht" %>
<%@ Register TagPrefix="cc1" Namespace="MirrorControl" Assembly="MirrorControl" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" />
<head id="Head2" runat="server">
    <meta charset="utf-8" >
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="apple-touch-icon" href="apple-touch-icon.png">
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="icon" href="Images2\LogoGuardian.ico" type="image/x-icon" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.4/jquery.min.js"></script> 

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
        jQuery(function ($) {
            $("#AccordionPane5_content_txtHomePhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane5_content_txtWorkPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane5_content_txtCellular").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //            $("#AccordionPane1_content_txtEffectiveDate").mask("00/00/0000", { placeholder: "__/__/____" });
            //            $("#AccordionPane1_content_txtExpirationDate").mask("00/00/0000", { placeholder: "__/__/____" });
        });
    </script>

    <script type='text/javascript'>
        function onlyNumbers(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;


            return true;


        }
    </script>

    <script type = "text/javascript">
        window.onload = function () {
            var scrollY = parseInt('<%=Request.Form["scrollY"] %>');
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
    
</head>

<body>



    <form id="Form1" method="post" runat="server">
    <div class="container-fluid" style="height: 100%">
        <Toolkit:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnableScriptGlobalization="True"
            AsyncPostBackTimeout="0">
        </Toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                <div class="row row-offcanvas row-offcanvas-left" style="height: 100%">
                    <div class="col-sm-3 col-md-2 sidebar-offcanvas" id="Div1" role="navigation">
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
                            Yacht</h1>
                        <div class="form=group" align="center">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnSave_Click"
                                TabIndex="86" Text="SAVE" Style="margin-left: 10px;" Width="270px"/> <%--UseSubmitBehavior="false"--%> 
                            <asp:Button ID="btnModify" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnModify_Click"
                                TabIndex="87" Text="MODIFY" Style="margin-left: 10px;" Width="270px" Visible="false" Enabled="false"/>
                            <asp:DropDownList ID="ddlPrintOptions" TabIndex="1" runat="server" Width="320px" Visible="false" Enabled="false"
                                OnSelectedIndexChanged="ddlPrintOptions_SelectedIndexChanged" CssClass="btn btn-primary btn-lg" Height="42px" AutoPostBack="true">
                                <asp:ListItem>PRINT OPTIONS</asp:ListItem>
                                <asp:ListItem>PRINT DEC PAGE & ENDORS</asp:ListItem>
                                <asp:ListItem>PRINT POLICY FORMS</asp:ListItem>
                                <asp:ListItem>PRINT CERTIFICATE</asp:ListItem>
                                <asp:ListItem>PRINT BANK CERTIFICATE</asp:ListItem>
                                <asp:ListItem>PRINT PORT CERTIFICATE</asp:ListItem>
                                <asp:ListItem>PRINT PORT CERT ADD INS</asp:ListItem>
                                <asp:ListItem>PRINT PORT ENDORSEMENT</asp:ListItem>
                            </asp:DropDownList>
                            <%--<asp:Button ID="btnPrintPolicy" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPrintPolicy_Click"
                                TabIndex="88" Text="PRINT POLICY" Style="margin-left: 10px;" Width="270px" Visible="false" Enabled="false"/>
                            <asp:Button ID="btnPrintCertificate" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPrintCertificate_Click"
                                TabIndex="89" Text="PRINT CERTIFICATE" Style="margin-left: 10px;" Width="270px" Visible="false" Enabled="false"/>
                            <asp:Button ID="btnPrintCertificateBank" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPrintCertificateBank_Click"
                                TabIndex="90" Text="PRINT BANK CERTIFICATE" Style="margin-left: 10px;" Width="270px" Visible="false" Enabled="false"/>
                            <asp:Button ID="btnPrintCertificateMarina" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPrintCertificateMarina_Click"
                                TabIndex="91" Text="PRINT PORT CERTIFICATE" Style="margin-left: 10px;" Width="270px" Visible="false" Enabled="false" />
                            <asp:Button ID="btnPrintPortEndorsement" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPrintPortEndorsement_Click"
                                TabIndex="92" Text="PRINT PORT ENDORSEMENT" Style="margin-left: 10px;" Width="285px" Visible="false" Enabled="false"/>--%>
                            <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPreview_Click" Visible="false" Enabled="false"
                                TabIndex="93" Text="PRINT WORKSHEET" Style="margin-left: 10px;" Width="270px" />
                            <asp:Button ID="btnPreview2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPreview2_Click" Visible="false" Enabled="false"
                                TabIndex="94" Text="PRINT CLIENT QUOTE" Style="margin-left: 10px;" Width="270px" />
                            <asp:Button ID="btnPreviewPolicy" runat="server" CssClass="btn btn-primary btn-lg" 
                                OnClick="btnPreviewPolicy_Click" TabIndex="9000" Text="PREVIEW" 
                                Visible="False" Width="220px" />
                            <asp:Button ID="btnConvert" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnConvert_Click"
                                TabIndex="95" Text="CONVERT" Style="margin-left: 10px;" Width="270px" Visible="false" Enabled="false"/>
                             <asp:Button ID="btnAcceptQuote" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnAcceptQuote_Click"
                                TabIndex="96" Text="ACCEPT QUOTE" Style="margin-left: 10px;" Width="270px" Visible="false" Enabled="false"/>
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnCancel_Click"
                                TabIndex="97" Text="CANCEL"  Style="margin-left: 10px;" Width="270px" />
                            <asp:Button ID="btnPremiumFinance" runat="server" 
                                CssClass="btn btn-primary btn-lg" Width="270px" Style="margin-left: 10px;"
                                OnClick="btnPremiumFinance_Click" TabIndex="98" Text="PREMIUM FINANCE" 
                                Visible="False" />
                            <asp:Button ID="btnPremiumFinance2" runat="server" Height="42px"
                                CssClass="btn btn-primary btn-lg" Width="320px" Style="margin-left: 10px;"
                                OnClick="btnPremiumFinance_Click" TabIndex="98" Text="PREMIUM FINANCE" 
                                Visible="False" />
                            <asp:Button ID="btnSentToPPS" runat="server" CssClass="btn btn-primary btn-lg" 
                                OnClick="btnSentToPPS_Click" TabIndex="9000" Text="Send To PPS" 
                                Visible="False" Width="220px" />
                            <asp:Button ID="btnModify2" runat="server" CssClass="btn btn-primary btn-lg" 
                                OnClick="btnModify2_Click" TabIndex="9000" Text="MODIFY" 
                                Visible="False" Width="320px" Height="42px"/>
                            <asp:Button ID="btnSave2" runat="server" CssClass="btn btn-primary btn-lg" 
                                OnClick="btnSave2_Click" TabIndex="9000" Text="SAVE" 
                                Visible="False" Width="220px"/>		
                            <asp:Button ID="btnEndor" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnEndor_Click" Visible="false" Enabled="false"
                                Text="NEW ENDORSEMENT" ToolTip="New Endorsement" Width="320px" Height="42px"/>
                                <asp:Button ID="BtnExitEndorsement" runat="server" CssClass="btn btn-primary btn-lg" OnClick="BtnExitEndorsement_Click" TabIndex="72" Text="EXIT" Visible="False" Width="220px" />
                            <br />
                            <br />
                            <div align="left">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Gray">Control #.:</asp:Label>
                                <asp:Label ID="LblControlNo" runat="server" CssClass="" Font-Bold="True" ForeColor="Gray"> No.:</asp:Label>
                            </div>
                        </div>
                           
                        <div class="form-group" align="center">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="10">
                                <ProgressTemplate>
                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                    <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span></ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>

                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="Accordion4" runat="Server" AutoSize="None" CssClass="accordion"
                                HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head"
                                ContentCssClass="accordion-body" RequireOpenedPane="false" SelectedIndex="0"
                                SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane5" runat="server">
                                        <Header>
                                            CUSTOMER INFORMATION
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-lg-12">
                                            <center>
                                                <asp:CheckBox ID="chkIsRenew" runat="server" Font-Bold="false" AutoPostBack="True"
                                                    CssClass="labelForControl" OnCheckedChanged="chkIsRenew_CheckedChanged"
                                                    Text="Yacht Renewal?" />

                                                     <asp:CheckBox ID="chkIsCommercial" runat="server" Font-Bold="false" AutoPostBack="True"
                                                    CssClass="labelForControl"
                                                    Text="Is Commercial?" />
                                                    </center>
                                                    </div>
                                                    

                                            <div class="col-sm-2">
                                                <br />
                                                <div id="DivRenew" runat="server" visible="false">
                                                <asp:Label ID="lblPolicyNoToRenew" runat="server" CssClass="labelForControl" Visible="false">PPS Policy to Renew:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPolicyToRenewType" runat="server" TabIndex="2000" Visible="false" Enabled="false" CssClass="form-controlWhite"
                                                    Width="45px">MAR</asp:TextBox>
                                                <asp:TextBox ID="txtPolicyNoToRenew" runat="server" CssClass="form-controlWhite"
                                                TabIndex="1" Width="68px" Enabled="false" Visible="false"></asp:TextBox>
                                                <asp:TextBox ID="txtPolicyNoToRenewSuffix" runat="server" TabIndex="1" Visible="false" Enabled="false" CssClass="form-controlWhite"
                                                    Width="45px" MaxLength="2"></asp:TextBox>
                                                <br />
                                                <asp:Button ID="btnVerifyYachtInPPS" runat="server" Visible="false" Enabled="false" class="btn btn-primary btn-xs" OnClick="btnVerifyYachtInPPS_Click" Text="Click to Verify" />
                                                <br />
                                                <br />
                                                </div>
                                                <asp:Label ID="lblCustomerNo" runat="server" CssClass="labelForControl">Customer No.:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtCustomerNo" runat="server" CssClass="form-controlWhite" MaxLength="10"
                                                    TabIndex="1" Width="200px" Enabled="false"></asp:TextBox>
                                                <br />
                                                <br />

                                                <asp:Label ID="lblFirstName" runat="server" CssClass="labelForControl" ForeColor="Red">First Name:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtFirstName" Style="" runat="server" CssClass="form-controlWhite"
                                                    TabIndex="2" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblInitial" runat="server" CssClass="labelForControl" EnableViewState="False">Initial:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtInitial" Style="" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="1" TabIndex="3" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblLastName" runat="server" CssClass="labelForControl" ForeColor="Red">Last Name:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtLastName" Style="" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="100" TabIndex="4" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblSSN" runat="server" CssClass="labelForControl">Social Security No:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtSSN" Style="" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="9" TabIndex="4" Width="200px" onkeypress="return onlyNumbers(event);" ></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtSSN"
                                                    Mask="999-99-9999"  ClearMaskOnLostFocus="false"
                                                    InputDirection="RightToLeft">
                                                    </asp:MaskedEditExtender>

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <br />
                                                <asp:Label ID="lblCompanyName" runat="server" CssClass="labelForControl">Company Name:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtCompanyName" Style="" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                    MaxLength="500" TabIndex="5" Width="200px" Enabled="False"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblHomePhone" runat="server" CssClass="labelForControl">Home Phone:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtHomePhone" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                    MaxLength="20" TabIndex="6" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblWorkPhone" runat="server" CssClass="labelForControl">Work Phone:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtWorkPhone" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                    MaxLength="20" TabIndex="7" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblCellular" runat="server" CssClass="labelForControl">Cellular</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtCellular" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                    MaxLength="20" TabIndex="8" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblemail" runat="server" CssClass="labelForControl" ForeColor="Red">E-mail</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtemail" runat="server" CssClass="form-controlWhite" MaxLength="100"
                                                    Style="" TabIndex="9" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <div style="text-align: center;">
                                                    <asp:Label ID="Label18" runat="server" Font-Bold="True" CssClass="labelForControl"
                                                        Text="Mailing Address"></asp:Label>
                                                </div>
                                                <asp:Label ID="lbladdress1" runat="server" CssClass="labelForControl" ForeColor="Red">Address 1</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtAddrs1" Style="" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="30" TabIndex="10"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lbladdress2" runat="server" CssClass="labelForControl">Address 2</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtAddrs2" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    Style="" TabIndex="11"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblZipCode" runat="server" CssClass="labelForControl" ForeColor="Red">Zip Code</asp:Label>
                                                <br />


                                                <asp:TextBox ID="txtZip" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    Style="" TabIndex="12"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblCity" runat="server" CssClass="labelForControl" ForeColor="Red">City</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    Style="" TabIndex="13"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblState" runat="server" CssClass="labelForControl">State</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtState" runat="server" CssClass="form-controlWhite" MaxLength="2"
                                                    Style="" TabIndex="14"></asp:TextBox>
                                                <br />
                                                <br />
                                            </div>
                                            <div class="col-sm-1">
                                                <br />
                                                <asp:CheckBox ID="chkSameMailing" runat="server" Font-Bold="false" AutoPostBack="True"
                                                     CssClass="labelForControl" TabIndex="15" OnCheckedChanged="chkSameMailing_CheckedChanged"
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
                                                    TabIndex="16" Style="" AutoPostBack="True" ></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lbladdress9" runat="server" CssClass="labelForControl">Address 2</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPhyAddress2" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    TabIndex="17" Style="" AutoPostBack="True" ></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblZipCode2" runat="server" CssClass="labelForControl" ForeColor="Red">Zip Code</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPhyZipCode" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    TabIndex="18" Style=""></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblCity4" runat="server" CssClass="labelForControl" ForeColor="Red">City</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPhyCity" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    TabIndex="19" Style=""></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblState2" runat="server" CssClass="labelForControl">State</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPhyState" runat="server" CssClass="form-controlWhite" MaxLength="2"
                                                    Style="" TabIndex="20" AutoPostBack="True" ></asp:TextBox>
                                            </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>

                                       <%-- POLICY DETAILS ACCORDION --%>
                            <br />
                            <div id="PolicySectionDiv" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="MyAccordion" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40"
                                    HeaderCssClass="accordion-head" ContentCssClass="accordion-body" RequireOpenedPane="false"
                                    SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane1" runat="server">
                                            <Header>
                                                YACHT DETAILS
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>

                                                <%-- YACHT DETAILS DIVISION --%>
                                                <div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:Label ID="lblEntryDate" runat="server" CssClass="labelForControl">Entry
                                                            Date</asp:Label>
                                                        <br />

                                                        <asp:TextBox ID="txtEntryDate" runat="server" Columns="30"
                                                            CssClass="form-controlWhite" Enabled="false" IsDate="True"
                                                            TabIndex="21" Width="250"></asp:TextBox>
                                                        <br />
                                                        <br />

                                                        <asp:Label ID="lblEffectiveDate" runat="server" CssClass="labelForControl"
                                                            ForeColor="Red">Effective Date</asp:Label>
                                                        <br />


<%--                                                        <asp:TextBox ID="txtEffectiveDate" runat="server" Columns="30" CssClass="form-controlWhite"
                                                            IsDate="True" TabIndex="22" Width="250" OnTextChanged="txtEffectiveDate_TextChanged"></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="form-controlWhite" IsDate="True"
                                                                TabIndex="22" Width="250px" Enabled="false"></asp:TextBox>
                                                         
                                                            <asp:ImageButton ID="imgCalendarEffectiveDate" runat="server" Height="16px" 
                                                                ImageUrl="~/Images2/Calendar.png" Width="16px" onclick="imgCalendarEffectiveDate_Click" />

                                                            <asp:Calendar ID="Calendar3" runat="server" BackColor="#ffffff" BorderColor="#e2e0e0"  
                                                            BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"  
                                                            ForeColor="#000000" ShowGridLines="True"  OnSelectionChanged="Calendar3_SelectionChanged" Visible="False">   
                                                            <SelectedDayStyle BackColor="#17529b" Font-Bold="True" />  
                                                            <SelectorStyle BackColor="#ffffff" />  
                                                            <OtherMonthDayStyle ForeColor="#000000" />  
                                                            <NextPrevStyle Font-Size="9pt" ForeColor="#ffffff" />  
                                                            <DayHeaderStyle BackColor="#ffffff" Font-Bold="True" Height="1px" ForeColor="#000000"/>  
                                                            <TitleStyle BackColor="#9b9898" Font-Bold="True" Font-Size="9pt" ForeColor="#ffffff" />  
                                                        </asp:Calendar> 
                                                        <br />
                                                        <br />
                                                         <asp:Label ID="lblAgency" runat="server" CssClass="labelForControl" ForeColor="Red">Agency</asp:Label>

                                                        <br />

                                                        <asp:DropDownList ID="ddlAgency" runat="server" CssClass="form-controlWhite"
                                                             TabIndex="28" Width="250px">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <br />
                                                        
                                                        <asp:Label ID="lblInsuranceCompany" runat="server" CssClass="labelForControl">Ins. Company</asp:Label>

                                                        <br />

                                                        <asp:DropDownList ID="ddlInsuranceCompany" runat="server" CssClass="form-controlWhite"
                                                             TabIndex="24" Width="250px">
                                                        </asp:DropDownList>

                                                        <br />
                                                        <br />

<%--                                                        <asp:Label ID="lblPolicyNo" runat="server" CssClass="labelForControl">Policy Number</asp:Label>
                                                        <br />


                                                        <asp:TextBox ID="txtPolicyNo" runat="server" Columns="30" CssClass="form-controlWhite"
                                                            TabIndex="25" Width="250" Enabled="false"></asp:TextBox>--%>
                                                    <asp:Label ID="lblPolicyNumber" runat="server" Text="Policy Number" CssClass="labelForControl"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPolicyType" runat="server" TabIndex="2000" Enabled="false" CssClass="form-controlWhite"
                                                        Width="65px">MAR</asp:TextBox>
                                                    <asp:TextBox ID="txtPolicyNo" runat="server" TabIndex="2001" Enabled="false" CssClass="form-controlWhite"
                                                        Width="100px"></asp:TextBox>
                                                    <asp:TextBox ID="txtSuffix" runat="server" TabIndex="2002" Enabled="false" CssClass="form-controlWhite"
                                                        Width="50px"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblRenewalNumber" runat="server" Text="Renewal of" CssClass="labelForControl" Visible="false"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtRenewalType" runat="server" TabIndex="2000" CssClass="form-controlWhite"
                                                        Width="65px" Visible="false" Enabled="false">MAR</asp:TextBox>
                                                    <asp:TextBox ID="txtPolicyNoRewnewal" runat="server" TabIndex="2001" CssClass="form-controlWhite"
                                                        Width="100px" Visible="false" Enabled="false"></asp:TextBox>
                                                    <asp:TextBox ID="txtSuffixRenewal" runat="server" TabIndex="2002" CssClass="form-controlWhite"
                                                        Width="50px" Visible="false" Enabled="false"></asp:TextBox>
                                                    <br />
                                                    <br />

                                                    </div>

                                                    <div class="col-sm-1">
                                                     </div>

                                                    <div class="col-sm-5">

                                                    <asp:Label ID="lblExpirationDate" runat="server" CssClass="labelForControl"
                                                            ForeColor="Red">Expiration Date</asp:Label>

                                                        <br />
                                                        <asp:TextBox ID="txtExpirationDate" runat="server" Columns="30" CssClass="form-controlWhite"
                                                            TabIndex="26" Width="250" Enabled="false"></asp:TextBox>


                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblTerm" runat="server" CssClass="labelForControl"
                                                            ForeColor="Red">Term</asp:Label>
                                                        <br />


                                                        <asp:TextBox ID="txtTerm" runat="server" CssClass="form-controlWhite"
                                                            ISDATE="False" MaxLength="2" TabIndex="27" AutoPostBack="True" Width="250" Enabled="false"
                                                           ></asp:TextBox>

                                                        <br />
                                                        <br />  
<%--                                                        <asp:Label ID="lblSelectedAgent" runat="server" CssClass="labelForControl">Agent</asp:Label>
                                                        <br />
                                                         <asp:DropDownList ID="ddlAgent" runat="server" CssClass="form-controlWhite" TabIndex="23"
                                                             Width="250px">
                                                         </asp:DropDownList>
                                                       
                                                        <br />
                                                        <br />--%>
                                                        <asp:Label ID="lblProducer" runat="server" CssClass="labelForControl"
                                                           >Producer</asp:Label>
                                                        <br />

                                                        <asp:TextBox ID="txtProducer" runat="server" CssClass="form-controlWhite"
                                                            ISDATE="False" MaxLength="500" TabIndex="29" Width="250"
                                                           ></asp:TextBox>

                                                        <br />
                                                        <br />

                                                    <asp:Label ID="lblBank" CssClass="labelForControl" runat="server" Text="Loss Payee"></asp:Label>
                                                    <br />
                                                    <asp:Button ID="btnBankList" runat="server" Text="SELECT" CssClass="btn btn-primary btn-lg" OnClick="btnBankList_Click" Enabled="false" TabIndex="30"></asp:Button>
                                                    <br />
                                                    <asp:Label ID="lblBankListSelected2" runat="server"></asp:Label>

                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                </div>

                                                <div class="form-group" align="center">
                                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server"
                                                        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="10">
                                                        <ProgressTemplate>
                                                            <img alt="" src="Images2/loader.gif" style="width: 35px; height: 35px" />
                                                            <span><span class="style5"></span><span class="style6">Please
                                                                    wait...</span></span>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </div>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>
                            <%-- END POLICY DETAILS ACCORDION --%>

                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="Accordion1" runat="Server" AutoSize="None" CssClass="accordion"
                                FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane2" runat="server" TabIndex="1999">
                                        <Header>
                                            YACHT INFORMATION
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Label ID="lblBoatName" runat="server" CssClass="labelForControl" ForeColor="Red">Boat Name:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtBoatName" runat="server" CssClass="form-controlWhite" 
                                                    TabIndex="31" Width="250"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="LabelBoatBuilder" runat="server" CssClass="labelForControl" ForeColor="Red">Boat Builder:</asp:Label>
                                                <br />
                                                 <asp:TextBox ID="txtBoatBuilder" runat="server" CssClass="form-controlWhite" 
                                                    TabIndex="32" Width="250"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="LabelBoatModel" runat="server" CssClass="labelForControl" ForeColor="Red">Boat Model:</asp:Label>
                                                <br />
                                                 <asp:TextBox ID="txtBoatModel" runat="server" CssClass="form-controlWhite" 
                                                    TabIndex="33" Width="250"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblBoatYear" runat="server" CssClass="labelForControl" ForeColor="Red">Year:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtBoatYear" runat="server" CssClass="form-controlWhite" TabIndex="34" Width="250"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblNavigationLimit" runat="server" CssClass="labelForControl" ForeColor="Red">Navigation Limit:</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlNavigationLimit" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                        TabIndex="35" Width="250px">
                                              
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <br />
                                                <asp:Button ID="btnAddNavigationLimit" runat="server" CssClass="btn btn-primary btn-lg"
                                                TabIndex="36" Text="ADD" Width="150px" onclick="btnAddNavigationLimit_Click"/>
                                                <br />
                                                <br />
                                                <asp:GridView ID="gridViewNavigationLimit" runat="server" OnRowDataBound="gridViewNavigationLimit_RowDataBound" AutoGenerateColumns="false"
                                                    OnRowCommand="gridViewNavigationLimit_RowCommand" onrowdeleting="gridViewNavigationLimit_RowDeleting" AlternatingItemStyle-BackColor="#FEFBF6" 
                                                    AlternatingItemStyle-CssClass="HeadForm3" BackColor="White"
                                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Names="Arial"
                                                    Font-Size="10pt" HeaderStyle-BackColor="#5C8BAE" HeaderStyle-CssClass="HeadForm2"
                                                    HeaderStyle-HorizontalAlign="Center" Height="60px" ItemStyle-CssClass="HeadForm3"
                                                    ItemStyle-HorizontalAlign="center"  Width="100%" GridLines="Both">
                                                    <HeaderStyle HorizontalAlign="Center" Height="10px" ForeColor="White" CssClass="HeadForm2" BackColor="#17529B"></HeaderStyle>
                                                    <RowStyle HorizontalAlign="Center"/>
                                                    <AlternatingRowStyle BackColor="#CFDBE7" />
                                                    <Columns>
                                                        <asp:BoundField DataField="No" HeaderText="No."/>
                                                        <asp:BoundField DataField="NavigationLimitID" HeaderText="NavigationLimitID"/>
                                                        <asp:BoundField DataField="TaskControlID" HeaderText="TaskControlID"/>
                                                        <asp:BoundField DataField="NavigationLimitDesc" HeaderText="Navigation Limit">
                                                             <ItemStyle Width="150px" />
                                                        </asp:BoundField>
                                                        <asp:buttonfield buttontype="Link" commandname="Modify" text="Modify"/>
                                                        <asp:buttonfield buttontype="Link" commandname="Delete" text="Delete"/>
                                                        </Columns>

                                                    </asp:GridView> 
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Label ID="lblLOA" runat="server" Visible="true" CssClass="labelForControl" ForeColor="Red">LOA:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtLOA" runat="server" CssClass="form-controlWhite" 
                                                   TabIndex="37" Width="250" onkeypress="return onlyNumbers(event);" AutoPostBack="true"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblHullNumber" runat="server" CssClass="labelForControl">Hull Number Registration:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtHullNumber" runat="server" CssClass="form-controlWhite" 
                                                    TabIndex="38" Width="250"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblHomeport" runat="server" CssClass="labelForControl">Homeport:</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlHomeport" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                        TabIndex="39" Width="250px" OnSelectedIndexChanged="ddlHomeport_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblHomeportAddress" runat="server" CssClass="labelForControl">Homeport Address:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtHomeportAddress" runat="server" CssClass="form-controlWhite" 
                                                    TabIndex="40" Width="250" Enabled = "false"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblHomeportLocation" runat="server" CssClass="labelForControl">Homeport Location:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtHomeportLocation" runat="server" CssClass="form-controlWhite" 
                                                    TabIndex="40" Width="250" Enabled = "false"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblHullLimit" runat="server" Visible="true" CssClass="labelForControl">Hull Limit:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtHullLimit" runat="server" CssClass="form-controlWhite" 
                                                   TabIndex="41" Width="250" OnTextChanged="txtHullLimit_TextChanged" AutoPostBack="True" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                                                
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                             <div class="col-sm-3">
                                            
                                             <asp:Label ID="lblEngine" runat="server" CssClass="labelForControl">Engine:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtEngine" runat="server" CssClass="form-controlWhite" 
                                                    TabIndex="42" Width="250" Enabled = "false"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblEngineSerialNumber" runat="server" CssClass="labelForControl">Engine Serial Number:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtEngineSerialNumber" runat="server" CssClass="form-controlWhite" 
                                                    TabIndex="43" Width="250" Enabled = "false"></asp:TextBox>
                                                 <br />
                                                <br />
                                            <asp:Label ID="lblTenderDesc" runat="server" CssClass="labelForControl">Tender Description:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtTenderDesc" runat="server" CssClass="form-controlWhite" 
                                                    TabIndex="44" Width="250"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblTenderSerial" runat="server" CssClass="labelForControl">Tender Serial Number:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtTenderSerial" runat="server" CssClass="form-controlWhite" 
                                                    TabIndex="45" Width="250"></asp:TextBox>
                                                <br />
                                                <br />
                                                
                                             <asp:Label ID="lblTenderLimit" runat="server" CssClass="labelForControl">Tender Limit:</asp:Label>
                                                <br />
                                                 <asp:TextBox ID="txtTenderLimit" runat="server" CssClass="form-controlWhite" 
                                                    TabIndex="46" Width="250" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                                                <br />
                                                <br />
                                                <br />
                                                <asp:Button ID="btnAddRows" runat="server" CssClass="btn btn-primary btn-lg"
                                                TabIndex="47" Text="ADD" Width="150px" onclick="btnAddRows_Click" />
                                                <br />
                                                <br />
                                                
                                                    <asp:GridView ID="gridViewTenderLimit" runat="server" OnRowDataBound="gridViewTenderLimit_RowDataBound" AutoGenerateColumns="false"
                                                                  OnRowCommand="gridViewTenderLimit_RowCommand" onrowdeleting="gridViewTenderLimit_RowDeleting" AlternatingItemStyle-BackColor="#FEFBF6" 
                                                                   AlternatingItemStyle-CssClass="HeadForm3" BackColor="White"
                                                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Names="Arial"
                                                                    Font-Size="10pt" HeaderStyle-BackColor="#5C8BAE" HeaderStyle-CssClass="HeadForm2"
                                                                    HeaderStyle-HorizontalAlign="Center" Height="60px" ItemStyle-CssClass="HeadForm3"
                                                                    ItemStyle-HorizontalAlign="center"  Width="100%" GridLines="Both">
                                                                    <HeaderStyle HorizontalAlign="Center" Height="10px" ForeColor="White" CssClass="HeadForm2" BackColor="#17529B"></HeaderStyle>
                                                                    <RowStyle HorizontalAlign="Center"/>
                                                                    <AlternatingRowStyle BackColor="#CFDBE7" />
                                                    <Columns>
                                                        <asp:BoundField DataField="No" HeaderText="No."/>
                                                        <asp:BoundField DataField="TaskControlID" HeaderText="TaskControlID"/>
                                                        <asp:BoundField DataField="TenderLimitAmount" HeaderText="Limit">
                                                             <ItemStyle Width="90px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TenderDesc" HeaderText="Description"/>
                                                        <asp:BoundField DataField="TenderSerial" HeaderText="Serial Number"/>
                                                        <asp:buttonfield buttontype="Link" commandname="Modify" text="Modify"/>
                                                        <asp:buttonfield buttontype="Link" commandname="Delete" text="Delete"/>
                                                    </Columns>

                                                    </asp:GridView>
                                                    <asp:HiddenField ID="txtGridTenderLimit" runat="server"></asp:HiddenField>
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
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane3" runat="server">
                                        <Header>
                                            LIMITS & DEDUCTIBLES
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                         <Content>
                                               <div>
                                                    <div class="col-lg-12">
                                                        <asp:Label ID="lblWaterCraftLimitHeader" runat="server" Font-Bold="True" CssClass="labelForControl"
                                                            Text="Watercraft Limit"></asp:Label>
                                                    </div>
                                                   <div class="col-sm-1">
                                                   <br />
                                                   <asp:Label ID="lblOption1" runat="server" CssClass="labelForControl" Font-Bold="True" Width="100px">Option 1:</asp:Label>
                                                   <br />
                                                   <br />
                                                    <asp:Label ID="lblDeductibles1" runat="server" CssClass="labelForControl">Deductibles:</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlDeductibles1" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                        TabIndex="48" Width="100px" OnSelectedIndexChanged="ddlDeductibles1_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblOption2" runat="server" CssClass="labelForControl" Font-Bold="True" Width="100px">Option 2:</asp:Label>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblDeductibles2" runat="server" CssClass="labelForControl">Deductibles:</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlDeductibles2" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                        TabIndex="52" Width="100px" OnSelectedIndexChanged="ddlDeductibles2_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <div style="display:none">
                                                    <asp:Label ID="lblDeductibleCalculatedTender1" runat="server" CssClass="labelForControl" Visible="False">None</asp:Label>
                                                    <asp:Label ID="lblDeductibleCalculatedTender2" runat="server" CssClass="labelForControl" Visible="False">None</asp:Label>
                                                    </div>
                                                   </div>

                                                   <div class="col-sm-1">
                                                   </div>

                                                   <div class="col-sm-2">
                                                       <div style="margin-top:60px;">
                                                            <asp:Label ID="lblDeductibleAmount1" runat="server" CssClass="labelForControl">Deductible Amount:</asp:Label>
                                                       </div>
                                                       <div style="margin-top:10px;">
                                                       <asp:Label ID="lblDeductibleCalculated1" runat="server" CssClass="labelForControl">None</asp:Label>
                                                       </div>

                                                       <div style="margin-top:80px;">
                                                            <asp:Label ID="lblDeductibleAmount2" runat="server" CssClass="labelForControl">Deductible Amount:</asp:Label>
                                                       </div>

                                                       <div style="margin-top:10px;">
                                                            <asp:Label ID="lblDeductibleCalculated2" runat="server" CssClass="labelForControl">None</asp:Label>
                                                       </div>
                                                   </div>

                                                   <div class="col-sm-2">
                                                   <br />
                                                   <br />
                                                   <br />
                                                   <asp:Label ID="lblWatercraftLimit1" runat="server" CssClass="labelForControl">Watercraft Limit:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtWatercraftLimit1" Style="" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="49" Width="150px" Enabled="false"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblWatercraftLimit2" runat="server" CssClass="labelForControl">Watercraft Limit:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtWatercraftLimit2" Style="" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="53" Width="150px" Enabled="false"></asp:TextBox>

                                                   </div>

                                                   <div class="col-sm-1">
                                          
                                                   </div>

                                                   <div class="col-sm-2">
                                                   <br />
                                                   <br />
                                                   <br />
                                                    <asp:Label ID="lblRate1" runat="server" CssClass="labelForControl">Rate(%):</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtRate1" Style="" runat="server" CssClass="form-controlWhite"
                                                         TabIndex="50" Width="150px" OnTextChanged="txtRate1_TextChanged" AutoPostBack="True" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblRate2" runat="server" CssClass="labelForControl">Rate(%):</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtRate2" Style="" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="54" Width="150px" OnTextChanged="txtRate2_TextChanged" AutoPostBack="True" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                                                   </div>

                                                   <div class="col-sm-1">
                                          
                                                   </div>

                                                    <div class="col-sm-2">
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblWatercraftLimitTotal1" runat="server" CssClass="labelForControl">Watercraft Limit Total Premium:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtWatercraftLimitTotal1" Style="" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="51" Width="150px" Enabled="False"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                     <br />
                                                    <asp:Label ID="lblWatercraftLimitTotal2" runat="server" CssClass="labelForControl">Watercraft Limit Total Premium:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtWatercraftLimitTotal2" Style="" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="55" Width="150px" Enabled="False"></asp:TextBox>
                                                   </div>

                                                        <div class="col-sm-1">
                                                   </div>
                                              </div>
                                              

                                               <div class="col-lg-12">
                                               <hr style="border: 2px solid#D3D3D3;" />
                                               </div>

                                               <div>
                                                    <div class="col-lg-12">
                                                        <asp:Label ID="lblPIHeader" runat="server" Font-Bold="True" CssClass="labelForControl"
                                                            Text="Protection and Indemnity"></asp:Label>
                                                    </div>
                                              
                                                    <div class="col-sm-2">
                                                        <br />
                                                        <asp:Label ID="lblPI" runat="server" CssClass="labelForControl">Protection and Indemnity:</asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlPI" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                            TabIndex="56" Width="150px" OnSelectedIndexChanged="ddlPI_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="txtddlPI" runat="server"></asp:HiddenField>
                                                       </div>

                                                   <div class="col-sm-1">
                                          
                                                   </div>

                                                  <div class="col-sm-2">
                                                  <br />
                                                    <asp:Label ID="lblPILiabilityOnly" runat="server" CssClass="labelForControl">Protection and Indemnity</asp:Label>
                                                    <asp:Label ID="lblPILiabilityOnly2" runat="server" CssClass="labelForControl">Liability Only:</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlPILiabilityOnly" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                        TabIndex="57"  Width="150px" OnSelectedIndexChanged="ddlPILiabilityOnly_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="txtddlPILiabilityOnly" runat="server"></asp:HiddenField>
                                                  </div>

                                                  <div class="col-sm-1">
                                          
                                                   </div>

                                                  <div class="col-sm-2">
                                                    <br />
                                                    <asp:Label ID="lblOtherPI" runat="server" CssClass="labelForControl">Other Protection and Indemnity:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtOtherPI" runat="server" CssClass="form-controlWhite" 
                                                         TabIndex="58" Width="150px" onkeypress="return onlyNumbers(event);" OnTextChanged="txtOtherPI_TextChanged" AutoPostBack="True"></asp:TextBox>

                                                  </div>

                                                    <div class="col-sm-2">
                                          
                                                   </div>

                                                   <div class="col-sm-2">
                                                     <br />
                                                    <asp:Label ID="lblPI2" runat="server" CssClass="labelForControl">Protection and Indemnity</asp:Label>
                                                    <asp:Label ID="lblPIPremium" runat="server" CssClass="labelForControl">Premium:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPIPremium" Style="" runat="server" CssClass="form-controlWhite"
                                                         TabIndex="59" Width="150px" onkeypress="return onlyNumbers(event);" OnTextChanged="txtPIPremium_TextChanged" AutoPostBack="True" Enable="False"></asp:TextBox>

                                                  </div>
                                               </div>

                                               <div class="col-lg-12">
                                               <hr style="border: 2px solid#D3D3D3;" />
                                               </div>

                                               <div>
                                                  <div class="col-lg-12">
                                                    <asp:Label ID="lblMedicalPaymentHeader" runat="server" Font-Bold="True" CssClass="labelForControl"
                                                            Text="Medical Payments"></asp:Label>
                                                  </div>
                                                 <div class="col-sm-2">
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblMedicalPayment" runat="server" CssClass="labelForControl">Medical Payments:</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlMedicalPayment" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                        TabIndex="60" Width="150px" OnSelectedIndexChanged="ddlMedicalPayment_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="txtddlMedicalPayment" runat="server"></asp:HiddenField>
                                                 </div>

                                                 <div class="col-sm-1">
                                          
                                                 </div>

                                                 <div class="col-sm-2">
                                                 <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblOtherMedicalPayment" runat="server" CssClass="labelForControl">Other Medical Payments:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtOtherMedicalPayment" runat="server" CssClass="form-controlWhite" 
                                                         TabIndex="61" Width="150px" OnTextChanged="txtOtherMedicalPayment_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                 </div>

                                                 <div class="col-sm-1">
                                          
                                                 </div>

                                                 <div class="col-sm-2">
                                                 </div>

                                           

                                                 <div class="col-sm-2">
                                          
                                                   </div>
                                         
                                                 <div class="col-sm-2">
                                                 <br />
                                                 <br />
                                                 <br />
                                                     <asp:Label ID="lblMedicalPaymentPremium" runat="server" CssClass="labelForControl">Medical Payments Premium:</asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtMedicalPaymentPremiumTotal" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                            TabIndex="62" Width="150px" Enabled="false" onkeypress="return onlyNumbers(event);" OnTextChanged="txtMedicalPaymentPremiumTotal_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                 </div>

                                                 </div>
                                             

                                               <div class="col-lg-12">
                                               <hr style="border: 2px solid#D3D3D3;" />
                                               </div>

                                               <div>

                                                <div class="col-lg-12">
                                                    <asp:Label ID="lblPersonalEffectHeader" runat="server" Font-Bold="True" CssClass="labelForControl"
                                                        Text="Personal Effects"></asp:Label>
                                                </div>

                                                 <div class="col-sm-2">
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblPersonalEffect" runat="server" CssClass="labelForControl">Personal Effects:</asp:Label>
                                                    <br />
                                                   <asp:TextBox ID="txtPersonalEffect" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                        TabIndex="63" Width="150px" onkeypress="return onlyNumbers(event);" OnTextChanged="txtPersonalEffect_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                 </div>

                                                 <div class="col-sm-1">
                                          
                                                 </div>

                                                 <div class="col-sm-2">
                                                  <br />
                                                    <asp:Label ID="lblPersonalEffectDeductible" runat="server" CssClass="labelForControl">Personal Effects Deductible:</asp:Label>
                                                    
                                                    <asp:TextBox ID="txtPersonalEffectDeductible" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="64" Width="150px" onkeypress="return onlyNumbers(event);" 
                                                        OnTextChanged="txtPersonalEffectDeductible_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                 </div>

                                                 <div class="col-sm-1">
                                          
                                                 </div>

                                                 <div class="col-sm-2">
                                                 <br />
                                                 <br />
                                                    <asp:Label ID="lblPERate" runat="server" CssClass="labelForControl">Rate($):</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPERate" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                        TabIndex="65" Width="150px" Text="$3" Enabled="False"></asp:TextBox>
                                          
                                                 </div>

                                                 <div class="col-sm-2">
                                          
                                                 </div>

                                                 <div class="col-sm-2">
                                                 <br />
                                                    <br />
                                                    <asp:Label ID="lblPersonalEffectPremium" runat="server" CssClass="labelForControl">Personal Effect Premium:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPersonalEffectPremium" runat="server" CssClass="form-controlWhite"
                                                         TabIndex="66" Width="150px" Enabled="False"></asp:TextBox>
                                                 </div>
                                               
                                               </div>

                                               <div class="col-lg-12">
                                               <hr style="border: 2px solid#D3D3D3;" />
                                               </div>

                                                <div>

                                                    <div class="col-lg-12">
                                                        <asp:Label ID="lblOtherCoverage" runat="server" Font-Bold="True" CssClass="labelForControl"
                                                            Text="Other Coverage"></asp:Label>
                                                    </div>

                                                    <div class="col-sm-2">
                                                    <br />
                                                    <asp:Label ID="lblTrailer" runat="server" CssClass="labelForControl">Trailer:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtTrailer" Style="" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="67" Width="150px" onkeypress="return onlyNumbers(event);" OnTextChanged="txtTrailer_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblUninsuredBoaters" runat="server" CssClass="labelForControl">Uninsured Boaters:</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlUninsuredBoaters" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                        TabIndex="70" Width="150px" OnSelectedIndexChanged="ddlUninsuredBoaters_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:HiddenField ID="txtDDLUninsuredBoaters" runat="server"></asp:HiddenField>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblTripTransitNotes" runat="server" CssClass="labelForControl">Trip Transit Notes:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtTripTransitNotes" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="73" Width="900px"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblMiscellaneousNotes" runat="server" CssClass="labelForControl">Miscellaneous Notes</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtMiscellaneousNotes" runat="server" Style="" 
                                                        CssClass="form-controlWhite" TabIndex="75" Width="900px"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-1">
                                          
                                                </div>

                                                <div class="col-sm-2">
                                                    <br />
                                                     <asp:Label ID="lblTrailerRate" runat="server" CssClass="labelForControl">Rate($):</asp:Label>
                                                      <br />
                                                    <asp:TextBox ID="txtTrailerRate" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                        TabIndex="68" Width="150px" Text="$3" Enabled="False"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblOtherUninsuredBoater" runat="server" CssClass="labelForControl">Other Uninsured Boater:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtOtherUninsuredBoater" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="71" Width="150px" onkeypress="return onlyNumbers(event);" OnTextChanged="txtOtherUninsuredBoater_TextChanged" AutoPostBack="True"></asp:TextBox>

                                                </div>

                                                <div class="col-sm-1">
                                          
                                                </div>

                                                <div class="col-sm-2">
                                                <br />
                                                    <asp:Label ID="lblTrailerModel" runat="server" CssClass="labelForControl">Trailer Model:</asp:Label>
                                                      <br />
                                                    <asp:TextBox ID="txtTrailerModel" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                        TabIndex="68" Width="150px" Enabled="False"></asp:TextBox>
                                                </div>


                                                <div class="col-sm-2">
                                                <br />
                                                    <asp:Label ID="lblTrailerSerial" runat="server" CssClass="labelForControl">Trailer Serial Number:</asp:Label>
                                                      <br />
                                                    <asp:TextBox ID="txtTrailerSerial" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                        TabIndex="68" Width="150px" Enabled="False"></asp:TextBox>
                                                </div>

                                                <div class="col-sm-2">
                                                <br />
                                                 <asp:Label ID="lblTrailerPremium" runat="server" CssClass="labelForControl">Trailer Premium:</asp:Label>

                                                    <br />
                                                        <asp:TextBox ID="txtTrailerPremium" Style="" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="69" Width="150px" Enable="False"></asp:TextBox>  
                                                        <br />
                                                        <br />
                                                        <br />
                                                    <asp:Label ID="lblOtherUninsuredBoaterPremium" runat="server" CssClass="labelForControl">Uninsured Boater Premium:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtOtherUninsuredBoaterPremium" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="72" Width="150px" Enable="False" onkeypress="return onlyNumbers(event);" OnTextChanged="txtOtherUninsuredBoaterPremium_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblTripTransit" runat="server" CssClass="labelForControl">Trip Transit Premium:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtTripTransit" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="74" Width="150px" onkeypress="return onlyNumbers(event);" OnTextChanged="txtTripTransit_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblMiscellaneous" runat="server" CssClass="labelForControl">Miscellaneous Premium:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtMiscellaneous" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="76" Width="150px" onkeypress="return onlyNumbers(event);" OnTextChanged="txtMiscellaneous_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                </div>

                                               </div>
    
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                           
                        </div>

                            <div id="Div3" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion6" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40"
                                    HeaderCssClass="accordion-head" ContentCssClass="accordion-body" RequireOpenedPane="false"
                                    SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane7" runat="server">
                                            <Header>
                                                ADDITIONAL INFORMATION
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>

                                                <%-- ADDITIONAL INFORMATION DIVISION --%>
                                                <div>
                                                    <div>
                                                         <div class="col-lg-12">
                                                            <asp:Label ID="lblSurveyHeader" runat="server" Font-Bold="True" CssClass="labelForControl"
                                                                Text="Survey"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-2">
                                                         <br />
                                                        <asp:Label ID="lblSurveyName" runat="server" CssClass="labelForControl">Name:</asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtSurveyName" runat="server" CssClass="form-controlWhite" 
                                                            TabIndex="77" Width="200px"></asp:TextBox>
                                                        </div>

                                                        <div class="col-sm-1">
                                          
                                                        </div>

                                                         <div class="col-sm-2">
                                                            <br />
                                                            <asp:Label ID="lblSurveyFee" runat="server" CssClass="labelForControl">Survey Fee:</asp:Label>
                                                            <br />
                                                            <asp:TextBox ID="txtSurveyFee" runat="server" CssClass="form-controlWhite" 
                                                                TabIndex="78" Width="200px" onkeypress="return onlyNumbers(event);"></asp:TextBox>
                                                         </div>

                                                        <div class="col-sm-1">
                                          
                                                        </div>

                                                         <div class="col-sm-2">
                                                            <br />
                                                           <asp:Label ID="lblSurveyDate" runat="server" CssClass="labelForControl">Survey Date:</asp:Label>
                                                            <br />
                                                            <asp:TextBox ID="txtSurveyDate" runat="server" CssClass="form-controlWhite" 
                                                                TabIndex="79" Width="130px" Enabled="False"></asp:TextBox>
                                                         
<%--                                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" 
                                                                PopupButtonID="imgCalendarSurveyDate" TargetControlID="txtSurveyDate" CssClass="Calendar"> 
                                                            </asp:CalendarExtender>--%>
                                                            <asp:ImageButton ID="imgCalendarSurveyDate" runat="server" Height="16px" 
                                                                ImageUrl="~/Images2/Calendar.png" Width="16px" onclick="imgCalendarSurveyDate_Click" />

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


                                                         </div>

                                                
                                                           <div class="col-sm-2" style="margin-left:90px;">
                                                           <br />
                                                            <asp:Label ID="lblSurveyDateCompleted" runat="server" CssClass="labelForControl" >Survey Date Completed:</asp:Label>
                                                            <br />
                                                            <asp:TextBox ID="txtSurveyDateCompleted" runat="server" CssClass="form-controlWhite" 
                                                                TabIndex="80" Width="130px" Enabled="False"></asp:TextBox>
                                                        <%--    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" 
                                                                PopupButtonID="imgCalendarSurveyDateCompleted" TargetControlID="txtSurveyDateCompleted" CssClass="Calendar"> 
                                                            </asp:CalendarExtender>--%>
                                                            <asp:ImageButton ID="imgCalendarSurveyDateCompleted" runat="server" Height="16px" 
                                                                ImageUrl="~/Images2/Calendar.png" Width="16px" onclick="imgCalendarSurveyDateCompleted_Click"/>
                                                           
                                                            <asp:Calendar ID="Calendar2" runat="server" BackColor="#ffffff" BorderColor="#e2e0e0"  
                                                            BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"  
                                                            ForeColor="#000000" ShowGridLines="True"  OnSelectionChanged="Calendar2_SelectionChanged" Visible="False">   
                                                            <SelectedDayStyle BackColor="#17529b" Font-Bold="True" />  
                                                            <SelectorStyle BackColor="#ffffff" />  
                                                            <OtherMonthDayStyle ForeColor="#000000" />  
                                                            <NextPrevStyle Font-Size="9pt" ForeColor="#ffffff" />  
                                                            <DayHeaderStyle BackColor="#ffffff" Font-Bold="True" Height="1px" ForeColor="#000000"/>  
                                                            <TitleStyle BackColor="#9b9898" Font-Bold="True" Font-Size="9pt" ForeColor="#ffffff" />  
                                                        </asp:Calendar> 
                                                         </div>

                                               

                                                          <div class="col-lg-12">
                                                          <center>
                                                           <div style="margin-right:350px; margin-top:20px;">
                                                                  <asp:GridView ID="gridViewSurvey" runat="server"  OnRowDataBound="gridViewSurvey_RowDataBound" AutoGenerateColumns="false" 
                                                                        onrowdeleting="gridViewSurvey_RowDeleting" OnRowCommand="gridViewSurvey_RowCommand"
                                                                        AlternatingItemStyle-BackColor="#FEFBF6" AlternatingItemStyle-CssClass="HeadForm3" BackColor="White"
                                                                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Names="Arial"
                                                                                    Font-Size="10pt" HeaderStyle-BackColor="#5C8BAE" HeaderStyle-CssClass="HeadForm2"
                                                                                    HeaderStyle-HorizontalAlign="Center" Height="100px" ItemStyle-CssClass="HeadForm3"
                                                                                    ItemStyle-HorizontalAlign="center"  Width="100%" GridLines="Both" style="margin-left: 15%; margin-right: auto;">
                                                                                    <HeaderStyle HorizontalAlign="Center" Height="10px" ForeColor="White" CssClass="HeadForm2" BackColor="#17529B"></HeaderStyle>
                                                                                    <RowStyle HorizontalAlign="Center"/>
                                                                                    <AlternatingRowStyle BackColor="#CFDBE7" />

                                                                <Columns>
                                                                    <asp:BoundField DataField="No" HeaderText="No."/>
                                                                    <asp:BoundField DataField="TaskControlID" HeaderText="TaskControlID"/>

                                                                    <asp:BoundField DataField="SurveyorName" HeaderText="Name">
                                                                         <ItemStyle Width="90px" />
                                                                    </asp:BoundField>

                                                                    <asp:BoundField DataField="SurveyFee" HeaderText="Survey Fee">
                                                                         <ItemStyle Width="90px" HorizontalAlign="Center" VerticalAlign="Middle"/>
                                                                    </asp:BoundField>

                                                                    <asp:BoundField DataField="SurveyDate" HeaderText="Survey Date">
                                                                        <ItemStyle Width="90px" />
                                                                    </asp:BoundField>

                                                                    <asp:BoundField DataField="SurveyDateCompleted" HeaderText="Survey Date Completed">
                                                                         <ItemStyle Width="90px" />
                                                                    </asp:BoundField>

                                                                     <asp:TemplateField HeaderText="Recomendaciones Pendientes" ItemStyle-Width="50px">
                                                                         <ItemTemplate>
                                                                             <asp:CheckBox ID="checkBoxRecomendaciones" runat="server" Enabled="true" AutoPostBack="true" />
                                                                         </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:buttonfield buttontype="Link" commandname="Modify" text="Modify"/>
                                                                    <asp:buttonfield buttontype="Link" commandname="Delete" text="Delete"/>
                                                              </Columns>

                                                                    </asp:GridView>
                                                                 </div>
                                                                 </center>

                                                     
                                                            </div>

                                                            <div class="col-lg-12">
                                                            <center>
                                                                <br />
                                                                <br />
                                                                <asp:Button ID="btnAddSurvey" runat="server" CssClass="btn btn-primary btn-lg"
                                                                TabIndex="81" Text="ADD" Width="150px" onclick="btnAddSurvey_Click" />
                                                            </center>
                                                            </div>
                                                    

                                                    
                                                    </div>

                                                    <div class="col-lg-12">
                                                    <hr style="border: 2px solid#D3D3D3;" />
                                                    </div>

                                                    <div>

                                                        <div class="col-lg-12">
                                                            <asp:Label ID="lblSubjectivityHeader" runat="server" Font-Bold="True" CssClass="labelForControl"
                                                                Text="Subjectivity"></asp:Label>
                                                        </div>  

                                                         <div class="col-sm-12">
                                                                <br />
                                                                <asp:Label ID="lblSubjectivityNote" runat="server" CssClass="labelForControl">Notes</asp:Label>
                                                                <br />
                                                                <asp:TextBox ID="txtSubjectivityNote" runat="server" Style="" Width="1150px"
                                                                    CssClass="form-controlWhite"  TabIndex="82"></asp:TextBox>
                                                        </div>

                                                    </div>

                                                    
                                                </div>
                                                <%-- END ADDITIONAL INFORMATION COLUMN--%>


                                                <div class="form-group" align="center">
                                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server"
                                                        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="10">
                                                        <ProgressTemplate>
                                                            <img alt="" src="Images2/loader.gif" style="width: 35px; height: 35px" />
                                                            <span><span class="style5"></span><span class="style6">Please
                                                                    wait...</span></span>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </div>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                                 
                            </div>
                            <%-- END ADDITIONAL INFORMATION ACCORDION --%>

                               <div id="Div2" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion5" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40"
                                    HeaderCssClass="accordion-head" ContentCssClass="accordion-body" RequireOpenedPane="false"
                                    SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane6" runat="server">
                                            <Header>
                                                TOTAL PREMIUM INFORMATION
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>

                                            <div>
                                            <center>
                                                <div class="col-sm-3">
                                                  
                                                    <asp:Label ID="lblTotalPremiumOpcion1" runat="server" CssClass="labelForControl" Font-Bold="True">Option 1:</asp:Label>
                                                    <br />
                                                    
                                                    <asp:Label ID="lblTotalPremium" runat="server" CssClass="labelForControl" ForeColor="red">Total Premium:</asp:Label>
                                                    <br />
                                                    <asp:RadioButton ID="radioBtnTP1" runat="server" GroupName="TotalPremium" Enable="false" Visible="False"/>  
                                                    <asp:TextBox ID="txtTotalPremium" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="83" Width="200px" Enabled="false"></asp:TextBox>
                                                   
                                                </div>

                                                <div class="col-sm-1">
                                          
                                                </div>

                                                <div class="col-sm-3">
                                                 <br />
                                                    <asp:Label ID="lblTotalPremiumPoliza" runat="server" CssClass="labelForControl" Visible="false">Total Premium:</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtTotalPremiumPoliza" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="84" Width="200px" Enabled="false" Visible="false"></asp:TextBox>
                                                    <div id="DivOPPEndorsement" runat="server" visible="false">
                                                        <asp:TextBox ID="txtPreviousTotalPremiumPoliza" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="84" Width="200px"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                         <asp:TextBox ID="txtDiffPremYacht" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="84" Width="200px"></asp:TextBox>
                                                    </div>
                                                
                                                </div>
                                                <div class="col-sm-1">
                                          
                                                </div>

                                                <div class="col-sm-3">
                                               
                                                    <asp:Label ID="lblTotalPremiumOpcion2" runat="server" CssClass="labelForControl" Font-Bold="True">Option 2:</asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblTotalPremium2" runat="server" CssClass="labelForControl">Total Premium:</asp:Label>
                                                    <br />
                                                    <asp:RadioButton ID="radioBtnTP2" runat="server" GroupName="TotalPremium" Enable="false" Visible="False"/>
                                                    <asp:TextBox ID="txtTotalPremium2" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="85" Width="200px" Enabled="false"></asp:TextBox>
                                                 
                                                </div>
                                                </center>
                                             
                                            </div>



                                                <div class="form-group" align="center">
                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server"
                                                        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="10">
                                                        <ProgressTemplate>
                                                            <img alt="" src="Images2/loader.gif" style="width: 35px; height: 35px" />
                                                            <span><span class="style5"></span><span class="style6">Please
                                                                    wait...</span></span>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </div>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                                <div class="col-sm-12" align="center">
                             <br />
                            <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                            <br />

                            <cc1:Mirror id="btnSaveMirror" ControlID="btnSave" runat="server" />
                            <cc1:Mirror id="btnModifyMirror" ControlID="btnModify" runat="server" />
                            <%--<cc1:Mirror id="btnPrintPolicyMirror" ControlID="btnPrintPolicy" runat="server" />
                            <cc1:Mirror id="btnPrintCertificateMirror" ControlID="btnPrintCertificate" runat="server" />
                            <cc1:Mirror id="btnPrintPortEndorsementMirror" ControlID="btnPrintPortEndorsement" runat="server" />
                            <cc1:Mirror id="btnPrintCertificateBankMirror" ControlID="btnPrintCertificateBank" runat="server" />
                            <cc1:Mirror id="btnPrintCertificateMarinaMirror" ControlID="btnPrintCertificateMarina" runat="server" />--%>
                            <cc1:Mirror id="btnPreviewMirror" ControlID="btnPreview" runat="server" />
                            <cc1:Mirror id="btnPreview2Mirror" ControlID="btnPreview2" runat="server" />
                            <cc1:Mirror id="btnPreviewPolicyMirror" ControlID="btnPreviewPolicy" runat="server" />
                            <cc1:Mirror id="btnConvertMirror" ControlID="btnConvert" runat="server" />
                            <cc1:Mirror id="btnAcceptQuoteMirror" ControlID="btnAcceptQuote" runat="server" />
                            <cc1:Mirror id="btnCancelMirror" ControlID="btnCancel" runat="server" />
                            <cc1:Mirror id="btnPremiumFinanceMirror" ControlID="btnPremiumFinance" runat="server" />
                            <cc1:Mirror id="btnSave2Mirror" ControlID="btnSave2" runat="server" />
                            <cc1:Mirror id="btnModify2Mirror" ControlID="btnModify2" runat="server" />
                            
                 
                           <br /><br /><br />
                            </div>
                </div>
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
                            <asp:DropDownList ID="ddlBank" runat="server" TabIndex="7010" CssClass="form-controlWhite"
                            OnSelectedIndexChanged="ddlBank_SelectedIndexChanged" AutoPostBack="True" EnableViewState="true" >
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
                                                TextMode="MultiLine" Width="400px" Height="150px"></asp:TextBox>
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
                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="PrintIDCard" HeaderText="Print ID Cards" Visible="False">
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
        <input id="ConfirmDialogBoxPopUp" runat="server" name="ConfirmDialogBoxPopUp" size="1"
            style="z-index: 102; left: 783px; width: 35px; position: absolute; top: 895px;
            height: 22px" type="hidden" value="false" />

                        
    </div>
    </form>

</body>

