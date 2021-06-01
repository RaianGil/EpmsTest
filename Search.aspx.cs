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
using EPolicy.TaskControl;

namespace EPolicy
{
	/// <summary>
	/// Summary description for EventControl.
	/// </summary>
	public partial class EventControl : System.Web.UI.Page
	{
		private DataTable DtTaskControl;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
				Response.Redirect("Default.aspx?001");
			}
			else
			{
                if (!cp.IsInRole("SEARCH MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("Default.aspx?001");
				}
			}

            //this.Form.DefaultButton = this.btnSearch.UniqueID;

			if (!Page.IsPostBack)
			{
                if(Request.QueryString["TaskControlID"] != null)
                {
                    if (Request.QueryString["TaskControlID"] != "")
                    {
                        TxtTaskControlID.Text = Request.QueryString["TaskControlID"].ToString();
                        btnSearch_Click(String.Empty,EventArgs.Empty);
                    }
                }
				//btnCalendar.Visible		  = false;
				//btnCalendar2.Visible	  = false;
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
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.phTopBanner.Controls.Add(Banner);

            //Setup top Banner
            Control BannerLIST = new Control();
            BannerLIST = LoadControl(@"TODOLIST.ascx");
            this.PlaceHolder1.Controls.Add(BannerLIST);

			//Load DownDropList
	
			DataTable dtTaskControlType	= LookupTables.LookupTables.GetTable("TaskControlType");
			DataTable dtAgent			= LookupTables.LookupTables.GetTable("Agent");
			DataTable dtBank			= LookupTables.LookupTables.GetTable("Bank");
			//DataTable dtTaskStatus		= LookupTables.LookupTables.GetTable("TaskStatus");
			

			//TaskControlType
			ddlTaskControlType.DataSource = dtTaskControlType;
			ddlTaskControlType.DataTextField = "TaskControlTypeDesc";
			ddlTaskControlType.DataValueField = "TaskControlTypeID";
			ddlTaskControlType.DataBind();
			ddlTaskControlType.SelectedIndex = -1;
			ddlTaskControlType.Items.Insert(0,"");

			//Agent
			ddlAgent.DataSource = dtAgent;
			ddlAgent.DataTextField = "AgentDesc";
			ddlAgent.DataValueField = "AgentID";
			ddlAgent.DataBind();
			ddlAgent.SelectedIndex = -1;
			ddlAgent.Items.Insert(0,"");

			//Bank
			ddlBank.DataSource = dtBank;
			ddlBank.DataTextField = "BankDesc";
			ddlBank.DataValueField = "BankID";
			ddlBank.DataBind();
			ddlBank.SelectedIndex = -1;
			ddlBank.Items.Insert(0,"");

			//TaskStatus
//			ddlTaskStatus.DataSource = dtTaskStatus;
//			ddlTaskStatus.DataTextField = "TaskStatusDesc";
//			ddlTaskStatus.DataValueField = "TaskStatusID";
//			ddlTaskStatus.DataBind();
			ddlTaskStatus.SelectedIndex = -1;
			ddlTaskStatus.Items.Insert(0,"");
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

		protected void RdbIndividual_CheckedChanged(object sender, System.EventArgs e)
		{
			LblLineOfBusiness.Visible = true;
            LblAgent.Visible = false;
            LblBank.Visible = false;
            LblEventControlStatus.Visible = false;
			LblBeginningDate.Visible		= true;
			LblEndingDate.Visible			= true;
			LblDataType.Visible				= true;

			ddlTaskControlType.Visible = true;
            ddlAgent.Visible = false;
            ddlBank.Visible = false;
            ddlTaskStatus.Visible = false;
			ddlDateType.Visible	= true;

			LblEventControlNo.Visible = false;
			TxtTaskControlID.Visible = false;
			txtBegDate.Visible		  = true;
			TxtEndDate.Visible		  = true;
            imgCalendarEff.Visible = true;
            imgCalendarEnd.Visible = true;

			//btnCalendar.Visible		  = true;
			//btnCalendar2.Visible	  = true;

			ClearTextBox();
		}

		protected void RdbBusiness_CheckedChanged(object sender, System.EventArgs e)
		{
			LblLineOfBusiness.Visible = false;
			LblAgent.Visible = false;
			LblBank.Visible = false;
			LblEventControlStatus.Visible = false;
			LblBeginningDate.Visible		= false;
			LblEndingDate.Visible			= false;
			LblDataType.Visible				= false;

			ddlTaskControlType.Visible = false;
			ddlAgent.Visible = false;
			ddlBank.Visible = false;
			ddlTaskStatus.Visible = false;
			ddlDateType.Visible	= false;

			LblEventControlNo.Visible = true;
			TxtTaskControlID.Visible = true;
			txtBegDate.Visible		  = false;
			TxtEndDate.Visible		  = false;
            imgCalendarEff.Visible = false;
            imgCalendarEnd.Visible = false;
			//btnCalendar.Visible		  = false;
			//btnCalendar2.Visible	  = false;

			ClearTextBox();
		}

		private void GoToSpecificWebPage()
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

			DataTable dtSpec = (DataTable) Session["DtTaskControl"];

			int i = (int) dtSpec.Rows[0]["TaskControlID"];
			TaskControl.TaskControl taskControl = TaskControl.TaskControl.GetTaskControlByTaskControlID(i, userID);

			Session["Prospect"] = taskControl.Prospect;
				
			if(Session["DtTaskControl"] != null)
			{

				DataTable dtFilter = (DataTable) Session["DtTaskControl"];
				
				DataTable dt = dtFilter.Clone();

                dt.Columns.Remove("OriginatedAt");

				DataRow[] dr = 
					dtFilter.Select("TaskControlTypeID = "+taskControl.TaskControlTypeID,"TaskControlID");				

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
                        if (taskControl.TaskControlTypeID == 15) //PersonalPackageQuote
                        {
                            ToPage = "PersonalPackage.aspx";
                        }
                        else
                        {
                            if (taskControl.TaskControlTypeID == 18) // Use for QCertified
                            {
                                ToPage = "VehicleServiceContractQuote.aspx";
                            }
                            else
                            {
                                ToPage = taskControl.GetType().Name.Trim() + ".aspx";
                            }
                        }
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
		}

		private void ClearTextBox()
		{
			searchIndividual.Visible = false;
			searchIndividual.DataSource = null;

			LblTotalCases.Text = "Total Cases: 0";

			txtBegDate.Text	= "";
			TxtEndDate.Text	= "";
			TxtTaskControlID.Text				= "";
			ddlDateType.SelectedIndex	= -1;
			ddlTaskControlType.SelectedIndex = -1;
			ddlAgent.SelectedIndex = -1;
			ddlBank.SelectedIndex = -1;
			ddlTaskStatus.SelectedIndex = -1;
		}

		private void FieldVerify()
		{
			string errorMessage = String.Empty;
			bool found = false;

			if (RdbTaskControlID.Checked)
			{
				if(TxtTaskControlID.Text.Trim() =="" && found == false)
				{
					errorMessage = "The Control No. Field is empty.";
					found = true;
				}
			}
			else
			{
				if (this.txtBegDate.Text == "" &&  this.TxtEndDate.Text != "" &&
					found == false)
				{
					errorMessage = "Please enter the beginning date.";
					found = true;
				}
				if (this.txtBegDate.Text != "" &&  this.TxtEndDate.Text == "" &&
					found == false)
				{
					errorMessage = "Please enter the ending date.";
					found = true;
				}
				else if (this.txtBegDate.Text == "" &&  this.TxtEndDate.Text == "" &&
					this.ddlDateType.SelectedItem.Value.Trim() != "" && found == false)
				{
					errorMessage = "Please enter the beginning date and ending date.";
					found = true;
				}
				else if (this.txtBegDate.Text != "" && this.TxtEndDate.Text != "" &&
					this.ddlDateType.SelectedItem.Value.Trim() == "" && found == false)
				{
					errorMessage = "Please choose the date type.";
					found = true;
				}
				else if ((this.txtBegDate.Text != "" && this.TxtEndDate.Text != "" &&
					DateTime.Parse(this.txtBegDate.Text) > DateTime.Parse(this.TxtEndDate.Text)) && found == false)
				{
					errorMessage = "The Ending Date must be great than beginning date .";
					found = true;
				}
			}
			//throw the exception.
			if (errorMessage != String.Empty)
			{
				throw new Exception(errorMessage);
			}	
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

				//RPR 2004-03-25
				Session["Prospect"] = taskControl.Prospect;
				
				//RPR 2004-03-26
				if(Session["DtTaskControl"] != null)
				{
					DataTable dtFilter = (DataTable) Session["DtTaskControl"];
				
					DataTable dt = dtFilter.Clone();

					DataRow[] dr = 
						dtFilter.Select("TaskControlTypeID = "+taskControl.TaskControlTypeID,"TaskControlID");				

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
                            if (taskControl.TaskControlTypeID == 15) //PersonalPackageQuote
                            {
                                ToPage = "PersonalPackage.aspx";
                            }
                            else
                            {
                                ToPage = taskControl.GetType().Name.Trim() + ".aspx";
                            }
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
			}
			else  // Pager
			{
				searchIndividual.CurrentPageIndex = int.Parse(e.CommandArgument.ToString())-1;

				searchIndividual.DataSource = (DataTable) Session["DtTaskControl"];
				searchIndividual.DataBind();
			}
		}

		private void searchIndividual_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			DataTable dtCol = (DataTable) Session["DtTaskControl"];
			DataColumnCollection dc = dtCol.Columns;

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DateTime EntryDateField;
				DateTime CloseDateField;

				if ((string) DataBinder.Eval(e.Item.DataItem,"EntryDate") != "")
				{
					EntryDateField = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem,"EntryDate","{0:MM/dd/yyyy}"));
					e.Item.Cells[8].Text = EntryDateField.ToShortDateString();
				}

				if ((string) DataBinder.Eval(e.Item.DataItem,"CloseDate") != "")
				{
					CloseDateField = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem,"CloseDate","{0:MM/dd/yyyy}"));
					e.Item.Cells[9].Text = CloseDateField.ToShortDateString();
				}
			}
		}

		protected void ddlTaskControlType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillStatusDDList();
		}

		private void FillStatusDDList()
		{
			if(this.ddlTaskControlType.SelectedItem.Text.Trim() == "")
			{
				ddlTaskStatus.Items.Clear();
				ddlTaskStatus.SelectedIndex  = -1;
				ddlTaskStatus.Items.Insert(0,"");
			}
			else
			{
				ddlTaskStatus.Items.Clear();
				DataTable dtTaskStatus	= LookupTables.LookupTables.GetTableTaskStatusByTaskType(int.Parse(this.ddlTaskControlType.SelectedItem.Value));

				ddlTaskStatus.DataSource     = dtTaskStatus;
				ddlTaskStatus.DataTextField  = "TaskStatusDesc";
				ddlTaskStatus.DataValueField = "TaskStatusID";
				ddlTaskStatus.DataBind();
				ddlTaskStatus.SelectedIndex  = -1;
				ddlTaskStatus.Items.Insert(0,"");
			}
		}

	
		private void BtnExit_Click(object sender, System.EventArgs e)
		{
		
		}

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			try
			{
				FieldVerify();
			}
			catch (Exception exp)
			{
				lblRecHeader.Text = exp.Message;
				mpeSeleccion.Show();
				return;
			}

			searchIndividual.DataSource = null;
			DtTaskControl = null;

			searchIndividual.CurrentPageIndex = 0;
			searchIndividual.Visible = false;

			Login.Login cp = HttpContext.Current.User as Login.Login;
			int userID = 0;
			userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

			if(RdbTaskControlID.Checked)
			{
				DtTaskControl = TaskControl.TaskControl.GetTaskControlByTaskControlIDInTable(TxtTaskControlID.Text.Trim(),userID);
			}
			else
			{
				DtTaskControl = TaskControl.TaskControl.GetTaskControlByCriteria(ddlTaskControlType.SelectedItem.Value.ToString().Trim(),ddlTaskStatus.SelectedItem.Value.ToString().Trim(),
					ddlAgent.SelectedItem.Value.ToString().Trim(), ddlBank.SelectedItem.Value.ToString().Trim(),
					txtBegDate.Text.ToString().Trim(),TxtEndDate.Text.ToString().ToString().Trim(),ddlDateType.SelectedItem.Value.ToString().Trim(),userID);
			}
				
			Session.Remove("DtTaskControl");
			Session.Add("DtTaskControl",DtTaskControl);

			if (DtTaskControl.Rows.Count != 0)
			{
				searchIndividual.Visible = true;
	
				searchIndividual.DataSource = DtTaskControl;
				searchIndividual.DataBind();
			}
			else
			{
				searchIndividual.DataSource = null;
				searchIndividual.DataBind();

				lblRecHeader.Text = "Could not find a match for your search criteria, please try again.";
                mpeSeleccion.Show();
			}

			LblTotalCases.Text = "Total Cases: "+DtTaskControl.Rows.Count.ToString();
	
			//Si tiene un solo record se va a dirigir a la pantalla correspondiente.
			if (DtTaskControl.Rows.Count == 1)
				GoToSpecificWebPage();
		}

		protected void btnClear_Click(object sender, System.EventArgs e)
		{
			ClearTextBox();
		}
	}
}
