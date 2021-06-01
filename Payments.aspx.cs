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
using EPolicy2.Reports;

namespace EPolicy
{
	/// <summary>
	/// Summary description for Payments.
	/// </summary>
	public partial class Payments : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox TxtPayeeName;
		private Control LeftMenu;

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
			phTopBanner1.Controls.Add(LeftMenu);


			//Load DownDropList
			DataTable dtPolicyClass			= LookupTables.LookupTables.GetTable("PolicyClass");
			DataTable dtCreditDebit			= LookupTables.LookupTables.GetTable("CreditDebit");
			DataTable dtTaskStatus			= LookupTables.LookupTables.GetTable("TaskStatus"); //LookupTables.LookupTables.GetTableTaskStatusByTaskType(3);
			DataTable dtBank				= LookupTables.LookupTables.GetTable("Bank");
            DataTable dtAdjustmentType = LookupTables.LookupTables.GetTable("PaymentAdjustmentType");
            DataTable dtCompanyDealer = LookupTables.LookupTables.GetTable("CompanyDealer");
            DataTable dtInsuranceCompany = LookupTables.LookupTables.GetTable("InsuranceCompany");

			//PolicyClass
			ddlPolicyClass.DataSource = dtPolicyClass;
			ddlPolicyClass.DataTextField = "PolicyClassDesc";
			ddlPolicyClass.DataValueField = "PolicyClassID";
			ddlPolicyClass.DataBind();
			ddlPolicyClass.SelectedIndex = -1;
			ddlPolicyClass.Items.Insert(0,"");

			//CreditDebit
			ddlCreditDebit.DataSource = dtCreditDebit;
			ddlCreditDebit.DataTextField = "CreditDebitDesc";
			ddlCreditDebit.DataValueField = "CreditDebitID";
			ddlCreditDebit.DataBind();
			ddlCreditDebit.SelectedIndex = -1;
			ddlCreditDebit.Items.Insert(0,"");

			//TaskStatus
			ddlTaskStatus.DataSource = dtTaskStatus;
			ddlTaskStatus.DataTextField = "TaskStatusDesc";
			ddlTaskStatus.DataValueField = "TaskStatusID";
			ddlTaskStatus.DataBind();
			ddlTaskStatus.SelectedIndex = -1;
			ddlTaskStatus.Items.Insert(0,"");

			//Bank
			ddlBank.DataSource = dtBank;
			ddlBank.DataTextField = "BankDesc";
			ddlBank.DataValueField = "BankID";
			ddlBank.DataBind();
			ddlBank.SelectedIndex = -1;
			ddlBank.Items.Insert(0,"");

            //AdjustmentType
            ddlAdjustmentType.DataSource = dtAdjustmentType;
            ddlAdjustmentType.DataTextField = "AdjustmentTypeDesc";
            ddlAdjustmentType.DataValueField = "AdjustmentTypeID";
            ddlAdjustmentType.DataBind();
            ddlAdjustmentType.SelectedIndex = -1;
            ddlAdjustmentType.Items.Insert(0, "");

            //CompanyDealer
             ddlCompanyDealer.DataSource = dtCompanyDealer;
             ddlCompanyDealer.DataTextField = "CompanyDealerDesc";
             ddlCompanyDealer.DataValueField = "CompanyDealerID";
             ddlCompanyDealer.DataBind();
             ddlCompanyDealer.SelectedIndex = -1;
             ddlCompanyDealer.Items.Insert(0, "");

             //InsuranceCompany
             ddlInsuranceCompany.DataSource = dtInsuranceCompany;
             ddlInsuranceCompany.DataTextField = "InsuranceCompanyDesc";
             ddlInsuranceCompany.DataValueField = "InsuranceCompanyID";
             ddlInsuranceCompany.DataBind();
             ddlInsuranceCompany.SelectedIndex = -1;
             ddlInsuranceCompany.Items.Insert(0, "");
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
			this.litPopUp.Visible = false;

			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
				Response.Redirect("HomePage.aspx?001");
			}
			else
			{
				if(!cp.IsInRole("PAYMENTS") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("HomePage.aspx?001");
				}
			}

			if(Session["AutoPostBack"] == null)
			{
				if(!IsPostBack)
				{
					if(Session["TaskControl"] != null)
					{
						TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];
                        
						switch(taskControl.Mode) 
						{
							case 1:  //ADD
								//Verifica si puede añadir o modificar pagos.
								if(!cp.IsInRole("EDITPAYMENTS") && !cp.IsInRole("ADMINISTRATOR"))
								{
									Response.Redirect("HomePage.aspx?001");
								}

								FillTextControl();
								EnableControls();
								break;
						
							case 2: //Update
								FillTextControl();
								EnableControls();
								break;

							default:

								FillTextControl();

								DisableControls();
								break;
						}
					}
                    this.TxtNamePayee.Text = this.TxtName.Text.ToString() + " " + this.TxtLastName1.Text.ToString() + " " + TxtLastName2.Text.ToString();
                    this.TxtName.Enabled = false;
				}
				else
				{
					if(Session["TaskControl"] != null)
					{
						TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];
						if(taskControl.Mode ==4)
						{
							DisableControls();
						}
					}
				}
			}
			else
			{
				FillTextControl();
				EnableControls();
				Session.Remove("AutoPostBack");
                
			}

		}

		public void FillTextControl()
		{
			TaskControl.Payments payments = (TaskControl.Payments) Session["TaskControl"];
			
			//PolicyClass
			ddlPolicyClass.SelectedIndex = 0;
			if(payments.PolicyClassID != 0)
			{
				for(int i = 0;ddlPolicyClass.Items.Count-1 >= i ;i++)
				{
					if (ddlPolicyClass.Items[i].Value == payments.PolicyClassID.ToString())
					{
						ddlPolicyClass.SelectedIndex = i;
						i = ddlPolicyClass.Items.Count-1;
					}
				}
			}

			
			//CreditDebit
			ddlCreditDebit.SelectedIndex = 0;
			if(payments.CreditDebitID != 0)
			{
				for(int i = 0;ddlCreditDebit.Items.Count-1 >= i ;i++)
				{
					if (ddlCreditDebit.Items[i].Value == payments.CreditDebitID.ToString())
					{
						ddlCreditDebit.SelectedIndex = i;
						i = ddlCreditDebit.Items.Count-1;
					}
				}
			}

			//TaskStatus
			ddlTaskStatus.SelectedIndex = 0;
			if(payments.TaskStatusID != 0)
			{
				for(int i = 0;ddlTaskStatus.Items.Count-1 >= i ;i++)
				{
					if (ddlTaskStatus.Items[i].Value == payments.TaskStatusID.ToString())
					{
						ddlTaskStatus.SelectedIndex = i;
						i = ddlTaskStatus.Items.Count-1;
					}
				}
			}

			//Bank
			ddlBank.SelectedIndex = 0;
			if(payments.Bank != "000")
			{
				for(int i = 0;ddlBank.Items.Count-1 >= i ;i++)
				{
					if (ddlBank.Items[i].Value == payments.Bank.ToString())
					{
						ddlBank.SelectedIndex = i;
						i = ddlBank.Items.Count-1;
					}
				}
			}

            //CompanyDealer
            ddlCompanyDealer.SelectedIndex = 0;
            if (payments.CompanyDealer != "000")
            {
                for (int i = 0; ddlCompanyDealer.Items.Count - 1 >= i; i++)
                {
                    if (ddlCompanyDealer.Items[i].Value == payments.CompanyDealer.ToString())
                    {
                        ddlCompanyDealer.SelectedIndex = i;
                        i = ddlCompanyDealer.Items.Count - 1;
                    }
                }
            }

			if(Session["PolicyInfo"] != null) //Obtiene informacion desde la poliza (hdpolicy). Desde ViewOtherPaymentInfo.aspx.
			{
				string[] policyInfo = (string[]) Session["PolicyInfo"];
				Session.Remove("PolicyInfo");

				TxtPolicyType.Text	= policyInfo[0].ToString();
				txtPolicyNo.Text    = policyInfo[1].ToString();
				txtCertificate.Text = policyInfo[2].ToString();
				TxtSuffix.Text		= policyInfo[3].ToString();
				TxtCustomerNo.Text	= policyInfo[4].ToString();  
				TxtName.Text		= policyInfo[5].ToString();  
				TxtLastName1.Text	= policyInfo[6].ToString();  
				TxtLastName2.Text	= policyInfo[7].ToString(); 
				TxtLoanNo.Text		= policyInfo[8].ToString();
				payments.Bank		= policyInfo[9].ToString();
                payments.PolicyClassID = int.Parse(policyInfo[10].ToString());
                payments.CompanyDealer = policyInfo[11].ToString();
                payments.InsuranceCompany = policyInfo[12].ToString();

				ddlBank.SelectedIndex = 0;
				if(payments.Bank != "000")
				{
					for(int i = 0;ddlBank.Items.Count-1 >= i ;i++)
					{
						if (ddlBank.Items[i].Value == payments.Bank.ToString())
						{
							ddlBank.SelectedIndex = i;
							i = ddlBank.Items.Count-1;
						}
					}
				}

                //InsuranceCompany
                ddlInsuranceCompany.SelectedIndex = 0;
                if (payments.InsuranceCompany != "000")
                {
                    for (int i = 0; ddlInsuranceCompany.Items.Count - 1 >= i; i++)
                    {
                        if (ddlInsuranceCompany.Items[i].Value == payments.InsuranceCompany.ToString())
                        {
                            ddlInsuranceCompany.SelectedIndex = i;
                            i = ddlInsuranceCompany.Items.Count - 1;
                        }
                    }
                }

                //CompanyDealer
                ddlCompanyDealer.SelectedIndex = 0;
                if (payments.CompanyDealer != "000")
                {
                    for (int i = 0; ddlCompanyDealer.Items.Count - 1 >= i; i++)
                    {
                        if (ddlCompanyDealer.Items[i].Value == payments.CompanyDealer.ToString())
                        {
                            ddlCompanyDealer.SelectedIndex = i;
                            i = ddlCompanyDealer.Items.Count - 1;
                        }
                    }
                }

                //PolicyClass
                ddlPolicyClass.SelectedIndex = 0;
                if (payments.PolicyClassID != 0)
                {
                    for (int i = 0; ddlPolicyClass.Items.Count - 1 >= i; i++)
                    {
                        if (ddlPolicyClass.Items[i].Value == payments.PolicyClassID.ToString())
                        {
                            ddlPolicyClass.SelectedIndex = i;
                            i = ddlPolicyClass.Items.Count - 1;
                        }
                    }
                }

				payments.CustomerNo = TxtCustomerNo.Text.ToUpper();
				payments.Customer.CustomerNo = TxtCustomerNo.Text;
				payments.Customer.FirstName = TxtName.Text;
				payments.Customer.LastName1 = TxtLastName1.Text;
				payments.Customer.LastName2 = TxtLastName2.Text;
				payments.Customer.SocialSecurity = TxtSocialSecurity.Text;
				
				Session.Add("TaskControl",payments);
			}
			else
			{
				txtPolicyNo.Text    = payments.PolicyNo;
				TxtPolicyType.Text	= payments.PolicyType;
				txtCertificate.Text = payments.Certificate;
				TxtSuffix.Text		= payments.Sufijo;
				TxtLoanNo.Text		= payments.LoanNumber;
			}

			lblTaskControlID.Text	 = payments.TaskControlID.ToString();
			txtOriginalPolicyNo.Text = payments.OriginalPolicyNo;
			txtComments.Text	= payments.Comments;
			//TxtComments1.Text	= payments.Comments1;
			txtDepositDate.Text	= payments.DepositDate;
			
			if(payments.CustomerNo!="")
			{
				TxtCustomerNo.Text		= payments.Customer.CustomerNo;
				TxtName.Text			= payments.Customer.FirstName;
				TxtLastName1.Text		= payments.Customer.LastName1;
				TxtLastName2.Text		= payments.Customer.LastName2;
				TxtSocialSecurity.Text	= payments.Customer.SocialSecurity;
			}
			else
			{
				TxtCustomerNo.Text		= "";
				TxtName.Text			= "";
				TxtLastName1.Text		= "";
				TxtLastName2.Text		= "";
				TxtSocialSecurity.Text	= "";
			}
            
			TxtAging.Text		  = payments.Aging.ToString();
			TxtEntryDate.Text	  = payments.EntryDate.ToShortDateString();
			TxtCloseDate.Text	  = payments.CloseDate;
			TxtPaymentAmount.Text = payments.PaymentAmount.ToString("###,###.00");
			TxtPaymentCheck.Text  = payments.CheckNo;
			TxtReceiptNo.Text	  = payments.ReceiptNo;
			TxtNamePayee.Text	  = payments.Name;	
			ChkSRO.Checked		  = payments.Licence;
			ChkIsNEwTransaction.Checked	= payments.IsNewTransaction;

			TxtPaymentDate.Text	  = payments.PaymentDate;
			TxtEntryDate.Text	  = payments.EntryDate.ToShortDateString();

			payments.GetTotalAmount(payments.CheckNo,payments.PaymentDate,payments.PaymentDate);
			TxtCheckNo.Text		  = payments.CheckNo;
			TxtTotalAmount.Text   = payments.TotalAmount.ToString("###,###.00");
			LblTotalCases.Text    = LblTotalCases.Text+" "+payments.TotalCheck.ToString();
			TxtAuthorizeBy.Text	  = payments.AuthorizeUserName.Trim();

            
			if (Session["OldPayment"] != null)
			{
				TaskControl.Payments OldPayment = (TaskControl.Payments) Session["OldPayment"];

				if (OldPayment.CheckNo != payments.CheckNo)
				{
					ChkMultiple.Checked = false;
				}
				else
				{
					ChkMultiple.Checked	  = true;
				}
			}
		}
		
		private void BtnBegin_Click(object sender, System.EventArgs e)
		{
			Session.Remove("Address1");
		}

		private void BtnPrevious_Click(object sender, System.EventArgs e)
		{
			Session.Remove("Address1");
		}

		private void BtnNext_Click(object sender, System.EventArgs e)
		{
			Session.Remove("Address1");
		}

		private void BtnEnd_Click(object sender, System.EventArgs e)
		{
			Session.Remove("Address1");
		}

		private void BtnSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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
				FillProperties();
				TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];

				taskControl.Save(userID);
				FillTextControl();
				DisableControls();
	
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Task Control information saved successfully.");
				this.litPopUp.Visible = true;
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Unable to save this Task." +"\r\n"+ exp.Message);
				this.litPopUp.Visible = true;
			}
		}

		private void FillProperties()
		{
			TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];

			taskControl.PolicyClassID	= ddlPolicyClass.SelectedItem.Value != "" ? int.Parse(ddlPolicyClass.SelectedItem.Value.ToString()):0;
			taskControl.OriginalPolicyNo= txtOriginalPolicyNo.Text.ToUpper();
			taskControl.PolicyType      = TxtPolicyType.Text.ToUpper();
			taskControl.PolicyNo		= txtPolicyNo.Text.ToUpper();
			taskControl.Certificate  	= txtCertificate.Text.ToUpper();
			taskControl.Sufijo	  	    = TxtSuffix.Text.ToUpper();
			taskControl.LoanNumber		= TxtLoanNo.Text.ToUpper();
			taskControl.Comments		= txtComments.Text.ToUpper();
			//taskControl.Comments1		= TxtComments1.Text.ToUpper();
			taskControl.TaskStatusID	= ddlTaskStatus.SelectedItem.Value != "" ? int.Parse(ddlTaskStatus.SelectedItem.Value.ToString()):0;
			taskControl.CustomerNo		= TxtCustomerNo.Text.ToUpper();
			taskControl.Bank			= ddlBank.SelectedItem.Value != "" ? ddlBank.SelectedItem.Value:"000";
			taskControl.Licence			= ChkSRO.Checked;
			taskControl.DepositDate  	= txtDepositDate.Text;
			taskControl.PaymentDate		= TxtPaymentDate.Text;
			taskControl.CheckNo			= TxtPaymentCheck.Text.ToUpper();
			taskControl.CreditDebitID	= ddlCreditDebit.SelectedItem.Value != "" ?int.Parse(ddlCreditDebit.SelectedItem.Value):0;
			taskControl.Name			= TxtNamePayee.Text.ToUpper();
			taskControl.MultiplePayment = ChkMultiple.Checked;
			taskControl.IsNewTransaction= ChkIsNEwTransaction.Checked;
            taskControl.AdjustmentTypeID = ddlAdjustmentType.SelectedItem.Value != "" ? int.Parse(ddlAdjustmentType.SelectedItem.Value):0;
            taskControl.Comments1 = ddlAdjustmentType.SelectedItem.Text.ToString();

            int AdjNameID = 0;
            if (chkAdjustment.Checked == true)
            {
                if (rdbName.Checked == true)
                {
                    AdjNameID = 1; //Nombre del Cliente
                }
                else
                    if (rdbDealer.Checked == true)
                    {
                        AdjNameID = 2; //Nombre del Dealer
                    }
                    else
                        if (rdbBank.Checked == true)
                        {
                            AdjNameID = 3; //Nombre del Banco
                        }
            }
            taskControl.AdjustmentNameID = AdjNameID;

            

			if (taskControl.Mode == (int) TaskControl.TaskControl.TaskControlMode.ADD)
			{
				Login.Login cp = HttpContext.Current.User as Login.Login;
				taskControl.EnteredBy = cp.Identity.Name.Split("|".ToCharArray())[0].ToString();
			}

			bool error = false;
			string payAmt = TxtPaymentAmount.Text.Trim();

			for (int i=0; i<=payAmt.Length-1; i++)
			{
				if (System.Char.IsDigit(payAmt,i) || Char.Parse(payAmt.Substring(i,1)) == '.' || Char.Parse(payAmt.Substring(i,1)) == ',' || Char.Parse(payAmt.Substring(i,1)) == '-')
				{
					error  = false;
				}
				else
				{
					error  = true;
					i = TxtPaymentAmount.Text.Trim().Length-1;
				}
			}

			if (error)
			{
				throw new Exception("Payment Amount must be numeric digit.");
			}

			if(TxtPaymentAmount.Text != "")
				taskControl.PaymentAmount	= Decimal.Parse(TxtPaymentAmount.Text.Replace(",",""));
			
			Session.Add("TaskControl",taskControl);
		}

		private void BtnExit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.litPopUp.Visible = false;
			Session.Clear();
			Response.Redirect("TaskControl.aspx");
		}

	
		private void btnEdit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			bool privilege = false;
			try
			{
				Login.Login cp = HttpContext.Current.User as Login.Login;
				if (cp == null)
				{
					Response.Redirect("HomePage.aspx?001");
				}
				else
				{
					if(!cp.IsInRole("EDITPAYMENTS") && !cp.IsInRole("ADMINISTRATOR"))
					{
						privilege = true;
						throw new Exception("Don't have privilege to add or modify payments.");
						//Response.Redirect("HomePage.aspx?001");
					}
				}
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString(exp.Message);
				this.litPopUp.Visible = true;
			}

			if (privilege == false)
			{
				TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];
				taskControl.Mode		= (int) TaskControl.TaskControl.TaskControlMode.UPDATE;

				Session.Add("TaskControl",taskControl);

				EnableControls();

				if (decimal.Parse(TxtPaymentAmount.Text) > 0 && taskControl.PaymentApplied)
				{
					TxtPaymentAmount.Enabled	= false;
					ddlCreditDebit.Enabled		= false;
				}
			}
		}

		private void btnCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];

			if(taskControl.Mode == 1) //ADD
			{
				Session.Clear();
				Response.Redirect("TaskControl.aspx");
			}
			else
			{
				taskControl.Mode		= (int) TaskControl.TaskControl.TaskControlMode.CLEAR;
				Session["TaskControl"] = taskControl;

				DisableControls();
			}
		}

		private void EnableControls()
		{
			TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];

			btnEdit.Visible		= false;
			BtnExit.Visible		= false;
			BtnSave.Visible		= true;
			btnCancel.Visible	= true;
			btnAuditTrail.Visible = false;

			btnCalendar.Visible = true;
			Button1.Visible	    = true;
			BtnNewPayment.Visible = false;
			btnReceipt.Visible  = false;
			btnDelete.Visible	= false;

			//DisableDeleteBtn(); //Verifica si tiene permiso para el boton de delete.

			if (taskControl.Mode == 2)
			{
				this.BtnGo.Visible          = true;
				this.BtnGo2.Visible          = true;
			}
			
			VerifyChkIsNEwTransaction(); //Verifica si tiene permiso para el CHK IsNewTransaction.
			ChkIsNEwTransaction.Enabled =false ;
			ChkMultiple.Enabled = false;
			ChkSRO.Enabled	    = true;

			ddlTaskStatus.Enabled		= false;
			ddlCreditDebit.Enabled		= true;
			ddlPolicyClass.Enabled		= true;
			ddlBank.Enabled		  	    = true;

			txtOriginalPolicyNo.Enabled	= true;
			TxtPolicyType.Enabled		= true;
			txtPolicyNo.Enabled			= true;
			TxtAging.Enabled			= false;
			TxtEntryDate.Enabled		= false;
			TxtCloseDate.Enabled		= false;
			txtCertificate.Enabled		= true;
			TxtSuffix.Enabled			= true;
			TxtLoanNo.Enabled			= true;
			txtComments.Enabled			= false;
			//TxtComments1.Enabled		= true;
			TxtCustomerNo.Enabled		= false;
			TxtName.Enabled				= false;
			TxtLastName1.Enabled		= false;
			TxtLastName2.Enabled		= false;
			TxtSocialSecurity.Enabled	= false;
			TxtReceiptNo.Enabled		= false;
			TxtNamePayee.Enabled		= true;
			TxtAuthorizeBy.Enabled		= false;

			TxtPaymentDate.Enabled		= true;
			TxtPaymentCheck.Enabled		= true;
			TxtPaymentAmount.Enabled	= true;
			txtDepositDate.Enabled		= true;
			TxtCheckNo.Enabled			= false;
			TxtTotalAmount.Enabled		= false;
            chkAdjustment.Enabled = true;
            ddlAdjustmentType.Enabled = true;
            rdbBank.Enabled = true;
            rdbName.Enabled = true;
            rdbDealer.Enabled = true;

        }

		private void DisableControls()
		{
			btnEdit.Visible		= true;
			BtnExit.Visible		= true;
			BtnSave.Visible		= false;
			btnCancel.Visible	= false;
			btnAuditTrail.Visible = true;

			btnCalendar.Visible = false;
			Button1.Visible	    = false;
			BtnNewPayment.Visible = true;
			btnReceipt.Visible  = true;
			BtnGo.Visible       = false;
			BtnGo2.Visible       = false;
			
			DisableDeleteBtn(); //Verifica si tiene permiso para el boton de delete.

			VerifyChkIsNEwTransaction(); //Verifica si tiene permiso para el CHK IsNewTransaction.
			ChkIsNEwTransaction.Enabled = false;
			ChkMultiple.Enabled = true;
			ChkSRO.Enabled	    = false;

			ddlTaskStatus.Enabled		= false;
			ddlCreditDebit.Enabled		= false;
			ddlPolicyClass.Enabled		= false;
			ddlBank.Enabled		  	    = false;

			txtOriginalPolicyNo.Enabled	= false;
			TxtPolicyType.Enabled		= false;
			txtPolicyNo.Enabled			= false;
			TxtAging.Enabled			= false;
			TxtEntryDate.Enabled		= false;
			TxtCloseDate.Enabled		= false;
			txtCertificate.Enabled		= false;
			TxtSuffix.Enabled			= false;
			TxtLoanNo.Enabled			= false;
			txtComments.Enabled			= false;
			//TxtComments1.Enabled		= false;
			TxtCustomerNo.Enabled		= false;
			TxtName.Enabled				= false;
			TxtLastName1.Enabled		= false;
			TxtLastName2.Enabled		= false;
			TxtSocialSecurity.Enabled	= false;
			TxtReceiptNo.Enabled		= false;
			TxtNamePayee.Enabled     	= false;
			TxtAuthorizeBy.Enabled		= false;

			TxtPaymentDate.Enabled		= false;
			TxtPaymentCheck.Enabled		= false;
			TxtPaymentAmount.Enabled	= false;
			txtDepositDate.Enabled		= false;
			TxtCheckNo.Enabled			= false;
			TxtTotalAmount.Enabled		= false;
            chkAdjustment.Enabled = false;
            ddlAdjustmentType.Enabled = false;
            rdbBank.Enabled = false;
            rdbName.Enabled = false;
            rdbDealer.Enabled = false;

		}

	
		private void btnAuditTrail_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if(Session["TaskControl"] != null)
			{
				TaskControl.Payments payments = (TaskControl.Payments) Session["TaskControl"];
				Response.Redirect("SearchAuditItems.aspx?type=2&taskControlID=" + 
					payments.TaskControlID.ToString());
			}
		}

		protected void BtnGo2_Click(object sender, System.EventArgs e)
		{
			TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];
			try
			{
				ValidateInfo(true);

				string PolClassID = "";	

				switch (int.Parse(ddlPolicyClass.SelectedItem.Value.ToString()))
				{
					case 3:				// AUTO
						PolClassID = "A";
						break;
					case 4:				// DWELLING
						PolClassID = "D";
						break;
					case 5:				// BOND
						PolClassID = "B";
						break;
					case 6:				// CONDOMINIUM
						PolClassID = "O";
						break;
					case 7:				// YACHT
						PolClassID = "Y";
						break;
					case 8:				// OTHER
						PolClassID = "O";
						break;
					case 11:			// FLOOD
						PolClassID = "F";
						break;
				}                            
	
				DataTable dtHd;
				dtHd = TaskControl.TaskControl.GetPoliciesFromHdpolicyByLoanNoDB(PolClassID,TxtLoanNo.Text.ToUpper().Trim());
			
				if ( dtHd != null)
				{
					if (dtHd.Rows.Count != 0)
					{
						FillProperties();
						Session.Add("DtHD",dtHd);
						Response.Redirect("ViewOtherPaymentInfo.aspx");	
					}
					else
						throw new Exception("This Loan Number is not found in our databases, Please verify.");
				}
				else
					throw new Exception("This Loan Number is not found in our databases, Please verify.");
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString(exp.Message);
				this.litPopUp.Visible = true;
			}
		}

		protected void BtnGo_Click(object sender, System.EventArgs e)
		{
			TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];
			try
			{
				ValidateInfo(false);

				string PolClassID = "";	

				switch (int.Parse(ddlPolicyClass.SelectedItem.Value.ToString()))
				{
					case 3:				// AUTO
						PolClassID = "A";
						break;
					case 4:				// DWELLING
						PolClassID = "D";
						break;
					case 5:				// BOND
						PolClassID = "B";
						break;
					case 6:				// CONDOMINIUM
						PolClassID = "O";
						break;
					case 7:				// YACHT
						PolClassID = "Y";
						break;
					case 8:				// OTHER
						PolClassID = "O";
						break;
					case 11:			// FLOOD
						PolClassID = "F";
						break;
				}                            
	
				DataTable dtHd;
				dtHd = TaskControl.TaskControl.GetPoliciesFromHdpolicyByPolicyNoDB(PolClassID,TxtPolicyType.Text.ToUpper().Trim(),
					txtPolicyNo.Text.ToUpper().Trim(), txtCertificate.Text.ToUpper().Trim());
			
				if ( dtHd != null)
				{
					if (dtHd.Rows.Count != 0)
					{
						FillProperties();
						Session.Add("DtHD",dtHd);
						Response.Redirect("ViewOtherPaymentInfo.aspx");	
					}
					else
						throw new Exception("This Policy Number is not found in our databases, Please verify.");
				}
				else
					throw new Exception("This Policy Number is not found in our databases, Please verify.");
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString(exp.Message);
				this.litPopUp.Visible = true;
			}
		}

		protected void ChkSRO_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.ChkSRO.Checked && this.ddlCreditDebit.SelectedItem.Text == "CREDIT")
			{
				LblPaymentDate.Text = "Expiration Date";
			}
			else
			{
				LblPaymentDate.Text = "Payment Date";
			}
		}

		private void ddlCreditDebit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.ChkSRO.Checked && this.ddlCreditDebit.SelectedItem.Text == "CREDIT")
			{
				LblPaymentDate.Text = "Expiration Date";
			}
			else
			{
				LblPaymentDate.Text = "Payment Date";
			}
		}

		private void txtPolicyNo_TextChanged(object sender, System.EventArgs e)
		{

		}

		protected void txtCertificate_TextChanged(object sender, System.EventArgs e)
		{
			//			TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];
			//			if (taskControl.Mode == 1)
			//			{
			//				string custno = TaskControl.TaskControl.GetPoliciesFromHdpolicyByPolicyNoDB(TxtPolicyType.Text.ToUpper().Trim(),
			//					txtPolicyNo.Text.ToUpper().Trim(), txtCertificate.Text.ToUpper().Trim());
			//				taskControl.CustomerNo		= custno.ToUpper();
			//			}
		}

		protected void ddlPolicyClass_SelectedIndexChanged(object sender, System.EventArgs e)
		{
//			switch (int.Parse(ddlPolicyClass.SelectedItem.Value))
//			{
//				case 10:  //Etch
//					this.TxtPolicyType.Text		= "ETC";
//					this.TxtSuffix.Text			= "00";
//					this.TxtPaymentAmount.Text	= "299.00";
//					this.BtnGo.Visible       = false;
//					this.BtnGo2.Visible       = false;
//					break;
//				case 1:  //AG
//					this.TxtPolicyType.Text		= "AG";
//					this.TxtSuffix.Text			= "00";
//					this.TxtPaymentAmount.Text	= "";
//					this.BtnGo.Visible       = false;
//					this.BtnGo2.Visible       = false;
//					break;
//				default:
//					this.TxtPolicyType.Text		= "";
//					this.TxtSuffix.Text			= "";
//					this.TxtPaymentAmount.Text	= "";
//					this.BtnGo.Visible          = true;
//					this.BtnGo2.Visible          = true;
//					break;
//			}
		}

		private void btnDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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

			try
			{
				TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];
				taskControl.DeleteTaskPayment(userID);
				
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Delete Task Control successfully.");
				this.litPopUp.Visible = true;
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Unable to delete this Task. " + exp.Message);
				this.litPopUp.Visible = true;
			}
						
		}

		private void VerifyChkIsNEwTransaction()
		{
			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
				Response.Redirect("HomePage.aspx?001");
			}
			else
			{
				if(cp.IsInRole("PAYMENTISNEWTRANS") || cp.IsInRole("ADMINISTRATOR"))
				{
					this.ChkIsNEwTransaction.Visible = false;
				}
				else
				{
					this.ChkIsNEwTransaction.Visible = false;
				}
			}
		}

		private void DisableDeleteBtn()
		{
			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
				Response.Redirect("HomePage.aspx?001");
			}
			else
			{
				if(cp.IsInRole("TASKPAYMENTDELETE") || cp.IsInRole("ADMINISTRATOR"))
				{
					this.btnDelete.Visible = true;
				}
				else
				{
					this.btnDelete.Visible = false;
				}
			}
		}

		private void ValidateInfo(bool byLoanNo)
		{
			string errorMessage = String.Empty;

			if (ddlPolicyClass.SelectedItem.Value == "")
				errorMessage = "The line of business is missing or wrong. Please choose a line of businees.";

			if (byLoanNo)
			{
				if (TxtLoanNo.Text.Trim() == "")
					errorMessage = "The loan number is missing or wrong. Please verify.";
			}
			else
			{
				if (TxtPolicyType.Text.Trim() == "")
					errorMessage = "The policy type is missing or wrong. Please verify.";
				else
					if (txtPolicyNo.Text.Trim() == "")
					errorMessage = "The policy number is missing or wrong. Please verify.";
			}

			//throw the exception.
			if (errorMessage != String.Empty)
			{
				throw new Exception(errorMessage);
			}
		}

		protected void TxtSuffix_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		protected void Button3_Click(object sender, System.EventArgs e)
		{
			if(Session["TaskControl"] != null)
			{
				TaskControl.Payments payments = (TaskControl.Payments) Session["TaskControl"];
				Response.Redirect("SearchAuditItems.aspx?type=2&taskControlID=" + 
					payments.TaskControlID.ToString());
			}
		}

		protected void BtnDele_Click(object sender, System.EventArgs e)
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

			try
			{
				TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];
				taskControl.DeleteTaskPayment(userID);
				
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Delete Task Control successfully.");
				this.litPopUp.Visible = true;
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Unable to delete this Task. " + exp.Message);
				this.litPopUp.Visible = true;
			}
		}

		protected void btnReceip_Click(object sender, System.EventArgs e)
		{
//			TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];
//
//			PaymentReceipt rpt = new PaymentReceipt(taskControl);				
//			rpt.Run(false);
//
//			Session.Add("Report",rpt);
//
//			Session.Add("FromPage","Payments.aspx");
//
//			Response.Redirect("ActiveXViewer.aspx",false);
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			this.litPopUp.Visible = false;
			Session.Clear();
			Response.Redirect("MainMenu.aspx");
		}

		protected void Button5_Click(object sender, System.EventArgs e)
		{
			TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];

			if(taskControl.Mode == 1) //ADD
			{
				Session.Clear();
				Response.Redirect("MainMenu.aspx");
			}
			else
			{
				taskControl.Mode		= (int) TaskControl.TaskControl.TaskControlMode.CLEAR;
				Session["TaskControl"] = taskControl;

				DisableControls();
			}
		}

		protected void Button6_Click(object sender, System.EventArgs e)
		{
			Customer.Prospect prospect = (Customer.Prospect)Session["Prospect"];
			Login.Login cp = HttpContext.Current.User as Login.Login;
			int userID = 0;

            string errorMessage = String.Empty;

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
				FillProperties();
				TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];

                //string Amount = TxtTotalAmount.Text.Trim();
                
                //for (int i = 0; i < Amount.Length; i++)
                //{
                //    if (Char.IsNumber(Amount[i]) == false)  //evalua si es true.
                //    {
                //        errorMessage = "Total amount must be numeric.";
                //        i = Amount.Length;    //Para obligar que termine el for yno seguir evaluando         
                //    }
                //}

                //throw the exception.
                if (errorMessage != String.Empty)
                {
                    throw new Exception(errorMessage);
                }

                int AdjValue = ddlAdjustmentType.SelectedItem.Value != "" ? int.Parse(ddlAdjustmentType.SelectedItem.Value) : 0;
                if (chkAdjustment.Checked == true)
                {
                    if (AdjValue == 0)
                    {
                      errorMessage = "Select type of adjustment to continue.";
                    }

                    //throw the exception.
                    if (errorMessage != String.Empty)
                    {
                       throw new Exception(errorMessage);
                    }
                }
                

				taskControl.Save(userID);
				FillTextControl();
				DisableControls();
	
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Task Control information saved successfully.");
				this.litPopUp.Visible = true;
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Unable to save this Task." +"\r\n"+ exp.Message);
				this.litPopUp.Visible = true;
			}
		}

		protected void Button4_Click(object sender, System.EventArgs e)
		{
			bool privilege = false;
			try
			{
				Login.Login cp = HttpContext.Current.User as Login.Login;
				if (cp == null)
				{
					Response.Redirect("HomePage.aspx?001");
				}
				else
				{
					if(!cp.IsInRole("EDITPAYMENTS") && !cp.IsInRole("ADMINISTRATOR"))
					{
						privilege = true;
						throw new Exception("Don't have privilege to add or modify payments.");
						//Response.Redirect("HomePage.aspx?001");
					}
				}
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString(exp.Message);
				this.litPopUp.Visible = true;
			}

			if (privilege == false)
			{
				TaskControl.Payments taskControl = (TaskControl.Payments) Session["TaskControl"];
				taskControl.Mode		= (int) TaskControl.TaskControl.TaskControlMode.UPDATE;

				Session.Add("TaskControl",taskControl);

				EnableControls();

				if (decimal.Parse(TxtPaymentAmount.Text) > 0 && taskControl.PaymentApplied)
				{
					TxtPaymentAmount.Enabled	= false;
					ddlCreditDebit.Enabled		= false;
				}
			}
		}

		protected void BtnNewPayment_Click(object sender, System.EventArgs e)
		{
			TaskControl.Payments taskControl = new TaskControl.Payments();
			TaskControl.Payments OldPayment = (TaskControl.Payments) Session["TaskControl"];
			
			if (ChkMultiple.Checked)
			{
//				switch (int.Parse(ddlPolicyClass.SelectedItem.Value))
//				{
//					case 10:  //Etch
//						taskControl.PaymentAmount	= OldPayment.PaymentAmount;
//						break;
//				}
				
				taskControl.PolicyClassID	= OldPayment.PolicyClassID;
				taskControl.CheckNo			= OldPayment.CheckNo;
				taskControl.PaymentDate		= OldPayment.PaymentDate;
				taskControl.TotalAmount		= OldPayment.TotalAmount;
			}

			taskControl.MultiplePayment = ChkMultiple.Checked;

			taskControl.Mode = 1;
			Session.Add("TaskControl",taskControl);
			Session.Add("OldPayment",OldPayment);
			Response.Redirect("Payments.aspx",false);
		}

        protected void btnReceipt_Click(object sender, EventArgs e)
        {
            TaskControl.Payments taskControl = (TaskControl.Payments)Session["TaskControl"];

            EPolicy2.Reports.Payments.PaymentReceipt rpt = new EPolicy2.Reports.Payments.PaymentReceipt(taskControl);
            rpt.Run(false);

            Session.Add("Report", rpt);

            Session.Add("FromPage", "Payments.aspx");

            Response.Redirect("ActiveXViewer.aspx", false);
        }
        protected void chkAdjustment_CheckedChanged(object sender, EventArgs e)
        {
            txtDepositDate.Enabled =  !chkAdjustment.Checked;
            Button1.Visible = !chkAdjustment.Checked;
            TxtNamePayee.Enabled = !rdbName.Checked;
            ddlAdjustmentType.Visible = chkAdjustment.Checked;
            lblAdjustmentType.Visible = chkAdjustment.Checked;
            rdbBank.Visible = chkAdjustment.Checked;
            rdbDealer.Visible = chkAdjustment.Checked;
            rdbName.Visible = chkAdjustment.Checked;
            rdbName.Checked = true;
            this.TxtName.Enabled = false;
            this.TxtNamePayee.Text = this.TxtName.Text.ToString() + " " + this.TxtLastName1.Text.ToString() + " " + TxtLastName2.Text.ToString();

        }
        protected void ddlAdjustmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int AdjValue = ddlAdjustmentType.SelectedItem.Value != "" ? int.Parse(ddlAdjustmentType.SelectedItem.Value) : 0;
            switch (AdjValue)
            {
                //case 0:
                //    TxtPaymentCheck.Text = "";
                //    break;
                //case 1:
                //    TxtPaymentCheck.Text = "Move Pmt";
                //    break;
                case 2:
                    ddlCreditDebit.SelectedValue = "2";
                    break;
                //case 3:
                //    TxtPaymentCheck.Text = "Adjustment";
                //    break;
            }
        }
        protected void rdbBank_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbBank.Checked == true)
            {
                TxtNamePayee.Text = ddlBank.SelectedItem.Text.ToString();
                TxtNamePayee.Enabled = true;
            }
        }
        protected void rdbName_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbName.Checked == true)
            {
                TxtNamePayee.Text = this.TxtName.Text.ToString() + " " + this.TxtLastName1.Text.ToString() + " " + TxtLastName2.Text.ToString();
                TxtNamePayee.Enabled = false;
            }
        }
        protected void rdbDealer_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDealer.Checked == true)
            {
                TxtNamePayee.Text = ddlCompanyDealer.SelectedItem.Text.ToString();
                TxtNamePayee.Enabled = true;


            }
        }
}
}
