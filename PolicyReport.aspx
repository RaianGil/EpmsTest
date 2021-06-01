<%@ Page language="c#" Inherits="EPolicy.PolicyReport" CodeFile="PolicyReport.aspx.cs" %>
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
	</HEAD>
	<body bottomMargin="0" leftMargin="0" background="Images2/SQUARE.GIF"
		topMargin="19" rightMargin="0">
		<form method="post" runat="server">
			<TABLE id="Table8" style="Z-INDEX: 122; LEFT: 4px; WIDTH: 914px; POSITION: static; TOP: 4px; HEIGHT: 340px"
				cellSpacing="0" cellPadding="0" width="914" align="center" bgColor="white" dataPageSize="25"
				border="0">
				<TR>
					<TD style="WIDTH: 764px; HEIGHT: 1px" colSpan="3">
						<P>
							<asp:placeholder id="Placeholder1" runat="server"></asp:placeholder></P>
					</TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 10pt; WIDTH: 5px; POSITION: static; HEIGHT: 395px" align="right"
						colSpan="1" rowSpan="5">
						<P align="center">
							<asp:placeholder id="phTopBanner1" runat="server"></asp:placeholder></P>
					</TD>
					<TD style="FONT-SIZE: 0pt; WIDTH: 86px; HEIGHT: 184px" align="center">
						<P align="center">
							<TABLE id="Table9" style="WIDTH: 811px; HEIGHT: 336px" cellSpacing="0" cellPadding="0"
								width="811" bgColor="white" border="0">
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="center"></TD>
									<TD style="FONT-SIZE: 0pt; HEIGHT: 3px" align="right" colSpan="3"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 1px" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table10" style="WIDTH: 808px; HEIGHT: 12px" cellSpacing="0" cellPadding="0"
											width="808" border="0">
											<TR>
												<TD align="left">&nbsp;
													<asp:Label id="Label8" runat="server" Height="16px" Width="198px" CssClass="headForm1 " ForeColor="Navy"
														Font-Names="Tahoma" Font-Size="11pt" Font-Bold="True">Auto Policy  Reports</asp:Label></TD>
												<TD></TD>
												<TD align="right">
													<asp:button id="Button2" tabIndex="44" runat="server" Height="23px" Width="64px" Text="Print"
														ForeColor="White" BorderWidth="0px" BorderColor="MidnightBlue" Font-Names="Tahoma" Font-Size="9pt"
														BackColor="MidnightBlue" onclick="Button2_Click"></asp:button>
													<asp:button id="BtnExit" tabIndex="45" runat="server" Height="23px" Width="61px" Text="Exit"
														ForeColor="White" BorderWidth="0px" BorderColor="MidnightBlue" Font-Names="Tahoma" BackColor="MidnightBlue" onclick="BtnExit_Click"></asp:button>&nbsp;
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
									<TD style="WIDTH: 112px; HEIGHT: 138px" vAlign="middle" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table1" style="WIDTH: 806px; HEIGHT: 32px" cellSpacing="0" cellPadding="0" width="806"
											border="0">
											<TR>
												<TD style="FONT-SIZE: 1pt" align="center">
													<asp:radiobuttonlist id="rblPolicyReports" style="Design_Time_Lock: False" tabIndex="1" runat="server"
														RepeatLayout="Flow" AutoPostBack="True" Design_Time_Lock="False" Width="201px" Height="81px" Font-Names="Tahoma"
														Font-Size="9pt" onselectedindexchanged="rblPolicyReports_SelectedIndexChanged">
														<asp:ListItem Value="0" Selected="True">Auto Policy Report</asp:ListItem>
														<asp:ListItem Value="1">Payment Certification Report</asp:ListItem>
													</asp:radiobuttonlist></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 10pt; WIDTH: 112px; HEIGHT: 3px" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:label id="LblTotalCases" Height="9px" RUNAT="server" CSSCLASS="headform3" WIDTH="141px"
											Font-Names="Tahoma" Font-Size="9pt">Report Filter</asp:label></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 35px; HEIGHT: 3px">
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
									<TD style="FONT-SIZE: 0pt; WIDTH: 35px; HEIGHT: 27px" align="right">
										<TABLE id="Table4" style="WIDTH: 806px; HEIGHT: 32px" cellSpacing="0" cellPadding="0" width="806"
											border="0">
											<TR>
												<TD style="FONT-SIZE: 1pt" align="center">
													<TABLE id="Table5" style="WIDTH: 355px; HEIGHT: 99px" cellSpacing="0" cellPadding="0" width="355"
														border="0">
														<TR>
															<TD style="WIDTH: 87px"></TD>
															<TD>
																<asp:checkbox id="ChkSummary" runat="server" CssClass="headform3" Text="Summary" Font-Names="Tahoma"
																	Font-Size="9pt"></asp:checkbox></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 87px">
																<asp:label id="LblBeginningDate1" RUNAT="server" HEIGHT="17px" CSSCLASS="headfield1" WIDTH="88px"
																	Font-Names="Tahoma" Font-Size="9pt">Beginning Date:</asp:label></TD>
															<TD>
																<maskedinput:maskedtextbox id="txtBegDate1" tabIndex="1" RUNAT="server" ISDATE="True" HEIGHT="19px" CSSCLASS="headfield1"
																	WIDTH="134" Font-Names="Tahoma" Font-Size="9pt"></maskedinput:maskedtextbox><INPUT id="btnCalendar3" style="WIDTH: 21px; HEIGHT: 21px" onclick="javascript:calendar_window=window.open('selectDate.aspx?formname=PolicyReport.txtBegDate1','calendar_window','width=250,height=250');calendar_window.focus()"
																	tabIndex="2" type="button" value="..." name="btnCalendar" RUNAT="server"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 87px; HEIGHT: 20px">
																<asp:label id="LblEndingDate1" RUNAT="server" HEIGHT="17px" CSSCLASS="headfield1" WIDTH="71px"
																	Font-Names="Tahoma" Font-Size="9pt">Ending Date:</asp:label></TD>
															<TD style="HEIGHT: 20px">
																<maskedinput:maskedtextbox id="TxtEndDate1" tabIndex="3" RUNAT="server" WIDTH="134px" CSSCLASS="headfield1"
																	HEIGHT="19px" ISDATE="True" Font-Names="Tahoma" Font-Size="9pt"></maskedinput:maskedtextbox><INPUT id="btnCalendar4" style="WIDTH: 21px; HEIGHT: 21px" onclick="javascript:calendar_window=window.open('selectDate.aspx?formname=PolicyReport.TxtEndDate1','calendar_window','width=250,height=250');calendar_window.focus()"
																	tabIndex="4" type="button" value="..." name="btnCalendar" RUNAT="server"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 87px; HEIGHT: 20px">
																<asp:label id="lblCompanyDealer" Width="94px" RUNAT="server" HEIGHT="19" CSSCLASS="headfield1"
																	ForeColor="Black" Font-Names="Tahoma" Font-Size="9pt">Company Dealer:</asp:label></TD>
															<TD style="HEIGHT: 20px">
																<asp:dropdownlist id="ddlDealer" tabIndex="7" RUNAT="server" HEIGHT="19px" CSSCLASS="headfield1" WIDTH="213"
																	Font-Names="Tahoma" Font-Size="9pt"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 87px; HEIGHT: 20px">
																<asp:label id="lblInsurance" RUNAT="server" HEIGHT="17" CSSCLASS="headfield1" WIDTH="91px"
																	Font-Names="Tahoma" Font-Size="9pt">Insurance Co.:</asp:label></TD>
															<TD style="HEIGHT: 20px">
																<asp:dropdownlist id="ddlInsuranceCompany" tabIndex="3" RUNAT="server" HEIGHT="19px" CSSCLASS="headfield1"
																	WIDTH="213px" Font-Names="Tahoma" Font-Size="9pt">
																	<asp:ListItem></asp:ListItem>
																	<asp:ListItem Value="E">Entry Date</asp:ListItem>
																	<asp:ListItem Value="C">Close Date</asp:ListItem>
																</asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 87px; HEIGHT: 20px">
																<asp:label id="LblBeginningDate" RUNAT="server" HEIGHT="17px" CSSCLASS="headfield1" WIDTH="88px"
																	Font-Names="Tahoma" Font-Size="9pt">Beginning Date:</asp:label></TD>
															<TD style="HEIGHT: 20px">
																<maskedinput:maskedtextbox id="txtBegDate" tabIndex="1" RUNAT="server" ISDATE="True" HEIGHT="19px" CSSCLASS="headfield1"
																	WIDTH="134" Font-Names="Tahoma" Font-Size="9pt"></maskedinput:maskedtextbox><INPUT id="btnCalendar" style="WIDTH: 21px; HEIGHT: 21px" onclick="javascript:calendar_window=window.open('selectDate.aspx?formname=PolicyReport.txtBegDate','calendar_window','width=250,height=250');calendar_window.focus()"
																	tabIndex="2" type="button" value="..." name="btnCalendar" RUNAT="server"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 87px">
																<asp:label id="LblEndingDate" RUNAT="server" HEIGHT="17px" CSSCLASS="headfield1" WIDTH="71px"
																	Font-Names="Tahoma" Font-Size="9pt">Ending Date:</asp:label></TD>
															<TD>
																<maskedinput:maskedtextbox id="TxtEndDate" tabIndex="3" RUNAT="server" ISDATE="True" HEIGHT="19px" CSSCLASS="headfield1"
																	WIDTH="134px" Font-Names="Tahoma" Font-Size="9pt"></maskedinput:maskedtextbox><INPUT id="btnCalendar2" style="WIDTH: 21px; HEIGHT: 21px" onclick="javascript:calendar_window=window.open('selectDate.aspx?formname=PolicyReport.TxtEndDate','calendar_window','width=250,height=250');calendar_window.focus()"
																	tabIndex="4" type="button" value="..." name="btnCalendar" RUNAT="server"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 87px">
																<asp:label id="LblDataType" RUNAT="server" HEIGHT="17px" CSSCLASS="headfield1" WIDTH="74px"
																	Font-Names="Tahoma" Font-Size="9pt">Date Type:</asp:label></TD>
															<TD>
																<asp:dropdownlist id="ddlDateType" tabIndex="5" RUNAT="server" HEIGHT="23px" CSSCLASS="headfield1"
																	WIDTH="161px" Font-Names="Tahoma" Font-Size="9pt">
																	<asp:ListItem></asp:ListItem>
																	<asp:ListItem Value="E">Entry Date</asp:ListItem>
																	<asp:ListItem Value="F">Effective Date</asp:ListItem>
																</asp:dropdownlist></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 35px; HEIGHT: 12px" align="center">
										<P align="center">&nbsp;</P>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 35px; HEIGHT: 36px">
										<P>&nbsp;</P>
										<P align="center">&nbsp;</P>
										<P align="center">&nbsp;</P>
										<P align="center">&nbsp;</P>
										<P align="center">&nbsp;</P>
										<P align="center">&nbsp;</P>
										<P align="center">&nbsp;</P>
									</TD>
								</TR>
							</TABLE>
						</P>
						<P>&nbsp;</P>
					</TD>
				</TR>
			</TABLE>
			&nbsp;&nbsp;&nbsp;
			<maskedinput:maskedtextheader id="MaskedTextHeader1" RUNAT="server"></maskedinput:maskedtextheader>
			<asp:literal id="litPopUp" runat="server" Visible="False"></asp:literal>
		</form>
	</body>
</HTML>
