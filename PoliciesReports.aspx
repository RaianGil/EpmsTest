<%@ Page language="c#" Inherits="EPolicy.PoliciesReports" CodeFile="PoliciesReports.aspx.cs" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
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
            //$("#AccordionPane1_content_TxtWorkPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane1_content_TxtCellPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#txtBegDate").mask("00/00/0000", { placeholder: "__/__/____" });
            $("#TxtEndDate").mask("00/00/0000", { placeholder: "__/__/____" });
            
            ////          $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            //$("#txtWorkPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
        });
    </script>
</head>
<body>
		<form id="Rep" method="post" runat="server">
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
                            Reports</h1>
                        <div class="form=group" align="center">
                        <asp:button id="btnDownLoad" runat="server" Text="DOWNLOAD" 
                                onclick="btnDownLoad_Click" Visible="False" Width="155px" 
                                CssClass="btn btn-primary btn-lg"></asp:button>
                        <asp:Button ID="BtnPrint2" runat="server" OnClick="BtnPrint2_Click" 
                                Text="PRINTOLD"  Visible="False" Width="155px" 
                                CssClass="btn btn-primary btn-lg"/>
                        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" Text="PRINT" 
                                Width="155px" CssClass="btn btn-primary btn-lg" TabIndex="1"/>
                        <asp:button id="BtnExit" runat="server" Text="EXIT" onclick="BtnExit_Click" 
                                Width="155px" CssClass="btn btn-primary btn-lg" TabIndex="1008"></asp:button>
                         <br />
                            <br />
                            <div align="left">
                             <asp:Label id="Label8" runat="server" Font-Bold="True" >Report List</asp:Label>                   
                            </div>
                        </div>
                         <div class="form-group" align="center">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="10">
                                <ProgressTemplate>
                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                    <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span> </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                         <div class="row formWraper" style="padding: 0px;" align="center">
                            <div class="col-sm-12" align="center">
                            <br />
                             <asp:label id="lblBusiness" runat="server" CssClass="labelForControl" 
                                    Font-Bold="True">Line of Business</asp:label>
                            <br />
                            <asp:dropdownlist id="ddlCompanyType" tabIndex="1002" runat="server" onselectedindexchanged="ddlCompanyType_SelectedIndexChanged"   
                                    CssClass="form-controlWhite" Width="300px" AutoPostBack="True">
                                <asp:ListItem Selected="True"></asp:ListItem>
								<asp:ListItem Value="1">Auto VI</asp:ListItem>
								<asp:ListItem Value="2">GuardianXtra</asp:ListItem>
							    <asp:ListItem Value="3">Double Interest</asp:ListItem>
                                <asp:ListItem Value="4">Home Owner</asp:ListItem>
                                <asp:ListItem Value="5">Yacht</asp:ListItem>
                                <asp:ListItem Value="6">Auto Personal</asp:ListItem>
                                <asp:ListItem Value="7">Road Assist</asp:ListItem>
                                <asp:ListItem Value="8">Auto High Limit</asp:ListItem>

							</asp:dropdownlist>
                                <br />
                                <asp:radiobuttonlist id="rblAutoGuardReports" style="Design_Time_Lock: False" 
                                tabIndex="1000" runat="server"
								 CssClass="labelForControl" Design_Time_Lock="False" 
                                AutoPostBack="True" 
                                onselectedindexchanged="rblAutoGuardReports_SelectedIndexChanged" 
                                RepeatColumns="2">
								<asp:ListItem Value="0" Selected="True">Account Current Statement</asp:ListItem>
								<asp:ListItem Value="1">Policy Premium Written</asp:ListItem>
                                <asp:ListItem Value="2">Renewals Report</asp:ListItem>
                                <asp:ListItem Value="3">Quotes vs  Policies Issued</asp:ListItem>
							    <asp:ListItem Value="4">Guardian Payments</asp:ListItem>
							    <asp:ListItem Value="5">Renewal ID Cards</asp:ListItem>
                                <asp:ListItem Value="6">Yacht Policies with Pending Fields</asp:ListItem>
							</asp:radiobuttonlist>
                                 
                            <div align="center" style="background-color:Gray;height:35px">
                                <asp:Label id="Label1" runat="server" Font-Bold="True" ForeColor="White">Report Filter</asp:Label>                   
                            </div>
                              <asp:label id="LblDataType" RUNAT="server" CssClass="labelForControl">Date Type</asp:label>
								 <br />							
                            <asp:dropdownlist id="ddlDateType" tabIndex="1002" runat="server"  
                                    CssClass="form-controlWhite" Width="300px">
								<asp:ListItem Value="F" Selected="True">Effective Date</asp:ListItem>
								<asp:ListItem Value="E">Entry Date</asp:ListItem>
							</asp:dropdownlist>
                             <br />		
                              <br />		
			                <asp:label id="lblDateFrom" runat="server" CssClass="labelForControl">Date From</asp:label>
                             <br />		
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="txtBegDate" TabIndex="1003" runat="server" ISDATE="True"  CssClass="form-controlWhite" 
                            Width="300px"></asp:TextBox>
                                &nbsp;
                                <Toolkit:CalendarExtender ID="txtBegDate_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="imgCalendarBeg" TargetControlID="txtBegDate"  CssClass="Calendar">
                            </Toolkit:CalendarExtender>
                            <%--<Toolkit:MaskedEditExtender ID="txtBegDate_MaskedEditExtender" runat="server" CultureName="en-US"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="txtBegDate">
                            </Toolkit:MaskedEditExtender>--%>
                            <asp:ImageButton ID="imgCalendarBeg" runat="server" ImageUrl="~/Images2/Calendar.png"
                                TabIndex="4" Width="25px" Height="25px"/>
                             <br />		
                              <br />		
	                        <asp:label id="lblToDate" runat="server" CssClass="labelForControl">To</asp:label>
                                               <br />		                      
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="TxtEndDate" TabIndex="1004" runat="server" ISDATE="True"  
                                    CssClass="form-controlWhite" Width="300px"></asp:TextBox>
                            &nbsp;
                            <Toolkit:CalendarExtender ID="TxtEndDate_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="imgCalendarEnd" TargetControlID="TxtEndDate"  CssClass="Calendar">
                            </Toolkit:CalendarExtender>
<%--                            <Toolkit:MaskedEditExtender ID="TxtEndDate_MaskedEditExtender" runat="server" CultureName="en-US"
                                Mask="99/99/9999" MaskType="Date" TargetControlID="TxtEndDate">
                            </Toolkit:MaskedEditExtender>--%>
                            <asp:ImageButton ID="imgCalendarEnd" runat="server" ImageUrl="~/Images2/Calendar.png"
                                TabIndex="6" Width="25px" Height="25px"/>
                             <br />		
                              <br />		
                               <asp:Label ID="lblAgent" runat="server" CssClass="labelForControl">Agent:</asp:Label>
															<br />			
                        <asp:dropdownlist id="ddlAgent" tabIndex="1005" runat="server" AutoPostBack="True"  
                                    CssClass="form-controlWhite" Width="300px">
                        </asp:DropDownList>
                        <br />	
                        <br />	
                         <asp:Label ID="lblPolicyType" runat="server" Visible="False" CssClass="labelForControl">Policy Type:</asp:Label>
											<br />					
                    <asp:dropdownlist id="ddlPolicyType" runat="server"  CssClass="form-controlWhite" 
                                    Width="300px" TabIndex="1006">
                    </asp:DropDownList>
                    <br />	
                    <br />	
                     <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                            <br /><br />
                    	<rsweb:ReportViewer ID="ReportViewer2" runat="server" Width="1166px" 
                                            Height="527px">
                                        </rsweb:ReportViewer>
                                    


															<asp:checkbox id="ChkSummary" runat="server" Font-Size="9pt" Font-Names="Tahoma" CssClass="headform3"
																	Text="Summary"></asp:checkbox>
                                                                &nbsp;&nbsp;
                                                                <asp:CheckBox ID="ChkAverages" runat="server" CssClass="headform3" Font-Names="Tahoma"
                                                                    Font-Size="9pt" Text="Averages" />
                                                                &nbsp;&nbsp;
                                                                <asp:CheckBox ID="chkVscFile" runat="server" CssClass="headform3" Font-Names="Tahoma"
                                                                    Font-Size="9pt" Text="Create File" />
                                                                &nbsp;&nbsp;
                                                                <asp:CheckBox ID="chkPartial" runat="server" CssClass="headform3" Font-Names="Tahoma"
                                                                    Font-Size="9pt" Text="Partial" />
                                                                <asp:label id="lblOutstanding" Font-Size="10pt" 
                                                                    Font-Names="Tahoma" ForeColor="Black"
																	RUNAT="server" Visible="False"> Policy Outstanding as of:</asp:label>
															<maskedinput:maskedtextbox id="txtOutstandingDate" tabIndex="5" 
                                                                    Font-Size="9pt" Font-Names="Tahoma" WIDTH="89px"
																	RUNAT="server" CSSCLASS="headfield1" HEIGHT="19px" ISDATE="True" Visible="False"></maskedinput:maskedtextbox>
                                                                <INPUT id="btnCalendar3" style="WIDTH: 21px; HEIGHT: 21px" onclick="javascript:calendar_window=window.open('selectDate.aspx?formname=document.Rep.txtOutstandingDate','calendar_window','width=250,height=250');calendar_window.focus()"
																	tabIndex="6" type="button" value="..." name="btnCalendar" RUNAT="server" visible="False">
														
                                                                <asp:label id="lblPaidEntry" Font-Size="10pt" Font-Names="Tahoma" ForeColor="Black"
																	RUNAT="server" CSSCLASS="headfield1" Visible="False">Paid Entry:</asp:label></TD>
															<maskedinput:maskedtextbox id="txtFollowUpCancellation" 
                                                                    tabIndex="5" Font-Size="9pt" Font-Names="Tahoma" WIDTH="89px"
																	RUNAT="server" CSSCLASS="headfield1" HEIGHT="21" ISDATE="True" Visible="False"></maskedinput:maskedtextbox>
                                                                <INPUT id="btnCalendar4" style="WIDTH: 21px; HEIGHT: 21px" onclick="javascript:calendar_window=window.open('selectDate.aspx?formname=document.Rep.txtOutstandingDate','calendar_window','width=250,height=250');calendar_window.focus()"
																	tabIndex="6" type="button" value="..." name="btnCalendar" RUNAT="server" visible="False">
														
                                                               

															
                                                               
														<asp:label id="lblCompanyDealer" Font-Size="10pt" 
                                                                    Font-Names="Tahoma" ForeColor="Black" RUNAT="server" CSSCLASS="headfield1" 
                                                                    Visible="False">Company Dealer:</asp:label>
															
                                                                <asp:dropdownlist id="ddlBank" tabIndex="7" Font-Size="9pt" 
                                                                    Font-Names="Tahoma" WIDTH="275px" RUNAT="server"
																	CSSCLASS="headfield1" HEIGHT="19px" Visible="False"></asp:dropdownlist>
                                                                <asp:dropdownlist id="ddlDealer" tabIndex="7" Font-Size="9pt" 
                                                                    Font-Names="Tahoma" WIDTH="275px" RUNAT="server"
																	CSSCLASS="headfield1" HEIGHT="19px" Visible="False"></asp:dropdownlist>
													
                                                                <asp:label id="lblInsuranceCompany" Font-Size="10pt" 
                                                                    Font-Names="Tahoma" ForeColor="Black" RUNAT="server" CSSCLASS="headfield1" 
                                                                    Visible="False">Insurance Company:</asp:label>
															<asp:dropdownlist id="ddlInsuranceCompany" tabIndex="7" 
                                                                    Font-Size="9pt" Font-Names="Tahoma" WIDTH="238px"
																	RUNAT="server" CSSCLASS="headfield1" HEIGHT="19px" Visible="False"></asp:dropdownlist>
													
                                                                <asp:label id="lblMonth" Font-Size="10pt" Font-Names="Tahoma" ForeColor="Black"
																	Visible="False" RUNAT="server" CSSCLASS="headfield1">Month:</asp:label>
														
															<asp:dropdownlist id="ddlMonths" runat="server" Width="111px" Visible="False">
																	<asp:ListItem Value="1">January</asp:ListItem>
																	<asp:ListItem Value="2">February</asp:ListItem>
																	<asp:ListItem Value="3">March</asp:ListItem>
																	<asp:ListItem Value="4">April</asp:ListItem>
																	<asp:ListItem Value="5">May</asp:ListItem>
																	<asp:ListItem Value="6">June</asp:ListItem>
																	<asp:ListItem Value="7">July</asp:ListItem>
																	<asp:ListItem Value="8">August</asp:ListItem>
																	<asp:ListItem Value="9">September</asp:ListItem>
																	<asp:ListItem Value="10">October</asp:ListItem>
																	<asp:ListItem Value="11">November</asp:ListItem>
																	<asp:ListItem Value="12">December</asp:ListItem>
																</asp:dropdownlist>

                                                                <asp:label id="lblYear" 
                                                                    Font-Size="10pt" Font-Names="Tahoma" ForeColor="Black"
																		Visible="False" RUNAT="server" CSSCLASS="headfield1">Year:</asp:label>
															
                                                                <asp:dropdownlist id="ddlYears" runat="server" Width="111px" Visible="False" Font-Names="Tahoma" Font-Size="9pt"></asp:dropdownlist>
                                                      
                                                                <asp:Label ID="lblQuarter" runat="server" CssClass="headfield1" Font-Names="Tahoma"
                                                                    Font-Size="10pt" ForeColor="Black" Visible="False">Quarter</asp:Label></td>
                                                            <td align="left">
                                                                <asp:dropdownlist id="ddlQuarter" runat="server" Width="111px" Visible="False" Font-Names="Tahoma" Font-Size="9pt">
                                                                    <asp:ListItem Value="1">Quarter I</asp:ListItem>
                                                                    <asp:ListItem Value="2">Quarter II</asp:ListItem>
                                                                    <asp:ListItem Value="3">Quarter III</asp:ListItem>
                                                                    <asp:ListItem Value="4">Quarter IV</asp:ListItem>
                                                                </asp:DropDownList>
                                                       
                                                                <asp:Label ID="lblPending" runat="server" CssClass="headfield1" Font-Names="Tahoma"
                                                                    Font-Size="10pt" ForeColor="Black" Visible="False">Pending or Active:</asp:Label>
                                                                <asp:dropdownlist id="ddlVSCPending" runat="server" Width="275px" 
                                                                    Visible="False" Font-Names="Tahoma" Font-Size="9pt">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>Active</asp:ListItem>
                                                                    <asp:ListItem>Pending</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:label id="lblPolicyClass" Font-Size="10pt" 
                                                                    Font-Names="Tahoma" ForeColor="Black"
																	RUNAT="server" CSSCLASS="headfield1" Visible="False">Line Of  Business:</asp:label>
                                                                <asp:dropdownlist id="ddlPolicyClass" tabIndex="7" Font-Size="9pt" 
                                                                    Font-Names="Tahoma" WIDTH="275px"
																	RUNAT="server" CSSCLASS="headfield1" HEIGHT="19px" AutoPostBack="True" 
                                                                    OnSelectedIndexChanged="ddlPolicyClass_SelectedIndexChanged" 
                                                                    Visible="False"></asp:dropdownlist>
                                                        
                                                                <asp:Label ID="lblFilter" runat="server" CssClass="headfield1" 
                                                                    Font-Names="Tahoma" Font-Size="10pt"
                                                                    ForeColor="Black" Height="19px" Visible="False">Filter:</asp:Label>
                                                            
                                                                <asp:dropdownlist id="ddlFilter" runat="server" Width="275px" 
                                                                    Font-Names="Tahoma" Font-Size="9pt" Visible="False">
                                                                    <asp:ListItem Value="A">Premium &amp; Cancellation</asp:ListItem>
                                                                    <asp:ListItem Value="P">Premium</asp:ListItem>
                                                                    <asp:ListItem Value="C">Cancellation</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblCancellationMethod" runat="server" CssClass="headfield1" 
                                                                    Font-Names="Tahoma" Font-Size="10pt"
                                                                    ForeColor="Black" Visible="False">Cancellation Method:</asp:Label>
                                                           
                                                                <asp:DropDownList ID="ddlCancellationMethod" runat="server" Font-Names="Tahoma" Font-Size="9pt"
                                                                    Width="275px" Visible="False">
                                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                                    <asp:ListItem Value="1">ProRata</asp:ListItem>
                                                                    <asp:ListItem Value="2">ShortRate</asp:ListItem>
                                                                    <asp:ListItem Value="3">Flat</asp:ListItem>
                                                                    <asp:ListItem Value="99">Reinstatement</asp:ListItem>
                                                                </asp:DropDownList>
                                                        
                                                                <asp:RadioButtonList ID="rblPremWrittenOrder" runat="server" Font-Names="Tahoma" Font-Size="9pt" OnSelectedIndexChanged="rblPremWrittenOrder_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">By Dealer</asp:ListItem>
                                                                    <asp:ListItem Value="1">By Bank</asp:ListItem>
                                                                </asp:RadioButtonList>

                                                                    <asp:radiobuttonlist id="rblCertificatePaidOrder" runat="server" Font-Size="9pt" Font-Names="Tahoma" Height="34px" CssClass="HeadField1" 
                                                                    AutoPostBack="True" Visible="False" 
                                                                    style="Z-INDEX: 104; LEFT: 1409px; POSITION: absolute; TOP: 938px; width: 351px;" 
                                                                    onselectedindexchanged="rblCertificatePaidOrder_SelectedIndexChanged">
				                                            <asp:ListItem Value="0" Selected="True">By Co. Dealer / Ins. Co.</asp:ListItem>
				                                            <asp:ListItem Value="1">By Company Dealer</asp:ListItem>
				                                            <asp:ListItem Value="2">Non Commission</asp:ListItem>
			                                            </asp:radiobuttonlist>										
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
                   	<asp:literal id="litPopUp" RUNAT="server" VISIBLE="False"></asp:literal>
            <maskedinput:maskedtextheader id="MaskedTextHeader1" RUNAT="server"></maskedinput:maskedtextheader>
			<asp:imagebutton id="btnPrint1" style="Z-INDEX: 102; LEFT: 39px; POSITION: absolute; TOP: 222px"
				tabIndex="7" runat="server" Height="19" Visible="False" ImageUrl="Images/printreport_btn.gif" AlternateText="Print Report"></asp:imagebutton>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
       </div>
    </form>

<%--        <script>
     $(document).ready(function () {
//            $('#TxtBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
//            $('#TxtEffBinderDate').mask('00/00/0000', { placeholder: '__/__/____' });
//            $('#TxtExpBinderDate').mask('00/00/0000', { placeholder: '__/__/____' });
//            $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
//            $('#TxtWorkPhone').mask('(000) 000-0000', { placeholder: '(###) ###-####' });
//            $('#TxtCellPhone').mask('(000) 000-0000', { placeholder: '(###) ###-####' });
//            $('#TxtDriverGrade').mask('0.00', { placeholder: '#.##' });

            $('#txtBegDate').mask('AB/CD/EFGH', { 'translation': {
                A: { pattern: /[0-1]/ },
                B: { pattern: /[0-9]/ },
                C: { pattern: /[0-3]/ },
                D: { pattern: /[0-9]/ },
                E: { pattern: /[1-2]/ },
                F: { pattern: /[0-9]/ },
                G: { pattern: /[0-9]/ },
                H: { pattern: /[0-9]/ }
            }
        });
            $('#TxtEndDate').mask('AB/CD/EFGH', { 'translation': {
                A: { pattern: /[0-1]/ },
                B: { pattern: /[0-9]/ },
                C: { pattern: /[0-3]/ },
                D: { pattern: /[0-9]/ },
                E: { pattern: /[1-2]/ },
                F: { pattern: /[0-9]/ },
                G: { pattern: /[0-9]/ },
                H: { pattern: /[0-9]/ }
            }
        });
        });
    </script>--%>
	</body>
</HTML>
