<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuardianXtra.aspx.cs" Inherits="EPolicy.GuardianXtra" %>

<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" />
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

        <script type='text/javascript'>
        jQuery(function ($) {
            $("#AccordionPane1_content_TxtHomePhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane1_content_TxtCellular").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane1_content_TxtBirthdate").mask("00/00/0000", { placeholder: "__/__/____" });
            //          $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            $("#AccordionPane1_content_txtWorkPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane1_content_txtSocialSec").mask("000-00-0000", { placeholder: "__-__-____" });
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
            <ContentTemplate>
                <script type="text/javascript">
                    function getexpdt() {
                        
                        //var odt = document.ra.txteffdt.value;
                        var pdt = new date(document.ra.txteffdt.value);
                        var trm = parseint(document.ra.txtterm.value);
                        var mnth = pdt.getmonth() + trm;
                        
                            if (!isnan(mnth)) 
                            {
                                
                                pdt.setmonth(mnth % 12);
                                if (mnth > 11) 
                                {
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
                <script src="js/jquery-1.12.1.min.js" type="text/javascript"></script>
                <script src="js/jquery.mask.js" type="text/javascript"></script>
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
                            Guardian Xtra</h1>
                        <div class="form=group" align="center">
                            <asp:Button ID="btnReinstallation" runat="server" CssClass="btn btn-primary btn-lg"
                                TabIndex="73" Text="REINSTALLATION" Visible="False" OnClick="btnReinstallation_Click"
                                Style="margin-left: 10px;" Width="200px"/>
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnDelete_Click"
                                TabIndex="74" Text="DELETE" Visible="False" Style="margin-left: 10px;" Width="200px"/>
                            <asp:Button ID="btnAdd2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnAdd2_Click"
                                TabIndex="75" Text="ADD" Visible="false" Style="margin-left: 10px;" Width="200px"/>
                            <asp:Button ID="btnGuardianPay" runat="server" CssClass="btn btn-primary btn-lg"
                                OnClick="btnGuardianPay_Click" TabIndex="81" Text="GUARDIAN PAY" Style="margin-left: 10px;" Width="220px"/>
                            <asp:Button ID="btnRenew" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnRenew_Click"
                                TabIndex="70" Text="RENEW" Visible="False" Style="margin-left: 10px;" Width="200px"/>
                            <asp:Button ID="btnCancellation" runat="server" CssClass="btn btn-primary btn-lg"
                                TabIndex="72" Text="CANCELLATION" Visible="False" OnClick="btnCancellation_Click"
                                Style="margin-left: 10px;" Width="200px"/>
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnEdit_Click"
                                TabIndex="76" Text="MODIFY" Style="margin-left: 10px;" Width="200px"/>
                            <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-primary btn-lg" OnClick="BtnSave_Click"
                                TabIndex="77" Text="SAVE" Visible="False" Style="margin-left: 10px;" Width="200px"/>
                            <asp:Button ID="btnPrintInvoice" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPrintInvoice_Click" 
                                TabIndex="78" Text="PRINT INVOICE" Style="margin-left: 10px;" Width="200px" Visible="False"/>
                            &nbsp;
                            <asp:DropDownList ID="ddlPrintOption" runat="server" 
                                CssClass="btn btn-primary btn-lg" TabIndex="18" Width="200px">
                                <asp:ListItem>Print All</asp:ListItem>
                                <asp:ListItem>Insured Information</asp:ListItem>
                                <asp:ListItem>Payment Information</asp:ListItem>
                                <asp:ListItem>Declaration Page</asp:ListItem>
                                <asp:ListItem>Terms & Services</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnPrintPolicy" runat="server" CssClass="btn btn-primary btn-lg"
                                OnClick="btnPrint_Click" TabIndex="78" Text="PRINT" Style="margin-left: 10px;" Width="220px"/>
                            &nbsp;
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnCancel_Click"
                                TabIndex="80" Text="CANCEL" Visible="False" Style="margin-left: 10px;" Width="220px"/>
                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary btn-lg" OnClick="BtnExit_Click"
                                TabIndex="81" Text="EXIT" Visible="False" Style="margin-left: 10px;" Width="220px"/>
                            <br />
                            <br />
                            <div align="left">
                                <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="Gray">Guardian Xtra:</asp:Label>
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
                            <Toolkit:Accordion ID="MyAccordion" runat="Server" AutoSize="None" CssClass="accordion" HeaderSelectedCssClass=""
                                FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
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
                                                <br />
                                                <asp:Label ID="lblProspectNo" runat="server" CssClass="labelForControl">Customer No.:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtProspectNo" runat="server" CssClass="form-controlWhite" MaxLength="10"
                                                    TabIndex="1000" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label13" runat="server" CssClass="labelForControl" ForeColor="Red">First Name:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtFirstName" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite"
                                                     TabIndex="1001" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblInitial" runat="server" CssClass="labelForControl" EnableViewState="False">Initial:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtInitial" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="1" TabIndex="1002" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblLastName" runat="server" CssClass="labelForControl" ForeColor="Red">Last Name 1:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtLastname1" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="15" TabIndex="1003" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblLastName2" runat="server" CssClass="labelForControl">Last Name 2:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtLastname2" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="15" TabIndex="1004" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblBirthdate" runat="server" CssClass="labelForControl" ForeColor="Red">Date of Birth:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtBirthdate" TabIndex="1005" runat="server" ISDATE="True" CssClass="form-controlWhite" Width="200px"></asp:TextBox>
                                                <%--<Toolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-US" Mask="99/99/9999" 
                                                MaskType="Date" TargetControlID="TxtBirthDate"> </Toolkit:MaskedEditExtender>--%>
                                                <br />
                                                <br />

                                                <asp:Label ID="lblSocialSec" runat="server" CssClass="labelForControl">Social Security No:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtSocialSec" TabIndex="1005" runat="server" CssClass="form-controlWhite" Width="200px" MaxLength="11" ></asp:TextBox>
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtSocialSec"
                                                    Mask="999-99-9999"  ClearMaskOnLostFocus="false"
                                                    InputDirection="RightToLeft">
                                                    </asp:MaskedEditExtender>

                                                <br />
                                                <br />

                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
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
                                                <asp:Label ID="Label58" runat="server" CssClass="labelForControl" ForeColor="Red">Cellular</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtCellular" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                    MaxLength="20" TabIndex="1008" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblEmail" runat="server" CssClass="labelForControl">E-mail</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-controlWhite" MaxLength="100"
                                                    Style="text-transform: uppercase" TabIndex="1009" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblLicense" runat="server" CssClass="labelForControl" ForeColor="Red">License Num.</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtLicense" runat="server" CssClass="form-controlWhite" MaxLength="7"
                                                    TabIndex="1010" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblOccupa" runat="server" CssClass="labelForControl">Occupation</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtOccupa" runat="server" CssClass="form-controlWhite" Style="text-transform: uppercase"
                                                    TabIndex="1011" MaxLength="100" Width="200px"></asp:TextBox>
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
                                                <asp:TextBox ID="TxtAddrs1" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="30" TabIndex="1012"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lbladdress2" runat="server" CssClass="labelForControl">Address 2</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtAddrs2" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    Style="text-transform: uppercase" TabIndex="1013"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblZipCode" runat="server" CssClass="labelForControl" ForeColor="Red">Zip Code</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlZip" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                    DataTextField="ZipCode" DataValueField="ZipCode" OnSelectedIndexChanged="ddlZip_SelectedIndexChanged"
                                                    TabIndex="1014" TxtCity="">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:TextBox ID="TxtCity" runat="server" CssClass="headfield1" Font-Names="Tahoma"
                                                    MaxLength="14" TabIndex="8" Visible="False"></asp:TextBox>
                                                <asp:Label ID="lblCity" runat="server" CssClass="labelForControl" ForeColor="Red">City</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlCiudad" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                    OnSelectedIndexChanged="ddlCiudad_SelectedIndexChanged" TabIndex="1015" TxtCity="" Enabled="False">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblState" runat="server" CssClass="labelForControl">State</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtState" runat="server" CssClass="form-controlWhite" MaxLength="2"
                                                    Style="text-transform: uppercase" TabIndex="1016"></asp:TextBox>
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
                                                    TabIndex="1018" Style="text-transform: uppercase" AutoPostBack="True" OnTextChanged="txtPhyAddress_TextChanged"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lbladdress9" runat="server" CssClass="labelForControl">Address 2</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPhyAddress2" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    TabIndex="1019" Style="text-transform: uppercase" AutoPostBack="True" OnTextChanged="txtPhyAddress2_TextChanged"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblZipCode2" runat="server" CssClass="labelForControl" ForeColor="Red">Zip Code</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlPhyZipCode" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                    DataTextField="ZipCode" DataValueField="ZipCode" OnSelectedIndexChanged="ddlPhyZipCode_SelectedIndexChanged"
                                                    TabIndex="1020" TxtCity="">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:TextBox ID="TxtCity2" runat="server" CssClass="headfield1" Font-Names="Tahoma"
                                                    MaxLength="14" TabIndex="8" Visible="False"></asp:TextBox>
                                                <asp:Label ID="lblCity4" runat="server" CssClass="labelForControl" ForeColor="Red">City</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlPhyCity" runat="server" CssClass="form-controlWhite" TabIndex="1021"
                                                   OnSelectedIndexChanged="ddlPhyCity_SelectedIndexChanged" AutoPostBack="True" Enabled="False">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblState2" runat="server" CssClass="labelForControl">State</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPhyState" runat="server" CssClass="form-controlWhite" MaxLength="2"
                                                    Style="text-transform: uppercase" TabIndex="1022" AutoPostBack="True" OnTextChanged="txtPhyState_TextChanged"></asp:TextBox>
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
                                            POLICY DETAIL
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
                                                    TabIndex="14" Text=" " ToolTip="Auto Assign Policy" Visible="False" />
                                                <br />
                                                <asp:TextBox ID="TxtPolicyNo" runat="server" CssClass="form-controlWhite" MaxLength="15"
                                                    TabIndex="2000"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblPolicyType" runat="server" CssClass="labelForControl" Width="83px">Policy Type</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtPolicyType" runat="server" CssClass="form-controlWhite" MaxLength="3"
                                                    TabIndex="2001"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblSuffix" runat="server" CssClass="labelForControl" Width="34px">Suffix</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtSufijo" runat="server" CssClass="form-controlWhite" MaxLength="2"
                                                    TabIndex="2002"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblTerm" runat="server" CssClass="labelForControl">Term</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtTerm" runat="server" CssClass="form-controlWhite" MaxLength="3"
                                                    TabIndex="2003"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                               
                                            <div class="col-sm-2">
                                                <asp:Label ID="Label59" runat="server" CssClass="labelForControl" ForeColor="Red">Effective Date</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtEffDt" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                    OnTextChanged="txtEffDt_TextChanged" TabIndex="2004" Width="175px"></asp:TextBox>   &nbsp;                                            
                                                <Toolkit:CalendarExtender ID="txtEffDt_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                    PopupButtonID="imgCalendarEff" TargetControlID="txtEffDt"  CssClass="Calendar">
                                                </Toolkit:CalendarExtender>
                                                <%--<Toolkit:MaskedEditExtender ID="txtEffDt_MaskedEditExtender" runat="server" CultureName="en-US"
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtEffDt">
                                                </Toolkit:MaskedEditExtender>--%>
                                                <asp:ImageButton ID="imgCalendarEff" runat="server" ImageUrl="~/Images2/Calendar.png"
                                                    TabIndex="2005" Width="25px" Height="25px"/>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label61" runat="server" CssClass="labelForControl">Exp. Date</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtExpDt" runat="server" AutoPostBack="False" CssClass="form-controlWhite"
                                                    OnTextChanged="txtExpDt_TextChanged" TabIndex="2006" ></asp:TextBox>
                                                <Toolkit:CalendarExtender ID="txtExpDt_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                    PopupButtonID="imgCalendarExp" TargetControlID="txtExpDt"  CssClass="Calendar">
                                                </Toolkit:CalendarExtender>
                                                <Toolkit:MaskedEditExtender ID="txtExpDt_MaskedEditExtender" runat="server" CultureName="en-US"
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtExpDt">
                                                </Toolkit:MaskedEditExtender>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label63" runat="server" CssClass="labelForControl">Entry Date</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-controlWhite" TabIndex="2007"
                                                    ></asp:TextBox>
                                                <Toolkit:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                                    PopupButtonID="imgCalendarEnt" TargetControlID="txtEntryDate"  CssClass="Calendar">
                                                </Toolkit:CalendarExtender>
                                                <Toolkit:MaskedEditExtender ID="txtEntryDate_MaskedEditExtender" runat="server" CultureName="en-US"
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtEntryDate">
                                                </Toolkit:MaskedEditExtender>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label64" runat="server" CssClass="labelForControl" ForeColor="Red">Deducible</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlDeducible" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                    OnSelectedIndexChanged="ddlDeducible_SelectedIndexChanged" TabIndex="2008" >
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="$89">$200</asp:ListItem>
                                                    <asp:ListItem Value="$95">$150</asp:ListItem>
                                                    <asp:ListItem Value="$100">$100</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblTotalPremium" runat="server" CssClass="labelForControl">Total Premium</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPremium" runat="server" CssClass="form-controlWhite" Enabled="False"
                                                    ReadOnly="True" TabIndex="2009" ></asp:TextBox>
                                                <br />
                                                <br />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Label ID="Label60" runat="server" CssClass="labelForControl">Ins. Company</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlInsuranceCompany" runat="server" CssClass="form-controlWhite" TabIndex="2010" Width="250px">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label62" runat="server" CssClass="labelForControl">Agency</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlAgency" runat="server" CssClass="form-controlWhite" TabIndex="2011" Width="250px">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblSelectedAgent" runat="server" CssClass="labelForControl" ForeColor="Red">Agent</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlAgent" runat="server" CssClass="form-controlWhite" TabIndex="2012" Enabled="False" Width="250px">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label65" runat="server" CssClass="labelForControl">Originated At</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlOriginatedAt" runat="server" CssClass="form-controlWhite" TabIndex="2013" Width="250px">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:TextBox ID="txtTotalPremium" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                    MaxLength="15" ReadOnly="True" TabIndex="22" Visible="False" Width="125px"></asp:TextBox>
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
                                            <div class="col-sm-1">
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
                                            VEHICLES INFORMATION
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>

                                          <div class="col-sm-1">
                                             </div>

                                        <div style="width:100%">
                                         <asp:CheckBox ID="chkIsCommercialAuto" runat="server" AutoPostBack="True" Font-Bold="True"
                                                    CssClass="labelForControl" OnCheckedChanged="chkIsCommercialAuto_CheckedChanged"
                                                    TabIndex="3000" Text="Is Commercial Auto" />
                                          &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                                  <asp:CheckBox ID="chkIsPersonalAuto" runat="server" AutoPostBack="True" Checked="True"
                                                    Font-Bold="True" OnCheckedChanged="chkIsPersonalAuto_CheckedChanged" TabIndex="3001"
                                                    Text="Is Personal Auto" />
                                        &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                                  <asp:CheckBox ID="chkHasAccident12" runat="server" AutoPostBack="True" Checked="False"
                                                    Font-Bold="True" TabIndex="3001"
                                                    Text="Any accidents in the last 12 months?" />
                                                <br />
                                                <br />
                                        </div>
                                        
                                           <div class="col-sm-1">
                                             </div>

                                            <div class="col-sm-2">
                                               
                                                <asp:Label ID="lblDealer0" runat="server" CssClass="labelForControl" ForeColor="Red">VIN</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtVehicleVIN" runat="server" Style="text-transform: uppercase" CssClass="form-controlWhite" MaxLength="17"
                                                    OnTextChanged="txtVehicleVIN_TextChanged" TabIndex="3002"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblCoverage" runat="server" CssClass="labelForControl">
                                                Does this vehicle have another coverage?</asp:Label>
                                                <br />
                                                <asp:RadioButton ID="rdbCoverageYes" runat="server" AutoPostBack="True" GroupName="chkcoverage"
                                                    OnCheckedChanged="rdbCoverageYes_CheckedChanged" TabIndex="3008" Text="Yes" />
                                                    &nbsp;&nbsp;
                                                <asp:RadioButton ID="rdbCoverageNo" runat="server" AutoPostBack="True" Checked="True"
                                                    Font-Names="arial narrow" GroupName="chkcoverage" Height="16px" OnCheckedChanged="rdbCoverageNo_CheckedChanged"
                                                    TabIndex="3009" Text="No" />
                                            </div>
                                          
                                            <div class="col-sm-2">
                                              
                                                <asp:Label ID="lblDealer1" runat="server" CssClass="labelForControl" ForeColor="Red">Make</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlVehicleMake" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                                    OnSelectedIndexChanged="ddlVehicleMake_SelectedIndexChanged" TabIndex="3003">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                    <asp:Label ID="lblExplain" runat="server" CssClass="labelForControl" Visible="False">If the answer is yes, explain:</asp:Label>
                                                <asp:TextBox ID="TxtExplain" runat="server" CssClass="form-controlWhite" Style="text-transform: uppercase" TabIndex="3010" Visible="False" Width="500px"></asp:TextBox>
                                            </div>
                                       
                                            <div class="col-sm-2">
                                                <asp:Label ID="lblDealer2" runat="server" CssClass="labelForControl" ForeColor="Red">Model</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlVehicleModel" runat="server" CssClass="form-controlWhite"
                                                    TabIndex="3004">
                                                </asp:DropDownList>
                                            </div>
                                     
                                            <div class="col-sm-2">
                                                <asp:Label ID="lblDealer3" runat="server" CssClass="labelForControl" ForeColor="Red">Year</asp:Label>
                                                <asp:DropDownList ID="ddlVehicleYear" runat="server" CssClass="form-controlWhite"
                                                    TabIndex="3005" >
                                                </asp:DropDownList>
                                            </div>
                                          
                                            <div class="col-sm-2">
                                                <asp:Label ID="lblDealer6" runat="server" CssClass="labelForControl" ForeColor="Red">Plate</asp:Label>
                                                <asp:TextBox ID="txtVehiclePlate" runat="server" CssClass="form-controlWhite" MaxLength="15"
                                                    Style="text-transform: uppercase" TabIndex="3006" ></asp:TextBox>
                                                <asp:Label ID="lblDealer5" runat="server" CssClass="labelForControl" ForeColor="Red"
                                                    Visible="False">New/Used</asp:Label>
                                                <asp:DropDownList ID="ddlNewUsed" runat="server" CssClass="form-controlWhite" TabIndex="3007"
                                                    Visible="False" Width="125px">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnAddVehicle" runat="server" CssClass="ButtonText-14" Font-Bold="True"
                                                    ForeColor="White" Height="25px" OnClick="btnAddVehicle_Click" TabIndex="3006"
                                                    Text="Add" Visible="false" Width="56px" />
                                            
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>
                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="Accordion3" runat="Server" AutoSize="None" CssClass="accordion"
                                FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
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
                                                Which is your preffered payment method:
                                                </asp:Label>

                                                <br />
                                                
                                                <br />

                                                <asp:Label ID="lblDefpayment" runat="server" CssClass="labelForControl" >
                                                Would you like deferred payment plan for payment:
                                                </asp:Label>
                                                <br />
                                             
                                                <br />
                                                </div>


                                              <div class="col-sm-3">

                                            <asp:CheckBox ID="chkCredit" runat="server" AutoPostBack="True" GroupName="chkprefpayment"
                                                    OnCheckedChanged="chkCredit_CheckedChanged" TabIndex="3015" Text="Credit" Visible="False" />
                                                    &nbsp;&nbsp;
                                            <asp:CheckBox ID="chkDebit" runat="server" AutoPostBack="True" GroupName="chkprefpayment"
                                                    OnCheckedChanged="chkDebit_CheckedChanged" TabIndex="3016" Text="ACH" Visible="False" />
                                                    &nbsp;&nbsp;
                                            <asp:CheckBox ID="chkCash" runat="server" AutoPostBack="True"
                                                     TabIndex="3017" OnCheckedChanged="chkCash_CheckedChanged" Text="Cash" Visible="False" />
                                                
                                                    <br />
                                                    <br />
                                                <asp:RadioButton ID="rdbDefpaymentYes" runat="server" AutoPostBack="True" GroupName="chkdefpay" OnCheckedChanged="rdbDefpaymentYes_CheckedChanged" TabIndex="3011" Text="Yes"  />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:RadioButton ID="rdbDefpaymentNo" runat="server" AutoPostBack="True" Checked="True" GroupName="chkdefpay" OnCheckedChanged="rdbDefpaymentNo_CheckedChanged" TabIndex="3012" Text="No" />

                                                 <br />                                                                                            
                                                <asp:CheckBox ID="chkDefpayfour" runat="server" AutoPostBack="True" GroupName="chkpremiumpay" OnCheckedChanged="chkDefpayfour_CheckedChanged" TabIndex="3013" Text="Annex I - 4 Payments"  Visible="False" />
                                                <br />
                                                <asp:CheckBox ID="chkDefpaysix" runat="server" AutoPostBack="True" GroupName="chkpremiumpay"  OnCheckedChanged="chkDefpaysix_CheckedChanged" TabIndex="3014" Text="Annex II - 6 Payments" Visible="False" />
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
                                            <div class="col-sm-1">
                                                <asp:Button ID="btnCashPayment" runat="server" CssClass="btn btn-primary btn-lg" TabIndex="3018" OnClientClick="return myCashPayment();" OnClick="BtnCashPayment_Click" Text="Pay Cash" Visible="False" />
                                                <%--<asp:TextBox ID="txtCashPayment" runat="server" CssClass="form-controlWhite" Style="text-transform: uppercase" TabIndex="3019" Visible="False" Width="200px"></asp:TextBox>--%>
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

                                 <asp:TextBox ID="TextBox1" runat="server" MaxLength="60" placeholder="" 
                                      visible="false"      CssClass="form-controlWhite"/>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
        <input id="ConfirmDialogBoxPopUp" runat="server" name="ConfirmDialogBoxPopUp" size="1"
            style="z-index: 102; left: 783px; width: 35px; position: absolute; top: 895px;
            height: 22px" type="hidden" value="false" />
    </div>
    </form>
<%--    <script>
        $(document).ready(function () {
            $('#TxtHomePhone').mask('(000) 000-0000', { placeholder: '(###) ###-####' });
            $('#TxtCellular').mask('(000) 000-0000', { placeholder: '(###) ###-####' });
            $('#TxtBirthdate').mask('00/00/0000', { placeholder: '__/__/____' });
            //          $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            $('#txtWorkPhone').mask('(000) 000-0000', { placeholder: '(###) ###-####' });
        });

       
    </script>--%>
</body>
