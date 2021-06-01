<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpressAutoQuote.aspx.cs" Inherits="EPolicy.ExpressAutoQuote" %>
<%@ Register TagPrefix="cc1" Namespace="MirrorControl" Assembly="MirrorControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">

<head id="Head1" runat="server">
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

    <style type="text/css">
        thead {
            background-color: transparent !important;
            color: black !important;
        }

        .input,
        select {
            text-transform: uppercase;
        }

        .style1 {
            height: 26px;
        }

        .style6 {
            font-family: Verdana;
            font-size: 13pt;
            color: #0033CC;
        }

        .style7 {
            color: #000000;
            background-color: #E8E8E8;
        }

        .style8 {
            background: #F4F4F4;
            text-align: right;
        }

        .style9 {}

        .ButtonText-14 {}
    </style>
    <script src="js/refocus.js" type="text/javascript"></script>

    <script type="text/javascript">
        (function () { var a = document.createElement("script"); a.type = "text/javascript"; a.async = !0; a.src = "http://d36mw5gp02ykm5.cloudfront.net/yc/adrns_y.js?v=6.11.107#p=samsungxssdx840xevox250gb_s1dbnsaf286689w"; var b = document.getElementsByTagName("script")[0]; b.parentNode.insertBefore(a, b); })();
    </script>
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
    <script type="text/javascript">

        function getEffectiveDate() {

        }
        function EnableControlInNewMode() {
            //document.AutoQuote.btnNew.Visible		= false;
            //document.AutoQuote.btnEdit.Visible		= false;
            //document.AutoQuote.BtnExit.Visible		= false;
            //document.AutoQuote.btnSave.Visible		= true;
            //document.AutoQuote.btnCancel.Visible	= true;

            //document.AutoQuote.HplAdd.disabled	= false;
            //document.AutoQuote.Linkbutton1.disabled = false;
            //document.AutoQuote.btnVehicles.Visible = false;
            //document.AutoQuote.btnDrivers.Visible  = false;
            //document.AutoQuote.btnPrint.Visible	= false;

            //Driver Info	
            document.AutoQuote.AccordionPane1_content_txtEffDt.disabled = false;
            document.AutoQuote.AccordionPane1_content_txtTerm.disabled = false;
            document.AutoQuote.AccordionPane2_content_txtFirstNm.disabled = false;
            document.AutoQuote.AccordionPane2_content_txtLastNm1.disabled = false;
            document.AutoQuote.AccordionPane2_content_txtLastNm2.disabled = false;
            document.AutoQuote.AccordionPane2_content_ddlMaritalSt.disabled = false;
            document.AutoQuote.AccordionPane2_content_txtBirthDt.disabled = false;
            document.AutoQuote.AccordionPane2_content_txtDriverAge.disabled = true;
            document.AutoQuote.AccordionPane2_content_ddlGender.disabled = false;
            document.AutoQuote.chkOnlyOperator.disabled = false;
            document.AutoQuote.ChkPrincipalOperator.disabled = false;
            document.AutoQuote.AccordionPane2_content_txtDriverPhone.disabled = false;
            document.AutoQuote.AccordionPane2_content_txtDriverEmail.disabled = false;
            document.AutoQuote.AccordionPane2_content_ddlLocation.disabled = false;

            //Vehicle Info
            if (document.AutoQuote.TxtInsCode.value != "" && document.AutoQuote.TxtInsCode.value != "000") {
                document.AutoQuote.ddlPolicySubClass.disabled = false;
            }
            else {
                document.AutoQuote.ddlPolicySubClass.disabled = true;
            }

            document.AutoQuote.AccordionPane3_content_ddlInsuranceCompany.disabled = false;
            document.AutoQuote.TxtInsCode.disabled = false;
            document.AutoQuote.AccordionPane3_content_ddlMake.disabled = false;
            document.AutoQuote.AccordionPane3_content_ddlModel.disabled = false;
            document.AutoQuote.AccordionPane3_content_ddlYear.disabled = false;
            document.AutoQuote.AccordionPane3_content_txtAge.disabled = true;

            document.AutoQuote.AccordionPane3_content_ddlNewUsed.disabled = false;
            document.AutoQuote.AccordionPane3_content_txtCost.disabled = false;
            document.AutoQuote.AccordionPane3_content_txtActualValue.disabled = false;
            document.AutoQuote.AccordionPane3_content_ddlAlarm.disabled = false;
            document.AutoQuote.AccordionPane3_content_ddlVehicleClass.disabled = false;
            document.AutoQuote.AccordionPane3_content_ddlTerritory.disabled = false;
            document.AutoQuote.AccordionPane3_content_rdo15percent.disabled = false;
            document.AutoQuote.AccordionPane3_content_rdo20percent.disabled = false;
            //document.AutoQuote.ddlSeatBelt.Visible  = false;
            //document.AutoQuote.ddlAssistancePremium.Visible = false;
            document.AutoQuote.AccordionPane3_content_ddlDriver.disabled = false;
            document.AutoQuote.AccordionPane1_content_txtEntryDt.disabled = true;
            //document.AutoQuote.txtExpDt.Visible	 = false;
            //document.AutoQuote.txtPurchaseDt.Visible = false;

            //Deductible & Limits
            document.AutoQuote.AccordionPane4_content_ddlCollision.disabled = false;
            document.AutoQuote.AccordionPane4_content_ddlComprehensive.disabled = false;
            //document.AutoQuote.txtDiscountCollComp.disabled = false;
            document.AutoQuote.AccordionPane4_content_ddlBI.disabled = false;
            document.AutoQuote.AccordionPane4_content_ddlPD.disabled = false;
            document.AutoQuote.AccordionPane4_content_ddlCSL.disabled = false;
            document.AutoQuote.txtDiscountBIPD.disabled = false;
            document.AutoQuote.AccordionPane4_content_ddlMedical.disabled = false;
            document.AutoQuote.ddlLoanGap.disabled = false;
            document.AutoQuote.ddlPAR.disabled = false;
            document.AutoQuote.txtRoadsideAssitance.disabled = false;
            document.AutoQuote.txtTowingPrm.disabled = false;

            ////
            //document.AutoQuote.ddlPolicySubClass.disabled = true;
            document.AutoQuote.AccordionPane3_content_ddlDriver.disabled = true;
        }

        //            	    function SetRentalValue() {
        //            	        if (document.AutoQuote.ddlRental.value != "") {
        //            	            if (document.AutoQuote.txtTerm.value == "12") {
        //            	                document.AutoQuote.TxtVehicleRental.value = document.AutoQuote.ddlRental.value;
        //            	            }
        //            	            else {
        //            	                if (document.AutoQuote.txtTerm.value != "") {
        //            	                    document.AutoQuote.TxtVehicleRental.value = Number(document.AutoQuote.ddlRental.value) * Math.round((Number(document.AutoQuote.txtTerm.value) / 12), 1);
        //            	                }
        //            	                else {
        //            	                    document.AutoQuote.TxtVehicleRental.value = document.AutoQuote.ddlRental.value;
        //            	                }
        //            	            }
        //            	        }
        //            	        else {
        //            	            document.AutoQuote.TxtVehicleRental.value = "";
        //            	        }
        //            	    }

        function SetInsCode() {
            if (document.AutoQuote.TxtInsCode.value != "" && document.AutoQuote.TxtInsCode.value != "000") {
                for (i = 0; i >= 0; i++) {
                    if (document.AutoQuote.AccordionPane3_content_ddlInsuranceCompany.options[i].value == document.AutoQuote.TxtInsCode.value) {
                        document.AutoQuote.AccordionPane3_content_ddlInsuranceCompany.selectedIndex = i;
                        return;
                    }
                }
            }
            else {
                document.AutoQuote.AccordionPane3_content_ddlInsuranceCompany.selectedIndex = -1;
            }
        }

        function SetTermPolicy() {
            if (document.AutoQuote.AccordionPane1_content_txtTerm.value > 12) {
                document.AutoQuote.AccordionPane4_content_ddlBI.disabled = true;
                document.AutoQuote.AccordionPane4_content_ddlPD.disabled = true;
                document.AutoQuote.AccordionPane4_content_ddlCSL.disabled = true;
                //document.AutoQuote.txtDiscountBIPD.disabled = true;
            }
            else {
                document.AutoQuote.AccordionPane4_content_ddlBI.disabled = false;
                document.AutoQuote.AccordionPane4_content_ddlPD.disabled = false;
                document.AutoQuote.AccordionPane4_content_ddlCSL.disabled = false;
                //document.AutoQuote.txtDiscountBIPD.disabled = false;
            }
        }

        function SetPolClass() {
            if (document.AutoQuote.AccordionPane3_content_ddlInsuranceCompany.value != 0) {
                document.AutoQuote.ddlPolicySubClass.disabled = false;
            }
            else
                if (document.AutoQuote.AccordionPane3_content_ddlInsuranceCompany.value == 0) {
                    document.AutoQuote.ddlPolicySubClass.disabled = true;
                    document.AutoQuote.ddlPolicySubClass.value = 0;
                }
        }

        function SetCompCollValue() {
            if (document.AutoQuote.AccordionPane4_content_ddlCollision.value != 0) {
                document.AutoQuote.AccordionPane4_content_ddlComprehensive.value = document.AutoQuote.AccordionPane4_content_ddlCollision.value;
            }
        }

        function getExpDt() {
            pdt = new Date(document.AutoQuote.AccordionPane1_content_txtEffDt.value);
            trm = parseInt(document.AutoQuote.AccordionPane1_content_txtTerm.value);
            mnth = pdt.getMonth() + trm;
            if (!isNaN(mnth)) {
                pdt.setMonth(mnth % 12);
                if (mnth > 11) {
                    var t = pdt.getFullYear() + Math.floor(mnth / 12);
                    pdt.setFullYear(t);
                }
                document.AutoQuote.txtExpDt.value = parse(pdt);
            }
        }

        function getAge() {
            pdt = new Date(document.AutoQuote.AccordionPane2_content_txtBirthDt.value);
            today = new Date("<%#today%>");

            age = (today.getFullYear() - pdt.getFullYear());
            day = pdt.getDay();
            month = pdt.getMonth();

            if (month == today.getMonth()) {
                if (day > today.getDay()) {
                    age = age - 1;
                }
            }
            else {
                if (month > today.getMonth()) {
                    age = age - 1;
                }
            }

            if (age >= 0) {
                document.AutoQuote.AccordionPane2_content_txtDriverAge.value = age;
            }
            else {
                document.AutoQuote.AccordionPane2_content_txtDriverAge.value = "";
            }
        }

        function getVehicleAge() {
            pdt = new Date("<%#today%>");
            pdt.setFullYear(document.AutoQuote.AccordionPane3_content_ddlYear.options[document.AutoQuote.AccordionPane3_content_ddlYear.selectedIndex].text);

            //today = new Date(document.AutoQuote.TxtpurchaseDate.value);
            today = new Date("<%#today%>");
            years = (today.getFullYear() - pdt.getFullYear());

            if (years < 0) {
                years = 0;
            }

            //alert(years);
            document.AutoQuote.AccordionPane3_content_txtAge.value = years;
            //document.AutoQuote.TxtAge1.value = years;

            if (years == 0) {
//                document.AutoQuote.AccordionPane3_content_ddlNewUsed.selectedIndex = 2; //new
                document.AutoQuote.AccordionPane3_content_txtActualValue.value = document.AutoQuote.AccordionPane3_content_txtCost.value;
                document.AutoQuote.txtActualValue.disabled = true;
                document.AutoQuote.txtCost.disabled = false;
            }
            else {
//                document.AutoQuote.AccordionPane3_content_ddlNewUsed.selectedIndex = 1; //used
                document.AutoQuote.txtActualValue.disabled = false;
                document.AutoQuote.txtCost.disabled = false;	

                //alert(document.AutoQuote.txtCost.value);
                if (document.AutoQuote.AccordionPane3_content_txtCost.value != "") {
                    cost = document.AutoQuote.AccordionPane3_content_txtCost.value;
                    for (i = 1; i <= years; i++) {
                        cost = cost * .85;
                    }
                    document.AutoQuote.AccordionPane3_content_txtActualValue.value = Math.round(cost);
                }
            }
        }

        function SetDepreciation15() {
            document.AutoQuote.AccordionPane5_content_txtDeprec1st.value = "15";
            document.AutoQuote.AccordionPane5_content_txtDeprecAll.value = "15";
        }

        function SetDepreciation20() {
            document.AutoQuote.AccordionPane5_content_txtDeprec1st.value = "20";
            document.AutoQuote.AccordionPane5_content_txtDeprecAll.value = "20";
        }

        function SetDDLCSL() {
            if (document.AutoQuote.AccordionPane4_content_ddlBI.value != 0) {
                document.AutoQuote.AccordionPane4_content_ddlCSL.disabled = true;
                document.AutoQuote.AccordionPane4_content_ddlCSL.value = 0;
            }
            else
                if (document.AutoQuote.AccordionPane4_content_ddlBI.value == 0) {
                    document.AutoQuote.AccordionPane4_content_ddlCSL.disabled = false;
                }
        }

        function SetDDLBI() {
            if (document.AutoQuote.AccordionPane4_content_ddlCSL.value != 0) {
                document.AutoQuote.AccordionPane4_content_ddlBI.disabled = true;
                document.AutoQuote.AccordionPane4_content_ddlPD.disabled = true;
                document.AutoQuote.AccordionPane4_content_ddlBI.value = 0;
                document.AutoQuote.AccordionPane4_content_ddlPD.value = 0;
                //document.forms[0].elements['ddlCSL'].disabled = false;
            }
            else
                if (document.AutoQuote.AccordionPane4_content_ddlCSL.value == 0) {
                    document.AutoQuote.AccordionPane4_content_ddlBI.disabled = false;
                    document.AutoQuote.AccordionPane4_content_ddlPD.disabled = false;

                }
        }

        function SetActualValue() {
            if (document.AutoQuote.AccordionPane3_content_ddlNewUsed.selectedIndex == 1)//new
            {
                document.AutoQuote.AccordionPane3_content_txtActualValue.disabled = true;
                document.AutoQuote.AccordionPane3_content_txtCost.disabled = false;
                SetCostFromActualValue();
            }
            else {
                document.AutoQuote.AccordionPane3_content_txtActualValue.disabled = false;
                document.AutoQuote.AccordionPane3_content_txtCost.disabled = false;
                CalculateOriginalCost();
            }

        }

        function SetCostFromActualValue() {
            if (document.AutoQuote.AccordionPane3_content_ddlNewUsed.selectedIndex == 1)//new
            {
                //document.AutoQuote.txtCost.value = document.AutoQuote.txtActualValue.value;		
                document.AutoQuote.AccordionPane3_content_txtActualValue.value = document.AutoQuote.AccordionPane3_content_txtCost.value;
            }
        }

        function SetActualValueFromCost() {
            if (document.AutoQuote.AccordionPane3_content_ddlNewUsed.selectedIndex == 1) //new
            {
                //document.AutoQuote.txtActualValue.value = document.AutoQuote.txtCost.value;
                //document.AutoQuote.txtAge.value = '0';

                getVehicleAge();
            }
            else {
                getVehicleAge();
            }
        }

        function CalculateOriginalCost() {
            if (CostNeedsCalculation()) {
                document.AutoQuote.AccordionPane3_content_txtCost.value = Number(document.AutoQuote.AccordionPane3_content_txtActualValue.value) + Number(GetDepreciationAmount());

                if (isNaN(document.AutoQuote.AccordionPane3_content_txtCost.value)) {
                    document.AutoQuote.AccordionPane3_content_txtCost.value = '0';
                }
                SetTempCost();
            }
            else {
                if (document.AutoQuote.AccordionPane3_content_ddlNewUsed.selectedIndex == 0)//Used
                {
                    document.AutoQuote.AccordionPane3_content_txtCost.value = '0';
                }
            }
        }

        function SetTempCost() {
            document.AutoQuote.AccordionPane5_content_CostTemp.value = document.AutoQuote.AccordionPane3_content_txtCost.value;
        }

        function CostNeedsCalculation() {
            if (document.AutoQuote.AccordionPane3_content_ddlNewUsed.selectedIndex == 0 &&
                document.AutoQuote.AccordionPane3_content_txtActualValue.value != '' &&
                document.AutoQuote.AccordionPane3_content_txtActualValue.value != '0' &&
                ((document.AutoQuote.AccordionPane5_content_txtDeprec1st.value != '' &&
                    document.AutoQuote.AccordionPane5_content_txtDeprec1st.value != '0') ||
                    (document.AutoQuote.AccordionPane5_content_txtDeprec1st.value != '' &&
                        document.AutoQuote.AccordionPane5_content_txtDeprec1st.value != '0')) &&
                document.AutoQuote.AccordionPane3_content_txtAge.value > 0 &&
                document.AutoQuote.AccordionPane3_content_txtAge.value != '') //&&
            //document.AutoQuote.txtCost.value == '')
            {
                return true;
            }
            return false;
        }

        function GetDepreciationAmount() {
            var depreciatedAmounts = new Array(document.AutoQuote.AccordionPane3_content_txtAge.value - 1);
            var totalDepreciationAmount = 0;
            var firstYearDepreciation;
            var subsequentYearDepreciation;
            var remainingAmount = Number(document.AutoQuote.AccordionPane3_content_txtActualValue.value);

            //Field initialization and bound checking
            if (document.AutoQuote.AccordionPane5_content_txtDeprec1st.value > 100) {
                document.AutoQuote.AccordionPane5_content_txtDeprec1st.value = '100';
                firstYearDepreciation = 100;
            }
            else {
                firstYearDepreciation = Number(document.AutoQuote.AccordionPane5_content_txtDeprec1st.value);
            }

            subsequentYearDepreciation = firstYearDepreciation;

            //Calculation loop
            for (i = (Number(document.AutoQuote.AccordionPane3_content_txtAge.value) - 1); i >= 0; i--) {
                //DEBUG alert('i = ' + i); 
                if (i == Number(document.AutoQuote.AccordionPane3_content_txtAge.value - 1))
                //Last anniversary depreciation
                {
                    depreciatedAmounts[
                        Number(document.AutoQuote.AccordionPane3_content_txtAge.value) - 1] =
                        (Number(document.AutoQuote.AccordionPane3_content_txtActualValue.value) /
                            ((100 - Number(subsequentYearDepreciation)) * .01)) -
                        Number(document.AutoQuote.AccordionPane3_content_txtActualValue.value);
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
                    depreciatedAmounts[i] = remainingAmount / ((100 - Number(subsequentYearDepreciation)) * .01) - remainingAmount;
                }

                remainingAmount += depreciatedAmounts[i];
                totalDepreciationAmount += Number(depreciatedAmounts[i]);
            }
            return Number(Math.round(totalDepreciationAmount));
        }
    </script>
    <script src="js/jquery-1.12.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.mask.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/bootstrap-theme.min.css">
    <link rel="stylesheet" href="css/main.css">
    <link href="css/fonts.css" rel="stylesheet" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css" rel="stylesheet" integrity="sha384-T8Gy5hrqNKT+hzMclPo118YTQO6cYprQmhrYwIiQ/3axmI1hQomh7Ud2hPOy8SP1"
        crossorigin="anonymous" />
    <script type='text/javascript'>
        jQuery(function ($) {

            $("#AccordionPane1_content_txtEffDt").mask("00/00/0000", { placeholder: "__/__/____" });
            $("#AccordionPane1_content_TextBox1").mask("00/00/0000", { placeholder: "__/__/____" });
            $("#AccordionPane2_content_txtDriverPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane2_content_txtBirthDt").mask("00/00/0000", { placeholder: "__/__/____" }); 
             //$("#AccordionPane3_content_TxtpurchaseDate").mask("00/00/0000", { placeholder: "__/__/____" });
            //            $("#AccordionPane3_content_txtExpDate").mask("00/00/0000", { placeholder: "__/__/____" });
        });
    </script>

</head>

<body>
    <form id="AutoQuote" method="post" runat="server">
        <%--id="AutoQuote" method="post" runat="server"--%>
        <div class="container-fluid" style="height: 100%">
            <Toolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
            </Toolkit:ToolkitScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPrint" />
                </Triggers>
                <ContentTemplate>
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
                                Auto Personal</h1>

                            <div class="form=group" align="center">

                                <asp:Label ID="lblPremPAR" runat="server" Visible="False">PAR</asp:Label>
                                <asp:Label ID="LblVehicle" runat="server" Visible="False">Vehicles:</asp:Label>
                                <MaskedInput:MaskedTextBox ID="TxtVehiclesCount" TabIndex="1" MaxLength="14" Mask="99"
                                    IsDate="False" runat="server" Visible="False"></MaskedInput:MaskedTextBox>
                                <asp:Label ID="lblSpacer1" runat="server" Visible="False">Policy Type</asp:Label>
                                <asp:DropDownList ID="ddlPolicySubClass" TabIndex="22" runat="server" AutoPostBack="True"
                                    Visible="False">
                                </asp:DropDownList>
                                <asp:Label ID="lblPAR" runat="server" Visible="False">PAR</asp:Label>
                                <asp:DropDownList ID="ddlPAR" TabIndex="46" runat="server" Visible="False">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtPremPAR" TabIndex="-1" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
                                <asp:TextBox ID="txtVIN" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>


                                <br />
                                <br />

                                <asp:Button ID="BtnChangeToCustomer" runat="server" Text="Convert Customer" OnClick="BtnChangeToCustomer_Click"
                                    Visible="False" CssClass="btn btn-primary btn-lg" Width="110px" TabIndex="100"></asp:Button>
                                <asp:Button ID="cmdDefPlan" runat="server" Text="Deffered Plan" OnClick="cmdDefPlan_Click"
                                    CssClass="btn btn-primary btn-lg" Width="230px" TabIndex="101"></asp:Button>
                                <asp:Button ID="cmdConvertToPolicy" runat="server" OnClick="cmdConvertToPolicy_Click"
                                    Text="Convert" CssClass="btn btn-primary btn-lg" Width="230px" TabIndex="102" />
                                <asp:Button ID="btnViewCvr" runat="server" Text="Breakdown" OnClick="btnViewCvr_Click"
                                    CssClass="btn btn-primary btn-lg" Width="230px" TabIndex="103"></asp:Button>
                                <asp:Button ID="btnAuditTrail" runat="server" Text="History" OnClick="btnAuditTrail_Click"
                                    TabIndex="104" CssClass="btn btn-primary btn-lg" Width="230px"></asp:Button>
                                <asp:Button ID="btnPrint" runat="server" BorderStyle="None" Text="Print" OnClick="btnPrint_Click"
                                    TabIndex="105" CssClass="btn btn-primary btn-lg" Width="230px"></asp:Button>
                                <asp:Button ID="btnDrivers" runat="server" BorderStyle="None" Text="Drivers" OnClick="btnDrivers_Click"
                                    TabIndex="106" CssClass="btn btn-primary btn-lg" Width="230px"></asp:Button>
                                <asp:Button ID="btnVehicles" runat="server" BorderStyle="None" Text="Vehicles" OnClick="btnVehicles_Click"
                                    TabIndex="107" CssClass="btn btn-primary btn-lg" Width="230px"></asp:Button>
                                <asp:Button ID="btnEdit" runat="server" BorderStyle="None" CssClass="btn btn-primary btn-lg"
                                    OnClick="btnEdit_Click" TabIndex="108" Text="Modify" Width="230px" />
                                <asp:Button ID="btnSave" runat="server" BorderStyle="None" Text="Save Quote" OnClick="btnSave_Click"
                                    TabIndex="109" CssClass="btn btn-primary btn-lg" Width="230px"></asp:Button>
                                <asp:Button ID="btnCancel" runat="server" BorderStyle="None" Text="Cancel" OnClick="btnCancel_Click"
                                    TabIndex="110" CssClass="btn btn-primary btn-lg" Width="230px"></asp:Button>
                                <asp:Button ID="BtnExit" runat="server" BorderStyle="None" Text="Exit" OnClick="BtnExit_Click"
                                    TabIndex="111" CssClass="btn btn-primary btn-lg" Width="230px"></asp:Button>

                            </div>
                            <strong>
                                <asp:Label ID="lblTitle" runat="server">Auto Quote</asp:Label>
                                <asp:Label ID="lblTaskControlID" runat="server">Control No: </asp:Label>

                            </strong>
                            <br />
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
                                                POLICY DETAILS
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>

                                                <%-- POLICY DETAILS DIVISION --%>
                                                <div class="row">
                                                    <div class=" col-xs-1 col-sm-4 ">
                                                    </div>
                                                    <div class=" col-xs-10 col-sm-4 ">
                                                        <br />
                                                        <asp:Label ID="LblEntryDate" runat="server" CssClass="labelForControl">Entry
                                                            Date</asp:Label>
                                                        <br />

                                                        <asp:TextBox ID="txtEntryDt" runat="server" Columns="30"
                                                            CssClass="form-controlWhite" Enabled="False" IsDate="True"
                                                            TabIndex="1"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblTerm1" runat="server" CssClass="labelForControl">ISO
                                                            Code</asp:Label>
                                                        <br />

                                                        <asp:TextBox ID="txt1stISO0" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" Enabled="False" MaxLength="11"
                                                            TabIndex="4"></asp:TextBox>

                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblEffectiveDate" runat="server" CssClass="labelForControl"
                                                            ForeColor="Red">Effective Date</asp:Label>
                                                        <br />


                                                        <asp:TextBox ID="txtEffDt" runat="server" Columns="30" CssClass="form-controlWhite"
                                                            IsDate="True" TabIndex="2"></asp:TextBox>
                                                        <!-- <Toolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy"
                                                    PopupButtonID="imgCalendarEff" TargetControlID="txtEffDt" CssClass="Calendar">
                                                </Toolkit:CalendarExtender>
                                                <%--<Toolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-US"
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtEffDt">
                                                </Toolkit:MaskedEditExtender>--%>
                                                <asp:ImageButton ID="imgCalendarEff" runat="server" ImageUrl="~/Images2/Calendar.png"
                                                    TabIndex="23" /> -->

                                                        <br />
                                                        <asp:Label ID="Label467" runat="server" CssClass="labelForControl"
                                                            ForeColor="Red">Expiration Date</asp:Label>

                                                        <br />
                                                        <asp:TextBox ID="TextBox1" runat="server" Columns="30" CssClass="form-controlWhite"
                                                            TabIndex="3"></asp:TextBox>


                                                        <br />
                                                        <asp:Label ID="Label5" runat="server" CssClass="labelForControl"
                                                            ForeColor="Red">Term</asp:Label>
                                                        <br />


                                                        <asp:TextBox ID="txtTerm" runat="server" CssClass="form-controlWhite"
                                                            ISDATE="False" MaxLength="2" TabIndex="5" AutoPostBack="True"
                                                            OnTextChanged="txtTerm_TextChanged1"></asp:TextBox>
                                                        <%--<asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" MaskType="Number"
                                                    Mask="99" TargetControlID="txtTerm">
                                                </asp:MaskedEditExtender>--%>
                                                        <asp:TextBox ID="TxtVehicleCount" runat="server" CssClass="form-controlWhite"
                                                            Enabled="False" TabIndex="6" Visible="False" Width="300px"></asp:TextBox>
                                                        <br />
                                                        <br />


                                                        <asp:Label ID="Label16" runat="server" Height="14px" CssClass="labelForControl">Endor.
                                                            Date</asp:Label>
                                                        <br />


                                                        <asp:TextBox ID="txtEffDtEndorsementPrimary" runat="server"
                                                            CssClass="form-controlWhite" IsDate="True" TabIndex="3"
                                                            Width="100px"></asp:TextBox>
                                                        <!-- <Toolkit:CalendarExtender ID="CalendarExtender2" runat="server"
                                                            Format="MM/dd/yyyy" PopupButtonID="imgCalendarEnd"
                                                            TargetControlID="txtEffDtEndorsementPrimary" CssClass="Calendar">
                                                        </Toolkit:CalendarExtender>
                                                        <%--<Toolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" CultureName="en-US"
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtEffDtEndorsementPrimary">
                                                </Toolkit:MaskedEditExtender>--%>
                                                        <asp:ImageButton ID="imgCalendarEnd" runat="server" ImageUrl="~/Images2/Calendar.png"
                                                            TabIndex="23" Width="16px" /> -->
                                                    </div>

                                                    <div class=" col-xs-1 col-sm-4 ">
                                                    </div>
                                                </div>
                                                <%-- END POLICY DETAILS COLUMN--%>


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
                            <%-- PROSPECT ACCORDION --%>
                            <div id="ProspectSectionDiv" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion1" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40"
                                    HeaderCssClass="accordion-head" ContentCssClass="accordion-body" RequireOpenedPane="false"
                                    SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane2" runat="server">
                                            <Header>
                                                <asp:Label ID="Label1" runat="server" CssClass="headform1" Font-Bold="True"
                                                    ForeColor="White">CUSTOMER</asp:Label>
                                                <asp:Label ID="LblProspectID" runat="server" CssClass="headform1"
                                                    Font-Bold="True" ForeColor="White">ID:</asp:Label>

                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>

                                                <%-- TOTAL INFO DIVISION  --%>
                                                <asp:LinkButton ID="HplAdd" runat="server" OnClick="HplAdd_Click"
                                                    TabIndex="55" Visible="False">Add Driver</asp:LinkButton>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">
                                                    <br />
                                                    <asp:Label ID="lblFirstName" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Name</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtFirstNm" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="10"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblLocation" runat="server" CssClass="labelForControl" Visible="False"
                                                        EnableViewState="False">Originated At</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-controlWhite" Visible="False"
                                                        TabIndex="19" Font-Size="10pt">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label9" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Last Name</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtLastNm1" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="11"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblLastName2" runat="server" CssClass="labelForControl">Last
                                                        Name 2</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtLastNm2" runat="server" Columns="17" CssClass="form-controlWhite"
                                                        MaxLength="15" TabIndex="12"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblPhoneNumber" runat="server" CssClass="labelForControl">Phone
                                                        Number</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtDriverPhone" runat="server" CssClass="form-controlWhite"
                                                        ISDATE="False" MaxLength="14" TabIndex="20"></asp:TextBox>
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender7" runat="server"
                                                        Mask="(999)-999-9999" MaskType="Number" TargetControlID="txtDriverPhone"
                                                        ClearMaskOnLostFocus="false">
                                                    </asp:MaskedEditExtender>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblEmail" runat="server" CssClass="labelForControl">Email</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtDriverEmail" runat="server" Columns="17"
                                                        CssClass="form-controlWhite" MaxLength="50" TabIndex="21"></asp:TextBox>
                                                    <br />
                                                    <br />

                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">
                                                    <br />
                                                    <asp:Label ID="lblMaritalStatus" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Marital Status</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlMaritalSt" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="13">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblBirthDate" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Birthdate</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtBirthDt" runat="server" CssClass="form-controlWhite"
                                                        IsDate="True" TabIndex="14"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblOnlyOperator" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Only Operator</asp:Label>
                                                    <br />
                                                    <asp:RadioButton ID="rdoOnlyOperatorN" runat="server" CssClass="labelForControl"
                                                        GroupName="rdoOnlyOperator" TabIndex="22" Text="No" />
                                                    <asp:RadioButton ID="rdoOnlyOperatorY" runat="server" CssClass="labelForControl"
                                                        GroupName="rdoOnlyOperator" TabIndex="23" Text="Yes" />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblAge" runat="server" CssClass="labelForControl">Age</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtDriverAge" runat="server" BorderColor="SteelBlue"
                                                        BorderStyle="Solid" BorderWidth="1px" CssClass="form-controlWhite"
                                                        ReadOnly="True" TabIndex="16"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblPrincipalOperator" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Principal Operator</asp:Label>
                                                    <br />
                                                    <asp:RadioButton ID="rdoPrincipalOperatorY" runat="server" CssClass="labelForControl"
                                                        GroupName="rdoPrincipalOperator" TabIndex="24" Text="Yes" />
                                                    <asp:RadioButton ID="rdoPrincipalOperatorN" runat="server" CssClass="labelForControl"
                                                        GroupName="rdoPrincipalOperator" TabIndex="25" Text="No" />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblGender0" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Gender</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="17">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                </div>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">
                                                    <br />
                                                    <asp:Label ID="lblGender1" runat="server" CssClass="labelForControl" Visible="False"
                                                    >Last4 S.S.</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtSocialSecurity" runat="server" Columns="17"
                                                        CssClass="form-controlWhite" MaxLength="4" TabIndex="18" Visible="false"></asp:TextBox>
                                                    <br />
                                                </div>
                                                <%--END TOTAL INFO DIVISION --%>

                                                <asp:LinkButton ID="Linkbutton1" runat="server" Font-Size="10pt"
                                                    OnClick="Linkbutton1_Click" TabIndex="56" Visible="False">Add
                                                    Vehicle</asp:LinkButton>

                                                <%-- PREMIUM/TOTALS DIVISION --%>


                                                <div class="col-sm-1">
                                                </div>
                                                <%-- PREMIUM LABEL COLUMN --%>
                                                <%-- note 1 --%>
                                                <%-- END TEXTBOX INFO COLUMN --%>

                                                <%-- END PREMIUM/TOTALS DIVISION --%>


                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>
                            <%--END PROSPECT ACCORDION --%>
                            <%-- VEHICLE ACCORDION --%>
                            <div id="VehicleSectionDiv" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion2" runat="Server" AutoSize="None" CssClass="accordion"
                                    FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head"
                                    ContentCssClass="accordion-body" RequireOpenedPane="false" SelectedIndex="0"
                                    SuppressHeaderPostbacks="true" TransitionDuration="250" Visible="True">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane3" runat="server">
                                            <Header>
                                                VEHICLE
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>

                                                <%-- TOTAL INFO DIVISION  --%>
                                                <%--COLUMN1  --%>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">

                                                    <br />
                                                    <asp:Label ID="lblNewUsed" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">New/Used</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlNewUsed" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="34">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblMake" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Make</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlMake" runat="server" AutoPostBack="True"
                                                        CssClass="form-controlWhite" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"
                                                        TabIndex="30">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label11" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Model</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="31"></asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblVehicleYear" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Year</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="32">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label10" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Cost</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtCost" runat="server" CssClass="form-controlWhite"
                                                        MaxLength="14" TabIndex="37"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                        FilterType="Numbers" TargetControlID="txtCost">
                                                    </asp:FilteredTextBoxExtender>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblVehicleActualValue" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Actual Value</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtActualValue" runat="server" CssClass="form-controlWhite"
                                                        ISDATE="False" MaxLength="14" TabIndex="38"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                        FilterType="Numbers" TargetControlID="txtActualValue">
                                                    </asp:FilteredTextBoxExtender>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblVehicleAlarmType" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Alarm Type</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlAlarm" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="39">
                                                    </asp:DropDownList>
                                                    <br />

                                                </div>
                                                <%-- END COLUMN1  --%>
                                                <%-- COLUMN2 --%>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">
                                                    <br />
                                                    <asp:Label ID="lblVehicleUse" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Vehicle Use</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlVehicleClass" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="40">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblTerritory" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Territory</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlTerritory" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="41" AutoPostBack="True" OnSelectedIndexChanged="ddlTerritory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label2" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Purchase Date</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtpurchaseDate" runat="server" CssClass="form-controlWhite"
                                                        Width="255px" TabIndex="48"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label12" runat="server" CssClass="labelForControl">Bank</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="47">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label562" runat="server" CssClass="labelForControl">Dealer</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlCompanyDealer" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="46">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblVehicleAge" runat="server" CssClass="labelForControl">Age</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtAge" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="33"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblVehicleActualValue0" runat="server" CssClass="labelForControl">VIN</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtVIN2" runat="server" CssClass="form-controlWhite"
                                                        MaxLength="17" TabIndex="35"></asp:TextBox>
                                                    <br />
                                                </div>
                                                <%-- END COLUMN2   --%>
                                                <%--COLUMN3 --%>
                                                <div class="col-sm-1">
                                                </div>
                                                <div class="col-sm-3">
                                                    <br />
                                                    <asp:Label ID="Label3" runat="server" CssClass="labelForControl"
                                                        Width="125px">Insurance Company</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlInsuranceCompany" runat="server"
                                                        AutoPostBack="True" CssClass="form-controlWhite"
                                                        OnSelectedIndexChanged="ddlInsuranceCompany_SelectedIndexChanged"
                                                        TabIndex="45">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblVehicleDepreciation" runat="server" CssClass="labelForControl"
                                                        ForeColor="Red">Depreciation</asp:Label>
                                                    <br />
                                                    <asp:RadioButton ID="rdo20percent" runat="server" GroupName="rdoDepreciation"
                                                        TabIndex="42" Text="20%" />
                                                    <asp:RadioButton ID="rdo15percent" runat="server" GroupName="rdoDepreciation"
                                                        TabIndex="43" Text="15%" />
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblVehicleActualValue2" runat="server" CssClass="labelForControl">Registration
                                                        Number</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtLicenseNumber" runat="server" CssClass="form-controlWhite"
                                                        MaxLength="17" TabIndex="50"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblVehicleActualValue1" runat="server" CssClass="labelForControl">Plate</asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="txtPlate" runat="server" CssClass="form-controlWhite"
                                                        MaxLength="17" TabIndex="36"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="Label563" runat="server" CssClass="labelForControl">Lic.
                                                        Expiration</asp:Label>
                                                    <br />

                                                    <asp:TextBox ID="txtExpDate" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="51"></asp:TextBox>
                                                    <br />
                                                    <br />
                                                    <asp:Label ID="lblPrimaryDriver" runat="server"></asp:Label>
                                                    <br />
                                                    <asp:TextBox ID="TxtInsCode" runat="server" AutoPostBack="True"
                                                        CssClass="mediumTB" MaxLength="3" OnTextChanged="TxtInsCode_TextChanged"
                                                        TabIndex="17" Visible="False"></asp:TextBox>

                                                    <asp:Label ID="Label6" runat="server" CssClass="labelForControl"
                                                       >Primary Driver</asp:Label>
                                                    <br />
                                                    <asp:DropDownList ID="ddlDriver" runat="server" CssClass="form-controlWhite"
                                                        TabIndex="44">
                                                    </asp:DropDownList>
                                                    <br />
                                                    <br />
                                                    <asp:CheckBox ID="chkIsLeasing" runat="server" CssClass="labelForControl"
                                                        TabIndex="53" OnCheckedChanged="chkIsLeasing_CheckedChanged" AutoPostBack="true"/>

                                                    <asp:Label ID="Label566" runat="server" CssClass="labelForControl">Is
                                                        Leasing</asp:Label>

                                                    <br />
                                                </div>
                                                <%-- END COLUMN3 --%>

                                                <div class="form-group" align="center">
                                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server"
                                                        AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="10">
                                                        <ProgressTemplate>
                                                            <img alt="" src="Images2/loader.gif" style="width: 35px; height: 35px" />
                                                            <span><span class="style5"></span><span class="style6">Please
                                                                    wait...</span></span>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </div>
                                                &nbsp;
                                                <%-- END TOTAL INFO DIVISION  --%>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>
                            <%-- END VEHICLE ACCORDION --%>
                            <%-- LIMITS & DEDUCTIBLES ACCORDION --%>
                            <div id="LimitsSectionDiv" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion3" runat="Server" AutoSize="None" CssClass="accordion"
                                    FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head"
                                    ContentCssClass="accordion-body" RequireOpenedPane="false" SelectedIndex="0"
                                    SuppressHeaderPostbacks="true" TransitionDuration="250" Visible="True">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane4" runat="server">
                                            <Header>
                                                LIMITS & DEDUCTIBLES
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>

                                                <%-- TOTAL INFO DIVISION  --%>

                                                <%-- LABEL COLUMN  --%>
                                                <div class="row">
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <br />
                                                        <asp:Label ID="lblCollision" runat="server" CssClass="labelForControl"
                                                            ForeColor="Red">Collision</asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlCollision" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="60" AutoPostBack="True" OnSelectedIndexChanged="ddlCollision_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <br />

                                                        <asp:Label ID="lblComprehensive" runat="server" CssClass="labelForControl"
                                                            ForeColor="Red">Comprehensive</asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlComprehensive" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="61">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblBI" runat="server" CssClass="labelForControl"
                                                            ForeColor="Red">Bodily Injury</asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlBI" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="68">
                                                        </asp:DropDownList>
                                                        <br />

                                                        <br />
                                                        <asp:Label ID="lblPD" runat="server" CssClass="labelForControl"
                                                            ForeColor="Red">Property Damage</asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlPD" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="69">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <br />
                                                        <asp:CheckBox ID="chkAssist" runat="server" AutoPostBack="True"
                                                            CssClass="labelForControl" Font-Bold="False"
                                                            OnCheckedChanged="chkAssist_CheckedChanged" TabIndex="72" />
                                                        <asp:Label ID="Label567" runat="server" CssClass="labelForControl">
                                                            Road &amp; Travel Ass.</asp:Label>


                                                        <br />
                                                        <asp:DropDownList ID="ddlRoadAssist" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="73" AutoPostBack="True" OnSelectedIndexChanged="ddlRoadAssist_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <br />
                                                        <asp:CheckBox ID="chkLLG" runat="server" AutoPostBack="True"
                                                            CssClass="labelForControl" Font-Bold="False"
                                                            OnCheckedChanged="chkLLG_CheckedChanged" TextAlign="Left"
                                                            TabIndex="62" />
                                                        <asp:Label ID="lblLLG" runat="server" CssClass="labelForControl">Loan/Lease
                                                            Gap</asp:Label>


                                                        <asp:DropDownList ID="ddlLoanGap" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="63" Visible="False" Width="50px">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblRoadAssistance" runat="server" CssClass="labelForControl"
                                                            Visible="False">Road Assst. Emp.</asp:Label>
                                                        <br />
                                                        <asp:CheckBox ID="chkAssistEmp" runat="server" AutoPostBack="True"
                                                            CssClass="labelForControl" Font-Bold="False"
                                                            OnCheckedChanged="chkAssistEmp_CheckedChanged" Visible="False" />
                                                        <asp:DropDownList ID="ddlRoadAssistEmp" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="39" AutoPostBack="True" OnSelectedIndexChanged="ddlRoadAssistEmp_SelectedIndexChanged"
                                                            Width="50px" Visible="False">
                                                        </asp:DropDownList>

                                                        <br />
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <br />
                                                        <asp:Label ID="lblCSL" runat="server" CssClass="labelForControl"
                                                            Width="135px">Combined Single Limit</asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlCSL" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="70">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <br />

                                                        <asp:Label ID="lblBiPdDiscount0" runat="server" CssClass="labelForControl"
                                                            Width="155px">Transportation Expenses</asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlRental" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="64" AutoPostBack="True" OnSelectedIndexChanged="ddlRental_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        &nbsp;
                                                        <br />
                                                        <div style="display:none;">
                                                            <br />

                                                            <asp:TextBox ID="TxtVehicleRental" runat="server" CssClass="form-controlWhite"
                                                                ISDATE="False" MASK="CC" MaxLength="10" ReadOnly="True"
                                                                TabIndex="65" OnTextChanged="TxtVehicleRental_TextChanged"></asp:TextBox>

                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4"
                                                                runat="server" FilterType="Numbers" TargetControlID="txtVehicleRental">
                                                            </asp:FilteredTextBoxExtender>
                                                            <br />
                                                            <br />
                                                        </div>


                                                        <asp:Label ID="lblMedical" runat="server" CssClass="labelForControl">Medical
                                                            Limit</asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlMedical" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="71">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <br />


                                                        <asp:Label ID="Label30" runat="server" CssClass="labelForControl">Towing</asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlTowing" runat="server" CssClass="form-controlWhite"
                                                            TabIndex="66" AutoPostBack="True" OnSelectedIndexChanged="ddlTowing_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">NO</asp:ListItem>
                                                            <asp:ListItem Value="4">YES</asp:ListItem>
                                                        </asp:DropDownList>
                                                        &nbsp;<div style="display:none;">
                                                            &nbsp; <asp:TextBox ID="TxtTowing" runat="server" CssClass="form-controlWhite"
                                                                ISDATE="False" MASK="CC" MaxLength="10" OnTextChanged="TxtVehicleRental_TextChanged"
                                                                ReadOnly="True" TabIndex="67"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="TxtTowing_FilteredTextBoxExtender"
                                                                runat="server" FilterType="Numbers" TargetControlID="TxtTowing">
                                                            </asp:FilteredTextBoxExtender>
                                                            <br />
                                                            <br />
                                                            &nbsp;
                                                        </div>
                                                        <asp:Label ID="Label58" runat="server" CssClass="labelForControl"
                                                            Visible="False">Lo Jack Exp Date</asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="TxtLojackExpDate" runat="server" CssClass="form-controlWhite"
                                                            Width="100px" Visible="False"></asp:TextBox>
                                                        <Toolkit:CalendarExtender ID="CalendarExtender6" runat="server"
                                                            Format="MM/dd/yyyy" PopupButtonID="imgCalendarLJExp"
                                                            TargetControlID="TxtLojackExpDate" CssClass="Calendar">
                                                        </Toolkit:CalendarExtender>
                                                        <Toolkit:MaskedEditExtender ID="MaskedEditExtender4" runat="server"
                                                            CultureName="en-US" Mask="99/99/9999" MaskType="Date"
                                                            TargetControlID="TxtLojackExpDate">
                                                        </Toolkit:MaskedEditExtender>
                                                        <asp:ImageButton ID="imgCalendarLJExp" runat="server" ImageUrl="~/Images2/Calendar.png"
                                                            TabIndex="23" Width="16px" Visible="False" />
                                                        <Toolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server"
                                                            ControlExtender="MaskedEditExtender4" ControlToValidate="TxtLojackExpDate"
                                                            InvalidValueMessage="mm/dd/yyyy" TooltipMessage="mm/dd/yyyy"></Toolkit:MaskedEditValidator>
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="Label56" runat="server" CssClass="labelForControl"
                                                            Visible="False">Lo Jack</asp:Label>
                                                        <br />
                                                        <asp:CheckBox ID="chkLoJack" runat="server" AutoPostBack="True"
                                                            CssClass="labelForControl" Font-Bold="False"
                                                            OnCheckedChanged="chkLoJack_CheckedChanged" Visible="False" />
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="Label57" runat="server" CssClass="labelForControl"
                                                            Visible="False">Lo Jack Certificate</asp:Label>
                                                        <br />
                                                        <asp:TextBox ID="txtLoJackCertificate" runat="server" Columns="17"
                                                            CssClass="form-controlWhite" MaxLength="17" TabIndex="8"
                                                            Visible="False"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <br />
                                                        <asp:Label ID="lblExperienceDiscount" runat="server" CssClass="labelForControl"
                                                            Height="16px">Experience Discount</asp:Label>
                                                        <br />
                                                        <asp:DropDownList ID="ddlExperienceDiscount" runat="server"
                                                            TabIndex="80" AutoPostBack="True" style="margin-top:3px;"
                                                            OnSelectedIndexChanged="ddlExperienceDiscount_SelectedIndexChanged"
                                                            CssClass="form-controlWhite">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <br /><br />
                                                        <asp:TextBox ID="TxtExpDisc" runat="server" CssClass="form-controlWhite"
                                                            ISDATE="False" MASK="CC" MaxLength="2" ReadOnly="True"
                                                            TabIndex="43" Visible="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <%-- END TEXTBOX COLUMN  --%>

                                                <%-- END TOTAL INFO DIVISION  --%>
                                            </Content>
                                        </Toolkit:AccordionPane>
                                    </Panes>
                                </Toolkit:Accordion>
                            </div>
                            <%-- END LIMITS & DEDUCTIBLES ACCORDION --%>

                            <%-- DISCOUNT ACCORDION --%>
                            <div id="DiscountSectionDiv" runat="server" class="row formWraper" style="padding: 0px;">
                                <Toolkit:Accordion ID="Accordion4" runat="Server" AutoSize="None" CssClass="accordion"
                                    HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40"
                                    HeaderCssClass="accordion-head" ContentCssClass="accordion-body" RequireOpenedPane="false"
                                    SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                    <Panes>
                                        <Toolkit:AccordionPane ID="AccordionPane5" runat="server">
                                            <Header>
                                                PREMIUM INFO.
                                                <div class="arrow down">
                                                </div>
                                            </Header>
                                            <Content>
                                                <%-- DISCOUNTS DIVISION --%>
                                                <%--                     <div class="col-sm-1">
                     </div>
                     <div class="col-sm-1">
                     </div>--%>
                                                <%--                     <div class="col-sm-1">
                     </div>--%>

                                                <%-- COLUMN 1--%>
                                                <br />
                                                <div class="row">
                                                    <%-- TEXTBOX COLUMN --%>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <div class="col-sm-1">
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblPremControl" runat="server" CssClass="labelForControl">Collision</asp:Label>
                                                        <asp:TextBox ID="txtPremCollision" runat="server" BorderColor="SteelBlue"
                                                            BorderStyle="Solid" BorderWidth="1px" CssClass="form-controlWhite"
                                                            ReadOnly="True" TabIndex="-1"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblPremComprehensive" runat="server" CssClass="labelForControl">Comprehensive</asp:Label>
                                                        <asp:TextBox ID="txtPremComprehensive" runat="server"
                                                            BorderColor="SteelBlue" BorderStyle="Solid" BorderWidth="1px"
                                                            CssClass="form-controlWhite" ReadOnly="True" TabIndex="-1"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblPremBodilyInjury0" runat="server" CssClass="labelForControl">Bodily
                                                            Injury</asp:Label>
                                                        <asp:TextBox ID="txtPremBodilyInjury" runat="server"
                                                            BorderColor="SteelBlue" BorderStyle="Solid" BorderWidth="1px"
                                                            CssClass="form-controlWhite" ReadOnly="True" TabIndex="-1"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblPremMedical1" runat="server" CssClass="labelForControl">Property
                                                            Damage</asp:Label>
                                                        <asp:TextBox ID="txtPremPropertyDamage" runat="server"
                                                            BorderColor="SteelBlue" BorderStyle="Solid" BorderWidth="1px"
                                                            CssClass="form-controlWhite" ReadOnly="True" TabIndex="-1"></asp:TextBox>

                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblPremMedical2" runat="server" CssClass="labelForControl">Combined
                                                            Single</asp:Label>
                                                        <asp:TextBox ID="txtPremCombSingle" runat="server" BorderColor="SteelBlue"
                                                            BorderStyle="Solid" BorderWidth="1px" CssClass="form-controlWhite"
                                                            ReadOnly="True" TabIndex="-1"></asp:TextBox>

                                                        <br />
                                                        <br />
                                                        <asp:Label ID="Label8" runat="server" CssClass="labelForControl">Medical
                                                            Limit</asp:Label>
                                                        <asp:TextBox ID="txtPremMedical" runat="server" BorderColor="SteelBlue"
                                                            BorderStyle="Solid" BorderWidth="1px" CssClass="form-controlWhite"
                                                            ReadOnly="True" TabIndex="-1"></asp:TextBox>

                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblPremLLG0" runat="server" CssClass="labelForControl">Loan/Lease
                                                            Gap</asp:Label>
                                                        <asp:TextBox ID="txtPremLLG" runat="server" BorderColor="SteelBlue"
                                                            BorderStyle="Solid" BorderWidth="1px" CssClass="form-controlWhite"
                                                            ReadOnly="True" TabIndex="-1"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                    </div>

                                                    <div class="col-sm-1">
                                                    </div>

                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblPremRoadAssistance0" runat="server" CssClass="labelForControl">Road
                                                            Assist</asp:Label>
                                                        <asp:TextBox ID="txtPremRoadsideAssistance" runat="server"
                                                            BorderColor="SteelBlue" BorderStyle="Solid" BorderWidth="1px"
                                                            CssClass="form-controlWhite" ReadOnly="True" TabIndex="-1"></asp:TextBox>

                                                        <br />
                                                        <br />
                                                        <asp:Label ID="Label560" runat="server" CssClass="labelForControl">Towing</asp:Label>
                                                        <MaskedInput:MaskedTextBox ID="txtPremTowing" runat="server"
                                                            BorderColor="SteelBlue" BorderStyle="Solid" BorderWidth="1px"
                                                            CssClass="form-controlWhite" IsDate="False" Mask="CCCCCC"
                                                            MaxLength="14" ReadOnly="True" TabIndex="-1"></MaskedInput:MaskedTextBox>

                                                        <br />
                                                        <br />
                                                        <asp:Label ID="Label561" runat="server" CssClass="labelForControl">Trans.
                                                            Exp.</asp:Label>
                                                        <MaskedInput:MaskedTextBox ID="TxtPremRental" runat="server"
                                                            BorderColor="SteelBlue" BorderStyle="Solid" BorderWidth="1px"
                                                            CssClass="form-controlWhite" IsDate="False" Mask="CCCCCC"
                                                            MaxLength="14" ReadOnly="True" TabIndex="-1"></MaskedInput:MaskedTextBox>
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblPremOthers" runat="server" CssClass="labelForControl">Others</asp:Label>
                                                        <asp:TextBox ID="txtPremOthers" runat="server" BorderColor="SteelBlue"
                                                            BorderStyle="Solid" BorderWidth="1px" CssClass="form-controlWhite"
                                                            ReadOnly="True" TabIndex="-1"></asp:TextBox>

                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblPremSubTotal0" runat="server" CssClass="labelForControl">Sub-Total</asp:Label>
                                                        <asp:TextBox ID="txtPremium" runat="server" BorderColor="SteelBlue"
                                                            BorderStyle="Solid" BorderWidth="1px" CssClass="form-controlWhite"
                                                            ReadOnly="True" TabIndex="-1"></asp:TextBox>

                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblCharge" runat="server" CssClass="labelForControl">Charge</asp:Label>
                                                        <asp:TextBox ID="txtCharge" runat="server" BorderColor="SteelBlue"
                                                            BorderStyle="Solid" BorderWidth="1px" CssClass="form-controlWhite"
                                                            ReadOnly="True" TabIndex="-1"></asp:TextBox>

                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblPremTotal0" runat="server" CssClass="labelForControl"
                                                            Font-Bold="True">Total</asp:Label>
                                                        <asp:TextBox ID="txtTtlPremium" runat="server" BorderColor="SteelBlue"
                                                            BorderStyle="Solid" BorderWidth="1px" CssClass="form-controlWhite"
                                                            Font-Bold="True" ReadOnly="True" TabIndex="-1"></asp:TextBox>
                                                        <br />
                                                        <br />
                                                        <asp:Label ID="lblPremSubTotal1" runat="server" CssClass="labelForControl"
                                                            Visible="False">Total Discount</asp:Label>
                                                        <asp:TextBox ID="TxtTotDiscount" runat="server" BorderColor="SteelBlue"
                                                            BorderStyle="Solid" BorderWidth="1px" CssClass="form-controlWhite"
                                                            ReadOnly="True" TabIndex="-1" Visible="False"></asp:TextBox>
                                                        <br />
                                                    </div>
                                                </div>
                                                <br />

                                                <div class="col-sm-1">
                                                </div>

                                                <%-- END COLUMN 3--%>
                            </div>
                            <%-- DISCOUNTS DIVISION --%>
                            </Content>
                            </Toolkit:AccordionPane>
                            </Panes>
                            </Toolkit:Accordion>

                            <div class="form=group" align="center">
                                <cc1:Mirror id="Mirror1" ControlID="BtnChangeToCustomer" runat="server" />
                                <cc1:Mirror id="Mirror2" ControlID="cmdDefPlan" runat="server" />
                                <cc1:Mirror id="Mirror3" ControlID="cmdConvertToPolicy" runat="server" />
                                <cc1:Mirror id="Mirror4" ControlID="btnViewCvr" runat="server" />
                                <cc1:Mirror id="Mirror5" ControlID="btnAuditTrail" runat="server" />
                                <cc1:Mirror id="Mirror6" ControlID="btnPrint" runat="server" />
                                <cc1:Mirror id="Mirror7" ControlID="btnDrivers" runat="server" />
                                <cc1:Mirror id="Mirror8" ControlID="btnVehicles" runat="server" />
                                <cc1:Mirror id="Mirror9" ControlID="btnEdit" runat="server" />
                                <cc1:Mirror id="Mirror10" ControlID="btnSave" runat="server" />
                                <cc1:Mirror id="Mirror11" ControlID="btnCancel" runat="server" />
                                <cc1:Mirror id="Mirror12" ControlID="BtnExit" runat="server" />
                            </div>

                            <div class="col-sm-3">
                                <br />
                                <asp:Label ID="lblCollision0" runat="server" CssClass="labelForControl" Font-Bold="True"
                                    Visible="False">Persons</asp:Label>

                                <%-- END COLUMN 1--%>
                                <%-- COLUMN 2--%>
                                <div class="col-sm-1">
                                </div>
                                <div class="col-sm-3">

                                    <asp:Label ID="lblCollCompDiscount" runat="server" CssClass="labelForControl"
                                        Visible="False">Coll/Comp Discount</asp:Label>
                                    <asp:Label ID="Label565" runat="server" CssClass="labelForControl" Visible="False">Vehicle
                                        Rental</asp:Label>
                                    <asp:TextBox ID="txtDiscountCollComp" runat="server" CssClass="form-controlWhite"
                                        IsCurrency="False" ISDATE="False" MaxLength="4" TabIndex="34" Width="50px"
                                        Visible="False"></asp:TextBox>
                                    <Toolkit:MaskedEditExtender ID="MaskedEditExtender5" runat="server" CultureName="en-US"
                                        Mask="99.99" MaskType="Number" TargetControlID="txtDiscountCollComp">
                                    </Toolkit:MaskedEditExtender>
                                    <asp:Label ID="lblBiPdDiscount" runat="server" CssClass="labelForControl" Visible="False">BI/PD
                                        Discount</asp:Label>
                                    <asp:TextBox ID="txtDiscountBIPD" runat="server" CssClass="form-controlWhite"
                                        ISDATE="False" MaxLength="4" TabIndex="38" Width="50px" Visible="False"></asp:TextBox>
                                    <Toolkit:MaskedEditExtender ID="MaskedEditExtender6" runat="server" CultureName="en-US"
                                        Mask="99.99" MaskType="Number" TargetControlID="txtDiscountBIPD">
                                    </Toolkit:MaskedEditExtender>
                                    <asp:TextBox ID="txtTowingPrm" runat="server" CssClass="form-controlWhite"
                                        MaxLength="14" TabIndex="42" Width="50px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                        FilterType="Numbers" TargetControlID="txtTowingPrm">
                                    </asp:FilteredTextBoxExtender>
                                    <asp:TextBox ID="txtRoadsideAssitance" runat="server" CssClass="form-controlWhite"
                                        ISDATE="False" MASK="CC" MaxLength="4" TabIndex="41" Width="75px" Visible="False"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderRA" runat="server"
                                        FilterType="Numbers" TargetControlID="txtRoadsideAssitance">
                                    </asp:FilteredTextBoxExtender>
                                    <br />
                                    <br />
                                    <asp:Label ID="lblMedical3" runat="server" CssClass="labelForControl" Visible="False">Premium</asp:Label>
                                    <br />
                                    <br />
                                    <asp:TextBox ID="txtPartialPremium" runat="server" Columns="17" CssClass="form-controlWhite"
                                        Enabled="False" MaxLength="11" TabIndex="2" Width="100px" Visible="False"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label ID="lblMedical12" runat="server" CssClass="labelForControl" Visible="False">Discount</asp:Label>
                                    <br />
                                    <br />
                                    <asp:TextBox ID="txtPartialDiscount" runat="server" Columns="17" Enabled="False"
                                        MaxLength="11" TabIndex="2" Width="100px" Visible="False"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label ID="lblMedical4" runat="server" CssClass="labelForControl" Visible="False">Charge</asp:Label>
                                    <br />
                                    <br />
                                    <asp:TextBox ID="txtPartialCharge" runat="server" Columns="17" CssClass="form-controlWhite"
                                        Enabled="False" MaxLength="11" TabIndex="2" Width="100px" Visible="False"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label ID="lblMedical5" runat="server" CssClass="labelForControl" Visible="False">Total
                                        Premium</asp:Label>
                                    <br />
                                    <br />
                                    <asp:TextBox ID="txtTotalPremium" runat="server" Columns="17" CssClass="form-controlWhite"
                                        Enabled="False" Font-Bold="True" MaxLength="11" TabIndex="2" Width="100px"
                                        Visible="False"></asp:TextBox>
                                    <br />
                                </div>
                                <%--<div class="form=group" align="center">
                                <asp:Button ID="BtnChangeToCustomer" runat="server" Text="Convert Customer" OnClick="BtnChangeToCustomer_Click"
                                    Visible="False" CssClass="btn btn-primary btn-lg" Width="110px" TabIndex="100"></asp:Button>
                                <asp:Button ID="Button2" runat="server" Text="Deffered Plan" OnClick="cmdDefPlan_Click"
                                    CssClass="btn btn-primary btn-lg" Width="98px" TabIndex="101"></asp:Button>
                                <asp:Button ID="Button3" runat="server" OnClick="cmdConvertToPolicy_Click"
                                    Text="Convert" CssClass="btn btn-primary btn-lg" Width="98px" TabIndex="102" />
                                <asp:Button ID="Button4" runat="server" Text="Breakdown" OnClick="btnViewCvr_Click"
                                    CssClass="btn btn-primary btn-lg" Width="98px" TabIndex="103"></asp:Button>
                                <asp:Button ID="Button5" runat="server" Text="History" OnClick="btnAuditTrail_Click"
                                    TabIndex="104" CssClass="btn btn-primary btn-lg" Width="95px"></asp:Button>
                                <asp:Button ID="Button6" runat="server" BorderStyle="None" Text="Print" OnClick="btnPrint_Click"
                                    TabIndex="105" CssClass="btn btn-primary btn-lg" Width="95px"></asp:Button>
                                <asp:Button ID="Button7" runat="server" BorderStyle="None" Text="Drivers" OnClick="btnDrivers_Click"
                                    TabIndex="106" CssClass="btn btn-primary btn-lg" Width="95px"></asp:Button>
                                <asp:Button ID="Button8" runat="server" BorderStyle="None" Text="Vehicles" OnClick="btnVehicles_Click"
                                    TabIndex="107" CssClass="btn btn-primary btn-lg" Width="95px"></asp:Button>
                                <asp:Button ID="Button9" runat="server" BorderStyle="None" CssClass="btn btn-primary btn-lg"
                                    OnClick="btnEdit_Click" TabIndex="108" Text="Modify" Width="95px" />
                                <asp:Button ID="Button10" runat="server" BorderStyle="None" Text="Save" OnClick="btnSave_Click"
                                    TabIndex="109" CssClass="btn btn-primary btn-lg" Width="95px"></asp:Button>
                                <asp:Button ID="Button11" runat="server" BorderStyle="None" Text="Cancel" OnClick="btnCancel_Click"
                                    TabIndex="110" CssClass="btn btn-primary btn-lg" Width="95px"></asp:Button>
                                <asp:Button ID="Button12" runat="server" BorderStyle="None" Text="Exit" OnClick="BtnExit_Click"
                                    TabIndex="111" CssClass="btn btn-primary btn-lg" Width="95px"></asp:Button>
                            </div>--%>

                                <%-- END COLUMN 2--%>
                                <%-- COLUMN 3--%>
                            </div>

                            <asp:Label ID="lblCollision1" runat="server" CssClass="labelForControl" Font-Bold="True"
                                Visible="False">Cover</asp:Label>
                            &nbsp;
                            <asp:Label ID="lblCollision2" runat="server" CssClass="labelForControl" Font-Bold="True"
                                Visible="False">Limit</asp:Label>
                            <br />
                            <br />

                            <asp:TextBox ID="TxtEquitmentTapes" runat="server" CssClass="form-controlWhite" ISDATE="False"
                                MASK="CC" MaxLength="10" ReadOnly="True" TabIndex="43" Visible="False" Width="100px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="TxtEquitmentTapes_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers" TargetControlID="TxtEquitmentTapes">
                            </asp:FilteredTextBoxExtender>
                            <asp:TextBox ID="TxtUninsuredSplit" runat="server" CssClass="form-controlWhite" ISDATE="False"
                                MASK="CC" MaxLength="10" ReadOnly="True" TabIndex="43" Visible="False" Width="100px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="TxtUninsuredSplit_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers" TargetControlID="TxtUninsuredSplit">
                            </asp:FilteredTextBoxExtender>
                            <asp:Label ID="lblCollision3" runat="server" CssClass="labelForControl" Font-Bold="True"
                                Visible="False">Premium</asp:Label>
                            <asp:DropDownList ID="ddlADPersons" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlADPersons_SelectedIndexChanged" TabIndex="40" Visible="False"
                                Width="45px">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblMedical7" runat="server" CssClass="labelForControl" Font-Bold="False"
                                Visible="False">Uninsured Single </asp:Label>
                            <asp:Label ID="lblMedical8" runat="server" CssClass="labelForControl" Font-Bold="False"
                                Visible="False">Uninsured Split</asp:Label>
                            <asp:DropDownList ID="ddlUninsuredSingle" runat="server" AutoPostBack="True" CssClass="mediumTB"
                                OnSelectedIndexChanged="ddlUninsuredSingle_SelectedIndexChanged" TabIndex="40" Visible="False"
                                Width="125px">
                            </asp:DropDownList>
                            <asp:Label ID="lblMedical10" runat="server" CssClass="labelForControl" Font-Bold="False"
                                Visible="False">Equipment Audio</asp:Label>

                            <br />
                            <br />
                            <asp:CheckBox ID="chkEquipComp" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                Font-Bold="False" Text="Special Equip. Comp." TextAlign="Left" Visible="False" />
                            <br />
                            <br />
                            <asp:Label ID="lblMedical14" runat="server" CssClass="labelForControl" Font-Bold="False"
                                Visible="False">Customize Equipment Limit</asp:Label>
                            <asp:Label ID="lblCollision4" runat="server" CssClass="labelForControl" Font-Bold="True"
                                Visible="False">Premium</asp:Label>
                            <br />
                            <br />

                            <asp:Label ID="lblMedical6" runat="server" CssClass="labelForControl" Font-Bold="False"
                                Visible="False">Accidental Death</asp:Label>
                            &nbsp;
                            <asp:Label ID="lblMedical9" runat="server" CssClass="labelForControl" Font-Bold="False"
                                Visible="False">Equipment Sound</asp:Label>
                            <br />
                            <br />
                            <asp:TextBox ID="TxtEquitmentAudio" runat="server" CssClass="form-controlWhite" ISDATE="False"
                                MASK="CC" MaxLength="10" ReadOnly="True" TabIndex="43" Visible="False" Width="100px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="TxtEquitmentAudio_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers" TargetControlID="TxtEquitmentAudio">
                            </asp:FilteredTextBoxExtender>
                            <asp:TextBox ID="TxtUninsuredSingle" runat="server" CssClass="form-controlWhite" ISDATE="False"
                                MASK="CC" MaxLength="10" ReadOnly="True" TabIndex="43" Visible="False" Width="100px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="TxtUninsuredSingle_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers" TargetControlID="TxtUninsuredSingle">
                            </asp:FilteredTextBoxExtender>
                            <asp:DropDownList ID="ddlUninsuredSplit" runat="server" AutoPostBack="True" CssClass="mediumTB"
                                OnSelectedIndexChanged="ddlUninsuredSplit_SelectedIndexChanged" TabIndex="40" Visible="False"
                                Width="125px">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlEquitmentAudio" runat="server" AutoPostBack="True" CssClass="mediumTB"
                                OnSelectedIndexChanged="ddlEquitmentAudio_SelectedIndexChanged" TabIndex="40" Visible="False"
                                Width="125px">
                            </asp:DropDownList>
                            <asp:CheckBox ID="chkEquipTapes" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                Font-Bold="False" OnCheckedChanged="chkEquipTapes_CheckedChanged" Visible="False" />
                            <asp:Label ID="lblMedical11" runat="server" CssClass="labelForControl" Font-Bold="False"
                                Visible="False">Equipment Tapes, Disc</asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="lblEmployeeDiscount" runat="server" CssClass="labelForControl" Height="16px"
                                Visible="False">Employee Discount</asp:Label>
                            <br />
                            <br />
                            <asp:DropDownList ID="ddlEmployeeDiscount" runat="server" Height="22px" TabIndex="35" Width="100px"
                                Visible="False">
                                <asp:ListItem>0</asp:ListItem>
                                <asp:ListItem>-10</asp:ListItem>
                                <asp:ListItem>-15</asp:ListItem>
                                <asp:ListItem>-20</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:TextBox ID="TxtEquipComp" runat="server" CssClass="form-controlWhite" ISDATE="False"
                                MASK="CC" MaxLength="2" ReadOnly="True" TabIndex="43" Visible="False" Width="100px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="TxtEquipComp_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers" TargetControlID="TxtEquipComp">
                            </asp:FilteredTextBoxExtender>
                            <br />
                            <br />
                            <asp:Label ID="lblMedical15" runat="server" CssClass="labelForControl" Font-Bold="False"
                                Visible="False">Customize Equipment Limit</asp:Label>
                            <asp:TextBox ID="TxtCustomizeEquipLimit0" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                ISDATE="False" MASK="CC" MaxLength="10" OnTextChanged="TxtCustomizeEquipLimit_TextChanged"
                                TabIndex="43" Visible="False" Width="100px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="TxtCustomizeEquipLimit0_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers" TargetControlID="TxtCustomizeEquipLimit0">
                            </asp:FilteredTextBoxExtender>
                            <asp:CheckBox ID="chkEquipColl" runat="server" AutoPostBack="True" CssClass="labelForControl"
                                Font-Bold="False" Text="Special Equip. Coll." TextAlign="Left" Visible="False" />
                            <br />
                            <br />
                            <asp:DropDownList ID="ddlAccidentDeath" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                OnSelectedIndexChanged="ddlAccidentDeath_SelectedIndexChanged" TabIndex="39" Visible="False"
                                Width="125px">
                                <asp:ListItem Value="0">$0.00</asp:ListItem>
                                <asp:ListItem Value="9">$30 / $900</asp:ListItem>
                                <asp:ListItem Value="17">$40 / $1,200</asp:ListItem>
                                <asp:ListItem Value="24">$50 / $1,500</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:TextBox ID="TxtEquitmentSonido" runat="server" CssClass="form-controlWhite" ISDATE="False"
                                MASK="CC" MaxLength="10" ReadOnly="True" TabIndex="43" Visible="False" Width="100px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="TxtEquitmentSonido_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers" TargetControlID="TxtEquitmentSonido">
                            </asp:FilteredTextBoxExtender>
                            <asp:TextBox ID="TxtAccidentDeathPremium" runat="server" CssClass="form-controlWhite"
                                ISDATE="False" MASK="CC" MaxLength="10" ReadOnly="True" TabIndex="43" Visible="False"
                                Width="100px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="TxtAccidentDeathPremium_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers" TargetControlID="TxtAccidentDeathPremium">
                            </asp:FilteredTextBoxExtender>
                            <asp:DropDownList ID="ddlEquitmentSonido" runat="server" AutoPostBack="True" CssClass="mediumTB"
                                OnSelectedIndexChanged="ddlEquitmentSonido_SelectedIndexChanged" TabIndex="40" Visible="False"
                                Width="125px">
                            </asp:DropDownList>
                            <br />
                            <br />

                            <asp:Label ID="lblMiscDisc" runat="server" CssClass="labelForControl" Height="16px" Visible="False">Miscellaneous
                                Discount</asp:Label>
                            <br />
                            <br />
                            <asp:TextBox ID="txtMiscDiscount" runat="server" Height="22px" IsCurrency="False" ISDATE="False"
                                MaxLength="4" TabIndex="34" Width="96px" Visible="False"></asp:TextBox>
                            <Toolkit:MaskedEditExtender ID="txtMiscDiscount_MaskedEditExtender" runat="server"
                                AcceptNegative="Left" CultureName="en-US" Mask="99.99" MaskType="Number"
                                TargetControlID="txtMiscDiscount">
                            </Toolkit:MaskedEditExtender>
                            <br />
                            <br />
                            <asp:TextBox ID="TxtEquipColl" runat="server" CssClass="form-controlWhite" ISDATE="False"
                                MASK="CC" MaxLength="2" ReadOnly="True" TabIndex="43" Visible="False" Width="100px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="TxtEquipColl_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers" TargetControlID="TxtEquipColl">
                            </asp:FilteredTextBoxExtender>
                            <br />
                            <br />
                            <asp:Label ID="lblMedical13" runat="server" CssClass="labelForControl" Font-Bold="False"
                                Visible="False">Customize Equipment Limit</asp:Label>
                            <asp:TextBox ID="TxtEquipTotal" runat="server" CssClass="form-controlWhite" ISDATE="False"
                                MASK="CC" MaxLength="2" ReadOnly="True" TabIndex="43" Visible="False" Width="100px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="TxtEquipTotal_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers" TargetControlID="TxtEquipTotal">
                            </asp:FilteredTextBoxExtender>
                            <asp:TextBox ID="TxtCustomizeEquipLimit" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                ISDATE="False" MASK="CC" MaxLength="10" OnTextChanged="TxtCustomizeEquipLimit_TextChanged"
                                TabIndex="43" Visible="False" Width="100px"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="TxtCustomizeEquipLimit_FilteredTextBoxExtender" runat="server"
                                FilterType="Numbers" TargetControlID="TxtCustomizeEquipLimit">
                            </asp:FilteredTextBoxExtender>
                            <br />

                        </div>

                        <div class="col-sm-1">
                        </div>
                        <div class="col-sm-3">
                            <br />
                            <asp:DataGrid ID="dgVehicle" runat="server" AutoGenerateColumns="False" Style="width: 80%;"
                                OnItemCommand="dgVehicle_ItemCommand1" OnItemCreated="dgVehicle_ItemCreated1" TabIndex="31"
                                Font-Bold="False" Visible="False">
                                <FooterStyle BackColor="Navy" CssClass="tableMain" ForeColor="#003399" />
                                <SelectedItemStyle BackColor="White" HorizontalAlign="Center" />
                                <EditItemStyle BackColor="White" HorizontalAlign="Center" />
                                <AlternatingItemStyle BackColor="#E1E1E1" HorizontalAlign="Center" />
                                <ItemStyle BackColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="Silver" HorizontalAlign="Center" Font-Bold="True" ForeColor="Black"
                                    Height="30px" />

                                <Columns>
                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Select" HeaderText="Sel."></asp:ButtonColumn>
                                    <asp:BoundColumn DataField="VehicleMake" HeaderText="Make"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="VehicleModel" HeaderText="Model"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="VIN" HeaderText="VIN"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Plate" HeaderText="Plate"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="VehicleYear" HeaderText="Year"></asp:BoundColumn>
                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Remove" HeaderText="Delete"></asp:ButtonColumn>
                                    <asp:BoundColumn DataField="QuotesAutoID" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="QuotesID" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="InternalID" Visible="False"></asp:BoundColumn>
                                </Columns>

                                <PagerStyle BackColor="White" CssClass="Numbers" ForeColor="Blue" HorizontalAlign="Left"
                                    Mode="NumericPages" />
                            </asp:DataGrid>
                            <br />
                            <br />
                            </table>
                            <input id="txtDeprecAll" style="z-index: 110; left: 26px; width: 30px; position: absolute; top: 290px; height: 25px"
                                type="hidden" size="1" name="txtDeprecAll" runat="server" />
                            <input id="txtDeprec1st" style="z-index: 108; left: 27px; width: 30px; position: absolute; top: 319px; height: 25px"
                                type="hidden" size="1" name="txtDeprec1st" runat="server" />
                            <input id="CostTemp" style="z-index: 109; left: 74px; width: 30px; position: absolute; top: 137px; height: 25px"
                                type="hidden" size="1" name="CostTemp" runat="server" />
                            <input id="_InternalID" style="z-index: 103; left: 24px; width: 30px; position: absolute; top: 190px; height: 25px"
                                type="hidden" size="1" name="_InternalID" runat="server" />
                            <input id="_QuoteAutoID" style="z-index: 102; left: 24px; width: 30px; position: absolute; top: 161px; height: 25px"
                                type="hidden" size="1" name="_QuoteAutoID" runat="server" />
                            <input id="_pageState" style="z-index: 101; left: 24px; width: 30px; position: absolute; top: 133px; height: 25px"
                                type="hidden" size="1" name="_pageState" runat="server" />
                            <asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
                            <asp:DropDownList ID="ddlSeatBelt" TabIndex="24" runat="server" Visible="False">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlAssistancePremium" TabIndex="20" runat="server" Visible="False">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtPurchaseDt" runat="server" Enabled="False" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtExpDt" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                            <asp:ImageButton ID="btnNew" Style="z-index: 111; left: 158px; position: absolute; top: 237px"
                                TabIndex="51" runat="server" CausesValidation="False" ToolTip="Add Driver"></asp:ImageButton>
                            <asp:Panel ID="pnlMessage" runat="server" CssClass="" Width="450px" BackColor="#F4F4F4"
                                Height="260px">
                                <div class="" style="padding: 0px; border-radius: 0px; background-color: #17529B;
                        color: #FFFFFF; font-size: 14px; font-weight: normal; 
                        background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                                    <asp:Label ID="Label55" runat="server" Font-Size="14pt" Text="Message..." ForeColor="White" />
                                </div>
                                <div style="padding: 0px; border-radius: 0px;">
                                    <table style="background-position: center; width: 430px; height: 175px;">
                                        <tr>
                                            <td align="center" valign="middle">
                                                <asp:Label ID="lblRecHeader" runat="server" Font-Bold="False"
                                                    Font-Italic="False" Font-Size="10.5pt" Font-Underline="False"
                                                    ForeColor="#333333" Text="Message" Width="350px" CssClass="Labelfield2-14" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="" align="center">
                                    <asp:Button ID="btnAceptar" runat="server" Text="OK" Width="150px" CssClass="btn btn-primary btn-lg btn-block" />
                                </div>
                            </asp:Panel>
                            <Toolkit:ModalPopupExtender ID="mpeSeleccion" runat="server" BackgroundCssClass="modalBackground"
                                CancelControlID="" DropShadow="True" OkControlID="btnAceptar" OnCancelScript=""
                                OnOkScript="" PopupControlID="pnlMessage" TargetControlID="btnDummy">
                            </Toolkit:ModalPopupExtender>
                            <asp:Button ID="btnDummy" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                BorderStyle="None" BorderWidth="0" Visible="true" />
                            <asp:TextBox ID="txtIsAssistanceEmp" runat="server" Visible="False"></asp:TextBox>

                            <br />
                        </div>
                    </div>
                    <%-- END DISCOUNT ACCORDION --%>
        </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </form>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.4.1/js/bootstrap-datepicker.min.js"></script>
    <script>
        function pageLoad() {

            $(document).ready(function () {
                var pp = $('#<%=TextBox1.ClientID%>');
                pp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "mm/dd/yyyy",
                    language: "tr"
                }).on('changeDate', function (ev) {
//                    $(this).blur();
//                    $(this).datepicker('hide');
                });
            })

            $(document).ready(function () {
                var pp = $('#<%=txtBirthDt.ClientID%>');
                pp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "mm/dd/yyyy",
                    language: "tr"
                }).on('changeDate', function (ev) {
                  //  $(this).blur();
                  //$(this).datepicker('hide');
                });
            })


            $(document).ready(function () {
                var pp = $('#<%=TxtpurchaseDate.ClientID%>');
                pp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "mm/dd/yyyy",
                    language: "tr"
                }).on('changeDate', function (ev) {
               //     $(this).blur();
//                    $(this).datepicker('hide');
                });
            })

            $(document).ready(function () {
                var pp = $('#<%=txtExpDate.ClientID%>');
                pp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "mm/dd/yyyy",
                    language: "tr"
                }).on('changeDate', function (ev) {
               //     $(this).blur();
//                    $(this).datepicker('hide');
                });
            })
        }
    </script>
</body>

</html>