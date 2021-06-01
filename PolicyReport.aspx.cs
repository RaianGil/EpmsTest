using DataDynamics;
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
using Baldrich.DBRequest;
using EPolicy.XmlCooker;
using System.IO;
using System.Net;
using System.Text;
using EPolicy.LookupTables;
using EPolicy.TaskControl;
using EPolicy2.Reports;
using EPolicy;

namespace EPolicy
{
	/// <summary>
	/// Summary description for PolicyReport.
	/// </summary>
	public partial class PolicyReport : System.Web.UI.Page
	{
		private Control LeftMenu;
	
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
				if(!cp.IsInRole("POLICYREPORT") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("HomePage.aspx?001");
				}
			}

			if(!IsPostBack)
			{
				EnableControl();
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
			//((Baldrich.BaldrichWeb.Components.TopBanner)Banner).SelectedOption = (int)Baldrich.HeadBanner.MenuOptions.Home;
			this.Placeholder1.Controls.Add(Banner);

			//Setup Left-side Banner
			
			LeftMenu = new Control();
			LeftMenu = LoadControl(@"LeftReportMenu.ascx");
			//((Baldrich.BaldrichWeb.Components.MenuCustomers)LeftMenu).Height = "534px";
			phTopBanner1.Controls.Add(LeftMenu);

			//Load DownDropList
			DataTable dtCompanyDealer	=    LookupTables.LookupTables.GetTable("CompanyDealer");
			DataTable dtInsuranceCompany	= LookupTables.LookupTables.GetTable("InsuranceCompany");

			//CompanyDealer
			ddlDealer.DataSource = dtCompanyDealer;
			ddlDealer.DataTextField = "CompanyDealerDesc";
			ddlDealer.DataValueField = "CompanyDealerID";
			ddlDealer.DataBind();
			ddlDealer.SelectedIndex = -1;
			ddlDealer.Items.Insert(0,"");

			//InsuranceCompany
			ddlInsuranceCompany.DataSource = dtInsuranceCompany;
			ddlInsuranceCompany.DataTextField = "InsuranceCompanyDesc";
			ddlInsuranceCompany.DataValueField = "InsuranceCompanyID";
			ddlInsuranceCompany.DataBind();
			ddlInsuranceCompany.SelectedIndex = -1;
			ddlInsuranceCompany.Items.Insert(0,"");

			

		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				FieldVerify();

			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
				this.litPopUp.Visible = true;
				return;
			}

			switch(rblPolicyReports.SelectedItem.Value)       
			{         
				case "0":   
					AutoPolicyReport();
					break;

				case "1":   
					PaymentCertificateLetterReport();
					break; 
			}
		}



		private void FieldVerify()
		{
			string errorMessage = String.Empty;
			bool found = false;

			if (this.rblPolicyReports.SelectedItem.Value == "0")
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
					found == false)
				{
					errorMessage = "Please enter the beginning date and ending date.";
					found = true;
				}
				else if ((this.txtBegDate.Text != "" && this.TxtEndDate.Text != "" &&
					DateTime.Parse(this.txtBegDate.Text) > DateTime.Parse(this.TxtEndDate.Text)) && found == false)
				{
					errorMessage = "The Ending Date must be great than beginning date.";
					found = true;
				}

				if (this.rblPolicyReports.SelectedItem.Value == "0")
				{
					if (this.ddlDateType.SelectedItem.Text == "")
					{
						errorMessage = "Please enter the 'Date Type'.";
						found = true;
					}

				}

				if (this.ddlDealer.SelectedItem.Text == "")
				{
					errorMessage = "Please enter the 'Company Dealer'.";
					found = true;
				}

			}

			if (this.rblPolicyReports.SelectedItem.Value == "1")
			{
				if (this.txtBegDate1.Text == "" &&  this.TxtEndDate1.Text != "" &&
					found == false)
				{
					errorMessage = "Please enter the beginning date.";
					found = true;
				}
				if (this.txtBegDate1.Text != "" &&  this.TxtEndDate1.Text == "" &&
					found == false)
				{
					errorMessage = "Please enter the ending date.";
					found = true;
				}
				else if (this.txtBegDate1.Text == "" &&  this.TxtEndDate1.Text == "" && 
					found == false)
				{
					errorMessage = "Please enter the beginning date and ending date.";
					found = true;
				}
				else if ((this.txtBegDate1.Text != "" && this.TxtEndDate1.Text != "" &&
					DateTime.Parse(this.txtBegDate1.Text) > DateTime.Parse(this.TxtEndDate1.Text)) && found == false)
				{
					errorMessage = "The Ending Date must be great than beginning date.";
					found = true;
				}
			}
			//throw the exception.
			if (errorMessage != String.Empty)
			{
				throw new Exception(errorMessage);
			}	
	
		}

		private void AutoPolicyReport()
		{
			EPolicy2.Reports.AutoPolicy appAutoreport = new EPolicy2.Reports.AutoPolicy();
			DataTable dt = appAutoreport.AutoPolicyReport(txtBegDate.Text,TxtEndDate.Text,ddlDealer.SelectedItem.Value.Trim(),ddlInsuranceCompany.SelectedItem.Value.Trim(),ddlDateType.SelectedItem.Value.Trim());
	
			try
			{
				if (dt.Rows.Count == 0)
				{
					throw new Exception("Don't exist any information for this report");
				}

				DataDynamics.ActiveReports.ActiveReport3 rpt = new  AutoPolicyReport(txtBegDate.Text,TxtEndDate.Text,"Premium Written By Company Dealer & Insurance Company",ChkSummary.Checked);

				//rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
			
				rpt.DataSource = dt;
				rpt.DataMember = "Report";
				rpt.Run(false);

				Session.Add("Report",rpt);
				Session.Add("FromPage","PolicyReport.aspx");
				Response.Redirect("ActiveXViewer.aspx",false);
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
				this.litPopUp.Visible = true;
				return;
			}
		}

		private void PaymentCertificateLetterReport()
		{
			EPolicy2.Reports.AutoPolicy appAutoreport = new EPolicy2.Reports.AutoPolicy();
			DataTable dt = appAutoreport.PaymentCertificationLetterReport(txtBegDate1.Text,TxtEndDate1.Text);
	
			try
			{
				if (dt.Rows.Count == 0)
				{
					throw new Exception("Don't exist any information for this report");
				}

                DataDynamics.ActiveReports.ActiveReport3 rpt = new PaymentCertificateReport(txtBegDate1.Text, TxtEndDate1.Text, "Payment Certificate Letter Report", ChkSummary.Checked);

				//rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;
			
				rpt.DataSource = dt;
				rpt.DataMember = "Report";
				rpt.Run(false);

				Session.Add("Report",rpt);
				Session.Add("FromPage","PolicyReport.aspx");
				Response.Redirect("ActiveXViewer.aspx",false);
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
				this.litPopUp.Visible = true;
				return;
			}
		}

		protected void rblPolicyReports_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			EnableControl();
		}

		private void EnableControl()
		{
		
			switch(rblPolicyReports.SelectedItem.Value)     
			{    
				case "0":			//Auto Policy Report
					lblCompanyDealer.Visible    = true;
					LblDataType.Visible         = true;
					LblEndingDate.Visible       = true;
					lblInsurance.Visible        = true;
					LblBeginningDate.Visible    = true;
					txtBegDate.Visible          = true;
					TxtEndDate.Visible          = true;
				    ddlDateType.Visible         = true;
					ddlDealer.Visible           = true;
					ddlInsuranceCompany.Visible = true;
					btnCalendar.Visible         = true;
					btnCalendar2.Visible        = true;

					LblBeginningDate1.Visible   = false;
					LblEndingDate1.Visible      = false;
					txtBegDate1.Visible         = false;
					TxtEndDate1.Visible         = false;
					btnCalendar3.Visible        = false;
					btnCalendar4.Visible        = false;

					break;  
     
				case "1":			//Payment Certification Letter
					lblCompanyDealer.Visible    = false;
					LblDataType.Visible         = false;
					LblEndingDate.Visible       = false;
					lblInsurance.Visible        = false;
					LblBeginningDate.Visible    = false;
					txtBegDate.Visible          = false;
					TxtEndDate.Visible          = false;
					ddlDateType.Visible         = false;
					ddlDealer.Visible           = false;
					ddlInsuranceCompany.Visible = false;
					btnCalendar.Visible         = false;
					btnCalendar2.Visible        = false;

					LblBeginningDate1.Visible   = true;
					LblEndingDate1.Visible      = true;
					txtBegDate1.Visible         = true;
					TxtEndDate1.Visible         = true;
					btnCalendar3.Visible        = true;
					btnCalendar4.Visible        = true;


					break;  
			}
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			try
			{
				FieldVerify();

			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
				this.litPopUp.Visible = true;
				return;
			}

			switch(rblPolicyReports.SelectedItem.Value)       
			{         
				case "0":   
					AutoPolicyReport();
					break;

				case "1":   
					PaymentCertificateLetterReport();
					break; 
			}
		}

		protected void BtnExit_Click(object sender, System.EventArgs e)
		{
			Session.Clear();
			Response.Redirect("MainMenu.aspx");
		}





	}
}
