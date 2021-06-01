<%@ Page language="c#" Inherits="EPolicy.Payments" CodeFile="Payments.aspx.cs" %>
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
		<form id="Paym" method="post" runat="server">
			<TABLE id="Table8" style="Z-INDEX: 139; LEFT: 4px; WIDTH: 921px; POSITION: static; TOP: 4px; HEIGHT: 507px"
				cellSpacing="0" cellPadding="0" width="921" align="center" bgColor="white" dataPageSize="25"
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
					<TD style="FONT-SIZE: 0pt; WIDTH: 86px; HEIGHT: 395px" align="center">
						<P align="center">
							<TABLE id="Table9" style="WIDTH: 819px; HEIGHT: 345px" cellSpacing="0" cellPadding="0"
								width="819" bgColor="white" border="0">
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="center"></TD>
									<TD style="FONT-SIZE: 0pt; HEIGHT: 3px" align="right" colSpan="3"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 7px" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table10" style="WIDTH: 808px; HEIGHT: 12px" cellSpacing="0" cellPadding="0"
											width="808" border="0">
											<TR>
												<TD align="left">&nbsp;
													<asp:Label id="Label21" runat="server" Height="16px" Width="137px" CssClass="headForm1 " Font-Bold="True"
														ForeColor="Navy" Font-Names="Tahoma" Font-Size="11pt">Payment Center:</asp:Label>
													<asp:label id="lblTaskControlID" RUNAT="server" CSSCLASS="HeadField" Font-Size="9pt" Font-Names="Tahoma"></asp:label></TD>
												<TD></TD>
												<TD align="right">
													<asp:Button id="btnDelete" runat="server" Height="23px" Width="54" Text="Delete" BorderWidth="0px"
														BorderColor="MidnightBlue" BackColor="MidnightBlue" ForeColor="White" Font-Names="Tahoma" onclick="BtnDele_Click"></asp:Button>
													<asp:Button id="btnReceipt" runat="server" Height="23px" Width="54px" Text="Receipt" BorderWidth="0px"
														BorderColor="MidnightBlue" BackColor="MidnightBlue" ForeColor="White" Font-Names="Tahoma" onclick="btnReceipt_Click"></asp:Button>
													<asp:Button id="BtnSave" runat="server" Height="23px" Width="54px" Text="Save" BorderWidth="0px"
														BorderColor="MidnightBlue" BackColor="MidnightBlue" ForeColor="White" Font-Names="Tahoma" onclick="Button6_Click"></asp:Button>
													<asp:Button id="btnCancel" runat="server" Height="23px" Text="Cancel" BorderWidth="0px" BorderColor="MidnightBlue"
														BackColor="MidnightBlue" ForeColor="White" Font-Names="Tahoma" Width="54px" onclick="Button5_Click"></asp:Button>
													<asp:Button id="btnEdit" runat="server" Height="23px" Width="54px" Text="Edit" BorderWidth="0px"
														BorderColor="MidnightBlue" BackColor="MidnightBlue" ForeColor="White" Font-Names="Tahoma" onclick="Button4_Click"></asp:Button>
													<asp:Button id="btnAuditTrail" runat="server" Height="23px" Text="History" BorderWidth="0px"
														BorderColor="MidnightBlue" BackColor="MidnightBlue" ForeColor="White" Font-Names="Tahoma"
														Width="54px" onclick="Button3_Click"></asp:Button>
													<asp:Button id="BtnExit" runat="server" Height="23px" Width="54px" Text="Exit" BorderWidth="0px"
														BorderColor="MidnightBlue" BackColor="MidnightBlue" ForeColor="White" Font-Names="Tahoma" onclick="Button2_Click"></asp:Button>&nbsp;
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; HEIGHT: 4px">
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
									<TD style="FONT-SIZE: 2pt; WIDTH: 112px; HEIGHT: 46px" vAlign="middle" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table6" style="WIDTH: 811px; HEIGHT: 154px" cellSpacing="0" cellPadding="0"
											width="811" border="0">
											<TR>
												<TD style="FONT-SIZE: 1pt" align="center">
													<TABLE id="Table1" style="WIDTH: 806px; HEIGHT: 234px" cellSpacing="0" cellPadding="0"
														width="806" border="0">
														<TR>
															<TD style="WIDTH: 8px; HEIGHT: 1px"></TD>
															<TD style="WIDTH: 67px; HEIGHT: 1px">
																<asp:label id="Label2" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="6px" WIDTH="102px">Line of Business</asp:label></TD>
															<TD style="WIDTH: 33px; HEIGHT: 1px" align="left"></TD>
															<TD style="WIDTH: 178px; HEIGHT: 1px" align="left">
																<asp:dropdownlist id="ddlPolicyClass" tabIndex="1" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="12px" WIDTH="169" onselectedindexchanged="ddlPolicyClass_SelectedIndexChanged"></asp:dropdownlist></TD>
															<TD style="WIDTH: 135px; HEIGHT: 1px">&nbsp;
																<asp:label id="Label1" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="18px" WIDTH="76px">Customer No.</asp:label></TD>
															<TD style="WIDTH: 123px; HEIGHT: 1px">
																<asp:textbox id="TxtCustomerNo" tabIndex="9" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="19px" WIDTH="139px" MAXLENGTH="10"></asp:textbox></TD>
															<TD style="WIDTH: 6px; HEIGHT: 1px"></TD>
															<TD style="WIDTH: 240px; HEIGHT: 1px">
																<asp:label id="Label8" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="18" WIDTH="47px">Status</asp:label></TD>
															<TD style="WIDTH: 14px; HEIGHT: 1px">
																<asp:dropdownlist id="ddlTaskStatus" tabIndex="14" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="19px" WIDTH="170px"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 8px; HEIGHT: 15px"></TD>
															<TD style="WIDTH: 67px; HEIGHT: 15px">
                                                                <asp:Label ID="Label15" runat="server" CssClass="headfield1" EnableViewState="False"
                                                                    Font-Names="Tahoma" Font-Size="9pt" Height="6px" Width="102px">Ins. Company</asp:Label></TD>
															<TD style="WIDTH: 33px; HEIGHT: 15px" align="left"></TD>
															<TD style="WIDTH: 178px; HEIGHT: 15px" align="left"><asp:dropdownlist id="ddlInsuranceCompany" tabIndex="1" Font-Size="8pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="12px" WIDTH="201px" onselectedindexchanged="ddlPolicyClass_SelectedIndexChanged" Enabled="False">
                                                            </asp:DropDownList></TD>
															<TD style="WIDTH: 135px; HEIGHT: 15px">&nbsp;
																<asp:label id="lblSocialSecurity" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="89px" HEIGHT="18px" ENABLEVIEWSTATE="False">Name</asp:label></TD>
															<TD style="WIDTH: 123px; HEIGHT: 15px">
																<asp:textbox id="TxtName" tabIndex="10" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="139px" HEIGHT="19px" MAXLENGTH="15"></asp:textbox></TD>
															<TD style="WIDTH: 6px; HEIGHT: 15px"></TD>
															<TD style="WIDTH: 240px; HEIGHT: 15px">
																<asp:label id="Label5" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="18" WIDTH="47px">Aging</asp:label></TD>
															<TD style="WIDTH: 14px; HEIGHT: 15px">
																<asp:textbox id="TxtAging" tabIndex="15" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="19px" WIDTH="171px" MAXLENGTH="50"></asp:textbox></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 8px; HEIGHT: 15px"></TD>
															<TD style="WIDTH: 67px; HEIGHT: 15px">
																<asp:label id="lblGender" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	HEIGHT="2px" ENABLEVIEWSTATE="False">Policy Type</asp:label></TD>
															<TD style="WIDTH: 33px; HEIGHT: 15px" align="left"></TD>
															<TD style="WIDTH: 178px; HEIGHT: 15px" align="left">
                                                                
																<asp:textbox id="TxtPolicyType" tabIndex="3" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="70px" HEIGHT="21" MAXLENGTH="3"></asp:textbox></TD>
															<TD style="WIDTH: 135px; HEIGHT: 15px">&nbsp;
																<asp:label id="lblHouseIncome" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	WIDTH="105px" HEIGHT="17px">Last Name1</asp:label></TD>
															<TD style="WIDTH: 123px; HEIGHT: 15px">
																<asp:textbox id="TxtLastName1" tabIndex="11" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="139px" HEIGHT="19px" MAXLENGTH="15"></asp:textbox></TD>
															<TD style="WIDTH: 6px; HEIGHT: 15px"></TD>
															<TD style="WIDTH: 240px; HEIGHT: 15px">
																<asp:label id="Label3" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="18" WIDTH="60px">Entry Date</asp:label></TD>
															<TD style="WIDTH: 14px; HEIGHT: 15px">
																<asp:textbox id="TxtEntryDate" tabIndex="16" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="19px" WIDTH="170px" MAXLENGTH="50"></asp:textbox></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 8px; HEIGHT: 14px"></TD>
															<TD style="WIDTH: 67px; HEIGHT: 14px">
																<asp:label id="lblLastName2" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	WIDTH="92px" HEIGHT="2px" ENABLEVIEWSTATE="False">Policy No.</asp:label></TD>
															<TD style="WIDTH: 33px; HEIGHT: 14px" align="left"></TD>
															<TD style="WIDTH: 178px; HEIGHT: 14px" align="left">
																<asp:textbox id="txtPolicyNo" tabIndex="4" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="129" HEIGHT="19px" MAXLENGTH="11"></asp:textbox><asp:Button id="BtnGo" runat="server" Font-Names="Tahoma" ForeColor="White" Width="27px" Height="16px"
																	BackColor="MidnightBlue" BorderColor="MidnightBlue" BorderWidth="0px" Text="Go" Visible="False" onclick="BtnGo_Click"></asp:Button></TD>
															<TD style="WIDTH: 135px; HEIGHT: 14px">&nbsp;
																<asp:label id="Label6" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	WIDTH="74px" HEIGHT="17px">Last Name 2</asp:label></TD>
															<TD style="WIDTH: 123px; HEIGHT: 14px">
																<asp:textbox id="TxtLastName2" tabIndex="12" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="19px" WIDTH="139px" MAXLENGTH="15"></asp:textbox></TD>
															<TD style="WIDTH: 6px; HEIGHT: 14px"></TD>
															<TD style="WIDTH: 240px; HEIGHT: 14px">
																<asp:label id="Label9" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="18" WIDTH="72px">Close Date</asp:label></TD>
															<TD style="WIDTH: 14px; HEIGHT: 14px">
																<asp:textbox id="TxtCloseDate" tabIndex="17" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="19px" WIDTH="171px" MAXLENGTH="50"></asp:textbox></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 8px; HEIGHT: 11px"></TD>
															<TD style="WIDTH: 67px; HEIGHT: 11px">
																<asp:label id="lblBirthdate" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	WIDTH="92px" HEIGHT="18px" ENABLEVIEWSTATE="False">Certificate</asp:label></TD>
															<TD style="WIDTH: 33px; HEIGHT: 11px"></TD>
															<TD style="WIDTH: 178px; HEIGHT: 11px">
																<asp:textbox id="txtCertificate" tabIndex="5" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="171px" HEIGHT="19px" MAXLENGTH="10" ontextchanged="txtCertificate_TextChanged"></asp:textbox></TD>
															<TD style="WIDTH: 135px; HEIGHT: 11px">&nbsp;
																<asp:label id="Label7" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	WIDTH="88px" HEIGHT="17px" Visible="False">Social Security</asp:label></TD>
															<TD style="WIDTH: 123px; HEIGHT: 11px">
																<asp:textbox id="TxtSocialSecurity" tabIndex="13" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="HeadField1"
																	RUNAT="server" HEIGHT="19px" WIDTH="139px" MAXLENGTH="15" Visible="False"></asp:textbox></TD>
															<TD style="WIDTH: 6px; HEIGHT: 11px"></TD>
															<TD style="WIDTH: 240px; HEIGHT: 11px">
																<asp:label id="LblBank" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="7px" WIDTH="26px">Bank</asp:label></TD>
															<TD style="WIDTH: 14px; HEIGHT: 11px">
																<asp:dropdownlist id="ddlBank" tabIndex="18" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="19px" WIDTH="172px"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 8px; HEIGHT: 1px"></TD>
															<TD style="WIDTH: 67px; HEIGHT: 1px">
																<asp:label id="Label4" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	WIDTH="92px" HEIGHT="18px" ENABLEVIEWSTATE="False">Suffix</asp:label></TD>
															<TD style="WIDTH: 33px; HEIGHT: 1px" align="left"></TD>
															<TD style="WIDTH: 178px; HEIGHT: 1px" align="left">
                                                                
																<asp:textbox id="TxtSuffix" tabIndex="6" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="59px" HEIGHT="19px" MAXLENGTH="3" ontextchanged="TxtSuffix_TextChanged"></asp:textbox></TD>
															<TD style="WIDTH: 135px; HEIGHT: 1px"></TD>
															<TD style="WIDTH: 123px; HEIGHT: 1px" align="left"></TD>
															<TD style="WIDTH: 6px; HEIGHT: 1px" align="left"></TD>
															<TD style="WIDTH: 240px; HEIGHT: 1px" align="left">
																<asp:label id="Label13" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="7px" WIDTH="70px">Receipt No.</asp:label></TD>
															<TD style="WIDTH: 14px; HEIGHT: 1px" align="left">
																<asp:textbox id="TxtReceiptNo" tabIndex="15" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="19px" WIDTH="171px" MAXLENGTH="50"></asp:textbox></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 8px; HEIGHT: 10px"></TD>
															<TD style="WIDTH: 67px; HEIGHT: 10px">
																<asp:label id="lblMaritalStatus" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="92px" HEIGHT="1px" ENABLEVIEWSTATE="False">Loan No.</asp:label></TD>
															<TD style="WIDTH: 33px; HEIGHT: 10px" align="left"></TD>
															<TD style="WIDTH: 178px; HEIGHT: 10px" align="left">
																<asp:textbox id="TxtLoanNo" tabIndex="7" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="129px" HEIGHT="19px" MAXLENGTH="15"></asp:textbox><asp:Button id="BtnGo2" runat="server" Font-Names="Tahoma" ForeColor="White" Width="27px" Height="17px"
																	BackColor="MidnightBlue" BorderColor="MidnightBlue" BorderWidth="0px" Text="Go" Visible="False" onclick="BtnGo2_Click"></asp:Button></TD>
															<TD style="WIDTH: 135px; HEIGHT: 10px">
																<asp:label id="lblLastName1" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	WIDTH="102px" HEIGHT="2px" ENABLEVIEWSTATE="False" Visible="False">Ins. Co. Policy No.</asp:label></TD>
															<TD style="WIDTH: 123px; HEIGHT: 10px">
																<asp:textbox id="txtOriginalPolicyNo" tabIndex="2" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="88px" HEIGHT="19px" MAXLENGTH="15" Visible="False"></asp:textbox></TD>
															<TD style="WIDTH: 6px; HEIGHT: 10px"></TD>
															<TD style="WIDTH: 240px; HEIGHT: 10px">
																<asp:label id="Label16" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="7px" WIDTH="72px">Authorize By</asp:label></TD>
															<TD style="WIDTH: 14px; HEIGHT: 10px">
																<asp:textbox id="TxtAuthorizeBy" tabIndex="15" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="19px" WIDTH="171px" MAXLENGTH="40"></asp:textbox></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 8px; HEIGHT: 18px"></TD>
															<TD style="WIDTH: 67px; HEIGHT: 18px">
																<asp:label id="lblComments" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="9px" WIDTH="92px">Comments</asp:label></TD>
															<TD style="WIDTH: 33px; HEIGHT: 18px"></TD>
															<TD style="WIDTH: 178px; HEIGHT: 18px">
																<asp:textbox id="txtComments" tabIndex="8" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="202px" HEIGHT="80px" MAXLENGTH="200" TextMode="MultiLine"></asp:textbox></TD>
															<TD style="WIDTH: 135px; HEIGHT: 18px" align="left">
																<P align="left">&nbsp;</P>
																<P>&nbsp;</P>
															</TD>
															<TD style="WIDTH: 297px; HEIGHT: 18px" colSpan="4"></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 5pt; WIDTH: 112px; HEIGHT: 17px" align="left" colSpan="1">
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
									<TD style="FONT-SIZE: 0pt; WIDTH: 35px; HEIGHT: 28px">
										<TABLE id="Table3" style="WIDTH: 814px; HEIGHT: 76px" cellSpacing="1" cellPadding="1" width="814"
											border="0">
											<TR>
												<TD style="FONT-SIZE: 1pt" align="center">
													<TABLE id="Table4" style="WIDTH: 809px; HEIGHT: 88px" cellSpacing="0" cellPadding="0" width="809"
														border="0">
														<TR>
															<TD style="WIDTH: 174px; height: 19px;" colSpan="2">
																<asp:label id="lblTypeAddress1" Font-Size="9pt" Font-Names="Tahoma" Font-Bold="True" CSSCLASS="headform3"
																	RUNAT="server" WIDTH="162px">Payment Information</asp:label></TD>
															<TD style="height: 19px; width: 109px;"></TD>
															<TD style="WIDTH: 102px; height: 19px;"></TD>
															<TD style="height: 19px; width: 191px;">
																<asp:label id="Label12" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headform3" RUNAT="server"
																	WIDTH="60px" HEIGHT="2px" ENABLEVIEWSTATE="False">Check No.</asp:label></TD>
															<TD style="WIDTH: 69px; height: 19px;">
																<asp:label id="Label10" Font-Size="9pt" Font-Names="Tahoma" Height="8px" CSSCLASS="headform3"
																	RUNAT="server" WIDTH="79px">Total Amount</asp:label></TD>
															<TD style="width: 129px; height: 19px;"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 174px; height: 23px;" align="right" colSpan="2">
																<asp:checkbox id="ChkMultiple" tabIndex="19" runat="server" Font-Size="9pt" Font-Names="Tahoma"
																	CssClass="headform3" Width="126px" Height="16px" Text="Multiple Payments"></asp:checkbox></TD>
															<TD style="WIDTH: 109px; height: 23px;">
																<asp:Button id="BtnNewPayment" runat="server" Font-Names="Tahoma" ForeColor="White" Width="99px"
																	Height="23px" BackColor="MidnightBlue" BorderColor="MidnightBlue" BorderWidth="0px" Text="New Payment" onclick="BtnNewPayment_Click"></asp:Button></TD>
															<TD style="WIDTH: 102px; height: 23px;">
																<asp:label id="LblTotalCases" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headform3" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="2px" WIDTH="101px">Total Items:</asp:label></TD>
															<TD style="WIDTH: 191px; height: 23px;">
																<asp:textbox id="TxtCheckNo" tabIndex="20" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="19px" WIDTH="90px" MAXLENGTH="50"></asp:textbox></TD>
															<TD style="WIDTH: 69px; height: 23px;">
																<maskedinput:maskedtextbox id="TxtTotalAmount" tabIndex="21" runat="server" Font-Size="9pt" Font-Names="Tahoma"
																	CssClass="headfield1" Width="89px" Height="19px" IsCurrency="True"></maskedinput:maskedtextbox></TD>
															<TD style="width: 129px; height: 23px;"></TD>
														</TR>
														<TR>
															<TD style="FONT-SIZE: 5pt; WIDTH: 400px; HEIGHT: 7px" colSpan="7">
																<TABLE id="Table7" style="WIDTH: 808px" borderColor="#4b0082" height="1" cellSpacing="0"
																	borderColorDark="#4b0082" cellPadding="0" width="808" bgColor="indigo" borderColorLight="#4b0082"
																	border="0">
																	<TR>
																		<TD style="height: 6px"></TD>
																	</TR>
																</TABLE>
															</TD>
															<TD style="FONT-SIZE: 5pt; WIDTH: 400px; HEIGHT: 7px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 72px; height: 18px;" align="left">
                                                                <asp:Label ID="lblAdjustment" runat="server" CssClass="headfield1" EnableViewState="False"
                                                                    Font-Names="Tahoma" Font-Size="9pt" Text="Adjustment" Width="71px"></asp:Label></TD>
															<TD style="WIDTH: 117px; height: 18px;" align="center">
																<asp:label id="LblPaymentDate" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="18" WIDTH="107px">Payment Date</asp:label></TD>
															<TD align="center" style="height: 18px; width: 109px;">
																<asp:label id="Label17" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="18" WIDTH="94px">Payment Check</asp:label></TD>
															<TD style="WIDTH: 102px; height: 18px;" align="center">
																<asp:label id="Label18" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="18" WIDTH="101px">Payment Amount</asp:label></TD>
															<TD align="center" style="height: 18px; width: 191px;">
																<asp:label id="Label14" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="18" WIDTH="144px">Name</asp:label></TD>
															<TD style="WIDTH: 69px; height: 18px;" align="center">
																<asp:label id="Label19" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	WIDTH="87px" HEIGHT="18" ENABLEVIEWSTATE="False">Debit / Credit</asp:label></TD>
															<TD align="center" style="height: 18px; width: 129px;">
																<asp:label id="Label20" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	WIDTH="77px" HEIGHT="18" ENABLEVIEWSTATE="False"> Deposit Date</asp:label></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 72px; height: 23px;" align="center">
                                                                <asp:CheckBox ID="chkAdjustment" runat="server" AutoPostBack="True" Text=" " CssClass="headfield1"
                                                                    Width="16px" OnCheckedChanged="chkAdjustment_CheckedChanged" /></TD>
															<TD style="WIDTH: 117px; height: 23px;" align="center">
																<maskedinput:maskedtextbox id="TxtPaymentDate" tabIndex="22" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="19px" WIDTH="82px" ISDATE="True"></maskedinput:maskedtextbox><INPUT id="btnCalendar" style="WIDTH: 21px; HEIGHT: 21px" onclick="javascript:calendar_window=window.open('selectDate.aspx?formname=document.Paym.TxtPaymentDate','calendar_window','width=250,height=250');calendar_window.focus()"
																	tabIndex="23" type="button" value="..." name="btnCalendar" RUNAT="server"></TD>
															<TD align="center" style="height: 23px; width: 109px;">
																<asp:textbox id="TxtPaymentCheck" tabIndex="24" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="19px" WIDTH="111px" MAXLENGTH="50"></asp:textbox></TD>
															<TD style="WIDTH: 102px; height: 23px;" align="center">
																<asp:textbox id="TxtPaymentAmount" tabIndex="25" runat="server" Font-Size="9pt" Font-Names="Tahoma"
																	CssClass="headfield1" Width="94px" Height="19px"></asp:textbox></TD>
															<TD align="center" style="height: 23px; width: 191px;">
																<asp:textbox id="TxtNamePayee" tabIndex="26" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" HEIGHT="21" WIDTH="161px" MAXLENGTH="75"></asp:textbox>&nbsp;</TD>
															<TD style="WIDTH: 69px; height: 23px;" align="center">
																<asp:dropdownlist id="ddlCreditDebit" tabIndex="27" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="73px" HEIGHT="13px" AutoPostBack="True"></asp:dropdownlist></TD>
															<TD style="height: 23px; width: 129px;">
																<maskedinput:maskedtextbox id="txtDepositDate" tabIndex="28" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="102px" HEIGHT="19px" ISDATE="True"></maskedinput:maskedtextbox>
																<INPUT id="Button1" style="WIDTH: 21px; HEIGHT: 21px" onclick="javascript:calendar_window=window.open('selectDate.aspx?formname=document.Paym.txtDepositDate','calendar_window','width=250,height=250');calendar_window.focus()"
																	tabIndex="29" type="button" value="..." name="btnCalendar" RUNAT="server"></TD>
														</TR>
                                                        <tr>
                                                            <td align="center" rowspan="1" style="width: 72px; height: 23px">
																<asp:label id="lblAdjustmentType" Font-Size="9pt" Font-Names="Tahoma" Font-Bold="False" CSSCLASS="headfield1"
																	RUNAT="server" WIDTH="20px" Visible="False">Type:</asp:label></td>
                                                            <td align="center" colspan="3" style="width: 118px; height: 23px">
                                                                <asp:DropDownList ID="ddlAdjustmentType" runat="server" CssClass="headfield1" Font-Names="Tahoma" 
                                                                    Font-Size="9pt" Height="12px" Width="104px" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="ddlAdjustmentType_SelectedIndexChanged">
                                                                </asp:DropDownList></td>
                                                            <td align="left" colspan="1" style="width: 191px; height: 23px">
                                                                <asp:RadioButton ID="rdbName" runat="server" Font-Names="Tahoma" Font-Size="9pt"
                                                                    Text="Customer" Checked="True" AutoPostBack="True" GroupName="Name" OnCheckedChanged="rdbName_CheckedChanged" Visible="False" />
                                                                <asp:RadioButton ID="rdbBank" runat="server" Font-Names="Tahoma" Font-Size="9pt"
                                                                    Text="Bank" AutoPostBack="True" GroupName="Name" OnCheckedChanged="rdbBank_CheckedChanged" Visible="False" />
                                                                <asp:RadioButton ID="rdbDealer" runat="server" Font-Names="Tahoma" Font-Size="9pt"
                                                                    Text="Dealer" Width="46px" AutoPostBack="True" GroupName="Name" OnCheckedChanged="rdbDealer_CheckedChanged" Visible="False" /></td>
                                                        </tr>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</P>
						<P>
                            &nbsp;&nbsp;</P>
						<P>
																<asp:checkbox id="ChkSRO" tabIndex="21" runat="server" CssClass="headfield1" Width="16px" Text=" "
																	AutoPostBack="True" oncheckedchanged="ChkSRO_CheckedChanged" Visible="False"></asp:checkbox>&nbsp;
																<asp:label id="Label11" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1" RUNAT="server"
																	ENABLEVIEWSTATE="False" HEIGHT="18px" WIDTH="10px" Visible="False">SRO</asp:label></P>
					</TD>
				</TR>
			</TABLE>
			<asp:checkbox id="ChkIsNEwTransaction" style="Z-INDEX: 101; LEFT: 408px; POSITION: absolute; TOP: 624px"
				tabIndex="21" runat="server" CssClass="headform1" Width="152px" Text="Is New Transaction"
				Visible="False"></asp:checkbox>
			<maskedinput:maskedtextheader id="MaskedTextHeader1" RUNAT="server"></maskedinput:maskedtextheader>
			<asp:literal id="litPopUp" runat="server" Visible="False"></asp:literal>&nbsp;<asp:DropDownList
                ID="ddlCompanyDealer" runat="server" Visible="False">
            </asp:DropDownList></form>
	</body>
</HTML>
