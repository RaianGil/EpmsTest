using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace EPolicy
{
	/// <summary>
	/// Summary description for GeneralLookupTableMaintenance.
	/// </summary>
	public partial class GeneralLookupTableMaintenance : System.Web.UI.Page
	{
		private int _controlState = 2; //Default: VIEW

		#region Page Properties
		public int ControlState
		{
			get
			{
				return this._controlState;
			}
			set
			{
				this._controlState = value;
			}
		}
		#endregion

		#region Enumerations

		public enum UserAction{ADD = 1, VIEW = 2, SAVE = 3, EDIT = 4, CANCEL = 5, DELETE = 6};
		
		#endregion


		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.litPopUp.Visible = false;

            Control Banner = new Control();
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.phTopBanner.Controls.Add(Banner);

			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
				Response.Redirect("Default.aspx?001");
			}
			else
			{
				if(!cp.IsInRole("VALUEPAIRLOOKUPTABLEMAINTENANCE") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("Default.aspx?001");
				}
			}

			if(!Page.IsPostBack && Request.QueryString["SelectedItem"] != "")
			{
				try
				{
					this.SetTableDropDownListSelectedItem(
						int.Parse(Request.QueryString["SelectedItem"]));
					this.RefreshDescriptionsGrid(
						int.Parse(this.ddlLookupTables.SelectedItem.Value), true);
					this.SetControlState((int)UserAction.ADD);
					
				}
				catch { /*For error login system use.*/ }
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			this.InitializeControls();

			base.OnInit(e);
			
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.grdDisplayDescriptions.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.grdDisplayDescriptions_ItemCreated);
			this.grdDisplayDescriptions.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdDisplayDescriptions_ItemCommand);

		}

		private void grdDisplayDescriptions_ItemCreated (
			object source, DataGridItemEventArgs e)
		{	
			if(e.Item.ItemType == ListItemType.Item || 
				e.Item.ItemType == ListItemType.AlternatingItem ||
				e.Item.ItemType  == ListItemType.EditItem)
			{
				TableCell tableCell = new TableCell();
				tableCell =	e.Item.Cells[3];
				Button button = new Button();
				button = (Button)tableCell.Controls[0];
				button.Attributes.Add("onclick", 
					"return confirm('Are you sure you want to delete this description?')");
			}
		}

		private void InitializeControls()
		{
			//Setup ddlLookupTables
			DataTable dtLookupTables =
				LookupTables.LookupTables.GetValuePairLookupTableNames();

			this.ddlLookupTables.DataSource = dtLookupTables;
			this.ddlLookupTables.DataTextField = "TableName";
			this.ddlLookupTables.DataValueField = "LookupTableID";
			this.ddlLookupTables.DataBind();
			this.ddlLookupTables.SelectedIndex = -1;
			this.ddlLookupTables.Items.Insert(0,"");

			this.SetControlState((int)UserAction.VIEW);

			this.litPopUp.Visible = false;

		}
		#endregion	

		private void SetTableDropDownListSelectedItem(int SelectedItemID)
		{
			for(int i = 0; this.ddlLookupTables.Items.Count - 1 >= i; i++)
			{
				try
				{
					if (int.Parse(this.ddlLookupTables.Items[i].Value) == 
						SelectedItemID)
					{
						this.ddlLookupTables.SelectedIndex = i;
						i = this.ddlLookupTables.Items.Count - 1;
					}
				}
				catch {/*For error login system use.*/}
			}
		}

		protected void ddlLookupTables_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.ddlLookupTables.SelectedItem.Value == "")
			{
				this.grdDisplayDescriptions.Visible = false;
				this.SetControlState((int)UserAction.VIEW);
			}
			else
			{
				this.RefreshDescriptionsGrid(
					int.Parse(this.ddlLookupTables.SelectedItem.Value), true);
				this.SetControlState((int)UserAction.ADD);
	
				
			}
		}

		private void RefreshDescriptionsGrid(int LookupTableID, bool IndexChanged)
		{
			DataTable dtDescriptions = LookupTables.LookupTables.GetLookupTableFromSP(
				LookupTableID, "GetLookupTableFromSP");

			int currentPageIndex = this.grdDisplayDescriptions.CurrentPageIndex;
			this.grdDisplayDescriptions.CurrentPageIndex = 0;
			
			this.grdDisplayDescriptions.DataSource = dtDescriptions;
			Session["Descriptions"] = dtDescriptions;
			
			if(IndexChanged)
			{
				this.grdDisplayDescriptions.CurrentPageIndex = 0;
			}
			else
			{
				this.grdDisplayDescriptions.DataBind();

				if(this.ControlState == (int)UserAction.ADD || 
					this.ControlState == (int)UserAction.DELETE)
				{
					this.grdDisplayDescriptions.CurrentPageIndex = 
						(this.grdDisplayDescriptions.PageCount - 1);
				}
				else
				{
					this.grdDisplayDescriptions.CurrentPageIndex = currentPageIndex;
				}
			}
			this.grdDisplayDescriptions.SelectedIndex = -1;			
			this.grdDisplayDescriptions.DataBind();
			this.grdDisplayDescriptions.Visible = true;
		}
		
		private void grdDisplayDescriptions_ItemCommand(object source, 
			System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch(e.CommandName)
			{
				case "Select":
					this.SelectHandler(e);
					break;
				case "Delete":
					this.DeleteDescription(e);
					break;
				default: //Page
					this.PageHandler(e);
					break;
			}
		}

		private void SelectHandler(System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
          
			DataTable dtDescriptions = (DataTable)Session["DtDescriptions"];
			

			this.SetControlState((int)UserAction.EDIT);
			this.txtInput.Text = e.Item.Cells[2].Text.Trim();
			this.lblDescriptionID.Text = e.Item.Cells[1].Text.ToString().Trim();
				
			
		}
	
		private void PageHandler(System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if((int.Parse(e.CommandArgument.ToString()) - 1) >= 0 &&
				(int.Parse(e.CommandArgument.ToString()) - 1) <=	
				this.grdDisplayDescriptions.PageCount)
			{
				this.grdDisplayDescriptions.CurrentPageIndex = 
					int.Parse(e.CommandArgument.ToString())-1;
				this.grdDisplayDescriptions.DataSource = 
					(DataTable) Session["Descriptions"];
				this.grdDisplayDescriptions.DataBind();
				
				this.grdDisplayDescriptions.SelectedIndex = -1;
					
				this.SetControlState((int)UserAction.ADD);
			}
		}

		private void AddDescription()
		{
			if(this.txtInput.Text.ToUpper() != "")
			{
				try
				{

					LookupTables.LookupTables.Add(
						int.Parse(this.ddlLookupTables.SelectedItem.Value),
						this.txtInput.Text.Trim().ToUpper());

					this.ShowMessage(
						"Description '" + this.txtInput.Text.Trim().ToUpper() + 
						"' was successfully added to lookup table '" +
						this.ddlLookupTables.SelectedItem.Text + "'.");
				}
				catch(Exception exp)
				{
					this.ShowMessage("An error ocurred while adding " + 
						" the lookup table description. "+exp.Message);
				}
				this.SetControlState((int)UserAction.ADD);
				this.RefreshDescriptionsGrid(
					int.Parse(this.ddlLookupTables.SelectedItem.Value),	false);
			}
		}

		private void SaveDescription()
		{
			try
			{
				LookupTables.LookupTables.Update(
					int.Parse(this.ddlLookupTables.SelectedItem.Value),
					int.Parse(this.grdDisplayDescriptions.SelectedItem.Cells[1].Text), 
					this.txtInput.Text.Trim());

				this.ShowMessage(
					"Description was successfully updated in lookup table '" +
					this.ddlLookupTables.SelectedItem.Text + "'.");
				this.SetControlState((int)UserAction.SAVE);
				this.RefreshDescriptionsGrid(
					int.Parse(this.ddlLookupTables.SelectedItem.Value),false);
				
			}
			catch (Exception exp)
			{
				this.ShowMessage("An error ocurred while updating " + 
					" the lookup table description.  The description will " +
					"retain its original value. " +exp.Message);
			}

		}

		private void DeleteDescription(
			System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				LookupTables.LookupTables.Delete(
					int.Parse(this.ddlLookupTables.SelectedItem.Value), 
					int.Parse(e.Item.Cells[1].Text.Trim()));

				this.ShowMessage("Description '" + e.Item.Cells[1].Text.Trim() +
					"' was sucessfully deleted from lookup table '" + 
					this.ddlLookupTables.SelectedItem.Text.Trim() + "'.");
				
				this.ClearInputControls();
				this.SetControlState((int)UserAction.DELETE);
				this.RefreshDescriptionsGrid(
					int.Parse(this.ddlLookupTables.SelectedItem.Value.Trim()), false);
			}
			catch(Exception ex)
			{
				this.HandleDeleteError(ex);
			}
		}

		private void HandleDeleteError(Exception Ex)
		{
            switch(Ex.GetBaseException().GetType().FullName)
			{
				case "System.Data.SqlClient.SqlException":
					SqlException sqlException = (SqlException)Ex.GetBaseException();
				switch(sqlException.Number)
				{
					case 547:
						this.ShowMessage("The description you are attempting to " +
							"delete is being referenced by one or more database " +
							"entities. Please delete any existing references to " +
							"this description before attempting to delete it.");
						break;
					default:
						this.ShowMessage("An database server error ocurred while " +
							"deleting the lookup table description.");
						break;
				}
					break;
				default:
					this.ShowMessage("An error ocurred while deleting " + 
					" the lookup table description.");
					break;
			}
		}

		private void SetControlState(int Action) //TODO:  Refactor this function.
		{
			switch(Action)
			{
				case 1: //ADD ACTION
					this.EnableInputControls(true);
					this.cmdSave.Visible = false;
					this.lblInput.Text = "Add:";
					this.ClearInputControls();					
					break;
				case 2: //VIEW ACTION
					this.EnableInputControls(false);
					break;
				case 3: //SAVE ACTION
					this.SetControlState((int)UserAction.ADD); //For clarity.
					break; 
				case 4: //EDIT ACTION
					this.lblInput.Text = "Edit:";
					this.cmdAdd.Visible = false;
					this.cmdSave.Visible = true;
					break;
				case 5: //CANCEL ACTION					
					this.lblInput.Text = "Add:";
					this.cmdSave.Visible = false;
					this.cmdAdd.Visible = true;
					this.grdDisplayDescriptions.SelectedIndex = -1;
					this.ClearInputControls();
					break;
				default: //DELETE ACTION
					this.SetControlState((int)UserAction.ADD); //For clarity.
					break;
			}
			this.ControlState = Action; //Set page property.
		}

		private void ClearInputControls()
		{
			this.txtInput.Text = "";
			this.lblDescriptionID.Text = "";
		}

		private void EnableInputControls(bool State)
		{
			if(State == false)
			{
				this.lblInput.Visible = false;
				this.txtInput.Visible = false;
				this.cmdAdd.Visible = false;
				this.cmdCancel.Visible = false;
			}
			else
			{
				this.lblInput.Visible = true;
				this.txtInput.Visible = true;
				this.cmdAdd.Visible = true;
				this.cmdCancel.Visible = true;
			}
		}

		private void ShowMessage(string Message)
		{
			this.litPopUp.Text = 
				Utilities.MakeLiteralPopUpString(Message);
			this.litPopUp.Visible = true;
		}

		protected void cmdExit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("LookupTableMaintenance.aspx");
		}

		protected void cmdAdd_Click(object sender, System.EventArgs e)
		{
			this.AddDescription();
		}

		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
			this.SaveDescription();
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.SetControlState((int)UserAction.CANCEL);
		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			
		}

		protected void cmdExit_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("LookupTableMaintenance.aspx");
		}
	}
}