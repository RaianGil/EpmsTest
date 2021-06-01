<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Page language="c#" Inherits="EPolicy.InsuranceCompany" CodeFile="InsuranceCompany.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ePMS | electronic Policy Manager Solution</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="baldrich.css" type="text/css" rel="stylesheet">
	</HEAD>
       <script src="js/jquery-1.12.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.mask.js" type="text/javascript"></script>

    <script type='text/javascript'>
        jQuery(function ($) {
            //$("#AccordionPane1_content_TxtWorkPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane1_content_TxtCellPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane1_content_TxtBirthDate").mask("00/00/0000", { placeholder: "__/__/____" });
            //          $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            $("#txtPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane3_content_TxtDriverBirthDate").mask("00/00/0000", { placeholder: "__/__/____" });

        });
    </script>
	<body background="Images2/SQUARE.GIF" bottomMargin="0" leftMargin="0"
		topMargin="19" rightMargin="0">
		<form method="post" runat="server">
			<TABLE id="Table8" style="Z-INDEX: 149; LEFT: 4px; WIDTH: 911px; POSITION: static; TOP: 4px; HEIGHT: 407px"
				cellSpacing="0" cellPadding="0" width="911" align="center" bgColor="white" dataPageSize="25"
				border="0">
				<TR>
					<TD style="WIDTH: 764px; HEIGHT: 1px" colSpan="3">
						<P>
							<asp:placeholder id="Placeholder1" runat="server"></asp:placeholder></P>
					</TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 5px; POSITION: static; HEIGHT: 395px" vAlign="top"
						align="right" colSpan="1" rowSpan="5">
						<P align="center">
							<asp:placeholder id="phTopBanner1" runat="server"></asp:placeholder></P>
					</TD>
					<TD style="FONT-SIZE: 0pt; WIDTH: 86px; HEIGHT: 184px" align="center">
						<P align="center">
							<TABLE id="Table2" style="WIDTH: 809px; HEIGHT: 387px" cellSpacing="0" cellPadding="0"
								width="809" bgColor="white" border="0">
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="center"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table1" style="WIDTH: 808px; HEIGHT: 12px" cellSpacing="0" cellPadding="0" width="808"
											border="0">
											<TR>
												<TD style="WIDTH: 349px" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
													<asp:Label id="Label2" runat="server" Font-Size="11pt" Font-Names="tahoma,11pt" CssClass="headForm1 "
														Font-Bold="True" ForeColor="Navy">Insurance Company ID:</asp:Label>
													<asp:label id="lblInsuranceCompanyID" runat="server" Width="136px" Font-Size="Smaller" Font-Names="Tahoma"></asp:label></TD>
												<TD align="right">
													<asp:button id="btnAuditTrail" tabIndex="45" runat="server" Width="55px" 
                                                        BorderWidth="0px" BorderColor="MidnightBlue"
														Height="23px" Text="AuditTrail" ForeColor="White" Font-Names="Tahoma" BackColor="MidnightBlue" 
                                                        onclick="btnAuditTrail_Click" Visible="False"></asp:button>
													<asp:button id="btnPrint" tabIndex="45" runat="server" BackColor="MidnightBlue" Width="52px"
														BorderColor="MidnightBlue" Height="23px" BorderWidth="0px" Font-Names="Tahoma" Text="Print"
														ForeColor="White" onclick="btnPrint_Click" Visible="False"></asp:button>
													<asp:button id="btnSearch" tabIndex="45" runat="server" BackColor="MidnightBlue" Width="52px"
														BorderColor="MidnightBlue" Height="23px" BorderWidth="0px" Font-Names="Tahoma" Text="Search"
														ForeColor="White" onclick="btnSearch_Click"></asp:button>
													<asp:button id="btnAddNew" tabIndex="45" runat="server" BackColor="MidnightBlue" Width="52px"
														BorderColor="MidnightBlue" Height="23px" BorderWidth="0px" Font-Names="Tahoma" Text="Add"
														ForeColor="White" onclick="btnAddNew_Click"></asp:button>
													<asp:button id="btnEdit" tabIndex="45" runat="server" BackColor="MidnightBlue" Width="52px"
														BorderColor="MidnightBlue" Height="23px" BorderWidth="0px" Font-Names="Tahoma" Text="Edit"
														ForeColor="White" onclick="btnEdit_Click"></asp:button>
													<asp:button id="BtnSave" tabIndex="45" runat="server" BackColor="MidnightBlue" Width="52px"
														BorderColor="MidnightBlue" Height="23px" BorderWidth="0px" Font-Names="Tahoma" Text="Save"
														ForeColor="White" onclick="BtnSave_Click"></asp:button>
													<asp:button id="cmdCancel" tabIndex="45" runat="server" BackColor="MidnightBlue" Width="52px"
														BorderColor="MidnightBlue" Height="23px" BorderWidth="0px" Font-Names="Tahoma" Text="Cancel"
														ForeColor="White" onclick="cmdCancel_Click"></asp:button>
													<asp:button id="BtnExit" tabIndex="45" runat="server" BackColor="MidnightBlue" Width="52px"
														BorderColor="MidnightBlue" Height="23px" BorderWidth="0px" Font-Names="Tahoma" Text="Exit"
														ForeColor="White" onclick="BtnExit_Click"></asp:button>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; HEIGHT: 5px">
										<TABLE id="Table11" style="WIDTH: 808px" borderColor="#4b0082" height="1" cellSpacing="0"
											borderColorDark="#4b0082" cellPadding="0" width="808" bgColor="indigo" borderColorLight="#4b0082"
											border="0">
											<TR>
												<TD></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 5pt; WIDTH: 112px; HEIGHT: 192px" vAlign="middle" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table6" style="WIDTH: 802px; HEIGHT: 314px" cellSpacing="0" cellPadding="0"
											width="802" border="0">
											<TR>
												<TD style="FONT-SIZE: 1pt" align="center">
													<TABLE id="Table4" style="WIDTH: 784px; HEIGHT: 10px" cellSpacing="0" cellPadding="0" width="784"
														border="0">
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 1px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 1px"><asp:label id="lblB" CSSCLASS="headfield1" RUNAT="server" HEIGHT="19px" ForeColor="Black" Font-Names="Tahoma"
																	Font-Size="Smaller">Insurance Description</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 1px">
																<asp:textbox id="txtInsuranceDescription" tabIndex="1" RUNAT="server" CSSCLASS="headfield1" HEIGHT="22px"
																	WIDTH="249px" MAXLENGTH="30"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 1px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 10px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 10px">
																<asp:label id="lblInsuranceName" Width="99px" Font-Size="Smaller" Font-Names="Tahoma" RUNAT="server"
																	CSSCLASS="headfield1">Insurance Name</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 10px">
																<asp:textbox id="txtInsuranceName" tabIndex="2" RUNAT="server" CSSCLASS="headfield1" HEIGHT="22px"
																	WIDTH="249" MAXLENGTH="10"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 10px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 1px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 1px"><asp:label id="lblAddress1" Width="99px" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma"
																	Font-Size="Smaller"> Address 1</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 1px">
																<asp:textbox id="txtAddress1" tabIndex="3" RUNAT="server" CSSCLASS="headfield1" HEIGHT="22px"
																	WIDTH="249" MAXLENGTH="60"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 1px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px"><asp:label id="lblAddress2" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma" Font-Size="Smaller"> Address 2</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"><asp:textbox id="txtAddress2" tabIndex="4" 
                                                                    CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="60"
																	WIDTH="249" HEIGHT="22px"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px"><asp:label id="lblCity" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma" Font-Size="Smaller">City</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"><asp:textbox id="txtCity" tabIndex="5" CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="10" WIDTH="142"
																	HEIGHT="22px"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px"><asp:label id="lblState" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma" Font-Size="Smaller">State</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"><asp:textbox id="txtSt" tabIndex="6" CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="2" WIDTH="142px"
																	HEIGHT="22px"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px"><asp:label id="lblZipCode" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma" Font-Size="Smaller">Zip Code</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
                                                                <asp:TextBox id="txtZipCode" tabIndex="7" CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="10"
																	WIDTH="142px" HEIGHT="22px"></asp:TextBox>
                                                            </TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<P id="P1" runat="server"><asp:label id="lblPhone" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma" Font-Size="Smaller">Phone </asp:label></P>
															</TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
																
                                                                <asp:TextBox id="txtPhone" tabIndex="8" runat="server" Width="142px" Height="22px" CssClass="headfield1"></asp:TextBox>
                                                                    </TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<asp:label id="lblEntryDate" Font-Size="Smaller" Font-Names="Tahoma" 
                                                                    RUNAT="server" CSSCLASS="headfield1" Visible="False">Entry Date</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
																<maskedinput:maskedtextbox id="txtEntryDate" tabIndex="9" RUNAT="server" 
                                                                    CSSCLASS="headfield1" HEIGHT="22px"
																	WIDTH="101px" ISDATE="True" Visible="False"></maskedinput:maskedtextbox>&nbsp;<INPUT 
                                                                    id="btnCalendar" style="WIDTH: 21px; HEIGHT: 21px" onclick="javascript:calendar_window=window.open('selectDate.aspx?formname=Agency.txtEntryDate','calendar_window','width=250,height=250');calendar_window.focus()"
																	tabIndex="7" type="button" value="..." name="btnCalendar" RUNAT="server" visible="False"></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<asp:label id="lblSequence" Font-Size="Smaller" Font-Names="Tahoma" 
                                                                    RUNAT="server" CSSCLASS="headfield1" Visible="False">Sequence</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
																<asp:textbox id="txtSequence" tabIndex="10" RUNAT="server" 
                                                                    CSSCLASS="headfield1" HEIGHT="22px"
																	WIDTH="142px" MAXLENGTH="5" Visible="False"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<asp:checkbox id="chkPrintPolicy" runat="server" Width="102px" Font-Size="Smaller" Font-Names="Tahoma"
																	Visible="False" Text="Print Policy" CssClass="headfield1" TextAlign="Left" AutoPostBack="True"></asp:checkbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
																<asp:checkbox id="chkPrintCancel" runat="server" Width="102px" Font-Size="Smaller" Font-Names="Tahoma"
																	Visible="False" Text="Print Cancel" CssClass="headfield1" TextAlign="Left" AutoPostBack="True"></asp:checkbox>
																<asp:checkbox id="chkPrintLabels" runat="server" Width="102" Font-Size="Smaller" Font-Names="Tahoma"
																	Visible="False" Text="Print Labels" CssClass="headfield1" TextAlign="Left" AutoPostBack="True"></asp:checkbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 18px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 18px">
																<asp:label id="lblApuntador" Width="60px" Font-Size="Smaller" Font-Names="Tahoma" Visible="False"
																	RUNAT="server" CSSCLASS="headfield1">Apuntador</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 18px">
																<asp:textbox id="txtApuntador" Visible="False" RUNAT="server" CSSCLASS="headfield1" HEIGHT="22px"
																	WIDTH="126px" MAXLENGTH="3"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 18px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<asp:label id="lblQuote" Width="57px" Font-Size="Smaller" Font-Names="Tahoma" Visible="False"
																	RUNAT="server" CSSCLASS="headfield1">Quote</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
																<asp:textbox id="txtQuote" Visible="False" RUNAT="server" CSSCLASS="headfield1" HEIGHT="22px"
																	WIDTH="95px" MAXLENGTH="3"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<asp:label id="lblApun_Trans" Width="57px" Font-Size="Smaller" Font-Names="Tahoma" Visible="False"
																	RUNAT="server" CSSCLASS="headfield1">Apun_Trams</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
																<asp:textbox id="txtApun_Trams" Visible="False" RUNAT="server" CSSCLASS="headfield1" HEIGHT="22px"
																	WIDTH="95px" MAXLENGTH="3"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 5pt; WIDTH: 112px; HEIGHT: 1px" align="left"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 35px; HEIGHT: 27px"></TD>
								</TR>
							</TABLE>
						</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
					</TD>
				</TR>
			</TABLE>
			<maskedinput:maskedtextheader id="MaskedTextHeader1" runat="server"></maskedinput:maskedtextheader>
			<asp:imagebutton id="btnAddNew1" style="Z-INDEX: 135; LEFT: 249px; POSITION: absolute; TOP: 585px"
				tabIndex="9" runat="server" Visible="False" ImageUrl="Images/addNew_btn.gif" CausesValidation="False"></asp:imagebutton>
			<asp:imagebutton id="btnEdit1" style="Z-INDEX: 136; LEFT: 289px; POSITION: absolute; TOP: 585px"
				tabIndex="9" runat="server" Visible="False" ImageUrl="Images/edit_btn.GIF" CausesValidation="False"></asp:imagebutton>
			<asp:imagebutton id="BtnSave1" style="Z-INDEX: 137; LEFT: 329px; POSITION: absolute; TOP: 585px"
				tabIndex="10" Visible="False" RUNAT="server" CAUSESVALIDATION="False" TOOLTIP="Save Person Information"
				IMAGEURL="Images/save_btn.gif"></asp:imagebutton>
			<asp:imagebutton id="btnSearch1" style="Z-INDEX: 138; LEFT: 369px; POSITION: absolute; TOP: 585px"
				tabIndex="9" runat="server" Visible="False" ImageUrl="Images/search_btn.gif" CausesValidation="False"></asp:imagebutton>
			<asp:imagebutton id="cmdCancel1" style="Z-INDEX: 139; LEFT: 409px; POSITION: absolute; TOP: 585px"
				tabIndex="11" runat="server" Visible="False" ImageUrl="Images/cancel_btn.gif" CausesValidation="False"></asp:imagebutton>
			<asp:imagebutton id="btnPrint1" style="Z-INDEX: 140; LEFT: 441px; POSITION: absolute; TOP: 585px"
				tabIndex="7" runat="server" Visible="False" ImageUrl="Images/printreport_btn.gif" AlternateText="Print Report"></asp:imagebutton>
			<asp:ImageButton id="btnAuditTrail1" style="Z-INDEX: 141; LEFT: 537px; POSITION: absolute; TOP: 593px"
				runat="server" Width="48px" Height="19px" Visible="False" ImageUrl="Images/History_btn.bmp"></asp:ImageButton>
			<asp:imagebutton id="BtnExit1" style="Z-INDEX: 142; LEFT: 593px; POSITION: absolute; TOP: 593px"
				tabIndex="12" Visible="False" RUNAT="server" CAUSESVALIDATION="False" IMAGEURL="Images/exit_btn.gif"></asp:imagebutton>
			<asp:label id="lblRecordCount" style="Z-INDEX: 143; LEFT: 249px; POSITION: absolute; TOP: 617px"
				Visible="False" RUNAT="server" CSSCLASS="headform2" HEIGHT="17px" WIDTH="129px">1 of 1</asp:label>
			<asp:button id="BtnBegin" style="Z-INDEX: 144; LEFT: 401px; POSITION: absolute; TOP: 617px"
				tabIndex="14" runat="server" Height="23px" Visible="False" Text="<<" onclick="BtnBegin_Click"></asp:button>
			<asp:button id="BtnPrevious" style="Z-INDEX: 145; LEFT: 433px; POSITION: absolute; TOP: 617px"
				tabIndex="15" runat="server" Width="24px" Height="23px" Visible="False" Text="<" onclick="BtnPrevious_Click"></asp:button>
			<asp:button id="BtnNext" style="Z-INDEX: 146; LEFT: 465px; POSITION: absolute; TOP: 617px" tabIndex="16"
				runat="server" Width="24px" Height="23px" Visible="False" Text=">" onclick="BtnNext_Click"></asp:button>
			<asp:literal id="litPopUp" RUNAT="server" VISIBLE="False"></asp:literal>
			<P>
				<asp:button id="BtnEnd" style="Z-INDEX: 147; LEFT: 497px; POSITION: absolute; TOP: 617px" tabIndex="17"
					runat="server" Height="23px" Visible="False" Text=">>" onclick="BtnEnd_Click"></asp:button></P>
		</form>
	</body>
</HTML>
