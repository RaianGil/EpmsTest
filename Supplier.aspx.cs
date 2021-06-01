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
	/// Summary description for Supplier.
	/// </summary>
	public partial class Supplier : System.Web.UI.Page
	{
		private Control LeftMenu;
        private DataTable DtSupplier;


		#region Enumerations

		public enum UserAction{ADD = 1, VIEW = 2, SAVE = 3, EDIT = 4, CANCEL = 5};
		
		#endregion

		private void SetSupplierNumerationLabel()
		{
			LookupTables.Supplier supplier = 
				(LookupTables.Supplier)Session["Supplier"];

			this.lblSupplierID.Text = (supplier.SupplierID != "") ?
				supplier.SupplierID.ToString():
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
				if(!cp.IsInRole("SUPPLIER") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("HomePage.aspx?001");
				}
			}

			if(!Page.IsPostBack)
			{
				LookupTables.Supplier supplier = new LookupTables.Supplier();

				if(Request.QueryString["item"] != null &&																	
					Request.QueryString["item"].ToString() != String.Empty)
				{	
					try
					{
						supplier.GetSupplier(Request.QueryString["item"].ToString());
						supplier.ActionMode = 2; //UPDATE
						supplier.NavigationViewModelTable = 
							(DataTable)Session["DtRecordsForNonValuePairLookupTable"];
						Session["Supplier"] = supplier;
					}
					catch(Exception ex)
					{
						this.ShowMessage("There is no Supplier for the supplied " +
							"ID. " + ex.Message);
					}
				}
				else
				{
					supplier.ActionMode = 1; //ADD
					Session["Supplier"] = supplier;
				}
				
			}

			if(Session["Supplier"] != null)
			{
				LookupTables.Supplier supplier = 
					(LookupTables.Supplier)Session["Supplier"];

				this.InitializeControls();
			
				switch(supplier.ActionMode)
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

							if (supplier.NavigationViewModelTable != null)
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
			//((Baldrich.Components.MenuTaskControl)LeftMenu).Height = "534px";
			phTopBanner1.Controls.Add(LeftMenu);

            DataTable dtSupplier = LookupTables.LookupTables.GetTable("Supplier");

            //Dealer
            ddlSupplier2.DataSource = dtSupplier;
            ddlSupplier2.DataTextField = "SupplierDesc";
            ddlSupplier2.DataValueField = "SupplierID";
            ddlSupplier2.DataBind();
            ddlSupplier2.SelectedIndex = -1;
            ddlSupplier2.Items.Insert(0, "");

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
			this.SetSupplierNumerationLabel();
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
					this.btnIncentive.Visible = false;
					this.txtEntryDate.Enabled = false;
					this.btnAuditTrail.Visible = false;
					this.btnPrint.Visible = false;
					this.btnSearch.Visible = false;
					this.BtnExit.Visible = false;
                    this.btnAdd2.Visible = false;
                    this.lblAddSupplier.Visible = false;
                    this.ddlSupplier2.Visible = false;
					break;
				case 2: //VIEW ACTION
					this.EnableRecordNavigationControl(true);
					this.EnableInputControls(false);
					this.btnEdit.Visible = true;
					this.BtnSave.Visible = false;
					this.cmdCancel.Visible = false;
					this.btnAddNew.Visible = true;
					this.btnIncentive.Visible = true;
					this.btnAuditTrail.Visible = true;
					this.btnPrint.Visible = true;
					this.btnSearch.Visible = true;
					this.BtnExit.Visible = true;
                    this.btnAdd2.Visible = true;
                    this.lblAddSupplier.Visible = true;
                    this.ddlSupplier2.Visible = true;
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
					this.btnIncentive.Visible = false;
					this.txtEntryDate.Enabled = false;
					this.btnAuditTrail.Visible = false;
					this.btnPrint.Visible = false;
					this.btnSearch.Visible = false;
					this.BtnExit.Visible = false;
                    this.btnAdd2.Visible = false;
                    this.lblAddSupplier.Visible = false;
                    this.ddlSupplier2.Visible = false;
					break;
				default: //CANCEL ACTION
					LookupTables.Supplier supplier = (LookupTables.Supplier)Session["Supplier"];
					if(supplier.ActionMode == 0) //ADD
					{
						Session["Supplier"] = null;
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
			if(Session["Supplier"] != null)
			{
				LookupTables.Supplier supplier = 
					(LookupTables.Supplier)Session["Supplier"];
				string supplierID = supplier.SupplierID;

				if(supplier.NavigationViewModelTable != null)
				{
					int rec = 0;

					for(int i=0; 
						i<= (supplier.NavigationViewModelTable.Rows.Count - 1); i++)
					{
						if (supplierID == 
							supplier.NavigationViewModelTable.Rows[i]["SupplierID"].ToString())
						{
							if (Case == "")
							{
								rec =  i + 1;
								lblRecordCount.Text = 
									rec.ToString() + " of " +
									supplier.NavigationViewModelTable.Rows.Count;
							}
							else if (Case == "Previous" || Case == "Begin")
							{
								rec = 1;
								if (i > 0)
								{
									switch(Case)
									{
										case "Previous":
											supplierID = 
												supplier.NavigationViewModelTable.Rows[i - 1]["SupplierID"].ToString();
											rec = i;
											break;
										case "Begin":
											supplierID
												= 
												supplier.NavigationViewModelTable.Rows[0]["SupplierID"].ToString();
											rec = 1;
											break;
									}
									lblRecordCount.Text = 
										rec.ToString() + " of " + 
										supplier.NavigationViewModelTable.Rows.Count;
									i = 
										supplier.NavigationViewModelTable.Rows.Count-1; 
									// Para salir del loop.
								}
							}
							else if (Case == "Next" || Case == "End")
							{
								if (supplier.NavigationViewModelTable.Rows.Count-1 > i)
								{					
									switch(Case)
									{
										case "Next":
											supplierID = 
												
												supplier.NavigationViewModelTable.Rows[i + 1]["SupplierID"].ToString();
											rec = i + 2;
											break;
										case "End":
											supplierID =
												supplier.NavigationViewModelTable.Rows[supplier.NavigationViewModelTable.Rows.Count - 1]["SupplierID"].ToString();
											rec = supplier.NavigationViewModelTable.Rows.Count;
											break;
									}
									lblRecordCount.Text = 
										rec.ToString() + " of " + 
										supplier.NavigationViewModelTable.Rows.Count;
									i = supplier.NavigationViewModelTable.Rows.Count-1; 
									// Para salir del loop.
								}
							}
						}
					}																
				}
				DataTable dtSupplier = supplier.NavigationViewModelTable;
				
				try
				{
					supplier.GetSupplier(supplierID);
			
					supplier.NavigationViewModelTable = dtSupplier;

					Session.Add("Supplier", supplier);
					this.FillControls();
					this.SetControlState((int)UserAction.VIEW);
					this.SetSupplierNumerationLabel();
				}
				catch(Exception ex)
				{
					this.ShowMessage("There is no Supplier for the supplied " +
						"ID. " + ex.Message);
				}
			}
		}// End RecordNavigation

		private void EnableInputControls(bool State)
		{
			if(State)
			{
				this.txtSupplierDescription.Enabled = true;
				this.txtAddress1.Enabled = true;
				this.txtAddress2.Enabled = true;
				this.txtCity.Enabled = true;
				this.txtSt.Enabled = true;
				this.txtZipCode.Enabled = true;
				this.txtPhone.Enabled = true;
				this.txtEntryDate.Enabled = true;
                this.btnAdd2.Visible = true;
			}
			else
			{
				this.txtSupplierDescription.Enabled = false;
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
			LookupTables.Supplier supplier =
				(LookupTables.Supplier)Session["Supplier"];
			
			
			this.txtSupplierDescription.Text = (supplier.SupplierDesc != "" ?
				supplier.SupplierDesc.ToString():
				String.Empty);
			
			this.txtAddress1.Text = 
				supplier.Supplier_addr1.Trim();

			this.txtAddress2.Text = 
				supplier.Supplier_addr2.Trim();

			this.txtCity.Text =
				supplier.Supplier_city.Trim();

			this.txtSt.Text = 
				supplier.Supplier_st.Trim();

			this.txtZipCode.Text = 
				supplier.Supplier_zip.Trim();

			this.txtPhone.Text = 
				supplier.Supplier_phone.Trim();

			this.txtEntryDate.Text = 
				supplier.Supplier_actdt.ToString().Trim();

            FillDataGridSupplier();
         }

		private void ShowMessage(string MessageText)
		{
			this.litPopUp.Text = 
				Utilities.MakeLiteralPopUpString(MessageText);
			this.litPopUp.Visible = true;
		}

		private void FillProperties(ref LookupTables.Supplier supplier)
		{	
			try
			{
				supplier.SupplierDesc = (this.txtSupplierDescription.Text.ToString().ToUpper());
			}
			catch
			{
				this.ShowMessage("Could not fill 'SupplierID' property. " +
					"Please enter a valid value for this field.");
			}
			supplier.Supplier_addr1 = this.txtAddress1.Text.ToString().ToUpper();
			supplier.Supplier_addr2 = this.txtAddress2.Text.ToString().ToUpper();
			supplier.Supplier_city  = this.txtCity.Text.ToString().ToUpper();
			supplier.Supplier_st    = this.txtSt.Text.ToString().ToUpper();
			supplier.Supplier_zip   = this.txtZipCode.Text;
			supplier.Supplier_phone = this.txtPhone.Text;
			supplier.Supplier_actdt = this.txtEntryDate.Text;

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
			LookupTables.Supplier supplier = 
				(LookupTables.Supplier)Session["supplier"];

			if(supplier.NavigationViewModelTable == null)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		protected void BtnExit_Click(object sender, System.EventArgs e)
		{
			LookupTables.Supplier supplier = 
				(LookupTables.Supplier)Session["Supplier"];
			
			if(supplier.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				Response.Redirect(
					"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
					LookupTables.LookupTables.GetLookupTableIdFromTableName(
					"Supplier").ToString());
			}
		}

		protected void btnAuditTrail_Click(object sender, System.EventArgs e)
		{
			if(Session["Supplier"] != null)
			{
				LookupTables.Supplier supplier = (LookupTables.Supplier) Session["Supplier"];
				Response.Redirect("SearchAuditItems.aspx?type=18&supplierID=" + 
					supplier.SupplierID.Trim());
			}
		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			EPolicy2.Reports.AdministrativeTools atreport = new EPolicy2.Reports.AdministrativeTools();
			DataTable dt = atreport.SupplierList();
	
			DataDynamics.ActiveReports.ActiveReport3 rpt = new  GeneralList("Supplier");
	
			rpt.DataSource = dt;
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage",LookupTables.LookupTables.GetTableMaintenancePathFromTableID(75)+ "?item=" + this.lblSupplierID.Text);
			Response.Redirect("ActiveXViewer.aspx",false);
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			LookupTables.Supplier supplier = (LookupTables.Supplier)Session["Supplier"];
			
			if(supplier.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				this.SetControlState((int)UserAction.CANCEL);
				this.RecordNavigation("");
			}
		}

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(
				"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
				LookupTables.LookupTables.GetLookupTableIdFromTableName(
				"Supplier").ToString());
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

			LookupTables.Supplier supplier = 
				(LookupTables.Supplier)Session["Supplier"];
			try
			{
				switch(supplier.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref supplier);
						supplier.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref supplier);
						supplier.Save(userID);
						Session["Supplier"] = supplier;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Supplier information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetSupplierNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["Supplier"] = supplier;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Supplier. " + xcp.Message);
			}		
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			LookupTables.Supplier supplier =
				(LookupTables.Supplier)Session["Supplier"];
			supplier.ActionMode = 2;
			Session["Supplier"] = supplier;
			this.SetControlState((int)UserAction.EDIT);		
		}

		protected void btnAddNew_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Supplier.aspx");		
		}

		protected void btnIncentive_Click(object sender, System.EventArgs e)
		{
			LookupTables.Supplier supplier = (LookupTables.Supplier) Session["Supplier"];
			Response.Redirect("IncentiveSupplier.aspx?" +supplier.SupplierID);
		}
        protected void btnAdd2_Click(object sender, EventArgs e)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
			int userID = 0;
			userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
           
            LookupTables.Supplier supplier = (LookupTables.Supplier)Session["Supplier"];

            try
            {
                if (ddlSupplier2.SelectedIndex > 0 && ddlSupplier2.SelectedItem != null)
                {
                    supplier.SaveSupplier(supplier.SupplierID, ddlSupplier2.SelectedItem.Value.ToString());
                    FillControls();
                    //DisableControls();

                    this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Supplier saved successfully.");
                    this.litPopUp.Visible = true;
                }
                else
                {
                    this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Must choose one supplier.");
                    this.litPopUp.Visible = true;
                }

            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Unable to save this supplier. " + exp.Message);
                this.litPopUp.Visible = true;
            }
        }
        protected void dgSupplier2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    try
                    {
                        LookupTables.Supplier supplier = (LookupTables.Supplier)Session["Supplier"];

                        supplier.DeleteGroupSupplierByGroupSupplierID(int.Parse(e.Item.Cells[3].Text));

                        FillControls();

                        this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Supplier " + e.Item.Cells[2].Text + " has been deleted.");
                        this.litPopUp.Visible = true;
                    }
                    catch (Exception exp)
                    {
                        this.litPopUp.Text = Utilities.MakeLiteralPopUpString(exp.Message);
                        this.litPopUp.Visible = true;

                    }
                    break;

                default: //Page
                    dgSupplier2.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;

                    dgSupplier2.DataSource = (DataTable)Session["DtSupplier"];
                    dgSupplier2.DataBind();
                    break;
            }
        }

        private void FillDataGridSupplier()
        {
            LookupTables.Supplier supplier = (LookupTables.Supplier)Session["Supplier"];

            dgSupplier2.DataSource = null;
            DtSupplier = null;

            DtSupplier = LookupTables.Supplier.GetGroupSupplierBySupplierID(supplier.SupplierID);

            Session.Remove("DtSupplier");
            Session.Add("DtSupplier", DtSupplier);

            if (DtSupplier != null)
            {
                if (DtSupplier.Rows.Count != 0)
                {
                    dgSupplier2.DataSource = DtSupplier;
                    dgSupplier2.DataBind();
                }
                else
                {
                    dgSupplier2.DataSource = null;
                    dgSupplier2.DataBind();
                }
            }
            else
            {
                dgSupplier2.DataSource = null;
                dgSupplier2.DataBind();
            }
        }

}
}
