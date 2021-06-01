<%@ Page language="c#" Inherits="EPolicy.Bank" CodeFile="Bank.aspx.cs" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ePMS | electronic Policy Manager Solution</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="baldrich.css" type="text/css" rel="stylesheet">
	    <style type="text/css">
            .headfield1
            {}
        </style>
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
			<TABLE id="Table8" style="Z-INDEX: 138; LEFT: 4px; WIDTH: 911px; POSITION: static; TOP: 4px; HEIGHT: 281px"
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
							<TABLE id="Table2" style="WIDTH: 809px; HEIGHT: 99px" cellSpacing="0" cellPadding="0" width="809"
								bgColor="white" border="0">
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="center"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table1" style="WIDTH: 808px; HEIGHT: 12px" cellSpacing="0" cellPadding="0" width="808"
											border="0">
											<TR>
												<TD style="WIDTH: 272px" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
													<asp:Label id="Label2" runat="server" Font-Size="11pt" Font-Names="tahoma,11pt" CssClass="headForm1 "
														Font-Bold="True" ForeColor="Navy">Bank ID:</asp:Label>
													<asp:label id="lblBankID" runat="server" Width="155px" Font-Size="Smaller" Font-Names="Tahoma"></asp:label></TD>
												<TD align="right">
													<asp:button id="btnAuditTrail" tabIndex="45" runat="server" Width="62px" 
                                                        BorderWidth="0px" BorderColor="MidnightBlue"
														Height="23px" Text="AuditTrail" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" 
                                                        onclick="btnAuditTrail_Click" Visible="False"></asp:button>
													<asp:button id="btnPrint" tabIndex="45" runat="server" Width="52px" 
                                                        BorderWidth="0px" BorderColor="MidnightBlue"
														Height="23px" Text="Print" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" onclick="btnPrint_Click" 
                                                        Visible="False"></asp:button>
													<asp:button id="btnSearch" tabIndex="45" runat="server" BackColor="MidnightBlue" Width="52px"
														BorderColor="MidnightBlue" Height="23px" BorderWidth="0px" Font-Names="Tahoma" Text="Search"
														ForeColor="White" onclick="btnSearch_Click"></asp:button>
													<asp:button id="btnAddNew" tabIndex="45" runat="server" Width="52px" BorderWidth="0px" BorderColor="MidnightBlue"
														Height="23px" Text="Add" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" onclick="btnAddNew_Click"></asp:button>
													<asp:button id="btnEdit" tabIndex="45" runat="server" Width="52px" BorderWidth="0px" BorderColor="MidnightBlue"
														Height="23px" Text="Edit" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" onclick="btnEdit_Click"></asp:button>
													<asp:button id="BtnSave" tabIndex="45" runat="server" Width="52px" BorderWidth="0px" BorderColor="MidnightBlue"
														Height="23px" Text="Save" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" onclick="BtnSave_Click"></asp:button>
													<asp:button id="cmdCancel" tabIndex="45" runat="server" Width="52px" BorderWidth="0px" BorderColor="MidnightBlue"
														Height="23px" Text="Cancel" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" onclick="cmdCancel_Click"></asp:button>
													<asp:button id="BtnExit" tabIndex="45" runat="server" BackColor="MidnightBlue" Width="52px"
														BorderColor="MidnightBlue" Height="23px" BorderWidth="0px" Font-Names="Tahoma" Text="Exit"
														ForeColor="White" onclick="BtnExit_Click"></asp:button>&nbsp;
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
										<TABLE id="Table6" style="WIDTH: 802px; HEIGHT: 84px" cellSpacing="0" cellPadding="0" width="802"
											border="0">
											<TR>
												<TD style="FONT-SIZE: 1pt" align="center">
													<TABLE id="Table4" style="WIDTH: 784px; HEIGHT: 10px" cellSpacing="0" cellPadding="0" width="784"
														border="0">
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 9px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 9px">
																<asp:label id="lblB" Width="107px" RUNAT="server" CSSCLASS="headfield1" 
                                                                    HEIGHT="19px" ForeColor="Black"
																	Font-Names="Tahoma" Font-Size="Smaller">Bank Description</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 9px">
																<asp:textbox id="txtBankDescription" tabIndex="1" RUNAT="server" 
                                                                    CSSCLASS="headfield1" HEIGHT="21px"
																	MAXLENGTH="60" WIDTH="257px" Font-Names="Tahoma" Font-Size="Smaller"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 9px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 10px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 10px"><asp:label id="lblAddress1" Width="105px" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma"
																	Font-Size="Smaller"> Address 1</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 10px">
																<asp:textbox id="txtAddress1" tabIndex="2" RUNAT="server" CSSCLASS="headfield1" HEIGHT="21px"
																	MAXLENGTH="60" WIDTH="257px" Font-Names="Tahoma" Font-Size="Smaller"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 10px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 1px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 1px"><asp:label id="lblAddress2" CSSCLASS="headfield1" RUNAT="server" Width="65px" Font-Names="Tahoma"
																	Font-Size="Smaller"> Address 2</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 1px">
																<asp:textbox id="txtAddress2" tabIndex="3" RUNAT="server" CSSCLASS="headfield1" HEIGHT="22px"
																	MAXLENGTH="60" WIDTH="257px" Font-Names="Tahoma" Font-Size="Smaller"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 1px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px"><asp:label id="lblCity" CSSCLASS="headfield1" RUNAT="server" Width="30px" Font-Names="Tahoma"
																	Font-Size="Smaller">City</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
																<asp:textbox id="txtCity" tabIndex="4" RUNAT="server" CSSCLASS="headfield1" 
                                                                    HEIGHT="22px" MAXLENGTH="50"
																	WIDTH="153px" Font-Names="Tahoma" Font-Size="Smaller"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
                                                                <asp:CheckBox ID="chkNetCollection" runat="server" CssClass="headfield1" Font-Names="Tahoma"
                                                                    Font-Size="Smaller" Text="Is Net Collection Method" Width="161px" 
                                                                    Visible="False" /></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px"><asp:label id="lblState" CSSCLASS="headfield1" RUNAT="server" Width="39px" Font-Names="Tahoma"
																	Font-Size="Smaller">State</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"><asp:textbox id="txtSt" tabIndex="5" CSSCLASS="headfield1" RUNAT="server" WIDTH="153px" HEIGHT="22px"
																	MAXLENGTH="2" Font-Names="Tahoma" Font-Size="Smaller"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<asp:label id="lblZipCode" Width="57px" Font-Size="Smaller" Font-Names="Tahoma" RUNAT="server"
																	CSSCLASS="headfield1">Zip Code</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
                                                                <asp:TextBox id="txtZipCode" tabIndex="6" RUNAT="server" 
                                                                    CSSCLASS="headfield1" HEIGHT="22px"
																	MAXLENGTH="10" WIDTH="153px" ISDATE="False" Font-Names="Tahoma" Font-Size="Smaller"></asp:TextBox>
                                                                    </TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<asp:label id="lblPhone" Width="44px" Font-Size="Smaller" Font-Names="Tahoma" RUNAT="server"
																	CSSCLASS="headfield1">Phone </asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
                                                            <asp:TextBox id="txtPhone" tabIndex="7" runat="server" Width="153px" Height="22px" CssClass="headfield1"
																	Columns="34" Font-Names="Tahoma" Font-Size="Smaller"></asp:TextBox>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
                                                                <asp:Label ID="lblRoutingNumber" runat="server" CssClass="headfield1" 
                                                                    Font-Names="Tahoma" Font-Size="Smaller" Width="97px">Routing Number</asp:Label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
                                                                <asp:TextBox ID="txtRoutingNumber" runat="server" CssClass="headfield1" Font-Names="Tahoma"
                                                                    Font-Size="Smaller" Height="22px" MaxLength="25" TabIndex="10"
                                                                    Width="254px" OnTextChanged="txtRoutingNumber_TextChanged"></asp:TextBox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<P id="P1" runat="server">
																	<asp:label id="Label1" Width="97px" Font-Size="Smaller" Font-Names="Tahoma" RUNAT="server"
																		CSSCLASS="headfield1" Visible="False">Universal Code:</asp:label></P>
															</TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
																<asp:textbox id="txtUniversal" tabIndex="3" RUNAT="server" CSSCLASS="headfield1" HEIGHT="22px"
																	MAXLENGTH="3" WIDTH="153px" Visible="False" Font-Names="Tahoma" Font-Size="Smaller"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
                                                        <tr>
                                                            <td style="width: 186px; height: 5px">
                                                                &nbsp;</td>
                                                            <td style="width: 162px; height: 5px">
                                                                <asp:Label ID="Label3" runat="server" CssClass="headfield1" Font-Names="Tahoma" 
                                                                    Font-Size="Smaller" Width="97px" Visible="False">VSC Bank Fee:</asp:Label></td>
                                                            <td style="width: 168px; height: 5px">
                                                                <asp:TextBox ID="txtVSCBankFee" runat="server" CssClass="headfield1" Font-Names="Tahoma"
                                                                    Font-Size="Smaller" Height="22px" MaxLength="3" TabIndex="10"
                                                                    Width="153px" Visible="False"></asp:TextBox></td>
                                                            <td style="width: 168px; height: 5px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 186px; height: 5px">
                                                                &nbsp;</td>
                                                            <td style="width: 162px; height: 5px">
																<asp:label id="lblEntryDate" Width="65px" Font-Size="Smaller" 
                                                                    Font-Names="Tahoma" RUNAT="server"
																	CSSCLASS="headfield1" Visible="False">Entry Date</asp:label></td>
                                                            <td style="width: 168px; height: 5px">
																<maskedinput:maskedtextbox id="txtEntryDate" tabIndex="8" RUNAT="server" 
                                                                    CSSCLASS="headfield1" HEIGHT="22px"
																	WIDTH="153px" ISDATE="True" Font-Names="Tahoma" Font-Size="Smaller" Enabled="False" Visible="False"></maskedinput:maskedtextbox></td>
                                                            <td style="width: 168px; height: 5px">
                                                                &nbsp;</td>
                                                        </tr>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 5pt; WIDTH: 112px; HEIGHT: 5px" align="left"></TD>
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
						<P>&nbsp;</P>
						<P>&nbsp;</P>
						<P>&nbsp;</P>
					</TD>
				</TR>
			</TABLE>
			<asp:checkbox id="chkLease" style="Z-INDEX: 122; LEFT: 1328px; POSITION: absolute; TOP: 148px"
				tabIndex="11" runat="server" Width="105px" Text="Lease" CssClass="headfield1" AutoPostBack="True"
				TextAlign="Left" Font-Names="Tahoma" Font-Size="Smaller" Visible="False"></asp:checkbox>
			<maskedinput:maskedtextheader id="MaskedTextHeader1" runat="server"></maskedinput:maskedtextheader>
			<asp:imagebutton id="btnAddNew1" style="Z-INDEX: 123; LEFT: 131px; POSITION: absolute; TOP: 627px"
				tabIndex="10" runat="server" Visible="False" ImageUrl="Images/addNew_btn.gif" CausesValidation="False"></asp:imagebutton>
			<asp:imagebutton id="btnEdit1" style="Z-INDEX: 124; LEFT: 163px; POSITION: absolute; TOP: 627px"
				tabIndex="11" runat="server" Visible="False" ImageUrl="Images/edit_btn.GIF" CausesValidation="False"></asp:imagebutton>
			<asp:imagebutton id="BtnSave1" style="Z-INDEX: 126; LEFT: 195px; POSITION: absolute; TOP: 627px"
				tabIndex="12" RUNAT="server" Visible="False" CAUSESVALIDATION="False" TOOLTIP="Save Person Information"
				IMAGEURL="Images/save_btn.gif"></asp:imagebutton>
			<asp:imagebutton id="btnSearch1" style="Z-INDEX: 127; LEFT: 227px; POSITION: absolute; TOP: 627px"
				tabIndex="13" runat="server" Visible="False" ImageUrl="Images/search_btn.gif" CausesValidation="False"></asp:imagebutton>
			<asp:imagebutton id="cmdCancel1" style="Z-INDEX: 128; LEFT: 259px; POSITION: absolute; TOP: 627px"
				tabIndex="14" runat="server" Visible="False" ImageUrl="Images/cancel_btn.gif" CausesValidation="False"></asp:imagebutton>
			<asp:imagebutton id="btnPrint1" style="Z-INDEX: 129; LEFT: 291px; POSITION: absolute; TOP: 627px"
				tabIndex="7" runat="server" Visible="False" ImageUrl="Images/printreport_btn.gif" AlternateText="Print Report"></asp:imagebutton>
			<asp:ImageButton id="btnAuditTrail1" style="Z-INDEX: 130; LEFT: 131px; POSITION: absolute; TOP: 659px"
				runat="server" Width="48px" Height="19px" Visible="False" ImageUrl="Images/History_btn.bmp"></asp:ImageButton>
			<asp:imagebutton id="BtnExit1" style="Z-INDEX: 131; LEFT: 187px; POSITION: absolute; TOP: 659px"
				tabIndex="15" RUNAT="server" Visible="False" CAUSESVALIDATION="False" IMAGEURL="Images/exit_btn.gif"></asp:imagebutton>
			<asp:button id="BtnEnd" style="Z-INDEX: 132; LEFT: 387px; POSITION: absolute; TOP: 635px" tabIndex="19"
				runat="server" Height="23px" Text=">>" Visible="False" onclick="BtnEnd_Click"></asp:button>
			<asp:button id="BtnNext" style="Z-INDEX: 133; LEFT: 419px; POSITION: absolute; TOP: 635px" tabIndex="18"
				runat="server" Width="24px" Height="23px" Text=">" Visible="False" onclick="BtnNext_Click"></asp:button>
			<asp:button id="BtnPrevious" style="Z-INDEX: 134; LEFT: 443px; POSITION: absolute; TOP: 635px"
				tabIndex="17" runat="server" Width="24px" Height="23px" Text="<" Visible="False" onclick="BtnPrevious_Click"></asp:button>
			<asp:button id="BtnBegin" style="Z-INDEX: 135; LEFT: 275px; POSITION: absolute; TOP: 667px"
				tabIndex="16" runat="server" Height="23px" Text="<<" Visible="False" onclick="BtnBegin_Click"></asp:button>
			<asp:Label id="lblRecordCount" style="Z-INDEX: 136; LEFT: 219px; POSITION: absolute; TOP: 667px"
				runat="server" Width="48px" Height="16px" Visible="False">Label</asp:Label>
			<asp:literal id="litPopUp" RUNAT="server" VISIBLE="False"></asp:literal>
		</form>
	</body>
</HTML>
