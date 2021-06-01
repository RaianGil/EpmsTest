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
using EPolicy.LookupTables;

namespace EPolicy
{
	/// <summary>
	/// Summary description for CompanyDealer.
	/// </summary>
	public partial class CompanyDealer : System.Web.UI.Page
	{
		private Control LeftMenu;


		#region Enumerations

		public enum UserAction{ADD = 1, VIEW = 2, SAVE = 3, EDIT = 4, CANCEL = 5};
		
		#endregion



		private void SetCompanyDealerNumerationLabel()
		{
			LookupTables.CompanyDealer companyDealer = 
				(LookupTables.CompanyDealer)Session["CompanyDealer"];

			this.lblCompanyDealerID.Text = (companyDealer.CompanyDealerID != "") ?
				companyDealer.CompanyDealerID.ToString():
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
				if(!cp.IsInRole("COMPANYDEALER") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("HomePage.aspx?001");
				}
			}

			if(!Page.IsPostBack)
			{
				LookupTables.CompanyDealer companyDealer = new LookupTables.CompanyDealer();

				if(Request.QueryString["item"] != null &&																	
					Request.QueryString["item"].ToString() != String.Empty)
				{	
					try
					{
						companyDealer.GetCompanyDealer(Request.QueryString["item"].ToString());
						companyDealer.ActionMode = 2; //UPDATE
						companyDealer.NavigationViewModelTable = 
							(DataTable)Session["DtRecordsForNonValuePairLookupTable"];
						Session["CompanyDealer"] = companyDealer;
					}
					catch(Exception ex)
					{
						this.ShowMessage("There is no Company Dealer for the supplied " +
							"ID. " + ex.Message);
					}
				}
				else
				{
					companyDealer.ActionMode = 1; //ADD
					Session["CompanyDealer"] = companyDealer;
				}
				
			}
			
			if(Session["CompanyDealer"] != null)
			{
				LookupTables.CompanyDealer companyDealer = 
					(LookupTables.CompanyDealer)Session["CompanyDealer"];

				this.InitializeControls();
			
				switch(companyDealer.ActionMode)
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

							if (companyDealer.NavigationViewModelTable != null)
							{
								this.RecordNavigation("");
							}
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



            //Control Banner = new Control();
            //Banner = LoadControl(@"TopBanner.ascx");
            //this.phTopBanner.Controls.Add(Banner);

			//Setup Left-side Banner
//			
//			LeftMenu = new Control();
//			LeftMenu = LoadControl(@"Components\MenuAdministration.ascx");
//			//((Baldrich.Components.MenuTaskControl)LeftMenu).Height = "534px";
//			phTopBanner.Controls.Add(LeftMenu);

		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnAddNew1.Click += new System.Web.UI.ImageClickEventHandler(this.btnAddNew_Click);
			this.btnEdit1.Click += new System.Web.UI.ImageClickEventHandler(this.btnEdit_Click);
			this.BtnSave1.Click += new System.Web.UI.ImageClickEventHandler(this.BtnSave_Click);
			this.btnSearch1.Click += new System.Web.UI.ImageClickEventHandler(this.btnSearch_Click);
			this.cmdCancel1.Click += new System.Web.UI.ImageClickEventHandler(this.cmdCancel_Click);
			this.btnPrint1.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrint_Click);
			this.btnAuditTrail1.Click += new System.Web.UI.ImageClickEventHandler(this.btnAuditTrail_Click);
			this.BtnExit1.Click += new System.Web.UI.ImageClickEventHandler(this.BtnExit_Click);

		}
		#endregion




		private void InitializeControls()
		{
			this.SetCompanyDealerNumerationLabel();
			this.litPopUp.Visible = false;
		}

		private void SetControlState(int Action)
		{
			switch(Action)
			{
				case 1: //ADD ACTION
					this.EnableRecordNavigationControl(false);
					this.EnableInputControls(true);
					this.btnEdit.Visible = false;
					this.BtnSave.Visible = true;
					this.cmdCancel.Visible = false;
					this.btnAddNew.Visible = false;
					this.txtEntryDate.Enabled = false;
					break;
				case 2: //VIEW ACTION
					this.EnableRecordNavigationControl(true);
					this.EnableInputControls(false);
					this.btnEdit.Visible = true;
					this.BtnSave.Visible = false;
					this.cmdCancel.Visible = false;
					this.btnAddNew.Visible = true;
					break;
				case 3: //SAVE ACTION
					this.SetControlState((int)UserAction.VIEW);
					break; 
				case 4: //EDIT ACTION
					this.EnableRecordNavigationControl(false);
					this.EnableInputControls(true);
					this.btnEdit.Visible = false;
					this.BtnSave.Visible = true;
					this.cmdCancel.Visible = true;
					this.btnAddNew.Visible = false;
					this.txtEntryDate.Enabled = false;
					break;
				default: //CANCEL ACTION
					LookupTables.CompanyDealer companyDealer = 
						(LookupTables.CompanyDealer)Session["CompanyDealer"];
					if(companyDealer.ActionMode == 0) //ADD
					{
						Session["CompanyDealer"] = null;
						Response.Redirect("SearchLookupTableDescriptions.aspx");
					}
					else
					{
						this.SetControlState((int)UserAction.VIEW);
					}
					break;
			}
		}// End SetControlState



		private void EnableRecordNavigationControl(bool State)
		{
			if(State)
			{
				this.BtnBegin.Enabled = true;
				this.BtnEnd.Enabled = true;
				this.BtnNext.Enabled = true;
				this.BtnPrevious.Enabled = true;
			}
			else
			{
				this.BtnBegin.Enabled = false;
				this.BtnEnd.Enabled = false;
				this.BtnNext.Enabled = false;
				this.BtnPrevious.Enabled = false;
			}
		}

		private void RecordNavigation(string Case)
		{
			if(Session["CompanyDealer"] != null)
			{
				LookupTables.CompanyDealer companyDealer = 
					(LookupTables.CompanyDealer)Session["CompanyDealer"];
				string companyDealerID = companyDealer.CompanyDealerID;

				if(companyDealer.NavigationViewModelTable != null)
				{
					int rec = 0;

					for(int i=0; 
						i<= (companyDealer.NavigationViewModelTable.Rows.Count - 1); i++)
					{
						if (companyDealerID == 
							companyDealer.NavigationViewModelTable.Rows[i]["CompanyDealerID"].ToString())
						{
							if (Case == "")
							{
								rec = i + 1;
								lblRecordCount.Text = 
									rec.ToString() + " of " +
									companyDealer.NavigationViewModelTable.Rows.Count;
							}
							else if (Case == "Previous" || Case == "Begin")
							{
								rec = 1;
								if (i > 0)
								{
									switch(Case)
									{
										case "Previous":
											companyDealerID = 
												companyDealer.NavigationViewModelTable.Rows[i - 1]["CompanyDealerID"].ToString();
											rec = i;
											break;
										case "Begin":
											companyDealerID = 
												companyDealer.NavigationViewModelTable.Rows[0]["CompanyDealerID"].ToString();
											rec = 1;
											break;
									}
									lblRecordCount.Text = 
										rec.ToString() + " of " + 
										companyDealer.NavigationViewModelTable.Rows.Count;
									i = 
										companyDealer.NavigationViewModelTable.Rows.Count-1; 
									// Para salir del loop.
								}
							}
							else if (Case == "Next" || Case == "End")
							{
								if (companyDealer.NavigationViewModelTable.Rows.Count-1 > i)
								{					
									switch(Case)
									{
										case "Next":
											companyDealerID = 
												
												companyDealer.NavigationViewModelTable.Rows[i + 1]["CompanyDealerID"].ToString();
											rec = i + 2;
											break;
										case "End":
											companyDealerID =
												companyDealer.NavigationViewModelTable.Rows[companyDealer.NavigationViewModelTable.Rows.Count - 1]["CompanyDealerID"].ToString();
											rec = companyDealer.NavigationViewModelTable.Rows.Count;
											break;
									}
									lblRecordCount.Text = 
										rec.ToString() + " of " + 
										companyDealer.NavigationViewModelTable.Rows.Count;
									i = companyDealer.NavigationViewModelTable.Rows.Count-1; 
									// Para salir del loop.
								}
							}
						}
					}																
				}
				DataTable dtCompanyDealer = companyDealer.NavigationViewModelTable;
				
				try
				{
					companyDealer.GetCompanyDealer(companyDealerID);
			
					companyDealer.NavigationViewModelTable = dtCompanyDealer;

					Session.Add("CompanyDealer", companyDealer);
					this.FillControls();
					this.SetControlState((int)UserAction.VIEW);
					this.SetCompanyDealerNumerationLabel();
				}
				catch(Exception ex)
				{
					this.ShowMessage("There is no Company Dealer for the supplied " +
						"ID. " + ex.Message);
				}
			}
		}// End RecordNavigation



		private void EnableInputControls(bool State)
		{
			if(State)
			{
				this.txtCompanyDealerDescription.Enabled = true;
                this.txtDealerCode.Enabled = true;
				this.txtCode.Enabled = true;
				this.txtEntryDate.Enabled = true;
				this.txtCoresu.Enabled = true;
				this.txtName.Enabled = true;
				this.chkBaldrich.Enabled = true;
				this.chkEas.Enabled = true;
				this.chkTriangle.Enabled = true;
				this.txtAuto.Enabled = true;
				this.txtmbi.Enabled = true;
				this.txtAddress1.Enabled = true;
				this.txtAddress2.Enabled = true;
				this.txtCity.Enabled = true;
				this.txtSt.Enabled = true;
				this.txtZipCode.Enabled = true;
				this.ChkRfloor.Enabled = true;
				this.ChkCars.Enabled  = true;
                this.TXTVSCClientID.Enabled = true;

                this.TXTVSCClientID.Enabled = true;
                this.TxtProfit.Enabled = true;
                this.TxtConcurso.Enabled = true;
                this.TxtMasterGap.Enabled = true;
                this.TxtProfitDealer.Enabled = true;

			}
			else
			{
				this.txtCompanyDealerDescription.Enabled = false;
                this.txtDealerCode.Enabled = false;
				this.txtCode.Enabled = false;
				this.txtEntryDate.Enabled = false;
				this.txtCoresu.Enabled = false;
				this.txtName.Enabled = false;
				this.chkBaldrich.Enabled = false;
				this.chkEas.Enabled = false;
				this.chkTriangle.Enabled = false;
				this.txtAuto.Enabled = false;
				this.txtmbi.Enabled = false;
				this.txtAddress1.Enabled = false;
				this.txtAddress2.Enabled = false;
				this.txtCity.Enabled = false;
				this.txtSt.Enabled = false;
				this.txtZipCode.Enabled = false;
				this.ChkRfloor.Enabled = false;
				this.ChkCars.Enabled  = false;
                this.TXTVSCClientID.Enabled = false;
                this.TxtProfit.Enabled = false;
                this.TxtConcurso.Enabled = false;
                this.TxtMasterGap.Enabled = false;
                this.TxtProfitDealer.Enabled = false;
			}
		}

		private void FillControls()
		{	
			LookupTables.CompanyDealer companyDealer =
				(LookupTables.CompanyDealer)Session["CompanyDealer"];
			
			
			this.txtCompanyDealerDescription.Text = (companyDealer.CompanyDealerDesc != "" ?
				companyDealer.CompanyDealerDesc.ToString():
				String.Empty);

            this.txtDealerCode.Text =
                companyDealer.CompanyDealerID.Trim();

			this.txtCode.Text = 
				companyDealer.dea_unico.Trim();

			this.txtEntryDate.Text = 
				companyDealer.entry_dt.Trim();

			this.txtCoresu.Text =
				companyDealer.dea_coresu.Trim();

			this.txtName.Text = 
				companyDealer.dea_namer.Trim();

			this.chkBaldrich.Checked = 
				companyDealer.bal;

			this.chkEas.Checked = 
				companyDealer.eas;

			this.chkTriangle.Checked = 
				companyDealer.triangle;

			this.txtAuto.Text = 
				companyDealer.mgrp_auto.Trim();

			this.txtmbi.Text = 
				companyDealer.mgrp_mbi.Trim();

			this.txtAddress1.Text = 
				companyDealer.dea_add1.Trim();

			this.txtAddress2.Text = 
				companyDealer.dea_add2.Trim();

			this.txtCity.Text = 
				companyDealer.dea_city.Trim();

			this.txtSt.Text = 
				companyDealer.dea_state.Trim();

			this.txtZipCode.Text = 
				companyDealer.dea_zip.Trim();

			this.ChkRfloor.Checked = 
				companyDealer.Rfloor;

            this.TXTVSCClientID.Text =
                companyDealer.VSCClientID;

            this.TxtProfit.Text =
                companyDealer.Profit;

            this.TxtConcurso.Text =
                companyDealer.Concurso;

            this.TxtMasterGap.Text =
                companyDealer.MasterPolicyID;

            this.TxtProfitDealer.Text =
                companyDealer.ProfitDealer;
//Angel Scharon Se comenta por que no encuentro la propiedad
//			this.ChkCars.Checked = 
//				companyDealer.Cars;

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



			LookupTables.CompanyDealer companyDealer = 
				(LookupTables.CompanyDealer)Session["CompanyDealer"];
			try
			{
				switch(companyDealer.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref companyDealer);
						companyDealer.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref companyDealer);
						companyDealer.Save(userID);
						Session["CompanyDealer"] = companyDealer;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Company Dealer information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetCompanyDealerNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["CompanyDealer"] = companyDealer;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Company Dealer. " + xcp.Message);
			}
		}//End BtnSave_Click


		private void ShowMessage(string MessageText)
		{
			this.litPopUp.Text = 
				Utilities.MakeLiteralPopUpString(MessageText);
			this.litPopUp.Visible = true;
		}

		private void FillProperties(ref LookupTables.CompanyDealer companyDealer)
		{	
			try
			{
				companyDealer.CompanyDealerDesc = (this.txtCompanyDealerDescription.Text.ToString().ToUpper());
			}
			catch
			{
				this.ShowMessage("Could not fill 'Company DealerID' property. " +
					"Please enter a valid value for this field.");
			}

            companyDealer.CompanyDealerID = this.txtDealerCode.Text.Trim();
			companyDealer.dea_unico = this.txtCode.Text;
			companyDealer.entry_dt = this.txtEntryDate.Text;
			companyDealer.dea_coresu = this.txtCoresu.Text;
			companyDealer.dea_namer = this.txtName.Text.ToString().ToUpper();
			companyDealer.bal = this.chkBaldrich.Checked;
			companyDealer.eas = this.chkEas.Checked;
			companyDealer.triangle = this.chkTriangle.Checked;
			companyDealer.mgrp_auto = this.txtAuto.Text;
			companyDealer.mgrp_mbi = this.txtmbi.Text;
			companyDealer.dea_add1 = this.txtAddress1.Text.ToString().ToUpper();
			companyDealer.dea_add2 = this.txtAddress2.Text.ToString().ToUpper();
			companyDealer.dea_city = this.txtCity.Text.ToString().ToUpper();
			companyDealer.dea_state = this.txtSt.Text.ToString().ToUpper();
			companyDealer.dea_zip = this.txtZipCode.Text;
			companyDealer.Rfloor = this.ChkRfloor.Checked;
            companyDealer.VSCClientID = this.TXTVSCClientID.Text.ToString().ToUpper();
            companyDealer.Profit = this.TxtProfit.Text;
            companyDealer.Concurso = this.TxtConcurso.Text;
            companyDealer.MasterPolicyID = this.TxtMasterGap.Text;
            companyDealer.ProfitDealer = this.TxtProfitDealer.Text;
//			companyDealer.Cars = this.ChkCars.Checked; Angel Scharon Se comenta por que no encuentro la propiedad

		}

	  
			
		protected void BtnExit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.CompanyDealer companyDealer = 
				(LookupTables.CompanyDealer)Session["CompanyDealer"];
			
			if(companyDealer.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				Response.Redirect(
					"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
					LookupTables.LookupTables.GetLookupTableIdFromTableName(
					"CompanyDealer").ToString());
			}
		}

		protected void btnAddNew_Click(object sender, System.EventArgs e)
		{
				Response.Redirect("CompanyDealer.aspx");
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			LookupTables.CompanyDealer companyDealer =
				(LookupTables.CompanyDealer)Session["CompanyDealer"];			
			companyDealer.ActionMode = 2;
			Session["CompanyDealer"] = companyDealer;
			this.SetControlState((int)UserAction.EDIT);
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



			LookupTables.CompanyDealer companyDealer = 
				(LookupTables.CompanyDealer)Session["CompanyDealer"];
			try
			{
				switch(companyDealer.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref companyDealer);
						companyDealer.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref companyDealer);
						companyDealer.Save(userID);
						Session["CompanyDealer"] = companyDealer;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Company Dealer information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetCompanyDealerNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["CompanyDealer"] = companyDealer;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Company Dealer. " + xcp.Message);
			}

		}

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(
				"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
				LookupTables.LookupTables.GetLookupTableIdFromTableName(
				"CompanyDealer").ToString());
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.SetControlState((int)UserAction.CANCEL);
			this.RecordNavigation("");
		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			EPolicy2.Reports.AdministrativeTools atreport = new EPolicy2.Reports.AdministrativeTools();
			DataTable dt = atreport.CompanyDealerList();

            DataDynamics.ActiveReports.ActiveReport3 rpt = new GeneralList("Company Dealer");
		
			rpt.DataSource = dt;
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage",LookupTables.LookupTables.GetTableMaintenancePathFromTableID(29)+ "?item=" + this.lblCompanyDealerID.Text);
			Response.Redirect("ActiveXViewer.aspx",false);
		}

		protected void btnAuditTrail_Click(object sender, System.EventArgs e)
		{
			if(Session["CompanyDealer"] != null)
			{
				LookupTables.CompanyDealer companyDealer = (LookupTables.CompanyDealer) Session["CompanyDealer"];
				Response.Redirect("SearchAuditItems.aspx?type=8&companyDealerID=" + 
					companyDealer.CompanyDealerID.Trim());
			}
		}


		protected void cmdCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.SetControlState((int)UserAction.CANCEL);
			this.RecordNavigation("");
		}

		protected void btnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.CompanyDealer companyDealer =
				(LookupTables.CompanyDealer)Session["CompanyDealer"];			
			companyDealer.ActionMode = 2;
			Session["CompanyDealer"] = companyDealer;
			this.SetControlState((int)UserAction.EDIT);
		}

		protected void BtnBegin_Click(object sender, System.EventArgs e)
		{
			if(!this.IsNavigationTableNull())
			{
				this.RecordNavigation("Begin");
			}
		}

		protected void BtnPrevious_Click(object sender, System.EventArgs e)
		{
			if(!this.IsNavigationTableNull())
			{
				this.RecordNavigation("Previous");
			}
		}

		protected void BtnNext_Click(object sender, System.EventArgs e)
		{
			if(!this.IsNavigationTableNull())
			{
				this.RecordNavigation("Next");
			}
		}

		protected void BtnEnd_Click(object sender, System.EventArgs e)
		{
			if(!this.IsNavigationTableNull())
			{
				this.RecordNavigation("End");
			}
		}

		private bool IsNavigationTableNull()
		{
			LookupTables.CompanyDealer companyDealer = 
				(LookupTables.CompanyDealer)Session["companyDealer"];
			if(companyDealer.NavigationViewModelTable == null)
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
			Response.Redirect("CompanyDealer.aspx");
		}

		protected void btnSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(
				"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
				LookupTables.LookupTables.GetLookupTableIdFromTableName(
				"CompanyDealer").ToString());
		}

		protected void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			EPolicy2.Reports.AdministrativeTools atreport = new EPolicy2.Reports.AdministrativeTools();
			DataTable dt = atreport.CompanyDealerList();

            DataDynamics.ActiveReports.ActiveReport3 rpt = new GeneralList("Company Dealer");
		
			rpt.DataSource = dt;
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage",LookupTables.LookupTables.GetTableMaintenancePathFromTableID(29)+ "?item=" + this.lblCompanyDealerID.Text);
			Response.Redirect("ActiveXViewer.aspx",false);

		}

		protected void btnAuditTrail_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if(Session["CompanyDealer"] != null)
			{
				LookupTables.CompanyDealer companyDealer = (LookupTables.CompanyDealer) Session["CompanyDealer"];
				Response.Redirect("SearchAuditItems.aspx?type=8&companyDealerID=" + 
					companyDealer.CompanyDealerID.Trim());
			}
		}

		protected void BtnExit_Click(object sender, System.EventArgs e)
		{
			LookupTables.CompanyDealer companyDealer = 
				(LookupTables.CompanyDealer)Session["CompanyDealer"];
			
			if(companyDealer.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				Response.Redirect(
					"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
					LookupTables.LookupTables.GetLookupTableIdFromTableName(
					"CompanyDealer").ToString());
			}
		}

	}

}




		
	

