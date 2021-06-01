<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Page language="c#" Inherits="EPolicy.Agency" CodeFile="Agency.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ePMS | electronic Policy Manager Solution</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
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
			<P>
				<table id="Table8" style="Z-INDEX: 127; LEFT: 4px; WIDTH: 911px; POSITION: static; TOP: 4px; HEIGHT: 281px"
					cellSpacing="0" cellPadding="0" width="911" align="center" bgColor="white" dataPageSize="25"
					border="0">
					<tr>
						<td style="WIDTH: 764px; HEIGHT: 1px" colSpan="3">
							<P>
								<asp:placeholder id="Placeholder1" runat="server"></asp:placeholder></P>
						</td>
					</tr>
					<tr>
						<td style="FONT-SIZE: 10pt; WIDTH: 5px; POSITION: static; HEIGHT: 395px" vAlign="top"
							align="right" colSpan="1" rowSpan="5">
							<P align="center">
								<asp:placeholder id="phTopBanner1" runat="server"></asp:placeholder></P>
						</td>
						<td style="FONT-SIZE: 0pt; WIDTH: 86px; HEIGHT: 184px" align="center">
							<P align="center">
								<table id="Table9" style="WIDTH: 809px; HEIGHT: 99px" cellSpacing="0" cellPadding="0" width="809"
									bgColor="white" border="0">
									<tr>
										<td style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="center"></td>
									</tr>
									<tr>
										<td style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											<table id="Table10" style="WIDTH: 808px; HEIGHT: 12px" cellSpacing="0" cellPadding="0"
												width="808" border="0">
												<tr>
													<td style="WIDTH: 272px" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
														<asp:Label id="Label1" runat="server" Font-Names="tahoma,11pt" Font-Size="11pt" CssClass="headForm1 "
															ForeColor="Navy" Font-Bold="True">Agency ID:</asp:Label>
														<asp:label id="lblAgencyID" runat="server" Font-Names="Tahoma" Font-Size="Smaller" Width="91px"></asp:label></td>
													<td align="right">
														<asp:button id="btnAuditTrail" tabIndex="44" runat="server" Font-Names="Tahoma" ForeColor="White"
															Width="59px" BackColor="MidnightBlue" Text="AuditTrail" BorderWidth="0px" BorderColor="MidnightBlue"
															Height="23px" Font-Size="9pt" onclick="btnAuditTrail_Click" CssClass="ButtonText-14" Visible="False"></asp:button>
														<asp:button id="btnCommission" tabIndex="45" runat="server" Font-Names="Tahoma" ForeColor="White"
															Width="86px" BackColor="MidnightBlue" Text="Commission" BorderWidth="0px" BorderColor="MidnightBlue"
															Height="23px" onclick="btnCommission_Click"></asp:button>
														<asp:button id="btnPrint" tabIndex="40" runat="server" Font-Names="Tahoma" ForeColor="White"
															Width="52px" BackColor="MidnightBlue" Text="Print" BorderWidth="0px" BorderColor="MidnightBlue"
															Height="23px" Font-Size="9pt" onclick="btnPrint_Click" Visible="False"></asp:button>
														<asp:button id="btnSearch" tabIndex="45" runat="server" Font-Names="Tahoma" ForeColor="White"
															Width="52px" BackColor="MidnightBlue" Text="Search" BorderWidth="0px" BorderColor="MidnightBlue"
															Height="23px" onclick="btnSearch_Click"></asp:button>
														<asp:button id="btnAddNew" tabIndex="45" runat="server" Font-Names="Tahoma" ForeColor="White"
															Width="52px" BackColor="MidnightBlue" Text="Add" BorderWidth="0px" BorderColor="MidnightBlue"
															Height="23px" onclick="btnAddNew_Click"></asp:button>
														<asp:button id="btnEdit" tabIndex="41" runat="server" Font-Names="Tahoma" ForeColor="White"
															Width="52px" BackColor="MidnightBlue" Text="Edit" BorderWidth="0px" BorderColor="MidnightBlue"
															Height="23px" Font-Size="9pt" onclick="btnEdit_Click"></asp:button>
														<asp:button id="BtnSave" tabIndex="43" runat="server" Font-Names="Tahoma" ForeColor="White"
															Width="52px" BackColor="MidnightBlue" Text="Save" BorderWidth="0px" BorderColor="MidnightBlue"
															Height="23px" Font-Size="9pt" onclick="BtnSave_Click"></asp:button>
														<asp:button id="cmdCancel" tabIndex="42" runat="server" Font-Names="Tahoma" ForeColor="White"
															Width="52px" BackColor="MidnightBlue" Text="Cancel" BorderWidth="0px" BorderColor="MidnightBlue"
															Height="23px" Font-Size="9pt" onclick="cmdCancel_Click"></asp:button>
														<asp:button id="Button2" tabIndex="45" runat="server" Font-Names="Tahoma" ForeColor="White"
															Width="52px" BackColor="MidnightBlue" Text="Exit" BorderWidth="0px" BorderColor="MidnightBlue"
															Height="23px" onclick="Button2_Click"></asp:button>&nbsp;
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td style="FONT-SIZE: 0pt; HEIGHT: 5px">
											<table id="Table11" style="WIDTH: 808px" borderColor="#4b0082" height="1" cellSpacing="0"
												borderColorDark="#4b0082" cellPadding="0" width="808" bgColor="indigo" borderColorLight="#4b0082"
												border="0">
												<tr>
													<td></td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td style="FONT-SIZE: 5pt; WIDTH: 112px; HEIGHT: 192px" vAlign="middle" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											<table id="Table6" style="WIDTH: 802px; HEIGHT: 84px" cellSpacing="0" cellPadding="0" width="802"
												border="0">
												<tr>
													<td style="FONT-SIZE: 1pt" align="center">
														<table id="Table4" style="WIDTH: 784px; HEIGHT: 10px" cellSpacing="0" cellPadding="0" width="784"
															border="0">
															<tr>
																<td style="WIDTH: 186px; HEIGHT: 1px"></td>
																<td style="WIDTH: 162px; HEIGHT: 1px"><asp:label id="lblB" RUNAT="server" CSSCLASS="HeadField1" HEIGHT="16px" ForeColor="Black" Font-Names="Tahoma"
																		Font-Size="9pt">Agency Description</asp:label></td>
																<td style="WIDTH: 168px; HEIGHT: 1px">
																	<asp:textbox id="txtAgencyDescription" tabIndex="1" Font-Names="Tahoma" Font-Size="9pt" RUNAT="server"
																		MAXLENGTH="30" CSSCLASS="headfield1" WIDTH="249px" HEIGHT="22px"></asp:textbox></td>
																<td style="WIDTH: 168px; HEIGHT: 1px"></td>
															</tr>
															<tr>
																<td style="WIDTH: 186px; HEIGHT: 10px"></td>
																<td style="WIDTH: 162px; HEIGHT: 10px">
																	<asp:label id="lblAddress1" Font-Names="Tahoma" Font-Size="9pt" RUNAT="server" CSSCLASS="HeadField1"
																		Height="16" Width="99px"> Address 1</asp:label></td>
																<td style="WIDTH: 168px; HEIGHT: 10px">
																	<asp:textbox id="txtAddress1" tabIndex="2" Font-Names="Tahoma" Font-Size="9pt" RUNAT="server"
																		MAXLENGTH="60" CSSCLASS="headfield1" WIDTH="249" HEIGHT="22px"></asp:textbox></td>
																<td style="WIDTH: 168px; HEIGHT: 10px"></td>
															</tr>
															<tr>
																<td style="WIDTH: 186px; HEIGHT: 1px"></td>
																<td style="WIDTH: 162px; HEIGHT: 1px">
																	<asp:label id="lblAddress2" Font-Names="Tahoma" Font-Size="9pt" RUNAT="server" CSSCLASS="HeadField1"
																		Height="16"> Address 2</asp:label></td>
																<td style="WIDTH: 168px; HEIGHT: 1px">
																	<asp:textbox id="txtAddress2" tabIndex="3" Font-Names="Tahoma" Font-Size="9pt" RUNAT="server"
																		MAXLENGTH="60" CSSCLASS="headfield1" WIDTH="249" HEIGHT="22px"></asp:textbox></td>
																<td style="WIDTH: 168px; HEIGHT: 1px"></td>
															</tr>
															<tr>
																<td style="WIDTH: 186px; HEIGHT: 5px"></td>
																<td style="WIDTH: 162px; HEIGHT: 5px">
																	<asp:label id="lblCity" Font-Names="Tahoma" Font-Size="9pt" RUNAT="server" CSSCLASS="HeadField1"
																		Height="16">City</asp:label></td>
																<td style="WIDTH: 168px; HEIGHT: 5px">
																	<asp:textbox id="txtCity" tabIndex="4" Font-Names="Tahoma" Font-Size="9pt" RUNAT="server" MAXLENGTH="10"
																		CSSCLASS="headfield1" WIDTH="144px" HEIGHT="22px"></asp:textbox></td>
																<td style="WIDTH: 168px; HEIGHT: 5px"></td>
															</tr>
															<tr>
																<td style="WIDTH: 186px; HEIGHT: 5px"></td>
																<td style="WIDTH: 162px; HEIGHT: 5px">
																	<asp:label id="lblState" Font-Names="Tahoma" Font-Size="9pt" RUNAT="server" CSSCLASS="HeadField1"
																		Height="16">State</asp:label></td>
																<td style="WIDTH: 168px; HEIGHT: 5px">
																	<asp:textbox id="txtSt" tabIndex="5" Font-Names="Tahoma" Font-Size="9pt" RUNAT="server" MAXLENGTH="2"
																		CSSCLASS="headfield1" WIDTH="47px" HEIGHT="22px"></asp:textbox></td>
																<td style="WIDTH: 168px; HEIGHT: 5px"></td>
															</tr>
															<tr>
																<td style="WIDTH: 186px; HEIGHT: 5px"></td>
																<td style="WIDTH: 162px; HEIGHT: 5px">
																	<asp:label id="lblZipCode" Font-Names="Tahoma" Font-Size="9pt" RUNAT="server" CSSCLASS="HeadField1"
																		Height="16">Zip Code</asp:label></td>
																<td style="WIDTH: 168px; HEIGHT: 5px">
                                                                    <asp:TextBox id="txtZipCode" tabIndex="6" Font-Names="Tahoma" 
                                                                        Font-Size="9pt" RUNAT="server"
																		MAXLENGTH="10" CSSCLASS="headfield1" WIDTH="144px" HEIGHT="22px"></asp:TextBox>
                                                                </td>
																<td style="WIDTH: 168px; HEIGHT: 5px"></td>
															</tr>
															<tr>
																<td style="WIDTH: 186px; HEIGHT: 5px"></td>
																<td style="WIDTH: 162px; HEIGHT: 5px">
																	<asp:label id="lblPhone" Font-Names="Tahoma" Font-Size="9pt" RUNAT="server" CSSCLASS="HeadField1"
																		Height="16">Phone </asp:label></td>
																<td style="WIDTH: 168px; HEIGHT: 5px">
                                                                       <asp:TextBox id="txtPhone" tabIndex="7" runat="server" Font-Names="Tahoma" Font-Size="9pt" Columns="34"
																		Height="22px" Width="144" CssClass="headfield1"></asp:TextBox> 
                                                                </td>
																<td style="WIDTH: 168px; HEIGHT: 5px"></td>
															</tr>
															<tr>
																<td style="WIDTH: 186px; HEIGHT: 5px"></td>
																<td style="WIDTH: 162px; HEIGHT: 5px">
																	<P id="P1" runat="server">
																		<asp:label id="lblEntryDate" RUNAT="server" CSSCLASS="HeadField1" 
                                                                            Font-Names="Tahoma" Font-Size="9pt"
																			Height="16px" Visible="False">Entry Date</asp:label></P>
																</td>
																<td style="WIDTH: 168px; HEIGHT: 5px">
																	<maskedinput:maskedtextbox id="txtEntryDate" tabIndex="8" Font-Names="Tahoma" 
                                                                        Font-Size="9pt" RUNAT="server"
																		CSSCLASS="headfield1" WIDTH="144px" HEIGHT="22px" ISDATE="True" Enabled="False" Visible="False"></maskedinput:maskedtextbox></td>
																<td style="WIDTH: 168px; HEIGHT: 5px"></td>
															</tr>
														</table>
													</td>
												</tr>
											</table>
										</td>
									</tr>
									<tr>
										<td style="FONT-SIZE: 5pt; WIDTH: 112px; HEIGHT: 5px" align="left"></td>
									</tr>
									<tr>
										<td style="FONT-SIZE: 0pt; WIDTH: 35px; HEIGHT: 27px"></td>
									</tr>
								</table>
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
							<P>&nbsp;</P>
							<P>&nbsp;</P>
						</td>
					</tr>
				</table>
				<maskedinput:maskedtextheader id="MaskedTextHeader1" runat="server"></maskedinput:maskedtextheader>
				<asp:literal id="litPopUp" RUNAT="server" VISIBLE="False"></asp:literal></P>
			&nbsp;
			<P>&nbsp;</P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
		</form>
	</body>
</HTML>
