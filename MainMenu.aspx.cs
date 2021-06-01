using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;

namespace EPolicy
{
    /// <summary>
    /// Summary description for MainMenu.
    /// </summary>
    public partial class MainMenu : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            if (cp == null)
            {
                Response.Redirect("Default.aspx?001");
            }
            else
            {
                VerifyAccess(cp);
            }
        }

        private void VerifyAccess(Login.Login cp)
        {

        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);

            //Setup top Banner
            Control Banner = new Control();
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.phTopBanner.Controls.Add(Banner);

            //Setup top Banner
            Control BannerLIST = new Control();
            BannerLIST = LoadControl(@"TODOLIST.ascx");
            this.PlaceHolder1.Controls.Add(BannerLIST);

            //Control leftMenu = new Control();
            //leftMenu = LoadControl(@"LeftMenu.ascx");
            //this.phTopBanner1.Controls.Add(leftMenu);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected void Button2_Click(object sender, System.EventArgs e)
        {
            Session.Clear();
            HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
            Response.Cookies.Add(authCookies);
            FormsAuthentication.SignOut();
            Response.Redirect("Default.aspx", false);
        }

        protected void Button1_Click(object sender, System.EventArgs e)
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

        protected void BtnLogIn_Click(object sender, System.EventArgs e)
        {
            Session.Clear();

            Customer.Customer customer = new Customer.Customer();
            customer.Mode = (int)Customer.Customer.CustomerMode.ADD;
            Session.Add("Customer", customer);

            Response.Redirect("ClientIndividual.aspx");
        }

        protected void Button5_Click(object sender, System.EventArgs e)
        {
            Session.Clear();
            Customer.Prospect prospect = new Customer.Prospect();
            prospect.Mode = 1;
            prospect.IsBusiness = false;
            Session["Prospect"] = prospect;
            Response.Redirect("ProspectIndividual.aspx");
        }

        private void Button3_Click(object sender, System.EventArgs e)
        {
            Session.Clear();

            TaskControl.AutoGap autoGap = new TaskControl.AutoGap();
            autoGap.Mode = 1;
            Session.Add("AutoGap", autoGap);

            Response.Redirect("AutoGap.aspx");
        }

        protected void BtnSalir_Click(object sender, System.EventArgs e)
        {
            Session.Clear();
            Response.Redirect("PoliciesReports.aspx");
        }


        protected void btnAutoGap_Click(object sender, System.EventArgs e)
        {
            Session.Clear();
            TaskControl.Policies taskControl = new TaskControl.Policies();

            taskControl.Mode = 1; //ADD
            taskControl.PolicyClassID = 9;

            Session.Add("TaskControl", taskControl);
            Response.Redirect("Policies.aspx", false);

        }

        protected void HtmlIconAutoVI_Click(object sender, EventArgs e)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            if (LookupTables.LookupTables.GetDescription("Location", Login.Login.GetLocationByUserID(cp.UserID).ToString()).Contains("THOMAS"))
            {
                Session.Clear();
                TaskControl.Autos taskControl = new TaskControl.Autos(true);
                taskControl.Mode = 1; //ADD
                taskControl.isQuote = true;
                taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Auto VI Quote"));
                Session.Add("TaskControl", taskControl);
                Response.Redirect("Autos.aspx");
            }
            else
            {
                if (cp.IsInRole("BONDS"))
                {
                    Session.Clear();
                    TaskControl.Bonds Bnds = new TaskControl.Bonds(true);
                    Bnds.Mode = 1; //ADD
                    Bnds.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Bonds Quote"));
                    Session.Add("TaskControl", Bnds);
                    Response.Redirect("Bonds.aspx");
                }
                else
                {
                    Session.Clear();
                    TaskControl.GuardianXtra GX = new TaskControl.GuardianXtra();
                    GX.Mode = 1; //ADD
                    Session.Add("TaskControl", GX);
                    Response.Redirect("GuardianXtra.aspx");
                }
            }
        }
    }
}
