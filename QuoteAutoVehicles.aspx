<%@ Page Language="c#" Inherits="EPolicy.QuoteAutoVehicles" CodeFile="QuoteAutoVehicles.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="cc1" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>ePMS | electronic Policy Manager Solution</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    <link rel="apple-touch-icon" href="apple-touch-icon.png" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="icon" href="Images2\LogoGuardian.ico" type="image/x-icon" />
    <script src="js/jquery-1.12.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.mask.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/bootstrap-theme.min.css" />
    <link rel="stylesheet" href="css/main.css" />
    <link href="css/fonts.css" rel="stylesheet" />


    <style type="text/css">
        .container {
            margin-left: 0px;
            margin-right: 0px;
        }

        collapsabestyle {
            background-color: #FFFFFF;
            padding: 10px 10px;
        }

        .titles {
            position: absolute;
            left: 50%;
            top: 50%;
            transform: translate(-50%, -50%);
            font-family: -webkit-pictograph;
            color: white;
            font-size: 15px;
        }

        .titleclass {
            font-size: 100px;
            text-align: center;
            margin-bottom: 100px;
            color: #17529b;
        }

        .btn {
            width: 150px;
        }

        .content {
            height: 17px;
        }

        .contentmover {
            display: inline-block;
            left: 50%;
            position: relative;
            transform: translateX(-50%);

        }

        .divisionspace {
            margin-bottom: 30px;
        }

        .style6 {
            color: #0033CC;
            font-family: Verdana;
            font-size: 13pt;
        }

        .contentcenter {
            display: inline-block;
            position: relative;
            left: 50%;
            transform: translateX(-50%);
        }

        .centeraccordion {
            position: relative;
            left: 50%;
            transform: translateX(-50%);
            max-width: 100%;
            padding: 0px 10px;
        }

        .grabme2 {
            margin-left: 15px;
        }

        .grabme1 {
            height: 40px;
            position: relative;
            top: 50%;
            left: 50%;
            transform: translate(-50%, 50%);
        }
        
        

        .centeritem {
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, 10%);
        }

        .style7 {
            height: 26px;
        }


        .style8 {
            color: #0033CC;
            font-family: Verdana;
            font-size: 13pt;
            font-weight: normal;
        }

        .hightseparator {
            position: relative;
            cursor: pointer;
            height: 40px;
            background-color: #777777;
            font-weight: 800;
            letter-spacing: 5px;
        }
        .hightseparator:hover{
            background-color: #acacac;
        }
        body{
            height: 100%;
        }
        .principalrow{
            width: 101%;
        }
    </style>

    <script type='text/javascript'>
        jQuery(function ($) {

           // $("#first-container-separator divisionspace centeraccordion_content_txtPurchaseDt").mask("00/00/0000", { placeholder: "__/__/____" });
            $("#MaskedEditValidator3_content_txtPurchaseDt").mask("00/00/0000", { placeholder: "__/__/____" });
            $("#txtExpDate").mask("00/00/0000", { placeholder: "__/__/____" });
        });
    </script>


</head>

<body bgcolor="#e6e6e6">
    <form id="QAV" method="post" runat="server" visible="True">
        <Toolkit:ToolkitScriptManager ID="ScriptManager1" runat="server">
        </Toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <ContentTemplate>
                <!-- page content 2 in cols -->
                <div class="principalrow row">
                    <div class="col-sm-12 col-md-2  sidebar-offcanvas">
                        <input id="Callback" type="hidden" size="1" value="N" name="Callback" runat="server" />
                        <div class="row">
                            <div class="col-sm-12" style="padding-top:50px;padding-left: 30px;">
                                <asp:PlaceHolder ID="Placeholder1" runat="server"></asp:PlaceHolder>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-10" style="padding-top:40px; background-color:#e6e6e6;">
                        <div class="row">
                            <div class="col-sm-12">
                                <p class="titleclass">Vehicles</p>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:Label ID="Label40" runat="server" CssClass="Labelfield2-14" Font-Bold="True">Vehicles:</asp:Label>
                                <asp:Label ID="lblTaskControlID" runat="server">Control No:</asp:Label>
                                <asp:Label ID="txtTaskControlID" runat="server">0</asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" style="text-align: center">
                                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" class="btn btn-primary btn-lg">
                                </asp:Button>
                                <asp:Button ID="btnAssignDrv" runat="server" Text="AssignDrv" Cssclass="btn btn-primary btn-lg"
                                    OnClick="btnAssignDrv_Click"></asp:Button>
                                <asp:Button ID="btnViewCvr" runat="server" Text="Breakdown" CssClass="btn btn-primary btn-lg"
                                    OnClick="btnViewCvr_Click"></asp:Button>
                                <asp:Button ID="btnDrivers" runat="server" Text="Drivers" Cssclass="btn btn-primary btn-lg"
                                    OnClick="btnDrivers_Click"></asp:Button>
                                <asp:Button ID="btnSave" runat="server" Text="Save" Cssclass="btn btn-primary btn-lg"
                                    OnClick="btnSave_Click">
                                </asp:Button>
                                <asp:Button ID="btnAddVhcl" runat="server" Text="Add" Cssclass="btn btn-primary btn-lg"
                                    OnClick="btnAddVhcl_Click"></asp:Button>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Cssclass="btn btn-primary btn-lg"
                                    OnClick="btnCancel_Click"></asp:Button>
                                <asp:Button ID="btnEdit" runat="server" Text="Modify" Cssclass="btn btn-primary btn-lg"
                                    OnClick="btnEdit_Click"></asp:Button>
                                <asp:Button ID="btnBack" runat="server" Text="Exit" Cssclass="btn btn-primary btn-lg"
                                    OnClick="btnBack_Click">
                                </asp:Button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12" style="text-align: center; height: 10px; max-height: 400px;z-index:3;">
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                    DisplayAfter="10">
                                    <ProgressTemplate>
                                        <img alt="" src="Images2/loader.gif" style="width: 35px; height: 35px" />
                                        <span><span class="style5"></span><span class="style8">Please wait...</span></span>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                        <div class="row">
                            <div class="first-container-separator divisionspace centeraccordion">
                                <div class="hightseparator clickfirstcollapse">
                                    <asp:Label ID="lblVehicleInformation" runat="server" CssClass="titles">VEHICLE</asp:Label>
                                </div>
                                <div class="contentcollapse" style=" background-color: #FFFFFF;padding: 10px 10px;">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label9" style="display:block;" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red">New/Used</asp:Label>
                                                <asp:DropDownList ID="ddlNewUsed" runat="server" CssClass="form-controlWhite"
                                                    TabIndex="17" Width="145px">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label34" style="display:block;" runat="server" CssClass="LabelNormaSize">ISO
                                                    Code
                                                </asp:Label>
                                                <asp:TextBox ID="txt1stISO" runat="server" Columns="17" CssClass="form-controlWhite"
                                                    Enabled="False" MaxLength="11" TabIndex="2" Width="145px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="LblBank" style="display:block;" runat="server" CssClass="LabelNormaSize">Bank</asp:Label>
                                                <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-controlWhite"
                                                    TabIndex="11" style="width: 145px;">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                             <%-- Segunda posicion --%>
                                              <asp:Label style="display:block;" ID="Label28" runat="server" CssClass="LabelNormaSize">PolicyAuto
                                                    Type
                                                </asp:Label>
                                                <asp:DropDownList style="width: 145px;" ID="ddlPolicySubClass" runat="server"
                                                    AutoPostBack="True" CssClass="form-controlWhite"
                                                    OnSelectedIndexChanged="ddlPolicySubClass_SelectedIndexChanged1"
                                                    TabIndex="2">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label7" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red" style="display:block;">Cost</asp:Label>
                                                <asp:TextBox ID="txtCost" runat="server" CssClass="form-controlWhite" AutoPostBack="true"
                                                    Width="145px" ontextchanged="txtCost_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label8" style="display:block;" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red">Purchase
                                                    Date
                                                </asp:Label>
                                                <asp:TextBox ID="txtPurchaseDt" runat="server" Columns="10" CssClass="form-controlWhite" 
                                                    IsDate="True" TabIndex="9" style="width: 145px;"></asp:TextBox>
                                                <%--<asp:ImageButton ID="imgPurchaseDt" runat="server" ImageUrl="~/Images2/Calendar.png"
                                                    TabIndex="30" Width="16px" />--%>
                                                <Toolkit:MaskedEditValidator ID="MaskedEditValidator3" runat="server"
                                                    ControlExtender="MaskedEditExtender2" ControlToValidate="txtPurchaseDt"
                                                    InvalidValueMessage="mm/dd/yyyy" TooltipMessage="mm/dd/yyyy"></Toolkit:MaskedEditValidator>
                                                <%--<Toolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy"
                                                    PopupButtonID="imgPurchaseDt" TargetControlID="txtPurchaseDt">
                                                </Toolkit:CalendarExtender>--%>
                                                <Toolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                                                    CultureName="en-US" Mask="99/99/9999" MaskType="Date"
                                                    TargetControlID="txtPurchaseDt">
                                                </Toolkit:MaskedEditExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">

                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                    FilterType="Numbers" TargetControlID="txtCost">
                                                </asp:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <%--Tercera Posicion--%>
                                                  <asp:Label ID="Label4" style="display:block;" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red">Make</asp:Label>
                                                <asp:DropDownList ID="ddlMake" runat="server" AutoPostBack="True"
                                                    CssClass="form-controlWhite" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
                                                    TabIndex="13" Width="145px">
                                                </asp:DropDownList>
												
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label23" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red" style="display:block;">Actual
                                                    Value
                                                </asp:Label>
                                                <asp:TextBox ID="txtActualValue" runat="server" CssClass="form-controlWhite"
                                                    Width="145px"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                    FilterType="Numbers" TargetControlID="txtActualValue">
                                                </asp:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label39" style="display:block;" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red">Lic.
                                                    Expiration
                                                </asp:Label>
                                                <asp:TextBox ID="txtExpDate" runat="server" AutoPostBack="True"
                                                    CssClass="form-controlWhite" style="width: 145px;"></asp:TextBox>
                                                <%--<asp:ImageButton ID="imgExpDate" runat="server" ImageUrl="~/Images2/Calendar.png"
                                                    TabIndex="30" />--%>
                                                <Toolkit:MaskedEditValidator ID="MaskedEditValidator1" runat="server"
                                                    ControlExtender="MaskedEditExtender2" ControlToValidate="txtExpDate"
                                                    InvalidValueMessage="mm/dd/yyyy" TooltipMessage="mm/dd/yyyy"></Toolkit:MaskedEditValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                               <%--Cuarta Posicion--%>
                                                <asp:Label style="display:block;" ID="Label5" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red">Model</asp:Label>
                                                <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-controlWhite"
                                                    TabIndex="14" Width="145px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label13" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red" style="display:block;">Alarm
                                                    Type
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlAlarm" runat="server" CssClass="form-controlWhite"
                                                    Width="145px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <div class="grabber">
                                                    <asp:Label ID="Label2" runat="server" CssClass="LabelNormaSize"
                                                        ForeColor="Red" style="display:block;">Plate</asp:Label>
                                                    <asp:TextBox ID="txtPlateEdit" runat="server" CssClass="form-controlWhite"
                                                        Width="145px"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="contentcenter">
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterType="Numbers" TargetControlID="txtCost">
                                                </asp:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label style="display:block;" ID="LblDealer" runat="server"
                                                    CssClass="LabelNormaSize">Dealer</asp:Label>
                                                <asp:DropDownList ID="ddlCompanyDealer" runat="server" CssClass="form-controlWhite">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">

                                            <div class="contentcenter">
                                               <%-- Quinta posicion --%>
                                                <asp:Label ID="Label37" style="display:block;" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red">Registration
                                                    Number
                                                </asp:Label>
                                                <asp:TextBox ID="txtLicenseNumber" runat="server" Columns="17" CssClass="form-controlWhite"
                                                    MaxLength="17" TabIndex="8" style="width: 145px;"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label11" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red" style="display:block;">Vehicle
                                                    Use
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlVehicleClass" runat="server" CssClass="form-controlWhite"
                                                    Width="145px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label1" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red" style="display:block;">VIN</asp:Label>
                                                <asp:TextBox ID="txtVINEdit" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="17" TabIndex="8" Width="145px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <%--sexta posicion --%>
                                                <asp:Label ID="Label16" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red" style="display:block;">Year</asp:Label>
                                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-controlWhite"
                                                    OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" TabIndex="15"
                                                    Width="145px" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label12" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red" style="display:block;">Territory</asp:Label>
                                                <asp:DropDownList ID="ddlTerritory" runat="server" AutoPostBack="True"
                                                    CssClass="form-controlWhite" OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged"
                                                    Width="145px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                        <br />
                                            <div class="contentmover">
                                                <div class="content1 content">
                                                    <asp:Label ID="Label14" Style="display:inline-block" runat="server"
                                                        CssClass="LabelNormaSize" ForeColor="Red"> Depreciation</asp:Label>
                                                    <asp:RadioButton ID="rdo20percent" runat="server" GroupName="rdoDepreciation"
                                                        TabIndex="29" Text="20%" style="margin-left:10px;" />
                                                    <asp:RadioButton ID="rdo15percent" runat="server" GroupName="rdoDepreciation"
                                                        TabIndex="30" Text="15%" />
                                                </div>
                                                <div class="content2 content">
                                                    <asp:Label ID="Label41" runat="server" CssClass="LabelNormaSize"
                                                        style="display:inline-block;">Is
                                                        Leasing
                                                    </asp:Label>
                                                    <asp:CheckBox ID="chkIsLeasing" runat="server" CssClass="Labelfield2-14"
                                                        Text="Yes" style="margin-left:76px;"  OnCheckedChanged="chkIsLeasing_CheckedChanged" AutoPostBack="true" />
                                                </div>

                                                <div class="content3 content">
                                                    <asp:Label ID="Label24" runat="server" CssClass="LabelNormaSize"
                                                        style="display:inline-block;">Only
                                                        Operator
                                                    </asp:Label>
                                                    <asp:CheckBox ID="chkOnlyOperator" runat="server" CssClass="Labelfield2-14"
                                                        TabIndex="5" Text="Yes" style="margin-left:53px;" />
                                                </div>
                                                <div class="content4 content">
                                                    <asp:Label ID="Label25" runat="server" CssClass="LabelNormaSize"
                                                        style="display:inline-block;">Principal
                                                        Operator
                                                    </asp:Label>
                                                    <asp:CheckBox ID="chkPrincipalOperator" runat="server" CssClass="Labelfield2-14"
                                                        TabIndex="6" Text="Yes" style="margin-left:30px;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                              <%-- septima posicion --%>
                                              <asp:Label ID="lblAge" runat="server" ForeColor="Red" style="display:block;">Age</asp:Label>
                                                <asp:TextBox ID="txtAge" runat="server" Columns="1" CssClass="form-controlWhite"
                                                    Enabled="False" MaxLength="2" style="width:145px;" TabIndex="16" 
                                                    ontextchanged="txtAge_TextChanged"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label6" runat="server" CssClass="LabelNormaSize"
                                                    ForeColor="Red" style="display:block;">Primary
                                                    Driver
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlDriver" runat="server" CssClass="form-controlWhite"
                                                    Width="145px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-4">

                                            <div class="contentcenter">

                                            </div>

                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="grabber">
                                                <div class="contentcenter">
                                                    <asp:TextBox ID="txtDeprec1st" runat="server" CssClass="form-controlWhite"
                                                        Visible="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="second-container-separator divisionspace centeraccordion">
                                <div class="hightseparator clicksecondcolapse">
                                    <asp:Label ID="lblDeductiblesAndLimits4" runat="server" class="titles">LIMITS
                                        DEDUCTIBLES
                                    </asp:Label>
                                </div>
                                <div class="contentcolapse2" style="padding: 15px 15px;background-color:#fff">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label15" runat="server" style="display:block;" CssClass="LabelNormaSize"
                                                    ForeColor="Red">Collision</asp:Label>
                                                <asp:DropDownList ID="ddlCollision" runat="server" AutoPostBack="true"
                                                    CssClass="form-controlWhite" OnSelectedIndexChanged="ddlCollision_SelectedIndexChanged"
                                                    TabIndex="26" Width="150px" >
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label17" runat="server" style="display:block;" CssClass="LabelNormaSize"
                                                    ForeColor="Red">Bodily
                                                    Injury
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlBI" runat="server" CssClass="form-controlWhite"
                                                    TabIndex="29" Width="150px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="lblRoadAssistance" runat="server" CssClass="LabelNormaSize"
                                                    Visible="False">Road
                                                    Assst. Emp.</asp:Label>
                                                <asp:DropDownList ID="ddlRoadAssistEmp" runat="server" AutoPostBack="True"
                                                    CssClass="largeTB" OnSelectedIndexChanged="ddlRoadAssistEmp_SelectedIndexChanged"
                                                    TabIndex="39" Width="50px" Visible="False">
                                                </asp:DropDownList>

                                                <div class="grabentire">
                                                    <div>
                                                        <asp:CheckBox ID="chkLLG" runat="server" AutoPostBack="True"
                                                            CssClass="LabelNormaSize" Font-Bold="False"
                                                            OnCheckedChanged="chkLLG_CheckedChanged" TextAlign="Left" />
                                                        <asp:Label ID="Label31" runat="server" style="display:inline-block;"
                                                            CssClass="LabelNormaSize">
                                                            Lease/Loan
                                                            Gap
                                                        </asp:Label>
                                                        <asp:DropDownList ID="ddlLoanGap" runat="server" CssClass="form-controlWhite"
                                                            Visible="False" Width="80px">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="grabme5">
                                                        <asp:CheckBox ID="chkAssist" runat="server" AutoPostBack="True" Enabled="True"
                                                            CssClass="LabelNormaSize" Font-Bold="False"
                                                            OnCheckedChanged="chkAssist_CheckedChanged" />
                                                        <asp:Label ID="Label29" runat="server" style="display:inline-block;"
                                                            CssClass="LabelNormaSize">
                                                            Road &amp;
                                                            Travel Ass.</asp:Label>
                                                        <asp:DropDownList ID="ddlRoadAssist" runat="server"
                                                            AutoPostBack="True" CssClass="form-controlWhite"
                                                            OnSelectedIndexChanged="ddlRoadAssist_SelectedIndexChanged" Enabled="True"
                                                            TabIndex="39" Width="50px" style="display:inline-block;height:20px;">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="grabme2">
                                                        <asp:Label ID="Label56" runat="server" style="display:block;"
                                                            CssClass="LabelNormaSize" Visible="False">Lo
                                                            Jack
                                                        </asp:Label>
                                                        <asp:CheckBox ID="chkLoJack" runat="server" AutoPostBack="True"
                                                            CssClass="LabelNormaSize" Font-Bold="False"
                                                            OnCheckedChanged="chkLoJack_CheckedChanged" Visible="False" />
                                                        <asp:Label ID="lblDiscountCollComp" runat="server" CssClass="LabelNormaSize"
                                                            style="display:inline-block;" Visible="False">Coll/Comp
                                                            Disc.
                                                        </asp:Label>
                                                        <asp:TextBox ID="txtDiscountCollComp" runat="server" CssClass="form-controlWhite"
                                                            Visible="False" Width="50px" Style="height:20px;margin-left:17px;"></asp:TextBox>
                                                        <Toolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server"
                                                            AcceptNegative="Left" CultureName="en-US" Mask="99.99"
                                                            MaskType="Number" TargetControlID="txtDiscountCollComp">
                                                        </Toolkit:MaskedEditExtender>
                                                    </div>
                                                    <div class="gabme3">
                                                        <asp:Label ID="lblDiscountBIPD" runat="server" CssClass="LabelNormaSize"
                                                            style="display:inline-block;margin-left:19px" Visible="False">BI/PD
                                                            Discount
                                                        </asp:Label>
                                                        <asp:TextBox ID="txtDiscountBIPD" runat="server" Style="height:20px;margin-left:19px;"
                                                            CssClass="form-controlWhite" Visible="False" Width="50px"></asp:TextBox>
                                                        <Toolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                                            AcceptNegative="Left" CultureName="en-US" Mask="99.99"
                                                            MaskType="Number" TargetControlID="txtDiscountBIPD">
                                                        </Toolkit:MaskedEditExtender>
                                                    </div>
                                                </div>
                                                <asp:CheckBox ID="chkAssistEmp" runat="server" AutoPostBack="True"
                                                    CssClass="LabelNormaSize" Font-Bold="False" OnCheckedChanged="chkAssistEmp_CheckedChanged"
                                                    Visible="False" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label10" runat="server" CssClass="LabelNormaSize" style="display:block;"
                                                    ForeColor="Red">Comprehensive</asp:Label>
                                                <asp:DropDownList ID="ddlComprehensive" runat="server" CssClass="form-controlWhite"
                                                    TabIndex="27" Width="150px" 
                                                    onselectedindexchanged="ddlComprehensive_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label18" runat="server" style="display:block;" CssClass="LabelNormaSize"
                                                    ForeColor="Red">Property
                                                    Damage
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlPD" runat="server" CssClass="form-controlWhite"
                                                    TabIndex="30" Width="150px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">

                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label27" runat="server" CssClass="LabelNormaSize" style="display:block;">Transportation
                                                    Expenses
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlRental" runat="server" AutoPostBack="True"
                                                    CssClass="form-controlWhite" OnSelectedIndexChanged="ddlRental_SelectedIndexChanged"
                                                    Width="75px">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtVehicleRental" runat="server" CssClass="form-controlWhite"
                                                    ReadOnly="True" Width="70.5px"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                    FilterType="Numbers" TargetControlID="txtVehicleRental">
                                                </asp:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label19" style="display:block;" runat="server" CssClass="LabelNormaSize">Combined
                                                    Single
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlCSL" runat="server" CssClass="form-controlWhite"
                                                    TabIndex="31" Width="150px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label30" runat="server" CssClass="LabelNormaSize" style="display:block;">Towing</asp:Label>
                                                <asp:DropDownList ID="ddlTowing" runat="server" AutoPostBack="True"
                                                    CssClass="form-controlWhite" OnSelectedIndexChanged="ddlTowing_SelectedIndexChanged"
                                                    TabIndex="40" Width="75px">
                                                    <asp:ListItem Value="0">No</asp:ListItem>
                                                    <asp:ListItem Value="4">Yes</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txtTowing" runat="server" CssClass="form-controlWhite"
                                                    ReadOnly="True" Width="70.5px"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="txtTowing_FilteredTextBoxExtender"
                                                    runat="server" FilterType="Numbers" TargetControlID="txtTowing">
                                                </asp:FilteredTextBoxExtender>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label22" runat="server" style="display:block;" CssClass="LabelNormaSize">Medical
                                                    Limit
                                                </asp:Label>
                                                <asp:DropDownList ID="ddlMedical" runat="server" CssClass="form-controlWhite"
                                                    Width="150px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="third-container-separator divisionspace centeraccordion">
                                <div class="clickthirdforcollapse hightseparator">
                                    <asp:Label ID="lblDeductiblesAndLimits1" CssClass="titles" runat="server">DISCOUNTS</asp:Label>
                                </div>
                                <div class="thirdcollpasecontent" style="padding: 15px 15px;background-color:#fff">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="grabme0">
                                                <div class="contentcenter">
                                                    <asp:Label ID="lblExperienceDiscount" runat="server" style="display:block;"
                                                        CssClass="LabelNormaSize">Experience
                                                        Discount
                                                    </asp:Label>
                                                    <asp:DropDownList ID="ddlExperienceDiscount" runat="server"
                                                        CssClass="form-controlWhite" TabIndex="35" Width="75px"
                                                        AutoPostBack="True" onselectedindexchanged="ddlExperienceDiscount_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="TxtExpDisc" runat="server" CssClass="form-controlWhite"
                                                        Enabled="False" ISDATE="False" MASK="CC" MaxLength="2" ReadOnly="True"
                                                        TabIndex="43" Visible="False" Width="70.5px"></asp:TextBox>
                                                    <asp:Label ID="lblEmployeeDiscount" runat="server" CssClass="LabelNormaSize"
                                                        Visible="False">Employee
                                                        Discount
                                                    </asp:Label>
                                                    <asp:DropDownList ID="ddlEmployeeDiscount" runat="server" TabIndex="35"
                                                        Visible="False" Width="100px">
                                                        <asp:ListItem>0</asp:ListItem>
                                                        <asp:ListItem>-10</asp:ListItem>
                                                        <asp:ListItem>-15</asp:ListItem>
                                                        <asp:ListItem>-20</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblMiscDisc" runat="server" CssClass="LabelNormaSize"
                                                        Visible="False">Miscellaneous
                                                        Discount
                                                    </asp:Label>
                                                    <asp:TextBox ID="txtMiscDiscount" runat="server" IsCurrency="False"
                                                        ISDATE="False" MaxLength="4" TabIndex="34" Width="100px"></asp:TextBox>
                                                    <Toolkit:MaskedEditExtender ID="txtMiscDiscount_MaskedEditExtender"
                                                        runat="server" AcceptNegative="Left" CultureName="en-US" Mask="99.99"
                                                        MaskType="Number" TargetControlID="txtMiscDiscount">
                                                    </Toolkit:MaskedEditExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="separateor-selector-four divisionspace centeraccordion">
                                <div class=" clickcollapsefour hightseparator">
                                        <asp:Label ID="lblDeductiblesAndLimits2" CssClass="titles" runat="server">TOTAL
                                            BY VEHICLE</asp:Label>
                                </div>
                                <div class="collapsecontetfour" style="padding: 15px 15px;background-color:#fff">
                                    <div class="row">
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label26" runat="server" Style="display:block;" CssClass="Labelfield2-14">Premium</asp:Label>
                                                <asp:TextBox ID="txtPartialPremium" CssClass="form-controlWhite" runat="server"
                                                    Columns="17" Enabled="False" MaxLength="11" TabIndex="2" Width="100px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
    
                                                <asp:Label ID="lblMedical12" runat="server" CssClass="LabelNormaSize"
                                                    Visible="False">Discount</asp:Label>
                                                <asp:TextBox ID="txtPartialDiscount" runat="server" Columns="17" Enabled="False"
                                                    MaxLength="11" TabIndex="2" Visible="False" Width="100px"></asp:TextBox>
                                                <asp:Label ID="Label35" runat="server" Style="display:block;" CssClass="LabelNormaSize">Charge</asp:Label>
                                                <asp:TextBox ID="txtPartialCharge" CssClass="form-controlWhite" runat="server"
                                                    Columns="17" Enabled="False" MaxLength="11" TabIndex="2" Width="100px"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="contentcenter">
                                                <asp:Label ID="Label36" runat="server" Style="display:block;" CssClass="LabelNormaSize">Total</asp:Label>
                                                <asp:TextBox ID="txtTotalPremium" CssClass="form-controlWhite" runat="server"
                                                    Columns="17" Enabled="False" MaxLength="11" TabIndex="2" Width="100px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="caontent-five-selector divisionspace centeraccordion">
                                <div class="clickcollapsefive hightseparator">
                                        <asp:Label CssClass="titles" ID="lblDeductiblesAndLimits3" runat="server">VEHICLE
                                            LIST
                                        </asp:Label>
                                </div>
                                <div class="collapsecontentfive" style="padding: 10px 10px;background-color:#fff;">
                                    <div class="row">
                                        <div class="col-sm-12 ">
                                            <div class="contentcenter">
                                                <asp:DataGrid ID="dgVehicle" CssClass="contentcenter" runat="server"
                                                    AutoGenerateColumns="False" BackColor="White" BorderColor="#D6E3EA"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt"
                                                    OnItemCommand="dgVehicle_ItemCommand1" OnItemCreated="dgVehicle_ItemCreated1"
                                                    TabIndex="31">
                                                    <FooterStyle BackColor="Navy" ForeColor="#003399" />
                                                    <SelectedItemStyle BackColor="White" HorizontalAlign="Center" />
                                                    <EditItemStyle BackColor="White" HorizontalAlign="Center" />
                                                    <AlternatingItemStyle BackColor="#CCCCCC" CssClass="HeadForm3"
                                                        HorizontalAlign="Center" />
                                                    <ItemStyle BackColor="White" Font-Bold="False" Font-Italic="False"
                                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                        ForeColor="Black" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="Silver" CssClass="HeadForm2" Font-Bold="True"
                                                        Font-Names="verdana" Font-Size="10pt" ForeColor="Black" Height="30px"
                                                        HorizontalAlign="Center" />
                                                    <Columns>
                                                        <asp:ButtonColumn ButtonType="PushButton" CommandName="Select"
                                                            HeaderText="Sel.">
                                                        </asp:ButtonColumn>
                                                        <asp:BoundColumn DataField="VehicleMake" HeaderText="Make"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="VehicleModel" HeaderText="Model"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="VIN" HeaderText="VIN"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Plate" HeaderText="Plate"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="VehicleYear" HeaderText="Year"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="Premium" HeaderText="Premium" DataFormatString="{0:N}"></asp:BoundColumn>
                                                        <asp:ButtonColumn ButtonType="PushButton" CommandName="Remove"
                                                            HeaderText="Delete">
                                                        </asp:ButtonColumn>
                                                        <asp:BoundColumn DataField="QuotesAutoID" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="QuotesID" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="InternalID" Visible="False"></asp:BoundColumn>
                                                    </Columns>
                                                    <PagerStyle BackColor="White" CssClass="Numbers" ForeColor="Blue"
                                                        HorizontalAlign="Left" Mode="NumericPages" />
                                                </asp:DataGrid>

                                               <div align="right">
                                               <asp:Label ID="lblTotalPremium" runat="server" CssClass="LabelNormaSize"
                                                    Visible="True">Total Premium</asp:Label> 
                                                <asp:TextBox ID="txtTotalPremiumGV" runat="server" Columns="17" Enabled="False" BackColor="#ffffff" CssClass="form-controlWhite"
                                                    MaxLength="11" TabIndex="2" Visible="True" Width="100px"></asp:TextBox>

                                               </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="row">
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-4">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:Label ID="Label58" runat="server" style="display:block;" CssClass="LabelNormaSize"
                                        Visible="False">Lo
                                        Jack Exp Date</asp:Label>
                                </div>
                                <div class="col-sm-6">
                                    <div class="contentcenter">
                                        <asp:TextBox ID="TxtLojackExpDate" runat="server" CssClass="form-controlWhite"
                                            Visible="False" Width="100px"></asp:TextBox>
                                        <Toolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy"
                                            PopupButtonID="imgCalendarLJExp" TargetControlID="TxtLojackExpDate">
                                        </Toolkit:CalendarExtender>
                                        <Toolkit:MaskedEditExtender ID="MaskedEditExtender8" runat="server" CultureName="en-US"
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="TxtLojackExpDate">
                                        </Toolkit:MaskedEditExtender>
                                        <asp:ImageButton ID="imgCalendarLJExp" runat="server" ImageUrl="~/Images2/Calendar.png"
                                            TabIndex="23" Visible="False" Width="16px" />
                                        <Toolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                            ControlExtender="MaskedEditExtender8" ControlToValidate="TxtLojackExpDate"
                                            InvalidValueMessage="mm/dd/yyyy" TooltipMessage="mm/dd/yyyy"></Toolkit:MaskedEditValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <asp:TextBox ID="txtLoJackCertificate" runat="server" Columns="17" CssClass="form-controlWhite"
                                        MaxLength="17" TabIndex="8" Visible="False"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- page content 2 in cols end -->
                <div class="row">
                    <div class="col-sm-6">
                        <input id="CostTemp" runat="server" name="CostTemp" size="1" style="z-index: 105;
                                                    left: 8px; width: 18px; position: absolute; top: 478px; height: 25px"
                            type="hidden" />
                        <input id="term" runat="server" name="term" size="1" style="z-index: 104; left: 9px;
                                                    width: 18px; position: absolute; top: 514px; height: 25px"
                            type="hidden" value="0" />
                        <input id="applyToAll" runat="server" name="applyToAll" size="1" style="z-index: 103;
                                                    left: 9px; width: 18px; position: absolute; top: 553px; height: 25px"
                            type="hidden" value="0" />
                        <cc1:MaskedTextHeader ID="MaskedTextHeader1" runat="server"></cc1:MaskedTextHeader>
                        <asp:Label ID="Label21" runat="server" Style="z-index: 106; left: 8px; position: absolute;top:628px;"
                            Visible="False">Subsequent Years</asp:Label>
                        <asp:TextBox ID="txtDeprecAll" runat="server" Visible="False"></asp:TextBox>
                        <asp:Literal ID="litPopUpB" runat="server"></asp:Literal>
                        <asp:Label ID="lblPrimaryDriver" runat="server" BorderColor="SteelBlue" BorderWidth="1" Height="18"
                            Style="z-index: 112; left: 12px; position: absolute; top: 688px" Width="112px"></asp:Label>
                    </div>
                    <div class="col-sm-6">
                        <div class="contentcenter">
                            <asp:Label ID="Label32" runat="server" CssClass="LabelNormaSize" Visible="False">Seat
                                Belt
                            </asp:Label>
                            <asp:Label ID="Label33" runat="server" CssClass="LabelNormaSize" Visible="False">PAR</asp:Label>
                            <asp:DropDownList ID="ddlPAR" runat="server" CssClass="form-controlWhite" TabIndex="35"
                                Visible="False" Width="50px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSeatBelt" runat="server" CssClass="form-controlWhite" TabIndex="20"
                                Visible="False" Width="50px">
                            </asp:DropDownList>
                            <asp:TextBox ID="TxtAssistance" runat="server" Columns="10" CssClass="form-controlWhite"
                                Visible="False" Width="50px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                TargetControlID="txtAssistance">
                            </asp:FilteredTextBoxExtender>
                            <asp:Label ID="lblHomeCity" runat="server" CssClass="LabelNormaSize" Visible="False">Home</asp:Label>
                            <asp:DropDownList ID="ddlWorkCity" runat="server" CssClass="form-controlWhite"
                                OnSelectedIndexChanged="ddlWorkCity_SelectedIndexChanged" TabIndex="4" Visible="False"
                                Width="50px">
                            </asp:DropDownList>
                            <asp:Label ID="lblWorkCity" runat="server" CssClass="LabelNormaSize" Visible="False">Work</asp:Label>
                            <asp:DropDownList ID="ddlHomeCity" runat="server" CssClass="form-controlWhite"
                                OnSelectedIndexChanged="ddlHomeCity_SelectedIndexChanged" TabIndex="3" Visible="False"
                                Width="50px">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtIsAssistanceEmp" runat="server" Visible="False"></asp:TextBox>
                            <asp:Label ID="Label38" runat="server" CssClass="Labelfield2-14" Font-Bold="True" Visible="False">Vehicles:</asp:Label>
                            <asp:TextBox ID="TxtVehicleCount" runat="server" class="form-controlWhite" TabIndex="1"
                                Visible="False"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnlMessage" runat="server" BackColor="#F4F4F4" CssClass="CajaDialogo" Style="display: none;"
                    Width="400px">


                    <div align="left" class="CajaDialogoDiv" style="background-color:#17529B;" >

                        <asp:Label ID="Label55" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Lucida Sans"
                            Font-Size="14pt" ForeColor="White"  Text="Message.." />

                      
                    </div>
                    <div class="CajaDialogoDiv" style="padding: 0px; background-color: #FFFFFF; color: #C0C0C0;
                            font-family: tahoma; font-size: 14px; font-weight: normal; font-style: normal;
                            background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                    </div>
                    <div class="CajaDialogoDiv" style="color: #FFFFFF">
                        <div style="background-position: center; width: 400px; height: 175px;">
                            <asp:TextBox ID="lblRecHeader" runat="server" BorderColor="Transparent" BorderStyle="None"
                                CssClass="" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Size="10pt"
                                Font-Underline="False" ForeColor="333333" Height="170px" Text="Message" TextMode="MultiLine"
                                Width="390px"></asp:TextBox>
                                
                        </div>
                        <div align="center" class="CajaDialogoDiv">
                                  <br />
                                  <asp:Button ID="btnAceptar" runat="server" Text="OK" CssClass="btn btn-primary btn-lg btn-block" Width="150px" />
                                 
                                  <br />
                        </div>

                    </div>

                    
                </asp:Panel>
                <Toolkit:ModalPopupExtender ID="mpeSeleccion" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="" DropShadow="True" OkControlID="btnAceptar" OnCancelScript="" OnOkScript=""
                    PopupControlID="pnlMessage" TargetControlID="btnDummy">
                </Toolkit:ModalPopupExtender>
                <asp:Button ID="btnDummy" runat="server" BackColor="Transparent" BorderColor="Transparent" BorderStyle="None"
                    BorderWidth="0" Visible="true" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script type="text/javascript" src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"
        integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
    <script type="text/javascript">
        var collapsefirst = $(".clickfirstcollapse");
        var collapseSecond = $(".clicksecondcolapse");
        var collapsethird = $(".clickthirdforcollapse");
        var collapsefour = $(".clickcollapsefour");
        var collapsefive = $(".clickcollapsefive")
        collapsefirst.click(function () {
            //$(".contentcollapse").slideToggle();
            $('.contentcollapse')
                .stop(true, true)
                .animate({
                    height: "toggle",
                    opacity: "toggle",
                    padding: "toggle",
                }, 300);
        });

        collapseSecond.click(function () {
            //$(".contentcolapse2").slideToggle();
            $('.contentcolapse2')
                .stop(true, true)
                .animate({
                    height: "toggle",
                    opacity: "toggle",
                    padding: "toggle"
                }, 300);
        });

        collapsethird.click(function () {
            //$(".contentcolapse2").slideToggle();
            $('.thirdcollpasecontent')
                .stop(true, true)
                .animate({
                    height: "toggle",
                    opacity: "toggle",
                    padding: "toggle",
                }, 300);
        });

        collapsefour.click(function () {
            //$(".contentcolapse2").slideToggle();
            $('.collapsecontetfour')
                .stop(true, true)
                .animate({
                    height: "toggle",
                    opacity: "toggle",
                    padding: "toggle"
                }, 300);
        });

        collapsefive.click(function () {
            //$(".contentcolapse2").slideToggle();
            $('.collapsecontentfive')
                .stop(true, true)
                .animate({
                    height: "toggle",
                    opacity: "toggle",
                    padding: "toggle"
                }, 300);
        });
    </script>
    <script type="text/javascript">
        function getAge() {
            pdt = new Date("<%=today%>");
            pdt.setFullYear(
                document.QAV.ddlYear.options[
                    document.QAV.ddlYear.selectedIndex].text);

            today = new Date("<%=today%>");
            years = (today.getFullYear() -
                pdt.getFullYear());

            if (years < 0) {
                years = 0;
            }
            document.QAV.txtAge.value = years;

            if (years == 0) {
//                document.QAV.ddlNewUsed.selectedIndex = 2; //new
                document.QAV.txtActualValue.disabled = false;
                document.QAV.txtCost.disabled = false;
            }
            else {
//                document.QAV.ddlNewUsed.selectedIndex = 1; //used
                document.QAV.txtActualValue.disabled = false;
                document.QAV.txtCost.disabled = false;
            }
        }

        //                    function SetRentalValue() {
        //                        if (document.QAV.ddlRental.value != "") {
        //                            if (document.QAV.term.value == "12") {
        //                                document.QAV.txtVehicleRental.value = document.QAV.ddlRental.value;
        //                            }
        //                            else {
        //                                if (document.QAV.term.value != "") {
        //                                    document.QAV.txtVehicleRental.value = Number(document.QAV.ddlRental.value) * Math.round((Number(document.QAV.term.value) / 12), 1);
        //                                }
        //                                else {
        //                                    document.QAV.txtVehicleRental.value = document.QAV.ddlRental.value;
        //                                }
        //                            }
        //                        }
        //                        else {
        //                            document.QAV.txtVehicleRental.value = "";
        //                        }
        //                    }

        function SetDDLCSL() {
            if (document.QAV.ddlBI.value != 0) {
                document.QAV.ddlCSL.disabled = true;
                document.QAV.ddlCSL.value = 0;
            }
            else
                if (document.QAV.ddlBI.value == 0) {
                    document.QAV.ddlCSL.disabled = false;
                }
        }

        function SetDDLBI() {
            if (document.QAV.ddlCSL.value != 0) {
                document.QAV.ddlBI.disabled = true;
                document.QAV.ddlPD.disabled = true;
                document.QAV.ddlBI.value = 0;
                document.QAV.ddlPD.value = 0;
            }
            else
                if (document.QAV.ddlCSL.value == 0) {
                    document.QAV.ddlBI.disabled = false;
                    document.QAV.ddlPD.disabled = false;

                }
        }


        function SetCompCollValue() {
            if (document.QAV.ddlCollision.value != 0) {
                document.QAV.ddlComprehensive.value = document.QAV.ddlCollision.value;
            }
        }

        function SetActualValueFromCost() {
            if (document.QAV.ddlNewUsed.selectedIndex == 1) //new
            {
                                            document.QAV.txtActualValue.value = document.QAV.txtCost.value;
                                            document.QAV.txtAge.value = '0';

                getVehicleAge();
            }
            else {
                getVehicleAge()
            }
        }

//        function getVehicleAge() {
//            pdt = new Date("<%=today%>");
//            pdt.setFullYear(document.QAV.ddlYear.options[document.QAV.ddlYear.selectedIndex].text);

//            //today = new Date(document.QAV.txtPurchaseDt.value);
//            today = new Date("<%=today%>");
//            years = (today.getFullYear() - pdt.getFullYear());

//            if (years < 0) {
//                years = 0;
//            }
//            document.QAV.txtAge.value = years;
//            //document.QAV.TxtAge1.value = years;


//            if (years == 0) {
////                document.QAV.ddlNewUsed.selectedIndex = 2; //new
//                document.QAV.txtActualValue.value = document.QAV.txtCost.value;
//            }
//            else {
////                document.QAV.ddlNewUsed.selectedIndex = 1; //used
//                if (document.QAV.txtCost.value != "") {
//                    cost = document.QAV.txtCost.value;
//                    for (i = 1; i <= years; i++) {
//                        cost = cost * .85;
//                    }
//                    document.QAV.txtActualValue.value = Math.round(cost);
//                }
//


//                                    if (years == 0) {
////                                        document.QAV.ddlNewUsed.selectedIndex = 1; //new
//                                        document.QAV.txtActualValue.value = document.QAV.txtCost.value;
//                                        document.QAV.txtActualValue.disabled = true;
//                                        document.QAV.txtCost.disabled = false;
//                                    }
//                                    else {
////                                        document.QAV.ddlNewUsed.selectedIndex = 2; //2; //used
//                                        document.QAV.txtActualValue.disabled = false;
//                                        document.QAV.txtCost.disabled = false;
//                                    }
        }

        function SetCostFromActualValue() {
            if (document.QAV.ddlNewUsed.selectedIndex == 2) {
                document.QAV.txtCost.value = document.QAV.txtActualValue.value;
            }
        }

        function SetCostAndValueControlState() {
            if (document.QAV.ddlNewUsed.selectedIndex == 2)//new
            {
                document.QAV.txtActualValue.disabled = true;
                document.QAV.txtCost.disabled = false;
                SetCostFromActualValue();
            }
            else {
                document.QAV.txtActualValue.disabled = false;
                document.QAV.txtCost.disabled = false;
                CalculateOriginalCost();
            }
        }

        //                function SetDepreciation() {
        //                    //document.QAV.txtDeprecAll.value = document.QAV.txtDeprec1st.value;
        //                    document.QAV.txtDeprec1st.value = document.QAV.txtDeprec1st.value;
        //                }

        function SetDepreciation15() {
            document.QAV.txtDeprec1st.value = "15";
            //document.QAV.txtDeprecAll.value = "15";
        }

        function SetDepreciation20() {
            document.QAV.txtDeprec1st.value = "20";
            //document.QAV.txtDeprecAll.value = "20";
        }

        function CalculateOriginalCost() {
            if (CostNeedsCalculation()) {
                document.QAV.txtCost.value = Number(document.QAV.txtActualValue.value) + Number(GetDepreciationAmount());
                if (isNaN(document.QAV.txtCost.value)) {
                    document.QAV.txtCost.value = '0';
                }
                SetTempCost();
            }
            else {
                if (document.QAV.ddlNewUsed.selectedIndex == 0) {
                    document.QAV.txtCost.value = '0';
                }
            }
        }

        function ResetDepreciationPercentages() {
            if (document.QAV.ddlNewUsed.selectedIndex != 2 &&
                Number(document.QAV.txtCost.value) !=
                Number(document.QAV.CostTemp.value) &&
                ((document.QAV.txtDeprec1st.value
                    != '' &&
                    document.QAV.txtDeprec1st.value != '0') ||
                    (document.QAV.txtDeprecAll.value != '' &&
                        document.QAV.txtDeprecAll.value != '0'))) {
                if (confirm('Modifying the calculated original cost for a used vehicle ' +
                    'will set depreciation percentages to zero. Do you wish to proceed?')) {
                    document.QAV.txtDeprec1st.value = '';
                    document.QAV.txtDeprecAll.value = '';
                }
                else {

                    document.QAV.txtCost.value =
                        document.QAV.CostTemp.value;
                    document.QAV.txtCost.focus();
                }
            }

        }

        function SetTempCost() {
            document.QAV.CostTemp.value = document.QAV.txtCost.value;
        }

        function GetDepreciationAmount() {
            var depreciatedAmounts = new Array(document.QAV.txtAge.value - 1);
            var totalDepreciationAmount = 0;
            var firstYearDepreciation;
            var subsequentYearDepreciation;
            var remainingAmount = Number(document.QAV.txtActualValue.value);

            //Field initialization and bound checking
            if (document.QAV.txtDeprec1st.value > 100) {
                document.QAV.txtDeprec1st.value = '100';
                firstYearDepreciation = 100;
            }
            else {
                firstYearDepreciation =
                    Number(document.QAV.txtDeprec1st.value);
            }

            /*if(document.QAV.txtDeprec1st.value > 100)
            {
            document.QAV.txtDeprecAll.value = '100';
            subsequentYearDepreciation = 100;
            }
            else
            {
            //RPR 2004-05-12
            subsequentYearDepreciation = 
            Number(document.QAV.txtDeprec1st.value);
            }*/

            /*if(subsequentYearDepreciation == 0 || 
            document.QAV.txtDeprecAll.value == '')
            {
            subsequentYearDepreciation = firstYearDepreciation;
            }*/
            subsequentYearDepreciation = firstYearDepreciation;

            //Calculation loop
            for (i = (Number(document.QAV.txtAge.value) - 1);
                i >= 0; i--) {
                //DEBUG alert('i = ' + i); 

                if (i == Number(document.QAV.txtAge.value - 1))
                //Last anniversary depreciation
                {
                    //DEBUG alert('enter lastAnniversaryDepre'); 
                    depreciatedAmounts[
                        Number(document.QAV.txtAge.value) - 1] =
                        (Number(document.QAV.txtActualValue.value) /
                            ((100 - Number(subsequentYearDepreciation)) * .01)) -
                        Number(document.QAV.txtActualValue.value);
                }
                else if (i == 0)
                //First anniversary depreciation
                {
                    //DEBUG alert('enter firstAnniversaryDepre');
                    depreciatedAmounts[i] =
                        remainingAmount / ((100 -
                            Number(firstYearDepreciation)) * .01) -
                        remainingAmount;
                }
                else
                //Other anniversary depreciation
                {
                    //DEBUG alert('enter otherAnniversaryDepre');
                    depreciatedAmounts[i] =
                        remainingAmount /
                        ((100 - Number(subsequentYearDepreciation)) * .01) -
                        remainingAmount;
                }

                remainingAmount += depreciatedAmounts[i];
                totalDepreciationAmount += Number(depreciatedAmounts[i]);

                /*DEBUG
                alert('totalDepreciationAmount =' + totalDepreciationAmount);							
                alert('depreciatedAmounts[' + i + '] = ' + depreciatedAmounts[i]);
                alert('remainingAmount = ' + remainingAmount);*/
            }
            return Number(Math.round(totalDepreciationAmount));
        }

        function CostNeedsCalculation() {
            if (document.QAV.ddlNewUsed.selectedIndex == 1 &&
                document.QAV.txtActualValue.value != '' &&
                document.QAV.txtActualValue.value != '0' &&
                ((document.QAV.txtDeprec1st.value != '' &&
                    document.QAV.txtDeprec1st.value != '0') ||
                    (document.QAV.txtDeprec1st.value != '' &&
                        document.QAV.txtDeprec1st.value != '0')) &&
                document.QAV.txtAge.value > 0 &&
                document.QAV.txtAge.value != '')// &&
            //document.QAV.txtCost.value ==''
            {
                return true;
            }
            return false;
        }

        function InitializeFields() {
            //if (document.QAV.ddlNewUsed.selectedIndex == 2) {
            //document.QAV.txtActualValue.disabled = true;
            //document.QAV.txtActualValue.value = document.QAV.txtCost.value;						
        }

    </script>
</body>

</html>