<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Page language="c#" Inherits="EPolicy.SearchProspect" CodeFile="SearchProspect.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<HEAD>
		<title>ePMS | electronic Policy Manager Solution</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        
		<LINK href="epolicy.css" type="text/css" rel="stylesheet">
	    <style type="text/css">
            .style2
            {
                height: 9px;
            }
        </style>
	    </HEAD>
	<body bgcolor="#e5e2dd">
    <div class="middleMain">
		<form method="post" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
			<table class="tableMain">
                <tr>
                    <td>
                        <asp:PlaceHolder ID="Placeholder1" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
            <table class="tableMain"> 
                <tr>
                    <th>&nbsp;</th>
               </tr>
            </table>
			<table class="tableMain">
                <tr><th><asp:Label id="Label1" runat="server">Search Prospect</asp:Label></th></tr>
                <tr class="dividers"><th><asp:Button id="btnSearch" runat="server" Text="Search" 
                        CssClass="ButtonText-14" onclick="btnSearch_Click" TabIndex="8"></asp:Button>
						<asp:Button id="btnClear" runat="server"  Text="Clear" 
                        CssClass="ButtonText-14" onclick="btnClear_Click" TabIndex="9"></asp:Button></th></tr>
	        </table>
			<table class="tableMain">
				<tr class="trColor1">
                <td class="alignRight"><asp:label id="lblProspectID" runat="server" 
                        Font-Names="Arial Narrow" Font-Size="10pt">Prospect ID</asp:label></td>
                <td><asp:textbox id="TxtProspectID" runat="server" TabIndex="1" 
                        Font-Names="Arial Narrow" Width="150px"></asp:textbox></td>
                </tr>
                <tr>
				    <td class="alignRight"><asp:label id="lblFirstName" RUNAT="server" 
                            Font-Names="Arial Narrow" Font-Size="10pt">First Name</asp:label></td>
				    <td ><asp:textbox id="txtFirstName" style="text-transform:uppercase" tabIndex="2" runat="server" MAXLENGTH="15" 
                            Font-Names="Arial Narrow" Width="150px"></asp:textbox></td>
                </tr>
				<tr class="trColor1">
				   <td class="alignRight"><asp:label id="lblLastName1" RUNAT="server" 
                           Font-Names="Arial Narrow" Font-Size="10pt">Last Name</asp:label></td>
				   <td><asp:textbox id="txtLastName1" style="text-transform:uppercase" tabIndex="3" runat="server" MAXLENGTH="15" 
                           Font-Names="Arial Narrow" Width="150px"></asp:textbox></td>
				</tr>
				<tr class="trColor1">
					<td class="alignRight"><asp:label id="lblEmail" RUNAT="server" 
                            Font-Names="Arial Narrow" Font-Size="10pt">E-Mail</asp:label></td> 
					<td><asp:textbox id="TxtEmail" style="text-transform:uppercase" tabIndex="4" RUNAT="server" MAXLENGTH="50" 
                            Font-Names="Arial Narrow" Width="150px"></asp:textbox></td>
				</tr>
				<tr>
					<td class="alignRight"><asp:label id="lblTelephone"  RUNAT="server" 
                            Font-Names="Arial Narrow" Font-Size="10pt">Phone</asp:label></td>
					<td><asp:textbox id="TxtPhone" tabIndex="5"  RUNAT="server" 
                            Font-Names="Arial Narrow" Width="150px"></asp:textbox>
                     <asp:MaskedEditExtender ID="MaskedEditExtenderTB" runat="server"  Mask="(999)-999-9999" MaskType="Number" TargetControlID="TxtPhone" ClearMaskOnLostFocus="false">
                     </asp:MaskedEditExtender>
                    </td>
				</tr>
				<tr class="trColor1">
					<td class="alignRight"><asp:label id="lblTypeAddress1" RUNAT="server" 
                            Font-Names="Arial Narrow" Font-Size="10pt">Prospect Type:</asp:label></td>
					<td><asp:radiobutton id="RdbIndividual" tabIndex="6" runat="server" 
                            Text="Individual" AutoPostBack="True" Checked="True" GroupName="ClientType" 
                            oncheckedchanged="RdbIndividual_CheckedChanged" Font-Names="Arial Narrow"></asp:radiobutton>
					<asp:radiobutton id="RdbBusiness" tabIndex="7" runat="server" Text="Business" 
                            AutoPostBack="True" GroupName="ClientType" 
                            oncheckedchanged="RdbBusiness_CheckedChanged" Font-Names="Arial Narrow"></asp:radiobutton></td>
				</tr>
				</table>
				<table class="tableMain">
					<tr><th class="style2">
                        <img alt="" src="Images2/GreyLine.png" style="height: 6px" width="100%" /></th></tr>
					<tr><th align="left"><asp:label id="LblTotalCases"  RUNAT="server" 
                            Font-Names="Arial Narrow">Total Cases:</asp:label>
                        <asp:label id="lblLastName2" runat="server" Visible="False" 
                            Font-Names="Arial Narrow">Last Name 2</asp:label>
                        <asp:textbox id="txtLastName2" tabIndex="4" RUNAT="server" MAXLENGTH="15" 
                            Visible="False" Font-Names="Arial Narrow"></asp:textbox></th></tr>
			        <tr><th>
                        <asp:Label id="LblError" runat="server" Visible="False" Font-Bold="True" 
                            ForeColor="Red" Font-Names="Arial Narrow">Label</asp:Label></th></tr>
				</table>
				<table class="tableMain">
                <th>
                    <asp:datagrid id="searchIndividual" RUNAT="server" Visible="False" 
                        BORDERSTYLE="Solid" CssClass="tableMain"
													BORDERWIDTH="1px" BORDERCOLOR="#D6E3EA" ITEMSTYLE-HORIZONTALALIGN="center" HEADERSTYLE-HORIZONTALALIGN="Center"
													BACKCOLOR="White" AUTOGENERATECOLUMNS="False" ALLOWPAGING="True" FONT-SIZE="9pt" FONT-NAMES="Arial Narrow"
													CELLPADDING="0" ALTERNATINGITEMSTYLE-CSSCLASS="HeadForm3" ALTERNATINGITEMSTYLE-BACKCOLOR="#FEFBF6"
													HEADERSTYLE-CSSCLASS="HeadForm2" HEADERSTYLE-BACKCOLOR="#5C8BAE" ITEMSTYLE-CSSCLASS="HeadForm3"
													PageSize="9">
                <FooterStyle></FooterStyle>
				<SelectedItemStyle HorizontalAlign="Center" BackColor="White"></SelectedItemStyle>
                <AlternatingItemStyle HorizontalAlign="Center" CssClass="HeadForm3" BackColor="White"></AlternatingItemStyle>
				<EditItemStyle HorizontalAlign="Center" BackColor="White"></EditItemStyle>
				<ItemStyle HorizontalAlign="Center" CssClass="HeadForm3" BackColor="White"></ItemStyle>
				<HeaderStyle  HorizontalAlign="Center" Height="30px" ForeColor="Black" 
                            CssClass="HeadForm2" BackColor="Silver" Font-Bold="True" Font-Italic="False" 
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                            Font-Names="Verdana"></HeaderStyle>
				<Columns><asp:ButtonColumn ButtonType="PushButton" HeaderText="Sel." CommandName="Select">
				<ItemStyle ></ItemStyle></asp:ButtonColumn>
				<asp:BoundColumn DataField="prospectID" HeaderText="Prospect ID">
				<ItemStyle  HorizontalAlign="Left"></ItemStyle>
				</asp:BoundColumn>
				<asp:BoundColumn DataField="firstName" HeaderText="First Name">
				<ItemStyle  HorizontalAlign="Left"></ItemStyle>
				</asp:BoundColumn>
				<asp:BoundColumn DataField="lastName1" HeaderText="Last Name1">
				<ItemStyle  HorizontalAlign="Left"></ItemStyle></asp:BoundColumn>
				<asp:BoundColumn DataField="lastName2" HeaderText="Last Name2">
				<ItemStyle  HorizontalAlign="Left"></ItemStyle></asp:BoundColumn>
				<asp:BoundColumn DataField="homePhone" HeaderText="Home Phone">
				<ItemStyle  HorizontalAlign="Left"></ItemStyle></asp:BoundColumn>
				<asp:BoundColumn DataField="workPhone" HeaderText="Work Phone"><ItemStyle  HorizontalAlign="Left"></ItemStyle>
				</asp:BoundColumn>
				<asp:BoundColumn DataField="cellular" HeaderText="Cellular">
				<ItemStyle  HorizontalAlign="Left"></ItemStyle>
				</asp:BoundColumn>
				<asp:BoundColumn DataField="email" HeaderText="E-mail">
				<ItemStyle ></ItemStyle>
				</asp:BoundColumn>
				</Columns>
				<PagerStyle  HorizontalAlign="Left" ForeColor="Gray" BackColor="White" 
                            PageButtonCount="20" CssClass="Numbers" Mode="NumericPages" Font-Bold="False" 
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                            Font-Underline="False"></PagerStyle>
				</asp:datagrid>
                </th>
                </table>
				<table class="tableMain">
                <th>					
												<asp:datagrid id="searchBusiness"  RUNAT="server" Visible="False" 
                                                    BORDERSTYLE="Solid" CssClass="tableMain"
													BORDERWIDTH="1px" BORDERCOLOR="#D6E3EA" ITEMSTYLE-HORIZONTALALIGN="center" HEADERSTYLE-HORIZONTALALIGN="Center"
													BACKCOLOR="White" AUTOGENERATECOLUMNS="False" ALLOWPAGING="True" FONT-SIZE="9pt" FONT-NAMES="Arial Narrow"
													CELLPADDING="0" ALTERNATINGITEMSTYLE-CSSCLASS="HeadForm3" ALTERNATINGITEMSTYLE-BACKCOLOR="#FEFBF6"
													HEADERSTYLE-CSSCLASS="HeadForm2" HEADERSTYLE-BACKCOLOR="#5C8BAE" ITEMSTYLE-CSSCLASS="HeadForm3"
													PageSize="9">
													<FooterStyle ForeColor="#003399" BackColor="Navy"></FooterStyle>
													<SelectedItemStyle HorizontalAlign="Center" BackColor="White"></SelectedItemStyle>
													<EditItemStyle HorizontalAlign="Center" BackColor="White"></EditItemStyle>
													<AlternatingItemStyle HorizontalAlign="Center" CssClass="HeadForm3" BackColor="White"></AlternatingItemStyle>
													<ItemStyle HorizontalAlign="Center" CssClass="HeadForm3" BackColor="#CFCFCF" 
                                                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="False"></ItemStyle>
													<HeaderStyle  HorizontalAlign="Center" Height="10px" ForeColor="White" CssClass="HeadForm2"
														BackColor="#A8A8A8" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                                        Font-Underline="False"></HeaderStyle>
													<Columns>
														<asp:ButtonColumn ButtonType="PushButton" HeaderText="Sel." CommandName="Select">
															<ItemStyle ></ItemStyle>
														</asp:ButtonColumn>
														<asp:BoundColumn DataField="ProspectID" HeaderText="Prospect ID">
															<ItemStyle  HorizontalAlign="Left"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="BusinessName" HeaderText="Business Name">
															<ItemStyle  HorizontalAlign="Left"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="FirstName" HeaderText="First Name">
															<ItemStyle  HorizontalAlign="Left"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="lastName" HeaderText="Last Name">
															<ItemStyle  HorizontalAlign="Left"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="WorkPhone" HeaderText="Phone">
															<ItemStyle  HorizontalAlign="Left"></ItemStyle>
														</asp:BoundColumn>
														<asp:BoundColumn DataField="Email" HeaderText="E-mail">
															<ItemStyle  HorizontalAlign="Left"></ItemStyle>
														</asp:BoundColumn>
													</Columns>
													<PagerStyle  HorizontalAlign="Left" ForeColor="Gray" BackColor="White" PageButtonCount="20"
														CssClass="Numbers" Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="False"></PagerStyle>
												</asp:datagrid>
                                        </th>
						</table>
				<maskedinput:maskedtextheader id="MaskedTextHeader1" RUNAT="server"></maskedinput:maskedtextheader>
				<asp:literal id="litPopUp" runat="server" Visible="False"></asp:literal>
		</form>
        </div>
	</body>
</HTML>
