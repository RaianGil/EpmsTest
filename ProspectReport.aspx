<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Page language="c#" Inherits="EPolicy.ProspectReport" CodeFile="ProspectReport.aspx.cs" %>
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
		<form id="Pros" method="post" runat="server">
			<TABLE id="Table8" style="Z-INDEX: 124; LEFT: 8px; WIDTH: 914px; POSITION: static; TOP: 8px; HEIGHT: 452px"
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
						colSpan="1" rowSpan="5" vAlign="top">
						<P align="center">
							<asp:placeholder id="phTopBanner1" runat="server"></asp:placeholder></P>
					</TD>
					<TD style="FONT-SIZE: 0pt; WIDTH: 86px; HEIGHT: 184px" align="center">
						<P align="center">
							<TABLE id="Table9" style="WIDTH: 811px; HEIGHT: 293px" cellSpacing="0" cellPadding="0"
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
													<asp:Label id="Label8" runat="server" Width="198px" Height="16px" CssClass="headForm1 " Font-Names="Tahoma"
														Font-Size="11pt" ForeColor="Navy" Font-Bold="True">Prospect Reports</asp:Label></TD>
												<TD></TD>
												<TD align="right">
													<asp:button id="Button2" tabIndex="44" runat="server" Width="64px" Height="23px" BorderColor="MidnightBlue"
														BorderWidth="0px" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" Text="Print" BackColor="MidnightBlue" onclick="Button2_Click"></asp:button>
													<asp:button id="BtnExit" tabIndex="45" runat="server" Width="61px" Height="23px" BorderColor="MidnightBlue"
														BorderWidth="0px" Font-Names="Tahoma" ForeColor="White" Text="Exit" BackColor="MidnightBlue" onclick="BtnExit_Click"></asp:button>&nbsp;
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
													<asp:radiobuttonlist id="rblProspectsReports" style="Design_Time_Lock: False" tabIndex="1" runat="server"
														CssClass="HeadField1" Design_Time_Lock="False" Font-Names="Tahoma" Font-Size="9pt" AutoPostBack="True" OnSelectedIndexChanged="rblProspectsReports_SelectedIndexChanged">
														<asp:ListItem Value="0" Selected="True">Prospects List</asp:ListItem>
														<asp:ListItem Value="1">Prospects Without Quotes</asp:ListItem>
														<asp:ListItem Value="2">Prospects With Quotes</asp:ListItem>
														<asp:ListItem Value="3">Prospects Converted To Clients</asp:ListItem>
													</asp:radiobuttonlist></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 10pt; WIDTH: 112px; HEIGHT: 3px" align="left" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:label id="LblTotalCases" Height="9px" RUNAT="server" WIDTH="141px" CSSCLASS="headform3"
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
													<TABLE id="Table5" style="WIDTH: 396px; HEIGHT: 99px" cellSpacing="0" cellPadding="0"
														border="0">
                                                        <tr>
                                                            <td style="width: 87px; height: 20px">
                                                                <asp:Label ID="lblPolicyClass" runat="server" CssClass="headfield1" Font-Names="Tahoma"
                                                                    Font-Size="Smaller" ForeColor="Black" Height="19" Width="130px">Line Of  Business:</asp:Label></td>
                                                            <td style="height: 20px">
                                                                <asp:DropDownList ID="ddlPolicyClass" runat="server" CssClass="headfield1"
                                                                    Font-Names="Tahoma" Font-Size="9pt" Height="19px" TabIndex="7" Width="212px">
                                                                </asp:DropDownList></td>
                                                        </tr>
														<TR>
															<TD style="WIDTH: 87px">
																<asp:label id="Label4" runat="server" Width="100px" CssClass="HeadField1" Font-Names="Tahoma"
																	Font-Size="9pt">Prospect Type:</asp:label></TD>
															<TD>
																<asp:dropdownlist id="ddlCustType" tabIndex="2" runat="server" Width="212px" Height="19px" CssClass="HeadField1"
																	Font-Names="Tahoma" Font-Size="9pt">
																	<asp:ListItem Value="I">Individual</asp:ListItem>
																	<asp:ListItem Value="B">Business</asp:ListItem>
																</asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 87px"><asp:label id="Label1" runat="server" CssClass="HeadField1" Width="72px" Font-Names="Tahoma"
																	Font-Size="9pt">Date From:</asp:label></TD>
															<TD><maskedinput:maskedtextbox id="txtBegDate" tabIndex="3" CSSCLASS="headfield1" WIDTH="89px" RUNAT="server" HEIGHT="19px"
																	ISDATE="True" Font-Names="Tahoma" Font-Size="9pt"></maskedinput:maskedtextbox><INPUT id="btnCalendar" style="WIDTH: 21px; HEIGHT: 21px" onclick="javascript:calendar_window=window.open('selectDate.aspx?formname=document.Pros.txtBegDate','calendar_window','width=250,height=250');calendar_window.focus()"
																	tabIndex="4" type="button" value="..." name="btnCalendar" RUNAT="server" onserverclick="btnCalendar_ServerClick">&nbsp;&nbsp;
																<asp:label id="Label2" runat="server" CssClass="HeadField1" Font-Names="Tahoma" Font-Size="9pt">To</asp:label>&nbsp;
																<maskedinput:maskedtextbox id="TxtEndDate" tabIndex="5" CSSCLASS="headfield1" WIDTH="89px" RUNAT="server" HEIGHT="19px"
																	ISDATE="True" Font-Names="Tahoma" Font-Size="9pt"></maskedinput:maskedtextbox><INPUT id="Button1" style="WIDTH: 21px; HEIGHT: 21px" onclick="javascript:calendar_window=window.open('selectDate.aspx?formname=document.Pros.TxtEndDate','calendar_window','width=250,height=250');calendar_window.focus()"
																	tabIndex="6" type="button" value="..." name="btnCalendar" RUNAT="server"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 87px; HEIGHT: 20px"></TD>
															<TD style="HEIGHT: 20px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 87px"></TD>
															<TD></TD>
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
									<TD style="FONT-SIZE: 0pt; WIDTH: 35px; HEIGHT: 84px">
										<P>&nbsp;</P>
										<P align="center">&nbsp;</P>
										<P align="center">&nbsp;</P>
										<P align="center">&nbsp;</P>
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
			<asp:literal id="litPopUp" RUNAT="server" VISIBLE="False"></asp:literal>
			<maskedinput:maskedtextheader id="MaskedTextHeader1" RUNAT="server"></maskedinput:maskedtextheader></form>
	</body>
</HTML>
