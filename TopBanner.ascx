<%@ Control Language="c#" Inherits="EPolicy.TopBanner2" CodeFile="TopBanner.ascx.cs" %>
<link href="epolicy.css" type="text/css" rel="stylesheet" />
 <link rel="icon" href="http://www.onealliance.net/Images2/LogoGuardian.ico" type="image/x-icon" />
 <title>One Alliance - Insurance Corporation</title>
 <script type="text/javascript">
     javascript: window.history.forward(1);
    </script>
<table class="tableMain">
    <tr>
        <td align="left"><asp:Image ID="imgGuardianLogo" runat="server" Height="78px" 
                ImageUrl="~/Images2/guardianLogo.png" Visible="True" Width="215px" />
        </td>
        <td align="right"><asp:Image ID="imgGuessWho" runat="server" Height="70px" ImageUrl="~/Images2/login.png" Visible="False" Width="70px" />
        <asp:Label ID="lblUserName" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label></td>
    </tr>
    <tr style="background: #036893;">
      <td style="background: #036893;" colspan="2">
      <div class="hideSkiplink">
                        <div style="height: 34px; width:100%; background:#036893;">
                           <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="False"
                            IncludeStyleBlock="False" Orientation="Horizontal" 
                            RenderingMode="List" Font-Names="Arial Narrow" Font-Size="14pt" 
                                onmenuitemclick="NavigationMenu_MenuItemClick" BackColor="#036893">
                                <Items>
                                    <asp:MenuItem Text="Main Menu" Value="Main Menu" ></asp:MenuItem>
                                    <asp:MenuItem Text="Search" Value="Search">
                                        <asp:MenuItem Text="Search by ControlID" Value="Search by ControlID"></asp:MenuItem>
                                        <asp:MenuItem Text="Search by Customers" Value="Search by Customers"></asp:MenuItem>
                                        <asp:MenuItem Text="Search by Policies" Value="Search by Policies"></asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem Text="New Transaction" Value="New Transaction">
                                        <asp:MenuItem Text="Customers" Value="Customers"></asp:MenuItem>
                                        <asp:MenuItem Text="Auto Personal" Value="Auto Personal"> </asp:MenuItem>
                                        <asp:MenuItem Text="New Quote" Value="Autos VI"></asp:MenuItem>
                                        <asp:MenuItem Text="GuardianXtra" Value="GuardianXtra"></asp:MenuItem>
                                    </asp:MenuItem>                                   
                                    <asp:MenuItem Text="Reports" Value="Reports"></asp:MenuItem>
                                    <asp:MenuItem Text="Administration" Value="Administration">
                                        <asp:MenuItem Text="Lookup Tables" Value="Lookup Tables"></asp:MenuItem>
                                        <asp:MenuItem Text="User Properties List" Value="User Properties List">
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="Add User" Value="Add User"></asp:MenuItem>
                                        <asp:MenuItem Text="Group &amp; Permissions" Value="GroupPermissions">
                                        </asp:MenuItem>
                                        <asp:MenuItem Text="XML Policy" Value="XML Policy"></asp:MenuItem>
                                    </asp:MenuItem>
                                    <asp:MenuItem Text="Logout" Value="Logout"></asp:MenuItem>
                                </Items>
                            </asp:Menu>
                        </div>
                        </div>
                    </td>
                </tr>
            </table>