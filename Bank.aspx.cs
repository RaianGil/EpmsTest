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
	/// Summary description for Bank.
	/// </summary>
	public partial class Bank : System.Web.UI.Page
	{
		private Control LeftMenu;


		#region Enumerations

		public enum UserAction{ADD = 1, VIEW = 2, SAVE = 3, EDIT = 4, CANCEL = 5};
		
		#endregion


		private void SetBankNumerationLabel()
		{
			LookupTables.Bank bank = 
				(LookupTables.Bank)Session["Bank"];

			this.lblBankID.Text = (bank.BankID != "") ?
				bank.BankID.ToString():
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
				if(!cp.IsInRole("BANK") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("HomePage.aspx?001");
				}
			}

			if(!Page.IsPostBack)
			{
				LookupTables.Bank bank = new LookupTables.Bank();

				if(Request.QueryString["item"] != null &&																	
					Request.QueryString["item"].ToString() != String.Empty)
				{	
					try
					{
                        if (Session["OTHERTABLE"].ToString() == "Bank_VI")
                        {
                            bank.GetBank_VI(Request.QueryString["item"].ToString());
                        }
                        else
                        {
                            bank.GetBank(Request.QueryString["item"].ToString());
                        }

						
						bank.ActionMode = 2; //UPDATE
						bank.NavigationViewModelTable = 
							(DataTable)Session["DtRecordsForNonValuePairLookupTable"];
						Session["Bank"] = bank;
					}
					catch(Exception ex)
					{
						this.ShowMessage("There is no bank for the supplied " +
							"ID. " + ex.Message);
					}
				}
				else
				{
					bank.ActionMode = 1; //ADD
					Session["Bank"] = bank;
				}
				
			}

			if(Session["Bank"] != null)
			{
				LookupTables.Bank bank = 
					(LookupTables.Bank)Session["Bank"];

				this.InitializeControls();
			
				switch(bank.ActionMode)
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

							if (bank.NavigationViewModelTable != null)
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
			this.SetBankNumerationLabel();
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
					this.txtEntryDate.Enabled = false;
					//this.btnAuditTrail.Visible = false;
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
					this.txtEntryDate.Enabled = false;
					//this.btnAuditTrail.Visible = false;
					this.btnSearch.Visible = false;
					this.btnPrint.Visible = false;
					this.BtnExit.Visible = false;
					break;
				default: //CANCEL ACTION
					LookupTables.Bank bank = 
						(LookupTables.Bank)Session["Bank"];
					if(bank.ActionMode == 0) //ADD
					{
						Session["Bank"] = null;
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
			if(Session["Bank"] != null)
			{
				LookupTables.Bank bank = 
					(LookupTables.Bank)Session["Bank"];
				string bankID = bank.BankID;

				if(bank.NavigationViewModelTable != null)
				{
					int rec = 0;

					for(int i=0; 
						i<= (bank.NavigationViewModelTable.Rows.Count - 1); i++)
					{
						if (bankID == 
							bank.NavigationViewModelTable.Rows[i]["BankID"].ToString())
						{
							if (Case == "")
							{
								rec = i + 1;
								lblRecordCount.Text = 
									rec.ToString() + " of " +
									bank.NavigationViewModelTable.Rows.Count;
							}
							else if (Case == "Previous" || Case == "Begin")
							{
								rec = 1;
								if (i > 0)
								{
									switch(Case)
									{
										case "Previous":
											bankID = 
												bank.NavigationViewModelTable.Rows[i - 1]["BankID"].ToString();
											rec = i;
											break;
										case "Begin":
											bankID
												= 
												bank.NavigationViewModelTable.Rows[0]["BankID"].ToString();
											rec = 1;
											break;
									}
									lblRecordCount.Text = 
										rec.ToString() + " of " + 
										bank.NavigationViewModelTable.Rows.Count;
									i = 
										bank.NavigationViewModelTable.Rows.Count-1; 
									// Para salir del loop.
								}
							}
							else if (Case == "Next" || Case == "End")
							{
								if (bank.NavigationViewModelTable.Rows.Count-1 > i)
								{					
									switch(Case)
									{
										case "Next":
											bankID = 
												
												bank.NavigationViewModelTable.Rows[i + 1]["BankID"].ToString();
											rec = i + 2;
											break;
										case "End":
											bankID =
												bank.NavigationViewModelTable.Rows[bank.NavigationViewModelTable.Rows.Count - 1]["BankID"].ToString();
											rec = bank.NavigationViewModelTable.Rows.Count;
											break;
									}
									lblRecordCount.Text = 
										rec.ToString() + " of " + 
										bank.NavigationViewModelTable.Rows.Count;
									i = bank.NavigationViewModelTable.Rows.Count-1; 
									// Para salir del loop.
								}
							}
						}
					}																
				}
				DataTable dtBank = bank.NavigationViewModelTable;
				
				try
				{

                    if (bank.NavigationViewModelTable.TableName == "Bank_VI")
                    {
                        bank.GetBank_VI(bankID);
                    }
                    else
                    {
                        bank.GetBank(bankID);
                    }

					bank.NavigationViewModelTable = dtBank;

					Session.Add("Bank", bank);
					this.FillControls();
					this.SetControlState((int)UserAction.VIEW);
					this.SetBankNumerationLabel();
				}
				catch(Exception ex)
				{
					this.ShowMessage("There is no Bank for the supplied " +
						"ID. " + ex.Message);
				}
			}
		}// End RecordNavigation

		private void EnableInputControls(bool State)
		{
			if(State)
			{
				this.txtBankDescription.Enabled = true;
				this.txtAddress1.Enabled        = true;
				this.txtAddress2.Enabled        = true;
				this.txtCity.Enabled            = true;
				this.txtSt.Enabled              = true;
				this.txtZipCode.Enabled         = true;
				this.txtPhone.Enabled           = true;
				this.txtEntryDate.Enabled       = true;
				this.txtUniversal.Enabled       = true;
				this.chkLease.Enabled           = true;
                this.chkNetCollection.Enabled = true;
                this.txtVSCBankFee.Enabled = true;
                this.txtRoutingNumber.Enabled = true;
                


            }
			else
			{
				this.txtBankDescription.Enabled = false;
				this.txtAddress1.Enabled        = false;
				this.txtAddress2.Enabled        = false;
				this.txtCity.Enabled            = false;
				this.txtSt.Enabled              = false;
				this.txtZipCode.Enabled         = false;
				this.txtPhone.Enabled           = false;
				this.txtEntryDate.Enabled       = false;
				this.txtUniversal.Enabled       = false;
				this.chkLease.Enabled           = false;
                this.chkNetCollection.Enabled = false;
                this.txtVSCBankFee.Enabled = false;
                this.txtRoutingNumber.Enabled = false;
            }

            if (Session["OTHERTABLE"].ToString() == "Bank_VI")
            {
                lblRoutingNumber.Visible = false;
                txtRoutingNumber.Visible = false;
            }
		}

		private void FillControls()
		{	
			LookupTables.Bank bank =
				(LookupTables.Bank)Session["Bank"];
			
			

			this.txtBankDescription.Text = (bank.BankDesc != "" ?
				bank.BankDesc.ToString():
				String.Empty);
			
			this.txtAddress1.Text = 
				bank.Address1.Trim();

			this.txtAddress2.Text = 
				bank.Address2.Trim();

			this.txtCity.Text =
				bank.City.Trim();

			this.txtSt.Text = 
				bank.State.Trim();

			this.txtZipCode.Text = 
				bank.ZipCode.Trim();

			this.txtPhone.Text = 
				bank.Phone.Trim();

			this.txtEntryDate.Text = 
				bank.Bank_actdt.Trim();

			this.txtUniversal.Text = 
				bank.Unico.Trim();
			
			this.chkLease.Checked = 
				bank.Lease;

            this.chkNetCollection.Checked =
                bank.NetCollection;

            this.txtVSCBankFee.Text =
                bank.VSCBankFee.ToString("###,##.00");

            this.txtRoutingNumber.Text =
                bank.RoutingNumber.Trim();


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

				LookupTables.Bank bank = 
				(LookupTables.Bank)Session["Bank"];

			try
			{
				switch(bank.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref bank);
						bank.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref bank);
						bank.Save(userID);
						Session["Bank"] = bank;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Bank information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetBankNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["Bank"] = bank;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Bank. " + xcp.Message);
			}
		}//End BtnSave_Click



		private void ShowMessage(string MessageText)
		{
			this.litPopUp.Text = 
				Utilities.MakeLiteralPopUpString(MessageText);
			this.litPopUp.Visible = true;
		}

		private void FillProperties(ref LookupTables.Bank bank)
		{	
			try
			{
				bank.BankDesc = (this.txtBankDescription.Text.ToString().ToUpper());
			}
			catch
			{
				this.ShowMessage("Could not fill 'BankID' property. " +
					"Please enter a valid value for this field.");
			}
			bank.Address1   = this.txtAddress1.Text.ToString().ToUpper();
			bank.Address2   = this.txtAddress2.Text.ToString().ToUpper();
			bank.City       = this.txtCity.Text.ToString().ToUpper();
			bank.State      = this.txtSt.Text.ToString().ToUpper();
			bank.ZipCode    = this.txtZipCode.Text;
			bank.Phone      = this.txtPhone.Text;
			bank.Bank_actdt = this.txtEntryDate.Text;
			bank.Unico      = this.txtUniversal.Text;
			bank.Lease      = this.chkLease.Checked;
            bank.NetCollection = this.chkNetCollection.Checked;
            bank.VSCBankFee = double.Parse(this.txtVSCBankFee.Text);
            bank.RoutingNumber = this.txtRoutingNumber.Text;


            bank.BankType = Session["OTHERTABLE"].ToString();

		}

	  
			
		protected void BtnExit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.Bank bank = 
				(LookupTables.Bank)Session["Bank"];
			
			if(bank.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				Response.Redirect(
					"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
					LookupTables.LookupTables.GetLookupTableIdFromTableName(
					"Bank").ToString());
			}
		}

		protected void btnAddNew_Click(object sender, System.EventArgs e)
		{
			Response.Redirect("Bank.aspx");
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			LookupTables.Bank bank =
				(LookupTables.Bank)Session["Bank"];			
			bank.ActionMode = 2;
			Session["Bank"] = bank;
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

			LookupTables.Bank bank = 
				(LookupTables.Bank)Session["Bank"];

			try
			{
				switch(bank.ActionMode)
				{
					case 1: //ADD
						this.FillProperties(ref bank);
						bank.Save(userID);
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref bank);
						bank.Save(userID);
						Session["Bank"] = bank;
						this.SetControlState((int)UserAction.VIEW);
						break;
				}
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString(
					"The Bank information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetBankNumerationLabel();
				this.SetControlState((int)UserAction.SAVE);

				Session["Bank"] = bank;
			}
			catch(Exception xcp)
			{
				this.ShowMessage("Unable to save or modify Bank. " + xcp.Message);
			}
		}

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{

            if (Session["OTHERTABLE"].ToString() == "Bank_VI")
            {
                Response.Redirect(
                "SearchLookupTableDescriptions.aspx?SelectedItem=" +
                LookupTables.LookupTables.GetLookupTableIdFromTableName(
                "Bank_VI").ToString());
            }
            else
            {
                Response.Redirect(
                    "SearchLookupTableDescriptions.aspx?SelectedItem=" +
                    LookupTables.LookupTables.GetLookupTableIdFromTableName(
                    "Bank").ToString());
            }
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
//			this.SetControlState((int)UserAction.CANCEL);
//			this.RecordNavigation("");

			LookupTables.Bank bank = (LookupTables.Bank)Session["Bank"];
			
			if(bank.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{
				this.SetControlState((int)UserAction.CANCEL);
			}
		}

		protected void btnPrint_Click(object sender, System.EventArgs e)
		{
			EPolicy2.Reports.AdministrativeTools atreport = new EPolicy2.Reports.AdministrativeTools();
			DataTable dt = atreport.BankList();
	
			DataDynamics.ActiveReports.ActiveReport3 rpt = new  GeneralList("Bank");
			
			//rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

			rpt.DataSource = dt;
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage",LookupTables.LookupTables.GetTableMaintenancePathFromTableID(27)+ "?item=" + this.lblBankID.Text);
			Response.Redirect("ActiveXViewer.aspx",false);
		}

		protected void btnAuditTrail_Click(object sender, System.EventArgs e)
		{
			if(Session["Bank"] != null)
			{
				LookupTables.Bank bank = (LookupTables.Bank) Session["Bank"];
				Response.Redirect("SearchAuditItems.aspx?type=6&bankID=" + 
					bank.BankID.Trim());
			}
		}


		protected void cmdCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.SetControlState((int)UserAction.CANCEL);
			this.RecordNavigation("");
		}

		protected void btnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			LookupTables.Bank bank =
				(LookupTables.Bank)Session["Bank"];			
			bank.ActionMode = 2;
			Session["Bank"] = bank;
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
			LookupTables.Bank bank = 
				(LookupTables.Bank)Session["bank"];
			if(bank.NavigationViewModelTable == null)
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
			Response.Redirect("Bank.aspx");
		}

		protected void btnSearch_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(
				"SearchLookupTableDescriptions.aspx?SelectedItem=" + 
				LookupTables.LookupTables.GetLookupTableIdFromTableName(
				"Bank").ToString());
		}

		protected void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			EPolicy2.Reports.AdministrativeTools atreport = new EPolicy2.Reports.AdministrativeTools();
			DataTable dt = atreport.BankList();
	
			DataDynamics.ActiveReports.ActiveReport3 rpt = new  GeneralList("Bank");
			
			//rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

			rpt.DataSource = dt;
			rpt.DataMember = "Report";
			rpt.Run(false);

			Session.Add("Report",rpt);
			Session.Add("FromPage",LookupTables.LookupTables.GetTableMaintenancePathFromTableID(27)+ "?item=" + this.lblBankID.Text);
			Response.Redirect("ActiveXViewer.aspx",false);

		}

		protected void btnAuditTrail_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if(Session["Bank"] != null)
			{
				LookupTables.Bank bank = (LookupTables.Bank) Session["Bank"];
				Response.Redirect("SearchAuditItems.aspx?type=6&bankID=" + 
					bank.BankID.Trim());
			}
		}

		protected void BtnExit_Click(object sender, System.EventArgs e)
		{
			LookupTables.Bank bank = 
				(LookupTables.Bank)Session["Bank"];
			
			if(bank.ActionMode == 1) //ADD
				Response.Redirect("LookupTableMaintenance.aspx");
			else
			{

                if (Session["OTHERTABLE"].ToString() == "Bank_VI")
                {
                    Response.Redirect(
                    "SearchLookupTableDescriptions.aspx?SelectedItem=" +
                    LookupTables.LookupTables.GetLookupTableIdFromTableName(
                    "Bank_VI").ToString());
                }
                else
                {
                    Response.Redirect(
                        "SearchLookupTableDescriptions.aspx?SelectedItem=" +
                        LookupTables.LookupTables.GetLookupTableIdFromTableName(
                        "Bank").ToString());
                }
			}
		}

        protected void txtRoutingNumber_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
