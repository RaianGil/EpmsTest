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
using EPolicy.LookupTables;

namespace EPolicy
{
	/// <summary>
	/// Summary description for UserPropertiesGroup.
	/// </summary>
	public partial class UserPropertiesGroup : System.Web.UI.Page
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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.DtgAuthenticatedGroup.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DtgAuthenticatedGroup_ItemCommand);

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.litPopUp.Visible = false;

			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
				Response.Redirect("Default.aspx?001");
			}
			else
			{
				if(!cp.IsInRole("ADD USER MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("Default.aspx?001");
				}
			}

			if(!IsPostBack)
			{
				Session.Add("AutoPostBack", (bool) true);

				DataTable dtAuthenticateGroup	  = LookupTables.LookupTables.GetTable("AuthenticatedGroup");
				Session.Add("DtAuthenticateGroup",dtAuthenticateGroup);

				DtgAuthenticatedGroup.DataSource = dtAuthenticateGroup;
				DtgAuthenticatedGroup.DataBind();
			}
		}

		private void DtgAuthenticatedGroup_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.Item.ItemType.ToString() != "Pager") // Select
			{
				Login.Login login = (Login.Login) Session["Login"];
				try
				{
					login.AddAuthenticatedGroupUse(int.Parse(e.Item.Cells[1].Text),e.Item.Cells[2].Text.Trim());
					
					login = Login.Login.GetUser(login.UserID);
					Session.Add("Login",login);

                    //string url = "UserPropertiesGroup.aspx";
                    //string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=10,top=10,resizable=yes');";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "script", s, true);

                    string url = "UsersProperties.aspx";
                    string s = "window.opener.location.href='" + url + "'; window.close();";
                    ScriptManager.RegisterStartupScript(this, GetType(), "script", s, true);


                    //string js = "<script language=javascript> window.opener.location.href='UsersProperties.aspx'; window.close(); </script>";
                    //Response.Write(js);
				}
				catch (Exception exp)
				{
					this.litPopUp.Text = Utilities.MakeLiteralPopUpString(exp.Message);
					this.litPopUp.Visible = true;

				}
			}
			else  // Pager
			{
				DtgAuthenticatedGroup.CurrentPageIndex = int.Parse(e.CommandArgument.ToString())-1;

				DtgAuthenticatedGroup.DataSource = (DataTable) Session["DtAuthenticateGroup"];
				DtgAuthenticatedGroup.DataBind();
			}
		}

		protected void btnClose_Click(object sender, System.EventArgs e)
		{
			Session.Remove("DtAuthenticateGroup");

            //string url = "UserPropertiesGroup.aspx";
            //string s = "window.open('" + url + "', 'popup_window', 'width=800,height=600,left=10,top=10,resizable=yes');";
            //ScriptManager.RegisterStartupScript(this, GetType(), "script", s, true);

            string url = "UserPropertiesGroup.aspx";
            string s = "window.opener.location.href = window.opener.location.href; window.close();";
            ScriptManager.RegisterStartupScript(this, GetType(), "script", s, true);


            //string js = "<script language=javascript> window.opener.location.href = window.opener.location.href; window.close(); </script>";
            //Response.Write(js);
		}
	}
}
