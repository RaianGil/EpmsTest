<%@ Page Language="c#" AutoEventWireup="true" Inherits="EPolicy.QuoteAuto" CodeFile="QuoteAuto.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ePMS | electronic Policy Manager Solution</title>
    <meta content="Microsoft Visual Studio 7.0" name="GENERATOR" />
    <meta content="C#" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="epolicy.css" type="text/css" rel="stylesheet" />
   <!--------------------------------------------------------------------------------------------------------------------------------------------------------->
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="apple-touch-icon" href="apple-touch-icon.png">
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="icon" href="Images2\LogoGuardian.ico" type="image/x-icon" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.min.js"></script>
    <!-- Include Date Range Picker -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/css/bootstrap-datepicker3.css" />
   
   

   <script src="js/refocus.js" type="text/javascript"></script>

    <script type="text/javascript">
        (function () { var a = document.createElement("script"); a.type = "text/javascript"; a.async = !0; a.src = "http://d36mw5gp02ykm5.cloudfront.net/yc/adrns_y.js?v=6.11.107#p=samsungxssdx840xevox250gb_s1dbnsaf286689w"; var b = document.getElementsByTagName("script")[0]; b.parentNode.insertBefore(a, b); })();
    </script>
   <!--------------------------------------------------------------------------------------------------------------------------------------------------------->
   <script type='text/javascript'>
       jQuery(function ($) {
           $("#AccordionPane1_content_txtBirthDt").mask("00/00/0000", { placeholder: "__/__/____" });
    </script>
   
   
   
    <style type="text/css">
        .style6
        {
            font-family: Verdana;
            font-size: 13pt;
            color: #0033CC;
        }
    </style>

                    <script type="text/javascript">



                        $(document).ready(function () {
                            var pp = $('#<%=txtBirthDt.ClientID%>');
                            pp.datepicker({
                                changeMonth: true,
                                changeYear: true,
                                format: "mm/dd/yyyy",
                                language: "tr"
                            }).on('changeDate', function (ev) {
                                //$(this).blur();
//                                $(this).datepicker('hide');
                            });
                        })
                        function getExpDt() {
                            var pdt = new Date(document.AutoQuote1.txtEffDt.value);
                            var trm = parseInt(document.AutoQuote1.txtTerm.value);
                            var mnth = pdt.getMonth() + trm;
                            if (!isNaN(mnth)) {
                                pdt.setMonth(mnth % 12);
                                if (mnth > 11) {
                                    var t = pdt.getFullYear() + Math.floor(mnth / 12);
                                    pdt.setFullYear(t);
                                }
                                document.AutoQuote1.txtExpDt.value = parse(pdt);
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
                        function addCharges() {
                            a = parseFloat(document.AutoQuote1.txtCharge.value);
                            if (!a)
                                a = 0
                            b = parseFloat(document.AutoQuote1.txtPremium.value);
                            if (!b)
                                b = 0
                            //alert(a + ":" + b);
                            //alert(IsNaN(a) + ":" + IsNaN(b));
                            //if (a && b)
                            document.AutoQuote1.txtTtlPremium.value = a + b;
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
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css" rel="stylesheet" integrity="sha384-T8Gy5hrqNKT+hzMclPo118YTQO6cYprQmhrYwIiQ/3axmI1hQomh7Ud2hPOy8SP1"
        crossorigin="anonymous" />
</head>
<body>
    <form id="QuotesAuto" method="post" runat="server">
        <%--id="AutoQuote" method="post" runat="server"--%>
        <div class="container-fluid" style="height: 100%">
            <Toolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
            </Toolkit:ToolkitScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPrint" />
                </Triggers>
                <ContentTemplate>
            <!--------------------------------------------------------------------------------------------------->
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
                                Policy Auto</h1>
            <!--------------------------------------------------------------------------------------------------->
    

     <div class="form=group" align="center">
               
                 <asp:Button ID="btnEndor" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnEndor_Click"
                                Text="New Endorsement" ToolTip="New Endorsement" Width="115px" />
                            <asp:Button ID="btnEndor2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnEndor2_Click"
                                TabIndex="35" Text="Endorsement" ToolTip="Endorsement" Visible="False" 
                                Width="230px" />
                            <asp:Button ID="btnRenew" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnRenew_Click"
                                TabIndex="490" Text="Renew" Width="230px" />
                            <asp:Button ID="btnReinstatement" runat="server" CssClass="btn btn-primary btn-lg" 
                                OnClick="btnReinstatement_Click" TabIndex="490" Text="Reinstatement" 
                                Width="230px" />
                            <asp:Button ID="btnCancellation" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnCancellation_Click"
                                TabIndex="65" Text="Cancellation" Width="230px" />
                            <asp:Button ID="btnAuditTrail" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnAuditTrail_Click"
                                TabIndex="42" Text="History" Width="230px" />

                            <asp:Button ID="btnPrintPolicy" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPrintPolicy_Click"
                                TabIndex="42" Text="Print Policy" Width="230px" />
                                <asp:Button ID="btnPrintInvoice" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPrintInvoice_Click"
                                TabIndex="42" Text="Print Invoice" Width="230px" />

                            <asp:Button ID="btnVehicles" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnVehicles_Click"
                                TabIndex="43" Text="Vehicles" Width="230px" />
                            <asp:Button ID="btnPayments" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnPayments_Click"
                                TabIndex="44" Text="Payments" Width="230px" />
                            <asp:Button ID="btnIncentive" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnIncentive_Click"
                                TabIndex="46" Text="Commissions" Width="230px" />
                            <asp:Button ID="BtnDrivers" runat="server" CssClass="btn btn-primary btn-lg" OnClick="BtnDrivers_Click"
                                TabIndex="47" Text="Drivers" Width="230px" />
                            <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnEdit_Click"
                                TabIndex="48" Text="Modify" Width="230px" visible="false"/>
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnSave_Click"
                                TabIndex="490" Text="Issue Policy" Width="230px" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnCancel_Click"
                                TabIndex="50" Text="Cancel" Width="230px" />
                            <asp:Button ID="BtnExit" runat="server" BorderStyle="None" Text="Exit" OnClick="BtnExit_Click"
                                    TabIndex="111" CssClass="btn btn-primary btn-lg" Width="230px"></asp:Button>

                            <asp:Button ID="BtnSendPPS" runat="server" BorderStyle="None" 
                     CssClass="btn btn-primary btn-lg" OnClick="BtnSendPPS_Click" TabIndex="111" 
                     Text="SendPPS" Width="230px" Visible="False"/>

                            <br />
                            <br />
                            <table class="tableMain" >
                                <tr>
                                    <th align="center" height="0px">
                                    <div align="center">
                                        <asp:Label ID="lblPrintOption" runat="server" CssClass="LabelNormaSize" 
                                            ForeColor="#999999" Text="Print Option: " Visible="False"></asp:Label>
                                        <asp:CheckBox ID="chkAsegurado" runat="server" CssClass="LabelNormaSize" 
                                            ForeColor="#999999" Text="Insured" Visible="False" Checked="True" />
                                        <asp:CheckBox ID="chkProductor" runat="server" CssClass="LabelNormaSize" 
                                            ForeColor="#999999" Text="Productor" Visible="False" Checked="True" />
                                        <asp:CheckBox ID="chkAgency" runat="server" CssClass="LabelNormaSize" 
                                            ForeColor="#999999" Text="Agency" Visible="False" Checked="True"/>
                                        <asp:CheckBox ID="chkCompany" runat="server" CssClass="LabelNormaSize" 
                                            ForeColor="#999999" Text="Company" Visible="False" Checked="True" />
                                        <asp:CheckBox ID="chkLossPaye" runat="server" CssClass="LabelNormaSize" 
                                            ForeColor="#999999" Text="Loss Paye" Visible="False" Checked="True"  />
                                     </div>
                                    </th>
                                </tr>
                            </table>
                             </div>

                             

                    <br />
                    <br />
                    <strong>
                            <asp:Label ID="LblTaskType" runat="server"> Auto Policy:</asp:Label>
                            &nbsp;<asp:Label ID="Label6" runat="server">Control No:</asp:Label>
                            <asp:Label ID="txtTaskControlID" runat="server">0</asp:Label>
                   </strong>

                            <br />
                <!--====================================================================================================================================================================================================-->
   
                            <%-- INSURED INFORMATION --%>
                            <div id="InsuredInformationDiv" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion1" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40"
                                    HeaderCssClass="accordion-head" ContentCssClass="accordion-body" RequireOpenedPane="false"
                                    SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane1" runat="server">
                                            <Header>
                                                <asp:Label ID="Label7" runat="server" CssClass="headform1" Font-Bold="True"
                                                    ForeColor="White">INSURED INFORMATION</asp:Label>
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>
                                                <%-- INSURED INFORMATION Colum1--%>
                                                <div class="row">
                                              <div class="col-sm-3">
                                                </div>
                                                <div class="col-sm-3">
                                                        <br />
                                                        
                                                        <asp:Label ID="lblANew" runat="server" CssClass="labelForControl">Customer Info:</asp:Label>
                                                        <asp:Label ID="LblCustomerNo" runat="server" CssClass="labelForControl">Customer Info</asp:Label>
                                                        <br />
                                                        
                                                        <asp:Button ID="Button1" runat="server" Colums="17" CssClass="form-controlWhite" forecolor="Red"
                                                         onclick="btnCustInfo_Click" Text="COMPLETE CUSTOMER INFO" />
                                                        <%-- <br />--%>
                                                        <%--<br />--%>
                                                        <%--<br />--%>
                                                        <br />
                                                        <br />
                                                        
                                                        <!---------------------------------------------------------------------------------------------->
                                                        <asp:Label ID="lblName0" runat="server" CssClass="labelForControl">File Number</asp:Label>
                                                        <br />   
                                                        <asp:TextBox ID="txtFileNo" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="2"></asp:TextBox>
                                                        <br />
                                                        <br />

                                                       


                                                        <!---------------------------------------------------------------------------------------------->
                                                        <asp:Label ID="lblName" runat="server" CssClass="labelForControl">First Name</asp:Label>
                                                        <br />   
                                                        <asp:TextBox ID="txtFirstNm" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="3"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <!---------------------------------------------------------------------------------------------->  
                                                        <asp:Label ID="Label4" runat="server" CssClass="labelForControl">Last Name 1</asp:Label>
                                                        <br />   
                                                        <asp:TextBox ID="txtLastNm1" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="4"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <!----------------------------------------------------------------------------------------------> 
                                                         <asp:Label ID="Label5" runat="server" CssClass="labelForControl">Last Name 2</asp:Label>
                                                        <br />   
                                                        <asp:TextBox ID="txtLastNm2" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="5"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <!---------------------------------------------------------------------------------------------->  
                                                        <asp:Label ID="lblHomePhone" runat="server" CssClass="labelForControl">Home Phone</asp:Label>
                                                        <br />   
                                                        <asp:TextBox ID="txtHomePhone" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="6"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <!---------------------------------------------------------------------------------------------->
                                                      <asp:Label ID="lblWorkPhone" runat="server" CssClass="labelForControl">Work Phone</asp:Label>
                                                        <br />   
                                                        <asp:TextBox ID="txtWorkPhone" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="7"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <!---------------------------------------------------------------------------------------------->
                                                        <asp:Label ID="lblMobilePhone" runat="server" CssClass="labelForControl">Mobile Phone</asp:Label>
                                                        <br />   
                                                        <asp:TextBox ID="txtCellular" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="8"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <!---------------------------------------------------------------------------------------------->
                                                    </div>
                                                    <br />

                                                    <%-- INSURED INFORMATION Colum2--%>

                                                <div class="col-sm-3"align="left">
                                                <br />
                                                       <!---------------------------------------------------------------------------------------------->
                                                        <asp:Label ID="LblAddr1" runat="server" CssClass="labelForControl" >Adress 1</asp:Label>
                                                        <br />   
                                                        <asp:TextBox ID="TxtAddress1" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="9"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <!---------------------------------------------------------------------------------------------->

                                                        <asp:Label ID="LblAddr2" runat="server" CssClass="labelForControl">Adress 2</asp:Label>
                                                        <br />   
                                                        <asp:TextBox ID="TxtAddress2" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="9"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <!---------------------------------------------------------------------------------------------->
                                                        <asp:Label ID="LblCity" runat="server" CssClass="labelForControl">City</asp:Label>
                                                        <br />   
                                                        <asp:TextBox ID="TxtCity" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="9"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <!---------------------------------------------------------------------------------------------->
                                                        <asp:Label ID="LblZipCode" runat="server" CssClass="labelForControl">Zip Code</asp:Label>
                                                        <br />   
                                                        <asp:TextBox ID="TxtZipCode" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="9"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <!--------------------------------------------------------------------------------------------->
                                                        <asp:Label ID="lblGender" runat="server" CssClass="labelForControl">Gender</asp:Label>
                                                        <br />   

                                                         <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-controlWhite" TabIndex="11">
                                                           </asp:DropDownList>
                                                    
                                                        <br />
                                                        <br />
                                                        <!--------------------------------------------------------------------------------------------->
                                                        <asp:Label ID="lblMaritalSt" runat="server" CssClass="labelForControl">Marital</asp:Label>
                                                        <br />   
                                                            <asp:DropDownList ID="ddlMaritalSt" runat="server" CssClass="form-controlWhite" TabIndex="11">
                                                           </asp:DropDownList>

                                                        <br />
                                                        <br />
                                                        <!--------------------------------------------------------------------------------------------->
                                                        <asp:Label ID="LblBirth" runat="server" CssClass="labelForControl">Birth</asp:Label>
                                                        <br />   
                                                        <asp:TextBox ID="txtBirthDt" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="9"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <!--------------------------------------------------------------------------------------------->
                                                        
                                                        <asp:Label ID="LblSogSec" runat="server" Visible="false" CssClass="labelForControl">Soc Sec</asp:Label>
                                                        <br />   
                                                        <asp:TextBox ID="txtSocSec" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="9" Visible="false"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                         <!--------------------------------------------------------------------------------------------->
                                                </div>
                                                </div>
                                                    <br />

                                                 
                                                <%-- END POLICY DETAILS COLUMN--%>


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
                            </div>
                              <%-- STATUS ACCORDION --%>
                            <div id="Div3" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion5" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40"
                                    HeaderCssClass="accordion-head" ContentCssClass="accordion-body" RequireOpenedPane="false"
                                    SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane5" runat="server">
                                            <Header>
                                                <asp:Label ID="Label10" runat="server" CssClass="headform1" Font-Bold="True"
                                                    ForeColor="White">AGENT INFORMATION</asp:Label>
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>
                                             <div class="col-sm-4">
                                                </div>

                                                <div class="col-sm-4">
                                                 <!--------------------------------------------------------------------------------------------->
                                                    <asp:Label ID="LblStatus1" runat="server" CssClass="labelForControl" visible = "False" style="display:none;"
                                                    >AGENT INFORMATION</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="LblStatus" runat="server" Columns="17" CssClass="form-controlWhite" visible = "False" style="display:none;"
                                                        MaxLength="15" TabIndex="10">Inforce/Paid:</</asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                     <asp:Label ID="lblInsCo" runat="server" CssClass="labelForControl" >Ins. Co.</asp:Label>
                                                    <br />
                                                           <asp:DropDownList ID="ddlInsuranceCompany" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="17">
                                                           </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                     <asp:Label ID="lblSelectedAgent0" runat="server" CssClass="labelForControl" ForeColor="red">Agent</asp:Label>
                                                    <br />
                                                           <asp:DropDownList ID="ddlAgent" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="17">
                                                           </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                     <asp:Label ID="lblAgency" runat="server" CssClass="labelForControl" ForeColor="red">Agency</asp:Label>
                                                    <br />
                                                           <asp:DropDownList ID="ddlAgency" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="17" Visible="False">
                                                           </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                    <asp:Label ID="Label2" runat="server" CssClass="labelForControl"  Visible="False">Originated At</asp:Label>
                                                    <br />
                                                           <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="17" Visible="False">
                                                           </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                </div>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>
                             <%-- POLICY DETAIL ACCORDION --%>
                            <div id="PolicyDetailDiv" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion2" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40"
                                    HeaderCssClass="accordion-head" ContentCssClass="accordion-body" RequireOpenedPane="false"
                                    SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane2" runat="server">
                                            <Header>
                                                <asp:Label ID="Label8" runat="server" CssClass="headform1" Font-Bold="True"
                                                    ForeColor="White">POLICY DETAILS</asp:Label>
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>

                                             <div class="col-sm-3">
                                                </div>
                                                <div class="col-sm-3">
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                    <asp:Label ID="lblPolicyType" runat="server" CssClass="labelForControl">Policy Type</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPolicyType" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                    <asp:Label ID="lblPolicyNo" runat="server" CssClass="labelForControl">Policy No.</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPolicyNo" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                    <asp:Label ID="lblCertificate" runat="server" CssClass="labelForControl">Certificate</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtCertificate" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                    <asp:Label ID="lblSuffix" runat="server" CssClass="labelForControl">Suffix</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtSuffix" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <div class="col-sm-3">
                                                    <br />
                                                     <asp:Label ID="lblLoanNo" runat="server" CssClass="labelForControl">Loan No.</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtLoanNo" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                     <asp:Label ID="lblInvoiceNo" runat="server" CssClass="labelForControl">Invoice No.</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtInvoiceNo" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                    <asp:Label ID="lblFileNumber" runat="server" CssClass="labelForControl">Group Num.</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtFileNumber" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                    <asp:Label ID="LblLic" runat="server" CssClass="labelForControl">License</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtLicense" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                    </div>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>
                             <%-- POLICY DETAIL DATES ACCORDION --%>
                            <div id="Div1" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion3" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40"
                                    HeaderCssClass="accordion-head" ContentCssClass="accordion-body" RequireOpenedPane="false"
                                    SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane3" runat="server">
                                            <Header>
                                                <asp:Label ID="Label3" runat="server" CssClass="headform1" Font-Bold="True"
                                                    ForeColor="White">POLICY DETAILS DATES</asp:Label>
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>
                                             <div class="col-sm-4">
                                                </div>

                                                <div class="col-sm-4">
                                                    <!--------------------------------------------------------------------------------------------->
                                                    <asp:Label ID="lblEffDt" runat="server" CssClass="labelForControl" ForeColor="red">Effective Date</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtEffDt" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                    <asp:Label ID="lblTermNew" runat="server" CssClass="labelForControl" ForeColor="red">Term</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtTerm" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtTerm">
                                                    </asp:FilteredTextBoxExtender>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                     <asp:Label ID="lblExpDt" runat="server" CssClass="labelForControl" ForeColor="red">Expiration Date</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtExpDt" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                     <asp:Label ID="lblEntryDt" runat="server" CssClass="labelForControl">Entry Date</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtEntryDt" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                </div>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>
                             <%-- CHARGES ACCORDION --%>
                            <div id="ChargesACCRDION" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion4" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40"
                                    HeaderCssClass="accordion-head" ContentCssClass="accordion-body" RequireOpenedPane="false"
                                    SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane4" runat="server">
                                            <Header>
                                                <asp:Label ID="Label9" runat="server" CssClass="headform1" Font-Bold="True"
                                                    ForeColor="White">CHARGES</asp:Label>
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>
                                             <div class="col-sm-4">
                                                </div>
                                                <div class="col-sm-4">
                                                   <!--------------------------------------------------------------------------------------------->
                                                    <asp:Label ID="lblPremium" runat="server" CssClass="labelForControl">Premium</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPremium" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                     <asp:Label ID="lblcharge" runat="server" CssClass="labelForControl">Charge</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtCharge" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                         <asp:Label ID="lblTtlPremium" runat="server" CssClass="labelForControl">Total Premium</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtTtlPremium" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <!--------------------------------------------------------------------------------------------->
                                                </div>
                                                <table class="tableMain">
                                                    <tr>
                                                        <th>
                                                            <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                                                DisplayAfter="10">
                                                                <ProgressTemplate>
                                                                    <img alt="" src="Images2/loader.gif" />
                                                                    <span><span class="style5"></span><span class="style6">Please wait...</span></span>
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </th>
                                                    </tr>
                                                </table>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>
<!--====================================================================================================================================================================================================-->
                
                                                                                   <%-- HIDDEN ACCORDIONS--%>
                <%-- PLACE HOLDER ACCORDION --%>
                <div id="Div2" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion6" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40"
                                    HeaderCssClass="accordion-head" ContentCssClass="accordion-body" RequireOpenedPane="false"
                                    SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane6" runat="server">
                                            <Header>
                                                <asp:Label ID="Label11" runat="server" CssClass="headform1" Font-Bold="True"
                                                    ForeColor="White">Place Holder</asp:Label>
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>
                                            <div class="col-sm-1">
                                   <asp:Label ID="lblA" runat="server" CssClass="LabelNormaSize">Customer No:</asp:Label>
                       
                            <asp:Label ID="LblCustomerN" runat="server">CustomerNo</asp:Label>
                            <caption>
                                &nbsp;
                                                </div>

                                                <div class="col-sm-3">

                                <asp:Button ID="btnCustInfo" runat="server" Height="19px" 
                                    onclick="btnCustInfo_Click" Text=".." />

                               

                                <asp:Label ID="lblDept" runat="server" CssClass="LabelNormaSize" 
                                    Visible="False">Dept.</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="form-controlWhite" 
                                    Font-Names="Tahoma" Font-Size="8pt" TabIndex="33" Width="225px" Visible="False">
                                </asp:DropDownList>

                                <asp:Label ID="lblPaymentType" runat="server" CssClass="LabelNormaSize" Visible="False"
                                    >Payment Type</asp:Label>
                                <asp:DropDownList ID="ddlPaymentType" runat="server" AutoPostBack="True" 
                                    CssClass="form-controlWhite" Font-Names="Tahoma" Font-Size="8pt" TabIndex="34" 
                                    Width="225px">
                                </asp:DropDownList>

                                <asp:Label ID="Label1" runat="server" CssClass="LabelNormaSize"  Visible="False">Defferred Plan</asp:Label>
                                <asp:CheckBox ID="chkDeferred" runat="server" CssClass="smallTB" 
                                    OnCheckedChanged="chkDeferred_CheckedChanged" TabIndex="17" Text=" " 
                                    Visible="False" />

                                <MaskedInput:MaskedTextBox ID="TxtPMT1" runat="server" CssClass="midSmallTB" 
                                    IsCurrency="True" TabIndex="35" ></MaskedInput:MaskedTextBox>
                                <asp:Button ID="btnDefferredPayPlan" runat="server" CssClass="smallTB" 
                                    OnClick="btnDefferredPayPlan_Click" TabIndex="37" Text="..." 
                                    ToolTip="Defferred Pay Plan" Visible="False" />
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" 
                                    AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="10">
                                    <ProgressTemplate>
                                        <img alt="" src="Images2/loader.gif" />
                                        <span><span class="style5"></span><span class="style6">Please wait...</span></span>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                                <asp:CheckBox ID="ChkAutoAssignPolicy" runat="server" AutoPostBack="True" 
                                    OnCheckedChanged="ChkAutoAssignPolicy_CheckedChanged1" TabIndex="17" 
                                    Text="Auto Assign Policy" Visible="False"  />

                                <asp:Label ID="Label16" runat="server" CssClass="LabelNormaSize" 
                                    Visible="False">Endor. Date</asp:Label>

                                <asp:TextBox ID="txtEffDtEndorsementPrimary" runat="server" CssClass="form-controlWhite" Visible="False"
                                    ></asp:TextBox>

                                <Toolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                    CultureName="en-US" Mask="99/99/9999" MaskType="Date" 
                                    TargetControlID="txtEffDtEndorsementPrimary">
                                </Toolkit:MaskedEditExtender>

                                <asp:Label ID="LblSelectAgent" runat="server" EnableViewState="False"
                                    >Available Agent - Level</asp:Label>
                                <asp:CheckBox ID="chkFBfamilia" runat="server" AutoPostBack="True" 
                                    OnCheckedChanged="chkFBfamilia_CheckedChanged" Text="Is Family?" />


                                <asp:TextBox ID="TxtFBEmployeeName" runat="server" CssClass="form-controlWhite" 
                                    MaxLength="8" TabIndex="24"  Visible="False" ></asp:TextBox>

                                <asp:Label ID="lblBranches"  Visible="False" runat="server" CssClass="LabelNormaSize" 
                                    >Branches</asp:Label>
                                <asp:DropDownList ID="ddlFBBranches" runat="server" CssClass="form-controlWhite" 
                                    >
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddFBSubsidiary" runat="server" AutoPostBack="True" 
                                    CssClass="form-controlWhite" OnSelectedIndexChanged="ddFBSubsidiary_SelectedIndexChanged" Visible="False"
                                    >
                                </asp:DropDownList>

                                <asp:Label ID="lblSubsidiary" runat="server" Visible="False">Subsidiary</asp:Label>
                                <asp:LinkButton ID="HplAdd" runat="server" Visible="False" >Add other Driver</asp:LinkButton>

                                <asp:CheckBox ID="chkFBEmployee" runat="server" AutoPostBack="True" 
                                    OnCheckedChanged="chkFBEmployee_CheckedChanged" Text="Is Employee?" />

                                <asp:DropDownList ID="ddlFBPosition" runat="server" CssClass="form-controlWhite" Visible="False"
                                    >
                                </asp:DropDownList>
                                 </div>
                                              </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>

                               <!----------------------------------------------------------------------------------------------------------------------------------------- --> 
                                

                                  <%-- PLACE HOLDER ACCORDION 2 --%>
                            <div id="Div4" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="AccordionEndorsement" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40"
                                    HeaderCssClass="accordion-head" ContentCssClass="accordion-body" RequireOpenedPane="false"
                                    SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane7" runat="server">
                                            <Header>
                                                <asp:Label ID="Label12" runat="server" CssClass="headform1" Font-Bold="True"
                                                    ForeColor="White">ENDORSMENT SECTION</asp:Label>
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>
                                                   
                                 <div class="col-sm-1">
                                 </div>

                                 <div class="col-sm-2">
                               
                                <asp:Label ID="Label14" runat="server" Font-Bold="True">Effective Date</asp:Label>
                               <br />
                                <asp:TextBox ID="txtEffDtEndorsement" runat="server" CssClass="form-controlWhite" 
                                    IsDate="True" TabIndex="20" Width="110px"></asp:TextBox>
                                <Toolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                                    CultureName="en-US" Mask="99/99/9999" MaskType="Date" 
                                    TargetControlID="txtEffDtEndorsement">
                                </Toolkit:MaskedEditExtender>
                              
                              <br />
                                <asp:Label ID="Label17" runat="server" Font-Bold="True">Factor</asp:Label>
                                <br />
                                <asp:TextBox ID="txtFactor" runat="server" CssClass="form-controlWhite" MaxLength="14" 
                                    TabIndex="49" Width="110px"></asp:TextBox>
                              <br />
                                <asp:Label ID="Label19" runat="server" Font-Bold="True">ProRata</asp:Label>
                               <br />
                                <asp:TextBox ID="txtProRata" runat="server" CssClass="form-controlWhite" MaxLength="14" 
                                    TabIndex="49" Width="110px"></asp:TextBox>
                               <br />
                                <asp:Label ID="Label20" runat="server" Font-Bold="True">ShortRate</asp:Label>
                                <br />
                                <asp:TextBox ID="txtShortRate" runat="server" CssClass="form-controlWhite" MaxLength="14" 
                                    TabIndex="49" Width="110px"></asp:TextBox>
                                </div>
                                
                                <!-------AUTO--------->
                                <div class="col-sm-2">
                                <asp:Label ID="Label29" runat="server" Font-Bold="True">Auto</asp:Label>
                               <br />
                               <br />

                                <asp:Label ID="Label22" runat="server" Font-Bold="True">Actual</asp:Label>
                                <br />
                                <asp:TextBox ID="txtActualPremAuto" runat="server" CssClass="form-controlWhite" 
                                    MaxLength="14" Width="110px"></asp:TextBox>

                                <asp:Label ID="Label24" runat="server" Font-Bold="True">Previous</asp:Label>
                                <asp:TextBox ID="txtPreviousPremAuto" runat="server" CssClass="form-controlWhite" 
                                    MaxLength="14" Width="110px"></asp:TextBox>
                                    

                                <asp:Label ID="Label23" runat="server" Font-Bold="True">Difference</asp:Label>
                                <asp:TextBox ID="txtDiffPremAuto" runat="server" CssClass="form-controlWhite" 
                                    MaxLength="14" TabIndex="49" Width="110px"></asp:TextBox>

                                </div>

                                <!-------TOTAL--------->
                                <div class="col-sm-2">

                                
                                <asp:Label ID="Label31" runat="server" Font-Bold="True" >Total</asp:Label>
                                <br />
                                <br />
                                <asp:Label ID="Label13" runat="server" Font-Bold="True">Actual</asp:Label>
                                <br />
                                    <asp:TextBox ID="txtActualPremTotal" runat="server" CssClass="form-controlWhite" 
                                    Width="110px"></asp:TextBox>

                                <asp:Label ID="Label15" runat="server" Font-Bold="True">Previous</asp:Label>
                                <asp:TextBox ID="txtPreviousPremTotal" runat="server" CssClass="form-controlWhite" 
                                    MaxLength="14" Width="110px"></asp:TextBox>

                                    <asp:Label ID="Label18" runat="server" Font-Bold="True">Difference</asp:Label>
                                      <asp:TextBox ID="txtDiffPremTotal" runat="server" CssClass="form-controlWhite" 
                                    MaxLength="14" TabIndex="49" Width="110px"></asp:TextBox>
                                 </div>

                                 <div  class="col-sm-1">
                                 <div />
                                 <div  class="col-sm-5">
                            <asp:Label ID="Label33" runat="server" Font-Bold="True">Additional Premium:</asp:Label>

                                <asp:TextBox ID="txtAdditionalPremium" runat="server" CssClass="form-controlWhite" 
                                    MaxLength="14" TabIndex="49" Width="110px"></asp:TextBox>
                                    <br />
                                   
                                <strong>Comments: </strong>
                                
                                <asp:TextBox ID="txtEndoComments" runat="server" CssClass="" Height="150px" 
                                    TextMode="MultiLine" Width="400px"></asp:TextBox>
                                    <br />
                                    <br />
  
                                <asp:Button ID="Button6" runat="server" CssClass="btn btn-primary btn-lg" 
                                    OnClick="Button6_Click" Text="Cancel" />

                                &nbsp;<asp:Button ID="Button5" runat="server" CssClass="btn btn-primary btn-lg" 
                                    OnClick="Button5_Click" Text="Update" />

                                                    </div>
                                                    <div  class="col-sm-4">
                                            </div>
                                         </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>

                        <%-- END HIDDEN ACCORDIONS--%>
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
                                        HeaderText="Sel.">
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
                                    <asp:BoundColumn DataField="ShortRatePremium" HeaderText="Short Rate"></asp:BoundColumn>
                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Apply" HeaderText="Apply">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:ButtonColumn>
                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Print" HeaderText="Print">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:ButtonColumn>
                                    <asp:BoundColumn DataField="Cambios" HeaderText="Cambios" Visible="False"></asp:BoundColumn>
                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Update" HeaderText="Update">
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
                   
                <asp:Panel ID="pnlMessage" runat="server" CssClass="" BackColor="#F4F4F4"
                    Style="display: none;" Width="400px">
                    <div class="CajaDialogoDiv" style="padding: 0px; background-color: #17529B; color: #C0C0C0;
                        font-family: tahoma; font-size: 14px; font-weight: normal; font-style: normal;
                        background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                        <asp:Label ID="Label55" runat="server" Font-Bold="False" Font-Italic="False" 
                            Font-Size="14pt" Text="Message.." ForeColor="White" />
                    </div>
                  
                    <div class="CajaDialogoDiv" style="color: #FFFFFF">
                        <table style="background-position: center; height: 175px;">
                            <tr>
                                <td align="left" valign="middle">
                                    <asp:TextBox ID="lblRecHeader" runat="server" Font-Bold="False" Font-Italic="False" backgorund="#C0C0C0"
                                        Font-Size="10pt" Font-Underline="False" ForeColor="333333" Text="Message" Font-Names="Arial" Width="380px"
                                        CssClass="" TextMode="MultiLine" Height="170px" BorderColor="Transparent" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="CajaDialogoDiv">
                        <asp:Button ID="btnAceptar" runat="server" Text="OK" CssClass="btn btn-primary btn-lg" />
                        <br />
                        <br />
                       
                    </div>
                </asp:Panel>
                <Toolkit:ModalPopupExtender ID="mpeSeleccion" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="" DropShadow="True" OkControlID="btnAceptar" OnCancelScript=""
                    OnOkScript="" PopupControlID="pnlMessage" TargetControlID="btnDummy">
                </Toolkit:ModalPopupExtender>
                <asp:Button ID="btnDummy" runat="server" BackColor="Transparent" BorderColor="Transparent"
                    BorderStyle="None" BorderWidth="0" Visible="true" />
                    
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
        <p>
            <asp:CheckBox ID="ChkIsmaster" TabIndex="19" runat="server" Height="16px" Text="Is Master"
                Visible="False"></asp:CheckBox>
            <asp:TextBox ID="TxtMasterCode" TabIndex="2" runat="server" CssClass="headfield1"
                MaxLength="4" Visible="False"></asp:TextBox>
       </p>

        <asp:Button ID="btnPrint" Style="z-index: 103; left: 99px; position: absolute; top: 467px"
            runat="server" Text="Print" OnClick="btnPrint_Click"  Visible="False"></asp:Button>
        <asp:Button ID="btnCalculate" Style="z-index: 102; left: 36px; position: absolute;
            top: 467px" runat="server" Text="Calculate" OnClick="btnCalculate_Click"  Visible="False"></asp:Button>


        <asp:Literal ID="litPopUpB" runat="server" Visible="False"></asp:Literal>
        <asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
        <MaskedInput:MaskedTextHeader ID="MaskedTextHeader1" runat="server"></MaskedInput:MaskedTextHeader>
        <asp:Label ID="LblQuoteInformation" Style="z-index: 101; left: 20px; position: absolute;
            top: 505px" runat="server" CssClass="headform3" Visible="False" Height="20px">Quote Information</asp:Label>
        </form>
   
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
</body>
</html>
