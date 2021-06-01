<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Page language="c#" Inherits="EPolicy.ProspectIndividual" CodeFile="ProspectIndividual.aspx.cs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head id="Head1" runat="server">
		<title>ePMS | electronic Policy Manager Solution</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="epolicy.css" type="text/css" rel="stylesheet">
        </head>
<body bgcolor="#e5e2dd">
    <div class="middleMain">
    <form id="Form1" method="post" runat="server">
          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
           <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
        <ContentTemplate>
            <table class="tableMain">
				<tr>
					<td><asp:placeholder id="Placeholder1" runat="server"></asp:placeholder></td>
				</tr>
            </table>
            <table class="tableMain"> 
                <tr><th>&nbsp;</th>
            </table>
            <table class="tableMain">
				<tr>
                    <td>
                        <asp:Label ID="Label17" runat="server" ForeColor="Navy">Prospect: </asp:Label>
                        <asp:Label ID="lblProspectNo" runat="server"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label2" runat="server">Client: </asp:Label>
                        <asp:Label ID="lblParentCustomer" runat="server"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr class="dividers">
                    <td colspan="2" align="center" class="dividers">
                        <asp:Button ID="btnNew" runat="server" CssClass="ButtonText-14" 
                            onclick="btnNew_Click" Text="Add" />
                        <asp:Button ID="cmdExit" runat="server" CssClass="ButtonText-14" 
                            onclick="cmdExit_Click" Text="Exit" />
                        <asp:Button ID="cmdConvertToCustomer" runat="server" CssClass="ButtonText-14" 
                            onclick="cmdConvertToCustomer_Click" Text="Convert" />
                        <asp:Button ID="btnAuditTrail" runat="server" CssClass="ButtonText-14" 
                            onclick="btnAuditTrail_Click" Text="History" />
                        <asp:Button ID="cmdCancel" runat="server" CssClass="ButtonText-14" 
                            onclick="cmdCancel_Click" Text="Cancel" />
                        <asp:Button ID="BtnSave" runat="server" CssClass="ButtonText-14" 
                            onclick="BtnSave_Click" Text="Save" />
                        <asp:Button ID="btnEdit" runat="server" CssClass="ButtonText-14" 
                            onclick="btnEdit_Click" Text="Edit" />
                        <asp:Button ID="btnNewApplication" runat="server" CssClass="ButtonText-14" 
                            onclick="btnNewApplication_Click" Text="New Application" Width="100px" />
                    </td>
                </tr>
			</table>
			 <table class="tableMain"> 
				<tr>							
					<td class="td106"> *<asp:label id="lblFirstName"  runat="server">First Name</asp:label></td>
					<td><asp:textbox id="txtFirstname" runat="server" MAXLENGTH="15" CssClass="largeTB" 
                            Width="196px"></asp:textbox></td>
					<td class="td106"><asp:label id="lblHomePhone" runat="server">Home Phone</asp:label></td>
					<td class="td22">
                    <asp:TextBox id="txtHomePhone" tabIndex="5" runat="server" 
                            MAXLENGTH="14" CssClass="largeTB"></asp:TextBox>
                            <asp:MaskedEditExtender ID="MaskedEditExtender7" runat="server"  Mask="(999)-999-9999" MaskType="Number" TargetControlID="txtHomePhone" ClearMaskOnLostFocus="false" >
                            </asp:MaskedEditExtender>
                            </td>
					
					<td class="td106"><asp:label id="lblReferredBy" runat="server" ENABLEVIEWSTATE="False">Referred By</asp:label></td>
					<td class="td22"><asp:dropdownlist id="ddlReferredBy" tabIndex="9" runat="server" 
                            AUTOPOSTBACK="True" CssClass="largeTB"></asp:dropdownlist></td>
				</tr>
				<tr class="trColor1">
					<td>*<asp:label id="lblLastName1" runat="server" ENABLEVIEWSTATE="False">Last Name</asp:label></td>
					<td><asp:textbox id="txtLastname1" tabIndex="2" runat="server" MAXLENGTH="15" 
                            CssClass="largeTB" Width="196px"></asp:textbox></td>
					<td><asp:label id="lblWorkPhone" runat="server" ENABLEVIEWSTATE="False" >Work Phone</asp:label></td>
					<td><asp:textbox id="txtWorkPhone" tabIndex="6" runat="server" CssClass="largeTB">
                            </asp:textbox>
                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server"  Mask="(999)-999-9999" MaskType="Number" TargetControlID="txtWorkPhone" ClearMaskOnLostFocus="false" >
                            </asp:MaskedEditExtender>
                            </td>
                   
                    <td></td>
					<td><asp:textbox id="txtReferredByName" tabIndex="10" runat="server" MAXLENGTH="20" 
                            VISIBLE="False" CssClass="largeTB"></asp:textbox></td>
				</tr>
				<tr>
					<td>
                        <asp:Label ID="lblEmail" runat="server" ENABLEVIEWSTATE="False">E-mail</asp:Label>
                    </td>
					<td>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="largeTB" MAXLENGTH="50" 
                            tabIndex="8" Width="196px"></asp:TextBox>
                    </td>
					<td><asp:label id="lblCellular"  runat="server">Cellular Phone</asp:label></td>
					<td><asp:TextBox id="txtCellular" tabIndex="7" runat="server"  CssClass="largeTB">
                    </asp:TextBox>
                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server"  Mask="(999)-999-9999" MaskType="Number" TargetControlID="txtCellular" ClearMaskOnLostFocus="false" >
                            </asp:MaskedEditExtender>
                    </td>
					
                    <td>*<asp:label id="lblLocation" runat="server" ENABLEVIEWSTATE="False" >Originated At</asp:label></td>
					<td><asp:dropdownlist id="ddlLocation" tabIndex="11" runat="server" 
                            CssClass="largeTB"></asp:dropdownlist></td>
				</tr>
				<tr class="trColor1">
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td><asp:dropdownlist id="ddlHouseIncome" tabIndex="4" runat="server" 
                            Visible="False" CssClass="largeTB" ></asp:dropdownlist></td>
                            <td></td>
                            <td>
                                <asp:Label ID="lblLastName2" runat="server" ENABLEVIEWSTATE="False" 
                                    Visible="False">Last Name 2</asp:Label>
                    </td>
                            <td>
                                <asp:TextBox ID="txtLastname2" runat="server" CssClass="largeTB" MAXLENGTH="15" 
                                    tabIndex="3" Visible="False"></asp:TextBox>
                    </td>
                </tr>
				</table>
				<table class="tableMain">					
					<tr>
                        <th align="center" colspan="3">
                            <img alt="" 
                                src="Images2/LineRed.png" style="height: 6px" width="100%" />
                        </th>
                    </tr>
                    <tr>
                        <th align="left">
                            <asp:Label ID="LblTotalCases" runat="server">Total Cases:</asp:Label>
                            <th>
                                <asp:Label ID="LblError" runat="server" ForeColor="IndianRed" Visible="False">Label</asp:Label>
                            </th>
                        </th>
                    </tr>
                </table>
                <table class="tableMain">
                    <tr>
                        <th>
                            <asp:DataGrid ID="searchIndividual" runat="server" ALLOWPAGING="True" 
                                ALTERNATINGITEMSTYLE-BACKCOLOR="#FEFBF6" 
                                ALTERNATINGITEMSTYLE-CSSCLASS="HeadForm3" AUTOGENERATECOLUMNS="False" 
                                BACKCOLOR="White" BORDERCOLOR="#D6E3EA" BORDERSTYLE="Solid" BORDERWIDTH="1px" 
                                CELLPADDING="0" FONT-NAMES="Arial"  
                                HEADERSTYLE-BACKCOLOR="#5C8BAE" HEADERSTYLE-CSSCLASS="HeadForm2" 
                                HEADERSTYLE-HORIZONTALALIGN="Center" ITEMSTYLE-CSSCLASS="HeadForm3" 
                                ITEMSTYLE-HORIZONTALALIGN="center" PageSize="7" Width="100%">
                                <FooterStyle BackColor="Blue" ForeColor="#003399" />
                                <SelectedItemStyle BackColor="White" HorizontalAlign="Center" />
                                <EditItemStyle BackColor="White" HorizontalAlign="Center" />
                                <AlternatingItemStyle BackColor="White" CssClass="HeadForm3" 
                                    HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" 
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle BackColor="#E0E0E0" CssClass="HeadForm3" HorizontalAlign="Center" 
                                    Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" />
                                <HeaderStyle BackColor="#CCCCCC" CssClass="HeadForm2"  
                                     Height="30px" HorizontalAlign="Center" Font-Bold="True" 
                                    Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                    Font-Underline="False" Font-Names="Verdana" Font-Size="10pt" 
                                    ForeColor="Black" />
                                <Columns>
                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Select" 
                                        HeaderText="Sel.">
                                        <ItemStyle  />
                                    </asp:ButtonColumn>
                                    <asp:BoundColumn DataField="TaskControlID" HeaderText="Control No.">
                                        <ItemStyle  HorizontalAlign="Left" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TaskControlTypeDesc" HeaderText="Task Type">
                                        <ItemStyle  HorizontalAlign="Left" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="EntryDate" DataFormatString="{0:d}" 
                                        HeaderText="Entry Date">
                                        <ItemStyle  HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TaskStatusDesc" HeaderText="Status">
                                        <ItemStyle  HorizontalAlign="Left" />
                                    </asp:BoundColumn>
                                </Columns>
                                <PagerStyle BackColor="White" CssClass="Numbers"  
                                    ForeColor="Blue" HorizontalAlign="Left" Mode="NumericPages" 
                                    PageButtonCount="20" />
                            </asp:DataGrid>
                        </th>
                    </tr>
					    </table>
			<maskedinput:maskedtextheader id="MaskedTextHeader1" runat="server"></maskedinput:maskedtextheader>
			<asp:literal id="litPopUp" runat="server" VISIBLE="False"></asp:literal></form>
           
              </ContentTemplate>
               </asp:UpdatePanel>

    </form>
    </div>
	</body>
</html>
