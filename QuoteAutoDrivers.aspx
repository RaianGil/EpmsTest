<%@ Page Language="c#" Inherits="EPolicy.QuoteAutoDrivers" CodeFile="QuoteAutoDrivers.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<%@ Register TagPrefix="MaskedInput" Namespace="MaskedInput" Assembly="MaskedInput" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"xml:lang="en">

    <head runat="server">
    <title>ePMS | electronic Policy Manager Solution</title>
    <link href="css/fonts.css" rel="stylesheet" />
   
                <style type="text/css">
                   html{
                       height:100%;
                   }
                    .colcenter{
                        display: block;
                        margin-left: auto;
                        margin-right:auto;
                        width: 180px;
                    }
                    
                    .title{
                        position: relative;
                        font-size:100px;
                        color: #17529b;
                        margin-left:auto;
                        margin-right:auto;
                        margin-bottom:100px;
                        width:300px;
                        
                    }
                </style>
            </head>
            <body>
                <form id="QAD" method="post" runat="server">
                      <div class="container-fluid" >
                    <input id="Callback" visible="false" type="hidden" size="1" value="N" name="Callback" runat="server" />
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
                        <ContentTemplate>
                         <div class="row row-offcanvas row-offcanvas-left" >
                          
                            <div class="col-sm-3 col-md-2 sidebar-offcanvas" id="sidebar" role="navigation">
                                       <asp:PlaceHolder ID="phTopBanner" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="col-sm-8 col-md-10" style="padding-top:40px; background-color:#e6e6e6;">
                                <div class="row">
                                    <p class="title" >Drivers</p>
                                </div>
                            <div class="row">
                                <div class="col-lg-1 ">
                                        <asp:Label ID="Label1" runat="server" style="font-weight:800;" Visible="False">Driver Information:</asp:Label>&nbsp;
                                        <asp:Label ID="lblTaskControlID" runat="server" DESIGNTIMEDRAGDROP="197" style="font-weight:800;">Control
                                            ID:</asp:Label>
                                        <asp:Label ID="txtTaskControlID" runat="server" DESIGNTIMEDRAGDROP="198" style="font-weight:700;">0</asp:Label>
                                </div>
                                <div class="col-sm-10" style="text-align:center;">
                                        <asp:Button ID="btnAddDriver" runat="server" Text="Add" OnClick="btnAddDriver_Click" CssClass="btn btn-primary btn-lg" Width="230px"></asp:Button>
                                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary btn-lg" Width="230px">
                                        </asp:Button>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-primary btn-lg" Width="230px"></asp:Button>
                                        <asp:Button ID="btnVehicles" runat="server" Text="Vehicles" OnClick="btnVehicles_Click" CssClass="btn btn-primary btn-lg" Width="230px"></asp:Button>
                                        <asp:Button ID="btnEdit" runat="server" Text="Modify" OnClick="btnEdit_Click" CssClass="btn btn-primary btn-lg" Width="230px">
                                        </asp:Button>
                                        <asp:Button ID="btnBack" runat="server" Text="Exit" OnClick="btnBack_Click" CssClass="btn btn-primary btn-lg" Width="230px">
                                        </asp:Button>
                                </div>
                                <div class="col-sm-1">
                               </div>
                            </div>
                            <div class="row" > 
                                <br />
                            </div>
                                 <Toolkit:Accordion ID="Accordion4" runat="Server" AutoSize="None" CssClass="accordion"
                                HeaderSelectedCssClass="" FadeTransitions="true" FramesPerSecond="40" HeaderCssClass="accordion-head"
                                ContentCssClass="accordion-body" RequireOpenedPane="false" SelectedIndex="0"
                                SuppressHeaderPostbacks="true" TransitionDuration="250" EnableTheming="False" 
                                    EnableViewState="False">
                 <Panes>
                 <Toolkit:AccordionPane ID="AccordionPane5" runat="server">
                 <Header>
                 Add Drivers
                 <div class="arrow down">
                 </div>
                 </Header>
                 <Content>
                            <div class="container" style="background-color:white;">
                                <div class="row">
                                    <div class="col-sm-1 col-md-4"></div>
                                    <div class="col-sm-3 col-md-2">
                                       <div class="row">
                                            <div class="col-12 colcenter">
                                                <asp:Label ID="lblFirstNm" runat="server">First Name</asp:Label>
                                                <asp:TextBox ID="txtFirstNm" TabIndex="6" runat="server" MaxLength="15" CssClass="form-controlWhite" style="background-color:#ffffff;"></asp:TextBox>
                                            </div>
                                            <div class="col-12 colcenter">
                                                <asp:Label ID="lblLastNm1" runat="server">Last Name</asp:Label>
                                                <asp:TextBox ID="txtLastNm1" TabIndex="7" runat="server" MaxLength="15" CssClass="form-controlWhite"></asp:TextBox>
                                            </div>
                                            <div class="col-12 colcenter">
                                                <asp:Label ID="lblLastNm2" runat="server">Last Name</asp:Label>
                                                <asp:TextBox ID="txtLastNm2" TabIndex="8" runat="server" MaxLength="15" CssClass="form-controlWhite"></asp:TextBox>
                                            </div>
                                            <div class="col-12 colcenter">
                                                <asp:Label ID="lblBirthDt" runat="server" style="display:block;">Birth Date</asp:Label>
                                                <asp:TextBox ID="txtBirthDt" TabIndex="9" runat="server" ISDATE="True" Width="105px" CssClass="form-controlWhite"></asp:TextBox>
                                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" PopupButtonID="imgCalendarBT" TargetControlID="txtBirthDt">
                                                </asp:CalendarExtender>
                                                <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" CultureName="en-US" Mask="99/99/9999" MaskType="Date" TargetControlID="txtBirthDt">
                                                </asp:MaskedEditExtender>
                                                <asp:ImageButton ID="imgCalendarBT" runat="server" ImageUrl="~/Images2/Calendar.png" TabIndex="23" Width="16px" />
                                            </div>
                                           
                                       </div>
                                    </div>
                                    <div class="col-sm-3 col-md-2">
                                      <div class="row">
                                            <div class="col-12 colcenter">
                                                <asp:Label ID="lblGender" runat="server">Gender</asp:Label>
                                                <asp:DropDownList ID="ddlGender" TabIndex="11" runat="server" CssClass="form-controlWhite">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-12 colcenter">
                                                <asp:Label ID="lblMaritalSt" runat="server">Marital Status</asp:Label>
                                                <asp:DropDownList ID="ddlMaritalSt" TabIndex="12" runat="server" CssClass="form-controlWhite">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-12 colcenter">
                                                <asp:Label ID="lblLicense" runat="server">License</asp:Label>
                                                <asp:TextBox ID="txtLicense" TabIndex="13" runat="server" MaxLength="10" CssClass="form-controlWhite"></asp:TextBox>

                                                 <div class="col-12 colcenter">
                                                <asp:Label ID="lblAge" runat="server" style="display:block;">Age</asp:Label>
                                                <asp:TextBox ID="txtAge" TabIndex="-1" runat="server" CssClass="form-controlWhite" Width="100px"></asp:TextBox>
                                                </div>

                                            </div>
                                            <div class="col-12 colcenter">
                                                <asp:Label ID="lblSSN" runat="server">Soc. Sec.</asp:Label>
                                                <asp:TextBox ID="txtSocSec" TabIndex="14" runat="server" MaxLength="4" cssclass="form-controlWhite"></asp:TextBox>
                                            </div>
                                      </div>
                                    </div>
                                    <div class="col-sm-2 col-md-4"></div>
                                </div>
                                <br />
                                
                            </div>
                       </Content>
                 </Toolkit:AccordionPane>
                 </Panes>
                </Toolkit:Accordion>
                 <br />
                <br />
                <br />
                <div class="row">
                                    <div class="col-sm-3 col-md-12" style="align-content:center">
                                        <asp:DataGrid ID="dgDriverList" TabIndex="15" runat="server" Font-Names="Verdana" CssClass="tableMain" BackColor="White"
                                            BorderColor="#D6E3EA" BorderWidth="1px" BorderStyle="Solid" AutoGenerateColumns="False"
                                            CellPadding="0" AllowPaging="True" PageSize="7" OnItemCreated="dgDriverList_ItemCreated1" OnItemCommand="dgDriverList_ItemCommand"
                                            Font-Size="10pt" Width="75%" style="margin-left:auto;margin-right:auto;">

                                            <FooterStyle ForeColor="#003399" BackColor="Navy"></FooterStyle>
                                            <SelectedItemStyle HorizontalAlign="Center" BackColor="White"></SelectedItemStyle>
                                            <EditItemStyle HorizontalAlign="Center" BackColor="#F0F0F0"></EditItemStyle>
                                            <AlternatingItemStyle HorizontalAlign="Center" CssClass="HeadForm3" BackColor="White">
                                            </AlternatingItemStyle>
                                            <ItemStyle HorizontalAlign="Center" CssClass="HeadForm3" BackColor="WhiteSmoke">
                                            </ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" Height="30px" ForeColor="White" CssClass="HeadForm2" BackColor="Gray" Font-Bold="True"
                                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                                Font-Names="Verdana" Font-Size="10pt">
                                            </HeaderStyle>
                                            <Columns>
                                                <asp:ButtonColumn ButtonType="PushButton" HeaderText="Sel." CommandName="Select">
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn DataField="FirstName" HeaderText="First Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="LastName1" HeaderText="Last Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="LastName2" HeaderText="Last Name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="BirthDate" HeaderText="Birth Date"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="Gender" HeaderText="Gender"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MaritalStatusDesc" HeaderText="Marital Status"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="License" HeaderText="License"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="SocialSecurity" HeaderText="Sec. Sec."></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="ProspectID"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="QuotesDriversID"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="GenderID"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="MaritalStatusID"></asp:BoundColumn>
                                                <asp:ButtonColumn ButtonType="PushButton" HeaderText="Delete" CommandName="Remove">
                                                    
                                                </asp:ButtonColumn>
                                                <asp:BoundColumn Visible="False" DataField="HomePhone"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="WorkPhone"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="Cellular"></asp:BoundColumn>
                                                <asp:BoundColumn Visible="False" DataField="InternalDriverID"></asp:BoundColumn>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" ForeColor="Blue" BackColor="White" PageButtonCount="20" CssClass="Numbers" Mode="NumericPages"></PagerStyle>
                                        </asp:DataGrid>
                                    </div>
                                </div>
               
                </div>
                </div>
                            <input id="prospectOffered" style="z-index: 102; left: 4px; width: 23px; position: absolute;
                top: 404px; height: 25px" type="hidden" size="1" value="0" name="prospectOffered" runat="server" />
                            <asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
                            <MaskedInput:MaskedTextHeader ID="MaskedTextHeader2" runat="server"></MaskedInput:MaskedTextHeader>
                             

                           <asp:Panel ID="pnlMessage" runat="server" BackColor="#F4F4F4" CssClass="CajaDialogo" Style="display: none;"
                    Width="400px">
                    <div class="CajaDialogoDiv" style="padding: 0px; background-color: #17529B; color: #C0C0C0;
                        font-family: tahoma; font-size: 14px; font-weight: normal; font-style: normal;
                        background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                        <asp:Label ID="Label55" runat="server" Font-Bold="False" Font-Italic="False" 
                            Font-Size="14pt" Text="Message.." ForeColor="White" />
                    </div>
                  
                    <div class="CajaDialogoDiv" style="color: #FFFFFF">
                        <table style="background-position: center; height: 175px;">
                            <tr>
                                <td align="left" valign="middle">
                                    <asp:TextBox ID="lblRecHeader" runat="server" Font-Bold="False" Font-Italic="False" backgorund="#C0C0C0"
                                        Font-Size="10pt" Font-Underline="False" ForeColor="333333" Text="Message"  Font-Names="Arial" Width="380px"
                                        CssClass="" TextMode="MultiLine" Height="170px" BorderColor="Transparent" BorderStyle="None"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="CajaDialogoDiv" style="text-align:center;">
                        <asp:Button ID="btnAceptar" runat="server" Text="OK" CssClass="btn btn-primary btn-lg"/>
                        <br />
                        
            
                    </div>
                 </asp:Panel>
                 <Toolkit:ModalPopupExtender ID="mpeSeleccion" runat="server" BackgroundCssClass="modalBackground"
                    CancelControlID="" DropShadow="True" OkControlID="btnAceptar" OnCancelScript="" OnOkScript=""
                    PopupControlID="pnlMessage" TargetControlID="btnDummy">
                </Toolkit:ModalPopupExtender>
                <asp:Button ID="btnDummy" runat="server" BackColor="Transparent" BorderColor="Transparent"
                                BorderStyle="None" BorderWidth="0" Visible="true" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                  

                  
                </form>

                  <script type="text/ecmascript">
                      function getAge() {
                          pdt = new Date(document.QAD.txtBirthDt.value);
                          today = new Date("<%=today%>");

                          age = (today.getFullYear() - pdt.getFullYear());
                          day = pdt.getDay();
                          month = pdt.getMonth();

                          if (month == today.getMonth()) {
                              if (day > today.getDay()) {
                                  age = age - 1;
                              }
                          }
                          else {
                              if (month > today.getMonth()) {
                                  age = age - 1;
                              }
                          }

                          if (age >= 0) {
                              document.QAD.txtAge.value = age;
                          }
                      }
                            </script> 
           
            </body>

            </html>
                <%--<asp:Literal>teral ID="litPopUp" runat="server" Visible="False"></asp:Literal>--%>
