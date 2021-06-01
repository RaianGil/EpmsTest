using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Xml;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPolicy;
using EPolicy.Customer;
using EPolicy.TaskControl;
using Baldrich.DBRequest;
using OPPReport;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using EPolicy.Quotes;
using EPolicy.XmlCooker;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Web.Services;
using System.Configuration;
using System.Xml.Schema;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace EPolicy
{
    /// <summary>
    /// Summary description for AutoGuardServicesContractReport.
    /// </summary>
    public partial class ChangePassword : System.Web.UI.Page
    {
        #region Web Form Designer Generated Code

        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);

        }

        //protected void Page_Unload(object sender, EventArgs e)
        //{
        //    Session.Abandon();
        //}

        private void InitializeComponent()
        {

        }

        #endregion Web Form Designer Generated Code

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.litPopUp.Visible = false;
            Session["ChangePassword"] = true;
            Control Banner = new Control();
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.phTopBanner.Controls.Add(Banner);

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), Guid.NewGuid().ToString(), "SetWaitCursor();", true);

            Login.Login cp = HttpContext.Current.User as Login.Login;

            if (cp == null)
            {
                Response.Redirect("Default.aspx?001");
            }
            else
            {
               //if (!cp.IsInRole("CHANGEPASSWORD") && !cp.IsInRole("ADMINISTRATOR"))
               //{
               //    Response.Redirect("Default.aspx?001");
               //}
                if(Session["Username"] != null)
                {
                    TxtUserName.Text = Session["Username"].ToString();
                }
            }
        }

        protected void BtnSave_Click(object sender, System.EventArgs e)
        {
            Login.Login login = new Login.Login();

            try
            {
                bool IsUserAuthenticate = false;
                string encrypt = FormsAuthentication.HashPasswordForStoringInConfigFile(TxtPassword.Text.Trim(), "SHA1");
                string newPasswordText = TxtNewPassword.Text;
                TxtPassword.Text = "";

                if (login.GetAuthenticatedUser(TxtUserName.Text, encrypt))
                {
                    string encryptNewPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(TxtNewPassword.Text.Trim(), "SHA1");
                    string encryptConfirmPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(TxtConfirmPassword.Text.Trim(), "SHA1");

                    login.SavePasword(encryptNewPassword, encryptConfirmPassword, encrypt, newPasswordText);

                    IsUserAuthenticate = login.GetAuthenticatedUser(TxtUserName.Text.Trim(), encryptNewPassword);
                }

                if (IsUserAuthenticate)
                {



                    TxtUserName.Text = "";
                    TxtPassword.Text = "";
                    TxtNewPassword.Text = "";
                    TxtConfirmPassword.Text = "";

                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, login.UserName + "|" + login.UserID.ToString(), DateTime.Now, DateTime.Now.AddHours(10), false, ""); //login.GetRole());
                    //FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, login.FirstName.Trim() + " " + login.LastName.Trim() + "|" + login.UserName.Trim(), DateTime.Now, DateTime.Now.AddHours(1), false, ""); //login.GetRole());
                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookies);

                    Session["ChangePassword"] = null;
                    Response.Redirect("MainMenu.aspx", true);

                }

            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
                //this.litPopUp.Text = Utilities.MakeLiteralPopUpString(exp.Message);
                //this.litPopUp.Visible = true;
            }
        }

        protected void BtnExit_Click(object sender, System.EventArgs e)
        {
            FormsAuthentication.SignOut();
            HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
            Response.Cookies.Add(authCookies);

            Session.Add("WrongPass", "Usuario Desconectado Satisfactoria!");

            Response.Redirect("Default.aspx?004", true);
        }

        private void btnSearch_Click(object sender, System.EventArgs e)
        {

        }
    }
}