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

namespace EPolicy
{
	/// <summary>
	/// Summary description for VehicleModel.
	/// </summary>
	public partial class LookupTableMetadata : System.Web.UI.Page
	{

		#region Enumerations

		public enum UserAction{ADD = 1, VIEW = 2, SAVE = 3, EDIT = 4, CANCEL = 5};
		
		#endregion

		private void SetLookupTableIDLabel()
		{
			LookupTables.LookupTableMetadata lookupTableMetadata = 
				(LookupTables.LookupTableMetadata)Session["LookupTableMetadata"];

			this.lblLookupTableID.Text = (lookupTableMetadata.LookupTableID != 0) ?
				lookupTableMetadata.LookupTableID.ToString(): String.Empty;
		}

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
				if(!cp.IsInRole("LOOKUPTABLEMETADATA") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("Default.aspx?001");
				}


				if(!Page.IsPostBack)
				{
					//this.InitializeDriverObjectSessionVariable();				
				}

				if(Session["LookupTableMetadata"] != null)
				{
					this.InitializePageState();
				}
			}
		}
		private void InitializeDriverObjectSessionVariable()
		{
			LookupTables.LookupTableMetadata lookupTableMetadata
				= new LookupTables.LookupTableMetadata();

			if(Request.QueryString["item"] != null && 
				Request.QueryString["item"].ToString() != String.Empty)
			{	
				lookupTableMetadata.Get(
					int.Parse(Request.QueryString["item"].ToString()));				
			}
			
			lookupTableMetadata.ActionMode = 2; //UPDATE	
			Session["LookupTableMetadata"] = lookupTableMetadata;
		}

		private void InitializePageState()
		{
			LookupTables.LookupTableMetadata lookupTableMetadata = 
				(LookupTables.LookupTableMetadata)Session["LookupTableMetadata"];

			this.InitializeControls();
			
			switch(lookupTableMetadata.ActionMode)
			{
				case 1: //ADD
					this.SetControlState((int)UserAction.ADD);
					if(!Page.IsPostBack)
					{
						this.FillControls();
					}
					break;
					
				default: //UPDATE
					if(!Page.IsPostBack)
					{
						this.FillControls();
						this.SetControlState((int)UserAction.VIEW);
					}
					break;
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

			this.InitializeDriverObjectSessionVariable();			

			this.BindDdls();
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void BindDdls()
		{
			this.BindDdl(this.ddlTableNames, "GetLookupTableNames",
				"LookupTableID", "TableName", 0, false);

			if(Session["LookupTableMetadata"] != null)
			{
				LookupTables.LookupTableMetadata lookupTableMetadata =
					(LookupTables.LookupTableMetadata)Session["LookupTableMetadata"];
				
				try
				{
					this.ddlTablesNotInMetadataStore.DataSource = 
						LookupTables.LookupTables.GetLookupTableNamesNotInMetadataStore();
					this.ddlTablesNotInMetadataStore.DataValueField = string.Empty;
					this.ddlTablesNotInMetadataStore.DataTextField = "name";
					this.ddlTablesNotInMetadataStore.DataBind();
					this.ddlTablesNotInMetadataStore.SelectedIndex = -1;
					this.ddlTablesNotInMetadataStore.Items.Insert(0, string.Empty);
				}
				catch {/*For error subsystem use*/;}
			
				this.SetDdlSelectedIndex(this.ddlTableNames, 
					lookupTableMetadata.LookupTableID.ToString(), true);

				this.ddlTableNames.SelectedIndex = 0;
				
				this.BindDdl(this.ddlIdFieldNames, "GetFieldNames", String.Empty, "Name",  
					lookupTableMetadata.LookupTableID, true);
				
				this.BindDdl(this.ddlDescriptionFieldNames, "GetFieldNames", 
					String.Empty, "Name", lookupTableMetadata.LookupTableID, true);

				this.BindDdl(this.ddlDefaultSearchFieldsA, "GetFieldNames", 
					String.Empty, "Name", lookupTableMetadata.LookupTableID, true);
				
				this.BindDdl(this.ddlDefaultSearchFieldsB, "GetFieldNames", 
					String.Empty, "Name", lookupTableMetadata.LookupTableID, true);
			}
		}

		private void BindDdl(
			System.Web.UI.WebControls.DropDownList DropDownList,
			string StoredProcedureName, string ValueFieldName, 
			string TextFieldName, int LookupTableID, bool IsDynamic)
		{
			DataTable dtResults;

			if(IsDynamic)
			{
				dtResults =
					LookupTables.LookupTables.GetLookupTableColumnsByLookupTableID(
					LookupTableID);
			}
			else
			{
				dtResults =
					LookupTables.LookupTables.GetLookupTableNames();
			}

			if(dtResults != null && dtResults.Rows.Count > 0)
			{
				DropDownList.DataSource = dtResults;
				DropDownList.DataValueField = ValueFieldName;
				DropDownList.DataTextField = TextFieldName;
				DropDownList.DataBind();
				DropDownList.SelectedIndex = -1;
				DropDownList.Items.Insert(0,"");
			}
		}

		private void InitializeControls()
		{
			this.SetLookupTableIDLabel();
			this.litPopUp.Visible = false;
		}

		private void SetControlState(int Action)
		{
			switch(Action)
			{
				case 1: //ADD ACTION
					this.EnableInputControls(true);
					this.btnEdit.Visible = false;
					this.BtnSave.Visible = true;
					this.cmdCancel.Visible = false;
					break;
				case 2: //VIEW ACTION
					this.EnableInputControls(false);
					this.btnEdit.Visible = true;
					this.BtnSave.Visible = false;
					this.cmdCancel.Visible = false;
					break;
				case 3: //SAVE ACTION
					this.SetControlState((int)UserAction.VIEW);
					break; 
				case 4: //EDIT ACTION
					this.EnableInputControls(true);
					this.btnEdit.Visible = false;
					this.BtnSave.Visible = true;
					this.cmdCancel.Visible = true;
					break;
				default: //CANCEL ACTION
					LookupTables.LookupTableMetadata lookupTableMetadata = 
						(LookupTables.LookupTableMetadata)Session["LookupTableMetadata"];
					if(lookupTableMetadata.ActionMode == 1) //ADD
					{
						Session["LookupTableMetadata "] = null;
						Response.Redirect("SearchLookupTableDescriptions.aspx");
					}
					else
					{
						this.SetControlState((int)UserAction.VIEW);
					}
					break;
			}
		}// End SetControlState

		private void EnableInputControls(bool State)
		{
			if(State)
			{
				this.chkIsValuePair.Enabled = true;
				
				if(this.chkIsValuePair.Checked)
				{
					this.ddlIdFieldNames.Enabled = true;
					this.ddlDescriptionFieldNames.Enabled = true;
					this.ddlDefaultSearchFieldsA.Enabled = false;
					this.ddlDefaultSearchFieldsB.Enabled = false;
					this.txtMaintenancePagePath.Enabled = false;
				}
				else
				{
					this.ddlIdFieldNames.Enabled = true;
					this.ddlDefaultSearchFieldsA.Enabled = true;
					this.ddlDefaultSearchFieldsB.Enabled = true;
                    this.txtMaintenancePagePath.Enabled = true;
					this.ddlDescriptionFieldNames.Enabled = false;
				}
			}
			else
			{
				this.chkIsValuePair.Enabled = false;

				if(!this.chkIsValuePair.Checked)
				{
					this.ddlIdFieldNames.Enabled = false;
					this.ddlDescriptionFieldNames.Enabled = false;
					this.ddlDefaultSearchFieldsA.Enabled = false;
					this.ddlDefaultSearchFieldsB.Enabled = false;
					this.txtMaintenancePagePath.Enabled = false;
				}
				else
				{
					this.ddlIdFieldNames.Enabled = false;
					this.ddlDescriptionFieldNames.Enabled = false;
					this.ddlDefaultSearchFieldsA.Enabled = false;
					this.ddlDefaultSearchFieldsB.Enabled = false;
					this.txtMaintenancePagePath.Enabled = false;
				}
			}
		}

		private void FillControls()
		{	
			if(Request.QueryString["item"] != null && 
				Request.QueryString["item"].ToString() != String.Empty)
			{
                this.SetDdlSelectedIndex(this.ddlTableNames,
					Request.QueryString["item"].ToString(), true);
			}

			if(Session["LookupTableMetadata"] != null)
			{
				LookupTables.LookupTableMetadata lookupTableMetadata =
					(LookupTables.LookupTableMetadata)Session["LookupTableMetadata"];

				this.SetDdlSelectedIndex(this.ddlIdFieldNames, 
					lookupTableMetadata.IdFieldName, false);
				this.SetDdlSelectedIndex(this.ddlDescriptionFieldNames,
					lookupTableMetadata.DescriptionFieldName, false);
				this.SetDdlSelectedIndex(this.ddlDefaultSearchFieldsA,
					lookupTableMetadata.DefaultSearchFieldA, false);
				this.SetDdlSelectedIndex(this.ddlDefaultSearchFieldsB,
					lookupTableMetadata.DefaultSearchFieldB, false);
				this.txtMaintenancePagePath.Text = 
					lookupTableMetadata.MaintenancePagePath;

				if(lookupTableMetadata.IsValuePair)
				{
					this.chkIsValuePair.Checked = true;
				}
				else
				{
					this.chkIsValuePair.Checked = false;
				}   
			}
		}// End FillControls()
        
		private void SetDdlSelectedIndex(
			System.Web.UI.WebControls.DropDownList DropDownList, 
			string Criterion, bool IsValue)
		{
			DropDownList.SelectedIndex = 0;

			if(!IsValue || (IsValue && Criterion.Trim() != "0"))
			{
				for(int i = 0; DropDownList.Items.Count - 1 >= i; i++)
				{
					if(IsValue)
					{
						if (DropDownList.Items[i].Value == Criterion.Trim())
						{
							DropDownList.SelectedIndex = i;
							i = DropDownList.Items.Count-1;
						}
					}
					else
					{
						if (DropDownList.Items[i].Text.Trim() == Criterion.Trim())
						{
							DropDownList.SelectedIndex = i;
							i = DropDownList.Items.Count-1;
						}
					}
				}
			}
		}

		private void ShowMessage(string MessageText)
		{
			this.litPopUp.Text = 
				Utilities.MakeLiteralPopUpString(MessageText);
			this.litPopUp.Visible = true;
		}

		private void FillProperties(
			ref LookupTables.LookupTableMetadata LookupTableMetadata)
		{	
			if(this.chkIsValuePair.Checked)
			{
				LookupTableMetadata.IsValuePair = true;
				LookupTableMetadata.IdFieldName = 
					this.ddlIdFieldNames.SelectedItem.Text;
				LookupTableMetadata.DescriptionFieldName =
					this.ddlDescriptionFieldNames.SelectedItem.Text;
				
				LookupTableMetadata.DefaultSearchFieldA = 
					this.ddlDefaultSearchFieldsA.SelectedItem.Text;
				LookupTableMetadata.DefaultSearchFieldB = 
					this.ddlDefaultSearchFieldsB.SelectedItem.Text;
				LookupTableMetadata.MaintenancePagePath =
					this.txtMaintenancePagePath.Text;
			}
			else
			{
				LookupTableMetadata.IsValuePair = false;

				LookupTableMetadata.IdFieldName = 
					this.ddlIdFieldNames.SelectedItem.Text;

				LookupTableMetadata.DescriptionFieldName = String.Empty;

				LookupTableMetadata.DefaultSearchFieldA = 
					this.ddlDefaultSearchFieldsA.SelectedItem.Text;
				LookupTableMetadata.DefaultSearchFieldB = 
					this.ddlDefaultSearchFieldsB.SelectedItem.Text;
				LookupTableMetadata.MaintenancePagePath =
					this.txtMaintenancePagePath.Text;
			}
		}

		private bool IsNavigationTableNull()
		{
			LookupTables.VehicleModel vehicleModel = 
				(LookupTables.VehicleModel)Session["VehicleModel"];
			if(vehicleModel.NavigationViewModelTable == null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		protected void ddlTableNames_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Response.Redirect("LookupTableMetadata.aspx?item=" + 
				this.ddlTableNames.SelectedItem.Value.ToString());
		}

		protected void chkIsValuePair_CheckedChanged(object sender, System.EventArgs e)
		{
			this.EnableInputControls(true);
		}

		protected void btnAddTableToMetadataStore_Click(object sender, System.EventArgs e)
		{
			int addedTableID = 0;
			try
			{
				addedTableID = LookupTables.LookupTableMetadata.AddTableNameToMetadataStore(
					this.ddlTablesNotInMetadataStore.SelectedItem.Text.Trim());
				this.ShowMessage(
					"Table '" + this.ddlTablesNotInMetadataStore.SelectedItem.Text + 
					"' added sucessfully to metadata store.  " + 
					"Press 'Edit' and set the 'ID Field Name'");
			}
			catch(Exception ex)
			{
				this.ShowMessage(
					"Could not add table '" + 
					this.ddlTablesNotInMetadataStore.SelectedItem.Text + "' to metadata store. " +
					ex.Message);
			}

			Response.Redirect("LookupTableMetadata.aspx?item=" + addedTableID.ToString());
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			LookupTables.LookupTableMetadata lookupTableMetadata =
				(LookupTables.LookupTableMetadata)Session["LookupTableMetadata"];
			lookupTableMetadata.ActionMode = 2;
			Session["LookupTableMetadata"] = lookupTableMetadata;
			this.SetControlState((int)UserAction.EDIT);
		}

		protected void BtnSave_Click(object sender, System.EventArgs e)
		{
			LookupTables.LookupTableMetadata lookupTableMetadata = 
				(LookupTables.LookupTableMetadata)Session["LookupTableMetadata"];
			try
			{
				switch(lookupTableMetadata.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref lookupTableMetadata);
						lookupTableMetadata.Save();
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref lookupTableMetadata);
						lookupTableMetadata.Save();
						Session["LookupTableMetadata"] = lookupTableMetadata;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"Lookup table metadata for table " + 
					lookupTableMetadata.TableName.Trim() +
					" saved successfully.");
				this.litPopUp.Visible = true;
				this.SetLookupTableIDLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["LookupTableMetadata"] = lookupTableMetadata;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify lookup table metadata for " +
					lookupTableMetadata.TableName + ". " + xcp.Message);
			}
		}

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(
				"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
				LookupTables.LookupTables.GetLookupTableIdFromTableName(
				"LookupTableMetadata").ToString());
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.FillControls();
			this.SetControlState((int)UserAction.CANCEL);
		}

		protected void cmdExit_Click(object sender, System.EventArgs e)
		{
			LookupTables.LookupTableMetadata lookupTableMetadata = 
				(LookupTables.LookupTableMetadata)Session["LookupTableMetadata"];
			
			if(lookupTableMetadata.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				Response.Redirect(
					"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
					LookupTables.LookupTables.GetLookupTableIdFromTableName(
					"LookupTableMetadata").ToString());
			}
		}
	}
}
