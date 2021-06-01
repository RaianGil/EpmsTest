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
	/// Summary description for SearchPolicies.
	/// </summary>
	public partial class SearchPolicies : System.Web.UI.Page
	{
		private DataTable DtTaskPolicy;
	
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
            //this.PlaceHolder1.Controls.Add(BannerLIST);

			//Setup Left-side Banner
			//Control LeftMenu = new Control();
			//LeftMenu = LoadControl(@"LeftMenu.ascx");
			//this.phTopBanner1.Controls.Add(LeftMenu);
			
			//Load DownDropList
            DataTable dtPolicyClass = LookupTables.LookupTables.GetTable("PolicyClass");
            DataTable dtPolicyType = LookupTables.LookupTables.GetTable("PolicyType");

			//PolicyClass
			ddlPolicyClass.DataSource = dtPolicyClass;
			ddlPolicyClass.DataTextField = "PolicyClassDesc";
			ddlPolicyClass.DataValueField = "PolicyClassID";
			ddlPolicyClass.DataBind();
			ddlPolicyClass.SelectedIndex = -1;
			ddlPolicyClass.Items.Insert(0,"");

            if (ddlPolicyClass.Items.Count > 1)
                foreach (ListItem item in ddlPolicyClass.Items)
                {
                    if (item.Text != "")
                    {
                        item.Text = item.Text.Replace("Home Owners", "Residential Property");
                    }
                }

            //PolicyType
            TxtPolicyType.DataSource = dtPolicyType;
            TxtPolicyType.DataTextField = "PolicyTypeDesc";
            TxtPolicyType.DataValueField = "PolicyTypeID";
            TxtPolicyType.DataBind();
            TxtPolicyType.SelectedIndex = -1;
            TxtPolicyType.Items.Insert(0, "");
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.DtSearchPayments.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DtSearchPayments_ItemCommand);
			this.DtSearchAll.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.DtSearchAll_ItemCommand);

		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.litPopUp.Visible = false;

			Login.Login cp = HttpContext.Current.User as Login.Login;
			if (cp == null)
			{
				Response.Redirect("Default.aspx?001");
			}
			else
			{
                if (!cp.IsInRole("SEARCH POLICIES MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
				{
					Response.Redirect("Default.aspx?001");					
				}
			}

            //this.Form.DefaultButton = this.Imagebutton2.UniqueID;

			if(!IsPostBack)
			{
                //this.lblVIN.Visible = false;
                //this.txtVIN.Visible = false;

                if (!cp.IsInRole("ADMINISTRATOR"))
                {
                    if (!cp.IsInRole("GUARDIAN XTRA"))
                    {
                        //if (ddlPolicyClass.Items.Count > 2)
                            ddlPolicyClass.Items.RemoveAt(ddlPolicyClass.Items.IndexOf(ddlPolicyClass.Items.FindByText("GuardianXtra")));
                    }
                    if (!cp.IsInRole("GUARIDANROADASSISTANCE"))
                    {
                        //if (ddlPolicyClass.Items.Count > 2)
                        ddlPolicyClass.Items.RemoveAt(ddlPolicyClass.Items.IndexOf(ddlPolicyClass.Items.FindByText("GuardianRoadAssit")));
                    }
                    if (!cp.IsInRole("AUTO VI"))
                    {
                        //if (ddlPolicyClass.Items.Count > 2)
                            ddlPolicyClass.Items.RemoveAt(ddlPolicyClass.Items.IndexOf(ddlPolicyClass.Items.FindByText("Auto VI")));
                    }
                    if (!cp.IsInRole("DOUBLE INTERST"))
                    {
                        //if (ddlPolicyClass.Items.Count > 2)
                        ddlPolicyClass.Items.RemoveAt(ddlPolicyClass.Items.IndexOf(ddlPolicyClass.Items.FindByText("Double Interest Policy")));
                    }
                    if (!cp.IsInRole("AUTO PERSONAL POLICY"))
                    {
                        //if (ddlPolicyClass.Items.Count > 2)
                        ddlPolicyClass.Items.RemoveAt(ddlPolicyClass.Items.IndexOf(ddlPolicyClass.Items.FindByText("Auto Personal Policy")));
                    }
                }
			}

            //if (!cp.IsInRole("ADMINISTRATOR"))
            //{
            //    if (!cp.IsInRole("GUARDIAN XTRA") && txtHiddenIndex.Text != "G" && !cp.IsInRole("AUTO VI"))
            //    {
            //        txtHiddenIndex.Text = ""; //Para que remueva un numero y lo haga negativo
            //    }
            //    if (!cp.IsInRole("GUARDIAN XTRA") && txtHiddenIndex.Text != "G")
            //    {
            //        if (ddlPolicyClass.Items.Count > 2)
            //            ddlPolicyClass.Items.RemoveAt(3);
            //        txtHiddenIndex.Text = "G";

            //    }
            //    if (!cp.IsInRole("GUARIDANROADASSISTANCE"))
            //    {
            //        if (ddlPolicyClass.Items.Count > 2)
            //            ddlPolicyClass.Items.RemoveAt(1);
            //    }
            //    if (!cp.IsInRole("AUTO VI") && txtHiddenIndex.Text != "G")
            //    {
            //        if (ddlPolicyClass.Items.Count > 2)
            //            ddlPolicyClass.Items.RemoveAt(2);
            //        txtHiddenIndex.Text = "G";

            //    }

            //    if (!cp.IsInRole("AUTO VI") && txtHiddenIndex.Text == "" && cp.IsInRole("GUARIDANROADASSISTANCE"))
            //    {
            //        if (ddlPolicyClass.Items.Count > 2)
            //            ddlPolicyClass.Items.RemoveAt(2);
            //        txtHiddenIndex.Text = "G";

            //    }

            //}



		}

		private void ClearTextBox()
		{
			DtSearchPayments.Visible = false;
			DtSearchAll.Visible      = false;
			DtSearchPayments.DataSource = null;
			DtSearchAll = null;

			LblTotalCases.Text			= "Total Cases: 0";

			TxtPolicyNo.Text		    = "";
			//TxtPolicyType.Text		    = "";
			TxtCertificate.Text			= "";
			TxtSuffix.Text				= "";
			TxtBank.Text				= "";
			TxtLoanNo.Text				= ""; //LoneNo = Plate 
			txtVIN.Text                 = "";
            //txtVIN.Visible              = false;
            //lblVIN.Visible				= false;

			ddlPolicyClass.SelectedIndex= 0;
            TxtPolicyType.SelectedIndex = 0;
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

			DataTable dtSpec = (DataTable) Session["DtTaskPolicy"];

			int i = (int) dtSpec.Rows[0]["TaskControlID"];
			TaskControl.TaskControl taskControl = TaskControl.TaskControl.GetTaskControlByTaskControlID(i, userID);
		
			Session["Prospect"] = taskControl.Prospect;

			if(Session["DtTaskPolicy"] != null)
			{
				DataTable dtFilter = (DataTable) Session["DtTaskPolicy"];
				
				DataTable dt = dtFilter.Clone();

				DataRow[] dr = dtFilter.Select("TaskControlTypeID = "+taskControl.TaskControlTypeID,"TaskControlID");				

				/* for (int rec = 0; rec<=dr.Length-1; rec++)
				{
					DataRow myRow = dt.NewRow();
					myRow["TaskControlID"] = (int) dr[rec].ItemArray[0];
					myRow["TaskStatusID"] = (int) dr[rec].ItemArray[1];
					myRow["TaskControlTypeID"] = (int) dr[rec].ItemArray[2];
					myRow["TaskControlID1"] = (int) dr[rec].ItemArray[0];

					dt.Rows.Add(myRow);
					dt.AcceptChanges();
				} */

				taskControl.NavegationTaskControlTable = dt;

				string ToPage;

				if(Session["ToPage"] == null) 
				{
					ToPage = taskControl.GetType().Name.Trim()+".aspx";
				}
				else
				{
					ToPage = Session["ToPage"].ToString();
				}
	
				if(Session["TaskControl"] == null)
					Session.Add("TaskControl",taskControl);
				else
					Session["TaskControl"] = taskControl;

				Session.Remove("DtTaskPolicy");
	
				Response.Redirect(ToPage+"?"+taskControl.TaskControlID);
			}
		}

		private void DtSearchPayments_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{

		}

		protected void ddlPolicyClass_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            DataTable dtPolicyType = LookupTables.LookupTables.GetTable("PolicyType");
            //PolicyType
            TxtPolicyType.DataSource = dtPolicyType;
            TxtPolicyType.DataTextField = "PolicyTypeDesc";
            TxtPolicyType.DataValueField = "PolicyTypeID";
            TxtPolicyType.DataBind();
            TxtPolicyType.SelectedIndex = -1;
            TxtPolicyType.Items.Insert(0, "");

            if (ddlPolicyClass.SelectedItem.Text.ToString().Trim() == "GuardianXtra")
            {
                for (int i = TxtPolicyType.Items.Count - 1; i >= 0; i--)
                {
                    if (TxtPolicyType.Items[i].Text != "XPA" && TxtPolicyType.Items[i].Text != "XCA")
                    {
                        TxtPolicyType.Items.RemoveAt(i);
                    }
                }
            }
            else if (ddlPolicyClass.SelectedItem.Text.ToString().Trim() == "Auto VI")
            {
                for (int i = TxtPolicyType.Items.Count - 1 ; i >= 0; i--)
                {
                    if (TxtPolicyType.Items[i].Text != "PAP" && TxtPolicyType.Items[i].Text != "BAP")
                    {
                        TxtPolicyType.Items.RemoveAt(i);
                    }
                }
            }
            else if (ddlPolicyClass.SelectedItem.Text.ToString().Trim() == "GuardianRoadAssit")
            {
                for (int i = TxtPolicyType.Items.Count - 1; i >= 0; i--)
                {
                    if (TxtPolicyType.Items[i].Text != "GVI" && TxtPolicyType.Items[i].Text != "GPR")
                    {
                        TxtPolicyType.Items.RemoveAt(i);
                    }
                }
            }
            else if (ddlPolicyClass.SelectedItem.Text.ToString().Trim() == "Double Interest Policy")
            {
                for (int i = TxtPolicyType.Items.Count - 1; i >= 0; i--)
                {
                    if (TxtPolicyType.Items[i].Text != "PAP")
                    {
                        TxtPolicyType.Items.RemoveAt(i);
                    }
                }
            }
            else if (ddlPolicyClass.SelectedItem.Text.ToString().Trim() == "Auto Personal Policy")
            {
                for (int i = TxtPolicyType.Items.Count - 1; i >= 0; i--)
                {
                    if (TxtPolicyType.Items[i].Text != "PAP")
                    {
                        TxtPolicyType.Items.RemoveAt(i);
                    }
                }
            }
            else if (ddlPolicyClass.SelectedItem.Text.ToString().Trim().Contains("Bond"))
            {
                for (int i = TxtPolicyType.Items.Count - 1; i >= 0; i--)
                {
                    if (TxtPolicyType.Items[i].Text != "BND")
                    {
                        TxtPolicyType.Items.RemoveAt(i);
                    }
                }
            }
            else if (ddlPolicyClass.SelectedItem.Text.ToString().Trim().Contains("Residential Property"))
            {
                for (int i = TxtPolicyType.Items.Count - 1; i >= 0; i--)
                {
                    if (TxtPolicyType.Items[i].Text != "INC" && TxtPolicyType.Items[i].Text != "HOM")
                    {
                        TxtPolicyType.Items.RemoveAt(i);
                    }
                }
            }
            else if (ddlPolicyClass.SelectedItem.Text.ToString().Trim().Contains("Yacht"))
            {
                for (int i = TxtPolicyType.Items.Count - 1; i >= 0; i--)
                {
                    if (TxtPolicyType.Items[i].Text != "MAR")
                    {
                        TxtPolicyType.Items.RemoveAt(i);
                    }
                }
            }

//			if(this.ddlPolicyClass.SelectedItem.Value == "10" )
//			{
//				this.lblVIN.Visible = true;
//				this.txtVIN.Visible = true;
//			}
//			else
//			{
//				this.lblVIN.Visible = false;
//				this.txtVIN.Visible = false;
//			}
//			txtVIN.Text = "";



		}

		private void DtSearchAll_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{

		}

		protected void Imagebutton2_Click(object sender, System.EventArgs e)
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

			Login.Login cp = HttpContext.Current.User as Login.Login;
			int userID = 0;
			userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            if (cp.IsInRole("AUTO VI AGENCY"))
            {
                userID = 0;
                //se envia 0 para que no filtre
            }

			if(ddlPolicyClass.SelectedItem.Value != "10")
			{
				DtSearchPayments.Visible = true;
				DtSearchAll.Visible      = false;
				LblError.Visible = false;
				DtSearchPayments.DataSource = null;
				DtTaskPolicy = null;

				DtSearchPayments.CurrentPageIndex = 0;

				DtSearchPayments.Visible = false;

				int policyClass = 0;
				if (ddlPolicyClass.SelectedItem.Value.ToString() != "")
					policyClass = int.Parse(ddlPolicyClass.SelectedItem.Value.ToString());
			
				DtTaskPolicy = TaskControl.Policy.GetPolicies(policyClass,TxtPolicyType.SelectedItem.Text.Trim(),
					TxtPolicyNo.Text.Trim(),TxtCertificate.Text.Trim(),	TxtSuffix.Text.Trim(),
					TxtLoanNo.Text.Trim(), TxtBank.Text.Trim(),txtVIN.Text.Trim(),userID);//,txtVIN.Text.Trim()); //LoneNo = Plate 
				
				Session.Remove("DtTaskPolicy");
				Session.Add("DtTaskPolicy",DtTaskPolicy);

                if (DtTaskPolicy.Rows.Count != 0)
				{
                    for (int i = 0; i < DtTaskPolicy.Rows.Count; i++)
                    {
                        DtTaskPolicy.Rows[i]["PolicyClassDesc"] = DtTaskPolicy.Rows[i]["PolicyClassDesc"].ToString().Trim().Replace("Home Owners", "Residential Property");
                    }

					DtSearchPayments.Visible = true;
					DtSearchPayments.DataSource = DtTaskPolicy;
					DtSearchPayments.DataBind();

				}
				else
				{
					DtSearchPayments.DataSource = null;
					DtSearchPayments.DataBind();	
				

                    lblRecHeader.Text = "Could not find a match for your search criteria, please try again.";
                    mpeSeleccion.Show();

				}

				LblTotalCases.Text = "Total Cases: "+DtTaskPolicy.Rows.Count.ToString();	
				//Si tiene un solo record se va a dirigir a la pantalla correspondiente.
				if (DtTaskPolicy.Rows.Count == 1)
					GoToSpecificWebPage();
			}
			else
			{
				DtSearchPayments.Visible = false;
				DtSearchAll.Visible      = true;
				LblError.Visible = false;
				DtSearchPayments.DataSource = null;
				DtTaskPolicy = null;

				DtSearchAll.CurrentPageIndex = 0;

				DtSearchAll.Visible = false;

				int policyClass = 0;
				if (ddlPolicyClass.SelectedItem.Value.ToString() != "")
					policyClass = int.Parse(ddlPolicyClass.SelectedItem.Value.ToString());
			
				DtTaskPolicy = TaskControl.Policy.GetPolicies(policyClass,TxtPolicyType.SelectedItem.Text.Trim(),
					TxtPolicyNo.Text.Trim(),TxtCertificate.Text.Trim(),	TxtSuffix.Text.Trim(),
					TxtLoanNo.Text.Trim(),TxtBank.Text.Trim(),txtVIN.Text.Trim(),userID); //LoneNo = Plate 
				
				Session.Remove("DtTaskPolicy");
				Session.Add("DtTaskPolicy",DtTaskPolicy);

				if (DtTaskPolicy.Rows.Count != 0)
				{
					DtSearchAll.Visible = true;
					DtSearchAll.DataSource = DtTaskPolicy;
					DtSearchAll.DataBind();

				}
				else
				{

					DtSearchAll.DataSource = null;
					DtSearchAll.DataBind();

                    lblRecHeader.Text = "Could not find a match for your search criteria, please try again.";
                    mpeSeleccion.Show();
				}

				LblTotalCases.Text = "Total Cases: "+DtTaskPolicy.Rows.Count.ToString();

				//Si tiene un solo record se va a dirigir a la pantalla correspondiente.
				if (DtTaskPolicy.Rows.Count == 1)
					GoToSpecificWebPage();
			}
		}

		private void FieldVerify()
		{
			string errorMessage = String.Empty;
			//bool found = false;

			if (ddlPolicyClass.SelectedItem.Text.Trim() == "" &&
				TxtPolicyNo.Text.Trim() == "" && TxtPolicyType.SelectedItem.Text.Trim() == "" &&
				TxtCertificate.Text.Trim() == "" & TxtSuffix.Text.Trim() == "" &&
				TxtBank.Text.Trim() == "" && TxtLoanNo.Text.Trim() == "" && txtVIN.Text.Trim() == "") //LoneNo = Plate 
			{
				errorMessage = "Please choose fill one field.";
				//found = true;
			}

            if (ddlPolicyClass.SelectedItem.Text.Trim() == "" &&
                TxtPolicyNo.Text.Trim() == "" && TxtPolicyType.SelectedItem.Text.Trim() == "" &&
                TxtCertificate.Text.Trim() == "" & TxtSuffix.Text.Trim() == "" &&
                TxtBank.Text.Trim() == "" && TxtLoanNo.Text.Trim() != "" && txtVIN.Text.Trim() == "") //LoneNo = Plate 
            {
                errorMessage = "Please select line of buisness when searching by plate.";
                //found = true;
            }

			//throw the exception.
			if (errorMessage != String.Empty)
			{
				throw new Exception(errorMessage);
			}	
		}

		protected void Imagebutton1_Click(object sender, System.EventArgs e)
		{
			ClearTextBox();
		}

        protected void DtSearchPayments_ItemCommand1(object source, DataGridCommandEventArgs e)
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

                Session["Prospect"] = taskControl.Prospect;

                if (Session["DtTaskPolicy"] != null)
                {
                    DataTable dtFilter = (DataTable)Session["DtTaskPolicy"];

                    DataTable dt = dtFilter.Clone();

                    DataRow[] dr = dtFilter.Select("TaskControlTypeID = " + taskControl.TaskControlTypeID, "TaskControlID");

                    /* for (int rec = 0; rec <= dr.Length - 1; rec++)
                    {
                        DataRow myRow = dt.NewRow();
                        myRow["TaskControlID"] = (int)dr[rec].ItemArray[0];
                        myRow["TaskStatusID"] = (int)dr[rec].ItemArray[1];
                        myRow["TaskControlTypeID"] = (int)dr[rec].ItemArray[2];
                        myRow["TaskControlID1"] = (int)dr[rec].ItemArray[0];

                        dt.Rows.Add(myRow);
                        dt.AcceptChanges();
                    } */

                    taskControl.NavegationTaskControlTable = dt;

                    string ToPage;

                    if (Session["ToPage"] == null)
                    {
                        ToPage = taskControl.GetType().Name.Trim() + ".aspx";
                    }
                    else
                    {
                        ToPage = Session["ToPage"].ToString();
                    }

                    if (Session["TaskControl"] == null)
                        Session.Add("TaskControl", taskControl);
                    else
                        Session["TaskControl"] = taskControl;

                    Session.Remove("DtTaskPolicy");

                    Response.Redirect(ToPage + "?" + taskControl.TaskControlID);
                }
            }
            else  // Pager
            {
                DtSearchPayments.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;

                DtSearchPayments.DataSource = (DataTable)Session["DtTaskPolicy"];
                DtSearchPayments.DataBind();
            }
        }
        protected void DtSearchAll_ItemCommand1(object source, DataGridCommandEventArgs e)
        {
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

                Session["Prospect"] = taskControl.Prospect;

                if (Session["DtTaskPolicy"] != null)
                {
                    DataTable dtFilter = (DataTable)Session["DtTaskPolicy"];

                    DataTable dt = dtFilter.Clone();

                    DataRow[] dr = dtFilter.Select("TaskControlTypeID = " + taskControl.TaskControlTypeID, "TaskControlID");

                   /*  for (int rec = 0; rec <= dr.Length - 1; rec++)
                    {
                        DataRow myRow = dt.NewRow();
                        myRow["TaskControlID"] = (int)dr[rec].ItemArray[0];
                        myRow["TaskStatusID"] = (int)dr[rec].ItemArray[1];
                        myRow["TaskControlTypeID"] = (int)dr[rec].ItemArray[2];
                        myRow["TaskControlID1"] = (int)dr[rec].ItemArray[0];

                        dt.Rows.Add(myRow);
                        dt.AcceptChanges();
                    } */

                    taskControl.NavegationTaskControlTable = dt;

                    string ToPage;

                    if (Session["ToPage"] == null)
                    {
                        ToPage = taskControl.GetType().Name.Trim() + ".aspx";
                    }
                    else
                    {
                        ToPage = Session["ToPage"].ToString();
                    }

                    if (Session["TaskControl"] == null)
                        Session.Add("TaskControl", taskControl);
                    else
                        Session["TaskControl"] = taskControl;

                    Session.Remove("DtTaskPolicy");

                    Response.Redirect(ToPage + "?" + taskControl.TaskControlID);
                }
            }
            else  // Pager
            {
                DtSearchAll.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;

                DtSearchAll.DataSource = (DataTable)Session["DtTaskPolicy"];
                DtSearchAll.DataBind();
            }
        }
}
}
