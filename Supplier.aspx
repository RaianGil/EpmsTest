<%@ Page language="c#" Inherits="EPolicy.Supplier" CodeFile="Supplier.aspx.cs" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ePMS | electronic Policy Manager Solution</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body background="Images2/SQUARE.GIF" topMargin="19">
		<form id="Supp" method="post" runat="server">
			<TABLE id="Table8" style="Z-INDEX: 132; LEFT: 4px; WIDTH: 911px; POSITION: static; TOP: 96px; HEIGHT: 281px"
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
							<TABLE id="Table9" style="WIDTH: 809px; HEIGHT: 99px" cellSpacing="0" cellPadding="0" width="809"
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
													<asp:Label id="Label21" runat="server" Width="47px" Height="16px" CssClass="headForm1 " ForeColor="Navy"
														Font-Size="11pt" Font-Bold="True" Font-Names="Tahoma">Supplier:</asp:Label>
													<asp:Label id="lblSupplierID" runat="server" Width="120px" Font-Names="Tahoma" Font-Size="9pt"></asp:Label></TD>
												<TD align="right">
													<asp:Button id="btnAuditTrail" runat="server" Width="70px" Height="23px" BorderWidth="0px" BorderColor="MidnightBlue"
														ForeColor="White" Text="AuditTrail" Font-Names="Tahoma" BackColor="MidnightBlue" onclick="btnAuditTrail_Click"></asp:Button>
													<asp:Button id="btnIncentive" runat="server" Width="73px" Height="23px" BorderWidth="0px" BorderColor="MidnightBlue"
														ForeColor="White" Text="Incentive" Font-Names="Tahoma" BackColor="MidnightBlue" onclick="btnIncentive_Click"></asp:Button>
													<asp:Button id="btnPrint" runat="server" Width="46px" Height="23px" BorderWidth="0px" BorderColor="MidnightBlue"
														ForeColor="White" Text="Print" Font-Names="Tahoma" BackColor="MidnightBlue" onclick="btnPrint_Click"></asp:Button>
													<asp:Button id="btnSearch" runat="server" Width="46px" Height="23px" BorderWidth="0px" BorderColor="MidnightBlue"
														ForeColor="White" Text="Search" Font-Names="Tahoma" BackColor="MidnightBlue" onclick="btnSearch_Click"></asp:Button>
													<asp:Button id="BtnSave" runat="server" Width="46px" Height="23px" BorderWidth="0px" BorderColor="MidnightBlue"
														ForeColor="White" Text="Save" Font-Names="Tahoma" BackColor="MidnightBlue" onclick="BtnSave_Click"></asp:Button>
													<asp:Button id="btnAddNew" runat="server" Width="59px" Height="23px" BorderWidth="0px" BorderColor="MidnightBlue"
														ForeColor="White" Text="AddNew" Font-Names="Tahoma" BackColor="MidnightBlue" onclick="btnAddNew_Click"></asp:Button>
													<asp:Button id="btnEdit" runat="server" Width="46px" Height="23px" BorderWidth="0px" BorderColor="MidnightBlue"
														ForeColor="White" Text="Edit" Font-Names="Tahoma" BackColor="MidnightBlue" onclick="btnEdit_Click"></asp:Button>
													<asp:Button id="cmdCancel" runat="server" Width="46px" Height="23px" BorderWidth="0px" BorderColor="MidnightBlue"
														ForeColor="White" Text="Cancel" Font-Names="Tahoma" BackColor="MidnightBlue" onclick="cmdCancel_Click"></asp:Button>
													<asp:Button id="BtnExit" runat="server" Width="46px" Height="23px" BorderWidth="0px" BorderColor="MidnightBlue"
														ForeColor="White" Text="Exit" Font-Names="Tahoma" BackColor="MidnightBlue" onclick="BtnExit_Click"></asp:Button>&nbsp;
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
															<TD style="WIDTH: 186px; HEIGHT: 1px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 1px">
																<asp:label id="lblB" CSSCLASS="headfield1" RUNAT="server" HEIGHT="19px" ForeColor="Black" Font-Names="Tahoma"
																	Font-Size="9pt">Supplier Description</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 1px">
																<asp:textbox id="txtSupplierDescription" tabIndex="1" CSSCLASS="headfield1" RUNAT="server" HEIGHT="21"
																	WIDTH="249px" MAXLENGTH="30" Font-Names="Tahoma" Font-Size="9pt"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 1px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 10px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 10px">
																<asp:label id="lblAddress1" Width="99px" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma"
																	Font-Size="9pt"> Address 1</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 10px">
																<asp:textbox id="txtAddress1" tabIndex="2" CSSCLASS="headfield1" RUNAT="server" HEIGHT="21" WIDTH="249"
																	MAXLENGTH="20" Font-Names="Tahoma" Font-Size="9pt"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 10px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 1px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 1px">
																<asp:label id="lblAddress2" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma" Font-Size="9pt"> Address 2</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 1px">
																<asp:textbox id="txtAddress2" tabIndex="3" CSSCLASS="headfield1" RUNAT="server" HEIGHT="21" WIDTH="249"
																	MAXLENGTH="20" Font-Names="Tahoma" Font-Size="9pt"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 1px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<asp:label id="lblCity" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma" Font-Size="9pt">City</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
																<asp:textbox id="txtCity" tabIndex="4" CSSCLASS="headfield1" RUNAT="server" HEIGHT="21" WIDTH="144px"
																	MAXLENGTH="10" Font-Names="Tahoma" Font-Size="9pt"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<asp:label id="lblState" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma" Font-Size="9pt">State</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
																<asp:textbox id="txtSt" tabIndex="5" CSSCLASS="headfield1" RUNAT="server" HEIGHT="21px" WIDTH="49px"
																	MAXLENGTH="2" Font-Names="Tahoma" Font-Size="9pt"></asp:textbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 20px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 20px">
																<asp:label id="lblZipCode" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma" Font-Size="9pt">Zip Code</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 20px">
																<maskedinput:maskedtextbox id="txtZipCode" tabIndex="6" CSSCLASS="headfield1" RUNAT="server" ISDATE="False"
																	HEIGHT="19px" WIDTH="144px" MASK="99999Z9999" MAXLENGTH="10" Font-Names="Tahoma" Font-Size="9pt"></maskedinput:maskedtextbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 20px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<asp:label id="lblPhone" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma" Font-Size="9pt">Phone </asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
																<maskedinput:MaskedTextBox id="txtPhone" tabIndex="7" runat="server" Width="144" Height="19px" CssClass="headfield1"
																	Mask="(999) 999-9999" Columns="34" Font-Names="Tahoma" Font-Size="9pt"></maskedinput:MaskedTextBox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 186px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<P id="P1" runat="server">
																	<asp:label id="lblEntryDate" CSSCLASS="headfield1" RUNAT="server" Font-Names="Tahoma" Font-Size="9pt">Entry Date</asp:label></P>
															</TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px">
																<maskedinput:maskedtextbox id="txtEntryDate" tabIndex="8" CSSCLASS="headfield1" RUNAT="server" Enabled="False"
																	ISDATE="True" HEIGHT="19px" WIDTH="78px" Font-Names="Tahoma" Font-Size="9pt"></maskedinput:maskedtextbox></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
                                                        <tr>
                                                            <td style="width: 186px; height: 5px">
                                                            </td>
                                                            <td style="width: 162px; height: 5px">
                                                            </td>
                                                            <td style="width: 168px; height: 5px">
                                                            </td>
                                                            <td style="width: 168px; height: 5px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 186px; height: 5px">
                                                            </td>
                                                            <td style="width: 162px; height: 5px">
                                                                <asp:Label ID="lblAddSupplier" runat="server" CssClass="headform3" Font-Bold="True"
                                                                    Font-Names="Arial" Font-Size="9pt" Height="3px" Width="112px">Add Supplier:</asp:Label></td>
                                                            <td style="width: 168px; height: 5px">
                                                            </td>
                                                            <td style="width: 168px; height: 5px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 186px; height: 5px">
                                                            </td>
                                                            <td style="width: 162px; height: 5px">
                                                                <asp:DropDownList ID="ddlSupplier2" runat="server" CssClass="headfield1" Font-Names="Tahoma"
                                                                    Font-Size="8pt" Height="19px" TabIndex="23" Width="227px">
                                                                </asp:DropDownList></td>
                                                            <td style="width: 168px; height: 5px">
                                                                <asp:Button ID="btnAdd2" runat="server" BackColor="MidnightBlue" BorderColor="MidnightBlue"
                                                                    BorderWidth="0px" Font-Names="Tahoma" ForeColor="White" Height="18px" OnClick="btnAdd2_Click"
                                                                    TabIndex="47" Text="Add" Width="69px" /></td>
                                                            <td style="width: 168px; height: 5px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 186px; height: 5px">
                                                            </td>
                                                            <td style="width: 162px; height: 5px">
                                                            </td>
                                                            <td style="width: 168px; height: 5px">
                                                            </td>
                                                            <td style="width: 168px; height: 5px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 186px; height: 5px">
                                                            </td>
                                                            <td colspan="2" style="width: 162px; height: 5px">
                                                                <asp:DataGrid ID="dgSupplier2" runat="server" AllowPaging="True" AlternatingItemStyle-BackColor="#FEFBF6"
                                                                    AlternatingItemStyle-CssClass="HeadForm3" AutoGenerateColumns="False" BackColor="White"
                                                                    BorderColor="#003366" BorderStyle="Solid" BorderWidth="1px" CellPadding="0" Font-Names="Arial"
                                                                    Font-Size="Smaller" ForeColor="Black" HeaderStyle-BackColor="#5C8BAE" HeaderStyle-CssClass="HeadForm2"
                                                                    HeaderStyle-HorizontalAlign="Center" Height="170px" ItemStyle-CssClass="HeadForm3"
                                                                    ItemStyle-HorizontalAlign="center" OnItemCommand="dgSupplier2_ItemCommand" PageSize="6"
                                                                    Width="355px">
                                                                    <FooterStyle BackColor="#003366" ForeColor="White" />
                                                                    <PagerStyle BackColor="#003366" CssClass="Numbers" ForeColor="White" HorizontalAlign="Left"
                                                                        Mode="NumericPages" PageButtonCount="20" />
                                                                    <AlternatingItemStyle BackColor="#F1FFFF" CssClass="HeadForm3" />
                                                                    <ItemStyle CssClass="HeadForm3" HorizontalAlign="Center" />
                                                                    <Columns>
                                                                        <asp:BoundColumn DataField="SupplierID" HeaderText="SupplierID" Visible="False">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="SupplierDesc" HeaderText="Supplier">
                                                                            <ItemStyle HorizontalAlign="Left" />
                                                                        </asp:BoundColumn>
                                                                        <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" HeaderImageUrl="Images2/x.bmp">
                                                                        </asp:ButtonColumn>
                                                                        <asp:BoundColumn DataField="GroupSupplierID" HeaderText="GroupSupplierID" Visible="False">
                                                                        </asp:BoundColumn>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#003366" CssClass="HeadForm2" ForeColor="White" Height="10px"
                                                                        HorizontalAlign="Center" />
                                                                </asp:DataGrid></td>
                                                            <td style="width: 168px; height: 5px">
                                                            </td>
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
						<P>&nbsp;</P>
						<P>&nbsp;</P>
					</TD>
				</TR>
			</TABLE>
			<asp:literal id="litPopUp" RUNAT="server" VISIBLE="False"></asp:literal>
			<maskedinput:maskedtextheader id="MaskedTextHeader1" runat="server"></maskedinput:maskedtextheader>
			<asp:button id="BtnBegin" tabIndex="16" runat="server" Height="23px" Text="<<" style="Z-INDEX: 116; LEFT: 666px; POSITION: absolute; TOP: 598px"
				Visible="False" onclick="BtnBegin_Click"></asp:button>
			<asp:button id="BtnPrevious" tabIndex="17" runat="server" Height="23px" Width="24px" Text="<"
				style="Z-INDEX: 117; LEFT: 686px; POSITION: absolute; TOP: 598px" Visible="False" onclick="BtnPrevious_Click"></asp:button>
			<asp:button id="BtnNext" tabIndex="18" runat="server" Height="23px" Width="24px" Text=">" style="Z-INDEX: 118; LEFT: 706px; POSITION: absolute; TOP: 598px"
				Visible="False" onclick="BtnNext_Click"></asp:button>
			<asp:button id="BtnEnd" tabIndex="19" runat="server" Height="23px" Text=">>" style="Z-INDEX: 119; LEFT: 726px; POSITION: absolute; TOP: 598px"
				Visible="False" onclick="BtnEnd_Click"></asp:button>
			<asp:label id="lblRecordCount" RUNAT="server" CSSCLASS="headform2" WIDTH="129px" HEIGHT="17px"
				style="Z-INDEX: 120; LEFT: 746px; POSITION: absolute; TOP: 598px" Visible="False">1 of 1</asp:label>
		</form>
	</body>
</HTML>
