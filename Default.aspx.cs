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
using System.Net;
using System.Web.Security;
using System.IO;
using System.Text;
using EPolicy.Login;
using System.Threading;
using System.Timers;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace EPolicy
{
    /// <summary>
    /// Summary description for WebForm1.
    /// </summary>
    public partial class Default : System.Web.UI.Page
    {
        #region Page Pre-Init: force uplevel browser setting
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (BrowserCompatibility.IsUplevel)
            {
                Page.ClientTarget = "uplevel";
            }
        }
        #endregion


        protected void Page_Load(object sender, System.EventArgs e)
        {
            //this.Form.DefaultButton = this.BtnLogIn.UniqueID;
            this.litPopUp.Visible = false;
            Login.Login cp = HttpContext.Current.User as Login.Login;

            if (cp != null)
            {
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                Response.Cookies.Add(authCookies);
                FormsAuthentication.SignOut();

                TxtUserName.Enabled = true;
                TxtPassword.Enabled = true;
                this.BtnLogIn.Visible = true;
                this.BtnSalir.Visible = false;
            }
            else
            {
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                Response.Cookies.Add(authCookies);
                FormsAuthentication.SignOut();

                TxtUserName.Enabled = true;
                TxtPassword.Enabled = true;
                this.BtnLogIn.Visible = true;
                this.BtnSalir.Visible = false;
            }

            if (!IsPostBack)
            {
                if (this.Request.QueryString.ToString().Trim() == "001")
                {
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    FormsAuthentication.SignOut();
                    Response.Cookies.Add(authCookies);

                    lblRecHeader.Text = "You don't have privileges to view this page.";
                    mpeSeleccion.Show();
                }

                else if (this.Request.QueryString.ToString().Trim() == "002") //Error entrando el username o password.
                {
                    if (Session["WrongPass"] != null)
                    {
                        string message = (string)Session["WrongPass"];
                        lblRecHeader.Text = message;
                        mpeSeleccion.Show();
                        Session.Remove("WrongPass");
                    }
                }
                else if (this.Request.QueryString.ToString().Trim() == "003") //Under Construction.
                {
                    if (Session["WrongPass"] != null)
                    {
                        string message = (string)Session["WrongPass"];
                        lblRecHeader.Text = message;
                        mpeSeleccion.Show();
                        Session.Remove("WrongPass");
                    }
                }

                else if (this.Request.QueryString.ToString().Trim() == "004") //Logout Message.
                {
                    if (Session["WrongPass"] != null)
                    {
                        string message = (string)Session["WrongPass"];
                        lblRecHeader.Text = message;
                        mpeSeleccion.Show();
                        Session.Remove("WrongPass");
                    }
                }

                else if (this.Request.QueryString.ToString().Trim() == "005") //Misc. Message.
                {
                    if (cp == null)
                    {
                        HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                        Response.Cookies.Add(authCookies);
                        FormsAuthentication.SignOut();

                        Response.Redirect("Default.aspx?001");
                    }
                    else
                    {
                        if (!cp.IsInRole("MENSAJES") && !cp.IsInRole("ADMINISTRATOR"))
                        {
                            Session.Remove("Message");
                        }
                        else
                        {
                            string MenSol = ConfigurationManager.AppSettings["MENSAJESOLICITADOR"];
                            if (MenSol == "True" && Session["Message"] != null)
                            {
                                Session.Remove("Message");
                                string js = "<script language=javascript> javascript:popwindow=window.open('Mensaje.aspx','popwindow','toolbar=no,location=center,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,copyhistory=no,width=387,height=145');popwindow.focus(); </script>";
                                Response.Write(js);
                            }
                        }
                    }
                }

                else if (this.Request.QueryString.ToString().Trim() == "006") //Go To Main Menu when Login.
                {
                    if (Session["Message"] != null)
                    {
                        Session.Remove("Message");
                        Response.Redirect("MainMenu.aspx");
                    }
                }

                else if (this.Request.QueryString.ToString().Trim() == "007")
                {
                    lblRecHeader.Text = "Session Lost, Please Login Again";
                    mpeSeleccion.Show();
                }

                try
                {
                    //SingleSignOn SSO
                    //domian:nationalgroup.local
                    if (HttpContext.Current.Request.Url.ToString().Trim() == "http://172.16.2.92" || HttpContext.Current.Request.Url.ToString().Trim() == "http://172.16.2.92/default.aspx" ||
                        HttpContext.Current.Request.Url.ToString().Trim().Substring(0, 18) == "http://172.16.2.92")
                    {			
                        LoginADUser();
                    }
                    else
                        TxtUserName.Text = "";
                }
                catch (Exception ecp)
                {
                    lblRecHeader.Text = "This Account can't be validate against the Active Directory, Please verify with the Administrator.";
                    mpeSeleccion.Show();
                }
            }
        }

        private void LoginADUser()
        {
            try
            {
                string name = System.Web.HttpContext.Current.User.Identity.Name;
                name = name.Split('\\')[1];
                TxtUserName.Text = name;
                //if (Request.QueryString["u465s8789e987e867r87i9667d"] != null)
                //{
                //    string qst = Request.QueryString["u465s8789e987e867r87i9667d"].Trim();
                //    TxtUserName.Text = qst.Trim();
                //}
                //TxtUserName.Text ="Franchesca.cruz";

                //System.DirectoryServices.AccountManagement.UserPrincipal user = System.DirectoryServices.AccountManagement.UserPrincipal.Current;
                //TxtUserName.Text = user.SamAccountName;

                //TxtUserName.Text = HttpContext.Current.User;
                //TxtUserName.Text = System.Environment.UserName;//user.SamAccountName;

                //System.Security.Principal.WindowsIdentity UserSecurtityPrincipal = Page.Request.LogonUserIdentity;
                //TxtUserName.Text = UserSecurtityPrincipal.Name;

                //if (IsAuthenticated("nationalgroup.local", TxtUserName.Text, ""))
                //{
                    AuthenticateUserAD(true, TxtUserName.Text);
                    //Login.Login login = new Login.Login();
                    //Console.WriteLine(login.GetAuthenticatedUserAD("victor.lanza").ToString());
                //}
                //else
                //{
                //    lblRecHeader.Text = "This Account can't be validate against the Active Directory, Please verify with the Administrator.";
                //    mpeSeleccion.Show();
                //}

            }
            catch (Exception ecp)
            {
                lblRecHeader.Text = "This Account can't be validate against the Active Directory, Please verify with the Administrator.";
                mpeSeleccion.Show();
            }
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
            //Control Banner = new Control();
            //Banner = LoadControl(@"TopBanner.ascx");
            //this.phTopBanner.Controls.Add(Banner);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        private bool IsAuthenticated(string domain, string username, string pwd)
        {
            bool userExists = false;
            using (var context = new System.DirectoryServices.AccountManagement.PrincipalContext(ContextType.Domain, domain))
            {
                using (var searcher = new System.DirectoryServices.AccountManagement.PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        System.DirectoryServices.DirectoryEntry de = result.GetUnderlyingObject() as System.DirectoryServices.DirectoryEntry;
                        string us = (string)de.Properties["samAccountName"].Value;
                        if (us == username)
                        {
                            //Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
                            //Console.WriteLine("Last Name : " + de.Properties["sn"].Value);
                            //Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
                            //Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
                            //Console.WriteLine();
                            userExists = true;
                        }
                    }
                }
            }

            return userExists;
        }

        protected void BtnLogIn_Click(object sender, System.EventArgs e)
        {
            AuthenticateUser(false);
        }

        private void AuthenticateUserAD(bool IsADUsername, string usernameAD)
        {
            EPolicy.Login.Login login = new EPolicy.Login.Login();
            bool IsUserAuthenticate = false;
            try
            {
                string encrypt = FormsAuthentication.HashPasswordForStoringInConfigFile(TxtPassword.Text.Trim(), "SHA1");
                TxtPassword.Text = "";

                TxtUserName.Text = usernameAD;
                IsUserAuthenticate = login.GetAuthenticatedUserAD(TxtUserName.Text.Trim());

                if (IsUserAuthenticate)
                {
                    if ((login.AllDay) ||
                        (DateTime.Parse(login.Time1) <= DateTime.Parse(DateTime.Now.ToShortTimeString()) &&
                        DateTime.Parse(login.Time2) >= DateTime.Parse(DateTime.Now.ToShortTimeString()) &&
                        TiempoLimitadoAcceso(login)))
                    {
                        if (!login.ChangePassword)
                        {
                            //La propeidad IsPersistent se cambio a true para que el cookies dure solo 10 horas. Asi el usuario debe de hacer login de nuevo.
                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, login.UserName + "|" + login.UserID.ToString(), DateTime.Now, DateTime.Now.AddHours(10), false, ""); //login.GetRole());

                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                            HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                            Response.Cookies.Add(authCookies);

                            TxtUserName.Text = "";
                            TxtPassword.Text = "";
                            TxtUserName.Enabled = false;
                            TxtPassword.Enabled = false;
                            BtnLogIn.Visible = false;

                            Session.Add("Message", "message");
                        }
                        else
                        {
                            //For user that have change password.
                            //throw new Exception("Por favor debes de cambiar su contraseña.");
                            FormsAuthentication.SignOut();
                            Response.Redirect("ChangePassword.aspx", true);
                        }
                    }
                    else
                    {
                        //Restriccion de horario.
                        throw new Exception("Usted esta restringido para entrar al sistema a esta hora.");
                    }
                }
            }
            catch (Exception exp)
            {
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                Response.Cookies.Add(authCookies);

                throw new Exception("");
            }
            finally
            {
                if (IsUserAuthenticate)
                {
                    //Verifica si la contraseña expira en 60 dias.
                    if (login.Dias30 == true)
                    {
                        int mdays = DateTime.Now.DayOfYear - login.Dias30Date.DayOfYear;

                        if (mdays >= 60)
                        {
                            FormsAuthentication.SignOut();
                            Response.Redirect("ChangePassword.aspx", true);
                        }
                    }

                    Response.Redirect("MainMenu.aspx");
                }
            }
        }

        private void AuthenticateUser(bool IsADUsername)
        {
            Login.Login login = new Login.Login();
            try
            {
                string encrypt = FormsAuthentication.HashPasswordForStoringInConfigFile(TxtPassword.Text.Trim(), "SHA1");
                TxtPassword.Text = "";

                bool IsUserAuthenticate = false;

                IsUserAuthenticate = login.GetAuthenticatedUser(TxtUserName.Text.Trim(), encrypt);

                if (IsUserAuthenticate)
                {
                    if ((login.AllDay) ||
                        (DateTime.Parse(login.Time1) <= DateTime.Parse(DateTime.Now.ToShortTimeString()) &&
                        DateTime.Parse(login.Time2) >= DateTime.Parse(DateTime.Now.ToShortTimeString()) &&
                        TiempoLimitadoAcceso(login)))
                    {
                        if (!login.ChangePassword)
                        {
                            //La propeidad IsPersistent se cambio a true para que el cookies dure solo 10 horas. Asi el usuario debe de hacer login de nuevo.
                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, login.UserName + "|" + login.UserID.ToString(), DateTime.Now, DateTime.Now.AddHours(10), false, ""); //login.GetRole());

                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                            HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                            Response.Cookies.Add(authCookies);

                            TxtUserName.Text = "";
                            TxtPassword.Text = "";
                            TxtUserName.Enabled = false;
                            TxtPassword.Enabled = false;
                            BtnLogIn.Visible = false;

                            Session.Add("Message", "message");
                        }
                        else
                        {
                            ////For user that have change password.
                            ////throw new Exception("Por favor debes de cambiar su contraseña.");
                            //FormsAuthentication.SignOut();
                            //La propeidad IsPersistent se cambio a true para que el cookies dure solo 10 horas. Asi el usuario debe de hacer login de nuevo.
                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, login.UserName + "|" + login.UserID.ToString(), DateTime.Now, DateTime.Now.AddHours(10), false, ""); //login.GetRole());

                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                            HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                            Response.Cookies.Add(authCookies);

                            Session["Username"] = TxtUserName.Text.ToString().Trim();
                            TxtUserName.Text = "";
                            TxtPassword.Text = "";
                            TxtUserName.Enabled = false;
                            TxtPassword.Enabled = false;
                            BtnLogIn.Visible = false;

                            Response.Redirect("ChangePassword.aspx", false);
                            return;
                        }
                    }
                    else
                    {
                        //Restriccion de horario.
                        throw new Exception("Usted esta restringido para entrar al sistema a esta hora.");
                    }
                }
            }
            catch (Exception exp)
            {
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                Response.Cookies.Add(authCookies);

                Session.Add("WrongPass", exp.Message);
                Response.Redirect("Default.aspx?002");
            }

            //Verifica si la contraseña expira en 60 dias.
            if (login.Dias30 == true)
            {
                int mdays = DateTime.Now.DayOfYear - login.Dias30Date.DayOfYear;

                if (mdays >= 60)
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("ChangePassword.aspx", true);
                }
            }
            Session["ChangePassword"] = null;
            Response.Redirect("MainMenu.aspx");
        }

        private bool TiempoLimitadoAcceso(Login.Login login)
        {
            string dayofweek = Utilities.DayofWeek(DateTime.Now.DayOfWeek.ToString().Trim().ToUpper());

            bool accessDays = false;

            switch (dayofweek)
            {
                case "DOMINGO":
                    if (login.Domingo)
                        accessDays = true;
                    else
                        accessDays = false;
                    break;
                case "LUNES":
                    if (login.Lunes)
                        accessDays = true;
                    else
                        accessDays = false;
                    break;
                case "MARTES":
                    if (login.Martes)
                        accessDays = true;
                    else
                        accessDays = false;
                    break;
                case "MIERCOLES":
                    if (login.Miercoles)
                        accessDays = true;
                    else
                        accessDays = false;
                    break;
                case "JUEVES":
                    if (login.Jueves)
                        accessDays = true;
                    else
                        accessDays = false;
                    break;
                case "VIERNES":
                    if (login.Viernes)
                        accessDays = true;
                    else
                        accessDays = false;
                    break;
                case "SABADO":
                    if (login.Sabado)
                        accessDays = true;
                    else
                        accessDays = false;
                    break;
            }
            return accessDays;
        }

        protected void BtnSalir_Click(object sender, System.EventArgs e)
        {
            HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
            Response.Cookies.Add(authCookies);

            Session.Add("WrongPass", "Exit succesfully!");
            Response.Redirect("Default.aspx?004");
        }
    }
}
