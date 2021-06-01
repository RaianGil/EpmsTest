<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>

<%@ Page Language="c#" Inherits="EPolicy.SearchPolicies" CodeFile="SearchPolicies.aspx.cs" %>

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
        <toolkit:ToolkitScriptManager ID="ScriptManager" runat="server" EnableScriptGlobalization="True">
        </toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <ContentTemplate>
                <div class="row row-offcanvas row-offcanvas-left" style="height: 100%">
                    <div class="col-sm-3 col-md-2 sidebar-offcanvas" id="sidebar" role="navigation">
                        <asp:PlaceHolder ID="phTopBanner" runat="server"></asp:PlaceHolder>
                    </div>
                    <div class="col-sm-9 col-md-10 main" style="height: 100%">
                    <div>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server" ></asp:PlaceHolder>
                    </div>
                        <!--toggle sidebar button-->
                        <p class="visible-xs">
                            <button type="button" class="btn btn-primary btn-xs" data-toggle="offcanvas">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                            </button>
                        </p>
                        <h1 class="page-header">
                            Search Policy</h1>
                        <div class="row formWraper">
                            <div class="form col-sm-12">
                                <div class="row generalSearch">
                                    <div class="searchBy col-sm-2">
                                     <asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Gray">Search By:</asp:Label>                                        
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Label ID="LblEventControlNo" runat="server" Font-Bold="True" ForeColor="Gray">Line Of Business</asp:Label>
                                        <asp:DropDownList ID="ddlPolicyClass" TabIndex="1" runat="server" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlPolicyClass_SelectedIndexChanged" placeholder="Line Of Business" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCustID" runat="server" Font-Bold="True" ForeColor="Gray">Policy Type</asp:Label>
                                        <asp:DropDownList ID="TxtPolicyType" TabIndex="1" runat="server" CssClass="form-control"
                                             placeholder="Policy Type">
                                        </asp:DropDownList>
                                        <asp:Label ID="LblDataType" runat="server" Font-Bold="True" ForeColor="Gray">Policy No.</asp:Label>
                                        <asp:TextBox ID="TxtPolicyNo" TabIndex="3" runat="server" CssClass="form-control"
                                            placeholder="Policy Number"></asp:TextBox>
                                        <asp:Label ID="lblFirstName" runat="server" Font-Bold="True" ForeColor="Gray" style="display:none;" visible="false">Certificate</asp:Label>
                                        <asp:TextBox ID="TxtCertificate" TabIndex="4" runat="server" CssClass="form-control" style="display:none;" visible="false"
                                            placeholder="Certificate"></asp:TextBox>
                                        <asp:Label ID="lblLastName2" runat="server" Font-Bold="True" ForeColor="Gray">Suffix</asp:Label>
                                        <asp:TextBox ID="TxtSuffix" TabIndex="5" runat="server" MaxLength="2" CssClass="form-control"
                                            placeholder="Suffix"></asp:TextBox>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Gray" style="display:none;" visible="false">Bank</asp:Label>
                                        <asp:TextBox ID="TxtBank" Style="text-transform: uppercase; display:none;" TabIndex="6" runat="server" visible="false"
                                            MaxLength="3" CssClass="form-control" placeholder="Bank"></asp:TextBox>
                                        <%--<asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Gray">Loan No.</asp:Label>
                                        <asp:TextBox ID="TxtLoanNo" TabIndex="7" runat="server" CssClass="form-control" placeholder="Loan Number"></asp:TextBox>--%>
                                        <asp:Label ID="lblVIN" runat="server" Font-Bold="True" ForeColor="Gray">VIN</asp:Label></TD>
                                        <asp:TextBox ID="txtVIN" TabIndex="8" runat="server" CssClass="form-control" placeholder="VIN"></asp:TextBox>
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Gray">Plate</asp:Label>
                                        <asp:TextBox ID="TxtLoanNo" TabIndex="7" runat="server" CssClass="form-control" placeholder="Plate"></asp:TextBox>
                                        <br />
                                        <br />
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="btns col-sm-3">
                                        <asp:Button ID="Imagebutton2" runat="server" Text="SEARCH" CssClass="btn btn-primary btn-lg btn-block"
                                            OnClick="Imagebutton2_Click" TabIndex="9"></asp:Button>
                                        <asp:Button ID="Imagebutton1" runat="server" Text="REFRESH" OnClick="Imagebutton1_Click"
                                            CssClass="btn btn-primary btn-lg btn-block" TabIndex="10"></asp:Button>
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
                                    <br />
                                    <br />
                                    <asp:Label ID="LblError" runat="server" ForeColor="Red" Visible="False">Label</asp:Label>
                                      <br /> <br /> <br /><br /> <br />
                                    <img alt="" src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="100%" />
                                      <br /> <br /> <br />
                                    <asp:Label ID="LblTotalCases" runat="server" Font-Bold="false" CssClass="h2 sub-header" ForeColor="#17529B">Total Cases:</asp:Label>
                                    <br /> <br />
                                    <div class="table-responsive">
                                        <asp:DataGrid ID="DtSearchPayments" runat="server" AllowPaging="True" AlternatingItemStyle-BackColor="#FEFBF6"
                                            AlternatingItemStyle-CssClass="HeadForm3" AutoGenerateColumns="False" CellPadding="0"
                                            CssClass="table table-striped" HeaderStyle-BackColor="#5C8BAE" HeaderStyle-CssClass="HeadForm2"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="HeadForm3" ItemStyle-HorizontalAlign="center"
                                            OnItemCommand="DtSearchPayments_ItemCommand1" TabIndex="9" Font-Bold="True" Font-Size="14px"
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
                                                <asp:BoundColumn DataField="TaskControlID" HeaderText="Task No.">
                                                    <ItemStyle  HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="PolicyClassDesc" HeaderText="Line Of Business">
                                                    <ItemStyle Font-Names="Tahoma" HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="PolicyType" HeaderText="Policy Type">
                                                    <ItemStyle  HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="PolicyNo" HeaderText="Policy No.">
                                                    <ItemStyle  />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Certificate" HeaderText="Certificate">
                                                    <ItemStyle  HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Sufijo" HeaderText="Suffix">             
                                                    <ItemStyle  HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="CustomerNo" HeaderText="Customer No.">
                                                     <ItemStyle  HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="CustomerName" HeaderText="Customer Name">
                                                      <ItemStyle  HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Bank" HeaderText="Bank">
                                                   <ItemStyle  HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="LoanNo" HeaderText="Loan No.">
                                                      <ItemStyle  HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="VIN" HeaderText="V.I.N.">
                                                     <ItemStyle  HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                            </Columns>
                                              <PagerStyle HorizontalAlign="Center" Mode="NumericPages" BackColor="White" ForeColor="Gray" Font-Size="18px"
                                                PageButtonCount="15" BorderWidth="0px" />
                                        </asp:DataGrid>
                                        <asp:DataGrid ID="DtSearchAll" runat="server" AllowPaging="True" AlternatingItemStyle-BackColor="#FEFBF6"
                                            AlternatingItemStyle-CssClass="HeadForm3" AutoGenerateColumns="False" CellPadding="0"
                                            CssClass="table table-striped" HeaderStyle-BackColor="#5C8BAE" HeaderStyle-CssClass="HeadForm2"
                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="HeadForm3" ItemStyle-HorizontalAlign="center"
                                            OnItemCommand="DtSearchAll_ItemCommand1" TabIndex="9" Font-Bold="True" Font-Size="14px"
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
                                                <asp:BoundColumn DataField="TaskControlID" HeaderText="Task No.">
                                                    <ItemStyle />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="PolicyClassDesc" HeaderText="Line Of Business">
                                                    <ItemStyle />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="PolicyType" HeaderText="Policy Type">
                                                    <ItemStyle />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="PolicyNo" HeaderText="Policy No.">
                                                    <ItemStyle  />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Certificate" HeaderText="Certificate">
                                                    <ItemStyle />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Sufijo" HeaderText="Suffix">
                                                    <ItemStyle />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="CustomerNo" HeaderText="Customer No.">
                                                    <ItemStyle  />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="CustomerName" HeaderText="Customer Name">
                                                    <ItemStyle  />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="Bank" HeaderText="Bank">
                                                    <ItemStyle  />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="LoanNo" HeaderText="Loan No.">
                                                    <ItemStyle  />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="VIN" HeaderText="V.I.N.">
                                                    <ItemStyle  />
                                                </asp:BoundColumn>
                                            </Columns>
                                             <PagerStyle HorizontalAlign="Center" Mode="NumericPages" BackColor="White" ForeColor="Gray" Font-Size="18px"
                                                PageButtonCount="15" BorderWidth="0px" />
                                        </asp:DataGrid>
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
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                         <asp:TextBox ID="txtHiddenIndex" runat="server" CssClass="form-control"
                                           visible="false" placeholder="Policy Number"></asp:TextBox>
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
