using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using System.Security.Principal;
using EPolicy.Login;

namespace EPolicy 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
            Application.Add("MaxDailyTransactions", Convert.ToInt32("10"));
            Application.Add("LastUpdateTime", DateTime.Now);
            Application.Add("TransactionLimiter", new DailyTransactionLimiter());
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
			string cookieName = FormsAuthentication.FormsCookieName;
			HttpCookie authCookie = Context.Request.Cookies[cookieName];
			if(authCookie==null)
			{
				return;
			}

			FormsAuthenticationTicket authTicket= null;
			try
			{
				authTicket = FormsAuthentication.Decrypt(authCookie.Value);
			}
			catch
			{
				return;
			}

			if (authTicket == null)
			{
				return;
			}
			
			string[] roles = authTicket.UserData.Split('|');

			FormsIdentity id = new FormsIdentity(authTicket);
			//Login.Login principal = new Login.Login(id,roles);

            Login.Login principal = new Login.Login(id, id.Name.Split("|".ToCharArray())[1].ToString());

			Context.User = principal;
		}

		protected void Application_Error(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}

