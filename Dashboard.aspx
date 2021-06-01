<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="EPolicy.Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SRO Sales Dashboard</title>
    <script>
        function validatePage() {
            flagProcessing = true;
        }

        function disableBotonPagar() {
            var boton = document.getElementById('btnPagar2');
            if (boton != null) {
                boton.style.visibility = 'hidden';
                //document.TransactionPrepaid.TxtNumRef.value = ""; 
            }
        } 
    </script>
    <style type="text/css">
        .style14
        {
            height: 22px;
        }
        .dividers
        {
            background-color: #ffe189;
            font-size: 11px;
            color: #999999;
            font-weight: bold;
            font-family: Sans-Serif;
            text-align: center;
        }
        .ButtonText-14
        {
            color: black;
            font-size: 12px;
            font-family: Arial Narrow;
            font-weight: bold;
            text-align: center;
            background-color: #ffac37;
            width: 80px;
            border: medium solid #f2c32f;
            height: 25px;
        }
        
        .ButtonText-Dashboard
        {
            color: black;
            font-size: 12px;
            font-family: Arial Narrow;
            font-weight: bold;
            text-align: center;
            background-color: #ffac37;
            width: 60px;
            border: medium solid #f2c32f;
            height: 25px;
        }
    </style>
</head>
<body background="Images2/SQUARE.GIF" bottommargin="0" leftmargin="0" rightmargin="0"
    topmargin="0">
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <table align="center" bgcolor="#ffffff" bordercolor="white" cellpadding="0" cellspacing="0"
        style="width: 100%; position: static; height: 220px">
        <tr>
            <td style="height: 1px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: 932px;
                    height: 100%" bgcolor="White">
                    <tr>
                        <td align="left" background="Images/bk.bmp" class="style14" bgcolor="#F4F4F4">
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </td>
                        <td class="style14" align="right" bgcolor="#F4F4F4">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" background="Images/bk.bmp" class="style14" bgcolor="#F4F4F4">
                            <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="tahoma" Font-Size="11pt"
                                ForeColor="#004C97" Text="Dashboard"></asp:Label>
                        </td>
                        <td class="style14" align="right" bgcolor="#F4F4F4">
                            &nbsp;
                        </td>
                    </tr>
                    <td style="height: 1px;" colspan="2" class="dividers">
                    </td>
                    <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                        <td align="center" colspan="2">
                            <asp:Label ID="lblCompany" runat="server" AssociatedControlID="ddlCompany" Visible="False">Company Name:</asp:Label>
                            <asp:DropDownList ID="ddlCompany" runat="server" CssClass="textEntry" Width="300px"
                                Visible="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                        <td align="center" colspan="2">
                            <asp:Label ID="lblTipoDoc20" runat="server">From:</asp:Label>
                            <asp:TextBox ID="txtBegDate" runat="server" CssClass="textEntry" TabIndex="8" Width="100px"></asp:TextBox>
                            &nbsp;<asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="imgCalendarBegDate" TargetControlID="txtBegDate">
                            </asp:CalendarExtender>
                            <asp:Label ID="lblTipoDoc21" runat="server">To:</asp:Label>
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="textEntry" TabIndex="8" Width="100px"></asp:TextBox>
                            <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="imgCalendarEndDate" TargetControlID="txtEndDate">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                        <td align="center" colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                        <td align="center" colspan="2">
                            <asp:Button ID="BtnRefresh" runat="server" Text="Refresh" Width="120px" OnClick="BtnRefresh_Click"
                                Visible="False" />
                            <asp:Button ID="BtnGuardian" runat="server" Text="Guardian Info" Width="120px" OnClick="BtnGuardian_Click"
                                CssClass="ButtonText-Dashboard" />
                            <asp:Button ID="BtnTotalSales" runat="server" Text="Market Info" Width="120px" OnClick="BtnTotalSales_Click"
                                CssClass="ButtonText-Dashboard" />
                            <asp:Button ID="BtnBank" runat="server" Text="Bank" Width="120px" OnClick="BtnBank_Click"
                                CssClass="ButtonText-Dashboard" />
                            <asp:Button ID="BtnCol" runat="server" Text="Colecturia" Width="120px" OnClick="BtnCol_Click"
                                CssClass="ButtonText-Dashboard" />
                            <asp:Button ID="BtnCoop" runat="server" Text="Cooperative" Width="120px" OnClick="BtnCoop_Click"
                                CssClass="ButtonText-Dashboard" />
                            <asp:Button ID="BtnEoi" runat="server" Text="EOI" Width="120px" OnClick="BtnEoi_Click"
                                CssClass="ButtonText-Dashboard" />
                            <asp:Button ID="BtnWeek" runat="server" Text="Weekdays" Width="120px" OnClick="BtnWeek_Click"
                                CssClass="ButtonText-Dashboard" />
                            <asp:Button ID="BtnDistribution" runat="server" Text="Distributions" Width="120px"
                                OnClick="BtnDistributions_Click" CssClass="ButtonText-Dashboard" />
                            <asp:Button ID="BtnExit" runat="server" OnClick="BtnExit_Click" Text="Exit" Width="120px"
                                CssClass="ButtonText-Dashboard" Visible="False" />
                        </td>
                    </tr>
                    <br />
                    <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                        <td align="center" colspan="2">
                            &nbsp;</td>
                    </tr>
                    <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                        <td align="center" colspan="2" class="dividers">
                            <asp:DropDownList ID="DropDownList1" runat="server" Visible="False">
                                <asp:ListItem>Compañias</asp:ListItem>
                                <asp:ListItem>ANTILLES INS COMPANY</asp:ListItem>
                                <asp:ListItem>ASC</asp:ListItem>
                                <asp:ListItem>COOP DE SEGUROS MULTIPLES</asp:ListItem>
                                <asp:ListItem>GUARDIAN INS COMPANY</asp:ListItem>
                                <asp:ListItem>INTEGRAND ASSURANCE COMPANY</asp:ListItem>
                                <asp:ListItem>MAPFRE PRAICO</asp:ListItem>
                                <asp:ListItem>MULTINATIONAL INS COMPANY</asp:ListItem>
                                <asp:ListItem>POINT GUARD</asp:ListItem>
                                <asp:ListItem>QBE OPTIMA INS COMPANY</asp:ListItem>
                                <asp:ListItem>SEGUROS TRIPLE S</asp:ListItem>
                                <asp:ListItem>UNIVERSAL INS COMPANY</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList2" runat="server" Visible="False">
                                <asp:ListItem>Bancos</asp:ListItem>
                                <asp:ListItem>SCOTIABANK - LEASING</asp:ListItem>
                                <asp:ListItem>ORIENTAL BANK - BBVA</asp:ListItem>
                                <asp:ListItem>SANTANDER</asp:ListItem>
                                <asp:ListItem>BANCO COOPERATIVO</asp:ListItem>
                                <asp:ListItem>POPULAR AUTO LEASING</asp:ListItem>
                                <asp:ListItem>SCOTIABANK</asp:ListItem>
                                <asp:ListItem>FIRST BANK</asp:ListItem>
                                <asp:ListItem>BANCO POPULAR DE PR</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList3" runat="server" Visible="False">
                                <asp:ListItem>Colecturias</asp:ListItem>
                                <asp:ListItem>ACREDITACION DE PAGOS</asp:ListItem>
                                <asp:ListItem>ADJUNTAS</asp:ListItem>
                                <asp:ListItem>AGUADA</asp:ListItem>
                                <asp:ListItem>AGUADILLA</asp:ListItem>
                                <asp:ListItem>AGUAS BUENAS</asp:ListItem>
                                <asp:ListItem>AIBONITO</asp:ListItem>
                                <asp:ListItem>ANASCO</asp:ListItem>
                                <asp:ListItem>ARECIBO CESCO</asp:ListItem>
                                <asp:ListItem>ARROYO</asp:ListItem>
                                <asp:ListItem>BARCELONETA</asp:ListItem>
                                <asp:ListItem>BAYAMON _CESCO_</asp:ListItem>
                                <asp:ListItem>CENTRO JUDICIAL CAROLINA</asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="DropDownList4" runat="server" Visible="False">
                                <asp:ListItem>EOI</asp:ListItem>
                                <asp:ListItem>AF SHELL</asp:ListItem>
                                <asp:ListItem>ALBERTO ELECTROMECANICO</asp:ListItem>
                                <asp:ListItem>ALBORAX INTERNATIONAL GULF</asp:ListItem>
                                <asp:ListItem>ANGEL SERVICE CENTER</asp:ListItem>
                                <asp:ListItem>APOLO TEXACO SERVICENTRO</asp:ListItem>
                                <asp:ListItem>ARAMBURU ESSO SERVICE</asp:ListItem>
                                <asp:ListItem>ARROYO AUTO GLASS</asp:ListItem>
                                <asp:ListItem>AUTO PIEZAS LUGO</asp:ListItem>
                                <asp:ListItem>AUTO SERVICIO CAPARRA</asp:ListItem>
                                <asp:ListItem>AUTO SERVICIO VARGAS</asp:ListItem>
                                <asp:ListItem>AUTOMECH SERVICENTER</asp:ListItem>
                                <asp:ListItem>BARRANQUITAS CAR CARE</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                        <td align="left" colspan="2" class="dividers">
                            <%--Filters:--%>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                        <td align="left" colspan="2" class="dividers">
                            <asp:DropDownList ID="ddlPosType" runat="server" Visible="False" 
                                AutoPostBack="True" onselectedindexchanged="ddlPosType_SelectedIndexChanged">
                                <asp:ListItem Value="Tipo Pos"></asp:ListItem>
                                <asp:ListItem>Banco</asp:ListItem>
                                <asp:ListItem>Colecturia</asp:ListItem>
                                <asp:ListItem>Cooperativa</asp:ListItem>
                                <asp:ListItem>EOI</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                        <td align="center" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                        <td align="center" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                        <td align="center" colspan="2">
                            <asp:Chart ID="ChartCompanySells" runat="server" BackColor="WhiteSmoke" BackGradientStyle="TopBottom"
                                BackSecondaryColor="White" BorderlineDashStyle="Solid" Height="450px" Width="800px"
                                OnClick="ChartCompanySells_Click">
                                <Series>
                                    <asp:Series Color="OrangeRed" Legend="Default" Name="Series1">
                                    </asp:Series>
                                </Series>
                                <ChartAreas>
                                    <asp:ChartArea Name="ChartArea1">
                                        <AxisY TitleFont="Microsoft Sans Serif, 6.75pt">
                                        </AxisY>
                                    </asp:ChartArea>
                                </ChartAreas>
                                <Legends>
                                    <asp:Legend Name="Default">
                                    </asp:Legend>
                                </Legends>
                                <Titles>
                                    <asp:Title Font="Verdana, 12pt" Name="Title1">
                                    </asp:Title>
                                </Titles>
                            </asp:Chart>
                            <asp:RoundedCornersExtender ID="ChartCompanySells_RoundedCornersExtender" runat="server"
                                Enabled="True" TargetControlID="ChartCompanySells">
                            </asp:RoundedCornersExtender>
                        </td>
                    </tr>
                    <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                        <td align="center" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                    &nbsp;
                </table>
            </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <td align="center" colspan="2">
                <asp:Panel ID="Panel1" runat="server" Height="350px" Width="90%" ScrollBars="Both">
                    <asp:GridView ID="gvGuardianSales" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                        BorderStyle="Solid" Font-Size="10pt" ForeColor="Black" Height="200px" Style="text-align: right"
                        Width="50%">
                        <AlternatingRowStyle BackColor="#FFF0C1" />
                        <Columns>
                            <asp:BoundField DataField="Month" HeaderText="Month">
                                <HeaderStyle Font-Names="11pt" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="2017" DataFormatString="{0:c0}" HeaderText="2017" />
                            <asp:BoundField DataField="2016" DataFormatString="{0:c0}" HeaderText="2016" />
                        </Columns>
                        <HeaderStyle BackColor="#FFE189" Font-Size="Small" />
                    </asp:GridView>
                    <br />
                    <asp:GridView ID="gvSales" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                        BorderStyle="Solid" Font-Size="10pt" ForeColor="Black" Height="200px" Style="text-align: left"
                        Width="1200px">
                        <AlternatingRowStyle BackColor="#FFF0C1" />
                        <Columns>
                            <asp:BoundField DataField="Company" HeaderText="COMPANY">
                                <HeaderStyle Font-Names="11pt" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ARECIBO" HeaderText="ARECIBO" />
                            <asp:BoundField DataField="BAYAMON" HeaderText="BAYAMON" />
                            <asp:BoundField DataField="CAGUAS" HeaderText="CAGUAS" />
                            <asp:BoundField DataField="CAROLINA" HeaderText="CAROLINA" />
                            <asp:BoundField DataField="CENTRO" HeaderText="CENTRO" />
                            <asp:BoundField DataField="CUQUES" HeaderText="CULEBRA &amp; VIEQUES" />
                            <asp:BoundField DataField="ESTE" HeaderText="ESTE" />
                            <asp:BoundField DataField="MAYAGUEZ" HeaderText="MAYAGUEZ" />
                            <asp:BoundField DataField="METRO" HeaderText="METRO" />
                            <asp:BoundField DataField="NORTE" HeaderText="NORTE" />
                            <asp:BoundField DataField="OESTE" HeaderText="OESTE" />
                            <asp:BoundField DataField="PONCE" HeaderText="PONCE" />
                            <asp:BoundField DataField="SANJUAN" HeaderText="SAN JUAN" />
                            <asp:BoundField DataField="SUR" HeaderText="SUR" />
                            <asp:BoundField DataField="NA" HeaderText="#N/A" />
                            <asp:BoundField DataField="Total" HeaderText="Grand Total" />
                        </Columns>
                        <HeaderStyle BackColor="#FFE189" Font-Size="Small" />
                    </asp:GridView>
                    <br />
                    <asp:Label ID="LblTopX" runat="server">Top:</asp:Label>
                    <asp:TextBox ID="TxtTopX" runat="server" CssClass="textEntry" Text="10" Width="50px"
                        TabIndex="5"></asp:TextBox>
                    <asp:Button ID="BtnTopX" runat="server" OnClick="BtnTopX_Click" TabIndex="99" Text="Go"
                        CssClass="GeneralButton-Dashboard" />
                    <br />
                    <asp:Button ID="BtnExportTop" runat="server" OnClick="BtnExportTop_Click" TabIndex="99" Text="Export Top Sellers to Excel"
                        CssClass="GeneralButton-Dashboard" />
                    <br />
                    <br />
                    <asp:GridView ID="gvTopTen" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                        BorderStyle="Solid" Font-Size="10pt" ForeColor="Black" Height="200px" Style="text-align: left"
                        Visible="False" Width="1200px">
                        <AlternatingRowStyle BackColor="#FFF0C1" />
                        <Columns>
                            <asp:BoundField DataField="NOMBRE_EA" HeaderText="Top Sellers">
                                <HeaderStyle Font-Names="11pt" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Ventas" HeaderText="Sum of Counts" />
                            <asp:BoundField DataField="Ventas_Guardian" HeaderText="Guardian Sales" />
                            <asp:BoundField DataField="Guardian_Percent" DataFormatString="{0:P2}" HeaderText="Guardian Percentage Sales" />
                            <asp:BoundField DataField="Ventas_First" HeaderText="Top Seller Company" />
                            <asp:BoundField DataField="Ventas_Second" HeaderText="Second Seller Company" />
                        </Columns>
                        <HeaderStyle BackColor="#FFE189" />
                    </asp:GridView>
                    <br />
                    <br />
                    <asp:Button ID="BtnExport" runat="server" OnClick="BtnExport_Click" TabIndex="99"
                        Text="Export to Excel" CssClass="GeneralButton-Dashboard" />
                    <br />
                    <br />
                    <asp:GridView ID="gvIndividualSales" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                        BorderStyle="Solid" Font-Size="10pt" ForeColor="Black" Height="200px" Style="text-align: left"
                        Visible="False" Width="1200px">
                        <AlternatingRowStyle BackColor="#FFF0C1" />
                        <Columns>
                            <asp:BoundField DataField="Company" HeaderText="COMPANY">
                                <HeaderStyle Font-Names="11pt" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ARECIBO" HeaderText="ARECIBO" />
                            <asp:BoundField DataField="BAYAMON" HeaderText="BAYAMON" />
                            <asp:BoundField DataField="CAGUAS" HeaderText="CAGUAS" />
                            <asp:BoundField DataField="CAROLINA" HeaderText="CAROLINA" />
                            <asp:BoundField DataField="CENTRO" HeaderText="CENTRO" />
                            <asp:BoundField DataField="CUQUES" HeaderText="CULEBRA &amp; VIEQUES" />
                            <asp:BoundField DataField="ESTE" HeaderText="ESTE" />
                            <asp:BoundField DataField="MAYAGUEZ" HeaderText="MAYAGUEZ" />
                            <asp:BoundField DataField="METRO" HeaderText="METRO" />
                            <asp:BoundField DataField="NORTE" HeaderText="NORTE" />
                            <asp:BoundField DataField="OESTE" HeaderText="OESTE" />
                            <asp:BoundField DataField="PONCE" HeaderText="PONCE" />
                            <asp:BoundField DataField="SANJUAN" HeaderText="SAN JUAN" />
                            <asp:BoundField DataField="SUR" HeaderText="SUR" />
                            <asp:BoundField DataField="NA" HeaderText="#N/A" />
                            <asp:BoundField DataField="Total" HeaderText="Grand Total" />
                        </Columns>
                        <HeaderStyle BackColor="#FFE189" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <td align="center" colspan="2">
                <asp:Image ID="imgDashboardMap" runat="server" 
                    ImageUrl="~/Images2/mapaPR.png" />
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="900px" BorderStyle="Solid"
                    Font-Names="Verdana" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" Visible="False"
                    WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ShowCredentialPrompts="False"
                    ShowDocumentMapButton="False">
                </rsweb:ReportViewer>
            </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <td valign="top" align="center" colspan="2">
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
                <asp:UpdateProgress ID="UpdateProgress3" runat="server" DisplayAfter="8">
                    <ProgressTemplate>
                        <img alt="" src="Images2/loader.gif" style="width: 35px; height: 35px" />
                        <span><span class="style5">&nbsp;&nbsp;</span><span class="style6">Please wait...</span></span>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                &nbsp;
            </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <td valign="top" align="center" colspan="2">
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
                <asp:UpdateProgress ID="UpdateProgress5" runat="server" DisplayAfter="8">
                    <ProgressTemplate>
                        <img alt="" src="Images2/loader.gif" style="width: 35px; height: 35px" />
                        <span><span class="style5">&nbsp;&nbsp;</span><span class="style6">Please wait...</span></span>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <%--</asp:UpdatePanel>--%>
            </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <div id="Div2" runat="server">
                <td valign="top" align="center" colspan="2" class="dividers">
                    &nbsp;
                </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <td valign="top" align="center" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <td valign="top" align="center" colspan="2">
                <asp:Chart ID="ChartPercentage" runat="server" BackColor="WhiteSmoke" BackGradientStyle="TopBottom"
                    BackSecondaryColor="White" BorderlineDashStyle="Solid" Height="450px" OnClick="ChartCompanySells_Click"
                    Width="800px">
                    <Series>
                        <asp:Series ChartType="Doughnut" Color="OrangeRed" Legend="Default" Name="Series1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisY TitleFont="Microsoft Sans Serif, 6.75pt">
                            </AxisY>
                        </asp:ChartArea>
                    </ChartAreas>
                    <Legends>
                        <asp:Legend Name="Default">
                        </asp:Legend>
                    </Legends>
                    <Titles>
                        <asp:Title Font="Verdana, 12pt" Name="Title1">
                        </asp:Title>
                    </Titles>
                </asp:Chart>
                <asp:RoundedCornersExtender ID="ChartPercentage_RoundedCornersExtender" runat="server"
                    Enabled="True" TargetControlID="ChartPercentage">
                </asp:RoundedCornersExtender>
            </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <td valign="top" align="center" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <td valign="top" align="center" colspan="2">
                <asp:Panel ID="Panel2" runat="server" Height="350px" Width="900px" ScrollBars="Both">
                    <asp:GridView ID="gv4" runat="server" AutoGenerateColumns="False" ForeColor="Black"
                        Font-Size="10pt" BorderColor="Black" BorderStyle="Solid" Height="200px" Width="1200px"
                        Style="text-align: left">
                        <AlternatingRowStyle BackColor="#FFF0C1" />
                        <Columns>
                            <asp:BoundField DataField="Company" HeaderText="COMPANY">
                                <HeaderStyle Font-Names="11pt" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="10pt" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ARECIBO" HeaderText="ARECIBO" />
                            <asp:BoundField DataField="BAYAMON" HeaderText="BAYAMON" />
                            <asp:BoundField DataField="CAGUAS" HeaderText="CAGUAS" />
                            <asp:BoundField DataField="CAROLINA" HeaderText="CAROLINA" />
                            <asp:BoundField DataField="CENTRO" HeaderText="CENTRO" />
                            <asp:BoundField DataField="CUQUES" HeaderText="CULEBRA &amp; VIEQUES" />
                            <asp:BoundField DataField="ESTE" HeaderText="ESTE" />
                            <asp:BoundField DataField="MAYAGUEZ" HeaderText="MAYAGUEZ" />
                            <asp:BoundField DataField="METRO" HeaderText="METRO" />
                            <asp:BoundField DataField="NORTE" HeaderText="NORTE" />
                            <asp:BoundField DataField="OESTE" HeaderText="OESTE" />
                            <asp:BoundField DataField="PONCE" HeaderText="PONCE" />
                            <asp:BoundField DataField="SANJUAN" HeaderText="SAN JUAN" />
                            <asp:BoundField DataField="SUR" HeaderText="SUR" />
                            <asp:BoundField DataField="NA" HeaderText="#N/A" />
                            <asp:BoundField DataField="Total" HeaderText="Grand Total" />
                        </Columns>
                        <HeaderStyle BackColor="#FFE189" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <td valign="top" align="center" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <td valign="top" align="center" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
            <td valign="top" align="center" colspan="2">
                &nbsp;
            </td>
        </tr>
        </div>
        <div id="Div3" runat="server">
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td valign="top" align="center" colspan="2" class="dividers">
                    &nbsp;
                </td>
            </tr>
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td valign="top" align="center" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td valign="top" align="center" colspan="2">
                    <asp:Chart ID="ChartSales" runat="server" BackColor="WhiteSmoke" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" BorderlineDashStyle="Solid" Height="450px" OnClick="ChartCompanySells_Click"
                        Width="800px">
                        <Series>
                            <asp:Series ChartType="Line" Color="OrangeRed" Legend="Default" Name="Series1" YValuesPerPoint="6">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                                <AxisY TitleFont="Microsoft Sans Serif, 6.75pt">
                                </AxisY>
                            </asp:ChartArea>
                        </ChartAreas>
                        <Legends>
                            <asp:Legend Name="Default">
                            </asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Font="Verdana, 12pt" Name="Title1">
                            </asp:Title>
                        </Titles>
                    </asp:Chart>
                    <asp:RoundedCornersExtender ID="ChartSales_RoundedCornersExtender" runat="server"
                        Enabled="True" TargetControlID="ChartSales">
                    </asp:RoundedCornersExtender>
                </td>
            </tr>
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td valign="top" align="center" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td valign="top" align="center" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td valign="top" align="center" colspan="2">
                    <asp:Panel ID="Panel3" runat="server" Height="350px" Width="840px" ScrollBars="Both">
                        <asp:GridView ID="gvSolds" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                            BorderStyle="Solid" Font-Size="10pt" ForeColor="Black" Height="200px" Style="text-align: left"
                            Width="1200px">
                            <AlternatingRowStyle BackColor="#FFF0C1" />
                            <Columns>
                                <asp:BoundField DataField="Company" HeaderText="Company" />
                                <asp:BoundField DataField="Arecibo" HeaderText="Arecibo" />
                                <asp:BoundField DataField="Bayamon" HeaderText="Bayamon" />
                            </Columns>
                            <HeaderStyle BackColor="#FFE189" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td valign="top" align="center" colspan="2">
                    &nbsp;
                </td>
            </tr>
        </div>
        <div id="Div4" runat="server">
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td valign="top" align="center" colspan="2" class="dividers">
                    &nbsp;
                </td>
            </tr>
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td valign="top" align="center" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td valign="top" align="center" colspan="2">
                    <asp:Chart ID="ChartPremium" runat="server" BackColor="WhiteSmoke" BackGradientStyle="TopBottom"
                        BackSecondaryColor="White" BorderlineDashStyle="Solid" Height="450px" OnClick="ChartCompanySells_Click"
                        Width="800px">
                        <Series>
                            <asp:Series ChartType="Line" Color="OrangeRed" Legend="Default" Name="Series1" YValuesPerPoint="6">
                            </asp:Series>
                        </Series>
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1">
                                <AxisY TitleFont="Microsoft Sans Serif, 6.75pt">
                                </AxisY>
                            </asp:ChartArea>
                        </ChartAreas>
                        <Legends>
                            <asp:Legend Name="Default">
                            </asp:Legend>
                        </Legends>
                        <Titles>
                            <asp:Title Font="Verdana, 12pt" Name="Title1">
                            </asp:Title>
                        </Titles>
                    </asp:Chart>
                    <asp:RoundedCornersExtender ID="ChartPremium_RoundedCornersExtender" runat="server"
                        Enabled="True" TargetControlID="ChartPremium">
                    </asp:RoundedCornersExtender>
                </td>
            </tr>
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td valign="top" align="center" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td valign="top" align="center" colspan="2">
                    <asp:Panel ID="Panel4" runat="server" Height="350px" Width="840px" ScrollBars="Both">
                        <asp:GridView ID="gvPremiums" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                            BorderStyle="Solid" Font-Size="10pt" ForeColor="Black" Height="200px" Style="text-align: left"
                            Width="1200px">
                            <AlternatingRowStyle BackColor="#FFF0C1" />
                            <Columns>
                                <asp:BoundField DataField="Company" HeaderText="COMPANY">
                                    <HeaderStyle Font-Names="11pt" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="10pt" HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ARECIBO" HeaderText="ARECIBO" />
                                <asp:BoundField DataField="BAYAMON" HeaderText="BAYAMON" />
                                <asp:BoundField DataField="CAGUAS" HeaderText="CAGUAS" />
                                <asp:BoundField DataField="CAROLINA" HeaderText="CAROLINA" />
                                <asp:BoundField DataField="CENTRO" HeaderText="CENTRO" />
                                <asp:BoundField DataField="CUQUES" HeaderText="CULEBRA &amp; VIEQUES" />
                                <asp:BoundField DataField="ESTE" HeaderText="ESTE" />
                                <asp:BoundField DataField="MAYAGUEZ" HeaderText="MAYAGUEZ" />
                                <asp:BoundField DataField="METRO" HeaderText="METRO" />
                                <asp:BoundField DataField="NORTE" HeaderText="NORTE" />
                                <asp:BoundField DataField="OESTE" HeaderText="OESTE" />
                                <asp:BoundField DataField="PONCE" HeaderText="PONCE" />
                                <asp:BoundField DataField="SANJUAN" HeaderText="SAN JUAN" />
                                <asp:BoundField DataField="SUR" HeaderText="SUR" />
                                <asp:BoundField DataField="NA" HeaderText="#N/A" />
                                <asp:BoundField DataField="Total" HeaderText="Grand Total" />
                            </Columns>
                            <HeaderStyle BackColor="#FFE189" />
                        </asp:GridView>
                        &nbsp;
                    </asp:Panel>
                </td>
            </tr>
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td valign="top" align="center" colspan="2">
                    &nbsp;
                </td>
            </tr>
        </div>
    </table>
    <asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
    </form>
</body>
</html>
