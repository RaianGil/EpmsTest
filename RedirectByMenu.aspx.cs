using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using EPolicy.TaskControl;

namespace EPolicy
{
    public partial class RedirectByMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            if (cp == null)
            {
                Response.Redirect("Default.aspx?001");
            }

            string page = Request.QueryString["page"];

            switch (page)
            {
                case "Home":
                    Session.Clear();
                    Response.Redirect("MainMenu.aspx");
                    break;

                case "By ControlID":
                    Session.Clear();
                    Response.Redirect("Search.aspx");
                    break;

                case "By Customers":
                    Session.Clear();
                    Response.Redirect("SearchClient.aspx");
                    break;

                case "By Policies":
                    Session.Clear();
                    Response.Redirect("SearchPolicies.aspx");
                    break;

                case "Customers":
                    Session.Clear();
                    Customer.Customer customer = new Customer.Customer();
                    customer.Mode = (int)Customer.Customer.CustomerMode.ADD;
                    Session.Add("Customer", customer);
                    Response.Redirect("ClientIndividual.aspx");
                    break;

                case "Auto Personal":
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
                    break;

                case "Dashboard":
                    Session.Clear();
                    Response.Redirect("Dashboard.aspx");
                    break;

                case "New VI Quote":
                    Session.Clear();
                    TaskControl.Autos taskControl = new TaskControl.Autos(true);
                    taskControl.Mode = 1; //ADD
                    taskControl.isQuote = true;
                    taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Auto VI Quote"));
                    Session.Add("TaskControl", taskControl);
                    Response.Redirect("Autos.aspx");
                    break;

                case "Auto High Limit":
                    Session.Clear();
                    TaskControl.AutoHighLimit AHL = new TaskControl.AutoHighLimit(true);
                    AHL.Mode = 1; //ADD
                    AHL.isQuote = true;
                    AHL.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Auto High Limit Quote"));
                    Session.Add("TaskControl", AHL);
                    Response.Redirect("AutoHighLimit.aspx");
                    break;


                case "DoubleInterest":
                    Session.Clear();
                    TaskControl.DoubleInterest DPI = new TaskControl.DoubleInterest(true);
                    DPI.Mode = 1; //ADD
                    DPI.isQuote = true;
                    DPI.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Double Interest Policy Quote"));
                    Session.Add("TaskControl", DPI);
                    Response.Redirect("DoubleInterest.aspx");
                    break;

                case "GuardianXtra":
                    Session.Clear();
                    TaskControl.GuardianXtra GX = new TaskControl.GuardianXtra();
                    GX.Mode = 1; //ADD
                    Session.Add("TaskControl", GX);
                    Response.Redirect("GuardianXtra.aspx");
                    break;

                case "GuardianRoadAssist":
                    Session.Clear();
                    TaskControl.RoadAssistance GX2 = new TaskControl.RoadAssistance();
                    GX2.Mode = 1; //ADD
                    Session.Add("TaskControl", GX2);
                    Response.Redirect("RoadAssistance.aspx");
                    break;

                case "Reports":
                    Session.Clear();
                    Response.Redirect("PoliciesReports.aspx");
                    break;

                case "LookupTables":
                    Session.Clear();
                    Response.Redirect("LookupTableMaintenance.aspx");
                    break;

                case "UserPropertiesList":
                    Session.Clear();
                    Response.Redirect("UserPropertiesList.aspx");
                    break;

                case "AddUser":
                    Session.Clear();
                    EPolicy.Login.Login login = new EPolicy.Login.Login();
                    login.Mode = (int)EPolicy.Login.Login.LoginMode.ADD;
                    Session.Add("Login", login);
                    Response.Redirect("UsersProperties.aspx");
                    break;

                case "GroupPermissions":
                    Session.Clear();
                    Response.Redirect("GroupAndPermissions.aspx");
                    break;

                case "Logout":
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    FormsAuthentication.SignOut();
                    Response.Cookies.Add(authCookies);
                    Session.Clear();
                    Response.Redirect("Default.aspx?004");
                    break;

                case "Renewals from PPS":
                    Session.Clear();
                    TaskControl.Autos RVI = new TaskControl.Autos(true);
                    RVI.Mode = 1; //ADD
                    RVI.isQuote = true;
                    RVI.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Auto VI Quote"));
                    Session.Add("TaskControl", RVI);
                    Response.Redirect("RenewalSearch.aspx");
                    break;

                case "Premium Finance":
                    Session.Clear();
                    Response.Redirect("PremiumFinance.aspx");
                    break;
					
				case "HomeOwners":
                    Session.Clear();
                    TaskControl.HomeOwners HM = new TaskControl.HomeOwners(true);
                    HM.Mode = 1; //ADD
                    HM.isQuote = true;
                    HM.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Home Owners Policy Quote"));
                    Session.Add("TaskControl", HM);
                    Response.Redirect("HomeOwners.aspx");
                    break;

                case "Renewal from PPS HomeOwners":
                    Session.Clear();
                    TaskControl.HomeOwners RHM = new TaskControl.HomeOwners(true);
                    RHM.Mode = 1; //ADD
                    RHM.isQuote = true;
                    RHM.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Home Owners Policy Quote"));
                    Session.Add("TaskControl", RHM);
                    Response.Redirect("RenewalHomeOwnerSearch.aspx");
                    break;

                case "Bonds":
                    Session.Clear();
                    TaskControl.Bonds Bnds = new TaskControl.Bonds(true);
                    Bnds.Mode = 1; //ADD
                    Bnds.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Bonds Quote"));
                    Session.Add("TaskControl", Bnds);
                    Response.Redirect("Bonds.aspx");
                    break;

                case "Yacht":
                    Session.Clear();
                    TaskControl.Yacht Yat = new TaskControl.Yacht(true);
                    Yat.Mode = 1; //ADD
                    Yat.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Yacht Quote"));
                    Session.Add("TaskControl", Yat);
                    Response.Redirect("Yacht.aspx");
                    break;

                default:
                    break;
            }

        }
    }
}