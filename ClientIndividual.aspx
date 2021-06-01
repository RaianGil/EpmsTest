<%@ Page Language="c#" Inherits="EPolicy.ClientIndividual" CodeFile="ClientIndividual.aspx.cs" %>

<%@ Register Src="AddDocuments.ascx" TagName="SessionCtl" TagPrefix="uc2" %>
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
            $("#AccordionPane1_content_txtHomePhone").mask("(000) 000-0000", { placeholder: "(___) ___-____" });
            $("#AccordionPane1_content_txtWorkPhone").mask("(000) 000-0000", { placeholder: "(___) ___-____" });
            $("#AccordionPane1_content_TxtCellular").mask("(000) 000-0000", { placeholder: "(___) ___-____" });
            $("#AccordionPane1_content_txtBirthdate").mask("00/00/0000", { placeholder: "__/__/____" });
            //          $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            
        });
    </script>

</head>
<body>
    <form id="Form2" method="post" runat="server">
    <div class="container-fluid" style="height: 100%">
        <Toolkit:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnableScriptGlobalization="True">
        </Toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdjuntarCargar" />
                <asp:AsyncPostBackTrigger ControlID="ddlTransaction" EventName="SelectedIndexChanged"  />
            </Triggers>
            <ContentTemplate>
                <script type="text/javascript">
                    function getAge() {
                        pdt = new Date(document.ClientIndividual.txtBirthdate.value);
                        today = new Date("<%=today%>"); age = (today.getFullYear() - pdt.getFullYear());
                        day = pdt.getDay(); month = pdt.getMonth(); if (month == today.getMonth()) {
                            if
                            (day > today.getDay()) { age = age - 1; }
                        } else {
                            if (month > today.getMonth())
                            { age = age - 1; }
                        } if (age >= 0) {
                            document.ClientIndividual.txtAge.value = age;
                        } else { document.ClientIndividual.txtAge.value = ""; }
                    } </script>
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
                            Customer</h1>
                        <div class="form=group" align="center">
                            <asp:Button ID="btnAuditTrail" runat="server" Text="HISTORY" OnClick="Button1_Click"
                                Width="155px" CssClass="btn btn-primary btn-lg" Visible="False"></asp:Button>
                            <asp:Button ID="btnAdjuntar" runat="server" CssClass="btn btn-primary btn-lg" Text="DOCUMENTS" OnClick="btnAdjuntar_Click" Width="155px" />
                            <asp:Button ID="BtnViewTask" runat="server" Text="ACTIVITY" OnClick="BtnViewTask_Click"
                                Width="155px" CssClass="btn btn-primary btn-lg"></asp:Button>
                            <asp:Button ID="btnProfile" runat="server" Text="PROFILE" OnClick="btnProfile_Click"
                                Width="155px" CssClass="btn btn-primary btn-lg" Visible="False"></asp:Button>
                            <asp:Button ID="btnNew" runat="server" Text="ADD CUSTOMER" OnClick="btnNew_Click"
                                Width="190px" CssClass="btn btn-primary btn-lg"></asp:Button>
                            <asp:Button ID="btnEdit" runat="server" Text="MODIFY" OnClick="btnEdit_Click" Width="155px"
                                CssClass="btn btn-primary btn-lg"></asp:Button>
                            <asp:Button ID="BtnSave" runat="server" Text="SAVE" OnClick="BtnSave_Click" Width="155px"
                                CssClass="btn btn-primary btn-lg"></asp:Button>
                            <asp:Button ID="btnCancel" runat="server" Text="CANCEL" OnClick="btnCancel_Click"
                                Width="155px" CssClass="btn btn-primary btn-lg"></asp:Button>
                            <asp:Button ID="BtnExit" runat="server" Text="EXIT" OnClick="BtnExit_Click" Width="155px"
                                CssClass="btn btn-primary btn-lg"></asp:Button>
                            <asp:DropDownList ID="ddlNewApplication" TabIndex="18" runat="server" Width="210px"
                                CssClass="btn btn-primary btn-lg" Height="46px">
                                <asp:ListItem>NEW AUTO VI</asp:ListItem>
                                <asp:ListItem>NEW GUARDIANXTRA</asp:ListItem>
                                <asp:ListItem>NEW RESIDENTIAL PROPERTY</asp:ListItem>
                                <asp:ListItem>NEW ROAD ASSIST</asp:ListItem>
                                <asp:ListItem>NEW BOND</asp:ListItem>
                                <asp:ListItem>NEW YACHT</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnNewApplication" runat="server" Width="210px" CssClass="btn btn-primary btn-lg"
                                OnClick="btnNewApplication_Click" Text="NEW APPLICATION" />
                            <asp:Button ID="btnNewBond" runat="server" Width="210px" CssClass="btn btn-primary btn-lg"
                                OnClick="btnNewBond_Click" Text="NEW BOND" />
                            <br />
                            <br />
                            <div align="left">
                                <asp:Label ID="Label21" runat="server" Font-Bold="True">Individual Client:</asp:Label>
                                <asp:Label ID="lblCustNumber" runat="server" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="TxtCustomerNo" TabIndex="1" MaxLength="10" runat="server"></asp:TextBox>
                                <asp:Label ID="LblProspectID" runat="server" Visible="False"></asp:Label>
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
                                            CUSTOMER INFORMATION
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">                                               
                                                <asp:CheckBox ID="ChkNoticeOfCancellation" TabIndex="29" runat="server" AutoPostBack="True"
                                                    Text="Notice of Cancellation" Visible="False"></asp:CheckBox>
                                                <asp:CheckBox ID="ChkDisableAutomaticCustNo" TabIndex="36" runat="server" AutoPostBack="True"
                                                    Text="Disable Automatic Cust. No."></asp:CheckBox> 
                                                   <br />
                                                <asp:Label ID="Label2" runat="server" EnableViewState="False" CssClass="labelForControl">First Name</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtFirstName" TabIndex="1" runat="server" MaxLength="50" OnTextChanged="TxtFirstName_TextChanged"
                                                    CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label1" runat="server" EnableViewState="False" CssClass="labelForControl">Init.</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtInitial" TabIndex="2" runat="server" MaxLength="1" CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblLastName1" runat="server" EnableViewState="False" CssClass="labelForControl">Last Name</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtLastname1" TabIndex="3" runat="server" MaxLength="50" CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblGender" runat="server" EnableViewState="False" CssClass="labelForControl">Gender</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlGender" TabIndex="5" runat="server" CssClass="form-controlWhite">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblBirthdate" runat="server" EnableViewState="False" CssClass="labelForControl">Birthdate</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtBirthdate" TabIndex="6" runat="server" ISDATE="True" CssClass="form-controlWhite"
                                                    AutoPostBack="True"></asp:TextBox>
                                                <%--<Toolkit:MaskedEditExtender ID="MaskedEditExtenderBirthDate" runat="server" MaskType="Date"
                                                    Mask="99/99/9999" TargetControlID="txtBirthdate" CultureName="en-US">
                                                </Toolkit:MaskedEditExtender>--%>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label4" runat="server" EnableViewState="False" CssClass="labelForControl">Age</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtAge" TabIndex="8" runat="server" MaxLength="3" CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblMaritalStatus" runat="server" EnableViewState="False" CssClass="labelForControl">Marital Status</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlMaritalStatus" TabIndex="9" runat="server" CssClass="form-controlWhite">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label16" runat="server" CssClass="labelForControl">License</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtLicence" TabIndex="16" CssClass="form-controlWhite" runat="server"
                                                    MaxLength="7"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblComments" runat="server" EnableViewState="False" CssClass="labelForControl" Visible="false">Comments</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtComments" TabIndex="10" runat="server" MaxLength="200" CssClass="form-controlWhite" Visible="false"></asp:TextBox>
                                                <br />
                                                <br />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">    
                                            <br />                                            
                                                <asp:Label ID="lblHomePhone" runat="server" EnableViewState="False" CssClass="labelForControl">Home Phone</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtHomePhone" TabIndex="11" CssClass="form-controlWhite" runat="server"></asp:TextBox>
                                                <%--<Toolkit:MaskedEditExtender ID="MaskedEditExtenderHP" runat="server" Mask="(999)-999-9999"
                                                    MaskType="Number" TargetControlID="txtHomePhone" ClearMaskOnLostFocus="false">
                                                </Toolkit:MaskedEditExtender>--%>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblWorkPhone" runat="server" EnableViewState="False" CssClass="labelForControl">Work Phone</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtWorkPhone" TabIndex="12" runat="server" CssClass="form-controlWhite"></asp:TextBox>
                                                <%--<Toolkit:MaskedEditExtender ID="MaskedEditExtenderWP" runat="server" Mask="(999)-999-9999"
                                                    MaskType="Number" TargetControlID="txtWorkPhone" ClearMaskOnLostFocus="false">
                                                </Toolkit:MaskedEditExtender>--%>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label8" runat="server" EnableViewState="False" CssClass="labelForControl">Cellular</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtCellular" TabIndex="13" runat="server" CssClass="form-controlWhite"></asp:TextBox>
                                                <%--<Toolkit:MaskedEditExtender ID="MaskedEditExtenderCel" runat="server" Mask="(999)-999-9999"
                                                    MaskType="Number" TargetControlID="txtCellular" ClearMaskOnLostFocus="false">
                                                </Toolkit:MaskedEditExtender>--%>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblEmail" runat="server" EnableViewState="False" CssClass="labelForControl">E-mail</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtEmail" TabIndex="14" runat="server" MaxLength="100" CssClass="form-controlWhite" TextMode="MultiLine"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblJobName" runat="server" EnableViewState="false" CssClass="labelForControl" Visible="False">Work Name</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtWorkName" TabIndex="15" runat="server" MaxLength="20" CssClass="form-controlWhite" Visible="False"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblWorkcity" runat="server" EnableViewState="False" CssClass="labelForControl" Visible="False">Work City</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtWorkCity" TabIndex="16" runat="server" MaxLength="20" CssClass="form-controlWhite" Visible="False"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblHouseIncome" runat="server" CssClass="labelForControl" Visible="False">Household Income</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlHouseIncome" TabIndex="18" runat="server" CssClass="form-controlWhite" Visible="False">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <br />
                                                <asp:Label ID="lblOriginatedAt" runat="server" CssClass="labelForControl">Originated At</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlOriginatedAt" TabIndex="19" runat="server" CssClass="form-controlWhite">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblAgentAssigned" runat="server" CssClass="labelForControl">Agent Assigned:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtAgent" TabIndex="17" runat="server" CssClass="form-controlWhite" TextMode="MultiLine"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblLastName2" runat="server" EnableViewState="False" Visible="True"
                                                    CssClass="labelForControl">Company</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtLastname2" TabIndex="4" runat="server" MaxLength="25" CssClass="form-controlWhite"
                                                    Visible="True"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblOccupation" runat="server" EnableViewState="False" CssClass="labelForControl" Visible="False">Occupation</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlOccupation" TabIndex="21" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlOccupation_SelectedIndexChanged" CssClass="form-controlWhite" Visible="False">
                                                </asp:DropDownList>
                                                <br />
                                                <br />

                                                <asp:Label ID="lblSocialSecurity" runat="server" EnableViewState="True" Visible="True"
                                                    CssClass="labelForControl">Social Security</asp:Label>

                                                <asp:TextBox ID="txtSocialSecurity" TabIndex="17" runat="server" Visible="True" MaxLength="9"
                                                    CssClass="form-controlWhite"></asp:TextBox>
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtSocialSecurity"
                                                    Mask="999-99-9999"  ClearMaskOnLostFocus="false"
                                                    InputDirection="RightToLeft">
                                                    </asp:MaskedEditExtender>

                                                <asp:TextBox ID="txtOtherOccupation" TabIndex="22" runat="server" MaxLength="15"
                                                    Visible="False" CssClass="form-controlWhite"></asp:TextBox>
                                                <asp:Label ID="Label13" runat="server" Visible="False" CssClass="labelForControl">Related To</asp:Label>
                                                <asp:DropDownList ID="ddlRelatedTo" TabIndex="23" runat="server" Visible="False"
                                                    CssClass="form-controlWhite">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="E">Entry Date</asp:ListItem>
                                                    <asp:ListItem Value="C">Close Date</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:Label ID="Label7" runat="server" Visible="False" CssClass="labelForControl">Master Client</asp:Label>
                                                <asp:DropDownList ID="ddlMasterCustomer" TabIndex="20" runat="server" Visible="False"
                                                    CssClass="form-controlWhite">
                                                </asp:DropDownList>
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
                                    <Toolkit:AccordionPane ID="AccordionPane2" runat="server">
                                        <Header>
                                            CUSTOMER ADDRESS
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-2">
                                                <asp:Label ID="Label14" runat="server" Font-Size="XX-Small" CssClass="labelForControl">*Address1 (Urb.,Cond.Bo.,Res.,Secc.Coop.,QBDA,Parcelas,Sector)</asp:Label>
                                                <asp:Label ID="Label15" runat="server" Font-Size="XX-Small" CssClass="labelForControl">**Address2(PoBox,Street,HC,Ave.,BLVD.,Camino,RR,Parque)</asp:Label>
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Label ID="lblTypeAddress1" runat="server" Font-Bold="true" CssClass="labelForControl">Postal Address</asp:Label>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblHomeUrb" runat="server" CssClass="labelForControl">*Address1</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtHomeUrb1" TabIndex="24" Style="text-transform: uppercase" MaxLength="30" runat="server" CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblAddress1" runat="server" CssClass="labelForControl">**Address2</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtAddress1" TabIndex="25" Style="text-transform: uppercase" MaxLength="30" runat="server" CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblCity" runat="server" CssClass="labelForControl">City</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtCity" TabIndex="26" Style="text-transform: uppercase" MaxLength="14" runat="server" CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblState" runat="server" CssClass="labelForControl">State</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtState" TabIndex="27" Style="text-transform: uppercase" MaxLength="2" runat="server" CssClass="form-controlWhite">PR</asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblZipCode" runat="server" CssClass="labelForControl">Zip Code</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtZipCode" TabIndex="28" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="5"></asp:TextBox>
                                                <Toolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderZipCode1" runat="server"
                                                    TargetControlID="txtZipCode" FilterType="Numbers">
                                                </Toolkit:FilteredTextBoxExtender>
                                                <br />
                                                <br />
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:CheckBox ID="chkSameAddr" TabIndex="30" runat="server" Text="Same as postal"
                                                    AutoPostBack="True" Enabled="False" OnCheckedChanged="chkSameAddr_CheckedChanged">
                                                </asp:CheckBox>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Label ID="LblTypeAddress2" runat="server" Font-Bold="true">Physical Address</asp:Label>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label9" runat="server" CssClass="labelForControl">Address1</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtAddress1Phys" TabIndex="31" Style="text-transform: uppercase" MaxLength="30" runat="server" CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label17" runat="server" CssClass="labelForControl">Address2</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtAddress2Phys" TabIndex="32" Style="text-transform: uppercase" MaxLength="30" runat="server" CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label10" runat="server" CssClass="labelForControl">City</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtCityPhys" TabIndex="33" Style="text-transform: uppercase" MaxLength="14" runat="server" CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label11" runat="server" CssClass="labelForControl">State</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtStatePhys" TabIndex="34" Style="text-transform: uppercase" MaxLength="2" runat="server" CssClass="form-controlWhite"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label12" runat="server" CssClass="labelForControl">Zip Code</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtZipCodePhys" TabIndex="35" Style="text-transform: uppercase" MaxLength="5" runat="server" CssClass="form-controlWhite"></asp:TextBox>
                                                <Toolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtenderZipCodePhys" runat="server"
                                                    TargetControlID="txtZipCodePhys" FilterType="Numbers">
                                                </Toolkit:FilteredTextBoxExtender>
                                                <br />
                                                <br />
                                            </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>
                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="Accordion2" runat="Server" AutoSize="None" CssClass="accordion"
                                FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250" Visible="False">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane3" runat="server">
                                        <Header>
                                            FLAGS INFORMATION
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <br />
                                                <asp:CheckBox ID="chkExcludePerson" TabIndex="29" runat="server" CssClass="labelForControl"
                                                    Text="Exclude Person From Policy"></asp:CheckBox>
                                                <br />
                                                <asp:CheckBox ID="chkKeepWatch" TabIndex="29" runat="server" CssClass="labelForControl"
                                                    Text="Keep watch on this person/policy"></asp:CheckBox>
                                                <br />
                                                <asp:CheckBox ID="chkFrontingPolicy" TabIndex="29" runat="server" CssClass="labelForControl"
                                                    Text="Fronting Policy"></asp:CheckBox>
                                                <br />
                                                <asp:CheckBox ID="chkPFCPolicyDisp" TabIndex="29" runat="server" CssClass="labelForControl"
                                                    Text="PFC Policy Display"></asp:CheckBox>
                                                <br />
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <br />
                                                <asp:CheckBox ID="chkEmployeeDiscount" TabIndex="29" runat="server" CssClass="labelForControl"
                                                    Text="Employee Discount"></asp:CheckBox>
                                                <br />
                                                <asp:CheckBox ID="chkDoNotRenew" TabIndex="29" runat="server" CssClass="labelForControl"
                                                    Text="Do Not Renew"></asp:CheckBox>
                                                <br />
                                                <asp:CheckBox ID="chkDUIPersonDisp" TabIndex="29" runat="server" CssClass="labelForControl"
                                                    Text="DUI on Person Display"></asp:CheckBox>
                                                <br />
                                                <asp:CheckBox ID="chkClaimsPolicyDisplay" TabIndex="29" runat="server" CssClass="labelForControl"
                                                    Text="Claims on Policy Display" Visible="False"></asp:CheckBox>
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-3">
                                                <br />
                                                <asp:CheckBox ID="chkClientDisp" TabIndex="29" runat="server" CssClass="labelForControl"
                                                    Text="Client Display"></asp:CheckBox>
                                                <br />
                                                <asp:CheckBox ID="chkLegalNameDisp" TabIndex="29" runat="server" CssClass="labelForControl"
                                                    Text="Legal Name Display"></asp:CheckBox>
                                                <br />
                                                <asp:CheckBox ID="chkPolicyDisplay" TabIndex="29" runat="server" CssClass="labelForControl"
                                                    Text="Policy Display" ></asp:CheckBox>
                                                <br />
                                                <asp:CheckBox ID="chkPersonPolicyDisp" TabIndex="29" runat="server" CssClass="labelForControl"
                                                Text="Person on Policy Display"></asp:CheckBox>
                                                <MaskedInput:MaskedTextHeader ID="MaskedTextHeader1" runat="server"></MaskedInput:MaskedTextHeader>
                                                <asp:Label ID="lblAddress2" Style="z-index: 101; left: 441px; position: absolute;
                                                    top: 536px" Width="51px" Height="10px" runat="server" Visible="False">Urbanization</asp:Label>
                                                <asp:TextBox ID="txtAddress2" Style="z-index: 102; left: 541px; position: absolute;
                                                    top: 532px" TabIndex="25" MaxLength="30" Width="163px" Height="19px" runat="server"
                                                    Visible="False"></asp:TextBox>
                                                <asp:CheckBox ID="ChkOptOut" Style="z-index: 103; left: 12px; position: absolute;
                                                    top: 132px" TabIndex="36" Width="71px" runat="server" CssClass=" headForm1 "
                                                    Text="Opt-Out" AutoPostBack="True" Visible="False"></asp:CheckBox>
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
                                            COMMENTS
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-10" align="center">
                                                <br />
                                                <asp:Label ID="lblCustomerComments" runat="server" Text="Comments: " Visible="False"
                                                    CssClass="labelForControl"></asp:Label>
                                                <asp:TextBox ID="txtCustomerComments" runat="server" TextMode="MultiLine" Width="500px"
                                                    Height="90px" CssClass="form-controlWhite" MaxLength="500"></asp:TextBox>
                                                <br />
                                                <asp:Button ID="btnCustomerComments" runat="server" OnClick="btnCustomerComments_Click"
                                                    Text="Add Comment" CssClass="btn btn-primary btn-lg" Width="500px" />
                                                <br />
                                                <br />
                                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="100%" />
                                                <br />
                                                <%-- </div>--%>
                                                <br />
                                                <%-- --%><div class="col-sm-10" align="center">
                                                    <%--<div class="table-responsive" align="center" style="width:100%">--%>
                                                    <asp:GridView ID="GridComments" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        CellPadding="0" Font-Bold="False" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center"
                                                        PageSize="12" Width="75%" CssClass="table table-striped" OnRowCommand="GridComments_RowCommand"
                                                        OnRowCreated="GridComments_RowCreated" OnPageIndexChanging="GridComments_PageIndexChanging">
                                                        <AlternatingRowStyle HorizontalAlign="Center" BackColor="#EBEBEB" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Comments" HeaderText="Comments">
                                                                <ControlStyle Font-Size="13pt" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="EntryDate" HeaderText="Entry Date">
                                                                <ControlStyle Font-Size="13pt" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="CustomerNo" HeaderText="CustomerNo" Visible="False">
                                                                <ControlStyle Font-Size="12pt" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle ForeColor="Black" />
                                                        <HeaderStyle BackColor="Gray" ForeColor="White" Height="30px" HorizontalAlign="Center"
                                                            Font-Bold="True" />
                                                        <PagerStyle BackColor="White" HorizontalAlign="Left" />
                                                        <RowStyle HorizontalAlign="Center" BorderColor="#EBEBEB" />
                                                        <SelectedRowStyle BackColor="White" CssClass="" HorizontalAlign="Center" />
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                    </asp:GridView>
                                                    <%-- </div>--%>
                                                </div>
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
                                    <Toolkit:AccordionPane ID="AccordionPane5" runat="server">
                                        <Header>
                                            CLAIM INFORMATION
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            <div class="col-sm-1">
                                            </div>
                                            <%--AQUI--%>
                                            <div class="col-sm-10" align="center">
                                                <br />
                                                 <div class="col-sm-10" align="left">
                                                    <asp:Label ID="lbltotalclaims" style="margin-left:180px;" runat="server" Text="TOTAL CLAIMS:" Visible="False"
                                                        CssClass="labelForControl"></asp:Label>
                                                </div>
                                                <br />
                                                <div align="center" style="width:100%;">
                                                    <asp:GridView ID="GridClaims" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        CellPadding="0" Font-Bold="False" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center"
                                                        PageSize="12" Width="100%" CssClass="table table-striped">
                                                        <AlternatingRowStyle HorizontalAlign="Center" BackColor="#EBEBEB" />
                                                        <Columns>
                                                            <asp:BoundField DataField="PolicyNumber" HeaderText="Policy Number">
                                                                <ControlStyle Font-Size="13pt" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ClaimNo" HeaderText="ClaimNo">
                                                                <ControlStyle Font-Size="13pt" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="LossType" HeaderText="Loss Type">
                                                                <ControlStyle Font-Size="13pt" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ClaimStatus" HeaderText="Claim Status">
                                                                <ControlStyle Font-Size="12pt" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Asl" HeaderText="Asl">
                                                                <ControlStyle Font-Size="12pt" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ClaimPaid" HeaderText="Claim Paid" DataFormatString="{0:c}" HtmlEncode="False">
                                                                <ControlStyle Font-Size="12pt" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Flag" HeaderText="Flag">
                                                                <ControlStyle Font-Size="12pt" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Surcharge" HeaderText="Surcharge %">
                                                                <ControlStyle Font-Size="12pt" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle ForeColor="Black" />
                                                        <HeaderStyle BackColor="Gray" ForeColor="White" Height="30px" HorizontalAlign="Center"
                                                            Font-Bold="True" />
                                                        <PagerStyle BackColor="White" HorizontalAlign="Left" />
                                                        <RowStyle HorizontalAlign="Center" BorderColor="#EBEBEB" />
                                                        <SelectedRowStyle BackColor="White" CssClass="" HorizontalAlign="Center" />
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                    </asp:GridView>
                                                    <%-- </div>--%>
                                                </div>
                                            </div>
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>
                        </div>
                    </div>
                    <!--#region UPLOAD DOCUMENTS  -->
                   <div style="font-weight: 700">

                       <asp:Panel ID="pnlAdjunto" runat="server" BackColor="#F4F4F4" CssClass="" Width="750px">
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
                            <div style="width: 900px; margin-left: auto; margin-right: auto; margin-top: 20px;">
                            </div>
                       </asp:Panel>                
                       <asp:ModalPopupExtender ID="mpeAlert" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" PopupControlID="pnlAlert" TargetControlID="btnDummyAlert" OkControlID="btnAceptar">
                    </asp:ModalPopupExtender>
                    <asp:Button ID="btnDummyAlert" runat="server" Visible="true" BackColor="Transparent" BorderStyle="None"
                        BorderWidth="0" BorderColor="Transparent" />
                         <asp:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server"
                                TargetControlID="pnlAlert" Radius="10" Corners="All" />
                    </div>
                    <!--#endregion UPLOAD DOCUMENTS  -->
                    <div style="height: 100%">
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
                         </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
   
     </div>
    </form>
</body>
