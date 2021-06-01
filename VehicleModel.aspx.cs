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
	/// Summary description for VehicleModel.
	/// </summary>
	public partial class VehicleModel : System.Web.UI.Page
	{
	    private Control LeftMenu;

		#region Enumerations

		public enum UserAction{ADD = 1, VIEW = 2, SAVE = 3, EDIT = 4, CANCEL = 5};
		
		#endregion

		private void SetVehicleModelNumerationLabel()
		{
			LookupTables.VehicleModel vehicleModel = 
				(LookupTables.VehicleModel)Session["VehicleModel"];

			this.lblVehicleModelID.Text = (vehicleModel.VehicleModelID != 0) ?
				vehicleModel.VehicleModelID.ToString():
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
				if(!cp.IsInRole("VEHICLEMODEL") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("HomePage.aspx?001");
				}
			}

			if(!Page.IsPostBack)
			{
				LookupTables.VehicleModel vehicleModel = new LookupTables.VehicleModel();

				if(Request.QueryString["item"] != null && 
					Request.QueryString["item"].ToString() != String.Empty)
				{	
					try
					{
						vehicleModel.GetVehicleModel(int.Parse(Request.QueryString["item"].ToString()));
						vehicleModel.ActionMode = 2; //UPDATE
						vehicleModel.NavigationViewModelTable = 
							(DataTable)Session["DtRecordsForNonValuePairLookupTable"];
						Session["VehicleModel"] = vehicleModel;
					}
					catch(Exception ex)
					{
						this.ShowMessage("There is no vehicle model for the supplied " +
							"ID. " + ex.Message);
					}
				}
				else
				{
                    vehicleModel.ActionMode = 1; //ADD
					Session["VehicleModel"] = vehicleModel;
				}
				
			}

			if(Session["VehicleModel"] != null)
			{
				LookupTables.VehicleModel vehicleModel = 
					(LookupTables.VehicleModel)Session["VehicleModel"];

				this.InitializeControls();
			
				switch(vehicleModel.ActionMode)
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

							if (vehicleModel.NavigationViewModelTable != null)
							{
								//this.RecordNavigation("");
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
			
			//Load DownDropList
			DataTable dtMake = LookupTables.LookupTables.GetTable("VehicleMake");
			DataTable dtModel = LookupTables.LookupTables.GetTable("VehicleModel");

			//Make
			ddlVehicleMake.DataSource = dtMake;
			ddlVehicleMake.DataTextField = "VehicleMakeDesc";
			ddlVehicleMake.DataValueField = "VehicleMakeID";
			ddlVehicleMake.DataBind();
//			ddlVehicleMake.SelectedIndex = -1;
//			ddlVehicleMake.Items.Insert(0,"");

			FillModelDDList();
			
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
            //this.BtnExit1.Click += new System.Web.UI.ImageClickEventHandler(this.BtnExit_Click);
            //this.btnAuditTrail1.Click += new System.Web.UI.ImageClickEventHandler(this.btnAuditTrail_Click);
            //this.btnPrint1.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrint_Click);
            //this.cmdCancel1.Click += new System.Web.UI.ImageClickEventHandler(this.cmdCancel_Click);
            //this.btnSearch1.Click += new System.Web.UI.ImageClickEventHandler(this.btnSearch_Click);
            //this.BtnSave1.Click += new System.Web.UI.ImageClickEventHandler(this.BtnSave_Click);
            //this.btnEdit1.Click += new System.Web.UI.ImageClickEventHandler(this.btnEdit_Click);
            //this.btnAddNew1.Click += new System.Web.UI.ImageClickEventHandler(this.btnAddNew_Click);

		}
		#endregion

		private void InitializeControls()
		{
			this.SetVehicleModelNumerationLabel();
			this.litPopUp.Visible = false;
		}

		private void SetControlState(int Action)
		{
			switch(Action)
			{
				case 1: //ADD ACTION
					this.EnableRecordNavigationControl(false);
					this.EnableInputControls(true);
					this.btnEdit.Visible = true;
					this.BtnSave.Visible = false;
					this.cmdCancel.Visible = false;
					this.btnAddNew.Visible = true;
					break;
				case 2: //VIEW ACTION
					this.EnableRecordNavigationControl(true);
					this.EnableInputControls(false);
					this.btnEdit.Visible = true;
					this.BtnSave.Visible = false;
					this.cmdCancel.Visible = false;
					this.btnAddNew.Visible = true;
                    this.txtVehicleModelDescription.Text = "";
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
					break;
				default: //CANCEL ACTION
					LookupTables.VehicleModel vehicleModel = 
						(LookupTables.VehicleModel)Session["VehicleModel"];
					if(vehicleModel.ActionMode == 1) //ADD
					{
						Session["VehicleModel"] = null;
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
                //this.BtnBegin.Enabled = true;
                //this.BtnEnd.Enabled = true;
                //this.BtnNext.Enabled = true;
                //this.BtnPrevious.Enabled = true;
			}
			else
			{
                //this.BtnBegin.Enabled = false;
                //this.BtnEnd.Enabled = false;
                //this.BtnNext.Enabled = false;
                //this.BtnPrevious.Enabled = false;
			}
		}

		private void RecordNavigation(string Case)
		{
            //if(Session["VehicleModel"] != null)
            //{
            //    LookupTables.VehicleModel vehicleModel = 
            //        (LookupTables.VehicleModel)Session["VehicleModel"];
            //    int vehicleModelID = vehicleModel.VehicleModelID;

            //    if(vehicleModel.NavigationViewModelTable != null)
            //    {
            //        int rec = 0;

            //        for(int i=0; 
            //            i<= (vehicleModel.NavigationViewModelTable.Rows.Count - 1); i++)
            //        {
            //            if (vehicleModelID == 
            //                int.Parse(
            //                vehicleModel.NavigationViewModelTable.Rows[i]["VehicleModelID"].ToString()))
            //            {
            //                if (Case == "")
            //                {
            //                    rec = i + 1;
            //                    lblRecordCount.Text = 
            //                        rec.ToString() + " of " +
            //                        vehicleModel.NavigationViewModelTable.Rows.Count;
            //                }
            //                else if (Case == "Previous" || Case == "Begin")
            //                {
            //                    rec = 1;
            //                    if (i > 0)
            //                    {
            //                        switch(Case)
            //                        {
            //                            case "Previous":
            //                                vehicleModelID = 
            //                                    int.Parse(
            //                                    vehicleModel.NavigationViewModelTable.Rows[i - 1]["VehicleModelID"].ToString());
            //                                rec = i;
            //                                break;
            //                            case "Begin":
            //                                vehicleModelID 
            //                                    = int.Parse(
            //                                    vehicleModel.NavigationViewModelTable.Rows[0]["VehicleModelID"].ToString());
            //                                rec = 1;
            //                                break;
            //                        }
            //                        lblRecordCount.Text = 
            //                            rec.ToString() + " of " + 
            //                            vehicleModel.NavigationViewModelTable.Rows.Count;
            //                        i = 
            //                            vehicleModel.NavigationViewModelTable.Rows.Count-1; 
            //                                                    // Para salir del loop.
            //                    }
            //                }
            //                else if (Case == "Next" || Case == "End")
            //                {
            //                    if (vehicleModel.NavigationViewModelTable.Rows.Count-1 > i)
            //                    {					
            //                        switch(Case)
            //                        {
            //                            case "Next":
            //                                vehicleModelID = 
            //                                    int.Parse(
            //                                    vehicleModel.NavigationViewModelTable.Rows[i + 1]["VehicleModelID"].ToString());
            //                                rec = i + 2;
            //                                break;
            //                            case "End":
            //                                vehicleModelID =
            //                                    int.Parse(
            //                                    vehicleModel.NavigationViewModelTable.Rows[vehicleModel.NavigationViewModelTable.Rows.Count - 1]["VehicleModelID"].ToString());
            //                                rec = vehicleModel.NavigationViewModelTable.Rows.Count;
            //                                break;
            //                        }
            //                        lblRecordCount.Text = 
            //                            rec.ToString() + " of " + 
            //                            vehicleModel.NavigationViewModelTable.Rows.Count;
            //                        i = vehicleModel.NavigationViewModelTable.Rows.Count-1; 
            //                                                        // Para salir del loop.
            //                    }
            //                }
            //            }
            //        }																
            //    }
            //    DataTable dtVehicleModel = vehicleModel.NavigationViewModelTable;
				
            //    try
            //    {
            //        vehicleModel.GetVehicleModel(vehicleModelID);
			
            //        vehicleModel.NavigationViewModelTable = dtVehicleModel;

            //        Session.Add("VehicleModel", vehicleModel);
            //        this.FillControls();
            //        this.SetControlState((int)UserAction.VIEW);
            //        this.SetVehicleModelNumerationLabel();
            //    }
            //    catch(Exception ex)
            //    {
            //        this.ShowMessage("There is no vehicle model for the supplied " +
            //            "ID. " + ex.Message);
            //    }
            //}
		}// End RecordNavigation

		private void EnableInputControls(bool State)
		{
			if(State)
			{
				//this.txtVehicleMakeID.Enabled = true;
				this.txtVehicleModelDescription.Enabled = true;
			}
			else
			{
				//this.txtVehicleMakeID.Enabled = false;
				//this.txtVehicleModelDescription.Enabled = false;
			}
		}

		private void FillControls()
		{	
			LookupTables.VehicleModel vehicleModel =
				(LookupTables.VehicleModel)Session["VehicleModel"];
			
			
//			this.txtVehicleMakeID.Text = (vehicleModel.VehicleMakeID != 0) ?
//				vehicleModel.VehicleMakeID.ToString():
//				String.Empty;

			//Make
			ddlVehicleMake.SelectedIndex = 0;
			if(vehicleModel.VehicleMakeID != 0)
			{
				for(int i = 0;ddlVehicleMake.Items.Count-1 >= i ;i++)
				{
					if (ddlVehicleMake.Items[i].Value == vehicleModel.VehicleMakeID.ToString())
					{
						ddlVehicleMake.SelectedIndex = i;
						i = ddlVehicleMake.Items.Count-1;
					}
				}
			}


			FillModelDDList();

			//Model
			ddlModel.SelectedIndex = 0;
			if(vehicleModel.VehicleModelID != 0)
			{
				for(int i = 0;ddlModel.Items.Count-1 >= i ;i++)
				{
					if (ddlModel.Items[i].Value == vehicleModel.VehicleModelID.ToString())
					{
						ddlModel.SelectedIndex = i;
						i = ddlModel.Items.Count-1;
					}
				}
			}

			this.txtVehicleModelDescription.Text = 
				vehicleModel.VehicleModelDescription.Trim();
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

			LookupTables.VehicleModel vehicleModel = 
				(LookupTables.VehicleModel)Session["VehicleModel"];
			try
			{
				switch(vehicleModel.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref vehicleModel);
						vehicleModel.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref vehicleModel);
						vehicleModel.Save(userID);
						Session["VehicleModel"] = vehicleModel;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
						"Vehicle model information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetVehicleModelNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["VehicleModel"] = vehicleModel;
				FillModelDDList();
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify vehicle model. " + xcp.Message);
			}
		}//End BtnSave_Click

		private void ShowMessage(string MessageText)
		{
			this.litPopUp.Text = 
				Utilities.MakeLiteralPopUpString(MessageText);
			this.litPopUp.Visible = true;
		}

		private void FillProperties(ref LookupTables.VehicleModel VehicleModel)
		{	
			try
			{
				//VehicleModel.VehicleMakeID = int.Parse(this.txtVehicleMakeID.Text);
				VehicleModel.VehicleMakeID	= ddlVehicleMake.SelectedItem.Value != "" ?int.Parse(ddlVehicleMake.SelectedItem.Value):0;
				VehicleModel.VehicleModelID	= ddlModel.SelectedItem.Value != "" ?int.Parse(ddlModel.SelectedItem.Value):0;
			}
			catch
			{
				this.ShowMessage("Could not fill 'VehicleMakeID' property. " +
					"Please enter a valid value for this field.");
			}
			VehicleModel.VehicleModelDescription = this.txtVehicleModelDescription.Text;
		}

		protected void BtnExit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.VehicleModel vehicleModel = 
				(LookupTables.VehicleModel)Session["VehicleModel"];
			
			if(vehicleModel.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				Response.Redirect(
					"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
					LookupTables.LookupTables.GetLookupTableIdFromTableName(
					"VehicleModel").ToString());
			}
		}

        //protected void btnAddNew_Click(object sender, System.EventArgs e)
        //{
        //        Response.Redirect("VehicleModel.aspx");
        //}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			LookupTables.VehicleModel vehicleModel =
				(LookupTables.VehicleModel)Session["VehicleModel"];
			vehicleModel.ActionMode = 2;
			Session["VehicleModel"] = vehicleModel;
			this.SetControlState((int)UserAction.EDIT);
		}

        protected void btnAddNew_Click(object sender, System.EventArgs e)
        {
            LookupTables.VehicleModel vehicleModel =
                (LookupTables.VehicleModel)Session["VehicleModel"];
            vehicleModel.ActionMode = 1;
            Session["VehicleModel"] = vehicleModel;
            this.SetControlState((int)UserAction.ADD);


            Login.Login cp = HttpContext.Current.User as Login.Login;
            int userID = 0;

            try
            {
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Could not parse user id from cp.Identity.Name.", ex);
            }

            try
            {
                switch (vehicleModel.ActionMode)
                {
                    case 1: //ADD
                        this.FillProperties(ref vehicleModel);
                        vehicleModel.Save(userID);
                        break;
                    case 3: //DELETE
                        break;
                    case 4: //CLEAR
                        break;
                    default: //UPDATE
                        this.FillProperties(ref vehicleModel);
                        vehicleModel.Save(userID);
                        Session["VehicleModel"] = vehicleModel;
                        this.SetControlState((int)UserAction.EDIT);
                        break;
                }
                this.litPopUp.Text =
                    Utilities.MakeLiteralPopUpString(
                    "Vehicle model information saved successfully.");
                this.litPopUp.Visible = true;
                this.SetVehicleModelNumerationLabel();
                this.SetControlState((int)UserAction.SAVE);

                Session["VehicleModel"] = vehicleModel;
                FillModelDDList();
            }
            catch (Exception xcp)
            {
                this.ShowMessage("Unable to save or modify vehicle model. " + xcp.Message);
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

			LookupTables.VehicleModel vehicleModel = 
				(LookupTables.VehicleModel)Session["VehicleModel"];
			try
			{
				switch(vehicleModel.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref vehicleModel);
						vehicleModel.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref vehicleModel);
						vehicleModel.Save(userID);
						Session["VehicleModel"] = vehicleModel;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"Vehicle model information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetVehicleModelNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["VehicleModel"] = vehicleModel;
				FillModelDDList();
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify vehicle model. " + xcp.Message);
			}
		}

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(
				"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
				LookupTables.LookupTables.GetLookupTableIdFromTableName(
				"VehicleModel").ToString());
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.SetControlState((int)UserAction.CANCEL);
			//this.RecordNavigation("");
		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			EPolicy2.Reports.AdministrativeTools atreport = new EPolicy2.Reports.AdministrativeTools();
			DataTable dt = atreport.VehicleModelList();
	
			DataDynamics.ActiveReports.ActiveReport3 rpt = new  GeneralList("Vehicle Model");
			
			rpt.DataSource = dt;
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage",LookupTables.LookupTables.GetTableMaintenancePathFromTableID(22)+ "?item=" + this.lblVehicleModelID.Text);
			Response.Redirect("ActiveXViewer.aspx",false);
		}

		protected void btnAuditTrail_Click(object sender, System.EventArgs e)
		{
			if(Session["VehicleModel"] != null)
			{
				LookupTables.VehicleModel vehicleModel = (LookupTables.VehicleModel) Session["VehicleModel"];
				Response.Redirect("SearchAuditItems.aspx?type=12&vehicleModelID=" + 
					vehicleModel.VehicleModelID.ToString());
			}
		}

		protected void cmdCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.SetControlState((int)UserAction.CANCEL);
			//this.RecordNavigation("");
		}

		protected void btnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.VehicleModel vehicleModel =
				(LookupTables.VehicleModel)Session["VehicleModel"];
			 vehicleModel.ActionMode = 2;
			Session["VehicleModel"] = vehicleModel;
			this.SetControlState((int)UserAction.EDIT);
		}

		protected void BtnBegin_Click(object sender, System.EventArgs e)
		{
			if(!this.IsNavigationTableNull())
			{
				//this.RecordNavigation("Begin");
			}
		}

		protected void BtnPrevious_Click(object sender, System.EventArgs e)
		{
			if(!this.IsNavigationTableNull())
			{
				//this.RecordNavigation("Previous");
			}
		}

		protected void BtnNext_Click(object sender, System.EventArgs e)
		{
			if(!this.IsNavigationTableNull())
			{
				//this.RecordNavigation("Next");
			}
		}

		protected void BtnEnd_Click(object sender, System.EventArgs e)
		{
			if(!this.IsNavigationTableNull())
			{
				//this.RecordNavigation("End");
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

		protected void btnAddNew_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("VehicleModel.aspx");
		}

		protected void btnSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(
				"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
				LookupTables.LookupTables.GetLookupTableIdFromTableName(
				"VehicleModel").ToString());
		}

		protected void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			EPolicy2.Reports.AdministrativeTools atreport = new EPolicy2.Reports.AdministrativeTools();
			DataTable dt = atreport.VehicleModelList();
	
			DataDynamics.ActiveReports.ActiveReport3 rpt = new  GeneralList("Vehicle Model");
			
			rpt.DataSource = dt;
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage",LookupTables.LookupTables.GetTableMaintenancePathFromTableID(22)+ "?item=" + this.lblVehicleModelID.Text);
			Response.Redirect("ActiveXViewer.aspx",false);
		}

		protected void ddlVehicleMake_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillModelDDList();
		}

		private void FillModelDDList()
		{
			if(ddlVehicleMake.Items[ddlVehicleMake.SelectedIndex].Text !="")
			{
				int makeID = Int32.Parse(ddlVehicleMake.SelectedItem.Value.ToString());
				DataTable dtModel  = LookupTables.LookupTables.GetTable("VehicleModel"); 
				DataTable dt = dtModel.Clone();
				DataRow[] DrModel = dtModel.Select("VehicleMakeID = "+makeID,"VehicleModelDesc");

				for (int i=0;i<=DrModel.Length-1;i++)
				{
					DataRow myRow = dt.NewRow();
					myRow["VehicleModelID"] = (int) DrModel[i].ItemArray[0];
					myRow["VehicleMakeID"] = (int) DrModel[i].ItemArray[1];
					myRow["VehicleModelDesc"] = DrModel[i].ItemArray[2].ToString();

					dt.Rows.Add(myRow);
					dt.AcceptChanges();
				}

				ddlModel.Items.Clear();

				ddlModel.DataSource = dt; 
				ddlModel.DataTextField = "VehicleModelDesc";
				ddlModel.DataValueField = "VehicleModelID";
				ddlModel.DataBind();
//				ddlModel.SelectedIndex = -1;
//				ddlModel.Items.Insert(0,"");
			}
			else
			{
				DataTable dtModel  = LookupTables.LookupTables.GetTable("VehicleModel"); 

				ddlModel.DataSource = dtModel; 
				ddlModel.DataTextField = "VehicleModelDesc";
				ddlModel.DataValueField = "VehicleModelID";
				ddlModel.DataBind();
//				ddlModel.SelectedIndex = -1;
//				ddlModel.Items.Insert(0,"");
			}
		}
	
		protected void btnAuditTrail_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if(Session["VehicleModel"] != null)
			{
				LookupTables.VehicleModel vehicleModel = (LookupTables.VehicleModel) Session["VehicleModel"];
				Response.Redirect("SearchAuditItems.aspx?type=12&vehicleModelID=" + 
					vehicleModel.VehicleModelID.ToString());
			}
		}

		protected void BtnExit_Click(object sender, System.EventArgs e)
		{
			LookupTables.VehicleModel vehicleModel = 
				(LookupTables.VehicleModel)Session["VehicleModel"];
			
			if(vehicleModel.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				Response.Redirect(
					"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
					LookupTables.LookupTables.GetLookupTableIdFromTableName(
					"VehicleModel").ToString());
			}
		}
        protected void chkIsMotorcycle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsMotorcycle.Checked)
            {
                DataTable dtMake = LookupTables.LookupTables.GetTable("VehicleMakeMotorcycle");


                //Make
                ddlVehicleMake.DataSource = dtMake;
                ddlVehicleMake.DataTextField = "VehicleMakeDesc";
                ddlVehicleMake.DataValueField = "VehicleMakeID";
                ddlVehicleMake.DataBind();

                FillModelDDList();
            }
            else
            {
                DataTable dtMake = LookupTables.LookupTables.GetTable("VehicleMake");
                //Make
                ddlVehicleMake.DataSource = dtMake;
                ddlVehicleMake.DataTextField = "VehicleMakeDesc";
                ddlVehicleMake.DataValueField = "VehicleMakeID";
                ddlVehicleMake.DataBind();

                FillModelDDList();
            }

        }
}
}
