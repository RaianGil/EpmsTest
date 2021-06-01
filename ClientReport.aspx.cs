using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServerControls = System.Web.UI.WebControls; 
using System.Web.UI.HtmlControls;
using EPolicy2.Reports;
using System.Text;

namespace EPolicy
{
	/// <summary>
	/// Summary description for ClientReport.
	/// </summary>
	public partial class ClientReport : System.Web.UI.Page
	{
	
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
				if(!cp.IsInRole("CLIENTREPORT") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("HomePage.aspx?001");
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
			//((Baldrich.BaldrichWeb.Components.TopBanner)Banner).SelectedOption = (int)Baldrich.HeadBanner.MenuOptions.Home;
			this.Placeholder1.Controls.Add(Banner);

			//Setup Left-side Banner
			
			Control LeftMenu = new Control();
			LeftMenu = LoadControl(@"LeftReportMenu.ascx");
			//((Baldrich.BaldrichWeb.Components.MenuEventControl)LeftMenu).Height = "534px";
			phTopBanner1.Controls.Add(LeftMenu);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void rblReportList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (rblReportList.SelectedItem.Value == "2")
			{
				Label2.Visible = false;
				Label4.Visible = false;
				Label5.Visible = false;
				ddlCustType.Visible = false;
				txtBegDate.Visible  = false;
				TxtEndDate.Visible  = false;
				btnCalendar.Visible = false;
				Button1.Visible     = false;
				
				txtCust1.Visible = true;
				txtCust2.Visible = true;
				txtCust3.Visible = true;
				txtCust4.Visible = true;
				txtCust5.Visible = true;
				txtCust6.Visible = true;
				lblLabelsClient.Visible = true;
				Label3.Visible = true;
				Label6.Visible = true;
				Label7.Visible = true;
				Label8.Visible = true;
				Label9.Visible = true;
				Label10.Visible = true;
				
			}
			else
			{
				Label2.Visible = true;
				Label4.Visible = true;
				Label5.Visible = true;
				ddlCustType.Visible = true;
				txtBegDate.Visible  = true;
				TxtEndDate.Visible  = true;	
				btnCalendar.Visible = true;
				Button1.Visible     = true;
				
				

                txtCust1.Visible = false;
				txtCust2.Visible = false;
				txtCust3.Visible = false;
				txtCust4.Visible = false;
				txtCust5.Visible = false;
				txtCust6.Visible = false;
				lblLabelsClient.Visible = false;
				Label3.Visible = false;
				Label6.Visible = false;
				Label7.Visible = false;
				Label8.Visible = false;
				Label9.Visible = false;
				Label10.Visible = false;
				
			}

		}

		private void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				//FieldVerify();
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
				this.litPopUp.Visible = true;
				return;
			}

			bool IsBusiness = false;

			if (ddlCustType.SelectedItem.Value == "B")
			{
				IsBusiness = true;
			}
			else
			{
				IsBusiness = false;
			}
			
			switch(rblReportList.SelectedItem.Value)       
			{         
				case "0":   
					ClienttWithActivePolices(IsBusiness);
					break;

				case "1":   
					ClienttWithoutPolices(IsBusiness);
					break; 
                 
				case "2":            
					ClienttLabels(IsBusiness);
					break;      
			}
		}

		private void ClienttWithActivePolices(bool IsBusiness)
		{

		}

		private void ClienttWithoutPolices(bool IsBusiness)
		{

		}

		private void ClienttLabels(bool IsBusiness)
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
			
			DataTable dt = new DataTable();
		     dt.Columns.Add("FirstName",typeof(string));
			 dt.Columns.Add("Initial",typeof(string));
			 dt.Columns.Add("LastName1",typeof(string));
			 dt.Columns.Add("LastName2",typeof(string));
			 dt.Columns.Add("Address1",typeof(string));
			 dt.Columns.Add("Address2",typeof(string));
			 dt.Columns.Add("City",typeof(string));
			 dt.Columns.Add("State",typeof(string));
			 dt.Columns.Add("ZipCode",typeof(string));
			 dt.Columns.Add("Position",typeof(string));

			string customerNo = "";

			for (int i=1; i<=6;i++)
          		
			{
				switch (i)
				{
					case 1:
						customerNo = txtCust1.Text.Trim();
						break;
					
					case 2:
						customerNo = txtCust2.Text.Trim();
						break;					
					
					case 3:
						customerNo = txtCust3.Text.Trim();
						break;					
					
					case 4:
						customerNo = txtCust4.Text.Trim();
						break;					
					
					case 5:
						customerNo = txtCust5.Text.Trim();
						break;					

					case 6:
						customerNo = txtCust6.Text.Trim();
						break;					

				}

				if(customerNo != "" )
				{ 
					Customer.Customer customer = 
						Customer.Customer.GetCustomer(customerNo, userID);
					if (customer != null)
					{
						DataRow myRow = dt.NewRow();
						myRow["FirstName"]	= customer.FirstName.Trim()+" "+customer.Initial.Trim()+" "+customer.LastName1.Trim()+" "+customer.LastName2.Trim();
						myRow["Address1"]	= customer.Address1.Trim();
						myRow["Address2"]   = customer.Address2.Trim();
						myRow["City"]       = customer.City.Trim()+" "+customer.State.Trim()+" "+customer.ZipCode.Trim();
						myRow["Position"]   = i.ToString().Trim();
			       
       
						dt.Rows.Add(myRow);
						dt.AcceptChanges();
					}
				}
			}  

			if (dt.Rows.Count !=0)
			{
				CustomersLabels rpt = new  CustomersLabels(dt);

				//rpt.DataSource = dt;
				rpt.DataMember = "Report";
				rpt.Run(false);
			
				Session.Add("Report",rpt);
				Session.Add("FromPage","ClientReport.aspx?SelectedItem=2");//" + this.ddlLookupTables.SelectedItem.Value);
				Response.Redirect("ActiveXViewer.aspx",false);
			}
			else
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("Customer No. is not found, Please verify.");
				this.litPopUp.Visible = true;
			}		
		}

		private void FieldVerify()
		{
			string errorMessage = String.Empty;
			bool found = false;

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
				errorMessage = "The Ending Date must be great than beginning date .";
				found = true;
			}

			//throw the exception.
			if (errorMessage != String.Empty)
			{
				throw new Exception(errorMessage);
			}	
		}

		private Hashtable setControlHashTable()
		{
			Hashtable custNo = new Hashtable();
			// Put user code to initialize the page here
			custNo.Add(0,txtCust1);
			custNo.Add(1,txtCust2);
			custNo.Add(2,txtCust3);
			custNo.Add(3,txtCust4);
			custNo.Add(4,txtCust5);
			custNo.Add(5,txtCust6);
			return custNo;
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			try
			{
				//FieldVerify();
			}
			catch (Exception exp)
			{
				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
				this.litPopUp.Visible = true;
				return;
			}

			bool IsBusiness = false;

			if (ddlCustType.SelectedItem.Value == "B")
			{
				IsBusiness = true;
			}
			else
			{
				IsBusiness = false;
			}
			
			switch(rblReportList.SelectedItem.Value)       
			{         
				case "0":   
					ClienttWithActivePolices(IsBusiness);
					break;

				case "1":   
					ClienttWithoutPolices(IsBusiness);
					break; 
                 
				case "2":            
					ClienttLabels(IsBusiness);
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
