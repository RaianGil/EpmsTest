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
using EPolicy.Customer;
using EPolicy.TaskControl;

namespace EPolicy
{
	/// <summary>
	/// Summary description for ClientTasks.
	/// </summary>
	public partial class ClientTasks : System.Web.UI.Page
	{
		private DataTable DtTask;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
				Response.Redirect("Default.aspx?001");
			}
			else
			{
				if(!cp.IsInRole("CUSTOMER MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("Default.aspx?001");
				}
			}

			if(!IsPostBack)
			{
				FillDataGrid();
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

		
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void FillDataGrid()
		{
            Login.Login cp = HttpContext.Current.User as Login.Login;
			Customer.Customer customer = (Customer.Customer)Session["Customer"];

			LblError.Visible = false;
			searchIndividual.DataSource = null;
			DtTask = null;

            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            if (cp.IsInRole("AUTO VI AGENCY"))
            {
                userID = 0;
                //se envia 0 para que no filtre
            }

            DtTask = TaskControl.TaskControl.GetTaskControlByCustomerNo(customer.CustomerNo, userID);
				
			Session.Remove("DtTask");
			Session.Add("DtTask",DtTask);

			if (DtTask.Rows.Count != 0)
			{
                for (int i = 0; i < DtTask.Rows.Count; i++)
                {
                    DtTask.Rows[i]["TaskControlTypeDesc"] = DtTask.Rows[i]["TaskControlTypeDesc"].ToString().Trim().Replace("Home Owners", "Residential Property");
                }

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

		protected void btnClose_Click(object sender, System.EventArgs e)
		{
			Customer.Customer customer = (Customer.Customer) Session["Customer"];

			if(customer.IsBusiness == true)
			{
				Response.Redirect("ClientBusiness.aspx");
			}
			else
			{
                Session.Clear();
                string ToPage;
                ToPage = "ClientIndividual.aspx";

                if (Session["Customer"] == null)
                    Session.Add("Customer", customer);
                else
                    Session["Customer"] = customer;

                Response.Redirect(ToPage + "?" + customer.CustomerNo);
			}
		}
        protected void searchIndividual_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void searchIndividual_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            //RPR 2004-05-17
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

            if (e.Item.ItemType.ToString() != "Pager") // Select
            {

                int i = int.Parse(e.Item.Cells[1].Text);
                TaskControl.TaskControl taskControl =
                    TaskControl.TaskControl.GetTaskControlByTaskControlID(i, userID);

                DataTable dtFilter = (DataTable)Session["DtTask"];

                DataTable dt = dtFilter.Clone();

                DataRow[] dr = dtFilter.Select("TaskControlTypeID = " + taskControl.TaskControlTypeID, "TaskControlID");

                for (int rec = 0; rec <= dr.Length - 1; rec++)
                {
                    DataRow myRow = dt.NewRow();
                    myRow["TaskControlID"] = (int)dr[rec].ItemArray[0];
                    myRow["TaskControlTypeID"] = (int)dr[rec].ItemArray[3];

                    dt.Rows.Add(myRow);
                    dt.AcceptChanges();
                }

                taskControl.NavegationTaskControlTable = dt;

                string ToPage;

                if (Session["ToPage"] == null)
                {
                    if (taskControl.TaskControlTypeID == 4)
                    {
                        ToPage = "ExpressAutoQuote.aspx";
                    }
                    else
                    {
                        ToPage = taskControl.GetType().Name.Trim() + ".aspx";
                    }
                }
                else
                {
                    ToPage = Session["ToPage"].ToString();
                }

                Session.Clear();
                if (Session["TaskControl"] == null)
                    Session.Add("TaskControl", taskControl);
                else
                    Session["TaskControl"] = taskControl;

                Session.Remove("DtTaskControl");

                Response.Redirect(ToPage + "?" + taskControl.TaskControlID);
            }
            else  // Pager
            {
                searchIndividual.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;

                searchIndividual.DataSource = (DataTable)Session["DtTask"];
                searchIndividual.DataBind();
            }
        }
}
}
