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
using EPolicy.Login;

namespace EPolicy
{
	/// <summary>
	/// Summary description for GroupAndPermissions.
	/// </summary>
	public partial class GroupAndPermissions : System.Web.UI.Page
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

		}
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
                if (!cp.IsInRole("GROUP PERMISSION MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("Default.aspx?001");
				}
			}

			if(!IsPostBack)
			{
                DataTable dtAuthenticatedGroup = LookupTables.LookupTables.GetTable("AuthenticatedGroup");

                //AuthenticatedGroup
                ddlAuthenticatedGroup.DataSource = dtAuthenticatedGroup;
                ddlAuthenticatedGroup.DataTextField = "AuthenticatedGroupDesc";
                ddlAuthenticatedGroup.DataValueField = "AuthenticatedGroupID";
                ddlAuthenticatedGroup.DataBind();
                ddlAuthenticatedGroup.SelectedIndex = 0;

                ddlAuthenticatedGroupSelectItem();


				DisableControls();
			}
		}

		protected void ddlAuthenticatedGroup_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			ddlAuthenticatedGroupSelectItem();
		}

		private void ddlAuthenticatedGroupSelectItem()
		{
			lbxAvailable.Items.Clear();
			lbxSelected.Items.Clear();
			if(this.ddlAuthenticatedGroup.SelectedItem.Value != "")
			{
				FillTextControl();
			}
		}

		protected void cmdSelect_Click(object sender, System.EventArgs e)
		{
			for(int i=0; i < lbxAvailable.Items.Count; i++)
			{
				if(lbxAvailable.Items[i].Selected)
				{
					ListItem list = new ListItem(lbxAvailable.Items[i].Text,lbxAvailable.Items[i].Value);
					lbxSelected.Items.Add(list);
					lbxAvailable.Items.Remove(lbxAvailable.Items[i]);
				}
			}
		}

		protected void cmdRemove_Click(object sender, System.EventArgs e)
		{
			for(int i=0; i < lbxSelected.Items.Count; i++)
			{
				if(lbxSelected.Items[i].Selected)
				{
					ListItem list = new ListItem(lbxSelected.Items[i].Text,lbxSelected.Items[i].Value);
					lbxAvailable.Items.Add(list);
					lbxSelected.Items.Remove(lbxSelected.Items[i]);
				}
			}	
		}

		private void FillTextControl()
		{
			lbxAvailable.Items.Clear();
			lbxSelected.Items.Clear();

			DataTable dtAvailable = Login.Login.GetPermissions(int.Parse(ddlAuthenticatedGroup.SelectedItem.Value));
			for (int i=0; i < dtAvailable.Rows.Count; i++)
			{				 
				lbxAvailable.Items.Add(dtAvailable.Rows[i]["AuthenticatedPermissionDesc"].ToString().Trim());
				lbxAvailable.Items[i].Value = dtAvailable.Rows[i]["AuthenticatedPermissionID"].ToString();
			}

			DataTable dtSelected = Login.Login.GetPermissionByGroupID(int.Parse(ddlAuthenticatedGroup.SelectedItem.Value));
			
			for (int i=0; i < dtSelected.Rows.Count; i++)
			{				 
				lbxSelected.Items.Add(dtSelected.Rows[i]["AuthenticatedPermissionDesc"].ToString().Trim());
				lbxSelected.Items[i].Value = dtSelected.Rows[i]["AuthenticatedPermissionID"].ToString();
			}
		}

		private void EnableControls()
		{
			btnEdit.Visible		= false;
			BtnExit.Visible		= false;
			BtnSave.Visible		= true;
			btnCancel.Visible	= true;

            cmdRemove.Enabled	= true;
			cmdSelect.Enabled	= true;

			ddlAuthenticatedGroup.Enabled = false;
		}

		private void DisableControls()
		{
			btnEdit.Visible		= true;
			BtnExit.Visible		= true;
			BtnSave.Visible		= false;
			btnCancel.Visible	= false;

			cmdRemove.Enabled	= false;
			cmdSelect.Enabled	= false;

			ddlAuthenticatedGroup.Enabled = true;
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			EnableControls();
		}

		protected void BtnSave_Click(object sender, System.EventArgs e)
		{
			Login.Login login = new Login.Login();
			try
			{
				login.SaveGroupAndPermissions(int.Parse(ddlAuthenticatedGroup.SelectedItem.Value),lbxSelected);
				
				DisableControls();

				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Permissions saved successfully for the this Group.");
				this.litPopUp.Visible = true;
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Unable to save ermission. " + exp.Message);
				this.litPopUp.Visible = true;
			}
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			FillTextControl();
			DisableControls();
		}

		protected void BtnExit_Click(object sender, System.EventArgs e)
		{
			this.litPopUp.Visible = false;
			Session.Clear();
			Response.Redirect("LookupTableMaintenance.aspx");
		}
	}
}
