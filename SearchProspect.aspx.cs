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
	/// Summary description for SearchProspect.
	/// </summary>
	public partial class SearchProspect : System.Web.UI.Page
	{
		//private DataTable DtProspects;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
				Response.Redirect("Default.aspx?001");
			}
			else
			{
                if (!cp.IsInRole("SEARCH PROSPECT MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("Default.aspx?001");
				}
			}

			if(Session["DtProspects"] != null && !Page.IsPostBack)
			{
				searchIndividual.DataSource = (DataTable)Session["DtProspects"];
				searchIndividual.DataBind();
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
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.searchIndividual.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.searchIndividual_ItemCommand);
			this.searchBusiness.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.searchBusiness_ItemCommand);

		}
		#endregion

		protected void RdbIndividual_CheckedChanged(object sender, System.EventArgs e)
		{
			lblFirstName.Text = "First Name";
			lblLastName1.Text = "Last Name 1";
			lblLastName2.Text = "Last Name 2";


			ClearTextBox();
		}

		protected void RdbBusiness_CheckedChanged(object sender, System.EventArgs e)
		{
			lblFirstName.Text = "Contact First Name";
			lblLastName1.Text = "Contact Last Name";
			lblLastName2.Text = "Business Name";

			ClearTextBox();
		}

		private void ClearTextBox()
		{
			searchIndividual.Visible = false;
			searchBusiness.Visible   = false;

			searchBusiness.DataSource = null;
			searchIndividual.DataSource = null;

			LblTotalCases.Text				= "Total Cases: 0";

			TxtProspectID.Text				= "";
			txtFirstName.Text				= "";
			txtLastName1.Text				= "";
			txtLastName2.Text			    = "";
			TxtEmail.Text					= "";
			TxtPhone.Text					= "";
		}

		private void GoToSpecificWebPage(bool isBusiness)
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

			DataTable dtSpec = (DataTable) Session["DtProspects"];

			int i = (int) dtSpec.Rows[0]["ProspectID"];

			Customer.Prospect prospect = new Customer.Prospect();
			prospect.GetProspect(i);
			prospect.Mode = 2;
	
			prospect.NavigationProspectTable = (DataTable) Session["DtProspects"];

			string ToPage;

			if(isBusiness)
			{
				if(Session["ToPage"] == null) 
				{
					ToPage = "ProspectBusiness.aspx";
				}
				else
				{
					ToPage = Session["ToPage"].ToString();
				}
			}
			else
			{
				if(Session["ToPage"] == null) 
				{
					ToPage = "ProspectIndividual.aspx";
				}
				else
				{
					ToPage = Session["ToPage"].ToString();
				}
			}

			if(Session["Prospect"] == null)
				Session.Add("Prospect",prospect);
			else
				Session["Prospect"] = prospect;

			Response.Redirect(ToPage);
		}


		private void searchIndividual_ItemCommand(object source, 
			System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.Item.ItemType.ToString() != "Pager") // Select
			{
				int i = int.Parse(e.Item.Cells[1].Text);

				Customer.Prospect prospect = new Customer.Prospect();
                prospect.GetProspect(i);
				prospect.Mode = 2;
	
				prospect.NavigationProspectTable = (DataTable) Session["DtProspects"];

				string ToPage;

				if(Session["ToPage"] == null) 
				{
					ToPage = "ProspectIndividual.aspx";
				}
				else
				{
					ToPage = Session["ToPage"].ToString();
				}
	
				if(Session["Prospect"] == null)
					Session.Add("Prospect",prospect);
				else
					Session["Prospect"] = prospect;

				//Session.Remove("DtProspects");
	
				//Response.Redirect(ToPage+"?"+prospect.ProspectID.ToString());
				Response.Redirect(ToPage);
			}
			else  // Pager
			{
				searchIndividual.CurrentPageIndex = int.Parse(e.CommandArgument.ToString())-1;

				searchIndividual.DataSource = (DataTable) Session["DtProspects"];
				searchIndividual.DataBind();
			}
		}

		
		private void searchBusiness_ItemCommand(object source, 
			System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.Item.ItemType.ToString() != "Pager") // Select
			{
				int i = int.Parse(e.Item.Cells[1].Text);

				Customer.Prospect prospect = new Customer.Prospect();
				prospect.GetProspect(i);
				prospect.Mode = 2;
	
				prospect.NavigationProspectTable = (DataTable) Session["DtProspects"];

				string ToPage;

				if(Session["ToPage"] == null) 
				{
					ToPage = "ProspectBusiness.aspx";
				}
				else
				{
					ToPage = Session["ToPage"].ToString();
				}
	
				if(Session["Prospect"] == null)
					Session.Add("Prospect",prospect);
				else
					Session["Prospect"] = prospect;

				//Session.Remove("DtProspects");
	
				//Response.Redirect(ToPage+"?"+prospect.ProspectID.ToString());
				Response.Redirect(ToPage);
			}
			else  // Pager
			{
				searchBusiness.CurrentPageIndex = int.Parse(e.CommandArgument.ToString())-1;

				searchBusiness.DataSource = (DataTable) Session["DtProspects"];
				searchBusiness.DataBind();
			}
		}

		protected void btnSearch_Click(object sender, System.EventArgs e)
		{
			Customer.Prospect prospect = new Customer.Prospect();
            Login.Login cp = HttpContext.Current.User as Login.Login;
            int userID = 0;

            try
            {
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Could not parse user id from cp.Identity.Name.", ex);
            }
			
			searchIndividual.Visible = false;
			searchBusiness.Visible   = false;

			LblError.Visible = false;
			searchBusiness.DataSource = null;
			searchIndividual.DataSource = null;
			DataTable DtProspects;

			searchIndividual.CurrentPageIndex = 0;
			searchBusiness.CurrentPageIndex   = 0;

			//LblPleaseWait.Visible = true;

			bool business = RdbBusiness.Checked;

            string PhoneN = TxtPhone.Text.Trim();

            if (PhoneN == "(___)-___-____")
                PhoneN = "";
			
			if(!business)
			{
				DtProspects = 
					prospect.GetProspectByCriteria(this.TxtProspectID.Text.Trim(), 
					this.txtFirstName.Text.Trim(), this.txtLastName1.Text.Trim(),
					this.txtLastName2.Text.Trim(), this.TxtEmail.Text.Trim(),
                    PhoneN.Trim(), business, false, "", userID);
			}
			else
			{
				DtProspects = 
					prospect.GetProspectByCriteria(this.TxtProspectID.Text.Trim(), 
					this.txtFirstName.Text.Trim(), this.txtLastName1.Text.Trim(),
					"", this.TxtEmail.Text.Trim(), PhoneN.Trim(),
                    business, false, this.txtLastName2.Text.Trim(), userID);
			}
				
			Session.Remove("DtProspects");
			Session.Add("DtProspects",DtProspects);

			if (DtProspects.Rows.Count != 0)
			{
				if(RdbBusiness.Checked == true)
				{
					searchIndividual.Visible = false;
					searchBusiness.Visible   = true;

					searchBusiness.DataSource = DtProspects;
					searchBusiness.DataBind();
				}
				else
				{
					searchIndividual.Visible = true;
					searchBusiness.Visible   = false;

					searchIndividual.DataSource = DtProspects;
					searchIndividual.DataBind();
				}
			}
			else
			{
				if(RdbBusiness.Checked == true)
				{
					searchBusiness.DataSource = null;
					searchBusiness.DataBind();
				}
				else
				{
					searchIndividual.DataSource = null;
					searchIndividual.DataBind();
				}
				LblError.Visible = true;
				LblError.Text = "Could not find a match for your search criteria, please try again.";
			}
			LblTotalCases.Text = "Total Cases: "+DtProspects.Rows.Count.ToString();	

			//Si tiene un solo record se va a dirigir a la pantalla correspondiente.
			if (DtProspects.Rows.Count == 1)
				GoToSpecificWebPage(RdbBusiness.Checked);
		}

		protected void btnClear_Click(object sender, System.EventArgs e)
		{
			ClearTextBox();
			this.LblError.Text = "";
		}

		private void BtnNewIndProspect_Click(object sender, System.EventArgs e)
		{
			Customer.Prospect prospect = new Customer.Prospect();
			prospect.Mode = 1; //ADD
			Session["Prospect"] = prospect;

			Page.Response.Redirect("ProspectIndividual.aspx");
		}

		private void BtnNewBusProspect_Click(object sender, System.EventArgs e)
		{
			Customer.Prospect prospect = new Customer.Prospect();
			prospect.Mode = 1;
			Session["Prospect"] = prospect;

			Page.Response.Redirect("ProspectBusiness.aspx");
		}

		private void TxtEmail_TextChanged(object sender, System.EventArgs e)
		{
		
		}
	}//End searchBusiness_ItemCommand
} 
