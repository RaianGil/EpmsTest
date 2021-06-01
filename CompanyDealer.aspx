<%@ Page language="c#" Inherits="EPolicy.CompanyDealer" CodeFile="CompanyDealer.aspx.cs" %>
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
	<body background="Images2/SQUARE.GIF">
		<form method="post" runat="server">
			<asp:panel id="Panel1" style="Z-INDEX: 101; LEFT: 140px; POSITION: absolute; TOP: 128px" runat="server"
				Height="454px" Wrap="False" BorderStyle="None" Width="983px" BackColor="White">
				<P>&nbsp;</P>
				<P>
					<TABLE id="Table9" style="WIDTH: 928px; HEIGHT: 19px" cellSpacing="0" cellPadding="0" width="928"
						align="center" bgColor="midnightblue" border="0" RUNAT="server">
						<TR>
							<TD width="100%">
								<P>&nbsp;</P>
							</TD>
						</TR>
					</TABLE>
				</P>
				<P>&nbsp;</P>
				<P>&nbsp;</P>
				<P>&nbsp;</P>
				<P>&nbsp;</P>
				<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				</P>
				<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				</P>
				<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				</P>
			</asp:panel>
            &nbsp;
			<asp:checkbox id="ChkCars" style="Z-INDEX: 135; LEFT: 880px; POSITION: absolute; TOP: 259px" tabIndex="11"
				runat="server" Width="76px" CssClass="headfield1" TextAlign="Left" AutoPostBack="True" Text="Cars Plan"
				Font-Size="Smaller" Font-Names="Tahoma" Visible="False"></asp:checkbox>
			<asp:checkbox id="ChkRfloor" style="Z-INDEX: 134; LEFT: 828px; POSITION: absolute; TOP: 227px"
				tabIndex="11" runat="server" Width="128px" Text="Reliable Floor Plan" AutoPostBack="True"
				TextAlign="Left" CssClass="headfield1" Font-Size="Smaller" Font-Names="Tahoma" Visible="False"></asp:checkbox><asp:label id="lblCity" style="Z-INDEX: 133; LEFT: 444px; POSITION: absolute; TOP: 448px" CSSCLASS="headfield1"
				RUNAT="server" Font-Size="Smaller" Font-Names="Tahoma">City</asp:label><asp:label id="lblState" style="Z-INDEX: 129; LEFT: 444px; POSITION: absolute; TOP: 420px"
				CSSCLASS="headfield1" RUNAT="server" Font-Size="Smaller" Font-Names="Tahoma">State</asp:label>
            <asp:Label ID="Label4" runat="server" CssClass="headfield1" Font-Names="Tahoma" Font-Size="Smaller"
                Style="z-index: 129; left: 788px; position: absolute; top: 420px" Width="84px">Profit Premier:</asp:Label>
            <asp:Label ID="Label5" runat="server" CssClass="headfield1" Font-Names="Tahoma" Font-Size="Smaller"
                Style="z-index: 129; left: 812px; position: absolute; top: 452px" Width="36px">Concurso:</asp:Label>
            <asp:Label ID="Label7" runat="server" CssClass="headfield1" Font-Names="Tahoma" Font-Size="Smaller"
                Style="z-index: 129; left: 804px; position: absolute; top: 480px" Width="36px">ProfitDealer</asp:Label>
            <asp:Label ID="Label6" runat="server" CssClass="headfield1" Font-Names="Tahoma" Font-Size="Smaller"
                Height="12px" Style="z-index: 129; left: 796px; position: absolute; top: 512px"
                Width="76px">Master Gap:</asp:Label>
            <asp:label id="lblZipCode" style="Z-INDEX: 130; LEFT: 444px; POSITION: absolute; TOP: 480px"
				Width="68px" CSSCLASS="headfield1" RUNAT="server" Font-Size="Smaller" Font-Names="Tahoma">Zip Code</asp:label><maskedinput:maskedtextbox id="txtZipCode" style="Z-INDEX: 131; LEFT: 572px; POSITION: absolute; TOP: 480px"
				tabIndex="9" CSSCLASS="headfield1" RUNAT="server" MASK="99999Z9999" ISDATE="False" MAXLENGTH="10" WIDTH="144px" HEIGHT="19px"></maskedinput:maskedtextbox><asp:textbox id="txtSt" style="Z-INDEX: 132; LEFT: 572px; POSITION: absolute; TOP: 416px" tabIndex="7"
				CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="2" WIDTH="144px" HEIGHT="21px"></asp:textbox>
            <asp:TextBox ID="TxtProfit" runat="server" CssClass="headfield1" Height="21px" MaxLength="7"
                Style="z-index: 132; left: 884px; position: absolute; top: 416px" TabIndex="7"
                Width="144px"></asp:TextBox>
            <asp:TextBox ID="TxtConcurso" runat="server" CssClass="headfield1" Height="21px"
                MaxLength="7" Style="z-index: 132; left: 884px; position: absolute; top: 448px"
                TabIndex="7" Width="144px"></asp:TextBox>
            <asp:TextBox ID="TxtProfitDealer" runat="server" CssClass="headfield1" Height="21px" MaxLength="7"
                Style="z-index: 132; left: 884px; position: absolute; top: 476px" TabIndex="7"
                Width="144px"></asp:TextBox>
            <asp:TextBox ID="TxtMasterGap" runat="server" CssClass="headfield1" Height="21px"
                MaxLength="10" Style="z-index: 132; left: 884px; position: absolute; top: 508px"
                TabIndex="7" Width="144px"></asp:TextBox>
            <asp:textbox id="txtCity" style="Z-INDEX: 126; LEFT: 572px; POSITION: absolute; TOP: 448px" tabIndex="8"
				CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="14" WIDTH="144px" HEIGHT="21"></asp:textbox><asp:label id="lblAdd2" style="Z-INDEX: 128; LEFT: 444px; POSITION: absolute; TOP: 385px" CSSCLASS="headfield1"
				RUNAT="server" HEIGHT="19px" ForeColor="Black" Font-Size="Smaller" Font-Names="Tahoma">Address 2</asp:label><asp:textbox id="txtAddress2" style="Z-INDEX: 127; LEFT: 572px; POSITION: absolute; TOP: 384px"
				tabIndex="6" CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="20" WIDTH="249px" HEIGHT="21"></asp:textbox><asp:label id="lbladd1" style="Z-INDEX: 125; LEFT: 444px; POSITION: absolute; TOP: 357px" CSSCLASS="headfield1"
				RUNAT="server" HEIGHT="19px" ForeColor="Black" Font-Size="Smaller" Font-Names="Tahoma">Address 1</asp:label><asp:textbox id="txtAddress1" style="Z-INDEX: 123; LEFT: 572px; POSITION: absolute; TOP: 356px"
				tabIndex="5" CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="20" WIDTH="249px" HEIGHT="21"></asp:textbox><asp:label id="lblmbi" style="Z-INDEX: 124; LEFT: 220px; POSITION: absolute; TOP: 360px" Width="57px"
				CSSCLASS="headfield1" RUNAT="server" HEIGHT="19px" ForeColor="Black" Font-Size="Smaller" Font-Names="Tahoma" Visible="False">Mgrp mbi</asp:label><asp:textbox id="txtmbi" style="Z-INDEX: 122; LEFT: 288px; POSITION: absolute; TOP: 356px" tabIndex="15"
				CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="2" WIDTH="30px" HEIGHT="21" Visible="False"></asp:textbox><asp:checkbox id="chkTriangle" style="Z-INDEX: 121; LEFT: 220px; POSITION: absolute; TOP: 259px"
				tabIndex="12" runat="server" Width="123px" CssClass="headfield1" TextAlign="Left" AutoPostBack="True" Text="Triangle Dealers" Font-Size="Smaller" Font-Names="Tahoma" Visible="False"></asp:checkbox><asp:checkbox id="chkEas" style="Z-INDEX: 120; LEFT: 836px; POSITION: absolute; TOP: 291px" tabIndex="12"
				runat="server" Width="120px" CssClass="headfield1" TextAlign="Left" Text="Special VSC Rate" Font-Size="Smaller" Font-Names="Tahoma"></asp:checkbox><asp:checkbox id="chkBaldrich" style="Z-INDEX: 119; LEFT: 838px; POSITION: absolute; TOP: 315px"
				tabIndex="11" runat="server" Width="118px" CssClass="headfield1" TextAlign="Left" Text="Is Cabrera Group" Font-Size="Smaller" Font-Names="Tahoma"></asp:checkbox><asp:label id="lblAuto" style="Z-INDEX: 118; LEFT: 220px; POSITION: absolute; TOP: 331px" Width="60px"
				CSSCLASS="headfield1" RUNAT="server" HEIGHT="1px" ForeColor="Black" Font-Size="Smaller" Font-Names="Tahoma" Visible="False">Mgrp Auto</asp:label><asp:textbox id="txtAuto" style="Z-INDEX: 116; LEFT: 288px; POSITION: absolute; TOP: 324px" tabIndex="14"
				CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="2" WIDTH="30px" HEIGHT="21" Visible="False"></asp:textbox><asp:label id="lblName" style="Z-INDEX: 117; LEFT: 444px; POSITION: absolute; TOP: 325px" CSSCLASS="headfield1"
				RUNAT="server" HEIGHT="19px" ForeColor="Black" Font-Size="Smaller" Font-Names="Tahoma">Dealer Resume Name:</asp:label><asp:textbox id="txtName" style="Z-INDEX: 115; LEFT: 572px; POSITION: absolute; TOP: 324px" tabIndex="4"
				CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="30" WIDTH="249px" HEIGHT="21"></asp:textbox><asp:label id="lblCoresu" style="Z-INDEX: 114; LEFT: 444px; POSITION: absolute; TOP: 290px"
				CSSCLASS="headfield1" RUNAT="server" HEIGHT="19px" ForeColor="Black" Font-Size="Smaller" Font-Names="Tahoma">Dealer Coresu:</asp:label><asp:textbox id="txtCoresu" style="Z-INDEX: 110; LEFT: 572px; POSITION: absolute; TOP: 288px"
				tabIndex="3" CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="3" WIDTH="249px" HEIGHT="21"></asp:textbox><asp:label id="lblEntryDate" style="Z-INDEX: 113; LEFT: 444px; POSITION: absolute; TOP: 512px"
				CSSCLASS="headfield1" RUNAT="server" Font-Size="Smaller" Font-Names="Tahoma">Entry Date</asp:label>
            <asp:Label ID="Label2" runat="server" CssClass="headfield1" Font-Names="Tahoma" Font-Size="Smaller"
                Style="z-index: 113; left: 444px; position: absolute; top: 540px">VSC Client ID:</asp:Label>
            <maskedinput:maskedtextbox id="txtEntryDate" style="Z-INDEX: 112; LEFT: 572px; POSITION: absolute; TOP: 512px"
				tabIndex="10" CSSCLASS="headfield1" RUNAT="server" ISDATE="True" WIDTH="144px" HEIGHT="19px"></maskedinput:maskedtextbox>
            &nbsp;
            <asp:label id="lblCode" style="Z-INDEX: 109; LEFT: 444px; POSITION: absolute; TOP: 256px" CSSCLASS="headfield1"
				RUNAT="server" HEIGHT="19px" ForeColor="Black" Font-Size="Smaller" Font-Names="Tahoma">Universal Code:</asp:label><asp:textbox id="txtCode" style="Z-INDEX: 111; LEFT: 572px; POSITION: absolute; TOP: 256px" tabIndex="2"
				CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="3" WIDTH="249px" HEIGHT="21"></asp:textbox>
			<TABLE class="headForm1 " id="Table3" style="Z-INDEX: 105; LEFT: 156px; WIDTH: 967px; POSITION: absolute; TOP: 132px; HEIGHT: 31px"
				height="31" cellSpacing="0" cellPadding="0" width="967" align="center" border="0">
				<TR>
					<TD style="WIDTH: 413px" height="1">
						<P>&nbsp;
							<asp:Label id="Label1" runat="server" CssClass="headForm1 " ForeColor="Navy" Font-Size="11pt"
								Font-Bold="True" Font-Names="tahoma,11pt">Company Dealer ID:</asp:Label>
							<asp:label id="lblCompanyDealerID" runat="server" Width="84px"></asp:label></P>
					</TD>
					<TD style="WIDTH: 503px" height="1" align="right">
						<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:Button id="btnAddNew" runat="server" Width="64px" BorderWidth="0px" BorderColor="MidnightBlue"
								Height="23px" Text="AddNew" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" onclick="btnAddNew_Click"></asp:Button>
							<asp:Button id="btnEdit" runat="server" Width="46px" BorderWidth="0px" BorderColor="MidnightBlue"
								Height="23px" Text="Edit" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" onclick="btnEdit_Click"></asp:Button>
							<asp:Button id="BtnSave" runat="server" Width="46px" BorderWidth="0px" BorderColor="MidnightBlue"
								Height="23px" Text="Save" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" onclick="BtnSave_Click"></asp:Button>
							<asp:Button id="btnSearch" runat="server" Width="46px" BorderWidth="0px" BorderColor="MidnightBlue"
								Height="23px" Text="Search" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" onclick="btnSearch_Click"></asp:Button>
							<asp:Button id="cmdCancel" runat="server" Width="46px" BorderWidth="0px" BorderColor="MidnightBlue"
								Height="23px" Text="Cancel" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" onclick="cmdCancel_Click"></asp:Button>
							<asp:Button id="btnPrint" runat="server" Width="46px" BorderWidth="0px" BorderColor="MidnightBlue"
								Height="23px" Text="Print" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" onclick="btnPrint_Click"></asp:Button>
							<asp:Button id="btnAuditTrail" runat="server" Width="59px" BorderWidth="0px" BorderColor="MidnightBlue"
								Height="23px" Text="AuditTrail" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" onclick="btnAuditTrail_Click"></asp:Button>
							<asp:Button id="BtnExit" runat="server" Width="46px" BorderWidth="0px" BorderColor="MidnightBlue"
								Height="23px" Text="Exit" ForeColor="White" BackColor="MidnightBlue" Font-Names="Tahoma" onclick="BtnExit_Click"></asp:Button></P>
					</TD>
				</TR>
			</TABLE>
			<maskedinput:maskedtextheader id="MaskedTextHeader1" runat="server"></maskedinput:maskedtextheader>
			<TABLE id="Table2" style="Z-INDEX: 102; LEFT: 220px; WIDTH: 795px; POSITION: absolute; TOP: 168px; HEIGHT: 4px"
				height="4" cellSpacing="0" cellPadding="0" width="795" align="center" bgColor="#3f7b9f"
				border="0">
				<TR>
				</TR>
			</TABLE>
			<asp:label id="Label3" style="Z-INDEX: 106; LEFT: 424px; POSITION: absolute; TOP: 224px" Width="5px"
				CSSCLASS="headfield1" RUNAT="server" HEIGHT="19px" ForeColor="OrangeRed">*</asp:label><asp:label id="lblB" style="Z-INDEX: 107; LEFT: 444px; POSITION: absolute; TOP: 224px" CSSCLASS="headfield1"
				RUNAT="server" HEIGHT="19px" ForeColor="Black" Font-Size="Smaller" Font-Names="Tahoma">Dealer Name:</asp:label>
            <asp:Label ID="Label8" runat="server" CssClass="headfield1" Font-Names="Tahoma" Font-Size="Smaller"
                ForeColor="Black" Height="19px" Style="z-index: 107; left: 444px; position: absolute;
                top: 196px">Dealer Code:</asp:Label>
            <asp:TextBox ID="txtCompanyDealerDescription" runat="server" CssClass="headfield1"
                Height="21" MaxLength="30" Style="z-index: 108; left: 572px; position: absolute;
                top: 224px" TabIndex="1" Width="249px"></asp:TextBox>
            <asp:textbox id="txtDealerCode" style="Z-INDEX: 108; LEFT: 572px; POSITION: absolute; TOP: 196px"
				tabIndex="1" CSSCLASS="headfield1" RUNAT="server" MAXLENGTH="30" WIDTH="80px" HEIGHT="21"></asp:textbox><asp:imagebutton id="btnAddNew1" tabIndex="16" runat="server" CausesValidation="False" ImageUrl="Images/addNew_btn.gif"
				ToolTip="Add New" style="Z-INDEX: 136; LEFT: 736px; POSITION: absolute; TOP: 12px" Visible="False"></asp:imagebutton>
			<asp:imagebutton id="btnEdit1" tabIndex="17" runat="server" CausesValidation="False" ImageUrl="Images/edit_btn.GIF"
				ToolTip="Edit" style="Z-INDEX: 137; LEFT: 772px; POSITION: absolute; TOP: 12px" Visible="False"></asp:imagebutton>
			<asp:imagebutton id="BtnSave1" tabIndex="18" RUNAT="server" IMAGEURL="Images/save_btn.gif" TOOLTIP="Save Person Information"
				CAUSESVALIDATION="False" style="Z-INDEX: 138; LEFT: 812px; POSITION: absolute; TOP: 12px" Visible="False"></asp:imagebutton>
			<asp:imagebutton id="btnSearch1" tabIndex="19" runat="server" CausesValidation="False" ImageUrl="Images/search_btn.gif"
				ToolTip="Search" style="Z-INDEX: 139; LEFT: 848px; POSITION: absolute; TOP: 12px" Visible="False"></asp:imagebutton><asp:imagebutton id="cmdCancel1" tabIndex="20" runat="server" CausesValidation="False" ImageUrl="Images/cancel_btn.gif"
				ToolTip="Cancel" style="Z-INDEX: 140; LEFT: 888px; POSITION: absolute; TOP: 12px" Visible="False"></asp:imagebutton>
			<asp:imagebutton id="btnPrint1" tabIndex="7" runat="server" ImageUrl="Images/printreport_btn.gif"
				AlternateText="Print Report" ToolTip="Print Report" style="Z-INDEX: 141; LEFT: 924px; POSITION: absolute; TOP: 12px"
				Visible="False"></asp:imagebutton>
			<asp:ImageButton id="btnAuditTrail1" runat="server" Width="48px" Height="19px" ImageUrl="Images/History_btn.bmp"
				style="Z-INDEX: 142; LEFT: 1024px; POSITION: absolute; TOP: 16px" Visible="False"></asp:ImageButton>
			<asp:imagebutton id="BtnExit1" tabIndex="21" RUNAT="server" IMAGEURL="Images/exit_btn.gif" CAUSESVALIDATION="False"
				ToolTip="Exit" style="Z-INDEX: 143; LEFT: 1084px; POSITION: absolute; TOP: 16px" Visible="False"></asp:imagebutton><asp:button id="BtnEnd" tabIndex="24" runat="server" Height="23px" Text=">>" style="Z-INDEX: 144; LEFT: 748px; POSITION: absolute; TOP: 48px"
				Visible="False" onclick="BtnEnd_Click"></asp:button><asp:button id="BtnNext" tabIndex="23" runat="server" Height="23px" Width="24px" Text=">" style="Z-INDEX: 145; LEFT: 780px; POSITION: absolute; TOP: 48px"
				Visible="False" onclick="BtnNext_Click"></asp:button><asp:label id="lblRecordCount" CSSCLASS="headform2" RUNAT="server" WIDTH="129px" HEIGHT="17px"
				style="Z-INDEX: 146; LEFT: 808px; POSITION: absolute; TOP: 48px" Visible="False">1 of 1</asp:label><asp:button id="BtnPrevious" tabIndex="22" runat="server" Height="23px" Width="24px" Text="<"
				style="Z-INDEX: 147; LEFT: 940px; POSITION: absolute; TOP: 48px" Visible="False" onclick="BtnPrevious_Click"></asp:button><asp:button id="BtnBegin" tabIndex="21" runat="server" Height="23px" Text="<<" style="Z-INDEX: 148; LEFT: 968px; POSITION: absolute; TOP: 48px"
				Visible="False" onclick="BtnBegin_Click"></asp:button>
			<asp:literal id="litPopUp" RUNAT="server" VISIBLE="False"></asp:literal>
            <asp:TextBox ID="TXTVSCClientID" runat="server" CssClass="headfield1" Height="21" MaxLength="20"
                Style="z-index: 127; left: 572px; position: absolute; top: 536px" TabIndex="6"
                Width="144px"></asp:TextBox>
            <table id="Table4" align="center" bgcolor="white" border="0" cellpadding="0" style="left: 140px;
                width: 858px; position: absolute; top: 8px; height: 122px" width="858">
                <tr>
                    <td style="font-size: 0pt; width: 980px; height: 50px">
                        <table id="Table1" border="0" cellpadding="0" cellspacing="0" style="width: 979px;
                            height: 5px" width="979">
                            <tr>
                                <td style="width: 868px; height: 62px">
                                    <p>
                                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                        <img src="Images2/OptimaLogo.bmp" style="width: 220px; height: 64px" />
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</p>
                                </td>
                                <td style="width: 868px; height: 62px">
                                    <asp:Image ID="Img1" runat="server" ImageUrl="Images2/epmsLogo2.gif" Visible="False" /></td>
                            </tr>
                            <tr>
                                <td background="Images2/Barra3.gif" colspan="2" style="width: 732px; height: 33px">
                                    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                                    <asp:Label ID="lblUserName" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="White"
                                        Width="218px"></asp:Label>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                    <asp:Label ID="LblDate" runat="server" Font-Names="arial" Font-Size="9pt" ForeColor="White"
                                        Height="11px" Width="237px"></asp:Label>
                                    &nbsp; &nbsp;&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </form>
	</body>
</HTML>
