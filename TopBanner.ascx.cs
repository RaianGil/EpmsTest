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
    public partial class TopBanner2 : System.Web.UI.UserControl
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
                NavigationMenu.Visible = true;
                imgGuessWho.Visible = true;
                //this.Img1.Visible = false;
                //this.Img2.Visible = true;
                //this.Img3.Visible = true;
                //LblOficina.Text = LookupTables.LookupTables.GetDescription("Location",locationID.ToString());
                VerifyAccess(cp);
            }
            else
            {
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                FormsAuthentication.SignOut();
                Response.Cookies.Add(authCookies);

                this.lblUserName.Visible = true;
                this.lblUserName.Text = string.Empty;
                NavigationMenu.Visible = false;
                imgGuessWho.Visible = false;
                //this.Img1.Visible = true;
                //this.Img2.Visible = false;
                //this.Img3.Visible = false;
                //LblOficina.Text = string.Empty;               
            }
        }

      
        private void VerifyAccess(EPolicy.Login.Login cp)
        {
            if (!cp.IsInRole("ADMINISTRATOR"))
            {
                NavigationMenu.Items.Remove(NavigationMenu.FindItem("Administration"));
            }
            else
            {
                if (!cp.IsInRole("ADMINISTRATOR"))
                {
                    MenuItem item = null;

                    if (!cp.IsInRole("LOOKUPTABLE MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                    {
                        item = NavigationMenu.FindItem("Administration/Lookup Tables"); //Prospect
                        item.Parent.ChildItems.Remove(item);
                    }

                    if (!cp.IsInRole("USER PROPERTIES LIST MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                    {
                        item = NavigationMenu.FindItem("Administration/User Properties List"); //Customers
                        item.Parent.ChildItems.Remove(item);
                    }

                    if (!cp.IsInRole("ADD USER MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                    {
                        item = NavigationMenu.FindItem("Administration/Add User"); //Auto Personal
                        item.Parent.ChildItems.Remove(item);
                    }

                    if (!cp.IsInRole("GROUP PERMISSION MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                    {
                        item = NavigationMenu.FindItem("Administration/GroupPermissions"); //Personal Package
                        item.Parent.ChildItems.Remove(item);
                    }
                }
            }

            if (!cp.IsInRole("NEW TRANSACTION MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
            {
                NavigationMenu.Items.Remove(NavigationMenu.FindItem("New Transaction"));
                //MenuItem item = NavigationMenu.FindItem("New Transaction"); //New Transaction
                //item.Parent.ChildItems.Remove(item);
            }
            else
            {
                if (!cp.IsInRole("ADMINISTRATOR"))
                {
                    MenuItem item = null;

                    if (!cp.IsInRole("PROSPECT MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                    {
                        item = NavigationMenu.FindItem("New Transaction/Prospect"); //Prospect
                        item.Parent.ChildItems.Remove(item);
                    }

                    if (!cp.IsInRole("CUSTOMER MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                    {
                        item = NavigationMenu.FindItem("New Transaction/Customers"); //Customers
                        item.Parent.ChildItems.Remove(item);
                    }

                    if (!cp.IsInRole("AUTO PERSONAL MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                    {
                        item = NavigationMenu.FindItem("New Transaction/Auto Personal"); //Auto Personal
                        item.Parent.ChildItems.Remove(item);
                    }

                    if (!cp.IsInRole("PERSONAL PACKAGE MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                    {
                        //item = NavigationMenu.FindItem("New Transaction/Personal Package"); //Personal Package
                        //item.Parent.ChildItems.Remove(item);
                    }

                    if (!cp.IsInRole("AUTOS VI MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                    {
                        item = NavigationMenu.FindItem("New Transaction/Autos VI"); //Autos VI
                        item.Parent.ChildItems.Remove(item);
                    }
                }
            }


            if (!cp.IsInRole("SEARCH MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
            {
                //NavigationMenu.Items.Remove(NavigationMenu.FindItem("Search").ChildItems[0]); //Search by ControlID
                //NavigationMenu.Items.Remove(NavigationMenu.FindItem("Search").ChildItems[1]); //Search by Prospects
                //NavigationMenu.Items.Remove(NavigationMenu.FindItem("Search").ChildItems[2]); //Search by Customers
                //NavigationMenu.Items.Remove(NavigationMenu.FindItem("Search").ChildItems[3]); //Search by Policies
                NavigationMenu.Items.Remove(NavigationMenu.FindItem("Search"));
            }
            else
            {
                MenuItem item = null;

                if (!cp.IsInRole("SEARCH CONTROL MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    item = NavigationMenu.FindItem("Search/Search by ControlID"); //Search by ControlID
                    item.Parent.ChildItems.Remove(item);
                }

                if (!cp.IsInRole("SEARCH PROSPECT MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    item = NavigationMenu.FindItem("Search/Search by Prospects"); //Search by Prospects
                    item.Parent.ChildItems.Remove(item);
                }

                if (!cp.IsInRole("SEARCH CUSTOMER MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    item = NavigationMenu.FindItem("Search/Search by Customers"); //Search by Customers
                    item.Parent.ChildItems.Remove(item);
                }

                if (!cp.IsInRole("SEARCH POLICIES MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    item = NavigationMenu.FindItem("Search/Search by Policies"); //Search by Policies
                    item.Parent.ChildItems.Remove(item);
                }
            }

            if (!cp.IsInRole("REPORT MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
            {
                NavigationMenu.Items.Remove(NavigationMenu.FindItem("Reports"));
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

            if (e.Item.Text.Trim() == "XML Policy")
            {
                Session.Clear();
                Response.Redirect("XMLPolicy.aspx");
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


            if (e.Item.Text.Trim() == "New Quote")
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
    }
}