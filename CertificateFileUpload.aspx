<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CertificateFileUpload.aspx.cs" Inherits="CertificateFileUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>File Upload</title>
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
        </style>
</head>
<body background="Images2/SQUARE.GIF" bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
		<form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
        <table align="center" bgcolor="#ffffff" bordercolor="white" cellpadding="0"
            cellspacing="0" style="width: 344px; position: static; height: 220px">
            <tr>
            <td style="height: 1px">
                    <asp:PlaceHolder ID="phTopBanner" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td>
                    <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: 739px;
                        height: 425px" bgcolor="White">
                        <tr>
                            <td align="left" background="Images/bk.bmp" class="style14" bgcolor="#F4F4F4">
                                <asp:Label ID="Label3" runat="server" Font-Bold="True" 
                                    Font-Names="tahoma" Font-Size="11pt" ForeColor="#004C97" 
                                    Text="Sale Files Upload"></asp:Label>
                               </td>
                            <td class="style14" align="right" bgcolor="#F4F4F4">
                                <asp:Button ID="BtnExit" runat="server" BackColor="#004C97"
                                                                        BorderColor="White" 
                                    BorderWidth="1px" Font-Names="tahoma" ForeColor="White"
                                                                        Height="23px" OnClick="BtnExit_Click" 
                                    TabIndex="43" Text="Exit" Width="95px" Font-Size="9pt" 
                                    BorderStyle="Solid" />
                            </td>
                        </tr>

                            <td style="height: 1px;" colspan="2" bgColor="#FFE189"></td>
                            
                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td align="center" colspan="2">
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="tahoma"
                                    Font-Size="10pt" Text="File:"></asp:Label>&nbsp;<asp:FileUpload 
                                    ID="FileUpload1" runat="server" 
                                    Font-Names="tahoma" Font-Size="9pt" />
                            </td>
                        </tr>

                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td align="center" colspan="2">
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" 
                                    Font-Names="tahoma" Font-Size="9pt" Width="243px" />
                            </td>
                        </tr>

                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td align="center" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td valign="top" align="center" colspan="2">
                                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
                                <asp:UpdateProgress ID="UpdateProgress3" runat="server" 
                                     DisplayAfter="8">
                                    <ProgressTemplate>
                                            <img alt="" src="Images2/loader.gif" style="width: 35px; height: 35px" />
                                            <span><span class="style5">&nbsp;&nbsp;</span><span 
                                            class="style6">Please wait...</span></span>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <%--</asp:UpdatePanel>--%>
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td valign="top" align="center" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td valign="top" align="center" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td valign="top" align="center" colspan="2">
                                <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="tahoma"
                                    Font-Size="10pt" Text="Existing SRO Files"></asp:Label></td>
                        </tr>
                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td valign="top" align="center" colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlMonth_SelectedIndexChanged" Width="100px">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" 
                                            onselectedindexchanged="ddlMonth_SelectedIndexChanged" Width="100px">
                                        </asp:DropDownList>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnIsClosed" runat="server" Font-Names="tahoma" Font-Size="9pt" 
                                            OnClick="btnIsClosed_Click" Text="Close" Visible="False" Width="243px" />
                                        <br />
                                        <br />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                             </td>
                        </tr>
                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td valign="top" align="center" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td valign="top" align="center" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td valign="top" align="center" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td valign="top" align="center" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td valign="top" align="center" colspan="2">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                            <td valign="top" align="center" colspan="2">
                                &nbsp;</td>
                        </tr>
                        </table>
                </td>
            </tr>
        </table>
        <asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
    </form>
</body>
</html>
