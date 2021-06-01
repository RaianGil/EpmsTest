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
	/// Summary description for CommissionAgency.
	/// </summary>
	public partial class CommissionAgency : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlInputButton btnCalendar;
		private Control LeftMenu;


		#region Enumerations

		public enum UserAction{ADD = 1, VIEW = 2, SAVE = 3, EDIT = 4, CANCEL = 5};
		
		#endregion


		private void SetCommissionAgencyNumerationLabel()
		{
			
			LookupTables.Agency agency = 
				(LookupTables.Agency)Session["Agency"];

			this.lblAgencyID.Text = (agency.AgencyID != "") ?
				agency.AgencyID.ToString():
				String.Empty;
			


		}

		private void InitializeControls()
		{
			this.SetCommissionAgencyNumerationLabel();
			this.litPopUp.Visible = false;
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
				if(!cp.IsInRole("COMMISSIONAGENCY") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("HomePage.aspx?001");
				}
			}

			if(!Page.IsPostBack)
			{
				LookupTables.CommissionAgency commissionAgency = new LookupTables.CommissionAgency();

				if(Request.QueryString["item"] != null &&																	
					Request.QueryString["item"].ToString() != String.Empty)
				{	
					try
					{
						commissionAgency.GetCommissionAgencyByCommissionAgencyID(int.Parse(Request.QueryString["item"].ToString()));
						commissionAgency.ActionMode = 2; //UPDATE
						commissionAgency.NavigationViewModelTable = 
							(DataTable)Session["DtRecordsForNonValuePairLookupTable"];
						Session["CommissionAgency"] = commissionAgency;
					}
					catch(Exception ex)
					{
						this.ShowMessage("There is no Commission Agency for the supplied " +
							"ID. " + ex.Message);
					}
				}
				else
				{
					commissionAgency.ActionMode = 1; //ADD
					Session["CommissionAgency"] = commissionAgency;
				}
				
			}

			if(Session["CommissionAgency"] != null)
			{
				LookupTables.CommissionAgency commissionAgency = 
					(LookupTables.CommissionAgency)Session["CommissionAgency"];

				this.InitializeControls();
			
				switch(commissionAgency.ActionMode)
				{
					case 1: //ADD
						this.SetControlState((int)UserAction.ADD);
						if(!Page.IsPostBack)
						{
							this.FillTextControl();
						}
						break;
					
					default: //UPDATE
						if(!Page.IsPostBack)
						{
							this.FillTextControl();
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
			//((Baldrich.BaldrichWeb.Components.TopBanner)Banner).SelectedOption = (int)Baldrich.HeadBanner.MenuOptions.Home;
			this.Placeholder1.Controls.Add(Banner);

			//Setup Left-side Banner
			
			LeftMenu = new Control();
			LeftMenu = LoadControl(@"LeftMenu.ascx");
			//((Baldrich.BaldrichWeb.Components.MenuCustomers)LeftMenu).Height = "534px";
			phTopBanner1.Controls.Add(LeftMenu);

			//Load DownDropList
			DataTable dtPolicyClass	        = LookupTables.LookupTables.GetTable("PolicyClass");
			DataTable dtInsuranceCompany	= LookupTables.LookupTables.GetTable("InsuranceCompany");
			DataTable dtBank				= LookupTables.LookupTables.GetTable("Bank");
			DataTable dtCompanyDealer		= LookupTables.LookupTables.GetTable("CompanyDealer");

			//PolicyClass
			ddlPolicyID.DataSource = dtPolicyClass;
			ddlPolicyID.DataTextField = "PolicyClassDesc";
			ddlPolicyID.DataValueField = "PolicyClassID";
			ddlPolicyID.DataBind();
			ddlPolicyID.SelectedIndex = -1;
			ddlPolicyID.Items.Insert(0,"");

			//InsuranceCompany
			ddlInsuranceCompany.DataSource = dtInsuranceCompany;
			ddlInsuranceCompany.DataTextField = "InsuranceCompanyDesc";
			ddlInsuranceCompany.DataValueField = "InsuranceCompanyID";
			ddlInsuranceCompany.DataBind();
			ddlInsuranceCompany.SelectedIndex = -1;
			ddlInsuranceCompany.Items.Insert(0,"");
            
			
			//Bank
			ddlBankID.DataSource = dtBank;
			ddlBankID.DataTextField = "BankDesc";
			ddlBankID.DataValueField = "BankID";
			ddlBankID.DataBind();
			ddlBankID.SelectedIndex = -1;
			ddlBankID.Items.Insert(0,"");

			//CompanyDealer
			ddlDealerID.DataSource = dtCompanyDealer;
			ddlDealerID.DataTextField = "CompanyDealerDesc";
			ddlDealerID.DataValueField = "CompanyDealerID";
			ddlDealerID.DataBind();
			ddlDealerID.SelectedIndex = -1;
			ddlDealerID.Items.Insert(0,"");
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		/// 

		private void InitializeComponent()
		{    
			this.searchCommission.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.searchCommission_ItemCommand);
			this.searchCommission.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.searchCommission_ItemDataBound);
			this.btnAddNew1.Click += new System.Web.UI.ImageClickEventHandler(this.btnAddNew_Click);
			this.btnEdit1.Click += new System.Web.UI.ImageClickEventHandler(this.btnEdit_Click);
			this.BtnSave1.Click += new System.Web.UI.ImageClickEventHandler(this.BtnSave_Click);
			this.cmdCancel1.Click += new System.Web.UI.ImageClickEventHandler(this.cmdCancel_Click);
			this.btnPrint1.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrint_Click);
			this.btnAuditTrail1.Click += new System.Web.UI.ImageClickEventHandler(this.btnAuditTrail_Click);
			this.BtnExit1.Click += new System.Web.UI.ImageClickEventHandler(this.BtnExit_Click);

		}

		#endregion


		private void FillTextControl()
		{
			LookupTables.CommissionAgency commissionAgency = (LookupTables.CommissionAgency) Session["CommissionAgency"];
			LookupTables.Agency agency = (LookupTables.Agency) Session["Agency"];

			DataTable dt = LookupTables.CommissionAgency.GetCommissionAgencyByAgencyID(agency.AgencyID);
			
			Session.Add("dtCommissionAgency",dt);
			searchCommission.DataSource = dt;
			searchCommission.DataBind();

			Baldrich.DBRequest.DBRequest  executor = new Baldrich.DBRequest.DBRequest();

			Session.Add("CommissionAgency", commissionAgency);

			//Policy
			ddlPolicyID.SelectedIndex = 0;
			if (commissionAgency.PolicyClassID != 0)
			{
				for(int i = 0;ddlPolicyID.Items.Count-1 >= i ;i++)
				{
					if (ddlPolicyID.Items[i].Value == commissionAgency.PolicyClassID.ToString())
					{
						ddlPolicyID.SelectedIndex = i;
						i = ddlPolicyID.Items.Count-1;
					}
				}
			}
		
			//Insurance Company
			ddlInsuranceCompany.SelectedIndex = 0;
			if(commissionAgency.InsuranceCompanyID != "000")
			{
				for(int i = 0;ddlInsuranceCompany.Items.Count-1 >= i ;i++)
				{
					if (ddlInsuranceCompany.Items[i].Value == commissionAgency.InsuranceCompanyID.ToString())
					{
						ddlInsuranceCompany.SelectedIndex = i;
						i = ddlInsuranceCompany.Items.Count-1;
					}
				}
			}

			//Bank
			ddlBankID.SelectedIndex = 0;
			if(commissionAgency.BankID!= "000")
			{
				for(int i = 0;ddlBankID.Items.Count-1 >= i ;i++)
				{
					if (ddlBankID.Items[i].Value == commissionAgency.BankID.ToString())
					{
						ddlBankID.SelectedIndex = i;
						i = ddlBankID.Items.Count-1;
					}
				}
			}
		
			//CompanyDealer
			ddlDealerID.SelectedIndex = 0;
			if(commissionAgency.CompanyDealerID != "000")
			{
				for(int i = 0;ddlDealerID.Items.Count-1 >= i ;i++)
				{
					if (ddlDealerID.Items[i].Value == commissionAgency.CompanyDealerID.ToString())
					{
						ddlDealerID.SelectedIndex = i;
						i = ddlDealerID.Items.Count-1;
					}
				}
			}
			
			this.lblAgencyID.Text      = agency.AgencyID;
			this.txtPolicyType.Text    = commissionAgency.PolicyType ;
		    this.txtRate.Text          = commissionAgency.CommissionRate;
			this.txtEffectiveDate.Text = commissionAgency.EffectiveDate ;
			this.txtEntryDate.Text     = commissionAgency.CommissionEntryDate;

		}

		private void searchCommission_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.Item.ItemType.ToString() != "Pager") // Select
			{
				int i = int.Parse(e.Item.Cells[1].Text);
				LookupTables.CommissionAgency commissionAgency = new LookupTables.CommissionAgency();
				commissionAgency.GetCommissionAgencyByCommissionAgencyID(i);

				Session.Add("CommissionAgency", commissionAgency);
				FillTextControl();
				this.EnableInputControls(false);
				this.SetControlState((int)UserAction.VIEW);	
			}
			else  // Pager
			{
				searchCommission.CurrentPageIndex = int.Parse(e.CommandArgument.ToString())-1;
		
				searchCommission.DataSource = (DataTable) Session["dtCommissionAgency"];
				searchCommission.DataBind();

			}				
		}

		private void FillProperties(ref LookupTables.CommissionAgency commissionAgency)
		{	
			try
			{commissionAgency.AgencyID =  (this.lblAgencyID.Text.ToString());
			}
			catch
			{
				this.ShowMessage("Could not fill 'Commission' property. " +
					"Please enter a valid value for this field.");
			}
			
			
			commissionAgency.PolicyClassID     = this.ddlPolicyID.SelectedItem.Value != "" ?int.Parse(ddlPolicyID.SelectedItem.Value):0;
			commissionAgency.PolicyType        = this.txtPolicyType.Text.ToString().ToUpper();
			commissionAgency.InsuranceCompanyID = this.ddlInsuranceCompany.SelectedItem.Value != "" ?(ddlInsuranceCompany.SelectedItem.Value):"000";
			commissionAgency.BankID            = this.ddlBankID.SelectedItem.Value != "" ?(ddlInsuranceCompany.SelectedItem.Value):"000";
			commissionAgency.CompanyDealerID   = this.ddlDealerID.SelectedItem.Value != ""?(ddlDealerID.SelectedItem.Value):"000";
			commissionAgency.CommissionRate    = (this.txtRate.Text);
			commissionAgency.EffectiveDate     =  (this.txtEffectiveDate.Text);
			commissionAgency.CommissionEntryDate  = (this.txtEntryDate.Text);

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
					this.btnAuditTrail.Visible = false;
					this.btnPrint.Visible = false;
					this.BtnExit.Visible = false;
					break;
				case 2: //VIEW ACTION
					this.EnableInputControls(false);
					this.btnEdit.Visible = true;
					this.BtnSave.Visible = false;
					this.cmdCancel.Visible = false;
					this.btnAddNew.Visible = true;
					this.btnAuditTrail.Visible = true;
					this.btnPrint.Visible = true;
					this.BtnExit.Visible = true;
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
					this.btnAuditTrail.Visible = false;
					this.btnPrint.Visible = false;
					this.BtnExit.Visible = false;
					break;
				default: //CANCEL ACTION
					LookupTables.CommissionAgency commissionAgency = 
						(LookupTables.CommissionAgency)Session["commissionAgency"];
					if(commissionAgency.ActionMode == 0) //ADD
					{
						Session["CommissionAgency"] = null;
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
				
				this.ddlPolicyID.Enabled = true;
				this.txtPolicyType.Enabled = true;
				this.ddlInsuranceCompany.Enabled = true;
				this.ddlBankID.Enabled = true;
				this.ddlDealerID.Enabled = true;
				this.txtRate.Enabled = true;
				this.txtEffectiveDate.Enabled = true;
				this.txtEntryDate.Enabled = false;
				//this.Button1.Visible = true;

			}
			else
			{
				
				this.ddlPolicyID.Enabled = false;
				this.txtPolicyType.Enabled = false;
				this.ddlInsuranceCompany.Enabled = false;
				this.ddlBankID.Enabled = false;
				this.ddlDealerID.Enabled = false;
				this.txtRate.Enabled = false;
				this.txtEffectiveDate.Enabled = false;
				this.txtEntryDate.Enabled = false;
				this.Button1.Visible = false;
			}
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

			LookupTables.CommissionAgency commissionAgency = 
				(LookupTables.CommissionAgency)Session["CommissionAgency"];
			try
			{
				switch(commissionAgency.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref commissionAgency);
						commissionAgency.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref commissionAgency);
						commissionAgency.Save(userID);
						Session["CommissionAgency"] = commissionAgency;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Commission Agency information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetCommissionAgencyNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["CommissionAgency"] = commissionAgency;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Commission Agency. " + xcp.Message);
			}
		}//End BtnSave_Click


		protected void BtnExit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.CommissionAgency commissionAgency = 
				(LookupTables.CommissionAgency)Session["CommissionAgency"];
			
			LookupTables.Agency agency = 
				(LookupTables.Agency)Session["Agency"];
		

			Response.Redirect(
				LookupTables.LookupTables.GetTableMaintenancePathFromTableID(25) + "?item=" + this.lblAgencyID.Text);

		
		}

		private void ShowMessage(string MessageText)
		{
			this.litPopUp.Text = 
				Utilities.MakeLiteralPopUpString(MessageText);
			this.litPopUp.Visible = true;
		}

		protected void cmdCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.SetControlState((int)UserAction.CANCEL);
		}

		protected void btnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.CommissionAgency commissionAgency =
				(LookupTables.CommissionAgency)Session["CommissionAgency"];			
			commissionAgency.ActionMode = 2;
			Session["CommissionAgency"] = commissionAgency;
			this.SetControlState((int)UserAction.EDIT);
		}



		private bool IsNavigationTableNull()
		{
			LookupTables.CommissionAgency commissionAgency = 
				(LookupTables.CommissionAgency)Session["commissionAgency"];
			if(commissionAgency.NavigationViewModelTable == null)
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
			Response.Redirect("CommissionAgency.aspx");
		}

		protected void BtnExit_Click(object sender, System.EventArgs e)
		{
			LookupTables.CommissionAgency commissionAgency = 
				(LookupTables.CommissionAgency)Session["CommissionAgency"];
			
			LookupTables.Agency agency = 
				(LookupTables.Agency)Session["Agency"];
		

			Response.Redirect(
				LookupTables.LookupTables.GetTableMaintenancePathFromTableID(25) + "?item=" + this.lblAgencyID.Text);

		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			LookupTables.Agency agency = (LookupTables.Agency) Session["Agency"];
	
			DataDynamics.ActiveReports.ActiveReport3 rpt = new  CommissionList("Commission Agency",agency.AgencyDesc,agency.AgencyID);
		
			rpt.DataSource = ( DataTable) Session["dtCommissionAgency"];
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage","CommissionAgency.aspx?item= + this.lblAgencyID.Text");
			Response.Redirect("ActiveXViewer.aspx",false);
		}

		protected void btnAuditTrail_Click(object sender, System.EventArgs e)
		{
			if(Session["CommissionAgency"] != null)
			{
				LookupTables.CommissionAgency commissionAgency = (LookupTables.CommissionAgency) Session["CommissionAgency"];
				Response.Redirect("SearchAuditItems.aspx?type=13&commissionAgencyID=" + 
					commissionAgency.CommissionAgencyID.ToString());
			}
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.SetControlState((int)UserAction.CANCEL);
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

			LookupTables.CommissionAgency commissionAgency = 
				(LookupTables.CommissionAgency)Session["CommissionAgency"];
			try
			{
				switch(commissionAgency.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref commissionAgency);
						commissionAgency.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref commissionAgency);
						commissionAgency.Save(userID);
						Session["CommissionAgency"] = commissionAgency;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Commission Agency information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetCommissionAgencyNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				FillTextControl();

				Session["CommissionAgency"] = commissionAgency;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Commission Agency. " + xcp.Message);
			}

		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			LookupTables.CommissionAgency commissionAgency =
				(LookupTables.CommissionAgency)Session["CommissionAgency"];			
			commissionAgency.ActionMode = 2;
			Session["CommissionAgency"] = commissionAgency;
			this.SetControlState((int)UserAction.EDIT);		
		}

		protected void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{ 

			LookupTables.Agency agency = (LookupTables.Agency) Session["Agency"];
	
			DataDynamics.ActiveReports.ActiveReport3 rpt = new  CommissionList("Commission Agency",agency.AgencyDesc,agency.AgencyID);
		
			rpt.DataSource = ( DataTable) Session["dtCommissionAgency"];
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage","CommissionAgency.aspx?item= + this.lblAgencyID.Text");
			Response.Redirect("ActiveXViewer.aspx",false);
		}

		protected void btnAuditTrail_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if(Session["CommissionAgency"] != null)
			{
				LookupTables.CommissionAgency commissionAgency = (LookupTables.CommissionAgency) Session["CommissionAgency"];
				Response.Redirect("SearchAuditItems.aspx?type=13&commissionAgencyID=" + 
					commissionAgency.CommissionAgencyID.ToString());
			}
		}

		private void searchCommission_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			LookupTables.CommissionAgency commissionAgency = (LookupTables.CommissionAgency) Session["CommissionAgency"];
			DataTable dtCommissionAgency = LookupTables.LookupTables.GetTable("CommissionAgency");

			DataColumnCollection dc = dtCommissionAgency.Columns;

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DateTime EffectiveDateField;
				DateTime EntryDateField;

				if (DataBinder.Eval(e.Item.DataItem,"EffectiveDate") != "")
				{
					EffectiveDateField = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem,"EffectiveDate","{0:MM/dd/yyyy}"));
					e.Item.Cells[9].Text = EffectiveDateField.ToShortDateString();
				}

				if (DataBinder.Eval(e.Item.DataItem,"CommissionEntryDate") != "")
				{
					EntryDateField = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem,"CommissionEntryDate","{0:MM/dd/yyyy}"));
					e.Item.Cells[10].Text = EntryDateField.ToShortDateString();
				}
			}
		
		}

		protected void btnAddNew_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("CommissionAgency.aspx");
		}				
	}
}

	

