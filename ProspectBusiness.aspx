<%@ Page language="c#" Inherits="EPolicy.ProspectBusiness" CodeFile="ProspectBusiness.aspx.cs" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ePMS | electronic Policy Manager Solution</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="epolicy.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" background="Images2/SQUARE.GIF"
		topMargin="19" rightMargin="0">
		<form method="post" runat="server">
			<TABLE id="Table8" style="Z-INDEX: 153; LEFT: 4px; WIDTH: 1065px; POSITION: static; TOP: 4px; HEIGHT: 580px"
				cellSpacing="0" cellPadding="0" width="1065" align="center" bgColor="white" dataPageSize="25"
				border="0">
				<TR>
					<TD style="WIDTH: 764px; HEIGHT: 1px" colSpan="3">
						<P>
							<asp:placeholder id="Placeholder1" runat="server"></asp:placeholder></P>
					</TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 5px; POSITION: static; HEIGHT: 395px" align="right"
						colSpan="1" rowSpan="5" vAlign="top">
						<P align="center">
							<asp:placeholder id="phTopBanner1" runat="server"></asp:placeholder></P>
					</TD>
					<TD style="FONT-SIZE: 0pt; WIDTH: 86px; HEIGHT: 184px" align="center">
						<P align="center">
							<TABLE id="Table9" style="WIDTH: 726px; HEIGHT: 540px" cellSpacing="0" cellPadding="0"
								width="726" bgColor="white" border="0">
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="center"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table10" style="WIDTH: 808px; HEIGHT: 12px" cellSpacing="0" cellPadding="0"
											width="808" border="0">
											<TR>
												<TD align="left">&nbsp;
													<asp:Label id="Label17" runat="server" Font-Names="Tahoma" ForeColor="Navy" Height="16px" Width="77px"
														CssClass="headForm1 " Font-Bold="True" Font-Size="11pt">Prospect: </asp:Label>
													<asp:label id="lblProspectNo" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headfield1" WIDTH="121px"
														Font-Size="9pt"></asp:label>
													<asp:Label id="Label5" runat="server" Font-Names="Tahoma" ForeColor="Black" Height="10px" Width="33px"
														CssClass="headForm1 " Font-Size="9pt">Client: </asp:Label>&nbsp;
													<asp:label id="lblParentCustomer" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headfield1"
														WIDTH="96px" Font-Size="9pt"></asp:label></TD>
												<TD></TD>
												<TD align="right">
													<asp:Button id="btnNew" runat="server" Width="70px" Height="23px" ForeColor="White" Font-Names="Tahoma"
														BorderWidth="0px" BorderColor="MidnightBlue" BackColor="MidnightBlue" Text="Add" onclick="btnNew_Click"></asp:Button>
													<asp:Button id="cmdConvertToCustomer" runat="server" Font-Names="Tahoma" ForeColor="White" Height="23px"
														Text="ChangeToCustomer" Width="127px" BackColor="MidnightBlue" BorderColor="MidnightBlue"
														BorderWidth="0px" onclick="cmdConvertToCustomer_Click"></asp:Button>
													<asp:Button id="BtnSave" runat="server" Font-Names="Tahoma" ForeColor="White" Height="23px"
														Text="Save" BackColor="MidnightBlue" BorderColor="MidnightBlue" BorderWidth="0px" Width="71px" onclick="BtnSave_Click"></asp:Button>
													<asp:Button id="cmdCancel" runat="server" Font-Names="Tahoma" ForeColor="White" Height="23px"
														Text="Cancel" BackColor="MidnightBlue" BorderColor="MidnightBlue" BorderWidth="0px" Width="71px" onclick="cmdCancel_Click"></asp:Button>
													<asp:Button id="btnEdit" runat="server" Font-Names="Tahoma" ForeColor="White" Height="23px"
														Text="Edit" Width="71px" BackColor="MidnightBlue" BorderColor="MidnightBlue" BorderWidth="0px" onclick="btnEdit_Click"></asp:Button>
													<asp:Button id="btnAuditTrail" runat="server" Font-Names="Tahoma" ForeColor="White" Height="23px"
														Text="History" Width="82px" BackColor="MidnightBlue" BorderColor="MidnightBlue" BorderWidth="0px" onclick="btnAuditTrail_Click"></asp:Button>
													<asp:Button id="BtnExit" runat="server" Font-Names="Tahoma" ForeColor="White" Height="23px"
														Text="Exit" Width="71px" BackColor="MidnightBlue" BorderColor="MidnightBlue" BorderWidth="0px" onclick="BtnExit_Click"></asp:Button>&nbsp;
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
									<TD style="FONT-SIZE: 5pt; WIDTH: 112px; HEIGHT: 128px" vAlign="middle" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table6" style="WIDTH: 802px; HEIGHT: 194px" cellSpacing="0" cellPadding="0"
											width="802" border="0">
											<TR>
												<TD style="FONT-SIZE: 1pt" align="center">
													<TABLE id="tblContactInfo" style="WIDTH: 669px; HEIGHT: 169px" cellSpacing="0" cellPadding="0">
														<TR>
															<TD style="WIDTH: 165px; HEIGHT: 11px">
																<asp:label id="lblBusinessName" Width="86px" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headfield1"
																	Font-Size="9pt">Business Name</asp:label></TD>
															<TD style="WIDTH: 294px; HEIGHT: 11px" colSpan="3">
																<asp:textbox id="txtBusinessName" tabIndex="1" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headfield1"
																	WIDTH="206px" HEIGHT="19px" MAXLENGTH="20" Font-Size="9pt"></asp:textbox></TD>
															<TD style="WIDTH: 32px; HEIGHT: 11px">
																<asp:label id="lblReferredBy" Width="66px" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headfield1"
																	HEIGHT="21" ENABLEVIEWSTATE="False" Font-Size="9pt">Referred By</asp:label></TD>
															<TD style="WIDTH: 162px; HEIGHT: 11px">
																<asp:dropdownlist id="ddlReferredBy" tabIndex="3" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headfield1"
																	WIDTH="138" AUTOPOSTBACK="True" HEIGHT="19px" Font-Size="9pt"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 165px; HEIGHT: 11px">
																<asp:label id="lblPhone" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headfield1" ForeColor="Black"
																	HEIGHT="19px" Font-Size="9pt">Phone</asp:label></TD>
															<TD style="WIDTH: 294px; HEIGHT: 11px" colSpan="3">
																<maskedinput:maskedtextbox id="txtPhone" tabIndex="2" Font-Names="Tahoma" RUNAT="server" CSSCLASS="HeadField1"
																	WIDTH="110px" HEIGHT="19px" MAXLENGTH="20" MASK="(999) 999-9999" Font-Size="9pt"></maskedinput:maskedtextbox></TD>
															<TD style="WIDTH: 32px; HEIGHT: 11px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 11px">
																<asp:textbox id="txtReferredByName" tabIndex="4" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headfield1"
																	WIDTH="138" HEIGHT="19px" MAXLENGTH="20" VISIBLE="False" Font-Size="9pt"></asp:textbox></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 165px; HEIGHT: 11px"></TD>
															<TD style="WIDTH: 294px; HEIGHT: 11px" colSpan="3"></TD>
															<TD style="WIDTH: 32px; HEIGHT: 11px">
																<asp:label id="Label2" Width="74px" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headfield1"
																	HEIGHT="21" ENABLEVIEWSTATE="False" Font-Size="9pt">Originated At</asp:label></TD>
															<TD style="WIDTH: 162px; HEIGHT: 11px">
																<asp:dropdownlist id="ddlLocation" tabIndex="5" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headfield1"
																	WIDTH="158px" AUTOPOSTBACK="True" HEIGHT="19px" Font-Size="9pt"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 165px; HEIGHT: 11px">
																<asp:label id="Label9" RUNAT="server" CSSCLASS="headform3" ForeColor="Black" WIDTH="136px"
																	Font-Size="9pt" Font-Names="Tahoma">Contact Information</asp:label></TD>
															<TD style="WIDTH: 294px; HEIGHT: 11px" colSpan="3"></TD>
															<TD style="WIDTH: 32px; HEIGHT: 11px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 11px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 165px; HEIGHT: 11px">
																<asp:label id="lblFirstname" Width="89px" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headfield1"
																	HEIGHT="19px" Font-Size="9pt">First Name</asp:label></TD>
															<TD style="WIDTH: 294px; HEIGHT: 11px" colSpan="3">
																<asp:textbox id="txtFirstName" tabIndex="6" Font-Names="Tahoma" RUNAT="server" CSSCLASS="HeadField1"
																	WIDTH="201" HEIGHT="19px" MAXLENGTH="15" Font-Size="9pt"></asp:textbox></TD>
															<TD style="WIDTH: 32px; HEIGHT: 11px">&nbsp;
																<asp:label id="lblEmail" Width="66px" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headfield1"
																	HEIGHT="19px" Font-Size="9pt">E-Mail</asp:label></TD>
															<TD style="WIDTH: 162px; HEIGHT: 11px">
																<asp:textbox id="txtEmail" tabIndex="8" Font-Names="Tahoma" RUNAT="server" CSSCLASS="HeadField1"
																	WIDTH="165px" HEIGHT="19px" MAXLENGTH="50" Font-Size="9pt"></asp:textbox></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 165px; HEIGHT: 26px">
																<asp:label id="lblLastName" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headfield1" HEIGHT="19px"
																	Font-Size="9pt">Last Name</asp:label></TD>
															<TD style="WIDTH: 294px; HEIGHT: 26px" colSpan="3">
																<asp:textbox id="txtLastName1" tabIndex="7" Font-Names="Tahoma" RUNAT="server" CSSCLASS="HeadField1"
																	WIDTH="201px" HEIGHT="19px" MAXLENGTH="15" Font-Size="9pt"></asp:textbox></TD>
															<TD style="WIDTH: 32px; HEIGHT: 26px"></TD>
															<TD style="WIDTH: 134px; HEIGHT: 26px"></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 10pt; WIDTH: 112px; HEIGHT: 2px" align="left">&nbsp;&nbsp;&nbsp;
										<asp:label id="LblTotalCases" Font-Names="Tahoma" RUNAT="server" CSSCLASS="headform3" Height="9px"
											ForeColor="Black" WIDTH="131px" Font-Size="9pt">Total Cases:</asp:label></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 5pt; WIDTH: 112px; HEIGHT: 6px" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table2" style="WIDTH: 808px" borderColor="#4b0082" height="1" cellSpacing="0"
											borderColorDark="#4b0082" cellPadding="0" width="808" bgColor="indigo" borderColorLight="#4b0082"
											border="0">
											<TR>
												<TD></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 5pt; WIDTH: 112px; HEIGHT: 12px" align="left">
										<asp:Label id="LblError" runat="server" Font-Names="Tahoma" ForeColor="IndianRed" Width="765px"
											Font-Size="13pt" Visible="False">Label</asp:Label></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 35px; HEIGHT: 27px">
										<asp:datagrid id="searchIndividual" RUNAT="server" Height="204px" WIDTH="795px" PageSize="7" BORDERSTYLE="Solid"
											BORDERWIDTH="1px" BORDERCOLOR="#D6E3EA" ITEMSTYLE-HORIZONTALALIGN="center" HEADERSTYLE-HORIZONTALALIGN="Center"
											BACKCOLOR="White" AUTOGENERATECOLUMNS="False" ALLOWPAGING="True" FONT-SIZE="Smaller" FONT-NAMES="Arial"
											CELLPADDING="0" ALTERNATINGITEMSTYLE-CSSCLASS="HeadForm3" ALTERNATINGITEMSTYLE-BACKCOLOR="#FEFBF6"
											HEADERSTYLE-CSSCLASS="HeadForm2" HEADERSTYLE-BACKCOLOR="#5C8BAE" ITEMSTYLE-CSSCLASS="HeadForm3">
											<FooterStyle ForeColor="#003399" BackColor="Navy"></FooterStyle>
											<SelectedItemStyle HorizontalAlign="Center" BackColor="White"></SelectedItemStyle>
											<EditItemStyle HorizontalAlign="Center" BackColor="White"></EditItemStyle>
											<AlternatingItemStyle HorizontalAlign="Center" CssClass="HeadForm3" BackColor="White"></AlternatingItemStyle>
											<ItemStyle HorizontalAlign="Center" CssClass="HeadForm3" BackColor="White"></ItemStyle>
											<HeaderStyle Font-Names="tahoma" HorizontalAlign="Center" Height="10px" CssClass="HeadForm2"
												BackColor="Navy"></HeaderStyle>
											<Columns>
												<asp:ButtonColumn ButtonType="PushButton" HeaderText="Sel." CommandName="Select">
													<HeaderStyle Width="1px"></HeaderStyle>
													<ItemStyle Font-Names="tahoma"></ItemStyle>
												</asp:ButtonColumn>
												<asp:BoundColumn DataField="TaskControlID" HeaderText="Control No">
													<ItemStyle Font-Names="tahoma" HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TaskControlTypeDesc" HeaderText="Task Type">
													<ItemStyle Font-Names="tahoma" HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="EntryDate" HeaderText="Entry Date" DataFormatString="{0:d}">
													<ItemStyle Font-Names="tahoma" HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="TaskStatusDesc" HeaderText="Status">
													<ItemStyle Font-Names="tahoma" HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle Font-Names="tahoma" HorizontalAlign="Left" ForeColor="Blue" BackColor="White" PageButtonCount="20"
												CssClass="Numbers" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
							</TABLE>
						</P>
						<P>&nbsp;</P>
					</TD>
				</TR>
			</TABLE>
			<maskedinput:maskedtextheader id="MaskedTextHeader1" RUNAT="server"></maskedinput:maskedtextheader>
			<asp:literal id="litPopUp" RUNAT="server" VISIBLE="False"></asp:literal></form>
	</body>
</HTML>
