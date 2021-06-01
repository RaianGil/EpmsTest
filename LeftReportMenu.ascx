<%@ Control Language="c#" Inherits="EPolicy.EPolicyWeb.Components.LeftReportMenu" CodeFile="LeftReportMenu.ascx.cs" %>
<TABLE id="Table1" style="WIDTH: 169px; HEIGHT: 523px" borderColor="#003366" height="523"
	cellSpacing="0" cellPadding="0" width="169" align="left" border="1" runat="server">
	<TR>
		<TD style="FONT-SIZE: 0pt" vAlign="top" align="left" height="1">
			<TABLE id="Table3" style="WIDTH: 164px; HEIGHT: 295px" height="295" cellSpacing="1" cellPadding="1"
				width="164" bgColor="white" border="0">
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 25px">
						<P>
							<asp:label id="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Height="12px" Width="113px"
								Font-Size="9pt" ForeColor="Navy">REPORT MENU</asp:label>
							<asp:textbox id="Textbox3" runat="server" Height="1px" Width="158px" ForeColor="#000040" BorderColor="#000040"
								BorderStyle="Solid"></asp:textbox></P>
					</TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 4px" colSpan="1" rowSpan="1">
						<P align="center">
							<asp:Button id="btnProspectReport" runat="server" Font-Names="Tahoma" Height="19px" Width="147px"
								ForeColor="White" BorderColor="DodgerBlue" BorderStyle="None" BorderWidth="0px" BackColor="DodgerBlue"
								Text="Prospect Reports" onclick="Button8_Click"></asp:Button></P>
					</TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 11px" align="center">
						<P align="center">
							<asp:Button id="btnClientReport" runat="server" Font-Names="Tahoma" Height="19px" Width="147px"
								ForeColor="White" BorderColor="DodgerBlue" BorderStyle="None" BorderWidth="0px" BackColor="DodgerBlue"
								Text="Client Reports" onclick="Button10_Click"></asp:Button></P>
					</TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 2px" align="center">
						<P>
							<asp:Button id="btnQueriesGroupReport" runat="server" Font-Names="Tahoma" Height="19px" Width="147px"
								ForeColor="White" BorderColor="DodgerBlue" BorderStyle="None" BorderWidth="0px" BackColor="DodgerBlue"
								Text="Policies Reports" onclick="Button11_Click"></asp:Button></P>
					</TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 2px" align="center">
						<asp:Button id="btnPoliciesReports" runat="server" ForeColor="White" Width="147px" Height="19px"
							Font-Names="Tahoma" BorderStyle="None" BorderColor="DodgerBlue" Text="Reports Dealer"
							BackColor="DodgerBlue" BorderWidth="0px" onclick="btnPoliciesReports_Click"></asp:Button></TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 2px" align="center">
						<P>
							<asp:Button id="btnPaymentReports" runat="server" Font-Names="Tahoma" Height="19px" Width="147px"
								ForeColor="White" BorderColor="DodgerBlue" BorderStyle="None" BorderWidth="0px" BackColor="DodgerBlue"
								Text="Payment Reports" onclick="Button12_Click"></asp:Button></P>
					</TD>
				</TR>
                <TR>
					<TD class="menuOptionsHead" style="HEIGHT: 18px" align="center">
                        <asp:Button ID="btnAdjustmentReports" runat="server" BackColor="DodgerBlue" BorderColor="DodgerBlue"
                            BorderStyle="None" BorderWidth="0px" Font-Names="Tahoma" ForeColor="White" Text="Adjustment Reports"
                            Width="147px" OnClick="btnAdjustmentReports_Click" /></TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 2px" align="center">
						<P align="center">
							<asp:Button id="btnCommissionReport" runat="server" ForeColor="White" Width="147px" Height="19px"
								Font-Names="Tahoma" BorderStyle="None" BorderColor="DodgerBlue" Text="Commission Report"
								BackColor="DodgerBlue" BorderWidth="0px" onclick="Button1_Click"></asp:Button></P>
					</TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 1px" align="center">
                        <asp:Button ID="btnInactiveUsers" runat="server" BackColor="DodgerBlue" BorderColor="DodgerBlue"
                            BorderStyle="None" BorderWidth="0px" Font-Names="Tahoma" ForeColor="White" Text="Inactive Users"
                            Width="147px" OnClick="btnInactiveUsers_Click" /></TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 21px" align="left">
						<P align="center">
							<asp:Button id="btnIncentiveReport" runat="server" ForeColor="White" Width="147px" Height="19px"
								Font-Names="Tahoma" BorderStyle="None" BorderColor="DodgerBlue" Text="Incentive Report"
								BackColor="DodgerBlue" BorderWidth="0px" onclick="Button2_Click"></asp:Button></P>
					</TD>
				</TR>
                <tr>
                    <td align="center" class="menuOptionsHead" style="height: 18px">
                        <asp:Button ID="btnAccountingSummary" runat="server" BackColor="DodgerBlue" BorderColor="DodgerBlue"
                            BorderStyle="None" BorderWidth="0px" Font-Names="Tahoma" ForeColor="White" Height="19px"
                            OnClick="btnAccountingSummary_Click" Text="Accounting Summary" Width="147px" /></td>
                </tr>
                <tr>
                    <td align="center" class="menuOptionsHead" style="height: 18px">
                        <asp:Button ID="btnSalesReport" runat="server" BackColor="DodgerBlue" BorderColor="DodgerBlue"
                            BorderStyle="None" BorderWidth="0px" Font-Names="Tahoma" ForeColor="White" Height="19px"
                            OnClick="btnSalesReport_Click" Text="Sales Report" Width="147px" /></td>
                </tr>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 18px" align="center">
							<asp:Button id="btnMainMenu" runat="server" Font-Names="Tahoma" Width="147px"
								ForeColor="White" BorderColor="DodgerBlue" BorderStyle="None" BorderWidth="0px" BackColor="DodgerBlue"
								Text="Main Menu" onclick="Button3_Click"></asp:Button></TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 4px" align="center"></TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 8px" align="center"></TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 18px"></TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 18px">
						<P align="center">&nbsp;</P>
					</TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 18px">
						<P align="center">&nbsp;</P>
					</TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 18px">
						<P align="center">&nbsp;</P>
					</TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 18px" align="center"></TD>
				</TR>
				<TR>
					<TD class="menuOptionsHead" style="HEIGHT: 18px" align="center">
						<asp:textbox id="Textbox4" runat="server" Height="1px" Width="158px" ForeColor="#000040" BorderColor="#000040"
							BorderStyle="Solid"></asp:textbox></TD>
				</TR>
			</TABLE>
		</TD>
	</TR>
</TABLE>
