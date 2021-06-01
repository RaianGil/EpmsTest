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
using EPolicy.Quotes;
using System.Web.Security;
using Baldrich.DBRequest;
using EPolicy.XmlCooker;
using System.Xml;

namespace EPolicy
{
	/// <summary>
	/// Summary description for Drivers.
	/// </summary>
	public partial class QuoteAutoDrivers : System.Web.UI.Page
	{

		protected string today = DateTime.Now.ToString("MM/dd/yyy");
		protected System.Web.UI.WebControls.Label lblAsterisk4;
		protected System.Web.UI.WebControls.Label lblAsterisk5;
		protected System.Web.UI.WebControls.Label lblAsterisk3;
		protected System.Web.UI.WebControls.Label lblAsterisk2;
		protected System.Web.UI.WebControls.Label lblAsterisk1;
		protected System.Web.UI.WebControls.CustomValidator cvPage;
		protected System.Web.UI.HtmlControls.HtmlInputHidden
			ProspectOffered;


		protected void Page_Load(object sender, System.EventArgs e)
		{
            lblSSN.Visible = false;
            txtSocSec.Visible = false;


            DataTable dtGender = LookupTables.LookupTables.GetTable("Gender");
            DataTable dtMaritalStatus = LookupTables.LookupTables.GetTable("MaritalStatus");

            //MaritalStatus
            ddlMaritalSt.DataSource = dtMaritalStatus;
            ddlMaritalSt.DataTextField = "MaritalStatusDesc";
            ddlMaritalSt.DataValueField = "MaritalStatusID";
            ddlMaritalSt.DataBind();
            ddlMaritalSt.SelectedIndex = -1;
            ddlMaritalSt.Items.Insert(0, "");

            //Gender
            ddlGender.DataSource = dtGender;
            ddlGender.DataTextField = "GenderDesc";
            ddlGender.DataValueField = "GenderID";
            ddlGender.DataBind();
            ddlGender.SelectedIndex = -1;
            ddlGender.Items.Insert(0, "");

			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                Response.Cookies.Add(authCookies);
                FormsAuthentication.SignOut();
                Response.Redirect("Default.aspx?001");
			}
			else
			{
                if (!cp.IsInRole("AUTO PERSONAL DRIVERS") && 
					!cp.IsInRole("ADMINISTRATOR"))
				{
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    Response.Cookies.Add(authCookies);
                    FormsAuthentication.SignOut();
                    Response.Redirect("Default.aspx?001");
				}
			}
		
            txtBirthDt.Attributes.Add("onchange","getAge()");
            imgCalendarBT.Attributes.Add("onblur", "getAge()");
			
			TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
			
//			if (QA.IsPolicy)
//			{
//				btnVehicles.Visible = false;
//			}

            if (!IsPostBack)
			{
                if (Session["TaskControl"] == null)
                {
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    Response.Cookies.Add(authCookies);
                    FormsAuthentication.SignOut();

                    Response.Redirect("Default.aspx?007");
                }

            litPopUp.Visible = false;

                    //if (Session["LookUpTables"] == null)
                    //{
                    //    DataTable dtGender = LookupTables.LookupTables.GetTable("Gender");
                    //    DataTable dtMaritalStatus = LookupTables.LookupTables.GetTable("MaritalStatus");

                    //    //MaritalStatus
                    //    ddlMaritalSt.DataSource = dtMaritalStatus;
                    //    ddlMaritalSt.DataTextField = "MaritalStatusDesc";
                    //    ddlMaritalSt.DataValueField = "MaritalStatusID";
                    //    ddlMaritalSt.DataBind();
                    //    ddlMaritalSt.SelectedIndex = -1;
                    //    ddlMaritalSt.Items.Insert(0, "");

                    //    //Gender
                    //    ddlGender.DataSource = dtGender;
                    //    ddlGender.DataTextField = "GenderDesc";
                    //    ddlGender.DataValueField = "GenderID";
                    //    ddlGender.DataBind();
                    //    ddlGender.SelectedIndex = -1;
                    //    ddlGender.Items.Insert(0, "");
                    //}

				// First Time Visit or not added principal Prospect
				if (QA.Drivers.Count == 0)
				{
					FillProspectAsDriver(QA.Prospect);
					ViewState.Add("Status", "MAINPROSPECT");

					txtFirstNm.Enabled = false;
					txtLastNm1.Enabled = false;
					txtLastNm2.Enabled = false;
				}
				else
				{
					enableFields(false);
					// hide Save btn
					btnSave.Visible = false;

					ViewState.Add("Status","NEW");
				}
				// Show DataGrid
				fillDataGrid(QA.Drivers);


				// if it comes from SelectDriver
				if(Session["DriverID"] != null) // Comes from selectDriver
				{
					int DrvID = int.Parse(Session["DriverID"].ToString());
					AutoDriver AD = AutoDriver.GetAutoDriver(DrvID);
					FillDriver(AD);
				}
				else if (Session["ProspectID"] != null) 
				// Comes from selectDriver
				{
					int PrID = int.Parse(Session["ProspectID"].ToString());
					EPolicy.Customer.Prospect P = 
						(new EPolicy.Customer.Prospect()).GetProspect(PrID);
					// AutoDriver AD = P as AutoDriver;
					// Fill prospect sending prospect.... use it here!!!
					FillProspectAsDriver(P);
				}
				else if (Session["Driver"] != null) 
				// Comes from SelectDriver without selection
				{
					AutoDriver AD = (AutoDriver)Session["Driver"];	
					FillDriver(AD);
				}

				/* set QuoteID
				if (QA.QuoteId != 0)
				{
					txtQuoteID.Text = QA.QuoteId.ToString();
					lblQuoteID.Visible = true;
				}
				else 
				{
					txtQuoteID.Text = "";
					lblQuoteID.Visible = false;
				}*/
								
				// : RPR 2004-03-09
				if(this.IsDriversEmpty())
				{
					this.SetControlState((int)States.NEW_MAIN);
				}
				else if(Session["ProspectID"] != null &&
					Request.QueryString["selectDriver"] == null)
				{
					this.prospectOffered.Value = "1";
					this.SetControlState((int)States.READWRITE);
				}
				else if(Session["ProspectID"] != null &&
					Request.QueryString["selectDriver"] != null &&
					Request.QueryString["selectDriver"].Trim() == "1")
				{
					this.prospectOffered.Value = "1";
					this.SetControlState((int)States.NEW);
				}
				else
				{
					this.SetControlState((int)States.REST);
				}
				// :~*

				//: RPR 2004-03-23
				if(Session["ProspectID"] == null)
				{
					this.SelectFirstDriver();
				}

				Session.Remove("DriverID");
				Session.Remove("ProspectID");
				Session.Remove("Driver");


                

			}
			else
			{
				if (Callback.Value == "Y")
				{
					AutoDriver AD = LoadFromForm(0,0,0);
					Session.Add("Driver",AD);
					Response.Redirect("QuoteAutoSelectDriver.aspx");
				}
                this.SetStateFromSelectDriver();
			}
			this.SetStateFromSelectDriver();
			litPopUp.Visible = false;
			this.SetTaskControlIDLabel();
		}

		private void SelectFirstDriver()
		{
			System.Web.UI.WebControls.DataGridCommandEventArgs e;
			object argument = new Object();
			System.Web.UI.WebControls.CommandEventArgs originalArgs =
                new System.Web.UI.WebControls.CommandEventArgs("dgDriverList_ItemCommand", argument); 
			
			if(this.dgDriverList.Items.Count > 0)
			{
				e = new System.Web.UI.WebControls.DataGridCommandEventArgs(
					this.dgDriverList.Items[0], this.dgDriverList, 
					originalArgs);
                this.SelectDriver(e);
				this.dgDriverList.SelectedIndex = 0;
			}
		}

		//RPR 2004-03-12
		private void SetStateFromSelectDriver()
		{
			if(Session["FromDriverSelect"] != null &&
				Session["FromDriverSelect"].ToString().Trim() == "1")
			{
				this.SetControlState((int)States.NEW);
				this.prospectOffered.Value = "1";
				Session.Remove("FromDriverSelect");
			}
		}

		//RPR 2004-03-11
		private void SetTaskControlIDLabel()
		{
			if(Session["TaskControl"] != null)
			{
				TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

				if (QA.TaskControlID != 0)
				{
					txtTaskControlID.Text = QA.TaskControlID.ToString();
					lblTaskControlID.Visible = true;
				}
				else 
				{
					txtTaskControlID.Text = "";
					//RPR 2004-03-18
					//lblTaskControlID.Visible = false;
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

			//  Benny:	this.InitializeDriverObjectSessionVariable();

			Control Banner = new Control();
			Banner = LoadControl(@"TopBannerNew.ascx");
			this.phTopBanner.Controls.Add(Banner);

			//Setup Left-side Banner
			//Control LeftMenu = new Control();
			//LeftMenu = LoadControl(@"LeftMenu.ascx");
			//this.BindDdls();
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

        private void BindDdls()
        {
            this.BindDdl(this.ddlMaritalSt, "MaritalStatus",
                "MaritalStatusID", "MaritalStatusDesc");
            this.BindDdl(this.ddlGender, "Gender", "GenderID", "GenderDesc");
        }

        private void BindDdl(System.Web.UI.WebControls.DropDownList DropDownList,
            string TableName, string ValueFieldName, string TextFieldName)
        {
            DataTable dtResults;

            try
            {
                dtResults = LookupTables.LookupTables.GetTable(TableName);

                if (dtResults != null && dtResults.Rows.Count > 0)
                {
                    DropDownList.DataSource = dtResults;
                    DropDownList.DataValueField = ValueFieldName;
                    DropDownList.DataTextField = TextFieldName;
                    DropDownList.DataBind();
                    DropDownList.SelectedIndex = -1;
                    DropDownList.Items.Insert(0, "");
                }
            }
            catch (Exception e)
            {
                string a = e.Message;
            }
        }

        
        

		private AutoDriver LoadFromForm(
			int DriverID, int ProspectID, int InternalDriverID)
		{
			// TaskControl.QuoteAuto QA = 
			//(TaskControl.QuoteAuto)Session["TaskControl"];
			AutoDriver AD = new AutoDriver();

			// PROSPECT INFORMATION
			AD.FirstName = txtFirstNm.Text.ToUpper();
			AD.LastName1 = txtLastNm1.Text.ToUpper();
			AD.LastName2 = txtLastNm2.Text.ToUpper();
			
			// PROSPECT INFORMATION FROM QUOTES PRIMARY PROSPECT
			if(Session["TaskControl"] != null)
			{
				Customer.Prospect P 
					= ((TaskControl.QuoteAuto)
					Session["TaskControl"]).Prospect;

				AD.HomePhone = P.HomePhone;
				AD.WorkPhone = P.WorkPhone;
				AD.Cellular = P.Cellular;
				AD.LocationID = P.LocationID;
			}

			// DRIVER INFORMATION
			AD.BirthDate = txtBirthDt.Text;
			AD.Gender = int.Parse(ddlGender.SelectedItem.Value);
			AD.MaritalStatus = int.Parse(ddlMaritalSt.SelectedItem.Value);
			AD.License = txtLicense.Text.ToUpper();
			AD.SocialSecurity = txtSocSec.Text;
			//			AD.DriverID = ;
			AD.QuoteID = ((TaskControl.QuoteAuto)Session["TaskControl"]).QuoteId;
			//			AD.OccupationId = float.Parse(txtCharge.Text);
			//			AD.EmpAddr1 = float.Parse(txtPremium.Text) + QA.Charge;
			//			AD.EmpAddr2 = QA.TotalPremium.ToString();
			//			AD.EmpCity = null;
			if(ProspectID != 0)
			{
				AD.ProspectID = ProspectID;
				((EPolicy.Customer.Prospect)AD).Mode = 
					(int) EPolicy.Customer.Prospect.ProspectMode.UPDATE;
				AD.Mode = 2; // Update
			}
			else 
			{
				AD.Mode = 1; // Insert
				((EPolicy.Customer.Prospect)AD).Mode = 
					(int) EPolicy.Customer.Prospect.ProspectMode.ADD;
			}
			
			if(DriverID != 0)
			{
				AD.DriverID = DriverID;
				AD.Mode = 2; // Update
			}

			//Added by Rafael Pérez
			//2004-02-09
			if(InternalDriverID != 0)
			{
				AD.InternalID = InternalDriverID;
				AD.Mode = 2; //Update
			}

			return AD;
		}
		//		private void AddDriverToQuote(AutoDriver AD)
		//		{
		//			TaskControl.QuoteAuto QA = ((TaskControl.QuoteAuto)Session["TaskControl"]);
		//			QA.AddDriver(AD);
		//			//Session["TaskControl"] = QA;
		//		}
		private void fillDataGrid(ArrayList AL)
		{
			if (AL != null && AL.Count > 0)
			{
				DataTable DT = getDisplayDataTable();
				DataRow row;
				// Add all values in one shot
				for (int i = 0; i < AL.Count; i++)
				{
					AutoDriver AD = (AutoDriver)AL[i];
					if (AD.Mode != (int)Enumerators.Modes.Delete)
					{
						row = DT.NewRow();
						//RPR 2004-03-10
						if(Session["TaskControl"] != null)
						{
							TaskControl.QuoteAuto QA = 
								(TaskControl.QuoteAuto)Session["TaskControl"];

							if(AD.ProspectID == QA.Prospect.ProspectID)
								row["FirstName"] = "*" + AD.FirstName;
							else
								row["FirstName"] = AD.FirstName;
						}
						else
						{
							row["FirstName"] = AD.FirstName;
						}
						
						row["LastName1"] = AD.LastName1;
						row["LastName2"] = AD.LastName2;
						
						row["HomePhone"] = AD.HomePhone;
						row["WorkPhone"] = AD.WorkPhone;
						row["Cellular"] = AD.Cellular;
						
						if(AD.BirthDate == null || AD.BirthDate == "")
							row["BirthDate"] = DBNull.Value;
						else
							row["BirthDate"] = AD.BirthDate;
						try
						{
							row["Gender"] = 
								((Enumerators.Gender)AD.Gender).ToString();
						}
						catch
						{
							row["Gender"] = "";
						}
						
						try
						{
							row["MaritalStatusDesc"] = 
								((Enumerators.MaritalStatus)
								AD.MaritalStatus).ToString();
						}
						catch
						{
							row["MaritalStatusDesc"] = "";
						}
						
						row["License"] = AD.License;
						row["SocialSecurity"] = AD.SocialSecurity;
						row["ProspectID"] = AD.ProspectID;
						row["QuotesDriversID"] = AD.DriverID;
						row["GenderID"] = AD.Gender;
						row["MaritalStatusID"] = AD.MaritalStatus;
						//Added by Rafael Pérez
						//2004-02-09
						row["InternalDriverID"] = AD.InternalID;
						DT.Rows.Add(row);
					}
				}
				dgDriverList.DataSource = DT;
				dgDriverList.DataBind();
			}
		}
		private DataTable getDisplayDataTable()
		{
			DataSet ds = new DataSet("DSQuotesDrivers");
			DataTable dt = ds.Tables.Add("QuotesDrivers");

			dt.Columns.Add("ID", typeof(Int32));
			dt.Columns.Add("FirstName", typeof(string));
			dt.Columns.Add("LastName1", typeof(string));
			dt.Columns.Add("LastName2", typeof(string));
			dt.Columns.Add("BirthDate", typeof(string));
			dt.Columns.Add("Gender", typeof(string));
			dt.Columns.Add("MaritalStatusDesc", typeof(string));
			dt.Columns.Add("License", typeof(string));
			dt.Columns.Add("SocialSecurity", typeof(string));
			dt.Columns.Add("ProspectID", typeof(int));
			dt.Columns.Add("QuotesDriversID", typeof(int));
			dt.Columns.Add("GenderID", typeof(int));
			dt.Columns.Add("MaritalStatusID", typeof(int));
			dt.Columns.Add("HomePhone", typeof(string));
			dt.Columns.Add("WorkPhone", typeof(string));
			dt.Columns.Add("Cellular", typeof(string));
			//Added by Rafael Pérez
			//2004-02-09
			dt.Columns.Add("InternalDriverID", typeof(int));
			return dt;

		}
					
		private void clearFields()
		{
			txtFirstNm.Text = "";
			txtLastNm1.Text = "";
			txtLastNm2.Text = "";
			txtBirthDt.Text = "";
			ddlGender.SelectedIndex = 0;
			ddlMaritalSt.SelectedIndex = 0;
			txtLicense.Text = "";
			txtSocSec.Text = "";
			txtAge.Text = "";
			//			txtHomePhone.Text = "";
			//			txtWorkPhone.Text = "";
			//			txtCellular.Text = "";

			//		ViewState.Add("Status", "NEW");
			

			txtFirstNm.Enabled = true;
			txtLastNm1.Enabled = true;
			txtLastNm2.Enabled = true;
		}

		private void SelectDriver(System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			//RPR 2004-03-10
			if(int.Parse(e.Item.Cells[9].Text) == 
				((TaskControl.QuoteAuto)
				Session["TaskControl"]).Prospect.ProspectID)
				txtFirstNm.Text = e.Item.Cells[1].Text.Remove(0, 1);
			else
				txtFirstNm.Text = e.Item.Cells[1].Text;
			//:~

			txtLastNm1.Text = e.Item.Cells[2].Text;

			if (e.Item.Cells[3].Text.ToLower() != "&nbsp;")
				txtLastNm2.Text = e.Item.Cells[3].Text;
			else 
				txtLastNm2.Text = "";

			txtBirthDt.Text = e.Item.Cells[4].Text;
			txtAge.Text = CalcAge(e.Item.Cells[4].Text).ToString();
			ddlGender.SelectedIndex = 
				ddlGender.Items.IndexOf(
				ddlGender.Items.FindByValue(e.Item.Cells[11].Text));
			ddlMaritalSt.SelectedIndex = 
				ddlMaritalSt.Items.IndexOf(
				ddlMaritalSt.Items.FindByValue(e.Item.Cells[12].Text));
			
			if (e.Item.Cells[7].Text.ToLower() != "&nbsp;")
				txtLicense.Text = e.Item.Cells[7].Text;
			else 
				txtLicense.Text = "";
			if (e.Item.Cells[8].Text.ToLower()  != "&nbsp;")
				txtSocSec.Text = e.Item.Cells[8].Text;
			else 
				txtSocSec.Text = "";

			if(int.Parse(e.Item.Cells[9].Text) ==
				((TaskControl.QuoteAuto)
				Session["TaskControl"]).Prospect.ProspectID)
			{
				ViewState.Add("Status", "MAINPROSPECT");

				txtFirstNm.Enabled = false;
				txtLastNm1.Enabled = false;
				txtLastNm2.Enabled = false;
			}
			else
			{
				ViewState.Add("Status", "EDIT");
				txtFirstNm.Enabled = true;
				txtLastNm1.Enabled = true;
				txtLastNm2.Enabled = true;
			}
			ViewState.Add("ProspectID", e.Item.Cells[9].Text);
			ViewState.Add("DriverID", e.Item.Cells[10].Text);
				
			//Added by Rafael Pérez
			//2004-02-09
			ViewState.Add("InternalDriverID", e.Item.Cells[17].Text);
				
			enableFields(true);
			btnSave.Visible = true;
			this.SetControlState((int)States.READONLY); //RPR 2004-03-09
		}

      

		private int CalcAge(string birthDT)
		{
			DateTime pdt = DateTime.Parse(birthDT);
			DateTime now = DateTime.Now;
			TimeSpan ts = now - pdt;
			int Years = (int)(((float)ts.Days) / 365.25f);
			return Years;
		}
		
		private void FillProspectAsDriver(Customer.Prospect P)
		{
			// Customer.Prospect P = QA.Prospect;
			//			AutoDriver AD = new AutoDriver();
			//			AD.ProspectID = P.ProspectID;
			//			AD.FirstName = P.FirstName;
			//			AD.LastName1 = P.LastName1;
			//			AD.LastName2 = P.LastName2;
			//			QA.Drivers.Add(AD);
					
			txtFirstNm.Text = P.FirstName;
			txtLastNm1.Text = P.LastName1;
			txtLastNm2.Text = P.LastName2;
			//			txtHomePhone.Text = P.HomePhone;
			//			txtWorkPhone.Text = P.WorkPhone;
			//			txtCellular.Text = P.Cellular;
		}

		private bool ValidateNew(AutoDriver AD)
		{
			bool result = true;
			// Validate if Driver or Prospect exists on DB

            //Se comento para que no validara y guardara cualquier driver.
            //if((AutoDriver.GetQuotesDriversByCriteria(AD)).Rows.Count > 0
            //    && this.prospectOffered.Value == "0")
            //{
            //    result = false;
            //    string tmpMesg = "Driver already exists. " +
            //        "\\n Do you want to select one of the existing records?";
            //    litPopUp.Text = MakeConfirmPopUpString(tmpMesg);
            //    litPopUp.Visible = true;
            //}
			return result;
		}

		private bool ValidateDelete(AutoDriver AD)
		{
			bool result = true;
			//	string tmpMesg = "";

			TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
			
			///Test if Driver is Main Prospect
			if(AD.ProspectID == QA.Prospect.ProspectID)
				return false;
				
			// Test if Driver is assigned to auto
			for(int i = 0; i < QA.AutoCovers.Count; i++)
			{
				AutoCover AC = (AutoCover)QA.AutoCovers[i];
				// test for existance & not mode=DELETE
				AssignedDriver AsDrv = new AssignedDriver();
				AsDrv.AutoDriver = AD;
				int t = AC.AssignedDrivers.IndexOf(AsDrv);
				if(t >= 0)
				{ 
					AssignedDriver AssnDrv = (AssignedDriver)AC.AssignedDrivers[t];
					if (AssnDrv.Mode != (int)Quotes.Enumerators.Modes.Delete)
						return false;
				}
			}
			return result;
		}

		private string MakeConfirmPopUpString(string message)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("<script language=javascript>if(confirm('");
			message = System.Text.RegularExpressions.Regex.Replace(message,"\\r\\n",@"\r\n");
			message = System.Text.RegularExpressions.Regex.Replace(message,"\\n\\r",@"\n\r");
			message = System.Text.RegularExpressions.Regex.Replace(message,"'",@"\'");
			message = System.Text.RegularExpressions.Regex.Replace(message,"\"","\\\"");
			sb.Append(message);
			sb.Append("')){QuoteAutoDrivers.Callback.value='Y';QuoteAutoDrivers.submit();QuoteAutoDrivers.prospectOffered.value='1';}else{QuoteAutoDrivers.Callback.value='N';QuoteAutoDrivers.prospectOffered.value='1';QuoteAutoDrivers.submit();}</script>");
			return(sb.ToString());
		}
		private void FillDriver(AutoDriver AD)
		{
			txtFirstNm.Text = AD.FirstName;
			txtLastNm1.Text = AD.LastName1;
			txtLastNm2.Text = AD.LastName2;
			txtLicense.Text = AD.License;
			txtSocSec.Text = AD.SocialSecurity;
			txtBirthDt.Text = AD.BirthDate;
			ddlGender.SelectedIndex = ddlGender.Items.IndexOf(ddlGender.Items.FindByValue(AD.Gender.ToString()));
			ddlMaritalSt.SelectedIndex = ddlMaritalSt.Items.IndexOf(ddlMaritalSt.Items.FindByValue(AD.MaritalStatus.ToString()));
			txtAge.Text = CalcAge(AD.BirthDate).ToString();
		}

		private void cvPage_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
		{
			string tmpMesg = "";
			//	rfvFirstNm
			if (txtFirstNm.Text == "")
			{
				args.IsValid = false;
				tmpMesg = tmpMesg + "\\n" + "Please enter Driver Name.";
			}
			//	rfvLastNm1
			if (txtLastNm1.Text == "")
			{
				args.IsValid = false;
				tmpMesg = tmpMesg + "\\n" + "Please enter Driver Last Name.";
			}
			//	rfvBirthDt
			if (txtBirthDt.Text == "" || CalcAge(txtBirthDt.Text) < 18)
			{
				args.IsValid = false;
				tmpMesg = tmpMesg + "\\n" + "Please enter a valid Birth Date.";
			}
			//	rfvGender
			if (ddlGender.SelectedIndex <= 0)
			{
				args.IsValid = false;
				tmpMesg = tmpMesg + "\\n" + "Please select Gender.";
			}	
			//	rfvMaritalSt
			if (ddlMaritalSt.SelectedIndex <= 0)
			{
				args.IsValid = false;
				tmpMesg = tmpMesg + "\\n" + "Please select Marital Status.";
			}
			//	cvLicense
//			if (txtLicense.Text == "")
//			{
//				args.IsValid = false;
//				tmpMesg = tmpMesg + "\\n" + "Please enter Driver's License.";
//			}
			//	cvSSN
//			if (txtSocSec.Text == "")
//			{
//				args.IsValid = false;
//				tmpMesg = tmpMesg + "\\n" + "Please enter Social Security Number.";
//			}
			// Wrap it Up!!
			if(!args.IsValid)
			{
				litPopUp.Text = Utilities.MakeLiteralPopUpString(tmpMesg);
				litPopUp.Visible = true;
			}
		}

		private void enableFields(bool status)
		{
			txtFirstNm.Enabled = status;
			txtLastNm1.Enabled = status;
			txtLastNm2.Enabled = status;
			txtBirthDt.Enabled = status;
			txtAge.Enabled = status;
			ddlMaritalSt.Enabled = status;
			ddlGender.Enabled = status;
			txtSocSec.Enabled = status;
			txtLicense.Enabled = status;
		}
		
		//RPR 2004-03-17
		private void ReassignDrivers(AutoDriver Driver)
		{
			TaskControl.QuoteAuto QA = 
				(TaskControl.QuoteAuto)
				(TaskControl.QuoteAuto)Session["TaskControl"];

			foreach(AutoCover autoCover in QA.AutoCovers)
			{
				foreach(AssignedDriver assignedDriver in 
					autoCover.AssignedDrivers)
				{
					if(assignedDriver.AutoDriver.Equals(Driver))
					{
                        assignedDriver.AutoDriver = Driver;
					}
				}
			}
			Session["TaskControl"] = QA;
		}

		// : RPR 2004-03-09
		private bool IsNewQuote()
		{
			TaskControl.QuoteAuto quoteAuto = 
				this.GetQuoteObjectFromSession();

			if(quoteAuto != null)
			{
				if(quoteAuto.QuoteId == 0)
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
				return true;
			}
		}

		private bool IsDriversEmpty()
		{
			TaskControl.QuoteAuto quoteAuto = 
				this.GetQuoteObjectFromSession();
			if(quoteAuto != null)
			{
				if(quoteAuto.Drivers != null && 
					quoteAuto.Drivers.Count > 0)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			else
			{
				return true;
			}
		}

		private void SetControlState(int State)
		{
			switch(State)
			{
				case (int)States.REST:
					this.txtFirstNm.Enabled = false;
					this.txtLastNm1.Enabled = false;
					this.txtLastNm2.Enabled = false;
					this.txtBirthDt.Enabled = false;
                    this.imgCalendarBT.Visible = false;
					this.ddlGender.Enabled = false;
					this.ddlMaritalSt.Enabled = false;
					this.txtLicense.Enabled = false;
					this.txtSocSec.Enabled = false;

					this.btnEdit.Visible = true;
					this.btnSave.Visible = false;
					this.btnCancel.Visible = false;
					this.btnAddDriver.Visible = true;
					this.btnVehicles.Visible = true;
					this.btnBack.Visible = true;
					break;
				case (int)States.NEW_MAIN:
					this.txtFirstNm.Enabled = false;
					this.txtLastNm1.Enabled = false;
					this.txtLastNm2.Enabled = false;
					this.txtBirthDt.Enabled = true;
                    this.imgCalendarBT.Visible = true;
					this.ddlGender.Enabled = true;
					this.ddlMaritalSt.Enabled = true;
					this.txtLicense.Enabled = true;
					this.txtSocSec.Enabled = true;

					this.btnEdit.Visible = false;
					this.btnSave.Visible = true;
					this.btnCancel.Visible = true;
					this.btnAddDriver.Visible = false;
					this.btnVehicles.Visible = false;
					this.btnBack.Visible = false;
					break;
				case (int)States.NEW:
					this.txtFirstNm.Enabled = true;
					this.txtLastNm1.Enabled = true;
					this.txtLastNm2.Enabled = true;
					this.txtBirthDt.Enabled = true;
                    this.imgCalendarBT.Visible = true;
					this.ddlGender.Enabled = true;
					this.ddlMaritalSt.Enabled = true;
					this.txtLicense.Enabled = true;
					this.txtSocSec.Enabled = true;

					this.btnEdit.Visible = false;
					this.btnSave.Visible = true;
					this.btnCancel.Visible = true;
					this.btnAddDriver.Visible = false;
					this.btnVehicles.Visible = false;
					this.btnBack.Visible = false;
					break;
				case (int)States.READONLY:
					this.txtFirstNm.Enabled = false;
					this.txtLastNm1.Enabled = false;
					this.txtLastNm2.Enabled = false;
					this.txtBirthDt.Enabled = false;
                    this.imgCalendarBT.Visible = false;
					this.ddlGender.Enabled = false;
					this.ddlMaritalSt.Enabled = false;
					this.txtLicense.Enabled = false;
					this.txtSocSec.Enabled = false;

					this.btnEdit.Visible = true;
					this.btnSave.Visible = false;
					this.btnCancel.Visible = false;
					this.btnAddDriver.Visible = true;
					this.btnVehicles.Visible = true;
					this.btnBack.Visible = true;
					break;
				case (int)States.READWRITE:
					this.txtFirstNm.Enabled = true;
					this.txtLastNm1.Enabled = true;
					this.txtLastNm2.Enabled = true;
					this.txtBirthDt.Enabled = true;
                    this.imgCalendarBT.Visible = true;
					this.ddlGender.Enabled = true;
					this.ddlMaritalSt.Enabled = true;
					this.txtLicense.Enabled = true;	
					this.txtSocSec.Enabled = true;

					this.btnEdit.Visible = false;
					this.btnSave.Visible = true;
					this.btnCancel.Visible = true;
					this.btnAddDriver.Visible = false;
					this.btnVehicles.Visible = false;
					this.btnBack.Visible = false;
					break;
				case (int)States.READWRITEMAIN:
					this.txtFirstNm.Enabled = false;
					this.txtLastNm1.Enabled = false;
					this.txtLastNm2.Enabled = false;
					this.txtBirthDt.Enabled = true;
                    this.imgCalendarBT.Visible = true;
					this.ddlGender.Enabled = true;
					this.ddlMaritalSt.Enabled = true;
					this.txtLicense.Enabled = true;
					this.txtSocSec.Enabled = true;

					this.btnEdit.Visible = false;
					this.btnSave.Visible = true;
					this.btnCancel.Visible = true;
					this.btnAddDriver.Visible = false;
					this.btnVehicles.Visible = false;
					this.btnBack.Visible = false;
					break;
				default:
					//
					break;
			}

			if(this.IsDriversEmpty())
			{
				//this.btnAddDriver.Visible = false;
			}
			else
			{
				//this.btnAddDriver.Visible = false; //true;
			}
		}
		
		private TaskControl.QuoteAuto GetQuoteObjectFromSession()
		{
			if(Session["TaskControl"] != null)
			{
				return (TaskControl.QuoteAuto) Session["TaskControl"];
			}
			else
			{
				return null;
			}
		}

        //private void dgDriverList_ItemCreated (object source, DataGridItemEventArgs e)
        //{	
        //    if(e.Item.ItemType == ListItemType.Item || 
        //        e.Item.ItemType == ListItemType.AlternatingItem ||
        //        e.Item.ItemType  == ListItemType.EditItem)
        //    {
        //        TableCell tableCell = new TableCell();
        //        tableCell =	e.Item.Cells[13];
        //        Button button = new Button();
        //        button = (Button)tableCell.Controls[0];
        //        button.Attributes.Add("onclick", 
        //            "return confirm( " + 
        //            "\"Are you sure you want to delete this driver?\")");
        //    }
        //}

		private enum States {NEW_MAIN, READONLY, 
			READWRITE, REST, READWRITEMAIN, NEW};

		private bool ValidateThis()
		{
			TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto) Session["TaskControl"];
			ArrayList errorMessages = new ArrayList();

			//First Name
			if(this.txtFirstNm.Text == "")
			{
				errorMessages.Add("First Name is missing.");
			}

//			//Last Name 1
//			if(this.txtLastNm1.Text == "")
//			{
//				errorMessages.Add("Last Name is missing.");
//			}			
			
			//Effective date
			if(this.txtBirthDt.Text == "")
			{
				errorMessages.Add("Birthdate is missing.");
			}
			else if(!this.IsSamsValidDate(this.txtBirthDt.Text))
			{
				errorMessages.Add("Birthdate is invalid.  The " +
					"correct format is \"mm/dd/yyyy\".");
			}
			else if(this.CalcAge(this.txtBirthDt.Text.Trim()) < 16)
			{
				errorMessages.Add("Invalid birthdate. A driver " + 
					"must be 16 years old or more.");
			}

			//Gender
			if(this.ddlGender.SelectedItem.Text.Trim() == 
				string.Empty)
			{
				errorMessages.Add("Gender is missing.");
			}

			//Marital Status
			if(this.ddlMaritalSt.SelectedItem.Text.Trim() == 
				string.Empty)
			{
				errorMessages.Add("Marital status is missing.");
			}

            //if (QA.IsPolicy)
            //{
            //    //Social Security
            //    if(this.txtSocSec.Text.Trim() == 
            //        string.Empty)
            //    {
            //        errorMessages.Add("Social Security is missing.");
            //    }

            //    //License
            //    if(this.txtLicense.Text.Trim() == 
            //        string.Empty)
            //    {
            //        errorMessages.Add("License is missing.");
            //    }
            //}

			if(errorMessages.Count > 0)
			{
				string popUpString = "";
				
				foreach(string message in errorMessages)
				{
					popUpString += message + " ";
				}

                //this.litPopUp.Text = 
                //    Utilities.MakeLiteralPopUpString(popUpString);
                //this.litPopUp.Visible = true;

                lblRecHeader.Text = Utilities.MakeLiteralPopUpString(popUpString);
               //lblRecHeader.Text = popUpString;
               mpeSeleccion.Show();
				return false;
			}
			return true;
		}

		public bool IsSamsValidDate(string sdate) 
		{
			DateTime dt = DateTime.Today;
			bool isDate = true;
			char[] splitter = {'/'};

			try 
			{
				dt = DateTime.Parse(sdate);  
			}
			catch 
			{
				isDate = false;
				return isDate;
			}

			if(sdate.Split(splitter).Length != 3 ||
				sdate.Split(splitter, 3)[2].Length != 4)
			{
				isDate = false;
			}

			return isDate;
		}

        protected void btnBack_Click1(object sender, EventArgs e)
        {

        }
        protected void btnAddDriver_Click(object sender, EventArgs e)
        {
            ViewState.Add("Status", "NEW");
            clearFields();
            enableFields(true);
            btnSave.Visible = true;
            this.prospectOffered.Value = "0";
            this.SetControlState((int)States.NEW);

            this.dgDriverList.SelectedIndex = -1;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateThis())
            {
                TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
                AutoDriver AD = null;
                int DID = 0;
                int PID = 0;
                int IDID = 0;

                if (ViewState["DriverID"] != null)
                    DID = int.Parse(ViewState["DriverID"].ToString());
                if (ViewState["ProspectID"] != null)
                    PID = int.Parse(ViewState["ProspectID"].ToString());
                if (ViewState["InternalDriverID"] != null)
                    IDID = int.Parse(ViewState["InternalDriverID"].ToString());
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = 0;
                try
                {
                    userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
                }
                catch (Exception ex)
                {
                    throw new Exception(
                        "Could not parse user id from cp.Identity.Name.",ex);
                }

                switch (ViewState["Status"].ToString())
                {
                    case "MAINPROSPECT":
                        AD = LoadFromForm(DID, QA.Prospect.ProspectID, IDID);

                        if (DID == 0)
                            AD.Mode = (int)Enumerators.Modes.Insert;
                        else
                            AD.Mode = (int)Enumerators.Modes.Update;

                        ((EPolicy.Customer.Prospect)AD).Mode =
                            (int)
                            EPolicy.Customer.Prospect.ProspectMode.CLEAR;
                        QA.RemoveDriver(AD);
                        QA.AddDriver(AD);

                        if (QA.TaskControlID != 0)
                        {
                            QA.Mode = (int)Enumerators.Modes.Update;
                        }
                        else
                        {
                            QA.Mode = (int)Enumerators.Modes.Insert;
                        }

                        QA.Save(userID, null, AD, false);
                        lblRecHeader.Text = "Driver Saved Successfully ";
                        mpeSeleccion.Show();

                        if (QA.IsPolicy)  //Salvar los cambios en Customer.
                        {
                            QA.Customer.Birthday = AD.BirthDate;
                            QA.Customer.MaritalStatus = AD.MaritalStatus;
                            QA.Customer.Licence = AD.License;
                            QA.Customer.SocialSecurity = AD.SocialSecurity;

                            switch (int.Parse(ddlGender.SelectedItem.Value))
                            {
                                case 1:
                                    QA.Customer.Sex = "M";  //1
                                    break;

                                case 2:
                                    QA.Customer.Sex = "F"; //2
                                    break;

                                default:
                                    QA.Customer.Sex = "";
                                    break;
                            }
                            QA.Customer.Mode = 2;  //Update
                            QA.Customer.Save(userID);
                        }

                        if (AD.Mode == (int)Enumerators.Modes.Update)
                        {
                            Session["TaskControl"] = QA;
                            this.ReassignDrivers(AD);
                            QA = (TaskControl.QuoteAuto)
                                (TaskControl.QuoteAuto)
                                Session["TaskControl"];
                        }

                        //:~
                        fillDataGrid(QA.Drivers);

                        this.dgDriverList.SelectedIndex =
                            this.dgDriverList.Items.Count - 1;

                        ViewState.Add("Status", "NEW");
                        btnSave.Visible = false;
                        //RPR 2004-03-09
                        this.SetControlState((int)States.REST);
                        break;
                    case "EDIT":
                        AD = LoadFromForm(DID, PID, /*RPR*/ IDID);
                        AD.Mode = (int)Enumerators.Modes.Update;
                        ((EPolicy.Customer.Prospect)AD).Mode =
                            (int)
                            EPolicy.Customer.Prospect.ProspectMode.UPDATE;
                        QA.RemoveDriver(AD);
                        QA.AddDriver(AD);

                        AD.Save(userID, false, 0);
                        SaveAutoDriverLicense(AD.ProspectID,txtLicense.Text);

                        Session["TaskControl"] = QA;
                        this.ReassignDrivers(AD);
                        QA = (TaskControl.QuoteAuto)
                            (TaskControl.QuoteAuto)
                            Session["TaskControl"];
                        fillDataGrid(QA.Drivers);

                        this.dgDriverList.SelectedIndex =
                            this.dgDriverList.Items.Count - 1;

                        ViewState.Add("Status", "NEW");
                        btnSave.Visible = false;
                        this.SetControlState((int)States.REST);
                        break;
                    case "NEW":
                        AD = LoadFromForm(0, 0, 0);
                        if (ValidateNew(AD))
                        {
                            AD.Mode = (int)Enumerators.Modes.Insert;
                            ((EPolicy.Customer.Prospect)AD).Mode =
                                (int)
                                EPolicy.Customer.Prospect.ProspectMode.ADD;

                            //Identifies the driver as chosen
                            //from "QuoteAutoSelectDriver.aspx"
                            //in order to avoid duplicating the
                            //existing prospect.
                            if (Request.QueryString["selectDriver"] !=
                                null &&
                                Request.QueryString["selectDriver"].Trim()
                                == "1" &&
                                Request.QueryString["prospectID"] != null)
                            {
                                if (QA.IsPolicy)
                                {
                                    AD.SetIsPolicy(true);
                                }

                                AD.Save(userID, true,
                                    int.Parse(
                                    Request.QueryString
                                    ["prospectID"].Trim()));
                            }
                            else
                            {
                                if (QA.IsPolicy)
                                {
                                    AD.SetIsPolicy(true);
                                }

                                AD.Save(userID, false, 0);
                            }

                            QA.AddDriver(AD);
                            Session["TaskControl"] = QA;
                            this.ReassignDrivers(AD);
                            QA = (TaskControl.QuoteAuto)
                                (TaskControl.QuoteAuto)
                                Session["TaskControl"];

                            fillDataGrid(QA.Drivers);

                            this.dgDriverList.SelectedIndex =
                                this.dgDriverList.Items.Count - 1;

                            ViewState.Add("Status", "NEW");
                            btnSave.Visible = false;
                            this.SetControlState((int)States.REST);
                        }
                        else
                        {
                            if (prospectOffered.Value == "0")
                            {
                                this.SetControlState((int)States.NEW);
                            }
                            else
                            {
                                this.SetControlState((int)States.READWRITE);
                            }
                        }
                        break;
                    default:
                        break;
                }

              
                if (QA.IsPolicy)
                {QA.Mode = 1; }
                else
                {QA.Mode = 4;}



                Session["TaskControl"] = QA;
                Session["Driver"] = AD;
                ViewState["InternalDriverID"] = AD.InternalID.ToString();
                this.SetControlState((int)States.READONLY);
            }
            this.SetTaskControlIDLabel();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (ViewState["Status"].ToString().Trim() != "NEW")
            {
                TaskControl.QuoteAuto autoQuote = null;
                AutoDriver AD = null;

                if (Session["TaskControl"] != null)
                    autoQuote = (TaskControl.QuoteAuto)Session["TaskControl"];

                if (autoQuote != null && ViewState["InternalDriverID"] != null)
                {
                    AD = (AutoDriver)autoQuote.Drivers[
                        int.Parse(
                        ViewState["InternalDriverID"].ToString().Trim()) - 1];
                }
                else if (Session["Driver"] != null)
                {
                    AD = (AutoDriver)Session["Driver"];
                }

                if (AD != null)
                {
                    this.FillDriver(AD);
                }

                this.SetControlState((int)States.READONLY);
            }
            else
            {
                this.clearFields();
                this.SetControlState((int)States.REST);
            }
        }
        protected void btnVehicles_Click(object sender, EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];
            if (QA.IsPolicy)
            {
                Response.Redirect("QuoteAutoVehicles.aspx");
            }
            else
            {
                if (QA.AutoCovers.Count == 0)
                {
                    Response.Redirect("ExpressAutoQuote.aspx");
                }
                else
                {
                    Response.Redirect("QuoteAutoVehicles.aspx");
                }
            }
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            TaskControl.QuoteAuto QA =
    (TaskControl.QuoteAuto)Session["TaskControl"];

            

            AutoDriver AD = null;

            if (ViewState["DriverID"] != null)
            {
                if (QA.IsPolicy)
                {
                    AD = AutoDriver.GetAutoDriverForPolicy(
                        int.Parse(ViewState["DriverID"].ToString().Trim()));
                }
                else
                {
                    AD = AutoDriver.GetAutoDriver(
                        int.Parse(ViewState["DriverID"].ToString().Trim()));
                }
            }
            else if (Session["Driver"] != null)
            {
                AD = (AutoDriver)Session["Driver"];
            }

            if (ViewState["InternalDriverID"] != null)
            {
                if (int.Parse(
                    ViewState["InternalDriverID"].ToString().Trim()) == 1)
                {
                    this.SetControlState((int)States.READWRITEMAIN);
                }
                else
                {
                    this.SetControlState((int)States.READWRITE);
                }
            }

            //RPR 2004-03-19
            if (ViewState["Status"].ToString() == "MAINPROSPECT")
            {
                this.txtFirstNm.Enabled = false;
                this.txtLastNm1.Enabled = false;
                this.txtLastNm2.Enabled = false;
            }
            else
            {
                ViewState["Status"] = "EDIT";
            }

            //RPR 2004-03-19
            if (AD != null)
                AD.Mode = 2; //UPDATE
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto)Session["TaskControl"];

            if (QA.IsPolicy)
            {
                Response.Redirect("QuoteAuto.aspx");
            }
            else
            {
                Response.Redirect("ExpressAutoQuote.aspx");
            }
        }

        
        protected void dgDriverList_ItemCreated1(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                e.Item.ItemType == ListItemType.AlternatingItem ||
                e.Item.ItemType == ListItemType.EditItem)
            {
                TableCell tableCell = new TableCell();
                tableCell = e.Item.Cells[13]; //13
                Button button = new Button();
                button = (Button)tableCell.Controls[0];
                button.Attributes.Add("onclick",
                    "return confirm( " +
                    "\"Are you sure you want to delete this driver?\")");
            }

        }


    



        protected void dgDriverList_ItemCommand(object source, DataGridCommandEventArgs e)
        {
                      
            // 0  btn
            // 1  firstName
            // 2  lastName1
            // 3  lastName2
            // 4  birthDate
            // 5  gender
            // 6  maritalStatus
            // 7  license
            // 8  ssn
            // 9  ProspectID
            // 10 QuotesDriversID
            // 11 GenderID
            // 12 MaritalStatusID
            // 13 Remove Btn
            // 14 HomePhone
            // 15 WorkPhone
            // 16 Cellular
            // 17 InternalDriverID
            if (e.CommandName == "Select") // Select
            {
                this.SelectDriver(e);
            }
            else if (e.CommandName == "Remove") // Remove
            {
                TaskControl.QuoteAuto QA =
                    (TaskControl.QuoteAuto)Session["TaskControl"];
                int ProspectID = int.Parse(e.Item.Cells[9].Text);
                int DriverID = int.Parse(e.Item.Cells[10].Text);
                string SocialSecurity = e.Item.Cells[8].Text;

                int InternalDriverID = int.Parse(e.Item.Cells[17].Text);

                AutoDriver S = new AutoDriver();
                S.ProspectID = ProspectID;
                S.DriverID = DriverID;
                S.InternalID = InternalDriverID;
                //S.SocialSecurity = SocialSecurity;

                AutoDriver AD = QA.GetDriver(S);
                if (ValidateDelete(AD))
                {
                    fillDataGrid(QA.Drivers);

                    if (ViewState["InternalDriverID"] != null &&
                        ViewState["InternalDriverID"].ToString() ==
                        AD.InternalID.ToString())
                    {
                        this.clearFields();
                        this.SetControlState((int)States.REST);
                    }
                    Login.Login cp = HttpContext.Current.User as Login.Login;
                    int userID = 0;

                    try
                    {
                        userID =
                            int.Parse(
                            cp.Identity.Name.Split("|".ToCharArray())[1]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(
                            "Could not parse user id from cp.Identity.Name.",
                            ex);
                    }
                    AD.Mode = (int)Enumerators.Modes.Delete;

                    if (QA.IsPolicy)
                    {
                        QA.Save(userID, null, AD, false);
                    }
                    else
                    {
                        QA.Save(userID);
                    }

                    QA.RemoveDriver(AD);
                    Session["TaskControl"] = QA;
                    fillDataGrid(QA.Drivers);
                    //:~
                }
                else
                {
                    // Show message that Driver is assigned

                    if (QA.IsPolicy)
                    {
                        string tmpMesg = "";
                        if (AD.ProspectID == QA.Prospect.ProspectID)
                            tmpMesg = "This Driver is the Main of the Policy." +
                                "\\nIt can't be deleted.";
                        else
                            tmpMesg = "Driver is currently assigned to a vehicle." +
                                "\\nTo delete the driver you must un-assign it.";
                        litPopUp.Text = Utilities.MakeLiteralPopUpString(tmpMesg);
                        litPopUp.Visible = true;
                    }
                    else
                    {

                        string tmpMesg = "";
                        if (AD.ProspectID == QA.Prospect.ProspectID)
                            tmpMesg = "This Driver is the Main Prospect of the Quote." +
                                "\\nIt can't be deleted.";
                        else
                            tmpMesg = "Driver is currently assigned to a vehicle." +
                                "\\nTo delete the driver you must un-assign it.";
                        litPopUp.Text = Utilities.MakeLiteralPopUpString(tmpMesg);
                        litPopUp.Visible = true;
                    }
                }
            }
        }

        protected void SaveAutoDriverLicense(int ProspectId, string License)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("ProspectId", SqlDbType.Int, 0, ProspectId.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("License", SqlDbType.Int, 0, License, ref cookItems);

            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();

            exec.GetQuery("SaveAutoDriverLicense", xmlDoc);

            return;

        }
}
}
