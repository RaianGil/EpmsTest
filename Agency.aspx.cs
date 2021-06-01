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
using EPolicy2.Reports;


namespace EPolicy
{
	/// <summary>
	/// Summary description for Agency.
	/// </summary>
	public partial class Agency : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton BtnExit;
		private Control LeftMenu;


		#region Enumerations

		public enum UserAction{ADD = 1, VIEW = 2, SAVE = 3, EDIT = 4, CANCEL = 5};
		
		#endregion

		private void SetAgencyNumerationLabel()
		{
			LookupTables.Agency agency = 
				(LookupTables.Agency)Session["Agency"];

			this.lblAgencyID.Text = (agency.AgencyID != "") ?
				agency.AgencyID.ToString():
				String.Empty;
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
				Response.Redirect("HomePage.aspx?001");
			}
			else
			{
				if(!cp.IsInRole("AGENCY") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("HomePage.aspx?001");
				}
			}

			if(!Page.IsPostBack)
			{
				LookupTables.Agency agency = new LookupTables.Agency();

				if(Request.QueryString["item"] != null &&																	
					Request.QueryString["item"].ToString() != String.Empty)
				{	
					try
					{
						agency.GetAgency(Request.QueryString["item"].ToString());
						agency.ActionMode = 2; //UPDATE
						agency.NavigationViewModelTable = 
							(DataTable)Session["DtRecordsForNonValuePairLookupTable"];
						Session["Agency"] = agency;
					}
					catch(Exception ex)
					{
						this.ShowMessage("There is no agency for the supplied " +
							"ID. " + ex.Message);
					}
				}
				else
				{
					agency.ActionMode = 1; //ADD
					Session["Agency"] = agency;
				}
				
			}

			if(Session["Agency"] != null)
			{
				LookupTables.Agency agency = 
					(LookupTables.Agency)Session["Agency"];

				this.InitializeControls();
			
				switch(agency.ActionMode)
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
			Banner = LoadControl(@"TopBanner.ascx");
			this.Placeholder1.Controls.Add(Banner);

			//Setup Left-side Banner
			LeftMenu = new Control();
			LeftMenu = LoadControl(@"LeftMenu.ascx");
			//((Baldrich.Components.MenuTaskControl)LeftMenu).Height = "534px";
			phTopBanner1.Controls.Add(LeftMenu);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{   

		}
		#endregion

		
		private void InitializeControls()
		{
			this.SetAgencyNumerationLabel();
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
					this.cmdCancel.Visible = true;
					this.btnAddNew.Visible = false;
					this.btnCommission.Visible = false;
					this.txtEntryDate.Enabled = false;
					this.btnAuditTrail.Visible = false;
					this.btnPrint.Visible = false;
					this.btnSearch.Visible = false;
					this.Button2.Visible = false;
					break;
				case 2: //VIEW ACTION
					this.EnableInputControls(false);
					this.btnEdit.Visible = true;
					this.BtnSave.Visible = false;
					this.cmdCancel.Visible = false;
					this.btnAddNew.Visible = true;
					this.btnCommission.Visible = true;
					this.btnAuditTrail.Visible = true;
					this.btnPrint.Visible = true;
					this.btnSearch.Visible = true;
					this.Button2.Visible = true;
					break;
				case 3: //SAVE ACTION
					this.SetControlState((int)UserAction.VIEW);
					break; 
				case 4: //EDIT ACTION
					this.EnableInputControls(true);
					this.btnEdit.Visible = false;
					this.BtnSave.Visible = true;
					this.cmdCancel.Visible = true;
					this.btnAddNew.Visible = false;
					this.btnCommission.Visible = false;
					this.txtEntryDate.Enabled = false;
					this.btnAuditTrail.Visible = false;
					this.btnPrint.Visible = false;
					this.btnSearch.Visible = false;
					this.Button2.Visible = false;
					break;
				default: //CANCEL ACTION
					LookupTables.Agency agency = 
						(LookupTables.Agency)Session["Agency"];
					if(agency.ActionMode == 0) //ADD
					{
						Session["Agency"] = null;
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
				this.txtAgencyDescription.Enabled = true;
				this.txtAddress1.Enabled = true;
				this.txtAddress2.Enabled = true;
				this.txtCity.Enabled = true;
				this.txtSt.Enabled = true;
				this.txtZipCode.Enabled = true;
				this.txtPhone.Enabled = true;
				this.txtEntryDate.Enabled = true;

			}
			else
			{
				this.txtAgencyDescription.Enabled = false;
				this.txtAddress1.Enabled = false;
				this.txtAddress2.Enabled = false;
				this.txtCity.Enabled = false;
				this.txtSt.Enabled = false;
				this.txtZipCode.Enabled = false;
				this.txtPhone.Enabled = false;
				this.txtEntryDate.Enabled = false;
			}
		}

		private void FillControls()
		{	
			LookupTables.Agency agency =
				(LookupTables.Agency)Session["Agency"];
			
			
			this.txtAgencyDescription.Text = (agency.AgencyDesc != "" ?
				agency.AgencyDesc.ToString():
				String.Empty);
			
			this.txtAddress1.Text = 
				agency.agy_addr1.Trim();

			this.txtAddress2.Text = 
				agency.agy_addr2.Trim();

			this.txtCity.Text =
				agency.agy_city.Trim();

            this.txtSt.Text = 
				agency.agy_st.Trim();

            this.txtZipCode.Text = 
				agency.agy_zip.Trim();

            this.txtPhone.Text = 
				agency.agy_phone.Trim();

            this.txtEntryDate.Text = 
				agency.agy_actdt.Trim();


		}
		protected void BtnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Login.Login cp = HttpContext.Current.User as Login.Login;
			int userID = 0;

			try
			{
				userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
			}
			catch(Exception ex)
			{
				throw new Exception(
					"Could not parse user id from cp.Identity.Name.", ex);
			}

			LookupTables.Agency agency = 
				(LookupTables.Agency)Session["Agency"];
			try
			{
				switch(agency.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref agency);
						agency.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref agency);
						agency.Save(userID);
						Session["Agency"] = agency;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Agency information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetAgencyNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["Agency"] = agency;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Agency. " + xcp.Message);
			}
		}

		private void cmdCancel1_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{		
			LookupTables.Agency agency = (LookupTables.Agency)Session["Agency"];
			
			if(agency.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				this.SetControlState((int)UserAction.CANCEL);
			}
		}

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(
				"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
				LookupTables.LookupTables.GetLookupTableIdFromTableName(
				"Agency").ToString());
		}

		protected void btnAddNew_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Agency.aspx");
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			LookupTables.Agency agency =
				(LookupTables.Agency)Session["Agency"];			
			agency.ActionMode = 2;
			Session["Agency"] = agency;
			this.SetControlState((int)UserAction.EDIT);
		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			EPolicy2.Reports.AdministrativeTools atreport = new EPolicy2.Reports.AdministrativeTools();
			DataTable dt = atreport.AgencyList();

            DataDynamics.ActiveReports.ActiveReport3 rpt = new GeneralList("Agency");
			
			//rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

			rpt.DataSource = dt;
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage",LookupTables.LookupTables.GetTableMaintenancePathFromTableID(25)+ "?item=" + this.lblAgencyID.Text);
			Response.Redirect("ActiveXViewer.aspx",false);		
		}

		protected void btnCommission_Click(object sender, System.EventArgs e)
		{
			LookupTables.Agency agency = (LookupTables.Agency) Session["Agency"];

			Response.Redirect("CommissionAgency.aspx?" +agency.AgencyID);		
		}

		protected void btnAuditTrail_Click(object sender, System.EventArgs e)
		{
			if(Session["Agency"] != null)
			{
				LookupTables.Agency agency = (LookupTables.Agency) Session["Agency"];
				Response.Redirect("SearchAuditItems.aspx?type=3&agencyID=" + 
					agency.AgencyID.Trim());
			}
		}

	
		protected void Button2_Click(object sender, System.EventArgs e)
		{
			LookupTables.Agency agency = (LookupTables.Agency)Session["Agency"];
			
			if(agency.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				Response.Redirect(
					"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
					LookupTables.LookupTables.GetLookupTableIdFromTableName(
					"Agency").ToString());
			}
		}//End BtnSave_Click

		
		
		private void ShowMessage(string MessageText)
		{
			this.litPopUp.Text = 
				Utilities.MakeLiteralPopUpString(MessageText);
			this.litPopUp.Visible = true;
		}

		private void FillProperties(ref LookupTables.Agency Agency)
		{	
			try
			{
				Agency.AgencyDesc = (this.txtAgencyDescription.Text.ToString().ToUpper());
			}
			catch
			{
				this.ShowMessage("Could not fill 'AgencyID' property. " +
					"Please enter a valid value for this field.");
			}
			Agency.agy_addr1 = this.txtAddress1.Text.ToString().ToUpper();
			Agency.agy_addr2 = this.txtAddress2.Text.ToString().ToUpper();
			Agency.agy_city = this.txtCity.Text.ToString().ToUpper();
			Agency.agy_st = this.txtSt.Text.ToString().ToUpper();
			Agency.agy_zip = this.txtZipCode.Text;
			Agency.agy_phone = this.txtPhone.Text;
			Agency.agy_actdt = this.txtEntryDate.Text;

}

	  
			
			private void BtnExit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.Agency agency = 
				(LookupTables.Agency)Session["Agency"];
			
			if(agency.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				Response.Redirect(
					"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
					LookupTables.LookupTables.GetLookupTableIdFromTableName(
					"Agency").ToString());
			}
		}


       protected void cmdCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.SetControlState((int)UserAction.CANCEL);
		}

		protected void btnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.Agency agency =
				(LookupTables.Agency)Session["Agency"];			
			agency.ActionMode = 2;
			Session["Agency"] = agency;
			this.SetControlState((int)UserAction.EDIT);
		}

		private bool IsNavigationTableNull()
		{
			LookupTables.Agency agency = 
				(LookupTables.Agency)Session["agency"];
			if(agency.NavigationViewModelTable == null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		protected void btnAddNew_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("Agency.aspx");
		}

		protected void btnSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(
				"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
				LookupTables.LookupTables.GetLookupTableIdFromTableName(
				"Agency").ToString());
		}

		protected void btnCommission_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.Agency agency = (LookupTables.Agency) Session["Agency"];

			Response.Redirect("CommissionAgency.aspx?" +agency.AgencyID);
		}

		protected void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			EPolicy2.Reports.AdministrativeTools atreport = new EPolicy2.Reports.AdministrativeTools();
			DataTable dt = atreport.AgencyList();

            DataDynamics.ActiveReports.ActiveReport3 rpt = new GeneralList("Agency");
			
			//rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

			rpt.DataSource = dt;
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage",LookupTables.LookupTables.GetTableMaintenancePathFromTableID(25)+ "?item=" + this.lblAgencyID.Text);
			Response.Redirect("ActiveXViewer.aspx",false);

		}

		protected void btnAuditTrail_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if(Session["Agency"] != null)
			{
				LookupTables.Agency agency = (LookupTables.Agency) Session["Agency"];
				Response.Redirect("SearchAuditItems.aspx?type=3&agencyID=" + 
					agency.AgencyID.Trim());
			}
		}

		protected void BtnSave_Click(object sender, System.EventArgs e)
		{
			Login.Login cp = HttpContext.Current.User as Login.Login;
			int userID = 0;

			try
			{
				userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
			}
			catch(Exception ex)
			{
				throw new Exception(
					"Could not parse user id from cp.Identity.Name.", ex);
			}

			LookupTables.Agency agency = 
				(LookupTables.Agency)Session["Agency"];
			try
			{
				switch(agency.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref agency);
						agency.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref agency);
						agency.Save(userID);
						Session["Agency"] = agency;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Agency information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetAgencyNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["Agency"] = agency;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Agency. " + xcp.Message);
			}
		}

						
		}
	}





