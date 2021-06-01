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
using DataDynamics;

namespace EPolicy
{
	/// <summary>
	/// Summary description for InsuranceCompany.
	/// </summary>
	public partial class InsuranceCompany : System.Web.UI.Page
	{
		private Control LeftMenu;

		
		
		
		#region Enumerations

		public enum UserAction{ADD = 1, VIEW = 2, SAVE = 3, EDIT = 4, CANCEL = 5};
		
		#endregion



		private void SetInsuranceCompanyNumerationLabel()
		{
			LookupTables.InsuranceCompany insuranceCompany = 
				(LookupTables.InsuranceCompany)Session["InsuranceCompany"];

			this.lblInsuranceCompanyID.Text = (insuranceCompany.InsuranceCompanyID != "") ?
				insuranceCompany.InsuranceCompanyID.ToString():
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
				if(!cp.IsInRole("INSURANCECOMPANY") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("HomePage.aspx?001");
				}
			}

			if(!Page.IsPostBack)
			{
				LookupTables.InsuranceCompany insuranceCompany = new LookupTables.InsuranceCompany();

				if(Request.QueryString["item"] != null &&																	
					Request.QueryString["item"].ToString() != String.Empty)
				{	
					try
					{
						insuranceCompany.GetInsuranceCompany(Request.QueryString["item"].ToString());
						insuranceCompany.ActionMode = 2; //UPDATE
						insuranceCompany.NavigationViewModelTable = 
							(DataTable)Session["DtRecordsForNonValuePairLookupTable"];
						Session["InsuranceCompany"] = insuranceCompany;
					}
					catch(Exception ex)
					{
						this.ShowMessage("There is no InsuranceCompany for the supplied " +
							"ID. " + ex.Message);
					}
				}
				else
				{
					insuranceCompany.ActionMode = 1; //ADD
					Session["InsuranceCompany"] = insuranceCompany;
				}
				
			}

			if (Session["InsuranceCompany"] != null)
			{
				LookupTables.InsuranceCompany insuranceCompany = 
					(LookupTables.InsuranceCompany)Session["InsuranceCompany"];

				this.InitializeControls();
			
				switch(insuranceCompany.ActionMode)
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

							if (insuranceCompany.NavigationViewModelTable != null)
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

			Control Banner = new Control();
			Banner = LoadControl(@"TopBanner.ascx");
			this.Placeholder1.Controls.Add(Banner);

			//Setup Left-side Banner
			LeftMenu = new Control();
			LeftMenu = LoadControl(@"LeftMenu.ascx");
			phTopBanner1.Controls.Add(LeftMenu);
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
			this.SetInsuranceCompanyNumerationLabel();
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
					this.cmdCancel.Visible = true;
					this.btnAddNew.Visible = false;
					//this.btnCalendar.Visible = true;
					this.btnAuditTrail.Visible = false;
					this.btnSearch.Visible = false;
					this.btnPrint.Visible = false;
					this.BtnExit.Visible = false;

					break;
				case 2: //VIEW ACTION
					this.EnableRecordNavigationControl(true);
					this.EnableInputControls(false);
					this.btnEdit.Visible = true;
					this.BtnSave.Visible = false;
					this.cmdCancel.Visible = false;
					this.btnAddNew.Visible = true;
					//this.btnCalendar.Visible = false;
					//this.btnAuditTrail.Visible = true;
					this.btnSearch.Visible = true;
					//this.btnPrint.Visible = true;
					this.BtnExit.Visible = true;
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
					//this.btnCalendar.Visible = true;
					this.btnAuditTrail.Visible = false;
					this.btnSearch.Visible = false;
					this.btnPrint.Visible = false;
					this.BtnExit.Visible = false;
					break;
				default: //CANCEL ACTION
					LookupTables.InsuranceCompany insuranceCompany = 
						(LookupTables.InsuranceCompany)Session["InsuranceCompany"];
					if(insuranceCompany.ActionMode == 0) //ADD
					{
						Session["InsuranceCompany"] = null;
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
			if(Session["InsuranceCompany"] != null)
			{
				LookupTables.InsuranceCompany insuranceCompany = 
					(LookupTables.InsuranceCompany)Session["InsuranceCompany"];
				string insuranceCompanyID = insuranceCompany.InsuranceCompanyID;

				if(insuranceCompany.NavigationViewModelTable != null)
				{
					int rec = 0;

					for(int i=0; 
						i<= (insuranceCompany.NavigationViewModelTable.Rows.Count - 1); i++)
					{
						if (insuranceCompanyID == 
							insuranceCompany.NavigationViewModelTable.Rows[i]["InsuranceCompanyID"].ToString())
						{
							if (Case == "")
							{
								rec = i + 1;
								lblRecordCount.Text = 
									rec.ToString() + " of " +
									insuranceCompany.NavigationViewModelTable.Rows.Count;
							}
							else if (Case == "Previous" || Case == "Begin")
							{
								rec = 1;
								if (i > 0)
								{
									switch(Case)
									{
										case "Previous":
											insuranceCompanyID = 
												insuranceCompany.NavigationViewModelTable.Rows[i - 1]["InsuranceCompanyID"].ToString();
											rec = i;
											break;
										case "Begin":
											insuranceCompanyID
												= 
												insuranceCompany.NavigationViewModelTable.Rows[0]["InsuranceCompanyID"].ToString();
											rec = 1;
											break;
									}
									lblRecordCount.Text = 
										rec.ToString() + " of " + 
										insuranceCompany.NavigationViewModelTable.Rows.Count;
									i = 
										insuranceCompany.NavigationViewModelTable.Rows.Count-1; 
									// Para salir del loop.
								}
							}
							else if (Case == "Next" || Case == "End")
							{
								if (insuranceCompany.NavigationViewModelTable.Rows.Count-1 > i)
								{					
									switch(Case)
									{
										case "Next":
											insuranceCompanyID = 
												
												insuranceCompany.NavigationViewModelTable.Rows[i + 1]["InsuranceCompanyID"].ToString();
											rec = i + 2;
											break;
										case "End":
											insuranceCompanyID =
												insuranceCompany.NavigationViewModelTable.Rows[insuranceCompany.NavigationViewModelTable.Rows.Count - 1]["InsuranceCompanyID"].ToString();
											rec = insuranceCompany.NavigationViewModelTable.Rows.Count;
											break;
									}
									lblRecordCount.Text = 
										rec.ToString() + " of " + 
										insuranceCompany.NavigationViewModelTable.Rows.Count;
									i = insuranceCompany.NavigationViewModelTable.Rows.Count-1; 
									// Para salir del loop.
								}
							}
						}
					}																
				}
				DataTable dtInsuranceCompany = insuranceCompany.NavigationViewModelTable;
				
				try
				{
					insuranceCompany.GetInsuranceCompany(insuranceCompanyID);
			
					insuranceCompany.NavigationViewModelTable = dtInsuranceCompany;

					Session.Add("InsuranceCompany", insuranceCompany);
					this.FillControls();
					this.SetControlState((int)UserAction.VIEW);
					this.SetInsuranceCompanyNumerationLabel();
				}
				catch(Exception ex)
				{
					this.ShowMessage("There is no Insurance Company for the supplied " +
						"ID. " + ex.Message);
				}
			}
		}// End RecordNavigation

		private void EnableInputControls(bool State)
		{
			if(State)
			{
				this.txtInsuranceDescription.Enabled = true;
				this.txtInsuranceName.Enabled = true;
				this.txtAddress1.Enabled = true;
				this.txtAddress2.Enabled = true;
				this.txtCity.Enabled = true;
				this.txtSt.Enabled = true;
				this.txtZipCode.Enabled = true;
				this.txtPhone.Enabled = true;
				this.txtEntryDate.Enabled = true;
				this.chkPrintPolicy.Enabled = true;
				this.txtSequence.Enabled = true;
				this.chkPrintCancel.Enabled = true;
				this.chkPrintLabels.Enabled = true;
				this.txtApuntador.Enabled = true;
				this.txtQuote.Enabled = true;
				this.txtApun_Trams.Enabled = true;
				this.btnCalendar.EnableViewState = true;
				
				

			}
			else
			{
				this.txtInsuranceDescription.Enabled = false;
				this.txtInsuranceName.Enabled = false;
				this.txtAddress1.Enabled = false;
				this.txtAddress2.Enabled = false;
				this.txtCity.Enabled = false;
				this.txtSt.Enabled = false;
				this.txtZipCode.Enabled = false;
				this.txtPhone.Enabled = false; 
				this.txtEntryDate.Enabled = false;
				this.chkPrintPolicy.Enabled = false;
				this.txtSequence.Enabled = false;
				this.chkPrintCancel.Enabled = false;
				this.txtApuntador.Enabled = false;
				this.chkPrintLabels.Enabled = false;
				this.txtQuote.Enabled = false;
				this.txtApun_Trams.Enabled = false;
				this.btnCalendar.EnableViewState = false;
				
				

			}
		}


		private void FillControls()
		{	
			LookupTables.InsuranceCompany insuranceCompany =
				(LookupTables.InsuranceCompany)Session["InsuranceCompany"];
			
			
			this.txtInsuranceDescription.Text = (insuranceCompany.InsuranceCompanyDesc != "" ?
				insuranceCompany.InsuranceCompanyDesc.ToString():
				String.Empty);

			this.txtInsuranceName.Text = 
				insuranceCompany.ins_names.Trim();

			this.txtAddress1.Text = 
				insuranceCompany.ins_addr1.Trim();

			this.txtAddress2.Text = 
				insuranceCompany.ins_addr2.Trim();

			this.txtCity.Text =
				insuranceCompany.ins_city.Trim();

			this.txtSt.Text = 
				insuranceCompany.ins_st.Trim();

			this.txtZipCode.Text = 
				insuranceCompany.ins_zip.Trim();

			this.txtPhone.Text = 
				insuranceCompany.ins_phone.Trim();

			this.txtEntryDate.Text = 
				insuranceCompany.ins_actdt.Trim();

			this.chkPrintPolicy.Checked = 
				insuranceCompany.ins_print;

			this.txtSequence.Text = 
				insuranceCompany.ins_seq.ToString().Trim();

			this.chkPrintCancel.Checked = 
				insuranceCompany.ins_canpr;

			this.chkPrintLabels.Checked = 
				insuranceCompany.ins_labpr;

			this.txtApuntador.Text = 
				insuranceCompany.apuntador.ToString().Trim();

			this.txtQuote.Text =
				insuranceCompany.quote.ToString().Trim();

			this.txtApun_Trams.Text = 
				insuranceCompany.apun_trams.ToString().Trim();
			
			

			

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


			LookupTables.InsuranceCompany insuranceCompany = 
				(LookupTables.InsuranceCompany)Session["InsuranceCompany"];
			try
			{
				switch(insuranceCompany.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref insuranceCompany);
						insuranceCompany.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref insuranceCompany);
						insuranceCompany.Save(userID);
						Session["InsuranceCompany"] = insuranceCompany;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Insurance Company information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetInsuranceCompanyNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["InsuranceCompany"] = insuranceCompany;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Insurance Company. " + xcp.Message);
			}
		}//End BtnSave_Click


		private void ShowMessage(string MessageText)
		{
			this.litPopUp.Text = 
				Utilities.MakeLiteralPopUpString(MessageText);
			this.litPopUp.Visible = true;
		}

		private void FillProperties(ref LookupTables.InsuranceCompany insuranceCompany)
		{	
			try
			{
				insuranceCompany.InsuranceCompanyDesc = (this.txtInsuranceDescription.Text.ToString().ToUpper());
			}
			catch
			{
				this.ShowMessage("Could not fill 'Insurance CompanyID' property. " +
					"Please enter a valid value for this field.");
			}
			insuranceCompany.ins_names = this.txtInsuranceName.Text.ToString().Trim().ToUpper();
			insuranceCompany.ins_addr1 = this.txtAddress1.Text.ToString().Trim().ToUpper();
			insuranceCompany.ins_addr2 = this.txtAddress2.Text.Trim();
			insuranceCompany.ins_city = this.txtCity.Text.ToString().Trim().ToUpper();
			insuranceCompany.ins_st = this.txtSt.Text.Trim().ToUpper();
			insuranceCompany.ins_zip = this.txtZipCode.Text.Trim();
			insuranceCompany.ins_phone = this.txtPhone.Text.Trim();
			insuranceCompany.ins_actdt = this.txtEntryDate.Text.Trim();
			insuranceCompany.ins_seq = int.Parse(this.txtSequence.Text);
			insuranceCompany.apuntador = int.Parse(this.txtApuntador.Text);
			insuranceCompany.quote = int.Parse( this.txtQuote.Text);
			insuranceCompany.apun_trams = int.Parse (this.txtApun_Trams.Text);
			insuranceCompany.ins_print = this.chkPrintPolicy.Checked;
			insuranceCompany.ins_canpr = this.chkPrintCancel.Checked;
			insuranceCompany.ins_labpr = this.chkPrintLabels.Checked;

		}

	  
			
		protected void BtnExit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.InsuranceCompany insuranceCompany = 
				(LookupTables.InsuranceCompany)Session["InsuranceCompany"];
			
			if(insuranceCompany.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				Response.Redirect(
					"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
					LookupTables.LookupTables.GetLookupTableIdFromTableName(
					"InsuranceCompany").ToString());
			}
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			LookupTables.InsuranceCompany insuranceCompany = (LookupTables.InsuranceCompany)Session["InsuranceCompany"];
			
			if(insuranceCompany.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				this.SetControlState((int)UserAction.CANCEL);
			}
		}

		protected void btnAddNew_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("InsuranceCompany.aspx");
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			LookupTables.InsuranceCompany insuranceCompany =
				(LookupTables.InsuranceCompany)Session["InsuranceCompany"];			
			insuranceCompany.ActionMode = 2;
			Session["InsuranceCompany"] = insuranceCompany;
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


			LookupTables.InsuranceCompany insuranceCompany = 
				(LookupTables.InsuranceCompany)Session["InsuranceCompany"];
			try
			{
				switch(insuranceCompany.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref insuranceCompany);
						insuranceCompany.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref insuranceCompany);
						insuranceCompany.Save(userID);
						Session["InsuranceCompany"] = insuranceCompany;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Insurance Company information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetInsuranceCompanyNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["InsuranceCompany"] = insuranceCompany;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Insurance Company. " + xcp.Message);
			}
		}

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(
				"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
				LookupTables.LookupTables.GetLookupTableIdFromTableName(
				"InsuranceCompany").ToString());
		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			EPolicy2.Reports.AdministrativeTools atreport = new EPolicy2.Reports.AdministrativeTools();
			DataTable dt = atreport.InsuranceCompanyList();

            DataDynamics.ActiveReports.ActiveReport3 rpt = new GeneralList("Insurance Company");
			
			//rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

			rpt.DataSource = dt;
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage",LookupTables.LookupTables.GetTableMaintenancePathFromTableID(30)+ "?item=" + this.lblInsuranceCompanyID.Text);
			Response.Redirect("ActiveXViewer.aspx",false);
		}

		protected void btnAuditTrail_Click(object sender, System.EventArgs e)
		{
			if(Session["InsuranceCompany"] != null)
			{
				LookupTables.InsuranceCompany insuranceCompany = (LookupTables.InsuranceCompany) Session["InsuranceCompany"];
				Response.Redirect("SearchAuditItems.aspx?type=9&insuranceCompanyID=" + 
					insuranceCompany.InsuranceCompanyID.ToString());
			}
		}


		protected void cmdCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.SetControlState((int)UserAction.CANCEL);
			this.RecordNavigation("");
		}

		protected void btnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.InsuranceCompany insuranceCompany =
				(LookupTables.InsuranceCompany)Session["InsuranceCompany"];			
			insuranceCompany.ActionMode = 2;
			Session["InsuranceCompany"] = insuranceCompany;
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
			LookupTables.InsuranceCompany insuranceCompany = 
				(LookupTables.InsuranceCompany)Session["insuranceCompany"];
			if(insuranceCompany.NavigationViewModelTable == null)
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
			Response.Redirect("InsuranceCompany.aspx");
		}

		protected void btnSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(
				"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
				LookupTables.LookupTables.GetLookupTableIdFromTableName(
				"InsuranceCompany").ToString());
		}

		protected void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			EPolicy2.Reports.AdministrativeTools atreport = new EPolicy2.Reports.AdministrativeTools();
			DataTable dt = atreport.InsuranceCompanyList();

            DataDynamics.ActiveReports.ActiveReport3 rpt = new GeneralList("Insurance Company");
			
			//rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

			rpt.DataSource = dt;
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage",LookupTables.LookupTables.GetTableMaintenancePathFromTableID(30)+ "?item=" + this.lblInsuranceCompanyID.Text);
			Response.Redirect("ActiveXViewer.aspx",false);
		}

		protected void btnAuditTrail_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if(Session["InsuranceCompany"] != null)
			{
				LookupTables.InsuranceCompany insuranceCompany = (LookupTables.InsuranceCompany) Session["InsuranceCompany"];
				Response.Redirect("SearchAuditItems.aspx?type=9&insuranceCompanyID=" + 
					insuranceCompany.InsuranceCompanyID.ToString());
			}
		}

		protected void BtnExit_Click(object sender, System.EventArgs e)
		{
			LookupTables.InsuranceCompany insuranceCompany = 
				(LookupTables.InsuranceCompany)Session["InsuranceCompany"];
			
			if(insuranceCompany.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				Response.Redirect(
					"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
					LookupTables.LookupTables.GetLookupTableIdFromTableName(
					"InsuranceCompany").ToString());
			}
		}
	}
}