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
using EPolicy;
using EPolicy.Customer;
using EPolicy.TaskControl;
using Baldrich.DBRequest;
using OPPReport;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using EPolicy.Quotes;
using EPolicy.XmlCooker;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Web.Services;
using System.Configuration;
using System.Xml.Schema;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net.Mail;
using WebMail = System.Web.Mail;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.IO;
using Xceed.Words.NET;
using Spire.Doc;
using Spire.Doc.Documents;



namespace EPolicy
{
    public partial class RES : System.Web.UI.Page
    {
        private string NAMECONVENTION = "";
        private string PolicyNumber = "";
        private int intTypePolicy = 1;
        private string strInsuredPremises = "";
        private string strTextoExcluded = "EXCLUDED";
        private int intRESLiabilityID = 0;
        private int intRESLiability = 0;
        private string strRESLiabilityPremium = "";
        private string strAgencyName = "";
        private int intBILimitIndex = 0;
        //0 - Load Page
        //1 - Edit Page
        private int intAppStatus = 0;

        //General Converages
        private int intOwner = 0;
        private int intGeneralLesee = 0;
        private int intTenant = 0;
        private int intOther = 0;
        //Type of Insured
        private int intIndividual;
        private int intPartnership = 0;
        private int intCorporation = 0;
        private int intJoinVenture = 0;
        private int intOtherTI = 0;
        //Liability Limit
        private string strMedicalPayment = "5,000";
        private string strFireDamage = "50,000";
        private string strInsuredName = "";
        //
        private string strTypePolicy = "RES";
        private string strRenewalOf = "";
        private string strInsuredNameMailingAddress = "";
        private string strRESLiabilityDT = "RESLiabilityResidential";
        private int intTypeIndex;
        private bool boFunction = false;


        private DataTable DtPolicyDetail = null;

        private DailyTransactionLimiter dtl;

        private HttpApplicationState app;

        private DataTable DtEndorsement;


        protected void Page_Load(object sender, EventArgs e)
        {

            this.litPopUp.Visible = false;
            PaymentInfo.Visible = true;

            Control Banner = new Control();
            Banner = LoadControl(@"TopBannerNew.ascx");
            this.phTopBanner.Controls.Add(Banner);

            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            if (cp == null)
            {
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                Response.Cookies.Add(authCookies);
                FormsAuthentication.SignOut();
                Response.Redirect("Default.aspx?001");
            }
            else
            {
                if (!cp.IsInRole("RES") && !cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("RESVI"))
                {
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    Response.Cookies.Add(authCookies);
                    FormsAuthentication.SignOut();
                    Response.Redirect("Default.aspx?001");
                }
            }
            if (Page.IsPostBack)
            {
                if (Session["TaskControl"] == null)
                {
                    HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                    Response.Cookies.Add(authCookies);
                    FormsAuthentication.SignOut();

                    Response.Redirect("Default.aspx?007");
                }
            }
            
            if (Session["AutoPostBack"] == null)
            {
                if (!IsPostBack)
                {
                    //if (Session["TaskControl"] == null)
                    //{
                    txtEffDt.Attributes.Add("onblur", "checkDate()");
                    // txtExpDt.Attributes.Add("onblur", "checkDate()");
                    imgCalendarEff.Attributes.Add("onBlur", "getExpDt()");
                    TxtTerm.Attributes.Add("onBlur", "getExpDt()");

                    DivRenew.Visible = false;

                    int userID = 0;
                    userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);



                    EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];

                    if (Session["LookUpTables"] == null)
                    {
                        DataTable dtLocation = EPolicy.LookupTables.LookupTables.GetTable("Location");
                        DataTable dtAgency = EPolicy.LookupTables.LookupTables.GetTable("Agency");
                        DataTable dtAgent = EPolicy.LookupTables.LookupTables.GetTable("Agent");
                        DataTable dtAgentVI = EPolicy.LookupTables.LookupTables.GetTable("AgentVI");
                        DataTable dtInsuranceCompany = EPolicy.LookupTables.LookupTables.GetTable("InsuranceCompany");
                        DataTable dtSupplier = EPolicy.LookupTables.LookupTables.GetTable("Supplier");
                        DataTable dtCiudad = EPolicy.LookupTables.LookupTables.GetTable("Ciudad");
                        DataTable dtZipCode = EPolicy.LookupTables.LookupTables.GetTable("Ciudad");
                        DataTable dtVehicleModel = EPolicy.LookupTables.LookupTables.GetTable("VehicleModel");
                        DataTable dtVehicleMake = EPolicy.LookupTables.LookupTables.GetTable("VehicleMake");
                        DataTable dtVehicleYear = EPolicy.LookupTables.LookupTables.GetTable("VehicleYear");
                        DataTable dtNewUse = EPolicy.LookupTables.LookupTables.GetTable("NewUse");
                        DataTable dtBank = EPolicy.LookupTables.LookupTables.GetTable("Bank");
                        DataTable dtDealer = EPolicy.Login.Login.GetGroupDealerByUserID(userID);
                        DataTable dtRESLiabilityResidential = EPolicy.LookupTables.LookupTables.GetTable(strRESLiabilityDT);
                        DataTable dtPolicyClass = EPolicy.LookupTables.LookupTables.GetTable("PolicyClass");

                        List<System.Web.UI.WebControls.ListItem> items = new List<System.Web.UI.WebControls.ListItem>();
                        items.Add(new System.Web.UI.WebControls.ListItem("Residential", "1"));
                        items.Add(new System.Web.UI.WebControls.ListItem("Commercial", "2"));
                        ddlType.Items.AddRange(items.ToArray());
                        //
                        
                        //
                        if (dtDealer.Rows.Count == 0)
                        {
                            //DataTable dtDealer = LookupTables.LookupTables.GetTable("CompanyDealer");
                            dtDealer = EPolicy.LookupTables.LookupTables.GetTable("CompanyDealer");
                        }

                        //Location
                        ddlOriginatedAt.DataSource = dtLocation;
                        ddlOriginatedAt.DataTextField = "locationDesc";
                        ddlOriginatedAt.DataValueField = "locationID";
                        ddlOriginatedAt.DataBind();
                        ddlOriginatedAt.SelectedIndex = -1;
                        ddlOriginatedAt.Items.Insert(0, "");

                        //Agency
                        ddlAgency.DataSource = dtAgency;
                        ddlAgency.DataTextField = "AgencyDesc";
                        ddlAgency.DataValueField = "AgencyID";
                        ddlAgency.DataBind();
                        ddlAgency.SelectedIndex = -1;
                        ddlAgency.Items.Insert(0, "");

                        //Agent
                        if (cp.IsInRole("RESVI") || cp.IsInRole("ADMINISTRATOR"))
                        {
                            ddlAgent.DataSource = dtAgentVI;
                            ddlAgent.DataTextField = "AgentDesc";
                            ddlAgent.DataValueField = "AgentID";
                            ddlAgent.DataBind();
                            ddlAgent.SelectedIndex = -1;
                            ddlAgent.Items.Insert(0, "");
                        }
                        else if (cp.IsInRole("RES"))
                        {
                            ddlAgent.DataSource = dtAgent;
                            ddlAgent.DataTextField = "AgentDesc";
                            ddlAgent.DataValueField = "AgentID";
                            ddlAgent.DataBind();
                            ddlAgent.SelectedIndex = -1;
                            ddlAgent.Items.Insert(0, "");
                        }


                        //InsuranceCompany
                        ddlInsuranceCompany.DataSource = dtInsuranceCompany;
                        ddlInsuranceCompany.DataTextField = "InsuranceCompanyDesc";
                        ddlInsuranceCompany.DataValueField = "InsuranceCompanyID";
                        ddlInsuranceCompany.DataBind();
                        ddlInsuranceCompany.SelectedIndex = -1;
                        ddlInsuranceCompany.Items.Insert(0, "");

                        //PolicyClass
                        ddlPolicyClass.DataSource = dtPolicyClass;
                        ddlPolicyClass.DataTextField = "PolicyClassDesc";
                        ddlPolicyClass.DataValueField = "PolicyClassID";
                        ddlPolicyClass.DataBind();
                        ddlPolicyClass.SelectedValue = taskControl.PolicyClassID.ToString();
                        ddlPolicyClass.Items.Insert(0, "");

                        //Bank
                        ddlBank.DataSource = dtBank;
                        ddlBank.DataTextField = "BankDesc";
                        ddlBank.DataValueField = "BankID";
                        ddlBank.DataBind();
                        ddlBank.SelectedIndex = -1;
                        ddlBank.Items.Insert(0, "");

                        //Dealer
                        ddlCompanyDealer.DataSource = dtDealer;
                        ddlCompanyDealer.DataTextField = "CompanyDealerDesc";
                        ddlCompanyDealer.DataValueField = "CompanyDealerID";
                        ddlCompanyDealer.DataBind();
                        ddlCompanyDealer.SelectedIndex = -1;
                        ddlCompanyDealer.Items.Insert(0, "");

                        //Location
                        dllBILimit.DataSource = dtRESLiabilityResidential;
                        dllBILimit.DataTextField = "LimitDesc";
                        dllBILimit.DataValueField = "RliabilityID";
                        dllBILimit.DataBind();
                        dllBILimit.SelectedIndex = -1;
                        dllBILimit.Items.Insert(0, "");

                        Session.Add("LookUpTables", "In");
                    }

                    MyAccordion.SelectedIndex = 0;
                    Accordion1.SelectedIndex = -1;
                    Accordion2.SelectedIndex = -1;
                    Accordion3.SelectedIndex = -1;
                    if (taskControl.isQuote == false) 
                    {
                        TxtPolicyNo.Text = taskControl.PolicyID.ToString();
                    }
                    switch (taskControl.Mode)
                    {
                        case 1: //ADD
                            EnableControls();
                            FillTextControl();
                            MyAccordion.SelectedIndex = 0;
                            break;

                        case 2: //UPDATE
                            FillTextControl();
                            EnableControls();
                            break;

                        default:    //DELETE & CLEAR
                            FillTextControl();
                            DisableControls();
                            break;
                    }
                    TxtFirstName.Focus();
                    CustomerTypeSelection();

                }
                else
                {
                    if (Session["TaskControl"] != null)
                    {
                        EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
                        if (taskControl.Mode == 4)
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

        #region FuncionBoton
        protected void BtnExit_Click(object sender, EventArgs e)
        {

            RemoveSessionLookUp();
            this.litPopUp.Visible = false;
            Session.Clear();
            Response.Redirect("Search.aspx");

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
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

            try
            {
                ValidateFields();
                FillProperties();
                FillReqDocsGrid();

                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];

                taskControl.SaveRES(userID);

                //taskControl.SaveGuardianXtra(userID);  //(userID);

                // UpdateGuadianXtraHasAccident12(taskControl.TaskControlID, HasAccident12);

                taskControl = (EPolicy.TaskControl.RES)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);

                Session["TaskControl"] = taskControl;

                if (taskControl.isQuote == false && taskControl.Trams_FL == false)
                {
                    //PolicyXML(taskControl.TaskControlID);
                    if (!(HttpContext.Current.Request.Url.ToString().Contains("localhost")))
                    {
                        System.Data.DataTable dt = null;
                        dt = GetVerifyPolicyExist(taskControl.TaskControlID);

                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                throw new Exception("This quote has already been converted to Policy.");
                            }
                        }

                        SendPolicyToPPS(taskControl.TaskControlID);
                        taskControl.Trams_FL = true;
                    }
                }

                FillTextControl();
                DisableControls();
                btnAdjuntar.Visible = true;
                btnAdjuntar.Enabled = true;
                btnInvoice.Enabled = true;
                btnPrintQuote.Visible = true;
                btnPrintQuote.Enabled = true;
                lblRecHeader.Text = "RES information saved successfully.";// + taskControl.Mode.ToString() + CUSTOMER2.ToString();
                mpeSeleccion.Show();
            }
            catch (SqlException exp)
            {
                LogError(exp);
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
            }
        }
        protected void btnAcceptQuote_Click(object sender, EventArgs e)
        {

            EPolicy.TaskControl.RES taskContro1 = (EPolicy.TaskControl.RES)Session["TaskControl"];

            try
            {
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;


                //VerifyPolicyExist();


                EPolicy.TaskControl.RES taskControlQuote = (EPolicy.TaskControl.RES)Session["TaskControl"];

                Session.Clear();
                EPolicy.TaskControl.RES taskControl = new EPolicy.TaskControl.RES(false);

                //taskControl = taskControlQuote;


                taskControl.Mode = 1; //ADD
                taskControl.isQuote = false;
                taskControl.AutoAssignPolicy = true;
                taskControl.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "RES"));
                taskControl.InsuranceCompany = taskControl.InsuranceCompany;
                taskControl.Agency = taskControlQuote.Agency;
                taskControl.Agent = taskControlQuote.Agent;
                taskControl.OriginatedAt = taskControlQuote.OriginatedAt;
                taskControl.DepartmentID = 1;
                taskControl.EffectiveDate = taskControlQuote.EffectiveDate;
                taskControl.ExpirationDate = taskControlQuote.ExpirationDate;

                taskControl.Customer.CustomerNo = taskControlQuote.Customer.CustomerNo;
                taskControl.Customer.FirstName = taskControlQuote.Customer.FirstName;
                taskControl.Customer.LastName1 = taskControlQuote.Customer.LastName1;
                taskControl.Customer.LastName2 = taskControlQuote.Customer.LastName2;
                taskControl.Customer.Initial = taskControlQuote.Customer.Initial;
                taskControl.Customer.Sex = taskControlQuote.Customer.Sex;
                taskControl.Customer.Address1 = taskControlQuote.Customer.Address1;
                taskControl.Customer.City = taskControlQuote.Customer.City;
                taskControl.Customer.State = taskControlQuote.Customer.State;
                taskControl.Customer.ZipCode = taskControlQuote.Customer.ZipCode;
                taskControl.Customer.Licence = taskControlQuote.Customer.Licence;
                taskControl.Customer.MaritalStatus = taskControlQuote.Customer.MaritalStatus;
                taskControl.Customer.Birthday = taskControlQuote.Customer.Birthday;
                taskControl.Customer.Age = taskControlQuote.Customer.Age;
                taskControl.Customer.JobPhone = taskControlQuote.Customer.JobPhone;
                taskControl.Customer.HomePhone = taskControlQuote.Customer.HomePhone;
                taskControl.Customer.OccupationID = taskControlQuote.Customer.OccupationID;
                taskControl.Customer.Occupation = taskControlQuote.Customer.Occupation;
                taskControl.Customer.EmplName = taskControlQuote.Customer.EmplName;
                taskControl.Customer.Address1 = taskControlQuote.Customer.Address1;
                taskControl.Customer.Address2 = taskControlQuote.Customer.Address2;
                taskControl.Customer.City = taskControlQuote.Customer.City;
                taskControl.Customer.State = taskControlQuote.Customer.State;
                taskControl.Customer.ZipCode = taskControlQuote.Customer.ZipCode;
                taskControl.Customer.AddressPhysical1 = taskControlQuote.Customer.AddressPhysical1;
                taskControl.Customer.AddressPhysical2 = taskControlQuote.Customer.AddressPhysical2;
                taskControl.Customer.CityPhysical = taskControlQuote.Customer.CityPhysical;
                taskControl.Customer.StatePhysical = taskControlQuote.Customer.StatePhysical;
                taskControl.Customer.ZipPhysical = taskControlQuote.Customer.ZipPhysical;
                taskControl.Customer.Description = taskControlQuote.Customer.Description;
                taskControl.Customer.LocationID = taskControlQuote.Customer.LocationID;
                taskControl.Customer.Email = taskControlQuote.Customer.Email;
                taskControl.Customer.Cellular = taskControlQuote.Customer.Cellular;

                taskControl.Customer.SocialSecurity = taskControlQuote.Customer.SocialSecurity;



                taskControl.PolicyType = "RES";

                taskControl.CompanyDealer = taskControlQuote.CompanyDealer;
                taskControl.InsuranceCompany = taskControlQuote.InsuranceCompany;
                taskControl.OriginatedAt = taskControlQuote.OriginatedAt;
                taskControl.Agent = taskControlQuote.Agent;
                taskControl.Agency = taskControlQuote.Agency;
                //taskControl.AgentDesc = taskControlQuote.AgentDesc;

                taskControl.CancellationDate = taskControlQuote.CancellationDate;
                taskControl.CancellationEntryDate = "";
                taskControl.CancellationUnearnedPercent = taskControlQuote.CancellationUnearnedPercent;
                taskControl.CancellationMethod = taskControlQuote.CancellationMethod;
                taskControl.CancellationReason = taskControlQuote.CancellationReason;
                taskControl.PaidAmount = taskControlQuote.PaidAmount;
                taskControl.PaidDate = "";
                taskControl.Ren_Rei = taskControlQuote.Ren_Rei;
                taskControl.CommissionAgency = taskControlQuote.CommissionAgency; // taskControl.ReturnCommissionAgency;
                taskControl.CommissionAgent = taskControlQuote.CommissionAgent; // taskControl.ReturnCommissionAgent;
                taskControl.CommissionAgencyPercent = taskControlQuote.CommissionAgencyPercent; // taskControl.CommissionAgencyPercent.Trim();
                taskControl.CommissionAgentPercent = taskControlQuote.CommissionAgentPercent;  //taskControl.CommissionAgentPercent.Trim();
                taskControl.TaskControlID = taskControlQuote.TaskControlID;
                taskControl.Status = taskControlQuote.Status;
                taskControl.ReturnCharge = taskControlQuote.ReturnCharge;
                taskControl.ReturnPremium = taskControlQuote.ReturnPremium;
                taskControl.CancellationAmount = taskControlQuote.CancellationAmount;
                taskControl.ReturnCommissionAgency = taskControlQuote.ReturnCommissionAgency;
                taskControl.ReturnCommissionAgent = taskControlQuote.ReturnCommissionAgent;

                taskControl.EntryDate = DateTime.Now;
                taskControl.Term = taskControl.Term;

                taskControl.TotalPremium = taskControlQuote.TotalPremium;
                taskControl.Charge = taskControlQuote.Charge;
                taskControl.TCIDQuotes = taskControlQuote.TaskControlID;
                SetRESParameters(taskControl, taskControlQuote);

                Session.Add("TaskControl", taskControl);

                try
                {
                    Response.Redirect("RES.aspx");
                    //}
                }
                catch (Exception xp)
                {
                    LogError(xp);
                    //lblRecHeader.Text = xp.Message;
                    //mpeSeleccion.Show();
                }

            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
            }

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {            
            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
            taskControl.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.UPDATE;

            Session.Add("TaskControl", taskControl);

            btnRenew.Visible = false;

            EnableControls();

            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            if (cp.IsInRole("MODIFY SOCIAL SECURITY"))
            {
                EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();

                txtSocSec.Enabled = true;

                if (taskControl.Customer.SocialSecurity.Trim() != "")
                    txtSocSec.Text = encrypt.Decrypt(taskControl.Customer.SocialSecurity);
                else
                    txtSocSec.Text = "";

                MaskedEditExtender1.Mask = "999-99-9999";
            }
            //
            editForm();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];

            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
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

            if (taskControl.Mode == 1) //ADD
            {
                Session.Clear();
                Response.Redirect("Search.aspx");
            }
            else
            {
                RemoveSessionLookUp();
                taskControl = (EPolicy.TaskControl.RES)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
                taskControl.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                Session["TaskControl"] = taskControl;
                FillTextControl();
                DisableControls();

            }
        }
        protected void ChkAutoAssignPolicy_CheckedChanged(object sender, EventArgs e)
        {
            VerifyAssignPolicyFields();
        }
        protected void chkIsRenew_CheckedChanged(object sender, EventArgs e)
        {
            //VerifyAssignPolicyFields();
            //TxtPolicyNo.Visible = !chkIsRenew.Checked;
        }
        protected void btnCancellation_Click(object sender, EventArgs e)
        {

            RemoveSessionLookUp();
            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
            Session.Add("TaskControl", taskControl);
            Session.Add("FromUI", "GuardianXtra.aspx");
            Session.Add("CancFromGuardianXtra", "CancFromGuardianXtra");
            // Session.Add("CancFromRoadAssistanceExit", "CancFromRoadAssistanceExit");
            Response.Redirect("CancellationPolicy.aspx", false);
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            Session.Remove("OPPEndorUpdate");
            Session.Remove("OPPEndorsementID");
            Session.Remove("ONLYAUTOEndorsement");
            Session.Remove("ApplyEndorsement");
        }
        protected void DataGridGroup_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                switch (e.CommandName)
                {
                    case "Select":
                        int i = int.Parse(e.Item.Cells[2].Text);

                        if (i != 0)
                        {
                            EPolicy.TaskControl.RES opp = (EPolicy.TaskControl.RES)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(i, userID);
                            opp.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                            opp.IsEndorsement = true;

                            if (Session["TaskControl"] != null)
                            {
                                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
                                Session.Clear();
                                Session.Add("AUTOEndorsement", taskControl);
                                Session.Add("OPPEndorUpdate", "Update");
                                Session.Remove("TaskControl");
                            }

                            Session.Add("TaskControl", opp);
                            Response.Redirect("ExpressAutoQuote.aspx");
                        }
                        break;

                    case "Apply":

                        string date = e.Item.Cells[3].Text.Trim();
                        if (date.ToString().Trim() != "&nbsp;")
                        {
                            throw new Exception("This Endorsement is already Applied.");
                        }

                        int a = int.Parse(e.Item.Cells[2].Text);
                        EPolicy.TaskControl.RES newOPP = (EPolicy.TaskControl.RES)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(a, userID);

                        int OPPEndorsementID = int.Parse(e.Item.Cells[1].Text);
                        Session.Add("OPPEndorsementID", OPPEndorsementID);


                        ///CalculateEndorsement(newOPP);
                        ///VerifyChanges(newOPP, userID);
                        Session.Add("ApplyEndorsement", a);
                        //ApplyEndorsement(newOPP, userID);
                        break;

                    case "Update":

                        string date3 = e.Item.Cells[3].Text.Trim();
                        if (date3.ToString().Trim() == "&nbsp;")
                        {
                            throw new Exception("This Endorsement is not Applied.");
                        }

                        //int a = int.Parse(e.Item.Cells[2].Text);
                        //EPolicy.TaskControl.OptimaPersonalPackage newOPP = (EPolicy.TaskControl.OptimaPersonalPackage)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(a, userID);

                        int OPPEndorsement3ID = int.Parse(e.Item.Cells[1].Text);
                        Session.Add("OPPEndorsementID", OPPEndorsement3ID);
                        Session.Add("OPPEndorUpdate", "Update");
                        break;

                    case "Print":

                        string date2 = e.Item.Cells[3].Text.Trim();
                        if (date2.ToString().Trim() == "&nbsp;")
                        {
                            throw new Exception("This Endorsement is not Applied.");
                        }

                        EPolicy.TaskControl.RES taskControl2 = (EPolicy.TaskControl.RES)Session["TaskControl"];

                        int s = int.Parse(e.Item.Cells[2].Text);
                        string comments = e.Item.Cells[10].Text.Trim();
                        EPolicy.TaskControl.RES newOPP2 = (EPolicy.TaskControl.RES)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(s, userID);
                        int OPPEndorID = int.Parse(e.Item.Cells[1].Text);

                        //Print Document
                        try
                        {
                            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];

                            //GetReportEndosoTableAdapters.GetReportEndosoTableAdapter ds = new GetReportEndosoTableAdapters.GetReportEndosoTableAdapter();
                            //Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportEndoso_GetReportEndoso", (DataTable)ds.GetData(OPPEndorID));

                            Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                            viewer.LocalReport.DataSources.Clear();
                            viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                            viewer.LocalReport.ReportPath = Server.MapPath("Reports/Endoso.rdlc");
                            //viewer.LocalReport.DataSources.Add(rds);
                            viewer.LocalReport.Refresh();

                            // Variables 
                            Warning[] warnings;
                            string[] streamIds;
                            string mimeType;
                            string encoding = string.Empty;
                            string extension;
                            string fileName = "Endorsement" + taskControl.Prospect.FirstName + taskControl.TaskControlID.ToString().Trim() + "-" + OPPEndorID.ToString().Trim();
                            string _FileName = "Endorsement" + taskControl.Prospect.FirstName + taskControl.TaskControlID.ToString().Trim() + "-" + OPPEndorID.ToString().Trim() + ".pdf";
                            //Ññ
                            if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName))
                                File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName);

                            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                            using (FileStream fs = new FileStream(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName, FileMode.Create))
                            {
                                fs.Write(bytes, 0, bytes.Length);
                            }

                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + _FileName + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);

                        }
                        catch (Exception ecp)
                        {

                        }

                        break;

                    default: //Page
                        break;
                }
            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
            }
        }
        protected void btnRenew_Click(object sender, EventArgs e)
        {
            try
            {
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                TaskControl.RES taskControl = new TaskControl.RES(false);  //Policy
                TaskControl.RES GuardianXtra = (TaskControl.RES)Session["TaskControl"];

                taskControl = GuardianXtra;

                //taskControl.IsPolicy = true;
                taskControl.Mode = 1;
                taskControl.TaskControlTypeID = 29; // Guardian Xtra

                taskControl.IsEndorsement = false;
                taskControl.CancellationDate = "";
                taskControl.CancellationEntryDate = "";
                taskControl.CancellationUnearnedPercent = 0.00;
                taskControl.CancellationMethod = 0;
                taskControl.CancellationReason = 0;
                taskControl.PaidAmount = taskControl.PaidAmount;
                taskControl.PaidDate = "";
                taskControl.Ren_Rei = "REI";
                taskControl.Rein_Amt = GuardianXtra.CancellationAmount;
                taskControl.PaidDate = GuardianXtra.PaidDate;
                taskControl.CommissionAgency = 0.00; // taskControl.ReturnCommissionAgency;
                taskControl.CommissionAgent = 0.00; // taskControl.ReturnCommissionAgent;
                taskControl.CommissionAgencyPercent = ""; // taskControl.CommissionAgencyPercent.Trim();
                taskControl.CommissionAgentPercent = "";  //taskControl.CommissionAgentPercent.Trim();
                //taskControl.TaskControlID = 0;
                taskControl.Status = "Inforce";

                taskControl.ReturnCharge = 0.00;
                taskControl.ReturnPremium = 0.00;
                taskControl.CancellationAmount = 0.00;
                taskControl.ReturnCommissionAgency = 0.00;
                taskControl.ReturnCommissionAgent = 0.00;

                taskControl.IsDeferred = false;
                taskControl.Agency = GuardianXtra.Agency;
                taskControl.Agent = GuardianXtra.Agent;
                taskControl.Bank = GuardianXtra.Bank;
                taskControl.CompanyDealer = GuardianXtra.CompanyDealer;
                taskControl.InsuranceCompany = GuardianXtra.InsuranceCompany;

                taskControl.AgentList = GuardianXtra.AgentList;
                taskControl.LbxAgentSelected = GuardianXtra.LbxAgentSelected;
                taskControl.LbxAgentSelected = GuardianXtra.LbxAgentSelected;

                taskControl.Customer = GuardianXtra.Customer; // quoteAuto.Customer;
                taskControl.CustomerNo = GuardianXtra.Customer.CustomerNo; // quoteAuto.CustomerNo;
                taskControl.Prospect = GuardianXtra.Prospect;
                taskControl.ProspectID = GuardianXtra.ProspectID;
                taskControl.Term = GuardianXtra.Term;
                taskControl.EffectiveDate = (DateTime.Parse(GuardianXtra.EffectiveDate).AddMonths(int.Parse(TxtTerm.Text.ToString().Trim()))).ToShortDateString();
                taskControl.ExpirationDate = DateTime.Parse(GuardianXtra.ExpirationDate).AddMonths(int.Parse(TxtTerm.Text.ToString().Trim())).ToShortDateString();
                taskControl.EntryDate = DateTime.Now;

                taskControl.PolicyType = GuardianXtra.PolicyType;

                if (GuardianXtra.MasterPolicyID.Trim() == "")
                    taskControl.IsMaster = false; // quoteAuto.IsMaster;
                else
                    taskControl.IsMaster = true;

                taskControl.MasterPolicyID = GuardianXtra.MasterPolicyID;
                taskControl.FileNumber = GuardianXtra.FileNumber;
                taskControl.PolicyType = GuardianXtra.PolicyType;
                taskControl.PolicyNo = GuardianXtra.PolicyNo;
                taskControl.Certificate = GuardianXtra.Certificate;
                taskControl.AutoAssignPolicy = false;

                //Added by Joshua
                taskControl.Suffix = DateTime.Parse(taskControl.EffectiveDate).Year.ToString().Remove(0, 2);

                taskControl.PolicyCicleEnd = 1; //Para que en la pantalla de auto no entre de modo new.
                taskControl.Agent = GuardianXtra.Agent;
                taskControl.Agency = GuardianXtra.Agency;
                taskControl.InsuranceCompany = "001";  //MULTI INS.
                taskControl.PMT1 = 0;
                //taskControl.QuoteId = 0;

                taskControl.Charge = GuardianXtra.Charge;
                taskControl.TotalPremium = GuardianXtra.TotalPremium;

                //taskControl.TaskControlID = 0;

                Session.Clear();
                Session.Add("TaskControl", taskControl);
                Response.Redirect("GuardianXtra.aspx", false);

            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
            }
        }
        protected void btnReinstallation_Click(object sender, EventArgs e)
        {
            try
            {
                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                TaskControl.RES taskControl = new TaskControl.RES(false);  //Policy
                TaskControl.RES GuardianXtra = (TaskControl.RES)Session["TaskControl"];


                EPolicy.TaskControl.RES taskControlEndo = null;

                taskControl = GuardianXtra;

                //taskControl.IsPolicy = true;
                taskControl.Mode = 1;
                taskControl.TaskControlTypeID = 29; // Guardian Xtra
                taskControl.TaskControlID = GuardianXtra.TaskControlID;
                taskControl.IsEndorsement = false;
                taskControl.CancellationDate = "";
                taskControl.CancellationEntryDate = "";
                taskControl.CancellationUnearnedPercent = 0.00;
                taskControl.CancellationMethod = 0;
                taskControl.CancellationReason = 0;
                taskControl.PaidAmount = taskControl.PaidAmount;
                taskControl.PaidDate = "";
                taskControl.Ren_Rei = "REI";
                taskControl.Rein_Amt = GuardianXtra.CancellationAmount;
                taskControl.PaidDate = GuardianXtra.PaidDate;
                taskControl.CommissionAgency = 0.00; // taskControl.ReturnCommissionAgency;
                taskControl.CommissionAgent = 0.00; // taskControl.ReturnCommissionAgent;
                taskControl.CommissionAgencyPercent = ""; // taskControl.CommissionAgencyPercent.Trim();
                taskControl.CommissionAgentPercent = "";  //taskControl.CommissionAgentPercent.Trim();
                //taskControl.TaskControlID = 0;
                taskControl.Status = "Inforce";

                taskControl.ReturnCharge = 0.00;
                taskControl.ReturnPremium = 0.00;
                taskControl.CancellationAmount = 0.00;
                taskControl.ReturnCommissionAgency = 0.00;
                taskControl.ReturnCommissionAgent = 0.00;

                taskControl.IsDeferred = false;
                taskControl.Agency = GuardianXtra.Agency;
                taskControl.Agent = GuardianXtra.Agent;
                taskControl.Bank = GuardianXtra.Bank;
                taskControl.CompanyDealer = GuardianXtra.CompanyDealer;
                taskControl.InsuranceCompany = GuardianXtra.InsuranceCompany;

                taskControl.AgentList = GuardianXtra.AgentList;
                taskControl.LbxAgentSelected = GuardianXtra.LbxAgentSelected;
                taskControl.LbxAgentSelected = GuardianXtra.LbxAgentSelected;

                taskControl.Customer = GuardianXtra.Customer; // quoteAuto.Customer;
                taskControl.CustomerNo = GuardianXtra.Customer.CustomerNo; // quoteAuto.CustomerNo;
                taskControl.Prospect = GuardianXtra.Prospect;
                taskControl.ProspectID = GuardianXtra.ProspectID;
                taskControl.Term = GuardianXtra.Term;
                taskControl.EffectiveDate = DateTime.Now.ToShortDateString();
                taskControl.ExpirationDate = DateTime.Parse(GuardianXtra.ExpirationDate).AddMonths(int.Parse(TxtTerm.Text.ToString().Trim())).ToShortDateString();
                taskControl.EntryDate = DateTime.Now;

                taskControl.PolicyType = GuardianXtra.PolicyType;

                //taskControl.OriginatedAt = int.Parse(ddlLocation.SelectedItem.Value);

                if (GuardianXtra.MasterPolicyID.Trim() == "")
                    taskControl.IsMaster = false; // quoteAuto.IsMaster;
                else
                    taskControl.IsMaster = true;

                taskControl.MasterPolicyID = GuardianXtra.MasterPolicyID;
                taskControl.FileNumber = GuardianXtra.FileNumber;
                taskControl.PolicyType = GuardianXtra.PolicyType;
                taskControl.PolicyNo = GuardianXtra.PolicyNo;
                taskControl.Certificate = GuardianXtra.Certificate;
                taskControl.AutoAssignPolicy = false;

                int msufijo;
                int sufijo = int.Parse(GuardianXtra.Suffix);
                msufijo = sufijo + 1;
                taskControl.Suffix = "0".ToString() + msufijo.ToString();
                // taskControl.Suffix = GuardianXtra.Suffix;

                taskControl.PolicyCicleEnd = 1; //Para que en la pantalla de auto no entre de modo new.
                taskControl.Agent = GuardianXtra.Agent;
                taskControl.Agency = GuardianXtra.Agency;
                taskControl.InsuranceCompany = "001";  //MULTI INS.
                taskControl.PMT1 = 0;
                //taskControl.QuoteId = 0;



                taskControl.Charge = GuardianXtra.Charge;
                taskControl.TotalPremium = GuardianXtra.TotalPremium;


                // taskControl.TaskControlID = 0;

                Session.Clear();
                Session.Add("TaskControl", taskControl);

                Response.Redirect("GuardianXtra.aspx", false);

            }
            catch (Exception exp)
            {
                lblRecHeader.Text = exp.Message;
                mpeSeleccion.Show();
            }
        }
        protected void PrintRESMSWord(string reportName, Dictionary<string, string> bookmarks, List<string> mergePaths, string ProcessedPath)
        {
            try
            {

                string ProcessedPath2 = ConfigurationManager.AppSettings["ReportPathName"];
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
                int tc_id = taskControl.TaskControlID;
                string copyFileName = CopyFile(reportName);
                string fileName = ProcessedPath2 + "\\RES\\Copy\\" + copyFileName;
                var document = DocX.Load(fileName);
                foreach (var bookmark in bookmarks)
                {
                    var bm = document.Bookmarks[bookmark.Key];
                    bm.SetText(bookmark.Value);
                }
                document.Save();

                //Load Document
                Document document2 = new Document();

                document2.LoadFromFile(fileName);

                //Convert Word to PDF
                string FinalFile = ProcessedPath + tc_id + reportName + "_" + DateTime.Now.ToString().Replace("/", "-").Replace(":", "").Replace(" ", "") + ".pdf";

                document2.SaveToFile(FinalFile, FileFormat.PDF);
                mergePaths.Add(FinalFile);
            }

            catch (Exception ex)
            {
                LogError(ex);
            }

        }
        protected void PrintRESPDFMerge(List<string> mergePaths, string ProcessedPath)
        {
            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
            OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
            string FinalFileMerge = "";
            FinalFileMerge = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFileMerge + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
        }
        private string CopyFile(string reportName)
        {
            string fileName = reportName;
            string ProcessedPath = ConfigurationManager.AppSettings["ReportPathName"];
            string sourcePath = ProcessedPath + @"\RES\";
            string targetPath = ProcessedPath + @"\RES\Copy\";

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName + ".docx");
            string copyFileName = fileName + DateTime.Now.ToString().Replace("/", "-").Replace(":", "").Replace(" ", "") + ".docx";
            string destFile = System.IO.Path.Combine(targetPath, copyFileName);

            System.IO.File.Copy(sourceFile, destFile, true);

            return copyFileName;
        }
        protected void btnInvoice_Click(object sender, EventArgs e)
        {
            PrintInvoice();
        }
        protected void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
                List<string> mergePaths = new List<string>();
                string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

                mergePaths.Add(PrintPreview("MidOceanInvoiceES.rdlc"));

                //Generar PDF
                OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                string FinalFile = "";
                FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.Customer.FirstName + "" + taskControl.Customer.LastName1 + taskControl.Customer.LastName2);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void btnPrintQuote_Click(object sender, EventArgs e)
        {
            UtilitiesComponents.RES.print_report ReportParameters = LoadPrintPolicy();
            EPolicy.TaskControl.RES taskControl = LoadTaskControl();
            String strTemplate = "ReportQuote";
            String strQuoteNo = taskControl.TaskControlID.ToString();
            ReportParameters.quote_number = strQuoteNo;
            String strVacio = "";

            PrintReportRES(strTemplate, strVacio);
        }
        protected void btnPrintPolicy_Click(object sender, EventArgs e)
        {
            EPolicy.TaskControl.RES taskControl = LoadTaskControl();
            String strTemplate = "ReportPolicy";
            String strPolicyNo1 = "RES" + TxtPolicyNo.Text.Trim() + "-" + taskControl.Suffix;
            PrintReportRES(strTemplate, strPolicyNo1);
        }
        protected void GridReqDocs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());

            }

            catch (Exception ex)
            {

            }
        }
        protected void chkSameMailing_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSameMailing.Checked)
            {
                txtPhyAddress.Text = TxtAddrs1.Text.Trim();
                txtPhyAddress2.Text = TxtAddrs2.Text.Trim();
                txtPhyState.Text = TxtState.Text.Trim();

                ddlPhyZipCode.Text = ddlZip.Text.Trim();
                ddlPhyCity.Text = ddlCiudad.Text.Trim();
            }
            if (!chkSameMailing.Checked)
            {
                txtPhyAddress.Text = "";
                txtPhyAddress2.Text = "";

                ddlPhyZipCode.Text = "";
                ddlPhyCity.Text = "";
            }
        }
        protected void txtEffDt_TextChanged(object sender, EventArgs e)
        {
            DateTime Expdate = DateTime.Parse(txtEffDt.Text.ToString());
            Expdate = DateTime.Parse(DateTime.Parse(txtEffDt.Text.ToString()).AddMonths(int.Parse(TxtTerm.Text.ToString().Trim())).ToShortDateString());

            txtExpDt.Text = Expdate.ToString();
        }
        protected void TxtTerm_TextChanged(object sender, EventArgs e)
        {
            DateTime Expdate = DateTime.Parse(txtEffDt.Text.ToString());
            Expdate = DateTime.Parse(DateTime.Parse(txtEffDt.Text.ToString()).AddMonths(int.Parse(TxtTerm.Text.ToString().Trim())).ToShortDateString());

            txtExpDt.Text = Expdate.ToString();
        }
        protected void txtPhyAddress_TextChanged(object sender, EventArgs e)
        {
            chkSameMailing.Checked = false;
        }
        protected void txtPhyAddress2_TextChanged(object sender, EventArgs e)
        {
            chkSameMailing.Checked = false;
        }
        protected void txtPhyState_TextChanged(object sender, EventArgs e)
        {
            chkSameMailing.Checked = false;
        }
        protected void txtPenalty_TextChanged(object sender, EventArgs e)
        {
            Page.MaintainScrollPositionOnPostBack = true;
        }
        protected void DocumentChecked_Click(object sender, EventArgs e)
        {
            try
            {
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
                GridViewRow row = (sender as CheckBox).Parent.Parent as GridViewRow;
                int index = row.RowIndex;
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustomerTypeSelection();
            LoadDropDownBILimit();
        }
        protected void ddlAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            strAgencyName = ddlAgent.SelectedItem.Text;
            getBILimitValue();
        }
        protected void CustomerTypeSelection()
        {
            try
            {
                intBILimitIndex = dllBILimit.SelectedIndex;
                lblFirstName.ForeColor = System.Drawing.Color.Red;
                lblLastName.ForeColor = System.Drawing.Color.Red;
                lblCompanyName.ForeColor = System.Drawing.Color.Black;
                txtCompanyName.Enabled = false;
                //
                if (ddlType.SelectedIndex > 0)
                {
                    lblFirstName.ForeColor = System.Drawing.Color.Black;
                    lblLastName.ForeColor = System.Drawing.Color.Black;
                    lblCompanyName.ForeColor = System.Drawing.Color.Red;
                    txtCompanyName.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString(), ex);
            }
        }
        protected bool CheckIfBondExistInPPS()
        {
            bool Found = false;

            try
            {
                string cmConfiguracion = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
                //@"Data Source=GIC-MSQL\PPSSQLSERVER;Initial Catalog=AgentTestData;User ID=urclaims;password=3G@TD@t!1";
                DataTable dtTabla = new DataTable();
                using (var sqlconConexion = new SqlConnection(cmConfiguracion))
                using (var sqlcoCommand = new SqlCommand("sproc_ConsumeXMLePPS-BONDS_Verify", sqlconConexion))
                using (var sqldaDataAdapter = new SqlDataAdapter(sqlcoCommand))
                {
                    sqlcoCommand.CommandType = CommandType.StoredProcedure;
                    sqlcoCommand.Parameters.Clear();
                    sqlcoCommand.Parameters.AddWithValue("PolicyID", txtPolicyNoToRenew.Text.Trim());
                    sqldaDataAdapter.Fill(dtTabla);
                }

                if (dtTabla.Rows.Count > 0)
                {
                    EPolicy.TaskControl.RES tcRES = (EPolicy.TaskControl.RES)Session["TaskControl"];

                    Found = true;

                    //FILL FIELDS WITH PPS INFO

                    tcRES.Customer.Description = dtTabla.Rows[0]["Client"].ToString();

                    if (DateTime.Parse(DateTime.Now.ToShortDateString()) <= DateTime.Parse(dtTabla.Rows[0]["Expire"].ToString()))
                    {
                        txtEffDt.Text = DateTime.Parse(dtTabla.Rows[0]["Expire"].ToString()).ToShortDateString();
                        txtExpDt.Text = DateTime.Parse(dtTabla.Rows[0]["Expire"].ToString()).AddYears(1).ToShortDateString();
                    }
                    else
                    {
                        txtEffDt.Text = DateTime.Parse(DateTime.Now.ToShortDateString()).ToShortDateString();
                        txtExpDt.Text = DateTime.Parse(DateTime.Now.ToShortDateString()).AddYears(1).ToShortDateString();
                    }



                    TxtAddrs1.Text = dtTabla.Rows[0]["Maddr1"].ToString();
                    TxtAddrs2.Text = dtTabla.Rows[0]["Maddr2"].ToString();
                    ddlZip.Text = dtTabla.Rows[0]["Mzip"].ToString();
                    ddlCiudad.Text = dtTabla.Rows[0]["Mcity"].ToString();
                    TxtState.Text = dtTabla.Rows[0]["Mstate"].ToString();

                    txtPhyAddress.Text = dtTabla.Rows[0]["Raddr1"].ToString();
                    txtPhyAddress2.Text = dtTabla.Rows[0]["Raddr2"].ToString();
                    ddlPhyZipCode.Text = dtTabla.Rows[0]["Rzip"].ToString();
                    ddlPhyCity.Text = dtTabla.Rows[0]["Rcity"].ToString();
                    txtPhyState.Text = dtTabla.Rows[0]["Rstate"].ToString();

                    TxtHomePhone.Text = dtTabla.Rows[0]["Wphone"].ToString();
                    txtWorkPhone.Text = dtTabla.Rows[0]["Wphone"].ToString();
                    TxtCellular.Text = dtTabla.Rows[0]["Cphone"].ToString();

                    txtEmail.Text = dtTabla.Rows[0]["Eaddr"].ToString();

                    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(ddlAgent.Items.FindByValue(GetAgentByCarsID(dtTabla.Rows[0]["BrokerID"].ToString())));


                    if (bool.Parse(dtTabla.Rows[0]["BusFlag"].ToString()) == false) // INDIVIDUAL
                    {
                        TxtFirstName.Text = dtTabla.Rows[0]["FirstName"].ToString();
                        txtLastname1.Text = dtTabla.Rows[0]["LastName"].ToString();
                        TxtInitial.Text = dtTabla.Rows[0]["Middle"].ToString();
                        ddlType.SelectedValue = "1"; // table.Rows[0]["BusType"].ToString();

                        lblFirstName.ForeColor = System.Drawing.Color.Red;
                        lblLastName.ForeColor = System.Drawing.Color.Red;
                        lblCompanyName.ForeColor = System.Drawing.Color.Black;
                        txtCompanyName.Enabled = false;

                    }
                    else if (bool.Parse(dtTabla.Rows[0]["BusFlag"].ToString()) == true) // CORPORATE
                    {
                        TxtFirstName.Text = "";
                        txtLastname1.Text = "";
                        txtCompanyName.Text = dtTabla.Rows[0]["LastName"].ToString();
                        ddlType.SelectedValue = "2"; // table.Rows[0]["BusType"].ToString();

                        lblFirstName.ForeColor = System.Drawing.Color.Black;
                        lblLastName.ForeColor = System.Drawing.Color.Black;
                        lblCompanyName.ForeColor = System.Drawing.Color.Red;
                        txtCompanyName.Enabled = true;

                    }
                    //else if (table.Rows[0]["BusType"].ToString() == "3") // DBA
                    //{
                    //    TxtFirstName.Text = "";
                    //    txtLastname1.Text = "";
                    //    txtCompanyName.Text = table.Rows[0]["LastName"].ToString();
                    //    ddlType.SelectedValue = table.Rows[0]["BusType"].ToString();

                    //    lblFirstName.ForeColor = Color.Black;
                    //    lblLastName.ForeColor = Color.Black;
                    //    lblCompanyName.ForeColor = Color.Red;
                    //    txtCompanyName.Enabled = true;
                    //}
                }
                else
                {
                    Found = false;
                }


                return Found;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString(), ex);
            }

            return Found;
        }
        protected void btnVerifyRESInPPS_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckIfBondExistInPPS())
                {
                    lblBondFound.Visible = true;
                    lblBondFound.Text = "RES Verified!";
                    //TxtPolicyNo.Text = TxtPolicyNo.Text.Trim() + "-" + DateTime.Parse(txtEffDt.Text.Trim()).Year.ToString().Substring(2).ToString();
                }
                else
                {
                    lblBondFound.Visible = true;
                    lblBondFound.Text = "RES NOT Verified";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.ToString(), ex);
            }
        }
        protected void ddlLiaLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            getBILimitValue();
        }
        protected void btnAdjuntar_Click(object sender, EventArgs e)
        {
            FillGridDocuments(true);
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
            customer = taskControl.Customer;

            //var uc = (UserControl)Page.LoadControl("~/AddDocuments.ascx");
            //Panel1.Controls.Add(uc);
            //ModalPopupExtender1.Show();
            //return;

            if (customer.CustomerNo == "0")
            {
                //ShowMessageDialog("You must save customer in order to proceed.");
            }
            else
            {
                txtDocumentDesc.Text = "";
                mpeAdjunto.Show();
            }
        }
        protected void ddlTransaction_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //Session["Transaction"] = ddlTransaction.SelectedIndex;
            mpeAdjunto.Show();
        }
        protected void ddlPolicyClass_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //Session["Transaction"] = ddlTransaction.SelectedIndex;
            mpeAdjunto.Show();
        }
        protected void gvAdjuntar_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
            customer = taskControl.Customer;
            int documentID = 0;
            try
            {
                if (e.CommandName.Trim() == "View")
                {
                    int index = Int32.Parse(e.CommandArgument.ToString());
                    GridViewRow row = gvAdjuntar.Rows[index];

                    System.Web.UI.WebControls.TableCell cell = row.Cells[1]; //ID is displayed in 2nd column  
                    int i = int.Parse(cell.Text);

                    documentID = i;

                    string fileName = System.Configuration.ConfigurationManager.AppSettings["RootURL"].ToString().Trim();

                    fileName = fileName + "Documents\\";

                    string[] fileNames = System.IO.Directory.GetFiles(fileName, @"*" + i.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + "*");

                    fileName = fileNames[0].ToString();

                    fileName = fileName.Substring(fileName.LastIndexOf('.'));

                    string fileType = fileName.Substring(fileName.LastIndexOf('.') + 1).ToUpper();

                    ddlTransaction.SelectedIndex = ddlTransaction.Items.IndexOf(ddlTransaction.Items.FindByValue(row.Cells[4].Text.Trim()));

                    //Session["Transaction"] = ddlTransaction.SelectedIndex;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('Documents/" + documentID.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + "" + fileName + "','" + fileType + "','status=yes,menubar,scrollbars=yes,resizable=yes,copyhistory=no,width=1150,height=725');", true);
                }
            }
            catch (Exception exp)
            {
                mpeSeleccion.Show();
                lblRecHeader.Text = exp.Message;
                return;
            }

            mpeAdjunto.Show();
        }
        protected void gvAdjuntar_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[1].Visible = false;

            }
            catch (Exception exc)
            {

            }
        }
        protected void gvAdjuntar_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                Customer.Customer customer = (Customer.Customer)Session["Customer"];
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
                customer = taskControl.Customer;
                int index = e.RowIndex;
                GridViewRow row = gvAdjuntar.Rows[index];
                System.Web.UI.WebControls.TableCell cell = row.Cells[1]; // ID is displayed in 2nd column  
                int i = int.Parse(cell.Text);

                //Se elimna de la tabla
                EPolicy.Customer.Customer.DeleteDocumentsByDocumentsID(i);

                //Se elimina el documento fisicamente
                string fileName = System.Configuration.ConfigurationManager.AppSettings["RootURL"].ToString().Trim();

                fileName = fileName + "Documents\\";

                string[] fileNames = System.IO.Directory.GetFiles(fileName, @"*" + i.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + "*");

                fileName = fileNames[0].ToString();

                //fileName = fileName + "Documents\\" + i.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".pdf";

                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "key", "alert('Document has been deleted!');", true);

                FillTextControl();
                mpeAdjunto.Show();
            }
            catch (Exception exp)
            {
                mpeSeleccion.Show();
                lblRecHeader.Text = exp.Message;
            }
        }
        protected void btnAdjuntarCargar_Click(object sender, EventArgs e)
        {
            try
            {
                Customer.Customer customer = (Customer.Customer)Session["Customer"];
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
                customer = taskControl.Customer;

                if (txtDocumentDesc.Text.Trim() == "")
                    throw new Exception("Please Fill the description Field.");

                if (ddlTransaction.Items.Count > 1)
                    if (ddlTransaction.SelectedItem.Text == "")
                        throw new Exception("Please Select a Transaction.");

                if (this.FileUpload1.PostedFile != null)
                {
                    if (FileUpload1.PostedFile.FileName == "")
                    {
                        throw new Exception("Please select a file from the browser.");
                    }
                }
                else
                {
                    throw new Exception("Please select a file from the browser.");
                }

                if (this.FileUpload1.PostedFile.FileName != "")
                {
                    if (this.FileUpload1.PostedFile != null)
                    {
                        string File = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf('.'));

                        switch (File.ToLower())
                        {
                            case ".pdf":

                                break;

                            case ".jpeg":

                                break;

                            case ".png":

                                break;

                            case ".jpg":

                                break;

                            default:

                                if (this.FileUpload1.PostedFile.FileName.Split(".".ToCharArray())[1].ToString().ToLower() != "pdf")
                                {
                                    throw new Exception("The File Format is not supported.");
                                }
                                break;
                        }

                        if (this.FileUpload1.PostedFile.ContentLength > 12000001)
                        {
                            throw new Exception("The file size must be up to 12MB.");
                        }
                    }
                }

                //SaveDocuments
                int docid = EPolicy.Customer.Customer.Savedocuments(customer.CustomerNo.ToString(), txtDocumentDesc.Text.Trim(), ddlTransaction.SelectedItem.Value.Trim(), taskControl.TaskControlTypeID.ToString());

                //Upload Document
                if (FileUpload1.PostedFile.FileName != null)
                {
                    string fileName = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf('.'));


                    switch (fileName.ToLower())
                    {
                        case ".pdf":

                            fileName = Server.MapPath("./Documents/") + docid.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".pdf";
                            break;

                        case ".jpeg":
                            fileName = Server.MapPath("./Documents/") + docid.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".jpeg";
                            break;

                        case ".png":
                            fileName = Server.MapPath("./Documents/") + docid.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".png";
                            break;

                        case ".jpg":
                            fileName = Server.MapPath("./Documents/") + docid.ToString().Trim() + "_" + customer.CustomerNo.ToString().Trim() + ".jpg";
                            break;

                        default:
                            break;
                    }

                    FileUpload1.PostedFile.SaveAs(fileName);

                    FillGridDocuments(false);
                    txtDocumentDesc.Text = "";
                    ddlTransaction.SelectedIndex = 1;
                    Session["Transaction"] = null;
                    mpeAdjunto.Show();
                }
            }
            catch (Exception exp)
            {
                ddlTransaction.SelectedIndex = 1;
                mpeSeleccion.Show();
                lblRecHeader.Text = exp.Message;
                return;
            }
        }
        protected void btnAddVehicle_Click(object sender, EventArgs e)
        {
            try
            {
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];

                string tempDocID = "0";


                //////////////////////
                //myRow["HasCoverageExplain"] = TxtExplain.Text.Trim();
                Login.Login cp = HttpContext.Current.User as Login.Login;


                //FillBondReqDocumentsGridLoad();


                //this.GridVehicle.CurrentPageIndex = 0;

                txtIDRoadAssist.Text = "";
            }
            catch (Exception xcp)
            {

                lblRecHeader.Text = xcp.Message;  //"You can only add a maximun of two cars.";// + taskControl.Mode.ToString() + CUSTOMER2.ToString();
                mpeSeleccion.Show();
            }
        }

        #endregion
        #region LocalFunction
        public void SendPolicyToPPS(int TaskControlID)
        {
            string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();

            SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            DataTable PPSPolicy = new DataTable();
            DataTable dt = GetBondsToPPSByTaskControlID(TaskControlID);
            DataTable dtBond = GetBondInfoToPPS(TaskControlID);

            if (dt.Rows.Count > 0)
            {

                string BndTypeID = "";
                string BndReinsASL = "";

                if (dtBond.Rows.Count > 0)
                {
                    BndTypeID = dtBond.Rows[0]["TypeOfBondID"].ToString().Trim();
                    BndReinsASL = dtBond.Rows[0]["ReinsASL"].ToString().Trim();
                }

                cmd.CommandText = "sproc_ConsumeXMLePPS-BONDS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();


                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Incept", DateTime.Parse(dt.Rows[0]["EffectiveDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00");
                cmd.Parameters.AddWithValue("@Expire", DateTime.Parse(dt.Rows[0]["ExpirationDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00");

                string FN = dt.Rows[0]["FirstNa"].ToString().Trim();
                string LN = dt.Rows[0]["LastNa1"].ToString().Trim();
                string BusType = "1";
                string BusFlag = "0";

                if (this.ddlType.SelectedItem.Text.ToString() != "Individual")
                {
                    FN = "";
                    LN = txtCompanyName.Text.Trim();
                    BusFlag = "0";
                }

                if (this.ddlType.SelectedItem.Text.ToString() == "Corporate")
                {
                    BusType = "2";
                    BusFlag = "1";
                }
                else if (this.ddlType.SelectedItem.Text.ToString() == "DBA")
                {
                    BusType = "3";
                    BusFlag = "1";
                }

                string ComRate = "0.0000000e+000";

                DataTable DtCommision = GetCommissionAgentRateByAgentID(TaskControlID.ToString(), "26"); //GetCommissionAgentRateByAgentID(AgentID.ToString().Trim(), "22");

                if (DtCommision.Rows.Count > 0)
                {
                    ComRate = DtCommision.Rows[0]["CommissionRate"].ToString();

                    ComRate = (double.Parse(ComRate) / 100).ToString();
                }
                else
                {
                    ComRate = "0.0000000e+000";
                }

                cmd.Parameters.AddWithValue("@BrokerID", dt.Rows[0]["CarsID"].ToString().Trim());
                cmd.Parameters.AddWithValue("@CanDate", "");
                cmd.Parameters.AddWithValue("@TmpTime", "");
                cmd.Parameters.AddWithValue("@BinderID", "true");
                cmd.Parameters.AddWithValue("@ComRate", ComRate);
                cmd.Parameters.AddWithValue("@Tag", "true");
                cmd.Parameters.AddWithValue("@Premium", dt.Rows[0]["TotalPremium"].ToString().Trim());
                cmd.Parameters.AddWithValue("@DispImage", "Policy");
                cmd.Parameters.AddWithValue("@SpecEndorse", "");
                cmd.Parameters.AddWithValue("@SID", "0");
                cmd.Parameters.AddWithValue("@UDPolicyID", "0");
                cmd.Parameters.AddWithValue("@PreparedBy", dt.Rows[0]["EnteredBy"].ToString().Trim());
                cmd.Parameters.AddWithValue("@ExcessLink", "0");
                cmd.Parameters.AddWithValue("@PolSubType", "");
                cmd.Parameters.AddWithValue("@ReinsPcnt", "0.0000000e+000");
                cmd.Parameters.AddWithValue("@Assessment", "0.0000");
                cmd.Parameters.AddWithValue("@PayDate", "");
                cmd.Parameters.AddWithValue("@Polrelat", "NI");
                cmd.Parameters.AddWithValue("@LastName", LN); //dt.Rows[0]["LastNa1"].ToString().Trim());
                cmd.Parameters.AddWithValue("@FirstName", FN); //dt.Rows[0]["FirstNa"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Middle", dt.Rows[0]["Initial"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Upid", "0");
                cmd.Parameters.AddWithValue("@Dob", dt.Rows[0]["Birthday"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Sex", dt.Rows[0]["Sex"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Marital", dt.Rows[0]["MaritalStatus"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Yrsexp", "0");
                cmd.Parameters.AddWithValue("@License", "true");
                cmd.Parameters.AddWithValue("@State", dt.Rows[0]["State"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Ssn", "SSn");
                cmd.Parameters.AddWithValue("@BusFlag", BusFlag);
                cmd.Parameters.AddWithValue("@Nsbyt", "0");
                cmd.Parameters.AddWithValue("@BusOther", "0");
                cmd.Parameters.AddWithValue("@BusType", BusType);
                cmd.Parameters.AddWithValue("@Client", "0");
                cmd.Parameters.AddWithValue("@Maddr1", dt.Rows[0]["Adds1"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Maddr2", dt.Rows[0]["Adds2"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Maddr3", "");
                cmd.Parameters.AddWithValue("@Mcity", dt.Rows[0]["City"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Mstate", dt.Rows[0]["State"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Mnation", "");
                cmd.Parameters.AddWithValue("@Mzip", dt.Rows[0]["Zip"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Raddr1", dt.Rows[0]["Adds1PH"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Raddr2", dt.Rows[0]["Adds2PH"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Raddr3", "");
                cmd.Parameters.AddWithValue("@Rcity", dt.Rows[0]["CityPH"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Rstate", dt.Rows[0]["StatePH"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Rnation", "");
                cmd.Parameters.AddWithValue("@Rzip", dt.Rows[0]["ZipPH"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Wphone", dt.Rows[0]["JobPhone"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Rphone", dt.Rows[0]["Cellular"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Csbyt", "0");
                cmd.Parameters.AddWithValue("@Cphone", dt.Rows[0]["Cellular"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Eaddr", dt.Rows[0]["Email"].ToString().Trim());
                cmd.Parameters.AddWithValue("@ReinsAsl", BndReinsASL);
                cmd.Parameters.AddWithValue("@Lim1", dt.Rows[0]["Limits"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Lim2", "0.0000");
                cmd.Parameters.AddWithValue("@Island", "4");
                cmd.Parameters.AddWithValue("@BNDType", BndTypeID);

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(PPSPolicy);

                //cmd.ExecuteReader();
            }

            sqlConnection1.Close();

            if (PPSPolicy.Rows.Count > 0)
            {
                TxtPolicyNo.Text = PPSPolicy.Rows[0]["PolicyID"].ToString().Trim().Replace("BND", "");
                string ClientID = PPSPolicy.Rows[0]["Client"].ToString().Trim();
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
                taskControl.Customer.Description = ClientID;
                taskControl.PolicyNo = TxtPolicyNo.Text;
                UpdatePolicyFromPPSByTaskControlID(TaskControlID, TxtPolicyNo.Text, ClientID);
            }
        }
        public void PrintIndemnityQuote(List<string> mergePaths, string ProcessedPath)
        {
            try
            {
                FileInfo mFileIndex;
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];

                mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/Bonds/Indemnity_Bond_Quote.pdf");
                mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Indemnity_Bond_Quote" + taskControl.TaskControlID.ToString() + ".pdf", true);
                mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "Indemnity_Bond_Quote" + taskControl.TaskControlID.ToString() + ".pdf");

                //DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                DataTable dt = null;

                Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()},
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},
                { "NameToPrint2", dt.Rows[0]["NameToPrint"].ToString().Trim()}};

                //PrintBondsMSWord("Indemnity_Bond_2", bookmarks, mergePaths, ProcessedPath);

            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
        public void PrintIndemnityPolicy(List<string> mergePaths, string ProcessedPath)
        {
            try
            {
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];

                //DataTable dt = GetBondInfoPolicy(taskControl.TaskControlID);
                DataTable dt = null;
                Dictionary<string, string> bookmarks = new Dictionary<string, string> { 
                { "PolicyNo",dt.Rows[0]["PolicyNo"].ToString().Trim()}
                };

                PrintRESMSWord("Indemnity_Bond", bookmarks, mergePaths, ProcessedPath);

                Dictionary<string, string> bookmarks2 = new Dictionary<string, string> { 
                { "NameToPrint", dt.Rows[0]["NameToPrint"].ToString().Trim()},
                { "Day", DateTime.Now.Day.ToString()},{ "Month", DateTime.Now.ToString("MMMM")},{ "Year", DateTime.Now.Year.ToString()},
                { "CompleteAddress", dt.Rows[0]["Adds11"].ToString().Trim() + " " + dt.Rows[0]["Adds21"].ToString().Trim() 
                + ", " + dt.Rows[0]["City1"].ToString().Trim() + ", " + dt.Rows[0]["State1"].ToString().Trim()+ " " + dt.Rows[0]["Zip1"].ToString().Trim()},
                { "NameToPrint2", dt.Rows[0]["NameToPrint"].ToString().Trim()}};

                PrintRESMSWord("Indemnity_Bond_2", bookmarks2, mergePaths, ProcessedPath);

            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
        public void DisableControls()
        {
            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

            //Botones 

            //btnCashPayment.Enabled = false;

            if (taskControl.TaskControlID.ToString() != "0")
            {
                DataTable dtPayment = GetPaymentByTaskControlID();

                btnPrintInvoice.Visible = false;
                btnPrintPolicy.Visible = false; // false Alexis
                //btnIndemnityPolicy.Visible = false;
                btnInvoice.Visible = false;
                btnPrintQuote.Visible = false;
                ddlPrintOption.Visible = false;
                //btnGuardianPay.Visible = false;

            }


            btnAdd2.Visible = false;
            //btnNew.Visible = true;
            btnPrintPolicy.Visible = false;
            if (btnPrintPolicy.Visible == true)
            {
                btnEdit.Visible = false;
                btnPreview.Visible = false;
                //btnIndemnityQuote.Visible = false;
            }
            else
            {
                btnEdit.Visible = true;
                btnPreview.Visible = true;
                //btnIndemnityQuote.Visible = true;

                if (TxtPolicyNo.Text.Trim() == "")
                {
                    btnAcceptQuote.Visible = true;
                    btnInvoice.Visible = false;
                    btnPrintInvoice.Visible = false;
                    btnPreview.Visible = false;
                    //btnIndemnityPolicy.Visible = false;

                }
                else
                {
                    //if (!lblBondFound.Visible)
                    //{
                    btnAcceptQuote.Visible = false;
                    btnEdit.Visible = false;
                    btnPreview.Visible = false;
                    btnPrintPolicy.Visible = true;
                    btnInvoice.Visible = true;
                    btnPrintQuote.Visible = true;
                    //}
                }
            }

            BtnExit.Visible = true;
            BtnSave.Visible = false;
            btnCancel.Visible = false;
            //btnDelete.Visible = true;
            btnReinstallation.Visible = false;
            btnCancellation.Visible = false;

            chkSameMailing.Visible = true;
            chkSameMailing.Enabled = false;
            ChkAutoAssignPolicy.Enabled = false;

            TxtProspectNo.Enabled = false;
            TxtFirstName.Enabled = false;
            txtLastname1.Enabled = false;
            TxtInitial.Enabled = false;
            TxtAddrs1.Enabled = false;
            TxtAddrs2.Enabled = false;
            TxtState.Enabled = false;

            txtPhyAddress.Enabled = false;
            txtPhyAddress2.Enabled = false;
            txtPhyState.Enabled = false;
            txtSocSec.Enabled = false;


            TxtInitial.Visible = true;
            TxtAddrs1.Visible = true;
            TxtAddrs2.Visible = true;
            TxtState.Visible = true;
            TxtTerm.Visible = true;
            txtPhyAddress.Visible = true;
            txtPhyAddress2.Visible = true;
            txtPhyState.Visible = true;

            //TxtPolicyNo.Visible = true;

            if (TxtPolicyNo.Text.Trim() != "")
            {
                txtPolicyNoToRenew.Visible = false;
                btnPrintQuote.Visible = false;
                //TxtPolicyNo.Visible = true;
            }
            else
            {
                txtPolicyNoToRenew.Visible = true;
                txtPolicyNoToRenew.Enabled = false;
                //TxtPolicyNo.Visible = false;
            }

            TxtPolicyType.Visible = false;
            TxtSufijo.Visible = false;
            TxtCity.Enabled = false;
            TxtInitial.Visible = true;
            TxtCity.Visible = false;

            TxtOccupa.Visible = false;
            TxtLicense.Visible = false;
            TxtOccupa.Enabled = false;
            TxtLicense.Enabled = false;
            lblPolicyNo.Visible = true;
            lblPolicyType.Visible = false;
            lblSuffix.Visible = false;
            lblSelectedAgent.Visible = true;
            lblOccupa.Visible = false;
            lblLicense.Visible = false;
            lblInitial.Visible = true;
            lbladdress1.Visible = true;
            lbladdress2.Visible = true;
            lblCity.Visible = true;
            lblZipCode.Visible = true;
            lblState.Visible = true;
            lblTerm.Visible = true;

            TxtHomePhone.Enabled = false;
            txtWorkPhone.Enabled = false;
            TxtCellular.Enabled = false;
            txtEmail.Enabled = false;
            TxtPolicyNo.Enabled = false;
            TxtPolicyType.Enabled = false;
            TxtSufijo.Enabled = false;
            TxtTerm.Enabled = false;
            txtEffDt.Enabled = false;
            txtExpDt.Enabled = false;
            txtEntryDate.Enabled = false;
            txtTotalPremium.Enabled = false;
            imgCalendarEff.Visible = false;

            ddlZip.Enabled = false;
            ddlPhyZipCode.Enabled = false;
            ddlZip.Visible = true;
            ddlPhyZipCode.Visible = true;
            ddlPhyCity.Enabled = false;
            ddlCiudad.Enabled = false;
            ddlCiudad.Visible = true;
            ddlPhyCity.Visible = true;
            ddlOriginatedAt.Enabled = false;
            ddlInsuranceCompany.Enabled = false;
            ddlAgency.Enabled = false;
            ddlAgent.Enabled = false;
            //ddlPaymentAmount.Enabled = false;

            ddlBank.Enabled = false;
            ddlCompanyDealer.Enabled = false;

            // Disable auto control

            // Option Print

            lblPrintOption.Visible = false;
            chkInsured.Visible = false;
            chkProducer.Visible = false;
            chkCompany.Visible = false;
            chkAgency.Visible = false;
            chkExtraCopy.Visible = false;

            ddlType.Enabled = false;
            txtCompanyName.Enabled = false;
            EnableDisableRESParameters(false);
            VerifyAccess();
        }
        public static string NumberToCurrencyText(decimal number) //, MidpointRounding midpointRounding)
        {

            // Round the value just in case the decimal value is longer than two digits      

            number = decimal.Round(number, 2); //, midpointRounding);       

            string wordNumber = string.Empty;

            // Divide the number into the whole and fractional part strings      

            string[] arrNumber = number.ToString().Split('.');

            // Get the whole number text      

            long wholePart = long.Parse(arrNumber[0]);

            string strWholePart = NumberToText(wholePart);

            // For amounts of zero dollars show 'No Dollars...' instead of 'Zero Dollars...'  

            wordNumber = (wholePart == 0 ? "No" : strWholePart) + (wholePart == 1 ? " Dollar and " : " Dollars and ");

            // If the array has more than one element then there is a fractional part otherwise there isn't   

            // just add 'No Cents' to the end     

            if (arrNumber.Length > 1)
            {

                // If the length of the fractional element is only 1, add a 0 so that the text returned isn't,    

                // 'One', 'Two', etc but 'Ten', 'Twenty', etc.          

                long fractionPart = long.Parse((arrNumber[1].Length == 1 ? arrNumber[1] + "0" : arrNumber[1]));

                string strFarctionPart = NumberToText(fractionPart);

                wordNumber += (fractionPart == 0 ? " No" : strFarctionPart) + (fractionPart == 1 ? " Cent" : " Cents");

            }

            else

                wordNumber += "No Cents";

            return "**" + wordNumber + "**";

        }
        public static string NumberToText(long number)
        {

            StringBuilder wordNumber = new StringBuilder();

            string[] powers = new string[] { "Thousand ", "Million ", "Billion " };

            string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            string[] ones = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",

                "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };



            if (number == 0)
            {

                return "Zero";

            }



            if (number < 0)
            {

                wordNumber.Append("Negative ");

                number = -number;

            }



            long[] groupedNumber = new long[] { 0, 0, 0, 0 };

            int groupIndex = 0;



            while (number > 0)
            {

                groupedNumber[groupIndex++] = number % 1000;

                number /= 1000;

            }



            for (int i = 3; i >= 0; i--)
            {

                long group = groupedNumber[i];



                if (group >= 100)
                {

                    wordNumber.Append(ones[group / 100 - 1] + " Hundred ");

                    group %= 100;



                    if (group == 0 && i > 0)

                        wordNumber.Append(powers[i - 1]);

                }



                if (group >= 20)
                {

                    if ((group % 10) != 0)

                        wordNumber.Append(tens[group / 10 - 2] + " " + ones[group % 10 - 1] + " ");

                    else

                        wordNumber.Append(tens[group / 10 - 2] + " ");

                }

                else

                    if (group > 0)

                        wordNumber.Append(ones[group - 1] + " ");

                if (group != 0 && i > 0)

                    wordNumber.Append(powers[i - 1]);

            }

            return wordNumber.ToString().Trim();

        }
        public string enletras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = NumberToCurrencyTextESP(Convert.ToDouble(entero)) + dec;
            return res;
        }
        public bool VerificarAutos(DataTable dt)
        {
            bool ver = false;
            try
            {
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double Premium = double.Parse(dt.Rows[i]["Premium"].ToString());

                        if (Premium == 44.0)
                            ver = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return ver;
        }
        public bool CheckPolicyNo(string policyType, string policyNo, string certificate, string sufijo)
        {
            DataTable dt = Policy.GetPolicyByPolicyNo(policyType, policyNo, certificate, sufijo);

            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void SetDDLValue(DropDownList DropDownList, string taskControlValue, string findBy)
        {
            if (DropDownList.ID == "ddlCiudad" || DropDownList.ID == "ddlZip" || DropDownList.ID == "ddlPhyCity" || DropDownList.ID == "ddlPhyCity")
            {
                if (taskControlValue != "")
                {
                    for (int i = 0; DropDownList.Items.Count - 1 >= i; i++)
                    {
                        if (findBy == "Text")
                        {
                            if (DropDownList.Items[i].Text.Trim() == taskControlValue.ToString().Trim())
                            {
                                DropDownList.SelectedIndex = i;
                                break;
                            }
                        }
                        else
                        {
                            if (DropDownList.Items[i].Value.Trim() == taskControlValue.ToString().Trim())
                            {
                                DropDownList.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                DropDownList.SelectedIndex = 0;
                if (taskControlValue != "")
                {
                    for (int i = 0; DropDownList.Items.Count - 1 >= i; i++)
                    {
                        if (findBy == "Value")
                        {
                            if (DropDownList.Items[i].Value.Trim() == taskControlValue.ToString().Trim())
                            {
                                DropDownList.SelectedIndex = i;
                                break;
                            }
                        }
                        else
                        {
                            if (DropDownList.Items[i].Text.Trim() == taskControlValue.ToString().Trim())
                            {
                                DropDownList.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
        }
        private DataTable GetPaymentByTaskControlID()
        {

            DataTable dt = null;
            try
            {
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];

                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
                DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, taskControl.TaskControlID.ToString(), ref cookItems);
                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }

                Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
                dt = exec.GetQuery("GetPaymentByTaskControlID", xmlDoc);

            }
            catch (Exception ep)
            {
            }
            return dt;
        }
        private static DataTable GetAgentByUserID(string UserID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("UserID",
                SqlDbType.VarChar, 10, UserID.ToString(),
                ref cookItems);


            DBRequest exec = new DBRequest();
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
                dt = exec.GetQuery("GetAgentByUserID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the liability rates.", ex);
            }
        }
        private void FillTextControl()
        {
            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];

            LblControlNo.Text = taskControl.TaskControlID.ToString().Trim();
            TxtPolicyNo.Text = taskControl.PolicyID.ToString();
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            //Busca la localidad del usuario
            taskControl.OriginatedAt = EPolicy.Login.Login.GetLocationByUserID(userID);

            ChkAutoAssignPolicy.Checked = taskControl.AutoAssignPolicy;



            if (taskControl.OriginatedAt != 0)
                ddlOriginatedAt.SelectedIndex = ddlOriginatedAt.Items.IndexOf(
                    ddlOriginatedAt.Items.FindByValue(taskControl.OriginatedAt.ToString()));
            //ddlOriginatedAt.SelectedIndex = int.Parse(taskControl.OriginatedAt.ToString());

            if (taskControl.InsuranceCompany != "")
                ddlInsuranceCompany.SelectedIndex = ddlInsuranceCompany.Items.IndexOf(
                    ddlInsuranceCompany.Items.FindByValue(taskControl.InsuranceCompany.ToString()));

            if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("AUTO VI ADMINISTRATOR"))
            {
                if (taskControl.Agent.Trim() != "000" && taskControl.Agent.Trim() != "")
                {
                    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
                        ddlAgent.Items.FindByValue(taskControl.Agent.Trim()));
                }
                else
                {
                    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
                        ddlAgent.Items.FindByValue("060"));
                }

                ddlAgent.Enabled = true;

            }
            else
            {
                DataTable dtAgentByUserID = GetAgentByUserID(cp.UserID.ToString());
                string Agent = "000";

                if (dtAgentByUserID.Rows.Count > 0)
                {
                    Agent = dtAgentByUserID.Rows[0]["AgentID"].ToString();
                }

                if (taskControl.Agent.Trim() != "000" && taskControl.Agent.Trim() != "")
                {
                    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
                        ddlAgent.Items.FindByValue(taskControl.Agent.Trim()));
                }
                else
                {
                    ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
                        ddlAgent.Items.FindByValue(Agent.Trim()));
                }

                ddlAgent.Enabled = false;
            }

            if (taskControl.Agency != "")
                ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(
                    ddlAgency.Items.FindByValue(taskControl.Agency));

            if (taskControl.Bank != "")
            {
                ddlBank.SelectedIndex = ddlBank.Items.IndexOf(ddlBank.Items.FindByValue(taskControl.Bank));
            }

            if (taskControl.CompanyDealer != "")
            {
                ddlCompanyDealer.SelectedIndex = ddlCompanyDealer.Items.IndexOf(ddlCompanyDealer.Items.FindByValue(taskControl.CompanyDealer));
            }

            ddlCiudad.Text = taskControl.Customer.City.Trim();
            ddlPhyCity.Text = taskControl.Customer.CityPhysical.Trim();

            ddlZip.Text = taskControl.Customer.ZipCode;
            ddlCiudad.Text = taskControl.Customer.City;
            ddlPhyZipCode.Text = taskControl.Customer.ZipPhysical;
            ddlPhyCity.Text = taskControl.Customer.CityPhysical;

            DataTable dtTask = new DataTable();

            LblControlNo.Text = taskControl.TaskControlID.ToString();
            TxtProspectNo.Text = taskControl.Customer.CustomerNo;

            TxtLicense.Text = taskControl.Customer.Licence;
            TxtOccupa.Text = taskControl.Customer.Occupation;
            //TxtBirthdate.Text = taskControl.Customer.Birthday;

            TxtFirstName.Text = taskControl.Customer.FirstName;
            TxtInitial.Text = taskControl.Customer.Initial;
            txtLastname1.Text = taskControl.Customer.LastName1;
            //txtLastname2.Text = taskControl.Customer.LastName2;
            TxtAddrs1.Text = taskControl.Customer.Address1;
            TxtAddrs2.Text = taskControl.Customer.Address2;
            if (cp.IsInRole("RESVI"))
                TxtState.Text = taskControl.Customer.State == "" ? "St.Thomas" : taskControl.Customer.State;
            if (cp.IsInRole("RES") || cp.IsInRole("ADMINISTRATOR"))
                TxtState.Text = taskControl.Customer.State == "" ? "PR" : taskControl.Customer.State;
            TxtHomePhone.Text = taskControl.Customer.HomePhone;
            txtWorkPhone.Text = taskControl.Customer.JobPhone;
            TxtCellular.Text = taskControl.Customer.Cellular;
            txtEmail.Text = taskControl.Customer.Email;
            TxtPolicyNo.Text = taskControl.PolicyNo;
            txtCompanyName.Text = taskControl.Customer.LastName2;
            if (TxtPolicyNo.Text.Trim() != "")
            {
                txtPolicyNoToRenew.Visible = false;
                txtSuffix.Text = taskControl.Suffix;
                //TxtPolicyNo.Visible = true;
            }
            else
            {
                txtPolicyNoToRenew.Visible = true;
                txtPolicyNoToRenew.Enabled = false;
                //TxtPolicyNo.Visible = false;
            }
            TxtPolicyType.Text = taskControl.PolicyType;
            TxtSufijo.Text = taskControl.Suffix;
            TxtTerm.Text = taskControl.Term.ToString();
            if (taskControl.EffectiveDate.ToString() != "" && taskControl.ExpirationDate.ToString() != "")
            {
                txtEffDt.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(taskControl.EffectiveDate));
                txtExpDt.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(taskControl.ExpirationDate));
            }
            else
            {
                txtEffDt.Text = System.DateTime.Today.ToShortDateString();
                txtExpDt.Text = System.DateTime.Today.AddYears(1).ToShortDateString();
            }


            txtEntryDate.Text = taskControl.EntryDate.ToShortDateString();

            //txtTotalPremium.Text = taskControl.TotalPremium.ToString("###,###.00"); //Se descomento para que llenara el textbox y se pueda salvar la poliza sin cambiar el dropdown de premium
            if (taskControl.TotalPremium.ToString() == "0") //aqui
            { } //txtPremium.Text = taskControl.TotalPremium.ToString("###,###"); //Se descomento para que llenara el textbox y se pueda salvar la poliza sin cambiar el dropdown de premium
            else { }
            //txtPremium.Text = taskControl.TotalPremium.ToString("$###,###.00");


            txtPhyAddress.Text = taskControl.Customer.AddressPhysical1;
            txtPhyAddress2.Text = taskControl.Customer.AddressPhysical2;

            if (cp.IsInRole("RESVI"))
                txtPhyState.Text = taskControl.Customer.State == "" ? "St.Thomas" : taskControl.Customer.State;
            if (cp.IsInRole("RES") || cp.IsInRole("ADMINISTRATOR"))
                txtPhyState.Text = taskControl.Customer.State == "" ? "PR" : taskControl.Customer.State;


            txtTotalPremium.Text = taskControl.TotalPremium.ToString();

            EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();



            if (taskControl.Customer.SocialSecurity.Trim() != "")
            {
                txtSocSec.Text = encrypt.Decrypt(taskControl.Customer.SocialSecurity);
                txtSocSec.Text = new string('*', txtSocSec.Text.Trim().Length - 4) + txtSocSec.Text.Trim().Substring(txtSocSec.Text.Trim().Length - 4);
                MaskedEditExtender1.Mask = "???-??-9999";
            }
            else
                txtSocSec.Text = "";

            FillReqDocsGrid();
            if (taskControl.TypePolicy == 2)
            {
                ddlType.SelectedIndex = 1;
                CustomerTypeSelection();
            }
            LoadRESParameters(taskControl);

            if (cp.IsInRole("RESVI"))
            {
                //ddlObligee.Enabled = false;
            }

            if (taskControl.TaskControlID != 0 && BtnSave.Text != "ISSUE Policy")
            {
                btnAdjuntar.Visible = true;
                btnAdjuntar.Enabled = true;
            }
        }
        private void HideControls(Control id, bool condition)
        {
            try
            {
                id.Visible = condition;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LogError(Exception exp)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", exp.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", exp.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", exp.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", exp.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            string path = Server.MapPath("~/ErrorLog/ErrorLogBond.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
        private void LogErrorBond(Exception exp, string line)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", exp.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", exp.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", exp.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", exp.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Line of error: {0}", line);
            string path = Server.MapPath("~/ErrorLog/ErrorLogBond.txt");
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }
        private void VerifyAccess()
        {
            try
            {
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];

                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;

                if (!cp.IsInRole("RENEWOPP") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    this.btnRenew.Visible = false;
                }
                else
                {
                    if (taskControl.IsEndorsement.ToString() == "False")
                    {
                        this.btnRenew.Visible = false; // true alexis
                    }
                }

                if (!cp.IsInRole("BTNCONVERTOPTIMAPERSONALPACKAGE") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    //this.btnCancellation.Visible = false;
                }
                if (!cp.IsInRole("ROADASSISTANCE REINSTATEMENT") && !cp.IsInRole("ADMINISTRATOR"))
                {
                    this.btnReinstallation.Visible = false;
                }
                else
                {
                    if (taskControl.CancellationDate != "")
                    {
                        btnReinstallation.Visible = true;
                    }
                }
            }
            catch (Exception xp)
            {

            }
        }
        private static DataTable GetCommissionAgentRateByAgentID(string TaskControlID, string PolicyClassID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[2];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.VarChar, 10, TaskControlID.ToString(),
                ref cookItems);

            DbRequestXmlCooker.AttachCookItem("PolicyClassID",
                SqlDbType.VarChar, 10, PolicyClassID.ToString(),
                ref cookItems);


            DBRequest exec = new DBRequest();
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
                dt = exec.GetQuery("GetCommissionAgentRateByAgentID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the liability rates.", ex);
            }
        }
        private static DataTable UpdatePolicyFromPPSByTaskControlID(int TaskControl, string PolicyNo, string ClientID)
        {

            DataTable dt = new DataTable();

            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[3];

                DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, TaskControl.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("PolicyNo", SqlDbType.VarChar, 50, PolicyNo.ToString(), ref cookItems);
                DbRequestXmlCooker.AttachCookItem("ClientID", SqlDbType.VarChar, 50, ClientID.ToString(), ref cookItems);

                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }

                dt = Executor.GetQuery("UpdatePolicyFromPPSByTaskControlID", xmlDoc);
                return dt;

            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items." + ex.ToString(), ex);
            }

            return dt;

        }
        private static DataTable GetTypeOfBond()
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[0];


            DBRequest exec = new DBRequest();
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
                dt = exec.GetQuery("GetTypeOfBond", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get Type Of Bond.", ex);
            }
        }
        private static DataTable GetBondsToPPSByTaskControlID(int TaskControlID)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.VarChar, 10, TaskControlID.ToString(), ref cookItems);


            DBRequest exec = new DBRequest();
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
                dt = exec.GetQuery("GetBondsToPPSByTaskControlID", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the Stored Procedure called GetBondsToPPSByTaskControlID.", ex);
            }
        }
        private static DataTable GetBondInfoToPPS(int TaskControlID)
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, TaskControlID.ToString(), ref cookItems);


            DBRequest exec = new DBRequest();
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
                dt = exec.GetQuery("GetBondInfoToPPS", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve the Stored Procedure called GetBondInfoToPPS.", ex);
            }
        }
        private void DeleteRoadAssist(int DocID)
        {

            try
            {
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];

                DataTable dt = null;

                FillReqDocsGrid();
            }
            catch (Exception xcp)
            {
                throw new Exception("Error deleting row.", xcp);
            }

        }
        private void FillGridDocuments(bool Refresh)
        {
            Login.Login cp = HttpContext.Current.User as Login.Login;
            Customer.Customer customer = (Customer.Customer)Session["Customer"];
            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
            customer = taskControl.Customer;

            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            gvAdjuntar.DataSource = null;
            System.Data.DataTable DtCert = null;
            System.Data.DataTable dtTransaction = null;

            if (customer.CustomerNo != "")
            {
                    DtCert = EPolicy.Customer.Customer.GetDocumentsByCustomerNo(taskControl.CustomerNo, taskControl.TaskControlID, taskControl.isQuote ? 0 : taskControl.TCIDQuotes);
                dtTransaction = TaskControl.TaskControl.GetTaskControlByCustomerNo(customer.CustomerNo, userID);
            }

            if ((dtTransaction != null && !IsPostBack) || Refresh)
            {
                if (dtTransaction.Rows.Count > 0)
                {
                    for (int i = dtTransaction.Rows.Count - 1; i >= 0; i--)
                    {
                        if (taskControl.TaskControlID != 0)
                            if (dtTransaction.Rows[i]["TaskControlID"].ToString().Trim() != taskControl.TaskControlID.ToString().Trim())
                            {
                                if (dtTransaction.Rows[i]["TaskControlID"].ToString().Trim() != taskControl.TCIDQuotes.ToString().Trim())
                                    dtTransaction.Rows.RemoveAt(i);
                            }
                    }

                    //Transaction
                    ddlTransaction.DataSource = dtTransaction;
                    ddlTransaction.DataTextField = "TaskControlTypeID";
                    ddlTransaction.DataValueField = "TaskControlID";
                    ddlTransaction.DataBind();
                    ddlTransaction.SelectedIndex = -1;
                    ddlTransaction.Items.Insert(0, "");

                    if (ddlTransaction.Items.Count > 1)
                        foreach (System.Web.UI.WebControls.ListItem item in ddlTransaction.Items)
                        {
                            if (item.Text != "")
                            {
                                DataRow[] Row = dtTransaction.Select("TaskControlID = '" + item.Value + "'");
                                item.Text = Row[0]["TaskControlTypeDesc"].ToString().Trim().Contains("Home Owners") ? Row[0]["TaskControlTypeDesc"].ToString().Trim().Replace("Home Owners", "Residential Property") + " - " + Row[0]["TaskControlID"].ToString().Trim() : Row[0]["TaskControlTypeDesc"].ToString().Trim() + " - " + Row[0]["TaskControlID"].ToString().Trim();
                            }
                        }
                    ddlTransaction.SelectedIndex = 1;
                }
            }
            else
            { }

            if (DtCert != null)
            {
                if (DtCert.Rows.Count != 0)
                {
                    gvAdjuntar.DataSource = DtCert;
                    gvAdjuntar.DataBind();
                }
                else
                {
                    gvAdjuntar.DataSource = null;
                    gvAdjuntar.DataBind();
                }
            }
            else
            {
                gvAdjuntar.DataSource = null;
                gvAdjuntar.DataBind();
            }
        }
        private string GetAgentByCarsID(string CarsID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("CarsID",
                SqlDbType.VarChar, 10, CarsID.ToString(),
                ref cookItems);
            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            DataTable dt;
            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            dt = exec.GetQuery("GetAgentByCarsIDBND", xmlDoc);
            string rtAgentID = "0";

            if (dt.Rows.Count > 0)
            {
                rtAgentID = dt.Rows[0]["dAgentID"].ToString();
            }
            return rtAgentID.ToString();
        }
        private static System.Data.DataTable GetVerifyPolicyExist(int TaskControlID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
                ref cookItems);


            DBRequest exec = new DBRequest();
            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
            System.Data.DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetVerifyPolicyExist", xmlDoc);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data from database.", ex);
            }

        }
        private void FillReqDocsGridLoad()
        {

        }
        private string GetDateInSpanish(string Month)
        {
            switch (Month)
            {
                case "January":
                    return "enero";
                case "February":
                    return "febrero";
                case "March":
                    return "marzo";
                case "April":
                    return "abril";
                case "May":
                    return "mayo";
                case "June":
                    return "junio";
                case "July":
                    return "julio";
                case "August":
                    return "agosto";
                case "September":
                    return "septiembre";
                case "October":
                    return "octubre";
                case "November":
                    return "noviembre";
                case "December":
                    return "diciembre";
                default:
                    throw new Exception("Could not translate month into spanish date.");
            }
        }
        private string PrintPreview(string rdlcDoc)
        {
            return "";
            //try
            //{
            //    EPolicy.TaskControl.Bonds taskControl = (EPolicy.TaskControl.Bonds)Session["TaskControl"];
            //    ReportViewer viewer = new ReportViewer();
            //    string ProcessPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];

            //    viewer.LocalReport.DataSources.Clear();
            //    viewer.ProcessingMode = ProcessingMode.Local;
            //    viewer.LocalReport.ReportPath = Server.MapPath("Reports/GuardianXtra/" + rdlcDoc);
            //    Microsoft.Reporting.WebForms.ReportDataSource rds = null;

            //        ReportParameter[] parameters = new ReportParameter[26];

            //        parameters[0] = new ReportParameter("Prefix", taskControl.PolicyType.ToString().Trim());
            //        parameters[1] = new ReportParameter("Term", taskControl.Term.ToString().Trim());
            //        parameters[2] = new ReportParameter("PolicyNo", taskControl.PolicyNo.ToString().Trim());
            //        parameters[3] = new ReportParameter("EffDate", taskControl.EffectiveDate.ToString().Trim());
            //        parameters[4] = new ReportParameter("ExpDate", taskControl.ExpirationDate.ToString().Trim());
            //        parameters[5] = new ReportParameter("VehicleMake", taskControl.XtraVehicleMake.ToString().Trim());
            //        parameters[6] = new ReportParameter("VehicleModel", taskControl.XtraVehicleModel.ToString().Trim());
            //        parameters[7] = new ReportParameter("VehicleYear", taskControl.XtraVehicleYear.ToString().Trim());
            //        parameters[8] = new ReportParameter("VehicleVIN", taskControl.XtraVIN.ToString().Trim());
            //        parameters[9] = new ReportParameter("VehiclePlate", taskControl.XtraPlate.ToString().Trim());
            //        parameters[10] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
            //        parameters[11] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.ToString().Trim());
            //        parameters[12] = new ReportParameter("CustomerInitial", taskControl.Customer.Initial.ToString().Trim());
            //        parameters[13] = new ReportParameter("CustomerLastName1", taskControl.Customer.LastName1.ToString().Trim());
            //        parameters[14] = new ReportParameter("CustomerLastName2", taskControl.Customer.LastName2.ToString().Trim());
            //        parameters[15] = new ReportParameter("CustomerAddrs1", taskControl.Customer.Address1.ToString().Trim());
            //        parameters[16] = new ReportParameter("CustomerAddrs2", taskControl.Customer.Address2.ToString().Trim());
            //        parameters[17] = new ReportParameter("CustomerCity", taskControl.Customer.City.ToString().Trim());
            //        parameters[18] = new ReportParameter("CustomerState", taskControl.Customer.State.ToString().Trim());
            //        parameters[19] = new ReportParameter("CustomerZip", taskControl.Customer.ZipCode.ToString().Trim());
            //        parameters[20] = new ReportParameter("Agency", taskControl.Agency.ToString().Trim());
            //        parameters[21] = new ReportParameter("Agent", taskControl.AgentDesc.ToString().Trim());
            //        parameters[22] = new ReportParameter("AgentNo", taskControl.AgentCode.ToString().Trim());
            //        parameters[23] = new ReportParameter("Premium", taskControl.TotalPremium.ToString().Trim());
            //        parameters[24] = new ReportParameter("Deducible", taskControl.XtraPremium.ToString().Trim());
            //        parameters[25] = new ReportParameter("PhysicalAddrs", taskControl.Customer.AddressPhysical1.ToString().Trim() + ", " + taskControl.Customer.AddressPhysical2.ToString().Trim() + " " + taskControl.Customer.CityPhysical.ToString().Trim() + ", " + taskControl.Customer.StatePhysical.ToString().Trim() + " " + taskControl.Customer.ZipPhysical.ToString().Trim());

            //        // viewer.LocalReport.ReportPath = Server.MapPath("Reports/GuardianXtra/SolicitudGuardianXtra.rdlc");
            //        viewer.LocalReport.SetParameters(parameters);
            //        viewer.LocalReport.DataSources.Add(rds);
            //        viewer.LocalReport.Refresh();

            //    }

            //    if (rdlcDoc == "Solicitud_de_Plan_Diferido_de_Pago_de_Primas_6_Plazos_3.rdlc")
            //    {

            //        GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
            //        GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
            //        ds.Fill(dt, taskControl.TaskControlID);
            //        rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

            //        string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

            //        ReportParameter[] param = new ReportParameter[3];

            //        param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
            //        param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
            //        param[2] = new ReportParameter("VehiclePlate", taskControl.XtraPlate.ToString().Trim());


            //        viewer.LocalReport.SetParameters(param);
            //        viewer.LocalReport.DataSources.Add(rds);
            //        viewer.LocalReport.Refresh();

            //    }

            //    if (rdlcDoc == "PlandePagoDiferidodePrimas6Plazos.rdlc")
            //    {


            //        GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
            //        GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
            //        ds.Fill(dt, taskControl.TaskControlID);
            //        rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

            //        string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

            //        string Fecha = DateTime.Parse(taskControl.EffectiveDate.Trim()).AddMonths(2).AddDays(-(DateTime.Parse(taskControl.EffectiveDate.Trim()).Day)).AddDays(1).ToShortDateString();

            //        ReportParameter[] param = new ReportParameter[8];

            //        param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
            //        param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
            //        param[2] = new ReportParameter("Sufix", taskControl.Suffix.Trim());
            //        param[3] = new ReportParameter("Date1", Fecha);
            //        param[4] = new ReportParameter("Date2", DateTime.Parse(Fecha).AddMonths(1).ToShortDateString());
            //        param[5] = new ReportParameter("Date3", DateTime.Parse(Fecha).AddMonths(2).ToShortDateString());
            //        param[6] = new ReportParameter("Date4", DateTime.Parse(Fecha).AddMonths(3).ToShortDateString());
            //        param[7] = new ReportParameter("Date5", DateTime.Parse(Fecha).AddMonths(4).ToShortDateString());

            //        viewer.LocalReport.SetParameters(param);
            //        viewer.LocalReport.DataSources.Add(rds);
            //        viewer.LocalReport.Refresh();

            //    }

            //    if (rdlcDoc == "SolicituddePlanDiferidodePagodePrimas4Plazos3.rdlc")
            //    {


            //        GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
            //        GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
            //        ds.Fill(dt, taskControl.TaskControlID);
            //        rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

            //        string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));


            //        ReportParameter[] param = new ReportParameter[3];

            //        param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
            //        param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
            //        param[2] = new ReportParameter("VehiclePlate", taskControl.XtraPlate.ToString().Trim());


            //        viewer.LocalReport.SetParameters(param);
            //        viewer.LocalReport.DataSources.Add(rds);
            //        viewer.LocalReport.Refresh();

            //    }

            //    if (rdlcDoc == "PlandePagoDiferidodePrimas4Plazos.rdlc")
            //    {


            //        GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
            //        GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
            //        ds.Fill(dt, taskControl.TaskControlID);
            //        rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

            //        string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

            //        string Fecha = DateTime.Parse(taskControl.EffectiveDate.Trim()).AddMonths(2).AddDays(-(DateTime.Parse(taskControl.EffectiveDate.Trim()).Day)).AddDays(1).ToShortDateString();

            //        ReportParameter[] param = new ReportParameter[6];

            //        param[0] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
            //        param[1] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.Trim() + " " + taskControl.Customer.Initial.Trim() + " " + taskControl.Customer.LastName1.Trim() + " " + taskControl.Customer.LastName2.Trim());
            //        param[2] = new ReportParameter("Sufix", taskControl.Suffix.Trim());
            //        param[3] = new ReportParameter("Date1", Fecha);
            //        param[4] = new ReportParameter("Date2", DateTime.Parse(Fecha).AddMonths(1).ToShortDateString());
            //        param[5] = new ReportParameter("Date3", DateTime.Parse(Fecha).AddMonths(2).ToShortDateString());

            //        viewer.LocalReport.SetParameters(param);
            //        viewer.LocalReport.DataSources.Add(rds);
            //        viewer.LocalReport.Refresh();

            //    }

            //    if (rdlcDoc == "HojadeDeclaraciones_XTRA.rdlc")
            //    {

            //        GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter ds = new GetGuardianXtraDeclarationReportTableAdapters.GetGuardianXtraDeclarationReportTableAdapter();
            //        GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable dt = new GetGuardianXtraDeclarationReport.GetGuardianXtraDeclarationReportDataTable();
            //        ds.Fill(dt, taskControl.TaskControlID);
            //        rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetDeclarationReport", (DataTable)dt);

            //        string Month = GetDateInSpanish(DateTime.Now.ToString("MMMM"));

            //        ReportParameter[] parameters = new ReportParameter[25];

            //        parameters[0] = new ReportParameter("Prefix", taskControl.PolicyType.ToString().Trim());
            //        parameters[1] = new ReportParameter("Term", taskControl.Term.ToString().Trim());
            //        parameters[2] = new ReportParameter("PolicyNo", taskControl.PolicyNo.ToString().Trim());
            //        parameters[3] = new ReportParameter("EffDate", taskControl.EffectiveDate.ToString().Trim());
            //        parameters[4] = new ReportParameter("ExpDate", taskControl.ExpirationDate.ToString().Trim());
            //        parameters[5] = new ReportParameter("VehicleMake", taskControl.XtraVehicleMake.ToString().Trim());
            //        parameters[6] = new ReportParameter("VehicleModel", taskControl.XtraVehicleModel.ToString().Trim());
            //        parameters[7] = new ReportParameter("VehicleYear", taskControl.XtraVehicleYear.ToString().Trim());
            //        parameters[8] = new ReportParameter("VehicleVIN", taskControl.XtraVIN.ToString().Trim());
            //        parameters[9] = new ReportParameter("VehiclePlate", taskControl.XtraPlate.ToString().Trim());
            //        parameters[10] = new ReportParameter("ReportDate", DateTime.Now.Day.ToString() + " de " + Month + " de " + DateTime.Now.Year.ToString());
            //        parameters[11] = new ReportParameter("CustomerName", taskControl.Customer.FirstName.ToString().Trim());
            //        parameters[12] = new ReportParameter("CustomerInitial", taskControl.Customer.Initial.ToString().Trim());
            //        parameters[13] = new ReportParameter("CustomerLastName1", taskControl.Customer.LastName1.ToString().Trim());
            //        parameters[14] = new ReportParameter("CustomerLastName2", taskControl.Customer.LastName2.ToString().Trim());
            //        parameters[15] = new ReportParameter("CustomerAddrs1", taskControl.Customer.Address1.ToString().Trim());
            //        parameters[16] = new ReportParameter("CustomerAddrs2", taskControl.Customer.Address2.ToString().Trim());
            //        parameters[17] = new ReportParameter("CustomerCity", taskControl.Customer.City.ToString().Trim());
            //        parameters[18] = new ReportParameter("CustomerState", taskControl.Customer.State.ToString().Trim());
            //        parameters[19] = new ReportParameter("CustomerZip", taskControl.Customer.ZipCode.ToString().Trim());
            //        parameters[20] = new ReportParameter("Agency", taskControl.Agency.ToString().Trim());
            //        parameters[21] = new ReportParameter("Agent", taskControl.AgentDesc.ToString().Trim());
            //        parameters[22] = new ReportParameter("AgentNo", taskControl.AgentCode.ToString().Trim());
            //        parameters[23] = new ReportParameter("Premium", taskControl.TotalPremium.ToString().Trim());
            //        parameters[24] = new ReportParameter("Deducible", taskControl.XtraPremium.ToString().Trim());

        }
        private DataTable GetTransactionAmount(int _taskcontrolID)
        {
            TaskControl.GuardianXtra taskControl = (TaskControl.GuardianXtra)Session["TaskControl"];
            DataTable dt = new DataTable();

            DBRequest Executor = new DBRequest();

            try
            {
                DbRequestXmlCookRequestItem[] cookItems =
                    new DbRequestXmlCookRequestItem[1];

                DbRequestXmlCooker.AttachCookItem("TaskControlID",
                                SqlDbType.Int, 0, _taskcontrolID.ToString(),
                                ref cookItems);


                XmlDocument xmlDoc;

                try
                {
                    xmlDoc = DbRequestXmlCooker.Cook(cookItems);
                }
                catch (Exception ex)
                {
                    throw new Exception("Could not cook items.", ex);
                }

                Executor.GetQuery("GetTotalPaid", xmlDoc);

            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            return dt;

        }
        private List<string> ImprimirCertification(List<string> mergePaths, int taskControl, string RDLC, string IsPreview)
        {
            try
            {

                ReportDataSource rpd = new ReportDataSource();
                rpd = null;
                ReportParameter[] param = new ReportParameter[1];
                param[0] = new ReportParameter("IsPreview", IsPreview);

                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/RES/" + RDLC);
                viewer1.LocalReport.DataSources.Add(rpd);
                viewer1.LocalReport.SetParameters(param);
                viewer1.LocalReport.Refresh();

                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;

                string RandomString = Guid.NewGuid().ToString();

                string fileName1 = "PolicyNo-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                string _FileName1 = "PolicyNo-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";

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
            catch (Exception exp)
            {
                throw new Exception(exp.Message.ToString());
            }
        }
        private List<string> ImprimirAmount(List<string> mergePaths, int taskControl, string RDLC, decimal TotalPremium, string IsPreview, string isAAA)
        {
            try
            {
                //GetBondInfoPolicyTableAdapters.GetBondInfoPolicyTableAdapter ta = new GetBondInfoPolicyTableAdapters.GetBondInfoPolicyTableAdapter();
                ReportDataSource rpd = new ReportDataSource();
                //rpd = new ReportDataSource("GetBondInfoPolicy", (DataTable)ta.GetData(taskControl));

                string Amount = NumberToCurrencyText(TotalPremium);
                string Cantidad = NumberToCurrencyTextESP(double.Parse(TotalPremium.ToString()));
                Cantidad = "**" + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Cantidad.ToLower()) + " y Cero Centavos" + "**";

                Cantidad = Cantidad.Replace("Dieciuno Mil", "Dieciun Mil");
                Cantidad = Cantidad.Replace("Veintiuno Mil", "Veintiun Mil");
                Cantidad = Cantidad.Replace("Treinta Y Uno", "Treintaiun Mil");
                Cantidad = Cantidad.Replace("Cuarenta Y Uno", "Cuarentaiun Mil");
                Cantidad = Cantidad.Replace("Cincuenta Y Uno", "Cincuentaiun Mil");
                Cantidad = Cantidad.Replace("Sesenta Y Uno", "Sesentaiun Mil");
                Cantidad = Cantidad.Replace("Setenta Y Uno", "Setentaiun Mil");
                Cantidad = Cantidad.Replace("Ochenta Y Uno", "Ochentaiun Mil");
                Cantidad = Cantidad.Replace("Ciento Uno Mil", "Cientoun Mil");


                int ParametersCount = 3;
                string Amt1 = "";
                string Amt2 = "";
                string AmtTotal = "";
                string AmtTotal2 = "";

                if (RDLC == "FianzaELA.rdlc") // ddlObligee.SelectedItem.Text == "ASC" || 
                {

                    ParametersCount = 7;
                    Amt1 = "**" + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Amt1.ToLower()) + " y Cero Centavos" + "**";

                    AmtTotal2 = AmtTotal;

                    AmtTotal = "**" + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(AmtTotal.ToLower()) + " y Cero Centavos" + "**";
                    Amt2 = NumberToCurrencyTextESP(double.Parse(AmtTotal2));
                    Amt2 = "**" + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Amt2.ToLower()) + " y Cero Centavos" + "**";
                }

                if (RDLC == "CertificadoRenovacion.rdlc" && isAAA == "AAA")
                {
                    ParametersCount = ParametersCount + 2;
                }
                else if ((RDLC == "CertificadoRenovacionENG.rdlc" || RDLC == "CertificadoRenovacion.rdlc") && isAAA == "")
                {
                    ParametersCount = ParametersCount + 2;
                }
                else if (RDLC == "CertificadoRenovacionENG.rdlc" && (isAAA == "NotaryPublic" || isAAA == "NotaryPublic2"))
                {
                    ParametersCount = ParametersCount + 2;
                }
                else if (RDLC == "MortageLendersBond.rdlc" || RDLC == "MortgageLoanOriginatorsBond.rdlc" || RDLC == "MortageLendersBondCompany.rdlc")
                {
                    ParametersCount = ParametersCount + 2;
                }
                else if (RDLC == "NOTARY-PUBLIC-BOND.rdlc" || RDLC == "POWER-OF-ATTORNEY.rdlc"
                         || RDLC == "SurplusLinesBrokerBond.rdlc" || RDLC == "Non-ResidentBrokersBond.rdlc" || RDLC == "RESIDENT-LINE-BROKERS-BOND.rdlc" || RDLC == "ProcessServerBond.rdlc"
                         || RDLC == "PrivateInvestigativeAgencySurety.rdlc")
                {
                    ParametersCount = ParametersCount + 1;
                }

                ReportParameter[] param = new ReportParameter[ParametersCount];

                param[0] = new ReportParameter("Amount", Amount);
                param[1] = new ReportParameter("Cantidad", Cantidad);
                param[2] = new ReportParameter("IsPreview", IsPreview);

                if (RDLC == "FianzaELA.rdlc") // ddlObligee.SelectedItem.Text == "ASC" || 
                {
                    param[3] = new ReportParameter("Amt1", Amt1);
                    param[4] = new ReportParameter("AmtTotal", AmtTotal);
                    param[5] = new ReportParameter("AmtTotal2", AmtTotal2);
                    param[6] = new ReportParameter("Amt2", Amt2);
                }
                else
                {
                    if (RDLC == "CertificadoRenovacion.rdlc" && isAAA == "AAA")
                    {
                        param[3] = new ReportParameter("IsAAAorAEE", "1");

                        if (TxtPolicyNo.Text.Trim().Contains("-"))
                        {
                            param[4] = new ReportParameter("BondsNo", TxtPolicyNo.Text.Trim().Substring(0, TxtPolicyNo.Text.Trim().IndexOf("-")).Replace("RES", ""));
                        }
                        else
                        {
                            param[4] = new ReportParameter("BondsNo", TxtPolicyNo.Text.Trim().Replace("RES", ""));
                        }
                    }
                    else if (RDLC == "CertificadoRenovacionENG.rdlc" && (isAAA == "NotaryPublic" || isAAA == "NotaryPublic2"))
                    {
                        param[3] = new ReportParameter("IsAAAorAEE", isAAA);

                        if (TxtPolicyNo.Text.Trim().Contains("-"))
                        {
                            param[4] = new ReportParameter("BondsNo", TxtPolicyNo.Text.Trim().Substring(0, TxtPolicyNo.Text.Trim().IndexOf("-")).Replace("bnd", ""));
                        }
                        else
                        {
                            param[4] = new ReportParameter("BondsNo", TxtPolicyNo.Text.Trim().Replace("bnd", ""));
                        }

                    }
                    else if (RDLC == "CertificadoRenovacionENG.rdlc" && isAAA == "")
                    {
                        if (TxtPolicyNo.Text.Trim().Contains("-"))
                        {
                            param[3] = new ReportParameter("IsAAAorAEE", "");
                            param[4] = new ReportParameter("BondsNo", TxtPolicyNo.Text.Trim().Substring(0, TxtPolicyNo.Text.Trim().IndexOf("-")).Replace("bnd", ""));
                        }
                        else
                        {
                            param[3] = new ReportParameter("IsAAAorAEE", "");
                            param[4] = new ReportParameter("BondsNo", TxtPolicyNo.Text.Trim().Replace("bnd", ""));
                        }
                    }
                    else if (RDLC == "CertificadoRenovacion.rdlc" && isAAA == "")
                    {
                        param[3] = new ReportParameter("IsAAAorAEE", "");

                        if (TxtPolicyNo.Text.Trim().Contains("-"))
                        {
                            param[4] = new ReportParameter("BondsNo", TxtPolicyNo.Text.Trim().Substring(0, TxtPolicyNo.Text.Trim().IndexOf("-")).Replace("bnd", ""));
                        }
                        else
                        {
                            param[4] = new ReportParameter("BondsNo", TxtPolicyNo.Text.Trim().Replace("bnd", ""));
                        }
                    }

                    else if (RDLC == "MortageLendersBond.rdlc")
                    {

                        //if (ddlPaymentAmount.SelectedItem.Text == "$25,000.00")
                        //{
                        //    param[4] = new ReportParameter("PaymentAmount", "$25,000.00");
                        //    param[5] = new ReportParameter("PaymentAmountWords", "Twenty-Five Thousand Dollars");
                        //}
                        //if (ddlPaymentAmount.SelectedItem.Text == "$50,000.00")
                        //{
                        //    param[4] = new ReportParameter("PaymentAmount", "$50,000.00");
                        //    param[5] = new ReportParameter("PaymentAmountWords", "Fifty Thousand Dollars");
                        //}

                    }

                    else if (RDLC == "MortgageLoanOriginatorsBond.rdlc")
                    {
                        //if (ddlPaymentAmount.SelectedItem.Text == "$25,000.00")
                        //{
                        //    param[4] = new ReportParameter("PaymentAmount", "$25,000.00");
                        //    param[5] = new ReportParameter("PaymentAmountWords", "Twenty-Five Thousand Dollars");
                        //}
                        //if (ddlPaymentAmount.SelectedItem.Text == "$50,000.00")
                        //{
                        //    param[4] = new ReportParameter("PaymentAmount", "$50,000.00");
                        //    param[5] = new ReportParameter("PaymentAmountWords", "Fifty Thousand Dollars");
                        //}

                    }

                    else if (RDLC == "NOTARY-PUBLIC-BOND.rdlc")
                    {

                    }

                    else if (RDLC == "POWER-OF-ATTORNEY.rdlc")
                    {

                    }

                    else if (RDLC == "SurplusLinesBrokerBond.rdlc")
                    {

                    }

                    else if (RDLC == "Non-ResidentBrokersBond.rdlc")
                    {

                    }

                    else if (RDLC == "RESIDENT-LINE-BROKERS-BOND.rdlc")
                    {

                    }

                    else if (RDLC == "ProcessServerBond.rdlc")
                    {

                    }

                    else if (RDLC == "PrivateInvestigativeAgencySurety.rdlc")
                    {

                    }


                }



                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/Bonds/" + RDLC);
                viewer1.LocalReport.SetParameters(param);
                viewer1.LocalReport.DataSources.Add(rpd);
                viewer1.LocalReport.Refresh();

                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;

                string RandomString = Guid.NewGuid().ToString();

                string fileName1 = "PolicyNo-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                string _FileName1 = "PolicyNo-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";

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
            catch (Exception exp)
            {
                throw new Exception(exp.Message.ToString());
            }
        }
        private string NumberToCurrencyTextESP(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + NumberToCurrencyTextESP(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + NumberToCurrencyTextESP(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = NumberToCurrencyTextESP(Math.Truncate(value / 10) * 10) + " Y " + NumberToCurrencyTextESP(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + NumberToCurrencyTextESP(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = NumberToCurrencyTextESP(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = NumberToCurrencyTextESP(Math.Truncate(value / 100) * 100) + " " + NumberToCurrencyTextESP(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + NumberToCurrencyTextESP(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = NumberToCurrencyTextESP(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + NumberToCurrencyTextESP(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + NumberToCurrencyTextESP(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = NumberToCurrencyTextESP(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + NumberToCurrencyTextESP(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + NumberToCurrencyTextESP(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = NumberToCurrencyTextESP(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + NumberToCurrencyTextESP(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }
        private List<string> ImprimirRoadAssist(List<string> mergePaths, int taskControl)
        {
            try
            {
                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];

                int s = taskControl;

                EPolicy.TaskControl.Autos taskControl1 = (EPolicy.TaskControl.Autos)Session["TaskControl"];

                GetPaymentByTaskControlID_VITableAdapters.GetPaymentByTaskControlID_VITableAdapter ds1 = new GetPaymentByTaskControlID_VITableAdapters.GetPaymentByTaskControlID_VITableAdapter();

                ReportDataSource rds1 = new ReportDataSource();

                rds1 = new ReportDataSource("GetPaymentByTaskControlID_VI", (DataTable)ds1.GetData(s));

                //Nuevo

                string ImgPath = "";
                Uri pathAsUri = null;
                DataTable dt = null;

                ReportParameter[] parameters = new ReportParameter[1];

                parameters[0] = new ReportParameter("ImgPath", pathAsUri.AbsoluteUri);


                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/VI/AgentInvoice_VI.rdlc");
                viewer1.LocalReport.EnableExternalImages = true;
                viewer1.LocalReport.DataSources.Add(rds1);
                viewer1.LocalReport.SetParameters(parameters);
                viewer1.LocalReport.Refresh();

                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;


                string fileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-RoadAssist";
                string _FileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + taskControl.ToString().Trim() + "-RoadAssist" + ".pdf";

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
        private List<string> WriteRdlcToPDF(ReportViewer viewer, EPolicy.TaskControl.RES taskControl, List<string> mergePaths, int index)
        {
            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string filetype = string.Empty;


            string fileName1 = "FileNo-" + index.ToString();
            string _FileName1 = "FileNo-" + index.ToString() + ".pdf";

            if (File.Exists(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1))
                File.Delete(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);

            byte[] bytes1 = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

            using (FileStream fs1 = new FileStream(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1, FileMode.Create))
            {
                fs1.Write(bytes1, 0, bytes1.Length);
            }

            try
            {
                mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName1);
            }
            catch (Exception ecp)
            {
                // ShowMessage(ecp.Message);

            }
            return mergePaths;
        }
        private DataTable GetRESInfoPolicy(int TaskControlID)
        {
            DbRequestXmlCookRequestItem[] cookItems =
                new DbRequestXmlCookRequestItem[1];

            DbRequestXmlCooker.AttachCookItem("TaskControlID",
                SqlDbType.Int, 0, TaskControlID.ToString(),
                ref cookItems);


            DBRequest exec = new DBRequest();
            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }
            System.Data.DataTable dt = null;
            try
            {
                dt = exec.GetQuery("GetRESInfoPolicy", xmlDoc);

                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve data from database.", ex);
            }

        }
        private void FillReqDocsGrid()
        {
            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];


        }
        private void DeleteFile(string pathAndFileName)
        {
            if (File.Exists(pathAndFileName))
                File.Delete(pathAndFileName);
        }
        private void VerifyAssignPolicyFields()
        {
            if (this.ChkAutoAssignPolicy.Checked)
            {
                TxtPolicyType.Enabled = false;
                TxtPolicyNo.Enabled = false;
                TxtSufijo.Enabled = false;
            }
            else
            {
                TxtPolicyType.Enabled = true;
                TxtPolicyNo.Enabled = true;
                TxtSufijo.Enabled = true;
            }
        }
        private void ValidateFields()
        {
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = 0;
            userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);


            ArrayList errorMessages = new ArrayList();

            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];

            try
            {
                if (this.ddlType.SelectedIndex > 0)
                {
                    if (this.ddlType.SelectedItem.Text.ToString() == "Individual")
                    {
                        if (this.TxtFirstName.Text == "")
                        {
                            errorMessages.Add("First Name is missing." + "\r\n");
                        }
                        if (this.txtLastname1.Text == "")
                        {
                            errorMessages.Add("Last Name is missing." + "\r\n");
                        }
                    }
                }

                if (lblCompanyName.ForeColor == System.Drawing.Color.Red && txtCompanyName.Text == "")
                {
                    throw new Exception("You must enter a company name for this type of RES.");
                }

                if (this.TxtTerm.Text == "")
                {
                    errorMessages.Add("Term is missing." + "\r\n");
                }
                else if (this.TxtTerm.Text == "0")
                {
                    errorMessages.Add("Term must be greater than zero." + "\r\n");
                }

                if ((ddlType.SelectedItem.Text.ToString() == "Corporate" || ddlType.SelectedItem.Text.ToString() == "DBA") && txtCompanyName.Text.Trim() == "")
                {
                    errorMessages.Add("If Corporate or DBA is selected, a company name or DBA is required." + "\r\n");
                }

                if (ddlCiudad.Text == "")
                {
                    errorMessages.Add("The City is missing or wrong." + "\r\n");
                }

                if (ddlZip.Text.Trim() == "")
                {
                    errorMessages.Add("The Zipcode is missing or wrong." + "\r\n");
                }

                if (ddlPhyCity.Text.Trim() == "")
                {
                    errorMessages.Add("The Physical City is missing or wrong." + "\r\n");
                }

                if (ddlPhyZipCode.Text.Trim() == "")
                {
                    errorMessages.Add("The Physical Zipcode is missing or wrong." + "\r\n");
                }

                if (this.txtTotalPremium.Text == "")
                {
                    errorMessages.Add("TotalPremium is missing or the vehicle is missing." + "\r\n");
                }

                if (this.TxtState.Text == "")
                {
                    errorMessages.Add("State is missing." + "\r\n");
                }

                if (this.TxtAddrs1.Text == "")
                {
                    errorMessages.Add("Address is missing." + "\r\n");
                }

                if (this.TxtState.Text == "")
                {
                    errorMessages.Add("State is missing." + "\r\n");
                }

                if (this.txtPhyAddress.Text == "")
                {
                    errorMessages.Add("Physical Address is missing." + "\r\n");
                }

                if (this.txtInsuredPremise.Text == "")
                {
                    errorMessages.Add("Insured Premise is missing." + "\r\n");
                }

                if (this.txtPartOcupied.Text == "")
                {
                    errorMessages.Add("Part Ocupied is missing." + "\r\n");
                }

                if (this.dllBILimit.SelectedIndex < 0)
                {
                    errorMessages.Add("Select Body Injury Liability." + "\r\n");
                }

                if (taskControl.Mode == 1 || taskControl.Mode == 2 && DateTime.Parse(txtEffDt.Text.ToString()) < DateTime.Parse(taskControl.EffectiveDate))
                {
                    if (this.txtEffDt.Text == "")
                    {
                        errorMessages.Add("Effective date is missing." + "\r\n");
                    }

                    if (!cp.IsInRole("RES_ALLOWBACKDATE") && !cp.IsInRole("ADMINISTRATOR"))
                    {
                        if (taskControl.Mode == 1)
                        {
                            if (DateTime.Parse(txtEffDt.Text.ToString()) < DateTime.Parse(DateTime.Now.ToShortDateString()))
                            {
                                errorMessages.Add("Effective Date must be equal or greater than today." + "\r\n");
                            }
                        }

                        if (taskControl.Mode == 2)
                        {
                            if (DateTime.Parse(txtEffDt.Text.ToString()) < DateTime.Parse(DateTime.Now.ToShortDateString()))
                            {
                                errorMessages.Add("Effective Date must be equal or greater than the original date." + "\r\n");
                            }
                        }

                        if (DateTime.Parse(txtExpDt.Text.ToString()) < DateTime.Parse(DateTime.Now.AddMonths(int.Parse(TxtTerm.Text.ToString().Trim())).ToShortDateString()))
                        {
                            errorMessages.Add("Expiration Date is wrong please select Effective Date again." + "\r\n");
                        }
                    }
                }

                if (errorMessages.Count > 0)
                {
                    string popUpString = "";

                    foreach (string message in errorMessages)
                    {
                        popUpString += message + " ";
                    }

                    throw new Exception(popUpString);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                lblRecHeader.Text = ex.Message; //"Transaction saved successfully.";// + taskControl.Mode.ToString() + CUSTOMER2.ToString();
                mpeSeleccion.Show();
            }
        }
        private void FillProperties()
        {
            try
            {
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];

                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                //Agent
                if (ddlAgent.SelectedIndex > 0 && ddlAgent.SelectedItem != null)
                {
                    taskControl.Agent = ddlAgent.SelectedItem.Value;
                    taskControl.AgentDesc = ddlAgent.SelectedItem.Text.Trim();
                }
                else
                {
                    taskControl.Agent = "000";
                    taskControl.AgentDesc = "";
                }

                //Agency
                if (ddlAgency.SelectedIndex > 0 && ddlAgency.SelectedItem != null)
                    taskControl.Agency = ddlAgency.SelectedItem.Value;
                else
                    taskControl.Agency = "000";

                //InsuranceCompany
                if (ddlInsuranceCompany.SelectedIndex > 0 && ddlInsuranceCompany.SelectedItem != null)
                    taskControl.InsuranceCompany = ddlInsuranceCompany.SelectedItem.Value;
                else
                    taskControl.InsuranceCompany = "000";

                //Bank
                if (ddlBank.SelectedIndex > 0 && ddlBank.SelectedItem != null)
                    taskControl.Bank = ddlBank.SelectedItem.Value;
                else
                    taskControl.Bank = "000";

                //CompanyDealer
                if (ddlCompanyDealer.SelectedIndex > 0 && ddlCompanyDealer.SelectedItem != null)
                    taskControl.CompanyDealer = ddlCompanyDealer.SelectedItem.Value;
                else
                    taskControl.CompanyDealer = "000";

                if (taskControl.IsMaster)
                {
                    EPolicy.LookupTables.CompanyDealer dealer = new EPolicy.LookupTables.CompanyDealer();
                    dealer = dealer.GetCompanyDealer(taskControl.CompanyDealer);

                    taskControl.MasterPolicyID = dealer.MasterPolicyID;
                }

                //Originated At
                if (ddlOriginatedAt.SelectedIndex > 0 && ddlOriginatedAt.SelectedItem != null)
                {
                    taskControl.OriginatedAt = int.Parse(ddlOriginatedAt.SelectedItem.Value.ToString());
                }

                if (ddlCiudad.Text.Trim() != "")
                    taskControl.Customer.City = ddlCiudad.Text.ToString().Trim();
                else
                    taskControl.Customer.City = "";

                if (ddlPhyCity.Text.Trim() != "")
                    taskControl.Customer.CityPhysical = ddlPhyCity.Text.ToString().Trim();
                else
                    taskControl.Customer.CityPhysical = "";

                if (ddlZip.Text != "")
                    taskControl.Customer.ZipCode = ddlZip.Text.Trim();
                else
                    taskControl.Customer.ZipCode = "";

                taskControl.Customer.ZipPhysical = ddlPhyZipCode.Text.Trim();

                taskControl.TaskControlID = int.Parse(LblControlNo.Text.Trim());

                //Customer Information
                taskControl.Customer.FirstName = TxtFirstName.Text.Trim();
                taskControl.Customer.Initial = TxtInitial.Text.Trim();
                taskControl.Customer.LastName1 = txtLastname1.Text.Trim();
                //taskControl.Customer.LastName2 = txtLastname2.Text.Trim();
                taskControl.Customer.Address1 = TxtAddrs1.Text.Trim();
                taskControl.Customer.Address2 = TxtAddrs2.Text.Trim();
                //taskControl.Customer.Birthday = TxtBirthdate.Text.Trim();
                taskControl.Customer.Occupation = TxtOccupa.Text.Trim();
                taskControl.Customer.Licence = TxtLicense.Text.Trim();
                taskControl.Customer.Email = txtEmail.Text.Trim();
                if (ddlType.SelectedIndex == 1)
                {
                    taskControl.Customer.LastName2 = txtCompanyName.Text.Trim();
                }

                taskControl.Customer.State = TxtState.Text.Trim();

                taskControl.Customer.HomePhone = TxtHomePhone.Text.Trim();
                taskControl.Customer.JobPhone = txtWorkPhone.Text.Trim();
                taskControl.Customer.Cellular = TxtCellular.Text.Trim();

                taskControl.Customer.AddressPhysical1 = txtPhyAddress.Text.ToString().Trim();
                taskControl.Customer.AddressPhysical2 = txtPhyAddress2.Text.ToString().Trim();
                taskControl.Customer.StatePhysical = txtPhyState.Text.ToString().Trim();


                taskControl.Prospect.FirstName = TxtFirstName.Text.ToString().Trim();
                taskControl.Prospect.LastName1 = txtLastname1.Text.ToString().Trim();
                //taskControl.Prospect.LastName2 = txtLastname2.Text.ToString().Trim();
                taskControl.Prospect.HomePhone = TxtHomePhone.Text.ToString().Trim();
                taskControl.Prospect.WorkPhone = txtWorkPhone.Text.ToString().Trim();
                taskControl.Prospect.Cellular = TxtCellular.Text.ToString().Trim();
                taskControl.Prospect.Email = txtEmail.Text.ToString().Trim();
                taskControl.Customer.Cellular = TxtCellular.Text.ToString().Trim();
                taskControl.Customer.Email = txtEmail.Text.ToString().Trim();


                if (taskControl.PolicyClassID == 1 || taskControl.PolicyClassID == 16) // VSC, QCertified
                {
                    taskControl.PolicyNo = TxtPolicyNo.Text.Trim().Replace(" ", "");
                    taskControl.PolicyNo = taskControl.PolicyNo.Trim().Replace("-", "");
                }
                else
                {
                    taskControl.PolicyNo = TxtPolicyNo.Text.Trim().Replace(" ", "");
                }
                taskControl.PolicyType = TxtPolicyType.Text.Trim();
                taskControl.Suffix = TxtSufijo.Text.Trim();
                taskControl.Term = int.Parse(TxtTerm.Text.Trim());
                taskControl.EffectiveDate = txtEffDt.Text.Trim();

                if (taskControl.ExpirationDate.Trim() == string.Empty) // && this.TxtTerm.Text.Trim() != string.Empty)
                {
                    if (this.TxtTerm.Text.Trim() == string.Empty)
                    {
                        this.TxtTerm.Text = "0";
                    }
                    taskControl.ExpirationDate = DateTime.Parse(this.txtEffDt.Text).AddMonths(int.Parse(this.TxtTerm.Text.Trim())).ToShortDateString();
                    this.txtExpDt.Text = taskControl.ExpirationDate;
                }
                else
                {
                    if (this.txtExpDt.Text.Trim() != string.Empty)
                        taskControl.ExpirationDate = this.txtExpDt.Text.Trim();
                }

                taskControl.EntryDate = DateTime.Parse(txtEntryDate.Text);
                Login.Login cp2 = HttpContext.Current.User as Login.Login;

                if (txtTotalPremiumLimit.Text.Trim() != "")
                    taskControl.TotalPremium = double.Parse(txtTotalPremiumLimit.Text.Trim().Replace("$", ""));

                taskControl.Customer.SamesAsMail = chkSameMailing.Checked;
                taskControl.AutoAssignPolicy = true;

                if (taskControl.Mode == 1)
                {
                    //EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                    taskControl.EnteredBy = cp.Identity.Name.Split("|".ToCharArray())[0];
                }


                //taskControl.Obligee = txtObligee.Text.Trim();
                taskControl.Customer.Dependents = int.Parse(ddlType.SelectedValue.ToString());
                taskControl.Customer.LocationID = 1;// PUERTO RICO ONLY

                EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();

                if (txtSocSec.Text.Trim().Replace("*", "").Replace("-", "").Replace("_", "") != "")
                {
                    string encryptString = txtSocSec.Text.Trim().ToUpper();
                    taskControl.Customer.SocialSecurity = encrypt.Encrypt(encryptString.ToUpper());
                }
                else
                    taskControl.Customer.SocialSecurity = "";

                taskControl.Customer.LocationID = EPolicy.Login.Login.GetLocationByUserID(userID);


                if (TxtFirstName.Text.Trim() == "")
                {
                    taskControl.Customer.FirstName = txtCompanyName.Text.Trim();

                }
                if (txtLastname1.Text.Trim() == "")
                {
                    taskControl.Customer.LastName1 = ".";
                    //YDJ nose
                }
                SetRESQuoteParameters(taskControl);
                Session["TaskControl"] = taskControl;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                lblRecHeader.Text = ex.Message; //"Transaction saved successfully.";// + taskControl.Mode.ToString() + CUSTOMER2.ToString();
                mpeSeleccion.Show();
            }
        }
        private void EnableControls()
        {
            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];


            //Botones  
            btnEdit.Visible = false;
            btnPreview.Visible = false;
            BtnExit.Visible = false;
            BtnSave.Visible = true;
            btnCancel.Visible = true;
            btnCancellation.Visible = false;

            // TextBox
            TxtProspectNo.Enabled = false;
            TxtFirstName.Enabled = true;
            txtLastname1.Enabled = true;
            chkSameMailing.Visible = true;
            chkSameMailing.Enabled = true;

            TxtInitial.Enabled = true;
            TxtAddrs1.Enabled = true;
            TxtAddrs2.Enabled = true;
            TxtState.Enabled = true;
            TxtInitial.Visible = true;
            TxtAddrs1.Visible = true;
            TxtAddrs2.Visible = true;
            TxtState.Visible = true;
            ddlCiudad.Visible = true;
            lblInitial.Visible = true;
            lbladdress1.Visible = true;
            lbladdress2.Visible = true;
            lblCity.Visible = true;
            lblZipCode.Visible = true;
            lblState.Visible = true;
            ChkAutoAssignPolicy.Enabled = true;

            TxtOccupa.Enabled = true;
            TxtLicense.Enabled = true;
            TxtOccupa.Visible = false;
            TxtLicense.Visible = false;
            lblOccupa.Visible = false;
            lblLicense.Visible = false;

            txtPhyAddress.Enabled = true;
            txtPhyAddress2.Enabled = true;
            txtPhyState.Enabled = true;


            //al entrar un RES nuevo el SS puede ser añadido por cualquier usario
            //una vez añadido solo los que tengan el permiso "MODIFY SOCIAL SECURITY" podran cambiarlo 

            if (taskControl.TaskControlID != 0)
            {
                txtSocSec.Enabled = false;//false
            }
            else
            {
                txtSocSec.Enabled = true;
            }

            ddlPhyZipCode.Enabled = true;
            ddlZip.Enabled = true;
            //ddlPhyCity.Enabled = true;

            txtPhyAddress.Visible = true;
            txtPhyAddress2.Visible = true;
            txtPhyState.Visible = true;

            ddlPhyZipCode.Visible = true;
            ddlZip.Visible = true;
            ddlPhyCity.Visible = true;

            //TxtPolicyNo.Visible = true;

            if (TxtPolicyNo.Text.Trim() != "")
            {
                txtPolicyNoToRenew.Visible = false;
                //TxtPolicyNo.Visible = true;
            }
            else
            {
                txtPolicyNoToRenew.Visible = true;
                txtPolicyNoToRenew.Enabled = true;
                //TxtPolicyNo.Visible = false;
            }

            TxtPolicyType.Visible = false;
            TxtSufijo.Visible = false;
            TxtCity.Enabled = false;
            TxtCity.Visible = false;
            lblPolicyNo.Visible = true;
            lblPolicyType.Visible = false;
            lblSuffix.Visible = false;
            lblSelectedAgent.Visible = true;

            //BOTONES PRINT
            btnPrintPolicy.Visible = false; //false alexis
            //btnIndemnityPolicy.Visible = false;
            ddlPrintOption.Visible = false;
            btnInvoice.Visible = false;
            btnPrintQuote.Visible = false;

            TxtInitial.Enabled = true;
            TxtAddrs1.Enabled = true;
            TxtAddrs2.Enabled = true;
            TxtCity.Enabled = false;
            TxtState.Enabled = true;
            //ddlCiudad.Enabled = true;

            TxtHomePhone.Enabled = true;
            txtWorkPhone.Enabled = true;
            TxtCellular.Enabled = true;
            txtEmail.Enabled = true;
            TxtPolicyNo.Enabled = false;
            ddlAgent.Enabled = true;

            TxtPolicyType.Enabled = false;
            TxtSufijo.Enabled = false;
            TxtTerm.Enabled = true;
            txtEffDt.Enabled = true;
            txtExpDt.Enabled = false;
            txtEntryDate.Enabled = false;
            imgCalendarEff.Visible = true;

            //Enable Auto Control
            ddlBank.Enabled = true;
            ddlCompanyDealer.Enabled = true;

            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            ddlOriginatedAt.Enabled = false;
            ddlInsuranceCompany.Enabled = false;
            ddlAgency.Enabled = false;

            lblPrintOption.Visible = false;
            chkInsured.Visible = false;
            chkProducer.Visible = false;
            chkCompany.Visible = false;
            chkAgency.Visible = false;
            chkExtraCopy.Visible = false;

            if (taskControl.TaskControlID != 0 && btnAcceptQuote.Visible == false)
            {
                BtnSave.Text = "ISSUE Policy";
            }

            ddlType.Enabled = true;
            txtCompanyName.Enabled = true;
            EnableDisableRESParameters(true);

        }
        private void SendEmail(string CustomerName, string Email, string PaymentMethod, string PaymentType, string PaymentAmount, string AccNumber, string EntryDate, string PolicyNumber, string DebitDate)
        {
            try
            {

                //EPolicy.TaskControl.GuardianXtra taskControl = (EPolicy.TaskControl.GuardianXtra)Session["TaskControl"];
                EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
                //Email (El email que ve el que recibe)
                string emailNoreplay = "policyconfirmation@midoceanpr.com"; //"lsemailservice@gmail.com"; //"policyconfirmation@midoceanpr.com";//"lsemailservice@gmail.com";
                string EmailNoReplayPass = "Conf1rm@tion";
                //Email (That send the message)
                string emailSend = "lsemailservice@gmail.com";
                string msg = "";
                string pdf = "";// TextBox1.Text;
                MailMessage SM = new MailMessage();

                SM.Subject = "Guardian Insurance - Your payment has been received";
                SM.From = new System.Net.Mail.MailAddress(emailNoreplay);
                SM.Body = "<p>Dear " + CustomerName + " (" + Email + ")</p><p>This email is to inform you that MidOcean Insurance Agency has processed a single " + PaymentMethod + " for the amount of $" + PaymentAmount + " on " + EntryDate + ". The description MidOcean Insurance Agency has provided for this transaction is as follows: " + PolicyNumber + ".</p><p>If this transaction is an error or is a fraudulent transaction, please contact MidOcean Insurance Agency at 787-520-6178 if you have any questions or concerns. Thank you for your payment.</p>";
                SM.IsBodyHtml = true;
                SM.Attachments.Add(new Attachment(@"F:\inetpub\wwwroot\EpmsTest\ExportFiles\" + pdf));

                SM.To.Add(Email);

                SM.Bcc.Add("arosado@lanzasoftware.com");
                SM.Bcc.Add("econcepcion@guardianinsurance.com");
                SM.Bcc.Add("smartinez@guardianinsurance.com");
                SM.Bcc.Add("rcruz@guardianinsurance.com");
                SM.Bcc.Add("susana.martinez11@gmail.com");
                //SM.Bcc.Add("jnieves@lanzasoftware.com");
                SM.Bcc.Add("ydejesus@lanzasoftware.com");


                try
                {
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(emailNoreplay, EmailNoReplayPass);// new NetworkCredential(emailNoreplay, "L@nzaSoft1$"); //"Conf1rm@tion"
                    client.Host = ConfigurationManager.AppSettings["IPMail"].ToString().Trim();    //client.Host = "smtp.gmail.com";
                    client.Port = 587; // 25;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    client.Send(SM);
                    msg = "0001";
                }
                catch (Exception exc)
                {
                    //fail = true;
                    msg = exc.InnerException.ToString() + " " + exc.Message;
                }

                // msg = SM.SendHTMLMailPagoMembresia(TaskControlID, PaymentID);
                //msg = SM.SendHTMLMailPagoMembresia(1, 7);
            }
            catch (Exception exc)
            {

            }
        }
        private string GetTransactionNumber()
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[0];
            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            int transNo = exec.Insert("AddTransactionNumber", xmlDoc);

            return transNo.ToString();
        }
        private void RemoveSessionLookUp()
        {
            Session.Remove("LookUpTables");
        }
        #endregion
        #region Funciones Optimizadas
        private void GetCheckResPolicy()
        {
            //Ruta de la clase App_Code/Class/RES/checked_retun.cs
            UtilitiesComponents.RES.checked_return getCheckResult = new UtilitiesComponents.RES.checked_return();
            //General Coverage
            intOwner = getCheckResult.intValue(chkOwner);
            intGeneralLesee = getCheckResult.intValue(chkGeneralLesee);
            intTenant = getCheckResult.intValue(chkTenant);
            intOther = getCheckResult.intValue(chkOther);
            //Type of Insured
            intIndividual = getCheckResult.intValue(chkIndividual);
            intPartnership = getCheckResult.intValue(chkPartnership);
            intCorporation = getCheckResult.intValue(chkCorporation);
            intJoinVenture = getCheckResult.intValue(chkJoinVenture);
            intOtherTI = getCheckResult.intValue(chkOtherTI);
        }
        private void SetRESQuoteParameters(EPolicy.TaskControl.RES taskControl)
        {
            GetCheckResPolicy();
            strInsuredName = taskControl.Customer.FirstName.ToString() + " " +
                             taskControl.Customer.Initial.ToString() + ". " +
                             taskControl.Customer.LastName1.ToString() + " " +
                             taskControl.Customer.LastName2.ToString();

            //General Coverages
            taskControl.Owner = intOwner;
            taskControl.GeneralLesee = intGeneralLesee;
            taskControl.Tenant = intTenant;
            taskControl.Other = intOther;
            //Policy Information
            taskControl.InsuredPremises = txtInsuredPremise.Text;
            taskControl.InsuredName = strInsuredName;
            taskControl.PartOccupied = txtPartOcupied.Text;
            //Type of Insured
            taskControl.Individual = intIndividual;
            taskControl.Partnership = intPartnership;
            taskControl.Corporation = intCorporation;
            taskControl.JoinVenture = intJoinVenture;
            taskControl.OtherTI = intOtherTI;
            //Liabily Limit
            if (taskControl.isQuote)
            {
                LoadBILimitIndex();
                taskControl.BILimit = intRESLiabilityID;
                taskControl.PDLimit = intRESLiabilityID;
            }
            taskControl.RESLiability = intRESLiability;
            taskControl.MedicalPayment = strMedicalPayment;
            taskControl.FireDamage = strFireDamage;
            //
            if (BtnSave.Text == "ISSUE Policy" && taskControl.TypePolicy != 0)
                intTypePolicy = taskControl.TypePolicy;
            taskControl.TypePolicy = intTypePolicy;
        }
        private void SetRESParameters(EPolicy.TaskControl.RES taskControl, EPolicy.TaskControl.RES taskControlQuote)
        {
            GetCheckResPolicy();
            //General Coverages			  
            taskControl.Owner = taskControlQuote.Owner;
            taskControl.GeneralLesee = taskControlQuote.GeneralLesee;
            taskControl.Tenant = taskControlQuote.Tenant;
            taskControl.Other = taskControlQuote.Other;
            //Policy Information        
            taskControl.InsuredPremises = taskControlQuote.InsuredPremises;
            taskControl.InsuredName = taskControlQuote.InsuredName;
            taskControl.PartOccupied = taskControlQuote.PartOccupied;
            //Type of Insured             
            taskControl.Individual = taskControlQuote.Individual;
            taskControl.Partnership = taskControlQuote.Partnership;
            taskControl.Corporation = taskControlQuote.Corporation;
            taskControl.JoinVenture = taskControlQuote.JoinVenture;
            taskControl.OtherTI = taskControlQuote.OtherTI;
            //Liabily Limit               
            taskControl.BILimit = taskControlQuote.BILimit;
            taskControl.PDLimit = taskControlQuote.PDLimit;
            taskControl.MedicalPayment = taskControlQuote.MedicalPayment;
            taskControl.FireDamage = taskControlQuote.FireDamage;
            //Liability Properties
            taskControl.TypePolicy = taskControlQuote.TypePolicy;
            intTypePolicy = taskControlQuote.TypePolicy;

        }
        private void LoadRESParameters(EPolicy.TaskControl.RES taskControl)
        {
            LoadchkRESPolicy(taskControl.Owner, chkOwner);
            LoadchkRESPolicy(taskControl.GeneralLesee, chkGeneralLesee);
            LoadchkRESPolicy(taskControl.Tenant, chkTenant);
            LoadchkRESPolicy(taskControl.Other, chkOther);
            //
            LoadchkRESPolicy(taskControl.Individual, chkIndividual);
            LoadchkRESPolicy(taskControl.Partnership, chkPartnership);
            LoadchkRESPolicy(taskControl.Corporation, chkCorporation);
            LoadchkRESPolicy(taskControl.JoinVenture, chkJoinVenture);
            LoadchkRESPolicy(taskControl.OtherTI, chkOtherTI);
            //
            txtInsuredPremise.Text = taskControl.InsuredPremises;
            txtPartOcupied.Text = taskControl.PartOccupied;
            //
            txtCampoExclude1.Text = strTextoExcluded;
            txtCampoExclude2.Text = strTextoExcluded;
            txtMedicalPayment.Text = SetUSDFormat(strMedicalPayment);
            txtFireDamage.Text = SetUSDFormat(strFireDamage);
            //
            if (taskControl.BILimit > 0)
            {
                LoadDropDownBILimit();
                if (taskControl.BILimit > 5)
                    intBILimitIndex = taskControl.BILimit - 5;
                else
                    intBILimitIndex = taskControl.BILimit;
                dllBILimit.SelectedIndex = intBILimitIndex;
                getBILimitValue();
            }
        }
        private void EnableDisableRESParameters(bool isEnabled)
        {
            bool boDefaultEnable = false;
            //
            txtPartOcupied.Enabled = isEnabled;
            dllBILimit.Enabled = isEnabled;
            txtInsuredPremise.Enabled = isEnabled;
            //General Coverages
            chkOwner.Enabled = isEnabled;
            chkGeneralLesee.Enabled = isEnabled;
            chkTenant.Enabled = isEnabled;
            chkOther.Enabled = isEnabled;
            //Type of Insured
            chkIndividual.Enabled = isEnabled;
            chkPartnership.Enabled = isEnabled;
            chkCorporation.Enabled = isEnabled;
            chkJoinVenture.Enabled = isEnabled;
            chkOtherTI.Enabled = isEnabled;
            //Default Disabled
            txtMedicalPayment.Enabled = boDefaultEnable;
            txtFireDamage.Enabled = boDefaultEnable;
            txtCampoExclude1.Enabled = boDefaultEnable;
            txtCampoExclude2.Enabled = boDefaultEnable;
            txtPDLimit.Enabled = boDefaultEnable;
            txtTotalPremiumLimit.Enabled = boDefaultEnable;
            txtGrossTax.Enabled = boDefaultEnable;

        }
        private void getBILimitValue()
        {
            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
            //
            double GrossTax = 0, LiaTotalPremium = 0;
            if (dllBILimit.SelectedIndex > 0)
                intRESLiabilityID = int.Parse(dllBILimit.SelectedItem.Value);
            string strRESLiabilityDesc = dllBILimit.SelectedItem.Text;
            strRESLiabilityPremium = GetPremiunLimit(intRESLiabilityID);
            txtPDLimit.Text = SetUSDFormat(strRESLiabilityDesc);
            //
            double.TryParse(strRESLiabilityPremium,
                System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol,
                System.Globalization.CultureInfo.CreateSpecificCulture("en-US"),
                out LiaTotalPremium);

            txtGrossTax.Text =
               LiaTotalPremium > 0.0 ?
               (Math.Round(CalculateAgentCommision(ddlAgent.SelectedItem.Value, LiaTotalPremium), 2)).ToString()
               : "0";

            double.TryParse(txtGrossTax.Text,
                System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol,
                System.Globalization.CultureInfo.CreateSpecificCulture("en-US"),
                out GrossTax);

            if (GrossTax > 0)
            {
                txtTotalPremiumResult.Text = (LiaTotalPremium + GrossTax).ToString("$##,###,###.00");
                txtGrossTax.Text = (GrossTax).ToString("$##,###,###.00");
            }
            else
            {
                txtTotalPremiumResult.Text = (LiaTotalPremium).ToString("$##,###,###.00");
            }
            txtTotalPremiumLimit.Text = (LiaTotalPremium).ToString("$##,###,###.00");
        }
        private void LoadchkRESPolicy(int intValue, CheckBox chkCheckBoxInput)
        {
            if (intValue == 1)
                chkCheckBoxInput.Checked = true;
            else
                chkCheckBoxInput.Checked = false;
        }
        private double CalculateAgentCommision(string AgentID, double TotalPremium)
        {
            try
            {
                double Result = TotalPremium;
                string strPolicyID = "'29'";
                string strPolicyClass = "'RES'";
                string conString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    connection.Open();

                    // SqlCommand should be created inside using.
                    // ... It receives the SQL statement.
                    // ... It receives the connection object.
                    // ... The SQL text works with a specific database.

                    using (SqlCommand command = new SqlCommand(
                        String.Format(@"SELECT top 1 * FROM CommissionAgent 
                                        WHERE RTRIM(LTRIM(AgentID)) = RTRIM(LTRIM({0})) 
                                        and RTRIM(LTRIM(PolicyClassID)) = RTRIM(LTRIM({1})) 
                                        AND PolicyType = {2}
                                        order by EffectiveDate desc", "'" + AgentID + "'", strPolicyID, strPolicyClass), connection))
                    {
                        //
                        // Instance methods can be used on the SqlCommand.
                        // ... These read data.
                        //
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            var tb = new System.Data.DataTable();
                            tb.Load(reader);
                            if (tb != null)
                            {
                                if (tb.Rows.Count > 0)
                                {
                                    var ComRate = (double.Parse(tb.Rows[0]["CommissionRate"].ToString()) / 100.0);

                                    Result = ((TotalPremium * ComRate) * 0.05);
                                }
                                else
                                {
                                    Result = 0;
                                }
                            }
                        }
                    }
                }

                return Result;
            }
            catch (Exception)
            {
                return TotalPremium;
            }
        }
        private string GetPremiunLimit(int intRESLiabilityID)
        {
            String strResult = "";
            DataTable dtRESLiability = EPolicy.LookupTables.LookupTables.GetTable("RESLiability");
            DataRow[] drWhere = dtRESLiability.Select("RliabilityID = " + intRESLiabilityID);

            foreach (DataRow row in drWhere)
            {
                strResult = row["Premium"].ToString();
            }

            return strResult;
        }
        private string SetUSDFormat(string strInput)
        {
            double douProcesar = 0;
            string strReturn = "";

            double.TryParse(strInput,
                System.Globalization.NumberStyles.Number | System.Globalization.NumberStyles.AllowCurrencySymbol,
                System.Globalization.CultureInfo.CreateSpecificCulture("en-US"),
                out douProcesar);

            strReturn = douProcesar.ToString("$##,###,###.00");
            return strReturn;
        }
        private void PrintReportRES(String strTemplate, String strPolicyNo)
        {
            try
            {
                UtilitiesComponents.RES.print_report ReportParamaters = LoadPrintPolicy();
                EPolicy.TaskControl.RES taskControl = LoadTaskControl();
                string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];
                strInsuredNameMailingAddress = taskControl.Customer.FirstName + " " + taskControl.Customer.Initial + ". " + taskControl.Customer.LastName1 + " " + taskControl.Customer.Address1 + " " + taskControl.Customer.Address2 + " " + taskControl.Customer.City + " " + taskControl.Customer.State + " " + taskControl.Customer.ZipCode;
                //
                ReportParamaters.effective_dates = taskControl.EffectiveDate;
                ReportParamaters.expiration_dates = taskControl.ExpirationDate;
                ReportParamaters.type_policy = strTypePolicy;
                ReportParamaters.policy_number = strPolicyNo;
                ReportParamaters.renewal_of = strRenewalOf;
                ReportParamaters.insured_name_mailing_address = strInsuredNameMailingAddress;
                ReportParamaters.mobile_phone = taskControl.Customer.HomePhone;
                ReportParamaters.email = txtEmail.Text;
                ReportParamaters.occupation = taskControl.Customer.Occupation;
                ReportParamaters.mobile_phone = taskControl.Customer.Cellular;
                ReportParamaters.business_phone = taskControl.Customer.JobPhone;
                ReportParamaters.email = taskControl.Customer.Email;
                ReportParamaters.agency = ddlAgent.SelectedItem.Text;
                ReportParamaters.insured_premises = taskControl.InsuredPremises;
                ReportParamaters.owner = taskControl.Owner.ToString();
                ReportParamaters.general_lesee = taskControl.GeneralLesee.ToString();
                ReportParamaters.tenant = taskControl.Tenant.ToString();
                ReportParamaters.other = taskControl.Other.ToString();
                ReportParamaters.individual = taskControl.Individual.ToString();
                ReportParamaters.corporation = taskControl.Corporation.ToString();
                ReportParamaters.partnership = taskControl.Partnership.ToString();
                ReportParamaters.join_venture = taskControl.JoinVenture.ToString();
                ReportParamaters.other_ti = taskControl.OtherTI.ToString();
                ReportParamaters.bodily_injury = txtPDLimit.Text;
                ReportParamaters.property_damage = txtPDLimit.Text;
                ReportParamaters.medical_payment = SetUSDFormat(strMedicalPayment);
                ReportParamaters.fire_damage = SetUSDFormat(strFireDamage);
                ReportParamaters.gross_tax = txtGrossTax.Text;
                ReportParamaters.total_premium_limit = txtTotalPremiumLimit.Text;
                ReportParamaters.total_premium = txtTotalPremiumResult.Text;

                List<String> mergePaths = ReportParamaters.print_RES_report(strTemplate, taskControl.TaskControlID, Server);

                OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                string FinalFile = "";
                FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);

            }
            catch (Exception exc)
            {

                lblRecHeader.Text = exc.Message.Trim() + " - ";
                mpeSeleccion.Show();
            }
        }
        private void LoadDropDownBILimit() 
        {
            if (boFunction == false)
            {
                strRESLiabilityDT = "RESLiabilityResidential";
                intTypePolicy = 1;
                //
                if (ddlType.SelectedIndex > 0)
                {
                    strRESLiabilityDT = "RESLiabilityCommercial";
                    intTypePolicy = 2;
                }
                //
                DataTable dtRESLiability = EPolicy.LookupTables.LookupTables.GetTable(strRESLiabilityDT);
                dllBILimit.DataSource = dtRESLiability;
                dllBILimit.DataTextField = "LimitDesc";
                dllBILimit.DataValueField = "RliabilityID";
                dllBILimit.DataBind();
                dllBILimit.Items.Insert(0, "");
                //
                if (intBILimitIndex == 0)
                    dllBILimit.SelectedIndex = -1;
                else
                {
                    dllBILimit.SelectedIndex = intBILimitIndex;
                    getBILimitValue();
                }
                boFunction = true;
            }
            else
                boFunction = false;
        }
        private void LoadBILimitIndex() 
        {
            intTypePolicy = 1;
            if (ddlType.SelectedIndex > 0)
                intTypePolicy = 2;

            if (intRESLiabilityID == 0)
                getBILimitValue();
            else
                LoadDropDownBILimit();
        }
        private void btnDisable() 
        {
            if (intAppStatus == 0) 
            {
                btnAcceptQuote.Visible = false;
                btnAdjuntar.Visible = false;
                BtnSave.Text = "SAVE";
            }
        }
        private void onLoadPage() 
        {
            intAppStatus = 1;
        }
        private void editForm() 
        {
            intAppStatus = 0;
            btnDisable();
        }
        private void PrintInvoice() 
        {
            EPolicy.TaskControl.RES taskControl = (EPolicy.TaskControl.RES)Session["TaskControl"];
            List<string> mergePaths = new List<string>();
            mergePaths = imprimirPolicy(mergePaths, taskControl.TaskControlID, "AgentInvoice_VI");
            string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
            //Generar PDF
            OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
            string FinalFile = "";
            FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
        }
        private List<string> imprimirPolicy(List<string> mergePaths, int taskControlID, string name) 
        {
            try 
            {
                ReportViewer viewer1 = new ReportViewer();
                viewer1.LocalReport.ReportPath = Server.MapPath("Reports/RES/" + name + ".rdlc");
                viewer1.LocalReport.DataSources.Clear();
                viewer1.ProcessingMode = ProcessingMode.Local;
                EPolicy.TaskControl.RES taskControl1 = (EPolicy.TaskControl.RES)Session["TaskControl"];
                GetInvoiceInfoByTaskControlIDTableAdapters.GetInvoiceInfoByTaskControlIDTableAdapter ta2 = new GetInvoiceInfoByTaskControlIDTableAdapters.GetInvoiceInfoByTaskControlIDTableAdapter();
                ReportDataSource rds3 = new ReportDataSource();
                rds3 = new ReportDataSource("GetInvoiceInfoByTaskControlID", (System.Data.DataTable)ta2.GetData(taskControlID));
    
                if (name == "AgentInvoice_VI")
                {
                    string ImgPath = "", AgentDesc = "";
                    Uri pathAsUri = null;
                    System.Data.DataTable dt = null, dtAgent = null;
                    dt = EPolicy.TaskControl.TaskControl.GetImageLogoByAgentID(taskControl1.Agent.ToString().Trim());
                    dtAgent = EPolicy.TaskControl.TaskControl.GetAgentByAgentID(taskControl1.Agent.ToString().Trim());

                    if (dtAgent.Rows.Count > 0)
                        AgentDesc = dtAgent.Rows[0]["AgentDesc"].ToString().Trim();

                    if (dt.Rows.Count > 0)
                    {
                        ImgPath = Server.MapPath("Images2\\AgencyLogos\\" + dt.Rows[0]["ImgPath"].ToString().Trim());
                        pathAsUri = new Uri(ImgPath);
                    }
                    else if (AgentDesc.ToUpper().Contains("GUARDIAN"))
                    {
                        ImgPath = Server.MapPath("Images2\\AgencyLogos\\guardianLogo.png");
                        pathAsUri = new Uri(ImgPath);
                    }

                    ReportParameter[] parameters = new ReportParameter[1];
                    parameters[0] = new ReportParameter("ImgPath", pathAsUri != null ? pathAsUri.AbsoluteUri : "");

                    viewer1.LocalReport.EnableExternalImages = true;
                    viewer1.LocalReport.DataSources.Add(rds3);
                    viewer1.LocalReport.SetParameters(parameters);
                }
                viewer1.LocalReport.Refresh();

                Warning[] warnings = null;
                string[] streamIds = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;
                string filetype = string.Empty;


                //string fileName1 = "PolicyNo- " + taskControl.ToString().Trim() + "-" + name; //+ "-" + VehicleDetailID.ToString().Trim() + "-Com1";
                string _FileName1 = "PolicyNo- " + taskControlID + "-" + name + ".pdf"; //+ "-" + VehicleDetailID.ToString().Trim() + "-Com1" + ".pdf";

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private EPolicy.TaskControl.RES LoadTaskControl()
        {
            return (EPolicy.TaskControl.RES)Session["TaskControl"];
        }
        private UtilitiesComponents.RES.print_report LoadPrintPolicy() 
        {
            return new UtilitiesComponents.RES.print_report();
        }
        #endregion
    }
}