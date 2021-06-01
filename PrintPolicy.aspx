<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Page language="c#" Inherits="EPolicy.PrintPolicy" CodeFile="PrintPolicy.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ePMS | electronic Policy Manager Solution</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="epolicy.css" type="text/css" rel="stylesheet">
<SCRIPT LANGUAGE=Javascript>
<!--

function OnClear() 
{
   SigPlus1.ClearTablet(); //Clears the signature, in case of error or mistake
}

function OnCancel() 
{
   SigPlus1.TabletState = 0; //Turns tablet off
}

function OnSign() 
{
    SigPlus1.TabletState = 1; //Turns tablet on
}

function OnSave() 
{
    SigPlus1.TabletState = 0; //Turns tablet off
    SigPlus1.SigCompressionMode = 1; //Compresses the signature at a 2.5 to 1 ratio, making it smaller...to display the signature again later, you WILL HAVE TO set the SigCompressionMode of the new SigPlus object = 1, also
    document.PrintPolicy.hiddenSigString.value = SigPlus1.SigString;
    
    alert("The signature you have taken is the following data: " + SigPlus1.SigString);
    
    alert("Test: " +document.PrintPolicy.hiddenSigString.value);
    //The signature is now taken, and you may access it using the SigString property of SigPlus. 
    //This SigString is the actual signature, in ASCII format. 
    //You may pass this string value like you would any other String. 
    //To display the signature again, you simply pass this String back to the SigString property of SigPlus 
    //(BE SURE TO SET SigCompressionMode=1 PRIOR TO REASSIGNING THE SigString)
}

//-->
</SCRIPT>

	</HEAD>
	<body bottomMargin="0" leftMargin="0" background="Images2/SQUARE.GIF"
		topMargin="19" rightMargin="0">
		<form name="PrintPolicy" method="post" runat="server">
			<TABLE id="Table8" style="Z-INDEX: 153; LEFT: 4px; WIDTH: 911px; POSITION: static; TOP: 4px; HEIGHT: 526px"
				cellSpacing="0" cellPadding="0" width="911" align="center" bgColor="white" dataPageSize="25"
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
							<TABLE id="Table9" style="WIDTH: 809px; HEIGHT: 451px" cellSpacing="0" cellPadding="0"
								width="809" bgColor="white" border="0">
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="center"></TD>
								</TR>
								<TR>
									<TD style="FONT-SIZE: 0pt; WIDTH: 6px; HEIGHT: 3px" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table10" style="WIDTH: 808px; HEIGHT: 12px" cellSpacing="0" cellPadding="0"
											width="808" border="0">
											<TR>
												<TD align="left">&nbsp;
													<asp:Label id="Label17" runat="server" CssClass="headForm1 " Height="16px" Width="212px" Font-Names="Tahoma"
														Font-Size="11pt" ForeColor="Navy" Font-Bold="True">Print Policy:</asp:Label></TD>
												<TD></TD>
												<TD align="right">
                                                    <input id="hiddenSigString" runat="server" enableviewstate="true" name="ConfirmDialogBoxPopUp"
                                                        size="1" style="z-index: 102; left: 704px; width: 35px; position: absolute; top: 44px;
                                                        height: 22px" type="hidden" />
                                                    &nbsp;
                                                    &nbsp;&nbsp;
                                                    <asp:Button ID="btnSign" runat="server" BackColor="MidnightBlue" BorderColor="MidnightBlue"
                                                        BorderWidth="0px" Font-Names="Tahoma" ForeColor="White" Height="23px" OnClick="btnSign_Click"
                                                        TabIndex="45" Text="Sign" Width="72px" />
													<asp:Button id="PrintPolicies" runat="server" BorderWidth="0px" BorderColor="MidnightBlue" Height="23px"
														Width="51px" Text="Print" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" BackColor="MidnightBlue" onclick="PrintPolicies_Click"></asp:Button>
													<asp:Button id="BtnBack" runat="server" BorderWidth="0px" BorderColor="MidnightBlue" Height="23px"
														Width="51px" Text="Exit" Font-Names="Tahoma" Font-Size="9pt" ForeColor="White" BackColor="MidnightBlue" onclick="BtnBack_Click"></asp:Button>&nbsp;
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
									<TD style="FONT-SIZE: 2pt; WIDTH: 112px; HEIGHT: 470px" vAlign="middle" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<TABLE id="Table6" style="WIDTH: 807px; HEIGHT: 416px" cellSpacing="0" cellPadding="0"
											width="807" border="0">
											<TR>
												<TD style="FONT-SIZE: 1pt" align="center">
                                                    &nbsp; &nbsp;&nbsp; &nbsp;
													<TABLE id="Table1" style="WIDTH: 789px; HEIGHT: 124px" cellSpacing="0" cellPadding="0"
														width="789" border="0">
														<TR>
															<TD style="WIDTH: 83px; HEIGHT: 27px"></TD>
															<TD style="WIDTH: 202px; HEIGHT: 27px" align="left">
																<asp:label id="Label4" runat="server" Width="127px" CssClass="headform1" Font-Names="Tahoma"
																	Font-Size="9pt" ForeColor="Black">Policy & Document List</asp:label></TD>
															<TD style="WIDTH: 252px; HEIGHT: 27px">
																<asp:label id="Label9" runat="server" Width="60px" CssClass="headform1" Font-Names="Tahoma"
																	Font-Size="9pt" ForeColor="Black">Copy List:</asp:label></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 83px; HEIGHT: 27px"></TD>
															<TD style="WIDTH: 202px; HEIGHT: 27px" align="left">
																<asp:CheckBoxList id="ChkPolicyList" runat="server" CssClass="headfield1" RepeatLayout="Flow" BorderStyle="Solid"
																	BorderWidth="1px" BorderColor="Navy" Height="353px" Width="330px" Font-Names="Tahoma" Font-Size="9pt"></asp:CheckBoxList></TD>
															<TD style="WIDTH: 252px; HEIGHT: 27px">
																<P>
																	<asp:CheckBoxList id="ChkCopyList" runat="server" CssClass="headfield1" RepeatLayout="Flow" BorderStyle="Solid"
																		BorderWidth="1px" BorderColor="Navy" Height="161px" Width="316px" Font-Names="Tahoma" Font-Size="9pt"></asp:CheckBoxList></P>
																<P>
																	<asp:label id="Label1" runat="server" Width="139px" Height="1px" CssClass="headform1" Font-Names="Tahoma"
																		Font-Size="9pt" ForeColor="Black">Print Only One Copy of:</asp:label>&nbsp;</P>
                                                                <p>
                                                                    <asp:HyperLink ID="hplAgreements" runat="server" NavigateUrl="Images2\VSCAgreements.pdf"
                                                                        Target="_blank" Visible="False">Print Agreements</asp:HyperLink>
																	<asp:CheckBoxList id="ChkPrintJustOne" runat="server" Width="316px" Height="161px" BorderColor="Navy"
																		BorderWidth="1px" BorderStyle="Solid" RepeatLayout="Flow" CssClass="headfield1" Font-Names="Tahoma"
																		Font-Size="9pt"></asp:CheckBoxList></p>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
										<P>&nbsp;</P>
									</TD>
								</TR>
							</TABLE>
						</P>
						<P>&nbsp;</P>
					</TD>
				</TR>
			</TABLE>
			<maskedinput:maskedtextheader id="MaskedTextHeader1" RUNAT="server"></maskedinput:maskedtextheader>
			<asp:literal id="litPopUp" RUNAT="server" VISIBLE="False"></asp:literal>
			<P>&nbsp;</P>
		</form>
	</body>
</HTML>
