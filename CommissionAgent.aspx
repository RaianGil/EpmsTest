<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Page language="c#" Inherits="EPolicy.CommissionAgent" buffer="True" CodeFile="CommissionAgent.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ePMS | electronic Policy Manager Solution</title>
		<META content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="baldrich.css" type="text/css" rel="stylesheet">
	<script src="js/jquery-1.12.1.min.js" type="text/javascript"></script>
       <script src="js/jquery.mask.js" type="text/javascript"></script>

        <script type='text/javascript'>
        jQuery(function ($) {
            //$("#AccordionPane1_content_TxtWorkPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane1_content_TxtCellPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane1_content_TxtBirthDate").mask("00/00/0000", { placeholder: "__/__/____" });
            //          $('#TxtDriverBirthDate')
            $("#txtEffectiveDate").mask('00/00/0000', { placeholder: '__/__/____' });
        });
    </script>
    </HEAD>
    
	<BODY background="Images2/SQUARE.GIF" bottomMargin="0" leftMargin="0"
		topMargin="19" rightMargin="0">
		<FORM id="commAgt" method="post" runat="server">
			<TABLE id="Table8" style="Z-INDEX: 136; LEFT: 4px; WIDTH: 911px; POSITION: static; TOP: 37px; HEIGHT: 412px"
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
							<TABLE id="Table2" style="WIDTH: 809px; HEIGHT: 81px" cellSpacing="0" cellPadding="0" width="809"
								bgColor="white" border="0">
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="center"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table1" style="WIDTH: 808px; HEIGHT: 12px" cellSpacing="0" cellPadding="0" width="808"
											border="0">
											<TR>
												<TD style="WIDTH: 272px" align="left">&nbsp;&nbsp;&nbsp;
													<asp:Label id="Label3" runat="server" Font-Size="11pt" Font-Bold="True" Font-Names="tahoma,11pt"
														ForeColor="Navy" CssClass="headForm1 " Visible="False">Agent ID:</asp:Label>
													<asp:label id="lblAgentID" runat="server" Width="161px" Visible="False"></asp:label>
                                                    <br />
													<asp:Label id="Label4" runat="server" Font-Size="11pt" Font-Bold="True" Font-Names="tahoma,11pt"
														ForeColor="Navy" CssClass="headForm1 ">Agent ID:</asp:Label>
													<asp:label id="lblCarsID" runat="server" Width="161px"></asp:label></TD>
												<TD align="right">
													<asp:Button id="btnAuditTrail" runat="server" Font-Names="Tahoma" ForeColor="White" Width="68px"
														BorderWidth="0px" BorderColor="MidnightBlue" Text="AuditTrail" BackColor="MidnightBlue"
														Height="23px" onclick="btnAuditTrail_Click"></asp:Button>
													<asp:Button id="btnPrint" runat="server" Font-Names="Tahoma" ForeColor="White" Width="46px"
														BorderWidth="0px" BorderColor="MidnightBlue" Text="Print" BackColor="MidnightBlue" Height="23px" onclick="btnPrint_Click"></asp:Button>
													<asp:Button id="BtnSave" runat="server" Font-Names="Tahoma" ForeColor="White" Width="46px" BorderWidth="0px"
														BorderColor="MidnightBlue" Text="Save" BackColor="MidnightBlue" Height="23px" onclick="BtnSave_Click"></asp:Button>
													<asp:Button id="btnAddNew" runat="server" Font-Names="Tahoma" ForeColor="White" Width="68px"
														BorderWidth="0px" BorderColor="MidnightBlue" Text="AddNew" BackColor="MidnightBlue" Height="23px" onclick="btnAddNew_Click"></asp:Button>
													<asp:Button id="btnEdit" runat="server" Font-Names="Tahoma" ForeColor="White" Width="46px" BorderWidth="0px"
														BorderColor="MidnightBlue" Text="Edit" BackColor="MidnightBlue" Height="23px" onclick="btnEdit_Click"></asp:Button>
													<asp:Button id="cmdCancel" runat="server" Font-Names="Tahoma" ForeColor="White" Width="46px"
														BorderWidth="0px" BorderColor="MidnightBlue" Text="Cancel" BackColor="MidnightBlue" Height="23px" onclick="cmdCancel_Click"></asp:Button>
													<asp:Button id="BtnExit" runat="server" Font-Names="Tahoma" ForeColor="White" Width="46px" BorderWidth="0px"
														BorderColor="MidnightBlue" Text="Exit" BackColor="MidnightBlue" Height="23px" onclick="BtnExit_Click"></asp:Button>&nbsp;
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
									<TD style="FONT-SIZE: 5pt; WIDTH: 112px; HEIGHT: 62px" vAlign="middle" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table6" style="WIDTH: 802px; HEIGHT: 15px" cellSpacing="0" cellPadding="0" width="802"
											border="0">
											<TR>
												<TD style="FONT-SIZE: 1pt" align="center">
													<TABLE id="Table4" style="WIDTH: 796px; HEIGHT: 4px" cellSpacing="0" cellPadding="0" width="796"
														border="0">
														<TR>
															<TD style="WIDTH: 97px; HEIGHT: 1px">
																<asp:label id="lblPolicyID" Font-Size="Smaller" Font-Names="Tahoma" CSSCLASS="headfield1" WIDTH="104px"
																	HEIGHT="17" RUNAT="server">Line Of Business</asp:label></TD>
															<TD style="WIDTH: 162px; HEIGHT: 1px">
																<asp:dropdownlist id="ddlPolicyID" tabIndex="1" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	WIDTH="211px" HEIGHT="23px" RUNAT="server">
																	<asp:ListItem></asp:ListItem>
																	<asp:ListItem Value="E">Entry Date</asp:ListItem>
																	<asp:ListItem Value="C">Close Date</asp:ListItem>
																</asp:dropdownlist></TD>
															<TD style="WIDTH: 85px; HEIGHT: 1px">
																<asp:label id="Label1" Font-Size="Smaller" Font-Names="Tahoma" Width="88px" CSSCLASS="headfield1"
																	RUNAT="server">Effective Date</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 1px">
                                                            
                                                            <asp:TextBox ID="txtEffectiveDate" CSSCLASS="headfield1" tabIndex="7" runat="server" AutoPostBack="True" 
                                                            Font-Names="Tahoma" Font-Size="9pt"
                                                             WIDTH="103px" HEIGHT="21"></asp:TextBox>

                                                            
																<INPUT id="Button1" style="WIDTH: 24px; HEIGHT: 21px" onclick="javascript:calendar_window=window.open('selectDate.aspx?formname=document.commAgt.txtEffectiveDate','calendar_window','width=250,height=250');calendar_window.focus()"
																	tabIndex="8" type="button" value="..." name="btnCalendar" RUNAT="server" visible="False"><asp:ImageButton 
                                                                    ID="ImgCalendar" runat="server" 
                                ImageUrl="~/Images2/Calendar.png" TabIndex="15" Width="16px" Visible="False" />

                                                            </TD>
														</TR>
														<TR>
															<TD style="WIDTH: 97px; HEIGHT: 21px">
																<P>
																	<asp:label id="PolicyType" Font-Size="Smaller" Font-Names="Tahoma" CSSCLASS="headfield1" WIDTH="92px"
																		HEIGHT="17" RUNAT="server">Policy Type:</asp:label></P>
															</TD>
															<TD style="WIDTH: 162px; HEIGHT: 21px">
																<asp:textbox id="txtPolicyType" tabIndex="2" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	WIDTH="55px" HEIGHT="21" RUNAT="server" MAXLENGTH="10"></asp:textbox></TD>
															<TD style="WIDTH: 85px; HEIGHT: 21px">
																<asp:label id="lblEntryDate" Font-Size="Smaller" Font-Names="Tahoma" Width="80px" CSSCLASS="headfield1"
																	RUNAT="server">Entry Date</asp:label></TD>
															<TD style="FONT-SIZE: 2pt; WIDTH: 168px; HEIGHT: 21px">
																<maskedinput:maskedtextbox id="txtEntryDate" tabIndex="9" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	WIDTH="107px" HEIGHT="21" RUNAT="server" ISDATE="True" Enabled="False"></maskedinput:maskedtextbox></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 97px; HEIGHT: 1px">
																<asp:label id="lblInsurance" Font-Size="Smaller" Font-Names="Tahoma" CSSCLASS="headfield1"
																	WIDTH="92px" HEIGHT="17" RUNAT="server">Insurance Co.:</asp:label></TD>
															<TD style="WIDTH: 162px; HEIGHT: 1px">
																<asp:dropdownlist id="ddlInsuranceCompany" tabIndex="3" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	WIDTH="212px" HEIGHT="23px" RUNAT="server">
																	<asp:ListItem></asp:ListItem>
																	<asp:ListItem Value="E">Entry Date</asp:ListItem>
																	<asp:ListItem Value="C">Close Date</asp:ListItem>
																</asp:dropdownlist></TD>
															<TD style="WIDTH: 85px; HEIGHT: 1px">
																<asp:label id="lblAgentLevel" Font-Size="Smaller" Font-Names="Tahoma" Width="72px" CSSCLASS="headfield1"
																	RUNAT="server" Visible="False">Agent Level</asp:label></TD>
															<TD style="WIDTH: 168px; HEIGHT: 1px">
																<asp:textbox id="txtAgentLevel" tabIndex="10" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	WIDTH="63px" HEIGHT="21" RUNAT="server" MAXLENGTH="3" Visible="False">1</asp:textbox></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 97px; HEIGHT: 5px">
																<asp:label id="Label2" Font-Size="Smaller" Font-Names="Tahoma" CSSCLASS="headfield1" WIDTH="100px"
																	HEIGHT="17" RUNAT="server">Selected  Type:</asp:label></TD>
															<TD style="WIDTH: 162px; HEIGHT: 5px">
																<asp:radiobutton class="headform1 " id="RdbRate" tabIndex="4" runat="server" Font-Size="9pt" Font-Names="Tahoma"
																	CssClass="headform3" Width="36px" Text="%" BackColor="Transparent" Height="5px" AutoPostBack="True"
																	GroupName="SelectedType" Checked="True" oncheckedchanged="RdbRate_CheckedChanged"></asp:radiobutton>&nbsp;
																<asp:radiobutton class="headform1 " id="RdbCommissionAmount" tabIndex="5" runat="server" Font-Size="9pt"
																	Font-Names="Tahoma" CssClass="headform3" Width="48px" Text="Amt" BackColor="Transparent" Height="2px"
																	AutoPostBack="True" GroupName="SelectedType" oncheckedchanged="RdbCommissionAmount_CheckedChanged"></asp:radiobutton></TD>
															<TD style="WIDTH: 85px; HEIGHT: 5px"></TD>
															<TD style="WIDTH: 168px; HEIGHT: 5px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 97px; HEIGHT: 32px">
																<asp:label id="lblCommissionAmount" Font-Size="Smaller" Font-Names="Tahoma" CSSCLASS="headfield1"
																	WIDTH="112px" HEIGHT="17" RUNAT="server">Comm. Amount:</asp:label>
																<asp:label id="lblCommissionRate" RUNAT="server" HEIGHT="17px" WIDTH="68px" CSSCLASS="headfield1">Rate:</asp:label></TD>
															<TD style="WIDTH: 162px; HEIGHT: 32px">
																<asp:textbox id="txtCommissionAmount" tabIndex="6" Font-Size="9pt" Font-Names="Tahoma" CSSCLASS="headfield1"
																	WIDTH="101px" HEIGHT="21" RUNAT="server" MAXLENGTH="5"></asp:textbox>
																<asp:textbox id="txtRate" tabIndex="6" Font-Names="Tahoma" Font-Size="9pt" RUNAT="server" HEIGHT="21"
																	WIDTH="101px" CSSCLASS="headfield1" MAXLENGTH="5"></asp:textbox></TD>
															<TD style="WIDTH: 85px; HEIGHT: 32px"></TD>
															<TD style="WIDTH: 168px; HEIGHT: 32px"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 97px; HEIGHT: 2px"></TD>
															<TD style="WIDTH: 162px; HEIGHT: 2px"></TD>
															<TD style="WIDTH: 85px; HEIGHT: 2px"></TD>
															<TD style="WIDTH: 168px; HEIGHT: 2px"></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 5pt; WIDTH: 112px; HEIGHT: 4px" align="left"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 35px; HEIGHT: 27px">
										<asp:datagrid id="searchCommission" Height="151px" WIDTH="804px" RUNAT="server" BORDERSTYLE="Solid"
											BORDERWIDTH="1px" BORDERCOLOR="#D6E3EA" ITEMSTYLE-HORIZONTALALIGN="center" HEADERSTYLE-HORIZONTALALIGN="Center"
											BACKCOLOR="White" AUTOGENERATECOLUMNS="False" ALLOWPAGING="True" FONT-SIZE="9pt" FONT-NAMES="Tahoma"
											CELLPADDING="0" ALTERNATINGITEMSTYLE-CSSCLASS="HeadForm3" ALTERNATINGITEMSTYLE-BACKCOLOR="#FEFBF6"
											HEADERSTYLE-CSSCLASS="HeadForm2" HEADERSTYLE-BACKCOLOR="#5C8BAE" ITEMSTYLE-CSSCLASS="HeadForm3"
											PageSize="6">
											<FooterStyle ForeColor="MidnightBlue" BackColor="#99CCCC"></FooterStyle>
											<AlternatingItemStyle CssClass="HeadForm3" BackColor="#FEFBF6"></AlternatingItemStyle>
											<ItemStyle HorizontalAlign="Center" CssClass="HeadForm3"></ItemStyle>
											<HeaderStyle HorizontalAlign="Center" Height="10px" ForeColor="White" CssClass="HeadForm2" BackColor="MidnightBlue"></HeaderStyle>
											<Columns>
												<asp:ButtonColumn ButtonType="PushButton" HeaderText="Sel." CommandName="Select"></asp:ButtonColumn>
												<asp:BoundColumn Visible="False" DataField="CommissionAgentID" HeaderText="CommissionAgentID"></asp:BoundColumn>
												<asp:BoundColumn DataField="PolicyClassID" HeaderText="Line of Business"></asp:BoundColumn>
												<asp:BoundColumn DataField="AgentID" HeaderText="Agent ID"></asp:BoundColumn>
												<asp:BoundColumn DataField="PolicyType" HeaderText="Policy Type"></asp:BoundColumn>
												<asp:BoundColumn DataField="InsuranceCompanyID" HeaderText="Insurance Co."></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="BankID" HeaderText="Bank"></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="CompanyDealerID" HeaderText="Dealer"></asp:BoundColumn>
												<asp:BoundColumn DataField="CommissionRate" HeaderText="Comm. Rate"></asp:BoundColumn>
												<asp:BoundColumn DataField="CommissionAmount" HeaderText="Comm.Amount"></asp:BoundColumn>
												<asp:BoundColumn DataField="EffectiveDate" HeaderText="Effective Date"></asp:BoundColumn>
												<asp:BoundColumn DataField="CommissionEntryDate" HeaderText="Entry Date"></asp:BoundColumn>
												<asp:BoundColumn DataField="AgentLevel" HeaderText="Agent Level"></asp:BoundColumn>
                                                <asp:ButtonColumn ButtonType="PushButton" HeaderText="Del." CommandName="Delete"></asp:ButtonColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Left" ForeColor="White" BackColor="MidnightBlue" PageButtonCount="20"
												CssClass="Numbers" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
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
			<asp:imagebutton id="BtnExit1" tabIndex="17" RUNAT="server" CAUSESVALIDATION="False" IMAGEURL="Images/exit_btn.gif"
				style="Z-INDEX: 129; LEFT: 126px; POSITION: absolute; TOP: 602px" Visible="False"></asp:imagebutton>
			<asp:ImageButton id="btnAuditTrail1" runat="server" Width="48px" ImageUrl="Images/History_btn.bmp"
				Height="19px" style="Z-INDEX: 130; LEFT: 158px; POSITION: absolute; TOP: 602px" Visible="False"></asp:ImageButton>
			<asp:imagebutton id="btnPrint1" tabIndex="15" runat="server" ImageUrl="Images/printreport_btn.gif"
				AlternateText="Print Report" style="Z-INDEX: 131; LEFT: 214px; POSITION: absolute; TOP: 602px"
				Visible="False"></asp:imagebutton>
			<asp:imagebutton id="cmdCancel1" tabIndex="14" runat="server" ImageUrl="Images/cancel_btn.gif" CausesValidation="False"
				style="Z-INDEX: 132; LEFT: 310px; POSITION: absolute; TOP: 602px" Visible="False"></asp:imagebutton>
			<asp:imagebutton id="BtnSave1" tabIndex="13" RUNAT="server" CAUSESVALIDATION="False" TOOLTIP="Save Person Information"
				IMAGEURL="Images/save_btn.gif" style="Z-INDEX: 133; LEFT: 338px; POSITION: absolute; TOP: 602px"
				Visible="False"></asp:imagebutton>
			<asp:imagebutton id="btnEdit1" tabIndex="12" runat="server" ImageUrl="Images/edit_btn.GIF" CausesValidation="False"
				style="Z-INDEX: 134; LEFT: 370px; POSITION: absolute; TOP: 602px" Visible="False"></asp:imagebutton>
			<asp:imagebutton id="btnAddNew1" tabIndex="11" runat="server" ImageUrl="Images/addNew_btn.gif" CausesValidation="False"
				style="Z-INDEX: 135; LEFT: 402px; POSITION: absolute; TOP: 602px" Visible="False"></asp:imagebutton>
			<asp:literal id="litPopUp" RUNAT="server" VISIBLE="False"></asp:literal></FORM>
	</BODY>
    <script>
        $(document).ready(function () {
            //$('#TxtBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
//            $('#TxtEffBinderDate').mask('00/00/0000', { placeholder: '__/__/____' });
//            $('#TxtExpBinderDate').mask('00/00/0000', { placeholder: '__/__/____' });
//            $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
//            $('#TxtWorkPhone').mask('(000) 000-0000', { placeholder: '(###) ###-####' });
//            $('#TxtCellPhone').mask('(000) 000-0000', { placeholder: '(###) ###-####' });
//            $('#TxtDriverGrade').mask('0.00', { placeholder: '#.##' });

            $('#txtEffectiveDate').mask('AB/CD/EFGH', { 'translation': {
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
    </script>
</HTML>
