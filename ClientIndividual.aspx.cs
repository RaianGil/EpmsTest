using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
//using CalendarButton;
using EPolicy.Customer;
using EPolicy;
using Baldrich.DBRequest;
using OPPReport;
using EPolicy.XmlCooker;
using System.Xml;
using System.Collections.Generic;
using EPolicy2.Reports;
using System.Web.Security;
using EPolicy.LookupTables;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Text;
using AjaxControlToolkit;

namespace EPolicy
{
	public partial class ClientIndividual : System.Web.UI.Page
	{
		protected string today = DateTime.Now.ToString("MM/dd/yyyy");
		protected System.Web.UI.WebControls.Label lblHomeUrb1;
		//protected System.Web.UI.WebControls.Label Label3;

        static string _CustomerNo = "0";
        static string _UserID  = "";

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

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
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
                if (!cp.IsInRole("CUSTOMER MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
				{
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    Response.Cookies.Add(authCookies);
                    FormsAuthentication.SignOut();
                    Response.Redirect("Default.aspx?001");
				}
			}

            if (cp.IsInRole("ADMINISTRATOR"))
            {
                btnAuditTrail.Visible = false;
            }

			if (!cp.IsInRole("CUSTOMER FLAGS"))	
			{			
				ShowFlags("hidden");			
			}			
			else			
			{			
				ShowFlags("");			
			}

            _UserID = cp.Identity.Name.Split("|".ToCharArray())[1];
			
			Customer.Customer customer1 = (Customer.Customer)Session["Customer"];
            if (customer1.CustomerNo == "")
                customer1.CustomerNo = "0";

            if (!cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("AUTO VI ADMINISTRATOR") && !cp.IsInRole("AUTO VI AGENCY") && !cp.IsInRole("GUARDIAN CENTRAL OFFICE"))
            {
                ShowCustomerbyGroupAgent(int.Parse(_UserID), int.Parse(customer1.CustomerNo));
            }

            if (!cp.IsInRole("ADMINISTRATOR"))
            {
                if (!IsPostBack)
                {

                    if (!cp.IsInRole("GUARDIAN XTRA"))
                    {
                        ddlNewApplication.Items.RemoveAt(ddlNewApplication.Items.IndexOf(ddlNewApplication.Items.FindByText("NEW GUARDIANXTRA")));
                    }

                    if (!cp.IsInRole("AUTO VI"))
                    {
                        ddlNewApplication.Items.RemoveAt(ddlNewApplication.Items.IndexOf(ddlNewApplication.Items.FindByText("NEW AUTO VI")));
                    }

                    if (!cp.IsInRole("HOME OWNERS"))
                    {
                        ddlNewApplication.Items.RemoveAt(ddlNewApplication.Items.IndexOf(ddlNewApplication.Items.FindByText("NEW RESIDENTIAL PROPERTY")));
                    }

                    if (!cp.IsInRole("GUARIDANROADASSISTANCE"))
                    {
                        ddlNewApplication.Items.RemoveAt(ddlNewApplication.Items.IndexOf(ddlNewApplication.Items.FindByText("NEW ROAD ASSIST")));
                    }
                    if (!cp.IsInRole("BONDS"))
                    {
                        ddlNewApplication.Items.RemoveAt(ddlNewApplication.Items.IndexOf(ddlNewApplication.Items.FindByText("NEW BOND")));
                    }
                    if (!cp.IsInRole("RES"))
                    {
                        ddlNewApplication.Items.RemoveAt(ddlNewApplication.Items.IndexOf(ddlNewApplication.Items.FindByText("NEW RES")));
                    }
                    if (!cp.IsInRole("YACHT"))
                    {
                        ddlNewApplication.Items.RemoveAt(ddlNewApplication.Items.IndexOf(ddlNewApplication.Items.FindByText("NEW ")));
                    }  

                }
            }

			txtBirthdate.Attributes.Add("onchange","getAge()");
			//btnCalendar.Attributes.Add("onblur","getAge()");

			this.litPopUp.Visible = false;

            Control Banner = new Control();
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.phTopBanner.Controls.Add(Banner);

            //if (Session["AddDocumentOpen"] == null)
            //{
            //    var uc = (UserControl)Page.LoadControl("~/AddDocuments.ascx");
            //    Panel1.Controls.Add(uc);
            //}
            //else
            //{
            //    var uc = (UserControl)Page.LoadControl("~/AddDocuments.ascx");
            //    Panel1.Controls.Add(uc);
            //    if (Session["AddDocumentMessage"] != null)
            //    {
            //        try
            //        {
            //            throw new Exception(Session["AddDocumentMessage"].ToString());
            //        }
            //        catch (Exception exp)
            //        {
            //            lblRecHeader.Text = exp.Message;
            //            mpeSeleccion.Show();
            //        }
            //    }
            //    else
            //    {
            //        ModalPopupExtender1.Show();
            //    }
            //}

                    if (Session["LookUpTables"] == null)
                    {
                        //Load DownDropList	
                        DataTable dtMaritalStatus = LookupTables.LookupTables.GetTable("MaritalStatus");
                        DataTable dtMasterCustomer = LookupTables.LookupTables.GetTable("MasterCustomer");
                        DataTable dtOccupations = LookupTables.LookupTables.GetTable("Occupations");
                        DataTable dtHouseHoldIncome = LookupTables.LookupTables.GetTable("HouseHoldIncome");
                        DataTable dtLocation = LookupTables.LookupTables.GetTable("Location");
                        DataTable dtRelatedTo = LookupTables.LookupTables.GetTable("RelatedTo");
                        DataTable dtGender = LookupTables.LookupTables.GetTable("Gender");
                        DataTable dtPolicyClass = EPolicy.LookupTables.LookupTables.GetTable("PolicyClass");

                        //MaritalStatus
                        ddlMaritalStatus.DataSource = dtMaritalStatus;
                        ddlMaritalStatus.DataTextField = "MaritalStatusDesc";
                        ddlMaritalStatus.DataValueField = "MaritalStatusID";
                        ddlMaritalStatus.DataBind();
                        ddlMaritalStatus.SelectedIndex = -1;
                        ddlMaritalStatus.Items.Insert(0, "");


                        //PolicyClass
                        ddlPolicyClass.DataSource = dtPolicyClass;
                        ddlPolicyClass.DataTextField = "PolicyClassDesc";
                        ddlPolicyClass.DataValueField = "PolicyClassID";
                        ddlPolicyClass.DataBind();
                        ddlPolicyClass.SelectedIndex = -1;
                        ddlPolicyClass.Items.Insert(0, "");

                        //Occupations
                        ddlOccupation.DataSource = dtOccupations;
                        ddlOccupation.DataTextField = "OccupationsDesc";
                        ddlOccupation.DataValueField = "OccupationsID";
                        ddlOccupation.DataBind();
                        ddlOccupation.SelectedIndex = -1;
                        ddlOccupation.Items.Insert(0, "");

                        //MasterCustomer
                        ddlMasterCustomer.DataSource = dtMasterCustomer;
                        ddlMasterCustomer.DataTextField = "MasterCustomerDesc";
                        ddlMasterCustomer.DataValueField = "MasterCustomerID";
                        ddlMasterCustomer.DataBind();
                        ddlMasterCustomer.SelectedIndex = -1;
                        ddlMasterCustomer.Items.Insert(0, "");

                        //HouseHoldIncome
                        ddlHouseIncome.DataSource = dtHouseHoldIncome;
                        ddlHouseIncome.DataTextField = "HouseholdIncomeDesc";
                        ddlHouseIncome.DataValueField = "HouseholdIncomeID";
                        ddlHouseIncome.DataBind();
                        ddlHouseIncome.SelectedIndex = -1;
                        ddlHouseIncome.Items.Insert(0, "");


                        if (Session["QuoteAuto"] != null)
                        {
                            lblLastName2.Text = "Last Name 2";
                        //Location (Originated At)
                            ddlOriginatedAt.DataSource = dtLocation;
                            ddlOriginatedAt.DataTextField = "LocationDesc";
                            ddlOriginatedAt.DataValueField = "LocationID";
                            ddlOriginatedAt.DataBind();
                            //ddlOriginatedAt.Items.RemoveAt(ddlOriginatedAt.Items.IndexOf(ddlOriginatedAt.Items.FindByText("ST THOMAS OFFICE")));
                            //ddlOriginatedAt.Items.RemoveAt(ddlOriginatedAt.Items.IndexOf(ddlOriginatedAt.Items.FindByText("PUERTO RICO OFFICE")));
                            //ddlOriginatedAt.Items.RemoveAt(ddlOriginatedAt.Items.IndexOf(ddlOriginatedAt.Items.FindByText("GUARDIAN - HAVENSIGHT")));
                            //ddlOriginatedAt.Items.RemoveAt(ddlOriginatedAt.Items.IndexOf(ddlOriginatedAt.Items.FindByText("GUARDIAN - LOCKHART GARDENS")));
                            //ddlOriginatedAt.Items.RemoveAt(ddlOriginatedAt.Items.IndexOf(ddlOriginatedAt.Items.FindByText("GUARDIAN - RED HOOK")));
                            //ddlOriginatedAt.Items.RemoveAt(ddlOriginatedAt.Items.IndexOf(ddlOriginatedAt.Items.FindByText("GUARDIAN - TUTU PARK")));
                            for (int i = ddlOriginatedAt.Items.Count - 1; i >= 0; i--)
                            {
                                if (ddlOriginatedAt.Items[i].Text.Trim() != "CENTRAL OFFICE")
                                {
                                    ddlOriginatedAt.Items.RemoveAt(i);
                                }
                            }
                            //ddlOriginatedAt.SelectedIndex = -1;
                            //ddlOriginatedAt.Items.Insert(0, "");


                            //aqui .ForeColor = Color.Red;

                            lblTypeAddress1.ForeColor = Color.Red; 
                            lblHomeUrb.ForeColor = Color.Red; 
                            lblAddress1.ForeColor = Color.Red; 
                            lblCity.ForeColor = Color.Red; 
                            lblState.ForeColor = Color.Red; 
                            lblZipCode.ForeColor = Color.Red; 

                            LblTypeAddress2.ForeColor = Color.Red;
                            Label9.ForeColor = Color.Red;
                            Label17.ForeColor = Color.Red;
                            Label10.ForeColor = Color.Red;
                            Label11.ForeColor = Color.Red;
                            Label12.ForeColor = Color.Red;

                            lblEmail.ForeColor = Color.Red;
                            Label8.ForeColor = Color.Red;

                        }
                        else
                        {

                            //Location (Originated At)
                            ddlOriginatedAt.DataSource = dtLocation;
                            ddlOriginatedAt.DataTextField = "LocationDesc";
                            ddlOriginatedAt.DataValueField = "LocationID";
                            ddlOriginatedAt.DataBind();
                            //ddlOriginatedAt.SelectedIndex = -1;
                            //ddlOriginatedAt.Items.Insert(0, "");
                        }

                        //RelatedTo
                        ddlRelatedTo.DataSource = dtRelatedTo;
                        ddlRelatedTo.DataTextField = "RelatedToDesc";
                        ddlRelatedTo.DataValueField = "RelatedToID";
                        ddlRelatedTo.DataBind();
                        ddlRelatedTo.SelectedIndex = -1;
                        ddlRelatedTo.Items.Insert(0, "");

                        //Gender
                        ddlGender.DataSource = dtGender;
                        ddlGender.DataTextField = "GenderDesc";
                        ddlGender.DataValueField = "GenderID";
                        ddlGender.DataBind();
                        ddlGender.SelectedIndex = -1;
                        ddlGender.Items.Insert(0, "");
                        Session.Add("LookUpTables", "In");
                   }

                if (Session["AutoPostBack"] == null)
                {
                    if (!IsPostBack)
                    {

                     MyAccordion.SelectedIndex = 0;
                     Accordion1.SelectedIndex = -1;
                     Accordion2.SelectedIndex = -1;
                     Accordion3.SelectedIndex = -1;
                     Accordion4.SelectedIndex = -1;

					if(Session["Customer"] != null)
					{
						Customer.Customer customer = (Customer.Customer) Session["Customer"];

						switch(customer.Mode)
						{
							case 1: //ADD
								FillTextControl();
                                CommentsFlagsVisibleState();
								EnableControls();
								btnNewApplication.Visible = false;
								ddlNewApplication.Visible = false;
                                btnNewBond.Visible = false;
								btnNew.Visible = false;
								DisableAutomaticCustNo();
								break;
							
							case 2: //UPDATE
								FillTextControl();
                                //CommentsFlagsVisibleState();
								EnableControls();
								break;

							default: //UPDATE
								if (customer.NavegationCustomerTable == null)
								{
									FillTextControl();
								}
								else
								{
									NavegationCustomers("");
								}
                                //CommentsFlagsVisibleState();
								DisableControls();
								break;
						}
					}
				}
				else
				{
					if(Session["Customer"] != null)
					{
						Customer.Customer customer = (Customer.Customer) Session["Customer"];
						if(customer.Mode == 4)
						{
                            //CommentsFlagsVisibleState();
                            FillTextControl();
                            DisableControls();
						}
					}
				}
			}
			else
			{
				if(Session["AutoPostBack"] == null)
				{
					NavegationCustomers("");
				}

				FillTextControl();
                //CommentsFlagsVisibleState();
				EnableControls();
				Session.Remove("AutoPostBack");
			}
		}

        protected override void LoadViewState(object savedState)
        {
            string ctrlName = Page.Request.Params.Get("__EVENTTARGET");
            if (!ctrlName.Contains("Button1"))
            {
                base.LoadViewState(savedState);
                if (ViewState["controsladded"] == null)
                    LoadControl();
            }
        }
        protected void btnTest_Click(object sender, EventArgs e)
        {
            LoadControl();
        }


        private void LoadControl()
        {
            UserControl uc = (UserControl)LoadControl("~/AddDocuments.ascx");
            pnlAlert.Controls.Add(uc);
            ViewState["controsladded"] = true;
        }

		private void FillTextControl()
		{
            Login.Login cp = HttpContext.Current.User as Login.Login;
            int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            Customer.Customer customer = (Customer.Customer) Session["Customer"];

          

			//MaritalStatus
			ddlMaritalStatus.SelectedIndex = 0;
			if(customer.MaritalStatus != 0)
			{
				for(int i = 0;ddlMaritalStatus.Items.Count-1 >= i ;i++)
				{
					if (ddlMaritalStatus.Items[i].Value == customer.MaritalStatus.ToString())
					{
						ddlMaritalStatus.SelectedIndex = i;
						i = ddlMaritalStatus.Items.Count-1;
					}
				}
			}
				
			//Occupations
			ddlOccupation.SelectedIndex = 0;
			if(customer.OccupationID != 0)
			{
				for(int i = 0;ddlOccupation.Items.Count-1 >= i ;i++)
				{
					if(ddlOccupation.Items[i].Value == customer.OccupationID.ToString())
					{
						ddlOccupation.SelectedIndex = i;
						i = ddlOccupation.Items.Count-1;
					}
				}

				if(customer.OccupationID == 40) //Others
				{
					txtOtherOccupation.Visible  = true;
					txtOtherOccupation.Text		= customer.Occupation;
				}
				else
				{
					txtOtherOccupation.Visible  = false;
					txtOtherOccupation.Text		= "";
				}
			}

			//MasterCustomer
			ddlMasterCustomer.SelectedIndex = 0;
			if(customer.MasterCustomer != 0)
			{
				for(int i = 0;ddlMasterCustomer.Items.Count-1 >= i ;i++)
				{
					if (ddlMasterCustomer.Items[i].Value == customer.MasterCustomer.ToString())
					{
						ddlMasterCustomer.SelectedIndex = i;
						i = ddlMasterCustomer.Items.Count-1;
					}
				}
			}

			//HouseHoldIncome
			ddlHouseIncome.SelectedIndex = 0;
			if(customer.HouseHoldIncome != 0)
			{
				for(int i = 0;ddlHouseIncome.Items.Count-1 >= i ;i++)
				{
					if (ddlHouseIncome.Items[i].Value == customer.HouseHoldIncome.ToString())
					{
						ddlHouseIncome.SelectedIndex = i;
						i = ddlHouseIncome.Items.Count-1;
					}
				}
			}

			//Location (Originated At)
			//ddlOriginatedAt.SelectedIndex = -1;
			if(customer.LocationID != 0)
			{
				for(int i = 0;ddlOriginatedAt.Items.Count-1 >= i ;i++)
				{
					if (ddlOriginatedAt.Items[i].Value == customer.LocationID.ToString())
					{
						ddlOriginatedAt.SelectedIndex = i;
						i = ddlOriginatedAt.Items.Count-1;
					}
				}				
			}

			//RelatedTo
			ddlRelatedTo.SelectedIndex = 0;
			if(customer.RelatedToID != 0)
			{
				for(int i = 0;ddlRelatedTo.Items.Count-1 >= i ;i++)
				{
					if (ddlRelatedTo.Items[i].Value == customer.RelatedToID.ToString())
					{
						ddlRelatedTo.SelectedIndex = i;
						i = ddlRelatedTo.Items.Count-1;
					}
				}
			}

					
			int mSex =0;
			if (customer.Sex !="")
			{
				if (customer.Sex == "M")
				{
					mSex = 1;
				}
				else
				{
					mSex = 2;
				}
			}
			else
			{
				mSex = 0;
			}

			//Gender
			ddlGender.SelectedIndex = 0;
			if(mSex != 0)
			{
				for(int i = 0;ddlGender.Items.Count-1 >= i ;i++)
				{
					if (ddlGender.Items[i].Value == mSex.ToString())
					{
						ddlGender.SelectedIndex = i;
						i = ddlGender.Items.Count-1;
					}
				}
			}

			if (customer.NoticeCancellation)
			{
				ChkNoticeOfCancellation.Checked = true;
			}
			else
			{
				ChkNoticeOfCancellation.Checked = false;
			}
			
			ChkOptOut.Checked		= customer.OptOut;

			if(customer.ProspectID != 0)
			{
				LblProspectID.Text		= "Prospect: " + customer.ProspectID.ToString();
			}
			else
			{
				LblProspectID.Text		= "Prospect: None";
			}

            _CustomerNo = customer.CustomerNo;

			lblCustNumber.Text		= customer.CustomerNo;
			TxtFirstName.Text		= customer.FirstName;
			TxtInitial.Text			= customer.Initial;
			txtLastname1.Text		= customer.LastName1;
			txtLastname2.Text		= customer.LastName2;
	 					
			bool skip = false;
  
			if (LoadVerifyAddress())
			{
				skip = true;
			}
	
			if (skip==false)
			{
				customer = (Customer.Customer) Session["Customer"];
				txtHomeUrb1.Text		= customer.Address1.Trim();
				txtAddress1.Text     	= customer.Address2.Trim();
				txtCity.Text			= customer.City.Trim();
				txtState.Text			= customer.State.Trim();
				txtZipCode.Text			= customer.ZipCode.Trim();
			}

			txtAddress1Phys.Text	= customer.AddressPhysical1.Trim();
			txtAddress2Phys.Text	= customer.AddressPhysical2.Trim();
			txtCityPhys.Text    = customer.CityPhysical.Trim();
			txtStatePhys.Text   = customer.StatePhysical.Trim();
			txtZipCodePhys.Text		= customer.ZipPhysical.Trim();

			if(txtAddress2Phys.Text.Trim() == txtAddress1.Text.Trim() && (txtAddress2Phys.Text.Trim() != "" && txtAddress1.Text.Trim() != ""))
			{
				chkSameAddr.Checked = true;
			}
			else
			{
				chkSameAddr.Checked = false;
			}

			//txtSocialSecurity.Text	= customer.SocialSecurity;


            EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();
            if (customer.SocialSecurity.Trim() != "")
            {
                txtSocialSecurity.Text = encrypt.Decrypt(customer.SocialSecurity);
                txtSocialSecurity.Text = new string('*', txtSocialSecurity.Text.Trim().Length - 4) + txtSocialSecurity.Text.Trim().Substring(txtSocialSecurity.Text.Trim().Length - 4);
                MaskedEditExtender1.Mask = "???-??-9999";
            }
            else
                txtSocialSecurity.Text = "";
					 
			txtBirthdate.Text		= customer.Birthday;
			TxtAge.Text				= customer.Age;
			txtWorkName.Text		= customer.EmplName;
			TxtWorkCity.Text		= customer.EmplCity;
			txtHomePhone.Text		= customer.HomePhone;
			txtWorkPhone.Text		= customer.JobPhone;
			TxtCellular.Text		= customer.Cellular;
			txtEmail.Text			= customer.Email;
			txtComments.Text		= customer.Comments;
			TxtLicence.Text			= customer.Licence;

            
            DataTable dt = null;

            string UserAgent = "1";

            DataTable dtAgentByUserID = EPolicy.Customer.Customer.GetAgentByUserID(_UserID);

            if (dtAgentByUserID.Rows.Count > 0)
            {

                if (dtAgentByUserID.Rows[0]["AgentID"].ToString().Trim() != "")
                {
                    UserAgent = dtAgentByUserID.Rows[0]["AgentID"].ToString().Trim();
                }

            }

            //Esto permite al usuario ver el agente y no esconde el campo
            if (cp.IsInRole("AUTO VI AGENCY"))
            {
                UserAgent = "1";
            }

            dt = EPolicy.Customer.Customer.GetAgentByCustomer(_CustomerNo, UserAgent);		

			if (dt.Rows.Count > 0)
			{			
				txtAgent.Text = dt.Rows[0]["Agentdesc"].ToString();			
			}			
			else			
			{			
				txtAgent.Text = "NO TIENE AGENTE!";			
			}
           

            if (customer.CustomerNo != "")
            {
                FillFlags();
            }

            FillCommentsGrid();
            FillGridDocuments();
            FillClaimsGrid();

        }
		
		public void AgentView()			
		{			
			DataTable dt = null;			
			
            string UserAgent = "";

			Login.Login cp = HttpContext.Current.User as Login.Login;			
			
			TaskControl.Policy taskControl = (TaskControl.Policy)Session["TaskControl"];	
		
			DataTable dtAgentByUserID = EPolicy.Customer.Customer.GetAgentByUserID(_UserID);	

            if (dtAgentByUserID.Rows.Count > 0)
            {
                
                if(dtAgentByUserID.Rows[0]["AgentID"].ToString().Trim() != "")
                {
                    UserAgent = dtAgentByUserID.Rows[0]["AgentID"].ToString().Trim();
                }

            }

            dt = EPolicy.Customer.Customer.GetAgentByCustomer(_CustomerNo, UserAgent);			
			
					
			// Admin privilage
            if (cp.IsInRole("AUTO VI ADMINISTRATOR") || cp.IsInRole("AUTO VI AGENCY")) //dtAgentByUserID.Rows[0]["AgentDesc"].ToString().Trim() == "NO AGENT" && )			
            {
                btnEdit.Visible = true;
                btnNewApplication.Visible = true;
                ddlNewApplication.Visible = true;
                btnAuditTrail.Visible = false;
                BtnViewTask.Visible = true;
                btnAdjuntar.Visible = true;
            }
            else if (cp.IsInRole("HOME OWNERS"))
            {
                btnEdit.Visible = true;
                btnAuditTrail.Visible = false;
                BtnViewTask.Visible = true;
                btnAdjuntar.Visible = true;
                btnNewBond.Visible = false;
                btnNewApplication.Visible = true;
                ddlNewApplication.Visible = true;
            }
            else if (cp.IsInRole("BONDS")) 	
            {
                btnEdit.Visible = true;
                btnNewBond.Visible = false;
                btnAuditTrail.Visible = false;
                BtnViewTask.Visible = true;
                btnAdjuntar.Visible = true;
                btnNewBond.Visible = false;
            }

            else if (cp.IsInRole("YACHT"))
            {
                btnEdit.Visible = true;
                btnNewBond.Visible = false;
                btnAuditTrail.Visible = false;
                BtnViewTask.Visible = true;
                btnAdjuntar.Visible = true;
                btnNewBond.Visible = false;
            }
            //If user has an agent and customer has agent
            else if (dtAgentByUserID.Rows.Count > 0 && dt.Rows.Count > 0)
            {
                //Has the same agent as the customer
                if (dtAgentByUserID.Rows[0]["AgentDesc"].ToString().Trim() == dt.Rows[0]["AgentDesc"].ToString().Trim())
                {
                    btnAdjuntar.Visible = true;
                    btnEdit.Visible = true;
                    btnNewApplication.Visible = true;
                    ddlNewApplication.Visible = true;
                }
                // Doesn't have the same agent (Can't Modify or edit)
                else
                {
                    btnAdjuntar.Visible = false;
                    BtnViewTask.Visible = false;
                    btnEdit.Visible = false;
                    btnNewApplication.Visible = true;
                    ddlNewApplication.Visible = true;
                }

            }
            else if (dt.Rows.Count == 0)
            {
                    btnAdjuntar.Visible = false;
                    BtnViewTask.Visible = false;
                    btnEdit.Visible = false;
                    btnNewApplication.Visible = true;
                    ddlNewApplication.Visible = true;
            }
		}


		protected void ddlOccupation_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(ddlOccupation.SelectedItem.Value == "40") //Others
			{
				txtOtherOccupation.Visible  = true;
				txtOtherOccupation.Text		= "";
			}
			else
			{
				txtOtherOccupation.Visible  = false;
				txtOtherOccupation.Text		= "";
			}
		}

		private void UpdatedProspectData(Customer.Prospect prospect, Customer.Customer customer, int userID)
		{
			//Actualizo la data del prospecto.
			prospect.ConvertToClient = DateTime.Now;
			prospect.FirstName	= customer.FirstName;
			prospect.LastName1	= customer.LastName1;
			prospect.LastName2	= customer.LastName2;
			prospect.WorkPhone	= customer.JobPhone;
			prospect.HomePhone	= customer.HomePhone;
			prospect.Cellular	= customer.Cellular;
			prospect.Email		= customer.Email;

			prospect.SaveProspect(userID);
		}

		private void FillProperties()
		{
			Customer.Customer customer = (Customer.Customer) Session["Customer"];

			//Asigna el customer no. manualmente.
			if (ChkDisableAutomaticCustNo.Checked)
			{
				customer.DisableAutomaticCustNo = ChkDisableAutomaticCustNo.Checked;
				customer.CustomerNo			= TxtCustomerNo.Text.Trim().ToUpper();
			}
			else
			{
				customer.DisableAutomaticCustNo = ChkDisableAutomaticCustNo.Checked;
			}

			if(Session["Prospect"] !=null)
			{
				Customer.Prospect prospect = (Customer.Prospect) Session["Prospect"];

				customer.ProspectID		= prospect.ProspectID;
				
				Session["Prospect"]		= prospect;
			}

			customer.FirstName			= TxtFirstName.Text.Trim().ToUpper();
			customer.Initial			= TxtInitial.Text.Trim().ToUpper();
			customer.LastName1			= txtLastname1.Text.Trim().ToUpper();
			customer.LastName2			= txtLastname2.Text.Trim().ToUpper();
			customer.Address1			= txtHomeUrb1.Text.Trim().ToUpper();
			customer.Address2			= txtAddress1.Text.Trim().ToUpper();
			customer.City				= txtCity.Text.Trim().ToUpper();
			customer.State				= txtState.Text.Trim().ToUpper();
			customer.ZipCode			= txtZipCode.Text.Trim().ToUpper();
			customer.AddressPhysical1	= txtAddress1Phys.Text.Trim().ToUpper();
			customer.AddressPhysical2	= txtAddress2Phys.Text.Trim().ToUpper();
			customer.CityPhysical		= txtCityPhys.Text.Trim().ToUpper();
			customer.StatePhysical		= txtStatePhys.Text.Trim().ToUpper();
			customer.ZipPhysical		= txtZipCodePhys.Text.Trim();
			
//			switch (ddlGender.SelectedItem.Text)
//			{
//				case "MALE":
//					customer.Sex = "M";  //1
//					break;
//
//				case "FEMALE":
//					customer.Sex = "F"; //2
//					break;
//
//				case "":
//					customer.Sex = "";
//					break;
//			}

			string gen;
			if (ddlGender.SelectedItem.Value == "")
			{
				gen = "0";
			}
			else				
			{
				gen = ddlGender.SelectedItem.Value;
			}

			switch (int.Parse(gen))
			{
				case 1:
					customer.Sex = "M";  //1
					break;

				case 2:
					customer.Sex = "F"; //2
					break;

				default:
					customer.Sex = "";
					break;
			}

			//= System.DBNull.Value)?(int) customer._dtCustomer.Rows[0]["HouseHoldIncome"]:0;
			customer.OccupationID   = ddlOccupation.SelectedItem.Value != "" ? int.Parse(ddlOccupation.SelectedItem.Value):0;
			customer.Occupation		= txtOtherOccupation.Text.Trim().ToUpper();
			customer.HouseHoldIncome= ddlHouseIncome.SelectedItem.Value != "" ?int.Parse(ddlHouseIncome.SelectedItem.Value):0;
			customer.MaritalStatus	= ddlMaritalStatus.SelectedItem.Value != "" ?int.Parse(ddlMaritalStatus.SelectedItem.Value):0;
			customer.MasterCustomer = ddlMasterCustomer.SelectedItem.Value != "" ?int.Parse(ddlMasterCustomer.SelectedItem.Value):0;
			//customer.SocialSecurity	= txtSocialSecurity.Text.Trim();

            EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();

            if (txtSocialSecurity.Text.Trim().Replace("*", "").Replace("-", "").Replace("_", "") != "")
                customer.SocialSecurity = encrypt.Encrypt(txtSocialSecurity.Text.Trim().ToUpper());
            else
                customer.SocialSecurity = "";

			customer.Birthday		= txtBirthdate.Text;
			customer.Age			= TxtAge.Text.Trim();
			customer.EmplName		= txtWorkName.Text.Trim().ToUpper();
			customer.EmplCity		= TxtWorkCity.Text.Trim().ToUpper();
			customer.HomePhone		= txtHomePhone.Text.Trim();
			customer.JobPhone		= txtWorkPhone.Text.Trim();
			customer.Cellular		= TxtCellular.Text.Trim();
			customer.Email			= txtEmail.Text.Trim().ToUpper();
			customer.Comments		= txtComments.Text.Trim().ToUpper();
			customer.LocationID		= ddlOriginatedAt.SelectedItem.Value != "" ?int.Parse(ddlOriginatedAt.SelectedItem.Value):0;	
			customer.OptOut			= ChkOptOut.Checked;
			customer.RelatedToID    = ddlRelatedTo.SelectedItem.Value != "" ? int.Parse(ddlRelatedTo.SelectedItem.Value):0;
			customer.Licence		= TxtLicence.Text.Trim();

			if (ChkNoticeOfCancellation.Checked)
			{
				customer.NoticeCancellation = true;
			}
			else
			{
				customer.NoticeCancellation = false;
			}

            //FillFlags();

			Session.Add("Customer",customer);
		}

		private bool LoadVerifyAddress()
		{
			if ((Convert.ToString(Session["Address1"])!= ""))
			{
				if(Convert.ToString(Session["ZipCode"]) != "") 
				{
					//this.txtAddress1.Text = Convert.ToString(Session["Address1"]);
					//this.txtAddress2.Text = Convert.ToString(Session["Address2"]);
					this.txtAddress1.Text = Convert.ToString(Session["Address2"]);
					this.txtAddress2.Text = Convert.ToString(Session["Address2"]);
					this.txtCity.Text     = Convert.ToString(Session["CityName"]);
					this.txtState.Text    = "PR";
					this.txtZipCode.Text  = Convert.ToString(Session["ZipCode"]);
					this.txtHomeUrb1.Text = Convert.ToString(Session["UrbName"]);
					//this.txtZipCode.Text  = oAddressLookup.MemoryZipCode + "-" + oAddressLookup.Zip4;
			
					//Session.Remove("Address1");
					Session.Remove("Address2");
					Session.Remove("CityName");
					Session.Remove("ZipCode");
					Session.Remove("AddressType");
					Session.Remove("UrbName");
				}
				return (true);
			}
			return (false);
		}

		protected void chkSameAddr_CheckedChanged(object sender, System.EventArgs e)
		{
			SameAsPostal();
		}

		private void SameAsPostal()
		{
			if(chkSameAddr.Checked == true)
			{
				txtAddress1Phys.Text	= txtHomeUrb1.Text.Trim();
				txtAddress2Phys.Text	= txtAddress1.Text.Trim(); //txtAddress2.Text.Trim();
				txtCityPhys.Text		= txtCity.Text.Trim();
				txtStatePhys.Text		= txtState.Text.Trim();
				txtZipCodePhys.Text		= txtZipCode.Text.Trim();
			}
		}

        private void CommentsFlagsVisibleState()
        {
            if (lblCustNumber.Text.ToString() == "" || lblCustNumber.Text.ToString() == "0")
            {
                //Flags Visible
                //lblFlags.Visible = false;
                chkExcludePerson.Visible = false;
                chkKeepWatch.Visible = false;
                chkFrontingPolicy.Visible = false;
                chkClaimsPolicyDisplay.Visible = false;
                chkPersonPolicyDisp.Visible = false;
                chkPolicyDisplay.Visible = false;
                chkPFCPolicyDisp.Visible = false;
                chkDoNotRenew.Visible = false;
                chkDUIPersonDisp.Visible = false;
                chkClientDisp.Visible = false;
                chkLegalNameDisp.Visible = false;
                chkEmployeeDiscount.Visible = false;


                AccordionPane3.Visible = false;
                //Flags1.Attributes["class"] = "hidden";
                //Flags2.Attributes["class"] = "hidden";
                //Flags3.Attributes["class"] = "hidden";
                //Flags4.Attributes["class"] = "hidden";
                //Flags5.Attributes["class"] = "hidden";
                //Flags6.Attributes["class"] = "hidden";
                //Flags7.Attributes["class"] = "hidden";
                //Flags8.Attributes["class"] = "hidden";


                //Comments Visible
                AccordionPane4.Visible = false;

                lblCustomerComments.Visible = false;
                txtCustomerComments.Visible = false;
                btnCustomerComments.Visible = false;
                GridComments.Visible = false;
                txtAgent.Visible = false;
                lblAgentAssigned.Visible = false;
                btnAuditTrail.Visible = false;
                BtnViewTask.Visible = false;
                btnAdjuntar.Visible = false;

                //Comments1.Attributes["class"] = "hidden";
                //Comments2.Attributes["class"] = "hidden";
                //Comments3.Attributes["class"] = "hidden";
            }
            else
            {
                ////Flags Visible
                //AccordionPane3.Visible = false;
                //lblFlags.Visible = true;
                //chkExcludePerson.Visible = true;
                //chkKeepWatch.Visible = true;
                //chkFrontingPolicy.Visible = true;
                //chkClaimsPolicyDisplay.Visible = true;

                //No se utilizan
                //chkPersonPolicyDisp.Visible = false;
                //chkPolicyDisplay.Visible = false;
                //chkClientDisp.Visible = false;
                //chkLegalNameDisp.Visible = false;

                //chkPFCPolicyDisp.Visible = true;
                //chkDoNotRenew.Visible = true;
                //chkDUIPersonDisp.Visible = true;


                //chkEmployeeDiscount.Visible = true;

                //Comments Visible
                //lblCommentsdiv.Visible = true;
                //AccordionPane4.Visible = true;

                //lblCustomerComments.Visible = true;
                //txtCustomerComments.Visible = true;
                //btnCustomerComments.Visible = true;
                //GridComments.Visible = true;
                //txtAgent.Visible = true;
                //lblAgentAssigned.Visible = true;
            }
        }
		
		private void ShowFlags(string ClassName)
        {
            if (ClassName == "hidden")
            {
                Accordion2.Visible = false;
            }
            else 
            {
                Accordion2.Visible = true;
            }
            //Flags1.Attributes["class"] = ClassName;
            //Flags2.Attributes["class"] = ClassName;
            //Flags3.Attributes["class"] = ClassName;
            //Flags4.Attributes["class"] = ClassName;
            //Flags5.Attributes["class"] = ClassName;
            //Flags6.Attributes["class"] = ClassName;
            //Flags7.Attributes["class"] = ClassName;
            //Flags8.Attributes["class"] = ClassName;
        }


        private void ShowCustomerbyGroupAgent(int UsetID, int CustomerID)
        {
            DataTable dt = null;
            dt = GetIfUserCanSeeCustomer(UsetID, CustomerID);

            if (dt.Rows.Count > 0)
            {
                //ReturnValue == 1 NO POLICY
                //ReturnValue == 2 Your POLICY
                //ReturnValue == 0 NOT YOUR POLICY
                if (dt.Rows[0]["ReturnValue"].ToString() == "2")
                {
                    

                    //Accordion2.Visible = true;
                    //DIVAddFlagsComments.Visible = true;
                    btnAuditTrail.Visible = false;
                    //BtnViewTask.Visible = false;
                }
                else if (dt.Rows[0]["ReturnValue"].ToString() == "0")
                {
                    Accordion1.Visible = false;
                    Accordion2.Visible = false;
                    Accordion4.Visible = false;
                    AccordionPane4.Visible = false;
                    //DIVAddFlagsComments.Visible = false;

                    txtEmail.Visible = false;
                    lblEmail.Visible = false;
                    txtWorkName.Visible = false;
                    lblJobName.Visible = false;
                    lblWorkcity.Visible = false;
                    TxtWorkCity.Visible = false;
                    lblAgentAssigned.Visible = false;
                    txtAgent.Visible = false;
                    lblOriginatedAt.Visible = false;
                    ddlOriginatedAt.Visible = false;
                    lblOccupation.Visible = false;
                    ddlOccupation.Visible = false;
                    lblMaritalStatus.Visible = false;
                    ddlMaritalStatus.Visible = false;
                    btnAuditTrail.Visible = false;
                }
                else 
                {
 
                }
            }
        }
        protected bool ValidateFlags()
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                if (!cp.IsInRole("ADMINISTRATOR"))// Maureen le debe aparecer el mensaje
                {
                    //if (chkExcludePerson.Checked == true || chkKeepWatch.Checked == true || chkFrontingPolicy.Checked == true || chkClaimsPolicyDisplay.Checked == true ||
                    //    chkPersonPolicyDisp.Checked == true || chkPolicyDisplay.Checked == true || chkPFCPolicyDisp.Checked == true ||
                    //    chkDoNotRenew.Checked == true || chkDUIPersonDisp.Checked == true || chkClientDisp.Checked == true ||
                    //    chkLegalNameDisp.Checked == true || chkEmployeeDiscount.Checked == true)
                    //{
                    //    lblRecHeader.Text = "Please refer this case to the Guardian Insurance Underwriting Manager.";
                    //    mpeSeleccion.Show();
                    //    return true;
                    //    //throw new Exception("Please refer this case to the Guardian Insurance Underwriting Manager.");
                    //}
                    if (chkExcludePerson.Checked == true || chkDoNotRenew.Checked == true || chkDUIPersonDisp.Checked == true || chkLegalNameDisp.Checked == true || chkClaimsPolicyDisplay.Checked == true)
                    {
                        lblRecHeader.Text = "Please refer this case to the Guardian Insurance Underwriting Manager.";
                        mpeSeleccion.Show();
                        return true;
                        //throw new Exception("Please refer this case to the Guardian Insurance Underwriting Manager.");
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
            catch (Exception xp)
            {
                lblRecHeader.Text = xp.Message;
                mpeSeleccion.Show();
                return true;

                //this.litPopUp.Text = xp.Message;
                //this.litPopUp.Visible = true;
                //return;
            }
        }

		private void EnableControls()
		{
            Login.Login cp = HttpContext.Current.User as Login.Login;
            ChkDisableAutomaticCustNo.Visible = false;
			TxtCustomerNo.Visible			  = false;

			btnEdit.Visible		  = false;
			BtnExit.Visible		  = false;
			btnProfile.Visible	  = false;
			BtnSave.Visible		  = true;
			btnCancel.Visible	  = true;
			//btnAuditTrail.Visible = false;
			//BtnViewTask.Visible   = false;
            //btnNew.Visible = false;
            btnNewApplication.Visible = true;
            ddlNewApplication.Visible = true;
            btnNewBond.Visible = false;

            btnCustomerComments.Visible = true;
			//btnCalendar.Visible   = true;
		
			ChkOptOut.Enabled		= true;
			ChkNoticeOfCancellation.Enabled = true;
			chkSameAddr.Enabled		= true;

            if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("AUTO VI ADMINISTRATOR"))
            {
                //Flags
                chkExcludePerson.Enabled = true;
                chkKeepWatch.Enabled = true;
                chkFrontingPolicy.Enabled = true;
                chkClaimsPolicyDisplay.Enabled = true;
                chkPersonPolicyDisp.Visible = false;
                chkPolicyDisplay.Visible = false;
                chkPFCPolicyDisp.Enabled = true;
                chkDoNotRenew.Enabled = true;
                chkDUIPersonDisp.Enabled = true;
                chkClientDisp.Visible = false;
                chkLegalNameDisp.Visible = false;
                chkEmployeeDiscount.Enabled = true;
            }
            else if (cp.IsInRole("AUTO VI AGENCY"))
            {
                //Flags
                chkExcludePerson.Enabled = false;
                chkKeepWatch.Enabled = false;
                chkFrontingPolicy.Enabled = false;
                chkClaimsPolicyDisplay.Enabled = false;
                chkPersonPolicyDisp.Visible = false;
                chkPolicyDisplay.Visible = false;
                chkPFCPolicyDisp.Enabled = false;
                chkDoNotRenew.Enabled = false;
                chkDUIPersonDisp.Enabled = false;
                chkClientDisp.Visible = false;
                chkLegalNameDisp.Visible = false;
                chkEmployeeDiscount.Enabled = false;
            }
            else
            {
                //Flags
                chkExcludePerson.Enabled = false;
                chkKeepWatch.Enabled = false;
                chkFrontingPolicy.Enabled = false;
                chkClaimsPolicyDisplay.Enabled = false;
                chkPersonPolicyDisp.Visible = false;
                chkPolicyDisplay.Visible = false;
                chkPFCPolicyDisp.Enabled = false;
                chkDoNotRenew.Enabled = false;
                chkDUIPersonDisp.Enabled = false;
                chkClientDisp.Visible = false;
                chkLegalNameDisp.Visible = false;
                chkEmployeeDiscount.Enabled = false;
            }
            

			ddlGender.Enabled         = true;
			ddlMaritalStatus.Enabled  = true;
			ddlOccupation.Enabled     = false;
			ddlHouseIncome.Enabled    = true;
			ddlOriginatedAt.Enabled   = true;
			ddlMasterCustomer.Enabled = true;
			ddlRelatedTo.Enabled      = true;

			txtOtherOccupation.Enabled = true;
			TxtFirstName.Enabled	= true;
			TxtInitial.Enabled		= true;
			txtLastname1.Enabled	= true;
			txtLastname2.Enabled	= true;
			txtBirthdate.Enabled	= true;
			TxtAge.Enabled			= true;
			txtComments.Enabled	    = true;
			txtHomePhone.Enabled	= true;
			txtWorkPhone.Enabled	= true;
			TxtCellular.Enabled  	= true;
			txtEmail.Enabled		= true;
			txtWorkName.Enabled	    = false;
			TxtWorkCity.Enabled	    = false;

            Customer.Customer customer = (Customer.Customer) Session["Customer"];

            //al entrar un cliente nuevo el SS puede ser añadido por cualquier usario
            //una vez añadido solo los que tengan el permiso "MODIFY SOCIAL SECURITY" podran cambiarlo 
            if (customer.Mode == 2)
            {
                txtSocialSecurity.Enabled = false;//false
            }
            else
            {
                txtSocialSecurity.Enabled = true;
            }
 
			txtHomeUrb1.Enabled	    = true;
			txtAddress1.Enabled	    = true;
			txtAddress2.Enabled	    = true;
			txtCity.Enabled		    = true;
			txtState.Enabled		= true;
			txtZipCode.Enabled		= true;
			txtAddress1Phys.Enabled = true;
			txtAddress2Phys.Enabled = true;
			txtCityPhys.Enabled	    = true;
			txtStatePhys.Enabled	= true;
			txtZipCodePhys.Enabled	= true;
			TxtLicence.Enabled		= true;
            txtCustomerComments.Enabled = true;
            //txtAgent.Enabled = true;


		}

		private void DisableControls()
		{
            Login.Login cp = HttpContext.Current.User as Login.Login;
			ChkDisableAutomaticCustNo.Visible = false;
			TxtCustomerNo.Visible			  = false;

			btnEdit.Visible		  = true;
			BtnExit.Visible		  = true;
			btnProfile.Visible	  = false;
			BtnSave.Visible		  = false;
			btnCancel.Visible	  = false;
			//btnAuditTrail.Visible = true;
			//BtnViewTask.Visible   = true;
            btnCustomerComments.Visible = false;
            btnNew.Visible = true;
            btnNewApplication.Visible = true;
            ddlNewApplication.Visible = true;
            btnNewBond.Visible = false;
			//btnCalendar.Visible   = false;

            //Flags And Comments
            //if (Accordion2.Visible == true)
            //{
            //    btnEdit.Visible = false;
            //    BtnExit.Visible = false;
            //}

            if (!cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("GUARDIAN CENTRAL OFFICE"))// && !cp.IsInRole("AUTO VI ADMINISTRATOR"))
            {
                AgentView();
            }
			
			ChkOptOut.Enabled		= false;
			ChkNoticeOfCancellation.Enabled = false;
			chkSameAddr.Enabled		= false;

            //Flags
            chkExcludePerson.Enabled = false;
            chkKeepWatch.Enabled = false;
            chkFrontingPolicy.Enabled = false;
            chkClaimsPolicyDisplay.Enabled = false;
            chkPersonPolicyDisp.Visible = false;
            chkPolicyDisplay.Visible = false;
            chkPFCPolicyDisp.Enabled = false;
            chkDoNotRenew.Enabled = false;
            chkDUIPersonDisp.Enabled = false;
            chkClientDisp.Visible = false;
            chkLegalNameDisp.Visible = false;
            chkEmployeeDiscount.Enabled = false;

			ddlGender.Enabled        = false;
			ddlMaritalStatus.Enabled = false;
			ddlOccupation.Enabled    = false;
			ddlHouseIncome.Enabled   = false;
			ddlOriginatedAt.Enabled  = false;
			ddlMasterCustomer.Enabled = false;
			ddlRelatedTo.Enabled     = false;

			txtOtherOccupation.Enabled = false;
			TxtFirstName.Enabled	= false;
			TxtInitial.Enabled		= false;
			txtLastname1.Enabled	= false;
			txtLastname2.Enabled	= false;
			txtBirthdate.Enabled	= false;
			TxtAge.Enabled			= false;
			txtComments.Enabled	    = false;
			txtHomePhone.Enabled	= false;
			txtWorkPhone.Enabled	= false;
			TxtCellular.Enabled	    = false;
			txtEmail.Enabled		= false;
			txtWorkName.Enabled	    = false;
			TxtWorkCity.Enabled	    = false;
			txtSocialSecurity.Enabled = false;

			txtHomeUrb1.Enabled	    = false;
			txtAddress1.Enabled	    = false;
			txtAddress2.Enabled	    = false;
			txtCity.Enabled		    = false;
			txtState.Enabled		= false;
			txtZipCode.Enabled		= false;
			txtAddress1Phys.Enabled = false;
			txtAddress2Phys.Enabled = false;
			txtCityPhys.Enabled	    = false;
			txtStatePhys.Enabled	= false;
			txtZipCodePhys.Enabled	= false;
			TxtLicence.Enabled		= false;
            txtCustomerComments.Enabled = false;
			txtAgent.Enabled = false;
            //lblCommentsdiv.Visible = false;
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			if(Session["Customer"] != null)
			{
				Customer.Customer customer = (Customer.Customer) Session["Customer"];
				Response.Redirect("SearchAuditItems.aspx?type=1&customerNo=" + 
					customer.CustomerNo.Trim());
			}
		}

		private void DisableAutomaticCustNo()
		{
			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
				Response.Redirect("HomePage.aspx?001");
			}
			else
			{
				if(cp.IsInRole("DISABLEAUTOMATICCUSTNO") || cp.IsInRole("ADMINISTRATOR"))
				{
					ChkDisableAutomaticCustNo.Visible = true;
				}
			}
		}

		private void ChkDisableAutomaticCustNo_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.ChkDisableAutomaticCustNo.Checked)
			{
				TxtCustomerNo.Visible			  = true;
			}
			else
			{
				TxtCustomerNo.Visible			  = false;
			}
		}

		protected void btnEdit_Click(object sender, System.EventArgs e)
		{
			if (ValidateFlags())
			return;
			Customer.Customer customer = (Customer.Customer) Session["Customer"];
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
			customer.Mode		= (int) Customer.Customer.CustomerMode.UPDATE;

			Session.Add("Customer",customer);

            //CommentsFlagsVisibleState();
			EnableControls();

            if (cp.IsInRole("MODIFY SOCIAL SECURITY"))
            {
                EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();

                txtSocialSecurity.Enabled = true;

                if (customer.SocialSecurity.Trim() != "")
                    txtSocialSecurity.Text = encrypt.Decrypt(customer.SocialSecurity);
                else
                    txtSocialSecurity.Text = "";

                MaskedEditExtender1.Mask = "999-99-9999";
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

			FillProperties();
			Customer.Customer customer = (Customer.Customer) Session["Customer"];
			try
			{
				customer.Save(userID);
                SaveFlags(_CustomerNo);
				FillTextControl();
                //CommentsFlagsVisibleState();
				DisableControls();
                
	
				if(Session["Prospect"] !=null) 
				{
					Customer.Prospect prospect = (Customer.Prospect) Session["Prospect"];
					UpdatedProspectData(prospect,customer,userID);
				}
				else
				{
					if(customer.ProspectID != 0)
					{
						Customer.Prospect prospect = new Customer.Prospect();
						prospect.GetProspect(customer.ProspectID);

						UpdatedProspectData(prospect,customer,userID);
					}
				}

                if (Session["QuoteAuto"] != null)
				{
					TaskControl.QuoteAuto taskControl = (TaskControl.QuoteAuto) Session["TaskControl"];
					taskControl.Customer = customer;
					taskControl.CustomerNo = customer.CustomerNo;
					//taskControl.Prospect.CustomerNumber = customer.CustomerNo;
					//taskControl.Mode = 2;	//Update
					//taskControl.Save(userID);
					//taskControl.Mode = (int) EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
					
					Session["TaskControl"] = taskControl;

                    Session.Remove("QuoteAuto");
                    Session.Remove("LookUpTables");
                    Response.Redirect("QuoteAuto.aspx", false);
				}

				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Customer information saved successfully." + "\r\n" + customer.Warning.Trim());
				this.litPopUp.Visible = true;
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Unable to save customer. " + exp.Message);
				this.litPopUp.Visible = true;
			}
		}

		protected void btnCancel_Click(object sender, System.EventArgs e)
		{
			Customer.Customer customer = (Customer.Customer) Session["Customer"];

			if(customer.Mode == 1) //ADD
			{
				Session.Clear();
				Response.Redirect("SearchClient.aspx");
			}
			else
			{
				customer.Mode		= (int) Customer.Customer.CustomerMode.CLEAR;
				Session["Customer"] = customer;

				Session.Remove("Address1");
				NavegationCustomers("");
                //CommentsFlagsVisibleState();
				DisableControls();
			}
		}

		protected void BtnExit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.litPopUp.Visible = false;
			Session.Clear();
			Response.Redirect("SearchClient.aspx");
		}

		protected void btnNew_Click(object sender, System.EventArgs e)
		{
			Session.Clear();

			Customer.Customer customer = new Customer.Customer();
			customer.Mode = (int) Customer.Customer.CustomerMode.ADD;
			Session.Add("Customer",customer);

			Response.Redirect("ClientIndividual.aspx");
		}

		protected void TxtFirstName_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void btnProfile_Click(object sender, System.EventArgs e)
		{
            //Customer.Customer customer = (Customer.Customer) Session["Customer"];

            //EPolicy2.Reports.CustomerProfile rpt = new EPolicy2.Reports.CustomerProfile(customer);				
            //rpt.Run(false);

            //Session.Add("Report",rpt);

            //Session.Add("FromPage","ClientIndividual.aspx");

            //Response.Redirect("ActiveXViewer.aspx",false);
		}

		protected void BtnViewTask_Click(object sender, System.EventArgs e)
		{
			if (ValidateFlags())
				return;
			Response.Redirect("ClientTasks.aspx");
		}		

		private void NavegationCustomers(string Case)
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

			Customer.Customer customer = (Customer.Customer) Session["Customer"];

			string customerNo = customer.CustomerNo;
		 
			int rec =0;

			if(customer.NavegationCustomerTable != null)
			{
				for(int i=0;i<=customer.NavegationCustomerTable.Rows.Count-1;i++)
				{
					if (customerNo.Trim() == customer.NavegationCustomerTable.Rows[i]["CustomerNo"].ToString().Trim())
					{
						if (Case == "")
						{
							rec = i+1;
							//LblRecordCount.Text = rec.ToString()+" of " + customer.NavegationCustomerTable.Rows.Count;
						}
						else if (Case == "Previous" || Case == "Begin")
						{
							rec = 1;
							if (i>0)
							{
								switch(Case)
								{
									case "Previous":
										customerNo = customer.NavegationCustomerTable.Rows[i-1]["CustomerNo"].ToString();
										rec = i;
										break;
									case "Begin":
										customerNo = customer.NavegationCustomerTable.Rows[0]["CustomerNo"].ToString();
										rec = 1;
										break;
								}
								//LblRecordCount.Text = rec.ToString()+" of " + customer.NavegationCustomerTable.Rows.Count;
								i = customer.NavegationCustomerTable.Rows.Count-1; // Para salir del loop.
							}
							else
							{
								//MessageBox.Show(this,"Beginning of the records selected","Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);				
							}
						}
						else if (Case == "Next" || Case == "End")
						{
							if (customer.NavegationCustomerTable.Rows.Count-1>i)
							{					
								switch(Case)
								{
									case "Next":
										customerNo = customer.NavegationCustomerTable.Rows[i+1]["CustomerNo"].ToString();
										rec = i+2;
										break;
									case "End":
										customerNo = customer.NavegationCustomerTable.Rows[customer.NavegationCustomerTable.Rows.Count-1]["CustomerNo"].ToString();
										rec = customer.NavegationCustomerTable.Rows.Count;
										break;
								}
								//LblRecordCount.Text = rec.ToString()+" of " + customer.NavegationCustomerTable.Rows.Count;
								i = customer.NavegationCustomerTable.Rows.Count-1; // Para salir del loop.
							}
							else
							{
								//MessageBox.Show(this,"End of the records selected","Warning",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
							}
						}
					}
				}																
		
				DataTable dtCustomer = customer.NavegationCustomerTable;
				int mode  = customer.Mode;

				customer  = Customer.Customer.GetCustomer(customerNo, userID);
			
				customer.Mode = mode;
				customer.NavegationCustomerTable = dtCustomer;

				Session.Add("Customer",customer);

				FillTextControl();
			}
			else
			{
				customer  = Customer.Customer.GetCustomer(customerNo, userID);	
				Session.Add("Customer",customer);
				FillTextControl();
			}
		}

		protected void BtnExit_Click(object sender, System.EventArgs e)
		{
            if (Session["QuoteAuto"] != null)
            {
                this.litPopUp.Visible = false;
                Session.Remove("QuoteAuto");
                Session.Remove("LookUpTables");
                Response.Redirect("QuoteAuto.aspx", false);
            }
            else
            {
                this.litPopUp.Visible = false;
                Session.Clear();
                Response.Redirect("SearchClient.aspx");
            }
		}
        protected void btnCalendar_ServerClick(object sender, EventArgs e)
        {

        }

        private void NewBond()
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            Session.Clear();
            TaskControl.Bonds taskControl = new TaskControl.Bonds(true);
            taskControl.Mode = 1; //ADD
            //taskControl.isQuote = true;

            //taskControl.Customer.MaritalStatus = ddlMaritalStatus.Items.IndexOf(ddlMaritalStatus.Items.FindByText(ddlMaritalStatus.SelectedItem.Text.ToString()));
            taskControl.Customer.Sex = ddlGender.SelectedItem.Text.ToString();
            taskControl.Customer.Birthday = txtBirthdate.Text.ToString().Trim();
            taskControl.Customer.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
            taskControl.Customer.Initial = TxtInitial.Text.ToString().Trim().ToUpper();
            taskControl.Customer.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
            taskControl.Customer.LastName2 = txtLastname2.Text.ToString().Trim().ToUpper();
            taskControl.CompanyName = txtLastname2.Text.ToString().Trim().ToUpper();
            taskControl.Customer.Email = txtEmail.Text.ToString().Trim().ToUpper();
            taskControl.Customer.JobPhone = txtWorkPhone.Text.ToString().Trim();
            taskControl.Customer.Cellular = TxtCellular.Text.ToString().Trim();
            taskControl.Customer.HomePhone = txtHomePhone.Text.ToString().Trim();
            taskControl.Customer.Licence = TxtLicence.Text.ToString().Trim();
            taskControl.CustomerType = customer.Dependents;

            taskControl.Customer.Address1 = txtHomeUrb1.Text.ToString().Trim().ToUpper();
            taskControl.Customer.Address2 = txtAddress1.Text.ToString().Trim().ToUpper();
            taskControl.Customer.City = txtCity.Text.ToString().Trim().ToUpper();
            taskControl.Customer.State = txtState.Text.ToString().Trim().ToUpper();
            taskControl.Customer.ZipCode = txtZipCode.Text.ToString().Trim().ToUpper();

            taskControl.Customer.AddressPhysical1 = txtAddress1Phys.Text.ToString().Trim().ToUpper();
            taskControl.Customer.AddressPhysical2 = txtAddress2Phys.Text.ToString().Trim().ToUpper();
            taskControl.Customer.CityPhysical = txtCityPhys.Text.ToString().Trim().ToUpper();
            taskControl.Customer.StatePhysical = txtStatePhys.Text.ToString().Trim().ToUpper();
            taskControl.Customer.ZipPhysical = txtZipCodePhys.Text.ToString().Trim().ToUpper();


            taskControl.Customer.CustomerNo = lblCustNumber.Text.ToString().Trim().ToUpper();

            taskControl.Prospect.ProspectID = int.Parse(LblProspectID.Text.Replace("Prospect: ", ""));

            taskControl.Prospect.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.LastName2 = txtLastname2.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.HomePhone = txtHomePhone.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.WorkPhone = txtWorkPhone.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.Cellular = TxtCellular.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.Email = txtEmail.Text.ToString().Trim().ToUpper();

            taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Bonds Quote"));
            Session.Add("TaskControl", taskControl);
            Session.Add("Customer", taskControl.Customer);
            Response.Redirect("Bonds.aspx");
        }

        private void NewRES()
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            Session.Clear();
            TaskControl.RES taskControl = new TaskControl.RES(true);
            taskControl.Mode = 1; //ADD
            //taskControl.isQuote = true;

            //taskControl.Customer.MaritalStatus = ddlMaritalStatus.Items.IndexOf(ddlMaritalStatus.Items.FindByText(ddlMaritalStatus.SelectedItem.Text.ToString()));
            taskControl.Customer.Sex = ddlGender.SelectedItem.Text.ToString();
            taskControl.Customer.Birthday = txtBirthdate.Text.ToString().Trim();
            taskControl.Customer.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
            taskControl.Customer.Initial = TxtInitial.Text.ToString().Trim().ToUpper();
            taskControl.Customer.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
            taskControl.Customer.LastName2 = txtLastname2.Text.ToString().Trim().ToUpper();
            taskControl.Customer.Email = txtEmail.Text.ToString().Trim().ToUpper();
            taskControl.Customer.JobPhone = txtWorkPhone.Text.ToString().Trim();
            taskControl.Customer.Cellular = TxtCellular.Text.ToString().Trim();
            taskControl.Customer.HomePhone = txtHomePhone.Text.ToString().Trim();
            taskControl.Customer.Licence = TxtLicence.Text.ToString().Trim();

            taskControl.Customer.Address1 = txtHomeUrb1.Text.ToString().Trim().ToUpper();
            taskControl.Customer.Address2 = txtAddress1.Text.ToString().Trim().ToUpper();
            taskControl.Customer.City = txtCity.Text.ToString().Trim().ToUpper();
            taskControl.Customer.State = txtState.Text.ToString().Trim().ToUpper();
            taskControl.Customer.ZipCode = txtZipCode.Text.ToString().Trim().ToUpper();

            taskControl.Customer.AddressPhysical1 = txtAddress1Phys.Text.ToString().Trim().ToUpper();
            taskControl.Customer.AddressPhysical2 = txtAddress2Phys.Text.ToString().Trim().ToUpper();
            taskControl.Customer.CityPhysical = txtCityPhys.Text.ToString().Trim().ToUpper();
            taskControl.Customer.StatePhysical = txtStatePhys.Text.ToString().Trim().ToUpper();
            taskControl.Customer.ZipPhysical = txtZipCodePhys.Text.ToString().Trim().ToUpper();

            taskControl.Customer.CustomerNo = lblCustNumber.Text.ToString().Trim().ToUpper();

            taskControl.Prospect.ProspectID = int.Parse(LblProspectID.Text.Replace("Prospect: ", ""));

            taskControl.Prospect.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.LastName2 = txtLastname2.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.HomePhone = txtHomePhone.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.WorkPhone = txtWorkPhone.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.Cellular = TxtCellular.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.Email = txtEmail.Text.ToString().Trim().ToUpper();

            taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "RES Quote"));
            Session.Add("TaskControl", taskControl);
            Session.Add("Customer", taskControl.Customer);
            Response.Redirect("RES.aspx");
        }

        private void NewYacht()
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            Session.Clear();
            EPolicy.TaskControl.Yacht taskControl = new TaskControl.Yacht(true);
            taskControl.Mode = 1; //ADD
            //taskControl.isQuote = true;

            //taskControl.Customer.MaritalStatus = ddlMaritalStatus.Items.IndexOf(ddlMaritalStatus.Items.FindByText(ddlMaritalStatus.SelectedItem.Text.ToString()));
            taskControl.Customer.Sex = ddlGender.SelectedItem.Text.ToString();
            taskControl.Customer.Birthday = txtBirthdate.Text.ToString().Trim();
            taskControl.Customer.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
            taskControl.Customer.Initial = TxtInitial.Text.ToString().Trim().ToUpper();
            taskControl.Customer.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
            taskControl.Customer.LastName2 = txtLastname2.Text.ToString().Trim().ToUpper();
            taskControl.Customer.Email = txtEmail.Text.ToString().Trim().ToUpper();
            taskControl.Customer.JobPhone = txtWorkPhone.Text.ToString().Trim();
            taskControl.Customer.Cellular = TxtCellular.Text.ToString().Trim();
            taskControl.Customer.HomePhone = txtHomePhone.Text.ToString().Trim();
            taskControl.Customer.Licence = TxtLicence.Text.ToString().Trim();

            taskControl.Customer.Address1 = txtHomeUrb1.Text.ToString().Trim().ToUpper();
            taskControl.Customer.Address2 = txtAddress1.Text.ToString().Trim().ToUpper();
            taskControl.Customer.City = txtCity.Text.ToString().Trim().ToUpper();
            taskControl.Customer.State = txtState.Text.ToString().Trim().ToUpper();
            taskControl.Customer.ZipCode = txtZipCode.Text.ToString().Trim().ToUpper();

            taskControl.Customer.AddressPhysical1 = txtAddress1Phys.Text.ToString().Trim().ToUpper();
            taskControl.Customer.AddressPhysical2 = txtAddress2Phys.Text.ToString().Trim().ToUpper();
            taskControl.Customer.CityPhysical = txtCityPhys.Text.ToString().Trim().ToUpper();
            taskControl.Customer.StatePhysical = txtStatePhys.Text.ToString().Trim().ToUpper();
            taskControl.Customer.ZipPhysical = txtZipCodePhys.Text.ToString().Trim().ToUpper();


            taskControl.Customer.CustomerNo = lblCustNumber.Text.ToString().Trim().ToUpper();

            taskControl.Prospect.ProspectID = int.Parse(LblProspectID.Text.Replace("Prospect: ", ""));

            taskControl.Prospect.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.LastName2 = txtLastname2.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.HomePhone = txtHomePhone.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.WorkPhone = txtWorkPhone.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.Cellular = TxtCellular.Text.ToString().Trim().ToUpper();
            taskControl.Prospect.Email = txtEmail.Text.ToString().Trim().ToUpper();

            taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Yacht Quote"));
            Session.Add("TaskControl", taskControl);
            Session.Add("Customer", taskControl.Customer);
            Response.Redirect("Yacht.aspx");
        }

        private void ApplicationInfo()
        {
            try
            {
                if (ddlNewApplication.SelectedItem.Text == "NEW AUTO VI")
                {
                    Customer.Customer customer = (Customer.Customer)Session["Customer"];
                    Session.Clear();
                    TaskControl.Autos taskControl = new TaskControl.Autos(true);
                    
                    //Customer.Customer customer = (Customer.Customer)Session["Customer"];
                    taskControl.Mode = 1; //ADD
                    taskControl.isQuote = true;

                    taskControl.Customer.CustomerNo = lblCustNumber.Text.Trim();
                    taskControl.Customer.MaritalStatus = customer.MaritalStatus;//ddlMaritalStatus.Items.IndexOf(ddlMaritalStatus.Items.FindByText(ddlMaritalStatus.SelectedItem.Text.ToString()));
                    taskControl.Customer.Sex = customer.Sex;//ddlGender.SelectedItem.Text.ToString();
                    taskControl.Customer.Birthday = txtBirthdate.Text.ToString().Trim();
                    
                    DateTime now = DateTime.Today;
                    DateTime birthday = DateTime.Parse(txtBirthdate.Text.ToString().Trim());
                    int age = now.Year - birthday.Year;
                    
                    if (now < birthday.AddYears(age)) age--;

                    taskControl.Customer.Age = age.ToString();
                    taskControl.Customer.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Initial = TxtInitial.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Email = txtEmail.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.JobPhone = txtWorkPhone.Text.ToString().Trim();
                    taskControl.Customer.Cellular = TxtCellular.Text.ToString().Trim();
                    taskControl.Customer.HomePhone = txtHomePhone.Text.ToString().Trim();
                    taskControl.Customer.Licence = TxtLicence.Text.ToString().Trim();

                    taskControl.Customer.Address1 = txtHomeUrb1.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Address2 = txtAddress1.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.City = txtCity.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.State = txtState.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.ZipCode = txtZipCode.Text.ToString().Trim().ToUpper();

                    taskControl.Customer.AddressPhysical1 = txtAddress1Phys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.AddressPhysical2 = txtAddress2Phys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.CityPhysical = txtCityPhys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.StatePhysical = txtStatePhys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.ZipPhysical = txtZipCodePhys.Text.ToString().Trim().ToUpper();


                    taskControl.Customer.CustomerNo = lblCustNumber.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Description = customer.Description;

                    taskControl.Prospect.ProspectID = int.Parse(LblProspectID.Text.Replace("Prospect: ", ""));

                    taskControl.Prospect.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.LastName2 = txtLastname2.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.HomePhone = txtHomePhone.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.WorkPhone = txtWorkPhone.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.Cellular = TxtCellular.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.Email = txtEmail.Text.ToString().Trim().ToUpper();

                    taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Auto VI Quote"));
                    Session.Add("TaskControl", taskControl);
                    Session.Add("Customer", taskControl.Customer);
                    Response.Redirect("Autos.aspx");

                    #region Comentado
                    //Client Information
                    //taskControl.Customer.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.LastName1 = TxtLastName.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.LastName2 = TxtLastName2.Text.ToString().Trim().ToUpper();
                    //taskControl.UseCompanyAsNamedInsured = chkNamedInsured.Checked;
                    //taskControl.Customer.Initial = TxtInitial.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.Sex = ddlGender.SelectedItem.Text.ToString().Trim();
                    //taskControl.Customer.Address1 = TxtAddress.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.Address2 = TxtAddress2.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.City = TxtCity.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.State = TxtState.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.ZipCode = TxtZIPCode.Text.ToString().Trim().ToUpper();

                    //taskControl.Customer.AddressPhysical1 = txtPhyAddress.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.AddressPhysical2 = txtPhyAddress2.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.CityPhysical = txtPhyCity.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.StatePhysical = txtPhyState.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.ZipPhysical = txtPhyZipCode.Text.ToString().Trim().ToUpper();

                    //taskControl.Customer.JobPhone = TxtWorkPhone.Text.ToString().Trim();
                    //taskControl.Customer.Cellular = TxtCellPhone.Text.ToString().Trim();
                    //taskControl.Customer.HomePhone = TxtHomePhone.Text.ToString().Trim();
                    //taskControl.Customer.Birthday = TxtBirthDate.Text.ToString().Trim();
                    //taskControl.Customer.Age = TxtAge.Text.ToString().Trim();
                    //taskControl.Customer.MaritalStatus = ddlMaritalStatus.SelectedIndex;
                    //taskControl.Customer.Licence = TxtLicenseNumber.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.OccupationID = ddlOccupation.SelectedIndex;
                    //taskControl.Customer.Occupation = TxtOtherOccupation.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.EmplName = TxtEmployerName.Text.ToString().Trim().ToUpper();
                    //taskControl.Customer.Email = txtEmailAddress.Text.ToString().Trim().ToUpper();

                    ////Comented by Joshua -Se repite
                    ////taskControl.EffectiveDate = TxtEffBinderDate.Text.ToString();
                    ////taskControl.ExpirationDate = TxtExpBinderDate.Text.ToString();

                    //taskControl.Prospect.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
                    //taskControl.Prospect.LastName1 = TxtLastName.Text.ToString().Trim().ToUpper();
                    //taskControl.Prospect.LastName2 = TxtLastName2.Text.ToString().Trim().ToUpper();
                    //taskControl.Prospect.HomePhone = TxtHomePhone.Text.ToString().Trim().ToUpper();
                    //taskControl.Prospect.WorkPhone = TxtWorkPhone.Text.ToString().Trim().ToUpper();
                    //taskControl.Prospect.Cellular = TxtCellPhone.Text.ToString().Trim().ToUpper();
                    //taskControl.Prospect.Email = txtEmailAddress.Text.ToString().Trim().ToUpper();
                    #endregion
                }
                else if (ddlNewApplication.SelectedItem.Text == "NEW GUARDIANXTRA")
                {
                    Customer.Customer customer = (Customer.Customer)Session["Customer"];
                    Session.Clear();
                    TaskControl.GuardianXtra taskControl = new TaskControl.GuardianXtra();
                    taskControl.Mode = 1; //ADD
                    //taskControl.isQuote = true;

                    //taskControl.Customer.MaritalStatus = ddlMaritalStatus.Items.IndexOf(ddlMaritalStatus.Items.FindByText(ddlMaritalStatus.SelectedItem.Text.ToString()));
                    taskControl.Customer.Sex = ddlGender.SelectedItem.Text.ToString();
                    taskControl.Customer.Birthday = txtBirthdate.Text.ToString().Trim();
                    taskControl.Customer.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Initial = TxtInitial.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Email = txtEmail.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.JobPhone = txtWorkPhone.Text.ToString().Trim();
                    taskControl.Customer.Cellular = TxtCellular.Text.ToString().Trim();
                    taskControl.Customer.HomePhone = txtHomePhone.Text.ToString().Trim();
                    taskControl.Customer.Licence = TxtLicence.Text.ToString().Trim();

                    taskControl.Customer.Address1 = txtHomeUrb1.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Address2 = txtAddress1.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.City = txtCity.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.State = txtState.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.ZipCode = txtZipCode.Text.ToString().Trim().ToUpper();

                    taskControl.Customer.AddressPhysical1 = txtAddress1Phys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.AddressPhysical2 = txtAddress2Phys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.CityPhysical = txtCityPhys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.StatePhysical = txtStatePhys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.ZipPhysical = txtZipCodePhys.Text.ToString().Trim().ToUpper();


                    taskControl.Customer.CustomerNo = lblCustNumber.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Description = customer.Description;

                    taskControl.Prospect.ProspectID = int.Parse(LblProspectID.Text.Replace("Prospect: ", ""));

                    taskControl.Prospect.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.LastName2 = txtLastname2.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.HomePhone = txtHomePhone.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.WorkPhone = txtWorkPhone.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.Cellular = TxtCellular.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.Email = txtEmail.Text.ToString().Trim().ToUpper();

                    taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "GuardianXtra"));
                    Session.Add("TaskControl", taskControl);
                    Session.Add("Customer", taskControl.Customer);
                    Response.Redirect("GuardianXtra.aspx");
                }
                else if (ddlNewApplication.SelectedItem.Text == "NEW RESIDENTIAL PROPERTY")
                {
                    Customer.Customer customer = (Customer.Customer)Session["Customer"];
                    Session.Clear();
                    TaskControl.HomeOwners taskControl = new TaskControl.HomeOwners(true);
                    taskControl.Mode = 1; //ADD
                    //taskControl.isQuote = true;

                    //taskControl.Customer.MaritalStatus = ddlMaritalStatus.Items.IndexOf(ddlMaritalStatus.Items.FindByText(ddlMaritalStatus.SelectedItem.Text.ToString()));
                    taskControl.Customer.Sex = ddlGender.SelectedItem.Text.ToString();
                    taskControl.Customer.Birthday = txtBirthdate.Text.ToString().Trim();
                    taskControl.Customer.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Initial = TxtInitial.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Email = txtEmail.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.JobPhone = txtWorkPhone.Text.ToString().Trim();
                    taskControl.Customer.Cellular = TxtCellular.Text.ToString().Trim();
                    taskControl.Customer.HomePhone = txtHomePhone.Text.ToString().Trim();
                    taskControl.Customer.Licence = TxtLicence.Text.ToString().Trim();

                    taskControl.Customer.Address1 = txtHomeUrb1.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Address2 = txtAddress1.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.City = txtCity.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.State = txtState.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.ZipCode = txtZipCode.Text.ToString().Trim().ToUpper();

                    taskControl.Customer.AddressPhysical1 = txtAddress1Phys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.AddressPhysical2 = txtAddress2Phys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.CityPhysical = txtCityPhys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.StatePhysical = txtStatePhys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.ZipPhysical = txtZipCodePhys.Text.ToString().Trim().ToUpper();


                    taskControl.Customer.CustomerNo = lblCustNumber.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Description = customer.Description;

                    taskControl.Prospect.ProspectID = int.Parse(LblProspectID.Text.Replace("Prospect: ", ""));

                    taskControl.Prospect.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.LastName2 = txtLastname2.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.HomePhone = txtHomePhone.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.WorkPhone = txtWorkPhone.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.Cellular = TxtCellular.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.Email = txtEmail.Text.ToString().Trim().ToUpper();

                    taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "Home Owners Policy Quote"));
                    Session.Add("TaskControl", taskControl);
                    Session.Add("Customer", taskControl.Customer);
                    Response.Redirect("HomeOwners.aspx");
                }

                else if (ddlNewApplication.SelectedItem.Text == "NEW ROAD ASSIST")
                {
                    Customer.Customer customer = (Customer.Customer)Session["Customer"];
                    Session.Clear();
                    TaskControl.RoadAssistance taskControl = new TaskControl.RoadAssistance();
                    taskControl.Mode = 1; //ADD
                    //taskControl.isQuote = true;
                    //taskControl.Customer.MaritalStatus = ddlMaritalStatus.Items.IndexOf(ddlMaritalStatus.Items.FindByText(ddlMaritalStatus.SelectedItem.Text.ToString()));
                    taskControl.Customer.Sex = ddlGender.SelectedItem.Text.ToString();
                    taskControl.Customer.Birthday = txtBirthdate.Text.ToString().Trim();
                    taskControl.Customer.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Initial = TxtInitial.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Email = txtEmail.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.JobPhone = txtWorkPhone.Text.ToString().Trim();
                    taskControl.Customer.Cellular = TxtCellular.Text.ToString().Trim();
                    taskControl.Customer.HomePhone = txtHomePhone.Text.ToString().Trim();
                    taskControl.Customer.Licence = TxtLicence.Text.ToString().Trim();
                    

                    taskControl.Customer.Address1 = txtHomeUrb1.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Address2 = txtAddress1.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.City = txtCity.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.State = txtState.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.ZipCode = txtZipCode.Text.ToString().Trim().ToUpper();

                    taskControl.Customer.AddressPhysical1 = txtAddress1Phys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.AddressPhysical2 = txtAddress2Phys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.CityPhysical = txtCityPhys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.StatePhysical = txtStatePhys.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.ZipPhysical = txtZipCodePhys.Text.ToString().Trim().ToUpper();


                    taskControl.Customer.CustomerNo = lblCustNumber.Text.ToString().Trim().ToUpper();
                    taskControl.Customer.Description = customer.Description;

                    taskControl.Prospect.ProspectID = int.Parse(LblProspectID.Text.Replace("Prospect: ", ""));

                    taskControl.Prospect.FirstName = TxtFirstName.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.LastName1 = txtLastname1.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.LastName2 = txtLastname2.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.HomePhone = txtHomePhone.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.WorkPhone = txtWorkPhone.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.Cellular = TxtCellular.Text.ToString().Trim().ToUpper();
                    taskControl.Prospect.Email = txtEmail.Text.ToString().Trim().ToUpper();

                    taskControl.TaskControlTypeID = int.Parse(LookupTables.LookupTables.GetID("TaskControlType", "GuardianRoadAssit"));
                    Session.Add("TaskControl", taskControl);
                    Session.Add("Customer", taskControl.Customer);
                    Response.Redirect("RoadAssistance.aspx");
                }
                else if (ddlNewApplication.SelectedItem.Text == "NEW BOND")
                {
                    NewBond();
                }
                else if (ddlNewApplication.SelectedItem.Text == "NEW RES")
                {
                    NewRES();
                }
                else if (ddlNewApplication.SelectedItem.Text == "NEW YACHT")
                {
                    NewYacht();
                }   



            }
            catch (Exception xp)
            {
 
            }
        }
        protected void btnNewApplication_Click(object sender, EventArgs e)
        {
			if (ValidateFlags())
				return;
            ApplicationInfo();
        }

        protected void btnNewBond_Click(object sender, EventArgs e)
        {
             NewBond();
        }
        protected void GridComments_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void btnCustomerComments_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtMedicalName.Text.Trim() == "")
                //    throw new Exception("The Check Name field is missing.");

                //if (txtMedicalPayment.Text.Trim() == "")
                //    throw new Exception("The Payment Amount field is missing.");

                SaveComments();

                ClearComments();

                FillCommentsGrid();

            }
            catch (Exception xcp)
            {
                //lblRecHeader.Text = xcp.Message.Trim();
                //mpeSeleccion.Show();
            }
        }

        public void ClearComments()
        {
            txtCustomerComments.Text = "";
            
        }

        private void FillCommentsGrid()
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];

            GridComments.DataSource = null;
            DataTable DtCert = null;

            if (lblCustNumber.Text.ToString() == "")
            {
                return;
            }
            if (ddlPolicyClass.SelectedIndex == 0)
                DtCert = GetCustomerCommentsByCustomerNo(int.Parse(lblCustNumber.Text.ToString()));
            else
            {
                int PolicyClassID = int.Parse(ddlPolicyClass.SelectedItem.Value);
                DtCert = GetCustomerCommentsByCustomerNo1(int.Parse(lblCustNumber.Text.ToString()), PolicyClassID);
            }
            if (DtCert != null)
            {
                if (DtCert.Rows.Count != 0)
                {
                    GridComments.DataSource = DtCert;
                    GridComments.DataBind();
                }
                else
                {
                    GridComments.DataSource = null;
                    GridComments.DataBind();
                }
            }
            else
            {
                GridComments.DataSource = null;
                GridComments.DataBind();
            }
        }

        private void FillClaimsGrid()
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];

            GridClaims.DataSource = null;
            DataTable DtClaims = null;
            DataTable DtPolicies = null;

            if (lblCustNumber.Text.ToString() == "")
            {
                return;
            }
            try
            {
                DtPolicies = GetValidPoliciesByCustomerNo(lblCustNumber.Text.ToString().Trim());

                if (DtPolicies != null)
                {
                    if (DtPolicies.Rows.Count > 0)
                    {
                        for (int i = 0; i < DtPolicies.Rows.Count; i++)
                        {
                            DataTable DtClaim = new DataTable();
                            string PolicyNo = "";
                            string EffectiveDate = "";
                            string ExpirationDate = "";
                            string CoverageType = "";
                            string BI = "";
                            string PD = "";

                            //PolicyNo = DtPolicies.Rows[i]["PolicyType"].ToString().Trim() + int.Parse(DtPolicies.Rows[i]["PolicyNo"].ToString().Trim()).ToString();
                            //PolicyNo = DtPolicies.Rows[i]["Sufijo"].ToString().Trim() != "00" ? PolicyNo + "-" + DtPolicies.Rows[i]["Sufijo"].ToString().Trim() : PolicyNo;
                            PolicyNo = DtPolicies.Rows[i]["PolicyNo"].ToString();
                            EffectiveDate = DtPolicies.Rows[i]["EffectiveDate"].ToString().Trim();
                            ExpirationDate = DtPolicies.Rows[i]["ExpirationDate"].ToString().Trim();
                            BI = DtPolicies.Rows[i]["BIPremium"].ToString().Trim();
                            PD = DtPolicies.Rows[i]["PDPremium"].ToString().Trim();

                            CoverageType = DtPolicies.Rows[i]["CoverageType"].ToString().Trim();

                            DtClaim = GetFlagFromClaimNext(PolicyNo, EffectiveDate, ExpirationDate);

                            if (DtClaim != null)
                            {
                                DataTable dtFull = new DataTable();
                                DataTable DtSurchargeRenewal = new DataTable();

                                if (CoverageType == "FullCover")
                                {
                                    dtFull.Columns.Add("Flag");
                                    dtFull.Columns.Add("Surcharge");
                                    dtFull.NewRow();
                                    dtFull.Rows.Add("FullCover", "0");
                                    DtSurchargeRenewal = dtFull.Copy();
                                }
                                else
                                {
                                    //if (BI != "0" && PD != "0")
                                    //{
                                    //CoverageType = "BI & PD";
                                    //}
                                    //else if (BI != "0")
                                    //{
                                    //    CoverageType = "BI ONLY";
                                    //}
                                    //else if (PD != "0")
                                    //{
                                    //    CoverageType = "PD ONLY";
                                    //}

                                    if (DtClaim.Rows.Count > 0)
                                    {
                                        for (int j = 0; DtClaim.Rows.Count > j; j++)
                                        {
                                            if (DtClaim.Rows[j]["LossType"].ToString().Contains("Bodily Injury"))
                                            {
                                                CoverageType = "BI ONLY";
                                            }
                                            else if (DtClaim.Rows[j]["LossType"].ToString().Contains("Property Damage"))
                                            {
                                                CoverageType = "PD ONLY";
                                            }
                                        }
                                    }

                                    DtSurchargeRenewal = GetVI_SurchargesRenewalByFlag(CoverageType);
                                }
                                DtClaim.Columns.Add("Flag");
                                DtClaim.Columns.Add("Surcharge");

                                for (int o = 0; o < DtClaim.Rows.Count; o++)
                                {
                                    if (DtClaim.Rows[o]["LossType"].ToString().Contains("Bodily Injury"))
                                    {
                                        DtClaim.Rows[o]["Flag"] = "BI ONLY";
                                        DtClaim.Rows[o]["Surcharge"] = DtSurchargeRenewal.Rows[0]["Surcharge"].ToString();
                                    }
                                    else if (DtClaim.Rows[o]["LossType"].ToString().Contains("Property Damage"))
                                    {
                                        DtClaim.Rows[o]["Flag"] = "PD ONLY";
                                        DtClaim.Rows[o]["Surcharge"] = DtSurchargeRenewal.Rows[0]["Surcharge"].ToString();
                                    }

                                    //if (PolicyNo == DtClaim.Rows[o]["PolicyNumber"].ToString())
                                    //{
                                    //    DtClaim.Rows[o]["Flag"] = DtSurchargeRenewal.Rows[0]["Flag"].ToString();
                                    //    DtClaim.Rows[o]["Surcharge"] = DtSurchargeRenewal.Rows[0]["Surcharge"].ToString();
                                    //}
                                }
                            }

                            if (DtClaims == null)
                                DtClaims = DtClaim.Copy();
                            else
                                DtClaims.Merge(DtClaim);
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message.ToString();
                mpeSeleccion.Show();
            }

            if (DtClaims != null)
            {
                if (DtClaims.Rows.Count != 0)
                {
                    GridClaims.DataSource = DtClaims;
                    GridClaims.DataBind();
                }
                else
                {
                    GridClaims.DataSource = null;
                    GridClaims.DataBind();
                }
            }
            else
            {
                GridClaims.DataSource = null;
                GridClaims.DataBind();
            }
        }

        private DataTable GetCustomerCommentsByCustomerNo(int CustomerNo)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("CustomerNo", SqlDbType.Int, 0, CustomerNo.ToString(), ref cookItems);

                Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }
                DataTable dt = null;
                try
                {
                    dt = exec.GetQuery("GetCustomerCommentsByCustomerNo", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    //throw new Exception("There is no information to display, please try again.", ex);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable GetCustomerCommentsByCustomerNo1(int CustomerNo, int PolicyClassID)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("CustomerNo", SqlDbType.Int, 0, CustomerNo.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("PolicyClassID", SqlDbType.Int, 0, PolicyClassID.ToString(), ref cookItems);

                Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }
                DataTable dt = null;
                try
                {
                    dt = exec.GetQuery("GetCustomerCommentsByCustomerNo", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    //throw new Exception("There is no information to display, please try again.", ex);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void SaveComments() 
        {
            try
            {
                    
                    Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();


                    DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];

                    DbRequestXmlCooker.AttachCookItem("CustomerNo", SqlDbType.Int, 0, lblCustNumber.Text.ToString().Trim(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("Comment", SqlDbType.VarChar, 500, txtCustomerComments.Text.ToString().Trim().ToUpper(), ref cookItems);
                   

                    XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                    Executor.BeginTrans();
                    int id2 = Executor.Insert("AddComments", xmlDoc);
                    Executor.CommitTrans();

            }
            catch (Exception exp)
            {
               
            }
        }
        protected void GridComments_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Cells[0].Visible = false;
            //e.Row.Cells[1].Visible = false;
        }
        protected void GridComments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetCustomerCommentsByCustomerNo(int.Parse(lblCustNumber.Text.ToString()));
            FillCommentsGrid();
            GridComments.PageIndex = e.NewPageIndex;
            GridComments.DataBind();
           
        }
        private void FillFlags()
        {

            DataTable dt = GetCustomerFlags(_CustomerNo);

            if (dt.Rows.Count > 0)
            {

                if ((bool)dt.Rows[0]["ExcludePerson"] == true)
                    chkExcludePerson.Checked = true;
                else
                    chkExcludePerson.Checked = false;

                if ((bool)dt.Rows[0]["KeepWatch"] == true)
                    chkKeepWatch.Checked = true;
                else
                    chkKeepWatch.Checked = false;

                if ((bool)dt.Rows[0]["FrontingPolicy"] == true)
                    chkFrontingPolicy.Checked = true;
                else
                    chkFrontingPolicy.Checked = false;
				
				if ((bool)dt.Rows[0]["ClaimsPolicyDisplay"] == true)
					chkClaimsPolicyDisplay.Checked = true;
				else
					chkClaimsPolicyDisplay.Checked = false;

                if ((bool)dt.Rows[0]["PersonPolicyDisplay"] == true)
                    chkPersonPolicyDisp.Checked = true;
                else
                    chkPersonPolicyDisp.Checked = false;

                if ((bool)dt.Rows[0]["PolicyDisplay"] == true)
                    chkPolicyDisplay.Checked = true;
                else
                    chkPolicyDisplay.Checked = false;

                if ((bool)dt.Rows[0]["PFCPolicyDisplay"] == true)
                    chkPFCPolicyDisp.Checked = true;
                else
                    chkPFCPolicyDisp.Checked = false;

                if ((bool)dt.Rows[0]["DoNotRenew"] == true)
                    chkDoNotRenew.Checked = true;
                else
                    chkDoNotRenew.Checked = false;

                if ((bool)dt.Rows[0]["DUIPersonDisplay"] == true)
                    chkDUIPersonDisp.Checked = true;
                else
                    chkDUIPersonDisp.Checked = false;

                if ((bool)dt.Rows[0]["ClientDisplay"] == true)
                    chkClientDisp.Checked = true;
                else
                    chkClientDisp.Checked = false;

                if ((bool)dt.Rows[0]["LegalNameDisplay"] == true)
                    chkLegalNameDisp.Checked = true;
                else
                    chkLegalNameDisp.Checked = false;

                if ((bool)dt.Rows[0]["EmployeeDiscount"] == true)
                    chkEmployeeDiscount.Checked = true;
                else
                    chkEmployeeDiscount.Checked = false;
            }
            

        }

        
        private static DataTable GetCustomerFlags(string CustomerNo)
        
        {
            try
            {
                
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("CustomerNo", SqlDbType.Int, 0, int.Parse(CustomerNo).ToString(), ref cookItems);
                

                Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }
                DataTable dt = null;
                try
                {
                    dt = exec.GetQuery("GetCustomerFlags", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    //throw new Exception("There is no information to display, please try again.", ex);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
        }

        private static DataTable GetCustomerFlagsByCustomerNo(string CustomerNo)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("CustomerNo", SqlDbType.Int, 0, int.Parse(CustomerNo).ToString(), ref cookItems);

                Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }
                DataTable dt = null;
                try
                {
                    dt = exec.GetQuery("GetCustomerFlagsByCustomerNo", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    //throw new Exception("There is no information to display, please try again.", ex);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        private void SaveFlags(string CustomerNo)
        {
            try
            {
                Customer.Customer customer = (Customer.Customer)Session["Customer"];

                DataTable dt =  GetCustomerFlagsByCustomerNo(CustomerNo);

                if (dt.Rows.Count > 0)
                {
                    CustomerNo = dt.Rows[0]["CustomerNo"].ToString();
                }
                else
                {
                    CustomerNo = "0";
                }

                if(CustomerNo.ToString() != customer.CustomerNo)
                {    
                    
                    Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();


                    DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[13];

                    
                    DbRequestXmlCooker.AttachCookItem("CustomerNo", SqlDbType.Int, 0, customer.CustomerNo.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("ExcludePerson", SqlDbType.Bit, 0, chkExcludePerson.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("KeepWatch", SqlDbType.Bit, 0, chkKeepWatch.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("FrontingPolicy", SqlDbType.Bit, 0, chkFrontingPolicy.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("ClaimsPolicyDisplay", SqlDbType.Bit, 0, chkClaimsPolicyDisplay.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("PersonPolicyDisplay", SqlDbType.Bit, 0, chkPersonPolicyDisp.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("PolicyDisplay", SqlDbType.Bit, 0, chkPolicyDisplay.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("PFCPolicyDisplay", SqlDbType.Bit, 0, chkPFCPolicyDisp.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("DoNotRenew", SqlDbType.Bit, 0, chkDoNotRenew.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("DUIPersonDisplay", SqlDbType.Bit, 0, chkDUIPersonDisp.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("ClientDisplay", SqlDbType.Bit, 0, chkClientDisp.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("LegalNameDisplay", SqlDbType.Bit, 0, chkLegalNameDisp.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("EmployeeDiscount", SqlDbType.Bit, 0, chkEmployeeDiscount.Checked.ToString(), ref cookItems);
                    



                    XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                    Executor.BeginTrans();
                    int id2 = Executor.Insert("AddCustomerFlags", xmlDoc);
                    Executor.CommitTrans();
                }
                else
            {
                 Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();


                    DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[13];

                    
                    DbRequestXmlCooker.AttachCookItem("CustomerNo", SqlDbType.Int, 0, customer.CustomerNo.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("ExcludePerson", SqlDbType.Bit, 0, chkExcludePerson.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("KeepWatch", SqlDbType.Bit, 0, chkKeepWatch.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("FrontingPolicy", SqlDbType.Bit, 0, chkFrontingPolicy.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("ClaimsPolicyDisplay", SqlDbType.Bit, 0, chkClaimsPolicyDisplay.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("PersonPolicyDisplay", SqlDbType.Bit, 0, chkPersonPolicyDisp.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("PolicyDisplay", SqlDbType.Bit, 0, chkPolicyDisplay.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("PFCPolicyDisplay", SqlDbType.Bit, 0, chkPFCPolicyDisp.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("DoNotRenew", SqlDbType.Bit, 0, chkDoNotRenew.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("DUIPersonDisplay", SqlDbType.Bit, 0, chkDUIPersonDisp.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("ClientDisplay", SqlDbType.Bit, 0, chkClientDisp.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("LegalNameDisplay", SqlDbType.Bit, 0, chkLegalNameDisp.Checked.ToString(), ref cookItems);
                    DbRequestXmlCooker.AttachCookItem("EmployeeDiscount", SqlDbType.Bit, 0, chkEmployeeDiscount.Checked.ToString(), ref cookItems);
                    



                    XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

                    Executor.BeginTrans();
                    int id2 = Executor.Insert("UpdateCustomerFlags", xmlDoc);
                    Executor.CommitTrans();
            }
            }
            
            catch (Exception exp)
            {
            }
        }
		
		private DataTable GetIfUserCanSeeCustomer(int UserID ,int CustomerNo)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];
                DbRequestXmlCooker.AttachCookItem("UserID", SqlDbType.Int, 0, UserID.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("CustomerID", SqlDbType.Int, 0, CustomerNo.ToString(), ref cookItems);

                Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }
                DataTable dt = null;
                try
                {
                    dt = exec.GetQuery("GetIfUserCanSeeCustomer", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    //throw new Exception("There is no information to display, please try again.", ex);
                }

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region UPLOAD DOCUMENTS
        protected void btnAdjuntar_Click(object sender, EventArgs e)
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];

            //var uc = (UserControl)Page.LoadControl("~/AddDocuments.ascx");
            //Panel1.Controls.Add(uc);
            //ModalPopupExtender1.Show();
            //return;

            if (customer.CustomerNo == "0")
            {
                lblRecHeader.Text = "You must save customer in order to proceed.";
                mpeSeleccion.Show();
            }
            else
            {
                txtDocumentDesc.Text = "";
                mpeAdjunto.Show();
            }
        }

        protected void btnAdjuntarCargar_Click(object sender, EventArgs e)
        {
            try
            {
                Customer.Customer customer = (Customer.Customer)Session["Customer"];

                if (txtDocumentDesc.Text.Trim() == "")
                    throw new Exception("Please Fill the description Field.");

                if(ddlTransaction.Items.Count > 1)
                    if (ddlTransaction.SelectedItem.Text == "")
                        throw new Exception("Please Select a Transaction.");

                if (this.FileUpload1.PostedFile != null)
                {
                    if (FileUpload1.PostedFile.FileName == "")
                    {
                        throw new Exception("Please select a file from the browser.");
                    }
                }
                else
                {
                    throw new Exception("Please select a file from the browser.");
                }

                if (this.FileUpload1.PostedFile.FileName != "")
                {
                    if (this.FileUpload1.PostedFile != null)
                    {
                        string File = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf('.'));

                          switch (File.ToLower())
                        {
                            case ".pdf":

                                break;

                            case ".jpeg":
                                
                                break;

                            case ".png":
                                
                                break;

                            case ".jpg":
                                
                                break;

                            default:

                                if (this.FileUpload1.PostedFile.FileName.Split(".".ToCharArray())[1].ToString().ToLower() != "pdf")
                                {
                                    throw new Exception("The File Format is not supported.");
                                }
                                break;
                        }

                        if (this.FileUpload1.PostedFile.ContentLength > 12000001)
                        {
                            throw new Exception("The file size must be up to 12MB.");
                        }
                    }
                }

                //SaveDocuments
                int docid = EPolicy.Customer.Customer.Savedocuments(customer.CustomerNo.ToString(), txtDocumentDesc.Text.Trim(), ddlTransaction.SelectedItem.Value.Trim(), "0");

                //Upload Document
                if (FileUpload1.PostedFile.FileName != null)
                {
                    string fileName = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf('.'));


                    switch (fileName.ToLower())
                    {
                        case ".pdf":

                            fileName = Server.MapPath("./Documents/") + docid.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".pdf";
                            break;

                        case ".jpeg":
                            fileName = Server.MapPath("./Documents/") + docid.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".jpeg";
                            break;

                        case ".png":
                            fileName = Server.MapPath("./Documents/") + docid.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".png";
                            break;

                        case ".jpg":
                            fileName = Server.MapPath("./Documents/") + docid.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".jpg";
                            break;

                        default:
                            break;
                    }
                       
                    FileUpload1.PostedFile.SaveAs(fileName);

                    FillGridDocuments();
                    txtDocumentDesc.Text = "";
                    ddlTransaction.SelectedIndex = 0;
                    Session["Transaction"] = null;
                    mpeAdjunto.Show();
                }
            }
            catch (Exception exp)
            {
                ddlTransaction.SelectedIndex = 0;
                mpeSeleccion.Show();
                lblRecHeader.Text = exp.Message;
                return;
            }
        }

        private void FillGridDocuments()
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            Customer.Customer customer = (Customer.Customer)Session["Customer"];

            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            gvAdjuntar.DataSource = null;
            DataTable DtCert = null;
            DataTable dtTransaction = null;
            int intPolicyClassID = 0;

            if (ddlPolicyClass.SelectedIndex != 0)
                intPolicyClassID = int.Parse(ddlPolicyClass.SelectedItem.Value);


            if (customer.CustomerNo != "")
            {
                DtCert = EPolicy.Customer.Customer.GetDocumentsByCustomerNo1(customer.CustomerNo, 0, 0, intPolicyClassID);
                dtTransaction = TaskControl.TaskControl.GetTaskControlByCustomerNo(customer.CustomerNo, userID);
            }

            if (dtTransaction != null && !IsPostBack)
            {
                if (dtTransaction.Rows.Count > 0)
                {
                    //Transaction
                    ddlTransaction.DataSource = dtTransaction;
                    ddlTransaction.DataTextField = "TaskControlTypeID";
                    ddlTransaction.DataValueField = "TaskControlID";
                    ddlTransaction.DataBind();
                    ddlTransaction.SelectedIndex = -1;
                    ddlTransaction.Items.Insert(0, "");

                    if (ddlTransaction.Items.Count > 1)
                        foreach (ListItem item in ddlTransaction.Items)
                        {
                            if (item.Text != "")
                            {
                                DataRow[] Row = dtTransaction.Select("TaskControlID = '" + item.Value + "'");
                                item.Text = Row[0]["TaskControlTypeDesc"].ToString().Trim().Contains("Home Owners") ?  Row[0]["TaskControlTypeDesc"].ToString().Trim().Replace("Home Owners", "Residential Property")  + " - " + Row[0]["TaskControlID"].ToString().Trim() : Row[0]["TaskControlTypeDesc"].ToString().Trim() + " - " + Row[0]["TaskControlID"].ToString().Trim();
                            }
                        }
                }
            }
            else
            { }

            if (DtCert != null)
            {
                if (DtCert.Rows.Count != 0)
                {
                    gvAdjuntar.DataSource = DtCert;
                    gvAdjuntar.DataBind();
                }
                else
                {
                    gvAdjuntar.DataSource = null;
                    gvAdjuntar.DataBind();
                }
            }
            else
            {
                gvAdjuntar.DataSource = null;
                gvAdjuntar.DataBind();
            }
        }

        protected void gvAdjuntar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            int documentID = 0;
            try
            {
                if (e.CommandName.Trim() == "View")
                {
                    int index = Int32.Parse(e.CommandArgument.ToString());
                    GridViewRow row = gvAdjuntar.Rows[index];
                    TableCell cell = row.Cells[1]; //ID is displayed in 2nd column  
                    int i = int.Parse(cell.Text);

                    documentID = i;

                    string fileName = System.Configuration.ConfigurationManager.AppSettings["RootURL"].ToString().Trim();

                    fileName = fileName + "Documents\\";

                    string[] fileNames = System.IO.Directory.GetFiles(fileName, @"*" + i.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + "*");

                    fileName = fileNames[0].ToString();

                    fileName = fileName.Substring(fileName.LastIndexOf('.'));

                    string fileType = fileName.Substring(fileName.LastIndexOf('.') + 1).ToUpper();

                    ddlTransaction.SelectedIndex = ddlTransaction.Items.IndexOf(ddlTransaction.Items.FindByValue(row.Cells[4].Text.Trim()));

                    //Session["Transaction"] = ddlTransaction.SelectedIndex;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('Documents/" + documentID.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + "" + fileName + "','" + fileType + "','status=yes,menubar,scrollbars=yes,resizable=yes,copyhistory=no,width=1150,height=725');", true);
                }
            }
            catch (Exception exp)
            {
                mpeSeleccion.Show();
                lblRecHeader.Text = exp.Message;
                return;
            }

            mpeAdjunto.Show();
        }

        protected void gvAdjuntar_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[1].Visible = false;

            }
            catch (Exception exc)
            {

            }
        }

        protected void gvAdjuntar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                Customer.Customer customer = (Customer.Customer)Session["Customer"];
                int index = e.RowIndex;
                GridViewRow row = gvAdjuntar.Rows[index];
                TableCell cell = row.Cells[1]; // ID is displayed in 2nd column  
                int i = int.Parse(cell.Text);

                //Se elimna de la tabla
                EPolicy.Customer.Customer.DeleteDocumentsByDocumentsID(i);

                //Se elimina el documento fisicamente
                string fileName = System.Configuration.ConfigurationManager.AppSettings["RootURL"].ToString().Trim();

                fileName = fileName + "Documents\\";

                string[] fileNames = System.IO.Directory.GetFiles(fileName, @"*" + i.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + "*");

                fileName = fileNames[0].ToString();

                //fileName = fileName + "Documents\\" + i.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".pdf";

                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }

                ScriptManager.RegisterStartupScript(this,GetType(),"key","alert('Document has been deleted!');",true);

                FillTextControl();
                mpeAdjunto.Show();
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
            }
        }

        protected void ddlTransaction_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //Session["Transaction"] = ddlTransaction.SelectedIndex;
            mpeAdjunto.Show();
        }
        protected void ddlPolicyClass_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            mpeAdjunto.Show();
        }

        #endregion UPLOAD DOCUMENTS

        private void SaveComment(bool IsSystemComment, string Comments)
        {
            //try
            //{
            //    Customer.Customer customer = (Customer.Customer)Session["Customer"];
            //    Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
            //    int id = 0;
            //    try
            //    {
            //        Customer.Customer customer = (Customer.Customer)Session["Customer"];
            //        int userID = cp.UserID;

            //        if (IsSystemComment == false)
            //            Comments = txtComments.Text.Trim();

            //        DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[4];
            //        DbRequestXmlCooker.AttachCookItem("ClaimID", SqlDbType.Int, 0, claim.ClaimID.ToString(), ref cookItems);
            //        DbRequestXmlCooker.AttachCookItem("CommentDesc", SqlDbType.VarChar, 3000, Comments.Trim(), ref cookItems);
            //        DbRequestXmlCooker.AttachCookItem("UserID", SqlDbType.Int, 0, userID.ToString(), ref cookItems);
            //        DbRequestXmlCooker.AttachCookItem("IsSystemComment", SqlDbType.Bit, 0, IsSystemComment.ToString(), ref cookItems);
            //        XmlDocument xmlDoc = DbRequestXmlCooker.Cook(cookItems);

            //        Executor.BeginTrans();
            //        id = Executor.Insert("AddComment", xmlDoc);
            //        Executor.CommitTrans();
            //    }
            //    catch (Exception xcp)
            //    {
            //        Executor.RollBackTrans();
            //        throw new Exception("Error Could not Save the Comment, Try again.");
            //    }

            //    txtComments.Text = "";
            //}
            //catch (Exception exp)
            //{
            //    mpeSeleccion.Show();
            //    lblRecHeader.Text = exp.Message;
            //}
        }

        private DataTable GetValidPoliciesByCustomerNo(string CustomerNo)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("CustomerNo", SqlDbType.Int, 0, CustomerNo.ToString(), ref cookItems);
                
                Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }
                DataTable dt = null;
                try
                {
                    dt = exec.GetQuery("GetValidPoliciesByCustomerNo", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    //throw new Exception("There is no information to display, please try again.", ex);
					//return null;			  
                }

				return dt;		  
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
				return null;
            }
        }

        private DataTable GetFlagFromClaimNext(string PolicyNumber, string EffectiveDate, string ExpirationDate)
        {
            EPolicy.TaskControl.TaskControl taskControl = (EPolicy.TaskControl.TaskControl)Session["TaskControl"];
            DataTable dt = new DataTable();;

            try
            {
                string connection = System.Configuration.ConfigurationManager.AppSettings["ConnStrClaimNext"].ToString();//@"Data Source=ASPIREJ;Initial Catalog=ClaimNext;User ID=sa;password=sqlserver;";
                SqlConnection con = new SqlConnection(connection);
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GetFlagFromClaimNext", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@PolicyNumber", SqlDbType.VarChar).Value = PolicyNumber.ToString().Trim() == "" ? PolicyNumber.ToString().Trim() : PolicyNumber.ToString().Trim();
                    cmd.Parameters.Add("@EffectiveDate", SqlDbType.VarChar).Value = EffectiveDate.ToString().Trim() == "" ? EffectiveDate.ToString().Trim() : EffectiveDate.ToString().Trim();
                    cmd.Parameters.Add("@ExpirationDate", SqlDbType.VarChar).Value = ExpirationDate.ToString().Trim() == "" ? ExpirationDate.ToString().Trim() : ExpirationDate.ToString().Trim();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        //dt.Rows[0]["Flag"].ToString();
                    }

                    con.Close();
                }
            }
            catch (Exception exp)
            {
            }

            return dt;
        }

        private DataTable GetVI_SurchargesRenewalByFlag(string Flag)
        {
            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("Flag", SqlDbType.VarChar, 20, Flag.ToString(), ref cookItems);

                Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }
                DataTable dt = null;
                try
                {
                    dt = exec.GetQuery("GetVI_SurchargesRenewalByFlag", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    //throw new Exception("There is no information to display, please try again.", ex);
                }
				return dt;
				
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
