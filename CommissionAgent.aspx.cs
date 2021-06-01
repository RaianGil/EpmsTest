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
using Baldrich.DBRequest;


namespace EPolicy
{
	/// <summary>
	/// Summary description for CommissionAgent.
	/// </summary>
	public partial class CommissionAgent : System.Web.UI.Page
	{
		private Control LeftMenu;


		#region Enumerations

		public enum UserAction{ADD = 1, VIEW = 2, SAVE = 3, EDIT = 4, CANCEL = 5};
		
		#endregion

		private void SetCommissionAgentNumerationLabel()
		{
			LookupTables.Agent agent = 
				(LookupTables.Agent)Session["Agent"];

			this.lblAgentID.Text = (agent.AgentID != "") ?
				agent.AgentID.ToString():
				String.Empty;
		}

		private void InitializeControls()
		{
			this.SetCommissionAgentNumerationLabel();
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
				if(!cp.IsInRole("COMMISSIONAGENT") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("HomePage.aspx?001");
				}
			}

			if(!Page.IsPostBack)
			{
				LookupTables.CommissionAgent commissionAgent = new LookupTables.CommissionAgent();

				if(Request.QueryString["item"] != null &&																	
					Request.QueryString["item"].ToString() != String.Empty)
				{	
					try
					{
						commissionAgent.GetCommissionAgentByCommissionAgentID(int.Parse(Request.QueryString["item"].ToString()));
						commissionAgent.ActionMode = 2; //UPDATE
						commissionAgent.NavigationViewModelTable = 
							(DataTable)Session["DtRecordsForNonValuePairLookupTable"];
						Session["CommissionAgent"] = commissionAgent;
					}
					catch(Exception ex)
					{
						this.ShowMessage("There is no Commission Agent for the supplied " +
							"ID. " + ex.Message);
					}
				}
				else
				{
					commissionAgent.ActionMode = 1; //ADD
					Session["CommissionAgent"] = commissionAgent;
				}
				
			}

			if(Session["CommissionAgent"] != null)
			{
				LookupTables.CommissionAgent commissionAgent = 
					(LookupTables.CommissionAgent)Session["CommissionAgent"];

				this.InitializeControls();
			
				switch(commissionAgent.ActionMode)
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

		}	


		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.searchCommission.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.searchCommission_ItemCommand);
			this.searchCommission.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.searchCommission_ItemDataBound);
			this.BtnExit1.Click += new System.Web.UI.ImageClickEventHandler(this.BtnExit_Click);
			this.btnAuditTrail1.Click += new System.Web.UI.ImageClickEventHandler(this.btnAuditTrail_Click);
			this.btnPrint1.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrint_Click);
			this.cmdCancel1.Click += new System.Web.UI.ImageClickEventHandler(this.cmdCancel_Click);
			this.BtnSave1.Click += new System.Web.UI.ImageClickEventHandler(this.BtnSave_Click);
			this.btnEdit1.Click += new System.Web.UI.ImageClickEventHandler(this.btnEdit_Click);
			this.btnAddNew1.Click += new System.Web.UI.ImageClickEventHandler(this.btnAddNew_Click);

		}
		#endregion

		private void FillTextControl()
		{
			LookupTables.CommissionAgent commissionAgent = (LookupTables.CommissionAgent) Session["CommissionAgent"];
			LookupTables.Agent agent = (LookupTables.Agent) Session["Agent"];

			DataTable dt = LookupTables.CommissionAgent.GetCommissionAgentByAgentID(agent.AgentID);

			Session.Add("dtCommissionAgent",dt);
			searchCommission.DataSource = dt;
			searchCommission.DataBind();

			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();

			Session.Add("CommissionAgent", commissionAgent);

			//Policy
			ddlPolicyID.SelectedIndex = 0;
			if (commissionAgent.PolicyClassID != 0)
			{
				for(int i = 0;ddlPolicyID.Items.Count-1 >= i ;i++)
				{
					if (ddlPolicyID.Items[i].Value == commissionAgent.PolicyClassID.ToString())
					{
						ddlPolicyID.SelectedIndex = i;
						i = ddlPolicyID.Items.Count-1;
					}
				}
			}
		
			//Insurance Company
			ddlInsuranceCompany.SelectedIndex = 0;
			if(commissionAgent.InsuranceCompanyID != "000")
			{
				for(int i = 0;ddlInsuranceCompany.Items.Count-1 >= i ;i++)
				{
					if (ddlInsuranceCompany.Items[i].Value == commissionAgent.InsuranceCompanyID.ToString())
					{
						ddlInsuranceCompany.SelectedIndex = i;
						i = ddlInsuranceCompany.Items.Count-1;
					}
				}
			}

            this.lblCarsID.Text           = agent.CarsID;
			this.lblAgentID.Text          = agent.AgentID;
			this.txtPolicyType.Text       = commissionAgent.PolicyType ;
			this.txtRate.Text             = commissionAgent.CommissionRate;
			this.txtEffectiveDate.Text    = commissionAgent.EffectiveDate;
			this.txtEntryDate.Text        = commissionAgent.CommissionEntryDate;
            this.txtAgentLevel.Text = "1";//commissionAgent.AgentLevel.ToString().Trim();
			this.txtCommissionAmount.Text = commissionAgent.CommissionAmount.ToString().Trim();
            
			if (commissionAgent.RateAmt)
			{
				this.RdbCommissionAmount.Checked   = false;
				this.RdbRate.Checked			   = true;
			} 
			else
			{
				this.RdbCommissionAmount.Checked   = true;
				this.RdbRate.Checked               = false;	
			}

			this.RbdButtons();
		}

		private void RbdButtons()
		{
			if(this.RdbRate.Checked == true)
			{
				this.lblCommissionRate.Visible   = true;
				this.txtRate.Visible             = true;
				this.txtCommissionAmount.Visible = false;
				this.lblCommissionAmount.Visible = false;
			}
			else
			{
				this.lblCommissionRate.Visible   = false;
				this.txtRate.Visible             = false;
				this.txtCommissionAmount.Visible = true;
				this.lblCommissionAmount.Visible = true;
			}
		}

		private void searchCommission_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName.ToString() == "Select") // Select
			{
				int i = int.Parse(e.Item.Cells[1].Text);
				LookupTables.CommissionAgent commissionAgent = new LookupTables.CommissionAgent();
				commissionAgent.GetCommissionAgentByCommissionAgentID(i);

				Session.Add("CommissionAgent", commissionAgent);
				FillTextControl();
				this.EnableInputControls(false);
				this.SetControlState((int)UserAction.VIEW);							
			}
            else if(e.CommandName.ToString() == "Delete")//Delete
            {
                int i = int.Parse(e.Item.Cells[1].Text);
                LookupTables.CommissionAgent commissionAgent = new LookupTables.CommissionAgent();
                commissionAgent.GetCommissionAgentByCommissionAgentID(i);
                
                if (searchCommission.SelectedIndex == -1)
                {
                    searchCommission.SelectedIndex = 0;
                }

                DeleteCommissionAgentByCommissionAgentID(commissionAgent.CommissionAgentID);

                //Session.Add("CommissionAgent", commissionAgent);
                FillTextControl();
                this.EnableInputControls(false);
                this.SetControlState((int)UserAction.VIEW);



            }
			else  // Pager
			{
				searchCommission.CurrentPageIndex = int.Parse(e.CommandArgument.ToString())-1;
		
				searchCommission.DataSource = (DataTable) Session["dtCommissionAgent"];
				searchCommission.DataBind();
			}		

		}

        private void DeleteCommissionAgentByCommissionAgentID(int CommissionAgentID)
        {
            try
            {
                LookupTables.CommissionAgent commissionAgent = new LookupTables.CommissionAgent();
                commissionAgent.DeleteCommissionAgentByCommissionAgentID(CommissionAgentID);

                //FillVehicleDetailGrid();
            }
            catch (Exception xcp)
            {
                //lblRecHeader.Text = xcp.Message.Trim();
                //mpeSeleccion.Show();
            }
        }


        private void FillProperties(ref LookupTables.CommissionAgent commissionAgent)
		{	
			try
			{
				commissionAgent.AgentID =  (this.lblAgentID.Text.ToString());
			}
			catch
			{
				this.ShowMessage("Could not fill 'Commission' property. " +
					"Please enter a valid value for this field.");
			}
			
			commissionAgent.PolicyClassID        = this.ddlPolicyID.SelectedItem.Value != "" ?int.Parse(ddlPolicyID.SelectedItem.Value):0;
			commissionAgent.PolicyType           = this.txtPolicyType.Text.ToString().ToUpper();
			commissionAgent.InsuranceCompanyID   = this.ddlInsuranceCompany.SelectedItem.Value != "" ?(ddlInsuranceCompany.SelectedItem.Value):"000";
			commissionAgent.CommissionRate       = (this.txtRate.Text.ToString().Trim());
			commissionAgent.EffectiveDate        = (this.txtEffectiveDate.Text);
			commissionAgent.CommissionEntryDate  = (this.txtEntryDate.Text);
			commissionAgent.AgentLevel           = (int.Parse(this.txtAgentLevel.Text.ToString().Trim()));
			commissionAgent.CommissionAmount     = (decimal.Parse(this.txtCommissionAmount.Text.ToString().Trim()));
            commissionAgent.RateAmt              = this.RdbRate.Checked;
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
					LookupTables.CommissionAgent commissionAgent = 
						(LookupTables.CommissionAgent)Session["commissionAgent"];
					if(commissionAgent.ActionMode == 0) //ADD
					{
						Session["CommissionAgent"] = null;
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
				this.ddlPolicyID.Enabled         = true;
				this.txtPolicyType.Enabled       = true;
				this.ddlInsuranceCompany.Enabled = true;
				this.txtRate.Enabled             = true;
				this.txtEffectiveDate.Enabled    = true;
				this.txtEntryDate.Enabled        = false;
				this.txtAgentLevel.Enabled       = true;
				this.Button1.Visible             = false;
				this.txtCommissionAmount.Enabled = true;
				this.RdbRate.Enabled             = true;
				this.RdbCommissionAmount.Enabled = true;

			}
			else
			{
				this.ddlPolicyID.Enabled         = false;
				this.txtPolicyType.Enabled       = false;
				this.ddlInsuranceCompany.Enabled = false;
				this.txtRate.Enabled             = false;
				this.txtEffectiveDate.Enabled    = false;
				this.txtEntryDate.Enabled        = false;
				this.txtAgentLevel.Enabled       = false;
				this.Button1.Visible             = false;
				this.txtCommissionAmount.Enabled = false;
				this.RdbRate.Enabled             = false;
				this.RdbCommissionAmount.Enabled = false;

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

			LookupTables.CommissionAgent commissionAgent = 
				(LookupTables.CommissionAgent)Session["CommissionAgent"];
			try
			{
				switch(commissionAgent.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref commissionAgent);
						commissionAgent.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref commissionAgent);
						commissionAgent.Save(userID);
						Session["CommissionAgent"] = commissionAgent;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Commission Agent information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetCommissionAgentNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["CommissionAgent"] = commissionAgent;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Commission Agent. " + xcp.Message);
			}
		}//End BtnSave_Click


		protected void BtnExit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.CommissionAgent commissionAgent = 
				(LookupTables.CommissionAgent)Session["CommissionAgent"];
			LookupTables.Agent agent = (LookupTables.Agent) Session["Agent"];
			
			Response.Redirect(
				LookupTables.LookupTables.GetTableMaintenancePathFromTableID(26) + "?item=" + this.lblAgentID.Text);
		
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
			LookupTables.CommissionAgent commissionAgent =
				(LookupTables.CommissionAgent)Session["CommissionAgent"];			
			commissionAgent.ActionMode = 2;
			Session["CommissionAgent"] = commissionAgent;
			this.SetControlState((int)UserAction.EDIT);
		}

		private bool IsNavigationTableNull()
		{
			LookupTables.CommissionAgent commissionAgent = 
				(LookupTables.CommissionAgent)Session["commissionAgent"];
			if(commissionAgent.NavigationViewModelTable == null)
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
			Response.Redirect("CommissionAgent.aspx");
		}

		protected void BtnExit_Click(object sender, System.EventArgs e)
		{
			LookupTables.CommissionAgent commissionAgent = 
				(LookupTables.CommissionAgent)Session["CommissionAgent"];
			LookupTables.Agent agent = (LookupTables.Agent) Session["Agent"];
			
			Response.Redirect(
				LookupTables.LookupTables.GetTableMaintenancePathFromTableID(26) + "?item=" + this.lblAgentID.Text);
		
		}

		protected void btnAuditTrail_Click(object sender, System.EventArgs e)
		{
			if(Session["CommissionAgent"] != null)
			{
				LookupTables.CommissionAgent commissionAgent = (LookupTables.CommissionAgent) Session["CommissionAgent"];
				Response.Redirect("SearchAuditItems.aspx?type=14&commissionAgentID=" + 
					commissionAgent.CommissionAgentID.ToString());
			}
		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			LookupTables.Agent agent = (LookupTables.Agent) Session["Agent"];

            DataDynamics.ActiveReports.ActiveReport3 rpt = new CommissionList("Commission Agent", agent.AgentDesc, agent.AgentID);
	
			rpt.DataSource = ( DataTable) Session["dtCommissionAgent"];
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage","CommissionAgent.aspx?item= + this.lblAgentID.Text");
			Response.Redirect("ActiveXViewer.aspx",false);
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

			LookupTables.CommissionAgent commissionAgent = 
				(LookupTables.CommissionAgent)Session["CommissionAgent"];
			try
			{
				switch(commissionAgent.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref commissionAgent);
						commissionAgent.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref commissionAgent);
						commissionAgent.Save(userID);
						Session["CommissionAgent"] = commissionAgent;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Commission Agent information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetCommissionAgentNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				FillTextControl();

				Session["CommissionAgent"] = commissionAgent;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Commission Agent. " + xcp.Message);
			}
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			LookupTables.CommissionAgent commissionAgent =
				(LookupTables.CommissionAgent)Session["CommissionAgent"];			
			commissionAgent.ActionMode = 2;
			Session["CommissionAgent"] = commissionAgent;
			this.SetControlState((int)UserAction.EDIT);
		}

		protected void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{	

			LookupTables.Agent agent = (LookupTables.Agent) Session["Agent"];

            DataDynamics.ActiveReports.ActiveReport3 rpt = new CommissionList("Commission Agent", agent.AgentDesc, agent.AgentID);
	
			rpt.DataSource = ( DataTable) Session["dtCommissionAgent"];
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage","CommissionAgent.aspx?item= + this.lblAgentID.Text");
			Response.Redirect("ActiveXViewer.aspx",false);
		}

		protected void btnAuditTrail_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if(Session["CommissionAgent"] != null)
			{
				LookupTables.CommissionAgent commissionAgent = (LookupTables.CommissionAgent) Session["CommissionAgent"];
				Response.Redirect("SearchAuditItems.aspx?type=14&commissionAgentID=" + 
					commissionAgent.CommissionAgentID.ToString());
			}
		}

		protected void RdbCommissionAmount_CheckedChanged(object sender, System.EventArgs e)
		{
			txtRate.Visible             = false;
			lblCommissionRate.Visible   = false;
			txtCommissionAmount.Visible = true;
			lblCommissionAmount.Visible = true;

		}

		protected void RdbRate_CheckedChanged(object sender, System.EventArgs e)
		{
			txtRate.Visible             = true; 
			lblCommissionRate.Visible   = true;
			txtCommissionAmount.Visible = false;
			lblCommissionAmount.Visible = false;
		}

		private void searchCommission_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			LookupTables.CommissionAgent commissionAgent = (LookupTables.CommissionAgent) Session["CommissionAgent"];
			DataTable dtCommissionAgent = LookupTables.LookupTables.GetTable("CommissionAgent");

			DataColumnCollection dc = dtCommissionAgent.Columns;

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DateTime EffectiveDateField;
				DateTime EntryDateField;

				if (DataBinder.Eval(e.Item.DataItem,"EffectiveDate") != "")
				{
					EffectiveDateField = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem,"EffectiveDate","{0:MM/dd/yyyy}"));
					e.Item.Cells[10].Text = EffectiveDateField.ToShortDateString();
				}

				if (DataBinder.Eval(e.Item.DataItem,"CommissionEntryDate") != "")
				{
					EntryDateField = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem,"CommissionEntryDate","{0:MM/dd/yyyy}"));
					e.Item.Cells[11].Text = EntryDateField.ToShortDateString();
				}
			}
		}

		protected void btnAddNew_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("CommissionAgent.aspx");
		}			
	}
}
	









