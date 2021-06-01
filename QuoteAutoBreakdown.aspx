<%@ Page language="c#" Inherits="EPolicy.QuoteAutoBreakdown" CodeFile="QuoteAutoBreakdown.aspx.cs" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
	<title>ePMS | electronic Policy Manager Solution</title>
	<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
	<meta name="CODE_LANGUAGE" Content="C#">
	<meta name="vs_defaultClientScript" content="JavaScript">
	<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<LINK href="epolicy.css" type="text/css" rel="stylesheet">
	<script>
		function PrintPage() {
			window.print();
		}
	</script>
	<style>
		html{
		height: 100%;
	}
	.tablecustom{
		max-width: 1250px;
		min-width: 900px;
	}
	.MainTitle{
		position: absolute;
		left: 50%;
		top: 50%;
		transform: translate(-50%,-50%);
		color: #17529B;
		font-size: 80px;
		font-family: 'Times New Roman', Times, serif;
	}
	.titlerow{
		position: relative;
		height: 200px;
	}
	.bannergrabber{
		cursor: pointer;
		text-align: center;
		height: 40px;
		background-color: rgb(117, 117, 117);
		position: absolute;
		left: 50%;
		transform: translateX(-50%);
		width: 90.5%;
	}

	.contentdivider{
		background-color: white;
		padding-bottom: 15px;
	}
	#ctl00_UpdatePanel100{
		
		padding-left: 10px;
		padding-top: 50px;
	}
	.titleclass{
		position: relative;
		left: 50%;
		top: 50%;
		transform: translate(-50%,-50%);
		font-family: 'Times New Roman', Times, serif;
		font-weight: 800;
		font-size: 15px;
		letter-spacing: 3px;
		text-transform: uppercase;
		color: white;
	}
	</style>
</head>

<body style="height: 0px;background-color: #17529B">
	<form id="QAD" method="post" runat="server">
		<INPUT id="Callback" visible="false" type="hidden" size="1" value="N" name="Callback" runat="server">
		<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
		<div class="row">
			<div class="col-sm-12 col-md-2  sidebar-offcanvas">
				<asp:placeholder id="Placeholder1" runat="server"></asp:placeholder>
			</div>
			<div class="col-sm-12 col-md-10" style="background-color: #e5e2dd;min-height: 900px ">
				<div>
					<div class="row titlerow">
						<asp:Label id="lblHeading" runat="server" class="MainTitle">Cover Breakdown</asp:Label>
					</div>
					<div class="row" style="text-align:center;margin-bottom: 40px;">
						<asp:Button id="btnDrivers" runat="server" Text="Drivers" CssClass="ButtonText-14" onclick="btnDrivers_Click"></asp:Button>
						<asp:Button id="BtnPrint" runat="server" Text="Print" CssClass="ButtonText-14" onclick="BtnPrint_Click" Visible="False"></asp:Button>
						<asp:Button id="btnBack" runat="server" Text="Exit" CssClass="btn btn-primary btn-lg" Width="230px" onclick="btnBack_Click"></asp:Button>
					</div>
				</div>
				<div class="contentdivider container">
					<div class="bannergrabber">
						<p class="titleclass">Cover Breakdown</p>
					</div>
					<div class="collapser" style="width:100%;">
						<table class="tableMain tablecustom" style="margin-top:60px;">
							<th>
								<asp:DataGrid id="dgBreakdown" runat="server" CssClass="tableMain" BackColor="White" PageSize="12" Font-Names="Verdana"
								 Font-Size="10pt" onitemcommand="dgBreakdown_ItemCommand1">
									<FooterStyle ForeColor="#003399" BackColor="Navy"></FooterStyle>
									<SelectedItemStyle HorizontalAlign="Center" BackColor="White"></SelectedItemStyle>
									<EditItemStyle HorizontalAlign="Center" BackColor="White"></EditItemStyle>
									<AlternatingItemStyle HorizontalAlign="Center" CssClass="HeadForm3" BackColor="White"></AlternatingItemStyle>
									<ItemStyle HorizontalAlign="Center" CssClass="HeadForm3" BackColor="#EBEBEB" Font-Bold="False" Font-Italic="False"
									 Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
									<HeaderStyle HorizontalAlign="Center" Height="30px" CssClass="HeadForm2" BackColor="#CCCCCC" Font-Bold="True"
									 Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Names="Verdana"
									 Font-Size="10pt" ForeColor="Black"></HeaderStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="QuoteAutoID" HeaderText="QuoteAutoID"></asp:BoundColumn>
										<asp:BoundColumn Visible="False" DataField="BCID" HeaderText="BCID"></asp:BoundColumn>
										<asp:ButtonColumn ButtonType="PushButton" HeaderText="Sel." CommandName="Select" Visible="False"></asp:ButtonColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Left" ForeColor="Gray" BackColor="White" PageButtonCount="20" CssClass="Numbers"
									 Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
									 Font-Underline="False"></PagerStyle>
								</asp:DataGrid>
							</th>
						</table>
					</div>
					<div style="display:none">
						<table class="tableMain">
							<th>
								<asp:label id="LblCover" RUNAT="server">Cover</asp:label>
							</th>
							<tr>
								<td>
									<asp:label id="LblPeriod1" RUNAT="server">Period 1</asp:label>
								</td>
								<td>
									<asp:label id="LblPeriod2" RUNAT="server">Period 2</asp:label>
								</td>
								<td>
									<asp:label id="LblPeriod3" RUNAT="server">Period 3</asp:label>
								</td>
								<td>
									<asp:label id="LblPeriod4" RUNAT="server">Period 4</asp:label>
								</td>
								<td>
									<asp:label id="LblPeriod5" RUNAT="server">Period 5</asp:label>
								</td>
								<td>
									<asp:label id="LblPeriod6" RUNAT="server">Period 6</asp:label>
								</td>
								<td>
									<asp:label id="LblPeriod7" RUNAT="server">Period 7</asp:label>
								</TD>
							</tr>
							<tr>
								<td>
									<asp:textbox id="TxtPeriod1" tabIndex="4" RUNAT="server" MAXLENGTH="50"></asp:textbox>
								</td>
								<td>
									<asp:textbox id="TxtPeriod2" tabIndex="5" RUNAT="server" MAXLENGTH="50"></asp:textbox>
								</td>
								<td>
									<asp:textbox id="TxtPeriod3" tabIndex="6" RUNAT="server" MAXLENGTH="50"></asp:textbox>
								</td>
								<td>
									<asp:textbox id="TxtPeriod4" tabIndex="7" RUNAT="server" MAXLENGTH="50"></asp:textbox>
								</td>
								<td>
									<asp:textbox id="TxtPeriod5" tabIndex="8" RUNAT="server" MAXLENGTH="50"></asp:textbox>
								</td>
								<td>
									<asp:textbox id="TxtPeriod6" tabIndex="9" RUNAT="server" MAXLENGTH="50"></asp:textbox>
								</td>
								<td>
									<asp:textbox id="TxtPeriod7" tabIndex="10" RUNAT="server" MAXLENGTH="50"></asp:textbox>
								</td>
							</tr>
						</table>
					</div>
					<table class="tableMain">
						<th>
							<asp:Button id="BtnUpdate" runat="server" Text="Update" CssClass="ButtonText-14"></asp:Button>
						</th>
					</table>
					<INPUT id="InpBCID" style="Z-INDEX: 119; LEFT: 4px; WIDTH: 18px; POSITION: absolute; TOP: 484px; HEIGHT: 25px"
					 type="hidden" size="1" value="0" name="term" runat="server"> <INPUT id="InpQuotesAutoID" style="Z-INDEX: 118; LEFT: 4px; WIDTH: 18px; POSITION: absolute; TOP: 444px; HEIGHT: 25px"
					 type="hidden" size="1" value="0" name="term" runat="server">
					<maskedinput:maskedtextheader id="MaskedTextHeader1" RUNAT="server"></maskedinput:maskedtextheader>
					<asp:literal id="litPopUp" RUNAT="server" VISIBLE="False"></asp:literal>

				</div>
				<div style="height: 40px;"></div>
			</div>
		</div>
	</form>
	
    <script>
        function pageLoad() {
           
            var collapsefive = $(".bannergrabber");

            collapsefive.click(function () {
                //$(".contentcolapse2").slideToggle();
                $('.collapser')
                    .stop(true, true)
                    .animate({
                        height: "toggle",
                        opacity: "toggle",
                        padding: "toggle"
                    }, 300);
            });
        }
    </script>
</body>

</HTML>