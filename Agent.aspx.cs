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
	/// Summary description for Agent.
	/// </summary>
	public partial class Agent : System.Web.UI.Page
	{
	    private Control LeftMenu;

		#region Enumerations

		public enum UserAction{ADD = 1, VIEW = 2, SAVE = 3, EDIT = 4, CANCEL = 5};
		
		#endregion

       
		private void SetAgentNumerationLabel()
		{
			LookupTables.Agent agent = 
				(LookupTables.Agent)Session["Agent"];

			this.lblAgentID.Text = (agent.AgentID != "") ?
				agent.AgentID.ToString():
				String.Empty;
		}

       protected void Page_Load(object sender, System.EventArgs e)
		{
		   Login.Login cp = HttpContext.Current.User as Login.Login;

           Control Banner = new Control();
           Banner = LoadControl(@"TopBannerNew.ascx");
           this.phTopBanner.Controls.Add(Banner);

		   if (cp == null)
		   {
			   Response.Redirect("HomePage.aspx?001");
		   }
		   else
		   {
			   if(!cp.IsInRole("AGENT") && !cp.IsInRole("ADMINISTRATOR"))
			   {
				   Response.Redirect("HomePage.aspx?001");
			   }
		   }

		   if(!Page.IsPostBack)
		   {
			   LookupTables.Agent agent = new LookupTables.Agent();

			   if(Request.QueryString["item"] != null &&																	
				   Request.QueryString["item"].ToString() != String.Empty)
			   {	
				   try
				   {
					   agent.GetAgent(Request.QueryString["item"].ToString());
					   agent.ActionMode = 2; //UPDATE
					   agent.NavigationViewModelTable = 
						   (DataTable)Session["DtRecordsForNonValuePairLookupTable"];
					   Session["Agent"] = agent;
				   }
				   catch(Exception ex)
				   {
					   this.ShowMessage("There is no Agent for the supplied " +
						   "ID. " + ex.Message);
				   }
			   }
			   else
			   {
				   agent.ActionMode = 1; //ADD
				   Session["Agent"] = agent;
			   }
				
		   }

		   if(Session["Agent"] != null)
		   {
			   LookupTables.Agent agent = 
				   (LookupTables.Agent)Session["Agent"];

			   this.InitializeControls();
			
			   switch(agent.ActionMode)
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

           if (Session["OTHERTABLE"].ToString() != "")
           {
               if (Session["OTHERTABLE"].ToString() == "Agent")
               {
                   this.txtAgentType.Text = "1";
               }
               else if (Session["OTHERTABLE"].ToString() == "AgentVI")
               {
                   this.txtAgentType.Text = "2";
               }
               else
               {

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
			
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{   
            //this.btnEdit1.Click += new System.Web.UI.ImageClickEventHandler(this.btnEdit_Click);
            //this.BtnSave1.Click += new System.Web.UI.ImageClickEventHandler(this.BtnSave_Click);
            //this.btnSearch1.Click += new System.Web.UI.ImageClickEventHandler(this.btnSearch_Click);
            //this.cmdCancel1.Click += new System.Web.UI.ImageClickEventHandler(this.cmdCancel_Click);
            //this.btnPrint1.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrint_Click);
            //this.btnAuditTrail1.Click += new System.Web.UI.ImageClickEventHandler(this.btnAuditTrail_Click);
            //this.BtnExit1.Click += new System.Web.UI.ImageClickEventHandler(this.BtnExit_Click);

		}
		#endregion
        			
		
		private void InitializeControls()
		{
			this.SetAgentNumerationLabel();
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
					this.BtnExit.Visible = false;
					break;
				case 2: //VIEW ACTION
					this.EnableInputControls(false);
					this.btnEdit.Visible = true;
					this.BtnSave.Visible = false;
					this.cmdCancel.Visible = false;
					this.btnAddNew.Visible = true;
					this.btnCommission.Visible = true;
					this.btnAuditTrail.Visible = false;
					//this.btnPrint.Visible = true;
					this.btnSearch.Visible = true;
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
					this.btnCommission.Visible = false;
					this.txtEntryDate.Enabled = false;
					this.btnAuditTrail.Visible = false;
					this.btnPrint.Visible = false;
					this.btnSearch.Visible = false;
					this.BtnExit.Visible = false;
					break;
				default: //CANCEL ACTION
					LookupTables.Agent agent = 
						(LookupTables.Agent)Session["Agent"];
					if(agent.ActionMode == 0) //ADD
					{
						Session["Agent"] = null;
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
				this.txtAgentDescription.Enabled = true;
				this.txtAddress1.Enabled = true;
				this.txtAddress2.Enabled = true;
				this.txtCity.Enabled = true;
				this.txtSt.Enabled = true;
				this.txtZipCode.Enabled = true;
				this.txtPhone.Enabled = true;
				this.txtEntryDate.Enabled = true;
                this.txtCarsID.Enabled = true;

			}
			else
			{
				this.txtAgentDescription.Enabled = false;
				this.txtAddress1.Enabled = false;
				this.txtAddress2.Enabled = false;
				this.txtCity.Enabled = false;
				this.txtSt.Enabled = false;
				this.txtZipCode.Enabled = false;
				this.txtPhone.Enabled = false;
				this.txtEntryDate.Enabled = false;
                this.txtCarsID.Enabled = false;
			}
		}

		private void FillControls()
		{	
			LookupTables.Agent agent =
				(LookupTables.Agent)Session["Agent"];
			
			
			this.txtAgentDescription.Text = (agent.AgentDesc != "" ? agent.AgentDesc.ToString(): String.Empty);
			
			this.txtAddress1.Text = 
				agent.agt_addr1.Trim();

			this.txtAddress2.Text = 
				agent.agt_addr2.Trim();

			this.txtCity.Text =
				agent.agt_city.Trim();

			this.txtSt.Text = 
				agent.agt_st.Trim();

			this.txtZipCode.Text = 
				agent.agt_zip.Trim();

			this.txtPhone.Text = 
				agent.agt_phone.Trim();

			this.txtEntryDate.Text = 
				agent.agt_actdt.Trim();

            this.txtCarsID.Text = 
                agent.CarsID;

            this.txtAgentType.Text =
                agent.AgentType;


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

			LookupTables.Agent agent = 
				(LookupTables.Agent)Session["Agent"];
			try
			{
				switch(agent.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref agent);
						agent.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref agent);
						agent.Save(userID);
						Session["Agent"] = agent;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Agent information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetAgentNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["Agent"] = agent;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Agent. " + xcp.Message);
			}
		}//End BtnSave_Click



		private void ShowMessage(string MessageText)
		{
            lblRecHeader.Text = MessageText;
            mpeSeleccion.Show();

            //this.litPopUp.Text = 
            //    Utilities.MakeLiteralPopUpString(MessageText);
            //this.litPopUp.Visible = true;
		}

		private void FillProperties(ref LookupTables.Agent agent)
		{	
			try
			{
				agent.AgentDesc = (this.txtAgentDescription.Text.ToString().ToUpper());
			}
			catch
			{
				this.ShowMessage("Could not fill 'AgentID' property. " +
					"Please enter a valid value for this field.");
			}
			agent.agt_addr1 = this.txtAddress1.Text.ToString().ToUpper();
			agent.agt_addr2 = this.txtAddress2.Text.ToString().ToUpper();
			agent.agt_city = this.txtCity.Text.ToString().ToUpper();
			agent.agt_st = this.txtSt.Text.ToString().ToUpper();
			agent.agt_zip = this.txtZipCode.Text;
			agent.agt_phone = this.txtPhone.Text;
			agent.agt_actdt = this.txtEntryDate.Text;
            agent.CarsID = this.txtCarsID.Text;
            agent.AgentType = this.txtAgentType.Text;


		}

	  
			
		protected void BtnExit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.Agent agent = 
				(LookupTables.Agent)Session["Agent"];
			
			if(agent.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				Response.Redirect(
					"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
					LookupTables.LookupTables.GetLookupTableIdFromTableName(
					"Agent").ToString());
			}
		}


		protected void cmdCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.SetControlState((int)UserAction.CANCEL);
		}

		protected void btnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.Agent agent =
				(LookupTables.Agent)Session["Agent"];			
			agent.ActionMode = 2;
			Session["Agent"] = agent;
			this.SetControlState((int)UserAction.EDIT);
		}

		private bool IsNavigationTableNull()
		{
			LookupTables.Agent agent = 
				(LookupTables.Agent)Session["agent"];
			if(agent.NavigationViewModelTable == null)
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
			Response.Redirect("Agent.aspx");
		}

		protected void btnSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(
				"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
				LookupTables.LookupTables.GetLookupTableIdFromTableName(
				"Agent").ToString());
		}

		protected void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			EPolicy2.Reports.AdministrativeTools atreport = new EPolicy2.Reports.AdministrativeTools();
			DataTable dt = atreport.AgentList();
	
			DataDynamics.ActiveReports.ActiveReport3 rpt = new  GeneralList("Agent");
			
			//rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

			rpt.DataSource = dt;
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage",LookupTables.LookupTables.GetTableMaintenancePathFromTableID(26)+ "?item=" + this.lblAgentID.Text);
			Response.Redirect("ActiveXViewer.aspx",false);
		}

		protected void btnAddNew_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Agent.aspx");
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			LookupTables.Agent agent =
				(LookupTables.Agent)Session["Agent"];			
			agent.ActionMode = 2;
			Session["Agent"] = agent;
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

			LookupTables.Agent agent = 
				(LookupTables.Agent)Session["Agent"];
			try
			{
				switch(agent.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref agent);
						agent.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref agent);
						agent.Save(userID);
						Session["Agent"] = agent;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
                //this.litPopUp.Text = 
                //    Utilities.MakeLiteralPopUpString(
                //    "The Agent information saved successfully.");
                //this.litPopUp.Visible = true;

                lblRecHeader.Text = "The Agent information saved successfully.";
                mpeSeleccion.Show();

				this.SetAgentNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["Agent"] = agent;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Agent. " + xcp.Message);
			}
		}

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
            if (Session["OTHERTABLE"].ToString() == "Agent")
            {
                Response.Redirect(
                    "SearchLookupTableDescriptions.aspx?SelectedItem=" +
                    LookupTables.LookupTables.GetLookupTableIdFromTableName(
                    "Agent").ToString());
            }
            else
            {
                Response.Redirect(
                    "SearchLookupTableDescriptions.aspx?SelectedItem=" +
                    LookupTables.LookupTables.GetLookupTableIdFromTableName(
                    "AgentVI").ToString());
            }
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{		
			LookupTables.Agent agent = (LookupTables.Agent)Session["Agent"];
			
			if(agent.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				this.SetControlState((int)UserAction.CANCEL);
			}
		}

		protected void btnAuditTrail_Click(object sender, System.EventArgs e)
		{
			if(Session["Agent"] != null)
			{
				LookupTables.Agent agent = (LookupTables.Agent) Session["Agent"];
				Response.Redirect("SearchAuditItems.aspx?type=4&agentID=" + 
					agent.AgentID.Trim());
			}
		}

		protected void BtnExit_Click(object sender, System.EventArgs e)
		{

			LookupTables.Agent agent = 
				(LookupTables.Agent)Session["Agent"];
			
			if(agent.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{

                if (Session["OTHERTABLE"].ToString() == "Agent")
                {
                    Response.Redirect(
                        "SearchLookupTableDescriptions.aspx?SelectedItem=" +
                        LookupTables.LookupTables.GetLookupTableIdFromTableName(
                        "Agent").ToString());
                }
                else
                {
                    Response.Redirect(
                        "SearchLookupTableDescriptions.aspx?SelectedItem=" +
                        LookupTables.LookupTables.GetLookupTableIdFromTableName(
                        "AgentVI").ToString());
                }
			}
		}

		protected void btnAuditTrail_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if(Session["Agent"] != null)
			{
				LookupTables.Agent agent = (LookupTables.Agent) Session["Agent"];
				Response.Redirect("SearchAuditItems.aspx?type=4&agentID=" + 
					agent.AgentID.Trim());
			}
		}

		protected void btnCommission_Click(object sender, System.EventArgs e)
		{
			LookupTables.Agent agent = (LookupTables.Agent) Session["Agent"];
			Response.Redirect("CommissionAgent.aspx?" +agent.AgentID);
		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			EPolicy2.Reports.AdministrativeTools atreport = new EPolicy2.Reports.AdministrativeTools();
			DataTable dt = atreport.AgentList();
	
			DataDynamics.ActiveReports.ActiveReport3 rpt = new  GeneralList("Agent");
			
			//rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

			rpt.DataSource = dt;
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage",LookupTables.LookupTables.GetTableMaintenancePathFromTableID(26)+ "?item=" + this.lblAgentID.Text);
			Response.Redirect("ActiveXViewer.aspx",false);
		}	
				
	}
}


	

