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

namespace EPolicy
{
	/// <summary>
	/// Summary description for ProspectBusiness.
	/// </summary>
	public partial class ProspectBusiness : System.Web.UI.Page
	{
		private DataTable DtTask;

		#region Enumerations

		public enum UserAction{ADD = 1, VIEW = 2, SAVE = 3, EDIT = 4, CANCEL = 5};
		
		#endregion

		private void SetProspectNumerationLabels()
		{
			Customer.Prospect prospect = (Customer.Prospect)Session["Prospect"];

			this.lblProspectNo.Text = prospect.ProspectID.ToString();

			if(prospect.CustomerNumber != "")
			{
				this.lblParentCustomer.Text = prospect.CustomerNumber.Trim();
			}
			else
			{
				this.lblParentCustomer.Text = "None";
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
				Response.Redirect("Default.aspx?001");
			}
			else
			{
				if(!cp.IsInRole("PROSPECTBUSINESS") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("Default.aspx?001");
				}
			}

			if(Session["Prospect"] != null)
			{
				Customer.Prospect prospect = (Customer.Prospect)Session["Prospect"];

				this.InitializeControls();
			
				switch(prospect.Mode)
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
							
							if (prospect.NavigationProspectTable != null)
							{
								this.NavigationProspects("");
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
			Control LeftMenu = new Control();
			LeftMenu = LoadControl(@"LeftMenu.ascx");
			this.phTopBanner1.Controls.Add(LeftMenu);

			//Setup Referred By ddl
			DataTable dtReferredBy	=
				LookupTables.LookupTables.GetTable("ReferredBy");
			this.ddlReferredBy.DataSource = dtReferredBy;
			this.ddlReferredBy.DataTextField = "referredByDesc";
			this.ddlReferredBy.DataValueField = "referredByID";
			this.ddlReferredBy.DataBind();
			this.ddlReferredBy.SelectedIndex = -1;
			this.ddlReferredBy.Items.Insert(0,"");

			//Setup Originated At ddl
			DataTable dtLocation =
				LookupTables.LookupTables.GetTable("Location");
			this.ddlLocation.DataSource = dtLocation;
			this.ddlLocation.DataTextField = "locationDesc";
			this.ddlLocation.DataValueField = "locationID";
			this.ddlLocation.DataBind();
			this.ddlLocation.SelectedIndex = -1;
			this.ddlLocation.Items.Insert(0,"");
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.searchIndividual.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.searchIndividual_ItemCommand);
			this.searchIndividual.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.searchIndividual_ItemDataBound);

		}
		#endregion

		private void InitializeControls()
		{
			this.SetProspectNumerationLabels();
			this.litPopUp.Visible = false;
			this.SetReferredByState();
		}

		private void SetReferredByState()
		{
			int ddlReferredByValue = 0;

			if(this.ddlReferredBy.SelectedItem.Value != "")
			{
				ddlReferredByValue = 
					int.Parse(this.ddlReferredBy.SelectedItem.Value);
			}
			
			// 7 = Agent, 8 = Company, 9 = Other
			if(ddlReferredByValue == 7 || ddlReferredByValue  == 8 ||
				ddlReferredByValue == 9)
			{
				this.txtReferredByName.Visible = true;
			}
			else
			{
				this.txtReferredByName.Visible = false;
				this.txtReferredByName.Text = "";
			}
		}

		private bool ProspectHasParentCustomer()
		{
			if((Customer.Prospect)Session["Prospect"] != null)
			{
				Customer.Prospect prospect = (Customer.Prospect)Session["Prospect"];
				
				if(prospect.CustomerNumber.Trim() != "None")
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		private void SetControlState(int Action)
		{
			switch(Action)
			{
				case 1: //ADD ACTION
					this.EnableInputControls(true);
					this.btnEdit.Visible = false;
					this.btnAuditTrail.Visible = false;
					this.BtnSave.Visible = true;
					this.BtnExit.Visible	= false;
					this.cmdCancel.Visible = true;
					this.cmdConvertToCustomer.Visible = false;
					break;
				case 2: //VIEW ACTION
					this.EnableInputControls(false);
					this.btnEdit.Visible = true;
					this.btnAuditTrail.Visible = true;
					this.BtnExit.Visible	= true;
					this.BtnSave.Visible = false;
					this.cmdCancel.Visible = false;
					
					this.cmdConvertToCustomer.Visible = 
						!this.ProspectHasParentCustomer();					
					
					break;
				case 3: //SAVE ACTION
					this.SetControlState((int)UserAction.VIEW);
					break; 
				case 4: //EDIT ACTION
					this.EnableInputControls(true);
					this.btnEdit.Visible = false;
					this.btnAuditTrail.Visible = false;
					this.BtnExit.Visible	= false;
					this.BtnSave.Visible = true;
					this.cmdCancel.Visible = true;
					
					this.cmdConvertToCustomer.Visible = false;
					
					break;
				default: //CANCEL ACTION
					Customer.Prospect prospect = (Customer.Prospect)Session["Prospect"];
					if(prospect.Mode == 1) //ADD
					{
						Session["Prospect"] = null;
						Response.Redirect("SearchProspect.aspx");
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
				this.txtBusinessName.Enabled = true;
				this.txtPhone.Enabled = true;
				this.txtFirstName.Enabled = true;
				this.txtLastName1.Enabled = true;
				this.txtEmail.Enabled = true;
				this.ddlReferredBy.Enabled = true;
				this.txtReferredByName.Enabled = true;
				this.ddlLocation.Enabled = true;
			}
			else
			{
				this.txtBusinessName.Enabled = false;
				this.txtPhone.Enabled = false;
				this.txtFirstName.Enabled = false;
				this.txtLastName1.Enabled = false;
				this.txtEmail.Enabled = false;
				this.ddlReferredBy.Enabled = false;
				this.txtReferredByName.Enabled = false;
				this.ddlLocation.Enabled = false;
			}
		}

		private void FillControls()
		{
			Customer.Prospect prospect = new Customer.Prospect();
			prospect = (Customer.Prospect)Session["Prospect"];
			
			this.txtBusinessName.Text = prospect.BusinessName;
			this.txtFirstName.Text = prospect.FirstName;
			this.txtLastName1.Text = prospect.LastName1;

			this.txtPhone.Text = prospect.WorkPhone;
			this.txtEmail.Text = prospect.Email;

			//Referred by
			this.ddlReferredBy.SelectedIndex = 0;
			if(prospect.ReferredID != 0)
			{
				for(int i = 0; this.ddlReferredBy.Items.Count - 1 >= i; i++)
				{
					if (this.ddlReferredBy.Items[i].Value == 
						prospect.ReferredID.ToString())
					{
						this.ddlReferredBy.SelectedIndex = i;
						i = this.ddlReferredBy.Items.Count-1;
					}
				}
			}

			//Originated at
			this.ddlLocation.SelectedIndex = 0;
			if(prospect.LocationID != 0)
			{
				for(int i = 0; this.ddlLocation.Items.Count - 1 >= i; i++)
				{
					if (this.ddlLocation.Items[i].Value == 
						prospect.LocationID.ToString())
					{
						this.ddlLocation.SelectedIndex = i;
						i = this.ddlLocation.Items.Count-1;
					}
				}
			}

			this.SetReferredByState();

			if(this.txtReferredByName.Visible)
			{
				this.txtReferredByName.Text = prospect.ReferredDesc;
			}

			if(prospect.Mode != 1) 
			{
				FillDataGrid();
			}
		}// End FillControls

		private void RelateCustomerToProspect()
		{
			//RPR 2004-05-17
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

			if(Session["Prospect"] != null && this.lblParentCustomer.Text != "None")
			{
				Customer.Prospect prospect = (Customer.Prospect)Session["Prospect"];
				Customer.Customer customer = 
					Customer.Customer.GetCustomer(this.lblParentCustomer.Text.Trim(),
					userID);
				customer.Mode = 2; //UPDATE
				customer.ProspectID = prospect.ProspectID;
				//customer.Save();
				prospect.CustomerNumber = customer.CustomerNo;
			}
		}

		private void FillProperties(ref Customer.Prospect prospect)
		{	
			prospect.LocationID = 
				ddlLocation.SelectedItem.Value != "" ?int.Parse(ddlLocation.SelectedItem.Value):0;
			prospect.BusinessName = this.txtBusinessName.Text;
			prospect.ReferredID = 
				ddlReferredBy.SelectedItem.Value != "" ?int.Parse(ddlReferredBy.SelectedItem.Value):0;
			prospect.ReferredDesc = this.txtReferredByName.Text;
			prospect.FirstName = this.txtFirstName.Text;
			prospect.LastName1 = this.txtLastName1.Text;
			prospect.WorkPhone = this.txtPhone.Text;
			prospect.Email = this.txtEmail.Text;
			prospect.Modify = true;
			prospect.IsBusiness = true;
		}

		private void FillDataGrid()
		{
			Customer.Prospect prospect = (Customer.Prospect)Session["Prospect"];

			LblError.Visible = false;
			searchIndividual.DataSource = null;
			DtTask = null;

			DtTask = TaskControl.TaskControl.GetTaskControlByProspectID(prospect.ProspectID);
				
			Session.Remove("DtTask");
			Session.Add("DtTask",DtTask);

			if (DtTask.Rows.Count != 0)
			{
				searchIndividual.DataSource = DtTask;
				searchIndividual.DataBind();
			}
			else
			{
				searchIndividual.DataSource = null;
				searchIndividual.DataBind();

				//LblError.Visible = true;
				LblError.Text = "Could not find a match for your search criteria, please try again.";
			}

			LblTotalCases.Text = "Total Cases: "+DtTask.Rows.Count.ToString();	
		}

		private void searchIndividual_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
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

			if(e.Item.ItemType.ToString() != "Pager") // Select
			{
				int i = int.Parse(e.Item.Cells[1].Text);
				TaskControl.TaskControl taskControl = 
					TaskControl.TaskControl.GetTaskControlByTaskControlID(i, userID);
	
				DataTable dtFilter = (DataTable) Session["DtTask"];
				
				DataTable dt = dtFilter.Clone();

				DataRow[] dr = dtFilter.Select("TaskControlTypeID = "+taskControl.TaskControlTypeID,"TaskControlID");				

				for (int rec = 0; rec<=dr.Length-1; rec++)
				{
					DataRow myRow = dt.NewRow();
					myRow["TaskControlID"] = (int) dr[rec].ItemArray[0];
					myRow["TaskControlTypeID"] = (int) dr[rec].ItemArray[3];

					dt.Rows.Add(myRow);
					dt.AcceptChanges();
				}

				taskControl.NavegationTaskControlTable = dt;

				string ToPage;

				if(Session["ToPage"] == null) 
				{
					if (taskControl.TaskControlTypeID == 4)
					{
						ToPage = "ExpressAutoQuote.aspx";
					}
					else
					{
						ToPage = taskControl.GetType().Name.Trim()+".aspx";
					}		
				}
				else
				{
					ToPage = Session["ToPage"].ToString();
				}
	
				if(Session["TaskControl"] == null)
					Session.Add("TaskControl",taskControl);
				else
					Session["TaskControl"] = taskControl;

				Session.Remove("DtTaskControl");
	
				Response.Redirect(ToPage+"?"+taskControl.TaskControlID);
			}
			else  // Pager
			{
				searchIndividual.CurrentPageIndex = int.Parse(e.CommandArgument.ToString())-1;

				searchIndividual.DataSource = (DataTable) Session["DtTask"];
				searchIndividual.DataBind();
			}
		}

		private void searchIndividual_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			DataTable dtCol = (DataTable) Session["DtTask"];
			DataColumnCollection dc = dtCol.Columns;

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DateTime EntryDateField;
				
				if ((string) DataBinder.Eval(e.Item.DataItem,"EntryDate") != "")
				{
					EntryDateField = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem,"EntryDate","{0:MM/dd/yyyy}"));
					e.Item.Cells[3].Text = EntryDateField.ToShortDateString();
				}
			}
		}

		protected void cmdConvertToCustomer_Click(object sender, System.EventArgs e)
		{
			if(Session["Prospect"] != null)
			{
				Customer.Prospect prospect = (Customer.Prospect)Session["Prospect"];
			
				DataTable dtCustomersMatchingPhoneNumbers = 
					prospect.GetClientsWithMatchingPhoneNumbers();
			
				if(dtCustomersMatchingPhoneNumbers.Rows.Count == 0)
				{
					Customer.Customer customer = prospect.GetPopulatedCustomerFromProspect();
					customer.Mode = 1; //ADD
					Session["Customer"] = customer;
					Response.Redirect("ClientBusiness.aspx");
				}
				else
				{
					prospect.CustomersMatchingPhoneNumbers = dtCustomersMatchingPhoneNumbers;
					Session["Prospect"] = prospect;	
					Response.Redirect("MatchingEntities.aspx");
				}
			}
		}

		protected void btnAuditTrail_Click(object sender, System.EventArgs e)
		{
			if(Session["Prospect"] != null)
			{
				Customer.Prospect prospect = (Customer.Prospect) Session["Prospect"];
				Response.Redirect("SearchAuditItems.aspx?type=0&prospectID=" + 
					prospect.ProspectID.ToString());
			}
		}

		protected void BtnExit_Click(object sender, System.EventArgs e)
		{
			this.litPopUp.Visible = false;
			Session["Prospect"] = null;
			Session.Clear();
			Page.Response.Redirect("SearchProspect.aspx");
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.SetControlState((int)UserAction.CANCEL);
			this.NavigationProspects("");
		}

		protected void BtnSave_Click(object sender, System.EventArgs e)
		{
			Customer.Prospect prospect = (Customer.Prospect)Session["Prospect"];
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
			try
			{
				switch(prospect.Mode)
				{
					case 1: //ADD
						this.FillProperties(ref prospect);

						Customer.Customer customer;
						string custNo = prospect.CustomerNumber;

						prospect.SaveProspect(userID);

						if (custNo !="")
						{
							customer  = Customer.Customer.GetCustomer(custNo, userID);
							customer.ProspectID = prospect.ProspectID;
							customer.Mode = (int) Customer.Customer.CustomerMode.UPDATE;
							customer.Save(userID);
						}

						this.RelateCustomerToProspect();
						break;
					case 3: //DELETE
						break;
					case 4: //CLEAR
						break;
					default: //UPDATE
						this.FillProperties(ref prospect);
						prospect.SaveProspect(userID);
						Session["Prospect"] = prospect;
						this.RelateCustomerToProspect();
						this.SetControlState((int)UserAction.VIEW);
						break;
				}

				Session["Prospect"] = prospect;

				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString("Prospect information saved successfully.");
				this.litPopUp.Visible = true;
				this.SetProspectNumerationLabels();
				this.SetControlState((int)UserAction.SAVE);

				FillControls();
			}
			catch(Exception xcp)
			{
				this.litPopUp.Text = 
					Utilities.MakeLiteralPopUpString("Unable to save or modify prospect. " +
					xcp.Message);
				this.litPopUp.Visible = true;
			}
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			Customer.Prospect prospect = (Customer.Prospect)Session["Prospect"];
			prospect.Mode = 2;
			Session["Prospect"] = prospect;
			this.SetControlState((int)UserAction.EDIT);
		}

		private void NavigationProspects(string Case)
		{
			if(Session["Prospect"] != null)
			{
				Customer.Prospect prospect = (Customer.Prospect) Session["Prospect"];
				int prospectID = prospect.ProspectID;
				if(prospect.NavigationProspectTable != null)
				{
					int rec =0;

					for(int i=0;i<=prospect.NavigationProspectTable.Rows.Count-1;i++)
					{
						if (prospectID == 
							int.Parse(prospect.NavigationProspectTable.Rows[i]["ProspectID"].ToString()))
						{
							if (Case == "")
							{
								rec = i+1;
								//lblRecordCount.Text = rec.ToString()+" of " + prospect.NavigationProspectTable.Rows.Count;
							}
							else if (Case == "Previous" || Case == "Begin")
							{
								rec = 1;
								if (i>0)
								{
									switch(Case)
									{
										case "Previous":
											prospectID = 
												int.Parse(prospect.NavigationProspectTable.Rows[i-1]["ProspectID"].ToString());
											rec = i;
											break;
										case "Begin":
											prospectID 
												= int.Parse(prospect.NavigationProspectTable.Rows[0]["ProspectID"].ToString());
											rec = 1;
											break;
									}
									//lblRecordCount.Text = rec.ToString()+" of " + prospect.NavigationProspectTable.Rows.Count;
									i = 
										prospect.NavigationProspectTable.Rows.Count-1; // Para salir del loop.
								}
								else
								{
									//MessageBox.Show(this,"Beginning of the records selected","Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);				
								}
							}
							else if (Case == "Next" || Case == "End")
							{
								if (prospect.NavigationProspectTable.Rows.Count-1>i)
								{					
									switch(Case)
									{
										case "Next":
											prospectID = 
												int.Parse(prospect.NavigationProspectTable.Rows[i+1]["ProspectID"].ToString());
											rec = i+2;
											break;
										case "End":
											prospectID 
												= int.Parse(prospect.NavigationProspectTable.Rows[prospect.NavigationProspectTable.Rows.Count-1]["ProspectID"].ToString());
											rec = prospect.NavigationProspectTable.Rows.Count;
											break;
									}
									//lblRecordCount.Text = rec.ToString()+" of " + prospect.NavigationProspectTable.Rows.Count;
									i 
										= prospect.NavigationProspectTable.Rows.Count-1; // Para salir del loop.
								}
								else
								{
									//MessageBox.Show(this,"End of the records selected","Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
								}
							}
						}
					}																
				}
				DataTable dtProspect = prospect.NavigationProspectTable;
				prospect = prospect.GetProspect(prospectID);
			
				prospect.NavigationProspectTable = dtProspect;

				Session.Add("Prospect",prospect);
				this.FillControls();
				this.SetControlState((int)UserAction.VIEW);
				this.SetProspectNumerationLabels();
			}
		}

		protected void btnNew_Click(object sender, System.EventArgs e)
		{
			Session.Clear();
			Customer.Prospect prospect = new Customer.Prospect();
			prospect.Mode = 1;
			prospect.IsBusiness = true;
			Session["Prospect"] = prospect;
			Response.Redirect("ProspectBusiness.aspx");
		}
	}
}
