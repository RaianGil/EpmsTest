namespace EPolicy.EPolicyWeb.Components
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using EPolicy;

	/// <summary>
	///		Summary description for LeftReportMenu.
	/// </summary>
	public partial  class LeftReportMenu : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
			if (cp == null)
			{
				Response.Redirect("Default.aspx?001");
			}
			else
			{
				VerifyAccess(cp);
			}
		}

		private void VerifyAccess(EPolicy.Login.Login cp)
		{
			if(!cp.IsInRole("BTNPROSPECTREPORTSLEFTREPORTMENU") && !cp.IsInRole("ADMINISTRATOR"))
			{
				this.btnProspectReport.Visible = false;
			}
			else
			{
				this.btnProspectReport.Visible = true;
			}

			if(!cp.IsInRole("BTNCLIENTREPORTSLEFTREPORTMENU") && !cp.IsInRole("ADMINISTRATOR"))
			{
				this.btnClientReport.Visible = false;
			}
			else
			{
				this.btnClientReport.Visible = true;
			}

			if(!cp.IsInRole("BTNPOLICIESREPORTSLEFTREPORTMENU") && !cp.IsInRole("ADMINISTRATOR"))
			{
				this.btnPoliciesReports.Visible = false;
			}
			else
			{
				this.btnPoliciesReports.Visible = true;
			}

			if(!cp.IsInRole("BTNQUERIESGROUPREPORTSLEFTREPORTMENU") && !cp.IsInRole("ADMINISTRATOR"))
			{
				this.btnQueriesGroupReport.Visible = false;
			}
			else
			{
				this.btnQueriesGroupReport.Visible = true;
			}

			if(!cp.IsInRole("BTNPAYMENTREPORTSLEFTREPORTMENU") && !cp.IsInRole("ADMINISTRATOR"))
			{
				this.btnPaymentReports.Visible = false;
			}
			else
			{
				this.btnPaymentReports.Visible = true;
			}

			if(!cp.IsInRole("BTNCOMMISSIONREPORTSLEFTREPORTMENU") && !cp.IsInRole("ADMINISTRATOR"))
			{
				this.btnCommissionReport.Visible = false;
			}
			else
			{
				this.btnCommissionReport.Visible = true;
			}

			if(!cp.IsInRole("BTNINCENTIVELEFTREPORTMENU") && !cp.IsInRole("ADMINISTRATOR"))
			{
				this.btnIncentiveReport.Visible = false;
			}
			else
			{
				this.btnIncentiveReport.Visible = true;
			}

            if (!cp.IsInRole("BTNACCOUNTINGSUMMARYLEFTREPORTMENU") && !cp.IsInRole("ADMINISTRATOR"))
            {
                this.btnAccountingSummary.Visible = false;
            }
            else
            {
                this.btnAccountingSummary.Visible = true;
            }

            if (!cp.IsInRole("BTNADJUSTMENTREPORTLEFTREPORTMENU") && !cp.IsInRole("ADMINISTRATOR"))
            {
                this.btnAdjustmentReports.Visible = false;
            }
            else
            {
                this.btnAdjustmentReports.Visible = true;
            }

            if (!cp.IsInRole("BTNINACTIVEUSERLEFTREPORTMENU") && !cp.IsInRole("ADMINISTRATOR"))
            {
                this.btnInactiveUsers.Visible = false;
            }
            else
            {
                this.btnInactiveUsers.Visible = true;
            }

            if (!cp.IsInRole("BTNSALESREPORTLEFTREPORTMENU") && !cp.IsInRole("ADMINISTRATOR"))
            {
                this.btnSalesReport.Visible = false;
            }
            else
            {
                this.btnSalesReport.Visible = true;
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
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		protected void Button8_Click(object sender, System.EventArgs e)
		{
			Session.Clear();
			Response.Redirect("ProspectReport.aspx");
		}

		protected void Button3_Click(object sender, System.EventArgs e)
		{
			Session.Clear();
			Response.Redirect("MainMenu.aspx");
		}

		protected void Button10_Click(object sender, System.EventArgs e)
		{
			Session.Clear();
			Response.Redirect("ClientReport.aspx");
		}

		protected void Button11_Click(object sender, System.EventArgs e)
		{
			Session.Clear();
			Response.Redirect("PoliciesReports.aspx");
		}

		protected void Button12_Click(object sender, System.EventArgs e)
		{
			Session.Clear();
			Response.Redirect("PaymentsReport.aspx");
		}

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			Session.Clear();
			Response.Redirect("CommissionReport.aspx");
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			Session.Clear();
			Response.Redirect("IncentiveReport.aspx");
		}

		protected void btnPoliciesReports_Click(object sender, System.EventArgs e)
		{
			Session.Clear();
			Response.Redirect("QueriesGroupReports.aspx");
		}
        protected void btnAdjustmentReports_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("AdjustmentReport.aspx");
        }
        protected void btnInactiveUsers_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("InactiveUserReport.aspx");
        }
        protected void btnAccountingSummary_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("AccountingSummary.aspx");
        }
        protected void btnSalesReport_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("SalesReports.aspx");
        }
}
}
