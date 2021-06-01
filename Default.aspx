<%@ Page Language="c#" Inherits="EPolicy.Default" CodeFile="Default.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit, Version=3.5.50508.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e"
    Namespace="AjaxControlToolkit" TagPrefix="Toolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
       <meta content="" charset="utf-8"/>
     <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
    <title>Guardian Insurance</title>
    <%--<link href="epolicy.css" type="text/css" rel="stylesheet" />--%>

     <meta name="description" content=""/>
        <meta name="viewport" content="width=device-width, initial-scale=1"/>
        <link rel="apple-touch-icon" href="apple-touch-icon.png"/>
     
     <link rel="icon" href="Images2\LogoGuardian.ico" type="image/x-icon" />
    <!--[if lt IE 7]> <script src="http://ie7-js.googlecode.com/svn/version/2.1(beta4)/IE7.js"></script> <![endif]-->
    <!--[if lt IE 8]> <script src="http://ie7-js.googlecode.com/svn/version/2.1(beta4)/IE8.js"></script> <![endif]-->
    <!--[if lt IE 9]> <script src="http://ie7-js.googlecode.com/svn/version/2.1(beta4)/IE9.js"></script> <![endif]-->
   
    <link rel="stylesheet" href="css/bootstrap.min.css"/>     
     <link rel="stylesheet" href="css/main.css"/>
     <link rel="stylesheet" href="css/stylesheet.css"/>
     <link href="css/fonts.css" rel="stylesheet"/>
    <style>
           body {
				padding-top: 200px;
				padding-bottom: 20px;
				background-color: #e6e6e6;
			}
			.eq-height {
			  overflow: hidden; 
			}
			
			[class*="col-"]{
			  margin-bottom: -99999px;
			  padding-bottom: 99999px;
			}
        </style>
       
</head>

<body>
    <div class="container">
        <form id="Form1" method="post" runat="server" >
        <Toolkit:ToolkitScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" AsyncPostBackTimeout="0">
        </Toolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Block">
            <ContentTemplate>
               
                <asp:Literal ID="litPopUp" runat="server" Visible="False"></asp:Literal>
                

        <div class="row eq-height mainLoginPage">
            <div class="guardianLogo col-sm-6">
            	<div class="img-center-wrapper">
       	    		<img src="Images2/guardian-login-logo.png" class="img-responsive img-centered" 
                        alt=""/> </div>
                </div>
            <div class="userInfo col-sm-6">
            	<section class="formWrapper">
            		<header><h1 class="formTitle">Sign In</h1></header>

                       <div class="form-group">
                        <label for="exampleInputEmail1">Username</label>                       
                         <asp:TextBox ID="TxtUserName" runat="server" CssClass="form-control"  placeholder="Username" TabIndex="1"></asp:TextBox>
                      </div>

                      <div class="form-group">
                        <label for="exampleInputPassword1">Password</label>
                        <asp:TextBox ID="TxtPassword" runat="server" CssClass="form-control" TabIndex="2" TextMode="Password" placeholder="Password"></asp:TextBox>
                      </div>
                       <div class="form-group" align="center">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                                DisplayAfter="10">
                                <ProgressTemplate>
                                    <img alt="" src="Images2/loader.gif" style="width: 40px; height: 40px;" />
                                    <span><span class=""></span><span class="" style="font-size:18px"> Please wait...</span></span>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                          </div>                                            
                        <asp:Button ID="BtnLogIn" runat="server"  CssClass="btn btn-primary btn-lg btn-block" OnClick="BtnLogIn_Click" TabIndex="3" Text="SIGN IN"/>
                        <asp:Button ID="BtnSalir" runat="server" CssClass="btn btn-primary btn-lg btn-block" OnClick="BtnSalir_Click" TabIndex="4" Text="Logout" Visible="False"  />
                    <footer>
               	    <img src="Images2/epps-login-logo.png" class="img-responsive img-centered" alt=""/> </footer>
                    <h5 class="companyInfo">&copy;2016 all rights reserved <a href="http://www.guardianinsurance.com/" title="Guardian Insurance Company" target="new">www.guardianinsurance.com</a></h5>
            	</section>
          </div><!--userInfo-->
        </div>
 
                
                
               <asp:Panel ID="pnlMessage" runat="server" CssClass="" Width="450px" BackColor="#F4F4F4"
                    Height="260px">
                    <div class="" style="padding: 0px; border-radius: 5px; background-color: #036893;
                        color: #FFFFFF; font-family: tahoma; font-size: 14px; font-weight: normal; font-style: normal;
                        background-repeat: no-repeat; text-align: left; vertical-align: bottom;">
                        &nbsp;&nbsp;
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="arialnarrow"
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
                            
            </ContentTemplate>
        </asp:UpdatePanel>
         
        </form>
    </div>
</body>
</html>
