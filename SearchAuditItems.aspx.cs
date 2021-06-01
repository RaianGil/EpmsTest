using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Xml;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using EPolicy.XmlCooker;
using EPolicy.Customer;
using EPolicy.TaskControl;
using EPolicy.LookupTables;
using System.Web.Security;

namespace EPolicy
{
	/// <summary>
	/// Summary description for SearchAuditItems.
	/// </summary>
	/// 
    //


	public partial class SearchAuditItems : System.Web.UI.Page
	{
		private Customer.Customer _customer;
		private Prospect _prospect;
		private TaskControl.Payments _payment;
		private LookupTables.Agency _agency;
		private LookupTables.Agent _agent;
		private TaskControl.QuoteAuto _quote;
		private TaskControl.Policies _policies;
//		private Quotes.AutoDriver _AutoDriver;
		private LookupTables.Bank _bank;
		private LookupTables.CustomerCounter _customerCounter;
		private LookupTables.CompanyDealer _companyDealer;
		private LookupTables.InsuranceCompany _insuranceCompany;
		private LookupTables.Location _location;
		private LookupTables.MasterPolicy _masterPolicy;
		private LookupTables.VehicleModel _vehicleModel;
		private LookupTables.CommissionAgency _commissionAgency;
		private LookupTables.CommissionAgent _commissionAgent;
		private Customer.MasterCustomer _masterCustomer;
		private LookupTables.Supplier _supplier;
		private TaskControl.PaymentCertificationLetter _paymentCertificationLetter;
	
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
				if(!cp.IsInRole("AUDIT TRAIL") && !cp.IsInRole("ADMINISTRATOR"))
				{
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    Response.Cookies.Add(authCookies);
                    FormsAuthentication.SignOut();
					Response.Redirect("Default.aspx?001");
				}
			}
			if(!IsPostBack)
			{
				this.DisplayResults();
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

			//Setup Left-side Banner
			//Control LeftMenu = new Control();
			//LeftMenu = LoadControl(@"LeftMenu.ascx");
			//this.phTopBanner1.Controls.Add(LeftMenu);
			
			this.PopulateLocalOrSessionVars();
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.grdAuditItems.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.grdAuditItems_ItemCommand);

		}
		#endregion

		private void RenderHeading(string HeaderText)
		{
			this.lblHeading.Text = HeaderText.Trim();
		}

		private void DisplayResults()
		{
			Baldrich.DBRequest.DBRequest executor = new Baldrich.DBRequest.DBRequest();
			
			if(Request.QueryString["type"] != null)
			{
				switch(Request.QueryString["type"].Trim())
				{	
					case "0": //Prospect
						if(Request.QueryString["prospectID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["prospectID"].Trim(),"PROSPECT");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();

							this.BindGrid(dt);
							this.RenderHeading("Prospect Information History (" +
								((this._prospect.IsBusiness) ? 
								(this._prospect.BusinessName.Trim()) : 
								(this._prospect.FirstName.Trim() +
								" " + this._prospect.LastName1.Trim() + 
								" " + this._prospect.LastName2.Trim())) + 
								"): " +	dt.Rows.Count.ToString()
								+ " items.");
						}
						break;	
	
					case "1": //Customer
						if(Request.QueryString["customerNo"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["customerNo"].Trim(),"CUSTOMER");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();

							this.BindGrid(dt);
							this.RenderHeading("Customer Information History(" +
								((this._customer.IsBusiness) ? 
								(this._customer.EmplName.Trim()) : 
								(this._customer.FirstName.Trim() +
								" " + this._customer.LastName1.Trim() + 
								" " + this._customer.LastName2.Trim())) +
								"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;

					case "2": //Payment
						if(Request.QueryString["taskControlID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["taskControlID"].Trim(),"PAYMENTS");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Payment Information History (" +
								this._payment.TaskControlID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;

//					case "2": //Payment
//						if(Request.QueryString["taskPaymentID"] != null)
//						{
//								DataTable dt = 
//								executor.GetQuery("GetPaymentAuditFromTaskPaymentID", 
//									this.GetPaymentAuditXml());
//							this.BindGrid(dt);
//							this.RenderHeading("Payment Information Audit Trail (" +
//								this._payment.TaskPaymentID.ToString() +"): " +
//								dt.Rows.Count.ToString() + " items.");
//						}
//						break;

					case "3": //Agency
						if(Request.QueryString["AgencyID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["agencyID"].Trim(),"AGENCY");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Agency Information History (" +
								this._agency.AgencyID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;

					case "4": //Agent
						if(Request.QueryString["AgentID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["agenTID"].Trim(),"AGENT");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Agent Information History (" +
								this._agent.AgentID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;

					case "5": 
						break;

					case "6": //Bank
						if(Request.QueryString["BankID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["bankID"].Trim(),"BANK");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Bank Information History (" +
								this._bank.BankID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;

					case "7": //CustomerCounter
						if(Request.QueryString["CustomerCounterID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["customerCounterID"].Trim(),"CUSTOMER COUNTER");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Customer Counter Information History (" +
								this._customerCounter.CustomerCounterID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;        

					case "8": //CompanyDealer
						if(Request.QueryString["CompanyDealerID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["companyDealerID"].Trim(),"COMPANY DEALER");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Company Dealer Information History (" +
								this._companyDealer.CompanyDealerID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;

					case "9": //InsuranceCompany
						if(Request.QueryString["InsuranceCompanyID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["insuranceCompanyID"].Trim(),"INSURANCE COMPANY");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Insurance Company Information History (" +
								this._insuranceCompany.InsuranceCompanyID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;

					case "10": //Location
						if(Request.QueryString["LocationID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["locationID"].Trim(),"LOCATION");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Location Information History (" +
								this._location.LocationID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;

					case "11": //MasterPolicy
						if(Request.QueryString["MasterPolicyID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["masterPolicyID"].Trim(),"MASTER POLICY");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Master Policy Information History (" +
								this._masterPolicy.MasterPolicyID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;

					case "12": //VehicleModel
						if(Request.QueryString["VehicleModelID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["vehicleModelID"].Trim(),"VEHICLE MODEL");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Vehicle Model Information History (" +
								this._vehicleModel.VehicleModelID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;

					case "13": //CommissionAgency
						if(Request.QueryString["CommissionAgencyID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["commissionAgencyID"].Trim(),"COMMISSIONAGENCY");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Commission Agency Information History (" +
								this._commissionAgency.CommissionAgencyID.ToString() +"): Agency(" +
								this._commissionAgency.AgencyID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;

					case "14": //CommissionAgent
						if(Request.QueryString["CommissionAgentID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["commissionAgentID"].Trim(),"COMMISSION AGENT");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Commission Agent Information History (" +
								this._commissionAgent.CommissionAgentID.ToString() +"): Agent(" +
								this._commissionAgent.AgentID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;

					case "15": //MasterCustomer/CientMaster.aspx
						if(Request.QueryString["MasterCustomerID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["masterCustomerID"].Trim(),"MASTER CUSTOMER");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Master Client Information History (" +
								this._masterCustomer.MasterCustomerID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;
					
					case "16": 
						break;

//					case "17": //Personal Auto Quote
//						if(Request.QueryString["TID"] != null &&
//							Request.QueryString["QuoteID"] != null)
//						{
//							DataTable dt = executor.GetQuery( 
//								"GetAutoQuoteAuditFromQuoteID", 					
//								this.GetPersonalAutoQuoteAuditXml());
//							if(dt != null)
//							{
//								this.BindGrid(dt);
//								this.RenderHeading("Quote Information Audit Trail " + 
//									"(Task Control number " + 
//									Request.QueryString["TID"].ToString() + 
//									"): " +	dt.Rows.Count.ToString()
//									+ " items.");
//							}
//						}
//						break;

					case "17": //Personal Auto Quote
						if(Request.QueryString["TaskControlID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["taskControlID"].Trim(),"QUOTE");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							if(dt != null)
							{
								this.BindGrid(dt);
								this.RenderHeading("Quote Information History " + 
									"(Task Control Number " + 
									Request.QueryString["TaskControlID"].ToString() + 
									"): " +	dt.Rows.Count.ToString()
									+ " items.");
							}
						}
						break;

					case "18": //Supplier
						if(Request.QueryString["SupplierID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["supplierID"].Trim(),"SUPPLIER");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Supplier Information History (" +
								this._supplier.SupplierID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;

					case "19": 
						break;

					case "20": //Payment Certification Letter
						if(Request.QueryString["taskControlID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["taskControlID"].Trim(),"PAYMENT CERTIFICATION LETTER");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							this.BindGrid(dt);
							this.RenderHeading("Payment Certification Letter Information History (" +
								this._paymentCertificationLetter.TaskControlID.ToString() +"): " +
								dt.Rows.Count.ToString() + " items.");
						}
						break;
					

					case "22": //PersonalAutoPolicy
						if(Request.QueryString["TaskControlID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["taskControlID"].Trim(),"AUTOPERSONALPOLICY");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							if(dt != null)
							{
								this.BindGrid(dt);
								this.RenderHeading("Personal Auto Policy  Information History " + 
									"(Task Control number " + 
									Request.QueryString["TaskControlID"].ToString() + 
									"): " +	dt.Rows.Count.ToString()
									+ " items.");
							}
						}
						break;
					case "23": //AutoGap
						if(Request.QueryString["TaskControlID"] != null)
						{
							DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["taskControlID"].Trim(),"POLICIES");
								
							if(dt.Rows.Count != 0)
								TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
							if(dt != null)
							{
								this.BindGrid(dt);
								this.RenderHeading("Policies Information History " + 
									"(Control number " + 
									Request.QueryString["TaskControlID"].ToString() + 
									"): " +	dt.Rows.Count.ToString()
									+ " items.");
							}
						}
						break;

                        //Anadido por Yamil: VSC Maintenance
                    case "24": //Bank
                        if (Request.QueryString["BankID"] != null)
                        {
                            DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["bankID"].Trim(), "BANK");

                            if (dt.Rows.Count != 0)
                                TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
                            this.BindGrid(dt);
                            this.RenderHeading("Bank Information History (" +
                                this._bank.BankID.ToString() + "): " +
                                dt.Rows.Count.ToString() + " items.");
                        }
                        break;

                    case "25": //CompanyDealer
                        if (Request.QueryString["CompanyDealerID"] != null)
                        {
                            DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["companyDealerID"].Trim(), "COMPANY DEALER");

                            if (dt.Rows.Count != 0)
                                TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
                            this.BindGrid(dt);
                            this.RenderHeading("Company Dealer Information History (" +
                                this._companyDealer.CompanyDealerID.ToString() + "): " +
                                dt.Rows.Count.ToString() + " items.");
                        }
                        break;
                    case "26": //VehicleModel
                        if (Request.QueryString["VehicleModelID"] != null)
                        {
                            DataTable dt = Audit.History.GetHistoryByKeyIDAndSubject(Request.QueryString["vehicleModelID"].Trim(), "VEHICLE MODEL");

                            if (dt.Rows.Count != 0)
                                TxtNotes.Text = dt.Rows[0]["Notes"].ToString().Trim();
                            this.BindGrid(dt);
                            this.RenderHeading("Vehicle Model Information History (" +
                                this._vehicleModel.VehicleModelID.ToString() + "): " +
                                dt.Rows.Count.ToString() + " items.");
                        }
                        break;
					default:
						break;

				}
			}
		}

		private void BindGrid(DataTable DtResult)
		{
			this.grdAuditItems.DataSource = DtResult;
			this.grdAuditItems.DataBind();
		}

		private XmlDocument GetCustomerAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("CustomerNo", 
				SqlDbType.Char, 10, Request.QueryString["customerNo"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}

		private XmlDocument GetApplicationAutoAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("TaskApplicationAutoID", 
				SqlDbType.Int, 0, Request.QueryString["taskApplicationAutoID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}

		private XmlDocument GetPaymentAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("TaskPaymentID", 
				SqlDbType.Int, 0, Request.QueryString["taskPaymentID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}

		private XmlDocument GetProspectAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("ProspectID", 
				SqlDbType.Int, 0, Request.QueryString["prospectID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}

		private XmlDocument GetPersonalAutoQuoteAuditXml()
		{
			if(Request.QueryString["QuoteID"] != null)
			{
				DbRequestXmlCookRequestItem[] cookItems = 
					new DbRequestXmlCookRequestItem[1];
			
				DbRequestXmlCooker.AttachCookItem("QuoteID", 
					SqlDbType.Int, 0, Request.QueryString["QuoteID"].Trim(), 
					ref cookItems);
			
				return DbRequestXmlCooker.Cook(cookItems);
			}
			else
			{
				DbRequestXmlCookRequestItem[] cookItems = 
					new DbRequestXmlCookRequestItem[1];
			
				DbRequestXmlCooker.AttachCookItem("QuoteID", 
					SqlDbType.Int, 0, "0", 
					ref cookItems);
				return DbRequestXmlCooker.Cook(cookItems);
			}
		}

		private XmlDocument GetAgencyAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("AgencyID", 
				SqlDbType.Char, 3, Request.QueryString["agencyID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
}

		private XmlDocument GetAgentAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("AgentID", 
				SqlDbType.Char, 3, Request.QueryString["agentID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
	}

		private XmlDocument GetBankAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("BankID", 
				SqlDbType.Char, 3, Request.QueryString["bankID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
	    }

		private XmlDocument GetCustomerCounterAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("CustomerCounterID", 
				SqlDbType.Int, 0, Request.QueryString["customerCounterID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}

		private XmlDocument GetCompanyDealerAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("CompanyDealerID", 
				SqlDbType.Char, 3, Request.QueryString["companyDealerID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}

		private XmlDocument GetInsuranceCompanyAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("InsuranceCompanyID", 
				SqlDbType.Char, 3, Request.QueryString["insuranceCompanyID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}

		private XmlDocument GetLocationAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("LocationID", 
				SqlDbType.Int, 0, Request.QueryString["locationID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}


		private XmlDocument GetMasterPolicyAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("MasterPolicyID", 
				SqlDbType.Char, 4, Request.QueryString["masterPolicyID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}

		private XmlDocument GetVehicleModelAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("VehicleModelID", 
				SqlDbType.Int, 0, Request.QueryString["vehicleModelID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}


		private XmlDocument GetCommissionAgencyAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("CommissionAgencyID", 
				SqlDbType.Int, 0, Request.QueryString["commissionAgencyID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}

		private XmlDocument GetCommissionAgentAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("CommissionAgentID", 
				SqlDbType.Int, 0, Request.QueryString["commissionAgentID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}

		private XmlDocument GetMasterCustomerAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("MasterCustomerID", 
				SqlDbType.Int, 0, Request.QueryString["masterCustomerID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}

		private XmlDocument GetEtchAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("TaskControlID", 
				SqlDbType.Int, 0, Request.QueryString["taskControlID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}


		private XmlDocument GetSupplierAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("SupplierID", 
				SqlDbType.Char, 3, Request.QueryString["supplierID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}

		private XmlDocument GetIncentiveSupplierAuditXml()
		{
			DbRequestXmlCookRequestItem[] cookItems = 
				new DbRequestXmlCookRequestItem[1];
			
			DbRequestXmlCooker.AttachCookItem("IncentiveSupplierID", 
				SqlDbType.Int, 0, Request.QueryString["incentiveSupplierID"].Trim(), 
				ref cookItems);
			
			return DbRequestXmlCooker.Cook(cookItems);
		}

		private void PaymentRedirect()
		{
			Response.Redirect("Payments.aspx");
		}

		private void ClientMasterRedirect()
		{
			Response.Redirect("ClientMaster.aspx");
		}

		private void QuoteAutoRedirect()
		{
			Response.Redirect("ExpressAutoQuote.aspx");
		}

		private void AutoPersonalPolicyRedirect()
		{
			Response.Redirect("QuoteAuto.aspx");
		}

		private void PoliciesRedirect()
		{
			Response.Redirect("Policies.aspx");
		}

		private void ApplicationAutoRedirect()
		{
			Response.Redirect("ApplicationAuto.aspx");
		}

		private void PaymentCertificationLetterRedirect()
		{
			Response.Redirect("PaymentCertificationLetter.aspx");
		}

		private void AgencyRedirect()
		{
			LookupTables.Agency agency = 
				(LookupTables.Agency)Session["Agency"];

			Response.Redirect(LookupTables.LookupTables.GetTableMaintenancePathFromTableID(25) + "?item=" + lblHeading.Text);
		}

		private void AgentRedirect()
		{
			LookupTables.Agent agent = 
				(LookupTables.Agent)Session["Agent"];

			Response.Redirect(LookupTables.LookupTables.GetTableMaintenancePathFromTableID(26) + "?item=" + lblHeading.Text);
		}

		private void BankRedirect()
		{
			LookupTables.Bank bank = 
				(LookupTables.Bank)Session["Bank"];

			Response.Redirect(LookupTables.LookupTables.GetTableMaintenancePathFromTableID(27) + "?item=" + lblHeading.Text);

		}

        private void VSCBankRedirect()
        {
            LookupTables.Bank bank =
                (LookupTables.Bank)Session["Bank"];

            Response.Redirect(LookupTables.LookupTables.GetVSCTableMaintenancePathFromTableID(4) + "?item=" + lblHeading.Text);

        }

		private void CompanyDealerRedirect()
		{
			LookupTables.CompanyDealer companyDealer = 
				(LookupTables.CompanyDealer)Session["CompanyDealer"];

			Response.Redirect(LookupTables.LookupTables.GetTableMaintenancePathFromTableID(29) + "?item=" + lblHeading.Text);

		}

        private void VSCCompanyDealerRedirect()
        {
            LookupTables.CompanyDealer companyDealer =
                (LookupTables.CompanyDealer)Session["CompanyDealer"];

            Response.Redirect(LookupTables.LookupTables.GetVSCTableMaintenancePathFromTableID(1) + "?item=" + lblHeading.Text);

        }

		private void InsuranceCompanyRedirect()
		{
			LookupTables.InsuranceCompany insuranceCompany = 
				(LookupTables.InsuranceCompany)Session["InsuranceCompany"];

			Response.Redirect(LookupTables.LookupTables.GetTableMaintenancePathFromTableID(30) + "?item=" + lblHeading.Text);

		}

		private void LocationRedirect()
		{
			LookupTables.Location location = 
				(LookupTables.Location)Session["Location"];

			Response.Redirect(LookupTables.LookupTables.GetTableMaintenancePathFromTableID(71) + "?item=" + lblHeading.Text);

		}

		private void CustomerCounterRedirect()
		{
			LookupTables.CustomerCounter customerCounter = 
				(LookupTables.CustomerCounter)Session["CustomerCounter"];

			Response.Redirect(LookupTables.LookupTables.GetTableMaintenancePathFromTableID(69) + "?item=" + lblHeading.Text);

		}

		private void MasterPolicyRedirect()
		{
			LookupTables.MasterPolicy masterPolicy = 
				(LookupTables.MasterPolicy)Session["MasterPolicy"];

			Response.Redirect(LookupTables.LookupTables.GetTableMaintenancePathFromTableID(68) + "?item=" + lblHeading.Text);

		}

		private void VehicleModelRedirect()
		{
			LookupTables.VehicleModel vehicleModel = 
				(LookupTables.VehicleModel)Session["VehicleModel"];

			Response.Redirect(LookupTables.LookupTables.GetTableMaintenancePathFromTableID(22) + "?item=" + lblHeading.Text);
		}

        private void VSCVehicleModelRedirect()
        {
            LookupTables.VehicleModel vehicleModel =
                (LookupTables.VehicleModel)Session["VehicleModel"];

            Response.Redirect(LookupTables.LookupTables.GetVSCTableMaintenancePathFromTableID(2) + "?item=" + lblHeading.Text);
        }

		private void CommissionAgencyRedirect()
		{
			LookupTables.CommissionAgency commissionAgency = 
				(LookupTables.CommissionAgency)Session["CommissionAgency"];

                   Response.Redirect("CommissionAgency.aspx?item= + this.lblHeading.Text");
		}


		private void CommissionAgentRedirect()
		{
			LookupTables.CommissionAgent commissionAgent = 
				(LookupTables.CommissionAgent)Session["CommissionAgent"];

					Response.Redirect("CommissionAgent.aspx?item= + this.lblHeading.Text");
		}


		private void ProspectRedirect(Prospect Prospect)
		{
			switch(Prospect.IsBusiness)
			{
				case true:
					Response.Redirect("ProspectBusiness.aspx");
					break;
				case false:
					Response.Redirect("ProspectIndividual.aspx");
					break;
			}
		}

		private void CustomerRedirect(Customer.Customer Customer)
		{
			switch(Customer.IsBusiness)
			{
				case true:
					Response.Redirect("ClientBusiness.aspx");
					break;
				case false:
					Response.Redirect("ClientIndividual.aspx");
					break;
			}
		}

		private void EtchRedirect()
		{
			Response.Redirect("Etch.aspx");
		}

		private void SupplierRedirect()
		{
			LookupTables.Supplier supplier = 
				(LookupTables.Supplier)Session["Supplier"];

			Response.Redirect(LookupTables.LookupTables.GetTableMaintenancePathFromTableID(75) + "?item=" + lblHeading.Text);
		}

		private void EndorsementAutoRedirect()
		{
			Response.Redirect("EndorsementAuto.aspx");	
		}

		private void PopulateLocalOrSessionVars()
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

			if(Request.QueryString["type"] != null)
			{
				switch(Request.QueryString["type"])
				{
					case "0": //Prospect
						if(Session["Prospect"] == null)
						{
							if(Request.QueryString["prospectID"] != null)
							{
								this._prospect = new Prospect();
								this._prospect.GetProspect(
									int.Parse(
									Request.QueryString["ProspectID"].Trim()));
								Session["Prospect"] = this._prospect;
							}
						}
						else
						{
							this._prospect = (Prospect)Session["Prospect"];
						}
						break;
				
					case "1": //Customer
						if(Session["Customer"] == null)
						{
							if(Request.QueryString["customerNo"] != null)
							{
								this._customer = Customer.Customer.GetCustomer(
									Request.QueryString["customerNo"].Trim(), userID);
								Session["Customer"] = this._customer;
							}
						}
						else
						{
							this._customer = (Customer.Customer)Session["Customer"];
						}
						break;
					
					case "2": //Payment
						if(Session["TaskControl"] == null)
						{
							if(Request.QueryString["taskPaymentID"] != null)
							{
								this._payment = TaskControl.Payments.GetPayments(
									int.Parse(Request.QueryString["taskPaymentID"].Trim()));
								Session["TaskControl"] = this._payment;
							}
						}
						else
						{
							this._payment = (TaskControl.Payments)Session["TaskControl"];
						}
						break;
					
					case "3": //Agency
						if(Session["Agency"] == null)
						{
							if(Request.QueryString["agencyID"] != null)
							{
								this._agency = new LookupTables.Agency();
								this._agency.GetAgency(Request.QueryString["agencyID"].Trim());
								Session["Agency"] = this._agency;
							}
						}
						else
						{
							this._agency = (LookupTables.Agency)Session["Agency"];
						}
						break;

					case "4": //Agent
						if(Session["Agent"] == null)
						{
							if(Request.QueryString["agentID"] != null)
							{
								this._agent = new LookupTables.Agent();
								this._agent.GetAgent(Request.QueryString["agentID"].Trim());
								Session["Agent"] = this._agent;
							}
						}
						else
						{
							this._agent = (LookupTables.Agent)Session["Agent"];
						}
						break;

					case "5": 
						break;

					case "6": //Bank
						if(Session["Bank"] == null)
						{
							if(Request.QueryString["bankID"] != null)
							{
								this._bank = new LookupTables.Bank();
								this._bank.GetBank(Request.QueryString["bankID"].Trim());
								Session["Bank"] = this._bank;
							}
						}
						else
						{
							this._bank = (LookupTables.Bank)Session["Bank"];
						}
						break;
				
					case "7": //CustomerCounter
						if(Session["CustomerCounter"] == null)
						{
							if(Request.QueryString["customerCounterID"] != null)
							{
								this._customerCounter = new LookupTables.CustomerCounter();
							this._customerCounter.GetCustomerCounterByCustomerCounterID(int.Parse(Request.QueryString["customerCounterID"].ToString().Trim()));
								Session["CustomerCounter"] = this._customerCounter;
							}
						}
						else
						{
							this._customerCounter = (LookupTables.CustomerCounter)Session["CustomerCounter"];
						}
						break;

					case "8": //CompanyDealer
						if(Session["CompanyDealer"] == null)
						{
							if(Request.QueryString["companyDealerID"] != null)
							{
								this._companyDealer = new LookupTables.CompanyDealer();
								this._companyDealer.GetCompanyDealer(Request.QueryString["companyDealerID"].Trim());
								Session["CompanyDealer"] = this._companyDealer;
							}
						}
						else
						{
							this._companyDealer = (LookupTables.CompanyDealer)Session["CompanyDealer"];
						}
						break;

					case "9": //InsuranceCompany
						if(Session["InsuranceCompany"] == null)
						{
							if(Request.QueryString["insuranceCompanyID"] != null)
							{
								this._insuranceCompany = new LookupTables.InsuranceCompany();
								this._insuranceCompany.GetInsuranceCompany(Request.QueryString["insuranceCompanyID"].Trim());
								Session["InsuranceCompany"] = this._insuranceCompany;
							}
						}
						else
						{
							this._insuranceCompany = (LookupTables.InsuranceCompany)Session["InsuranceCompany"];
						}
						break;

					case "10": //Location
						if(Session["Location"] == null)
						{
							if(Request.QueryString["locationID"] != null)
							{
								this._location = new LookupTables.Location();
								this._location.GetLocation(int.Parse(Request.QueryString["locationID"].Trim()));
								Session["Location"] = this._location;
							}
						}
						else
						{
							this._location = (LookupTables.Location)Session["Location"];
						}
						break;

					case "11": //MasterPolicy
						if(Session["MasterPolicy"] == null)
						{
							if(Request.QueryString["masterPolicyID"] != null)
							{
								this._masterPolicy = new LookupTables.MasterPolicy();
								this._masterPolicy.GetMasterPolicy(Request.QueryString["masterPolicyID"].Trim());
								Session["Location"] = this._location;
							}
						}
						else
						{
							this._masterPolicy = (LookupTables.MasterPolicy)Session["MasterPolicy"];
						}
						break;

					case "12": //VehicleModel
						if(Session["VehicleModel"] == null)
						{
							if(Request.QueryString["vehicleModelID"] != null)
							{
								this._vehicleModel = new LookupTables.VehicleModel();
								this._vehicleModel.GetVehicleModel(int.Parse(Request.QueryString["vehicleModelID"].Trim()));
								Session["VehicleModel"] = this._vehicleModel;
							}
						}
						else
						{
							this._vehicleModel = (LookupTables.VehicleModel)Session["VehicleModel"];
						}
						break;

					case "13": //CommissionAgency
						if(Session["CommissionAgency"] == null)
						{
							if(Request.QueryString["commissionAgencyID"] != null)
							{
//								this._commissionAgency = new LookupTables.CommissionAgency();
								this._commissionAgency.GetCommissionAgencyByCommissionAgencyID(int.Parse(Request.QueryString["commissionAgencyID"].Trim()));
								Session["CommisionAgency"] = this._commissionAgency;
							}
						}
						else
						{
							this._commissionAgency = (LookupTables.CommissionAgency)Session["CommissionAgency"];
						}
						break;

					case "14": //CommissionAgent 
						if(Session["CommissionAgent"] == null)
						{
							if(Request.QueryString["commissionAgentID"] != null)
							{
//								this._commissionAgent = new LookupTables.CommissionAgent();
								this._commissionAgent.GetCommissionAgentByCommissionAgentID(int.Parse(Request.QueryString["commissionAgentID"].ToString().Trim()));
								Session["CommissionAgent"] = this._commissionAgent;
							}
						}
						else
						{
							this._commissionAgent = (LookupTables.CommissionAgent)Session["CommissionAgent"];
						}
						break;

					case "15": //MasterCustomer/ClientMaster.aspx
						if(Session["MasterCustomer"] == null)
						{
							if(Request.QueryString["masterCustomerID"] != null)
							{

								//this._masterCustomer = new MasterCustomer();//this._masterCustomer.GetType 
								this._masterCustomer = Customer.MasterCustomer.GetMasterCustomer(
									int.Parse(
									Request.QueryString["MasterCustomerID"].Trim()));
								Session["MasterCustomer"] = this._masterCustomer;
							}
						}
						else
						{
							this._masterCustomer = (Customer.MasterCustomer)Session["MasterCustomer"];
						}
						break;

					case "16": 
						break;

					case "17": //Quote
						if(Session["TaskControl"] == null)
						{
							if(Request.QueryString["TaskControlID"] != null)
							{
								this._quote = TaskControl.QuoteAuto.GetQuoteAuto(
									int.Parse(Request.QueryString["TaskControlID"].Trim()),userID,false);
								Session["TaskControl"] = this._quote;
							}
						}
						else
						{
							this._quote = (TaskControl.QuoteAuto)Session["TaskControl"];
						}
						break;

					case "18": //Supplier
						if(Session["Supplier"] == null)
						{
							if(Request.QueryString["supplierID"] != null)
							{
								this._supplier = new LookupTables.Supplier();
								this._supplier.GetSupplier(Request.QueryString["supplierID"].Trim());
								Session["Supplier"] = this._supplier;
							}
						}
						else
						{
							this._supplier = (LookupTables.Supplier)Session["Supplier"];
						}
						break;

					case "19": 
						break;

					case "20": //PaymentCetificationLetter
						if(Session["TaskControl"] == null)
						{
							if(Request.QueryString["paymentCertificationLetterID"] != null)
							{
								this._paymentCertificationLetter = TaskControl.PaymentCertificationLetter.GetPaymentCertificationLetter(
									int.Parse(Request.QueryString["taskControlID"].Trim()));
								Session["TaskControl"] = this._paymentCertificationLetter;
							}
						}
						else
						{
							this._paymentCertificationLetter = (TaskControl.PaymentCertificationLetter)Session["TaskControl"];
						}
						break;

					

					case "22": //AutoPersonalPolicy
						if(Session["TaskControl"] == null)
						{
							if(Request.QueryString["TaskControlID"] != null)
							{
								this._quote = TaskControl.QuoteAuto.GetQuoteAuto(
									int.Parse(Request.QueryString["TaskControlID"].Trim()),userID,false);
								Session["TaskControl"] = this._quote;
							}
						}
						else
						{
							this._quote = (TaskControl.QuoteAuto)Session["TaskControl"];
						}
						break;

					case "23": //Policies
						if(Session["TaskControl"] == null)
						{
							if(Request.QueryString["TaskControlID"] != null)
							{
								this._policies = TaskControl.Policies.GetPolicies(
									int.Parse(Request.QueryString["TaskControlID"].Trim()));
								Session["TaskControl"] = this._policies;
							}
						}
						else
						{
							this._policies = (TaskControl.Policies) Session["TaskControl"];
						}
						break;

                        //Anadido por Yamil: VSC Maintenance
                    case "24": //VSCBank
                        if (Session["Bank"] == null)
                        {
                            if (Request.QueryString["bankID"] != null)
                            {
                                this._bank = new LookupTables.Bank();
                                this._bank.GetBank(Request.QueryString["bankID"].Trim());
                                Session["Bank"] = this._bank;
                            }
                        }
                        else
                        {
                            this._bank = (LookupTables.Bank)Session["Bank"];
                        }
                        break;

                    case "25": //CompanyDealer
                        if (Session["CompanyDealer"] == null)
                        {
                            if (Request.QueryString["companyDealerID"] != null)
                            {
                                this._companyDealer = new LookupTables.CompanyDealer();
                                this._companyDealer.GetCompanyDealer(Request.QueryString["companyDealerID"].Trim());
                                Session["CompanyDealer"] = this._companyDealer;
                            }
                        }
                        else
                        {
                            this._companyDealer = (LookupTables.CompanyDealer)Session["CompanyDealer"];
                        }
                        break;

                    case "26": //VehicleModel
                        if (Session["VehicleModel"] == null)
                        {
                            if (Request.QueryString["vehicleModelID"] != null)
                            {
                                this._vehicleModel = new LookupTables.VehicleModel();
                                this._vehicleModel.GetVehicleModel(int.Parse(Request.QueryString["vehicleModelID"].Trim()));
                                Session["VehicleModel"] = this._vehicleModel;
                            }
                        }
                        else
                        {
                            this._vehicleModel = (LookupTables.VehicleModel)Session["VehicleModel"];
                        }
                        break;

				}
			}
		}

		

		protected void btnBack_Click(object sender, System.EventArgs e)
		{
			if(Request.QueryString["type"] != null)
			{
				switch(Request.QueryString["type"])
				{
					case "0": //Prospect
						this.ProspectRedirect(this._prospect);
						break;
					case "1": //Customer
						this.CustomerRedirect(this._customer);
						break;
					case "2": //Payment
						this.PaymentRedirect();
						break;
					case "3": //Agency
						this.AgencyRedirect();
						break;	
					case "4": //Agent
						this.AgentRedirect();
						break;	
					case "5": //Application Auto
						this.ApplicationAutoRedirect();
						break;	
					case "6": //Bank
						this.BankRedirect();
						break;
					case "7": //CustomerCounter
						this.CustomerCounterRedirect();
						break;
					case "8": //CompanyDealer
						this.CompanyDealerRedirect();
						break;
					case "9": //InsuranceCompany
						this.InsuranceCompanyRedirect();
						break;
					case "10": //Location
						this.LocationRedirect();
						break;
					case "11": //MasterPolicy
						this.MasterPolicyRedirect();
						break;
					case "12": //VehicleModel
						this.VehicleModelRedirect();
						break;
					case "13": //CommissionAgency/Agency.aspx
						this.CommissionAgencyRedirect();
						break;
					case "14": //CommissionAgent/Agent.aspx
						this.CommissionAgentRedirect();
						break;
					case "15": //MasterCustomer/ClientMaster.aspx
						this.ClientMasterRedirect();
						break;
					case "16": //Etch/Etch.aspx
						this.EtchRedirect();
						break;
					case "17": //QuoteAuto.aspx
						this.QuoteAutoRedirect();
						//Response.Redirect("ExpressAutoQuote.aspx");
						break;
					case "18": //Supplier
						this.SupplierRedirect();
						break;	
					case "19": 
						break;
					case "20": //PaymentCertificationLetter
						this.PaymentCertificationLetterRedirect();
						break;
					case "21": //EndorsementAuto
						this.EndorsementAutoRedirect();
						break;
					case "22": //AutoPersonalPolicy
						this.AutoPersonalPolicyRedirect();
						break;
					case "23": //Policies
						this.PoliciesRedirect();
						break;

                        //Anadido por Yamil: VSC Maintenance
                    case "24": //VSCBank
                        this.VSCBankRedirect();
                        break;
                    case "25": //VSCCompanyDealer
                        this.VSCCompanyDealerRedirect();
                        break;
                    case "26": //VSCVehicleModel
                        this.VSCVehicleModelRedirect();
                        break;


					default:
						break;
				}
			}
		}


        protected void grdAuditItems_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.Item.ItemType.ToString() != "Pager") // Select
            {
                TxtNotes.Text = e.Item.Cells[7].Text;
            }
            //else  // Pager
            //{
            //    grdAuditItems.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;
            //    grdAuditItems.DataBind();
            //}
        }
        protected void grdAuditItems_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DisplayResults();
            grdAuditItems.CurrentPageIndex = e.NewPageIndex;
            grdAuditItems.DataBind();
        }
}
}
