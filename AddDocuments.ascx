<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddDocuments.ascx.cs" Inherits="EPolicy.AddDocuments" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>

    <link href="css/fonts.css" rel="stylesheet"/>
    <link rel="stylesheet" href="css/stylesheet.css"/>
    <link rel="stylesheet" href="css/bootstrap.min.css"/>
    <link rel="stylesheet" href="css/main.css"/>
    <link rel="Stylesheet"" href="font-awesome/css/font-awesome.css" />
    <link href="css/fonts.css" rel="stylesheet"/>
</head>
<body>
    <asp:UpdatePanel ID="UpdatePanel100" runat="server" RenderMode="Block">
        <Triggers>
                <asp:PostBackTrigger ControlID="btnAdjuntarCargar2" />
                <%--<asp:PostBackTrigger ControlID="btnLoadPhoto" />--%>
        </Triggers>
        <ContentTemplate>
            <div style="font-weight: 700">
                <%--<asp:Panel ID="pnlAdjunto" runat="server" BackColor="#F4F4F4" CssClass="" Width="750px">--%>
                    <div class="accordion" style="padding: 0px;
                    font-size: 14px; font-weight: normal; font-style: normal; background-repeat: no-repeat;
                    text-align: left; vertical-align: bottom; background-color: #808080;">
                            &nbsp;&nbsp;&nbsp;<asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Gotham Book" Font-Size="14pt" ForeColor="White" Text="Attach Documents" />
                        </div>
                        <div class="">
                            <table class="MainMenu" style="width: 78%; margin-right: 0px; height: 276px;">
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="Gotham Book">Description:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDocumentDesc2" runat="server" CssClass="textEntry" Width="350px" Height="31px"></asp:TextBox>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" valign="middle">
                                        <asp:FileUpload ID="FileUpload2" runat="server" BorderColor="#8CB3D9" BorderStyle="Double" BorderWidth="1px" CssClass="btn" Height="33px" Width="400px" Font-Names="Gotham Book" />
                                        <br/>
                                        <asp:Button ID="btnAdjuntarCargar2" runat="server" 
                                            onclick="btnAdjuntarCargar2_Click" style="margin-left: 0px; margin-right: 20px;" Text="Load Document" Width="135px" CssClass="btn btn-primary" Font-Names="Gotham Book" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2" valign="middle"><strong>
                                        <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Gotham Book">You can upload an image or PDF with a maximum size of 4MB.</asp:Label>
                                        </strong></td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="2">
                                        <img alt="" src="" width="740" style="height: 48px" class="hidden" />
                                        <asp:Button ID="btnAceptar3" runat="server" CssClass="btn btn-primary" Font-Names="Gotham Book" Height="33px" Text="Exit" Width="80px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" class="style12" colspan="2">
                                        &nbsp;&nbsp;
                                        <asp:GridView ID="gvAdjuntar2" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                            CellPadding="1" Font-Names="Gotham Book" Font-Size="9pt" ForeColor="Black" GridLines="Horizontal" Height="224px" 
                                            onrowcommand="gvAdjuntar2_RowCommand" onrowcreated="gvAdjuntar2_RowCreated" onrowdeleting="gvAdjuntar2_RowDeleting" PageSize="5" Width="696px">
                                            <Columns>
                                                <asp:ButtonField ButtonType="Button" CommandName="View" HeaderText="View" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                                <asp:BoundField DataField="DocumentsID" HeaderText="DocumentsID">
                                                <ControlStyle Width="200px" />
                                                <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Description" HeaderText="Description">
                                                <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                                <ItemStyle Width="145px" />
                                                </asp:BoundField>
                                                <asp:ButtonField ButtonType="Button" CommandName="Delete" HeaderText="Delete" ItemStyle-HorizontalAlign="Left" CausesValidation="False">
                                                </asp:ButtonField>
                                            </Columns>
                                            <HeaderStyle BackColor="#CCCCCC" Height="15px" />
                                            <RowStyle Height="30px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div align="center" class="">
                        </div>

                            <asp:Panel ID="pnlMessage" runat="server" CssClass="" Width="450px" BackColor="#F4F4F4"
                                Height="260px">
                                <div class="" style="padding: 0px; border-radius: 0px; background-color: #17529B;
                                    color: #FFFFFF; font-size: 14px; font-weight: normal; 
                                    background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                                    &nbsp;&nbsp;
                                    <asp:Label ID="Label3" runat="server"
                                        Font-Size="14pt" Text="Message..." ForeColor="White" />
                                </div>
                                <div style="padding: 0px; border-radius: 0px;">
                                    <table style="background-position: center; width: 430px; height: 175px;">
                                        <tr>
                                            <td align="center" valign="middle">
                                                <asp:Label ID="lblRecHeader" runat="server" Font-Bold="False" Font-Italic="False"
                                                    Font-Size="10.5pt" Font-Underline="False" ForeColor="#333333" Text="Message"
                                                    Width="350px" CssClass="Labelfield2-14" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="" align="center">
                                    <asp:Button ID="btnAceptar" runat="server" Text="OK" Width="150px" CssClass="btn btn-primary btn-lg btn-block" />
                                </div>
                            </asp:Panel>
                            <Toolkit:ModalPopupExtender ID="mpeSeleccion" runat="server" BackgroundCssClass=""
                                DropShadow="True" PopupControlID="pnlMessage" TargetControlID="btnDummy2" OkControlID="btnAceptar">
                            </Toolkit:ModalPopupExtender>
                            <asp:Button ID="btnDummy2" runat="server" Visible="true" BackColor="Transparent"
                                BorderStyle="None" BorderWidth="0" BorderColor="Transparent" />
                            <Toolkit:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="pnlMessage"
                                Radius="0" Corners="All" />
                       </div>
        </ContentTemplate>
</asp:UpdatePanel>
</body>
</html>
