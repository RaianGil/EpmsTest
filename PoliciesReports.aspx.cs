using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Xml;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Baldrich.DBRequest;
using EPolicy.XmlCooker;
using System.IO;
using System.Net;
using System.Text;
using EPolicy;
using EPolicy.LookupTables;
using EPolicy.TaskControl;
using EPolicy2.Reports;
using Microsoft.Reporting.WebForms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using System.Configuration;

namespace EPolicy
{
	/// <summary>
	/// Summary description for AutoGuardServicesContractReport.
	/// </summary>
    public partial class PoliciesReports : System.Web.UI.Page
    {

        private static string _userID = "";
        private string FileName;
        protected void Page_Load(object sender, System.EventArgs e)
        {
            
            Session["IsEffectiveDate"] = false;
            Session["BegDate"] = "";
            Session["EndDate"] = "";
            Session["Agent"] = "";
            Session["PolicyType"] = "";
            Session["rdlcDoc"] = "";
            Session["ReportType"] = "";
            Session["CompanyPolicy"] = "";

            this.litPopUp.Visible = false;

            Control Banner = new Control();
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.phTopBanner.Controls.Add(Banner);

            Login.Login cp = HttpContext.Current.User as Login.Login;
            
            if (cp == null)
            {
                Response.Redirect("Default.aspx?001");
            }
            else
            {
                if (!cp.IsInRole("REPORT MAIN MENU") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    Response.Redirect("Default.aspx?001");
                }
            }

            if (!IsPostBack)
            {

                if (!cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("GUARDIAN CENTRAL OFFICE") && !cp.IsInRole("AUTO VI ADMINISTRATOR"))
                {
                    if (rblAutoGuardReports.Items.Count > 4)
                    {
                        rblAutoGuardReports.Items.RemoveAt(4);
                        //rblAutoGuardReports.Items.RemoveAt(3);
                    }
                }
                if (!cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("GUARDIAN CENTRAL OFFICE") && !cp.IsInRole("GUARDIAN XTRA") && !cp.IsInRole("AUTO VI ADMINISTRATOR"))
                {
                    if (rblAutoGuardReports.Items.Count > 4)
                    {
                        rblAutoGuardReports.Items.RemoveAt(3);
                        rblAutoGuardReports.Items.RemoveAt(3);
                    }
                }
                if (!cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("GUARDIAN CENTRAL OFFICE") && !cp.IsInRole("AUTO VI"))
                {

                }

                _userID = cp.Identity.Name.Split("|".ToCharArray())[1];
                EnableControl();
                FillTextControl();
                rblAutoGuardReports.Focus();

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



            // Control LeftMenu = new Control();
            // LeftMenu = LoadControl(@"LeftReportMenu.ascx");
            // //((Baldrich.BaldrichWeb.Components.MenuEventControl)LeftMenu).Height = "534px";
            //// phTopBanner1.Controls.Add(LeftMenu);

            if (!IsPostBack)
            {

                //Load DownDropList
                DataTable dtCompanyDealer = LookupTables.LookupTables.GetTable("CompanyDealer");
                DataTable dtInsuranceCompany = LookupTables.LookupTables.GetTable("InsuranceCompany");
                DataTable dtBank = LookupTables.LookupTables.GetTable("Bank");
                DataTable dtPolicyClass = LookupTables.LookupTables.GetTable("PolicyClass");
                DataTable dtPolicyType = LookupTables.LookupTables.GetTable("PolicyType");
                DataTable dtYears = LookupTables.LookupTables.GetTable("VehicleYear");
                DataTable dtAgent = LookupTables.LookupTables.GetTable("AgentList");

                //CompanyDealer
                ddlDealer.DataSource = dtCompanyDealer;
                ddlDealer.DataTextField = "CompanyDealerDesc";
                ddlDealer.DataValueField = "CompanyDealerID";
                ddlDealer.DataBind();
                ddlDealer.SelectedIndex = -1;
                ddlDealer.Items.Insert(0, "");

                //Bank
                ddlBank.DataSource = dtBank;
                ddlBank.DataTextField = "BankDesc";
                ddlBank.DataValueField = "BankID";
                ddlBank.DataBind();
                ddlBank.SelectedIndex = -1;
                ddlBank.Items.Insert(0, "");

                //Agent List
                ddlAgent.DataSource = dtAgent;
                ddlAgent.DataTextField = "AgentDesc";
                ddlAgent.DataValueField = "AgentID";
                ddlAgent.DataBind();
                ddlAgent.SelectedIndex = -1;
                ddlAgent.Items.Insert(0, "");

                //InsuranceCompany
                ddlInsuranceCompany.DataSource = dtInsuranceCompany;
                ddlInsuranceCompany.DataTextField = "InsuranceCompanyDesc";
                ddlInsuranceCompany.DataValueField = "InsuranceCompanyID";
                ddlInsuranceCompany.DataBind();
                ddlInsuranceCompany.SelectedIndex = -1;
                ddlInsuranceCompany.Items.Insert(0, "");

                //Policy Type
                ddlPolicyType.DataSource = dtPolicyType;
                ddlPolicyType.DataTextField = "PolicyTypeDesc";
                ddlPolicyType.DataValueField = "PolicyTypeID";
                ddlPolicyType.DataBind();
                ddlPolicyType.SelectedIndex = -1;
                ddlPolicyType.Items.Insert(0, "");


                //Policy Class
                ddlPolicyClass.DataSource = dtPolicyClass;
                ddlPolicyClass.DataTextField = "PolicyClassDesc";
                ddlPolicyClass.DataValueField = "PolicyClassID";
                ddlPolicyClass.DataBind();
                ddlPolicyClass.SelectedIndex = -1;
                ddlPolicyClass.Items.Insert(0, "");

                //Years
                ddlYears.DataSource = dtYears;
                ddlYears.DataTextField = "VehicleYearDesc";
                ddlYears.DataValueField = "VehicleYearDesc";
                ddlYears.DataBind();
                ddlYears.SelectedIndex = -1;
                ddlYears.Items.Insert(0, "");
            }
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPrint1.Click += new System.Web.UI.ImageClickEventHandler(this.btnPrint_Click);

        }
        #endregion

        protected void btnPrint_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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

            switch (rblAutoGuardReports.SelectedItem.Value)
            {
                case "0":
                    AutoGuardPremiumWritten();
                    break;

                case "1":
                    AutoGuardCertificateOutstanding();
                    break;

                case "2":
                    AutoGuardCertificatePaid();
                    break;

                case "3":
                    MonthlyPolicyProduction();
                    //CancellationNotice();
                    break;

                case "4":
                    AnualPolicyProduction();   //FollowUpCancellation();
                    break;

                case "5":
                    //AutoGuardTodayPayments();
                    break;

                case "6":
                    CertificatePaidAndEffectivity();
                    break;

            }
        }

        protected void BtnExit_Click(object sender, System.EventArgs e)
        {
            Session.Clear();
            Response.Redirect("MainMenu.aspx", false);

        }

        private void FieldVerify()
        {
            string errorMessage = String.Empty;
            bool found = false;

            if (this.rblAutoGuardReports.SelectedItem.Value == "0" || this.rblAutoGuardReports.SelectedItem.Value == "2"
                || this.rblAutoGuardReports.SelectedItem.Value == "5" || this.rblAutoGuardReports.SelectedItem.Value == "4")
            {
                if (this.TxtEndDate.Text == "" &&
                    found == false)
                {
                    errorMessage = "Please enter the ending date.";
                    found = true;
                }
            }

            if (ddlCompanyType.SelectedItem.Text == "")
            {
                errorMessage = "Please select the line of business.";
                found = true;
            }

            //throw the exception.
            if (errorMessage != String.Empty)
            {
                throw new Exception(errorMessage);
            }

            //if (this.rblAutoGuardReports.SelectedItem.Value == "1")
            //{
            //    if (this.txtOutstandingDate.Text == "")
            //    {
            //        errorMessage = "Please enter the outstanding date.";
            //        found = true;
            //    }

            //    //throw the exception.
            //    if (errorMessage != String.Empty)
            //    {
            //        throw new Exception(errorMessage);
            //    }
            //}
        }

        private void PremiumCancellation()
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = 0;
                _userID = cp.Identity.Name.Split("|".ToCharArray())[1];
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
                DataTable dt = null;
                DataDynamics.ActiveReports.ActiveReport3 rpt = null;

                string dateType = ddlDateType.SelectedItem.Value.Trim();
                string mHead = "";
                string CompanyHead = "";

                if (ddlDateType.SelectedItem.Value.Trim() == "E")
                    mHead = "Premium Cancellations - Cancellation Entry Date Criteria";
                else
                    mHead = "Premium Cancellations - Cancellation Date Criteria";


                dt = appAutoreport.AutoGuardPremiumWritten(txtBegDate.Text, TxtEndDate.Text, ddlDealer.SelectedItem.Value.Trim(), ddlAgent.SelectedItem.Value.Trim(), dateType, ddlPolicyClass.SelectedItem.Value.Trim(), userID, ddlFilter.SelectedItem.Value.Trim());

                if (ddlPolicyClass.SelectedItem.Value.Trim() != "")
                {
                    if (dt.Rows.Count != 0)
                    {
                        CompanyHead = dt.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();
                    }
                }
                else
                {
                    CompanyHead = "";
                }

                rpt = new EPolicy2.Reports.AutoGuard.PremiumCancellation(txtBegDate.Text, TxtEndDate.Text, mHead, ChkSummary.Checked, CompanyHead);

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("There is no existing information for this report");
                }

                rpt.DataSource = dt;
                rpt.DataMember = "Report";

                rpt.Document.Printer.PrinterName = "";

                rpt.Run(false);

                Session.Add("Report", rpt);
                Session.Add("FromPage", "PoliciesReports.aspx");
                Response.Redirect("ActiveXViewer.aspx", false);
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }

        private void FillTextControl()
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            Login.Login cp = HttpContext.Current.User as Login.Login;

            if (!cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("GUARDIAN CENTRAL OFFICE") && !cp.IsInRole("AUTO VI ADMINISTRATOR"))
            {

                if (cp.IsInRole("GUARDIAN XTRA"))
                {
                    ddlCompanyType.SelectedIndex = 2;
                    ddlCompanyType_SelectedIndexChanged(this, EventArgs.Empty);
                    ddlCompanyType.Enabled = false;
                }
                else
                {
                    ddlCompanyType.SelectedIndex = 1;
                    //ddlCompanyType_SelectedIndexChanged(this, EventArgs.Empty);
                    //ddlCompanyType.Enabled = false;
                }

                DataTable dt = EPolicy.Customer.Customer.GetAgentByUserID(_userID);

                if (dt.Rows.Count > 0)
                {
                    ddlAgent.SelectedItem.Text = dt.Rows[0]["AgentDesc"].ToString().Trim();
                    ddlAgent.SelectedItem.Value = dt.Rows[0]["AgentID"].ToString().Trim();
                    ddlAgent.Enabled = false;
                }
                else
                {
                    ddlAgent.SelectedItem.Text = "";
                    ddlAgent.Enabled = false;
                }

            }
            else if (cp.IsInRole("AUTO VI ADMINISTRATOR") && !cp.IsInRole("ADMINISTRATOR"))
            {
                ddlCompanyType.SelectedIndex = 1;
                //ddlCompanyType_SelectedIndexChanged(this, EventArgs.Empty);
                //ddlCompanyType.Enabled = false;
            }




        }
        private void AutoGuardPremiumWritten()
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
                DataTable dt = null;
                DataDynamics.ActiveReports.ActiveReport3 rpt = null;

                string dateType = ddlDateType.SelectedItem.Value.Trim();
                string mHead = "";
                string CompanyHead = "";

                if (ddlFilter.SelectedItem.Value.Trim() == "A")
                    mHead = "Premium written & Cancellations";

                if (ddlFilter.SelectedItem.Value.Trim() == "P")
                    mHead = "Premium written";

                if (ddlFilter.SelectedItem.Value.Trim() == "C")
                    mHead = "Cancellations";

                if (ddlCancellationMethod.SelectedItem.Value.Trim() == "0")
                    mHead = mHead + " - All Cancellation Methods";
                else
                    if (ddlCancellationMethod.SelectedItem.Value.Trim() == "1")
                        mHead = mHead + " - ProRata";
                    else
                        if (ddlCancellationMethod.SelectedItem.Value.Trim() == "2")
                            mHead = mHead + " - ShortRate";
                        else
                            if (ddlCancellationMethod.SelectedItem.Value.Trim() == "3")
                                mHead = mHead + " - Flat";

                if (ddlDateType.SelectedItem.Value.Trim() == "E")
                    mHead = mHead + " - Entry Date Criteria";
                else
                    mHead = mHead + " - Effective Date Criteria";


                string IsPending = "0";
                if (ddlPolicyClass.SelectedItem.Value == "1" || ddlPolicyClass.SelectedItem.Value == "16" || ddlPolicyClass.SelectedItem.Value == "13" || ddlPolicyClass.SelectedItem.Value == "17") // VSC, QCertified, ETCH,PP
                {
                    if (ddlVSCPending.SelectedItem.Value.ToUpper() == "PENDING")
                        IsPending = "1";
                    if (ddlVSCPending.SelectedItem.Value.ToUpper() == "ACTIVE")
                        IsPending = "0";
                    if (ddlVSCPending.SelectedItem.Value.ToUpper().Trim() == "")
                        IsPending = "";
                }

                string PolicyType = "";
                if (ddlPolicyClass.SelectedItem.Value == "3")
                {
                    PolicyType = ddlPolicyType.SelectedItem.Value.Trim();
                }
              
                dt = appAutoreport.AutoGuardPremiumWritten(txtBegDate.Text, TxtEndDate.Text, ddlDealer.SelectedItem.Value.Trim(), ddlAgent.SelectedItem.Value.Trim(), dateType, ddlPolicyClass.SelectedItem.Value.Trim(), userID, IsPending, PolicyType, ddlFilter.SelectedItem.Value.Trim(),  ddlCancellationMethod.SelectedItem.Value.Trim(), ddlInsuranceCompany.SelectedItem.Value.Trim());

                if (ddlPolicyClass.SelectedItem.Value.Trim() != "")
                {
                    if (ddlInsuranceCompany.SelectedItem.Value.Trim() != "")
                        CompanyHead = ddlInsuranceCompany.SelectedItem.Text.Trim();
                    else
                        CompanyHead = "All Companies";
                }
                else
                {
                    CompanyHead = "All Companies";
                }

                if (ddlFilter.SelectedItem.Value.Trim() == "A")
                    rpt = new EPolicy2.Reports.AutoGuard.AutoGuardPremiumWritten(txtBegDate.Text, TxtEndDate.Text, mHead, ChkSummary.Checked, CompanyHead);

                if (ddlFilter.SelectedItem.Value.Trim() == "P")
                    rpt = new EPolicy2.Reports.AutoGuard.AutoGuardPremiumWrittenPremium(txtBegDate.Text, TxtEndDate.Text, mHead, ChkSummary.Checked, CompanyHead);

                if (ddlFilter.SelectedItem.Value.Trim() == "C")
                    rpt = new EPolicy2.Reports.AutoGuard.AutoGuardPremiumWrittenCancellation(txtBegDate.Text, TxtEndDate.Text, mHead, ChkSummary.Checked, CompanyHead);

                //    break;

                //case "1":
                //    dt = appAutoreport.AutoGuardPremiumWrittenByBank(txtBegDate.Text, TxtEndDate.Text, ddlBank.SelectedItem.Value.Trim(), dateType, IsPending);

                //    if (ddlPolicyClass.SelectedItem.Value.Trim() != "")
                //    {
                //        if (dt.Rows.Count != 0)
                //        {
                //            CompanyHead = dt.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();
                //        }
                //    }
                //    else
                //    {
                //        CompanyHead = "";
                //    }

                //    rpt = new EPolicy2.Reports.AutoGuard.AutoGuardPremiumWrittenByBank(txtBegDate.Text, TxtEndDate.Text, mHead, ChkSummary.Checked, CompanyHead);
                //    break;

                //    case "2":
                //        dt = appAutoreport.AutoGuardPremiumWrittenByDealerIns(txtBegDate.Text, TxtEndDate.Text, ddlDealer.SelectedItem.Value.Trim(), ddlInsuranceCompany.SelectedItem.Value.Trim(), dateType, IsPending);

                //        if (ddlPolicyClass.SelectedItem.Value.Trim() != "")
                //        {
                //            if (dt.Rows.Count != 0)
                //            {
                //                CompanyHead = dt.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();
                //            }
                //        }
                //        else
                //        {
                //            CompanyHead = "";
                //        }

                //        rpt = new EPolicy2.Reports.AutoGuard.AutoGuardPremiumWrittenByDealerIns(txtBegDate.Text, TxtEndDate.Text, mHead, ChkSummary.Checked, CompanyHead);
                //        break;

                //}


                if (dt.Rows.Count == 0)
                {
                    throw new Exception("There is no existing information for this report");
                }

                //DataDynamics.ActiveReports.ActiveReport rpt = new AutoGuardPremiumWritten(txtBegDate.Text,TxtEndDate.Text,"Premium written & Cancellations",ChkSummary.Checked);

                //rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                rpt.DataSource = dt;
                rpt.DataMember = "Report";

                rpt.Document.Printer.PrinterName = "";

                rpt.Run(false);

                Session.Add("Report", rpt);
                Session.Add("FromPage", "PoliciesReports.aspx");
                Response.Redirect("ActiveXViewer.aspx", false);
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }

        private void AutoGuardCertificateOutstanding()
        {
            try
            {
                EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
                string CompanyHead = "";
                DataTable dt = null;
                DataDynamics.ActiveReports.ActiveReport3 rpt = null;

                switch (rblPremWrittenOrder.SelectedItem.Value)
                {
                    case "0":
                        dt = appAutoreport.AutoGuardCertificateOutstanding(txtOutstandingDate.Text, 8, ddlDealer.SelectedItem.Value.Trim(), ddlPolicyClass.SelectedItem.Value.Trim(), chkPartial.Checked, ddlInsuranceCompany.SelectedItem.Value.Trim());
                        if (ddlPolicyClass.SelectedItem.Value.Trim() != "")
                        {
                            if (dt.Rows.Count != 0)
                            {
                                CompanyHead = dt.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();
                            }
                        }
                        else
                        {
                            CompanyHead = "";
                        }

                        rpt = new AutoGuardCertificateOustanding(txtOutstandingDate.Text, "Contract Receivable Aging", ChkSummary.Checked, CompanyHead);
                        break;

                    case "1":
                        dt = appAutoreport.AutoGuardCertificateOutstandingByBank(txtOutstandingDate.Text, 8, ddlBank.SelectedItem.Value.Trim(), ddlPolicyClass.SelectedItem.Value.Trim(), chkPartial.Checked, ddlInsuranceCompany.SelectedItem.Value.Trim());
                        if (ddlPolicyClass.SelectedItem.Value.Trim() != "")
                        {
                            if (dt.Rows.Count != 0)
                            {
                                CompanyHead = dt.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();
                            }
                        }
                        else
                        {
                            CompanyHead = "";
                        }

                        rpt = new AutoGuardCertificateOutstandingByBank(txtOutstandingDate.Text, "Contract Receivable Aging", ChkSummary.Checked, CompanyHead);
                        break;
                }

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("There is no existing information for this report");
                }



                //rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                rpt.DataSource = dt;
                rpt.DataMember = "Report";

                rpt.Document.Printer.PrinterName = "";

                rpt.Run(false);

                Session.Add("Report", rpt);
                Session.Add("FromPage", "PoliciesReports.aspx");
                Session.Add("FromAging", true);
                Response.Redirect("ActiveXViewer.aspx", false);
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }

        private void AutoGuardCertificatePaid()
        {
            try
            {
                //TaskControl.AutoGuardServicesContract taskControl = new TaskControl.AutoGuardServicesContract();// (TaskControl.AutoGuardServicesContract) Session["TaskControl"];
                EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
                DataTable dt = null;
                DataDynamics.ActiveReports.ActiveReport3 rpt = null;
                string CompanyHead = "";

                switch (rblCertificatePaidOrder.SelectedItem.Value)
                {
                    case "0":
                        dt = appAutoreport.AutoGuardCertificatePaid(txtBegDate.Text, TxtEndDate.Text, ddlDealer.SelectedItem.Value.Trim(), ddlInsuranceCompany.SelectedItem.Value.Trim(), ddlPolicyClass.SelectedItem.Value.Trim());

                        if (ddlPolicyClass.SelectedItem.Value.Trim() != "")
                        {
                            if (dt.Rows.Count != 0)
                            {
                                CompanyHead = dt.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();
                            }
                        }
                        else
                        {
                            CompanyHead = "";
                        }

                        rpt = new AutoGuardCertificatePaid(txtBegDate.Text, TxtEndDate.Text, "Policies & Cancellations Paid", ChkSummary.Checked, dt, CompanyHead);
                        break;

                    case "1":
                        dt = appAutoreport.AutoGuardCertificatePaid2(txtBegDate.Text, TxtEndDate.Text, ddlDealer.SelectedItem.Value.Trim());

                        if (ddlPolicyClass.SelectedItem.Value.Trim() != "")
                        {
                            if (dt.Rows.Count != 0)
                            {
                                CompanyHead = dt.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();
                            }
                        }
                        else
                        {
                            CompanyHead = "";
                        }

                        rpt = new AutoGuardCertificate2(txtBegDate.Text, TxtEndDate.Text, "Policies & Cancellations Paid", ChkSummary.Checked, dt, CompanyHead);
                        break;

                    case "2":
                        dt = appAutoreport.AutoGuardCertificateNonCommission(txtBegDate.Text, TxtEndDate.Text, ddlDealer.SelectedItem.Value.Trim(), ddlInsuranceCompany.SelectedItem.Value.Trim());

                        if (ddlPolicyClass.SelectedItem.Value.Trim() != "")
                        {
                            if (dt.Rows.Count != 0)
                            {
                                CompanyHead = dt.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();
                            }
                        }
                        else
                        {
                            CompanyHead = "";
                        }

                        rpt = new AutoGuardCertificateNonCommission(txtBegDate.Text, TxtEndDate.Text, "Policies & Cancellations Paid", ChkSummary.Checked, dt, CompanyHead);
                        break;

                }


                if (dt.Rows.Count == 0)
                {
                    throw new Exception("There is no existing information for this report");
                }

                //rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                rpt.DataSource = dt;
                rpt.DataMember = "Report";

                rpt.Document.Printer.PrinterName = "";

                rpt.Run(false);

                Session.Add("Report", rpt);
                Session.Add("FromPage", "PoliciesReports.aspx");
                Response.Redirect("ActiveXViewer.aspx", false);
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }

            //			EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
            //			DataTable dt = appAutoreport.AutoGuardCertificatePaid(txtBegDate.Text,TxtEndDate.Text,ddlDealer.SelectedItem.Value.Trim(),ddlInsuranceCompany.SelectedItem.Value.Trim());
            //	
            //			try
            //			{
            //				if (dt.Rows.Count == 0)
            //				{
            //					throw new Exception("Don't exist any information for this report");
            //				}
            //
            //				DataDynamics.ActiveReports.ActiveReport rpt = new AutoGuardCertificatePaid(txtBegDate.Text,TxtEndDate.Text,"Certificate Paid & Cancellations",ChkSummary.Checked, dt);
            //
            //				rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;
            //			
            //				rpt.DataSource = dt;
            //				rpt.DataMember = "Report";
            //				rpt.Run(false);
            //
            //				Session.Add("Report",rpt);
            //				Session.Add("FromPage","PoliciesReports.aspx");
            //				Response.Redirect("ActiveXViewer.aspx",false);
            //			}
            //			catch (Exception exp)
            //			{
            //				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
            //				this.litPopUp.Visible = true;
            //				return;
            //			}
        }

        private void CancellationNotice()
        {
            
        }

        //		private void FollowUpCancellation()
        //		{
        //			EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
        //			DataTable dt = appAutoreport.AutoGuardFollowUpCancellation(txtFollowUpCancellation.Text);
        //	
        //			try
        //			{
        //				if (dt.Rows.Count == 0)
        //				{
        //					throw new Exception("Don't exist any information for this report");
        //				}
        //
        //				DataDynamics.ActiveReports.ActiveReport rpt = new FollowUpCancellation(txtFollowUpCancellation.Text,"Follow Up Cancellation",ChkSummary.Checked);
        //
        //				rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;
        //			
        //				rpt.DataSource = dt;
        //				rpt.DataMember = "Report";
        //				rpt.Run(false);
        //
        //				Session.Add("Report",rpt);
        //				Session.Add("FromPage","PoliciesReports.aspx");
        //				Response.Redirect("ActiveXViewer.aspx",false);
        //			}
        //			catch (Exception exp)
        //			{
        //				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
        //				this.litPopUp.Visible = true;
        //				return;
        //			}
        //		}

        //		private void AutoGuardTodayPayments()
        //		{
        //			EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
        //			DataTable dt = appAutoreport.AutoGuardTodayPayments(txtFollowUpCancellation.Text);
        //	
        //			try
        //			{
        //				if (dt.Rows.Count == 0)
        //				{
        //					throw new Exception("Don't exist any information for this report");
        //				}
        //
        //				DataDynamics.ActiveReports.ActiveReport rpt = new TodayPayments(txtFollowUpCancellation.Text,"Today Payments",ChkSummary.Checked);
        //
        //				rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;
        //			
        //				rpt.DataSource = dt;
        //				rpt.DataMember = "Report";
        //				rpt.Run(false);
        //
        //				Session.Add("Report",rpt);
        //				Session.Add("FromPage","PoliciesReports.aspx");
        //				Response.Redirect("ActiveXViewer.aspx",false);
        //			}
        //			catch (Exception exp)
        //			{
        //				this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
        //				this.litPopUp.Visible = true;
        //				return;
        //			}
        //		}

        private void VSCContractControl(int PolicyClassID)
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
                DataDynamics.ActiveReports.ActiveReport3 rpt = null;

                string dateType = ddlDateType.SelectedItem.Value.Trim();
                string mHead = "";
                string CompanyHead = "";

                mHead = "VSC Control Contract";

                DataTable dt = appAutoreport.VSCContractCounter(ddlDealer.SelectedItem.Value.Trim(), PolicyClassID);
                
                CompanyHead = "PREMIER WARRANTY SERVICES";

                rpt = new EPolicy2.Reports.AutoGuard.VSCContractCounter("", "", mHead, ChkSummary.Checked, CompanyHead);

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("There is no existing information for this report");
                }

                rpt.DataSource = dt;
                rpt.DataMember = "Report";

                rpt.Document.Printer.PrinterName = "";

                rpt.Run(false);

                Session.Add("Report", rpt);
                Session.Add("FromPage", "PoliciesReports.aspx");
                Response.Redirect("ActiveXViewer.aspx", false);
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }

        private DataTable GetAutoGuardDetailInfo(int taskControlID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, taskControlID.ToString(),
                ref cookItems);

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
                dt = exec.GetQuery("GetAutoGuardCancellationNotice2", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not validated the Auto Guard Report.", ex);
            }
        }

        private void CertificatePaidAndEffectivity()
        {
            EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
            DataTable dt = appAutoreport.AutoGuardCertificatePaidAndEffectivity(txtBegDate.Text, TxtEndDate.Text, ddlDealer.SelectedItem.Value.Trim(), "");
            string CompanyHead = "";

            if (ddlPolicyClass.SelectedItem.Value.Trim() != "")
            {
                if (dt.Rows.Count != 0)
                {
                    CompanyHead = dt.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();
                }
            }
            else
            {
                CompanyHead = "";
            }

            try
            {
                if (dt.Rows.Count == 0)
                {
                    throw new Exception("There is no existing information for this report");
                }

                DataDynamics.ActiveReports.ActiveReport3 rpt = new AutoGuardCertificatePaidEffective(txtBegDate.Text, TxtEndDate.Text, "Certificate Paid & Effectivity", ChkSummary.Checked, CompanyHead);

                //rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                rpt.DataSource = dt;
                rpt.DataMember = "Report";

                rpt.Document.Printer.PrinterName = "";

                rpt.Run(false);

                Session.Add("Report", rpt);
                Session.Add("FromPage", "PoliciesReports.aspx");
                Response.Redirect("ActiveXViewer.aspx", false);
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }

        private void MonthlyPolicyProduction()
        {
            try
            {
                EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
                DataTable dt = null;
                DataDynamics.ActiveReports.ActiveReport3 rpt = null;

                string dateType = ddlDateType.SelectedItem.Value.Trim();
                string mHead = "";
                string CompanyHead = "";
                string InsCompanyHeader = "Insurance Company: ";

                if (ddlInsuranceCompany.SelectedItem.Value.Trim() != "")
                    InsCompanyHeader = InsCompanyHeader + ddlInsuranceCompany.SelectedItem.Text.Trim();
                else
                    InsCompanyHeader = InsCompanyHeader + "All";

                if (ddlDateType.SelectedItem.Value.Trim() == "E")
                    mHead = "Premium written & Cancellations - Entry Date Criteria";
                else
                    mHead = "Premium written & Cancellations - Effective Date Criteria";


                dt = appAutoreport.MonthlyPolicyProduction(ddlMonths.SelectedItem.Value.Trim(), ddlYears.SelectedItem.Value.Trim(), ddlPolicyClass.SelectedItem.Value.Trim(), ddlInsuranceCompany.SelectedItem.Value.Trim());
                rpt = new EPolicy2.Reports.AutoGuard.MonthlyPolicyProduction(ddlPolicyClass.SelectedItem.Text.Trim() + " - Month: " + ddlMonths.SelectedItem.Text.Trim() + " " + ddlYears.SelectedItem.Value.Trim(), ddlYears.SelectedItem.Value.Trim(), mHead, ChkSummary.Checked, CompanyHead, InsCompanyHeader);

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("There is no existing information for this report");
                }

                //DataDynamics.ActiveReports.ActiveReport rpt = new AutoGuardPremiumWritten(txtBegDate.Text,TxtEndDate.Text,"Premium written & Cancellations",ChkSummary.Checked);

                //rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                rpt.DataSource = dt;
                rpt.DataMember = "Report";

                rpt.Document.Printer.PrinterName = "";

                rpt.Run(false);

                Session.Add("Report", rpt);
                Session.Add("FromPage", "PoliciesReports.aspx");
                Response.Redirect("ActiveXViewer.aspx", false);
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }

        private void AnualPolicyProduction()
        {
            try
            {
                EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
                DataTable dt = null;
                DataDynamics.ActiveReports.ActiveReport3 rpt = null;

                string dateType = ddlDateType.SelectedItem.Value.Trim();
                string mHead = "";
                string CompanyHead = "";
                string InsCompanyHeader = "Insurance Company: ";

                if (ddlInsuranceCompany.SelectedItem.Value.Trim() != "")
                    InsCompanyHeader = InsCompanyHeader + ddlInsuranceCompany.SelectedItem.Text.Trim();
                else
                    InsCompanyHeader = InsCompanyHeader + "All";

                if (ddlDateType.SelectedItem.Value.Trim() == "E")
                    mHead = ddlPolicyClass.SelectedItem.Text.Trim() + " Year: " + ddlYears.SelectedItem.Value.Trim() + " - " + ddlQuarter.SelectedItem.Text.Trim() + " - Entry Date Criteria";
                else
                    mHead = ddlPolicyClass.SelectedItem.Text.Trim() + " Year: " + ddlYears.SelectedItem.Value.Trim() + " - " + ddlQuarter.SelectedItem.Text.Trim() + " - Entry Date Criteria";

                dt = appAutoreport.AnualPolicyProduction(ddlYears.SelectedItem.Value.Trim(), ddlPolicyClass.SelectedItem.Value.Trim(), ddlQuarter.SelectedItem.Value.Trim(), ddlDateType.SelectedItem.Value.Trim(), ddlInsuranceCompany.SelectedItem.Value.Trim());
                rpt = new EPolicy2.Reports.AutoGuard.AnualPolicyProduction(mHead, ddlYears.SelectedItem.Value.Trim(), mHead, ChkSummary.Checked, CompanyHead, InsCompanyHeader);

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("There is no existing information for this report");
                }

                //DataDynamics.ActiveReports.ActiveReport rpt = new AutoGuardPremiumWritten(txtBegDate.Text,TxtEndDate.Text,"Premium written & Cancellations",ChkSummary.Checked);
                //rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                rpt.DataSource = dt;
                rpt.DataMember = "Report";

                rpt.Document.Printer.PrinterName = "";

                rpt.Run(false);

                Session.Add("Report", rpt);
                Session.Add("FromPage", "PoliciesReports.aspx");
                Response.Redirect("ActiveXViewer.aspx", false);
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }

        private void History(int taskControlID, int userID)
        {
            Audit.History history = new Audit.History();

            history.Actions = "PRINT";
            history.KeyID = taskControlID.ToString();
            history.Subject = "AUTOGUARDSERVICESCONTRACT";
            history.BuildNotesForHistory("TaskControlID.", "", taskControlID.ToString(), 7);  //7 = mode NOTICEOFCANC
            history.UsersID = userID;
            history.GetSaveHistory();
        }

        protected void rblAutoGuardReports_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            EnableControl();
        }

        
        private void EnableControl()
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;

            try
            {
                #region Switch Report
                switch (rblAutoGuardReports.SelectedItem.Value)
                {
                    case "0":			//Account Current Statement
                        ddlPolicyClass.Visible = false;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ChkSummary.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        chkPartial.Visible = false;
                        LblDataType.Visible = true;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;

                        ddlDateType.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = false;
                        lblPolicyClass.Visible = false;
                        //lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = false;

                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;

                        ddlDateType.SelectedIndex = 1;
                        ddlDateType.Enabled = false;

                        lblAgent.Visible = true;
                        ddlAgent.Visible = true;
						
						_userID = cp.Identity.Name.Split("|".ToCharArray())[1];
                        DataTable dt = EPolicy.Customer.Customer.GetAgentByUserID(_userID);

                        if (dt.Rows.Count > 0)
                        {
                            ddlAgent.SelectedItem.Text = dt.Rows[0]["AgentDesc"].ToString().Trim();
                            ddlAgent.SelectedItem.Value = dt.Rows[0]["AgentID"].ToString().Trim();
                            ddlAgent.Enabled = false;
                        }
                        else
                        {
                            ddlAgent.Enabled = true;
                        }
						
						
                        lblPolicyType.Visible = true;
                        ddlPolicyType.Visible = true;

                        ddlPolicyType.Enabled = true;
                        txtBegDate.Enabled = true;
                        TxtEndDate.Enabled = true;
                        lblDateFrom.Enabled = true;
                        //ddlAgent.Enabled = true;

                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        rblPremWrittenOrder.Visible = false;

                        //lblFollowUpCancellation.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;

                        lblPaidEntry.Visible = false;

                        //rblCertificatePaidOrder.Visible = true;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";
                        break;

                    case "1":           //Policy Premium Written
                        ddlPolicyClass.Visible = false;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ChkSummary.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        chkPartial.Visible = false;
                        LblDataType.Visible = true;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;

                        ddlDateType.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = false;
                        lblPolicyClass.Visible = false;
                        //lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = false;

                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;


                        lblAgent.Visible = true;
                        ddlAgent.Visible = true;
						
						 _userID = cp.Identity.Name.Split("|".ToCharArray())[1];
                        DataTable dt2 = EPolicy.Customer.Customer.GetAgentByUserID(_userID);

                        if (dt2.Rows.Count > 0)
                        {
                            ddlAgent.SelectedItem.Text = dt2.Rows[0]["AgentDesc"].ToString().Trim();
                            ddlAgent.SelectedItem.Value = dt2.Rows[0]["AgentID"].ToString().Trim();
                            ddlAgent.Enabled = false;
                        }
                        else
                        {
                            ddlAgent.Enabled = true;
                        }
						
                        lblPolicyType.Visible = true;
                        ddlPolicyType.Visible = true;

                        ddlPolicyType.Enabled = true;
                        ddlDateType.Enabled = true;
                        txtBegDate.Enabled = true;
                        TxtEndDate.Enabled = true;
                        lblDateFrom.Enabled = true;
                        //ddlAgent.Enabled = true;

                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        rblPremWrittenOrder.Visible = false;

                        //lblFollowUpCancellation.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;

                        lblPaidEntry.Visible = false;

                        //rblCertificatePaidOrder.Visible = true;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";
                        break;

                    case "2":			//Renewals Report
                        ddlPolicyClass.Visible = false;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ChkSummary.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        chkPartial.Visible = false;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;

                        LblDataType.Visible = false;
                        ddlDateType.Visible = false;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = false;
                        lblPolicyClass.Visible = false;
                        //lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = false;

                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;

                        lblAgent.Visible = true;
                        ddlAgent.Visible = true;
						
						 _userID = cp.Identity.Name.Split("|".ToCharArray())[1];
                        DataTable dt3 = EPolicy.Customer.Customer.GetAgentByUserID(_userID);

                        if (dt3.Rows.Count > 0)
                        {
                            ddlAgent.SelectedItem.Text = dt3.Rows[0]["AgentDesc"].ToString().Trim();
                            ddlAgent.SelectedItem.Value = dt3.Rows[0]["AgentID"].ToString().Trim();
                            ddlAgent.Enabled = false;
                        }
                        else
                        {
                            ddlAgent.Enabled = true;
                        }
						
                        lblPolicyType.Visible = true;
                        ddlPolicyType.Visible = true;

                        ddlPolicyType.Enabled = true;
                        ddlDateType.Enabled = true;
                        txtBegDate.Enabled = true;
                        TxtEndDate.Enabled = true;
                        lblDateFrom.Enabled = true;
                        //ddlAgent.Enabled = true;

                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        rblPremWrittenOrder.Visible = false;

                        //lblFollowUpCancellation.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;

                        lblPaidEntry.Visible = false;

                        //rblCertificatePaidOrder.Visible = true;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";
                        break;
                    case "3":			// Quotes vs Policies Issued
                        ddlPolicyClass.Visible = false;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ChkSummary.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        chkPartial.Visible = false;
                        LblDataType.Visible = true;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;

                        ddlDateType.SelectedIndex = 1;
                        ddlDateType.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = false;
                        lblPolicyClass.Visible = false;
                        //lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = false;

                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;



                        lblAgent.Visible = true;
                        ddlAgent.Visible = true;
                        lblPolicyType.Visible = true;
                        ddlPolicyType.Visible = true;

                        ddlPolicyType.Enabled = true;
                        ddlDateType.Enabled = false;
                        txtBegDate.Enabled = true;
                        TxtEndDate.Enabled = true;
                        lblDateFrom.Enabled = true;

                        DataTable dt4 = EPolicy.Customer.Customer.GetAgentByUserID(_userID);

                        if (dt4.Rows.Count > 0)
                        {
                            ddlAgent.SelectedItem.Text = dt4.Rows[0]["AgentDesc"].ToString().Trim();
                            ddlAgent.SelectedItem.Value = dt4.Rows[0]["AgentID"].ToString().Trim();
                            ddlAgent.Enabled = false;
                        }
                        else
                        {
                            ddlAgent.Enabled = true;
                        }

                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        rblPremWrittenOrder.Visible = false;

                        //lblFollowUpCancellation.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;

                        lblPaidEntry.Visible = false;

                        //rblCertificatePaidOrder.Visible = true;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";
                        break;

                    case "4":           //Guardian Payments
                        ddlPolicyClass.Visible = false;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ChkSummary.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        chkPartial.Visible = false;
                        LblDataType.Visible = true;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;

                        //ddlDateType.SelectedItem.Text = "Entry Date";
                        ddlDateType.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = false;
                        lblPolicyClass.Visible = false;
                        //lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = false;

                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;



                        lblAgent.Visible = true;
                        ddlAgent.Visible = true;
                        lblPolicyType.Visible = true;
                        ddlPolicyType.Visible = true;

                        ddlPolicyType.Enabled = true;
                        ddlDateType.Enabled = true;
                        txtBegDate.Enabled = true;
                        TxtEndDate.Enabled = true;
                        lblDateFrom.Enabled = true;
                        ddlAgent.Enabled = true;

                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        rblPremWrittenOrder.Visible = false;

                        //lblFollowUpCancellation.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;

                        lblPaidEntry.Visible = false;

                        //rblCertificatePaidOrder.Visible = true;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";
                        break;

                    case "5":			//PRINT ID Cards
                        ddlPolicyClass.Visible = false;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ChkSummary.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        chkPartial.Visible = false;
                        LblDataType.Visible = true;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;

                        //ddlDateType.SelectedItem.Text = "Entry Date";
                        ddlDateType.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = false;
                        lblPolicyClass.Visible = false;
                        //lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = false;

                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;



                        lblAgent.Visible = true;
                        ddlAgent.Visible = true;
                        lblPolicyType.Visible = true;
                        ddlPolicyType.Visible = true;

                        ddlPolicyType.Enabled = true;
                        ddlDateType.Enabled = false;
                        txtBegDate.Enabled = true;
                        TxtEndDate.Enabled = true;
                        lblDateFrom.Enabled = true;
                        ddlAgent.Enabled = true;

                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        rblPremWrittenOrder.Visible = false;

                        //lblFollowUpCancellation.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;

                        lblPaidEntry.Visible = false;

                        //rblCertificatePaidOrder.Visible = true;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";
                        break;

                    case "6":			//VSCBreakdown
                        //lblPolicyClass.Visible = false;
                        //ddlPolicyClass.Visible = false;
                        //rblPremWrittenOrder.SelectedIndex = 0;
                        //lblMonth.Visible = false;
                        //ddlMonths.Visible = false;
                        //lblYear.Visible = false;
                        //ddlYears.Visible = false;
                        //ddlQuarter.Visible = false;
                        //lblQuarter.Visible = false;
                        //txtBegDate.Visible = true;
                        //TxtEndDate.Visible = true;
                        //lblDateFrom.Visible = true;
                        //lblToDate.Visible = true;
                        //lblInsuranceCompany.Visible = true;
                        //ddlInsuranceCompany.Visible = true;
                        //LblDataType.Visible = true;
                        //ddlDateType.Visible = true;
                        ////btnCalendar.Visible = true;
                        ////btnCalendar2.Visible = true;
                        //lblCompanyDealer.Visible = true;
                        //lblCompanyDealer.Text = "CompanyDealer";
                        //ddlDealer.Visible = true;
                        //ddlBank.Visible = false;
                        //ChkSummary.Visible = true;
                        //ChkAverages.Visible = true;
                        //chkVscFile.Visible = true;
                        //btnPrint.Visible = true;
                        //BtnPrint2.Visible = false;
                        //btnDownLoad.Visible = false;
                        //lblOutstanding.Visible = false;
                        //txtOutstandingDate.Visible = false;
                        //btnCalendar3.Visible = false;
                        //txtFollowUpCancellation.Visible = false;
                        //btnCalendar4.Visible = false;
                        //lblPaidEntry.Visible = false;
                        //ddlAgent.Visible = false;
                        //lblAgent.Visible = false;
                        //ddlVSCPending.Visible = false;
                        //lblPending.Visible = false;
                        //ddlPolicyType.Visible = false;
                        //lblPolicyType.Visible = false;
                        //rblPremWrittenOrder.Visible = false;
                        //ddlFilter.Visible = false;
                        //lblFilter.Visible = false;
                        //chkPartial.Visible = false;
                        //ddlCancellationMethod.Visible = true;
                        //lblCancellationMethod.Visible = true;

                        //btnPrint.Text = "PRINT";

                        //break;
                        ddlPolicyClass.Visible = false;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ChkSummary.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        chkPartial.Visible = false;
                        LblDataType.Visible = true;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;

                        ddlDateType.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = false;
                        lblPolicyClass.Visible = false;
                        //lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = false;

                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;


                        lblAgent.Visible = true;
                        ddlAgent.Visible = true;
                        ddlAgent.Enabled = true;
						
                        // _userID = cp.Identity.Name.Split("|".ToCharArray())[1];
                        //DataTable dt2 = EPolicy.Customer.Customer.GetAgentByUserID(_userID);

                        //if (dt2.Rows.Count > 0)
                        //{
                        //    ddlAgent.SelectedItem.Text = dt2.Rows[0]["AgentDesc"].ToString().Trim();
                        //    ddlAgent.SelectedItem.Value = dt2.Rows[0]["AgentID"].ToString().Trim();
                        //    ddlAgent.Enabled = false;
                        //}
                        //else
                        //{
                        //    ddlAgent.Enabled = true;
                        //}
						
                        lblPolicyType.Visible = true;
                        ddlPolicyType.Visible = true;

                        ddlPolicyType.Enabled = true;
                        ddlDateType.Enabled = true;
                        txtBegDate.Enabled = true;
                        TxtEndDate.Enabled = true;
                        lblDateFrom.Enabled = true;
                        //ddlAgent.Enabled = true;

                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        rblPremWrittenOrder.Visible = false;

                        //lblFollowUpCancellation.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;

                        lblPaidEntry.Visible = false;

                        //rblCertificatePaidOrder.Visible = true;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";
                        break;


                    case "7":			//ProductionReport
                        lblPolicyClass.Visible = false;
                        ddlPolicyClass.Visible = false;
                        rblPremWrittenOrder.SelectedIndex = 0;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = true;
                        ddlInsuranceCompany.Visible = true;
                        LblDataType.Visible = true;
                        ddlDateType.Visible = true;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = true;
                        lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = true;
                        ddlBank.Visible = false;
                        ChkSummary.Visible = true;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;
                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        rblPremWrittenOrder.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";

                        break;

                    case "8":			//VSCSunGuard
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;
                        lblToDate.Visible = true;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        LblDataType.Visible = false;
                        ddlDateType.Visible = false;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = false;
                        ddlDealer.Visible = false;
                        ChkSummary.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        btnPrint.Visible = false;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = true;

                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        lblPolicyClass.Visible = false;
                        ddlPolicyClass.Visible = false;
                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        rblPremWrittenOrder.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";

                        break;

                    case "9":			//VSCSunGuard
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        LblDataType.Visible = false;
                        ddlDateType.Visible = false;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = false;
                        ddlDealer.Visible = false;
                        ChkSummary.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        btnPrint.Visible = false;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = true;

                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        lblPolicyClass.Visible = false;
                        ddlPolicyClass.Visible = false;
                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        rblPremWrittenOrder.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";

                        break;

                    case "10":			//PremiumCancellation
                        lblPolicyClass.Visible = true;
                        ddlPolicyClass.Visible = true;
                        rblPremWrittenOrder.SelectedIndex = 0;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        LblDataType.Visible = true;
                        ddlDateType.Visible = true;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = true;
                        lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = true;
                        ddlBank.Visible = false;
                        ChkSummary.Visible = true;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;
                        ddlAgent.Visible = true;
                        lblAgent.Visible = true;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;

                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        rblPremWrittenOrder.Visible = false;

                        //lblFollowUpCancellation.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;

                        lblPaidEntry.Visible = false;
                        chkPartial.Visible = false;

                        //rblCertificatePaidOrder.Visible = false;

                        ddlCancellationMethod.Visible = true;
                        lblCancellationMethod.Visible = true;

                        btnPrint.Text = "PRINT";

                        break;

                    case "11":			//ETCHBreakdown
                        lblPolicyClass.Visible = false;
                        ddlPolicyClass.Visible = false;
                        rblPremWrittenOrder.SelectedIndex = 0;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = true;
                        ddlInsuranceCompany.Visible = true;
                        LblDataType.Visible = true;
                        ddlDateType.Visible = true;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = true;
                        lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = true;
                        ddlBank.Visible = false;
                        ChkSummary.Visible = true;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = true;
                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;
                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        rblPremWrittenOrder.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";

                        break;

                    case "12":			//VSC Imaging Data File
                        lblPolicyClass.Visible = false;
                        ddlPolicyClass.Visible = false;
                        rblPremWrittenOrder.SelectedIndex = 0;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        LblDataType.Visible = false;
                        ddlDateType.Visible = false;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = false;
                        lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = false;
                        ddlBank.Visible = false;
                        ChkSummary.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;
                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        rblPremWrittenOrder.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";

                        break;

                    case "13":			//VSC Contract Control
                        lblPolicyClass.Visible = false;
                        ddlPolicyClass.Visible = false;
                        rblPremWrittenOrder.SelectedIndex = 0;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        txtBegDate.Visible = false;
                        TxtEndDate.Visible = false;
                        lblDateFrom.Visible = false;
                        lblToDate.Visible = false;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        LblDataType.Visible = false;
                        ddlDateType.Visible = false;
                        //btnCalendar.Visible = false;
                        //btnCalendar2.Visible = false;
                        lblCompanyDealer.Visible = true;
                        lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = true;
                        ddlBank.Visible = false;
                        ChkSummary.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;
                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        rblPremWrittenOrder.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";

                        break;

                    case "14":  //Interface OPP
                        lblPolicyClass.Visible = true;
                        ddlPolicyClass.Visible = true;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        LblDataType.Visible = false;
                        ddlDateType.Visible = false;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = false;
                        lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = false;
                        ChkSummary.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;

                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        rblPremWrittenOrder.Visible = false;

                        //lblFollowUpCancellation.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "Process";

                        break;

                    case "15":			//QC-Breakdown
                        lblPolicyClass.Visible = false;
                        ddlPolicyClass.Visible = false;
                        rblPremWrittenOrder.SelectedIndex = 0;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = true;
                        ddlInsuranceCompany.Visible = true;
                        LblDataType.Visible = true;
                        ddlDateType.Visible = true;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = true;
                        lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = true;
                        ddlBank.Visible = false;
                        ChkSummary.Visible = true;
                        ChkAverages.Visible = true;
                        chkVscFile.Visible = true;
                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;
                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        rblPremWrittenOrder.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = true;
                        lblCancellationMethod.Visible = true;

                        btnPrint.Text = "PRINT";

                        break;

                    case "16":			//ProductionReport --> QCertified
                        lblPolicyClass.Visible = false;
                        ddlPolicyClass.Visible = false;
                        rblPremWrittenOrder.SelectedIndex = 0;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = true;
                        ddlInsuranceCompany.Visible = true;
                        LblDataType.Visible = true;
                        ddlDateType.Visible = true;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = true;
                        lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = true;
                        ddlBank.Visible = false;
                        ChkSummary.Visible = true;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;
                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        rblPremWrittenOrder.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";

                        break;

                    case "17":			//QCertified Contract Control
                        lblPolicyClass.Visible = false;
                        ddlPolicyClass.Visible = false;
                        rblPremWrittenOrder.SelectedIndex = 0;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        txtBegDate.Visible = false;
                        TxtEndDate.Visible = false;
                        lblDateFrom.Visible = false;
                        lblToDate.Visible = false;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        LblDataType.Visible = false;
                        ddlDateType.Visible = false;
                        //btnCalendar.Visible = false;
                        //btnCalendar2.Visible = false;
                        lblCompanyDealer.Visible = true;
                        lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = true;
                        ddlBank.Visible = false;
                        ChkSummary.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;
                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = true;
                        lblPending.Visible = true;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        rblPremWrittenOrder.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";

                        break;

                    case "18":			//QCert-ProductionFile
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        txtBegDate.Visible = false;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = false;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        LblDataType.Visible = false;
                        ddlDateType.Visible = false;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = false;
                        lblCompanyDealer.Visible = false;
                        ddlDealer.Visible = false;
                        ChkSummary.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        btnPrint.Visible = false;
                        BtnPrint2.Visible = true;
                        btnDownLoad.Visible = true;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;

                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        lblPolicyClass.Visible = false;
                        ddlPolicyClass.Visible = false;
                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";

                        break;

                    case "19":			//P.Prot-Breakdown
                        lblPolicyClass.Visible = false;
                        ddlPolicyClass.Visible = false;
                        rblPremWrittenOrder.SelectedIndex = 0;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = true;
                        ddlInsuranceCompany.Visible = true;
                        LblDataType.Visible = true;
                        ddlDateType.Visible = true;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = true;
                        lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = true;
                        ddlBank.Visible = false;
                        ChkSummary.Visible = true;
                        ChkAverages.Visible = true;
                        chkVscFile.Visible = true;
                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;
                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        rblPremWrittenOrder.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = true;
                        lblCancellationMethod.Visible = true;

                        btnPrint.Text = "PRINT";

                        break;

                    case "20":			//Etch Bill
                        lblPolicyClass.Visible = false;
                        ddlPolicyClass.Visible = false;
                        rblPremWrittenOrder.SelectedIndex = 0;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        LblDataType.Visible = true;
                        ddlDateType.Visible = true;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = true;
                        lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = true;
                        ddlBank.Visible = false;
                        ChkSummary.Visible = true;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = true;
                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;
                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        rblPremWrittenOrder.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";

                        break;

                    case "21":			//Accounting File
                        lblPolicyClass.Visible = true;
                        ddlPolicyClass.Visible = true;
                        rblPremWrittenOrder.SelectedIndex = 0;
                        lblMonth.Visible = false;
                        ddlMonths.Visible = false;
                        lblYear.Visible = false;
                        ddlYears.Visible = false;
                        ddlQuarter.Visible = false;
                        lblQuarter.Visible = false;
                        txtBegDate.Visible = true;
                        TxtEndDate.Visible = true;
                        lblDateFrom.Visible = true;
                        lblToDate.Visible = true;
                        lblInsuranceCompany.Visible = false;
                        ddlInsuranceCompany.Visible = false;
                        LblDataType.Visible = true;
                        ddlDateType.Visible = true;
                        //btnCalendar.Visible = true;
                        //btnCalendar2.Visible = true;
                        lblCompanyDealer.Visible = false;
                        lblCompanyDealer.Text = "CompanyDealer";
                        ddlDealer.Visible = false;
                        ddlBank.Visible = false;
                        ChkSummary.Visible = false;
                        ChkAverages.Visible = false;
                        chkVscFile.Visible = false;
                        btnPrint.Visible = true;
                        BtnPrint2.Visible = false;
                        btnDownLoad.Visible = false;
                        lblOutstanding.Visible = false;
                        txtOutstandingDate.Visible = false;
                        btnCalendar3.Visible = false;
                        txtFollowUpCancellation.Visible = false;
                        btnCalendar4.Visible = false;
                        lblPaidEntry.Visible = false;
                        ddlAgent.Visible = false;
                        lblAgent.Visible = false;
                        ddlVSCPending.Visible = false;
                        lblPending.Visible = false;
                        ddlPolicyType.Visible = false;
                        lblPolicyType.Visible = false;
                        rblPremWrittenOrder.Visible = false;
                        ddlFilter.Visible = false;
                        lblFilter.Visible = false;
                        chkPartial.Visible = false;
                        ddlCancellationMethod.Visible = false;
                        lblCancellationMethod.Visible = false;

                        btnPrint.Text = "PRINT";

                        break;
                }
                #endregion
            }
            catch(Exception exp)
            { 

            }
        }

        protected void rblPremWrittenOrder_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (rblPremWrittenOrder.SelectedItem.Value)
            {
                case "0":
                    ddlDealer.Visible = true;
                    ddlBank.Visible = false;
                    lblCompanyDealer.Text = "CompanyDealer";
                    lblInsuranceCompany.Visible = false;
                    ddlInsuranceCompany.Visible = false;
                    break;

                case "1":
                    ddlDealer.Visible = false;
                    ddlBank.Visible = true;
                    lblCompanyDealer.Text = "Bank";
                    lblInsuranceCompany.Visible = false;
                    ddlInsuranceCompany.Visible = false;
                    break;

                case "2":
                    ddlDealer.Visible = true;
                    ddlBank.Visible = false;
                    lblCompanyDealer.Text = "CompanyDealer";
                    lblInsuranceCompany.Visible = true;
                    ddlInsuranceCompany.Visible = true;
                    break;


            }
        }

        protected void ddlCompanyType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;

                DataTable dtPolicyType = LookupTables.LookupTables.GetTable("PolicyType");
                //Policy Type
                ddlPolicyType.DataSource = dtPolicyType;
                ddlPolicyType.DataTextField = "PolicyTypeDesc";
                ddlPolicyType.DataValueField = "PolicyTypeID";
                ddlPolicyType.DataBind();
                ddlPolicyType.SelectedIndex = -1;
                ddlPolicyType.Items.Insert(0, "");

                if (ddlCompanyType.SelectedItem.Text.Trim() == "Auto VI")
                {
                    string[] itemsToDisable = { "6" };
                    foreach (string item in itemsToDisable)
                    {
                        var listItem = rblAutoGuardReports.Items.FindByValue(item);
                        if (listItem != null)
                            listItem.Enabled = false;
                    }

                    ddlPolicyType.Enabled = true;
                    DataTable dtAgentVi = LookupTables.LookupTables.GetTable("AgentVi");
                    //Agent VI
                    ddlAgent.DataSource = dtAgentVi;
                    ddlAgent.DataTextField = "AgentDesc";
                    ddlAgent.DataValueField = "AgentID";
                    ddlAgent.DataBind();
                    ddlAgent.SelectedIndex = -1;
                    ddlAgent.Items.Insert(0, "");

                    if (ddlPolicyType.Items.Count > 3)
                    {
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("XPA")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("XCA")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("GPR")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("GVI")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("HOM")));
						ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("INC")));
						ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("BND")));
                    }

                    if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("GUARDIAN CENTRAL OFFICE") || cp.IsInRole("AUTO VI ADMINISTRATOR"))
                    {

                        if (rblAutoGuardReports.Items.Count > 4)
                        {
                            rblAutoGuardReports.Items.FindByValue("0").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("1").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("2").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("3").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("4").Enabled = false;
                            rblAutoGuardReports.Items.FindByValue("5").Enabled = false;

                            rblAutoGuardReports.SelectedIndex = 0;
                        }
                    }

                }
                else if (ddlCompanyType.SelectedItem.Text.Trim() == "GuardianXtra")
                {
                    string[] itemsToDisable = { "6" };
                    foreach (string item in itemsToDisable)
                    {
                        var listItem = rblAutoGuardReports.Items.FindByValue(item);
                        if (listItem != null)
                            listItem.Enabled = false;
                    }
                    ddlPolicyType.Enabled = true;
                    DataTable dtAgent = LookupTables.LookupTables.GetTable("Agent");

                    //Agent PR
                    ddlAgent.DataSource = dtAgent;
                    ddlAgent.DataTextField = "AgentDesc";
                    ddlAgent.DataValueField = "AgentID";
                    ddlAgent.DataBind();
                    ddlAgent.SelectedIndex = -1;
                    ddlAgent.Items.Insert(0, "");

                    if (ddlPolicyType.Items.Count > 3)
                    {
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("PAP")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("BAP")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("GPR")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("GVI")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("HOM")));
						ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("INC")));
						ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("BND")));
                    }


                    if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("GUARDIAN CENTRAL OFFICE") || cp.IsInRole("AUTO VI ADMINISTRATOR"))
                    {
                        if (rblAutoGuardReports.Items.Count > 3)// < 5
                        {

                            rblAutoGuardReports.Items.FindByValue("0").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("1").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("2").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("3").Enabled = false;
                            rblAutoGuardReports.Items.FindByValue("4").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("5").Enabled = false;

                            rblAutoGuardReports.SelectedIndex = 0;
                            //rblAutoGuardReports.Items.Insert(4 , "Guardian Payments");
                        }
                    }
                }
                else if (ddlCompanyType.SelectedItem.Text.Trim() == "Double Interest")
                {
                    string[] itemsToDisable = { "6" };
                    foreach (string item in itemsToDisable)
                    {
                        var listItem = rblAutoGuardReports.Items.FindByValue(item);
                        if (listItem != null)
                            listItem.Enabled = false;
                    }
                    ddlPolicyType.Enabled = true;
                    DataTable dtAgentVi = LookupTables.LookupTables.GetTable("AgentVi");
                    //Agent VI
                    ddlAgent.DataSource = dtAgentVi;
                    ddlAgent.DataTextField = "AgentDesc";
                    ddlAgent.DataValueField = "AgentID";
                    ddlAgent.DataBind();
                    ddlAgent.SelectedIndex = -1;
                    ddlAgent.Items.Insert(0, "");

                    if (ddlPolicyType.Items.Count > 3)
                    {
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("XPA")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("XCA")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("GPR")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("GVI")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("HOM")));
						ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("INC")));
						ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("BND")));
						ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("BAP")));
                    }

                    if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("GUARDIAN CENTRAL OFFICE") || cp.IsInRole("AUTO VI ADMINISTRATOR"))
                    {

                        if (rblAutoGuardReports.Items.Count > 4)
                        {
                            rblAutoGuardReports.Items.FindByValue("0").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("1").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("2").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("3").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("4").Enabled = false;
                            rblAutoGuardReports.Items.FindByValue("5").Enabled = true;
                            //rblAutoGuardReports.SelectedIndex = 5;
                            ddlDateType.SelectedIndex = 0;
                            EnableControl();
                        }
                    }
                }
                else if (ddlCompanyType.SelectedItem.Text.Trim() == "Home Owner")
                {
                    string[] itemsToDisable = { "6" };
                    foreach (string item in itemsToDisable)
                    {
                        var listItem = rblAutoGuardReports.Items.FindByValue(item);
                        if (listItem != null)
                            listItem.Enabled = false;
                    }
                    ddlPolicyType.Enabled = true;
                    DataTable dtAgentVi = LookupTables.LookupTables.GetTable("AgentVi");
                    //Agent VI
                    ddlAgent.DataSource = dtAgentVi;
                    ddlAgent.DataTextField = "AgentDesc";
                    ddlAgent.DataValueField = "AgentID";
                    ddlAgent.DataBind();
                    ddlAgent.SelectedIndex = -1;
                    ddlAgent.Items.Insert(0, "");

                    if (ddlPolicyType.Items.Count > 3)
                    {
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("PAP")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("BAP")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("XPA")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("XCA")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("GPR")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("GVI")));
						ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("BND")));
                    }

                    if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("GUARDIAN CENTRAL OFFICE") || cp.IsInRole("AUTO VI ADMINISTRATOR"))
                    {

                        if (rblAutoGuardReports.Items.Count > 4)
                        {
                            rblAutoGuardReports.Items.FindByValue("0").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("1").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("2").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("3").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("4").Enabled = false;
                            rblAutoGuardReports.Items.FindByValue("5").Enabled = false;

                            rblAutoGuardReports.SelectedIndex = 0;
                        }
                    }
                }

                else if (ddlCompanyType.SelectedItem.Text.Trim() == "Road Assist")
                {
                    string[] itemsToDisable = { "6" };
                    foreach (string item in itemsToDisable)
                    {
                        var listItem = rblAutoGuardReports.Items.FindByValue(item);
                        if (listItem != null)
                            listItem.Enabled = false;
                    }
                    ddlPolicyType.Enabled = true;
                    DataTable dtAgentVi = LookupTables.LookupTables.GetTable("AgentVi");
                    //Agent VI
                    ddlAgent.DataSource = dtAgentVi;
                    ddlAgent.DataTextField = "AgentDesc";
                    ddlAgent.DataValueField = "AgentID";
                    ddlAgent.DataBind();
                    ddlAgent.SelectedIndex = -1;
                    ddlAgent.Items.Insert(0, "");

                    if (ddlPolicyType.Items.Count > 3)
                    {
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("PAP")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("BAP")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("XPA")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("XCA")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("HOM")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("INC")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("MAR")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("BND")));
                    }

                    if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("GUARDIAN CENTRAL OFFICE") || cp.IsInRole("AUTO VI ADMINISTRATOR"))
                    {

                        if (rblAutoGuardReports.Items.Count > 4)
                        {
                            rblAutoGuardReports.Items.FindByValue("0").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("1").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("2").Enabled = false;
                            rblAutoGuardReports.Items.FindByValue("3").Enabled = false;
                            rblAutoGuardReports.Items.FindByValue("4").Enabled = false;
                            rblAutoGuardReports.Items.FindByValue("5").Enabled = false;

                            rblAutoGuardReports.SelectedIndex = 0;
                        }
                    }
                }

                else if (ddlCompanyType.SelectedItem.Text.Trim() == "Auto Personal")
                {
                    string[] itemsToDisable = { "6" };
                    foreach (string item in itemsToDisable)
                    {
                        var listItem = rblAutoGuardReports.Items.FindByValue(item);
                        if (listItem != null)
                            listItem.Enabled = false;
                    }
                    ddlPolicyType.Enabled = true;
                    DataTable dtAgentVi = LookupTables.LookupTables.GetTable("AgentVi");
                    //Agent VI
                    ddlAgent.DataSource = dtAgentVi;
                    ddlAgent.DataTextField = "AgentDesc";
                    ddlAgent.DataValueField = "AgentID";
                    ddlAgent.DataBind();
                    ddlAgent.SelectedIndex = -1;
                    ddlAgent.Items.Insert(0, "");

                    if (ddlPolicyType.Items.Count > 3)
                    {
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("GPR")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("GVI")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("XPA")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("XCA")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("HOM")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("INC")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("MAR")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("BND")));
                    }

                    if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("GUARDIAN CENTRAL OFFICE") || cp.IsInRole("AUTO VI ADMINISTRATOR"))
                    {

                        if (rblAutoGuardReports.Items.Count > 4)
                        {
                            rblAutoGuardReports.Items.FindByValue("0").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("1").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("2").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("3").Enabled = false;
                            rblAutoGuardReports.Items.FindByValue("4").Enabled = false;
                            rblAutoGuardReports.Items.FindByValue("5").Enabled = false;
                            rblAutoGuardReports.Items.FindByValue("6").Enabled = false;
                            rblAutoGuardReports.SelectedIndex = 0;
                        }
                    }
                }

                else if (ddlCompanyType.SelectedItem.Text.Trim() == "Yacht")
                {
                    string[] itemsToEnable = { "6" };
                    foreach (string item in itemsToEnable)
                    {
                        var listItem = rblAutoGuardReports.Items.FindByValue(item);
                        if (listItem != null)
                            listItem.Enabled = true;
                    }
                    DataTable dtAgent = LookupTables.LookupTables.GetTable("AgencyYacht");

                    //Agent Yacht
                    ddlAgent.DataSource = dtAgent;
                    ddlAgent.DataTextField = "AgentDesc";
                    ddlAgent.DataValueField = "AgentID";
                    ddlAgent.DataBind();
                    ddlAgent.SelectedIndex = -1;
                    ddlAgent.Items.Insert(0, "");

                    ddlPolicyType.SelectedItem.Text = "MAR";
                    ddlPolicyType.Enabled = false;
                }

                else if (ddlCompanyType.SelectedItem.Text.Trim() == "Auto High Limit")
                {
                    string[] itemsToDisable = { "6" };
                    foreach (string item in itemsToDisable)
                    {
                        var listItem = rblAutoGuardReports.Items.FindByValue(item);
                        if (listItem != null)
                            listItem.Enabled = false;
                    }

                    ddlPolicyType.Enabled = true;
                    DataTable dtAgentVi = LookupTables.LookupTables.GetTable("AgentVi");
                    //Agent VI
                    ddlAgent.DataSource = dtAgentVi;
                    ddlAgent.DataTextField = "AgentDesc";
                    ddlAgent.DataValueField = "AgentID";
                    ddlAgent.DataBind();
                    ddlAgent.SelectedIndex = -1;
                    ddlAgent.Items.Insert(0, "");

                    if (ddlPolicyType.Items.Count > 3)
                    {
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("XPA")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("XCA")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("GPR")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("GVI")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("HOM")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("INC")));
                        ddlPolicyType.Items.RemoveAt(ddlPolicyType.Items.IndexOf(ddlPolicyType.Items.FindByText("BND")));
                    }

                    if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("GUARDIAN CENTRAL OFFICE") || cp.IsInRole("AUTO VI ADMINISTRATOR"))
                    {

                        if (rblAutoGuardReports.Items.Count > 4)
                        {
                            rblAutoGuardReports.Items.FindByValue("0").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("1").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("2").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("3").Enabled = true;
                            rblAutoGuardReports.Items.FindByValue("4").Enabled = false;
                            rblAutoGuardReports.Items.FindByValue("5").Enabled = false;

                            rblAutoGuardReports.SelectedIndex = 0;
                        }
                    }
                }
				
				 _userID = cp.Identity.Name.Split("|".ToCharArray())[1];
			    DataTable dtAG = EPolicy.Customer.Customer.GetAgentByUserID(_userID);

                if (dtAG.Rows.Count > 0)
                {
                    ddlAgent.SelectedItem.Text = dtAG.Rows[0]["AgentDesc"].ToString().Trim();
                    ddlAgent.SelectedItem.Value = dtAG.Rows[0]["AgentID"].ToString().Trim();
                    ddlAgent.Enabled = false;
                }
                //else
                //{
                //    ddlAgent.SelectedItem.Text = "";
                //    ddlAgent.Enabled = false;
                //}
            }
            catch (Exception ep)
            {
            }

        }

        protected void rblCertificatePaidOrder_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            switch (rblCertificatePaidOrder.SelectedItem.Value)
            {
                case "0":
                    ddlDealer.Visible = true;
                    ddlBank.Visible = false;
                    lblCompanyDealer.Text = "CompanyDealer";
                    lblInsuranceCompany.Visible = true;
                    ddlInsuranceCompany.Visible = true;
                    break;


                case "1":
                    ddlDealer.Visible = true;
                    ddlBank.Visible = false;
                    lblCompanyDealer.Text = "CompanyDealer";
                    lblInsuranceCompany.Visible = false;
                    ddlInsuranceCompany.Visible = false;
                    break;


                case "2":
                    ddlDealer.Visible = true;
                    ddlBank.Visible = false;
                    lblCompanyDealer.Text = "CompanyDealer";
                    lblInsuranceCompany.Visible = true;
                    ddlInsuranceCompany.Visible = true;
                    break;
            }
        }

        protected void btnPrintOLD_Click(object sender, System.EventArgs e)
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

            switch (rblAutoGuardReports.SelectedItem.Value)
            {
                case "0":
                    AutoGuardPremiumWritten();
                    break;

                case "1":
                    AutoGuardCertificateOutstanding();
                    break;

                case "2":
                    AutoGuardCertificatePaid();
                    break;

                case "3":
                    MonthlyPolicyProduction(); //CancellationNotice();
                    break;

                case "4":
                    AnualPolicyProduction();  //FollowUpCancellation();
                    break;

                case "5":
                    PrintVSCReport();
                    break;

                case "6":
                    VSCTotalPriceBreakdown("1"); //VSC PolicyClassID
                    //CertificatePaidAndEffectivity();
                    break;

                case "7":
                    VSCProductionReport("1"); // VSC PolicyClassID
                    break;

                case "8":
                    VSCSunGuard();
                    break;

                case "10":
                    PremiumCancellation();
                    break;

                case "11":
                    ETCHBreakdown();
                    break;

                case "12":
                    VSCImagingDataFile();
                    break;

                case "13":
                    VSCContractControl(1); //-> VSC (PolicyClassID)
                    break;

                case "14":
                    InterfaceOPP();
                    break;

                case "15":
                    VSCTotalPriceBreakdown("16"); //-> QCertified
                    break;

                case "16":
                    VSCProductionReport("16"); //-> QCertified
                    break;

                case "17":
                    VSCContractControl(16); //-> QCertified (PolicyClassID)
                    break;

                case "19":
                    VSCTotalPriceBreakdown("17"); //-> Paint Protector
                    break;

                case "20":
                    EtchBill("13");
                    break;

                case "21":
                    AccountingFile();
                    break;
            }
        }

        protected void btnPrint_Click(object sender, System.EventArgs e)
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;

                FieldVerify();

                bool IsEffectiveDate = true; ;
                string BegDate = "01/01/1999";
                string EndDate = "01/01/1999";
                string Agent = "000";
                string PolicyType = "";
                string rdlcDoc = "";
                string CompanyPolicy = "";
                string TaskControlTypeID = "";

                if (ddlDateType.SelectedItem.Text == "Effective Date")
                {
                    IsEffectiveDate = true;
                }
                else 
                {
                    IsEffectiveDate = false;
                }
                BegDate = txtBegDate.Text.ToString();
                EndDate = TxtEndDate.Text.ToString();
                Agent = ddlAgent.SelectedItem.Value.Trim();
                PolicyType = ddlPolicyType.SelectedItem.Text;

                if (!cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("GUARDIAN CENTRAL OFFICE") && !cp.IsInRole("AUTO VI ADMINISTRATOR"))
                {
                    if (Agent.Trim() == "")
                    {
                        throw new Exception("This user does not have the required privilages.\n\r Please contact Administration.");
                    }
                }
                if (ddlCompanyType.SelectedItem.Text == "GuardianXtra")
                {
                    CompanyPolicy = "GuardianXtra";
                }
                else if (ddlCompanyType.SelectedItem.Text == "Auto VI")
                {
                    CompanyPolicy = "Auto VI";
                    TaskControlTypeID = "26";
                }
                else if (ddlCompanyType.SelectedItem.Text == "Double Interest")
                {
                    CompanyPolicy = "Double Interest";
                    TaskControlTypeID = "31";
                }
                else if (ddlCompanyType.SelectedItem.Text == "Home Owner")
                {
                    CompanyPolicy = "Home Owner";
                }
                else if (ddlCompanyType.SelectedItem.Text == "Yacht")
                {
                    CompanyPolicy = "Yacht";
                }
                else if (ddlCompanyType.SelectedItem.Text == "Auto Personal")
                {
                    CompanyPolicy = "Auto Personal";
                }
                else if (ddlCompanyType.SelectedItem.Text == "Road Assist")
                {
                    CompanyPolicy = "Road Assist";
                }
                else if (ddlCompanyType.SelectedItem.Text == "Auto High Limit")
                {
                    CompanyPolicy = "Auto High Limit";
                    TaskControlTypeID = "41";
                }

                switch (rblAutoGuardReports.SelectedItem.Text)
                {
                    case "Account Current Statement":
                        if (CompanyPolicy == "Auto VI" || CompanyPolicy == "Double Interest")
                            rdlcDoc = "ReportCurrentStatement.rdlc";
                        else if (CompanyPolicy == "GuardianXtra")
                            rdlcDoc = "ReportCurrentStatementXtra.rdlc";
                        else if (CompanyPolicy == "Home Owner")
                            rdlcDoc = "ReportCurrentStatement.rdlc";
                        else if (CompanyPolicy == "Yacht")
                            rdlcDoc = "ReportCurrentStatementYacht.rdlc";
                        else if (CompanyPolicy == "Auto Personal")
                            rdlcDoc = "ReportCurrentStatementAutoPR.rdlc";
                        else if (CompanyPolicy == "Road Assist")
                            rdlcDoc = "ReportCurrentStatementRoadAssist.rdlc";

                        else if (CompanyPolicy == "Auto High Limit")
                            rdlcDoc = "ReportCurrentStatement.rdlc";
                        
                        break;

                    case "Policy Premium Written":
                        if (CompanyPolicy == "Auto VI" || CompanyPolicy == "Double Interest")
                             rdlcDoc = "ReportPremiumWritten.rdlc";
                        else if (CompanyPolicy == "GuardianXtra")
                            rdlcDoc = "ReportPremiumWrittenXtra.rdlc";
                        else if (CompanyPolicy == "Home Owner")
                            rdlcDoc = "ReportPremiumWritten.rdlc";
                        else if (CompanyPolicy == "Yacht")
                            rdlcDoc = "ReportPremiumWrittenYacht.rdlc";
                        else if (CompanyPolicy == "Auto Personal")
                            rdlcDoc = "ReportPremiumWrittenAutoPR.rdlc";
                        else if (CompanyPolicy == "Road Assist")
                            rdlcDoc = "ReportPremiumWrittenRoadAssist.rdlc";

                        else if (CompanyPolicy == "Auto High Limit")
                            rdlcDoc = "ReportPremiumWritten.rdlc";

                        break;

                    case "Renewals Report":
                        if (CompanyPolicy == "Auto VI" || CompanyPolicy == "Double Interest")
                            rdlcDoc = "ReportRenewals.rdlc";
                        else if (CompanyPolicy == "GuardianXtra")
                            rdlcDoc = "ReportRenewalsXtra.rdlc";
                        else if (CompanyPolicy == "Home Owner")
                            rdlcDoc = "ReportRenewals.rdlc";
                        else if (CompanyPolicy == "Yacht")
                            rdlcDoc = "ReportRenewalsYacht.rdlc";
                        else if (CompanyPolicy == "Auto Personal")
                            rdlcDoc = "ReportRenewalsAutoPR.rdlc";
                        else if (CompanyPolicy == "Road Assist")
                            rdlcDoc = "ReportRenewalsRoadAssist.rdlc";

                        else if (CompanyPolicy == "Auto High Limit")
                            rdlcDoc = "ReportRenewals.rdlc";

                        break;

                    case "Quotes vs  Policies Issued":
                        if (CompanyPolicy == "Auto VI" || CompanyPolicy == "Double Interest")
                            rdlcDoc = "ReportQuotesPoliciesIssued.rdlc";
                        else if (CompanyPolicy == "GuardianXtra")
                            rdlcDoc = "ReportQuotesPoliciesIssuedXtra.rdlc";
                        else if (CompanyPolicy == "Home Owner")
                            rdlcDoc = "ReportQuotesPoliciesIssued.rdlc";
                        else if (CompanyPolicy == "Yacht")
                            rdlcDoc = "ReportQuotesPoliciesIssuedYacht.rdlc";
                        else if (CompanyPolicy == "Auto Personal")
                            rdlcDoc = "ReportQuotesPoliciesIssuedAutoPR.rdlc";
                        else if (CompanyPolicy == "Road Assist")
                            rdlcDoc = "ReportQuotesPoliciesIssuedRoadAssist.rdlc";

                        else if (CompanyPolicy == "Auto High Limit")
                            rdlcDoc = "ReportQuotesPoliciesIssued.rdlc";
                        break;

                    case "Guardian Payments":
                        if (CompanyPolicy == "GuardianXtra")
                            rdlcDoc = "ReportPaidTransactionXtra.rdlc";
                        break;

                    case "Yacht Policies with Pending Fields":
                        if (CompanyPolicy == "Yacht")
                            rdlcDoc = "ReportYachtPoliciesPendingFields.rdlc";
                        break;

                    case "Renewal ID Cards":
                        if (CompanyPolicy == "Double Interest")
                        {
                            rdlcDoc = "IDCardsDI.rdlc";
                            PrintIDCards(BegDate, EndDate, Agent);
                            return;
                        }
                        break;
                }

                string ReportType = rblAutoGuardReports.SelectedItem.Value;

                Session["IsEffectiveDate"] = IsEffectiveDate;
                Session["BegDate"] = BegDate;
                Session["EndDate"] = EndDate;
                Session["Agent"] = Agent;
                Session["PolicyType"] = PolicyType;
                Session["rdlcDoc"] = rdlcDoc;
                Session["ReportType"] = ReportType;
                Session["CompanyPolicy"] = CompanyPolicy;
                Session["TaskControlTypeID"] = TaskControlTypeID;


                string url = "PoliciesReportViewer.aspx";
                string s = "window.open('" + url + "', 'popup_window', 'width=900,height=762,left=10,top=10,resizable=yes');";
                ScriptManager.RegisterStartupScript(this, GetType(), "script", s, true);

                //ReportPrint(IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, rdlcDoc, ReportType);

            }

            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
                //this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                //this.litPopUp.Visible = true;
                return;
            }
        }

        protected void PrintIDCards(string BegDate, string EndDate, string Agent)
        {
            List<string> mergePaths = new List<string>();

            int taskControlID = 0;

            DateTime EffectiveDate = new DateTime(), ExpirationDate = new DateTime();

            DataTable dtIDCards = GetReportAutoIDCards_VI(BegDate, EndDate, Agent);

            if(dtIDCards != null)
            {
                if(dtIDCards.Rows.Count > 0)
                {
                    for (int i = 0; i < dtIDCards.Rows.Count; i++)
                    {
                        EffectiveDate = DateTime.Parse(dtIDCards.Rows[i]["EffectiveDate"].ToString());
                        ExpirationDate = DateTime.Parse(dtIDCards.Rows[i]["ExpirationDate"].ToString());
                        taskControlID = int.Parse(dtIDCards.Rows[i]["TaskControlID"].ToString());
                        mergePaths = ImprimirIDCards(mergePaths, taskControlID, int.Parse(dtIDCards.Rows[i]["VehicleDetailID"].ToString()), EffectiveDate.ToShortDateString(), ExpirationDate.ToShortDateString());
                    }
                }
            }

            string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
            //Generar PDF
            OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
            string FinalFile = "";
            FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControlID.ToString());

            //if (Attachment)
            //    TextBox1.Text = FinalFile;
            //else
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);

        }

        private List<string> ImprimirIDCards(List<string> mergePaths, int taskControl, int VehicleDetailID, string EffectiveDate, string ExpirationDate)
        {
            try
            {
                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

                int s = taskControl;

                GetReportAutoGeneralInfods_VITableAdapters.GetReportAutoGeneralInfo_VITableAdapter ds1 = new GetReportAutoGeneralInfods_VITableAdapters.GetReportAutoGeneralInfo_VITableAdapter();
                GetReportAutoVehicleInfoPolicyTableAdapters.GetReportAutoVehiclesInfoPolicyByVehicle_VITableAdapter ds2 = new GetReportAutoVehicleInfoPolicyTableAdapters.GetReportAutoVehiclesInfoPolicyByVehicle_VITableAdapter();


                //ReportDataSource rds1 = new ReportDataSource();
                ReportDataSource rds1 = new ReportDataSource();
                // ReportDataSource rds3 = new ReportDataSource();
                ReportDataSource rds2 = new ReportDataSource();


                //rds1 = new ReportDataSource("GetReportAutoVehiclesInfo_VI", (DataTable)ds1.GetData(s));
                rds1 = new ReportDataSource("dsAutoGeneralInfo_VI", (DataTable)ds1.GetData(s));
                //rds3 = new ReportDataSource("GetReportAutoDriversInfo_VI", (DataTable)ds3.GetData(s));
                rds2 = new ReportDataSource("dsAutoVehicleInfoPolicy", (DataTable)ds2.GetData(s, VehicleDetailID)); //, VehicleDetailID));


                ReportParameter[] parameters = new ReportParameter[2];

                parameters[0] = new ReportParameter("EffDate", EffectiveDate);
                parameters[1] = new ReportParameter("ExpDate", ExpirationDate);


                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/VI/IDCardsDI.rdlc");
                //viewer1.LocalReport.DataSources.Add(rds1);
                viewer1.LocalReport.DataSources.Add(rds1);
                //viewer1.LocalReport.DataSources.Add(rds3);
                viewer1.LocalReport.DataSources.Add(rds2);
                viewer1.LocalReport.SetParameters(parameters);
                viewer1.LocalReport.Refresh();

                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;

                Guid RandomString = Guid.NewGuid();
                string fileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + VehicleDetailID.ToString().Trim() + RandomString.ToString() + DateTime.Now.Month + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "-IDCardsDI";
                string _FileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + VehicleDetailID.ToString().Trim() + RandomString.ToString() + DateTime.Now.Month + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "-IDCardsDI" + ".pdf";

                if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1))
                    File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);

                byte[] bytes1 = viewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (FileStream fs1 = new FileStream(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1, FileMode.Create))
                {
                    fs1.Write(bytes1, 0, bytes1.Length);
                    fs1.Close();
                }

                mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);
                return mergePaths;
            }
            catch (Exception ecp)
            {
                throw new Exception(ecp.Message.ToString());
            }
        }

        private static DataTable GetReportAutoIDCards_VI(string BegDate, string EndDate, string Agent)
        {

            DataTable dt = new DataTable();

            //Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[3];

                DbRequestXmlCooker.AttachCookItem("BegDate", SqlDbType.VarChar, 20, BegDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("EndDate", SqlDbType.VarChar, 20, EndDate.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("AgentID", SqlDbType.VarChar, 4, Agent.ToString() == "" ? "-99" : Agent.ToString(), ref cookItems);

                DBRequest Executor = new DBRequest();
                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }

                try
                {
                    //Executor.
                    //Executor.BeginTrans();

                    dt = Executor.GetQuery("GetReportAutoIDCards_VI", xmlDoc);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception("No ID Cards.", ex);
                }

                return dt;

            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
        }


        private void AccountingFile()
        {
            EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
            DataTable dt = appAutoreport.VSCTotalPriceBreakdown(txtBegDate.Text, TxtEndDate.Text, ddlDealer.SelectedItem.Value.Trim(), "E", ddlPolicyClass.SelectedItem.Value.Trim(), 1, ddlCancellationMethod.SelectedItem.Value.Trim(), ddlInsuranceCompany.SelectedItem.Value.Trim());

            StreamWriter sr;

            //FileName = @"C:\Inetpub\wwwroot\Optima\VSC\ImagingDataFile\ACC" +
            FileName = @"C:\Inetpub\Optima Page\Optima\VSC\ImagingDataFile\ACC" +
            DateTime.Now.Month.ToString().Trim() +
            DateTime.Now.Day.ToString().Trim() +
            DateTime.Now.Year.ToString().Trim() + ".txt";

             sr = File.CreateText(FileName);

            string[] array2 = new string[43];
            array2 = SetAccountingHeader(array2);

            sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +
            array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +
            array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +
            array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() +
            array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +
            array2[25].ToString() + array2[26].ToString() + array2[27].ToString() +array2[28].ToString() + array2[29].ToString() +
            array2[30].ToString() + array2[31].ToString() + array2[32].ToString());


            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (double.Parse(dt.Rows[i]["TotalPremium"].ToString()) != 0.0)
                {
                    //LossFund
                    array2[0] = "VSC-12/2012" + ","; // dt.Rows[i]["TaskControlID"].ToString() + ",";
                    array2[1] = dt.Rows[i]["PolicyNumber"].ToString().Replace(',', ' ') + ",";
                    array2[2] = "" + ",";  //dt.Rows[i]["Status"].ToString() + ",";
                    array2[3] = GetAccountingIDBankByBankID(dt.Rows[i]["Bank"].ToString()) + ","; //Buscar Codigo Banco
                    array2[4] = DateTime.Now.ToShortDateString() + ","; // dt.Rows[i]["CustomerName"].ToString().Replace(',', ' ') + ',';
                    array2[5] = DateTime.Now.ToShortDateString() + ","; //((DateTime)dt.Rows[i]["EntryDate"]).ToShortDateString() + ",";
                    array2[6] = dt.Rows[i]["TotalPremium"].ToString() + ",";  //((DateTime)dt.Rows[i]["EffectiveDate"]).ToShortDateString() + ",";
                    array2[7] = "" + ",";  //dt.Rows[i]["CompanyDealerDesc"].ToString().Replace(',', ' ') + ",";
                    array2[8] = "" + ",";  //dt.Rows[i]["BankDesc"].ToString() + ",";
                    array2[9] = "" + ",";  //dt.Rows[i]["CoveragePlanDesc"].ToString() + ",";
                    array2[10] = dt.Rows[i]["PolicyNumber"].ToString().Replace(',', ' ') + " - " + dt.Rows[i]["CustomerName"].ToString().Replace(',', ' ') + ',';  //dt.Rows[i]["Term"].ToString() + ",";
                    array2[11] = "" + ",";  //dt.Rows[i]["Miles"].ToString() + ",";
                    array2[12] = "" + ",";  //dt.Rows[i]["Milleages"].ToString() + ",";
                    array2[13] = "" + ",";  //dt.Rows[i]["VehicleMakeDesc"].ToString() + ",";
                    array2[14] = "" + ",";  //dt.Rows[i]["VehicleModelDesc"].ToString() + ",";
                    array2[15] = "" + ",";  //dt.Rows[i]["VehicleYearDesc"].ToString() + ",";
                    array2[16] = "" + ",";  //dt.Rows[i]["VIN"].ToString() + ",";
                    array2[17] = dt.Rows[i]["PolicyNumber"].ToString().Replace(',', ' ') + " - " + dt.Rows[i]["CustomerName"].ToString().Replace(',', ' ') + ',';  //dt.Rows[i]["Pending"].ToString() + ",";
                    array2[18] = "2010-001" + ",";  //2045-001,2025-001, dt.Rows[i]["Charge"].ToString() + ",";
                    array2[19] = "Loss Fund - Vehicle Service Contract" + ","; //dt.Rows[i]["LossFund"].ToString() + ",";
                    array2[20] = GetLoacationIDDealerByDealerID(dt.Rows[i]["CompanyDealer"].ToString()) + ","; //dt.Rows[i]["OverHead"].ToString() + ",";
                    array2[21] = "vsc" + ",";  //dt.Rows[i]["BankFee"].ToString() + ",";
                    array2[22] = "" + ",";  //dt.Rows[i]["Profit"].ToString() + ",";
                    array2[23] = dt.Rows[i]["LossFund"].ToString() + ",";  //dt.Rows[i]["CanReserve"].ToString() + ",";
                    array2[24] = "" + ",";  //dt.Rows[i]["Concurso"].ToString() + ",";
                    array2[25] = "" + ",";  //dt.Rows[i]["DealerCost"].ToString() + ",";
                    array2[26] = "" + ",";  //dt.Rows[i]["DealerProfit"].ToString() + ",";
                    array2[27] = "" + ",";  //dt.Rows[i]["TotalPremium"].ToString() + ",";
                    array2[28] = "" + ",";  //((dt.Rows[i]["CancellationDate"] != System.DBNull.Value) ? ((DateTime)dt.Rows[i]["CancellationDate"]).ToShortDateString() : "") + ",";
                    array2[29] = "" + ",";  //((dt.Rows[i]["CancellationEntryDate"] != System.DBNull.Value) ? ((DateTime)dt.Rows[i]["CancellationEntryDate"]).ToShortDateString() : "") + ",";
                    array2[30] = "" + ",";  //((DateTime)dt.Rows[i]["ExpirationDate"]).ToShortDateString() + ",";
                    array2[31] = "" + ",";  //((dt.Rows[i]["CancellationAmount"] != System.DBNull.Value) ? (((double)dt.Rows[i]["CancellationAmount"]).ToString()) : "0") + ",";
                    array2[32] = "" + ",";  //dt.Rows[i]["PremNet"].ToString();

                    sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +
                    array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +
                    array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +
                    array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() +
                    array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +
                    array2[25].ToString() + array2[26].ToString() + array2[27].ToString() + array2[28].ToString() + array2[29].ToString() +
                    array2[30].ToString() + array2[31].ToString() + array2[32].ToString());
                    
                    //2020-001	Cancellation Reserve - Vehicle Service Contract
                    //2025-001	Bank Fee Payable - Vehicle Service Contract
                    //2020-001	Cancellation Reserve - Vehicle Service Contract
                    //4100-001	Revenues - Vehicle Service Contract
                    //2010-001	Loss Fund - Vehicle Service Contract
                    //2040-001	Dealer Payable - Vehicle Service Contract
                    //2045-001	Concurso Payable - Vehicle Service Contract

                    //OverHead
                    if (double.Parse(dt.Rows[i]["OverHead"].ToString()) != 0.0)
                    {
                        array2[23] = dt.Rows[i]["OverHead"].ToString() + ",";
                        array2[18] = "2020-001" + ",";  //2045-001,2025-001, dt.Rows[i]["Charge"].ToString() + ",";
                        array2[19] = "Cancellation Reserve - Vehicle Service Contract" + ","; //dt.Rows[i]["LossFund"].ToString() + ",";
                        sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +
                        array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +
                        array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +
                        array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() +
                        array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +
                        array2[25].ToString() + array2[26].ToString() + array2[27].ToString() + array2[28].ToString() + array2[29].ToString() +
                        array2[30].ToString() + array2[31].ToString() + array2[32].ToString());
                    }

                    //BankFee
                    if (double.Parse(dt.Rows[i]["BankFee"].ToString()) != 0.0)
                    {
                        array2[23] = dt.Rows[i]["BankFee"].ToString() + ",";
                        array2[18] = "2025-001" + ",";  //2045-001,2025-001, dt.Rows[i]["Charge"].ToString() + ",";
                        array2[19] = "Bank Fee Payable - Vehicle Service Contract" + ","; //dt.Rows[i]["LossFund"].ToString() + ",";
                        sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +
                        array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +
                        array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +
                        array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() +
                        array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +
                        array2[25].ToString() + array2[26].ToString() + array2[27].ToString() + array2[28].ToString() + array2[29].ToString() +
                        array2[30].ToString() + array2[31].ToString() + array2[32].ToString());
                    }

                    //Profit
                    if (double.Parse(dt.Rows[i]["Profit"].ToString()) != 0.0)
                    {
                        array2[23] = dt.Rows[i]["Profit"].ToString() + ",";
                        array2[18] = "4100-001" + ",";  //2045-001,2025-001, dt.Rows[i]["Charge"].ToString() + ",";
                        array2[19] = "Revenues - Vehicle Service Contract" + ","; //dt.Rows[i]["LossFund"].ToString() + ",";
                        sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +
                        array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +
                        array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +
                        array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() +
                        array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +
                        array2[25].ToString() + array2[26].ToString() + array2[27].ToString() + array2[28].ToString() + array2[29].ToString() +
                        array2[30].ToString() + array2[31].ToString() + array2[32].ToString());
                    }

                    //CanReserve
                    if (double.Parse(dt.Rows[i]["CanReserve"].ToString()) != 0.0)
                    {
                        array2[23] = dt.Rows[i]["CanReserve"].ToString() + ",";
                        array2[18] = "2025-001" + ",";  //2045-001,2025-001, dt.Rows[i]["Charge"].ToString() + ",";
                        array2[19] = "Cancellation Reserve - Vehicle Service Contract" + ","; //dt.Rows[i]["LossFund"].ToString() + ",";
                        sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +
                        array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +
                        array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +
                        array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() +
                        array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +
                        array2[25].ToString() + array2[26].ToString() + array2[27].ToString() + array2[28].ToString() + array2[29].ToString() +
                        array2[30].ToString() + array2[31].ToString() + array2[32].ToString());
                    }

                    //Concurso
                    if (double.Parse(dt.Rows[i]["Concurso"].ToString()) != 0.0)
                    {
                        array2[23] = dt.Rows[i]["Concurso"].ToString() + ",";
                        array2[18] = "2045-001" + ",";  //2045-001,2025-001, dt.Rows[i]["Charge"].ToString() + ",";
                        array2[19] = "Concurso Payable - Vehicle Service Contract" + ","; //dt.Rows[i]["LossFund"].ToString() + ",";
                        sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +
                        array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +
                        array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +
                        array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() +
                        array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +
                        array2[25].ToString() + array2[26].ToString() + array2[27].ToString() + array2[28].ToString() + array2[29].ToString() +
                        array2[30].ToString() + array2[31].ToString() + array2[32].ToString());
                    }

                    //DealerProfit
                    if (double.Parse(dt.Rows[i]["DealerProfit"].ToString()) != 0.0)
                    {
                        array2[23] = dt.Rows[i]["DealerProfit"].ToString() + ",";
                        array2[18] = "2040-001" + ",";  //2045-001,2025-001, dt.Rows[i]["Charge"].ToString() + ",";
                        array2[19] = "Dealer Payable - Vehicle Service Contract" + ","; //dt.Rows[i]["LossFund"].ToString() + ",";
                        sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +
                        array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +
                        array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +
                        array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() +
                        array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +
                        array2[25].ToString() + array2[26].ToString() + array2[27].ToString() + array2[28].ToString() + array2[29].ToString() +
                        array2[30].ToString() + array2[31].ToString() + array2[32].ToString());
                    }
                }
            }

            sr.Close();

            DownLoadFile();
        }

        private string[] SetAccountingHeader(string[] array2)
        {
            array2[0] = "BATCH_TITLE,"; //VSC-12/2012
            array2[1] = "INVOICE_NO,"; //Contract No
            array2[2] = "PO_NO,";     //blank 
            array2[3] = "CUSTOMER_ID,"; //Codigo Banco C-0021
            array2[4] = "CREATED_DATE,"; //DateTime.Now
            array2[5] = "DUE_DATE,"; //DateTime.Now
            array2[6] = "TOTAL_DUE,"; //TotPremium
            array2[7] = "TOTAL_PAID,"; //blank
            array2[8] = "PAID_DATE,";//blank
            array2[9] = "TERM_NAME,"; //blank
            array2[10] = "DESCRIPTION,"; //PR03798 - MARIA DEL C MUÑIZ RIVERA 
            array2[11] = "BASECURR,";//blank
            array2[12] = "CURRENCY,";//blank
            array2[13] = "EXCH_RATE_DATE,";//blank
            array2[14] = "EXCH_RATE_TYPE_ID,";//blank
            array2[15] = "EXCHANGE_RATE,";//blank
            array2[16] = "LINE_NO,";//blank
            array2[17] = "MEMO,"; //PR03798 - MARIA DEL C MUÑIZ RIVERA 
            array2[18] = "ACCT_NO,"; //2020-001,2045-001,2025-001, etc
            array2[19] = "ACCT_LABEL,";//Cancellation Reserve - Vehicle Service Contrac, etc
            array2[20] = "LOCATION_ID,"; //cngautogroup
            array2[21] = "DEPT_ID,"; //vsc
            array2[22] = "ALLOCATION_ID,"; //blank
            array2[23] = "AMOUNT,"; //Breakdown Amt
            array2[24] = "SUBTOTAL,";//blank								
            array2[25] = "REVREC_TEMPLATE,";//blank
            array2[26] = "REVREC_STARTDATE,";//blank
            array2[27] = "DEFERREDREV_ACCOUNT,";//blank
            array2[28] = "REVREC_JOURNAL,";//blank
            array2[29] = "REVREC_SCHEDULE_LINE_NO,";//blank
            array2[30] = "REVENUE_ACCOUNT,";//blank
            array2[31] = "REVREC_POSTINGDATE,";//blank
            array2[32] = "REVREC_AMOUNT,";//blank
            return array2;
        }

        private string GetAccountingIDBankByBankID(string bankID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("BankID",
            SqlDbType.Int, 0, bankID.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetBankByBankID", xmlDoc);
                return dt.Rows[0]["AccountingID"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        private string GetLoacationIDDealerByDealerID(string DealerID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("CompanyDealerID",
            SqlDbType.Int, 0, DealerID.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetCompanyDealerByCompanyDealerID", xmlDoc);
                return dt.Rows[0]["LocationID"].ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        private int GetSeqNo()
        {
            //string file = @"Z:\VSCSequenceFile.txt";
            string file = @"C:\Inetpub\Optima Page\Optima\VSCSequenceFile.txt";
            //string file = @"C:\Inetpub\wwwroot\Optima\VSCSequenceFile.txt";
            StreamWriter sr2;
            string varYear = DateTime.Now.Year.ToString().Substring(2, 2);
            string varMonth = DateTime.Now.Month.ToString();
            string varDay = DateTime.Now.Day.ToString();
            string date = varYear + varMonth.PadLeft(2, '0') + varDay.PadLeft(2, '0');
            int seq = 1;

            if (File.Exists(file))
            {
                StreamReader sr = File.OpenText(file);
                string a = sr.ReadToEnd();
                sr.Close();

                if (a.Trim() != "")
                {
                    if (a.Substring(0, 6) == date)
                    {
                        seq = int.Parse(a.Substring(6, 1));

                        File.Delete(file);
                        sr2 = File.CreateText(file);
                        seq = seq + 1;
                        string mdata = date + seq.ToString();


                        sr2.WriteLine(mdata);
                        sr2.WriteLine();
                        sr2.Close();
                    }
                    else
                    {
                        File.Delete(file);
                        sr2 = File.CreateText(file);
                        string mdata = date + seq.ToString();


                        sr2.WriteLine(mdata);
                        sr2.WriteLine();
                        sr2.Close();
                    }
                }
                else
                {
                    File.Delete(file);
                    sr2 = File.CreateText(file);
                    string mdata = date + seq.ToString();

                    sr2.WriteLine(mdata);
                    sr2.WriteLine();
                    sr2.Close();
                }
            }
            else
            {
                sr2 = File.CreateText(file);
                string mdata = date + seq.ToString();

                sr2.WriteLine(mdata);
                sr2.WriteLine();
                sr2.Close();
            }

            return seq;
        }

        private int GetQCertSeqNo()
        {
            //string file = @"Z:\VSCSequenceFile.txt";
            string file = @"C:\Inetpub\Optima Page\Optima\QCertSequenceFile.txt";
            //string file = @"C:\Inetpub\wwwroot\Optima\VSCSequenceFile.txt";
            StreamWriter sr2;
            string varYear = DateTime.Now.Year.ToString().Substring(2, 2);
            string varMonth = DateTime.Now.Month.ToString();
            string varDay = DateTime.Now.Day.ToString();
            string date = varYear + varMonth.PadLeft(2, '0') + varDay.PadLeft(2, '0');
            int seq = 1;

            if (File.Exists(file))
            {
                StreamReader sr = File.OpenText(file);
                string a = sr.ReadToEnd();
                sr.Close();

                if (a.Trim() != "")
                {
                    if (a.Substring(0, 6) == date)
                    {
                        seq = int.Parse(a.Substring(6, 1));

                        File.Delete(file);
                        sr2 = File.CreateText(file);
                        seq = seq + 1;
                        string mdata = date + seq.ToString();


                        sr2.WriteLine(mdata);
                        sr2.WriteLine();
                        sr2.Close();
                    }
                    else
                    {
                        File.Delete(file);
                        sr2 = File.CreateText(file);
                        string mdata = date + seq.ToString();


                        sr2.WriteLine(mdata);
                        sr2.WriteLine();
                        sr2.Close();
                    }
                }
                else
                {
                    File.Delete(file);
                    sr2 = File.CreateText(file);
                    string mdata = date + seq.ToString();

                    sr2.WriteLine(mdata);
                    sr2.WriteLine();
                    sr2.Close();
                }
            }
            else
            {
                sr2 = File.CreateText(file);
                string mdata = date + seq.ToString();

                sr2.WriteLine(mdata);
                sr2.WriteLine();
                sr2.Close();
            }

            return seq;
        }

        private void VSCSunGuard()
        {
            StreamWriter sr;
            DataTable dt = TaskControl.Policies.GetVSCSunGuardInterfase(1, txtBegDate.Text, TxtEndDate.Text);

            string varYear = DateTime.Now.Year.ToString().Substring(2, 2);
            string varMonth = DateTime.Now.Month.ToString();
            string varDay = DateTime.Now.Day.ToString();
            string file = varYear + varMonth.PadLeft(2, '0') + varDay.PadLeft(2, '0');
            file = file.Trim();
            file = file.PadRight(8, ' ');

            FileName = @"C:\Inetpub\Optima Page\Optima\VSC\VSCSunGuard" + file.Trim() + ".txt";
            //FileName = @"C:\Inetpub\wwwroot\Optima\VSC\VSCSunGuard" + file.Trim() + ".txt";

            sr = File.CreateText(FileName);

            if (dt.Rows.Count == 0)
            {
                throw new Exception("There is no existing information for this file.");
            }

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sr = SetDataTOSunGuard(dt, i, sr);
            }

            sr.Close();
            DownLoadFile();
        }

        private StreamWriter SetDataTOSunGuard(DataTable dt, int index, StreamWriter sr)
        {

            for (int a = 1; a <= 6; a++)
            {
                string rowString = "";
                string val = "";
                string var1 = "";
                string var2 = "";
                string var3 = "";
                string var4 = "";
                string var5 = "";
                string var6 = "";
                string var7 = "";
                string var8 = "";
                string var9 = "";
                string var10 = "";
                string var11 = "";
                string var12 = "";
                string var13 = "";
                string var14 = "";

                switch (a)
                {
                    case 1:     //A/R SERV CONTRACT
                        var1 = "1120-010";
                        val = dt.Rows[index]["Price"].ToString().Trim() + ".00";
                        var5 = val.PadLeft(9, ' ');
                        var6 = "D";
                        break;
                    case 2:     //LOSS FUND
                        var1 = "2010-000";
                        val = dt.Rows[index]["LossFund"].ToString().Trim() + ".00";
                        var5 = val.PadLeft(9, ' ');
                        var6 = "C";
                        break;
                    case 3:     //BANK FEE
                        var1 = "2040-010";
                        val = dt.Rows[index]["BankFee"].ToString() + ".00";
                        var5 = val.PadLeft(9, ' ');
                        var6 = "C";
                        break;
                    case 4:     //CANCELLATION RESERVE
                        var1 = "2040-020";
                        val = dt.Rows[index]["CanReserve"].ToString() + ".00";
                        var5 = val.PadLeft(9, ' ');
                        var6 = "C";
                        break;
                    case 5:     //SERV CONT FEE
                        var1 = "2040-000";
                        val = dt.Rows[index]["DealerProfit"].ToString() + ".00";
                        var5 = val.PadLeft(9, ' ');
                        var6 = "C";
                        break;
                    case 6:     //SERV CONT INCOME
                        var1 = "4100-000";
                        val = dt.Rows[index]["Income"].ToString() + ".00";
                        var5 = val.PadLeft(9, ' ');
                        var6 = "C";
                        break;
                }

                var2 = "CASH";
                var3 = TxtEndDate.Text.Substring(0, 2) + TxtEndDate.Text.Substring(3, 2) + TxtEndDate.Text.Substring(6, 4);
                var4 = "PRMWS";
                var7 = "PROD " + TxtEndDate.Text.Substring(6, 4) + TxtEndDate.Text.Substring(3, 2) + TxtEndDate.Text.Substring(0, 2) + " - " + txtBegDate.Text.Substring(6, 4) + txtBegDate.Text.Substring(3, 2) + "-" + txtBegDate.Text.Substring(0, 2);
                var8 = "WGE";
                var9 = TxtEndDate.Text.Substring(3, 2) + TxtEndDate.Text.Substring(6, 4);
                var10 = dt.Rows[index]["Bank"].ToString();
                var11 = dt.Rows[index]["CompanyDealer"].ToString();
                var12 = "010";
                var13 = TxtEndDate.Text.Substring(0, 2) + TxtEndDate.Text.Substring(3, 2) + TxtEndDate.Text.Substring(6, 4);
                var14 = "To record issuance of service contracts";

                rowString = var1 + var2 + var3 + var4 + var5 + var6 + var7 + var8 + var9 + var10 + var11 + var12 + var13 + var14;

                sr.WriteLine(rowString);
            }

            return sr;
        }

        private void VSCSunGuardDeposit()
        {
            StreamWriter sr;
            DataTable dt = TaskControl.Policies.GetVSCSunGuardInterfase(1, txtBegDate.Text, TxtEndDate.Text);

            string varYear = DateTime.Now.Year.ToString().Substring(2, 2);
            string varMonth = DateTime.Now.Month.ToString();
            string varDay = DateTime.Now.Day.ToString();
            string file = varYear + varMonth.PadLeft(2, '0') + varDay.PadLeft(2, '0');
            file = file.Trim();
            file = file.PadRight(8, ' ');

            FileName = @"C:\Inetpub\Optima Page\Optima\VSC\VSCSunGuardDeposit" + file.Trim() + ".txt";
            //FileName = @"C:\Inetpub\wwwroot\Optima\VSC\VSCSunGuardDeposit" + file.Trim() + ".txt";

            sr = File.CreateText(FileName);

            if (dt.Rows.Count == 0)
            {
                throw new Exception("There is no existing information for this file.");
            }

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sr = SetDataTOSunGuardDeposit(dt, i, sr);
            }

            sr.Close();
            DownLoadFile();
        }

        private StreamWriter SetDataTOSunGuardDeposit(DataTable dt, int index, StreamWriter sr)
        {

            for (int a = 1; a <= 2; a++)
            {
                string rowString = "";
                string val = "";
                string var1 = "";
                string var2 = "";
                string var3 = "";
                string var4 = "";
                string var5 = "";
                string var6 = "";
                string var7 = "";
                string var8 = "";

                var1 = TxtEndDate.Text.Substring(0, 2) + TxtEndDate.Text.Substring(3, 2) + TxtEndDate.Text.Substring(6, 4);
                switch (a)
                {
                    case 1:     //CONCENTRATION DEPOSIT
                        var2 = "1050-602";
                        val = dt.Rows[index]["Price"].ToString().Trim() + ".00";
                        var3 = val.PadLeft(9, ' ');
                        var4 = "D";
                        var5 = "CONCENTRATION DEPOSIT";
                        var5 = var5.PadRight(25, ' ');
                        break;
                    case 2:     //A/R SERV CONTRACT
                        var2 = "1120-010";
                        val = dt.Rows[index]["Price"].ToString().Trim() + ".00";
                        var3 = val.PadLeft(9, ' ');
                        var4 = "C";
                        var5 = "A/R SERV CONTRACT";
                        var5 = var5.PadRight(25, ' ');
                        break;
                }

                var6 = dt.Rows[index]["Bank"].ToString();
                var7 = dt.Rows[index]["CompanyDealer"].ToString();
                var8 = "010";

                rowString = var1 + var2 + var3 + var4 + var5 + var6 + var7 + var8;

                sr.WriteLine(rowString);
            }

            return sr;
        }

        private void VSCImagingDataFile()
        {
            StreamWriter sr;
            FileName = @"C:\Inetpub\Optima Page\Optima\VSC\ImagingDataFile\IM" +
                DateTime.Now.Month.ToString().Trim() +
                DateTime.Now.Day.ToString().Trim() +
                DateTime.Now.Year.ToString().Trim() + ".txt";
            //FileName = @"C:\Inetpub\wwwroot\Optima\VSC\ImagingDataFile\IM" + 
                //DateTime.Now.Month.ToString().Trim() +
                //DateTime.Now.Day.ToString().Trim() +
               // DateTime.Now.Year.ToString().Trim() + ".txt";
            sr = File.CreateText(FileName);

            DataTable dt = this.GetVSCImagingDataFile(txtBegDate.Text, TxtEndDate.Text);

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                string var1 = dt.Rows[i]["DataFile"].ToString().Trim();

                sr.WriteLine(var1);
            }

            sr.Close();
            DownLoadFile();
        }

        private void VCSProductionFile()
        {
            try
            {
                StreamWriter sr;

                DataTable dt = TaskControl.Policies.GetVSCProductionHeader(1, txtBegDate.Text, TxtEndDate.Text, true);
                DataTable dtCanc = TaskControl.Policies.GetVSCProductionHeader(1, txtBegDate.Text, TxtEndDate.Text, false);

                //DataTable dt = TaskControl.Policies.GetVSCProductionHeader(1, "5/1/2009", "8/13/2009", true);
                //DataTable dtCanc = TaskControl.Policies.GetVSCProductionHeader(1, "5/1/2009", "8/13/2009", false);

                string varYear = DateTime.Now.Year.ToString().Substring(2, 2);
                string varMonth = DateTime.Now.Month.ToString();
                string varDay = DateTime.Now.Day.ToString();
                string file = varYear + varMonth.PadLeft(2, '0') + varDay.PadLeft(2, '0');
                file = file.Trim();
                file = file.PadRight(8, ' ');

                int seq = GetSeqNo();
                FileName = @"C:\Inetpub\Optima Page\Optima\VSC\OP" + file.Trim() + seq.ToString() + ".txt";
                //FileName = @"C:\Inetpub\wwwroot\Optima\VSC\OP" + file.Trim() + seq.ToString() + ".txt";

                sr = File.CreateText(FileName);

                if (dt.Rows.Count == 0 || dt.Rows[0]["TotalNewPolicies"].ToString().Trim() == "")
                {
                    throw new Exception("There is no existing information for this report.");
                }

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    string rowString = "";

                    //string var0 = "OPTIMA";
                    string var0 = "OPTPRM";
                    var0 = var0.Trim();
                    var0 = var0.PadRight(8, ' ');

                    string var1 = "H"; //1
                    var1 = var1.Trim();
                    var1 = var1.PadRight(1, ' ');

                    varYear = DateTime.Now.Year.ToString(); //8 (CCYYMMDD)
                    varMonth = DateTime.Now.Month.ToString();
                    varDay = DateTime.Now.Day.ToString();
                    string var2 = varYear + varMonth.PadLeft(2, '0') + varDay.PadLeft(2, '0');
                    var2 = var2.Trim();
                    var2 = var2.PadRight(8, ' ');


                    string varHour = DateTime.Now.Hour.ToString(); //8 (HHMMSS)
                    string varMinute = DateTime.Now.Minute.ToString();
                    string varSecond = DateTime.Now.Second.ToString();
                    string var3 = varHour + varMinute + varSecond;
                    var3 = var3.Trim();
                    var3 = var3.PadRight(6, ' ');

                    string var4 = dt.Rows[i]["SequenceID"].ToString().Trim(); //3
                    var4 = var4.Trim();
                    var4 = var4.PadLeft(3, '0');

                    string var5 = ""; //10
                    var5 = var5.Trim();
                    var5 = var5.PadRight(10, ' ');

                    int CancCount = 0;
                    if (dtCanc.Rows[i]["TotalCancPolicies"].ToString().Trim() != "")
                    {
                        CancCount = int.Parse(dtCanc.Rows[i]["TotalCancPolicies"].ToString());
                    }

                    int tot = int.Parse(dt.Rows[i]["TotalNewPolicies"].ToString()); //+ CancCount;
                    string var6 = tot.ToString().Trim(); //6
                    var6 = var6.Trim();
                    var6 = var6.PadLeft(6, '0');

                    string var7 = dt.Rows[i]["TotalNewPolicies"].ToString(); //5
                    var7 = var7.Trim();
                    var7 = var7.PadLeft(5, '0');

                    string var8 = "0";// CancCount.ToString(); //5
                    var8 = var8.Trim();
                    var8 = var8.PadLeft(5, '0');

                    string var9 = "0"; //5
                    var9 = var9.Trim();
                    var9 = var9.PadLeft(5, '0');

                    double amt = double.Parse(dt.Rows[i]["TotalClientCost"].ToString());
                    string var10 = "0000000";  // amt.ToString().Replace(".", "");
                    var10 = var10.Trim();
                    var10 = var10.PadLeft(7, '0');

                    amt = double.Parse(dt.Rows[i]["TotalCustomerCost"].ToString());
                    string var11 = "0000000";  // amt.ToString().Replace(".", "");
                    var11 = var11.Trim();
                    var11 = var11.PadLeft(7, '0');

                    string var12 = ""; //182
                    var12 = var12.Trim();
                    var12 = var12.PadLeft(182, ' ');

                    amt = double.Parse(dt.Rows[i]["TotalClientCost"].ToString());
                    string var13 = amt.ToString().Replace(".", "");
                    var13 = var13.Trim() + "00";
                    var13 = var13.PadLeft(9, '0');

                    amt = double.Parse(dt.Rows[i]["TotalCustomerCost"].ToString());
                    string var14 = amt.ToString().Replace(".", "");
                    var14 = var14.Trim() + "00";
                    var14 = var14.PadLeft(9, '0');

                    rowString = var0 + var1 + var2 + var3 + var4 + var5 + var6 + var7 + var8 + var9 + var10 + var11 + var12 + var13 + var14;  //+"\r\n";

                    sr.WriteLine(rowString);
                }

                dt = TaskControl.Policies.GetVSCProductionDetail(1, txtBegDate.Text, TxtEndDate.Text, true);
                //dt = TaskControl.Policies.GetVSCProductionDetail(1, "5/1/2009", "8/13/2009", true);
                //dtCanc = TaskControl.Policies.GetVSCProductionDetail(1, txtBegDate.Text, TxtEndDate.Text, false);
                sr = WriteProduction(dt, sr, true);
                sr = WriteProduction(dt, sr, false);

                //sr.WriteLine();
                sr.Close();

                //Actualiza el campo trams_fl de los casos transmitidos.
                TaskControl.Policies.GetVSCProductionUpdate(1, "01/01/2009", TxtEndDate.Text, true);

                if (Session["VSCProductionFile"] != null)
                    Session["VSCProductionFile"] = dt;
                else
                    Session.Add("VSCProductionFile", dt);

                DownLoadFile();
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }

        private void QCertProductionFile()
        {
            try
            {
                StreamWriter sr;

                DataTable dt = TaskControl.Policies.GetVSCProductionHeader(16, txtBegDate.Text, TxtEndDate.Text, true);
                DataTable dtCanc = TaskControl.Policies.GetVSCProductionHeader(16, txtBegDate.Text, TxtEndDate.Text, false);

                //DataTable dt = TaskControl.Policies.GetVSCProductionHeader(1, "5/1/2009", "8/13/2009", true);
                //DataTable dtCanc = TaskControl.Policies.GetVSCProductionHeader(1, "5/1/2009", "8/13/2009", false);

                string varYear = DateTime.Now.Year.ToString().Substring(2, 2);
                string varMonth = DateTime.Now.Month.ToString();
                string varDay = DateTime.Now.Day.ToString();
                string file = varYear + varMonth.PadLeft(2, '0') + varDay.PadLeft(2, '0');
                file = file.Trim();
                file = file.PadRight(8, ' ');

                int seq = GetQCertSeqNo();
                FileName = @"C:\Inetpub\Optima Page\Optima\QCert\OP" + file.Trim() + seq.ToString() + ".txt";
                //FileName = @"C:\Inetpub\wwwroot\Optima\VSC\OP" + file.Trim() + seq.ToString() + ".txt";

                sr = File.CreateText(FileName);

                if (dt.Rows.Count == 0 || dt.Rows[0]["TotalNewPolicies"].ToString().Trim() == "")
                {
                    throw new Exception("There is no existing information for this report.");
                }

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    string rowString = "";

                    //string var0 = "OPTIMA";
                    string var0 = "OPTPRM";
                    var0 = var0.Trim();
                    var0 = var0.PadRight(8, ' ');

                    string var1 = "H"; //1
                    var1 = var1.Trim();
                    var1 = var1.PadRight(1, ' ');

                    varYear = DateTime.Now.Year.ToString(); //8 (CCYYMMDD)
                    varMonth = DateTime.Now.Month.ToString();
                    varDay = DateTime.Now.Day.ToString();
                    string var2 = varYear + varMonth.PadLeft(2, '0') + varDay.PadLeft(2, '0');
                    var2 = var2.Trim();
                    var2 = var2.PadRight(8, ' ');


                    string varHour = DateTime.Now.Hour.ToString(); //8 (HHMMSS)
                    string varMinute = DateTime.Now.Minute.ToString();
                    string varSecond = DateTime.Now.Second.ToString();
                    string var3 = varHour + varMinute + varSecond;
                    var3 = var3.Trim();
                    var3 = var3.PadRight(6, ' ');

                    string var4 = dt.Rows[i]["SequenceID"].ToString().Trim(); //3
                    var4 = var4.Trim();
                    var4 = var4.PadLeft(3, '0');

                    string var5 = ""; //10
                    var5 = var5.Trim();
                    var5 = var5.PadRight(10, ' ');

                    int CancCount = 0;
                    if (dtCanc.Rows[i]["TotalCancPolicies"].ToString().Trim() != "")
                    {
                        CancCount = int.Parse(dtCanc.Rows[i]["TotalCancPolicies"].ToString());
                    }

                    int tot = int.Parse(dt.Rows[i]["TotalNewPolicies"].ToString()); //+ CancCount;
                    string var6 = tot.ToString().Trim(); //6
                    var6 = var6.Trim();
                    var6 = var6.PadLeft(6, '0');

                    string var7 = dt.Rows[i]["TotalNewPolicies"].ToString(); //5
                    var7 = var7.Trim();
                    var7 = var7.PadLeft(5, '0');

                    string var8 = "0";// CancCount.ToString(); //5
                    var8 = var8.Trim();
                    var8 = var8.PadLeft(5, '0');

                    string var9 = "0"; //5
                    var9 = var9.Trim();
                    var9 = var9.PadLeft(5, '0');

                    double amt = double.Parse(dt.Rows[i]["TotalClientCost"].ToString());
                    string var10 = "0000000";  // amt.ToString().Replace(".", "");
                    var10 = var10.Trim();
                    var10 = var10.PadLeft(7, '0');

                    amt = double.Parse(dt.Rows[i]["TotalCustomerCost"].ToString());
                    string var11 = "0000000";  // amt.ToString().Replace(".", "");
                    var11 = var11.Trim();
                    var11 = var11.PadLeft(7, '0');

                    string var12 = ""; //182
                    var12 = var12.Trim();
                    var12 = var12.PadLeft(182, ' ');

                    amt = double.Parse(dt.Rows[i]["TotalClientCost"].ToString());
                    string var13 = amt.ToString().Replace(".", "");
                    var13 = var13.Trim() + "00";
                    var13 = var13.PadLeft(9, '0');

                    amt = double.Parse(dt.Rows[i]["TotalCustomerCost"].ToString());
                    string var14 = amt.ToString().Replace(".", "");
                    var14 = var14.Trim() + "00";
                    var14 = var14.PadLeft(9, '0');

                    rowString = var0 + var1 + var2 + var3 + var4 + var5 + var6 + var7 + var8 + var9 + var10 + var11 + var12 + var13 + var14;  //+"\r\n";

                    sr.WriteLine(rowString);
                }

                dt = TaskControl.Policies.GetVSCProductionDetail(16, txtBegDate.Text, TxtEndDate.Text, true);
                //dt = TaskControl.Policies.GetVSCProductionDetail(1, "5/1/2009", "8/13/2009", true);
                //dtCanc = TaskControl.Policies.GetVSCProductionDetail(1, txtBegDate.Text, TxtEndDate.Text, false);
                sr = WriteProduction(dt, sr, true);
                sr = WriteProduction(dt, sr, false);

                //sr.WriteLine();
                sr.Close();

                //Actualiza el campo trams_fl de los casos transmitidos.
                TaskControl.Policies.GetVSCProductionUpdate(16, "01/01/2009", TxtEndDate.Text, true);

                if (Session["VSCProductionFile"] != null)
                    Session["VSCProductionFile"] = dt;
                else
                    Session.Add("VSCProductionFile", dt);

                DownLoadFile();
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }
        
        private StreamWriter WriteProduction(DataTable dt, StreamWriter sr, bool IsPolicies)
        {
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (IsPolicies == true && dt.Rows[i]["PrmCount"].ToString().Trim() == "1")
                {
                    sr = WriteLineInStream(dt, sr, IsPolicies, i);
                }

                if (IsPolicies == false && dt.Rows[i]["CancCount"].ToString().Trim() == "1")
                {
                    //sr = WriteLineInStream(dt, sr, IsPolicies, i);
                }
            }
            return sr;
        }

        private StreamWriter WriteLineInStream(DataTable dt, StreamWriter sr, bool IsPolicies, int i)
        {
            string rowString = "";

            //string var1 = "OPTIMA";
            string var1 = "OPTPRM";
            var1 = var1.Trim();
            var1 = var1.PadRight(8, ' ');

            string var2;
            if (IsPolicies)
                var2 = "P"; //1
            else
                var2 = "C"; //1

            var2 = var2.Trim();
            var2 = var2.PadRight(1, ' ');

            LookupTables.CompanyDealer cd = new LookupTables.CompanyDealer();
            cd = cd.GetCompanyDealer(dt.Rows[i]["CompanyDealer"].ToString());
            string var3 = cd.VSCClientID;
            var3 = var3.Trim();
            var3 = var3.PadLeft(8, '0');

            string var4 = "C"; //1
            var4 = var4.Trim();

            string var5 = "";
            if (dt.Rows[0]["VehicleCode"] != System.DBNull.Value)
            {
                var5 = dt.Rows[i]["VehicleCode"].ToString();
            }
            var5 = var5.Trim();
            var5 = var5.PadRight(5, ' ');

            string var6 = dt.Rows[i]["PolicyNumber"].ToString();
            var6 = var6.Trim();
            var6 = var6.PadRight(8, ' ');

            string var7 = "PR001R1210"; // "PR0011007";
            var7 = var7.Trim();
            var7 = var7.PadRight(25, ' ');

            string var8 = "";

            switch (int.Parse(dt.Rows[i]["CoveragePlan"].ToString()))
            {
                case 1:     //POWER TRAIN - PT
                    var8 = "PC";
                    break;
                case 2:     //SILVER - SL
                    var8 = "SL";
                    break;
                case 3:     //GOLD - GD
                    var8 = "GD";
                    break;
                case 4:     //PLATINUM - PL
                    var8 = "PL";
                    break;
            }

            var8 = var8.Trim();
            var8 = var8.PadRight(6, ' ');

            string var9 = "WAR";
            var9 = var9.Trim();
            var9 = var9.PadRight(3, ' ');

            string var10 = dt.Rows[i]["Term"].ToString();
            var10 = var10.Trim();
            var10 = var10.PadLeft(3, '0');

            int miles = int.Parse(dt.Rows[i]["Miles"].ToString());
            miles = miles / 1000;
            string var11 = miles.ToString();
            var11 = var11.Trim();
            var11 = var11.PadLeft(3, '0');

            double amt = double.Parse(dt.Rows[i]["LossFund"].ToString());
            string var12 = amt.ToString().Replace(".", "");
            var12 = var12.Trim() + "00";
            var12 = var12.PadLeft(7, '0');

            string var13 = " ";
            var13 = var13.Trim();
            var13 = var13.PadLeft(1, ' ');

            string var14 = dt.Rows[i]["ZipCode"].ToString().Replace("-", "");
            var14 = var14.Trim();
            var14 = var14.PadLeft(13, ' ');

            string var15 = "";
            if (dt.Rows[0]["HomePhone"] != System.DBNull.Value)
            {
                var15 = dt.Rows[i]["HomePhone"].ToString().Substring(1, 3);
            }
            var15 = var15.Trim();
            var15 = var15.PadLeft(3, '0');

            string var16 = "";
            if (dt.Rows[0]["HomePhone"] != System.DBNull.Value)
            {
                var16 = dt.Rows[i]["HomePhone"].ToString().Substring(6, 8);
            }
            var16 = var16.Replace("-", "").Trim();
            var16 = var16.PadLeft(7, '0');

            string varYear = DateTime.Parse(dt.Rows[i]["EffectiveDate"].ToString()).Year.ToString(); //8 (CCYYMMDD)
            string varMonth = DateTime.Parse(dt.Rows[i]["EffectiveDate"].ToString()).Month.ToString();
            string varDay = DateTime.Parse(dt.Rows[i]["EffectiveDate"].ToString()).Day.ToString();
            string var17 = varYear + varMonth.PadLeft(2, '0') + varDay.PadLeft(2, '0');
            var17 = var17.Trim();
            var17 = var17.PadRight(8, '0');

            string var18 = "00000000";
            var18 = var18.Trim();
            var18 = var18.PadRight(8, '0');

            varYear = DateTime.Parse(dt.Rows[i]["EffectiveDate"].ToString()).Year.ToString(); //8 (CCYYMMDD)
            varMonth = DateTime.Parse(dt.Rows[i]["EffectiveDate"].ToString()).Month.ToString();
            varDay = DateTime.Parse(dt.Rows[i]["EffectiveDate"].ToString()).Day.ToString();
            string var19 = varYear + varMonth.PadLeft(2, '0') + varDay.PadLeft(2, '0');
            var19 = var19.Trim();
            var19 = var19.PadRight(8, '0');

            string var20 = dt.Rows[i]["Milleages"].ToString();
            var20 = var20.Trim();
            var20 = var20.PadLeft(6, '0');

            string var21 = dt.Rows[i]["VehicleYearDesc"].ToString();
            var21 = var21.Trim();
            var21 = var21.PadLeft(4, '0');

            string var22 = "";
            if ((double)dt.Rows[i]["Charge"] == 0)
            {
                var22 = "100";
                var22 = var22.Trim();
                var22 = var22.PadLeft(4, '0');
            }
            else
            {
                var22 = "0000";
                var22 = var22.Trim();
                var22 = var22.PadLeft(4, '0');
            }

            string var23 = "N";
            var23 = var23.Trim();

            amt = double.Parse(dt.Rows[i]["TotalPremium"].ToString());
            string var24 = amt.ToString().Replace(".", "");
            var24 = var24.Trim() + "00";
            var24 = var24.PadLeft(7, '0');

            string var25 = "000";
            var25 = var25.Trim();

            string var26 = "0000000";
            var26 = var26.Trim();

            string var27 = "00";
            var27 = var27.Trim();


            //CommercialUse
            string var28 = "N";
            //if ((bool)dt.Rows[i]["CommercialUse"] == true)
            //{
            //    var28 = "Y";
            //}
            //else
            //{
            //    var28 = "N";
            var28 = var28.Trim();
            //}

            string var29 = "N";
            var29 = var29.Trim();

            string var30 = "N";
            var30 = var30.Trim();

            string var31 = dt.Rows[i]["firstna"].ToString().Replace("Ñ", "N");
            var31 = var31.Trim();
            var31 = var31.PadRight(25, ' ');

            string var32 = dt.Rows[i]["Initial"].ToString().Replace("Ñ", "N");
            var32 = var32.Trim();
            var32 = var32.PadRight(1, ' ');

            string var33 = dt.Rows[i]["lastna1"].ToString().Replace("Ñ", "N");
            var33 = var33.Trim();
            var33 = var33.PadRight(25, ' ');

            string var34 = dt.Rows[i]["Adds1"].ToString().Replace("Ñ", "N");
            var34 = var34.Trim();
            var34 = var34.PadRight(30, ' ');

            string var35 = dt.Rows[i]["Adds2"].ToString().Replace("Ñ", "N");
            var35 = var35.Trim();
            var35 = var35.PadRight(30, ' ');

            string var36 = dt.Rows[i]["City"].ToString().Replace("Ñ", "N");
            var36 = var36.Trim();
            var36 = var36.PadRight(30, ' ');

            string var37 = dt.Rows[i]["Vin"].ToString().Replace("Ñ", "N");
            var37 = var37.Trim();
            var37 = var37.PadRight(17, ' ');

            string var38 = "USA";
            var38 = var38.Trim();
            var38 = var38.PadRight(3, ' ');

            string var39 = dt.Rows[i]["State"].ToString().Replace("Ñ", "N");
            var39 = var39.Trim();
            var39 = var39.PadRight(2, ' ');

            string var40 = dt.Rows[i]["VehicleClass"].ToString();
            var40 = var40.Trim();
            var40 = var40.PadLeft(3, ' ');

            string var41 = " ";
            var41 = var41.Trim();
            var41 = var41.PadLeft(1, ' ');

            string var42 = " ";
            var42 = var42.Trim();
            var42 = var42.PadLeft(1, ' ');

            string var43 = " ";
            var43 = var43.Trim();
            var43 = var43.PadLeft(1, ' ');

            string var44 = " ";
            var44 = var44.Trim();
            var44 = var44.PadLeft(1, ' ');

            string var45 = " ";
            var45 = var45.Trim();
            var45 = var45.PadLeft(1, ' ');

            string var46 = " ";
            var46 = var46.Trim();
            var46 = var46.PadLeft(1, ' ');

            string var47 = " ";
            var47 = var47.Trim();
            var47 = var47.PadLeft(1, ' ');

            string var48 = " ";
            var48 = var48.Trim();
            var48 = var48.PadLeft(1, ' ');

            string var49 = "000000000";
            var49 = var49.Trim();
            var49 = var49.PadLeft(9, '0');

            string var50 = "  ";
            var50 = var50.Trim();
            var50 = var50.PadLeft(2, ' ');

            string var51 = "  ";
            var51 = var51.Trim();
            var51 = var51.PadLeft(2, ' ');

            string var52 = "  ";
            var52 = var52.Trim();
            var52 = var52.PadLeft(2, ' ');

            string var53 = "  ";
            var53 = var53.Trim();
            var53 = var53.PadLeft(2, ' ');

            string var54 = "  ";
            var54 = var54.Trim();
            var54 = var54.PadLeft(2, ' ');

            string var55 = "PR";
            var55 = var55.Trim();
            var55 = var55.PadLeft(2, ' ');

            string var56 = "               ";
            var56 = var56.Trim();
            var56 = var56.PadLeft(15, ' ');

            string var57 = "0000000";
            var57 = var57.Trim();
            var57 = var57.PadLeft(7, '0');

            string var58 = "          ";
            var58 = var58.Trim();
            var58 = var58.PadLeft(10, ' ');

            string var59 = "00000000";
            var59 = var59.Trim();
            var59 = var59.PadLeft(8, '0');

            string var60 = "     ";
            var60 = var60.Trim();
            var60 = var60.PadLeft(5, ' ');

            string var61 = "000000";
            var61 = var61.Trim();
            var61 = var61.PadLeft(6, '0');

            string var62 = "000000000000";
            var62 = var62.Trim();
            var62 = var62.PadLeft(12, '0');

            string var63 = dt.Rows[i]["VehicleMakeDesc"].ToString().Trim().Replace("Ñ", "N");
            if (var63.Length > 20)
                var63 = var63.Trim().Substring(0, 20);

            var63 = var63.Trim();
            var63 = var63.PadRight(20, ' ');

            string var64 = dt.Rows[i]["VehicleModelDesc"].ToString().Trim().Replace("Ñ", "N");
            if (var64.Length > 20)
                var64 = var64.Trim().Substring(0, 20);

            var64 = var64.Trim();
            var64 = var64.PadRight(20, ' ');

            string var65 = "      ";
            var65 = var65.Trim();
            var65 = var65.PadLeft(6, ' ');

            string var66 = "        ";
            var66 = var66.Trim();
            var66 = var66.PadLeft(8, ' ');

            string var67 = "0000000";
            var67 = var67.Trim();
            var67 = var67.PadLeft(7, '0');

            string var68 = "000000000";
            var68 = var68.Trim();
            var68 = var68.PadLeft(9, '0');

            string var69 = "000000000";
            var69 = var69.Trim();
            var69 = var69.PadLeft(9, '0');

            string var70 = " ";    //Bank Name
            var70 = var70.Trim();
            var70 = var70.PadLeft(30, ' ');

            string var71 = " ";
            var71 = var71.Trim();
            var71 = var71.PadLeft(30, ' ');

            string var72 = " ";
            var72 = var72.Trim();
            var72 = var72.PadLeft(30, ' ');

            string var73 = " ";
            var73 = var73.Trim();
            var73 = var73.PadLeft(30, ' ');

            string var74 = "  ";
            var74 = var74.Trim();
            var74 = var74.PadLeft(2, ' ');

            string var75 = "0000000000000";
            var75 = var75.Trim();
            var75 = var75.PadLeft(13, '0');

            string var76 = "000";
            var76 = var76.Trim();
            var76 = var76.PadLeft(3, '0');

            string var77 = "0000000";
            var77 = var77.Trim();
            var77 = var77.PadLeft(7, '0');

            string var78 = " ";
            var78 = var78.Trim();
            var78 = var78.PadLeft(15, ' ');

            string var79 = " ";
            var79 = var79.Trim();
            var79 = var79.PadLeft(20, ' ');

            string var80 = "00000000";
            var80 = var80.Trim();
            var80 = var80.PadLeft(8, '0');

            string var81 = "000000000";
            var81 = var81.Trim();
            var81 = var81.PadLeft(9, '0');

            string var82 = " ";
            var82 = var82.Trim();
            var82 = var82.PadLeft(6, ' ');

            string var83 = "00000000000";
            var83 = var83.Trim();
            var83 = var83.PadLeft(11, '0');

            string var84 = "00000000000";
            var84 = var84.Trim();
            var84 = var84.PadLeft(11, '0');

            string var85 = "000";
            var85 = var85.Trim();
            var85 = var85.PadLeft(3, '0');

            string var86 = "00000000";
            var86 = var86.Trim();
            var86 = var86.PadLeft(8, '0');

            string var87 = "0000";
            var87 = var87.Trim();
            var87 = var87.PadLeft(4, '0');

            string var88 = "00000000000";
            var88 = var88.Trim();
            var88 = var88.PadLeft(11, '0');

            string var89 = "00000000000";
            var89 = var89.Trim();
            var89 = var89.PadLeft(11, '0');

            string var90 = "00000000";
            var90 = var90.Trim();
            var90 = var90.PadLeft(8, '0');

            string var91 = "00000000000";
            var91 = var91.Trim();
            var91 = var91.PadLeft(11, '0');

            string var92 = "00000000000";
            var92 = var92.Trim();
            var92 = var92.PadLeft(11, '0');

            string var93 = " ";
            var93 = var93.Trim();
            var93 = var93.PadLeft(5, ' ');

            string var94 = " ";
            var94 = var94.Trim();
            var94 = var94.PadLeft(1, ' ');

            string var95 = " ";
            var95 = var95.Trim();
            var95 = var95.PadLeft(30, ' ');

            string var96 = " ";
            var96 = var96.Trim();
            var96 = var96.PadLeft(30, ' ');

            rowString = var1 + var2 + var3 + var4 + var5 + var6 + var7 + var8 + var9 + var10 + var11 + var12
                + var13 + var14 + var15 + var16 + var17 + var18 + var19 + var20 + var21 + var22 + var23
                + var24 + var25 + var26 + var27 + var28 + var29 + var30 + var31 + var32 + var33 + var34
                + var35 + var36 + var37 + var38 + var39 + var40 + var41 + var42 + var43 + var44 + var45
                + var46 + var47 + var48 + var49 + var50 + var51 + var52 + var53 + var54 + var55 + var56
                + var57 + var58 + var59 + var60 + var61 + var62 + var63 + var64 + var65 + var66 + var67
                + var68 + var69 + var70 + var71 + var72 + var73 + var74 + var75 + var76 + var77 + var78
                + var79 + var80 + var81 + var82 + var83 + var84 + var85 + var86 + var87 + var88 + var89
                + var90 + var91 + var92 + var93 + var94 + var95 + var96;
            sr.WriteLine(rowString);

            return sr;
        }

        private void DownLoadFile()
        {
            string FileNameOf = FileName;
            FileInfo myFile = new FileInfo(FileNameOf);

            Response.ClearHeaders();
            Response.Expires = 0;
            Response.Buffer = true;
            Response.Clear();
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + myFile.Name.Replace(".resources", ""));
            Response.AppendHeader("Content-Length", myFile.Length.ToString());
            Response.ContentType = "Text/TXT"; //= "application/octet-stream";

            Response.Flush();
            Response.WriteFile(myFile.FullName);
            Response.Flush();

            Response.Clear();
            Response.End();
        }

        private void PrintVSCReport()
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
            DataTable dt = (DataTable)Session["VSCProductionFile"];
            DataDynamics.ActiveReports.ActiveReport3 rpt = null;

            //string dateType = "E"; //EntryDate
            string mHead = "";
            string CompanyHead = "";

            mHead = "Premium written & Cancellations - Entry Date Criteria";

            //dt = appAutoreport.AutoGuardPremiumWritten(txtBegDate.Text, TxtEndDate.Text, "", dateType, "1", userID);

            if (dt.Rows.Count != 0)
            {
                CompanyHead = dt.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();
            }

            rpt = new EPolicy2.Reports.AutoGuard.AutoGuardPremiumWritten(txtBegDate.Text, TxtEndDate.Text, mHead, ChkSummary.Checked, CompanyHead);

            rpt.DataSource = dt;
            rpt.DataMember = "Report";

            rpt.Document.Printer.PrinterName = "";

            rpt.Run(false);

            Session.Add("Report", rpt);
            Session.Add("FromPage", "PoliciesReports.aspx");
            Response.Redirect("ActiveXViewer.aspx", true);
        }

        private void InterfaceOPP()
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                DataTable dt = null;

                if (ddlPolicyClass.SelectedValue.Trim() == "3") //Se corre dentro de GAP
                {
                    //AutoMasterInterfaceProcess(txtBegDate.Text, TxtEndDate.Text);
                    //dt = AutoMasterInterfaceProcessData(txtBegDate.Text, TxtEndDate.Text);
                }

                if (ddlPolicyClass.SelectedValue.Trim() == "18")
                {
                    DwellingInterfaceProcess(txtBegDate.Text, TxtEndDate.Text);
                    dt = DwellingInterfaceProcessData(txtBegDate.Text, TxtEndDate.Text);
                }

                if (ddlPolicyClass.SelectedValue.Trim() == "19")
                {
                    RoadAssistInterfaceProcess(txtBegDate.Text, TxtEndDate.Text);
                    dt = RoadAssistInterfaceProcessData(txtBegDate.Text, TxtEndDate.Text);
                }

                if (ddlPolicyClass.SelectedValue.Trim() == "20")
                {
                    OPLInterfaceProcess(txtBegDate.Text, TxtEndDate.Text);
                    dt = OPLInterfaceProcessData(txtBegDate.Text, TxtEndDate.Text);
                }

                if (ddlPolicyClass.SelectedValue.Trim() == "12")
                {
                    OPPInterfaceProcess(txtBegDate.Text, TxtEndDate.Text);
                    dt = OPPInterfaceProcessData(txtBegDate.Text, TxtEndDate.Text);
                }

                DataDynamics.ActiveReports.ActiveReport3 rptOSO = null;
                if (ddlPolicyClass.SelectedValue.Trim() == "9")
                {
                    GAPInterfaceProcess(txtBegDate.Text, TxtEndDate.Text);
                    dt = GAPInterfaceProcessData(txtBegDate.Text, TxtEndDate.Text);

                    //Solo Para OSO (Corre en la misma Interface que Gap).
                    rptOSO = SetOSODecPageInPDF(dt);
                    rptOSO = SetOMPDecPageInPDF(dt, rptOSO);
                    rptOSO = SetGapDecPageInPDF(dt, rptOSO);
                }

                
                DataDynamics.ActiveReports.ActiveReport3 rpt = null;

                rpt = new EPolicy2.Reports.OPPInterfaceList();

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("There is no existing information for this report");
                }

                rpt.DataSource = dt;
                rpt.DataMember = "Report";
                rpt.Document.Printer.PrinterName = "";
                rpt.Run(false);

                if (ddlPolicyClass.SelectedValue.Trim() == "9" && rptOSO!=null)
                    rpt.Document.Pages.InsertRange(rpt.Document.Pages.Count, rptOSO.Document.Pages);

                Session.Add("Report", rpt);
                Session.Add("FromPage", "PoliciesReports.aspx");
                Response.Redirect("ActiveXViewer.aspx", false);
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }

        private DataDynamics.ActiveReports.ActiveReport3 SetOSODecPageInPDF(DataTable dt)
        {
            DataDynamics.ActiveReports.ActiveReport3 rpt1=null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Symbol"].ToString().Trim() == "OSO")
                {
                    TaskControl.Policies taskControl = TaskControl.Policies.GetPolicies((int)dt.Rows[i]["ControlID"]);
                    TaskControl.TaskControl taskControl2 = TaskControl.TaskControl.GetTaskControlByTaskControlID((int)dt.Rows[i]["ControlID"],1);
                    taskControl.Agent = taskControl2.Agent;
                    taskControl.Customer = taskControl2.Customer;
                    TaskControl.CompulsoryInsuranceQuote tc = null;

                    LookupTables.Agency agency = new LookupTables.Agency();

                    agency = agency.GetAgency(taskControl2.Agency);
                    string AgencyAdd3 = agency.agy_city.ToString() + " " + agency.agy_st.ToString() + ", " + agency.agy_zip.ToString();

                    //throw new Exception(AgencyAdd3 + " " + taskControl.Agent + " " + taskControl.VehicleMakeID.ToString() + " " + taskControl.VehicleModelID.ToString());
                    //throw new Exception(taskControl.CommercialUse.ToString());
                    if (taskControl.CommercialUse == false)
                    {
                        if (rpt1 == null)
                        {
                            rpt1 = new OPPReport.OSO.DecPageOSO(taskControl, LookupTables.LookupTables.GetDescription("VehicleMake", taskControl.VehicleMakeID.ToString()),
                                LookupTables.LookupTables.GetDescription("VehicleModel", taskControl.VehicleModelID.ToString()),
                                LookupTables.LookupTables.GetDescription("VehicleYear", taskControl.VehicleModelID.ToString()),
                                agency.AgencyDesc.ToString(), agency.agy_addr1.ToString(), agency.agy_addr2.ToString(), AgencyAdd3);
                            rpt1.Document.Printer.PrinterName = "";
                            rpt1.Run(false);
                        }
                        else
                        {
                            DataDynamics.ActiveReports.ActiveReport3 rpt2 = new OPPReport.OSO.DecPageOSO(taskControl, LookupTables.LookupTables.GetDescription("VehicleMake", taskControl.VehicleMakeID.ToString()),
                                LookupTables.LookupTables.GetDescription("VehicleModel", taskControl.VehicleModelID.ToString()),
                                LookupTables.LookupTables.GetDescription("VehicleYear", taskControl.VehicleModelID.ToString()),
                                agency.AgencyDesc.ToString(), agency.agy_addr1.ToString(), agency.agy_addr2.ToString(), AgencyAdd3);
                            rpt2.Document.Printer.PrinterName = "";
                            rpt2.Run(false);
                            rpt1.Document.Pages.InsertRange(rpt1.Document.Pages.Count, rpt2.Document.Pages);
                        }                        
                    }
                    else
                    {
                        if (rpt1 == null)
                        {
                            rpt1 = new OPPReport.OSO.DecPageOSOCommercial(taskControl, agency.AgencyDesc.ToString(), agency.agy_addr1.ToString(), agency.agy_addr2.ToString(), AgencyAdd3);
                            rpt1.Document.Printer.PrinterName = "";
                            rpt1.Run(false);

                        }
                        else
                        {
                            DataDynamics.ActiveReports.ActiveReport3 rpt2 = new OPPReport.OSO.DecPageOSOCommercial(taskControl, agency.AgencyDesc.ToString(), agency.agy_addr1.ToString(), agency.agy_addr2.ToString(), AgencyAdd3);
                            rpt2.Document.Printer.PrinterName = "";
                            rpt2.Run(false);
                            rpt1.Document.Pages.InsertRange(rpt1.Document.Pages.Count, rpt2.Document.Pages);
                        }

                        DataDynamics.ActiveReports.ActiveReport3 rpt3 = new OPPReport.OSO.DecPageOSOCommercial2(taskControl, agency.AgencyDesc.ToString(), agency.agy_addr1.ToString(), agency.agy_addr2.ToString(), AgencyAdd3);
                        rpt3.Document.Printer.PrinterName = "";
                        rpt3.Run(false);

                        rpt1.Document.Pages.InsertRange(rpt1.Document.Pages.Count, rpt3.Document.Pages);
                    }
                }
            }

            return rpt1;
        }

        //AutoMaster
        private DataDynamics.ActiveReports.ActiveReport3 SetOMPDecPageInPDF(DataTable dt,DataDynamics.ActiveReports.ActiveReport3 rptOSO)
        {
            DataDynamics.ActiveReports.ActiveReport3 rpt1 = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Symbol"].ToString().Trim() != "OSO" && dt.Rows[i]["Symbol"].ToString().Trim() != "GAP")
                {
                    TaskControl.TaskControl taskControl2 = TaskControl.TaskControl.GetTaskControlByTaskControlID((int)dt.Rows[i]["ControlID"], 1);
                    TaskControl.QuoteAuto QA = (TaskControl.QuoteAuto) taskControl2;
                    EPolicy.TaskControl.Policy taskControl3;

                    taskControl3 = (EPolicy.TaskControl.Policy)QA.Policy;
                    EPolicy.TaskControl.QuoteAuto taskControl4 = (EPolicy.TaskControl.QuoteAuto)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl3.TaskControlID, 1);
                    taskControl3 = taskControl4.Policy;

                    taskControl4.Calculate();
                    taskControl4.Charge = decimal.Parse(taskControl3.Charge.ToString());                  

                    DataDynamics.ActiveReports.ActiveReport3 rpt2;

                    if (rpt1 == null)
                    {
                        if (taskControl3.Term > 12)
                            rpt1 = new EPolicy2.Reports.AutoMasterPolicyDI(taskControl4, "ORIGINAL");
                        else
                            rpt1 = new EPolicy2.Reports.AutoMasterPolicy(taskControl4, "ORIGINAL");

                        rpt1.Document.Printer.PrinterName = "";
                        rpt1.Run(false);

                        if (rptOSO == null)
                            rptOSO = rpt1;
                        else
                        {
                            rptOSO.Document.Pages.InsertRange(rptOSO.Document.Pages.Count, rpt1.Document.Pages);
                        }
                    }
                    else
                    {
                        if (taskControl3.Term > 12)
                            rpt2 = new EPolicy2.Reports.AutoMasterPolicyDI(taskControl4, "ORIGINAL");
                        else
                            rpt2 = new EPolicy2.Reports.AutoMasterPolicy(taskControl4, "ORIGINAL");

                        rpt2.Document.Printer.PrinterName = "";
                        rpt2.Run(false);
                        rptOSO.Document.Pages.InsertRange(rptOSO.Document.Pages.Count, rpt2.Document.Pages);
                    }
                }
            }

            return rptOSO;
        }

        //SetGapDecPageInPDF
        private DataDynamics.ActiveReports.ActiveReport3 SetGapDecPageInPDF(DataTable dt, DataDynamics.ActiveReports.ActiveReport3 rptOSO)
        {
            DataDynamics.ActiveReports.ActiveReport3 rpt1 = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Symbol"].ToString().Trim() != "OSO" && dt.Rows[i]["Symbol"].ToString().Trim().Substring(0,1) != "M")
                {

                    TaskControl.Policies taskControl = TaskControl.Policies.GetPolicies((int)dt.Rows[i]["ControlID"]);
                    TaskControl.TaskControl taskControl2 = TaskControl.TaskControl.GetTaskControlByTaskControlID((int)dt.Rows[i]["ControlID"], 1);
                    taskControl.Agent = taskControl2.Agent;
                    taskControl.Agency = taskControl2.Agency;
                    taskControl.Customer = taskControl2.Customer;

                    DataDynamics.ActiveReports.ActiveReport3 rpt2;

                    if (rpt1 == null)
                    {
                        if (taskControl.TotalPremium == 450.00)
                            rpt1 = new EPolicy2.Reports.AutoGapCertificate(taskControl, "ORIGINAL");
                        else
                            rpt1 = new EPolicy2.Reports.AutoGapDeclaration(taskControl, "ORIGINAL");

                        rpt1.Document.Printer.PrinterName = "";
                        rpt1.Run(false);

                        if (rptOSO == null)
                            rptOSO = rpt1;
                        else
                        {
                            rptOSO.Document.Pages.InsertRange(rptOSO.Document.Pages.Count, rpt1.Document.Pages);
                        }
                    }
                    else
                    {
                        if (taskControl.TotalPremium == 450.00)
                            rpt2 = new EPolicy2.Reports.AutoGapCertificate(taskControl, "ORIGINAL");
                        else
                            rpt2 = new EPolicy2.Reports.AutoGapDeclaration(taskControl, "ORIGINAL");

                        rpt2.Document.Printer.PrinterName = "";
                        rpt2.Run(false);
                        rptOSO.Document.Pages.InsertRange(rptOSO.Document.Pages.Count, rpt2.Document.Pages);
                    }
                }
            }

            return rptOSO;
        }

        private void EtchBill(string PolicyClassID)
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
                DataTable dt = null;
                DataDynamics.ActiveReports.ActiveReport3 rpt = null;

                string dateType = ddlDateType.SelectedItem.Value.Trim();
                string mHead = "";
                string CompanyHead = "";

                if (ddlDateType.SelectedItem.Value.Trim() == "E")
                    mHead = "ETCH Bill - Entry Date Criteria";
                else
                    mHead = "ETCH Bill - Effective Date Criteria";


                dt = appAutoreport.ETCHBill(txtBegDate.Text, TxtEndDate.Text, ddlDealer.SelectedItem.Value.Trim(), dateType, "13", userID);

                if (chkVscFile.Checked)
                {
                    CreateEtchBillExcellFile(dt);
                }
                else
                {
                    if (dt.Rows.Count != 0)
                    {
                        CompanyHead = dt.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();
                    }
                    else
                    {
                        CompanyHead = "";
                    }
                }

                rpt = new EPolicy2.Reports.AutoGuard.EtchBill(txtBegDate.Text, TxtEndDate.Text, mHead, ChkSummary.Checked, CompanyHead);

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("There is no existing information for this report");
                }

                rpt.DataSource = dt;
                rpt.DataMember = "Report";

                rpt.Document.Printer.PrinterName = "";

                rpt.Run(false);

                Session.Add("Report", rpt);
                Session.Add("FromPage", "PoliciesReports.aspx");
                Response.Redirect("ActiveXViewer.aspx", false);
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }

        private void VSCTotalPriceBreakdown(string PolicyClassID)
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
                DataTable dt = null;
                DataDynamics.ActiveReports.ActiveReport3 rpt = null;

                string dateType = ddlDateType.SelectedItem.Value.Trim();
                string mHead = "";
                string CompanyHead = "";

                if (ddlDateType.SelectedItem.Value.Trim() == "E")
                    mHead = "Premium written & Cancellations - Entry Date Criteria";
                else
                    mHead = "Premium written & Cancellations - Effective Date Criteria";

                dt = appAutoreport.VSCTotalPriceBreakdown(txtBegDate.Text, TxtEndDate.Text, ddlDealer.SelectedItem.Value.Trim(), dateType, PolicyClassID, userID, ddlCancellationMethod.SelectedItem.Value.Trim(), ddlInsuranceCompany.SelectedItem.Value.Trim());

                if (chkVscFile.Checked)
                {
                    CreateVSCExcellFile(dt);
                }
                else
                {
                    if (dt.Rows.Count != 0)
                    {
                        CompanyHead = dt.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();
                    }
                    else
                    {
                        CompanyHead = "";
                    }

                    if (ChkAverages.Checked)
                        rpt = new EPolicy2.Reports.AutoGuard.VSCBreakdownAVG(txtBegDate.Text, TxtEndDate.Text, mHead, true, CompanyHead);
                    else
                        rpt = new EPolicy2.Reports.AutoGuard.VSCBreakdown(txtBegDate.Text, TxtEndDate.Text, mHead, ChkSummary.Checked, CompanyHead);

                    if (dt.Rows.Count == 0)
                    {
                        throw new Exception("There is no existing information for this report");
                    }

                    rpt.DataSource = dt;
                    rpt.DataMember = "Report";

                    rpt.Document.Printer.PrinterName = "";

                    rpt.Run(false);

                    Session.Add("Report", rpt);
                    Session.Add("FromPage", "PoliciesReports.aspx");
                    Response.Redirect("ActiveXViewer.aspx", false);
                }
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }

        private void CreateVSCExcellFile(DataTable dt)
        {

            StreamWriter sr;

            FileName = @"C:\Inetpub\Optima Page\Optima\VSC\ImagingDataFile\IM" +

            DateTime.Now.Month.ToString().Trim() +

            DateTime.Now.Day.ToString().Trim() +

            DateTime.Now.Year.ToString().Trim() + ".txt";

            //FileName = @"C:\Inetpub\wwwroot\Optima\VSC\ImagingDataFile\VSCBreakDown" +

            //DateTime.Now.Month.ToString().Trim() +

            //DateTime.Now.Day.ToString().Trim() +

            // DateTime.Now.Year.ToString().Trim() + ".txt";

            sr = File.CreateText(FileName);

            string[] array2 = new string[43];

            array2 = SetHeader(array2);

            sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +

            array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +

            array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +

            array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() +

            array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +

            array2[25].ToString() + array2[26].ToString() + array2[27].ToString() + 
            
            array2[35].ToString() + array2[36].ToString() +

            array2[37].ToString() + array2[38].ToString() + array2[39].ToString() +

            array2[40].ToString() + array2[41].ToString() + array2[42].ToString());

            //array2[28].ToString()+ array2[29].ToString()+ array2[30].ToString()+ array2[31].ToString()+

            //array2[32].ToString()+ array2[33].ToString() + array2[34].ToString() +



            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (double.Parse(dt.Rows[i]["TotalPremium"].ToString()) != 0.0)
                {
                    array2[0] = dt.Rows[i]["TaskControlID"].ToString() + ",";

                    array2[1] = dt.Rows[i]["PolicyNumber"].ToString().Replace(',', ' ') + ",";

                    array2[2] = dt.Rows[i]["Status"].ToString() + ",";

                    array2[3] = dt.Rows[i]["Sufijo"].ToString() + ",";

                    array2[4] = dt.Rows[i]["CustomerName"].ToString().Replace(',', ' ') + ',';

                    array2[5] = ((DateTime)dt.Rows[i]["EntryDate"]).ToShortDateString() + ",";

                    array2[6] = ((DateTime)dt.Rows[i]["EffectiveDate"]).ToShortDateString() + ",";

                    array2[7] = dt.Rows[i]["CompanyDealerDesc"].ToString().Replace(',', ' ') + ",";

                    array2[8] = dt.Rows[i]["BankDesc"].ToString() + ",";

                    array2[9] = dt.Rows[i]["CoveragePlanDesc"].ToString() + ",";

                    array2[10] = dt.Rows[i]["Term"].ToString() + ",";

                    array2[11] = dt.Rows[i]["Miles"].ToString() + ",";

                    array2[12] = dt.Rows[i]["Milleages"].ToString() + ",";

                    array2[13] = dt.Rows[i]["VehicleMakeDesc"].ToString() + ",";

                    array2[14] = dt.Rows[i]["VehicleModelDesc"].ToString() + ",";

                    array2[15] = dt.Rows[i]["VehicleYearDesc"].ToString() + ",";
               
                    array2[16] = dt.Rows[i]["VIN"].ToString() + ",";

                    array2[17] = dt.Rows[i]["Pending"].ToString() + ",";

                    array2[18] = dt.Rows[i]["Charge"].ToString() + ",";

                    array2[19] = dt.Rows[i]["LossFund"].ToString() + ",";

                    array2[20] = dt.Rows[i]["OverHead"].ToString() + ",";

                    array2[21] = dt.Rows[i]["BankFee"].ToString() + ",";

                    array2[22] = dt.Rows[i]["Profit"].ToString() + ",";

                    array2[23] = dt.Rows[i]["CanReserve"].ToString() + ",";

                    array2[24] = dt.Rows[i]["Concurso"].ToString() + ",";

                    array2[25] = dt.Rows[i]["DealerCost"].ToString() + ",";

                    array2[26] = dt.Rows[i]["DealerProfit"].ToString() + ",";

                    array2[27] = dt.Rows[i]["TotalPremium"].ToString() + ",";

                    //array2[28] = dt.Rows[i]["CancLossFund"].ToString() + ",";

                    //array2[29] = dt.Rows[i]["CancOverHead"].ToString() + ",";

                    //array2[30] = dt.Rows[i]["CancBankFee"].ToString() + ",";

                    //array2[31] = dt.Rows[i]["CancProfit"].ToString() + ",";

                    //array2[32] = dt.Rows[i]["CancConcurso"].ToString() + ",";

                    //array2[33] = dt.Rows[i]["CancDealerProfit"].ToString() + ",";

                    //array2[34] = dt.Rows[i]["CancCanReserve"].ToString() + ",";

                    array2[35] = ((dt.Rows[i]["CancellationDate"] != System.DBNull.Value) ? ((DateTime)dt.Rows[i]["CancellationDate"]).ToShortDateString() : "") + ",";

                    array2[36] = ((dt.Rows[i]["CancellationEntryDate"] != System.DBNull.Value) ? ((DateTime)dt.Rows[i]["CancellationEntryDate"]).ToShortDateString() : "") + ","; 

                    array2[37] = ((DateTime)dt.Rows[i]["ExpirationDate"]).ToShortDateString() + ","; 

                    array2[38] = ((dt.Rows[i]["CancellationAmount"] != System.DBNull.Value) ? (((double)dt.Rows[i]["CancellationAmount"]).ToString()) : "0") + ","; 

                    array2[39] = dt.Rows[i]["PremNet"].ToString();

                    //array2[40] = dt.Rows[i]["CancWarranty"].ToString();

                    //array2[41] = dt.Rows[i]["CancInitialMileage"].ToString();

                    //array2[42] = dt.Rows[i]["CancMileage"].ToString();

                    sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +

                    array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +

                    array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +

                    array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() +

                    array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +

                    array2[25].ToString() + array2[26].ToString() + array2[27].ToString() + array2[35].ToString() + array2[36].ToString() +

                    array2[37].ToString() + array2[38].ToString() + array2[39].ToString());

                  //  array2[40].ToString() + array2[41].ToString() + array2[42].ToString());

                    //+ array2[28].ToString() + array2[29].ToString() + array2[30].ToString() + array2[31].ToString() +

                    //array2[32].ToString() + array2[33].ToString() + array2[34].ToString()
                }
            

                //Segunda linea para cancelaciones
                if ((int.Parse(dt.Rows[i]["CancellationMethod"].ToString()) != 0 && int.Parse(dt.Rows[i]["CancellationMethod"].ToString()) != 99) && (DateTime.Parse(dt.Rows[i]["CancellationEntryDate"].ToString()) <= DateTime.Parse(this.TxtEndDate.Text).AddDays(1)))
                {
                array2[0] = dt.Rows[i]["TaskControlID"].ToString() + ",";
                array2[1] = dt.Rows[i]["PolicyNumber"].ToString() + ",";
                array2[2] = dt.Rows[i]["Status"].ToString() + ",";
                array2[3] = dt.Rows[i]["Sufijo"].ToString() + ",";
                array2[4] = dt.Rows[i]["CustomerName"].ToString().Replace(',', ' ') + ",";
                array2[5] = ((DateTime)dt.Rows[i]["EntryDate"]).ToShortDateString() + ",";
                array2[6] = ((DateTime)dt.Rows[i]["EffectiveDate"]).ToShortDateString() + ",";
                array2[7] = dt.Rows[i]["CompanyDealerDesc"].ToString().Replace(',', ' ') + ",";
                array2[8] = dt.Rows[i]["BankDesc"].ToString().Replace(',', ' ') + ',';
                array2[9] = dt.Rows[i]["CoveragePlanDesc"].ToString() + ",";
                array2[10] = dt.Rows[i]["Term"].ToString() + ","; 
                // TotalPremium2 = Para efectos del archivo.
                    
                    //(double.Parse(dt.Rows[i]["TotalPremium"].ToString())):
                    //(double.Parse(dt.Rows[i]["LossFund"].ToString()) + 
                    //double.Parse(dt.Rows[i]["OverHead"].ToString()) + 
                    //double.Parse(dt.Rows[i]["BankFee"].ToString()) + 
                    //double.Parse(dt.Rows[i]["Profit"].ToString()) + 
                    //double.Parse(dt.Rows[i]["CanReserve"].ToString()) +
                    //double.Parse(dt.Rows[i]["Concurso"].ToString()))) + ","; 

                array2[11] = dt.Rows[i]["Miles"].ToString() + ",";
                array2[12] = dt.Rows[i]["Milleages"].ToString() + ",";
                array2[13] = dt.Rows[i]["VehicleMakeDesc"].ToString() + ",";
                array2[14] = dt.Rows[i]["VehicleModelDesc"].ToString() + ","; 
                array2[15] = dt.Rows[i]["VehicleYearDesc"].ToString() + ",";
                array2[16] = dt.Rows[i]["VIN"].ToString() + ",";
                array2[17] = dt.Rows[i]["Pending"].ToString() + ",";
                array2[18] = ((DateTime.Parse(dt.Rows[i]["CancellationEntryDate"].ToString()) > DateTime.Parse(this.TxtEndDate.Text)) ? "0" : "-" + (dt.Rows[i]["Charge"].ToString())) + ",";
                   
                    //dt.Rows[i]["Charge"].ToString() + ",";
                array2[19] = dt.Rows[i]["CancLossFund"].ToString() + ",";
                array2[20] = dt.Rows[i]["CancOverHead"].ToString() + ",";
                array2[21] = dt.Rows[i]["CancBankFee"].ToString() + ",";
                array2[22] = dt.Rows[i]["CancProfit"].ToString() + ",";
                array2[23] = dt.Rows[i]["CancCanReserve"].ToString() + ",";
                array2[24] = dt.Rows[i]["CancConcurso"].ToString() + ",";
                array2[25] = double.Parse(dt.Rows[i]["CancLossFund"].ToString()) + double.Parse(dt.Rows[i]["CancOverHead"].ToString()) + double.Parse(dt.Rows[i]["CancBankFee"].ToString()) + double.Parse(dt.Rows[i]["CancProfit"].ToString()) + double.Parse(dt.Rows[i]["CancCanReserve"].ToString()) + double.Parse(dt.Rows[i]["CancConcurso"].ToString()) + ","; //"-"+dt.Rows[i]["DealerCost"].ToString() + ",";
                array2[26] = dt.Rows[i]["CancDealerProfit"].ToString() + ",";
                array2[27] = (double.Parse(dt.Rows[i]["CancLossFund"].ToString()) + double.Parse(dt.Rows[i]["CancOverHead"].ToString()) + double.Parse(dt.Rows[i]["CancBankFee"].ToString()) + double.Parse(dt.Rows[i]["CancProfit"].ToString()) + double.Parse(dt.Rows[i]["CancCanReserve"].ToString()) + double.Parse(dt.Rows[i]["CancConcurso"].ToString())) + 
                              double.Parse(dt.Rows[i]["CancDealerProfit"].ToString()) + ","; //(double.Parse(dt.Rows[i]["TotalPremium2"].ToString())) + ",";
                //array2[28] = ",";
                //array2[29] = ",";
                //array2[30] = ",";
                //array2[31] = ",";
                //array2[32] = ",";
                //array2[33] = ",";
                //array2[34] = ",";
                array2[35] = ((dt.Rows[i]["CancellationDate"] != System.DBNull.Value) ? ((DateTime)dt.Rows[i]["CancellationDate"]).ToShortDateString() : "") + ",";
                array2[36] = ((dt.Rows[i]["CancellationEntryDate"] != System.DBNull.Value) ? ((DateTime)dt.Rows[i]["CancellationEntryDate"]).ToShortDateString() : "") + ","; 
                array2[37] = ((DateTime)dt.Rows[i]["ExpirationDate"]).ToShortDateString() + ","; 
                array2[38] = ((dt.Rows[i]["CancellationAmount"] != System.DBNull.Value) ? (("-" + (double)dt.Rows[i]["CancellationAmount"]).ToString()) : "0") + ",";
                array2[39] = dt.Rows[i]["PremNet"].ToString() + ","; ;
                array2[40] = dt.Rows[i]["CancWarranty"].ToString() + ","; ;
                array2[41] = dt.Rows[i]["CancInitialMileage"].ToString() + ","; ;
                array2[42] = dt.Rows[i]["CancMileage"].ToString();

               sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +

                array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +

                array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +

                array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() +

                array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +

                array2[25].ToString() + array2[26].ToString() + array2[27].ToString() + array2[35].ToString() + array2[36].ToString() +

                array2[37].ToString() + array2[38].ToString() + array2[39].ToString() +

                array2[40].ToString() + array2[41].ToString() + array2[42].ToString());

                //array2[26].ToString() + array2[27].ToString() + array2[28].ToString() + array2[29].ToString() +

                //array2[30].ToString() + array2[31].ToString() + array2[32].ToString()
                }



            }

            sr.Close();

            DownLoadFile();

            //// make sure nothing is in response stream 

            //response.Clear();

            //response.Charset = "";

            //// set MIME type to be Excel file. 

            //response.ContentType = "application/vnd.ms-excel";

            //// add a header to response to force download (specifying filename) 

            ////response.AddHeader("Content-Disposition", "attachment; filename=\"MyFile.xls\"");

            //response.AddHeader("Content-Disposition", "attachment; filename=\"MyFile.xls\"");



            //// Send the data. Tab delimited, with newlines. 

            //response.Write("Col1\tCol2\tCol3\tCol4\n");

            //response.Write("Data 1\tData 2\tData 3\tData 4\n");

            //response.Write("Data 1\tData 2\tData 3\tData 4\n");

            //response.Write("Data 1\tData 2\tData 3\tData 4\n");

            //response.Write("Data 1\tData 2\tData 3\tData 4\n");

            //// Close response stream. 

            //response.End(); 

        }
                
        private string[] SetHeader(string[] array2)
        {

            array2[0] = "TaskControlID,";

            array2[1] = "ContractNo,";

            array2[2] = "Status,";

            array2[3] = "Suffix,";

            array2[4] = "CustomerName,";

            array2[5] = "EntryDate,";

            array2[6] = "EffectiveDate,";

            array2[7] = "Dealer,";

            array2[8] = "Bank,";

            array2[9] = "Plan,";

            array2[10] = "Term,";

            array2[11] = "Miles,";

            array2[12] = "Milleages,";

            array2[13] = "Make,";

            array2[14] = "Model,"; 

            array2[15] = "Year,";

            array2[16] = "VIN,";

            array2[17] = "Pending,";

            array2[18] = "Deductible,";

            array2[19] = "LossFund,";

            array2[20] = "OverHead,";

            array2[21] = "BankFee,";

            array2[22] = "Profit,";

            array2[23] = "CanReserve,";

            array2[24] = "Concurso,";

            array2[25] = "DealerCost,"; 

            array2[26] = "DealerProfit,";

            array2[27] = "TotalPremium,"; 

            array2[28] = "CancLossFund,";

            array2[29] = "CancOverHead,";

            array2[30] = "CancBankFee,";

            array2[31] = "CancProfit,";

            array2[32] = "CancConcurso,";

            array2[33] = "CancDealerProfit,";

            array2[34] = "CancCanReserve,";

            array2[35] = "CancellationDate,";

            array2[36] = "CancellationEntryDate,"; 

            array2[37] = "ExpirationDate,"; 

            array2[38] = "CancellationAmount,"; 

            array2[39] = "PremiumNet,";

            array2[40] = "CancWarranty,";

            array2[41] = "CancInitialMileage,";

            array2[42] = "CancMileage";

            return array2;

        }

        private void CreateEtchExcellFile(DataTable dt)
        {

            StreamWriter sr;

            FileName = @"C:\Inetpub\Optima Page\Optima\VSC\ImagingDataFile\IM" +

            DateTime.Now.Month.ToString().Trim() +

            DateTime.Now.Day.ToString().Trim() +

            DateTime.Now.Year.ToString().Trim() + ".txt";

            sr = File.CreateText(FileName);

            string[] array2 = new string[29];

            array2 = SetHeaderEtch(array2);

            sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +

            array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +

            array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +

            array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() + 
            
            //array2[19].ToString() + 

            //array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +

            array2[26].ToString() + array2[27].ToString() + array2[28].ToString());


            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (double.Parse(dt.Rows[i]["TotalPremium"].ToString()) != 0.0)
                {
                    array2[0] = dt.Rows[i]["TaskControlID"].ToString() + ",";

                    array2[1] = dt.Rows[i]["PolicyNumber"].ToString().Replace(',', ' ') + ",";

                    array2[2] = dt.Rows[i]["Status"].ToString() + ",";

                    array2[3] = dt.Rows[i]["CustomerName"].ToString().Replace(',', ' ') + ',';

                    array2[4] = ((DateTime)dt.Rows[i]["EntryDate"]).ToShortDateString() + ",";

                    array2[5] = ((DateTime)dt.Rows[i]["EffectiveDate"]).ToShortDateString() + ",";

                    array2[6] = dt.Rows[i]["CompanyDealerDesc"].ToString().Replace(',', ' ') + ",";

                    array2[7] = dt.Rows[i]["BankDesc"].ToString() + ",";

                    array2[8] = dt.Rows[i]["Term"].ToString() + ",";

                    array2[9] = dt.Rows[i]["Make"].ToString() + ",";

                    array2[10] = dt.Rows[i]["Model"].ToString() + ",";

                    array2[11] = dt.Rows[i]["Yr"].ToString() + ",";

                    array2[12] = dt.Rows[i]["VIN"].ToString() + ",";

                    array2[13] = dt.Rows[i]["Pending"].ToString() + ",";

                    array2[14] = dt.Rows[i]["LossFund"].ToString() + ",";

                    array2[15] = dt.Rows[i]["Profit"].ToString() + ",";

                    array2[16] = dt.Rows[i]["DealerPayable"].ToString() + ",";

                    array2[17] = dt.Rows[i]["DealerCost"].ToString() + ",";

                    array2[18] = dt.Rows[i]["DealerProfit"].ToString() + ",";

                    array2[19] = dt.Rows[i]["TotalPremium"].ToString() + ",";

                    //array2[19] = dt.Rows[i]["CancLossFund"].ToString() + ",";

                    //array2[20] = dt.Rows[i]["CancProfit"].ToString() + ",";

                    //array2[21] = dt.Rows[i]["CancDealerPayable"].ToString() + ",";

                    //array2[22] = dt.Rows[i]["CancDealerCost"].ToString() + ",";

                    //array2[23] = dt.Rows[i]["CancDealerProfit"].ToString() + ",";

                    //array2[24] = dt.Rows[i]["CancPremium"].ToString() + ",";

                    array2[26] = ((dt.Rows[i]["CancellationDate"] != System.DBNull.Value) ? ((DateTime)dt.Rows[i]["CancellationDate"]).ToShortDateString() : "") + ",";

                    array2[27] = ((dt.Rows[i]["CancellationEntryDate"] != System.DBNull.Value) ? ((DateTime)dt.Rows[i]["CancellationEntryDate"]).ToShortDateString() : "") + ",";

                    array2[28] = ((DateTime)dt.Rows[i]["ExpirationDate"]).ToShortDateString() + ","; 

                    sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +

                    array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +

                    array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +

                    array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() + 
                    
                    //array2[19].ToString() +

                    //array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +

                    array2[26].ToString() + array2[27].ToString() + array2[28].ToString());

                }


                //Segunda linea para cancelaciones
                if ((int.Parse(dt.Rows[i]["CancellationMethod"].ToString()) != 0 && int.Parse(dt.Rows[i]["CancellationMethod"].ToString()) != 99) && (DateTime.Parse(dt.Rows[i]["CancellationEntryDate"].ToString()) <= DateTime.Parse(this.TxtEndDate.Text).AddDays(1)))
                {
                    array2[0] = dt.Rows[i]["TaskControlID"].ToString() + ",";
                    array2[1] = dt.Rows[i]["PolicyNumber"].ToString() + ",";
                    array2[2] = dt.Rows[i]["Status"].ToString() + ",";
                    array2[3] = dt.Rows[i]["CustomerName"].ToString().Replace(',', ' ') + ",";
                    array2[4] = ((DateTime)dt.Rows[i]["EntryDate"]).ToShortDateString() + ",";
                    array2[5] = ((DateTime)dt.Rows[i]["EffectiveDate"]).ToShortDateString() + ",";
                    array2[6] = dt.Rows[i]["CompanyDealerDesc"].ToString().Replace(',', ' ') + ",";
                    array2[7] = dt.Rows[i]["BankDesc"].ToString().Replace(',', ' ') + ',';
                    array2[8] = dt.Rows[i]["Term"].ToString() + ",";

                    array2[9] = dt.Rows[i]["Make"].ToString() + ",";
                    array2[10] = dt.Rows[i]["Model"].ToString() + ",";
                    array2[11] = dt.Rows[i]["Yr"].ToString() + ",";
                    array2[12] = dt.Rows[i]["VIN"].ToString() + ",";
                    array2[13] = dt.Rows[i]["Pending"].ToString() + ",";

                    //dt.Rows[i]["Charge"].ToString() + ",";
                    array2[14] = dt.Rows[i]["CancLossFund"].ToString() + ",";
                    array2[15] = dt.Rows[i]["CancProfit"].ToString() + ",";
                    array2[16] = dt.Rows[i]["CancDealerPayable"].ToString() + ",";
                    array2[17] = dt.Rows[i]["CancDealerCost"].ToString() + ",";
                    array2[18] = dt.Rows[i]["CancDealerProfit"].ToString() + ",";
                    array2[19] = dt.Rows[i]["CancellationAmount"].ToString() + ",";
            
                    array2[26] = ((dt.Rows[i]["CancellationDate"] != System.DBNull.Value) ? ((DateTime)dt.Rows[i]["CancellationDate"]).ToShortDateString() : "") + ",";
                    array2[27] = ((dt.Rows[i]["CancellationEntryDate"] != System.DBNull.Value) ? ((DateTime)dt.Rows[i]["CancellationEntryDate"]).ToShortDateString() : "") + ",";
                    array2[28] = ((DateTime)dt.Rows[i]["ExpirationDate"]).ToShortDateString() + ","; 

                    sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +

                     array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +

                     array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +

                     array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() + 
                     
                     //array2[19].ToString() +

                     //array2[20].ToString() + array2[21].ToString() + array2[22].ToString() + array2[23].ToString() + array2[24].ToString() +

                     array2[26].ToString() + array2[27].ToString() + array2[28].ToString());

                }
            }

            sr.Close();

            DownLoadFile();
        }

        private string[] SetHeaderEtch(string[] array2)
        {

            array2[0] = "TaskControlID,";

            array2[1] = "ContractNo,";

            array2[2] = "Status,";

            array2[3] = "CustomerName,";

            array2[4] = "EntryDate,";

            array2[5] = "EffectiveDate,";

            array2[6] = "Dealer,";

            array2[7] = "Bank,";

            array2[8] = "Term,";

            array2[9] = "Make,";

            array2[10] = "Model,";

            array2[11] = "Year,";

            array2[12] = "VIN,";

            array2[13] = "Pending,";

            array2[14] = "LossFund,";

            array2[15] = "Profit,";

            array2[16] = "DealerPayable,";

            array2[17] = "DealerCost,";

            array2[18] = "DealerProfit,";

            array2[19] = "Premium,";

            array2[20] = "CancLossFund,";

            array2[21] = "CancProfit,";

            array2[22] = "CancDealerPayable,";

            array2[23] = "CancDealerCost,";

            array2[24] = "CancDealerProfit,";

            array2[25] = "CancPremium,";

            array2[26] = "CancellationDate,";

            array2[27] = "CancellationEntryDate,";

            array2[28] = "ExpirationDate,";

            return array2;

        }

        private void CreateEtchBillExcellFile(DataTable dt)
        {

            StreamWriter sr;

            FileName = @"C:\Inetpub\Optima Page\Optima\VSC\ImagingDataFile\ET" +
            DateTime.Now.Month.ToString().Trim() +
            DateTime.Now.Day.ToString().Trim() +
            DateTime.Now.Year.ToString().Trim() + ".txt";
            sr = File.CreateText(FileName);
            string[] array2 = new string[29];
            array2 = SetHeaderEtch(array2);
            sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +
            array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +
            array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +
            array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() +
            array2[20].ToString());

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (double.Parse(dt.Rows[i]["TotalPremium"].ToString()) != 0.0)
                {
                    array2[0] = dt.Rows[i]["TaskControlID"].ToString() + ",";
                    array2[1] = dt.Rows[i]["PolicyNumber"].ToString().Replace(',', ' ') + ",";
                    array2[2] = dt.Rows[i]["Status"].ToString() + ",";
                    array2[3] = dt.Rows[i]["CustomerName"].ToString().Replace(',', ' ') + ',';
                    array2[4] = ((DateTime)dt.Rows[i]["EntryDate"]).ToShortDateString() + ",";
                    array2[5] = ((DateTime)dt.Rows[i]["EffectiveDate"]).ToShortDateString() + ",";
                    array2[6] = dt.Rows[i]["CompanyDealerDesc"].ToString().Replace(',', ' ') + ",";
                    array2[7] = dt.Rows[i]["BankDesc"].ToString() + ",";
                    array2[8] = dt.Rows[i]["Term"].ToString() + ",";
                    array2[9] = dt.Rows[i]["Make"].ToString() + ",";
                    array2[10] = dt.Rows[i]["Model"].ToString() + ",";
                    array2[11] = dt.Rows[i]["Yr"].ToString() + ",";
                    array2[12] = dt.Rows[i]["VIN"].ToString() + ",";
                    array2[13] = dt.Rows[i]["Pending"].ToString() + ",";
                    array2[14] = dt.Rows[i]["LossFund"].ToString() + ",";
                    array2[15] = dt.Rows[i]["Profit"].ToString() + ",";
                    array2[16] = dt.Rows[i]["DealerPayable"].ToString() + ",";
                    array2[17] = dt.Rows[i]["DealerCost"].ToString() + ",";
                    array2[18] = dt.Rows[i]["DealerProfit"].ToString() + ",";
                    array2[19] = dt.Rows[i]["TotalPremium"].ToString() + ",";
                    //array2[19] = dt.Rows[i]["CancLossFund"].ToString() + ",";
                    //array2[20] = dt.Rows[i]["CancProfit"].ToString() + ",";
                    //array2[21] = dt.Rows[i]["CancDealerPayable"].ToString() + ",";
                    //array2[22] = dt.Rows[i]["CancDealerCost"].ToString() + ",";
                    //array2[23] = dt.Rows[i]["CancDealerProfit"].ToString() + ",";
                    //array2[24] = dt.Rows[i]["CancPremium"].ToString() + ",";
                    //array2[26] = ((dt.Rows[i]["CancellationDate"] != System.DBNull.Value) ? ((DateTime)dt.Rows[i]["CancellationDate"]).ToShortDateString() : "") + ",";
                    //array2[27] = ((dt.Rows[i]["CancellationEntryDate"] != System.DBNull.Value) ? ((DateTime)dt.Rows[i]["CancellationEntryDate"]).ToShortDateString() : "") + ",";
                    array2[20] = ((DateTime)dt.Rows[i]["ExpirationDate"]).ToShortDateString() + ",";
                    sr.WriteLine(array2[0].ToString() + array2[1].ToString() + array2[2].ToString() + array2[3].ToString() + array2[4].ToString() +

                    array2[5].ToString() + array2[6].ToString() + array2[7].ToString() + array2[8].ToString() + array2[9].ToString() +
                    array2[10].ToString() + array2[11].ToString() + array2[12].ToString() + array2[13].ToString() + array2[14].ToString() +
                    array2[15].ToString() + array2[16].ToString() + array2[17].ToString() + array2[18].ToString() + array2[19].ToString() +
                    array2[20].ToString());
                }
            }

            sr.Close();

            DownLoadFile();
        }

        private string[] SetHeaderEtchBill(string[] array2)
        {

            array2[0] = "TaskControlID,";
            array2[1] = "ContractNo,";
            array2[2] = "Status,";
            array2[3] = "CustomerName,";
            array2[4] = "EntryDate,";
            array2[5] = "EffectiveDate,";
            array2[6] = "Dealer,";
            array2[7] = "Bank,";
            array2[8] = "Term,";
            array2[9] = "Make,";
            array2[10] = "Model,";
            array2[11] = "Year,";
            array2[12] = "VIN,";
            array2[13] = "Pending,";
            array2[14] = "LossFund,";
            array2[15] = "Profit,";
            array2[16] = "DealerPayable,";
            array2[17] = "DealerCost,";
            array2[18] = "DealerProfit,";
            array2[19] = "Premium,";
            array2[20] = "ExpirationDate,";

            return array2;

        }

        private void ETCHBreakdown()
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
                DataTable dt = null;
                DataDynamics.ActiveReports.ActiveReport3 rpt = null;

                string dateType = ddlDateType.SelectedItem.Value.Trim();
                string mHead = "";
                string CompanyHead = "";
                string InsCo = "000";

                if (ddlDateType.SelectedItem.Value.Trim() == "E")
                    mHead = "ETCH Breakdown - Entry Date Criteria";
                else
                    mHead = "ETCH Breakdown - Effective Date Criteria";
            
                if (ddlInsuranceCompany.SelectedItem.Value.Trim() != "")
                    InsCo = ddlInsuranceCompany.SelectedItem.Value.Trim();
                else
                    InsCo = "000";

                dt = appAutoreport.ETCHBreakdown(txtBegDate.Text, TxtEndDate.Text, ddlDealer.SelectedItem.Value.Trim(), dateType, "13", userID, InsCo);

                if (chkVscFile.Checked)
                {
                    CreateEtchExcellFile(dt);
                }
                else
                {
                    if (ddlInsuranceCompany.SelectedItem.Value.Trim() != "")
                        CompanyHead = ddlInsuranceCompany.SelectedItem.Text.Trim();
                    else
                        CompanyHead = "All Companies - ETCH";
                }

                rpt = new EPolicy2.Reports.AutoGuard.EtchBreakdown(txtBegDate.Text, TxtEndDate.Text, mHead, ChkSummary.Checked, CompanyHead);

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("There is no existing information for this report");
                }

                rpt.DataSource = dt;
                rpt.DataMember = "Report";

                rpt.Document.Printer.PrinterName = "";

                rpt.Run(false);

                Session.Add("Report", rpt);
                Session.Add("FromPage", "PoliciesReports.aspx");
                Response.Redirect("ActiveXViewer.aspx", false);
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }

        private void VSCProductionReport(string PolicyClassID)
        {
            try
            {
                Login.Login cp = HttpContext.Current.User as Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                EPolicy2.Reports.AutoGuardServicesContractReport appAutoreport = new EPolicy2.Reports.AutoGuardServicesContractReport();
                DataTable dt = null;
                DataDynamics.ActiveReports.ActiveReport3 rpt = null;

                string dateType = ddlDateType.SelectedItem.Value.Trim();
                string mHead = "";
                string CompanyHead = "";

                if (ddlDateType.SelectedItem.Value.Trim() == "E")
                    mHead = "Production Report - Entry Date Criteria";
                else
                    mHead = "Production Report - Effective Date Criteria";


                dt = appAutoreport.AutoGuardPremiumWrittenToDealer(txtBegDate.Text, TxtEndDate.Text, ddlDealer.SelectedItem.Value.Trim(), dateType, PolicyClassID, userID, ddlInsuranceCompany.SelectedItem.Value.Trim());


                if (dt.Rows.Count != 0)
                {
                    CompanyHead = dt.Rows[0]["InsuranceCompanyDesc"].ToString().Trim();
                }
                else
                {
                    CompanyHead = "";
                }

                rpt = new EPolicy2.Reports.AutoGuard.VSCProductionDealer(txtBegDate.Text, TxtEndDate.Text, mHead, ChkSummary.Checked, CompanyHead);

                if (dt.Rows.Count == 0)
                {
                    throw new Exception("There is no existing information for this report");
                }

                rpt.DataSource = dt;
                rpt.DataMember = "Report";

                rpt.Document.Printer.PrinterName = "";

                rpt.Run(false);

                Session.Add("Report", rpt);
                Session.Add("FromPage", "PoliciesReports.aspx");
                Response.Redirect("ActiveXViewer.aspx", false);
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }

        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                FieldVerify();

                switch (rblAutoGuardReports.SelectedItem.Value)
                {
                    case "5":
                        btnPrint.Visible = true;
                        VCSProductionFile();
                        break;

                    case "8":
                        btnPrint.Visible = false;
                        VSCSunGuard();
                        break;

                    case "9":
                        btnPrint.Visible = false;
                        VSCSunGuardDeposit();
                        break;
                    case "18":
                        btnPrint.Visible = true;
                        QCertProductionFile();
                        break;
                }
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }
        protected void BtnPrint2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["VSCProductionFile"] == null)
                {
                    throw new Exception("Please download the VSC Production File first.");
                }

                //FieldVerify();
                PrintVSCReport();
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }
        protected void ddlPolicyClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblAutoGuardReports.SelectedItem.Value == "0")
            {
                if (ddlPolicyClass.SelectedItem.Value == "1" || ddlPolicyClass.SelectedItem.Value == "13" || ddlPolicyClass.SelectedItem.Value == "17")
                {
                    ddlVSCPending.Visible = true;
                    lblPending.Visible = true;
                }
                else
                {
                    ddlVSCPending.Visible = false;
                    lblPending.Visible = false;
                }

                if (ddlPolicyClass.SelectedItem.Value == "3")
                {
                    ddlPolicyType.Visible = true;
                    lblPolicyType.Visible = true;
                }
                else
                {
                    ddlPolicyType.Visible = false;
                    lblPolicyType.Visible = false;
                }

            }
        }

        private DataTable GetVSCImagingDataFile(string BegDate,string EndDate)
        {
            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

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
               dt = exec.GetQuery("GetVSCImaging", xmlDoc);
               return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }        
        }

        private DataTable OPPInterfaceProcessData(string BegDate, string EndDate)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetOPPCPPReport", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        private DataTable OPLInterfaceProcessData(string BegDate, string EndDate)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetProfessionalLiabilityCPPReport", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        private DataTable DwellingInterfaceProcessData(string BegDate, string EndDate)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetDwellingCPPReport", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        private DataTable RoadAssistInterfaceProcessData(string BegDate, string EndDate)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetRoadAssistCPPReport", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        private DataTable AutoMasterInterfaceProcessData(string BegDate, string EndDate)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetAutoMasterCPPReport", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        private DataTable GAPInterfaceProcessData(string BegDate, string EndDate)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetGAPCPPReport", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        private void OPPInterfaceProcess(string BegDate, string EndDate)
        {
            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetOPPCPPMain", xmlDoc);
                //return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        private void OPLInterfaceProcess(string BegDate, string EndDate)
        {
            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetProfessionalLiabilityCPPMain", xmlDoc);
                //return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        private void DwellingInterfaceProcess(string BegDate, string EndDate)
        {
            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetDwellingCPPMain", xmlDoc);
                //return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        private void RoadAssistInterfaceProcess(string BegDate, string EndDate)
        {
            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetRoadAssistCPPMain", xmlDoc);
                //return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        private void AutoMasterInterfaceProcess(string BegDate, string EndDate)
        {
            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetAutoMasterCPPMain", xmlDoc);
                //return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }
        }

        private void GAPInterfaceProcess(string BegDate, string EndDate)
        {

            DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("BegDate",
            SqlDbType.VarChar, 10, BegDate.ToString(),
            ref cookItems);

            DbRequestXmlCooker.AttachCookItem("EndDate",
            SqlDbType.VarChar, 10, EndDate.ToString(),
            ref cookItems);

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
                dt = exec.GetQuery("GetGAPCPPMain", xmlDoc);
                //return dt;
            }

            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data.", ex);
            }

        }

        private void ReportPrint(bool IsEffectiveDate, string BegDate, string EndDate, string Agent, string PolicyType, string rdlcDoc, string ReportType)
        {
            try
            {
                EPolicy.TaskControl.Autos taskControl = (EPolicy.TaskControl.Autos)Session["TaskControl"];
                ReportViewer viewer = new ReportViewer();

                //EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

                if (rdlcDoc != "")
                {
                    #region Account Current Statement
                    if (rdlcDoc == "ReportCurrentStatement.rdlc")
                    {
                        ReportViewer2.LocalReport.ReportPath = ("Reports/VI/ReportCurrentStatement.rdlc");
                        ReportViewer2.LocalReport.DataSources.Clear();
                        ReportViewer2.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter
                                            ta = new GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter();

                        GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable
                            dt = new GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, "26");

                        DataTable dt2 = LookupTables.LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_VI", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer2.LocalReport.SetParameters(param);
                        ReportViewer2.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer2.LocalReport.Refresh();
                        ReportViewer2.Visible = true;

                    }
                    #endregion Account Current Statement

                    #region Premium Written
                    if (rdlcDoc == "ReportPremiumWritten.rdlc")
                    {
                        ReportViewer2.LocalReport.ReportPath = ("Reports/VI/ReportPremiumWritten.rdlc");
                        ReportViewer2.LocalReport.DataSources.Clear();
                        ReportViewer2.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter
                                            ta = new GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter();

                        GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable
                            dt = new GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, "26");

                        DataTable dt2 = LookupTables.LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_VI", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer2.LocalReport.SetParameters(param);
                        ReportViewer2.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer2.LocalReport.Refresh();
                        ReportViewer2.Visible = true;

                    }
                    #endregion Premium Written

                    #region Renewals
                    if (rdlcDoc == "ReportRenewals.rdlc")
                    {
                        ReportViewer2.LocalReport.ReportPath = ("Reports/VI/ReportRenewals.rdlc");
                        ReportViewer2.LocalReport.DataSources.Clear();
                        ReportViewer2.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter
                                            ta = new GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter();

                        GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable
                            dt = new GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, "26");

                        DataTable dt2 = LookupTables.LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Expiration Date: ";
                        }
                        else
                        {
                            DateType = "Expiration Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_VI", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer2.LocalReport.SetParameters(param);
                        ReportViewer2.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer2.LocalReport.Refresh();
                        ReportViewer2.Visible = true;

                    }
                    #endregion Renewals

                    #region Quotes vs Policies Issued
                    if (rdlcDoc == "ReportQuotesPoliciesIssued.rdlc")
                    {
                        ReportViewer2.LocalReport.ReportPath = ("Reports/VI/ReportQuotesPoliciesIssued.rdlc");
                        ReportViewer2.LocalReport.DataSources.Clear();
                        ReportViewer2.ProcessingMode = ProcessingMode.Local;

                        string AgentAdd = "";
                        string AgentName = "";
                        string DateType = "";

                        GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter
                                            ta = new GetReportAutoPolicyInfo_VITableAdapters.GetReportAutoPolicyInfo_VITableAdapter();

                        GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable
                            dt = new GetReportAutoPolicyInfo_VI.GetReportAutoPolicyInfo_VIDataTable();

                        ta.Fill(dt, IsEffectiveDate, BegDate, EndDate, Agent, PolicyType, ReportType, "26");

                        DataTable dt2 = LookupTables.LookupTables.GetTable("AgentList");

                        if (dt2.Rows.Count > 0)
                        {
                            for (int i = 0; dt2.Rows.Count > i; i++)
                            {
                                if (dt2.Rows[i]["AgentID"].ToString().Trim() == Agent.ToString().Trim())
                                {
                                    AgentName = dt2.Rows[i]["AgentDesc"].ToString().Trim();
                                    if (dt2.Rows[i]["agt_addr1"].ToString().Trim() != "")
                                    {
                                        AgentAdd = dt2.Rows[i]["agt_addr1"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_city"].ToString().Trim() + ", " + dt2.Rows[i]["agt_st"].ToString().Trim() + " " + dt2.Rows[i]["agt_zip"].ToString().Trim() + "\n\r" + dt2.Rows[i]["agt_phone"].ToString().Trim();
                                    }
                                    break;
                                }
                            }
                        }
                        if (IsEffectiveDate == true)
                        {
                            DateType = "Effective Date: ";
                        }
                        else
                        {
                            DateType = "Entry Date: ";
                        }


                        Microsoft.Reporting.WebForms.ReportDataSource rptDataSource = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportAutoPolicyInfo_VI", (DataTable)dt);

                        ReportParameter[] param = new ReportParameter[5];

                        param[0] = new ReportParameter("BegDate", BegDate);
                        param[1] = new ReportParameter("EndDate", EndDate);
                        param[2] = new ReportParameter("AgentName", AgentName);
                        param[3] = new ReportParameter("AgentAdd", AgentAdd);
                        param[4] = new ReportParameter("DateType", DateType);


                        ReportViewer2.LocalReport.SetParameters(param);
                        ReportViewer2.LocalReport.DataSources.Add(rptDataSource);
                        ReportViewer2.LocalReport.Refresh();
                        ReportViewer2.Visible = true;

                    }

                    #endregion Quotes vs Policies Issued


                }
            }
            catch (Exception exp)
            {
                this.litPopUp.Text = Utilities.MakeLiteralPopUpString("" + exp.Message);
                this.litPopUp.Visible = true;
                return;
            }
        }


    }
}
