<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TODOLIST.ascx.cs" Inherits="EPolicy.TODOLIST" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>

    <link href="css/fonts.css" rel="stylesheet"/>
    <link rel="stylesheet" href="css/stylesheet.css"/>
    <link rel="stylesheet" href="css/bootstrap.min.css"/>
    <link rel="stylesheet" href="css/main.css"/>
    <link rel="Stylesheet"" href="font-awesome/css/font-awesome.css" />
    <link href="css/fonts.css" rel="stylesheet"/>
    <%--<link rel="stylesheet" type="text/css" src="css/ui-cookies/jquery-ui.css"/>--%>
    <link rel="stylesheet" type="text/css" href="https://code.jquery.com/ui/1.9.2/themes/base/jquery-ui.css"/>
    <style>
        #note{
        display:none;
        position:fixed;
        z-index: 999;
        right:3px;
        height: 300px;
        width:500px;
        background: #4679bd;     
        padding: 20px 20px 20px;    
        margin-bottom: 20px;
        color: #fff;     
        border-radius: 9px;
        opacity: 0.9;
        filter: alpha(opacity=50);
        border:1px Solid Black;
    }
    
    #note:hover{
        display:none;
        position:fixed;
        z-index: 999;
        right:3px;
        
        width:500px;
        background: #4679bd;     
        padding: 20px 20px 20px;     
        margin-bottom: 20px;
        color: #fff;     
        border-radius: 9px;
        opacity: 0.99;
        filter: alpha(opacity=100);
        border:1px Solid Black;
        cursor:move;
    }
    
    #border
    {
        height:80%; 
        overflow:auto;
        cursor:auto;
    }
    
    .p
    {
        cursor:auto;
    }
 
    .close {
        background: transparent url('Images2/close-sign.png') 0 0 no-repeat;
        position: absolute;
        top: 5px;
        right: 5px;
        width:40px;
        height:48px;
        display:block;
    }
    
    
    .myGridClass {
        width: 100%;
        /*this will be the color of the odd row*/
        background-color: #fff;
        margin: 5px 0 10px 0;
        border: solid 1px #525252;
        border-collapse:collapse;
    }

    /*data elements*/
    .myGridClass td {
      padding: 2px;
      border: solid 1px #c1c1c1;
      color: #717171;
    }

    /*header elements*/
    .myGridClass th {
      padding: 4px 2px;
      color: #fff;
      background: #424242;
      border-left: solid 1px #525252;
      font-size: 0.9em;
    }

    /*his will be the color of even row*/
    .myGridClass .myAltRowClass { background: #fcfcfc repeat-x top; }

    /*and finally, we style the pager on the bottom*/
    .myGridClass .myPagerClass { background: #424242; }

    .myGridClass .myPagerClass table { margin: 5px 0; }

    .myGridClass .myPagerClass td {
      border-width: 0;
      padding: 0 6px;
      border-left: solid 1px #666;
      font-weight: bold;
      color: #fff;
      
    }

    .myGridClass .myPagerClass a { color: #666; text-decoration: none; }

    .myGridClass .myPagerClass a:hover { color: #000; text-decoration: none; } 

    </style>
    <script src="https://code.jquery.com/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js" type="text/javascript"></script>

    <%--<script src="js/ui-cookies/jquery-1.12.4.js" type="text/javascript"></script>
    <script src="js/ui-cookies/jquery-ui.js" type="text/javascript"></script>--%>
    <script src="js/ui-cookies/jquery-cookies.js" type="text/javascript"></script>

    <%--<script>
        function pageLoad() {
            $(document).ready(function () {

                var cookieHeight = 'divHeight';
                var cookieWidth = 'divWidth';

                //On document ready, if we find height from our cookie, 
                //we set the div to this height.
                var height = Cookies.get(cookieHeight);
                var width = Cookies.get(cookieWidth);
                if (height != null && width != null) {
                    $('#note').css('height', height + 'px');
                    $('#note').css('width', width + 'px');
                }

                $("#note").resizable({
                    maxHeight: 1000,
                    minHeight: 300,
                    minWidth: 500,
                    handles: 'ne, se, sw, nw',
                    stop: function (e, ui) {
                        Cookies.set("divHeight", ui.size.height);
                        Cookies.set("divWidth", ui.size.width);
                    }
                });

                $("#note").draggable({ containment: "window", cancel: "#border" });

                var $window = $(window),
                $body = $('body'),
                $note = $("#note");

                $note.fadeIn();

                $('.close').on('click', function () {
                    $note.fadeOut("slow");
                    $window.off('scroll');
                });
            });
        }
    </script>--%>
</head>
<body>
    <asp:UpdatePanel ID="UpdatePanel100" runat="server" RenderMode="Block">
        <ContentTemplate>
<%--<h2>Scroll to the Bottom of the Page to see the Notification</h2>--%>
    <div id="note">
            <p id="title"><b>QUOTES PENDING APPROVAL<b></p>
            <hr>
             <div id="border">
                <asp:GridView ID="GridQuotes" CssClass="myGridClass" runat="server" AutoGenerateColumns="False" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="1px" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridQuotes_RowCommand"
                    OnRowDeleting="GridQuotes_RowDeleting" OnRowCreated="GridQuotes_RowCreated"  Height="85%" Font-Bold="False">                                                   
                    <Columns>
                        <asp:ButtonField ButtonType="Button" CommandName="Select" HeaderText="SELECT">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="TaskControlID" HeaderText="QUOTE #" ReadOnly="True" SortExpression="Quote">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EntryDate" DataFormatString="{0:d}" HeaderText="CREATED DATE" ReadOnly="True" SortExpression="Quote">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SubmittedDate" DataFormatString="{0:d}" HeaderText="SUBMITTED DATE" ReadOnly="True" SortExpression="Quote">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EnteredBy" HeaderText="USER" ReadOnly="True" SortExpression="Quote">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:ButtonField ButtonType="Button" CommandName="Approved" HeaderText="Reviewed">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" CommandName="Rejected" HeaderText="Decline">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:ButtonField>
                    </Columns>
                    <EditRowStyle BackColor="" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Gray" Font-Bold="False" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" Height="20px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                        ForeColor="#333333" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <AlternatingRowStyle BackColor="#F7F6F3" HorizontalAlign="Center"/>
                        <SelectedRowStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            <hr>
            </div>
            <span class="close">
            </span>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</body>
</html>
