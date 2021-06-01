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
using System.Configuration;

namespace EPolicy
{
	/// <summary>
	/// Summary description for LookupTableMaintenance.
	/// </summary>
	public partial class LookupTableMaintenance : System.Web.UI.Page
	{

		#region Enumerations
		
		public enum Pages{DESCRIPTIONSEARCH = 0, VALUEPAIRS = 1, OTHER = 2};

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
                if (!cp.IsInRole("LOOKUPTABLE MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("Default.aspx?001");
				}
			}

			// Put user code to initialize the page here
			if(!Page.IsPostBack)
			{
				this.InitializeControls();
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

			Control Banner = new Control();
			Banner = LoadControl(@"TopBannerNew.ascx");
			this.phTopBanner.Controls.Add(Banner);

			
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.grdDisplayTables.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdDisplayTables_ItemCommand);

		}
		#endregion

		#region Private Functions

		private void ShowMessage(string Message)
		{
			this.litPopUp.Text = 
				Utilities.MakeLiteralPopUpString(Message);
			this.litPopUp.Visible = true;
		}

		private void InitializeControls()
		{
			this.RefreshTablesGrid();
			
			DataTable dtLookupTables = (DataTable)Session["DtLookupTables"];

			this.lblTotalTables.Text = dtLookupTables.Rows.Count.ToString();
		}

		private void RefreshTablesGrid()
		{
			DataTable dtLookupTables = LookupTables.LookupTables.GetTable(ConfigurationManager.AppSettings["LookupTableMetadataTableName"].ToString());

			Session["DtLookupTables"] = dtLookupTables;
			
			this.grdDisplayTables.DataSource = dtLookupTables;			
			this.grdDisplayTables.DataBind();

			this.grdDisplayTables.CurrentPageIndex = 0;

			this.grdDisplayTables.Visible = true;

            //Se añadio para que no explote al añadir un Agente VI
            this.grdDisplayTables.Columns[2].Visible = false;
		}
		
		private void grdDisplayTables_ItemCommand(object source, 
			System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(e.CommandName)
			{
				case "Add":
					this.AddDescriptionHandler(e);
					break;
				case "Edit":
					this.EditTableHandler(e);
					break;
				default: //Page
					this.PageHandler(e);
					break;
			}
		}

		private void AddDescriptionHandler(//TODO: Refactor (').
			System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.Item.Cells[3].Text == "True")
			{
				this.RedirectToPage(
					(int)Pages.VALUEPAIRS, "?SelectedItem=" + 
					e.Item.Cells[4].Text);
			}
			else
			{
				try
				{
					this.RedirectToPage((int)Pages.OTHER, 
						int.Parse(e.Item.Cells[4].Text));
				}
				catch {this.ShowMessage("There was a problem redirecting you to the " + 
						   "appropriate description page.  Please contact the system " + 
						   "administrator.");}
			}
		}

		private void EditTableHandler(//TODO: Refactor (').
			System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.Item.Cells[3].Text == "True")
			{
                this.AddDescriptionHandler(e);
			}
			else
			{
				this.RedirectToPage(
					(int)Pages.DESCRIPTIONSEARCH, "?SelectedItem=" +
					e.Item.Cells[4].Text);
			}
		}

		private void PageHandler(
			System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.grdDisplayTables.CurrentPageIndex = 
				int.Parse(e.CommandArgument.ToString())-1;
			this.grdDisplayTables.DataSource = 
				(DataTable)Session["DtLookupTables"];
			this.grdDisplayTables.DataBind();
		}

		private void RedirectToPage(int Page, string Querystring, int TableID)
		{
			switch(Page)
			{
				case (int)Pages.VALUEPAIRS:
                    Response.Redirect(
						ConfigurationSettings.AppSettings[
						"ValuePairLookupTableMaintenancePath"].ToString()
						+ Querystring);
					break;
				case (int)Pages.DESCRIPTIONSEARCH:
					Response.Redirect(ConfigurationSettings.AppSettings[
					"SearchLookupTablesPath"].ToString()
						+ Querystring);
					break;
				default: //Maintenance paths.
					if(TableID != 0)
					{
						string maintenancePath = "";

						try
						{
							maintenancePath = 
								LookupTables.LookupTables.GetTableMaintenancePathFromTableID(
								TableID);
						}
						catch { this.ShowMessage("An error ocurred while retrieving " +
								"this table's  maintenance path. Please contact " +
								"the system administrator.");}
						
						if(maintenancePath != "")
						{
							Response.Redirect(maintenancePath);
						}
						else
						{
							this.ShowMessage("A maintenance page for this table has " +
								"not been registered in the system.  Please contact " +
								"the system administrator.");
						}
					}
					break;
			}
		}

		private void Button3_Click(object sender, System.EventArgs e)
		{
			Session.Clear();
			Response.Redirect("GroupAndPermissions.aspx");
		}

		private void BtnAddUser_Click(object sender, System.EventArgs e)
		{
			Session.Clear();
			Login.Login login = new Login.Login();
			login.Mode = (int) Login.Login.LoginMode.ADD;
			Session.Add("Login",login);
			Response.Redirect("UsersProperties.aspx");
		}

		private void BtnUserProperties_Click(object sender, System.EventArgs e)
		{
			Session.Clear();
			Response.Redirect("UserPropertiesList.aspx");
		}

		protected void btnLookupTableMetadata_Click(object sender, System.EventArgs e)
		{
			DataTable tableNames = LookupTables.LookupTables.GetLookupTableNames();
			Response.Redirect("LookupTableMetadata.aspx?item=" + 
				tableNames.Rows[0]["LookupTableID"].ToString());
		}

		private void ImageButton1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			DataTable tableNames = LookupTables.LookupTables.GetLookupTableNames();
			Response.Redirect("LookupTableMetadata.aspx?item=" + 
				tableNames.Rows[0]["LookupTableID"].ToString());
		}

		private void RedirectToPage(int Page, int TableID)
		{
			this.RedirectToPage(Page, "", TableID);
		}

		private void RedirectToPage(int Page, string Querystring)
		{
			this.RedirectToPage(Page, Querystring, 0);
		}
		#endregion
	}
}
