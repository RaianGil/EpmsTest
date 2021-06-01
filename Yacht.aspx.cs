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
using System.Linq;

public partial class Yacht : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        this.MaintainScrollPositionOnPostBack = true;
        AccordionEndorsement.Visible = false;
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
            if (!cp.IsInRole("ADMINISTRATOR") && !cp.IsInRole("YACHT"))
            {
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                Response.Cookies.Add(authCookies);
                FormsAuthentication.SignOut();
                Response.Redirect("Default.aspx?001");
            }
        }

        if (cp.UserID == 1)
            btnSentToPPS.Visible = true;
        else
            btnSentToPPS.Visible = false;

        if (Page.IsPostBack)
        {

            if (Session["TaskControl"] == null)
            {
                HttpCookie authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, null);
                Response.Cookies.Add(authCookies);
                FormsAuthentication.SignOut();

                Response.Redirect("Default.aspx?007");
            }

            string targetId = Page.Request.Params.Get("__EVENTTARGET");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "focusthis", "document.getElementById('" + targetId + "').focus()", true);
        }

        if (Session["AutoPostBack"] == null)
        {
            if (!IsPostBack)
            {
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);


                EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

                if (Session["LookUpTables"] == null)
                {
                    DataTable dtHomeport = EPolicy.LookupTables.LookupTables.GetTable("Homeport");
                    DataTable dtDeductible = EPolicy.LookupTables.LookupTables.GetTable("Deductible");
                    DataTable dtProtectionAndIndemnity = EPolicy.LookupTables.LookupTables.GetTable("ProtectionAndIndemnity");
                    DataTable dtPILiabilityOnly = EPolicy.LookupTables.LookupTables.GetTable("PILiabilityOnly");
                    DataTable dtMedicalPayment = EPolicy.LookupTables.LookupTables.GetTable("MedicalPayment");
                    DataTable dtUninsuredBoaters = EPolicy.LookupTables.LookupTables.GetTable("UninsuredBoater");
                    DataTable dtNavigationLimit = EPolicy.LookupTables.LookupTables.GetTable("NavigationLimit");
                    //DataTable dtAgent = EPolicy.LookupTables.LookupTables.GetTable("Agent");
                    DataTable dtInsuranceCompany = EPolicy.LookupTables.LookupTables.GetTable("InsuranceCompany");
                    DataTable dtBank = EPolicy.LookupTables.LookupTables.GetTable("Bank_VI");
                    DataTable dtAgency = EPolicy.LookupTables.LookupTables.GetTable("AgencyYacht");

                    ddlHomeport.DataSource = dtHomeport;
                    ddlHomeport.DataTextField = "HomeportDesc";
                    ddlHomeport.DataValueField = "HomeportID";
                    ddlHomeport.DataBind();
                    ddlHomeport.SelectedIndex = -1;
                    ddlHomeport.Items.Insert(0, "");

                    ddlDeductibles1.DataSource = dtDeductible;
                    ddlDeductibles1.DataTextField = "DeductibleDesc";
                    ddlDeductibles1.DataValueField = "DeductibleID";
                    ddlDeductibles1.DataBind();
                    ddlDeductibles1.SelectedIndex = -1;
                    ddlDeductibles1.Items.Insert(0, "");

                    ddlDeductibles2.DataSource = dtDeductible;
                    ddlDeductibles2.DataTextField = "DeductibleDesc";
                    ddlDeductibles2.DataValueField = "DeductibleID";
                    ddlDeductibles2.DataBind();
                    ddlDeductibles2.SelectedIndex = -1;
                    ddlDeductibles2.Items.Insert(0, "");


                    ddlPI.DataSource = dtProtectionAndIndemnity;
                    ddlPI.DataTextField = "PIAmount";
                    ddlPI.DataValueField = "PIID";
                    ddlPI.DataBind();
                    ddlPI.SelectedIndex = -1;
                    ddlPI.Items.Insert(0, "");

                    ddlPILiabilityOnly.DataSource = dtPILiabilityOnly;
                    ddlPILiabilityOnly.DataTextField = "PILiabilityOnlyAmount";
                    ddlPILiabilityOnly.DataValueField = "PILiabilityOnlyID";
                    ddlPILiabilityOnly.DataBind();
                    ddlPILiabilityOnly.SelectedIndex = -1;
                    ddlPILiabilityOnly.Items.Insert(0, "");

                    ddlMedicalPayment.DataSource = dtMedicalPayment;
                    ddlMedicalPayment.DataTextField = "MedicalPaymentAmount";
                    ddlMedicalPayment.DataValueField = "MedicalPaymentID";
                    ddlMedicalPayment.DataBind();
                    ddlMedicalPayment.SelectedIndex = -1;
                    ddlMedicalPayment.Items.Insert(0, "");

                    ddlUninsuredBoaters.DataSource = dtUninsuredBoaters;
                    ddlUninsuredBoaters.DataTextField = "UninsuredBoaterAmount";
                    ddlUninsuredBoaters.DataValueField = "UninsuredBoaterID";
                    ddlUninsuredBoaters.DataBind();
                    ddlUninsuredBoaters.SelectedIndex = -1;
                    ddlUninsuredBoaters.Items.Insert(0, "");

                    ddlNavigationLimit.DataSource = dtNavigationLimit;
                    ddlNavigationLimit.DataTextField = "NavigationLimitDesc";
                    ddlNavigationLimit.DataValueField = "NavigationLimitID";
                    ddlNavigationLimit.DataBind();
                    ddlNavigationLimit.SelectedIndex = -1;
                    ddlNavigationLimit.Items.Insert(0, "");

                    //ddlAgent.DataSource = dtAgent;
                    //ddlAgent.DataTextField = "AgentDesc";
                    //ddlAgent.DataValueField = "AgentID";
                    //ddlAgent.DataBind();
                    //ddlAgent.SelectedIndex = -1;
                    //ddlAgent.Items.Insert(0, "");

                    //Agency
                    ddlAgency.DataSource = dtAgency;
                    ddlAgency.DataTextField = "AgentDesc";
                    ddlAgency.DataValueField = "AgentID";
                    ddlAgency.DataBind();
                    ddlAgency.SelectedIndex = -1;
                    ddlAgency.Items.Insert(0, "");

                    //InsuranceCompany
                    ddlInsuranceCompany.DataSource = dtInsuranceCompany;
                    ddlInsuranceCompany.DataTextField = "InsuranceCompanyDesc";
                    ddlInsuranceCompany.DataValueField = "InsuranceCompanyID";
                    ddlInsuranceCompany.DataBind();
                    ddlInsuranceCompany.SelectedIndex = 0;
                    ddlInsuranceCompany.Items.Insert(0, "");

                    //Bank
                    ddlBank.DataSource = dtBank;
                    ddlBank.DataTextField = "BankDesc";
                    ddlBank.DataValueField = "PPSID";
                    ddlBank.DataBind();
                    ddlBank.SelectedIndex = -1;
                    ddlBank.Items.Insert(0, "");

                    if (txtEntryDate.Text == "")
                        txtEntryDate.Text = DateTime.Now.ToShortDateString();

                    if (txtTerm.Text == "")
                        txtTerm.Text = "12";

                    if (txtEffectiveDate.Text == "")
                        txtEffectiveDate.Text = DateTime.Now.ToString("MM/dd/yyyy");

                    if (txtExpirationDate.Text == "")
                        txtExpirationDate.Text = DateTime.Now.AddYears(1).ToString("MM/dd/yyyy");

                    Calendar1.FirstDayOfWeek = FirstDayOfWeek.Sunday;
                    Calendar1.NextPrevFormat = NextPrevFormat.ShortMonth;
                    Calendar1.TitleFormat = TitleFormat.Month;
                    Calendar1.ShowGridLines = true;
                    Calendar1.DayStyle.HorizontalAlign = HorizontalAlign.Center;
                    Calendar1.DayStyle.VerticalAlign = VerticalAlign.Middle;
                    Calendar1.OtherMonthDayStyle.BackColor = System.Drawing.Color.LightGray;

                    Calendar2.FirstDayOfWeek = FirstDayOfWeek.Sunday;
                    Calendar2.NextPrevFormat = NextPrevFormat.ShortMonth;
                    Calendar2.TitleFormat = TitleFormat.Month;
                    Calendar2.ShowGridLines = true;
                    Calendar2.DayStyle.HorizontalAlign = HorizontalAlign.Center;
                    Calendar2.DayStyle.VerticalAlign = VerticalAlign.Middle;
                    Calendar2.OtherMonthDayStyle.BackColor = System.Drawing.Color.LightGray;

                    //if (ddlUninsuredBoaters.SelectedIndex == 0 && txtOtherUninsuredBoater.Text == "")
                    //{
                    //    ddlUninsuredBoaters.SelectedIndex = 1;
                    //}

                    switch (taskControl.Mode)
                    {
                        case 1: //ADD
                            EnableControl();
                            FillTextControl();
                            radioBtnTP1.Visible = false;
                            radioBtnTP1.Enabled = false;
                            radioBtnTP2.Visible = false;
                            radioBtnTP2.Enabled = false;
                            break;

                        case 2: //UPDATE
                            FillTextControl();
                            EnableControl();

                            break;

                        default:    //DELETE & CLEAR
                            if (taskControl.isQuote == true)
                            {
                                FillTextControl();
                                DisableControl();
                                btnSave.Visible = false;
                                btnCancel.Visible = false;
                                if (taskControl.isQuote == true && Session["AUTOEndorsement"] != null)
                                {
                                    BtnExitEndorsement.Visible = true;
                                    btnModify.Visible = false;
                                    btnAcceptQuote.Visible = false;
                                }
                                else
                                {
                                    btnModify.Enabled = true;
                                    btnModify.Visible = true;
                                    if (taskControl.isAcceptQuote == false)
                                    {
                                        btnAcceptQuote.Enabled = true;
                                        btnAcceptQuote.Visible = true;

                                    }
                                    else
                                    {
                                        btnConvert.Enabled = true;
                                        btnConvert.Visible = true;
                                        btnAcceptQuote.Enabled = false;
                                        btnAcceptQuote.Visible = false;
                                        btnPremiumFinance.Visible = true;
                                        taskControl.isAcceptQuote = true;
                                        if (txtTotalPremiumPoliza.Text.Trim() != "$0")
                                        {
                                            radioBtnTP1.Enabled = true;
                                            radioBtnTP1.Visible = true;
                                        }
                                        if (txtTotalPremium2.Text.Trim() != "$0")
                                        {
                                            radioBtnTP2.Enabled = true;
                                            radioBtnTP2.Visible = true;
                                        }
                                    }
                                }
                                btnPreview.Enabled = true;
                                btnPreview.Visible = true;
                                btnPreview2.Enabled = true;
                                btnPreview2.Visible = true;
                            }
                            else
                            {
                                FillTextControl();
                                DisableControl();
                                btnSave.Visible = false;
                                btnCancel.Visible = false;
                                btnAcceptQuote.Enabled = false;
                                btnAcceptQuote.Visible = false;
                                btnPremiumFinance.Visible = false;
                                radioBtnTP1.Visible = false;
                                radioBtnTP1.Enabled = false;
                                radioBtnTP2.Visible = false;
                                radioBtnTP2.Enabled = false;
                                lblTotalPremiumOpcion1.Visible = false;
                                lblTotalPremium.Visible = false;
                                txtTotalPremium.Visible = false;
                                lblTotalPremiumOpcion2.Visible = false;
                                lblTotalPremium2.Visible = false;
                                txtTotalPremium2.Visible = false;
                            }
                            break;
                    }

                }

            }
            else
            {
                if (Session["TaskControl"] != null)
                {

                    EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
                    if (taskControl.Mode == 4)
                    {
                        //DisableControl();
                    }
                }
            }

        }
        else
        {
            FillTextControl();
            EnableControl();
            Session.Remove("AutoPostBack");
        }

        if (!Page.IsPostBack && (Request.QueryString["EndorsementSection"] != "" && Request.QueryString["EndorsementSection"] != null))
        {
            int OppEndorID = int.Parse(Request.QueryString["EndorsementSection"].ToString());
            ActiveEndorsementSectionFromQuoteForApply(OppEndorID);

            string script = "$(document).ready(function() {" +
         "$('html,body').animate({ " +
             "scrollTop: $('#EndorsementSection').offset().top " +
         "}, 0);" + "});";

            if (!Page.ClientScript.IsStartupScriptRegistered("ScrollToElement"))
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ScrollToElement", script, true);

        }

    }

    private void EnableControl()
    {
        //TEXTBOXES
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        //POLICY DETAILS
        imgCalendarEffectiveDate.Enabled = true;
        //txtExpirationDate.Enabled = true;
        //txtTerm.Enabled = true;
        ddlInsuranceCompany.Enabled = false;

        //INSURED INFORMATION TEXBOXES
        txtCustomerNo.Enabled = false;
        txtFirstName.Enabled = true;
        txtInitial.Enabled = true;
        txtLastName.Enabled = true;
        txtCompanyName.Enabled = true;
        txtHomePhone.Enabled = true;
        txtWorkPhone.Enabled = true;
        txtCellular.Enabled = true;
        txtemail.Enabled = true;
        txtAddrs1.Enabled = true;
        txtAddrs2.Enabled = true;
        txtZip.Enabled = true;
        txtCiudad.Enabled = true;
        txtState.Enabled = true;
        txtPhyAddress.Enabled = true;
        txtPhyAddress2.Enabled = true;
        txtPhyZipCode.Enabled = true;
        txtPhyCity.Enabled = true;
        txtPhyState.Enabled = true;
        if (taskControl.TaskControlID != 0)
        {
            txtSSN.Enabled = false;
        }
        else
        {
            txtSSN.Enabled = true;
        }

        txtProducer.Enabled = true;

        //Yacht INFORMATION TEXTBOXES
        txtBoatName.Enabled = true;
        txtHullLimit.Enabled = true;
        txtBoatYear.Enabled = true;
        txtLOA.Enabled = true;
        txtBoatModel.Enabled = true;
        txtBoatBuilder.Enabled = true;
        txtMiscellaneousNotes.Enabled = true;
        txtHullNumber.Enabled = true;
        txtHomeportLocation.Enabled = true;
        txtTenderLimit.Enabled = true;
        txtTenderDesc.Enabled = true;
        txtTenderSerial.Enabled = true;


        //Limit coverage TEXTBOXES
        txtRate1.Enabled = true;
        txtRate2.Enabled = true;
        txtOtherMedicalPayment.Enabled = true;
        txtPersonalEffect.Enabled = true;
        txtPersonalEffectDeductible.Enabled = true;
        //txtPersonalEffectPremium.Enabled = true;
        txtTrailer.Enabled = true;
        if (ddlUninsuredBoaters.SelectedIndex == 0)
            txtOtherUninsuredBoater.Enabled = true;
        else
            txtOtherUninsuredBoater.Enabled = false;
        txtOtherUninsuredBoaterPremium.Enabled = true;


        //Additional information
        txtSubjectivityNote.Enabled = true;
        txtTripTransit.Enabled = true;
        txtSurveyFee.Enabled = true;
        txtSurveyName.Enabled = true;
        txtMiscellaneous.Enabled = true;


        //DROPDOWN

        //Yacht INFORMATION
        ddlHomeport.Enabled = true;
        ddlNavigationLimit.Enabled = true;

        //Yacht COVERAGE AND LIMIT DROPDOWN
        ddlDeductibles1.Enabled = true;
        ddlDeductibles2.Enabled = true;
        ddlPI.Enabled = true;
        ddlPILiabilityOnly.Enabled = true;
        ddlMedicalPayment.Enabled = true;
        ddlUninsuredBoaters.Enabled = true;
        //ddlAgent.Enabled = true;
        ddlAgency.Enabled = true;

        //Buttons
        btnSave.Enabled = true;
        btnCancel.Enabled = true;
        //btnSave.Visible = true;
        //btnCancel.Visible = true;
        btnAddRows.Enabled = true;
        btnAddRows.Visible = true;
        btnAddSurvey.Enabled = true;
        btnAddSurvey.Visible = true;
        btnAddNavigationLimit.Enabled = true;
        btnAddNavigationLimit.Visible = true;

        //GridViews
        gridViewTenderLimit.Enabled = true;
        gridViewSurvey.Enabled = true;
        gridViewNavigationLimit.Enabled = true;
        
        chkIsRenew.Enabled = true;
        chkIsCommercial.Enabled = true;
        chkSameMailing.Enabled = true;

        if (txtPolicyNoToRenew.Visible == true && taskControl.isQuote == true)
        {
            txtPolicyNoToRenew.Enabled = true;
            txtPolicyNoToRenewSuffix.Enabled = true;
            btnVerifyYachtInPPS.Enabled = true;
        }
        else
        {
            DivRenew.Visible = false;
            lblPolicyNoToRenew.Visible = false;
            txtPolicyNoToRenew.Visible = false;
            txtPolicyNoToRenew.Enabled = false;
            txtPolicyToRenewType.Visible = false;
            txtPolicyNoToRenew.Text = "";
            txtPolicyNoToRenewSuffix.Visible = false;
            txtPolicyNoToRenewSuffix.Enabled = false;
            txtPolicyNoToRenewSuffix.Text = "";
            btnVerifyYachtInPPS.Visible = false;
            btnVerifyYachtInPPS.Enabled = false;
        }

        if (taskControl.isQuote == true && Session["AUTOEndorsement"] != null)
        {
            BtnExitEndorsement.Visible = true;
            btnModify.Visible = false;
            btnAcceptQuote.Visible = false;
        }

        //if (taskControl.isQuote == true && Session["AUTOEndorsement"] != null)
        //{
        //    lblEffDtEndorsementPrimary.Visible = true;
        //    txtEffDtEndorsementPrimary.Visible = true;
        //    txtEffDtEndorsementPrimary.Enabled = true;

        //    BtnExitEndorsement.Visible = true;
        //    BtnExitEndorsement0.Visible = true;

        //}
        //else
        //{
        //    lblEffDtEndorsementPrimary.Visible = false;
        //    txtEffDtEndorsementPrimary.Visible = false;
        //    txtEffDtEndorsementPrimary.Enabled = false;
        //    BtnExitEndorsement.Visible = false;
        //    BtnExitEndorsement0.Visible = false;
        //}
    }

    private void DisableControl()
    {
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        //TEXTBOXES

        //POLICY DETAILS
        imgCalendarEffectiveDate.Enabled = false;
        //txtExpirationDate.Enabled = false;
        txtTerm.Enabled = false;
        ddlInsuranceCompany.Enabled = false;

        //CUSTOMER INFORMATION TEXBOXES
        txtCustomerNo.Enabled = false;
        txtFirstName.Enabled = false;
        txtInitial.Enabled = false;
        txtLastName.Enabled = false;
        txtCompanyName.Enabled = false;
        txtHomePhone.Enabled = false;
        txtWorkPhone.Enabled = false;
        txtCellular.Enabled = false;
        txtemail.Enabled = false;
        txtAddrs1.Enabled = false;
        txtAddrs2.Enabled = false;
        txtZip.Enabled = false;
        txtCiudad.Enabled = false;
        txtState.Enabled = false;
        txtPhyAddress.Enabled = false;
        txtPhyAddress2.Enabled = false;
        txtPhyZipCode.Enabled = false;
        txtPhyCity.Enabled = false;
        txtPhyState.Enabled = false;
        txtSSN.Enabled = false;

        txtProducer.Enabled = false;

        //Yacht INFORMATION TEXTBOXES
        txtBoatName.Enabled = false;
        txtHullLimit.Enabled = false;
        txtBoatYear.Enabled = false;
        txtLOA.Enabled = false;
        txtBoatModel.Enabled = false;
        txtBoatBuilder.Enabled = false;
        txtMiscellaneousNotes.Enabled = false;
        txtHullNumber.Enabled = false;
        txtHomeportLocation.Enabled = false;
        txtTenderLimit.Enabled = false;
        txtTenderDesc.Enabled = false;
        txtTenderSerial.Enabled = false;

        //Limit coverage 
        txtRate1.Enabled = false;
        txtRate2.Enabled = false;
        txtWatercraftLimitTotal1.Enabled = false;
        txtPIPremium.Enabled = false;
        txtOtherMedicalPayment.Enabled = false;
        txtMedicalPaymentPremiumTotal.Enabled = false;
        txtPersonalEffect.Enabled = false;
        txtPersonalEffectDeductible.Enabled = false;
        txtPersonalEffectPremium.Enabled = false;
        txtTrailer.Enabled = false;
        txtOtherUninsuredBoaterPremium.Enabled = false;
        txtOtherUninsuredBoater.Enabled = false;

        //Additional information
        txtSubjectivityNote.Enabled = false;
        txtTripTransit.Enabled = false;
        txtSurveyFee.Enabled = false;
        txtSurveyName.Enabled = false;
        txtMiscellaneous.Enabled = false;

        //DROPDOWNS

        //Yacht INFORMATION
        ddlHomeport.Enabled = false;
        ddlNavigationLimit.Enabled = false;

        //Yacht COVERAGE AND LIMIT DROPDOWN
        ddlDeductibles1.Enabled = false;
        ddlDeductibles2.Enabled = false;
        ddlPI.Enabled = false;
        ddlPILiabilityOnly.Enabled = false;
        ddlMedicalPayment.Enabled = false;
        ddlUninsuredBoaters.Enabled = false;
        //ddlAgent.Enabled = false;
        ddlAgency.Enabled = false;

        //Buttons
        btnSave.Enabled = false;
        btnCancel.Enabled = false;
        btnAddRows.Enabled = false;
        btnAddRows.Visible = false;
        btnAddSurvey.Enabled = false;
        btnAddSurvey.Visible = false;
        btnAddNavigationLimit.Enabled = false;
        btnAddNavigationLimit.Visible = false;

        //GridViews
        gridViewTenderLimit.Enabled = false;
        gridViewSurvey.Enabled = false;
        gridViewNavigationLimit.Enabled = false;

        chkIsRenew.Enabled = false;
        chkIsCommercial.Enabled = false;
        chkSameMailing.Enabled = false;

        ddlBank.Visible = false;
        ddlBank.Enabled = false;
        txtEngine.Enabled = false;
        txtEngineSerialNumber.Enabled = false;
        txtTrailerModel.Enabled = false;
        txtTrailerSerial.Enabled = false;
        btnBankList.Enabled = false;

        if (txtPolicyNoToRenew.Visible == true)
        {
            txtPolicyNoToRenew.Enabled = false;
            btnVerifyYachtInPPS.Enabled = false;
            if (taskControl.isQuote == false)
            {
                DivRenew.Visible = false;
                lblPolicyNoToRenew.Visible = false;
                txtPolicyNoToRenew.Visible = false;
                txtPolicyNoToRenew.Enabled = false;
                txtPolicyNoToRenew.Text = "";
                txtPolicyToRenewType.Visible = false;
                txtPolicyNoToRenewSuffix.Visible = false;
                txtPolicyNoToRenewSuffix.Enabled = false;
                txtPolicyNoToRenewSuffix.Text = "";
                btnVerifyYachtInPPS.Visible = false;
                btnVerifyYachtInPPS.Enabled = false;
            }
        }

        if (taskControl.isQuote == true && Session["AUTOEndorsement"] != null)
        {
            BtnExitEndorsement.Visible = true;
            btnModify.Visible = false;
            btnAcceptQuote.Visible = false;
        }

    }

    private void FillTextControl()
    {
        try
        {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
            LblControlNo.Text = taskControl.TaskControlID.ToString().Trim();

            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
            //Check Marks
            if (taskControl.IsCommercial == true)
                chkIsCommercial.Checked = true;
            else
                chkIsCommercial.Checked = false;

            if (taskControl.RenewalOfYacht == true && taskControl.IsEndorsement != true) 
            {
                chkIsRenew.Checked = true;
                lblBank.Visible = true;
                ddlBank.Visible = true;
                lblBankListSelected2.Visible = true;
                if (taskControl.isQuote == true)
                {
                    DivRenew.Visible = true;
                    lblPolicyNoToRenew.Visible = true;
                    txtPolicyNoToRenew.Visible = true;
                    txtPolicyNoToRenew.Enabled = true;
                    btnVerifyYachtInPPS.Visible = true;
                    btnVerifyYachtInPPS.Enabled = true;
                    txtPolicyToRenewType.Visible = true;
                    txtPolicyNoToRenewSuffix.Visible = true;
                    txtPolicyNoToRenewSuffix.Enabled = true;
                    String[] arrayPolicytoRenew;
                    arrayPolicytoRenew = taskControl.PolicyToRenew.Trim().Replace("MAR","").Split('-');
                    txtPolicyNoToRenew.Text = arrayPolicytoRenew[0];
                    txtPolicyNoToRenewSuffix.Text = arrayPolicytoRenew[1];
                }
            }
            else
            {
                chkIsRenew.Checked = false;
                ddlBank.SelectedIndex = 0;
                lblBank.Visible = true;
                ddlBank.Visible = false;
                lblBankListSelected2.Visible = false;
                btnBankList.Enabled = false;
                lblBankListSelected2.Text = GetBankListInfo(ddlBank.SelectedItem.Value);
                txtEffectiveDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                txtExpirationDate.Text = DateTime.Now.AddYears(1).ToString("MM/dd/yyyy");
            }
            //DROPDOWNS
            ddlHomeport.SelectedIndex = ddlHomeport.Items.IndexOf(
                    ddlHomeport.Items.FindByValue(taskControl.HomeportID.ToString()));
            ddlNavigationLimit.SelectedIndex = taskControl.NavigationLimitID;
            if (taskControl.isQuote == true)
            {
                ddlDeductibles1.SelectedIndex = ddlDeductibles1.Items.IndexOf(
                    ddlDeductibles1.Items.FindByValue(taskControl.DeductibleID1.ToString())); 
                ddlDeductibles2.SelectedIndex = ddlDeductibles2.Items.IndexOf(
                    ddlDeductibles2.Items.FindByValue(taskControl.DeductibleID2.ToString()));
            }
            else
            {
                ddlDeductibles1.SelectedIndex = ddlDeductibles1.Items.IndexOf(
                   ddlDeductibles1.Items.FindByValue(taskControl.DeductibleIDPoliza.ToString()));
                txtWatercraftLimit1.Text = taskControl.WatercraftLimitPoliza;
                txtRate1.Text = taskControl.RatePoliza;
                txtWatercraftLimitTotal1.Text = taskControl.WatercraftLimitTotalPoliza;
                lblOption1.Visible = false;
                lblOption2.Visible = false;
                lblDeductibles2.Visible = false;
                ddlDeductibles2.Visible = false;
                ddlDeductibles2.Enabled = false;
                lblDeductibleAmount2.Visible = false;
                lblDeductibleCalculated2.Visible = false;
                lblWatercraftLimit2.Visible = false;
                txtWatercraftLimit2.Visible = false;
                txtWatercraftLimit2.Enabled = false;
                lblRate2.Visible = false;
                txtRate2.Visible = false;
                txtRate2.Enabled = false;
                lblWatercraftLimitTotal2.Visible = false;
                txtWatercraftLimitTotal2.Visible = false;
                lblTotalPremiumPoliza.Visible = true;
                txtTotalPremiumPoliza.Visible = true;
                txtTotalPremiumPoliza.Text = currencyFormat(taskControl.TotalPremiumPoliza.ToString());
                lblTotalPremiumOpcion1.Visible = false;
                lblTotalPremium.Visible = false;
                txtTotalPremium.Visible = false;
                lblTotalPremiumOpcion2.Visible = false;
                lblTotalPremium2.Visible = false;
                txtTotalPremium2.Visible = false;
                if (taskControl.PolicyNo == "")
                {
                    txtPolicyNo.Text = "";
                    txtSuffix.Text = "";
                }
                else
                {
                    txtPolicyNo.Text = taskControl.PolicyNo;
                    txtSuffix.Text = taskControl.Suffix;
                }

            }
            ddlPI.SelectedIndex = taskControl.PIID;
            ddlPILiabilityOnly.SelectedIndex = taskControl.PILiabilityOnlyID;
            ddlMedicalPayment.SelectedIndex = taskControl.MedicalPaymentID;
            if (taskControl.UninsuredBoaterID != 0)
                ddlUninsuredBoaters.SelectedIndex = taskControl.UninsuredBoaterID;
            else if (taskControl.UninsuredBoaterID == 0 && taskControl.OtherUninsuredBoater == "")
                ddlUninsuredBoaters.SelectedIndex = 1;
            else
                ddlUninsuredBoaters.SelectedIndex = 0;


            //POLICY DETAILS
            txtEntryDate.Text = taskControl.EntryDate.ToShortDateString();

            if (taskControl.EffectiveDate.Trim() != "")
                txtEffectiveDate.Text = Convert.ToDateTime(taskControl.EffectiveDate.Trim()).ToString("MM/dd/yyyy");

            if (taskControl.ExpirationDate.Trim() != "")
                txtExpirationDate.Text = Convert.ToDateTime(taskControl.ExpirationDate.Trim()).ToString("MM/dd/yyyy");

            txtTerm.Text = taskControl.Term.ToString();

            if (taskControl.InsuranceCompany != "")
                ddlInsuranceCompany.SelectedIndex = ddlInsuranceCompany.Items.IndexOf(
                    ddlInsuranceCompany.Items.FindByValue(taskControl.InsuranceCompany.ToString()));

            //Agent
            //if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("AUTO VI ADMINISTRATOR"))
            //{
            //    if (taskControl.Agent.Trim() != "000" && taskControl.Agent.Trim() != "")
            //    {
            //        ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
            //            ddlAgent.Items.FindByValue(taskControl.Agent.Trim()));
            //    }

            //    ddlAgent.Enabled = true;

            //}
            //else
            //{
            //    DataTable dtAgentByUserID = GetAgentByUserID(cp.UserID.ToString());
            //    string Agent = "000";

            //    if (dtAgentByUserID.Rows.Count > 0)
            //    {
            //        Agent = dtAgentByUserID.Rows[0]["AgentID"].ToString();
            //    }

            //    if (taskControl.Agent.Trim() != "000" && taskControl.Agent.Trim() != "")
            //    {
            //        ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
            //            ddlAgent.Items.FindByValue(taskControl.Agent.Trim()));
            //    }
            //    else
            //    {
            //        ddlAgent.SelectedIndex = ddlAgent.Items.IndexOf(
            //            ddlAgent.Items.FindByValue(Agent.Trim()));
            //    }

            //    ddlAgent.Enabled = false;
            //}

            //Agency
            if (taskControl.Agent.Trim() != "000" && taskControl.Agent.Trim() != "")
            {
                ddlAgency.SelectedIndex = 0;
                for (int i = 0; ddlAgency.Items.Count - 1 >= i; i++)
                {
                    if (ddlAgency.Items[i].Value.Trim() == taskControl.Agent.Trim())
                    {
                        ddlAgency.SelectedIndex = i;
                        i = ddlAgency.Items.Count - 1;
                    }
                }
                ddlAgency.Enabled = true;
            }
            //if (cp.IsInRole("ADMINISTRATOR") || cp.IsInRole("AUTO VI ADMINISTRATOR"))
            //{
            //    if (taskControl.Agency.Trim() != "000" && taskControl.Agency.Trim() != "")
            //    {
            //        //ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(
            //        //    ddlAgency.Items.FindByValue(taskControl.Agency));

            //        ddlAgency.SelectedIndex = 0;
            //        for (int i = 0; ddlAgency.Items.Count - 1 >= i; i++)
            //        {
            //            if (ddlAgency.Items[i].Value.Trim() == taskControl.Agency.Trim())
            //            {
            //                ddlAgency.SelectedIndex = i;
            //                i = ddlAgency.Items.Count - 1;
            //            }
            //        }

            //    }

            //    ddlAgency.Enabled = true;
            //}
            //else
            //{
            //    DataTable dtAgencyByUserID = GetAgentByUserID(cp.UserID.ToString());
            //    string Agency = "000";

            //    if (dtAgencyByUserID.Rows.Count > 0)
            //    {
            //        Agency = dtAgencyByUserID.Rows[0]["AgentID"].ToString();
            //    }

            //    if (taskControl.Agency.Trim() != "000" && taskControl.Agency.Trim() != "")
            //    {
            //        ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(
            //            ddlAgency.Items.FindByValue(taskControl.Agency.Trim()));
            //    }
            //    else
            //    {
            //        ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(
            //            ddlAgency.Items.FindByValue(Agency.Trim()));
            //    }

            //    ddlAgency.Enabled = false;
            //}

            //if (taskControl.Agency != "")
            //    ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(
            //        ddlAgency.Items.FindByValue(taskControl.Agency));

            //INSURED INFORMATION TEXBOXES
            txtCustomerNo.Text = taskControl.Customer.CustomerNo;

            EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();
            if (taskControl.Customer.SocialSecurity.Trim() != "")
            {
                txtSSN.Text = encrypt.Decrypt(taskControl.Customer.SocialSecurity);
                txtSSN.Text = new string('*', txtSSN.Text.Trim().Length - 4) + txtSSN.Text.Trim().Substring(txtSSN.Text.Trim().Length - 4);
                MaskedEditExtender1.Mask = "???-??-9999";
            }
            else
                txtSSN.Text = "";

            txtFirstName.Text = taskControl.Customer.FirstName;
            txtInitial.Text = taskControl.Customer.Initial;
            txtLastName.Text = taskControl.Customer.LastName1;
            txtCompanyName.Text = taskControl.Customer.LastName2;
            txtHomePhone.Text = taskControl.Customer.HomePhone;
            txtWorkPhone.Text = taskControl.Customer.JobPhone;
            txtCellular.Text = taskControl.Customer.Cellular;
            txtemail.Text = taskControl.Customer.Email;
            txtAddrs1.Text = taskControl.Customer.Address1;
            txtAddrs2.Text = taskControl.Customer.Address2;
            txtZip.Text = taskControl.Customer.ZipCode;
            txtCiudad.Text = taskControl.Customer.City;
            txtState.Text = taskControl.Customer.State;
            txtPhyAddress.Text = taskControl.Customer.AddressPhysical1;
            txtPhyAddress2.Text = taskControl.Customer.AddressPhysical2;
            txtPhyZipCode.Text = taskControl.Customer.ZipPhysical;
            txtPhyCity.Text = taskControl.Customer.CityPhysical;
            txtPhyState.Text = taskControl.Customer.StatePhysical;

            txtProducer.Text = taskControl.Producer;
            txtHomeportAddress.Text = taskControl.HomeportAddress;

            //Yacht INFORMATION TEXTBOXES
            txtBoatName.Text = taskControl.BoatName;
            txtHullLimit.Text = taskControl.HullLimit;
            txtBoatYear.Text = taskControl.BoatYear;
            txtLOA.Text = taskControl.Loa;
            txtBoatModel.Text = taskControl.BoatModel;
            txtBoatBuilder.Text = taskControl.BoatBuilder;
            txtHullNumber.Text = taskControl.HullNumberRegistration;
            txtHomeportLocation.Text = taskControl.HomeportLocation;
            //Yacht INFORMATION GRIDVIEW
            //if (taskControl.isQuote == true && taskControl.TaskControlID == 0)
            //{
            //    AddDefaultNavigationLimit();
            //}
            //else
            //{
            //    FillNavigationLimitGrid();
            //}
            FillNavigationLimitGrid();
            FillTenderLimitGrid();

            //Yacht COVERAGE AND LIMIT TEXTBOXES

            if (taskControl.isQuote == true)
            {
                txtWatercraftLimit1.Text = taskControl.WatercraftLimit1;
                txtRate1.Text = taskControl.Rate1;
                txtWatercraftLimitTotal1.Text = taskControl.WatercraftLimitTotal1;
                txtWatercraftLimit2.Text = taskControl.WatercraftLimit2;
                txtRate2.Text = taskControl.Rate2;
                txtWatercraftLimitTotal2.Text = taskControl.WatercraftLimitTotal2;
                txtTotalPremium.Text = currencyFormat(taskControl.TotalPremium1.ToString());
                txtTotalPremium2.Text = currencyFormat(taskControl.TotalPremium2.ToString());
                if (taskControl.isAcceptQuote == true)
                {
                    btnPreviewPolicy.Visible = true;
                }
            }
            txtOtherPI.Text = taskControl.OtherPI;
            txtOtherMedicalPayment.Text = taskControl.OtherMedicalPayment;
            if (taskControl.PersonalEffects.Trim() != "$1,000" && taskControl.PersonalEffects.Trim() != "")
                txtPersonalEffect.Text = taskControl.PersonalEffects;
            else
                txtPersonalEffect.Text = "$1,000";
            if (taskControl.PEDeductible.Trim() != "$250" && taskControl.PEDeductible.Trim() != "")
                txtPersonalEffectDeductible.Text = taskControl.PEDeductible;
            else
                txtPersonalEffectDeductible.Text = "$250";
            if (taskControl.PersonalEffectsPremium.Trim() != "$0" && taskControl.PersonalEffectsPremium.Trim() != "")
                txtPersonalEffectPremium.Text = taskControl.PersonalEffectsPremium;
            else
                txtPersonalEffectPremium.Text = "$0";
            txtTrailer.Text = taskControl.Trailer;
            txtTrailerPremium.Text = taskControl.TrailerPremium;
            if (taskControl.OtherUninsuredBoaterPremium != "")
                txtOtherUninsuredBoaterPremium.Text = taskControl.OtherUninsuredBoaterPremium;
            else
                txtOtherUninsuredBoaterPremium.Text = "$0";
            txtOtherUninsuredBoater.Text = taskControl.OtherUninsuredBoater;
            txtTripTransit.Text = taskControl.TripTransit;
            txtTripTransitNotes.Text = taskControl.TripTransitNotes;
            txtMiscellaneous.Text = taskControl.Miscellaneous;
            txtMiscellaneousNotes.Text = taskControl.MiscellaneousNotes;
            txtSubjectivityNote.Text = taskControl.SubjectivityNotes;
            txtEngine.Text = taskControl.Engine;
            txtEngineSerialNumber.Text = taskControl.EngineSerialNumber;
            txtTrailerModel.Text = taskControl.TrailerModel;
            txtTrailerSerial.Text = taskControl.TrailerSerial;
            lblBank.Visible = true;
            ddlBank.Visible = true;
            if (taskControl.BankPPSID.Trim() != "000" && taskControl.BankPPSID != "")
            {
                ddlBank.SelectedIndex = ddlBank.Items.IndexOf(ddlBank.Items.FindByValue(taskControl.BankPPSID));
                lblBankListSelected2.Text = GetBankListInfo(ddlBank.SelectedItem.Value);
            }


            //Yacht Premium Info Textboxes

            txtPIPremium.Text = taskControl.PIPremium;
            txtMedicalPaymentPremiumTotal.Text = taskControl.MedicalPaymentPremiumTotal;

            //Yacht COVERAGE AND LIMIT GRIDVIEW
            FillSurveyGrid();

            calculatelblDeductibleCalculated1();
            calculatelblDeductibleCalculated2();

            if (taskControl.isQuote == false)
            {
                //btnPrintPolicy.Enabled = true;
                //btnPrintPolicy.Visible = true;
                //btnPrintCertificate.Enabled = true;
                //btnPrintCertificate.Visible = true;
                ddlPrintOptions.Visible = true;
                ddlPrintOptions.Enabled = true;
                btnEndor.Visible = true;
                btnEndor.Enabled = true;
                btnPremiumFinance2.Visible = true;
                btnFM2.Enabled = true;
                btnFM5.Enabled = true;
                btnFM8.Enabled = true;
                btnModify2.Visible = true;
                //if (ddlHomeport.SelectedItem.Value == "14" || ddlHomeport.SelectedItem.Value == "20" || ddlHomeport.SelectedItem.Text == "" || ddlHomeport.SelectedItem.Value == "11")
                //{
                //    ddlPrintOptions.Items.Remove(ddlPrintOptions.Items.FindByValue("PRINT PORT ENDORSEMENT"));
                //}
                if (ddlHomeport.SelectedItem.Text == "")
                {
                    ddlPrintOptions.Items.Remove(ddlPrintOptions.Items.FindByValue("PRINT PORT CERTIFICATE"));
                    ddlPrintOptions.Items.Remove(ddlPrintOptions.Items.FindByValue("PRINT PORT ENDORSEMENT"));
                    ddlPrintOptions.Items.Remove(ddlPrintOptions.Items.FindByValue("PRINT PORT CERT ADD INS"));

                }

                if (taskControl.PreviousPolicy.Trim() != "")
                {
                    lblRenewalNumber.Visible = true;
                    txtRenewalType.Visible = true;
                    txtPolicyNoRewnewal.Visible = true;
                    txtSuffixRenewal.Visible = true;
                    String[] arrayPolicyNoRenewal;
                    arrayPolicyNoRenewal = taskControl.PreviousPolicy.Trim().Replace("MAR", "").Split('-');
                    txtPolicyNoRewnewal.Text = arrayPolicyNoRenewal[0];
                    txtSuffixRenewal.Text = arrayPolicyNoRenewal[1];
                }

                lblBank.Visible = true;
                ddlBank.Visible = true;

                if (taskControl.BankPPSID.Trim() != "000" && taskControl.BankPPSID.Trim() != "")
                {
                    ddlBank.SelectedIndex = ddlBank.Items.IndexOf(ddlBank.Items.FindByValue(taskControl.BankPPSID));
                    lblBankListSelected2.Text = GetBankListInfo(ddlBank.SelectedItem.Value);
                    lblBankListSelected2.Visible = true;
                }
                else
                {
                    ddlPrintOptions.Items.Remove(ddlPrintOptions.Items.FindByValue("PRINT BANK CERTIFICATE"));
                }

                FillDataGrid();

            }

        }
        catch (Exception exp)
        {
            lblRecHeader.Text = exp.Message;
            mpeSeleccion.Show();
        }

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

    private double formatPremium(string totalPremium)
    {
        double total = 0.0;
        string totalP = "";


        if (totalPremium == "")
            return 0.0;
        else
        {
            totalP = totalPremium.Replace("$", "").Replace(",", "");
            total = double.Parse(totalP);
            return total;
        }
    }

    private void FillProperties(bool isQuote)
    {
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
        int userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

        try
        {

            //Esto es por ahora en lo que a-ado agent a la pagina CAMBIAR
            taskControl.AgentDesc = "";
            taskControl.Agency = "000";
            taskControl.InsuranceCompany = "000";
            taskControl.Bank = "000";
            taskControl.CompanyDealer = "000";


            if (txtTerm.Text.Trim() != "")
                taskControl.Term = int.Parse(txtTerm.Text);

            if (ddlInsuranceCompany.SelectedIndex > 0 && ddlInsuranceCompany.SelectedItem != null)
                taskControl.InsuranceCompany = ddlInsuranceCompany.SelectedItem.Value;
            else
                taskControl.InsuranceCompany = "000";


            taskControl.EffectiveDate = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(this.txtEffectiveDate.Text).ToShortDateString());


            if (this.txtEffectiveDate.Text.Trim() != string.Empty && this.txtTerm.Text.Trim() != string.Empty)
                this.txtExpirationDate.Text = String.Format("{0:MM/dd/yyyy}", DateTime.Parse(this.txtEffectiveDate.Text).AddMonths(int.Parse(this.txtTerm.Text.Trim())).ToShortDateString());

            taskControl.ExpirationDate = txtExpirationDate.Text;
            taskControl.EntryDate = DateTime.Parse(txtEntryDate.Text.Trim());

            //Agent
            //if (ddlAgent.SelectedIndex > 0 && ddlAgent.SelectedItem != null)
            //{
            //    taskControl.Agent = ddlAgent.SelectedItem.Value;
            //    taskControl.AgentDesc = ddlAgent.SelectedItem.Text.Trim();
            //}
            //else
            //{
            //    taskControl.Agent = "000";
            //    taskControl.AgentDesc = "";
            //}
            taskControl.Agency = "000";


            if (ddlAgency.SelectedIndex > 0 && ddlAgency.SelectedItem != null)
            {
                taskControl.Agent = ddlAgency.SelectedItem.Value.Trim();
                taskControl.AgentDesc = ddlAgency.SelectedItem.Text;
            }
            else
            {
                taskControl.Agent = "000";
                taskControl.AgentDesc = "";
            }

            //if (taskControl.Mode == 1)
            //{
            taskControl.EnteredBy = cp.Identity.Name.Split("|".ToCharArray())[0];
            //}

            if (txtPolicyNoToRenew.Text.Trim() != "")
            {
                taskControl.PreviousPolicy = txtPolicyToRenewType.Text.Trim() + txtPolicyNoToRenew.Text.Trim() + "-" + txtPolicyNoToRenewSuffix.Text.Trim();
                taskControl.PolicyToRenew = txtPolicyToRenewType.Text.Trim() + txtPolicyNoToRenew.Text.Trim() + "-" + txtPolicyNoToRenewSuffix.Text.Trim();
            }
            else
            {
                taskControl.PreviousPolicy = "";
                taskControl.PolicyToRenew = "";
            }

            taskControl.Customer.FirstName = txtFirstName.Text.Trim();
            taskControl.Customer.Initial = txtInitial.Text.Trim();
            taskControl.Customer.LastName1 = txtLastName.Text.Trim();
            taskControl.Customer.LastName2 = txtCompanyName.Text.Trim();
            taskControl.Customer.HomePhone = txtHomePhone.Text.Trim();
            taskControl.Customer.JobPhone = txtWorkPhone.Text.Trim();
            taskControl.Customer.Cellular = txtCellular.Text.Trim();
            taskControl.Customer.Email = txtemail.Text.Trim();
            taskControl.Customer.Address1 = txtAddrs1.Text.Trim();
            taskControl.Customer.Address2 = txtAddrs2.Text.Trim();
            taskControl.Customer.ZipCode = txtZip.Text.Trim();
            taskControl.Customer.City = txtCiudad.Text.Trim();
            taskControl.Customer.State = txtState.Text.Trim();
            taskControl.Customer.AddressPhysical1 = txtPhyAddress.Text.Trim();
            taskControl.Customer.AddressPhysical2 = txtPhyAddress2.Text.Trim();
            taskControl.Customer.ZipPhysical = txtPhyZipCode.Text.Trim();
            taskControl.Customer.CityPhysical = txtPhyCity.Text.Trim();
            taskControl.Customer.StatePhysical = txtPhyState.Text.Trim();
            EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();
            if(cp.IsInRole("MODIFY SOCIAL SECURITY") || taskControl.TaskControlID == 0)
            {
                if (txtSSN.Text.Replace("_","").Replace("-","").Replace("*","").Trim() != "")
                    taskControl.Customer.SocialSecurity = encrypt.Encrypt(txtSSN.Text.Trim().ToUpper());
                else 
                    taskControl.Customer.SocialSecurity = "";
            }
             //else 
             //   taskControl.Customer.SocialSecurity = "";

            taskControl.isQuote = isQuote;
            taskControl.Producer = txtProducer.Text;
            taskControl.HomeportAddress = txtHomeportAddress.Text;
            taskControl.BoatName = txtBoatName.Text.Trim();
            taskControl.HullLimit = txtHullLimit.Text.Trim();
            taskControl.HomeportLocation = txtHomeportLocation.Text.Trim();
            taskControl.BoatYear = txtBoatYear.Text.Trim();
            taskControl.Loa = txtLOA.Text.Trim();
            taskControl.BoatModel = txtBoatModel.Text.Trim();
            taskControl.BoatBuilder = txtBoatBuilder.Text.Trim();
            taskControl.HullNumberRegistration = txtHullNumber.Text.Trim();
            taskControl.WatercraftLimit1 = txtWatercraftLimit1.Text.Trim();
            taskControl.WatercraftLimit2 = txtWatercraftLimit2.Text.Trim();
            taskControl.Rate1 = txtRate1.Text.Trim();
            taskControl.Rate2 = txtRate2.Text.Trim();
            taskControl.WatercraftLimitTotal1 = txtWatercraftLimitTotal1.Text.Trim();
            taskControl.WatercraftLimitTotal2 = txtWatercraftLimitTotal2.Text.Trim();
            taskControl.OtherPI = txtOtherPI.Text.Trim();
            taskControl.PIPremium = txtPIPremium.Text.Trim();
            taskControl.OtherMedicalPayment = txtOtherMedicalPayment.Text.Trim();
            taskControl.MedicalPaymentPremiumTotal = txtMedicalPaymentPremiumTotal.Text.Trim();
            taskControl.PersonalEffects = txtPersonalEffect.Text.Trim();
            taskControl.PEDeductible = txtPersonalEffectDeductible.Text.Trim();
            taskControl.PersonalEffectsPremium = txtPersonalEffectPremium.Text.Trim();
            taskControl.Trailer = txtTrailer.Text.Trim();
            taskControl.TrailerPremium = txtTrailerPremium.Text.Trim();
            taskControl.OtherUninsuredBoaterPremium = txtOtherUninsuredBoaterPremium.Text.Trim();
            taskControl.OtherUninsuredBoater = txtOtherUninsuredBoater.Text.Trim();
            taskControl.TripTransit = txtTripTransit.Text.Trim();
            taskControl.TripTransitNotes = txtTripTransitNotes.Text.Trim();
            if (isQuote == true && Session["AUTOEndorsement"] == null) 
            {
                taskControl.TotalPremium1 = formatPremium(txtTotalPremium.Text);
                taskControl.TotalPremium2 = formatPremium(txtTotalPremium2.Text);
            }
            else if (isQuote == true && Session["AUTOEndorsement"] != null)
            {
                taskControl.TotalPremium1 = formatPremium(txtTotalPremium.Text);
                taskControl.TotalPremium2 = formatPremium(txtTotalPremium2.Text);
                taskControl.TotalPremium = formatPremium(txtTotalPremium.Text);
            }
            taskControl.Miscellaneous = txtMiscellaneous.Text.Trim();
            taskControl.MiscellaneousNotes = txtMiscellaneousNotes.Text.Trim();
            taskControl.SubjectivityNotes = txtSubjectivityNote.Text.Trim();
            taskControl.OriginatedAt = EPolicy.Login.Login.GetLocationByUserID(userID);
            if (ddlBank.SelectedItem.Value.ToString() == "")
                taskControl.BankPPSID = "000";
            else
                taskControl.BankPPSID = ddlBank.SelectedItem.Value.ToString();
            taskControl.Engine = txtEngine.Text.Trim();
            taskControl.EngineSerialNumber = txtEngineSerialNumber.Text.Trim();
            taskControl.TrailerModel = txtTrailerModel.Text.Trim();
            taskControl.TrailerSerial = txtTrailerSerial.Text.Trim();

            if (chkIsRenew.Checked)
                taskControl.RenewalOfYacht = true;
            else
                taskControl.RenewalOfYacht = false;

            if (chkIsCommercial.Checked)
                taskControl.IsCommercial = true;
            else
                taskControl.IsCommercial = false;

            if (ddlHomeport.SelectedItem.Value.ToString() == "")
                taskControl.HomeportID = 0;
            else
                taskControl.HomeportID = int.Parse(ddlHomeport.SelectedItem.Value.ToString());

            if (ddlNavigationLimit.SelectedItem.Value.ToString() == "")
                taskControl.NavigationLimitID = 0;
            else
                taskControl.NavigationLimitID = int.Parse(ddlNavigationLimit.SelectedItem.Value.ToString());

            if (isQuote == true)
            {
                if (ddlDeductibles1.SelectedItem.Value.ToString() == "")
                {
                    taskControl.DeductibleID1 = 0;
                    taskControl.HullDeductibleOption1 = "";
                }
                else
                {
                    taskControl.DeductibleID1 = int.Parse(ddlDeductibles1.SelectedItem.Value.ToString());
                    if (txtHullLimit.Text != "")
                    {
                        double deductible1 = 0.0;
                        double deductible2 = 0.0;
                        double hullLimit = 0.0;
                        double total1 = 0.0;
                        double total2 = 0.0;
                        if (ddlDeductibles1.SelectedIndex != 0)
                        {
                            DataTable dt = GetDeductibleAmount(int.Parse(ddlDeductibles1.SelectedItem.Value));
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                                {
                                    deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                    hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                                    total1 = deductible1 * hullLimit;

                                    taskControl.HullDeductibleOption1 = total1.ToString("c0");
                                }
                                else
                                {
                                    deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                    deductible2 = double.Parse(dt.Rows[0]["DeductibleAmount2"].ToString().Trim());
                                    hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                                    total1 = deductible1 * hullLimit;
                                    total2 = deductible2 * hullLimit;

                                    taskControl.HullDeductibleOption1 = total1.ToString("c0") + " / " + total2.ToString("c0");
                                }
                            }
                        }
                    }
                }

                if (ddlDeductibles2.SelectedItem.Value.ToString() == "")
                {
                    taskControl.DeductibleID2 = 0;
                    taskControl.HullDeductibleOption2 = "";
                }
                else
                {
                    taskControl.DeductibleID2 = int.Parse(ddlDeductibles2.SelectedItem.Value.ToString());
                    if (txtHullLimit.Text != "")
                    {
                        double deductible1 = 0.0;
                        double deductible2 = 0.0;
                        double hullLimit = 0.0;
                        double total1 = 0.0;
                        double total2 = 0.0;
                        if (ddlDeductibles2.SelectedIndex != 0)
                        {
                            DataTable dt = GetDeductibleAmount(int.Parse(ddlDeductibles2.SelectedItem.Value));
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                                {
                                    deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                    hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                                    total1 = deductible1 * hullLimit;

                                    taskControl.HullDeductibleOption2 = total1.ToString("c0");
                                }
                                else
                                {
                                    deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                    deductible2 = double.Parse(dt.Rows[0]["DeductibleAmount2"].ToString().Trim());
                                    hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                                    total1 = deductible1 * hullLimit;
                                    total2 = deductible2 * hullLimit;

                                    taskControl.HullDeductibleOption2 = total1.ToString("c0") + " / " + total2.ToString("c0");
                                }
                            }
                        }
                    }
                }
            }

            else
            {
                taskControl.DeductibleIDPoliza = taskControl.DeductibleIDPoliza;
                taskControl.WatercraftLimitPoliza = taskControl.WatercraftLimitPoliza;
                taskControl.RatePoliza = taskControl.RatePoliza;
                taskControl.WatercraftLimitTotalPoliza = taskControl.WatercraftLimitTotalPoliza;
            }

            if (ddlPI.SelectedItem.Value.ToString() == "")
                taskControl.PIID = 0;
            else
                taskControl.PIID = int.Parse(ddlPI.SelectedItem.Value.ToString());

            if (ddlPILiabilityOnly.SelectedItem.Value.ToString() == "")
                taskControl.PILiabilityOnlyID = 0;
            else
                taskControl.PILiabilityOnlyID = int.Parse(ddlPILiabilityOnly.SelectedItem.Value.ToString());

            if (ddlMedicalPayment.SelectedItem.Value.ToString() == "")
                taskControl.MedicalPaymentID = 0;
            else
                taskControl.MedicalPaymentID = int.Parse(ddlMedicalPayment.SelectedItem.Value.ToString());

            if (ddlUninsuredBoaters.SelectedItem.Value.ToString() == "")
                taskControl.UninsuredBoaterID = 0;
            else
                taskControl.UninsuredBoaterID = int.Parse(ddlUninsuredBoaters.SelectedItem.Value.ToString());

            if (isQuote == false)
            {
                taskControl.TotalPremiumPoliza = double.Parse(txtTotalPremiumPoliza.Text.Replace("$", "").Replace(",", ""));
                taskControl.PreviousPolicy = taskControl.PreviousPolicy;
            }
            else
            {
                taskControl.TotalPremiumPoliza = 0.0;
            }
            //taskControl.DeductibleIDPoliza = 0;

            Session["TaskControl"] = taskControl;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            mpeSeleccion.Show();
        }
    }

    private void Validation(bool isQuote)
    {
        try
        {

            ArrayList errorMessages = new ArrayList();

            //TEXTBOXES VALIDATION

            if (txtEffectiveDate.Text == "")
            {
                errorMessages.Add("Effective date is missing." + "\r\n");
            }

            if (txtExpirationDate.Text == "")
            {
                errorMessages.Add("Expiration date is missing." + "\r\n");
            }

            if (txtTerm.Text == "")
            {
                errorMessages.Add("Term is missing." + "\r\n");
            }

            if (this.txtFirstName.Text == "" && chkIsCommercial.Checked == false)
            {
                errorMessages.Add("First Name is missing." + "\r\n");
            }
            if (this.txtLastName.Text == "" && chkIsCommercial.Checked == false)
            {
                errorMessages.Add("Last Name is missing." + "\r\n");
            }

            if (txtCompanyName.Text == "" && chkIsCommercial.Checked == true)
            {
                errorMessages.Add("You must enter a company name." + "\r\n");
            }

            if (txtCiudad.Text == "")
            {
                errorMessages.Add("The City is missing." + "\r\n");
            }

            if (txtZip.Text.Trim() == "")
            {
                errorMessages.Add("The Zipcode is missing or wrong." + "\r\n");
            }

            if (txtPhyCity.Text.Trim() == "")
            {
                errorMessages.Add("The Physical City is missing or wrong." + "\r\n");
            }

            if (txtPhyZipCode.Text.Trim() == "")
            {
                errorMessages.Add("The Physical Zipcode is missing or wrong." + "\r\n");
            }

            if (isQuote == true)
            {
                if (this.txtTotalPremium.Text == "")
                {
                    errorMessages.Add("TotalPremium is missing." + "\r\n");
                }

                if (chkIsRenew.Checked && txtPolicyNoToRenew.Text.Trim() == "")
                {
                    errorMessages.Add("Policy number to renew is missing." + "\r\n");
                }
            }
            else
            {
                if (this.txtTotalPremiumPoliza.Text == "")
                {
                    errorMessages.Add("TotalPremium is missing." + "\r\n");
                }
            }

            if (txtWatercraftLimitTotal1.Text.Trim() != "" && ddlDeductibles1.SelectedIndex == 0)
            {
                errorMessages.Add("Please select a deductible." + "\r\n");
            }

            if (txtWatercraftLimitTotal2.Text.Trim() != "" && ddlDeductibles2.SelectedIndex == 0)
            {
                errorMessages.Add("Please select a deductible." + "\r\n");
            }

            if (this.txtState.Text == "")
            {
                errorMessages.Add("State is missing." + "\r\n");
            }

            if (this.txtAddrs1.Text == "")
            {
                errorMessages.Add("Address is missing." + "\r\n");
            }

            if (this.txtPhyAddress.Text == "")
            {
                errorMessages.Add("Physical Address is missing." + "\r\n");
            }

            if (this.txtBoatName.Text == "")
            {
                errorMessages.Add("Boat name is missing." + "\r\n");
            }

            if (this.txtBoatModel.Text == "")
            {
                errorMessages.Add("Boat model is missing." + "\r\n");
            }

            if (this.txtBoatYear.Text == "")
            {
                errorMessages.Add("Boat year is missing." + "\r\n");
            }

            if (this.txtLOA.Text == "")
            {
                errorMessages.Add("LOA is missing." + "\r\n");
            }

            if (this.txtHullLimit.Text == "" && ddlPILiabilityOnly.SelectedIndex == 0)
            {
                errorMessages.Add("Hull Limit is missing." + "\r\n");
            }

            if (ddlAgency.SelectedIndex == 0)
            {
                errorMessages.Add("You must select an Agency." + "\r\n");
            }

            if (ViewState["navigationData"] == null)
            {
                errorMessages.Add("You must select an Navigation Limit." + "\r\n");
            }
            else
            {
                DataTable dt = (DataTable)ViewState["navigationData"];
                if (dt.Rows.Count == 0)
                {
                    errorMessages.Add("You must select an Navigation Limit." + "\r\n");
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
            lblRecHeader.Text = ex.Message;
            mpeSeleccion.Show();
        }
    }



    protected void chkSameMailing_CheckedChanged(object sender, EventArgs e)
    {
        //If checkbox is checked copy Mailing Address to Physical Address
        if (chkSameMailing.Checked)
        {
            txtPhyAddress.Text = txtAddrs1.Text.Trim();
            txtPhyAddress2.Text = txtAddrs2.Text.Trim();
            txtPhyState.Text = txtState.Text.Trim();
            txtPhyZipCode.Text = txtZip.Text.Trim();
            txtPhyCity.Text = txtCiudad.Text.Trim();
        }

        //If checkbox is not checked set Physical Address flieds to blank
        if (!chkSameMailing.Checked)
        {
            txtPhyAddress.Text = "";
            txtPhyAddress2.Text = "";
            txtPhyZipCode.Text = "";
            txtPhyCity.Text = "";
            txtPhyState.Text = "";
        }
    }

    protected void FillNavigationLimitGrid()
    {
        try
        {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

            gridViewNavigationLimit.DataSource = null;
            DataTable dt = null;

            dt = taskControl.NavigationLimitCollection;

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    gridViewNavigationLimit.DataSource = dt;
                    gridViewNavigationLimit.DataBind();
                    ViewState["navigationData"] = dt;
                }
                else
                {
                    gridViewNavigationLimit.DataSource = null;
                    gridViewNavigationLimit.DataBind();
                }
            }
            else
            {
                gridViewNavigationLimit.DataSource = null;
                gridViewNavigationLimit.DataBind();
            }
        }

        catch (Exception exp)
        {
            lblRecHeader.Text = exp.Message;
            mpeSeleccion.Show();
        }
    }

    //protected void AddDefaultNavigationLimit()
    //{
    //    DataTable dt = new DataTable();
    //    // Initialize data table if viewstate is null

    //    dt.Columns.Add("No", typeof(int));
    //    dt.Columns.Add("NavigationLimitID", typeof(int));
    //    dt.Columns.Add("TaskControlID", typeof(int));
    //    dt.Columns.Add("NavigationLimitDesc", typeof(string));
    //    dt.Columns[0].AutoIncrement = true;    // Autogenerate serial key column for example
    //    dt.Columns[0].AutoIncrementSeed = 1;
    //    DataRow dr = dt.NewRow();
    //    dr["NavigationLimitID"] = 1;
    //    dr["TaskControlID"] = 0;
    //    dr["NavigationLimitDesc"] = "PR, USVI, BVI";
    //    dt.Rows.Add(dr);
    //    // Bind your gridview
    //    gridViewNavigationLimit.DataSource = dt;
    //    gridViewNavigationLimit.DataBind();
    //    // Save datatable to ViewState
    //    ViewState["navigationData"] = dt;
    //    ddlNavigationLimit.SelectedIndex = 1;
    //}

    protected void btnAddNavigationLimit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlNavigationLimit.SelectedItem.Text == "")
            {
                throw new Exception("You must select a navigation limit first.");
            }

            if (btnAddNavigationLimit.Text == "ADD")
            {
                DataTable dt = new DataTable();
                if (ViewState["navigationData"] == null)
                {
                    // Initialize data table if viewstate is null

                    dt.Columns.Add("No", typeof(int));
                    dt.Columns.Add("NavigationLimitID", typeof(int));
                    dt.Columns.Add("TaskControlID", typeof(int));
                    dt.Columns.Add("NavigationLimitDesc", typeof(string));
                    dt.Columns[0].AutoIncrement = true;    // Autogenerate serial key column for example
                    dt.Columns[0].AutoIncrementSeed = 1;
                }
                else
                    dt = (DataTable)ViewState["navigationData"];  // Grab datatable from viewstate if its not null

                // Add your data row
                DataRow dr = dt.NewRow();
                dr["NavigationLimitID"] = int.Parse(ddlNavigationLimit.SelectedItem.Value.ToString());
                dr["TaskControlID"] = 0;
                dr["NavigationLimitDesc"] = ddlNavigationLimit.SelectedItem.Text;
                dt.Rows.Add(dr);
                // Bind your gridview
                gridViewNavigationLimit.DataSource = dt;
                gridViewNavigationLimit.DataBind();
                // Save datatable to ViewState
                ViewState["navigationData"] = dt;
            }
            if (btnAddNavigationLimit.Text == "UPDATE")
            {
                DataTable dt = new DataTable();
                if (ViewState["navigationData"] != null)
                {
                    dt = (DataTable)ViewState["navigationData"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Session["gridViewNavigationLimitSelectedIndex"].ToString().Trim() == dt.Rows[i]["No"].ToString().Trim())
                        {
                            dt.Rows[i]["NavigationLimitID"] = int.Parse(ddlNavigationLimit.SelectedItem.Value.ToString());
                            dt.Rows[i]["NavigationLimitDesc"] = ddlNavigationLimit.SelectedItem.Text;
                        }
                    }
                }

                btnAddNavigationLimit.Text = "Add";
                gridViewNavigationLimit.DataSource = dt;
                gridViewNavigationLimit.DataBind();
                ViewState["navigationData"] = dt;
            }
        }
        catch (Exception ex)
        {

            lblRecHeader.Text = ex.Message;
            mpeSeleccion.Show();
        }
    }

    protected void gridViewNavigationLimit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Visible = false;
        }
    }

    protected void gridViewNavigationLimit_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        DataTable dt = new DataTable();
        if (e.CommandName == "Modify")
        {
            if (ViewState["navigationData"] != null)
            {
                //Retrieve the table from the session object.
                dt = (DataTable)ViewState["navigationData"];
                int rowIndex = Int32.Parse(e.CommandArgument.ToString());
                GridViewRow row1 = gridViewNavigationLimit.Rows[rowIndex];
                Session.Add("gridViewNavigationLimitSelectedIndex", row1.Cells[0].Text);
                ddlNavigationLimit.SelectedIndex = rowIndex;
                btnAddNavigationLimit.Text = "UPDATE";
            }
        }
    }

    protected void gridViewNavigationLimit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = new DataTable();
        if (ViewState["navigationData"] != null)
        {
            dt = (DataTable)ViewState["navigationData"];
            int currentRowDeleting = e.RowIndex;
            dt.Rows.RemoveAt(e.RowIndex);

            //These instructions set the number of row correctly in the No. Column of the GridView
            if (currentRowDeleting == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["No"] = i + 1;
                }
            }
            else
            {
                for (int i = currentRowDeleting; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["No"] = i + 1;
                }
            }
            gridViewNavigationLimit.DataSource = dt;
            gridViewNavigationLimit.DataBind();
            ViewState["navigationData"] = dt;
        }
    }



    protected void FillTenderLimitGrid()
    {
        try
        {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

            gridViewTenderLimit.DataSource = null;
            DataTable dt = null;

            dt = taskControl.TenderLimitCollection;

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    gridViewTenderLimit.DataSource = dt;
                    gridViewTenderLimit.DataBind();
                    ViewState["tenderData"] = dt;
                }
                else
                {
                    gridViewTenderLimit.DataSource = null;
                    gridViewTenderLimit.DataBind();
                }
            }
            else
            {
                gridViewTenderLimit.DataSource = null;
                gridViewTenderLimit.DataBind();
            }
        }

        catch (Exception exp)
        {
            lblRecHeader.Text = exp.Message;
            mpeSeleccion.Show();
        }
    }

    protected void btnAddRows_Click(object sender, EventArgs e)
    {
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        try
        {
            if (txtTenderLimit.Text == "")
            {
                throw new Exception("You must write a tender limit first.");
            }

            else
            {
                txtTenderLimit.Text = currencyFormat(txtTenderLimit.Text);
            }

            if (btnAddRows.Text == "ADD")
            {
                DataTable dt = new DataTable();
                if (ViewState["tenderData"] == null)
                {
                    // Initialize data table if viewstate is null

                    dt.Columns.Add("No", typeof(int));
                    dt.Columns.Add("TaskControlID", typeof(int));
                    dt.Columns.Add("TenderLimitAmount", typeof(string));
                    dt.Columns.Add("TenderDesc", typeof(string));
                    dt.Columns.Add("TenderSerial", typeof(string));
                    dt.Columns[0].AutoIncrement = true;    // Autogenerate serial key column for example
                    dt.Columns[0].AutoIncrementSeed = 1;
                }
                else
                    dt = (DataTable)ViewState["tenderData"];  // Grab datatable from viewstate if its not null

                // Add your data row
                DataRow dr = dt.NewRow();
                if (dt.Rows.Count > 0)
                {
                    dr["No"] = int.Parse(dt.Rows[dt.Rows.Count - 1]["No"].ToString()) + 1;
                }
                else
                {
                    dr["No"] = 1;
                }
                dr["TaskControlID"] = 0;
                dr["TenderLimitAmount"] = txtTenderLimit.Text;
                dr["TenderDesc"] = txtTenderDesc.Text;
                dr["TenderSerial"] = txtTenderSerial.Text;
                dt.Rows.Add(dr);
                // Bind your gridview
                gridViewTenderLimit.DataSource = dt;
                gridViewTenderLimit.DataBind();
                // Save datatable to ViewState
                ViewState["tenderData"] = dt;
            }
            if (btnAddRows.Text == "UPDATE")
            {
                DataTable dt = new DataTable();
                if (ViewState["tenderData"] != null)
                {
                    dt = (DataTable)ViewState["tenderData"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Session["gridViewTenderLimitSelectedIndex"].ToString().Trim() == dt.Rows[i]["No"].ToString().Trim())
                        {
                            dt.Rows[i]["TenderLimitAmount"] = txtTenderLimit.Text;
                            dt.Rows[i]["TenderDesc"] = txtTenderDesc.Text;
                            dt.Rows[i]["TenderSerial"] = txtTenderSerial.Text;
                        }
                    }
                }

                btnAddRows.Text = "ADD";
                if (taskControl.isQuote == false)
                {
                    btnAddRows.Enabled = false;
                }
                gridViewTenderLimit.DataSource = dt;
                gridViewTenderLimit.DataBind();
                ViewState["tenderData"] = dt;
            }

            calculateWatercraftLimit();
            if (txtRate1.Text != "")
            {
                calculateWatercraftLimitPremium1();
                calculateTotalPremium1();
            }
            else
            {
                txtRate1.Text = "";
                txtWatercraftLimitTotal1.Text = "";
            }

            if (txtRate2.Text != "")
            {
                calculateWatercraftLimitPremium2();
                calculateTotalPremium2();
            }
            else
            {
                txtRate2.Text = "";
                txtWatercraftLimitTotal2.Text = "";
            }
            if (ddlDeductibles1.SelectedIndex == -1)
                lblDeductibleCalculated1.Text = "None";
            else
                calculatelblDeductibleCalculated1();

            if (ddlDeductibles2.SelectedIndex == -1)
                lblDeductibleCalculated2.Text = "None";
            else
                calculatelblDeductibleCalculated2();

        }
        catch (Exception ex)
        {

            lblRecHeader.Text = ex.Message;
            mpeSeleccion.Show();
        }
    }

    protected void gridViewTenderLimit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }
    }

    protected void gridViewTenderLimit_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        DataTable dt = new DataTable();
        if (e.CommandName == "Modify")
        {
            if (ViewState["tenderData"] != null)
            {
                //Retrieve the table from the session object.
                dt = (DataTable)ViewState["tenderData"];
                int rowIndex = Int32.Parse(e.CommandArgument.ToString());
                GridViewRow row1 = gridViewTenderLimit.Rows[rowIndex];
                Session.Add("gridViewTenderLimitSelectedIndex", row1.Cells[0].Text);
                txtTenderLimit.Text = row1.Cells[2].Text;
                txtTenderDesc.Text = row1.Cells[3].Text;
                txtTenderSerial.Text = row1.Cells[4].Text;
                btnAddRows.Text = "UPDATE";
                if(taskControl.isQuote == false)
                {
                    btnAddRows.Enabled = true;
                }

            }
        }
    }


    protected void gridViewTenderLimit_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = new DataTable();
        if (ViewState["tenderData"] != null)
        {
            dt = (DataTable)ViewState["tenderData"];
            int currentRowDeleting = e.RowIndex;
            dt.Rows.RemoveAt(e.RowIndex);

            //These instructions set the number of row correctly in the No. Column of the GridView
            if (currentRowDeleting == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["No"] = i + 1;
                }
            }
            else
            {
                for (int i = currentRowDeleting; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["No"] = i + 1;
                }
            }
            gridViewTenderLimit.DataSource = dt;
            gridViewTenderLimit.DataBind();
            ViewState["tenderData"] = dt;
            calculateWatercraftLimit();
            //txtRate1.Text = "";
            //txtRate2.Text = "";
            //txtWatercraftLimitTotal1.Text = "";
            //txtWatercraftLimitTotal2.Text = "";
            calculateWatercraftLimitPremium1();
            calculateWatercraftLimitPremium2();
            calculateTotalPremium1();
            calculateTotalPremium2();
        }
    }

    protected double gridViewTenderLimit_sumTotal()
    {
        //These set of instruction return the sum of all the tender limits in the gridview
        double total = 0.00;
        string rowCell1 = "";
        string rowCell2 = "";

        if (gridViewTenderLimit.Rows.Count == 0)
        {
            return 0.00;
        }
        else
        {
            GridViewRow row = gridViewTenderLimit.Rows[0];
            for (int i = 0; i < gridViewTenderLimit.Rows.Count; i++)
            {
                row = gridViewTenderLimit.Rows[i];
                rowCell1 = row.Cells[2].Text.ToString().Replace("$", "");
                rowCell2 = rowCell1.Replace(",", "");
                total += double.Parse(rowCell2);

            }

            return total;

        }

    }

    protected double gridViewTenderLimit_SpecificRowValue(int rowPosition)
    {
        //These set of instruction return the sum of all the tender limits in the gridview
        double total = 0.00;
        string rowCell1 = "";
        string rowCell2 = "";

        if (gridViewTenderLimit.Rows.Count == 0)
        {
            return 0.00;
        }
        else
        {
            GridViewRow row = gridViewTenderLimit.Rows[rowPosition - 1];
            rowCell1 = row.Cells[2].Text.ToString().Replace("$", "");
            rowCell2 = rowCell1.Replace(",", "");
            total = double.Parse(rowCell2);
            return total;
        }

    }


    protected void FillSurveyGrid()
    {
        try
        {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

            gridViewSurvey.DataSource = null;
            DataTable dt = null;

            dt = taskControl.SurveyCollection;

            if (dt != null)
            {
                if (dt.Rows.Count != 0)
                {
                    gridViewSurvey.DataSource = dt;
                    gridViewSurvey.DataBind();
                    ViewState["surveyData"] = dt;
                    for (int i = 0; i < gridViewSurvey.Rows.Count; i++)
                    {
                        var CheckBoxSelected = gridViewSurvey.Rows[i].FindControl("checkBoxRecomendaciones") as CheckBox;
                        if ((bool)dt.Rows[i]["Recomendaciones"] == true)
                        {
                            CheckBoxSelected.Checked = true;
                        }
                        else
                        {
                            CheckBoxSelected.Checked = false;
                        }
                    }
                }
                else
                {
                    gridViewSurvey.DataSource = null;
                    gridViewSurvey.DataBind();
                }
            }
            else
            {
                gridViewSurvey.DataSource = null;
                gridViewSurvey.DataBind();
            }
        }

        catch (Exception exp)
        {
            lblRecHeader.Text = exp.Message;
            mpeSeleccion.Show();
        }
    }

    protected void btnAddSurvey_Click(object sender, EventArgs e)
    {
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        try
        {
            if (btnAddSurvey.Text == "ADD")
            {
                if (txtSurveyFee.Text != "")
                {
                    txtSurveyFee.Text = currencyFormat(txtSurveyFee.Text);
                }

                DataTable dt = new DataTable();
                if (ViewState["surveyData"] == null)
                {
                    // Initialize data table if viewstate is null

                    dt.Columns.Add("No", typeof(int));
                    dt.Columns.Add("TaskControlID", typeof(int));
                    dt.Columns.Add("SurveyorName", typeof(string));
                    dt.Columns.Add("SurveyFee", typeof(string));
                    dt.Columns.Add("SurveyDate", typeof(string));
                    dt.Columns.Add("SurveyDateCompleted", typeof(string));
                    dt.Columns.Add("Recomendaciones", typeof(bool));
                    dt.Columns[0].AutoIncrement = true;    // Autogenerate serial key column for example
                    dt.Columns[0].AutoIncrementSeed = 1;
                }
                else
                    dt = (DataTable)ViewState["surveyData"];  // Grab datatable from viewstate if its not null

                // Add your data row
                DataRow dr = dt.NewRow();
                dr["TaskControlID"] = 0;
                dr["SurveyorName"] = txtSurveyName.Text;
                dr["SurveyFee"] = txtSurveyFee.Text;
                dr["SurveyDate"] = txtSurveyDate.Text;
                dr["SurveyDateCompleted"] = txtSurveyDateCompleted.Text;
                dr["Recomendaciones"] = false;

                dt.Rows.Add(dr);
                // Bind your gridview
                gridViewSurvey.DataSource = dt;
                gridViewSurvey.DataBind();
                // Save datatable to ViewState
                ViewState["surveyData"] = dt;
                btnAddSurvey.Text = "ADD";
            }

            if (btnAddSurvey.Text == "UPDATE")
            {
                DataTable dt = new DataTable();
                if (ViewState["surveyData"] != null)
                {
                    dt = (DataTable)ViewState["surveyData"];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Session["gridViewSurveySelectedIndex"].ToString().Trim() == dt.Rows[i]["No"].ToString().Trim())
                        {
                            dt.Rows[i]["SurveyorName"] = txtSurveyName.Text;
                            dt.Rows[i]["SurveyFee"] = txtSurveyFee.Text;
                            dt.Rows[i]["SurveyDate"] = txtSurveyDate.Text;
                            dt.Rows[i]["SurveyDateCompleted"] = txtSurveyDateCompleted.Text;
                        }
                    }
                }

                btnAddSurvey.Text = "ADD";
                if (taskControl.isQuote == false)
                {
                    btnAddSurvey.Enabled = false;
                }
                gridViewSurvey.DataSource = dt;
                gridViewSurvey.DataBind();
                ViewState["surveyData"] = dt;
            }
        }
        catch (Exception ex)
        {

            lblRecHeader.Text = ex.Message;
            mpeSeleccion.Show();
        }

    }

    protected void gridViewSurvey_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = new DataTable();
        if (ViewState["surveyData"] != null)
        {
            dt = (DataTable)ViewState["surveyData"];
            int currentRowDeleting = e.RowIndex;
            dt.Rows.RemoveAt(e.RowIndex);

            //These instructions set the number of row correctly in the No. Column of the GridView
            if (currentRowDeleting == 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["No"] = i + 1;
                }
            }
            else
            {
                for (int i = currentRowDeleting; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["No"] = i + 1;
                }
            }
            gridViewSurvey.DataSource = dt;
            gridViewSurvey.DataBind();
            ViewState["surveyData"] = dt;
        }
    }

    protected void gridViewSurvey_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //These instructions make the columns "No." and "TaskControlID" not visible. 1 is for TaskControlID and 0 is for No.
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Visible = false;
        }
    }

    protected void gridViewSurvey_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        DataTable dt = new DataTable();
        if (e.CommandName == "Modify")
        {
            if (ViewState["surveyData"] != null)
            {
                //Retrieve the table from the session object.
                dt = (DataTable)ViewState["surveyData"];
                int rowIndex = Int32.Parse(e.CommandArgument.ToString());
                GridViewRow row1 = gridViewSurvey.Rows[rowIndex];
                Session.Add("gridViewSurveySelectedIndex", row1.Cells[0].Text);
                if (row1.Cells[2].Text.Trim() == "&nbsp;")
                    txtSurveyName.Text = "";
                else
                    txtSurveyName.Text = row1.Cells[2].Text;
                if (row1.Cells[3].Text.Trim() == "&nbsp;")
                    txtSurveyFee.Text = "";
                else
                    txtSurveyFee.Text = row1.Cells[3].Text;
                if (row1.Cells[4].Text.Trim() == "&nbsp;")
                    txtSurveyDate.Text = "";
                else
                    txtSurveyDate.Text = row1.Cells[4].Text;
                if (row1.Cells[5].Text.Trim() == "&nbsp;")
                    txtSurveyDateCompleted.Text = "";
                else
                    txtSurveyDateCompleted.Text = row1.Cells[5].Text;
                btnAddSurvey.Text = "UPDATE";
                if (taskControl.isQuote == false)
                {
                    btnAddSurvey.Enabled = true;
                }


            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            saveYacht(true);
        }
        catch(Exception exp)
        {
            lblRecHeader.Text = exp.Message;
            mpeSeleccion.Show();
        }

    }

    protected void btnModify_Click(object sender, EventArgs e)
    {
        try
        {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            taskControl.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.UPDATE;
            Session.Add("TaskControl", taskControl);
            VerifyPolicyExist();
            EnableControl();
            btnModify.Visible = false;
            btnModify.Enabled = false;
            btnPreview.Visible = false;
            btnPreview.Enabled = false;
            btnPreview2.Visible = false;
            btnPreview2.Enabled = false;
            btnConvert.Enabled = false;
            btnConvert.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            radioBtnTP1.Enabled = false;
            radioBtnTP1.Visible = false;
            radioBtnTP2.Enabled = false;
            radioBtnTP2.Visible = false;
            btnAcceptQuote.Visible = false;
            btnAcceptQuote.Enabled = false;
            txtTrailerPremium.Enabled = false;
            txtEntryDate.Text = DateTime.Now.ToShortDateString();
            if (cp.IsInRole("MODIFY SOCIAL SECURITY"))
            {
                EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();

                if (taskControl.Customer.SocialSecurity.Trim() != "")
                    txtSSN.Text = encrypt.Decrypt(taskControl.Customer.SocialSecurity);
                else
                    txtSSN.Text = "";


                MaskedEditExtender1.Mask = "999-99-9999";
                txtSSN.Enabled = true;
            }
            else if (txtSSN.Text.Replace("_", "").Replace("-", "").Replace("*", "").Trim() != "")
            {
                EncryptClass.EncryptClass encrypt = new EncryptClass.EncryptClass();
                txtSSN.Text = encrypt.Decrypt(taskControl.Customer.SocialSecurity);
                txtSSN.Text = new string('*', txtSSN.Text.Trim().Length - 4) + txtSSN.Text.Trim().Substring(txtSSN.Text.Trim().Length - 4);
                MaskedEditExtender1.Mask = "???-??-9999";
            }
            if (taskControl.RenewalOfYacht == true)
            {
                lblBank.Visible = true;
                ddlBank.Visible = true;
                ddlBank.Enabled = true;
                btnBankList.Enabled = true;
                lblBankListSelected2.Visible = true;
            }

            if (taskControl.isAcceptQuote == true)
            {
                lblBank.Visible = true;
                ddlBank.Visible = true;
                ddlBank.Enabled = true;
                lblBankListSelected2.Visible = true;
                lblEngine.Visible = true;
                lblEngineSerialNumber.Visible = true;
                txtEngine.Enabled = true;
                txtEngine.Visible = true;
                txtEngineSerialNumber.Enabled = true;
                txtEngineSerialNumber.Visible = true;
                btnAcceptQuote.Enabled = false;
                btnAcceptQuote.Visible = false;
                btnPremiumFinance.Visible = false;
                txtTrailerModel.Enabled = true;
                txtTrailerSerial.Enabled = true;
                btnBankList.Enabled = true;
                btnPreviewPolicy.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblRecHeader.Text = ex.Message;
            mpeSeleccion.Show();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

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

            Session.Clear();
            Response.Redirect("Search.aspx");
            
        }


    protected void calculateTotalPremium1()
    {
        double watercraftlimitPremium1 = 0.00;
        double piPremium = 0.00;
        double medicalPaymentsPremium = 0.00;
        double pePremium = 0.00;
        double trailerPremium = 0.00;
        double uninsuredBoaterPremium = 0.00;
        double tripTransitPremium = 0.00;
        double miscellaneousPremium = 0.00;
        double totalPremium = 0.00;

        if (txtWatercraftLimitTotal1.Text != "")
            watercraftlimitPremium1 = double.Parse(txtWatercraftLimitTotal1.Text.Replace("$", "").Replace(",", "").ToString());

        if (txtPIPremium.Text != "")
            piPremium = double.Parse(txtPIPremium.Text.Replace("$", "").Replace(",", "").ToString());

        if (txtMedicalPaymentPremiumTotal.Text != "")
            medicalPaymentsPremium = double.Parse(txtMedicalPaymentPremiumTotal.Text.Replace("$", "").Replace(",", "").ToString());

        if (txtPersonalEffectPremium.Text != "")
            pePremium = double.Parse(txtPersonalEffectPremium.Text.Replace("$", "").Replace(",", "").ToString());

        if (txtTrailerPremium.Text != "")
            trailerPremium = double.Parse(txtTrailerPremium.Text.Replace("$", "").Replace(",", "").ToString());

        if (txtOtherUninsuredBoaterPremium.Text != "")
            uninsuredBoaterPremium = double.Parse(txtOtherUninsuredBoaterPremium.Text.Replace("$", "").Replace(",", "").ToString());

        if (txtTripTransit.Text != "")
            tripTransitPremium = double.Parse(txtTripTransit.Text.Replace("$", "").Replace(",", "").ToString());

        if (txtMiscellaneous.Text != "")
            miscellaneousPremium = double.Parse(txtMiscellaneous.Text.Replace("$", "").Replace(",", "").ToString());

        totalPremium = watercraftlimitPremium1 + piPremium + medicalPaymentsPremium + pePremium + trailerPremium + uninsuredBoaterPremium + tripTransitPremium + miscellaneousPremium;

        txtTotalPremium.Text = currencyFormat(totalPremium.ToString());
    }

    protected void calculateTotalPremium2()
    {
        double watercraftlimitPremium2 = 0.00;
        double piPremium = 0.00;
        double medicalPaymentsPremium = 0.00;
        double pePremium = 0.00;
        double trailerPremium = 0.00;
        double uninsuredBoaterPremium = 0.00;
        double tripTransitPremium = 0.00;
        double miscellaneousPremium = 0.00;
        double totalPremium = 0.00;

        if (txtWatercraftLimitTotal2.Text == "")
            txtTotalPremium2.Text = "$0";

        else
        {
            if (txtWatercraftLimitTotal2.Text != "")
                watercraftlimitPremium2 = double.Parse(txtWatercraftLimitTotal2.Text.Replace("$", "").Replace(",", "").ToString());

            if (txtPIPremium.Text != "")
                piPremium = double.Parse(txtPIPremium.Text.Replace("$", "").Replace(",", "").ToString());

            if (txtMedicalPaymentPremiumTotal.Text != "")
                medicalPaymentsPremium = double.Parse(txtMedicalPaymentPremiumTotal.Text.Replace("$", "").Replace(",", "").ToString());

            if (txtPersonalEffectPremium.Text != "")
                pePremium = double.Parse(txtPersonalEffectPremium.Text.Replace("$", "").Replace(",", "").ToString());

            if (txtTrailerPremium.Text != "")
                trailerPremium = double.Parse(txtTrailerPremium.Text.Replace("$", "").Replace(",", "").ToString());

            if (txtOtherUninsuredBoaterPremium.Text != "")
                uninsuredBoaterPremium = double.Parse(txtOtherUninsuredBoaterPremium.Text.Replace("$", "").Replace(",", "").ToString());

            if (txtTripTransit.Text != "")
                tripTransitPremium = double.Parse(txtTripTransit.Text.Replace("$", "").Replace(",", "").ToString());

            if (txtMiscellaneous.Text != "")
                miscellaneousPremium = double.Parse(txtMiscellaneous.Text.Replace("$", "").Replace(",", "").ToString());

            totalPremium = watercraftlimitPremium2 + piPremium + medicalPaymentsPremium + pePremium + trailerPremium + uninsuredBoaterPremium + tripTransitPremium + miscellaneousPremium;

            txtTotalPremium2.Text = currencyFormat(totalPremium.ToString());
        }

    }

    public string currencyFormat(string tocurrency)
    {
        double number = 0.00;
        string currency = "";

        if (tocurrency.Trim() != "")
        {
            number = double.Parse(tocurrency.Replace("$", "").Replace(",", "").ToString());
            currency = number.ToString("c0");
            //if (currency == "$0")
            //    currency = "";
            return currency;
        }
        else
            return currency;
    }

    public void calculatelblDeductibleCalculated1()
    {
        if (txtHullLimit.Text != "")
        {
            double deductible1 = 0.0;
            double deductible2 = 0.0;
            double hullLimit = 0.0;
            double total1 = 0.0;
            double total2 = 0.0;
            if (ddlDeductibles1.SelectedIndex != 0)
            {
                DataTable dt = GetDeductibleAmount(int.Parse(ddlDeductibles1.SelectedItem.Value));
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                    {
                        deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                        hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                        total1 = deductible1 * hullLimit;

                        lblDeductibleCalculated1.Text = total1.ToString("c0");
                    }
                    else
                    {
                        deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                        deductible2 = double.Parse(dt.Rows[0]["DeductibleAmount2"].ToString().Trim());
                        hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                        total1 = deductible1 * hullLimit;
                        total2 = deductible2 * hullLimit;

                        lblDeductibleCalculated1.Text = total1.ToString("c0") + " / " + total2.ToString("c0");
                    }
                }
                else
                {
                    lblDeductibleCalculated1.Text = "None";
                }
            }
            else
            {
                lblDeductibleCalculated1.Text = "None";
            }
            
        }
        else
            lblDeductibleCalculated1.Text = "None";
    }

    public void ddlDeductibles1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtDeductibles = EPolicy.LookupTables.LookupTables.GetTable("Deductible");

        if (txtWatercraftLimit1.Text != "")
        {
            double deductible1 = 0.0;
            double deductible2 = 0.0;
            double hullLimit = 0.0;
            double total1 = 0.0;
            double total2 = 0.0;
            if (ddlDeductibles1.SelectedIndex != 0)
            {
                DataTable dt = GetDeductibleAmount(int.Parse(ddlDeductibles1.SelectedItem.Value));
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                    {
                        deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                        hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                        total1 = deductible1 * hullLimit;

                        lblDeductibleCalculated1.Text = total1.ToString("c0");
                    }
                    else
                    {
                        deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                        deductible2 = double.Parse(dt.Rows[0]["DeductibleAmount2"].ToString().Trim());
                        hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                        total1 = deductible1 * hullLimit;
                        total2 = deductible2 * hullLimit;

                        lblDeductibleCalculated1.Text = total1.ToString("c0") + " / " + total2.ToString("c0");
                    }
                }
                else
                    lblDeductibleCalculated1.Text = "None";
            }
            else
                lblDeductibleCalculated1.Text = "None";
        }
        else
            lblDeductibleCalculated1.Text = "None";
    }

    public void calculatelblDeductibleCalculated2()
    {
        if (txtHullLimit.Text != "")
        {
            double deductible1 = 0.0;
            double deductible2 = 0.0;
            double hullLimit = 0.0;
            double total1 = 0.0;
            double total2 = 0.0;
            if (ddlDeductibles2.SelectedIndex != 0)
            {
                DataTable dt = GetDeductibleAmount(int.Parse(ddlDeductibles2.SelectedItem.Value));
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                    {
                        deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                        hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                        total1 = deductible1 * hullLimit;

                        lblDeductibleCalculated2.Text = total1.ToString("c0");
                    }
                    else
                    {
                        deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                        deductible2 = double.Parse(dt.Rows[0]["DeductibleAmount2"].ToString().Trim());
                        hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                        total1 = deductible1 * hullLimit;
                        total2 = deductible2 * hullLimit;

                        lblDeductibleCalculated2.Text = total1.ToString("c0") + " / " + total2.ToString("c0");
                    }
                }
                else
                    lblDeductibleCalculated2.Text = "None";
            }
            else
                lblDeductibleCalculated2.Text = "None";
        }
        else
            lblDeductibleCalculated2.Text = "None";
    }

    public void ddlDeductibles2_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtDeductibles = EPolicy.LookupTables.LookupTables.GetTable("Deductible");

        if (txtWatercraftLimit2.Text != "")
        {
            double deductible1 = 0.0;
            double deductible2 = 0.0;
            double hullLimit = 0.0;
            double total1 = 0.0;
            double total2 = 0.0;
            if (ddlDeductibles2.SelectedIndex != 0)
            {
                DataTable dt = GetDeductibleAmount(int.Parse(ddlDeductibles2.SelectedItem.Value));
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                    {
                        deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                        hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                        total1 = deductible1 * hullLimit;

                        lblDeductibleCalculated2.Text = total1.ToString("c0");
                    }
                    else
                    {
                        deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                        deductible2 = double.Parse(dt.Rows[0]["DeductibleAmount2"].ToString().Trim());
                        hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                        total1 = deductible1 * hullLimit;
                        total2 = deductible2 * hullLimit;

                        lblDeductibleCalculated2.Text = total1.ToString("c0") + " / " + total2.ToString("c0");
                    }
                }
                else
                    lblDeductibleCalculated2.Text = "None";
            }
            else
                lblDeductibleCalculated2.Text = "None";
        }
        else
            lblDeductibleCalculated2.Text = "None";
    }

    public void calculatelblDeductibleCalculatedTender()
    {
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        if (gridViewTenderLimit.Rows.Count > 0)
        {
            if (gridViewTenderLimit_SpecificRowValue(1) != 0.0)
            {
                double deductible1 = 0.0;
                double deductible2 = 0.0;
                double tenderLimit = 0.0;
                double total1 = 0.0;
                double total2 = 0.0;
                if (ddlDeductibles1.SelectedIndex != 0)
                {
                    DataTable dt = GetDeductibleAmount(taskControl.DeductibleIDPoliza);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                        {
                            deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                            tenderLimit = gridViewTenderLimit_SpecificRowValue(1);

                            total1 = deductible1 * tenderLimit;
                            if (total1 < 500.0)
                                total1 = 500.0;

                            lblDeductibleCalculatedTender1.Text = total1.ToString("c0");
                        }
                        else
                        {
                            deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                            deductible2 = double.Parse(dt.Rows[0]["DeductibleAmount2"].ToString().Trim());
                            tenderLimit = gridViewTenderLimit_SpecificRowValue(1);

                            total1 = deductible1 * tenderLimit;
                            if (total1 < 500.0)
                                total1 = 500.0;
                            total2 = deductible2 * tenderLimit;
                            if (total2 < 500.0)
                                total2 = 500.0;

                            lblDeductibleCalculatedTender1.Text = total1.ToString("c0") + " / " + total2.ToString("c0");
                        }
                    }
                    else
                        lblDeductibleCalculatedTender1.Text = "None";
                }
                else
                    lblDeductibleCalculatedTender1.Text = "None";
            }
            else
                lblDeductibleCalculatedTender1.Text = "None";
        }
        else
        {
            lblDeductibleCalculatedTender1.Text = "None";
        }
    }

    public void calculatelblDeductibleCalculatedTenderPreview(bool Option1Or2)
    {
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        if (gridViewTenderLimit.Rows.Count > 0)
        {
            if (gridViewTenderLimit_SpecificRowValue(1) != 0.0)
            {
                double deductible1 = 0.0;
                double deductible2 = 0.0;
                double tenderLimit = 0.0;
                double total1 = 0.0;
                double total2 = 0.0;
                if (Option1Or2)
                {
                    if (ddlDeductibles2.SelectedIndex != 0)
                    {
                        DataTable dt = GetDeductibleAmount(taskControl.DeductibleID2);
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                            {
                                deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                tenderLimit = gridViewTenderLimit_SpecificRowValue(1);

                                total1 = deductible1 * tenderLimit;
                                if (total1 < 500.0)
                                    total1 = 500.0;

                                lblDeductibleCalculatedTender1.Text = total1.ToString("c0");
                            }
                            else
                            {
                                deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                deductible2 = double.Parse(dt.Rows[0]["DeductibleAmount2"].ToString().Trim());
                                tenderLimit = gridViewTenderLimit_SpecificRowValue(1);

                                total1 = deductible1 * tenderLimit;
                                if (total1 < 500.0)
                                    total1 = 500.0;
                                total2 = deductible2 * tenderLimit;
                                if (total2 < 500.0)
                                    total2 = 500.0;

                                lblDeductibleCalculatedTender1.Text = total1.ToString("c0") + " / " + total2.ToString("c0");
                            }
                        }
                        else
                            lblDeductibleCalculatedTender1.Text = "None";
                    }
                    else
                        lblDeductibleCalculatedTender1.Text = "None";
                }
                else
                {
                    if (ddlDeductibles1.SelectedIndex != 0)
                    {
                        DataTable dt = GetDeductibleAmount(taskControl.DeductibleID1);
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                            {
                                deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                tenderLimit = gridViewTenderLimit_SpecificRowValue(1);

                                total1 = deductible1 * tenderLimit;
                                if (total1 < 500.0)
                                    total1 = 500.0;

                                lblDeductibleCalculatedTender1.Text = total1.ToString("c0");
                            }
                            else
                            {
                                deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                deductible2 = double.Parse(dt.Rows[0]["DeductibleAmount2"].ToString().Trim());
                                tenderLimit = gridViewTenderLimit_SpecificRowValue(1);

                                total1 = deductible1 * tenderLimit;
                                if (total1 < 500.0)
                                    total1 = 500.0;
                                total2 = deductible2 * tenderLimit;
                                if (total2 < 500.0)
                                    total2 = 500.0;

                                lblDeductibleCalculatedTender1.Text = total1.ToString("c0") + " / " + total2.ToString("c0");
                            }
                        }
                        else
                            lblDeductibleCalculatedTender1.Text = "None";
                    }
                    else
                        lblDeductibleCalculatedTender1.Text = "None";
                }
                
            }
            else
                lblDeductibleCalculatedTender1.Text = "None";
        }
        else
        {
            lblDeductibleCalculatedTender1.Text = "None";
        }
    }

    public void calculatelblDeductibleCalculatedTender2()
    {
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        if (gridViewTenderLimit.Rows.Count > 1)
        {
            if (gridViewTenderLimit_SpecificRowValue(2) != 0.0)
            {
                double deductible1 = 0.0;
                double deductible2 = 0.0;
                double tenderLimit = 0.0;
                double total1 = 0.0;
                double total2 = 0.0;
                if (ddlDeductibles1.SelectedIndex != 0)
                {
                    DataTable dt = GetDeductibleAmount(taskControl.DeductibleIDPoliza);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                        {
                            deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                            tenderLimit = gridViewTenderLimit_SpecificRowValue(2);

                            total1 = deductible1 * tenderLimit;
                            if (total1 < 500.0)
                                total1 = 500.0;

                            lblDeductibleCalculatedTender2.Text = total1.ToString("c0");
                        }
                        else
                        {
                            deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                            deductible2 = double.Parse(dt.Rows[0]["DeductibleAmount2"].ToString().Trim());
                            tenderLimit = gridViewTenderLimit_SpecificRowValue(2);

                            total1 = deductible1 * tenderLimit;
                            if (total1 < 500.0)
                                total1 = 500.0;
                            total2 = deductible2 * tenderLimit;
                            if (total2 < 500.0)
                                total2 = 500.0;

                            lblDeductibleCalculatedTender2.Text = total1.ToString("c0") + " / " + total2.ToString("c0");
                        }
                    }
                }
                else
                    lblDeductibleCalculatedTender2.Text = "None";
            }
            else
                lblDeductibleCalculatedTender2.Text = "None";
        }
        else
            lblDeductibleCalculatedTender2.Text = "None";
    }

    public void calculatelblDeductibleCalculatedTender2Preview(bool Option1Or2)
    {
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        if (gridViewTenderLimit.Rows.Count > 1)
        {
            if (gridViewTenderLimit_SpecificRowValue(2) != 0.0)
            {
                double deductible1 = 0.0;
                double deductible2 = 0.0;
                double tenderLimit = 0.0;
                double total1 = 0.0;
                double total2 = 0.0;
                if (Option1Or2)
                {
                    if (ddlDeductibles2.SelectedIndex != 0)
                    {
                        DataTable dt = GetDeductibleAmount(taskControl.DeductibleID2);
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                            {
                                deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                tenderLimit = gridViewTenderLimit_SpecificRowValue(2);

                                total1 = deductible1 * tenderLimit;
                                if (total1 < 500.0)
                                    total1 = 500.0;

                                lblDeductibleCalculatedTender2.Text = total1.ToString("c0");
                            }
                            else
                            {
                                deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                deductible2 = double.Parse(dt.Rows[0]["DeductibleAmount2"].ToString().Trim());
                                tenderLimit = gridViewTenderLimit_SpecificRowValue(2);

                                total1 = deductible1 * tenderLimit;
                                if (total1 < 500.0)
                                    total1 = 500.0;
                                total2 = deductible2 * tenderLimit;
                                if (total2 < 500.0)
                                    total2 = 500.0;

                                lblDeductibleCalculatedTender2.Text = total1.ToString("c0") + " / " + total2.ToString("c0");
                            }
                        }
                    }
                    else
                        lblDeductibleCalculatedTender2.Text = "None";
                }
                else
                {
                    if (ddlDeductibles1.SelectedIndex != 0)
                    {
                        DataTable dt = GetDeductibleAmount(taskControl.DeductibleID1);
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                            {
                                deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                tenderLimit = gridViewTenderLimit_SpecificRowValue(2);

                                total1 = deductible1 * tenderLimit;
                                if (total1 < 500.0)
                                    total1 = 500.0;

                                lblDeductibleCalculatedTender2.Text = total1.ToString("c0");
                            }
                            else
                            {
                                deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                deductible2 = double.Parse(dt.Rows[0]["DeductibleAmount2"].ToString().Trim());
                                tenderLimit = gridViewTenderLimit_SpecificRowValue(2);

                                total1 = deductible1 * tenderLimit;
                                if (total1 < 500.0)
                                    total1 = 500.0;
                                total2 = deductible2 * tenderLimit;
                                if (total2 < 500.0)
                                    total2 = 500.0;

                                lblDeductibleCalculatedTender2.Text = total1.ToString("c0") + " / " + total2.ToString("c0");
                            }
                        }
                    }
                    else
                        lblDeductibleCalculatedTender2.Text = "None";
                }
             
            }
            else
                lblDeductibleCalculatedTender2.Text = "None";
        }
        else
            lblDeductibleCalculatedTender2.Text = "None";
    }

    private DataTable GetDeductibleAmount(int DeductibleID)
    {
        DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[1];

        DbRequestXmlCooker.AttachCookItem("DeductibleID",
                   SqlDbType.Int, 0, DeductibleID.ToString(),
                   ref cookItems);

        Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
        DataTable dt = exec.GetQuery("GetDeductibleAmount", DbRequestXmlCooker.Cook(cookItems));

        try
        {
            return dt;

        }
        catch (Exception ex)
        {
            throw new Exception("Could not cook items.", ex);
        }

    }

    public void ddlPI_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtProtectionAndIndemnity = EPolicy.LookupTables.LookupTables.GetTable("ProtectionAndIndemnity");

        double piPremium = 0.00;
        switch (ddlPI.SelectedIndex)
        {
            case 1:
                ddlPILiabilityOnly.Enabled = false;
                txtOtherPI.Enabled = false;
                txtPIPremium.Enabled = false;
                piPremium = double.Parse(dtProtectionAndIndemnity.Rows[0]["PIPremium"].ToString());
                txtPIPremium.Text = piPremium.ToString("c0");
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            case 2:
                ddlPILiabilityOnly.Enabled = false;
                txtOtherPI.Enabled = false;
                txtPIPremium.Enabled = false;
                piPremium = double.Parse(dtProtectionAndIndemnity.Rows[1]["PIPremium"].ToString());
                txtPIPremium.Text = piPremium.ToString("c0");
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            case 3:
                ddlPILiabilityOnly.Enabled = false;
                txtOtherPI.Enabled = false;
                txtPIPremium.Enabled = false;
                piPremium = double.Parse(dtProtectionAndIndemnity.Rows[2]["PIPremium"].ToString());
                txtPIPremium.Text = piPremium.ToString("c0");
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            case 4:
                ddlPILiabilityOnly.Enabled = false;
                txtOtherPI.Enabled = false;
                txtPIPremium.Enabled = false;
                piPremium = double.Parse(dtProtectionAndIndemnity.Rows[3]["PIPremium"].ToString());
                txtPIPremium.Text = piPremium.ToString("c0");
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            default:
                if (txtHullLimit.Text.Trim() == "")
                    ddlPILiabilityOnly.Enabled = true;
                else
                    ddlPILiabilityOnly.Enabled = false;
                txtOtherPI.Enabled = true;
                txtPIPremium.Text = "";
                txtPIPremium.Enabled = false;
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
        }
    }

    public void ddlPILiabilityOnly_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtPILiabilityOnly = EPolicy.LookupTables.LookupTables.GetTable("PILiabilityOnly");

        double piPremium = 0.00;
        double personalEffects = 0.00;
        double personalEffectsDefault = 1000.00;
        double personalEffectsDeductibleDefault = 250.00;
        switch (ddlPILiabilityOnly.SelectedIndex)
        {
            case 1:
                ddlPI.Enabled = false;
                txtOtherPI.Enabled = false;
                txtPIPremium.Enabled = false;
                piPremium = double.Parse(dtPILiabilityOnly.Rows[0]["PILiabilityOnlyPremium"].ToString());
                txtPIPremium.Text = piPremium.ToString("c0");
                txtPersonalEffect.Text = personalEffects.ToString("c0");
                txtPersonalEffectDeductible.Text = personalEffects.ToString("c0");
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            case 2:
                ddlPI.Enabled = false;
                txtOtherPI.Enabled = false;
                txtPIPremium.Enabled = false;
                piPremium = double.Parse(dtPILiabilityOnly.Rows[1]["PILiabilityOnlyPremium"].ToString());
                txtPIPremium.Text = piPremium.ToString("c0");
                txtPersonalEffect.Text = personalEffects.ToString("c0");
                txtPersonalEffectDeductible.Text = personalEffects.ToString("c0");
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            case 3:
                ddlPI.Enabled = false;
                txtOtherPI.Enabled = false;
                txtPIPremium.Enabled = false;
                piPremium = double.Parse(dtPILiabilityOnly.Rows[2]["PILiabilityOnlyPremium"].ToString());
                txtPIPremium.Text = piPremium.ToString("c0");
                txtPersonalEffect.Text = personalEffects.ToString("c0");
                txtPersonalEffectDeductible.Text = personalEffects.ToString("c0");
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            case 4:
                ddlPI.Enabled = false;
                txtOtherPI.Enabled = false;
                txtPIPremium.Enabled = false;
                piPremium = double.Parse(dtPILiabilityOnly.Rows[3]["PILiabilityOnlyPremium"].ToString());
                txtPIPremium.Text = piPremium.ToString("c0");
                txtPersonalEffect.Text = personalEffects.ToString("c0");
                txtPersonalEffectDeductible.Text = personalEffects.ToString("c0");
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            default:
                ddlPI.Enabled = true;
                txtOtherPI.Enabled = true;
                txtPIPremium.Text = "";
                txtPIPremium.Enabled = false;
                txtPersonalEffect.Text = personalEffectsDefault.ToString("c0");
                txtPersonalEffectDeductible.Text = personalEffectsDeductibleDefault.ToString("c0");
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
        }
    }

    protected void txtOtherPI_TextChanged(object sender, EventArgs e)
    {
        if (txtOtherPI.Text != "")
        {
            ddlPI.SelectedIndex = -1;
            ddlPI.Enabled = false;
            ddlPILiabilityOnly.SelectedIndex = -1;
            ddlPILiabilityOnly.Enabled = false;
            txtPIPremium.Enabled = true;
            txtOtherPI.Text = currencyFormat(txtOtherPI.Text);
            if (txtOtherPI.Text.Trim() == "")
            {
                ddlPI.Enabled = true;
                ddlPILiabilityOnly.Enabled = true;
                txtPIPremium.Enabled = false;
            }
        }
        else
        {
            ddlPI.Enabled = true;
            ddlPILiabilityOnly.Enabled = true;
            txtPIPremium.Text = "";
            txtPIPremium.Enabled = false;
            txtOtherPI.Text = "";
        }
    }

    public void ddlMedicalPayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtMedicalPayment = EPolicy.LookupTables.LookupTables.GetTable("MedicalPayment");
        double medicalPremiumTotal = 0.00;
        switch (ddlMedicalPayment.SelectedIndex)
        {
            case 1:
                txtOtherMedicalPayment.Enabled = false;
                medicalPremiumTotal = double.Parse(dtMedicalPayment.Rows[0]["MedicalPaymentPremium"].ToString());
                txtMedicalPaymentPremiumTotal.Text = medicalPremiumTotal.ToString("c0");
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            case 2:
                txtOtherMedicalPayment.Enabled = false;
                medicalPremiumTotal = double.Parse(dtMedicalPayment.Rows[1]["MedicalPaymentPremium"].ToString());
                txtMedicalPaymentPremiumTotal.Text = medicalPremiumTotal.ToString("c0");
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            case 3:
                txtOtherMedicalPayment.Enabled = false;
                medicalPremiumTotal = double.Parse(dtMedicalPayment.Rows[2]["MedicalPaymentPremium"].ToString());
                txtMedicalPaymentPremiumTotal.Text = medicalPremiumTotal.ToString("c0");
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            default:
                txtOtherMedicalPayment.Enabled = true;
                txtMedicalPaymentPremiumTotal.Text = "";
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
        }
    }

    protected void txtOtherMedicalPayment_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtOtherMedicalPayment.Text.Contains("/") == false && txtOtherMedicalPayment.Text != "")
            {
                txtOtherMedicalPayment.Text = "";
                throw new Exception("You must enter a valid format. Example of format: $1,000 / $2,000");
            }
            string[] arrayOfOtherMedical = { "", "" };
            if (txtOtherMedicalPayment.Text != "")
            {
                ddlMedicalPayment.SelectedIndex = -1;
                ddlMedicalPayment.Enabled = false;
                txtMedicalPaymentPremiumTotal.Enabled = true;
                txtOtherMedicalPayment.Text = CleaningEverythingButSlash(txtOtherMedicalPayment.Text);
                arrayOfOtherMedical = txtOtherMedicalPayment.Text.Split('/');
                arrayOfOtherMedical[0] = arrayOfOtherMedical[0].Trim();
                arrayOfOtherMedical[1] = arrayOfOtherMedical[1].Trim();
                arrayOfOtherMedical[0] = currencyFormat(arrayOfOtherMedical[0]);
                arrayOfOtherMedical[1] = currencyFormat(arrayOfOtherMedical[1]);
                txtOtherMedicalPayment.Text = arrayOfOtherMedical[0] + " / " + arrayOfOtherMedical[1];
                //txtOtherMedicalPayment.Text = currencyFormat(txtOtherMedicalPayment.Text);
                if (txtOtherMedicalPayment.Text == "")
                {
                    ddlMedicalPayment.Enabled = true;
                    txtMedicalPaymentPremiumTotal.Enabled = false;
                }
            }
            else
            {
                ddlMedicalPayment.Enabled = true;
                txtMedicalPaymentPremiumTotal.Text = "";
                txtMedicalPaymentPremiumTotal.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            lblRecHeader.Text = ex.Message;
            mpeSeleccion.Show();
        }

    }

    protected void txtMedicalPaymentPremiumTotal_TextChanged(object sender, EventArgs e)
    {
        if (txtMedicalPaymentPremiumTotal.Text != "")
        {
            txtMedicalPaymentPremiumTotal.Text = currencyFormat(txtMedicalPaymentPremiumTotal.Text);
            calculateTotalPremium1();
            calculateTotalPremium2();
        }
    }

    public void ddlUninsuredBoaters_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtUninsuredBoaters = EPolicy.LookupTables.LookupTables.GetTable("UninsuredBoater");

        double premium = 0.00;

        switch (ddlUninsuredBoaters.SelectedIndex)
        {
            case 1:
                txtOtherUninsuredBoater.Enabled = false;
                txtOtherUninsuredBoaterPremium.Enabled = false;
                premium = double.Parse(dtUninsuredBoaters.Rows[0]["UninsuredBoaterPremium"].ToString());
                txtOtherUninsuredBoaterPremium.Text = premium.ToString("c0");
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            case 2:
                txtOtherUninsuredBoater.Enabled = false;
                txtOtherUninsuredBoaterPremium.Enabled = false;
                txtOtherUninsuredBoaterPremium.Text = dtUninsuredBoaters.Rows[1]["UninsuredBoaterPremium"].ToString();
                txtOtherUninsuredBoaterPremium.Text = currencyFormat(txtOtherUninsuredBoaterPremium.Text);
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            case 3:
                txtOtherUninsuredBoater.Enabled = false;
                txtOtherUninsuredBoaterPremium.Enabled = false;
                txtOtherUninsuredBoaterPremium.Text = dtUninsuredBoaters.Rows[2]["UninsuredBoaterPremium"].ToString();
                txtOtherUninsuredBoaterPremium.Text = currencyFormat(txtOtherUninsuredBoaterPremium.Text);
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            case 4:
                txtOtherUninsuredBoater.Enabled = false;
                txtOtherUninsuredBoaterPremium.Enabled = false;
                txtOtherUninsuredBoaterPremium.Text = dtUninsuredBoaters.Rows[2]["UninsuredBoaterPremium"].ToString();
                txtOtherUninsuredBoaterPremium.Text = currencyFormat(txtOtherUninsuredBoaterPremium.Text);
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;
            default:
                txtOtherUninsuredBoater.Enabled = true;
                txtOtherUninsuredBoaterPremium.Enabled = true;
                txtOtherUninsuredBoaterPremium.Text = "";
                calculateTotalPremium1();
                calculateTotalPremium2();
                break;

        }
    }

    protected void txtOtherUninsuredBoater_TextChanged(object sender, EventArgs e)
    {
        if (txtOtherUninsuredBoater.Text != "")
        {
            ddlUninsuredBoaters.SelectedIndex = -1;
            ddlUninsuredBoaters.Enabled = false;
            txtOtherUninsuredBoaterPremium.Enabled = true;
            txtOtherUninsuredBoater.Text = currencyFormat(txtOtherUninsuredBoater.Text);

        }
        else
        {
            ddlUninsuredBoaters.Enabled = true;
            txtOtherUninsuredBoaterPremium.Enabled = false;
        }
    }

    protected void txtOtherUninsuredBoaterPremium_TextChanged(object sender, EventArgs e)
    {
        if (txtOtherUninsuredBoaterPremium.Text != "")
        {

            txtOtherUninsuredBoaterPremium.Text = currencyFormat(txtOtherUninsuredBoaterPremium.Text);
            calculateTotalPremium1();
            calculateTotalPremium2();

        }
        else
        {
            txtOtherUninsuredBoaterPremium.Text = "";
        }
    }


    protected void txtHullLimit_TextChanged(object sender, EventArgs e)
    {
        if (txtHullLimit.Text.Trim() == "")
        {
            calculateWatercraftLimit();
            //if(ddlDeductibles1.SelectedIndex != -1)
            //    calculatelblDeductibleCalculated1();
            //else
            //   lblDeductibleCalculated1.Text = "None";
            //if (ddlDeductibles2.SelectedIndex != -1)
            //    calculatelblDeductibleCalculated2();
            //else
            //    lblDeductibleCalculated2.Text = "None";
            ddlDeductibles1.SelectedIndex = -1;
            ddlDeductibles2.SelectedIndex = -1;
            lblDeductibleCalculated1.Text = "None";
            lblDeductibleCalculated2.Text = "None";
            txtRate1.Text = "";
            txtRate2.Text = "";
            if ((ddlPI.SelectedIndex != -1 && ddlPI.SelectedIndex != 0) || txtOtherPI.Text.Trim() != "")
            {
                ddlPILiabilityOnly.Enabled = false;
            }
            else
            {
                ddlPILiabilityOnly.Enabled = true;
            }

            calculateWatercraftLimitPremium1();
            calculateTotalPremium1();
            calculateWatercraftLimitPremium2();
            calculateTotalPremium2();
        }
        else
        {
            calculateWatercraftLimit();
            if (ddlDeductibles1.SelectedIndex != -1)
                calculatelblDeductibleCalculated1();
            else
                lblDeductibleCalculated1.Text = "None";
            if (ddlDeductibles2.SelectedIndex != -1)
                calculatelblDeductibleCalculated2();
            else
                lblDeductibleCalculated2.Text = "None";
            if (ddlPILiabilityOnly.SelectedIndex != -1 && ddlPILiabilityOnly.SelectedIndex != 0)
            {
                ddlPILiabilityOnly.SelectedIndex = -1;
                ddlPILiabilityOnly.Enabled = false;
                txtPIPremium.Text = "";
                ddlPI.Enabled = true;
                
            }
            //if (ddlPI.SelectedIndex != -1 && ddlPI.SelectedIndex != 0)
            //{
            //    ddlPILiabilityOnly.SelectedIndex = -1;
            //    txtPIPremium.Text = "";
            //    ddlPILiabilityOnly.Enabled = false;
            //}
            //else
            //{
            //    ddlPILiabilityOnly.Enabled = false;
            //}
            calculateWatercraftLimitPremium1();
            calculateTotalPremium1();
            calculateWatercraftLimitPremium2();
            calculateTotalPremium2();
            txtHullLimit.Text = currencyFormat(txtHullLimit.Text);
        }

    }

    //protected void txtTenderLimit_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtTenderLimit.Text != "")
    //        txtTenderLimit.Text = currencyFormat(txtTenderLimit.Text);
    //}

    protected void calculateWatercraftLimit()
    {
        double tenderLimitTotal = 0.00;
        double hullLimit = 0.00;
        double total = 0.00;

        if (txtWatercraftLimit1.Text != "" || txtWatercraftLimit2.Text != "")
        {
            txtWatercraftLimit1.Text = "";
            txtWatercraftLimit2.Text = "";
        }

        tenderLimitTotal = gridViewTenderLimit_sumTotal();
        if (txtHullLimit.Text != "")
        {
            hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());
        }

        total = tenderLimitTotal + hullLimit;
        txtWatercraftLimit1.Text = total.ToString("c0");
        txtWatercraftLimit2.Text = total.ToString("c0");

        if (txtWatercraftLimit1.Text.Trim() == "$0" || txtWatercraftLimit2.Text.Trim() == "$0")
        {
            txtWatercraftLimit1.Text = "";
            txtWatercraftLimit2.Text = "";
            ddlPILiabilityOnly.Enabled = true;
        }
        else
            ddlPILiabilityOnly.Enabled = false;
    }

    protected void calculateWatercraftLimitPremium1()
    {
        double rate = 0.00;
        double watercraftLimit = 0.00;
        double total = 0.00;

        if (txtRate1.Text != "")
            rate = double.Parse(txtRate1.Text.Replace("%", "").ToString());
        rate = rate / 100.00;
        if (txtWatercraftLimit1.Text != "")
            watercraftLimit = double.Parse(txtWatercraftLimit1.Text.Replace("$", "").Replace(",", "").ToString());

        total = rate * watercraftLimit;

        txtWatercraftLimitTotal1.Text = currencyFormat(total.ToString());
        if (txtWatercraftLimitTotal1.Text == "$0")
            txtWatercraftLimitTotal1.Text = "";

    }

    protected void calculateWatercraftLimitPremium2()
    {
        double rate = 0.00;
        double watercraftLimit = 0.00;
        double total = 0.00;

        if (txtRate2.Text != "")
            rate = double.Parse(txtRate2.Text.Replace("%", "").ToString());
        rate = rate / 100.00;
        if (txtWatercraftLimit2.Text != "")
            watercraftLimit = double.Parse(txtWatercraftLimit2.Text.Replace("$", "").Replace(",", "").ToString());

        total = rate * watercraftLimit;

        txtWatercraftLimitTotal2.Text = currencyFormat(total.ToString());
        if (txtWatercraftLimitTotal2.Text == "$0")
            txtWatercraftLimitTotal2.Text = "";

    }

    protected void txtRate1_TextChanged(object sender, EventArgs e)
    {
        if (txtRate1.Text != "")
        {
            calculateWatercraftLimitPremium1();
            txtRate1.Text = txtRate1.Text.Replace("%", "").ToString();
            txtRate1.Text = txtRate1.Text + "%";
            calculateTotalPremium1();
        }
        else
        {
            txtRate1.Text = "";
            txtWatercraftLimitTotal1.Text = "";
            calculateTotalPremium1();
        }

    }

    protected void txtRate2_TextChanged(object sender, EventArgs e)
    {
        if (txtRate2.Text != "")
        {
            calculateWatercraftLimitPremium2();
            txtRate2.Text = txtRate2.Text.Replace("%", "").ToString();
            txtRate2.Text = txtRate2.Text + "%";
            calculateTotalPremium2();
        }
        else
        {
            txtRate2.Text = "";
            txtWatercraftLimitTotal2.Text = "";
            calculateTotalPremium2();
        }
    }

    protected void txtPIPremium_TextChanged(object sender, EventArgs e)
    {
        if (txtPIPremium.Text.Trim() != "")
        {
            txtPIPremium.Text = currencyFormat(txtPIPremium.Text);
            calculateTotalPremium1();
            calculateTotalPremium2();
        }

    }

    protected void txtPersonalEffect_TextChanged(object sender, EventArgs e)
    {
        double personalEffects = 0.00;
        if (txtPersonalEffect.Text.Trim() != "")
        {
            personalEffects = double.Parse(txtPersonalEffect.Text.Replace("$", "").Replace(",", "").ToString());
            if (personalEffects > 1000.00)
            {
                calculatePEPremium(personalEffects);
            }
            else
            {
                txtPersonalEffect.Text = "$1,000";
                txtPersonalEffectDeductible.Text = "$250";
                txtPersonalEffectPremium.Text = "$0";
                calculateTotalPremium1();
                calculateTotalPremium2();
            }

            txtPersonalEffect.Text = currencyFormat(txtPersonalEffect.Text);
        }
    }


    protected void txtPersonalEffectDeductible_TextChanged(object sender, EventArgs e)
    {
        double personalEffects = 0.00;
        if (txtPersonalEffect.Text.Trim() != "")
        {
            personalEffects = double.Parse(txtPersonalEffect.Text.Replace("$", "").Replace(",", "").ToString());
            txtPersonalEffectDeductible.Text = currencyFormat(txtPersonalEffectDeductible.Text);
            calculatePEPremium(personalEffects);
        }
    }

    protected void calculatePEPremium(double personalEffects)
    {
        double excess = 0.00;
        double pePremium = 0.00;

        excess = (personalEffects - 1000.00) * 0.03;

        pePremium = excess;

        txtPersonalEffectPremium.Text = currencyFormat(pePremium.ToString());
        calculateTotalPremium1();
        calculateTotalPremium2();
    }

    protected void txtTrailer_TextChanged(object sender, EventArgs e)
    {
        double trailer = 0.00;

        if (txtTrailer.Text != "")
        {
            trailer = double.Parse(txtTrailer.Text.Replace("$", "").Replace(",", "").ToString());
            if (trailer == 0)
            {
                txtTrailer.Text = "";
                txtTrailerPremium.Text = "";
            }
            else
            {
                calculateTrailerPremium();
                txtTrailer.Text = currencyFormat(txtTrailer.Text);
            }
        }
        else
        {
            txtTrailerPremium.Text = "";
            calculateTrailerPremium();
        }
    }

    protected void calculateTrailerPremium()
    {
        double premium = 0.00;
        double trailer = 0.00;
        if (txtTrailer.Text != "")
        {
            trailer = double.Parse(txtTrailer.Text.Replace("$", "").Replace(",", "").ToString());
            premium = trailer * 0.03;
            txtTrailerPremium.Text = currencyFormat(premium.ToString());
            calculateTotalPremium1();
            calculateTotalPremium2();
        }
        else
        {
            calculateTotalPremium1();
            calculateTotalPremium2();
        }
    }

    protected void txtTripTransit_TextChanged(object sender, EventArgs e)
    {
        if (txtTripTransit.Text != "")
        {

            txtTripTransit.Text = currencyFormat(txtTripTransit.Text);
            calculateTotalPremium1();
            calculateTotalPremium2();

        }
        else
        {
            txtTripTransit.Text = "";
            calculateTotalPremium1();
            calculateTotalPremium2();
        }
    }

    protected void txtMiscellaneous_TextChanged(object sender, EventArgs e)
    {
        if (txtMiscellaneous.Text != "")
        {

            txtMiscellaneous.Text = currencyFormat(txtMiscellaneous.Text);
            calculateTotalPremium1();
            calculateTotalPremium2();

        }
        else
        {
            txtMiscellaneous.Text = "";
            calculateTotalPremium1();
            calculateTotalPremium2();
        }
    }

    //protected void txtSurveyFee_TextChanged(object sender, EventArgs e)
    //{
    //    if (txtSurveyFee.Text != "")
    //    {

    //        txtSurveyFee.Text = currencyFormat(txtSurveyFee.Text);

    //    }
    //    else
    //    {
    //        txtSurveyFee.Text = "";
    //    }
    //}

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        
        try
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

            if (!(radioBtnTP1.Checked) && !(radioBtnTP2.Checked))
                throw new Exception("You must select one option of Total Premium");
            btnConvert.Enabled = false;
            VerifyPolicyExist();

            EPolicy.TaskControl.Yacht taskControlQuote = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

            Session.Clear();
            EPolicy.TaskControl.Yacht taskControl = new EPolicy.TaskControl.Yacht(false);

            txtEngine.Enabled = false;
            txtEngineSerialNumber.Enabled = false;
            btnBankList.Enabled = false;
            btnPreviewPolicy.Visible = false;


            taskControl.Mode = 1; //ADD
            taskControl.isQuote = false;
            taskControl.IsCommercial = taskControlQuote.IsCommercial;
            taskControl.EnteredBy = cp.Identity.Name.Split("|".ToCharArray())[0];
            taskControl.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Yacht"));
            taskControl.InsuranceCompany = taskControl.InsuranceCompany;
            taskControl.Agency = taskControlQuote.Agency;
            taskControl.Agent = taskControlQuote.Agent;
            taskControl.OriginatedAt = taskControlQuote.OriginatedAt;
            taskControl.DepartmentID = 1;
            taskControl.EffectiveDate = taskControlQuote.EffectiveDate;
            taskControl.ExpirationDate = taskControlQuote.ExpirationDate;
            taskControl.PreviousPolicy = taskControlQuote.PreviousPolicy;
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
            taskControl.Customer.SocialSecurity = taskControlQuote.Customer.SocialSecurity.Trim();


            taskControl.PolicyType = "MAR";
            taskControl.InsuranceCompany = "001";

            taskControl.CompanyDealer = taskControlQuote.CompanyDealer;
            taskControl.InsuranceCompany = taskControlQuote.InsuranceCompany;
            taskControl.OriginatedAt = taskControlQuote.OriginatedAt;

            taskControl.CancellationDate = taskControlQuote.CancellationDate;
            taskControl.CancellationEntryDate = "";
            taskControl.CancellationUnearnedPercent = taskControlQuote.CancellationUnearnedPercent;
            taskControl.CancellationMethod = taskControlQuote.CancellationMethod;
            taskControl.CancellationReason = taskControlQuote.CancellationReason;
            taskControl.PaidAmount = taskControlQuote.PaidAmount;
            taskControl.PaidDate = "";
            taskControl.Ren_Rei = taskControlQuote.Ren_Rei;
            taskControl.CommissionAgency = taskControlQuote.CommissionAgency;
            taskControl.CommissionAgent = taskControlQuote.CommissionAgent;
            taskControl.CommissionAgencyPercent = taskControlQuote.CommissionAgencyPercent;
            taskControl.CommissionAgentPercent = taskControlQuote.CommissionAgentPercent;
            taskControl.TaskControlID = taskControlQuote.TaskControlID;
            taskControl.Status = taskControlQuote.Status;
            taskControl.ReturnCharge = taskControlQuote.ReturnCharge;
            taskControl.ReturnPremium = taskControlQuote.ReturnPremium;
            taskControl.CancellationAmount = taskControlQuote.CancellationAmount;
            taskControl.ReturnCommissionAgency = taskControlQuote.ReturnCommissionAgency;
            taskControl.ReturnCommissionAgent = taskControlQuote.ReturnCommissionAgent;

            taskControl.EntryDate = DateTime.Now;
            taskControl.Term = taskControl.Term;

            taskControl.Charge = taskControlQuote.Charge;
            taskControl.TCIDQuotes = taskControlQuote.TaskControlID;

            taskControl.RenewalOfYacht = taskControlQuote.RenewalOfYacht;
            taskControl.Producer = taskControlQuote.Producer;
            taskControl.BoatName = taskControlQuote.BoatName;
            taskControl.HullLimit = taskControlQuote.HullLimit;
            taskControl.BoatYear = taskControlQuote.BoatYear;
            taskControl.Loa = taskControlQuote.Loa;
            taskControl.BoatModel = taskControlQuote.BoatModel;
            taskControl.BoatBuilder = taskControlQuote.BoatBuilder;
            taskControl.HullNumberRegistration = taskControlQuote.HullNumberRegistration;
            taskControl.HomeportLocation = taskControlQuote.HomeportLocation;

            if (radioBtnTP1.Checked)
            {
                taskControl.TotalPremium = taskControlQuote.TotalPremium1;
                taskControl.TotalPremiumPoliza = taskControlQuote.TotalPremium1;
                taskControl.WatercraftLimitPoliza = taskControlQuote.WatercraftLimit1;
                taskControl.RatePoliza = taskControlQuote.Rate1;
                taskControl.WatercraftLimitTotalPoliza = taskControlQuote.WatercraftLimitTotal1;
                taskControl.DeductibleIDPoliza = taskControlQuote.DeductibleID1;
                txtWatercraftLimit2.Enabled = false;
                txtWatercraftLimit2.Visible = false;
                txtRate2.Enabled = false;
                txtRate2.Visible = false;
                txtWatercraftLimitTotal2.Enabled = false;
                txtWatercraftLimitTotal2.Visible = false;
                ddlDeductibles2.Enabled = false;
                ddlDeductibles2.Visible = false;
            }
            else if (radioBtnTP2.Checked)
            {
                taskControl.TotalPremium = taskControlQuote.TotalPremium2;
                taskControl.TotalPremiumPoliza = taskControlQuote.TotalPremium2;
                taskControl.WatercraftLimitPoliza = taskControlQuote.WatercraftLimit2;
                taskControl.RatePoliza = taskControlQuote.Rate2;
                taskControl.WatercraftLimitTotalPoliza = taskControlQuote.WatercraftLimitTotal2;
                taskControl.DeductibleIDPoliza = taskControlQuote.DeductibleID2;
                txtWatercraftLimit1.Enabled = false;
                txtWatercraftLimit1.Visible = false;
                txtRate1.Enabled = false;
                txtRate1.Visible = false;
                txtWatercraftLimitTotal1.Enabled = false;
                txtWatercraftLimitTotal1.Visible = false;
                ddlDeductibles1.Enabled = false;
                ddlDeductibles1.Visible = false;
            }
            taskControl.OtherPI = taskControlQuote.OtherPI;
            taskControl.PIPremium = taskControlQuote.PIPremium;
            taskControl.OtherMedicalPayment = taskControlQuote.OtherMedicalPayment;
            taskControl.MedicalPaymentPremiumTotal = taskControlQuote.MedicalPaymentPremiumTotal;
            taskControl.PersonalEffects = taskControlQuote.PersonalEffects;
            taskControl.PEDeductible = taskControlQuote.PEDeductible;
            taskControl.PersonalEffectsPremium = taskControlQuote.PersonalEffectsPremium;
            taskControl.Trailer = taskControlQuote.Trailer;
            taskControl.TrailerPremium = taskControlQuote.TrailerPremium;
            taskControl.OtherUninsuredBoaterPremium = taskControlQuote.OtherUninsuredBoaterPremium;
            taskControl.OtherUninsuredBoater = taskControlQuote.OtherUninsuredBoater;
            taskControl.TripTransit = taskControlQuote.TripTransit;
            taskControl.TripTransitNotes = taskControlQuote.TripTransitNotes;
            taskControl.Miscellaneous = taskControlQuote.Miscellaneous;
            taskControl.MiscellaneousNotes = taskControlQuote.MiscellaneousNotes;
            taskControl.SubjectivityNotes = taskControlQuote.SubjectivityNotes;
            taskControl.HomeportID = taskControlQuote.HomeportID;
            taskControl.HomeportAddress = taskControlQuote.HomeportAddress;
            taskControl.NavigationLimitID = taskControlQuote.NavigationLimitID;
            taskControl.PIID = taskControlQuote.PIID;
            taskControl.PILiabilityOnlyID = taskControlQuote.PILiabilityOnlyID;
            taskControl.MedicalPaymentID = taskControlQuote.MedicalPaymentID;
            taskControl.UninsuredBoaterID = taskControlQuote.UninsuredBoaterID;
            if (ddlBank.SelectedItem.Value.ToString() == "")
                taskControl.BankPPSID = "000";
            else
                taskControl.BankPPSID = ddlBank.SelectedItem.Value.ToString();
            taskControl.Bank = "000";
            taskControl.Engine = txtEngine.Text.Trim();
            taskControl.EngineSerialNumber = txtEngineSerialNumber.Text.Trim();
            taskControl.TrailerModel = txtTrailerModel.Text.Trim();
            taskControl.TrailerSerial = txtTrailerSerial.Text.Trim();

            taskControl.NavigationLimitCollection = taskControlQuote.NavigationLimitCollection;
            taskControl.TenderLimitCollection = taskControlQuote.TenderLimitCollection;
            taskControl.SurveyCollection = taskControlQuote.SurveyCollection;

            taskControl.SaveYacht(userID);

            taskControl = (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);

            Session["TaskControl"] = taskControl;

            try
            {
                SendPolicyToPPS(taskControl.TaskControlID);
                DataTable dtTender = GetYachtToPPSByTaskControlIDInfoTenderLimit(taskControl.TaskControlID);
                if (dtTender.Rows.Count > 0 && dtTender.Rows[0]["TenderLimitAmount2"].ToString() != "")
                {
                    SendPolicyToPPSTender2(taskControl.TaskControlID);
                }
            }
            catch (Exception xp)
            {
                LogError(xp);
                Session.Remove("TaskControl");
                //Si hubo error en PPS, se pone el numero de poliza en blanco para que no muestre el numero de poliza de ePPS.
                UpdatePolicyErrorPPSModifyPolicyNO(taskControl.TaskControlID);
                //UpdatePolicyMode_VI(taskControlQuote.TaskControlID, "2");
                //taskControlQuote.Mode = 4;
                Session.Add("TaskControl", taskControlQuote);
                DisableControl();
                FillTextControl();
            }

            Session.Add("TaskControl", taskControl);
            if (taskControl.BankPPSID.Trim() != "000" || taskControl.BankPPSID.Trim() != "")
            {
                lblBankListSelected2.Visible = true;
            }
            btnPremiumFinance2.Enabled = true;
            


            try
            {
                Response.Redirect("Yacht.aspx");
                lblRecHeader.Text = "Yacht information converted to policy successfully. The policy number is: " + taskControl.PolicyNo;
                mpeSeleccion.Show();
            }
            catch (Exception xp)
            {

            }

        }
        catch (Exception ex)
        {
            lblRecHeader.Text = ex.Message;
            mpeSeleccion.Show();
        }
    }

    private void UpdatePolicyErrorPPSModifyPolicyNO(int TaskControlID)
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

        try
        {
            exec.GetQuery("UpdatePolicyErrorPPSModifyPolicyNO", xmlDoc);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve policy information.", ex);
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
        string path = Server.MapPath("~/ErrorLog/ErrorLogYacht.txt");
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine(message);
            writer.Close();
        }
    }


    protected void imgCalendarSurveyDate_Click(object sender, EventArgs e)
    {
        if (Calendar1.Visible == false)
            Calendar1.Visible = true;
        else
            Calendar1.Visible = false;
    }


    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        txtSurveyDate.Text = Calendar1.SelectedDate.ToShortDateString();
        Calendar1.Visible = false;
    }

    protected void imgCalendarSurveyDateCompleted_Click(object sender, EventArgs e)
    {
        if (Calendar2.Visible == false)
            Calendar2.Visible = true;
        else
            Calendar2.Visible = false;
    }


    protected void Calendar2_SelectionChanged(object sender, EventArgs e)
    {
        txtSurveyDateCompleted.Text = Calendar2.SelectedDate.ToShortDateString();
        Calendar2.Visible = false;
    }

    protected void imgCalendarEffectiveDate_Click(object sender, EventArgs e)
    {
        if (Calendar3.Visible == false)
            Calendar3.Visible = true;
        else
            Calendar3.Visible = false;
    }


    protected void Calendar3_SelectionChanged(object sender, EventArgs e)
    {
        txtEffectiveDate.Text = Calendar3.SelectedDate.ToShortDateString();
        Calendar3.Visible = false;
        txtExpirationDate.Text = (Convert.ToDateTime(txtEffectiveDate.Text).AddYears(1)).ToShortDateString();
    }

    //protected void txtEffectiveDate_TextChanged(object sender, EventArgs e)
    //{
    //    txtExpirationDate.Text = (Convert.ToDateTime(txtEffectiveDate.Text).AddYears(1)).ToString();
    //}

    private List<string> ImprimirQuote(List<string> mergePaths, string rdlcname)
    {
        try
        {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
            int taskControl1 = taskControl.TaskControlID;
            GetYachtQuoteReportTableAdapters.GetYachtQuoteReportTableAdapter ta = new GetYachtQuoteReportTableAdapters.GetYachtQuoteReportTableAdapter();
            ReportDataSource rpd = new ReportDataSource();
            rpd = new ReportDataSource("GetYachtQuoteReport", (DataTable)ta.GetData(taskControl1));

            GetReportTenderLimitByTaskControlIDTableAdapters.GetReportTenderLimitByTaskControlIDTableAdapter ta2 = new GetReportTenderLimitByTaskControlIDTableAdapters.GetReportTenderLimitByTaskControlIDTableAdapter();
            ReportDataSource rpd2 = new ReportDataSource();
            rpd2 = new ReportDataSource("GetReportTenderLimitByTaskControlID", (DataTable)ta2.GetData(taskControl1));

            GetReportSurveyByTaskControlIDTableAdapters.GetReportSurveyByTaskControlIDTableAdapter ta3 = new GetReportSurveyByTaskControlIDTableAdapters.GetReportSurveyByTaskControlIDTableAdapter();
            ReportDataSource rpd3 = new ReportDataSource();
            rpd3 = new ReportDataSource("GetReportSurveyByTaskControlID", (DataTable)ta3.GetData(taskControl1));

            GetTaskControlByTaskControlIDTableAdapters.GetTaskControlByTaskControlIDTableAdapter ta4 = new GetTaskControlByTaskControlIDTableAdapters.GetTaskControlByTaskControlIDTableAdapter();
            ReportDataSource rpd4 = new ReportDataSource();
            rpd4 = new ReportDataSource("GetTaskControlByTaskControlID", (DataTable)ta4.GetData(taskControl1));

            GetAgentByTaskControlIDTableAdapters.GetAgentByTaskControlIDTableAdapter ta5 = new GetAgentByTaskControlIDTableAdapters.GetAgentByTaskControlIDTableAdapter();
            ReportDataSource rpd5 = new ReportDataSource();
            rpd5 = new ReportDataSource("GetAgentByTaskControlID", (DataTable)ta5.GetData(taskControl1));

            //GetAgencyByTaskControlIDTableAdapters.GetAgencyByTaskControlIDTableAdapter ta6 = new GetAgencyByTaskControlIDTableAdapters.GetAgencyByTaskControlIDTableAdapter();
            //ReportDataSource rpd6 = new ReportDataSource();
            //rpd6 = new ReportDataSource("GetAgencyByTaskControlID", (DataTable)ta6.GetData(taskControl1));


            var QRTaskcontrolID = taskControl1.ToString();

            if (!string.IsNullOrEmpty(QRTaskcontrolID))
                GenerateQRCode(ref QRTaskcontrolID);

            ReportParameter[] parameters = new ReportParameter[8];
            Uri pathasUri = null;
            if (QRTaskcontrolID != "")
            {
                pathasUri = new Uri(QRTaskcontrolID);
                parameters[1] = new ReportParameter("QRCode", pathasUri.AbsoluteUri);
            }
            else
            {
                parameters[1] = new ReportParameter("QRCode", QRTaskcontrolID != "" ? QRTaskcontrolID : "");
            }


            if (rdlcname == "PrivateYachtInsuranceQuote.rdlc")
            {
                DataTable dtNavigationLimit = taskControl.NavigationLimitCollection;
                string[] navigationcollection;
                if (dtNavigationLimit.Rows.Count == 0)
                {

                    navigationcollection = new string[1];
                    navigationcollection[0] = "";
                }
                else
                {
                    navigationcollection = new string[dtNavigationLimit.Rows.Count];
                    for (int i = 0; i < dtNavigationLimit.Rows.Count; i++)
                    {
                        navigationcollection[i] = dtNavigationLimit.Rows[i]["NavigationLimitDesc"].ToString();
                    }
                }
                parameters[0] = new ReportParameter("NavigationLimitCollection", navigationcollection);

                string firstOtherMedicalPayment, secondOtherMedicalPayment, completeOtherMedicalPayment, firstMedicalPayment, secondMedicalPayment, completeMedicalPayment;
                string[] arrayOfOtherMedical = { "", "" };
                string[] arrayOfMedicalPayment = { "", "" };

                if (txtOtherMedicalPayment.Text.Trim() == "")
                {
                    parameters[2] = new ReportParameter("FirstOtherMedicalPayment", "");
                    parameters[3] = new ReportParameter("SecondOtherMedicalPayment", "");
                    parameters[4] = new ReportParameter("CompleteOtherMedicalPayment", "");
                }
                else
                {
                    arrayOfOtherMedical = txtOtherMedicalPayment.Text.Split('/');
                    firstOtherMedicalPayment = arrayOfOtherMedical[0].Trim().Replace("$", "").Replace(",", "");
                    secondOtherMedicalPayment = arrayOfOtherMedical[1].Trim().Replace("$", "").Replace(",", "");
                    double firstOtherMedicalPaymentDouble = double.Parse(firstOtherMedicalPayment);
                    double secondOtherMedicalPaymentDouble = double.Parse(secondOtherMedicalPayment);
                    int firstOtherMedicalPaymentInt = (int)firstOtherMedicalPaymentDouble;
                    int secondOtherMedicalPaymentInt = (int)secondOtherMedicalPaymentDouble;
                    completeOtherMedicalPayment = firstOtherMedicalPaymentInt.ToString("C0") + " p / " + secondOtherMedicalPaymentInt.ToString("C0") + " a *";
                    parameters[2] = new ReportParameter("FirstOtherMedicalPayment", firstOtherMedicalPaymentInt.ToString("C0"));
                    parameters[3] = new ReportParameter("SecondOtherMedicalPayment", secondOtherMedicalPaymentInt.ToString("C0"));
                    parameters[4] = new ReportParameter("CompleteOtherMedicalPayment", completeOtherMedicalPayment);
                }

                if (ddlMedicalPayment.SelectedItem.Text == "")
                {
                    parameters[5] = new ReportParameter("FirstMedicalPayment", "");
                    parameters[6] = new ReportParameter("SecondMedicalPayment", "");
                    parameters[7] = new ReportParameter("CompleteMedicalPayment", "");
                }
                else
                {
                    arrayOfMedicalPayment = ddlMedicalPayment.SelectedItem.Text.Split('/');
                    firstMedicalPayment = arrayOfMedicalPayment[0].Trim().Replace("$", "").Replace(",", "");
                    secondMedicalPayment = arrayOfMedicalPayment[1].Trim().Replace("$", "").Replace(",", "");
                    double firstMedicalPaymentDouble = double.Parse(firstMedicalPayment);
                    double secondMedicalPaymentDouble = double.Parse(secondMedicalPayment);
                    int firstMedicalPaymentInt = (int)firstMedicalPaymentDouble;
                    int secondMedicalPaymentInt = (int)secondMedicalPaymentDouble;
                    completeMedicalPayment = firstMedicalPaymentInt.ToString("C0") + " p / " + secondMedicalPaymentInt.ToString("C0") + " a *";
                    parameters[5] = new ReportParameter("FirstMedicalPayment", firstMedicalPaymentInt.ToString("C0"));
                    parameters[6] = new ReportParameter("SecondMedicalPayment", secondMedicalPaymentInt.ToString("C0"));
                    parameters[7] = new ReportParameter("CompleteMedicalPayment", completeMedicalPayment);
                }
            }

            ReportViewer viewer1 = new ReportViewer();
            viewer1.LocalReport.DataSources.Clear();
            viewer1.ProcessingMode = ProcessingMode.Local;
            viewer1.LocalReport.EnableExternalImages = true;
            viewer1.LocalReport.ReportPath = Server.MapPath("Reports/Yacht/" + rdlcname); //Reports/Yacht/YachtNewQuote.rdlc
            if (rdlcname == "PrivateYachtInsuranceQuote.rdlc" || rdlcname == "YachtNewQuote.rdlc")
            {
                viewer1.LocalReport.DataSources.Add(rpd);
                viewer1.LocalReport.DataSources.Add(rpd2);
                viewer1.LocalReport.DataSources.Add(rpd3);
                viewer1.LocalReport.DataSources.Add(rpd4);
                if (rdlcname == "YachtNewQuote.rdlc")
                {
                    viewer1.LocalReport.DataSources.Add(rpd5);
                    //viewer1.LocalReport.DataSources.Add(rpd6);
                }
            }
            if (rdlcname == "PrivateYachtInsuranceQuote.rdlc")
            {
                viewer1.LocalReport.SetParameters(parameters);
            }
            viewer1.LocalReport.Refresh();

            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string filetype = string.Empty;

            string RandomString = Guid.NewGuid().ToString();
            string fileName1 = "";
            string _FileName1 = "";
            if (rdlcname == "YachtNewQuote.rdlc")
            {
                fileName1 = "WorksheetQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "WorksheetQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }
            else if (rdlcname == "PrivateYachtInsuranceQuote.rdlc")
            {
                fileName1 = "ClientQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "ClientQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }

            else if (rdlcname == "YachtCertificateLiability.rdlc")
            {
                fileName1 = "Policy-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "Policy-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }


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
    private List<string> ImprimirPolicy(List<string> mergePaths, string rdlcname)
    {
        try
        {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
            int taskControl1 = taskControl.TaskControlID;


            //-------------------------------------------------GetYachtEndorsementAReport------------------------------------------------------------------------------------
            GetYachtEndorsementAReportTableAdapters.GetYachtEndorsementAReportTableAdapter ta = new GetYachtEndorsementAReportTableAdapters.GetYachtEndorsementAReportTableAdapter();
            ReportDataSource rpd = new ReportDataSource();
            rpd = new ReportDataSource("GetYachtEndorsementAReport", (DataTable)ta.GetData(taskControl1));

            GetReportTenderLimitByTaskControlIDTableAdapters.GetReportTenderLimitByTaskControlIDTableAdapter ta1 = new GetReportTenderLimitByTaskControlIDTableAdapters.GetReportTenderLimitByTaskControlIDTableAdapter();
            ReportDataSource rpd1 = new ReportDataSource();
            rpd1 = new ReportDataSource("GetReportTenderLimitByTaskControlID", (DataTable)ta1.GetData(taskControl1));

            //-------------------------------------------------GetYachtEndorsementAReport------------------------------------------------------------------------------------

            //-------------------------------------------------GetYachtEndorsementBReport------------------------------------------------------------------------------------
            GetYachtEndorsementBTableAdapters.GetYachtEndorsementBTableAdapter ta2 = new GetYachtEndorsementBTableAdapters.GetYachtEndorsementBTableAdapter();
            ReportDataSource rpd2 = new ReportDataSource();
            rpd2 = new ReportDataSource("GetYachtEndorsementB", (DataTable)ta2.GetData(taskControl1));
            //-------------------------------------------------GetYachtEndorsementBReport------------------------------------------------------------------------------------

            //-------------------------------------------------GetYachtCertificateReport------------------------------------------------------------------------------------
            GetYachtCertificateReportTableAdapters.GetYachtCertificateReportTableAdapter ta3 = new GetYachtCertificateReportTableAdapters.GetYachtCertificateReportTableAdapter();
            ReportDataSource rpd3 = new ReportDataSource();
            rpd3 = new ReportDataSource("GetYachtCertificateReport", (DataTable)ta3.GetData(taskControl1));
            //-------------------------------------------------GetYachtCertificateReport------------------------------------------------------------------------------------

            //-------------------------------------------------GetYachtPortsEndorsement------------------------------------------------------------------------------------
            GetYachtPortsEndorsementTableAdapters.GetYachtPortsEndorsementTableAdapter ta4 = new GetYachtPortsEndorsementTableAdapters.GetYachtPortsEndorsementTableAdapter();
            ReportDataSource rpd4 = new ReportDataSource();
            rpd4 = new ReportDataSource("GetYachtPortsEndorsement", (DataTable)ta4.GetData(taskControl1));
            //-------------------------------------------------GetYachtPortsEndorsement------------------------------------------------------------------------------------

            //-------------------------------------------------YachtDecPage------------------------------------------------------------------------------------
            GetYachtDecPageReportTableAdapters.GetYachtDecPageReportTableAdapter ta5 = new GetYachtDecPageReportTableAdapters.GetYachtDecPageReportTableAdapter();
            ReportDataSource rpd5 = new ReportDataSource();
            rpd5 = new ReportDataSource("GetYachtDecPageReport", (DataTable)ta5.GetData(taskControl1));

            GetTenderLimitDecPageByTaskControlIDTableAdapters.GetTenderLimitDecPageByTaskControlIDTableAdapter ta6 = new GetTenderLimitDecPageByTaskControlIDTableAdapters.GetTenderLimitDecPageByTaskControlIDTableAdapter();
            ReportDataSource rpd6 = new ReportDataSource();
            rpd6 = new ReportDataSource("GetTenderLimitDecPageByTaskControlID", (DataTable)ta6.GetData(taskControl1));



            //-------------------------------------------------YachtDecPage------------------------------------------------------------------------------------

            var QRTaskcontrolID = taskControl1.ToString();

            if (!string.IsNullOrEmpty(QRTaskcontrolID))
                GenerateQRCode(ref QRTaskcontrolID);

            ReportParameter[] parameterQR = new ReportParameter[1];
            ReportParameter[] parameters = new ReportParameter[6];
            ReportParameter[] parametersDecPage = new ReportParameter[8];
            Uri pathasUri = null;
            if (QRTaskcontrolID != "")
            {
                pathasUri = new Uri(QRTaskcontrolID);
                parameterQR[0] = new ReportParameter("QRCode", pathasUri.AbsoluteUri);
            }
            else
            {
                parameterQR[0] = new ReportParameter("QRCode", QRTaskcontrolID != "" ? QRTaskcontrolID : "");
            }
            ReportViewer viewer1 = new ReportViewer();
            viewer1.LocalReport.DataSources.Clear();
            viewer1.ProcessingMode = ProcessingMode.Local;
            viewer1.LocalReport.EnableExternalImages = true;
            viewer1.LocalReport.ReportPath = Server.MapPath("Reports/Yacht/" + rdlcname); //Reports/Yacht/YachtNewQuote.rdlc


            if (rdlcname == "YachtEndorsmentA.rdlc")
            {
                viewer1.LocalReport.DataSources.Add(rpd);
                viewer1.LocalReport.DataSources.Add(rpd6);
            }

            if (rdlcname == "YachtDecPage.rdlc")
            {
                //  viewer1.LocalReport.DataSources.Add(rpd1);
                viewer1.LocalReport.DataSources.Add(rpd5);
                viewer1.LocalReport.DataSources.Add(rpd6);
                parametersDecPage[0] = new ReportParameter("DeductibleCalculated", lblDeductibleCalculated1.Text);
                string firstOtherMedicalPayment, secondOtherMedicalPayment, firstMedicalPayment, secondMedicalPayment;
                string[] arrayOfOtherMedical = { "", "" };
                string[] arrayOfMedicalPayment = { "", "" };

                if (txtOtherMedicalPayment.Text.Trim() == "")
                {
                    parametersDecPage[1] = new ReportParameter("FirstOtherMedicalPayment", "");
                    parametersDecPage[2] = new ReportParameter("SecondOtherMedicalPayment", "");
                }
                else
                {
                    arrayOfOtherMedical = txtOtherMedicalPayment.Text.Split('/');
                    firstOtherMedicalPayment = arrayOfOtherMedical[0].Trim().Replace("$", "").Replace(",", "");
                    secondOtherMedicalPayment = arrayOfOtherMedical[1].Trim().Replace("$", "").Replace(",", "");
                    double firstOtherMedicalPaymentDouble = double.Parse(firstOtherMedicalPayment);
                    double secondOtherMedicalPaymentDouble = double.Parse(secondOtherMedicalPayment);
                    int firstOtherMedicalPaymentInt = (int)firstOtherMedicalPaymentDouble;
                    int secondOtherMedicalPaymentInt = (int)secondOtherMedicalPaymentDouble;
                    parametersDecPage[1] = new ReportParameter("FirstOtherMedicalPayment", firstOtherMedicalPaymentInt.ToString("C0"));
                    parametersDecPage[2] = new ReportParameter("SecondOtherMedicalPayment", secondOtherMedicalPaymentInt.ToString("C0"));
                }

                if (ddlMedicalPayment.SelectedItem.Text == "")
                {
                    parametersDecPage[3] = new ReportParameter("FirstMedicalPayment", "");
                    parametersDecPage[4] = new ReportParameter("SecondMedicalPayment", "");
                }
                else
                {
                    arrayOfMedicalPayment = ddlMedicalPayment.SelectedItem.Text.Split('/');
                    firstMedicalPayment = arrayOfMedicalPayment[0].Trim().Replace("$", "").Replace(",", "");
                    secondMedicalPayment = arrayOfMedicalPayment[1].Trim().Replace("$", "").Replace(",", "");
                    double firstMedicalPaymentDouble = double.Parse(firstMedicalPayment);
                    double secondMedicalPaymentDouble = double.Parse(secondMedicalPayment);
                    int firstMedicalPaymentInt = (int)firstMedicalPaymentDouble;
                    int secondMedicalPaymentInt = (int)secondMedicalPaymentDouble;
                    parametersDecPage[3] = new ReportParameter("FirstMedicalPayment", firstMedicalPaymentInt.ToString("C0"));
                    parametersDecPage[4] = new ReportParameter("SecondMedicalPayment", secondMedicalPaymentInt.ToString("C0"));
                }
                calculatelblDeductibleCalculatedTender();
                parametersDecPage[5] = new ReportParameter("DeductibleCalculatedTender", lblDeductibleCalculatedTender1.Text);
                calculatelblDeductibleCalculatedTender2();
                parametersDecPage[6] = new ReportParameter("DeductibleCalculatedTender2", lblDeductibleCalculatedTender2.Text);

                DataTable dtNavigationLimit = taskControl.NavigationLimitCollection;
                string[] navigationcollection;
                if (dtNavigationLimit.Rows.Count == 0)
                {

                    navigationcollection = new string[1];
                    navigationcollection[0] = "";
                }
                else
                {
                    navigationcollection = new string[dtNavigationLimit.Rows.Count];
                    for (int i = 0; i < dtNavigationLimit.Rows.Count; i++)
                    {
                        navigationcollection[i] = dtNavigationLimit.Rows[i]["NavigationLimitDesc"].ToString();
                    }
                }
                parametersDecPage[7] = new ReportParameter("NavigationLimitCollection", navigationcollection);
                viewer1.LocalReport.SetParameters(parametersDecPage);
            }

            if (rdlcname == "YachtEndorsementB.rdlc")
            {
                viewer1.LocalReport.DataSources.Add(rpd2);
            }

            if (rdlcname == "YachtCertificateLiability.rdlc" || rdlcname == "YachtCertificateLiabilityBanco.rdlc" || rdlcname == "YachtCertificateLiabilityMarina.rdlc" || rdlcname == "YachtCertificateLiabilityMarinaAddInsured.rdlc")
            {
                viewer1.LocalReport.DataSources.Add(rpd1);
                viewer1.LocalReport.DataSources.Add(rpd3);
                viewer1.LocalReport.DataSources.Add(rpd6);
                string firstOtherMedicalPayment, secondOtherMedicalPayment, firstMedicalPayment, secondMedicalPayment, firstDeductible, secondDeductible;
                string[] arrayOfOtherMedical = { "", "" };
                string[] arrayOfMedicalPayment = { "", "" };
                string[] arrayOfDeductible = { "", "" };

                if (txtOtherMedicalPayment.Text.Trim() == "")
                {
                    parameters[0] = new ReportParameter("FirstOtherMedicalPayment", "");
                    parameters[1] = new ReportParameter("SecondOtherMedicalPayment", "");
                }
                else
                {
                    arrayOfOtherMedical = txtOtherMedicalPayment.Text.Split('/');
                    firstOtherMedicalPayment = arrayOfOtherMedical[0].Trim().Replace("$", "").Replace(",", "");
                    secondOtherMedicalPayment = arrayOfOtherMedical[1].Trim().Replace("$", "").Replace(",", "");
                    double firstOtherMedicalPaymentDouble = double.Parse(firstOtherMedicalPayment);
                    double secondOtherMedicalPaymentDouble = double.Parse(secondOtherMedicalPayment);
                    int firstOtherMedicalPaymentInt = (int)firstOtherMedicalPaymentDouble;
                    int secondOtherMedicalPaymentInt = (int)secondOtherMedicalPaymentDouble;
                    parameters[0] = new ReportParameter("FirstOtherMedicalPayment", firstOtherMedicalPaymentInt.ToString("C0"));
                    parameters[1] = new ReportParameter("SecondOtherMedicalPayment", secondOtherMedicalPaymentInt.ToString("C0"));
                }

                if (ddlMedicalPayment.SelectedItem.Text == "")
                {
                    parameters[2] = new ReportParameter("FirstMedicalPayment", "");
                    parameters[3] = new ReportParameter("SecondMedicalPayment", "");
                }
                else
                {
                    arrayOfMedicalPayment = ddlMedicalPayment.SelectedItem.Text.Split('/');
                    firstMedicalPayment = arrayOfMedicalPayment[0].Trim().Replace("$", "").Replace(",", "");
                    secondMedicalPayment = arrayOfMedicalPayment[1].Trim().Replace("$", "").Replace(",", "");
                    double firstMedicalPaymentDouble = double.Parse(firstMedicalPayment);
                    double secondMedicalPaymentDouble = double.Parse(secondMedicalPayment);
                    int firstMedicalPaymentInt = (int)firstMedicalPaymentDouble;
                    int secondMedicalPaymentInt = (int)secondMedicalPaymentDouble;
                    parameters[2] = new ReportParameter("FirstMedicalPayment", firstMedicalPaymentInt.ToString("C0"));
                    parameters[3] = new ReportParameter("SecondMedicalPayment", secondMedicalPaymentInt.ToString("C0"));
                }
                if (ddlDeductibles1.SelectedIndex != 0)
                {
                    DataTable dt = GetDeductibleAmount(int.Parse(ddlDeductibles1.SelectedItem.Value));
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                        {
                            parameters[4] = new ReportParameter("FirstDeductible", ddlDeductibles1.SelectedItem.Text);
                            parameters[5] = new ReportParameter("SecondDeductible", "");
                        }
                        else
                        {
                            arrayOfDeductible = ddlDeductibles1.SelectedItem.Text.Split('/');
                            firstDeductible = arrayOfDeductible[0].Trim();
                            secondDeductible = arrayOfDeductible[1].Trim();
                            parameters[4] = new ReportParameter("FirstDeductible", firstDeductible);
                            parameters[5] = new ReportParameter("SecondDeductible", secondDeductible);
                        }
                    }
                    else
                    {
                        parameters[4] = new ReportParameter("FirstDeductible", "");
                        parameters[5] = new ReportParameter("SecondDeductible", "");
                    }
                }
                else
                {
                    parameters[4] = new ReportParameter("FirstDeductible", "");
                    parameters[5] = new ReportParameter("SecondDeductible", "");
                }
                viewer1.LocalReport.SetParameters(parameters);
            }

            if (rdlcname == "YachtPortEndorsement.rdlc" || rdlcname == "YachtPDREndorsement.rdlc" || rdlcname ==  "YachtMPCEndorsement.rdlc")
            {
                viewer1.LocalReport.DataSources.Add(rpd4);
            }

            viewer1.LocalReport.Refresh();

            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string filetype = string.Empty;

            string RandomString = Guid.NewGuid().ToString();
            string fileName1 = "";
            string _FileName1 = "";

            if (rdlcname == "YachtEndorsmentA.rdlc")
            {
                fileName1 = "YachtEndorsmentA-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtEndorsmentA-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }
            else if (rdlcname == "YachtDecPage.rdlc")
            {
                fileName1 = "YachtDecPage-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtDecPage-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }
            else if (rdlcname == "YachtImportantChangesPolicy.rdlc")
            {
                fileName1 = "YachtImportantChangesPolicy-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtImportantChangesPolicy-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }
            else if (rdlcname == "YachtEndorsementB.rdlc")
            {
                fileName1 = "YachtEndorsementB-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtEndorsementB-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }
            else if (rdlcname == "YachtCertificateLiability.rdlc")
            {
                fileName1 = "YachtCertificateLiability-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtCertificateLiability-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }
            else if (rdlcname == "YachtPortEndorsement.rdlc")
            {
                fileName1 = "YachtPortEndorsement-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtPortEndorsement-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }

            else if (rdlcname == "YachtPDREndorsement.rdlc")
            {
                fileName1 = "YachtPDREndorsement-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtPDREndorsement-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }

            else if (rdlcname == "YachtMPCEndorsement.rdlc")
            {
                fileName1 = "YachtMPCEndorsement-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtMPCEndorsement-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }

            else if (rdlcname == "YachtCertificateLiabilityBanco.rdlc")
            {
                fileName1 = "YachtCertificateLiabilityBanco-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtCertificateLiabilityBanco-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }

            else if (rdlcname == "YachtCertificateLiabilityMarina.rdlc")
            {
                fileName1 = "YachtCertificateLiabilityMarina-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtCertificateLiabilityMarina-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }

            else if (rdlcname == "YachtCertificateLiabilityMarinaAddInsured.rdlc")
            {
                fileName1 = "YachtCertificateLiabilityMarinaAddInsured-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtCertificateLiabilityMarinaAddInsured-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }

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

    protected void PrintYachtQuote(string rdlcname)
    {
        try
        {
            FileInfo mFileIndex;
            List<string> mergePaths = new List<string>();
            string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
            int tc_id = taskControl.TaskControlID;


            if (taskControl.isQuote)
            {
                mergePaths = ImprimirQuote(mergePaths, rdlcname);
            }
            else if (taskControl.isQuote == false && rdlcname != "YachtCertificateLiability.rdlc" && rdlcname != "YachtCertificateLiabilityBanco.rdlc" && rdlcname != "YachtCertificateLiabilityMarina.rdlc" && rdlcname != "YachtCertificateLiabilityMarinaAddInsured.rdlc" && rdlcname != "YachtImportantChangesPolicy.rdlc")
            {
                if (rdlcname == "YachtPortEndorsement.rdlc" || rdlcname == "YachtPDREndorsement.rdlc" || rdlcname == "YachtMPCEndorsement.rdlc")
                {
                    mergePaths = ImprimirPolicy(mergePaths, rdlcname);
                }
                else
                {
                    mergePaths = ImprimirPolicy(mergePaths, "YachtDecPage.rdlc");
                    if (taskControl.HullLimit != "")
                        mergePaths = ImprimirPolicy(mergePaths, "YachtEndorsmentA.rdlc");
                    if (taskControl.BankPPSID.Trim() != "000")
                    {
                        mergePaths = ImprimirPolicy(mergePaths, "YachtEndorsementB.rdlc");
                    }
                    if (ddlHomeport.SelectedIndex != 0)
                    {
                    if (GetIsMandatoryHomeport(ddlHomeport.SelectedItem.Value) == true)
                    {
                        mergePaths = ImprimirPolicy(mergePaths, GetHomeportReportName(ddlHomeport.SelectedItem.Value,true));
                        //mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/Yacht/YachtPortImportantNotice.pdf");
                        //mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "YachtPortImportantNotice" + taskControl.TaskControlID.ToString() + ".pdf", true);
                        //mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "YachtPortImportantNotice" + taskControl.TaskControlID.ToString() + ".pdf");
                    }}
                    
                }
            }
            else if (rdlcname == "YachtImportantChangesPolicy.rdlc")
            {
                mergePaths = ImprimirPolicy(mergePaths, "YachtImportantChangesPolicy.rdlc");
                mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/Yacht/YachtFormDefinitionsAndCoverages.pdf");
                mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "YachtFormDefinitionsAndCoverages" + taskControl.TaskControlID.ToString() + ".pdf", true);
                mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "YachtFormDefinitionsAndCoverages" + taskControl.TaskControlID.ToString() + ".pdf");
                mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/Yacht/YachtMandatoryPremiumEndorsement.pdf");
                mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "YachtMandatoryPremiumEndorsement" + taskControl.TaskControlID.ToString() + ".pdf", true);
                mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "YachtMandatoryPremiumEndorsement" + taskControl.TaskControlID.ToString() + ".pdf");
                mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/Yacht/YachtPortImportantNotice.pdf");
                mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "YachtPortImportantNotice" + taskControl.TaskControlID.ToString() + ".pdf", true);
                mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "YachtPortImportantNotice" + taskControl.TaskControlID.ToString() + ".pdf");
                if (DateTime.Parse(taskControl.EffectiveDate) > DateTime.Parse("03/31/2020"))
                {
                    mFileIndex = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["ReportPathName"] + "/Yacht/YachtFormDefinitionsAndCoverages2.pdf");
                    mFileIndex.CopyTo(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "YachtFormDefinitionsAndCoverages2" + taskControl.TaskControlID.ToString() + ".pdf", true);
                    mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + "YachtFormDefinitionsAndCoverages2" + taskControl.TaskControlID.ToString() + ".pdf");
                }
                
            }
            else if (rdlcname == "YachtCertificateLiability.rdlc")
            {
                mergePaths = ImprimirPolicy(mergePaths, "YachtCertificateLiability.rdlc");
            }
            else if (rdlcname == "YachtCertificateLiabilityBanco.rdlc")
            {
                mergePaths = ImprimirPolicy(mergePaths, "YachtCertificateLiabilityBanco.rdlc");
            }
            else if (rdlcname == "YachtCertificateLiabilityMarina.rdlc")
            {
                mergePaths = ImprimirPolicy(mergePaths, "YachtCertificateLiabilityMarina.rdlc");
            }
            else if (rdlcname == "YachtCertificateLiabilityMarinaAddInsured.rdlc")
            {
                mergePaths = ImprimirPolicy(mergePaths, "YachtCertificateLiabilityMarinaAddInsured.rdlc");
            }

            //Generar PDF
            OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
            string FinalFile = "";
            FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());

            System.Diagnostics.Debug.Write("");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);

        }
    }
    private void GenerateQRCode(ref string Values)
    {

        EncryptClass.EncryptClass encryptClass = new EncryptClass.EncryptClass();
        Values = Values + "&q";
        Values = encryptClass.Encrypt(Values);
        string FileNameLogo = "QR" + Values.ToString() + ".png";
        string qrcodeValue = Values;

        //Values = "https://epps.guardianinsurance.com/ValidInfoQrCode.aspx?Tc=" + Values;
        //Values = "http://epps-test.guardianinsurance.com/ValidInfoQrCode.aspx?isPolicy=q&Tc=" + qrcodeValue;
        //Values = "http://epps-test.guardianinsurance.com/ValidInfoQrCode.aspx?Tc="+qrcodeValue +"&isPolicy=q";
        Values = "http://192.168.21.11:84/ValidInfoQrCode.aspx?Tc=" + qrcodeValue;

        //decryptTransValue = encryptClass.Decrypt(Transaction.Trim());

        string imgPath = System.Web.Hosting.HostingEnvironment.MapPath("~/QRCodes/");


        var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chs={1}x{2}&chl={0}", Values, "500", "500");
        WebResponse response = default(WebResponse);
        Stream remoteStream = default(Stream);
        StreamReader readStream = default(StreamReader);
        WebRequest request = WebRequest.Create(url);
        response = request.GetResponse();
        remoteStream = response.GetResponseStream();
        readStream = new StreamReader(remoteStream);
        System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
        CleanillegalChar(ref FileNameLogo);
        img.Save(imgPath + FileNameLogo);
        Values = imgPath + FileNameLogo;
        response.Close();
        remoteStream.Close();
        readStream.Close();
    }

    private string CleanillegalChar(ref string messyText)
    {
        Regex regex = new Regex(@"[\s,:;/\\|]+");
        messyText = regex.Replace(messyText, "").Replace("\"", "");
        return messyText;
    }

    private string CleaningEverythingButSlash(string messyText)
    {
        string[] array;
        array = new string[52] {"q","w","e","r","t","y","u","i","o","p","a","s","d","f","g","h","j","k","l",
            "z","x","c","v","b","n","m","{","}","'","!","@","#","%","^","&","*","(",")","-","_","=","?",
            "[","\\",",",":",";","|","]","+","`","~"};

        for (int i = 0; i < array.Length; i++)
        {
            messyText = messyText.Replace(array[i], "");
        }
        return messyText;
    }

    //private string CleaningEverythingButapostrophe(string messyText)
    //{
    //    string[] array;
    //    array = new string[52] {"q","w","e","r","t","y","u","i","o","p","a","s","d","f","g","h","j","k","l",
    //        "z","x","c","v","b","n","m","{","}","/","!","@","#","%","^","&","*","(",")","-","_","=","?",
    //        "[","\\",",",":",";","|","]","+","`","~"};

    //    for (int i = 0; i < array.Length; i++)
    //    {
    //        messyText = messyText.Replace(array[i], "");
    //    }
    //    return messyText;
    //}

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        PrintYachtQuote("YachtNewQuote.rdlc");
    }

    protected void btnPreview2_Click(object sender, EventArgs e)
    {
        PrintYachtQuote("PrivateYachtInsuranceQuote.rdlc");
    }

    public void SendPolicyToPPSTender2(int TaskControlID)
    {
        string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();

        SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand();
        DataTable PPSPolicy = new DataTable();
        DataTable dt = GetYachtToPPSByTaskControlIDInfoGeneral(TaskControlID);
        DataTable dtTender = GetYachtToPPSByTaskControlIDInfoTenderLimit(TaskControlID);
        DataTable dtTenderDesc = GetYachtToPPSByTaskControlIDInfoTenderLimitDesc(TaskControlID);
        DataTable dtSurvey = GetYachtToPPSByTaskControlIDInfoSurvey(TaskControlID);
        DataTable dtReport = GetYachtToPPSByTaskControlIDInfoReport(TaskControlID);
        DataTable dtNavigation = GetNavigationLimitCollectionToPPSByTaskControlID(TaskControlID);
        System.Data.DataTable dtReinsAsl = new System.Data.DataTable();
        System.Data.DataTable dtOtherCvrgDetail = new System.Data.DataTable();
        string CoverageCodesXml = "";
        string OtherCvrgDetailXml = "";

        try
        {
            dtReinsAsl.TableName = "Coverages";
            dtReinsAsl.Columns.Add("ReinsAsl");
            dtReinsAsl.Columns.Add("Desc");
            dtReinsAsl.Columns.Add("CoveragePremium");
            dtReinsAsl.Columns.Add("Total");
            dtReinsAsl.Columns.Add("Lim1");
            dtReinsAsl.Columns.Add("Lim2");
            dtReinsAsl.Columns.Add("Lim3");
            dtReinsAsl.Columns.Add("Lim4");
            dtReinsAsl.Columns.Add("Lim5");
            dtReinsAsl.Columns.Add("Deductible");
            dtReinsAsl.Columns.Add("MinDeductible");

            DataRow row = dtReinsAsl.NewRow(); //Tender Limit con primer deducible
            row["ReinsAsl"] = "66080";
            row["Desc"] = "";
            row["CoveragePremium"] = "0.00";
            row["Total"] = "";
            if (dtTender.Rows.Count > 0)
            {
                if (dtTender.Rows[0]["TenderLimitAmount2"].ToString() == "")
                {
                    row["Lim1"] = "0.00";
                }
                else
                {
                    row["Lim1"] = dtTender.Rows[0]["TenderLimitAmount2"].ToString().Replace("$", "").Replace(",", "").Trim();
                }
                row["Lim2"] = "0.00";
                row["Lim3"] = "0.00";
                if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
                {
                    row["Lim4"] = "0.00";
                    row["Lim5"] = "0.00";
                    row["Deductible"] = "0.00";
                    row["MinDeductible"] = "0.00";
                }
                else
                {
                    string[] arrayOfDeductible3 = { "", "" };
                    string deductible3 = "";
                    arrayOfDeductible3 = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                    deductible3 = arrayOfDeductible3[0].Trim().Replace("%", "");
                    double ded3 = double.Parse(deductible3) / 100.00;
                    row["Lim4"] = ded3.ToString();
                    row["Lim5"] = "0.00";
                    row["Deductible"] = ded3.ToString();
                    row["MinDeductible"] = "0.00";
                }

            }
            else
            {
                row["Lim1"] = "0.00";
                row["Lim2"] = "0.00";
                row["Lim3"] = "0.00";
                row["Lim4"] = "0.00";
                row["Lim5"] = "0.00";
                row["Deductible"] = "0.00";
                row["MinDeductible"] = "0.00";
            }

            dtReinsAsl.Rows.Add(row);

            DataRow row2 = dtReinsAsl.NewRow(); //Tender Limit con segundo deducible
            row2["ReinsAsl"] = "63080";
            row2["Desc"] = "";
            row2["CoveragePremium"] = "0.00";
            row2["Total"] = "";
            if (dtTender.Rows.Count > 0)
            {
                if (dtTender.Rows[0]["TenderLimitAmount2"].ToString() == "")
                {
                    row2["Lim1"] = "0.00";
                }
                else
                {
                    row2["Lim1"] = dtTender.Rows[0]["TenderLimitAmount2"].ToString().Replace("$", "").Replace(",", "").Trim();
                }
                row2["Lim2"] = "0.00";
                row2["Lim3"] = "0.00";
                if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
                {
                    row2["Lim4"] = "0.00";
                    row2["Lim5"] = "0.00";
                    row2["Deductible"] = "0.00";
                    row2["MinDeductible"] = "0.00";
                }
                else
                {
                    string[] arrayOfDeductible4 = { "", "" };
                    string deductible4 = "";
                    arrayOfDeductible4 = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                    if (arrayOfDeductible4.Length > 1)
                    {

                        deductible4 = arrayOfDeductible4[1].Trim().Replace("%", "");
                        double ded4 = double.Parse(deductible4) / 100.00;
                        row2["Lim4"] = ded4.ToString();
                        row2["Lim5"] = "0.00";
                        row2["Deductible"] = ded4.ToString();
                        row2["MinDeductible"] = "0.00";
                    }
                    else
                    {
                        row2["Lim4"] = "0.00";
                        row2["Lim5"] = "0.00";
                        row2["Deductible"] = "0.00";
                        row2["MinDeductible"] = "0.00";
                    }
                }


            }
            else
            {
                row2["Lim1"] = "0.00";
                row2["Lim2"] = "0.00";
                row2["Lim3"] = "0.00";
                row2["Lim4"] = "0.00";
                row2["Lim5"] = "0.00";
                row2["Deductible"] = "0.00";
                row2["MinDeductible"] = "0.00";
            }

            dtReinsAsl.Rows.Add(row2);

            dtOtherCvrgDetail.TableName = "OtherCvrgDetail";
            dtOtherCvrgDetail.Columns.Add("ReinsAsl");
            dtOtherCvrgDetail.Columns.Add("DisplayAs");
            dtOtherCvrgDetail.Columns.Add("IndexNo");
            dtOtherCvrgDetail.Columns.Add("Limit1");
            dtOtherCvrgDetail.Columns.Add("Limit2");
            dtOtherCvrgDetail.Columns.Add("Tag");

            if (dtReinsAsl.Rows.Count > 0)
            {
                for (int i = 0; i < dtReinsAsl.Rows.Count; i++)
                {
                    int Count = 1;
                    DataRow row8 = dtOtherCvrgDetail.NewRow();
                    switch (dtReinsAsl.Rows[i]["ReinsAsl"].ToString())
                    {

                        case "66080": //Tender Limit con primer deducible
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Tender Limit - All Other Perils";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "63080": //Tender Limit con segundo deducible
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Tender Limit - Windstorm";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;
                    }
                }
            }

            using (StringWriter sw = new StringWriter())
            {
                dtReinsAsl.WriteXml(sw);
                CoverageCodesXml = sw.ToString();
                XmlDocument docSave = new XmlDocument();
                docSave.LoadXml(sw.ToString());
                docSave.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + TaskControlID + "_OtherCvrg2" + ".xml");
                sw.Close();
            }

            using (StringWriter sw = new StringWriter())
            {
                dtOtherCvrgDetail.WriteXml(sw);
                OtherCvrgDetailXml = sw.ToString();
                XmlDocument docSave = new XmlDocument();
                docSave.LoadXml(sw.ToString());
                docSave.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + TaskControlID + "_OtherCvrgDetail2" + ".xml");
                sw.Close();
            }


            if (dt.Rows.Count > 0)
            {

                cmd.CommandText = "sproc_ConsumeXMLePPS-YACHT_AddTenderLimit2";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();


                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Tag", "true");
                // Add the parameters for Bldgs
                cmd.Parameters.AddWithValue("@PolicyID", "MAR" + txtPolicyNo.Text);
                cmd.Parameters.AddWithValue("@Descrip", SqlDbType.NVarChar).Value = DBNull.Value;
                if (dt.Rows[0]["HomeportDesc"].ToString().Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@Location", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Location", dt.Rows[0]["HomeportDesc"].ToString().Trim());
                }
                cmd.Parameters.AddWithValue("@Island", "4");

                double tender2;

                if (dtTender.Rows.Count > 0 && dtTender.Rows[0]["TenderLimitAmount2"].ToString().Replace("$", "").Replace(",", "").Trim() != "")
                {
                    tender2 = double.Parse(dtTender.Rows[0]["TenderLimitAmount2"].ToString().Replace("$", "").Replace(",", "").Trim());
                }
                else
                {
                    tender2 = 0.00;
                }

                cmd.Parameters.AddWithValue("@InsVal", tender2.ToString());
                cmd.Parameters.AddWithValue("@AnyNum", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@PayeeID", dt.Rows[0]["BankPPSID"].ToString().Trim());
                cmd.Parameters.AddWithValue("@LoanNo", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@PayeeID2", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@LoanNo2", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Families", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@RowHouse", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Rented", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Construction", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@ProtectionClass", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@YearBuilt", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@FireDistrict", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Occupancy", SqlDbType.NVarChar).Value = DBNull.Value;
                string navigationLimitAll = "";
                if (dtNavigation.Rows.Count == 1)
                {
                    cmd.Parameters.Add("@NavLimit", dtNavigation.Rows[0]["NavigationLimitDesc"].ToString().Trim());
                }
                else if (dtNavigation.Rows.Count > 1)
                {
                    for (int i = 0; i < dtNavigation.Rows.Count; i++)
                    {
                        if (i == 0)
                            navigationLimitAll = dtNavigation.Rows[i]["NavigationLimitDesc"].ToString().Trim();
                        else
                            navigationLimitAll = navigationLimitAll + " " + dtNavigation.Rows[i]["NavigationLimitDesc"].ToString().Trim();
                    }

                    cmd.Parameters.Add("@NavLimit", navigationLimitAll);
                }
                else
                {
                    cmd.Parameters.Add("@NavLimit", SqlDbType.NVarChar).Value = DBNull.Value;

                }
                if (dtTender.Rows.Count > 0)
                {
                    if (dtTender.Rows[0]["TenderDesc2"].ToString().Trim() != "" && dtTender.Rows[0]["TenderSerial2"].ToString().Trim() != "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderDesc2"].ToString().Trim() + " " + dtTender.Rows[0]["TenderSerial2"].ToString().Trim());
                    else if (dtTender.Rows[0]["TenderDesc2"].ToString().Trim() != "" && dtTender.Rows[0]["TenderSerial2"].ToString().Trim() == "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderDesc2"].ToString().Trim());
                    else if (dtTender.Rows[0]["TenderDesc2"].ToString().Trim() == "" && dtTender.Rows[0]["TenderSerial2"].ToString().Trim() != "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderSerial2"].ToString().Trim());
                    else
                        cmd.Parameters.Add("@TenderText", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@TenderText", SqlDbType.NVarChar).Value = DBNull.Value;
                }

                cmd.Parameters.Add("@TrailerText", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@VName", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@Make", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@Model", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@HIN", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@LOA", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@VesselProp", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Storeys", SqlDbType.NVarChar).Value = DBNull.Value;
                //OtherCoverage & OtherCovrgDetail
                cmd.Parameters.AddWithValue("@CoverageCodesXml", CoverageCodesXml);
                cmd.Parameters.AddWithValue("@OtherCvrgDetailXml", OtherCvrgDetailXml);

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(PPSPolicy);

                //cmd.ExecuteReader();
            }

            sqlConnection1.Close();
        }
        catch (Exception ex)
        {
            LogError(ex);
            sqlConnection1.Close();
        }
    }

    public void SendPolicyToPPS(int TaskControlID)
    {
        string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();

        SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand();
        DataTable PPSPolicy = new DataTable();
        DataTable dt = GetYachtToPPSByTaskControlIDInfoGeneral(TaskControlID);
        DataTable dtTender = GetYachtToPPSByTaskControlIDInfoTenderLimit(TaskControlID);
        DataTable dtTenderDesc = GetYachtToPPSByTaskControlIDInfoTenderLimitDesc(TaskControlID);
        DataTable dtSurvey = GetYachtToPPSByTaskControlIDInfoSurvey(TaskControlID);
        DataTable dtReport = GetYachtToPPSByTaskControlIDInfoReport(TaskControlID);
        DataTable dtNavigation = GetNavigationLimitCollectionToPPSByTaskControlID(TaskControlID);
        System.Data.DataTable dtReinsAsl = new System.Data.DataTable();
        System.Data.DataTable dtOtherCvrgDetail = new System.Data.DataTable();
        string CoverageCodesXml = "";
        string OtherCvrgDetailXml = "";

        try
        {
            dtReinsAsl.TableName = "Coverages";
            dtReinsAsl.Columns.Add("ReinsAsl");
            dtReinsAsl.Columns.Add("Desc");
            dtReinsAsl.Columns.Add("CoveragePremium");
            dtReinsAsl.Columns.Add("Total");
            dtReinsAsl.Columns.Add("Lim1");
            dtReinsAsl.Columns.Add("Lim2");
            dtReinsAsl.Columns.Add("Lim3");
            dtReinsAsl.Columns.Add("Lim4");
            dtReinsAsl.Columns.Add("Lim5");
            dtReinsAsl.Columns.Add("Deductible");
            dtReinsAsl.Columns.Add("MinDeductible");

            DataRow row = dtReinsAsl.NewRow(); //Medical Payments
            row["ReinsAsl"] = "59080";
            row["Desc"] = "";
            row["CoveragePremium"] = dt.Rows[0]["MedicalPaymentPremiumTotal"].ToString().Replace("$", "").Replace(",", "").Trim();
            row["Total"] = "";
            string firstMedicalPayment = "", secondMedicalPayment = "";
            string[] arrayOfOtherMedical = { "", "" };
            string[] arrayOfMedicalPayment = { "", "" };

            if (txtOtherMedicalPayment.Text.Trim() == "" && ddlMedicalPayment.SelectedItem.Text == "")
            {
                firstMedicalPayment = "";
                secondMedicalPayment = "";
            }
            else if (ddlMedicalPayment.SelectedItem.Text == "")
            {
                arrayOfOtherMedical = txtOtherMedicalPayment.Text.Split('/');
                firstMedicalPayment = arrayOfOtherMedical[0].Trim().Replace("$", "").Replace(",", "");
                secondMedicalPayment = arrayOfOtherMedical[1].Trim().Replace("$", "").Replace(",", "");
            }

            else if (txtOtherMedicalPayment.Text.Trim() == "")
            {
                arrayOfMedicalPayment = ddlMedicalPayment.SelectedItem.Text.Split('/');
                firstMedicalPayment = arrayOfMedicalPayment[0].Trim().Replace("$", "").Replace(",", "");
                secondMedicalPayment = arrayOfMedicalPayment[1].Trim().Replace("$", "").Replace(",", "");
            }
            row["Lim1"] = firstMedicalPayment;
            row["Lim2"] = secondMedicalPayment;
            row["Lim3"] = "0.00";
            row["Lim4"] = firstMedicalPayment;
            row["Lim5"] = "0.00";
            row["Deductible"] = "0.00";
            row["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row);

            DataRow row2 = dtReinsAsl.NewRow(); //Hull Limit con segundo deducible
            row2["ReinsAsl"] = "58080";
            row2["Desc"] = "";
            if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() == "")
                row2["CoveragePremium"] = "0.00";
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() == "")
                row2["CoveragePremium"] = (double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.40).ToString();
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() == "")
                row2["CoveragePremium"] = (double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.40).ToString();
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() == "")
            {
                double num1 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2) * 0.40;
                row2["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() != "")
            {
                double num1 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2) * 0.40;
                row2["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() != "")
            {
                double num1 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2) * 0.40;
                row2["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() != "")
            {
                double num1 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num3 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2 + num3) * 0.40;
                row2["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() != "")
                row2["CoveragePremium"] = (double.Parse(dt.Rows[0]["WatercraftLimitTotal"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.40).ToString();
            row2["Total"] = "";
            row2["Lim1"] = dt.Rows[0]["HullLimit"].ToString().Replace("$", "").Replace(",", "").Trim();
            row2["Lim2"] = "0.00";
            row2["Lim3"] = "0.00";
            string[] arrayOfDeductible = { "", "" };
            string deductible2 = "";
            if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
            {
                row2["Lim4"] = "0.00";
            }
            else
            {
                arrayOfDeductible = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                if (arrayOfDeductible.Length > 1)
                {
                    deductible2 = arrayOfDeductible[1].Trim().Replace("%", "");
                    double ded2 = double.Parse(deductible2) / 100.00;
                    row2["Lim4"] = ded2.ToString();
                }
                else
                {
                    row2["Lim4"] = "0.00";
                }
            }

            row2["Lim5"] = "0.00";
            if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
            {
                row2["Deductible"] = "0.00";
            }
            else
            {
                arrayOfDeductible = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                if (arrayOfDeductible.Length > 1)
                {
                    deductible2 = arrayOfDeductible[1].Trim().Replace("%", "");
                    double ded2 = double.Parse(deductible2) / 100.00;
                    row2["Deductible"] = ded2.ToString();
                }
                else
                {
                    row2["Deductible"] = "0.00";
                }
            }
            row2["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row2);

            DataRow row9 = dtReinsAsl.NewRow(); //Hull Limit con primer deducible
            row9["ReinsAsl"] = "60080";
            row9["Desc"] = "";
            if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() == "")
                row9["CoveragePremium"] = "0.00";
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() == "")
                row9["CoveragePremium"] = (double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.60).ToString();
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() == "")
                row9["CoveragePremium"] = (double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.60).ToString();
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() == "")
            {
                double num1 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2) * 0.60;
                row9["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() != "")
            {
                double num1 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2) * 0.60;
                row9["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() != "")
            {
                double num1 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2) * 0.60;
                row9["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() != "")
            {
                double num1 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num3 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2 + num3) * 0.60;
                row9["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal"].ToString().Trim() != "")
                row9["CoveragePremium"] = (double.Parse(dt.Rows[0]["WatercraftLimitTotal"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.60).ToString();
            row9["Total"] = "";
            row9["Lim1"] = dt.Rows[0]["HullLimit"].ToString().Replace("$", "").Replace(",", "").Trim();
            row9["Lim2"] = "0.00";
            row9["Lim3"] = "0.00";
            string[] arrayOfDeductible2 = { "", "" };
            string deductible1 = "";
            if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
            {
                row9["Lim4"] = "0.00";
            }
            else
            {
                arrayOfDeductible2 = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                deductible1 = arrayOfDeductible2[0].Trim().Replace("%", "");
                double ded1 = double.Parse(deductible1) / 100.00;
                row9["Lim4"] = ded1.ToString();
            }

            row9["Lim5"] = "0.00";
            if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
            {
                row9["Deductible"] = "0.00";
            }
            else
            {
                arrayOfDeductible2 = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                deductible1 = arrayOfDeductible2[0].Trim().Replace("%", "");
                double ded1 = double.Parse(deductible1) / 100.00;
                row9["Deductible"] = ded1.ToString();
            }
            row9["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row9);

            DataRow row3 = dtReinsAsl.NewRow(); //Protection & Indemnity
            row3["ReinsAsl"] = "61080";
            row3["Desc"] = "";
            row3["CoveragePremium"] = dt.Rows[0]["PIPremium"].ToString().Replace("$", "").Replace(",", "").Trim();
            row3["Total"] = "";
            string pi = "";
            if (ddlPI.SelectedItem.Text == "" && ddlPILiabilityOnly.SelectedItem.Text == "" && txtOtherPI.Text != "")
                pi = txtOtherPI.Text;
            else if (ddlPI.SelectedItem.Text == "" && ddlPILiabilityOnly.SelectedItem.Text != "" && txtOtherPI.Text == "")
                pi = ddlPILiabilityOnly.SelectedItem.Text;
            else if (ddlPI.SelectedItem.Text != "" && ddlPILiabilityOnly.SelectedItem.Text == "" && txtOtherPI.Text == "")
                pi = ddlPI.SelectedItem.Text;
            row3["Lim1"] = pi.ToString().Replace("$", "").Replace(",", "").Trim();
            row3["Lim2"] = "0.00";
            row3["Lim3"] = "0.00";
            row3["Lim4"] = pi.ToString().Replace("$", "").Replace(",", "").Trim();
            row3["Lim5"] = "0.00";
            row3["Deductible"] = "0.00";
            row3["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row3);

            DataRow row4 = dtReinsAsl.NewRow(); //Personal Effects
            row4["ReinsAsl"] = "62080";
            row4["Desc"] = "";
            row4["CoveragePremium"] = dt.Rows[0]["PersonalEffectsPremium"].ToString().Replace("$", "").Replace(",", "").Trim();
            row4["Total"] = "";
            row4["Lim1"] = dt.Rows[0]["PersonalEffects"].ToString().Replace("$", "").Replace(",", "").Trim();
            row4["Lim2"] = "0.00";
            row4["Lim3"] = "0.00";
            row4["Lim4"] = dt.Rows[0]["PEDeductible"].ToString().Replace("$", "").Replace(",", "").Trim();
            row4["Lim5"] = "0.00";
            row4["Deductible"] = dt.Rows[0]["PEDeductible"].ToString().Replace("$", "").Replace(",", "").Trim();
            row4["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row4);

            DataRow row5 = dtReinsAsl.NewRow(); //Tender Limit con primer deducible
            row5["ReinsAsl"] = "66080";
            row5["Desc"] = "";
            row5["CoveragePremium"] = "0.00";
            row5["Total"] = "";
            if (dtTender.Rows.Count > 0)
            {
                if (dtTender.Rows[0]["TenderLimitAmount1"].ToString() == "")
                    row5["Lim1"] = "0.00";
                else
                    row5["Lim1"] = dtTender.Rows[0]["TenderLimitAmount1"].ToString().Replace("$", "").Replace(",", "").Trim();
                row5["Lim2"] = "0.00";
                row5["Lim3"] = "0.00";
                if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
                {
                    row5["Lim4"] = "0.00";
                    row5["Lim5"] = "0.00";
                    row5["Deductible"] = "0.00";
                    row5["MinDeductible"] = "0.00";
                }
                else
                {
                    string[] arrayOfDeductible3 = { "", "" };
                    string deductible3 = "";
                    arrayOfDeductible3 = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                    deductible3 = arrayOfDeductible3[0].Trim().Replace("%", "");
                    double ded3 = double.Parse(deductible3) / 100.00;
                    row5["Lim4"] = ded3.ToString();
                    row5["Lim5"] = "0.00";
                    row5["Deductible"] = ded3.ToString();
                    row5["MinDeductible"] = "0.00";
                }

            }
            else
            {
                row5["Lim1"] = "0.00";
                row5["Lim2"] = "0.00";
                row5["Lim3"] = "0.00";
                row5["Lim4"] = "0.00";
                row5["Lim5"] = "0.00";
                row5["Deductible"] = "0.00";
                row5["MinDeductible"] = "0.00";
            }

            dtReinsAsl.Rows.Add(row5);

            DataRow row6 = dtReinsAsl.NewRow(); //Trailer
            row6["ReinsAsl"] = "65080";
            row6["Desc"] = "";
            row6["CoveragePremium"] = dt.Rows[0]["TrailerPremium"].ToString().Replace("$", "").Replace(",", "").Trim();
            row6["Total"] = "";
            row6["Lim1"] = dt.Rows[0]["Trailer"].ToString().Replace("$", "").Replace(",", "").Trim();
            row6["Lim2"] = "0.00";
            row6["Lim3"] = "0.00";
            row6["Lim4"] = dt.Rows[0]["Trailer"].ToString().Replace("$", "").Replace(",", "").Trim();
            row6["Lim5"] = "0.00";
            row6["Deductible"] = "0.00";
            row6["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row6);

            DataRow row7 = dtReinsAsl.NewRow(); //Uninsured Boater
            row7["ReinsAsl"] = "67080";
            row7["Desc"] = "";
            row7["CoveragePremium"] = dt.Rows[0]["OtherUninsuredBoaterPremium"].ToString().Replace("$", "").Replace(",", "").Trim();
            row7["Total"] = "";
            string uninsuredBoater = "";
            if (ddlUninsuredBoaters.SelectedItem.Text == "" && txtOtherUninsuredBoater.Text != "")
                uninsuredBoater = txtOtherUninsuredBoater.Text;
            else if (ddlUninsuredBoaters.SelectedItem.Text != "" && txtOtherUninsuredBoater.Text == "")
                uninsuredBoater = ddlUninsuredBoaters.SelectedItem.Text;
            row7["Lim1"] = uninsuredBoater.Replace("$", "").Replace(",", "");
            row7["Lim2"] = "0.00";
            row7["Lim3"] = "0.00";
            row7["Lim4"] = uninsuredBoater.Replace("$", "").Replace(",", "");
            row7["Lim5"] = "0.00";
            row7["Deductible"] = "0.00";
            row7["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row7);


            DataRow row10 = dtReinsAsl.NewRow(); //Tender Limit con segundo deducible
            row10["ReinsAsl"] = "63080";
            row10["Desc"] = "";
            row10["CoveragePremium"] = "0.00";
            row10["Total"] = "";
            if (dtTender.Rows.Count > 0)
            {
                if (dtTender.Rows[0]["TenderLimitAmount1"].ToString() == "")
                    row10["Lim1"] = "0.00";
                else
                    row10["Lim1"] = dtTender.Rows[0]["TenderLimitAmount1"].ToString().Replace("$", "").Replace(",", "").Trim();
                row10["Lim2"] = "0.00";
                row10["Lim3"] = "0.00";
                if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
                {
                    row10["Lim4"] = "0.00";
                    row10["Lim5"] = "0.00";
                    row10["Deductible"] = "0.00";
                    row10["MinDeductible"] = "0.00";
                }
                else
                {
                    string[] arrayOfDeductible4 = { "", "" };
                    string deductible4 = "";
                    arrayOfDeductible4 = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                    if (arrayOfDeductible4.Length > 1)
                    {
                        deductible4 = arrayOfDeductible4[1].Trim().Replace("%", "");
                        double ded4 = double.Parse(deductible4) / 100.00;
                        row10["Lim4"] = ded4.ToString();
                        row10["Lim5"] = "0.00";
                        row10["Deductible"] = ded4.ToString();
                        row10["MinDeductible"] = "0.00";
                    }
                    else
                    {
                        row10["Lim4"] = "0.00";
                        row10["Lim5"] = "0.00";
                        row10["Deductible"] = "0.00";
                        row10["MinDeductible"] = "0.00";
                    }
                }


            }
            else
            {
                row10["Lim1"] = "0.00";
                row10["Lim2"] = "0.00";
                row10["Lim3"] = "0.00";
                row10["Lim4"] = "0.00";
                row10["Lim5"] = "0.00";
                row10["Deductible"] = "0.00";
                row10["MinDeductible"] = "0.00";
            }

            dtReinsAsl.Rows.Add(row10);

            dtOtherCvrgDetail.TableName = "OtherCvrgDetail";
            dtOtherCvrgDetail.Columns.Add("ReinsAsl");
            dtOtherCvrgDetail.Columns.Add("DisplayAs");
            dtOtherCvrgDetail.Columns.Add("IndexNo");
            dtOtherCvrgDetail.Columns.Add("Limit1");
            dtOtherCvrgDetail.Columns.Add("Limit2");
            dtOtherCvrgDetail.Columns.Add("Tag");

            if (dtReinsAsl.Rows.Count > 0)
            {
                for (int i = 0; i < dtReinsAsl.Rows.Count; i++)
                {
                    int Count = 1;
                    DataRow row8 = dtOtherCvrgDetail.NewRow();
                    switch (dtReinsAsl.Rows[i]["ReinsAsl"].ToString())
                    {
                        case "59080": //Medical Payments
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"];
                            row8[1] = "Medical Payments";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = dtReinsAsl.Rows[i]["Lim2"];
                            row8[5] = "Per Person/Per Occurrence";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "58080": //Hull Limit con segundo deducible
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"];
                            row8[1] = "Hull && Machinery - Windstorm";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "60080": //Hull Limit con primer deducible
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"];
                            row8[1] = "Hull && Machinery - All Other Perils";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "61080": //Protection & Indemnity
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Protection & Indemnity";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Each Occurrence";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "62080": //Personal Effects
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Personal Contents";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "66080": //Tender Limit con primer deducible
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Tender Limit - All Other Perils";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "63080": //Tender Limit con segundo deducible
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Tender Limit - Windstorm";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "65080": //Trailers
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Trailer";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "67080": //Uninsured Boaters
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Uninsured Boaters";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Each Occurrence";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;
                    }
                }
            }

            using (StringWriter sw = new StringWriter())
            {
                dtReinsAsl.WriteXml(sw);
                CoverageCodesXml = sw.ToString();
                XmlDocument docSave = new XmlDocument();
                docSave.LoadXml(sw.ToString());
                docSave.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + TaskControlID + "_OtherCvrg1" + ".xml");
                sw.Close();
                docSave = null;
            }

            using (StringWriter sw = new StringWriter())
            {
                dtOtherCvrgDetail.WriteXml(sw);
                OtherCvrgDetailXml = sw.ToString();
                XmlDocument docSave = new XmlDocument();
                docSave.LoadXml(sw.ToString());
                docSave.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + TaskControlID + "_OtherCvrgDetail1" + ".xml");
                sw.Close();
                docSave = null;
            }


            if (dt.Rows.Count > 0)
            {

                cmd.CommandText = "sproc_ConsumeXMLePPS-YACHT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();


                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Incept", DateTime.Parse(dt.Rows[0]["EffectiveDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00");
                cmd.Parameters.AddWithValue("@Expire", DateTime.Parse(dt.Rows[0]["ExpirationDate"].ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00");
                if (chkIsRenew.Checked)
                {
                    cmd.Parameters.AddWithValue("@PolicyID", txtPolicyToRenewType.Text.Trim() + txtPolicyNoToRenew.Text.Trim() + "-" + DateTime.Parse(txtEffectiveDate.Text.Trim()).Year.ToString().Substring(2).ToString());
                    if (txtPolicyNoToRenewSuffix.Text.Trim() != "" && txtPolicyNoToRenewSuffix.Text.Trim() != "00")
                    {
                        cmd.Parameters.AddWithValue("@RenewalOf", txtPolicyToRenewType.Text.Trim() + txtPolicyNoToRenew.Text.Trim() + "-" + txtPolicyNoToRenewSuffix.Text.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RenewalOf", txtPolicyToRenewType.Text.Trim() + txtPolicyNoToRenew.Text.Trim());
                    }
                    cmd.Parameters.AddWithValue("@TransType", "REN");
                    cmd.Parameters.AddWithValue("@Client", dt.Rows[0]["Description"].ToString().Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PolicyID", dt.Rows[0]["PolicyType"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@TransType", "NEW");
                    cmd.Parameters.AddWithValue("@RenewalOf", "");
                    cmd.Parameters.AddWithValue("@Client", "0");
                }

                string FN = dt.Rows[0]["FirstNa"].ToString().Trim();
                string LN = dt.Rows[0]["LastNa1"].ToString().Trim();
                string BusType = "1";
                string BusFlag = "0";
                BusFlag = "0";

                if (chkIsCommercial.Checked)
                {
                    FN = "";
                    LN = txtCompanyName.Text.Trim();
                    BusFlag = "0";
                }

                string ComRate = "0.0000000e+000";

                DataTable DtCommision = GetCommissionAgentRateByAgentID(TaskControlID.ToString(), "27"); //GetCommissionAgentRateByAgentID(AgentID.ToString().Trim(), "22");

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
                cmd.Parameters.AddWithValue("@DispImagePerson", "Person");
                cmd.Parameters.AddWithValue("@DispImagePolicy", "Policy");
                cmd.Parameters.AddWithValue("@SpecEndorse", "");
                cmd.Parameters.AddWithValue("@SID", "0");
                cmd.Parameters.AddWithValue("@UDPolicyID", "0");
                cmd.Parameters.AddWithValue("@PreparedBy", dt.Rows[0]["EnteredBy"].ToString().Trim());
                cmd.Parameters.AddWithValue("@ExcessLink", "0");
                if (dt.Rows[0]["IsCommercial"].ToString() == "True")
                    cmd.Parameters.AddWithValue("@PolSubType", "Cml");
                else
                    cmd.Parameters.AddWithValue("@PolSubType", "Pvt");
                cmd.Parameters.AddWithValue("@ReinsPcnt", "0.0000000e+000");
                cmd.Parameters.AddWithValue("@Assessment", "0.0000");
                cmd.Parameters.AddWithValue("@PayDate", "");
                cmd.Parameters.AddWithValue("@Polrelat", "NI");
                if(LN.Trim() == "")
                    cmd.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = DBNull.Value;
                else
                    cmd.Parameters.AddWithValue("@LastName", LN);
                if (FN.Trim() == "")
                    cmd.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = DBNull.Value;
                else
                    cmd.Parameters.AddWithValue("@FirstName", FN); 
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
                cmd.Parameters.AddWithValue("@Rphone", dt.Rows[0]["HomePhone"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Csbyt", "0");
                cmd.Parameters.AddWithValue("@Cphone", dt.Rows[0]["Cellular"].ToString().Trim());
                cmd.Parameters.AddWithValue("@Eaddr", dt.Rows[0]["Email"].ToString().Trim());

                // Add the parameters for Bldgs
                cmd.Parameters.AddWithValue("@Descrip", dt.Rows[0]["BoatName"].ToString().Trim() + " - " + dt.Rows[0]["LOA"].ToString().Trim() + " " + dt.Rows[0]["BoatYear"].ToString().Trim());
                if (dt.Rows[0]["HomeportDesc"].ToString().Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@Location", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Location", dt.Rows[0]["HomeportDesc"].ToString().Trim());
                }
                cmd.Parameters.AddWithValue("@Island", "4");

                double hulllimit, personaleffects, tender1, totalInsVal = 0.00;

                if (dtTender.Rows.Count > 0 && dtTender.Rows[0]["TenderLimitAmount1"].ToString().Replace("$", "").Replace(",", "").Trim() != "")
                {
                    tender1 = double.Parse(dtTender.Rows[0]["TenderLimitAmount1"].ToString().Replace("$", "").Replace(",", "").Trim());
                }
                else
                {
                    tender1 = 0.00;
                }

                if (dt.Rows[0]["HullLimit"].ToString().Replace("$", "").Replace(",", "").Trim() != "")
                {
                    hulllimit = double.Parse(dt.Rows[0]["HullLimit"].ToString().Replace("$", "").Replace(",", "").Trim());
                }
                else
                {
                    hulllimit = 0.00;
                }

                if (dt.Rows[0]["PersonalEffects"].ToString().Replace("$", "").Replace(",", "").Trim() != "")
                {
                    personaleffects = double.Parse(dt.Rows[0]["PersonalEffects"].ToString().Replace("$", "").Replace(",", "").Trim());
                }
                else
                {
                    personaleffects = 0.00;
                }

                totalInsVal = tender1 + hulllimit + personaleffects;
                cmd.Parameters.AddWithValue("@InsVal", totalInsVal.ToString());
                cmd.Parameters.AddWithValue("@AnyNum", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@PayeeID", dt.Rows[0]["BankPPSID"].ToString().Trim());
                cmd.Parameters.AddWithValue("@LoanNo", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@PayeeID2", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@LoanNo2", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Families", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@RowHouse", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Rented", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Construction", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@ProtectionClass", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@YearBuilt", dt.Rows[0]["BoatYear"]);
                cmd.Parameters.AddWithValue("@FireDistrict", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Occupancy", SqlDbType.NVarChar).Value = DBNull.Value;
                string navigationLimitAll = "";
                if (dtNavigation.Rows.Count == 1)
                {
                    cmd.Parameters.Add("@NavLimit", dtNavigation.Rows[0]["NavigationLimitDesc"].ToString().Trim());
                }
                else if (dtNavigation.Rows.Count > 1)
                {
                    for (int i = 0; i < dtNavigation.Rows.Count; i++)
                    {
                        if (i == 0)
                            navigationLimitAll = dtNavigation.Rows[i]["NavigationLimitDesc"].ToString().Trim();
                        else
                            navigationLimitAll = navigationLimitAll + " " + dtNavigation.Rows[i]["NavigationLimitDesc"].ToString().Trim();
                    }

                    cmd.Parameters.Add("@NavLimit", navigationLimitAll);
                }
                else
                {
                    cmd.Parameters.Add("@NavLimit", SqlDbType.NVarChar).Value = DBNull.Value;

                }
                if (dtTender.Rows.Count > 0)
                {
                    if (dtTender.Rows[0]["TenderDesc1"].ToString().Trim() != "" && dtTender.Rows[0]["TenderSerial1"].ToString().Trim() != "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderDesc1"].ToString().Trim() + " " + dtTender.Rows[0]["TenderSerial1"].ToString().Trim());
                    else if (dtTender.Rows[0]["TenderDesc1"].ToString().Trim() != "" && dtTender.Rows[0]["TenderSerial1"].ToString().Trim() == "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderDesc1"].ToString().Trim());
                    else if (dtTender.Rows[0]["TenderDesc1"].ToString().Trim() == "" && dtTender.Rows[0]["TenderSerial1"].ToString().Trim() != "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderSerial1"].ToString().Trim());
                    else
                        cmd.Parameters.Add("@TenderText", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@TenderText", SqlDbType.NVarChar).Value = DBNull.Value;
                }

                if ((dt.Rows[0]["TrailerModel"].ToString().Trim() + dt.Rows[0]["TrailerSerial"].ToString().Trim()) == "")
                    cmd.Parameters.Add("@TrailerText", SqlDbType.NVarChar).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@TrailerText", dt.Rows[0]["TrailerModel"].ToString().Trim() + " " + dt.Rows[0]["TrailerSerial"].ToString().Trim());
                cmd.Parameters.Add("@VName", dt.Rows[0]["BoatName"].ToString().Trim());
                cmd.Parameters.Add("@Make", dt.Rows[0]["BoatBuilder"].ToString().Trim());
                cmd.Parameters.Add("@Model", dt.Rows[0]["BoatModel"].ToString().Trim());
                if (dt.Rows[0]["HullNumberRegistration"].ToString().Trim() == "")
                    cmd.Parameters.Add("@HIN", SqlDbType.NVarChar).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@HIN", dt.Rows[0]["HullNumberRegistration"].ToString().Trim());
                cmd.Parameters.AddWithValue("@LOA", dt.Rows[0]["LOA"].ToString().Trim());
                if (dt.Rows[0]["Engine"].ToString().Trim() == "" && dt.Rows[0]["EngineSerialNumber"].ToString().Trim() == "")
                {
                    cmd.Parameters.Add("@VesselProp", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else if (dt.Rows[0]["Engine"].ToString().Trim() == "" && dt.Rows[0]["EngineSerialNumber"].ToString().Trim() != "")
                {
                    cmd.Parameters.Add("@VesselProp", dt.Rows[0]["EngineSerialNumber"].ToString().Trim());
                }
                else if (dt.Rows[0]["Engine"].ToString().Trim() != "" && dt.Rows[0]["EngineSerialNumber"].ToString().Trim() == "")
                {
                    cmd.Parameters.Add("@VesselProp", dt.Rows[0]["Engine"].ToString().Trim());
                }
                else
                {
                    cmd.Parameters.Add("@VesselProp", dt.Rows[0]["Engine"].ToString().Trim() + " " + dt.Rows[0]["EngineSerialNumber"].ToString().Trim());
                }
                cmd.Parameters.AddWithValue("@Storeys", SqlDbType.NVarChar).Value = DBNull.Value;
                //OtherCoverage & OtherCovrgDetail
                cmd.Parameters.AddWithValue("@CoverageCodesXml", CoverageCodesXml);
                cmd.Parameters.AddWithValue("@OtherCvrgDetailXml", OtherCvrgDetailXml);

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(PPSPolicy);

                //cmd.ExecuteReader();
            }

            sqlConnection1.Close();

            if (PPSPolicy.Rows.Count > 0)
            {
                EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
                string ClientID = PPSPolicy.Rows[0]["Client"].ToString().Trim();
                taskControl.Customer.Description = ClientID;
                if (PPSPolicy.Rows[0]["PolicyID"].ToString().Trim().Contains('-'))
                {
                    String[] arrayPolicyNo;
                    arrayPolicyNo = PPSPolicy.Rows[0]["PolicyID"].ToString().Trim().Replace("MAR", "").Split('-');
                    txtPolicyNo.Text = arrayPolicyNo[0];
                    txtSuffix.Text = arrayPolicyNo[1];
                    taskControl.PolicyNo = arrayPolicyNo[0];
                    taskControl.Suffix = arrayPolicyNo[1];
                    UpdatePolicyYachtRenewFromPPSByTaskControlID(TaskControlID, arrayPolicyNo[0], ClientID, arrayPolicyNo[1]);
                }
                else
                {
                    txtPolicyNo.Text = PPSPolicy.Rows[0]["PolicyID"].ToString().Trim().Replace("MAR", "");
                    txtSuffix.Text = "00";
                    taskControl.PolicyNo = PPSPolicy.Rows[0]["PolicyID"].ToString().Trim().Replace("MAR", "");
                    taskControl.Suffix = "00";
                    UpdatePolicyFromPPSByTaskControlID(TaskControlID, PPSPolicy.Rows[0]["PolicyID"].ToString().Trim().Replace("MAR", ""), ClientID);
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex);
            sqlConnection1.Close();
            UpdatePolicyErrorPPSModifyPolicyNO(TaskControlID);
            lblRecHeader.Text = ex.Message;
            mpeSeleccion.Show();
        }
    }

    private static DataTable GetYachtToPPSByTaskControlIDInfoGeneral(int TaskControlID)
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
            dt = exec.GetQuery("GetYachtToPPSByTaskControlID", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve the Stored Procedure called GetYachtToPPSByTaskControlID.", ex);
        }
    }

    private static DataTable GetYachtToPPSByTaskControlIDInfoGeneralTenderLimit2END(int TaskControlID)
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
            dt = exec.GetQuery("GetYachtToPPSByTaskControlID_TenderLimit2_END", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve the Stored Procedure called GetYachtToPPSByTaskControlID.", ex);
        }
    }

    private static DataTable GetYachtToPPSByTaskControlIDInfoTenderLimit(int TaskControlID)
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
            dt = exec.GetQuery("GetTenderLimitDecPageByTaskControlID", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve the Stored Procedure called GetReportTenderLimitByTaskControlID.", ex);
        }
    }

    private static DataTable GetYachtToPPSByTaskControlIDInfoTenderLimitDesc(int TaskControlID)
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
            dt = exec.GetQuery("GetTenderLimitByTaskControlID", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve the Stored Procedure called GetReportTenderLimitByTaskControlID.", ex);
        }
    }

    private static DataTable GetYachtToPPSByTaskControlIDInfoSurvey(int TaskControlID)
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
            dt = exec.GetQuery("GetReportSurveyByTaskControlID", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve the Stored Procedure called GetReportSurveyByTaskControlID.", ex);
        }
    }

    private static DataTable GetYachtToPPSByTaskControlIDInfoReport(int TaskControlID)
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
            dt = exec.GetQuery("GetYachtQuoteReport", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve the Stored Procedure called GetYachtQuoteReport.", ex);
        }
    }

    private static DataTable GetNavigationLimitCollectionToPPSByTaskControlID(int TaskControlID)
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
            dt = exec.GetQuery("GetNavigationLimitCollectionToPPSByTaskControlID", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve the Stored Procedure called GetNavigationLimitCollectionToPPSByTaskControlID.", ex);
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

    private static DataTable UpdatePolicyYachtRenewFromPPSByTaskControlID(int TaskControl, string PolicyNo, string ClientID, string Sufijo)
    {

        DataTable dt = new DataTable();

        DBRequest Executor = new DBRequest();

        try
        {
            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[4];

            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, TaskControl.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("PolicyNo", SqlDbType.VarChar, 50, PolicyNo.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("ClientID", SqlDbType.VarChar, 50, ClientID.ToString(), ref cookItems);
            DbRequestXmlCooker.AttachCookItem("Sufijo", SqlDbType.VarChar, 2, Sufijo.ToString(), ref cookItems);

            XmlDocument xmlDoc;

            try
            {
                xmlDoc = DbRequestXmlCooker.Cook(cookItems);
            }
            catch (Exception ex)
            {
                throw new Exception("Could not cook items.", ex);
            }

            dt = Executor.GetQuery("UpdatePolicyYachtRenewFromPPSByTaskControlID", xmlDoc);
            return dt;

        }
        catch (Exception ex)
        {
            throw new Exception("Could not cook items." + ex.ToString(), ex);
        }

        return dt;

    }

    //protected void btnPrintPolicy_Click(object sender, EventArgs e)
    //{
    //    PrintYachtQuote("YachtDecPage.rdlc");
    //}

    //protected void btnPrintCertificate_Click(object sender, EventArgs e)
    //{
    //    PrintYachtQuote("YachtCertificateLiability.rdlc");
    //}

    //protected void btnPrintCertificateBank_Click(object sender, EventArgs e)
    //{
    //    PrintYachtQuote("YachtCertificateLiabilityBanco.rdlc");
    //}

    //protected void btnPrintCertificateMarina_Click(object sender, EventArgs e)
    //{
    //    PrintYachtQuote("YachtCertificateLiabilityMarina.rdlc");
    //}

    protected void btnAcceptQuote_Click(object sender, EventArgs e)
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
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
            btnConvert.Enabled = true;
            btnConvert.Visible = true;
            btnAcceptQuote.Enabled = false;
            btnAcceptQuote.Visible = false;
            btnPremiumFinance.Visible = true;
            btnPreviewPolicy.Visible = true;
            UpdateAcceptQuote(true, taskControl.TaskControlID);
            taskControl = (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
            Session["TaskControl"] = taskControl;
            FillTextControl();
            DisableControl();
            if (txtTotalPremiumPoliza.Text.Trim() != "$0")
            {
                radioBtnTP1.Enabled = true;
                radioBtnTP1.Visible = true;
            }
            if (txtTotalPremium2.Text.Trim() != "$0")
            {
                radioBtnTP2.Enabled = true;
                radioBtnTP2.Visible = true;
            }
            
            lblRecHeader.Text = "Yacht quote accepted successfully. Loss payee, engine, engine serial number, trailer model and trailer serial number fields are now available in the modify option.";
            mpeSeleccion.Show();
        }
        catch (Exception ex)
        {
            lblRecHeader.Text = ex.Message;
            mpeSeleccion.Show();
        }
    }


    public void ddlHomeport_SelectedIndexChanged(object sender, EventArgs e)
    {
        setHomeportAddress();
        Page.SetFocus(ddlHomeport);
    }

    private void setHomeportAddress()
    {
        DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[1];

        DbRequestXmlCooker.AttachCookItem("HomeportID",
            SqlDbType.Int, 0, ddlHomeport.SelectedItem.Value,
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

        DataTable dt = exec.GetQuery("GetHomeportAddressByHomeportID", xmlDoc);

        if (ddlHomeport.SelectedItem.Value == "29" || ddlHomeport.SelectedItem.Value == "30" || ddlHomeport.SelectedItem.Value == "43" || ddlHomeport.SelectedItem.Value == "44")
        {
            txtHomeportAddress.Text = "";
            txtHomeportAddress.Enabled = true;
        }
        else if (ddlHomeport.SelectedIndex == 0)
        {
            txtHomeportAddress.Enabled = false;
            txtHomeportAddress.Text = "";
        }
        else
        {
            txtHomeportAddress.Enabled = false;
            txtHomeportAddress.Text = dt.Rows[0]["HomeportAddress"].ToString();
        }
    }

    //protected void btnPrintPortEndorsement_Click(object sender, EventArgs e)
    //{
    //    EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
    //    if (ddlHomeport.SelectedItem.Value != "14" && ddlHomeport.SelectedItem.Value != "20" && ddlHomeport.SelectedItem.Value != "11")
    //    {
    //        PrintYachtQuote("YachtPortEndorsement.rdlc");
    //    }
    //}

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

    protected void VerifyPolicyExist()
    {
        try
        {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

            System.Data.DataTable dt = null;
            dt = GetVerifyPolicyExist(taskControl.TaskControlID);

            if (dt != null)


            {
                if (dt.Rows.Count > 0)
                {
                    throw new Exception("This quote has already been converted to Policy.");
                }
            }
        }
        catch (Exception exp)
        {
            throw new Exception("This quote has already been converted to Policy.");
        }
    }

    protected void txtLOA_TextChanged(object sender, EventArgs e)
    {
        txtLOA.Text = txtLOA.Text.Replace("'",".");
    }

    protected void btnBankList_Click(object sender, EventArgs e)
    {
        mpeBankList.Show();
    }

    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblBankListSelected.Text = GetBankListInfo(ddlBank.SelectedItem.Value);
        lblBankListSelected2.Text = lblBankListSelected.Text;
        mpeBankList.Show();
    }

    private string GetBankListInfo(string bankid)
    {
        if (bankid == String.Empty)
            return "";
        string Info = "";
        string conString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        using (SqlConnection connection = new SqlConnection(conString))
        {
            connection.Open();
            //
            // SqlCommand should be created inside using.
            // ... It receives the SQL statement.
            // ... It receives the connection object.
            // ... The SQL text works with a specific database.
            //
            using (SqlCommand command = new SqlCommand(
                String.Format("SELECT * FROM BANK_VI WHERE PPSID = {0}", bankid),
                connection))
            {
                //
                // Instance methods can be used on the SqlCommand.
                // ... These read data.
                //
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    var tb = new DataTable();
                    tb.Load(reader);
                    if (tb != null)
                    {
                        if (tb.Rows.Count > 0)
                        {
                            var sb = new StringBuilder();
                            sb.AppendLine(String.Format("<b>{0}</b>", "ADDRESS:"));
                            if (tb.Rows[0]["BankDesc"].ToString().Trim() != "")
                                sb.AppendLine(String.Format("{0}", tb.Rows[0]["BankDesc"].ToString().Trim()));
                            if (tb.Rows[0]["Address1"].ToString().Trim() != "")
                                sb.AppendLine(String.Format("{0}", tb.Rows[0]["Address1"].ToString().Trim()));
                            if (tb.Rows[0]["Address2"].ToString().Trim() != "" && tb.Rows[0]["Address2"].ToString().Trim() != "NULL")
                                sb.AppendLine(String.Format("{0}", tb.Rows[0]["Address2"].ToString().Trim()));
                            if ((tb.Rows[0]["City"].ToString().Trim() + tb.Rows[0]["State"].ToString().Trim() + tb.Rows[0]["ZipCode"].ToString().Trim()) != String.Empty)
                                sb.AppendLine(String.Format("{0} {1}, {2}", tb.Rows[0]["City"].ToString().Trim(), tb.Rows[0]["State"].ToString().Trim(), tb.Rows[0]["ZipCode"].ToString().Trim()));
                            Info = sb.ToString().Replace(Environment.NewLine, "<br />");
                        }
                    }
                }
            }
        }

        return Info;
    }

    public void ddlPrintOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            switch (ddlPrintOptions.SelectedValue)
            {
                case "PRINT DEC PAGE & ENDORS":
                    PrintYachtQuote("YachtDecPage.rdlc");
                    break;
                case "PRINT POLICY FORMS":
                    PrintYachtQuote("YachtImportantChangesPolicy.rdlc");
                    break;
                case "PRINT CERTIFICATE":
                    PrintYachtQuote("YachtCertificateLiability.rdlc");
                    break;
                case "PRINT BANK CERTIFICATE":
                    PrintYachtQuote("YachtCertificateLiabilityBanco.rdlc");
                    break;
                case "PRINT PORT CERTIFICATE":
                    PrintYachtQuote("YachtCertificateLiabilityMarina.rdlc");
                    break;
                case "PRINT PORT CERT ADD INS":
                    PrintYachtQuote("YachtCertificateLiabilityMarinaAddInsured.rdlc");
                    break;
                case "PRINT PORT ENDORSEMENT":
                    EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
                    //if (ddlHomeport.SelectedItem.Value != "14" && ddlHomeport.SelectedItem.Value != "20" && ddlHomeport.SelectedItem.Value != "11")
                    //{
                    //    PrintYachtQuote("YachtPortEndorsement.rdlc");
                    //}
                    string reportName = GetHomeportReportName(ddlHomeport.SelectedItem.Value, true);
                    PrintYachtQuote(reportName);
                    break;
            }
        }
        catch(Exception ex){
            LogError(ex);
        }
    }

    private void UpdateAcceptQuote(bool isAcceptQuote, int taskcontrolID)
    {
        string connString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        using (var conn = new SqlConnection(connString))
        {
            var cmd = new SqlCommand("UpdateAcceptedQuote", conn);
            cmd.Parameters.AddWithValue("TaskControlID", taskcontrolID);
            cmd.Parameters.AddWithValue("isAcceptQuote", isAcceptQuote);
            // set all other parameters
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.ExecuteNonQuery();
        }


    }


    protected bool CheckYachtExistPPS()
    {
        bool Found = false;

        try
        {
            //string cn = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
            //SqlConnection cn = new SqlConnection("Data Source=gic-msql\\ppssqlserver;Initial Catalog=AgentTestData;User ID=urclaims;password=3G@TD@t!1");
            //SqlConnection cn = new SqlConnection("Data Source=gic-msql\\ppssqlserver;Initial Catalog=GICPPSDATA;User ID=urclaims;password=3G@TD@t!1");
            //SqlConnection cn = new SqlConnection(@"Data Source=192.168.1.22\ppssqlserver;Initial Catalog=GICPPSDATA;User ID=urclaims;password=3G@TD@t!1");
            SqlConnection cn = new SqlConnection("Data Source=192.168.1.22\\ppssqlserver;Initial Catalog=TestGIC;User ID=URClaims;password=3G@TD@t!1");

            //@"Data Source=GIC-MSQL\PPSSQLSERVER;Initial Catalog=AgentTestData;User ID=urclaims;password=3G@TD@t!1";
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            DataTable table3 = new DataTable();
            using (var con = cn)
            {
                using (var cmd = new SqlCommand("sproc_ConsumeXMLePPS-YACHT_Verify", con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    if (txtPolicyNoToRenewSuffix.Text.Trim() == "" || txtPolicyNoToRenewSuffix.Text.Trim() == "00")
                    {
                        cmd.Parameters.AddWithValue("PolicyID", txtPolicyToRenewType.Text.Trim() + txtPolicyNoToRenew.Text.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("PolicyID", txtPolicyToRenewType.Text.Trim() + txtPolicyNoToRenew.Text.Trim() + "-" + txtPolicyNoToRenewSuffix.Text.Trim());
                    }
                    da.Fill(table);
                }
                //using (var con1 = cn)
                using (var cmd1 = new SqlCommand("sproc_ConsumeXMLePPS-YACHT_Verify_Coverage", con))
                using (var da1 = new SqlDataAdapter(cmd1))
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Clear();
                    if (txtPolicyNoToRenewSuffix.Text.Trim() == "" || txtPolicyNoToRenewSuffix.Text.Trim() == "00")
                    {
                        cmd1.Parameters.AddWithValue("PolicyID", txtPolicyToRenewType.Text.Trim() + txtPolicyNoToRenew.Text.Trim());
                    }
                    else
                    {
                        cmd1.Parameters.AddWithValue("PolicyID", txtPolicyToRenewType.Text.Trim() + txtPolicyNoToRenew.Text.Trim() + "-" + txtPolicyNoToRenewSuffix.Text.Trim());
                    }
                    da1.Fill(table2);
                }
                //using (var con2 = cn)
                using (var cmd2 = new SqlCommand("sproc_ConsumeXMLePPS-YACHT_Verify_HullLimit", con))
                using (var da2 = new SqlDataAdapter(cmd2))
                {
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Clear();
                    if (txtPolicyNoToRenewSuffix.Text.Trim() == "" || txtPolicyNoToRenewSuffix.Text.Trim() == "00")
                    {
                        cmd2.Parameters.AddWithValue("PolicyID", txtPolicyToRenewType.Text.Trim() + txtPolicyNoToRenew.Text.Trim());
                    }
                    else
                    {
                        cmd2.Parameters.AddWithValue("PolicyID", txtPolicyToRenewType.Text.Trim() + txtPolicyNoToRenew.Text.Trim() + "-" + txtPolicyNoToRenewSuffix.Text.Trim());
                    }
                    da2.Fill(table3);
                }
            }

            if (table.Rows.Count > 0)
            {
                EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

                Found = true;

                //FILL FIELDS WITH PPS INFO

                taskControl.Customer.Description = table.Rows[0]["Client"].ToString();

                if (DateTime.Parse(DateTime.Now.ToShortDateString()) <= DateTime.Parse(table.Rows[0]["Expire"].ToString()))
                {
                    txtEffectiveDate.Text = DateTime.Parse(table.Rows[0]["Expire"].ToString()).ToShortDateString();
                    txtExpirationDate.Text = DateTime.Parse(table.Rows[0]["Expire"].ToString()).AddYears(1).ToShortDateString();
                }
                else
                {
                    txtEffectiveDate.Text = DateTime.Parse(DateTime.Now.ToShortDateString()).ToShortDateString();
                    txtExpirationDate.Text = DateTime.Parse(DateTime.Now.ToShortDateString()).AddYears(1).ToShortDateString();
                }
                txtAddrs1.Text = table.Rows[0]["Maddr1"].ToString();
                txtAddrs2.Text = table.Rows[0]["Maddr2"].ToString();
                txtZip.Text = table.Rows[0]["Mzip"].ToString();
                txtCiudad.Text = table.Rows[0]["Mcity"].ToString();
                txtState.Text = table.Rows[0]["Mstate"].ToString();

                txtPhyAddress.Text = table.Rows[0]["Raddr1"].ToString();
                txtPhyAddress2.Text = table.Rows[0]["Raddr2"].ToString();
                txtPhyZipCode.Text = table.Rows[0]["Rzip"].ToString();
                txtPhyCity.Text = table.Rows[0]["Rcity"].ToString();
                txtPhyState.Text = table.Rows[0]["Rstate"].ToString();

                txtWorkPhone.Text = table.Rows[0]["Wphone"].ToString();
                txtCellular.Text = table.Rows[0]["Cphone"].ToString();

                txtemail.Text = table.Rows[0]["Eaddr"].ToString();

                ddlAgency.SelectedIndex = ddlAgency.Items.IndexOf(ddlAgency.Items.FindByValue(GetAgentByCarsID(table.Rows[0]["BrokerID"].ToString().Trim())));
                //ddlAgency.SelectedIndex = 0;
                //for (int i = 0; ddlAgency.Items.Count - 1 >= i; i++)
                //{
                //    if (ddlAgency.Items[i].Value.Trim() == table.Rows[0]["BrokerID"].ToString().Trim())
                //    {
                //        ddlAgency.SelectedIndex = i;
                //        i = ddlAgency.Items.Count - 1;
                //    }
                //}
                //ddlAgency.Enabled = true;


                txtFirstName.Text = table.Rows[0]["FirstName"].ToString();
                txtLastName.Text = table.Rows[0]["LastName"].ToString();
                txtInitial.Text = table.Rows[0]["Middle"].ToString();

                if (table.Rows[0]["Location"].ToString().Trim().ToUpper().Contains("TBA"))
                {
                    ddlHomeport.SelectedIndex = ddlHomeport.Items.IndexOf(ddlHomeport.Items.FindByText("TBA"));
                }
                else
                {
                    ddlHomeport.SelectedIndex = ddlHomeport.Items.IndexOf(ddlHomeport.Items.FindByText(table.Rows[0]["Location"].ToString().Trim()));
                }
                setHomeportAddress();
                txtBoatYear.Text = table.Rows[0]["YearBuilt"].ToString().Trim();
                txtBoatName.Text = table.Rows[0]["VName"].ToString().Trim();
                txtBoatModel.Text = table.Rows[0]["Model"].ToString().Trim();
                txtBoatBuilder.Text = table.Rows[0]["Make"].ToString().Trim();
                txtLOA.Text = table.Rows[0]["LOA"].ToString().Trim();
                txtEngine.Text = table.Rows[0]["VesselProp"].ToString().Trim();
                txtHullNumber.Text = table.Rows[0]["HIN"].ToString().Trim();
                txtTrailerModel.Text = table.Rows[0]["TrailerText"].ToString().Trim();

                if (table.Rows[0]["NavLimit"].ToString().Trim() != "")
                {
                    DataTable navLimit = GetNavigationLimitIDByPPSDesc(table.Rows[0]["NavLimit"].ToString().Trim());
                    if (navLimit.Rows.Count == 0)
                    {
                        ddlNavigationLimit.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlNavigationLimit.SelectedIndex = ddlNavigationLimit.Items.IndexOf(ddlNavigationLimit.Items.FindByText(navLimit.Rows[0]["NavigationLimitDesc"].ToString().Trim()));
                    
                    
                        DataTable dtNav = new DataTable();
                        if (ViewState["navigationData"] == null)
                        {
                            // Initialize data table if viewstate is null

                            dtNav.Columns.Add("No", typeof(int));
                            dtNav.Columns.Add("NavigationLimitID", typeof(int));
                            dtNav.Columns.Add("TaskControlID", typeof(int));
                            dtNav.Columns.Add("NavigationLimitDesc", typeof(string));
                            dtNav.Columns[0].AutoIncrement = true;    // Autogenerate serial key column for example
                            dtNav.Columns[0].AutoIncrementSeed = 1;
                        }
                        else
                            dtNav = (DataTable)ViewState["navigationData"];  // Grab datatable from viewstate if its not null

                        // Add your data row
                        DataRow dr = dtNav.NewRow();
                        if (ddlNavigationLimit.SelectedItem.Value.ToString().Trim() == "")
                            dr["NavigationLimitID"] = 0;
                        else
                            dr["NavigationLimitID"] = int.Parse(ddlNavigationLimit.SelectedItem.Value.ToString());
                        dr["TaskControlID"] = 0;
                        dr["NavigationLimitDesc"] = ddlNavigationLimit.SelectedItem.Text;
                        dtNav.Rows.Add(dr);
                        // Bind your gridview
                        gridViewNavigationLimit.DataSource = dtNav;
                        gridViewNavigationLimit.DataBind();
                        // Save datatable to ViewState
                        ViewState["navigationData"] = dtNav;
                    }
                }

                ddlBank.SelectedIndex = ddlBank.Items.IndexOf(ddlBank.Items.FindByValue(table.Rows[0]["PayeeID"].ToString().Trim()));
                lblBankListSelected2.Text = GetBankListInfo(ddlBank.SelectedItem.Value);

                double totalPremium = 0.00;
                totalPremium = double.Parse(table.Rows[0]["Premium"].ToString().Trim());
                txtTotalPremium.Text = totalPremium.ToString("c0");

                if (table3.Rows.Count > 0)
                {
                    if (table3.Rows[0]["Limit1"].ToString().Trim() != "0.00")
                    {
                        double hull = 0.00;
                        hull = double.Parse(table3.Rows[0]["Limit1"].ToString().Trim());
                        txtHullLimit.Text = hull.ToString("c0");
                    }
                }

                if (table2.Rows.Count > 0)
                {
                    double watercraftTotal = 0.00, watercraftTotal2 = 0.00;
                    string deductible1Text = "", deductible2Text = "";
                    for (int i = 0; i < table2.Rows.Count; i++)
                    {
                        switch (table2.Rows[i]["ReinsAsl"].ToString().Trim())
                        {
                            case "59080": //Medical Payments
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.0000" && table2.Rows[i]["Lim2"].ToString().Trim() != "0.0000")
                                {
                                    double medicalpayment1 = 0.00, medicalpayment2 = 0.00, medicalpremium = 0.00;
                                    string medicalpaymenttotal = "", medicalpremiumtext = "";
                                    medicalpayment1 = double.Parse(table2.Rows[i]["Lim1"].ToString().Trim());
                                    medicalpayment2 = double.Parse(table2.Rows[i]["Lim2"].ToString().Trim());
                                    medicalpaymenttotal = medicalpayment1.ToString("c0") + " / " + medicalpayment2.ToString("c0");
                                    ddlMedicalPayment.SelectedIndex = ddlMedicalPayment.Items.IndexOf(ddlMedicalPayment.Items.FindByText(medicalpaymenttotal));
                                    if (ddlMedicalPayment.SelectedIndex == 0)
                                    {
                                        txtOtherMedicalPayment.Text = medicalpaymenttotal;
                                    }
                                    medicalpremium = double.Parse(table2.Rows[i]["Premium"].ToString().Trim());
                                    medicalpremiumtext = medicalpremium.ToString("c0");
                                    txtMedicalPaymentPremiumTotal.Text = medicalpremiumtext;
                                }
                                break;

                            case "58080": //Hull Limit con segundo deducible
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.0000")
                                {
                                    double deductible2 = 0.00, deductibleCalculated = 0.00;
                                    watercraftTotal2 = double.Parse(table2.Rows[i]["Premium"].ToString().Trim());
                                    deductible2 = double.Parse(table2.Rows[i]["Deductible"].ToString().Trim());
                                    deductibleCalculated = deductible2 * 100;
                                    deductible2Text = deductibleCalculated.ToString() + "%";
                                }
                                break;

                            case "60080": //Hull Limit con primer deducible
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.0000")
                                {
                                    double deductible1 = 0.00, deductibleCalculated = 0.00;
                                    watercraftTotal = double.Parse(table2.Rows[i]["Premium"].ToString().Trim());
                                    deductible1 = double.Parse(table2.Rows[i]["Deductible"].ToString().Trim());
                                    deductibleCalculated = deductible1 * 100;
                                    deductible1Text = deductibleCalculated.ToString() + "%";
                                }
                                break;

                            case "61080": //Protection & Indemnity
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.0000")
                                {
                                    double piPremium = 0.00, protectionAndIndemnity = 0.00;
                                    piPremium = double.Parse(table2.Rows[i]["Premium"].ToString().Trim());
                                    txtPIPremium.Text = piPremium.ToString("c0");
                                    if (txtHullLimit.Text != "")
                                    {
                                        protectionAndIndemnity = double.Parse(table2.Rows[i]["Lim1"].ToString().Trim());
                                        ddlPI.SelectedIndex = ddlPI.Items.IndexOf(ddlPI.Items.FindByText(protectionAndIndemnity.ToString("c0")));
                                        if (ddlPI.SelectedIndex == 0)
                                        {
                                            txtOtherPI.Text = protectionAndIndemnity.ToString("c0");
                                        }
                                    }
                                    else
                                    {
                                        protectionAndIndemnity = double.Parse(table2.Rows[i]["Lim1"].ToString().Trim());
                                        ddlPILiabilityOnly.SelectedIndex = ddlPILiabilityOnly.Items.IndexOf(ddlPILiabilityOnly.Items.FindByText(protectionAndIndemnity.ToString("c0")));
                                        if (ddlPILiabilityOnly.SelectedIndex != 0)
                                        {
                                            txtPersonalEffect.Text = "$0";
                                            txtPersonalEffectDeductible.Text = "$0";
                                            txtPersonalEffectPremium.Text = "$0";
                                        }
                                        if (ddlPILiabilityOnly.SelectedIndex == 0)
                                        {
                                            txtOtherPI.Text = protectionAndIndemnity.ToString("c0");
                                        }
                                    }
                                }
                                break;

                            case "62080": //Personal Effects
                                double pELimit = 0.00, pEDeductible = 0.00, pEPremium = 0.00;
                                pELimit = double.Parse(table2.Rows[i]["Lim1"].ToString().Trim());
                                pEDeductible = double.Parse(table2.Rows[i]["Deductible"].ToString().Trim());
                                pEPremium = double.Parse(table2.Rows[i]["Premium"].ToString().Trim());
                                txtPersonalEffect.Text = pELimit.ToString("c0");
                                txtPersonalEffectDeductible.Text = pEDeductible.ToString("c0");
                                txtPersonalEffectPremium.Text = pEPremium.ToString("c0");
                                break;

                            case "66080": //Tender Limit
                                double tenderLimit1 = 0.00, tenderLimit2 = 0.00;
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.0000")
                                {
                                    DataTable dt = new DataTable();
                                    if (ViewState["tenderData"] == null)
                                    {
                                        // Initialize data table if viewstate is null

                                        dt.Columns.Add("No", typeof(int));
                                        dt.Columns.Add("TaskControlID", typeof(int));
                                        dt.Columns.Add("TenderLimitAmount", typeof(string));
                                        dt.Columns.Add("TenderDesc", typeof(string));
                                        dt.Columns.Add("TenderSerial", typeof(string));
                                        dt.Columns[0].AutoIncrement = true;    // Autogenerate serial key column for example
                                        dt.Columns[0].AutoIncrementSeed = 1;
                                    }
                                    else
                                        dt = (DataTable)ViewState["tenderData"];  // Grab datatable from viewstate if its not null

                                    // Add your data row
                                    DataRow dr = dt.NewRow();
                                    dr["TaskControlID"] = 0;
                                    tenderLimit1 = double.Parse(table2.Rows[i]["Lim1"].ToString().Trim());
                                    dr["TenderLimitAmount"] = tenderLimit1.ToString("c0");
                                    dr["TenderDesc"] = table.Rows[0]["TenderText"].ToString().Trim();
                                    dr["TenderSerial"] = "";
                                    dt.Rows.Add(dr);
                                    // Bind your gridview
                                    gridViewTenderLimit.DataSource = dt;
                                    gridViewTenderLimit.DataBind();
                                    // Save datatable to ViewState
                                    ViewState["tenderData"] = dt;
                                }
                                if (table2.Rows[i]["Lim2"].ToString().Trim() != "0.0000")
                                {
                                    DataTable dt = new DataTable();
                                    if (ViewState["tenderData"] == null)
                                    {
                                        // Initialize data table if viewstate is null

                                        dt.Columns.Add("No", typeof(int));
                                        dt.Columns.Add("TaskControlID", typeof(int));
                                        dt.Columns.Add("TenderLimitAmount", typeof(string));
                                        dt.Columns.Add("TenderDesc", typeof(string));
                                        dt.Columns.Add("TenderSerial", typeof(string));
                                        dt.Columns[0].AutoIncrement = true;    // Autogenerate serial key column for example
                                        dt.Columns[0].AutoIncrementSeed = 1;
                                    }
                                    else
                                        dt = (DataTable)ViewState["tenderData"];  // Grab datatable from viewstate if its not null

                                    // Add your data row
                                    DataRow dr = dt.NewRow();
                                    dr["TaskControlID"] = 0;
                                    tenderLimit2 = double.Parse(table2.Rows[i]["Lim2"].ToString().Trim());
                                    dr["TenderLimitAmount"] = tenderLimit2.ToString("c0");
                                    if (table.Rows.Count > 1)
                                    {
                                        dr["TenderDesc"] = table.Rows[1]["TenderText"].ToString().Trim();
                                    }
                                    else
                                    {
                                        dr["TenderDesc"] = "";
                                    }
                                    dr["TenderSerial"] = "";
                                    dt.Rows.Add(dr);
                                    // Bind your gridview
                                    gridViewTenderLimit.DataSource = dt;
                                    gridViewTenderLimit.DataBind();
                                    // Save datatable to ViewState
                                    ViewState["tenderData"] = dt;
                                }
                                break;

                            case "65080": //Trailers
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.0000")
                                {
                                    double trailerLimit = 0.00, trailerPremium = 0.00;
                                    trailerLimit = double.Parse(table2.Rows[i]["Lim1"].ToString().Trim());
                                    trailerPremium = double.Parse(table2.Rows[i]["Premium"].ToString().Trim());
                                    txtTrailer.Text = trailerLimit.ToString("c0");
                                    txtTrailerPremium.Text = trailerPremium.ToString("c0");
                                }
                                break;

                            case "67080": //Uninsured Boaters
                                if (table2.Rows[i]["Lim1"].ToString().Trim() != "0.0000")
                                {
                                    double uninsuredBoater = 0.00, uninsuredBoaterPremium = 0.00;
                                    uninsuredBoater = double.Parse(table2.Rows[i]["Lim1"].ToString().Trim());
                                    uninsuredBoaterPremium = double.Parse(table2.Rows[i]["Premium"].ToString().Trim());
                                    ddlUninsuredBoaters.SelectedIndex = ddlUninsuredBoaters.Items.IndexOf(ddlUninsuredBoaters.Items.FindByText(uninsuredBoater.ToString("c0")));
                                    if (ddlUninsuredBoaters.SelectedIndex == 0)
                                    {
                                        txtOtherUninsuredBoater.Text = uninsuredBoater.ToString("c0");
                                    }
                                }
                                break;
                        }
                    }
                    txtWatercraftLimitTotal1.Text = (watercraftTotal + watercraftTotal2).ToString("c0");
                    if (deductible1Text != "" && deductible2Text != "")
                        ddlDeductibles1.SelectedIndex = ddlDeductibles1.Items.IndexOf(ddlDeductibles1.Items.FindByText(deductible1Text + " / " + deductible2Text));
                    else if (deductible1Text != "" && deductible2Text == "")
                        ddlDeductibles1.SelectedIndex = ddlDeductibles1.Items.IndexOf(ddlDeductibles1.Items.FindByText(deductible1Text));
                    calculateWatercraftLimit();
                    if (txtWatercraftLimit1.Text != "")
                    {
                        double deductible1 = 0.0;
                        double deductible2 = 0.0;
                        double hullLimit = 0.0;
                        double total1 = 0.0;
                        double total2 = 0.0;
                        if (ddlDeductibles1.SelectedIndex != 0)
                        {
                            DataTable dt = GetDeductibleAmount(int.Parse(ddlDeductibles1.SelectedItem.Value));
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                                {
                                    deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                    hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                                    total1 = deductible1 * hullLimit;

                                    lblDeductibleCalculated1.Text = total1.ToString("c0");
                                }
                                else
                                {
                                    deductible1 = double.Parse(dt.Rows[0]["DeductibleAmount1"].ToString().Trim());
                                    deductible2 = double.Parse(dt.Rows[0]["DeductibleAmount2"].ToString().Trim());
                                    hullLimit = double.Parse(txtHullLimit.Text.Replace("$", "").Replace(",", "").ToString());

                                    total1 = deductible1 * hullLimit;
                                    total2 = deductible2 * hullLimit;

                                    lblDeductibleCalculated1.Text = total1.ToString("c0") + " / " + total2.ToString("c0");
                                }
                            }
                            else
                                lblDeductibleCalculated1.Text = "None";
                        }
                        else
                            lblDeductibleCalculated1.Text = "None";
                    }
                    else
                        lblDeductibleCalculated1.Text = "None";
                }

                lblRecHeader.Text = "Yacht policy information successfully found.";
                mpeSeleccion.Show();

            }
            else
            {
                Found = false;
                lblRecHeader.Text = "Yacht policy information not found.";
                mpeSeleccion.Show();
            }




            return Found;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.InnerException.ToString(), ex);
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
        dt = exec.GetQuery("GetAgentByCarsIDMAR", xmlDoc);
        string rtAgentID = "0";

        if (dt.Rows.Count > 0)
        {
            rtAgentID = dt.Rows[0]["dAgentID"].ToString();
        }
        return rtAgentID.ToString();
    }

    protected void chkIsRenew_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIsRenew.Checked == true)
        {
            DivRenew.Visible = true;
            txtPolicyToRenewType.Visible = true;
            lblPolicyNoToRenew.Visible = true;
            txtPolicyNoToRenew.Visible = true;
            txtPolicyNoToRenew.Enabled = true;
            txtPolicyNoToRenewSuffix.Visible = true;
            txtPolicyNoToRenewSuffix.Enabled = true;
            btnVerifyYachtInPPS.Visible = true;
            btnVerifyYachtInPPS.Enabled = true;
            lblBank.Visible = true;
            ddlBank.Visible = true;
            ddlBank.Enabled = true;
            btnBankList.Enabled = true;
            lblBankListSelected2.Visible = true;
        }
        else
        {
            DivRenew.Visible = false;
            txtPolicyToRenewType.Visible = false;
            lblPolicyNoToRenew.Visible = false;
            txtPolicyNoToRenew.Visible = false;
            txtPolicyNoToRenew.Enabled = false;
            txtPolicyNoToRenew.Text = "";
            txtPolicyNoToRenewSuffix.Visible = false;
            txtPolicyNoToRenewSuffix.Enabled = false;
            txtPolicyNoToRenewSuffix.Text = "";
            btnVerifyYachtInPPS.Visible = false;
            btnVerifyYachtInPPS.Enabled = false;
            FillTextControl();

        }
    }

    protected void btnVerifyYachtInPPS_Click(object sender, EventArgs e)
    {
        txtPolicyNoToRenew.Text = txtPolicyNoToRenew.Text.Trim().ToUpper();
        CheckYachtExistPPS();
    }

    protected void btnPremiumFinance_Click(object sender, EventArgs e)
    {
        try
        {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
            if (taskControl.isQuote == true && radioBtnTP1.Checked == false && radioBtnTP2.Checked == false)
            {
                throw new Exception("You must select a total premium option.");
            }

            if (ddlAgency.SelectedItem.Value.ToString().Trim() != "057")
            {
                throw new Exception("This option is only available for MidOcean Insurance Agency.");
            }

            if (taskControl.isQuote == true && radioBtnTP1.Checked == true && double.Parse(txtTotalPremium.Text.Replace("$", "").Replace(",", "").Trim()) < 500)
            {
                mpeSeleccion.Show();
                lblRecHeader.Text = String.Concat(
                    @"<p>Option not available for <b>Total Premium</b> less than $500.</p>",
                    @"<p>Current <b>Total Premium:</b> $",
                    txtTotalPremium.Text,
                    @"</p>"
                );

            }
            else if (taskControl.isQuote == true && radioBtnTP2.Checked == true && double.Parse(txtTotalPremium2.Text.Replace("$", "").Replace(",", "").Trim()) < 500)
            {
                mpeSeleccion.Show();
                lblRecHeader.Text = String.Concat(
                    @"<p>Option not available for <b>Total Premium</b> less than $500.</p>",
                    @"<p>Current <b>Total Premium:</b> $",
                    txtTotalPremium2.Text,
                    @"</p>"
                );
            }

            else if (taskControl.isQuote == false && double.Parse(txtTotalPremiumPoliza.Text.Replace("$", "").Replace(",", "").Trim()) < 500)
            {
                mpeSeleccion.Show();
                lblRecHeader.Text = String.Concat(
                    @"<p>Option not available for <b>Total Premium</b> less than $500.</p>",
                    @"<p>Current <b>Total Premium:</b> $",
                    txtTotalPremiumPoliza.Text,
                    @"</p>"
                );
            }
            else
            {
                if (taskControl.isQuote == false)
                {
                    btnFM2.Enabled = true;
                    btnFM5.Enabled = true;
                    btnFM8.Enabled = true;
                }
                else
                {
                    btnFM2.Enabled = false;
                    btnFM5.Enabled = false;
                    btnFM8.Enabled = false;
                }
                string URL = "";
                System.Data.DataTable dtFM = (new FinanceMaster()).GetFinanceMasterByTaskcontrolID(taskControl.TaskControlID);
                if (dtFM != null)
                {
                    if (dtFM.Rows.Count > 0)
                    {
                        URL = dtFM.Rows[0][0].ToString();
                    }
                }
                if (URL == "")
                    GetPaymentOptions();
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('" + URL + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
                }
            }
        }
        catch (Exception ex)
        {
            mpeSeleccion.Show();
            lblRecHeader.Text = ex.Message;
        }
    }

    public void GetPaymentOptions()
    {
        try
        {
            string xml = "";
            FinanceMaster FM = new FinanceMaster();
            FillFinanceMaster(FM);
            xml = FM.GetPremiumFinanceWs();
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.XmlResolver = null;
            XmlDoc = FM.RemoveXmlns(xml);
            //XmlDoc.Load(new StringReader(xml));
            XmlNodeList XmlBase = XmlDoc.GetElementsByTagName("string");
            string DivAlert = @"<div align=""center"">", DefaultText = "10 Payments, $100.00 Deposit, $100.00 Payment, 10.00% APR, $100.00 Interest, $10.00 Fee",
                DefaultText1 = "$100.00", DefaultText2 = "$100.00", DynamicText = "", DynamicText2 = "";
            //DivAlert += @"<h4><b>PLEASE CLICK 'ACCEPT' NEXT TO YOUR PREFFERED PAYMENT OPTION:</b></h4>";
            DivAlert += @"<br>";
            DynamicText = DefaultText1;
            DynamicText2 = DefaultText2;
            foreach (XmlNode XmlPolicyBase in XmlBase)
            {
                if (XmlPolicyBase.HasChildNodes)
                    foreach (XmlNode Childs in XmlPolicyBase.ChildNodes)
                    {
                        XmlDoc = FM.RemoveXmlns(Childs.InnerText);
                        XmlNodeList InnerXmlBase = XmlDoc.GetElementsByTagName("Quote");
                        foreach (XmlNode InnerXmlPolicyBase in InnerXmlBase)
                        {
                            foreach (XmlNode GrandChilds in InnerXmlPolicyBase.ChildNodes)
                            {

                                //DivAlert += "<br>";
                                //DivAlert += "<br>";
                                //DivAlert += @"<p><b>.	Insured ID </b></p>";
                                //DivAlert += "<br>";
                                //DivAlert += @"<p><b>.	Vehicle Registration </b></p>";
                                //DivAlert += "<br>";
                                //DivAlert += @"<p><b>.	Signed Application </b></p>";

                                if (GrandChilds.Name == "QuoteCode")
                                {
                                    Session["FMQuoteCode"] = GrandChilds.InnerText.ToString();
                                    continue;
                                }

                                if (GrandChilds.Name == "DownPay10")
                                {
                                    //DivAlert += @"<input type=""button""  value=""ACCEPT"" class=""btn btn-primary btn-lg"" style=""width:50px"" />";
                                    //DivAlert += @"<p><b>";
                                    //DynamicText = DynamicText.Replace("$100.00 Deposit", "$" + GrandChilds.InnerText + " Deposit");
                                    continue;
                                }
                                if (GrandChilds.Name == "NumberOfPay10")
                                {
                                    //DynamicText = DynamicText.Replace("10 Payments", GrandChilds.InnerText + " Payments");
                                    continue;
                                }
                                if (GrandChilds.Name == "MonthlyPayment10")
                                {
                                    //DynamicText = DynamicText.Replace("$100.00 Payment", "$" + GrandChilds.InnerText + " Payment");
                                    continue;
                                }
                                if (GrandChilds.Name == "APR10")
                                {
                                    //DynamicText = DynamicText.Replace("10.00% APR", GrandChilds.InnerText + "%" + " APR");
                                    continue;
                                }
                                if (GrandChilds.Name == "Interest10")
                                {
                                    // DynamicText = DynamicText.Replace("$100.00 Interest", "$" + GrandChilds.InnerText + " Interest");
                                    continue;
                                }
                                if (GrandChilds.Name == "SetupFee10")
                                {
                                    //DynamicText = DynamicText.Replace("$10.00 Fee", "$" + GrandChilds.InnerText + " Fee");
                                    //lblFM9.Text = DynamicText;
                                    //DivAlert += DynamicText;
                                    //DynamicText = DefaultText;
                                    //DivAlert += @"<img alt="""" src=""Images2/GreyLine.png"" style=""height: 6px; margin-top: 0px;"" width=""85%;"" />";
                                    //DivAlert += @"</b></p>";
                                    continue;
                                }
                                if (GrandChilds.Name == "DownPay9")
                                {
                                    DivAlert += @"<p><b>";
                                    DynamicText = DynamicText.Replace("$100.00", "$" + GrandChilds.InnerText);
                                    // DynamicText = DynamicText.Replace("$100.00 Deposit", "$" + GrandChilds.InnerText + " Deposit");
                                    continue;
                                }
                                if (GrandChilds.Name == "NumberOfPay9")
                                {
                                    // DynamicText = DynamicText.Replace("10 Payments", GrandChilds.InnerText + " Payments");
                                    continue;
                                }
                                if (GrandChilds.Name == "MonthlyPayment9")
                                {
                                    //DynamicText = DynamicText.Replace("$100.00 Payment", "$" + GrandChilds.InnerText + " Payment");
                                    DynamicText2 = DynamicText2.Replace("$100.00", "$" + GrandChilds.InnerText);
                                    continue;
                                }
                                if (GrandChilds.Name == "APR9")
                                {
                                    // DynamicText = DynamicText.Replace("10.00% APR", GrandChilds.InnerText + "%" + " APR");
                                    continue;
                                }
                                if (GrandChilds.Name == "Interest9")
                                {
                                    // DynamicText = DynamicText.Replace("$100.00 Interest", "$" + GrandChilds.InnerText + " Interest");
                                    continue;
                                }
                                if (GrandChilds.Name == "SetupFee9")
                                {
                                    //DynamicText = DynamicText.Replace("$10.00 Fee", "$" + GrandChilds.InnerText + " Fee");
                                    lblFM8.Text = DynamicText;
                                    LabelFM8_Payment.Text = DynamicText2;
                                    DivAlert += DynamicText;
                                    DynamicText = DefaultText1;
                                    DynamicText2 = DefaultText2;
                                    DivAlert += @"<img alt="""" src=""Images2/GreyLine.png"" style=""height: 6px; margin-top: 0px;"" width=""85%;"" />";
                                    DivAlert += @"</b></p>";
                                    continue;
                                }
                                if (GrandChilds.Name == "DownPay8")
                                {
                                    //DivAlert += @"<p><b>";
                                    //DynamicText = DynamicText.Replace("$100.00 Deposit", "$" + GrandChilds.InnerText + " Deposit");
                                    continue;
                                }
                                if (GrandChilds.Name == "NumberOfPay8")
                                {
                                    // DynamicText = DynamicText.Replace("10 Payments", GrandChilds.InnerText + " Payments");
                                    continue;
                                }
                                if (GrandChilds.Name == "MonthlyPayment8")
                                {
                                    // DynamicText = DynamicText.Replace("$100.00 Payment", "$" + GrandChilds.InnerText + " Payment");
                                    continue;
                                }
                                if (GrandChilds.Name == "APR8")
                                {
                                    // DynamicText = DynamicText.Replace("10.00% APR", GrandChilds.InnerText + "%" + " APR");
                                    continue;
                                }
                                if (GrandChilds.Name == "Interest8")
                                {
                                    //DynamicText = DynamicText.Replace("$100.00 Interest", "$" + GrandChilds.InnerText + " Interest");
                                    continue;
                                }
                                if (GrandChilds.Name == "SetupFee8")
                                {
                                    //DynamicText = DynamicText.Replace("$10.00 Fee", "$" + GrandChilds.InnerText + " Fee");
                                    //lblFM7.Text = DynamicText;
                                    //DivAlert += DynamicText;
                                    //DynamicText = DefaultText;
                                    //DivAlert += @"<img alt="""" src=""Images2/GreyLine.png"" style=""height: 6px; margin-top: 0px;"" width=""85%;"" />";
                                    //DivAlert += @"</b></p>";
                                    continue;
                                }
                                if (GrandChilds.Name == "DownPay7")
                                {
                                    //DivAlert += @"<p><b>";
                                    //DynamicText = DynamicText.Replace("$100.00 Deposit", "$" + GrandChilds.InnerText + " Deposit");
                                    continue;
                                }
                                if (GrandChilds.Name == "NumberOfPay7")
                                {
                                    // DynamicText = DynamicText.Replace("10 Payments", GrandChilds.InnerText + " Payments");
                                    continue;
                                }
                                if (GrandChilds.Name == "MonthlyPayment7")
                                {
                                    // DynamicText = DynamicText.Replace("$100.00 Payment", "$" + GrandChilds.InnerText + " Payment");
                                    continue;
                                }
                                if (GrandChilds.Name == "APR7")
                                {
                                    // DynamicText = DynamicText.Replace("10.00% APR", GrandChilds.InnerText + "%" + " APR");
                                    continue;
                                }
                                if (GrandChilds.Name == "Interest7")
                                {
                                    // DynamicText = DynamicText.Replace("$100.00 Interest", "$" + GrandChilds.InnerText + " Interest");
                                    continue;
                                }
                                if (GrandChilds.Name == "SetupFee7")
                                {
                                    //DynamicText = DynamicText.Replace("$10.00 Fee", "$" + GrandChilds.InnerText + " Fee");
                                    //lblFM6.Text = DynamicText;
                                    //DivAlert += DynamicText;
                                    //DynamicText = DefaultText;
                                    //DivAlert += @"<img alt="""" src=""Images2/GreyLine.png"" style=""height: 6px; margin-top: 0px;"" width=""85%;"" />";
                                    //DivAlert += @"</b></p>";
                                    continue;
                                }
                                if (GrandChilds.Name == "DownPay6")
                                {
                                    DivAlert += @"<p><b>";
                                    //DynamicText = DynamicText.Replace("$100.00 Deposit", "$" + GrandChilds.InnerText + " Deposit");
                                    DynamicText = DynamicText.Replace("$100.00", "$" + GrandChilds.InnerText);
                                    continue;
                                }
                                if (GrandChilds.Name == "NumberOfPay6")
                                {
                                    //DynamicText = DynamicText.Replace("10 Payments", GrandChilds.InnerText + " Payments");
                                    continue;
                                }
                                if (GrandChilds.Name == "MonthlyPayment6")
                                {
                                    //DynamicText = DynamicText.Replace("$100.00 Payment", "$" + GrandChilds.InnerText + " Payment");
                                    DynamicText2 = DynamicText2.Replace("$100.00", "$" + GrandChilds.InnerText);
                                    continue;
                                }
                                if (GrandChilds.Name == "APR6")
                                {
                                    //DynamicText = DynamicText.Replace("10.00% APR", GrandChilds.InnerText + "%" + " APR");
                                    continue;
                                }
                                if (GrandChilds.Name == "Interest6")
                                {
                                    //DynamicText = DynamicText.Replace("$100.00 Interest", "$" + GrandChilds.InnerText + " Interest");
                                    continue;
                                }
                                if (GrandChilds.Name == "SetupFee6")
                                {
                                    //DynamicText = DynamicText.Replace("$10.00 Fee", "$" + GrandChilds.InnerText + " Fee");
                                    lblFM5.Text = DynamicText;
                                    LabelFM5_Payment.Text = DynamicText2;
                                    DivAlert += DynamicText;
                                    DynamicText = DefaultText1;
                                    DynamicText2 = DefaultText2;
                                    DivAlert += @"<img alt="""" src=""Images2/GreyLine.png"" style=""height: 6px; margin-top: 0px;"" width=""85%;"" />";
                                    DivAlert += @"</b></p>";
                                    continue;
                                }
                                if (GrandChilds.Name == "DownPay5")
                                {
                                    //DivAlert += @"<p><b>";
                                    //DynamicText = DynamicText.Replace("$100.00 Deposit", "$" + GrandChilds.InnerText + " Deposit");
                                    continue;
                                }
                                if (GrandChilds.Name == "NumberOfPay5")
                                {
                                    //DynamicText = DynamicText.Replace("10 Payments", GrandChilds.InnerText + " Payments");
                                    continue;
                                }
                                if (GrandChilds.Name == "MonthlyPayment5")
                                {
                                    //DynamicText = DynamicText.Replace("$100.00 Payment", "$" + GrandChilds.InnerText + " Payment");
                                    continue;
                                }
                                if (GrandChilds.Name == "APR5")
                                {
                                    //DynamicText = DynamicText.Replace("10.00% APR", GrandChilds.InnerText + "%" + " APR");
                                    continue;
                                }
                                if (GrandChilds.Name == "Interest5")
                                {
                                    //DynamicText = DynamicText.Replace("$100.00 Interest", "$" + GrandChilds.InnerText + " Interest");
                                    continue;
                                }
                                if (GrandChilds.Name == "SetupFee5")
                                {
                                    //DynamicText = DynamicText.Replace("$10.00 Fee", "$" + GrandChilds.InnerText + " Fee");
                                    //lblFM4.Text = DynamicText;
                                    //DivAlert += DynamicText;
                                    //DynamicText = DefaultText;
                                    //DivAlert += @"<img alt="""" src=""Images2/GreyLine.png"" style=""height: 6px; margin-top: 0px;"" width=""85%;"" />";
                                    //DivAlert += @"</b></p>";
                                    continue;
                                }
                                if (GrandChilds.Name == "DownPay4")
                                {
                                    //DivAlert += @"<p><b>";
                                    //DynamicText = DynamicText.Replace("$100.00 Deposit", "$" + GrandChilds.InnerText + " Deposit");
                                    continue;
                                }
                                if (GrandChilds.Name == "NumberOfPay4")
                                {
                                    //DynamicText = DynamicText.Replace("10 Payments", GrandChilds.InnerText + " Payments");
                                    continue;
                                }
                                if (GrandChilds.Name == "MonthlyPayment4")
                                {
                                    //DynamicText = DynamicText.Replace("$100.00 Payment", "$" + GrandChilds.InnerText + " Payment");
                                    continue;
                                }
                                if (GrandChilds.Name == "APR4")
                                {
                                    //DynamicText = DynamicText.Replace("10.00% APR", GrandChilds.InnerText + "%" + " APR");
                                    continue;
                                }
                                if (GrandChilds.Name == "Interest4")
                                {
                                    //DynamicText = DynamicText.Replace("$100.00 Interest", "$" + GrandChilds.InnerText + " Interest");
                                    continue;
                                }
                                if (GrandChilds.Name == "SetupFee4")
                                {
                                    //DynamicText = DynamicText.Replace("$10.00 Fee", "$" + GrandChilds.InnerText + " Fee");
                                    //lblFM3.Text = DynamicText;
                                    //DivAlert += DynamicText;
                                    //DynamicText = DefaultText;
                                    //DivAlert += @"<img alt="""" src=""Images2/GreyLine.png"" style=""height: 6px; margin-top: 0px;"" width=""85%;"" />";
                                    //DivAlert += @"</b></p>";
                                    continue;
                                }
                                if (GrandChilds.Name == "DownPay3")
                                {
                                    DivAlert += @"<p><b>";
                                    //DynamicText = DynamicText.Replace("$100.00 Deposit", "$" + GrandChilds.InnerText + " Deposit");
                                    DynamicText = DynamicText.Replace("$100.00", "$" + GrandChilds.InnerText);
                                    continue;
                                }
                                if (GrandChilds.Name == "NumberOfPay3")
                                {
                                    //DynamicText = DynamicText.Replace("10 Payments", GrandChilds.InnerText + " Payments");
                                    continue;
                                }
                                if (GrandChilds.Name == "MonthlyPayment3")
                                {
                                    // DynamicText = DynamicText.Replace("$100.00 Payment", "$" + GrandChilds.InnerText + " Payment");
                                    DynamicText2 = DynamicText2.Replace("$100.00", "$" + GrandChilds.InnerText);
                                    continue;
                                }
                                if (GrandChilds.Name == "APR3")
                                {
                                    //DynamicText = DynamicText.Replace("10.00% APR", GrandChilds.InnerText + "%" + " APR");
                                    continue;
                                }
                                if (GrandChilds.Name == "Interest3")
                                {
                                    //DynamicText = DynamicText.Replace("$100.00 Interest", "$" + GrandChilds.InnerText + " Interest");
                                    continue;
                                }
                                if (GrandChilds.Name == "SetupFee3")
                                {
                                    //DynamicText = DynamicText.Replace("$10.00 Fee", "$" + GrandChilds.InnerText + " Fee");
                                    lblFM2.Text = DynamicText;
                                    LabelFM2_Payment.Text = DynamicText2;
                                    DivAlert += DynamicText;
                                    DynamicText = DefaultText1;
                                    DynamicText2 = DefaultText2;
                                    DivAlert += @"<img alt="""" src=""Images2/GreyLine.png"" style=""height: 6px; margin-top: 0px;"" width=""85%;"" />";
                                    DivAlert += @"</b></p>";
                                    continue;
                                }
                                if (GrandChilds.Name == "DownPay2")
                                {
                                    //DivAlert += @"<p><b>";
                                    //DynamicText = DynamicText.Replace("$100.00 Deposit", "$" + GrandChilds.InnerText + " Deposit");
                                    continue;
                                }
                                if (GrandChilds.Name == "NumberOfPay2")
                                {
                                    //DynamicText = DynamicText.Replace("10 Payments", GrandChilds.InnerText + " Payments");
                                    continue;
                                }
                                if (GrandChilds.Name == "MonthlyPayment2")
                                {
                                    //DynamicText = DynamicText.Replace("$100.00 Payment", "$" + GrandChilds.InnerText + " Payment");
                                    continue;
                                }
                                if (GrandChilds.Name == "APR2")
                                {
                                    //DynamicText = DynamicText.Replace("10.00% APR", GrandChilds.InnerText + "%" + " APR");
                                    continue;
                                }
                                if (GrandChilds.Name == "Interest2")
                                {
                                    //DynamicText = DynamicText.Replace("$100.00 Interest", "$" + GrandChilds.InnerText + " Interest");
                                    continue;
                                }
                                if (GrandChilds.Name == "SetupFee2")
                                {
                                    //DynamicText = DynamicText.Replace("$10.00 Fee", "$" + GrandChilds.InnerText + " Fee");
                                    //lblFM1.Text = DynamicText;
                                    //DivAlert += DynamicText;
                                    //DynamicText = DefaultText;
                                    //DivAlert += @"<img alt="""" src=""Images2/GreyLine.png"" style=""height: 6px; margin-top: 0px;"" width=""85%;"" />";
                                    //DivAlert += @"</b></p>";
                                    continue;
                                }
                                if (GrandChilds.Name == "AmountFinanced")
                                { continue; }
                                if (GrandChilds.Name == "QuoteAgreement")
                                { continue; }
                            }
                        }
                    }
            }
            DivAlert += "<div>";
            //phAlert2.Controls.Add(new Literal() { Text = DivAlert });
            mpeAlert2.Show();
        }
        catch (Exception exp)
        {
            mpeSeleccion.Show();
            lblRecHeader.Text = exp.Message;
        }
    }

    private FinanceMaster FillFinanceMaster(FinanceMaster FM)
    {

        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        FinanceMaster.Bank[] Banks = GetFMBanks();

        FM.AgencyCode = (new FinanceMaster()).GetFMID("Agent", String.Format(" LTRIM(RTRIM(AgentID)) = '{0}'", ddlAgency.SelectedItem.Value));
        FM.PolicyNumber = txtPolicyNo.Text.Trim();
        if (taskControl.isQuote == true)
        {
            if (radioBtnTP1.Checked)
            {
                FM.PolicyPremium = txtTotalPremium.Text.Trim();
            }
            else if (radioBtnTP2.Checked)
            {
                FM.PolicyPremium = txtTotalPremium2.Text.Trim();
            }
        }
        else
        {
            FM.PolicyPremium = txtTotalPremiumPoliza.Text.Trim();
        }
        FM.PolicyFee = "0.00";
        FM.PolicyTax = "0.00";
        FM.PersonalOrComm = chkIsCommercial.Checked ? "Commercial" : "Private";
        FM.NewOrRenewal = "Y";
        FM.AssignedRisk = "Y";
        FM.Othernner = "N";
        FM.Auditable = "N";
        FM.ParameterCode = "VI";
        FM.Term = "12";
        FM.ShortRate = "N";
        FM.WebQuoteODBC = ConfigurationManager.AppSettings["WebQuoteODBC"];
        FM.FMQODBCnn = ConfigurationManager.AppSettings["FMQODBC"];
        FM.ProcessType = ConfigurationManager.AppSettings["ProcessType"];
        FM.NumOfPayments = "0";
        //FM.InsuranceCode = "C102";
        FM.InsuranceCode = "C0003";
        FM.Lien1 = Banks[0].FMID;
        FM.Lien1Name = Banks[0].Name;
        FM.Lien1Address = Banks[0].Address;
        FM.Lien1City = Banks[0].City;
        FM.Lien1State = Banks[0].State;
        FM.Lien1Zip = Banks[0].Zip;
        FM.Lien1b = Banks[1].FMID;
        FM.Lien1bName = Banks[1].Name;
        FM.Lien1bAddress = Banks[1].Address;
        FM.Lien1bCity = Banks[1].City;
        FM.Lien1bState = Banks[1].State;
        FM.Lien1bZip = Banks[1].Zip;
        FM.Coverage1 = "MARINE";
        return FM;
    }

    private FinanceMaster.Bank[] GetFMBanks()
    {
        FinanceMaster.Bank[] Banks = new FinanceMaster.Bank[2].Select(h => new FinanceMaster.Bank()).ToArray();
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        if (taskControl != null)
        {
            if (ddlBank.SelectedIndex > 0)
            {
                Banks[0].FMID = (new FinanceMaster()).
                    GetFMID("Bank_VI",
                    String.Format(" LTRIM(RTRIM(PPSID)) = '{0}'",
                    ddlBank.SelectedItem.Value)
                    );
                Banks[0].PPSID = ddlBank.SelectedItem.Value;
                if (!string.IsNullOrEmpty(Banks[0].PPSID.Trim()))
                    FillBank(Banks[0]);
            }

        }

        return Banks;
    }

    private FinanceMaster.Bank FillBank(FinanceMaster.Bank Bank)
    {
        string conString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"].ToString();
        DataTable table = new DataTable();
        using (var con = new SqlConnection(conString))
        using (var cmd = new SqlCommand(
            String.Format(@"SELECT * FROM BANK_VI WHERE PPSID = {0}"
            , Bank.PPSID), con))
        using (var da = new SqlDataAdapter(cmd))
        {
            cmd.CommandType = CommandType.Text;
            da.Fill(table);
        }
        if (table.Rows.Count > 0)
        {
            if (table.Rows[0]["BankDesc"].ToString().Trim() != "")
                Bank.Name = table.Rows[0]["BankDesc"].ToString().Trim();
            if (table.Rows[0]["Address1"].ToString().Trim() != "")
                Bank.Address = table.Rows[0]["Address1"].ToString().Trim();
            if (table.Rows[0]["Address2"].ToString().Trim() != "")
                Bank.Address = table.Rows[0]["Address2"].ToString().Trim();
            if (table.Rows[0]["City"].ToString().Trim() != "")
                Bank.City = table.Rows[0]["City"].ToString().Trim();
            if (table.Rows[0]["State"].ToString().Trim() != "")
                Bank.State = table.Rows[0]["State"].ToString().Trim();
            if (table.Rows[0]["ZipCode"].ToString().Trim() != "")
                Bank.Zip = table.Rows[0]["ZipCode"].ToString().Trim();
        }

        return Bank;
    }

    protected void btnFinanceMaster_Click(object sender, EventArgs e)
    {
        try
        {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
            FinanceMaster FM = new FinanceMaster();
            string redirUrl = "";
            //NUMBER OF PAYMENTS
            var SelectedBtn = (int.Parse(((System.Web.UI.WebControls.Button)sender).ID.ToString().Replace("btnFM", "")) + 1).ToString();
            var Params = Session["FMQuoteCode"].ToString() + "&Name=" +
                (txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim() == "") +
                "&Address1=" + txtAddrs1.Text.Trim() + "&City=" + txtCiudad.Text.Trim() + "&State=" + txtState.Text.Trim() +
                "&Zip=" + txtZip.Text.Trim() + "&HomePhone=" + txtHomePhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") +
                "&WorkPhone=" + txtWorkPhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "") +
                "&NewNumberOfPay=" + SelectedBtn + "&PROP_LOCATION=VA";

            string Installment = "";
            string DownPayment = "";
            string UnpaidBalance = "";
            string Charge = "";
            string TotalPayment = "";

            if (Convert.ToInt32(SelectedBtn) == 3)
            {
                DownPayment = lblFM2.Text.Trim().Replace("$", "");
                Installment = LabelFM2_Payment.Text.Trim().Replace("$", "");
            }

            if (Convert.ToInt32(SelectedBtn) == 6)
            {
                DownPayment = lblFM5.Text.Trim().Replace("$", "");
                Installment = LabelFM5_Payment.Text.Trim().Replace("$", "");
            }

            if (Convert.ToInt32(SelectedBtn) == 9)
            {
                DownPayment = lblFM8.Text.Trim().Replace("$", "");
                Installment = LabelFM8_Payment.Text.Trim().Replace("$", "");
            }

            double totpay = Convert.ToDouble(SelectedBtn) * Convert.ToDouble(Installment);
            TotalPayment = totpay.ToString();
            double chg = (totpay + Convert.ToDouble(DownPayment)) - double.Parse(txtTotalPremiumPoliza.Text.Replace("$", "").Replace(",", "").Trim()); //TotalPremium
            Charge = chg.ToString();
            double unpBal = totpay - chg;
            UnpaidBalance = unpBal.ToString();

            FM.FinanceMasterWS(Session["FMQuoteCode"].ToString(), Params, Convert.ToInt32(SelectedBtn), out redirUrl, taskControl.TaskControlID, SelectedBtn.ToString(), DownPayment, UnpaidBalance, Charge, TotalPayment);
            SendFinanceMasterInfoToPPS(taskControl);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('" + redirUrl + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
            mpeAlert2.Hide();
        }
        catch (Exception exp)
        {
            mpeSeleccion.Show();
            lblRecHeader.Text = exp.Message;
        }

    }

    private void SendFinanceMasterInfoToPPS(EPolicy.TaskControl.Yacht taskControl)
    {
        System.Data.SqlClient.SqlConnection cn = new System.Data.SqlClient.SqlConnection();
        cn.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
        cn.Open();

        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cn;
        cmd.CommandType = CommandType.Text;

        FinanceMaster fm = new FinanceMaster();
        string FMQuoteID = "0";
        string FMDownPayment = "";
        string FMUnpaidBalance = "";
        string PolicyID = taskControl.Suffix.Trim() == "00" ? taskControl.PolicyType.Trim() + taskControl.PolicyNo.Trim() : taskControl.PolicyType.Trim() + taskControl.PolicyNo.Trim() + "-" + taskControl.Suffix.Trim();
        DataTable dtfm = fm.GetFinanceMasterByTaskcontrolIDAllData(taskControl.TaskControlID);

        if (dtfm.Rows.Count > 0)
        {
            FMQuoteID = dtfm.Rows[0]["FMQuoteID"].ToString().Replace("**", "");
            FMDownPayment = dtfm.Rows[0]["DownPayment"].ToString();
            FMUnpaidBalance = dtfm.Rows[0]["UnpaidBalance"].ToString();
        }

        cmd.CommandText = "exec sproc_ConsumeXMLePPS_FinanceMaster @PolicyID = '" + PolicyID +
            "', @FMQuoteID = '" + FMQuoteID + "', @FMDownPayment = '" + FMDownPayment + "', @FMUnpaidBalance = '" + FMUnpaidBalance + "'";
        cmd.Parameters.Clear();
        //cmd.Parameters.AddWithValue("@PremiumEndorAmount", txtAdditionalPremium.Text.Trim());
        cmd.CommandTimeout = 0;

        int c = cmd.ExecuteNonQuery();

        cn.Close();
    }

    protected void btnSentToPPS_Click(object sender, EventArgs e)
    {
        try
        {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
            SendPolicyToPPS(taskControl.TaskControlID);
            DataTable dtTender = GetYachtToPPSByTaskControlIDInfoTenderLimit(taskControl.TaskControlID);
            if (dtTender.Rows.Count > 0 && dtTender.Rows[0]["TenderLimitAmount2"].ToString() != "")
            {
                SendPolicyToPPSTender2(taskControl.TaskControlID);
            }
        }
        catch (Exception exp)
        {
            LogError(exp);
            DisableControl();
            FillTextControl();
            lblRecHeader.Text = "The policy was sent to pps, please verify.";
            mpeSeleccion.Show();
        }
    }

    private bool GetIsMandatoryHomeport(string HomeportID)
    {
        DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[1];

        DbRequestXmlCooker.AttachCookItem("HomeportID",
            SqlDbType.Int, 0, HomeportID,
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
            dt = exec.GetQuery("GetIsMandatoryHomeport", xmlDoc);
            if (dt.Rows[0]["isMandatory"].ToString().Trim() == "")
                return false;
            else
                return bool.Parse(dt.Rows[0]["isMandatory"].ToString());
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve data from database.", ex);
        }

    }

    private string GetHomeportReportName(string HomeportID, bool isQuoteOrPolicy)
    {
        DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[2];

        DbRequestXmlCooker.AttachCookItem("HomeportID",
            SqlDbType.Int, 0, HomeportID,
            ref cookItems);

        DbRequestXmlCooker.AttachCookItem("isQuoteOrPolicy",
            SqlDbType.Bit, 0, isQuoteOrPolicy.ToString(),
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
            dt = exec.GetQuery("GetHomeportReportName", xmlDoc);

            if (isQuoteOrPolicy == true)
            {
                if (dt.Rows[0]["HomeportReportName"].ToString().Trim() != "")
                    return dt.Rows[0]["HomeportReportName"].ToString().Trim();
                else
                    return "YachtPortEndorsement.rdlc";
            }
            else
            {
                if (dt.Rows[0]["HomeportReportName2"].ToString().Trim() != "")
                    return dt.Rows[0]["HomeportReportName2"].ToString().Trim();
                else
                    return "YachtPortEndorsementQuote.rdlc";
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve data from database.", ex);
        }

    }

    private DataTable GetNavigationLimitIDByPPSDesc(string NavLimitPPS)
    {
        DbRequestXmlCookRequestItem[] cookItems =
           new DbRequestXmlCookRequestItem[1];

        DbRequestXmlCooker.AttachCookItem("NavLimitPPS",
            SqlDbType.VarChar, 500, NavLimitPPS,
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
            dt = exec.GetQuery("GetNavigationLimitIDByPPSDesc", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve data from database.", ex);
        }
    }

    protected void btnEndor_Click(object sender, EventArgs e)
    {
        EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
        int userID = 0;
        userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

        //Eliminar en OppEndorsement los casos que no tienen numero de endosos para que el usuario no se confunda.
        DeleteOppEndorsementByTaskControlIDNotInUse(taskControl.TaskControlID);


        EPolicy.TaskControl.Yacht taskControlQuote = new EPolicy.TaskControl.Yacht(false);

        int tcID = taskControl.TaskControlID;

        taskControlQuote.Prospect.FirstName = txtFirstName.Text.ToString().Trim().ToUpper();
        taskControlQuote.Prospect.LastName1 = txtLastName.Text.ToString().Trim().ToUpper();
        taskControlQuote.Prospect.HomePhone = txtHomePhone.Text.ToString().Trim().ToUpper();
        taskControlQuote.Prospect.WorkPhone = txtWorkPhone.Text.ToString().Trim().ToUpper();
        taskControlQuote.Prospect.Cellular = txtCellular.Text.ToString().Trim().ToUpper();
        taskControlQuote.Prospect.Email = txtemail.Text.ToString().Trim().ToUpper();

        //Para aplicar el ultimo endoso, sino a la poliza original
        DataTable endososList = PersonalPackage.GetEndorsementByEndoNum(tcID);
        if (endososList.Rows.Count == 0)
        {
            taskControlQuote = taskControl;
            taskControlQuote.Mode = 1; //ADD
            taskControlQuote.TaskControlID = 0;
            taskControlQuote.isQuote = true;//.IsPolicy = false;
            taskControlQuote.IsEndorsement = true;
            taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Yacht Quote"));

            
            taskControlQuote.TenderLimitCollection = taskControl.TenderLimitCollection.Copy();
            taskControlQuote.SurveyCollection = taskControl.SurveyCollection.Copy();
            taskControlQuote.NavigationLimitCollection = taskControl.NavigationLimitCollection.Copy();
            taskControlQuote.DeductibleID1 = taskControl.DeductibleIDPoliza;
            taskControlQuote.WatercraftLimit1 = taskControl.WatercraftLimitPoliza;
            taskControlQuote.Rate1 = taskControl.RatePoliza;
            taskControlQuote.WatercraftLimitTotal1 = taskControl.WatercraftLimitTotalPoliza;
            taskControlQuote.TotalPremium1 = taskControl.TotalPremiumPoliza;


        }
        else
        {
            //Aplica al Ultimo endoso
            bool isExistEndo = false;
            EPolicy.TaskControl.Yacht taskControlEndo = null; ;
            for (int s = 1; s <= endososList.Rows.Count; s++)
            {
                if ((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"] != 0)
                {
                    taskControlEndo = EPolicy.TaskControl.Yacht.GetYacht((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"], true);
                    isExistEndo = true;
                    s = endososList.Rows.Count;
                }
            }

            if (!isExistEndo)
            {
                taskControlQuote = taskControl;
                taskControlQuote.Mode = 1; //ADD
                taskControlQuote.TaskControlID = 0;
                taskControlQuote.isQuote = true;//.IsPolicy = false;
                taskControlQuote.IsEndorsement = true;
                taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Yacht Quote"));

                taskControlQuote.TenderLimitCollection = taskControl.TenderLimitCollection.Copy();
                taskControlQuote.SurveyCollection = taskControl.SurveyCollection.Copy();
                taskControlQuote.NavigationLimitCollection = taskControl.NavigationLimitCollection.Copy();
                taskControlQuote.DeductibleID1 = taskControl.DeductibleIDPoliza;
                taskControlQuote.WatercraftLimit1 = taskControl.WatercraftLimitPoliza;
                taskControlQuote.Rate1 = taskControl.RatePoliza;
                taskControlQuote.WatercraftLimitTotal1 = taskControl.WatercraftLimitTotalPoliza;
                taskControlQuote.TotalPremium1 = taskControl.TotalPremiumPoliza;
                

            }
            else
            {
                taskControlQuote = taskControlEndo;
                taskControlQuote.Mode = 1; //ADD
                taskControlQuote.TaskControlID = 0;
                taskControlQuote.isQuote = true;//.IsPolicy = false;
                taskControlQuote.IsEndorsement = true;
                taskControlQuote.TaskControlTypeID = int.Parse(EPolicy.LookupTables.LookupTables.GetID("TaskControlType", "Yacht Quote"));

                taskControlQuote.Prospect.FirstName = txtFirstName.Text.ToString().Trim();
                taskControlQuote.Prospect.LastName1 = txtLastName.Text.ToString().Trim();
                taskControlQuote.Prospect.HomePhone = txtHomePhone.Text.ToString().Trim();
                taskControlQuote.Prospect.WorkPhone = txtWorkPhone.Text.ToString().Trim();
                taskControlQuote.Prospect.Cellular = txtCellular.Text.ToString().Trim();
                taskControlQuote.Prospect.Email = txtemail.Text.ToString().Trim();

                taskControlQuote.Customer.FirstName = taskControl.Customer.FirstName;
                taskControlQuote.Customer.LastName1 = taskControl.Customer.LastName1;
                taskControlQuote.Customer.LastName2 = taskControl.Customer.LastName2;
                taskControlQuote.Customer.Initial = taskControl.Customer.Initial;
                taskControlQuote.Customer.Sex = taskControl.Customer.Sex;
                taskControlQuote.Customer.Address1 = taskControl.Customer.Address1;
                taskControlQuote.Customer.City = taskControl.Customer.City;
                taskControlQuote.Customer.State = taskControl.Customer.State;
                taskControlQuote.Customer.ZipCode = taskControl.Customer.ZipCode;
                taskControlQuote.Customer.Licence = taskControl.Customer.Licence;
                taskControlQuote.Customer.MaritalStatus = taskControl.Customer.MaritalStatus;
                taskControlQuote.Customer.Birthday = taskControl.Customer.Birthday;
                taskControlQuote.Customer.Age = taskControl.Customer.Age;
                taskControlQuote.Customer.JobPhone = taskControl.Customer.JobPhone;
                taskControlQuote.Customer.HomePhone = taskControl.Customer.HomePhone;
                taskControlQuote.Customer.OccupationID = taskControl.Customer.OccupationID;
                taskControlQuote.Customer.Occupation = taskControl.Customer.Occupation;
                taskControlQuote.Customer.EmplName = taskControl.Customer.EmplName;
                taskControlQuote.Customer.Address1 = taskControl.Customer.Address1;
                taskControlQuote.Customer.Address2 = taskControl.Customer.Address2;
                taskControlQuote.Customer.City = taskControl.Customer.City;
                taskControlQuote.Customer.State = taskControl.Customer.State;
                taskControlQuote.Customer.ZipCode = taskControl.Customer.ZipCode;
                taskControlQuote.Customer.AddressPhysical1 = taskControl.Customer.AddressPhysical1;
                taskControlQuote.Customer.AddressPhysical2 = taskControl.Customer.AddressPhysical2;
                taskControlQuote.Customer.CityPhysical = taskControl.Customer.CityPhysical;
                taskControlQuote.Customer.StatePhysical = taskControl.Customer.StatePhysical;
                taskControlQuote.Customer.ZipPhysical = taskControl.Customer.ZipPhysical;
                taskControlQuote.Customer.Email = taskControl.Customer.Email;
                taskControlQuote.Customer.JobPhone = taskControl.Customer.JobPhone;
                taskControlQuote.Customer.Cellular = taskControl.Customer.Cellular;
                taskControlQuote.Customer.HomePhone = taskControl.Customer.HomePhone;

                taskControlQuote.Agency = taskControl.Agency;
                taskControlQuote.Agent = taskControl.Agent;
                taskControlQuote.Bank = taskControl.Bank;
                taskControlQuote.CompanyDealer = taskControl.CompanyDealer;
                taskControlQuote.InsuranceCompany = taskControl.InsuranceCompany;
                taskControlQuote.OriginatedAt = taskControl.OriginatedAt;
                taskControlQuote.LbxAgentSelected = taskControl.LbxAgentSelected;
                taskControlQuote.LbxAgentSelected = taskControl.LbxAgentSelected;
                taskControlQuote.PolicyClassID = taskControl.PolicyClassID;

                taskControlQuote.TenderLimitCollection = taskControlEndo.TenderLimitCollection.Copy();
                taskControlQuote.SurveyCollection = taskControlEndo.SurveyCollection.Copy();
                taskControlQuote.NavigationLimitCollection = taskControlEndo.NavigationLimitCollection.Copy();
                taskControlQuote.DeductibleID1 = taskControlEndo.DeductibleID1;
                taskControlQuote.WatercraftLimit1 = taskControlEndo.WatercraftLimit1;
                taskControlQuote.Rate1 = taskControlEndo.Rate1;
                taskControlQuote.WatercraftLimitTotal1 = taskControlEndo.WatercraftLimitTotal1;
                taskControlQuote.TotalPremium1 = taskControlEndo.TotalPremium1;

            }
        }

        taskControlQuote.Mode = 1; //ADD
        taskControlQuote.Term = taskControl.Term;
        taskControlQuote.TCIDQuotes = tcID;//taskControlQuote.TaskControlID;

        taskControlQuote.SaveYacht(userID);  //(userID);

        taskControlQuote.Mode = 2;

        EPolicy.TaskControl.Yacht taskControl2 = (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(tcID, userID);
        Session.Remove("TaskControl");
        Session.Add("TaskControl", taskControlQuote);
        Session.Add("AUTOEndorsement", taskControl2);

        //////////////////
        //Salvar AUTOEndorsement
        double mFactor = 0.0;
        double NewProRataTotPrem = 0.0;
        double NewShotRateTotPrem = 0.0;
        int taskControlIDPolicy = 0;
        //if (taskControlQuote.isQuote == true && Session["AUTOEndorsement"] != null)
        //{
        //    // Esta seccion es porque existe ya en la base de datos
        //    // y no hay que insertar nuevamente el quotes.
        //    if (Session["OPPEndorUpdate"] == null)
        //    {
        //        EPolicy.TaskControl.Autos OpptaskControl = (EPolicy.TaskControl.Autos)Session["AUTOEndorsement"];
        //        int oppEndoID = AddOPPEndorsement(OpptaskControl.TaskControlID, taskControlQuote.TaskControlID, mFactor, NewProRataTotPrem, NewShotRateTotPrem);
        //        taskControlIDPolicy = OpptaskControl.TaskControlID;
        //        Session.Add("OPPENDORID", oppEndoID);
        //    }
        //    else
        //    {
        //        EPolicy.TaskControl.Autos OpptaskControl = (EPolicy.TaskControl.Autos)Session["AUTOEndorsement"];
        //        taskControlIDPolicy = OpptaskControl.TaskControlID;
        //        Session.Remove("OPPEndorUpdate");
        //    }
        //}

        RemoveSessionLookUp();
        Response.Redirect("Yacht.aspx");
    }

    private void DeleteOppEndorsementByTaskControlIDNotInUse(int TaskControlID)
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

        try
        {
            exec.GetQuery("DeleteOppEndorsementByTaskControlIDNotInUse", xmlDoc);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve the data.", ex);
        }
    }

    private void RemoveSessionLookUp()
    {
        Session.Remove("LookUpTables");
    }

    private void PrintYachtPreview(bool Option1Or2)
    {
        try
        {
            FileInfo mFileIndex;
            List<string> mergePaths = new List<string>();
            string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

            mergePaths = ImprimirPolicyPreview(mergePaths, "YachtDecPageQuote.rdlc", Option1Or2);
            if (taskControl.HullLimit != "")
            {
                mergePaths = ImprimirPolicyPreview(mergePaths, "YachtEndorsmentAQuote.rdlc", Option1Or2);
            }
            if (taskControl.BankPPSID.Trim() != "000")
            {
                mergePaths = ImprimirPolicyPreview(mergePaths, "YachtEndorsementBQuote.rdlc", Option1Or2);
            }
            if (ddlHomeport.SelectedIndex != 0)
            {
                mergePaths = ImprimirPolicyPreview(mergePaths, GetHomeportReportName(ddlHomeport.SelectedItem.Value, false), Option1Or2);
            }
            mergePaths = ImprimirPolicyPreview(mergePaths, "YachtCertificateLiabilityQuote.rdlc", Option1Or2);
            if (ddlHomeport.SelectedIndex != 0)
            {
                mergePaths = ImprimirPolicyPreview(mergePaths, "YachtCertificateLiabilityMarinaQuote.rdlc", Option1Or2);
                mergePaths = ImprimirPolicyPreview(mergePaths, "YachtCertificateLiabilityMarinaAddInsuredQuote.rdlc", Option1Or2);
            }
            if (taskControl.BankPPSID.Trim() != "000")
            {
                mergePaths = ImprimirPolicyPreview(mergePaths, "YachtCertificateLiabilityBancoQuote.rdlc", Option1Or2);
            }

            //Generar PDF
            OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
            string FinalFile = "";
            FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());

            System.Diagnostics.Debug.Write("");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);

        }

    }

    private List<string> ImprimirPolicyPreview(List<string> mergePaths, string rdlcname, bool Option1Or2)
    {
        try
        {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

            int taskControl1 = taskControl.TaskControlID;

            GetYachtPolicyPreviewReportTableAdapters.GetYachtPolicyPreviewReportTableAdapter ta = new GetYachtPolicyPreviewReportTableAdapters.GetYachtPolicyPreviewReportTableAdapter();
            ReportDataSource rpd = new ReportDataSource();
            rpd = new ReportDataSource("GetYachtPolicyPreviewReport", (DataTable)ta.GetData(taskControl1, Option1Or2));

            GetReportTenderLimitByTaskControlIDTableAdapters.GetReportTenderLimitByTaskControlIDTableAdapter ta1 = new GetReportTenderLimitByTaskControlIDTableAdapters.GetReportTenderLimitByTaskControlIDTableAdapter();
            ReportDataSource rpd1 = new ReportDataSource();
            rpd1 = new ReportDataSource("GetReportTenderLimitByTaskControlID", (DataTable)ta1.GetData(taskControl1));

            GetTenderLimitDecPageByTaskControlIDTableAdapters.GetTenderLimitDecPageByTaskControlIDTableAdapter ta6 = new GetTenderLimitDecPageByTaskControlIDTableAdapters.GetTenderLimitDecPageByTaskControlIDTableAdapter();
            ReportDataSource rpd6 = new ReportDataSource();
            rpd6 = new ReportDataSource("GetTenderLimitDecPageByTaskControlID", (DataTable)ta6.GetData(taskControl1));

            var QRTaskcontrolID = taskControl1.ToString();

            if (!string.IsNullOrEmpty(QRTaskcontrolID))
                GenerateQRCode(ref QRTaskcontrolID);

            ReportParameter[] parameterQR = new ReportParameter[1];
            ReportParameter[] parameters = new ReportParameter[6];
            ReportParameter[] parametersDecPage = new ReportParameter[8];
            Uri pathasUri = null;
            if (QRTaskcontrolID != "")
            {
                pathasUri = new Uri(QRTaskcontrolID);
                parameterQR[0] = new ReportParameter("QRCode", pathasUri.AbsoluteUri);
            }
            else
            {
                parameterQR[0] = new ReportParameter("QRCode", QRTaskcontrolID != "" ? QRTaskcontrolID : "");
            }
            ReportViewer viewer1 = new ReportViewer();
            viewer1.LocalReport.DataSources.Clear();
            viewer1.ProcessingMode = ProcessingMode.Local;
            viewer1.LocalReport.EnableExternalImages = true;
            viewer1.LocalReport.ReportPath = Server.MapPath("Reports/Yacht/" + rdlcname); //Reports/Yacht/YachtNewQuote.rdlc


            if (rdlcname == "YachtEndorsmentAQuote.rdlc")
            {
                viewer1.LocalReport.DataSources.Add(rpd);
                viewer1.LocalReport.DataSources.Add(rpd6);
            }

            if (rdlcname == "YachtDecPageQuote.rdlc")
            {
                //  viewer1.LocalReport.DataSources.Add(rpd1);
                viewer1.LocalReport.DataSources.Add(rpd);
                viewer1.LocalReport.DataSources.Add(rpd6);
                parametersDecPage[0] = new ReportParameter("DeductibleCalculated", lblDeductibleCalculated1.Text);
                string firstOtherMedicalPayment, secondOtherMedicalPayment, firstMedicalPayment, secondMedicalPayment;
                string[] arrayOfOtherMedical = { "", "" };
                string[] arrayOfMedicalPayment = { "", "" };

                if (txtOtherMedicalPayment.Text.Trim() == "")
                {
                    parametersDecPage[1] = new ReportParameter("FirstOtherMedicalPayment", "");
                    parametersDecPage[2] = new ReportParameter("SecondOtherMedicalPayment", "");
                }
                else
                {
                    arrayOfOtherMedical = txtOtherMedicalPayment.Text.Split('/');
                    firstOtherMedicalPayment = arrayOfOtherMedical[0].Trim().Replace("$", "").Replace(",", "");
                    secondOtherMedicalPayment = arrayOfOtherMedical[1].Trim().Replace("$", "").Replace(",", "");
                    double firstOtherMedicalPaymentDouble = double.Parse(firstOtherMedicalPayment);
                    double secondOtherMedicalPaymentDouble = double.Parse(secondOtherMedicalPayment);
                    int firstOtherMedicalPaymentInt = (int)firstOtherMedicalPaymentDouble;
                    int secondOtherMedicalPaymentInt = (int)secondOtherMedicalPaymentDouble;
                    parametersDecPage[1] = new ReportParameter("FirstOtherMedicalPayment", firstOtherMedicalPaymentInt.ToString("C0"));
                    parametersDecPage[2] = new ReportParameter("SecondOtherMedicalPayment", secondOtherMedicalPaymentInt.ToString("C0"));
                }

                if (ddlMedicalPayment.SelectedItem.Text == "")
                {
                    parametersDecPage[3] = new ReportParameter("FirstMedicalPayment", "");
                    parametersDecPage[4] = new ReportParameter("SecondMedicalPayment", "");
                }
                else
                {
                    arrayOfMedicalPayment = ddlMedicalPayment.SelectedItem.Text.Split('/');
                    firstMedicalPayment = arrayOfMedicalPayment[0].Trim().Replace("$", "").Replace(",", "");
                    secondMedicalPayment = arrayOfMedicalPayment[1].Trim().Replace("$", "").Replace(",", "");
                    double firstMedicalPaymentDouble = double.Parse(firstMedicalPayment);
                    double secondMedicalPaymentDouble = double.Parse(secondMedicalPayment);
                    int firstMedicalPaymentInt = (int)firstMedicalPaymentDouble;
                    int secondMedicalPaymentInt = (int)secondMedicalPaymentDouble;
                    parametersDecPage[3] = new ReportParameter("FirstMedicalPayment", firstMedicalPaymentInt.ToString("C0"));
                    parametersDecPage[4] = new ReportParameter("SecondMedicalPayment", secondMedicalPaymentInt.ToString("C0"));
                }
                calculatelblDeductibleCalculatedTenderPreview(Option1Or2);
                parametersDecPage[5] = new ReportParameter("DeductibleCalculatedTender", lblDeductibleCalculatedTender1.Text);
                calculatelblDeductibleCalculatedTender2Preview(Option1Or2);
                parametersDecPage[6] = new ReportParameter("DeductibleCalculatedTender2", lblDeductibleCalculatedTender2.Text);

                DataTable dtNavigationLimit = taskControl.NavigationLimitCollection;
                string[] navigationcollection;
                if (dtNavigationLimit.Rows.Count == 0)
                {

                    navigationcollection = new string[1];
                    navigationcollection[0] = "";
                }
                else
                {
                    navigationcollection = new string[dtNavigationLimit.Rows.Count];
                    for (int i = 0; i < dtNavigationLimit.Rows.Count; i++)
                    {
                        navigationcollection[i] = dtNavigationLimit.Rows[i]["NavigationLimitDesc"].ToString();
                    }
                }
                parametersDecPage[7] = new ReportParameter("NavigationLimitCollection", navigationcollection);
                viewer1.LocalReport.SetParameters(parametersDecPage);
            }

            if (rdlcname == "YachtEndorsementBQuote.rdlc")
            {
                viewer1.LocalReport.DataSources.Add(rpd);
            }

            if (rdlcname == "YachtCertificateLiabilityQuote.rdlc" || rdlcname == "YachtCertificateLiabilityBancoQuote.rdlc" || rdlcname == "YachtCertificateLiabilityMarinaQuote.rdlc" || rdlcname == "YachtCertificateLiabilityMarinaAddInsuredQuote.rdlc")
            {
                viewer1.LocalReport.DataSources.Add(rpd1);
                viewer1.LocalReport.DataSources.Add(rpd);
                viewer1.LocalReport.DataSources.Add(rpd6);
                string firstOtherMedicalPayment, secondOtherMedicalPayment, firstMedicalPayment, secondMedicalPayment, firstDeductible, secondDeductible;
                string[] arrayOfOtherMedical = { "", "" };
                string[] arrayOfMedicalPayment = { "", "" };
                string[] arrayOfDeductible = { "", "" };

                if (txtOtherMedicalPayment.Text.Trim() == "")
                {
                    parameters[0] = new ReportParameter("FirstOtherMedicalPayment", "");
                    parameters[1] = new ReportParameter("SecondOtherMedicalPayment", "");
                }
                else
                {
                    arrayOfOtherMedical = txtOtherMedicalPayment.Text.Split('/');
                    firstOtherMedicalPayment = arrayOfOtherMedical[0].Trim().Replace("$", "").Replace(",", "");
                    secondOtherMedicalPayment = arrayOfOtherMedical[1].Trim().Replace("$", "").Replace(",", "");
                    double firstOtherMedicalPaymentDouble = double.Parse(firstOtherMedicalPayment);
                    double secondOtherMedicalPaymentDouble = double.Parse(secondOtherMedicalPayment);
                    int firstOtherMedicalPaymentInt = (int)firstOtherMedicalPaymentDouble;
                    int secondOtherMedicalPaymentInt = (int)secondOtherMedicalPaymentDouble;
                    parameters[0] = new ReportParameter("FirstOtherMedicalPayment", firstOtherMedicalPaymentInt.ToString("C0"));
                    parameters[1] = new ReportParameter("SecondOtherMedicalPayment", secondOtherMedicalPaymentInt.ToString("C0"));
                }

                if (ddlMedicalPayment.SelectedItem.Text == "")
                {
                    parameters[2] = new ReportParameter("FirstMedicalPayment", "");
                    parameters[3] = new ReportParameter("SecondMedicalPayment", "");
                }
                else
                {
                    arrayOfMedicalPayment = ddlMedicalPayment.SelectedItem.Text.Split('/');
                    firstMedicalPayment = arrayOfMedicalPayment[0].Trim().Replace("$", "").Replace(",", "");
                    secondMedicalPayment = arrayOfMedicalPayment[1].Trim().Replace("$", "").Replace(",", "");
                    double firstMedicalPaymentDouble = double.Parse(firstMedicalPayment);
                    double secondMedicalPaymentDouble = double.Parse(secondMedicalPayment);
                    int firstMedicalPaymentInt = (int)firstMedicalPaymentDouble;
                    int secondMedicalPaymentInt = (int)secondMedicalPaymentDouble;
                    parameters[2] = new ReportParameter("FirstMedicalPayment", firstMedicalPaymentInt.ToString("C0"));
                    parameters[3] = new ReportParameter("SecondMedicalPayment", secondMedicalPaymentInt.ToString("C0"));
                }
                if (Option1Or2)
                {
                    if (ddlDeductibles2.SelectedIndex != 0)
                    {
                        DataTable dt = GetDeductibleAmount(int.Parse(ddlDeductibles2.SelectedItem.Value));
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                            {
                                parameters[4] = new ReportParameter("FirstDeductible", ddlDeductibles2.SelectedItem.Text);
                                parameters[5] = new ReportParameter("SecondDeductible", "");
                            }
                            else
                            {
                                arrayOfDeductible = ddlDeductibles2.SelectedItem.Text.Split('/');
                                firstDeductible = arrayOfDeductible[0].Trim();
                                secondDeductible = arrayOfDeductible[1].Trim();
                                parameters[4] = new ReportParameter("FirstDeductible", firstDeductible);
                                parameters[5] = new ReportParameter("SecondDeductible", secondDeductible);
                            }
                        }
                        else
                        {
                            parameters[4] = new ReportParameter("FirstDeductible", "");
                            parameters[5] = new ReportParameter("SecondDeductible", "");
                        }
                    }
                    else
                    {
                        parameters[4] = new ReportParameter("FirstDeductible", "");
                        parameters[5] = new ReportParameter("SecondDeductible", "");
                    }
                }
                else
                {
                    if (ddlDeductibles1.SelectedIndex != 0)
                    {
                        DataTable dt = GetDeductibleAmount(int.Parse(ddlDeductibles1.SelectedItem.Value));
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == null || dt.Rows[0]["DeductibleAmount2"].ToString().Trim() == "")
                            {
                                parameters[4] = new ReportParameter("FirstDeductible", ddlDeductibles1.SelectedItem.Text);
                                parameters[5] = new ReportParameter("SecondDeductible", "");
                            }
                            else
                            {
                                arrayOfDeductible = ddlDeductibles1.SelectedItem.Text.Split('/');
                                firstDeductible = arrayOfDeductible[0].Trim();
                                secondDeductible = arrayOfDeductible[1].Trim();
                                parameters[4] = new ReportParameter("FirstDeductible", firstDeductible);
                                parameters[5] = new ReportParameter("SecondDeductible", secondDeductible);
                            }
                        }
                        else
                        {
                            parameters[4] = new ReportParameter("FirstDeductible", "");
                            parameters[5] = new ReportParameter("SecondDeductible", "");
                        }
                    }
                    else
                    {
                        parameters[4] = new ReportParameter("FirstDeductible", "");
                        parameters[5] = new ReportParameter("SecondDeductible", "");
                    }
                }
                viewer1.LocalReport.SetParameters(parameters);
            }

            if (rdlcname == "YachtPortEndorsementQuote.rdlc" || rdlcname == "YachtPDREndorsementQuote.rdlc" || rdlcname == "YachtMPCEndorsementQuote.rdlc")
            {
                viewer1.LocalReport.DataSources.Add(rpd);
            }

            viewer1.LocalReport.Refresh();

            Warning[] warnings = null;
            string[] streamIds = null;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            string filetype = string.Empty;

            string RandomString = Guid.NewGuid().ToString();
            string fileName1 = "";
            string _FileName1 = "";

            if (rdlcname == "YachtEndorsmentAQuote.rdlc")
            {
                fileName1 = "YachtEndorsmentAQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtEndorsmentAQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }
            else if (rdlcname == "YachtDecPageQuote.rdlc")
            {
                fileName1 = "YachtDecPageQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtDecPageQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }
            else if (rdlcname == "YachtEndorsementBQuote.rdlc")
            {
                fileName1 = "YachtEndorsementBQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtEndorsementBQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }
            else if (rdlcname == "YachtCertificateLiabilityQuote.rdlc")
            {
                fileName1 = "YachtCertificateLiabilityQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtCertificateLiabilityQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }
            else if (rdlcname == "YachtPortEndorsementQuote.rdlc")
            {
                fileName1 = "YachtPortEndorsementQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtPortEndorsementQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }

            else if (rdlcname == "YachtPDREndorsementQuote.rdlc")
            {
                fileName1 = "YachtPDREndorsementQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtPDREndorsementQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }

            else if (rdlcname == "YachtMPCEndorsementQuote.rdlc")
            {
                fileName1 = "YachtMPCEndorsementQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtMPCEndorsementQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }

            else if (rdlcname == "YachtCertificateLiabilityBancoQuote.rdlc")
            {
                fileName1 = "YachtCertificateLiabilityBancoQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtCertificateLiabilityBancoQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }

            else if (rdlcname == "YachtCertificateLiabilityMarinaQuote.rdlc")
            {
                fileName1 = "YachtCertificateLiabilityMarinaQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtCertificateLiabilityMarinaQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }

            else if (rdlcname == "YachtCertificateLiabilityMarinaAddInsuredQuote.rdlc")
            {
                fileName1 = "YachtCertificateLiabilityMarinaAddInsuredQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2";
                _FileName1 = "YachtCertificateLiabilityMarinaAddInsuredQuote-" + RandomString + "-" + taskControl.ToString().Trim() + "-Com2" + ".pdf";
            }

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

    protected void btnPreviewPolicy_Click(object sender, EventArgs e)
    {
        try
        {
            bool Option1Or2;
            if (!(radioBtnTP1.Checked) && !(radioBtnTP2.Checked))
            {
                throw new Exception("Please select a total premium.");
            }
            else if (radioBtnTP1.Checked)
            {
                Option1Or2 = false;
            }
            else
            {
                Option1Or2 = true;
            }
            PrintYachtPreview(Option1Or2);
        }
        catch(Exception ex){
            lblRecHeader.Text = ex.Message;
            mpeSeleccion.Show();
        }
    }

    private void saveYacht(bool isQuote)
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
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

            Validation(isQuote);

            txtSurveyName.Text = "";
            txtSurveyFee.Text = "";
            txtSurveyDate.Text = "";
            txtSurveyDateCompleted.Text = "";
            txtTenderLimit.Text = "";
            txtTenderDesc.Text = "";
            txtTenderSerial.Text = "";

            FillProperties(isQuote);

            DataTable dtNavigationLimit = new DataTable();
            if (ViewState["navigationData"] != null)
            {
                dtNavigationLimit = (DataTable)ViewState["navigationData"];
                taskControl.NavigationLimitCollection = dtNavigationLimit;
            }
            else
                taskControl.NavigationLimitCollection = null;

            DataTable dtTenderLimit = new DataTable();
            if (ViewState["tenderData"] != null)
            {
                dtTenderLimit = (DataTable)ViewState["tenderData"];
                taskControl.TenderLimitCollection = dtTenderLimit;
            }
            else
                taskControl.TenderLimitCollection = null;

            DataTable dtSurvey = new DataTable();
            if (ViewState["surveyData"] != null)
            {
                dtSurvey = (DataTable)ViewState["surveyData"];
                for (int i = 0; i < gridViewSurvey.Rows.Count; i++)
                {
                    var CheckBoxSelected = gridViewSurvey.Rows[i].FindControl("checkBoxRecomendaciones") as CheckBox;
                    if (CheckBoxSelected.Checked)
                    {
                        dtSurvey.Rows[i]["Recomendaciones"] = true;
                    }
                    else
                    {
                        dtSurvey.Rows[i]["Recomendaciones"] = false;
                    }
                }
                taskControl.SurveyCollection = dtSurvey;
            }
            else
                taskControl.SurveyCollection = null;

            taskControl.isQuote = isQuote;
            taskControl.SaveYacht(userID);

            //Salvar AUTOEndorsement
            double mFactor = 0.0;
            double NewProRataTotPrem = 0.0;
            double NewShotRateTotPrem = 0.0;
            int taskControlIDPolicy = 0;
            if (taskControl.isQuote == true && Session["AUTOEndorsement"] != null)
            {
                // Esta seccion es porque existe ya en la base de datos
                // y no hay que insertar nuevamente el quotes.
                if (Session["OPPEndorUpdate"] == null)
                {
                    EPolicy.TaskControl.Yacht OpptaskControl = (EPolicy.TaskControl.Yacht)Session["AUTOEndorsement"];
                    int oppEndoID = AddOPPEndorsement(OpptaskControl.TaskControlID, taskControl.TaskControlID, mFactor, NewProRataTotPrem, NewShotRateTotPrem);
                    taskControlIDPolicy = OpptaskControl.TaskControlID;
                    Session.Add("OPPENDORID", oppEndoID);
                }
                else
                {
                    EPolicy.TaskControl.Yacht OpptaskControl = (EPolicy.TaskControl.Yacht)Session["AUTOEndorsement"];
                    taskControlIDPolicy = OpptaskControl.TaskControlID;
                    Session.Remove("OPPEndorUpdate");
                }
            }

            if (taskControl.IsEndorsement)
            {
                int oppEndoID = 0;
                if (Session["OPPENDORID"] != null)
                {
                    oppEndoID = (int)Session["OPPENDORID"];
                    Session.Remove("OPPENDORID");
                }

                ExitEndorsement(oppEndoID);

                return;
            }

            taskControl = (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(taskControl.TaskControlID, userID);
            Session["TaskControl"] = taskControl;
            FillTextControl();
            DisableControl();
            if (taskControl.RenewalOfYacht == true)
            {
                lblBank.Visible = true;
                ddlBank.Visible = true;
                ddlBank.Enabled = false;
                btnBankList.Enabled = false;
                lblBankListSelected2.Visible = true;
            }
            if (taskControl.BankPPSID.Trim() != "000" || taskControl.BankPPSID.Trim() != "")
            {
                lblBankListSelected2.Visible = true;
            }
            lblRecHeader.Text = "Yacht information saved successfully.";
            mpeSeleccion.Show();

            if (taskControl.isQuote == true)
            {
                btnModify.Visible = true;
                btnModify.Enabled = true;
                btnPreview.Visible = true;
                btnPreview.Enabled = true;
                btnPreview2.Visible = true;
                btnPreview2.Enabled = true;

                if (taskControl.isAcceptQuote == false)
                {
                    btnAcceptQuote.Enabled = true;
                    btnAcceptQuote.Visible = true;
                }
                else
                {
                    btnConvert.Enabled = true;
                    btnConvert.Visible = true;
                    btnAcceptQuote.Enabled = false;
                    btnAcceptQuote.Visible = false;
                    btnPremiumFinance.Visible = true;
                    taskControl.isAcceptQuote = true;
                    if (txtTotalPremiumPoliza.Text.Trim() != "$0")
                    {
                        radioBtnTP1.Enabled = true;
                        radioBtnTP1.Visible = true;
                    }
                    if (txtTotalPremium2.Text.Trim() != "$0")
                    {
                        radioBtnTP2.Enabled = true;
                        radioBtnTP2.Visible = true;
                    }
                    btnPreviewPolicy.Visible = true;
                }
                btnSave.Visible = false;
                btnCancel.Visible = false;
            }

        }
        catch (Exception exp)
        {
            throw new Exception(exp.Message);
        }
    }

    protected void btnModify2_Click(object sender, EventArgs e)
    {
        btnSave2.Visible = true;
        btnModify2.Visible = false;
        ddlPrintOptions.Visible = false;
        btnPremiumFinance2.Visible = false;
        btnCancel.Visible = true;
        btnCancel.Enabled = true;

        txtBoatName.Enabled = true;
        txtHomeportLocation.Enabled = true;
        txtEngineSerialNumber.Enabled = true;
        txtTenderSerial.Enabled = true;
        ddlHomeport.Enabled = true;
        txtHomeportLocation.Enabled = true;

        lblBank.Visible = true;
        ddlBank.Visible = true;
        ddlBank.Enabled = true;
        lblBankListSelected2.Visible = true;
        btnBankList.Enabled = true;

        if (ViewState["tenderData"] != null)
        {
            gridViewTenderLimit.Enabled = true;
            btnAddRows.Visible = true;
        }

        if (ViewState["surveyData"] != null)
        {
            gridViewSurvey.Enabled = true;
            btnAddSurvey.Visible = true;
            txtSurveyName.Enabled = true;
            txtSurveyFee.Enabled = true;
            imgCalendarSurveyDate.Enabled = true;
            imgCalendarSurveyDateCompleted.Enabled = true;
        }

    }

    protected void btnSave2_Click(object sender, EventArgs e)
    {
        try
        {
            btnSave2.Visible = false;
            btnModify2.Visible = true;
            ddlPrintOptions.Visible = true;
            btnPremiumFinance2.Visible = true;
            btnCancel.Visible = false;
            btnCancel.Enabled = false;
            saveYacht(false);
            updateYachtPendingFieldsPPS();
        }
        catch (Exception exp)
        {
            lblRecHeader.Text = exp.Message;
            mpeSeleccion.Show();
        }
    }

    protected void updateYachtPendingFieldsPPS()
    {
        try
        {
            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
            DataTable dt = GetYachtToPPSByTaskControlIDInfoGeneral(taskControl.TaskControlID);
            DataTable dtTender = GetYachtToPPSByTaskControlIDInfoTenderLimit(taskControl.TaskControlID);

            if (dt.Rows.Count > 0)
            {
                updateYachtPendingFieldsTender1PPS(dt, dtTender);

                if (dtTender.Rows.Count > 0 && dtTender.Rows[0]["TenderLimitAmount2"].ToString() != "")
                {
                    updateYachtPendingFieldsTender2PPS(dt, dtTender);
                }
            }

        }
        catch (Exception xp)
        {
            LogError(xp);
        }
    }

    protected void updateYachtPendingFieldsTender1PPS(DataTable dt, DataTable dtTender)
    {
            string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();

            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = new SqlCommand("sproc_ConsumeXMLePPS-UPDATE_YACHT_PENDING_FIELDS", conn);
                if (chkIsRenew.Checked)
                {
                    cmd.Parameters.AddWithValue("@PolicyID", dt.Rows[0]["PolicyType"].ToString().Trim() + dt.Rows[0]["PolicyNo"].ToString().Trim() + "-" + dt.Rows[0]["Sufijo"].ToString().Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PolicyID", dt.Rows[0]["PolicyType"].ToString().Trim() + dt.Rows[0]["PolicyNo"].ToString().Trim());
                }
                cmd.Parameters.AddWithValue("@Descrip", dt.Rows[0]["BoatName"].ToString().Trim() + " - " + dt.Rows[0]["LOA"].ToString().Trim() + " " + dt.Rows[0]["BoatYear"].ToString().Trim());
                if (dtTender.Rows.Count > 0)
                {
                    if (dtTender.Rows[0]["TenderDesc1"].ToString().Trim() != "" && dtTender.Rows[0]["TenderSerial1"].ToString().Trim() != "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderDesc1"].ToString().Trim() + " " + dtTender.Rows[0]["TenderSerial1"].ToString().Trim());
                    else if (dtTender.Rows[0]["TenderDesc1"].ToString().Trim() != "" && dtTender.Rows[0]["TenderSerial1"].ToString().Trim() == "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderDesc1"].ToString().Trim());
                    else if (dtTender.Rows[0]["TenderDesc1"].ToString().Trim() == "" && dtTender.Rows[0]["TenderSerial1"].ToString().Trim() != "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderSerial1"].ToString().Trim());
                    else
                        cmd.Parameters.Add("@TenderText", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@TenderText", SqlDbType.NVarChar).Value = DBNull.Value;
                }

                cmd.Parameters.Add("@VName", dt.Rows[0]["BoatName"].ToString().Trim());
                if (dt.Rows[0]["Engine"].ToString().Trim() == "" && dt.Rows[0]["EngineSerialNumber"].ToString().Trim() == "")
                {
                    cmd.Parameters.Add("@VesselProp", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else if (dt.Rows[0]["Engine"].ToString().Trim() == "" && dt.Rows[0]["EngineSerialNumber"].ToString().Trim() != "")
                {
                    cmd.Parameters.Add("@VesselProp", dt.Rows[0]["EngineSerialNumber"].ToString().Trim());
                }
                else if (dt.Rows[0]["Engine"].ToString().Trim() != "" && dt.Rows[0]["EngineSerialNumber"].ToString().Trim() == "")
                {
                    cmd.Parameters.Add("@VesselProp", dt.Rows[0]["Engine"].ToString().Trim());
                }
                else
                {
                    cmd.Parameters.Add("@VesselProp", dt.Rows[0]["Engine"].ToString().Trim() + " " + dt.Rows[0]["EngineSerialNumber"].ToString().Trim());
                }
                if (dt.Rows[0]["HomeportDesc"].ToString().Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@Location", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Location", dt.Rows[0]["HomeportDesc"].ToString().Trim());
                }
                if (dt.Rows[0]["BankPPSID"].ToString().Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@PayeeID", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PayeeID", dt.Rows[0]["BankPPSID"].ToString().Trim());
                }
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
    }

    protected void updateYachtPendingFieldsTender2PPS(DataTable dt, DataTable dtTender)
    {
        string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();
        using (var conn = new SqlConnection(ConnectionString))
        {
            var cmd = new SqlCommand("sproc_ConsumeXMLePPS-UPDATE_YACHT_PENDING_FIELDS_TENDER2", conn);
            if (chkIsRenew.Checked)
            {
                cmd.Parameters.AddWithValue("@PolicyID", dt.Rows[0]["PolicyType"].ToString().Trim() + dt.Rows[0]["PolicyNo"].ToString().Trim() + "-" + dt.Rows[0]["Sufijo"].ToString().Trim());
            }
            else
            {
                cmd.Parameters.AddWithValue("@PolicyID", dt.Rows[0]["PolicyType"].ToString().Trim() + dt.Rows[0]["PolicyNo"].ToString().Trim());
            }
            if (dtTender.Rows.Count > 0)
            {
                if (dtTender.Rows[0]["TenderDesc2"].ToString().Trim() != "" && dtTender.Rows[0]["TenderSerial2"].ToString().Trim() != "")
                    cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderDesc2"].ToString().Trim() + " " + dtTender.Rows[0]["TenderSerial2"].ToString().Trim());
                else if (dtTender.Rows[0]["TenderDesc2"].ToString().Trim() != "" && dtTender.Rows[0]["TenderSerial2"].ToString().Trim() == "")
                    cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderDesc2"].ToString().Trim());
                else if (dtTender.Rows[0]["TenderDesc2"].ToString().Trim() == "" && dtTender.Rows[0]["TenderSerial2"].ToString().Trim() != "")
                    cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderSerial2"].ToString().Trim());
                else
                    cmd.Parameters.Add("@TenderText", SqlDbType.NVarChar).Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters.Add("@TenderText", SqlDbType.NVarChar).Value = DBNull.Value;
            }
            // set all other parameters
            cmd.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }

    private int AddOPPEndorsement(int OPPTaskControlID, int OPPQuotesID, double mFactor, double NewProRataTotPrem, double NewShotRateTotPrem)
    {
        Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
        try
        {
            Executor.BeginTrans();
            int a = Executor.Insert("AddOPPEndorsement", this.GetInsertOPPEndorsementXml(OPPTaskControlID, OPPQuotesID, mFactor, NewProRataTotPrem, NewShotRateTotPrem));
            Executor.CommitTrans();

            return a;
        }
        catch (Exception xcp)
        {
            Executor.RollBackTrans();
            throw new Exception("Error while trying to save Endorsement Quote. " + xcp.Message, xcp);
        }
    }

    private XmlDocument GetInsertOPPEndorsementXml(int OPPTaskControlID, int OPPQuotesID, double mFactor, double NewProRataTotPrem, double NewShotRateTotPrem)
    {
        DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[22];
        DbRequestXmlCooker.AttachCookItem("OPPTaskControlID", SqlDbType.Int, 0, OPPTaskControlID.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("OPPQuotesID", SqlDbType.Int, 0, OPPQuotesID.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("EndoEffective", SqlDbType.DateTime, 0, DateTime.Now.ToShortDateString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("Factor", SqlDbType.Float, 0, mFactor.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("ProRataPremium", SqlDbType.Float, 0, NewProRataTotPrem.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("ShortRatePremium", SqlDbType.Float, 0, NewShotRateTotPrem.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("ActualPremPropeties", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("ActualPremLiability", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("ActualPremAuto", SqlDbType.Float, 0, txtTotalPremiumPoliza.Text.ToString(), ref cookItems); //total premium endoso
        DbRequestXmlCooker.AttachCookItem("ActualPremUmbrella", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("ActualPremTotal", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("PreviousPremProperties", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("PreviousPremLiability", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("PreviousPremAuto", SqlDbType.Float, 0, txtPreviousTotalPremiumPoliza.Text.ToString(), ref cookItems);//total premium poliza original
        DbRequestXmlCooker.AttachCookItem("PreviousPremUmbrella", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("PreviousPremTotal", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("DiffPremProperties", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("DiffPremLiability", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("DiffPremAuto", SqlDbType.Float, 0, txtDiffPremYacht.Text.ToString(), ref cookItems);//total premium endoso - total premium poliza orginal
        DbRequestXmlCooker.AttachCookItem("DiffPremUmbrella", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("DiffPremTotal", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("AdditionalPremium", SqlDbType.Float, 0, "0", ref cookItems);

        XmlDocument xmlDoc;

        try
        {
            xmlDoc = DbRequestXmlCooker.Cook(cookItems);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not cook items.", ex);
        }

        return xmlDoc;
    }

    protected void BtnExitEndorsement_Click(object sender, EventArgs e)
    {
        //Para que vuelva a la poliza original.
        EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
        int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);


        EPolicy.TaskControl.Yacht OpptaskControl = (EPolicy.TaskControl.Yacht)Session["AUTOEndorsement"];
        int taskControlIDPolicy = OpptaskControl.TaskControlID;
        Session.Remove("AUTOEndorsement");
        Session.Clear();

        EPolicy.TaskControl.Yacht QA = (EPolicy.TaskControl.Yacht)TaskControl.GetTaskControlByTaskControlID(taskControlIDPolicy, UserID);
        QA.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
        QA.IsEndorsement = false;
        Session["TaskControl"] = QA;
        Response.Redirect("Yacht.aspx", false);
    }

    private void ExitEndorsement(int OppEndoID)
    {
        RemoveSessionLookUp();

        if (Session["AUTOEndorsement"] != null)
        {
            //Para que vuelva a la poliza original.
            EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
            int UserID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

            EPolicy.TaskControl.Yacht OpptaskControl = (EPolicy.TaskControl.Yacht)Session["AUTOEndorsement"];
            int taskControlIDPolicy = OpptaskControl.TaskControlID;
            Session.Remove("AUTOEndorsement");
            Session.Clear();

            EPolicy.TaskControl.Yacht QA = (EPolicy.TaskControl.Yacht)TaskControl.GetTaskControlByTaskControlID(taskControlIDPolicy, UserID);
            QA.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
            QA.IsEndorsement = false;
            Session["TaskControl"] = QA;
            Response.Redirect("Yacht.aspx?EndorsementSection=" + OppEndoID.ToString(), false);
        }
        else
        {
            Session.Clear();
            Response.Redirect("Search.aspx");
        }
    }

    private void ActiveEndorsementSectionFromQuoteForApply(int OppEndorID)
    {
        EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
        int userID = 0;
        userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

        DataTable dt = GetOPPEndorsementByLastOneOPPEndorsementID(OppEndorID);

        if (dt.Rows.Count > 0)
        {
            string date = dt.Rows[0]["EndoNum"].ToString().Trim(); // e.Item.Cells[3].Text.Trim();
            if (date.ToString().Trim() == "&nbsp;" || date.ToString().Trim() == "")
            {
                int a = int.Parse(dt.Rows[0]["OPPQuotesID"].ToString().Trim()); // int.Parse(e.Item.Cells[2].Text);
                EPolicy.TaskControl.Yacht newOPP = (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(a, userID);

                int OPPEndorsementID = OppEndorID; // int.Parse(e.Item.Cells[1].Text);
                Session.Add("OPPEndorsementID", OPPEndorsementID);
                //Buscar Quotes para endosar
                // Panel1.Visible = true;
                AccordionEndorsement.Visible = true;
                txtEffDtEndorsement.Text = dt.Rows[0]["EndoEffective"].ToString().Trim(); // e.Item.Cells[4].Text.Trim(); //DateTime.Now.ToShortDateString();
                txtFactor.Text = dt.Rows[0]["Factor"].ToString().Trim(); //e.Item.Cells[5].Text.Trim();
                txtProRata.Text = dt.Rows[0]["ProRataPremium"].ToString().Trim(); //e.Item.Cells[6].Text.Trim();
                txtShortRate.Text = dt.Rows[0]["ShortRatePremium"].ToString().Trim(); //e.Item.Cells[7].Text.Trim();
                txtActualPremAuto.Text = dt.Rows[0]["ActualPremAuto"].ToString().Trim(); //e.Item.Cells[15].Text.Trim();
                txtActualPremTotal.Text = dt.Rows[0]["ActualPremTotal"].ToString().Trim(); //e.Item.Cells[17].Text.Trim();
                txtPreviousPremAuto.Text = dt.Rows[0]["PreviousPremAuto"].ToString().Trim(); //e.Item.Cells[20].Text.Trim();
                txtPreviousPremTotal.Text = dt.Rows[0]["PreviousPremTotal"].ToString().Trim(); //e.Item.Cells[22].Text.Trim();
                txtDiffPremAuto.Text = dt.Rows[0]["DiffPremAuto"].ToString().Trim(); //e.Item.Cells[25].Text.Trim();
                txtDiffPremTotal.Text = dt.Rows[0]["DiffPremTotal"].ToString().Trim(); //e.Item.Cells[27].Text.Trim();
                txtAdditionalPremium.Text = dt.Rows[0]["AdditionalPremium"].ToString().Trim(); //e.Item.Cells[28].Text.Trim();

                CalculateEndorsement(newOPP);
                Session.Add("ApplyEndorsement", a);

                var oppLast = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
                if (DataGridGroup.Items.Count > 0)
                {
                    for (int y = DataGridGroup.Items.Count - 1; y >= 0; y--)
                    {

                        if (DataGridGroup.Items[y].Cells[3].Text.Trim() != "" && DataGridGroup.Items[y].Cells[3].Text.Trim() != "&nbsp;")
                        {
                            oppLast =
                                (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl
                                .GetTaskControlByTaskControlID(Convert.ToInt32(DataGridGroup.Items[y].Cells[2].Text.Trim()), userID);
                            oppLast.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                            oppLast.IsEndorsement = true;
                            break;
                        }
                    }

                    //Se crea Record en la tabla OppEndorsementByCoverages para obtener el detalle de prima por cubierta
                    //con elproposito de utilizar esta info para PPS en la tabla de InvLine
                    double AdditionalPremiumTemp = 0.0;
                    if (double.Parse(txtProRata.Text) == 0)
                    {
                        //AdditionalPremium = (convertedtTxtAdditionalPremium == 0) && (convertedtTxtDiffPremTotal == 0) ? "0" : txtDiffPremTotal.Text;
                        AdditionalPremiumTemp = (double.Parse(txtAdditionalPremium.Text) == 0) && (double.Parse(txtDiffPremTotal.Text) == 0) ? 0 : double.Parse(txtDiffPremTotal.Text);
                    }
                    else
                    {
                        AdditionalPremiumTemp = double.Parse(txtAdditionalPremium.Text);
                    }

                    AddOppEndorsementByCoverages(int.Parse(LblControlNo.Text.Trim()), OPPEndorsementID, newOPP.TaskControlID, oppLast.TaskControlID, double.Parse(txtFactor.Text.Trim()), AdditionalPremiumTemp);

                    EPolicy.TaskControl.Yacht[] Autos = new EPolicy.TaskControl.Yacht[] { oppLast, newOPP };
                    List<String> CarsAdded = new List<String>();
                    txtEndoComments.Enabled = true;
                    //Session.Add("EndoComments", txtEndoComments.Text);
                }
            }
        }
    }

    private DataTable GetOPPEndorsementByLastOneOPPEndorsementID(int OPPEndorsementID)
    {
        DataTable dt = null;
        try
        {

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
            DbRequestXmlCooker.AttachCookItem("OPPEndorsementID", SqlDbType.Int, 0, OPPEndorsementID.ToString(), ref cookItems);
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
            dt = exec.GetQuery("GetOPPEndorsementByLastOneOPPEndorsementID", xmlDoc);

        }
        catch (Exception ep)
        {
        }
        return dt;
    }

    private void CalculateEndorsement(EPolicy.TaskControl.Yacht OpptaskControl)
    {
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

        double totprem = SetEndorsementToCalculateDifference(taskControl, OpptaskControl);

        double mFactorProRata = 0.0;
        double mFactorShortRate = 0.0;
        double NewProRataTotPrem = 0.0;
        double NewShotRateTotPrem = 0.0;
        string EndorDate = txtEffDtEndorsement.Text.Trim().Replace("&nbsp;", "");
        //Si no es Flat no hace calculo de prima prorrateada.
        if (taskControl.EffectiveDate != EndorDate.Trim())
        {
            TimeSpan tsDAYS1 = DateTime.Parse(taskControl.ExpirationDate) - DateTime.Parse(EndorDate.Trim());
            TimeSpan tsDAYS2 = DateTime.Parse(taskControl.ExpirationDate) - DateTime.Parse(taskControl.EffectiveDate);

            int mDAYS1 = tsDAYS1.Days;
            int mDAYS2 = tsDAYS2.Days;

            mFactorProRata = double.Parse(mDAYS1.ToString()) / double.Parse(mDAYS2.ToString());
            mFactorShortRate = mFactorProRata;

            mFactorProRata = Math.Round(mFactorProRata, 3);
            mFactorShortRate = Math.Round(mFactorShortRate * .90, 3);

            //TODO: CALCULATE PER COVERAGE
            NewProRataTotPrem = Math.Round(totprem * mFactorProRata, 0);
            NewShotRateTotPrem = Math.Round(totprem * mFactorShortRate, 0);

            txtFactor.Text = mFactorProRata.ToString().Trim();
            txtProRata.Text = NewProRataTotPrem.ToString().Trim();
            txtShortRate.Text = NewShotRateTotPrem.ToString().Trim();
            txtAdditionalPremium.Text = NewProRataTotPrem.ToString("###,###,###.00") == "0.00" ? txtDiffPremTotal.Text.ToString() : NewProRataTotPrem.ToString("###,###,###.00");
        }
        else
        {
            txtAdditionalPremium.Text = "0.0";
        }
    }

    private double SetEndorsementToCalculateDifference(EPolicy.TaskControl.Yacht taskControl, EPolicy.TaskControl.Yacht OpptaskControl)
    {
        EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
        int userID = 0;
        userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

        txtActualPremAuto.Text = GetNetTotalPremium(OpptaskControl.TaskControlID, OpptaskControl.isQuote).ToString("###,###,###.00");//OpptaskControl.TotalPremium.ToString("###,###,###.00");
        txtActualPremTotal.Text = GetNetTotalPremium(OpptaskControl.TaskControlID, OpptaskControl.isQuote).ToString("###,###,###.00");//OpptaskControl.TotalPremium.ToString("###,###,###.00");

        //Buscar el endoso anterior
        //Para aplicar el ultimo endoso, sino a la poliza original
        DataTable endososList = PersonalPackage.GetEndorsementByEndoNum(taskControl.TaskControlID);
        EPolicy.TaskControl.Yacht taskControlEndo = null;

        if (endososList.Rows.Count == 1)
        {
            if ((int)endososList.Rows[endososList.Rows.Count - 1]["OPPQuotesID"] != 0)
            {
                taskControlEndo = EPolicy.TaskControl.Yacht.GetYacht((int)endososList.Rows[endososList.Rows.Count - 1]["OPPQuotesID"], true);
                taskControl = taskControlEndo;

                txtPreviousPremAuto.Text = GetNetTotalPremium(taskControlEndo.TaskControlID, taskControlEndo.isQuote).ToString("###,###,###.00");//taskControlEndo.TotalPremium.ToString("###,###,###.00");
                txtPreviousPremTotal.Text = GetNetTotalPremium(taskControlEndo.TaskControlID, taskControlEndo.isQuote).ToString("###,###,###.00");//taskControlEndo.TotalPremium.ToString("###,###,###.00");
            }
            else
            {
                txtPreviousPremAuto.Text = GetNetTotalPremium(taskControl.TaskControlID, taskControl.isQuote).ToString("###,###,###.00");//taskControl.TotalPremium.ToString("###,###,###.00");
                txtPreviousPremTotal.Text = GetNetTotalPremium(taskControl.TaskControlID, taskControl.isQuote).ToString("###,###,###.00");//taskControl.TotalPremium.ToString("###,###,###.00");
            }
        }
        else
            if (endososList.Rows.Count > 1)
            {
                bool isExistEndo = false;
                for (int s = 1; s <= endososList.Rows.Count; s++)
                {
                    if ((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"] != 0)
                    {
                        taskControlEndo = EPolicy.TaskControl.Yacht.GetYacht((int)endososList.Rows[endososList.Rows.Count - s]["OPPQuotesID"], true);
                        taskControl = taskControlEndo;
                        isExistEndo = true;
                        s = endososList.Rows.Count;
                    }
                }

                if (isExistEndo)
                {
                    txtPreviousPremAuto.Text = GetNetTotalPremium(taskControlEndo.TaskControlID, taskControlEndo.isQuote).ToString("###,###,###.00");//taskControlEndo.TotalPremium.ToString("###,###,###.00");
                    txtPreviousPremTotal.Text = GetNetTotalPremium(taskControlEndo.TaskControlID, taskControlEndo.isQuote).ToString("###,###,###.00");//taskControlEndo.TotalPremium.ToString("###,###,###.00");
                }
                else
                {
                    txtPreviousPremAuto.Text = GetNetTotalPremium(taskControl.TaskControlID, taskControl.isQuote).ToString("###,###,###.00");//taskControl.TotalPremium.ToString("###,###,###.00");
                    txtPreviousPremTotal.Text = GetNetTotalPremium(taskControl.TaskControlID, taskControl.isQuote).ToString("###,###,###.00");//taskControl.TotalPremium.ToString("###,###,###.00");
                }
            }
            else
            {
                txtPreviousPremAuto.Text = GetNetTotalPremium(taskControl.TaskControlID, taskControl.isQuote).ToString("###,###,###.00");//taskControl.TotalPremium.ToString("###,###,###.00");
                txtPreviousPremTotal.Text = GetNetTotalPremium(taskControl.TaskControlID, taskControl.isQuote).ToString("###,###,###.00");//taskControl.TotalPremium.ToString("###,###,###.00");
            }

        //Calculate Difference
        txtDiffPremAuto.Text = CalculateEndorsementDifference(txtActualPremAuto.Text.Trim(), txtPreviousPremAuto.Text);

        double totalPrev = GetNetTotalPremium(taskControl.TaskControlID, taskControl.isQuote);//double.Parse(taskControl.TotalPremium.ToString()) + double.Parse(taskControl.Charge.ToString());
        double totalActual = GetNetTotalPremium(OpptaskControl.TaskControlID, OpptaskControl.isQuote);//double.Parse(OpptaskControl.TotalPremium.ToString()) + double.Parse(OpptaskControl.Charge.ToString());

        txtDiffPremTotal.Text = CalculateEndorsementDifference(totalActual.ToString(), totalPrev.ToString());

        return double.Parse(txtDiffPremTotal.Text);
    }

    private double GetNetTotalPremium(int TaskControlID, bool isQuote)
    {
        double Total = 0;
        DataTable dt = null;
        if (isQuote)
        {
            dt = GetNetTotalPremiumQuoteDB(TaskControlID);
        }
        else
        {
            dt = GetNetTotalPremiumPolicyDB(TaskControlID);
        }
        if (dt.Rows.Count > 0)
        {
            //When modifing Verifies premium claculated, if the premium is not equal or does not have 1 dollar less or 1 dollar more the total premium shwon will be the one calculated inside the application
            if (double.Parse(dt.Rows[0]["TotalPremium"].ToString()).ToString("##,###.00") != "0")
            {
                Total = double.Parse(dt.Rows[0]["TotalPremium"].ToString());
            }
        }

        return Total;
    }

    private DataTable GetNetTotalPremiumQuoteDB(int TaskControlID)
    {
        DataTable dt = null;
        try
        {

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, TaskControlID.ToString(), ref cookItems);
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
            dt = exec.GetQuery("GetTotalPremiumDB", xmlDoc);

        }
        catch (Exception ep)
        {
        }
        return dt;
    }

    private DataTable GetNetTotalPremiumPolicyDB(int TaskControlID)
    {
        DataTable dt = null;
        try
        {

            DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
            DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, TaskControlID.ToString(), ref cookItems);
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
            dt = exec.GetQuery("GetTotalPremiumPolicyDB", xmlDoc);

        }
        catch (Exception ep)
        {
        }
        return dt;
    }

    private string CalculateEndorsementDifference(string ActualValue, string PreviousValue)
    {
        double actual = 0.0;
        double previous = 0.0;
        double result = 0.0;

        actual = double.Parse(ActualValue);
        previous = double.Parse(PreviousValue);
        result = actual - previous;

        return result.ToString("###,###,###.00");
    }

    private void AddOppEndorsementByCoverages(int TaskControlID, int OPPEndorsementID, int NewTaskControlID, int OldTaskControlID, double EndorFactor, double EndorPremium)
    {
        DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[6];

        DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, TaskControlID.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("OPPEndorsementID", SqlDbType.Int, 0, OPPEndorsementID.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("NewTaskControlID", SqlDbType.Int, 0, NewTaskControlID.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("OldTaskControlID", SqlDbType.Int, 0, OldTaskControlID.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("EndorFactor", SqlDbType.Float, 0, EndorFactor.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("EndorPremium", SqlDbType.Float, 0, EndorPremium.ToString(), ref cookItems);

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
        exec.BeginTrans();
        exec.Insert("AddOppEndorsementByCoverages", xmlDoc);
        exec.CommitTrans();
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        Session.Remove("OPPEndorUpdate");
        Session.Remove("OPPEndorsementID");
        Session.Remove("ONLYAUTOEndorsement");
        Session.Remove("ApplyEndorsement");
        //Panel1.Visible = false;
        AccordionEndorsement.Visible = false;
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        //Si la descripcion del endoso esta vacio no debe de aplicar el endoso, debe de tratar de hacer otro quote endorsement.
            try
            {
                ArrayList errorMessages = new ArrayList();

                //TEXTBOXES VALIDATION

                if (txtEndoComments.Text.Trim() == "")
                {
                    errorMessages.Add("Endorsement comment is a require field. Endorsement could not be apply, please press the apply button and enter an endorsement comment" + "\r\n");
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


                EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
                int userID = 0;
                userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);

                if (Session["OPPEndorUpdate"] == null)
                {
                    // Salvar el num. de Endo en Policy
                    EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
                    //int endNum = taskControl.Endoso + 1;
                    //taskControl.Endoso = endNum;

                    taskControl.SaveOnlyPolicy(userID);
                    Session["TaskControl"] = taskControl;
                }

                if (Session["ApplyEndorsement"] != null)
                {
                    int a = (int)Session["ApplyEndorsement"];
                    EPolicy.TaskControl.Yacht newOPP2 = (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(a, userID);
                    EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
                    Session["OPPEndorTaskControlID"] = newOPP2.TaskControlID;

                    taskControl.Endoso = GetOPPEndorsementLastEndoNum(taskControl.TaskControlID);

                    if (!IsLocalHost())
                    {
                        int OPPEndorsementID = (int)Session["OPPEndorsementID"];
                        //SI ES UN ENDOSO SOLO DEBO SABER EL TASKCONTROL PARA LOS CAMBIOS DE VEHICULO Y LA PRIMA
                        UpdateOppEndorsementByCoverages(taskControl.TaskControlID, OPPEndorsementID, taskControl.Endoso + 1);
                        //Se usa solo para ambiente de prueba -Copiar info en la tabla OppEndorsementUsedToPPSInvLine en PPS anes del endoso
                        //AddOppEndorsementUsedToPPSInvLineToPPS(taskControl.TaskControlID, OPPEndorsementID);
                        //UpdateDriverDetailByTaskControlIDAndPrimaryDriver(taskControl.TaskControlID, newOPP2.TaskControlID); //Actualiza el nomre del Primary Driver or Insured si el nombre del cliente cambio
                        //ApplyEndorsementPPS(taskControl.TaskControlID, taskControl.Endoso);
                    }

                    ApplyEndorsement(newOPP2, userID);
                    SendEndosoToPPS(newOPP2.TaskControlID);
                    DataTable dtTender = GetYachtToPPSByTaskControlIDInfoTenderLimit(newOPP2.TaskControlID);
                    SendEndosoToPPSTender2(newOPP2.TaskControlID);
                    //if (dtTender.Rows.Count > 0 && dtTender.Rows[0]["TenderLimitAmount2"].ToString() != "")
                    //{
                    //    SendEndosoToPPSTender2(newOPP2.TaskControlID);
                    //}

                }

                // Salvar la info en OPP Endorsement.
                if (Session["ONLYAUTOEndorsement"] != null)
                {
                    EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
                    taskControl.Mode = 2; //EDIT
                    taskControl.Endoso = GetOPPEndorsementLastEndoNum(taskControl.TaskControlID);
                    int endNum = taskControl.Endoso + 1;
                    taskControl.Endoso = endNum;
                    Session["TaskControl"] = taskControl;
                    taskControl.SaveOnlyPolicy(userID);

                    int endID = AddOPPEndorsement(taskControl.TaskControlID, 0, 0.00, 0.00, 0.00);
                    Session.Add("OPPEndorsementID", endID);

                    UpdateOPPEndorsement();
                    Session.Remove("ONLYAUTOEndorsement");
                    Session.Remove("OPPEndorsementID");
                }
                else
                {
                    UpdateOPPEndorsement();
                }

                Session.Remove("OPPEndorsementID");
                Session.Remove("OPPEndorUpdate");
                Session.Remove("ONLYAUTOEndorsement");
                Session.Remove("ApplyEndorsement");
                //Panel1.Visible = false;
                AccordionEndorsement.Visible = false;

                FillDataGrid();
            }
            catch (Exception ex)
            {
                lblRecHeader.Text = ex.Message;
                mpeSeleccion.Show();
            }
        }

    private static bool IsLocalHost()
    {
        return //false;
               HttpContext.Current.Request.Url.ToString().Contains("localhost");
    }

    private int GetOPPEndorsementLastEndoNum(int OPPTaskControlID)
    {
        int endoNum = 0;
        DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[1];
        DbRequestXmlCooker.AttachCookItem("OPPTaskControlID", SqlDbType.Int, 0, OPPTaskControlID.ToString(), ref cookItems);

        DBRequest exec = new DBRequest();
        XmlDocument xmlDoc;
        xmlDoc = DbRequestXmlCooker.Cook(cookItems);

        try
        {
            DataTable dt = exec.GetQuery("GetOPPEndorsementLastEndoNum", xmlDoc);

            if (dt.Rows.Count > 0)
                endoNum = int.Parse(dt.Rows[0]["EndoNum"].ToString());


            return endoNum;

        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve the data.", ex);
        }
    }

    private void UpdateOppEndorsementByCoverages(int TaskControlID, int OPPEndorsementID, int Endoso)
    {
        DbRequestXmlCookRequestItem[] cookItems = new DbRequestXmlCookRequestItem[3];

        DbRequestXmlCooker.AttachCookItem("TaskControlID", SqlDbType.Int, 0, TaskControlID.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("OPPEndorsementID", SqlDbType.Int, 0, OPPEndorsementID.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("EndNumber", SqlDbType.Int, 0, Endoso.ToString().Trim(), ref cookItems);

        XmlDocument xmlDoc;

        try
        {
            xmlDoc = DbRequestXmlCooker.Cook(cookItems);

            Baldrich.DBRequest.DBRequest exec = new Baldrich.DBRequest.DBRequest();
            //exec.BeginTrans();
            exec.Update("UpdateOppEndorsementByCoverages", xmlDoc);
            //exec.CommitTrans();
        }
        catch (Exception ex)
        {
            throw new Exception("Could not cook items.", ex);
        }
    }

    private void ApplyEndorsement(EPolicy.TaskControl.Yacht newOPP, int userID)
    {
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        taskControl.Mode = 2; //EDIT
        taskControl.Endoso = GetOPPEndorsementLastEndoNum(taskControl.TaskControlID);
        int endNum = taskControl.Endoso + 1;
        taskControl.Endoso = endNum;
        Session["TaskControl"] = taskControl;
        taskControl.SaveOnlyPolicy(userID);

        UpdateOPPEndorsement();
        //FillTextControl();
        //DisableControls();

        FillDataGrid();
        Session["TaskControl"] = taskControl;
    }

    private void UpdateOPPEndorsement()
    {
        Baldrich.DBRequest.DBRequest Executor = new Baldrich.DBRequest.DBRequest();
        try
        {
            Executor.BeginTrans();
            Executor.Update("UpdateOPPEndorsement", this.GetUpdateOPPEndorsementXml());
            Executor.CommitTrans();
        }
        catch (Exception xcp)
        {
            Executor.RollBackTrans();
            throw new Exception("Error while trying to save Endorsement Quote. " + xcp.Message, xcp);
        }
    }

    private XmlDocument GetUpdateOPPEndorsementXml()
    {
        DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[23];

        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
        int OPPEndorsementID = (int)Session["OPPEndorsementID"];

        DbRequestXmlCooker.AttachCookItem("OPPEndorsementID", SqlDbType.Int, 0, OPPEndorsementID.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("EndoEffective", SqlDbType.DateTime, 0, txtEffDtEndorsement.Text.Trim(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("EndoNum", SqlDbType.Char, 4, taskControl.Endoso.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("Cambios", SqlDbType.VarChar, 5000, txtEndoComments.Text.Trim(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("Factor", SqlDbType.Float, 0, txtFactor.Text.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("ProRataPremium", SqlDbType.Float, 0, txtProRata.Text.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("ShortRatePremium", SqlDbType.Float, 0, txtShortRate.Text.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("ActualPremPropeties", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("ActualPremLiability", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("ActualPremAuto", SqlDbType.Float, 0, txtActualPremAuto.Text.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("ActualPremUmbrella", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("ActualPremTotal", SqlDbType.Float, 0, txtActualPremTotal.Text.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("PreviousPremProperties", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("PreviousPremLiability", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("PreviousPremAuto", SqlDbType.Float, 0, txtPreviousPremAuto.Text.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("PreviousPremUmbrella", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("PreviousPremTotal", SqlDbType.Float, 0, txtPreviousPremTotal.Text.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("DiffPremProperties", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("DiffPremLiability", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("DiffPremAuto", SqlDbType.Float, 0, txtDiffPremAuto.Text.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("DiffPremUmbrella", SqlDbType.Float, 0, "0", ref cookItems);
        DbRequestXmlCooker.AttachCookItem("DiffPremTotal", SqlDbType.Float, 0, txtDiffPremTotal.Text.ToString(), ref cookItems);
        DbRequestXmlCooker.AttachCookItem("AdditionalPremium", SqlDbType.Float, 0, txtAdditionalPremium.Text.ToString(), ref cookItems);
        XmlDocument xmlDoc;

        try
        {
            xmlDoc = DbRequestXmlCooker.Cook(cookItems);
        }
        catch (Exception ex)
        {
            throw new Exception("Could not cook items.", ex);
        }

        return xmlDoc;
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
                        DataGridGroup.DataSource = null;

                        EPolicy.TaskControl.Yacht opp = (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(i, userID);
                        opp.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                        opp.IsEndorsement = true;

                        if (Session["TaskControl"] != null)
                        {
                            EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
                            Session.Clear();
                            if (e.Item.Cells[4].Text.ToString().Replace("&nbsp;", "") != "")
                                Session.Add("EndorsementDate", e.Item.Cells[4].Text);
                            Session.Add("AUTOEndorsement", taskControl);
                            Session.Add("OPPEndorUpdate", "Update");
                            Session.Remove("TaskControl");
                        }

                        Session.Add("TaskControl", opp);
                        Response.Redirect("Yacht.aspx");
                    }
                    break;

                case "Apply":
                    DataGridGroup.DataSource = null;

                    string date = e.Item.Cells[3].Text.Trim();
                    if (date.ToString().Trim() != "&nbsp;")
                    {
                        throw new Exception("This Endorsement is already Applied.");
                    }

                    int a = int.Parse(e.Item.Cells[2].Text);
                    EPolicy.TaskControl.Yacht newOPP = (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(a, userID);

                    int OPPEndorsementID = int.Parse(e.Item.Cells[1].Text);
                    Session.Add("OPPEndorsementID", OPPEndorsementID);
                    //Buscar Quotes para endosar
                    // Panel1.Visible = true;
                    AccordionEndorsement.Visible = true;
                    txtEffDtEndorsement.Text = e.Item.Cells[4].Text.Trim(); //DateTime.Now.ToShortDateString();
                    txtFactor.Text = e.Item.Cells[5].Text.Trim();
                    txtProRata.Text = e.Item.Cells[6].Text.Trim();
                    txtShortRate.Text = e.Item.Cells[7].Text.Trim();
                    txtActualPremAuto.Text = e.Item.Cells[15].Text.Trim();
                    txtActualPremTotal.Text = e.Item.Cells[17].Text.Trim();
                    txtPreviousPremAuto.Text = e.Item.Cells[20].Text.Trim();
                    txtPreviousPremTotal.Text = e.Item.Cells[22].Text.Trim();
                    txtDiffPremAuto.Text = e.Item.Cells[25].Text.Trim();
                    txtDiffPremTotal.Text = e.Item.Cells[27].Text.Trim();
                    txtAdditionalPremium.Text = e.Item.Cells[28].Text.Trim();

                    CalculateEndorsement(newOPP);
                    //VerifyChanges(newOPP, userID);
                    Session.Add("ApplyEndorsement", a);

                    var oppLast = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
                    if (DataGridGroup.Items.Count > 0)
                    {
                        for (int y = DataGridGroup.Items.Count - 1; y >= 0; y--)
                        {

                            if (DataGridGroup.Items[y].Cells[3].Text.Trim() != "" && DataGridGroup.Items[y].Cells[3].Text.Trim() != "&nbsp;")
                            {
                                oppLast =
                                    (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl
                                    .GetTaskControlByTaskControlID(Convert.ToInt32(DataGridGroup.Items[y].Cells[2].Text.Trim()), userID);
                                oppLast.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                                oppLast.IsEndorsement = true;
                                break;
                            }
                        }

                        //Se crea Record en la tabla OppEndorsementByCoverages para obtener el detalle de prima por cubierta
                        //con elproposito de utilizar esta info para PPS en la tabla de InvLine
                        double AdditionalPremiumTemp = 0.0;
                        if (double.Parse(txtProRata.Text) == 0)
                        {
                            //AdditionalPremium = (convertedtTxtAdditionalPremium == 0) && (convertedtTxtDiffPremTotal == 0) ? "0" : txtDiffPremTotal.Text;
                            AdditionalPremiumTemp = (double.Parse(txtAdditionalPremium.Text) == 0) && (double.Parse(txtDiffPremTotal.Text) == 0) ? 0 : double.Parse(txtDiffPremTotal.Text);
                        }
                        else
                        {
                            AdditionalPremiumTemp = double.Parse(txtAdditionalPremium.Text);
                        }

                        AddOppEndorsementByCoverages(int.Parse(LblControlNo.Text.Trim()), OPPEndorsementID, newOPP.TaskControlID, oppLast.TaskControlID, double.Parse(txtFactor.Text.Trim()), AdditionalPremiumTemp);

                        EPolicy.TaskControl.Yacht[] Autos = new EPolicy.TaskControl.Yacht[] { oppLast, newOPP };
                        List<String> CarsAdded = new List<String>();
                        txtEndoComments.Text = txtEndoComments.Text;
                        Session.Add("EndoComments", txtEndoComments.Text);
                        txtEndoComments.Text = StripHTML(txtEndoComments.Text);

                    }
                    break;

                case "Update":
                    DataGridGroup.DataSource = null;

                    string date3 = e.Item.Cells[3].Text.Trim();
                    if (date3.ToString().Trim() == "&nbsp;")
                    {
                        throw new Exception("This Endorsement is not Applied.");
                    }

                    int a2 = int.Parse(e.Item.Cells[2].Text);
                    EPolicy.TaskControl.Yacht newOPP2 = (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(a2, userID);

                    int OPPEndorsement3ID = int.Parse(e.Item.Cells[1].Text);
                    Session.Add("OPPEndorsementID", OPPEndorsement3ID);
                    Session.Add("OPPEndorUpdate", "Update");
                    //Buscar Quotes para endosar
                    // Panel1.Visible = true;
                    AccordionEndorsement.Visible = true;
                    txtEffDtEndorsement.Text = e.Item.Cells[4].Text.Trim(); //DateTime.Now.ToShortDateString();
                    txtFactor.Text = e.Item.Cells[5].Text.Trim();
                    txtProRata.Text = e.Item.Cells[6].Text.Trim();
                    txtShortRate.Text = e.Item.Cells[7].Text.Trim();
                    txtActualPremAuto.Text = e.Item.Cells[15].Text.Trim();
                    txtActualPremTotal.Text = e.Item.Cells[17].Text.Trim();
                    txtPreviousPremAuto.Text = e.Item.Cells[20].Text.Trim();
                    txtPreviousPremTotal.Text = e.Item.Cells[22].Text.Trim();
                    txtDiffPremAuto.Text = e.Item.Cells[25].Text.Trim();
                    txtDiffPremTotal.Text = e.Item.Cells[27].Text.Trim();
                    txtAdditionalPremium.Text = e.Item.Cells[28].Text.Trim();

                    //CalculateEndorsement(newOPP2);
                    //VerifyChanges(newOPP2, userID);
                    ////Session.Add("ApplyEndorsement", a2);

                    //var oppLast2 = (EPolicy.TaskControl.Autos)Session["TaskControl"];
                    //if (DataGridGroup.Items.Count > 0)
                    //{
                    //    for (int y = DataGridGroup.Items.Count - 1; y >= 0; y--)
                    //    {

                    //        if (DataGridGroup.Items[y].Cells[3].Text.Trim() != "" && DataGridGroup.Items[y].Cells[3].Text.Trim() != "&nbsp;")
                    //        {
                    //            oppLast2 =
                    //                (EPolicy.TaskControl.Autos)EPolicy.TaskControl.TaskControl
                    //                .GetTaskControlByTaskControlID(Convert.ToInt32(DataGridGroup.Items[y].Cells[2].Text.Trim()), userID);
                    //            oppLast2.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                    //            oppLast2.IsEndorsement = true;
                    //            break;
                    //        }
                    //    }
                    //    EPolicy.TaskControl.Autos[] Autos = new EPolicy.TaskControl.Autos[] { oppLast2, newOPP2 };
                    //    List<String> CarsAdded = new List<String>();
                    //    txtEndoComments.Text = EndorVehicleChanges(Autos, ref CarsAdded);
                    //    Session.Add("EndoComments", txtEndoComments.Text);
                    //    txtEndoComments.Text = StripHTML(txtEndoComments.Text);

                    //}


                    ///OLD

                    //Session.Add("OPPEndorsementID", OPPEndorsement3ID);
                    //Session.Add("OPPEndorUpdate", "Update");
                    ////Buscar Quotes para endosar
                    ////Panel1.Visible = true;
                    //AccordionEndorsement.Visible = true;
                    //txtEffDtEndorsement.Text = e.Item.Cells[4].Text.Trim();
                    //txtFactor.Text = e.Item.Cells[5].Text.Trim();
                    //txtProRata.Text = e.Item.Cells[6].Text.Trim();
                    //txtShortRate.Text = e.Item.Cells[7].Text.Trim();
                    //txtEndoComments.Text = e.Item.Cells[11].Text.Trim();
                    //txtActualPremAuto.Text = e.Item.Cells[15].Text.Trim();
                    //txtActualPremTotal.Text = e.Item.Cells[17].Text.Trim();
                    //txtPreviousPremAuto.Text = e.Item.Cells[20].Text.Trim();
                    //txtPreviousPremTotal.Text = e.Item.Cells[22].Text.Trim();
                    //txtDiffPremAuto.Text = e.Item.Cells[25].Text.Trim();
                    //txtDiffPremTotal.Text = e.Item.Cells[27].Text.Trim();
                    //txtAdditionalPremium.Text = e.Item.Cells[28].Text.Trim();
                    break;

                case "Print":
                    DataGridGroup.DataSource = null;

                    string date2 = e.Item.Cells[3].Text.Trim();
                    if (date2.ToString().Trim() == "&nbsp;")
                    {
                        throw new Exception("This Endorsement is not Applied.");
                    }

                    EPolicy.TaskControl.Yacht taskControl2 = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
                    var CustomerNo = taskControl2.Customer.CustomerNo;
                    var AdditionalPremium = e.Item.Cells[27].Text.Trim();//e.Item.Cells[27].Text.Trim();

                    int s = int.Parse(e.Item.Cells[2].Text);
                    string comments = e.Item.Cells[11].Text.Trim();
                    EPolicy.TaskControl.Yacht newOPP3 = (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(s, userID);
                    int OPPEndorID = int.Parse(e.Item.Cells[1].Text);

                    //Print Document
                    try
                    {
                        List<string> mergePaths = new List<string>();
                        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

                        int taskControlID = taskControl.TaskControlID;

                        if (AdditionalPremium != "0")
                            mergePaths = PrintEndosoInvoice(mergePaths, OPPEndorID, taskControlID);

                        string ProcessedPath = System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"];
                        //Generar PDF
                        OPTIMAIns.CreatePDFBatch mergeFinal = new OPTIMAIns.CreatePDFBatch();
                        string FinalFile = "";

                        string ImgPath = "";
                        Uri pathAsUri = null;


                        ImgPath = Server.MapPath("Images2\\guardianLogo.png");
                        pathAsUri = new Uri(ImgPath);

                        GetReportEndoso_YachtTableAdapters.GetReportEndoso_YachtTableAdapter ds = new GetReportEndoso_YachtTableAdapters.GetReportEndoso_YachtTableAdapter();
                        Microsoft.Reporting.WebForms.ReportDataSource rds = new Microsoft.Reporting.WebForms.ReportDataSource("GetReportEndoso_Yacht", (DataTable)ds.GetData(OPPEndorID));

                        ReportParameter[] parameters = new ReportParameter[3];
                        parameters[0] = new ReportParameter("CustomerNo", CustomerNo.ToString().Trim());
                        parameters[1] = new ReportParameter("AdditionalPremium", AdditionalPremium.ToString().Trim());
                        parameters[2] = new ReportParameter("ImgPath", pathAsUri != null ? pathAsUri.AbsoluteUri : "");

                        Microsoft.Reporting.WebForms.ReportViewer viewer = new Microsoft.Reporting.WebForms.ReportViewer();
                        viewer.LocalReport.DataSources.Clear();
                        viewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                        viewer.LocalReport.ReportPath = Server.MapPath("Reports/EndososYacht.rdlc");
                        viewer.LocalReport.EnableExternalImages = true;
                        viewer.LocalReport.SetParameters(parameters);
                        viewer.LocalReport.DataSources.Add(rds);
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

                        mergePaths.Add(System.Configuration.ConfigurationManager.AppSettings["ExportsFilesPathName"] + _FileName);

                        FinalFile = mergeFinal.MergePDFFiles(mergePaths, ProcessedPath, taskControl.TaskControlID.ToString());
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + FinalFile + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "key", "window.open('ExportFiles/" + _FileName + "','Reports','addressbar=no,status=1,menubar=0,scrollbars=1,resizable=1,copyhistory=no,width=900,height=700');", true);
                    }
                    catch (Exception ecp)
                    {
                        LogError(ecp);
                        throw new Exception(ecp.Message);
                    }

                    break;

                //case "PrintIDCard":

                //    //var MaxID = (DataGridGroup.Items.Cast<DataGridItem>()).Max(r => r.o[0].Cells[3]);
                //    var MaxNum = "";
                //    for (int r = 0; r < DataGridGroup.Items.Count; r++)
                //    {
                //        if (DataGridGroup.Items[r].Cells[3].Text.Trim() != "&nbsp;" && DataGridGroup.Items[r].Cells[3].Text.Trim() != "")
                //            MaxNum = DataGridGroup.Items[r].Cells[3].Text.Trim();
                //    }

                //    //if (e.Item.Cells[3].Text.Trim().Replace("&nbsp;", "") != MaxNum)
                //    //    throw new Exception("Please select the last Endorsement Applied.");

                //    DataGridGroup.DataSource = null;

                //    date2 = e.Item.Cells[3].Text.Trim();
                //    if (date2.ToString().Trim() == "&nbsp;")
                //    {
                //        throw new Exception("This Endorsement is not Applied.");
                //    }

                //    taskControl2 = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
                //    CustomerNo = taskControl2.Customer.CustomerNo;
                //    AdditionalPremium = e.Item.Cells[27].Text.Trim();//e.Item.Cells[27].Text.Trim();

                //    s = int.Parse(e.Item.Cells[2].Text);
                //    comments = e.Item.Cells[11].Text.Trim();
                //    newOPP3 = (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl.GetTaskControlByTaskControlID(s, userID);
                //    OPPEndorID = int.Parse(e.Item.Cells[1].Text);


                //    var oppLastApplied = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
                //    if (DataGridGroup.Items.Count > 0)
                //    {
                //        for (int y = DataGridGroup.Items.Count - 1; y >= 0; y--)
                //        {

                //            if (DataGridGroup.Items[y].Cells[3].Text.Trim() != "" && DataGridGroup.Items[y].Cells[3].Text.Trim() != "&nbsp;")
                //            {
                //                if (DataGridGroup.Items.Count > 1)
                //                {
                //                    oppLastApplied =
                //                    (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl
                //                    .GetTaskControlByTaskControlID(Convert.ToInt32(DataGridGroup.Items[y - 1].Cells[2].Text.Trim()), userID);
                //                    oppLastApplied.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                //                    oppLastApplied.IsEndorsement = true;
                //                    break;
                //                }
                //                else
                //                {
                //                    oppLastApplied =
                //                 (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl
                //                 .GetTaskControlByTaskControlID(Convert.ToInt32(DataGridGroup.Items[y].Cells[2].Text.Trim()), userID);
                //                    oppLastApplied.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                //                    oppLastApplied.IsEndorsement = true;
                //                    break;
                //                }
                //            }
                //        }
                //        if (oppLastApplied.TaskControlID == newOPP3.TaskControlID)
                //        {
                //            oppLastApplied = taskControl2;
                //        }
                //        EPolicy.TaskControl.Yacht[] Autos = new EPolicy.TaskControl.Yacht[] { oppLastApplied, newOPP3 };
                //        List<String> CarsAdded = new List<String>();
                //        //EndorVehicleChanges(Autos, ref CarsAdded);

                //        //Print Document
                //        try
                //        {
                //            if (CarsAdded.Count == 0)
                //            {
                //                throw new Exception("No vehicles where added!");
                //            }
                //            //PrintIDCards(newOPP3, CarsAdded);
                //        }
                //        catch (Exception ecp)
                //        {
                //            throw new Exception(ecp.Message);
                //        }
                //    }

                //    break;

                default: //Page
                    DataGridGroup.CurrentPageIndex = int.Parse(e.CommandArgument.ToString()) - 1;

                    DataGridGroup.DataSource = (DataTable)Session["dtPermission"];
                    DataGridGroup.DataBind();
                    break;
            }
        }
        catch (Exception exp)
        {
            lblRecHeader.Text = exp.Message;
            mpeSeleccion.Show();
        }
    }
    protected void DataGridGroup_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        DataTable dtCol = (DataTable)Session["DtEndorsement"];
        DataColumnCollection dc = dtCol.Columns;

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DateTime EndoEffectiveField;

            if (DataBinder.Eval(e.Item.DataItem, "EndoEffective") != System.DBNull.Value)
            {
                EndoEffectiveField = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "EndoEffective", "{0:MM/dd/yyyy}"));
                e.Item.Cells[4].Text = EndoEffectiveField.ToShortDateString();
            }
        }
    }

    public static string StripHTML(string input)
    {
        return Regex.Replace(input, "<.*?>", String.Empty);
    }

    protected List<string> PrintEndosoInvoice(List<string> mergePaths, int OppEndorsementID, int taskControlID)
    {
        try
        {
            string ProcessedPath = ConfigurationManager.AppSettings["ExportsFilesPathName"];
            EPolicy.TaskControl.Yacht taskControl1 = (EPolicy.TaskControl.Yacht)Session["TaskControl"];
            GetOppEndorsementByOppEndorsementIDForInvoince_VI_YachtTableAdapters.GetOppEndorsementByOppEndorsementIDForInvoince_VI_YachtTableAdapter ds1 = new GetOppEndorsementByOppEndorsementIDForInvoince_VI_YachtTableAdapters.GetOppEndorsementByOppEndorsementIDForInvoince_VI_YachtTableAdapter();
            ReportDataSource rds1 = new ReportDataSource();
            rds1 = new ReportDataSource("GetOppEndorsementByOppEndorsementIDForInvoince_VI_Yacht", (DataTable)ds1.GetData(taskControlID, OppEndorsementID));

            //Nuevo
            string ImgPath = "", AgentDesc = "";
            Uri pathAsUri = null;
            //DataTable dt = null, dtAgent = null;

            //dt = GetImageLogoByAgentID(taskControl1.Agent.ToString().Trim());
            //dtAgent = GetAgentByAgentID(taskControl1.Agent.ToString().Trim());

            //if (dtAgent.Rows.Count > 0)
            //{
            //    AgentDesc = dtAgent.Rows[0]["AgentDesc"].ToString().Trim();
            //}

            //if (dt.Rows.Count > 0)
            //{
            //    ImgPath = Server.MapPath("Images2\\AgencyLogos\\" + dt.Rows[0]["ImgPath"].ToString().Trim());
            //    pathAsUri = new Uri(ImgPath);
            //}
            //else if (AgentDesc.ToUpper().Contains("GUARDIAN"))
            //{
            //    ImgPath = Server.MapPath("Images2\\AgencyLogos\\guardianLogo.png");
            //    pathAsUri = new Uri(ImgPath);
            //}
            //else
            //{

            //}

            ImgPath = Server.MapPath("Images2\\MidOcean_logo.png");
            pathAsUri = new Uri(ImgPath);

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("ImgPath", pathAsUri != null ? pathAsUri.AbsoluteUri : "");

            ReportViewer viewer1 = new ReportViewer();
            viewer1.LocalReport.DataSources.Clear();
            viewer1.ProcessingMode = ProcessingMode.Local;
            viewer1.LocalReport.ReportPath = Server.MapPath("Reports/VI/EndorInvoice_VI_Yacht.rdlc");
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


            string fileName1 = "PolicyNo- " + taskControlID.ToString().Trim() + "-" + taskControlID.ToString().Trim() + "-Com5";
            string _FileName1 = "PolicyNo- " + taskControlID.ToString().Trim() + "-" + taskControlID.ToString().Trim() + "-Com5" + ".pdf";

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
            LogError(ecp);
            throw new Exception(ecp.Message.ToString());
        }
    }

    private static DataTable GetImageLogoByAgentID(string AgentID)
    {
        DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[1];

        DbRequestXmlCooker.AttachCookItem("AgentID",
            SqlDbType.VarChar, 10, AgentID.ToString(),
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
            dt = exec.GetQuery("GetAgencyLogoByAgentID", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve the liability rates.", ex);
        }
    }

    private static DataTable GetAgentByAgentID(string AgentID)
    {
        DbRequestXmlCookRequestItem[] cookItems =
            new DbRequestXmlCookRequestItem[1];

        DbRequestXmlCooker.AttachCookItem("AgentID",
            SqlDbType.VarChar, 10, AgentID.ToString(),
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
            dt = exec.GetQuery("GetAgentByAgentID", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve the liability rates.", ex);
        }
    }

    private void FillDataGrid()
    {
        EPolicy.Login.Login cp = HttpContext.Current.User as EPolicy.Login.Login;
        int userID = 0;
        userID = int.Parse(cp.Identity.Name.Split("|".ToCharArray())[1]);
        EPolicy.TaskControl.Yacht taskControl = (EPolicy.TaskControl.Yacht)Session["TaskControl"];

        DataGridGroup.DataSource = null;
        DataTable DtEndorsement = null;

        EPolicy.TaskControl.PersonalPackage tcOPP = new EPolicy.TaskControl.PersonalPackage(true);
        tcOPP.TaskControlID = taskControl.TaskControlID;

        DtEndorsement = tcOPP.EndorsementCollection;

        Session.Remove("DtEndorsement");
        Session.Add("DtEndorsement", DtEndorsement);

        if (DtEndorsement != null)
        {
            if (DtEndorsement.Rows.Count != 0)
            {
                DataGridGroup.DataSource = DtEndorsement;
                DataGridGroup.DataBind();
                DataGridGroup.Visible = true;

                if (DataGridGroup.Items.Count > 0)
                {
                    for (int i = DataGridGroup.Items.Count - 1; i >= 0; i--)
                    {
                        if (DataGridGroup.Items[i].Cells[3].Text.Trim() != "" && DataGridGroup.Items[i].Cells[3].Text.Trim() != "&nbsp;")
                        {
                            EPolicy.TaskControl.Yacht opp =
                                (EPolicy.TaskControl.Yacht)EPolicy.TaskControl.TaskControl
                                .GetTaskControlByTaskControlID(Convert.ToInt32(DataGridGroup.Items[i].Cells[2].Text.Trim()), userID);
                            opp.Mode = (int)EPolicy.TaskControl.TaskControl.TaskControlMode.CLEAR;
                            opp.IsEndorsement = true;
                            ShowCurrent(true);
                            txtCurrentPremium.Text = GetNetTotalPremium(opp.TaskControlID, opp.isQuote).ToString();
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            ShowCurrent(false);
            DataGridGroup.DataSource = null;
            DataGridGroup.DataBind();
        }
    }

    private void ShowCurrent(bool show)
    {
        lblCurrentPremium.Visible = show;
        txtCurrentPremium.Visible = show;
        lblCurrentPremiumCurrency.Visible = show;
    }

    public void SendEndosoToPPS(int TaskControlID)
    {
        string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();

        SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand();
        DataTable PPSPolicy = new DataTable();
        DataTable dt =  GetYachtToPPSByTaskControlIDEndoso(TaskControlID);
        DataTable dtTender = GetYachtToPPSByTaskControlIDInfoTenderLimit(TaskControlID);
        DataTable dtTenderDesc = GetYachtToPPSByTaskControlIDInfoTenderLimitDesc(TaskControlID);
        DataTable dtSurvey = GetYachtToPPSByTaskControlIDInfoSurvey(TaskControlID);
        DataTable dtReport = GetYachtToPPSByTaskControlIDInfoReport(TaskControlID);
        DataTable dtNavigation = GetNavigationLimitCollectionToPPSByTaskControlID(TaskControlID);
        DataTable dtOPP = GetOPPEndorsementToPPSByTaskControlID(TaskControlID);
        DataTable dtYachtCoverages = GetYachtCoveragesPPS();
        System.Data.DataTable dtReinsAsl = new System.Data.DataTable();
        System.Data.DataTable dtOtherCvrgDetail = new System.Data.DataTable();
        string CoverageCodesXml = "";
        string OtherCvrgDetailXml = "";
        string InvLineXml = "";

        try
        {
            dtReinsAsl.TableName = "Coverages";
            dtReinsAsl.Columns.Add("ReinsAsl");
            dtReinsAsl.Columns.Add("Desc");
            dtReinsAsl.Columns.Add("CoveragePremium");
            dtReinsAsl.Columns.Add("Total");
            dtReinsAsl.Columns.Add("Lim1");
            dtReinsAsl.Columns.Add("Lim2");
            dtReinsAsl.Columns.Add("Lim3");
            dtReinsAsl.Columns.Add("Lim4");
            dtReinsAsl.Columns.Add("Lim5");
            dtReinsAsl.Columns.Add("Deductible");
            dtReinsAsl.Columns.Add("MinDeductible");

            DataRow row = dtReinsAsl.NewRow(); //Medical Payments
            row["ReinsAsl"] = "59080";
            row["Desc"] = "";
            row["CoveragePremium"] = dt.Rows[0]["MedicalPaymentPremiumTotal"].ToString().Replace("$", "").Replace(",", "").Trim();
            row["Total"] = "";
            string firstMedicalPayment = "", secondMedicalPayment = "";
            string[] arrayOfOtherMedical = { "", "" };
            string[] arrayOfMedicalPayment = { "", "" };

            if (txtOtherMedicalPayment.Text.Trim() == "" && ddlMedicalPayment.SelectedItem.Text == "")
            {
                firstMedicalPayment = "";
                secondMedicalPayment = "";
            }
            else if (ddlMedicalPayment.SelectedItem.Text == "")
            {
                arrayOfOtherMedical = txtOtherMedicalPayment.Text.Split('/');
                firstMedicalPayment = arrayOfOtherMedical[0].Trim().Replace("$", "").Replace(",", "");
                secondMedicalPayment = arrayOfOtherMedical[1].Trim().Replace("$", "").Replace(",", "");
            }

            else if (txtOtherMedicalPayment.Text.Trim() == "")
            {
                arrayOfMedicalPayment = ddlMedicalPayment.SelectedItem.Text.Split('/');
                firstMedicalPayment = arrayOfMedicalPayment[0].Trim().Replace("$", "").Replace(",", "");
                secondMedicalPayment = arrayOfMedicalPayment[1].Trim().Replace("$", "").Replace(",", "");
            }
            row["Lim1"] = firstMedicalPayment;
            row["Lim2"] = secondMedicalPayment;
            row["Lim3"] = "0.00";
            row["Lim4"] = firstMedicalPayment;
            row["Lim5"] = "0.00";
            row["Deductible"] = "0.00";
            row["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row);

            DataRow row2 = dtReinsAsl.NewRow(); //Hull Limit con segundo deducible
            row2["ReinsAsl"] = "58080";
            row2["Desc"] = "";
            if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                row2["CoveragePremium"] = "0.00";
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                row2["CoveragePremium"] = (double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.40).ToString();
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                row2["CoveragePremium"] = (double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.40).ToString();
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
            {
                double num1 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2) * 0.40;
                row2["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
            {
                double num1 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2) * 0.40;
                row2["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
            {
                double num1 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2) * 0.40;
                row2["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
            {
                double num1 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num3 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2 + num3) * 0.40;
                row2["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
                row2["CoveragePremium"] = (double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.40).ToString();
            row2["Total"] = "";
            row2["Lim1"] = dt.Rows[0]["HullLimit"].ToString().Replace("$", "").Replace(",", "").Trim();
            row2["Lim2"] = "0.00";
            row2["Lim3"] = "0.00";
            string[] arrayOfDeductible = { "", "" };
            string deductible2 = "";
            if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
            {
                row2["Lim4"] = "0.00";
            }
            else
            {
                arrayOfDeductible = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                if (arrayOfDeductible.Length > 1)
                {
                    deductible2 = arrayOfDeductible[1].Trim().Replace("%", "");
                    double ded2 = double.Parse(deductible2) / 100.00;
                    row2["Lim4"] = ded2.ToString();
                }
                else
                {
                    row2["Lim4"] = "0.00";
                }
            }

            row2["Lim5"] = "0.00";
            if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
            {
                row2["Deductible"] = "0.00";
            }
            else
            {
                arrayOfDeductible = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                if (arrayOfDeductible.Length > 1)
                {
                    deductible2 = arrayOfDeductible[1].Trim().Replace("%", "");
                    double ded2 = double.Parse(deductible2) / 100.00;
                    row2["Deductible"] = ded2.ToString();
                }
                else
                {
                    row2["Deductible"] = "0.00";
                }
            }
            row2["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row2);

            DataRow row9 = dtReinsAsl.NewRow(); //Hull Limit con primer deducible
            row9["ReinsAsl"] = "60080";
            row9["Desc"] = "";
            if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                row9["CoveragePremium"] = "0.00";
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                row9["CoveragePremium"] = (double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.60).ToString();
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                row9["CoveragePremium"] = (double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.60).ToString();
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
            {
                double num1 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2) * 0.60;
                row9["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
            {
                double num1 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2) * 0.60;
                row9["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
            {
                double num1 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2) * 0.60;
                row9["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
            {
                double num1 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim());
                double num3 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                double total = (num1 + num2 + num3) * 0.60;
                row9["CoveragePremium"] = total.ToString();
            }
            else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
                row9["CoveragePremium"] = (double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.60).ToString();
            row9["Total"] = "";
            row9["Lim1"] = dt.Rows[0]["HullLimit"].ToString().Replace("$", "").Replace(",", "").Trim();
            row9["Lim2"] = "0.00";
            row9["Lim3"] = "0.00";
            string[] arrayOfDeductible2 = { "", "" };
            string deductible1 = "";
            if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
            {
                row9["Lim4"] = "0.00";
            }
            else
            {
                arrayOfDeductible2 = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                deductible1 = arrayOfDeductible2[0].Trim().Replace("%", "");
                double ded1 = double.Parse(deductible1) / 100.00;
                row9["Lim4"] = ded1.ToString();
            }

            row9["Lim5"] = "0.00";
            if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
            {
                row9["Deductible"] = "0.00";
            }
            else
            {
                arrayOfDeductible2 = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                deductible1 = arrayOfDeductible2[0].Trim().Replace("%", "");
                double ded1 = double.Parse(deductible1) / 100.00;
                row9["Deductible"] = ded1.ToString();
            }
            row9["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row9);

            DataRow row3 = dtReinsAsl.NewRow(); //Protection & Indemnity
            row3["ReinsAsl"] = "61080";
            row3["Desc"] = "";
            row3["CoveragePremium"] = dt.Rows[0]["PIPremium"].ToString().Replace("$", "").Replace(",", "").Trim();
            row3["Total"] = "";
            string pi = "";
            if (ddlPI.SelectedItem.Text == "" && ddlPILiabilityOnly.SelectedItem.Text == "" && txtOtherPI.Text != "")
                pi = txtOtherPI.Text;
            else if (ddlPI.SelectedItem.Text == "" && ddlPILiabilityOnly.SelectedItem.Text != "" && txtOtherPI.Text == "")
                pi = ddlPILiabilityOnly.SelectedItem.Text;
            else if (ddlPI.SelectedItem.Text != "" && ddlPILiabilityOnly.SelectedItem.Text == "" && txtOtherPI.Text == "")
                pi = ddlPI.SelectedItem.Text;
            row3["Lim1"] = pi.ToString().Replace("$", "").Replace(",", "").Trim();
            row3["Lim2"] = "0.00";
            row3["Lim3"] = "0.00";
            row3["Lim4"] = pi.ToString().Replace("$", "").Replace(",", "").Trim();
            row3["Lim5"] = "0.00";
            row3["Deductible"] = "0.00";
            row3["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row3);

            DataRow row4 = dtReinsAsl.NewRow(); //Personal Effects
            row4["ReinsAsl"] = "62080";
            row4["Desc"] = "";
            row4["CoveragePremium"] = dt.Rows[0]["PersonalEffectsPremium"].ToString().Replace("$", "").Replace(",", "").Trim();
            row4["Total"] = "";
            row4["Lim1"] = dt.Rows[0]["PersonalEffects"].ToString().Replace("$", "").Replace(",", "").Trim();
            row4["Lim2"] = "0.00";
            row4["Lim3"] = "0.00";
            row4["Lim4"] = dt.Rows[0]["PEDeductible"].ToString().Replace("$", "").Replace(",", "").Trim();
            row4["Lim5"] = "0.00";
            row4["Deductible"] = dt.Rows[0]["PEDeductible"].ToString().Replace("$", "").Replace(",", "").Trim();
            row4["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row4);

            DataRow row5 = dtReinsAsl.NewRow(); //Tender Limit con primer deducible
            row5["ReinsAsl"] = "66080";
            row5["Desc"] = "";
            row5["CoveragePremium"] = "0.00";
            row5["Total"] = "";
            if (dtTender.Rows.Count > 0)
            {
                if (dtTender.Rows[0]["TenderLimitAmount1"].ToString() == "")
                    row5["Lim1"] = "0.00";
                else
                    row5["Lim1"] = dtTender.Rows[0]["TenderLimitAmount1"].ToString().Replace("$", "").Replace(",", "").Trim();
                row5["Lim2"] = "0.00";
                row5["Lim3"] = "0.00";
                if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
                {
                    row5["Lim4"] = "0.00";
                    row5["Lim5"] = "0.00";
                    row5["Deductible"] = "0.00";
                    row5["MinDeductible"] = "0.00";
                }
                else
                {
                    string[] arrayOfDeductible3 = { "", "" };
                    string deductible3 = "";
                    arrayOfDeductible3 = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                    deductible3 = arrayOfDeductible3[0].Trim().Replace("%", "");
                    double ded3 = double.Parse(deductible3) / 100.00;
                    row5["Lim4"] = ded3.ToString();
                    row5["Lim5"] = "0.00";
                    row5["Deductible"] = ded3.ToString();
                    row5["MinDeductible"] = "0.00";
                }

            }
            else
            {
                row5["Lim1"] = "0.00";
                row5["Lim2"] = "0.00";
                row5["Lim3"] = "0.00";
                row5["Lim4"] = "0.00";
                row5["Lim5"] = "0.00";
                row5["Deductible"] = "0.00";
                row5["MinDeductible"] = "0.00";
            }

            dtReinsAsl.Rows.Add(row5);

            DataRow row6 = dtReinsAsl.NewRow(); //Trailer
            row6["ReinsAsl"] = "65080";
            row6["Desc"] = "";
            row6["CoveragePremium"] = dt.Rows[0]["TrailerPremium"].ToString().Replace("$", "").Replace(",", "").Trim();
            row6["Total"] = "";
            row6["Lim1"] = dt.Rows[0]["Trailer"].ToString().Replace("$", "").Replace(",", "").Trim();
            row6["Lim2"] = "0.00";
            row6["Lim3"] = "0.00";
            row6["Lim4"] = dt.Rows[0]["Trailer"].ToString().Replace("$", "").Replace(",", "").Trim();
            row6["Lim5"] = "0.00";
            row6["Deductible"] = "0.00";
            row6["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row6);

            DataRow row7 = dtReinsAsl.NewRow(); //Uninsured Boater
            row7["ReinsAsl"] = "67080";
            row7["Desc"] = "";
            row7["CoveragePremium"] = dt.Rows[0]["OtherUninsuredBoaterPremium"].ToString().Replace("$", "").Replace(",", "").Trim();
            row7["Total"] = "";
            string uninsuredBoater = "";
            if (ddlUninsuredBoaters.SelectedItem.Text == "" && txtOtherUninsuredBoater.Text != "")
                uninsuredBoater = txtOtherUninsuredBoater.Text;
            else if (ddlUninsuredBoaters.SelectedItem.Text != "" && txtOtherUninsuredBoater.Text == "")
                uninsuredBoater = ddlUninsuredBoaters.SelectedItem.Text;
            row7["Lim1"] = uninsuredBoater.Replace("$", "").Replace(",", "");
            row7["Lim2"] = "0.00";
            row7["Lim3"] = "0.00";
            row7["Lim4"] = uninsuredBoater.Replace("$", "").Replace(",", "");
            row7["Lim5"] = "0.00";
            row7["Deductible"] = "0.00";
            row7["MinDeductible"] = "0.00";
            dtReinsAsl.Rows.Add(row7);


            DataRow row10 = dtReinsAsl.NewRow(); //Tender Limit con segundo deducible
            row10["ReinsAsl"] = "63080";
            row10["Desc"] = "";
            row10["CoveragePremium"] = "0.00";
            row10["Total"] = "";
            if (dtTender.Rows.Count > 0)
            {
                if (dtTender.Rows[0]["TenderLimitAmount1"].ToString() == "")
                    row10["Lim1"] = "0.00";
                else
                    row10["Lim1"] = dtTender.Rows[0]["TenderLimitAmount1"].ToString().Replace("$", "").Replace(",", "").Trim();
                row10["Lim2"] = "0.00";
                row10["Lim3"] = "0.00";
                if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
                {
                    row10["Lim4"] = "0.00";
                    row10["Lim5"] = "0.00";
                    row10["Deductible"] = "0.00";
                    row10["MinDeductible"] = "0.00";
                }
                else
                {
                    string[] arrayOfDeductible4 = { "", "" };
                    string deductible4 = "";
                    arrayOfDeductible4 = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                    if (arrayOfDeductible4.Length > 1)
                    {
                        deductible4 = arrayOfDeductible4[1].Trim().Replace("%", "");
                        double ded4 = double.Parse(deductible4) / 100.00;
                        row10["Lim4"] = ded4.ToString();
                        row10["Lim5"] = "0.00";
                        row10["Deductible"] = ded4.ToString();
                        row10["MinDeductible"] = "0.00";
                    }
                    else
                    {
                        row10["Lim4"] = "0.00";
                        row10["Lim5"] = "0.00";
                        row10["Deductible"] = "0.00";
                        row10["MinDeductible"] = "0.00";
                    }
                }


            }
            else
            {
                row10["Lim1"] = "0.00";
                row10["Lim2"] = "0.00";
                row10["Lim3"] = "0.00";
                row10["Lim4"] = "0.00";
                row10["Lim5"] = "0.00";
                row10["Deductible"] = "0.00";
                row10["MinDeductible"] = "0.00";
            }

            dtReinsAsl.Rows.Add(row10);

            dtOtherCvrgDetail.TableName = "OtherCvrgDetail";
            dtOtherCvrgDetail.Columns.Add("ReinsAsl");
            dtOtherCvrgDetail.Columns.Add("DisplayAs");
            dtOtherCvrgDetail.Columns.Add("IndexNo");
            dtOtherCvrgDetail.Columns.Add("Limit1");
            dtOtherCvrgDetail.Columns.Add("Limit2");
            dtOtherCvrgDetail.Columns.Add("Tag");

            if (dtReinsAsl.Rows.Count > 0)
            {
                for (int i = 0; i < dtReinsAsl.Rows.Count; i++)
                {
                    int Count = 1;
                    DataRow row8 = dtOtherCvrgDetail.NewRow();
                    switch (dtReinsAsl.Rows[i]["ReinsAsl"].ToString())
                    {
                        case "59080": //Medical Payments
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"];
                            row8[1] = "Medical Payments";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = dtReinsAsl.Rows[i]["Lim2"];
                            row8[5] = "Per Person/Per Occurrence";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "58080": //Hull Limit con segundo deducible
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"];
                            row8[1] = "Hull && Machinery - Windstorm";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "60080": //Hull Limit con primer deducible
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"];
                            row8[1] = "Hull && Machinery - All Other Perils";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "61080": //Protection & Indemnity
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Protection & Indemnity";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Each Occurrence";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "62080": //Personal Effects
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Personal Contents";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "66080": //Tender Limit con primer deducible
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Tender Limit - All Other Perils";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "63080": //Tender Limit con segundo deducible
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Tender Limit - Windstorm";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "65080": //Trailers
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Trailer";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "67080": //Uninsured Boaters
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Uninsured Boaters";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Each Occurrence";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;
                    }
                }
            }

            using (StringWriter sw = new StringWriter())
            {
                dtReinsAsl.WriteXml(sw);
                CoverageCodesXml = sw.ToString();
                XmlDocument docSave = new XmlDocument();
                docSave.LoadXml(sw.ToString());
                docSave.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + TaskControlID + "_OtherCvrg1" + ".xml");
                sw.Close();
                docSave = null;
            }

            using (StringWriter sw = new StringWriter())
            {
                dtOtherCvrgDetail.WriteXml(sw);
                OtherCvrgDetailXml = sw.ToString();
                XmlDocument docSave = new XmlDocument();
                docSave.LoadXml(sw.ToString());
                docSave.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + TaskControlID + "_OtherCvrgDetail1" + ".xml");
                sw.Close();
                docSave = null;
            }


            if (dt.Rows.Count > 0)
            {

                cmd.CommandText = "sproc_ConsumeXMLePPS-YACHT_END";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();


                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Incept", DateTime.Parse(txtEffectiveDate.Text.ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00");
                cmd.Parameters.AddWithValue("@Expire", DateTime.Parse(txtExpirationDate.Text.ToString().Trim()).ToString("yyyy-MM-dd") + "T00:00:00");
                if (txtSuffix.Text.Trim() != "" && txtSuffix.Text.Trim() != "00")
                {
                    cmd.Parameters.AddWithValue("@PolicyID", txtPolicyType.Text.Trim() + txtPolicyNo.Text.Trim() + "-" + txtSuffix.Text.Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PolicyID", txtPolicyType.Text.Trim() + txtPolicyNo.Text.Trim());
                }

                //cmd.Parameters.AddWithValue("@PolicyID", dt.Rows[0]["PolicyType"].ToString().Trim());
                //cmd.Parameters.AddWithValue("@TransType", "NEW");
                //cmd.Parameters.AddWithValue("@RenewalOf", "");
                //cmd.Parameters.AddWithValue("@Client", "0");

                string FN = dt.Rows[0]["FirstNa"].ToString().Trim();
                string LN = dt.Rows[0]["LastNa1"].ToString().Trim();
                string BusType = "1";
                string BusFlag = "0";
                BusFlag = "0";

                if (chkIsCommercial.Checked)
                {
                    FN = "";
                    LN = txtCompanyName.Text.Trim();
                    BusFlag = "0";
                }

                string ComRate = "0.0000000e+000";

                DataTable DtCommision = GetCommissionAgentRateByAgentIDEND(TaskControlID.ToString().Trim()); 

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
                cmd.Parameters.AddWithValue("@DispImagePerson", "Person");
                cmd.Parameters.AddWithValue("@DispImagePolicy", "Policy");
                cmd.Parameters.AddWithValue("@SpecEndorse", "");
                cmd.Parameters.AddWithValue("@SID", "0");
                cmd.Parameters.AddWithValue("@UDPolicyID", "0");
                cmd.Parameters.AddWithValue("@PreparedBy", dt.Rows[0]["EnteredBy"].ToString().Trim());
                cmd.Parameters.AddWithValue("@ExcessLink", "0");
                if (dt.Rows[0]["IsCommercial"].ToString() == "True")
                    cmd.Parameters.AddWithValue("@PolSubType", "Cml");
                else
                    cmd.Parameters.AddWithValue("@PolSubType", "Pvt");
                cmd.Parameters.AddWithValue("@ReinsPcnt", "0.0000000e+000");
                cmd.Parameters.AddWithValue("@Assessment", "0.0000");
                cmd.Parameters.AddWithValue("@PayDate", "");
                cmd.Parameters.AddWithValue("@Polrelat", "NI");
                if (LN.Trim() == "")
                    cmd.Parameters.AddWithValue("@LastName", SqlDbType.NVarChar).Value = DBNull.Value;
                else
                    cmd.Parameters.AddWithValue("@LastName", LN);
                if (FN.Trim() == "")
                    cmd.Parameters.AddWithValue("@FirstName", SqlDbType.NVarChar).Value = DBNull.Value;
                else
                    cmd.Parameters.AddWithValue("@FirstName", FN);
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

                // Add the parameters for Bldgs
                cmd.Parameters.AddWithValue("@Descrip", dt.Rows[0]["BoatName"].ToString().Trim() + " - " + dt.Rows[0]["LOA"].ToString().Trim() + " " + dt.Rows[0]["BoatYear"].ToString().Trim());
                if (dt.Rows[0]["HomeportDesc"].ToString().Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@Location", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Location", dt.Rows[0]["HomeportDesc"].ToString().Trim());
                }
                cmd.Parameters.AddWithValue("@Island", "4");

                double hulllimit, personaleffects, tender1, totalInsVal = 0.00;

                if (dtTender.Rows.Count > 0 && dtTender.Rows[0]["TenderLimitAmount1"].ToString().Replace("$", "").Replace(",", "").Trim() != "")
                {
                    tender1 = double.Parse(dtTender.Rows[0]["TenderLimitAmount1"].ToString().Replace("$", "").Replace(",", "").Trim());
                }
                else
                {
                    tender1 = 0.00;
                }

                if (dt.Rows[0]["HullLimit"].ToString().Replace("$", "").Replace(",", "").Trim() != "")
                {
                    hulllimit = double.Parse(dt.Rows[0]["HullLimit"].ToString().Replace("$", "").Replace(",", "").Trim());
                }
                else
                {
                    hulllimit = 0.00;
                }

                if (dt.Rows[0]["PersonalEffects"].ToString().Replace("$", "").Replace(",", "").Trim() != "")
                {
                    personaleffects = double.Parse(dt.Rows[0]["PersonalEffects"].ToString().Replace("$", "").Replace(",", "").Trim());
                }
                else
                {
                    personaleffects = 0.00;
                }

                totalInsVal = tender1 + hulllimit + personaleffects;
                cmd.Parameters.AddWithValue("@InsVal", totalInsVal.ToString());
                cmd.Parameters.AddWithValue("@AnyNum", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@PayeeID", dt.Rows[0]["BankPPSID"].ToString().Trim());
                cmd.Parameters.AddWithValue("@LoanNo", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@PayeeID2", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@LoanNo2", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Families", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@RowHouse", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Rented", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Construction", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@ProtectionClass", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@YearBuilt", dt.Rows[0]["BoatYear"]);
                cmd.Parameters.AddWithValue("@FireDistrict", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Occupancy", SqlDbType.NVarChar).Value = DBNull.Value;
                string navigationLimitAll = "";
                if (dtNavigation.Rows.Count == 1)
                {
                    cmd.Parameters.Add("@NavLimit", dtNavigation.Rows[0]["NavigationLimitDesc"].ToString().Trim());
                }
                else if (dtNavigation.Rows.Count > 1)
                {
                    for (int i = 0; i < dtNavigation.Rows.Count; i++)
                    {
                        if (i == 0)
                            navigationLimitAll = dtNavigation.Rows[i]["NavigationLimitDesc"].ToString().Trim();
                        else
                            navigationLimitAll = navigationLimitAll + " " + dtNavigation.Rows[i]["NavigationLimitDesc"].ToString().Trim();
                    }

                    cmd.Parameters.Add("@NavLimit", navigationLimitAll);
                }
                else
                {
                    cmd.Parameters.Add("@NavLimit", SqlDbType.NVarChar).Value = DBNull.Value;

                }
                if (dtTender.Rows.Count > 0)
                {
                    if (dtTender.Rows[0]["TenderDesc1"].ToString().Trim() != "" && dtTender.Rows[0]["TenderSerial1"].ToString().Trim() != "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderDesc1"].ToString().Trim() + " " + dtTender.Rows[0]["TenderSerial1"].ToString().Trim());
                    else if (dtTender.Rows[0]["TenderDesc1"].ToString().Trim() != "" && dtTender.Rows[0]["TenderSerial1"].ToString().Trim() == "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderDesc1"].ToString().Trim());
                    else if (dtTender.Rows[0]["TenderDesc1"].ToString().Trim() == "" && dtTender.Rows[0]["TenderSerial1"].ToString().Trim() != "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderSerial1"].ToString().Trim());
                    else
                        cmd.Parameters.Add("@TenderText", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@TenderText", SqlDbType.NVarChar).Value = DBNull.Value;
                }

                if ((dt.Rows[0]["TrailerModel"].ToString().Trim() + dt.Rows[0]["TrailerSerial"].ToString().Trim()) == "")
                    cmd.Parameters.Add("@TrailerText", SqlDbType.NVarChar).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@TrailerText", dt.Rows[0]["TrailerModel"].ToString().Trim() + " " + dt.Rows[0]["TrailerSerial"].ToString().Trim());
                cmd.Parameters.Add("@VName", dt.Rows[0]["BoatName"].ToString().Trim());
                cmd.Parameters.Add("@Make", dt.Rows[0]["BoatBuilder"].ToString().Trim());
                cmd.Parameters.Add("@Model", dt.Rows[0]["BoatModel"].ToString().Trim());
                if (dt.Rows[0]["HullNumberRegistration"].ToString().Trim() == "")
                    cmd.Parameters.Add("@HIN", SqlDbType.NVarChar).Value = DBNull.Value;
                else
                    cmd.Parameters.Add("@HIN", dt.Rows[0]["HullNumberRegistration"].ToString().Trim());
                cmd.Parameters.AddWithValue("@LOA", dt.Rows[0]["LOA"].ToString().Trim());
                if (dt.Rows[0]["Engine"].ToString().Trim() == "" && dt.Rows[0]["EngineSerialNumber"].ToString().Trim() == "")
                {
                    cmd.Parameters.Add("@VesselProp", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else if (dt.Rows[0]["Engine"].ToString().Trim() == "" && dt.Rows[0]["EngineSerialNumber"].ToString().Trim() != "")
                {
                    cmd.Parameters.Add("@VesselProp", dt.Rows[0]["EngineSerialNumber"].ToString().Trim());
                }
                else if (dt.Rows[0]["Engine"].ToString().Trim() != "" && dt.Rows[0]["EngineSerialNumber"].ToString().Trim() == "")
                {
                    cmd.Parameters.Add("@VesselProp", dt.Rows[0]["Engine"].ToString().Trim());
                }
                else
                {
                    cmd.Parameters.Add("@VesselProp", dt.Rows[0]["Engine"].ToString().Trim() + " " + dt.Rows[0]["EngineSerialNumber"].ToString().Trim());
                }
                cmd.Parameters.AddWithValue("@Storeys", SqlDbType.NVarChar).Value = DBNull.Value;
                //OtherCoverage & OtherCovrgDetail
                cmd.Parameters.AddWithValue("@CoverageCodesXml", CoverageCodesXml);
                cmd.Parameters.AddWithValue("@OtherCvrgDetailXml", OtherCvrgDetailXml);

                //OPPEndorsement
                cmd.Parameters.AddWithValue("@AdditionalPremium", dtOPP.Rows[0]["AdditionalPremium"].ToString().Trim());

                DataTable dtInvLine = new DataTable();

                // Initialize data table if viewstate is null
                dtInvLine.TableName = "CoveragesInvLine";
                dtInvLine.Columns.Add("Description", typeof(string));
                dtInvLine.Columns.Add("ReinsAsl", typeof(string));
                dtInvLine.Columns.Add("Premium", typeof(string));

                // Add your data row
                DataRow dr = dtInvLine.NewRow();
                dr["Description"] = "WatercraftLimitTotal1PremiumPrimerDeducible";
                dr["ReinsAsl"] = "60080";
                if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                    dr["Premium"] = "0.00";
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                    dr["Premium"] = Convert.ToInt32(double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.60).ToString() + ".00";
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                    dr["Premium"] = Convert.ToInt32(double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.60).ToString() + ".00";
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                {
                    double num1 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double num2 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double total = (num1 + num2) * 0.60;
                    dr["Premium"] = Convert.ToInt32(total).ToString() + ".00";
                }
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
                {
                    double num1 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double total = (num1 + num2) * 0.60;
                    dr["Premium"] = Convert.ToInt32(total).ToString() + ".00";
                }
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
                {
                    double num1 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double total = (num1 + num2) * 0.60;
                    dr["Premium"] = Convert.ToInt32(total).ToString() + ".00";
                }
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
                {
                    double num1 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double num3 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double total = (num1 + num2 + num3) * 0.60;
                    dr["Premium"] = Convert.ToInt32(total).ToString() + ".00";
                }
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
                    dr["Premium"] = Convert.ToInt32(double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.60).ToString() + ".00";
                dtInvLine.Rows.Add(dr);

                DataRow dr2 = dtInvLine.NewRow();
                dr2["Description"] = "ProtectionandIndemnityPremium";
                dr2["ReinsAsl"] = "61080";
                if (dt.Rows[0]["PIPremium"].ToString().Trim() == "")
                    dr2["Premium"] = "0.00";
                else
                    dr2["Premium"] = Convert.ToInt32(double.Parse(dt.Rows[0]["PIPremium"].ToString().Replace("$", "").Replace(",", "").Trim())).ToString() + ".00";
                dtInvLine.Rows.Add(dr2);

                DataRow dr3 = dtInvLine.NewRow();
                dr3["Description"] = "MedicalPaymentsPremium";
                dr3["ReinsAsl"] = "59080";
                if (dt.Rows[0]["MedicalPaymentPremiumTotal"].ToString().Trim() == "")
                    dr3["Premium"] = "0.00";
                else
                    dr3["Premium"] = Convert.ToInt32(double.Parse(dt.Rows[0]["MedicalPaymentPremiumTotal"].ToString().Replace("$", "").Replace(",", "").Trim())).ToString() + ".00";
                dtInvLine.Rows.Add(dr3);

                DataRow dr4 = dtInvLine.NewRow();
                dr4["Description"] = "PersonalEffectPremium";
                dr4["ReinsAsl"] = "62080";
                if (dt.Rows[0]["PersonalEffectsPremium"].ToString().Trim() == "")
                    dr4["Premium"] = "0.00";
                else
                    dr4["Premium"] = Convert.ToInt32(double.Parse(dt.Rows[0]["PersonalEffectsPremium"].ToString().Replace("$", "").Replace(",", "").Trim())).ToString() + ".00";
                dtInvLine.Rows.Add(dr4);

                DataRow dr5 = dtInvLine.NewRow();
                dr5["Description"] = "TrailerPremium";
                dr5["ReinsAsl"] = "65080";
                if (dt.Rows[0]["TrailerPremium"].ToString().Trim() == "")
                    dr5["Premium"] = "0.00";
                else
                    dr5["Premium"] = Convert.ToInt32(double.Parse(dt.Rows[0]["TrailerPremium"].ToString().Replace("$", "").Replace(",", "").Trim())).ToString() + ".00";
                dtInvLine.Rows.Add(dr5);

                DataRow dr6 = dtInvLine.NewRow();
                dr6["Description"] = "UninsuredBoaterPremium";
                dr6["ReinsAsl"] = "67080";
                if (dt.Rows[0]["OtherUninsuredBoaterPremium"].ToString().Trim() == "")
                    dr6["Premium"] = "0.00";
                else
                    dr6["Premium"] = Convert.ToInt32(double.Parse(dt.Rows[0]["OtherUninsuredBoaterPremium"].ToString().Replace("$", "").Replace(",", "").Trim())).ToString() + ".00";
                dtInvLine.Rows.Add(dr6);

                DataRow dr7 = dtInvLine.NewRow();
                dr7["Description"] = "WatercraftLimitTotal1PremiumSegundoDeducible";
                dr7["ReinsAsl"] = "58080";
                if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                    dr7["Premium"] = "0.00";
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                    dr7["Premium"] = Convert.ToInt32(double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.40).ToString() + ".00";
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                    dr7["Premium"] = Convert.ToInt32(double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.40).ToString() + ".00";
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() == "")
                {
                    double num1 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double num2 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double total = (num1 + num2) * 0.40;
                    dr7["Premium"] = Convert.ToInt32(total).ToString() + ".00";
                }
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
                {
                    double num1 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double total = (num1 + num2) * 0.40;
                    dr7["Premium"] = Convert.ToInt32(total).ToString() + ".00";
                }
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
                {
                    double num1 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double total = (num1 + num2) * 0.40;
                    dr7["Premium"] = Convert.ToInt32(total).ToString() + ".00";
                }
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() != "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() != "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
                {
                    double num1 = double.Parse(dt.Rows[0]["Miscellaneous"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double num2 = double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double num3 = double.Parse(dt.Rows[0]["TripTransit"].ToString().Replace("$", "").Replace(",", "").Trim());
                    double total = (num1 + num2 + num3) * 0.40;
                    dr7["Premium"] = Convert.ToInt32(total).ToString() + ".00";
                }
                else if (dt.Rows[0]["TripTransit"].ToString().Trim() == "" && dt.Rows[0]["Miscellaneous"].ToString().Trim() == "" && dt.Rows[0]["WatercraftLimitTotal1"].ToString().Trim() != "")
                    dr7["Premium"] = Convert.ToInt32(double.Parse(dt.Rows[0]["WatercraftLimitTotal1"].ToString().Replace("$", "").Replace(",", "").Trim()) * 0.40).ToString() + ".00";
                dtInvLine.Rows.Add(dr7);

                DataRow dr8 = dtInvLine.NewRow();
                dr8["Description"] = "PrimerDeducibleTender";
                dr8["ReinsAsl"] = "66080";
                dr8["Premium"] = "0.00";
                dtInvLine.Rows.Add(dr8);

                DataRow dr9 = dtInvLine.NewRow();
                dr9["Description"] = "SegundoDeducibleTender";
                dr9["ReinsAsl"] = "63080";
                dr9["Premium"] = "0.00";
                dtInvLine.Rows.Add(dr9);

                if (dtYachtCoverages.Rows.Count > 0)
                {
                    for (int i = 0; i < dtInvLine.Rows.Count; i++)
                    {
                        for (int x = 0; x < dtYachtCoverages.Rows.Count; x++)
			            {
                            if (dtInvLine.Rows[i]["ReinsAsl"].ToString().Trim() == dtYachtCoverages.Rows[x]["ReinsAsl"].ToString().Trim())
                            {
                                if (double.Parse(dtInvLine.Rows[i]["Premium"].ToString().Trim()) == double.Parse(dtYachtCoverages.Rows[x]["Premium"].ToString().Trim()))
                                {
                                    dtInvLine.Rows[i]["Premium"] = "0.00";
                                }
                                else
                                {
                                    dtInvLine.Rows[i]["Premium"] = (Convert.ToInt32(double.Parse(dtInvLine.Rows[i]["Premium"].ToString().Trim()) - double.Parse(dtYachtCoverages.Rows[x]["Premium"].ToString().Trim()))).ToString() + ".00";
                                }
                            }
			            }
                        
                    }
                

                for (int i = 0; i < dtInvLine.Rows.Count; i++)
			    {
                    dtInvLine.Rows[i]["Premium"] = (Convert.ToInt32(double.Parse(dtInvLine.Rows[i]["Premium"].ToString().Trim()) * double.Parse(dtOPP.Rows[0]["Factor"].ToString().Trim()))).ToString() + ".00";
			    }

                double sumPrimas = double.Parse(dtInvLine.Rows[0]["Premium"].ToString()) + double.Parse(dtInvLine.Rows[1]["Premium"].ToString()) + double.Parse(dtInvLine.Rows[2]["Premium"].ToString())
                    + double.Parse(dtInvLine.Rows[3]["Premium"].ToString()) + double.Parse(dtInvLine.Rows[4]["Premium"].ToString()) + double.Parse(dtInvLine.Rows[5]["Premium"].ToString())
                    + double.Parse(dtInvLine.Rows[6]["Premium"].ToString()) + double.Parse(dtInvLine.Rows[7]["Premium"].ToString()) + double.Parse(dtInvLine.Rows[8]["Premium"].ToString());

                double additionalPremiumFactor = double.Parse(dtOPP.Rows[0]["AdditionalPremium"].ToString().Trim());

                int sumPrimasInt = Convert.ToInt32(sumPrimas);

                int totalPremiumFactorInt = Convert.ToInt32(additionalPremiumFactor);

                if (sumPrimasInt != totalPremiumFactorInt)
                {
                    int posSumPrimasInt = 0;
                    int posTotalPremiumFactorInt = 0;

                    if (sumPrimasInt < 0)
                    {
                        posSumPrimasInt = sumPrimasInt * -1;
                    }
                    else
                    {
                        posSumPrimasInt = sumPrimasInt;
                    }
                    if (totalPremiumFactorInt < 0)
                    {
                        posTotalPremiumFactorInt = totalPremiumFactorInt * -1;
                    }
                    else
                    {
                        posTotalPremiumFactorInt = totalPremiumFactorInt;
                    }

                    int difference = 0;

                    if(posSumPrimasInt > posTotalPremiumFactorInt)
                        difference  = posSumPrimasInt - posTotalPremiumFactorInt;
                    else
                        difference = posTotalPremiumFactorInt - posSumPrimasInt;

                    if (sumPrimasInt > 0)
                    {
                        if (sumPrimasInt > totalPremiumFactorInt)
                        {
                            for (int i = 0; i < dtInvLine.Rows.Count; i++)
                            {
                                if (dtInvLine.Rows[i]["Premium"].ToString() != "0.00")
                                {
                                    double newPrem = double.Parse(dtInvLine.Rows[i]["Premium"].ToString().Trim()) - double.Parse(difference.ToString());
                                    dtInvLine.Rows[i]["Premium"] = Convert.ToInt32(newPrem.ToString()) + ".00";
                                    break;
                                }
                            }
                        }
                        else if (sumPrimasInt < totalPremiumFactorInt)
                        {
                            for (int i = 0; i < dtInvLine.Rows.Count; i++)
                            {
                                if (dtInvLine.Rows[i]["Premium"].ToString() != "0.00")
                                {
                                    double newPrem = double.Parse(dtInvLine.Rows[i]["Premium"].ToString().Trim()) + double.Parse(difference.ToString());
                                    dtInvLine.Rows[i]["Premium"] = Convert.ToInt32(newPrem.ToString()) + ".00";
                                    break;
                                }
                            }
                        }
                    }
                    else if (sumPrimasInt < 0)
                    {
                        if (sumPrimasInt > totalPremiumFactorInt)
                        {
                            for (int i = 0; i < dtInvLine.Rows.Count; i++)
                            {
                                if (dtInvLine.Rows[i]["Premium"].ToString() != "0.00")
                                {
                                    double newPrem = double.Parse(dtInvLine.Rows[i]["Premium"].ToString().Trim()) + double.Parse(difference.ToString());
                                    dtInvLine.Rows[i]["Premium"] = Convert.ToInt32(newPrem.ToString()) + ".00";
                                    break;
                                }
                            }
                        }
                        else if (sumPrimasInt < totalPremiumFactorInt)
                        {
                            for (int i = 0; i < dtInvLine.Rows.Count; i++)
                            {
                                if (dtInvLine.Rows[i]["Premium"].ToString() != "0.00")
                                {
                                    double newPrem = double.Parse(dtInvLine.Rows[i]["Premium"].ToString().Trim()) - double.Parse(difference.ToString());
                                    dtInvLine.Rows[i]["Premium"] = Convert.ToInt32(newPrem.ToString()) + ".00";
                                    break;
                                }
                            }
                        }
                    }
                }

                    for (int i = dtInvLine.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow drDelete = dtInvLine.Rows[i];
                        if (drDelete["Premium"].ToString().Trim() == "0.00")
                            drDelete.Delete();
                    }
                    dtInvLine.AcceptChanges();

                }

                using (StringWriter sw = new StringWriter())
                {
                    dtInvLine.WriteXml(sw);
                    InvLineXml = sw.ToString();
                    XmlDocument docSave = new XmlDocument();
                    docSave.LoadXml(sw.ToString());
                    docSave.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + TaskControlID + "_InvLine" + ".xml");
                    sw.Close();
                    docSave = null;
                }

                cmd.Parameters.AddWithValue("@InvLineXml", InvLineXml);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(PPSPolicy);
            }
            sqlConnection1.Close();
        }
        catch (Exception ex)
        {
            LogError(ex);
            sqlConnection1.Close();
        }
    }

    public void SendEndosoToPPSTender2(int TaskControlID)
    {
        string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnStrPPS"].ToString();

        SqlConnection sqlConnection1 = new SqlConnection(ConnectionString);
        SqlCommand cmd = new SqlCommand();
        DataTable PPSPolicy = new DataTable();
        DataTable dt = GetYachtToPPSByTaskControlIDInfoGeneralTenderLimit2END(TaskControlID);
        DataTable dtTender = GetYachtToPPSByTaskControlIDInfoTenderLimit(TaskControlID);
        DataTable dtTenderDesc = GetYachtToPPSByTaskControlIDInfoTenderLimitDesc(TaskControlID);
        DataTable dtSurvey = GetYachtToPPSByTaskControlIDInfoSurvey(TaskControlID);
        DataTable dtReport = GetYachtToPPSByTaskControlIDInfoReport(TaskControlID);
        DataTable dtNavigation = GetNavigationLimitCollectionToPPSByTaskControlID(TaskControlID);
        System.Data.DataTable dtReinsAsl = new System.Data.DataTable();
        System.Data.DataTable dtOtherCvrgDetail = new System.Data.DataTable();
        string CoverageCodesXml = "";
        string OtherCvrgDetailXml = "";

        try
        {
            dtReinsAsl.TableName = "Coverages";
            dtReinsAsl.Columns.Add("ReinsAsl");
            dtReinsAsl.Columns.Add("Desc");
            dtReinsAsl.Columns.Add("CoveragePremium");
            dtReinsAsl.Columns.Add("Total");
            dtReinsAsl.Columns.Add("Lim1");
            dtReinsAsl.Columns.Add("Lim2");
            dtReinsAsl.Columns.Add("Lim3");
            dtReinsAsl.Columns.Add("Lim4");
            dtReinsAsl.Columns.Add("Lim5");
            dtReinsAsl.Columns.Add("Deductible");
            dtReinsAsl.Columns.Add("MinDeductible");

            DataRow row = dtReinsAsl.NewRow(); //Tender Limit con primer deducible
            row["ReinsAsl"] = "66080";
            row["Desc"] = "";
            row["CoveragePremium"] = "0.00";
            row["Total"] = "";
            if (dtTender.Rows.Count > 0)
            {
                if (dtTender.Rows[0]["TenderLimitAmount2"].ToString() == "")
                {
                    row["Lim1"] = "0.00";
                }
                else
                {
                    row["Lim1"] = dtTender.Rows[0]["TenderLimitAmount2"].ToString().Replace("$", "").Replace(",", "").Trim();
                }
                row["Lim2"] = "0.00";
                row["Lim3"] = "0.00";
                if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
                {
                    row["Lim4"] = "0.00";
                    row["Lim5"] = "0.00";
                    row["Deductible"] = "0.00";
                    row["MinDeductible"] = "0.00";
                }
                else
                {
                    string[] arrayOfDeductible3 = { "", "" };
                    string deductible3 = "";
                    arrayOfDeductible3 = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                    deductible3 = arrayOfDeductible3[0].Trim().Replace("%", "");
                    double ded3 = double.Parse(deductible3) / 100.00;
                    row["Lim4"] = ded3.ToString();
                    row["Lim5"] = "0.00";
                    row["Deductible"] = ded3.ToString();
                    row["MinDeductible"] = "0.00";
                }

            }
            else
            {
                row["Lim1"] = "0.00";
                row["Lim2"] = "0.00";
                row["Lim3"] = "0.00";
                row["Lim4"] = "0.00";
                row["Lim5"] = "0.00";
                row["Deductible"] = "0.00";
                row["MinDeductible"] = "0.00";
            }

            dtReinsAsl.Rows.Add(row);

            DataRow row2 = dtReinsAsl.NewRow(); //Tender Limit con segundo deducible
            row2["ReinsAsl"] = "63080";
            row2["Desc"] = "";
            row2["CoveragePremium"] = "0.00";
            row2["Total"] = "";
            if (dtTender.Rows.Count > 0)
            {
                if (dtTender.Rows[0]["TenderLimitAmount2"].ToString() == "")
                {
                    row2["Lim1"] = "0.00";
                }
                else
                {
                    row2["Lim1"] = dtTender.Rows[0]["TenderLimitAmount2"].ToString().Replace("$", "").Replace(",", "").Trim();
                }
                row2["Lim2"] = "0.00";
                row2["Lim3"] = "0.00";
                if (dt.Rows[0]["DeductibleDesc"].ToString() == "")
                {
                    row2["Lim4"] = "0.00";
                    row2["Lim5"] = "0.00";
                    row2["Deductible"] = "0.00";
                    row2["MinDeductible"] = "0.00";
                }
                else
                {
                    string[] arrayOfDeductible4 = { "", "" };
                    string deductible4 = "";
                    arrayOfDeductible4 = dt.Rows[0]["DeductibleDesc"].ToString().Split('/');
                    if (arrayOfDeductible4.Length > 1)
                    {

                        deductible4 = arrayOfDeductible4[1].Trim().Replace("%", "");
                        double ded4 = double.Parse(deductible4) / 100.00;
                        row2["Lim4"] = ded4.ToString();
                        row2["Lim5"] = "0.00";
                        row2["Deductible"] = ded4.ToString();
                        row2["MinDeductible"] = "0.00";
                    }
                    else
                    {
                        row2["Lim4"] = "0.00";
                        row2["Lim5"] = "0.00";
                        row2["Deductible"] = "0.00";
                        row2["MinDeductible"] = "0.00";
                    }
                }


            }
            else
            {
                row2["Lim1"] = "0.00";
                row2["Lim2"] = "0.00";
                row2["Lim3"] = "0.00";
                row2["Lim4"] = "0.00";
                row2["Lim5"] = "0.00";
                row2["Deductible"] = "0.00";
                row2["MinDeductible"] = "0.00";
            }

            dtReinsAsl.Rows.Add(row2);

            dtOtherCvrgDetail.TableName = "OtherCvrgDetail";
            dtOtherCvrgDetail.Columns.Add("ReinsAsl");
            dtOtherCvrgDetail.Columns.Add("DisplayAs");
            dtOtherCvrgDetail.Columns.Add("IndexNo");
            dtOtherCvrgDetail.Columns.Add("Limit1");
            dtOtherCvrgDetail.Columns.Add("Limit2");
            dtOtherCvrgDetail.Columns.Add("Tag");

            if (dtReinsAsl.Rows.Count > 0)
            {
                for (int i = 0; i < dtReinsAsl.Rows.Count; i++)
                {
                    int Count = 1;
                    DataRow row8 = dtOtherCvrgDetail.NewRow();
                    switch (dtReinsAsl.Rows[i]["ReinsAsl"].ToString())
                    {

                        case "66080": //Tender Limit con primer deducible
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Tender Limit - All Other Perils";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;

                        case "63080": //Tender Limit con segundo deducible
                            row8[0] = dtReinsAsl.Rows[i]["ReinsAsl"].ToString();
                            row8[1] = "Tender Limit - Windstorm";
                            row8[2] = (Count++).ToString();
                            row8[3] = dtReinsAsl.Rows[i]["Lim1"];
                            row8[4] = "0.00";
                            row8[5] = "Insured Value";
                            dtOtherCvrgDetail.Rows.Add(row8);
                            break;
                    }
                }
            }

            using (StringWriter sw = new StringWriter())
            {
                dtReinsAsl.WriteXml(sw);
                CoverageCodesXml = sw.ToString();
                XmlDocument docSave = new XmlDocument();
                docSave.LoadXml(sw.ToString());
                docSave.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + TaskControlID + "_OtherCvrg2" + ".xml");
                sw.Close();
            }

            using (StringWriter sw = new StringWriter())
            {
                dtOtherCvrgDetail.WriteXml(sw);
                OtherCvrgDetailXml = sw.ToString();
                XmlDocument docSave = new XmlDocument();
                docSave.LoadXml(sw.ToString());
                docSave.Save(System.Configuration.ConfigurationManager.AppSettings["XMLPathName"] + TaskControlID + "_OtherCvrgDetail2" + ".xml");
                sw.Close();
            }


            if (dt.Rows.Count > 0)
            {

                cmd.CommandText = "sproc_ConsumeXMLePPS-YACHT_TenderLimit2_END";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();


                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Tag", "true");
                // Add the parameters for Bldgs
                if (txtSuffix.Text.Trim() != "" && txtSuffix.Text.Trim() != "00")
                {
                    cmd.Parameters.AddWithValue("@PolicyID", txtPolicyType.Text.Trim() + txtPolicyNo.Text.Trim() + "-" + txtSuffix.Text.Trim());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PolicyID", txtPolicyType.Text.Trim() + txtPolicyNo.Text.Trim());
                }
                cmd.Parameters.AddWithValue("@Descrip", SqlDbType.NVarChar).Value = DBNull.Value;
                if (dt.Rows[0]["HomeportDesc"].ToString().Trim() == "")
                {
                    cmd.Parameters.AddWithValue("@Location", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Location", dt.Rows[0]["HomeportDesc"].ToString().Trim());
                }
                cmd.Parameters.AddWithValue("@Island", "4");

                double tender2;

                if (dtTender.Rows.Count > 0 && dtTender.Rows[0]["TenderLimitAmount2"].ToString().Replace("$", "").Replace(",", "").Trim() != "")
                {
                    tender2 = double.Parse(dtTender.Rows[0]["TenderLimitAmount2"].ToString().Replace("$", "").Replace(",", "").Trim());
                }
                else
                {
                    tender2 = 0.00;
                }

                cmd.Parameters.AddWithValue("@InsVal", tender2.ToString());
                cmd.Parameters.AddWithValue("@AnyNum", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@PayeeID", dt.Rows[0]["BankPPSID"].ToString().Trim());
                cmd.Parameters.AddWithValue("@LoanNo", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@PayeeID2", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@LoanNo2", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Families", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@RowHouse", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Rented", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Construction", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@ProtectionClass", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@YearBuilt", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@FireDistrict", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Occupancy", SqlDbType.NVarChar).Value = DBNull.Value;
                string navigationLimitAll = "";
                if (dtNavigation.Rows.Count == 1)
                {
                    cmd.Parameters.Add("@NavLimit", dtNavigation.Rows[0]["NavigationLimitDesc"].ToString().Trim());
                }
                else if (dtNavigation.Rows.Count > 1)
                {
                    for (int i = 0; i < dtNavigation.Rows.Count; i++)
                    {
                        if (i == 0)
                            navigationLimitAll = dtNavigation.Rows[i]["NavigationLimitDesc"].ToString().Trim();
                        else
                            navigationLimitAll = navigationLimitAll + " " + dtNavigation.Rows[i]["NavigationLimitDesc"].ToString().Trim();
                    }

                    cmd.Parameters.Add("@NavLimit", navigationLimitAll);
                }
                else
                {
                    cmd.Parameters.Add("@NavLimit", SqlDbType.NVarChar).Value = DBNull.Value;

                }
                if (dtTender.Rows.Count > 0)
                {
                    if (dtTender.Rows[0]["TenderDesc2"].ToString().Trim() != "" && dtTender.Rows[0]["TenderSerial2"].ToString().Trim() != "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderDesc2"].ToString().Trim() + " " + dtTender.Rows[0]["TenderSerial2"].ToString().Trim());
                    else if (dtTender.Rows[0]["TenderDesc2"].ToString().Trim() != "" && dtTender.Rows[0]["TenderSerial2"].ToString().Trim() == "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderDesc2"].ToString().Trim());
                    else if (dtTender.Rows[0]["TenderDesc2"].ToString().Trim() == "" && dtTender.Rows[0]["TenderSerial2"].ToString().Trim() != "")
                        cmd.Parameters.Add("@TenderText", dtTender.Rows[0]["TenderSerial2"].ToString().Trim());
                    else
                        cmd.Parameters.Add("@TenderText", SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.Add("@TenderText", SqlDbType.NVarChar).Value = DBNull.Value;
                }

                cmd.Parameters.Add("@TrailerText", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@VName", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@Make", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@Model", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@HIN", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@LOA", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.Add("@VesselProp", SqlDbType.NVarChar).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@Storeys", SqlDbType.NVarChar).Value = DBNull.Value;
                //OtherCoverage & OtherCovrgDetail
                cmd.Parameters.AddWithValue("@CoverageCodesXml", CoverageCodesXml);
                cmd.Parameters.AddWithValue("@OtherCvrgDetailXml", OtherCvrgDetailXml);

                // create data adapter
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(PPSPolicy);

                //cmd.ExecuteReader();
            }

            sqlConnection1.Close();
        }
        catch (Exception ex)
        {
            LogError(ex);
            sqlConnection1.Close();
        }
    }

    protected DataTable GetYachtCoveragesPPS()
    {
        try
        {
            SqlConnection cn = new SqlConnection("Data Source=gic-msql\\ppssqlserver;Initial Catalog=TestGic;User ID=urclaims;password=3G@TD@t!1");
            //SqlConnection cn = new SqlConnection(@"Data Source=192.168.1.22\ppssqlserver;Initial Catalog=GICPPSDATA;User ID=urclaims;password=3G@TD@t!1");
            System.Data.DataTable table = new System.Data.DataTable();

            using (var con = cn)
            {
                using (var cmd = new SqlCommand("sproc_ConsumeXMLePPS-YACHT_Verify_Coverage", con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    if (txtSuffix.Text.Trim() != "" && txtSuffix.Text.Trim() != "00")
                    {
                        cmd.Parameters.AddWithValue("@PolicyID", txtPolicyType.Text.Trim() + txtPolicyNo.Text.Trim() + "-" + txtSuffix.Text.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@PolicyID", txtPolicyType.Text.Trim() + txtPolicyNo.Text.Trim());
                    }
                    da.Fill(table);
                }
            }

            return table;
        }
        catch (Exception exp)
        {
            lblRecHeader.Text = exp.Message;
            mpeSeleccion.Show();
            System.Data.DataTable table = new System.Data.DataTable();
            return table;
        }
    }

    private static DataTable GetYachtToPPSByTaskControlIDEndoso(int TaskControlID)
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
            dt = exec.GetQuery("GetYachtEndosoToPPSByTaskControlID", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve the Stored Procedure called GetYachtToPPSByTaskControlIDEndoso.", ex);
        }
    }

    private static DataTable GetOPPEndorsementToPPSByTaskControlID(int TaskControlID)
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
            dt = exec.GetQuery("GetOPPEndorsementToPPSByTaskControlID", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve the Stored Procedure called GetOPPEndorsementToPPSByTaskControlID.", ex);
        }
    }

    private static DataTable GetCommissionAgentRateByAgentIDEND(string TaskControlID)
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
        DataTable dt = null;
        try
        {
            dt = exec.GetQuery("GetCommissionAgentRateByAgentIDEND", xmlDoc);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve agent rates.", ex);
        }
    }

    
}