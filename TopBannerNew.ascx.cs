namespace EPolicy
{
	using System;
	using System.Configuration;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using EPolicy;
	using System.Globalization;
    using System.Web.Security;
    using EPolicy.TaskControl;

	/// <summary>
	///		Summary description for TopBanner.
	/// </summary>
    public partial class TopBannerNew : System.Web.UI.UserControl
    {

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion


        protected System.Web.UI.WebControls.ImageButton imgReport;
        //int locationID = 0;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //LblDate.Text = Utilities.LongDateSpanish(DateTime.Now);
            this.lblUserName.Text = "USER: ";

            Login.Login cp = HttpContext.Current.User as Login.Login;


            if (cp != null && cp.Identity.Name != string.Empty)
            {
                this.lblUserName.Visible = true;
                this.lblUserName.Text = cp.Identity.Name.Split("|".ToCharArray())[0];
                //TreeViewMenu.Visible = true;
                //NavigationMenu.Visible = true;
                //imgGuessWho.Visible = true;
                //this.Img1.Visible = false;
                //this.Img2.Visible = true;
                //this.Img3.Visible = true;
                //LblOficina.Text = LookupTables.LookupTables.GetDescription("Location",locationID.ToString());
                if (Session["ChangePassword"] != null)
                {
                }
                else
                {
                    VerifyAccess(cp);
                }
            }
            else
            {
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                FormsAuthentication.SignOut();
                Response.Cookies.Add(authCookies);

                this.lblUserName.Visible = true;
                this.lblUserName.Text = string.Empty;
                //TreeViewMenu.Visible = false;
                //NavigationMenu.Visible = false;
                //imgGuessWho.Visible = false;
                //this.Img1.Visible = true;
                //this.Img2.Visible = false;
                //this.Img3.Visible = false;
                //LblOficina.Text = string.Empty;               
            }
        }


        private void VerifyAccess(EPolicy.Login.Login cp)
        {
            EPolicy.Login.Login login = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = 0;
            userID = int.Parse(login.Identity.Name.Split("|".ToCharArray())[1]);

            string rtValue = "";
            Literal1.Text = "";
            try
            {

                //Home
                rtValue += @"<li class=""""><a href=""RedirectByMenu.aspx?page=Home""><span class=""fa fa-home icon-home-icon-epps-r""></span> Home</a></li>";

                //Search
                if (cp.IsInRole("SEARCH MAIN MENU") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"<li ><a href=""RedirectByMenu.aspx?page=By ControlID""><span class=""fa fa-search icon-search-icon-epps-r""></span> Search</a>";

                rtValue += @"<ul class=""nav3"">";

                if (cp.IsInRole("SEARCH CONTROL MAIN MENU") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=By ControlID"">By ControlID</a></li>";

                if (cp.IsInRole("SEARCH CUSTOMER MAIN MENU") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=By Customers"">By Customers</a></li>";

                if (cp.IsInRole("SEARCH POLICIES MAIN MENU") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=By Policies"">By Policies</a></li>";

                rtValue += @"</ul>";
                if (!cp.IsInRole("SEARCH MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"</li>";
                //End Search

                //Transaction
                if (cp.IsInRole("NEW TRANSACTION MAIN MENU") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"<li><a href=""#""><span class=""fa fa-plus icon-transaction-icon-epps-r""></span> Transaction</a>";

                rtValue += @"<ul>";

                if (cp.IsInRole("CUSTOMER MAIN MENU") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=Customers"">Customers</a></li>";

                //AUTOS
                if (cp.IsInRole("AUTO PERSONAL MAIN MENU") || cp.IsInRole("AUTOS VI MAIN MENU") || cp.IsInRole("RENEWAL MAIN MENU") || cp.IsInRole("DOUBLE INTERST") || cp.IsInRole("DEALERS") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"<li><a>Autos</a>";

                rtValue += @"<ul>";

                    if (cp.IsInRole("AUTO PERSONAL MAIN MENU") || cp.IsInRole("ADMINISTRATOR"))
                        rtValue += @"<li><a href=""RedirectByMenu.aspx?page=Auto Personal"">Auto Personal</a></li>";

                    if (cp.IsInRole("AUTOS VI MAIN MENU") || cp.IsInRole("ADMINISTRATOR"))
                        rtValue += @"<li><a href=""RedirectByMenu.aspx?page=New VI Quote"">New VI Quote</a></li>";

                    //if (userID == 1 || userID == 25 || userID == 26 || userID == 28 || userID == 33)
                    if (cp.IsInRole("AUTO HIGH LIMIT"))
                        rtValue += @"<li><a href=""RedirectByMenu.aspx?page=Auto High Limit"">Auto High Limit</a></li>";

                    if (cp.IsInRole("RENEWAL MAIN MENU") || cp.IsInRole("ADMINISTRATOR"))
                        rtValue += @"<li><a href=""RedirectByMenu.aspx?page=Renewals from PPS"">Renewals</a></li>";

                    if ((cp.IsInRole("DOUBLE INTERST") || cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("AUTOS VI")) && !cp.IsInRole("DEALERS"))
                        rtValue += @"<li><a href=""RedirectByMenu.aspx?page=DoubleInterest"">Double Interest</a></li>";
                    else if (cp.IsInRole("DEALERS"))
                        rtValue += @"<li><a href=""RedirectByMenu.aspx?page=DoubleInterest"">Quick Quote</a></li>";

                rtValue += @"</ul>";
                if (cp.IsInRole("AUTO PERSONAL MAIN MENU") || cp.IsInRole("AUTOS VI MAIN MENU") || cp.IsInRole("RENEWAL MAIN MENU") || cp.IsInRole("DOUBLE INTERST") || cp.IsInRole("DEALERS") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"</li>";
                //END AUTOS

                //GUARDIAN
                if (cp.IsInRole("GUARDIAN XTRA") || cp.IsInRole("GUARIDANROADASSISTANCE") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"<li><a>Guardian</a>";
                rtValue += @"<ul>";

                if (cp.IsInRole("GUARDIAN XTRA") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=GuardianXtra"">Xtra</a></li>";

                if (cp.IsInRole("GUARIDANROADASSISTANCE") || cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("AUTOS VI"))
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=GuardianRoadAssist"">Road Assist</a></li>";

                rtValue += @"</ul>";
                if (cp.IsInRole("GUARDIAN XTRA") || cp.IsInRole("GUARIDANROADASSISTANCE") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"</li>";
                //END GUARDIAN

                if (cp.IsInRole("AUTOS VI MAIN MENU") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=Premium Finance"">PFC</a></li>";
				
				if (cp.IsInRole("HOME OWNERS") || cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("AUTOS VI"))
                    rtValue += @"<li><a>Residential Property</a>";
                    rtValue += @"<ul>";
                        rtValue += @"<li><a href=""RedirectByMenu.aspx?page=HomeOwners"">New Quote</a></li>";
                        rtValue += @"<li><a href=""RedirectByMenu.aspx?page=Renewal from PPS HomeOwners"">Renewals</a></li>";
                    rtValue += @"</ul>";
                if (cp.IsInRole("BONDS") || cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("BONDSVI"))
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=Bonds"">Bonds</a></li>";

                if (cp.IsInRole("YACHT") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=Yacht"">Yacht</a></li>";

                rtValue += @"</ul>";
                if (cp.IsInRole("NEW TRANSACTION MAIN MENU") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"</li>";
                //End Transaction

                //Reports
                if (cp.IsInRole("REPORT MAIN MENU") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=Reports"" ><span class=""fa fa-newspaper-o icon-reports-icon-epps-r""></span> Report</a></li>";

                //Dashboard
                if (cp.IsInRole("SRO DASHBOARD ADMIN") || cp.IsInRole("ADMINISTRATOR"))
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=Dashboard"" ><span class=""fa fa-area-chart""></span>Sro Dashboard</a></li>";
                

                //Administration
                if (cp.IsInRole("ADMINISTRATOR"))
                {
                    rtValue += @"<li><a href=""""><span class=""fa fa-desktop icon-transaction-icon-epps-r""></span> Administration</a>";
                    rtValue += @"<ul>";
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=LookupTables"">Lookup Tables</a></li>";
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=UserPropertiesList"">User Properties List</a></li>";
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=AddUser"">Add User</a></li>";
                    rtValue += @"<li><a href=""RedirectByMenu.aspx?page=GroupPermissions"">Group & Permissions</a></li>";
                    rtValue += @"</ul>";
                    rtValue += @"</li>";
                }


                //User Manual
                //if (cp.IsInRole("REPORT MAIN MENU") || cp.IsInRole("ADMINISTRATOR"))
                //rtValue += @"<li><a href=""/User Manual/ePPS Users Manual.pdf"" target=""_blank""""><span class=""fa fa-file-text-o icon-reports-icon-epps-r""></span> Users Manual</a></li>";

                rtValue += @"<li><a href=""""><span class=""fa fa-file-text-o icon-reports-icon-epps-r""></span> Users Manual</a>";
               
                rtValue += @"<ul>";
                rtValue += @"<li><a href=""/User Manual/ePPS Users Manual.pdf"" target=""_blank"""">EPPS Users Manual</a></li>";
                rtValue += @"<li><a href=""/User Manual/EPPS ENDORSEMENTS QUICK GUIDE.pdf"" target=""_blank"""">EPPS Endorsement Quick Guide</a></li>";
                rtValue += @"</ul>";
                rtValue += @"</li>";


                //Logout
                rtValue += @"<li><a href=""RedirectByMenu.aspx?page=Logout""><span class=""fa fa-sign-out icon-logout-icon-epps-r""></span> Log Out</a></li>";
                rtValue += @"</ul>";

                Literal1.Text = rtValue;
            }
            catch (Exception e)
            {
                e.InnerException.ToString();
            }           
        }

        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (e.Item.Text.Trim() == "Main Menu")
            {
                Session.Clear();
                Response.Redirect("Search.aspx");
            }
            if (e.Item.Text.Trim() == "Auto Personal")
            {
                Session.Clear();
                TaskControl.QuoteAuto QA = new TaskControl.QuoteAuto(false);

                QA.IsMaster = false;
                QA.MasterPolicyID = ""; // "0206";
                QA.FileNumber = "27";
                QA.PolicyType = "PA ";
                QA.Agent = "001"; //
                QA.Policy.Agent = "001";
                QA.Term = 12;
                QA.Policy.Term = 12;
                QA.Policy.AutoAssignPolicy = true;
                QA.PolicyClassID = 3;
                QA.Policy.IsMaster = true;

                QA.Policy.MasterPolicyID = ""; // "0206";
                QA.Policy.FileNumber = "27";
                QA.Policy.PolicyType = "PA ";
                QA.IsPolicy = false;
                QA.Mode = 1; //ADD
                Session.Add("TaskControl", QA);
                Response.Redirect("ExpressAutoQuote.aspx", false);
            }

            if (e.Item.Text.Trim() == "Reports")
            {
                Session.Clear();
                Response.Redirect("PoliciesReports.aspx");
            }

            if (e.Item.Text.Trim() == "Search by ControlID")
            {
                Session.Clear();
                Response.Redirect("Search.aspx");
            }

            if (e.Item.Text.Trim() == "Search by Prospects")
            {
                Session.Clear();
                Response.Redirect("SearchProspect.aspx");
            }

            if (e.Item.Text.Trim() == "Search by Customers")
            {
                Session.Clear();
                Response.Redirect("SearchClient.aspx");
            }

            if (e.Item.Text.Trim() == "Search by Policies")
            {
                Session.Clear();
                Response.Redirect("SearchPolicies.aspx");
            }

            if (e.Item.Text.Trim() == "GuardianXtra")
            {
                Session.Clear();
                TaskControl.GuardianXtra GX = new TaskControl.GuardianXtra();

                GX.Mode = 1; //ADD

                Session.Add("TaskControl", GX);

                Response.Redirect("GuardianXtra.aspx");
            }

            if (e.Item.Text.Trim() == "Lookup Tables")
            {
                Session.Clear();
                Response.Redirect("LookupTableMaintenance.aspx");
            }

            if (e.Item.Text.Trim() == "User Properties List")
            {
                Session.Clear();
                Response.Redirect("UserPropertiesList.aspx");
            }

            if (e.Item.Text.Trim() == "Add User")
            {
                Session.Clear();
                EPolicy.Login.Login login = new EPolicy.Login.Login();
                login.Mode = (int)EPolicy.Login.Login.LoginMode.ADD;
                Session.Add("Login", login);
                Response.Redirect("UsersProperties.aspx");
            }

            if (e.Item.Value.Trim() == "GroupPermissions")
            {
                Session.Clear();
                Response.Redirect("GroupAndPermissions.aspx");
            }

            if (e.Item.Text.Trim() == "Logout")
            {
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                FormsAuthentication.SignOut();
                Response.Cookies.Add(authCookies);

                Session.Clear();
                Response.Redirect("Default.aspx?004");
            }

            if (e.Item.Text.Trim() == "Prospect")
            {
                Session.Clear();
                Customer.Prospect prospect = new Customer.Prospect();
                prospect.Mode = 1;
                prospect.IsBusiness = false;
                Session["Prospect"] = prospect;
                Response.Redirect("ProspectIndividual.aspx");
            }

            if (e.Item.Text.Trim() == "Customers")
            {
                Session.Clear();
                Customer.Customer customer = new Customer.Customer();
                customer.Mode = (int)Customer.Customer.CustomerMode.ADD;
                Session.Add("Customer", customer);
                Response.Redirect("ClientIndividual.aspx");
            }

            if (e.Item.Text.Trim() == "Auto Gap")
            {
                Session.Clear();
                TaskControl.Policies taskControl = new TaskControl.Policies();
                taskControl.Mode = 1; //ADD
                taskControl.PolicyClassID = 9;
                Session.Add("TaskControl", taskControl);
                Response.Redirect("Policies.aspx", false);
            }

            if (e.Item.Text.Trim() == "Auto Gap Master")
            {
                Session.Clear();
                TaskControl.Policies taskControl = new TaskControl.Policies();
                taskControl.Mode = 1; //ADD
                taskControl.PolicyClassID = 9;
                taskControl.IsMaster = true;
                Session.Add("TaskControl", taskControl);
                Response.Redirect("Policies.aspx", false);
            }

            if (e.Item.Text.Trim() == "First Bank - Client")
            {
                Session.Clear();
                TaskControl.QuoteAuto QA = new TaskControl.QuoteAuto(false);

                QA.IsMaster = true;
                QA.MasterPolicyID = "0004";
                QA.FileNumber = "10";
                QA.PolicyType = "MFC";
                QA.Agent = "398"; //FB
                QA.Term = 12; //FB

                QA.PolicyClassID = 3;
                QA.Policy.Agent = "398";
                QA.Policy.IsMaster = true;
                QA.Policy.MasterPolicyID = "0004";
                QA.Policy.FileNumber = "10";
                QA.Policy.PolicyType = "MFC";
                QA.IsPolicy = false;
                QA.Mode = 1; //ADD

                Session.Add("TaskControl", QA);
                Response.Redirect("ExpressAutoQuote.aspx", false);
            }

            if (e.Item.Text.Trim() == "First Bank - Employee")
            {
                Session.Clear();
                TaskControl.QuoteAuto QA = new TaskControl.QuoteAuto(false);

                QA.IsMaster = true;
                QA.MasterPolicyID = "0006";
                QA.FileNumber = "10";
                QA.PolicyType = "MFE";
                QA.Agent = "399"; //FB
                QA.Term = 12;

                QA.PolicyClassID = 3;
                QA.Policy.Agent = "399";
                QA.Policy.IsMaster = true;
                QA.Policy.MasterPolicyID = "0006";
                QA.Policy.FileNumber = "10";
                QA.Policy.PolicyType = "MFE";
                QA.IsPolicy = false;
                QA.Mode = 1; //ADD

                Session.Add("TaskControl", QA);
                Response.Redirect("ExpressAutoQuote.aspx", false);
            }

            if (e.Item.Text.Trim() == "Compulsory Insurance")
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
                Session.Clear();
                EPolicy.TaskControl.CompulsoryInsuranceQuote taskControl = new EPolicy.TaskControl.CompulsoryInsuranceQuote();
                taskControl.Mode = 1; //ADD
                taskControl.InsuranceCompany = "002";
                Session.Add("TaskControl", taskControl);
                Response.Redirect("CompulsoryInsuranceQuote.aspx", false);
            }


            if (e.Item.Text.Trim() == "New VI Quote")
            {
                Session.Clear();
                TaskControl.Autos taskControl = new TaskControl.Autos(true);
                taskControl.Mode = 1; //ADD
                taskControl.isQuote = true;
                taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Auto VI Quote"));
                Session.Add("TaskControl", taskControl);
                Response.Redirect("Autos.aspx");
            }

            if (e.Item.Text.Trim() == "Renewal VI Quote")
            {
                Session.Clear();
                TaskControl.Autos taskControl = new TaskControl.Autos(true);
                taskControl.Mode = 1; //ADD
                taskControl.isQuote = true;
                taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Auto VI Quote"));
                Session.Add("TaskControl", taskControl);
                Response.Redirect("Autos.aspx");
            }
        }


        //protected void TreeViewMenu_SelectedNodeChanged(object sender, EventArgs e)
        //{
        //    string a = TreeViewMenu.SelectedNode.Text;
        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Main Menu")
        //    {
        //        Session.Clear();
        //        Response.Redirect("MainMenu.aspx");
        //    }
        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Auto Personal")
        //    {
        //        Session.Clear();
        //        TaskControl.QuoteAuto QA = new TaskControl.QuoteAuto(false);

        //        QA.IsMaster = false;
        //        QA.MasterPolicyID = ""; // "0206";
        //        QA.FileNumber = "27";
        //        QA.PolicyType = "PA ";
        //        QA.Agent = "001"; //
        //        QA.Policy.Agent = "001";
        //        QA.Term = 12;
        //        QA.Policy.Term = 12;
        //        QA.Policy.AutoAssignPolicy = true;
        //        QA.PolicyClassID = 3;
        //        QA.Policy.IsMaster = true;

        //        QA.Policy.MasterPolicyID = ""; // "0206";
        //        QA.Policy.FileNumber = "27";
        //        QA.Policy.PolicyType = "PA ";
        //        QA.IsPolicy = false;
        //        QA.Mode = 1; //ADD
        //        Session.Add("TaskControl", QA);
        //        Response.Redirect("ExpressAutoQuote.aspx", false);
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Reports")
        //    {
        //        Session.Clear();
        //        Response.Redirect("PoliciesReports.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "By ControlID")
        //    {
        //        Session.Clear();
        //        Response.Redirect("Search.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "By Prospects")
        //    {
        //        Session.Clear();
        //        Response.Redirect("SearchProspect.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "By Customers")
        //    {
        //        Session.Clear();
        //        Response.Redirect("SearchClient.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "By Policies")
        //    {
        //        Session.Clear();
        //        Response.Redirect("SearchPolicies.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "GuardianXtra")
        //    {
        //        Session.Clear();
        //        TaskControl.GuardianXtra GX = new TaskControl.GuardianXtra();

        //        GX.Mode = 1; //ADD

        //        Session.Add("TaskControl", GX);

        //        Response.Redirect("GuardianXtra.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Lookup Tables")
        //    {
        //        Session.Clear();
        //        Response.Redirect("LookupTableMaintenance.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "User Properties List")
        //    {
        //        Session.Clear();
        //        Response.Redirect("UserPropertiesList.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Add User")
        //    {
        //        Session.Clear();
        //        EPolicy.Login.Login login = new EPolicy.Login.Login();
        //        login.Mode = (int)EPolicy.Login.Login.LoginMode.ADD;
        //        Session.Add("Login", login);
        //        Response.Redirect("UsersProperties.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Value.Trim() == "GroupPermissions")
        //    {
        //        Session.Clear();
        //        Response.Redirect("GroupAndPermissions.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Logout")
        //    {
        //        HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
        //        FormsAuthentication.SignOut();
        //        Response.Cookies.Add(authCookies);

        //        Session.Clear();
        //        Response.Redirect("Default.aspx?004");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Prospect")
        //    {
        //        Session.Clear();
        //        Customer.Prospect prospect = new Customer.Prospect();
        //        prospect.Mode = 1;
        //        prospect.IsBusiness = false;
        //        Session["Prospect"] = prospect;
        //        Response.Redirect("ProspectIndividual.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Customers")
        //    {
        //        Session.Clear();
        //        Customer.Customer customer = new Customer.Customer();
        //        customer.Mode = (int)Customer.Customer.CustomerMode.ADD;
        //        Session.Add("Customer", customer);
        //        Response.Redirect("ClientIndividual.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Auto Gap")
        //    {
        //        Session.Clear();
        //        TaskControl.Policies taskControl = new TaskControl.Policies();
        //        taskControl.Mode = 1; //ADD
        //        taskControl.PolicyClassID = 9;
        //        Session.Add("TaskControl", taskControl);
        //        Response.Redirect("Policies.aspx", false);
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Auto Gap Master")
        //    {
        //        Session.Clear();
        //        TaskControl.Policies taskControl = new TaskControl.Policies();
        //        taskControl.Mode = 1; //ADD
        //        taskControl.PolicyClassID = 9;
        //        taskControl.IsMaster = true;
        //        Session.Add("TaskControl", taskControl);
        //        Response.Redirect("Policies.aspx", false);
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "First Bank - Client")
        //    {
        //        Session.Clear();
        //        TaskControl.QuoteAuto QA = new TaskControl.QuoteAuto(false);

        //        QA.IsMaster = true;
        //        QA.MasterPolicyID = "0004";
        //        QA.FileNumber = "10";
        //        QA.PolicyType = "MFC";
        //        QA.Agent = "398"; //FB
        //        QA.Term = 12; //FB

        //        QA.PolicyClassID = 3;
        //        QA.Policy.Agent = "398";
        //        QA.Policy.IsMaster = true;
        //        QA.Policy.MasterPolicyID = "0004";
        //        QA.Policy.FileNumber = "10";
        //        QA.Policy.PolicyType = "MFC";
        //        QA.IsPolicy = false;
        //        QA.Mode = 1; //ADD

        //        Session.Add("TaskControl", QA);
        //        Response.Redirect("ExpressAutoQuote.aspx", false);
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "First Bank - Employee")
        //    {
        //        Session.Clear();
        //        TaskControl.QuoteAuto QA = new TaskControl.QuoteAuto(false);

        //        QA.IsMaster = true;
        //        QA.MasterPolicyID = "0006";
        //        QA.FileNumber = "10";
        //        QA.PolicyType = "MFE";
        //        QA.Agent = "399"; //FB
        //        QA.Term = 12;

        //        QA.PolicyClassID = 3;
        //        QA.Policy.Agent = "399";
        //        QA.Policy.IsMaster = true;
        //        QA.Policy.MasterPolicyID = "0006";
        //        QA.Policy.FileNumber = "10";
        //        QA.Policy.PolicyType = "MFE";
        //        QA.IsPolicy = false;
        //        QA.Mode = 1; //ADD

        //        Session.Add("TaskControl", QA);
        //        Response.Redirect("ExpressAutoQuote.aspx", false);
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Compulsory Insurance")
        //    {
        //        Login.Login cp = HttpContext.Current.User as Login.Login;
        //        int userID = 0;
        //        userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
        //        Session.Clear();
        //        EPolicy.TaskControl.CompulsoryInsuranceQuote taskControl = new EPolicy.TaskControl.CompulsoryInsuranceQuote();
        //        taskControl.Mode = 1; //ADD
        //        taskControl.InsuranceCompany = "002";
        //        Session.Add("TaskControl", taskControl);
        //        Response.Redirect("CompulsoryInsuranceQuote.aspx", false);
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Dwelling")
        //    {
        //        Session.Clear();
        //        TaskControl.Dwelling taskControl = new TaskControl.Dwelling(true);
        //        taskControl.Mode = 1; //ADD
        //        taskControl.IsOPPQuote = true;
        //        taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Dwelling Quote"));
        //        Session.Add("TaskControl", taskControl);
        //        Response.Redirect("Dwelling.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Personal Package")
        //    {
        //        Session.Clear();
        //        TaskControl.PersonalPackage taskControl = new TaskControl.PersonalPackage(true);
        //        taskControl.Mode = 1; //ADD
        //        taskControl.IsOPPQuote = true;
        //        taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Personal Package Quote"));
        //        Session.Add("TaskControl", taskControl);
        //        Response.Redirect("PersonalPackage.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "Professional Liability")
        //    {
        //        Session.Clear();
        //        TaskControl.ProfessionalLiability taskControl = new TaskControl.ProfessionalLiability(true);
        //        taskControl.Mode = 1; //ADD
        //        taskControl.IsQuote = true;
        //        taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Professional Liability Quote"));
        //        Session.Add("TaskControl", taskControl);
        //        Response.Redirect("ProfessionalLiability.aspx");
        //    }

        //    if (TreeViewMenu.SelectedNode.Text.Trim() == "New VI Quote")
        //    {
        //        Session.Clear();
        //        TaskControl.Autos taskControl = new TaskControl.Autos(true);
        //        taskControl.Mode = 1; //ADD
        //        taskControl.isQuote = true;
        //        taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Auto VI Quote"));
        //        Session.Add("TaskControl", taskControl);
        //        Response.Redirect("Autos.aspx");
        //    }
        //}

        //private void TreeViewMenu_Click(object sender, EventArgs e)
        //{
        //    string a = TreeViewMenu.SelectedNode.Text;
        //}
    }
}