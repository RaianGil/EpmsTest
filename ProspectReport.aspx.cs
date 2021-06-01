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
using EPolicy2.Reports;

namespace EPolicy
{
    /// <summary>
    /// Summary description for ProspectReport.
    /// </summary>
    public partial class ProspectReport : System.Web.UI.Page
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
                if (!cp.IsInRole("PROSPECTREPORT") && !cp.IsInRole("ADMINISTRATOR"))
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

            //Load DownDropList
            DataTable dtPolicyClass = LookupTables.LookupTables.GetTable("PolicyClass");

            //Policy Class
            ddlPolicyClass.DataSource = dtPolicyClass;
            ddlPolicyClass.DataTextField = "PolicyClassDesc";
            ddlPolicyClass.DataValueField = "PolicyClassID";
            ddlPolicyClass.DataBind();
            ddlPolicyClass.SelectedIndex = -1;
            ddlPolicyClass.Items.Insert(0, "");

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

        }

        private void ProspectList(bool IsBusiness)
        {
            int PolicyClass =0 ;
            if(ddlPolicyClass.SelectedItem.Value.Trim() == "")
                 PolicyClass= 0;
            else
               PolicyClass = int.Parse(ddlPolicyClass.SelectedItem.Value.Trim());

            EPolicy2.Reports.ProspectReport prospectreport = new EPolicy2.Reports.ProspectReport();
            DataTable dt = prospectreport.ProspectsList(IsBusiness, txtBegDate.Text, TxtEndDate.Text, PolicyClass);

            DataDynamics.ActiveReports.ActiveReport3 rpt;
            string CompanyHead = "";

            if (ddlPolicyClass.SelectedItem.Value.Trim() == "1")
            {
                if (dt.Rows.Count != 0)
                {
                    CompanyHead = "PREMIER WARRANTY SERVICES";
                }
            }
            else
            {
                CompanyHead = "OPTIMA INSURANCE COMPANY";
            }

            if (IsBusiness)
                rpt = new BusProspectList(txtBegDate.Text, TxtEndDate.Text);
            else
                rpt = new ProspectList(txtBegDate.Text, TxtEndDate.Text, CompanyHead);

            rpt.DataSource = dt;
            rpt.DataMember = "Report";
            rpt.Run(false);

            Session.Add("Report", rpt);
            Session.Add("FromPage", "ProspectReport.aspx");
            Response.Redirect("ActiveXViewer.aspx", false);
        }

        private void ProspectWithTask(bool IsBusiness)
        {
            EPolicy2.Reports.ProspectReport prospectreport = new EPolicy2.Reports.ProspectReport();
            DataTable dt = prospectreport.ProspectWithTasks(IsBusiness, txtBegDate.Text, TxtEndDate.Text);

            DataDynamics.ActiveReports.ActiveReport3 rpt;

            if (IsBusiness)
                rpt = new BusProspectWithTask(txtBegDate.Text, TxtEndDate.Text);
            else
                rpt = new ProspectWithTask(txtBegDate.Text, TxtEndDate.Text);

            rpt.DataSource = dt;
            rpt.DataMember = "Report";
            rpt.Run(false);

            Session.Add("Report", rpt);
            Session.Add("FromPage", "ProspectReport.aspx");
            Response.Redirect("ActiveXViewer.aspx", false);
        }

        private void ProspectWithoutQuotes(bool IsBusiness)
        {
            EPolicy2.Reports.ProspectReport prospectreport = new EPolicy2.Reports.ProspectReport();
            DataTable dt = prospectreport.ProspectWithoutQuotes(IsBusiness, txtBegDate.Text, TxtEndDate.Text);

            DataDynamics.ActiveReports.ActiveReport3 rpt;

            if (IsBusiness)
                rpt = new ProspectosSinCotizacionesNegocios(txtBegDate.Text, TxtEndDate.Text);
            else
                rpt = new ProspectosSinCotizacionesIndividuos(txtBegDate.Text, TxtEndDate.Text);

            rpt.DataSource = dt;
            rpt.DataMember = "Report";
            rpt.Run(false);

            Session.Add("Report", rpt);
            Session.Add("FromPage", "ProspectReport.aspx");
            Response.Redirect("ActiveXViewer.aspx", false);
        }

        private void ProspectWithQuotes(bool IsBusiness)
        {
            int PolicyClass = 0;
            if (ddlPolicyClass.SelectedItem.Value.Trim() == "")
                PolicyClass = 0;
            else
                PolicyClass = int.Parse(ddlPolicyClass.SelectedItem.Value.Trim());

            EPolicy2.Reports.ProspectReport prospectreport = new EPolicy2.Reports.ProspectReport();
            DataTable dt = prospectreport.ProspectWithQuotes(IsBusiness, txtBegDate.Text, TxtEndDate.Text, PolicyClass);

            DataDynamics.ActiveReports.ActiveReport3 rpt;
            string CompanyHead = "";

            if (ddlPolicyClass.SelectedItem.Value.Trim() == "1")
            {
                if (dt.Rows.Count != 0)
                {
                    CompanyHead = "PREMIER WARRANTY SERVICES";
                }
            }
            else
            {
                CompanyHead = "OPTIMA INSURANCE COMPANY";
            }

            if (IsBusiness)
                rpt = new ProspectosConCotizacionesNegocios(txtBegDate.Text, TxtEndDate.Text);
            else
                rpt = new ProspectosConCotizacionesIndividuos(txtBegDate.Text, TxtEndDate.Text, CompanyHead);


            rpt.DataSource = dt;
            rpt.DataMember = "Report";
            rpt.Run(false);

            Session.Add("Report", rpt);
            Session.Add("FromPage", "ProspectReport.aspx");
            Response.Redirect("ActiveXViewer.aspx", false);
        }

        private void ProspectConvertedToClient(bool IsBusiness)
        {
            int PolicyClass = 0;
            if (ddlPolicyClass.SelectedItem.Value.Trim() == "")
                PolicyClass = 0;
            else
                PolicyClass = int.Parse(ddlPolicyClass.SelectedItem.Value.Trim());

            EPolicy2.Reports.ProspectReport prospectreport = new EPolicy2.Reports.ProspectReport();
            DataTable dt = prospectreport.ProspectConvertedToClient(IsBusiness, txtBegDate.Text, TxtEndDate.Text, PolicyClass);

            DataDynamics.ActiveReports.ActiveReport3 rpt;

            string CompanyHead = "";

            if (ddlPolicyClass.SelectedItem.Value.Trim() == "1")
            {
                if (dt.Rows.Count != 0)
                {
                    CompanyHead = "PREMIER WARRANTY SERVICES";
                }
            }
            else
            {
                CompanyHead = "OPTIMA INSURANCE COMPANY";
            }

            if (IsBusiness)
                rpt = new BusProspectsConvertedToClients(txtBegDate.Text, TxtEndDate.Text);
            else
                rpt = new ProspectosConvirtieronClientes(txtBegDate.Text, TxtEndDate.Text, CompanyHead);


            rpt.DataSource = dt;
            rpt.DataMember = "Report";
            rpt.Run(false);

            Session.Add("Report", rpt);
            Session.Add("FromPage", "ProspectReport.aspx");
            Response.Redirect("ActiveXViewer.aspx", false);
        }

        private void FieldVerify()
        {
            string errorMessage = String.Empty;
            bool found = false;

            if (this.txtBegDate.Text == "" && this.TxtEndDate.Text != "" &&
                found == false)
            {
                errorMessage = "Please enter the beginning date.";
                found = true;
            }
            if (this.txtBegDate.Text != "" && this.TxtEndDate.Text == "" &&
                found == false)
            {
                errorMessage = "Please enter the ending date.";
                found = true;
            }
            else if (this.txtBegDate.Text == "" && this.TxtEndDate.Text == "" &&
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

            bool IsBusiness = false;

            if (ddlCustType.SelectedItem.Value == "B")
            {
                IsBusiness = true;
            }
            else
            {
                IsBusiness = false;
            }

            switch (rblProspectsReports.SelectedItem.Value)
            {
                case "0":
                    ProspectList(IsBusiness);
                    break;

                case "1":
                    ProspectWithoutQuotes(IsBusiness);
                    break;

                case "2":
                    ProspectWithQuotes(IsBusiness);
                    break;

                case "3":
                    ProspectConvertedToClient(IsBusiness);
                    break;
            }
        }

        protected void BtnExit_Click(object sender, System.EventArgs e)
        {
            Session.Clear();
            Response.Redirect("MainMenu.aspx");
        }
        protected void rblProspectsReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableControl();
        }

        private void EnableControl()
        {

            switch (rblProspectsReports.SelectedItem.Value)
            {
                case "0":
                    lblPolicyClass.Visible = true;
                    ddlPolicyClass.Visible = true;
                    break;
                case "1":
                    lblPolicyClass.Visible = false;
                    ddlPolicyClass.Visible = false;
                    break;
                case "2":
                    lblPolicyClass.Visible = true;
                    ddlPolicyClass.Visible = true;
                    break;
                case "3":
                    lblPolicyClass.Visible = true;
                    ddlPolicyClass.Visible = true;
                    break;
            }
        }
        protected void btnCalendar_ServerClick(object sender, EventArgs e)
        {

        }
}
}
